# PCCES 專案匯入驗證與 Commit 生命週期

更新日期：2026-08-02

## 1. 目的

本文記錄 `formNewProjectWizard` 在電子檔匯入、專案建立、成功提交及失敗回復方面的 Legacy 行為，作為 Web 版匯入服務與交易邊界的復刻基準。

主要來源：

- `PCCES_CS/Archnowledge.Pcces.PccesMain.Project/formNewProjectWizard.cs`
- `Archnowledge.Pcces.BUDClass.Project`
- `Archnowledge.Pcces.BUDClass.PubProject`
- `DBClass`
- `ModifyDB`

## 2. 匯入不是單一步驟 Upload

原桌面版匯入流程包含：

1. 使用者選擇來源檔案。
2. 系統辨識格式。
3. 解壓縮或解析來源。
4. 驗證內容、版本與用途。
5. 轉為 PCCES DataSet／Domain Input。
6. 建立新 ProjectCode。
7. 補建附屬狀態與權限。
8. 移動附加文件。
9. 將新專案代碼回傳專案目錄。

因此 Web 版不能只使用 `multipart upload -> insert project`。

## 3. 已確認來源格式分支

### 3.1 XML

若來源被辨識為 XML：

```text
IsOldXML(file)
  ├─ true  → ImportXMLInOldWay()
  └─ false → ImportXML(AppName)
```

Legacy 同時保留新、舊 XML 匯入路徑。

### 3.2 ZMD

ZMD 流程已確認：

1. 使用固定密碼 `ARCH13139409` 解壓縮。
2. 解壓到 `Report` 暫存路徑。
3. 確認壓縮內容存在。
4. 確認第一個有效檔案為 MDB。
5. 將 Access 資料匯入 DataSet。
6. 補齊舊格式缺少的欄位，例如 `CloseBidDate`、`CheckOut`。
7. 判斷是否為 CheckOut 文件。
8. 讀取 `srcKind`，失敗時回退到檔名尾碼判斷。
9. 特定 PID 下要求 `PccCodeCert == PCCCODECERT`。
10. 呼叫 `Project.InputXML(DataSet, XML_MODE)`。

## 4. 內容驗證與阻擋

已確認的阻擋條件包括：

- 解壓縮失敗。
- 電子檔無內容。
- 電子檔內容不是預期 MDB。
- 特定部署模式下不是發包用預算書。
- Domain 回傳 `編碼錯誤！無法轉入！`。
- Domain 回傳 `無工程代碼！無法轉入！`。
- Domain 回傳內容不符合成功訊息格式。

這些錯誤都會立即停止後續 Commit。

## 5. 成功回傳與 ProjectCode

`Project.InputXML` 的成功訊息內包含新專案代碼。Legacy 會由中文字括號內容擷取：

```text
（NewProjectCode）
```

並寫入：

```text
F_NewProjectCode
```

Web 版必須由後端回傳穩定欄位：

```json
{
  "projectCode": "...",
  "result": "success",
  "warnings": []
}
```

不可讓前端再解析人類可讀訊息取得專案代碼。

## 6. 成功後補完作業

匯入主資料成功後，Legacy 仍執行多項補完：

### 6.1 跳頁與發包標記

逐筆檢查 `Items.memo`：

- 包含 `[跳頁]`：寫入 `PageBreak.IsPageBreak = Y`。
- 包含 `[發包]`：寫入或更新 `PageBreak.IsBid = Y`。

若 DataSet 有 `itemKey`，優先使用 `itemKey`；否則使用 `sNo`。

### 6.2 廠商／Tenderer 資料

若 `Tenderer` 有資料，建立相應 `sub_memo` 紀錄，包含 invoice number 與預設專案屬性。

### 6.3 專案來源資訊

建立 `Project` Domain 物件後更新：

- `ps_srckind`
- `ps_projectCode`
- `ps_FileName`

### 6.4 建立操作人專案權限

成功後插入：

```sql
Insert Into ProjAuthority(ProjectCode, UserID)
```

表示建立者／匯入者會立即取得該專案權限。

### 6.5 附加文件搬移

若解壓目錄存在：

1. 依使用者目前資料庫取得 DBName。
2. 建立 `AddOn/{DBName}/{ProjectCode}`。
3. 將附件從暫存目錄移入正式目錄。
4. 更新 AddOn DBName。

## 7. 失敗與取消回復

### 7.1 分拆取消

若分拆尚未成功：

```text
!F_IsSplitSucceeded
→ DeleteNewProject()
→ PubProject.DeleAll(txtProjectCode)
→ DialogResult.Cancel
```

表示 Wizard 可能在中途已建立新專案；取消時必須做完整 Domain Rollback，而不是只關閉視窗。

### 7.2 回復語意

Web 版必須採用以下其中一種正式策略：

1. **單一資料庫交易**：整個 Commit 在同一 Transaction 完成。
2. **Wizard Draft**：先建立 draft，成功後 publish，取消時刪除 draft。
3. **Saga／補償交易**：跨檔案與資料庫操作逐步記錄，可補償。

不允許建立半完成專案後僅回傳錯誤。

## 8. Web API 建議

```text
POST /project-import-sessions
POST /project-import-sessions/{id}/upload
POST /project-import-sessions/{id}/validate
POST /project-import-sessions/{id}/commit
DELETE /project-import-sessions/{id}
GET /project-import-sessions/{id}
```

Commit 回應至少包含：

```text
projectCode
sourceType
sourceFileName
warnings
pageBreakCount
bidMarkerCount
attachmentCount
permissionCreated
```

## 9. 驗收要求

- 不合法來源不得留下 Project 主檔。
- Commit 失敗不得留下半完成 Items、PageBreak、Authority 或附件。
- 成功後建立者必須有專案權限。
- `[跳頁]`、`[發包]` 標記必須正確轉換。
- 新專案代碼必須由結構化回應取得。
- 舊 XML 與新 XML 必須有獨立測試案例。
- ZMD 解壓、內容驗證、用途驗證必須可追溯。

## 10. 尚待追蹤

- 各檔案副檔名的完整路由表。
- `InputXML` 完整成功／警告回傳格式。
- CheckOut 文件成功後的完整後處理。
- 暫存目錄清除時機。
- 匯入過程是否由 DB Transaction 包覆。
- ProjectCode 重複時的精確處理。
