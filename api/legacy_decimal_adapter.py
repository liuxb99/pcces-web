"""Rerunnable adapter from legacy Float ORM rows into exact decimal core tables."""

from __future__ import annotations

from decimal import Decimal

from sqlalchemy import delete, select

from api.budget_decimal import BudgetDecimalService, budget_items_decimal
from api.models import BudgetItem, Resource, ResourceBreakdownItem
from api.resource_decimal import ResourceDecimalService, resource_breakdowns_decimal, resources_decimal


def _text(value) -> str:
    if value is None:
        return "0"
    return format(Decimal(str(value)), "f")


def _budget_id(value) -> str:
    """Canonical identifier shared by the adapter and legacy route bridge."""
    return f"legacy-{value}"


class LegacyDecimalAdapter:
    def __init__(self, session_factory, budget_service: BudgetDecimalService, resource_service: ResourceDecimalService):
        self.session_factory = session_factory
        self.budget_service = budget_service
        self.resource_service = resource_service

    def migrate_project(self, project_id: int, project_code: str) -> dict:
        migrated = {"budget_items": 0, "resources": 0, "breakdowns": 0}
        db = self.session_factory()
        try:
            for item in db.query(BudgetItem).filter(BudgetItem.project_id == project_id).all():
                item_id = _budget_id(item.id)
                result, status = self.budget_service.save(item_id, {
                    "project_code": project_code,
                    "parent_id": _budget_id(item.parent_id) if item.parent_id is not None else None,
                    "item_no": item.item_no,
                    "name": item.c_name or item.item_no or str(item.id),
                    "kind": item.kind or "L",
                    "quantity": _text(item.quantity),
                    "unit_price": _text(item.unit_price),
                    "quantity_scale": item.decimal_qty or 2,
                    "price_scale": item.decimal_price or 2,
                    "amount_scale": item.decimal_amount or 2,
                    "row_version": self._budget_version(item_id),
                })
                if status >= 400:
                    raise RuntimeError(result)
                # Early Decimal adapters used the bare numeric ID.  Once the
                # canonical prefixed row exists, retire that obsolete shadow so
                # list/recalculation cannot see the same Legacy item twice.
                with self.budget_service.engine.begin() as conn:
                    conn.execute(delete(budget_items_decimal).where(
                        budget_items_decimal.c.id == str(item.id)
                    ))
                migrated["budget_items"] += 1
            for resource in db.query(Resource).all():
                result, status = self.resource_service.save_resource(str(resource.id), {
                    "code": resource.code,
                    "name": resource.c_name,
                    "unit": resource.c_unit,
                    "unit_price": _text(resource.unit_price),
                    "price_scale": 4,
                    "row_version": self._resource_version(str(resource.id)),
                })
                if status >= 400:
                    raise RuntimeError(result)
                migrated["resources"] += 1
            for row in db.query(ResourceBreakdownItem).all():
                result, status = self.resource_service.save_breakdown(str(row.id), {
                    "resource_id": str(row.resource_id), "code": row.code,
                    "name": row.c_name, "unit": row.c_unit,
                    "quantity": _text(row.quantity), "unit_price": _text(row.unit_price),
                    "quantity_scale": 4, "price_scale": 4, "amount_scale": 2,
                    "row_version": self._breakdown_version(str(row.id)),
                })
                if status >= 400:
                    raise RuntimeError(result)
                migrated["breakdowns"] += 1
            self.budget_service.recalculate_project(project_code)
            return migrated
        finally:
            db.close()

    def _budget_version(self, item_id: str) -> int:
        with self.budget_service.engine.connect() as conn:
            row = conn.execute(select(budget_items_decimal.c.row_version).where(budget_items_decimal.c.id == item_id)).first()
        return int(row[0]) if row else 0

    def _resource_version(self, item_id: str) -> int:
        with self.resource_service.engine.connect() as conn:
            row = conn.execute(select(resources_decimal.c.row_version).where(resources_decimal.c.id == item_id)).first()
        return int(row[0]) if row else 0

    def _breakdown_version(self, item_id: str) -> int:
        with self.resource_service.engine.connect() as conn:
            row = conn.execute(select(resource_breakdowns_decimal.c.row_version).where(resource_breakdowns_decimal.c.id == item_id)).first()
        return int(row[0]) if row else 0
