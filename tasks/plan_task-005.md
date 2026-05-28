# TASK-005 開發計畫：分包合約管理模組（SplitContract）

## 概述

本模組實作 PCCES 的分包合約管理功能，對應原始 WinForms 的 `SplitContract`（分包合約管理）、`SubClose`（分包結算）、`SubFinal`（分包終驗）三個子系統。使用者可在專案中建立分包合約、選取預算工項、設定基本資料（廠商、金額、工期）、進行期別計價管理，以及結算與終驗管理。

---

## 1. 資料庫模型設計

### 1.1 新增資料表

#### contracts（分包合約主檔）

| 欄位 | 型別 | 說明 |
|------|------|------|
| id | Integer PK | 自動遞增 |
| project_id | Integer FK → projects.id | 所屬專案 |
| contract_no | String(50) | 合約編號（唯一 per project） |
| c_name | String(300) | 合約名稱 |
| vendor_name | String(300) | 廠商名稱 |
| vendor_contact | String(100), nullable | 廠商聯絡人 |
| vendor_phone | String(50), nullable | 廠商電話 |
| contract_amount | Float | 合約金額 |
| account_code | String(100), nullable | 會計科目代碼 |
| budget_year | String(20), nullable | 預算年度 |
| sign_date | String(20), nullable | 簽約日期 (YYYY-MM-DD) |
| start_date | String(20), nullable | 預計開工日期 |
| end_date | String(20), nullable | 預計完工日期 |
| actual_start_date | String(20), nullable | 實際開工日期 |
| actual_end_date | String(20), nullable | 實際完工日期 |
| payment_terms | Text, nullable | 付款條件 |
| remark | Text, nullable | 備註 |
| status | String(20), default="active" | 狀態：active / closed / finalized |
| created_by | Integer FK → users.id, nullable | 建立者 |
| created_at | DateTime | 建立時間 |
| updated_at | DateTime | 更新時間 |

#### contract_items（分包合約工項明細）

| 欄位 | 型別 | 說明 |
|------|------|------|
| id | Integer PK | 自動遞增 |
| contract_id | Integer FK → contracts.id (CASCADE) | 所屬合約 |
| budget_item_id | Integer FK → budget_items.id (SET NULL), nullable | 對應預算項目 |
| item_no | String(50), nullable | 項次 |
| print_no | String(50), nullable | 列印編號 |
| c_name | String(500) | 中文名稱 |
| c_unit | String(50), nullable | 單位 |
| quantity | Float | 合約數量 |
| unit_price | Float | 單價 |
| amount | Float | 金額 (qty × price) |
| sort_order | String(50), nullable | 排序 |
| remark | Text, nullable | 備註 |
| created_at | DateTime | 建立時間 |
| updated_at | DateTime | 更新時間 |

#### contract_issues（分包合約期別計價）

| 欄位 | 型別 | 說明 |
|------|------|------|
| id | Integer PK | 自動遞增 |
| contract_id | Integer FK → contracts.id (CASCADE) | 所屬合約 |
| issue_no | Integer | 期別編號（1-based） |
| c_name | String(300), nullable | 期別名稱 |
| issue_date | String(20), nullable | 計價日期 (YYYY-MM-DD) |
| total_amount | Float | 本期總金額 |
| cumulative_amount | Float | 累計金額 |
| progress_rate | Float | 進度百分比 |
| remark | Text, nullable | 備註 |
| status | String(20), default="draft" | 狀態：draft / submitted / approved |
| created_by | Integer FK → users.id, nullable | 建立者 |
| created_at | DateTime | 建立時間 |
| updated_at | DateTime | 更新時間 |

#### contract_issue_items（分包合約期別計價明細）

| 欄位 | 型別 | 說明 |
|------|------|------|
| id | Integer PK | 自動遞增 |
| issue_id | Integer FK → contract_issues.id (CASCADE) | 所屬期別 |
| contract_item_id | Integer FK → contract_items.id (CASCADE) | 對應合約工項 |
| c_name | String(500) | 名稱（快照） |
| c_unit | String(50), nullable | 單位（快照） |
| contract_qty | Float | 合約數量 |
| unit_price | Float | 單價 |
| prev_completed_qty | Float | 前期累計完成數量 |
| this_completed_qty | Float | 本期完成數量 |
| total_completed_qty | Float | 累計完成數量 |
| remain_qty | Float | 剩餘數量 |
| this_amount | Float | 本期金額 |
| cumulative_amount | Float | 累計金額 |
| progress_rate | Float | 進度百分比 |
| remark | Text, nullable | 備註 |
| created_at | DateTime | 建立時間 |
| updated_at | DateTime | 更新時間 |

