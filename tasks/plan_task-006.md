# TASK-006 開發計畫：工項單價庫完整版（MrsBase）

## 概述

將原有的「資源管理（Resource）」從**專案級私有**擴充為**跨專案公共單價庫（MrsBase）**。
MrsBase 是 PCCES 的核心資料庫，存放所有公共工項及其工料機組成單價分析，
可供各專案在編列預算時直接引用。

---

## 1. 資料庫模型設計

在 `api/models.py` 新增以下模型：

### 1.1 MrsBaseCategory — 公共單價分類（樹狀結構）

| 欄位 | 型別 | 說明 |
|------|------|------|
| id | Integer PK | |
| parent_id | FK → MrsBaseCategory.id (nullable) | 父分類（樹狀結構） |
| code | String(50) | 分類代碼 |
| c_name | String(300) | 分類名稱 |
| sort_order | Integer | 排序 |
| level_no | Integer | 層級（0=根） |
| created_at | DateTime | |
| updated_at | DateTime | |

**關聯**：`children = relationship("MrsBaseCategory", backref="parent", remote_side=[id])`

### 1.2 MrsBaseItem — 公共單價項目

| 欄位 | 型別 | 說明 |
|------|------|------|
| id | Integer PK | |
| category_id | FK → MrsBaseCategory.id | 所屬分類 |
| code | String(50) | PccesCode（唯一編碼） |
| pub_code | String(50) | 公共工程代碼 |
| c_name | String(500) | 中文名稱 |
| e_name | String(500) (nullable) | 英文名稱 |
| c_unit | String(50) | 中文單位 |
| e_unit | String(50) (nullable) | 英文單位 |
| unit_price | Float | 單價 |
| cost_kind | String(10) | 成本種類（1=工,2=料,3=機,4=雜） |
| item_type | String(10) | 項目類型（B/L/W…） |
| is_analysis | Boolean | 是否啟用單價分析 |
| labor_rate | Float | 人工比率 % |
| material_rate | Float | 材料比率 % |
| equipment_rate | Float | 設備比率 % |
| misc_rate | Float | 雜項比率 % |
| decimal_qty | Integer | 數量小數位數 |
| decimal_price | Integer | 單價小數位數 |
| decimal_amount | Integer | 金額小數位數 |
| memo | Text (nullable) | 備註 |
| is_approved | Boolean | 是否已審核 |
| approved_by | Integer (nullable) | 審核人員 User.id |
| approved_at | DateTime (nullable) | 審核時間 |
| created_by | Integer | 建立者 User.id |
| created_at | DateTime | |
| updated_at | DateTime | |

**索引**：`code` 唯一索引；`c_name` 索引（搜尋用）

### 1.3 MrsBaseBreakdownItem — 工料機組成（單價分析細項）

| 欄位 | 型別 | 說明 |
|------|------|------|
| id | Integer PK | |
| item_id | FK → MrsBaseItem.id (ondelete=CASCADE) | 所屬項目 |
| code | String(50) | 資源代碼 |
| c_name | String(300) | 中文名稱 |
| c_unit | String(50) | 單位 |
| quantity | Float | 數量 |
| unit_price | Float | 單價 |
| amount | Float | 金額 = qty × price |
| remark | Text (nullable) | 備註 |
| created_at | DateTime | |
| updated_at | DateTime | |

### 1.4 MrsBaseBookmark — 書籤

| 欄位 | 型別 | 說明 |
|------|------|------|
| id | Integer PK | |
| user_id | FK → User.id | 使用者 |
| item_id | FK → MrsBaseItem.id | 書籤的項目 |
| created_at | DateTime | |

**唯一約束**：(user_id, item_id)

### 1.5 BudgetItem 擴充（選用）

在既有 `BudgetItem` 新增選用欄位：

| 欄位 | 型別 | 說明 |
|------|------|------|
| mrs_base_item_id | FK → MrsBaseItem.id (nullable) | 引用 MrsBase 單價的來源 |

---

## 2. API 端點設計

所有端點前綴 `/api/mrs-base`，使用 Flask Blueprint。

### 2.1 分類（Category）API

