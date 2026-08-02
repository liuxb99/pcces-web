# Phase 0 Cross-target Verification 與 Schema Mapping

更新日期：2026-08-02

## 1. 目的

確保 PCCES Web／PostgreSQL 與 PCCES Local Go／SQLite 雖採不同技術棧，仍產生相同 Domain 結果。

## 2. 三方對照

每個 Legacy Feature 必須比對：

```text
Legacy C#
Web / PostgreSQL
Local Go / SQLite
```

驗證分四層：

1. Schema Contract。
2. Command／Query Contract。
3. Domain Result。
4. Report／File Output。

## 3. Schema Mapping 原則

| Logical Type | PostgreSQL | SQLite | JSON |
|---|---|---|---|
| Identifier | UUID | TEXT/BLOB | string |
| Legacy Code | VARCHAR/TEXT | TEXT | string |
| Decimal | NUMERIC(p,s) | scaled INTEGER + scale | decimal string |
| Timestamp | TIMESTAMPTZ | UTC ISO-8601 TEXT 或 epoch integer | RFC3339 string |
| Boolean | BOOLEAN | INTEGER CHECK(0,1) | boolean |
| JSON Snapshot | JSONB | TEXT with JSON validation | object |
| Row Version | BIGINT | INTEGER | integer string/number |

禁止把 PostgreSQL NUMERIC 在 SQLite 對映為 REAL。

## 4. Canonical DTO

跨目標測試先將兩邊輸出正規化：

- 欄位排序。
- Timestamp 轉 UTC RFC3339。
- Decimal 轉 canonical string。
- 空集合固定 `[]`。
- 缺值與 null 不混用。
- UUID 大小寫正規化。
- 不比較 traceId、runtime timing 等非決定性欄位。

## 5. Golden Fixture 結構

```text
specs/golden-fixtures/<phase>/<feature-id>/<case-id>/
  input.json
  expected.json
  legacy-evidence.md
  web-result.json
  go-result.json
  diff.json
```

`expected.json` 是正式期望，不得以某一次 Web 輸出直接覆蓋。

## 6. Fixture Metadata

每個案例至少包含：

```json
{
  "featureId": "AUTHZ-F009",
  "caseId": "missing-function-code",
  "legacyConfidence": "CONFIRMED",
  "allowedDifferences": [],
  "sourceFiles": [],
  "createdAt": "2026-08-02T00:00:00Z"
}
```

## 7. Phase 0 Golden Cases

### Authentication

- valid login。
- invalid password。
- disabled user。
- anonymous／single-user compatibility decision。

### Authorization

- module disabled。
- missing Function Code。
- group inherited Function Code。
- project unauthorized。

### Action Eligibility

- project required but absent。
- project not eligible。
- eligible action returns capability。

### WorkContext

- create clean context。
- dirty switch requires decision。
- save then switch。
- discard then switch。
- cancel preserves source context。
- row version conflict。
- recover local draft。

### Decimal

- positive value。
- negative value。
- trailing zero normalization。
- maximum supported precision。
- rounding boundary。

### Error

- identical error code。
- identical feature ID。
- retryable semantics identical。

## 8. Cross-target Test Runner

建議提供統一命令：

```text
make golden-phase0
```

流程：

```text
seed fixture
→ run Web case
→ run Go case
→ normalize
→ compare expected
→ write diff
```

CI 中任一 target 缺少結果即失敗。

## 9. 資料庫 Seed

Web 與 Go 使用同一份 logical seed，例如：

```text
specs/seeds/phase0/catalog.json
specs/seeds/phase0/users.json
specs/seeds/phase0/permissions.json
```

各自 adapter 寫入 PostgreSQL／SQLite，不維護兩份手工 SQL 業務資料。

## 10. Migration Parity

每個 Phase 必須有 Logical Schema Version：

```text
phase0.schema.v1
```

Web Migration 與 Go Migration 均宣告對應版本。驗證工具檢查：

- 必要 table 存在。
- logical columns 存在。
- unique／foreign key 語意一致。
- catalog seed 一致。

不要求資料庫 DDL 字面相同。

## 11. 允許差異

僅允許：

- 資料庫內部 ID 表現。
- PostgreSQL／SQLite 特有索引與儲存優化。
- Web session 與 local process metadata。
- UI 排版。

任何 Domain、計算、權限、狀態或輸出差異必須有 ADR。

## 12. Diff 分類

```text
SCHEMA_DIFF
VALUE_DIFF
ROUNDING_DIFF
STATE_DIFF
AUTH_DIFF
ERROR_DIFF
ORDER_DIFF
OUTPUT_FORMAT_DIFF
NONDETERMINISTIC_DIFF
```

不得把 rounding 或 authorization 差異標成可忽略。

## 13. CI Gate

Phase 0 CI 至少包含：

```text
web migration + tests
go migration + tests
schema parity
contract validation
golden phase0
direct bypass security tests
backup/restore tests for Go
```

## 14. 完成條件

- Web／Go 使用同一 logical seed。
- Schema Mapping 文件與 Migration 一致。
- Phase 0 Golden Cases 全部通過。
- Decimal、權限、Action、WorkContext 無未決差異。
- 所有允許差異都有明確清單或 ADR。
