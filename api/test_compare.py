"""比較分析 API 測試"""

import pytest
import json
import tempfile
import os

from api.index import app, init_db, get_password_hash, _compare_budget_items_core, SessionLocal
from api.models import Base, User, Project, BudgetItem, BudgetItemKind, MrsBaseCategory, MrsBaseItem, MrsBaseBreakdownItem
from sqlalchemy import create_engine
from sqlalchemy.orm import sessionmaker


# ─── 輔助：將 Flask app 指向測試資料庫 ───

@pytest.fixture
def app_with_test_db(request):
    """將 Flask app 的資料庫連線指向測試用的記憶體 SQLite。
    此 fixture 會設立 engine/SessionLocal 覆蓋，並在結束時還原。
    測試結束後自動清理 app._db_initialized 以便下次重新初始化。"""
    from api import index as api_module
    orig_engine = api_module.engine
    orig_session = api_module.SessionLocal

    test_engine = create_engine("sqlite:///:memory:", echo=False)
    Base.metadata.create_all(test_engine)
    test_session_local = sessionmaker(bind=test_engine)
    api_module.engine = test_engine
    api_module.SessionLocal = test_session_local

    # 清除 _db_initialized 旗標，讓 _ensure_db 重新初始化
    if hasattr(app, '_db_initialized'):
        del app._db_initialized

    yield test_session_local

    api_module.engine = orig_engine
    api_module.SessionLocal = orig_session


@pytest.fixture
def client(app_with_test_db):
    """提供測試用的 Flask test client（已使用測試資料庫）"""
    app.config['TESTING'] = True
    with app.test_client() as c:
        yield c


@pytest.fixture
def empty_db(app_with_test_db):
    """回傳一個空的測試 session（用於需要自行建立資料的測試）"""
    session = app_with_test_db()
    yield session
    session.close()


@pytest.fixture
def seeded_db(app_with_test_db, request):
    """建立含示範資料的測試資料庫（透過 app 的 _ensure_db 自動建立），
    並回傳 session 供 test 直接查詢。"""
    # 觸發 _ensure_db（利用 test client 發送請求）
    with app.test_client() as c:
        c.get("/api/health")

    # 現在 app 的資料庫已有 seed 資料
    session = app_with_test_db()
    yield session
    session.close()


# ═══════════════════════════════════════════════
# 自訂 seed：測試用雙專案比對資料
# ═══════════════════════════════════════════════

@pytest.fixture
def compare_seed_db(app_with_test_db):
    """建立測試用的雙專案比對資料（不使用 seed_demo_data）"""
    session = app_with_test_db()

    # 建立使用者
    demo = User(
        username="demo",
        password_hash=get_password_hash("demo123"),
        display_name="示範使用者",
    )
    session.add(demo)
    session.commit()

    # 建立兩個專案
    proj_a = Project(code="PA001", name="專案 A", owner_id=demo.id)
    session.add(proj_a)
    session.flush()

    proj_b = Project(code="PB001", name="專案 B", owner_id=demo.id)
    session.add(proj_b)
    session.flush()

    # 專案 A 的預算項目
    direct_a = BudgetItem(
        project_id=proj_a.id, c_name="直接工程費",
        kind=BudgetItemKind.B, print_no="0001",
    )
    session.add(direct_a)
    session.flush()

    items_a = [
        ("0001.01", "鋼筋工程", "噸", 850, 28500, 24225000),
        ("0001.02", "混凝土工程", "m³", 5200, 2100, 10920000),
        ("0001.03", "模板工程", "㎡", 8500, 680, 5780000),
    ]
    for pn, name, unit, qty, price, amt in items_a:
        session.add(BudgetItem(
            project_id=proj_a.id, parent_id=direct_a.id,
            c_name=name, c_unit=unit, kind=BudgetItemKind.W,
            print_no=pn, quantity=qty, unit_price=price, amount=amt,
        ))

    # 專案 B 的預算項目
    direct_b = BudgetItem(
        project_id=proj_b.id, c_name="直接工程費",
        kind=BudgetItemKind.B, print_no="0001",
    )
    session.add(direct_b)
    session.flush()

    # B1: 鋼筋工程（數量/單價不同）
    session.add(BudgetItem(
        project_id=proj_b.id, parent_id=direct_b.id,
        c_name="鋼筋工程", c_unit="噸", kind=BudgetItemKind.W,
        print_no="0001.01", quantity=800, unit_price=29000, amount=23200000,
    ))
    # B2: 混凝土工程（完全相同）
    session.add(BudgetItem(
        project_id=proj_b.id, parent_id=direct_b.id,
        c_name="混凝土工程", c_unit="m³", kind=BudgetItemKind.W,
        print_no="0001.02", quantity=5200, unit_price=2100, amount=10920000,
    ))
    # B3: 裝修工程（A 無）
    session.add(BudgetItem(
        project_id=proj_b.id, parent_id=direct_b.id,
        c_name="裝修工程", c_unit="式", kind=BudgetItemKind.W,
        print_no="0001.04", quantity=1, unit_price=5000000, amount=5000000,
    ))

    session.commit()
    return session, proj_a, proj_b


