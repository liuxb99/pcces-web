# PCCES Web 完整復刻 Phase Roadmap

更新日期：2026-08-02

## 1. 目的

本文件將 PCCES C# 桌面版完整復刻工作，拆分為 10 個可獨立開發、測試與驗收的 Web Phase。

最終完成標準不是「網頁有相似頁面」，而是：

- Legacy 功能有明確 Feature ID 與 C# 源碼入口。
- Web 端具有對應資料模型、Domain 規則、API、前端互動與權限。
- 計算結果、狀態轉換、資料交換與正式報表具備相容性證據。
- 每項功能達到 `LEGACY_MATCHED`，並以永久回歸測試提升至 `VERIFIED`。

## 2. 固定開發流程

每一個 Phase 與 Segment 都依下列順序執行：

```text
選定 Legacy 功能節點
→ 依 Source Index 深讀相關 C# 源碼
→ 補齊事件鏈、狀態機與資料規格
→ 建立 API／Database／Domain 契約
→ 後端實作
→ 前端實作
→ 單元、整合與端到端測試
→ Legacy 對照驗收
→ 更新 Traceability Matrix
```

不得只依既有摘要直接寫程式。摘要用於定位，實作前仍須回讀 C#。

## 3. Phase 依賴順序

```text
Phase 0 平台基礎
  ↓
Phase 1 專案管理
  ↓
Phase 2 預算與投標核心
  ↓
Phase 3 MRS／工料機／單價分析
  ↓
Phase 4 成本結構與資料交換
  ↓
Phase 5 契約與分包
  ↓
Phase 6 變更、估驗、結算與驗收
  ↓
Phase 7 報表中心
  ↓
Phase 8 系統管理
  ↓
Phase 9 Legacy 收尾與 100% Traceability
```

Phase 7、8 可在 Phase 5、6 後段部分並行，但不得繞過 Phase 0 的平台契約。

---

# Phase 0：平台基礎與 API 收斂

## 目標

建立全部後續復刻模組共同依賴的唯一後端、資料庫、權限、Action 與工作上下文基礎。

## 範圍

- 確立唯一正式 Flask 後端，移除雙後端漂移。
- 統一 `/api` base path，消除 `/api/api/...` 類契約錯誤。
- 建立 Migration 與環境一致性。
- 建立 OpenAPI，前端 API 型別由契約生成或受契約測試保護。
- Legacy Function Code：`F001`～`F012` 與子功能碼。
- Module entitlement：Budget、Bid、Common、Invoice。
- `PccesFormAction` 對應的 Web Action Catalog。
- WorkContext、ProjectContext、Dirty State、Context Reuse。
- JWT 認證、細粒度授權、稽核日誌。
- Decimal／Numeric、精度政策、時區與統一錯誤格式。
- 樂觀鎖定、row version、交易邊界與 idempotency 基礎。

## Legacy 入口

- `frmPccesMain`
- `FormLogin`
- `FunctionButtons.cs`
- `ModuleManager`
- `PccesFormAction`
- `DBClass.ChkAuthority`
- `DBClass.GetFuncName`
- `OnlineList`
- `FormSys_A`

## 交付物

- 唯一 API application entrypoint。
- Alembic 或等效 Migration 基礎。
- OpenAPI 文件與契約測試。
- Function Code／Module／Action／WorkContext tables 與 services。
- 統一 API client。
- 權限守衛與審計中介層。

## 驗收標準

- 所有正式前端請求只經過唯一 `/api` 契約。
- 缺少 Legacy Function Code 時，API 與 UI 均拒絕操作。
- Disabled module 不可透過直接 URL 或 API 繞過。
- Dirty WorkContext 切換有保存、放棄或取消語意。
- 金額欄位不使用 binary float 作為正式儲存型別。
- Migration 可從空資料庫建立完整 schema，並可在 PostgreSQL 執行。

---

# Phase 1：專案管理與生命週期

