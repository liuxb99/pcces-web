"""Phase 4 long-running conversion jobs with progress and cancellation."""
from __future__ import annotations

import json
from datetime import datetime, timezone
from uuid import uuid4

from flask import Blueprint, jsonify, request
from sqlalchemy import Boolean, Column, DateTime, Integer, MetaData, String, Table, Text, and_, select

metadata = MetaData()
conversion_long_jobs = Table(
    "conversion_long_jobs", metadata,
    Column("id", String(100), primary_key=True),
    Column("job_type", String(20), nullable=False),
    Column("status", String(20), nullable=False),
    Column("progress", Integer, nullable=False),
    Column("stage", String(100), nullable=False),
    Column("payload_json", Text, nullable=False),
    Column("result_json", Text, nullable=True),
    Column("error_json", Text, nullable=True),
    Column("cancel_requested", Boolean, nullable=False, default=False),
    Column("created_by", String(100), nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
    Column("updated_at", DateTime(timezone=True), nullable=False),
    Column("row_version", Integer, nullable=False, default=1),
)

TERMINAL = {"COMPLETED", "FAILED", "CANCELLED"}


class ConversionLongJobService:
    def __init__(self, engine):
        self.engine = engine
        metadata.create_all(engine)

    def create(self, body: dict, actor: str) -> dict:
        job_type = str(body.get("job_type", "")).strip().upper()
        if job_type not in {"IMPORT", "EXPORT"}:
            raise ValueError("job_type must be IMPORT or EXPORT")
        now = datetime.now(timezone.utc)
        job_id = str(uuid4())
        with self.engine.begin() as conn:
            conn.execute(conversion_long_jobs.insert().values(
                id=job_id, job_type=job_type, status="QUEUED", progress=0, stage="QUEUED",
                payload_json=json.dumps(body.get("payload") or {}, ensure_ascii=False, sort_keys=True),
                result_json=None, error_json=None, cancel_requested=False,
                created_by=actor, created_at=now, updated_at=now, row_version=1,
            ))
        return self.get(job_id)

    def get(self, job_id: str) -> dict:
        with self.engine.connect() as conn:
            row = conn.execute(select(conversion_long_jobs).where(conversion_long_jobs.c.id == job_id)).mappings().first()
        if not row:
            raise LookupError("conversion job not found")
        item = dict(row)
        for key in ("payload_json", "result_json", "error_json"):
            value = item.pop(key)
            item[key.removesuffix("_json")] = json.loads(value) if value else None
        item["created_at"] = item["created_at"].isoformat()
        item["updated_at"] = item["updated_at"].isoformat()
        item["deep_link"] = f"/app/conversions/jobs/{job_id}"
        return item

    def advance(self, job_id: str, body: dict) -> dict:
        current = self.get(job_id)
        if current["status"] in TERMINAL:
            raise ValueError("terminal job cannot advance")
        requested = int(body.get("row_version", 0))
        if requested != int(current["row_version"]):
            raise RuntimeError("stale conversion job row_version")
        progress = int(body.get("progress", current["progress"]))
        if progress < int(current["progress"]) or progress > 100:
            raise ValueError("progress must be monotonic and between 0 and 100")
        now = datetime.now(timezone.utc)
        status = str(body.get("status") or ("COMPLETED" if progress == 100 else "RUNNING")).upper()
        if current["cancel_requested"]:
            status, progress = "CANCELLED", int(current["progress"])
        if status not in {"RUNNING", "COMPLETED", "FAILED", "CANCELLED"}:
            raise ValueError("invalid job status")
        result = body.get("result") if status == "COMPLETED" else None
        error = body.get("error") if status == "FAILED" else None
        with self.engine.begin() as conn:
            changed = conn.execute(conversion_long_jobs.update().where(and_(
                conversion_long_jobs.c.id == job_id,
                conversion_long_jobs.c.row_version == requested,
            )).values(
                status=status, progress=progress, stage=str(body.get("stage") or status),
                result_json=json.dumps(result, ensure_ascii=False, sort_keys=True) if result is not None else None,
                error_json=json.dumps(error, ensure_ascii=False, sort_keys=True) if error is not None else None,
                updated_at=now, row_version=requested + 1,
            ))
            if changed.rowcount != 1:
                raise RuntimeError("conversion job update conflict")
        return self.get(job_id)

    def cancel(self, job_id: str, row_version: int) -> dict:
        current = self.get(job_id)
        if current["status"] in TERMINAL:
            return current
        if int(row_version) != int(current["row_version"]):
            raise RuntimeError("stale conversion job row_version")
        now = datetime.now(timezone.utc)
        with self.engine.begin() as conn:
            changed = conn.execute(conversion_long_jobs.update().where(and_(
                conversion_long_jobs.c.id == job_id,
                conversion_long_jobs.c.row_version == row_version,
            )).values(
                status="CANCELLED", stage="CANCELLED", cancel_requested=True,
                result_json=None, error_json=None, updated_at=now, row_version=row_version + 1,
            ))
            if changed.rowcount != 1:
                raise RuntimeError("conversion job cancel conflict")
        return self.get(job_id)


def build_conversion_long_job_blueprint(service: ConversionLongJobService, resolve_user_id):
    bp = Blueprint("conversion_long_jobs", __name__, url_prefix="/api/conversions/jobs")

    @bp.post("")
    def create_job():
        actor = resolve_user_id()
        if actor is None:
            return jsonify({"code": "UNAUTHORIZED"}), 401
        try:
            return jsonify(service.create(request.get_json(silent=True) or {}, str(actor))), 201
        except ValueError as exc:
            return jsonify({"code": "INVALID_ARGUMENT", "detail": str(exc)}), 400

    @bp.get("/<job_id>")
    def get_job(job_id: str):
        try:
            return jsonify(service.get(job_id))
        except LookupError as exc:
            return jsonify({"code": "NOT_FOUND", "detail": str(exc)}), 404

    @bp.post("/<job_id>/advance")
    def advance(job_id: str):
        try:
            return jsonify(service.advance(job_id, request.get_json(silent=True) or {}))
        except LookupError as exc:
            return jsonify({"code": "NOT_FOUND", "detail": str(exc)}), 404
        except RuntimeError as exc:
            return jsonify({"code": "CONFLICT", "detail": str(exc)}), 409
        except ValueError as exc:
            return jsonify({"code": "INVALID_ARGUMENT", "detail": str(exc)}), 400

    @bp.post("/<job_id>/cancel")
    def cancel(job_id: str):
        try:
            body = request.get_json(silent=True) or {}
            return jsonify(service.cancel(job_id, int(body.get("row_version", 0))))
        except LookupError as exc:
            return jsonify({"code": "NOT_FOUND", "detail": str(exc)}), 404
        except RuntimeError as exc:
            return jsonify({"code": "CONFLICT", "detail": str(exc)}), 409

    return bp