#### contract_settlements（分包結算）

| 欄位 | 型別 | 說明 |
|------|------|------|
| id | Integer PK | 自動遞增 |
| contract_id | Integer FK → contracts.id (CASCADE) | 所屬合約 |
| settlement_no | String(50) | 結算編號 |
| c_name | String(300), nullable | 結算名稱 |
| settlement_date | String(20), nullable | 結算日期 (YYYY-MM-DD) |
| contract_amount | Float | 原合約金額（快照） |
| total_add_amount | Float | 追加金額合計 |
| total_deduct_amount | Float | 扣減金額合計 |
| settlement_amount | Float | 結算總金額 |
| remark | Text, nullable | 備註 |
| status | String(20), default="draft" | 狀態：draft / submitted / approved |
| created_by | Integer FK → users.id, nullable | 建立者 |
| created_at | DateTime | 建立時間 |
| updated_at | DateTime | 更新時間 |

#### contract_settlement_items（分包結算明細）

| 欄位 | 型別 | 說明 |
|------|------|------|
| id | Integer PK | 自動遞增 |
| settlement_id | Integer FK → contract_settlements.id (CASCADE) | 所屬結算 |
| budget_item_id | Integer FK → budget_items.id (SET NULL), nullable | 對應預算項目 |
| c_name | String(500) | 名稱 |
| c_unit | String(50), nullable | 單位 |
| contract_qty | Float | 合約數量 |
| contract_unit_price | Float | 合約單價 |
| contract_amount | Float | 合約金額 |
| actual_qty | Float | 實際數量 |
| actual_unit_price | Float | 實際單價 |
| actual_amount | Float | 實際金額 |
| diff_amount | Float | 差異金額（+追加/-扣減） |
| remark | Text, nullable | 備註 |
| created_at | DateTime | 建立時間 |
| updated_at | DateTime | 更新時間 |

#### contract_final_acceptances（分包終驗）

| 欄位 | 型別 | 說明 |
|------|------|------|
| id | Integer PK | 自動遞增 |
| contract_id | Integer FK → contracts.id (CASCADE) | 所屬合約 |
| acceptance_no | String(50) | 終驗編號 |
| c_name | String(300), nullable | 終驗名稱 |
| acceptance_date | String(20), nullable | 終驗日期 (YYYY-MM-DD) |
| inspector | String(100), nullable | 驗收人員 |
| result | String(50), nullable | 驗收結果：pass / conditional_pass / fail |
| defect_description | Text, nullable | 缺失說明 |
| remark | Text, nullable | 備註 |
| status | String(20), default="draft" | 狀態：draft / submitted / approved |
| created_by | Integer FK → users.id, nullable | 建立者 |
| created_at | DateTime | 建立時間 |
| updated_at | DateTime | 更新時間 |

#### contract_final_acceptance_items（分包終驗明細）

| 欄位 | 型別 | 說明 |
|------|------|------|
| id | Integer PK | 自動遞增 |
| acceptance_id | Integer FK → contract_final_acceptances.id (CASCADE) | 所屬終驗 |
| budget_item_id | Integer FK → budget_items.id (SET NULL), nullable | 對應預算項目 |
| c_name | String(500) | 名稱 |
| c_unit | String(50), nullable | 單位 |
| contract_qty | Float | 合約數量 |
| actual_qty | Float | 實際數量 |
| accepted_qty | Float | 驗收合格數量 |
| rejected_qty | Float | 不合格數量 |
| remark | Text, nullable | 備註 |
| created_at | DateTime | 建立時間 |
| updated_at | DateTime | 更新時間 |

### 1.2 模型關係圖（ER 簡圖）

```
Project (1) ──→ Contract (N)
Contract (1) ──→ ContractItem (N)
Contract (1) ──→ ContractIssue (N)
ContractIssue (1) ──→ ContractIssueItem (N)
Contract (1) ──→ ContractSettlement (N)
ContractSettlement (1) ──→ ContractSettlementItem (N)
Contract (1) ──→ ContractFinalAcceptance (N)
ContractFinalAcceptance (1) ──→ ContractFinalAcceptanceItem (N)
BudgetItem ←── ContractItem (FK)
BudgetItem ←── ContractSettlementItem (FK)
BudgetItem ←── ContractFinalAcceptanceItem (FK)
```

---

## 2. API 端點設計

### 2.1 分包合約 CRUD

