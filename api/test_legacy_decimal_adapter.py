import unittest
from types import SimpleNamespace
from sqlalchemy import create_engine

from api.budget_decimal import BudgetDecimalService
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
        self.assertEqual(len(budget.list_project("P10")), 1)
        self.assertEqual(resource.get_resource("2")["unit_price"], "7.0000")


if __name__ == "__main__": unittest.main()
