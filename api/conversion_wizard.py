"""Phase 4 conversion preflight, options and export wizard sessions."""
from __future__ import annotations

import json
from datetime import datetime, timezone
from uuid import uuid4

from flask import Blueprint, jsonify, request
from sqlalchemy import Boolean, Column, DateTime, Integer, MetaData, String, Table, Text, select

metadata = MetaData()

conversion_wizard_sessions = Table(
    "conversion_wizard_sessions", metadata,
    Column("id", String(100), primary_key=True),
    Column("source_project_code", String(100), nullable=False, index=True),
    Column("source_budget_version_id", String(100), nullable=False),
    Column("target_project_code", String(100), nullable=False),
    Column("mode", String(20), nullable=False),
    Column("status", String(20), nullable=False),
    Column("options_json", Text, nullable=False),
    Column("report_json", Text, nullable=False),
    Column("can_continue", Boolean, nullable=False),
    Column("created_by", String(100), nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
    Column("row_version", Integer, nullable=False, default=1),
)

ALLOWED_MODES = {"CREATE", "REPLACE", "APPEND"}
ALLOWED_FORMATS = {"BID_JSON", "XML_NEW", "XML_LEGACY", "XLSX"}


def build_preflight_report(items: list[dict], mode: str, options: dict) -> dict:
    errors: list[dict] = []
    warnings: list[dict] = []
    seen_ids: set[str] = set()
    seen_codes: set[str] = set()
    if not items:
        errors.append({"code": "EMPTY_BUDGET", "detail": "budget contains no convertible items"})
    for index, raw in enumerate(items):
        item_id = str(raw.get("id", "")).strip()
        code = str(raw.get("code", "")).strip().upper()
        name = str(raw.get("name", "")).strip()
        if not item_id:
            errors.append({"code": "MISSING_ITEM_ID", "index": index})
        elif item_id in seen_ids:
            errors.append({"code": "DUPLICATE_ITEM_ID", "item_id": item_id})
        seen_ids.add(item_id)
        if not code:
            errors.append({"code": "MISSING_ITEM_CODE", "item_id": item_id})
        elif code in seen_codes:
            warnings.append({"code": "DUPLICATE_ITEM_CODE", "item_code": code})
        seen_codes.add(code)
        if not name:
            warnings.append({"code": "MISSING_ITEM_NAME", "item_id": item_id})
        if raw.get("quantity") is None:
            warnings.append({"code": "MISSING_QUANTITY", "item_id": item_id})
        if raw.get("unit_price") is None:
            warnings.append({"code": "MISSING_UNIT_PRICE", "item_id": item_id})
    if mode == "APPEND" and not options.get("deduplicate_by_code", True):
        warnings.append({"code": "APPEND_WITHOUT_DEDUPLICATION"})
    output_format = str(options.get("format", "BID_JSON")).upper()
    if output_format not in ALLOWED_FORMATS:
        errors.append({"code": "UNSUPPORTED_FORMAT", "format": output_format})
    return {
        "errors": errors,
        "warnings": warnings,
        "error_count": len(errors),
        "warning_count": len(warnings),
        "can_continue": not errors,
    }


class ConversionWizardService:
    def __init__(self, engine):
        self.engine = engine
        metadata.create_all(engine)

    def create(self, body: dict, actor: str) -> dict:
        source = str(body.get("source_project_code", "")).strip()
        version = str(body.get("source_budget_version_id", "")).strip()
        target = str(body.get("target_project_code", "")).strip()
        mode = str(body.get("mode", "CREATE")).strip().upper()
        options = dict(body.get("options") or {})
        items = list(body.get("budget_items") or [])
        if not source or not version or not target:
            raise ValueError("source_project_code, source_budget_version_id and target_project_code are required")
        if mode not in ALLOWED_MODES:
            raise ValueError("mode must be CREATE, REPLACE or APPEND")
        options.setdefault("format", "BID_JSON")
        options.setdefault("include_resources", True)
        options.setdefault("include_analysis", True)
        options.setdefault("deduplicate_by_code", True)
        report = build_preflight_report(items, mode, options)
        session_id = str(uuid4())
        now = datetime.now(timezone.utc)
        status = "READY" if report["can_continue"] else "BLOCKED"
        with self.engine.begin() as conn:
            conn.execute(conversion_wizard_sessions.insert().values(
                id=session_id, source_project_code=source, source_budget_version_id=version,
                target_project_code=target, mode=mode, status=status,
                options_json=json.dumps(options, ensure_ascii=False, sort_keys=True),
                report_json=json.dumps(report, ensure_ascii=False, sort_keys=True),
                can_continue=report["can_continue"], created_by=actor, created_at=now, row_version=1,
            ))
        return self.get(session_id)

    def get(self, session_id: str) -> dict:
        with self.engine.connect() as conn:
            row = conn.execute(select(conversion_wizard_sessions).where(conversion_wizard_sessions.c.id == session_id)).mappings().first()
        if not row:
            raise LookupError("conversion wizard session not found")
        item = dict(row)
        item["options"] = json.loads(item.pop("options_json"))
        item["report"] = json.loads(item.pop("report_json"))
        item["created_at"] = item["created_at"].isoformat()
        item["deep_link"] = f"/app/conversions/wizard?session={session_id}"
        return item


def build_conversion_wizard_blueprint(service: ConversionWizardService, resolve_user_id):
    bp = Blueprint("conversion_wizard", __name__, url_prefix="/api/conversions")

    @bp.post("/preflight")
    def preflight():
        if resolve_user_id() is None:
            return jsonify({"code": "UNAUTHORIZED"}), 401
        body = request.get_json(silent=True) or {}
        try:
            mode = str(body.get("mode", "CREATE")).upper()
            if mode not in ALLOWED_MODES:
                raise ValueError("mode must be CREATE, REPLACE or APPEND")
            return jsonify(build_preflight_report(list(body.get("budget_items") or []), mode, dict(body.get("options") or {})))
        except ValueError as exc:
            return jsonify({"code": "INVALID_ARGUMENT", "detail": str(exc)}), 400

    @bp.post("/wizard-sessions")
    def create_session():
        actor = resolve_user_id()
        if actor is None:
            return jsonify({"code": "UNAUTHORIZED"}), 401
        try:
            item = service.create(request.get_json(silent=True) or {}, str(actor))
            return jsonify(item), 201
        except ValueError as exc:
            return jsonify({"code": "INVALID_ARGUMENT", "detail": str(exc)}), 400

    @bp.get("/wizard-sessions/<session_id>")
    def get_session(session_id: str):
        try:
            return jsonify(service.get(session_id))
        except LookupError as exc:
            return jsonify({"code": "NOT_FOUND", "detail": str(exc)}), 404

    return bp
