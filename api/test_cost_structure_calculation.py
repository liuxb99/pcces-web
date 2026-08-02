import unittest

from api.cost_structure_calculation import calculate_cost_structure


class CostStructureCalculationTests(unittest.TestCase):
    def test_percentage_order_and_adjustment(self):
        result = calculate_cost_structure([
            {"code":"MGT","kind":"MANAGEMENT","base_kind":"DIRECT","rate":"5","sign":1,"sort_order":10},
            {"code":"TAX","kind":"TAX","base_kind":"SUBTOTAL","rate":"5","sign":1,"sort_order":20},
            {"code":"DISC","kind":"ADJUSTMENT","base_kind":"FIXED","fixed_amount":"30","sign":-1,"sort_order":30},
        ], "1000", 2)
        self.assertEqual(result["total"], "1072.50")
        self.assertEqual(result["calculation_trace"]["order"], ["MGT", "TAX", "DISC"])
        self.assertEqual(result["lines"][1]["base_amount"], "1050.00")

    def test_previous_base_and_half_up_rounding(self):
        result = calculate_cost_structure([
            {"code":"A","kind":"PERCENT","base_kind":"DIRECT","rate":"3.333","sign":1,"sort_order":1},
            {"code":"B","kind":"PERCENT","base_kind":"PREVIOUS","rate":"10","sign":1,"sort_order":2},
        ], "100", 2)
        self.assertEqual(result["lines"][0]["amount"], "3.33")
        self.assertEqual(result["lines"][1]["amount"], "0.33")
        self.assertEqual(result["total"], "103.66")

    def test_rejects_duplicate_code_and_invalid_sign(self):
        with self.assertRaises(ValueError):
            calculate_cost_structure([
                {"code":"X","kind":"TAX","rate":"5"},
                {"code":"X","kind":"TAX","rate":"5"},
            ], "100")
        with self.assertRaises(ValueError):
            calculate_cost_structure([{"code":"X","kind":"TAX","rate":"5","sign":0}], "100")


if __name__ == "__main__":
    unittest.main()
