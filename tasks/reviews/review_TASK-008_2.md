# TASK-008 第 2 次評分 — 比較分析 + 報表增強

## 評分檢查清單

| # | 檢查項目 | 結果 | 備註 |
|---|----------|------|------|
| 1 | 後端比較 API（工項比較） | YES | `POST` + `GET` `/api/compare/budget-items`，支援 POST/GET |
| 2 | 後端 MrsBase 單價比較 API | YES | `POST /api/compare/mrs-base-prices`，含分類篩選/ID 篩選/統計摘要 |
| 3 | Excel 匯出 API | YES | `POST /api/compare/budget-items/export/excel`，含樣式與合計列 |
| 4 | 比較核心邏輯可共用 | YES | `_compare_budget_items_core` 被 API 與 Excel 匯出共同呼叫 |
| 5 | 前端 ComparePage | YES | 雙專案選擇/比較/差異表格/狀態篩選/統計摘要/Excel 匯出 |
| 6 | 前端 MrsBasePriceComparePage | YES | 分類篩選/搜尋/展開工料機組成/統計摘要 |
| 7 | 前端型別定義 | YES | CompareItem / CompareResult / CompareRequest 等均已定義 |
| 8 | 前端路由註冊 | YES | `/app/compare/budget-items` 和 `/app/compare/mrs-prices` 已掛載 |
| 9 | pytest 測試（10+13 項） | YES | 23 項全數通過 |
| 10 | npm run build 通過 | YES | TypeScript + Vite build 無錯誤 |
| 11 | 權限檢查 | YES | 所有 compare endpoint 皆使用 `@require_auth` + `_check_project_access` |

## 第 1 次評分勘誤

| # | 第 1 次評分所述問題 | 實際狀況 | 判定 |
|---|---------------------|----------|------|
| A | `scope` 參數在 `_compare_budget_items_core` 中**未被使用** | `scope` 已由 `_compare_budget_items_core` 傳入 `_flatten_budget_items`（line 4559-4560），`_flatten_budget_items` 依 scope 值過濾 B/Z，實作正確 | ❌ 原報告有誤，不扣分 |
| B | docstring 寫「只取葉節點 (W/L)」與 filter 不一致 | 實際 docstring 寫 `kind not in B/Z`（line 4549），與 filter 一致 | ❌ 原報告有誤，不扣分 |
| C | Excel 欄寬使用 `chr(64 + i)` 僅支援 A–Z | 該處固定 13 個欄位（A–M），`chr(64+1)` 到 `chr(64+13)` 完全覆蓋，無 bug | ❌ 原報告有誤，不扣分，降為 nit |

> 以上 3 項第 1 次評分為誤判，本次評分已排除這些項目。

## 四項細項評分

### 1. 功能性（滿分 25）— **22 分**

**優勢：**
- scope 邏輯已正確實作：`"leaf"` 排除 B/Z 節點，`"all"` 取全部，前後端一致
- 工項比較支援多維度差異分析（數量/單價/金額差異 + 百分比）
- 4 種狀態分類（added / removed / modified / unchanged）與前端篩選
- MrsBase 單價一覽提供統計摘要（總數/平均/最高/最低單價）
- Excel 匯出包含完整樣式（標題、表頭、顏色標示、合計列）
- 前端支援展開工料機組成明細（`is_analysis` 項目）

**扣分原因：**
- Excel 下載檔名無法正確從回應中擷取（詳見下節 Bug#1），前端永遠回退到固定檔名 `PCCES_比較報表.xlsx`，後端產生的完整檔名（含兩個專案名稱）未被使用
- MrsBase 搜尋後 `setSummary(null)`（MrsBasePriceComparePage.tsx:80），統計摘要卡片消失，使用者搜尋後無法看到平均/最高/最低單價
- GET endpoint 未傳遞 scope 參數（僅使用預設 leaf）

### 2. 程式品質與架構（滿分 25）— **22 分**

**優勢：**
- 比較核心邏輯獨立為 `_compare_budget_items_core`，API 與 Excel 匯出共用，避免重複
- Flask 路由設計清晰：POST/GET 雙版本 + 獨立 export/excel 路由
- 前後端型別一致（TypeScript interface ↔ Python dict 結構對應）
- `_flatten_budget_items` + `make_key` 設計簡潔，以 `set` 聯集走訪所有 key
- scope 流程完整：`前端 scope → API → _compare_budget_items_core → _flatten_budget_items`，參數鏈無斷裂
- 前端 useCallback / useMemo 使用恰當

**扣分原因：**
- **Bug#1（應修復）**：`ComparePage.tsx:95-103` — `(blob as any)?.headers?.['content-disposition']` 從 Blob 物件讀取 headers，永遠為 undefined。Axios 攔截器或 `api.ts:exportExcel` 應回傳完整 response 物件 `{data, headers}` 而非僅 `res.data`，或由後端將檔名放入回應 body
- GET endpoint (`compare_budget_items_get`) 未傳遞 scope 參數（line 4730），僅使用預設值 `"leaf"`，與 POST 版行為不對稱

