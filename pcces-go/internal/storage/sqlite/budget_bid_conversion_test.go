package sqlite

import (
	"context"
	"path/filepath"
	"testing"
)

func TestBudgetBidConversionSessionAndConflict(t *testing.T) {
	ctx := context.Background()
	store, err := Open(ctx, filepath.Join(t.TempDir(), "conversion.db"))
	if err != nil { t.Fatal(err) }
	defer store.Close()
	repo := NewBudgetBidConversionRepository(store)
	req := BudgetBidConversionRequest{
		SourceProjectCode: "BUD-1", SourceBudgetVersionID: "V2", TargetBidProjectCode: "BID-1",
		Mode: "CREATE", ActorID: "u1", BudgetItems: []map[string]any{{"id":"1","code":"a01","name":"work"}},
	}
	item, err := repo.Convert(ctx, req)
	if err != nil { t.Fatal(err) }
	if item.Status != "COMPLETED" || item.ResultSnapshot[0].Code != "A01" { t.Fatalf("unexpected %#v", item) }
	if item.Lineage["source_budget_version_id"] != "V2" { t.Fatal("missing lineage") }
	if _, err = repo.Convert(ctx, req); err == nil { t.Fatal("expected create conflict") }
}

func TestBudgetBidConversionRejectsDuplicateItems(t *testing.T) {
	ctx := context.Background()
	store, err := Open(ctx, filepath.Join(t.TempDir(), "conversion2.db"))
	if err != nil { t.Fatal(err) }
	defer store.Close()
	_, err = NewBudgetBidConversionRepository(store).Convert(ctx, BudgetBidConversionRequest{
		SourceProjectCode:"B", SourceBudgetVersionID:"V", TargetBidProjectCode:"X", Mode:"CREATE",
		BudgetItems: []map[string]any{{"id":"1"},{"id":"1"}},
	})
	if err == nil { t.Fatal("expected duplicate rejection") }
}
