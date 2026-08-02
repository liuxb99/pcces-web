# PCCES Project Lifecycle Traceability

更新日期：2026-08-02

## Project Catalog

| Feature ID | Legacy 行為 | C# 來源 | Web 現況 | 狀態 | 缺口 |
|---|---|---|---|---|---|
| PROJECT-001 | 依 UserID 載入專案清單 | `FormProject.GetNewData` / `PubProject.GetProjectList` | owner/admin 查詢 | `PARTIAL` | 尚未證明等價於 Legacy 專案權限 |
| PROJECT-002 | All/Template/Authorized 篩選 | `ProjectFilterEnum` / `BindDataToGrid` | 一般列表 | `NOT_STARTED` | 缺模板及授權篩選 |
| PROJECT-003 | 隱藏特殊 BudType 4 | `ShowProject.ShowBudType4` | 無 | `NOT_STARTED` | 缺顯示政策 |
| PROJECT-004 | 隱藏特殊 BidType 3 | `ShowProject.ShowBidType3` | 無 | `NOT_STARTED` | 缺顯示政策 |
| PROJECT-005 | 顯示 BUD/BID/CNT 存在狀態 | `IsBud/IsBid/IsCNT` | 部分統計 | `NOT_STARTED` | 缺逐專案能力欄位 |
| PROJECT-006 | 顯示模板狀態 | `IsTemplate` / TEMPLATE style | 無 | `NOT_STARTED` | 缺模板模型 |
| PROJECT-007 | 顯示無專案權限狀態 | `Auth` / NoProjectAuth style | 隱藏非 owner | `PARTIAL` | 缺可見但不可操作語意及細粒度權限 |
| PROJECT-008 | 最近 BUD/BID/CNT 專案 | INI RecentFile | 無 | `NOT_STARTED` | 缺最近工作上下文 |
| PROJECT-009 | 非 PCCES 專案標記 | NotPCCES style | 無 | `NOT_STARTED` | 缺相容性狀態 |
| PROJECT-010 | Domain 計算 IsCanDelete | hidden Grid field | owner/admin delete | `PARTIAL` | 目前刪除能力過度簡化 |
| PROJECT-011 | 穩定 projectCode 身分 | Grid `projectCode` | 數字 id | `PARTIAL` | 尚未建立 Legacy code 映射與唯一性契約 |
| PROJECT-012 | projectCodeAlias | Grid field | 未定位 | `NOT_STARTED` | 缺別名欄位 |
| PROJECT-013 | 中文／英文名稱及地址 | Grid fields | 部分欄位 | `PARTIAL` | 需逐欄位比對 |
| PROJECT-014 | mainProj 關聯 | Grid field | 未定位 | `NOT_STARTED` | 缺主專案關係 |
| PROJECT-015 | 專案目錄功能權限 F005 | `CreateFormProject` | 認證即可 | `NOT_STARTED` | 缺 function-code policy |
| PROJECT-016 | 建立與匯入共用入口但不同模式 | `DoProjectCreateImport` / `ExecuteNewProject` | create CRUD | `PARTIAL` | 缺正式匯入流程與 wizard |
| PROJECT-017 | 重用既有 FormProject 並刷新上下文 | `CreateFormProject` | Router | `NOT_STARTED` | 缺明確重用／刷新契約 |
| PROJECT-018 | 專案目錄載入鎖 | loading form / enabled state | 部分 loading | `PARTIAL` | 缺不可重入全域 gate |
| PROJECT-019 | OnlineList ProjectManagement 狀態 | `onlineList1` / `F_FunctionName` | 無 | `UNKNOWN` | 待確認 Presence/Lock 語意 |

## Action Eligibility

| Feature ID | Legacy 行為 | C# 來源 | Web 現況 | 狀態 | 缺口 |
|---|---|---|---|---|---|
| PROJECT-ELIG-001 | Eligibility 由模組、權限、專案狀態共同決定 | `FunctionButtons` + `FormBudgetProjectPick` | 直接路由 | `NOT_STARTED` | 缺中央 eligibility service |
| PROJECT-ELIG-002 | BUD/BID 分開選取與開啟 | `CreateFormBudgetByBUD/BID` | 單一 BudgetEditor | `PARTIAL` | 缺雙模式規則 |
| PROJECT-ELIG-003 | Contract 選取專案後開啟 | `SplitContract` | Contract routes | `PARTIAL` | 缺 eligible project query |
| PROJECT-ELIG-004 | Invoice 選取專案後開啟 | `Invoice` | Invoice routes | `PARTIAL` | 缺 lifecycle prerequisite |
| PROJECT-ELIG-005 | BudgetChange 選取專案後開啟 | `BudgetChange` | Contract issue/settlement 類頁面 | `PARTIAL` | 尚未建立 Legacy 對應 |
| PROJECT-ELIG-006 | SubClose 選取專案後開啟 | `SubClose` | Settlement page | `PARTIAL` | 缺 eligible project query |
| PROJECT-ELIG-007 | SubFinal 選取專案後開啟 | `SubFinal` | Final acceptance page | `PARTIAL` | 權限碼及前置狀態待確認 |
| PROJECT-ELIG-008 | 取消選取恢復原上下文 | FunctionButtons DialogResult branch | Browser back | `NOT_STARTED` | 缺 transaction-like context replacement |
| PROJECT-ELIG-009 | 專案權限不可由 URL 繞過 | `Auth` + Function Code | owner check | `PARTIAL` | 缺 action 細粒度驗證 |
| PROJECT-ELIG-010 | Template 與正式專案分流 | ProjectFilter / IsTemplate | 無 | `NOT_STARTED` | 缺模型與 API |

## 後續驗收規則

1. `ProjectsPage` 存在不等於 PROJECT Segment 完成。
2. 所有 Action API 必須在後端重新計算 eligibility。
3. 專案 DTO 必須攜帶穩定身分及能力，不可由前端推導。
4. 原碼未確認的 Type 值不得自行命名。
5. 刪除、匯入、模板、主專案與 OnlineList 仍需後續源碼補完。
