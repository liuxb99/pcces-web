import unittest
from sqlalchemy import create_engine

from api.budget_versioning import metadata as budget_metadata, budget_versions
from api.contract_core import ContractCoreService, metadata as contract_metadata, contracts_v2
from api.contract_changes import ContractChangeService


class ContractChangeTests(unittest.TestCase):
    def setUp(self):
        self.engine=create_engine("sqlite:///:memory:")
        budget_metadata.create_all(self.engine); contract_metadata.create_all(self.engine)
        with self.engine.begin() as conn:
            conn.execute(budget_versions.insert().values(id="V1",project_code="P1",label="approved",status="APPROVED",snapshot_json='[{"id":"B1","quantity":"10","amount":"100"}]',created_by="u",created_at="2026-08-02T00:00:00Z"))
        self.core=ContractCoreService(self.engine)
        self.contract=self.core.create({"project_code":"P1","budget_version_id":"V1","contract_no":"C-1","name":"Main","contract_amount":"100","items":[{"source_budget_item_id":"B1","name":"Concrete","quantity":"10","unit_price":"10","amount":"100"}]},"u")
        with self.engine.begin() as conn: conn.execute(contracts_v2.update().where(contracts_v2.c.id==self.contract["id"]).values(status="APPROVED"))
        self.service=ContractChangeService(self.engine)

    def test_add_and_decrease_recalculate_amount(self):
        item_id=self.contract["items"][0]["id"]
        result=self.service.create(self.contract["id"],{"change_no":"CH-1","reason":"design change","items":[{"action":"ADD","name":"Drainage","quantity_delta":"2","unit_price":"5"},{"action":"DECREASE","contract_item_id":item_id,"quantity_delta":"1","unit_price":"10"}]},"u")
        self.assertEqual(result["delta_amount"],"0E-8")
        self.assertEqual(result["after_amount"],"100.00000000")

    def test_requires_approved_contract(self):
        with self.engine.begin() as conn: conn.execute(contracts_v2.update().where(contracts_v2.c.id==self.contract["id"]).values(status="DRAFT"))
        with self.assertRaises(PermissionError): self.service.create(self.contract["id"],{"change_no":"CH-2","reason":"x","items":[{"action":"ADD","name":"x","amount_delta":"1"}]},"u")


if __name__=="__main__": unittest.main()
