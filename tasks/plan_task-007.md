# TASK-007 開發計畫：系統維護完整版（SysMaintain）

## 概述

本模組對應原始 WinForms 的 `SysMaintain`（22 個 .cs 檔案），提供系統層級的後台管理功能。包含：**使用者管理**、**角色/權限管理**、**系統參數設定**、**組織管理**，以及**系統維護主頁面**。

所有功能僅限 `admin` 角色存取，透過 JWT + role check 進行權限控管。

---

## 1. 實作架構圖

```
web-pcces/frontend/src/
├── pages/
│   ├── AdminPage.tsx              ← 系統維護主頁面（Tabs）
│   ├── admin/
│   │   ├── UserManagement.tsx     ← 使用者管理（列表/編輯/啟用停用/角色變更）
│   │   ├── UserEditForm.tsx       ← 使用者編輯表單 Modal
│   │   ├── RoleManagement.tsx     ← 角色/權限管理
│   │   ├── CodeTableManagement.tsx← 代碼表 CRUD
│   │   ├── SystemParamsPage.tsx   ← 系統參數設定
│   │   └── OrganizationManage.tsx ← 組織管理
│   └── ...
├── api.ts                         ← 新增 admin API 方法
└── types.ts                       ← 新增型別定義

api/
├── index.py                       ← 新增 admin API endpoints（admin 專屬路由）
├── models.py                      ← 新增 SysParam / CodeTable / Organization 模型
└── seed_data.py                   ← 新增系統參數、代碼表、組織等示範資料
```

---

## 2. 實作步驟

### Step 1 — 新增資料庫模型（SysMaintain 模型）

**目標**：在 `api/models.py` 新增系統維護所需的三個資料表。

**新增模型清單**：

#### 1.1 SystemParameter — 系統參數表
對應原始 `FormSys_E`、`FormSys_F`、`FormSys_G`（系統參數 E/F/G）

| 欄位 | 型別 | 說明 |
|------|------|------|
| id | Integer PK | |
| category | String(50) | 參數分類（E/F/G） |
| code | String(100) | 參數代碼（唯一 per category） |
| c_name | String(300) | 參數名稱 |
| c_value | Text, nullable | 參數值 |
| c_default | Text, nullable | 預設值 |
| sort_order | Integer | 排序 |
| is_active | Boolean | 是否啟用 |
| memo | Text, nullable | 備註 |
| created_at | DateTime | |
| updated_at | DateTime | |

#### 1.2 CodeTable — 代碼主表
對應原始 `FormSys_C`（部門/編碼管理）、`FormSys_D`（公物編碼）

| 欄位 | 型別 | 說明 |
|------|------|------|
| id | Integer PK | |
| table_code | String(50) | 代碼表識別碼（如 DEPT, ASSET, ORG） |
| table_name | String(300) | 代碼表名稱 |
| memo | Text, nullable | 備註 |
| is_active | Boolean | 是否啟用 |
| created_at | DateTime | |
| updated_at | DateTime | |

#### 1.3 CodeItem — 代表明細

| 欄位 | 型別 | 說明 |
|------|------|------|
| id | Integer PK | |
| table_id | FK → CodeTable.id (CASCADE) | 所屬代碼表 |
| parent_id | FK → CodeItem.id (nullable) | 父項目（樹狀結構） |
| code | String(50) | 代碼 |
| c_name | String(300) | 中文名稱 |
| sort_order | Integer | 排序 |
| is_active | Boolean | 是否啟用 |
| ext_data | JSON, nullable | 擴充欄位（用於公物編碼等額外資料） |
| memo | Text, nullable | 備註 |
| created_at | DateTime | |
| updated_at | DateTime | |

**關聯**：`children = relationship("CodeItem", backref="parent", remote_side=[id])`

#### 1.4 Organization — 組織機構表
對應原始 `OrganizationPicker`

| 欄位 | 型別 | 說明 |
|------|------|------|
| id | Integer PK | |
| parent_id | FK → Organization.id (nullable) | 上級組織 |
| code | String(50) | 組織代碼（唯一） |
| c_name | String(300) | 組織名稱 |
| org_type | String(50) | 組織類型（機關/部門/課室） |
| sort_order | Integer | 排序 |
| is_active | Boolean | 是否啟用 |
| contact_person | String(100), nullable | 聯絡人 |
| contact_phone | String(50), nullable | 聯絡電話 |
| address | String(500), nullable | 地址 |
| memo | Text, nullable | 備註 |
| created_at | DateTime | |
| updated_at | DateTime | |

