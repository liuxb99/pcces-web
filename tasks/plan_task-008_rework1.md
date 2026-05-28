# 返工規劃 — TASK-008 第 1 次返工

## 目標
修復 REVIEWER 指出的 4 項缺失，將評分從 87 → 90+ 分。

### 待修復問題總覽

| # | 問題 | 嚴重性 | 檔案 |
|---|------|--------|------|
| 1 | Excel 檔名擷取 bug：`(blob as any)?.headers?.['content-disposition']` 永遠為 undefined | Bug | `web-pcces/frontend/src/api.ts` + `ComparePage.tsx` |
| 2 | MrsBase 搜尋後 `setSummary(null)`，統計摘要消失 | UX 缺陷 | `MrsBasePriceComparePage.tsx` + `api/index.py` |
| 3 | GET endpoint 未傳 scope 參數 | 功能缺失 | `api/index.py` |
| 4 | 缺少 zero-division 邊界測試 | 測試不足 | `api/test_compare.py` |

---

## 修改檔案清單

| # | 檔案 | 動作 | 說明 |
|---|------|------|------|
| 1 | `web-pcces/frontend/src/api.ts` | **修改** | `exportExcel` 改回傳 `{data: Blob, filename: string}` 而非僅 Blob |
| 2 | `web-pcces/frontend/src/pages/ComparePage.tsx` | **修改** | 使用 `exportExcel` 回傳的完整 response 取得檔名 |
| 3 | `web-pcces/frontend/src/pages/MrsBasePriceComparePage.tsx` | **修改** | 搜尋後從搜尋結果自行計算 summary，不再設為 null |
| 4 | `web-pcces/frontend/src/api.ts` | **修改** | MrsBase search API 回傳型別調整（可選） |
| 5 | `api/index.py` | **修改** | GET `compare_budget_items_get` 補上 scope 參數 |
| 6 | `api/index.py` | **修改** | MrsBase search API 回傳 summary（後端方案） |
| 7 | `api/test_compare.py` | **修改** | 新增 zero-division 邊界測試 |

---

## 實作步驟

### Step-1：修復 Excel 檔名擷取 bug

**問題分析**：
- `api.ts:exportExcel` 回傳 `res.data`（僅 Blob），前端 `ComparePage.tsx` 嘗試從 Blob 讀取 `headers['content-disposition']`，但 Blob 沒有 headers 屬性
- 後端 `send_file(download_name=filename)` 已將檔名放入 response headers，但被 Axios 攔截器截斷

**解決方案**：
修改 `api.ts` 中 `exportExcel` 回傳完整 axios response，讓前端可取用 `res.headers['content-disposition']`。

#### 修改：`web-pcces/frontend/src/api.ts`（約 line 733-735）

**原始碼**：
```typescript
  /** 匯出工項比較報表 Excel */
  exportExcel: async (data: CompareRequest): Promise<Blob> => {
    const res = await api.post('/api/compare/budget-items/export/excel', data, {
      responseType: 'blob',
    });
    return res.data;
  },
```

**修改後**：
```typescript
  /** 匯出工項比較報表 Excel */
  exportExcel: async (data: CompareRequest): Promise<{ data: Blob; filename: string }> => {
    const res = await api.post('/api/compare/budget-items/export/excel', data, {
      responseType: 'blob',
    });
    // 從 response headers 解析檔名
    const disposition = res.headers?.['content-disposition'] as string | undefined;
    let filename = 'PCCES_比較報表.xlsx';
    if (disposition) {
      const match = disposition.match(/filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/);
      if (match) filename = match[1].replace(/['"]/g, '');
    }
    return { data: res.data, filename };
  },
```

> **注意**：需確保 Axios instance 的 `responseType: 'blob'` 不影響 headers 讀取。標準 Axios 行為中，設定 `responseType: 'blob'` 仍可讀取 `res.headers`。

#### 修改：`web-pcces/frontend/src/pages/ComparePage.tsx`（約 line 88-103）

**原始碼**：
```typescript
  const handleExportExcel = useCallback(async () => {
    if (!projectAId || !projectBId) return;
    try {
      const blob = await compareApi.exportExcel({
        project_a_id: projectAId,
        project_b_id: projectBId,
        scope: 'leaf',
      });
      // 從後端下載檔名
      const disposition = (blob as any)?.headers?.['content-disposition'];
      let filename = 'PCCES_比較報表.xlsx';
      if (disposition) {
        const match = disposition.match(/filename=(.+)/);
        if (match) filename = match[1];
      }
      const url = window.URL.createObjectURL(blob);
```

**修改後**：
```typescript
  const handleExportExcel = useCallback(async () => {
    if (!projectAId || !projectBId) return;
    try {
      const { data: blob, filename } = await compareApi.exportExcel({
        project_a_id: projectAId,
        project_b_id: projectBId,
        scope: 'leaf',
      });
      const url = window.URL.createObjectURL(blob);
      const a = document.createElement('a');
      a.href = url;
      a.download = filename;
      document.body.appendChild(a);
      a.click();
      document.body.removeChild(a);
      window.URL.revokeObjectURL(url);
      message.success('比較報表已匯出');
```

