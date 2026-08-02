"""Cross-project budget propagation, BUD/BID comparison and append-only runs."""
from __future__ import annotations

import json
from datetime import datetime, timezone
from decimal import Decimal, ROUND_HALF_UP
from uuid import uuid4

from flask import Blueprint, jsonify, request
from sqlalchemy import Column, DateTime, MetaData, String, Table, Text, and_, select

from api.budget_decimal import budget_items_decimal
from api.budget_validation import budget_cross_project_refs, budget_item_semantics

metadata = MetaData()
budget_cross_project_runs = Table(
    "budget_cross_project_runs", metadata,
    Column("id", String(100), primary_key=True),
    Column("source_project_code", String(100), nullable=False, index=True),
    Column("target_project_code", String(100), nullable=False, index=True),
    Column("operation", String(40), nullable=False),
    Column("status", String(30), nullable=False),
    Column("result_json", Text, nullable=False),
    Column("created_by", String(100), nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
)


def _amount(quantity: str, unit_price: str, scale: int) -> str:
    quantum = Decimal(1).scaleb(-scale)
    value = (Decimal(str(quantity)) * Decimal(str(unit_price))).quantize(quantum, rounding=ROUND_HALF_UP)
    return format(value, f".{scale}f")


class BudgetCrossProjectSyncService:
    def __init__(self, engine):
        self.engine = engine
        metadata.create_all(engine)

    def propagate(self, source_project: str, target_project: str, actor: str) -> dict:
        with self.engine.begin() as conn:
            refs = conn.execute(select(budget_cross_project_refs).where(and_(
                budget_cross_project_refs.c.source_project_code == source_project,
                budget_cross_project_refs.c.target_project_code == target_project,
                budget_cross_project_refs.c.enabled == True,
            ))).mappings().all()
            updated, broken = [], []
            for ref in refs:
                source = conn.execute(select(budget_items_decimal).where(and_(
                    budget_items_decimal.c.id == ref["source_item_id"],
                    budget_items_decimal.c.project_code == source_project,
                ))).mappings().first()
                target = conn.execute(select(budget_items_decimal).where(and_(
                    budget_items_decimal.c.id == ref["target_item_id"],
                    budget_items_decimal.c.project_code == target_project,
                ))).mappings().first()
                if not source or not target:
                    broken.append({"reference_id": ref["id"], "code": "BROKEN_REFERENCE"})
                    continue
                amount = _amount(str(target["quantity"]), str(source["unit_price"]), int(target["amount_scale"]))
                result = conn.execute(budget_items_decimal.update().where(and_(
                    budget_items_decimal.c.id == target["id"],
                    budget_items_decimal.c.row_version == target["row_version"],
                )).values(
                    unit_price=str(source["unit_price"]),
                    amount=amount,
                    row_version=int(target["row_version"]) + 1,
                    updated_at=datetime.now(timezone.utc),
                ))
                if result.rowcount != 1:
                    broken.append({"reference_id": ref["id"], "code": "CONFLICT"})
                    continue
                updated.append({
                    "reference_id": ref["id"], "source_item_id": source["id"],
                    "target_item_id": target["id"], "unit_price": str(source["unit_price"]),
                    "amount": amount,
                })
            status = "COMPLETED" if not broken else "COMPLETED_WITH_ERRORS"
            payload = {"updated": updated, "broken": broken, "updated_items": len(updated)}
            run = self._insert_run(conn, source_project, target_project, "PROPAGATE", status, payload, actor)
        return run

    def diff(self, left_project: str, right_project: str, actor: str) -> dict:
        with self.engine.begin() as conn:
            left = conn.execute(select(budget_items_decimal).where(
                budget_items_decimal.c.project_code == left_project
            )).mappings().all()
            right = conn.execute(select(budget_items_decimal).where(
                budget_items_decimal.c.project_code == right_project
            )).mappings().all()
            classes = {r["item_id"]: r["item_class"] for r in conn.execute(select(budget_item_semantics)).mappings().all()}
            lmap = {(r["item_no"] or r["id"]): r for r in left}
            rmap = {(r["item_no"] or r["id"]): r for r in right}
            added = [self._summary(r, classes) for key, r in rmap.items() if key not in lmap]
            removed = [self._summary(r, classes) for key, r in lmap.items() if key not in rmap]
            changed = []
            for key in sorted(lmap.keys() & rmap.keys()):
                before, after = self._summary(lmap[key], classes), self._summary(rmap[key], classes)
                if before != after:
                    changed.append({"item_no": key, "before": before, "after": after})
            payload = {
                "left_project_code": left_project, "right_project_code": right_project,
                "added": added, "removed": removed, "changed": changed,
            }
            run = self._insert_run(conn, left_project, right_project, "MODE_DIFF", "COMPLETED", payload, actor)
        return run

    def list_runs(self, project_code: str) -> list[dict]:
        with self.engine.connect() as conn:
            rows = conn.execute(select(budget_cross_project_runs).where(
                (budget_cross_project_runs.c.source_project_code == project_code) |
                (budget_cross_project_runs.c.target_project_code == project_code)
            ).order_by(budget_cross_project_runs.c.created_at.desc())).mappings().all()
        return [self._run_dict(r) for r in rows]

    def _insert_run(self, conn, source, target, operation, status, payload, actor):
        now = datetime.now(timezone.utc)
        row = {
            "id": str(uuid4()), "source_project_code": source, "target_project_code": target,
            "operation": operation, "status": status,
            "result_json": json.dumps(payload, ensure_ascii=False, sort_keys=True),
            "created_by": actor, "created_at": now,
        }
        conn.execute(budget_cross_project_runs.insert().values(**row))
        return self._run_dict(row)

    @staticmethod
    def _summary(row, classes):
        return {
            "id": row["id"], "item_no": row["item_no"], "name": row["name"],
            "kind": row["kind"], "item_class": classes.get(row["id"]),
            "quantity": str(row["quantity"]), "unit_price": str(row["unit_price"]),
            "amount": str(row["amount"]),
        }

    @staticmethod
    def _run_dict(row):
        result = row["result_json"] if isinstance(row["result_json"], dict) else json.loads(row["result_json"])
        created = row["created_at"].isoformat() if hasattr(row["created_at"], "isoformat") else str(row["created_at"])
        return {
            "id": row["id"], "source_project_code": row["source_project_code"],
            "target_project_code": row["target_project_code"], "operation": row["operation"],
            "status": row["status"], "result": result, "created_by": row["created_by"],
            "created_at": created,
            "deep_link": f"/app/projects/by-code/{row['target_project_code']}/budget-validation?sync={row['id']}",
        }


def build_budget_cross_project_blueprint(service: BudgetCrossProjectSyncService, resolve_user_id):
    bp = Blueprint("budget_cross_project", __name__, url_prefix="/api/decimal-budget")

    def actor() -> str:
        value = resolve_user_id()
        if value is None:
            raise PermissionError("authentication required")
        return str(value)

    @bp.post("/cross-project-references/propagate")
    def propagate():
        body = request.get_json(silent=True) or {}
        return jsonify(service.propagate(
            str(body.get("source_project_code", "")),
            str(body.get("target_project_code", "")), actor()
        ))

    @bp.get("/projects/<left_project>/mode-diff/<right_project>")
    def diff(left_project: str, right_project: str):
        return jsonify(service.diff(left_project, right_project, actor()))

    @bp.get("/projects/<project_code>/cross-project-runs")
    def runs(project_code: str):
        return jsonify(service.list_runs(project_code))

    return bp
