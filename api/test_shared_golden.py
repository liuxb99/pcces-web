import json
import unittest
from pathlib import Path

from api.budget_kind_engine import calculate_budget_kind


FIXTURE = Path(__file__).resolve().parents[1] / "tests" / "golden" / "core_financial.json"


class SharedGoldenWebTest(unittest.TestCase):
    def test_source_derived_financial_cases(self):
        data = json.loads(FIXTURE.read_text(encoding="utf-8"))
        self.assertEqual(data["evidence_type"], "SOURCE_DERIVED_GOLDEN")
        self.assertTrue(data["legacy_sources"])
        for case in data["cases"]:
            with self.subTest(case=case["id"]):
                trace = calculate_budget_kind(case["kind"], case["input"], case["scale"])
                self.assertEqual(trace.result, case["expected"])
                self.assertTrue(trace.steps)


if __name__ == "__main__":
    unittest.main()
