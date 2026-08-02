import unittest
from sqlalchemy import create_engine
from api.conversion_wizard import ConversionWizardService, build_preflight_report


class ConversionWizardTests(unittest.TestCase):
    def setUp(self):
        self.engine = create_engine("sqlite:///:memory:")
        self.service = ConversionWizardService(self.engine)

    def test_preflight_blocks_errors_and_preserves_warnings(self):
        report = build_preflight_report([
            {"id": "1", "code": "A", "name": "項目", "quantity": "1", "unit_price": "2"},
            {"id": "1", "code": "A", "name": "", "quantity": None, "unit_price": None},
        ], "CREATE", {"format": "BID_JSON"})
        self.assertFalse(report["can_continue"])
        self.assertGreaterEqual(report["error_count"], 1)
        self.assertGreaterEqual(report["warning_count"], 3)

    def test_ready_session_persists_options_and_report(self):
        item = self.service.create({
            "source_project_code": "BUD-1", "source_budget_version_id": "V1",
            "target_project_code": "BID-1", "mode": "CREATE",
            "options": {"format": "XML_NEW", "include_resources": False},
            "budget_items": [{"id": "1", "code": "a01", "name": "工程", "quantity": "2", "unit_price": "3"}],
        }, "u1")
        self.assertEqual(item["status"], "READY")
        self.assertTrue(item["can_continue"])
        self.assertEqual(item["options"]["format"], "XML_NEW")
        self.assertIn("session=", item["deep_link"])

    def test_invalid_format_creates_blocked_session(self):
        item = self.service.create({
            "source_project_code": "BUD-1", "source_budget_version_id": "V1",
            "target_project_code": "BID-1", "mode": "CREATE",
            "options": {"format": "BAD"},
            "budget_items": [{"id": "1", "code": "A", "name": "工程", "quantity": "1", "unit_price": "1"}],
        }, "u1")
        self.assertEqual(item["status"], "BLOCKED")
        self.assertFalse(item["can_continue"])


if __name__ == "__main__":
    unittest.main()
