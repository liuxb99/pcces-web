評分報告 for TASK-001 (第 3 次循環)

評分時間: 2025-01-15T12:00:00Z
評分者: reviewer-agent

評分檢查清單（必須 YES/NO）:
- 是否可執行: YES
- 是否有錯誤: NO
- 是否滿足需求條列: NO
- 是否有測試或滿足審美: YES

評分明細:
- 完整性: 8/25
- 正確性: 6/25
- 可維護性: 18/25
- 測試與驗證: 16/25

總分: 48/100
結果: 不合格

---

## 詳細說明

### 1. 是否可執行: YES
- Backend 相依套件完整 (`requirements.txt` 含 flask, flask-cors, pyjwt, sqlalchemy, openpyxl)
- Frontend 可透過 npm install + vite build 建置
- 但 `requirements.txt` 缺少 `pytest`（測試用，不影響主程式執行）

### 2. 是否有錯誤: NO（有錯誤）

**重大缺失 — 宣稱修正但未實作：**

#### 🔴 Fix #2: dashboard_stats 資料隔離 — 未實作
- `web-pcces/backend/main.py` line 154–176 (`dashboard_stats`)
- 所有查詢均為全域統計，未根據 `user_id` 過濾：
  ```python
  total_projects = db.query(func.count(Project.id)).scalar()  # 全部專案
  total_items = db.query(func.count(BudgetItem.id)).scalar()  # 全部預算項目
  total_resources = db.query(func.count(Resource.id)).scalar()  # 全部資源
  recent = db.query(Project).order_by(...).limit(5).all()  # 全部專案
  ```
- 未加入任何 `owner_id` 篩選或管理員檢查

#### 🔴 Fix #3: get_budget_tree 所有權檢查 — 未實作
- `web-pcces/backend/main.py` line 397–402
- 直接回傳樹狀結構，無任何專案所有權或權限驗證：
  ```python
  def get_budget_tree(project_id, user_id):
      tree = _build_tree_dict(db, project_id)
      return jsonify(tree)
  ```
- 其他未做所有權檢查的端點：`get_budget_list` (line 405), `create_budget_item` (line 412), `recalc_budget` (line 468)

#### 🟡 Fix #4: Z 類型處理 — ✅ 已修正
- `_recalc_children()` 中 `child.kind in (BudgetItemKind.B, BudgetItemKind.Z)` 正確包含 Z 類型

### 3. 是否滿足需求條列: NO
- 核心功能（認證、專案 CRUD、預算樹、資源、報表）存在
- 但兩項關鍵安全性需求（儀表板資料隔離、預算樹權限控制）宣稱修正但未實作
- 測試覆蓋模型層，但未測試 API 端點的授權邏輯，無法驗證修正是否有效

### 4. 是否有測試或滿足審美: YES
- 19 個 pytest 測試（`web-pcces/backend/tests/test_api.py`）
- 涵蓋 User、Project、BudgetItem、Resource 四種模型
- 包含邊界案例（零數量、負數、大數字、多層樹）
- 測試結構清晰，使用 fixture 管理資料庫生命週期

### 評分明細

#### 完整性: 8/25
- 需求條列=NO，最高 10 分
- 雖有完整功能面，但宣稱修正的 dashboard 隔離與樹權限兩項關鍵需求未完成
- 扣分至 8 分

#### 正確性: 6/25
- 有錯誤=NO（有錯誤），最高 10 分
- 2 項宣稱修正未實作 + 多個端點缺乏所有權檢查（get_budget_list, create_budget_item, recalc_budget）
- dashboard_stats 全域查詢洩漏所有使用者的專案資料
- 扣分至 6 分

#### 可維護性: 18/25
- 程式碼結構清晰，main.py 路由分區明確，函數命名一致
- 模型層 (models.py) 與應用層 (main.py) 分離良好
- 使用輔助函數（`model_to_dict`, `_calc_amount`, `_recalc_children`）減少重複
- 缺失：SECRET_KEY 寫死程式碼中、無設定管理、無統一錯誤處理器、部分路由重複查詢邏輯
- 扣分至 18 分

#### 測試與驗證: 16/25
- 19 個測試覆蓋模型層基本操作，edge cases（零數量、負數、大數字）良好
- 重大缺失：測試僅操作 SQLAlchemy 模型，**未使用 Flask test client 測試 API 端點**
  - 未測試 HTTP 狀態碼、JSON 回應格式
  - 未測試認證/授權行為（JWT token 驗證、所有權檢查）
  - 未測試 dashboard_stats 隔離邏輯
  - 未測試 get_budget_tree 權限檢查
  - `test_recalc_children` 手動計算總和，未呼叫實際的 `_recalc_children()` 函數
- 測試無法驗證本次循環宣稱的修正是否正確
- 扣分至 16 分

### 結論

總分 48/100 — 不合格。

本次循環宣稱修復 4 項問題，但僅 Fix #1（新增測試）和 Fix #4（Z 類型）實際完成。Fix #2（dashboard_stats 資料隔離）和 Fix #3（get_budget_tree 所有權檢查）在程式碼中**完全未實作**，與任務描述不符。此外，測試僅覆蓋模型層，未測試 API 端點，無法驗證修正是否有效。
