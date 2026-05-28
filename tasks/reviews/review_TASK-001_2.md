評分報告 for TASK-001 (第 2 次循環)

評分時間: 2025-06-12T10:30:00Z
評分者: reviewer-agent

評分檢查清單（必須 YES/NO）:
- 是否可執行: YES
- 是否有錯誤: YES
- 是否滿足需求條列: YES
- 是否有測試或滿足審美: NO

評分明細:
- 完整性: 25/25 (全部 11 項 issue 均已修正，包含巢狀樹狀結構、move/updatePrice 雙格式支援、Excel 用 fetch+Authorization、B/Z 金額跳過、PBKDF2-SHA256 密碼、資料隔離、所有權檢查、requirements.txt、CASCADE 刪除、children relationship)
- 正確性: 22/25 (程式邏輯正確，所有修正功能正常運作。minor: `_recalc_children` 對 Z 小計項未做子項加總僅做 qty*price，與 CRUD 中跳過 Z 的行為不一致，但非關鍵錯誤)
- 可維護性: 23/25 (程式碼結構清晰，函式名稱語義明確，使用 helper 函式抽離共用邏輯，有中文註解說明商業邏輯。export_excel 函式較長但屬報表生成正常現象)
- 測試與驗證: 0/25 (原因: 專案中無任何測試檔案，亦無自動化測試覆蓋)

總分: 70/100
結果: 不合格

缺失項目與改進建議:
1. 無任何測試覆蓋 — 建議為 API endpoint 加入 pytest 單元測試，至少涵蓋 CRUD、資料隔離、所有權檢查、密碼驗證等核心邏輯
2. `dashboard_stats` 端點無資料隔離，非管理員使用者會看到全系統統計資料（非本次 list_projects 修正範圍，但建議一併處理）
3. `get_budget_tree` 端點無專案所有權檢查，任何已認證使用者可瀏覽任意專案預算樹
4. `_recalc_children` 中 Z 類型項目 (小計/合計) 應比照 B 類型遞迴加總子項，而非以 qty*price 計算

具體建議:
- 在 `backend/` 下新增 `tests/` 目錄，撰寫 pytest 測試 fixtures 和 SQLite in-memory 資料庫測試
- `dashboard_stats` 可參考 `list_projects` 的資料隔離模式，依使用者角色過濾統計資料
- `_recalc_children` 第 5 行 `if child.kind == BudgetItemKind.B:` 建議改為 `if child.kind in (BudgetItemKind.B, BudgetItemKind.Z):`
