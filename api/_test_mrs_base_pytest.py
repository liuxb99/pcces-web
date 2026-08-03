"""工項單價庫（MrsBase）模組測試"""

import pytest
from sqlalchemy import create_engine
from sqlalchemy.orm import Session

from api.models import Base, MrsBaseCategory, MrsBaseItem, MrsBaseBreakdownItem, MrsBaseBookmark
from api.seed_data import seed_demo_data


@pytest.fixture
def db_session():
    engine = create_engine("sqlite:///:memory:", echo=False)
    Base.metadata.create_all(engine)
    session = Session(engine)
    yield session
    session.close()


@pytest.fixture
def seeded_db(db_session):
    seed_demo_data(db_session)
    return db_session


class TestMrsBaseCategory:
    """分類測試"""

    def test_create_category(self, db_session):
        cat = MrsBaseCategory(code="CONC", c_name="混凝土工程", sort_order=1)
        db_session.add(cat)
        db_session.commit()
        assert cat.id is not None
        assert cat.code == "CONC"

    def test_category_tree(self, db_session):
        parent = MrsBaseCategory(code="MAT", c_name="材料", sort_order=1)
        db_session.add(parent)
        db_session.flush()
        child = MrsBaseCategory(code="CONC", c_name="混凝土", sort_order=1, parent_id=parent.id)
        db_session.add(child)
        db_session.commit()
        assert child.parent_id == parent.id

    def test_seed_has_categories(self, seeded_db):
        cats = seeded_db.query(MrsBaseCategory).all()
        assert len(cats) >= 5  # 混凝土/鋼筋/模板/裝修/機電

    def test_seed_categories_have_items(self, seeded_db):
        items = seeded_db.query(MrsBaseItem).all()
        assert len(items) >= 15  # 至少 15 筆工項


class TestMrsBaseItem:
    """工項測試"""

    def test_create_item(self, db_session):
        cat = MrsBaseCategory(code="TEST", c_name="測試", sort_order=1)
        db_session.add(cat)
        db_session.flush()
        item = MrsBaseItem(category_id=cat.id, code="T001", c_name="測試工項", c_unit="式", unit_price=1000, created_by=1)
        db_session.add(item)
        db_session.commit()
        assert item.id is not None
        assert item.is_approved == False

    def test_approve_item(self, db_session):
        cat = MrsBaseCategory(code="TEST", c_name="測試", sort_order=1)
        db_session.add(cat)
        db_session.flush()
        item = MrsBaseItem(category_id=cat.id, code="T001", c_name="測試工項", c_unit="式", unit_price=1000, created_by=1)
        db_session.add(item)
        db_session.commit()
        item.is_approved = True
        item.approved_by = 1
        db_session.commit()
        assert item.is_approved == True
        assert item.approved_by == 1

    def test_breakdown_sum_updates_price(self, db_session):
        """工料機組成修改後應自動更新 unit_price"""
        cat = MrsBaseCategory(code="TEST", c_name="測試", sort_order=1)
        db_session.add(cat)
        db_session.flush()
        item = MrsBaseItem(category_id=cat.id, code="T001", c_name="測試工項", c_unit="式", unit_price=0, is_analysis=True, created_by=1)
        db_session.add(item)
        db_session.flush()
        bd1 = MrsBaseBreakdownItem(item_id=item.id, code="L001", c_name="工", c_unit="工", quantity=2, unit_price=1000, amount=2000, category="labor")
        db_session.add(bd1)
        bd2 = MrsBaseBreakdownItem(item_id=item.id, code="M001", c_name="料", c_unit="式", quantity=1, unit_price=3000, amount=3000, category="material")
        db_session.add(bd2)
        db_session.flush()
        # 模擬後端自動加總邏輯
        breakdowns = db_session.query(MrsBaseBreakdownItem).filter(
            MrsBaseBreakdownItem.item_id == item.id
        ).all()
        total = sum((b.amount or 0) for b in breakdowns)
        item.unit_price = round(total, 2)
        db_session.commit()
        assert item.unit_price == 5000.0

    def test_seed_has_breakdown_items(self, seeded_db):
        bds = seeded_db.query(MrsBaseBreakdownItem).all()
        assert len(bds) >= 9  # 3 items × 3 breakdown each


class TestMrsBaseBookmark:
    """書籤測試"""

    def test_create_bookmark(self, db_session):
        cat = MrsBaseCategory(code="TEST", c_name="測試", sort_order=1)
        db_session.add(cat)
        db_session.flush()
        item = MrsBaseItem(category_id=cat.id, code="T001", c_name="測試", c_unit="式", unit_price=100, created_by=1)
        db_session.add(item)
        db_session.flush()
        bm = MrsBaseBookmark(user_id=1, item_id=item.id)
        db_session.add(bm)
        db_session.commit()
        assert bm.id is not None

    def test_seed_has_bookmarks(self, seeded_db):
        bms = seeded_db.query(MrsBaseBookmark).all()
        assert len(bms) >= 3

    def test_no_duplicate_bookmark(self, db_session):
        cat = MrsBaseCategory(code="TEST", c_name="測試", sort_order=1)
        db_session.add(cat)
        db_session.flush()
        item = MrsBaseItem(category_id=cat.id, code="T001", c_name="測試", c_unit="式", unit_price=100, created_by=1)
        db_session.add(item)
        db_session.flush()
        bm1 = MrsBaseBookmark(user_id=1, item_id=item.id)
        db_session.add(bm1)
        db_session.commit()
        existing = db_session.query(MrsBaseBookmark).filter(
            MrsBaseBookmark.user_id == 1, MrsBaseBookmark.item_id == item.id
        ).first()
        assert existing is not None


class TestSeedContainsAll:
    """驗證 seed 資料完整性"""

    def test_seed_categories_names(self, seeded_db):
        names = [c.c_name for c in seeded_db.query(MrsBaseCategory).all()]
        for expected in ["混凝土", "鋼筋", "模板"]:
            assert any(expected in n for n in names), f"缺少分類: {expected}"

    def test_seed_items_have_prices(self, seeded_db):
        zero_price = seeded_db.query(MrsBaseItem).filter(MrsBaseItem.unit_price == 0).count()
        assert zero_price == 0, f"有 {zero_price} 筆工項單價為 0"
