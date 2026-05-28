"""PCCES 網頁版版本資訊設定檔"""

import os

APP_NAME = "PCCES 公共工程經費估算系統 — 網頁版"
APP_VERSION = os.environ.get("PCCES_APP_VERSION", "1.0.0")
BUILD_DATE = "2025-06-01"
REPO_URL = "https://github.com/your-org/pcces-web"
RELEASE_NOTES_URL = "https://github.com/your-org/pcces-web/releases"
CHANGELOG = [
    {"version": "1.0.0", "date": "2025-06-01", "changes": [
        "初始版本",
        "專案管理、預算編輯、資源管理",
        "公共單價庫（MrsBase）",
        "計價管理、分包合約、結算、終驗",
        "工項比較、單價比較",
        "系統維護（使用者/參數/代碼/組織）",
        "功能開關管理",
        "報表匯出（PDF/Excel）",
    ]},
    {"version": "0.9.0", "date": "2025-05-15", "changes": [
        "Beta 版本",
        "核心功能完成",
    ]},
]

DEPENDENCIES = {
    "backend": {
        "python": "3.11+",
        "flask": "3.0+",
        "sqlalchemy": "2.0+",
    },
    "frontend": {
        "react": "18+",
        "antd": "5+",
        "vite": "5+",
    },
}
