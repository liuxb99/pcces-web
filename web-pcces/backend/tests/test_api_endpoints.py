"""PCCES API 端點測試（使用 Flask test client）"""

import os
import sys
import json
import tempfile
import pytest

sys.path.insert(0, os.path.join(os.path.dirname(__file__), '..'))

# 測試用環境變數（在 import main 前設定）
os.environ["PCCES_SECRET_KEY"] = "test-secret-key-for-testing"
os.environ["PCCES_DATABASE_URL"] = "sqlite:///./test_api_pcces.db"
os.environ["PCCES_REPORT_DIR"] = tempfile.mkdtemp()

from app.models import Base
from main import app, init_db, engine


@pytest.fixture(autouse=True)
def setup_db():
    """每個測試前重建資料庫"""
    Base.metadata.drop_all(engine)
    Base.metadata.create_all(engine)
    yield
    Base.metadata.drop_all(engine)


@pytest.fixture
def client():
    """提供 Flask test client"""
    app.config["TESTING"] = True
    with app.test_client() as c:
        yield c


def register_user(client, username="test", password="pass123", display_name="測試"):
    """註冊使用者並回傳 token"""
    res = client.post("/api/auth/register", json={
        "username": username, "password": password,
        "display_name": display_name,
    })
    return res.get_json()


# ═══════════════════════════════════════════════
# 認證 API 測試
# ═══════════════════════════════════════════════

class TestAuthAPI:
    def test_register(self, client):
        res = client.post("/api/auth/register", json={
            "username": "newuser", "password": "pass123", "display_name": "新使用者",
        })
        assert res.status_code == 201
        data = res.get_json()
        assert "access_token" in data
        assert data["user"]["username"] == "newuser"

    def test_register_duplicate(self, client):
        client.post("/api/auth/register", json={
            "username": "dup", "password": "pass123", "display_name": "Dup",
        })
        res = client.post("/api/auth/register", json={
            "username": "dup", "password": "pass456", "display_name": "Dup2",
        })
        assert res.status_code == 400

    def test_login(self, client):
        client.post("/api/auth/register", json={
            "username": "loginuser", "password": "mypass", "display_name": "Login",
        })
        res = client.post("/api/auth/login", json={
            "username": "loginuser", "password": "mypass",
        })
        assert res.status_code == 200
        assert "access_token" in res.get_json()

    def test_login_wrong_password(self, client):
        client.post("/api/auth/register", json={
            "username": "u1", "password": "correct", "display_name": "U1",
        })
        res = client.post("/api/auth/login", json={
            "username": "u1", "password": "wrong",
        })
        assert res.status_code == 401

    def test_health(self, client):
        res = client.get("/api/health")
        assert res.status_code == 200
        assert res.get_json()["status"] == "ok"


# ═══════════════════════════════════════════════
# 專案 API 測試
# ═══════════════════════════════════════════════

class TestProjectAPI:
    def _auth_headers(self, client):
        data = register_user(client)
        return {"Authorization": f"Bearer {data['access_token']}"}

    def test_create_project(self, client):
        headers = self._auth_headers(client)
        res = client.post("/api/projects/", json={
            "code": "P001", "name": "測試專案",
        }, headers=headers)
        assert res.status_code == 201
        assert res.get_json()["code"] == "P001"

    def test_list_projects(self, client):
        headers = self._auth_headers(client)
        res = client.get("/api/projects/", headers=headers)
        assert res.status_code == 200
        assert isinstance(res.get_json(), list)

    def test_data_isolation(self, client):
        # user1 建立專案
        r1 = register_user(client, "user1", "pass1", "User1")
        h1 = {"Authorization": f"Bearer {r1['access_token']}"}
        client.post("/api/projects/", json={"code": "U1P1", "name": "User1的專案"}, headers=h1)

        # user2 看不到 user1 的專案
        r2 = register_user(client, "user2", "pass2", "User2")
        h2 = {"Authorization": f"Bearer {r2['access_token']}"}
        res = client.get("/api/projects/", headers=h2)
        projects = res.get_json()
        assert len(projects) == 0  # 資料隔離

    def test_ownership(self, client):
        r1 = register_user(client, "owner", "pass", "Owner")
        h1 = {"Authorization": f"Bearer {r1['access_token']}"}
        res = client.post("/api/projects/", json={"code": "OWN", "name": "我的專案"}, headers=h1)
        pid = res.get_json()["id"]

        # user2 不能刪除
        r2 = register_user(client, "attacker", "pass", "Attacker")
        h2 = {"Authorization": f"Bearer {r2['access_token']}"}
        res = client.delete(f"/api/projects/{pid}", headers=h2)
        assert res.status_code == 403

    def test_dashboard_stats(self, client):
        headers = self._auth_headers(client)
        res = client.get("/api/projects/stats", headers=headers)
        assert res.status_code == 200
        data = res.get_json()
        assert "total_projects" in data
        assert "active_projects" in data


