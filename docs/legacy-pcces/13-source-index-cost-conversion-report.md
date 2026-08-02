# PCCES C# 源碼索引：成本結構、轉換、匯出與報表

更新日期：2026-08-02

本索引提供「功能節點 → 主要 C# 檔案」快速定位。正式實作前仍需重新閱讀對應源碼。

## 1. 成本結構

| 功能 | 主要源碼 | 用途摘要 |
|---|---|---|
| 成本結構類型選擇 | `SysMaintain/CostStructureTypePicker.cs` | 選取及辨識成本結構類型 |
| 成本結構匯入 | `SysMaintain/CostStructureImport.cs` | 匯入成本結構定義 |
| 專案成本結構選擇 | `Budget/FormBudgetCostStructurePicker.cs` | 將成本結構掛入預算專案 |
| 工項成本屬性 | `Budget/FormBudgetCostProperty.cs` | 設定工項成本分類或屬性 |
| 成本結構 Domain | `DomainModule.CostStructure/*` | 成本類型、結構及計算邏輯 |
| 成本類別 | `CostKind` | 工項與成本分類的 Domain 表達 |
| 系統維護入口 | `SysMaintain/FormSys_G.cs`、`FormSys_Z.cs` | 維護成本結構及相關參數 |

## 2. 預算／標單轉換

| 功能 | 主要源碼 | 用途摘要 |
|---|---|---|
| 預算匯出精靈 | `Budget/FormBudgetExp_Wzd.cs` | 多步驟輸出與轉換主流程 |
| 匯出選項 | `Budget/FormBudgetExp_WzdOption.cs` | 匯出格式及行為選項 |
| 匯出前檢查 | `Budget/FormBudgetExp_Wzd_SelfExamDiaglog.cs` | 顯示匯出阻擋錯誤與警告 |
| 匯出說明 | `Budget/FormBudgetExp_Wzd_Help1.cs` | 精靈說明與操作指引 |
| 預算併標 | `Budget/FormBudgetCombineBid.cs` | 多來源預算／標單合併入口 |
| 併標控制項 | `Budget/ucBudgetCombineBid.cs` | 併標清單與操作 UI |
| 預算／標單轉換核心 | `Conversion.cs` | BUD、BID 與交換格式轉換 |
| 投標 Domain | `DomainModule.Bid/*` | 投標資料與轉換行為 |
| 預算 Domain | `DomainModule.Budget/*` | 預算資料與轉換來源 |
| 投標附加匯入 | `Project/formNewProjectWizard.cs` | BID Add-on／回轉匯入 |

## 3. MRS 與資源匯出

| 功能 | 主要源碼 | 用途摘要 |
|---|---|---|
| MRS 匯出精靈 | `MrsBase/FormMrsBase_ExpWizard.cs` | 工料機／分析資料匯出 |
| 預算資源匯出 | `Budget/FormBudgetRes.cs` | 專案資源 Grid 與 Excel 輸出 |
| 單價分析匯出 | `MrsBase/FormMrsBaseBreakdown.cs` | 分析明細與 Excel 操作 |
| Excel Domain | `DomainModule.ExportExcel/*` | Excel 產出與格式處理 |

## 4. 匯入格式

| 格式／功能 | 主要源碼 | 用途摘要 |
|---|---|---|
| 新 XML | `formNewProjectWizard.cs`、`XMLClass/*` | 新版 XML 匯入與 Domain 轉換 |
| 舊 XML | `IsOldXML`、`ImportXMLInOldWay` | 舊版 XML 相容路徑 |
| ZMD | `formNewProjectWizard.cs`、`MyZip` | 解壓、內容驗證與 MDB 匯入 |
| MDB／Access | `CommonMethods.ImportAccess` | Access 資料表轉 DataSet |
| Excel 匯入 | `formNewProjectWizard.cs`、Excel 相關類別 | 預算／專案資料匯入 |
| PX 類文件 | `formNewProjectWizard.cs` | PCCES 電子文件來源 |
| AddOn 附件 | `AddOnDownLoad`、精靈搬移流程 | 專案附件與下載資料處理 |

## 5. 報表平台

| 功能 | 主要源碼 | 用途摘要 |
|---|---|---|
| 統一報表 Viewer | `Report/FormReportViewer.cs` | 報表容器、預覽與操作 |
| Crystal Viewer 控制項 | `Report/ucCrystalViewer.cs` | 顯示 Crystal 報表 |
| 估驗報表 | `Report/FormInvoiceReport.cs` | 估驗期別報表入口 |
| 估驗報表檢查 | `Report/FormInvReportCheck.cs` | 產出前資料檢核 |
| 契約報表 | `Report/ucSubCtr.cs` | 契約／分包輸出 |
| 契約變更報表 | `Report/ucSubChg.cs` | 變更差異輸出 |
| 履約累計／會計報表 | `Report/ucSubAcc.cs` | 用途待實作前深讀 |
| 結算報表 | `Report/ucSubClose.cs` | 結算數量與金額輸出 |
| 驗收報表 | `Report/ucSubFinal.cs` | 驗收及最終狀態輸出 |

## 6. 報表下載與非同步處理

| 功能 | 主要源碼 | 用途摘要 |
|---|---|---|
| 下載狀態 | `Report.WebDownload/RequestState.cs` | 保存下載請求狀態 |
| 下載執行緒 | `Report.WebDownload/DownloadThread.cs` | 背景下載／產出流程 |
| 進度處理 | `DownloadProgressHandler.cs` | 回報進度 |
| 完成處理 | `DownloadCompleteHandler.cs` | 完成後處理 |
| 失敗處理 | `DownloadFailHandler.cs` | 錯誤與失敗回報 |

## 7. 外部函式庫與格式工具

| 技術／類別 | 用途摘要 |
|---|---|
| Crystal Reports | 正式報表版型與預覽 |
| Aspose.Cells | Excel 讀寫及格式 |
| C1.C1Excel | Excel Workbook 操作 |
| `DomainModule.ExportExcel` | 專案 Excel 匯出邏輯 |
| `MyZip` | ZMD 等壓縮電子檔解壓 |
| `CommonMethods.ImportAccess` | MDB／Access 匯入 |
| `XMLClass` | XML 交換格式 |
| `ShellLib` | 檔案、預覽、外部程序或列印相關共用能力 |

## 8. 尚待掃描的細目

- `Conversion.cs` 的全部公開方法與呼叫端。
- `DomainModule.CostStructure` 實際檔案清單。
- `DomainModule.ExportExcel` 實際檔案清單。
- 全部 `.rpt` 名稱與對應報表入口。
- 預算報表的各 Form／UserControl 檔名。
- 各格式版本號、檔案副檔名與相容條件。
- 各報表資料集、參數與分頁規則。

## 9. 實作使用規則

1. 先由功能樹找到節點，再由本索引定位 C# 檔案。
2. 實作前重新閱讀主檔、呼叫端及 Domain 類別。
3. 匯出與報表不得只依畫面名稱推測欄位。
4. 格式相容、版面與計算規則未驗證前不得標記 `LEGACY_MATCHED`。
