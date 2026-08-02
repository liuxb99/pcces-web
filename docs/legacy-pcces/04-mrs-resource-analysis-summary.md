# PCCES C# 功能摘要：MRS Base、工料機與專案資源分析

更新日期：2026-08-02

本文件採「功能樹優先」方式，只記錄功能入口、用途與主要源碼。實作 Web 對應功能前，再回到所列 C# 類別補讀事件鏈、資料表及計算規則。

## 1. 功能樹

```text
MRS Base／工料機／單價分析
├── 基本工料機資料庫
│   ├── 分類樹瀏覽
│   ├── 工項／資源搜尋
│   ├── 類型與文字篩選
│   ├── 排序與欄位設定
│   ├── 中文／英文名稱及單位
│   ├── 工項代碼與 PCCES 代碼
│   └── 不同資料庫／公司資料來源切換
├── 單價分析
│   ├── 工項分析明細
│   ├── 人工／材料／機具／雜項比例
│   ├── 分析數量、單價與金額
│   ├── 新增分析項
│   ├── 移除分析項
│   ├── 層級向上／向下
│   ├── 重算與價格調整
│   ├── 歷史工率／價格
│   └── 變更歷史
├── 工料機挑選
│   ├── 分類樹
│   ├── 候選清單
│   ├── 已選清單
│   ├── 加入／移除
│   ├── 成本結構篩選
│   └── Action／專案上下文
├── 專案資源分析
│   ├── 專案資源彙總
│   ├── 資源與引用工項雙 Grid
│   ├── 關鍵字與進階條件搜尋
│   ├── 小計／全案重算
│   ├── 資源價格與匯率設定
│   ├── 自動編碼與代碼檢查
│   ├── 父專案資源引用
│   ├── 書籤加入
│   ├── Excel 匯出
│   └── 通知預算工作台重新載入
└── 收藏與快速引用
    ├── 新增書籤
    ├── 移除書籤
    ├── 工項／資源快速挑選
    └── 跨預算或父專案引用
```

## 2. 功能節點

### MRS-001 基本工料機資料庫

- 功能摘要：瀏覽與維護公共工程工項、人工、材料、機具及其他資源資料。
- 主要源碼：`PCCES_CS/Archnowledge.Pcces.PccesMain.MrsBase/*`。
- 主要 Domain：`MrsBaseA`、`DomainModule.MrsBase`。
- 關聯設定：`MrsBase.ini`、`OptionSet.ini`。
- Web 復刻時需再確認：分類代碼、資源種類、欄位權限、資料庫切換與新增／修改／刪除規則。

### MRS-002 單價分析主畫面

- 功能摘要：顯示工項的單價分析組成、分析數量、價格、總額與人工／材料／機具等比例，支援調整、重算、匯出及歷史資料。
- 主要源碼：`PCCES_CS/Archnowledge.Pcces.PccesMain.MrsBase/FormMrsBaseBreakdown.cs`。
- 主要控制：`gridMrsBase1`、`gridMrsBase2`、`txtAnalysisQty`、`txtPrice`、`BtnAdjust`、`chkReCalcu`。
- 關聯功能：Excel、Budget、BudgetChange、BusinessLogical、Report／Shell、歷史工率與變更記錄。
- Web 復刻時需再確認：Grid 兩層資料語意、比例計算、價格調整公式、重算觸發及鎖定條件。

### MRS-003 新增／挑選分析項

- 功能摘要：從分類樹與資源清單挑選工料機，加入或移除單價分析明細，支援文字／類型篩選、排序與資料來源切換。
- 主要源碼：`PCCES_CS/Archnowledge.Pcces.PccesMain.MrsBase/FormMrsBaseBreakdown_Addnew.cs`。
- 主要控制：`ultraTree1`、`gridMrsBase`、`c1FlexGrid2`、`GridUnit1`、`BtnAdd`、`BtnRemove`、`txtFilter`。
- 主要上下文：`ProjectCode`、`F_ActionName`、`F_CurrentDBName`、`CompanyDBName`、`F_CostUID`、`F_CostType`。
- 成本結構關聯：直接引用 `DomainModule.CostStructure.CostStructure`。
- Web 復刻時需再確認：加入後的用量預設、重複項規則、成本類別限制與專案／公共庫的複製方式。

### MRS-004 專案資源分析

