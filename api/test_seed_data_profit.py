"""PCCES 示範資料 — 利潤計算與資料結構自動化測試"""

import os
import sys
import pytest

from sqlalchemy import create_engine
from sqlalchemy.orm import Session, sessionmaker

# 加入專案根目錄（讓 from api.models 等 import 生效）
sys.path.insert(0, os.path.join(os.path.dirname(__file__), '..'))

from api.models import Base, Project, BudgetItem, Resource, BudgetItemKind
from api.seed_data import seed_demo_data, _recalc_budget_tree, _apply_profit_rules


TEST_DB_URL = "sqlite://"  # in-memory


@pytest.fixture(autouse=True)
def setup_db():
    """每個測試前重建 in-memory 資料庫"""
    engine = create_engine(TEST_DB_URL, echo=False)
    Base.metadata.create_all(engine)
    yield engine
    Base.metadata.drop_all(engine)


@pytest.fixture
def db(setup_db):
    """提供資料庫 session"""
    session = Session(bind=setup_db)
    try:
        yield session
    finally:
        session.close()


# ═══════════════════════════════════════════════
# 測試 1：seed_demo_data 建立示範使用者
# ═══════════════════════════════════════════════

class TestSeedCreatesDemoUser:
    """驗證 seed_demo_data 執行後，資料庫中存在 demo 使用者"""
    def test_seed_creates_demo_user(self, db):
        """seed_demo_data 應建立示範使用者"""
        seed_demo_data(db)
        from api.models import User
        user = db.query(User).filter(User.username == "demo").first()
        assert user is not None
        assert user.display_name == "示範使用者"
        assert user.company == "測試機關"


# ═══════════════════════════════════════════════
# 測試 2：seed_demo_data 建立示範專案
# ═══════════════════════════════════════════════

class TestSeedCreatesProject:
    """驗證 seed_demo_data 執行後，資料庫中存在 DEMO001 專案"""
    def test_seed_creates_project(self, db):
        """seed_demo_data 應建立示範專案"""
        seed_demo_data(db)
        project = db.query(Project).filter(Project.code == "DEMO001").first()
        assert project is not None
        assert "大樓" in project.name
        assert project.owner_id is not None


# ═══════════════════════════════════════════════
# 測試 3：包商利潤金額正確（直接+間接）× 5%
# ═══════════════════════════════════════════════

class TestProfitItemAmountCorrect:
    """驗證包商利潤金額 = (直接工程費 + 間接工程費) × 5%"""
    def test_profit_item_amount_correct(self, db):
        """包商利潤金額應為直接費與間接費總和之 5%"""
        seed_demo_data(db)

        # 取得直接工程費與間接工程費
        direct = db.query(BudgetItem).filter(
            BudgetItem.c_name == "直接工程費",
            BudgetItem.kind == BudgetItemKind.B,
        ).first()
        indirect = db.query(BudgetItem).filter(
            BudgetItem.c_name == "間接工程費",
            BudgetItem.kind == BudgetItemKind.B,
        ).first()
        assert direct is not None, "直接工程費不存在"
        assert indirect is not None, "間接工程費不存在"

        base = (direct.amount or 0) + (indirect.amount or 0)

        # 取得包商利潤
        profit_item = db.query(BudgetItem).filter(
            BudgetItem.c_name.like("%包商利潤%"),
        ).first()
        assert profit_item is not None, "包商利潤項目不存在"

        expected = round(base * 0.05, 2)
        assert profit_item.amount == expected, (
            f"包商利潤預期 {expected}，實際 {profit_item.amount}（直接費 {direct.amount} + 間接費 {indirect.amount} = {base}）"
        )


# ═══════════════════════════════════════════════
# 測試 4：營業稅金額正確（工程費+利潤）× 5%
# ═══════════════════════════════════════════════