| 方法 | 路徑 | 說明 |
|------|------|------|
| GET | `/api/projects/{project_id}/contracts/` | 列表 |
| POST | `/api/projects/{project_id}/contracts/` | 新增 |
| GET | `/api/projects/{project_id}/contracts/{contract_id}` | 單筆 |
| PUT | `/api/projects/{project_id}/contracts/{contract_id}` | 更新 |
| DELETE | `/api/projects/{project_id}/contracts/{contract_id}` | 刪除 |

### 2.2 合約工項明細

| 方法 | 路徑 | 說明 |
|------|------|------|
| GET | `/api/contracts/{contract_id}/items/` | 列表 |
| POST | `/api/contracts/{contract_id}/items/` | 新增（單筆） |
| PUT | `/api/contracts/{contract_id}/items/{item_id}` | 更新 |
| DELETE | `/api/contracts/{contract_id}/items/{item_id}` | 刪除 |
| POST | `/api/contracts/{contract_id}/items/batch` | 批次匯入預算工項 |

### 2.3 期別計價

| 方法 | 路徑 | 說明 |
|------|------|------|
| GET | `/api/contracts/{contract_id}/issues/` | 列表 |
| POST | `/api/contracts/{contract_id}/issues/` | 新增 |
| GET | `/api/contracts/{contract_id}/issues/{issue_id}` | 單筆 |
| PUT | `/api/contracts/{contract_id}/issues/{issue_id}` | 更新 |
| DELETE | `/api/contracts/{contract_id}/issues/{issue_id}` | 刪除 |
| POST | `/api/contracts/{contract_id}/issues/{issue_id}/submit` | 提交審核 |
| POST | `/api/contracts/{contract_id}/issues/{issue_id}/approve` | 核准 |

### 2.4 期別計價明細

| 方法 | 路徑 | 說明 |
|------|------|------|
| GET | `/api/issues/{issue_id}/items/` | 列表 |
| POST | `/api/issues/{issue_id}/items/` | 新增 |
| PUT | `/api/issues/{issue_id}/items/{item_id}` | 更新 |
| DELETE | `/api/issues/{issue_id}/items/{item_id}` | 刪除 |
| POST | `/api/issues/{issue_id}/items/recalc` | 重算（自動計算金額/進度） |
| POST | `/api/issues/{issue_id}/items/batch-from-contract` | 批次從合約工項導入 |

### 2.5 結算管理

| 方法 | 路徑 | 說明 |
|------|------|------|
| GET | `/api/contracts/{contract_id}/settlements/` | 列表 |
| POST | `/api/contracts/{contract_id}/settlements/` | 新增 |
| GET | `/api/contracts/{contract_id}/settlements/{settlement_id}` | 單筆 |
| PUT | `/api/contracts/{contract_id}/settlements/{settlement_id}` | 更新 |
| DELETE | `/api/contracts/{contract_id}/settlements/{settlement_id}` | 刪除 |
| POST | `/api/contracts/{contract_id}/settlements/{settlement_id}/submit` | 提交 |
| POST | `/api/contracts/{contract_id}/settlements/{settlement_id}/approve` | 核准 |

### 2.6 結算明細

| 方法 | 路徑 | 說明 |
|------|------|------|
| GET | `/api/settlements/{settlement_id}/items/` | 列表 |
| POST | `/api/settlements/{settlement_id}/items/` | 新增 |
| PUT | `/api/settlements/{settlement_id}/items/{item_id}` | 更新 |
| DELETE | `/api/settlements/{settlement_id}/items/{item_id}` | 刪除 |
| POST | `/api/settlements/{settlement_id}/items/recalc` | 重算差異金額 |

### 2.7 終驗管理

| 方法 | 路徑 | 說明 |
|------|------|------|
| GET | `/api/contracts/{contract_id}/acceptances/` | 列表 |
| POST | `/api/contracts/{contract_id}/acceptances/` | 新增 |
| GET | `/api/contracts/{contract_id}/acceptances/{acceptance_id}` | 單筆 |
| PUT | `/api/contracts/{contract_id}/acceptances/{acceptance_id}` | 更新 |
| DELETE | `/api/contracts/{contract_id}/acceptances/{acceptance_id}` | 刪除 |
| POST | `/api/contracts/{contract_id}/acceptances/{acceptance_id}/submit` | 提交 |
| POST | `/api/contracts/{contract_id}/acceptances/{acceptance_id}/approve` | 核准 |

### 2.8 終驗明細

