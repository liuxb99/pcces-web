"""Append-only calculation traces for the Decimal Budget Core."""
from __future__ import annotations

import json
from datetime import datetime, timezone
from uuid import uuid4
from flask import Blueprint, jsonify, request
from sqlalchemy import Column, DateTime, MetaData, String, Table, Text, select

from api.budget_kind_engine import calculate_budget_kind

metadata = MetaData()
budget_calculation_traces = Table(
    "budget_calculation_traces", metadata,
    Column("id", String(100), primary_key=True),
    Column("project_code", String(100), nullable=False, index=True),
    Column("item_id", String(100), nullable=True, index=True),
    Column("kind", String(10), nullable=False),
    Column("input_json", Text, nullable=False),
    Column("steps_json", Text, nullable=False),
    Column("result", String(100), nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
)


class BudgetTraceService:
    def __init__(self, engine):
        self.engine = engine
        metadata.create_all(engine)

    def calculate(self, project_code: str, item_id: str | None, kind: str, scale: int, payload: dict) -> dict:
        trace = calculate_budget_kind(kind, payload, scale)
        trace_id = str(uuid4())
        row = {
            "id": trace_id,
            "project_code": project_code,
            "item_id": item_id,
            "kind": trace.kind,
            "input_json": json.dumps(payload, ensure_ascii=False, sort_keys=True),
            "steps_json": json.dumps(trace.to_dict()["steps"], ensure_ascii=False, sort_keys=True),
            "result": trace.result,
            "created_at": datetime.now(timezone.utc),
        }
        with self.engine.begin() as conn:
            conn.execute(budget_calculation_traces.insert().values(**row))
        return self.get(trace_id)

    def get(self, trace_id: str) -> dict | None:
        with self.engine.connect() as conn:
            row = conn.execute(select(budget_calculation_traces).where(budget_calculation_traces.c.id == trace_id)).mappings().first()
        if not row:
            return None
        return {
            "id": row["id"], "project_code": row["project_code"], "item_id": row["item_id"],
            "kind": row["kind"], "input": json.loads(row["input_json"]),
            "steps": json.loads(row["steps_json"]), "result": row["result"],
            "created_at": row["created_at"].isoformat(),
        }

    def list_project(self, project_code: str) -> list[dict]:
        with self.engine.connect() as conn:
            ids = [row[0] for row in conn.execute(select(budget_calculation_traces.c.id).where(
                budget_calculation_traces.c.project_code == project_code
            ).order_by(budget_calculation_traces.c.created_at.desc()))]
        return [self.get(trace_id) for trace_id in ids]


def build_budget_trace_blueprint(service: BudgetTraceService, resolve_user_id):
    bp = Blueprint("budget_trace", __name__, url_prefix="/api/decimal-budget")

    @bp.post("/calculate")
    def calculate():
        if resolve_user_id() is None: return jsonify({"code":"UNAUTHORIZED"}), 401
        body = request.get_json(silent=True) or {}
        try:
            result = service.calculate(str(body.get("project_code", "")), body.get("item_id"), str(body.get("kind", "")), int(body.get("scale", 2)), body.get("input", {}))
        except (ValueError, ArithmeticError) as exc:
            return jsonify({"code":"INVALID_ARGUMENT","detail":str(exc)}), 400
        if not body.get("project_code"):
            return jsonify({"code":"INVALID_ARGUMENT","detail":"project_code is required"}), 400
        return jsonify(result), 201

    @bp.get("/traces/<trace_id>")
    def get_trace(trace_id: str):
        if resolve_user_id() is None: return jsonify({"code":"UNAUTHORIZED"}), 401
        result = service.get(trace_id)
        return (jsonify(result), 200) if result else (jsonify({"code":"NOT_FOUND"}), 404)

    @bp.get("/projects/<project_code>/traces")
    def list_traces(project_code: str):
        if resolve_user_id() is None: return jsonify({"code":"UNAUTHORIZED"}), 401
        return jsonify(service.list_project(project_code))

    return bp