**關聯**：`children = relationship("Organization", backref="parent", remote_side=[id])`

**受影響檔案**：
- `api/models.py` — 新增 SystemParameter、CodeTable、CodeItem、Organization 模型
- `api/seed_data.py` — 新增對應的示範資料種子函數

**預計工時**：1.5 小時

---

### Step 2 — 新增後端 Admin API Endpoints

**目標**：在 `api/index.py` 新增 admin 專屬 API，所有路由加上 `require_admin` 裝飾器。

**新增函數**：

#### 2.1 require_admin 裝飾器
```python
def require_admin(f):
    @wraps(f)
    def decorated(*args, **kwargs):
        auth = request.headers.get("Authorization", "")
        if not auth.startswith("Bearer "):
            return jsonify({"detail": "未授權"}), 401
        payload = decode_token(auth[7:])
        if not payload:
            return jsonify({"detail": "Token 無效"}), 401
        user_id = int(payload["sub"])
        db = next(get_db())
        try:
            user = db.query(User).filter(User.id == user_id).first()
            if not user or user.role != UserRole.ADMIN.value:
                return jsonify({"detail": "需要管理員權限"}), 403
            kwargs["user_id"] = user_id
            return f(*args, **kwargs)
        finally:
            db.close()
    return decorated
```

#### 2.2 使用者管理 API
| 路由 | 方法 | 功能 | 說明 |
|------|------|------|------|
| `/api/admin/users` | GET | 使用者列表 | 分頁、查詢、排序 |
| `/api/admin/users` | POST | 建立使用者 | 管理員代建 |
| `/api/admin/users/<id>` | GET | 單筆詳情 | |
| `/api/admin/users/<id>` | PUT | 更新使用者 | 含角色變更、啟用停用 |
| `/api/admin/users/<id>` | DELETE | 刪除使用者 | 僅可刪除非管理員 |
| `/api/admin/users/<id>/toggle-active` | POST | 啟用/停用切換 | |
| `/api/admin/users/<id>/change-role` | POST | 變更角色 | |

**請求/回應規格**：

PUT `/api/admin/users/<id>`
```json
{
  "display_name": "王小明",
  "email": "wang@example.com",
  "company": "測試機關",
  "department": "工務課",
  "phone": "0912345678",
  "role": "editor",
  "is_active": true
}
```

#### 2.3 系統參數 API（SystemParameter）
| 路由 | 方法 | 功能 |
|------|------|------|
| `/api/admin/params` | GET | 參數列表（可依 category 篩選） |
| `/api/admin/params` | POST | 新增參數 |
| `/api/admin/params/<id>` | PUT | 更新參數 |
| `/api/admin/params/<id>` | DELETE | 刪除參數 |

#### 2.4 代碼表 API（CodeTable / CodeItem）
| 路由 | 方法 | 功能 |
|------|------|------|
| `/api/admin/code-tables` | GET | 代碼表列表 |
| `/api/admin/code-tables` | POST | 新增代碼表 |
| `/api/admin/code-tables/<id>` | PUT | 更新代碼表 |
| `/api/admin/code-tables/<id>` | DELETE | 刪除代碼表 |
| `/api/admin/code-tables/<table_id>/items` | GET | 代碼項列表（樹狀） |
| `/api/admin/code-tables/<table_id>/items` | POST | 新增代碼項 |
| `/api/admin/code-items/<id>` | PUT | 更新代碼項 |
| `/api/admin/code-items/<id>` | DELETE | 刪除代碼項 |

#### 2.5 組織機構 API
| 路由 | 方法 | 功能 |
|------|------|------|
| `/api/admin/organizations` | GET | 組織列表（樹狀） |
| `/api/admin/organizations` | POST | 新增組織 |
| `/api/admin/organizations/<id>` | PUT | 更新組織 |
| `/api/admin/organizations/<id>` | DELETE | 刪除組織 |

**受影響檔案**：
- `api/index.py` — 新增約 20 個 API endpoints
- `api/models.py` — 已於 Step 1 新增

**預計工時**：3 小時

---

### Step 3 — 前端型別定義擴充

**目標**：在 `types.ts` 新增 SysMaintain 相關型別。

**新增型別**：

