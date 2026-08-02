import json
import unittest
from pathlib import Path
from sqlalchemy import create_engine
from sqlalchemy.pool import StaticPool

from api.budget_decimal import BudgetDecimalService, calculate_leaf, calculate_rollup


class BudgetDecimalTest(unittest.TestCase):
    def test_shared_golden_calculations(self):
        fixture = json.loads((Path(__file__).parents[1] / "specs/golden/budget-decimal-calculations.json").read_text(encoding="utf-8"))
        for case in fixture["cases"]:
            with self.subTest(case=case["name"]):
                if case["kind"] == "B":
                    actual = calculate_rollup(case["children"], case["amount_scale"])
                elif case["kind"] == "RESOURCE":
                    parts = [calculate_leaf(x["quantity"], x["unit_price"], 8) for x in case["components"]]
                    actual = calculate_rollup(parts, case["amount_scale"])
                else:
                    actual = calculate_leaf(case["quantity"], case["unit_price"], case["amount_scale"])
                self.assertEqual(case["expected_amount"], actual)

    def test_persistence_rollup_and_conflict(self):
        engine = create_engine("sqlite://", connect_args={"check_same_thread": False}, poolclass=StaticPool)
        service = BudgetDecimalService(engine)
        service.create_schema()
        parent, status = service.save("P", {"project_code":"X","name":"Parent","kind":"B"})
        self.assertEqual(200, status)
        child1, _ = service.save("C1", {"project_code":"X","parent_id":"P","name":"One","kind":"L","quantity":"2","unit_price":"10.125"})
        service.save("C2", {"project_code":"X","parent_id":"P","name":"Two","kind":"L","quantity":"3","unit_price":"4.335"})
        result, status = service.recalculate_project("X")
        self.assertEqual(200, status)
        self.assertEqual("33.26", service.get("P")["amount"])
        conflict, status = service.save("C1", {"row_version": child1["row_version"] - 1, "name":"stale"})
        self.assertEqual(409, status)
        self.assertEqual("CONFLICT", conflict["code"])


if __name__ == "__main__":
    unittest.main()
