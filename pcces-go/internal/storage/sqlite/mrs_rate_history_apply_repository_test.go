package sqlite

import (
	"context"
	"encoding/json"
	"path/filepath"
	"testing"
	"time"
)

func TestApplyHistoricalRatesRestoresSnapshotAndRejectsStaleVersion(t *testing.T) {
	ctx := context.Background()
	store, err := Open(ctx, filepath.Join(t.TempDir(), "mrs-rate-history.db"))
	if err != nil {
		t.Fatal(err)
	}
	defer store.Close()
	repo := NewMRSCatalogRepository(store)
	if _, err = repo.SaveItem(ctx, MRSCatalogItem{ID: "mat-1", Code: "M0000100000", Name: "材料", Category: "MATERIAL", CurrentPrice: "10", PriceScale: 4, Enabled: true}, "tester", ""); err != nil {
		t.Fatal(err)
	}
	if _, err = repo.SaveItem(ctx, MRSCatalogItem{ID: "lab-1", Code: "L000010000000", Name: "人工", Category: "LABOR", CurrentPrice: "20", PriceScale: 4, Enabled: true}, "tester", ""); err != nil {
		t.Fatal(err)
	}
	original, err := repo.SaveRecipe(ctx, "recipe-1", "R1", "分析", nil, 4, []MRSAnalysisComponent{
		{ID: "c1", CatalogItemID: "mat-1", Quantity: "2.5", QuantityScale: 4},
		{ID: "c2", CatalogItemID: "lab-1", Quantity: "1.25", QuantityScale: 4},
	}, 0)
	if err != nil {
		t.Fatal(err)
	}
	snapshot, _ := json.Marshal(original)
	now := time.Now().UTC().Format(time.RFC3339Nano)
	if _, err = store.db.ExecContext(ctx, `INSERT INTO mrs_recipe_versions(id,recipe_id,label,unit_price,snapshot_json,created_by,created_at) VALUES(?,?,?,?,?,?,?)`, "version-1", "recipe-1", "baseline", original.UnitPrice, string(snapshot), "tester", now); err != nil {
		t.Fatal(err)
	}
	current, err := repo.SaveRecipe(ctx, "recipe-1", "R1", "分析", nil, 4, []MRSAnalysisComponent{{ID: "c3", CatalogItemID: "mat-1", Quantity: "9", QuantityScale: 4}}, original.RowVersion)
	if err != nil {
		t.Fatal(err)
	}
	result, err := repo.ApplyHistoricalRates(ctx, "recipe-1", "version-1", current.RowVersion, "tester")
	if err != nil {
		t.Fatal(err)
	}
	if result.ComponentCount != 2 || result.AppliedComponents[0].Quantity != "2.5000" {
		t.Fatalf("unexpected result %#v", result)
	}
	restored, err := repo.CalculateRecipe(ctx, "recipe-1")
	if err != nil {
		t.Fatal(err)
	}
	if len(restored.Components) != 2 || restored.RowVersion != current.RowVersion+1 {
		t.Fatalf("unexpected restored %#v", restored)
	}
	if _, err = repo.ApplyHistoricalRates(ctx, "recipe-1", "version-1", current.RowVersion, "tester"); err == nil {
		t.Fatal("expected stale row version conflict")
	}
	afterConflict, err := repo.CalculateRecipe(ctx, "recipe-1")
	if err != nil {
		t.Fatal(err)
	}
	if len(afterConflict.Components) != 2 {
		t.Fatal("conflict changed components")
	}
}

func TestApplyHistoricalRatesRejectsWrongRecipeVersion(t *testing.T) {
	ctx := context.Background()
	store, err := Open(ctx, filepath.Join(t.TempDir(), "mrs-rate-history-missing.db"))
	if err != nil {
		t.Fatal(err)
	}
	defer store.Close()
	repo := NewMRSCatalogRepository(store)
	if _, err = repo.SaveRecipe(ctx, "recipe-1", "R1", "分析", nil, 4, nil, 0); err != nil {
		t.Fatal(err)
	}
	if _, err = repo.ApplyHistoricalRates(ctx, "recipe-1", "missing", 1, "tester"); err == nil {
		t.Fatal("expected missing version error")
	}
}
