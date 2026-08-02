import json
import unittest
from pathlib import Path

from sqlalchemy import create_engine

from api.work_context import WorkContextService, transition


class WorkContextTransitionTests(unittest.TestCase):
    def test_shared_golden_transitions(self):
        fixture = json.loads((Path(__file__).parents[1] / "specs/golden/work-context-transitions.json").read_text(encoding="utf-8"))
        for case in fixture["cases"]:
            current = case["current"]
            result = transition(current["exists"], current["dirty"], current["row_version"], case["command"], case.get("request_row_version"))
            self.assertEqual({"exists":result.exists,"dirty":result.dirty,"row_version":result.row_version,"outcome":result.outcome}, case["expected"], case["name"])

    def test_persistent_save_discard_cancel_and_conflict(self):
        engine = create_engine("sqlite+pysqlite:///:memory:")
        service = WorkContextService(engine)
        service.create_schema()
        item, status = service.apply("ctx-1", 7, "SAVE_DRAFT", {"action_code":"BUD","draft_payload":"{}"})
        self.assertEqual(200, status)
        self.assertTrue(item["dirty"])
        stale, status = service.apply("ctx-1", 7, "SAVE", {"row_version":0})
        self.assertEqual(409, status)
        self.assertEqual("CONFLICT", stale["code"])
        item, status = service.apply("ctx-1", 7, "DISCARD", {"row_version":1})
        self.assertEqual(200, status)
        self.assertFalse(item["dirty"])
        cancelled, status = service.apply("ctx-1", 7, "CANCEL", {"row_version":2})
        self.assertEqual(200, status)
        self.assertFalse(cancelled["exists"])


if __name__ == "__main__":
    unittest.main()
