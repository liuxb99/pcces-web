import unittest
from sqlalchemy import create_engine
from api.mrs_project_state import MRSProjectStateService

class MRSProjectStateTests(unittest.TestCase):
    def setUp(self):
        self.service = MRSProjectStateService(create_engine("sqlite+pysqlite:///:memory:", future=True))

    def test_default_is_writable_draft(self):
        state = self.service.get("P1")
        self.assertEqual("DRAFT", state["state"])
        self.assertFalse(state["effective_readonly"])
        self.service.assert_writable("P1")

    def test_approved_template_and_manual_readonly_are_guarded(self):
        submitted = self.service.save("P1", {"state":"SUBMITTED","row_version":0}, "u1")
        approved = self.service.save("P1", {"state":"APPROVED","row_version":submitted["row_version"]}, "u1")
        self.assertTrue(approved["effective_readonly"])
        with self.assertRaises(PermissionError): self.service.assert_writable("P1")
        template = self.service.save("T1", {"state":"DRAFT","template":True,"row_version":0}, "u1")
        self.assertTrue(template["effective_readonly"])
        manual = self.service.save("R1", {"state":"DRAFT","readonly":True,"row_version":0}, "u1")
        self.assertTrue(manual["effective_readonly"])

    def test_invalid_transition_and_stale_version_rejected(self):
        with self.assertRaises(ValueError): self.service.save("P1", {"state":"APPROVED","row_version":0}, "u1")
        first = self.service.save("P1", {"state":"SUBMITTED","row_version":0}, "u1")
        with self.assertRaises(RuntimeError): self.service.save("P1", {"state":"DRAFT","row_version":0}, "u1")
        with self.assertRaises(ValueError): self.service.save("P1", {"state":"ARCHIVED","row_version":first["row_version"]}, "u1")

    def test_template_cannot_be_approved(self):
        state = self.service.save("P1", {"state":"DRAFT","template":True,"row_version":0}, "u1")
        with self.assertRaises(ValueError):
            self.service.save("P1", {"state":"SUBMITTED","template":True,"row_version":state["row_version"]}, "u1")

if __name__ == "__main__": unittest.main()
