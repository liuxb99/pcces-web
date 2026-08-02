# PCCES 桌面版預算編輯器主框架與狀態模型

更新日期：2026-08-02

## 1. 調研範圍

本文件整理以下 C# 原始碼目前可直接確認的預算主框架行為：

- `PCCES_CS/Archnowledge.Pcces.PccesMain.Budget/frmBudget.cs`
- `FunctionButtons.CreateFormBudgetByBUD`
- `FunctionButtons.CreateFormBudgetByBID`
- `FormBudgetProjectPick`
- `BDGT_Component/B_Form.cs`
- `BDGT_Component/L_Form.cs`
- `BDGT_Component/S_Form2.cs`

本批先處理預算編輯器外殼、Action 模式、核心狀態、精度及子編輯器責任；工項 Grid 的完整事件、單價分析、資源與報表在後續文件繼續拆解。

## 2. frmBudget 並非單一 CRUD 頁面

`frmBudget` 同時承擔：

- 預算書編製 `PccesFormAction.BUD`
- 投標單填寫 `PccesFormAction.BID`
- 部分契約／變更相關共用編輯能力
- 工項樹與明細 Grid
- 下層單價累算
- 直接單價輸入
- 分段計價／公式
- 歷史價格
- 專案切換
- 自動保存
- 全案重新計算
- 貼上、複製與跨專案引用
- 精度、鎖定、唯讀與版本狀態

因此 Web 版不能將 `BudgetEditorPage` 只視為預算項目 CRUD。

## 3. Action 模式

### 3.1 BUD

入口：

```text
FunctionButtons.CreateFormBudgetByBUD
→ 權限 F003
→ FormBudgetProjectPick.ActionName = BUD
→ frmBudget.ActionName = BUD
```

### 3.2 BID

入口：

```text
FunctionButtons.CreateFormBudgetByBID
→ 權限 F004
→ FormBudgetProjectPick.ActionName = BID
→ frmBudget.ActionName = BID
```

BUD 與 BID 共用 `frmBudget`，但不能共用完全相同的 Domain 規則。`PccesFormAction` 會持續傳遞到子元件與資料查詢，並透過 `CommonMethods.GetActionNameString(ActionName)` 決定來源資料種類。

## 4. 主框架可確認狀態

`frmBudget` 目前可直接確認包含以下狀態群組。

### 4.1 工作與表單狀態

```text
FORM_STATUS
F_ModifyMode
_needClose
Is_SwitchProject
F_IsNeedToReloadAllData
ReadOnlyMode
IsLocked
IsLockedCnt
IsLockAnalys
```

這些狀態共同參與：

- 是否可編輯
- 是否可切換專案
- 是否需要全量重載
- 是否可離開或關閉
- 契約與分析區是否鎖定

### 4.2 專案與來源上下文

```text
projectCode
projectName
sourceProjectCode
parentProjectCode
currentDBName
F_FromDBName
companyDBName
```

Web 工作上下文至少必須帶：

```text
action
projectCode
sourceProjectCode
databaseContext
versionContext
readOnly
lockState
```

### 4.3 預算／變更版本

```text
budgetChangeCurrentVersion
changeManagementCurrentVersion
budgetType
IsAwardOfBid
F_IsBid
```

因此 Web 版不能只用 `project.status` 表達預算、投標、契約及變更版本。

### 4.4 計算與重載

```text
F_IsHasConfirmReCal
F_IsAnConfirmReCal
F_IsNeedToReloadAllData
tmrReCalAll
F_IsUseIR
UseCostStructure
```

可確認桌面版存在：

- 是否已確認重算
- 是否需要重新載入全案
- 延遲或定時重新計算
- 成本結構模式

Web 版後續需要正式 `Calculation State`，不能只在單筆更新後即時計算一個欄位。

### 4.5 自動保存與離開

```text
TM_BDGT_AutoSave
_needClose
FORM_STATUS
```

`frmBudget` 有自動保存 Timer，且主框架另有 `BDGT_DONT_CLOSE` 與 `IsCanSwitchForm` 閘門。Web 必須建立一致的 dirty state、autosave state、saving state 與 navigation guard。

## 5. 精度模型

`frmBudget` 明確區分：

```text
MainItemQtyPrecision
MainItemCostPrecison
MainItemAmountPrecision
MainItemAmountPrecisionDec
AnalysisQtyPrecision
AnalysisCostPrecision
AnalysisAmountPrecision
```

表示主工項與分析明細的：

- 數量精度
- 單價精度
- 金額精度

是分開設定的。

Web 不得以單一全域 `round()` 規則取代。

建議 Domain 模型：

```text
PrecisionPolicy
- main_item_quantity
- main_item_unit_cost
- main_item_amount
- analysis_quantity
- analysis_unit_cost
- analysis_amount
- rounding_mode
- effective_version
```

## 6. Grid 與編輯前後值

`frmBudget` 保存：

```text
QtyBeforeEdit / QtyAfterEdit
CostBeforeEdit / CostAfterEdit
AddQtyBeforeEdit / AddQtyAfterEdit
```

這表示桌面版會追蹤欄位修改前後值，用於：

- 驗證
- 重算
- 變更判斷
- 可能的還原或歷程

Web API 後續至少要支援：

```text
old_value
new_value
row_version
calculation_effects
```

## 7. 鎖定與唯讀

已確認：

```text
ReadOnlyMode
IsLocked
IsLockedCnt
IsLockAnalys
```

鎖定至少分成：

- 整體編輯器唯讀
- 一般預算鎖定
- 契約區鎖定
- 分析資料鎖定

Web 不得只用單一 `disabled` 或 `project.status=closed`。

## 8. 主畫面組成

`frmBudget` 主要包含：

- `FunctionButtons`
- `OnlineList`
- `LeftPanel`
- `MainPanel`
- `gridBudget`
- `statusBar`
- 專案切換按鈕
- 歷史價格選擇
- 子項數量／金額選擇
- 變更歷史選擇
- 自動保存與重新計算 Timer

它是一個有中央工作狀態的專案編輯工作台，而不是單一路由頁。

## 9. Web 復刻要求

Web 預算編輯器第一層至少要具備：

1. BUD／BID Action 明確分流。
2. 穩定 `projectCode` 工作上下文。
3. 專案切換前 dirty-state gate。
4. autosave 狀態與錯誤恢復。
5. read-only 與多層 lock policy。
6. 主工項／分析明細分離的精度政策。
7. 計算中、待重算、已確認重算狀態。
8. 版本與變更上下文。
9. 子編輯器由 item kind／action 決定。
10. 所有保存回傳 row version 與 calculation effects。

## 10. 尚待後續確認

- `FormStatus` enum 完整值。
- `ModiftyMode` enum 完整值。
- `IsCanSwitchForm()` 真實判斷。
- `frmBudget` 的 FormClosing、autosave Tick 與重算事件。
- `FormBudgetEditMain` 如何依 Kind 選取 B/L/S/Z 元件。
- `gridBudget` 各欄位編輯規則及 Domain 寫入鏈。
