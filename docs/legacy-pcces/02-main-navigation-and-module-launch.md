# PCCES 桌面版主導航與模組啟動規格

更新日期：2026-08-02

## 1. 範圍

本文件描述 PCCES Win 4.3 登入後的主導航、首頁面板、模組分類、權限檢查、表單切換與模組啟動生命週期。主要依據：

- `PCCES_CS/Archnowledge.Pcces.PccesMain/frmPccesMain.cs`
- `PCCES_CS/Archnowledge.Pcces.PccesMain.ArchControls/FunctionButtons.cs`
- `PCCES_CS/Archnowledge.Pcces.PccesMain/FormPanel.cs`
- `PCCES_CS/Archnowledge.Pcces.PccesMain/FormPanel2.cs`
- `PCCES_CS/Archnowledge.Pcces.PccesMain/FormPanel3.cs`

可信度標記：

- `CONFIRMED`：可由 C# 源碼直接確認。
- `INFERRED`：依多處事件與調用關係推導。
- `REQUIRES_RUNTIME_TEST`：需實際執行桌面程式確認。

---

## 2. 主導航不是一般選單

桌面版以 `FunctionButtons` 作為中央導航協調器。首頁面板按鈕不直接建立業務表單，而是轉呼叫主框架中的同一個 `FunctionButtons` 實例。因此三種首頁面板只是不同視覺入口，真正的權限、切換、關閉舊表單與建立新表單邏輯集中在 `FunctionButtons`。

```text
FormPanel / FormPanel2 / FormPanel3
        ↓ 按鈕事件
frmPccesMain.functionButtons1
        ↓
權限檢查、切換閘門、關閉/重用 MDI 子表單
        ↓
建立或顯示目標模組
```

這個集中式協調器是 Web 復刻時必須保留的行為邊界。React Router 只能負責 URL，不足以取代模組啟動協調器。

---

## 3. 模組分類與二級按鈕

`FunctionButtons` 使用 `FunctionOpenMode` 管理目前功能群組。

### 3.1 預算群組

主按鈕切換到 `Budget` 後顯示：

- 專案目錄：`BtnFunc5` → `CreateFormProject()`
- 預算書編製：`BtnFunc3` → `CreateFormBudgetByBUD()`

### 3.2 投標群組

主按鈕切換到 `Bid` 後顯示：

- 空白標單／投標資料匯入：`BtnFuncBidImport`
- 投標單填寫：`BtnFunc4` → `CreateFormBudgetByBID()`

匯入流程不是單純上傳檔案，而是：

```text
檢查 F00500010002 權限
→ 開啟／建立專案目錄
→ 建立 formNewProjectWizard
→ _IniMode = "2"
→ _IsAddOn = "BID"
→ 完成後重新讀取專案資料
→ 重綁 Grid
→ 定位到新增資料列
```

### 3.3 共用資料群組

主按鈕切換到 `Common` 後顯示：

- 基本資料庫維護：`BtnFunc2`，權限 `F002`
- 歷史工程單位造價比對：`BtnFunc8`，權限 `F008`
- 經費審查比對：`BtnFunc7`，權限 `F007`

### 3.4 契約／估驗群組

主按鈕切換到 `Invoice` 後顯示多個履約模組，包括：

- 契約編製：`BtnFunc9`，權限 `F009`
- 契約變更：`BtnFunc6`
- 估驗記錄：`BtnFunc10`，權限 `F010`
- 後續結算、驗收相關按鈕：`BtnFunc11`、`BtnFunc12` 等

這些功能不是互相獨立的 CRUD 頁面，而是以專案／預算選取對話框作為前置入口，再開啟指定工作表單。

---

## 4. 首頁面板行為

### 4.1 三種首頁

`frmPccesMain.LoadingForm()` 依 INI 的 `HomePanel/Home` 值建立：

- `1` → `FormPanel`
- `2` → `FormPanel2`
- `3` → `FormPanel3`

每種首頁在 MDI 中保持單一實例。若已存在，不重建。

### 4.2 FormPanel2 模組顯示

`FormPanel2.UpdateMenu()` 依 `ModuleManager` 決定模組按鈕是否顯示：

- `EnableBudgetMdoule`
- `EnableContractModule`
- `EnableBidMdoule`
- `EnableCommonMdoule`

因此桌面版至少存在兩層可用性控制：

1. 模組級啟用／停用，決定入口是否可見。
2. 功能權限，決定使用者是否能執行。

Web 版不能只靠前端選單顯示，後端仍需再次驗證功能權限。

### 4.3 首頁說明文字

滑鼠進入首頁按鈕時，桌面版顯示該模組正式用途說明。已確認的功能語意包括：

- 基本資料庫維護：工項及單價分析、工程會工項編碼組合。
- 專案目錄：建立、轉入、刪除、瀏覽專案屬性。
- 投標單填寫：轉入空白標單、製作投標單與電子檔。
- 預算書編製：總表、詳細表、單價分析、資源統計、空白電子標單、Excel 匯入、併標與分標。
- 契約編製：決標後契約核定與契約書相關報表。
- 契約變更：追加追減及變更報表。
- 估驗記錄：分期估驗資料及總表、明細表。
- 經費審查比對：多專案單價與單價分析內容比對，可設定精度與方式。
- 歷史工程單位造價：多專案數量、單價、複價比對。
- 系統維護：主辦單位、廠商、行情、常用字串、系統訊息、專案權限、帳號權限、資料庫切換。

