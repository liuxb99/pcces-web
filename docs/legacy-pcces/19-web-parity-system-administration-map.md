# PCCES Web 系統管理與平台復刻對照

更新日期：2026-08-02

## 1. 對照狀態

| Legacy 功能 | C# 入口 | Web 現況 | 狀態 | 後續復刻重點 |
|---|---|---|---|---|
| 使用者管理 | `FormSys_A`、`FormSys_A_Edit` | 有基本使用者／管理頁 | `PARTIAL` | 密碼政策、停用、刪除、稽核與完整欄位 |
| 群組管理 | `FormSys_A`、`FormSys_A_UsrGroup` | 未確認完整對應 | `NOT_STARTED` | 群組 CRUD 與唯一性 |
| 群組成員 | `FormSys_A_GrpMember` | 未確認 | `NOT_STARTED` | 雙向成員管理 |
| 使用者功能權限 | `FormSys_A`、`DBClass` | 主要為角色判斷 | `PARTIAL` | Function Code 逐項授權 |
| 群組功能權限 | `FormSys_A`、`DBClass` | 未確認 | `NOT_STARTED` | 功能樹與繼承規則 |
| 專案權限 | `ProjAuthority`、`DBClass` | owner/admin 模型 | `PARTIAL` | Legacy Project Authority 與 Action Eligibility |
| 模組授權 | `ModuleManager` | 未確認 | `NOT_STARTED` | Budget/Bid/Common/Invoice entitlement |
| 系統設定 | `FormSys_Z` | 有少量一般設定 | `PARTIAL` | 作用域、預設值、版本、稽核與生效時機 |
| 預算自動保存設定 | `FormSys_Z`、`frmBudget` | 未達 Legacy 行為 | `PARTIAL` | 間隔、清理、恢復與錯誤處理 |
| MRS 載入／匯率設定 | `FormSys_Z` | 未確認 | `NOT_STARTED` | 策略設定與重算影響 |
| Excel 字型／匯出偏好 | `FormSys_Z` | 未確認 | `NOT_STARTED` | 使用者／組織偏好 |
| 報表套件設定 | `FormSys_Z` | 報表 API 雛形 | `PARTIAL` | 報表包版本、路徑與相容性 |
| 資料庫清單 | `FormSys_G` | 單一應用資料庫 | `NOT_STARTED` | 若 Web 採多租戶，改為 Tenant／Organization DB 管理 |
| 建立組織資料庫 | `FormSys_G` | 未實作 | `NOT_STARTED` | Provisioning Job、進度與回滾 |
| 成本結構初始化 | `FormSys_G`、`CostStructureImport` | 未實作 | `NOT_STARTED` | 建庫模板與版本 |
| 資料庫切換 | `FormSys_G` | 未實作 | `NOT_STARTED` | Tenant Context 與安全切換 |
| 資料庫刪除 | `FormSys_G` | 未實作 | `NOT_STARTED` | 保護條件、備份與雙重確認 |
| 資料庫升級 | `DatabaseUpgrade` | SQLAlchemy 建表為主 | `PARTIAL` | 正式 Migration、鎖、進度、失敗恢復 |
| 版本重置／舊版還原 | `FormSys_G` | 未實作 | `NOT_STARTED` | 僅在明確需要 Legacy 相容時復刻 |
| 備份與復原 | `FormSys_Z` | 未確認 | `NOT_STARTED` | 備份版本、驗證、restore drill |
| 線上使用者 | `OnlineList` | 未確認 | `NOT_STARTED` | Presence、active work context、timeout |
| 更新／註冊／Proxy | 主框架與平台類別 | 未完整盤點 | `DISCOVERING` | 先補源碼清單，再決定 Web 對應 |

## 2. 建議 Web 邊界

```text
Identity
├── User
├── Group
├── Membership
└── Credential policy

Authorization
├── Function Code
├── Group grants
├── User overrides
├── Project authority
└── Module entitlement

Settings
├── System scope
├── Organization scope
├── Project scope
└── User scope

Platform Administration
├── Tenant / database provisioning
├── Migration
├── Backup / restore
├── Report package
├── Version
└── Audit
```

## 3. 不應直接照搬的桌面實作

- INI 應轉成具作用域的設定資料，不直接依賴伺服器本機檔案。
- `DBClass` 與 `ModifyDB` 應拆成 Repository、Domain Service 與 Authorization Service。
- 資料庫切換不得依靠全域可變連線字串，必須使用請求／工作上下文。
- 建庫、升級、備份與復原應成為可追蹤 Job，保留狀態、進度、錯誤與操作者。
- 密碼不可沿用可逆或弱式 Legacy 儲存方式，僅保留功能能力，不保留不安全實作。

## 4. 實作優先順序

### P0

1. User / Group / Membership。
2. Function Code + Project Authority + Module Entitlement。
3. Settings scopes 與稽核。
4. Migration 基礎。

### P1

1. Tenant／組織資料庫 provisioning。
2. 備份與復原。
3. Presence／OnlineList。
4. 報表套件版本。

### P2

1. 舊版 Restore 103 等歷史維修工具。
2. Legacy Proxy 與桌面更新器的 Web 對應。

## 5. 驗收原則

- 每個 Web 管理功能必須反查 Legacy 節點與 C# 來源。
- 權限驗收必須同時測試群組授權、使用者覆寫、專案資格與模組開關。
- Migration／備份／復原必須有失敗案例與可恢復證據。
- 設定變更必須記錄 old value、new value、scope、operator、timestamp。