- 功能摘要：彙總專案中的工料機資源，顯示資源及其引用工項，提供搜尋、編碼、價格、重算、父專案引用、書籤及匯出。
- 主要源碼：`PCCES_CS/Archnowledge.Pcces.PccesMain.Budget/FormBudgetRes.cs`。
- 主要控制：`gridMrsBase1`、`gridMrsBase2`、`BtnReCalSmall`、`btnAddBookList`、匯出選單與狀態列。
- 主要 Domain：`ProjMrsA`、`MrsBaseA`、`ItemA`、`CodeValidator`、`CodeFitter`。
- 重要狀態：`IsCanEdit`、`HasApproved`、`IsTemplate`、`FormActionName`、`budgetType`、`parentProjectCode`。
- Web 復刻時需再確認：資源彙總公式、價格更新影響範圍、代碼自動補齊、核定後唯讀與預算工作台重新載入條件。

### MRS-005 資源精度與顯示政策

- 功能摘要：主工項及分析項分別使用數量、單價、金額精度，影響 Grid 顯示、輸入與計算。
- 主要源碼：`FormBudgetRes.cs`、`FormMrsBaseBreakdown_Addnew.cs`、專案精度設定類別。
- 欄位：`F_MainQty`、`F_MainCst`、`F_MainAmt`、`F_AnaQty`、`F_AnaCst`、`F_AnaAmt`。
- Web 復刻要求：不得只用單一全域小數位；實作前需追蹤每個欄位的取位與重算位置。

### MRS-006 書籤與快速引用

- 功能摘要：將常用工項或資源加入書籤，供預算與單價分析快速引用，並支援移除。
- 主要源碼：`PCCES_CS/Archnowledge.Pcces.PccesMain.MrsBase.Bookmark/*`、`FormBudgetRes.cs`。
- Web 復刻時需再確認：書籤作用域是使用者、公司資料庫或專案，以及引用時是複製還是連結。

### MRS-007 歷史價格與變更追蹤

- 功能摘要：讀取歷史工率／價格，顯示預算變更歷史並支援比對。
- 主要源碼：`FormMrsBaseBreakdown.cs`、`FormBudgetRes.cs`、BudgetChange 與歷史價格相關 Domain。
- Web 復刻時需再確認：歷史版本鍵、有效日期、來源資料庫及套用後是否觸發全案重算。

### MRS-008 Excel 與外部資料交換

- 功能摘要：匯出資源與單價分析 Grid，並使用 Aspose Cells、C1Excel 或 ExportExcel 模組處理格式。
- 主要源碼：`FormMrsBaseBreakdown.cs`、`FormBudgetRes.cs`、`DomainModule.ExportExcel/*`。
- Web 復刻時需再確認：工作表名稱、欄位順序、格式、精度、合併儲存格與錯誤訊息。

## 3. 與其他模組的關係

```text
預算工項 ItemA
→ 單價分析 ItemB／資源明細
→ ProjMrsA 專案資源彙總
→ FormBudgetRes 資源檢視與調整
→ frmBudget 重載與全案重算
```

MRS 功能同時被以下 Action 使用：

- BUD 預算編製
- BID 投標單填寫
- 預算變更／契約變更
- 契約與分包
- 父專案或歷史專案引用

因此 Web 版不能把 MRS Base 做成獨立且無上下文的資料表 CRUD。

## 4. 實作前深挖清單

正式復刻本模組前，需重新讀取並確認：

1. `FormMrsBaseBreakdown` 的載入、編輯前後、刪除、重算及關閉事件。
2. `FormMrsBaseBreakdown_Addnew` 的分類樹建立、搜尋、加入與移除事件。
3. `FormBudgetRes` 的資源彙總、價格修改、編碼、自動重算及匯出事件。
4. `MrsBaseA`、`ProjMrsA`、`ItemA` 的資料表與交易邊界。
5. Bookmark namespace 的作用域與資料結構。
6. 歷史價格、生效日期與 BudgetChange 關聯。
7. Excel 輸出的正式欄位及格式。

## 5. 目前 Web 對照

現有 Web 已有資源頁與部分單價分析 API，但目前只能標記為 `PARTIAL`：

- 尚未證明具備完整分類樹與公共庫／專案庫切換。
- 尚未證明支援雙 Grid 引用關係。
- 尚未復刻成本結構篩選、父專案引用與 Action 上下文。
- 尚未復刻主項／分析項分離的精度政策。
- 尚未建立歷史價格、核定唯讀與完整重算契約。
