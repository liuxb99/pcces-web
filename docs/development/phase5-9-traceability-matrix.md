# Phase 5～9 Traceability Matrix

更新日期：2026-08-02

狀態定義：`VERIFIED` 表示 Web/Python、Local Go、永久測試與正式路由均有證據；`INTEGRATION_TESTING` 表示主程式已完成但仍等待全量 CI／Legacy golden fixture；`OPEN` 表示不得宣稱完成。

| ID | Capability | Legacy entry | Web/Python | Local Go | Frontend/contract | Status |
|---|---|---|---|---|---|---|
| P5-01 | 核定預算建立契約與來源 lineage | FormSplitContract | `contract_core.py` | `contract_core.go` | `roadmapClient.ts` | VERIFIED |
| P5-02 | 分配上限、基準統計、主／分包關聯 | FormSplitCnt_ItemPick | `contract_allocation.py` | `contract_allocation.go` | typed client | VERIFIED |
| P5-03 | 契約版本、送審、核定、鎖定 | DomainModule.Sub | `contract_governance.py` | `contract_governance.go` | typed client | VERIFIED |
| P5-04 | 正式變更案件、核定後 atomic apply | DomainModule.SubChg | `contract_change_governance.py` | `contract_change_cases.go` | typed client | VERIFIED |
| P5-05 | 契約報表版本快照來源 | ucSubCtr | `report_center.py` | `report_admin.go` | report client | VERIFIED |
| P6-01 | 估驗期別、前期、當期、累計 | FormInvoice | `contract_execution.py` | `contract_execution.go` | execution client | VERIFIED |
| P6-02 | 扣款、保留款、調整與淨支付 | FormInvoiceDec2 / IndexNumber | `contract_execution.py` | `contract_execution.go` | execution client | VERIFIED |
| P6-03 | 估驗送審／核定與後期鎖定 | FormInvoiceSummary | `contract_execution.py` | transition repository | execution client | VERIFIED |
| P6-04 | 結算狀態鏈與契約封鎖 | FormSubClose | `contract_execution.py` | `contract_execution_acceptance.go` | execution client | VERIFIED |
| P6-05 | 驗收、缺失、改善、完成與封存 | FormSubFinal | `contract_execution.py` | `contract_execution_acceptance.go` | execution client | VERIFIED |
| P7-01 | 報表定義目錄與 Legacy entry | FormReportViewer / ucCrystalViewer | `report_center.py` | `report_admin.go` | report client | VERIFIED |
| P7-02 | 版本快照報表 Job、進度、失敗、重試 | Report.WebDownload | `report_center.py` + `report_job_lifecycle.py` | report job repository | report client | INTEGRATION_TESTING |
| P7-03 | 有效 PDF／CSV／OOXML XLSX／JSON 與下載稽核 | ExportExcel | `report_center.py` | `report_render.go` | report client | VERIFIED |
| P8-01 | 型別化設定、範圍、預設與 row version | FormSys_A～J | `admin_console.py` | `report_admin.go` | admin client | VERIFIED |
| P8-02 | 群組與成員管理、稽核 | SysUser | `admin_console.py` | `admin_operations.go` | admin client | VERIFIED |
| P8-03 | 備份、precheck、hash、smoke、下載 | DBClass / DatabaseUpgrade | `admin_console.py` | `admin_operations.go` | admin client | VERIFIED |
| P8-04 | Health 與資料庫連線檢查 | FormSys_Z | `admin_console.py` | `admin_operations.go` | admin client | VERIFIED |
| P9-01 | 全量 C# 類別 → Feature family Catalog | Source Index | `phase9_legacy_catalog.py` | shared artifact | n/a | INTEGRATION_TESTING |
| P9-02 | Golden fixtures：計算／交換／報表 | all critical Legacy modules | permanent suites + `production_readiness.py` | permanent suites | n/a | INTEGRATION_TESTING |
| P9-03 | Production migration／restore／security／E2E | all | CI gate and backup-restore smoke | CI gate | production build | OPEN |

## Gate decision

Phase 5、Phase 6 與 Phase 8 的主要 Domain 閉環已落地。Phase 7 的失敗／重試 Blueprint 尚待 Canonical 註冊驗收；Phase 9 必須取得全量掃描、Golden、migration／restore、security、frontend 與 E2E 的實際 CI 通過證據後，才能把整個 Roadmap 標記為完成。
