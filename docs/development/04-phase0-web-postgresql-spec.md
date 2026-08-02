# Phase 0 Web／PostgreSQL 平台規格

更新日期：2026-08-02

## 1. 目標

建立 PCCES Web 唯一正式後端與 PostgreSQL 資料基礎，作為後續全部 Phase 的集中式、多使用者平台。

## 2. 唯一後端

正式程式只能保留一個 Application Entrypoint，統一管理：

- config
- database session
- authentication
- authorization
- error handler
- audit
- OpenAPI
- health check
- migration version

根目錄 `/api` 與 `web-pcces/backend` 不得再維持兩套獨立 Domain 實作。

## 3. API Path

正式路徑：

```text
/api/v1/...
```

Axios 或其他 client 的 base URL 與 endpoint path 必須避免重複 `/api/api`。

建議：

```text
baseURL = /api/v1
endpoint = /projects
```

## 4. 分層

```text
app/
  api/
  application/
  domain/
  repositories/
  models/
  schemas/
  infrastructure/
  migrations/
```

限制：

- Route 不得直接寫核心計算。
- ORM Model 不等同 Domain Entity。
- Repository 不做授權決策。
- Service 必須控制 transaction boundary。

## 5. PostgreSQL 基礎表

Phase 0 至少建立：

```text
users
groups
group_members
features
modules
function_codes
user_function_codes
group_function_codes
actions
work_contexts
audit_events
idempotency_records
schema_versions
```

共通欄位：

```text
id UUID PRIMARY KEY
created_at TIMESTAMPTZ
updated_at TIMESTAMPTZ
row_version BIGINT
```

## 6. Decimal

PostgreSQL 使用 `NUMERIC(p,s)`，不得以 `REAL`、`DOUBLE PRECISION` 作為正式金額或精度敏感資料。

初始建議：

```text
quantity NUMERIC(28, 8)
unit_price NUMERIC(28, 8)
amount NUMERIC(28, 2)
rate NUMERIC(18, 8)
percentage NUMERIC(18, 8)
```

實際 scale 仍需依 Legacy 專案精度政策覆寫。

## 7. Migration

Migration 必須支援：

- 空資料庫建立。
- 現有 SQLite 開發資料轉入策略。
- PostgreSQL 升級。
- catalog seed。
- rollback 或 forward-fix 說明。

每個 Migration 需有：

```text
revision
feature_id
up
verification
down_or_forward_fix
```

## 8. Authentication 與 Authorization

JWT 只處理身分，功能授權另行判斷：

```text
authenticated
→ module enabled
→ function code
→ action eligibility
→ project capability
```

所有授權必須在 API 端重做，不能只依賴 React 隱藏按鈕。

## 9. WorkContext API

第一批 endpoint：

```text
POST   /work-contexts
GET    /work-contexts/current
PATCH  /work-contexts/{id}
POST   /work-contexts/{id}/save
POST   /work-contexts/{id}/discard
POST   /work-contexts/{id}/close
POST   /work-contexts/{id}/recover
```

衝突回應使用 `409 Conflict`，並包含 current row version。

## 10. Catalog API

```text
GET /features
GET /modules
GET /function-codes
GET /actions
GET /capabilities/current
```

Catalog 寫入僅允許系統管理功能與 Migration Seed。

## 11. Idempotency

匯入、建立專案、分拆、重算與報表 Job 等命令必須支援 `Idempotency-Key`。

資料表至少保存：

```text
key
actor_id
command_type
request_hash
response_snapshot
status
expires_at
```

## 12. Audit

Audit 必須在 transaction 成功時提交；失敗的操作另記 operation log，但不得產生成功異動 Audit。

## 13. OpenAPI

- OpenAPI 為唯一 API 契約。
- Decimal 欄位宣告為 string＋format decimal。
- Error 使用統一 schema。
- 前端型別由 OpenAPI 生成或以 contract test 保護。

## 14. 測試

最低要求：

```text
migration empty-db test
catalog seed test
authentication test
function-code denial test
module bypass test
action eligibility test
work-context conflict test
idempotency replay test
decimal round-trip test
audit atomicity test
```

## 15. 完成條件

- 唯一後端入口。
- `/api/v1` 契約一致。
- PostgreSQL Migration 可從空庫執行。
- 權限無法由直接 URL 或 API 繞過。
- Decimal 無 binary float 落庫。
- OpenAPI、contract test 與前端 client 一致。
