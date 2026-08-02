"""Phase 3 MRS price intelligence, recipe snapshots and impact analysis."""
from __future__ import annotations

import json
from datetime import datetime, timezone
from decimal import Decimal
from uuid import uuid4

from flask import Blueprint, jsonify, request
from sqlalchemy import Column, DateTime, Integer, MetaData, String, Table, Text, and_, select

from api.decimal_math import multiply, parse_decimal, quantize, sum_values
from api.mrs_catalog import (
    mrs_analysis_components,
    mrs_analysis_recipes,
    mrs_catalog_items,
    mrs_price_history,
)

metadata = MetaData()
mrs_price_quotes = Table(
    "mrs_price_quotes", metadata,
    Column("id", String(100), primary_key=True),
    Column("catalog_item_id", String(100), nullable=False, index=True),
    Column("vendor", String(300), nullable=False),
    Column("quoted_price", String(100), nullable=False),
    Column("price_scale", Integer, nullable=False),
    Column("source_document", String(500)),
    Column("effective_date", String(30)),
    Column("created_by", String(100), nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
)
mrs_analysis_snapshots = Table(
    "mrs_analysis_snapshots", metadata,
    Column("id", String(100), primary_key=True),
    Column("recipe_id", String(100), nullable=False, index=True),
    Column("unit_price", String(100), nullable=False),
    Column("snapshot_json", Text, nullable=False),
    Column("created_by", String(100), nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
)
mrs_impact_runs = Table(
    "mrs_impact_runs", metadata,
    Column("id", String(100), primary_key=True),
    Column("catalog_item_id", String(100), nullable=False, index=True),
    Column("old_price", String(100), nullable=False),
    Column("new_price", String(100), nullable=False),
    Column("result_json", Text, nullable=False),
    Column("created_by", String(100), nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
)


class MRSIntelligenceService:
    def __init__(self, engine):
        self.engine = engine
        metadata.create_all(engine)

    def add_quote(self, item_id: str, body: dict, actor: str) -> dict:
        vendor = str(body.get("vendor", "")).strip()
        if not vendor:
            raise ValueError("vendor is required")
        with self.engine.connect() as conn:
            item = conn.execute(select(mrs_catalog_items).where(mrs_catalog_items.c.id == item_id)).mappings().first()
        if not item:
            raise LookupError("catalog item not found")
        scale = int(body.get("price_scale", item["price_scale"]))
        quote = quantize(str(body.get("quoted_price", "0")), scale)
        quote_id, now = str(uuid4()), datetime.now(timezone.utc)
        with self.engine.begin() as conn:
            conn.execute(mrs_price_quotes.insert().values(
                id=quote_id, catalog_item_id=item_id, vendor=vendor, quoted_price=quote,
                price_scale=scale, source_document=body.get("source_document"),
                effective_date=body.get("effective_date"), created_by=actor, created_at=now,
            ))
        return {"id": quote_id, "catalog_item_id": item_id, "vendor": vendor, "quoted_price": quote,
                "price_scale": scale, "source_document": body.get("source_document"),
                "effective_date": body.get("effective_date"), "created_by": actor,
                "created_at": now.isoformat()}

    def compare_quotes(self, item_id: str) -> dict:
        with self.engine.connect() as conn:
            item = conn.execute(select(mrs_catalog_items).where(mrs_catalog_items.c.id == item_id)).mappings().first()
            rows = conn.execute(select(mrs_price_quotes).where(mrs_price_quotes.c.catalog_item_id == item_id)
                                .order_by(mrs_price_quotes.c.created_at.desc())).mappings().all()
        if not item:
            raise LookupError("catalog item not found")
        quotes = [{**dict(row), "created_at": row["created_at"].isoformat()} for row in rows]
        prices = [parse_decimal(row["quoted_price"]) for row in rows]
        if prices:
            low, high = min(prices), max(prices)
            difference = quantize(high - low, int(item["price_scale"]))
            current = parse_decimal(item["current_price"])
            best_delta = quantize(current - low, int(item["price_scale"]))
        else:
            difference = quantize("0", int(item["price_scale"]))
            best_delta = difference
        return {"catalog_item_id": item_id, "current_price": item["current_price"], "quotes": quotes,
                "lowest_quote": quantize(min(prices), int(item["price_scale"])) if prices else None,
                "highest_quote": quantize(max(prices), int(item["price_scale"])) if prices else None,
                "spread": difference, "current_vs_lowest": best_delta}

    def snapshot_recipe(self, recipe_id: str, actor: str) -> dict:
        calculation = self._calculate_recipe(recipe_id)
        snapshot_id, now = str(uuid4()), datetime.now(timezone.utc)
        with self.engine.begin() as conn:
            conn.execute(mrs_analysis_snapshots.insert().values(
                id=snapshot_id, recipe_id=recipe_id, unit_price=calculation["unit_price"],
                snapshot_json=json.dumps(calculation, ensure_ascii=False, sort_keys=True),
                created_by=actor, created_at=now,
            ))
        return {"id": snapshot_id, "recipe_id": recipe_id, "unit_price": calculation["unit_price"],
                "snapshot": calculation, "created_by": actor, "created_at": now.isoformat(),
                "deep_link": f"/app/mrs-insights?recipe={recipe_id}&snapshot={snapshot_id}"}

    def list_snapshots(self, recipe_id: str) -> list[dict]:
        with self.engine.connect() as conn:
            rows = conn.execute(select(mrs_analysis_snapshots).where(
                mrs_analysis_snapshots.c.recipe_id == recipe_id).order_by(
                mrs_analysis_snapshots.c.created_at.desc())).mappings().all()
        return [{"id": row["id"], "recipe_id": row["recipe_id"], "unit_price": row["unit_price"],
                 "snapshot": json.loads(row["snapshot_json"]), "created_by": row["created_by"],
                 "created_at": row["created_at"].isoformat(),
                 "deep_link": f"/app/mrs-insights?recipe={recipe_id}&snapshot={row['id']}"} for row in rows]

    def impact(self, item_id: str, actor: str, old_price: str | None = None, new_price: str | None = None) -> dict:
        with self.engine.connect() as conn:
            item = conn.execute(select(mrs_catalog_items).where(mrs_catalog_items.c.id == item_id)).mappings().first()
            if not item:
                raise LookupError("catalog item not found")
            history = conn.execute(select(mrs_price_history).where(
                mrs_price_history.c.catalog_item_id == item_id).order_by(
                mrs_price_history.c.created_at.desc())).mappings().first()
            components = conn.execute(select(mrs_analysis_components.c.recipe_id,
                mrs_analysis_components.c.quantity, mrs_analysis_recipes.c.code,
                mrs_analysis_recipes.c.name, mrs_analysis_recipes.c.price_scale).join(
                mrs_analysis_recipes, mrs_analysis_recipes.c.id == mrs_analysis_components.c.recipe_id)
                .where(mrs_analysis_components.c.catalog_item_id == item_id)).mappings().all()
        old_value = old_price or (history["old_price"] if history and history["old_price"] is not None else item["current_price"])
        new_value = new_price or item["current_price"]
        affected = []
        total_delta_values = []
        for row in components:
            old_amount = multiply(row["quantity"], old_value, int(row["price_scale"]))
            new_amount = multiply(row["quantity"], new_value, int(row["price_scale"]))
            delta = quantize(parse_decimal(new_amount) - parse_decimal(old_amount), int(row["price_scale"]))
            total_delta_values.append(delta)
            affected.append({"recipe_id": row["recipe_id"], "recipe_code": row["code"], "recipe_name": row["name"],
                             "quantity": row["quantity"], "old_amount": old_amount,
                             "new_amount": new_amount, "delta": delta})
        run_id, now = str(uuid4()), datetime.now(timezone.utc)
        result = {"id": run_id, "catalog_item_id": item_id,
                  "old_price": quantize(old_value, int(item["price_scale"])),
                  "new_price": quantize(new_value, int(item["price_scale"])),
                  "affected_recipes": affected, "affected_count": len(affected),
                  "total_component_delta": sum_values(total_delta_values, int(item["price_scale"])),
                  "deep_link": f"/app/mrs-insights?item={item_id}&impact={run_id}"}
        with self.engine.begin() as conn:
            conn.execute(mrs_impact_runs.insert().values(
                id=run_id, catalog_item_id=item_id, old_price=result["old_price"],
                new_price=result["new_price"], result_json=json.dumps(result, ensure_ascii=False, sort_keys=True),
                created_by=actor, created_at=now,
            ))
        result["created_at"] = now.isoformat()
        return result

    def summary(self) -> dict:
        with self.engine.connect() as conn:
            items = conn.execute(select(mrs_catalog_items)).mappings().all()
            recipes = conn.execute(select(mrs_analysis_recipes.c.id)).all()
            quotes = conn.execute(select(mrs_price_quotes.c.id)).all()
        categories: dict[str, dict] = {}
        for item in items:
            bucket = categories.setdefault(item["category"], {"count": 0, "price_total": []})
            bucket["count"] += 1
            bucket["price_total"].append(item["current_price"])
        output = {key: {"count": value["count"], "price_total": sum_values(value["price_total"], 2)}
                  for key, value in categories.items()}
        return {"catalog_count": len(items), "recipe_count": len(recipes), "quote_count": len(quotes),
                "categories": output}

    def _calculate_recipe(self, recipe_id: str) -> dict:
        with self.engine.connect() as conn:
            recipe = conn.execute(select(mrs_analysis_recipes).where(mrs_analysis_recipes.c.id == recipe_id)).mappings().first()
            if not recipe:
                raise LookupError("recipe not found")
            rows = conn.execute(select(mrs_analysis_components, mrs_catalog_items.c.code,
                mrs_catalog_items.c.name, mrs_catalog_items.c.current_price).join(
                mrs_catalog_items, mrs_catalog_items.c.id == mrs_analysis_components.c.catalog_item_id)
                .where(mrs_analysis_components.c.recipe_id == recipe_id)
                .order_by(mrs_analysis_components.c.sequence)).mappings().all()
        components, amounts = [], []
        for row in rows:
            amount = multiply(row["quantity"], row["current_price"], int(recipe["price_scale"]))
            amounts.append(amount)
            components.append({"catalog_item_id": row["catalog_item_id"], "code": row["code"],
                               "name": row["name"], "quantity": row["quantity"],
                               "unit_price": row["current_price"], "amount": amount})
        return {"id": recipe["id"], "code": recipe["code"], "name": recipe["name"],
                "price_scale": recipe["price_scale"], "components": components,
                "unit_price": sum_values(amounts, int(recipe["price_scale"]))}


def build_mrs_intelligence_blueprint(service: MRSIntelligenceService, resolve_user_id):
    bp = Blueprint("mrs_intelligence", __name__, url_prefix="/api/mrs")
    def actor() -> str:
        value = resolve_user_id()
        if value is None:
            raise PermissionError("authentication required")
        return str(value)

    @bp.post("/catalog/<item_id>/quotes")
    def add_quote(item_id):
        try: return jsonify(service.add_quote(item_id, request.get_json(silent=True) or {}, actor())), 201
        except LookupError as exc: return jsonify({"code":"NOT_FOUND","detail":str(exc)}),404
        except ValueError as exc: return jsonify({"code":"INVALID_ARGUMENT","detail":str(exc)}),400

    @bp.get("/catalog/<item_id>/quote-comparison")
    def compare_quotes(item_id):
        try: return jsonify(service.compare_quotes(item_id))
        except LookupError as exc: return jsonify({"code":"NOT_FOUND","detail":str(exc)}),404

    @bp.post("/analysis-recipes/<recipe_id>/snapshots")
    def snapshot(recipe_id):
        try: return jsonify(service.snapshot_recipe(recipe_id, actor())), 201
        except LookupError as exc: return jsonify({"code":"NOT_FOUND","detail":str(exc)}),404

    @bp.get("/analysis-recipes/<recipe_id>/snapshots")
    def snapshots(recipe_id): return jsonify(service.list_snapshots(recipe_id))

    @bp.post("/catalog/<item_id>/impact")
    def impact(item_id):
        body = request.get_json(silent=True) or {}
        try: return jsonify(service.impact(item_id, actor(), body.get("old_price"), body.get("new_price")))
        except LookupError as exc: return jsonify({"code":"NOT_FOUND","detail":str(exc)}),404

    @bp.get("/summary")
    def summary(): return jsonify(service.summary())
    return bp
