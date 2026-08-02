# PCCES C# 系統管理與平台服務摘要

更新日期：2026-08-02

本文件採「功能樹優先」方式，整理 `SysMaintain`、資料庫管理、使用者／群組／權限、系統設定與平台服務。細部事件、SQL 與交易規則於對應 Web Segment 實作前再深入追蹤。

## 1. 使用者、群組與權限

### SYS-ADMIN-USER-001 使用者帳號維護

- 摘要：新增、編輯、刪除使用者，維護 UserID、姓名、密碼與確認密碼。
- 主要源碼：`SysMaintain/FormSys_A.cs`、`FormSys_A_Edit.cs`。
- 直接證據：`FormSys_A` 包含 `BtnAddUser`、`BtnUser_Add`、`BtnUser_Edt`、`BtnUser_Del`、`BtnSaveUser`、`txtUserID`、`txtUserName`、`txtPwd`、`txtPwdConfirm`。
- Web 復刻：使用者 API、密碼政策、停用／刪除規則、稽核紀錄。

### SYS-ADMIN-GROUP-001 群組維護

- 摘要：建立與刪除群組、維護群組代碼及名稱。
- 主要源碼：`FormSys_A.cs`、`FormSys_A_UsrGroup.cs`。
- 直接證據：`GridGroups`、`txtGroupID`、`txtGroupName`、`BtnSaveGroup`、`BtnGRP_Del`。

### SYS-ADMIN-MEMBER-001 使用者與群組成員關係

- 摘要：維護群組內使用者與使用者所屬群組。
- 主要源碼：`FormSys_A.cs`、`FormSys_A_GrpMember.cs`。
- 直接證據：`GridGroupUsers`、`GridUserGroups`、`DT_GroupUsers`、`DT_UserGroups`。

### SYS-ADMIN-AUTHZ-001 使用者／群組功能權限

- 摘要：以功能樹為基礎，分別維護群組功能與使用者功能授權。
- 主要源碼：`FormSys_A.cs`、`DBClass`。
- 直接證據：`DT_GroupFuncs`、`DT_UserFuncs`、`DT_GRPChk`、`DT_UsrChk`、兩組 `UltraTree`。
- 與既有功能碼關聯：F001～F012 及細分 Function Code。

## 2. 系統維護子模組

`SysMaintain` 至少存在下列主控制項／對話框，先列入功能地圖；字母對應的正式業務名稱，未經完整事件與 UI 文字確認前不擅自命名。

- `FormSys_A`、`FormSys_A_Edit`、`FormSys_A_UsrGroup`、`FormSys_A_GrpMember`
- `FormSys_B`
- `FormSys_C`、`FormSys_C_Edit`
- `FormSys_D`、`FormSys_D_Pick`
- `FormSys_E`
- `FormSys_F`
- `FormSys_G`、`FormSys_G1`、`FormSys_G_Info1`
- `FormSys_I`
- `FormSys_J`
- `FormSys_Z`
- `CostStructureImport`
- `CostStructureTypePicker`

未確認節點統一標記 `DISCOVERED / PURPOSE_REQUIRES_SOURCE_REVIEW`。

## 3. 資料庫與組織資料庫管理

### SYS-DB-001 資料庫清單與搜尋

- 摘要：列出系統資料庫，支援搜尋與選取。
- 主要源碼：`FormSys_G.cs`。
- 直接證據：`gridDatabases`、搜尋工具列與 `PreviousKeyword`。

### SYS-DB-002 建立資料庫

- 摘要：建立資料庫，輸入資料庫名稱、組織名稱與相關設定。
- 主要源碼：`FormSys_G.cs`、`FormSys_G_Info1.cs`、`DomainModule.DatabaseUpgrade`。
- 直接證據：`btnCreateDB`、`tbDBName`、`tbDBOrganization`、`ProgressDialog`。

### SYS-DB-003 建立組織資料庫

- 摘要：選取組織代碼並建立組織專用資料庫。
- 主要源碼：`FormSys_G.cs`。
- 直接證據：`btnPickOrganizationCode`、`CreateOrganizationDatabase`。

### SYS-DB-004 公司資料庫與估驗資料庫選項

- 摘要：建立資料庫時可設定公司資料庫及估驗相關欄位。
- 主要源碼：`FormSys_G.cs`。
- 直接證據：`cbCompanyDB`、`tb_dbInv`。

### SYS-DB-005 成本結構初始化

- 摘要：建立資料庫時可匯入選定成本結構類型。
- 主要源碼：`FormSys_G.cs`、`CostStructureImport.cs`、`CostStructureTypePicker.cs`。
- 直接證據：`cbImportCostStructure`、`CostStructureSelectedTypes`。

### SYS-DB-006 資料庫刪除與切換

- 摘要：提供刪除資料庫與切換資料庫入口。
- 主要源碼：`FormSys_G.cs`。
- 直接證據：`mnuDelete`、`mnuChangeDB`。

### SYS-DB-007 舊版本還原與版本重置

- 摘要：提供舊 Build 還原與資料庫版本重置工具。
- 主要源碼：`FormSys_G.cs`、`DomainModule.DatabaseUpgrade`。
- 直接證據：`restoreBuild103`、`mnuRestore103`、`mnuResetVer`。
- 注意：實際適用版本與安全條件需實作前深讀。

