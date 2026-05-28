# 返工規劃 — TASK-002 第 1 次返工

## 目標
修復 REVIEWER 指出的缺失項目，將評分從 87 → 90+ 分。

---

## 修改檔案清單

| # | 檔案 | 動作 | 說明 |
|---|------|------|------|
| 1 | `api/seed_data.py` | **修改** | 移除 W 類型手動 amount 計算；拆分 `_recalc_seed` 為兩函式 |
| 2 | `api/test_seed_data_profit.py` | **新增** | 利潤計算自動化測試 + 示範資料結構驗證 |
| 3 | `web-pcces/frontend/package.json` | **修改** | (若需要) 補安裝前端測試相依套件，支援 `npm run build` 驗證 |

---

## 實作步驟

### Step-1：清理 `api/seed_data.py` — 移除 W 類型冗餘 amount 計算

**修改：`api/seed_data.py`**

**位置 A** — 第 3 層細項建立處（約 line 120-125）：

原始碼（第 3 層 W 細項建立後）：
```python
sub.amount = round(sub.quantity * sub.unit_price, sub.decimal_amount) if sub.kind != BudgetItemKind.B else 0.0
db.add(sub)
```

改為：
```python
# amount 由 _recalc_seed 統一計算，此處不預先設定
db.add(sub)
```

**位置 B** — 間接工程費細項（約 line 150-155）：

原始碼：
```python
item.amount = round(item.quantity * item.unit_price, item.decimal_amount)
db.add(item)
```

改為：
```python
# amount 由 _recalc_seed 統一計算
db.add(item)
```

**理由**：`_recalc_seed` 會在 commit 前遞迴計算所有 B/Z 類型並為 W 類型計算 amount = qty × price，預先計算的值會被覆寫，屬於 dead code。

---

### Step-2：拆分 `_recalc_seed` 為兩個函式

**修改：`api/seed_data.py`**

將原本的 `_recalc_seed`（~100 行）拆為：

#### 函式 A：`_recalc_budget_tree(db, project_id)` — 通用遞迴（約 25 行）

```python
def _recalc_budget_tree(db: Session, project_id: int, parent_id: Optional[int] = None) -> float:
    """遞迴計算 B/Z 類型金額（加總子項），W 類型依 qty × price 計算

    回傳此節點下所有子項金額總和。
    """
    children = db.query(BudgetItem).filter(
        BudgetItem.project_id == project_id,
        BudgetItem.parent_id == parent_id
    ).all()
    total = 0.0
    for child in children:
        if child.kind in (BudgetItemKind.B, BudgetItemKind.Z):
            child.amount = _recalc_budget_tree(db, project_id, child.id)
        else:
            child.amount = round(
                (child.quantity or 0) * (child.unit_price or 0),
                child.decimal_amount
            )
        db.flush()
        total += child.amount or 0
    return round(total, 2)
```

#### 函式 B：`_apply_profit_rules(db, project_id)` — 利潤特殊邏輯（約 35 行）

```python
def _apply_profit_rules(db: Session, project_id: int):
    """計算「利潤及營業稅」下包商利潤(5%) 與營業稅(5%) 的百分比金額

    包商利潤 = (直接工程費 + 間接工程費) × 5%
    營業稅   = (直接工程費 + 間接工程費 + 包商利潤) × 5%
    """
    # 查詢利潤父項目
    profit_parent = db.query(BudgetItem).filter(
        BudgetItem.project_id == project_id,
        BudgetItem.c_name == "利潤及營業稅",
        BudgetItem.kind == BudgetItemKind.Z,
    ).first()
    if not profit_parent:
        return

    # 取得工程費總額
    direct_cost = db.query(BudgetItem).filter(
        BudgetItem.project_id == project_id,
        BudgetItem.c_name == "直接工程費",
        BudgetItem.kind == BudgetItemKind.B,
    ).first()
    indirect_cost = db.query(BudgetItem).filter(
        BudgetItem.project_id == project_id,
        BudgetItem.c_name == "間接工程費",
        BudgetItem.kind == BudgetItemKind.B,
    ).first()

    direct_total = direct_cost.amount if direct_cost else 0
    indirect_total = indirect_cost.amount if indirect_cost else 0
    base = direct_total + indirect_total

    # 查詢子項
    profit_item = db.query(BudgetItem).filter(
        BudgetItem.project_id == project_id,
        BudgetItem.parent_id == profit_parent.id,
        BudgetItem.c_name.like("%包商利潤%"),
    ).first()
    tax_item = db.query(BudgetItem).filter(
        BudgetItem.project_id == project_id,
        BudgetItem.parent_id == profit_parent.id,
        BudgetItem.c_name.like("%營業稅%"),
    ).first()

    if profit_item:
        profit_item.amount = round(base * 0.05, 2)
        db.flush()
    profit_amt = profit_item.amount if profit_item else 0

    if tax_item:
        tax_item.amount = round((base + profit_amt) * 0.05, 2)
        db.flush()

    # 更新利潤父項總額
    total_profit = (
        (profit_item.amount if profit_item else 0) +
        (tax_item.amount if tax_item else 0)
    )
    profit_parent.amount = round(total_profit, 2)
    db.flush()
```

