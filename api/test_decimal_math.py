import json
import unittest
from pathlib import Path

from api.decimal_math import DecimalValueError, multiply, quantize, sum_values


class DecimalGoldenTests(unittest.TestCase):
    def test_shared_decimal_cases(self):
        fixture_path = Path(__file__).resolve().parents[1] / "specs" / "golden" / "decimal-calculations.json"
        fixture = json.loads(fixture_path.read_text(encoding="utf-8"))
        for case in fixture["cases"]:
            with self.subTest(case=case["name"]):
                if case["operation"] == "quantize":
                    actual = quantize(case["value"], case["scale"])
                elif case["operation"] == "multiply":
                    actual = multiply(case["left"], case["right"], case["scale"])
                elif case["operation"] == "sum":
                    actual = sum_values(case["values"], case["scale"])
                else:
                    self.fail(f"unsupported fixture operation: {case['operation']}")
                self.assertEqual(case["expected"], actual)

    def test_binary_float_is_rejected(self):
        with self.assertRaises(DecimalValueError):
            quantize(1.005, 2)

    def test_scale_limit_is_enforced(self):
        with self.assertRaises(DecimalValueError):
            quantize("1.23", 9)


if __name__ == "__main__":
    unittest.main()
