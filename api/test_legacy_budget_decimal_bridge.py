import unittest

from flask import Flask
from sqlalchemy import create_engine, select
from sqlalchemy.orm import sessionmaker
from sqlalchemy.pool import StaticPool

from api.budget_decimal import budget_items_decimal, metadata as decimal_metadata
from api.legacy_budget_decimal_bridge import install_legacy_budget_bridge
from api.models import Base, BudgetItem, Project, User, UserRole


class LegacyBudgetDecimalBridgeTests(unittest.TestCase):
    def setUp(self):
        self.engine = create_engine(
            "sqlite+pysqlite:///:memory:", future=True,
            connect_args={"check_same_thread": False}, poolclass=StaticPool,
        )
        Base.metadata.create_all(self.engine)
        decimal_metadata.create_all(self.engine)
        self.sessions = sessionmaker(bind=self.engine)
        with self.sessions.begin() as session:
            session.add(User(
                id=1, username="admin", password_hash="x", display_name="Admin",
                role=UserRole.ADMIN.value, is_active=True,
            ))
            session.add(Project(id=1, code="P-001", name="Project", owner_id=1))

        app = Flask(__name__)

        def placeholder(*args, **kwargs):
            raise AssertionError("legacy placeholder must be replaced")

        app.add_url_rule("/api/projects/<int:project_id>/budget/", "get_budget_list", placeholder, methods=["GET"])
        app.add_url_rule("/api/projects/<int:project_id>/budget/", "create_budget_item", placeholder, methods=["POST"])
        app.add_url_rule("/api/projects/<int:project_id>/budget/tree", "get_budget_tree", placeholder, methods=["GET"])
        app.add_url_rule("/api/projects/<int:project_id>/budget/<int:item_id>", "update_budget_item", placeholder, methods=["PUT"])
        app.add_url_rule("/api/projects/<int:project_id>/budget/<int:item_id>", "delete_budget_item", placeholder, methods=["DELETE"])
        app.add_url_rule("/api/projects/<int:project_id>/budget/<int:item_id>/move", "move_budget_item", placeholder, methods=["POST"])
        app.add_url_rule("/api/projects/<int:project_id>/budget/recalc", "recalc_budget", placeholder, methods=["POST"])
        install_legacy_budget_bridge(app, self.engine, self.sessions)

        for endpoint in ("create_budget_item", "get_budget_list", "get_budget_tree", "recalc_budget"):
            view = app.view_functions[endpoint]
            app.view_functions[endpoint] = lambda project_id, view=view: view(project_id, 1)
        for endpoint in ("update_budget_item", "delete_budget_item", "move_budget_item"):
            view = app.view_functions[endpoint]
            app.view_functions[endpoint] = lambda project_id, item_id, view=view: view(project_id, item_id, 1)
        self.client = app.test_client()

    def _create(self, payload):
        response = self.client.post("/api/projects/1/budget/", json=payload)
        self.assertEqual(201, response.status_code, response.get_data(as_text=True))
        return response.get_json()

    def test_legacy_urls_dual_write_and_return_decimal_strings(self):
        item = self._create({
            "kind": "L", "c_name": "Concrete", "quantity": "1.005",
            "unit_price": "10", "decimal_qty": 3, "decimal_price": 2,
            "decimal_amount": 2,
        })
        self.assertEqual("10.05", item["amount"])
        self.assertTrue(item["decimal_core"])
        with self.engine.connect() as conn:
            shadow = conn.execute(select(budget_items_decimal)).mappings().one()
        self.assertEqual(f"legacy-{item['id']}", shadow["id"])
        self.assertEqual("10.05000000", str(shadow["amount"]))

    def test_tree_recalculate_update_move_and_delete_remain_compatible(self):
        parent = self._create({"kind": "B", "c_name": "Chapter", "decimal_amount": 2})
        child = self._create({
            "kind": "L", "c_name": "Work", "parent_id": parent["id"],
            "quantity": "2", "unit_price": "3.335", "decimal_amount": 2,
            "decimal_price": 3,
        })
        recalc = self.client.post("/api/projects/1/budget/recalc")
        self.assertEqual(200, recalc.status_code, recalc.get_data(as_text=True))
        self.assertEqual("6.67", recalc.get_json()["total_amount"])

        listing = self.client.get("/api/projects/1/budget/").get_json()
        by_id = {row["id"]: row for row in listing}
        self.assertEqual("6.67", by_id[parent["id"]]["amount"])

        updated = self.client.put(
            f"/api/projects/1/budget/{child['id']}",
            json={"quantity": "3", "row_version": by_id[child["id"]]["row_version"]},
        )
        self.assertEqual(200, updated.status_code, updated.get_data(as_text=True))
        self.assertEqual("10.01", updated.get_json()["amount"])

        tree = self.client.get("/api/projects/1/budget/tree").get_json()
        self.assertEqual(child["id"], tree[0]["children"][0]["id"])

        moved = self.client.post(
            f"/api/projects/1/budget/{child['id']}/move", json={"new_parent_id": None}
        )
        self.assertEqual(200, moved.status_code)
        deleted = self.client.delete(f"/api/projects/1/budget/{parent['id']}")
        self.assertEqual(200, deleted.status_code)
        remaining = self.client.get("/api/projects/1/budget/").get_json()
        self.assertEqual([child["id"]], [row["id"] for row in remaining])

    def test_invalid_formula_rolls_back_both_tables(self):
        response = self.client.post("/api/projects/1/budget/", json={
            "kind": "F", "c_name": "Fee", "quantity": "1", "unit_price": "1"
        })
        self.assertEqual(400, response.status_code)
        with self.sessions() as session:
            self.assertEqual(0, session.query(BudgetItem).count())
        with self.engine.connect() as conn:
            self.assertEqual([], conn.execute(select(budget_items_decimal)).all())

    def test_stale_decimal_row_version_rolls_back_legacy_update(self):
        item = self._create({"kind": "L", "c_name": "Work", "quantity": "2", "unit_price": "5"})
        response = self.client.put(
            f"/api/projects/1/budget/{item['id']}",
            json={"quantity": "9", "row_version": item["row_version"] + 1},
        )
        self.assertEqual(409, response.status_code)
        with self.sessions() as session:
            stored = session.query(BudgetItem).filter(BudgetItem.id == item["id"]).one()
            self.assertEqual(2.0, stored.quantity)


if __name__ == "__main__":
    unittest.main()