## 目標

完整復刻桌面版專案目錄、新建、匯入、模板、分拆、權限與 Action Eligibility。

## 範圍

- Legacy `projectCode` 穩定識別，不以資料庫 UUID 取代。
- `projectCodeAlias`、`mainProj`、source／old／new／sub project code。
- 專案目錄與使用者可見範圍。
- 模板、已授權、BUD／BID／CNT 資料存在狀態。
- 最近使用、非 PCCES 相容、操作能力。
- 建立新專案 Wizard。
- XML、舊 XML、ZMD、MDB、Excel、PX 匯入 Session。
- 匯入驗證、暫存、commit、rollback、附件與來源檔案。
- 專案分拆、階層選取、SplQty／SplCost、子專案建立。
- `IsCanDelete` Domain capability。
- 各 Action 的 eligible project query。

## Legacy 入口

- `Project/FormProject.cs`
- `Project/formNewProjectWizard.cs`
- `Budget/FormBudgetProjectPick.cs`
- `BUDClass.Project`
- `BUDClass.PubProject`
- `ProjAuthority`

## 交付物

- Project domain model 與 capability endpoint。
- Project Catalog UI。
- Wizard Session APIs。
- Import adapters 與 job／progress model。
- Split Session 與 rollback。
- Project Action Eligibility API。

## 驗收標準

- 建立、匯入與分拆是不同的 Domain command。
- 取消未完成的 Wizard 不留下孤兒專案或附件。
- ProjectCode 唯一、穩定且可由所有後續模組引用。
- 專案刪除由 capability 決定，不能只用 owner/admin 判斷。
- 每個 Action 只能選取符合 Legacy 前置資料與權限的專案。

---

# Phase 2：預算書與投標單核心

## 目標

完整復刻 `frmBudget` 工作台、預算樹、工項類型、計算、保存、鎖定與 BUD／BID 雙模式。

## 範圍

- BUD 與 BID 共用工作台，但規則、權限、欄位與資料來源分流。
- 章、節、工項樹與 Grid 操作。
- 新增、插入、移動、複製、貼上、刪除、展開與收合。
- B／L／F／S／U／Z 工項類型。
- Child rollup、direct input、rate、tiered rate、formula、signed sum。
- ItemA／ItemB／ItemC 與變數、公式、區間、正負號。
- 主項與分析項各自的數量、單價、金額精度。
- Autosave、全案重算、局部重算與錯誤恢復。
- 工項鎖定、分析鎖定、契約鎖定、唯讀模式。
- 預算版本、凍結、差異與 Calculation Trace。
- 自我檢查、第三方資料與跨專案工項引用。

## Legacy 入口

- `Budget/frmBudget.cs`
- `Budget/FormBudgetEditMain.cs`
- `Budget/BDGT_Component/B_Form.cs`
- `L_Form.cs`、`F_Form.cs`、`S_Form.cs`、`S_Form2.cs`
- `U_Form.cs`、`Z_Form.cs`
- `BUDClass.ItemA`、`ItemB`、`ItemC`、`PCals`
- `FormBudgetSelfExam.cs`
- `FormBudgetThirdParty.cs`
- `FormPickProjWkItem_Wzd.cs`

## 交付物

- Budget Tree domain 與 editor API。
- 工項類型 polymorphic calculation model。
- Calculation Trace 與 deterministic recalculation。
- Budget/Bid Editor UI。
- Autosave、dirty state、locking、versioning。

## 驗收標準

- B 類單價不可直接覆寫，必須由子層累算。
- 各類工項的公式與精度結果可與 Legacy 測例比對。
- 重算可重入且相同輸入產生相同結果。
- 鎖定、凍結或無權限狀態無法由 API 繞過。
- Autosave 失敗不會覆蓋較新版本，並可恢復未提交修改。

---

# Phase 3：MRS Base、工料機與單價分析

## 目標

