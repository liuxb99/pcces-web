"""Phase 4 budget-to-bid conversion sessions and immutable lineage."""
from __future__ import annotations
import json
from datetime import datetime, timezone
from uuid import uuid4
from flask import Blueprint, jsonify, request
from sqlalchemy import Column, DateTime, Integer, MetaData, String, Table, Text, select

from api.conversion_wizard import ALLOWED_MODES, ConversionWizardService, build_preflight_report

metadata = MetaData()
conversion_sessions = Table(
    "budget_bid_conversion_sessions", metadata,
    Column("id", String(100), primary_key=True),
    Column("source_project_code", String(100), nullable=False, index=True),
    Column("source_budget_version_id", String(100), nullable=False),
    Column("target_bid_project_code", String(100), nullable=False, index=True),
    Column("mode", String(20), nullable=False),
    Column("status", String(20), nullable=False),
    Column("options_json", Text, nullable=False),
    Column("source_snapshot_json", Text, nullable=False),
    Column("result_snapshot_json", Text, nullable=False),
    Column("created_by", String(100), nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
    Column("row_version", Integer, nullable=False, default=1),
)


class BudgetBidConversionService:
    def __init__(self, engine):
        self.engine = engine
        metadata.create_all(engine)

    def convert(self, body: dict, actor: str) -> dict:
        source_project = str(body.get("source_project_code", "")).strip()
        source_version = str(body.get("source_budget_version_id", "")).strip()
        target_project = str(body.get("target_bid_project_code", "")).strip()
        mode = str(body.get("mode", "CREATE")).strip().upper()
        if not source_project or not source_version or not target_project:
            raise ValueError("source project, source version and target bid project are required")
        if mode not in ALLOWED_MODES:
            raise ValueError("mode must be CREATE, REPLACE or APPEND")
        source_items = list(body.get("budget_items") or [])
        options = dict(body.get("options") or {})
        report = build_preflight_report(source_items, mode, options)
        if not report["can_continue"]:
            raise ValueError("conversion preflight contains blocking errors")
        converted = []
        seen = set()
        for index, raw in enumerate(source_items):
            item_id = str(raw.get("id", "")).strip()
            if item_id in seen:
                raise ValueError("duplicate source budget item id")
            seen.add(item_id)
            converted.append({
                "source_budget_item_id": item_id,
                "bid_item_id": f"{target_project}:{item_id}",
                "code": str(raw.get("code", "")).strip().upper(),
                "name": str(raw.get("name", "")).strip(),
                "unit": str(raw.get("unit", "")).strip(),
                "quantity": str(raw.get("quantity", "0")),
                "unit_price": str(raw.get("unit_price", "0")),
                "amount": str(raw.get("amount", "0")),
                "sort_order": int(raw.get("sort_order", index + 1)),
            })
        converted.sort(key=lambda item: (item["sort_order"], item["code"], item["source_budget_item_id"]))
        session_id = str(uuid4())
        now = datetime.now(timezone.utc)
        with self.engine.begin() as conn:
            if mode == "CREATE" and conn.execute(select(conversion_sessions.c.id).where(
                conversion_sessions.c.target_bid_project_code == target_project
            )).first():
                raise RuntimeError("CONFLICT")
            conn.execute(conversion_sessions.insert().values(
                id=session_id, source_project_code=source_project,
                source_budget_version_id=source_version,
                target_bid_project_code=target_project, mode=mode, status="COMPLETED",
                options_json=json.dumps(options, ensure_ascii=False, sort_keys=True),
                source_snapshot_json=json.dumps(source_items, ensure_ascii=False, sort_keys=True),
                result_snapshot_json=json.dumps(converted, ensure_ascii=False, sort_keys=True),
                created_by=actor, created_at=now, row_version=1,
            ))
        return self.get(session_id)

    def get(self, session_id: str) -> dict:
        with self.engine.connect() as conn:
            row = conn.execute(select(conversion_sessions).where(conversion_sessions.c.id == session_id)).mappings().first()
        if not row:
            raise LookupError("conversion session not found")
        item = dict(row)
        item["options"] = json.loads(item.pop("options_json"))
        item["source_snapshot"] = json.loads(item.pop("source_snapshot_json"))
        item["result_snapshot"] = json.loads(item.pop("result_snapshot_json"))
        item["created_at"] = item["created_at"].isoformat()
        item["lineage"] = {
            "source_project_code": item["source_project_code"],
            "source_budget_version_id": item["source_budget_version_id"],
            "target_bid_project_code": item["target_bid_project_code"],
            "session_id": item["id"],
        }
        item["deep_link"] = f"/app/bid-conversion?session={item['id']}"
        return item


def build_budget_bid_conversion_blueprint(service: BudgetBidConversionService, resolve_user_id):
    bp = Blueprint("budget_bid_conversion", __name__, url_prefix="/api/conversions")
    wizard = ConversionWizardService(service.engine)

    @bp.post("/preflight")
    def preflight():
        if resolve_user_id() is None:
            return jsonify({"code": "UNAUTHORIZED"}), 401
        body = request.get_json(silent=True) or {}
        mode = str(body.get("mode", "CREATE")).upper()
        if mode not in ALLOWED_MODES:
            return jsonify({"code": "INVALID_ARGUMENT", "detail": "mode must be CREATE, REPLACE or APPEND"}), 400
        return jsonify(build_preflight_report(list(body.get("budget_items") or []), mode, dict(body.get("options") or {})))

    @bp.post("/wizard-sessions")
    def create_wizard_session():
        actor = resolve_user_id()
        if actor is None:
            return jsonify({"code": "UNAUTHORIZED"}), 401
        try:
            return jsonify(wizard.create(request.get_json(silent=True) or {}, str(actor))), 201
        except ValueError as exc:
            return jsonify({"code": "INVALID_ARGUMENT", "detail": str(exc)}), 400

    @bp.get("/wizard-sessions/<session_id>")
    def get_wizard_session(session_id: str):
        try:
            return jsonify(wizard.get(session_id))
        except LookupError as exc:
            return jsonify({"code": "NOT_FOUND", "detail": str(exc)}), 404

    @bp.post("/budget-to-bid")
    def convert():
        actor = resolve_user_id()
        if actor is None:
            return jsonify({"code": "UNAUTHORIZED"}), 401
        try:
            return jsonify(service.convert(request.get_json(silent=True) or {}, str(actor)))
        except ValueError as exc:
            return jsonify({"code": "INVALID_ARGUMENT", "detail": str(exc)}), 400
        except RuntimeError:
            return jsonify({"code": "CONFLICT", "detail": "target bid project already has a conversion"}), 409

    @bp.get("/sessions/<session_id>")
    def get_session(session_id: str):
        try:
            return jsonify(service.get(session_id))
        except LookupError as exc:
            return jsonify({"code": "NOT_FOUND", "detail": str(exc)}), 404

    return bp
