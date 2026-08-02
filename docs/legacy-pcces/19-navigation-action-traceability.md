# LEGACY-002 導航狀態與 Action 追蹤矩陣

更新日期：2026-08-02

## 狀態

- `NOT_STARTED`
- `UI_ONLY`
- `PARTIAL`
- `IMPLEMENTED`
- `LEGACY_MATCHED`
- `VERIFIED`

## Function Mode 與 Module Gate

| Feature ID | Legacy 行為 | C# 來源 | 證據 | Web 現況 | 狀態 | 缺口 |
|---|---|---|---|---|---|---|
| NAV-MODE-001 | 單一 `FunctionOpenMode` 控制 Budget/Bid/Common/Invoice 群組 | `FunctionButtons.F_CurrOpenMode` | `CONFIRMED` | 側邊路由 | `UI_ONLY` | 缺正式 mode state 與群組契約 |
| NAV-MODE-002 | 模式切換先隱藏全部子入口再重新顯示 | `HideAllButton`, `BtnMain*` | `CONFIRMED` | 靜態選單 | `NOT_STARTED` | 缺配置驅動入口 |
| NAV-MODE-003 | `ModuleManager` 控制四類模組可見性 | `OPEN_MODE_CHECK` | `CONFIRMED` | 未定位 | `NOT_STARTED` | 前端、路由與 API 均需套用 |
| NAV-MODE-004 | Module Flow 依模組啟用狀態顯示 | `OPEN_MODE_CHECK` | `CONFIRMED` | 未定位 | `NOT_STARTED` | 缺流程圖／快速工作入口 |
| NAV-MODE-005 | Invoice/Common 開關原碼存在疑點 | `OPEN_MODE_CHECK` | `REQUIRES_RUNTIME_TEST` | 無 | `NOT_STARTED` | 需桌面執行確認，不可盲目複製 |

## Active Function

| Feature ID | Active key | 功能 | Web 現況 | 狀態 | 缺口 |
|---|---|---|---|---|---|
| NAV-ACTIVE-001 | `MRSBASE` | 基本資料庫 | `MrsBasePage` | `PARTIAL` | 缺正式 active key |
| NAV-ACTIVE-002 | `PROJECT` | 專案目錄 | `ProjectsPage` | `PARTIAL` | 缺工作上下文狀態 |
| NAV-ACTIVE-003 | `BUD` | 預算編製 | `BudgetEditorPage` | `PARTIAL` | 缺 action mode、版本與 dirty state |
| NAV-ACTIVE-004 | `BID` | 投標單 | 未完整定位 | `NOT_STARTED` | 與 BUD 共用 editor 但業務模式不同 |
| NAV-ACTIVE-005 | `COMPAREITEM` | 工程項目比對 | `ComparePage` | `PARTIAL` | 缺 Legacy 候選與精度規則 |
| NAV-ACTIVE-006 | `COMPAREMRS` | 單價分析比對 | `MrsBasePriceComparePage` | `PARTIAL` | 缺比對方式契約 |
| NAV-ACTIVE-007 | `SPLIT_CONTRACT` | 契約編製 | Contract routes | `PARTIAL` | 缺 project picker 與 work context |
| NAV-ACTIVE-008 | `BDGT_CHANGE` | 契約變更 | Contract issue/change pages | `PARTIAL` | 尚未證明語意一致 |
| NAV-ACTIVE-009 | `SYSMAINTAIN` | 系統維護 | `AdminPage` | `UI_ONLY` | 缺 Legacy tabs 與深連結 |

## Function Code 新增確認

