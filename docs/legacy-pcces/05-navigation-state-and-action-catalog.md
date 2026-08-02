# PCCES 主導航狀態與 Action Catalog

更新日期：2026-08-02

## 1. 目的

本文件收口 `FunctionButtons` 中的主導航狀態、模組開放模式、Active Function、表單重用、工作上下文切換及 `PccesFormAction` 已確認用法。它是後續 Web `ModuleLaunchService`、`WorkContextStore`、路由守衛與 Legacy function-code policy 的基準。

可信度標記：

- `CONFIRMED`：可由目前 C# 原始碼直接確認。
- `INFERRED`：由多個事件與表單分支推導，仍需補 enum 定義或執行驗證。
- `REQUIRES_RUNTIME_TEST`：需執行桌面程式確認。

## 2. FunctionOpenMode

`FunctionButtons` 保存 `F_CurrOpenMode`，已確認至少有四種模式：

| Mode | 主功能群 | 子入口 |
|---|---|---|
| `Budget` | 預算 | 專案目錄、預算書編製 |
| `Bid` | 投標 | 投標資料匯入、投標單填寫 |
| `Common` | 共用資料 | 基本資料庫、工程比對、單價分析比對 |
| `Invoice` | 履約 | 契約、估驗、契約變更、結算、驗收 |

切換主功能群時，桌面版先隱藏所有子按鈕，再按模式逐一顯示，並在顯示過程呼叫 `Application.DoEvents()`。Web 不需仿製逐顆按鈕刷新，但必須保留「單一 active mode + 模組開放狀態決定可見入口」的結果。

## 3. OPEN_MODE_CHECK

`OPEN_MODE_CHECK()` 會建立 `ModuleManager`，重新計算：

- 子功能按鈕可見性；
- Budget、Bid、Contract、Common 主功能群可見性；
- 模組流程圖入口是否顯示。

### 3.1 已確認的模組開關

- `EnableBudgetMdoule`
- `EnableBidMdoule`
- `EnableContractModule`
- `EnableCommonMdoule`

### 3.2 需要保留的行為

Web 必須在三處一致套用模組開關：

1. 選單與 Dashboard 入口不可見或不可用；
2. 直接輸入 URL 不得繞過；
3. API 必須拒絕停用模組的請求。

### 3.3 原碼疑點

`OPEN_MODE_CHECK()` 的 Invoice 分支以 `EnableContractModule` 判斷，但對子按鈕賦值時使用 `EnableBudgetMdoule`；Common 分支條件使用 `EnableBidMdoule`，但子按鈕使用 `EnableCommonMdoule`。

此處標記為 `REQUIRES_RUNTIME_TEST`。Web 不得盲目複製疑似反編譯或歷史程式缺陷；應以桌面執行結果與 ModuleManager 真實配置驗證。

## 4. Active Function

`SetActiveFunction()` 已確認以下 active key：

| Active key | 功能 |
|---|---|
| `MRSBASE` | 基本工項資料庫 |
| `PROJECT` | 專案目錄 |
| `BUD` | 預算書編製 |
| `COMPAREITEM` | 工程項目比對 |
| `COMPAREMRS` | 單價分析比對 |
| `BID` | 投標資料／投標單 |
| `SPLIT_CONTRACT` | 契約編製 |
| `BDGT_CHANGE` | 契約變更 |
| `SYSMAINTAIN` | 系統維護 |

桌面版主要用它改變按鈕圖示與背景。Web 可現代化為 active navigation state，但 active key 必須是正式 contract，而非只根據 URL 猜測。

## 5. 已確認 Function Code Catalog

| Function Code | 功能 | C# 入口 |
|---|---|---|
| `F001` | 系統維護 | `BtnFunc1_Click` |
| `F0010007` | 系統維護特定入口 | `linkLabel1_LinkClicked` |
| `F002` | 基本資料庫維護 | `BtnFunc2_Click` |
| `F003` | 預算書編製 | `CreateFormBudgetByBUD` |
| `F004` | 投標單填寫 | `CreateFormBudgetByBID` |
| `F005` | 專案目錄 | `CreateFormProject` |
| `F00500010002` | 投標資料匯入 | `BtnFuncBidImport_Click` |
| `F006` | 系統外掛／Plugin | `BtnFunc13_Click` |
| `F007` | 單價分析／經費審查比對 | `BtnFunc7_Click` |
| `F008` | 歷史工程單位造價／工程項目比對 | `BtnFunc8_Click` |
| `F009` | 契約編製 | `BtnFunc9_Click` |
| `F010` | 估驗記錄 | `BtnFunc10_Click` |
| `F011` | 契約變更 | `BtnFunc6_Click` |
| `F012` | 結算作業 | `BtnFunc11_Click` |

