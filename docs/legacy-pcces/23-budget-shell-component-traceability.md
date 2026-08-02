# PCCES 預算主框架與子元件追蹤矩陣

更新日期：2026-08-02

## 狀態

- `NOT_STARTED`
- `UI_ONLY`
- `PARTIAL`
- `IMPLEMENTED`
- `LEGACY_MATCHED`
- `VERIFIED`

## 預算主框架

| Feature ID | Legacy 功能 | C# 來源 | 證據 | Web 現況 | 狀態 | 主要缺口 |
|---|---|---|---|---|---|---|
| BUD-SHELL-001 | BUD/BID 共用 frmBudget | `CreateFormBudgetByBUD/BID`, `frmBudget` | `CONFIRMED` | `BudgetEditorPage` | `PARTIAL` | 缺 Action 分流與獨立規則 |
| BUD-SHELL-002 | 專案工作上下文 | `projectCode/sourceProjectCode/parentProjectCode` | `CONFIRMED` | route project id | `PARTIAL` | 缺 legacy projectCode 與來源上下文 |
| BUD-SHELL-003 | 表單狀態機 | `FORM_STATUS`, `F_ModifyMode` | `CONFIRMED` | 未定位 | `NOT_STARTED` | 缺正式 editor state machine |
| BUD-SHELL-004 | Dirty/close gate | `_needClose`, autosave, 主框架關閉閘門 | `CONFIRMED` | 基本頁面離開 | `PARTIAL` | 缺 saving/dirty/blocked 狀態 |
| BUD-SHELL-005 | 自動保存 | `TM_BDGT_AutoSave` | `CONFIRMED` | 未證明 | `NOT_STARTED` | 缺 autosave 契約與失敗恢復 |
| BUD-SHELL-006 | 全案重算狀態 | `F_IsHasConfirmReCal`, `tmrReCalAll` | `CONFIRMED` | recalculate API | `PARTIAL` | 缺待重算與確認狀態 |
| BUD-SHELL-007 | 全量重載 | `F_IsNeedToReloadAllData` | `CONFIRMED` | query refetch | `PARTIAL` | 缺 Domain reload reason |
| BUD-SHELL-008 | 唯讀模式 | `ReadOnlyMode` | `CONFIRMED` | UI disabled | `PARTIAL` | 缺伺服器 capability |
| BUD-SHELL-009 | 多層鎖定 | `IsLocked/IsLockedCnt/IsLockAnalys` | `CONFIRMED` | 未定位 | `NOT_STARTED` | 缺分層 lock policy |
| BUD-SHELL-010 | 專案切換 | `BtnSwitchProject`, `Is_SwitchProject` | `CONFIRMED` | route navigation | `PARTIAL` | 缺 dirty gate 與 context reuse |
| BUD-SHELL-011 | 版本上下文 | budget/change management versions | `CONFIRMED` | 部分 change pages | `PARTIAL` | 缺統一 version context |
| BUD-SHELL-012 | 歷史價格 | `cboHisPrice` | `CONFIRMED` | 部分 comparison | `UI_ONLY` | 缺價格選取對編輯器影響 |
| BUD-SHELL-013 | 主工項精度 | MainItem precision fields | `CONFIRMED` | 通用 rounding | `PARTIAL` | 缺獨立 precision policy |
| BUD-SHELL-014 | 分析精度 | Analysis precision fields | `CONFIRMED` | 通用 rounding | `PARTIAL` | 缺分析層精度 |
| BUD-SHELL-015 | 編輯前後值 | Qty/Cost Before/AfterEdit | `CONFIRMED` | PATCH item | `PARTIAL` | 缺 old/new value 與 row version |

## 工項子編輯器

| Feature ID | Legacy 功能 | C# 來源 | 證據 | Web 現況 | 狀態 | 主要缺口 |
|---|---|---|---|---|---|---|
| BUD-COMP-B-001 | B 類下層自動累算 | `B_Form` | `CONFIRMED` | 預算樹重算 | `PARTIAL` | 尚可直接修改單價的風險 |
| BUD-COMP-B-002 | B 類 Action 上下文 | `B_Form._ActionName` | `CONFIRMED` | 未定位 | `NOT_STARTED` | 缺 action-aware rollup |
| BUD-COMP-L-001 | L 類直接單價 | `L_Form.txtCost` | `CONFIRMED` | item unit_price | `PARTIAL` | 缺 kind-specific editor |
| BUD-COMP-L-002 | L 類可編輯控制 | `SetCostInputEnabled` | `CONFIRMED` | UI disabled | `PARTIAL` | 缺後端 capability enforcement |
| BUD-COMP-L-003 | L 類數字驗證 | `txtCost_Validating` | `CONFIRMED` | request validation | `PARTIAL` | 必須改 Decimal 並對照錯誤 |
| BUD-COMP-S-001 | ItemB 加總來源 | `S_Form2.LoadingData` | `CONFIRMED` | 未定位 | `NOT_STARTED` | 缺來源項管理 |
| BUD-COMP-S-002 | BUD 正負號 | `VarSign` | `CONFIRMED` | 未定位 | `NOT_STARTED` | 缺 signed source list |
| BUD-COMP-S-003 | 自訂變數 | `PCals.GetCustomVarList` | `CONFIRMED` | 未定位 | `NOT_STARTED` | 缺穩定變數與別名 |
| BUD-COMP-S-004 | 分段費率 | `ItemC` | `CONFIRMED` | 未定位 | `NOT_STARTED` | 缺區間、費率及排序 |
| BUD-COMP-S-005 | 預設費率區間 | `PreSetRange` | `CONFIRMED` | 未定位 | `NOT_STARTED` | 缺 Legacy defaults |
| BUD-COMP-S-006 | Formula capability | formula column + project capability | `CONFIRMED` | 未定位 | `NOT_STARTED` | 缺 schema/project capability gate |
| BUD-COMP-S-007 | Issue 上下文 | `F_Issue` | `CONFIRMED` | 未定位 | `NOT_STARTED` | 缺 issue/version key |
| BUD-COMP-S-008 | Action 決定 srckind | `GetActionNameString` | `CONFIRMED` | generic budget tables | `PARTIAL` | 缺 action-specific persistence |

## 必要測試

```text
test_BUD_SHELL_001_bud_and_bid_use_distinct_domain_modes
test_BUD_SHELL_004_dirty_editor_blocks_project_switch
test_BUD_SHELL_005_autosave_failure_preserves_dirty_state
test_BUD_SHELL_009_analysis_lock_rejects_direct_api_mutation
test_BUD_SHELL_013_main_and_analysis_precision_are_independent
test_BUD_COMP_B_001_rollup_cost_cannot_be_overwritten
test_BUD_COMP_L_002_direct_cost_requires_capability
test_BUD_COMP_S_002_negative_rollup_source_is_preserved
test_BUD_COMP_S_004_rate_tiers_reject_overlap
test_BUD_COMP_S_006_formula_requires_project_capability
```

## 下一批來源

- `FormBudgetEditMain`
- `Z_Form`
- `S_Form2` 保存與刪除事件
- `frmBudget` closing/autosave/recalculate/grid events
