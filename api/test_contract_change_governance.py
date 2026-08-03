import unittest
from datetime import datetime, timezone
from sqlalchemy import create_engine

from api.budget_versioning import metadata as budget_metadata, budget_versions
from api.contract_core import ContractCoreService
from api.contract_governance import ContractGovernanceService


class ContractChangeGovernanceTest(unittest.TestCase):
    def setUp(self):
        self.engine=create_engine("sqlite:///:memory:")
        budget_metadata.create_all(self.engine)
        with self.engine.begin() as conn:
            conn.execute(budget_versions.insert().values(id="V1",project_code="P1",label="Approved",status="APPROVED",snapshot_json='[{"id":"B1","quantity":"10","amount":"100"}]',created_by="u",created_at=datetime(2026, 8, 2, tzinfo=timezone.utc)))
        self.core=ContractCoreService(self.engine)
        self.gov=ContractGovernanceService(self.engine)
        self.contract=self.core.create({"project_code":"P1","budget_version_id":"V1","contract_no":"C1","name":"Main","contract_amount":"100","items":[{"source_budget_item_id":"B1","name":"Concrete","quantity":"10","unit_price":"10","amount":"100"}]},"u")
        version=self.gov.create_version(self.contract["id"],{"row_version":1},"u")
        version=self.gov.transition(version["id"],{"status":"SUBMITTED","row_version":1},"u")
        version=self.gov.transition(version["id"],{"status":"APPROVED","row_version":2},"approver")

    def test_change_is_applied_only_after_approval(self):
        item_id=self.core.get(self.contract["id"])["items"][0]["id"]
        case=self.gov.change_cases.create(self.contract["id"],{"change_no":"CO-1","reason":"scope","items":[{"action":"INCREASE","contract_item_id":item_id,"quantity_delta":"2","amount_delta":"20","unit_price":"10"}]},"u")
        self.assertEqual(case["status"],"DRAFT")
        self.assertEqual(self.core.get(self.contract["id"])["contract_amount"],"100.00000000")
        case=self.gov.change_cases.transition(case["id"],{"status":"SUBMITTED","row_version":1},"u")
        case=self.gov.change_cases.transition(case["id"],{"status":"APPROVED","row_version":2},"approver")
        case=self.gov.change_cases.transition(case["id"],{"status":"APPLIED","row_version":3},"operator")
        self.assertEqual(case["status"],"APPLIED")
        self.assertEqual(self.core.get(self.contract["id"])["contract_amount"],"120.00000000")
        self.assertEqual(case["applied_by"],"operator")

    def test_invalid_transition_rejected(self):
        case=self.gov.change_cases.create(self.contract["id"],{"change_no":"CO-2","reason":"scope","items":[{"action":"ADD","source_budget_item_id":"B2","name":"Steel","quantity_delta":"1","amount_delta":"5","unit_price":"5"}]},"u")
        with self.assertRaises(ValueError): self.gov.change_cases.transition(case["id"],{"status":"APPLIED","row_version":1},"u")

if __name__ == "__main__": unittest.main()
