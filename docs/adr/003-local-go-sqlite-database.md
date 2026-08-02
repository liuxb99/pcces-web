# ADR-003：本地 Go 版固定使用 SQLite

日期：2026-08-02
狀態：Accepted

## 背景

PCCES C# 將同步復刻為 Web 版與本地 Go 版。本地 Go 版的主要目標是可攜、離線、低維運成本，且不依賴外部資料庫服務。

## 決策

PCCES Local Go 的唯一正式本地資料庫固定採用 SQLite。

- 不將 PostgreSQL、SQL Server 或其他資料庫設為本地執行依賴。
- SQLite migration、外鍵、索引、交易、備份、復原及完整性檢查均列入正式功能。
- Web 版可使用 PostgreSQL，但 Web／Go 必須維持相同 Domain 語意與 Golden Fixture 結果。
- Web 與 Local Go 的資料交換或同步須透過正式 adapter／export-import contract，不以共用資料庫連線取代。

## SQLite 實作要求

1. 預設啟用 foreign keys。
2. 採 WAL 或經測試核准的等效 journal policy。
3. 設定 busy timeout，明確處理 single-writer contention。
4. 所有 migration 可從空資料庫建庫，也可從既有版本逐版升級。
5. 金額、數量、單價與費率不得以 binary float 作正式儲存。
6. 必須提供 online backup、restore、integrity check 與損毀檔案錯誤處理。
7. 本地草稿、Autosave 與 WorkContext 必須具備 crash recovery。
8. 整合測試與 Golden Tests 必須使用真正 SQLite，不得只測 mock repository。

## 後果

### 正面

- 使用者不需安裝或維護資料庫伺服器。
- 易於單機、可攜式與離線部署。
- 備份可圍繞單一資料檔與附件目錄設計。
- Go 版可提供一致的 Windows／Linux 體驗。

### 代價

- Web PostgreSQL 與 Local SQLite 需要明確 schema mapping。
- Decimal、併發、全文搜尋及大型批次寫入需制定 SQLite 專用策略。
- 不可直接照搬只適用 PostgreSQL 的型別、鎖定或 SQL。

## 驗收

- Local Go 在沒有 PostgreSQL／SQL Server 的環境可完成建庫、執行、升級、備份及復原。
- 相同 Golden Fixture 在 Web/PostgreSQL 與 Local Go/SQLite 產生相同 Domain 結果。
- SQLite migration、foreign key、transaction rollback、busy handling、backup／restore 與 integrity tests 進入 CI。
