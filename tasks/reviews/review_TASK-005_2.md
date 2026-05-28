# TASK-005 第 2 次評分審查 — 分包合約管理模組（返工後）

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
| 9 | 前端側邊欄選單 | YES | AppLayout 已加入「分包合約」+「計價管理」選項 |
| 10 | 前端 9 個頁面/元件 | YES | 含 BudgetItemPicker 共用元件 |
| 11 | Seed data（≥2 份合約） | YES | 結構體工程(active) + 裝修工程(draft)，附期別/結算/終驗 |
| 12 | npm run build 通過 | YES | 驗證通過（0 TypeScript errors） |
| 13 | Flask 載入無錯誤 | YES | 驗證通過 |

---

## 🔴 阻擋級 Bug 修復驗證

### Bug 1：Sidebar 導航路由 — ✅ 已修復

**原始問題**：`handleMenuClick` 將專案子頁面導航至 `/projects/${projectId}/...` 而非 `/app/projects/${projectId}/...`。

**修復驗證**（`web-pcces/frontend/src/components/AppLayout.tsx:56-62`）：
```typescript
if (info.key.startsWith('budget-')) navigate(`/app/projects/${projectId}/budget`);
else if (info.key.startsWith('resources-')) navigate(`/app/projects/${projectId}/resources`);
else if (info.key.startsWith('invoices-')) navigate(`/app/projects/${projectId}/invoices`);
else if (info.key.startsWith('contracts-')) navigate(`/app/projects/${projectId}/contracts`);
else if (info.key.startsWith('reports-')) navigate(`/app/projects/${projectId}/reports`);
```

所有 5 個側邊欄子項目路由均已加上 `/app` 前綴，與 `dashboard`、`projects` 頂層路由寫法一致。**修復正確。**

> 額外觀察：`getSelectedKey()`（第 28 行）仍比對 `/projects/...` 而非 `/app/projects/...`，這與側邊欄高亮的邏輯有關。若 React Router 的 `useLocation()` 回傳完整路徑以 `/app` 開頭，則此處無法正確匹配到對應的 menu key。請確認該函式預期收到的 `location.pathname` 格式。

### Bug 2：ContractItem.completed_qty 未同步 — ✅ 已修復

**原始問題**：`approve_contract_issue` 核准期別計價後未將完成數量寫回 `ContractItem`。

**修復驗證**（`api/index.py:2312-2323`）：
```python
issue_items = db.query(ContractIssueItem).filter(
    ContractIssueItem.issue_id == issue_id
).all()
total_completed_amount = 0.0
for ii in issue_items:
    if ii.contract_item_id:
        ci = db.query(ContractItem).filter(ContractItem.id == ii.contract_item_id).first()
        if ci:
            ci.completed_qty = ii.total_completed_qty
            ci.completed_amount = round(ii.total_completed_qty * ci.unit_price, 2)
            total_completed_amount += ci.completed_amount
if c:
    c.total_paid_amount = round(total_completed_amount, 2)
```

核准時會遍歷所有期別明細，將 `total_completed_qty` 寫回對應的 `ContractItem.completed_qty` 與 `completed_amount`，同時更新 `Contract.total_paid_amount`。**修復正確。**

---

## 四項細項評分

### 1. 功能性（滿分 25）— **23 分**

**修復後改善：**
- ContractItem.completed_qty 同步問題已修正（前次扣分的核心功能缺陷已解決）
- 合約生命週期管理完整：建立 → 工項 → 期別計價 → 結算 → 終驗
- 狀態機轉換嚴謹，前置條件檢查完整
- 批次匯入（預算→合約工項、合約工項→期別/終驗）已實作

**仍存在的扣分原因：**
- 結算金額計算仍使用兩種不一致的基準（`_recalc_settlement` line 2588: `st.settlement_amount = (st.contract_amount or 0) + total_add - total_deduct`，但明細級的 `diff_amount` 是 `actual_amount - item.contract_amount`）。若 `sum(item.contract_amount) ≠ st.contract_amount`，結果不一致
- `_recalc_issue` 的 `progress_rate` 仍以該期別明細範圍內的合約金額加總為分母（line 2109），而非真正的合約總額 `Contract.contract_amount`，部分導入時進度率失真

### 2. 程式品質與架構（滿分 25）— **20 分**

**修復後改善：**
- Sidebar 路由 Bug 已修正，所有子頁面導航正常運作
- 後端路由結構清晰，按 CRUD 與子功能分組
- 前端善用 TypeScript 介面與 useCallback
- BudgetItemPicker 元件可複用

**仍存在的扣分原因：**
- `_calc_invoice_item_amounts`（line 1057）與 `_calc_issue_item_amounts`（line 2087）仍為完全重複的計算邏輯，僅型別不同，應萃取共用
- Seed data 仍手動實作了金額計算公式（seed_data.py），而非呼叫 `_calc_issue_item_amounts`，未來公式異動時會不同步
- `getSelectedKey()` 的路徑比對邏輯可能與實際 `location.pathname` 格式不一致（見上方 Bug 1 的「額外觀察」）