class TestTaxItemAmountCorrect:
    """驗證營業稅金額 = (直接工程費 + 間接工程費 + 包商利潤) × 5%"""
    def test_tax_item_amount_correct(self, db):
        """營業稅金額應為工程費加包商利潤之 5%"""
        seed_demo_data(db)

        direct = db.query(BudgetItem).filter(
            BudgetItem.c_name == "直接工程費",
            BudgetItem.kind == BudgetItemKind.B,
        ).first()
        indirect = db.query(BudgetItem).filter(
            BudgetItem.c_name == "間接工程費",
            BudgetItem.kind == BudgetItemKind.B,
        ).first()
        profit_item = db.query(BudgetItem).filter(
            BudgetItem.c_name.like("%包商利潤%"),
        ).first()
        # 用更精確的 c_name 過濾，避免匹配到「利潤及營業稅」(父項)
        tax_item = db.query(BudgetItem).filter(
            BudgetItem.c_name == "營業稅（5%）",
        ).first()

        assert tax_item is not None, "營業稅（5%）項目不存在"
        assert profit_item is not None, "包商利潤項目不存在"

        base = (direct.amount or 0) + (indirect.amount or 0) + (profit_item.amount or 0)
        expected = round(base * 0.05, 2)
        assert tax_item.amount == expected, (
            f"營業稅預期 {expected}，實際 {tax_item.amount}"
        )


# ═══════════════════════════════════════════════
# 測試 5：三層加總正確（B/Z 父項 = 子項加總）
# ═══════════════════════════════════════════════

class TestTotalAmountMatches:
    """驗證樹狀結構中 B/Z 父項金額等於子項金額加總"""
    def test_total_amount_matches(self, db):
        """B/Z 父項金額應等於其直接子項加總"""
        seed_demo_data(db)
        project = db.query(Project).filter(Project.code == "DEMO001").first()

        # 取所有 B/Z 類型的父項
        parents = db.query(BudgetItem).filter(
            BudgetItem.project_id == project.id,
            BudgetItem.kind.in_([BudgetItemKind.B, BudgetItemKind.Z]),
        ).all()

        for parent in parents:
            children = db.query(BudgetItem).filter(
                BudgetItem.parent_id == parent.id
            ).all()
            if children:
                expected = round(sum((c.amount or 0) for c in children), 2)
                assert parent.amount == expected, (
                    f"「{parent.c_name}」(ID={parent.id}) 金額 {parent.amount} "
                    f"不等於子項加總 {expected}"
                )


# ═══════════════════════════════════════════════
# 測試 6：樹狀深度（至少 3 層）
# ═══════════════════════════════════════════════

class TestBudgetTreeDepth:
    """驗證示範資料的樹狀結構至少有 3 層深度"""
    def test_budget_tree_depth(self, db):
        """seed 後應有 ≥ 3 層樹狀結構"""
        seed_demo_data(db)
        project = db.query(Project).filter(Project.code == "DEMO001").first()

        # 第 1 層：根節點（parent_id 為 null）
        roots = db.query(BudgetItem).filter(
            BudgetItem.project_id == project.id,
            BudgetItem.parent_id.is_(None)
        ).all()
        assert len(roots) >= 3, f"根節點數 ≥ 3（實際 {len(roots)}）"

        # 第 2 層：根節點的直接子項
        l2_count = 0
        for root in roots:
            l2 = db.query(BudgetItem).filter(
                BudgetItem.project_id == project.id,
                BudgetItem.parent_id == root.id
            ).all()
            l2_count += len(l2)
        assert l2_count >= 5, f"第 2 層節點數 ≥ 5（實際 {l2_count}）"

        # 第 3 層：有孫項（parent_id 不為 null 且有子項的項目）
        l3_count = 0
        l2_items = db.query(BudgetItem).filter(
            BudgetItem.project_id == project.id,
            BudgetItem.parent_id.isnot(None)
        ).all()
        for item in l2_items:
            l3 = db.query(BudgetItem).filter(
                BudgetItem.project_id == project.id,
                BudgetItem.parent_id == item.id
            ).all()
            l3_count += len(l3)
        assert l3_count >= 10, f"第 3 層節點數 ≥ 10（實際 {l3_count}）"


# ═══════════════════════════════════════════════
# 測試 7：葉節點金額不為 0（利潤項除外）
# ═══════════════════════════════════════════════

