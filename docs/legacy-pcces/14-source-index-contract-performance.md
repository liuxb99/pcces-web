# PCCES C# 契約與履約源碼索引

更新日期：2026-08-02

本索引提供契約、變更、估驗、結算與驗收功能的快速定位。正式復刻前，須重新讀取對應源碼確認事件、欄位與 Domain 規則。

## 1. 契約／分包

| 功能 | 主要源碼 | 用途摘要 |
|---|---|---|
| 契約工作台 | `Archnowledge.Pcces.PccesMain.SplitContract/FormSplitContract.cs` | 契約建立、預算項目關聯與契約工作上下文 |
| 契約入口 | `Budget/FormBudgetProjectPick.cs` | 按 `SplitContract` Action 選取專案 |
| 契約導航 | `ArchControls/FunctionButtons.cs` | `F009` 權限、表單切換與重用 |
| 契約 Domain | `DomainModule.Sub/*` | 契約資料與業務邏輯 |
| 契約報表 | `Report/ucSubCtr.cs` | 契約正式報表入口 |

## 2. 預算／契約變更

| 功能 | 主要源碼 | 用途摘要 |
|---|---|---|
| 變更工作台 | `BudgetChange/FormBudgetChange.cs` | 變更版本、工項與金額管理 |
| 新增變更 | `BudgetChange/FormBudgetChange_Addnew.cs` | 建立變更案 |
| 變更資訊 | `Budget.BudgetChange/FormBudgetChangeInfo.cs` | 變更案基本資料 |
| 變更挑選 | `Budget.BudgetChange/FormBudgetChangeInfoPicker.cs` | 選取變更資訊／版本 |
| 責任歸屬 | `Budget.BudgetChange/FormBudgetChangeResponsibility.cs` | 變更原因或責任分類 |
| 變更歷史 | `Budget.BudgetChange/FormBudgetChangeHistory.cs` | 查詢前後差異及歷程 |
| 契約變更 Domain | `DomainModule.SubChg/*` | 契約變更資料與邏輯 |
| 變更報表 | `Report/ucSubChg.cs` | 變更正式報表入口 |

## 3. 估驗計價

| 功能 | 主要源碼 | 用途摘要 |
|---|---|---|
| 估驗工作台 | `Invoice/FormInvoice.cs` | 估驗期別、當期與累計資料主畫面 |
| 進度 | `Invoice/FormInvoiceProgress.cs` | 工程／估驗進度管理 |
| 彙總 | `Invoice/FormInvoiceSummary.cs` | 當期、累計、剩餘金額彙總 |
| 扣款／調整 | `Invoice/FormInvoiceDec2.cs` | 扣款或減項資料 |
| 期別編號 | `Invoice/FormInvoiceIndexNumber.cs` | 估驗期別與索引編號 |
| 圖表 | `Invoice/FormInvoiceGraphic.cs` | 進度與金額圖形分析 |
| 匯入 | `Invoice/FormInvoiceImport.cs` | 估驗資料匯入 |
| 匯出 | `Invoice/FormInvoiceExport.cs` | 估驗資料匯出 |
| 報表前檢查 | `Report/FormInvReportCheck.cs` | 正式輸出前資料檢核 |
| 估驗報表 | `Report/FormInvoiceReport.cs` | 報表選項與輸出 |
| 累計報表 | `Report/ucSubAcc.cs` | 估驗累計報表 |

## 4. 結算

| 功能 | 主要源碼 | 用途摘要 |
|---|---|---|
| 結算工作台 | `SubClose/FormSubClose.cs` | 最終結算資料主畫面 |
| 結算輸入 | `SubClose/FormSubCloseInput.cs` | 最終數量與金額輸入 |
| 結算資訊 | `SubClose/FormSubCloseInfo.cs` | 結算基本資料與狀態 |
| 結算報表 | `Report/ucSubClose.cs` | 結算正式報表 |

## 5. 驗收

| 功能 | 主要源碼 | 用途摘要 |
|---|---|---|
| 驗收工作台 | `SubFinal/FormSubFinal.cs` | 最終驗收主畫面 |
| 驗收輸入 | `SubFinal/FormSubFinalInput.cs` | 驗收結果與狀態輸入 |
| 驗收項目挑選 | `SubFinal/FormSubFinal_ItemPick.cs` | 選取驗收對象或契約項目 |
| 驗收報表 | `Report/ucSubFinal.cs` | 驗收正式報表 |

## 6. 共用報表與導航

| 功能 | 主要源碼 | 用途摘要 |
|---|---|---|
| 報表 Viewer | `Report/FormReportViewer.cs` | 共用報表容器 |
| Crystal Viewer | `Report/ucCrystalViewer.cs` | Crystal Reports 顯示 |
| 專案 Action 選取 | `Budget/FormBudgetProjectPick.cs` | 契約、估驗、變更、結算、驗收入口 |
| 功能權限與切換 | `ArchControls/FunctionButtons.cs` | `F009`～`F012` 與 Action 啟動 |

## 7. 待實作前深讀項目

- `FormSplitContract` 的契約識別、項目挑選、保存與刪除事件。
- `FormBudgetChange` 的 Issue／版本建立及原工項對應規則。
- `FormInvoice` 的期別建立、鎖定、累計、扣款與付款公式。
- `FormSubClose` 的結算前置條件及與最後一期估驗的關係。
- `FormSubFinal` 的驗收狀態、缺失及封存條件。
- 各 `ucSub*` 報表參數、資料集與排序規則。
