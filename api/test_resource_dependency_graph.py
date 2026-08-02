import unittest
from sqlalchemy import create_engine
from sqlalchemy.orm import sessionmaker

from api.budget_decimal import BudgetDecimalService
from api.models import Base, BudgetItem, Project, Resource
from api.resource_decimal import ResourceDecimalService
from api.resource_dependency_graph import ResourceDependencyGraphService


class ResourceDependencyGraphTests(unittest.TestCase):
    def setUp(self):
        self.engine = create_engine("sqlite+pysqlite:///:memory:", future=True)
        Base.metadata.create_all(self.engine)
        self.Session = sessionmaker(bind=self.engine)
        self.budget = BudgetDecimalService(self.engine); self.budget.create_schema()
        self.resource = ResourceDecimalService(self.engine); self.resource.create_schema()
        db = self.Session()
        project = Project(id=1, code="P1", name="Project", owner_id=1)
        resource = Resource(id=2, project_id=1, code="R01", c_name="Material", c_unit="kg", unit_price=10)
        item = BudgetItem(id=3, project_id=1, item_no="001", c_name="Work", kind="L", pcces_code="R01", quantity=3, unit_price=10, amount=30, decimal_qty=4, decimal_price=4, decimal_amount=2)
        db.add_all([project, resource, item]); db.commit(); db.close()
        self.budget.save("legacy-3", {"project_code":"P1","name":"Work","kind":"L","quantity":"3","unit_price":"10","quantity_scale":4,"price_scale":4,"amount_scale":2,"row_version":0})
        self.resource.save_resource("legacy-resource-2", {"code":"P1:R01","name":"Material","unit":"kg","unit_price":"12.3456","price_scale":4,"row_version":0})
        self.service = ResourceDependencyGraphService(self.engine, self.Session)

    def test_auto_link_local_recalc_history_and_graph(self):
        linked = self.service.auto_link_project(1)
        self.assertEqual(1, linked["matched_links"])
        self.assertEqual(1, self.service.auto_link_project(1)["matched_links"])
        history = self.service.record_price("P1", "legacy-resource-2", "10.0000", "12.3456", "TEST")
        self.assertIn("history=", history["deep_link"])
        run = self.service.recalculate_resource("P1", "legacy-resource-2")
        self.assertEqual("COMPLETED", run["status"])
        self.assertEqual(1, run["result"]["updated_items"])
        self.assertEqual("37.04", self.budget.get("legacy-3")["amount"])
        graph = self.service.graph("P1")
        self.assertEqual(2, len(graph["nodes"]))
        self.assertEqual(1, len(graph["edges"]))
        self.assertEqual(1, len(graph["price_history"]))
        self.assertEqual(1, len(graph["runs"]))

    def test_project_recalc_uses_distinct_resources(self):
        self.service.auto_link_project(1)
        run = self.service.recalculate_project("P1")
        self.assertEqual("PROJECT", run["scope"])
        self.assertEqual(1, run["result"]["resources"])
        self.assertEqual(1, run["result"]["updated_items"])

    def test_unchanged_price_does_not_create_history(self):
        self.assertIsNone(self.service.record_price("P1", "legacy-resource-2", "12.3456", "12.3456", "TEST"))
        self.assertEqual([], self.service.graph("P1")["price_history"])


if __name__ == "__main__": unittest.main()
