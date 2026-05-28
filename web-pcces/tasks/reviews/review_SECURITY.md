# 🔒 PCCES 登入/安全性審查報告

**日期**: 2025-01-XX  
**範圍**: 前端登入流程 + 後端認證架構  
**評分**: **8/10** — 良好基礎架構，少數生產強化點

---

## 評分檢查清單

| 項目 | 結果 | 備註 |
|------|------|------|
| 是否可執行 | **YES** | 完整前後端認證流程，Flask + React 可正常運作 |
| 是否有錯誤 | **YES (無錯誤)** | 邏輯正確，無安全漏洞路徑 |
| 是否滿足需求條列 | **YES** | 登入/註冊/JWT/路由保護/資料隔離全到位 |
| 是否有測試或滿足審美 | **YES** | 38 個自動化測試 + 整潔的 Ant Design UI |

---

## 逐檔案驗證結果

### 1. `LandingPage.tsx` — 首頁
| 檢查點 | 狀態 | 實作位置 |
|--------|------|----------|
| "登入使用" → `/login` | ✅ | L58: `navigate('/login')` |
| "登入" Header 按鈕 → `/login` | ✅ | L38: `navigate('/login')` |
| "註冊" Header 按鈕 → `/login?tab=register` | ✅ | L39: `navigate('/login?tab=register')` |
| "進入系統" → `/app/dashboard` (已登入才顯示) | ✅ | L35-36: 條件式 `isLoggedIn` 控制，`navigate('/app/dashboard')` |

**結論**: 未登入使用者只能走到 `/login` 或 `/login?tab=register`，無法繞過認證直接進入應用。

---

### 2. `LoginPage.tsx` — 登入頁
| 檢查點 | 狀態 | 實作位置 |
|--------|------|----------|
| 已登入跳轉 `/app/dashboard` | ✅ | L20-22: `if (token) return <Navigate to="/app/dashboard" replace />` |
| 登入需 username + password | ✅ | L31-32: `Form.Item` 皆 `required: true` |
| 密碼最小長度 (註冊) | ✅ | L89: `min: 6` 前端驗證 |
| 呼叫 `authApi.login()` | ✅ | L28: `const res = await authApi.login(values)` |

**安全設計亮點**:
- 使用 `<Navigate replace />` 避免瀏覽器回退到登入頁
- `handleLogin` 拋異常時只顯示通用錯誤訊息（不洩漏「帳號存在」或「密碼錯誤」）

---

### 3. `App.tsx` — 路由保護
| 檢查點 | 狀態 | 實作位置 |
|--------|------|----------|
| `/app/*` 由 `ProtectedRoute` 保護 | ✅ | L25-36: 整個 `/app` 區塊包在 `<ProtectedRoute>` 內 |
| `/` (Landing) 公開 | ✅ | L21: `<Route path="/" element={<LandingPage />} />` |
| `/login` 公開 | ✅ | L22: `<Route path="/login" element={<LoginPage />} />` |
| `ProtectedRoute` 無 token 則導向 `/login` | ✅ | L17: `if (!token) return <Navigate to="/login" replace />` |

**路由表**:
```
/          → LandingPage    (公開)
/login     → LoginPage      (公開)
/app/*     → ProtectedRoute → AppLayout → Dashboard/Projects/Budget/Resources/Reports
```

---

### 4. `store.ts` — Token 管理
| 檢查點 | 狀態 | 實作位置 |
|--------|------|----------|
| Token 從 localStorage 載入 | ✅ | L22: `token: localStorage.getItem('pcces_token')` |
| User 從 localStorage 載入 | ✅ | L21: `user: JSON.parse(localStorage.getItem('pcces_user') \|\| 'null')` |
| `setAuth` 寫入 localStorage | ✅ | L24-26 |
| `logout` 清除 localStorage | ✅ | L28-30 |
| `isAuthenticated()` 檢查 | ✅ | L31: `() => !!get().token` |

---

