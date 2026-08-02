# PCCES Legacy 功能代碼與權限目錄

更新日期：2026-08-02

## 1. 文件目的

本文件記錄 PCCES C# 桌面版由 `FunctionButtons` 協調的主要功能入口、權限代碼、模組群組與啟動前置條件。所有內容以目前可直接從源碼確認的行為為準；尚未讀到 `DBClass.ChkAuthority`、`DBClass.GetFuncName` 內部實作的部分標記為待確認。

## 2. 權限判定共通模式

多數功能入口遵循以下順序：

```text
使用者點擊功能
→ IsCanSwitchForm()
→ 設定 FunctionOpenMode
→ DBClass.ChkAuthority(UserID, FunctionCode)
→ 無權限：DBClass.GetFuncName(FunctionCode) + 固定提示
→ 有權限：清理或重用 MDI 子表單
→ 注入使用者與環境上下文
→ 開啟目標功能
```

標準拒絕訊息：

```text
<功能名稱>
這個功能您沒有權限使用
```

### 已確認特性

- 權限並非只控制選單顯示，也在功能事件中再次強制檢查。
- 權限鍵是字串代碼，不是單純角色名稱。
- 部分功能有階層式代碼，例如 `F00500010002`。
- 系統模組啟用狀態與使用者權限是兩層不同控制。
- Web 版不能只依前端隱藏選單，API 端必須再次驗證相同能力。

## 3. 模組群組

| FunctionOpenMode | 桌面版功能群 | 主要按鈕 | 說明 |
|---|---|---|---|
| `Budget` | 預算 | 專案目錄、預算書編製 | 先建立／選取專案，再進入預算作業 |
| `Bid` | 投標 | 投標資料匯入、投標單填寫 | 包含標單匯入精靈與投標內容編輯 |
| `Common` | 共用 | 基本資料庫、工程比對、單價比對 | 共用工項與跨專案分析工具 |
| `Invoice` | 契約履約 | 契約編製、契約變更、估驗、結算、驗收 | 多數入口必須先選取有效專案 |

## 4. 已確認功能代碼

| Legacy Function Code | 功能 | C# 入口 | 目標表單／流程 | Web 對應 | 證據狀態 |
|---|---|---|---|---|---|
| `F002` | 基本資料庫維護 | `BtnFunc2_Click` | `frmMrsBase` | `MrsBasePage` | `CONFIRMED` |
| `F00500010002` | 投標資料匯入 | `BtnFuncBidImport_Click` | `FormProject` + `formNewProjectWizard`，`_IniMode="2"`、`_IsAddOn="BID"` | 尚無完整對應 | `CONFIRMED` |
| `F007` | 經費審查比對／工項分析比對 | `BtnFunc7_Click` | `FormCompareMrs` | `MrsBasePriceComparePage` | `CONFIRMED` |
| `F008` | 歷史工程單位造價／工程項目比對 | `BtnFunc8_Click` | `FormCompareItm` | `ComparePage` | `CONFIRMED` |
| `F009` | 契約編製 | `BtnFunc9_Click` | `FormBudgetProjectPick(Action=SplitContract)` → `FormSplitContract` | `ContractListPage` | `CONFIRMED` |
| `F010` | 估驗記錄 | `BtnFunc10_Click` | `FormBudgetProjectPick(Action=Invoice)` → `FormInvoice` | `InvoiceListPage` | `CONFIRMED` |
| `F011` | 契約變更 | `BtnFunc6_Click` | `FormBudgetProjectPick(Action=BudgetChange)` → `FormBudgetChange` | Issue／Contract 相關頁面，尚未一對一 | `CONFIRMED` |
| `F0010007` | 系統維護指定入口 | `linkLabel1_LinkClicked` | `frmSysMaintain`，並選取 `Tab_G` | `AdminPage` | `CONFIRMED` |

## 5. 尚待補齊的功能代碼

以下功能入口已在 `FunctionButtons` 中出現，但本批尚未取得完整代碼與所有分支，必須在後續繼續讀取：

- 預算專案目錄。
- 預算書編製。
- 投標單填寫。
- 結算。
- 驗收。
- 系統插件／附加模組。
- 建立預算檔、建立空白標單、建立專案、匯入專案等流程圖捷徑。

不得根據按鈕編號自行猜測權限碼。

## 6. 模組授權與使用者權限的差異

`FormPanel2.UpdateMenu()` 先透過 `ModuleManager` 控制整個模組是否顯示：

- `EnableBudgetMdoule`
- `EnableContractModule`
- `EnableBidMdoule`
- `EnableCommonMdoule`

之後功能點擊時，`FunctionButtons` 再使用 `DBClass.ChkAuthority` 驗證個人權限。

因此完整權限模型至少為：

```text
部署／授權層：模組是否存在或啟用
            ↓
使用者層：目前 UserID 是否有 FunctionCode
            ↓
工作上下文層：目前表單能否安全切換
            ↓
資料層：使用者是否可操作所選專案
```

目前 Web 版主要只有 `User.role` 或管理員判斷，尚未證明具備 Legacy 的細粒度 Function Code 能力。

## 7. Web 復刻要求

### 7.1 正式功能目錄

Web 後端應建立穩定的功能目錄資料結構，至少包含：

```text
function_code
legacy_name
web_route
api_scope
module_code
parent_function_code
enabled
requires_project
requires_registration
```

### 7.2 權限檢查位置

每項功能必須同時在以下位置檢查：

1. 導航功能可見性。
2. 路由進入。
3. API 執行。
4. 專案／資料所有權。
5. 狀態轉換或寫入動作。

### 7.3 相容性原則

- Legacy Function Code 應保留為外部穩定識別碼。
- 可新增較清楚的 Web capability 名稱，但不能丟失 Legacy 對照。
- 所有拒絕結果應回傳可辨識的功能代碼。
- 功能名稱應從同一份目錄取得，不應散落硬編碼。
- 權限快照應包含版本，避免角色調整後仍使用舊權限。

## 8. 待確認來源

後續必讀：

```text
Archnowledge.Pcces.DatabaseAccess.DBClass
DBClass.ChkAuthority
DBClass.GetFuncName
PowerClass
StaffClass
UserClass
ModuleManager
FunctionOpenMode
```

在這些來源尚未讀完前，本文件只可作為已確認入口目錄，不能宣告完整權限模型已還原。
