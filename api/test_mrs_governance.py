import unittest
from sqlalchemy import create_engine
from sqlalchemy.pool import StaticPool

from api.mrs_catalog import MRSCatalogService
from api.mrs_operations import MRSOperationsService
from api.mrs_exchange import MRSExchangeService
from api.mrs_governance_paging import MRSGovernanceService


class MRSGovernanceTests(unittest.TestCase):
    def setUp(self):
        self.engine=create_engine("sqlite://",connect_args={"check_same_thread":False},poolclass=StaticPool,future=True)
        self.catalog=MRSCatalogService(self.engine)
        self.exchange=MRSExchangeService(self.catalog)
        self.ops=MRSOperationsService(self.engine,self.catalog,self.exchange)
        self.gov=MRSGovernanceService(self.engine)
        self.catalog.save_item("M1",{"code":"M-1","name":"水泥","category":"MATERIAL","current_price":"100","price_scale":2},"7")
        self.catalog.save_recipe("R1",{"code":"R-1","name":"分析","price_scale":2,"components":[{"catalog_item_id":"M1","quantity":"2","quantity_scale":2}]})
        self.version=self.ops.create_recipe_version("R1","baseline","7")

    def test_release_workflow_validity_freeze_and_audit(self):
        release=self.gov.create_release("2026-08","7")
        self.assertEqual(release["status"],"DRAFT")
        release=self.gov.transition_release(release["id"],"SUBMIT","7",release["row_version"])
        release=self.gov.transition_release(release["id"],"APPROVE","8",release["row_version"],"ok")
        release=self.gov.transition_release(release["id"],"PUBLISH","8",release["row_version"])
        self.assertEqual(release["status"],"PUBLISHED")
        validity=self.gov.set_validity("M1",{"valid_from":"2026-01-01","valid_to":"2026-06-30","status":"ACTIVE"},"7")
        self.assertEqual(validity["row_version"],1)
        alerts=self.gov.expiry_alerts("2026-08-02")
        self.assertEqual(alerts[0]["status"],"EXPIRED")
        freeze=self.gov.set_recipe_freeze("R1",{"version_id":self.version["id"],"frozen":True,"reason":"approved basis"},"8")
        self.assertTrue(freeze["frozen"])
        self.assertEqual(freeze["version_id"],self.version["id"])
        self.assertGreaterEqual(len(self.gov.audit()),6)

    def test_conflict_and_invalid_transition(self):
        release=self.gov.create_release("draft","7")
        with self.assertRaises(ValueError): self.gov.transition_release(release["id"],"APPROVE","8",release["row_version"])
        self.gov.set_validity("M1",{"status":"ACTIVE"},"7")
        with self.assertRaises(RuntimeError): self.gov.set_validity("M1",{"status":"SUSPENDED","row_version":0},"7")

    def test_release_query_filters_paging_and_bounds(self):
        first=self.gov.create_release("draft-1","7")
        second=self.gov.create_release("draft-2","7")
        second=self.gov.transition_release(second["id"],"submit","7",second["row_version"])
        page=self.gov.query_releases(" draft ",1,0)
        self.assertEqual(page["total"],1)
        self.assertEqual(page["limit"],1)
        self.assertEqual(page["items"][0]["id"],first["id"])
        submitted=self.gov.query_releases("submitted",500,-10)
        self.assertEqual(submitted["total"],1)
        self.assertEqual(submitted["limit"],200)
        self.assertEqual(submitted["offset"],0)
        self.assertEqual(submitted["items"][0]["id"],second["id"])
        with self.assertRaises(ValueError): self.gov.query_releases("unknown",50,0)

    def test_audit_query_filters_and_paging(self):
        release=self.gov.create_release("audit","7")
        release=self.gov.transition_release(release["id"],"submit","7",release["row_version"])
        page=self.gov.query_audit(" catalog_release ",release["id"]," release_submit ",50,0)
        self.assertEqual(page["total"],1)
        self.assertEqual(page["items"][0]["event_type"],"RELEASE_SUBMIT")
        self.assertEqual(page["items"][0]["resource_id"],release["id"])
        bounded=self.gov.query_audit(limit=999,offset=-5)
        self.assertEqual(bounded["limit"],200)
        self.assertEqual(bounded["offset"],0)
        self.assertGreaterEqual(bounded["total"],2)

if __name__=="__main__": unittest.main()