| 方法 | 路徑 | 說明 |
|------|------|------|
| GET | `/api/mrs-base/categories` | 取得分類樹（巢狀 JSON） |
| GET | `/api/mrs-base/categories/flat` | 取得分類列表（平面） |
| POST | `/api/mrs-base/categories` | 建立分類 |
| PUT | `/api/mrs-base/categories/<id>` | 更新分類 |
| DELETE | `/api/mrs-base/categories/<id>` | 刪除分類（無項目時） |

### 2.2 項目（Item）API

| 方法 | 路徑 | 說明 |
|------|------|------|
| GET | `/api/mrs-base/items` | 列表（支援查詢參數：category_id, search, kind, page, per_page） |
| GET | `/api/mrs-base/items/<id>` | 單筆（含 breakdown_items） |
| POST | `/api/mrs-base/items` | 新增 |
| PUT | `/api/mrs-base/items/<id>` | 更新 |
| DELETE | `/api/mrs-base/items/<id>` | 刪除 |
| POST | `/api/mrs-base/items/<id>/approve` | 審核通過 |
| POST | `/api/mrs-base/items/<id>/unapprove` | 取消審核 |
| POST | `/api/mrs-base/items/batch-delete` | 批次刪除 |
| GET | `/api/mrs-base/items/export` | 匯出（CSV/Excel） |
| POST | `/api/mrs-base/items/import` | 匯入 |

### 2.3 工料機組成（Breakdown）API

| 方法 | 路徑 | 說明 |
|------|------|------|
| GET | `/api/mrs-base/items/<id>/breakdown` | 列表 |
| POST | `/api/mrs-base/items/<id>/breakdown` | 新增細項（自動更新 item.unit_price） |
| PUT | `/api/mrs-base/items/<id>/breakdown/<bid>` | 更新細項 |
| DELETE | `/api/mrs-base/items/<id>/breakdown/<bid>` | 刪除細項 |
| POST | `/api/mrs-base/items/<id>/breakdown/recalc` | 重新計算總金額 |

### 2.4 書籤（Bookmark）API

| 方法 | 路徑 | 說明 |
|------|------|------|
| GET | `/api/mrs-base/bookmarks` | 我的書籤 |
| POST | `/api/mrs-base/bookmarks` | 新增書籤 { item_id } |
| DELETE | `/api/mrs-base/bookmarks/<id>` | 移除書籤 |

### 2.5 引用（Link to Budget）API

| 方法 | 路徑 | 說明 |
|------|------|------|
| POST | `/api/mrs-base/items/<id>/link-to-budget` | 將此單價引用到指定專案的預算項 |
| GET | `/api/mrs-base/items/<id>/linked-projects` | 列出引用此單價的專案/預算項 |

### 2.6 搜尋 API

| 方法 | 路徑 | 說明 |
|------|------|------|
| GET | `/api/mrs-base/search?q=keyword&category=&kind=` | 模糊搜尋名稱/代碼 |

---

## 3. 前端頁面與元件樹

### 3.1 頁面路由

```
/app/mrs-base                    → MrsBasePage（主頁）
/app/projects/:id/budget         → BudgetEditorPage（擴充：可從 MrsBase 選取）
```

### 3.2 元件樹

```
MrsBasePage (pages/MrsBasePage.tsx)
├── Card
│   ├── Toolbar (新增/編輯/刪除/搜尋/匯出/篩選)
│   └── SplitPane (左右分割)
│       ├── Left: CategoryTree (antd Tree)
│       │   ├── MrsBaseCategoryTree (components/mrs-base/MrsBaseCategoryTree.tsx)
│       │   └── CategoryContextMenu (右鍵選單：新增子分類/刪除/重新命名)
│       └── Right: ItemTable (AG Grid)
│           ├── MrsBaseItemGrid (components/mrs-base/MrsBaseItemGrid.tsx)
│           ├── MrsBaseItemEditModal (components/mrs-base/MrsBaseItemEditModal.tsx)
│           ├── MrsBaseBreakdownPanel (components/mrs-base/MrsBaseBreakdownPanel.tsx)
│           └── MrsBaseSearchModal (components/mrs-base/MrsBaseSearchModal.tsx)

BudgetEditorPage 擴充
├── BudgetItemPicker (現有)
│   └── Tab: "從公共單價庫選取" (新)
│       └── MrsBasePickerPanel (components/mrs-base/MrsBasePickerPanel.tsx)
```

