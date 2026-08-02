import unittest
from sqlalchemy import create_engine
from sqlalchemy.pool import StaticPool

from api.budget_cross_project_sync import BudgetCrossProjectSyncService
from api.budget_decimal import BudgetDecimalService
from api.budget_validation import BudgetValidationService


class BudgetCrossProjectSyncTests(unittest.TestCase):
    def setUp(self):
        self.engine = create_engine("sqlite://", connect_args={"check_same_thread": False}, poolclass=StaticPool, future=True)
        self.budget = BudgetDecimalService(self.engine); self.budget.create_schema()
        self.validation = BudgetValidationService(self.engine)
        self.sync = BudgetCrossProjectSyncService(self.engine)

    def save(self, item_id, project, no, quantity, price, row_version=0):
        row, status = self.budget.save(item_id, {
            "project_code": project, "item_no": no, "name": no, "kind": "L",
            "quantity": quantity, "unit_price": price,
            "quantity_scale": 2, "price_scale": 2, "amount_scale": 2,
            "row_version": row_version,
        })
        self.assertLess(status, 400)
        return row

    def test_explicit_reference_propagates_price_and_amount(self):
        self.save("S1", "BUD-P", "001", "1.00", "12.50")
        self.save("T1", "BID-P", "001", "3.00", "0.00")
        self.validation.set_item_class("BUD-P", "S1", "A", "7")
        self.validation.set_item_class("BID-P", "T1", "A", "7")
        self.validation.add_reference("BUD-P", "S1", "BID-P", "T1", "7")
        run = self.sync.propagate("BUD-P", "BID-P", "7")
        self.assertEqual(run["status"], "COMPLETED")
        self.assertEqual(run["result"]["updated_items"], 1)
        target = self.budget.get("T1")
        self.assertEqual(target["unit_price"], "12.50")
        self.assertEqual(target["amount"], "37.50")
        self.assertIn("sync=", run["deep_link"])

    def test_broken_reference_is_reported_not_silently_ignored(self):
        self.save("S1", "BUD-P", "001", "1.00", "12.50")
        self.save("T1", "BID-P", "001", "3.00", "0.00")
        ref = self.validation.add_reference("BUD-P", "S1", "BID-P", "T1", "7")
        with self.engine.begin() as conn:
            from api.budget_decimal import budget_items_decimal
            conn.execute(budget_items_decimal.delete().where(budget_items_decimal.c.id == "S1"))
        run = self.sync.propagate("BUD-P", "BID-P", "7")
        self.assertEqual(run["status"], "COMPLETED_WITH_ERRORS")
        self.assertEqual(run["result"]["broken"][0]["reference_id"], ref["id"])

    def test_mode_diff_reports_added_removed_and_changed(self):
        self.save("L1", "BUD-P", "001", "1.00", "10.00")
        self.save("L2", "BUD-P", "002", "1.00", "5.00")
        self.save("R1", "BID-P", "001", "1.00", "12.00")
        self.save("R3", "BID-P", "003", "1.00", "7.00")
        run = self.sync.diff("BUD-P", "BID-P", "7")
        result = run["result"]
        self.assertEqual(len(result["added"]), 1)
        self.assertEqual(len(result["removed"]), 1)
        self.assertEqual(len(result["changed"]), 1)
        self.assertEqual(result["changed"][0]["item_no"], "001")

    def test_self_check_blocks_bid_zero_price_before_submission(self):
        self.save("T1", "BID-P", "001", "1.00", "0.00")
        self.validation.set_mode("BID-P", "BID", "7")
        self.validation.set_item_class("BID-P", "T1", "A", "7")
        result = self.validation.check("BID-P", "7", True)
        self.assertFalse(result["passed"])
        self.assertIn("BID_PRICE_REQUIRED", {i["code"] for i in result["issues"]})


if __name__ == "__main__":
    unittest.main()
