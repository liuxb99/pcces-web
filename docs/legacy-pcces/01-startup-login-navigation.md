# PCCES Win 4.3 啟動、登入與主框架互動規格

狀態：`LEGACY-001 / IMPLEMENTED — WAITING FOR FULL SOURCE COVERAGE`

本文件只記錄已由 C# 原始碼確認或明確推導的行為。尚未讀取的相鄰類別及封閉 DLL 行為，保留為待補項目。

## 1. 涉及來源

### 已讀取

- `PCCES_CS/Archnowledge.Pcces.PccesMain/frmPccesMain.cs`
- `PCCES_CS/Archnowledge.Pcces.PccesMain/FormLogin.cs`

### 已定位、待深入

- `FormSplash.cs`
- `FormPanel.cs`
- `FormPanel2.cs`
- `FormPanel3.cs`
- `FunctionButtons`
- `OnlineList`
- `StaffClass`
- `UserClass`
- `PccesBaseHelper`
- `DatabaseUpgrade`
- `DatabaseChange`

## 2. 系統級狀態

`frmPccesMain` 使用 `FORM_STATUS` 管理主視窗生命週期：

| 狀態 | 已確認用途 |
|---|---|
| `INI` | 主視窗初始化中；Activated 時進入第一輪前景顯示。 |
| `ACT` | 主視窗已啟動；下一次 Activated 可能執行更新檢查。 |
| `NOR` | 正常狀態。 |
| `CLOSE` | 初始化或外部服務失敗，主視窗應關閉。 |
| `BDGT_DONT_CLOSE` | 預算模組要求禁止主程式關閉。 |

另有 `F_Freeze`：

- 空字串：主框架可正常互動與關閉。
- `FREEZE`：首頁面板停用，關閉事件必須取消。

這表示網頁版不能只用「登入／登出」描述生命週期，至少還需要：

- 啟動檢查狀態
- 系統升級／維護鎖定
- 預算編輯禁止離開狀態
- 初始化失敗關閉狀態

## 3. APP-START-001：程式進入點

可信度：`CONFIRMED`

來源：`frmPccesMain.Main(string[] args)`

### 流程

1. 寫入 `Pcces46` 日誌：「程式進入點」。
2. 執行 `CheckPccesUser()`。
3. 若資料庫檢查失敗：
   - 顯示資料庫錯誤訊息。
   - 啟動 `ConfigEditor.exe`。
   - 呼叫 `Application.Exit()`。
   - 不進入主程式。
4. 取得 SQL Server 版本。
5. 若版本為 SQL 2000：
   - 顯示「此版不支援SQL 2000」。
   - 結束啟動。
6. 建立名稱為 `PccesMain` 的 Mutex。
7. 若已有同名程序：
   - 顯示「已有相同 Pcces 程式正在執行中」。
   - 禁止重複啟動。
8. 若為唯一程序：
   - 建立 `zh-TW` CultureInfo。
   - 將日曆設定為 Gregorian Calendar。
   - 啟動 `frmPccesMain`。

### Web 對應要求

- 後端啟動健康檢查必須區分資料庫「可連線」與「Schema 是有效 PCCES」。
- 生產部署必須拒絕不支援的資料庫版本或 Schema 版本。
- Web 不需單例瀏覽器，但需避免同一資料被重複批次處理；此行為應轉化為工作鎖或交易鎖，而不是直接省略。
- 日期文化與曆法必須明確固定，不依賴伺服器區域設定。

## 4. APP-START-002：PCCES 資料庫帳號與結構預檢

可信度：`CONFIRMED`（內部 Helper 細節待查）

來源：`frmPccesMain.CheckPccesUser()`

### 流程

1. 從 `ConnectionStrings["Pcces"]` 讀取連線字串。
2. 建立 `PccesBaseHelper`。
3. 執行 `RemoveNotExistPcces()`。
4. 執行 `CheckPccesUser()`。
5. 全部完成才回傳成功。
6. 任一例外：
   - 寫入 `Pcces46` 日誌。
   - 回傳失敗。

### 待確認

- `RemoveNotExistPcces()` 刪除或修復哪些資料。
- `CheckPccesUser()` 建立哪些登入帳號、角色或權限。
- 是否有交易及回滾。

