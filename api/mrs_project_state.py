"""Phase 3 Legacy-compatible MRS project state and write guard."""
from __future__ import annotations

from datetime import datetime, timezone
from flask import Blueprint, jsonify, request
from sqlalchemy import Column, DateTime, Integer, MetaData, String, Table, and_, select

metadata = MetaData()
mrs_project_states = Table(
    "mrs_project_states", metadata,
    Column("project_code", String(100), primary_key=True),
    Column("state", String(20), nullable=False),
    Column("template", Integer, nullable=False, default=0),
    Column("readonly", Integer, nullable=False, default=0),
    Column("reason", String(500)),
    Column("updated_by", String(100), nullable=False),
    Column("updated_at", DateTime(timezone=True), nullable=False),
    Column("row_version", Integer, nullable=False, default=1),
)

_ALLOWED_STATES = {"DRAFT", "SUBMITTED", "APPROVED", "ARCHIVED"}
_ALLOWED_TRANSITIONS = {
    "DRAFT": {"DRAFT", "SUBMITTED"},
    "SUBMITTED": {"DRAFT", "SUBMITTED", "APPROVED"},
    "APPROVED": {"APPROVED", "ARCHIVED"},
    "ARCHIVED": {"ARCHIVED"},
}

class MRSProjectStateService:
    def __init__(self, engine):
        self.engine = engine
        metadata.create_all(engine)

    def get(self, project_code: str) -> dict:
        with self.engine.connect() as conn:
            row = conn.execute(select(mrs_project_states).where(mrs_project_states.c.project_code == project_code)).mappings().first()
        if not row:
            return {"project_code": project_code, "state": "DRAFT", "template": False, "readonly": False,
                    "reason": None, "row_version": 0, "effective_readonly": False}
        result = dict(row)
        result["template"] = bool(result["template"])
        result["readonly"] = bool(result["readonly"])
        result["effective_readonly"] = result["readonly"] or result["template"] or result["state"] in {"APPROVED", "ARCHIVED"}
        result["updated_at"] = result["updated_at"].isoformat()
        return result

    def save(self, project_code: str, body: dict, actor: str) -> dict:
        state = str(body.get("state", "DRAFT")).strip().upper()
        if state not in _ALLOWED_STATES:
            raise ValueError("state must be DRAFT, SUBMITTED, APPROVED or ARCHIVED")
        requested = int(body.get("row_version", 0))
        now = datetime.now(timezone.utc)
        with self.engine.begin() as conn:
            current = conn.execute(select(mrs_project_states).where(mrs_project_states.c.project_code == project_code)).mappings().first()
            current_state = str(current["state"]) if current else "DRAFT"
            if current and requested != int(current["row_version"]):
                raise RuntimeError("CONFLICT")
            if state not in _ALLOWED_TRANSITIONS[current_state]:
                raise ValueError(f"invalid state transition {current_state} -> {state}")
            template = bool(body.get("template", current["template"] if current else False))
            readonly = bool(body.get("readonly", current["readonly"] if current else False))
            if template and state == "APPROVED":
                raise ValueError("template project cannot be approved")
            values = dict(state=state, template=int(template), readonly=int(readonly),
                          reason=str(body.get("reason") or "") or None, updated_by=actor,
                          updated_at=now, row_version=(int(current["row_version"]) + 1 if current else 1))
            if current:
                result = conn.execute(mrs_project_states.update().where(and_(
                    mrs_project_states.c.project_code == project_code,
                    mrs_project_states.c.row_version == requested,
                )).values(**values))
                if result.rowcount != 1:
                    raise RuntimeError("CONFLICT")
            else:
                conn.execute(mrs_project_states.insert().values(project_code=project_code, **values))
        return self.get(project_code)

    def assert_writable(self, project_code: str) -> None:
        state = self.get(project_code)
        if state["effective_readonly"]:
            raise PermissionError("project MRS is read-only")


def build_mrs_project_state_blueprint(service: MRSProjectStateService, resolve_user_id):
    bp = Blueprint("mrs_project_state", __name__, url_prefix="/api/mrs/projects")

    @bp.get("/<project_code>/state")
    def get_state(project_code: str):
        return jsonify(service.get(project_code))

    @bp.put("/<project_code>/state")
    def put_state(project_code: str):
        actor = resolve_user_id()
        if actor is None:
            return jsonify({"code": "UNAUTHORIZED"}), 401
        try:
            return jsonify(service.save(project_code, request.get_json(silent=True) or {}, str(actor)))
        except RuntimeError:
            return jsonify({"code": "CONFLICT", "detail": "stale project state row_version"}), 409
        except ValueError as exc:
            return jsonify({"code": "INVALID_ARGUMENT", "detail": str(exc)}), 400

    return bp
