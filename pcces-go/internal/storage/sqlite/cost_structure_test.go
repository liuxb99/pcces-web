package sqlite

import (
	"context"
	"path/filepath"
	"testing"
)

func TestCostStructureCatalogAndProjectAssignment(t *testing.T) {
	ctx := context.Background()
	store, err := Open(ctx, filepath.Join(t.TempDir(), "cost.db"))
	if err != nil {
		t.Fatal(err)
	}
	defer store.Close()
	repo := NewCostStructureRepository(store)
	created, err := repo.SaveType(ctx, CostStructureType{ID: "standard", Code: " cs-01 ", Name: "公共工程標準成本結構", Source: "legacy", Version: "2026.1", Enabled: true, CreatedBy: "u1"})
	if err != nil {
		t.Fatal(err)
	}
	if created.Code != "CS-01" || created.RowVersion != 1 {
		t.Fatalf("unexpected type %#v", created)
	}
	assigned, err := repo.AssignProject(ctx, "P100", "standard", "bud", "u1", 0)
	if err != nil {
		t.Fatal(err)
	}
	if assigned.Issue != "BUD" || assigned.TypeCode != "CS-01" {
		t.Fatalf("unexpected assignment %#v", assigned)
	}
	if assigned.DeepLink == "" {
		t.Fatal("deep link required")
	}
}

func TestCostStructureRejectsDisabledAndStaleWrites(t *testing.T) {
	ctx := context.Background()
	store, err := Open(ctx, filepath.Join(t.TempDir(), "cost2.db"))
	if err != nil {
		t.Fatal(err)
	}
	defer store.Close()
	repo := NewCostStructureRepository(store)
	_, err = repo.SaveType(ctx, CostStructureType{ID: "disabled", Code: "CS-X", Name: "停用", Enabled: false, CreatedBy: "u1"})
	if err != nil {
		t.Fatal(err)
	}
	if _, err = repo.AssignProject(ctx, "P1", "disabled", "BUD", "u1", 0); err == nil {
		t.Fatal("disabled type must not be assignable")
	}
	created, err := repo.SaveType(ctx, CostStructureType{ID: "standard", Code: "CS-01", Name: "標準", Enabled: true, CreatedBy: "u1"})
	if err != nil {
		t.Fatal(err)
	}
	if _, err = repo.SaveType(ctx, CostStructureType{ID: "standard", Code: "CS-01", Name: "stale", Enabled: true, CreatedBy: "u2", RowVersion: 0}); err == nil {
		t.Fatal("expected stale type version")
	}
	assigned, err := repo.AssignProject(ctx, "P1", created.ID, "BUD", "u1", 0)
	if err != nil {
		t.Fatal(err)
	}
	if _, err = repo.AssignProject(ctx, "P1", created.ID, "BID", "u2", 0); err == nil {
		t.Fatal("expected stale assignment version")
	}
	changed, err := repo.AssignProject(ctx, "P1", created.ID, "BID", "u2", assigned.RowVersion)
	if err != nil {
		t.Fatal(err)
	}
	if changed.Issue != "BID" {
		t.Fatalf("unexpected issue %#v", changed)
	}
}
