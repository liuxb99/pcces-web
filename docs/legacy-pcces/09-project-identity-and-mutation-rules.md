# PCCES 專案身分與異動規則基線

更新日期：2026-08-02

## 1. 目的

本文件整理目前可由 `FormProject` 與 `formNewProjectWizard` 直接確認的專案身分、可刪除能力、模板、授權與異動邊界。精確刪除 SQL、級聯範圍及屬性儲存方法尚未完整取得，相關內容明確標記為待追蹤。

## 2. 專案身分不是單一資料庫 ID

桌面版至少同時使用：

- `projectCode`
- `projectCodeAlias`
- `mainProj`
- `F_OldProjectCode`
- `F_NewProjectCode`
- `F_SubProjectCode`

因此 Web 的內部 UUID 可以保留，但不能取代 Legacy 專案碼。完整復刻必須提供穩定且可查詢的 `projectCode`，並保留 alias、主子專案關係與來源碼。

## 3. 專案資料能力

`FormProject` 的隱藏欄位證明專案清單同時承載以下能力：

```text
IsBud
IsBid
IsCNT
IsCanDelete
Template
BudEst
BudQuote
IsBudEst
IsBudQuote
BudEstAuth
BudQuoteAuth
Auth
```

這些不是純 UI 欄位，而是後續操作資格的輸入。Web API 應以 server-side capability response 提供，不能由前端依是否有子資料自行猜測。

## 4. 可刪除能力

桌面版清單直接取得 `IsCanDelete`，表示刪除資格已由 Domain／查詢層計算。Web 不得只以「目前使用者是 owner」決定是否可刪除。

刪除前至少需要考慮：

- 使用者是否有專案權限。
- 專案是否為模板。
- 是否存在預算資料。
- 是否存在投標資料。
- 是否存在契約或履約資料。
- 是否為主專案或子專案。
- 是否正被其他使用者／工作上下文使用。
- 是否為最近使用中的 BUD/BID/CNT 專案。

上述項目中，只有 `IsCanDelete` 存在可直接確認；其內部公式仍標記 `REQUIRES_MORE_SOURCE`。

## 5. 模板與主子專案

專案篩選明確支援 `OnlyTemplate`，資料欄位包含 `IsTemplate`／`Template`。精靈另持有主專案、子專案與來源目的 Grid，表示桌面版支援：

- 以模板作為建立來源。
- 主專案／子專案關係。
- 分拆後建立新專案碼。
- 來源與目的工項移轉。

Web 的 `Project` 模型需要至少增加：

```text
legacy_project_code
project_code_alias
is_template
main_project_code
source_project_code
project_type
capabilities
```

## 6. 專案屬性

目前可確認的可見屬性包括：

- 中文名稱
- 英文名稱
- 地址
- 專案代碼
- 專案代碼別名
- 備註

但專案目錄另持有大量狀態欄位，因此 Web 的「編輯專案」不得允許直接任意修改由系統計算的能力欄位。

建議分類：

### 使用者可編輯

- 名稱
- 地址
- 備註
- 部分別名（仍待確認規則）

### 系統管理

- projectCode
- 主子專案關係
- IsBud/IsBid/IsCNT
- IsCanDelete
- 授權狀態
- 模板狀態（可能需專用操作）

## 7. Web Mutation 契約

建議拆分，不使用一個通用 PATCH 接收全部欄位：

```text
POST /projects/wizard-sessions
POST /projects/wizard-sessions/{id}/validate
POST /projects/wizard-sessions/{id}/commit
PATCH /projects/{projectCode}/profile
POST /projects/{projectCode}/convert-to-template
POST /projects/{projectCode}/split
DELETE /projects/{projectCode}
GET /projects/{projectCode}/capabilities
```

每個 mutation 必須：

1. 驗證 Legacy Function Code。
2. 驗證專案授權。
3. 取得最新 capability，不信任前端舊值。
4. 在交易中執行。
5. 寫入稽核紀錄。
6. 回傳新的 capability 與版本。

## 8. 併發與版本

桌面版有 OnlineList 與工作上下文語意，Web 應建立 optimistic concurrency：

```text
project_version
updated_at
updated_by
```

對刪除、拆分、匯入、模板轉換等操作，建議加入工作鎖或 idempotency key，避免重複提交。

## 9. 尚待確認

- `IsCanDelete` 的實際計算來源。
- 刪除確認訊息與不可刪除原因。
- 刪除是否物理刪除或軟刪除。
- 預算、投標、契約、估驗資料的級聯策略。
- Alias 可否修改及是否唯一。
- Template 建立、複製、刪除規則。
- 主子專案刪除順序。
- OnlineList 是否實際阻擋異動。

未確認前，Web 的刪除與屬性編輯只能標記為 `PARTIAL`。