復刻基本工料機庫、單價分析、專案資源、價格引用、書籤與歷史資料。

## 範圍

- 工項、材料、人工、機具與分類資料。
- MRS 搜尋、篩選、排序與維護。
- 單價分析展開、分析數量、價格、複價與工率。
- 新增／挑選分析項。
- 專案資源彙總與引用工項雙向追蹤。
- 資源替換、批次價格更新與重算。
- PCCES Code 驗證、補齊與換碼。
- 書籤、收藏與快速引用。
- 父專案、歷史價格、歷史工率與變更歷史。
- MRS 匯入匯出。

## Legacy 入口

- `PccesMain.MrsBase/*`
- `frmMrsBase.cs`
- `FormMrsBaseBreakdown.cs`
- `FormMrsBaseBreakdown_Addnew.cs`
- `FormMrsBase_ExpWizard.cs`
- `Budget/FormBudgetRes.cs`
- `MrsBase.Bookmark/*`
- `MrsBaseA`、`ProjMrsA`

## 交付物

- MRS catalog 與 analysis services。
- Resource aggregation engine。
- Code validation／fitting services。
- MRS 與 Budget resource UI。
- 價格來源與生效日期模型。

## 驗收標準

- 單價分析與預算工項之間具備可追溯關聯。
- 資源價格修改會依 Legacy 規則影響引用工項並重算。
- 分析項精度與主工項精度不混用。
- 書籤、歷史來源與跨專案引用保留來源身分。

---

# Phase 4：成本結構、轉換與資料交換

## 目標

復刻成本結構、預算／標單轉換、併標與 Legacy 檔案交換能力。

## 範圍

- 成本結構類型、選取、匯入、屬性與初始化。
- 費用分類、管理費、稅費及加減項。
- 預算轉電子標單。
- 標單回轉與投標 Add-on。
- 預算併標與資料來源衝突處理。
- XML／舊 XML／ZMD／MDB／Excel／PX。
- Schema Adapter、版本辨識、驗證、警告與錯誤目錄。
- 匯入／匯出 Session、進度、取消與 atomic commit。
- 附件、來源檔名與格式版本保存。

## Legacy 入口

- `DomainModule.CostStructure/*`
- `CostStructureImport.cs`
- `CostStructureTypePicker.cs`
- `FormBudgetCostStructurePicker.cs`
- `FormBudgetCostProperty.cs`
- `Conversion.cs`
- `FormBudgetExp_Wzd.cs`
- `FormBudgetExp_WzdOption.cs`
- `FormBudgetCombineBid.cs`
- `ucBudgetCombineBid.cs`
- `formNewProjectWizard.cs`

## 交付物

- Cost Structure domain。
- Conversion adapters。
- Import／Export job framework。
- 格式版本與檔案 lineage。
- 自我檢查與可下載錯誤報告。

## 驗收標準

- 匯入失敗不產生部分成功資料。
- 匯出結果可由 Legacy 或相容測試重新匯入。
- 格式版本、來源檔案、警告與轉換結果可稽核。
- 併標衝突必須有明確處理策略，不可靜默覆蓋。

---

# Phase 5：契約與分包管理

## 目標

從核定預算建立契約／分包，保存來源工項、契約版本與履約基準。

## 範圍

- 契約專案 eligibility。
- 契約基本資料與契約項目。
- 預算工項選取、數量／金額分配與來源追蹤。
- 分包與主契約關聯。
- 契約版本、核定、鎖定與狀態。
- 契約金額與預算金額一致性檢查。
- 契約報表資料來源。

## Legacy 入口

- `SplitContract/FormSplitContract.cs`
- `DomainModule.Sub/*`
- `Report/ucSubCtr.cs`
- `FormBudgetProjectPick.cs`

## 交付物

- Contract／Subcontract domain。
- Budget-to-contract allocation model。
- Contract Editor UI。
- Contract version／approval／lock services。

## 驗收標準

