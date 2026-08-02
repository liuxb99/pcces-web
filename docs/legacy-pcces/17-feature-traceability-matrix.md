# PCCES C# → Web 功能追蹤矩陣

更新日期：2026-08-02

## 狀態說明

- `NOT_STARTED`：尚無對應。
- `UI_ONLY`：只有頁面或元件。
- `PARTIAL`：已有部分流程，但規則、權限或副作用不完整。
- `IMPLEMENTED`：依規格完成，尚未證明與 Legacy 一致。
- `LEGACY_MATCHED`：已通過 Legacy 對照案例。
- `VERIFIED`：完整測試及整合驗證通過。

## LEGACY-001：啟動、登入與主框架

| Feature ID | 桌面版功能 | C# 來源 | 證據 | Web 對應 | 狀態 | 主要缺口 |
|---|---|---|---|---|---|---|
| APP-START-001 | 程式進入點與唯一程序 | `frmPccesMain.Main` | `CONFIRMED` | Serverless／Web 啟動 | `PARTIAL` | 缺 PCCES Schema 支援檢查、工作鎖語意 |
| APP-START-002 | PCCES DB 帳號及結構預檢 | `CheckPccesUser` | `CONFIRMED` | `/api/health` | `PARTIAL` | Health 未證明資料庫為有效 PCCES |
| APP-START-003 | SQL 2000 禁止啟動 | `frmPccesMain.Main` | `CONFIRMED` | 未定位 | `NOT_STARTED` | 缺資料庫版本相容政策 |
| APP-START-004 | zh-TW 與 Gregorian Calendar | `frmPccesMain.Main` | `CONFIRMED` | 前端 zh-TW | `PARTIAL` | 後端日期、時區與曆法未形成契約 |
| APP-START-005 | Splash 與預連線 | `frmPccesMain()` | `CONFIRMED` | Landing／Loading | `UI_ONLY` | 未復刻預連線成功才載入功能框架 |
| APP-SHELL-001 | 首頁面板 1/2/3 選擇 | `LoadingForm` | `CONFIRMED` | Dashboard | `UI_ONLY` | 缺首頁偏好及三種面板語意 |
| APP-SHELL-002 | MDI 子視窗唯一性 | `LoadingForm` | `CONFIRMED` | React Router | `PARTIAL` | 路由存在，但未證明狀態實例唯一與重用 |
| APP-SHELL-003 | OnlineList 連線 | `frmPccesMain_Load` | `CONFIRMED` | 未定位 | `UNKNOWN` | 必須先確認是 Presence、Lock 或 Chat |
| APP-SHELL-004 | 主框架 Freeze／Enable | `DisableMain` / `EnableMain` | `CONFIRMED` | 未定位 | `NOT_STARTED` | 缺全域阻塞狀態與交易保護 |
| APP-SHELL-005 | 視窗位置尺寸保存 | `Load` / `FormClosing` | `CONFIRMED` | 瀏覽器 UI 偏好 | `NOT_STARTED` | 可降級為非核心 UX 相容項 |
| APP-CLOSE-001 | 一般關閉確認 | `FormClosing` | `CONFIRMED` | Browser unload | `PARTIAL` | 缺一致的 dirty-state 管理 |
| APP-CLOSE-002 | Freeze 時禁止關閉 | `FormClosing` | `CONFIRMED` | 未定位 | `NOT_STARTED` | 缺關鍵操作全域離開閘門 |
| APP-CLOSE-003 | 預算狀態禁止主程式關閉 | `BDGT_DONT_CLOSE` | `CONFIRMED` | Budget editor | `NOT_STARTED` | 缺預算專用生命週期狀態 |
| AUTH-001 | 從 INI 讀取上次 UserID | `FormLogin_Load` | `CONFIRMED` | LoginPage | `NOT_STARTED` | 未確認保存最近帳號 |
| AUTH-002 | 有帳號時聚焦密碼 | `FormLogin_Activated` | `CONFIRMED` | LoginPage | `NOT_STARTED` | UX 細節待補 |
| AUTH-003 | 共用合法字串驗證 | `txtUserID_Validating` | `CONFIRMED` | 前後端 validation | `PARTIAL` | 尚未取得 `CheckValidString` 真實規則 |
| AUTH-004 | 帳密、IP、Machine 驗證 | `BtnOK_Click` | `CONFIRMED` | `/api/auth/login` | `PARTIAL` | Web 未保存 Machine；IP 與 Legacy 規則未對照 |
| AUTH-005 | 取得 UserID/UserName | `UserClass.ListItem` | `CONFIRMED` | Token response | `PARTIAL` | 尚未確認角色、功能權限快照 |
| AUTH-006 | 登入操作日誌 | `PubTools.WriteRoughlyLog` | `CONFIRMED` | 未定位 | `NOT_STARTED` | 缺登入稽核紀錄 |
| AUTH-007 | 登入成功保存 UserID | `IniWriteValue` | `CONFIRMED` | localStorage token | `PARTIAL` | 保存內容與安全目的不同 |
| AUTH-008 | 統一帳密錯誤、清空密碼 | `BtnOK_Click` | `CONFIRMED` | LoginPage | `PARTIAL` | 需驗證 UI 行為與錯誤結構 |
| AUTH-009 | 取消登入改變主程式生命週期 | `BtnCancel_Click` | `CONFIRMED` | 未定位 | `NOT_STARTED` | Web 取消／登出後續狀態未定義 |
| AUTH-010 | 匿名登入 | `WishRunLogon < 0` | `CONFIRMED` | 未定位 | `NOT_STARTED` | 是否保留需產品決策，但先保留 Legacy 規格 |
| AUTH-011 | 未設管理員，提示預設帳密 | `WishRunLogon == 2` | `CONFIRMED` | Register | `PARTIAL` | Web 註冊流程與 Legacy 初始化語意不同 |
| DB-UPGRADE-001 | 登入後檢查資料庫版本 | `CheckDatabaseVersion` 呼叫 | `CONFIRMED` | 未定位 | `NOT_STARTED` | 尚待讀取完整升級流程 |
| APP-UPDATE-001 | 每日版本檢查 | `GetUpdateVersion` | `CONFIRMED` | VersionInfoPage | `PARTIAL` | 缺每日策略與資料庫相容提示 |
| APP-UPDATE-002 | Proxy 更新服務 | `GetProxy` | `CONFIRMED` | 部署環境 | `NOT_STARTED` | 現代化後可改為伺服器代理，但需明確差異 |
| LICENSE-001 | 註冊資訊有效性檢查 | `GetUpdateVersion` | `CONFIRMED` | 未定位 | `NOT_STARTED` | 缺授權／註冊模型 |
| LICENSE-002 | TR- 教育帳號過期處理 | `GetUpdateVersion` | `CONFIRMED` | 未定位 | `NOT_STARTED` | 尚未決定是否屬必要復刻範圍 |