| 方法 | 路徑 | 說明 |
|------|------|------|
| GET | `/api/acceptances/{acceptance_id}/items/` | 列表 |
| POST | `/api/acceptances/{acceptance_id}/items/` | 新增 |
| PUT | `/api/acceptances/{acceptance_id}/items/{item_id}` | 更新 |
| DELETE | `/api/acceptances/{acceptance_id}/items/{item_id}` | 刪除 |
| POST | `/api/acceptances/{acceptance_id}/items/recalc` | 重算 |
| POST | `/api/acceptances/{acceptance_id}/items/batch-from-contract` | 批次從合約工項導入 |

### 2.9 合約狀態變更

| 方法 | 路徑 | 說明 |
|------|------|------|
| POST | `/api/contracts/{contract_id}/close` | 結案（標記為 closed） |
| POST | `/api/contracts/{contract_id}/finalize` | 終驗完成（標記為 finalized） |

---

## 3. 前端頁面與元件樹

### 3.1 路由設計

在 `App.tsx` 中新增以下路由（放在 `/app` 巢狀路由下 `projects/:id` 群組內）：

```
/app/projects/:id/contracts                  → ContractListPage
/app/projects/:id/contracts/new              → ContractDetailPage (新增模式)
/app/projects/:id/contracts/:contractId      → ContractDetailPage (檢視模式)
/app/projects/:id/contracts/:contractId/edit → ContractDetailPage (編輯模式)
/app/projects/:id/contracts/:contractId/issues       → IssueListPage
/app/projects/:id/contracts/:contractId/issues/:issueId → IssueDetailPage
/app/projects/:id/contracts/:contractId/settlements  → SettlementListPage
/app/projects/:id/contracts/:contractId/settlements/:settlementId → SettlementDetailPage
/app/projects/:id/contracts/:contractId/acceptances  → AcceptanceListPage
/app/projects/:id/contracts/:contractId/acceptances/:acceptanceId → AcceptanceDetailPage
```

### 3.2 側邊欄選單擴充

在 `AppLayout.tsx` 的專案功能區塊新增「分包合約」選項：

```typescript
{ key: `contracts-${projectId}`, icon: <FileTextOutlined />, label: '分包合約' },
```

### 3.3 頁面元件樹

```
ContractListPage
  ├── PageHeader (統計卡: 合約數/總金額/進行中/已結案)
  ├── ContractTable (Ant Design Table)
  │   ├── 欄位: 合約編號 / 名稱 / 廠商 / 合約金額 / 狀態 / 簽約日 / 操作
  │   └── 操作按鈕: 檢視 / 編輯 / 刪除
  └── CreateContractModal (Form Modal)
       ├── 合約編號 (自動產出/手動)
       ├── 合約名稱 (required)
       ├── 廠商資訊 (名稱/聯絡人/電話)
       ├── 金額與會計
       ├── 工期 (起訖日)
       └── 備註

ContractDetailPage
  ├── ContractBasicInfo (Descriptions 顯示基本資料)
  ├── ContractItemsSection (AG Grid 可編輯表格)
  │   ├── 欄位: 項次/編號/名稱/單位/數量/單價/金額/備註
  │   ├── 工具列: 新增/刪除/批次匯入預算工項/匯出
  │   └── BatchImportModal (選取預算工項樹狀對話框)
  ├── ContractIssuesSection (Card 內含迷你表格 + 連結到 IssueListPage)
  ├── ContractSettlementsSection (Card 內含迷你表格 + 連結)
  └── ContractAcceptancesSection (Card 內含迷你表格 + 連結)

IssueListPage (期別計價列表)
  ├── PageHeader + 返回按鈕
  ├── IssueTable (Ant Design Table)
  │   └── 欄位: 期別/名稱/日期/本期金額/累計金額/進度/狀態/操作
  └── CreateIssueModal

IssueDetailPage (期別計價明細)
  ├── IssueBasicInfo (Descriptions)
  ├── IssueItemsGrid (AG Grid 可編輯表格)
  │   ├── 欄位: 項次/名稱/單位/合約數量/單價/前期累計/本期完成/累計完成/剩餘/本期金額/累計金額/進度/備註
  │   ├── 內建公式: this_amount = this_completed_qty × unit_price
  │   ├── 內建公式: total_completed_qty = prev_completed_qty + this_completed_qty
  │   ├── 內建公式: progress_rate = total_completed_qty / contract_qty × 100
  │   └── 工具列: 新增/刪除/批次從合約工項導入/重算/提交/核准
  └── 底部摘要: 本期合計 / 累計合計 / 總進度

SettlementListPage (結算列表)
  ├── PageHeader + 返回
  └── SettlementTable + CreateSettlementModal

SettlementDetailPage (結算明細)
  ├── SettlementBasicInfo
  └── SettlementItemsGrid (AG Grid)
       ├── 欄位: 項次/名稱/單位/合約數量/合約單價/合約金額/實際數量/實際單價/實際金額/差異金額/備註
       └── 底部: 合約總計 / 追加合計 / 扣減合計 / 結算總金額

AcceptanceListPage (終驗列表)
  ├── PageHeader + 返回
  └── AcceptanceTable + CreateAcceptanceModal

AcceptanceDetailPage (終驗明細)
  ├── AcceptanceBasicInfo
  └── AcceptanceItemsGrid (AG Grid)
       ├── 欄位: 項次/名稱/單位/合約數量/實際數量/合格數量/不合格數量/備註
       └── 底部: 合格率統計
```