- 每個契約項目可反查原始預算工項。
- 分配量與金額不可超過可用基準，除非走明確變更流程。
- 已核定或已有下游估驗的契約不可直接覆寫。
- 契約報表使用相同版本快照。

---

# Phase 6：變更、估驗、結算與驗收

## 目標

復刻從契約變更到最終驗收的完整履約狀態鏈。

## 範圍

### 變更

- 預算／契約變更案件。
- 新增、減少、替代與調整項目。
- 變更原因、責任、日期、附件與歷史。
- 變更前後版本與差異。

### 估驗

- 期別建立、當期數量／金額、累計與前期結轉。
- 進度、摘要、扣款、保留款、指數調整與其他調整。
- 匯入、匯出、圖表與正式報表檢核。

### 結算

- 最終數量、最終金額、未完成項、調整與結算資訊。

### 驗收

- 驗收資料、項目選取、缺失、改善與最終狀態。

## Legacy 入口

- `BudgetChange/FormBudgetChange.cs`
- `FormBudgetChange_Addnew.cs`
- `FormBudgetChangeInfo.cs`
- `FormBudgetChangeInfoPicker.cs`
- `FormBudgetChangeResponsibility.cs`
- `FormBudgetChangeHistory.cs`
- `DomainModule.SubChg/*`
- `Invoice/FormInvoice.cs`
- `FormInvoiceProgress.cs`
- `FormInvoiceSummary.cs`
- `FormInvoiceDec2.cs`
- `FormInvoiceIndexNumber.cs`
- `FormInvoiceGraphic.cs`
- `FormInvoiceImport.cs`
- `FormInvoiceExport.cs`
- `SubClose/FormSubClose.cs`
- `FormSubCloseInput.cs`
- `FormSubCloseInfo.cs`
- `SubFinal/FormSubFinal.cs`
- `FormSubFinalInput.cs`
- `FormSubFinal_ItemPick.cs`

## 交付物

- Change Order domain。
- Invoice period／line／deduction／adjustment model。
- Settlement domain。
- Acceptance domain。
- 完整狀態機與版本快照。

## 驗收標準

- 當期、前期與累計數值可重算且一致。
- 後續期別建立後，已核定前期不得直接修改。
- 變更項目、估驗項目、結算項目均保留來源 lineage。
- 結算完成後的一般估驗操作受限。
- 驗收完成後進入最終封存狀態，解鎖需受控程序。

---

# Phase 7：報表中心

## 目標

建立取代 Crystal Reports 桌面入口的正式 Web 報表中心，保留報表內容、版本與輸出相容性。

## 範圍

- 預算總表、詳細表、單價分析表、資源統計表。
- 契約、變更、估驗、累計、結算與驗收報表。
- 報表參數、資料快照、模板版本與產生者。
- Preview、PDF、Excel、列印與批次輸出。
- 大型報表非同步生成、進度、完成、失敗與重試。
- 報表下載權限與稽核。
- Crystal `.rpt`／DataSet／Parameter 對照目錄。

## Legacy 入口

- `Report/FormReportViewer.cs`
- `Report/ucCrystalViewer.cs`
- `FormInvoiceReport.cs`
- `FormInvReportCheck.cs`
- `ucSubCtr.cs`
- `ucSubChg.cs`
- `ucSubAcc.cs`
- `ucSubClose.cs`
- `ucSubFinal.cs`
- `Report.WebDownload/*`
- `DomainModule.ExportExcel/*`

## 交付物

- Report Definition Catalog。
- Snapshot-based report jobs。
- PDF／Excel renderers。
- Download center 與 retention policy。

## 驗收標準

- 報表資料必須綁定明確業務版本，不隨後續修改漂移。
- Legacy 主要報表欄位、分組、合計與頁面語意有對照證據。
- 長時間生成不阻塞 API request，失敗可追蹤與重試。
- 報表下載受 Function Code、Project permission 與資料版本限制。

