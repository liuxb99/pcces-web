# PCCES C# → Web：預算工項編輯追蹤矩陣

更新日期：2026-08-02

## 狀態

- `NOT_STARTED`
- `UI_ONLY`
- `PARTIAL`
- `IMPLEMENTED`
- `LEGACY_MATCHED`
- `VERIFIED`

| Feature ID | Legacy 行為 | C# 來源 | 證據 | Web 現況 | 狀態 | 缺口 |
|---|---|---|---|---|---|---|
| BUD-EDIT-001 | 編輯上下文包含 Action、Issue、sNo、PrintNo | `FormBudgetEditMain` properties | `CONFIRMED` | Budget item API | `PARTIAL` | DTO 尚未完整表達 Action／Issue／PrintNo |
| BUD-EDIT-002 | B/F/L/S/U/Z 類型映射 | `SetItemType` | `CONFIRMED` | 通用 item type | `PARTIAL` | 缺正式 enum 與 Domain 規則 |
| BUD-EDIT-003 | 切換類型替換子編輯器 | `Reload_ChildForm` | `CONFIRMED` | 前端表單 | `UI_ONLY` | 缺後端類型轉換 command |
| BUD-EDIT-004 | 中文／英文單位來自 UserDefind | `GetUnit_DataSet` | `CONFIRMED` | 自由文字 | `NOT_STARTED` | 缺共用辭彙 API |
| BUD-EDIT-005 | 取位為負時要求攤提項目 | `BtnOK_Click` | `CONFIRMED` | 未定位 | `NOT_STARTED` | 缺 rounding/share 關聯驗證 |
| BUD-EDIT-006 | L 類中文單位必填 | `BtnOK_Click` | `CONFIRMED` | 一般 validation | `PARTIAL` | 未依 item kind 驗證 |
| BUD-EDIT-007 | 公式使用 Legacy parser 驗證 | `ArchChkFormula2` | `CONFIRMED` | 未定位 | `NOT_STARTED` | 缺後端公式 parser 契約 |
| BUD-EDIT-008 | SubChange 使用 ChgQty／ChgCost | `BtnOK_Click` | `CONFIRMED` | Change 模組 | `NOT_STARTED` | 缺 Action-specific field mapping |
| BUD-EDIT-009 | 保存完整 ItemA 欄位 | `ItemA.UpdItem` | `CONFIRMED` | item update | `PARTIAL` | 欄位與副作用不完整 |
| BUD-EDIT-010 | Z 類清空 itemNo | `BtnOK_Click` | `CONFIRMED` | 未定位 | `NOT_STARTED` | 缺後端不變量 |
| BUD-EDIT-011 | 非 S 類刪除 ItemC | `BtnOK_Click` | `CONFIRMED` | 未定位 | `NOT_STARTED` | 缺類型轉換資料清理 |
| BUD-EDIT-012 | 保存攤提 ShareSno | `BtnOK_Click` | `CONFIRMED` | 未定位 | `NOT_STARTED` | 缺 share relation |
| BUD-EDIT-013 | PrintToAnalysis 只適用 B 類 | `BtnOK_Click` | `CONFIRMED` | 未定位 | `NOT_STARTED` | 缺 capability validation |
| BUD-EDIT-014 | 保存 PCCES Code | `BtnOK_Click` | `CONFIRMED` | item code | `PARTIAL` | 尚未證明相容欄位與規則 |
| BUD-EDIT-015 | 關閉必須 ItemA_UnLock | `FormClosing` | `CONFIRMED` | 無正式 row lock | `NOT_STARTED` | 缺編輯租約與釋放 |
| BUD-KIND-B-001 | B 類單價由子層累算 | `B_Form` | `CONFIRMED` | 一般 unit_price | `NOT_STARTED` | API 仍可能允許覆寫 |
| BUD-KIND-L-001 | L 類直接輸入單價 | `L_Form` | `CONFIRMED` | unit_price | `PARTIAL` | 缺鎖定、Action 與單位規則 |
| BUD-KIND-F-001 | F 類使用 rate | `F_Form` / `FormBudgetEditMain` | `CONFIRMED` | 未定位 | `NOT_STARTED` | 缺 rate model 與 VDF1 |
| BUD-KIND-S-001 | S 類管理 ItemB／ItemC | `S_Form`, `S_Form2` | `CONFIRMED` | 未定位 | `NOT_STARTED` | 缺來源、區間、費率與公式 |
| BUD-KIND-U-001 | U 類使用公式 | `U_Form` | `CONFIRMED` | 未定位 | `NOT_STARTED` | 缺 parser 與計算引擎 |
| BUD-KIND-Z-001 | Z 類使用加總來源 | `Z_Form` | `CONFIRMED` | 未定位 | `NOT_STARTED` | 缺 ItemB relation |
| BUD-KIND-Z-002 | Z 類特殊 PrintNo 禁止挑選 | `Z_Form_Load` | `CONFIRMED` | 未定位 | `NOT_STARTED` | 業務名稱尚待追蹤 |
| BUD-LOCK-001 | 開啟工項需取得鎖 | 與 `ItemA_UnLock` 對應入口待追 | `INFERRED` | 無 | `NOT_STARTED` | 需找到確切 Lock 呼叫 |
| BUD-TXN-001 | ItemA、ItemB、ItemC 類型轉換原子化 | 多個保存副作用 | `REQUIRED` | 多 CRUD | `NOT_STARTED` | 必須改為單一 Domain transaction |

## 必要回歸測試

```text
test_BUD_EDIT_002_each_kind_loads_correct_editor
test_BUD_EDIT_005_negative_rounding_requires_share_item
test_BUD_EDIT_007_invalid_formula_is_rejected
test_BUD_EDIT_008_subchange_updates_change_fields_only
test_BUD_EDIT_010_z_kind_clears_item_number
test_BUD_EDIT_011_switching_from_s_removes_itemc_atomically
test_BUD_EDIT_015_closing_editor_releases_lock
test_BUD_KIND_B_001_child_rollup_cost_cannot_be_overridden
test_BUD_KIND_S_001_tiered_rules_round_trip
test_BUD_TXN_001_failed_kind_change_rolls_back_all_tables
```
