# PCCES C# Legacy：報表、列印、預覽與下載功能摘要

更新日期：2026-08-02

本文件先建立全系統報表功能地圖。報表版型、參數、欄位、公式與分頁規則於實作對應 Web 報表前再逐一深讀。

## 1. 報表平台

### REPORT-PLATFORM-001 統一報表檢視器

- 摘要：桌面版有獨立報表檢視視窗，負責載入、顯示與操作報表。
- 主要源碼：
  - `PCCES_CS/Archnowledge.Pcces.PccesMain.Report/FormReportViewer.cs`
  - `PCCES_CS/Archnowledge.Pcces.PccesMain.Report/ucCrystalViewer.cs`
- 技術：Crystal Reports／報表 wrapper。
- Web 復刻重點：需建立統一 preview／render／download 服務，而不是每頁各自拼報表。
- 優先級：P0。

### REPORT-PLATFORM-002 Crystal Report 相容層

- 摘要：舊版正式報表依賴 Crystal Viewer 與 `.rpt` 類資源。
- 主要源碼：`Report/*`、`ucCrystalViewer.cs`、專案中的 `.rpt`／報表資源。
- Web 復刻重點：先盤點報表參數、資料集與版面，再決定 HTML／PDF／Excel 替代方式。
- 優先級：P0。

### REPORT-PLATFORM-003 報表參數與專案上下文

- 摘要：報表需接收 ProjectCode、Action、Issue、契約／估驗期別及使用者等上下文。
- 主要源碼：各報表 Form／UserControl 與呼叫端。
- Web 復刻重點：報表請求應保存 report type、來源版本、參數與產出時間。
- 優先級：P0。

## 2. 預算與投標報表

### REPORT-BUD-001 預算總表

- 摘要：輸出專案預算總額及主要分類彙總。
- 來源入口：`frmBudget` 報表功能、Report namespace。
- 待深讀：欄位、層級、稅費與加減項呈現。
- 優先級：P0。

### REPORT-BUD-002 預算詳細表

- 摘要：依預算樹輸出章、節、工項、數量、單價與複價。
- 來源入口：`frmBudget`、GridBudget、Report 類別。
- 優先級：P0。

### REPORT-BUD-003 單價分析表

- 摘要：輸出工項及其工料機分析組成、用量、單價與複價。
- 來源入口：`FormMrsBaseBreakdown.cs`、預算報表入口。
- 優先級：P0。

### REPORT-BUD-004 資源統計表

- 摘要：輸出人工、材料、機具等資源彙總與價格資訊。
- 來源入口：`FormBudgetRes.cs`、Report／Excel 相關類別。
- 優先級：P0。

### REPORT-BUD-005 空白電子標單／投標輸出

- 摘要：產出供投標填寫或發包使用的標單資料與文件。
- 來源入口：`FormBudgetExp_Wzd.cs`、Bid／Conversion 類別。
- 優先級：P0。

## 3. 契約與履約報表

### REPORT-SUB-001 契約／分包報表

- 摘要：輸出契約基本資料、契約項目與金額。
- 主要源碼：
  - `PCCES_CS/Archnowledge.Pcces.PccesMain.Report/ucSubCtr.cs`
  - `SplitContract/FormSplitContract.cs`
- 優先級：P0。

### REPORT-SUB-002 契約變更報表

- 摘要：輸出變更前後項目、數量、單價、金額及版本差異。
- 主要源碼：
  - `PCCES_CS/Archnowledge.Pcces.PccesMain.Report/ucSubChg.cs`
  - BudgetChange／SubChg 類別
- 優先級：P0。

### REPORT-INV-001 估驗計價報表

- 摘要：按估驗期別輸出本期、累計、扣款、保留及應付資料。
- 主要源碼：
  - `PCCES_CS/Archnowledge.Pcces.PccesMain.Report/FormInvoiceReport.cs`
  - `PCCES_CS/Archnowledge.Pcces.PccesMain.Report/FormInvReportCheck.cs`
  - `Invoice/FormInvoice.cs`
- Web 復刻重點：產出前需有報表檢核，並固定期別資料快照。
- 優先級：P0。

### REPORT-INV-002 估驗報表檢查

