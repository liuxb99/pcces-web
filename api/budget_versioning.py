"""Budget version snapshots, locking, diff and restore for the Decimal Budget Core."""
from __future__ import annotations

import json
from datetime import datetime, timezone
from uuid import uuid4

from flask import Blueprint, jsonify, request
from sqlalchemy import Boolean, Column, DateTime, MetaData, String, Table, Text, and_, delete, select

from api.budget_decimal import budget_items_decimal

metadata = MetaData()
budget_versions = Table(
    "budget_versions", metadata,
    Column("id", String(100), primary_key=True),
    Column("project_code", String(100), nullable=False, index=True),
    Column("label", String(300), nullable=False),
    Column("status", String(30), nullable=False),
    Column("snapshot_json", Text, nullable=False),
    Column("created_by", String(100), nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
)
budget_project_locks = Table(
    "budget_project_locks", metadata,
    Column("project_code", String(100), primary_key=True),
    Column("locked", Boolean, nullable=False),
    Column("reason", String(500)),
    Column("locked_by", String(100)),
    Column("updated_at", DateTime(timezone=True), nullable=False),
)


class BudgetVersionService:
    def __init__(self, engine):
        self.engine = engine
        metadata.create_all(engine)

    def lock_state(self, project_code: str) -> dict:
        with self.engine.connect() as conn:
            row = conn.execute(select(budget_project_locks).where(budget_project_locks.c.project_code == project_code)).mappings().first()
        if not row:
            return {"project_code": project_code, "locked": False, "reason": None, "locked_by": None}
        return {"project_code": row["project_code"], "locked": bool(row["locked"]), "reason": row["reason"], "locked_by": row["locked_by"], "updated_at": row["updated_at"].isoformat()}

    def set_lock(self, project_code: str, locked: bool, actor_id: str, reason: str | None = None) -> dict:
        now = datetime.now(timezone.utc)
        with self.engine.begin() as conn:
            current = conn.execute(select(budget_project_locks).where(budget_project_locks.c.project_code == project_code)).first()
            values = {"locked": bool(locked), "reason": reason, "locked_by": actor_id if locked else None, "updated_at": now}
            if current:
                conn.execute(budget_project_locks.update().where(budget_project_locks.c.project_code == project_code).values(**values))
            else:
                conn.execute(budget_project_locks.insert().values(project_code=project_code, **values))
        return self.lock_state(project_code)

    def assert_writable(self, project_code: str) -> None:
        if self.lock_state(project_code)["locked"]:
            raise PermissionError("budget project is locked")

    def create_version(self, project_code: str, label: str, actor_id: str, status: str = "DRAFT") -> dict:
        with self.engine.connect() as conn:
            rows = conn.execute(select(budget_items_decimal).where(budget_items_decimal.c.project_code == project_code).order_by(budget_items_decimal.c.id)).mappings().all()
        snapshot = [self._row_dict(row) for row in rows]
        version_id = str(uuid4())
        now = datetime.now(timezone.utc)
        with self.engine.begin() as conn:
            conn.execute(budget_versions.insert().values(id=version_id, project_code=project_code, label=label or version_id, status=status, snapshot_json=json.dumps(snapshot, ensure_ascii=False, sort_keys=True), created_by=actor_id, created_at=now))
        return self.get(version_id)

    def get(self, version_id: str) -> dict | None:
        with self.engine.connect() as conn:
            row = conn.execute(select(budget_versions).where(budget_versions.c.id == version_id)).mappings().first()
        if not row: return None
        return {"id": row["id"], "project_code": row["project_code"], "label": row["label"], "status": row["status"], "snapshot": json.loads(row["snapshot_json"]), "created_by": row["created_by"], "created_at": row["created_at"].isoformat(), "deep_link": f"/app/projects/by-code/{row['project_code']}/budget-versions?version={row['id']}"}

    def list_project(self, project_code: str) -> list[dict]:
        with self.engine.connect() as conn:
            ids = [row[0] for row in conn.execute(select(budget_versions.c.id).where(budget_versions.c.project_code == project_code).order_by(budget_versions.c.created_at.desc()))]
        return [self.get(version_id) for version_id in ids]

    def diff(self, left_id: str, right_id: str) -> dict:
        left, right = self.get(left_id), self.get(right_id)
        if not left or not right: raise ValueError("version not found")
        if left["project_code"] != right["project_code"]: raise ValueError("versions belong to different projects")
        lmap = {row["id"]: row for row in left["snapshot"]}; rmap = {row["id"]: row for row in right["snapshot"]}
        added = [rmap[k] for k in sorted(rmap.keys() - lmap.keys())]
        removed = [lmap[k] for k in sorted(lmap.keys() - rmap.keys())]
        changed = []
        for key in sorted(lmap.keys() & rmap.keys()):
            if lmap[key] != rmap[key]: changed.append({"id": key, "before": lmap[key], "after": rmap[key]})
        return {"project_code": left["project_code"], "left_version": left_id, "right_version": right_id, "added": added, "removed": removed, "changed": changed}

    def restore(self, version_id: str, actor_id: str) -> dict:
        version = self.get(version_id)
        if not version: raise ValueError("version not found")
        self.assert_writable(version["project_code"])
        with self.engine.begin() as conn:
            conn.execute(delete(budget_items_decimal).where(budget_items_decimal.c.project_code == version["project_code"]))
            for row in version["snapshot"]:
                values = dict(row)
                values.pop("created_at", None); values.pop("updated_at", None)
                now = datetime.now(timezone.utc)
                values["created_at"] = now; values["updated_at"] = now
                values["row_version"] = int(values.get("row_version", 0)) + 1
                conn.execute(budget_items_decimal.insert().values(**values))
        restored = self.create_version(version["project_code"], f"RESTORE:{version['label']}", actor_id, "RESTORED")
        return {"restored_from": version_id, "new_version": restored}

    @staticmethod
    def _row_dict(row) -> dict:
        result = dict(row)
        for key in ("quantity", "unit_price", "amount"): result[key] = str(result[key])
        for key in ("created_at", "updated_at"):
            value = result.get(key); result[key] = value.isoformat() if value else None
        return result


def build_budget_version_blueprint(service: BudgetVersionService, resolve_user_id):
    bp = Blueprint("budget_versions", __name__, url_prefix="/api/decimal-budget")
    def actor():
        value = resolve_user_id()
        if value is None: raise PermissionError("authentication required")
        return str(value)

    @bp.get("/projects/<project_code>/versions")
    def versions(project_code): return jsonify(service.list_project(project_code))
    @bp.post("/projects/<project_code>/versions")
    def create(project_code):
        body=request.get_json(silent=True) or {}
        return jsonify(service.create_version(project_code,str(body.get("label") or "Snapshot"),actor(),str(body.get("status") or "DRAFT"))),201
    @bp.get("/versions/<version_id>")
    def get_version(version_id):
        row=service.get(version_id); return (jsonify(row),200) if row else (jsonify({"code":"NOT_FOUND"}),404)
    @bp.get("/versions/<left_id>/diff/<right_id>")
    def diff(left_id,right_id):
        try:return jsonify(service.diff(left_id,right_id))
        except ValueError as exc:return jsonify({"code":"INVALID_ARGUMENT","detail":str(exc)}),400
    @bp.post("/versions/<version_id>/restore")
    def restore(version_id):
        try:return jsonify(service.restore(version_id,actor()))
        except PermissionError as exc:return jsonify({"code":"LOCKED","detail":str(exc)}),423
        except ValueError as exc:return jsonify({"code":"INVALID_ARGUMENT","detail":str(exc)}),400
    @bp.get("/projects/<project_code>/lock")
    def get_lock(project_code): return jsonify(service.lock_state(project_code))
    @bp.put("/projects/<project_code>/lock")
    def set_lock(project_code):
        body=request.get_json(silent=True) or {}
        return jsonify(service.set_lock(project_code,bool(body.get("locked")),actor(),body.get("reason")))
    return bp
