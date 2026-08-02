import unittest
from sqlalchemy import create_engine

from api.resource_decimal import ResourceDecimalService


class ResourceDecimalTests(unittest.TestCase):
    def setUp(self):
        self.engine = create_engine("sqlite+pysqlite:///:memory:", future=True)
        self.service = ResourceDecimalService(self.engine)
        self.service.create_schema()

    def test_breakdowns_roll_up_to_resource_price(self):
        resource, status = self.service.save_resource("R1", {
            "code":"R001","name":"混凝土","unit":"m3","unit_price":"0","price_scale":2
        })
        self.assertEqual(status, 200)
        first, status = self.service.save_breakdown("D1", {
            "resource_id":"R1","code":"MAT","name":"材料","quantity":"2.5000",
            "unit_price":"100.0050","quantity_scale":4,"price_scale":4,"amount_scale":2
        })
        self.assertEqual(status, 200)
        self.assertEqual(first["amount"], "250.01")
        second, status = self.service.save_breakdown("D2", {
            "resource_id":"R1","code":"LAB","name":"人工","quantity":"1.0000",
            "unit_price":"50.0050","quantity_scale":4,"price_scale":4,"amount_scale":2
        })
        self.assertEqual(status, 200)
        resource = self.service.get_resource("R1")
        self.assertEqual(resource["unit_price"], "300.02")

    def test_stale_resource_update_is_rejected(self):
        item, _ = self.service.save_resource("R1", {"code":"R001","name":"材料","unit_price":"1.0000"})
        updated, status = self.service.save_resource("R1", {"row_version":item["row_version"],"unit_price":"2.0000"})
        self.assertEqual(status, 200)
        conflict, status = self.service.save_resource("R1", {"row_version":item["row_version"],"unit_price":"3.0000"})
        self.assertEqual(status, 409)
        self.assertEqual(conflict["code"], "CONFLICT")
        self.assertEqual(updated["unit_price"], "2.0000")


if __name__ == "__main__":
    unittest.main()