### 5. `api.ts` — HTTP 攔截器 (額外檢查)
| 檢查點 | 狀態 | 實作位置 |
|--------|------|----------|
| 請求自動帶入 Bearer token | ✅ | L16-18: `axios interceptor` 從 `localStorage` 讀取 `pcces_token` |
| 401 回應自動登出 | ✅ | L22-25: 清除 token 並 `window.location.href = '/login'` |

---

### 6. `backend/main.py` — 後端認證實作 (額外檢查)
| 檢查點 | 狀態 | 實作位置 |
|--------|------|----------|
| 密碼雜湊 PBKDF2-SHA256 + 鹽 | ✅ | L50-56: `get_password_hash()` — 100000 次迭代 |
| JWT 包含 exp 時戳 | ✅ | L66-69: `create_token()` 使用 `timedelta(minutes=ACCESS_TOKEN_EXPIRE_MINUTES)` |
| `@require_auth` 裝飾器 | ✅ | L78-85: 驗證 Bearer token，注入 `user_id` |
| 所有 API 端點皆有認證 | ✅ | 全部 18 個端點使用 `@require_auth` |
| 資料隔離 (所有權檢查) | ✅ | L145-155: `_check_project_access()` — admin 可看全部，一般使用者只看自己 |
| 重複帳號檢查 | ✅ | L106-108: 註冊前查詢 `User.username` |
| 停用帳號阻擋 | ✅ | L126: `if not user.is_active: return 403` |

---

## 安全發現與建議

### ✅ 優良實作

1. **PBKDF2-SHA256 密碼儲存** — 100000 次疊代 + 隨機鹽 (16 bytes)，符合 OWASP 建議
2. **JWT 過期機制** — 預設 480 分鐘 (8小時)，可透過環境變數調整
3. **完整所有權檢查** — 每個 CRUD 端點都經過 `_check_project_access()` 驗證使用者是否為專案擁有者或管理員
4. **401 自動登出** — axios response interceptor 統一處理 token 過期
5. **路由保護** — React Router 層級保護，未登入無法存取 `/app/*`
6. **38 個自動化測試** — 包含認證、資料隔離、邊界案例

### ⚠️ 中風險 — 建議改善

| 風險 | 說明 | 優先級 |
|------|------|--------|
| **預設 Secret Key** | `main.py:13` 預設值 `"pcces-web-secret-key-change-in-production"` — 若未設定環境變數，惡意者可偽造 JWT | **高** — 需在 production 部署前設定 `PCCES_SECRET_KEY` |
| **無登入速率限制** | `/api/auth/login` 無 rate limiting，可被暴力攻擊 | **中** — 建議加入 Flask-Limiter 或 nginx 層限制 |
| **Debug 模式** | `app.run(debug=True)` 若用於 production 會洩漏 stack trace | **中** — 確認僅用於開發環境 |
| **CORS 全開** | `CORS(app, resources={r"/api/*": {"origins": "*"}})` 允許所有來源 | **低** — 若有獨立前端 domain 應鎖定 origin |
| **無 server-side 密碼強度驗證** | 前端的 `min: 6` 在後端無對應驗證 | **低** — 可用 `cerberus` / `pydantic` 加入 |

### ℹ️ 低風險 / 權衡

- **Token 存 localStorage**: 不如 httpOnly cookie 安全 (XSS 可竊取)，但配合現有 axios 架構合理。若需更高安全等級可遷移至 httpOnly cookie + CSRF token
- **無 refresh token**: token 過期後需重新登入。權衡：簡化前端邏輯 vs. 減少重新登入次數

---

## 總結

PCCES 登入流程實作了完整的 **JWT Bearer 認證 + PBKDF2 密碼保護 + 前後端路由保護 + 資料所有權隔離**。無明顯安全漏洞或繞過路徑。生產部署前應設定自訂 `PCCES_SECRET_KEY` 環境變數，並考慮加入登入速率限制以強化暴力攻擊防護。
