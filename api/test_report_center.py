import unittest
from sqlalchemy import create_engine
from api.report_center import ReportCenterService

class ReportCenterTest(unittest.TestCase):
 def setUp(self):self.s=ReportCenterService(create_engine("sqlite:///:memory:"))
 def test_snapshot_job_render_and_download(self):
  job=self.s.create_job({"definition_code":"CONTRACT","project_code":"P1","business_version_id":"CV1","format":"PDF","snapshot":{"title":"Contract","rows":[{"amount":"100"}]}},"u")
  done=self.s.render(job["id"],1,"u")
  self.assertEqual(done["status"],"COMPLETED");self.assertEqual(done["progress"],100)
  content,ctype,name=self.s.download(done["artifact"]["id"],"u")
  self.assertTrue(content.startswith(b"%PDF"));self.assertEqual(ctype,"application/pdf");self.assertTrue(name.endswith(".pdf"))
 def test_version_is_required(self):
  with self.assertRaises(ValueError):self.s.create_job({"definition_code":"CONTRACT","project_code":"P1","snapshot":{}},"u")

if __name__=="__main__":unittest.main()
