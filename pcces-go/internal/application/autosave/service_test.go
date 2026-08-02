package autosave

import (
	"context"
	"path/filepath"
	"testing"

	"github.com/liuxb99/pcces-web/pcces-go/internal/domain/workcontext"
	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func TestRunOnceCreatesOnlyChangedSnapshots(t *testing.T) {
	ctx := context.Background()
	store, err := sqlite.Open(ctx, filepath.Join(t.TempDir(), "autosave.db"))
	if err != nil {
		t.Fatalf("open store: %v", err)
	}
	defer store.Close()

	contexts := sqlite.NewWorkContextRepository(store)
	payload := `{"name":"draft one"}`
	created, err := contexts.Save(ctx, workcontext.SaveRequest{
		ID: "ctx-1", ActorID: "local-admin", ActionCode: "BUD",
		Dirty: true, DraftPayload: &payload,
	})
	if err != nil {
		t.Fatalf("create work context: %v", err)
	}

	service := New(nil, store)
	first, err := service.RunOnce(ctx)
	if err != nil {
		t.Fatalf("first autosave: %v", err)
	}
	if first.Created != 1 || first.Scanned != 1 {
		t.Fatalf("unexpected first result: %+v", first)
	}

	second, err := service.RunOnce(ctx)
	if err != nil {
		t.Fatalf("second autosave: %v", err)
	}
	if second.Created != 0 || second.Skipped != 1 {
		t.Fatalf("expected duplicate snapshot to be skipped: %+v", second)
	}

	updatedPayload := `{"name":"draft two"}`
	_, err = contexts.Save(ctx, workcontext.SaveRequest{
		ID: created.ID, ActorID: created.ActorID, ActionCode: created.ActionCode,
		Dirty: true, DraftPayload: &updatedPayload, RowVersion: created.RowVersion,
	})
	if err != nil {
		t.Fatalf("update work context: %v", err)
	}

	third, err := service.RunOnce(ctx)
	if err != nil {
		t.Fatalf("third autosave: %v", err)
	}
	if third.Created != 1 {
		t.Fatalf("expected changed payload snapshot, got %+v", third)
	}

	pending, err := sqlite.NewRecoveryRepository(store).ListPending(ctx, "local-admin")
	if err != nil {
		t.Fatalf("list pending snapshots: %v", err)
	}
	if len(pending) != 2 {
		t.Fatalf("expected two distinct pending snapshots, got %d", len(pending))
	}
}
