import unittest
from sqlalchemy import create_engine
from sqlalchemy.pool import StaticPool
from api.conversion_long_jobs import ConversionLongJobService

class ConversionLongJobTests(unittest.TestCase):
    def setUp(self):
        self.engine=create_engine("sqlite+pysqlite:///:memory:",connect_args={"check_same_thread":False},poolclass=StaticPool)
        self.service=ConversionLongJobService(self.engine)
    def test_progress_is_monotonic_and_completes(self):
        job=self.service.create({"job_type":"EXPORT","payload":{"x":1}},"u")
        job=self.service.advance(job["id"],{"row_version":1,"progress":40,"stage":"SERIALIZE"})
        self.assertEqual("RUNNING",job["status"])
        job=self.service.advance(job["id"],{"row_version":2,"progress":100,"result":{"artifact_id":"A"}})
        self.assertEqual("COMPLETED",job["status"])
        self.assertEqual({"artifact_id":"A"},job["result"])
    def test_cancel_clears_partial_result(self):
        job=self.service.create({"job_type":"IMPORT"},"u")
        job=self.service.advance(job["id"],{"row_version":1,"progress":30,"stage":"VALIDATE"})
        job=self.service.cancel(job["id"],2)
        self.assertEqual("CANCELLED",job["status"])
        self.assertTrue(job["cancel_requested"])
        self.assertIsNone(job["result"])
    def test_stale_and_regressive_updates_rejected(self):
        job=self.service.create({"job_type":"EXPORT"},"u")
        self.service.advance(job["id"],{"row_version":1,"progress":50})
        with self.assertRaises(RuntimeError): self.service.cancel(job["id"],1)
        with self.assertRaises(ValueError): self.service.advance(job["id"],{"row_version":2,"progress":20})

if __name__=="__main__": unittest.main()
