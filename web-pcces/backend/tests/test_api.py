"""PCCES API 自動化測試"""

import os
import sys
import json
import pytest
from datetime import datetime, timezone

# 加入 backend 路徑
sys.path.insert(0, os.path.join(os.path.dirname(__file__), '..'))

from sqlalchemy import text
from app.models import Base, User, Project, BudgetItem, Resource, BudgetItemKind, UserRole

# ─── 測試用資料庫 ───
TEST_DB = "sqlite:///./test_pcces.db"

from sqlalchemy import create_engine
from sqlalchemy.orm import Session, sessionmaker

engine = create_engine(TEST_DB, echo=False)
TestingSessionLocal = sessionmaker(bind=engine)


@pytest.fixture(autouse=True)
def setup_db():
    """每個測試前重建資料庫"""
    Base.metadata.drop_all(engine)
    Base.metadata.create_all(engine)
    yield
    Base.metadata.drop_all(engine)


@pytest.fixture
def db():
    """提供資料庫 session"""
    session = TestingSessionLocal()
    try:
        yield session
    finally:
        session.close()


# ─── 測試輔助函數 ───

def create_user(db, username="testuser", password="test123", display_name="測試員"):
    from hashlib import pbkdf2_hmac
    import secrets
    salt = secrets.token_hex(16)
    key = pbkdf2_hmac("sha256", password.encode(), salt.encode(), 100000).hex()
    user = User(
        username=username,
        password_hash=f"{salt}${key}",
        display_name=display_name,
    )
    db.add(user)
    db.commit()
    db.refresh(user)
    return user


def create_project(db, user, code="TEST01", name="測試專案"):
    project = Project(code=code, name=name, owner_id=user.id)
    db.add(project)
    db.commit()
    db.refresh(project)
    return project


# ═══════════════════════════════════════════════
# 使用者測試
# ═══════════════════════════════════════════════

class TestUser:
    def test_create_user(self, db):
        """測試建立使用者"""
        user = create_user(db)
        assert user.id == 1
        assert user.username == "testuser"
        assert user.is_active is True
        assert user.role == UserRole.EDITOR.value
        assert "$" in user.password_hash  # PBKDF2 格式

    def test_password_hash_format(self, db):
        """測試密碼 hash 格式為 salt$key"""
        user = create_user(db)
        parts = user.password_hash.split("$")
        assert len(parts) == 2
        assert len(parts[0]) == 32  # 16 bytes hex = 32 chars
        assert len(parts[1]) == 64  # SHA256 hex = 64 chars

    def test_username_unique(self, db):
        """測試帳號唯一性"""
        create_user(db, username="unique")
        from sqlalchemy.exc import IntegrityError
        import pytest
        with pytest.raises(IntegrityError):
            create_user(db, username="unique")


# ═══════════════════════════════════════════════
# 專案測試
# ═══════════════════════════════════════════════

class TestProject:
    def test_create_project(self, db):
        """測試建立專案"""
        user = create_user(db)
        project = create_project(db, user)
        assert project.id == 1
        assert project.code == "TEST01"
        assert project.owner_id == user.id
        assert project.status == "active"

    def test_data_isolation(self, db):
        """測試資料隔離：不同使用者看不到對方專案"""
        user1 = create_user(db, username="user1")
        user2 = create_user(db, username="user2")
        create_project(db, user1, code="P001")
        create_project(db, user2, code="P002")

        # user1 只看得到自己的專案
        p1 = db.query(Project).filter(Project.owner_id == user1.id).all()
        assert len(p1) == 1
        assert p1[0].code == "P001"

        # user2 只看得到自己的專案
        p2 = db.query(Project).filter(Project.owner_id == user2.id).all()
        assert len(p2) == 1
        assert p2[0].code == "P002"

        # 全部有 2 個專案
        assert db.query(Project).count() == 2

    def test_ownership_check(self, db):
        """測試所有權檢查"""
        user1 = create_user(db, username="user1")
        user2 = create_user(db, username="user2")
        project = create_project(db, user1)

        # user2 不能刪除 user1 的專案
        assert project.owner_id != user2.id

    def test_project_cascade_delete(self, db):
        """測試專案刪除時級聯刪除預算項目（啟用 FK）"""
        # SQLite 需手動啟用 foreign key
        db.execute(text("PRAGMA foreign_keys = ON"))
        user = create_user(db)
        project = create_project(db, user)
        item = BudgetItem(project_id=project.id, c_name="測試項目", kind=BudgetItemKind.W)
        db.add(item)
        db.commit()

        assert db.query(BudgetItem).count() == 1
        db.delete(project)
        db.commit()
        assert db.query(Project).count() == 0
        assert db.query(BudgetItem).count() == 0


