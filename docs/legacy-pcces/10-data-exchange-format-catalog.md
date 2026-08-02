# PCCES C# Legacy：資料交換格式目錄

更新日期：2026-08-02

本文件整理桌面版已發現的匯入、匯出與附件格式。它是功能盤點，不是格式規格書；正式復刻前仍需逐一取得欄位、Schema、版本及錯誤案例。

## 1. 格式總覽

| 格式／通道 | 方向 | 主要用途 | 主要源碼 | 狀態 |
|---|---|---|---|---|
| XML（新版） | 匯入／匯出 | 預算、投標或專案電子資料 | `XMLClass/*`、`formNewProjectWizard.cs`、`Conversion.cs` | 已發現 |
| XML（舊版） | 匯入 | 舊 PCCES 電子檔相容 | `IsOldXML`、`ImportXMLInOldWay` | 已發現 |
| ZMD | 匯入 | 壓縮式預算電子檔 | `formNewProjectWizard.cs`、`MyZip` | 已發現 |
| MDB／Access | 匯入 | ZMD 或舊系統資料內容 | `CommonMethods.ImportAccess` | 已發現 |
| Excel | 匯入／匯出 | 預算、資源、分析、比較及報表 | `ExportExcel/*`、Aspose.Cells、C1Excel | 已發現 |
| PX 類電子檔 | 匯入 | 專案／預算電子資料 | `formNewProjectWizard.cs` | 待確認副檔名與版本 |
| BID Add-on | 匯入 | 將投標資料附加至既有工作流程 | `formNewProjectWizard.cs`、`FunctionButtons.cs` | 已發現 |
| 空白電子標單 | 匯出 | 提供投標填寫／發包 | `FormBudgetExp_Wzd.cs`、Bid／Conversion | 已發現 |
| MRS 資料 | 匯出 | 基本工料機及分析資料 | `FormMrsBase_ExpWizard.cs` | 已發現 |
| PDF | 匯出 | 固定版面正式報表 | Report／Viewer／Export 類別 | 待深讀 |
| 實體列印 | 輸出 | 正式紙本報表 | Crystal Viewer／Print 類別 | 已發現 |
| AddOn 文件 | 匯入／下載 | 專案附件與附加文件 | `AddOnDownLoad`、精靈搬移流程 | 已發現 |
| 報表網路下載 | 下載 | 背景取得報表或附件 | `Report.WebDownload/*` | 已發現 |

## 2. XML 系列

### FORMAT-XML-001 新版 XML

- 功能：由目前 XML 路徑解析電子專案資料。
- 入口：`ImportXML(AppName)`。
- Domain：`Project.InputXML` 及 XMLClass 相關類別。
- 需保留：Project、Items、Tenderer、來源種類、頁面標記及版本欄位。
- 實作前確認：Namespace、XSD／Schema、編碼、日期格式、Decimal 精度、缺欄位處理。

### FORMAT-XML-002 舊版 XML

- 功能：辨識舊格式後走獨立相容路徑。
- 入口：`IsOldXML()`、`ImportXMLInOldWay()`。
- Web 要求：不得將舊 XML 直接交給新版 parser；需保留 adapter／migration 層。

## 3. ZMD／MDB 系列

### FORMAT-ZMD-001 ZMD 容器

- 功能：解壓電子檔並取得 MDB 內容。
- 已確認行為：
  - 使用 `MyZip`。
  - 驗證壓縮內容非空。
  - 驗證第一個主要內容為 MDB。
  - 解壓失敗、電子檔損毀及內容錯誤分開提示。
- Web 要求：上傳後先進 sandbox／暫存區，不得直接寫正式專案。

### FORMAT-MDB-001 Access DataSet

- 功能：將 MDB 轉為 DataSet，再交由 Domain 匯入。
- 已確認相容處理：舊檔可能缺少 `CloseBidDate`、`CheckOut` 等欄位，匯入前會補預設值。
- Web 要求：建立 Legacy MDB Adapter，輸出結構化 validation 結果。

### FORMAT-MDB-002 發包認證與來源識別

