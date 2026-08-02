import unittest
from sqlalchemy import create_engine
from sqlalchemy.pool import StaticPool

from api.mrs_catalog import MRSCatalogService
from api.mrs_exchange import MRSExchangeService


class MRSCatalogTests(unittest.TestCase):
    def setUp(self):
        self.engine=create_engine("sqlite://",connect_args={"check_same_thread":False},poolclass=StaticPool,future=True)
        self.service=MRSCatalogService(self.engine)
        self.exchange=MRSExchangeService(self.service)

    def test_catalog_history_bookmarks_recipe_and_export(self):
        a=self.service.save_item("M1",{"code":"M-001","name":"水泥","category":"MATERIAL","unit":"包","current_price":"180.125","price_scale":2,"source":"base"},"7")
        self.assertEqual(a["current_price"],"180.13")
        self.service.save_item("L1",{"code":"L-001","name":"技工","category":"LABOR","unit":"工","current_price":"2500","price_scale":2,"source":"base"},"7")
        updated=self.service.save_item("M1",{"code":"M-001","name":"水泥","category":"MATERIAL","unit":"包","current_price":"190.00","price_scale":2,"source":"survey","row_version":a["row_version"]},"7")
        self.assertEqual(updated["row_version"],2)
        history=self.service.history("M1")
        self.assertEqual(len(history),2)
        self.assertEqual(history[0]["old_price"],"180.13")
        self.service.set_bookmark("7","M1",True)
        self.assertEqual([x["id"] for x in self.service.bookmarks("7")],["M1"])
        recipe=self.service.save_recipe("R1",{"code":"A-001","name":"混凝土單價分析","unit":"m3","price_scale":2,"components":[{"catalog_item_id":"M1","quantity":"2.5","quantity_scale":2},{"catalog_item_id":"L1","quantity":"0.1","quantity_scale":2}]})
        self.assertEqual(recipe["unit_price"],"725.00")
        payload,mimetype,run_id=self.service.export_items("json","7")
        self.assertIn("M-001",payload);self.assertEqual(mimetype,"application/json");self.assertTrue(run_id)

    def test_rerunnable_json_import(self):
        payload='[{"id":"I1","code":"I-001","name":"砂","category":"MATERIAL","unit":"m3","current_price":"500.125","price_scale":2}]'
        first=self.exchange.import_payload(payload,"json","7")
        second=self.exchange.import_payload(payload,"json","7")
        self.assertEqual(first["imported"],1)
        self.assertEqual(second["skipped"],1)
        overwrite=self.exchange.import_payload(payload,"json","7",True)
        self.assertEqual(overwrite["imported"],1)
        self.assertEqual(self.service.get_item("I1")["current_price"],"500.13")

    def test_row_version_conflict(self):
        self.service.save_item("E1",{"code":"E-001","name":"挖土機","category":"EQUIPMENT","current_price":"1000","price_scale":2},"7")
        with self.assertRaises(RuntimeError):
            self.service.save_item("E1",{"code":"E-001","name":"挖土機","category":"EQUIPMENT","current_price":"1200","price_scale":2,"row_version":0},"7")

if __name__=="__main__":unittest.main()
