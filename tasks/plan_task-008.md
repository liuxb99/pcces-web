# TASK-008 開發計畫：比較分析 + 報表增強

## 概述

將原始 WinForms 的比較分析功能（工項比較、單價比較）移植到網頁版，並強化現有報表功能。

### 原始 WinForms 參考
- `FormCompareItm.cs` — 工項比較（跨專案/跨版本預算項目差異）
- `FormCompareItm_Scope.cs` — 比較範圍選取
- `FormCompareMrs.cs` — 單價比較（MrsBase 工項單價比較）
- `FormCompareMrsAna.cs` — 單價分析比較（工料機組成比較）

### 現有可重複使用的基礎
- 預算樹狀 API (`/api/projects/:id/budget/tree`) ✅
- MrsBase 項目 API (`/mrs-base/items`) ✅
- Excel 匯出（openpyxl）已有模板 ✅
- AG Grid 尚未引入，需確認是否用 Ant Design Table 代替

---

## 實作步驟

### Step-1: 後端 — 工項比較 API

**目標**：建立比較兩個專案（或同一專案兩個版本）預算項目的 diff 端點。

**新增/修改檔案**：
| 檔案 | 操作 | 說明 |
|---|---|---|
| `api/index.py` | 修改 | 新增 `/api/compare/budget-items` 端點 |
| `api/models.py` | 不變（無需新模型） | 使用既有 BudgetItem, Project |

**API 規格**：

```
POST /api/compare/budget-items
Authorization: Bearer <token>
Content-Type: application/json

{
  "project_a_id": 1,        // 來源專案 A
  "project_b_id": 2,        // 對比專案 B
  "scope": "leaf" | "all"   // "leaf" 只比 W/L 類型; "all" 比所有
}
```

**回應格式**：
```json
{
  "project_a": { "id": 1, "name": "OO大樓新建工程" },
  "project_b": { "id": 2, "name": "XX大樓新建工程" },
  "items": [
    {
      "key": "0101",                    // 配對 key（依 print_no 或 item_no 配對）
      "c_name": "鋼筋工程",
      "c_unit": "噸",
      "a": { "quantity": 850, "unit_price": 28500, "amount": 24225000 },
      "b": { "quantity": 800, "unit_price": 29000, "amount": 23200000 },
      "diff": {                          // 差異值
        "quantity": -50,
        "unit_price": 500,
        "amount": -1025000
      },
      "diff_pct": {                      // 差異百分比
        "quantity": -5.88,
        "unit_price": 1.75,
        "amount": -4.23
      },
      "status": "modified"               // "added" / "removed" / "modified" / "unchanged"
    }
  ],
  "summary": {
    "total_a": 50000000,
    "total_b": 48000000,
    "diff": -2000000,
    "diff_pct": -4.0,
    "added_count": 2,
    "removed_count": 1,
    "modified_count": 5,
    "unchanged_count": 10
  }
}
```

**Diff 邏輯**：
1. 以 `print_no` 為配對 key（若無 print_no，則以 `c_name` + `c_unit` 模糊配對）
2. 數值差異 = B - A
3. 百分比 = diff / A × 100（若 A 為 0 則標記為 "N/A"）
4. 標色規則：差異 > 5% → 紅色（增加）/ 綠色（減少）；小於等於 5% → 淺色

**技術細節**：
- 兩份 budget_items 先各自 flat list（展平樹狀）
- 建立 dict key → item 的對照，逐項比對
- 對 A 有 B 沒有的 → status="removed"
- 對 B 有 A 沒有的 → status="added"
- 二者都有的 → 比較數值決定 "modified" 或 "unchanged"
- 支援查詢參數 `?project_a_id=X&project_b_id=Y` 的 GET 方式（簡易版）

**預計工時**：2 小時

---

### Step-2: 後端 — MrsBase 單價比較 API

**目標**：比較 MrsBase 項目的單價變化（同一項目不同時間點的價格差異，或兩組項目對比）。

**新增/修改檔案**：
| 檔案 | 操作 | 說明 |
|---|---|---|
| `api/index.py` | 修改 | 新增 `/api/compare/mrs-base-prices` 端點 |

**API 規格**：

```
POST /api/compare/mrs-base-prices
Authorization: Bearer <token>

{
  "category_id": 5,                        // 選擇的分類（可選）
  "item_ids": [1, 2, 3, ...],              // 或直接指定項目 ID 列表
  "compare_type": "all"                     // "all": 比所有, "changed_only": 只顯示有變動的
}
```

**回應格式**：
```json
{
  "items": [
    {
      "id": 1,
      "code": "CONC-001",
      "c_name": "210kgf/cm² 預拌混凝土",
      "c_unit": "m³",
      "cost_kind": "料",
      "unit_price": 1850,
      "has_analysis": false,
      "breakdown_items": []  // 若 has_analysis=true，附上工料機組成
    }
  ],
  "summary": {
    "total": 16,
    "avg_price": 2450,
    "max_price": 28500,
    "min_price": 85
  }
}
```

