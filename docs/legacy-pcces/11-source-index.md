# PCCES C# 源碼索引

更新日期：2026-08-02

本文件提供「功能節點 → C# 源碼」快速定位。先記主要入口與用途；實作前再沿呼叫鏈深讀。

## 1. 主程式與平台

| 功能 | 主要源碼 | 用途摘要 |
|---|---|---|
| 程式啟動 | `Archnowledge.Pcces.PccesMain/frmPccesMain.cs` | 啟動、主框架、面板、關閉與全域狀態 |
| 登入 | `FormLogin` | 帳密、匿名、管理員初始化與登入分流 |
| 首頁面板 | `FormPanel.cs`、`FormPanel2.cs`、`FormPanel3.cs` | 首頁功能入口與模組顯示 |
| 功能導航 | `Archnowledge.Pcces.PccesMain.ArchControls/FunctionButtons.cs` | 權限、模組入口、表單重用與上下文切換 |
| 線上狀態 | `OnlineList` | 線上使用者與工作狀態 |
| 系統管理 | `Archnowledge.Pcces.PccesMain.SysMaintain/*` | 使用者、權限、參數與維護 |

## 2. 專案管理

| 功能 | 主要源碼 | 用途摘要 |
|---|---|---|
| 專案目錄 | `Archnowledge.Pcces.PccesMain.Project/FormProject.cs` | 專案清單、篩選、能力與操作入口 |
| 新建／匯入精靈 | `Archnowledge.Pcces.PccesMain.Project/formNewProjectWizard.cs` | 建立、匯入、分拆與回滾 |
| 專案 Domain | `BUDClass.Project`、`BUDClass.PubProject` | 建立、匯入、查詢、刪除與專案資料 |
| 專案權限 | `DBClass.GetProjectAuthority`、`ProjAuthority` | 使用者與專案存取控制 |
| 專案選取 | `Budget/FormBudgetProjectPick.cs` | Action 對應候選專案與工作入口 |

## 3. 預算與投標

| 功能 | 主要源碼 | 用途摘要 |
|---|---|---|
| 預算工作台 | `Archnowledge.Pcces.PccesMain.Budget/frmBudget.cs` | BUD／BID 主畫面、Grid、重算、鎖定與保存 |
| 工項編輯 | `Budget/FormBudgetEditMain.cs` | 工項欄位、類型切換、驗證與保存 |
| B 類 | `Budget.BDGT_Component/B_Form.cs` | 下層自動累算 |
| L 類 | `Budget.BDGT_Component/L_Form.cs` | 直接輸入單價 |
| F 類 | `Budget.BDGT_Component/F_Form.cs` | 加總來源乘費率 |
| S 類 | `Budget.BDGT_Component/S_Form.cs`、`S_Form2.cs` | 分段計價與公式 |
| U 類 | `Budget.BDGT_Component/U_Form.cs` | 自訂公式與變數 |
| Z 類 | `Budget.BDGT_Component/Z_Form.cs` | 加總項目 |
| 預算資源 | `Budget/FormBudgetRes.cs` | 專案工料機資源彙總與處理 |
| 自我檢查 | `Budget/FormBudgetSelfExam.cs` | 預算檢查與問題清單 |
| 第三方資料 | `Budget/FormBudgetThirdParty.cs` | 第三方或外部資料引用 |
| 跨專案工項 | `Budget/FormPickProjWkItem_Wzd.cs` | 從其他專案挑選工項 |
| 預算分拆 | `Budget/FormBudgetSplit.cs` | 預算分拆相關流程 |
| 預算 Domain | `BUDClass.ItemA`、`ItemB`、`ItemC`、`PCals` | 工項、加總來源、分段與計算 |

## 4. MRS Base 與單價分析

