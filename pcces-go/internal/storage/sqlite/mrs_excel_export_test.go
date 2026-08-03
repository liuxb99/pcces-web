package sqlite

import (
	"archive/zip"
	"bytes"
	"context"
	"io"
	"path/filepath"
	"strings"
	"testing"
)

func TestMRSExcelExportKeepsTwoGridContract(t *testing.T) {
	ctx := context.Background()
	store, err := Open(ctx, filepath.Join(t.TempDir(), "mrs-export.db"))
	if err != nil {
		t.Fatal(err)
	}
	defer store.Close()
	budget := NewBudgetDecimalRepository(store)
	if _, err = budget.Save(ctx, BudgetDecimalItem{ID: "I1", ProjectCode: "P1", Name: "混凝土工項", Kind: "L", Quantity: "3.4567", UnitPrice: "12.3456", QuantityScale: 4, PriceScale: 4, AmountScale: 2}); err != nil {
		t.Fatal(err)
	}
	resources := NewResourceDecimalRepository(store)
	unit := "KG"
	if _, err = resources.SaveResource(ctx, ResourceDecimal{ID: "R1", Code: "M00001", Name: "水泥", Unit: &unit, UnitPrice: "7.8912", PriceScale: 4}); err != nil {
		t.Fatal(err)
	}
	if err = NewResourceBudgetLineageRepository(store).Link(ctx, "P1", "R1", "I1"); err != nil {
		t.Fatal(err)
	}
	payload, err := NewMRSExcelExporter(store).ExportProject(ctx, "P1")
	if err != nil {
		t.Fatal(err)
	}
	zr, err := zip.NewReader(bytes.NewReader(payload), int64(len(payload)))
	if err != nil {
		t.Fatal(err)
	}
	contents := map[string]string{}
	for _, f := range zr.File {
		r, e := f.Open()
		if e != nil {
			t.Fatal(e)
		}
		b, e := io.ReadAll(r)
		_ = r.Close()
		if e != nil {
			t.Fatal(e)
		}
		contents[f.Name] = string(b)
	}
	if !strings.Contains(contents["xl/workbook.xml"], "專案資源") || !strings.Contains(contents["xl/workbook.xml"], "引用工項") {
		t.Fatal("missing Legacy sheet names")
	}
	if !strings.Contains(contents["xl/worksheets/sheet1.xml"], "資源編碼") || !strings.Contains(contents["xl/worksheets/sheet1.xml"], "M00001") {
		t.Fatal("resource grid missing")
	}
	if !strings.Contains(contents["xl/worksheets/sheet2.xml"], "工項名稱") || !strings.Contains(contents["xl/worksheets/sheet2.xml"], "混凝土工項") {
		t.Fatal("reference grid missing")
	}
	if !strings.Contains(contents["xl/styles.xml"], `formatCode="0.0000"`) {
		t.Fatal("analysis precision format missing")
	}
}

func TestMRSExcelExportEmptyProjectStillProducesWorkbook(t *testing.T) {
	ctx := context.Background()
	store, err := Open(ctx, filepath.Join(t.TempDir(), "empty-export.db"))
	if err != nil {
		t.Fatal(err)
	}
	defer store.Close()
	payload, err := NewMRSExcelExporter(store).ExportProject(ctx, "EMPTY")
	if err != nil {
		t.Fatal(err)
	}
	if _, err = zip.NewReader(bytes.NewReader(payload), int64(len(payload))); err != nil {
		t.Fatal(err)
	}
}
