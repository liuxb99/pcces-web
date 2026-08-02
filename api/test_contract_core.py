import unittest
from sqlalchemy import create_engine
from sqlalchemy.pool import StaticPool

from api.budget_versioning import metadata as version_metadata, budget_versions
from api.contract_core import ContractCoreService


class ContractCoreTest(unittest.TestCase):
    def setUp(self):
        self.engine = create_engine("sqlite://", connect_args={"check_same_thread": False}, poolclass=StaticPool)
        version_metadata.create_all(self.engine)
        self.service = ContractCoreService(self.engine)
        with self.engine.begin() as conn:
            conn.execute(budget_versions.insert().values(id="V1", project_code="P1", label="Approved", status="APPROVED", snapshot_json="[]", created_by="u", created_at="2026-08-02T00:00:00Z"))
            conn.execute(budget_versions.insert().values(id="V2", project_code="P1", label="Draft", status="DRAFT", snapshot_json="[]", created_by="u", created_at="2026-08-02T00:00:00Z"))

    def test_approved_version_is_eligible(self):
        self.assertTrue(self.service.eligibility("P1", "V1")["eligible"])

    def test_draft_version_is_not_eligible(self):
        self.assertIn("BUDGET_VERSION_NOT_APPROVED", self.service.eligibility("P1", "V2")["reasons"])

    def test_create_preserves_budget_lineage(self):
        result = self.service.create({"project_code":"P1","budget_version_id":"V1","contract_no":"C-001","name":"Main Contract","items":[{"source_budget_item_id":"B1","item_no":"001","name":"Concrete","unit":"m3","quantity":"2","unit_price":"10","amount":"20"}]}, "actor")
        self.assertEqual(result["contract_amount"], "20.00000000")
        self.assertEqual(result["items"][0]["source_budget_item_id"], "B1")
        self.assertIn("item=B1", result["items"][0]["deep_link"])

    def test_total_mismatch_rolls_back(self):
        with self.assertRaises(ValueError):
            self.service.create({"project_code":"P1","budget_version_id":"V1","contract_no":"C-002","name":"Bad","contract_amount":"99","items":[{"source_budget_item_id":"B1","name":"Concrete","quantity":"2","unit_price":"10","amount":"20"}]}, "actor")
        with self.engine.connect() as conn:
            self.assertEqual(conn.exec_driver_sql("select count(*) from contracts_v2").scalar_one(), 0)


if __name__ == "__main__":
    unittest.main()
