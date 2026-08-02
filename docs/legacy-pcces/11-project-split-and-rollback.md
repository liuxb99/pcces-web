# PCCES 專案分拆與回復規則

更新日期：2026-08-02

## 1. 範圍

本文記錄 `formNewProjectWizard` 中已確認的專案分拆互動、數值欄位編輯、子專案建立、權限建立與取消回復規則。

## 2. 分拆狀態

Wizard 使用：

```text
F_SPLT_STATUS
F_IsSplitSucceeded
F_ProjectCode
F_SubProjectCode
DT_bud
```

分拆編輯只有在：

```text
F_SPLT_STATUS == "NOR"
```

時允許進行。

## 3. 分拆項目選擇

Grid 中以 `IsCheck` 決定是否納入子專案。

儲存前逐筆同步：

- 勾選：`chk = 1`
- 未勾選：`chk = 0`
- 分拆數量：`ThisQty = SplQty`
- 分拆單價／金額：`ThisCost = SplCost`

最後呼叫：

```text
ItemA.CopyItemA(F_SubProjectCode, DT_bud, F_ProjectCode)
```

將選取內容由主專案複製至子專案。

## 4. 編輯欄位規則

### 4.1 「式」與 B 類項目

若：

```text
qty == 1 && unitName == "式"
```

或：

```text
Kind == "B"
```

只允許編輯 `SplCost`。

### 4.2 一般項目

一般項目只允許編輯 `SplQty`。

### 4.3 非正值

若分拆數量或分拆金額小於等於 0，Legacy 會拒絕停留在該編輯欄位。

## 5. 階層選取傳播

修改 `IsCheck` 後，Legacy 會取得目前節點的最後子節點，並將勾選狀態傳播至整個子樹。

這表示 Web 的樹狀預算分拆不能逐列獨立勾選而忽略父子關係。

## 6. 數值精度

Grid 格式由以下設定決定：

```text
F_MainQty
F_MainCst
F_MainAmt
F_AnaQty
F_AnaCst
F_AnaAmt
```

分拆 Grid 至少使用主項數量與成本精度設定格式化：

- `qty`
- `cost`
- `RemainQty`
- `RemainCost`
- `SplQty`
- `SplCost`

Web 必須以 Decimal 與專案精度政策處理，不可直接用浮點數。

## 7. 子專案建立

`SaveProjectInfo()` 已確認：

1. 建立 `Project` Domain 物件。
2. 設定 `ps_projectCode = F_SubProjectCode`。
3. 設定 `ps_srckind = bud`。
4. 呼叫 `InseItem()` 建立子專案。
5. 插入 `ProjAuthority(ProjectCode, UserID)`，使操作人取得權限。

之後再由 `CopyItemA` 複製所選預算內容。

## 8. 取消回復

若分拆尚未成功而使用者取消：

```text
DeleteNewProject()
→ PubProject.DeleAll(txtProjectCode)
```

所以分拆不是「完成時才第一次建立子專案」，而可能先建立再填充；取消必須移除已建立的全部資料。

## 9. Web Domain 需求

建議建立：

```text
ProjectSplitSession
ProjectSplitSelection
ProjectSplitPrecisionPolicy
ProjectSplitCommitService
ProjectSplitRollbackService
```

Commit 流程：

```text
validate source project
→ reserve sub project code
→ create draft child project
→ validate selected tree
→ copy selected items
→ recalculate child project
→ create authority
→ mark committed
```

Rollback 流程：

```text
remove copied items
→ remove project-dependent records
→ remove project authority
→ remove child project
→ release reserved code
```

## 10. Web 驗收案例

- 父節點勾選會套用至全部子孫。
- 「式」及 B 類項目只能輸入分拆金額。
- 一般項目只能輸入分拆數量。
- 零值與負值被拒絕。
- 精度依專案設定處理。
- 分拆取消後不存在孤兒子專案。
- 分拆成功後建立者具專案權限。
- 子專案與主專案的 lineage 可追溯。

## 11. 尚待追蹤

- `F_SubProjectCode` 生成規則。
- 主／子專案的 `mainProj` 寫入規則。
- 分拆後主專案剩餘數量與金額更新方式。
- 分拆是否允許重複執行。
- `CopyItemA` 的階層、資源與單價分析複製範圍。
