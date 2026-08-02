"""Permanent contract checks for Phase 4 reverse-import HTTP activation."""
from pathlib import Path
import unittest


class BidImportCanonicalContractTest(unittest.TestCase):
    def test_web_routes_are_mounted_on_canonical_conversion_blueprint(self):
        source = Path("api/conversion_export_jobs.py").read_text(encoding="utf-8")
        for route in (
            '@bp.post("/import-preflight")',
            '@bp.post("/import-sessions")',
            '@bp.get("/import-sessions/<session_id>")',
        ):
            self.assertIn(route, source)
        self.assertIn("BidBudgetRoundTripService", source)

    def test_local_go_routes_are_registered(self):
        handler = Path("pcces-go/internal/platform/httpapi/bid_import_roundtrip_handlers.go").read_text(encoding="utf-8")
        registry = Path("pcces-go/internal/platform/httpapi/authorization_handlers.go").read_text(encoding="utf-8")
        for route in (
            "POST /api/conversions/import-preflight",
            "POST /api/conversions/import-sessions",
            "GET /api/conversions/import-sessions/{sessionID}",
        ):
            self.assertIn(route, handler)
        self.assertIn("s.bidImportRoundTripRoutes()", registry)

    def test_session_persistence_and_lineage_are_not_stubs(self):
        repository = Path("pcces-go/internal/storage/sqlite/bid_import_sessions.go").read_text(encoding="utf-8")
        self.assertIn("INSERT INTO bid_import_sessions", repository)
        self.assertIn('"round_trip_lineage"', repository)
        self.assertIn('"item_links"', repository)


if __name__ == "__main__":
    unittest.main()
