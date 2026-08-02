package sqlite

import (
	"context"
	"path/filepath"
	"testing"
)

func TestCostStructureImportAndItemProperty(t *testing.T) {
	ctx := context.Background()
	store, err := Open(ctx, filepath.Join(t.TempDir(), "cost-details.db"))
	if err != nil {
		t.Fatal(err)
	}
	defer store.Close()
	base := NewCostStructureRepository(store)
	_, err = base.SaveType(ctx, CostStructureType{ID: "T1", Code: "STD", Name: "標準", Enabled: true, CreatedBy: "u1"})
	if err != nil {
		t.Fatal(err)
	}
	repo := NewCostStructureDetailRepository(store)
	result, err := repo.ImportDefinition(ctx, "T1", CostStructureImportRequest{ActorID: "u1", Categories: []CostStructureCategory{
		{ID: "C1", Code: "D", Name: "直接費", Kind: "DIRECT", Enabled: true},
		{ID: "C2", Code: "M", Name: "管理費", Kind: "MANAGEMENT", Rate: "0.05", Enabled: true},
	}})
	if err != nil {
		t.Fatal(err)
	}
	if result.ImportedRows != 2 {
		t.Fatalf("unexpected import result %#v", result)
	}
	rows, err := repo.ListCategories(ctx, "T1")
	if err != nil || len(rows) != 2 {
		t.Fatalf("unexpected categories %#v err=%v", rows, err)
	}
	item, err := repo.SaveItemProperty(ctx, BudgetItemCostProperty{ProjectCode: "P1", BudgetItemID: "B1", CostCategoryID: "C2", CostKind: "MANAGEMENT", Sign: 1, Rate: "0.05", UpdatedBy: "u1"})
	if err != nil {
		t.Fatal(err)
	}
	if item.CategoryCode != "M" || item.RowVersion != 1 {
		t.Fatalf("unexpected property %#v", item)
	}
	if _, err = repo.SaveItemProperty(ctx, BudgetItemCostProperty{ProjectCode: "P1", BudgetItemID: "B1", CostCategoryID: "C2", CostKind: "MANAGEMENT", Sign: 1, Rate: "0.06", UpdatedBy: "u2", RowVersion: 0}); err == nil {
		t.Fatal("expected stale row version")
	}
}

func TestCostStructureImportValidationIsAtomic(t *testing.T) {
	ctx := context.Background()
	store, err := Open(ctx, filepath.Join(t.TempDir(), "cost-details-atomic.db"))
	if err != nil {
		t.Fatal(err)
	}
	defer store.Close()
	base := NewCostStructureRepository(store)
	_, _ = base.SaveType(ctx, CostStructureType{ID: "T1", Code: "STD", Name: "標準", Enabled: true, CreatedBy: "u1"})
	repo := NewCostStructureDetailRepository(store)
	_, err = repo.ImportDefinition(ctx, "T1", CostStructureImportRequest{ActorID: "u1", Categories: []CostStructureCategory{{Code: "D", Name: "直接費", Kind: "DIRECT", Enabled: true}}})
	if err != nil {
		t.Fatal(err)
	}
	_, err = repo.ImportDefinition(ctx, "T1", CostStructureImportRequest{ActorID: "u1", Categories: []CostStructureCategory{{Code: "X", Name: "一", Kind: "DIRECT", Enabled: true}, {Code: "X", Name: "二", Kind: "DIRECT", Enabled: true}}})
	if err == nil {
		t.Fatal("expected duplicate validation error")
	}
	rows, err := repo.ListCategories(ctx, "T1")
	if err != nil || len(rows) != 1 || rows[0].Code != "D" {
		t.Fatalf("existing definition was replaced: %#v err=%v", rows, err)
	}
}
