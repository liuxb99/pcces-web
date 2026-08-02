from __future__ import annotations

import unittest
from pathlib import Path


ROOT = Path(__file__).resolve().parents[1]


class CostStructureCanonicalContractTests(unittest.TestCase):
    def test_canonical_app_registers_all_phase4_blueprints(self):
        source = (ROOT / "api" / "app.py").read_text(encoding="utf-8")
        expected = {
            "build_cost_structure_blueprint",
            "build_cost_structure_detail_blueprint",
            "build_cost_structure_calculation_blueprint",
            "build_project_cost_structure_run_blueprint",
            "build_cost_structure_run_version_blueprint",
        }
        for symbol in expected:
            self.assertIn(symbol, source)
        for blueprint in {
            '"cost_structure"',
            '"cost_structure_details"',
            '"cost_structure_calculation"',
            '"project_cost_structure_run"',
            '"cost_structure_run_versions"',
        }:
            self.assertIn(blueprint, source)

    def test_frontend_client_matches_canonical_paths(self):
        source = (ROOT / "web-pcces" / "frontend" / "src" / "costStructureApi.ts").read_text(encoding="utf-8")
        self.assertIn("/api/cost-structures/runs/", source)
        self.assertIn("/budget-version", source)
        self.assertIn("/api/cost-structures/runs/compare", source)
        self.assertIn("left_budget_version_id", source)
        self.assertIn("right_budget_version_id", source)

    def test_version_service_exposes_get_and_compare_contracts(self):
        source = (ROOT / "api" / "cost_structure_run_versions.py").read_text(encoding="utf-8")
        self.assertIn('@bp.get("/runs/<run_id>/budget-version")', source)
        self.assertIn('@bp.get("/runs/compare")', source)
        self.assertIn("left and right run ids must be different", source)
        self.assertIn("runs must belong to the same project", source)


if __name__ == "__main__":
    unittest.main()
