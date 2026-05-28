# TASK-009 開發計畫：剩餘模組（系統插件 + 更新服務）

## 概述

將原始 WinForms 最後兩個未移植的模組 — **SysPlugin（系統插件）** 與 **PccesUpdateServices（更新服務）** — 以符合網頁架構的方式實作。

### 原始 WinForms 行為

| 模組 | 原始功能 | 檔案 |
|------|---------|------|
| **SysPlugin** | 讀取 `Addon.ini`，列出外部插件（TOOL1~TOOL20），點擊後以 `ShellExecute` 啟動外部程式。包含網站連結至 PCCES 外掛專區。 | `FormSysPlugin.cs` |
| **PccesUpdateServices** | ASMX SOAP WebService 客戶端，連線至 `bisc.archnowledge.com`，提供版本檢查、自動更新、線上註冊/驗證、公佈價格資料查詢等約 23 個 WebMethod。 | `Update.cs` + 28 個 callback 檔案 |

### 網頁版對應策略

| 原始模組 | 網頁版方案 | 理由 |
|---------|-----------|------|
| **SysPlugin** | **系統擴充功能管理（Feature Toggle）** — 管理員可啟用/停用系統中的各項功能開關，控制哪些模組對一般使用者可見。 | 網頁環境無法執行外部程式；功能開關是現代 SaaS 的標準做法，比插件更實用。 |
| **PccesUpdateServices** | **版本資訊頁面（Version Info）** — 顯示系統版本號、部署時間、技術棧資訊、更新日誌（CHANGELOG），並提供連結至 CI/CD Pipeline 或 GitHub Releases。 | 網頁版透過 Vercel CI/CD 更新，不需要獨立更新服務。版本資訊對使用者與管理員有參考價值。 |

---

## 實作步驟

### Step-1: 後端 — Feature Flag 模型 + API

**目標**：建立 `FeatureFlag` 資料表與管理 API，讓管理員可以啟用/停用系統功能。

**新增/修改檔案**：
| 檔案 | 操作 | 說明 |
|------|------|------|
| `api/models.py` | **修改** | 新增 `FeatureFlag` 模型 |
| `api/index.py` | **修改** | 新增 Feature Flag CRUD API + 公開查詢 API |
| `api/seed_data.py` | **修改** | 新增預設功能開關種子資料 |

#### 1.1 FeatureFlag 模型

```python
class FeatureFlag(Base):
    """功能開關（對應原始 SysPlugin 的插件啟用/停用）"""
    __tablename__ = "feature_flags"

    id = Column(Integer, primary_key=True, autoincrement=True)
    flag_key = Column(String(100), unique=True, index=True, nullable=False)   # 功能代號，如 "budget_compare"
    display_name = Column(String(300), nullable=False)                        # 顯示名稱，如 "工項比較"
    description = Column(Text, nullable=True)                                 # 功能說明
    category = Column(String(50), default="general")                          # 分類：general / budget / mrs / contract / invoice / report / admin
    is_enabled = Column(Boolean, default=True)                                # 是否啟用
    is_system = Column(Boolean, default=False)                                # 系統核心功能（不可停用）
    sort_order = Column(Integer, default=0)                                   # 排序
    created_at = Column(DateTime, default=_now)
    updated_at = Column(DateTime, default=_now, onupdate=_now)
```

#### 1.2 Feature Flag API 規格

**管理端點（Admin Only）**：

| 路由 | 方法 | 功能 |
|------|------|------|
| `/api/admin/feature-flags` | GET | 功能開關列表（後端分頁） |
| `/api/admin/feature-flags` | POST | 新增功能開關 |
| `/api/admin/feature-flags/<id>` | PUT | 更新功能開關（名稱、說明、啟用狀態等） |
| `/api/admin/feature-flags/<id>` | DELETE | 刪除功能開關（僅非 system 者可刪） |
| `/api/admin/feature-flags/<id>/toggle` | POST | 切換啟用/停用 |

