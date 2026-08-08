# PCCES Web

PCCES Web 是既有 PCCES C# 系統的 Web／Local Go 現代化與功能對等專案。開發原則是以 Legacy C# 行為為權威基準，Web/Python、Local Go、Frontend、資料庫、永久測試與 CI 同步完成，並以 Traceability Matrix 保存 Legacy → 新系統的證據鏈。

## Current Status

更新日期：2026-08-08

狀態定義：`VERIFIED` 表示 Web/Python、Local Go、永久測試與正式路由均有證據；最終 Roadmap 完成仍需要全量 CI／Golden Fixture／Migration／Restore／Security／Frontend Build／E2E 的實際通過證據。

```text
Phase 0  基礎契約／授權／持久化               VERIFIED
Phase 1  基礎功能                             VERIFIED
Phase 2  核心資料與作業                       VERIFIED
Phase 3  MRS／工料機／單價分析                VERIFIED / CLOSED
Phase 4  預算／投標／成本結構                  VERIFIED
Phase 5  契約／分包                           VERIFIED
Phase 6  估驗／結算／驗收                     VERIFIED
Phase 7  報表中心                             VERIFIED
Phase 8  系統管理／備份／健康檢查              VERIFIED
Phase 9  Legacy Catalog／Golden／Production    IMPLEMENTED，FINAL CI EVIDENCE PENDING
```

`docs/development/phase5-9-traceability-matrix.md` 已記錄 Phase 5～9 各 capability 的 Web/Python、Local Go、Frontend/contract 與永久測試證據。Phase 5～8 的主要 Domain、API、Local Go 與永久測試閉環已落地；Phase 9 的最終完成判定仍要求全量 Legacy Catalog 零 UNKNOWN、Golden Fixtures、migration／restore、security、frontend build 與 E2E 的實際 CI 通過證據。

## 目前真正剩餘的完成閘門

1. 在 Windows self-hosted GitHub Actions 上執行完整 Phase 5～9 completion gate。
2. 確認 Legacy Catalog 為零 UNKNOWN。
3. 執行 Golden Fixtures。
4. Migration／backup／restore smoke test。
5. Security gate。
6. Frontend production build。
7. Web/Python + Local Go E2E。
8. 全部通過後才將整體 Roadmap 標記為 `FULLY VERIFIED`。

## CI

GitHub Actions 已遷移至 Windows self-hosted runner 路線並採手動觸發。2026-08-08 起，GitHub-hosted Actions 分鐘／帳務不再作為主要 CI 執行資源；實際完成度仍以 self-hosted CI 的真實測試結果為準。

## 權威文件

- `docs/development/00-web-parity-phase-roadmap.md`
- `docs/development/01-phase-acceptance-matrix.md`
- `docs/development/phase-3-mrs-traceability-matrix.md`
- `docs/development/phase4-traceability-matrix.md`
- `docs/development/phase5-9-traceability-matrix.md`
- `tasks/implementation-roadmap.md`

## 完成原則

不得只因 Production Code 已存在就宣稱整個 PCCES Web 完成。最終狀態必須由 Legacy Traceability、永久測試、Windows self-hosted CI 與 Production E2E 共同證明。