| Feature ID | Function Code | 功能 | C# 來源 | Web 狀態 | 缺口 |
|---|---|---|---|---|---|
| AUTHZ-F001 | `F001` | 系統維護 | `BtnFunc1_Click` | `NOT_STARTED` | 目前 admin role 不能替代 function code |
| AUTHZ-F003 | `F003` | 預算書編製 | `CreateFormBudgetByBUD` | `NOT_STARTED` | API 與 UI 均無對應 policy |
| AUTHZ-F004 | `F004` | 投標單填寫 | `CreateFormBudgetByBID` | `NOT_STARTED` | 缺 BID mode 與輸出權限 |
| AUTHZ-F005 | `F005` | 專案目錄 | `CreateFormProject` | `NOT_STARTED` | 專案 CRUD 尚未 function-code 化 |
| AUTHZ-F006 | `F006` | 系統外掛 | `BtnFunc13_Click` | `NOT_STARTED` | 無對應模組 |
| AUTHZ-F012 | `F012` | 結算 | `BtnFunc11_Click` | `NOT_STARTED` | Settlement routes 尚無 Legacy policy |
| AUTHZ-SUBFINAL | 未在已讀入口看到 | 驗收 | `BtnFunc12_Click` | `UNKNOWN` | 必須繼續追蹤 Project Picker 或目標表單權限 |

## Action Catalog

| Feature ID | Action | 目標 | 權限 | Web 狀態 | 主要缺口 |
|---|---|---|---|---|---|
| ACTION-BUD | `BUD` | `frmBudget` 預算模式 | `F003` | `PARTIAL` | 缺 mode、版本、離開閘門 |
| ACTION-BID | `BID` | `frmBudget` 投標模式 | `F004` | `NOT_STARTED` | 不可與預算模式混同 |
| ACTION-CONTRACT | `SplitContract` | `FormSplitContract` | `F009` | `PARTIAL` | 缺 eligible project query |
| ACTION-INVOICE | `Invoice` | `FormInvoice` | `F010` | `PARTIAL` | 缺選取器、取消恢復與 context reuse |
| ACTION-CHANGE | `BudgetChange` | `FormBudgetChange` | `F011` | `PARTIAL` | 缺變更版次與候選條件 |
| ACTION-CLOSE | `SubClose` | `FormSubClose` | `F012` | `PARTIAL` | 缺結算前置狀態機 |
| ACTION-FINAL | `SubFinal` | `FormSubFinal` | `UNKNOWN` | `PARTIAL` | 缺驗收前置條件與權限來源 |

## Context Lifecycle

| Feature ID | Legacy 行為 | C# 來源 | Web 狀態 | 缺口 |
|---|---|---|---|---|
| CTX-001 | 同類表單存在時更新上下文並重用 | `CreateFormProject` 等 | `NOT_STARTED` | React route 重入語意未定義 |
| CTX-002 | BUD/BID 共用 editor，以 `_ActionName` 區分 | `CreateFormBudgetByBUD/BID` | `NOT_STARTED` | 需 action-aware editor contract |
| CTX-003 | 跨 BUD/BID 前執行切換閘門 | 同上 | `NOT_STARTED` | 缺 dirty-state confirmation |
| CTX-004 | Owned dialogs 在切換前關閉 | `HideAllChild` | `NOT_STARTED` | Modal lifecycle 未集中管理 |
| CTX-005 | MDI children 僅保留 shell 與目標表單 | 各 `BtnFunc*` | `NOT_STARTED` | 缺單一 primary work context |
| CTX-006 | 開啟時注入 UserID/UserName/ServerName/HasRegistered | 各建立／重用分支 | `PARTIAL` | JWT 未包含資料庫、授權與註冊 context |
| CTX-007 | 導航期間可 Disable 全部功能按鈕 | `DisableButtons/EnableButtons` | `NOT_STARTED` | 缺全域 navigation lock |
| CTX-008 | `IsCanSwitchForm` 是多數入口的前置閘門 | 多個入口 | `UNKNOWN` | 方法細節尚未取得，不得假設條件 |

## 後續實作門檻

以下條件完成前，不得把任何現有 Web 模組標為 `LEGACY_MATCHED`：

1. function code catalog 可由後端查詢並在 API 執行；
2. module enablement 同時約束 UI、route、API；
3. project/action eligibility 有正式 query；
4. active work context 可識別 action + project + version；
5. dirty/busy 狀態可阻擋導航；
6. BUD 與 BID 即使共用元件也有獨立 mode contract；
7. 結算與驗收的完整前置狀態、權限及資料候選規則完成源碼追蹤。
