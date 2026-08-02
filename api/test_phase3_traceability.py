import re
import unittest
from pathlib import Path


class Phase3TraceabilityGateTests(unittest.TestCase):
    MATRIX = Path("docs/development/phase-3-mrs-traceability-matrix.md")

    def test_all_phase3_features_are_verified(self):
        text = self.MATRIX.read_text(encoding="utf-8")
        rows = re.findall(r"^\| (P3-MRS-\d{2}) \|.*\| (VERIFIED) \|$", text, re.MULTILINE)
        self.assertEqual(20, len(rows), "Phase 3 must contain 20 verified feature rows")
        self.assertEqual([f"P3-MRS-{i:02d}" for i in range(1, 21)], [feature for feature, _ in rows])
        self.assertNotRegex(text, r"\b(?:PARTIAL|STUB|TODO|NOT_STARTED)\b")

    def test_matrix_contains_required_legacy_and_runtime_evidence(self):
        text = self.MATRIX.read_text(encoding="utf-8")
        required = (
            "frmMrsBase.cs",
            "FormMrsBaseBreakdown.cs",
            "FormMrsBase_ExpWizard.cs",
            "FormBudgetRes.cs",
            "MrsBase.Bookmark",
            "Web/Python",
            "Local Go",
            "永久測試",
            "Phase 4",
        )
        for value in required:
            with self.subTest(value=value):
                self.assertIn(value, text)

    def test_phase3_completion_artifacts_exist(self):
        paths = (
            "api/mrs_catalog.py",
            "api/mrs_code.py",
            "api/mrs_history_apply.py",
            "api/mrs_precision_policy.py",
            "api/mrs_excel_export.py",
            "api/mrs_project_state.py",
            "api/resource_budget_links.py",
            "api/resource_operations.py",
            "api/resource_project_reference.py",
            "pcces-go/internal/platform/httpapi/mrs_catalog_handlers.go",
            ".github/workflows/decimal-core-integration.yml",
        )
        missing = [path for path in paths if not Path(path).is_file()]
        self.assertEqual([], missing, f"missing Phase 3 completion artifacts: {missing}")


if __name__ == "__main__":
    unittest.main()
