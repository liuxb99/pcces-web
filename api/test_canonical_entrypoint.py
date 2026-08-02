"""Regression tests for the canonical Web API entrypoint."""

import unittest

from api.app import app


class CanonicalEntrypointTests(unittest.TestCase):
    def setUp(self):
        app.config.update(TESTING=True)
        self.client = app.test_client()

    def test_health_remains_public(self):
        response = self.client.get("/api/health")
        self.assertEqual(200, response.status_code)

    def test_protected_route_does_not_fall_back_to_guest(self):
        response = self.client.get("/api/projects/")
        self.assertEqual(401, response.status_code)
        self.assertEqual("UNAUTHORIZED", response.get_json()["code"])

    def test_invalid_token_does_not_fall_back_to_guest(self):
        response = self.client.get(
            "/api/projects/",
            headers={"Authorization": "Bearer invalid-token"},
        )
        self.assertEqual(401, response.status_code)
        self.assertEqual("UNAUTHORIZED", response.get_json()["code"])

    def test_capability_endpoint_requires_authentication(self):
        response = self.client.get("/api/capabilities/BUD")
        self.assertEqual(401, response.status_code)

    def test_cors_preflight_is_not_blocked_by_authentication(self):
        response = self.client.options("/api/projects/")
        self.assertNotEqual(401, response.status_code)

    def test_deployment_uses_canonical_entrypoint(self):
        with open("vercel.json", "r", encoding="utf-8") as handle:
            deployment = handle.read()
        self.assertIn('"dest": "/api/app.py"', deployment)
        self.assertNotIn('"dest": "/api/index.py"', deployment)


if __name__ == "__main__":
    unittest.main()
