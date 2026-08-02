"""Cross-target authorization golden tests for the Web implementation."""

from __future__ import annotations

import json
import unittest
from pathlib import Path

from sqlalchemy import create_engine, delete, update
from sqlalchemy.pool import StaticPool

from api.authorization import (
    AuthorizationService,
    function_codes,
    modules,
    user_function_grants,
    user_module_entitlements,
)
from api.route_policy import ACTION_ROWS, FUNCTION_ROWS, MODULE_ROWS


ROOT = Path(__file__).resolve().parents[1]
GOLDEN_PATH = ROOT / "specs" / "golden" / "authorization-decisions.json"
CATALOG_PATH = ROOT / "specs" / "catalog" / "phase0-action-catalog.json"


class AuthorizationGoldenTests(unittest.TestCase):
    def setUp(self):
        self.engine = create_engine(
            "sqlite+pysqlite:///:memory:",
            future=True,
            connect_args={"check_same_thread": False},
            poolclass=StaticPool,
        )
        self.service = AuthorizationService(self.engine)
        self.service.create_schema()
        self.service.seed_catalog(MODULE_ROWS, FUNCTION_ROWS, ACTION_ROWS)
        self.user_id = 9001

    def test_web_decisions_match_shared_golden_fixture(self):
        fixture = json.loads(GOLDEN_PATH.read_text(encoding="utf-8"))
        for case in fixture["cases"]:
            with self.subTest(case=case["name"]):
                self._reset_case(case)
                decision = self.service.decide(self.user_id, case["action_code"])
                expected = case["expected"]
                self.assertEqual(expected["allowed"], decision.allowed)
                self.assertEqual(expected["reason"], decision.reason)
                self.assertEqual(expected["module_code"], decision.module_code)
                self.assertEqual(expected["function_code"], decision.function_code)

    def test_web_catalog_matches_shared_catalog(self):
        catalog = json.loads(CATALOG_PATH.read_text(encoding="utf-8"))
        expected_modules = {(row["code"], row["name"]) for row in catalog["modules"]}
        expected_functions = {(row["code"], row["name"]) for row in catalog["function_codes"]}
        expected_actions = {
            (row["code"], row["name"], row["module_code"], row["function_code"])
            for row in catalog["actions"]
        }
        self.assertEqual(expected_modules, {(row["code"], row["name"]) for row in MODULE_ROWS})
        self.assertEqual(expected_functions, {(row["code"], row["name"]) for row in FUNCTION_ROWS})
        self.assertEqual(
            expected_actions,
            {(row["code"], row["name"], row["module_code"], row["function_code"]) for row in ACTION_ROWS},
        )

    def _reset_case(self, case: dict) -> None:
        with self.engine.begin() as conn:
            conn.execute(update(modules).values(enabled=True))
            conn.execute(update(function_codes).values(enabled=True))
            conn.execute(delete(user_module_entitlements).where(user_module_entitlements.c.user_id == self.user_id))
            conn.execute(delete(user_function_grants).where(user_function_grants.c.user_id == self.user_id))

            action = next((row for row in ACTION_ROWS if row["code"] == case["action_code"]), None)
            if action is None:
                return

            conn.execute(
                update(modules)
                .where(modules.c.code == action["module_code"])
                .values(enabled=case["module_enabled"])
            )
            conn.execute(user_module_entitlements.insert().values(
                user_id=self.user_id,
                module_code=action["module_code"],
                enabled=case["module_entitled"],
            ))

            function_code = action["function_code"]
            if function_code is None:
                return
            conn.execute(
                update(function_codes)
                .where(function_codes.c.code == function_code)
                .values(enabled=case["function_enabled"])
            )
            conn.execute(user_function_grants.insert().values(
                user_id=self.user_id,
                function_code=function_code,
                granted=case["function_granted"],
            ))


if __name__ == "__main__":
    unittest.main()
