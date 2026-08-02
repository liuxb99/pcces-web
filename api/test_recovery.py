import json
import tempfile
import unittest
from pathlib import Path

from sqlalchemy import create_engine

from api.recovery import RecoveryService
from api.work_context import WorkContextService


class RecoveryServiceTest(unittest.TestCase):
    def setUp(self):
        self.temp = tempfile.TemporaryDirectory()
        self.engine = create_engine(f"sqlite:///{self.temp.name}/recovery.db")
        self.contexts = WorkContextService(self.engine)
        self.contexts.create_schema()
        self.service = RecoveryService(self.engine, self.contexts)
        self.service.create_schema()

    def tearDown(self):
        self.engine.dispose()
        self.temp.cleanup()

    def test_restore_recreates_dirty_work_context_and_is_single_use(self):
        item, status = self.service.create("snap-1", 7, {
            "context_id": "ctx-1", "project_code": "P001", "action_code": "BUD",
            "payload": {"name": "unsaved"}, "reason": "CRASH",
        })
        self.assertEqual(200, status)
        self.assertEqual(64, len(item["payload_hash"]))

        restored, status = self.service.resolve("snap-1", 7, 1, "restore")
        self.assertEqual(200, status)
        self.assertIsNotNone(restored["restored_at"])
        context = self.contexts.get("ctx-1", 7)
        self.assertTrue(context["dirty"])
        self.assertIn("unsaved", context["draft_payload"])

        conflict, status = self.service.resolve("snap-1", 7, 2, "restore")
        self.assertEqual(409, status)
        self.assertEqual("CONFLICT", conflict["code"])

    def test_discard_removes_snapshot_from_pending_list(self):
        _, status = self.service.create("snap-2", 7, {"payload": "draft", "reason": "SHUTDOWN"})
        self.assertEqual(200, status)
        discarded, status = self.service.resolve("snap-2", 7, 1, "discard")
        self.assertEqual(200, status)
        self.assertIsNotNone(discarded["discarded_at"])
        self.assertEqual([], self.service.list_pending(7))

    def test_stale_row_version_conflicts(self):
        self.service.create("snap-3", 7, {"payload": "draft", "reason": "CRASH"})
        result, status = self.service.resolve("snap-3", 7, 99, "restore")
        self.assertEqual(409, status)
        self.assertEqual("CONFLICT", result["code"])

    def test_shared_fixture_documents_terminal_states(self):
        fixture = json.loads((Path(__file__).parents[1] / "specs/golden/recovery-snapshot-transitions.json").read_text(encoding="utf-8"))
        names = {case["name"] for case in fixture["cases"]}
        self.assertEqual({"restore_pending", "discard_pending", "stale_restore", "restore_twice", "discard_after_restore"}, names)


if __name__ == "__main__":
    unittest.main()
