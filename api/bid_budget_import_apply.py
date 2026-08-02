"""Apply READY electronic-bid import sessions into Decimal Budget Core."""
from __future__ import annotations

import json
from datetime import datetime, timezone
from decimal import Decimal
from uuid import uuid4

from flask import Blueprint, jsonify, request
from sqlalchemy import Column, DateTime, Integer, MetaData, String, Table, Text, delete, select

from api.bid_budget_roundtrip import bid_import_sessions
from api.budget_decimal import budget_items_decimal
from api.budget_versioning import budget_versions

metadata = MetaData()
bid_import_apply_runs = Table(
    "bid_import_apply_runs", metadata,
    Column("id", String(100), primary_key=True),
    Column("import_session_id", String(100), nullable=False, index=True),
    Column("target_budget_project_code", String(100), nullable=False, index=True),
    Column("target_budget_version_id", String(100), nullable=False, index=True),
    Column("mode", String(20), nullable=False),
    Column("status", String(20), nullable=False),
    Column("inserted_count", Integer, nullable=False),
    Column("replaced_count", Integer, nullable=False),
    Column("skipped_count", Integer, nullable=False),
    Column("lineage_json", Text, nullable=False),
    Column("created_by", String(100), nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
)

READ_ONLY_STATES = {"APPROVED", "FROZEN", "ARCHIVED"}
ALLOWED_MODES = {"CREATE", "REPLACE", "APPEND"}


class BidBudgetImportApplyService:
    def __init__(self, engine):
        self.engine = engine
        metadata.create_all(engine)

    def apply(self, session_id: str, body: dict, actor: str) -> dict:
        mode = str(body.get("mode", "CREATE")).strip().upper()
        target_version_id = str(body.get("target_budget_version_id", "")).strip()
        if mode not in ALLOWED_MODES:
            raise ValueError("mode must be CREATE, REPLACE or APPEND")
        if not target_version_id:
            raise ValueError("target_budget_version_id is required")
        now = datetime.now(timezone.utc)
        run_id = str(uuid4())
        with self.engine.begin() as conn:
            session = conn.execute(select(bid_import_sessions).where(bid_import_sessions.c.id == session_id)).mappings().first()
            if not session:
                raise LookupError("bid import session not found")
            if session["status"] != "READY":
                raise PermissionError("blocked import session cannot be applied")
            version = conn.execute(select(budget_versions).where(budget_versions.c.id == target_version_id)).mappings().first()
            if not version:
                raise LookupError("target budget version not found")
            if version["project_code"] != session["target_budget_project_code"]:
                raise ValueError("target budget version belongs to a different project")
            if str(version["status"]).upper() in READ_ONLY_STATES:
                raise PermissionError("approved or frozen budget version is read-only")
            items = json.loads(session["items_json"])
            existing = conn.execute(select(budget_items_decimal).where(
                budget_items_decimal.c.project_code == session["target_budget_project_code"]
            )).mappings().all()
            if mode == "CREATE" and existing:
                raise RuntimeError("CONFLICT")
            if mode == "REPLACE":
                conn.execute(delete(budget_items_decimal).where(
                    budget_items_decimal.c.project_code == session["target_budget_project_code"]
                ))
            existing_codes = {str(row["item_no"] or "").upper() for row in existing} if mode == "APPEND" else set()
            inserted = skipped = 0
            lineage = []
            for index, item in enumerate(items, start=1):
                code = str(item.get("code", "")).strip().upper()
                if mode == "APPEND" and code in existing_codes:
                    skipped += 1
                    continue
                new_id = f"{session['target_budget_project_code']}:{session_id}:{index}"
                quantity = Decimal(str(item.get("quantity", "0")))
                unit_price = Decimal(str(item.get("unit_price", "0")))
                amount = Decimal(str(item.get("amount", quantity * unit_price)))
                conn.execute(budget_items_decimal.insert().values(
                    id=new_id, project_code=session["target_budget_project_code"], parent_id=None,
                    item_no=code, name=str(item.get("name", "")).strip() or code,
                    kind="L", quantity=quantity, unit_price=unit_price, amount=amount,
                    quantity_scale=4, price_scale=4, amount_scale=2,
                    created_at=now, updated_at=now, row_version=1,
                ))
                inserted += 1
                lineage.append({
                    "source_budget_item_id": item.get("source_budget_item_id") or item.get("id"),
                    "imported_budget_item_id": new_id,
                })
                existing_codes.add(code)
            snapshot = conn.execute(select(budget_items_decimal).where(
                budget_items_decimal.c.project_code == session["target_budget_project_code"]
            ).order_by(budget_items_decimal.c.item_no, budget_items_decimal.c.id)).mappings().all()
            def serial(row):
                data = dict(row)
                for key in ("quantity", "unit_price", "amount"): data[key] = str(data[key])
                for key in ("created_at", "updated_at"): data[key] = data[key].isoformat()
                return data
            conn.execute(budget_versions.update().where(budget_versions.c.id == target_version_id).values(
                snapshot_json=json.dumps([serial(row) for row in snapshot], ensure_ascii=False, sort_keys=True)
            ))
            conn.execute(bid_import_apply_runs.insert().values(
                id=run_id, import_session_id=session_id,
                target_budget_project_code=session["target_budget_project_code"],
                target_budget_version_id=target_version_id, mode=mode, status="COMPLETED",
                inserted_count=inserted, replaced_count=len(existing) if mode == "REPLACE" else 0,
                skipped_count=skipped, lineage_json=json.dumps(lineage, ensure_ascii=False, sort_keys=True),
                created_by=actor, created_at=now,
            ))
        return self.get(run_id)

    def get(self, run_id: str) -> dict:
        with self.engine.connect() as conn:
            row = conn.execute(select(bid_import_apply_runs).where(bid_import_apply_runs.c.id == run_id)).mappings().first()
        if not row:
            raise LookupError("bid import apply run not found")
        item = dict(row)
        item["lineage"] = json.loads(item.pop("lineage_json"))
        item["created_at"] = item["created_at"].isoformat()
        item["deep_link"] = f"/app/projects/by-code/{item['target_budget_project_code']}/budget-versions?version={item['target_budget_version_id']}"
        return item


def build_bid_budget_import_apply_blueprint(service: BidBudgetImportApplyService, resolve_user_id):
    bp = Blueprint("bid_budget_import_apply", __name__, url_prefix="/api/conversions")

    @bp.post("/import-sessions/<session_id>/apply")
    def apply(session_id: str):
        actor = resolve_user_id()
        if actor is None:
            return jsonify({"code": "UNAUTHORIZED"}), 401
        try:
            return jsonify(service.apply(session_id, request.get_json(silent=True) or {}, str(actor))), 201
        except LookupError as exc:
            return jsonify({"code": "NOT_FOUND", "detail": str(exc)}), 404
        except PermissionError as exc:
            return jsonify({"code": "READ_ONLY", "detail": str(exc)}), 409
        except RuntimeError:
            return jsonify({"code": "CONFLICT", "detail": "target budget project already contains items"}), 409
        except (ValueError, ArithmeticError) as exc:
            return jsonify({"code": "INVALID_ARGUMENT", "detail": str(exc)}), 400

    @bp.get("/import-apply-runs/<run_id>")
    def get_run(run_id: str):
        try:
            return jsonify(service.get(run_id))
        except LookupError as exc:
            return jsonify({"code": "NOT_FOUND", "detail": str(exc)}), 404

    return bp
