import unittest
from sqlalchemy import create_engine

from api.mrs_catalog import MRSCatalogService
from api.mrs_history_apply import MRSHistoryApplyService
from api.mrs_operations import MRSOperationsService


class MRSRateHistoryApplyTests(unittest.TestCase):
    def setUp(self):
        self.engine = create_engine("sqlite+pysqlite:///:memory:", future=True)
        self.catalog = MRSCatalogService(self.engine)
        self.operations = MRSOperationsService(self.engine, self.catalog, None)
        self.history = MRSHistoryApplyService(self.engine)
        self.catalog.save_item("mat-1", {"code":"M0000100000","name":"材料","category":"MATERIAL","current_price":"10","price_scale":4}, "tester")
        self.catalog.save_item("lab-1", {"code":"L000010000000","name":"人工","category":"LABOR","current_price":"20","price_scale":4}, "tester")
        original = self.catalog.save_recipe("recipe-1", {"code":"R1","name":"分析","price_scale":4,"components":[
            {"catalog_item_id":"mat-1","quantity":"2.5","quantity_scale":4},
            {"catalog_item_id":"lab-1","quantity":"1.25","quantity_scale":4},
        ]})
        self.version = self.operations.create_recipe_version("recipe-1", "baseline", "tester")
        self.catalog.save_recipe("recipe-1", {"code":"R1","name":"分析","price_scale":4,"row_version":original["row_version"],"components":[
            {"catalog_item_id":"mat-1","quantity":"9","quantity_scale":4},
        ]})

    def test_apply_restores_historical_analysis_quantities(self):
        current = self.catalog.calculate_recipe("recipe-1")
        result = self.history.apply_rates("recipe-1", self.version["id"], current["row_version"], "tester")
        self.assertEqual(2, result["component_count"])
        self.assertEqual("2.5000", result["applied_components"][0]["quantity"])
        restored = self.catalog.calculate_recipe("recipe-1")
        self.assertEqual(["2.5000", "1.2500"], [item["quantity"] for item in restored["components"]])
        self.assertEqual(current["row_version"] + 1, restored["row_version"])

    def test_stale_version_rolls_back_without_partial_components(self):
        current = self.catalog.calculate_recipe("recipe-1")
        with self.assertRaises(RuntimeError):
            self.history.apply_rates("recipe-1", self.version["id"], current["row_version"] - 1, "tester")
        unchanged = self.catalog.calculate_recipe("recipe-1")
        self.assertEqual(1, len(unchanged["components"]))
        self.assertEqual("9.0000", unchanged["components"][0]["quantity"])

    def test_version_must_belong_to_recipe(self):
        current = self.catalog.calculate_recipe("recipe-1")
        with self.assertRaises(LookupError):
            self.history.apply_rates("recipe-1", "missing", current["row_version"], "tester")


if __name__ == "__main__":
    unittest.main()