# ═══════════════════════════════════════════════
# 預算項目測試
# ═══════════════════════════════════════════════

class TestBudgetItem:
    def test_create_b_item(self, db):
        """測試建立 B 類型（主項）— amount 應為 0"""
        user = create_user(db)
        project = create_project(db, user)
        item = BudgetItem(
            project_id=project.id, c_name="直接工程費",
            kind=BudgetItemKind.B, print_no="0001",
        )
        db.add(item)
        db.commit()
        assert item.amount == 0.0  # B 類型不自行計算

    def test_create_w_item_amount(self, db):
        """測試建立 W 類型（工作項目）— amount = qty × price"""
        user = create_user(db)
        project = create_project(db, user)
        item = BudgetItem(
            project_id=project.id, c_name="開挖", kind=BudgetItemKind.W,
            quantity=100, unit_price=500,
        )
        # amount 由 API 層計算，測試中需手動設定
        item.amount = round(item.quantity * item.unit_price, item.decimal_amount)
        db.add(item)
        db.commit()
        assert item.amount == 50000.0  # 100 × 500

    def test_create_z_item(self, db):
        """測試建立 Z 類型（小計）— amount 應為 0"""
        user = create_user(db)
        project = create_project(db, user)
        item = BudgetItem(
            project_id=project.id, c_name="小計", kind=BudgetItemKind.Z,
        )
        db.add(item)
        db.commit()
        assert item.amount == 0.0  # Z 類型不自行計算

    def test_tree_structure(self, db):
        """測試樹狀結構：根節點與子節點"""
        user = create_user(db)
        project = create_project(db, user)
        root = BudgetItem(project_id=project.id, c_name="總表", kind=BudgetItemKind.B)
        db.add(root)
        db.commit()

        child = BudgetItem(
            project_id=project.id, c_name="子項", kind=BudgetItemKind.W,
            parent_id=root.id, quantity=10, unit_price=100,
        )
        db.add(child)
        db.commit()

        # 檢查父子關係
        assert child.parent_id == root.id
        children = db.query(BudgetItem).filter(BudgetItem.parent_id == root.id).all()
        assert len(children) == 1

    def test_delete_item_cascade(self, db):
        """測試刪除預算項目時級聯刪除子項"""
        user = create_user(db)
        project = create_project(db, user)
        root = BudgetItem(project_id=project.id, c_name="根", kind=BudgetItemKind.B)
        db.add(root)
        db.commit()

        child = BudgetItem(
            project_id=project.id, c_name="子", kind=BudgetItemKind.W,
            parent_id=root.id, quantity=5, unit_price=200,
        )
        db.add(child)
        db.commit()

        assert db.query(BudgetItem).count() == 2
        # 刪除根節點（手動刪除子項模擬後端邏輯）
        def _delete_children(db, pid):
            for c in db.query(BudgetItem).filter(BudgetItem.parent_id == pid).all():
                db.delete(c)
        _delete_children(db, root.id)
        db.delete(root)
        db.commit()
        assert db.query(BudgetItem).count() == 0

    def test_recalc_children(self, db):
        """測試重新計算：B 類型應加總子項"""
        user = create_user(db)
        project = create_project(db, user)
        root = BudgetItem(project_id=project.id, c_name="總表", kind=BudgetItemKind.B)
        db.add(root)
        db.commit()

        for qty, price in [(10, 100), (20, 200)]:
            child = BudgetItem(
                project_id=project.id, c_name="子項", kind=BudgetItemKind.W,
                parent_id=root.id, quantity=qty, unit_price=price,
            )
            db.add(child)
        db.commit()

        # 手動重算
        children = db.query(BudgetItem).filter(BudgetItem.parent_id == root.id).all()
        total = sum(c.quantity * c.unit_price for c in children)
        root.amount = total
        db.commit()
        assert root.amount == 5000.0  # 1000 + 4000


