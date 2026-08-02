import unittest
from pathlib import Path


class MRSGovernanceFrontendContractTests(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.source = Path("web-pcces/frontend/src/pages/MrsGovernancePage.tsx").read_text(encoding="utf-8")

    def test_frontend_consumes_paged_release_and_audit_contracts(self):
        self.assertIn("Page<CatalogRelease>", self.source)
        self.assertIn("Page<GovernanceAudit>", self.source)
        self.assertIn("limit: PAGE_SIZE", self.source)
        self.assertIn("offset: releaseOffset", self.source)
        self.assertIn("offset: auditOffset", self.source)
        self.assertIn("releasePage.total", self.source)
        self.assertIn("auditPage.total", self.source)

    def test_frontend_exposes_legacy_governance_filters(self):
        self.assertIn("status: releaseStatus || undefined", self.source)
        self.assertIn("resource_type: auditResourceType || undefined", self.source)
        self.assertIn("resource_id: auditResourceId || undefined", self.source)
        self.assertIn("event_type: auditEventType || undefined", self.source)
        self.assertIn("全部狀態", self.source)

    def test_mutations_read_current_row_version_before_update(self):
        self.assertIn("/validity`, { headers }", self.source)
        self.assertIn("row_version: current.data.row_version", self.source)
        self.assertIn("/freeze`, { headers }", self.source)
        self.assertNotIn("row_version: 0 }, { headers });", self.source)

    def test_returned_release_can_be_resubmitted_and_errors_are_visible(self):
        self.assertIn("release.status === 'RETURNED'", self.source)
        self.assertIn("重新送審", self.source)
        self.assertIn('role="alert"', self.source)
        self.assertIn("disabled={busy}", self.source)


if __name__ == "__main__":
    unittest.main()