@pytest.fixture
def mrs_seed_db(app_with_test_db):
    """建立 MrsBase 測試資料（含一個 demo 專案避免 _ensure_db 寫入大量 seed 資料）"""
    session = app_with_test_db()

    # 建立一個 demo 專案（避免 _ensure_db 觸發 seed_demo_data 寫入大量資料）
    session.add(User(username="demo", password_hash="x", display_name="Demo"))
    session.commit()
    session.add(Project(code="MRS_TEST", name="MrsBase Test Project", owner_id=1))
    session.commit()

    cat = MrsBaseCategory(code="TEST", c_name="測試分類", sort_order=1)
    session.add(cat)
    session.flush()

    items_data = [
        ("T-001", "測試項 A", "m³", 1000, "料"),
        ("T-002", "測試項 B", "式", 2000, "工"),
        ("T-003", "測試項 C", "噸", 3000, "料"),
    ]
    for code, name, unit, price, kind in items_data:
        session.add(MrsBaseItem(
            category_id=cat.id, code=code, c_name=name,
            c_unit=unit, unit_price=price, cost_kind=kind, created_by=1,
        ))
    session.commit()
    return session, cat


# ═══════════════════════════════════════════════
# 測試案例
# ═══════════════════════════════════════════════

class TestCompareBudgetItems:
    """工項比較 API 測試"""

    def test_compare_same_project(self, compare_seed_db, client):
        """同一專案比對應全為 unchanged"""
        session, proj_a, _ = compare_seed_db
        result = _compare_budget_items_core(session, proj_a.id, proj_a.id)
        assert result["summary"]["unchanged_count"] > 0
        assert result["summary"]["modified_count"] == 0
        assert result["summary"]["added_count"] == 0
        assert result["summary"]["removed_count"] == 0

    def test_compare_diff_projects(self, compare_seed_db, client):
        """不同專案比對正確產出 diff"""
        session, proj_a, proj_b = compare_seed_db
        result = _compare_budget_items_core(session, proj_a.id, proj_b.id)
        assert len(result["items"]) > 0

        # 鋼筋工程 modified
        rebar = [i for i in result["items"] if "鋼筋" in i["c_name"]]
        assert len(rebar) > 0
        assert rebar[0]["status"] == "modified"
        assert rebar[0]["diff"]["quantity"] == -50
        assert rebar[0]["diff"]["unit_price"] == 500

        # 混凝土工程 unchanged
        conc = [i for i in result["items"] if "混凝土" in i["c_name"]]
        assert len(conc) > 0
        assert conc[0]["status"] == "unchanged"

        # 裝修工程 added（B 有 A 無）
        finish = [i for i in result["items"] if "裝修" in i["c_name"]]
        assert len(finish) > 0
        assert finish[0]["status"] == "added"

    def test_compare_summary(self, compare_seed_db, client):
        """驗證摘要統計正確"""
        session, proj_a, proj_b = compare_seed_db
        result = _compare_budget_items_core(session, proj_a.id, proj_b.id)
        summary = result["summary"]

        expected_a = 24225000 + 10920000 + 5780000  # 40925000
        expected_b = 23200000 + 10920000 + 5000000  # 39120000
        assert abs(summary["total_a"] - expected_a) < 0.01
        assert abs(summary["total_b"] - expected_b) < 0.01
        assert abs(summary["diff"] - (expected_b - expected_a)) < 0.01

    def test_compare_api_post(self, compare_seed_db, client):
        """測試 POST API 端點"""
        _, proj_a, proj_b = compare_seed_db
        resp = client.post("/api/compare/budget-items", json={
            "project_a_id": proj_a.id,
            "project_b_id": proj_b.id,
        })
        assert resp.status_code == 200
        data = resp.get_json()
        assert "project_a" in data
        assert "project_b" in data
        assert "items" in data
        assert "summary" in data
        assert len(data["items"]) > 0

    def test_compare_api_get(self, compare_seed_db, client):
        """測試 GET API 端點"""
        _, proj_a, proj_b = compare_seed_db
        resp = client.get(
            f"/api/compare/budget-items?project_a_id={proj_a.id}&project_b_id={proj_b.id}"
        )
        assert resp.status_code == 200
        data = resp.get_json()
        assert len(data["items"]) > 0

    def test_compare_missing_params(self, compare_seed_db, client):
        """缺少參數時應回傳 400"""
        resp = client.post("/api/compare/budget-items", json={})
        assert resp.status_code == 400

    def test_compare_export_excel(self, compare_seed_db, client):
        """比較報表 Excel 匯出正常"""
        _, proj_a, proj_b = compare_seed_db
        from api.index import REPORT_DIR
        os.makedirs(REPORT_DIR, exist_ok=True)

        resp = client.post("/api/compare/budget-items/export/excel", json={
            "project_a_id": proj_a.id,
            "project_b_id": proj_b.id,
        })
        assert resp.status_code == 200
        ct = resp.content_type or ""
        assert "spreadsheetml" in ct or "octet-stream" in ct


