# TASK-004 — 計價管理模組（Invoice）

## 狀態
📋 計畫撰寫中

## 目標
實作 PCCES 網頁版的計價管理（Invoice）模組，讓使用者能夠：
1. 選取專案後進入計價管理
2. 新增計價期別（第 N 期）
3. 從預算項目選取已完成工項，輸入完成數量/比例
4. 自動計算計價金額與累計進度
5. 產出計價明細表與匯出 Excel

---

## 資料庫模型設計

### 新增表格

#### 1. `invoices` — 計價主檔

| 欄位 | 型別 | 說明 |
|------|------|------|
| `id` | Integer PK | 主鍵 |
| `project_id` | Integer FK → projects.id | 所屬專案（CASCADE 刪除） |
| `invoice_no` | String(50) | 計價編號，如「INV-2025-001」 |
| `period_no` | Integer | 期次編號（1, 2, 3…） |
| `c_name` | String(300) | 計價名稱，如「第1期計價」 |
| `status` | String(20) | 狀態：`draft`(草稿), `submitted`(已送審), `approved`(已核准) |
| `total_amount` | Float | 本期計價總金額（自動加總明細） |
| `cumulative_amount` | Float | 累計計價金額（含前期） |
| `progress_rate` | Float | 累計進度百分比（0~100） |
| `invoice_date` | Date | 計價日期 |
| `remark` | Text | 備註 |
| `created_by` | Integer FK → users.id | 建立者 |
| `created_at` | DateTime | 建立時間 |
| `updated_at` | DateTime | 更新時間 |

#### 2. `invoice_items` — 計價明細

| 欄位 | 型別 | 說明 |
|------|------|------|
| `id` | Integer PK | 主鍵 |
| `invoice_id` | Integer FK → invoices.id | 所屬計價主檔（CASCADE 刪除） |
| `budget_item_id` | Integer FK → budget_items.id | 對應預算項目（SET NULL） |
| `item_no` | String(50) | 項次（從預算項目複製） |
| `print_no` | String(50) | 印號（從預算項目複製） |
| `c_name` | String(500) | 工項名稱（從預算項目複製） |
| `c_unit` | String(50) | 單位（從預算項目複製） |
| `contract_qty` | Float | 契約數量 |
| `unit_price` | Float | 單價（從預算項目引用） |
| `prev_completed_qty` | Float | 前期累計完成數量 |
| `this_completed_qty` | Float | **本期完成數量**（使用者輸入） |
| `total_completed_qty` | Float | 累計完成數量 = prev + this |
| `remain_qty` | Float | 剩餘數量 = contract - total |
| `this_amount` | Float | 本期金額 = this_qty × unit_price |
| `cumulative_amount` | Float | 累計金額 = total_qty × unit_price |
| `progress_rate` | Float | 完成比例 % = total_qty / contract_qty × 100 |
| `sort_order` | String(50) | 排序 |
| `remark` | Text | 備註 |
| `created_at` | DateTime | 建立時間 |
| `updated_at` | DateTime | 更新時間 |

### 關聯圖

```
projects (1) ──→ (N) invoices (1) ──→ (N) invoice_items
                  │
                  └──→ budget_items (reference, FK nullable)
```

### 業務邏輯規則

1. **自動計算** — 修改 `this_completed_qty` 時自動重算：
   - `total_completed_qty = prev_completed_qty + this_completed_qty`
   - `remain_qty = contract_qty - total_completed_qty`
   - `this_amount = this_completed_qty × unit_price`
   - `cumulative_amount = total_completed_qty × unit_price`
   - `progress_rate = (total_completed_qty / contract_qty) × 100`

2. **期次遞增** — 新增計價時自動給定下一期次編號

3. **前期資料繼承** — 新增第 N 期時，自動將第 N-1 期的 `total_completed_qty` 設為第 N 期的 `prev_completed_qty`

4. **累計檢查** — `this_completed_qty` 不得使 `total_completed_qty > contract_qty`（超量警告可選）

---

## API 端點設計

### 計價主檔 (Invoices)

| 方法 | 路徑 | 說明 | 權限 |
|------|------|------|------|
| `GET` | `/api/projects/{pid}/invoices/` | 列表（含累計金額） | 專案存取 |
| `POST` | `/api/projects/{pid}/invoices/` | 新增計價期別 | 編輯 |
| `GET` | `/api/projects/{pid}/invoices/{iid}` | 單筆明細 | 專案存取 |
| `PUT` | `/api/projects/{pid}/invoices/{iid}` | 更新主檔資訊 | 編輯 |
| `DELETE` | `/api/projects/{pid}/invoices/{iid}` | 刪除（含明細） | 管理員 |

### 計價明細 (Invoice Items)

| 方法 | 路徑 | 說明 |
|------|------|------|
| `GET` | `/api/projects/{pid}/invoices/{iid}/items` | 明細列表（依 sort_order 排序） |
| `POST` | `/api/projects/{pid}/invoices/{iid}/items` | 新增單筆明細 |
| `PUT` | `/api/projects/{pid}/invoices/{iid}/items/{item_id}` | 更新完成數量 → 自動重算 |
| `DELETE` | `/api/projects/{pid}/invoices/{iid}/items/{item_id}` | 刪除明細 |

### 批次操作

