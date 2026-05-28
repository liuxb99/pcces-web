# TASK-008 評分審查 — 比較分析 + 報表增強

## 評分檢查清單

| # | 檢查項目 | 結果 | 備註 |
|---|----------|------|------|
| 1 | 後端比較 API（工項比較） | YES | `POST` + `GET` `/api/compare/budget-items`，支援 POST/GET 兩種呼叫方式 |
| 2 | 後端 MrsBase 單價比較 API | YES | `POST /api/compare/mrs-base-prices`，支援分類篩選、ID 篩選、統計摘要 |
| 3 | Excel 匯出 API | YES | `POST /api/compare/budget-items/export/excel`，含標題/表頭/資料列/合計列與顏色標示 |
| 4 | 比較核心邏輯可共用 | YES | `_compare_budget_items_core` 被 API 與 Excel 匯出共同呼叫 |
| 5 | 前端 ComparePage | YES | 支援雙專案選擇、比較、差異表格、狀態篩選、統計摘要、Excel 匯出 |
| 6 | 前端 MrsBasePriceComparePage | YES | 支援分類篩選、搜尋、展開工料機組成、統計摘要 |
| 7 | 前端型別定義 | YES | CompareItem / CompareResult / CompareRequest / MrsBasePriceCompareResult 等均已定義 |
| 8 | 前端路由註冊 | YES | `/app/compare/budget-items` 和 `/app/compare/mrs-prices` 已掛載 |
| 9 | pytest 測試（10+13 項） | YES | `test_compare.py` 10 項 + `test_mrs_base.py` 13 項，共 23 項全數通過 |
| 10 | npm run build 通過 | YES | TypeScript + Vite build 無錯誤 |
| 11 | 權限檢查 | YES | 所有 compare endpoint 皆使用 `@require_auth` + `_check_project_access` |

---

## 四項細項評分

### 1. 功能性（滿分 25）— **21 分**

**優勢：**
- 工項比較支援多維度差異分析（數量/單價/金額差異 + 百分比）
- 比較結果提供 4 種狀態分類（added / removed / modified / unchanged）與篩選
- MrsBase 單價一覽提供統計摘要（總數/平均/最高/最低單價）
- Excel 匯出包含完整樣式（標題、表頭、顏色標示、合計列）
- 前端支援展開工料機組成明細（`is_analysis` 項目）

**扣分原因：**
- `scope` 參數在前端傳入 (`scope: 'leaf'`)，但在 `_compare_budget_items_core` 中**未被使用**，`_flatten_budget_items` 始終以相同邏輯過濾（B/Z 除外）。若未來需支援 `all` scope（含父項），後端須另行實作。
- MrsBase 搜尋後統計摘要消失（summary 被設為 null），使用者看不到搜尋結果的平均/最高/最低單價。
- 比較 key 僅以 `print_no` 配對，若兩個專案的 `print_no` 編碼體系不同，同一工項無法正確對應。

### 2. 程式品質與架構（滿分 25）— **20 分**

**優勢：**
- 將比較核心邏輯獨立為 `_compare_budget_items_core`，API 與 Excel 匯出共用，避免重複
- Flask 路由設計清晰：POST / GET 雙版本，`export/excel` 獨立路由
- 前後端型別一致（TypeScript interface ↔ Python dict 結構對應）
- `_flatten_budget_items` + `make_key` 設計簡潔，以 `set` 聯集走訪所有 key
- 前端 useCallback / useMemo 使用恰當，避免不必要的 re-render

**扣分原因：**
- `_flatten_budget_items` docstring 寫「只取葉節點 (W/L)」，但實作僅排除 B/Z（F/S/U 不會被排除），docstring 與實作不一致
- **Excel 下載檔名擷取失效**（Bug）：前端 `compareApi.exportExcel` 回傳 `res.data`（Blob），但程式 `(blob as any)?.headers` 試圖從 Blob 物件讀取 headers，永遠為 undefined，檔名永遠回退為 `PCCES_比較報表.xlsx`（ComparePage.tsx:95-103）
- `_compare_budget_items_core` 的 `scope` 參數雖被宣告（line 4557）但函式體內**完全未使用**
- Excel 欄寬使用 `chr(64 + i)` 僅支援 A–Z（26 欄），樣式編碼方式較脆弱

### 3. 測試與驗證（滿分 25）— **22 分**

