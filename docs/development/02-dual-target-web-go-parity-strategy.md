# PCCES C# 雙目標復刻策略：Web 版＋本地 Go 版

更新日期：2026-08-02

## 1. 最終目標

PCCES 的復刻不再只有 Web 版，而是由同一套 Legacy 規格同步產出兩個正式實作：

1. **PCCES Web**：瀏覽器使用的多人、集中式部署版本。
2. **PCCES Local Go**：可在 Windows／Linux 本機執行的 Go 桌面或本地服務版本。

兩個版本都必須以 PCCES C# 桌面版為行為基準，完整復刻功能、業務規則、計算、資料交換、權限、狀態轉換與報表能力。

```text
PCCES C# Legacy
      ↓
Legacy Feature Tree／Source Index／Detailed Spec
      ↓
Shared Domain Contract／Data Contract／Golden Fixtures
      ├── PCCES Web
      └── PCCES Local Go
```

## 2. 核心原則

### 2.1 一套規格，兩個實作

每個 Feature ID 只能有一份正式 Legacy 規格，但必須同時記錄：

- Web 實作位置與狀態。
- Local Go 實作位置與狀態。
- 共用測例與 Golden Fixture。
- Web／Go／Legacy 三方差異。

不得各自重新理解 C#，避免兩套產品產生不同業務規則。

### 2.2 Domain-first

優先抽出可跨實作共享的契約：

- Feature ID。
- Command／Query。
- 狀態機。
- 欄位與驗證規則。
- Decimal 與精度政策。
- 計算 Trace。
- 檔案格式與 Schema。
- Structured Error。
- Golden Fixture。

Web 與 Go 可以有不同 UI、儲存方式與部署模式，但 Domain 結果必須一致。

### 2.3 Go 版不是 Web API 的簡化包裝

Local Go 必須能在沒有 Web 伺服器的情況下完成主要工作：

- 本地專案資料庫。
- 預算、工料機、契約及履約計算。
- Legacy 檔案匯入匯出。
- 報表輸出。
- 本地備份與復原。
- 可選擇單機 UI、CLI 或 localhost UI。

### 2.4 可共享資料，不能共享錯誤假設

Web 與 Go 應共用：

- OpenAPI／JSON Schema 或等效資料契約。
- SQLite／PostgreSQL 可對映 Schema。
- Legacy Code Catalog。
- Calculation fixtures。
- Import／export test files。
- Report snapshot inputs。

但不得因 Web 已有某個欄位或 API，就假設它等同 Legacy 規則。

## 3. 建議架構

```text
/specs
  feature-catalog
  schemas
  calculation-rules
  golden-fixtures
  report-contracts

/web-pcces
  frontend
  backend

/pcces-go
  cmd/pcces
  cmd/pcces-server
  internal/domain
  internal/application
  internal/storage
  internal/importexport
  internal/report
  internal/platform
  migrations
```

### Web 版

- React／TypeScript UI。
- 正式 API 後端。
- PostgreSQL 為主要生產資料庫。
- 多使用者、集中權限、線上工作上下文。

### Local Go 版

- Go Domain 與 Application Service。
- SQLite 為預設本地資料庫，必要時可連 PostgreSQL。
- CLI＋本地 HTTP API 作為第一層穩定介面。
- 桌面 UI 可採 Wails 或其他 Go-compatible shell，但 UI 技術不得反向綁死 Domain。
- 可單檔或可攜式部署，支援離線工作。

## 4. 每個 Phase 的雙軌交付物

每個 Phase 不再只有 Database／Backend／Frontend，而是必須交付：

### Shared Contract

- Legacy Feature ID 與詳細規格。
- Data／API／Command／Error schema。
- Golden fixtures。
- Legacy matching cases。

### Web Track

- Migration。
- Backend domain／service／repository／API。
- React UI。
- Web unit／integration／E2E tests。

### Go Track

- Go domain／application／storage。
- SQLite migration。
- CLI 或 local API。
- Local UI（該 Phase 需要時）。
- Go unit／integration／golden tests。

### Cross-target Verification

同一筆輸入必須比對：

```text
Legacy C# result
Web result
Local Go result
```

三者在允許差異外應完全一致。任何允許差異必須有 ADR。

## 5. Phase 0 的新增工作

Phase 0 必須同步建立 Go 基礎，不可等 Web 全部完成後才補：

### P0-G1：Go Workspace 與執行入口

- 建立 `pcces-go`。
- Go module、config、logging、error model。
- `pcces` CLI。
- `pcces-server` localhost API。
- Windows／Linux build。

### P0-G2：本地資料庫與 Migration

- SQLite schema。
- Decimal 儲存政策。
- row version、audit、transaction。
- 與 Web PostgreSQL schema 的欄位對映測試。

### P0-G3：Function Code／Module／Action

- Go catalog types。
- Local authorization policy。
- Action eligibility。
- 可由 CLI／local API 查詢 capability。

### P0-G4：WorkContext

- 本地 current project／action／resource context。
- save／discard／cancel。
- crash recovery 與 local draft。

## 6. 狀態定義

每個 Feature ID 必須分開記錄：

```text
WEB_STATUS
GO_STATUS
CROSS_TARGET_STATUS
```

允許值：

- `NOT_STARTED`
- `LEGACY_DEEP_REVIEW`
- `SPEC_READY`
- `IMPLEMENTING`
- `INTEGRATION_TESTING`
- `LEGACY_MATCHING`
- `VERIFIED`

只有 Web 與 Go 都達到 `VERIFIED`，且 cross-target golden tests 通過，該 Feature 才能宣稱完整復刻完成。

## 7. 完成標準

整個專案的「完整復刻」需同時滿足：

1. Legacy Feature Tree 100% 可追溯。
2. Web 版全部 P0 Feature 達到 `VERIFIED`。
3. Local Go 版全部 P0 Feature 達到 `VERIFIED`。
4. Web 與 Go 使用相同 Feature ID、規格、錯誤碼與 Golden Fixture。
5. 計算、狀態、匯入匯出與報表通過 Legacy／Web／Go 三方對照。
6. Web 可正式部署；Go 可離線安裝與執行。
7. 不允許以「Web 已完成，所以 Go 可以之後再說」作為 Phase 完成判斷。

## 8. 開發順序

```text
Legacy 局部深讀
→ Shared Contract
→ Web Domain／API
→ Go Domain／Local API
→ Web UI／Go Local UI
→ Shared Golden Tests
→ Legacy 三方對照
→ 同步更新 Traceability
```

Web 與 Go 可以在同一 Phase 內前後錯開，但不得跨 Phase 長期失衡。建議任何 Phase 的兩條實作進度差距不得超過一個 Segment。
