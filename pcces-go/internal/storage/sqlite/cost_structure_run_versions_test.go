package sqlite

import (
	"context"
	"path/filepath"
	"testing"
)

func TestCostStructureRunVersionLinkAndCompare(t *testing.T) {
	ctx := context.Background()
	store, err := Open(ctx, filepath.Join(t.TempDir(), "cost-run-version.db"))
	if err != nil {
		t.Fatal(err)
	}
	defer store.Close()
	repo := NewCostStructureRunVersionRepository(store)
	if _, err = repo.Link(ctx, CostStructureRunVersion{ProjectCode: "P1", RunID: "R1", BudgetVersionID: "BV1", BudgetStatus: "DRAFT", DirectCost: "100", TotalCost: "110", Trace: map[string]any{"order": []string{"A"}}, CreatedBy: "u1"}); err != nil {
		t.Fatal(err)
	}
	if _, err = repo.Link(ctx, CostStructureRunVersion{ProjectCode: "P1", RunID: "R2", BudgetVersionID: "BV2", BudgetStatus: "DRAFT", DirectCost: "120", TotalCost: "135", Trace: map[string]any{}, CreatedBy: "u1"}); err != nil {
		t.Fatal(err)
	}
	diff, err := repo.Compare(ctx, "R1", "R2")
	if err != nil {
		t.Fatal(err)
	}
	if diff.DirectCostDelta != "20" || diff.TotalCostDelta != "25" {
		t.Fatalf("unexpected diff %#v", diff)
	}
}

func TestCostStructureRunVersionRejectsReadonlyAndDuplicate(t *testing.T) {
	ctx := context.Background()
	store, err := Open(ctx, filepath.Join(t.TempDir(), "cost-run-version-guard.db"))
	if err != nil {
		t.Fatal(err)
	}
	defer store.Close()
	repo := NewCostStructureRunVersionRepository(store)
	if _, err = repo.Link(ctx, CostStructureRunVersion{ProjectCode: "P1", RunID: "R0", BudgetVersionID: "BV0", BudgetStatus: "APPROVED", DirectCost: "1", TotalCost: "1", Trace: map[string]any{}, CreatedBy: "u1"}); err == nil {
		t.Fatal("expected readonly rejection")
	}
	item := CostStructureRunVersion{ProjectCode: "P1", RunID: "R1", BudgetVersionID: "BV1", BudgetStatus: "DRAFT", DirectCost: "1", TotalCost: "1", Trace: map[string]any{}, CreatedBy: "u1"}
	if _, err = repo.Link(ctx, item); err != nil {
		t.Fatal(err)
	}
	if _, err = repo.Link(ctx, item); err == nil {
		t.Fatal("expected duplicate rejection")
	}
}

func TestCostStructureRunVersionFailedValidationLeavesNoRow(t *testing.T) {
	ctx := context.Background()
	store, err := Open(ctx, filepath.Join(t.TempDir(), "cost-run-version-rollback.db"))
	if err != nil {
		t.Fatal(err)
	}
	defer store.Close()
	repo := NewCostStructureRunVersionRepository(store)
	if _, err = repo.Link(ctx, CostStructureRunVersion{ProjectCode: "P1", RunID: "BAD", BudgetVersionID: "BV1", BudgetStatus: "DRAFT", DirectCost: "bad", TotalCost: "1", Trace: map[string]any{}, CreatedBy: "u1"}); err == nil {
		t.Fatal("expected validation failure")
	}
	if _, err = repo.Get(ctx, "BAD"); err == nil {
		t.Fatal("failed link must not persist")
	}
}
