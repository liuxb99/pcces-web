from __future__ import annotations

import unittest

from sqlalchemy import create_engine, select
from sqlalchemy.orm import Session

from api.authorization import AuthorizationService, user_function_grants, user_module_entitlements
from api.models import Base, User
from api.route_policy import initialize_authorization


class NewUserAuthorizationProvisioningTest(unittest.TestCase):
    def setUp(self) -> None:
        self.engine = create_engine("sqlite+pysqlite:///:memory:")
        Base.metadata.create_all(self.engine)
        self.service = AuthorizationService(self.engine)
        initialize_authorization(self.service, [])

    def tearDown(self) -> None:
        self.engine.dispose()

    def test_new_user_receives_default_entitlements_in_insert_transaction(self) -> None:
        with Session(self.engine) as session:
            user = User(
                username="new-user",
                password_hash="test",
                display_name="New User",
            )
            session.add(user)
            session.commit()
            user_id = user.id

        decision = self.service.decide(user_id, "PROJECT_CATALOG")
        self.assertTrue(decision.allowed)
        self.assertEqual(decision.reason, "")

        with self.engine.connect() as conn:
            modules = conn.execute(
                select(user_module_entitlements).where(user_module_entitlements.c.user_id == user_id)
            ).mappings().all()
            functions = conn.execute(
                select(user_function_grants).where(user_function_grants.c.user_id == user_id)
            ).mappings().all()
        self.assertGreaterEqual(len(modules), 4)
        self.assertGreaterEqual(len(functions), 12)
        self.assertTrue(all(row["enabled"] for row in modules))
        self.assertTrue(all(row["granted"] for row in functions))


if __name__ == "__main__":
    unittest.main()
