package sqlite

import (
	"context"
	"path/filepath"
	"testing"
)

func TestProjectCostStructureRunAggregatesAndPersists(t *testing.T) {
	ctx := context.Background()
	store, err := Open(ctx, filepath.Join(t.TempDir(), "cost-run.db"))
	if err != nil {
		t.Fatal(err)
	}
	defer store.Close()
	types := NewCostStructureRepository(store)
	if _, err = types.SaveType(ctx, CostStructureType{ID: "T1", Code: "STD", Name: "Standard", Enabled: true, CreatedBy: "u1"}); err != nil {
		t.Fatal(err)
	}
	if _, err = types.AssignProject(ctx, "P1", "T1", "BUD", "u1", 0); err != nil {
		t.Fatal(err)
	}
	details := NewCostStructureDetailRepository(store)
	_, err = details.ImportDefinition(ctx, "T1", CostStructureImportRequest{ActorID: "u1", Categories: []CostStructureCategory{
		{Code: "MGT", Name: "Management", Kind: "MANAGEMENT", Sequence: 10, Rate: "10", Enabled: true},
		{Code: "TAX", Name: "Tax", Kind: "TAX", Sequence: 20, Rate: "5", Enabled: true},
	}})
	if err != nil {
		t.Fatal(err)
	}
	run, err := NewProjectCostStructureRunRepository(store).Recalculate(ctx, "P1", []BudgetSnapshotItem{
		{ID: "A", Quantity: "2", UnitPrice: "100"},
		{ID: "B", Amount: "300"},
		{ID: "S", Kind: "SECTION", Amount: "9999"},
	}, 2, "u1")
	if err != nil {
		t.Fatal(err)
	}
	if run.DirectCost != "500.00" || run.Total != "577.50" {
		t.Fatalf("unexpected run %#v", run)
	}
	loaded, err := NewProjectCostStructureRunRepository(store).Get(ctx, run.ID)
	if err != nil {
		t.Fatal(err)
	}
	if loaded.Total != run.Total || len(loaded.BudgetSnapshot) != 3 {
		t.Fatalf("unexpected persisted run %#v", loaded)
	}
}

func TestProjectCostStructureRunRequiresAssignment(t *testing.T) {
	ctx := context.Background()
	store, err := Open(ctx, filepath.Join(t.TempDir(), "missing.db"))
	if err != nil {
		t.Fatal(err)
	}
	defer store.Close()
	if _, err = NewProjectCostStructureRunRepository(store).Recalculate(ctx, "P0", nil, 2, "u1"); err == nil {
		t.Fatal("expected missing assignment")
	}
}
