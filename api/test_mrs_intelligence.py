import unittest
from sqlalchemy import create_engine
from sqlalchemy.pool import StaticPool

from api.mrs_catalog import MRSCatalogService
from api.mrs_intelligence import MRSIntelligenceService


class MRSIntelligenceTests(unittest.TestCase):
    def setUp(self):
        self.engine = create_engine("sqlite://", connect_args={"check_same_thread": False}, poolclass=StaticPool, future=True)
        self.catalog = MRSCatalogService(self.engine)
        self.intelligence = MRSIntelligenceService(self.engine)
        self.catalog.save_item("M1", {"code":"M-001","name":"水泥","category":"MATERIAL","unit":"包","current_price":"180.00","price_scale":2}, "7")
        self.catalog.save_item("L1", {"code":"L-001","name":"技工","category":"LABOR","unit":"工","current_price":"2500.00","price_scale":2}, "7")
        self.catalog.save_recipe("R1", {"code":"A-001","name":"混凝土分析","unit":"m3","price_scale":2,
            "components":[{"catalog_item_id":"M1","quantity":"2.50","quantity_scale":2},{"catalog_item_id":"L1","quantity":"0.10","quantity_scale":2}]})

    def test_quote_comparison_and_summary(self):
        self.intelligence.add_quote("M1", {"vendor":"甲商","quoted_price":"175.125","price_scale":2}, "7")
        self.intelligence.add_quote("M1", {"vendor":"乙商","quoted_price":"190.00","price_scale":2}, "7")
        result = self.intelligence.compare_quotes("M1")
        self.assertEqual(result["lowest_quote"], "175.13")
        self.assertEqual(result["highest_quote"], "190.00")
        self.assertEqual(result["spread"], "14.87")
        self.assertEqual(result["current_vs_lowest"], "4.87")
        summary = self.intelligence.summary()
        self.assertEqual(summary["catalog_count"], 2)
        self.assertEqual(summary["recipe_count"], 1)
        self.assertEqual(summary["quote_count"], 2)

    def test_recipe_snapshot_is_append_only(self):
        first = self.intelligence.snapshot_recipe("R1", "7")
        item = self.catalog.get_item("M1")
        self.catalog.save_item("M1", {"code":"M-001","name":"水泥","category":"MATERIAL","unit":"包",
            "current_price":"200.00","price_scale":2,"row_version":item["row_version"]}, "7")
        second = self.intelligence.snapshot_recipe("R1", "7")
        snapshots = self.intelligence.list_snapshots("R1")
        self.assertEqual(first["unit_price"], "700.00")
        self.assertEqual(second["unit_price"], "750.00")
        self.assertEqual(len(snapshots), 2)
        self.assertNotEqual(first["id"], second["id"])

    def test_price_change_impact(self):
        result = self.intelligence.impact("M1", "7", "180.00", "200.00")
        self.assertEqual(result["affected_count"], 1)
        self.assertEqual(result["affected_recipes"][0]["old_amount"], "450.00")
        self.assertEqual(result["affected_recipes"][0]["new_amount"], "500.00")
        self.assertEqual(result["affected_recipes"][0]["delta"], "50.00")
        self.assertEqual(result["total_component_delta"], "50.00")
        self.assertIn("impact=", result["deep_link"])


if __name__ == "__main__":
    unittest.main()