### 3.4 共用元件

| 元件 | 說明 |
|------|------|
| `BudgetItemPicker` | 彈出樹狀預算工項選取視窗（對應原始 `FormSplitCnt_ItemPick.cs`） |
| `StatusBadge` | 狀態標籤元件（草稿/已提交/已核准/進行中/已結案/已終驗） |
| `ContractSelector` | 合約選取下拉元件 |

---

## 4. 實作步驟與檔案清單

### Step 1：資料庫模型 — 新增分包合約相關模型

**修改檔案：**
- `api/models.py` — 新增以下 8 個模型類別：
  - `Contract`
  - `ContractItem`
  - `ContractIssue`
  - `ContractIssueItem`
  - `ContractSettlement`
  - `ContractSettlementItem`
  - `ContractFinalAcceptance`
  - `ContractFinalAcceptanceItem`

**技術細節：**
- 遵循現有模型的 coding style（`Column`、`ForeignKey`、`relationship`、`passive_deletes=True`）
- `Contract.status` 使用字串列舉值：`"active"`, `"closed"`, `"finalized"`
- 所有 `issue` / `settlement` / `acceptance` 的 `status` 使用：`"draft"`, `"submitted"`, `"approved"`
- 日期欄位統一使用 `String(20)` 格式 `YYYY-MM-DD`
- 所有金額欄位使用 `Float`，預設 `0`
- `ContractItem` 需要 `budget_item_id` 可為空（當預算項目被刪除時設為 NULL）

**預計工時：** 1 小時

---

### Step 2：API — 分包合約 CRUD + 工項管理

**修改檔案：**
- `api/index.py` — 新增合約相關路由和輔助函數

**新增 API 端點：**
```
GET    /api/projects/{project_id}/contracts/
POST   /api/projects/{project_id}/contracts/
GET    /api/projects/{project_id}/contracts/{contract_id}
PUT    /api/projects/{project_id}/contracts/{contract_id}
DELETE /api/projects/{project_id}/contracts/{contract_id}
POST   /api/projects/{project_id}/contracts/{contract_id}/close
POST   /api/projects/{project_id}/contracts/{contract_id}/finalize

GET    /api/contracts/{contract_id}/items/
POST   /api/contracts/{contract_id}/items/
PUT    /api/contracts/{contract_id}/items/{item_id}
DELETE /api/contracts/{contract_id}/items/{item_id}
POST   /api/contracts/{contract_id}/items/batch
```

**輔助函數：**
- `_recalc_contract_item(item)` — 計算 amount = qty × unit_price
- `_recalc_contract_amount(db, contract_id)` — 加總所有 items 金額更新合約總額
- `_check_contract_access(db, contract_id, user_id)` — 檢查合約存取權

**技術細節：**
- 所有端點加上 `@require_auth`
- `POST /contracts/` 自動產生 contract_no（格式：`SC-{project_code}-{序號}`）
- 批次匯入 `POST /contracts/{contract_id}/items/batch` 接收 `{budget_item_ids: [int]}` 或 `{include_all_leaf: true}`，從 `budget_items` 複製 W 類型項目的名稱/單位/數量/單價到 `contract_items`
- 刪除合約時 CASCADE 刪除所有子表資料
- `close` 端點將 `contract.status` 設為 `"closed"`
- `finalize` 端點將 `contract.status` 設為 `"finalized"`

**預計工時：** 3 小時

---

### Step 3：API — 期別計價管理

**修改檔案：**
- `api/index.py` — 新增期別計價相關路由

