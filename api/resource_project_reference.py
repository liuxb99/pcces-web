"""Phase 3 parent/historical project resource references with immutable provenance."""
from __future__ import annotations

import json
from datetime import datetime, timezone
from uuid import uuid4

from flask import Blueprint, jsonify, request
from sqlalchemy import Column, DateTime, MetaData, String, Table, Text, and_, select

from api.resource_decimal import resources_decimal

metadata = MetaData()
resource_project_references = Table(
    "resource_project_references", metadata,
    Column("id", String(100), primary_key=True),
    Column("target_project_code", String(100), nullable=False, index=True),
    Column("source_project_code", String(100), nullable=False, index=True),
    Column("source_resource_id", String(100), nullable=False),
    Column("target_resource_id", String(100), nullable=False, index=True),
    Column("reference_type", String(20), nullable=False),
    Column("snapshot_json", Text, nullable=False),
    Column("created_by", String(100), nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
)


class ResourceProjectReferenceService:
    TYPES = {"PARENT", "HISTORICAL"}

    def __init__(self, engine):
        self.engine = engine
        metadata.create_all(engine)

    def import_reference(self, target_project_code: str, source_project_code: str,
                         source_resource_id: str, target_resource_id: str,
                         reference_type: str, actor: str) -> dict:
        kind = reference_type.strip().upper()
        if kind not in self.TYPES:
            raise ValueError("reference_type must be PARENT or HISTORICAL")
        if not all(x.strip() for x in (target_project_code, source_project_code, source_resource_id, target_resource_id)):
            raise ValueError("project and resource identifiers are required")
        now = datetime.now(timezone.utc)
        with self.engine.begin() as conn:
            source = conn.execute(select(resources_decimal).where(resources_decimal.c.id == source_resource_id)).mappings().first()
            if not source:
                raise LookupError("source resource not found")
            existing = conn.execute(select(resources_decimal.c.id).where(resources_decimal.c.id == target_resource_id)).first()
            if existing:
                raise RuntimeError("TARGET_EXISTS")
            snapshot = {
                "id": source["id"], "code": source["code"], "name": source["name"],
                "unit": source["unit"], "unit_price": str(source["unit_price"]),
                "price_scale": int(source["price_scale"]), "row_version": int(source["row_version"]),
            }
            conn.execute(resources_decimal.insert().values(
                id=target_resource_id, code=f"{target_project_code}:{source['code']}", name=source["name"],
                unit=source["unit"], unit_price=source["unit_price"], price_scale=source["price_scale"],
                created_at=now, updated_at=now, row_version=1,
            ))
            reference_id = str(uuid4())
            conn.execute(resource_project_references.insert().values(
                id=reference_id, target_project_code=target_project_code,
                source_project_code=source_project_code, source_resource_id=source_resource_id,
                target_resource_id=target_resource_id, reference_type=kind,
                snapshot_json=json.dumps(snapshot, ensure_ascii=False, sort_keys=True),
                created_by=actor, created_at=now,
            ))
        return self.get(reference_id)

    def get(self, reference_id: str) -> dict:
        with self.engine.connect() as conn:
            row = conn.execute(select(resource_project_references).where(resource_project_references.c.id == reference_id)).mappings().first()
        if not row:
            raise LookupError("resource project reference not found")
        result = dict(row)
        result["snapshot"] = json.loads(row["snapshot_json"])
        result["created_at"] = row["created_at"].isoformat()
        result["deep_link"] = f"/app/project-resources?project={row['target_project_code']}&resource={row['target_resource_id']}"
        return result

    def list_target(self, target_project_code: str) -> list[dict]:
        with self.engine.connect() as conn:
            ids = conn.execute(select(resource_project_references.c.id).where(
                resource_project_references.c.target_project_code == target_project_code
            ).order_by(resource_project_references.c.created_at.desc())).all()
        return [self.get(row[0]) for row in ids]


def build_resource_project_reference_blueprint(service, resolve_user_id):
    bp = Blueprint("resource_project_reference", __name__, url_prefix="/api/decimal-resources")

    @bp.post("/projects/<target_project_code>/references")
    def create_reference(target_project_code: str):
        actor = resolve_user_id()
        if actor is None: return jsonify({"code": "UNAUTHORIZED"}), 401
        body = request.get_json(silent=True) or {}
        try:
            result = service.import_reference(target_project_code, str(body.get("source_project_code", "")),
                str(body.get("source_resource_id", "")), str(body.get("target_resource_id", "")),
                str(body.get("reference_type", "")), str(actor))
            return jsonify(result), 201
        except LookupError as exc: return jsonify({"code": "NOT_FOUND", "detail": str(exc)}), 404
        except RuntimeError: return jsonify({"code": "CONFLICT", "detail": "target resource already exists"}), 409
        except ValueError as exc: return jsonify({"code": "INVALID_ARGUMENT", "detail": str(exc)}), 400

    @bp.get("/projects/<target_project_code>/references")
    def list_references(target_project_code: str):
        if resolve_user_id() is None: return jsonify({"code": "UNAUTHORIZED"}), 401
        return jsonify(service.list_target(target_project_code))

    return bp