**公開端點（Any Authenticated User）**：

| 路由 | 方法 | 功能 |
|------|------|------|
| `/api/feature-flags` | GET | 取得所有已啟用的功能開關列表（前端用於決定顯示哪些選單/功能） |

**回應範例**（GET `/api/admin/feature-flags`）：
```json
{
  "total": 12,
  "flags": [
    {
      "id": 1,
      "flag_key": "budget_compare",
      "display_name": "工項比較",
      "description": "允許跨專案比對預算項目差異",
      "category": "compare",
      "is_enabled": true,
      "is_system": false,
      "sort_order": 1,
      "updated_at": "2025-01-15T10:00:00Z"
    }
  ]
}
```

**PUT `/api/admin/feature-flags/<id>` 請求體**：
```json
{
  "display_name": "工項比較",
  "description": "允許跨專案比對預算項目差異",
  "category": "compare",
  "is_enabled": false,
  "sort_order": 1
}
```

#### 1.3 預設功能開關種子資料

在 `seed_data.py` 的 `seed_sysmaintain_data()`（或新增 `seed_feature_flags()`）中寫入以下預設開關：

| flag_key | display_name | category | is_enabled | is_system |
|----------|-------------|----------|------------|-----------|
| `project_management` | 專案管理 | general | ✅ true | ✅ true |
| `budget_editor` | 預算編輯 | budget | ✅ true | ✅ true |
| `resource_management` | 資源管理 | budget | ✅ true | false |
| `mrs_base` | 公共單價庫 | mrs | ✅ true | false |
| `invoice_management` | 計價管理 | invoice | ✅ true | false |
| `contract_management` | 分包合約 | contract | ✅ true | false |
| `settlement_management` | 分包結算 | contract | ✅ true | false |
| `acceptance_management` | 分包終驗 | contract | ✅ true | false |
| `budget_compare` | 工項比較 | compare | ✅ true | false |
| `mrs_price_compare` | 單價比較 | compare | ✅ true | false |
| `report_analysis` | 報表分析 | report | ✅ true | false |
| `system_maintenance` | 系統維護 | admin | ✅ true | ✅ true |

**技術細節**：
- `is_system=true` 的開關在前端隱藏「刪除」按鈕，且不可設為 `is_enabled=false`
- 前端在 `AppLayout` 初始化時呼叫 `GET /api/feature-flags`，將結果存入 store
- 後端 API 權限：管理端點使用 `require_admin`；公開端點使用 `require_auth`

**預計工時**：2.5 小時

---

### Step-2: 後端 — 版本資訊 API

**目標**：提供系統版本資訊端點，對應原始 Update Services 的版本查詢功能。

**新增/修改檔案**：
| 檔案 | 操作 | 說明 |
|------|------|------|
| `api/index.py` | **修改** | 新增 `/api/system/version`、`/api/system/health` 端點 |
| `api/version.py` | **新增** | 版本資訊設定檔（硬編碼版號，或讀取環境變數） |

#### 2.1 版本資訊設定檔

```python
# api/version.py
"""PCCES 網頁版版本資訊"""

APP_NAME = "PCCES 公共工程經費估算系統 — 網頁版"
APP_VERSION = "1.0.0"
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
```

#### 2.2 版本資訊 API 規格

| 路由 | 方法 | 功能 | 認證 |
|------|------|------|------|
| `/api/system/version` | GET | 取得系統版本資訊 | 無需認證（公開） |
| `/api/system/health` | GET | 系統健康檢查 | 無需認證（公開） |