### 3. 測試與驗證（滿分 25）— **22 分**

**優勢：**
- 23 項 pytest 全數通過
- 測試涵蓋：同一專案比對、不同專案比對、摘要統計驗證、POST/GET API、缺少參數錯誤處理、Excel 匯出 MIME type
- MrsBase 測試涵蓋分類/工項/書籤/seed 完整性
- 使用獨立記憶體 SQLite 測試資料庫
- `compare_seed_db` fixture 設計精心（雙專案各含不同工項組合）
- npm run build 通過 + TypeScript 型別檢查

**扣分原因：**
- 未測試 `calc_diff` 分母為 0 的 edge case（如雙方數量皆為 0 時 pct 傳回 None 的處理）
- 未測試大量工項（>1000 筆）比較的效能表現
- 未測試前端 API 錯誤處理路徑（network error / 500）
- 未測試 MrsBase 搜尋 API 與前端搜尋的整合

### 4. 使用體驗與安全（滿分 25）— **21 分**

**優勢：**
- 比較結果以顏色區分 added/removed/modified 三類，hover 也有對應背景色
- 差異欄位以正負號 + 百分比同時顯示，>5% 差異處以粗體標示
- 統計摘要卡片一目瞭然
- 狀態篩選下拉 + 顯示計數
- MrsBase 頁面支援分類篩選、文字搜尋、展開工料機組成
- 所有 compare endpoint 均使用 `@require_auth` + `_check_project_access`

**扣分原因：**
- MrsBase 搜尋後統計摘要消失（`setSummary(null)`），使用者需清除搜尋回到全部分類才能重新看到統計數字
- `require_auth` 採免登入模式（無 token 自動以 user_id=1 操作），外網部署需搭配認證閘道
- 比較結果表格 1700px 水平滾動，窄螢幕體驗受限（工程表格可接受）

---

## 總分計算

| 項目 | 得分 | 滿分 |
|------|------|------|
| 功能性 | 22 | 25 |
| 程式品質與架構 | 22 | 25 |
| 測試與驗證 | 22 | 25 |
| 使用體驗與安全 | 21 | 25 |
| **總分** | **87** | **100** |

> 較第 1 次評分（84 分）提升 3 分，主因為第 1 次評分中 3 項誤判（scope 未使用、docstring 不一致、chr(64+i) 限制）經實地確認後排除。

---

## 關鍵問題摘要

### Should-fix（建議合併前修正）

| # | 檔案 | 行號 | 問題 | 建議 |
|---|------|------|------|------|
| 1 | `web-pcces/frontend/src/api.ts`<br>`web-pcces/frontend/src/pages/ComparePage.tsx` | 733-735<br>95-103 | **Excel 下載檔名無法正確擷取**：`exportExcel` 回傳 `res.data`（僅 Blob），前端從 Blob 讀取 `headers['content-disposition']` 永遠為 undefined，檔名永遠回退到固定值 `PCCES_比較報表.xlsx`，忽略後端在 `send_file(download_name=...)` 中產生的完整檔名（含兩專案名稱） | 方案 A：修改 `exportExcel` 回傳 `{data: res.data, filename: 從 res.headers 解析}`；方案 B：後端將檔名放入回應 body 供前端直接取用 |
| 2 | `web-pcces/frontend/src/pages/MrsBasePriceComparePage.tsx` | 78-80 | 搜尋後 `setSummary(null)`，統計摘要卡片消失 | 前端可於搜尋後自行從 `result` 陣列計算 avg/min/max price，或後端搜尋 API 也回傳 summary |
| 3 | `api/index.py` | 4721-4730 | GET endpoint `compare_budget_items_get` 未傳遞 scope 參數給 `_compare_budget_items_core` | 從 `request.args.get("scope", "leaf")` 讀取並傳遞 |

### Nits（可選修）

| # | 檔案 | 行號 | 問題 | 建議 |
|---|------|------|------|------|
| 4 | `api/index.py` | 4616 | `calc_diff` 分母為 0 時 pct 回傳 None，前端顯示 "N/A" | 考量雙方皆為 0 時回傳 0% 而非 N/A |
| 5 | — | — | 未測試大量工項（>1000）比較效能 | 可於後續壓力測試階段補上 |
| 6 | — | — | `require_auth` 免登入模式 | 外網部署前應啟用強制登入驗證 |

---

## 結論

**ship with minor fixes** — 功能完整、scope 參數鏈正確運作、23 項 pytest 全數通過、npm run build 無誤。第 1 次評分中的 3 項誤判經再次確認已排除。應修復的主要項目為 **Excel 下載檔名擷取 Bug**（Should-fix #1）與 **MrsBase 搜尋後統計摘要消失**（Should-fix #2），其餘為可選修項目。

---

*審查日期：2026-05-28 | 審查範圍：TASK-008 比較分析模組第 2 次評分*