`SubFinal` 的按鈕事件在已讀片段未看到 `ChkAuthority`，而是先呼叫 `OPEN_MODE_CHECK()`。這不代表驗收無權限，可能在 Project Picker、目標表單或其他層執行，現階段標為 `UNKNOWN`。

## 6. 已確認 PccesFormAction Catalog

| Action | 目標功能／表單 | 入口權限 |
|---|---|---|
| `BUD` | 預算編輯 `frmBudget` | `F003` |
| `BID` | 投標單編輯 `frmBudget` | `F004` |
| `SplitContract` | 契約編製 `FormSplitContract` | `F009` |
| `Invoice` | 估驗記錄 `FormInvoice` | `F010` |
| `BudgetChange` | 契約變更 `FormBudgetChange` | `F011` |
| `SubClose` | 結算 `FormSubClose` | `F012` |
| `SubFinal` | 驗收 `FormSubFinal` | `UNKNOWN` |

同一個 `frmBudget` 依 `_ActionName` 區分 BUD 與 BID。Web 不得因共用同一編輯器而混淆兩種業務模式、權限、輸出及資料候選規則。

## 7. 表單與工作上下文重用

### 7.1 專案目錄

`CreateFormProject()`：

1. 執行切換檢查；
2. 設定 Budget mode；
3. 檢查 `F005`；
4. 顯示載入提示；
5. 關閉 owned child dialogs；
6. 掃描 MDI 是否已有 `FormProject`；
7. 已有則更新 UserID、UserName、ServerName、HasRegistered 並重用；
8. 沒有才建立；
9. 收起 LeftPanel。

### 7.2 預算與投標

`CreateFormBudgetByBUD/BID()`：

- 若目前已在 `frmBudget`，先依 `_ActionName` 判斷是否同模式；
- 跨 BUD/BID 模式時必須通過切換閘門；
- 分別檢查 `F003` / `F004`；
- 經 `FormBudgetProjectPick` 選擇專案；
- 同 Action 的既有 editor 可直接重用。

Web 應建立：

```text
WorkContextKey = action + projectId + revision/version
```

並在相同 key 重入時重用；不同 action 或不同 project 切換時先走 dirty-state gate。

## 8. HideAllChild 與導航清理

`HideAllChild()` 只關閉 `ParentForm.OwnedForms`。各功能事件另會掃描 MDI children，保留三種首頁面板及目標表單，關閉其他業務表單。

因此 Web 需要區分：

- modal/owned dialog；
- primary work context；
- persistent shell/dashboard；
- target context reuse。

不能用單一 `navigate()` 取代全部生命週期語意。

## 9. 導航鎖與不可重入

已確認的機制包括：

- `lock(this)`：部分模組避免重入；
- 暫停 ParentForm 或 FunctionButtons；
- Wait Cursor；
- `FormSys_G_Info1` loading modal；
- `DisableButtons()` / `EnableButtons()`；
- 切換前 `IsCanSwitchForm()`。

`IsCanSwitchForm()` 的完整方法內容尚未在本批可見片段中取得，因此其具體判斷條件保持 `UNKNOWN`，不得自行補寫成事實。可以確認的是，多數業務入口以其回傳值作為切換前置閘門。

## 10. Web 正式契約建議

### ModuleLaunchRequest

```text
action
projectId?
sourceContextId?
requestedRoute
```

### ModuleLaunchDecision

```text
moduleEnabled
functionAuthorized
projectEligible
canLeaveCurrentContext
reuseContextId?
denialCode?
denialMessage?
```

### ActiveWorkContext

```text
contextId
action
projectId
projectCode
revisionOrVersion
status
dirty
busy
openedAt
lastActiveAt
```

## 11. 必要永久測試

```text
test_AUTHZ_F003_budget_editor_requires_function_code
test_AUTHZ_F004_bid_editor_requires_function_code
test_AUTHZ_F005_project_catalog_requires_function_code
test_AUTHZ_F006_plugin_requires_function_code
test_AUTHZ_F012_settlement_requires_function_code
test_NAV_ACTION_same_budget_context_is_reused
test_NAV_ACTION_budget_to_bid_requires_leave_gate
test_NAV_ACTION_disabled_module_rejects_direct_route
test_NAV_ACTION_active_key_matches_launched_module
test_NAV_ACTION_owned_dialogs_close_before_context_switch
```

## 12. 下一步

主導航與 Action Catalog 已具備實作基準。下一個 Legacy Segment 應進入：

1. `FormProject` 專案目錄完整 CRUD、匯入、刪除及屬性；
2. 專案狀態與 BUD/BID/Contract 候選規則；
3. `FormBudgetProjectPick` 的查詢、搜尋、確認、刪除與各 Action 建表分支；
4. 再進入 `FormBudgetEditMain` 與 BDGT Components。