**GET `/api/system/version` 回應**：
```json
{
  "app_name": "PCCES 公共工程經費估算系統 — 網頁版",
  "app_version": "1.0.0",
  "build_date": "2025-06-01",
  "repo_url": "https://github.com/your-org/pcces-web",
  "release_notes_url": "https://github.com/your-org/pcces-web/releases",
  "changelog": [
    {
      "version": "1.0.0",
      "date": "2025-06-01",
      "changes": ["初始版本", "專案管理、預算編輯、資源管理", "..."]
    }
  ],
  "dependencies": {
    "backend": { "python": "3.11+", "flask": "3.0+", "sqlalchemy": "2.0+" },
    "frontend": { "react": "18+", "antd": "5+", "vite": "5+" }
  }
}
```

**GET `/api/system/health` 回應**：
```json
{
  "status": "healthy",
  "database": "connected",
  "uptime_seconds": 3600,
  "timestamp": "2025-06-01T12:00:00Z"
}
```

**技術細節**：
- `uptime_seconds` 可在 `api/index.py` 啟動時記錄 `start_time = time.time()`
- 資料庫健康檢查：執行 `SELECT 1` 或簡單 query 確認連線
- 兩個端點皆不加 `require_auth`，讓 `/health` 可被監控系統使用
- 環境變數 `PCCES_APP_VERSION` 可覆蓋內建版號（便於 CI/CD 注入）

**預計工時**：1.5 小時

---

### Step-3: 前端 — 型別擴充

**目標**：在 TypeScript 型別定義中新增 FeatureFlag 與 VersionInfo 型別。

**新增/修改檔案**：
| 檔案 | 操作 | 說明 |
|------|------|------|
| `web-pcces/frontend/src/types.ts` | **修改** | 新增 FeatureFlag、VersionInfo 等型別 |

**新增型別**：

```typescript
// ═══ 功能開關（Feature Flag） ═══

export interface FeatureFlag {
  id: number;
  flag_key: string;
  display_name: string;
  description: string | null;
  category: string;
  is_enabled: boolean;
  is_system: boolean;
  sort_order: number;
  created_at: string;
  updated_at: string;
}

export interface FeatureFlagCreateData {
  flag_key: string;
  display_name: string;
  description?: string;
  category?: string;
  is_enabled?: boolean;
  is_system?: boolean;
  sort_order?: number;
}

export interface FeatureFlagUpdateData {
  display_name?: string;
  description?: string;
  category?: string;
  is_enabled?: boolean;
  sort_order?: number;
}

// ═══ 版本資訊 ═══

export interface VersionInfo {
  app_name: string;
  app_version: string;
  build_date: string;
  repo_url: string;
  release_notes_url: string;
  changelog: ChangelogEntry[];
  dependencies: Record<string, Record<string, string>>;
}

export interface ChangelogEntry {
  version: string;
  date: string;
  changes: string[];
}

export interface HealthStatus {
  status: 'healthy' | 'degraded' | 'down';
  database: 'connected' | 'disconnected';
  uptime_seconds: number;
  timestamp: string;
}
```

**預計工時**：0.5 小時

---

### Step-4: 前端 — API 層擴充

**目標**：在 `api.ts` 新增 Feature Flag 與 Version Info 的 API 呼叫方法。

**新增/修改檔案**：
| 檔案 | 操作 | 說明 |
|------|------|------|
| `web-pcces/frontend/src/api.ts` | **修改** | 新增 `adminApi.featureFlags.*` 與 `systemApi.*` |

**新增程式碼**：

