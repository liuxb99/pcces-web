"""Phase 4 conversion export jobs, artifact metadata and XML serialization."""
from __future__ import annotations

import hashlib
import json
from datetime import datetime, timezone
from uuid import uuid4
from xml.etree.ElementTree import Element, SubElement, tostring

from flask import Blueprint, Response, jsonify, request
from sqlalchemy import Column, DateTime, Integer, LargeBinary, MetaData, String, Table, Text, select

metadata = MetaData()
conversion_export_jobs = Table(
    "conversion_export_jobs", metadata,
    Column("id", String(100), primary_key=True),
    Column("wizard_session_id", String(100), nullable=False, index=True),
    Column("source_budget_version_id", String(100), nullable=False),
    Column("target_project_code", String(100), nullable=False),
    Column("format", String(30), nullable=False),
    Column("status", String(20), nullable=False),
    Column("filename", String(300), nullable=False),
    Column("content_type", String(100), nullable=False),
    Column("size_bytes", Integer, nullable=False),
    Column("sha256", String(64), nullable=False),
    Column("artifact", LargeBinary, nullable=False),
    Column("metadata_json", Text, nullable=False),
    Column("created_by", String(100), nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
    Column("row_version", Integer, nullable=False, default=1),
)

SUPPORTED_FORMATS = {"BID_JSON", "XML_NEW", "XML_LEGACY"}


def _item_value(item: dict, key: str, default: str = "") -> str:
    value = item.get(key, default)
    return default if value is None else str(value)


def serialize_xml(items: list[dict], project_code: str, source_version: str, legacy: bool) -> bytes:
    root_name = "PCCES" if legacy else "PCCESBidExchange"
    root = Element(root_name)
    root.set("version", "1.0" if legacy else "2.0")
    header = SubElement(root, "Header")
    SubElement(header, "ProjectCode").text = project_code
    SubElement(header, "SourceBudgetVersion").text = source_version
    rows = SubElement(root, "Items" if not legacy else "Detail")
    for index, item in enumerate(items, start=1):
        node = SubElement(rows, "Item" if not legacy else "Record")
        node.set("sequence", str(index))
        fields = {
            "SourceItemId": _item_value(item, "source_budget_item_id", _item_value(item, "id")),
            "Code": _item_value(item, "code").strip().upper(),
            "Name": _item_value(item, "name").strip(),
            "Unit": _item_value(item, "unit").strip(),
            "Quantity": _item_value(item, "quantity", "0"),
            "UnitPrice": _item_value(item, "unit_price", "0"),
            "Amount": _item_value(item, "amount", "0"),
        }
        for name, value in fields.items():
            SubElement(node, name).text = value
    return tostring(root, encoding="utf-8", xml_declaration=True)


class ConversionExportJobService:
    def __init__(self, engine):
        self.engine = engine
        metadata.create_all(engine)

    def create(self, body: dict, actor: str) -> dict:
        wizard_id = str(body.get("wizard_session_id", "")).strip()
        source_version = str(body.get("source_budget_version_id", "")).strip()
        target = str(body.get("target_project_code", "")).strip()
        fmt = str(body.get("format", "BID_JSON")).strip().upper()
        items = list(body.get("items") or [])
        if not wizard_id or not source_version or not target:
            raise ValueError("wizard_session_id, source_budget_version_id and target_project_code are required")
        if fmt not in SUPPORTED_FORMATS:
            raise ValueError("format must be BID_JSON, XML_NEW or XML_LEGACY")
        if not items:
            raise ValueError("items are required")
        if fmt == "BID_JSON":
            payload = json.dumps({"project_code": target, "source_budget_version_id": source_version, "items": items}, ensure_ascii=False, sort_keys=True, indent=2).encode("utf-8")
            extension, content_type = "json", "application/json; charset=utf-8"
        else:
            payload = serialize_xml(items, target, source_version, fmt == "XML_LEGACY")
            extension, content_type = "xml", "application/xml; charset=utf-8"
        digest = hashlib.sha256(payload).hexdigest()
        job_id = str(uuid4())
        filename = f"{target}-{fmt.lower()}.{extension}"
        now = datetime.now(timezone.utc)
        meta = {"item_count": len(items), "serializer": "P4-EXPORT-001", "format_version": "1.0" if fmt == "XML_LEGACY" else "2.0"}
        with self.engine.begin() as conn:
            conn.execute(conversion_export_jobs.insert().values(
                id=job_id, wizard_session_id=wizard_id, source_budget_version_id=source_version,
                target_project_code=target, format=fmt, status="COMPLETED", filename=filename,
                content_type=content_type, size_bytes=len(payload), sha256=digest, artifact=payload,
                metadata_json=json.dumps(meta, ensure_ascii=False, sort_keys=True), created_by=actor,
                created_at=now, row_version=1,
            ))
        return self.get(job_id)

    def get(self, job_id: str) -> dict:
        with self.engine.connect() as conn:
            row = conn.execute(select(conversion_export_jobs).where(conversion_export_jobs.c.id == job_id)).mappings().first()
        if not row:
            raise LookupError("conversion export job not found")
        return {
            "id": row["id"], "wizard_session_id": row["wizard_session_id"],
            "source_budget_version_id": row["source_budget_version_id"],
            "target_project_code": row["target_project_code"], "format": row["format"],
            "status": row["status"], "filename": row["filename"], "content_type": row["content_type"],
            "size_bytes": row["size_bytes"], "sha256": row["sha256"],
            "metadata": json.loads(row["metadata_json"]), "created_by": row["created_by"],
            "created_at": row["created_at"].isoformat(), "row_version": row["row_version"],
            "download_url": f"/api/conversions/export-jobs/{row['id']}/download",
        }

    def artifact(self, job_id: str) -> tuple[bytes, str, str]:
        with self.engine.connect() as conn:
            row = conn.execute(select(
                conversion_export_jobs.c.artifact, conversion_export_jobs.c.content_type,
                conversion_export_jobs.c.filename,
            ).where(conversion_export_jobs.c.id == job_id)).first()
        if not row:
            raise LookupError("conversion export job not found")
        return bytes(row[0]), row[1], row[2]


def build_conversion_export_job_blueprint(service: ConversionExportJobService, resolve_user_id):
    bp = Blueprint("conversion_export_jobs", __name__, url_prefix="/api/conversions")

    @bp.post("/export-jobs")
    def create_job():
        actor = resolve_user_id()
        if actor is None:
            return jsonify({"code": "UNAUTHORIZED"}), 401
        try:
            return jsonify(service.create(request.get_json(silent=True) or {}, str(actor))), 201
        except ValueError as exc:
            return jsonify({"code": "INVALID_ARGUMENT", "detail": str(exc)}), 400

    @bp.get("/export-jobs/<job_id>")
    def get_job(job_id: str):
        try:
            return jsonify(service.get(job_id))
        except LookupError as exc:
            return jsonify({"code": "NOT_FOUND", "detail": str(exc)}), 404

    @bp.get("/export-jobs/<job_id>/download")
    def download(job_id: str):
        try:
            content, content_type, filename = service.artifact(job_id)
        except LookupError as exc:
            return jsonify({"code": "NOT_FOUND", "detail": str(exc)}), 404
        return Response(content, content_type=content_type, headers={"Content-Disposition": f'attachment; filename="{filename}"'})

    return bp
