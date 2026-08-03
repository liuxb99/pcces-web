import unittest
from datetime import datetime, timezone
from sqlalchemy import create_engine

from api.budget_versioning import metadata as budget_metadata, budget_versions
from api.contract_core import ContractCoreService
from api.contract_execution import ContractExecutionService
from api.contract_governance import ContractGovernanceService

class ContractExecutionTest(unittest.TestCase):
    def setUp(self):
        self.engine=create_engine("sqlite:///:memory:")
        budget_metadata.create_all(self.engine)
        with self.engine.begin() as conn: conn.execute(budget_versions.insert().values(id="V1",project_code="P1",label="A",status="APPROVED",snapshot_json='[{"id":"B1"}]',created_by="u",created_at=datetime(2026, 8, 2, tzinfo=timezone.utc)))
        core=ContractCoreService(self.engine);gov=ContractGovernanceService(self.engine)
        self.contract=core.create({"project_code":"P1","budget_version_id":"V1","contract_no":"C1","name":"Main","contract_amount":"100","items":[{"source_budget_item_id":"B1","name":"Concrete","quantity":"10","unit_price":"10","amount":"100"}]},"u")
        v=gov.create_version(self.contract["id"],{"row_version":1},"u");v=gov.transition(v["id"],{"status":"SUBMITTED","row_version":1},"u");gov.transition(v["id"],{"status":"APPROVED","row_version":2},"a")
        self.svc=ContractExecutionService(self.engine);self.item=self.contract["items"][0]["id"]

    def test_full_execution_chain(self):
        p=self.svc.create_invoice(self.contract["id"],{"items":[{"contract_item_id":self.item,"current_quantity":"5"}],"deduction":"5","retention":"5"},"u")
        self.assertEqual(p["net_payable"],"40.00000000")
        p=self.svc.transition_invoice(p["id"],{"status":"SUBMITTED","row_version":1},"u")
        p=self.svc.transition_invoice(p["id"],{"status":"APPROVED","row_version":2},"a")
        s=self.svc.create_settlement(self.contract["id"],{"final_adjustment":"10"},"u")
        self.assertEqual(s["final_amount"],"60.00000000")
        s=self.svc.transition_settlement(s["id"],{"status":"SUBMITTED","row_version":1},"u")
        s=self.svc.transition_settlement(s["id"],{"status":"APPROVED","row_version":2},"a")
        s=self.svc.transition_settlement(s["id"],{"status":"COMPLETED","row_version":3},"a")
        a=self.svc.create_acceptance(self.contract["id"],{"inspection_date":"2026-08-02","result":"PASS","defects":[]},"u")
        a=self.svc.transition_acceptance(a["id"],{"status":"INSPECTED","row_version":1},"u")
        a=self.svc.transition_acceptance(a["id"],{"status":"COMPLETED","row_version":2},"a")
        a=self.svc.transition_acceptance(a["id"],{"status":"ARCHIVED","row_version":3},"a")
        self.assertEqual(a["status"],"ARCHIVED")

    def test_cumulative_quantity_guard(self):
        p=self.svc.create_invoice(self.contract["id"],{"items":[{"contract_item_id":self.item,"current_quantity":"8"}]},"u")
        p=self.svc.transition_invoice(p["id"],{"status":"SUBMITTED","row_version":1},"u");self.svc.transition_invoice(p["id"],{"status":"APPROVED","row_version":2},"a")
        with self.assertRaises(ValueError): self.svc.create_invoice(self.contract["id"],{"items":[{"contract_item_id":self.item,"current_quantity":"3"}]},"u")

if __name__=="__main__":unittest.main()