```typescript
// ═══ 系統資訊（公開） ═══

export const systemApi = {
  getVersion: async (): Promise<VersionInfo> => {
    const res = await api.get('/api/system/version');
    return res.data;
  },
  getHealth: async (): Promise<HealthStatus> => {
    const res = await api.get('/api/system/health');
    return res.data;
  },
};

// ═══ 功能開關（公開 — 用於前端決定 UI 顯示） ═══

export const featureFlagApi = {
  listEnabled: async (): Promise<FeatureFlag[]> => {
    const res = await api.get('/api/feature-flags');
    return res.data;
  },
};

// ═══ 在 adminApi 中新增功能開關管理 ═══

// ... 在原有的 adminApi 中新增：

export const adminApi = {
  // ... 原有方法 ...

  // ── 功能開關管理（Admin Only） ──
  featureFlags: {
    list: async (params?: { category?: string; page?: number; per_page?: number }): Promise<{ total: number; flags: FeatureFlag[] }> => {
      const res = await api.get('/api/admin/feature-flags', { params });
      return res.data;
    },
    create: async (data: FeatureFlagCreateData): Promise<FeatureFlag> => {
      const res = await api.post('/api/admin/feature-flags', data);
      return res.data;
    },
    update: async (id: number, data: FeatureFlagUpdateData): Promise<FeatureFlag> => {
      const res = await api.put(`/api/admin/feature-flags/${id}`, data);
      return res.data;
    },
    delete: async (id: number): Promise<void> => {
      await api.delete(`/api/admin/feature-flags/${id}`);
    },
    toggle: async (id: number): Promise<FeatureFlag> => {
      const res = await api.post(`/api/admin/feature-flags/${id}/toggle`);
      return res.data;
    },
  },
};
```

**預計工時**：0.5 小時

---

### Step-5: 前端 — Feature Flag 管理頁面（Admin 新 Tab）

**目標**：在系統維護頁面新增「功能開關」Tab，管理員可檢視與切換所有功能開關。

**新增/修改檔案**：
| 檔案 | 操作 | 說明 |
|------|------|------|
| `web-pcces/frontend/src/pages/admin/FeatureFlagManagement.tsx` | **新增** | 功能開關管理頁面元件 |
| `web-pcces/frontend/src/pages/AdminPage.tsx` | **修改** | 新增「功能開關」Tab |

**畫面設計**：

```
┌─────────────────────────────────────────────────────────────┐
│  系統維護中心 > 功能開關                                       │
│  [全部] [一般] [預算] [單價庫] [計價] [合約] [比較] [報表] [管理]  │
├─────────────────────────────────────────────────────────────┤
│ ┌──────────┬────────────┬──────────┬──────────┬───────────┐ │
│ │ 功能代號  │ 功能名稱    │ 分類      │ 狀態      │ 操作       │ │
│ ├──────────┼────────────┼──────────┼──────────┼───────────┤ │
│ │ budget   │ 預算編輯    │ budget   │ 🟢 啟用  │ [停用]     │ │
│ │ _editor  │            │          │ (系統)   │ (不可操作)  │ │
│ ├──────────┼────────────┼──────────┼──────────┼───────────┤ │
│ │ budget   │ 工項比較    │ compare  │ 🟢 啟用  │ [停用]     │ │
│ │ _compare │            │          │          │           │ │
│ ├──────────┼────────────┼──────────┼──────────┼───────────┤ │
│ │ mrs_base │ 公共單價庫  │ mrs      │ 🔴 停用  │ [啟用]     │ │
│ └──────────┴────────────┴──────────┴──────────┴───────────┘ │
│                                                              │
│ [+ 新增功能開關]                                              │
│                                                              │
│ 提示：系統核心功能（標示「系統」者）不可停用或刪除。              │
└─────────────────────────────────────────────────────────────┘
```

**功能說明**：
1. **分類篩選按鈕**：上方按鈕列，點擊切換篩選特定分類
2. **功能開關列表**：Ant Design Table
   - 欄位：功能代號、功能名稱、分類（Tag）、狀態（Switch/標籤）、操作按鈕
   - `is_system=true` 的開關：Switch 禁用、操作欄顯示「系統核心」、隱藏刪除按鈕
3. **啟用/停用切換**：Switch 元件，即時調用 `adminApi.featureFlags.toggle(id)`
4. **新增功能開關**：Modal 表單（含 flag_key, display_name, description, category, sort_order）
5. **編輯功能開關**：Modal 表單（可編輯 display_name, description, category, sort_order — 不可修改 flag_key）
6. **刪除功能開關**：Popconfirm 確認（僅非 system 者可刪）
7. **狀態即時回饋**：切換後顯示 `message.success/error`