### 3.3 頁面功能說明

**MrsBasePage** 主頁：
- 左側：分類樹（antd Tree），點選分類後右側顯示該分類下的項目
- 右側：AG Grid 表格，欄位包含：編碼、名稱、單位、單價、成本種類、是否審核、建立時間
- 工具列：新增/編輯/刪除/搜尋/匯出/匯入/審核
- 雙擊項目開啟編輯視窗
- 點選「單價分析」切換至 Breakdown 面板

**MrsBaseItemEditModal**：
- 基本資料 Tab：編碼、名稱(中/英)、單位(中/英)、單價、成本種類、小數位數
- 分析設定 Tab：啟用單價分析、各類比率
- 備註 Tab：備註文字

**MrsBaseBreakdownPanel**：
- 顯示工料機組成細項表格（AG Grid）
- 新增/編輯/刪除細項
- 自動計算總金額
- 分頁顯示工、料、機、雜四類比率

**MrsBaseSearchModal**：
- 關鍵字搜尋（代碼/名稱）
- 分類篩選
- 成本種類篩選
- 結果列表（可多選）

**MrsBasePickerPanel**（在 BudgetEditorPage 中）：
- 嵌入在 BudgetItemPicker 中作為第二個 Tab
- 瀏覽/搜尋 MrsBase 項目
- 選取後自動填入預算項目的單價

---

## 4. 實作步驟與檔案清單

### 步驟 1：資料庫模型 — 新增 MrsBase 表格

**修改檔案**：
- `api/models.py` — 新增 MrsBaseCategory, MrsBaseItem, MrsBaseBreakdownItem, MrsBaseBookmark 類別
- `api/index.py` — 在 _migrate_schema() 中補上新增表格的遷移（SQLite ALTER TABLE 輔助）

**新增檔案**：無

**技術細節**：
- MrsBaseItem.code 設為 unique=True, index=True
- MrsBaseItem 的 cost_kind 使用字串而非 Enum（與既有模式一致）
- 使用 `passive_deletes=True` 處理 CASCADE 刪除
- BudgetItem 新增 mrs_base_item_id 選用 FK

**預計工時**：1 小時

---

### 步驟 2：後端 API — 分類 CRUD

**修改檔案**：
- `api/index.py` — 新增分類 API 端點（GET/POST/PUT/DELETE）
- `api/index.py` — 匯入新模型

**新增檔案**：無

**API 端點**：
```
GET    /api/mrs-base/categories       → 回傳樹狀結構（巢狀）
GET    /api/mrs-base/categories/flat  → 回傳平面列表
POST   /api/mrs-base/categories       → 建立 { parent_id, code, c_name }
PUT    /api/mrs-base/categories/<id>  → 更新
DELETE /api/mrs-base/categories/<id>  → 刪除（檢查無子分類及項目）
```

**技術細節**：
- 樹狀序列化：遞迴組裝 children 陣列
- 刪除前檢查：`db.query(MrsBaseItem).filter_by(category_id=id).count() == 0`

**預計工時**：1 小時

---

### 步驟 3：後端 API — 項目 CRUD + 搜尋

**修改檔案**：
- `api/index.py` — 新增項目 API 端點

**API 端點**：
```
GET    /api/mrs-base/items                 → 列表（可選 category_id, q, kind, page, per_page）
GET    /api/mrs-base/items/<id>            → 單筆（含 breakdown_items）
POST   /api/mrs-base/items                 → 新增
PUT    /api/mrs-base/items/<id>            → 更新
DELETE /api/mrs-base/items/<id>            → 刪除（含 CASCADE 子項）
POST   /api/mrs-base/items/batch-delete    → 批次刪除 { ids: [...] }
GET    /api/mrs-base/search                → 搜尋 ?q=xxx&category=xxx&kind=xxx
```

**技術細節**：
- 搜尋使用 `ilike` 或 `contains`（跨 SQLite/PostgreSQL 相容）
- 分頁：回傳 `{ items: [...], total: N, page: N, per_page: N }`
- 新增時自動產生排序序號