| 方法 | 路徑 | 說明 |
|------|------|------|
| `POST` | `/api/projects/{pid}/invoices/{iid}/items/batch` | 批次從預算 W 項目建立明細 |
| `POST` | `/api/projects/{pid}/invoices/{iid}/calculate` | 重新計算加總金額 |

### 進度查詢

| 方法 | 路徑 | 說明 |
|------|------|------|
| `GET` | `/api/projects/{pid}/invoices/progress` | 所有期次累計進度圖表資料 |
| `GET` | `/api/projects/{pid}/invoices/summary` | 計價摘要統計 |

### 報表匯出

| 方法 | 路徑 | 說明 |
|------|------|------|
| `GET` | `/api/projects/{pid}/invoices/{iid}/report` | 計價明細表（HTML/JSON） |
| `GET` | `/api/projects/{pid}/invoices/{iid}/report/excel` | 匯出 Excel |

---

## 前端頁面與元件樹

### 路由規劃

```
/app/projects/:id/invoices              → InvoiceListPage（計價列表）
/app/projects/:id/invoices/:invoiceId   → InvoiceDetailPage（計價編輯）
```

### 側邊欄選單（AppLayout 新增）

在「資源管理」與「報表分析」之間新增選單項目：
```
{ key: `invoices-${projectId}`, icon: <DollarOutlined />, label: '計價管理' }
```

### InvoiceListPage（計價列表頁）

```
InvoiceListPage
├── PageHeader (Title + Button "新增計價")
├── InvoiceSummaryCards
│   ├── Card: 總計價次數
│   ├── Card: 本期累計金額
│   ├── Card: 總計價金額
│   └── Card: 平均進度
├── InvoiceTable (Ant Design Table)
│   ├── Column: 期次/編號
│   ├── Column: 名稱
│   ├── Column: 計價日期
│   ├── Column: 本期金額
│   ├── Column: 累計金額
│   ├── Column: 進度 (Progress Bar)
│   ├── Column: 狀態 (Tag)
│   └── Column: 操作 (檢視/編輯/刪除)
└── CreateInvoiceModal
    ├── Form: invoice_no, c_name, invoice_date
    └── Button: 確認建立 + 自動導入前期資料
```

### InvoiceDetailPage（計價編輯頁）

```
InvoiceDetailPage
├── Breadcrumb (專案名稱 > 計價管理 > 第N期)
├── InvoiceHeader
│   ├── Row: 計價編號, 名稱, 日期
│   ├── Row: 本期總金額, 累計總金額
│   ├── Row: 累計進度 (Progress Bar, 大字顯示 %)
│   └── Row: 操作按鈕
│       ├── Button: "批次導入預算工項"
│       ├── Button: "重新計算"
│       ├── Button: "匯出 Excel"
│       └── Button: "返回列表"
├── InvoiceItemGrid (AG Grid)
│   ├── Column: 項次 (readonly)
│   ├── Column: 印號 (readonly)
│   ├── Column: 工項名稱 (readonly)
│   ├── Column: 單位 (readonly)
│   ├── Column: 契約數量 (readonly)
│   ├── Column: 單價 (readonly)
│   ├── Column: 前期完成 (readonly)
│   ├── Column: 本期完成 (✅ Editable Cell — 使用者輸入)
│   ├── Column: 累計完成 (auto)
│   ├── Column: 剩餘數量 (auto)
│   ├── Column: 本期金額 (auto)
│   ├── Column: 累計金額 (auto)
│   └── Column: 完成比例 (auto, Progress Bar)
├── InvoiceSummaryFooter
│   ├── Row: 本期合計金額
│   ├── Row: 前期累計金額
│   ├── Row: 累計總金額
│   └── Row: 整體進度 %
└── BatchImportModal
    ├── TreeSelect: 選擇預算項目（篩選 W 類型）
    └── Button: 確認匯入
```

### 圖形化進度顯示（InvoiceProgressChart）

```
InvoiceProgressChart (在 InvoiceListPage 或獨立區塊)
├── ECharts Line Chart: X=期次, Y=累計金額
├── ECharts Bar Chart: X=期次, Y=本期金額
└── ECharts Gauge: 整體進度 %
```

---

## 實作步驟

### Step 1: 資料庫模型 — 新增 invoice 相關表格

**修改檔案：`api/models.py`**

在現有 models 中新增 `Invoice` 與 `InvoiceItem` 兩個 ORM 類別：

