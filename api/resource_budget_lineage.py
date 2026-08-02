"""Explicit resource-to-budget links and append-only price propagation lineage."""
from __future__ import annotations

import json
from datetime import datetime, timezone
from decimal import Decimal
from uuid import uuid4

from sqlalchemy import Column, DateTime, MetaData, String, Table, Text, and_, select

from api.budget_decimal import budget_items_decimal
from api.decimal_math import multiply, quantize
from api.resource_decimal import resources_decimal

metadata = MetaData()
resource_budget_links = Table(
    "resource_budget_links", metadata,
    Column("id", String(100), primary_key=True),
    Column("project_code", String(100), nullable=False, index=True),
    Column("resource_id", String(100), nullable=False, index=True),
    Column("budget_item_id", String(100), nullable=False, index=True),
    Column("created_at", DateTime(timezone=True), nullable=False),
)
resource_price_lineage = Table(
    "resource_price_lineage", metadata,
    Column("id", String(100), primary_key=True),
    Column("project_code", String(100), nullable=False, index=True),
    Column("resource_id", String(100), nullable=False, index=True),
    Column("budget_item_id", String(100), nullable=False, index=True),
    Column("old_unit_price", String(100), nullable=False),
    Column("new_unit_price", String(100), nullable=False),
    Column("old_amount", String(100), nullable=False),
    Column("new_amount", String(100), nullable=False),
    Column("trigger", String(50), nullable=False),
    Column("trace_json", Text, nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
)


class ResourceBudgetLineageService:
    def __init__(self, engine):
        self.engine = engine
        metadata.create_all(engine)

    def link(self, project_code: str, resource_id: str, budget_item_id: str) -> dict:
        now = datetime.now(timezone.utc)
        link_id = f"{project_code}:{resource_id}:{budget_item_id}"
        with self.engine.begin() as conn:
            resource = conn.execute(select(resources_decimal.c.id).where(resources_decimal.c.id == resource_id)).first()
            item = conn.execute(select(budget_items_decimal.c.id).where(and_(
                budget_items_decimal.c.id == budget_item_id,
                budget_items_decimal.c.project_code == project_code,
            ))).first()
            if not resource or not item:
                raise ValueError("resource and budget item must exist")
            exists = conn.execute(select(resource_budget_links.c.id).where(resource_budget_links.c.id == link_id)).first()
            if not exists:
                conn.execute(resource_budget_links.insert().values(
                    id=link_id, project_code=project_code, resource_id=resource_id,
                    budget_item_id=budget_item_id, created_at=now,
                ))
        return {"id": link_id, "project_code": project_code, "resource_id": resource_id, "budget_item_id": budget_item_id}

    def propagate(self, resource_id: str, trigger: str = "RESOURCE_PRICE_CHANGED") -> list[dict]:
        now = datetime.now(timezone.utc)
        produced: list[dict] = []
        with self.engine.begin() as conn:
            resource = conn.execute(select(resources_decimal).where(resources_decimal.c.id == resource_id)).mappings().first()
            if not resource:
                raise ValueError("resource not found")
            links = conn.execute(select(resource_budget_links).where(
                resource_budget_links.c.resource_id == resource_id
            )).mappings().all()
            for link in links:
                item = conn.execute(select(budget_items_decimal).where(
                    budget_items_decimal.c.id == link["budget_item_id"]
                )).mappings().first()
                if not item:
                    continue
                price = quantize(str(resource["unit_price"]), int(item["price_scale"]))
                old_price = quantize(str(item["unit_price"]), int(item["price_scale"]))
                old_amount = quantize(str(item["amount"]), int(item["amount_scale"]))
                new_amount = multiply(str(item["quantity"]), price, int(item["amount_scale"]))
                conn.execute(budget_items_decimal.update().where(
                    budget_items_decimal.c.id == item["id"]
                ).values(unit_price=Decimal(price), amount=Decimal(new_amount),
                         updated_at=now, row_version=int(item["row_version"]) + 1))
                trace = {
                    "operation": "RESOURCE_PRICE_PROPAGATION",
                    "quantity": quantize(str(item["quantity"]), int(item["quantity_scale"])),
                    "resource_unit_price": price,
                    "result": new_amount,
                }
                row = {
                    "id": str(uuid4()), "project_code": link["project_code"],
                    "resource_id": resource_id, "budget_item_id": item["id"],
                    "old_unit_price": old_price, "new_unit_price": price,
                    "old_amount": old_amount, "new_amount": new_amount,
                    "trigger": trigger, "trace_json": json.dumps(trace, sort_keys=True),
                    "created_at": now,
                }
                conn.execute(resource_price_lineage.insert().values(**row))
                produced.append({**row, "trace": trace})
        for row in produced:
            row.pop("trace_json", None)
            row["created_at"] = row["created_at"].isoformat()
        return produced

    def list_project(self, project_code: str) -> list[dict]:
        with self.engine.connect() as conn:
            rows = conn.execute(select(resource_price_lineage).where(
                resource_price_lineage.c.project_code == project_code
            ).order_by(resource_price_lineage.c.created_at.desc())).mappings().all()
        return [{
            "id": r["id"], "project_code": r["project_code"], "resource_id": r["resource_id"],
            "budget_item_id": r["budget_item_id"], "old_unit_price": r["old_unit_price"],
            "new_unit_price": r["new_unit_price"], "old_amount": r["old_amount"],
            "new_amount": r["new_amount"], "trigger": r["trigger"],
            "trace": json.loads(r["trace_json"]), "created_at": r["created_at"].isoformat(),
        } for r in rows]
