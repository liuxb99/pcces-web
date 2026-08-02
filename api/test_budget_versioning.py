import unittest
from decimal import Decimal
from sqlalchemy import create_engine
from sqlalchemy.pool import StaticPool

from api.budget_decimal import BudgetDecimalService
from api.budget_versioning import BudgetVersionService


class BudgetVersioningTests(unittest.TestCase):
    def setUp(self):
        self.engine = create_engine("sqlite://", connect_args={"check_same_thread":False}, poolclass=StaticPool, future=True)
        self.budget = BudgetDecimalService(self.engine); self.budget.create_schema()
        self.versions = BudgetVersionService(self.engine)

    def save(self, item_id, price, row_version=0):
        result,status=self.budget.save(item_id,{"project_code":"P1","item_no":item_id,"name":item_id,"kind":"L","quantity":"2.00","unit_price":price,"quantity_scale":2,"price_scale":2,"amount_scale":2,"row_version":row_version})
        self.assertLess(status,400)
        return result

    def test_snapshot_diff_lock_and_restore(self):
        first=self.save("I1","10.00")
        v1=self.versions.create_version("P1","baseline","7","APPROVED")
        self.save("I1","12.50",first["row_version"])
        self.save("I2","3.00")
        v2=self.versions.create_version("P1","changed","7")
        diff=self.versions.diff(v1["id"],v2["id"])
        self.assertEqual(len(diff["added"]),1)
        self.assertEqual(len(diff["changed"]),1)
        lock=self.versions.set_lock("P1",True,"7","approved budget")
        self.assertTrue(lock["locked"])
        with self.assertRaises(PermissionError): self.versions.restore(v1["id"],"7")
        self.versions.set_lock("P1",False,"7")
        restored=self.versions.restore(v1["id"],"7")
        self.assertEqual(restored["new_version"]["status"],"RESTORED")
        item=self.budget.get("I1")
        self.assertEqual(item["unit_price"],"10.00")
        self.assertIsNone(self.budget.get("I2"))

    def test_versions_have_deep_links_and_are_append_only(self):
        self.save("I1","1.00")
        a=self.versions.create_version("P1","A","7")
        b=self.versions.create_version("P1","B","7")
        rows=self.versions.list_project("P1")
        self.assertEqual(len(rows),2)
        self.assertIn("version=",a["deep_link"])
        self.assertNotEqual(a["id"],b["id"])

if __name__ == "__main__": unittest.main()
