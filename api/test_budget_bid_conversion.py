import unittest
from sqlalchemy import create_engine
from api.budget_bid_conversion import BudgetBidConversionService

class BudgetBidConversionTests(unittest.TestCase):
    def setUp(self):
        self.service = BudgetBidConversionService(create_engine("sqlite+pysqlite:///:memory:"))

    def test_create_session_and_lineage(self):
        item = self.service.convert({
            "source_project_code":"BUD-1","source_budget_version_id":"V3",
            "target_bid_project_code":"BID-1","mode":"CREATE",
            "budget_items":[{"id":"10","code":"a01","name":"工程","quantity":"2","unit_price":"5","amount":"10"}],
            "options":{"include_resources":True},
        }, "u1")
        self.assertEqual(item["status"], "COMPLETED")
        self.assertEqual(item["result_snapshot"][0]["code"], "A01")
        self.assertEqual(item["lineage"]["source_budget_version_id"], "V3")
        self.assertIn("session=", item["deep_link"])

    def test_create_conflict_and_atomic_validation(self):
        body={"source_project_code":"B","source_budget_version_id":"V1","target_bid_project_code":"X","budget_items":[{"id":"1"}]}
        self.service.convert(body,"u")
        with self.assertRaises(RuntimeError): self.service.convert(body,"u")
        with self.assertRaises(ValueError): self.service.convert({**body,"target_bid_project_code":"Y","budget_items":[{"id":"1"},{"id":"1"}]},"u")

if __name__ == "__main__": unittest.main()
