# PCCES 契約與履約生命週期功能圖

更新日期：2026-08-02

本文件將契約、變更、估驗、結算及驗收串成一條完整生命週期，避免 Web 版將各頁面實作成互不相干的 CRUD 模組。

## 1. 總體生命週期

```text
預算／發包資料
→ 契約／分包建立
→ 契約項目與契約金額確立
→ 估驗期別持續累計
→ 契約／預算變更調整基準
→ 後續估驗依最新有效版本計算
→ 最終結算
→ 驗收
→ 報表、封存與查詢
```

## 2. 階段功能樹

### A. 契約建立

```text
契約入口
├── 專案資格與 F009 權限
├── 選取來源預算／發包資料
├── 建立契約基本資料
├── 選取契約項目
├── 形成契約金額
├── 保存契約版本
└── 契約報表
```

主要源碼：
- `SplitContract/FormSplitContract.cs`
- `DomainModule.Sub/*`
- `Report/ucSubCtr.cs`

### B. 預算／契約變更

```text
變更入口
├── 專案資格與 F011 權限
├── 建立變更案
├── 維護變更基本資訊
├── 指定責任／原因
├── 增項
├── 減項
├── 原項目數量或單價調整
├── 變更前後差異
├── 變更歷史
└── 變更報表
```

主要源碼：
- `BudgetChange/FormBudgetChange.cs`
- `FormBudgetChange_Addnew.cs`
- `Budget.BudgetChange/FormBudgetChangeInfo*.cs`
- `FormBudgetChangeResponsibility.cs`
- `FormBudgetChangeHistory.cs`
- `DomainModule.SubChg/*`
- `Report/ucSubChg.cs`

### C. 估驗計價

```text
估驗入口
├── 專案資格與 F010 權限
├── 建立估驗期別
├── 期別編號
├── 當期完成數量
├── 當期完成金額
├── 累計完成數量與金額
├── 扣款／保留／調整
├── 工程進度
├── 彙總
├── 圖表
├── 匯入／匯出
├── 報表前檢查
└── 估驗報表
```

主要源碼：
- `Invoice/FormInvoice.cs`
- `FormInvoiceProgress.cs`
- `FormInvoiceSummary.cs`
- `FormInvoiceDec2.cs`
- `FormInvoiceIndexNumber.cs`
- `FormInvoiceGraphic.cs`
- `FormInvoiceImport.cs`
- `FormInvoiceExport.cs`
- `Report/FormInvReportCheck.cs`
- `Report/FormInvoiceReport.cs`
- `Report/ucSubAcc.cs`

### D. 結算

```text
結算入口
├── 專案資格與 F012 權限
├── 讀取契約及歷次變更
├── 讀取累計估驗
├── 輸入最終數量／金額
├── 維護結算資訊
├── 結算差異
├── 確認最終值
└── 結算報表
```

主要源碼：
- `SubClose/FormSubClose.cs`
- `FormSubCloseInput.cs`
- `FormSubCloseInfo.cs`
- `Report/ucSubClose.cs`

### E. 驗收

```text
驗收入口
├── 選取可驗收專案
├── 讀取契約與結算資料
├── 選取驗收項目
├── 輸入驗收結果
├── 記錄缺失／處理狀態
├── 完成或退回
├── 最終狀態
└── 驗收報表
```

主要源碼：
- `SubFinal/FormSubFinal.cs`
- `FormSubFinalInput.cs`
- `FormSubFinal_ItemPick.cs`
- `Report/ucSubFinal.cs`

## 3. 共用資料關聯要求

Web 版至少需保留以下關聯概念：

```text
Project
└── Contract
    ├── ContractItem
    ├── ChangeIssue[]
    │   └── ChangeItem[]
    ├── InvoicePeriod[]
    │   ├── InvoiceItem[]
    │   └── Deduction[]
    ├── Closeout
    │   └── CloseoutItem[]
    └── FinalAcceptance
        └── AcceptanceItem[]
```

此為遷移模型建議；實際欄位與約束在開發前必須重新讀取 C# DomainModule 及資料查詢。

## 4. 狀態機要求

每個模組不可只用 `draft/completed` 兩種狀態。實作前需從源碼確認：

- 是否可新增下一期估驗。
- 是否可修改歷史期別。
- 變更生效後如何影響後續估驗。
- 結算後是否禁止新增估驗或變更。
- 驗收完成後是否封存契約。
- 取消、刪除與回退是否需要重算後續資料。

## 5. 計算與追溯要求

- 契約金額須能追溯至來源預算／發包工項。
- 變更金額須分別保存變更前、增減與變更後結果。
- 估驗須保存當期、前期累計、本期累計及剩餘值。
- 結算須保存契約／變更／估驗／最終結算的差異。
- 驗收須引用最終有效契約與結算資料。
- 所有數量與金額使用 Decimal／Numeric，並保留 Legacy 精度規則。