# ═══════════════════════════════════════════════
# 預算項目 API 測試
# ═══════════════════════════════════════════════

class TestBudgetAPI:
    def _setup(self, client):
        data = register_user(client)
        headers = {"Authorization": f"Bearer {data['access_token']}"}
        res = client.post("/api/projects/", json={"code": "BGT", "name": "預算測試"}, headers=headers)
        pid = res.get_json()["id"]
        return headers, pid

    def test_create_b_item(self, client):
        headers, pid = self._setup(client)
        res = client.post(f"/api/projects/{pid}/budget/", json={
            "c_name": "直接費", "kind": "B", "print_no": "0001",
        }, headers=headers)
        assert res.status_code == 201
        assert res.get_json()["amount"] == 0.0

    def test_create_w_item(self, client):
        headers, pid = self._setup(client)
        res = client.post(f"/api/projects/{pid}/budget/", json={
            "c_name": "開挖", "kind": "W", "quantity": 50, "unit_price": 1000,
        }, headers=headers)
        assert res.status_code == 201
        assert res.get_json()["amount"] == 50000.0

    def test_budget_tree(self, client):
        headers, pid = self._setup(client)
        # 建立根 + 子
        r = client.post(f"/api/projects/{pid}/budget/", json={
            "c_name": "總表", "kind": "B",
        }, headers=headers)
        root_id = r.get_json()["id"]
        client.post(f"/api/projects/{pid}/budget/", json={
            "parent_id": root_id, "c_name": "子項", "kind": "W",
            "quantity": 10, "unit_price": 500,
        }, headers=headers)

        res = client.get(f"/api/projects/{pid}/budget/tree", headers=headers)
        assert res.status_code == 200
        tree = res.get_json()
        assert len(tree) == 1
        assert len(tree[0]["children"]) == 1

    def test_budget_recalc(self, client):
        headers, pid = self._setup(client)
        r = client.post(f"/api/projects/{pid}/budget/", json={
            "c_name": "總表", "kind": "B",
        }, headers=headers)
        root_id = r.get_json()["id"]
        client.post(f"/api/projects/{pid}/budget/", json={
            "parent_id": root_id, "c_name": "工項", "kind": "W",
            "quantity": 100, "unit_price": 300,
        }, headers=headers)

        res = client.post(f"/api/projects/{pid}/budget/recalc", json={}, headers=headers)
        assert res.status_code == 200

    def test_unauthorized_access(self, client):
        """未授權使用者無法存取預算"""
        headers, pid = self._setup(client)
        # 第二個使用者
        r2 = register_user(client, "other", "pass", "Other")
        h2 = {"Authorization": f"Bearer {r2['access_token']}"}
        res = client.get(f"/api/projects/{pid}/budget/tree", headers=h2)
        assert res.status_code == 403


# ═══════════════════════════════════════════════
# 資源 API 測試
# ═══════════════════════════════════════════════

class TestResourceAPI:
    def _setup(self, client):
        data = register_user(client)
        headers = {"Authorization": f"Bearer {data['access_token']}"}
        res = client.post("/api/projects/", json={"code": "RES", "name": "資源測試"}, headers=headers)
        pid = res.get_json()["id"]
        return headers, pid

    def test_create_resource(self, client):
        headers, pid = self._setup(client)
        res = client.post(f"/api/projects/{pid}/resources/", json={
            "code": "M001", "c_name": "鋼筋", "c_unit": "噸",
            "category": "material", "unit_price": 25000,
        }, headers=headers)
        assert res.status_code == 201
        assert res.get_json()["unit_price"] == 25000

    def test_list_resources(self, client):
        headers, pid = self._setup(client)
        res = client.get(f"/api/projects/{pid}/resources/", headers=headers)
        assert res.status_code == 200


# ═══════════════════════════════════════════════
# 報表 API 測試
# ═══════════════════════════════════════════════

class TestReportAPI:
    def _setup(self, client):
        data = register_user(client)
        headers = {"Authorization": f"Bearer {data['access_token']}"}
        res = client.post("/api/projects/", json={"code": "RPT", "name": "報表測試"}, headers=headers)
        pid = res.get_json()["id"]
        return headers, pid

    def test_summary_report(self, client):
        headers, pid = self._setup(client)
        res = client.get(f"/api/projects/{pid}/reports/summary", headers=headers)
        assert res.status_code == 200
        data = res.get_json()
        assert "total_amount" in data
        assert "item_count" in data

    def test_excel_export(self, client):
        headers, pid = self._setup(client)
        res = client.get(f"/api/projects/{pid}/reports/excel", headers=headers)
        assert res.status_code == 200
        assert res.content_type.startswith("application/vnd.openxmlformats")
