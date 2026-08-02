import unittest

from sqlalchemy import create_engine
from sqlalchemy.orm import sessionmaker
from sqlalchemy.pool import StaticPool

from api.budget_approval import BudgetApprovalService
from api.budget_decimal import BudgetDecimalService
from api.budget_versioning import BudgetVersionService
from api.models import Base, User, UserRole


class BudgetApprovalTests(unittest.TestCase):
    def setUp(self):
        self.engine=create_engine("sqlite://",connect_args={"check_same_thread":False},poolclass=StaticPool,future=True)
        Base.metadata.create_all(self.engine)
        self.Session=sessionmaker(bind=self.engine)
        db=self.Session()
        db.add_all([
            User(id=1,username="editor",password_hash="x",display_name="Editor",role=UserRole.EDITOR.value,is_active=True),
            User(id=2,username="reviewer",password_hash="x",display_name="Reviewer",role=UserRole.REVIEWER.value,is_active=True),
            User(id=3,username="viewer",password_hash="x",display_name="Viewer",role=UserRole.VIEWER.value,is_active=True),
        ])
        db.commit();db.close()
        self.budget=BudgetDecimalService(self.engine);self.budget.create_schema()
        self.budget.save("I1",{"project_code":"P1","item_no":"1","name":"Item","kind":"L","quantity":"2","unit_price":"10","row_version":0})
        self.versions=BudgetVersionService(self.engine)
        self.service=BudgetApprovalService(self.engine,self.Session,self.versions)

    def test_submit_approve_creates_lock_snapshot_and_audit(self):
        submitted=self.service.transition("P1","SUBMIT",1,"ready",0)
        self.assertEqual(submitted["status"],"SUBMITTED")
        with self.assertRaises(PermissionError): self.service.assert_writable("P1")
        approved=self.service.transition("P1","APPROVE",2,"approved",1)
        self.assertEqual(approved["status"],"APPROVED")
        self.assertTrue(self.versions.lock_state("P1")["locked"])
        versions=self.versions.list_project("P1")
        self.assertEqual(versions[0]["status"],"APPROVED")
        audits=self.service.audits("P1")
        self.assertEqual([a["event_type"] for a in reversed(audits)], ["SUBMIT","APPROVE"])

    def test_return_unlocks_and_allows_resubmit(self):
        self.service.transition("P1","SUBMIT",1,None,0)
        returned=self.service.transition("P1","RETURN",2,"fix price",1)
        self.assertEqual(returned["status"],"RETURNED")
        self.assertFalse(self.versions.lock_state("P1")["locked"])
        self.service.assert_writable("P1")
        submitted=self.service.transition("P1","SUBMIT",1,"fixed",2)
        self.assertEqual(submitted["status"],"SUBMITTED")

    def test_role_separation_conflict_item_lock_and_autosave(self):
        with self.assertRaises(PermissionError): self.service.transition("P1","APPROVE",1,None,0)
        with self.assertRaises(PermissionError): self.service.set_item_lock("P1","I1",True,1,"review")
        lock=self.service.set_item_lock("P1","I1",True,2,"review")
        self.assertTrue(lock["locked"])
        with self.assertRaises(PermissionError): self.service.assert_writable("P1","I1")
        self.service.set_item_lock("P1","I1",False,2)
        conflict=self.service.autosave_check("P1","I1",1,2)
        self.assertFalse(conflict["allowed"])
        self.assertEqual(conflict["code"],"CONFLICT")
        self.assertTrue(self.service.autosave_check("P1","I1",2,2)["allowed"])
        with self.assertRaises(PermissionError): self.service.transition("P1","SUBMIT",3,None,0)

    def test_stale_approval_version_is_rejected(self):
        self.service.transition("P1","SUBMIT",1,None,0)
        with self.assertRaises(RuntimeError): self.service.transition("P1","RETURN",2,None,0)


if __name__=="__main__": unittest.main()
