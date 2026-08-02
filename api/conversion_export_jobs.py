"""Phase 4 conversion export jobs and versioned artifact lifecycle."""
from __future__ import annotations

import hashlib
import json
from datetime import datetime, timezone
from uuid import uuid4
from xml.etree.ElementTree import Element, SubElement, tostring

from flask import Blueprint, Response, jsonify, request
from sqlalchemy import Column, DateTime, Integer, LargeBinary, MetaData, String, Table, Text, select

from api.bid_budget_import_apply import BidBudgetImportApplyService
from api.bid_budget_roundtrip import BidBudgetRoundTripService, detect_and_parse, import_preflight
from api.conversion_export_lifecycle import ConversionExportLifecycleService, validate_xml

metadata = MetaData()
conversion_export_jobs = Table(
    "conversion_export_jobs", metadata,
    Column("id", String(100), primary_key=True), Column("wizard_session_id", String(100), nullable=False, index=True),
    Column("source_budget_version_id", String(100), nullable=False), Column("target_project_code", String(100), nullable=False),
    Column("format", String(30), nullable=False), Column("status", String(20), nullable=False),
    Column("filename", String(300), nullable=False), Column("content_type", String(100), nullable=False),
    Column("size_bytes", Integer, nullable=False), Column("sha256", String(64), nullable=False),
    Column("artifact", LargeBinary, nullable=False), Column("metadata_json", Text, nullable=False),
    Column("created_by", String(100), nullable=False), Column("created_at", DateTime(timezone=True), nullable=False),
    Column("row_version", Integer, nullable=False, default=1),
)
SUPPORTED_FORMATS = {"BID_JSON", "XML_NEW", "XML_LEGACY"}

def _v(item, key, default=""):
    value = item.get(key, default); return default if value is None else str(value)

def serialize_xml(items, project_code, source_version, legacy):
    root = Element("PCCES" if legacy else "PCCESBidExchange", version="1.0" if legacy else "2.0")
    header = SubElement(root, "Header"); SubElement(header, "ProjectCode").text = project_code; SubElement(header, "SourceBudgetVersion").text = source_version
    rows = SubElement(root, "Detail" if legacy else "Items")
    for index, item in enumerate(items, 1):
        node = SubElement(rows, "Record" if legacy else "Item", sequence=str(index))
        fields = {"SourceItemId": _v(item,"source_budget_item_id",_v(item,"id")), "Code": _v(item,"code").strip().upper(), "Name": _v(item,"name").strip(), "Unit": _v(item,"unit").strip(), "Quantity": _v(item,"quantity","0"), "UnitPrice": _v(item,"unit_price","0"), "Amount": _v(item,"amount","0")}
        for name, value in fields.items(): SubElement(node, name).text = value
    return tostring(root, encoding="utf-8", xml_declaration=True)

class ConversionExportJobService:
    def __init__(self, engine):
        self.engine = engine
        metadata.create_all(engine)
        self.lifecycle = ConversionExportLifecycleService(engine)
        self.roundtrip = BidBudgetRoundTripService(engine)
        self.import_apply = BidBudgetImportApplyService(engine)
    def create(self, body, actor):
        wizard_id = str(body.get("wizard_session_id","")).strip(); source = str(body.get("source_budget_version_id","")).strip(); target = str(body.get("target_project_code","")).strip(); fmt = str(body.get("format","BID_JSON")).upper(); items = list(body.get("items") or [])
        if not wizard_id or not source or not target: raise ValueError("wizard_session_id, source_budget_version_id and target_project_code are required")
        if fmt not in SUPPORTED_FORMATS: raise ValueError("format must be BID_JSON, XML_NEW or XML_LEGACY")
        if not items: raise ValueError("items are required")
        if fmt == "BID_JSON": payload = json.dumps({"project_code":target,"source_budget_version_id":source,"items":items}, ensure_ascii=False, sort_keys=True, indent=2).encode(); ext="json"; ctype="application/json; charset=utf-8"; validation={"valid":True,"errors":[],"schema":"PCCES-BID-JSON-1"}
        else: payload = serialize_xml(items,target,source,fmt=="XML_LEGACY"); ext="xml"; ctype="application/xml; charset=utf-8"; validation=validate_xml(payload,fmt)
        if not validation["valid"]: raise RuntimeError("XML schema validation failed")
        job_id=str(uuid4()); digest=hashlib.sha256(payload).hexdigest(); filename=f"{target}-{fmt.lower()}.{ext}"; now=datetime.now(timezone.utc)
        meta={"item_count":len(items),"serializer":"P4-EXPORT-002","format_version":"1.0" if fmt=="XML_LEGACY" else "2.0","validation":validation}
        with self.engine.begin() as conn: conn.execute(conversion_export_jobs.insert().values(id=job_id,wizard_session_id=wizard_id,source_budget_version_id=source,target_project_code=target,format=fmt,status="COMPLETED",filename=filename,content_type=ctype,size_bytes=len(payload),sha256=digest,artifact=payload,metadata_json=json.dumps(meta,ensure_ascii=False,sort_keys=True),created_by=actor,created_at=now,row_version=1))
        return self.get(job_id)
    def get(self, job_id):
        with self.engine.connect() as conn: row=conn.execute(select(conversion_export_jobs).where(conversion_export_jobs.c.id==job_id)).mappings().first()
        if not row: raise LookupError("conversion export job not found")
        return {"id":row["id"],"wizard_session_id":row["wizard_session_id"],"source_budget_version_id":row["source_budget_version_id"],"target_project_code":row["target_project_code"],"format":row["format"],"status":row["status"],"filename":row["filename"],"content_type":row["content_type"],"size_bytes":row["size_bytes"],"sha256":row["sha256"],"metadata":json.loads(row["metadata_json"]),"created_by":row["created_by"],"created_at":row["created_at"].isoformat(),"row_version":row["row_version"],"download_url":f"/api/conversions/export-jobs/{row['id']}/download"}
    def artifact(self, job_id):
        with self.engine.connect() as conn: row=conn.execute(select(conversion_export_jobs.c.artifact,conversion_export_jobs.c.content_type,conversion_export_jobs.c.filename).where(conversion_export_jobs.c.id==job_id)).first()
        if not row: raise LookupError("conversion export job not found")
        return bytes(row[0]),row[1],row[2]