**技術細節**：
- 預設載入所有開關，前端按分類篩選
- 使用 `useState` + `useEffect` 管理列表狀態
- 切換開關時：樂觀更新 UI，API 失敗則復原
- 分類 Tag 顏色：general=blue, budget=green, mrs=orange, invoice=purple, contract=cyan, compare=geekblue, report=magenta, admin=red

**AdminPage.tsx 修改**：在 Tabs 中加入新 Tab：
```tsx
{
  key: 'feature-flags',
  label: <span><ControlOutlined /> 功能開關</span>,
  children: <FeatureFlagManagement />,
}
```

**預計工時**：3 小時

---

### Step-6: 前端 — 版本資訊頁面

**目標**：建立版本資訊頁面，顯示系統版本、更新日誌、技術棧與系統健康狀態。

**新增/修改檔案**：
| 檔案 | 操作 | 說明 |
|------|------|------|
| `web-pcces/frontend/src/pages/VersionInfoPage.tsx` | **新增** | 版本資訊頁面 |
| `web-pcces/frontend/src/App.tsx` | **修改** | 新增路由 `/app/version` |
| `web-pcces/frontend/src/components/AppLayout.tsx` | **修改** | 在側邊欄底部或「系統維護」附近新增版本資訊連結 |
| `web-pcces/frontend/src/api.ts` | 已於 Step-4 完成 | — |

**畫面設計**：

```
┌─────────────────────────────────────────────────────────────┐
│  版本資訊                                                     │
├─────────────────────────────────────────────────────────────┤
│                                                              │
│  ╔═══════════════════════════════════════════════════════╗    │
│  ║   PCCES 公共工程經費估算系統 — 網頁版                    ║    │
│  ║   版本：1.0.0 ｜ 建置日期：2025-06-01                 ║    │
│  ║   系統狀態：🟢 正常運作                               ║    │
│  ╚═══════════════════════════════════════════════════════╝    │
│                                                              │
│  ┌─────── 更新日誌 ────────────────────────────────────────┐  │
│  │                                                          │  │
│  │  v1.0.0 (2025-06-01)                                     │  │
│  │  ├─ 初始版本                                              │  │
│  │  ├─ 專案管理、預算編輯、資源管理                           │  │
│  │  ├─ 公共單價庫（MrsBase）                                 │  │
│  │  ├─ 計價管理、分包合約、結算、終驗                        │  │
│  │  ├─ 工項比較、單價比較                                    │  │
│  │  ├─ 系統維護（使用者/參數/代碼/組織）                     │  │
│  │  └─ 功能開關管理                                          │  │
│  │                                                          │  │
│  │  v0.9.0 (2025-05-15)                                     │  │
│  │  └─ Beta 版本，核心功能完成                               │  │
│  └──────────────────────────────────────────────────────────  │
│                                                              │
│  ┌─────── 系統狀態 ────────────────────────────────────────┐  │
│  │                                                          │  │
│  │  🟢 資料庫：已連線                                       │  │
│  │  🕐 上線時間：1 小時 30 分鐘                             │  │
│  │  🔄 最後更新：2025-06-01 12:00:00                        │  │
│  └──────────────────────────────────────────────────────────  │
│                                                              │
│  ┌─────── 技術棧 ──────────────────────────────────────────┐  │
│  │                                                          │  │
│  │  後端：Python 3.11 / Flask 3.0 / SQLAlchemy 2.0         │  │
│  │  前端：React 18 / Ant Design 5 / Vite 5                 │  │
│  │  資料庫：SQLite / PostgreSQL（可切換）                    │  │
│  │  部署：Vercel                                            │  │
│  └──────────────────────────────────────────────────────────  │
│                                                              │
│  [🔗 查看 GitHub Release]  [🔗 查看原始碼]                    │
└─────────────────────────────────────────────────────────────┘
```

