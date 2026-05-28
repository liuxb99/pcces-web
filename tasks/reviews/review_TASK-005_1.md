# TASK-005 評分審查 — 分包合約管理模組

## 評分檢查清單

| # | 檢查項目 | 結果 | 備註 |
|---|----------|------|------|
| 1 | 後端模型完整性（8 個新模型） | YES | Contract, ContractItem, ContractIssue, ContractIssueItem, ContractSettlement, ContractSettlementItem, ContractFinalAcceptance, ContractFinalAcceptanceItem 均已定義 |
| 2 | API 路由數量（≈50 條） | YES | 合約 7 + 工項 5 + 期別 7 + 期別明細 6 + 結算 7 + 結算明細 5 + 終驗 7 + 終驗明細 6 = 50 條 |
| 3 | 狀態機正確性（draft→active→closed→finalized） | YES | 合約四態正確；子項三態 (draft→submitted→approved) 正確 |
| 4 | 自動金額計算 | YES | `_calc_issue_item_amounts`、`_recalc_contract_amount`、`_recalc_settlement`、`_recalc_issue` 均已實作 |
| 5 | 批次匯入功能 | YES | batch-from-contract（期別、終驗）、batchCreate（合約工項）均已實作 |
| 6 | 前端型別定義（9 個新型別） | YES | Contract → ContractFinalAcceptanceItemCreateData 全部定義 |
| 7 | 前端 API 服務層完整 | YES | contractApi 物件包含全部 50 路由的封裝 |
| 8 | 前端路由註冊 | YES | 8 條新路由正確掛載於 App.tsx |
| 9 | 前端側邊欄選單 | YES | AppLayout 已加入「分包合約」選項 |
| 10 | 前端 9 個頁面/元件 | YES | 含 BudgetItemPicker 共用元件 |
| 11 | Seed data（≥2 份合約） | YES | 結構體工程(active) + 裝修工程(draft)，附期別/結算/終驗 |
| 12 | npm run build 通過 | YES | 驗證通過 |
| 13 | Flask 載入無錯誤 | YES | 驗證通過 |

---

## 四項細項評分

### 1. 功能性（滿分 25）— **20 分**

**優勢：**
- 完整覆蓋分包合約生命週期：合約建立 → 工項管理 → 期別計價 → 結算 → 終驗
- 狀態機轉換嚴謹，每個轉換都有前置條件檢查
- 批次匯入（預算工項→合約工項、合約工項→期別明細）降低手動輸入
- AG Grid 可編輯表格提供良好的資料編輯體驗

**扣分原因：**
- `ContractItem.completed_qty` 在期別計價核准後**未被更新**，導致合約工項的完成數量永遠不會反映已核准的計價結果，是資料一致性的實質缺陷（嚴重）
- 結算金額計算混用兩種基準：`item.diff_amount` 以 `item.contract_amount`（明細級）為基準，但最終公式 `st.contract_amount + total_add - total_deduct` 以 `st.contract_amount`（表頭級快照）為基準。若兩者不一致，計算結果錯誤
- `_recalc_issue().progress_rate` 使用**期別明細內**的合約金額加總，而非合約主檔的 `contract_amount`，若工項未全數導入，進度率會失真

### 2. 程式品質與架構（滿分 25）— **18 分**

**優勢：**
- 後端路由結構清晰，按 CRUD 與子功能分組
- `_check_contract_access` 共用權限檢查避免重複
- 前端善用 TypeScript 介面與 useCallback 做效能優化
- BudgetItemPicker 元件可複用於多個頁面

**扣分原因：**
- **重複計算函式**：`_calc_issue_item_amounts`（line 2087）與 `_calc_invoice_item_amounts`（line 1057）為完全相同的邏輯，僅型別標註不同，應共用
- **Sidebar 路由錯誤**（BUG）：AppLayout 的 `handleMenuClick` 中，專案子頁面導航至 `/projects/${id}/...` 而非 `/app/projects/${id}/...`，導致點擊側邊欄所有子項目（預算、資源、計價、分包合約、報表）均導向不存在的路由。這是影響所有功能的前端路由 Bug
- **Seed data 手動計算**：seed_data.py 在建立 ContractIssueItem 時重複實作了 `_calc_issue_item_amounts` 的公式，未來若公式異動會不同步
- 無 `BudgetItemPicker` 被用在結算和終驗的批次匯入（僅期別計價有 batch-from-contract）

### 3. 測試與驗證（滿分 25）— **15 分**

**優勢：**
- 已通過 build 編譯驗證
- 已通過 Flask 載入驗證

**扣分原因：**
- **無單元測試**：50 條 API 路由完全無測試覆蓋，金流計算（結算、計價）的正確性僅能靠目視
- **無整合測試**：未測試狀態機轉換邊界（如已結案合約+ 新增期別計價）
- Edge case 未測試：如 `this_completed_qty` 超過 `remain_qty` 的超量情境
- 無前端的元件測試或 E2E 測試

### 4. 使用體驗與安全（滿分 25）— **17 分**

**優勢：**
- AG Grid 可編輯表格 + 即時儲存，操作直覺
- 狀態標籤顏色區分明確
- 使用者友善的 Modal 確認（提交/核准/刪除）
- 側邊欄有明確的「分包合約」功能分區