## LEGACY-002：主導航與模組啟動

| Feature ID | 桌面版功能 | C# 來源 | 證據 | Web 對應 | 狀態 | 主要缺口 |
|---|---|---|---|---|---|---|
| NAV-001 | 三種首頁共用中央導航協調器 | `FormPanel*` → `frmPccesMain.functionButtons1` | `CONFIRMED` | `AppLayout` / React Router | `PARTIAL` | 缺統一 Module Launch Service |
| NAV-002 | Budget/Bid/Common/Invoice 功能群組 | `FunctionButtons.BtnMain*` | `CONFIRMED` | 側邊選單 | `UI_ONLY` | 缺 Legacy 分群狀態與群組切換契約 |
| NAV-003 | 模組級啟用／停用 | `FormPanel2.UpdateMenu` / `ModuleManager` | `CONFIRMED` | 未定位 | `NOT_STARTED` | 缺模組授權與配置驅動入口 |
| NAV-004 | 首頁模組用途說明 | `FormPanel2.FuncBtn2_MouseEnter` | `CONFIRMED` | Landing／選單文字 | `PARTIAL` | 現有頁面未以 Legacy 功能邊界驗收 |
| NAV-005 | 切換前工作狀態閘門 | `FunctionButtons.IsCanSwitchForm` | `CONFIRMED` | Router navigation | `NOT_STARTED` | 尚待讀取方法細節；目前缺統一 dirty-state gate |
| NAV-006 | 關閉／隱藏其他業務子表單 | `HideAllChild` 與各 BtnFunc 事件 | `CONFIRMED` | Router outlet | `PARTIAL` | 缺單一有效工作上下文規則 |
| NAV-007 | 既有模組實例重用 | MDI type scan / Show / BringToFront | `CONFIRMED` | React component lifecycle | `NOT_STARTED` | 重進頁面是否保留狀態未形成契約 |
| NAV-008 | 模組切換時注入使用者上下文 | `_UserID/_UserName/_ServerName/_HasRegistered` | `CONFIRMED` | JWT / stores | `PARTIAL` | 缺資料庫與授權上下文；權限快照未定義 |
| NAV-009 | 切換時收起 LeftPanel | 多個 `BtnFunc*` | `CONFIRMED` | Responsive layout | `UI_ONLY` | 可現代化，但需保留工作區優先結果 |
| NAV-010 | 導航等待狀態與控制項停用 | Wait Cursor / `FormSys_G_Info1` / Enabled false | `CONFIRMED` | Loading state | `PARTIAL` | 缺統一不可重入 loading lock |
| NAV-011 | 權限不足顯示正式功能名稱 | `ChkAuthority` + `GetFuncName` | `CONFIRMED` | API 403 / message | `PARTIAL` | 缺 Legacy function code 與名稱回應契約 |
| NAV-012 | 前端入口與後端雙重權限 | 每個 `BtnFunc*` 先查權限 | `CONFIRMED` | Admin/JWT role | `PARTIAL` | 尚未證明直接 API 呼叫不可繞過功能權限 |
| NAV-013 | 投標資料匯入精靈 | `BtnFuncBidImport_Click` | `CONFIRMED` | 未定位 | `NOT_STARTED` | 缺 `_IniMode=2`、`_IsAddOn=BID` 與完成後列表刷新定位 |
| NAV-014 | 契約模組先選取預算專案 | `BtnFunc9_Click` / `FormBudgetProjectPick` | `CONFIRMED` | Contract routes | `PARTIAL` | 缺 ActionName、候選專案規則與取消恢復 |
| NAV-015 | 系統維護指定頁籤入口 | `linkLabel1_LinkClicked` | `CONFIRMED` | `AdminPage` | `UI_ONLY` | AdminPage 尚未拆分 Legacy 子功能及深連結 |
| NAV-016 | 基本資料庫單一實例與上下文刷新 | `BtnFunc2_Click` | `CONFIRMED` | `MrsBasePage` | `PARTIAL` | 缺切換閘門、上下文刷新、註冊狀態與單一工作實例 |
| NAV-017 | 工項比較單一實例 | `BtnFunc8_Click` | `CONFIRMED` | `ComparePage` | `PARTIAL` | 缺 Legacy 可選資料、狀態與離開規則 |
| NAV-018 | 單價分析比較單一實例 | `BtnFunc7_Click` | `CONFIRMED` | `MrsBasePriceComparePage` | `PARTIAL` | 缺比對精度、方式與資料庫一致性規則 |
| AUTHZ-F002 | 基本資料庫維護權限 | `BtnFunc2_Click` | `CONFIRMED` | `MrsBasePage` | `NOT_STARTED` | 需建立 Legacy function-code policy |
| AUTHZ-F00500010002 | 投標資料匯入權限 | `BtnFuncBidImport_Click` | `CONFIRMED` | 未定位 | `NOT_STARTED` | 尚無對應流程 |
| AUTHZ-F007 | 經費審查比對權限 | `BtnFunc7_Click` | `CONFIRMED` | `MrsBasePriceComparePage` | `NOT_STARTED` | 尚無 function-code 授權證據 |
| AUTHZ-F008 | 歷史工程單位造價權限 | `BtnFunc8_Click` | `CONFIRMED` | `ComparePage` | `NOT_STARTED` | 尚無 function-code 授權證據 |
| AUTHZ-F009 | 契約編製權限 | `BtnFunc9_Click` | `CONFIRMED` | `ContractListPage` | `NOT_STARTED` | JWT role 不能替代細粒度功能權限 |
| AUTHZ-F010 | 估驗記錄權限 | `BtnFunc10_Click` | `CONFIRMED` | `InvoiceListPage` | `NOT_STARTED` | 尚無 function-code 授權證據 |
| AUTHZ-F0010007 | 系統維護特定入口權限 | `linkLabel1_LinkClicked` | `CONFIRMED` | `AdminPage` | `NOT_STARTED` | 缺特定管理子功能授權 |

