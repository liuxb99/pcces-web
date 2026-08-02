# PCCES Web 完整復刻實作 Roadmap

更新日期：2026-08-02

## 1. 最終目標

`pcces-web` 的完成標準是：完整復刻 PCCES C# 桌面版的全部可用功能、業務規則、計算結果、資料交換格式、權限、狀態轉換與報表能力。介面可現代化，但不得任意省略舊版行為。

正式 Phase 規劃與驗收標準：

- `docs/development/00-web-parity-phase-roadmap.md`
- `docs/development/01-phase-acceptance-matrix.md`

## 2. 調研策略：功能樹優先

先完成全系統功能盤點，不再一開始逐方法撰寫完整規格。

### 第一層：Legacy 功能樹與源碼索引

掃描全部 C# 專案、namespace、Form、UserControl、主要 Domain 類別、選單與功能入口，建立：

- `docs/legacy-pcces/00-legacy-function-tree.md`
- `docs/legacy-pcces/11-source-index.md`
- 各模組簡要摘要文件

每個功能節點先只記錄：

- 功能名稱與用途
- 使用者入口
- 主要 C# 檔案、類別與 namespace
- 主要子功能
- 關聯模組
- Web 現況
- 復刻優先級
- 是否已有詳細文件

### 第二層：實作前局部深挖

準備復刻某個功能時，才回到該節點列出的 C# 源碼，補齊：

- 事件鏈與狀態機
- 欄位規則與驗證
- 權限與前置條件
- Domain／資料表／檔案呼叫
- 計算、交易、鎖定與回滾
- 錯誤訊息
- API、前端與永久測試契約

流程固定為：

```text
完整 C# 功能掃描
→ 功能樹與源碼索引
→ 選定 Web 開發 Segment
→ 依摘要定位 C# 檔案
→ 局部深讀與詳細規格
→ Web 復刻
→ Legacy 行為測試
```

## 3. 功能樹完成標準

功能樹階段完成必須符合：

1. 全部 C# 專案與主要 namespace 已盤點。
2. 所有 Form、UserControl、Wizard、主要 Domain 類別均掛入功能節點或標記為基礎設施。
3. 每個功能都有源碼入口與簡要用途。
4. 無法確認用途者標記 `UNKNOWN`，不得自行補寫。
5. 每個 Web 頁面可反查到一個或多個 Legacy 功能節點。
6. 已有詳細調研文件掛回對應節點，不刪除既有成果。

## 4. Legacy 功能主模組

- LEGACY-001：啟動、登入、主框架與系統生命週期
- LEGACY-002：導航、功能代碼、模組授權與權限
- LEGACY-003：專案目錄、建立、匯入、分拆與生命週期
- LEGACY-004：預算書與投標單編製
- LEGACY-005：工項單價庫、工料機與單價分析
- LEGACY-006：成本結構、轉標單、回轉與資料轉換
- LEGACY-007：契約、分包、變更與估驗計價
- LEGACY-008：結算、驗收、爭議與履約收尾
- LEGACY-009：報表、Excel、PDF、列印與匯入匯出
- LEGACY-010：系統管理、代碼表、升級與外部服務

## 5. Web 復刻 10 Phase

- Phase 0：平台基礎與 API 收斂
- Phase 1：專案管理與生命週期
- Phase 2：預算書與投標單核心
- Phase 3：MRS Base、工料機與單價分析
- Phase 4：成本結構、轉換與資料交換
- Phase 5：契約與分包管理
- Phase 6：變更、估驗、結算與驗收
- Phase 7：報表中心
- Phase 8：系統管理、設定與維運
- Phase 9：Legacy 收尾與 100% Traceability

完整範圍、依賴、Legacy 入口、交付物與出口條件，以 `docs/development/00-web-parity-phase-roadmap.md` 為準。

## 6. 簡要功能節點模板

```text
Feature ID:
功能名稱:
功能入口:
主要 C# 檔案:
主要類別／表單:
功能摘要:
主要子功能:
關聯模組:
Web 現況:
復刻優先級:
詳細文件:
可信度:
```

## 7. 詳細規格狀態

已完成的啟動、導航、專案生命週期與部分預算編輯器詳細文件保留，作為已深挖節點。後續功能樹完成前，不再要求所有節點先寫到事件級。

可信度標記：

- `CONFIRMED`
- `INFERRED`
- `UNKNOWN`
- `REQUIRES_RUNTIME_TEST`

## 8. Web 復刻狀態

功能級狀態：

- `NOT_STARTED`
- `UI_ONLY`
- `PARTIAL`
- `IMPLEMENTED`
- `LEGACY_MATCHED`
- `VERIFIED`

Phase 級狀態：

- `NOT_STARTED`
- `LEGACY_DEEP_REVIEW`
- `SPEC_READY`
- `IMPLEMENTING`
- `INTEGRATION_TESTING`
- `LEGACY_MATCHING`
- `VERIFIED`

頁面存在、API 可回傳或畫面可操作，不等於 Legacy 復刻完成。具體 Gate 以 `docs/development/01-phase-acceptance-matrix.md` 為準。

## 9. 第一個正式開發階段

### Phase 0：平台基礎與 API 收斂

第一批依序完成：

1. P0-S1：唯一 API 與路徑收斂。
2. P0-S2：Migration 與 Decimal 基礎。
3. P0-S3：Function Code、Module 與 Action。
4. P0-S4：WorkContext 與 Dirty State。

Phase 0 完成前，不得直接大規模擴充後續業務頁面，避免雙後端、API 契約、權限與資料型別持續漂移。

## 10. 不得回退的決策

1. 先建立全系統功能地圖，再逐段實作。
2. 詳細源碼調研改為實作前按需進行。
3. 既有詳細文件保留並掛入功能樹。
4. UI 可現代化，業務結果、資料格式、權限與狀態必須相容。
5. 推測內容必須標記，不能冒充已確認行為。
6. Web 開發依 10 Phase 推進，每個 Phase 必須通過明確 Gate。
7. 文件完成、頁面存在或 Reviewer 分數，均不能取代 Legacy Matching 與永久回歸測試。