- 功能：按 PID／用途檢查 `PccCodeCert`，並讀取 `srcKind` 或檔名尾碼判斷來源。
- Web 要求：用途不符時必須阻擋，不能只發 warning 後繼續匯入。

## 4. Excel 系列

### FORMAT-XLS-001 預算 Excel 匯入

- 功能：將既有預算 Excel 轉入專案。
- 主要源碼：`formNewProjectWizard.cs`、Excel parser 類別。
- 待確認：模板版本、工作表名稱、標題列、空白列、合併儲存格及公式處理。

### FORMAT-XLS-002 預算與報表 Excel 匯出

- 功能：輸出預算、資源、單價分析、比較及履約資料。
- 技術：Aspose.Cells、C1.C1Excel、`DomainModule.ExportExcel/*`。
- Web 要求：每種輸出建立獨立 report contract，不以通用 dataframe dump 取代。

### FORMAT-XLS-003 格式與公式相容

- 需盤點：
  - 欄寬與列高
  - 合併儲存格
  - 數字格式與小數位
  - Excel 公式
  - 標題、頁首頁尾
  - 分頁與列印範圍
  - 隱藏欄列
  - 多工作表關係

## 5. 投標與電子標單

### FORMAT-BID-001 空白電子標單

- 功能：由預算產生供投標填寫的資料。
- 來源：`FormBudgetExp_Wzd.cs`、`Conversion.cs`、Bid Domain。
- 需保留：工項層級、代碼、名稱、單位、數量及可填欄位。

### FORMAT-BID-002 投標資料附加匯入

- 功能：以 `_IniMode="2"`、`_IsAddOn="BID"` 類模式進入附加匯入。
- 完成後：刷新專案資料並定位新／更新結果。
- Web 要求：命令需明確區分 create-new、attach-bid、replace、merge。

### FORMAT-BID-003 預算併標

- 功能：組合多來源預算或標單。
- 來源：`FormBudgetCombineBid.cs`、`ucBudgetCombineBid.cs`。
- 實作前確認：來源排序、代碼衝突、重複項目、資源合併及輸出 ProjectCode。

## 6. MRS 與資源交換

### FORMAT-MRS-001 基本工料機匯出

- 功能：匯出基本工料機、分類與分析資料。
- 來源：`FormMrsBase_ExpWizard.cs`。
- 待確認：可選範圍、過濾條件、資料關聯及匯入對應。

### FORMAT-MRS-002 專案資源匯出

- 功能：匯出專案資源彙總、價格、匯率與代碼資訊。
- 來源：`FormBudgetRes.cs`。

## 7. 附件與報表下載

### FORMAT-ADDON-001 專案附件

- 功能：匯入電子檔後，將附件搬移至資料庫／ProjectCode 對應 AddOn 目錄。
- 來源：`formNewProjectWizard.cs`、`AddOnDownLoad`。
- Web 要求：附件應有檔案 metadata、hash、來源匯入 session、權限與生命週期。

### FORMAT-DOWNLOAD-001 非同步下載

- 功能：以 RequestState、DownloadThread 與 progress／complete／fail handler 管理下載。
- Web 要求：映射為 job id、狀態、進度、錯誤碼、產出檔案與到期時間。

## 8. 共通驗證模型

所有匯入格式應統一產生：

```text
ImportInspection
├── detected_format
├── detected_version
├── source_kind
├── project_identity
├── schema_errors
├── business_errors
├── warnings
├── attachments
├── proposed_actions
└── can_commit
```

所有匯出格式應統一保存：

```text
ExportJob
├── report_or_format_type
├── project_code
├── action
├── issue_or_version
├── options
├── requested_by
├── validation_snapshot
├── source_snapshot
├── result_file
└── status
```

## 9. 完成標準

本目錄標記「已發現」只代表源碼入口存在。各格式只有完成以下項目才可標記 `LEGACY_MATCHED`：

1. 樣本檔與版本已收集。
2. 欄位／Schema 已列出。
3. 驗證及錯誤訊息已對照。
4. 匯入後資料副作用已確認。
5. 匯出檔內容與版面已比對。
6. 新舊系統 round-trip 或 golden file 測試通過。
