# PCCES C# Legacy：成本結構與資料轉換功能摘要

更新日期：2026-08-02

本文件採「功能樹優先」方式，先盤點成本結構、預算／標單轉換與相關匯出精靈；正式復刻前再回到對應 C# 源碼補事件鏈與欄位規則。

## 1. 成本結構功能樹

### COST-001 成本結構類型管理

- 摘要：選擇、辨識及管理可套用的成本結構類型。
- 主要源碼：
  - `PCCES_CS/Archnowledge.Pcces.PccesMain.SysMaintain/CostStructureTypePicker.cs`
  - `PCCES_CS/Archnowledge.Pcces.PccesMain.SysMaintain/FormSys_G.cs`
  - `DomainModule.CostStructure/*`
- 關聯資料：成本結構類型、費用分類、成本項目定義。
- Web 復刻重點：成本結構類型不可只做自由文字欄位，需保留可選類型與套用來源。
- 優先級：P0。

### COST-002 成本結構匯入

- 摘要：由外部或系統維護資料匯入成本結構定義，供基本工料及專案預算使用。
- 主要源碼：
  - `PCCES_CS/Archnowledge.Pcces.PccesMain.SysMaintain/CostStructureImport.cs`
  - `PCCES_CS/Archnowledge.Pcces.PccesMain.MrsBase/frmMrsBase.cs`
- 關聯模組：系統維護、基本工料機、預算編製。
- Web 復刻重點：需要匯入驗證、重複處理、版本來源及錯誤回報，不可只做資料表批次新增。
- 優先級：P0。

### COST-003 專案成本結構選取

- 摘要：在預算專案內選取適用的成本結構，作為費用分類及計算基礎。
- 主要源碼：
  - `PCCES_CS/Archnowledge.Pcces.PccesMain.Budget/FormBudgetCostStructurePicker.cs`
  - `PCCES_CS/Archnowledge.Pcces.PccesMain.Budget/frmBudget.cs`
- Web 復刻重點：成本結構選取需掛在 ProjectCode／Action／Issue 上，並可追蹤變更。
- 優先級：P0。

### COST-004 預算工項成本屬性

- 摘要：設定工項的成本屬性、成本分類或與成本結構的對應。
- 主要源碼：
  - `PCCES_CS/Archnowledge.Pcces.PccesMain.Budget/FormBudgetCostProperty.cs`
  - `FormBudgetEditMain.cs`
  - `CostKind`
- Web 復刻重點：工項成本屬性應為正式 Domain 欄位，不可只存在於前端顯示。
- 優先級：P0。

### COST-005 成本費用項目與加減項

- 摘要：成本分類中可能包含直接費、間接費、管理費、稅費、百分比項及加減項。
- 主要源碼：`DomainModule.CostStructure/*`、`CostKind`、`F_Form.cs`、`S_Form.cs`、`Z_Form.cs`。
- 說明：此節點目前只確認功能邊界；各費用項公式與排序於實作前深讀。
- 優先級：P0。

## 2. 預算與標單轉換功能樹

### CONV-001 預算匯出精靈

- 摘要：以多步驟精靈將預算資料輸出為指定電子格式。
- 主要源碼：
  - `PCCES_CS/Archnowledge.Pcces.PccesMain.Budget/FormBudgetExp_Wzd.cs`
  - `FormBudgetExp_WzdOption.cs`
  - `FormBudgetExp_Wzd_Help1.cs`
- 相關功能：輸出選項、格式說明、目標路徑、結果訊息。
- Web 復刻重點：應做成 export job／wizard，不應只提供單一下載按鈕。
- 優先級：P0。

### CONV-002 匯出前自我檢查

- 摘要：匯出前執行預算資料檢查，顯示阻擋錯誤或警告。
- 主要源碼：
  - `FormBudgetExp_Wzd_SelfExamDiaglog.cs`
  - `FormBudgetSelfExam.cs`
- Web 復刻重點：匯出 API 必須先跑 validation report，並區分 error、warning、可忽略問題。
- 優先級：P0。

### CONV-003 匯出選項

- 摘要：精靈中存在獨立選項畫面，代表輸出格式並非固定單一路徑。
- 主要源碼：`FormBudgetExp_WzdOption.cs`。
- 待深讀：選項名稱、預設值、格式對輸出內容的影響。
- 優先級：P1。

