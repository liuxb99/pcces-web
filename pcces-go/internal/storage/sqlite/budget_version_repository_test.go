package sqlite

import (
	"context"
	"path/filepath"
	"testing"
)

func TestBudgetVersionRepositorySnapshotsLocksAndRestores(t *testing.T) {
	ctx := context.Background()
	store, err := Open(ctx, filepath.Join(t.TempDir(), "versions.db"))
	if err != nil {
		t.Fatal(err)
	}
	defer store.Close()
	budget := NewBudgetDecimalRepository(store)
	item, err := budget.Save(ctx, BudgetDecimalItem{ID: "I1", ProjectCode: "P1", Name: "Item", Kind: "L", Quantity: "2.00", UnitPrice: "10.00", Amount: "0", QuantityScale: 2, PriceScale: 2, AmountScale: 2})
	if err != nil {
		t.Fatal(err)
	}
	versions := NewBudgetVersionRepository(store)
	v1, err := versions.Create(ctx, "V1", "P1", "baseline", "APPROVED", "7")
	if err != nil {
		t.Fatal(err)
	}
	item.UnitPrice = "12.50"
	item.RowVersion = 1
	if _, err = budget.Save(ctx, item); err != nil {
		t.Fatal(err)
	}
	if _, err = versions.Create(ctx, "V2", "P1", "changed", "DRAFT", "7"); err != nil {
		t.Fatal(err)
	}
	if _, err = versions.SetLock(ctx, "P1", true, "7", "approved"); err != nil {
		t.Fatal(err)
	}
	if _, err = versions.Restore(ctx, v1.ID, "7", "V3"); err == nil {
		t.Fatal("expected locked restore conflict")
	}
	if _, err = versions.SetLock(ctx, "P1", false, "7", ""); err != nil {
		t.Fatal(err)
	}
	restored, err := versions.Restore(ctx, v1.ID, "7", "V3")
	if err != nil {
		t.Fatal(err)
	}
	if restored.Status != "RESTORED" {
		t.Fatalf("status %s", restored.Status)
	}
	current, err := budget.Get(ctx, "I1")
	if err != nil {
		t.Fatal(err)
	}
	if current.UnitPrice != "10.00" {
		t.Fatalf("price %s", current.UnitPrice)
	}
	rows, err := versions.List(ctx, "P1")
	if err != nil {
		t.Fatal(err)
	}
	if len(rows) != 3 {
		t.Fatalf("versions %d", len(rows))
	}
}
