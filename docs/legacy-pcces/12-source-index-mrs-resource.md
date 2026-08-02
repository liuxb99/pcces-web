# PCCES C# 源碼索引補充：MRS Base 與專案資源

更新日期：2026-08-02

| 功能節點 | 主要 C# 檔案／類別 | 快速用途 |
|---|---|---|
| MRS 基本資料庫 | `PCCES_CS/Archnowledge.Pcces.PccesMain.MrsBase/*` | 工項、人工、材料、機具、分類與資料庫維護 |
| 單價分析主畫面 | `PCCES_CS/Archnowledge.Pcces.PccesMain.MrsBase/FormMrsBaseBreakdown.cs` | 工項分析明細、比例、價格、重算、歷史與匯出 |
| 新增分析項 | `PCCES_CS/Archnowledge.Pcces.PccesMain.MrsBase/FormMrsBaseBreakdown_Addnew.cs` | 分類樹、資源搜尋、加入／移除分析明細 |
| 專案資源分析 | `PCCES_CS/Archnowledge.Pcces.PccesMain.Budget/FormBudgetRes.cs` | 資源彙總、引用工項、價格、代碼、重算及匯出 |
| 書籤 | `PCCES_CS/Archnowledge.Pcces.PccesMain.MrsBase.Bookmark/*` | 常用工項／資源收藏與移除 |
| 公共工料機 Domain | `Archnowledge.Pcces.BUDClass.MrsBaseA` | 公共工料機查詢、挑選及維護 |
| 專案資源 Domain | `Archnowledge.Pcces.DomainModule.LogicalBase.ProjMrsA` | 專案資源彙總與關聯 |
| 預算工項 Domain | `Archnowledge.Pcces.DomainModule.LogicalBase.ItemA`、`BUDClass.ItemA` | 預算工項及資源引用來源 |
| 代碼驗證 | `PCCES.CODECHECK.CodeValidator` | PCCES／資源代碼檢查 |
| 代碼補齊 | `PCCES.CODECHECK.CodeFitter` | 資源或工項代碼自動匹配／補齊 |
| 成本結構篩選 | `Archnowledge.Pcces.DomainModule.CostStructure.CostStructure` | 挑選資源時使用成本分類與限制 |
| Excel 匯出 | `Archnowledge.Pcces.DomainModule.ExportExcel/*`、Aspose Cells、C1Excel | 資源與單價分析表匯出 |
| 預算變更歷史 | BudgetChange 相關類別 | 顯示或套用單價分析變更歷史 |
| 歷史工率／價格 | `FormMrsBaseBreakdown`、`FormBudgetRes` 內相關 Combo 與 Domain | 歷史價格查詢及套用 |

## 實作時的源碼閱讀順序

```text
FormMrsBaseBreakdown_Addnew
→ MrsBaseA／CostStructure
→ FormMrsBaseBreakdown
→ ItemA／ProjMrsA
→ FormBudgetRes
→ frmBudget 重載與重算
```

## 需繼續發現的實際檔名

- MrsBase 主目錄／分類維護 Form。
- Bookmark namespace 全部表單。
- 歷史價格與比價表單。
- 資源代碼自動編碼相關表單。
- MRS Excel 匯入／匯出輔助類別。
- 公共庫、公司庫、專案庫切換相關類別。