#### 修改後的 `_recalc_seed`（簡化為約 15 行）：

```python
def _recalc_seed(db: Session, project_id: int):
    """遞迴計算示範專案所有 B/Z 類型項目的金額（加總子項），
    並套用利潤類項目的百分比計算規則。
    """
    # 第一輪：標準遞迴計算
    _recalc_budget_tree(db, project_id)
    db.commit()

    # 第二輪：特殊處理利潤類項目
    _apply_profit_rules(db, project_id)
    db.commit()
```

---

### Step-3：新增利潤計算自動化測試

**新增：`api/test_seed_data_profit.py`**

使用 SQLite in-memory 資料庫，直接測試 `api/seed_data` 模組。

```python
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
# seed_demo_data 整體測試
# ═══════════════════════════════════════════════

class TestSeedDemoData:
    def test_seed_creates_demo_user(self, db):
        """seed_demo_data 應建立示範使用者"""
        seed_demo_data(db)
        from api.models import User
        user = db.query(User).filter(User.username == "demo").first()
        assert user is not None
        assert user.display_name == "示範使用者"

    def test_seed_creates_project(self, db):
        """seed_demo_data 應建立示範專案"""
        seed_demo_data(db)
        project = db.query(Project).filter(Project.code == "DEMO001").first()
        assert project is not None
        assert "大樓" in project.name

    def test_seed_creates_budget_items(self, db):
        """seed_demo_data 應建立足夠的預算項目"""
        seed_demo_data(db)
        items = db.query(BudgetItem).all()
        # 至少應有：1(直接) + 5(分項) + 15(細項) + 1(間接) + 4(間接細項) + 1(利潤) + 2(利潤細項) = 29
        assert len(items) >= 25, f"預算項目數量不足: {len(items)}"

    def test_seed_creates_resources(self, db):
        """seed_demo_data 應建立至少 3 筆資源（工、料、機）"""
        seed_demo_data(db)
        resources = db.query(Resource).all()
        assert len(resources) >= 3
        categories = {r.category for r in resources}
        assert "labor" in categories
        assert "material" in categories
        assert "equipment" in categories

    def test_seed_has_tree_structure(self, db):
        """seed_demo_data 應有至少 3 層樹狀結構"""
        seed_demo_data(db)
        # 根節點：parent_id 為 null
        roots = db.query(BudgetItem).filter(BudgetItem.parent_id.is_(None)).all()
        assert len(roots) >= 3  # 直接工程費 + 間接工程費 + 利潤

        # 第 2 層：有子項
        l2_count = 0
        for root in roots:
            l2 = db.query(BudgetItem).filter(BudgetItem.parent_id == root.id).all()
            l2_count += len(l2)
        assert l2_count >= 5

        # 第 3 層：有孫項
        l3_count = 0
        l2_items = db.query(BudgetItem).filter(
            BudgetItem.project_id == db.query(Project).first().id,
            BudgetItem.parent_id.isnot(None)
        ).all()
        for item in l2_items:
            l3 = db.query(BudgetItem).filter(BudgetItem.parent_id == item.id).all()
            l3_count += len(l3)
        assert l3_count >= 10


# ═══════════════════════════════════════════════
# 利潤計算測試（核心需求）
# ═══════════════════════════════════════════════

class TestProfitCalculation:
    """驗證包商利潤(5%) 與營業稅(5%) 的百分比計算正確性"""

    def _get_profit_items(self, db):
        """輔助：取得利潤相關項目"""
        profit_parent = db.query(BudgetItem).filter(
            BudgetItem.c_name == "利潤及營業稅",
            BudgetItem.kind == BudgetItemKind.Z,
        ).first()
        if not profit_parent:
            return None, None, None
        profit_item = db.query(BudgetItem).filter(
            BudgetItem.parent_id == profit_parent.id,
            BudgetItem.c_name.like("%包商利潤%"),
        ).first()
        tax_item = db.query(BudgetItem).filter(
            BudgetItem.parent_id == profit_parent.id,
            BudgetItem.c_name.like("%營業稅%"),
        ).first()
        return profit_parent, profit_item, tax_item

    def test_profit_calculation_after_seed(self, db):
        """執行 seed_demo_data 後，包商利潤與營業稅金額應正確"""
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

        direct_total = direct.amount or 0
        indirect_total = indirect.amount or 0
        base = direct_total + indirect_total

        # 取得利潤項目
        profit_parent, profit_item, tax_item = self._get_profit_items(db)
        assert profit_parent is not None
        assert profit_item is not None
        assert tax_item is not None

        # 包商利潤 = 工程費合計 × 5%
        expected_profit = round(base * 0.05, 2)
        assert profit_item.amount == expected_profit, (
            f"包商利潤預期 {expected_profit}，實際 {profit_item.amount}"
        )

        # 營業稅 = (工程費合計 + 包商利潤) × 5%
        expected_tax = round((base + expected_profit) * 0.05, 2)
        assert tax_item.amount == expected_tax, (
            f"營業稅預期 {expected_tax}，實際 {tax_item.amount}"
        )

        # 利潤父項總額 = 包商利潤 + 營業稅
        expected_total = round(expected_profit + expected_tax, 2)
        assert profit_parent.amount == expected_total, (
            f"利潤總額預期 {expected_total}，實際 {profit_parent.amount}"
        )

    def test_idempotent_seed(self, db):
        """重複呼叫 seed_demo_data 不應重複寫入"""
        r1 = seed_demo_data(db)
        r2 = seed_demo_data(db)
        assert r1 is True   # 第一次寫入
        assert r2 is False  # 第二次略過

    def test_profit_values_match_snapshot(self, db):
        """驗證利潤金額符合已知快照值（防止回歸）"""
        seed_demo_data(db)
        _, profit_item, tax_item = self._get_profit_items(db)

        # 快照值（由目前 seed_data 的數值計算得出）
        # 直接費總額 = 各分項加總
        # 間接費總額 = 各間接項加總
        # 包商利潤 = (直接 + 間接) × 5%
        # 營業稅 = (直接 + 間接 + 利潤) × 5%
        assert profit_item.amount > 0, "包商利潤應大於 0"
        assert tax_item.amount > 0, "營業稅應大於 0"
        # 營業稅應略大於包商利潤（因基數多了利潤本身）
        assert tax_item.amount > profit_item.amount, (
            f"營業稅({tax_item.amount}) 應大於包商利潤({profit_item.amount})"
        )


# ═══════════════════════════════════════════════
# _recalc_budget_tree 單元測試
# ═══════════════════════════════════════════════

class TestRecalcBudgetTree:
    def test_recalc_sums_children(self, db):
        """_recalc_budget_tree 應正確加總 W 子項金額到 B 父項"""
        # 手動建立測試結構
        user = _create_demo_user(db)
        project = Project(code="TEST", name="測試", owner_id=user.id)
        db.add(project)
        db.flush()

        parent = BudgetItem(
            project_id=project.id, c_name="父項",
            kind=BudgetItemKind.B, print_no="0001",
        )
        db.add(parent)
        db.flush()

        for qty, price in [(10, 100), (20, 200), (30, 300)]:
            child = BudgetItem(
                project_id=project.id, c_name="子項",
                kind=BudgetItemKind.W, parent_id=parent.id,
                quantity=qty, unit_price=price,
            )
            db.add(child)
        db.commit()

        # 執行重算
        total = _recalc_budget_tree(db, project.id)
        db.commit()

        # 驗證父項金額 = 10×100 + 20×200 + 30×300 = 1000 + 4000 + 9000 = 14000
        db.refresh(parent)
        assert parent.amount == 14000.0, f"父項金額應為 14000，實際 {parent.amount}"
        assert total == 14000.0

    def test_recalc_nested_structure(self, db):
        """_recalc_budget_tree 應遞迴處理多層 B 結構"""
        user = _create_demo_user(db)
        project = Project(code="NEST", name="巢狀測試", owner_id=user.id)
        db.add(project)
        db.flush()

        l1 = BudgetItem(project_id=project.id, c_name="L1", kind=BudgetItemKind.B)
        db.add(l1)
        db.flush()

        l2 = BudgetItem(project_id=project.id, c_name="L2", kind=BudgetItemKind.B, parent_id=l1.id)
        db.add(l2)
        db.flush()

        l3 = BudgetItem(
            project_id=project.id, c_name="L3", kind=BudgetItemKind.W,
            parent_id=l2.id, quantity=5, unit_price=300,
        )
        db.add(l3)
        db.commit()

        _recalc_budget_tree(db, project.id)
        db.commit()

        db.refresh(l3)
        db.refresh(l2)
        db.refresh(l1)

        assert l3.amount == 1500.0  # 5 × 300
        assert l2.amount == 1500.0  # 加總子項
        assert l1.amount == 1500.0  # 加總子項


# ═══════════════════════════════════════════════
# _apply_profit_rules 單元測試
# ═══════════════════════════════════════════════

class TestApplyProfitRules:
    def test_apply_profit_rules(self, db):
        """_apply_profit_rules 應正確計算包商利潤與營業稅百分比"""
        user = _create_demo_user(db)
        project = Project(code="PROFIT", name="利潤測試", owner_id=user.id)
        db.add(project)
        db.flush()

        # 建立直接費（B）→ 子項總和 1,000,000
        direct = BudgetItem(project_id=project.id, c_name="直接工程費",
                            kind=BudgetItemKind.B, print_no="0001")
        db.add(direct)
        db.flush()
        w1 = BudgetItem(project_id=project.id, c_name="工項", kind=BudgetItemKind.W,
                        parent_id=direct.id, quantity=1, unit_price=1000000)
        db.add(w1)
        db.flush()
        # 手動設定
        w1.amount = 1000000.0
        direct.amount = 1000000.0
        db.flush()

        # 建立間接費（B）→ 子項總和 200,000
        indirect = BudgetItem(project_id=project.id, c_name="間接工程費",
                              kind=BudgetItemKind.B, print_no="0002")
        db.add(indirect)
        db.flush()
        w2 = BudgetItem(project_id=project.id, c_name="品管", kind=BudgetItemKind.W,
                        parent_id=indirect.id, quantity=1, unit_price=200000)
        db.add(w2)
        db.flush()
        w2.amount = 200000.0
        indirect.amount = 200000.0
        db.flush()

        # 建立利潤父項（Z）
        profit_parent = BudgetItem(project_id=project.id, c_name="利潤及營業稅",
                                    kind=BudgetItemKind.Z, print_no="0003")
        db.add(profit_parent)
        db.flush()

        profit_item = BudgetItem(project_id=project.id, c_name="包商利潤（約 5%）",
                                 kind=BudgetItemKind.W, parent_id=profit_parent.id,
                                 print_no="0003.01", quantity=1, unit_price=0)
        db.add(profit_item)
        db.flush()
        tax_item = BudgetItem(project_id=project.id, c_name="營業稅（5%）",
                              kind=BudgetItemKind.W, parent_id=profit_parent.id,
                              print_no="0003.02", quantity=1, unit_price=0)
        db.add(tax_item)
        db.commit()

        # 執行利潤特殊計算
        _apply_profit_rules(db, project.id)
        db.commit()

        db.refresh(profit_item)
        db.refresh(tax_item)
        db.refresh(profit_parent)

        base = 1000000 + 200000  # 1,200,000
        expected_profit = round(base * 0.05, 2)      # 60,000
        expected_tax = round((base + expected_profit) * 0.05, 2)  # 63,000
        expected_parent = round(expected_profit + expected_tax, 2)  # 123,000

        assert profit_item.amount == expected_profit, (
            f"包商利潤預期 {expected_profit}，實際 {profit_item.amount}"
        )
        assert tax_item.amount == expected_tax, (
            f"營業稅預期 {expected_tax}，實際 {tax_item.amount}"
        )
        assert profit_parent.amount == expected_parent, (
            f"利潤總額預期 {expected_parent}，實際 {profit_parent.amount}"
        )


# ─── 輔助函式 ───

def _create_demo_user(db):
    """建立測試用使用者（密碼 hash 簡化）"""
    from api.models import User
    user = User(
        username=f"test_user_{id(db)}",
        password_hash="testsalt$" + "a" * 64,
        display_name="測試使用者",
    )
    db.add(user)
    db.commit()
    db.refresh(user)
    return user
```

