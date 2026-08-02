# PCCES C# 契約、變更、估驗、結算與驗收功能摘要

更新日期：2026-08-02

本文件採功能樹優先策略，先記錄主要功能、入口及源碼位置；實作 Web 對應模組前再深入閱讀事件鏈與資料規則。

## 1. 契約／分包

### CNT-001 契約編製入口
- 摘要：由專案選取器進入契約編製工作台，建立或開啟專案契約資料。
- 權限：`F009`。
- Action：`SplitContract`。
- 主要源碼：`SplitContract/FormSplitContract.cs`、`Budget/FormBudgetProjectPick.cs`、`ArchControls/FunctionButtons.cs`。
- 優先級：P0。

### CNT-002 預算工項轉契約項目
- 摘要：從預算／發包資料挑選工項，形成契約或分包項目及金額結構。
- 主要源碼：`FormSplitContract.cs`、`DomainModule.Sub/*`、`BUDClass.ItemA`。
- 優先級：P0。

### CNT-003 契約基本資料與狀態
- 摘要：管理契約識別、承攬資料、金額、日期、履約狀態與後續估驗／變更關聯。
- 主要源碼：`FormSplitContract.cs`、`DomainModule.Sub/*`。
- 優先級：P0。

### CNT-004 契約報表
- 摘要：契約資料對應正式報表及預覽。
- 主要源碼：`Report/ucSubCtr.cs`、`Report/FormReportViewer.cs`、`Report/ucCrystalViewer.cs`。
- 優先級：P1。

## 2. 預算變更與契約變更

### CHG-001 預算變更工作台
- 摘要：建立、開啟及管理變更版本，處理增項、減項、數量／單價調整與變更後金額。
- 權限：`F011`。
- Action：`BudgetChange`。
- 主要源碼：`BudgetChange/FormBudgetChange.cs`、`BudgetChange/FormBudgetChange_Addnew.cs`。
- 優先級：P0。

### CHG-002 變更基本資料
- 摘要：維護變更案資訊、變更次數、說明、日期及相關識別資料。
- 主要源碼：`Budget.BudgetChange/FormBudgetChangeInfo.cs`、`FormBudgetChangeInfoPicker.cs`。
- 優先級：P0。

### CHG-003 變更責任歸屬
- 摘要：記錄變更原因或責任分類，作為變更管理及報表資料。
- 主要源碼：`Budget.BudgetChange/FormBudgetChangeResponsibility.cs`。
- 優先級：P1。

### CHG-004 變更歷史
- 摘要：查詢工項或資源的變更歷程與前後差異。
- 主要源碼：`Budget.BudgetChange/FormBudgetChangeHistory.cs`、`MrsBase/FormMrsBaseBreakdown.cs`。
- 優先級：P0。

### CHG-005 契約變更 Domain
- 摘要：契約成立後的變更資料、版本與原契約／預算關聯。
- 主要源碼：`DomainModule.SubChg/*`、`FormBudgetChange.cs`。
- 優先級：P0。

### CHG-006 變更報表
- 摘要：輸出契約／預算變更的正式報表。
- 主要源碼：`Report/ucSubChg.cs`、`Report/FormInvoiceReport.cs`。
- 優先級：P1。

## 3. 估驗計價

### INV-001 估驗工作台
- 摘要：依契約建立估驗期別，管理當期與累計完成數量、金額及付款資料。
- 權限：`F010`。
- Action：`Invoice`。
- 主要源碼：`Invoice/FormInvoice.cs`、`Budget/FormBudgetProjectPick.cs`。
- 優先級：P0。

### INV-002 估驗期別與進度
- 摘要：管理估驗期別、工程進度及期別狀態。
- 主要源碼：`Invoice/FormInvoiceProgress.cs`、`FormInvoiceIndexNumber.cs`。
- 優先級：P0。

### INV-003 估驗彙總
- 摘要：計算當期、累計與剩餘金額，提供估驗總表。
- 主要源碼：`Invoice/FormInvoiceSummary.cs`。
- 優先級：P0。

