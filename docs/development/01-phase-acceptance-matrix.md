# PCCES Web Phase 驗收矩陣

更新日期：2026-08-02

本文件用於區分「Legacy 調研完成」、「規格完成」、「程式存在」與「正式復刻完成」。

## 1. 統一狀態定義

| 狀態 | 定義 |
|---|---|
| `NOT_STARTED` | 尚未開始局部 C# 深讀或 Web 實作 |
| `LEGACY_DEEP_REVIEW` | 正在深讀該 Phase 對應 C# 事件鏈、Domain 與資料規則 |
| `SPEC_READY` | API、資料模型、狀態機、權限與測試規格已可實作 |
| `IMPLEMENTING` | Backend／Frontend／Migration 開發中 |
| `INTEGRATION_TESTING` | 模組已串接，進行整合與端到端測試 |
| `LEGACY_MATCHING` | 以桌面版測例、資料與報表進行結果對照 |
| `VERIFIED` | 功能、結果、權限、交換格式與回歸測試均符合完成標準 |

## 2. Phase 驗收總表

| Phase | 名稱 | 主要依賴 | Legacy 文件基礎 | Web 起始狀態 | 完成出口 |
|---|---|---|---|---|---|
| 0 | 平台基礎與 API 收斂 | 無 | 高 | `PARTIAL` | 唯一 API、Migration、OpenAPI、權限、WorkContext、Decimal 完成 |
| 1 | 專案管理與生命週期 | Phase 0 | 高 | `PARTIAL` | 專案目錄、Wizard、匯入、分拆、capability、eligibility 完成 |
| 2 | 預算與投標核心 | Phase 0、1 | 中高 | `PARTIAL` | BUD/BID、工項樹、類型計算、重算、Autosave、鎖定完成 |
| 3 | MRS／工料機／單價分析 | Phase 0、1、2 | 中 | `PARTIAL` | MRS catalog、analysis、resource aggregation、history 完成 |
| 4 | 成本結構與資料交換 | Phase 0～3 | 中 | `PARTIAL` | Cost Structure、conversion、import/export、lineage 完成 |
| 5 | 契約與分包 | Phase 0～4 | 中 | `UI_ONLY/PARTIAL` | 預算到契約來源追蹤、版本、核定與鎖定完成 |
| 6 | 變更、估驗、結算、驗收 | Phase 0～5 | 中 | `UI_ONLY/PARTIAL` | 完整履約狀態鏈、累計、扣款、結算、驗收完成 |
| 7 | 報表中心 | Phase 0、2～6 | 中 | `PARTIAL` | Snapshot report、PDF、Excel、async jobs、Legacy 對照完成 |
| 8 | 系統管理與維運 | Phase 0 | 中 | `PARTIAL` | 使用者、群組、細權限、設定、升級、備份復原完成 |
| 9 | Legacy 收尾 | Phase 0～8 | 功能樹已建立 | `NOT_STARTED` | 100% Traceability、golden tests、production readiness 完成 |

表中的「Legacy 文件基礎」只表示已具備功能樹與源碼索引，不代表該 Phase 已完成 C# 深讀。

## 3. 每個 Phase 的必要證據

### A. Legacy 證據

- Feature ID。
- C# 檔案、類別、Form／UserControl／Domain 入口。
- 使用者入口與主要事件鏈。
- 欄位、狀態、權限、交易、計算或格式規則。
- 無法確認部分的可信度標記。

### B. Database 證據

- Migration。
- Schema 與約束。
- Decimal／Numeric 精度。
- 唯一鍵、外鍵、版本欄位與稽核欄位。
- rollback 或向前修復策略。

### C. Backend 證據

- API 契約。
- Domain service。
- 權限與 capability 檢查。
- transaction boundary。
- structured error。
- unit／integration tests。

### D. Frontend 證據

- Legacy 功能入口可到達。
- 可編輯、唯讀、禁用與錯誤狀態正確。
- Dirty state、取消、恢復與重試流程。
- API 錯誤不被吞掉。
- E2E tests。

### E. Legacy Matching 證據

- 相同輸入資料。
- 相同計算或狀態結果。
- 差異清單與決策紀錄。
- Golden fixture 或 snapshot。
- 永久回歸測試。

## 4. Phase Gate

### Gate 1：可開始實作

需同時滿足：

- Phase Segment 已有 Feature ID 清單。
- 已重新讀取相關 C# 源碼。
- 已寫出明確非目標與未知項目。
- Database／API／權限／測試方案已定義。

### Gate 2：可宣稱 Implemented

需同時滿足：

- Migration、Backend、Frontend 均存在。
- API contract test 通過。
- 核心單元與整合測試通過。
- 沒有以 mock 資料取代正式 Domain 流程。

### Gate 3：可宣稱 Legacy Matched

需同時滿足：

- Legacy 測例或來源資料已比對。
- 計算、狀態、權限與輸出差異已關閉。
- 有無法一致部分時，必須有 ADR 或明確決策。

### Gate 4：可宣稱 Verified

需同時滿足：

- 永久回歸測試進入 CI。
- Production build 通過。
- Migration 在空庫與升級庫均通過。
- 權限繞過、直接 URL、重送與併發測試通過。
- 文件、Traceability 與實際程式一致。

## 5. 禁止的完成判斷

以下都不能單獨視為完成：

- 頁面已存在。
- API 回傳 200。
- CRUD 可操作。
- Demo 資料可顯示。
- 單一 happy path 測試通過。
- 文件聲稱已完成。
- Reviewer 分數達標但未做 Legacy 對照。

## 6. Phase 0 第一批建議 Segment

### P0-S1：唯一 API 與路徑收斂

- 判定正式後端。
- 統一 app factory、config、error handler。
- 修正 `/api/api`。
- 建立 OpenAPI baseline。
- 加入 contract tests。

### P0-S2：Migration 與 Decimal 基礎

- 建立正式 Migration。
- 金額、數量、單價、費率使用 Numeric。
- 加入 created／updated／row_version／audit 欄位。

### P0-S3：Function Code、Module 與 Action

- 建立 catalog tables。
- API permission guard。
- 前端 route／button capability。
- direct URL bypass tests。

### P0-S4：WorkContext 與 Dirty State

- current action／project／resource context。
- context reuse。
- save／discard／cancel。
- optimistic locking 與 conflict response。

完成 P0-S1～P0-S4 後，Phase 1 才可正式開始。