def build_conversion_export_job_blueprint(service, resolve_user_id):
    bp=Blueprint("conversion_export_jobs",__name__,url_prefix="/api/conversions")
    @bp.post("/export-jobs")
    def create_job():
        actor=resolve_user_id()
        if actor is None: return jsonify({"code":"UNAUTHORIZED"}),401
        try: return jsonify(service.create(request.get_json(silent=True) or {},str(actor))),201
        except ValueError as exc: return jsonify({"code":"INVALID_ARGUMENT","detail":str(exc)}),400
        except RuntimeError as exc: return jsonify({"code":"EXPORT_FAILED","detail":str(exc)}),422
    @bp.get("/export-jobs/<job_id>")
    def get_job(job_id):
        try: return jsonify(service.get(job_id))
        except LookupError as exc: return jsonify({"code":"NOT_FOUND","detail":str(exc)}),404
    @bp.get("/export-jobs/<job_id>/download")
    def download(job_id):
        try: content,ctype,filename=service.artifact(job_id)
        except LookupError as exc: return jsonify({"code":"NOT_FOUND","detail":str(exc)}),404
        return Response(content,content_type=ctype,headers={"Content-Disposition":f'attachment; filename="{filename}"'})
    @bp.post("/export-artifacts")
    def create_artifact():
        actor=resolve_user_id()
        if actor is None: return jsonify({"code":"UNAUTHORIZED"}),401
        try: return jsonify(service.lifecycle.create_version(request.get_json(silent=True) or {},str(actor))),201
        except ValueError as exc: return jsonify({"code":"INVALID_ARGUMENT","detail":str(exc)}),400
    @bp.get("/export-artifacts/<artifact_id>")
    def get_artifact(artifact_id):
        try: return jsonify(service.lifecycle.get(artifact_id))
        except LookupError as exc: return jsonify({"code":"NOT_FOUND","detail":str(exc)}),404
    @bp.post("/export-artifacts/<artifact_id>/retry")
    def retry_artifact(artifact_id):
        actor=resolve_user_id()
        if actor is None: return jsonify({"code":"UNAUTHORIZED"}),401
        try: return jsonify(service.lifecycle.retry(artifact_id,str(actor))),201
        except LookupError as exc: return jsonify({"code":"NOT_FOUND","detail":str(exc)}),404
    @bp.get("/export-artifacts/<artifact_id>/download")
    def download_artifact(artifact_id):
        try: content,ctype,filename=service.lifecycle.artifact(artifact_id)
        except LookupError as exc: return jsonify({"code":"NOT_FOUND","detail":str(exc)}),404
        return Response(content,content_type=ctype,headers={"Content-Disposition":f'attachment; filename="{filename}"'})
    @bp.post("/validate-xml")
    def validate():
        body=request.get_json(silent=True) or {}; return jsonify(validate_xml(str(body.get("xml","")).encode(),str(body.get("format","XML_NEW")).upper()))
    @bp.post("/import-preflight")
    def import_preflight_route():
        if resolve_user_id() is None: return jsonify({"code":"UNAUTHORIZED"}),401
        body=request.get_json(silent=True) or {}
        try:
            fmt,version,project,items=detect_and_parse(str(body.get("payload","")),str(body.get("format","")))
            return jsonify({"format":fmt,"format_version":version,"source_bid_project_code":project,"report":import_preflight(items),"items":items})
        except Exception as exc: return jsonify({"code":"INVALID_ARGUMENT","detail":str(exc)}),400
    @bp.post("/import-sessions")
    def create_import_session():
        actor=resolve_user_id()
        if actor is None: return jsonify({"code":"UNAUTHORIZED"}),401
        try: return jsonify(service.roundtrip.create(request.get_json(silent=True) or {},str(actor))),201
        except Exception as exc: return jsonify({"code":"INVALID_ARGUMENT","detail":str(exc)}),400
    @bp.get("/import-sessions/<session_id>")
    def get_import_session(session_id):
        try: return jsonify(service.roundtrip.get(session_id))
        except LookupError as exc: return jsonify({"code":"NOT_FOUND","detail":str(exc)}),404
    @bp.post("/import-sessions/<session_id>/apply")
    def apply_import_session(session_id):
        actor=resolve_user_id()
        if actor is None: return jsonify({"code":"UNAUTHORIZED"}),401
        try: return jsonify(service.import_apply.apply(session_id,request.get_json(silent=True) or {},str(actor))),201
        except LookupError as exc: return jsonify({"code":"NOT_FOUND","detail":str(exc)}),404
        except PermissionError as exc: return jsonify({"code":"READ_ONLY","detail":str(exc)}),409
        except RuntimeError: return jsonify({"code":"CONFLICT","detail":"target budget project already contains items"}),409
        except (ValueError,ArithmeticError) as exc: return jsonify({"code":"INVALID_ARGUMENT","detail":str(exc)}),400
    @bp.get("/import-apply-runs/<run_id>")
    def get_import_apply_run(run_id):
        try: return jsonify(service.import_apply.get(run_id))
        except LookupError as exc: return jsonify({"code":"NOT_FOUND","detail":str(exc)}),404
    return bp
