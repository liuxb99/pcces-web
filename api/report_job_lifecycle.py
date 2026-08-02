"""Report job failure and retry lifecycle separated from rendering workers."""
from __future__ import annotations

import json
from datetime import datetime, timezone

from flask import Blueprint, jsonify, request
from sqlalchemy import and_, select, update

from api.report_center import report_jobs


class ReportJobLifecycleService:
    def __init__(self, engine): self.engine = engine

    def fail(self, job_id: str, body: dict) -> dict:
        expected = int(body.get("row_version", 0)); error = body.get("error") or {"message": "report rendering failed"}
        with self.engine.begin() as conn:
            row = conn.execute(select(report_jobs).where(report_jobs.c.id == job_id)).mappings().first()
            if not row: raise LookupError("report job not found")
            if row["row_version"] != expected: raise RuntimeError("row version conflict")
            if row["status"] not in {"QUEUED", "RUNNING"}: raise ValueError("job cannot fail from current status")
            conn.execute(update(report_jobs).where(and_(report_jobs.c.id == job_id, report_jobs.c.row_version == expected)).values(status="FAILED", error_json=json.dumps(error, ensure_ascii=False, sort_keys=True), updated_at=datetime.now(timezone.utc), row_version=expected + 1))
        return self.get(job_id)

    def retry(self, job_id: str, body: dict) -> dict:
        expected = int(body.get("row_version", 0))
        with self.engine.begin() as conn:
            row = conn.execute(select(report_jobs).where(report_jobs.c.id == job_id)).mappings().first()
            if not row: raise LookupError("report job not found")
            if row["row_version"] != expected: raise RuntimeError("row version conflict")
            if row["status"] != "FAILED": raise ValueError("only failed report jobs can retry")
            conn.execute(update(report_jobs).where(and_(report_jobs.c.id == job_id, report_jobs.c.row_version == expected)).values(status="QUEUED", progress=0, error_json=None, updated_at=datetime.now(timezone.utc), row_version=expected + 1))
        return self.get(job_id)

    def get(self, job_id: str) -> dict:
        with self.engine.connect() as conn: row = conn.execute(select(report_jobs).where(report_jobs.c.id == job_id)).mappings().first()
        if not row: raise LookupError("report job not found")
        return {"id": row["id"], "status": row["status"], "progress": row["progress"], "error": json.loads(row["error_json"]) if row["error_json"] else None, "row_version": row["row_version"]}


def build_report_job_lifecycle_blueprint(service, resolve_user_id):
    bp = Blueprint("report_job_lifecycle", __name__, url_prefix="/api/reports/jobs")
    @bp.post("/<job_id>/fail")
    def fail(job_id):
        if resolve_user_id() is None: return jsonify({"code":"UNAUTHORIZED"}), 401
        try: return jsonify(service.fail(job_id, request.get_json(silent=True) or {}))
        except LookupError as exc: return jsonify({"code":"NOT_FOUND","detail":str(exc)}), 404
        except RuntimeError as exc: return jsonify({"code":"CONFLICT","detail":str(exc)}), 409
        except ValueError as exc: return jsonify({"code":"INVALID_ARGUMENT","detail":str(exc)}), 400
    @bp.post("/<job_id>/retry")
    def retry(job_id):
        if resolve_user_id() is None: return jsonify({"code":"UNAUTHORIZED"}), 401
        try: return jsonify(service.retry(job_id, request.get_json(silent=True) or {}))
        except LookupError as exc: return jsonify({"code":"NOT_FOUND","detail":str(exc)}), 404
        except RuntimeError as exc: return jsonify({"code":"CONFLICT","detail":str(exc)}), 409
        except ValueError as exc: return jsonify({"code":"INVALID_ARGUMENT","detail":str(exc)}), 400
    return bp