```typescript
// ─── 系統維護 ───

// 使用者管理擴充
export interface UserAdmin extends User {
  // 繼承 User 所有欄位
}

export interface UserCreateData {
  username: string;
  password: string;
  display_name: string;
  email?: string;
  company?: string;
  department?: string;
  phone?: string;
  role?: string;
}

export interface UserUpdateData {
  display_name?: string;
  email?: string;
  company?: string;
  department?: string;
  phone?: string;
  role?: string;
  is_active?: boolean;
}

// 系統參數
export interface SystemParameter {
  id: number;
  category: string;
  code: string;
  c_name: string;
  c_value: string | null;
  c_default: string | null;
  sort_order: number;
  is_active: boolean;
  memo: string | null;
  created_at: string;
  updated_at: string;
}

export interface SystemParamCreateData {
  category: string;
  code: string;
  c_name: string;
  c_value?: string;
  c_default?: string;
  sort_order?: number;
  is_active?: boolean;
  memo?: string;
}

// 代碼表
export interface CodeTable {
  id: number;
  table_code: string;
  table_name: string;
  memo: string | null;
  is_active: boolean;
  created_at: string;
  updated_at: string;
}

export interface CodeItem {
  id: number;
  table_id: number;
  parent_id: number | null;
  code: string;
  c_name: string;
  sort_order: number;
  is_active: boolean;
  ext_data: Record<string, any> | null;
  memo: string | null;
  created_at: string;
  updated_at: string;
  children?: CodeItem[];
}

// 組織
export interface Organization {
  id: number;
  parent_id: number | null;
  code: string;
  c_name: string;
  org_type: string;
  sort_order: number;
  is_active: boolean;
  contact_person: string | null;
  contact_phone: string | null;
  address: string | null;
  memo: string | null;
  created_at: string;
  updated_at: string;
  children?: Organization[];
}

export interface OrganizationCreateData {
  parent_id?: number | null;
  code: string;
  c_name: string;
  org_type?: string;
  sort_order?: number;
  contact_person?: string;
  contact_phone?: string;
  address?: string;
  memo?: string;
}
```

**受影響檔案**：
- `web-pcces/frontend/src/types.ts` — 新增上述型別

**預計工時**：0.5 小時

---

### Step 4 — 前端 API 層擴充

**目標**：在 `api.ts` 新增 admin API 呼叫方法。

**新增程式碼範例**：

```typescript
// ═══ 系統維護（Admin Only） ═══

export const adminApi = {
  // ── 使用者管理 ──
  listUsers: async (params?: { q?: string; role?: string; is_active?: string; page?: number; per_page?: number }): Promise<{ users: UserAdmin[]; total: number }> => {
    const res = await api.get('/api/admin/users', { params });
    return res.data;
  },
  getUser: async (id: number): Promise<UserAdmin> => {
    const res = await api.get(`/api/admin/users/${id}`);
    return res.data;
  },
  createUser: async (data: UserCreateData): Promise<UserAdmin> => {
    const res = await api.post('/api/admin/users', data);
    return res.data;
  },
  updateUser: async (id: number, data: UserUpdateData): Promise<UserAdmin> => {
    const res = await api.put(`/api/admin/users/${id}`, data);
    return res.data;
  },
  deleteUser: async (id: number): Promise<void> => {
    await api.delete(`/api/admin/users/${id}`);
  },
  toggleUserActive: async (id: number): Promise<UserAdmin> => {
    const res = await api.post(`/api/admin/users/${id}/toggle-active`);
    return res.data;
  },
  changeUserRole: async (id: number, role: string): Promise<UserAdmin> => {
    const res = await api.post(`/api/admin/users/${id}/change-role`, { role });
    return res.data;
  },

  // ── 系統參數 ──
  listParams: async (category?: string): Promise<SystemParameter[]> => {
    const res = await api.get('/api/admin/params', { params: { category } });
    return res.data;
  },
  createParam: async (data: SystemParamCreateData): Promise<SystemParameter> => {
    const res = await api.post('/api/admin/params', data);
    return res.data;
  },
  updateParam: async (id: number, data: Partial<SystemParamCreateData>): Promise<SystemParameter> => {
    const res = await api.put(`/api/admin/params/${id}`, data);
    return res.data;
  },
  deleteParam: async (id: number): Promise<void> => {
    await api.delete(`/api/admin/params/${id}`);
  },

  // ── 代碼表 ──
  listCodeTables: async (): Promise<CodeTable[]> => {
    const res = await api.get('/api/admin/code-tables');
    return res.data;
  },
  createCodeTable: async (data: Partial<CodeTable>): Promise<CodeTable> => {
    const res = await api.post('/api/admin/code-tables', data);
    return res.data;
  },
  updateCodeTable: async (id: number, data: Partial<CodeTable>): Promise<CodeTable> => {
    const res = await api.put(`/api/admin/code-tables/${id}`, data);
    return res.data;
  },
  deleteCodeTable: async (id: number): Promise<void> => {
    await api.delete(`/api/admin/code-tables/${id}`);
  },
  listCodeItems: async (tableId: number): Promise<CodeItem[]> => {
    const res = await api.get(`/api/admin/code-tables/${tableId}/items`);
    return res.data;
  },
  createCodeItem: async (tableId: number, data: Partial<CodeItem>): Promise<CodeItem> => {
    const res = await api.post(`/api/admin/code-tables/${tableId}/items`, data);
    return res.data;
  },
  updateCodeItem: async (id: number, data: Partial<CodeItem>): Promise<CodeItem> => {
    const res = await api.put(`/api/admin/code-items/${id}`, data);
    return res.data;
  },
  deleteCodeItem: async (id: number): Promise<void> => {
    await api.delete(`/api/admin/code-items/${id}`);
  },

  // ── 組織機構 ──
  listOrganizations: async (): Promise<Organization[]> => {
    const res = await api.get('/api/admin/organizations');
    return res.data;
  },
  createOrganization: async (data: OrganizationCreateData): Promise<Organization> => {
    const res = await api.post('/api/admin/organizations', data);
    return res.data;
  },
  updateOrganization: async (id: number, data: Partial<OrganizationCreateData>): Promise<Organization> => {
    const res = await api.put(`/api/admin/organizations/${id}`, data);
    return res.data;
  },
  deleteOrganization: async (id: number): Promise<void> => {
    await api.delete(`/api/admin/organizations/${id}`);
  },
};
```

