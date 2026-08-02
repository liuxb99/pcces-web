from pathlib import Path
import unittest


class BudgetCombineBidContractTests(unittest.TestCase):
    def test_web_and_go_routes_are_registered(self):
        web = Path("api/conversion_export_jobs.py").read_text(encoding="utf-8")
        go_handler = Path("pcces-go/internal/platform/httpapi/combine_bid_handlers.go").read_text(encoding="utf-8")
        registry = Path("pcces-go/internal/platform/httpapi/authorization_handlers.go").read_text(encoding="utf-8")
        for route in ("/combine-bid/preflight", "/combine-bid/sessions"):
            self.assertIn(route, web)
            self.assertIn(route, go_handler)
        self.assertIn("combineBidRoutes()", registry)

    def test_no_silent_overwrite_contract(self):
        py = Path("api/budget_combine_bid.py").read_text(encoding="utf-8")
        go = Path("pcces-go/internal/storage/sqlite/combine_bid.go").read_text(encoding="utf-8")
        for token in ("BLOCK", "KEEP_FIRST", "KEEP_LAST", "SUM_QUANTITY", "RENAME", "BLOCKED_INCOMPATIBLE_SUM"):
            self.assertIn(token, py)
            self.assertIn(token, go)


if __name__ == "__main__":
    unittest.main()
