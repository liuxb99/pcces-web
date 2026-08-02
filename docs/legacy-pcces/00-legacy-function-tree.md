# PCCES C# Legacy 功能樹

更新日期：2026-08-02

本文件是 PCCES 桌面版的全系統功能地圖。用途是先確保所有舊功能都被發現、分類並連結到源碼；詳細事件鏈於實作對應 Web Segment 前再深入補寫。

狀態：`DISCOVERING`

## 1. 系統啟動、登入與主框架

### SYS-START-001 程式啟動與環境預檢

- 摘要：檢查資料庫、SQL 版本、重複程序、連線與啟動畫面。
- 主要源碼：`frmPccesMain`、`FormSplash`。
- 優先級：P0。
- 詳細文件：`01-startup-login-navigation.md`。

### SYS-AUTH-001 使用者登入與登入模式

- 摘要：帳密登入、匿名模式、首次管理員初始化、登入日誌與機器／IP 資訊。
- 主要源碼：`FormLogin`、`StaffClass`、`DBClass`。
- 優先級：P0。
- 詳細文件：`01-startup-login-navigation.md`。

### SYS-SHELL-001 主框架與首頁面板

- 摘要：MDI 主框架、首頁面板、功能區、狀態列、線上使用者與關閉閘門。
- 主要源碼：`frmPccesMain`、`FormPanel`、`FormPanel2`、`FormPanel3`、`OnlineList`。
- 優先級：P0。

### SYS-UPGRADE-001 資料庫版本與程式更新

- 摘要：資料庫版本檢查、升級入口、更新服務與註冊資訊。
- 主要源碼：SysMaintain、Database Upgrade／Change 相關類別。
- 優先級：P1。

## 2. 導航、模組與權限

### NAV-001 功能按鈕與模組入口

- 摘要：Budget、Bid、Common、Invoice 群組與中央模組啟動協調。
- 主要源碼：`FunctionButtons.cs`、`FormPanel2.cs`。
- 優先級：P0。
- 詳細文件：`02-main-navigation-and-module-launch.md`、`05-navigation-state-and-action-catalog.md`。

### AUTHZ-001 Function Code 權限

- 摘要：以 F001～F012 等功能代碼判斷使用者可用功能。
- 主要源碼：`FunctionButtons.cs`、`DBClass.ChkAuthority`、`DBClass.GetFuncName`。
- 優先級：P0。
- 詳細文件：`03-function-catalog-and-permissions.md`。

### MODULE-001 模組授權與開放模式

- 摘要：Budget、Bid、Common、Invoice 模組開關及 OPEN_MODE_CHECK。
- 主要源碼：`ModuleManager`、`FunctionButtons.cs`。
- 優先級：P0。

### CONTEXT-001 工作上下文切換

- 摘要：目前工作、表單重用、衝突工作關閉、取消恢復與 Active Function。
- 主要源碼：`FunctionButtons.cs`、`PccesFormAction`、專案選取器。
- 優先級：P0。
- 詳細文件：`04-project-selection-and-work-context.md`。

## 3. 專案管理

### PROJECT-001 專案目錄

- 摘要：依使用者列出專案，顯示模板、授權、BUD/BID/CNT 狀態與最近使用狀態。
- 主要源碼：`Project/FormProject.cs`、`PubProject.GetProjectList`。
- 優先級：P0。
- 詳細文件：`06-project-catalog-and-lifecycle.md`。

### PROJECT-002 新建專案精靈

- 摘要：多步驟建立專案，輸入 ProjectCode、Alias、名稱、地址與備註。
- 主要源碼：`Project/formNewProjectWizard.cs`。
- 優先級：P0。
- 詳細文件：`08-project-create-import-wizard.md`。

### PROJECT-003 專案匯入

- 摘要：XML、舊 XML、ZMD、MDB、Excel、PX 等來源辨識、驗證、匯入與附件處理。
- 主要源碼：`formNewProjectWizard.cs`、XML／Import 類別。
- 優先級：P0。
- 詳細文件：`10-project-import-validation-and-commit.md`。

### PROJECT-004 專案模板與複製

- 摘要：模板專案、由既有專案建立與主子專案關係。
- 主要源碼：`FormProject.cs`、`formNewProjectWizard.cs`、`PubProject`。
- 優先級：P1。

### PROJECT-005 專案分拆

- 摘要：選取預算樹、輸入 SplQty／SplCost、建立子專案、複製工項與取消回滾。
- 主要源碼：`formNewProjectWizard.cs`。
- 優先級：P0。
- 詳細文件：`11-project-split-and-rollback.md`。

### PROJECT-006 專案權限與刪除能力

- 摘要：ProjAuthority、GetProjectAuthority、IsCanDelete 與專案操作資格。
- 主要源碼：`FormProject.cs`、`DBClass`、`PubProject`。
- 優先級：P0。

## 4. 預算書與投標單編製

### BUD-001 預算／投標工作台