| 功能 | 主要源碼 | 用途摘要 |
|---|---|---|
| 基本工料機 | `Archnowledge.Pcces.PccesMain.MrsBase/*` | 工項、材料、人工與機具資料庫 |
| 單價分析 | `MrsBase/FormMrsBaseBreakdown.cs` | 工項工料機組成與分析 |
| 新增分析項 | `MrsBase/FormMrsBaseBreakdown_Addnew.cs` | 新增分析明細 |
| 書籤 | `MrsBase.Bookmark/*` | 收藏、移除與快速引用 |
| MRS Domain | `MrsBaseA`、`ProjMrsA` | 基本庫與專案資源資料 |

## 5. 成本結構與轉換

| 功能 | 主要源碼 | 用途摘要 |
|---|---|---|
| 成本結構 | `DomainModule.CostStructure/*`、`CostKind` | 成本分類與費用結構 |
| 預算／標單轉換 | `Conversion.cs` | 預算、投標與交換格式轉換 |
| Excel | `DomainModule.ExportExcel/*` | Excel 匯出與格式處理 |
| XML | `XML/*`、`XMLClass/*` | XML 匯入、匯出與相容處理 |
| ZMD／MDB | `formNewProjectWizard.cs`、`CommonMethods.ImportAccess` | 壓縮電子檔與 Access 資料匯入 |

## 6. 契約與履約

| 功能 | 主要源碼 | 用途摘要 |
|---|---|---|
| 契約／分包 | `Archnowledge.Pcces.PccesMain.SplitContract/FormSplitContract.cs` | 契約建立與預算項目關聯 |
| 預算變更 | `Archnowledge.Pcces.PccesMain.BudgetChange/FormBudgetChange.cs` | 變更版本、增減項與差異 |
| 契約變更 | `DomainModule.SubChg/*`、相關 Form | 契約變更處理 |
| 估驗計價 | Invoice namespace／Form | 期別、當期、累計與扣款 |
| 結算 | `FormSubClose` | 最終數量與金額結算 |
| 驗收 | `FormSubFinal` | 驗收、缺失與最終狀態 |

## 7. 報表與輸出

| 功能 | 主要源碼 | 用途摘要 |
|---|---|---|
| 預算報表 | Budget 報表入口、Report 類別 | 總表、詳細表、分析表與資源表 |
| 履約報表 | Contract／Invoice／Close／Final Report 類別 | 契約、估驗、結算與驗收輸出 |
| Crystal Reports | `.rpt` 與 Report wrapper | 舊版正式報表格式 |
| Excel | `C1.C1Excel` 使用處、`ExportExcel` | Excel 格式、公式與分頁 |
| PDF／列印 | ShellLib、Print／Preview 類別 | 預覽、列印與 PDF 輸出 |

## 8. 共用與基礎設施

| 類別／namespace | 用途摘要 |
|---|---|
| `CommonMethods` | 共用轉換、INI、檔案、IP、日誌與輔助方法 |
| `DBClass` | 權限、SQL、使用者定義與資料庫共用操作 |
| `PubTools` | 數值、公式、日誌與應用設定 |
| `ModuleManager` | 模組啟用狀態 |
| `PccesFormAction` | BUD、BID、契約、估驗、變更、結算、驗收等 Action |
| `ModifyDB` | 低階資料新增、更新與刪除 |
| `AddOnDownLoad` | AddOn／附件資料與資料庫名稱處理 |
| `SysUser` | 使用者對應資料庫與系統資訊 |

## 9. 待掃描清單

以下需繼續列出實際檔名與掛入功能樹：

- Invoice 全部 Form／UserControl
- SubClose／SubFinal 全部檔案
- SysMaintain 全部子表單
- Database Upgrade／Change 類別
- Conversion 全部相關類別
- Report／Crystal Report 全部入口
- ExportExcel 全部分支
- MrsBase 全部維護與比較表單
- AddOn、Proxy、Update、Registration 全部類別

## 10. 使用規則

1. 功能樹節點先查本索引定位源碼。
2. 實作前必須重新讀取該功能相關源碼，不能只依摘要寫程式。
3. 新發現的 Form、UserControl 或 Domain 類別必須補入索引。
4. 無法確認用途的檔案標記 `UNKNOWN`，不得自行命名業務行為。