**受影響檔案**：
- `web-pcces/frontend/src/api.ts` — 新增 adminApi 物件

**預計工時**：0.5 小時

---

### Step 5 — 系統維護主頁面（AdminPage.tsx）

**目標**：建立管理後台主頁面，以 Ant Design Tabs 切換各子功能。

**頁面結構**：

```
┌─────────────────────────────────────────┐
│  Header: 系統維護中心                     │
├─────────────────────────────────────────┤
│  [使用者管理] [角色權限] [代碼管理] [系統參數] [組織機構]  │
├─────────────────────────────────────────┤
│                                         │
│  各 Tab 內容區                           │
│                                         │
└─────────────────────────────────────────┘
```

**實作細節**：
- 路由：`/app/admin`（需在 App.tsx 新增路由）
- 使用 `<Tabs>` 元件，5 個 TabPane
- 每個 Tab 延遲載入對應的子元件
- 首次進入時檢查當前使用者是否為 admin，非 admin 跳轉到 dashboard
- 側邊欄新增「系統維護」選單項（僅 admin 可看到）

**受影響檔案**：
- `web-pcces/frontend/src/pages/AdminPage.tsx` — 新增
- `web-pcces/frontend/src/App.tsx` — 新增路由 `/app/admin`
- `web-pcces/frontend/src/components/AppLayout.tsx` — 條件式顯示「系統維護」選單

**預計工時**：1 小時

---

### Step 6 — 使用者管理元件（UserManagement.tsx + UserEditForm.tsx）

**目標**：建立使用者列表、搜尋、編輯、啟用停用、角色變更功能。

**UserManagement.tsx**（列表頁）：

| 功能 | 實作方式 |
|------|----------|
| 使用者列表 | Ant Design Table，欄位：帳號、姓名、Email、公司、部門、角色、狀態、建立時間 |
| 搜尋篩選 | Search input（關鍵字搜尋 username/display_name/email）+ 角色下拉篩選 + 狀態篩選 |
| 分頁 | 後端分頁 API |
| 新增使用者 | Modal 表單（UserEditForm） |
| 編輯 | Modal 表單（UserEditForm，預填資料） |
| 啟用/停用 | Switch 元件，即時切換 is_active |
| 角色變更 | Select 下拉（admin/reviewer/editor/viewer），即時更新 |
| 刪除 | Popconfirm 確認後刪除 |

**UserEditForm.tsx**（編輯 Modal）：

