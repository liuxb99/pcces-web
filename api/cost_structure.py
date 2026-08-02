"""Phase 4 cost-structure catalog and project assignment.

Legacy anchors: CostStructureTypePicker, CostStructureImport,
FormBudgetCostStructurePicker and FormBudgetCostProperty.
"""
from __future__ import annotations

from datetime import datetime, timezone
from uuid import uuid4

from flask import Blueprint, jsonify, request
from sqlalchemy import Boolean, Column, DateTime, Integer, MetaData, String, Table, Text, and_, select

metadata = MetaData()

cost_structure_types = Table(
    "cost_structure_types", metadata,
    Column("id", String(100), primary_key=True),
    Column("code", String(100), nullable=False, unique=True, index=True),
    Column("name", String(300), nullable=False),
    Column("description", Text, nullable=False, default=""),
    Column("source", String(100), nullable=False),
    Column("version", String(50), nullable=False),
    Column("enabled", Boolean, nullable=False, default=True),
    Column("created_by", String(100), nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
    Column("updated_at", DateTime(timezone=True), nullable=False),
    Column("row_version", Integer, nullable=False, default=1),
)

project_cost_structures = Table(
    "project_cost_structures", metadata,
    Column("project_code", String(100), primary_key=True),
    Column("cost_structure_type_id", String(100), nullable=False, index=True),
    Column("issue", String(100), nullable=False, default="BUD"),
    Column("assigned_by", String(100), nullable=False),
    Column("assigned_at", DateTime(timezone=True), nullable=False),
    Column("row_version", Integer, nullable=False, default=1),
)


class CostStructureService:
    def __init__(self, engine):
        self.engine = engine
        metadata.create_all(engine)

    @staticmethod
    def _serialize(row) -> dict:
        item = dict(row)
        for key in ("created_at", "updated_at", "assigned_at"):
            if item.get(key) is not None:
                item[key] = item[key].isoformat()
        return item

    def list_types(self, enabled_only: bool = True) -> list[dict]:
        stmt = select(cost_structure_types)
        if enabled_only:
            stmt = stmt.where(cost_structure_types.c.enabled.is_(True))
        stmt = stmt.order_by(cost_structure_types.c.code)
        with self.engine.connect() as conn:
            return [self._serialize(row) for row in conn.execute(stmt).mappings().all()]

    def save_type(self, type_id: str, body: dict, actor: str) -> dict:
        code = str(body.get("code", "")).strip().upper()
        name = str(body.get("name", "")).strip()
        if not code or not name:
            raise ValueError("code and name are required")
        now = datetime.now(timezone.utc)
        with self.engine.begin() as conn:
            current = conn.execute(select(cost_structure_types).where(cost_structure_types.c.id == type_id)).mappings().first()
            requested = int(body.get("row_version", 0))
            values = dict(
                code=code, name=name,
                description=str(body.get("description", current["description"] if current else "")),
                source=str(body.get("source", current["source"] if current else "LEGACY")).strip().upper(),
                version=str(body.get("version", current["version"] if current else "1")),
                enabled=bool(body.get("enabled", current["enabled"] if current else True)),
                updated_at=now,
            )
            if current:
                if requested != int(current["row_version"]):
                    raise RuntimeError("CONFLICT")
                result = conn.execute(cost_structure_types.update().where(and_(
                    cost_structure_types.c.id == type_id,
                    cost_structure_types.c.row_version == requested,
                )).values(**values, row_version=requested + 1))
                if result.rowcount != 1:
                    raise RuntimeError("CONFLICT")
            else:
                conn.execute(cost_structure_types.insert().values(
                    id=type_id or str(uuid4()), created_by=actor, created_at=now,
                    row_version=1, **values,
                ))
        return self.get_type(type_id)

    def get_type(self, type_id: str) -> dict:
        with self.engine.connect() as conn:
            row = conn.execute(select(cost_structure_types).where(cost_structure_types.c.id == type_id)).mappings().first()
        if not row:
            raise LookupError("cost structure type not found")
        return self._serialize(row)

    def assign_project(self, project_code: str, type_id: str, issue: str, row_version: int, actor: str) -> dict:
        project_code = project_code.strip()
        issue = issue.strip().upper() or "BUD"
        if not project_code:
            raise ValueError("project_code is required")
        if issue not in {"BUD", "BID"}:
            raise ValueError("issue must be BUD or BID")
        now = datetime.now(timezone.utc)
        with self.engine.begin() as conn:
            structure = conn.execute(select(cost_structure_types).where(and_(
                cost_structure_types.c.id == type_id,
                cost_structure_types.c.enabled.is_(True),
            ))).mappings().first()
            if not structure:
                raise LookupError("enabled cost structure type not found")
            current = conn.execute(select(project_cost_structures).where(project_cost_structures.c.project_code == project_code)).mappings().first()
            if current:
                if int(current["row_version"]) != int(row_version):
                    raise RuntimeError("CONFLICT")
                conn.execute(project_cost_structures.update().where(and_(
                    project_cost_structures.c.project_code == project_code,
                    project_cost_structures.c.row_version == row_version,
                )).values(cost_structure_type_id=type_id, issue=issue, assigned_by=actor,
                         assigned_at=now, row_version=row_version + 1))
            else:
                if int(row_version) != 0:
                    raise RuntimeError("CONFLICT")
                conn.execute(project_cost_structures.insert().values(
                    project_code=project_code, cost_structure_type_id=type_id, issue=issue,
                    assigned_by=actor, assigned_at=now, row_version=1,
                ))
        return self.get_project(project_code)

    def get_project(self, project_code: str) -> dict:
        stmt = select(project_cost_structures, cost_structure_types.c.code.label("type_code"),
                      cost_structure_types.c.name.label("type_name"), cost_structure_types.c.version.label("type_version"))\
            .join(cost_structure_types, cost_structure_types.c.id == project_cost_structures.c.cost_structure_type_id)\
            .where(project_cost_structures.c.project_code == project_code)
        with self.engine.connect() as conn:
            row = conn.execute(stmt).mappings().first()
        if not row:
            raise LookupError("project cost structure not found")
        item = self._serialize(row)
        item["deep_link"] = f"/app/cost-structure?project={project_code}"
        return item


def build_cost_structure_blueprint(service: CostStructureService, resolve_user_id):
    bp = Blueprint("cost_structure", __name__, url_prefix="/api/cost-structures")

    def actor() -> str:
        value = resolve_user_id()
        if value is None:
            raise PermissionError("authentication required")
        return str(value)

    @bp.get("/types")
    def list_types():
        enabled_only = request.args.get("enabled_only", "true").lower() not in {"false", "0", "no"}
        return jsonify(service.list_types(enabled_only))

    @bp.put("/types/<type_id>")
    def save_type(type_id: str):
        try:
            return jsonify(service.save_type(type_id, request.get_json(silent=True) or {}, actor()))
        except PermissionError as exc:
            return jsonify({"code": "UNAUTHORIZED", "detail": str(exc)}), 401
        except ValueError as exc:
            return jsonify({"code": "INVALID_ARGUMENT", "detail": str(exc)}), 400
        except RuntimeError:
            return jsonify({"code": "CONFLICT", "detail": "stale row_version"}), 409

    @bp.get("/projects/<project_code>")
    def get_project(project_code: str):
        try:
            return jsonify(service.get_project(project_code))
        except LookupError as exc:
            return jsonify({"code": "NOT_FOUND", "detail": str(exc)}), 404

    @bp.put("/projects/<project_code>")
    def assign_project(project_code: str):
        body = request.get_json(silent=True) or {}
        try:
            return jsonify(service.assign_project(project_code, str(body.get("cost_structure_type_id", "")),
                                                  str(body.get("issue", "BUD")), int(body.get("row_version", 0)), actor()))
        except PermissionError as exc:
            return jsonify({"code": "UNAUTHORIZED", "detail": str(exc)}), 401
        except LookupError as exc:
            return jsonify({"code": "NOT_FOUND", "detail": str(exc)}), 404
        except ValueError as exc:
            return jsonify({"code": "INVALID_ARGUMENT", "detail": str(exc)}), 400
        except RuntimeError:
            return jsonify({"code": "CONFLICT", "detail": "stale row_version"}), 409

    return bp
