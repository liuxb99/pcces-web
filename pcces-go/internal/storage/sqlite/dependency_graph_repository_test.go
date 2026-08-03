package sqlite

import (
	"context"
	"path/filepath"
	"testing"
)

func TestDependencyGraphHistoryAndProjectRecalculation(t *testing.T) {
	ctx := context.Background()
	store, err := Open(ctx, filepath.Join(t.TempDir(), "graph.db"))
	if err != nil {
		t.Fatal(err)
	}
	defer store.Close()
	budget := NewBudgetDecimalRepository(store)
	resource := NewResourceDecimalRepository(store)
	lineage := NewResourceBudgetLineageRepository(store)
	graph := NewDependencyGraphRepository(store)
	if _, err = budget.Save(ctx, BudgetDecimalItem{ID: "I1", ProjectCode: "P1", Name: "Work", Kind: "L", Quantity: "3.0000", UnitPrice: "10.0000", Amount: "0", QuantityScale: 4, PriceScale: 4, AmountScale: 2}); err != nil {
		t.Fatal(err)
	}
	if _, err = resource.SaveResource(ctx, ResourceDecimal{ID: "R1", Code: "R01", Name: "Material", UnitPrice: "12.3456", PriceScale: 4}); err != nil {
		t.Fatal(err)
	}
	if err = lineage.Link(ctx, "P1", "R1", "I1"); err != nil {
		t.Fatal(err)
	}
	history, err := graph.RecordPrice(ctx, "P1", "R1", "10.0000", "12.3456", "TEST")
	if err != nil {
		t.Fatal(err)
	}
	if history == nil || history.DeepLink == "" {
		t.Fatal("missing history deep link")
	}
	run, err := graph.RecalculateProject(ctx, "P1")
	if err != nil {
		t.Fatal(err)
	}
	if run.Status != "COMPLETED" || run.Scope != "PROJECT" || run.DeepLink == "" {
		t.Fatalf("unexpected run %+v", run)
	}
	item, err := budget.Get(ctx, "I1")
	if err != nil {
		t.Fatal(err)
	}
	if item.Amount != "37.04" {
		t.Fatalf("amount %s", item.Amount)
	}
	histories, err := graph.ListPriceHistory(ctx, "P1")
	if err != nil || len(histories) != 1 {
		t.Fatalf("history %v %v", histories, err)
	}
	runs, err := graph.ListRuns(ctx, "P1")
	if err != nil || len(runs) != 1 {
		t.Fatalf("runs %v %v", runs, err)
	}
}

func TestDependencyGraphSkipsUnchangedPrice(t *testing.T) {
	ctx := context.Background()
	store, err := Open(ctx, filepath.Join(t.TempDir(), "same.db"))
	if err != nil {
		t.Fatal(err)
	}
	defer store.Close()
	graph := NewDependencyGraphRepository(store)
	history, err := graph.RecordPrice(ctx, "P1", "R1", "1.0000", "1.0000", "TEST")
	if err != nil {
		t.Fatal(err)
	}
	if history != nil {
		t.Fatal("unchanged price must not create history")
	}
}
