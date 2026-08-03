import unittest
from datetime import datetime, timezone
from sqlalchemy import create_engine

from api.budget_versioning import metadata as budget_metadata, budget_versions
from api.contract_core import ContractCoreService
from api.contract_governance import ContractGovernanceService


class ContractGovernanceTests(unittest.TestCase):
    def setUp(self):
        self.engine = create_engine("sqlite:///:memory:")
        budget_metadata.create_all(self.engine)
        with self.engine.begin() as conn:
            conn.execute(budget_versions.insert().values(id="V1", project_code="P1", label="A", status="APPROVED", snapshot_json='[{"id":"B1","quantity":"2","amount":"20"}]', created_by="u", created_at=datetime(2026, 8, 2, tzinfo=timezone.utc)))
        self.core = ContractCoreService(self.engine)
        self.gov = ContractGovernanceService(self.engine)
        self.contract = self.core.create({"project_code":"P1","budget_version_id":"V1","contract_no":"C1","name":"Main","contract_amount":"20","items":[{"source_budget_item_id":"B1","name":"Concrete","quantity":"2","unit_price":"10","amount":"20"}]}, "u")

    def test_version_submit_approve_lock(self):
        version = self.gov.create_version(self.contract["id"], {"row_version":1,"note":"baseline"}, "u")
        self.assertEqual(version["status"], "DRAFT")
        version = self.gov.transition(version["id"], {"row_version":1,"status":"SUBMITTED"}, "reviewer")
        version = self.gov.transition(version["id"], {"row_version":2,"status":"APPROVED"}, "approver")
        self.assertEqual(version["approved_by"], "approver")
        version = self.gov.transition(version["id"], {"row_version":3,"status":"LOCKED"}, "approver")
        self.assertEqual(version["status"], "LOCKED")
        with self.assertRaises(PermissionError):
            self.gov.create_version(self.contract["id"], {"row_version":3}, "u")

    def test_invalid_transition_and_row_version(self):
        version = self.gov.create_version(self.contract["id"], {"row_version":1}, "u")
        with self.assertRaises(ValueError):
            self.gov.transition(version["id"], {"row_version":1,"status":"APPROVED"}, "u")
        with self.assertRaises(RuntimeError):
            self.gov.transition(version["id"], {"row_version":9,"status":"SUBMITTED"}, "u")


if __name__ == "__main__":
    unittest.main()