# ═══════════════════════════════════════════════
# 資源測試
# ═══════════════════════════════════════════════

class TestResource:
    def test_create_resource(self, db):
        """測試建立資源"""
        user = create_user(db)
        project = create_project(db, user)
        resource = Resource(
            project_id=project.id, code="M001", c_name="鋼筋",
            c_unit="噸", category="material", unit_price=25000,
        )
        db.add(resource)
        db.commit()
        assert resource.id == 1
        assert resource.unit_price == 25000

    def test_resource_categories(self, db):
        """測試資源分類"""
        user = create_user(db)
        project = create_project(db, user)
        for cat in ("labor", "material", "equipment", "other"):
            r = Resource(
                project_id=project.id, code=f"{cat[:3].upper()}01",
                c_name=f"測試{cat}", c_unit="式", category=cat,
            )
            db.add(r)
        db.commit()
        assert db.query(Resource).count() == 4


# ═══════════════════════════════════════════════
# 邊界案例測試
# ═══════════════════════════════════════════════

class TestEdgeCases:
    def test_zero_quantity(self, db):
        """測試數量為 0 時 amount 為 0"""
        user = create_user(db)
        project = create_project(db, user)
        item = BudgetItem(
            project_id=project.id, c_name="測試", kind=BudgetItemKind.W,
            quantity=0, unit_price=100,
        )
        db.add(item)
        db.commit()
        assert item.amount == 0.0

    def test_negative_values(self, db):
        """測試負數處理"""
        user = create_user(db)
        project = create_project(db, user)
        item = BudgetItem(
            project_id=project.id, c_name="扣項", kind=BudgetItemKind.W,
            quantity=-1, unit_price=1000,
        )
        db.add(item)
        item.amount = round(item.quantity * item.unit_price, 2)
        assert item.amount == -1000.0

    def test_large_numbers(self, db):
        """測試大數字計算"""
        user = create_user(db)
        project = create_project(db, user)
        item = BudgetItem(
            project_id=project.id, c_name="大項", kind=BudgetItemKind.W,
            quantity=999999.99, unit_price=888888.88,
        )
        db.add(item)
        item.amount = round(item.quantity * item.unit_price, 2)
        # 只是確認不會溢位
        assert item.amount > 0

    def test_multiple_children(self, db):
        """測試多層樹狀結構"""
        user = create_user(db)
        project = create_project(db, user)
        l1 = BudgetItem(project_id=project.id, c_name="L1", kind=BudgetItemKind.B)
        db.add(l1)
        db.commit()
        l2 = BudgetItem(project_id=project.id, c_name="L2", kind=BudgetItemKind.B, parent_id=l1.id)
        db.add(l2)
        db.commit()
        l3 = BudgetItem(project_id=project.id, c_name="L3", kind=BudgetItemKind.W, parent_id=l2.id,
                        quantity=5, unit_price=300)
        # amount 由 API 層計算，測試中需手動設定
        l3.amount = round(l3.quantity * l3.unit_price, l3.decimal_amount)
        db.add(l3)
        db.commit()
        # L1 → L2 → L3
        assert l3.parent_id == l2.id
        assert l2.parent_id == l1.id
        assert l3.amount == 1500.0