## 5. APP-START-003：主視窗建構與 Splash

可信度：`CONFIRMED`

來源：`frmPccesMain()`

### 流程

1. 初始化控制項。
2. 將 `functionButtons1.ButtonOwner` 設為 `LeftPanelStatus.None`。
3. 從 AppSettings 取得 `ServerName`。
4. 建立 `FormSplash` 並設定 Owner 為主視窗。
5. 顯示 Splash。
6. 執行 `Application.DoEvents()`。
7. 呼叫 `LoadingForm()`。

### 重要條件

`LoadingForm()` 在 `_PreConnectOK == false` 時立即返回。因此主面板載入依賴另一段「預連線成功」流程。該流程尚待由建構器相鄰事件、Designer 或其他類別補齊。

## 6. APP-SHELL-001：首頁面板選擇與唯一性

可信度：`CONFIRMED`

來源：`frmPccesMain.LoadingForm()`

### 資料來源

INI：

```text
[HomePanel]
Home=1|2|3
```

空值預設為 `2`。

### 行為

- `Home=1`：開啟 `FormPanel`。
- `Home=2`：開啟 `FormPanel2`。
- `Home=3`：開啟 `FormPanel3`。
- 每次先掃描 `MdiChildren`，同類型視窗存在時不得重複建立。
- 新建面板時：
  - 傳入 `_UserID`。
  - 設定 `MdiParent=this`。
  - 呼叫 `Show()`。

### Web 對應要求

- 首頁類型必須作為使用者或系統偏好保存。
- 切換首頁不得產生重複的狀態實例。
- 使用者 ID 必須在載入首頁資料前完成解析。

## 7. APP-SHELL-002：主視窗位置與尺寸持久化

可信度：`CONFIRMED`

來源：`frmPccesMain_Load`、`frmPccesMain_FormClosing`

INI 欄位：

```text
[HomePanel]
LocationX=
LocationY=
Width=
Height=
FormStatus=NORMAL|MAX
```

### 啟動

- 有保存狀態時恢復 Normal 或 Maximized。
- 無保存狀態時預設 Maximized。
- X、Y 都大於 0 才恢復位置。
- Width、Height 大於 0 才恢復尺寸。

### 關閉

無論最後是否允許關閉，先寫回位置、尺寸及視窗狀態。

### Web 對應

可轉化為：

- 側欄展開狀態
- 首頁版面
- 表格欄寬
- 分頁與篩選器
- 最近開啟模組

但屬 UX 相容項，不應阻塞核心業務復刻。

## 8. AUTH-001：登入表單初始化

可信度：`CONFIRMED`

來源：`FormLogin_Load`、`FormLogin_Activated`

### 流程

1. 清空帳號與密碼。
2. 從執行目錄的 `PccesMain.ini` 讀取：

```text
[User]
UserID=
```

3. 將上次帳號填入帳號欄。
4. 取得本機 IP 並保存到表單欄位 `IPAddress`。
5. 表單 Activated 時，若帳號非空，焦點自動移至密碼欄。
6. Enter 等同按下「確定」。

### Web 缺口

目前 Web 登入可保存 JWT，但需要確認是否保留最近帳號、是否符合安全策略，以及是否要保留「帳號非空直接聚焦密碼」的操作便利性。

## 9. AUTH-002：輸入字串驗證

可信度：`CONFIRMED`（規則本體待查）

來源：`FormLogin.txtUserID_Validating`

帳號與密碼欄共用同一 Validating Handler：

1. 呼叫 `CommonMethods.CheckValidString(text)`。
2. 回傳 false 時設 `e.Cancel=true`。
3. 因此非法字串會阻止焦點離開或表單提交。

待讀取 `CheckValidString`，確認允許字元、SQL 特殊字元及空字串規則。

## 10. AUTH-003：登入驗證與成功副作用

可信度：`CONFIRMED`；`StaffClass` 內部規則待查。

來源：`FormLogin.BtnOK_Click`

### 驗證輸入

傳入 `StaffClass.ChkLogon`：

- `txtUserID.Text`
- `txtPassword.Text`
- 表單載入時保存的 IP
- `Environment.MachineName`

