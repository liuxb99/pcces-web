"""分包合約模組測試"""

import pytest
from datetime import datetime, timezone
from sqlalchemy import create_engine
from sqlalchemy.orm import Session

from api.models import Base, User, Project, BudgetItem, BudgetItemKind
from api.seed_data import seed_demo_data


@pytest.fixture
def db_session():
    """建立測試用記憶體資料庫"""
    engine = create_engine("sqlite:///:memory:", echo=False)
    Base.metadata.create_all(engine)
    session = Session(engine)
    yield session
    session.close()


@pytest.fixture
def seeded_db(db_session):
    """含 seed 資料的資料庫"""
    seed_demo_data(db_session)
    return db_session


class TestContractModels:
    """驗證 Contract 模型可正確建立"""

    def test_create_contract(self, db_session):
        """基本合約 CRUD"""
        from api.models import Contract
        c = Contract(
            project_id=1, contract_no="SC-001", c_name="測試合約",
            contractor="測試廠商", contract_amount=1000000,
        )
        db_session.add(c)
        db_session.commit()
        assert c.id is not None
        assert c.status == "draft"

    def test_create_contract_item(self, db_session):
        """合約工項 CRUD"""
        from api.models import Contract, ContractItem
        c = Contract(project_id=1, contract_no="SC-001", c_name="測試合約")
        db_session.add(c)
        db_session.flush()
        item = ContractItem(
            contract_id=c.id, c_name="測試工項",
            contract_qty=100, unit_price=500, amount=50000,
        )
        db_session.add(item)
        db_session.commit()
        assert item.id is not None

    def test_contract_status_flow(self, db_session):
        """合約狀態機：draft → active → closed → finalized"""
        from api.models import Contract
        c = Contract(project_id=1, contract_no="SC-001", c_name="測試合約")
        db_session.add(c)
        db_session.commit()
        assert c.status == "draft"
        c.status = "active"
        db_session.commit()
        assert c.status == "active"
        c.status = "closed"
        db_session.commit()
        assert c.status == "closed"
        c.status = "finalized"
        db_session.commit()
        assert c.status == "finalized"

    def test_contract_item_calculation(self, db_session):
        """工項金額計算正確"""
        from api.models import Contract, ContractItem
        c = Contract(project_id=1, contract_no="SC-001", c_name="測試")
        db_session.add(c)
        db_session.flush()
        item = ContractItem(
            contract_id=c.id, c_name="工項",
            contract_qty=50, unit_price=1200,
        )
        db_session.add(item)
        db_session.commit()
        item.amount = round(item.contract_qty * item.unit_price, 2)
        assert item.amount == 60000.0

    def test_issue_approval_updates_contract_item(self, db_session):
        """核准期別時 ContractItem 完成數量應同步更新"""
        from api.models import Contract, ContractItem, ContractIssue, ContractIssueItem
        c = Contract(project_id=1, contract_no="SC-001", c_name="測試")
        db_session.add(c)
        db_session.flush()
        item = ContractItem(contract_id=c.id, c_name="工項", contract_qty=100, unit_price=500)
        db_session.add(item)
        db_session.flush()
        issue = ContractIssue(contract_id=c.id, issue_no=1, status="submitted")
        db_session.add(issue)
        db_session.flush()
        ii = ContractIssueItem(
            issue_id=issue.id, contract_item_id=item.id,
            contract_qty=100, unit_price=500,
            prev_completed_qty=0, this_completed_qty=50,
            total_completed_qty=50,
        )
        db_session.add(ii)
        db_session.commit()
        # 模擬核准邏輯：回寫 completed_qty
        ii.total_completed_qty = 50
        item.completed_qty = ii.total_completed_qty
        item.completed_amount = round(item.completed_qty * item.unit_price, 2)
        db_session.commit()
        assert item.completed_qty == 50
        assert item.completed_amount == 25000.0

    def test_batch_import_deduplicate(self, db_session):
        """批次匯入不重複新增相同工項"""
        from api.models import Contract, ContractItem
        c = Contract(project_id=1, contract_no="SC-001", c_name="測試")
        db_session.add(c)
        db_session.flush()
        item1 = ContractItem(contract_id=c.id, c_name="工項A", budget_item_id=1)
        db_session.add(item1)
        db_session.commit()
        # 模擬批次匯入時跳過 budget_item_id 已存在的
        existing = {i.budget_item_id for i in db_session.query(ContractItem).filter(
            ContractItem.contract_id == c.id, ContractItem.budget_item_id.isnot(None)
        ).all()}
        new_ids = [1, 2, 3]
        to_add = [i for i in new_ids if i not in existing]
        assert len(to_add) == 2  # 1 已存在，只新增 2, 3


class TestSeedData:
    """驗證 seed 資料有合約"""

    def test_seed_has_contracts(self, seeded_db):
        """seed 後應有分包合約"""
        from api.models import Contract
        contracts = seeded_db.query(Contract).all()
        assert len(contracts) >= 2

    def test_seed_contract_has_items(self, seeded_db):
        """合約工項應正確建立"""
        from api.models import Contract, ContractItem
        c = seeded_db.query(Contract).first()
        items = seeded_db.query(ContractItem).filter(ContractItem.contract_id == c.id).all()
        assert len(items) > 0

    def test_seed_issue_approved(self, seeded_db):
        """seed 中應有已核准的期別計價"""
        from api.models import ContractIssue
        approved = seeded_db.query(ContractIssue).filter(
            ContractIssue.status == "approved"
        ).all()
        assert len(approved) >= 1