## 網頁版既有模組待掛接 Legacy ID

| Web Page / API | 現況 | 下一步 |
|---|---|---|
| `LoginPage` / `/auth/login` | 已有 JWT 登入 | 對照 AUTH-001～AUTH-011，補完整差異決策與測試 |
| `DashboardPage` | 已有統計頁 | 依 NAV-001～NAV-004 補模組可用性、首頁偏好及 Legacy 功能入口 |
| `ProjectsPage` | 已有專案 CRUD | 進入 LEGACY-003 前不得宣告 Legacy 完成 |
| `BudgetEditorPage` | 已有預算樹與 CRUD | 必須讀 `FormBudgetEditMain`、BDGT Components、Grid 事件及 Domain 呼叫 |
| `ResourcesPage` | 已有資源及部分分析 | 必須讀 MRS／資源分析原碼及精度規則 |
| `ReportsPage` | 已有圖表與 Excel | 必須建立原報表、Excel、PDF、列印格式清冊 |
| `Invoice*` | 已有計價頁面 | 必須對照桌面版計價期別、累計與狀態機 |
| `Contract*` | 已有合約頁面 | 必須對照 SplitContract、變更與結算流程 |
| `AdminPage` | 已有管理入口 | 必須對照 PowerClass、UserClass、功能權限與代碼表 |
| `ComparePage` | 已有比較頁 | 補 NAV-017、AUTHZ-F008 與 Legacy 數量／單價／複價比較規則 |
| `MrsBasePriceComparePage` | 已有單價比較頁 | 補 NAV-018、AUTHZ-F007 與比對精度／方式規則 |
| `VersionInfoPage` | 已有版本顯示 | 必須擴充 DB Schema、授權與升級相容資訊 |

## 待完成規則

1. 每讀完一個 Legacy 模組，先更新本矩陣，再修改 Web。
2. 每個 Feature ID 至少附一個 C# 類別／方法來源。
3. `INFERRED` 與 `REQUIRES_RUNTIME_TEST` 不得直接升為 `LEGACY_MATCHED`。
4. 有意不復刻的功能必須記錄：理由、替代方案、風險及使用者決策。
5. Web 測試名稱應包含 Feature ID，例如：

```text
test_AUTH_004_login_records_source_context
test_APP_CLOSE_003_budget_dirty_state_blocks_navigation
test_NAV_011_forbidden_response_contains_legacy_function_code
test_NAV_014_contract_launch_requires_eligible_project_context
```