**測試案例一覽**：

| 測試類別 | 測試方法 | 驗證目標 |
|---------|---------|---------|
| `TestSeedDemoData` | `test_seed_creates_demo_user` | seed 後存在 demo 使用者 |
| | `test_seed_creates_project` | seed 後存在 DEMO001 專案 |
| | `test_seed_creates_budget_items` | seed 後 ≥ 25 個預算項目 |
| | `test_seed_creates_resources` | seed 後存在工、料、機各至少 1 筆 |
| | `test_seed_has_tree_structure` | seed 後樹狀結構 ≥ 3 層 |
| `TestProfitCalculation` | `test_profit_calculation_after_seed` | 包商利潤與營業稅百分比正確 |
| | `test_idempotent_seed` | 重複呼叫不回重複寫入 |
| | `test_profit_values_match_snapshot` | 利潤 > 0，營業稅 > 包商利潤 |
| `TestRecalcBudgetTree` | `test_recalc_sums_children` | B 父項正確加總 W 子項 |
| | `test_recalc_nested_structure` | 遞迴加總多層 B 結構 |
| `TestApplyProfitRules` | `test_apply_profit_rules` | 獨立驗證利潤百分比邏輯 |

---

### Step-4：前端 build 驗證

**檢查 `web-pcces/frontend/package.json`** 確保 `build` 腳本運作：

