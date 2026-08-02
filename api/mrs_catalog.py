"""Phase 3 MRS catalog, price history, bookmarks, analysis recipes and exchange."""
from __future__ import annotations

import csv
import io
import json
from datetime import datetime, timezone
from uuid import uuid4

from flask import Blueprint, Response, jsonify, request
from sqlalchemy import Boolean, Column, DateTime, Integer, MetaData, String, Table, Text, and_, select

from api.decimal_math import multiply, quantize, sum_values

metadata = MetaData()
mrs_catalog_items = Table(
    "mrs_catalog_items", metadata,
    Column("id", String(100), primary_key=True),
    Column("code", String(100), nullable=False, unique=True, index=True),
    Column("name", String(500), nullable=False),
    Column("category", String(50), nullable=False),
    Column("unit", String(50)),
    Column("current_price", String(100), nullable=False),
    Column("price_scale", Integer, nullable=False),
    Column("source", String(500)),
    Column("enabled", Boolean, nullable=False),
    Column("row_version", Integer, nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
    Column("updated_at", DateTime(timezone=True), nullable=False),
)
mrs_price_history = Table(
    "mrs_price_history", metadata,
    Column("id", String(100), primary_key=True),
    Column("catalog_item_id", String(100), nullable=False, index=True),
    Column("old_price", String(100)),
    Column("new_price", String(100), nullable=False),
    Column("source", String(500)),
    Column("effective_date", String(30)),
    Column("created_by", String(100), nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
)
mrs_bookmarks = Table(
    "mrs_bookmarks", metadata,
    Column("actor_id", String(100), primary_key=True),
    Column("catalog_item_id", String(100), primary_key=True),
    Column("created_at", DateTime(timezone=True), nullable=False),
)
mrs_analysis_recipes = Table(
    "mrs_analysis_recipes", metadata,
    Column("id", String(100), primary_key=True),
    Column("code", String(100), nullable=False, unique=True),
    Column("name", String(500), nullable=False),
    Column("unit", String(50)),
    Column("price_scale", Integer, nullable=False),
    Column("row_version", Integer, nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
    Column("updated_at", DateTime(timezone=True), nullable=False),
)
mrs_analysis_components = Table(
    "mrs_analysis_components", metadata,
    Column("id", String(100), primary_key=True),
    Column("recipe_id", String(100), nullable=False, index=True),
    Column("catalog_item_id", String(100), nullable=False, index=True),
    Column("quantity", String(100), nullable=False),
    Column("quantity_scale", Integer, nullable=False),
    Column("sequence", Integer, nullable=False),
)
mrs_exchange_runs = Table(
    "mrs_exchange_runs", metadata,
    Column("id", String(100), primary_key=True),
    Column("operation", String(30), nullable=False),
    Column("format", String(20), nullable=False),
    Column("status", String(30), nullable=False),
    Column("result_json", Text, nullable=False),
    Column("created_by", String(100), nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
)


class MRSCatalogService:
    CATEGORIES = {"MATERIAL", "LABOR", "EQUIPMENT", "OTHER"}

    def __init__(self, engine):
        self.engine = engine
        metadata.create_all(engine)

    def save_item(self, item_id: str, body: dict, actor: str) -> dict:
        code = str(body.get("code", "")).strip()
        name = str(body.get("name", "")).strip()
        category = str(body.get("category", "OTHER")).upper()
        if not code or not name or category not in self.CATEGORIES:
            raise ValueError("code, name and valid category are required")
        scale = int(body.get("price_scale", 4))
        price = quantize(str(body.get("current_price", "0")), scale)
        expected = int(body.get("row_version", 0))
        now = datetime.now(timezone.utc)
        with self.engine.begin() as conn:
            current = conn.execute(select(mrs_catalog_items).where(mrs_catalog_items.c.id == item_id)).mappings().first()
            values = dict(code=code, name=name, category=category, unit=body.get("unit"), current_price=price,
                          price_scale=scale, source=body.get("source"), enabled=bool(body.get("enabled", True)), updated_at=now)
            if current:
                if int(current["row_version"]) != expected: raise RuntimeError("CONFLICT")
                conn.execute(mrs_catalog_items.update().where(and_(mrs_catalog_items.c.id == item_id,
                    mrs_catalog_items.c.row_version == expected)).values(**values, row_version=expected + 1))
                if str(current["current_price"]) != price:
                    conn.execute(mrs_price_history.insert().values(id=str(uuid4()), catalog_item_id=item_id,
                        old_price=current["current_price"], new_price=price, source=body.get("source"),
                        effective_date=body.get("effective_date"), created_by=actor, created_at=now))
            else:
                conn.execute(mrs_catalog_items.insert().values(id=item_id, **values, row_version=1, created_at=now))
                conn.execute(mrs_price_history.insert().values(id=str(uuid4()), catalog_item_id=item_id,
                    old_price=None, new_price=price, source=body.get("source"), effective_date=body.get("effective_date"),
                    created_by=actor, created_at=now))
        return self.get_item(item_id)

    def get_item(self, item_id: str) -> dict:
        with self.engine.connect() as conn:
            row = conn.execute(select(mrs_catalog_items).where(mrs_catalog_items.c.id == item_id)).mappings().first()
        if not row: raise LookupError("catalog item not found")
        return self._row(row)

    def list_items(self, query: str = "", category: str | None = None) -> list[dict]:
        stmt = select(mrs_catalog_items).order_by(mrs_catalog_items.c.code)
        with self.engine.connect() as conn:
            rows = conn.execute(stmt).mappings().all()
        q = query.strip().lower()
        return [self._row(r) for r in rows if (not category or r["category"] == category.upper()) and
                (not q or q in r["code"].lower() or q in r["name"].lower())]

    def history(self, item_id: str) -> list[dict]:
        with self.engine.connect() as conn:
            rows = conn.execute(select(mrs_price_history).where(mrs_price_history.c.catalog_item_id == item_id)
                                .order_by(mrs_price_history.c.created_at.desc())).mappings().all()
        return [{**dict(r), "created_at": r["created_at"].isoformat()} for r in rows]

    def set_bookmark(self, actor: str, item_id: str, enabled: bool) -> dict:
        self.get_item(item_id)
        with self.engine.begin() as conn:
            conn.execute(mrs_bookmarks.delete().where(and_(mrs_bookmarks.c.actor_id == actor,
                mrs_bookmarks.c.catalog_item_id == item_id)))
            if enabled:
                conn.execute(mrs_bookmarks.insert().values(actor_id=actor, catalog_item_id=item_id,
                                                           created_at=datetime.now(timezone.utc)))
        return {"actor_id": actor, "catalog_item_id": item_id, "bookmarked": enabled}

    def bookmarks(self, actor: str) -> list[dict]:
        with self.engine.connect() as conn:
            rows = conn.execute(select(mrs_catalog_items).join(mrs_bookmarks,
                mrs_bookmarks.c.catalog_item_id == mrs_catalog_items.c.id).where(mrs_bookmarks.c.actor_id == actor)
                .order_by(mrs_catalog_items.c.code)).mappings().all()
        return [self._row(r) for r in rows]

    def save_recipe(self, recipe_id: str, body: dict) -> dict:
        code, name = str(body.get("code", "")).strip(), str(body.get("name", "")).strip()
        if not code or not name: raise ValueError("recipe code and name are required")
        expected, scale = int(body.get("row_version", 0)), int(body.get("price_scale", 4))
        components = body.get("components") or []
        now = datetime.now(timezone.utc)
        with self.engine.begin() as conn:
            current = conn.execute(select(mrs_analysis_recipes).where(mrs_analysis_recipes.c.id == recipe_id)).mappings().first()
            values = dict(code=code, name=name, unit=body.get("unit"), price_scale=scale, updated_at=now)
            if current:
                if int(current["row_version"]) != expected: raise RuntimeError("CONFLICT")
                conn.execute(mrs_analysis_recipes.update().where(mrs_analysis_recipes.c.id == recipe_id)
                             .values(**values, row_version=expected + 1))
                conn.execute(mrs_analysis_components.delete().where(mrs_analysis_components.c.recipe_id == recipe_id))
            else:
                conn.execute(mrs_analysis_recipes.insert().values(id=recipe_id, **values, row_version=1, created_at=now))
            for index, component in enumerate(components):
                catalog_id = str(component.get("catalog_item_id", ""))
                exists = conn.execute(select(mrs_catalog_items.c.id).where(mrs_catalog_items.c.id == catalog_id)).first()
                if not exists: raise ValueError(f"catalog item not found: {catalog_id}")
                quantity_scale = int(component.get("quantity_scale", 4))
                conn.execute(mrs_analysis_components.insert().values(id=str(uuid4()), recipe_id=recipe_id,
                    catalog_item_id=catalog_id, quantity=quantize(str(component.get("quantity", "0")), quantity_scale),
                    quantity_scale=quantity_scale, sequence=index))
        return self.calculate_recipe(recipe_id)

    def calculate_recipe(self, recipe_id: str) -> dict:
        with self.engine.connect() as conn:
            recipe = conn.execute(select(mrs_analysis_recipes).where(mrs_analysis_recipes.c.id == recipe_id)).mappings().first()
            if not recipe: raise LookupError("recipe not found")
            rows = conn.execute(select(mrs_analysis_components, mrs_catalog_items.c.code, mrs_catalog_items.c.name,
                mrs_catalog_items.c.current_price).join(mrs_catalog_items,
                mrs_catalog_items.c.id == mrs_analysis_components.c.catalog_item_id)
                .where(mrs_analysis_components.c.recipe_id == recipe_id)
                .order_by(mrs_analysis_components.c.sequence)).mappings().all()
        components, amounts = [], []
        for row in rows:
            amount = multiply(row["quantity"], row["current_price"], int(recipe["price_scale"]))
            amounts.append(amount)
            components.append({"catalog_item_id": row["catalog_item_id"], "code": row["code"], "name": row["name"],
                               "quantity": row["quantity"], "unit_price": row["current_price"], "amount": amount})
        return {"id": recipe["id"], "code": recipe["code"], "name": recipe["name"], "unit": recipe["unit"],
                "price_scale": recipe["price_scale"], "row_version": recipe["row_version"],
                "components": components, "unit_price": sum_values(amounts, int(recipe["price_scale"]))}

    def export_items(self, fmt: str, actor: str):
        items = self.list_items()
        run_id, now = str(uuid4()), datetime.now(timezone.utc)
        if fmt == "json": payload, mimetype = json.dumps(items, ensure_ascii=False, indent=2), "application/json"
        elif fmt == "csv":
            output = io.StringIO(); writer = csv.DictWriter(output, fieldnames=["id","code","name","category","unit","current_price","price_scale","source","enabled"])
            writer.writeheader(); writer.writerows([{k: row.get(k) for k in writer.fieldnames} for row in items]); payload, mimetype = output.getvalue(), "text/csv"
        else: raise ValueError("format must be json or csv")
        with self.engine.begin() as conn:
            conn.execute(mrs_exchange_runs.insert().values(id=run_id, operation="EXPORT", format=fmt.upper(), status="COMPLETED",
                result_json=json.dumps({"count": len(items)}), created_by=actor, created_at=now))
        return payload, mimetype, run_id

    @staticmethod
    def _row(row):
        result = dict(row)
        result["created_at"] = row["created_at"].isoformat(); result["updated_at"] = row["updated_at"].isoformat()
        return result


def build_mrs_catalog_blueprint(service: MRSCatalogService, resolve_user_id):
    bp = Blueprint("mrs_catalog", __name__, url_prefix="/api/mrs")
    def actor():
        value = resolve_user_id()
        if value is None: raise PermissionError("authentication required")
        return str(value)
    @bp.get("/catalog")
    def list_catalog(): return jsonify(service.list_items(request.args.get("q", ""), request.args.get("category")))
    @bp.get("/catalog/<item_id>")
    def get_catalog(item_id):
        try: return jsonify(service.get_item(item_id))
        except LookupError as e: return jsonify({"code":"NOT_FOUND","detail":str(e)}),404
    @bp.put("/catalog/<item_id>")
    def save_catalog(item_id):
        try: return jsonify(service.save_item(item_id, request.get_json(silent=True) or {}, actor()))
        except RuntimeError: return jsonify({"code":"CONFLICT"}),409
        except ValueError as e: return jsonify({"code":"INVALID_ARGUMENT","detail":str(e)}),400
    @bp.get("/catalog/<item_id>/price-history")
    def price_history(item_id): return jsonify(service.history(item_id))
    @bp.put("/catalog/<item_id>/bookmark")
    def bookmark(item_id): return jsonify(service.set_bookmark(actor(), item_id, bool((request.get_json(silent=True) or {}).get("bookmarked"))))
    @bp.get("/bookmarks")
    def bookmarks(): return jsonify(service.bookmarks(actor()))
    @bp.put("/analysis-recipes/<recipe_id>")
    def save_recipe(recipe_id):
        try: return jsonify(service.save_recipe(recipe_id, request.get_json(silent=True) or {}))
        except RuntimeError: return jsonify({"code":"CONFLICT"}),409
        except ValueError as e: return jsonify({"code":"INVALID_ARGUMENT","detail":str(e)}),400
    @bp.get("/analysis-recipes/<recipe_id>/calculate")
    def calculate(recipe_id):
        try: return jsonify(service.calculate_recipe(recipe_id))
        except LookupError as e: return jsonify({"code":"NOT_FOUND","detail":str(e)}),404
    @bp.get("/catalog/export")
    def export_catalog():
        try:
            payload, mimetype, run_id = service.export_items(request.args.get("format", "json").lower(), actor())
            response = Response(payload, mimetype=mimetype); response.headers["X-Exchange-Run-ID"] = run_id; return response
        except ValueError as e: return jsonify({"code":"INVALID_ARGUMENT","detail":str(e)}),400
    return bp
