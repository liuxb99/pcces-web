import pathlib
import unittest


ROOT = pathlib.Path(__file__).resolve().parents[1]


class BidImportApplyCanonicalContractTests(unittest.TestCase):
    def test_web_routes_are_canonical(self):
        text = (ROOT / "api" / "conversion_export_jobs.py").read_text(encoding="utf-8")
        self.assertIn('/import-sessions/<session_id>/apply', text)
        self.assertIn('/import-apply-runs/<run_id>', text)
        self.assertIn('BidBudgetImportApplyService', text)

    def test_go_routes_are_registered(self):
        handler = (ROOT / "pcces-go" / "internal" / "platform" / "httpapi" / "bid_import_apply_handlers.go").read_text(encoding="utf-8")
        registry = (ROOT / "pcces-go" / "internal" / "platform" / "httpapi" / "authorization_handlers.go").read_text(encoding="utf-8")
        repository = (ROOT / "pcces-go" / "internal" / "storage" / "sqlite" / "bid_import_apply.go").read_text(encoding="utf-8")
        self.assertIn('POST /api/conversions/import-sessions/{sessionID}/apply', handler)
        self.assertIn('GET /api/conversions/import-apply-runs/{runID}', handler)
        self.assertIn('s.bidImportApplyRoutes()', registry)
        self.assertIn('BeginTx', repository)
        self.assertIn('APPROVED', repository)
        self.assertIn('REPLACE', repository)
        self.assertIn('APPEND', repository)


if __name__ == "__main__":
    unittest.main()
