import hashlib
import unittest

from sqlalchemy import create_engine

from api.conversion_source_artifacts import ConversionSourceArtifactService


class ConversionSourceArtifactTests(unittest.TestCase):
    def setUp(self):
        self.service = ConversionSourceArtifactService(create_engine("sqlite:///:memory:"))

    def test_source_file_is_immutable_and_downloadable(self):
        row = self.service.create_source({
            "session_type": "IMPORT", "session_id": "S1", "original_filename": "原始標單.px",
            "content_type": "application/xml", "format": "PX", "format_version": "1.0",
            "content": "<PX version='1.0'/>",
        }, "tester")
        payload, content_type, filename = self.service.source_content(row["id"])
        self.assertEqual(filename, "原始標單.px")
        self.assertEqual(content_type, "application/xml")
        self.assertEqual(row["sha256"], hashlib.sha256(payload).hexdigest())
        self.assertEqual(row["size_bytes"], len(payload))

    def test_error_catalogue_csv_contains_errors_and_warnings(self):
        row = self.service.create_catalogue({
            "session_type": "IMPORT", "session_id": "S2",
            "errors": [{"code": "DUPLICATE_ITEM_CODE", "item_code": "A01", "detail": "duplicate"}],
            "warnings": [{"code": "MISSING_ITEM_NAME", "index": 2}],
        }, "tester")
        payload, filename = self.service.catalogue_csv(row["id"])
        text = payload.decode("utf-8-sig")
        self.assertIn("DUPLICATE_ITEM_CODE", text)
        self.assertIn("MISSING_ITEM_NAME", text)
        self.assertTrue(filename.endswith(".csv"))
        self.assertEqual(row["error_count"], 1)
        self.assertEqual(row["warning_count"], 1)

    def test_required_source_fields(self):
        with self.assertRaises(ValueError):
            self.service.create_source({"session_type": "IMPORT"}, "tester")


if __name__ == "__main__":
    unittest.main()
