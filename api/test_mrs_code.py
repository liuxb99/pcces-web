import unittest

from api.mrs_code import canonical_unit, fit_code, validate_code


class MRSCodeTests(unittest.TestCase):
    def test_legacy_prefix_lengths_and_units(self):
        cases = [
            ("1234567890", "平方公尺", "WORK_ITEM", True, "12345", "M2"),
            ("M1234567890", "kg", "MATERIAL", True, "12345", "KG"),
            ("L123456789012", "公尺", "LABOR", True, "", "M"),
            ("E123456789012", "m3", "EQUIPMENT", True, "", "M3"),
            ("W1234567890", "噸", "OTHER", True, "", "T"),
        ]
        for code, unit, kind, valid, chapter, canonical in cases:
            result = validate_code(code, unit)
            self.assertEqual(result["resource_type"], kind)
            self.assertEqual(result["valid"], valid)
            self.assertEqual(result["chapter_code"], chapter)
            self.assertEqual(result["canonical_unit"], canonical)

    def test_invalid_prefix_and_short_code(self):
        invalid = validate_code("X123", "")
        self.assertFalse(invalid["valid"])
        self.assertIn("非正常編碼", invalid["errors"][0])
        short = validate_code("M123", "公斤")
        self.assertFalse(short["valid"])
        self.assertEqual(short["resource_type"], "MATERIAL")
        self.assertEqual(short["canonical_unit"], "KG")

    def test_fit_normalizes_code_and_unit(self):
        result = fit_code(" m12345 67890 ", "平方米")
        self.assertEqual(result["fitted_code"], "M1234567890")
        self.assertEqual(result["canonical_unit"], "M2")
        self.assertTrue(result["changed"])
        self.assertEqual(len(result["warnings"]), 2)
        self.assertEqual(canonical_unit("兛"), "KG")


if __name__ == "__main__":
    unittest.main()
