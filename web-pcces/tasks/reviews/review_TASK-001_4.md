# 評分報告 for TASK-001 (第 4 次循環 — FINAL)

評分時間: 2025-01-15T18:00:00Z  
評分者: reviewer-agent

---

## 評分檢查清單（必須 YES/NO）

| 項目 | 判定 | 說明 |
|------|------|------|
| 是否可執行 | **YES** | Backend 可啟動 (`main.py`)，Frontend 可建置 (Vite)，19 項 pytest 全部 PASS (耗時 2.09s) |
| 是否有錯誤 | **NO（有錯誤）** | 3 項宣稱修正已到位，但仍有 3 個端點缺少所有權檢查 |
| 是否滿足需求條列 | **YES** | 認證、專案 CRUD、預算樹、資源、報表、Excel 匯出齊全 |
| 是否有測試或滿足審美 | **YES** | 19 項模型層測試 + 邊界案例，結構清晰 |

---

## 本次循環修正驗證

| 項目 | 狀態 | 位置 |
|------|------|------|
| `dashboard_stats` 資料隔離 | ✅ **已實作** | `main.py` — `is_admin` 檢查 + 非管理員僅看 `owner_id == user_id` 的專案統計、資源、預算項目 |
| `get_budget_tree` 所有權檢查 | ✅ **已實作** | `main.py` — 查詢專案是否存在 → 檢查 `is_admin` / `project.owner_id != user_id` → 403 |
| Z 類型 recalc 跳過 | ✅ **已實作** | `_recalc_children()` 包含 `BudgetItemKind.Z`，`create_budget_item` / `update_budget_item` 中 B/Z 類型不計算 amount |

### 仍存在的問題（未宣稱修正，但列為已知）

| 端點 | 問題 | 位置 |
|------|------|------|
| `GET /api/projects/{id}/budget/` (get_budget_list) | 無所有權檢查 | `main.py` line ~405 |
| `POST /api/projects/{id}/budget/` (create_budget_item) | 無所有權檢查 | `main.py` line ~412 |
| `POST /api/projects/{id}/budget/recalc` (recalc_budget) | 無所有權檢查 | `main.py` line ~468 |
| `SECRET_KEY` | 寫死於程式碼中 | `main.py` line 18 |
| 錯誤處理 | 無統一 error handler | 各端點各自回傳錯誤 |

---

## 評分明細

### 完整性: 22/25
- 是否滿足需求條列 = **YES** → 可使用完整區間 (0-25)
- 核心功能面完整：認證註冊登入、專案 CRUD + 資料隔離、預算樹狀結構 + 遞迴重算、資源管理、摘要報表 + Excel 匯出
- 本次循環確實補齊了第 3 次遺漏的 2 項安全修正
- 微扣分：仍有 3 個端點缺乏所有權檢查，以及部分前端頁面 (ReportsPage, ResourcesPage) 為空白模板

### 正確性: 10/25
- 是否有錯誤 = **NO（有錯誤）** → 最高 10 分
- 3 項宣稱修正全部到位，程式碼邏輯正確 (資料隔離 `is_admin` + `owner_id`、Z 類型 recalc 跳過)
- 但 `get_budget_list`、`create_budget_item`、`recalc_budget` 三端點仍有未受權限控制的資料存取
- 密碼雜湊使用 PBKDF2-SHA256、JWT token 過期設定、CORS 配置正確
- 給 10 分（此類別上限）

### 可維護性: 18/25
- 路由分區明確（認證 / 專案 / 預算 / 資源 / 報表），函數命名一致 (snake_case)
- 模型層 (`models.py`) 與應用層 (`main.py`) 分離良好
- 使用輔助函數 (`model_to_dict`, `_calc_amount`, `_recalc_children`, `_build_tree_dict`) 減少重複
- 缺失：
  - `SECRET_KEY` 寫死於程式碼（應使用環境變數或設定檔）
  - 無統一錯誤處理器 (try/finally 模式重複)
  - `get_budget_list` 與 `get_budget_tree` 有重複的查詢邏輯
  - 無型別註解或 Pydantic schema 驗證

### 測試與驗證: 20/25
- 是否有測試 = **YES** → 可使用完整區間 (0-25)
- 19 項 pytest 測試全數 PASS（2.09 秒）
- 涵蓋 User (3)、Project (4)、BudgetItem (7)、Resource (2)、EdgeCases (4)
- 包含邊界案例：零數量、負數、大數字、多層樹結構
- 資料庫生命週期管理使用 fixture (drop_all → create_all)，獨立性良好
- 缺失：
  - 測試僅操作 SQLAlchemy 模型層，**未使用 Flask test client 測試 API 端點**
  - 未測試 HTTP 狀態碼、JSON 回應格式、JWT 認證失敗情境
  - `test_recalc_children` 手動計算總和，未呼叫實際的 `_recalc_children()` 函數
  - 無測試覆蓋 dashboard_stats 或 get_budget_tree 的授權邏輯

---

## 總分

| 項目 | 分數 |
|------|------|
| 完整性 | 22 |
| 正確性 | 10 |
| 可維護性 | 18 |
| 測試與驗證 | 20 |
| **總分** | **70/100** |

**結果：不合格**（未達 80 分門檻）

---

## 結論

與第 3 次循環 (48/100) 相比，本次循環實質進步 22 分，因為第 2 次循環宣稱修正、第 3 次未實作的 **2 項關鍵安全修正已全部到位**：`dashboard_stats` 資料隔離（管理員 bypass + owner_id 過濾）與 `get_budget_tree` 所有權檢查。Z 類型 recalc 正確處理。19 項測試全數通過。

分數回到與第 2 次循環相同的 70/100 水準，但仍有 3 個端點 (`get_budget_list`, `create_budget_item`, `recalc_budget`) 缺乏所有權檢查，以及 SECRET_KEY 寫死、無統一錯誤處理、測試僅涵蓋模型層等問題。建議下一循環優先解決這 3 個端點的權限檢查，並導入 Flask test client 進行 API 端對端測試。
