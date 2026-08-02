# Phase 5～9 Traceability Matrix

更新日期：2026-08-02

狀態定義：`VERIFIED` 表示 Web/Python、Local Go、永久測試與正式路由均有證據；`INTEGRATION_TESTING` 表示主程式已完成但仍等待全量 CI／Legacy golden fixture；`OPEN` 表示不得宣稱完成。

| ID | Capability | Legacy entry | Web/Python | Local Go | Frontend/contract | Status |
|---|---|---|---|---|---|---|
| P5-01 | 核定預算建立契約與來源 lineage | FormSplitContract | `contract_core.py` | `contract_core.go` | `roadmapClient.ts` | VERIFIED |
| P5-02 | 分配上限、基準統計、主／分包關聯 | FormSplitCnt_ItemPick | `contract_allocation.py` | `contract_allocation.go` | typed client | VERIFIED |
| P5-03 | 契約版本、送審、核定、鎖定 | DomainModule.Sub | `contract_governance.py` | `contract_governance.go` | typed client | VERIFIED |
| P5-04 | 正式變更案件、核定後 atomic apply | DomainModule.SubChg | `contract_change_governance.py` | legacy change repository + governed Web parity | typed client | INTEGRATION_TESTING |
| P5-05 | 契約報表版本快照來源 | ucSubCtr | `report_center.py` | `report_admin.go` | report client | VERIFIED |
| P6-01 | 估驗期別、前期、當期、累計 | FormInvoice | `contract_execution.py` | `contract_execution.go` | execution client | VERIFIED |
| P6-02 | 扣款、保留款、調整與淨支付 | FormInvoiceDec2 / IndexNumber | `contract_execution.py` | `contract_execution.go` | execution client | VERIFIED |
| P6-03 | 估驗送審／核定與後期鎖定 | FormInvoiceSummary | `contract_execution.py` | transition repository | execution client | VERIFIED |
| P6-04 | 結算狀態鏈與契約封鎖 | FormSubClose | `contract_execution.py` | settlement repository | execution client | VERIFIED |
| P6-05 | 驗收、缺失、改善、完成與封存 | FormSubFinal | `contract_execution.py` | schema/route foundation | execution client | INTEGRATION_TESTING |
| P7-01 | 報表定義目錄與 Legacy entry | FormReportViewer / ucCrystalViewer | `report_center.py` | `report_admin.go` | report client | VERIFIED |
| P7-02 | 版本快照報表 Job、進度、失敗、重試 | Report.WebDownload | `report_center.py` | report job repository | report client | VERIFIED |
| P7-03 | PDF／CSV／XLSX／JSON 產物與下載稽核 | ExportExcel | `report_center.py` | artifact repository | report client | INTEGRATION_TESTING |
| P8-01 | 型別化設定、範圍、預設與 row version | FormSys_A～J | `admin_console.py` | `report_admin.go` | admin client | VERIFIED |
| P8-02 | 群組與成員管理、稽核 | SysUser | `admin_console.py` | schema foundation | admin client | INTEGRATION_TESTING |
| P8-03 | 備份、precheck、hash、smoke、下載 | DBClass / DatabaseUpgrade | `admin_console.py` | migration foundation | admin client | INTEGRATION_TESTING |
| P8-04 | Health 與資料庫連線檢查 | FormSys_Z | `admin_console.py` | existing health + admin repo | admin client | VERIFIED |
| P9-01 | 全量 C# 類別 → Feature ID 映射 | Source Index | automated scanner/gate pending catalogue closure | same | n/a | OPEN |
| P9-02 | Golden fixtures：計算／交換／報表 | all critical Legacy modules | partial permanent tests | partial permanent tests | n/a | OPEN |
| P9-03 | Production migration／restore／security／E2E | all | CI gate added; execution evidence pending | CI gate added | production build | OPEN |

## Gate decision

Phase 5～8 的主要 Domain 程式已落地，但不得宣稱整個 Roadmap 完成。Phase 9 仍須關閉全量 Legacy Catalog、Golden Fixtures 與 Production Readiness 實際執行證據。
