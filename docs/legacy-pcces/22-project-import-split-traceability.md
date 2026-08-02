# PCCES 專案匯入／分拆追蹤矩陣

更新日期：2026-08-02

## 狀態

- `NOT_STARTED`
- `UI_ONLY`
- `PARTIAL`
- `IMPLEMENTED`
- `LEGACY_MATCHED`
- `VERIFIED`

## 匯入

| Feature ID | Legacy 行為 | C# 來源 | 證據 | Web 現況 | 狀態 | 缺口 |
|---|---|---|---|---|---|---|
| PROJECT-IMPORT-001 | 新舊 XML 分流 | `IsOldXML` / `ImportXMLInOldWay` / `ImportXML` | `CONFIRMED` | 無正式匯入服務 | `NOT_STARTED` | 缺版本辨識與雙路徑 |
| PROJECT-IMPORT-002 | ZMD 固定密碼解壓 | `MyZip.Open(..., "ARCH13139409")` | `CONFIRMED` | 無 | `NOT_STARTED` | 缺安全沙箱與解壓限制 |
| PROJECT-IMPORT-003 | ZMD 內容必須包含 MDB | ZMD branch | `CONFIRMED` | 無 | `NOT_STARTED` | 缺內容驗證 |
| PROJECT-IMPORT-004 | 舊格式欄位補齊 | `CloseBidDate` / `CheckOut` | `CONFIRMED` | 無 migration adapter | `NOT_STARTED` | 缺 Legacy schema adapter |
| PROJECT-IMPORT-005 | CheckOut 文件辨識 | `CheckOut == CKOUT` | `CONFIRMED` | 無 | `NOT_STARTED` | 後處理仍待追蹤 |
| PROJECT-IMPORT-006 | 特定 PID 要求發包認證 | `PccCodeCert` | `CONFIRMED` | 無 | `NOT_STARTED` | 缺部署政策與用途驗證 |
| PROJECT-IMPORT-007 | Domain InputXML 建立專案 | `Project.InputXML` | `CONFIRMED` | 一般 project create | `NOT_STARTED` | CRUD 不等於匯入 |
| PROJECT-IMPORT-008 | 從成功訊息擷取 ProjectCode | `F_NewProjectCode` | `CONFIRMED` | API id | `NOT_STARTED` | 應改結構化 projectCode 回應 |
| PROJECT-IMPORT-009 | `[跳頁]` 轉 PageBreak | Items memo loop | `CONFIRMED` | 無 | `NOT_STARTED` | 缺報表分頁語意 |
| PROJECT-IMPORT-010 | `[發包]` 轉 IsBid | Items memo loop | `CONFIRMED` | 無 | `NOT_STARTED` | 缺發包標記 |
| PROJECT-IMPORT-011 | Tenderer 建立 sub_memo | Tenderer branch | `CONFIRMED` | 無 | `NOT_STARTED` | 缺廠商附屬資料 |
| PROJECT-IMPORT-012 | 更新來源檔名 | `Project.UpdItem` | `CONFIRMED` | 無 | `NOT_STARTED` | 缺來源追蹤 |
| PROJECT-IMPORT-013 | 建立者取得 ProjAuthority | SQL insert | `CONFIRMED` | owner relation | `PARTIAL` | 需 Legacy 專案權限模型 |
| PROJECT-IMPORT-014 | 附件移入 DBName/ProjectCode 路徑 | AddOn flow | `CONFIRMED` | 無 | `NOT_STARTED` | 缺附件儲存與補償交易 |
| PROJECT-IMPORT-015 | 失敗不得繼續 Commit | 多個 return branch | `CONFIRMED` | 未證明 | `NOT_STARTED` | 缺 atomic commit |
| PROJECT-IMPORT-016 | 匯入警告與成功訊息分離 | `sRet` | `CONFIRMED` | 無 | `NOT_STARTED` | 缺 warnings schema |

## 分拆與回復