- 摘要：估驗報表產出前有獨立檢查畫面，表示資料完整性會阻擋或警告輸出。
- 主要源碼：`FormInvReportCheck.cs`。
- 優先級：P0。

### REPORT-CLOSE-001 結算報表

- 摘要：輸出最終結算數量、金額與契約差異。
- 主要源碼：
  - `PCCES_CS/Archnowledge.Pcces.PccesMain.Report/ucSubClose.cs`
  - `SubClose/FormSubClose.cs`
- 優先級：P0。

### REPORT-FINAL-001 驗收報表

- 摘要：輸出驗收、最終狀態及可能的缺失／處理資料。
- 主要源碼：
  - `PCCES_CS/Archnowledge.Pcces.PccesMain.Report/ucSubFinal.cs`
  - `SubFinal/FormSubFinal.cs`
- 優先級：P0。

### REPORT-ACC-001 履約累計／會計相關報表

- 摘要：`ucSubAcc` 顯示系統另有契約／履約累計或會計向報表節點。
- 主要源碼：`PCCES_CS/Archnowledge.Pcces.PccesMain.Report/ucSubAcc.cs`。
- 狀態：用途名稱需實作前深讀確認，不自行擴張解釋。
- 優先級：P1。

## 4. 輸出與下載

### OUTPUT-001 列印預覽

- 摘要：透過統一 Viewer 顯示正式版面，提供列印前預覽。
- 主要源碼：`FormReportViewer.cs`、`ucCrystalViewer.cs`。
- 優先級：P0。

### OUTPUT-002 實體列印

- 摘要：將報表送至印表機，需保留紙張、方向、頁面與印表設定。
- 主要源碼：Crystal Viewer、Print／Shell 類別、各報表 wrapper。
- 優先級：P1。

### OUTPUT-003 PDF 輸出

- 摘要：正式報表需支援可保存／傳遞的 PDF 或等價固定版面格式。
- 來源：報表 Viewer、Export／Shell 類別。
- 待深讀：桌面版實際 PDF 分支與錯誤處理。
- 優先級：P0。

### OUTPUT-004 Excel 輸出

- 摘要：預算、資源、單價分析與比較資料可輸出 Excel。
- 主要源碼：`DomainModule.ExportExcel/*`、Aspose.Cells、C1Excel 使用處。
- Web 復刻重點：版面與公式需按各報表逐一驗證，不可只輸出原始 CSV。
- 優先級：P0。

### OUTPUT-005 報表網路下載

- 摘要：系統有獨立報表下載執行緒、進度、完成及失敗處理。
- 主要源碼：
  - `Report.WebDownload/RequestState.cs`
  - `DownloadThread.cs`
  - `DownloadProgressHandler.cs`
  - `DownloadCompleteHandler.cs`
  - `DownloadFailHandler.cs`
- Web 復刻重點：長時間產出應採 job 狀態、進度與可重試下載，不應讓單一 HTTP 請求無限等待。
- 優先級：P1。

## 5. 報表功能樹

```text
報表中心
├── 預算／投標
│   ├── 預算總表
│   ├── 預算詳細表
│   ├── 單價分析表
│   ├── 資源統計表
│   └── 空白電子標單／投標輸出
├── 契約履約
│   ├── 契約／分包
│   ├── 契約變更
│   ├── 估驗計價
│   ├── 結算
│   ├── 驗收
│   └── 履約累計／會計向報表
├── 平台
│   ├── 報表參數
│   ├── Crystal Viewer
│   ├── 報表檢查
│   └── 預覽
└── 輸出
    ├── 列印
    ├── PDF
    ├── Excel
    └── 網路下載／進度／失敗重試
```

## 6. 實作前深讀清單

1. `FormReportViewer.cs`
2. `ucCrystalViewer.cs`
3. `FormInvoiceReport.cs`
4. `FormInvReportCheck.cs`
5. `ucSubCtr.cs`
6. `ucSubChg.cs`
7. `ucSubAcc.cs`
8. `ucSubClose.cs`
9. `ucSubFinal.cs`
10. `Report.WebDownload/*`
11. 所有 `.rpt` 與報表資料集／wrapper
12. `ExportExcel/*` 與各模組報表呼叫入口

本文件只確認報表節點與主要源碼位置；各報表內容、版型及計算規則尚需實作前逐項驗證。