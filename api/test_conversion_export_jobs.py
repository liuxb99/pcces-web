import unittest
from xml.etree import ElementTree
from sqlalchemy import create_engine

from api.conversion_export_jobs import ConversionExportJobService


class ConversionExportJobTests(unittest.TestCase):
    def setUp(self):
        self.service = ConversionExportJobService(create_engine("sqlite+pysqlite:///:memory:"))
        self.body = {
            "wizard_session_id": "W1", "source_budget_version_id": "BV1",
            "target_project_code": "BID1",
            "items": [{"source_budget_item_id": "I1", "code": " a001 ", "name": "混凝土", "unit": "M3", "quantity": "2", "unit_price": "100", "amount": "200"}],
        }

    def test_new_xml_job_metadata_and_download(self):
        item = self.service.create({**self.body, "format": "XML_NEW"}, "u1")
        self.assertEqual(item["status"], "COMPLETED")
        self.assertEqual(item["metadata"]["item_count"], 1)
        content, content_type, filename = self.service.artifact(item["id"])
        self.assertEqual(content_type, "application/xml; charset=utf-8")
        self.assertTrue(filename.endswith(".xml"))
        root = ElementTree.fromstring(content)
        self.assertEqual(root.tag, "PCCESBidExchange")
        self.assertEqual(root.findtext("./Items/Item/Code"), "A001")
        self.assertEqual(len(item["sha256"]), 64)

    def test_legacy_xml_contract(self):
        item = self.service.create({**self.body, "format": "XML_LEGACY"}, "u1")
        content, _, _ = self.service.artifact(item["id"])
        root = ElementTree.fromstring(content)
        self.assertEqual(root.tag, "PCCES")
        self.assertEqual(root.get("version"), "1.0")
        self.assertEqual(root.findtext("./Detail/Record/SourceItemId"), "I1")

    def test_json_and_invalid_inputs(self):
        item = self.service.create({**self.body, "format": "BID_JSON"}, "u1")
        content, content_type, _ = self.service.artifact(item["id"])
        self.assertIn(b'"project_code": "BID1"', content)
        self.assertTrue(content_type.startswith("application/json"))
        with self.assertRaises(ValueError):
            self.service.create({**self.body, "format": "XLSX"}, "u1")
        with self.assertRaises(ValueError):
            self.service.create({**self.body, "items": []}, "u1")


if __name__ == "__main__":
    unittest.main()
