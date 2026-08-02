# PCCES Legacy 專案選取與工作上下文生命週期

更新日期：2026-08-02

## 1. 文件目的

本文件整理桌面版在進入契約編製、估驗記錄、契約變更及其他履約功能前，如何透過 `FormBudgetProjectPick` 選取專案、建立工作上下文、控制表單切換並回傳使用者操作結果。

## 2. 核心角色

`FormBudgetProjectPick` 不是一般專案清單，而是多種業務動作共用的「工作入口選擇器」。它接收：

- `PccesFormAction ActionName`
- `UserID`
- `HasRegistered`
- `CurrentEditProjectCode`
- `CallUpType`
- `Mode`
- `IsAddOn`

並產生：

- 選取的 Project Code。
- 對應 Action 的後續表單。
- `DialogResult`，讓呼叫端判斷取消或成功切換。

## 3. 已確認資料欄位與清單行為

專案選取表格至少顯示：

| 欄位 | 說明 |
|---|---|
| `IsData` | 是否為有效資料列／圖示欄 |
| `ProjectCode` | 專案代碼 |
| `projCName` | 中文專案名稱 |
| `projAddress` | 工程地址 |
| `projEName` | 英文名稱，畫面隱藏 |

已確認 UI 行為：

- 清單不可直接編輯。
- 採 ListBox 選取模式。
- 支援搜尋條件 `cbFind`。
- 支援滑鼠移動與點擊。
- 有確定與取消。
- 顯示目前使用中的資料庫描述。
- 右鍵工具列包含刪除入口，但刪除限制仍待讀取。

## 4. 共用啟動流程

從 `FunctionButtons` 進入履約模組時，流程如下：

```text
功能點擊
→ IsCanSwitchForm()
→ 驗證 Function Code
→ 暫時停用來源表單或功能面板
→ 顯示等待游標
→ HideAllChild()
→ 關閉非首頁、非目標功能的 MDI 子表單
→ 建立 FormBudgetProjectPick
→ 設定 ActionName、UserID、HasRegistered
→ ShowDialog
→ 取消：恢復原表單
→ 成功：保留或建立目標功能表單
→ Dispose 選取器
→ 恢復游標
```

## 5. Action 對應

目前已直接確認：

| `PccesFormAction` | Legacy 功能 | 權限碼 | 目標表單 | Web 對應 |
|---|---|---|---|---|
| `SplitContract` | 契約編製 | `F009` | `FormSplitContract` | `ContractListPage` / `ContractDetailPage` |
| `Invoice` | 估驗記錄 | `F010` | `FormInvoice` | `InvoiceListPage` / `InvoiceDetailPage` |
| `BudgetChange` | 契約變更 | `F011` | `FormBudgetChange` | 目前分散於 Contract／Issue 流程 |

`SubClose`、`SubFinal` 等動作從引用關係可確認存在，但完整 Action 分支、條件及權限碼尚待後續讀取，因此本批不將其列為完整確認。

## 6. 取消與成功的差異

當選取器由既有功能表單中開啟時：

- 若 `ShowDialog` 回傳 `Cancel`，原表單重新啟用。
- 若成功，呼叫端會關閉除首頁與目標表單以外的 MDI 子表單。
- 若目前已在目標表單，則直接重新顯示並 BringToFront，而不是重建。

這代表桌面版保留「單一主要工作上下文」，不允許多個互相衝突的履約作業同時存在。

## 7. 專案上下文不是 URL 參數而已

桌面版專案上下文至少包含：

```text
ProjectCode
ActionName
UserID
HasRegistered
CurrentEditProjectCode
CallUpType
Mode
IsAddOn
Current MDI Form
Source Form Enabled State
```

Web 版目前多以 `/projects/:id/...` 表示專案，但仍缺：

- Action 型別。
- 工作上下文版本。
- 來源頁面與取消回復語意。
- 同一專案同一功能的單一實例／鎖定策略。
- 切換前 dirty-state／交易狀態檢查。
- Registration／module entitlement。
- Legacy ProjectCode 與 Web numeric id 的穩定映射。

## 8. Web 復刻建議契約

### 8.1 Work Context

建議建立正式物件：

```text
WorkContext
- context_id
- user_id
- project_id
- legacy_project_code
- action
- module
- source_route
- target_route
- state
- version
- created_at
- last_heartbeat_at
```

狀態至少包含：

```text
SELECTING
ACTIVE
DIRTY
SUBMITTING
BLOCKED
CLOSED
```

### 8.2 Action Catalog

Web 應保留 Legacy Action 名稱作為穩定對照：

```text
SPLIT_CONTRACT
INVOICE
BUDGET_CHANGE
SUB_CLOSE
SUB_FINAL
```

每個 Action 需定義：

- 必要 Function Code。
- 允許的專案狀態。
- 是否要求已註冊。
- 目標路由。
- 可否重用既有上下文。
- 取消時返回位置。
- 離開前驗證。

### 8.3 專案選取 API

單純 `GET /projects` 不足以還原 Legacy 行為。需要至少：

```text
GET  /api/work-actions/{action}/eligible-projects
POST /api/work-contexts
POST /api/work-contexts/{id}/activate
POST /api/work-contexts/{id}/close
GET  /api/work-contexts/current
```

`eligible-projects` 必須綜合：

- 使用者權限。
- 模組授權。
- 專案狀態。
- Action 前置條件。
- 是否已有衝突中的工作上下文。

## 9. Legacy 相容驗收案例

後續實作至少要有下列永久測試：

```text
test_NAV_CTX_001_contract_requires_project_selection
test_NAV_CTX_002_invoice_cancel_restores_source_context
test_NAV_CTX_003_existing_target_context_is_reused
test_NAV_CTX_004_unrelated_active_context_is_closed_or_blocked
test_NAV_CTX_005_action_filters_ineligible_projects
test_NAV_CTX_006_project_code_mapping_is_stable
test_NAV_CTX_007_permission_is_rechecked_server_side
```

## 10. 尚待讀取

- `FormBudgetProjectPick` 載入、搜尋、刪除、雙擊與確認事件完整分支。
- `PccesFormAction` 完整 enum。
- `FormBudget_PickType` 完整 enum。
- 各 Action 建立目標表單時注入的全部欄位。
- `IsCanSwitchForm()` 的真正阻擋條件。
- `HideAllChild()` 的例外表單清單。
- 預算編輯與履約模組的互斥狀態。

在上述來源尚未完成前，本文件狀態為 `CONFIRMED-PARTIAL`，可作為 Web 架構設計基準，但不可宣告所有專案選取規則已完整還原。