```python
class Invoice(Base):
    """計價主檔"""
    __tablename__ = "invoices"

    id = Column(Integer, primary_key=True, autoincrement=True)
    project_id = Column(Integer, ForeignKey("projects.id", ondelete="CASCADE"), nullable=False)
    invoice_no = Column(String(50), nullable=False)
    period_no = Column(Integer, nullable=False, default=1)
    c_name = Column(String(300), nullable=True)
    status = Column(String(20), default="draft")  # draft / submitted / approved
    total_amount = Column(Float, default=0)
    cumulative_amount = Column(Float, default=0)
    progress_rate = Column(Float, default=0)
    invoice_date = Column(String(20), nullable=True)  # YYYY-MM-DD
    remark = Column(Text, nullable=True)
    created_by = Column(Integer, ForeignKey("users.id"), nullable=True)
    created_at = Column(DateTime, default=_now)
    updated_at = Column(DateTime, default=_now, onupdate=_now)

    # 關係
    items = relationship("InvoiceItem", backref="invoice", passive_deletes=True,
                        order_by="InvoiceItem.sort_order, InvoiceItem.id")


class InvoiceItem(Base):
    """計價明細"""
    __tablename__ = "invoice_items"

    id = Column(Integer, primary_key=True, autoincrement=True)
    invoice_id = Column(Integer, ForeignKey("invoices.id", ondelete="CASCADE"), nullable=False)
    budget_item_id = Column(Integer, ForeignKey("budget_items.id", ondelete="SET NULL"), nullable=True)

    item_no = Column(String(50), nullable=True)
    print_no = Column(String(50), nullable=True)
    c_name = Column(String(500), nullable=True)
    c_unit = Column(String(50), nullable=True)

    contract_qty = Column(Float, default=0)
    unit_price = Column(Float, default=0)

    prev_completed_qty = Column(Float, default=0)
    this_completed_qty = Column(Float, default=0)
    total_completed_qty = Column(Float, default=0)
    remain_qty = Column(Float, default=0)

    this_amount = Column(Float, default=0)
    cumulative_amount = Column(Float, default=0)
    progress_rate = Column(Float, default=0)

    sort_order = Column(String(50), nullable=True)
    remark = Column(Text, nullable=True)
    created_at = Column(DateTime, default=_now)
    updated_at = Column(DateTime, default=_now, onupdate=_now)
```

**修改檔案：`api/index.py`** — 匯入新模型（無需手動改 import 即可，因使用 `from api.models import *` 或 `Base.metadata` 會自動註冊）

**預計工時：1.0 hr**

---

### Step 2: 後端 API — 計價主檔 CRUD

**修改檔案：`api/index.py`**

新增 `Invoice` / `InvoiceItem` 的匯入，以及以下 API 端點：

#### 2a. 列表 + 新增

```python
@app.route("/api/projects/<int:project_id>/invoices/", methods=["GET"])
@require_auth
def list_invoices(project_id, user_id):
    """列出專案所有計價期別（含統計資料）"""
    ...

@app.route("/api/projects/<int:project_id>/invoices/", methods=["POST"])
@require_auth
def create_invoice(project_id, user_id):
    """新增計價期別（自動計算下一期次、繼承前期資料）"""
    ...
```

**核心邏輯：**
- `list_invoices` — 回傳所有 invoices，並計算每個的累計金額
- `create_invoice` — 自動查詢最大 `period_no` 並 +1；若有前期則複製前期已完成的工項明細作為 `prev_completed_qty`；若無前期則從 budget_items (W類型) 建立初始明細

#### 2b. 單筆查詢/更新/刪除

```python
@app.route("/api/projects/<int:project_id>/invoices/<int:invoice_id>", methods=["GET"])
@require_auth
def get_invoice(project_id, invoice_id, user_id):
    ...

@app.route("/api/projects/<int:project_id>/invoices/<int:invoice_id>", methods=["PUT"])
@require_auth
def update_invoice(project_id, invoice_id, user_id):
    ...

@app.route("/api/projects/<int:project_id>/invoices/<int:invoice_id>", methods=["DELETE"])
@require_auth
def delete_invoice(project_id, invoice_id, user_id):
    ...
```

#### 2c. 重新計算與狀態變更

```python
@app.route("/api/projects/<int:project_id>/invoices/<int:invoice_id>/calculate", methods=["POST"])
@require_auth
def recalc_invoice(project_id, invoice_id, user_id):
    """重新加總本期金額、累計金額、進度%"""
    ...

@app.route("/api/projects/<int:project_id>/invoices/<int:invoice_id>/submit", methods=["POST"])
@require_auth
def submit_invoice(project_id, invoice_id, user_id):
    """提交審核（draft → submitted）"""
    ...

@app.route("/api/projects/<int:project_id>/invoices/<int:invoice_id>/approve", methods=["POST"])
@require_auth
def approve_invoice(project_id, invoice_id, user_id):
    """核准（submitted → approved，僅管理員）"""
    ...
```

**預計工時：2.0 hr**

---

### Step 3: 後端 API — 計價明細 CRUD + 批次匯入

**修改檔案：`api/index.py`**

#### 3a. 明細 CRUD

```python
@app.route("/api/projects/<int:project_id>/invoices/<int:invoice_id>/items", methods=["GET"])
@require_auth
def list_invoice_items(project_id, invoice_id, user_id):
    ...

@app.route("/api/projects/<int:project_id>/invoices/<int:invoice_id>/items", methods=["POST"])
@require_auth
def create_invoice_item(project_id, invoice_id, user_id):
    ...

@app.route("/api/projects/<int:project_id>/invoices/<int:invoice_id>/items/<int:item_id>", methods=["PUT"])
@require_auth
def update_invoice_item(project_id, invoice_id, item_id, user_id):
    """更新完成數量 → 自動重算相關欄位"""
    ...

@app.route("/api/projects/<int:project_id>/invoices/<int:invoice_id>/items/<int:item_id>", methods=["DELETE"])
@require_auth
def delete_invoice_item(project_id, invoice_id, item_id, user_id):
    ...
```