class TestCompareMrsBasePrices:
    """MrsBase 單價比較 API 測試"""

    def test_list_all_prices(self, mrs_seed_db, client):
        """列出所有 MrsBase 項目單價"""
        _, cat = mrs_seed_db

        resp = client.post("/api/compare/mrs-base-prices", json={
            "category_id": cat.id,
        })
        assert resp.status_code == 200
        data = resp.get_json()
        assert len(data["items"]) == 3
        assert data["summary"]["total"] == 3

    def test_list_by_category_filter(self, mrs_seed_db, client):
        """依分類篩選 MrsBase 項目"""
        session, cat_a = mrs_seed_db
        # 建立第二個分類
        cat_b = MrsBaseCategory(code="CAT_B", c_name="分類 B", sort_order=2)
        session.add(cat_b)
        session.flush()
        session.add(MrsBaseItem(
            category_id=cat_b.id, code="CAT_B-001",
            c_name="分類 B 項目", c_unit="式",
            unit_price=1000, cost_kind="料", created_by=1,
        ))
        session.commit()

        resp = client.post("/api/compare/mrs-base-prices", json={
            "category_id": cat_a.id,
        })
        assert resp.status_code == 200
        data = resp.get_json()
        assert len(data["items"]) == 3  # 只有 cat_a 的 3 項
        for item in data["items"]:
            assert item["category_id"] == cat_a.id

    def test_empty_result(self, app_with_test_db, client):
        """無符合條件的項目應回傳空陣列"""
        resp = client.post("/api/compare/mrs-base-prices", json={
            "category_id": 99999,
        })
        assert resp.status_code == 200
        data = resp.get_json()
        assert data["items"] == []
        assert data["summary"]["total"] == 0

    def test_compare_zero_division(self, compare_seed_db, client):
        """雙方數量皆為 0 時 diff_pct 應為 None（N/A）"""
        session, proj_a, proj_b = compare_seed_db

        # 在兩個專案中加入相同 key 但 quantity/unit_price 皆為 0 的工項
        direct_a = session.query(BudgetItem).filter(
            BudgetItem.project_id == proj_a.id,
            BudgetItem.kind == BudgetItemKind.B
        ).first()
        direct_b = session.query(BudgetItem).filter(
            BudgetItem.project_id == proj_b.id,
            BudgetItem.kind == BudgetItemKind.B
        ).first()

        zero_item_a = BudgetItem(
            project_id=proj_a.id, parent_id=direct_a.id,
            c_name="零值工項", c_unit="式", kind=BudgetItemKind.W,
            print_no="0099.01", quantity=0, unit_price=0, amount=0,
        )
        session.add(zero_item_a)
        zero_item_b = BudgetItem(
            project_id=proj_b.id, parent_id=direct_b.id,
            c_name="零值工項", c_unit="式", kind=BudgetItemKind.W,
            print_no="0099.01", quantity=0, unit_price=0, amount=0,
        )
        session.add(zero_item_b)
        session.commit()

        result = _compare_budget_items_core(session, proj_a.id, proj_b.id)
        zero_item = [i for i in result["items"] if "零值" in i["c_name"]]
        assert len(zero_item) == 1
        # diff 皆為 0，status 應為 unchanged
        assert zero_item[0]["status"] == "unchanged"
        # diff_pct 皆為 None（因分母為 0）
        assert zero_item[0]["diff_pct"]["quantity"] is None
        assert zero_item[0]["diff_pct"]["unit_price"] is None
        assert zero_item[0]["diff_pct"]["amount"] is None
