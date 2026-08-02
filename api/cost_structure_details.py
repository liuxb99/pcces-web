"""Phase 4 cost-structure import, fee categories, and budget item cost properties.

Legacy anchors: CostStructureImport.cs and FormBudgetCostProperty.cs.
"""
from __future__ import annotations

import json
from datetime import datetime, timezone
from uuid import uuid4

from flask import Blueprint, jsonify, request
from sqlalchemy import Boolean, Column, DateTime, Integer, MetaData, Numeric, String, Table, Text, and_, delete, select

metadata = MetaData()

cost_structure_categories = Table(
    "cost_structure_categories", metadata,
    Column("id", String(100), primary_key=True),
    Column("cost_structure_type_id", String(100), nullable=False, index=True),
    Column("code", String(100), nullable=False),
    Column("name", String(300), nullable=False),
    Column("kind", String(30), nullable=False),
    Column("sequence", Integer, nullable=False),
    Column("rate", Numeric(28, 8), nullable=False),
    Column("enabled", Boolean, nullable=False, default=True),
    Column("row_version", Integer, nullable=False, default=1),
)

budget_item_cost_properties = Table(
    "budget_item_cost_properties", metadata,
    Column("project_code", String(100), primary_key=True),
    Column("budget_item_id", String(100), primary_key=True),
    Column("cost_category_id", String(100), nullable=False, index=True),
    Column("cost_kind", String(30), nullable=False),
    Column("sign", Integer, nullable=False, default=1),
    Column("rate", Numeric(28, 8), nullable=False),
    Column("updated_by", String(100), nullable=False),
    Column("updated_at", DateTime(timezone=True), nullable=False),
    Column("row_version", Integer, nullable=False, default=1),
)