**核心邏輯 (`update_invoice_item`)：**
```python
# 更新 this_completed_qty 後的自動計算
item.total_completed_qty = item.prev_completed_qty + item.this_completed_qty
item.remain_qty = item.contract_qty - item.total_completed_qty
item.this_amount = round(item.this_completed_qty * item.unit_price, 2)
item.cumulative_amount = round(item.total_completed_qty * item.unit_price, 2)
if item.contract_qty > 0:
    item.progress_rate = round((item.total_completed_qty / item.contract_qty) * 100, 2)
else:
    item.progress_rate = 0
# 更新後自動重算 invoice 加總
_recalc_invoice_totals(db, invoice_id)
```

#### 3b. 批次匯入

```python
@app.route("/api/projects/<int:project_id>/invoices/<int:invoice_id>/items/batch", methods=["POST"])
@require_auth
def batch_import_invoice_items(project_id, invoice_id, user_id):
    """從預算項目批次匯入 W 類型工項作為計價明細"""
    data = request.get_json()
    budget_item_ids = data.get("budget_item_ids", [])  # [] 表示全部 W 項目
    
    # 若未指定 ID 則匯入所有 W 類型的預算項目
    query = db.query(BudgetItem).filter(
        BudgetItem.project_id == project_id,
        BudgetItem.kind == BudgetItemKind.W
    )
    if budget_item_ids:
        query = query.filter(BudgetItem.id.in_(budget_item_ids))
    
    # 檢查哪些工項已存在，跳過重複
    existing = {item.budget_item_id for item in db.query(InvoiceItem)
                .filter(InvoiceItem.invoice_id == invoice_id).all()}
    
    count = 0
    for bi in query.all():
        if bi.id in existing:
            continue
        # 查詢前期已完成數量
        prev_qty = _get_prev_completed_qty(db, project_id, invoice_id, bi.id)
        item = InvoiceItem(
            invoice_id=invoice_id,
            budget_item_id=bi.id,
            item_no=bi.item_no,
            print_no=bi.print_no,
            c_name=bi.c_name,
            c_unit=bi.c_unit,
            contract_qty=bi.quantity or 0,
            unit_price=bi.unit_price or 0,
            prev_completed_qty=prev_qty,
            this_completed_qty=0,
            sort_order=bi.sort_order,
        )
        # 自動計算
        item.total_completed_qty = item.prev_completed_qty
        item.remain_qty = item.contract_qty - item.total_completed_qty
        db.add(item)
        count += 1
    
    # 重算 invoice 總額
    _recalc_invoice_totals(db, invoice_id)
    db.commit()
    return jsonify({"message": f"已匯入 {count} 筆工項", "count": count})
```

#### 3c. 輔助函式

```python
def _get_prev_completed_qty(db: Session, project_id: int, current_invoice_id: int, budget_item_id: int) -> float:
    """查詢前期累計完成數量（取前一期的 total_completed_qty）"""
    # 取得目前期次
    current = db.query(Invoice).filter(Invoice.id == current_invoice_id).first()
    if not current or current.period_no <= 1:
        return 0
    # 查詢前一期的同一 budget_item 的明細
    prev_invoice = db.query(Invoice).filter(
        Invoice.project_id == project_id,
        Invoice.period_no == current.period_no - 1
    ).first()
    if prev_invoice:
        prev_item = db.query(InvoiceItem).filter(
            InvoiceItem.invoice_id == prev_invoice.id,
            InvoiceItem.budget_item_id == budget_item_id
        ).first()
        if prev_item:
            return prev_item.total_completed_qty or 0
    return 0

def _recalc_invoice_totals(db: Session, invoice_id: int):
    """重新計算 invoice 主檔的加總金額與進度"""
    invoice = db.query(Invoice).filter(Invoice.id == invoice_id).first()
    if not invoice:
        return
    items = db.query(InvoiceItem).filter(InvoiceItem.invoice_id == invoice_id).all()
    invoice.total_amount = round(sum(i.this_amount or 0 for i in items), 2)
    
    # 累計金額需包含前期
    # 從第一期開始累加所有 previous invoices 的 total_amount + 本期的
    all_invoices = db.query(Invoice).filter(
        Invoice.project_id == invoice.project_id,
        Invoice.period_no <= invoice.period_no
    ).order_by(Invoice.period_no).all()
    invoice.cumulative_amount = round(sum(iv.total_amount or 0 for iv in all_invoices), 2)
    
    # 進度 = 累計金額 / 總預算金額
    total_budget = db.query(func.coalesce(func.sum(BudgetItem.amount), 0)).filter(
        BudgetItem.project_id == invoice.project_id,
        BudgetItem.parent_id.is_(None)  # 根節點總額
    ).scalar() or 0
    # 更精確：取所有根節點 B/Z 類型金額加總
    root_items = db.query(BudgetItem).filter(
        BudgetItem.project_id == invoice.project_id,
        BudgetItem.parent_id.is_(None)
    ).all()
    total_budget = sum((i.amount or 0) for i in root_items)
    
    if total_budget > 0:
        invoice.progress_rate = round((invoice.cumulative_amount / total_budget) * 100, 2)
    else:
        invoice.progress_rate = 0
    
    db.flush()
```

#### 3d. 進度查詢 API

