import json
import unittest
from sqlalchemy import create_engine
from sqlalchemy.pool import StaticPool

from api.legacy_exchange_adapters import LegacyExchangeAdapterService, parse_legacy_exchange


class LegacyExchangeAdapterTests(unittest.TestCase):
    def setUp(self):
        self.engine = create_engine("sqlite://", connect_args={"check_same_thread": False}, poolclass=StaticPool)
        self.service = LegacyExchangeAdapterService(self.engine)

    def test_zmd(self):
        payload = json.dumps({"project_code": "P1", "version": "1.2", "items": [{"id": "A", "code": "a-1", "name": "N", "quantity": 2, "unit_price": 3, "amount": 6}]})
        project, version, items = parse_legacy_exchange(payload, "ZMD")
        self.assertEqual((project, version, items[0]["code"]), ("P1", "1.2", "A-1"))

    def test_mdb_csv_bridge(self):
        payload = "project_code,id,code,name,unit,quantity,unit_price,amount\nP2,B,b-1,Item,EA,1,5,5\n"
        project, version, items = parse_legacy_exchange(payload, "MDB")
        self.assertEqual(project, "P2")
        self.assertEqual(version, "CSV-1.0")
        self.assertEqual(items[0]["code"], "B-1")

    def test_px(self):
        payload = '<PX version="3.0"><Header><ProjectCode>P3</ProjectCode></Header><Items><Item><SourceItemId>C</SourceItemId><Code>c-1</Code><Name>Item</Name></Item></Items></PX>'
        project, version, items = parse_legacy_exchange(payload, "PX")
        self.assertEqual((project, version, items[0]["code"]), ("P3", "3.0", "C-1"))

    def test_session_blocked_on_duplicate(self):
        body = {"format": "ZMD", "payload": json.dumps({"items": [{"code": "A", "name": "1"}, {"code": "a", "name": "2"}]}), "source_filename": "x.zmd", "target_project_code": "T"}
        item = self.service.create(body, "u")
        self.assertEqual(item["status"], "BLOCKED")
        self.assertEqual(item["report"]["error_count"], 1)


if __name__ == "__main__":
    unittest.main()
