# PCCES 功能權限與工作上下文追蹤矩陣

更新日期：2026-08-02

本文件補充 `17-feature-traceability-matrix.md`，集中追蹤功能代碼、專案選取與工作上下文。待主矩陣下一次整併時再合併。

## 1. 功能代碼與 Web 對照

| Feature ID | Legacy 功能 | C# 來源 | Function Code | Web 對應 | 狀態 | 缺口 |
|---|---|---|---|---|---|---|
| AUTHZ-CAT-001 | 功能碼驅動權限 | `DBClass.ChkAuthority` 呼叫點 | 多組 | Admin/JWT role | `PARTIAL` | 缺正式 Function Catalog 與 API policy |
| AUTHZ-CAT-002 | 權限拒絕顯示功能名稱 | `DBClass.GetFuncName` 呼叫點 | 多組 | 通用 403 | `PARTIAL` | 缺 function code、名稱與拒絕原因 |
| AUTHZ-F002 | 基本資料庫維護 | `BtnFunc2_Click` | `F002` | `MrsBasePage` | `NOT_STARTED` | 無 Legacy function-code policy |
| AUTHZ-F00500010002 | 投標資料匯入 | `BtnFuncBidImport_Click` | `F00500010002` | 無完整流程 | `NOT_STARTED` | 缺匯入精靈與權限 |
| AUTHZ-F007 | 經費審查比對 | `BtnFunc7_Click` | `F007` | `MrsBasePriceComparePage` | `NOT_STARTED` | 無細粒度權限 |
| AUTHZ-F008 | 歷史工程單位造價 | `BtnFunc8_Click` | `F008` | `ComparePage` | `NOT_STARTED` | 無細粒度權限 |
| AUTHZ-F009 | 契約編製 | `BtnFunc9_Click` | `F009` | `ContractListPage` | `NOT_STARTED` | 無 action/project eligibility gate |
| AUTHZ-F010 | 估驗記錄 | `BtnFunc10_Click` | `F010` | `InvoiceListPage` | `NOT_STARTED` | 無 action/project eligibility gate |
| AUTHZ-F011 | 契約變更 | `BtnFunc6_Click` | `F011` | Contract/Issue pages | `NOT_STARTED` | Web 邊界與 Legacy 不一致 |
| AUTHZ-F0010007 | 系統維護指定入口 | `linkLabel1_LinkClicked` | `F0010007` | `AdminPage` | `NOT_STARTED` | 缺指定子頁權限與深連結 |

## 2. 模組授權

| Feature ID | Legacy 行為 | C# 來源 | Web 狀態 | 缺口 |
|---|---|---|---|---|
| MODULE-001 | 預算模組啟用控制 | `ModuleManager.EnableBudgetMdoule` | `NOT_STARTED` | 缺部署／授權層 module entitlement |
| MODULE-002 | 契約模組啟用控制 | `ModuleManager.EnableContractModule` | `NOT_STARTED` | 缺部署／授權層 module entitlement |
| MODULE-003 | 投標模組啟用控制 | `ModuleManager.EnableBidMdoule` | `NOT_STARTED` | 缺部署／授權層 module entitlement |
| MODULE-004 | 共用模組啟用控制 | `ModuleManager.EnableCommonMdoule` | `NOT_STARTED` | 缺部署／授權層 module entitlement |

## 3. 專案選取與工作上下文

| Feature ID | Legacy 行為 | C# 來源 | Web 對應 | 狀態 | 缺口 |
|---|---|---|---|---|---|
| NAV-CTX-001 | 履約功能先選取專案 | `FormBudgetProjectPick` | 專案路由 | `PARTIAL` | URL 參數不能取代 eligibility 選取器 |
| NAV-CTX-002 | Action 驅動候選專案與目標表單 | `_ActionName` / `PccesFormAction` | 無正式 Action Catalog | `NOT_STARTED` | 缺 Action 與前置條件契約 |
| NAV-CTX-003 | 契約編製 Action | `SplitContract` | Contract routes | `PARTIAL` | 缺 F009 與有效專案篩選 |
| NAV-CTX-004 | 估驗 Action | `Invoice` | Invoice routes | `PARTIAL` | 缺 F010、期別前置條件與取消恢復 |
| NAV-CTX-005 | 契約變更 Action | `BudgetChange` | Contract/Issue routes | `UI_ONLY` | Web 功能拆分與 Legacy Action 不一致 |
| NAV-CTX-006 | 取消選取恢復來源表單 | `ShowDialog == Cancel` 分支 | Browser back | `NOT_STARTED` | 缺來源上下文保存與恢復 |
| NAV-CTX-007 | 已存在目標表單則重用 | type scan / Show / BringToFront | React route remount | `NOT_STARTED` | 缺單一工作實例策略 |
| NAV-CTX-008 | 關閉非首頁、非目標表單 | MDI cleanup | Router outlet | `PARTIAL` | 缺 active work-context 規則 |
| NAV-CTX-009 | 注入 UserID | `_UserID` | JWT | `PARTIAL` | 缺 action-scoped context snapshot |
| NAV-CTX-010 | 注入 HasRegistered | `_HasRegistered` | 無 | `NOT_STARTED` | 缺 registration entitlement |
| NAV-CTX-011 | Legacy ProjectCode | `_SelectedProjectCode` | numeric `projectId` | `PARTIAL` | 缺穩定 ProjectCode 映射 |
| NAV-CTX-012 | 顯示目前資料庫 | `lblUseDatabase` | 無 | `NOT_STARTED` | 多資料庫／租戶上下文不明 |
| NAV-CTX-013 | 搜尋候選專案 | `cbFind` | Projects search | `PARTIAL` | 未按 Action eligibility 搜尋 |
| NAV-CTX-014 | 專案選取清單唯讀 | `GridBudget.AllowEditing=false` | Projects table | `PARTIAL` | 選取器與管理頁尚未分離 |

## 4. 必要永久測試

```text
test_AUTHZ_CAT_001_api_rejects_missing_legacy_function_code
test_AUTHZ_CAT_002_denial_returns_function_name_and_code
test_MODULE_001_disabled_module_is_not_routable
test_NAV_CTX_001_contract_requires_eligible_project
test_NAV_CTX_002_action_filters_candidate_projects
test_NAV_CTX_003_cancel_restores_source_context
test_NAV_CTX_004_existing_target_context_is_reused
test_NAV_CTX_005_unrelated_context_is_closed_or_blocked
test_NAV_CTX_006_project_code_mapping_is_stable
test_NAV_CTX_007_registration_entitlement_is_enforced
```

## 5. 狀態結論

此批已完成 Legacy 行為文件化，但 Web 尚未進入實作，因此所有權限代碼與工作上下文項目最高只能標為 `PARTIAL`，不得宣告 `LEGACY_MATCHED`。