**預計工時**：2 小時

---

### 步驟 4：後端 API — 工料機組成（Breakdown）

**修改檔案**：
- `api/index.py` — 新增 breakdown CRUD

**API 端點**：
```
GET    /api/mrs-base/items/<id>/breakdown            → 列表
POST   /api/mrs-base/items/<id>/breakdown            → 新增
PUT    /api/mrs-base/items/<id>/breakdown/<bid>      → 更新
DELETE /api/mrs-base/items/<id>/breakdown/<bid>      → 刪除
POST   /api/mrs-base/items/<id>/breakdown/recalc     → 重新計算
```

**技術細節**：
- 新增/刪除/更新細項後自動加總 amount 並更新 item.unit_price（與既有 ResourceBreakdownItem 邏輯一致）
- recalc 端點強制重新計算所有細項加總

**預計工時**：1.5 小時

---

### 步驟 5：後端 API — 審核 + 書籤 + 引用

**修改檔案**：
- `api/index.py` — 新增審核、書籤、引用 API

**API 端點**：
```
審核：
  POST /api/mrs-base/items/<id>/approve       → 設定 is_approved=True, approved_by, approved_at
  POST /api/mrs-base/items/<id>/unapprove     → 取消審核

書籤：
  GET    /api/mrs-base/bookmarks              → 我的書籤列表
  POST   /api/mrs-base/bookmarks              → { item_id }
  DELETE /api/mrs-base/bookmarks/<id>         → 刪除

引用：
  POST /api/mrs-base/items/<id>/link-to-budget   → { project_id, budget_item_id? }
  GET  /api/mrs-base/items/<id>/linked-projects  → 引用此單價的專案/預算項列表
```

**技術細節**：
- 書籤唯一約束：(user_id, item_id)
- 引用時在 BudgetItem 設定 mrs_base_item_id 和 unit_price

**預計工時**：1.5 小時

---

### 步驟 6：前端型別 + API 客戶端

**修改檔案**：
- `web-pcces/frontend/src/types.ts` — 新增 MrsBase 相關型別
- `web-pcces/frontend/src/api.ts` — 新增 mrsBaseApi 物件

**技術細節**：
- 新增型別：MrsBaseCategory, MrsBaseItem, MrsBaseBreakdownItem, MrsBaseBookmark
- 新增 API 方法：對應步驟 2-5 的所有端點

**預計工時**：1 小時

---

### 步驟 7：前端元件 — 分類樹 + 主頁佈局

**新增檔案**：
- `web-pcces/frontend/src/components/mrs-base/MrsBaseCategoryTree.tsx`
- `web-pcces/frontend/src/components/mrs-base/MrsBaseItemGrid.tsx`

**修改檔案**：
- `web-pcces/frontend/src/pages/MrsBasePage.tsx`（新增）
- `web-pcces/frontend/src/App.tsx`（新增路由）
- `web-pcces/frontend/src/components/AppLayout.tsx`（新增側邊欄選單）

**MrsBaseCategoryTree 功能**：
- 使用 antd Tree 元件
- 可拖曳排序（onDrop）
- 右鍵選單：新增子分類、重新命名、刪除
- 點選分類觸發篩選右側表格

**MrsBaseItemGrid 功能**：
- AG Grid 表格
- 欄位：編碼、名稱、單位、單價、成本種類(標籤)、分析狀態、審核狀態、建立時間
- 支援排序、篩選
- 點選可選取（單選/多選）
- 雙擊開啟編輯

**MrsBasePage 功能**：
- 左右分割佈局
- 左側樹、右側表格
- 工具列：新增、編輯、刪除、搜尋、重新整理

**路由**：
- `/app/mrs-base` → MrsBasePage
- 側邊欄新增「公共單價庫」選單項目

**預計工時**：3 小時

---

### 步驟 8：前端元件 — 項目編輯 + 工料機組成面板

**新增檔案**：
- `web-pcces/frontend/src/components/mrs-base/MrsBaseItemEditModal.tsx`
- `web-pcces/frontend/src/components/mrs-base/MrsBaseBreakdownPanel.tsx`
- `web-pcces/frontend/src/components/mrs-base/MrsBaseSearchModal.tsx`