### 成功流程

1. 用 `UserClass.ListItem(" UserId='...' ")` 查使用者。
2. 有資料時，將 `UserName`、`UserID` 回寫 Owner `frmPccesMain`。
3. 將日誌執行者改為使用者 ID。
4. 寫入粗略操作日誌：`使用者--登入(IP)`。
5. 設定 `DialogResult.OK`。
6. 將登入成功的 `UserID` 寫回 `PccesMain.ini`。
7. 關閉登入表單。

### 風險與待確認

- 查詢條件使用字串串接；需要確認 `CheckValidString` 是否是主要注入防線。
- `DT_Tmp.Rows.Count == 0` 時仍在後面讀取 `Rows[0]`；理論上 `ChkLogon=true` 應保證 User 資料存在，需以 Runtime Test 驗證。
- IP 使用載入時保存值，而日誌顯示值重新呼叫 `GetIPAddress()`，兩者可能不同。

### Web 對應要求

- 驗證成功後不只發 Token，還必須建立操作日誌及登入來源資訊。
- 應保存 user ID、顯示名稱、角色與權限快照。
- 必須定義使用者存在性不一致的錯誤，而不是讓流程在取第一列時崩潰。

## 11. AUTH-004：登入失敗

可信度：`CONFIRMED`

1. 清空密碼欄。
2. 顯示「帳號或密碼錯誤！」。
3. 焦點回到密碼欄。
4. 不關閉表單。

Web 版應避免透露帳號是否存在，現行統一錯誤方向相符；需補失敗次數、鎖定或稽核規則的 Legacy 調研。

## 12. AUTH-005：取消登入

可信度：`CONFIRMED`

1. 設定 `DialogResult.Cancel`。
2. 將 Owner `frmPccesMain._LoginIsCancel=true`。

`_LoginIsCancel` 會阻止後續更新檢查。因此取消登入不只是關閉彈窗，還會改變主程式後續生命週期。

## 13. AUTH-006：登入模式分流

可信度：`CONFIRMED`；`WishRunLogon` 回傳語意待深入。

來源：`frmPccesMain.functionButtons1_Load`

### 前置條件

若 `_PreConnectOK=false`：

- `FORM_STATUS=CLOSE`
- 中止後續載入

### 分流

呼叫 `StaffClass.WishRunLogon()`：

- `< 0`：匿名登入
  - `UserID=PccAdmin`
  - `UserName=匿名登入`
  - 同步 OnlineList
- `== 2`：未設定系統管理者
  - 顯示系統自動產生帳密：`PccesUser / 12345`
  - `FORM_STATUS=CLOSE`
- 其他：顯示 `FormLogin`

登入成功後：

1. 同步 OnlineList 使用者資訊。
2. 顯示資料庫結構檢查進度視窗。
3. 執行 `CheckDatabaseVersion()`。
4. 後續成功／失敗分支待補讀完整方法。

### Web 重大缺口

目前 Web 直接提供註冊與登入，但桌面版至少存在：

- 匿名登入模式
- 系統管理員未初始化模式
- 首次啟動預設帳密提示
- 登入後資料庫版本檢查／升級

是否在新版保留匿名模式可由產品決策調整，但必須先記錄 Legacy 行為，再明確標示「相容」或「有意差異」。

## 14. APP-SHELL-003：OnlineList 連線

可信度：`CONFIRMED`（協定待查）

主視窗載入時：

1. 從 INI 讀取 `User.ChatServer`。
2. 若 ChatServer 不是 `FALSE`，且預連線未成功，關閉主程式。
3. 設定 OnlineList ServerName 與 FunctionName。
4. 呼叫 `onlineList1.Connect()`。
5. Connect 失敗且 ChatServer 未停用時，關閉主程式。

此功能可能同時負責在線使用者、模組占用或互斥狀態，不能直接視為聊天功能刪除。必須讀取 `OnlineList` 後再決定 Web 對應為 Presence、Lock、Session Registry 或可淘汰功能。

## 15. APP-SHELL-004：停用與恢復主框架

可信度：`CONFIRMED`

### DisableMain

