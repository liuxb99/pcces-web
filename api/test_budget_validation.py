import unittest
from sqlalchemy import create_engine
from sqlalchemy.pool import StaticPool

from api.budget_decimal import BudgetDecimalService
from api.budget_validation import BudgetValidationService


class BudgetValidationTests(unittest.TestCase):
    def setUp(self):
        self.engine=create_engine("sqlite://",connect_args={"check_same_thread":False},poolclass=StaticPool,future=True)
        self.budget=BudgetDecimalService(self.engine); self.budget.create_schema()
        self.service=BudgetValidationService(self.engine)

    def save(self,item_id,project="P1",item_no=None,price="10.00",parent=None):
        row,status=self.budget.save(item_id,{"project_code":project,"parent_id":parent,"item_no":item_no or item_id,"name":item_id,"kind":"L","quantity":"2.00","unit_price":price,"quantity_scale":2,"price_scale":2,"amount_scale":2,"row_version":0})
        self.assertLess(status,400); return row

    def test_mode_and_item_class_use_optimistic_versions(self):
        self.assertEqual(self.service.mode("P1")["mode"],"BUD")
        first=self.service.set_mode("P1","BID","7","0")
        self.assertEqual(first["row_version"],"1")
        with self.assertRaises(RuntimeError): self.service.set_mode("P1","BUD","7","0")
        self.save("I1")
        item=self.service.set_item_class("P1","I1","A","7","0")
        self.assertEqual(item["item_class"],"A")

    def test_bid_check_blocks_missing_prices_and_classes(self):
        self.save("I1",price="0.00")
        self.service.set_mode("P1","BID","7","0")
        self.service.set_item_class("P1","I1","A","7","0")
        result=self.service.check("P1","7")
        self.assertFalse(result["passed"])
        self.assertIn("BID_PRICE_REQUIRED",[i["code"] for i in result["issues"]])
        self.assertIn("check=",result["deep_link"])

    def test_duplicate_numbers_and_broken_parent_are_blocking(self):
        self.save("I1",item_no="100")
        self.save("I2",item_no="100",parent="MISSING")
        self.service.set_item_class("P1","I1","A","7")
        self.service.set_item_class("P1","I2","B","7")
        result=self.service.check("P1","7")
        codes=[i["code"] for i in result["issues"]]
        self.assertIn("DUPLICATE_ITEM_NO",codes)
        self.assertIn("BROKEN_PARENT",codes)

    def test_cross_project_reference_is_explicit_and_idempotent(self):
        self.save("S1","P1"); self.save("T1","P2")
        first=self.service.add_reference("P1","S1","P2","T1","7")
        second=self.service.add_reference("P1","S1","P2","T1","7")
        self.assertFalse(first.get("duplicate",False))
        self.assertTrue(second["duplicate"])
        with self.assertRaises(ValueError): self.service.add_reference("P1","S1","P1","S1","7")

if __name__=="__main__": unittest.main()