**新增 API 端點：**
```
GET    /api/contracts/{contract_id}/issues/
POST   /api/contracts/{contract_id}/issues/
GET    /api/contracts/{contract_id}/issues/{issue_id}
PUT    /api/contracts/{contract_id}/issues/{issue_id}
DELETE /api/contracts/{contract_id}/issues/{issue_id}
POST   /api/contracts/{contract_id}/issues/{issue_id}/submit
POST   /api/contracts/{contract_id}/issues/{issue_id}/approve

GET    /api/issues/{issue_id}/items/
POST   /api/issues/{issue_id}/items/
PUT    /api/issues/{issue_id}/items/{item_id}
DELETE /api/issues/{issue_id}/items/{item_id}
POST   /api/issues/{issue_id}/items/recalc
POST   /api/issues/{issue_id}/items/batch-from-contract
```

**輔助函數：**
- `_recalc_issue(db, issue_id)` — 重新計算本期所有明細：
  - `this_amount = this_completed_qty × unit_price`
  - `total_completed_qty = prev_completed_qty + this_completed_qty`
  - `remain_qty = contract_qty - total_completed_qty`
  - `progress_rate = total_completed_qty / contract_qty × 100`
  - 加總 issue 所有 items → `issue.total_amount`
  - 查詢前期累計 → `issue.cumulative_amount = prev_issue_total + issue.total_amount`
  - `progress_rate = cumulative_amount / contract_amount × 100`

**技術細節：**
- `POST /issues/` 自動決定 `issue_no`（上期 +1）
- 批次導入 `batch-from-contract` 將 `contract_items` 複製到 `issue_items`，其中 `prev_completed_qty` 取前一期 `total_completed_qty`
- 提交/核准邏輯比照 Invoice 模組（僅草稿可提交，僅已提交可核准）

**預計工時：** 2.5 小時

---

### Step 4：API — 結算與終驗管理

**修改檔案：**
- `api/index.py` — 新增結算與終驗相關路由

**新增 API 端點：**
```
GET    /api/contracts/{contract_id}/settlements/
POST   /api/contracts/{contract_id}/settlements/
GET    /api/contracts/{contract_id}/settlements/{settlement_id}
PUT    /api/contracts/{contract_id}/settlements/{settlement_id}
DELETE /api/contracts/{contract_id}/settlements/{settlement_id}
POST   /api/contracts/{contract_id}/settlements/{settlement_id}/submit
POST   /api/contracts/{contract_id}/settlements/{settlement_id}/approve

GET    /api/settlements/{settlement_id}/items/
POST   /api/settlements/{settlement_id}/items/
PUT    /api/settlements/{settlement_id}/items/{item_id}
DELETE /api/settlements/{settlement_id}/items/{item_id}
POST   /api/settlements/{settlement_id}/items/recalc

GET    /api/contracts/{contract_id}/acceptances/
POST   /api/contracts/{contract_id}/acceptances/
GET    /api/contracts/{contract_id}/acceptances/{acceptance_id}
PUT    /api/contracts/{contract_id}/acceptances/{acceptance_id}
DELETE /api/contracts/{contract_id}/acceptances/{acceptance_id}
POST   /api/contracts/{contract_id}/acceptances/{acceptance_id}/submit
POST   /api/contracts/{contract_id}/acceptances/{acceptance_id}/approve

GET    /api/acceptances/{acceptance_id}/items/
POST   /api/acceptances/{acceptance_id}/items/
PUT    /api/acceptances/{acceptance_id}/items/{item_id}
DELETE /api/acceptances/{acceptance_id}/items/{item_id}
POST   /api/acceptances/{acceptance_id}/items/recalc
POST   /api/acceptances/{acceptance_id}/items/batch-from-contract
```

**輔助函數：**
- `_recalc_settlement(db, settlement_id)` — 計算各明細差異金額並加總：
  - `diff_amount = actual_amount - contract_amount`
  - 結算 total_add_amount = 正 diff 加總
  - 結算 total_deduct_amount = 負 diff 加總
  - 結算 settlement_amount = contract_amount + total_add_amount + total_deduct_amount
- `_recalc_acceptance(db, acceptance_id)` — 計算合格率

**預計工時：** 2.5 小時

---

### Step 5：前端型別與 API 服務層

**新增檔案：**
- `web-pcces/frontend/src/types.ts` — **合併新增型別到既有檔案**