- 摘要：BUD 與 BID 共用 `frmBudget`，但使用不同 Action、權限與資料來源。
- 主要源碼：`Budget/frmBudget.cs`。
- 優先級：P0。
- 詳細文件：`12-budget-editor-shell-and-state.md`。

### BUD-002 專案選取與切換

- 摘要：選取可用專案、開啟、切換、重用與離開閘門。
- 主要源碼：`FormBudgetProjectPick.cs`、`frmBudget.cs`。
- 優先級：P0。

### BUD-003 預算樹與 Grid 操作

- 摘要：章、節、工項階層，插入、移動、複製、貼上、刪除、展開與收合。
- 主要源碼：`frmBudget.cs`、`GridBudget`。
- 優先級：P0。
- 詳細狀態：待實作前深挖。

### BUD-004 工項編輯器

- 摘要：依 B/L/F/S/U/Z 類型切換子編輯器，處理單價、比例、分段、公式與加總來源。
- 主要源碼：`FormBudgetEditMain.cs`、`BDGT_Component/*.cs`。
- 優先級：P0。
- 詳細文件：`13-budget-item-editor-components.md` 及既有工項編輯文件。

### BUD-005 B 類下層累算

- 摘要：單價由子層自動累算，不允許直接輸入。
- 主要源碼：`B_Form.cs`。
- 優先級：P0。

### BUD-006 L 類直接輸入

- 摘要：直接輸入單價並驗證數值與單位。
- 主要源碼：`L_Form.cs`。
- 優先級：P0。

### BUD-007 F 類比例費用

- 摘要：挑選加總來源，總額乘百分比，可關聯攤提差額 VDF1。
- 主要源碼：`F_Form.cs`。
- 優先級：P0。

### BUD-008 S 類分段計價

- 摘要：加總來源、正負號、金額區間、費率與選用公式。
- 主要源碼：`S_Form.cs`、`S_Form2.cs`。
- 優先級：P0。

### BUD-009 U 類自訂公式

- 摘要：加總來源、自訂變數、公式輸入、公式檢查與說明。
- 主要源碼：`U_Form.cs`。
- 優先級：P0。

### BUD-010 Z 類加總項

- 摘要：挑選加總項目並依正負號計算總金額。
- 主要源碼：`Z_Form.cs`。
- 優先級：P0。

### BUD-011 精度、取位與攤提

- 摘要：主項與分析項分別管理數量、單價、金額精度；取位可能依賴攤提項目。
- 主要源碼：`frmBudget.cs`、`FormBudgetEditMain.cs`、專案精度設定。
- 優先級：P0。

### BUD-012 自動保存、重算與鎖定

- 摘要：Autosave、Recalculate、Item Lock、分析鎖定、契約鎖定與關閉解鎖。
- 主要源碼：`frmBudget.cs`、`FormBudgetEditMain.cs`。
- 優先級：P0。

### BUD-013 預算自我檢查

- 摘要：預算資料檢核、錯誤與警告列表。
- 主要源碼：`FormBudgetSelfExam.cs`。
- 優先級：P1。

### BUD-014 第三方／歷史資料引用

- 摘要：第三方資料、歷史價格與跨專案工項引用。
- 主要源碼：`FormBudgetThirdParty.cs`、`FormPickProjWkItem_Wzd.cs`。
- 優先級：P1。

## 5. 工項單價庫、工料機與單價分析

### MRS-001 基本工料機資料庫

- 摘要：工項、材料、人工、機具與分類資料的瀏覽、搜尋及維護。
- 主要源碼：`PccesMain.MrsBase` namespace。
- 優先級：P0。

### MRS-002 單價分析明細

- 摘要：工項展開成工料機組成、用量、單價與複價。
- 主要源碼：`FormMrsBaseBreakdown.cs`、`FormMrsBaseBreakdown_Addnew.cs`。
- 優先級：P0。

### MRS-003 書籤與收藏

- 摘要：工項／資源書籤新增、移除與快速引用。
- 主要源碼：`MrsBase.Bookmark` namespace。
- 優先級：P1。

### MRS-004 預算資源分析

- 摘要：專案內資源彙總、統計、替換與歷史價格。
- 主要源碼：`FormBudgetRes.cs`、`MrsBaseA`、`ProjMrsA`。
- 優先級：P0。

### MRS-005 單價與工程項目比對

- 摘要：經費審查、單價分析比對與歷史工程比較。
- 主要源碼：F007、F008 對應模組。
- 優先級：P1。

## 6. 成本結構與資料轉換

### CONV-001 預算轉電子標單

- 摘要：預算資料轉成投標／發包資料與交換格式。
- 主要源碼：`Conversion.cs`、Bid／Budget DomainModule。
- 優先級：P0。

### CONV-002 標單回轉與匯入

- 摘要：投標資料回轉、匯入、來源識別與專案更新。
- 主要源碼：Bid Import Wizard、`formNewProjectWizard.cs`。
- 優先級：P0。

### COST-001 成本結構

