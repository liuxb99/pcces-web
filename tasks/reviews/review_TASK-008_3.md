# TASK-008 第 3 次評分 — 比較分析模組（第 1 次返工後）

## 四項修復驗證

| # | 修復項目 | 驗證方式 | 結果 |
|---|----------|----------|------|
| 1 | Excel 檔名 bug — `api.ts` 回傳型別改為 `{data, filename}`，從 axios response headers 正確解析 | 閱讀 `api.ts:733-735` + `ComparePage.tsx:92-108` | ✅ **已修復** — `exportExcel` 回傳 `{data: Blob, filename: string}`，前端解構使用 `filename` 作為 `a.download` 屬性；舊的 `(blob as any)?.headers?.['content-disposition']` 殘碼已完全移除 |
| 2 | MrsBase 搜尋後 summary 消失 — 改為從搜尋結果自行計算 avg/max/min | 閱讀 `MrsBasePriceComparePage.tsx:76-91` | ✅ **已修復** — `handleSearch` 從 `result` 陣列過濾 `unit_price`、計算 `avg_price`/`max_price`/`min_price`，空陣列時回傳 0 |
| 3 | GET endpoint 未傳 scope — 補上 `scope = request.args.get("scope", "leaf")` | 閱讀 `api/index.py:4722,4734` | ✅ **已修復** — `compare_budget_items_get` 讀取 query param `scope` 並以 keyword argument `scope=scope` 傳入 `_compare_budget_items_core` |
| 4 | 新增 zero-division 測試 | 閱讀 `test_compare.py:215-254` | ✅ **已修復** — `test_compare_zero_division` 測試雙方 quantity/unit_price/amount 皆為 0 的情境，驗證 `diff_pct` 三個欄位皆為 `None` |

## 回歸檢查

- `_compare_budget_items_core` 簽章 `(db, project_a_id, project_b_id, scope="leaf")` — **scope 參數鏈完整**：`GET/POST → core → _flatten_budget_items`，無斷裂
- `calc_diff` 零分母保護邏輯無變動 — `a_val == 0 → pct = None`，與測試一致
- MrsBase 比較 API (`compare_mrs_base_prices`) summary 計算不受影響 — 正常情境仍由後端計算回傳，僅搜尋情境改由前端計算
- npm run build — 用戶通報通過 ✅
- 11 項 pytest — 用戶通報全部通過 ✅

## 四項細項評分

### 1. 功能性（滿分 25）— **24 分**

| 項目 | 分數 | 原因 |
|------|------|------|
| 工項比較（POST/GET） | 7/7 | scope 參數前後端一致；added/removed/modified/unchanged 四種狀態；量/價/額三維度差異 + 百分比 |
| MrsBase 單價一覽 | 7/7 | 分類篩選、搜尋、展開工料機組成、統計摘要（搜尋後仍保留） |
| Excel 匯出 | 5/5 | 完整樣式 + 合計列；**檔名 now correctly includes project names via Content-Disposition header** |
| 前端整合 | 5/6 | 路由註冊、型別定義、useCallback/useMemo 使用恰當；窄螢幕 1700px 水平捲動為工程表格固有限制 |

**扣分**：無。Excel 檔名 bug 與 GET scope 缺失均已修復。

### 2. 程式品質與架構（滿分 25）— **24 分**

| 項目 | 分數 | 原因 |
|------|------|------|
| 模組劃分 | 5/5 | 比較核心獨立為 `_compare_budget_items_core`，API + Excel 匯出共用 |
| API 設計 | 5/5 | GET/POST 雙路由、獨立 export/excel 路由、scope 參數鏈無斷裂 |
| 前端架構 | 5/5 | TypeScript 型別一致、axios 攔截器、response 封裝 |
| 程式細節 | 5/5 | Excel 檔名正確從 headers 解析、MrsBase 搜尋 summary 前端自行計算（無多餘 API 呼叫） |
| 零缺陷 | 4/5 | 無 blocking bug；`require_auth` 免登入模式（外網部署前需啟用強制驗證，屬已認知設計取捨） |

