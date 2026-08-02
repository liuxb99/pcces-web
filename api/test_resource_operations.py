import unittest
from sqlalchemy import create_engine
from sqlalchemy.pool import StaticPool

from api.budget_decimal import BudgetDecimalService
from api.resource_budget_lineage import ResourceBudgetLineageService
from api.resource_decimal import ResourceDecimalService
from api.resource_operations import ResourceOperationsService


class ResourceOperationsTests(unittest.TestCase):
    def setUp(self):
        self.engine=create_engine("sqlite://",connect_args={"check_same_thread":False},poolclass=StaticPool,future=True)
        self.budget=BudgetDecimalService(self.engine);self.budget.create_schema()
        self.resources=ResourceDecimalService(self.engine);self.resources.create_schema()
        self.lineage=ResourceBudgetLineageService(self.engine)
        self.ops=ResourceOperationsService(self.engine)
        for item_id,qty in (("I1","2"),("I2","3")):
            self.budget.save(item_id,{"project_code":"P1","name":item_id,"kind":"L","quantity":qty,"unit_price":"10","quantity_scale":2,"price_scale":2,"amount_scale":2,"row_version":0})
        for rid,price in (("R1","10"),("R2","20")):
            self.resources.save_resource(rid,{"code":rid,"name":rid,"unit_price":price,"price_scale":2,"row_version":0})
        self.lineage.link("P1","R1","I1");self.lineage.link("P1","R1","I2")
        self.lineage.link("P1","R2","I2")

    def test_replace_moves_links_and_deduplicates(self):
        result=self.ops.replace("P1","R1","R2","7")
        self.assertEqual(1,result["moved_links"])
        self.assertEqual(1,result["deduplicated_links"])
        rows=self.engine.connect().exec_driver_sql("select resource_id,budget_item_id from resource_budget_links order by budget_item_id").all()
        self.assertEqual([("R2","I1"),("R2","I2")],rows)

    def test_batch_prices_propagates_atomically(self):
        result=self.ops.batch_prices([
            {"resource_id":"R1","unit_price":"12.345","row_version":1},
            {"resource_id":"R2","unit_price":"25","row_version":1},
        ])
        self.assertEqual(2,result["updated_resources"])
        self.assertEqual(3,result["updated_budget_items"])
        self.assertEqual("12.35",self.resources.get_resource("R1")["unit_price"])
        self.assertEqual("24.70",self.budget.get("I1")["amount"])
        self.assertEqual("75.00",self.budget.get("I2")["amount"])

    def test_conflict_rolls_back_entire_batch(self):
        with self.assertRaises(RuntimeError):
            self.ops.batch_prices([
                {"resource_id":"R1","unit_price":"12","row_version":1},
                {"resource_id":"R2","unit_price":"30","row_version":99},
            ])
        self.assertEqual("10.00",self.resources.get_resource("R1")["unit_price"])
        self.assertEqual("20.00",self.resources.get_resource("R2")["unit_price"])


if __name__=="__main__": unittest.main()