### INV-004 扣款與調整
- 摘要：處理估驗扣款、保留、調整或其他減項資料。
- 主要源碼：`Invoice/FormInvoiceDec2.cs` 及 Invoice namespace 其他子表單。
- 優先級：P0。

### INV-005 估驗圖表
- 摘要：顯示期別進度或金額圖形分析。
- 主要源碼：`Invoice/FormInvoiceGraphic.cs`。
- 優先級：P1。

### INV-006 估驗匯入匯出
- 摘要：估驗資料的檔案匯入、匯出與交換。
- 主要源碼：`Invoice/FormInvoiceImport.cs`、`FormInvoiceExport.cs`。
- 優先級：P1。

### INV-007 估驗報表檢查與輸出
- 摘要：在正式輸出前檢查估驗報表資料，再透過報表平台產出。
- 主要源碼：`Report/FormInvReportCheck.cs`、`FormInvoiceReport.cs`、`ucSubAcc.cs`。
- 優先級：P0。

## 4. 結算

### CLOSE-001 結算工作台
- 摘要：彙整契約最終數量與金額，形成履約結算資料。
- 權限：`F012`。
- Action：`SubClose`。
- 主要源碼：`SubClose/FormSubClose.cs`。
- 優先級：P0。

### CLOSE-002 結算資料輸入
- 摘要：輸入或調整最終結算數量、金額及必要欄位。
- 主要源碼：`SubClose/FormSubCloseInput.cs`。
- 優先級：P0。

### CLOSE-003 結算基本資訊
- 摘要：維護結算案基本資料、日期、說明及狀態。
- 主要源碼：`SubClose/FormSubCloseInfo.cs`。
- 優先級：P0。

### CLOSE-004 結算報表
- 摘要：產生結算相關正式報表。
- 主要源碼：`Report/ucSubClose.cs`、`FormInvoiceReport.cs`。
- 優先級：P1。

## 5. 驗收

### FINAL-001 驗收工作台
- 摘要：在契約與結算基礎上建立最終驗收資料。
- Action：`SubFinal`。
- 主要源碼：`SubFinal/FormSubFinal.cs`。
- 優先級：P0。

### FINAL-002 驗收資料輸入
- 摘要：輸入驗收結果、日期、狀態、缺失或處理資料。
- 主要源碼：`SubFinal/FormSubFinalInput.cs`。
- 優先級：P0。

### FINAL-003 驗收項目挑選
- 摘要：從契約／結算項目中選取驗收對象或項目。
- 主要源碼：`SubFinal/FormSubFinal_ItemPick.cs`。
- 優先級：P0。

### FINAL-004 驗收報表
- 摘要：產生驗收正式報表。
- 主要源碼：`Report/ucSubFinal.cs`、`FormInvoiceReport.cs`。
- 優先級：P1。

## 6. 共用履約報表架構

### PERF-RPT-001 共用報表容器
- 摘要：契約、變更、估驗、結算與驗收報表共用 Viewer／Crystal Viewer。
- 主要源碼：`Report/FormReportViewer.cs`、`Report/ucCrystalViewer.cs`。

### PERF-RPT-002 履約報表控制項
- 契約：`ucSubCtr.cs`
- 變更：`ucSubChg.cs`
- 估驗累計：`ucSubAcc.cs`
- 結算：`ucSubClose.cs`
- 驗收：`ucSubFinal.cs`

## 7. Web 復刻時的共同要求

1. 契約、變更、估驗、結算與驗收必須以同一個穩定 `legacy_project_code` 及契約識別串接。
2. 每個階段要保留版本、狀態及前一階段來源資料，不可只以獨立 CRUD 表單實作。
3. 金額、數量、累計與剩餘值使用 Decimal／Numeric。
4. 後續實作前需再深讀各 Form 的事件、DomainModule 呼叫、交易及鎖定規則。
5. 所有正式報表必須保留欄位、排序、分組與計算結果相容性。