**功能說明**：
1. **版本資訊卡片**：顯示 APP 名稱、版號、建置日期
2. **系統狀態卡片**：顯示資料庫連線狀態、上線時間、最後更新時間
   - 每秒更新一次 uptime（前端計算） 
   - 初始載入時呼叫 `systemApi.getHealth()`
3. **更新日誌**：依版本從新到舊列出，使用 `<Timeline>` 元件
4. **技術棧**：使用 `<Descriptions>` 或自訂卡片顯示
5. **外部連結**：GitHub Release / 原始碼按鈕
6. **自動刷新**：頁面每 60 秒自動重新檢查健康狀態

**路由配置**：
```tsx
// App.tsx
<Route path="version" element={<VersionInfoPage />} />
```

**側邊欄整合**（AppLayout.tsx）：
- 在「系統維護」選單群組下方加入分隔線與「版本資訊」選項
- 或放在側邊欄底部（固定在 collapse 區域外）
- 圖示：`<InfoCircleOutlined />`

```tsx
// 在所有選單項目之後
menuItems.push({ type: 'divider' });
menuItems.push({ key: 'version', icon: <InfoCircleOutlined />, label: '版本資訊' });
```

**handleMenuClick 新增**：
```typescript
case 'version': navigate('/app/version'); break;
```

**技術細節**：
- 使用 `useEffect` + `setInterval` 每 60 秒刷新健康狀態
- Uptime 顯示格式化：`${hours} 小時 ${minutes} 分鐘`
- 日誌使用 `<Timeline>` 元件顯示版本迭代
- 健康狀態圓點顏色：healthy=green, degraded=orange, down=red

**預計工時**：3 小時

---

### Step-7: 前端 — Store 整合 Feature Flag

**目標**：將啟用的功能開關列表存入全域 Store，讓 `AppLayout` 根據開關動態顯示/隱藏選單項目。

**新增/修改檔案**：
| 檔案 | 操作 | 說明 |
|------|------|------|
| `web-pcces/frontend/src/store.ts` | **修改** | 新增 `featureFlags` state 與 `loadFeatureFlags` action |
| `web-pcces/frontend/src/components/AppLayout.tsx` | **修改** | 根據 feature flags 條件式顯示選單 |

**Store 修改**：

```typescript
// store.ts 新增
interface AppState {
  // ... 既有狀態 ...

  // 功能開關
  featureFlags: Record<string, boolean>;  // { "budget_editor": true, ... }
  loadFeatureFlags: () => Promise<void>;
  isFeatureEnabled: (flagKey: string) => boolean;
}

// 在 create 中新增
featureFlags: {},
loadFeatureFlags: async () => {
  try {
    const flags = await featureFlagApi.listEnabled();
    const map: Record<string, boolean> = {};
    flags.forEach(f => { map[f.flag_key] = f.is_enabled; });
    set({ featureFlags: map });
  } catch {
    // 預設全部啟用
  }
},
isFeatureEnabled: (flagKey) => {
  const state = get();
  // 若 store 中無該 key，預設為 true（向後相容）
  return state.featureFlags[flagKey] ?? true;
},
```

**AppLayout 修改**：在元件初始化時呼叫 `loadFeatureFlags`，並根據 flags 條件式顯示選單項目。

```typescript
// AppLayout.tsx
const { loadFeatureFlags, isFeatureEnabled } = useAppStore();

useEffect(() => {
  loadFeatureFlags();
}, []);
```

