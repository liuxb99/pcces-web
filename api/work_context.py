"""Phase 0 Web WorkContext and dirty-state lifecycle.

The module is independent from the legacy monolith and uses optimistic locking.
It exposes Save, Save Draft, Discard and Cancel semantics shared with Local Go.
"""

from __future__ import annotations

from dataclasses import asdict, dataclass
from datetime import datetime, timezone
from typing import Callable

from flask import Blueprint, jsonify, request
from sqlalchemy import Boolean, Column, DateTime, Integer, MetaData, String, Table, Text, and_, select

metadata = MetaData()
work_contexts = Table(
    "web_work_contexts", metadata,
    Column("id", String(100), primary_key=True),
    Column("user_id", Integer, nullable=False, index=True),
    Column("action_code", String(64), nullable=False),
    Column("project_code", String(100), nullable=True),
    Column("resource_type", String(100), nullable=True),
    Column("resource_id", String(100), nullable=True),
    Column("dirty", Boolean, nullable=False, default=False),
    Column("draft_payload", Text, nullable=True),
    Column("created_at", DateTime(timezone=True), nullable=False),
    Column("updated_at", DateTime(timezone=True), nullable=False),
    Column("row_version", Integer, nullable=False, default=1),
)


@dataclass(frozen=True)
class Transition:
    exists: bool
    dirty: bool
    row_version: int
    outcome: str


def transition(exists: bool, dirty: bool, row_version: int, command: str, request_row_version: int | None = None) -> Transition:
    command = command.upper()
    if exists and request_row_version is not None and request_row_version != row_version:
        return Transition(exists, dirty, row_version, "CONFLICT")
    if command == "SAVE_DRAFT":
        return Transition(True, True, 1 if not exists else row_version + 1, "DRAFT_SAVED")
    if command == "SAVE":
        return Transition(True, False, 1 if not exists else row_version + 1, "SAVED")
    if command == "DISCARD":
        if not exists:
            return Transition(False, False, 0, "NOT_FOUND")
        return Transition(True, False, row_version + 1, "DISCARDED")
    if command == "CANCEL":
        if not exists:
            return Transition(False, False, 0, "CANCELLED")
        if dirty:
            return Transition(True, True, row_version, "DECISION_REQUIRED")
        return Transition(False, False, 0, "CANCELLED")
    return Transition(exists, dirty, row_version, "INVALID_COMMAND")


class WorkContextService:
    def __init__(self, engine):
        self.engine = engine

    def create_schema(self) -> None:
        metadata.create_all(self.engine)

    def get(self, context_id: str, user_id: int) -> dict | None:
        with self.engine.connect() as conn:
            row = conn.execute(select(work_contexts).where(and_(work_contexts.c.id == context_id, work_contexts.c.user_id == user_id))).mappings().first()
        return dict(row) if row else None

    def apply(self, context_id: str, user_id: int, command: str, payload: dict) -> tuple[dict, int]:
        now = datetime.now(timezone.utc)
        with self.engine.begin() as conn:
            current = conn.execute(select(work_contexts).where(and_(work_contexts.c.id == context_id, work_contexts.c.user_id == user_id))).mappings().first()
            exists = current is not None
            current_version = int(current["row_version"]) if current else 0
            current_dirty = bool(current["dirty"]) if current else False
            requested = payload.get("row_version")
            result = transition(exists, current_dirty, current_version, command, requested)
            if result.outcome == "CONFLICT":
                return {"code":"CONFLICT","detail":"stale work context row_version","current_row_version":current_version}, 409
            if result.outcome == "DECISION_REQUIRED":
                return {"code":"DECISION_REQUIRED","detail":"dirty context requires save or discard","row_version":current_version}, 409
            if result.outcome == "NOT_FOUND":
                return {"code":"NOT_FOUND","detail":"work context not found"}, 404
            if result.outcome == "INVALID_COMMAND":
                return {"code":"INVALID_ARGUMENT","detail":"unsupported work context command"}, 400
            if not result.exists:
                if current:
                    conn.execute(work_contexts.delete().where(and_(work_contexts.c.id == context_id, work_contexts.c.user_id == user_id)))
                return {"id": context_id, "outcome": result.outcome, "exists": False}, 200
            values = {
                "user_id": user_id,
                "action_code": payload.get("action_code") or (current["action_code"] if current else ""),
                "project_code": payload.get("project_code", current["project_code"] if current else None),
                "resource_type": payload.get("resource_type", current["resource_type"] if current else None),
                "resource_id": payload.get("resource_id", current["resource_id"] if current else None),
                "dirty": result.dirty,
                "draft_payload": None if command.upper() in {"SAVE", "DISCARD"} else payload.get("draft_payload", current["draft_payload"] if current else None),
                "updated_at": now,
                "row_version": result.row_version,
            }
            if not values["action_code"]:
                return {"code":"INVALID_ARGUMENT","detail":"action_code is required"}, 400
            if current:
                conn.execute(work_contexts.update().where(and_(work_contexts.c.id == context_id, work_contexts.c.user_id == user_id, work_contexts.c.row_version == current_version)).values(**values))
            else:
                conn.execute(work_contexts.insert().values(id=context_id, created_at=now, **values))
        item = self.get(context_id, user_id)
        item["outcome"] = result.outcome
        return item, 200


def build_work_context_blueprint(service: WorkContextService, resolve_user_id: Callable[[], int | None]) -> Blueprint:
    blueprint = Blueprint("work_context", __name__, url_prefix="/api/work-contexts")

    @blueprint.get("/<context_id>")
    def get_context(context_id: str):
        user_id = resolve_user_id()
        if user_id is None:
            return jsonify({"code":"UNAUTHORIZED"}), 401
        item = service.get(context_id, user_id)
        if item is None:
            return jsonify({"code":"NOT_FOUND"}), 404
        return jsonify(item)

    @blueprint.post("/<context_id>/<command>")
    def apply_command(context_id: str, command: str):
        user_id = resolve_user_id()
        if user_id is None:
            return jsonify({"code":"UNAUTHORIZED"}), 401
        body = request.get_json(silent=True) or {}
        result, status = service.apply(context_id, user_id, command.replace("-", "_"), body)
        return jsonify(result), status

    return blueprint