| 欄位 | 型別 | 必填 |
|------|------|------|
| username | Input | 是（新增時） |
| password | Input.Password | 否（編輯時留空則不修改） |
| display_name | Input | 是 |
| email | Input | 否 |
| company | Input | 否 |
| department | Input | 否 |
| phone | Input | 否 |
| role | Select | 是（預設 editor） |
| is_active | Switch | 否（預設 true） |

**受影響檔案**：
- `web-pcces/frontend/src/pages/admin/UserManagement.tsx` — 新增
- `web-pcces/frontend/src/pages/admin/UserEditForm.tsx` — 新增

**預計工時**：2 小時

---

### Step 7 — 角色/權限管理元件（RoleManagement.tsx）

**目標**：建立角色管理頁面，可檢視與變更使用者的角色。

**注意**：目前 PCCES 的角色為 `UserRole` 枚舉（`admin`/`reviewer`/`editor`/`viewer`），尚無細粒度的功能權限表。此步驟實作**角色指派**功能（對應原始 FormSys_B 的簡化版）。

**功能**：
1. 以表格列出所有使用者與其當前角色
2. 透過 Select 下拉即時變更角色
3. 顯示各角色的人數統計

**角色說明**（對應原始權限設計）：

| 角色 | 說明 | 可存取功能 |
|------|------|-----------|
| admin | 系統管理員 | 所有功能 + 系統維護 |
| reviewer | 審核者 | 檢視/審核計價、合約 |
| editor | 編輯者（預設） | 專案 CRUD、預算編輯、資源管理 |
| viewer | 唯讀 | 僅可瀏覽資料 |

**UI 設計**：
- 上方卡片顯示各角色人數（admin/reviewer/editor/viewer）
- 下方 Table 列出使用者，角色欄位使用 Select 即可編輯
- 加入 Audit Log 顯示最近的角色變更記錄（可選）

**受影響檔案**：
- `web-pcces/frontend/src/pages/admin/RoleManagement.tsx` — 新增

**預計工時**：1.5 小時

---

### Step 8 — 代碼表管理元件（CodeTableManagement.tsx）

**目標**：建立代碼表 CRUD 頁面，對應原始 FormSys_C（部門/編碼管理）、FormSys_D（公物編碼）。

**功能**：
1. 左側代碼表列表（Card 列表）
2. 右側代碼項樹狀管理
3. 代碼表 CRUD（新增/編輯/刪除）
4. 代碼項 CRUD（新增/編輯/刪除，支援樹狀父子結構）
5. 拖曳排序

**UI 設計**：
```
┌─────────────────────────────────────────────────────┐
│  [新增代碼表]                                        │
├──────────────────┬──────────────────────────────────┤
│  代碼表列表        │  代碼項管理                        │
│                   │                                  │
│  📁 部門編碼       │  ├─ 工務課                        │
│  📁 公物編碼       │  ├─ 機電課                        │
│  📁 工程分類       │  │  ├─ 電機組                     │
│                   │  │  └─ 空調組                     │
│                   │  └─ 建築課                        │
│                   │                                  │
│                   │  [+新增] [✏編輯] [🗑刪除]           │
└──────────────────┴──────────────────────────────────┘
```

**示範資料種子**（在 seed_data.py 中新增）：

| 代碼表 | 代碼 | 名稱 |
|--------|------|------|
| DEPT（部門編碼） | GEN | 工務課 |
| DEPT | MEC | 機電課 |
| DEPT | ARC | 建築課 |
| ASSET（公物編碼） | PC | 個人電腦 |
| ASSET | PRT | 印表機 |
| ASSET | FURN | 辦公家具 |

**受影響檔案**：
- `web-pcces/frontend/src/pages/admin/CodeTableManagement.tsx` — 新增

**預計工時**：2.5 小時

---

### Step 9 — 系統參數設定元件（SystemParamsPage.tsx）

**目標**：建立系統參數設定頁面，對應原始 FormSys_E、FormSys_F、FormSys_G。

**功能**：
1. 以 Tabs 切換參數分類（E / F / G）
2. 表格列出該分類下的所有參數
3. 可編輯參數值（c_value）
4. 可新增/刪除參數

**預設參數分類**：

| 分類 | 說明 | 示範參數 |
|------|------|----------|
| E | 系統參數 E | 機關名稱、機關代碼、系統標題 |
| F | 系統參數 F | 預設利潤率(5%)、預設營業稅率(5%) |
| G | 系統參數 G | 工程分類、預算年度 |

