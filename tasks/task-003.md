# TASK-003 PCCES 源碼調研與遷移規劃

## Status
已完成 ✅

## Current Score
無（調研型任務，不需評分）

## 任務描述
對原始 PCCES Win 4.3 桌面應用（.NET Framework 4.8 WinForms）進行全面源碼調研，
產出結構化分析文檔，並制定網頁版遷移藍圖。

## 調研產出

| 文檔 | 路徑 | 內容重點 |
|------|------|---------|
| **源碼調研報告** | `PCCES_源碼調研報告.md` | 系統概述、技術棧、50+ 模組對照、核心類別、封閉 DLL 分析、資料庫推斷 |
| **網頁版遷移規劃** | `PCCES_網頁版遷移規劃.md` | React + ASP.NET Core 技術選型、6 階段路線圖（8-10 月）、API 規劃、風險對策 |

## 調研關鍵發現

### 1. 系統本質
PCCES Win 4.3 = 台灣公共工程電腦估算系統，開發商為聯宏資通（Archnowledge）
- 語言：C# 12.0 (.NET Framework 4.8)
- 資料庫：MS SQL Server（Windows 整合驗證 SSPI）
- 協力控制項：Infragistics、ComponentOne、Aspose.Cells、Crystal Reports

### 2. 核心模組

| 優先序 | 模組 | 原始 WinForms 位置 | 複雜度 |
|-------|------|-------------------|--------|
| P0 | 專案管理 | `PccesMain.Project/` (6 檔) | 🟢 中低 |
| P1 | **預算書編輯** 🏆 | `PccesMain.Budget/` (50+ 檔) + `frmBudget.cs` | 🔴 極高 |
| P1 | **工項單價庫** | `PccesMain.MrsBase/` (26 檔) | 🔴 高 |
| P2 | **計價管理** | `PccesMain.Invoice/` (10 檔) | 🟡 中高 |
| P2 | 分包合約 | `PccesMain.SplitContract/` (5 檔) | 🟡 中 |
| P3 | 結算/終驗 | `SubClose/` + `SubFinal/` (6 檔) | 🟡 中 |
| P3 | 系統維護 | `SysMaintain/` (22 檔) | 🟡 中 |
| P4 | 報表系統 | `Report/` (9 檔) + `Conversion.cs` | 🔴 高 |
| P4 | 比較分析 | `Compare/` (4 檔) | 🟢 低 |

### 3. 最大技術風險：封閉 DLL

所有 DomainModule 層均為**二進位 DLL 參考**（無源碼），安裝於 `C:\Program Files (x86)\PccesWin4.3\`：

```
Archnowledge.Pcces.DatabaseAccess    — 資料庫存取
Archnowledge.Pcces.DomainModule.*    — 預算、工項、標單、分包… 等業務邏輯
Archnowledge.Common                  — 通用工具
stdclass / budclass / PowerClass     — 標準/預算/權限類別庫
```

👉 **網頁版必須重新實作所有業務邏輯**，可透過反編譯參考 + UI 行為推導方式進行。

### 4. 最大檔案：Conversion.cs（25,897 行）

Excel 匯出引擎，涵蓋預算書、標單、計價等所有報表格式。
👉 建議逐步以 Openpyxl / ClosedXML 重新實作。

## 與現有專案之關聯

現有 `web-pcces/` 已實作部分功能，與源碼調研結果對照如下：

| 現有實作 | 對應原始模組 | 狀態 | 待加強 |
|---------|-------------|------|--------|
| `LoginPage` / JWT 認證 | `FormLogin.cs` + `StaffClass` | ✅ 完成 | — |
| `ProjectsPage` / CRUD | `Project/` 目录 | ✅ 完成 | 專案複製、匯入 |
| `BudgetEditorPage` (樹狀) | `frmBudget.cs` + `GridBudget.cs` | ✅ 基本完成 | WBS 拖曳排序、項次編號、印號管理 |
| `ResourcesPage` | `MrsBase/` (簡化版) | ✅ 基本完成 | 工料機分類、單價分析表 |
| `ReportsPage` (Excel) | `Conversion.cs` | ✅ 基本完成 | 多種報表格式、PDF |
| — | `Invoice/` 計價 | ❌ 未實作 | 下一階段重點 |
| — | `SplitContract/` 分包 | ❌ 未實作 | 下一階段重點 |
| — | `SysMaintain/` 系統維護 | ✅ 部分完成 | 角色權限管理 |

## 建議路線圖（整合後）

```
Phase 1 ✅ 已完成 — 基礎前後端框架 + 登入 + 專案管理 + 預算編輯 + 資源 + 報表
Phase 2 📋 TASK-002 — 示範資料修復、使用體驗優化
Phase 3 ⬜ 待規劃 — 計價管理模組（Invoice）
Phase 4 ⬜ 待規劃 — 分包合約管理（SplitContract + SubClose + SubFinal）
Phase 5 ⬜ 待規劃 — 工項單價庫（MrsBase 完整版）
Phase 6 ⬜ 待規劃 — 系統維護完整版 + 報表進階功能
```

## 參考文件
- `PCCES_源碼調研報告.md` — 完整技術分析
- `PCCES_網頁版遷移規劃.md` — 6 階段遷移路線圖
- `PCCES_CS/Archnowledge.Pcces.PccesMain/` — 原始碼目錄
