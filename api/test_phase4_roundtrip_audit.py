from unittest import TestCase

from api.phase4_roundtrip_audit import audit_roundtrip


class Phase4RoundTripAuditTests(TestCase):
    def test_consistent_roundtrip(self):
        source = [{"id": "A", "code": "A01", "name": "Concrete", "unit": "M3", "quantity": "2", "unit_price": "10", "amount": "20"}]
        imported = [{"source_budget_item_id": "A", "code": "A01", "name": "Concrete", "unit": "M3", "quantity": "2.000", "unit_price": "10.00", "amount": "20.00"}]
        result = audit_roundtrip(source, imported)
        self.assertTrue(result["consistent"])
        self.assertEqual("0.00", result["total_difference"])

    def test_detects_value_and_lineage_difference(self):
        source = [{"id": "A", "code": "A01", "name": "Concrete", "unit": "M3", "quantity": "2", "unit_price": "10", "amount": "20"}]
        imported = [
            {"source_budget_item_id": "A", "code": "A01", "name": "Concrete", "unit": "M3", "quantity": "3", "unit_price": "10", "amount": "30"},
            {"source_budget_item_id": "B", "code": "B01", "name": "Steel", "unit": "KG", "quantity": "1", "unit_price": "5", "amount": "5"},
        ]
        result = audit_roundtrip(source, imported)
        self.assertFalse(result["consistent"])
        self.assertEqual(["B"], result["added_lineage_ids"])
        self.assertEqual("10.00", result["total_difference"])
        self.assertEqual("A", result["item_differences"][0]["source_budget_item_id"])

    def test_rejects_duplicate_lineage(self):
        with self.assertRaises(ValueError):
            audit_roundtrip([{"id": "A"}, {"id": "A"}], [])
