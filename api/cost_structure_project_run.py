"""Persisted project cost-structure initialization and recalculation."""
from __future__ import annotations

import json
from datetime import datetime, timezone
from decimal import Decimal
from uuid import uuid4

from flask import Blueprint, jsonify, request
from sqlalchemy import Column, DateTime, Integer, MetaData, Numeric, String, Table, Text, and_, select

from api.cost_structure_calculation import calculate_cost_structure
from api.cost_structure_details import cost_structure_categories
from api.cost_structure import project_cost_structures
from api.decimal_math import quantize

metadata = MetaData()
project_cost_structure_runs = Table(
    "project_cost_structure_runs", metadata,
    Column("id", String(100), primary_key=True),
    Column("project_code", String(100), nullable=False, index=True),
    Column("cost_structure_type_id", String(100), nullable=False),
    Column("direct_cost", Numeric(24, 8), nullable=False),
    Column("total", Numeric(24, 8), nullable=False),
    Column("scale", Integer, nullable=False),
    Column("budget_snapshot_json", Text, nullable=False),
    Column("result_json", Text, nullable=False),
    Column("created_by", String(100), nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
    Column("row_version", Integer, nullable=False, default=1),
)


class ProjectCostStructureRunService:
    def __init__(self, engine):
        self.engine = engine
        metadata.create_all(engine)

    @staticmethod
    def _direct_cost(items: list[dict]) -> Decimal:
        total = Decimal("0")
        for item in items:
            if str(item.get("kind", "WORK")).upper() in {"SECTION", "CHAPTER", "FOLDER"}:
                continue
            amount = item.get("amount")
            if amount is None:
                amount = Decimal(str(item.get("quantity", "0"))) * Decimal(str(item.get("unit_price", "0")))
            total += Decimal(str(amount))
        return total

    def recalculate(self, project_code: str, budget_items: list[dict], scale: int, actor: str) -> dict:
        if not project_code.strip():
            raise ValueError("project_code is required")
        with self.engine.begin() as conn:
            assignment = conn.execute(select(project_cost_structures).where(
                project_cost_structures.c.project_code == project_code
            )).mappings().first()
            if not assignment:
                raise LookupError("project cost structure not assigned")
            rows = conn.execute(select(cost_structure_categories).where(and_(
                cost_structure_categories.c.cost_structure_type_id == assignment["cost_structure_type_id"],
                cost_structure_categories.c.enabled.is_(True),
            )).order_by(cost_structure_categories.c.sequence, cost_structure_categories.c.code)).mappings().all()
            if not rows:
                raise ValueError("assigned cost structure has no enabled categories")
            lines = [{
                "code": row["code"], "kind": row["kind"], "base_kind": "SUBTOTAL",
                "rate": str(row["rate"]), "sign": 1, "sort_order": row["sequence"],
            } for row in rows]
            direct = self._direct_cost(budget_items)
            result = calculate_cost_structure(lines, str(direct), scale)
            now = datetime.now(timezone.utc)
            run_id = str(uuid4())
            conn.execute(project_cost_structure_runs.insert().values(
                id=run_id, project_code=project_code,
                cost_structure_type_id=assignment["cost_structure_type_id"],
                direct_cost=Decimal(result["direct_cost"]), total=Decimal(result["total"]), scale=scale,
                budget_snapshot_json=json.dumps(budget_items, ensure_ascii=False, sort_keys=True),
                result_json=json.dumps(result, ensure_ascii=False, sort_keys=True),
                created_by=actor, created_at=now, row_version=1,
            ))
        return self.get(run_id)

    def get(self, run_id: str) -> dict:
        with self.engine.connect() as conn:
            row = conn.execute(select(project_cost_structure_runs).where(project_cost_structure_runs.c.id == run_id)).mappings().first()
        if not row:
            raise LookupError("project cost structure run not found")
        return {
            "id": row["id"], "project_code": row["project_code"],
            "cost_structure_type_id": row["cost_structure_type_id"],
            "direct_cost": quantize(str(row["direct_cost"]), row["scale"]), "total": quantize(str(row["total"]), row["scale"]),
            "scale": row["scale"], "budget_snapshot": json.loads(row["budget_snapshot_json"]),
            "result": json.loads(row["result_json"]), "created_by": row["created_by"],
            "created_at": row["created_at"].isoformat(), "row_version": row["row_version"],
            "deep_link": f"/app/cost-structure?project={row['project_code']}&run={row['id']}",
        }


def build_project_cost_structure_run_blueprint(service: ProjectCostStructureRunService, resolve_user_id):
    bp = Blueprint("project_cost_structure_run", __name__, url_prefix="/api/cost-structures")

    @bp.post("/projects/<project_code>/recalculate")
    def recalculate(project_code: str):
        actor = resolve_user_id()
        if actor is None:
            return jsonify({"code": "UNAUTHORIZED"}), 401
        body = request.get_json(silent=True) or {}
        try:
            return jsonify(service.recalculate(project_code, list(body.get("budget_items") or []), int(body.get("scale", 2)), str(actor)))
        except LookupError as exc:
            return jsonify({"code": "NOT_FOUND", "detail": str(exc)}), 404
        except (ValueError, ArithmeticError) as exc:
            return jsonify({"code": "INVALID_ARGUMENT", "detail": str(exc)}), 400

    @bp.get("/runs/<run_id>")
    def get_run(run_id: str):
        try:
            return jsonify(service.get(run_id))
        except LookupError as exc:
            return jsonify({"code": "NOT_FOUND", "detail": str(exc)}), 404

    return bp
