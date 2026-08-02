import unittest
from sqlalchemy import create_engine
from sqlalchemy.pool import StaticPool
from api.cost_structure_run_versions import CostStructureRunVersionService


class CostStructureRunVersionTests(unittest.TestCase):
    def setUp(self):
        self.engine = create_engine("sqlite://", connect_args={"check_same_thread": False}, poolclass=StaticPool)
        self.service = CostStructureRunVersionService(self.engine)

    def test_links_draft_run_and_compares_versions(self):
        self.service.link("P1", "R1", "BV1", "DRAFT", "100", "110", {"order": ["A"]}, "u1")
        self.service.link("P1", "R2", "BV2", "DRAFT", "120", "135", {"order": ["A", "TAX"]}, "u1")
        diff = self.service.compare("R1", "R2")
        self.assertEqual(diff["direct_cost_delta"], "20")
        self.assertEqual(diff["total_cost_delta"], "25")
        self.assertEqual(self.service.get("R1")["budget_version_id"], "BV1")

    def test_rejects_approved_version_and_duplicate_run(self):
        with self.assertRaises(PermissionError):
            self.service.link("P1", "R0", "BV0", "APPROVED", "1", "1", {}, "u1")
        self.service.link("P1", "R1", "BV1", "DRAFT", "1", "1", {}, "u1")
        with self.assertRaises(RuntimeError):
            self.service.link("P1", "R1", "BV2", "DRAFT", "2", "2", {}, "u1")

    def test_failed_link_leaves_no_partial_row(self):
        with self.assertRaises(Exception):
            self.service.link("P1", "BAD", "BV1", "DRAFT", "not-number", "2", {}, "u1")
        with self.assertRaises(LookupError):
            self.service.get("BAD")

    def test_cross_project_compare_is_rejected(self):
        self.service.link("P1", "R1", "BV1", "DRAFT", "1", "2", {}, "u1")
        self.service.link("P2", "R2", "BV2", "DRAFT", "1", "3", {}, "u1")
        with self.assertRaises(ValueError):
            self.service.compare("R1", "R2")


if __name__ == "__main__":
    unittest.main()