**修改檔案：**
- `web-pcces/frontend/src/types.ts` — 在末端新增以下型別：
  - `Contract`、`ContractItem`
  - `ContractIssue`、`ContractIssueItem`
  - `ContractSettlement`、`ContractSettlementItem`
  - `ContractFinalAcceptance`、`ContractFinalAcceptanceItem`
- `web-pcces/frontend/src/api.ts` — 新增 `contractApi` 物件

**技術細節：**
- `contractApi` 比照 `invoiceApi` 的模組化設計
- 每個子資源群組有各自的命名空間方法

**預計工時：** 1 小時

---

### Step 6：前端 — 合約列表頁面

**新增檔案：**
- `web-pcces/frontend/src/pages/ContractListPage.tsx`

**修改檔案：**
- `web-pcces/frontend/src/App.tsx` — 加入合約路由
- `web-pcces/frontend/src/components/AppLayout.tsx` — 側邊欄加「分包合約」選項

**頁面功能：**
- 顯示專案下所有分包合約的表格
- 合約統計卡片（總數 / 總金額 / 進行中 / 已結案）
- 新增合約 Modal（表單欄位：合約編號、名稱、廠商、金額、工期、備註）
- 編輯、刪除操作
- 每列有操作按鈕導向詳細頁面

**預計工時：** 2 小時

---

### Step 7：前端 — 合約詳細頁面 + 工項編輯

**新增檔案：**
- `web-pcces/frontend/src/pages/ContractDetailPage.tsx`

**頁面功能：**
- 上半部：合約基本資訊展示（Descriptions 元件）
- 中間：工項 AG Grid 可編輯表格
  - 支援行內編輯數量/單價（自動計算金額）
  - 新增工項（直接輸入或從預算工項選取）
  - 刪除工項
  - 批次匯入（彈出 BudgetItemPicker）
- 下半部：三個 Card 分別連到期別計價、結算、終驗列表

**共用元件（新增）：**
- `web-pcces/frontend/src/components/BudgetItemPicker.tsx` — 樹狀預算工項選取對話框

**預計工時：** 3 小時

---

### Step 8：前端 — 期別計價頁面

**新增檔案：**
- `web-pcces/frontend/src/pages/IssueListPage.tsx`
- `web-pcces/frontend/src/pages/IssueDetailPage.tsx`

**IssueListPage 功能：**
- 顯示合約下各期計價列表
- 新增期別 Modal
- 導向期別明細頁面

**IssueDetailPage 功能：**
- 期別基本資訊
- AG Grid 可編輯表格（參照 InvoiceDetailPage 模式）
  - 欄位完整：項次/名稱/單位/合約數量/單價/前期累計/本期完成/累計完成/剩餘/本期金額/累計金額/進度
  - 本期完成數量可編輯 → 自動計算本期金額、累計完成、剩餘、進度
  - 工具列：新增行/刪除行/批次導入/重算/提交/核准
- 底部匯總卡片

**預計工時：** 3 小時

---

### Step 9：前端 — 結算與終驗頁面

**新增檔案：**
- `web-pcces/frontend/src/pages/SettlementListPage.tsx`
- `web-pcces/frontend/src/pages/SettlementDetailPage.tsx`
- `web-pcces/frontend/src/pages/AcceptanceListPage.tsx`
- `web-pcces/frontend/src/pages/AcceptanceDetailPage.tsx`

**SettlementDetailPage 功能：**
- 結算基本資訊 + AG Grid 編輯表格
- 欄位：項次/名稱/單位/合約數量/合約單價/合約金額/實際數量/實際單價/實際金額/差異金額
- 實際數量/單價可編輯 → 自動重算
- 底部顯示合約總計、追加合計、扣減合計、結算總金額

**AcceptanceDetailPage 功能：**
- 終驗基本資訊 + AG Grid 編輯表格
- 欄位：項次/名稱/單位/合約數量/實際數量/合格數量/不合格數量/備註
- 底部顯示合格率

**預計工時：** 4 小時

---

### Step 10：種子資料擴充

**修改檔案：**
- `api/seed_data.py` — 為示範專案新增一筆分包合約（含工項、期別、結算資料）

**新增示範資料：**
- 一筆分包合約：「結構體工程分包合約」
- 3 個合約工項（對應 budget_items 的結構體工項）
- 2 期計價
- 1 筆結算
- 1 筆終驗

**預計工時：** 1 小時

---

### Step 11：整合測試與驗收

**新增檔案：**
- `api/test_contracts.py` — 自動化測試

