"""Paged and filtered MRS governance queries shared with the canonical Web API."""
from __future__ import annotations

import json

from flask import Blueprint, jsonify, request
from sqlalchemy import and_, func, select

from api.mrs_governance import (
    MRSGovernanceService as BaseMRSGovernanceService,
    mrs_catalog_releases,
    mrs_governance_audit,
)

_MAX_PAGE_SIZE = 200
_VALID_RELEASE_STATUSES = {"DRAFT", "SUBMITTED", "RETURNED", "APPROVED", "PUBLISHED"}


def _page_args(limit: int | str | None, offset: int | str | None) -> tuple[int, int]:
    try:
        page_limit = int(limit or 50)
    except (TypeError, ValueError):
        page_limit = 50
    try:
        page_offset = int(offset or 0)
    except (TypeError, ValueError):
        page_offset = 0
    page_limit = 50 if page_limit <= 0 else min(page_limit, _MAX_PAGE_SIZE)
    page_offset = max(page_offset, 0)
    return page_limit, page_offset


class MRSGovernanceService(BaseMRSGovernanceService):
    """Adds deterministic filtering and bounded paging without changing mutations."""

    @staticmethod
    def _release_status(status: str | None) -> str:
        normalized = str(status or "").strip().upper()
        if normalized and normalized not in _VALID_RELEASE_STATUSES:
            raise ValueError("invalid MRS release status")
        return normalized

    def query_releases(self, status: str | None = None, limit: int = 50, offset: int = 0) -> dict:
        normalized = self._release_status(status)
        limit, offset = _page_args(limit, offset)
        predicate = mrs_catalog_releases.c.status == normalized if normalized else None
        count_stmt = select(func.count()).select_from(mrs_catalog_releases)
        ids_stmt = select(mrs_catalog_releases.c.id)
        if predicate is not None:
            count_stmt = count_stmt.where(predicate)
            ids_stmt = ids_stmt.where(predicate)
        ids_stmt = ids_stmt.order_by(
            mrs_catalog_releases.c.created_at.desc(), mrs_catalog_releases.c.id.desc()
        ).limit(limit).offset(offset)
        with self.engine.connect() as conn:
            total = int(conn.execute(count_stmt).scalar_one())
            ids = [row[0] for row in conn.execute(ids_stmt)]
        return {
            "items": [self.get_release(release_id) for release_id in ids],
            "total": total,
            "limit": limit,
            "offset": offset,
        }

    def query_audit(
        self,
        resource_type: str | None = None,
        resource_id: str | None = None,
        event_type: str | None = None,
        limit: int = 50,
        offset: int = 0,
    ) -> dict:
        resource_type = str(resource_type or "").strip().upper()
        resource_id = str(resource_id or "").strip()
        event_type = str(event_type or "").strip().upper()
        limit, offset = _page_args(limit, offset)
        predicates = []
        if resource_type:
            predicates.append(mrs_governance_audit.c.resource_type == resource_type)
        if resource_id:
            predicates.append(mrs_governance_audit.c.resource_id == resource_id)
        if event_type:
            predicates.append(mrs_governance_audit.c.event_type == event_type)
        count_stmt = select(func.count()).select_from(mrs_governance_audit)
        rows_stmt = select(mrs_governance_audit)
        if predicates:
            condition = and_(*predicates)
            count_stmt = count_stmt.where(condition)
            rows_stmt = rows_stmt.where(condition)
        rows_stmt = rows_stmt.order_by(
            mrs_governance_audit.c.created_at.desc(), mrs_governance_audit.c.id.desc()
        ).limit(limit).offset(offset)
        with self.engine.connect() as conn:
            total = int(conn.execute(count_stmt).scalar_one())
            rows = conn.execute(rows_stmt).mappings().all()
        items = [
            {
                **dict(row),
                "payload": json.loads(row["payload_json"]),
                "created_at": row["created_at"].isoformat(),
            }
            for row in rows
        ]
        return {"items": items, "total": total, "limit": limit, "offset": offset}


def build_mrs_governance_blueprint(service: MRSGovernanceService, resolve_user_id):
    bp = Blueprint("mrs_governance", __name__, url_prefix="/api/mrs")

    def actor() -> str:
        value = resolve_user_id()
        if value is None:
            raise PermissionError("authentication required")
        return str(value)

    @bp.post("/catalog-releases")
    def create_release():
        body = request.get_json(silent=True) or {}
        return jsonify(service.create_release(str(body.get("label", "")), actor())), 201

    @bp.get("/catalog-releases")
    def list_releases():
        try:
            return jsonify(service.query_releases(
                request.args.get("status"), request.args.get("limit"), request.args.get("offset")
            ))
        except ValueError as exc:
            return jsonify({"code": "INVALID_ARGUMENT", "detail": str(exc)}), 400

    @bp.post("/catalog-releases/<release_id>/<command>")
    def transition_release(release_id, command):
        body = request.get_json(silent=True) or {}
        try:
            return jsonify(service.transition_release(
                release_id, command, actor(), int(body.get("row_version", 0)), str(body.get("comment", ""))
            ))
        except LookupError as exc:
            return jsonify({"code": "NOT_FOUND", "detail": str(exc)}), 404
        except RuntimeError:
            return jsonify({"code": "CONFLICT"}), 409
        except ValueError as exc:
            return jsonify({"code": "INVALID_TRANSITION", "detail": str(exc)}), 400

    @bp.get("/catalog/<item_id>/validity")
    def get_validity(item_id):
        return jsonify(service.get_validity(item_id))

    @bp.put("/catalog/<item_id>/validity")
    def set_validity(item_id):
        try:
            return jsonify(service.set_validity(item_id, request.get_json(silent=True) or {}, actor()))
        except LookupError as exc:
            return jsonify({"code": "NOT_FOUND", "detail": str(exc)}), 404
        except RuntimeError:
            return jsonify({"code": "CONFLICT"}), 409
        except ValueError as exc:
            return jsonify({"code": "INVALID_ARGUMENT", "detail": str(exc)}), 400

    @bp.get("/expiry-alerts")
    def expiry_alerts():
        return jsonify(service.expiry_alerts(request.args.get("as_of")))

    @bp.get("/analysis-recipes/<recipe_id>/freeze")
    def get_freeze(recipe_id):
        return jsonify(service.get_recipe_freeze(recipe_id))

    @bp.put("/analysis-recipes/<recipe_id>/freeze")
    def set_freeze(recipe_id):
        try:
            return jsonify(service.set_recipe_freeze(recipe_id, request.get_json(silent=True) or {}, actor()))
        except LookupError as exc:
            return jsonify({"code": "NOT_FOUND", "detail": str(exc)}), 404
        except RuntimeError:
            return jsonify({"code": "CONFLICT"}), 409
        except ValueError as exc:
            return jsonify({"code": "INVALID_ARGUMENT", "detail": str(exc)}), 400

    @bp.get("/governance-audit")
    def audit():
        return jsonify(service.query_audit(
            request.args.get("resource_type"), request.args.get("resource_id"),
            request.args.get("event_type"), request.args.get("limit"), request.args.get("offset")
        ))

    return bp
