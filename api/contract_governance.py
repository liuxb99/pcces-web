"""Phase 5 contract versioning, review, approval and immutable governance."""
from __future__ import annotations

import json
from datetime import datetime, timezone
from uuid import uuid4

from flask import Blueprint, jsonify, request
from sqlalchemy import Column, DateTime, Integer, MetaData, String, Table, Text, and_, select, update

from api.contract_changes import ContractChangeService
from api.contract_core import contracts_v2, contract_items_v2

metadata = MetaData()
contract_versions_v2 = Table(
    "contract_versions_v2", metadata,
    Column("id", String(100), primary_key=True),
    Column("contract_id", String(100), nullable=False, index=True),
    Column("version_no", Integer, nullable=False),
    Column("status", String(30), nullable=False),
    Column("snapshot_json", Text, nullable=False),
    Column("note", Text),
    Column("created_by", String(100), nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
    Column("approved_by", String(100)),
    Column("approved_at", DateTime(timezone=True)),
    Column("row_version", Integer, nullable=False, default=1),
)

TRANSITIONS = {
    "DRAFT": {"SUBMITTED"},
    "SUBMITTED": {"DRAFT", "APPROVED"},
    "APPROVED": {"LOCKED"},
    "LOCKED": set(),
}
READ_ONLY = {"APPROVED", "LOCKED"}


class ContractGovernanceService:
    def __init__(self, engine):
        self.engine = engine
        metadata.create_all(engine)
        self.changes = ContractChangeService(engine)

    def _snapshot(self, conn, contract_id):
        contract = conn.execute(select(contracts_v2).where(contracts_v2.c.id == contract_id)).mappings().first()
        if not contract:
            raise LookupError("contract not found")
        items = conn.execute(select(contract_items_v2).where(contract_items_v2.c.contract_id == contract_id).order_by(contract_items_v2.c.sort_order)).mappings().all()
        return {
            "contract": {k: (str(v) if v is not None else None) for k, v in contract.items()},
            "items": [{k: (str(v) if v is not None else None) for k, v in item.items()} for item in items],
        }

    def create_version(self, contract_id: str, body: dict, actor: str) -> dict:
        expected = int(body.get("row_version", 0))
        with self.engine.begin() as conn:
            contract = conn.execute(select(contracts_v2).where(contracts_v2.c.id == contract_id)).mappings().first()
            if not contract:
                raise LookupError("contract not found")
            if int(contract["row_version"]) != expected:
                raise RuntimeError("row version conflict")
            if str(contract["status"]).upper() in READ_ONLY:
                raise PermissionError("approved or locked contract cannot be overwritten; create a formal change issue")
            latest = conn.execute(select(contract_versions_v2.c.version_no).where(contract_versions_v2.c.contract_id == contract_id).order_by(contract_versions_v2.c.version_no.desc())).first()
            version_no = (latest[0] if latest else 0) + 1
            version_id = str(uuid4()); now = datetime.now(timezone.utc)
            snapshot = self._snapshot(conn, contract_id)
            conn.execute(contract_versions_v2.insert().values(id=version_id, contract_id=contract_id, version_no=version_no, status="DRAFT", snapshot_json=json.dumps(snapshot, ensure_ascii=False, sort_keys=True), note=body.get("note"), created_by=actor, created_at=now, row_version=1))
        return self.get_version(version_id)

    def transition(self, version_id: str, body: dict, actor: str) -> dict:
        target = str(body.get("status", "")).upper()
        expected = int(body.get("row_version", 0))
        now = datetime.now(timezone.utc)
        with self.engine.begin() as conn:
            row = conn.execute(select(contract_versions_v2).where(contract_versions_v2.c.id == version_id)).mappings().first()
            if not row:
                raise LookupError("contract version not found")
            current = str(row["status"]).upper()
            if int(row["row_version"]) != expected:
                raise RuntimeError("row version conflict")
            if target not in TRANSITIONS.get(current, set()):
                raise ValueError(f"invalid contract version transition {current} -> {target}")
            values = {"status": target, "row_version": expected + 1}
            if target == "APPROVED":
                values.update({"approved_by": actor, "approved_at": now})
            result = conn.execute(update(contract_versions_v2).where(and_(contract_versions_v2.c.id == version_id, contract_versions_v2.c.row_version == expected)).values(**values))
            if result.rowcount != 1:
                raise RuntimeError("row version conflict")
            contract_status = "APPROVED" if target == "APPROVED" else ("LOCKED" if target == "LOCKED" else None)
            if contract_status:
                conn.execute(update(contracts_v2).where(contracts_v2.c.id == row["contract_id"]).values(status=contract_status, updated_at=now, row_version=contracts_v2.c.row_version + 1))
        return self.get_version(version_id)

    def get_version(self, version_id: str) -> dict:
        with self.engine.connect() as conn:
            row = conn.execute(select(contract_versions_v2).where(contract_versions_v2.c.id == version_id)).mappings().first()
        if not row:
            raise LookupError("contract version not found")
        return {"id": row["id"], "contract_id": row["contract_id"], "version_no": row["version_no"], "status": row["status"], "snapshot": json.loads(row["snapshot_json"]), "note": row["note"], "created_by": row["created_by"], "created_at": row["created_at"].isoformat(), "approved_by": row["approved_by"], "approved_at": row["approved_at"].isoformat() if row["approved_at"] else None, "row_version": row["row_version"], "deep_link": f"/app/contracts/{row['contract_id']}?version={row['id']}"}


def build_contract_governance_blueprint(service, resolve_user_id):
    bp = Blueprint("contract_governance", __name__, url_prefix="/api/contracts")

    @bp.post("/<contract_id>/versions")
    def create_version(contract_id):
        actor = resolve_user_id()
        if actor is None: return jsonify({"code": "UNAUTHORIZED"}), 401
        try: return jsonify(service.create_version(contract_id, request.get_json(silent=True) or {}, str(actor))), 201
        except LookupError as exc: return jsonify({"code": "NOT_FOUND", "detail": str(exc)}), 404
        except PermissionError as exc: return jsonify({"code": "READ_ONLY", "detail": str(exc)}), 409
        except RuntimeError as exc: return jsonify({"code": "CONFLICT", "detail": str(exc)}), 409

    @bp.get("/versions/<version_id>")
    def get_version(version_id):
        try: return jsonify(service.get_version(version_id))
        except LookupError as exc: return jsonify({"code": "NOT_FOUND", "detail": str(exc)}), 404

    @bp.post("/versions/<version_id>/transition")
    def transition(version_id):
        actor = resolve_user_id()
        if actor is None: return jsonify({"code": "UNAUTHORIZED"}), 401
        try: return jsonify(service.transition(version_id, request.get_json(silent=True) or {}, str(actor)))
        except LookupError as exc: return jsonify({"code": "NOT_FOUND", "detail": str(exc)}), 404
        except RuntimeError as exc: return jsonify({"code": "CONFLICT", "detail": str(exc)}), 409
        except ValueError as exc: return jsonify({"code": "INVALID_TRANSITION", "detail": str(exc)}), 400

    @bp.post("/<contract_id>/changes")
    def create_change(contract_id):
        actor = resolve_user_id()
        if actor is None: return jsonify({"code": "UNAUTHORIZED"}), 401
        try: return jsonify(service.changes.create(contract_id, request.get_json(silent=True) or {}, str(actor))), 201
        except LookupError as exc: return jsonify({"code": "NOT_FOUND", "detail": str(exc)}), 404
        except PermissionError as exc: return jsonify({"code": "READ_ONLY", "detail": str(exc)}), 409
        except RuntimeError as exc: return jsonify({"code": "CONFLICT", "detail": str(exc)}), 409
        except ValueError as exc: return jsonify({"code": "INVALID_ARGUMENT", "detail": str(exc)}), 400

    @bp.get("/changes/<change_id>")
    def get_change(change_id):
        try: return jsonify(service.changes.get(change_id))
        except LookupError as exc: return jsonify({"code": "NOT_FOUND", "detail": str(exc)}), 404

    return bp
