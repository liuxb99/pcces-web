# PCCES Web 完整復刻實作 Roadmap

更新日期：2026-08-02

## 1. 最終目標

`pcces-web` 不再以「參考 PCCES 重新設計一套工程預算網站」為完成標準，而是：

> 完整復刻 PCCES C# 桌面版的全部可用功能、互動規則、計算結果、資料交換格式、權限邏輯、狀態轉換及報表能力；介面可以現代化，但不得任意省略桌面版行為。

## 2. 完成判定

任何功能只有同時具備下列證據，才可標記完成：

1. C# 來源檔案、類別、事件與呼叫鏈已記錄。
2. 使用者入口、前置條件、欄位狀態、驗證及錯誤處理已記錄。
3. 涉及的 DomainModule、資料表、Web Service、INI、Registry 或檔案已記錄。
4. 網頁版 API、Domain Service、前端互動及權限具有明確對應。
5. 已有永久測試驗證核心規則。
6. 已完成 Legacy 行為對照驗收。

網頁版狀態統一使用：

- `NOT_STARTED`
- `UI_ONLY`
- `PARTIAL`
- `IMPLEMENTED`
- `LEGACY_MATCHED`
- `VERIFIED`

頁面存在或 API 可回傳資料，不等於功能完成。

## 3. 證據可信度標記

- `CONFIRMED`：可由 C# 原始碼直接確認。
- `INFERRED`：由 UI 呼叫、參數、回傳值或相鄰程式推導。
- `UNKNOWN`：目前源碼不足以確認。
- `REQUIRES_RUNTIME_TEST`：必須執行 PCCES Win 4.3 驗證。

所有推導內容必須保留標記，不得寫成已確認事實。

## 4. 當前盤點

### 已存在

- React + TypeScript 前端及主要功能頁面。
- Flask + SQLAlchemy API。
- 專案、預算、資源、計價、合約、爭議、結算、驗收、管理及比較功能的部分實作。
- 原始 C# 專案與既有初步調研文件。
- Vercel 部署流程。

### P0 缺口

1. 尚無完整的桌面版功能清冊與事件級互動規格。
2. 尚無 C# → Web 的逐功能 Traceability Matrix。
3. 網頁版已有大量自行設計功能，但尚未證明與桌面版一致。
4. 根目錄 `/api` 與 `web-pcces/backend` 存在雙後端，可能形成契約及行為漂移。
5. 前端 `baseURL=/api`，部分 API 路徑又帶 `/api`，存在 `/api/api/...` 風險。
6. 金額與數量仍大量使用 `float`，尚未按桌面版逐欄位確認精度、截位及四捨五入規則。
7. 桌面版啟動、資料庫檢查、匿名登入、系統管理員初始化、資料庫升級、線上狀態與關閉閘門尚未復刻。
8. 原版報表、Excel/PDF、轉檔與列印格式尚未建立完整相容清單。

## 5. 分段順序

### LEGACY-001：啟動、登入、主框架與系統生命週期

範圍：

- `frmPccesMain`
- `FormLogin`
- `FormSplash`
- `FormPanel` / `FormPanel2` / `FormPanel3`
- `FunctionButtons`
- `OnlineList`
- 資料庫預檢、SQL 版本、單例程序、INI、更新檢查、關閉閘門

交付：

- `docs/legacy-pcces/00-system-overview.md`
- `docs/legacy-pcces/01-startup-login-navigation.md`
- Traceability Matrix 初版

驗收：所有已確認的入口、分支、狀態與副作用均可追溯到 C# 方法。

### LEGACY-002：主功能導航與權限

- 功能按鈕產生方式
- 使用者／角色／功能權限
- MDI 視窗唯一性與重用
- 主面板、選單、工具列、狀態列
- 功能啟用／停用條件

### LEGACY-003：專案與預算編製核心