---

### Step-2：MrsBase 搜尋後保留統計摘要

**問題分析**：
- `MrsBasePriceComparePage.tsx:80` 在搜尋後呼叫 `setSummary(null)`，導致統計摘要卡片消失
- 後端 search API（`GET /api/mrs-base/search`）僅回傳 items 列表，不回傳 summary

**解決方案（前端計算方案）**：
修改前端搜尋處理邏輯，從搜尋結果自行計算 avg/min/max price。

#### 修改：`web-pcces/frontend/src/pages/MrsBasePriceComparePage.tsx`（約 line 70-84）

**原始碼**：
```typescript
  const handleSearch = useCallback(async (value: string) => {
    setSearchText(value);
    if (!value.trim()) {
      loadItems(selectedCatId);
      return;
    }
    setLoading(true);
    try {
      const result = await mrsBaseApi.search({ q: value });
      setItems(result);
      setSummary(null);
    } catch (err) {
      message.error('搜尋失敗');
    } finally {
      setLoading(false);
    }
  }, [selectedCatId, loadItems]);
```

**修改後**：
```typescript
  const handleSearch = useCallback(async (value: string) => {
    setSearchText(value);
    if (!value.trim()) {
      loadItems(selectedCatId);
      return;
    }
    setLoading(true);
    try {
      const result = await mrsBaseApi.search({ q: value });
      setItems(result);
      // 從搜尋結果自行計算統計摘要
      const prices = result
        .map((item) => item.unit_price)
        .filter((p): p is number => p !== null && p !== undefined);
      setSummary({
        total: result.length,
        avg_price: prices.length > 0
          ? Math.round((prices.reduce((a, b) => a + b, 0) / prices.length) * 100) / 100
          : 0,
        max_price: prices.length > 0 ? Math.max(...prices) : 0,
        min_price: prices.length > 0 ? Math.min(...prices) : 0,
      });
    } catch (err) {
      message.error('搜尋失敗');
    } finally {
      setLoading(false);
    }
  }, [selectedCatId, loadItems]);
```

---

### Step-3：GET endpoint 補上 scope 參數

**問題分析**：
- `compare_budget_items_get()`（line 4718-4736）呼叫 `_compare_budget_items_core(db, project_a_id, project_b_id)` 時未傳遞 scope，永遠使用預設值 `"leaf"`
- POST 版本（line 4699-4715）已從 request body 讀取 scope 並傳入

**解決方案**：
從 `request.args.get("scope", "leaf")` 讀取並傳遞給 core 函式。

#### 修改：`api/index.py`（約 line 4718-4736）

**原始碼**：
```python
@app.route("/api/compare/budget-items", methods=["GET"])
@require_auth
def compare_budget_items_get(user_id):
    """GET 版比較兩個專案的預算項目差異（透過查詢參數）"""
    project_a_id = request.args.get("project_a_id", type=int)
    project_b_id = request.args.get("project_b_id", type=int)

    if not project_a_id or not project_b_id:
        return jsonify({"detail": "請提供 project_a_id 與 project_b_id"}), 400

    db = next(get_db())
    try:
        _, err_a = _check_project_access(db, project_a_id, user_id)
        if err_a:
            return err_a
        _, err_b = _check_project_access(db, project_b_id, user_id)
        if err_b:
            return err_b

        result = _compare_budget_items_core(db, project_a_id, project_b_id)
        return jsonify(result)
    finally:
        db.close()
```

**修改後**：
```python
@app.route("/api/compare/budget-items", methods=["GET"])
@require_auth
def compare_budget_items_get(user_id):
    """GET 版比較兩個專案的預算項目差異（透過查詢參數）"""
    project_a_id = request.args.get("project_a_id", type=int)
    project_b_id = request.args.get("project_b_id", type=int)
    scope = request.args.get("scope", "leaf")

    if not project_a_id or not project_b_id:
        return jsonify({"detail": "請提供 project_a_id 與 project_b_id"}), 400

    db = next(get_db())
    try:
        _, err_a = _check_project_access(db, project_a_id, user_id)
        if err_a:
            return err_a
        _, err_b = _check_project_access(db, project_b_id, user_id)
        if err_b:
            return err_b

        result = _compare_budget_items_core(db, project_a_id, project_b_id, scope=scope)
        return jsonify(result)
    finally:
        db.close()
```

---

### Step-4：補強 zero-division 邊界測試

**問題分析**：
- `calc_diff` 在 `a_val == 0` 時回傳 `pct = None`，但沒有測試驗證此行為
- 雙方數量皆為 0 時，diff_pct 應為 None（N/A），而非 0 或錯誤

**解決方案**：
在 `api/test_compare.py` 中新增測試案例，覆蓋 zero-division 情境。

