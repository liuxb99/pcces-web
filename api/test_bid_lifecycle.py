import unittest
from sqlalchemy import create_engine
from sqlalchemy.pool import StaticPool

from api.bid_lifecycle import BidLifecycleService
from api.budget_decimal import BudgetDecimalService
from api.budget_validation import BudgetValidationService


class BidLifecycleTests(unittest.TestCase):
    def setUp(self):
        self.engine=create_engine("sqlite://",connect_args={"check_same_thread":False},poolclass=StaticPool,future=True)
        self.budget=BudgetDecimalService(self.engine);self.budget.create_schema()
        self.validation=BudgetValidationService(self.engine)
        self.service=BidLifecycleService(self.engine,self.validation)

    def save(self,item,project,price,quantity="2.00",row_version=0,parent=None):
        result,status=self.budget.save(item,{"project_code":project,"parent_id":parent,"item_no":item,"name":item,"kind":"L","quantity":quantity,"unit_price":price,"quantity_scale":2,"price_scale":2,"amount_scale":2,"row_version":row_version})
        self.assertLess(status,400);return result

    def test_convert_price_versions_variance_and_rollback(self):
        self.save("A","BUD-P","10.00")
        run=self.service.convert("BUD-P","BID-P","7")
        self.assertEqual(run["copied_items"],1)
        self.assertEqual(self.validation.mode("BID-P")["mode"],"BID")
        versions=self.service.create_price_version("BID-P","baseline","7","SEALED")
        target=self.budget.get("bid-BID-P-A")
        self.save("bid-BID-P-A","BID-P","12.50",row_version=target["row_version"])
        current=self.service.create_price_version("BID-P","current","7")
        variance=self.service.variance(versions["id"],current["id"])
        self.assertEqual(variance["difference"],"5.00")
        self.assertEqual(variance["percentage"],"25.00")
        rollback=self.service.rollback(versions["id"],"7")
        self.assertEqual(rollback["restored_items"],1)
        self.assertEqual(self.budget.get("bid-BID-P-A")["unit_price"],"10.00")
        self.assertIn("bid-lifecycle",versions["deep_link"])

    def test_conversion_rejects_existing_target_without_overwrite(self):
        self.save("A","BUD-P","1.00");self.save("X","BID-P","2.00")
        with self.assertRaises(ValueError):self.service.convert("BUD-P","BID-P","7")

if __name__=="__main__":unittest.main()
