import unittest
from sqlalchemy import create_engine

from api.mrs_catalog import MRSCatalogService
from api.mrs_history_apply import MRSHistoryApplyService


class MRSHistoryApplyTests(unittest.TestCase):
    def setUp(self):
        self.engine = create_engine("sqlite+pysqlite:///:memory:", future=True)
        self.catalog = MRSCatalogService(self.engine)
        self.history = MRSHistoryApplyService(self.engine)
        self.catalog.save_item("M1", {"code":"M0000100000","name":"鋼筋","category":"MATERIAL","current_price":"10.0000","price_scale":4,"row_version":0,"effective_date":"2026-01-01"}, "u1")
        current = self.catalog.get_item("M1")
        self.catalog.save_item("M1", {"code":current["code"],"name":current["name"],"category":current["category"],"current_price":"12.5000","price_scale":4,"row_version":current["row_version"],"effective_date":"2026-02-01"}, "u1")

    def test_apply_historical_price_creates_trace_event(self):
        rows = self.catalog.history("M1")
        oldest = rows[-1]
        current = self.catalog.get_item("M1")
        result = self.history.apply_price("M1", oldest["id"], current["row_version"], "u2")
        self.assertEqual("10.0000", result["new_price"])
        self.assertEqual(current["row_version"] + 1, result["row_version"])
        item = self.catalog.get_item("M1")
        self.assertEqual("10.0000", item["current_price"])
        newest = self.catalog.history("M1")[0]
        self.assertEqual(f"HISTORY_APPLY:{oldest['id']}", newest["source"])

    def test_stale_version_and_foreign_history_are_rejected(self):
        rows = self.catalog.history("M1")
        with self.assertRaises(RuntimeError):
            self.history.apply_price("M1", rows[-1]["id"], 0, "u2")
        with self.assertRaises(LookupError):
            self.history.apply_price("M1", "missing", self.catalog.get_item("M1")["row_version"], "u2")


if __name__ == "__main__":
    unittest.main()
