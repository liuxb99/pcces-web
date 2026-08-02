import unittest

from sqlalchemy import create_engine
from sqlalchemy.pool import StaticPool

from api.cost_structure import CostStructureService


class CostStructureServiceTest(unittest.TestCase):
    def setUp(self):
        self.engine = create_engine("sqlite+pysqlite://", connect_args={"check_same_thread": False}, poolclass=StaticPool)
        self.service = CostStructureService(self.engine)

    def test_catalog_and_project_assignment(self):
        created = self.service.save_type("standard", {
            "code": " cs-01 ", "name": "公共工程標準成本結構", "source": "legacy",
            "version": "2026.1", "enabled": True, "row_version": 0,
        }, "u1")
        self.assertEqual("CS-01", created["code"])
        assigned = self.service.assign_project("P100", "standard", "bud", 0, "u1")
        self.assertEqual("BUD", assigned["issue"])
        self.assertEqual("CS-01", assigned["type_code"])
        self.assertIn("project=P100", assigned["deep_link"])

    def test_disabled_type_cannot_be_assigned(self):
        self.service.save_type("disabled", {
            "code": "CS-X", "name": "停用結構", "enabled": False, "row_version": 0,
        }, "u1")
        with self.assertRaises(LookupError):
            self.service.assign_project("P100", "disabled", "BUD", 0, "u1")

    def test_optimistic_locking_and_issue_validation(self):
        created = self.service.save_type("standard", {
            "code": "CS-01", "name": "標準", "enabled": True, "row_version": 0,
        }, "u1")
        with self.assertRaises(RuntimeError):
            self.service.save_type("standard", {
                "code": "CS-01", "name": "錯誤覆蓋", "enabled": True, "row_version": 0,
            }, "u2")
        assigned = self.service.assign_project("P100", "standard", "BUD", 0, "u1")
        with self.assertRaises(RuntimeError):
            self.service.assign_project("P100", "standard", "BID", 0, "u2")
        changed = self.service.assign_project("P100", "standard", "BID", assigned["row_version"], "u2")
        self.assertEqual("BID", changed["issue"])
        with self.assertRaises(ValueError):
            self.service.assign_project("P200", created["id"], "CNT", 0, "u1")


if __name__ == "__main__":
    unittest.main()
