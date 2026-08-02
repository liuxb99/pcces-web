# PCCES Web 契約與履約模組復刻對照

更新日期：2026-08-02

本文件將 Legacy 功能節點對照目前 Web 模組，僅作為差距盤點；正式實作前仍須回讀 C# 源碼。

## 1. 對照表

| Legacy 功能 | 主要 C# 入口 | Web 現況 | 判定 |
|---|---|---|---|
| 契約編製 | `FormSplitContract.cs` | 已有契約頁及部分 API | `PARTIAL` |
| 契約項目由預算形成 | `FormSplitContract.cs`、`DomainModule.Sub/*` | 尚未證明完整對應 | `UI_ONLY/PARTIAL` |
| 變更案建立 | `FormBudgetChange.cs`、`FormBudgetChange_Addnew.cs` | 有變更／議題類功能，但模型未證明一致 | `PARTIAL` |
| 變更責任與原因 | `FormBudgetChangeResponsibility.cs` | 尚無完整 Legacy 對應 | `NOT_STARTED/PARTIAL` |
| 變更歷史 | `FormBudgetChangeHistory.cs` | 尚無完整逐工項歷程 | `NOT_STARTED` |
| 估驗期別 | `FormInvoice.cs`、`FormInvoiceIndexNumber.cs` | 已有計價單頁及 API | `PARTIAL` |
| 當期／累計估驗 | `FormInvoiceSummary.cs` | 有金額欄位，但計算規則未驗證 | `PARTIAL` |
| 扣款／保留／調整 | `FormInvoiceDec2.cs` | 尚未證明完整 | `NOT_STARTED/PARTIAL` |
| 估驗進度 | `FormInvoiceProgress.cs` | 尚未對照 Legacy | `NOT_STARTED` |
| 估驗圖表 | `FormInvoiceGraphic.cs` | 尚未對照 Legacy | `NOT_STARTED` |
| 估驗匯入／匯出 | `FormInvoiceImport.cs`、`FormInvoiceExport.cs` | 尚未完整復刻 | `NOT_STARTED` |
| 估驗報表檢查 | `FormInvReportCheck.cs` | 尚無 Legacy 等價閘門 | `NOT_STARTED` |
| 結算 | `FormSubClose.cs`、`FormSubCloseInput.cs` | 已有結算頁面 | `UI_ONLY/PARTIAL` |
| 結算資訊與最終值 | `FormSubCloseInfo.cs` | 尚未驗證資料與狀態一致 | `PARTIAL` |
| 驗收 | `FormSubFinal.cs`、`FormSubFinalInput.cs` | 已有驗收頁面 | `UI_ONLY/PARTIAL` |
| 驗收項目挑選 | `FormSubFinal_ItemPick.cs` | 尚未證明存在等價能力 | `NOT_STARTED` |
| 契約／變更／估驗／結算／驗收報表 | `ucSubCtr`、`ucSubChg`、`ucSubAcc`、`ucSubClose`、`ucSubFinal` | 有通用報表能力，但格式未對照 | `PARTIAL` |

## 2. Web 架構主要缺口

### 2.1 生命週期未形成單一 Domain

目前頁面與 API 雖已涵蓋契約、計價、結算及驗收，但尚未證明存在以下完整鏈條：

```text
Contract baseline
→ Change effective version
→ Invoice cumulative calculation
→ Closeout final values
→ Final acceptance
```

### 2.2 缺 Legacy Action 與權限語意

Web 必須保留：

- `F009` 契約編製
- `F010` 估驗記錄
- `F011` 契約／預算變更
- `F012` 結算作業
- `SubFinal` 驗收 Action 的正式權限來源仍需深讀確認

### 2.3 缺版本與來源追蹤

需能記錄：

- 契約來源預算工項
- 變更案與原契約／原工項
- 估驗期別依據的有效契約版本
- 結算依據的累計估驗與變更
- 驗收依據的結算版本

### 2.4 缺狀態閘門

需從 Legacy 源碼確認並復刻：

- 已有估驗後能否修改契約。
- 變更生效後如何處理既有估驗。
- 結算後哪些操作鎖定。
- 驗收後是否封存。
- 刪除前是否需要回退後續階段。

### 2.5 缺正式報表相容性

通用 Excel 或 PDF 輸出不能直接取代 Legacy 報表。需逐份確認：

- 欄位
- 分組
- 排序
- 小計／累計
- 分頁
- 簽章欄
- 報表前檢查

## 3. 建議復刻順序

```text
1. Contract Domain baseline
2. ContractItem 與 BudgetItem 來源關聯
3. ChangeIssue／ChangeItem 版本模型
4. InvoicePeriod／InvoiceItem／Deduction
5. 累計計算與鎖定
6. Closeout／CloseoutItem
7. FinalAcceptance／AcceptanceItem
8. 履約報表相容
```

## 4. 實作前必讀源碼

- `SplitContract/FormSplitContract.cs`
- `BudgetChange/FormBudgetChange.cs`
- `BudgetChange/FormBudgetChange_Addnew.cs`
- `Budget.BudgetChange/FormBudgetChangeInfo*.cs`
- `Budget.BudgetChange/FormBudgetChangeResponsibility.cs`
- `Budget.BudgetChange/FormBudgetChangeHistory.cs`
- `Invoice/FormInvoice.cs`
- `Invoice/FormInvoiceProgress.cs`
- `Invoice/FormInvoiceSummary.cs`
- `Invoice/FormInvoiceDec2.cs`
- `Invoice/FormInvoiceIndexNumber.cs`
- `Invoice/FormInvoiceImport.cs`
- `Invoice/FormInvoiceExport.cs`
- `SubClose/FormSubClose*.cs`
- `SubFinal/FormSubFinal*.cs`
- `Report/FormInvReportCheck.cs`
- `Report/FormInvoiceReport.cs`
- `Report/ucSub*.cs`