**優勢：**
- 23 項 pytest 全數通過，測試覆蓋良好
- 測試包含：同一專案比對、不同專案比對、摘要統計驗證、POST/GET API、缺少參數錯誤處理、Excel 匯出 MIME type
- MrsBase 測試涵蓋分類/工項/書籤/seed 完整性
- 使用獨立記憶體 SQLite 測試資料庫，避免污染開發資料
- `compare_seed_db` fixture 精心設計（雙專案各含不同工項組合）確保 diff 邏輯可被驗證
- npm run build 通過 + TypeScript 型別檢查

**扣分原因：**
- 未測試 `calc_diff` 分母為 0 的 edge case（如 A 數量為 0 且 B 數量也為 0 的情境）
- 未測試大量工項（>1000 筆）比較的效能表現
- 未測試前端 API 錯誤處理路徑（如 network error / 500）
- 未測試 MrBase 搜尋 API 與前端搜尋的整合

### 4. 使用體驗與安全（滿分 25）— **21 分**

**優勢：**
- 比較結果以顏色區分 added/removed/modified 三類，hover 也有對應背景色
- 差異欄位以正負號 + 百分比同時顯示，且 >5% 差異處以粗體標示
- 統計摘要卡片（專案 A/B 總額、差異總額、差異百分比）一目瞭然
- 狀態篩選下拉 + 顯示計數，方便聚焦特定類別
- MrsBase 頁面支援分類篩選、文字搜尋、展開工料機組成
- 所有 compare endpoint 均使用 `@require_auth` + `_check_project_access` 權限檢查

**扣分原因：**
- `require_auth` 採免登入模式（無 token 自動以 user_id=1 操作），對外部署時需搭配外部認證閘道或啟用強制登入
- MrsBase 搜尋後統計摘要消失，使用者需回到全部分類才能重新看到平均/最高/最低單價
- 比較結果表格 1700px 水平滾動，窄螢幕操作較不便（但工程表格可接受）

---

## 總分計算

| 項目 | 得分 | 滿分 |
|------|------|------|
| 功能性 | 21 | 25 |
| 程式品質與架構 | 20 | 25 |
| 測試與驗證 | 22 | 25 |
| 使用體驗與安全 | 21 | 25 |
| **總分** | **84** | **100** |

---

## 關鍵問題摘要

### Should-fix（建議合併前修正）

| # | 檔案 | 行號 | 問題 | 建議 |
|---|------|------|------|------|
| 1 | `web-pcces/frontend/src/pages/ComparePage.tsx` | 95 | **Excel 下載檔名無法正確擷取**：`compareApi.exportExcel` 回傳 `res.data`（Blob），但程式從 Blob 物件讀取 `headers['content-disposition']`，永遠為 undefined，檔名永遠回退為固定值 | 改用 Axios 攔截器或修改 `exportExcel` 回傳完整 response (`{ data: Blob, headers }`) 再萃取 disposition |
| 2 | `api/index.py` | 4547-4554 | `_flatten_budget_items` docstring 寫「只取葉節點 (W/L)」，但 filter 僅排除 B/Z（F/S/U 仍保留） | 修正 docstring 或依需求調整 filter 邏輯 |
| 3 | `api/index.py` | 4557 | `scope` 參數傳入 `_compare_budget_items_core` 但**從未使用**，`_flatten_budget_items` 永遠以 same 邏輯執行 | 移除未使用的 `scope` 參數，或實作其對應邏輯 |
| 4 | `web-pcces/frontend/src/pages/MrsBasePriceComparePage.tsx` | 62 | 搜尋後 `setSummary(null)` 導致統計摘要卡片消失（avg/min/max 價格無法查看） | 搜尋結果也回傳 summary 資訊（或由前端自行計算） |

### Nits（可選修）

| # | 檔案 | 行號 | 問題 | 建議 |
|---|------|------|------|------|
| 5 | `api/index.py` | 4805 | Excel 欄寬使用 `chr(64 + i)` 僅支援 A–Z | 改用 openpyxl 的 `get_column_letter` |
| 6 | `api/index.py` | 4616 | `calc_diff` 分母為 0 時 pct 回傳 `None`，前端顯示 "N/A" —— 行為正確但可考慮補 default value | 若雙方皆為 0 時可回傳 0% 而非 N/A |
| 7 | `web-pcces/frontend/src/pages/ComparePage.tsx` | 80 | 前端傳入 `scope: 'leaf'` 但後端未使用 | 移除前端傳入的 scope 或確認後端實作 |

---

## 結論

**ship with minor fixes** — 功能完整、測試通過、build 無誤。主要問題為 Excel 下載檔名擷取 Bug（Should-fix #1）和 `scope` 參數未實作（Should-fix #3），建議於合併前修正此 2 項。其餘皆為可選修項目。
