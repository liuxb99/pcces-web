import unittest
from sqlalchemy import create_engine

from api.budget_decimal import BudgetDecimalService
from api.resource_budget_lineage import ResourceBudgetLineageService
from api.resource_budget_links import ResourceBudgetLinkService
from api.resource_decimal import ResourceDecimalService


class ResourceBudgetLinkTests(unittest.TestCase):
    def setUp(self):
        self.engine = create_engine("sqlite+pysqlite:///:memory:", future=True)
        self.budget = BudgetDecimalService(self.engine); self.budget.create_schema()
        self.resource = ResourceDecimalService(self.engine); self.resource.create_schema()
        self.lineage = ResourceBudgetLineageService(self.engine)
        self.links = ResourceBudgetLinkService(self.engine)
        for item_id, qty in (("I1", "2"), ("I2", "3")):
            self.budget.save(item_id, {"project_code":"P1", "name":item_id, "kind":"L",
                "quantity":qty, "unit_price":"10", "quantity_scale":2,
                "price_scale":2, "amount_scale":2, "row_version":0})
        self.resource.save_resource("R1", {"code":"M00001", "name":"水泥", "unit":"KG",
            "unit_price":"12.34", "price_scale":2, "row_version":0})
        self.lineage.link("P1", "R1", "I1")
        self.lineage.link("P1", "R1", "I2")

    def test_bidirectional_project_resource_drilldown(self):
        page = self.links.list_project_resources("P1", "水泥")
        self.assertEqual(1, page["total"])
        self.assertEqual(2, page["items"][0]["reference_count"])
        self.assertIn("resource=R1", page["items"][0]["deep_link"])
        refs = self.links.list_resource_references("P1", "R1", limit=1)
        self.assertEqual(2, refs["total"])
        self.assertEqual(1, len(refs["items"]))
        self.assertEqual("/app/budget/P1?item=I1", refs["items"][0]["deep_link"])

    def test_unlink_is_explicit_and_scoped(self):
        self.assertTrue(self.links.unlink("P1", "R1", "I1"))
        refs = self.links.list_resource_references("P1", "R1")
        self.assertEqual(1, refs["total"])
        self.assertEqual("I2", refs["items"][0]["budget_item_id"])
        self.assertFalse(self.links.unlink("P1", "R1", "missing"))

    def test_paging_bounds_are_stable(self):
        page = self.links.list_project_resources("P1", limit=999, offset=-5)
        self.assertEqual(200, page["limit"])
        self.assertEqual(0, page["offset"])


if __name__ == "__main__":
    unittest.main()