這些文字可直接作為 Web 功能規格與驗收範圍，不應只當首頁行銷文案。

---

## 5. 模組啟動共同流程

多數功能遵循以下生命週期：

```text
IsCanSwitchForm()
→ 設定目前 FunctionOpenMode
→ DBClass.ChkAuthority(UserID, FunctionCode)
→ 權限不足則顯示功能名稱與拒絕訊息
→ 必要時停用目前 ParentForm
→ 顯示 Wait Cursor／載入提示
→ HideAllChild()
→ 搜尋既有 MDI 子表單
→ 若存在則更新使用者上下文並重用
→ 否則建立新表單並注入 UserID/UserName/ServerName/HasRegistered
→ 關閉或 Dispose 其他非首頁子表單
→ 收起 LeftPanel
→ 顯示目標表單
→ 恢復 Cursor／控制項狀態
```

這表示 Web 版需要統一的 `Module Launch Service`，至少負責：

- 模組可用性判斷。
- 使用者功能權限。
- 是否允許離開目前工作。
- 目前工作上下文與 dirty state。
- 專案選取前置流程。
- 使用者、資料庫與授權上下文。
- 重複開啟的去重與狀態恢復。
- 導航失敗後恢復原畫面。

---

## 6. 表單切換與唯一性

### 6.1 重用既有表單

基本資料庫、比較及系統維護等模組會先掃描 `MdiChildren`。若同類型表單已存在：

- 不建立第二份。
- 更新使用者／伺服器／註冊狀態。
- 重新 Show／BringToFront。

### 6.2 關閉其他業務表單

切換主要模組時，通常保留三個首頁表單，關閉其他 MDI 子表單。部分流程允許保留目前目標模組。

### 6.3 父表單情境分流

`FunctionButtons` 可能嵌在：

- `frmPccesMain`
- 目標業務表單本身
- 其他子表單

源碼會依父層不同決定：

- 使用 `ParentForm.MdiChildren` 或 `ParentForm.ParentForm.MdiChildren`。
- 是否停用父表單。
- 新表單的 `MdiParent`。
- 是否關閉／Dispose 原父表單。

Web 版不需要照搬 WinForms 父子控制項，但必須保留「從任何模組切換後，只有一個有效主工作上下文」的結果。

---

## 7. 權限模型

已直接確認的功能代碼：

| 功能 | 權限代碼 |
|---|---|
| 系統維護特定入口 | `F0010007` |
| 基本資料庫維護 | `F002` |
| 投標資料匯入 | `F00500010002` |
| 歷史工程單位造價 | `F008` |
| 經費審查比對 | `F007` |
| 契約編製 | `F009` |
| 估驗記錄 | `F010` |

權限拒絕訊息使用 `DBClass.GetFuncName(code)` 取得正式功能名稱，再顯示「這個功能您沒有權限使用」。

Web 復刻要求：

1. 建立 Legacy Function Code 對照表。
2. 前端選單與按鈕依權限隱藏或停用。
3. API 必須再次檢查同一權限。
4. 權限拒絕回應需帶正式功能代碼與名稱。
5. 測試需證明直接呼叫 URL／API 不能繞過權限。

---

## 8. 前置專案選取

契約編製等履約模組不是直接開啟列表，而是先建立 `FormBudgetProjectPick`，設定 `_ActionName` 後讓使用者選擇可用的預算／專案。

已確認：

```text
_ActionName = PccesFormAction.SplitContract
_UserID = current user
_HasRegistered = current registration state
```

取消選取時，原畫面需恢復可操作；成功選取後才清理不相關子表單並進入目標模組。

Web 版目前由 URL 中的 `projectId` 直接進入多數合約頁面，尚未證明已復刻：

- 可選專案篩選規則。
- 專案狀態限制。
- 使用者專案權限。
- 取消後狀態恢復。
- ActionName 對應的不同入口模式。

---

## 9. 系統維護入口

系統維護不是單一頁面。桌面版建立／重用 `frmSysMaintain` 後，會：

- 注入 UserID、UserName、ServerName、HasRegistered。
- 收起主框架 LeftPanel。
- 選定特定頁籤，例如 `Tab_G.Tab.Selected = true`。
- 視來源表單決定是否先停用或關閉舊表單。

因此 Web `AdminPage` 必須拆成可追蹤的子功能，而不能以一個管理頁存在就視為完成。

---

## 10. Web 對照結論

目前 React 已有多個路由與側邊導航，但只證明「可到達頁面」。尚未證明：

- 模組級啟用／停用。
- Legacy Function Code 權限。
- 統一模組切換閘門。
- dirty state／工作鎖。
- 專案選取前置流程。
- 既有工作上下文重用。
- 取消後恢復。
- 授權狀態注入。
- 導航稽核。

因此現況統一標記為 `UI_ONLY` 或 `PARTIAL`，不得升為 `LEGACY_MATCHED`。

---

## 11. 下一步必讀

為完成本 Segment，下一批需繼續讀取：

- `FunctionButtons.cs` 後半部全部功能事件。
- `IsCanSwitchForm()`、`HideAllChild()`、`SetActiveFunction()`、`OPEN_MODE_CHECK()`。
- `FormBudgetProjectPick` 及 `PccesFormAction`。
- `ModuleManager`。
- `DBClass.ChkAuthority`、`GetFuncName`。
- `OnlineList` 與可能的跨使用者工作鎖。

完成後再建立正式的 `Legacy Function Catalog` 與 Web Module Launch Contract。
