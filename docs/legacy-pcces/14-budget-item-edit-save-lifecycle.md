# PCCES C# Legacy：預算工項編輯與保存生命週期

更新日期：2026-08-02

## 1. 範圍

本文件依據 `FormBudgetEditMain.cs`、`frmBudget.cs` 與 `BDGT_Component` 子元件，整理單一預算工項由開啟、鎖定、載入、類型切換、驗證、保存到解鎖的完整互動邏輯。

證據等級：

- `CONFIRMED`：可由 C# 原碼直接確認。
- `REQUIRES_MORE_SOURCE`：已有入口或狀態，但仍需追蹤 Domain 類別或其他事件。

## 2. 工項編輯上下文

`FormBudgetEditMain` 接收並保存下列必要上下文：

- `UserID`
- `PccesFormAction`
- `ProjectCode`
- `Issue`
- `Item_sNo`
- `PrintNo`
- `BDGT_ITEM_TYPE`
- `ItemCost`
- `ItemRate`
- `FormulaStr`
- `ChildCount`
- `PrintToAnalysis`
- `PccesCode`
- `IsTemplate`
- `AllowRestrictEdit`
- `IsCostStructure`
- 主項與分析項的數量、單價、金額精度

因此工項編輯不是以單一資料列 ID 即可完成；它依賴專案、Action、Issue、階層位置、工項種類、鎖定狀態與精度政策。

## 3. 工項類型與子編輯器映射

`SetItemType()` 與 `Reload_ChildForm()` 明確定義類型映射：

| `BDGT_ITEM_TYPE` | CheckedIndex | 子編輯器 | 核心語意 |
|---|---:|---|---|
| `B` | 0 | `B_Form` | 單價由下層自動累算 |
| `L` | 1 | `L_Form` | 獨立計價／直接輸入單價 |
| `F` | 2 | `F_Form` | 費率型工項 |
| `S` | 3 | `S_Form` | 分段計價／ItemB、ItemC 規則 |
| `Z` | 4 | `Z_Form` | 加總項目總金額 |
| `U` | 5 | `U_Form` | 公式型工項 |

每次切換類型時，舊子元件會 Dispose 並從 `PNL_CHILD` 移除，再建立新子元件，注入 Action、UserID、Issue 等上下文。

Web 版必須以明確的 `item_kind` 驅動對應編輯器與 Domain 規則，不能只靠前端顯示不同欄位。

## 4. 單位與名稱資料來源

中文、英文單位由 `UserDefind` 取得：

```sql
Select cString as 中文單位
From UserDefind
Where kind='cUnit'
Order By IsNull(Times,0) Desc
```

```sql
Select cString as Unit
From UserDefind
Where kind='eUnit'
Order By IsNull(Times,0) Desc
```

桌面版會額外加入空白選項。工項保存後也會呼叫 `AddNewCNameString()`、`AddNewENameString()` 更新常用名稱資料。

這表示單位與名稱不是純自由文字；系統具備可重用辭彙／最近使用資料來源。

## 5. 保存前驗證

`BtnOK_Click()` 至少執行以下檢查：

### 5.1 取位與攤提項目

當 `setDecimal < 0` 且未指定攤提項目時，禁止保存。

若存在可選攤提項目，提示「請先設定攤提項目」。若不存在直接輸入項，提示該主項大類無法設定個位數以上取位。

### 5.2 L 類單位必填

獨立計價項 `L` 在中文單位欄位啟用時，必須有單位名稱。

### 5.3 公式驗證

若公式非空，呼叫：

```text
PubTools.ArchChkFormula2(formula)
```

回傳錯誤時禁止保存，並顯示 Domain 錯誤訊息。

### 5.4 子編輯器輸入

- `L_Form`：讀取直接單價。
- `F_Form`：讀取費率。
- `U_Form`：讀取公式。
- `S`、`Z`：其 ItemB／ItemC 關係由子元件自行管理。

## 6. 保存映射

保存時建立 `ItemA` 並設定：

- `srckind`：由 `PccesFormAction` 轉換。
- `projectCode`
- `itemNo`
- `sNo`
- `kind`
- 中文／英文名稱
- 中文／英文單位
- 數量或變更數量
- 單價或變更單價
- 費率
- 公式
- 備註
- 取位設定
- 攤提項目 SNo
- PrintNo
- PrintToAnalysis
- PCCES 工項代碼
- Issue

最後呼叫：

```text
ItemA.UpdItem()
```

### 6.1 SubChange 特殊欄位

當 Action 為 `SubChange`：

- 單價寫入 `ChgCost`
- 數量寫入 `ChgQty`

其他 Action 使用一般 `cost`、`qty`。

### 6.2 Z 類清空 ItemNo

Z 類保存時強制：

```text
itemNo = ""
```

### 6.3 Share 欄位

L、F 類將 `share` 設為 null；其他類型使用特殊 `DBNull` 語意。若選定攤提來源，另保存 `ShareSno`。

### 6.4 非 S 類刪除 ItemC

若目前類型不是 S，保存後會刪除該 ProjectCode、PrintNo 對應的 ItemC。

此規則表示「工項類型轉換」會清除不再適用的分段計價資料，必須位於同一交易中。

## 7. 工項鎖定與關閉

`FormBudgetEditMain_FormClosing()` 會呼叫：

```text
DBClass.ItemA_UnLock(sNo, projectCode, actionName)
```

因此開啟編輯器前應存在相對應鎖定流程。即使使用者取消或直接關閉視窗，也必須釋放工項鎖。

Web 版需要：

- 編輯租約／row lock token
- lock owner
- lock acquired time
- heartbeat 或 expiry
- save／cancel／disconnect 時釋放

## 8. 視窗狀態

桌面版保存工項編輯視窗的位置、尺寸及 WindowState。這是 UX 相容項，可在 Web 現代化，但不影響 Domain 相容。

## 9. Web 必須建立的服務邊界

建議至少拆分：

```text
GET  /projects/{projectCode}/budget-items/{sNo}/edit-context
POST /projects/{projectCode}/budget-items/{sNo}/locks
PUT  /projects/{projectCode}/budget-items/{sNo}
DELETE /projects/{projectCode}/budget-items/{sNo}/locks/{lockToken}
```

保存請求應包含：

```text
projectCode
action
issue
sNo
printNo
itemKind
quantity
unitCost
rate
formula
roundingRule
shareSno
printToAnalysis
pccesCode
rowVersion
lockToken
```

## 10. 原子性要求

以下必須在同一交易內：

1. 驗證 lock／rowVersion。
2. 驗證類型與欄位。
3. 更新 ItemA。
4. 依類型更新／刪除 ItemB、ItemC。
5. 更新攤提關聯。
6. 寫入稽核紀錄。
7. 觸發必要的重算標記。

任一步驟失敗不得留下部分轉換狀態。
