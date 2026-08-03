# Phase 3 MRS／工料機／單價分析 Traceability Matrix

狀態：`VERIFIED`

完成日期：2026-08-02

本矩陣是 Phase 3 的正式完成證據。每一列都必須同時具備 Legacy 入口、Web/Python 實作、Local Go 實作、永久測試與 CI 覆蓋；缺少任一欄即不得標記 `VERIFIED`。

| Feature ID | Legacy 功能／入口 | Web／Python | Local Go | 永久測試 | 狀態 |
|---|---|---|---|---|---|
| P3-MRS-01 | `frmMrsBase.cs`：工料機維護、搜尋、分類 | `api/mrs_catalog.py` | `pcces-go/internal/storage/sqlite/mrs_catalog_repository.go` | `api/test_mrs_catalog.py`、`mrs_catalog_repository_test.go` | VERIFIED |
| P3-MRS-02 | `FormMrsBaseBreakdown.cs`：單價分析展開與複價 | `api/mrs_catalog.py` | `mrs_catalog_repository.go` | `test_mrs_catalog.py`、`mrs_catalog_repository_test.go` | VERIFIED |
| P3-MRS-03 | `FormMrsBaseBreakdown_Addnew.cs`：新增／挑選分析項 | `api/mrs_catalog.py` | `mrs_catalog_repository.go` | Catalog recipe component tests | VERIFIED |
| P3-MRS-04 | `FormBudgetRes.cs`：專案資源彙總 | `api/resource_budget_links.py` | `resource_budget_links_repository.go` | `test_resource_budget_links.py`、Go repository tests | VERIFIED |
| P3-MRS-05 | `FormBudgetRes.cs`：引用工項雙向追蹤 | `api/resource_budget_lineage.py`、`resource_budget_links.py` | `resource_budget_lineage_repository.go`、`resource_budget_links_repository.go` | Web／Go lineage tests | VERIFIED |
| P3-MRS-06 | Legacy 資源換碼／替換 | `api/resource_operations.py` | `resource_operations_repository.go` | `test_resource_operations.py`、Go operation tests | VERIFIED |
| P3-MRS-07 | Legacy 批次價格更新與重算 | `api/resource_operations.py` | `resource_operations_repository.go` | Atomic rollback／recalculation tests | VERIFIED |
| P3-MRS-08 | `CODECHECK/CodeValidator.cs`：PCCES Code 驗證 | `api/mrs_code.py` | `mrs_code.go` | `test_mrs_code.py`、`mrs_code_test.go` | VERIFIED |
| P3-MRS-09 | Legacy Code Fitter／單位別名 | `api/mrs_code.py` | `mrs_code.go` | Code normalization tests | VERIFIED |
| P3-MRS-10 | `MrsBase.Bookmark/*`：書籤與收藏 | `api/mrs_catalog.py` | `mrs_catalog_repository.go` | Bookmark isolation／filter tests | VERIFIED |
| P3-MRS-11 | 歷史價格與來源追蹤 | `api/mrs_history_apply.py`、`mrs_operations.py` | `mrs_history_apply.go`、`mrs_operations_repository.go` | `test_mrs_history_apply.py`、Go history tests | VERIFIED |
| P3-MRS-12 | 歷史工率／分析數量套用 | `api/mrs_history_apply.py` | `mrs_rate_history_apply.go` | `test_mrs_rate_history_apply.py`、Go rate tests | VERIFIED |
| P3-MRS-13 | 父專案／歷史專案資源引用 | `api/resource_project_reference.py` | `resource_project_reference_repository.go` | Web／Go project-reference tests | VERIFIED |
| P3-MRS-14 | 主工項／分析項分離精度 | `api/mrs_precision_policy.py` | `mrs_precision_policy_repository.go` | `test_mrs_precision_policy.py`、Go precision tests | VERIFIED |
| P3-MRS-15 | MRS JSON／CSV 匯入 | `api/mrs_exchange.py` | MRS import job repositories | `test_mrs_operations.py`、Go import tests | VERIFIED |
| P3-MRS-16 | `FormMrsBase_ExpWizard.cs`、`FormBudgetRes.cs`：Excel 匯出 | `api/mrs_excel_export.py` | `mrs_excel_export.go` | `test_mrs_excel_export.py`、Go XLSX tests | VERIFIED |
| P3-MRS-17 | MRS Release／核定／發布治理 | `api/mrs_governance_paging.py` | `mrs_governance_repository.go` | Governance state／audit tests | VERIFIED |
| P3-MRS-18 | 模板／核定／歸檔／人工唯讀 | `api/mrs_project_state.py` | `mrs_project_state_repository.go` | Web／Go project-state tests | VERIFIED |
| P3-MRS-19 | MRS Governance Audit 與分頁 | `api/mrs_governance_paging.py` | `mrs_governance_repository.go` | Paging/filter/payload tests | VERIFIED |
| P3-MRS-20 | MRS 前端治理工作台契約 | `web-pcces/frontend/src/pages/MrsGovernancePage.tsx` | 同一 API 契約 | `api/test_mrs_governance_frontend_contract.py`、production build | VERIFIED |

## Legacy 相容驗收結論

1. 單價分析、工料機與預算工項具有雙向且可持久化的來源關係。
2. 所有價格與工率套用均使用 Decimal、Row Version 與交易邊界；失敗不留下部分資料。
3. 主工項與分析項精度政策分離，不以單一全域小數位取代 Legacy 規則。
4. 父專案、歷史專案、歷史價格、歷史工率與書籤皆保留來源身分與 Deep Link。
5. 核定、模板、歸檔與人工鎖定均在 API 層強制唯讀，不能由直接請求繞過。
6. Web/Python 與 Local Go 對外契約已成對實作，並由永久測試保護。
7. MRS Excel 匯出保留 Legacy 中文雙 Grid 語意與專案精度格式。

## Phase Gate

Phase 3 僅在下列條件全部成立時視為完成：

- 本矩陣所有列為 `VERIFIED`。
- `api/test_phase3_traceability.py` 通過。
- Decimal Core Integration 的 Web、Frontend、Go 三個 Job 通過。
- 不存在未完成或未對應 Legacy 入口的 Phase 3 Feature。

結論：Phase 3 功能與永久驗收資料已齊備，可進入 Phase 4。