**MrsBaseItemEditModal 功能**：
- 使用 antd Modal + Form
- 基本資料 Tab：編碼、名稱(中/英)、單位(中/英)、單價、成本種類、類型
- 分析設定 Tab：是否啟用分析、各類比率
- 細項管理 Tab：嵌入 MrsBaseBreakdownPanel
- 備註 Tab
- 表單驗證：編碼必填且唯一、名稱必填

**MrsBaseBreakdownPanel 功能**：
- AG Grid 表格顯示工料機細項
- 新增/編輯/刪除按鈕
- 自動計算加總金額
- 顯示工、料、機、雜四項佔比圓餅圖（使用 ECharts）

**MrsBaseSearchModal 功能**：
- 關鍵字輸入框（即時搜尋）
- 分類下拉篩選
- 成本種類篩選
- 結果表格（可多選）
- 選取後回呼

**預計工時**：3 小時

---

### 步驟 9：預算編輯器擴充 — 從 MrsBase 選取單價

**修改檔案**：
- `web-pcces/frontend/src/components/BudgetItemPicker.tsx` — 新增 Tab 頁
- `web-pcces/frontend/src/components/mrs-base/MrsBasePickerPanel.tsx`（新增）

**MrsBasePickerPanel 功能**：
- 嵌入在 BudgetItemPicker 中作為第二個分頁
- 樹狀分類 + 表格（簡化版 MrsBasePage）
- 選取項目後自動填入預算編輯器的單價與名稱
- 可選擇「建立連結」（設定 mrs_base_item_id）或「僅複製單價」

**預計工時**：2 小時

---

### 步驟 10：起始示範資料 — MrsBase 種子

**修改檔案**：
- `api/seed_data.py` — 新增 MrsBase 示範資料

**示範資料內容**：
- 3~5 個分類（如：混凝土工程、鋼筋工程、模板工程、裝修工程、機電工程）
- 每個分類 3~5 個項目
- 至少 2 個項目啟用單價分析並建立完整工料機組成
- 為 demo 使用者建立幾個書籤

**技術細節**：
- 與專案種子資料分開檢測（檢查 MrsBaseItem 是否為空）
- 使用 `db.query(MrsBaseItem).count() > 0` 判斷

**預計工時**：1 小時

---

### 步驟 11：測試

**新增檔案**：
- `tests/test_mrs_base_api.py` — API 測試（使用 pytest + SQLite in-memory）
- `tests/test_mrs_base_models.py` — 模型測試

**測試範圍**：
1. 分類 CRUD 測試
2. 項目 CRUD + 唯一編碼檢查
3. 工料機組成 CRUD + 自動加總
4. 搜尋功能測試
5. 審核流程測試
6. 書籤 CRUD 測試
7. 引用 MrsBase 到預算測試
8. 權限測試（需登入才能操作）

**預計工時**：2 小時

---

## 5. 測試計畫

### 5.1 測試策略

| 層級 | 工具 | 範圍 |
|------|------|------|
| 單元測試 | pytest | Model 驗證、計算邏輯 |
| API 測試 | pytest + Flask test client | 所有端點 CRUD + 權限 |
| 前端測試 | 手動 + React DevTools | UI 操作流程 |
| 整合測試 | 手動 | 從 MrsBase 引用到預算的完整流程 |

### 5.2 關鍵測試案例

**後端 API 測試**：
1. ✅ 建立分類 → 回傳 201 + 正確的樹狀結構
2. ✅ 建立重複編碼的項目 → 回傳 409
3. ✅ 新增工料機細項後自動更新項目單價
4. ✅ 刪除分類時若存在子項目 → 回傳 400
5. ✅ 搜尋關鍵字回傳正確結果
6. ✅ 審核後項目不可編輯（可選）
7. ✅ 未登入存取 → 回傳 401

**前端功能測試**：
1. ✅ 分類樹展開/收合正常
2. ✅ 點選分類 → 表格篩選正確
3. ✅ 新增/編輯項目後列表更新
4. ✅ 工料機組成新增細項後單價自動更新
5. ✅ 從 MrsBase 選取單價到預算編輯器
6. ✅ 書籤新增/移除正常

