# PCCES 網頁版 — Vercel 部署

## 前置需求

- Node.js 18+
- Python 3.9+
- Vercel CLI (`npm i -g vercel`)

## 部署步驟

```bash
# 1. 安裝 Vercel CLI
npm i -g vercel

# 2. 登入 Vercel
vercel login

# 3. 部署（從專案根目錄）
vercel --prod
```

## 環境變數（Vercel 專案設定中配置）

| 變數 | 說明 | 預設值 |
|------|------|--------|
| `PCCES_SECRET_KEY` | JWT 加密密鑰 | （必填，請改為安全字串） |
| `PCCES_DATABASE_URL` | 資料庫連線 | `sqlite:///tmp/pcces.db` |
| `PCCES_TOKEN_EXPIRE_MINUTES` | Token 有效分鐘數 | `480` |

> ⚠️ **注意**：Vercel Serverless 環境中使用 SQLite 資料不會持久化。
> 生產環境建議使用 PostgreSQL，將 `PCCES_DATABASE_URL` 改為：
> `postgresql://user:pass@host:5432/pcces`

## 專案結構

```
/
├── api/
│   ├── index.py        # Flask API（Vercel Serverless Function）
│   └── models.py       # SQLAlchemy 資料庫模型
├── web-pcces/
│   └── frontend/
│       ├── src/        # React 原始碼
│       └── dist/       # 建置輸出（Vercel 自動產生）
├── vercel.json         # Vercel 設定
└── requirements.txt    # Python 依賴
```
