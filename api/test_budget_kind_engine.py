import json
import unittest
from pathlib import Path

from api.budget_kind_engine import calculate_budget_kind


class BudgetKindEngineTests(unittest.TestCase):
    def test_shared_golden_cases(self):
        fixture = json.loads((Path(__file__).parents[1] / "specs/golden/budget-item-kind-calculations.json").read_text(encoding="utf-8"))
        for case in fixture["cases"]:
            with self.subTest(case=case["name"]):
                payload = {key: value for key, value in case.items() if key not in {"name", "kind", "scale", "expected", "steps"}}
                trace = calculate_budget_kind(case["kind"], payload, case["scale"])
                self.assertEqual(case["expected"], trace.result)
                self.assertEqual(case["steps"], [step.operation for step in trace.steps])

    def test_rejects_uncovered_tiers(self):
        with self.assertRaises(ValueError):
            calculate_budget_kind("S", {"base":"200", "tiers":[{"up_to":"100", "rate":"0.1"}]}, 2)

    def test_rejects_invalid_signed_term(self):
        with self.assertRaises(ValueError):
            calculate_budget_kind("U", {"terms":[{"sign":0, "amount":"1"}]}, 2)


if __name__ == "__main__":
    unittest.main()