```python
@app.route("/api/projects/<int:project_id>/invoices/progress", methods=["GET"])
@require_auth
def get_invoice_progress(project_id, user_id):
    """回傳所有期次的累計進度（圖表用）"""
    invoices = db.query(Invoice).filter(
        Invoice.project_id == project_id
    ).order_by(Invoice.period_no).all()
    
    return jsonify({
        "periods": [i.period_no for i in invoices],
        "labels": [i.c_name or f"第{i.period_no}期" for i in invoices],
        "this_amounts": [i.total_amount or 0 for i in invoices],
        "cumulative_amounts": [i.cumulative_amount or 0 for i in invoices],
        "progress_rates": [i.progress_rate or 0 for i in invoices],
    })

@app.route("/api/projects/<int:project_id>/invoices/summary", methods=["GET"])
@require_auth
def get_invoice_summary(project_id, user_id):
    """計價摘要統計"""
    ...
```

**預計工時：2.5 hr**

---

### Step 4: 前端型別與 API 服務

**修改檔案：`web-pcces/frontend/src/types.ts`**

新增型別定義：

```typescript
// ─── 計價 ───
export interface Invoice {
  id: number;
  project_id: number;
  invoice_no: string;
  period_no: number;
  c_name: string | null;
  status: 'draft' | 'submitted' | 'approved';
  total_amount: number;
  cumulative_amount: number;
  progress_rate: number;
  invoice_date: string | null;
  remark: string | null;
  created_by: number | null;
  created_at: string;
  updated_at: string;
}

export interface InvoiceCreateData {
  invoice_no?: string;
  c_name?: string;
  invoice_date?: string;
  remark?: string;
}

export interface InvoiceItem {
  id: number;
  invoice_id: number;
  budget_item_id: number | null;
  item_no: string | null;
  print_no: string | null;
  c_name: string | null;
  c_unit: string | null;
  contract_qty: number;
  unit_price: number;
  prev_completed_qty: number;
  this_completed_qty: number;
  total_completed_qty: number;
  remain_qty: number;
  this_amount: number;
  cumulative_amount: number;
  progress_rate: number;
  sort_order: string | null;
  remark: string | null;
}

export interface InvoiceProgress {
  periods: number[];
  labels: string[];
  this_amounts: number[];
  cumulative_amounts: number[];
  progress_rates: number[];
}

export interface InvoiceSummary {
  total_invoices: number;
  total_invoice_amount: number;
  total_cumulative_amount: number;
  overall_progress_rate: number;
  latest_invoice: Invoice | null;
}
```

**修改檔案：`web-pcces/frontend/src/api.ts`**

新增 API 服務：

```typescript
// ═══ 計價 ═══

export const invoiceApi = {
  // 主檔
  list: async (projectId: number): Promise<Invoice[]> => {
    const res = await api.get(`/projects/${projectId}/invoices/`);
    return res.data;
  },
  get: async (projectId: number, invoiceId: number): Promise<Invoice> => {
    const res = await api.get(`/projects/${projectId}/invoices/${invoiceId}`);
    return res.data;
  },
  create: async (projectId: number, data: InvoiceCreateData): Promise<Invoice> => {
    const res = await api.post(`/projects/${projectId}/invoices/`, data);
    return res.data;
  },
  update: async (projectId: number, invoiceId: number, data: Partial<InvoiceCreateData>): Promise<Invoice> => {
    const res = await api.put(`/projects/${projectId}/invoices/${invoiceId}`, data);
    return res.data;
  },
  delete: async (projectId: number, invoiceId: number): Promise<void> => {
    await api.delete(`/projects/${projectId}/invoices/${invoiceId}`);
  },
  calculate: async (projectId: number, invoiceId: number): Promise<Invoice> => {
    const res = await api.post(`/projects/${projectId}/invoices/${invoiceId}/calculate`);
    return res.data;
  },
  submit: async (projectId: number, invoiceId: number): Promise<Invoice> => {
    const res = await api.post(`/projects/${projectId}/invoices/${invoiceId}/submit`);
    return res.data;
  },
  approve: async (projectId: number, invoiceId: number): Promise<Invoice> => {
    const res = await api.post(`/projects/${projectId}/invoices/${invoiceId}/approve`);
    return res.data;
  },

  // 明細
  getItems: async (projectId: number, invoiceId: number): Promise<InvoiceItem[]> => {
    const res = await api.get(`/projects/${projectId}/invoices/${invoiceId}/items`);
    return res.data;
  },
  createItem: async (projectId: number, invoiceId: number, data: Partial<InvoiceItem>): Promise<InvoiceItem> => {
    const res = await api.post(`/projects/${projectId}/invoices/${invoiceId}/items`, data);
    return res.data;
  },
  updateItem: async (projectId: number, invoiceId: number, itemId: number, data: Partial<InvoiceItem>): Promise<InvoiceItem> => {
    const res = await api.put(`/projects/${projectId}/invoices/${invoiceId}/items/${itemId}`, data);
    return res.data;
  },
  deleteItem: async (projectId: number, invoiceId: number, itemId: number): Promise<void> => {
    await api.delete(`/projects/${projectId}/invoices/${invoiceId}/items/${itemId}`);
  },
  batchImport: async (projectId: number, invoiceId: number, budgetItemIds?: number[]): Promise<any> => {
    const res = await api.post(`/projects/${projectId}/invoices/${invoiceId}/items/batch`, {
      budget_item_ids: budgetItemIds || [],
    });
    return res.data;
  },

  // 進度與報表
  getProgress: async (projectId: number): Promise<InvoiceProgress> => {
    const res = await api.get(`/projects/${projectId}/invoices/progress`);
    return res.data;
  },
  getSummary: async (projectId: number): Promise<InvoiceSummary> => {
    const res = await api.get(`/projects/${projectId}/invoices/summary`);
    return res.data;
  },
  getReportUrl: (projectId: number, invoiceId: number) => {
    return `/api/projects/${projectId}/invoices/${invoiceId}/report`;
  },
  getExcelUrl: (projectId: number, invoiceId: number) => {
    return `/api/projects/${projectId}/invoices/${invoiceId}/report/excel`;
  },
};
```

