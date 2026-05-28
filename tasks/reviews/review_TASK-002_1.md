# 評分報告 for TASK-002 (第 1 次循環)

## 評分時間
2026-05-28T18:00:00+08:00

## 評分者
reviewer-agent (REVIEWER 子代理)

---

## 評分檢查清單（必須 YES/NO）

| 檢查項目 | 判定 | 說明 |
|---------|------|------|
| 是否可執行 | **YES** | seed_data.py 可被 main.py 呼叫，不會拋例外；LoginPage 的示範帳號按鈕可點擊填入；LandingPage 可正常渲染 |
| 是否有錯誤 | **NO** | 無重大錯誤；`_recalc_seed` 邏輯正確，數值驗證通過 |
| 是否滿足需求條列 | **YES** | 三個需求項目（利潤計算修正、示範帳號按鈕、示範提示卡片）均已實作 |
| 是否有測試或滿足審美 | **Partial** | 現有 `test_recalc_children` 和 `test_budget_recalc` 涵蓋通用重算邏輯，但 seed_data 專屬利潤計算與前端互動無測試覆蓋 |

---

## 評分明細

| 項目 | 分數 | 說明 |
|------|------|------|
| 完整性 (25分) | **25/25** | 全部 3 項需求均已完成：(1) seed_data.py 正確實作包商利潤(5%)與營業稅(5%)的百分比計算邏輯；(2) LoginPage.tsx 已有「使用示範帳號」一鍵填入按鈕；(3) LandingPage.tsx 已有示範資料提示卡片。 |
| 正確性 (25分) | **25/25** | 所有數值驗證一致（直接費 59,591,000、間接費 3,630,000、利潤 3,161,050、稅金 3,319,102.5、三層總計 69,701,152.5）。`_recalc_seed` 兩階段走法（先遞迴計算 B/Z 樹，再特殊處理利潤類）正確處理了 profit items 的百分比計算。null quantity/price 有 `or 0` 保護。 |
| 可維護性 (25分) | **22/25** | 程式碼有繁體中文註解說明商業邏輯（如「包商利潤 = (直接＋間接)×5%」、「營業稅 = (直接＋間接＋利潤)×5%」），helper function 命名語義明確。扣分點：(1) `_recalc_seed` 函式較長（~100 行），通用遞迴邏輯與利潤特殊邏輯未拆分；(2) 建立 W 類型細項時在 line 124/154 先行計算 `amount`，被 `_recalc_seed` 覆寫 — 冗餘且可能誤導維護者。 |
| 測試與驗證 (25分) | **15/25** | 現有 `test_recalc_children` 涵蓋 B 類型加總子項的通用邏輯 ✅，`test_budget_recalc` 涵蓋 recalc endpoint ✅。但**(1) seed_data 的 `_recalc_seed` 利潤計算無任何測試** — 無人驗證包商利潤與營業稅的百分比計算是正確的；(2) 無測試驗證 seed 資料結構（樹狀層級、項目數量、金額）；(3) LoginPage 的示範帳號按鈕無前端測試；(4) LandingPage 的提示卡片無渲染測試。 |

---

## 總分

**87 / 100**

**結果：不合格（低於 90 分）**

---

## 缺失項目與改進建議

### 建議修復（對總分影響較大）

| # | 問題 | 影響 | 建議 |
|---|------|------|------|
| 1 | seed_data 利潤計算邏輯無專屬測試 — 現有 `test_recalc_children` 只測通用 B 加總，未驗證包商利潤的百分比計算是正確的 | 測試覆蓋缺口 | 在 `web-pcces/backend/tests/test_api.py` 新增 `test_seed_profit_calculation`，先呼叫 `seed_demo_data(db)` 再斷言 profit/tax 金額是否等於預期值 |
| 2 | 建立 W 細項時先行計算 amount（line 124/154），與 `_recalc_seed` 重複 | 可維護性 | 移除建立時的 `sub.amount = round(...)` 與 `item.amount = round(...)`，讓 `_recalc_seed` 統一負責所有金額計算 — 避免同一邏輯兩處實作 |
| 3 | `_recalc_seed` 函式過長（~100 行），通用樹遞迴與利潤特殊邏輯耦合 | 可維護性 | 將利潤特殊計算抽離為獨立函式 `_apply_profit_rules(db, project_id)`，讓 `_recalc_seed` 只做通用遞迴 |
| 4 | LoginPage 與 LandingPage 的示範功能無測試 | 測試與驗證 | 加入簡單的 React Testing Library 測試確認 `setFieldsValue({username: 'demo', password: 'demo123'})` 被正確觸發，以及 LandingPage 的示範卡片內容被渲染 |

### 次要問題

| # | 問題 | 建議 |
|---|------|------|
| 5 | B 類型第二層項目（開挖、結構等）建立時設定了 `quantity=1, unit_price=price`，但 B 類型忽略 qty×price，這些值無實際用途 | 建立 B 類型項目時可省略 quantity/unit_price 賦值，或加註解說明僅供參考 |

---

## 具體建議

1. **優先補測試**：為 `_recalc_seed` 的利潤計算撰寫 pytest 測試 (fixture 使用 SQLite in-memory)，驗證 profit_item.amount 與 tax_item.amount 等於預期值。
2. **消除冗餘代碼**：移除 line 124、154 的先行 `amount` 計算，讓 `_recalc_seed` 統管金額。
3. **重構 `_recalc_seed`**：將約 40 行的利潤特殊處理邏輯抽至獨立 helper，保留主函式只做通用的遞迴加總。
4. **前端測試**：可選加入簡單的 React Testing Library smoke test 驗證登入頁按鈕與首頁卡片的渲染。
