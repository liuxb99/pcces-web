"""Phase 4 electronic bid reverse import, preflight and round-trip lineage."""
from __future__ import annotations

import json
from datetime import datetime, timezone
from uuid import uuid4
from xml.etree import ElementTree as ET

from flask import Blueprint, jsonify, request
from sqlalchemy import Column, DateTime, Integer, MetaData, String, Table, Text, select

metadata = MetaData()
bid_import_sessions = Table(
    "bid_import_sessions", metadata,
    Column("id", String(100), primary_key=True),
    Column("source_format", String(30), nullable=False),
    Column("format_version", String(20), nullable=False),
    Column("source_bid_project_code", String(100), nullable=False),
    Column("target_budget_project_code", String(100), nullable=False),
    Column("source_conversion_session_id", String(100), nullable=True),
    Column("status", String(20), nullable=False),
    Column("report_json", Text, nullable=False),
    Column("items_json", Text, nullable=False),
    Column("created_by", String(100), nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
    Column("row_version", Integer, nullable=False, default=1),
)


def detect_and_parse(payload: str, hinted_format: str = "") -> tuple[str, str, str, list[dict]]:
    hinted = hinted_format.strip().upper()
    text = payload.strip()
    if hinted == "BID_JSON" or text.startswith("{"):
        data = json.loads(text)
        return "BID_JSON", "2.0", str(data.get("project_code", "")), list(data.get("items") or [])
    root = ET.fromstring(text)
    if root.tag == "PCCESBidExchange":
        rows = root.findall("./Items/Item")
        fmt, version = "XML_NEW", root.attrib.get("version", "2.0")
    elif root.tag == "PCCES":
        rows = root.findall("./Detail/Record")
        fmt, version = "XML_LEGACY", root.attrib.get("version", "1.0")
    else:
        raise ValueError("unsupported electronic bid format")
    project = root.findtext("./Header/ProjectCode", default="")
    items = []
    for index, row in enumerate(rows, start=1):
        items.append({
            "source_budget_item_id": row.findtext("SourceItemId", default=""),
            "id": row.findtext("SourceItemId", default=f"ROW-{index}"),
            "code": row.findtext("Code", default="").strip().upper(),
            "name": row.findtext("Name", default="").strip(),
            "unit": row.findtext("Unit", default="").strip(),
            "quantity": row.findtext("Quantity", default="0"),
            "unit_price": row.findtext("UnitPrice", default="0"),
            "amount": row.findtext("Amount", default="0"),
        })
    return fmt, version, project, items


def import_preflight(items: list[dict]) -> dict:
    errors, warnings, seen = [], [], set()
    if not items:
        errors.append({"code": "EMPTY_BID"})
    for index, item in enumerate(items):
        code = str(item.get("code", "")).strip().upper()
        if not code:
            errors.append({"code": "MISSING_ITEM_CODE", "index": index})
        elif code in seen:
            errors.append({"code": "DUPLICATE_ITEM_CODE", "item_code": code})
        seen.add(code)
        if not str(item.get("name", "")).strip():
            warnings.append({"code": "MISSING_ITEM_NAME", "index": index})
        if not str(item.get("source_budget_item_id", item.get("id", ""))).strip():
            warnings.append({"code": "MISSING_ROUNDTRIP_LINEAGE", "index": index})
    return {"errors": errors, "warnings": warnings, "error_count": len(errors), "warning_count": len(warnings), "can_continue": not errors}


class BidBudgetRoundTripService:
    def __init__(self, engine):
        self.engine = engine
        metadata.create_all(engine)

    def create(self, body: dict, actor: str) -> dict:
        target = str(body.get("target_budget_project_code", "")).strip()
        if not target:
            raise ValueError("target_budget_project_code is required")
        fmt, version, source_project, items = detect_and_parse(str(body.get("payload", "")), str(body.get("format", "")))
        report = import_preflight(items)
        session_id = str(uuid4())
        now = datetime.now(timezone.utc)
        status = "READY" if report["can_continue"] else "BLOCKED"
        with self.engine.begin() as conn:
            conn.execute(bid_import_sessions.insert().values(
                id=session_id, source_format=fmt, format_version=version,
                source_bid_project_code=source_project, target_budget_project_code=target,
                source_conversion_session_id=str(body.get("source_conversion_session_id", "")).strip() or None,
                status=status, report_json=json.dumps(report, ensure_ascii=False, sort_keys=True),
                items_json=json.dumps(items, ensure_ascii=False, sort_keys=True),
                created_by=actor, created_at=now, row_version=1,
            ))
        return self.get(session_id)

    def get(self, session_id: str) -> dict:
        with self.engine.connect() as conn:
            row = conn.execute(select(bid_import_sessions).where(bid_import_sessions.c.id == session_id)).mappings().first()
        if not row:
            raise LookupError("bid import session not found")
        item = dict(row)
        item["report"] = json.loads(item.pop("report_json"))
        item["items"] = json.loads(item.pop("items_json"))
        item["created_at"] = item["created_at"].isoformat()
        item["round_trip_lineage"] = {
            "source_conversion_session_id": item["source_conversion_session_id"],
            "source_bid_project_code": item["source_bid_project_code"],
            "target_budget_project_code": item["target_budget_project_code"],
            "item_links": [{"source_budget_item_id": x.get("source_budget_item_id", x.get("id", "")), "imported_budget_item_id": x.get("id", "")} for x in item["items"]],
        }
        item["deep_link"] = f"/app/conversions/import?session={session_id}"
        return item


def build_bid_budget_roundtrip_blueprint(service: BidBudgetRoundTripService, resolve_user_id):
    bp = Blueprint("bid_budget_roundtrip", __name__, url_prefix="/api/conversions")

    @bp.post("/import-preflight")
    def preflight():
        if resolve_user_id() is None:
            return jsonify({"code": "UNAUTHORIZED"}), 401
        try:
            fmt, version, project, items = detect_and_parse(str((request.get_json(silent=True) or {}).get("payload", "")), str((request.get_json(silent=True) or {}).get("format", "")))
            return jsonify({"format": fmt, "format_version": version, "source_bid_project_code": project, "report": import_preflight(items), "items": items})
        except (ValueError, ET.ParseError, json.JSONDecodeError) as exc:
            return jsonify({"code": "INVALID_ARGUMENT", "detail": str(exc)}), 400

    @bp.post("/import-sessions")
    def create_session():
        actor = resolve_user_id()
        if actor is None:
            return jsonify({"code": "UNAUTHORIZED"}), 401
        try:
            return jsonify(service.create(request.get_json(silent=True) or {}, str(actor))), 201
        except (ValueError, ET.ParseError, json.JSONDecodeError) as exc:
            return jsonify({"code": "INVALID_ARGUMENT", "detail": str(exc)}), 400

    @bp.get("/import-sessions/<session_id>")
    def get_session(session_id: str):
        try:
            return jsonify(service.get(session_id))
        except LookupError as exc:
            return jsonify({"code": "NOT_FOUND", "detail": str(exc)}), 404

    return bp