**預計工時：1.0 hr**

---

### Step 5: 前端 — 新增路由與側邊欄

**修改檔案：`web-pcces/frontend/src/App.tsx`**

新增路由：
```tsx
import InvoiceListPage from './pages/InvoiceListPage';
import InvoiceDetailPage from './pages/InvoiceDetailPage';

// 在 /app 路由下新增
<Route path="projects/:id/invoices" element={<InvoiceListPage />} />
<Route path="projects/:id/invoices/:invoiceId" element={<InvoiceDetailPage />} />
```

**修改檔案：`web-pcces/frontend/src/components/AppLayout.tsx`**

在側邊欄選單中「資源管理」與「報表分析」之間插入計價選單：

```tsx
// 在 menuItems push 資源管理之後
{ key: `invoices-${projectId}`, icon: <DollarOutlined />, label: '計價管理' },
{ key: `reports-${projectId}`, icon: <BarChartOutlined />, label: '報表分析' },

// 在 handleMenuClick 新增
else if (info.key.startsWith('invoices-')) navigate(`/projects/${projectId}/invoices`);
```

需要新增 Ant Design 圖示匯入：
```tsx
import { DollarOutlined } from '@ant-design/icons';
```

**預計工時：0.5 hr**

---

### Step 6: 前端 — InvoiceListPage（計價列表頁）

**新增檔案：`web-pcces/frontend/src/pages/InvoiceListPage.tsx`**

主要功能：
1. 載入 invoices 列表 + progress 資料
2. 統計卡片（總期數、累計金額、平均進度）
3. 表格顯示各期次資料
4. 新增計價 Modal
5. 點擊行進入計價編輯頁
6. 進度圖表（ECharts 折線 + 長條圖）

**預計工時：3.0 hr**

---

### Step 7: 前端 — InvoiceDetailPage（計價編輯頁）

**新增檔案：`web-pcces/frontend/src/pages/InvoiceDetailPage.tsx`**

主要功能：
1. Header 顯示計價主檔資訊
2. AG Grid 顯示所有明細，本期完成數量可編輯
3. 編輯後自動計算並更新（onCellValueChanged）
4. 批次匯入 Modal（從預算項目選取 W 工項）
5. 重新計算按鈕
6. 匯出 Excel 按鈕
7. 提交/核准狀態變更

**AG Grid 欄位定義重點：**
```tsx
const columnDefs = [
  { field: 'item_no', headerName: '項次', width: 80 },
  { field: 'print_no', headerName: '印號', width: 100 },
  { field: 'c_name', headerName: '工項名稱', width: 250 },
  { field: 'c_unit', headerName: '單位', width: 60 },
  { field: 'contract_qty', headerName: '契約數量', width: 100, type: 'numericColumn' },
  { field: 'unit_price', headerName: '單價', width: 100, type: 'numericColumn',
    valueFormatter: (p) => `$${p.value?.toLocaleString()}` },
  { field: 'prev_completed_qty', headerName: '前期完成', width: 100, type: 'numericColumn' },
  { 
    field: 'this_completed_qty', headerName: '本期完成', width: 110, type: 'numericColumn',
    editable: true,  // ← 唯一可編輯欄位
    cellClass: 'editable-cell',
    cellEditor: 'agNumberCellEditor',
    cellEditorParams: { precision: 2 },
  },
  { field: 'total_completed_qty', headerName: '累計完成', width: 100, type: 'numericColumn' },
  { field: 'remain_qty', headerName: '剩餘數量', width: 100, type: 'numericColumn' },
  { field: 'this_amount', headerName: '本期金額', width: 120, type: 'numericColumn',
    valueFormatter: (p) => `$${p.value?.toLocaleString()}` },
  { field: 'cumulative_amount', headerName: '累計金額', width: 120, type: 'numericColumn',
    valueFormatter: (p) => `$${p.value?.toLocaleString()}` },
  {
    field: 'progress_rate', headerName: '進度', width: 120,
    cellRenderer: 'agAnimateShowChangeCellRenderer',
    cellRendererParams: {
      innerRenderer: ProgressBarRenderer,  // 自訂進度條渲染
    },
  },
];
```

**預計工時：5.0 hr**

---

### Step 8: Excel 匯出 — 計價明細表

**修改檔案：`api/index.py`**

新增 Excel 匯出端點，參考現有 ReportsPage 的匯出邏輯：

