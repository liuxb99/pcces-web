import base64
import hashlib
import json
import unittest
from decimal import Decimal

from api.bid_budget_roundtrip import detect_and_parse
from api.conversion_export_jobs import serialize_xml
from api.conversion_export_lifecycle import serialize_xlsx


ITEMS = [
    {
        "id": "SRC-001",
        "source_budget_item_id": "SRC-001",
        "code": "a-001",
        "name": "Concrete",
        "unit": "m3",
        "quantity": "2.5000",
        "unit_price": "125.40",
        "amount": "313.50",
    },
    {
        "id": "SRC-002",
        "source_budget_item_id": "SRC-002",
        "code": "b-002",
        "name": "Rebar",
        "unit": "kg",
        "quantity": "10",
        "unit_price": "3.25",
        "amount": "32.50",
    },
]
PROJECT = "P-ROUNDTRIP"
SOURCE_VERSION = "BV-001"


def normalized(items):
    result = []
    for item in items:
        result.append({
            "lineage": str(item.get("source_budget_item_id") or item.get("id") or ""),
            "code": str(item.get("code") or "").strip().upper(),
            "name": str(item.get("name") or "").strip(),
            "unit": str(item.get("unit") or "").strip(),
            "quantity": Decimal(str(item.get("quantity", "0"))),
            "unit_price": Decimal(str(item.get("unit_price", "0"))),
            "amount": Decimal(str(item.get("amount", "0"))),
        })
    return sorted(result, key=lambda item: item["lineage"])


class ExchangeRoundTripContractTest(unittest.TestCase):
    def assert_roundtrip(self, payload, hinted_format, expected_format, expected_version):
        fmt, version, project, imported = detect_and_parse(payload, hinted_format)
        self.assertEqual(fmt, expected_format)
        self.assertEqual(version, expected_version)
        self.assertEqual(project, PROJECT)
        self.assertEqual(normalized(imported), normalized(ITEMS))
        self.assertEqual(sum(item["amount"] for item in normalized(imported)), Decimal("346.00"))

    def test_json_semantic_roundtrip(self):
        payload = json.dumps({
            "project_code": PROJECT,
            "source_budget_version_id": SOURCE_VERSION,
            "items": ITEMS,
        }, ensure_ascii=False, sort_keys=True)
        self.assert_roundtrip(payload, "BID_JSON", "BID_JSON", "2.0")
        self.assertEqual(len(hashlib.sha256(payload.encode()).hexdigest()), 64)

    def test_xml_new_semantic_roundtrip(self):
        payload = serialize_xml(ITEMS, PROJECT, SOURCE_VERSION, legacy=False)
        self.assert_roundtrip(payload.decode(), "XML_NEW", "XML_NEW", "2.0")
        self.assertIn(b"PCCESBidExchange", payload)

    def test_xml_legacy_semantic_roundtrip(self):
        payload = serialize_xml(ITEMS, PROJECT, SOURCE_VERSION, legacy=True)
        self.assert_roundtrip(payload.decode(), "XML_LEGACY", "XML_LEGACY", "1.0")
        self.assertIn(b"<PCCES", payload)

    def test_xlsx_semantic_roundtrip_bytes_and_base64(self):
        payload = serialize_xlsx(ITEMS, PROJECT, SOURCE_VERSION)
        self.assertTrue(payload.startswith(b"PK"))
        self.assert_roundtrip(payload, "XLSX", "XLSX", "1.0")
        encoded = base64.b64encode(payload).decode()
        self.assert_roundtrip(encoded, "XLSX", "XLSX", "1.0")


if __name__ == "__main__":
    unittest.main()
