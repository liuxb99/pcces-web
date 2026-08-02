"""Phase 2 Decimal Budget Core.

Provides exact quantity/unit-price/amount persistence and deterministic
recalculation without depending on the legacy Float-based ORM models.
"""

from __future__ import annotations

from datetime import datetime, timezone
from decimal import Decimal
from typing import Callable

from flask import Blueprint, jsonify, request
from sqlalchemy import Column, DateTime, Integer, MetaData, Numeric, String, Table, Text, and_, select

from api.decimal_math import multiply, quantize, sum_values

metadata = MetaData()
budget_items_decimal = Table(
    "budget_items_decimal", metadata,
    Column("id", String(100), primary_key=True),
    Column("project_code", String(100), nullable=False, index=True),
    Column("parent_id", String(100), nullable=True, index=True),
    Column("item_no", String(50), nullable=True),
    Column("name", String(500), nullable=False),
    Column("kind", String(10), nullable=False),
    Column("quantity", Numeric(28, 8), nullable=False, default=Decimal("0")),
    Column("unit_price", Numeric(28, 8), nullable=False, default=Decimal("0")),
    Column("amount", Numeric(28, 8), nullable=False, default=Decimal("0")),
    Column("quantity_scale", Integer, nullable=False, default=4),
    Column("price_scale", Integer, nullable=False, default=4),
    Column("amount_scale", Integer, nullable=False, default=2),
    Column("created_at", DateTime(timezone=True), nullable=False),
    Column("updated_at", DateTime(timezone=True), nullable=False),
    Column("row_version", Integer, nullable=False, default=1),
)


def _decimal_text(value: Decimal, scale: int) -> str:
    return quantize(str(value), scale)


def calculate_leaf(quantity: str, unit_price: str, amount_scale: int) -> str:
    return multiply(quantity, unit_price, amount_scale)


def calculate_rollup(children: list[str], amount_scale: int) -> str:
    return sum_values(children, amount_scale)