```python
@app.route("/api/projects/<int:project_id>/invoices/<int:invoice_id>/report/excel", methods=["GET"])
@require_auth
def export_invoice_excel(project_id, invoice_id, user_id):
    """匯出計價明細表 Excel"""
    import openpyxl
    from openpyxl.styles import Font, PatternFill, Alignment, Border, Side
    from openpyxl.utils import get_column_letter
    
    db = next(get_db())
    try:
        invoice = db.query(Invoice).filter(
            Invoice.id == invoice_id,
            Invoice.project_id == project_id
        ).first()
        if not invoice:
            return jsonify({"detail": "計價資料不存在"}), 404
        
        items = db.query(InvoiceItem).filter(
            InvoiceItem.invoice_id == invoice_id
        ).order_by(InvoiceItem.sort_order, InvoiceItem.id).all()
        
        # 建立 workbook
        wb = openpyxl.Workbook()
        ws = wb.active
        ws.title = f"計價明細表_第{invoice.period_no}期"
        
        # 標題區
        # ... (詳細格式設定)
        
        # 寫入資料
        # ...
        
        # 儲存至暫存檔
        import tempfile
        tmp = tempfile.NamedTemporaryFile(delete=False, suffix='.xlsx')
        wb.save(tmp.name)
        tmp.close()
        
        return send_file(
            tmp.name,
            as_attachment=True,
            download_name=f"計價明細表_{invoice.invoice_no}.xlsx",
            mimetype="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
        )
    finally:
        db.close()
```

**預計工時：2.0 hr**

---

### Step 9: 示範資料 — 加入計價種子資料

**修改檔案：`api/seed_data.py`**

在 `seed_demo_data()` 中新增計價示範資料：

```python
# 8. 建立計價示範資料
# 第1期 — 開挖及安全措施工程完成 60%
invoice1 = Invoice(
    project_id=project.id,
    invoice_no="INV-2025-001",
    period_no=1,
    c_name="第1期計價 — 開挖及安全措施",
    status="approved",
    invoice_date="2025-03-15",
)
db.add(invoice1)
db.flush()

# 第1期明細（從開挖及安全措施工程的子項建立）
excavation = db.query(BudgetItem).filter(
    BudgetItem.project_id == project.id,
    BudgetItem.c_name == "開挖及安全措施工程"
).first()
if excavation:
    children = db.query(BudgetItem).filter(
        BudgetItem.parent_id == excavation.id
    ).all()
    for child in children:
        completed = round((child.quantity or 0) * 0.6, 2)  # 60%
        item = InvoiceItem(
            invoice_id=invoice1.id,
            budget_item_id=child.id,
            item_no=child.item_no,
            print_no=child.print_no,
            c_name=child.c_name,
            c_unit=child.c_unit,
            contract_qty=child.quantity or 0,
            unit_price=child.unit_price or 0,
            prev_completed_qty=0,
            this_completed_qty=completed,
            sort_order=child.sort_order,
        )
        item.total_completed_qty = completed
        item.remain_qty = item.contract_qty - completed
        item.this_amount = round(completed * item.unit_price, 2)
        item.cumulative_amount = item.this_amount
        if item.contract_qty > 0:
            item.progress_rate = round((item.total_completed_qty / item.contract_qty) * 100, 2)
        db.add(item)

_recalc_invoice_totals(db, invoice1.id)

# 第2期 — 結構體工程完成 30%
invoice2 = Invoice(
    project_id=project.id,
    invoice_no="INV-2025-002",
    period_no=2,
    c_name="第2期計價 — 結構體工程",
    status="draft",
    invoice_date="2025-05-20",
)
db.add(invoice2)
db.flush()

# 第2期明細...
```

**預計工時：1.0 hr**

---

### Step 10: 重新建置前端 + 端到端測試

**執行指令：**
```bash
cd web-pcces/frontend
npm run build
cp -r web-pcces/frontend/dist/* api/static/
```

**測試計畫：**

| 測試項目 | 測試步驟 | 預期結果 |
|---------|---------|---------|
| 計價列表顯示 | 進入專案 → 點擊「計價管理」 | 顯示示範計價資料（2期） |
| 新增計價期別 | 點擊「新增計價」→ 填寫資訊 → 確認 | 新增成功，列在列表 |
| 批次匯入工項 | 進入編輯頁 → 批次匯入 → 選取預算工項 | 明細新增成功 |
| 編輯完成數量 | 在 AG Grid 修改「本期完成」欄位 | 自動計算金額與進度 |
| 重新計算 | 點擊「重新計算」 | 總額與進度更新 |
| 累計進度正確 | 建立第2期 → 輸入完成數量 | 累計數量 = 第1期 + 第2期 |
| 匯出 Excel | 點擊「匯出 Excel」 | 下載 .xlsx 檔案 |
| 刪除計價 | 回到列表 → 刪除第2期 | 確認後刪除成功 |
| 示範資料正確 | 首次啟動應用 | 看到 2 期示範計價資料 |

**預計工時：2.0 hr**

---

## 工時統計