### 3. 測試與驗證（滿分 25）— **24 分**

| 項目 | 分數 | 原因 |
|------|------|------|
| 測試覆蓋率 | 7/7 | POST/GET 端點、同一/不同專案、摘要驗證、缺少參數、Excel MIME type、**zero-division edge case** |
| 測試隔離 | 5/5 | 獨立記憶體 SQLite、fixture 設計精心、雙專案 seed 資料 |
| 前端驗證 | 5/5 | npm run build 無 TypeScript 錯誤 |
| 邊界情況 | 7/8 | **新增 zero-division 測試**；但仍缺少大規模（>1000 筆）效能壓力測試與前端錯誤路徑模擬 |

**改善**：較第 2 次評分新增 **zero-division 邊界測試**，補上之前最大的測試缺口。

### 4. 使用體驗與安全（滿分 25）— **23 分**

| 項目 | 分數 | 原因 |
|------|------|------|
| UI 清晰度 | 6/6 | 顏色標示、統計摘要、狀態篩選、hover 效果 |
| MrsBase 頁面 | 6/6 | 分類篩選、搜尋、**搜尋後統計摘要仍保留**（原有 UX 缺陷已修復） |
| Excel 匯出流程 | 5/5 | **下載檔名正確**（含兩專案名稱），回饋訊息 clear |
| 安全 | 6/8 | 所有 endpoint 使用 `@require_auth` + `_check_project_access`；`require_auth` 免登入模式為已知限制 |

**改善**：搜尋後統計摘要保留（+1 分）與 Excel 檔名正確（+1 分），較第 2 次評分提升 2 分。

---

## 總分計算

| 項目 | 第 2 次 | 本次 | 變動原因 |
|------|---------|------|---------|
| 功能性 | 22 | **24** | +2：Excel 檔名 bug 修復 + GET scope 補上 |
| 程式品質與架構 | 22 | **24** | +2：Blob headers 錯誤用法修正 + scope 參數鏈完整 |
| 測試與驗證 | 22 | **24** | +2：新增 zero-division 邊界測試 |
| 使用體驗與安全 | 21 | **23** | +2：搜尋後統計摘要保留 + Excel 檔名正確 |
| **總分** | **87** | **95** | **+8， ✅ 達標（>90）** |

---

## 關鍵問題摘要

### ✅ 已修復（來自第 2 次評分）

| # | 原問題 | 原始評分 | 現狀 |
|---|--------|---------|------|
| 1 | Excel 下載檔名無法正確擷取 | Should-fix | ✅ 回傳 `{data, filename}`，從 response headers 解析 |
| 2 | MrsBase 搜尋後統計摘要消失 | Should-fix | ✅ 從搜尋結果自行計算 avg/max/min |
| 3 | GET endpoint 未傳 scope 參數 | Should-fix | ✅ 補上 `request.args.get("scope", "leaf")` |
| 4 | 缺少 zero-division 邊界測試 | 測試缺口 | ✅ 新增 `test_compare_zero_division` |

### 剩餘 Nits（已知但不影響本次評分）

| # | 項目 | 說明 |
|---|------|------|
| A | 大規模效能測試 | >1000 筆比較未測試（可於後續壓力測試階段補上） |
| B | `require_auth` 免登入模式 | 外網部署前應啟用強制驗證（已註記） |
| C | `calc_diff` 雙方為 0 時回傳 None | 設計意圖為 N/A，與前端 `fmtPct` 的 N/A 顯示一致，改為 0% 可討論但非錯誤 |

---

## 結論

**ship as-is** — 第 1 次返工準確修復了第 2 次評分指出的全部 4 項 should-fix 問題，無引入新迴歸。評分從 87 → **95 分**（目標 >90，已達標），程式碼經逐行閱讀確認無遺漏。可合併至主線。

---

*審查日期：2026-05-29 | 審查範圍：TASK-008 比較分析模組第 3 次評分（第 1 次返工後）*
