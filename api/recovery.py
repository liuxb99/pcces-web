"""Cross-target crash-recovery snapshots for Phase 0 WorkContext."""

from __future__ import annotations

from datetime import datetime, timezone
from hashlib import sha256
from typing import Callable
import json

from flask import Blueprint, jsonify, request
from sqlalchemy import Column, DateTime, Integer, MetaData, String, Table, Text, and_, select

from api.work_context import WorkContextService

metadata = MetaData()
recovery_snapshots = Table(
    "web_recovery_snapshots", metadata,
    Column("id", String(100), primary_key=True),
    Column("user_id", Integer, nullable=False, index=True),
    Column("context_id", String(100), nullable=True),
    Column("project_code", String(100), nullable=True),
    Column("action_code", String(64), nullable=True),
    Column("payload", Text, nullable=False),
    Column("payload_hash", String(64), nullable=False),
    Column("reason", String(100), nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
    Column("restored_at", DateTime(timezone=True), nullable=True),
    Column("discarded_at", DateTime(timezone=True), nullable=True),
    Column("row_version", Integer, nullable=False, default=1),
)


class RecoveryService:
    def __init__(self, engine, work_context_service: WorkContextService):
        self.engine = engine
        self.work_context_service = work_context_service

    def create_schema(self) -> None:
        metadata.create_all(self.engine)

    def create(self, snapshot_id: str, user_id: int, body: dict) -> tuple[dict, int]:
        payload = body.get("payload")
        reason = body.get("reason")
        if not snapshot_id or payload in (None, "") or not reason:
            return {"code": "INVALID_ARGUMENT", "detail": "id, payload and reason are required"}, 400
        encoded = payload if isinstance(payload, str) else json.dumps(payload, ensure_ascii=False, sort_keys=True)
        now = datetime.now(timezone.utc)
        values = {
            "id": snapshot_id,
            "user_id": user_id,
            "context_id": body.get("context_id"),
            "project_code": body.get("project_code"),
            "action_code": body.get("action_code"),
            "payload": encoded,
            "payload_hash": sha256(encoded.encode("utf-8")).hexdigest(),
            "reason": reason,
            "created_at": now,
            "row_version": 1,
        }
        try:
            with self.engine.begin() as conn:
                conn.execute(recovery_snapshots.insert().values(**values))
        except Exception:
            return {"code": "CONFLICT", "detail": "recovery snapshot already exists"}, 409
        return self.get(snapshot_id, user_id)

    def get(self, snapshot_id: str, user_id: int) -> tuple[dict, int]:
        with self.engine.connect() as conn:
            row = conn.execute(select(recovery_snapshots).where(and_(
                recovery_snapshots.c.id == snapshot_id,
                recovery_snapshots.c.user_id == user_id,
            ))).mappings().first()
        if row is None:
            return {"code": "NOT_FOUND"}, 404
        return dict(row), 200

    def list_pending(self, user_id: int) -> list[dict]:
        with self.engine.connect() as conn:
            rows = conn.execute(select(recovery_snapshots).where(and_(
                recovery_snapshots.c.user_id == user_id,
                recovery_snapshots.c.restored_at.is_(None),
                recovery_snapshots.c.discarded_at.is_(None),
            )).order_by(recovery_snapshots.c.created_at.desc())).mappings().all()
        return [dict(row) for row in rows]

    def resolve(self, snapshot_id: str, user_id: int, row_version: int, action: str) -> tuple[dict, int]:
        now = datetime.now(timezone.utc)
        with self.engine.begin() as conn:
            current = conn.execute(select(recovery_snapshots).where(and_(
                recovery_snapshots.c.id == snapshot_id,
                recovery_snapshots.c.user_id == user_id,
            ))).mappings().first()
            if current is None:
                return {"code": "NOT_FOUND"}, 404
            if current["restored_at"] is not None or current["discarded_at"] is not None or current["row_version"] != row_version:
                return {"code": "CONFLICT", "detail": "recovery snapshot state conflict"}, 409
            if action == "restore" and current["context_id"]:
                try:
                    draft = json.loads(current["payload"])
                except json.JSONDecodeError:
                    draft = current["payload"]
                result, status = self.work_context_service.apply(current["context_id"], user_id, "SAVE_DRAFT", {
                    "action_code": current["action_code"] or "PROJECT_CATALOG",
                    "project_code": current["project_code"],
                    "draft_payload": current["payload"] if isinstance(draft, str) else json.dumps(draft, ensure_ascii=False),
                })
                if status >= 400:
                    return result, status
            column = "restored_at" if action == "restore" else "discarded_at"
            conn.execute(recovery_snapshots.update().where(and_(
                recovery_snapshots.c.id == snapshot_id,
                recovery_snapshots.c.user_id == user_id,
                recovery_snapshots.c.row_version == row_version,
            )).values(**{column: now, "row_version": row_version + 1}))
        return self.get(snapshot_id, user_id)


def build_recovery_blueprint(service: RecoveryService, resolve_user_id: Callable[[], int | None]) -> Blueprint:
    blueprint = Blueprint("recovery", __name__, url_prefix="/api/recovery-snapshots")

    def current_user():
        user_id = resolve_user_id()
        if user_id is None:
            return None, (jsonify({"code": "UNAUTHORIZED"}), 401)
        return user_id, None

    @blueprint.get("")
    def list_snapshots():
        user_id, error = current_user()
        if error: return error
        return jsonify(service.list_pending(user_id))

    @blueprint.get("/<snapshot_id>")
    def get_snapshot(snapshot_id: str):
        user_id, error = current_user()
        if error: return error
        result, status = service.get(snapshot_id, user_id)
        return jsonify(result), status

    @blueprint.post("/<snapshot_id>")
    def create_snapshot(snapshot_id: str):
        user_id, error = current_user()
        if error: return error
        result, status = service.create(snapshot_id, user_id, request.get_json(silent=True) or {})
        return jsonify(result), status

    @blueprint.post("/<snapshot_id>/<action>")
    def resolve_snapshot(snapshot_id: str, action: str):
        user_id, error = current_user()
        if error: return error
        if action not in {"restore", "discard"}:
            return jsonify({"code": "INVALID_ARGUMENT"}), 400
        body = request.get_json(silent=True) or {}
        result, status = service.resolve(snapshot_id, user_id, int(body.get("row_version", 0)), action)
        return jsonify(result), status

    return blueprint