---

# Phase 8：系統管理、設定與維運

## 目標

復刻使用者、群組、權限、系統設定、資料庫管理、備份與升級能力，並改造成適合 Web 生產環境的管理模式。

## 範圍

- 使用者、群組、群組成員。
- User／Group Function Code。
- Project Authority 與 module entitlement。
- 一般、預算、投標、分析、MRS、Excel、報表設定。
- Autosave 與重算設定。
- 資料庫／組織資料庫建立與管理語意。
- 成本結構初始化。
- Schema migration、版本檢查與升級。
- 備份、復原與災難復原。
- Update、Registration、Proxy、Add-on 與外部服務盤點。
- System health、memory／database metrics 與操作稽核。

## Legacy 入口

- `SysMaintain/FormSys_A.cs`～`FormSys_J.cs`
- `FormSys_Z.cs`
- `FormSys_G.cs`
- `FormSys_G1.cs`
- `FormSys_G_Info1.cs`
- `CostStructureImport.cs`
- `DBClass.cs`
- `DomainModule.DatabaseUpgrade/*`
- `CommonMethods`
- `PubTools`
- `AddOnDownLoad`
- `SysUser`

## 交付物

- Admin Console。
- Fine-grained permission editor。
- Typed setting registry。
- Migration／backup／restore runbooks。
- Health、audit、external integration catalog。

## 驗收標準

- 權限變更可稽核且即時影響 API。
- 設定有型別、範圍、預設值與版本，不能以任意字串散落。
- 生產資料庫升級具備 precheck、backup、transaction 與 rollback 指引。
- 備份可實際還原到隔離環境並通過 smoke test。

---

# Phase 9：Legacy 收尾與 100% Traceability

## 目標

關閉所有尚未映射的 Legacy 功能、工具、報表與例外行為，達成可證明的完整復刻。

## 範圍

- 掃描剩餘 Form、UserControl、Wizard、Picker、Dialog、Domain 類別。
- 完成 C# 類別 → Feature ID → Web capability 映射。
- 關閉 `UNKNOWN`、`DISCOVERED`、`PURPOSE_REQUIRES_SOURCE_REVIEW`。
- 補齊錯誤訊息、警告、舊資料相容與特殊模式。
- Runtime test 確認反編譯疑點與歷史 Bug。
- 全量資料交換、計算與報表 golden tests。
- Accessibility、效能、安全與生產部署驗收。
- Legacy 功能退役與資料遷移方案。

## 交付物

- 100% Legacy Feature Catalog。
- 100% Source Traceability Matrix。
- 完整永久回歸測試集。
- Migration／cutover／rollback plan。
- Production readiness report。

## 驗收標準

- 每個 Legacy 功能節點均為 `VERIFIED`、明確標記不復刻並有決策紀錄，或證明為不可達／無效代碼。
- 不存在只由 UI 宣稱完成、但沒有 API／Domain／測試支撐的功能。
- 關鍵計算、交換格式與正式報表具備 golden fixture。
- 生產 build、migration、backup restore、security scan 與端到端測試全部通過。

## 4. Phase 完成狀態

每個 Phase 使用以下狀態：

- `NOT_STARTED`
- `LEGACY_DEEP_REVIEW`
- `SPEC_READY`
- `IMPLEMENTING`
- `INTEGRATION_TESTING`
- `LEGACY_MATCHING`
- `VERIFIED`

不得用文件完成度代替 Web 實作完成度。

## 5. Segment 大小原則

每個開發 Segment 應至少形成一個完整業務能力，例如：

- Function Code + Module + Action 授權閉環。
- 專案建立／匯入 Session 的完整交易流程。
- 一種工項類型從資料模型到 UI、計算與測試。
- 一個完整估驗期別生命週期。

不得以單一按鈕、單一 API 或單一頁面作為正常 Segment，除非它能關閉同一根因的一組缺口。
