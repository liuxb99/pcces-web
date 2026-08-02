# PCCES 專案建立與匯入精靈互動規格

更新日期：2026-08-02

## 1. 範圍

本文件依據桌面版 `formNewProjectWizard` 與 `FormProject.ExecuteNewProject` 的可確認呼叫關係，整理專案建立、匯入、投標匯入與分拆相關互動。尚未取得完整分支內容的部分維持 `REQUIRES_MORE_SOURCE`，不得自行補成 Web 規格。

主要來源：

- `PCCES_CS/Archnowledge.Pcces.PccesMain.Project/formNewProjectWizard.cs`
- `PCCES_CS/Archnowledge.Pcces.PccesMain.Project/FormProject.cs`
- `PCCES_CS/Archnowledge.Pcces.PccesMain.ArchControls/FunctionButtons.cs`

## 2. 精靈不是單一表單

`formNewProjectWizard` 具有多個 Wizard Tab：`Tab_A` 至 `Tab_K`，並有各頁 Previous、Next、Cancel、Finish 按鈕。這表示桌面版專案建立／匯入是狀態式多步驟流程，不是單一 `POST /projects`。

已確認精靈持有：

- 建立／匯入模式選擇 Radio Buttons：`RB1`～`RB5`
- 專案代碼 `txtProjectCode`
- 專案代碼別名 `txtProjectCodeAlias`
- 中文名稱 `txtProjectCName`
- 英文名稱 `txtProjectEName`
- 地址 `txtProjectAddress`
- 備註 `txtProjectMemo`
- PX 檔案輸入 `txtPxfin`
- Excel 輸入 `txtExcelin`
- 來源與目的清單 `GridSource` / `GridDestination`
- 匯入進度 `Prog1`
- 匯入文件類型 `importdoctype`
- 既有專案碼 `F_OldProjectCode`
- 新專案碼 `F_NewProjectCode`
- 主／子專案拆分狀態
- 單價、數量、金額精度設定欄位

## 3. 入口模式

### 3.1 一般專案目錄入口

`FunctionButtons.DoProjectCreateImport`：

```text
CreateFormProject()
→ FormProject.ExecuteNewProject("0", InitCreateProject: true/false)
```

- `InitCreateProject=true`：預選建立新專案。
- `InitCreateProject=false`：預選匯入專案。

精靈 `_InitCreateProject` setter 直接控制：

```text
true  → RB1.Checked = true
false → RB1.Checked = false; RB2.Checked = true
```

因此建立與匯入至少是兩個正式模式。

### 3.2 投標匯入入口

`BtnFuncBidImport_Click`：

```text
_IniMode = "2"
_IsAddOn = "BID"
```

精靈關閉後，`FormProject` 必須執行：

```text
GetNewData()
BindDataToGrid()
LocateToSpecificRow()
```

所以投標匯入成功後必須刷新專案目錄並定位新建／匯入專案。

## 4. 已確認狀態欄位

| 欄位 | 意義 |
|---|---|
| `F_PID` | 執行環境／產品設定識別，需繼續追蹤 |
| `F_NewProjectCode` | 新建立或匯入後的專案代碼 |
| `F_UserID` | 執行使用者 |
| `F_ActionName` | 呼叫此精靈的業務 Action |
| `F_IniMode` | 精靈初始模式 |
| `F_IsAddOn` | 額外匯入型態，例如 `BID` |
| `F_OldProjectCode` | 來源或被轉換的舊專案代碼 |
| `F_SubProjectCode` | 分拆流程中的子專案代碼 |
| `F_SPLT_STATUS` | 分拆流程狀態 |
| `F_IsSplitSucceeded` | 分拆是否成功 |
| `OptionSet` | 匯入／建立選項組 |
| `importdoctype` | 匯入文件類型 |

## 5. 可確認的輸入類型

從控制項與 namespace 可確認精靈處理多種資料來源：

- 空白建立
- 既有專案／模板來源
- PCCES XML 類資料
- PX 檔案
- Excel 檔案
- BID 附加匯入
- 專案分拆／來源目的項目移動

但每個 Radio Button 對應的正式名稱與完整校驗仍需取得事件方法後確認。

## 6. 建立與匯入的共同結果契約

成功結果至少必須返回：

```text
newProjectCode
projectCodeAlias
projectNameC
sourceType
importDocumentType
isSplitSucceeded
warnings
```

並觸發：

```text
專案目錄重新載入
→ 重新套用目前篩選
→ 定位新專案
→ 更新最近使用或 Action 狀態（若有）
```

## 7. Web 復刻要求

Web 應建立正式 Wizard State，不應將所有模式塞進單一建立對話框。

建議契約：

```text
ProjectWizardSession
- mode
- action
- addonType
- currentStep
- draftProject
- sourceDocument
- sourceProject
- mapping
- precisionOptions
- validationResults
- executionStatus
```

必要能力：

1. 每一步有 server-side validation。
2. Finish 前重新驗證整份草稿。
3. 匯入操作具交易性；失敗不得留下半成品專案。
4. 上傳檔案必須先解析、顯示預覽與錯誤，再正式寫入。
5. 成功回傳穩定 `projectCode`，前端重新查詢並定位。
6. BID Add-on 必須保留獨立模式，不能等同一般專案匯入。
7. 分拆操作必須回傳來源、目的與結果追蹤。

## 8. 尚待確認

以下標記為 `REQUIRES_MORE_SOURCE`：

- RB1～RB5 的正式文字與分支。
- Tab A～K 的精確前後順序。
- ProjectCode 自動生成與重複檢查規則。
- ProjectCodeAlias 的必填與唯一性。
- PX、XML、Excel 支援版本與欄位映射。
- 精度設定如何影響匯入結果。
- 交易 rollback 與清理規則。
- 分拆來源／目的移動限制。
- God Mode 的真實用途與是否應復刻。

在以上項目確認前，Web 不得宣告 `PROJECT-CREATE` 或 `PROJECT-IMPORT` 已達 `LEGACY_MATCHED`。
