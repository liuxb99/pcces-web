# PCCES 專案精靈與異動追蹤矩陣

更新日期：2026-08-02

## 狀態

- `NOT_STARTED`
- `UI_ONLY`
- `PARTIAL`
- `IMPLEMENTED`
- `LEGACY_MATCHED`
- `VERIFIED`

## 專案建立／匯入精靈

| Feature ID | Legacy 行為 | 來源 | Web 現況 | 狀態 | 缺口 |
|---|---|---|---|---|---|
| PROJECT-WIZ-001 | 多步驟 Wizard Tab A～K | `formNewProjectWizard` | 單一建立／編輯頁 | `NOT_STARTED` | 缺持久化 Wizard State 與步驟驗證 |
| PROJECT-WIZ-002 | 建立模式預選 RB1 | `_InitCreateProject=true` | `POST /projects` | `PARTIAL` | 無 Legacy 建立精靈與前置規則 |
| PROJECT-WIZ-003 | 匯入模式預選 RB2 | `_InitCreateProject=false` | 未定位 | `NOT_STARTED` | 缺匯入 Session、預覽與 commit |
| PROJECT-WIZ-004 | 投標匯入 `_IniMode=2` | `BtnFuncBidImport_Click` | 未定位 | `NOT_STARTED` | 缺 BID Add-on 專用模式 |
| PROJECT-WIZ-005 | `_IsAddOn=BID` | `BtnFuncBidImport_Click` | 未定位 | `NOT_STARTED` | 一般匯入不可替代 |
| PROJECT-WIZ-006 | ProjectCode | `txtProjectCode` | DB id/name | `PARTIAL` | 缺 Legacy 穩定碼與規則 |
| PROJECT-WIZ-007 | ProjectCodeAlias | `txtProjectCodeAlias` | 未定位 | `NOT_STARTED` | 缺 alias 欄位與唯一性規則 |
| PROJECT-WIZ-008 | 中英文名稱、地址、備註 | Wizard controls | 部分欄位 | `PARTIAL` | DTO 與 validation 未對齊 |
| PROJECT-WIZ-009 | PX 檔案匯入 | `txtPxfin` | 未定位 | `NOT_STARTED` | 缺解析器與版本驗證 |
| PROJECT-WIZ-010 | Excel 匯入 | `txtExcelin` | 部分 Excel 匯出 | `NOT_STARTED` | 匯出不等於匯入；缺 mapping/preview |
| PROJECT-WIZ-011 | XML/PCCES 文件處理 | XML namespaces/importdoctype | 未定位 | `NOT_STARTED` | 缺格式、驗證與認證錯誤模型 |
| PROJECT-WIZ-012 | 匯入進度 | `Prog1/lblWait` | 未定位 | `NOT_STARTED` | 缺 job/status 與不可重入 |
| PROJECT-WIZ-013 | 來源／目的項目移動 | `GridSource/GridDestination` | 未定位 | `NOT_STARTED` | 缺分拆／選擇性匯入模型 |
| PROJECT-WIZ-014 | 分拆成功狀態 | `F_SPLT_STATUS/F_IsSplitSucceeded` | 未定位 | `NOT_STARTED` | 缺 split transaction 與結果 |
| PROJECT-WIZ-015 | 精度選項 | `F_Main*/F_Ana*` | 未定位 | `NOT_STARTED` | 缺數量、單價、金額精度契約 |
| PROJECT-WIZ-016 | 完成後刷新並定位 | `GetNewData/BindDataToGrid/LocateToSpecificRow` | 列表刷新部分 | `PARTIAL` | 缺以 projectCode 定位結果 |

## 專案身分與異動

| Feature ID | Legacy 行為 | 來源 | Web 現況 | 狀態 | 缺口 |
|---|---|---|---|---|---|
| PROJECT-ID-001 | projectCode 為穩定工作身分 | `FormProject` / Wizard | integer/UUID | `PARTIAL` | 需保留 Legacy code，不以 DB id 取代 |
| PROJECT-ID-002 | projectCodeAlias | `FormProject` / Wizard | 未定位 | `NOT_STARTED` | 缺欄位與規則 |
| PROJECT-ID-003 | mainProj／主子專案 | `FormProject` | 未定位 | `NOT_STARTED` | 缺關係模型 |
| PROJECT-ID-004 | old/new/sub project code | Wizard state | 未定位 | `NOT_STARTED` | 缺來源追蹤與分拆 lineage |
| PROJECT-MUT-001 | IsCanDelete 由 Domain 計算 | `FormProject` hidden field | owner delete | `PARTIAL` | 前端／owner 判斷不可替代 Domain capability |
| PROJECT-MUT-002 | 模板專案 | `IsTemplate/Template` | 未定位 | `NOT_STARTED` | 缺模板生命週期 |
| PROJECT-MUT-003 | BUD/BID/CNT 存在狀態 | `IsBud/IsBid/IsCNT` | 部分統計 | `PARTIAL` | 缺專案 capability response |
| PROJECT-MUT-004 | 專案授權 Auth | `PubProject.GetProjectList(UserID)` | owner/admin | `PARTIAL` | 權限模型未對齊 |
| PROJECT-MUT-005 | Profile 與系統欄位分離 | Grid/Wizard fields | 通用 PATCH | `PARTIAL` | 需禁止修改系統計算能力欄位 |
| PROJECT-MUT-006 | 交易性匯入 | Wizard Finish | 未定位 | `NOT_STARTED` | 失敗不得留下半成品 |
| PROJECT-MUT-007 | 交易性分拆 | Wizard split state | 未定位 | `NOT_STARTED` | 缺 rollback、lineage 與稽核 |
| PROJECT-MUT-008 | 異動後能力重算 | FormProject reload | 未定位 | `NOT_STARTED` | mutation response 需回傳新 capability |
| PROJECT-MUT-009 | 稽核與執行使用者 | `_UserID` / logs | 部分 created_by | `PARTIAL` | 缺完整 operation audit |
| PROJECT-MUT-010 | 併發工作保護 | OnlineList/work context | 未定位 | `UNKNOWN` | 待確認 lock/presence 真實語意 |

## 必要永久測試

```text
test_PROJECT_WIZ_002_create_mode_is_distinct_from_import
test_PROJECT_WIZ_004_bid_addon_uses_dedicated_validation
test_PROJECT_WIZ_006_project_code_is_stable_and_unique
test_PROJECT_WIZ_010_excel_import_requires_preview_before_commit
test_PROJECT_WIZ_012_duplicate_finish_is_idempotent
test_PROJECT_WIZ_014_failed_split_rolls_back_all_changes
test_PROJECT_WIZ_016_success_returns_locator_project_code
test_PROJECT_MUT_001_delete_rechecks_server_capability
test_PROJECT_MUT_003_capabilities_reflect_budget_bid_contract_state
test_PROJECT_MUT_005_profile_patch_rejects_system_fields
test_PROJECT_MUT_006_failed_import_leaves_no_partial_project
test_PROJECT_MUT_008_mutation_returns_recomputed_capabilities
```

## 收口標準

此 Segment 只有在以下條件成立後才能升為 `LEGACY_MATCHED`：

1. RB1～RB5 的正式模式與 Tab 流向已逐一確認。
2. 專案代碼生成、alias、重複檢查已形成契約。
3. PX/XML/Excel 匯入均有 fixture 與錯誤案例。
4. 建立、匯入、分拆全部具交易與 rollback 測試。
5. `IsCanDelete` 公式已由來源確認並由 API 統一執行。
6. Web 專案 DTO 可表達 Legacy 身分、能力、模板與 lineage。
