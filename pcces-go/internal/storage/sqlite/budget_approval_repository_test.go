package sqlite

import (
	"context"
	"path/filepath"
	"testing"
)

func TestBudgetApprovalWorkflowRolesLocksAndAudit(t *testing.T) {
	ctx := context.Background()
	store, err := Open(ctx, filepath.Join(t.TempDir(), "approval.db"))
	if err != nil {
		t.Fatal(err)
	}
	defer store.Close()
	repo := NewBudgetApprovalRepository(store)
	state, err := repo.Transition(ctx, "P1", "SUBMIT", "editor-1", "editor", "ready", 0)
	if err != nil {
		t.Fatal(err)
	}
	if state.Status != "SUBMITTED" || state.RowVersion != 1 {
		t.Fatalf("state %#v", state)
	}
	if err = repo.AssertWritable(ctx, "P1", ""); err == nil {
		t.Fatal("submitted budget must be read-only")
	}
	if _, err = repo.Transition(ctx, "P1", "APPROVE", "editor-1", "editor", "", 1); err == nil {
		t.Fatal("editor must not approve")
	}
	state, err = repo.Transition(ctx, "P1", "RETURN", "reviewer-1", "reviewer", "fix", 1)
	if err != nil {
		t.Fatal(err)
	}
	if state.Status != "RETURNED" {
		t.Fatalf("status %s", state.Status)
	}
	if err = repo.AssertWritable(ctx, "P1", ""); err != nil {
		t.Fatal(err)
	}
	state, err = repo.Transition(ctx, "P1", "SUBMIT", "editor-1", "editor", "fixed", 2)
	if err != nil {
		t.Fatal(err)
	}
	state, err = repo.Transition(ctx, "P1", "APPROVE", "reviewer-1", "reviewer", "ok", 3)
	if err != nil {
		t.Fatal(err)
	}
	if state.Status != "APPROVED" {
		t.Fatalf("status %s", state.Status)
	}
	audits, err := repo.Audits(ctx, "P1")
	if err != nil {
		t.Fatal(err)
	}
	if len(audits) != 4 {
		t.Fatalf("audit count %d", len(audits))
	}
}

func TestBudgetApprovalItemLockAndOptimisticConflict(t *testing.T) {
	ctx := context.Background()
	store, err := Open(ctx, filepath.Join(t.TempDir(), "item-lock.db"))
	if err != nil {
		t.Fatal(err)
	}
	defer store.Close()
	repo := NewBudgetApprovalRepository(store)
	if _, err = repo.SetItemLock(ctx, "P1", "I1", true, "editor", "editor", "x"); err == nil {
		t.Fatal("editor must not lock")
	}
	lock, err := repo.SetItemLock(ctx, "P1", "I1", true, "reviewer", "reviewer", "review")
	if err != nil {
		t.Fatal(err)
	}
	if !lock.Locked {
		t.Fatal("expected locked")
	}
	if err = repo.AssertWritable(ctx, "P1", "I1"); err == nil {
		t.Fatal("locked item must reject writes")
	}
	if _, err = repo.SetItemLock(ctx, "P1", "I1", false, "reviewer", "reviewer", ""); err != nil {
		t.Fatal(err)
	}
	if err = repo.AssertWritable(ctx, "P1", "I1"); err != nil {
		t.Fatal(err)
	}
	if _, err = repo.Transition(ctx, "P1", "SUBMIT", "editor", "editor", "", 0); err != nil {
		t.Fatal(err)
	}
	if _, err = repo.Transition(ctx, "P1", "RETURN", "reviewer", "reviewer", "", 0); err == nil {
		t.Fatal("expected stale conflict")
	}
}
