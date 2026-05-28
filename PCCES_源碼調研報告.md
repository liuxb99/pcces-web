# PCCES Win 4.3 — 源碼調研完整報告

> **系統**: 公共工程電腦估算系統  
> **版本**: 4.3.1000.220  
> **開發商**: 聯宏資通股份有限公司（Archnowledge）  
> **調研日期**: 2026 年  
> **調研目的**: 將現有 WinForms 桌面版改寫為網頁版

---

## 目錄

1. [系統概述](#一系統概述)
2. [技術棧分析](#二技術棧分析)
3. [模組架構](#三模組架構)
4. [關鍵類別與職責](#四關鍵類別與職責)
5. [資料庫架構](#五資料庫架構)
6. [主程式流程](#六主程式流程)
7. [網頁版遷移藍圖](#七網頁版遷移藍圖)

---

## 一、系統概述

PCCES（Public Construction Cost Estimation System）是台灣公共工程領域的核心預算編製系統，主要功能涵蓋：

| 功能領域 | 說明 |
|---------|------|
| **工程預算書編製** | 建立公共工程預算書，包含工項、單價、數量分析 |
| **招標文件產出** | 由預算轉換為招標用標單（Bid） |
| **分包/計價管理** | 分包合約管理、分包結算、終驗、計價請款 |
| **工項單價資料庫** | 維護公定/自訂的工項基本單價資料庫（MrsBase） |
| **系統管理** | 使用者、角色、群組、功能權限管理 |
| **報表匯出** | 支援 Excel（Aspose.Cells）、Crystal Reports 格式輸出 |

### 系統定位

```
公共工程生命週期 ─→ PCCES 涵蓋範圍
  規劃設計 ─→ 預算編製 ✓
  招標發包 ─→ 標單產出 ✓
  施工階段 ─→ 分包計價 ✓
  竣工結算 ─→ 結算終驗 ✓
  維護營運 ─→ (未涵蓋)
```

---

## 二、技術棧分析

### 2.1 核心技術

| 層次 | 技術 | 備註 |
|------|------|------|
| **語言** | C# 12.0（.NET Framework 4.8） | LangVersion=12.0 |
| **UI 框架** | Windows Forms（WinForms） | UseWindowsForms=True |
| **架構模式** | 分層式 UI → DomainModule DLL → DatabaseAccess DLL | 無 ASP.NET / MVC |
| **資料庫存取** | ADO.NET（OleDb + SqlClient） | 雙重連線模式 |
| **平台目標** | x86（32-bit） | 相容舊版 COM 元件 |
| **授權驗證** | Windows 整合驗證（SSPI） | |

### 2.2 第三方套件完整清單

從 `.csproj` 專案檔提取：

| 類別 | 套件名稱 | 用途 |
|------|---------|------|
| **UI 框架** | Infragistics.Win.v3.1 | 基礎 UI 框架 |
| | Infragistics.Shared.v3.1 | 共用基礎 |
| | Infragistics.Win.UltraWinTabbedMdi.v3.1 | MDI 分頁管理 |
| | Infragistics.Win.UltraWinGrid.v3.1 | 進階資料表格 |
| | Infragistics.Win.UltraWinToolbars.v3.1 | 工具列 |
| | Infragistics.Win.UltraWinEditors.v3.1 | 編輯器控制項 |
| | Infragistics.Win.UltraWinTabControl.v3.1 | 分頁控制 |
| | Infragistics.Win.UltraWinTree.v3.1 | 樹狀目錄 |
| | Infragistics.Win.UltraWinStatusBar.v3.1 | 狀態列 |
| | Infragistics.Win.UltraWinSchedule.v3.1 | 排程 |
| | Infragistics.Win.Misc.v3.1 | 雜項控制項 |
| | Infragistics.Win.UltraWinChart.v4 | 圖表 |
| **表格** | C1.Win.C1FlexGrid | 彈性表格 |
| | C1.Win.C1Command | 命令列 |
| | C1.Win.C1Sizer | 版面調整 |
| | C1.Win.C1Input | 輸入控制 |
| **Excel** | Aspose.Cells | 主要 Excel 匯出引擎（含授權檔） |
| | C1.C1Excel | 備用 Excel 操作 |
| **報表** | Crystal Reports（Crp92Ocx） | 報表檢視器 |
| **PDF** | Adobe Acrobat ActiveX（AxInterop.AcroPDFLib） | PDF 預覽 |
| **WebService** | ASMX 風格（.NET 2.0） | 更新檢查/資料查詢 |
| **資料庫** | ADODB / ADOX（Interop） | COM 資料庫存取 |
| **3D** | PVLINE3DLib / PVMarqueeLib / PVTEXT3DLib | 3D 跑馬燈效果 |

### 2.3 封閉商業 DLL（無源碼）

這些 DLL 安裝在 `C:\Program Files (x86)\PccesWin4.3\` 目錄下，**專案中僅有二進位參考，無原始碼**：

#### 領域層（DomainModule）

| DLL 名稱 | 功能 |
|----------|------|
| `Archnowledge.Pcces.DomainModule.Budget` | 預算領域模組 — 預算計算核心 |
| `Archnowledge.Pcces.DomainModule.MrsBase` | 工項單價資料庫模組 |
| `Archnowledge.Pcces.DomainModule.Bid` | 標單領域模組 |
| `Archnowledge.Pcces.DomainModule.Sub` | 分包領域模組 |
| `Archnowledge.Pcces.DomainModule.SubChg` | 分包變更模組 |
| `Archnowledge.Pcces.DomainModule.CostStructure` | 成本結構模組 |
| `Archnowledge.Pcces.DomainModule.General` | 通用領域（PubDecimal、UserDefined等） |
| `Archnowledge.Pcces.DomainModule.BusinessLogical` | 業務邏輯層 — 預算計算引擎 |
| `Archnowledge.Pcces.DomainModule.CostEstQuoation` | 成本估算報價模組 |
| `Archnowledge.Pcces.DomainModule.ExportExcel` | Excel 匯出領域模組 |
| `Archnowledge.Pcces.DomainModule.DatabaseUpgrade` | 資料庫版本升級 |
| `Archnowledge.Pcces.DomainModule.DatabaseChange` | 資料庫結構變更 |
| `Archnowledge.Pcces.DomainModule.Coms` | COMS 公共工程管理系統整合 |
| `Archnowledge.Pcces.DomainModule.BudExe` | 預算執行模組 |
| `Archnowledge.Pcces.DomainModule.LogicalBase` | 邏輯基礎類別 |
| `Archnowledge.Pcces.XML` | XML 處理 |
| `Archnowledge.DatabaseAccess` | 資料庫存取層 |
| `Archnowledge.Common` | 通用工具（CommonMethods、ArchNet） |
| `Archnowledge.Common.Compress` | 壓縮/解壓 |

#### 核心類別庫

| DLL 名稱 | 功能 |
|----------|------|
| `stdclass` | 標準類別庫（StaffClass、UserClass） |
| `budclass` | 預算類別庫（BUDClass） |
| `PowerClass` | 權限管理類別 |
| `ctrclass` | 合約類別庫（CTRClass） |
| `repclass` | 報表類別庫（REPClass） |
| `CommomClass` | 通用類別庫（命名筆誤） |
| `TRAClass` | 台鐵專用類別庫 |

> ⚠️ **最大障礙提醒**：以上所有 DLL 均為封閉商業元件，**無法取得源碼**。網頁版必須：
> 1. 從 UI 層對這些 DLL 的呼叫方式，**反向推導業務邏輯**
> 2. 或取得 Archnowledge 授權重新編譯
> 3. 或完全重新實作

---

## 三、模組架構

### 3.1 原始碼目錄結構

```
PCCES_CS/
├── PCCES_CS.sln                    # 解決方案檔
├── PccesMain.csproj                # 專案檔 (.NET 4.8 win-x86)
├── app.config                      # 資料庫連線設定
├── app.manifest                    # 應用程式資訊清單
│
├── Archnowledge.Pcces.PccesMain/          # **主命名空間 (UI 層)**
│   ├── frmPccesMain.cs             # 主視窗 (~1,308 行)
│   ├── DBClass.cs                  # 資料庫連線 (~2,625 行)
│   ├── Conversion.cs               # 轉換匯出 (~25,897 行) ⚡最大檔案
│   ├── FormLogin.cs                # 登入視窗
│   ├── FormSplash.cs               # 啟動畫面
│   ├── FormRegister.cs             # 註冊視窗
│   ├── FormMessage.cs              # 訊息視窗
│   ├── FormPanel.cs / Panel2 / Panel3  # 首頁面板
│   ├── PccesHelp.cs                # 說明
│   ├── PDFForm.cs / PDFErrorForm.cs # PDF 檢視
│   │
│   ├── Project/                    # 📁 **專案管理**
│   │   ├── FormProject.cs          # 專案清單主視窗
│   │   ├── FormProjectEdit.cs      # 專案編輯
│   │   ├── FormProjectClone.cs     # 專案複製
│   │   ├── FormProjectBidToBud.cs  # 標單轉預算
│   │   ├── formNewProjectWizard.cs # 新增專案精靈
│   │   └── uccShowProject.cs       # 專案顯示控制項
│   │
│   ├── Budget/                     # 📁 **預算書 (核心)**
│   │   ├── frmBudget.cs            # 預算編輯主視窗
│   │   ├── FormBudgetEditMain.cs   # 預算編輯主體
│   │   ├── FormBudgetCombine.cs    # 預算合併
│   │   ├── FormBudgetSplit.cs      # 預算拆分
│   │   ├── FormBudgetRes.cs        # 資源管理
│   │   ├── FormBudgetExp_Wzd.cs    # 預算匯出精靈
│   │   ├── FormBudgetSelfExam.cs   # 預算自我檢查
│   │   ├── FormBudgetPCalsCustomEdit.cs # PCals 自訂編輯
│   │   ├── FormBudgetPCalsCustomVar.cs  # PCals 自訂變數
│   │   ├── FormOpenExcel.cs        # Excel 開啟
│   │   ├── FormPickProjWkItem_Wzd.cs # 工項選取精靈
│   │   ├── FormDownloadDoc.cs      # 文件下載
│   │   ├── FormMemo.cs             # 備註
│   │   ├── FormDEBUG.cs            # 除錯
│   │   └── ... (共 ~50+ 檔案)
│   │   │
│   │   ├── BudgetChange/           # 預算變更
│   │   │   ├── FormBudgetChangeHistory.cs
│   │   │   ├── FormBudgetChangeInfo.cs
│   │   │   ├── FormBudgetChangeInfoPicker.cs
│   │   │   ├── FormBudgetChangeResponsibility.cs
│   │   │   ├── FormBudgetWorkItemChangeHistory.cs
│   │   │   ├── FormComsApplyDetailList.cs
│   │   │   ├── FormCostEstProjectList.cs
│   │   │   ├── FormDataExport_Wzd.cs
│   │   │   └── FormBudgetSubInfo.cs
│   │   │
│   │   ├── ItemNoset/              # 項次設定
│   │   │   ├── FormBDGT_ItemClass.cs
│   │   │   ├── FormBDGT_ItemSetCheck.cs
│   │   │   ├── FormBDGT_ItemSetGPS.cs
│   │   │   ├── FormBDGT_ItemSetMaintain.cs
│   │   │   ├── FormBDGT_ItemSetNewName.cs
│   │   │   └── FormBDGT_TemplateClass.cs
│   │   │
│   │   └── Option/                 # 預算選項設定
│   │       ├── FormBDGT_OptionMain.cs
│   │       ├── FormBDGT_OptionMain_Help1.cs
│   │       └── FormBDGT_SetMain.cs
│   │
│   ├── MrsBase/                    # 📁 **工項單價庫 (核心)**
│   │   ├── frmMrsBase.cs           # 單價庫主視窗
│   │   ├── FormMrsBaseEdit.cs      # 單價編輯
│   │   ├── FormMrsBaseFind.cs      # 單價搜尋
│   │   ├── FormMrsBaseBreakdown.cs # 單價分解
│   │   ├── FormMrsBaseApprove.cs   # 單價審核
│   │   ├── FormMrsBase_ExpWizard.cs # 匯出精靈
│   │   ├── FormMrsBase_ImpWizard.cs # 匯入精靈
│   │   ├── FormMrsBaseDecimal.cs   # 小數位設定
│   │   ├── FormMrsBaseChgCode.cs   # 編碼變更
│   │   ├── FormCommMrsImport.cs    # 共同單價匯入
│   │   ├── FormAutoNum.cs          # 自動編號
│   │   ├── FormAutoNumCustomEdit.cs # 自訂編號編輯
│   │   ├── FormAutoNumCreateChapterCode.cs # 章節碼產生
│   │   ├── FormAutoNumFind.cs      # 編號查詢
│   │   ├── FormAutoNum_LiveUpdate.cs # 即時更新
│   │   ├── FormAutosurName.cs      # 自動名稱
│   │   ├── FormConCost.cs          # 成本查詢
│   │   ├── FormConCost_Upd.cs      # 成本更新
│   │   ├── FormMrsParentFind.cs    # 母項查詢
│   │   ├── FormMrsBaseBreakdownIR.cs # 國際招標分解
│   │   ├── FormMrsBase_DeleteMessage.cs # 刪除確認
│   │   ├── FormMrsBaseBreakdown_Addnew.cs # 新增分解
│   │   └── ... (共 ~26 檔案)
│   │   │
│   │   ├── Bookmark/               # 書籤管理
│   │   │   └── FormMrsBase_BookmarkRemove.cs
│   │   │
│   │   └── PickFromOther/          # 他庫選取
│   │       ├── FormMrsBase_PickFromOtherDB.cs
│   │       └── ...
│   │
│   ├── Invoice/                    # 📁 **計價管理**
│   │   ├── FormInvoice.cs          # 計價主視窗
│   │   ├── FormInvoiceDec2.cs      # 計價編輯
│   │   ├── FormInvoiceExport.cs    # 計價匯出
│   │   ├── FormInvoiceImport.cs    # 計價匯入
│   │   ├── FormInvoiceGraphic.cs   # 計價圖形化
│   │   ├── FormInvoiceProgress.cs  # 計價進度
│   │   ├── FormInvoiceSummary.cs   # 計價摘要
│   │   ├── FormInvoiceIndexNumber.cs # 指數編號
│   │   └── FormInvoiceSubAcInfo.cs # 子項會計資訊
│   │
│   ├── SplitContract/              # 📁 **分包合約**
│   │   ├── FormSplitContract.cs    # 分包合約主視窗
│   │   ├── FormSplitCnt_Basic.cs   # 基本資料
│   │   ├── FormSplitCnt_EdtIssue.cs # 期別編輯
│   │   ├── FormSplitCnt_ItemPick.cs # 工項選取
│   │   └── FormSplitCnt_ResetCost.cs # 成本重設
│   │
│   ├── SubClose/                   # 📁 **分包結算**
│   │   ├── FormSubClose.cs         # 結算主視窗
│   │   ├── FormSubCloseInfo.cs     # 結算資訊
│   │   └── FormSubCloseInput.cs    # 結算輸入
│   │
│   ├── SubFinal/                   # 📁 **分包終驗**
│   │   ├── FormSubFinal.cs         # 終驗主視窗
│   │   ├── FormSubFinalInput.cs    # 終驗輸入
│   │   └── FormSubFinal_ItemPick.cs # 工項選取
│   │
│   ├── Compare/                    # 📁 **比較分析**
│   │   ├── FormCompareItm.cs       # 工項比較
│   │   ├── FormCompareMrs.cs       # 單價比較
│   │   ├── FormCompareMrsAna.cs    # 單價分析比較
│   │   └── FormCompareItm_Scope.cs # 比較範圍
│   │
│   ├── Report/                     # 📁 **報表系統**
│   │   ├── FormInvoiceReport.cs    # 計價報表
│   │   ├── FormReportViewer.cs     # 報表檢視器
│   │   ├── ucCrystalViewer.cs      # Crystal Reports 檢視控制項
│   │   ├── ucSubAcc.cs / ucSubChg.cs / ucSubClose.cs / ucSubCtr.cs / ucSubFinal.cs
│   │   └── WebDownload/            # 網路下載引擎
│   │       ├── DownloadThread.cs
│   │       ├── DownloadCompleteHandler.cs
│   │       ├── DownloadFailHandler.cs
│   │       ├── DownloadProgressHandler.cs
│   │       └── RequestState.cs
│   │
│   ├── SysMaintain/                # 📁 **系統維護**
│   │   ├── frmSysMaintain.cs       # 系統維護主視窗
│   │   ├── FormSys_A.cs            # 使用者管理
│   │   ├── FormSys_A_Edit.cs       # 使用者編輯
│   │   ├── FormSys_A_GrpMember.cs  # 群組成員
│   │   ├── FormSys_A_UsrGroup.cs   # 使用者群組
│   │   ├── FormSys_B.cs            # 功能權限設定
│   │   ├── FormSys_C.cs            # 部門/單位設定
│   │   ├── FormSys_C_Edit.cs       # 部門編輯
│   │   ├── FormSys_D.cs            # 公物編碼
│   │   ├── FormSys_D_Pick.cs       # 編碼選取
│   │   ├── FormSys_E.cs            # 系統參數 E
│   │   ├── FormSys_F.cs            # 系統參數 F
│   │   ├── FormSys_G.cs            # 系統參數 G
│   │   ├── FormSys_G1.cs           # 系統參數 G1
│   │   ├── FormSys_G_Info1.cs      # 參數說明
│   │   ├── FormSys_I.cs            # 個人設定
│   │   ├── FormSys_J.cs            # 專業工程
│   │   ├── FormSys_Z.cs            # 代碼轉換
│   │   ├── OrganizationPicker.cs   # 組織選取
│   │   ├── CostStructureTypePicker.cs # 成本結構類型選取
│   │   ├── CostStructureImport.cs  # 成本結構匯入
│   │   └── DatabaseNamingDialog.cs # 資料庫命名
│   │
│   ├── ArchControls/               # 📁 **自訂控制項**
│   │   ├── GridBudget.cs           # 預算表格控制項
│   │   ├── GridMrsBase.cs          # 單價表格控制項
│   │   ├── FunctionButtons.cs      # 功能按鈕列
│   │   ├── LevelSwitchButton.cs    # 層級切換按鈕
│   │   ├── OnlineList.cs           # 線上使用者清單
│   │   ├── FormChangeUserInfo.cs   # 使用者資訊變更
│   │   └── ProjectInfoSummaryControls/ # 專案摘要控制項
│   │       ├── BridgeSummary.cs     # 橋樑工程摘要
│   │       ├── TunnelSummary.cs     # 隧道工程摘要
│   │       ├── HighwaySummary.cs    # 公路工程摘要
│   │       ├── BuildingSummary.cs   # 建築工程摘要
│   │       └── RailTunnelSummary.cs # 鐵路隧道摘要
│   │
│   ├── Library/                    # 📁 **共用函式庫**
│   │   ├── ModuleManager.cs        # 模組管理器
│   │   ├── ItemNoSettingManager.cs # 項次設定管理器
│   │   ├── GridPropertySetting.cs  # 表格屬性設定
│   │   ├── DatabaseBackupRestore.cs# 資料庫備份還原
│   │   ├── ComsWebService.cs       # COMS Web Service
│   │   ├── ComsExpandBudget.cs     # COMS 預算展開
│   │   └── ...                     # 下載引擎等
│   │
│   ├── ShellLib/                   # 📁 Shell API
│   │   ├── ShellExecute.cs         # Shell 執行
│   │   └── ShellApi.cs             # Win32 API 封裝
│   │
│   ├── com.archnowledge.bisc/      # 📁 BISC WebService
│   │   ├── Service1.cs             # Web Service 代理
│   │   ├── GetCostListCompletedEventArgs.cs
│   │   ├── GetCostKindCompletedEventArgs.cs
│   │   └── ... (共 5 檔案)
│   │
│   ├── WSCode/                     # 📁 版本管理 WebService
│   │   ├── WSCode.cs               # 主服務代理
│   │   ├── ReEditionNameCompletedEventArgs.cs
│   │   ├── ReDataDocCompletedEventArgs.cs
│   │   ├── GetChapterInfoCompletedEventArgs.cs
│   │   └── ... (共 7+ 檔案)
│   │
│   ├── PccesUpdateServices/        # 📁 更新服務 (~28 檔案)
│   │   ├── Update.cs               # Update.asmx 代理
│   │   └── ...                     # 自動編號更新、版本檢查、註冊
│   │
│   ├── Railway1/                   # 📁 台鐵專用
│   │   ├── TRA_Service.cs          # 台鐵服務
│   │   ├── OutputMrsCompletedEventArgs.cs
│   │   ├── InputMrsCompletedEventArgs.cs
│   │   └── GetProjectCodeCompletedEventArgs.cs
│   │
│   ├── SysPlugin/                  # 📁 系統插件
│   │   └── FormSysPlugin.cs        # 插件管理
│   │
│   ├── BudgetChange/               # 📁 預算變更(獨立)
│   │   ├── FormBudgetChange.cs
│   │   ├── FormBudgetChange_Addnew.cs
│   │   └── FormBudgetEditItem.cs
│   │
│   ├── _Customize.Z14AC1100/       # 📁 客製化模組
│   │   └── FormSynchronize.cs      # 資料同步表單
│   │
│   └── Properties/                 # 專案屬性
│       ├── AssemblyInfo.cs
│       ├── Resources.cs
│       └── Settings.cs
│
└── PCCES.CODECHECK/                # 程式碼檢查工具
    ├── CodeValidator.cs
    ├── CodeFitter.cs
    ├── CommonWorkItems.cs
    └── MrsBaseData.cs
```

### 3.2 模組功能對照表

| 模組目錄 | 模組名稱 | 功能說明 | 重要性 | 檔案數 |
|---------|---------|---------|--------|-------|
| `PccesMain`（根） | 主框架 | 主視窗、登入、DBClass、轉換匯出、更新服務 | ★★★★★ | ~18 |
| `Project/` | 專案管理 | 新增/編輯/複製/清單/精靈 | ★★★★☆ | 6 |
| `Budget/` | **預算書** | 預算編輯、合併拆分、資源分配、自我檢查、PCals | ★★★★★ | ~50+ |
| `Budget/BudgetChange/` | 預算變更 | 變更歷程/資訊/責任歸屬/經費估算 | ★★★☆☆ | 9 |
| `Budget/ItemNoset/` | 項次設定 | 項次類別、檢查、GPS、維護、更名、範本 | ★★★☆☆ | 6 |
| `Budget/Option/` | 預算選項 | 選項設定、主設定畫面 | ★★☆☆☆ | 3 |
| `MrsBase/` | **工項單價庫** | 基本單價編輯/搜尋/分解/審核/匯出入/自動編號 | ★★★★★ | ~26 |
| `Invoice/` | **計價管理** | 計價編輯/匯出入/圖形化/進度/摘要 | ★★★★★ | 10 |
| `SplitContract/` | 分包合約 | 合約管理、期別、工項選取、成本重設 | ★★★★☆ | 5 |
| `SubClose/` | 分包結算 | 結算編輯/資訊/輸入 | ★★★☆☆ | 3 |
| `SubFinal/` | 分包終驗 | 終驗編輯/輸入/工項選取 | ★★★☆☆ | 3 |
| `SysMaintain/` | **系統維護** | 使用者/權限/部門/編碼/參數/個人設定 | ★★★★☆ | 22 |
| `Report/` | 報表系統 | Crystal Reports 檢視、計價報表、子項報表 | ★★★☆☆ | 9 |
| `Compare/` | 比較分析 | 工項/單價/分析比較 | ★★☆☆☆ | 4 |
| `ArchControls/` | 自訂控制項 | 預算表格、功能按鈕、層級切換、專案摘要 | ★★★★☆ | 8 |
| `Library/` | 共用函式庫 | ModuleManager、備份還原、ComsWebService | ★★★☆☆ | 9 |
| `ShellLib/` | Shell API | Win32 API 封裝 | ★☆☆☆☆ | 2 |

---

## 四、關鍵類別與職責

### 4.1 核心類別列表

| 類別 | 檔案路徑 | 行數 | 職責 |
|------|---------|------|------|
| `frmPccesMain` | `Archnowledge.Pcces.PccesMain/frmPccesMain.cs` | ~1,308 | 主視窗框架、MDI 分頁管理、功能區塊導航、事件總線 |
| `DBClass` | `Archnowledge.Pcces.PccesMain/DBClass.cs` | ~2,625 | 資料庫連線生命週期、OleDb/SqlClient 切換、交易包裝、多用戶連線 |
| `Conversion` | `Archnowledge.Pcces.PccesMain/Conversion.cs` | ~25,897 | **全系統最大檔案** — Excel 匯出、預算轉標單、報表生成、各類文件轉換 |
| `FormLogin` | `Archnowledge.Pcces.PccesMain/FormLogin.cs` | ~200 | 使用者認證、帳密驗證（StaffClass.ChkLogon） |
| `FormSplash` | `Archnowledge.Pcces.PccesMain/FormSplash.cs` | — | 啟動畫面顯示 |
| `FormRegister` | `Archnowledge.Pcces.PccesMain/FormRegister.cs` | — | 線上註冊/授權管理 |
| `FormModuleSetup` | `Archnowledge.Pcces.PccesMain/FormModuleSetup.cs` | — | 模組啟用/停用設定、按鈕配置 |
| `ModuleManager` | `Library/ModuleManager.cs` | — | 各業務模組的統一調度入口 |
| `frmBudget` | `Budget/frmBudget.cs` | — | 預算書編輯主視窗、樹狀工項列表、數量/單價編輯 |
| `frmMrsBase` | `MrsBase/frmMrsBase.cs` | — | 工項單價資料庫主視窗、單價瀏覽/搜尋/編輯 |
| `FormProject` | `Project/FormProject.cs` | — | 專案清單管理 |
| `FormInvoice` | `Invoice/FormInvoice.cs` | — | 計價請款主視窗 |
| `FormSplitContract` | `SplitContract/FormSplitContract.cs` | — | 分包合約管理 |
| `GridBudget` | `ArchControls/GridBudget.cs` | — | 預算表格（C1FlexGrid 封裝） |
| `GridMrsBase` | `ArchControls/GridMrsBase.cs` | — | 單價表格封裝 |
| `FunctionButtons` | `ArchControls/FunctionButtons.cs` | — | 功能按鈕列（動態產生） |
| `frmSysMaintain` | `SysMaintain/frmSysMaintain.cs` | — | 系統維護主視窗 |
| `FormReportViewer` | `Report/FormReportViewer.cs` | — | 報表檢視器（Crystal Reports 宿主） |

### 4.2 Conversion.cs 深入分析

`Conversion.cs`（25,897 行）是全系統最龐大的檔案，包含：

| 功能區塊 | 行數推估 | 說明 |
|---------|---------|------|
| 預算轉標單（Bid B→B 轉換） | ~3,000 | 預算書 → 招標標單 |
| Budget → Excel 匯出 | ~5,000 | 使用 Aspose.Cells |
| 報表產生 | ~4,000 | 各類制式報表格式 |
| Budget Change 相關 | ~2,000 | 變更預算相關匯出 |
| MrsBase 相關 | ~2,000 | 工項單價相關匯出 |
| Invoice 相關 | ~2,000 | 計價相關匯出 |
| 其他工具函數 | ~7,000+ | 共用方法、格式處理 |

---

## 五、資料庫架構

### 5.1 基本資訊

| 項目 | 內容 |
|------|------|
| **DBMS** | Microsoft SQL Server（含 Express） |
| **驗證方式** | Windows 整合驗證（SSPI） |
| **主資料庫** | `Pcces` |
| **參考資料庫** | `Pcces43`（公定單價參考資料庫） |
| **連線設定** | `app.config` → `connectionStrings/Pcces` |
| **ADO 層** | OleDbConnection + SqlConnection 雙模式 |

### 5.2 app.config 設定

```xml
<appSettings>
  <add key="Conn" value="Provider=SQLOLEDB.1;...Initial Catalog=Pcces;
       Data Source=ug912-1\sqlexpress;Connect Timeout=30"/>
  <add key="ServerName" value="localhost"/>
  <add key="CanChangeDataBase" value="true"/>
  <add key="UseNewChangDataBase" value="True"/>
  <add key="UseNewMrsB" value="true"/>
  <add key="EnableMasterSlave" value="false"/>
</appSettings>
```

### 5.3 DBClass 資料庫操作層

`DBClass.cs`（2,625 行）提供的關鍵功能：

- `ConnectionString` — 從 app.config 讀取連線字串
- `DbConn` / `DbAdpt` / `DbComm` — OleDb 三件套
- `GetMultiUserConnection2()` — 多用戶連線（支援 SqlConnection 模式）
- `CheckConnection()` / `CheckAlive()` — 連線檢查與保持
- 交易控制（Begin/Commit/Rollback）
- DataTable / DataSet 查詢

### 5.4 資料庫表格推斷

從程式碼中可推斷的主要資料表：

| 資料表 | 推斷用途 | 來源 |
|--------|---------|------|
| `Budget` | 預算書主表 | frmBudget |
| `BudgetItem` | 預算工項明細 | Budget 模組 |
| `BudgetRes` | 預算資源 | FormBudgetRes |
| `MrsBase` | 工項基本單價庫 | frmMrsBase |
| `MrsBaseBreakdown` | 單價分解表 | FormMrsBaseBreakdown |
| `Users` / `UserGroup` | 使用者/群組 | Sys_A |
| `ModuleSetting` | 模組設定 | FormModuleSetup |
| `Project` | 專案主檔 | FormProject |
| `Invoice` | 計價主表 | FormInvoice |
| `InvoiceItem` | 計價明細 | FormInvoice |
| `SplitContract` | 分包合約 | FormSplitContract |
| `SubClose` / `SubFinal` | 結算/終驗 | SubClose / SubFinal |
| `SystemParameter` | 系統參數 | Sys_E / Sys_F / Sys_G |

---

## 六、主程式流程

```
Program.Main()
  │
  ├── frmPccesMain 實例化
  │     │
  │     ├── CheckPccesUser() → DatabaseAccess.GetSQLVersion()
  │     │     └── 檢查 SQL Server 版本 + 驗證連線
  │     │
  │     ├── ShowSplash() → FormSplash (啟動畫面)
  │     │
  │     ├── LoadingForm()
  │     │     └── 依 HomePanel 設定載入：
  │     │           ├── FormPanel (風格1) — 功能選單
  │     │           ├── FormPanel2 (風格2)
  │     │           └── FormPanel3 (風格3) — 儀表板風格
  │     │
  │     ├── frmPccesMain_Load()
  │     │     ├── 設定視窗位置/大小
  │     │     └── 連線 ChatServer (線上狀態)
  │     │
  │     ├── functionButtons1_Load() → 登入流程
  │     │     ├── 嘗試自動匿名登入 (PccAdmin)
  │     │     ├── 失敗 → 顯示 FormLogin
  │     │     │     └── StaffClass.ChkLogon(user, pwd, ip, host)
  │     │     ├── 登入成功 → _UserID / _UserName 設定
  │     │     ├── CheckDatabaseVersion() → 自動資料庫升級
  │     │     ├── CheckRegister() → FormRegister
  │     │     └── FormModuleSetup → ModuleManager 載入模組
  │     │
  │     └── frmPccesMain_Activated()
  │           └── CheckUpdate() → Update.asmx 檢查新版
  │
  └── Application.Run(frmPccesMain) → MDI 子視窗管理
        ├── 各模組視窗透過 UltraTabbedMdiManager 管理分頁
        ├── 功能按鈕動態產生
        └── 各 Form 由 ModuleManager 統一調度
```

---

## 七、網頁版遷移藍圖

### 7.1 遷移策略建議

| 策略 | 適用範圍 | 優點 | 缺點 |
|------|---------|------|------|
| **策略 A：完全重寫** | 全部 | 技術架構統一、無歷史包袱 | 耗時長、業務邏輯需重新驗證 |
| **策略 B：Wrapper 包裝** | 封閉 DLL 部分 | 保留現有 DomainModule | ActiveX 無法包裝、WinForms 控制項不相容 |
| **策略 C：漸進式重構** | 先核心模組 + 後周邊 | 可分批上線、風險控管 | 較長的過渡期 |

**建議採用策略 C + A 混和**：核心業務邏輯（預算、單價、計價）完全重寫，但參考現有程式碼的行為定義規格。

### 7.2 技術選型建議

| 層次 | 建議技術 | 替代 WinForms 方案 |
|------|---------|-------------------|
| **前端框架** | React / Vue 3 + TypeScript | 取代 WinForms UI |
| **UI 元件庫** | Ant Design / Element Plus | 取代 Infragistics + C1FlexGrid |
| **後端** | .NET 8+ Web API | 取代 DomainModule DLL |
| **資料庫 ORM** | Entity Framework Core | 取代 OleDb + SqlClient |
| **認證** | JWT + Identity | 取代 SSPI + StaffClass |
| **報表** | 自定義 React 報表 / jsPDF / 後端產生 | 取代 Crystal Reports |
| **Excel** | EPPlus / ClosedXML（開源） | 取代 Aspose.Cells |
| **快取** | Redis | 取代 INI + Registry |

### 7.3 分階段實施建議

| 階段 | 內容 | 時程推估 |
|------|------|---------|
| **Phase 1** | 系統維護 + 登入/權限 + 專案管理 | 基礎建設 |
| **Phase 2** | 預算書模組（核心） | 最重的工作 |
| **Phase 3** | 工項單價資料庫（MrsBase） | 次核心 |
| **Phase 4** | 計價管理 + 分包合約 | 延伸功能 |
| **Phase 5** | 結算/終驗 + 比較分析 | 末期功能 |
| **Phase 6** | 報表系統 + 匯出入 + 台鐵整合 | 整合收尾 |

### 7.4 風險與緩解

| 風險 | 影響 | 緩解方案 |
|------|------|---------|
| 封閉 DLL 無源碼 | 🔴 無法直接移植業務邏輯 | 透過 UI 層反向推導；與 Archnowledge 協商提供 API 文件 |
| WinForms 複雜表格互動 | 🟡 網頁版難以完全比照 | 先簡化 UX 再逐步優化 |
| 25,897 行 Conversion.cs | 🟡 邏輯分散難拆解 | 逐一功能單元測試後重構 |
| Crystal Reports 報表 | 🔴 無網頁版 | 改用後端產生 PDF/Excel |
| ActiveX 控制項 | 🔴 完全不支援網頁 | Adobe PDF → 改用 PDF.js；3D 效果去除 |
| Windows 整合驗證 | 🟡 需改為表單驗證 | 設計 JWT 認證流程 |
| 大量 DataTable 直接操作 | 🟡 ADO.NET 緊耦合 | 抽象 Repository 模式 |
