import json
import unittest
from sqlalchemy import create_engine
from sqlalchemy.pool import StaticPool

from api.bid_budget_roundtrip import BidBudgetRoundTripService, detect_and_parse, import_preflight


class BidBudgetRoundTripTests(unittest.TestCase):
    def setUp(self):
        self.engine = create_engine("sqlite://", connect_args={"check_same_thread": False}, poolclass=StaticPool)
        self.service = BidBudgetRoundTripService(self.engine)

    def test_detect_new_xml_and_roundtrip_lineage(self):
        payload = """<?xml version='1.0' encoding='utf-8'?><PCCESBidExchange version='2.0'><Header><ProjectCode>BID-1</ProjectCode></Header><Items><Item><SourceItemId>A1</SourceItemId><Code>a-01</Code><Name>Concrete</Name><Unit>M3</Unit><Quantity>2</Quantity><UnitPrice>10</UnitPrice><Amount>20</Amount></Item></Items></PCCESBidExchange>"""
        fmt, version, project, items = detect_and_parse(payload)
        self.assertEqual((fmt, version, project), ("XML_NEW", "2.0", "BID-1"))
        self.assertEqual(items[0]["code"], "A-01")
        result = self.service.create({"payload": payload, "target_budget_project_code": "BUD-2", "source_conversion_session_id": "CONV-1"}, "user")
        self.assertEqual(result["status"], "READY")
        self.assertEqual(result["round_trip_lineage"]["source_conversion_session_id"], "CONV-1")
        self.assertEqual(result["round_trip_lineage"]["item_links"][0]["source_budget_item_id"], "A1")

    def test_detect_legacy_xml(self):
        payload = "<PCCES version='1.0'><Header><ProjectCode>L1</ProjectCode></Header><Detail><Record><SourceItemId>X</SourceItemId><Code>x</Code><Name>N</Name></Record></Detail></PCCES>"
        fmt, version, project, items = detect_and_parse(payload)
        self.assertEqual((fmt, version, project), ("XML_LEGACY", "1.0", "L1"))
        self.assertEqual(items[0]["code"], "X")

    def test_json_and_blocking_duplicate_code(self):
        payload = json.dumps({"project_code": "J1", "items": [{"id": "1", "code": "A"}, {"id": "2", "code": "a"}]})
        fmt, version, _, items = detect_and_parse(payload)
        self.assertEqual((fmt, version), ("BID_JSON", "2.0"))
        report = import_preflight(items)
        self.assertFalse(report["can_continue"])
        result = self.service.create({"payload": payload, "target_budget_project_code": "B1"}, "user")
        self.assertEqual(result["status"], "BLOCKED")


if __name__ == "__main__":
    unittest.main()
