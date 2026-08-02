import unittest
from sqlalchemy import create_engine

from api.resource_decimal import ResourceDecimalService
from api.resource_project_reference import ResourceProjectReferenceService


class ResourceProjectReferenceTests(unittest.TestCase):
    def setUp(self):
        self.engine = create_engine("sqlite+pysqlite:///:memory:", future=True)
        self.resources = ResourceDecimalService(self.engine); self.resources.create_schema()
        self.refs = ResourceProjectReferenceService(self.engine)
        self.resources.save_resource("SRC-R1", {"code":"M01234567890", "name":"鋼筋", "unit":"KG",
            "unit_price":"31.2500", "price_scale":4, "row_version":0})

    def test_parent_reference_copies_snapshot_and_preserves_origin(self):
        result = self.refs.import_reference("CHILD", "PARENT", "SRC-R1", "CHILD-R1", "parent", "7")
        copied = self.resources.get_resource("CHILD-R1")
        self.assertEqual("31.2500", copied["unit_price"])
        self.assertEqual("PARENT", result["source_project_code"])
        self.assertEqual("PARENT", result["reference_type"])
        self.assertEqual("SRC-R1", result["snapshot"]["id"])
        self.assertIn("project=CHILD", result["deep_link"])

    def test_snapshot_is_immutable_after_source_changes(self):
        result = self.refs.import_reference("HIST", "OLD", "SRC-R1", "HIST-R1", "HISTORICAL", "7")
        self.resources.save_resource("SRC-R1", {"unit_price":"99", "row_version":1})
        listed = self.refs.list_target("HIST")
        self.assertEqual("31.25000000", listed[0]["snapshot"]["unit_price"])
        self.assertEqual(result["id"], listed[0]["id"])

    def test_invalid_type_missing_source_and_duplicate_target_are_rejected(self):
        with self.assertRaises(ValueError):
            self.refs.import_reference("C", "P", "SRC-R1", "X", "LIVE", "7")
        with self.assertRaises(LookupError):
            self.refs.import_reference("C", "P", "missing", "X", "PARENT", "7")
        self.refs.import_reference("C", "P", "SRC-R1", "X", "PARENT", "7")
        with self.assertRaises(RuntimeError):
            self.refs.import_reference("C", "P", "SRC-R1", "X", "PARENT", "7")


if __name__ == "__main__":
    unittest.main()
