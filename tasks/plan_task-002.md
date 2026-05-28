# TASK-002 — 示範資料與實際操作體驗

## 狀態
📋 計畫撰寫中

## 目標
讓使用者一進入 PCCES 網頁版就能看到實際的專案、預算項目、資源資料，不需先登入或自行建立資料。同時修復現有示範資料功能中的 Bug，確保資料正確且可操作。

---

## 問題分析

### 現有實作概覽

| 項目 | 狀態 | 問題 |
|------|------|------|
| `api/seed_data.py` | ✅ 已存在 | 密碼 hash 格式錯誤、B 類型項目金額未計算 |
| `api/index.py` → `_ensure_db()` | ✅ 已存在 | 啟動時自動 seed，但 seed 資料有 bug |
| 前端 Dashboard | ✅ 已存在 | 功能完整，但示範資料不可用時顯示空白 |
| 前端 Landing Page | ✅ 已存在 | 有「立即開始使用」按鈕，但 guest 模式體驗差 |

### 關鍵 Bug 清單

1. **密碼 hash 錯誤** — `seed_data.py` 第 30 行使用 `password_hash="demo$123"`，不符合 `salt$key` 格式。`verify_password()` 會將 `"demo$123".split("$")` 得到 salt=`"demo"`、key=`"123"`，然後比對 PBKDF2-SHA256 的 hex 輸出是否等於 `"123"` — **永遠為 False**，因此 demo 使用者無法登入。

2. **B/Z 類型金額未計算** — `seed_data.py` 手動設定 W 類型 `amount = quantity × unit_price`，但 B 類型（直接工程費、間接工程費等）與 Z 類型（利潤及營業稅）的 amount 保持為 0，未遞迴加總子項金額。

3. **前端未針對有資料狀態做最佳化** — 雖然 guest 模式（user_id=1）可看到隸屬於 demo_user 的資料，但 LandingPage 的「立即開始使用」按鈕無登入引導，首次使用者可能不知道如何登入。

---

## 實作步驟

---

### Step 1: 修復 seed_data.py — 密碼 hash 與金額計算

#### 要修改的檔案
- `api/seed_data.py`

#### 技術細節

**1a. 修正 demo 使用者密碼 hash**

```python
# 原本（錯誤）
password_hash="demo$123",

# 修正後
password_hash=_hash_password("demo123"),
```

如此一來 demo 使用者可用帳號 `demo`、密碼 `demo123` 登入。

**1b. 加入示範資料金額重新計算**

在建立所有預算項目後，呼叫遞迴計算函數，讓 B 類型與 Z 類型項目正確加總子項金額。

新增 `_recalc_seed()` 函數：

```python
def _recalc_seed(db: Session, project_id: int):
    """遞迴計算示範專案所有 B/Z 類型項目的金額"""
    from sqlalchemy.orm import joinedload

    def _calc_amount(item: BudgetItem) -> float:
        return round((item.quantity or 0) * (item.unit_price or 0), item.decimal_amount)

    def _recalc(parent_id: Optional[int] = None) -> float:
        children = db.query(BudgetItem).filter(
            BudgetItem.project_id == project_id,
            BudgetItem.parent_id == parent_id
        ).all()
        total = 0.0
        for child in children:
            if child.kind in (BudgetItemKind.B, BudgetItemKind.Z):
                child.amount = _recalc(child.id)
            else:
                child.amount = _calc_amount(child)
            db.flush()
            total += child.amount or 0
        return round(total, 2)

    _recalc(None)
```

在 `seed_demo_data()` 回傳前（`db.commit()` 之前）呼叫：

```python
_recalc_seed(db, project.id)
```

**1c. 確保 W 類型項目金額一致**

目前 W 類型 item 的 amount 已在建立時手動計算，但為統一邏輯，改由 `_calc_amount()` 統一處理，或者維持手動計算但確保 formula 正確。

#### 預計工時
- 1 小時

---

### Step 2: 前端登入頁面加入「使用示範帳號」功能

#### 要修改的檔案
- `web-pcces/frontend/src/pages/LoginPage.tsx`

#### 技術細節

在登入頁面的登入 Tab 加入「使用示範帳號」快速填入按鈕，讓使用者一鍵填入 demo / demo123：

```tsx
// 在登入表單下方或旁邊加入
<Button 
  type="link" 
  onClick={() => form.setFieldsValue({ username: 'demo', password: 'demo123' })}
>
  使用示範帳號
</Button>
```

