import json
import unittest
from sqlalchemy import create_engine

from api.bid_budget_import_apply import BidBudgetImportApplyService
from api.bid_budget_roundtrip import BidBudgetRoundTripService
from api.budget_decimal import BudgetDecimalService
from api.budget_versioning import BudgetVersionService, budget_versions


class BidBudgetImportApplyTests(unittest.TestCase):
    def setUp(self):
        self.engine = create_engine("sqlite+pysqlite:///:memory:", future=True)
        BudgetDecimalService(self.engine).create_schema()
        self.versions = BudgetVersionService(self.engine)
        self.roundtrip = BidBudgetRoundTripService(self.engine)
        self.service = BidBudgetImportApplyService(self.engine)

    def make_session(self, target="P1", code="A-01"):
        payload = json.dumps({"project_code":"BID-1","items":[{"id":"SRC-1","source_budget_item_id":"SRC-1","code":code,"name":"Concrete","quantity":"2","unit_price":"10","amount":"20"}]})
        return self.roundtrip.create({"target_budget_project_code":target,"payload":payload,"format":"BID_JSON"}, "tester")

    def make_version(self, project="P1", status="DRAFT"):
        return self.versions.create_version(project, "target", "tester", status)

    def test_create_writes_items_and_lineage(self):
        session = self.make_session()
        version = self.make_version()
        result = self.service.apply(session["id"], {"mode":"CREATE","target_budget_version_id":version["id"]}, "tester")
        self.assertEqual(result["inserted_count"], 1)
        self.assertEqual(result["lineage"][0]["source_budget_item_id"], "SRC-1")
        self.assertEqual(len(BudgetDecimalService(self.engine).list_project("P1")), 1)

    def test_replace_replaces_existing_items(self):
        budget = BudgetDecimalService(self.engine)
        budget.save("OLD", {"project_code":"P1","item_no":"OLD","name":"Old","kind":"L","quantity":"1","unit_price":"1"})
        session = self.make_session()
        version = self.make_version()
        result = self.service.apply(session["id"], {"mode":"REPLACE","target_budget_version_id":version["id"]}, "tester")
        self.assertEqual(result["replaced_count"], 1)
        self.assertEqual(BudgetDecimalService(self.engine).list_project("P1")[0]["item_no"], "A-01")

    def test_append_skips_duplicate_codes(self):
        budget = BudgetDecimalService(self.engine)
        budget.save("OLD", {"project_code":"P1","item_no":"A-01","name":"Old","kind":"L","quantity":"1","unit_price":"1"})
        session = self.make_session()
        version = self.make_version()
        result = self.service.apply(session["id"], {"mode":"APPEND","target_budget_version_id":version["id"]}, "tester")
        self.assertEqual(result["skipped_count"], 1)
        self.assertEqual(result["inserted_count"], 0)

    def test_approved_version_is_read_only_and_rolls_back(self):
        session = self.make_session()
        version = self.make_version(status="APPROVED")
        with self.assertRaises(PermissionError):
            self.service.apply(session["id"], {"mode":"CREATE","target_budget_version_id":version["id"]}, "tester")
        self.assertEqual(BudgetDecimalService(self.engine).list_project("P1"), [])

    def test_cross_project_version_is_rejected(self):
        session = self.make_session("P1")
        version = self.make_version("P2")
        with self.assertRaises(ValueError):
            self.service.apply(session["id"], {"mode":"CREATE","target_budget_version_id":version["id"]}, "tester")


if __name__ == "__main__":
    unittest.main()