**測試項目：**
1. 合約 CRUD（新增/讀取/更新/刪除）
2. 工項管理（新增/批次匯入/刪除）
3. 期別計價（建立/明細編輯/重算/提交/核准）
4. 結算（建立/明細編輯/重算）
5. 終驗（建立/明細編輯/重算）
6. 合約狀態變更（close/finalize）
7. 權限檢查（非擁有者無法操作）
8. 資料隔離（不同專案資料不互相干擾）

**預計工時：** 2 小時

---

## 5. 測試計畫

### 5.1 單元測試（後端）

| 測試案例 | 說明 | 預期結果 |
|---------|------|---------|
| 建立合約 | POST 完整資料 | 201 + 回傳合約物件 |
| 建立合約無名稱 | POST 缺少 c_name | 400 錯誤 |
| 查詢合約列表 | GET 列表 | 200 + 合約陣列 |
| 更新合約 | PUT 修改金額 | 200 + 更新後資料 |
| 刪除合約 | DELETE | 200 + 子表也刪除 |
| 刪除不存在合約 | DELETE wrong id | 404 |
| 批次匯入工項 | POST batch with budget_item_ids | 201 + 工項列表 |
| 新增期別 | POST issues | 自動產生 issue_no |
| 期別重算 | POST recalc | 金額/進度正確 |
| 期別提交/核准 | POST submit/approve | 狀態變更 |

### 5.2 整合測試

- 建立合約 → 匯入工項 → 建立兩期計價 → 結算 → 終驗 → 結案
- 驗證金額計算的正確性
- 驗證資料庫約束（FK、CASCADE）

### 5.3 前端測試（手動驗收）

| 測試項目 | 操作步驟 | 預期結果 |
|---------|---------|---------|
| 合約列表 | 進入分包合約頁面 | 顯示示範合約 |
| 新增合約 | 點擊新增 → 填表 → 送出 | 列表新增合約 |
| 工項匯入 | 點擊批次匯入 → 選取工項 → 確認 | 工項出現在表格 |
| 期別計價 | 建立期別 → 編輯完成數量 → 重算 | 金額自動計算 |
| 結算 | 輸入實際數量 → 重算 | 差異金額正確 |
| 終驗 | 輸入合格數量 | 合格率正確 |
| 權限 | 不同使用者登入 | 看不到其他專案資料 |

---

## 6. 預計工時總表

| 步驟 | 內容 | 預計工時 |
|------|------|---------|
| Step 1 | 資料庫模型 | 1 小時 |
| Step 2 | API — 合約 CRUD + 工項管理 | 3 小時 |
| Step 3 | API — 期別計價管理 | 2.5 小時 |
| Step 4 | API — 結算與終驗管理 | 2.5 小時 |
| Step 5 | 前端型別與 API 服務層 | 1 小時 |
| Step 6 | 前端 — 合約列表頁面 | 2 小時 |
| Step 7 | 前端 — 合約詳細頁面 + 工項編輯 | 3 小時 |
| Step 8 | 前端 — 期別計價頁面 | 3 小時 |
| Step 9 | 前端 — 結算與終驗頁面 | 4 小時 |
| Step 10 | 種子資料擴充 | 1 小時 |
| Step 11 | 整合測試與驗收 | 2 小時 |
| **合計** | | **25 小時** |

---

## 7. 技術備註

### 7.1 AG Grid 使用注意事項
- 所有編輯表格使用 AG Grid Community Edition（`AllCommunityModule`）
- 遵循 InvoiceDetailPage.tsx 的 CellValueChanged 模式
- 金額欄位使用 valueFormatter 和 valueParser 確保數字格式
- 編輯完成後自動計算關聯欄位（`onCellValueChanged`）

### 7.2 狀態機流程

```
合約: draft (建立時) → active (正式) → closed (結算完成) → finalized (終驗完成)

期別/結算/終驗: draft → submitted → approved
                    ↻ (退回)
```

### 7.3 與既有模組的整合
- **預算模組（BudgetItem）**：工項選取時讀取 `budget_items` 的 W/L 類型葉節點
- **計價模組（Invoice）**：期別計價可與 Invoice 模組連動（選擇性），本階段先獨立運作
- **資源模組（Resource）**：合約廠商資訊可獨立輸入，不強制對應資源

### 7.4 Vercel 部署注意事項
- 資料庫模型變更需觸發 `init_db()` 中的 `Base.metadata.create_all(engine)`
- SQLite 遷移輔助函數 `_migrate_schema()` 需延伸支援新表格欄位
- 前端 build 產出放在 `api/static/` 下
- 需確認 API 路由不與前端靜態資源路由衝突
