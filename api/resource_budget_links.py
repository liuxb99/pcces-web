"""Phase 3 project resource and budget-item bidirectional traceability."""
from __future__ import annotations

from flask import Blueprint, jsonify, request
from sqlalchemy import and_, func, select

from api.budget_decimal import budget_items_decimal
from api.resource_budget_lineage import resource_budget_links
from api.resource_decimal import resources_decimal
from api.resource_operations import ResourceOperationsService


class ResourceBudgetLinkService:
    def __init__(self, engine):
        self.engine = engine

    @staticmethod
    def _page(limit: int, offset: int) -> tuple[int, int]:
        return min(max(limit or 50, 1), 200), max(offset, 0)

    def list_project_resources(self, project_code: str, query: str = "", limit: int = 50, offset: int = 0) -> dict:
        limit, offset = self._page(limit, offset)
        q = query.strip().lower()
        with self.engine.connect() as conn:
            rows = conn.execute(
                select(resource_budget_links.c.resource_id, func.count(resource_budget_links.c.budget_item_id).label("reference_count"))
                .where(resource_budget_links.c.project_code == project_code)
                .group_by(resource_budget_links.c.resource_id)
                .order_by(resource_budget_links.c.resource_id)
            ).mappings().all()
            items = []
            for row in rows:
                resource = conn.execute(select(resources_decimal).where(resources_decimal.c.id == row["resource_id"])).mappings().first()
                if not resource:
                    continue
                code, name = str(resource.get("code") or ""), str(resource.get("name") or "")
                if q and q not in code.lower() and q not in name.lower():
                    continue
                items.append({**dict(resource), "reference_count": int(row["reference_count"]),
                              "deep_link": f"/app/project-resources?project={project_code}&resource={row['resource_id']}"})
        total = len(items)
        return {"items": items[offset:offset + limit], "total": total, "limit": limit, "offset": offset}

    def list_resource_references(self, project_code: str, resource_id: str, limit: int = 50, offset: int = 0) -> dict:
        limit, offset = self._page(limit, offset)
        with self.engine.connect() as conn:
            stmt = select(resource_budget_links, budget_items_decimal).join(
                budget_items_decimal, budget_items_decimal.c.id == resource_budget_links.c.budget_item_id
            ).where(and_(resource_budget_links.c.project_code == project_code,
                         resource_budget_links.c.resource_id == resource_id)).order_by(budget_items_decimal.c.id)
            rows = conn.execute(stmt).mappings().all()
        items = [{"link_id": row["id"], "project_code": project_code, "resource_id": resource_id,
                  "budget_item_id": row["budget_item_id"], "item_type": row.get("kind"),
                  "quantity": str(row.get("quantity")), "unit_price": str(row.get("unit_price")),
                  "amount": str(row.get("amount")), "row_version": row.get("row_version"),
                  "deep_link": f"/app/budget/{project_code}?item={row['budget_item_id']}"} for row in rows]
        return {"items": items[offset:offset + limit], "total": len(items), "limit": limit, "offset": offset}

    def unlink(self, project_code: str, resource_id: str, budget_item_id: str) -> bool:
        with self.engine.begin() as conn:
            result = conn.execute(resource_budget_links.delete().where(and_(
                resource_budget_links.c.project_code == project_code,
                resource_budget_links.c.resource_id == resource_id,
                resource_budget_links.c.budget_item_id == budget_item_id,
            )))
        return result.rowcount == 1


def build_resource_budget_links_blueprint(service: ResourceBudgetLinkService, resolve_user_id):
    bp = Blueprint("resource_budget_links", __name__, url_prefix="/api/decimal-resources")
    operations = ResourceOperationsService(service.engine)

    def authenticated():
        return resolve_user_id() is not None

    @bp.get("/projects/<project_code>/resources")
    def project_resources(project_code: str):
        if not authenticated(): return jsonify({"code": "UNAUTHORIZED"}), 401
        return jsonify(service.list_project_resources(project_code, request.args.get("q", ""),
            request.args.get("limit", 50, type=int), request.args.get("offset", 0, type=int)))

    @bp.get("/projects/<project_code>/resources/<resource_id>/references")
    def resource_references(project_code: str, resource_id: str):
        if not authenticated(): return jsonify({"code": "UNAUTHORIZED"}), 401
        return jsonify(service.list_resource_references(project_code, resource_id,
            request.args.get("limit", 50, type=int), request.args.get("offset", 0, type=int)))

    @bp.delete("/projects/<project_code>/resources/<resource_id>/references/<budget_item_id>")
    def unlink(project_code: str, resource_id: str, budget_item_id: str):
        if not authenticated(): return jsonify({"code": "UNAUTHORIZED"}), 401
        if not service.unlink(project_code, resource_id, budget_item_id):
            return jsonify({"code": "NOT_FOUND", "detail": "resource reference not found"}), 404
        return "", 204

    @bp.post("/projects/<project_code>/replace")
    def replace(project_code: str):
        if not authenticated(): return jsonify({"code":"UNAUTHORIZED"}),401
        body=request.get_json(silent=True) or {}
        try:
            return jsonify(operations.replace(project_code,str(body.get("source_resource_id","")),
                str(body.get("target_resource_id","")),str(resolve_user_id())))
        except ValueError as exc: return jsonify({"code":"INVALID_ARGUMENT","detail":str(exc)}),400
        except LookupError as exc: return jsonify({"code":"NOT_FOUND","detail":str(exc)}),404

    @bp.post("/batch-prices")
    def batch_prices():
        if not authenticated(): return jsonify({"code":"UNAUTHORIZED"}),401
        body=request.get_json(silent=True) or {}
        try: return jsonify(operations.batch_prices(body.get("updates") or [],str(body.get("trigger") or "BATCH_RESOURCE_PRICE_UPDATE")))
        except ValueError as exc: return jsonify({"code":"INVALID_ARGUMENT","detail":str(exc)}),400
        except LookupError as exc: return jsonify({"code":"NOT_FOUND","detail":str(exc)}),404
        except RuntimeError as exc: return jsonify({"code":"CONFLICT","detail":str(exc)}),409

    return bp