**技術細節**：
- 此 API 主要為前端表格提供資料，單價比較的「差異」由前端比對使用者選取的兩批項目
- 也可支援查詢同一項目在不同版本的價格（需在 MrsBaseItem 加上 version 或 created_at 追蹤）
- 但 MVP 階段先實作「瀏覽所有項目的單價一覽表」，由前端做選取比對
- 內含 `has_analysis` 旗標，前端可以展開查看工料機組成

**預計工時**：1.5 小時

---

### Step-3: 前端 — ComparePage 工項比較頁面

**目標**：建立工項比較頁面，讓使用者選取兩個專案進行比對，以 AG Grid 或 Ant Design Table 顯示差異並標色。

**新增/修改檔案**：
| 檔案 | 操作 | 說明 |
|---|---|---|
| `web-pcces/frontend/src/pages/ComparePage.tsx` | **新增** | 工項比較主頁面 |
| `web-pcces/frontend/src/api.ts` | 修改 | 新增 `compareApi` 區塊 |
| `web-pcces/frontend/src/types.ts` | 修改 | 新增比較相關型別定義 |

**畫面設計**：

```
┌─────────────────────────────────────────────────────────┐
│  比較分析 > 工項比較                                      │
│  專案 A: [下拉選單 ▼]    專案 B: [下拉選單 ▼]    [開始比較] │
├─────────────────────────────────────────────────────────┤
│ 摘要卡片：A總額 | B總額 | 差異 | 新增N | 移除N | 修改N   │
├─────────────────────────────────────────────────────────┤
│ ┌──────┬──────┬──────────┬──────────┬──────────┬──────┐ │
│ │ 項次 │ 名稱 │ A數量     │ B數量     │ 差異      │ 狀態 │ │
│ ├──────┼──────┼──────────┼──────────┼──────────┼──────┤ │
│ │ 001  │ 鋼筋 │ 850.00   │ 800.00   │ -50(-5.9%)│ 修改 │ │
│ │      │ 工程 │ (紅底)    │ (綠底)    │ (紅字)    │      │ │
│ └──────┴──────┴──────────┴──────────┴──────────┴──────┘ │
│ [匯出比較報表 Excel]                                      │
└─────────────────────────────────────────────────────────┘
```

**標色規則**（使用 Ant Design Table `rowClassName` + `cell render`）：
- `status = "added"` → 整行淺綠色背景
- `status = "removed"` → 整行淺紅色背景
- `status = "modified"` → 差異欄位：
  - 數量差異 > ±5% → 橘色字體 + 淺黃背景
  - 單價差異 > ±5% → 橘色字體 + 淺黃背景
  - 金額差異 > ±5% → 橘色字體 + 淺黃背景
- `status = "unchanged"` → 正常（可折疊隱藏）

**技術細節**：
- 使用 Ant Design `<Table>` 搭配 `expandedRowRender` 展開顯示子項差異
- 專案下拉選單透過 `projectApi.list()` 取得
- 右上方放置「匯出比較報表」按鈕
- 採用 `useCallback` + `useMemo` 避免不必要的重新渲染
- 加載時顯示 `<Spin>`

**預計工時**：4 小時

---

### Step-4: 前端 — 路由 + 選單整合

**目標**：將比較頁面加入導航選單與路由。

**新增/修改檔案**：
| 檔案 | 操作 | 說明 |
|---|---|---|
| `web-pcces/frontend/src/App.tsx` | 修改 | 新增比較頁面路由 |
| `web-pcces/frontend/src/components/AppLayout.tsx` | 修改 | 新增「比較分析」選單項 |

**路由配置**：
```
/app/compare                   → 比較分析首頁（選擇比較類型）
/app/compare/budget-items      → 工項比較（選擇雙專案）
/app/compare/mrs-prices        → 單價比較（MrsBase 價格一覽）
```

**選單設計**：
- 在「公共單價庫」下方新增「比較分析」群組
```
比較分析
  ├─ 工項比較        → /app/compare/budget-items
  └─ 單價比較        → /app/compare/mrs-prices
```
- 選單圖示：`<BarChartOutlined />` 或 `<SwapOutlined />`

**技術細節**：
- 所有比較頁面置於 `/app/compare/*` 下
- 無需 projectId 參數（工項比較頁面內自選雙專案）
- 選單展開時高亮當前子項

**預計工時**：1 小時

---

### Step-5: 後端 — 比較報表 Excel 匯出

**目標**：將工項比較結果匯出為 Excel 檔案。

**新增/修改檔案**：
| 檔案 | 操作 | 說明 |
|---|---|---|
| `api/index.py` | 修改 | 新增 `/api/compare/budget-items/export/excel` 端點 |