- 專案建立、開啟、複製、刪除、鎖定
- 預算主畫面生命週期
- 章、節、工項、資源節點
- 插入、移動、複製、貼上、刪除
- 欄位編輯、Grid 事件、快捷鍵、右鍵選單
- 數量、單價、複價與總額重算

### LEGACY-004：工項單價庫與工料機分析

- MRS Base 分類與查詢
- 工料機資源組合
- 單價分析細項
- 價格更新、生效日期及來源
- 書籤、比價與引用流程

### LEGACY-005：預算轉換與成本結構

- 預算轉標單
- 標單回轉
- 成本結構及費用項目
- 加減項、稅費、管理費
- `Conversion.cs` 全部轉換及輸出分支

### LEGACY-006：分包、變更、估驗計價

- Split Contract
- Budget Change
- Contract Change
- 估驗計價期別、累計、保留款及審核
- 契約與預算項目關聯

### LEGACY-007：結算、驗收與履約收尾

- 結算數量與金額
- 最終驗收
- 爭議、缺失及處理狀態
- 關閉與封存條件

### LEGACY-008：報表、Excel、PDF、列印及匯入匯出

- 所有報表入口及參數
- Crystal Reports 替代規格
- Excel 欄位、合併儲存格、格式、公式、分頁
- PDF 預覽、轉檔及錯誤流程
- 舊檔案格式相容性

### LEGACY-009：系統管理、代碼表、升級與外部服務

- 使用者、組織、權限
- 系統參數、代碼表
- Database Upgrade / Change
- 更新服務、註冊資訊、Proxy
- 訊息、公告、版本資訊

### WEB-PARITY-001：後端與 API 收斂

在 Legacy 規格穩定後執行：

- 合併雙後端。
- 統一 `/api` 路徑。
- 建立 OpenAPI 契約。
- 抽出 routes / services / repositories / domain。
- 建立 Legacy Feature ID 與 API 對應。

### WEB-PARITY-002：Decimal 與計算可追溯性

- 全面盤點欄位型別及精度。
- 使用 Decimal／Numeric。
- 保存公式、輸入、精度規則與計算結果。
- 建立版本、凍結、差異及重新計算機制。

### WEB-PARITY-003 起：依 Traceability Matrix 分模組補齊

每批只關閉一個完整子系統，並加入永久回歸測試。

## 6. 文件結構

```text
docs/legacy-pcces/
├── 00-system-overview.md
├── 01-startup-login-navigation.md
├── 02-project-management.md
├── 03-budget-editor.md
├── 04-work-item-database.md
├── 05-resource-analysis.md
├── 06-cost-structure.md
├── 07-budget-conversion.md
├── 08-contract-management.md
├── 09-progress-payment.md
├── 10-change-orders.md
├── 11-settlement.md
├── 12-final-acceptance.md
├── 13-reports-export-print.md
├── 14-permission-administration.md
├── 15-database-and-services.md
├── 16-error-and-message-catalog.md
└── 17-feature-traceability-matrix.md
```

## 7. 每項功能規格模板

```text
Feature ID:
名稱:
可信度:
入口:
前置條件:
權限:
相關表單／控制項:
事件鏈:
Domain／DLL 呼叫:
資料來源:
輸入驗證:
計算規則:
資料副作用:
UI 副作用:
錯誤／訊息:
關閉／取消行為:
網頁版現況:
缺口:
驗收案例:
```

## 8. 不得回退的架構決策

1. 先完成 C# 行為級調研，再補網頁版，不再用頁面清單推測原功能。
2. UI 可現代化，但業務結果、資料交換格式、權限與狀態轉換必須相容。
3. 封閉 DLL 邏輯必須標示可信度並用 Runtime Test 補證，禁止假設完成。
4. 所有 Web 功能必須能反查 Legacy Feature ID。
5. 文件是實作契約，不是完成聲明；只有永久測試與行為對照通過才能標記 `VERIFIED`。