**扣分原因：**
- **缺少超量完成驗證**：`this_completed_qty` 無上限檢查，使用者可輸入超過 `remain_qty` 的數量，導致 `total_completed_qty > contract_qty`
- **`onCellValueChanged` 每次編輯都全量 reload**：編輯 1 格就觸發 `fetchData()` 重取所有資料 + 重繪 AG Grid。連續編輯 N 列會產生 N 次序列請求（同樣問題在 TASK-004 審查中已被標記）
- 合約刪除前端無 `Popconfirm` 對 active 或 closed 狀態的合約進行阻擋，僅靠後端 400 回應

---

## 總分計算

| 項目 | 得分 | 滿分 |
|------|:----:|:----:|
| 功能性 | 20 | 25 |
| 程式品質與架構 | 18 | 25 |
| 測試與驗證 | 15 | 25 |
| 使用體驗與安全 | 17 | 25 |
| **總分** | **70** | **100** |

---

## 低於 90 分的具體缺失說明

### 🔴 阻擋級（Blocking — 應修正後再合併）

1. **Sidebar 導航路由錯誤**（`web-pcces/frontend/src/components/AppLayout.tsx:59-63`）
   - 問題：`handleMenuClick` 中將專案子頁面導航到 `/projects/${projectId}/contracts`，但 React Router 路由定義於 `/app/projects/:id/contracts`，導致所有子頁面連結失效。
   - 修正：將子頁面導航改為 `/app/projects/${projectId}/contracts`（與 dashboard、projects 的寫法保持一致）。

2. **ContractItem.completed_qty 未隨期別核准同步**（`api/index.py:2284-2317`）
   - 問題：`approve_contract_issue` 核准期別計價後更新了 `c.total_issue_amount`，但未更新各 `ContractItem.completed_qty` 與 `completed_amount`。合約工項的完成進度永遠為 0（除非手動編輯），造成資料不一致。
   - 修正：核准時應將該期所有明細的 `total_completed_qty` 更新回對應 `ContractItem.completed_qty`，並重新計算 `completed_amount`。

### 🟡 應該修正（Should-fix）

3. **結算金額計算邏輯不一致**（`api/index.py:_recalc_settlement`）
   - 問題：`item.diff_amount = actual_amount - item.contract_amount`（明細級），但 `st.settlement_amount = st.contract_amount + total_add - total_deduct`（表頭級）。若 `sum(item.contract_amount) ≠ st.contract_amount`，結果不一致。
   - 修正：將 `st.settlement_amount` 改為 `sum(item.actual_amount)`，或確保 `st.contract_amount = sum(item.contract_amount)`。

4. **`_recalc_issue` 進度率使用錯誤分母**（`api/index.py:2109-2113`）
   - 問題：`progress_rate` = `cumulative_amount / sum(issue items 的 contract_qty × unit_price)`，而非真正的合約總額 `contract.contract_amount`。部分導入時進度率 ≠ 實際合約進度。
   - 修正：從 `Contract.contract_amount` 取得分母，或記錄這是「期別明細範圍內」的進度並在 UI 標示清楚。

5. **缺少超量完成驗證**（`api/index.py:2337-2379` `create_issue_item`、`api/issues/<id>/items/<id>` PUT）
   - 問題：`this_completed_qty` 無 `total_completed_qty ≤ contract_qty` 的驗證，使用者可填入不合理的數量。
   - 修正：在後端新增 `if item.total_completed_qty > item.contract_qty: return 400` 或至少發出警告。

### 🔵 建議改善（Nits）

6. **重複計算函式**（`api/index.py:1057` vs `2087`）
   - `_calc_invoice_item_amounts` 與 `_calc_issue_item_amounts` 邏輯完全重複，應萃取為共用輔助函式。

7. **`onCellValueChanged` 觸發全量重新請求**（`IssueDetailPage.tsx:119-126`、`ContractDetailPage.tsx`）
   - 每次編輯一格資料就發送 API 更新 + `fetchData()` 全量重取。連續編輯多列時效能不佳。
   - 建議：編輯後僅更新本地 state（由回傳的 API 回應更新），或 debounce 全量重取。

8. **無自動化測試覆蓋**（全域）
   - 50 條新 API 路由、複雜的金流計算（加減帳、進度率）完全無測試。建議至少為 _recalc 系列函式與狀態機轉換補上 pytest 測試。

9. **Seed data 手動實作計算公式**（`api/seed_data.py:388-393`）
   - 建立 ContractIssueItem 時直接 copy 了 `_calc_issue_item_amounts` 的公式。若未來調整重算邏輯，seed data 會產生不一致的歷史資料。
   - 建議：seed data 中呼叫 `_calc_issue_item_amounts` 函式。

10. **合約工項 `sort_order` 無排序邏輯**（`api/index.py:2024-2057` batch import）
    - 批次匯入時所有工項 `sort_order=0`，AG Grid 依賴 `sort_order, id` 排序，等同於插入順序。使用者無法自訂排序。
    - 建議：批次匯入時依 `BudgetItem.sort_order` 或 `item_no` 給定遞增序號。
