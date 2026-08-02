# Phase 0 Shared Contract 規格

更新日期：2026-08-02

## 1. 目的

本文件定義 PCCES Web 與 PCCES Local Go 在 Phase 0 必須共用的正式契約。兩個實作可以使用不同語言、框架與資料庫，但不得各自重新定義業務語意。

```text
PCCES C# Legacy
      ↓
Shared Contract
      ├── Web / PostgreSQL
      └── Local Go / SQLite
```

## 2. 共用識別

所有核心資料必須同時具有：

- `id`：實作內部識別，可使用 UUID 或其他穩定值。
- `legacy_code`：Legacy 穩定代碼，例如 `projectCode`、Function Code。
- `feature_id`：對應 Legacy Feature Tree。
- `row_version`：樂觀鎖定版本。
- `created_at`、`updated_at`。
- `created_by`、`updated_by`。

不得以內部 `id` 取代 Legacy 業務代碼。

## 3. Feature Catalog

每個 Feature 至少包含：

```text
feature_id
legacy_module
legacy_function_code
name
parent_feature_id
required_module
required_action
web_status
go_status
cross_target_status
source_files
```

狀態值統一為：

- `NOT_STARTED`
- `LEGACY_DEEP_REVIEW`
- `SPEC_READY`
- `IMPLEMENTING`
- `INTEGRATION_TESTING`
- `LEGACY_MATCHING`
- `VERIFIED`

## 4. Module Catalog

Phase 0 固定四個 Legacy Module：

| Code | 名稱 |
|---|---|
| `BUDGET` | 預算模組 |
| `BID` | 投標模組 |
| `COMMON` | 共用模組 |
| `INVOICE` | 履約模組 |

必要欄位：

```text
module_code
name
enabled
source
valid_from
valid_to
```

Disabled Module 必須同時阻擋 UI、API、CLI 與 Local API。

## 5. Function Code Catalog

第一批固定納入：

```text
F001
F0010007
F002
F003
F004
F005
F00500010002
F006
F007
F008
F009
F010
F011
F012
```

授權結果不是單純角色判斷，而是：

```text
module enabled
AND user/group has function code
AND action preconditions satisfied
AND project capability satisfied
```

## 6. Action Catalog

第一批 Action：

```text
BUD
BID
SplitContract
Invoice
BudgetChange
SubClose
SubFinal
ProjectCatalog
SystemMaintain
MrsBase
Report
```

每個 Action 必須定義：

- required module
- required function code
- project required
- eligible project rule
- work context type
- dirty-state policy
- target feature id

## 7. WorkContext Contract

共用欄位：

```text
context_id
user_id
action_code
project_code
resource_type
resource_id
state
is_dirty
row_version
last_saved_at
last_activity_at
recovery_token
```

狀態：

```text
OPEN
DIRTY
SAVING
SAVED
CONFLICT
DISCARDED
CLOSED
RECOVERABLE
```

切換規則：

```text
clean context → direct switch

dirty context → save / discard / cancel

conflict → reload / merge / cancel
```

Web 與 Go 必須使用相同狀態名稱與錯誤碼。

## 8. Command / Query Contract

Command 必須具有：

```text
command_id
feature_id
actor_id
occurred_at
idempotency_key
expected_row_version
payload
```

Query 必須具有：

```text
feature_id
actor_id
filters
sort
page
page_size
```

所有 mutating command 必須支援：

- authorization
- validation
- transaction
- idempotency
- audit
- structured error

## 9. Structured Error

統一格式：

```json
{
  "error": {
    "code": "PCCES.AUTH.FUNCTION_DENIED",
    "message": "目前使用者無此功能權限",
    "featureId": "AUTHZ-F009",
    "field": null,
    "details": {},
    "retryable": false,
    "traceId": "..."
  }
}
```

第一批錯誤碼：

```text
PCCES.AUTH.UNAUTHENTICATED
PCCES.AUTH.FUNCTION_DENIED
PCCES.MODULE.DISABLED
PCCES.ACTION.NOT_ELIGIBLE
PCCES.PROJECT.NOT_FOUND
PCCES.PROJECT.NOT_AUTHORIZED
PCCES.CONTEXT.DIRTY
PCCES.CONTEXT.CONFLICT
PCCES.CONCURRENCY.VERSION_MISMATCH
PCCES.VALIDATION.INVALID_FIELD
PCCES.TRANSACTION.FAILED
PCCES.STORAGE.BUSY
PCCES.STORAGE.INTEGRITY_FAILED
```

## 10. Decimal Contract

正式計算禁止使用 binary float。

共用邏輯型別：

```text
Quantity
UnitPrice
Amount
Rate
Percentage
ExchangeRate
```

每個值必須明確攜帶：

```text
value
scale
rounding_mode
source
```

JSON 一律以字串傳送 Decimal：

```json
{
  "amount": "123456.78"
}
```

禁止 JSON number 作為正式金額契約。

## 11. Audit Contract

所有資料異動至少記錄：

```text
audit_id
feature_id
action_code
actor_id
project_code
resource_type
resource_id
before_snapshot
after_snapshot
occurred_at
trace_id
```

Web 與 Go 均需保留同一欄位語意。

## 12. Golden Fixture Contract

Fixture 目錄建議：

```text
specs/golden-fixtures/
  phase0/
    auth/
    module/
    action/
    work-context/
    decimal/
    error/
```

每個 Fixture 包含：

```text
input.json
expected.json
legacy-evidence.md
web-result.json
go-result.json
```

## 13. Phase 0 Shared Contract 完成條件

- Feature／Module／Function／Action catalog schema 固定。
- WorkContext 狀態機固定。
- Structured Error 固定。
- Decimal JSON 規則固定。
- Web 與 Go 均有契約測試。
- Golden Fixture 可在兩條實作重複執行。