#### 修改：`api/test_compare.py` — 在 `TestCompareBudgetItems` 類別中新增測試方法

```python
    def test_compare_zero_division(self, compare_seed_db, client):
        """雙方數量皆為 0 時 diff_pct 應為 None（N/A）"""
        session, proj_a, proj_b = compare_seed_db

        # 在兩個專案中加入相同 key 但 quantity/unit_price 皆為 0 的工項
        direct_a = session.query(BudgetItem).filter(
            BudgetItem.project_id == proj_a.id,
            BudgetItem.kind == BudgetItemKind.B
        ).first()
        direct_b = session.query(BudgetItem).filter(
            BudgetItem.project_id == proj_b.id,
            BudgetItem.kind == BudgetItemKind.B
        ).first()

        zero_item_a = BudgetItem(
            project_id=proj_a.id, parent_id=direct_a.id,
            c_name="零值工項", c_unit="式", kind=BudgetItemKind.W,
            print_no="0099.01", quantity=0, unit_price=0, amount=0,
        )
        session.add(zero_item_a)
        zero_item_b = BudgetItem(
            project_id=proj_b.id, parent_id=direct_b.id,
            c_name="零值工項", c_unit="式", kind=BudgetItemKind.W,
            print_no="0099.01", quantity=0, unit_price=0, amount=0,
        )
        session.add(zero_item_b)
        session.commit()

        result = _compare_budget_items_core(session, proj_a.id, proj_b.id)
        zero_item = [i for i in result["items"] if "零值" in i["c_name"]]
        assert len(zero_item) == 1
        # diff 皆為 0，status 應為 unchanged
        assert zero_item[0]["status"] == "unchanged"
        # diff_pct 皆為 None（因分母為 0）
        assert zero_item[0]["diff_pct"]["quantity"] is None
        assert zero_item[0]["diff_pct"]["unit_price"] is None
        assert zero_item[0]["diff_pct"]["amount"] is None
```

---

## 技術細節

### Excel 檔名擷取流程圖

```
後端 send_file(download_name="PCCES_比較報表_PA001_vs_PB001.xlsx")
  → Flask 設定 Content-Disposition: attachment; filename="PCCES_比較報表_PA001_vs_PB001.xlsx"
  → Axios response headers 中可讀取
  → api.ts:exportExcel 回傳 {data: Blob, filename: string}
  → ComparePage.tsx: 使用 filename 作為 a.download 屬性
```

### MrsBase 搜尋 summary 計算邏輯

| 欄位 | 計算方式 |
|------|---------|
| `total` | `result.length`（搜尋結果總數） |
| `avg_price` | `prices 總和 / prices.length`（四捨五入至小數 2 位） |
| `max_price` | `Math.max(...prices)` |
| `min_price` | `Math.min(...prices)` |

> 採用 **前端計算方案** 而非後端修改，優點是：
> 1. 搜尋 API 保持單純（僅回傳 items）
> 2. 前端計算延遲極低（資料已在記憶體中）
> 3. 不影響其他使用搜尋 API 的地方

### GET scope 參數

| 方法 | 原有行為 | 修正後行為 |
|------|---------|-----------|
| `POST /api/compare/budget-items` | body: `{scope: "leaf"|"all"}` | ✅ 不變 |
| `GET /api/compare/budget-items` | 無 scope 參數，永遠 leaf | ✅ 新增 `?scope=leaf|all` |

### 測試驗證項目

| 測試 | 驗證目標 | 測試資料 |
|------|---------|---------|
| `test_compare_zero_division` | 雙方數量/單價皆為 0 時 diff_pct 為 None | 在 A、B 兩專案新增 print_no="0099.01" 的零值工項 |

---

## 預計工時

| 步驟 | 內容 | 預計工時 |
|------|------|---------|
| Step-1 | Excel 檔名擷取 bug 修復（api.ts + ComparePage.tsx） | 1.0 hr |
| Step-2 | MrsBase 搜尋 summary 保留（MrsBasePriceComparePage.tsx） | 1.0 hr |
| Step-3 | GET endpoint scope 補上（api/index.py） | 0.5 hr |
| Step-4 | Zero-division 邊界測試補強（test_compare.py） | 1.0 hr |
| **合計** | | **3.5 hr** |

---

## 預計改善分數

| 評分項目 | 原始分數 | 預計改善後 | 改善原因 |
|---------|---------|-----------|---------|
| 功能性 | 22/25 | **24/25** | 修復 Excel 檔名 bug + GET scope 缺失 |
| 程式品質與架構 | 22/25 | **24/25** | 修正 Blob headers 錯誤用法，scope 參數鏈完整 |
| 測試與驗證 | 22/25 | **24/25** | 新增 zero-division 邊界測試 |
| 使用體驗與安全 | 21/25 | **23/25** | MrsBase 搜尋後仍顯示統計摘要 |
| **總分** | **87/100** | **95/100** | ✅ 合格 |
