from __future__ import annotations

import unittest
from sqlalchemy import create_engine

from api.cost_structure import CostStructureService
from api.cost_structure_details import CostStructureDetailService


class CostStructureDetailTests(unittest.TestCase):
    def setUp(self):
        self.engine = create_engine("sqlite:///:memory:")
        self.base = CostStructureService(self.engine)
        self.service = CostStructureDetailService(self.engine)
        self.base.save_type("T1", {"code": "STD", "name": "標準成本結構", "enabled": True}, "u1")

    def test_atomic_import_and_ordered_categories(self):
        result = self.service.import_definition("T1", {"categories": [
            {"code": "D", "name": "直接費", "kind": "DIRECT", "sequence": 1},
            {"code": "M", "name": "管理費", "kind": "MANAGEMENT", "sequence": 2, "rate": "0.05"},
            {"code": "T", "name": "稅費", "kind": "TAX", "sequence": 3, "rate": "0.05"},
        ]}, "u1")
        self.assertEqual(result["imported_rows"], 3)
        rows = self.service.list_categories("T1")
        self.assertEqual([row["code"] for row in rows], ["D", "M", "T"])

    def test_invalid_import_does_not_replace_existing_definition(self):
        self.service.import_definition("T1", {"categories": [
            {"code": "D", "name": "直接費", "kind": "DIRECT"},
        ]}, "u1")
        with self.assertRaises(ValueError):
            self.service.import_definition("T1", {"categories": [
                {"code": "D", "name": "重複一", "kind": "DIRECT"},
                {"code": "D", "name": "重複二", "kind": "DIRECT"},
            ]}, "u1")
        self.assertEqual(len(self.service.list_categories("T1")), 1)

    def test_budget_item_property_keeps_category_identity_and_locking(self):
        self.service.import_definition("T1", {"categories": [
            {"id": "C1", "code": "MGMT", "name": "管理費", "kind": "MANAGEMENT", "rate": "0.075"},
        ]}, "u1")
        item = self.service.save_item_property("P1", "B1", {
            "cost_category_id": "C1", "cost_kind": "MANAGEMENT", "sign": 1,
            "rate": "0.075", "row_version": 0,
        }, "u1")
        self.assertEqual(item["category_code"], "MGMT")
        self.assertEqual(item["row_version"], 1)
        self.assertIn("panel=cost-property", item["deep_link"])
        with self.assertRaises(RuntimeError):
            self.service.save_item_property("P1", "B1", {
                "cost_category_id": "C1", "cost_kind": "MANAGEMENT", "row_version": 0,
            }, "u2")

    def test_rejects_invalid_sign_and_kind(self):
        self.service.import_definition("T1", {"categories": [
            {"id": "C1", "code": "D", "name": "直接費", "kind": "DIRECT"},
        ]}, "u1")
        with self.assertRaises(ValueError):
            self.service.save_item_property("P1", "B1", {
                "cost_category_id": "C1", "cost_kind": "UNKNOWN", "row_version": 0,
            }, "u1")
        with self.assertRaises(ValueError):
            self.service.save_item_property("P1", "B1", {
                "cost_category_id": "C1", "cost_kind": "DIRECT", "sign": 0, "row_version": 0,
            }, "u1")


if __name__ == "__main__":
    unittest.main()
