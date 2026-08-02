"""Phase 3 MRS governance: catalog releases, price validity, recipe freezes and audit."""
from __future__ import annotations

import json
from datetime import datetime, timezone
from uuid import uuid4

from flask import Blueprint, jsonify, request
from sqlalchemy import Boolean, Column, DateTime, Integer, MetaData, String, Table, Text, and_, select

from api.mrs_catalog import mrs_analysis_recipes, mrs_catalog_items
from api.mrs_operations import mrs_recipe_versions

metadata = MetaData()
mrs_catalog_releases = Table(
    "mrs_catalog_releases", metadata,
    Column("id", String(100), primary_key=True),
    Column("label", String(300), nullable=False),
    Column("status", String(30), nullable=False),
    Column("snapshot_json", Text, nullable=False),
    Column("created_by", String(100), nullable=False),
    Column("reviewed_by", String(100)),
    Column("review_comment", Text),
    Column("created_at", DateTime(timezone=True), nullable=False),
    Column("updated_at", DateTime(timezone=True), nullable=False),
    Column("row_version", Integer, nullable=False),
)
mrs_item_validity = Table(
    "mrs_item_validity", metadata,
    Column("catalog_item_id", String(100), primary_key=True),
    Column("valid_from", String(30)),
    Column("valid_to", String(30)),
    Column("status", String(30), nullable=False),
    Column("row_version", Integer, nullable=False),
    Column("updated_by", String(100), nullable=False),
    Column("updated_at", DateTime(timezone=True), nullable=False),
)
mrs_recipe_freezes = Table(
    "mrs_recipe_freezes", metadata,
    Column("recipe_id", String(100), primary_key=True),
    Column("version_id", String(100), nullable=False),
    Column("frozen", Boolean, nullable=False),
    Column("reason", Text),
    Column("row_version", Integer, nullable=False),
    Column("updated_by", String(100), nullable=False),
    Column("updated_at", DateTime(timezone=True), nullable=False),
)
mrs_governance_audit = Table(
    "mrs_governance_audit", metadata,
    Column("id", String(100), primary_key=True),
    Column("event_type", String(50), nullable=False),
    Column("resource_type", String(50), nullable=False),
    Column("resource_id", String(100), nullable=False),
    Column("actor_id", String(100), nullable=False),
    Column("payload_json", Text, nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
)


class MRSGovernanceService:
    RELEASE_TRANSITIONS = {
        "DRAFT": {"SUBMIT": "SUBMITTED"},
        "SUBMITTED": {"APPROVE": "APPROVED", "RETURN": "RETURNED"},
        "RETURNED": {"SUBMIT": "SUBMITTED"},
        "APPROVED": {"PUBLISH": "PUBLISHED"},
    }

    def __init__(self, engine):
        self.engine = engine
        metadata.create_all(engine)

    def create_release(self, label: str, actor: str) -> dict:
        now, release_id = datetime.now(timezone.utc), str(uuid4())
        with self.engine.connect() as conn:
            items = [dict(row) for row in conn.execute(select(mrs_catalog_items).order_by(mrs_catalog_items.c.code)).mappings().all()]
        for item in items:
            item["created_at"] = item["created_at"].isoformat(); item["updated_at"] = item["updated_at"].isoformat()
        with self.engine.begin() as conn:
            conn.execute(mrs_catalog_releases.insert().values(id=release_id, label=label or "MRS Catalog Release",
                status="DRAFT", snapshot_json=json.dumps(items, ensure_ascii=False, sort_keys=True), created_by=actor,
                reviewed_by=None, review_comment=None, created_at=now, updated_at=now, row_version=1))
            self._audit(conn, "RELEASE_CREATED", "CATALOG_RELEASE", release_id, actor, {"item_count": len(items)})
        return self.get_release(release_id)

    def get_release(self, release_id: str) -> dict:
        with self.engine.connect() as conn:
            row = conn.execute(select(mrs_catalog_releases).where(mrs_catalog_releases.c.id == release_id)).mappings().first()
        if not row: raise LookupError("MRS catalog release not found")
        result = dict(row); result["snapshot"] = json.loads(row["snapshot_json"])
        result["created_at"] = row["created_at"].isoformat(); result["updated_at"] = row["updated_at"].isoformat()
        result["deep_link"] = f"/app/mrs-governance?release={release_id}"
        return result

    def list_releases(self) -> list[dict]:
        with self.engine.connect() as conn:
            rows = conn.execute(select(mrs_catalog_releases.c.id).order_by(mrs_catalog_releases.c.created_at.desc())).all()
        return [self.get_release(row[0]) for row in rows]

    def transition_release(self, release_id: str, command: str, actor: str, row_version: int, comment: str = "") -> dict:
        command = command.upper(); now = datetime.now(timezone.utc)
        with self.engine.begin() as conn:
            row = conn.execute(select(mrs_catalog_releases).where(mrs_catalog_releases.c.id == release_id)).mappings().first()
            if not row: raise LookupError("MRS catalog release not found")
            if int(row["row_version"]) != int(row_version): raise RuntimeError("CONFLICT")
            next_status = self.RELEASE_TRANSITIONS.get(row["status"], {}).get(command)
            if not next_status: raise ValueError("INVALID_TRANSITION")
            reviewed_by = actor if command in {"APPROVE", "RETURN", "PUBLISH"} else row["reviewed_by"]
            result = conn.execute(mrs_catalog_releases.update().where(and_(mrs_catalog_releases.c.id == release_id,
                mrs_catalog_releases.c.row_version == row_version)).values(status=next_status, reviewed_by=reviewed_by,
                review_comment=comment or row["review_comment"], updated_at=now, row_version=row_version + 1))
            if result.rowcount != 1: raise RuntimeError("CONFLICT")
            self._audit(conn, f"RELEASE_{command}", "CATALOG_RELEASE", release_id, actor,
                        {"from": row["status"], "to": next_status, "comment": comment})
        return self.get_release(release_id)

    def set_validity(self, item_id: str, body: dict, actor: str) -> dict:
        status = str(body.get("status", "ACTIVE")).upper()
        if status not in {"ACTIVE", "SUSPENDED", "EXPIRED"}: raise ValueError("invalid validity status")
        expected, now = int(body.get("row_version", 0)), datetime.now(timezone.utc)
        with self.engine.begin() as conn:
            exists = conn.execute(select(mrs_catalog_items.c.id).where(mrs_catalog_items.c.id == item_id)).first()
            if not exists: raise LookupError("catalog item not found")
            current = conn.execute(select(mrs_item_validity).where(mrs_item_validity.c.catalog_item_id == item_id)).mappings().first()
            values = dict(valid_from=body.get("valid_from"), valid_to=body.get("valid_to"), status=status,
                          updated_by=actor, updated_at=now)
            if current:
                if int(current["row_version"]) != expected: raise RuntimeError("CONFLICT")
                conn.execute(mrs_item_validity.update().where(and_(mrs_item_validity.c.catalog_item_id == item_id,
                    mrs_item_validity.c.row_version == expected)).values(**values, row_version=expected + 1))
            else:
                conn.execute(mrs_item_validity.insert().values(catalog_item_id=item_id, **values, row_version=1))
            self._audit(conn, "ITEM_VALIDITY_SET", "CATALOG_ITEM", item_id, actor, values)
        return self.get_validity(item_id)

    def get_validity(self, item_id: str) -> dict:
        with self.engine.connect() as conn:
            row = conn.execute(select(mrs_item_validity).where(mrs_item_validity.c.catalog_item_id == item_id)).mappings().first()
        if not row: return {"catalog_item_id": item_id, "valid_from": None, "valid_to": None, "status": "ACTIVE", "row_version": 0}
        result = dict(row); result["updated_at"] = row["updated_at"].isoformat(); return result

    def expiry_alerts(self, as_of: str | None = None) -> list[dict]:
        today = as_of or datetime.now(timezone.utc).date().isoformat()
        with self.engine.connect() as conn:
            rows = conn.execute(select(mrs_item_validity, mrs_catalog_items.c.code, mrs_catalog_items.c.name)
                .join(mrs_catalog_items, mrs_catalog_items.c.id == mrs_item_validity.c.catalog_item_id)).mappings().all()
        alerts = []
        for row in rows:
            expired = row["status"] == "EXPIRED" or bool(row["valid_to"] and row["valid_to"] < today)
            if expired or row["status"] == "SUSPENDED":
                alerts.append({"catalog_item_id": row["catalog_item_id"], "code": row["code"], "name": row["name"],
                               "status": "EXPIRED" if expired else row["status"], "valid_to": row["valid_to"]})
        return alerts

    def set_recipe_freeze(self, recipe_id: str, body: dict, actor: str) -> dict:
        version_id, frozen = str(body.get("version_id", "")).strip(), bool(body.get("frozen", True))
        if not version_id: raise ValueError("version_id is required")
        expected, now = int(body.get("row_version", 0)), datetime.now(timezone.utc)
        with self.engine.begin() as conn:
            recipe = conn.execute(select(mrs_analysis_recipes.c.id).where(mrs_analysis_recipes.c.id == recipe_id)).first()
            version = conn.execute(select(mrs_recipe_versions.c.id).where(and_(mrs_recipe_versions.c.id == version_id,
                mrs_recipe_versions.c.recipe_id == recipe_id))).first()
            if not recipe or not version: raise LookupError("recipe or recipe version not found")
            current = conn.execute(select(mrs_recipe_freezes).where(mrs_recipe_freezes.c.recipe_id == recipe_id)).mappings().first()
            values = dict(version_id=version_id, frozen=frozen, reason=body.get("reason"), updated_by=actor, updated_at=now)
            if current:
                if int(current["row_version"]) != expected: raise RuntimeError("CONFLICT")
                conn.execute(mrs_recipe_freezes.update().where(and_(mrs_recipe_freezes.c.recipe_id == recipe_id,
                    mrs_recipe_freezes.c.row_version == expected)).values(**values, row_version=expected + 1))
            else:
                conn.execute(mrs_recipe_freezes.insert().values(recipe_id=recipe_id, **values, row_version=1))
            self._audit(conn, "RECIPE_FREEZE_SET", "ANALYSIS_RECIPE", recipe_id, actor, values)
        return self.get_recipe_freeze(recipe_id)

    def get_recipe_freeze(self, recipe_id: str) -> dict:
        with self.engine.connect() as conn:
            row = conn.execute(select(mrs_recipe_freezes).where(mrs_recipe_freezes.c.recipe_id == recipe_id)).mappings().first()
        if not row: return {"recipe_id": recipe_id, "version_id": None, "frozen": False, "reason": None, "row_version": 0}
        result = dict(row); result["updated_at"] = row["updated_at"].isoformat(); return result

    def audit(self) -> list[dict]:
        with self.engine.connect() as conn:
            rows = conn.execute(select(mrs_governance_audit).order_by(mrs_governance_audit.c.created_at.desc())).mappings().all()
        return [{**dict(row), "payload": json.loads(row["payload_json"]), "created_at": row["created_at"].isoformat()} for row in rows]

    @staticmethod
    def _audit(conn, event_type, resource_type, resource_id, actor, payload):
        conn.execute(mrs_governance_audit.insert().values(id=str(uuid4()), event_type=event_type,
            resource_type=resource_type, resource_id=resource_id, actor_id=actor,
            payload_json=json.dumps(payload, ensure_ascii=False, sort_keys=True, default=str), created_at=datetime.now(timezone.utc)))


def build_mrs_governance_blueprint(service: MRSGovernanceService, resolve_user_id):
    bp = Blueprint("mrs_governance", __name__, url_prefix="/api/mrs")
    def actor():
        value = resolve_user_id()
        if value is None: raise PermissionError("authentication required")
        return str(value)
    @bp.post("/catalog-releases")
    def create_release():
        body=request.get_json(silent=True) or {}; return jsonify(service.create_release(str(body.get("label", "")), actor())),201
    @bp.get("/catalog-releases")
    def list_releases(): return jsonify(service.list_releases())
    @bp.post("/catalog-releases/<release_id>/<command>")
    def transition_release(release_id, command):
        body=request.get_json(silent=True) or {}
        try:return jsonify(service.transition_release(release_id, command, actor(), int(body.get("row_version",0)), str(body.get("comment",""))))
        except LookupError as exc:return jsonify({"code":"NOT_FOUND","detail":str(exc)}),404
        except RuntimeError:return jsonify({"code":"CONFLICT"}),409
        except ValueError as exc:return jsonify({"code":"INVALID_TRANSITION","detail":str(exc)}),400
    @bp.get("/catalog/<item_id>/validity")
    def get_validity(item_id): return jsonify(service.get_validity(item_id))
    @bp.put("/catalog/<item_id>/validity")
    def set_validity(item_id):
        try:return jsonify(service.set_validity(item_id, request.get_json(silent=True) or {}, actor()))
        except LookupError as exc:return jsonify({"code":"NOT_FOUND","detail":str(exc)}),404
        except RuntimeError:return jsonify({"code":"CONFLICT"}),409
        except ValueError as exc:return jsonify({"code":"INVALID_ARGUMENT","detail":str(exc)}),400
    @bp.get("/expiry-alerts")
    def expiry_alerts(): return jsonify(service.expiry_alerts(request.args.get("as_of")))
    @bp.get("/analysis-recipes/<recipe_id>/freeze")
    def get_freeze(recipe_id): return jsonify(service.get_recipe_freeze(recipe_id))
    @bp.put("/analysis-recipes/<recipe_id>/freeze")
    def set_freeze(recipe_id):
        try:return jsonify(service.set_recipe_freeze(recipe_id, request.get_json(silent=True) or {}, actor()))
        except LookupError as exc:return jsonify({"code":"NOT_FOUND","detail":str(exc)}),404
        except RuntimeError:return jsonify({"code":"CONFLICT"}),409
        except ValueError as exc:return jsonify({"code":"INVALID_ARGUMENT","detail":str(exc)}),400
    @bp.get("/governance-audit")
    def audit(): return jsonify(service.audit())
    return bp