選單顯示邏輯範例：
```typescript
const menuItems: any[] = [
  { key: 'dashboard', icon: <DashboardOutlined />, label: '儀表板' },
];

if (isFeatureEnabled('project_management')) {
  menuItems.push({ key: 'projects', icon: <FolderOutlined />, label: '專案管理' });
}
if (isFeatureEnabled('mrs_base')) {
  menuItems.push({ key: 'mrs-base', icon: <DatabaseOutlined />, label: '公共單價庫' });
}
if (isFeatureEnabled('budget_compare') || isFeatureEnabled('mrs_price_compare')) {
  const compareChildren = [];
  if (isFeatureEnabled('budget_compare')) {
    compareChildren.push({ key: 'compare-budget-items', icon: <FileTextOutlined />, label: '工項比較' });
  }
  if (isFeatureEnabled('mrs_price_compare')) {
    compareChildren.push({ key: 'compare-mrs-prices', icon: <BarChartOutlined />, label: '單價比較' });
  }
  if (compareChildren.length > 0) {
    menuItems.push({ key: 'compare', icon: <SwapOutlined />, label: '比較分析', children: compareChildren });
  }
}
if (isFeatureEnabled('budget_editor') && projectId) {
  menuItems.push({ key: `budget-${projectId}`, icon: <FileTextOutlined />, label: '預算編輯' });
}
// ... 依此類推
```

**預計工時**：2 小時

---

### Step-8: 測試 — 後端 API 測試

**目標**：為 Feature Flag API 與 Version API 撰寫自動化測試。

**新增/修改檔案**：
| 檔案 | 操作 | 說明 |
|------|------|------|
| `api/test_feature_flags.py` | **新增** | Feature Flag API 測試 |
| `api/test_version.py` | **新增** | Version / Health API 測試 |

**測試案例**：

```python
# test_feature_flags.py

class TestFeatureFlagAdminAPI:
    """管理員功能開關 API 測試"""

    def test_list_flags(self, admin_client):
        """管理員可列出所有功能開關"""
        res = admin_client.get("/api/admin/feature-flags")
        assert res.status_code == 200
        data = res.json()
        assert "total" in data
        assert "flags" in data
        assert len(data["flags"]) > 0

    def test_toggle_flag(self, admin_client, seeded_db):
        """切換功能開關啟用/停用"""
        flag_id = 1  # 預設 seed 的第一個
        res = admin_client.post(f"/api/admin/feature-flags/{flag_id}/toggle")
        assert res.status_code == 200

    def test_toggle_system_flag_forbidden(self, admin_client, seeded_db):
        """系統核心開關不可停用"""
        # 查出一個 is_system=true 的開關
        flags = admin_client.get("/api/admin/feature-flags").json()["flags"]
        sys_flag = [f for f in flags if f["is_system"]][0]
        res = admin_client.post(f"/api/admin/feature-flags/{sys_flag['id']}/toggle")
        assert res.status_code == 400  # 或其他 4xx

    def test_non_admin_cannot_manage(self, client):
        """非管理員無法管理功能開關"""
        res = client.get("/api/admin/feature-flags")
        assert res.status_code == 403

    def test_public_enabled_list(self, client):
        """一般使用者可查詢已啟用的開關"""
        res = client.get("/api/feature-flags")
        assert res.status_code == 200
        for flag in res.json():
            assert flag["is_enabled"] is True


# test_version.py

class TestVersionAPI:
    """版本資訊 API 測試"""

    def test_get_version(self, client):
        """取得版本資訊"""
        res = client.get("/api/system/version")
        assert res.status_code == 200
        data = res.json()
        assert "app_name" in data
        assert "app_version" in data
        assert "changelog" in data

    def test_get_health(self, client):
        """健康檢查正常回傳"""
        res = client.get("/api/system/health")
        assert res.status_code == 200
        data = res.json()
        assert data["status"] == "healthy"
        assert data["database"] == "connected"
```

**預計工時**：2 小時

---

### Step-9: 測試 — 前端元件測試

**目標**：為 FeatureFlagManagement 與 VersionInfoPage 撰寫基礎測試。