| 步驟 | 描述 | 預計工時 | 代理人 |
|------|------|---------|--------|
| Step 1 | 資料庫模型 — 新增 invoice 相關表格 | 1.0 hr | DEVELOPER |
| Step 2 | 後端 API — 計價主檔 CRUD | 2.0 hr | DEVELOPER |
| Step 3 | 後端 API — 計價明細 CRUD + 批次匯入 | 2.5 hr | DEVELOPER |
| Step 4 | 前端型別與 API 服務 | 1.0 hr | DEVELOPER |
| Step 5 | 前端 — 新增路由與側邊欄 | 0.5 hr | DEVELOPER |
| Step 6 | 前端 — InvoiceListPage（計價列表頁） | 3.0 hr | DEVELOPER |
| Step 7 | 前端 — InvoiceDetailPage（計價編輯頁） | 5.0 hr | DEVELOPER |
| Step 8 | Excel 匯出 — 計價明細表 | 2.0 hr | DEVELOPER |
| Step 9 | 示範資料 — 加入計價種子資料 | 1.0 hr | DEVELOPER |
| Step 10 | 重新建置前端 + 端到端測試 | 2.0 hr | DEVELOPER |
| **合計** | | **20.0 hrs** | |

---

## 技術細節與注意事項

### Vercel 部署相容性
- 所有後端修改在 `api/` 目錄下，Vercel 自動識別
- 前端靜態檔案需 build 後複製至 `api/static/`
- 使用 `openpyxl` 產生 Excel（已在 `requirements.txt` 中）
- SQLite 在 Vercel Serverless 環境為唯讀，需注意：
  - 正式部署應使用 PostgreSQL（Vercel Postgres / Neon）
  - 此次開發仍以 SQLite 為主，後續可切換

### AG Grid 授權
- 開發環境使用 AG Grid Community（免費）
- 若需 Enterprise 功能（如 Excel export、Row Grouping）可後續升級
- 此模組使用 Community 版即可滿足需求

### 自動計算效能
- 每次修改 `this_completed_qty` 後需：
  1. 重新計算該明細所有派生欄位（前端即時更新）
  2. 發送 PUT 請求到後端（後端也重算一次，確保正確性）
  3. 後端回傳更新後資料 → 更新前端顯示
  4. 更新 footer 總計

### 資料完整性
- 刪除計價時，CASCADE 自動刪除明細
- 修改預算項目單價**不會**自動更新已建立的計價明細（歷史資料鎖定）
- 期次不可跳號（1, 2, 3…），確保累計邏輯正確

### 前端 AG Grid 編輯實作要點

```tsx
// InvoiceDetailPage.tsx 關鍵實作片段
const onCellValueChanged = useCallback(async (event) => {
  if (event.colDef.field === 'this_completed_qty') {
    const rowData = event.data;
    try {
      // 更新後端
      const updated = await invoiceApi.updateItem(pid, invoiceId, rowData.id, {
        this_completed_qty: rowData.this_completed_qty
      });
      // 更新 AG Grid 中的唯讀欄位
      gridRef.current?.applyTransaction({
        update: [{
          ...rowData,
          total_completed_qty: updated.total_completed_qty,
          remain_qty: updated.remain_qty,
          this_amount: updated.this_amount,
          cumulative_amount: updated.cumulative_amount,
          progress_rate: updated.progress_rate,
        }]
      });
      // 更新 footer 總計
      loadInvoiceSummary();
    } catch (err) {
      message.error('更新失敗');
      gridRef.current?.stopEditing();
    }
  }
}, [pid, invoiceId]);
```

### 建議開發順序
```
Step 1 (models) → Step 2 (invoices CRUD) → Step 4 (types+api) → Step 5 (routing)
→ Step 6 (list page) → Step 3 (items CRUD) → Step 7 (detail page)
→ Step 8 (excel) → Step 9 (seed data) → Step 10 (build+test)
```

此順序確保：
- 後端 API 先完成，前端可直接對接測試
- 列表頁先完成，可建立資料再由明細頁編輯
- 種子資料最後加入，確保所有功能穩定後再補資料

---

## 檔案修改/新增清單

| 檔案 | 動作 | 說明 |
|------|------|------|
| `api/models.py` | **修改** | 新增 `Invoice`、`InvoiceItem` 兩個 ORM 類別 |
| `api/index.py` | **修改** | 新增～20 個 API 端點 + 輔助函式 |
| `web-pcces/frontend/src/types.ts` | **修改** | 新增 `Invoice`、`InvoiceItem`、`InvoiceProgress`、`InvoiceSummary` |
| `web-pcces/frontend/src/api.ts` | **修改** | 新增 `invoiceApi` 物件 |
| `web-pcces/frontend/src/App.tsx` | **修改** | 新增 `InvoiceListPage`、`InvoiceDetailPage` 路由 |
| `web-pcces/frontend/src/components/AppLayout.tsx` | **修改** | 側邊欄新增「計價管理」選單 |
| `web-pcces/frontend/src/pages/InvoiceListPage.tsx` | **新增** | 計價列表頁面（表格 + 圖表 + 新增 Modal） |
| `web-pcces/frontend/src/pages/InvoiceDetailPage.tsx` | **新增** | 計價編輯頁面（AG Grid + 批次匯入 + 匯出） |
| `api/seed_data.py` | **修改** | 新增 2 期示範計價資料 |
| `api/static/` | **更新** | 前端 build 產出 |