| Feature ID | Legacy 行為 | C# 來源 | 證據 | Web 現況 | 狀態 | 缺口 |
|---|---|---|---|---|---|---|
| PROJECT-SPLIT-001 | 只在 NOR 狀態可編輯 | `F_SPLT_STATUS` | `CONFIRMED` | 無 | `NOT_STARTED` | 缺 split session state machine |
| PROJECT-SPLIT-002 | 勾選項寫入 chk | `Do_SaveCheckItem` | `CONFIRMED` | 無 | `NOT_STARTED` | 缺選取模型 |
| PROJECT-SPLIT-003 | 儲存 SplQty / SplCost | `Do_SaveCheckItem` | `CONFIRMED` | 無 | `NOT_STARTED` | 缺 Decimal 欄位 |
| PROJECT-SPLIT-004 | `ItemA.CopyItemA` 複製工項 | `Do_SaveCheckItem` | `CONFIRMED` | 無 | `NOT_STARTED` | 缺 Domain copy service |
| PROJECT-SPLIT-005 | 「式」與 B 類只能輸入 SplCost | Grid edit handlers | `CONFIRMED` | 無 | `NOT_STARTED` | 缺欄位策略 |
| PROJECT-SPLIT-006 | 一般項目只能輸入 SplQty | Grid edit handlers | `CONFIRMED` | 無 | `NOT_STARTED` | 缺欄位策略 |
| PROJECT-SPLIT-007 | 非正值不接受 | Grid edit handlers | `CONFIRMED` | 無 | `NOT_STARTED` | 缺 validation |
| PROJECT-SPLIT-008 | 父子節點勾選傳播 | `AfterEdit` | `CONFIRMED` | 無 | `NOT_STARTED` | 缺 tree selection semantics |
| PROJECT-SPLIT-009 | 使用專案精度格式化 | `F_MainQty/F_MainCst/...` | `CONFIRMED` | float/通用格式 | `NOT_STARTED` | 必須 Decimal + precision policy |
| PROJECT-SPLIT-010 | 建立子專案 | `SaveProjectInfo` | `CONFIRMED` | 無 | `NOT_STARTED` | 缺 child project lifecycle |
| PROJECT-SPLIT-011 | 子專案建立者取得權限 | `ProjAuthority` insert | `CONFIRMED` | 無 | `NOT_STARTED` | 缺權限建立 |
| PROJECT-SPLIT-012 | 取消未完成分拆時 DeleAll | `C_Btn_Cncl_Click` | `CONFIRMED` | 無 | `NOT_STARTED` | 缺 rollback |
| PROJECT-SPLIT-013 | 主子專案 lineage | `F_ProjectCode/F_SubProjectCode` | `CONFIRMED` | 無 | `NOT_STARTED` | 缺 lineage model |

## 必要測試

```text
test_PROJECT_IMPORT_003_rejects_zmd_without_mdb
test_PROJECT_IMPORT_006_rejects_non_certified_budget_for_restricted_pid
test_PROJECT_IMPORT_009_converts_page_break_markers
test_PROJECT_IMPORT_010_converts_bid_markers
test_PROJECT_IMPORT_013_grants_creator_project_authority
test_PROJECT_IMPORT_015_failure_leaves_no_partial_project
test_PROJECT_SPLIT_005_formula_item_accepts_cost_only
test_PROJECT_SPLIT_006_normal_item_accepts_quantity_only
test_PROJECT_SPLIT_008_parent_selection_propagates_to_descendants
test_PROJECT_SPLIT_009_uses_project_decimal_policy
test_PROJECT_SPLIT_012_cancel_rolls_back_draft_child_project
```

## 收口條件

專案生命週期 Segment 只有在以下全部完成後才可標記 `LEGACY_MATCHED`：

1. 建立、匯入、分拆皆有獨立 Domain Service。
2. ProjectCode、Alias、主子 lineage 可追溯。
3. 匯入與分拆具 atomic commit 或可驗證補償交易。
4. 所有失敗路徑不留孤兒專案、權限、工項或附件。
5. 建立者權限與 Legacy 一致。
6. 報表跳頁與發包標記能完整保留。
7. Decimal 精度與原專案設定一致。
