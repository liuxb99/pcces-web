"""Permanent regression tests for canonical Web route authorization."""

import unittest

from sqlalchemy import create_engine
from sqlalchemy.pool import StaticPool

from api.authorization import AuthorizationService
from api.route_policy import action_for_request, initialize_authorization


class RoutePolicyTests(unittest.TestCase):
    def setUp(self):
        self.engine = create_engine(
            "sqlite://",
            connect_args={"check_same_thread": False},
            poolclass=StaticPool,
        )
        self.service = AuthorizationService(self.engine)
        initialize_authorization(self.service, [7])

    def test_primary_business_routes_map_to_shared_action_codes(self):
        cases = {
            "/api/projects/": "PROJECT_CATALOG",
            "/api/budget/items": "BUD",
            "/api/bid/42": "BID",
            "/api/mrs/items": "MRS",
            "/api/resources/9": "MRS",
            "/api/dependency-graph/projects/P1": "MRS",
            "/api/reports/budget": "REPORT",
            "/api/admin/users": "SYSTEM_ADMIN",
            "/api/contracts/3": "SPLIT_CONTRACT",
            "/api/invoices/2": "INVOICE",
            "/api/budget-changes/1": "BUDGET_CHANGE",
            "/api/settlements/1": "SUB_CLOSE",
            "/api/final-acceptance/1": "SUB_FINAL",
        }
        for path, expected in cases.items():
            with self.subTest(path=path):
                self.assertEqual(action_for_request(path, "GET"), expected)

    def test_public_and_unmapped_paths_do_not_invent_permissions(self):
        self.assertIsNone(action_for_request("/api/health", "GET"))
        self.assertIsNone(action_for_request("/api/auth/login", "POST"))
        self.assertIsNone(action_for_request("/not-api", "GET"))

    def test_existing_user_receives_explicit_bootstrap_grants(self):
        decision = self.service.decide(7, "PROJECT_CATALOG")
        self.assertTrue(decision.allowed)
        self.assertEqual(decision.function_code, "F005")

    def test_revoked_function_blocks_direct_project_route(self):
        self.service.set_function_grant(7, "F005", False)
        action = action_for_request("/api/projects/", "GET")
        decision = self.service.decide(7, action)
        self.assertFalse(decision.allowed)
        self.assertEqual(decision.reason, "FUNCTION_NOT_GRANTED")

    def test_revoked_mrs_blocks_dependency_graph_direct_url(self):
        self.service.set_function_grant(7, "F007", False)
        action = action_for_request("/api/dependency-graph/projects/P1", "GET")
        decision = self.service.decide(7, action)
        self.assertFalse(decision.allowed)
        self.assertEqual(decision.reason, "FUNCTION_NOT_GRANTED")

    def test_disabled_module_blocks_budget_and_cannot_be_bypassed_by_method(self):
        self.service.set_module_entitlement(7, "BUDGET", False)
        for method in ("GET", "POST", "PUT", "DELETE"):
            with self.subTest(method=method):
                action = action_for_request("/api/budget/items/1", method)
                decision = self.service.decide(7, action)
                self.assertFalse(decision.allowed)
                self.assertEqual(decision.reason, "MODULE_NOT_ENTITLED")

    def test_catalog_initialization_is_idempotent(self):
        initialize_authorization(self.service, [7])
        self.assertTrue(self.service.decide(7, "INVOICE").allowed)


if __name__ == "__main__":
    unittest.main()
