"""Phase 4 cost-structure run versioning, comparison and approval guards."""
from __future__ import annotations

import json
from datetime import datetime, timezone
from decimal import Decimal
from uuid import uuid4

from flask import Blueprint, jsonify, request
from sqlalchemy import Column, DateTime, Integer, MetaData, String, Table, Text, and_, select

metadata = MetaData()

cost_structure_run_versions = Table(
    "cost_structure_run_versions", metadata,
    Column("id", String(100), primary_key=True),
    Column("project_code", String(100), nullable=False, index=True),
    Column("run_id", String(100), nullable=False, unique=True),
    Column("budget_version_id", String(100), nullable=False, index=True),
    Column("budget_status", String(30), nullable=False),
    Column("direct_cost", String(80), nullable=False),
    Column("total_cost", String(80), nullable=False),
    Column("trace_json", Text, nullable=False),
    Column("created_by", String(100), nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
    Column("row_version", Integer, nullable=False, default=1),
)

APPROVED_STATES = {"APPROVED", "FROZEN", "ARCHIVED"}


class CostStructureRunVersionService:
    def __init__(self, engine):
        self.engine = engine
        metadata.create_all(engine)

    def link(self, project_code: str, run_id: str, budget_version_id: str, budget_status: str,
             direct_cost: str, total_cost: str, trace: dict, actor: str) -> dict:
        project_code = project_code.strip(); run_id = run_id.strip(); budget_version_id = budget_version_id.strip()
        status = budget_status.strip().upper()
        if not project_code or not run_id or not budget_version_id:
            raise ValueError("project_code, run_id and budget_version_id are required")
        if status in APPROVED_STATES:
            raise PermissionError("approved or frozen budget version is read-only")
        Decimal(direct_cost); Decimal(total_cost)
        item_id = str(uuid4()); now = datetime.now(timezone.utc)
        with self.engine.begin() as conn:
            if conn.execute(select(cost_structure_run_versions.c.id).where(cost_structure_run_versions.c.run_id == run_id)).first():
                raise RuntimeError("CONFLICT")
            conn.execute(cost_structure_run_versions.insert().values(
                id=item_id, project_code=project_code, run_id=run_id,
                budget_version_id=budget_version_id, budget_status=status,
                direct_cost=str(direct_cost), total_cost=str(total_cost),
                trace_json=json.dumps(trace, ensure_ascii=False, sort_keys=True),
                created_by=actor, created_at=now, row_version=1,
            ))
        return self.get(run_id)

    def get(self, run_id: str) -> dict:
        with self.engine.connect() as conn:
            row = conn.execute(select(cost_structure_run_versions).where(cost_structure_run_versions.c.run_id == run_id)).mappings().first()
        if not row: raise LookupError("cost structure run version not found")
        item = dict(row); item["trace"] = json.loads(item.pop("trace_json")); item["created_at"] = item["created_at"].isoformat()
        item["deep_link"] = f"/app/cost-structure?project={item['project_code']}&run={run_id}"
        return item

    def compare(self, left_run_id: str, right_run_id: str) -> dict:
        left, right = self.get(left_run_id), self.get(right_run_id)
        if left["project_code"] != right["project_code"]:
            raise ValueError("runs must belong to the same project")
        direct_delta = Decimal(right["direct_cost"]) - Decimal(left["direct_cost"])
        total_delta = Decimal(right["total_cost"]) - Decimal(left["total_cost"])
        return {"project_code": left["project_code"], "left": left_run_id, "right": right_run_id,
                "direct_cost_delta": format(direct_delta, "f"), "total_cost_delta": format(total_delta, "f")}


def build_cost_structure_run_version_blueprint(service: CostStructureRunVersionService, resolve_user_id):
    bp = Blueprint("cost_structure_run_versions", __name__, url_prefix="/api/cost-structures")
    @bp.post("/projects/<project_code>/runs/<run_id>/budget-version")
    def link(project_code: str, run_id: str):
        actor = resolve_user_id()
        if actor is None: return jsonify({"code":"UNAUTHORIZED"}), 401
        body = request.get_json(silent=True) or {}
        try:
            return jsonify(service.link(project_code, run_id, str(body.get("budget_version_id", "")),
                str(body.get("budget_status", "DRAFT")), str(body.get("direct_cost", "0")),
                str(body.get("total_cost", "0")), dict(body.get("trace") or {}), str(actor)))
        except PermissionError as exc: return jsonify({"code":"READ_ONLY","detail":str(exc)}), 409
        except ValueError as exc: return jsonify({"code":"INVALID_ARGUMENT","detail":str(exc)}), 400
        except RuntimeError: return jsonify({"code":"CONFLICT","detail":"run already linked"}), 409
    @bp.get("/runs/compare")
    def compare():
        try: return jsonify(service.compare(request.args.get("left", ""), request.args.get("right", "")))
        except LookupError as exc: return jsonify({"code":"NOT_FOUND","detail":str(exc)}), 404
        except ValueError as exc: return jsonify({"code":"INVALID_ARGUMENT","detail":str(exc)}), 400
    return bp
