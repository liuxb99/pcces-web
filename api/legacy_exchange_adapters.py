"""Phase 4 ZMD, MDB and PX legacy exchange adapters.

The adapters normalize legacy exchange payloads into the canonical budget item
shape used by conversion/import services. Parsing is deterministic and all
validation happens before a session is persisted.
"""
from __future__ import annotations

import csv
import io
import json
from datetime import datetime, timezone
from uuid import uuid4
from xml.etree import ElementTree as ET

from flask import Blueprint, jsonify, request
from sqlalchemy import Column, DateTime, Integer, MetaData, String, Table, Text, select

metadata = MetaData()
legacy_adapter_sessions = Table(
    "legacy_adapter_sessions", metadata,
    Column("id", String(100), primary_key=True),
    Column("format", String(20), nullable=False),
    Column("format_version", String(20), nullable=False),
    Column("source_filename", String(500), nullable=False),
    Column("source_project_code", String(100), nullable=False),
    Column("target_project_code", String(100), nullable=False),
    Column("status", String(20), nullable=False),
    Column("report_json", Text, nullable=False),
    Column("items_json", Text, nullable=False),
    Column("created_by", String(100), nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
    Column("row_version", Integer, nullable=False, default=1),
)

SUPPORTED_FORMATS = {"ZMD", "MDB", "PX"}


def _normalize_item(raw: dict, index: int) -> dict:
    source_id = str(raw.get("source_budget_item_id") or raw.get("id") or f"ROW-{index}").strip()
    return {
        "source_budget_item_id": source_id,
        "id": source_id,
        "code": str(raw.get("code") or raw.get("item_no") or "").strip().upper(),
        "name": str(raw.get("name") or raw.get("description") or "").strip(),
        "unit": str(raw.get("unit") or "").strip(),
        "quantity": str(raw.get("quantity") if raw.get("quantity") is not None else "0"),
        "unit_price": str(raw.get("unit_price") if raw.get("unit_price") is not None else "0"),
        "amount": str(raw.get("amount") if raw.get("amount") is not None else "0"),
    }


def _preflight(items: list[dict]) -> dict:
    errors: list[dict] = []
    warnings: list[dict] = []
    seen: set[str] = set()
    if not items:
        errors.append({"code": "EMPTY_EXCHANGE"})
    for index, item in enumerate(items):
        code = item["code"]
        if not code:
            errors.append({"code": "MISSING_ITEM_CODE", "index": index})
        elif code in seen:
            errors.append({"code": "DUPLICATE_ITEM_CODE", "item_code": code})
        seen.add(code)
        if not item["name"]:
            warnings.append({"code": "MISSING_ITEM_NAME", "index": index})
        if item["quantity"] == "0" and item["amount"] != "0":
            warnings.append({"code": "ZERO_QUANTITY_WITH_AMOUNT", "index": index})
    return {
        "errors": errors,
        "warnings": warnings,
        "error_count": len(errors),
        "warning_count": len(warnings),
        "can_continue": not errors,
    }


def parse_legacy_exchange(payload: str, fmt: str) -> tuple[str, str, list[dict]]:
    fmt = fmt.strip().upper()
    if fmt not in SUPPORTED_FORMATS:
        raise ValueError("format must be ZMD, MDB or PX")
    text = payload.strip()
    project_code = ""
    version = "1.0"
    rows: list[dict]
    if fmt == "ZMD":
        data = json.loads(text)
        project_code = str(data.get("project_code", "")).strip()
        version = str(data.get("version", "1.0"))
        rows = list(data.get("items") or data.get("details") or [])
    elif fmt == "PX":
        root = ET.fromstring(text)
        if root.tag not in {"PX", "PCCESExchange"}:
            raise ValueError("invalid PX root element")
        version = root.attrib.get("version", "1.0")
        project_code = root.findtext("./Header/ProjectCode", default="").strip()
        rows = []
        for node in root.findall("./Items/Item") + root.findall("./Detail/Record"):
            rows.append({
                "id": node.findtext("SourceItemId", default=""),
                "code": node.findtext("Code", default=""),
                "name": node.findtext("Name", default=""),
                "unit": node.findtext("Unit", default=""),
                "quantity": node.findtext("Quantity", default="0"),
                "unit_price": node.findtext("UnitPrice", default="0"),
                "amount": node.findtext("Amount", default="0"),
            })
    else:
        # MDB is represented by the deterministic CSV interchange emitted by
        # the legacy bridge. Native Access database extraction remains outside
        # the HTTP process; this adapter consumes its exported table rows.
        reader = csv.DictReader(io.StringIO(text))
        rows = list(reader)
        if reader.fieldnames is None:
            raise ValueError("MDB interchange header is required")
        project_code = str(rows[0].get("project_code", "")).strip() if rows else ""
        version = "CSV-1.0"
    items = [_normalize_item(row, index) for index, row in enumerate(rows, start=1)]
    return project_code, version, items


class LegacyExchangeAdapterService:
    def __init__(self, engine):
        self.engine = engine
        metadata.create_all(engine)

    def preflight(self, body: dict) -> dict:
        fmt = str(body.get("format", "")).upper()
        project, version, items = parse_legacy_exchange(str(body.get("payload", "")), fmt)
        return {
            "format": fmt,
            "format_version": version,
            "source_project_code": project,
            "items": items,
            "report": _preflight(items),
        }

    def create(self, body: dict, actor: str) -> dict:
        target = str(body.get("target_project_code", "")).strip()
        filename = str(body.get("source_filename", "")).strip()
        if not target or not filename:
            raise ValueError("target_project_code and source_filename are required")
        result = self.preflight(body)
        session_id = str(uuid4())
        now = datetime.now(timezone.utc)
        status = "READY" if result["report"]["can_continue"] else "BLOCKED"
        with self.engine.begin() as conn:
            conn.execute(legacy_adapter_sessions.insert().values(
                id=session_id,
                format=result["format"],
                format_version=result["format_version"],
                source_filename=filename,
                source_project_code=result["source_project_code"],
                target_project_code=target,
                status=status,
                report_json=json.dumps(result["report"], ensure_ascii=False, sort_keys=True),
                items_json=json.dumps(result["items"], ensure_ascii=False, sort_keys=True),
                created_by=actor,
                created_at=now,
                row_version=1,
            ))
        return self.get(session_id)

    def get(self, session_id: str) -> dict:
        with self.engine.connect() as conn:
            row = conn.execute(select(legacy_adapter_sessions).where(legacy_adapter_sessions.c.id == session_id)).mappings().first()
        if not row:
            raise LookupError("legacy adapter session not found")
        return {
            "id": row["id"],
            "format": row["format"],
            "format_version": row["format_version"],
            "source_filename": row["source_filename"],
            "source_project_code": row["source_project_code"],
            "target_project_code": row["target_project_code"],
            "status": row["status"],
            "report": json.loads(row["report_json"]),
            "items": json.loads(row["items_json"]),
            "created_by": row["created_by"],
            "created_at": row["created_at"].isoformat(),
            "row_version": row["row_version"],
            "deep_link": f"/app/conversions/legacy-adapters?session={session_id}",
        }


def build_legacy_exchange_adapter_blueprint(service: LegacyExchangeAdapterService, resolve_user_id):
    bp = Blueprint("legacy_exchange_adapters", __name__, url_prefix="/api/conversions")

    @bp.post("/legacy-adapters/preflight")
    def preflight():
        if resolve_user_id() is None:
            return jsonify({"code": "UNAUTHORIZED"}), 401
        try:
            return jsonify(service.preflight(request.get_json(silent=True) or {}))
        except (ValueError, json.JSONDecodeError, ET.ParseError, csv.Error) as exc:
            return jsonify({"code": "INVALID_ARGUMENT", "detail": str(exc)}), 400

    @bp.post("/legacy-adapters/sessions")
    def create_session():
        actor = resolve_user_id()
        if actor is None:
            return jsonify({"code": "UNAUTHORIZED"}), 401
        try:
            return jsonify(service.create(request.get_json(silent=True) or {}, str(actor))), 201
        except (ValueError, json.JSONDecodeError, ET.ParseError, csv.Error) as exc:
            return jsonify({"code": "INVALID_ARGUMENT", "detail": str(exc)}), 400

    @bp.get("/legacy-adapters/sessions/<session_id>")
    def get_session(session_id: str):
        try:
            return jsonify(service.get(session_id))
        except LookupError as exc:
            return jsonify({"code": "NOT_FOUND", "detail": str(exc)}), 404

    return bp
