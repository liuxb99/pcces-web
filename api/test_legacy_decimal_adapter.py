import unittest
from types import SimpleNamespace
from sqlalchemy import create_engine, select

from api.budget_decimal import BudgetDecimalService, budget_items_decimal
from api.legacy_decimal_adapter import LegacyDecimalAdapter
from api.models import BudgetItem, Resource, ResourceBreakdownItem
from api.resource_decimal import ResourceDecimalService


class FakeQuery:
    def __init__(self, rows): self.rows = rows
    def filter(self, *_): return self
    def all(self): return list(self.rows)

class FakeSession:
    def __init__(self, mapping): self.mapping = mapping
    def query(self, model): return FakeQuery(self.mapping.get(model, []))
    def close(self): pass


class LegacyDecimalAdapterTests(unittest.TestCase):
    def test_adapter_can_be_rerun_without_duplicate_rows(self):
        engine = create_engine("sqlite+pysqlite:///:memory:", future=True)
        budget = BudgetDecimalService(engine); budget.create_schema()
        resource = ResourceDecimalService(engine); resource.create_schema()
        rows = {
            BudgetItem: [SimpleNamespace(id=1, project_id=10, parent_id=None, item_no="001", c_name="工項", kind="L", quantity=1.005, unit_price=10.0, decimal_qty=3, decimal_price=2, decimal_amount=2)],
            Resource: [SimpleNamespace(id=2, code="R01", c_name="材料", c_unit="kg", unit_price=3.5)],
            ResourceBreakdownItem: [SimpleNamespace(id=3, resource_id=2, code="M01", c_name="組成", c_unit="kg", quantity=2.0, unit_price=3.5)],
        }
        adapter = LegacyDecimalAdapter(lambda: FakeSession(rows), budget, resource)
        first = adapter.migrate_project(10, "P10")
        second = adapter.migrate_project(10, "P10")
        self.assertEqual(first, {"budget_items":1,"resources":1,"breakdowns":1})
        self.assertEqual(second, first)
        items = budget.list_project("P10")
        self.assertEqual(1, len(items))
        self.assertEqual("legacy-1", items[0]["id"])
        self.assertEqual(resource.get_resource("2")["unit_price"], "7.0000")

    def test_adapter_retires_pre_bridge_numeric_shadow(self):
        engine = create_engine("sqlite+pysqlite:///:memory:", future=True)
        budget = BudgetDecimalService(engine); budget.create_schema()
        resource = ResourceDecimalService(engine); resource.create_schema()
        result, status = budget.save("1", {
            "project_code":"P10", "name":"old", "kind":"L",
            "quantity":"1", "unit_price":"2", "row_version":0,
        })
        self.assertEqual(200, status)
        rows = {
            BudgetItem: [SimpleNamespace(id=1, project_id=10, parent_id=None, item_no="001", c_name="工項", kind="L", quantity=1.0, unit_price=2.0, decimal_qty=2, decimal_price=2, decimal_amount=2)],
            Resource: [], ResourceBreakdownItem: [],
        }
        LegacyDecimalAdapter(lambda: FakeSession(rows), budget, resource).migrate_project(10, "P10")
        with engine.connect() as conn:
            ids = [row[0] for row in conn.execute(select(budget_items_decimal.c.id)).all()]
        self.assertEqual(["legacy-1"], ids)


if __name__ == "__main__": unittest.main()
