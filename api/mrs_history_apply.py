"""Phase 3 historical price/rate application with optimistic locking and traceability."""
from __future__ import annotations

import json
from datetime import datetime, timezone
from uuid import uuid4

from flask import Blueprint, jsonify, request
from sqlalchemy import and_, select

from api.decimal_math import quantize
from api.mrs_catalog import mrs_analysis_components, mrs_analysis_recipes, mrs_catalog_items, mrs_price_history
from api.mrs_operations import mrs_recipe_versions


class MRSHistoryApplyService:
    def __init__(self, engine):
        self.engine = engine

    def apply_price(self, item_id: str, history_id: str, row_version: int, actor: str) -> dict:
        now = datetime.now(timezone.utc)
        with self.engine.begin() as conn:
            item = conn.execute(select(mrs_catalog_items).where(mrs_catalog_items.c.id == item_id)).mappings().first()
            if not item:
                raise LookupError("catalog item not found")
            if int(item["row_version"]) != int(row_version):
                raise RuntimeError("CONFLICT")
            historical = conn.execute(select(mrs_price_history).where(and_(
                mrs_price_history.c.id == history_id,
                mrs_price_history.c.catalog_item_id == item_id,
            ))).mappings().first()
            if not historical:
                raise LookupError("historical price not found")
            applied_price = quantize(str(historical["new_price"]), int(item["price_scale"]))
            result = conn.execute(mrs_catalog_items.update().where(and_(
                mrs_catalog_items.c.id == item_id,
                mrs_catalog_items.c.row_version == row_version,
            )).values(current_price=applied_price, source=f"HISTORY:{history_id}",
                      updated_at=now, row_version=row_version + 1))
            if result.rowcount != 1:
                raise RuntimeError("CONFLICT")
            apply_event_id = str(uuid4())
            conn.execute(mrs_price_history.insert().values(
                id=apply_event_id, catalog_item_id=item_id,
                old_price=str(item["current_price"]), new_price=applied_price,
                source=f"HISTORY_APPLY:{history_id}",
                effective_date=historical["effective_date"], created_by=actor, created_at=now,
            ))
        return {
            "catalog_item_id": item_id,
            "history_id": history_id,
            "apply_event_id": apply_event_id,
            "old_price": quantize(str(item["current_price"]), int(item["price_scale"])),
            "new_price": applied_price,
            "source": f"HISTORY:{history_id}",
            "effective_date": historical["effective_date"],
            "row_version": row_version + 1,
            "deep_link": f"/app/mrs-operations?item={item_id}&history={history_id}",
        }

    def apply_rates(self, recipe_id: str, version_id: str, row_version: int, actor: str) -> dict:
        now = datetime.now(timezone.utc)
        with self.engine.begin() as conn:
            recipe = conn.execute(select(mrs_analysis_recipes).where(mrs_analysis_recipes.c.id == recipe_id)).mappings().first()
            if not recipe:
                raise LookupError("recipe not found")
            if int(recipe["row_version"]) != int(row_version):
                raise RuntimeError("CONFLICT")
            version = conn.execute(select(mrs_recipe_versions).where(and_(
                mrs_recipe_versions.c.id == version_id,
                mrs_recipe_versions.c.recipe_id == recipe_id,
            ))).mappings().first()
            if not version:
                raise LookupError("recipe version not found")
            snapshot = json.loads(version["snapshot_json"])
            components = snapshot.get("components") or []
            conn.execute(mrs_analysis_components.delete().where(mrs_analysis_components.c.recipe_id == recipe_id))
            applied = []
            for index, component in enumerate(components):
                catalog_item_id = str(component.get("catalog_item_id", ""))
                exists = conn.execute(select(mrs_catalog_items.c.id).where(mrs_catalog_items.c.id == catalog_item_id)).first()
                if not catalog_item_id or not exists:
                    raise LookupError("historical component catalog item not found")
                quantity = quantize(str(component.get("quantity", "0")), 4)
                conn.execute(mrs_analysis_components.insert().values(
                    id=str(uuid4()), recipe_id=recipe_id, catalog_item_id=catalog_item_id,
                    quantity=quantity, quantity_scale=4, sequence=index,
                ))
                applied.append({"catalog_item_id": catalog_item_id, "quantity": quantity})
            result = conn.execute(mrs_analysis_recipes.update().where(and_(
                mrs_analysis_recipes.c.id == recipe_id,
                mrs_analysis_recipes.c.row_version == row_version,
            )).values(updated_at=now, row_version=row_version + 1))
            if result.rowcount != 1:
                raise RuntimeError("CONFLICT")
        return {
            "recipe_id": recipe_id, "version_id": version_id, "actor": actor,
            "applied_components": applied, "component_count": len(applied),
            "row_version": row_version + 1,
            "deep_link": f"/app/mrs-operations?recipe={recipe_id}&version={version_id}&applied=1",
        }


def build_mrs_history_apply_blueprint(service: MRSHistoryApplyService, resolve_user_id):
    bp = Blueprint("mrs_history_apply", __name__, url_prefix="/api/mrs")

    @bp.post("/catalog/<item_id>/price-history/<history_id>/apply")
    def apply_price(item_id: str, history_id: str):
        actor = resolve_user_id()
        if actor is None:
            return jsonify({"code": "UNAUTHORIZED"}), 401
        body = request.get_json(silent=True) or {}
        try:
            return jsonify(service.apply_price(item_id, history_id, int(body.get("row_version", -1)), str(actor)))
        except LookupError as exc:
            return jsonify({"code": "NOT_FOUND", "detail": str(exc)}), 404
        except RuntimeError:
            return jsonify({"code": "CONFLICT", "detail": "stale catalog row_version"}), 409
        except (TypeError, ValueError) as exc:
            return jsonify({"code": "INVALID_ARGUMENT", "detail": str(exc)}), 400

    @bp.post("/analysis-recipes/<recipe_id>/versions/<version_id>/apply-rates")
    def apply_rates(recipe_id: str, version_id: str):
        actor = resolve_user_id()
        if actor is None:
            return jsonify({"code": "UNAUTHORIZED"}), 401
        body = request.get_json(silent=True) or {}
        try:
            return jsonify(service.apply_rates(recipe_id, version_id, int(body.get("row_version", -1)), str(actor)))
        except LookupError as exc:
            return jsonify({"code": "NOT_FOUND", "detail": str(exc)}), 404
        except RuntimeError:
            return jsonify({"code": "CONFLICT", "detail": "stale recipe row_version"}), 409
        except (TypeError, ValueError, KeyError, json.JSONDecodeError) as exc:
            return jsonify({"code": "INVALID_ARGUMENT", "detail": str(exc)}), 400

    return bp