class TestNoZeroAmountLeaf:
    """驗證葉節點（無子項的 W 類型）金額應 > 0，利潤項除外"""
    def test_no_zero_amount_leaf(self, db):
        """葉節點金額應大於 0（利潤百分比項目初始為 0，但 seed 後應被計算）"""
        seed_demo_data(db)
        project = db.query(Project).filter(Project.code == "DEMO001").first()

        # 取得所有葉節點（沒有子項的項目）
        all_items = db.query(BudgetItem).filter(
            BudgetItem.project_id == project.id
        ).all()
        all_ids = {i.id for i in all_items}

        leaf_items = []
        for item in all_items:
            # 檢查此 item 是否有子項
            children = db.query(BudgetItem).filter(
                BudgetItem.parent_id == item.id
            ).all()
            if not children:
                leaf_items.append(item)

        for leaf in leaf_items:
            assert leaf.amount is not None and leaf.amount > 0, (
                f"葉節點「{leaf.c_name}」(ID={leaf.id}, kind={leaf.kind}) "
                f"amount 應 > 0，實際 {leaf.amount}"
            )


# ═══════════════════════════════════════════════
# 測試 8：修改後 recalc 仍正確
# ═══════════════════════════════════════════════

class TestRecalcAfterModify:
    """驗證某個 W 項修改數量/單價後，呼叫 recalc 仍能正確計算"""
    def test_recalc_after_modify(self, db):
        """修改 W 項目 quantity 後，_recalc_budget_tree 應更新父項金額"""
        seed_demo_data(db)
        project = db.query(Project).filter(Project.code == "DEMO001").first()

        # 找一個 W 類型的葉節點
        w_item = db.query(BudgetItem).filter(
            BudgetItem.project_id == project.id,
            BudgetItem.kind == BudgetItemKind.W,
        ).first()
        assert w_item is not None

        old_amount = w_item.amount
        old_qty = w_item.quantity

        # 修改數量（加倍）
        w_item.quantity = old_qty * 2
        db.flush()

        # 重新計算
        _recalc_budget_tree(db, project.id)
        db.commit()
        db.refresh(w_item)

        expected_new = round(old_qty * 2 * (w_item.unit_price or 0), w_item.decimal_amount)
        assert w_item.amount == expected_new, (
            f"修改後 W 項金額應為 {expected_new}，實際 {w_item.amount}"
        )

        # 驗證父項也更新
        if w_item.parent_id:
            parent = db.query(BudgetItem).filter(BudgetItem.id == w_item.parent_id).first()
            if parent and parent.kind in (BudgetItemKind.B, BudgetItemKind.Z):
                siblings = db.query(BudgetItem).filter(
                    BudgetItem.parent_id == parent.id
                ).all()
                expected_parent = round(sum((s.amount or 0) for s in siblings), 2)
                db.refresh(parent)
                assert parent.amount == expected_parent, (
                    f"父項「{parent.c_name}」金額應為 {expected_parent}，實際 {parent.amount}"
                )


# ═══════════════════════════════════════════════
# 測試 9：預算項目數 ≥ 25
# ═══════════════════════════════════════════════

class TestAtLeast25Items:
    """驗證示範資料至少有 25 個預算項目"""
    def test_at_least_25_items(self, db):
        """seed 後預算項目 count ≥ 25"""
        seed_demo_data(db)
        project = db.query(Project).filter(Project.code == "DEMO001").first()
        count = db.query(BudgetItem).filter(
            BudgetItem.project_id == project.id
        ).count()
        assert count >= 25, f"預算項目數應 ≥ 25（實際 {count}）"


# ═══════════════════════════════════════════════
# 測試 10：資源建立（工、料、機各至少一種）
# ═══════════════════════════════════════════════

class TestResourcesCreated:
    """驗證 seed_demo_data 建立至少 3 種資源（工、料、機）"""
    def test_resources_created(self, db):
        """seed 後應有至少 3 種資源，涵蓋 labor / material / equipment"""
        seed_demo_data(db)
        project = db.query(Project).filter(Project.code == "DEMO001").first()
        resources = db.query(Resource).filter(
            Resource.project_id == project.id
        ).all()
        assert len(resources) >= 3, f"資源數應 ≥ 3（實際 {len(resources)}）"

        categories = {r.category for r in resources}
        assert "labor" in categories, "缺少人工(labor)類資源"
        assert "material" in categories, "缺少材料(material)類資源"
        assert "equipment" in categories, "缺少設備(equipment)類資源"

        # 驗證資源有正確的 code 格式
        codes = {r.code for r in resources}
        assert any(c.startswith("L") for c in codes), "缺少 L 開頭的人工資源代碼"
        assert any(c.startswith("M") for c in codes), "缺少 M 開頭的材料資源代碼"
        assert any(c.startswith("E") for c in codes), "缺少 E 開頭的設備資源代碼"