### CONV-004 預算併標

- 摘要：將多個預算／標單資料合併成發包或投標使用的標單資料。
- 主要源碼：
  - `PCCES_CS/Archnowledge.Pcces.PccesMain.Budget/FormBudgetCombineBid.cs`
  - `ucBudgetCombineBid.cs`
- Web 復刻重點：需有來源專案集合、合併順序、重複工項處理、ProjectCode 對應與結果專案。
- 優先級：P0。

### CONV-005 預算轉投標／發包資料

- 摘要：由 BUD 資料建立 BID 或發包交換資料，保留工項、分析、資源及頁面／發包標記。
- 主要源碼：
  - `Conversion.cs`
  - `DomainModule.Bid/*`
  - `FormBudgetExp_Wzd.cs`
  - `formNewProjectWizard.cs`
- Web 復刻重點：轉換必須是可追蹤的 Domain operation，保留來源版本與轉換結果。
- 優先級：P0。

### CONV-006 標單回轉與附加匯入

- 摘要：投標資料可由匯入精靈回轉或附加至既有專案。
- 主要源碼：
  - `formNewProjectWizard.cs`
  - BID Add-on 模式
  - `FunctionButtons` 投標匯入入口
- Web 復刻重點：建立／附加／覆蓋必須分成不同指令，不能共用模糊的 import endpoint。
- 優先級：P0。

## 3. 基本工料機匯出

### CONV-MRS-001 MRS 匯出精靈

- 摘要：基本工料機資料庫具有獨立匯出精靈。
- 主要源碼：`PCCES_CS/Archnowledge.Pcces.PccesMain.MrsBase/FormMrsBase_ExpWizard.cs`。
- Web 復刻重點：需區分工項、資源、分析明細及分類資料的輸出範圍。
- 優先級：P1。

## 4. 多格式資料交換

### FORMAT-001 XML 新舊格式

- 摘要：系統保留新 XML 與舊 XML 兩條匯入路徑。
- 主要源碼：`formNewProjectWizard.cs`、`XMLClass/*`、XML 相關 namespace。
- 優先級：P0。

### FORMAT-002 ZMD 壓縮電子檔

- 摘要：ZMD 解壓後讀取 MDB／DataSet，再進入 Domain 匯入。
- 主要源碼：`formNewProjectWizard.cs`、`MyZip`、`CommonMethods.ImportAccess`。
- 優先級：P0。

### FORMAT-003 MDB／Access

- 摘要：舊電子檔使用 Access 資料表，匯入時需補舊版缺少欄位並做 Schema Adapter。
- 主要源碼：`CommonMethods.ImportAccess`、`formNewProjectWizard.cs`。
- 優先級：P0。

### FORMAT-004 Excel

- 摘要：預算、資源與 MRS 模組均存在 Excel 輸入／輸出能力，使用 Aspose.Cells、C1Excel 或 ExportExcel Domain。
- 主要源碼：
  - `DomainModule.ExportExcel/*`
  - `FormBudgetExp_Wzd.cs`
  - `FormBudgetRes.cs`
  - `FormMrsBaseBreakdown.cs`
  - `FormMrsBase_ExpWizard.cs`
- Web 復刻重點：需盤點欄位順序、儲存格格式、公式、合併、分頁與版本相容性。
- 優先級：P0。

### FORMAT-005 附件與 AddOn 文件

- 摘要：電子檔匯入後可能將附加文件搬移至專案 AddOn 目錄。
- 主要源碼：`formNewProjectWizard.cs`、`AddOnDownLoad`。
- 優先級：P1。

## 5. 實作前深讀清單

開始復刻本模組前，至少重新讀取：

1. `CostStructureImport.cs`
2. `CostStructureTypePicker.cs`
3. `FormBudgetCostStructurePicker.cs`
4. `FormBudgetCostProperty.cs`
5. `FormBudgetExp_Wzd.cs`
6. `FormBudgetExp_WzdOption.cs`
7. `FormBudgetExp_Wzd_SelfExamDiaglog.cs`
8. `FormBudgetCombineBid.cs`
9. `ucBudgetCombineBid.cs`
10. `FormMrsBase_ExpWizard.cs`
11. `Conversion.cs` 及其 Domain 呼叫
12. XML／ExportExcel 相關類別

本文件完成的是功能發現與定位，不代表計算規則或交換格式已驗收。