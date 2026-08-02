"""Resource dependency graph, automatic links, price history and recalculation runs."""
from __future__ import annotations

import json
from datetime import datetime, timezone
from decimal import Decimal
from uuid import uuid4

from flask import Blueprint, jsonify
from sqlalchemy import Column, DateTime, MetaData, String, Table, Text, select

from api.models import BudgetItem, Project, Resource
from api.resource_budget_lineage import ResourceBudgetLineageService, resource_budget_links
from api.resource_decimal import resources_decimal

metadata = MetaData()
resource_price_history = Table(
    "resource_price_history", metadata,
    Column("id", String(100), primary_key=True),
    Column("project_code", String(100), nullable=False, index=True),
    Column("resource_id", String(100), nullable=False, index=True),
    Column("old_price", String(100), nullable=False),
    Column("new_price", String(100), nullable=False),
    Column("source", String(80), nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
)
dependency_recalculation_runs = Table(
    "dependency_recalculation_runs", metadata,
    Column("id", String(100), primary_key=True),
    Column("project_code", String(100), nullable=False, index=True),
    Column("scope", String(30), nullable=False),
    Column("resource_id", String(100), nullable=True, index=True),
    Column("status", String(30), nullable=False),
    Column("result_json", Text, nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
)


def legacy_resource_id(value: int | str) -> str:
    return f"legacy-resource-{value}"


def legacy_budget_id(value: int | str) -> str:
    return f"legacy-{value}"


class ResourceDependencyGraphService:
    def __init__(self, engine, session_factory):
        self.engine = engine
        self.session_factory = session_factory
        self.lineage = ResourceBudgetLineageService(engine)
        metadata.create_all(engine)

    def auto_link_project(self, project_id: int) -> dict:
        """Create explicit links only where legacy pcces_code equals resource code."""
        db = self.session_factory()
        created: list[dict] = []
        try:
            project = db.query(Project).filter(Project.id == project_id).first()
            if not project:
                raise ValueError("project not found")
            resources = {row.code: row for row in db.query(Resource).filter(Resource.project_id == project_id).all() if row.code}
            items = db.query(BudgetItem).filter(BudgetItem.project_id == project_id).all()
            for item in items:
                resource = resources.get(item.pcces_code)
                if not resource:
                    continue
                created.append(self.lineage.link(project.code, legacy_resource_id(resource.id), legacy_budget_id(item.id)))
            return {"project_id": project_id, "project_code": project.code, "matched_links": len(created), "links": created}
        finally:
            db.close()

    def record_price(self, project_code: str, resource_id: str, old_price: str, new_price: str, source: str) -> dict | None:
        old_text, new_text = str(old_price), str(new_price)
        if Decimal(old_text) == Decimal(new_text):
            return None
        row = {
            "id": str(uuid4()), "project_code": project_code, "resource_id": resource_id,
            "old_price": old_text, "new_price": new_text, "source": source,
            "created_at": datetime.now(timezone.utc),
        }
        with self.engine.begin() as conn:
            conn.execute(resource_price_history.insert().values(**row))
        return {**row, "created_at": row["created_at"].isoformat(),
                "deep_link": f"/app/projects/by-code/{project_code}/traceability?resource={resource_id}&history={row['id']}"}

    def record_completed_run(self, project_code: str, resource_id: str | None, result: dict, scope: str = "RESOURCE") -> dict:
        """Record propagation already performed by the legacy bridge without running it twice."""
        return self._save_run(project_code, scope, resource_id, result)

    def recalculate_resource(self, project_code: str, resource_id: str, trigger: str = "DEPENDENCY_GRAPH_LOCAL") -> dict:
        lineage = self.lineage.propagate(resource_id, trigger)
        return self._save_run(project_code, "RESOURCE", resource_id, {
            "updated_items": len(lineage), "lineage": lineage,
        })

    def recalculate_project(self, project_code: str) -> dict:
        with self.engine.connect() as conn:
            resource_ids = [row[0] for row in conn.execute(select(resource_budget_links.c.resource_id).where(
                resource_budget_links.c.project_code == project_code
            ).distinct())]
        results = []
        for resource_id in resource_ids:
            rows = self.lineage.propagate(resource_id, "DEPENDENCY_GRAPH_PROJECT")
            results.append({"resource_id": resource_id, "updated_items": len(rows), "lineage": rows})
        return self._save_run(project_code, "PROJECT", None, {
            "resources": len(resource_ids), "updated_items": sum(r["updated_items"] for r in results),
            "results": results,
        })

    def _save_run(self, project_code: str, scope: str, resource_id: str | None, result: dict) -> dict:
        run_id = str(uuid4())
        now = datetime.now(timezone.utc)
        with self.engine.begin() as conn:
            conn.execute(dependency_recalculation_runs.insert().values(
                id=run_id, project_code=project_code, scope=scope, resource_id=resource_id,
                status="COMPLETED", result_json=json.dumps(result, ensure_ascii=False, sort_keys=True), created_at=now,
            ))
        return {"id": run_id, "project_code": project_code, "scope": scope, "resource_id": resource_id,
                "status": "COMPLETED", "result": result, "created_at": now.isoformat(),
                "deep_link": f"/app/projects/by-code/{project_code}/traceability?run={run_id}"}

    def graph(self, project_code: str) -> dict:
        with self.engine.connect() as conn:
            links = conn.execute(select(resource_budget_links).where(
                resource_budget_links.c.project_code == project_code
            )).mappings().all()
            history = conn.execute(select(resource_price_history).where(
                resource_price_history.c.project_code == project_code
            ).order_by(resource_price_history.c.created_at.desc())).mappings().all()
            runs = conn.execute(select(dependency_recalculation_runs).where(
                dependency_recalculation_runs.c.project_code == project_code
            ).order_by(dependency_recalculation_runs.c.created_at.desc())).mappings().all()
        nodes: dict[str, dict] = {}
        edges = []
        for link in links:
            nodes.setdefault(link["resource_id"], {"id": link["resource_id"], "type": "RESOURCE"})
            nodes.setdefault(link["budget_item_id"], {"id": link["budget_item_id"], "type": "BUDGET_ITEM"})
            edges.append({"from": link["resource_id"], "to": link["budget_item_id"], "type": "PRICE_DEPENDENCY"})
        return {
            "project_code": project_code, "nodes": list(nodes.values()), "edges": edges,
            "price_history": [{**dict(row), "created_at": row["created_at"].isoformat(),
                "deep_link": f"/app/projects/by-code/{project_code}/traceability?history={row['id']}"} for row in history],
            "runs": [{"id": row["id"], "scope": row["scope"], "resource_id": row["resource_id"],
                "status": row["status"], "result": json.loads(row["result_json"]),
                "created_at": row["created_at"].isoformat(),
                "deep_link": f"/app/projects/by-code/{project_code}/traceability?run={row['id']}"} for row in runs],
        }


def build_resource_dependency_blueprint(service: ResourceDependencyGraphService, resolve_user_id):
    bp = Blueprint("resource_dependency", __name__, url_prefix="/api/dependency-graph")
    def authenticated(): return resolve_user_id() is not None

    @bp.post("/projects/<int:project_id>/auto-link")
    def auto_link(project_id: int):
        if not authenticated(): return jsonify({"code":"UNAUTHORIZED"}), 401
        try: return jsonify(service.auto_link_project(project_id))
        except ValueError as exc: return jsonify({"code":"INVALID_ARGUMENT","detail":str(exc)}), 400

    @bp.post("/projects/<project_code>/recalculate")
    def recalculate_project(project_code: str):
        if not authenticated(): return jsonify({"code":"UNAUTHORIZED"}), 401
        return jsonify(service.recalculate_project(project_code))

    @bp.post("/projects/<project_code>/resources/<resource_id>/recalculate")
    def recalculate_resource(project_code: str, resource_id: str):
        if not authenticated(): return jsonify({"code":"UNAUTHORIZED"}), 401
        return jsonify(service.recalculate_resource(project_code, resource_id))

    @bp.get("/projects/<project_code>")
    def graph(project_code: str):
        if not authenticated(): return jsonify({"code":"UNAUTHORIZED"}), 401
        return jsonify(service.graph(project_code))

    return bp


def install_resource_automation(app, service: ResourceDependencyGraphService) -> None:
    """Wrap successful legacy writes without altering their public contracts."""
    for endpoint in ("create_budget_item", "update_budget_item"):
        if endpoint not in app.view_functions: continue
        original = app.view_functions[endpoint]
        def budget_wrapper(*args, _original=original, **kwargs):
            response = _original(*args, **kwargs)
            status = response[1] if isinstance(response, tuple) else getattr(response, "status_code", 200)
            if status < 400:
                project_id = kwargs.get("project_id") or (args[0] if args else None)
                if project_id is not None: service.auto_link_project(int(project_id))
            return response
        app.view_functions[endpoint] = budget_wrapper

    for endpoint in ("update_resource", "update_resource_price"):
        if endpoint not in app.view_functions: continue
        original = app.view_functions[endpoint]
        def resource_wrapper(*args, _original=original, _endpoint=endpoint, **kwargs):
            project_id = kwargs.get("project_id") or (args[0] if args else None)
            resource_id = kwargs.get("resource_id") or (args[1] if len(args) > 1 else None)
            decimal_id = legacy_resource_id(resource_id)
            with service.engine.connect() as conn:
                before = conn.execute(select(resources_decimal.c.unit_price).where(resources_decimal.c.id == decimal_id)).first()
            response = _original(*args, **kwargs)
            status = response[1] if isinstance(response, tuple) else getattr(response, "status_code", 200)
            if status < 400 and project_id is not None:
                db = service.session_factory()
                try:
                    project = db.query(Project).filter(Project.id == int(project_id)).first()
                    with service.engine.connect() as conn:
                        after = conn.execute(select(resources_decimal.c.unit_price).where(resources_decimal.c.id == decimal_id)).first()
                    if project and before and after:
                        service.record_price(project.code, decimal_id, str(before[0]), str(after[0]), _endpoint.upper())
                        service.auto_link_project(int(project_id))
                        payload = response[0].get_json() if isinstance(response, tuple) and hasattr(response[0], "get_json") else (response.get_json() if hasattr(response, "get_json") else {})
                        service.record_completed_run(project.code, decimal_id, {
                            "updated_items": int((payload or {}).get("propagated_items", 0)),
                            "source": _endpoint.upper(),
                        })
                finally: db.close()
            return response
        app.view_functions[endpoint] = resource_wrapper
