"""Budget approval workflow, separated permissions, item locks and audit."""
from __future__ import annotations

import json
from datetime import datetime, timezone
from uuid import uuid4

from flask import Blueprint, jsonify, request
from sqlalchemy import Boolean, Column, DateTime, MetaData, String, Table, Text, and_, select

from api.models import User, UserRole

metadata = MetaData()
budget_approval_states = Table(
    "budget_approval_states", metadata,
    Column("project_code", String(100), primary_key=True),
    Column("status", String(30), nullable=False),
    Column("submitted_by", String(100)),
    Column("reviewed_by", String(100)),
    Column("comment", Text),
    Column("row_version", String(30), nullable=False),
    Column("updated_at", DateTime(timezone=True), nullable=False),
)
budget_item_locks = Table(
    "budget_item_locks", metadata,
    Column("project_code", String(100), primary_key=True),
    Column("item_id", String(100), primary_key=True),
    Column("locked", Boolean, nullable=False),
    Column("reason", String(500)),
    Column("locked_by", String(100)),
    Column("updated_at", DateTime(timezone=True), nullable=False),
)
budget_workflow_audit = Table(
    "budget_workflow_audit", metadata,
    Column("id", String(100), primary_key=True),
    Column("project_code", String(100), nullable=False, index=True),
    Column("item_id", String(100), index=True),
    Column("event_type", String(80), nullable=False),
    Column("actor_id", String(100), nullable=False),
    Column("from_status", String(30)),
    Column("to_status", String(30)),
    Column("payload_json", Text, nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
)

TRANSITIONS = {
    "SUBMIT": {"DRAFT": "SUBMITTED", "RETURNED": "SUBMITTED"},
    "APPROVE": {"SUBMITTED": "APPROVED"},
    "RETURN": {"SUBMITTED": "RETURNED"},
    "REOPEN": {"APPROVED": "DRAFT"},
}


class BudgetApprovalService:
    def __init__(self, engine, session_factory, version_service):
        self.engine = engine
        self.session_factory = session_factory
        self.version_service = version_service
        metadata.create_all(engine)

    def _role(self, actor_id: int) -> str:
        db = self.session_factory()
        try:
            user = db.query(User).filter(User.id == actor_id).first()
            if not user or not user.is_active:
                raise PermissionError("active actor required")
            return str(user.role)
        finally:
            db.close()

    def state(self, project_code: str) -> dict:
        with self.engine.connect() as conn:
            row = conn.execute(select(budget_approval_states).where(
                budget_approval_states.c.project_code == project_code
            )).mappings().first()
        if not row:
            return {"project_code": project_code, "status": "DRAFT", "row_version": 0,
                    "submitted_by": None, "reviewed_by": None, "comment": None}
        return {**dict(row), "updated_at": row["updated_at"].isoformat(),
                "row_version": int(row["row_version"])}

    def transition(self, project_code: str, command: str, actor_id: int, comment: str | None,
                   expected_version: int) -> dict:
        command = command.upper()
        role = self._role(actor_id)
        if command == "SUBMIT" and role not in {UserRole.EDITOR.value, UserRole.REVIEWER.value, UserRole.ADMIN.value}:
            raise PermissionError("editor permission required")
        if command in {"APPROVE", "RETURN", "REOPEN"} and role not in {UserRole.REVIEWER.value, UserRole.ADMIN.value}:
            raise PermissionError("reviewer permission required")
        current = self.state(project_code)
        if int(current["row_version"]) != int(expected_version):
            raise RuntimeError("approval row version conflict")
        target = TRANSITIONS.get(command, {}).get(current["status"])
        if not target:
            raise ValueError(f"invalid transition {current['status']} -> {command}")
        now = datetime.now(timezone.utc)
        values = {
            "status": target,
            "submitted_by": str(actor_id) if command == "SUBMIT" else current.get("submitted_by"),
            "reviewed_by": str(actor_id) if command in {"APPROVE", "RETURN", "REOPEN"} else current.get("reviewed_by"),
            "comment": comment,
            "row_version": str(expected_version + 1),
            "updated_at": now,
        }
        with self.engine.begin() as conn:
            exists = conn.execute(select(budget_approval_states.c.project_code).where(
                budget_approval_states.c.project_code == project_code
            )).first()
            if exists:
                result = conn.execute(budget_approval_states.update().where(and_(
                    budget_approval_states.c.project_code == project_code,
                    budget_approval_states.c.row_version == str(expected_version),
                )).values(**values))
                if result.rowcount != 1:
                    raise RuntimeError("approval row version conflict")
            else:
                if expected_version != 0:
                    raise RuntimeError("approval row version conflict")
                conn.execute(budget_approval_states.insert().values(project_code=project_code, **values))
            self._audit_conn(conn, project_code, None, command, str(actor_id), current["status"], target,
                             {"comment": comment})
        if target == "APPROVED":
            self.version_service.create_version(project_code, "APPROVED", str(actor_id), "APPROVED")
            self.version_service.set_lock(project_code, True, str(actor_id), "approved budget")
        elif target in {"RETURNED", "DRAFT"}:
            self.version_service.set_lock(project_code, False, str(actor_id), comment)
        return self.state(project_code)

    def set_item_lock(self, project_code: str, item_id: str, locked: bool, actor_id: int,
                      reason: str | None = None) -> dict:
        role = self._role(actor_id)
        if role not in {UserRole.REVIEWER.value, UserRole.ADMIN.value}:
            raise PermissionError("reviewer permission required")
        now = datetime.now(timezone.utc)
        with self.engine.begin() as conn:
            exists = conn.execute(select(budget_item_locks.c.item_id).where(and_(
                budget_item_locks.c.project_code == project_code,
                budget_item_locks.c.item_id == item_id,
            ))).first()
            values = {"locked": bool(locked), "reason": reason,
                      "locked_by": str(actor_id) if locked else None, "updated_at": now}
            if exists:
                conn.execute(budget_item_locks.update().where(and_(
                    budget_item_locks.c.project_code == project_code,
                    budget_item_locks.c.item_id == item_id,
                )).values(**values))
            else:
                conn.execute(budget_item_locks.insert().values(project_code=project_code, item_id=item_id, **values))
            self._audit_conn(conn, project_code, item_id, "ITEM_LOCK" if locked else "ITEM_UNLOCK",
                             str(actor_id), None, None, {"reason": reason})
        return self.item_lock(project_code, item_id)

    def item_lock(self, project_code: str, item_id: str) -> dict:
        with self.engine.connect() as conn:
            row = conn.execute(select(budget_item_locks).where(and_(
                budget_item_locks.c.project_code == project_code,
                budget_item_locks.c.item_id == item_id,
            ))).mappings().first()
        if not row:
            return {"project_code": project_code, "item_id": item_id, "locked": False}
        return {**dict(row), "updated_at": row["updated_at"].isoformat()}

    def assert_writable(self, project_code: str, item_id: str | None = None) -> None:
        self.version_service.assert_writable(project_code)
        if self.state(project_code)["status"] in {"SUBMITTED", "APPROVED"}:
            raise PermissionError("budget approval state is read-only")
        if item_id and self.item_lock(project_code, item_id).get("locked"):
            raise PermissionError("budget item is locked")

    def autosave_check(self, project_code: str, item_id: str | None, client_row_version: int,
                       current_row_version: int) -> dict:
        self.assert_writable(project_code, item_id)
        if int(client_row_version) != int(current_row_version):
            return {"allowed": False, "code": "CONFLICT", "current_row_version": current_row_version}
        return {"allowed": True, "code": "OK", "current_row_version": current_row_version}

    def audits(self, project_code: str) -> list[dict]:
        with self.engine.connect() as conn:
            rows = conn.execute(select(budget_workflow_audit).where(
                budget_workflow_audit.c.project_code == project_code
            ).order_by(budget_workflow_audit.c.created_at.desc())).mappings().all()
        return [{**dict(r), "payload": json.loads(r["payload_json"]),
                 "created_at": r["created_at"].isoformat()} for r in rows]

    @staticmethod
    def _audit_conn(conn, project_code, item_id, event_type, actor_id, from_status, to_status, payload):
        conn.execute(budget_workflow_audit.insert().values(
            id=str(uuid4()), project_code=project_code, item_id=item_id, event_type=event_type,
            actor_id=actor_id, from_status=from_status, to_status=to_status,
            payload_json=json.dumps(payload, ensure_ascii=False, sort_keys=True),
            created_at=datetime.now(timezone.utc),
        ))


def build_budget_approval_blueprint(service: BudgetApprovalService, resolve_user_id):
    bp = Blueprint("budget_approval", __name__, url_prefix="/api/decimal-budget")
    def actor():
        value = resolve_user_id()
        if value is None: raise PermissionError("authentication required")
        return int(value)

    @bp.get("/projects/<project_code>/approval")
    def state(project_code): return jsonify(service.state(project_code))
    @bp.post("/projects/<project_code>/approval/<command>")
    def transition(project_code, command):
        body = request.get_json(silent=True) or {}
        try:
            return jsonify(service.transition(project_code, command, actor(), body.get("comment"),
                                              int(body.get("row_version", 0))))
        except PermissionError as exc: return jsonify({"code":"FORBIDDEN","detail":str(exc)}),403
        except RuntimeError as exc: return jsonify({"code":"CONFLICT","detail":str(exc)}),409
        except ValueError as exc: return jsonify({"code":"INVALID_TRANSITION","detail":str(exc)}),400
    @bp.get("/projects/<project_code>/items/<item_id>/lock")
    def get_item_lock(project_code, item_id): return jsonify(service.item_lock(project_code,item_id))
    @bp.put("/projects/<project_code>/items/<item_id>/lock")
    def set_item_lock(project_code,item_id):
        body=request.get_json(silent=True) or {}
        try:return jsonify(service.set_item_lock(project_code,item_id,bool(body.get("locked")),actor(),body.get("reason")))
        except PermissionError as exc:return jsonify({"code":"FORBIDDEN","detail":str(exc)}),403
    @bp.post("/projects/<project_code>/autosave-check")
    def autosave_check(project_code):
        body=request.get_json(silent=True) or {}
        try:
            result=service.autosave_check(project_code,body.get("item_id"),int(body.get("row_version",0)),int(body.get("current_row_version",0)))
            return jsonify(result), 200 if result["allowed"] else 409
        except PermissionError as exc:return jsonify({"allowed":False,"code":"LOCKED","detail":str(exc)}),423
    @bp.get("/projects/<project_code>/workflow-audit")
    def audits(project_code): return jsonify(service.audits(project_code))
    return bp
