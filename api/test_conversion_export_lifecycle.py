import io
import unittest
import zipfile
from sqlalchemy import create_engine
from api.conversion_export_lifecycle import ConversionExportLifecycleService, validate_xml

class ExportLifecycleTest(unittest.TestCase):
    def setUp(self):
        self.service = ConversionExportLifecycleService(create_engine("sqlite+pysqlite:///:memory:", future=True))
    def test_xlsx_version_retry_and_download(self):
        item = self.service.create_version({"job_id":"J1","format":"XLSX","target_project_code":"BID1","source_budget_version_id":"V1","items":[{"id":"1","code":"a1","name":"work"}]}, "u1")
        self.assertEqual(item["version_no"], 1); self.assertTrue(item["validation"]["valid"])
        payload, ctype, _ = self.service.artifact(item["id"])
        self.assertTrue(zipfile.is_zipfile(io.BytesIO(payload))); self.assertIn("spreadsheetml", ctype)
        retry = self.service.retry(item["id"], "u2")
        self.assertEqual(retry["version_no"], 2); self.assertEqual(retry["sha256"], item["sha256"])
    def test_xml_schema_contract(self):
        valid = b'<?xml version="1.0"?><PCCESBidExchange version="2.0"><Header/><Items/></PCCESBidExchange>'
        self.assertTrue(validate_xml(valid, "XML_NEW")["valid"])
        self.assertFalse(validate_xml(b'<PCCES version="2.0"/>', "XML_NEW")["valid"])

if __name__ == "__main__": unittest.main()
