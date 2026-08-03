package sqlite

import (
	"context"
	"testing"
)

func TestBudgetValidationModesClassesRefsAndChecks(t *testing.T) {
	store := newTestStore(t)
	ctx := context.Background()
	budget := NewBudgetDecimalRepository(store)
	itemNo1 := "100"
	itemNo2 := "200"
	for _, item := range []BudgetDecimalItem{
		{ID: "S1", ProjectCode: "P1", ItemNo: &itemNo1, Name: "Source", Kind: "L", Quantity: "2.00", UnitPrice: "0.00", Amount: "0.00", QuantityScale: 2, PriceScale: 2, AmountScale: 2},
		{ID: "S2", ProjectCode: "P1", ItemNo: &itemNo1, Name: "Duplicate", Kind: "L", Quantity: "1.00", UnitPrice: "1.00", Amount: "1.00", QuantityScale: 2, PriceScale: 2, AmountScale: 2},
		{ID: "T1", ProjectCode: "P2", ItemNo: &itemNo2, Name: "Target", Kind: "L", Quantity: "1.00", UnitPrice: "3.00", Amount: "3.00", QuantityScale: 2, PriceScale: 2, AmountScale: 2},
	} {
		if _, err := budget.Save(ctx, item); err != nil {
			t.Fatal(err)
		}
	}
	repo := NewBudgetValidationRepository(store)
	mode, version, err := repo.SetMode(ctx, "P1", "BID", "editor", 0)
	if err != nil || mode != "BID" || version != 1 {
		t.Fatalf("mode=%s version=%d err=%v", mode, version, err)
	}
	if _, _, err = repo.SetMode(ctx, "P1", "BUD", "editor", 0); err == nil {
		t.Fatal("expected optimistic conflict")
	}
	if err = repo.SetItemClass(ctx, "P1", "S1", "A", "editor", 0); err != nil {
		t.Fatal(err)
	}
	if err = repo.SetItemClass(ctx, "P1", "S2", "B", "editor", 0); err != nil {
		t.Fatal(err)
	}
	if err = repo.AddReference(ctx, "R1", "P1", "S1", "P2", "T1", "editor"); err != nil {
		t.Fatal(err)
	}
	if err = repo.AddReference(ctx, "R2", "P1", "S1", "P2", "T1", "editor"); err != nil {
		t.Fatal(err)
	}
	result, err := repo.Check(ctx, "C1", "P1", "editor")
	if err != nil {
		t.Fatal(err)
	}
	if result.Passed {
		t.Fatal("expected blocking validation result")
	}
	codes := map[string]bool{}
	for _, issue := range result.Issues {
		codes[issue.Code] = true
	}
	if !codes["BID_PRICE_REQUIRED"] || !codes["DUPLICATE_ITEM_NO"] {
		t.Fatalf("issues=%+v", result.Issues)
	}
	if result.DeepLink == "" {
		t.Fatal("expected deep link")
	}
}
