package sqlite

import (
	"context"
	"path/filepath"
	"testing"
)

func TestSettingsBackupAndRecovery(t *testing.T) {
	ctx := context.Background()
	dir := t.TempDir()
	dbPath := filepath.Join(dir, "pcces.db")
	store, err := Open(ctx, dbPath)
	if err != nil {
		t.Fatal(err)
	}
	defer store.Close()

	settings := NewSettingsRepository(store)
	current, err := settings.Get(ctx, "autosave.interval_seconds")
	if err != nil {
		t.Fatal(err)
	}
	current.Value = "90"
	updated, err := settings.Save(ctx, current)
	if err != nil {
		t.Fatal(err)
	}
	if updated.Value != "90" || updated.RowVersion != current.RowVersion+1 {
		t.Fatalf("unexpected updated setting: %#v", updated)
	}
	if _, err := settings.Save(ctx, current); err == nil {
		t.Fatal("expected stale setting conflict")
	}

	recovery := NewRecoveryRepository(store)
	snapshot, err := recovery.Create(ctx, RecoverySnapshot{
		ID: "snapshot-1", ActorID: "local-admin", Payload: `{"name":"draft"}`, Reason: "autosave",
	})
	if err != nil {
		t.Fatal(err)
	}
	if snapshot.PayloadHash == "" {
		t.Fatal("expected payload hash")
	}
	pending, err := recovery.ListPending(ctx, "local-admin")
	if err != nil {
		t.Fatal(err)
	}
	if len(pending) != 1 {
		t.Fatalf("expected one pending recovery snapshot, got %d", len(pending))
	}
	restored, err := recovery.MarkRestored(ctx, snapshot.ID, snapshot.RowVersion)
	if err != nil {
		t.Fatal(err)
	}
	if restored.RestoredAt == nil {
		t.Fatal("expected restored timestamp")
	}
	if _, err := recovery.MarkDiscarded(ctx, snapshot.ID, snapshot.RowVersion); err == nil {
		t.Fatal("expected recovery state conflict")
	}

	backupPath := filepath.Join(dir, "backups", "pcces-backup.db")
	info, err := store.Backup(ctx, backupPath)
	if err != nil {
		t.Fatal(err)
	}
	if info.SizeBytes == 0 {
		t.Fatal("backup must not be empty")
	}

	restoredPath := filepath.Join(dir, "restored.db")
	if err := RestoreFrom(ctx, backupPath, restoredPath); err != nil {
		t.Fatal(err)
	}
	restoredStore, err := Open(ctx, restoredPath)
	if err != nil {
		t.Fatal(err)
	}
	defer restoredStore.Close()
	if err := restoredStore.IntegrityCheck(ctx); err != nil {
		t.Fatal(err)
	}
	restoredSetting, err := NewSettingsRepository(restoredStore).Get(ctx, "autosave.interval_seconds")
	if err != nil {
		t.Fatal(err)
	}
	if restoredSetting.Value != "90" {
		t.Fatalf("backup did not preserve setting: %#v", restoredSetting)
	}
}
