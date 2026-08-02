"""Phase 3 project-resource replacement and atomic batch price updates."""
from __future__ import annotations

import json
from datetime import datetime, timezone
from decimal import Decimal
from uuid import uuid4

from flask import Blueprint, jsonify, request
from sqlalchemy import and_, select

from api.budget_decimal import budget_items_decimal
from api.decimal_math import multiply, quantize
from api.resource_budget_lineage import resource_budget_links, resource_price_lineage
from api.resource_decimal import resources_decimal


class ResourceOperationsService:
    def __init__(self, engine):
        self.engine = engine

    def replace(self, project_code: str, source_id: str, target_id: str, actor_id: str = "") -> dict:
        if not project_code or not source_id or not target_id or source_id == target_id:
            raise ValueError("project_code and distinct source/target resources are required")
        now = datetime.now(timezone.utc)
        moved = skipped = 0
        with self.engine.begin() as conn:
            source = conn.execute(select(resources_decimal).where(resources_decimal.c.id == source_id)).mappings().first()
            target = conn.execute(select(resources_decimal).where(resources_decimal.c.id == target_id)).mappings().first()
            if not source or not target:
                raise LookupError("source or target resource not found")
            links = conn.execute(select(resource_budget_links).where(and_(
                resource_budget_links.c.project_code == project_code,
                resource_budget_links.c.resource_id == source_id,
            ))).mappings().all()
            for link in links:
                new_id = f"{project_code}:{target_id}:{link['budget_item_id']}"
                exists = conn.execute(select(resource_budget_links.c.id).where(resource_budget_links.c.id == new_id)).first()
                if exists:
                    skipped += 1
                else:
                    conn.execute(resource_budget_links.insert().values(
                        id=new_id, project_code=project_code, resource_id=target_id,
                        budget_item_id=link["budget_item_id"], created_at=now,
                    ))
                    moved += 1
            conn.execute(resource_budget_links.delete().where(and_(
                resource_budget_links.c.project_code == project_code,
                resource_budget_links.c.resource_id == source_id,
            )))
        return {"project_code": project_code, "source_resource_id": source_id,
                "target_resource_id": target_id, "moved_links": moved,
                "deduplicated_links": skipped, "actor_id": actor_id}

    def batch_prices(self, updates: list[dict], trigger: str = "BATCH_RESOURCE_PRICE_UPDATE") -> dict:
        if not updates:
            raise ValueError("updates are required")
        now = datetime.now(timezone.utc)
        changed, lineage = [], []
        with self.engine.begin() as conn:
            prepared = []
            seen = set()
            for update in updates:
                resource_id = str(update.get("resource_id", "")).strip()
                if not resource_id or resource_id in seen:
                    raise ValueError("resource_id must be present and unique")
                seen.add(resource_id)
                row = conn.execute(select(resources_decimal).where(resources_decimal.c.id == resource_id)).mappings().first()
                if not row:
                    raise LookupError(f"resource not found: {resource_id}")
                expected = int(update.get("row_version", -1))
                if expected != int(row["row_version"]):
                    raise RuntimeError(f"CONFLICT:{resource_id}:{row['row_version']}")
                price = quantize(str(update.get("unit_price", "0")), int(row["price_scale"]))
                prepared.append((row, price))
            for row, price in prepared:
                result = conn.execute(resources_decimal.update().where(and_(
                    resources_decimal.c.id == row["id"],
                    resources_decimal.c.row_version == row["row_version"],
                )).values(unit_price=Decimal(price), updated_at=now,
                         row_version=int(row["row_version"]) + 1))
                if result.rowcount != 1:
                    raise RuntimeError(f"CONFLICT:{row['id']}:{row['row_version']}")
                changed.append({"resource_id": row["id"], "old_unit_price": str(row["unit_price"]),
                                "new_unit_price": price, "row_version": int(row["row_version"]) + 1})
                links = conn.execute(select(resource_budget_links).where(
                    resource_budget_links.c.resource_id == row["id"]
                )).mappings().all()
                for link in links:
                    item = conn.execute(select(budget_items_decimal).where(
                        budget_items_decimal.c.id == link["budget_item_id"]
                    )).mappings().first()
                    if not item:
                        continue
                    new_price = quantize(price, int(item["price_scale"]))
                    new_amount = multiply(str(item["quantity"]), new_price, int(item["amount_scale"]))
                    conn.execute(budget_items_decimal.update().where(
                        budget_items_decimal.c.id == item["id"]
                    ).values(unit_price=Decimal(new_price), amount=Decimal(new_amount),
                             updated_at=now, row_version=int(item["row_version"]) + 1))
                    trace = {"operation":"BATCH_RESOURCE_PRICE_PROPAGATION","quantity":str(item["quantity"]),
                             "resource_unit_price":new_price,"result":new_amount}
                    lineage_row = dict(id=str(uuid4()), project_code=link["project_code"], resource_id=row["id"],
                        budget_item_id=item["id"], old_unit_price=str(item["unit_price"]), new_unit_price=new_price,
                        old_amount=str(item["amount"]), new_amount=new_amount, trigger=trigger,
                        trace_json=json.dumps(trace, sort_keys=True), created_at=now)
                    conn.execute(resource_price_lineage.insert().values(**lineage_row))
                    lineage.append({**lineage_row, "trace": trace})
        for row in lineage:
            row.pop("trace_json", None); row["created_at"] = row["created_at"].isoformat()
        return {"updated_resources": len(changed), "updated_budget_items": len(lineage),
                "resources": changed, "lineage": lineage}


def build_resource_operations_blueprint(service: ResourceOperationsService, resolve_user_id):
    bp = Blueprint("resource_operations", __name__, url_prefix="/api/decimal-resources")
    def auth(): return resolve_user_id() is not None

    @bp.post("/projects/<project_code>/replace")
    def replace(project_code: str):
        if not auth(): return jsonify({"code":"UNAUTHORIZED"}),401
        body=request.get_json(silent=True) or {}
        try: return jsonify(service.replace(project_code,str(body.get("source_resource_id","")),str(body.get("target_resource_id","")),str(resolve_user_id())))
        except ValueError as exc: return jsonify({"code":"INVALID_ARGUMENT","detail":str(exc)}),400
        except LookupError as exc: return jsonify({"code":"NOT_FOUND","detail":str(exc)}),404

    @bp.post("/batch-prices")
    def batch_prices():
        if not auth(): return jsonify({"code":"UNAUTHORIZED"}),401
        body=request.get_json(silent=True) or {}
        try: return jsonify(service.batch_prices(body.get("updates") or [],str(body.get("trigger") or "BATCH_RESOURCE_PRICE_UPDATE")))
        except ValueError as exc: return jsonify({"code":"INVALID_ARGUMENT","detail":str(exc)}),400
        except LookupError as exc: return jsonify({"code":"NOT_FOUND","detail":str(exc)}),404
        except RuntimeError as exc: return jsonify({"code":"CONFLICT","detail":str(exc)}),409
    return bp