**API 規格**：

```
POST /api/compare/budget-items/export/excel
Authorization: Bearer <token>
Content-Type: application/json

Body: 同 Step-1 的比較請求參數
```

**回應**：Excel 檔案（`application/vnd.openxmlformats-officedocument.spreadsheetml.sheet`）

**Excel 格式**：
- 標題列：比較報表 — OO大樓 vs XX大樓
- 表頭：項次、項目名稱、單位、A數量、A單價、A金額、B數量、B單價、B金額、數量差異、單價差異、金額差異、狀態
- 資料列：與 API 回應相同的 diff 結果
- 合計列：A 總金額、B 總金額、差異總金額
- 標色：比照網頁版標色規則（使用 openpyxl PatternFill）

**技術細節**：
- 複用 Step-1 的 diff 邏輯（抽取為共用函式 `_compare_budget_items`）
- 使用 openpyxl（系統已有此依賴）
- 報表暫存於 `REPORT_DIR`
- 檔案命名：`PCCES_比較報表_{project_a_code}_vs_{project_b_code}.xlsx`

**預計工時**：2 小時

---

### Step-6: 前端 — 比較報表 Excel 下載功能

**目標**：在 ComparePage 加入 Excel 下載按鈕，呼叫後端匯出端點。

**新增/修改檔案**：
| 檔案 | 操作 | 說明 |
|---|---|---|
| `web-pcces/frontend/src/pages/ComparePage.tsx` | 修改 | 加入「匯出比較報表」按鈕及下載邏輯 |
| `web-pcces/frontend/src/api.ts` | 修改 | 新增 `compareApi.exportExcel()` |

**實作細節**：
- 下載方式：使用 `fetch` + `Blob`（如同現有 ReportsPage 的 `handleDownloadExcel`）
- 需帶入 `Authorization` header
- 下載前顯示 loading 狀態
- 下載成功顯示 `message.success()`
- 失敗顯示 `message.error()` + 詳細錯誤訊息

**預計工時**：1 小時

---

### Step-7: 前端 — MrsBase 單價比較頁面

**目標**：建立 MrsBase 單價比較頁面，顯示所有公共單價項目，可依分類篩選、搜尋，並展開查看工料機組成。

**新增/修改檔案**：
| 檔案 | 操作 | 說明 |
|---|---|---|
| `web-pcces/frontend/src/pages/MrsBasePriceComparePage.tsx` | **新增** | 單價比較頁面 |
| `web-pcces/frontend/src/api.ts` | 修改 | 在 `compareApi` 加入 price list 方法（或復用 `mrsBaseApi.listItems`） |
| `web-pcces/frontend/src/types.ts` | 修改 | 新增單價比較相關型別 |

**畫面設計**：

```
┌──────────────────────────────────────────────────────────────┐
│  比較分析 > 單價比較  分類：[下拉選單 ▼]  搜尋：[🔍 _____]   │
│  顯示設定：□ 僅顯示有單價分析的項目  □ 僅顯示有變動的項目      │
├──────────────────────────────────────────────────────────────┤
│ 統計：共 16 項 | 平均單價 $2,450 | 最高 $28,500 | 最低 $85  │
├──────────────────────────────────────────────────────────────┤
│ ┌──────┬────────────┬──────┬────────┬────────┬───────────┐ │
│ │ 編碼 │ 名稱        │ 單位 │ 單價    │ 成本類 │ 單價分析   │ │
│ ├──────┼────────────┼──────┼────────┼────────┼───────────┤ │
│ │C001  │混凝土 210   │ m³   │ $1,850 │ 料     │ 🔍檢視組成 │ │
│ │      │            │      │        │        │ (展開)     │ │
│ └──────┴────────────┴──────┴────────┴────────┴───────────┘ │
│                                                              │
│  展開的明細（可折疊區域）：                                   │
│  ┌──────────┬──────────┬──────┬──────┬────────┐            │
│  │ 細項名稱  │ 單位      │ 數量  │ 單價  │ 金額    │            │
│  ├──────────┼──────────┼──────┼──────┼────────┤            │
│  │ 鋼筋工    │ 工        │ 0.012│ 3,800│ 45.60  │            │
│  │ SD280鋼筋│ 噸        │ 1.0  │24,500│24,500  │            │
│  │ 吊車      │ 天        │ 0.003│18,000│ 54.00  │            │
│  └──────────┴──────────┴──────┴──────┴────────┘            │
└──────────────────────────────────────────────────────────────┘
```

**技術細節**：
- 左側分類樹：復用 `MrsBaseCategoryTree` 元件
- 主要表格：使用 Ant Design Table，加入 `expandedRowRender` 顯示工料機組成
- 展開細項時呼叫 `mrsBaseApi.getBreakdownItems(itemId)`
- 分類篩選：透過 `mrsBaseApi.listItems({ category_id })`
- 搜尋：使用 `mrsBaseApi.search({ q })`
- 統計摘要：前端計算 avg/max/min

