# PCCES Legacy Project Action Eligibility

更新日期：2026-08-02

## 1. 目的

本文件定義桌面版「哪些專案可以進入哪些工作 Action」的已確認基礎，作為 Web 專案入口、契約、估驗、變更、結算與驗收流程的 Eligibility Contract。

## 2. 已確認原則

專案是否可進入某個功能，不只取決於專案是否存在。

桌面版至少同時考慮：

1. 模組是否啟用。
2. 使用者是否具備 Legacy Function Code。
3. 使用者是否具備該專案權限。
4. 專案是否具備該 Action 所需前置資料。
5. 專案目前是否已有預算、投標或契約資料。
6. 專案類型、模板與特殊 Bud/Bid Type 是否允許顯示。
7. 是否已有衝突中的工作上下文。

## 3. 專案能力欄位

由 `FormProject` 可確認以下欄位與工作能力直接相關：

```text
IsBud
IsBid
IsCNT
IsCanDelete
BudEst
BudQuote
IsBudEst
IsBudQuote
BudEstAuth
BudQuoteAuth
Auth
IsTemplate
BudType
BidType
```

這些欄位不應被當成純 UI 欄位，而應形成後端計算的 capability contract。

建議 Web DTO：

```json
{
  "project_id": 123,
  "project_code": "...",
  "capabilities": {
    "authorized": true,
    "can_delete": false,
    "has_budget": true,
    "has_bid": false,
    "has_contract": true,
    "can_open_budget": true,
    "can_open_bid": false,
    "can_open_contract": true,
    "can_open_invoice": true,
    "can_open_change": true,
    "can_open_close": false,
    "can_open_final": false
  }
}
```

以上 JSON 是 Web 設計建議，不是 Legacy 原碼格式。

## 4. 已確認 Action 與前置權限

| Action | 功能權限 | 專案選取器 | 已確認目標 |
|---|---|---|---|
| Project Catalog | `F005` | 否 | `FormProject` |
| BUD | `F003` | 是 | `frmBudget` |
| BID | `F004` | 是 | `frmBudget` |
| SplitContract | `F009` | 是 | `FormSplitContract` |
| Invoice | `F010` | 是 | `FormInvoice` |
| BudgetChange | `F011` | 是 | `FormBudgetChange` |
| SubClose | `F012` | 是 | `FormSubClose` |
| SubFinal | 尚待確認 | 是 | `FormSubFinal` |

## 5. BUD 與 BID

BUD 與 BID 共用 `frmBudget`，但以 `_ActionName` 區分。

### BUD

```text
F003
→ FormBudgetProjectPick.ActionName = BUD
→ frmBudget.ActionName = BUD
```

### BID

```text
F004
→ FormBudgetProjectPick.ActionName = BID
→ frmBudget.ActionName = BID
```

因此 Eligibility 必須按 Action 分開計算，不能只用 `has_budget_editor_access`。

## 6. 履約 Action

契約、估驗、變更、結算與驗收都透過 `FormBudgetProjectPick` 選取專案。

確認流程：

```text
Function Code Gate
→ IsCanSwitchForm
→ 關閉衝突工作
→ FormBudgetProjectPick(ActionName)
→ 使用者選取符合條件專案
→ 建立或重用目標工作表單
```

目前仍需讀取 `FormBudgetProjectPick` 後半部與各 Domain 查詢，才能確認每個 Action 的精確 SQL／DataView 條件。

## 7. 取消與恢復

當選取器由其他工作表單開啟時：

- `DialogResult.Cancel` 會重新啟用原表單。
- 成功選取後才關閉其他非目標工作表單。

Web 必須保留此交易性語意：

```text
先驗證新上下文可建立
→ 成功後才替換舊上下文
```

不能在使用者尚未完成選取前就清空原頁面未保存狀態。

## 8. 刪除 Eligibility

`FormProject` 清單中存在 `IsCanDelete`。

可確認：

- 刪除能力是後端／Domain 計算結果。
- UI 不應只依角色或 owner 判斷。

精確刪除阻擋條件尚待讀取刪除事件與 Domain 方法。

狀態：`REQUIRES_MORE_SOURCE`。

## 9. Template 與特殊類型

專案清單支援：

- OnlyTemplate。
- 排除 `BudType=4`。
- 排除 `BidType=3`。

因此 Action Eligibility 可能與模板及特殊類型有關。

在未讀完來源前：

- 不將模板當一般正式專案。
- 不將 BudType/BidType 視為無關欄位。
- 不自行推導 Type 3／4 的業務名稱。

## 10. Web API 建議

應新增或形成等價能力：

```text
GET /api/projects?action=BUD
GET /api/projects?action=BID
GET /api/projects?action=SplitContract
GET /api/projects?action=Invoice
GET /api/projects?action=BudgetChange
GET /api/projects?action=SubClose
GET /api/projects?action=SubFinal
```

或：

```text
GET /api/actions/{action}/eligible-projects
```

後端必須同時驗證：

- module enabled
- function code
- project authority
- project capability
- lifecycle prerequisites

前端列表只作展示，不是安全邊界。

## 11. Feature IDs

| Feature ID | 規格 |
|---|---|
| PROJECT-ELIG-001 | 專案 Action Eligibility 由後端計算 |
| PROJECT-ELIG-002 | BUD 與 BID 分開判斷 |
| PROJECT-ELIG-003 | 履約 Action 需專案選取 |
| PROJECT-ELIG-004 | 使用者專案權限是必要條件 |
| PROJECT-ELIG-005 | 模組開關是必要條件 |
| PROJECT-ELIG-006 | Function Code 是必要條件 |
| PROJECT-ELIG-007 | 取消選取不破壞原工作上下文 |
| PROJECT-ELIG-008 | 刪除能力由 Domain 回傳 |
| PROJECT-ELIG-009 | Template 與正式專案分流 |
| PROJECT-ELIG-010 | BudType/BidType 顯示政策保留 |

## 12. 待確認

- 每個 `PccesFormAction` 的精確候選條件。
- `IsCanDelete` 的完整算法。
- `IsBud/IsBid/IsCNT` 與各 Action 的映射。
- `BudEst/BudQuote` 及 Auth 欄位語意。
- `SubFinal` 的 Function Code。
- 非 PCCES 專案的阻擋／轉換流程。

## 13. 永久測試建議

```text
test_PROJECT_ELIG_001_server_computes_action_eligibility
test_PROJECT_ELIG_002_budget_and_bid_are_distinct
test_PROJECT_ELIG_003_invoice_requires_contract_prerequisite
test_PROJECT_ELIG_004_project_authority_cannot_be_bypassed
test_PROJECT_ELIG_005_disabled_module_returns_forbidden
test_PROJECT_ELIG_006_missing_function_code_returns_forbidden
test_PROJECT_ELIG_007_cancel_preserves_existing_context
test_PROJECT_ELIG_008_delete_uses_domain_capability
test_PROJECT_ELIG_009_template_not_treated_as_live_project
test_PROJECT_ELIG_010_hidden_legacy_types_are_excluded
```
