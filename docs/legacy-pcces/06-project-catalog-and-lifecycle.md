# PCCES Legacy Project Catalog and Lifecycle

更新日期：2026-08-02

## 1. 範圍

本文件記錄 C# 桌面版 `FormProject` 專案目錄的已確認行為，作為 Web `ProjectsPage` 與 `/api/projects` 復刻基準。

主要來源：

- `PCCES_CS/Archnowledge.Pcces.PccesMain.Project/FormProject.cs`
- `PCCES_CS/Archnowledge.Pcces.PccesMain.ArchControls/FunctionButtons.cs`
- `PCCES_CS/Archnowledge.Pcces.PccesMain.Budget/FormBudgetProjectPick.cs`

證據等級：

- `CONFIRMED`：由目前已讀 C# 原碼直接確認。
- `REQUIRES_MORE_SOURCE`：類別或入口已確認，但內部方法尚未完整取得。
- `REQUIRES_RUNTIME_TEST`：需執行桌面版確認。

## 2. 專案目錄不是一般 CRUD

`FormProject` 是 PCCES 的中央專案目錄與工作入口，負責：

1. 依目前使用者載入可見專案。
2. 顯示專案是否已有預算、投標或契約資料。
3. 顯示專案權限、模板、最近使用及 PCCES 相容狀態。
4. 建立或匯入新專案。
5. 開啟預算、投標及契約相關工作。
6. 在切換工作前與 `FunctionButtons`、`OnlineList`、授權與工作上下文協作。

因此 Web 版不能只將專案定義為：

```text
id + name + status + CRUD
```

## 3. 載入流程

### 3.1 使用者上下文

`FormProject` 接收：

- `_UserID`
- `_UserName`
- `_ServerName`
- `_HasRegistered`

這些欄位由中央導航在建立或重用表單時注入。

### 3.2 專案資料來源

`GetNewData()`：

```text
PubProject.GetProjectList(F_UserID)
→ DataSet
→ DT1 = ds.Tables[0]
```

可確認專案清單是依 `UserID` 取得，不是無條件列出全部專案。

Web 對應要求：

- 後端必須依登入使用者及專案權限查詢。
- 管理員查看全部專案必須是明確權限，不可只靠前端隱藏。
- API 回傳應包含 Legacy 工作判斷所需欄位。

## 4. 專案篩選

`ProjectFilterEnum`：

```text
All
OnlyTemplate
OnlyAuthorized
```

已確認條件：

| Filter | Legacy 條件 |
|---|---|
| All | `1=1` |
| OnlyTemplate | `IsTemplate='Y'` |
| OnlyAuthorized | `Auth='Y'` |

另有顯示設定：

- `ShowBudType4 == false` 時排除 `BudType='4'`。
- `ShowBidType3 == false` 時排除 `BidType='3'`。

這表示專案清單不只依一般狀態過濾，還包含預算／投標類型顯示政策。

## 5. 專案清單欄位與狀態

目前已由原碼確認或直接引用的欄位包括：

- `projectCode`
- `projectCodeAlias`
- `projCName`
- `projEName`
- `projAddress`
- `mainProj`
- `IsTemplate`
- `Auth`
- `BudType`
- `BidType`
- `IsBud`
- `IsBid`
- `IsCNT`
- `IsCanDelete`
- `BudEst`
- `BudQuote`
- `IsBudEst`
- `IsBudQuote`
- `BudEstAuth`
- `BudQuoteAuth`

部分欄位在 Grid 中隱藏，但仍參與行為判斷。

Web DTO 必須區分：

```text
顯示欄位
操作能力欄位
模組資料存在欄位
權限欄位
模板／類型欄位
```

不可只回傳畫面上可見的名稱與地址。

## 6. 視覺狀態即業務狀態

桌面版為不同狀態建立 Grid Style：

- `NoProjectAuth`
- `RecentBUD`
- `RecentBID`
- `RecentCNT`
- `NotPCCES`
- `TEMPLATE`

並讀取 INI：

- `RecentFile/BUDProject`
- `RecentFile/BIDProject`
- `RecentFile/CNTProject`

顏色本身可現代化，但其語意必須保留：

- 使用者無專案權限。
- 最近使用的預算／投標／契約專案。
- 非 PCCES 相容專案。
- 專案模板。

## 7. 新增與匯入入口

`FunctionButtons.DoProjectCreateImport(bool isCreate)`：

```text
CreateFormProject()
→ FormProject.ExecuteNewProject("0", InitCreateProject: true/false)
```

已確認：

- 建立與匯入共用同一入口方法。
- 以 `InitCreateProject` 區分建立或匯入。
- 第一个參數目前為字串 `"0"`，語意待進一步追蹤。

`ExecuteNewProject` 內部完整流程尚未取得，標記 `REQUIRES_MORE_SOURCE`。

Web 不得先假設：

- 新增只需填名稱。
- 匯入只是上傳單一 Excel。
- 專案代碼可由 Web 自由生成。

## 8. 專案目錄的導航生命週期

`CreateFormProject()` 已確認：

1. 先執行 `IsCanSwitchForm()`。
2. 設定 `FunctionOpenMode.Budget`。
3. 檢查 `F005` 專案目錄權限。
4. 顯示「專案目錄載入中」狀態。
5. 關閉 Owned Forms。
6. 若已有 `FormProject`，刷新使用者上下文並重用。
7. 若沒有則建立新 MDI 表單。
8. 收起主框架 LeftPanel。

Web 對應：

- `GET /projects` 不等於完整專案目錄復刻。
- 需要 Module Launch Gate、`F005` 權限及工作切換保護。
- 重進專案頁時應有明確的狀態重用或刷新規則。

## 9. OnlineList 與工作狀態

`FormProject` 內包含：

- `OnlineList onlineList1`
- `F_FunctionName = "ProjectManagement"`
- `F_PID`
- `F_IsDirectOpenCNT`
- `IsHasLoadedBudget`
- `OpenFormBudget`

可確認專案目錄與線上／工作狀態有連結，但目前尚不能確認是 Presence、專案鎖或多人編輯控制。

狀態：`REQUIRES_MORE_SOURCE`。

## 10. Web 現況差距

目前 Web 專案模組主要有：

- 專案列表。
- 建立、更新、刪除。
- 一般使用者 owner 隔離。
- Dashboard 統計。

尚未證明具備：

- Legacy `projectCode` 身分模型。
- 模板專案。
- Authorized-only 篩選。
- Bud/Bid/CNT 資料存在狀態。
- 各模組的專案權限。
- 可刪除能力判斷。
- 最近使用專案。
- 非 PCCES 專案標記。
- 建立與匯入精靈。
- 專案類型顯示政策。
- 專案工作上下文與鎖。

## 11. 後續必讀

- `FormProject.ExecuteNewProject`
- FormProject ToolClick／刪除／屬性事件
- `formNewProjectWizard`
- `uccShowProject`
- `PubProject.GetProjectList`
- `Project` Domain／BUDClass
- `OnlineList`
- 專案權限資料表與查詢

## 12. 建議永久測試

```text
test_PROJECT_001_list_is_scoped_by_project_authority
test_PROJECT_002_filter_only_templates
test_PROJECT_003_filter_only_authorized
test_PROJECT_004_project_dto_contains_work_capabilities
test_PROJECT_005_disabled_budget_type_is_hidden
test_PROJECT_006_disabled_bid_type_is_hidden
test_PROJECT_007_recent_work_context_is_preserved
test_PROJECT_008_create_and_import_are_distinct_workflows
test_PROJECT_009_delete_requires_server_calculated_capability
test_PROJECT_010_project_code_is_stable_identity
```