**UI 設計**：
- 使用 Editable Table（Ant Design Table 內嵌可編輯 Input）
- 參數值修改後即時儲存（onBlur 觸發 API）
- 新增按鈕跳出 Modal 填寫完整參數資訊

**受影響檔案**：
- `web-pcces/frontend/src/pages/admin/SystemParamsPage.tsx` — 新增

**預計工時**：1.5 小時

---

### Step 10 — 組織機構管理元件（OrganizationManage.tsx）

**目標**：建立組織機構管理頁面，對應原始 OrganizationPicker。

**功能**：
1. 樹狀顯示組織架構（Ant Design Tree 元件）
2. 新增/編輯/刪除組織節點
3. 展開/折疊樹節點
4. 顯示組織類型標籤（機關/部門/課室）

**UI 設計**：
```
┌──────────────────────────────────────────────┐
│  組織機構管理                     [+新增根組織] │
├──────────────────────────────────────────────┤
│                                              │
│  🏢 工程會（機關）                              │
│  ├─ 🏢 工務組（部門）                            │
│  │  ├─ 📁 道路課（課室）                         │
│  │  └─ 📁 建築課（課室）                         │
│  └─ 🏢 秘書室（部門）                            │
│     └─ 📁 文書課（課室）                         │
│                                              │
│  選取節點後顯示編輯面板：                         │
│  ┌─ 名稱: [_________]                         │
│  │  代碼: [_________]                         │
│  │  類型: [機關 ▾]                             │
│  │  聯絡人: [________] 電話: [________]        │
│  │  地址: [________________________]           │
│  │  [儲存] [取消] [刪除]                        │
│  └───────────────────────────────────────────  │
└──────────────────────────────────────────────┘
```

**受影響檔案**：
- `web-pcces/frontend/src/pages/admin/OrganizationManage.tsx` — 新增

**預計工時**：2 小時

---

### Step 11 — 前端路由與側邊欄整合

**目標**：將系統維護頁面整合到現有路由與側邊欄中。

**路由整合**：
```tsx
// App.tsx 新增路由
<Route path="admin" element={<AdminPage />} />

// 完整路由順序（放在 MrsBasePage 之後）
```

**側邊欄整合**（AppLayout.tsx）：
```tsx
// 在 menuItems 中加入（僅 admin 使用者顯示）
const isAdmin = user?.role === 'admin';

// 在「公共單價庫」之後加入
if (isAdmin) {
  menuItems.push({ key: 'admin', icon: <SettingOutlined />, label: '系統維護' });
}

// handleMenuClick 中加入
case 'admin': navigate('/app/admin'); break;
```

**權限檢查**：
- AdminPage 元件首次 render 時檢查 user.role !== 'admin' → Navigate to dashboard

**受影響檔案**：
- `web-pcces/frontend/src/App.tsx` — 新增路由
- `web-pcces/frontend/src/components/AppLayout.tsx` — 條件式顯示選單
- `web-pcces/frontend/package.json` — 可能需新增依賴（如 `@ant-design/icons` 的 SettingOutlined 已存在）

**預計工時**：0.5 小時

---

### Step 12 — 種子資料擴充

**目標**：在 `api/seed_data.py` 中新增系統維護相關的起始示範資料。

**新增函數** `seed_sysmaintain_data(db)`：

