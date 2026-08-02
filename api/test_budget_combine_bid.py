from __future__ import annotations

import unittest
from sqlalchemy import create_engine

from api.budget_combine_bid import BudgetCombineBidService, combine_sources


class BudgetCombineBidTests(unittest.TestCase):
    def sources(self):
        return [
            {"project_code": "B1", "items": [{"id": "A", "code": "x", "name": "Concrete", "unit": "M3", "quantity": "2", "unit_price": "10", "amount": "20"}]},
            {"project_code": "B2", "items": [{"id": "B", "code": "X", "name": "Concrete", "unit": "M3", "quantity": "3", "unit_price": "10", "amount": "30"}]},
        ]

    def test_block_strategy_never_silently_overwrites(self):
        result = combine_sources(self.sources(), "BLOCK")
        self.assertEqual("BLOCKED", result["status"])
        self.assertEqual(1, len(result["items"]))
        self.assertEqual("B1", result["items"][0]["source_project_code"])

    def test_sum_quantity_requires_compatible_identity_and_price(self):
        result = combine_sources(self.sources(), "SUM_QUANTITY")
        self.assertEqual("READY", result["status"])
        self.assertEqual("5", result["items"][0]["quantity"])
        self.assertEqual("50", result["items"][0]["amount"])
        bad = self.sources()
        bad[1]["items"][0]["unit_price"] = "11"
        result = combine_sources(bad, "SUM_QUANTITY")
        self.assertEqual("BLOCKED", result["status"])
        self.assertEqual("BLOCKED_INCOMPATIBLE_SUM", result["conflicts"][0]["resolution"])

    def test_keep_and_rename_strategies_are_explicit(self):
        self.assertEqual("B1", combine_sources(self.sources(), "KEEP_FIRST")["items"][0]["source_project_code"])
        self.assertEqual("B2", combine_sources(self.sources(), "KEEP_LAST")["items"][0]["source_project_code"])
        renamed = combine_sources(self.sources(), "RENAME")
        self.assertEqual(["X", "X-2"], [x["code"] for x in renamed["items"]])

    def test_session_persists_conflict_audit(self):
        engine = create_engine("sqlite+pysqlite:///:memory:", future=True)
        service = BudgetCombineBidService(engine)
        item = service.create({"target_project_code": "T", "strategy": "BLOCK", "sources": self.sources()}, "u1")
        self.assertEqual("BLOCKED", item["status"])
        self.assertEqual(1, len(item["conflicts"]))
        self.assertIn("session=", item["deep_link"])


if __name__ == "__main__":
    unittest.main()
