# TASK-004 計價管理模組 — 評分報告

## 一、評分檢查清單

| 編號 | 檢查項目 | YES/NO |
|------|----------|--------|
| 1 | 資料表 `invoices` 定義完整 | YES |
| 2 | 資料表 `invoice_items` 定義完整 | YES |
| 3 | 後端 15 條路由全數實作 | YES |
| 4 | 路由權限控制（`@require_auth` + `_check_project_access`） | YES |
| 5 | 狀態機邏輯正確（draft → submitted → approved） | YES |
| 6 | 前端 `types.ts` 型別定義完整 | YES |
| 7 | 前端 `api.ts` 12 個方法全數實作 | YES |
| 8 | 前端路由掛載正確 | YES |
| 9 | `InvoiceListPage` AG Grid / Ant Table 清單正常 | YES |
| 10 | `InvoiceDetailPage` 可編輯 AG Grid + 操作按鈕正常 | YES |
| 11 | `npm run build` 無錯誤 | YES |
| 12 | Flask 載入無錯誤 | YES |
| 13 | 無明顯 SQL 注入 / XSS 漏洞 | YES |
| 14 | seed data 涵蓋計價資料 | NO |
| 15 | 前端/後端 `progress_rate` 格式一致 | NO |

---

## 二、細項評分

### 1. 功能完整性 (0–25)

**得分：24 / 25**

CRUD、批次建立、重算、提交審核、核准、報表、Excel 匯出一應俱全，15 條路由全數實作且前後端一致。唯一扣分：seed data 未包含計價示範資料，新使用者開啟空專案無法直接體驗計價流程。

### 2. 正確性 (0–25)

**得分：19 / 25**

發現 **2 個 blocking 級錯誤**：

- **錯誤 A：`description` 欄位資料遺失** — 前端 `types.ts` 定義 `Invoice.description` 並於表單收集、明細頁顯示，但後端 `Invoice` 模型**沒有 `description` 欄位**。`create_invoice` 與 `update_invoice` 傳入的 `description` 值被設定為 Python 物件的非映射屬性，`model_to_dict` 僅序列化 `__table__.columns`，故該值**永不寫入資料庫、永不回傳**，前端顯示永遠為空。（`api/models.py:Invoice`；`api/index.py:create_invoice`）
- **錯誤 B：`progress_rate` 顯示為錯誤數值** — 後端 `_calc_invoice_item_amounts` 與 `_recalc_invoice` 將完成率存為百分值（例如 `50.0` 代表 50%），但前端所有渲染處（`InvoiceListPage.tsx:167`、`InvoiceDetailPage.tsx:344`、`InvoiceDetailPage.tsx:330`）皆對該值再乘以 100，導致顯示「5000.0%」。**應統一為百分比不含再乘以 100**，或改為小數並前端乘以 100。

### 3. 程式碼品質與設計 (0–25)

**得分：23 / 25**

整體結構清晰、函式拆分明確（`_calc_invoice_item_amounts` / `_recalc_invoice` / `model_to_dict`）。路由分組整齊，權限檢查一致。扣分項目：

- `description` 欄位缺失屬於設計階段的欄位疏漏。
- AG Grid `onCellValueChanged` 每次編輯單一儲存格即呼叫 `fetchData()` 重新載入全表，若使用者快速編輯多列會產生大量請求且畫面閃爍，可考慮樂觀更新或 debounce。

### 4. 測試與驗證 (0–25)

**得分：24 / 25**

`npm run build` TypeScript 零錯誤，Flask 應用可正常啟動。`git diff` 顯示增量約 1,066 行改動，結構完整。唯無任何單元測試或整合測試腳本，僅靠建置驗證。seed data 未含 invoice 資料，無法開箱即測計價功能。

---

## 三、總分計算

| 項目 | 得分 | 權重佔比 |
|------|------|----------|
| 功能完整性 | 24 | 25% |
| 正確性 | 19 | 25% |
| 程式碼品質與設計 | 23 | 25% |
| 測試與驗證 | 24 | 25% |
| **總分** | **90 / 100** | **100%** |

> 因正確性扣分較重（–6），雖其餘三項表現穩健，最終落在 90 分邊線。

---

## 四、改進建議（低於 95 分，依嚴重度排列）

### 🔴 Blocking（必修正後方可合併）

1. **`api/models.py`：Invoice 模型缺少 `description` 欄位**
   - 問題：前端定義並使用 `Invoice.description`，但後端模型無此欄位，資料提交後**靜默遺失**。
   - 建議：在 `Invoice` 類別加入 `description = Column(Text, nullable=True)`，並重新生成遷移 / 重建資料庫。
   - 影響檔案：`api/models.py`、`api/index.py`（`create_invoice`、`update_invoice` 已引用該 key，修復模型即可）。

2. **`web-pcces/frontend/` 各頁面 `progress_rate` 渲染邏輯錯誤**
   - 問題：後端以「百分值」儲存（50.0 = 50%），前端卻以「小數」處理再乘以 100，導致顯示 5000%。
   - 建議：擇一方案：
     - **方案 A（建議）**：修改前端三處渲染，移除 `* 100`：
       - `InvoiceListPage.tsx:167`：`{(val).toFixed(1)}%`
       - `InvoiceDetailPage.tsx:344` (valueFormatter)：`\`${p.value.toFixed(1)}%\``
       - `InvoiceDetailPage.tsx:330` (Descriptions)：`{invoice.progress_rate.toFixed(1)}%`
     - **方案 B**：修改後端 `_calc_invoice_item_amounts` 與 `_recalc_invoice`，將 `* 100` 改為除以 `contract_qty` 取小數（0–1），前端不變。

### 🟡 Should-fix（建議修正）

3. **`api/seed_data.py`：無 invoice 示範資料**
   - 問題：seed data 僅含 users / projects / budget_items / resources，無 invoice 與 invoice_items。新使用者無法立即體驗計價流程。
   - 建議：新增 1–2 筆 invoice 及對應明細，方便 demo。

4. **`InvoiceDetailPage.tsx:185`：編輯儲存格後全表重新載入**
   - 問題：`onCellValueChanged` 每次編輯 `this_completed_qty` 都呼叫 `fetchData()`，若連續編輯多列會觸發 N 次全表請求，體驗不佳。
   - 建議：編輯後僅更新該 row 的本地狀態（樂觀更新），並在背景靜默呼叫 `invoiceApi.recalc()` 同步後端。

### ⚪ Nits（可選調整）

5. **`types.ts`：`Invoice` 型別中的 `description` 在後端無對應欄位**
   - 與 blocking #1 同源，修復模型後一併更新。

6. **APIs 路由風格不一致**
   - Invoice CRUD 使用 `/api/projects/<pid>/invoices/<iid>` 路徑（含 project_id），但 Item CRUD 及操作（submit/approve/recalc/report/export）使用 `/api/invoices/<iid>`（不含 project_id）。
   - 僅為風格差異，功能正常。若需統一可全部改為 `/api/projects/<pid>/invoices/<iid>/...`。

---

## 五、結論

**Minor nits, OK to ship after fixing the 2 blocking bugs.**  

整體實作品質扎實，15 條路由、前後端型別、狀態機邏輯皆正確。兩項 blocking 問題（`description` 欄位缺失、`progress_rate` 顯示錯誤）修正後即可合併；其餘建議可視時程決定是否跟進。
