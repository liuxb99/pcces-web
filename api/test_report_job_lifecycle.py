import unittest
from sqlalchemy import create_engine
from api.report_center import ReportCenterService
from api.report_job_lifecycle import ReportJobLifecycleService

class ReportJobLifecycleTest(unittest.TestCase):
 def setUp(self):
  self.engine=create_engine("sqlite:///:memory:");self.center=ReportCenterService(self.engine);self.life=ReportJobLifecycleService(self.engine)
 def test_failure_and_retry(self):
  job=self.center.create_job({"definition_code":"CONTRACT","project_code":"P1","business_version_id":"V1","format":"PDF","snapshot":{"title":"x","rows":[]}},"u")
  failed=self.life.fail(job["id"],{"row_version":1,"error":{"message":"boom"}});self.assertEqual(failed["status"],"FAILED")
  retried=self.life.retry(job["id"],{"row_version":2});self.assertEqual(retried["status"],"QUEUED");self.assertEqual(retried["progress"],0)

if __name__=="__main__":unittest.main()
