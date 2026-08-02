# PCCES C# Legacy：預算工項種類與 Domain 規則

更新日期：2026-08-02

## 1. 目的

本文件整理 `B/F/L/S/U/Z` 六種工項種類在桌面版的資料、輸入與計算差異，作為 Web Domain Model 與 API 驗證基準。

## 2. 共用上下文

所有工項類型都依賴：

- `ProjectCode`
- `PccesFormAction`
- `Issue`
- `sNo`
- `PrintNo`
- `UserID`

子編輯器不能脫離上述上下文獨立保存。

## 3. B：下層累算項

`B_Form` 顯示「單價 = 下層自動累算」。

確認規則：

- 單價不是使用者直接輸入。
- 單價來源是下層工項計算結果。
- 可選擇 `PrintToAnalysis`。
- 保存時一般不應接受前端傳入任意 unit cost。

Web Domain：

```text
item_kind = B
cost_mode = CHILD_ROLLUP
unit_cost_source = CHILDREN
```

## 4. L：獨立計價項

`L_Form` 提供直接單價欄位，載入父表單 `ItemCost`。

確認規則：

- 單價必須可轉成數字。
- 欄位是否可編輯由父表單控制。
- 中文單位啟用時必填。
- SubChange Action 寫入變更單價，而非原單價。

Web Domain：

```text
item_kind = L
cost_mode = DIRECT_INPUT
requires_unit = true
```

## 5. F：費率項

`FormBudgetEditMain` 會從 `F_Form` 讀取 `_txtRate`。

已確認：

- F 類以 rate 作為核心輸入。
- 可存在共用 VDF1；只有指定 sNo 顯示及帶入該值。
- F 類與 L 類在 `share` 欄位上使用 null 語意。

仍需後續追蹤：

- 完整費率計算基數。
- VDF1 的業務名稱與計算公式。
- F_Form 的完整驗證與保存副作用。

Web Domain：

```text
item_kind = F
cost_mode = RATE
rate = decimal
```

## 6. S：分段計價項

S 類包含兩組資料：

- ItemB：加總來源，可能帶正負號及自訂變數。
- ItemC：金額區間、費率與公式。

確認規則：

- BUD Action 顯示 `VarSign`。
- 正負號映射：`+1` 為加，`-1` 為減。
- ItemC 以 down、up、rate 表達分段。
- Formula 欄位是否有效受 Schema 與專案新計算能力雙重控制。
- 預設區間可由系統建立。
- 改成非 S 類時，原 ItemC 被刪除。

Web Domain：

```text
item_kind = S
cost_mode = TIERED_RATE
sources[]
tiers[]
formula_capability
```

## 7. U：公式項

`FormBudgetEditMain` 從 `U_Form` 讀取公式，保存前使用 `ArchChkFormula2()` 驗證。

確認規則：

- 非空公式必須通過 Domain parser。
- 前端語法檢查不能替代後端 parser。
- 公式錯誤禁止保存。

Web Domain：

```text
item_kind = U
cost_mode = FORMULA
formula = string
```

## 8. Z：加總項

`Z_Form` 顯示「單價 = 加總項目總金額」，管理 ItemB 加總來源。

確認規則：

- 來源包含 ItemNo、CName、PrintNo、VarSign。
- BUD Action 可顯示正負號。
- 可從 `PCals.GetCustomVarList()` 取得自訂變數。
- 特殊 PrintNo `99999999999999999999999999999999` 時會刪除既有加總來源並停用挑選。
- 保存 Z 類時 ItemNo 強制清空。

Web Domain：

```text
item_kind = Z
cost_mode = SUM_REFERENCES
sources[]
item_no = null
```

特殊 PrintNo 的真實業務名稱尚未由目前證據確認，必須保留原值並標記 `REQUIRES_MORE_SOURCE`。

## 9. 類型轉換規則

類型轉換不是只修改 `kind` 欄位。

至少確認：

- 轉成非 S 類會刪除 ItemC。
- Z 類會清空 ItemNo。
- 不同類型使用 cost、rate、formula、ItemB、ItemC 的不同組合。
- 子編輯器會整個替換。

Web 必須提供 Domain command：

```text
ChangeBudgetItemKind
```

由後端根據來源類型與目標類型執行資料清理、驗證與重算，不允許前端分開呼叫多個 CRUD API。

## 10. 建議類型資料模型

```text
BudgetItemCalculation
- item_kind
- cost_mode
- direct_cost
- rate
- formula
- rollup_policy
- source_relations
- tiers
- rounding_rule
- share_source_sno
```

## 11. 驗收案例

每一種類型至少需要：

- 新增。
- 載入。
- 修改。
- 取消。
- 保存。
- 類型互轉。
- Action BUD／BID／SubChange 差異。
- Issue 差異。
- 鎖定衝突。
- 精度與重算。