```python
def seed_sysmaintain_data(db: Session) -> bool:
    """若系統維護資料表為空則建立起始資料"""
    # 1. 系統參數
    if db.query(SystemParameter).count() == 0:
        params = [
            # 分類 E
            ("E", "ORG_NAME", "機關名稱", "工程會", "工程會"),
            ("E", "ORG_CODE", "機關代碼", "12345678", "12345678"),
            ("E", "SYS_TITLE", "系統標題", "PCCES 公共工程經費估算系統", "PCCES 公共工程經費估算系統"),
            # 分類 F
            ("F", "PROFIT_RATE", "包商利潤率", "5", "5"),
            ("F", "TAX_RATE", "營業稅率", "5", "5"),
            ("F", "OVERHEAD_RATE", "間接費用率", "8", "8"),
            # 分類 G
            ("G", "PROJECT_TYPE", "工程分類", "建築工程", "建築工程"),
            ("G", "BUDGET_YEAR", "預算年度", "2025", "2025"),
            ("G", "CURRENCY", "幣別", "TWD", "TWD"),
        ]
        for cat, code, name, value, default in params:
            db.add(SystemParameter(
                category=cat, code=code, c_name=name,
                c_value=value, c_default=default,
                sort_order=1, is_active=True,
            ))

    # 2. 代碼表
    if db.query(CodeTable).count() == 0:
        dept_table = CodeTable(table_code="DEPT", table_name="部門編碼", is_active=True)
        db.add(dept_table)
        db.flush()

        asset_table = CodeTable(table_code="ASSET", table_name="公物編碼", is_active=True)
        db.add(asset_table)
        db.flush()

        # 部門代碼項
        dept_items = [
            ("D001", "工務課", dept_table.id),
            ("D002", "機電課", dept_table.id),
            ("D003", "建築課", dept_table.id),
            ("D004", "秘書室", dept_table.id),
            ("D005", "會計室", dept_table.id),
        ]
        for code, name, tid in dept_items:
            db.add(CodeItem(table_id=tid, code=code, c_name=name, sort_order=1, is_active=True))

        # 公物編碼項
        asset_items = [
            ("PC-001", "個人電腦", asset_table.id),
            ("PRT-001", "印表機", asset_table.id),
            ("FURN-001", "辦公桌", asset_table.id),
            ("FURN-002", "辦公椅", asset_table.id),
        ]
        for code, name, tid in asset_items:
            db.add(CodeItem(table_id=tid, code=code, c_name=name, sort_order=1, is_active=True))

    # 3. 組織機構
    if db.query(Organization).count() == 0:
        root = Organization(code="ROOT", c_name="工程會", org_type="機關", sort_order=1, is_active=True)
        db.add(root)
        db.flush()

        dept1 = Organization(parent_id=root.id, code="DEPT-A", c_name="工務組", org_type="部門", sort_order=1, is_active=True)
        db.add(dept1)
        db.flush()

        sub1 = Organization(parent_id=dept1.id, code="SEC-A1", c_name="道路課", org_type="課室", sort_order=1, is_active=True)
        db.add(sub1)
        sub2 = Organization(parent_id=dept1.id, code="SEC-A2", c_name="建築課", org_type="課室", sort_order=2, is_active=True)
        db.add(sub2)

        dept2 = Organization(parent_id=root.id, code="DEPT-B", c_name="秘書室", org_type="部門", sort_order=2, is_active=True)
        db.add(dept2)
        db.flush()

        sub3 = Organization(parent_id=dept2.id, code="SEC-B1", c_name="文書課", org_type="課室", sort_order=1, is_active=True)
        db.add(sub3)

    db.commit()
    return True
```

並在主種子流程 `seed_demo_data` 中呼叫 `seed_sysmaintain_data`。

**受影響檔案**：
- `api/seed_data.py` — 新增 seed_sysmaintain_data 函數，並在 seed_demo_data 中呼叫

**預計工時**：1 小時

---

### Step 13 — 測試與整合驗證

**目標**：撰寫自動化測試，驗證各 API 端點正確運作。

**測試項目**：
1. 未登入無法存取 admin API → 401
2. 非 admin 使用者無法存取 admin API → 403
3. admin 使用者可正常 CRUD 使用者
4. admin 使用者可管理系統參數
5. admin 使用者可管理代碼表
6. admin 使用者可管理組織機構
7. 種子資料可正確初始化

**測試檔案**：
- `api/test_sysmaintain.py` — 新增

**受影響檔案**：
- `api/test_sysmaintain.py` — 新增

**預計工時**：1 小時

---

## 3. 檔案變更總表

| 步驟 | 檔案 | 動作 | 說明 |
|------|------|------|------|
| 1 | `api/models.py` | 修改 | 新增 SystemParameter、CodeTable、CodeItem、Organization 模型 |
| 2 | `api/index.py` | 修改 | 新增 require_admin + 約 20 個 admin API endpoints |
| 3 | `web-pcces/frontend/src/types.ts` | 修改 | 新增 SysMaintain 相關型別 |
| 4 | `web-pcces/frontend/src/api.ts` | 修改 | 新增 adminApi 物件 |
| 5 | `web-pcces/frontend/src/pages/AdminPage.tsx` | **新增** | 系統維護主頁面（Tabs） |
| 6 | `web-pcces/frontend/src/pages/admin/UserManagement.tsx` | **新增** | 使用者管理列表頁 |
| 6 | `web-pcces/frontend/src/pages/admin/UserEditForm.tsx` | **新增** | 使用者編輯表單 Modal |
| 7 | `web-pcces/frontend/src/pages/admin/RoleManagement.tsx` | **新增** | 角色/權限管理 |
| 8 | `web-pcces/frontend/src/pages/admin/CodeTableManagement.tsx` | **新增** | 代碼表 CRUD 頁面 |
| 9 | `web-pcces/frontend/src/pages/admin/SystemParamsPage.tsx` | **新增** | 系統參數設定頁面 |
| 10 | `web-pcces/frontend/src/pages/admin/OrganizationManage.tsx` | **新增** | 組織機構管理頁面 |
| 11 | `web-pcces/frontend/src/App.tsx` | 修改 | 新增 /app/admin 路由 |
| 11 | `web-pcces/frontend/src/components/AppLayout.tsx` | 修改 | 條件式顯示系統維護選單 |
| 12 | `api/seed_data.py` | 修改 | 新增 seed_sysmaintain_data |
| 13 | `api/test_sysmaintain.py` | **新增** | 自動化測試 |