**預計工時**：3 小時

---

### Step-8: 測試 — 後端 API 測試

**目標**：為比較 API 撰寫自動化測試。

**新增/修改檔案**：
| 檔案 | 操作 | 說明 |
|---|---|---|
| `api/test_compare.py` | **新增** | 比較 API 測試案例 |
| `api/index.py` | 修改 | 若有 bug 則修正 |

**測試案例**：

```python
# test_compare.py

class TestCompareBudgetItems:
    """工項比較 API 測試"""

    def test_compare_same_project(self, seeded_db, client):
        """同一專案比對應全為 unchanged"""
        ...

    def test_compare_diff_projects(self, seeded_db, client):
        """不同專案比對正確產出 diff"""
        ...

    def test_compare_no_access(self, db_session, client):
        """無權限使用者無法比較"""
        ...

    def test_compare_export_excel(self, seeded_db, client):
        """比較報表 Excel 匯出正常"""
        ...


class TestCompareMrsBasePrices:
    """MrsBase 單價比較 API 測試"""

    def test_list_all_prices(self, seeded_db, client):
        """列出所有 MrsBase 項目單價"""
        ...

    def test_list_by_category(self, seeded_db, client):
        """依分類篩選 MrsBase 項目"""
        ...
```

**預計工時**：2 小時

---

### Step-9: 測試 — 前端元件測試

**目標**：為比較頁面元件撰寫基礎測試。

**新增/修改檔案**：
| 檔案 | 操作 | 說明 |
|---|---|---|
| `web-pcces/frontend/src/__tests__/ComparePage.test.tsx` | **新增** | ComparePage 元件測試 |
| `web-pcces/frontend/src/__tests__/MrsBasePriceComparePage.test.tsx` | **新增** | MrsBase 單價比較頁面測試 |

**測試案例**：
1. ComparePage 正確渲染選擇雙專案的介面
2. 模擬 API 回傳 mock 資料後正確顯示 diff 表格
3. 差異標色邏輯正確
4. Excel 下載按鈕觸發正確的 API 呼叫
5. MrsBasePriceComparePage 正確顯示分類樹與項目列表

**預計工時**：2 小時

---

## 總工時預估

| 步驟 | 內容 | 工時 |
|---|---|---|
| Step-1 | 後端工項比較 API | 2.0h |
| Step-2 | 後端 MrsBase 單價比較 API | 1.5h |
| Step-3 | 前端 ComparePage 工項比較頁面 | 4.0h |
| Step-4 | 前端路由 + 選單 | 1.0h |
| Step-5 | 後端比較報表 Excel 匯出 | 2.0h |
| Step-6 | 前端 Excel 下載功能 | 1.0h |
| Step-7 | 前端 MrsBase 單價比較頁面 | 3.0h |
| Step-8 | 後端 API 測試 | 2.0h |
| Step-9 | 前端元件測試 | 2.0h |
| **合計** | | **18.5h** |

---

## 依賴關係

```
Step-1 ──→ Step-3 ──→ Step-6
  │                      ↑
  └──→ Step-5 ──────────┘
  │
Step-2 ──→ Step-7

Step-4 (可在 Step-3/7 完成後再整合)

Step-8 (依賴 Step-1, Step-2, Step-5)
Step-9 (依賴 Step-3, Step-7)
```

建議開發順序：
1. Step-1 + Step-2（後端 API — 可平行進行）
2. Step-3（前端工項比較 — 需 Step-1 完成）
3. Step-5 + Step-6（報表匯出 — 需 Step-1 完成，可與 Step-3 平行）
4. Step-7（前端單價比較 — 需 Step-2 完成）
5. Step-4（路由選單 — Step-3/7 完成後整合）
6. Step-8 + Step-9（測試）

---

## 注意事項

1. **Vercel 部署**：比較 API 端點也需加入 `_ensure_db()` 邏輯，確保自動建立示範資料
2. **Diff 演算法**：大量資料（>500 項）時建議在後端做 diff，前端只負責渲染
3. **標色標準**：差異 ±5% 為閾值（可調整），參考原始 WinForms 的 CompareForm 邏輯
4. **Excel 匯出**：沿用現有 `REPORT_DIR` 暫存機制，注意 Vercel 無塵檔案系統（僅 `/tmp` 可寫）
5. **類型安全**：前端 TypeScript 型別定義需與後端 API 回應一致
6. **AG Grid vs Ant Table**：若時間充裕可引入 AG Grid（社群版免費），其內建差色 + 排序 + 過濾更適合比較表。但 MVP 使用 Ant Design Table 已足夠
