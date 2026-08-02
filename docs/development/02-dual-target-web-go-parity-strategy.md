# PCCES C# 雙目標復刻策略：Web 版＋本地 Go 版

更新日期：2026-08-02

## 1. 最終目標

PCCES 的復刻不再只有 Web 版，而是由同一套 Legacy 規格同步產出兩個正式實作：

1. **PCCES Web**：瀏覽器使用的多人、集中式部署版本。
2. **PCCES Local Go**：可在 Windows／Linux 本機執行的 Go 桌面或本地服務版本，正式資料庫固定使用 SQLite。

兩個版本都必須以 PCCES C# 桌面版為行為基準，完整復刻功能、業務規則、計算、資料交換、權限、狀態轉換與報表能力。

```text
PCCES C# Legacy
      ↓
Legacy Feature Tree／Source Index／Detailed Spec
      ↓
Shared Domain Contract／Data Contract／Golden Fixtures
      ├── PCCES Web（PostgreSQL）
      └── PCCES Local Go（SQLite）
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

Local Go 必須能在沒有 Web 伺服器與 PostgreSQL 的情況下完成主要工作：

- 使用 SQLite 保存完整本地專案資料。
- 預算、工料機、契約及履約計算。
- Legacy 檔案匯入匯出。
- 報表輸出。
- 本地備份與復原。
- 可選擇單機 UI、CLI 或 localhost UI。

### 2.4 Local Go 資料庫固定為 SQLite

這是不可回退的架構決策：

- Local Go 正式執行時只依賴 SQLite。
- 不要求安裝 PostgreSQL、SQL Server 或其他外部資料庫。
- SQLite schema、migration、索引、外鍵、transaction、WAL、backup 與 integrity check 都是正式產品能力。
- 可提供資料交換或同步工具與 Web PostgreSQL 對接，但不能把 PostgreSQL 當成本地 Go 執行依賴。
- 所有 Local Go 測試必須在 SQLite 上執行，不得只用 mock repository 取代。

### 2.5 可共享資料，不能共享錯誤假設

Web 與 Go 應共用：

- OpenAPI／JSON Schema 或等效資料契約。
- SQLite／PostgreSQL 欄位與語意對映規格。
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
  internal/storage/sqlite
  internal/importexport
  internal/report
  internal/platform
  migrations/sqlite
```

### Web 版

- React／TypeScript UI。
- 正式 API 後端。
- PostgreSQL 為主要生產資料庫。
- 多使用者、集中權限、線上工作上下文。

### Local Go 版

- Go Domain 與 Application Service。
- SQLite 為唯一正式本地資料庫。
- CLI＋本地 HTTP API 作為第一層穩定介面。
- 桌面 UI 可採 Wails 或其他 Go-compatible shell，但 UI 技術不得反向綁死 Domain。
- 可單檔或可攜式部署，支援完全離線工作。
- 預設啟用 SQLite foreign keys；正式模式採 WAL 或經驗證的等效 journal policy。
- 金額、數量、單價與費率不得使用 SQLite binary float 作正式儲存；應使用可逆的定點整數、decimal string 或經 ADR 核准的 Numeric codec。

## 4. 每個 Phase 的雙軌交付物

每個 Phase 不再只有 Database／Backend／Frontend，而是必須交付：

### Shared Contract

- Legacy Feature ID 與詳細規格。
- Data／API／Command／Error schema。
- Golden fixtures。
- Legacy matching cases。

### Web Track

- PostgreSQL Migration。
- Backend domain／service／repository／API。
- React UI。
- Web unit／integration／E2E tests。

### Go Track

- Go domain／application／SQLite storage。
- SQLite migration。
- CLI 或 local API。
- Local UI（該 Phase 需要時）。
- Go unit／integration／golden tests。
- SQLite backup／restore／integrity tests。

### Cross-target Verification

同一筆輸入必須比對：

```text
Legacy C# result
Web/PostgreSQL result
Local Go/SQLite result
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

### P0-G2：SQLite 本地資料庫與 Migration

- 建立唯一正式 SQLite schema。
- SQLite migration runner 與 schema version table。
- Decimal／Numeric codec 與跨平台一致性測試。
- foreign keys、unique constraints、indexes、row version、audit、transaction。
- WAL、busy timeout、single-writer conflict 與 crash recovery policy。
- online backup、restore、integrity check 與 damaged-file handling。
- 與 Web PostgreSQL schema 的欄位和 Domain 語意對映測試。

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
4. Local Go 可只憑執行檔、SQLite 資料檔及必要資源完全離線運行。
5. Web 與 Go 使用相同 Feature ID、規格、錯誤碼與 Golden Fixture。
6. 計算、狀態、匯入匯出與報表通過 Legacy／Web／Go 三方對照。
7. Web 可正式部署；Go 可離線安裝、備份、復原與執行。
8. 不允許以「Web 已完成，所以 Go 可以之後再說」作為 Phase 完成判斷。

## 8. 開發順序

```text
Legacy 局部深讀
→ Shared Contract
→ Web Domain／PostgreSQL／API
→ Go Domain／SQLite／Local API
→ Web UI／Go Local UI
→ Shared Golden Tests
→ Legacy 三方對照
→ 同步更新 Traceability
```

Web 與 Go 可以在同一 Phase 內前後錯開，但不得跨 Phase 長期失衡。建議任何 Phase 的兩條實作進度差距不得超過一個 Segment。