class BudgetDecimalService:
    def __init__(self, engine):
        self.engine = engine

    def create_schema(self) -> None:
        metadata.create_all(self.engine)

    def get(self, item_id: str) -> dict | None:
        with self.engine.connect() as conn:
            row = conn.execute(select(budget_items_decimal).where(budget_items_decimal.c.id == item_id)).mappings().first()
        return self._serialize(row) if row else None

    def list_project(self, project_code: str) -> list[dict]:
        with self.engine.connect() as conn:
            rows = conn.execute(select(budget_items_decimal).where(
                budget_items_decimal.c.project_code == project_code
            ).order_by(budget_items_decimal.c.item_no, budget_items_decimal.c.id)).mappings().all()
        return [self._serialize(row) for row in rows]

    def save(self, item_id: str, body: dict) -> tuple[dict, int]:
        now = datetime.now(timezone.utc)
        with self.engine.begin() as conn:
            current = conn.execute(select(budget_items_decimal).where(budget_items_decimal.c.id == item_id)).mappings().first()
            requested_version = int(body.get("row_version", 0))
            if current is not None and requested_version != int(current["row_version"]):
                return {"code":"CONFLICT","detail":"stale budget item row_version","current_row_version":int(current["row_version"])}, 409

            kind = str(body.get("kind") or (current["kind"] if current else "L")).upper()
            amount_scale = int(body.get("amount_scale", current["amount_scale"] if current else 2))
            quantity_scale = int(body.get("quantity_scale", current["quantity_scale"] if current else 4))
            price_scale = int(body.get("price_scale", current["price_scale"] if current else 4))
            quantity = quantize(str(body.get("quantity", current["quantity"] if current else "0")), quantity_scale)
            unit_price = quantize(str(body.get("unit_price", current["unit_price"] if current else "0")), price_scale)
            amount = calculate_leaf(quantity, unit_price, amount_scale) if kind != "B" else _decimal_text(current["amount"] if current else Decimal("0"), amount_scale)
            values = {
                "project_code": body.get("project_code") or (current["project_code"] if current else ""),
                "parent_id": body.get("parent_id", current["parent_id"] if current else None),
                "item_no": body.get("item_no", current["item_no"] if current else None),
                "name": body.get("name") or (current["name"] if current else ""),
                "kind": kind,
                "quantity": Decimal(quantity),
                "unit_price": Decimal(unit_price),
                "amount": Decimal(amount),
                "quantity_scale": quantity_scale,
                "price_scale": price_scale,
                "amount_scale": amount_scale,
                "updated_at": now,
                "row_version": 1 if current is None else int(current["row_version"]) + 1,
            }
            if not values["project_code"] or not values["name"]:
                return {"code":"INVALID_ARGUMENT","detail":"project_code and name are required"}, 400
            if current is None:
                conn.execute(budget_items_decimal.insert().values(id=item_id, created_at=now, **values))
            else:
                result = conn.execute(budget_items_decimal.update().where(and_(
                    budget_items_decimal.c.id == item_id,
                    budget_items_decimal.c.row_version == requested_version,
                )).values(**values))
                if result.rowcount != 1:
                    return {"code":"CONFLICT","detail":"budget item update conflict"}, 409
        return self.get(item_id), 200

    def recalculate_project(self, project_code: str) -> tuple[dict, int]:
        with self.engine.begin() as conn:
            rows = conn.execute(select(budget_items_decimal).where(
                budget_items_decimal.c.project_code == project_code
            )).mappings().all()
            if not rows:
                return {"code":"NOT_FOUND","detail":"project has no decimal budget items"}, 404
            by_parent: dict[str | None, list[dict]] = {}
            by_id = {row["id"]: row for row in rows}
            for row in rows:
                by_parent.setdefault(row["parent_id"], []).append(row)

            updated = 0
            def visit(item_id: str) -> Decimal:
                nonlocal updated
                row = by_id[item_id]
                children = by_parent.get(item_id, [])
                scale = int(row["amount_scale"])
                if children or row["kind"] == "B":
                    child_amounts = [str(visit(child["id"])) for child in children]
                    amount_text = calculate_rollup(child_amounts, scale)
                else:
                    amount_text = calculate_leaf(str(row["quantity"]), str(row["unit_price"]), scale)
                amount = Decimal(amount_text)
                if amount != row["amount"]:
                    conn.execute(budget_items_decimal.update().where(
                        budget_items_decimal.c.id == item_id
                    ).values(amount=amount, updated_at=datetime.now(timezone.utc), row_version=int(row["row_version"]) + 1))
                    updated += 1
                return amount

            roots = by_parent.get(None, [])
            total = sum((visit(root["id"]) for root in roots), Decimal("0"))
        return {"project_code":project_code,"total_amount":quantize(str(total), 2),"updated_items":updated}, 200

    @staticmethod
    def _serialize(row) -> dict:
        return {
            "id": row["id"], "project_code": row["project_code"], "parent_id": row["parent_id"],
            "item_no": row["item_no"], "name": row["name"], "kind": row["kind"],
            "quantity": _decimal_text(row["quantity"], int(row["quantity_scale"])),
            "unit_price": _decimal_text(row["unit_price"], int(row["price_scale"])),
            "amount": _decimal_text(row["amount"], int(row["amount_scale"])),
            "quantity_scale": row["quantity_scale"], "price_scale": row["price_scale"],
            "amount_scale": row["amount_scale"], "row_version": row["row_version"],
        }


def build_budget_decimal_blueprint(service: BudgetDecimalService, resolve_user_id: Callable[[], int | None]) -> Blueprint:
    blueprint = Blueprint("budget_decimal", __name__, url_prefix="/api/decimal-budget")

    def authorized():
        return resolve_user_id() is not None

    @blueprint.get("/projects/<project_code>/items")
    def list_items(project_code: str):
        if not authorized(): return jsonify({"code":"UNAUTHORIZED"}), 401
        return jsonify(service.list_project(project_code))

    @blueprint.get("/items/<item_id>")
    def get_item(item_id: str):
        if not authorized(): return jsonify({"code":"UNAUTHORIZED"}), 401
        item = service.get(item_id)
        return (jsonify(item), 200) if item else (jsonify({"code":"NOT_FOUND"}), 404)

    @blueprint.put("/items/<item_id>")
    def save_item(item_id: str):
        if not authorized(): return jsonify({"code":"UNAUTHORIZED"}), 401
        result, status = service.save(item_id, request.get_json(silent=True) or {})
        return jsonify(result), status

    @blueprint.post("/projects/<project_code>/recalculate")
    def recalculate(project_code: str):
        if not authorized(): return jsonify({"code":"UNAUTHORIZED"}), 401
        result, status = service.recalculate_project(project_code)
        return jsonify(result), status

    return blueprint
