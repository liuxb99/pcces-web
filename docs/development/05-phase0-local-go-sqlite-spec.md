# Phase 0 Local Go／SQLite 平台規格

更新日期：2026-08-02

## 1. 目標

建立可離線執行的 PCCES Local Go 基礎。SQLite 是唯一正式本地資料庫，不是暫存方案。

## 2. 建議目錄

```text
pcces-go/
  cmd/pcces/
  cmd/pcces-server/
  internal/domain/
  internal/application/
  internal/storage/sqlite/
  internal/platform/
  internal/importexport/
  internal/report/
  migrations/
  tests/golden/
```

第一階段至少提供：

- `pcces` CLI。
- `pcces-server` localhost API。
- Windows／Linux build。
- SQLite migration。
- 本地 config、logging、error model。

## 3. SQLite 連線設定

每次連線必須確認：

```sql
PRAGMA foreign_keys = ON;
PRAGMA journal_mode = WAL;
PRAGMA busy_timeout = 5000;
```

建議另外設定：

```sql
PRAGMA synchronous = NORMAL;
PRAGMA temp_store = MEMORY;
```

設定值需可由測試驗證，不可只寫在文件。

## 4. SQLite Schema

Phase 0 表與 Web 對應：

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
local_drafts
backup_history
```

主鍵可使用 UUID text 或 binary representation，但對外契約須保持一致。

## 5. Decimal 儲存

SQLite 不使用 REAL 儲存正式 Decimal。

採固定縮放整數：

```text
quantity_value INTEGER + quantity_scale INTEGER
unit_price_value INTEGER + unit_price_scale INTEGER
amount_value INTEGER + amount_scale INTEGER
rate_value INTEGER + rate_scale INTEGER
```

例如：

```text
1234.5678 → value=12345678, scale=4
```

Go Domain 使用 decimal library 或自建不可變 Decimal Value Object，不允許以 `float64` 作為正式計算途徑。

## 6. Migration

Migration 檔案使用順序版本：

```text
000001_phase0_catalog.up.sql
000002_phase0_auth.up.sql
000003_phase0_context.up.sql
```

要求：

- migration table。
- 全新資料庫建立測試。
- 升級測試。
- 中途中斷恢復。
- seed 可重複執行。
- schema version 可由 CLI 查詢。

CLI：

```text
pcces db migrate
pcces db version
pcces db integrity-check
pcces db backup
pcces db restore
```

## 7. Transaction

Application Service 控制 transaction：

```text
Begin
→ validate
→ authorize
→ mutate
→ audit
→ commit
```

任一步失敗必須 rollback。

長時間匯入不得持有不必要的全程寫鎖；應先在 staging/session 中驗證，再以短 transaction commit。

## 8. Busy 與併發

SQLite 單寫者限制必須轉成結構化錯誤：

```text
PCCES.STORAGE.BUSY
```

策略：

- busy timeout。
- 有界重試。
- idempotency key。
- 寫入批次化。
- 不在 UI thread 長時間阻塞。

## 9. WorkContext 與本地草稿

`local_drafts` 至少包含：

```text
draft_id
context_id
feature_id
project_code
payload
base_row_version
saved_at
recovery_state
```

應用程式異常終止後，下次啟動必須能列出可恢復草稿。

CLI／Local API：

```text
pcces context current
pcces context save
pcces context discard
pcces context recover
```

## 10. Local Authorization

Local Go 即使單機使用，也必須保留：

- user identity
- group
- function code
- module entitlement
- project authorization
- audit actor

不可因為是本機版而刪除 Legacy 權限語意。

可提供 single-user mode，但其本質是預設建立一個本機管理員，不是繞過授權層。

## 11. Backup／Restore

備份必須使用 SQLite 安全機制，不可在寫入中直接複製 DB 檔。

備份內容：

```text
database.sqlite
attachments/
reports/
manifest.json
checksums.json
```

Manifest 包含：

```text
app_version
schema_version
created_at
database_checksum
attachment_count
```

Restore 前需：

- checksum 驗證。
- schema compatibility 驗證。
- 現有資料安全備份。
- restore 後 integrity check。

## 12. Integrity Check

啟動時可執行快速檢查；管理功能提供完整：

```sql
PRAGMA quick_check;
PRAGMA integrity_check;
PRAGMA foreign_key_check;
```

失敗時進入保護模式，禁止繼續寫入。

## 13. Local API

localhost API 與 Web API 共用 DTO、Error、Decimal 契約，但不要求路由完全一致。

第一批：

```text
GET  /health
GET  /version
GET  /features
GET  /capabilities
POST /work-contexts
POST /work-contexts/{id}/save
POST /work-contexts/{id}/discard
POST /work-contexts/{id}/recover
```

預設只監聽 loopback，外部監聽需明確設定。

## 14. 測試

最低要求：

```text
SQLite real-database integration test
foreign-key enforcement test
WAL setting test
busy timeout test
migration empty-db test
migration upgrade test
decimal fixed-scale test
transaction rollback test
crash recovery draft test
backup restore test
integrity failure protection test
golden contract test
```

## 15. Build

至少驗證：

```text
GOOS=windows
GOOS=linux
```

建議後續支援 amd64、arm64。不得要求使用者安裝外部 PostgreSQL 或 SQL Server。

## 16. 完成條件

- `pcces-go` 可建立並啟動 SQLite。
- Migration、Catalog、Auth、Action、WorkContext 可用。
- Decimal 不落入 REAL。
- WAL、foreign key、busy timeout 有測試。
- Backup／Restore／Integrity Check 可執行。
- Shared Golden Fixtures 與 Web 結果一致。
