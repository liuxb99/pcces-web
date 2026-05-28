# PCCES 網頁版 — 最終總結報告

## 專案概述
將 PCCES（公共工程經費估算系統）從 C# WinForms 重建為現代化網頁應用，前端 React + 後端 Flask，前後端一體部署於 Vercel。

## 技術棧

| 層級 | 技術 |
|------|------|
| 前端 | React 18 + TypeScript + Ant Design 5 + ECharts |
| 後端 | Flask + SQLAlchemy 2.0 + PyJWT + Openpyxl |
| 資料庫 | SQLite（開發）/ PostgreSQL（生產） |
| 部署 | Vercel（Python Serverless + Static Files） |
| 測試 | pytest（38 個自動化測試） |

## 評分歷程

```
1️⃣  20分 → 11 個關鍵錯誤（樹扁平、API 不一致、無安全）
2️⃣  70分 → 補測試、2 項安全缺失
3️⃣  48分 → 2 項修復未實際套用
4️⃣  70分 → 3 端點缺權限檢查
5️⃣  85分 → 缺環境變數配置、API 端點測試
6️⃣  92分 → ✅ 合格！
```

## 線上網址

- **正式網址：** [https://pcces-web.vercel.app](https://pcces-web.vercel.app)
- **API 健康檢查：** `https://pcces-web.vercel.app/api/health`

## 功能一覽

| 功能 | 說明 |
|------|------|
| 🏠 首頁說明頁 | 免登入，展示功能特色 |
| 🔐 使用者認證 | 註冊/登入，JWT + PBKDF2-SHA256 加鹽 |
| 📊 儀表板 | 統計卡片 + 圓餅圖 + 長條圖 + 最近專案 |
| 📁 專案管理 | CRUD + 資料隔離 + 所有權檢查 |
| ⭐ 預算編輯器 | 樹狀/表格檢視、搜尋、CRUD、自動計算 |
| 📦 資源管理 | 工/料/機分類、單價設定 |
| 📈 報表分析 | 互動圖表 + Excel 匯出（含 Auth） |
| 🔒 權限控管 | 全部 18 個 API 端點有所有權檢查 |

## 檔案結構

```
/
├── api/
│   ├── index.py         ← Flask API（22 routes）
│   ├── models.py        ← 資料庫模型
│   └── static/          ← 前端建置輸出（Vercel build 自動複製）
├── web-pcces/
│   └── frontend/        ← React 原始碼
├── vercel.json          ← Vercel 部署設定
├── requirements.txt     ← Python 依賴
└── DEPLOY.md            ← 部署說明
```

## 部署方式

```bash
npx vercel --prod --token YOUR_TOKEN
```

## 環境變數

| 變數 | 說明 | 預設值 |
|------|------|--------|
| `PCCES_SECRET_KEY` | JWT 密鑰（必填） | 內建預設值 |
| `PCCES_DATABASE_URL` | 資料庫連線字串 | `sqlite:///tmp/pcces.db` |
| `PCCES_JWT_ALGORITHM` | JWT 演算法 | HS256 |
| `PCCES_TOKEN_EXPIRE_MINUTES` | Token 時效 | 480 |

---

*報告生成時間：2026-05-28*
*最終評分：92/100 ✅ 合格*