---

## 4. 技術細節

### 4.1 Admin Only 權限控制

後端使用 `require_admin` 裝飾器（不同於現有的 `require_auth`），邏輯：
1. 解析 JWT token 取得 user_id
2. 查詢使用者角色是否為 admin
3. 非 admin 回傳 403

```python
def require_admin(f):
    @wraps(f)
    def decorated(*args, **kwargs):
        auth = request.headers.get("Authorization", "")
        if not auth.startswith("Bearer "):
            return jsonify({"detail": "未授權"}), 401
        payload = decode_token(auth[7:])
        if not payload:
            return jsonify({"detail": "Token 無效"}), 401
        user_id = int(payload["sub"])
        db = next(get_db())
        try:
            user = db.query(User).filter(User.id == user_id).first()
            if not user or user.role != UserRole.ADMIN.value:
                return jsonify({"detail": "需要管理員權限"}), 403
            kwargs["user_id"] = user_id
            return f(*args, **kwargs)
        finally:
            db.close()
    return decorated
```

### 4.2 前端權限檢查

- AppLayout 中根據 `user?.role === 'admin'` 決定是否顯示系統維護選單
- AdminPage 進入時 double check user role，非 admin 導向 dashboard

### 4.3 樹狀結構處理

- CodeItem 與 Organization 都有 parent_id 自引用結構
- 後端 API 回傳時使用遞迴組裝成巢狀 JSON
- 前端使用 Ant Design Tree 或 Table tree data 顯示

### 4.4 SQLite 相容性

- 所有新模型使用 SQLite 相容的欄位型別（無 Array/Enum 特定語法）
- 使用 JSON 欄位（SQLite 視為 TEXT）儲存 ext_data

### 4.5 Vercel 部署相容性

- 所有新 API 路由統一前綴 `/api/admin/`
- 靜態檔案已由 `serve_frontend` 處理
- 無需額外 serverless 配置變更

---

## 5. 預計總工時

| 步驟 | 內容 | 預計工時 |
|------|------|----------|
| Step 1 | 資料庫模型設計 | 1.5 小時 |
| Step 2 | 後端 API Endpoints | 3 小時 |
| Step 3 | 前端型別定義 | 0.5 小時 |
| Step 4 | 前端 API 層 | 0.5 小時 |
| Step 5 | 系統維護主頁面 | 1 小時 |
| Step 6 | 使用者管理元件 | 2 小時 |
| Step 7 | 角色/權限管理元件 | 1.5 小時 |
| Step 8 | 代碼表管理元件 | 2.5 小時 |
| Step 9 | 系統參數設定元件 | 1.5 小時 |
| Step 10 | 組織機構管理元件 | 2 小時 |
| Step 11 | 路由與側邊欄整合 | 0.5 小時 |
| Step 12 | 種子資料擴充 | 1 小時 |
| Step 13 | 測試與整合驗證 | 1 小時 |
| **總計** | | **18.5 小時** |

---

## 6. 子代理（DEVELOPER）執行順序建議

1. **先後端再前端**：先完成模型與 API，再開發前端頁面
2. **Step 1 優先**：所有功能依賴新模型
3. **Step 2 第二**：API 完成後前端才可串接
4. **Step 3+4 可並行**：與 Step 2 同時進行型別與 api 層開發
5. **Step 5-11 循序**：前端各頁面依賴路由與 API
6. **Step 12 在 Step 1 之後**：種子資料依賴新模型
7. **Step 13 最後**：所有功能完成後撰寫測試