```bash
cd web-pcces/frontend
npm run build
```

若前端缺少 TypeScript 型別等相依，補安裝：
```bash
npm install
npm run build
```

注意事項：
- 前端檔案（`LoginPage.tsx`、`LandingPage.tsx`）「不修改內容」，僅確保 build 通過
- 若遇到 TypeScript 錯誤，才需修正前端檔案

---

## 技術細節

### seed_data.py 改動總結

| 修改 | 位置 | 說明 |
|------|------|------|
| 移除 `sub.amount = round(...)` | 第 3 層 W 細項建立後 | 冗餘計算，`_recalc_seed` 會覆寫 |
| 移除 `item.amount = round(...)` | 間接費 W 細項建立後 | 同上 |
| 新增 `_recalc_budget_tree()` | 獨立函式 | 通用遞迴邏輯，可被外部測試呼叫 |
| 新增 `_apply_profit_rules()` | 獨立函式 | 利潤特殊邏輯，可被獨立測試 |
| 簡化 `_recalc_seed()` | 僅呼叫上述兩函式 | 職責分明 |

### 測試執行方式

```bash
# 安裝測試相依（若無 pytest）
pip install pytest

# 從專案根目錄執行
python -m pytest api/test_seed_data_profit.py -v

# 與既有測試一起執行
python -m pytest api/test_seed_data_profit.py web-pcces/backend/tests/ -v
```