或者更直接的「一鍵示範登入」按鈕，自動填寫帳密並觸發登入。

#### 預計工時
- 0.5 小時

---

### Step 3: Landing Page 加入示範資料提示

#### 要修改的檔案
- `web-pcces/frontend/src/pages/LandingPage.tsx`

#### 技術細節

在 Hero 區域加入示範資料提示，讓首次使用者知道系統有內建示範資料：

```tsx
<Card style={{ 
  maxWidth: 500, margin: '24px auto', 
  background: 'rgba(255,255,255,0.15)', border: 'none',
  color: '#fff'
}}>
  <Text style={{ color: '#fff' }}>
    💡 系統已內建「OO 大樓新建工程」示範專案，內含完整的預算樹與資源資料。
    可直接以訪客身分瀏覽，或使用示範帳號登入操作。
  </Text>
</Card>
```

同時在「立即開始使用」按鈕的點擊行為上，若無 token 仍允許進入（目前已允許），但可考慮導向 login 或顯示提示。

#### 預計工時
- 0.5 小時

---

### Step 4: 重新建置前端並更新靜態檔案

#### 要修改/新增的檔案
- `web-pcces/frontend/` — 執行 `npm run build`
- 建置輸出會自動產出到 `web-pcces/frontend/dist/`
- 需要將建置產出**複製**到 `api/static/`

#### 技術細節

1. 在 `web-pcces/frontend/` 目錄下執行：
   ```bash
   npm run build
   ```
2. 建置完成後，將 `web-pcces/frontend/dist/` 的內容複製到 `api/static/`：
   ```bash
   cp -r web-pcces/frontend/dist/* api/static/
   ```

或者，撰寫一個 `build_frontend.sh` 腳本自動化此流程。

#### 預計工時
- 0.5 小時

---

### Step 5: 端到端驗證測試

#### 要做的事

1. **啟動應用** — `python api/index.py`
2. **確認 seed 資料自動建立** — 首次請求時觸發
3. **訪客模式瀏覽** — 不登入，直接打開 `http://localhost:8000/app/dashboard`，確認：
   - 儀表板統計卡片顯示正確數字
   - 專案列表顯示「OO 大樓新建工程」
   - 預算編輯器顯示樹狀結構且金額正確
   - 資源管理顯示 9 筆資源
4. **示範帳號登入** — 使用 demo / demo123 登入
   - 確認登入成功
   - 確認顯示名稱正確
5. **資料可操作性** — 確認可以：
   - 修改專案名稱/描述
   - 新增/編輯/刪除預算項目
   - 修改資源單價
   - 重新計算預算
6. **資料庫不重複寫入** — 重啟應用，確認 seed 資料不會重複寫入

#### 預計工時
- 1 小時

---

## 工時統計

| 步驟 | 描述 | 預計工時 |
|------|------|----------|
| Step 1 | 修復 seed_data.py — 密碼 hash 與金額計算 | 1.0 hr |
| Step 2 | 前端登入頁面加入「使用示範帳號」功能 | 0.5 hr |
| Step 3 | Landing Page 加入示範資料提示 | 0.5 hr |
| Step 4 | 重新建置前端並更新靜態檔案 | 0.5 hr |
| Step 5 | 端到端驗證測試 | 1.0 hr |
| **合計** | | **3.5 hrs** |

---

## 注意事項

### Vercel 部署相容性
- 所有修改均在 `api/` 目錄下，Vercel 會自動識別 Flask 應用
- `api/static/` 中的前端靜態檔案需先建置再部署
- Vercel 的 `vercel.json` 需確認已正確設定靜態檔案路由（如已設定則不需修改）

### 資料隔離
- 示範資料隸屬於 demo_user（id=1），guest 模式（user_id=1）可正常存取
- 新註冊的使用者**看不到**示範資料（因 `_check_project_access` 檢查 owner_id）
- 若需讓所有使用者都能看到示範資料，可考慮將示範專案設為 `is_public` 欄位（此需求未在 requirements 中，暫不實作）

### 種子資料冪等性
- `seed_demo_data()` 已檢查 `db.query(Project).count() > 0`，確保僅第一次執行時寫入
- 若需重新載入示範資料，需手動清空資料庫

### 前端開發伺服器
- 開發時使用 `web-pcces/frontend/` 下的 `npm run dev`
- 前端 dev server 透過 Vite proxy 將 `/api` 請求轉發到 `http://localhost:8000`
- 修改前端後需重新建置才能讓 `api/static/` 更新
