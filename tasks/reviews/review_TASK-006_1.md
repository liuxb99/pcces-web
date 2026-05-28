# TASK-006 評分總表

## 審查範圍

- 後端：`api/models.py`、`api/index.py` — 4 個新模型 + MrsBase API 路由
- 前端：`web-pcces/frontend/src/` — 型別、API、5 個頁面/元件
- 驗證：npm run build ✅、Flask load ✅

---

## 1. 評分檢查清單

| 項目 | 結果 | 備註 |
|------|------|------|
| 4 個新模型實作 | YES | MrsBaseCategory, MrsBaseItem, MrsBaseBreakdownItem, MrsBaseBookmark |
| BudgetItem 擴充 mrs_base_item_id FK | YES | `api/models.py:145` |
| 分類樹狀 API (GET/POST/PUT/DELETE) | YES | 5 端點，含樹狀序列化 |
| 項目分頁搜尋 API (含篩選) | YES | GET `/mrs-base/items` 支援 category_id, q, kind, approved, page, per_page |
| 工料機組成 CRUD + 自動加總 | YES | 5 端點，新增/更新/刪除後自動 `_recalc_mrs_base_item` |
| 審核/取消審核 API | YES | POST approve + unapprove |
| 書籤 API (GET/POST/DELETE) | YES | 含重複加入檢查 |
| 引用到預算 API | YES | `link-to-budget` + `linked-projects` |
| 前端完整型別定義 | YES | `types.ts` MrsBase 相關介面 |
| 前端 API 客戶端 | YES | `api.ts` mrsBaseApi 完整對應 |
| MrsBasePage 主頁 | YES | 左右分割佈局 |
| MrsBaseCategoryTree 分類樹 | YES | antd Tree + 增刪功能 |
| MrsBaseItemGrid 項目表格 | YES | AG Grid + 多選/排序 |
| MrsBaseItemEditModal 編輯視窗 | YES | 分頁 Tab (基本/分析/細項/備註) |
| MrsBaseBreakdownPanel 工料機面板 | YES | 細項表格 + 類別佔比 |
| 路由 /app/mrs-base | YES | `App.tsx` 註冊 |
| 側邊欄「公共單價庫」 | YES | `AppLayout.tsx` |
| **測試檔案** | **NO** | 無任何 MrsBase 測試 |
| **種子資料** | **NO** | seed_data.py 未建立 MrsBase 示範資料 |
| **BudgetItemPicker MrsBase Tab** | **NO** | 未擴充 |
| **MrsBasePickerPanel / SearchModal** | **NO** | 計畫中提及但未實作 |

---

## 2. 四項細項評分

### 2.1 功能完整性 (23 / 25)

後端模型與 API 完備：4 模型 ✅、24 條路由 ✅、審核/書籤/引用 ✅。
**扣分原因**：
- 預算編輯器未整合 MrsBase 選取面板（`BudgetItemPicker.tsx` 未修改）
- `MrsBaseSearchModal`、`MrsBasePickerPanel` 未實作（前端功能遺漏約 10%）

### 2.2 程式碼品質 (22 / 25)

後端風格與現有程式碼一致，路由命名清晰，錯誤處理到位。
**扣分原因**：
- `_build_category_tree` 產生 N+1 查詢（每個分類多一次 `COUNT` query），深層樹可能慢
- `model_to_dict` 不包含 relationship（`breakdown_items` 須手動附加），但 `list_mrs_base_items` 未附帶，前端若需要得再呼叫 `getItem`
- `batch_delete` 未檢查項目是否已被預算引用（orphan FK 問題）
- 分類刪除 API 未檢查是否有子項目 (`MrsBaseItem`)，可能噴 500 FK 錯誤
- 部份字串 literal（如 `cost_kind` 對應值）前後端無共用常數（hard-code mapping）

### 2.3 前端實作 (22 / 25)

UI 佈局恰當，AG Grid + antd 整合良好，編輯 modal 分頁清楚。
**扣分原因**：
- `BudgetItemPicker` MrsBase Tab 未實作（計畫中步驟 9 未做）
- `MrsBaseSearchModal` 未實作（以 inline search box 替代，功能堪用但缺乏進階篩選 UI）
- Breakdown panel 未實作 ECharts 圓餅圖（計畫中提及，僅以文字顯示佔比）
- `decimal_qty`/`decimal_price`/`decimal_amount` 設定雖在表單中但 frontend render 未實際套用

### 2.4 測試與資料 (10 / 25)

**嚴重缺失**：
- 無任何 MrsBase 測試檔案（計畫中 `tests/test_mrs_base_api.py`、`tests/test_mrs_base_models.py` 均不存在）
- `api/seed_data.py` 雖 import MrsBase 模型但**完全沒有建立 MrsBase 示範資料**
- 若部署全新環境，MrsBase 頁面僅為空結構，無法直接測試

---

## 3. 總分

| 項目 | 評分 |
|------|:----:|
| 功能完整性 (25) | 23 |
| 程式碼品質 (25) | 22 |
| 前端實作 (25) | 22 |
| 測試與資料 (25) | 10 |
| **總分 (100)** | **77** |

> 總分 **77/100**，低於 90。需補上測試與種子資料。

---

## 4. 具體缺失說明

### 🔴 Blocking — 必須補上

1. **缺失測試** — `tests/` 目錄下無任何 MrsBase 相關測試。請建立：
   - `tests/test_mrs_base_api.py`：涵蓋分類 CRUD、項目 CRUD +唯一性、工料機自動加總、搜尋、審核流程、書籤 CRUD、引用到預算 API
   - 至少 20 個測試案例

2. **缺失種子資料** — `api/seed_data.py` 需在 `seed_demo_data()` 中新增：
   - 3~5 個分類（如：混凝土、鋼筋、模板、裝修）
   - 每分類 3~5 個項目
   - 至少 2 個啟用單價分析且有完整工料機組成
   - 為 demo 使用者建立書籤

### 🟡 Should-fix — 建議修改

3. **`BudgetItemPicker` 未整合 MrsBase Tab** — 無法在預算編輯器中直接選取公共單價。見 `plan_task-006.md` 步驟 9。

4. **分類刪除無防護** — `api/index.py:3431` `delete_mrs_base_category` 需在刪除前檢查：
   - 有無子分類 (`children`)
   - 有無隸屬項目 (`MrsBaseItem`)
   - 存在任一項則回傳 400

5. **`_build_category_tree` N+1 查詢** — `item_count` 每個節點獨立 COUNT，可改用 subquery join 一次查完。

6. **`batch_delete` 未檢查引用** — `api/index.py:3623` 刪除前應掃 `BudgetItem.mrs_base_item_id`，若被引用應阻止或提示。

### 🔵 Nits — 選修

7. **檢視 `decimal_qty`/`decimal_price`/`decimal_amount` 在 Grid/Modal 中實際生效** — 目前僅儲存但 render 時未使用 `toFixed(decimal_qty)` 等。

8. **`list_mrs_base_items` 未附帶 `breakdown_items`** — 雖然合理（效能考量），但前端若需要在列表顯示是否啟用分析時的細項數量摘要，可加 `breakdown_count` 欄位節省一次 API 呼叫。

