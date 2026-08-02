import unittest
from sqlalchemy import create_engine

from api.budget_decimal import BudgetDecimalService
from api.resource_budget_lineage import ResourceBudgetLineageService
from api.resource_decimal import ResourceDecimalService


class ResourceBudgetLineageTests(unittest.TestCase):
    def setUp(self):
        self.engine = create_engine("sqlite+pysqlite:///:memory:", future=True)
        self.budget = BudgetDecimalService(self.engine); self.budget.create_schema()
        self.resource = ResourceDecimalService(self.engine); self.resource.create_schema()
        self.lineage = ResourceBudgetLineageService(self.engine)
        self.budget.save("legacy-1", {
            "project_code":"P1", "name":"混凝土", "kind":"L",
            "quantity":"3.0000", "unit_price":"10.0000",
            "quantity_scale":4, "price_scale":4, "amount_scale":2, "row_version":0,
        })
        self.resource.save_resource("legacy-resource-2", {
            "code":"P1:R2", "name":"材料", "unit":"kg",
            "unit_price":"12.3456", "price_scale":4, "row_version":0,
        })

    def test_explicit_link_propagates_exact_price_and_amount(self):
        link = self.lineage.link("P1", "legacy-resource-2", "legacy-1")
        self.assertEqual("P1:legacy-resource-2:legacy-1", link["id"])
        rows = self.lineage.propagate("legacy-resource-2")
        self.assertEqual(1, len(rows))
        item = self.budget.get("legacy-1")
        self.assertEqual("12.3456", item["unit_price"])
        self.assertEqual("37.04", item["amount"])
        self.assertEqual("RESOURCE_PRICE_PROPAGATION", rows[0]["trace"]["operation"])

    def test_link_is_idempotent_and_lineage_is_append_only(self):
        self.lineage.link("P1", "legacy-resource-2", "legacy-1")
        self.lineage.link("P1", "legacy-resource-2", "legacy-1")
        self.lineage.propagate("legacy-resource-2", "FIRST")
        self.resource.save_resource("legacy-resource-2", {
            "unit_price":"20", "row_version":1,
        })
        self.lineage.propagate("legacy-resource-2", "SECOND")
        rows = self.lineage.list_project("P1")
        self.assertEqual(2, len(rows))
        self.assertEqual("SECOND", rows[0]["trigger"])
        self.assertEqual("60.00", rows[0]["new_amount"])

    def test_missing_endpoints_are_rejected(self):
        with self.assertRaises(ValueError):
            self.lineage.link("P1", "missing", "legacy-1")
        with self.assertRaises(ValueError):
            self.lineage.link("P1", "legacy-resource-2", "missing")


if __name__ == "__main__":
    unittest.main()