cost_structure_import_runs = Table(
    "cost_structure_import_runs", metadata,
    Column("id", String(100), primary_key=True),
    Column("cost_structure_type_id", String(100), nullable=False, index=True),
    Column("only_structure", Boolean, nullable=False),
    Column("status", String(30), nullable=False),
    Column("total_rows", Integer, nullable=False),
    Column("imported_rows", Integer, nullable=False),
    Column("errors_json", Text, nullable=False),
    Column("created_by", String(100), nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
)

_ALLOWED_KINDS = {"DIRECT", "INDIRECT", "MANAGEMENT", "TAX", "PERCENT", "ADJUSTMENT"}


class CostStructureDetailService:
    def __init__(self, engine):
        self.engine = engine
        metadata.create_all(engine)

    def import_definition(self, type_id: str, payload: dict, actor: str, only_structure: bool = False) -> dict:
        categories = payload.get("categories") or []
        if not isinstance(categories, list) or not categories:
            raise ValueError("categories must be a non-empty list")
        normalized, errors = [], []
        seen = set()
        for index, row in enumerate(categories, start=1):
            try:
                code = str(row.get("code", "")).strip().upper()
                name = str(row.get("name", "")).strip()
                kind = str(row.get("kind", "DIRECT")).strip().upper()
                if not code or not name:
                    raise ValueError("code and name are required")
                if code in seen:
                    raise ValueError("duplicate category code")
                if kind not in _ALLOWED_KINDS:
                    raise ValueError("unsupported cost kind")
                seen.add(code)
                normalized.append({
                    "id": str(row.get("id") or f"{type_id}:{code}"),
                    "cost_structure_type_id": type_id,
                    "code": code,
                    "name": name,
                    "kind": kind,
                    "sequence": int(row.get("sequence", index)),
                    "rate": str(row.get("rate", "0")),
                    "enabled": bool(row.get("enabled", True)),
                    "row_version": 1,
                })
            except Exception as exc:
                errors.append({"row": index, "detail": str(exc)})
        if errors:
            raise ValueError(json.dumps(errors, ensure_ascii=False))
        run_id, now = str(uuid4()), datetime.now(timezone.utc)
        with self.engine.begin() as conn:
            from api.cost_structure import cost_structure_types
            target = conn.execute(select(cost_structure_types.c.id).where(cost_structure_types.c.id == type_id)).first()
            if not target:
                raise LookupError("cost structure type not found")
            conn.execute(delete(cost_structure_categories).where(cost_structure_categories.c.cost_structure_type_id == type_id))
            conn.execute(cost_structure_categories.insert(), normalized)
            conn.execute(cost_structure_import_runs.insert().values(
                id=run_id, cost_structure_type_id=type_id, only_structure=bool(only_structure),
                status="COMPLETED", total_rows=len(normalized), imported_rows=len(normalized),
                errors_json="[]", created_by=actor, created_at=now,
            ))
        return {"id": run_id, "status": "COMPLETED", "cost_structure_type_id": type_id,
                "only_structure": bool(only_structure), "total_rows": len(normalized), "imported_rows": len(normalized)}

    def list_categories(self, type_id: str) -> list[dict]:
        stmt = select(cost_structure_categories).where(
            cost_structure_categories.c.cost_structure_type_id == type_id
        ).order_by(cost_structure_categories.c.sequence, cost_structure_categories.c.code)
        with self.engine.connect() as conn:
            return [dict(row) for row in conn.execute(stmt).mappings().all()]

    def save_item_property(self, project_code: str, item_id: str, body: dict, actor: str) -> dict:
        category_id = str(body.get("cost_category_id", "")).strip()
        cost_kind = str(body.get("cost_kind", "DIRECT")).strip().upper()
        sign = int(body.get("sign", 1))
        if not category_id:
            raise ValueError("cost_category_id is required")
        if cost_kind not in _ALLOWED_KINDS:
            raise ValueError("unsupported cost kind")
        if sign not in {-1, 1}:
            raise ValueError("sign must be -1 or 1")
        now = datetime.now(timezone.utc)
        requested = int(body.get("row_version", 0))
        with self.engine.begin() as conn:
            category = conn.execute(select(cost_structure_categories).where(and_(
                cost_structure_categories.c.id == category_id,
                cost_structure_categories.c.enabled.is_(True),
            ))).mappings().first()
            if not category:
                raise LookupError("enabled cost category not found")
            current = conn.execute(select(budget_item_cost_properties).where(and_(
                budget_item_cost_properties.c.project_code == project_code,
                budget_item_cost_properties.c.budget_item_id == item_id,
            ))).mappings().first()
            values = dict(cost_category_id=category_id, cost_kind=cost_kind, sign=sign,
                          rate=str(body.get("rate", category["rate"])), updated_by=actor, updated_at=now)
            if current:
                if requested != int(current["row_version"]):
                    raise RuntimeError("CONFLICT")
                result = conn.execute(budget_item_cost_properties.update().where(and_(
                    budget_item_cost_properties.c.project_code == project_code,
                    budget_item_cost_properties.c.budget_item_id == item_id,
                    budget_item_cost_properties.c.row_version == requested,
                )).values(**values, row_version=requested + 1))
                if result.rowcount != 1:
                    raise RuntimeError("CONFLICT")
            else:
                if requested != 0:
                    raise RuntimeError("CONFLICT")
                conn.execute(budget_item_cost_properties.insert().values(
                    project_code=project_code, budget_item_id=item_id, row_version=1, **values,
                ))
        return self.get_item_property(project_code, item_id)

    def get_item_property(self, project_code: str, item_id: str) -> dict:
        stmt = select(
            budget_item_cost_properties,
            cost_structure_categories.c.code.label("category_code"),
            cost_structure_categories.c.name.label("category_name"),
        ).join(cost_structure_categories, cost_structure_categories.c.id == budget_item_cost_properties.c.cost_category_id).where(and_(
            budget_item_cost_properties.c.project_code == project_code,
            budget_item_cost_properties.c.budget_item_id == item_id,
        ))
        with self.engine.connect() as conn:
            row = conn.execute(stmt).mappings().first()
        if not row:
            raise LookupError("budget item cost property not found")
        item = dict(row)
        item["rate"] = str(item["rate"])
        item["updated_at"] = item["updated_at"].isoformat()
        item["deep_link"] = f"/app/budget/{project_code}?item={item_id}&panel=cost-property"
        return item


def build_cost_structure_detail_blueprint(service: CostStructureDetailService, resolve_user_id):
    bp = Blueprint("cost_structure_details", __name__, url_prefix="/api/cost-structures")

    def actor() -> str:
        value = resolve_user_id()
        if value is None:
            raise PermissionError("authentication required")
        return str(value)

    @bp.post("/types/<type_id>/import")
    def import_definition(type_id: str):
        body = request.get_json(silent=True) or {}
        try:
            return jsonify(service.import_definition(type_id, body, actor(), bool(body.get("only_structure", False))))
        except PermissionError as exc:
            return jsonify({"code": "UNAUTHORIZED", "detail": str(exc)}), 401
        except LookupError as exc:
            return jsonify({"code": "NOT_FOUND", "detail": str(exc)}), 404
        except ValueError as exc:
            return jsonify({"code": "INVALID_ARGUMENT", "detail": str(exc)}), 400

    @bp.get("/types/<type_id>/categories")
    def list_categories(type_id: str):
        return jsonify(service.list_categories(type_id))

    @bp.get("/projects/<project_code>/items/<item_id>/cost-property")
    def get_property(project_code: str, item_id: str):
        try:
            return jsonify(service.get_item_property(project_code, item_id))
        except LookupError as exc:
            return jsonify({"code": "NOT_FOUND", "detail": str(exc)}), 404

    @bp.put("/projects/<project_code>/items/<item_id>/cost-property")
    def save_property(project_code: str, item_id: str):
        try:
            return jsonify(service.save_item_property(project_code, item_id, request.get_json(silent=True) or {}, actor()))
        except PermissionError as exc:
            return jsonify({"code": "UNAUTHORIZED", "detail": str(exc)}), 401
        except LookupError as exc:
            return jsonify({"code": "NOT_FOUND", "detail": str(exc)}), 404
        except ValueError as exc:
            return jsonify({"code": "INVALID_ARGUMENT", "detail": str(exc)}), 400
        except RuntimeError:
            return jsonify({"code": "CONFLICT", "detail": "stale row_version"}), 409

    return bp
