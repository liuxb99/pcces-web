import unittest
from sqlalchemy import create_engine
from sqlalchemy.pool import StaticPool

from api.mrs_catalog import MRSCatalogService
from api.mrs_exchange import MRSExchangeService
from api.mrs_intelligence import MRSIntelligenceService
from api.mrs_operations import MRSOperationsService


class MRSOperationsTests(unittest.TestCase):
    def setUp(self):
        self.engine=create_engine("sqlite://",connect_args={"check_same_thread":False},poolclass=StaticPool,future=True)
        self.catalog=MRSCatalogService(self.engine)
        self.exchange=MRSExchangeService(self.catalog)
        self.intelligence=MRSIntelligenceService(self.engine)
        self.service=MRSOperationsService(self.engine,self.catalog,self.exchange)
        self.catalog.save_item("M1",{"code":"M-1","name":"水泥","category":"MATERIAL","current_price":"100","price_scale":2},"7")
        self.catalog.save_item("L1",{"code":"L-1","name":"工資","category":"LABOR","current_price":"200","price_scale":2},"7")
        self.catalog.save_recipe("R1",{"code":"R-1","name":"分析","price_scale":2,"components":[{"catalog_item_id":"M1","quantity":"2","quantity_scale":2},{"catalog_item_id":"L1","quantity":"0.5","quantity_scale":2}]})

    def test_usage_versions_diff_and_lineage(self):
        usage=self.service.usage_summary()
        self.assertEqual(usage["catalog_items"],2)
        self.assertEqual(usage["estimated_amount"],"300.00")
        v1=self.service.create_recipe_version("R1","baseline","7")
        item=self.catalog.get_item("M1")
        self.catalog.save_item("M1",{"code":"M-1","name":"水泥","category":"MATERIAL","current_price":"120","price_scale":2,"row_version":item["row_version"]},"7")
        v2=self.service.create_recipe_version("R1","current","7")
        diff=self.service.diff_recipe_versions(v1["id"],v2["id"])
        self.assertEqual(diff["difference"],"40.00")
        self.assertEqual(len(diff["changed"]),1)
        self.intelligence.add_quote("M1",{"vendor":"供應商","quoted_price":"115","price_scale":2},"7")
        lineage=self.service.price_lineage("M1")
        self.assertGreaterEqual(len(lineage["events"]),3)
        self.assertIn("mrs-operations",lineage["deep_link"])

    def test_import_job_run_and_cancel(self):
        payload='[{"id":"I1","code":"I-1","name":"砂","category":"MATERIAL","current_price":"50","price_scale":2}]'
        job=self.service.create_import_job(payload,"json",False,"7")
        self.assertEqual(job["status"],"PENDING")
        done=self.service.run_import_job(job["id"])
        self.assertEqual(done["status"],"COMPLETED")
        self.assertEqual(done["imported_rows"],1)
        job2=self.service.create_import_job(payload,"json",False,"7")
        cancelled=self.service.cancel_import_job(job2["id"])
        self.assertEqual(cancelled["status"],"CANCELLED")
        with self.assertRaises(ValueError): self.service.run_import_job(job2["id"])

if __name__=="__main__": unittest.main()