- 摘要：成本分類、費用項目、管理費、稅費與加減項。
- 主要源碼：`DomainModule.CostStructure`、`CostKind`。
- 優先級：P0。

### CONV-003 Excel／XML／MDB／ZMD 轉換

- 摘要：多版本格式解析、Schema Adapter、輸出與錯誤處理。
- 主要源碼：XML、ExportExcel、Conversion、Project Import 類別。
- 優先級：P0。

## 7. 契約、分包、變更與估驗

### CNT-001 契約編製／分包

- 摘要：由預算選取項目建立契約或分包，管理契約項目與金額。
- 主要源碼：`SplitContract/FormSplitContract.cs`。
- 優先級：P0。

### CHG-001 預算／契約變更

- 摘要：變更版本、增減項、變更數量與單價及差異追蹤。
- 主要源碼：`BudgetChange/FormBudgetChange.cs`、Budget Change namespace。
- 優先級：P0。

### INV-001 估驗計價

- 摘要：期別、當期、累計、保留款、扣款、審核與報表。
- 主要源碼：Invoice 相關 namespace／Form。
- 優先級：P0。

### CNT-002 契約文件與附件

- 摘要：契約附加文件、下載、AddOn 與來源檔案管理。
- 主要源碼：AddOn、Document、Contract 相關類別。
- 優先級：P1。

## 8. 結算、驗收與履約收尾

### CLOSE-001 結算作業

- 摘要：最終數量、結算金額、差異與結算報表。
- 主要源碼：`FormSubClose`、F012 入口。
- 優先級：P0。

### FINAL-001 驗收作業

- 摘要：驗收、缺失、改善與最終狀態。
- 主要源碼：`FormSubFinal`。
- 優先級：P0。

### ISSUE-001 爭議與缺失管理

- 摘要：履約爭議、缺失、處理紀錄與狀態。
- 主要源碼：待掃描確認。
- 優先級：P1。
- 可信度：UNKNOWN。

### ARCHIVE-001 關閉與封存

- 摘要：完成後鎖定、關閉、封存與禁止修改條件。
- 主要源碼：SubClose、SubFinal、Project 狀態相關類別。
- 優先級：P1。

## 9. 報表、列印與匯入匯出

### REPORT-001 預算與單價分析報表

- 摘要：預算總表、詳細表、單價分析表、資源統計表。
- 主要源碼：Report、Crystal Reports、Budget 報表入口。
- 優先級：P0。

### REPORT-002 契約、估驗、結算與驗收報表

- 摘要：履約各階段的表單、彙總與正式輸出。
- 主要源碼：各模組 Report／Print 類別。
- 優先級：P0。

### EXPORT-001 Excel 輸出

- 摘要：欄位、格式、公式、合併儲存格、分頁與版本相容。
- 主要源碼：`DomainModule.ExportExcel`、C1Excel 使用處。
- 優先級：P0。

### EXPORT-002 PDF、預覽與列印

- 摘要：預覽、列印、PDF 轉換及錯誤流程。
- 主要源碼：Report、ShellLib、PDF／Print 相關類別。
- 優先級：P0。

### IMPORT-001 電子標單與舊格式匯入

- 摘要：XML、ZMD、MDB、PX、Excel 與舊版 Schema 相容。
- 主要源碼：Project Wizard、XMLClass、Conversion。
- 優先級：P0。

## 10. 系統管理與外部服務

### ADMIN-001 使用者、組織與權限

- 摘要：使用者、角色、功能代碼、專案權限與管理員功能。
- 主要源碼：SysMaintain、StaffClass、DBClass。
- 優先級：P0。

### ADMIN-002 系統參數與代碼表

- 摘要：UserDefined、單位、功能設定、INI 與應用設定。
- 主要源碼：SysMaintain、UserDefind、CommonMethods。
- 優先級：P1。

### ADMIN-003 資料庫升級與維護

- 摘要：資料庫版本、Schema 升級、修復與維護工具。
- 主要源碼：Database Upgrade／Change、SysMaintain。
- 優先級：P0。

### SERVICE-001 更新、註冊與 Proxy

- 摘要：軟體更新、註冊驗證、Proxy 與外部連線。
- 主要源碼：Update、Registration、Proxy 相關類別。
- 優先級：P1。

### SERVICE-002 OnlineList 與協作狀態

- 摘要：線上使用者、工作狀態、Freeze／Enable 與可能的協作鎖定。
- 主要源碼：`OnlineList`、主框架與各工作台。
- 優先級：P1。

## 11. 待完成掃描

以下區域仍需用檔案清單與 namespace 掃描補齊：

- 全部 Report／Crystal Report 類別
- Invoice、SubClose、SubFinal 全部表單
- SysMaintain 全部子功能
- Database Upgrade／Change
- Conversion 與 ExportExcel 全部分支
- MrsBase 全部 Form／UserControl
- 未掛入功能樹的共用工具、Wizard 與 AddOn

功能樹完成前，新增節點優先於逐方法深挖。
