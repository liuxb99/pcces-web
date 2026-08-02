# PCCES 預算工項子編輯器元件規格

更新日期：2026-08-02

## 1. 調研範圍

本文件整理目前可直接確認的：

- `BDGT_Component/B_Form.cs`
- `BDGT_Component/L_Form.cs`
- `BDGT_Component/S_Form2.cs`
- `frmBudget.cs` 中對 Action、精度、鎖定與計算狀態的依賴

`Z_Form` 與其他 S_Form 版本將在後續批次補齊。

## 2. 元件不是裝飾，而是不同 Domain 編輯模式

PCCES 桌面版不是所有工項都用同一組欄位編輯。不同工項種類會開啟不同子元件，分別代表：

- 下層累算
- 直接單價
- 分段計價／公式
- 其他特殊計算模式

Web 版若只用一張通用 `BudgetItemForm`，會遺失原始行為。

## 3. B_Form：下層自動累算

`B_Form` 顯示：

```text
單價 = 下層自動累算
```

它只接收 `PccesFormAction`，沒有直接單價輸入欄位。

### 可確認規則

- 此類項目的單價不能由使用者直接輸入。
- 單價來源為下層項目計算結果。
- Action 仍需傳入，表示 BUD/BID 等來源資料與計算規則可能不同。

### Web 要求

```text
item.cost_mode = CHILD_ROLLUP
item.unit_cost = calculated
editable = false
```

API 不得接受客戶端直接改寫此類單價。

## 4. L_Form：直接單價輸入

`L_Form` 包含：

- `txtCost`
- `_ActionName`
- `_Issue`
- `_UserID`
- `SetCostInputEnabled(bool)`

載入時：

```text
txtCost = Parent FormBudgetEditMain.ItemCost
```

驗證時會嘗試轉成數值；失敗顯示「金額有誤」。

### 可確認規則

- 單價從父編輯器的 `ItemCost` 載入。
- 是否可輸入由父層顯式控制。
- 金額必須為數值。
- 驗證失敗時不應允許離開該欄位或提交。

### Web 要求

```text
item.cost_mode = DIRECT_INPUT
editable = capability.can_edit_cost
```

伺服器端也必須做 Decimal 驗證，不能只靠前端 input type。

## 5. S_Form2：分段計價與公式

`S_Form2` 是獨立 Form，包含兩組主要資料：

1. `ItemB`：納入加總的工項或變數。
2. `ItemC`：金額區間、費率及可選公式。

### 5.1 工作上下文

```text
ActionName
UserID
ProjectCode
ParentPrintNo
ParentSNo
Issue
```

資料來源以：

```text
CommonMethods.GetActionNameString(ActionName)
```

決定 `srckind`。

### 5.2 BUD 特有行為

若 Action 為 `BUD`：

```text
gridItemB.VarSign.Visible = true
```

表示預算模式允許每個加總來源使用正負號。

### 5.3 ItemB

載入：

```text
ItemB.ListItem(projectCode, parent context)
```

Grid 顯示：

- ItemNo
- CName
- PrintNo
- VarSign
- parentCodeSno
- itemCodeSno

`VarSign`：

```text
+1 → ＋
-1 → －
```

以 `VAR` 開頭的 PrintNo 會視為自訂變數，名稱由 `PCals.GetCustomVarList()` 對照 `VarAlias`。

### 5.4 ItemC

載入：

```text
ItemC.ListItem(projectCode, printNo)
```

欄位：

- Lower
- Upper
- Rate
- Formula
- PrintNo
- sNo

顯示語意：

```text
Lower < 金額 ≦ Upper
```

### 5.5 預設分段

若尚無 ItemC，桌面版可預設建立：

| 下限 | 上限 | 費率 |
|---:|---:|---:|
| 0 | 5,000,000 | 3.0 |
| 5,000,000 | 25,000,000 | 1.5 |
| 25,000,000 | 100,000,000 | 1.0 |
| 100,000,000 | 500,000,000 | 0.7 |
| 500,000,000 | 極大值 | 0.5 |

這些值是 Legacy 預設資料，不應未經確認就改成現代 UI 的任意預設。

### 5.6 Formula 可用條件

公式欄位是否可用同時受：

- DataTable 是否存在 `formula` 欄位
- 專案是否啟用新計算方式 `GetPubProjectEnableNewCalculateCost(projectCode)`

控制。

這是 schema capability 加 project capability 的雙重條件。

## 6. 共通 Web Domain 模型

建議工項編輯模式至少包含：

```text
CHILD_ROLLUP
DIRECT_INPUT
TIERED_RATE
FORMULA
SPECIAL
```

建議 API：

```text
GET /projects/{projectCode}/budget-items/{itemId}/editor-schema
GET /projects/{projectCode}/budget-items/{itemId}/calculation-definition
PUT /projects/{projectCode}/budget-items/{itemId}/direct-cost
PUT /projects/{projectCode}/budget-items/{itemId}/rollup-sources
PUT /projects/{projectCode}/budget-items/{itemId}/rate-tiers
POST /projects/{projectCode}/budget-items/{itemId}/validate-formula
```

## 7. 驗收要點

- B 類單價不可直接修改。
- L 類依 capability 決定單價欄位可編輯。
- 所有數字以 Decimal 處理。
- S 類需保存來源項、正負號、區間與費率。
- 自訂變數需保存穩定變數名稱與顯示別名。
- Formula 需由後端驗證，不可直接執行任意文字。
- ActionName 與 Issue 必須存在於資料契約。

## 8. 尚待補完

- `FormBudgetEditMain` 的元件選擇條件。
- `Z_Form` 的完整責任。
- S_Form2 新增、刪除、保存及公式檢查事件。
- ItemB/ItemC 的完整交易與重算副作用。