- `F_Freeze=FREEZE`
- 重新讀取首頁類型
- HomePanel=2 時停用 `FM_PNL2.PNL1`
- 例外寫入 Log

### EnableMain

- 清空 `F_Freeze`
- HomePanel=2 時重新啟用 `FM_PNL2.PNL1`

Web 對應應建立全域 Maintenance/Blocking Overlay，並阻止路由離開及重複提交。

## 16. APP-CLOSE-001：主程式關閉閘門

可信度：`CONFIRMED`

來源：`frmPccesMain_FormClosing`

優先順序：

1. 保存視窗狀態。
2. `F_Freeze` 非空：直接取消關閉。
3. `FORM_STATUS=CLOSE`：允許直接關閉，不詢問。
4. `FORM_STATUS=BDGT_DONT_CLOSE`：取消關閉。
5. 其他狀態：詢問「確定要結束 Pcces Win 4.3？」
6. 使用者選 Yes：逐一關閉所有 MDI 子視窗。
7. 使用者選 No：取消關閉。

### Web 對應要求

- 預算存在未完成／不可離開狀態時，必須阻止路由切換、登出或關閉頁面。
- 系統維護鎖定期間不得中斷關鍵交易。
- 一般離開可顯示確認，但不能用確認框代替真正的 dirty-state／transaction-state 判斷。

## 17. APP-UPDATE-001：更新與註冊檢查

可信度：`CONFIRMED`

### 觸發

主視窗 Activated：

- `INI` 且預連線成功：顯示首頁並進入 `ACT`。
- `CLOSE`：關閉。
- `ACT`：執行一次 `CheckUpdate()`，再進入 `NOR`。

`CheckUpdate()` 只在：

- 未取消登入
- 尚未提示過
- `FORM_STATUS=ACT`

時執行。

### 更新服務

- 每日最多依 INI 日期判斷一次。
- 讀取 `DownloadInfo.webServiceRoute`，無值時使用既定 Web Service URL。
- 支援 Proxy。
- 取得最新版本。
- 取得註冊 ID、姓名、Email、MAC，向更新服務驗證註冊是否仍有效。
- 註冊失效時可詢問是否清空註冊資訊。
- 教育訓練帳號以 `TR-` 前綴顯示不同訊息。

Web 版的版本資訊頁目前不足以等價此流程；至少需要區分：部署版本、資料庫版本、Feature Flag、授權／註冊狀態及升級相容性。

## 18. 初步 C# → Web 差異

| Legacy Feature | Web 現況 | 狀態 |
|---|---|---|
| 啟動時驗證 PCCES DB | `/api/health` 僅一般健康狀態 | `PARTIAL` |
| SQL 版本／Schema 支援檢查 | 未見完整對應 | `NOT_STARTED` |
| 匿名登入模式 | 未見對應 | `NOT_STARTED` |
| 管理員首次初始化 | Web 有 register，但語意不同 | `PARTIAL` |
| 最近使用者帳號 | 未確認 | `NOT_STARTED` |
| 登入操作日誌 | 未見完整對應 | `NOT_STARTED` |
| 登入來源 IP／Machine | 未完整保存 | `PARTIAL` |
| 登入後 DB Upgrade | 未見對應 | `NOT_STARTED` |
| 首頁 1/2/3 偏好 | Dashboard 單一入口 | `UI_ONLY` |
| OnlineList／Presence／Lock | 未見對應 | `UNKNOWN` |
| Freeze／禁止關閉 | 未見全域狀態機 | `NOT_STARTED` |
| 預算禁止關閉 | 未見 Legacy 等價閘門 | `NOT_STARTED` |
| 更新與註冊驗證 | 只有版本／Feature Flag 頁面 | `PARTIAL` |

## 19. 下一步必讀

1. 完整讀取 `frmPccesMain.cs` 尚未展開部分。
2. `Form1.cs` 與 Designer／入口關係。
3. `FunctionButtons` 如何建立功能導航及權限。
4. `FormPanel*` 三種首頁的按鈕與事件。
5. `OnlineList` 的互斥與在線用途。
6. `StaffClass.WishRunLogon`、`ChkLogon`。
7. `CheckDatabaseVersion()`、Upgrade／Change 流程。
8. `CommonMethods.CheckValidString`。