**整合測試**：
1. ✅ 建立 MrsBase 項目 → 在預算編輯器引用 → 預算單價自動帶入
2. ✅ 修改 MrsBase 單價 → 已引用的預算項目提示更新（選用）
3. ✅ 匯出 MrsBase 資料 → 匯入到另一個環境

### 5.3 測試資料準備

使用 `seed_data.py` 中的示範 MrsBase 資料作為測試基底。

---

## 6. 預計工時

| 步驟 | 任務 | 工時 |
|------|------|------|
| 1 | 資料庫模型設計 | 1.0h |
| 2 | 分類 API | 1.0h |
| 3 | 項目 API + 搜尋 | 2.0h |
| 4 | 工料機組成 API | 1.5h |
| 5 | 審核/書籤/引用 API | 1.5h |
| 6 | 前端型別 + API 客戶端 | 1.0h |
| 7 | 分類樹 + 主頁 | 3.0h |
| 8 | 編輯/工料機/搜尋元件 | 3.0h |
| 9 | 預算編輯器擴充 | 2.0h |
| 10 | 起始示範資料 | 1.0h |
| 11 | 測試 | 2.0h |
| **合計** | | **19.0h** |

---

## 7. 實作注意事項

### 7.1 Vercel 部署相容性
- 使用 SQLite 時，MrsBase 資料庫位於同一個檔案（無需額外配置）
- Blueprint 註冊在 `create_app()` 中
- 跨專案資料共享：MrsBase 資料為全域性（不屬於特定 project_id）

### 7.2 與現有 Resource 的關係
- MrsBase 是「公共單價庫」，**獨立於專案**
- 現有 Resource 是「專案級私有資源」，維持不變
- 預算項目可同時引用 MrsBase 單價（設定 `mrs_base_item_id`）或使用自訂 Resource

### 7.3 子代理分工建議
- **DEVELOPER-1**（步驟 1-5）：後端全部
- **DEVELOPER-2**（步驟 6-9）：前端全部
- **DEVELOPER-3**（步驟 10-11）：種子資料 + 測試

### 7.4 命名約定
- 前端目錄：`components/mrs-base/`
- 後端 Blueprint：`mrs_base_bp`
- API 路徑前綴：`/api/mrs-base`
- 資料表前綴：`mrs_base_`

### 7.5 檔案修改摘要

| 檔案 | 動作 | 說明 |
|------|------|------|
| `api/models.py` | 修改 | 新增 4 個模型 + BudgetItem 擴充 |
| `api/index.py` | 修改 | 新增所有 API 端點 (~500 行) |
| `api/seed_data.py` | 修改 | 新增 MrsBase 種子資料 |
| `web-pcces/frontend/src/types.ts` | 修改 | 新增 MrsBase 型別 |
| `web-pcces/frontend/src/api.ts` | 修改 | 新增 mrsBaseApi |
| `web-pcces/frontend/src/App.tsx` | 修改 | 新增路由 |
| `web-pcces/frontend/src/components/AppLayout.tsx` | 修改 | 新增側邊欄選單 |
| `web-pcces/frontend/src/pages/MrsBasePage.tsx` | **新增** | 主頁 |
| `web-pcces/frontend/src/components/mrs-base/MrsBaseCategoryTree.tsx` | **新增** | 分類樹 |
| `web-pcces/frontend/src/components/mrs-base/MrsBaseItemGrid.tsx` | **新增** | 項目表格 |
| `web-pcces/frontend/src/components/mrs-base/MrsBaseItemEditModal.tsx` | **新增** | 編輯視窗 |
| `web-pcces/frontend/src/components/mrs-base/MrsBaseBreakdownPanel.tsx` | **新增** | 工料機面板 |
| `web-pcces/frontend/src/components/mrs-base/MrsBaseSearchModal.tsx` | **新增** | 搜尋視窗 |
| `web-pcces/frontend/src/components/mrs-base/MrsBasePickerPanel.tsx` | **新增** | 選取面板 |
| `web-pcces/frontend/src/components/BudgetItemPicker.tsx` | 修改 | 新增 Tab |
| `tests/test_mrs_base_api.py` | **新增** | API 測試 |
| `tests/test_mrs_base_models.py` | **新增** | 模型測試 |
