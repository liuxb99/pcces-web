# PCCES C# 系統管理與平台源碼索引

更新日期：2026-08-02

## 1. 使用者、群組與權限

| 功能 | 主要源碼 | 摘要 |
|---|---|---|
| 使用者／群組／權限主畫面 | `SysMaintain/FormSys_A.cs` | 使用者、群組、成員關係、使用者功能與群組功能 |
| 使用者編輯 | `FormSys_A_Edit.cs` | 帳號資料編輯對話框 |
| 使用者群組 | `FormSys_A_UsrGroup.cs` | 使用者所屬群組 |
| 群組成員 | `FormSys_A_GrpMember.cs` | 群組內成員 |
| 功能權限查詢 | `DBClass.ChkAuthority`、`DBClass.GetFuncName` | Function Code 權限 |
| 專案權限 | `DBClass.GetProjectAuthority`、`ProjAuthority` | 使用者與專案存取 |

## 2. SysMaintain 主控制項

| 源碼 | 掃描狀態 | 已確認內容 |
|---|---|---|
| `FormSys_A.cs` | `SUMMARY_COMPLETE` | 使用者、群組、成員、功能樹權限 |
| `FormSys_B.cs` | `DISCOVERED` | 待實作前深讀 |
| `FormSys_C.cs` / `FormSys_C_Edit.cs` | `DISCOVERED` | 主畫面與編輯對話框，正式用途待確認 |
| `FormSys_D.cs` / `FormSys_D_Pick.cs` | `DISCOVERED` | 主畫面與選取器，正式用途待確認 |
| `FormSys_E.cs` | `DISCOVERED` | 待確認 |
| `FormSys_F.cs` | `DISCOVERED` | 待確認 |
| `FormSys_G.cs` | `SUMMARY_COMPLETE` | 資料庫、組織資料庫、成本結構初始化、版本工具 |
| `FormSys_G1.cs` | `DISCOVERED` | G 子功能，待確認 |
| `FormSys_G_Info1.cs` | `SUMMARY_COMPLETE` | 建立／處理進度對話框 |
| `FormSys_I.cs` | `DISCOVERED` | 待確認 |
| `FormSys_J.cs` | `DISCOVERED` | 待確認 |
| `FormSys_Z.cs` | `SUMMARY_COMPLETE` | 全域設定、Autosave、MRS、分析、報表、DB 設定 |

## 3. 資料庫建立與升級

| 功能 | 主要源碼 | 摘要 |
|---|---|---|
| 資料庫管理 UI | `FormSys_G.cs` | 清單、建立、刪除、切換、搜尋 |
| 建立進度 | `FormSys_G_Info1.cs` | 長任務進度與結果 |
| 成本結構匯入 | `CostStructureImport.cs` | 建庫時匯入成本結構 |
| 成本結構類型 | `CostStructureTypePicker.cs` | 選擇要匯入的結構類型 |
| 資料庫升級 Domain | `DomainModule.DatabaseUpgrade/*` | 建立、升級、重置、還原 |
| DB 共用操作 | `PccesMain/DBClass.cs` | 權限與共用 SQL |
| 低階異動 | `ModifyDB` | 新增、更新、刪除 |

## 4. 系統設定

| 功能 | 主要源碼／設定 | 摘要 |
|---|---|---|
| 設定主畫面 | `FormSys_Z.cs` | 多分頁系統設定 |
| 桌面選項 | `OptionSet.ini` | 操作與模組偏好 |
| 主程式設定 | `PccesMain.ini` | 主程式與路徑設定 |
| 預算自動保存 | `FormSys_Z.cs`、`frmBudget.cs` | 啟用、間隔、清理 |
| 報表套件 | `FormSys_Z.cs`、Report namespace | 報表包位置與執行 |
| Excel 字型 | `FormSys_Z.cs`、ExportExcel | 匯出格式偏好 |
| 備份復原 | `FormSys_Z.cs` | 備份目錄與復原入口 |
| MRS 策略 | `FormSys_Z.cs`、MrsBase namespace | 載入方式、匯率變更 |

## 5. 共用服務與基礎類別

| 類別／namespace | 用途摘要 |
|---|---|
| `CommonMethods` | INI、檔案、IP、轉換、Access 匯入及共用工具 |
| `PubTools` | 數值、公式、設定與日誌 |
| `DBClass` | 系統／功能／專案權限與 SQL 共用操作 |
| `ModifyDB` | 低階資料異動 |
| `ModuleManager` | Budget、Bid、Common、Invoice 模組啟用狀態 |
| `OnlineList` | 線上使用者與工作狀態 |
| `AddOnDownLoad` | AddOn、附件與資料庫名稱處理 |
| `SysUser` | 使用者對應資料庫與系統資訊 |
| `PccesFormAction` | 全系統工作 Action 枚舉 |
| `ShellLib` | Shell、檔案、預覽或外部程序整合 |

## 6. 更新、註冊、Proxy 與外部服務

目前可由主程式引用與既有文件確認下列功能族存在，但實際類別清單仍需後續掃描：

- 程式更新檢查與更新啟動
- 註冊資訊與模組授權
- Proxy／網路設定
- Report WebDownload
- AddOn 下載與附件保存
- 版本資訊與公告

狀態：`DISCOVERED / SOURCE_LIST_INCOMPLETE`。

## 7. Web 實作定位

| Legacy 領域 | Web 建議模組 |
|---|---|
| User / Group / Function Code | Identity + Authorization Service |
| Project Authority | Project Access Service |
| ModuleManager | License / Feature Entitlement Service |
| FormSys_Z | Settings Service（system / organization / user scopes） |
| FormSys_G | Database Administration / Tenant Provisioning |
| DatabaseUpgrade | Migration Orchestrator |
| INI | Database settings + environment variables + user preferences |
| OnlineList | Presence / Work Session Service |
| Update / Registration | Deployment、License 與版本管理 |
