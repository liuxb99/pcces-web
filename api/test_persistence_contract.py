import unittest

from sqlalchemy import create_engine

from api.persistence_contract import PersistenceService


class PersistenceContractTests(unittest.TestCase):
    def setUp(self):
        self.engine = create_engine("sqlite+pysqlite:///:memory:", future=True)
        self.service = PersistenceService(self.engine)
        self.service.create_schema()

    def test_exact_decimal_storage_and_audit(self):
        item = self.service.create_decimal("amount-1", "1.005", "user-1")
        self.assertEqual("1.00500000", item["value"])
        self.assertEqual(1, item["row_version"])
        audit = self.service.list_audit("amount-1")
        self.assertEqual(["DECIMAL_CREATE"], [event["event_type"] for event in audit])

    def test_successful_update_increments_version_and_appends_audit(self):
        self.service.create_decimal("amount-2", "100", "user-1")
        item, status = self.service.update_decimal("amount-2", "100.125", 1, "user-2")
        self.assertEqual(200, status)
        self.assertEqual("100.12500000", item["value"])
        self.assertEqual(2, item["row_version"])
        audit = self.service.list_audit("amount-2")
        self.assertEqual(["DECIMAL_CREATE", "DECIMAL_UPDATE"], [event["event_type"] for event in audit])

    def test_stale_update_is_conflict_and_does_not_append_audit(self):
        self.service.create_decimal("amount-3", "10", "user-1")
        _, status = self.service.update_decimal("amount-3", "11", 1, "user-1")
        self.assertEqual(200, status)
        result, status = self.service.update_decimal("amount-3", "12", 1, "user-1")
        self.assertEqual(409, status)
        self.assertEqual("CONFLICT", result["code"])
        self.assertEqual(2, len(self.service.list_audit("amount-3")))


if __name__ == "__main__":
    unittest.main()
