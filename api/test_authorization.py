import unittest

from flask import Flask
from sqlalchemy import create_engine
from sqlalchemy.pool import StaticPool

from api.authorization import (
    AuthorizationService,
    build_authorization_blueprint,
    require_capability,
)


class AuthorizationTests(unittest.TestCase):
    def setUp(self):
        engine = create_engine(
            "sqlite://",
            connect_args={"check_same_thread": False},
            poolclass=StaticPool,
        )
        self.service = AuthorizationService(engine)
        self.service.create_schema()
        self.service.seed_catalog(
            [{"code": "BUDGET", "name": "Budget", "enabled": True}],
            [{"code": "F003", "name": "Budget Editor", "enabled": True}],
            [{"code": "BUD", "name": "Open Budget", "module_code": "BUDGET", "function_code": "F003"}],
        )
        self.service.set_module_entitlement(7, "BUDGET", True)
        self.service.set_function_grant(7, "F003", True)

    def test_allowed_requires_module_and_function(self):
        decision = self.service.decide(7, "BUD")
        self.assertTrue(decision.allowed)
        self.assertEqual("", decision.reason)

        self.service.set_function_grant(7, "F003", False)
        decision = self.service.decide(7, "BUD")
        self.assertFalse(decision.allowed)
        self.assertEqual("FUNCTION_NOT_GRANTED", decision.reason)

    def test_module_entitlement_denial(self):
        self.service.set_module_entitlement(7, "BUDGET", False)
        decision = self.service.decide(7, "BUD")
        self.assertFalse(decision.allowed)
        self.assertEqual("MODULE_NOT_ENTITLED", decision.reason)

    def test_unknown_action_is_denied(self):
        decision = self.service.decide(7, "UNKNOWN")
        self.assertFalse(decision.allowed)
        self.assertEqual("ACTION_NOT_FOUND", decision.reason)

    def test_direct_route_bypass_is_blocked(self):
        app = Flask(__name__)
        current_user = {"id": 7}
        resolver = lambda: current_user["id"]
        app.register_blueprint(build_authorization_blueprint(self.service, resolver))

        @app.get("/api/protected-budget")
        @require_capability(self.service, "BUD", resolver)
        def protected_budget():
            return {"status": "ok"}

        client = app.test_client()
        self.assertEqual(200, client.get("/api/protected-budget").status_code)

        self.service.set_function_grant(7, "F003", False)
        response = client.get("/api/protected-budget")
        self.assertEqual(403, response.status_code)
        self.assertEqual("FUNCTION_NOT_GRANTED", response.get_json()["reason"])

    def test_missing_identity_is_unauthorized(self):
        app = Flask(__name__)
        resolver = lambda: None
        app.register_blueprint(build_authorization_blueprint(self.service, resolver))
        response = app.test_client().get("/api/capabilities/BUD")
        self.assertEqual(401, response.status_code)


if __name__ == "__main__":
    unittest.main()