### 3. 測試與驗證（滿分 25）— **15 分**

**維持不變：**
- ✅ npm run build 通過
- ✅ Flask 載入無錯誤
- ❌ 無單元測試（50 條 API 路由完全無測試覆蓋）
- ❌ 無整合測試（未測試狀態機轉換邊界）
- ❌ Edge case 未測試（超量完成、前期累計不一致等）
- ❌ 無前端元件測試或 E2E 測試

### 4. 使用體驗與安全（滿分 25）— **18 分**

**修復後改善：**
- Sidebar 子頁面導航已修正，使用者可正確點選所有功能
- AG Grid 可編輯表格 + 即時儲存、狀態標籤顏色區分、Modal 確認機制仍維持良好

**仍存在的扣分原因：**
- 仍無 `this_completed_qty` 超量完成的後端驗證（`total_completed_qty > contract_qty` 可自由輸入）
- `onCellValueChanged` 每次編輯仍觸發全量 `fetchData()` 重取重繪（IssueDetailPage、ContractDetailPage），連續編輯 N 列產生 N 次序列請求
- 合約刪除前端無 Popconfirm 對 active/closed 狀態進行阻擋

---

## 總分計算

| 項目 | 得分 | 滿分 | 前次得分 | 變動 |
|------|:----:|:----:|:--------:|:----:|
| 功能性 | 23 | 25 | 20 | +3 |
| 程式品質與架構 | 20 | 25 | 18 | +2 |
| 測試與驗證 | 15 | 25 | 15 | 0 |
| 使用體驗與安全 | 18 | 25 | 17 | +1 |
| **總分** | **76** | **100** | **70** | **+6** |

---

## 低於 90 分的具體缺失說明

### 已修復（前次的反饋已關閉）
1. ~~Sidebar 導航路由錯誤~~ → ✅ **已修復**（`/app` 前綴已補上）
2. ~~ContractItem.completed_qty 未同步~~ → ✅ **已修復**（approve 時回寫 completed_qty/completed_amount/total_paid_amount）

### 🟡 應在下一階段修正（Should-fix）

3. **結算金額計算邏輯不一致**（`api/index.py:_recalc_settlement` line 2588）
   - 與前次審查相同，尚未修正。
   - 摘要：明細級 `diff_amount = actual_amount - item.contract_amount` 與表頭級 `st.settlement_amount = st.contract_amount + total_add - total_deduct` 使用不同基準。若 `sum(item.contract_amount) ≠ st.contract_amount`，結果不一致。
   - 建議：將 `st.settlement_amount` 改為 `sum(item.actual_amount)`。

4. **`_recalc_issue` 進度率使用錯誤分母**（`api/index.py:2109-2113`）
   - 與前次審查相同，尚未修正。
   - 摘要：`progress_rate` 分母為該期明細內的合約金額加總，而非 `Contract.contract_amount`。部分導入工項時進度率失真。
   - 建議：從 `Contract.contract_amount` 取得分母，或於 UI 註明「僅計入已導入工項」。

5. **缺少超量完成驗證**（`api/index.py:2167-2209` 建立/更新 Issue Item）
   - 與前次審查相同，尚未修正。
   - 建議：在後端新增 `if item.total_completed_qty > item.contract_qty: return 400`。

### 🔵 建議改善（Nits）

6. **重複計算函式**（`api/index.py:1057` vs `2087`）：`_calc_invoice_item_amounts` 與 `_calc_issue_item_amounts` 邏輯完全重複。

7. **`getSelectedKey()` 路徑比對不一致**（`AppLayout.tsx:28-33`）：比對 `/projects/...`，但 `useLocation().pathname` 實際上為 `/app/projects/...`。這可能導致側邊欄高亮失效。

8. **`onCellValueChanged` 全量 reload**：每次編輯觸發 `fetchData()` 全量重取，連續編輯效能不佳。

9. **無自動化測試覆蓋**：50 條 API 路由、金流計算、狀態機轉換完全無測試。

10. **Seed data 手動實作計算公式**（seed_data.py）：直接 copy 而非呼叫 `_calc_issue_item_amounts`。

---

## 結論

**評分：76 分（不合格，但較前次 70 分有進步）**

兩個 blocking bug 均已正確修復，程式碼改動精準且通過 build 驗證。然而前次審查指出的應修正項目（should-fix）全部未處理，導致分數僅提升 6 分。若要在下次達到 90 分以上，至少需要：

1. 處理 **結算金額基準不一致** 與 **進度率分母錯誤** 兩個功能性扣分項（可提升功能性至 24+）
2. 補上 **至少基礎的 API 單元測試**（可提升測試性至 20+）
3. 解決 **超量完成驗證** 的安全缺失（可提升 UX/安全至 21+）