**新增/修改檔案**：
| 檔案 | 操作 | 說明 |
|------|------|------|
| `web-pcces/frontend/src/__tests__/FeatureFlagManagement.test.tsx` | **新增** | 功能開關管理頁面測試 |
| `web-pcces/frontend/src/__tests__/VersionInfoPage.test.tsx` | **新增** | 版本資訊頁面測試 |

**測試案例**：
1. FeatureFlagManagement 正確渲染開關列表
2. 分類篩選按鈕正常切換
3. 切換開關時呼叫正確 API
4. 系統核心開關顯示禁用狀態
5. VersionInfoPage 正確顯示應用名稱與版本號
6. 健康狀態卡片顯示正確狀態
7. 更新日誌 Timeline 正確渲染

**預計工時**：2 小時

---

## 總工時預估

| 步驟 | 內容 | 工時 |
|------|------|------|
| Step-1 | 後端 Feature Flag 模型 + API | 2.5h |
| Step-2 | 後端版本資訊 API | 1.5h |
| Step-3 | 前端型別擴充 | 0.5h |
| Step-4 | 前端 API 層擴充 | 0.5h |
| Step-5 | 前端 Feature Flag 管理頁面 | 3.0h |
| Step-6 | 前端版本資訊頁面 | 3.0h |
| Step-7 | Store 整合 Feature Flag | 2.0h |
| Step-8 | 後端 API 測試 | 2.0h |
| Step-9 | 前端元件測試 | 2.0h |
| **合計** | | **17.0h** |

---

## 依賴關係圖

```
Step-1 ──→ Step-4 ──→ Step-5
  │                     │
  │                     ↓
  │                   Step-7 ──→ AppLayout 整合
  │
Step-2 ──→ Step-4 ──→ Step-6

Step-3 (可與 Step-1, Step-2 平行進行)

Step-8 (依賴 Step-1, Step-2)
Step-9 (依賴 Step-5, Step-6)
```

建議開發順序：
1. **Step-1 + Step-2**（後端 API — 可平行進行）
2. **Step-3 + Step-4**（型別 + API 層 — 可平行進行）
3. **Step-5**（Feature Flag 管理頁面 — 需 Step-1 完成）
4. **Step-6**（版本資訊頁面 — 需 Step-2 完成）
5. **Step-7**（Store 整合 — 需 Step-1 完成，與 Step-5 可平行）
6. **Step-8 + Step-9**（測試）

---

## 注意事項

### Vercel 部署
- Feature Flag API 與 Version API 不需要特殊部署設定，與現有 API 相同
- 版本資訊中的 `build_date` 可透過環境變數 `VERCEL_GIT_COMMIT_SHA` 或 `NOW_PIPELINE_ID` 注入
- 建議在 `vercel.json` 或 CI/CD pipeline 設定 `PCCES_APP_VERSION` 環境變數

### 向後相容
- 若 `feature_flags` 資料表不存在或為空，前端預設所有功能啟用
- `isFeatureEnabled(key)` 若 key 不在 store 中，回傳 `true`
- 這確保升級既有部署時不會中斷功能

### 擴充性
- Feature Flag 系統可未來擴充為 A/B 測試、階段性開放（canary release）
- 可在 flag 上附加 `ext_data`（JSON 欄位）儲存 A/B 測試參數
- 版本資訊的 `dependencies` 欄位可擴充為包含版本檢查 API

### 安全性
- Feature Flag 管理端點嚴格限制 `admin` 角色
- `is_system=true` 的開關即使透過 API 也無法停用或刪除（後端檢查）
- 版本資訊端點公開，但僅提供基本系統資訊，不洩漏敏感配置

### UI/UX
- 功能開關頁面加入分類篩選，避免列表過長
- 版本資訊頁面設計為唯讀資訊頁面，不需任何編輯操作
- 側邊欄的「版本資訊」連結放在最下方（屬於通用資訊，非專案功能）