### SYS-DB-008 自動編號設定

- 摘要：資料庫層級可設定自動編號策略。
- 主要源碼：`FormSys_G.cs`。
- 直接證據：`mnuSetAutoNum`、`F_IsAutoNumCustom`。

## 4. 全域系統設定

### SYS-SETTING-001 一般設定分頁

- 摘要：以多 Tab 管理一般、預算與投標、分析、MRS Base、資料庫及其他設定。
- 主要源碼：`FormSys_Z.cs`。
- 直接證據：`tabGeneralSetting`、`tbBudgetAndBidSetting`、`tabAnalysisSetting`、`tabMrsBaseSetting`、`tabDatabaseSetting`。

### SYS-SETTING-002 預算自動保存

- 摘要：啟用自動保存、設定間隔及是否清除自動保存資料。
- 主要源碼：`FormSys_Z.cs`。
- 直接證據：`chkBDGT_AutoSave`、`BDGT_Duration`、`chk_DeleteAutoSave`。

### SYS-SETTING-003 新舊重算與分析開啟模式

- 摘要：允許選擇舊重算行為及分析畫面新開啟模式。
- 主要源碼：`FormSys_Z.cs`。
- 直接證據：`chk_forOldReCal`、`chk_Ana_UseNewOpen`。

### SYS-SETTING-004 MRS 載入與匯率策略

- 摘要：控制 MRS 載入方式與自動匯率變更。
- 主要源碼：`FormSys_Z.cs`。
- 直接證據：`chkMrs_LoadMethod`、`chkMrs_AutoChangeRate`。

### SYS-SETTING-005 未使用工項清理

- 摘要：設定是否刪除未使用工項。
- 主要源碼：`FormSys_Z.cs`。
- 直接證據：`chk_forDeleteNoUsedItem`。

### SYS-SETTING-006 Excel 字型

- 摘要：設定 Excel 匯出字型。
- 主要源碼：`FormSys_Z.cs`。
- 直接證據：`cboExlFont`。

### SYS-SETTING-007 報表套件路徑

- 摘要：設定報表套件位置，支援檔案選取。
- 主要源碼：`FormSys_Z.cs`。
- 直接證據：`txtReportPack`、`OpenFileDialog`。

### SYS-SETTING-008 備份與復原

- 摘要：管理備份目錄及復原入口。
- 主要源碼：`FormSys_Z.cs`。
- 直接證據：`F_BackupFolder`、`BtnRecover`。

### SYS-SETTING-009 SQL／實體記憶體資訊

- 摘要：顯示實體記憶體與 SQL 記憶體資訊，可能用於資料庫設定判斷。
- 主要源碼：`FormSys_Z.cs`。
- 直接證據：`lblPhysicalMem`、`lblSQLMem`。

## 5. 共用平台服務

### PLATFORM-DB-001 `DBClass`

- 摘要：功能權限、專案權限、SQL 與系統共用資料操作的集中入口。
- Web 復刻：拆分為 Authz Service、Project Access Service、Repository 與 Migration Service，避免形成單一巨大類別。

### PLATFORM-DB-002 `ModifyDB`

- 摘要：低階新增、更新與刪除操作。
- Web 復刻：不得直接暴露給路由層，需經 Domain Service 與 Transaction。

### PLATFORM-CONFIG-001 INI 與本機設定

- 摘要：桌面版使用 `OptionSet.ini`、`PccesMain.ini` 及應用路徑保存設定。
- Web 復刻：區分系統設定、組織設定、使用者偏好與部署環境變數。

### PLATFORM-UPDATE-001 資料庫升級

- 摘要：`DomainModule.DatabaseUpgrade` 提供資料庫建立、升級、版本還原與重置能力。
- Web 復刻：使用正式 Migration、版本表、備份、dry-run 與失敗回滾。

### PLATFORM-REPORT-001 報表套件與下載

- 摘要：系統設定指定報表套件，報表模組另有預覽、下載進度、成功與失敗處理。

## 6. Web 版主要缺口

1. 目前角色模型尚不能替代使用者／群組／Function Code 三層授權。
2. 缺完整群組成員及群組功能管理。
3. 缺組織資料庫建立、切換、升級、版本重置與備份復原工作流。
4. 缺系統設定的作用域、版本與稽核。
5. 缺將桌面 INI 設定轉成資料庫／環境變數／使用者偏好的正式對照。
6. 缺 Migration 執行狀態、進度、失敗恢復及互斥鎖。
7. 缺報表套件版本與相容管理。

## 7. 實作前深讀清單

- `FormSys_A` 所有保存、刪除與權限樹事件。
- `FormSys_B`～`FormSys_J` 的 UI 文字、事件與資料來源。
- `FormSys_G` 的建立、刪除、還原、切換與版本重置流程。
- `FormSys_Z` 每個設定鍵的存放位置、預設值與生效時機。
- `DBClass`、`ModifyDB` 的公開方法與 SQL 副作用。
- `DomainModule.DatabaseUpgrade` 的版本圖與回滾能力。
