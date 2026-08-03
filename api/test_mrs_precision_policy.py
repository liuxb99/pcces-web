import unittest
from sqlalchemy import create_engine

from api.mrs_precision_policy import MRSPrecisionPolicyService


class MRSPrecisionPolicyTests(unittest.TestCase):
    def setUp(self):
        self.service = MRSPrecisionPolicyService(create_engine("sqlite+pysqlite:///:memory:", future=True))

    def test_legacy_defaults_keep_main_and_analysis_scales_separate(self):
        main = self.service.calculate("P1", "MAIN", "1.23456", "12.34567")
        analysis = self.service.calculate("P1", "ANALYSIS", "1.23456", "12.34567")
        self.assertEqual(("1.23", "12.35", "15"), (main["quantity"], main["unit_price"], main["amount"]))
        self.assertEqual(("1.2346", "12.3457", "15.24"), (analysis["quantity"], analysis["unit_price"], analysis["amount"]))

    def test_project_override_and_optimistic_lock(self):
        saved = self.service.save("P1", {
            "main_quantity_scale": 3, "main_price_scale": 2, "main_amount_scale": 1,
            "analysis_quantity_scale": 5, "analysis_price_scale": 4, "analysis_amount_scale": 3,
            "row_version": 0,
        }, "u1")
        self.assertEqual(1, saved["row_version"])
        result = self.service.calculate("P1", "ANALYSIS", "2.123456", "3.45678")
        self.assertEqual("2.12346", result["quantity"])
        self.assertEqual("3.4568", result["unit_price"])
        self.assertEqual("7.340", result["amount"])
        with self.assertRaises(RuntimeError):
            self.service.save("P1", {**saved, "row_version": 0}, "u2")

    def test_identical_or_out_of_range_policies_are_rejected(self):
        with self.assertRaises(ValueError):
            self.service.save("P1", {
                "main_quantity_scale": 2, "main_price_scale": 2, "main_amount_scale": 2,
                "analysis_quantity_scale": 2, "analysis_price_scale": 2, "analysis_amount_scale": 2,
            }, "u1")
        with self.assertRaises(ValueError):
            self.service.save("P2", {"analysis_quantity_scale": 9}, "u1")


if __name__ == "__main__":
    unittest.main()
