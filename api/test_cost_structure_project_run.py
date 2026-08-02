from __future__ import annotations

import unittest
from sqlalchemy import create_engine

from api.cost_structure import CostStructureService
from api.cost_structure_details import CostStructureDetailService
from api.cost_structure_project_run import ProjectCostStructureRunService


class ProjectCostStructureRunTest(unittest.TestCase):
    def setUp(self):
        self.engine = create_engine("sqlite:///:memory:")
        self.types = CostStructureService(self.engine)
        self.details = CostStructureDetailService(self.engine)
        self.runs = ProjectCostStructureRunService(self.engine)
        self.types.save_type("T1", {"code":"STD","name":"標準","enabled":True,"row_version":0}, "u1")
        self.types.assign_project("P1", "T1", "BUD", 0, "u1")
        self.details.import_categories("T1", [
            {"code":"MGT","name":"管理費","kind":"MANAGEMENT","sort_order":10,"rate":"10","enabled":True},
            {"code":"TAX","name":"稅費","kind":"TAX","sort_order":20,"rate":"5","enabled":True},
        ], False, "u1")

    def test_recalculate_aggregates_budget_and_persists_snapshot(self):
        result = self.runs.recalculate("P1", [
            {"id":"A","quantity":"2","unit_price":"100"},
            {"id":"B","amount":"300"},
            {"id":"S","kind":"SECTION","amount":"9999"},
        ], 2, "u1")
        self.assertEqual("500.00", result["direct_cost"])
        self.assertEqual("577.50", result["total"])
        self.assertEqual(["MGT", "TAX"], result["result"]["calculation_trace"]["order"])
        self.assertEqual(3, len(result["budget_snapshot"]))
        again = self.runs.get(result["id"])
        self.assertEqual(result["total"], again["total"])
        self.assertIn("project=P1", again["deep_link"])

    def test_requires_assignment_and_categories(self):
        with self.assertRaises(LookupError):
            self.runs.recalculate("NOPE", [], 2, "u1")
        self.types.assign_project("P2", "T1", "BUD", 0, "u1")
        self.details.import_categories("T1", [], False, "u1")
        with self.assertRaises(ValueError):
            self.runs.recalculate("P2", [], 2, "u1")


if __name__ == "__main__":
    unittest.main()
