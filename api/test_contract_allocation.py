import json
import unittest
from sqlalchemy import create_engine

from api.budget_versioning import budget_versions, metadata as version_metadata
from api.contract_core import ContractCoreService
from api.contract_allocation import ContractAllocationService


class ContractAllocationTests(unittest.TestCase):
    def setUp(self):
        self.engine = create_engine("sqlite+pysqlite:///:memory:", future=True)
        version_metadata.create_all(self.engine)
        self.core = ContractCoreService(self.engine)
        self.alloc = ContractAllocationService(self.engine)
        snapshot = [{"id":"B1","item_no":"001","name":"Concrete","unit":"m3","quantity":"10","unit_price":"100","amount":"1000"}]
        with self.engine.begin() as conn:
            conn.execute(budget_versions.insert().values(id="V1",project_code="P1",label="approved",status="APPROVED",snapshot_json=json.dumps(snapshot),created_by="u",created_at="2026-01-01T00:00:00Z"))

    def _contract(self, no, qty="4", amount="400"):
        return self.core.create({"project_code":"P1","budget_version_id":"V1","contract_no":no,"name":no,"contract_amount":amount,"items":[{"source_budget_item_id":"B1","name":"Concrete","unit":"m3","quantity":qty,"unit_price":"100","amount":amount}]}, "u")

    def test_basis_tracks_allocated_and_remaining(self):
        c = self._contract("C1")
        row = self.alloc.basis(c["id"])["items"][0]
        self.assertEqual(DecimalText(row["allocated_quantity"]), "4")
        self.assertEqual(DecimalText(row["remaining_amount"]), "600")

    def test_rejects_over_allocation_across_contracts(self):
        self._contract("C1", "7", "700")
        c2 = self.core.create({"project_code":"P1","budget_version_id":"V1","contract_no":"C2","name":"C2","contract_amount":"0","items":[{"source_budget_item_id":"X","name":"placeholder","quantity":"0","unit_price":"0","amount":"0"}]}, "u")
        with self.assertRaises(ArithmeticError):
            self.alloc.add_items(c2["id"], {"row_version":1,"items":[{"source_budget_item_id":"B1","quantity":"4","unit_price":"100","amount":"400"}]}, "u")

    def test_subcontract_requires_same_baseline(self):
        parent = self._contract("MAIN", "2", "200")
        child = self.core.create({"project_code":"P1","budget_version_id":"V1","contract_no":"SUB","name":"SUB","contract_amount":"0","items":[{"source_budget_item_id":"X","name":"placeholder","quantity":"0","unit_price":"0","amount":"0"}]}, "u")
        link = self.alloc.link_subcontract(parent["id"], child["id"], "u")
        self.assertEqual(link["parent_contract_id"], parent["id"])
        with self.assertRaises(RuntimeError):
            self.alloc.link_subcontract(parent["id"], child["id"], "u")


def DecimalText(value):
    return str(float(value)).rstrip("0").rstrip(".")


if __name__ == "__main__":
    unittest.main()