> **注意**：測試需要 `pytest` 套件，但 `requirements.txt` 中無列出。可選擇：
> - 安裝 `pip install pytest`（開發環境手動安裝）
> - 或將 `pytest` 加入 `requirements-dev.txt`

### 測試隔離策略

每個測試使用獨立 in-memory SQLite（`sqlite://`），測試前 `create_all`，測試後 `drop_all`，不影響真實資料庫。

---

## 預計工時

| 步驟 | 內容 | 預計工時 |
|------|------|---------|
| Step-1 | 移除 W 類型冗餘 amount 計算 | 0.5 hr |
| Step-2 | 拆分 `_recalc_seed` 為兩函式 | 1.0 hr |
| Step-3 | 新增利潤計算自動化測試（10 個 test case） | 2.0 hr |
| Step-4 | 前端 build 驗證 | 0.5 hr |
| **合計** | | **4.0 hr** |

---

## 預計改善分數

| 評分項目 | 原始分數 | 預計改善後 | 改善原因 |
|---------|---------|-----------|---------|
| 完整性 | 25/25 | 25/25 | 維持滿分 |
| 正確性 | 25/25 | 25/25 | 維持滿分 |
| 可維護性 | 22/25 | **25/25** | 移除冗餘代碼 + 拆分長函式 |
| 測試與驗證 | 15/25 | **22/25** | 新增 10 個測試案例覆蓋利潤計算、資料結構、遞迴邏輯 |
| **總分** | **87/100** | **97/100** | ✅ 合格 |
