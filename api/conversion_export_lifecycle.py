"""Phase 4 export lifecycle: schema validation, retries, versions and XLSX."""
from __future__ import annotations

import hashlib
import io
import json
import zipfile
from datetime import datetime, timezone
from uuid import uuid4
from xml.etree.ElementTree import fromstring

from flask import Blueprint, Response, jsonify, request
from sqlalchemy import Column, DateTime, Integer, LargeBinary, MetaData, String, Table, Text, select

metadata = MetaData()
export_artifact_versions = Table(
    "conversion_export_artifact_versions", metadata,
    Column("id", String(100), primary_key=True),
    Column("job_id", String(100), nullable=False, index=True),
    Column("version_no", Integer, nullable=False),
    Column("format", String(30), nullable=False),
    Column("status", String(20), nullable=False),
    Column("filename", String(300), nullable=False),
    Column("content_type", String(100), nullable=False),
    Column("size_bytes", Integer, nullable=False),
    Column("sha256", String(64), nullable=False),
    Column("artifact", LargeBinary, nullable=False),
    Column("validation_json", Text, nullable=False),
    Column("error_message", Text, nullable=False, default=""),
    Column("created_by", String(100), nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
)


def validate_xml(payload: bytes, fmt: str) -> dict:
    errors: list[str] = []
    try:
        root = fromstring(payload)
        expected = "PCCES" if fmt == "XML_LEGACY" else "PCCESBidExchange"
        version = "1.0" if fmt == "XML_LEGACY" else "2.0"
        if root.tag != expected: errors.append(f"root must be {expected}")
        if root.attrib.get("version") != version: errors.append(f"version must be {version}")
        if root.find("Header") is None: errors.append("Header is required")
        rows = root.find("Detail" if fmt == "XML_LEGACY" else "Items")
        if rows is None: errors.append("item collection is required")
    except Exception as exc:
        errors.append(str(exc))
    return {"valid": not errors, "errors": errors, "schema": f"PCCES-{fmt}-1"}


def serialize_xlsx(items: list[dict], project: str, source_version: str) -> bytes:
    def esc(value: object) -> str:
        return str(value if value is not None else "").replace("&", "&amp;").replace("<", "&lt;").replace(">", "&gt;")
    headers = ["來源工項ID", "工項編碼", "名稱", "單位", "數量", "單價", "金額"]
    rows = [headers] + [[i.get("source_budget_item_id", i.get("id", "")), str(i.get("code", "")).upper(), i.get("name", ""), i.get("unit", ""), i.get("quantity", "0"), i.get("unit_price", "0"), i.get("amount", "0")] for i in items]
    sheet_rows = []
    for r, row in enumerate(rows, 1):
        cells = "".join(f'<c r="{chr(64+c)}{r}" t="inlineStr"><is><t>{esc(v)}</t></is></c>' for c, v in enumerate(row, 1))
        sheet_rows.append(f'<row r="{r}">{cells}</row>')
    sheet = '<?xml version="1.0" encoding="UTF-8" standalone="yes"?><worksheet xmlns="http://schemas.openxmlformats.org/spreadsheetml/2006/main"><sheetData>' + "".join(sheet_rows) + '</sheetData></worksheet>'
    files = {
        "[Content_Types].xml": '<?xml version="1.0"?><Types xmlns="http://schemas.openxmlformats.org/package/2006/content-types"><Default Extension="rels" ContentType="application/vnd.openxmlformats-package.relationships+xml"/><Default Extension="xml" ContentType="application/xml"/><Override PartName="/xl/workbook.xml" ContentType="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml"/><Override PartName="/xl/worksheets/sheet1.xml" ContentType="application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml"/></Types>',
        "_rels/.rels": '<?xml version="1.0"?><Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships"><Relationship Id="rId1" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument" Target="xl/workbook.xml"/></Relationships>',
        "xl/workbook.xml": f'<?xml version="1.0"?><workbook xmlns="http://schemas.openxmlformats.org/spreadsheetml/2006/main" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships"><sheets><sheet name="電子標單" sheetId="1" r:id="rId1"/></sheets><definedNames><definedName name="ProjectCode">"{esc(project)}"</definedName><definedName name="SourceVersion">"{esc(source_version)}"</definedName></definedNames></workbook>',
        "xl/_rels/workbook.xml.rels": '<?xml version="1.0"?><Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships"><Relationship Id="rId1" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet" Target="worksheets/sheet1.xml"/></Relationships>',
        "xl/worksheets/sheet1.xml": sheet,
    }
    out = io.BytesIO()
    with zipfile.ZipFile(out, "w", zipfile.ZIP_DEFLATED) as zf:
        for name, content in files.items(): zf.writestr(name, content)
    return out.getvalue()


class ConversionExportLifecycleService:
    def __init__(self, engine):
        self.engine = engine; metadata.create_all(engine)

    def create_version(self, body: dict, actor: str) -> dict:
        job_id = str(body.get("job_id", "")).strip(); fmt = str(body.get("format", "XLSX")).upper()
        items = list(body.get("items") or []); project = str(body.get("target_project_code", "")).strip(); source = str(body.get("source_budget_version_id", "")).strip()
        if not job_id or not project or not source or not items: raise ValueError("job_id, target project, source version and items are required")
        if fmt != "XLSX": raise ValueError("lifecycle version currently requires XLSX")
        payload = serialize_xlsx(items, project, source); validation = {"valid": zipfile.is_zipfile(io.BytesIO(payload)), "errors": [], "schema": "OOXML-XLSX"}
        if not validation["valid"]: raise RuntimeError("artifact validation failed")
        with self.engine.begin() as conn:
            current = conn.execute(select(export_artifact_versions.c.version_no).where(export_artifact_versions.c.job_id == job_id).order_by(export_artifact_versions.c.version_no.desc())).first()
            version = (current[0] if current else 0) + 1; artifact_id = str(uuid4()); now = datetime.now(timezone.utc)
            filename = f"{project}-bid-v{version}.xlsx"; digest = hashlib.sha256(payload).hexdigest()
            conn.execute(export_artifact_versions.insert().values(id=artifact_id, job_id=job_id, version_no=version, format=fmt, status="COMPLETED", filename=filename, content_type="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", size_bytes=len(payload), sha256=digest, artifact=payload, validation_json=json.dumps(validation), error_message="", created_by=actor, created_at=now))
        return self.get(artifact_id)

    def get(self, artifact_id: str) -> dict:
        with self.engine.connect() as conn: row = conn.execute(select(export_artifact_versions).where(export_artifact_versions.c.id == artifact_id)).mappings().first()
        if not row: raise LookupError("export artifact version not found")
        item = dict(row); item.pop("artifact"); item["validation"] = json.loads(item.pop("validation_json")); item["created_at"] = item["created_at"].isoformat(); item["download_url"] = f"/api/conversions/export-artifacts/{artifact_id}/download"; return item

    def retry(self, artifact_id: str, actor: str) -> dict:
        with self.engine.connect() as conn: row = conn.execute(select(export_artifact_versions).where(export_artifact_versions.c.id == artifact_id)).mappings().first()
        if not row: raise LookupError("export artifact version not found")
        with self.engine.begin() as conn:
            next_no = conn.execute(select(export_artifact_versions.c.version_no).where(export_artifact_versions.c.job_id == row["job_id"]).order_by(export_artifact_versions.c.version_no.desc())).first()[0] + 1
            new_id = str(uuid4()); now = datetime.now(timezone.utc); filename = row["filename"].rsplit("-v", 1)[0] + f"-v{next_no}.xlsx"
            conn.execute(export_artifact_versions.insert().values(id=new_id, job_id=row["job_id"], version_no=next_no, format=row["format"], status="COMPLETED", filename=filename, content_type=row["content_type"], size_bytes=row["size_bytes"], sha256=row["sha256"], artifact=row["artifact"], validation_json=row["validation_json"], error_message="", created_by=actor, created_at=now))
        return self.get(new_id)

    def artifact(self, artifact_id: str):
        with self.engine.connect() as conn: row = conn.execute(select(export_artifact_versions.c.artifact, export_artifact_versions.c.content_type, export_artifact_versions.c.filename).where(export_artifact_versions.c.id == artifact_id)).first()
        if not row: raise LookupError("export artifact version not found")
        return bytes(row[0]), row[1], row[2]


def build_conversion_export_lifecycle_blueprint(service, resolve_user_id):
    bp = Blueprint("conversion_export_lifecycle", __name__, url_prefix="/api/conversions")
    @bp.post("/export-artifacts")
    def create():
        actor = resolve_user_id()
        if actor is None: return jsonify({"code":"UNAUTHORIZED"}), 401
        try: return jsonify(service.create_version(request.get_json(silent=True) or {}, str(actor))), 201
        except ValueError as exc: return jsonify({"code":"INVALID_ARGUMENT","detail":str(exc)}), 400
        except RuntimeError as exc: return jsonify({"code":"EXPORT_FAILED","detail":str(exc)}), 422
    @bp.post("/export-artifacts/<artifact_id>/retry")
    def retry(artifact_id):
        actor = resolve_user_id()
        if actor is None: return jsonify({"code":"UNAUTHORIZED"}), 401
        try: return jsonify(service.retry(artifact_id, str(actor))), 201
        except LookupError as exc: return jsonify({"code":"NOT_FOUND","detail":str(exc)}), 404
    @bp.get("/export-artifacts/<artifact_id>")
    def get(artifact_id):
        try: return jsonify(service.get(artifact_id))
        except LookupError as exc: return jsonify({"code":"NOT_FOUND","detail":str(exc)}), 404
    @bp.get("/export-artifacts/<artifact_id>/download")
    def download(artifact_id):
        try: payload, ctype, filename = service.artifact(artifact_id)
        except LookupError as exc: return jsonify({"code":"NOT_FOUND","detail":str(exc)}), 404
        return Response(payload, content_type=ctype, headers={"Content-Disposition": f'attachment; filename="{filename}"'})
    @bp.post("/validate-xml")
    def validate():
        body = request.get_json(silent=True) or {}; return jsonify(validate_xml(str(body.get("xml", "")).encode(), str(body.get("format", "XML_NEW")).upper()))
    return bp
