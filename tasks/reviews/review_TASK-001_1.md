# 評分報告 for TASK-001 (第 1 次循環)

## 評分時間
2026-05-28T14:40:00+08:00

## 評分者
reviewer-agent (REVIEWER 子代理)

---

## 評分檢查清單（必須 YES/NO）

| 檢查項目 | 判定 | 說明 |
|---------|------|------|
| 是否可執行 | **YES** | 前端 (port 5173) 與後端 (port 8000) 皆可啟動，登入/註冊流程正常 |
| 是否有錯誤 | **NO** | 存在多個嚴重錯誤（見下方詳細清單） |
| 是否滿足需求條列 | **NO** | 核心預算編輯功能（樹狀結構檢視、移動項目、更新價格）無法正常運作 |
| 是否有測試或滿足審美 | **NO** | 無任何自動化測試，前端 UI 有未完善之處 |

---

## 評分明細

| 項目 | 分數 | 說明 |
|------|------|------|
| 完整性 (25分) | **5/25** | 需求要求比原版 PCCES 更好的預算編輯器，但樹狀結構完全扁平、移動項目與更新價格 API 前後端不一致、Excel 匯出因無 token 而失敗。雖然有 6 個頁面骨架，但核心功能不可用。依據檢查清單「是否滿足需求條列=NO」，本項最高 10 分。 |
| 正確性 (25分) | **3/25** | 共發現 9 個嚴重/關鍵錯誤：預算樹無 children 序列化、金額計算忽略 item kind、Delete 無級聯、move/updatePrice API 前後端參數不一致、Excel 匯出無 token、密碼無鹽 SHA256、無資料隔離、無所有權檢查。依據檢查清單「是否有錯誤=NO」，本項最高 10 分。 |
| 可維護性 (25分) | **12/25** | 程式碼結構大致清晰，有繁體中文註解，但 requirements.txt 與實際使用套件不一致（寫 FastAPI 用 Flask），部分 naming 不明確（decimal_amount），資料庫缺少 relationship 定義 |
| 測試與驗證 (25分) | **0/25** | 完全沒有自動化測試。依據檢查清單「是否有測試=NO」，本項為 0 分。 |

---

## 總分

**20 / 100**

**結果：不合格（低於 90 分）**

---

## 缺失項目與改進建議

### 關鍵錯誤（必須修復）

| # | 錯誤描述 | 嚴重性 | 修復建議 |
|---|---------|--------|---------|
| 1 | 預算樹狀結構扁平 — `get_budget_tree` 回傳平面列表，前端的 Tree 元件無法顯示層級 | 🔴 關鍵 | 在 BudgetItem model 加入 children relationship，後端遞迴序列化樹狀結構 |
| 2 | `budgetApi.move` 前端傳 query param，後端讀 body — move 功能完全壞掉 | 🔴 關鍵 | 前端改用 POST body 傳 `new_parent_id`；或後端改讀 query param |
| 3 | `resourceApi.updatePrice` 前端傳 query param，後端讀 body — 更新單價完全壞掉 | 🔴 關鍵 | 前端改用 POST body 傳 `unit_price`；或後端改讀 query param |
| 4 | Excel 匯出沒有帶 Authorization header — 瀏覽器直接跳轉到 API URL 不會自動帶 token | 🔴 關鍵 | 改用 fetch + Blob 下載，手動加入 Authorization header |
| 5 | `_calc_amount` 對 B（主項）、Z（小計）類型也做 qty×price — 邏輯錯誤 | 🔴 關鍵 | B 類型應遞迴加總子項，不應該自行計算 amount |
| 6 | 密碼使用無鹽 SHA256 — 不安全 | 🟠 高 | 改用 bcrypt 或至少加鹽的 PBKDF2 |
| 7 | 無資料隔離 — 所有使用者看到所有專案 | 🟠 高 | `list_projects` 應過濾 `owner_id == user_id`（管理員除外） |
| 8 | 無所有權檢查 — 任何使用者可刪除任何專案 | 🟠 高 | delete/update 前檢查 `project.owner_id == user_id` |
| 9 | `requirements.txt` 與實際使用套件不一致 | 🟡 中 | 更新為 Flask, flask-cors, PyJWT, openpyxl |

### 次要問題

| # | 問題 | 建議 |
|---|------|------|
| 10 | 預算項目 Delete 無級聯刪除子項 | 加入 `ondelete="CASCADE"` |
| 11 | Model 無 `children` relationship | 加入 `children = relationship("BudgetItem", ...)` |
| 12 | CORS 全開 | 生產環境應限制域名 |
| 13 | 無任何測試 | 加入 pytest 與 React Testing Library |

---

## 具體建議

1. **優先修復 5 個關鍵錯誤**（樹狀結構、move API、updatePrice API、Excel 下載、amount 計算）
2. **補上資料庫 relationship** 讓 ORM 能正確處理父子關係與級聯
3. **前後端 API 規格應一致化** — 統一使用 JSON body 或 query params
4. **安全強化** — 密碼改用 bcrypt、加入資料所有權檢查
5. **更新 requirements.txt** 反映實際使用套件
