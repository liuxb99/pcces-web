package sqlite

import (
	"context"
	"testing"
)

func TestMRSCatalogHistoryBookmarksAndAnalysis(t *testing.T) {
	store := newTestStore(t)
	ctx := context.Background()
	repo := NewMRSCatalogRepository(store)
	material, err := repo.SaveItem(ctx, MRSCatalogItem{ID: "M1", Code: "M-001", Name: "Cement", Category: "MATERIAL", CurrentPrice: "180.125", PriceScale: 2, Enabled: true}, "7", "2026-08-02")
	if err != nil {
		t.Fatal(err)
	}
	if material.CurrentPrice != "180.13" {
		t.Fatalf("price=%s", material.CurrentPrice)
	}
	labor, err := repo.SaveItem(ctx, MRSCatalogItem{ID: "L1", Code: "L-001", Name: "Labor", Category: "LABOR", CurrentPrice: "2500", PriceScale: 2, Enabled: true}, "7", "")
	if err != nil {
		t.Fatal(err)
	}
	material.CurrentPrice = "190.00"
	material.RowVersion = 1
	material, err = repo.SaveItem(ctx, material, "7", "2026-08-03")
	if err != nil {
		t.Fatal(err)
	}
	history, err := repo.History(ctx, "M1")
	if err != nil || len(history) != 2 {
		t.Fatalf("history=%d err=%v", len(history), err)
	}
	if err = repo.SetBookmark(ctx, "7", "M1", true); err != nil {
		t.Fatal(err)
	}
	result, err := repo.SaveRecipe(ctx, "R1", "A-001", "Concrete", nil, 2, []MRSAnalysisComponent{{ID: "C1", CatalogItemID: "M1", Quantity: "2.5", QuantityScale: 2}, {ID: "C2", CatalogItemID: "L1", Quantity: "0.1", QuantityScale: 2}}, 0)
	if err != nil {
		t.Fatal(err)
	}
	if result.UnitPrice != "725.00" {
		t.Fatalf("unit price=%s", result.UnitPrice)
	}
	if len(result.Components) != 2 {
		t.Fatalf("components=%d", len(result.Components))
	}
	_ = labor
}

func TestMRSCatalogOptimisticConflict(t *testing.T) {
	store := newTestStore(t)
	ctx := context.Background()
	repo := NewMRSCatalogRepository(store)
	item, err := repo.SaveItem(ctx, MRSCatalogItem{ID: "E1", Code: "E-001", Name: "Excavator", Category: "EQUIPMENT", CurrentPrice: "1000", PriceScale: 2, Enabled: true}, "7", "")
	if err != nil {
		t.Fatal(err)
	}
	item.CurrentPrice = "1200"
	item.RowVersion = 0
	if _, err = repo.SaveItem(ctx, item, "7", ""); err == nil {
		t.Fatal("expected row version conflict")
	}
}
