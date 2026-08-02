package sqlite

import (
	"context"
	"crypto/sha256"
	"encoding/hex"
	"encoding/json"
	"os"
	"path/filepath"
	"runtime"
	"testing"
)

func TestRecoverySnapshotLifecycleAndHash(t *testing.T) {
	ctx := context.Background()
	store, err := Open(ctx, filepath.Join(t.TempDir(), "recovery.db"))
	if err != nil { t.Fatalf("open store: %v", err) }
	defer store.Close()
	repo := NewRecoveryRepository(store)
	payload := `{"name":"unsaved"}`
	item, err := repo.Create(ctx, RecoverySnapshot{ID:"snap-1", ActorID:"local-admin", Payload:payload, Reason:"CRASH"})
	if err != nil { t.Fatalf("create snapshot: %v", err) }
	digest := sha256.Sum256([]byte(payload))
	if item.PayloadHash != hex.EncodeToString(digest[:]) { t.Fatalf("unexpected payload hash %s", item.PayloadHash) }

	restored, err := repo.MarkRestored(ctx, item.ID, item.RowVersion)
	if err != nil { t.Fatalf("restore snapshot: %v", err) }
	if restored.RestoredAt == nil || restored.RowVersion != 2 { t.Fatalf("unexpected restored state: %+v", restored) }
	if _, err := repo.MarkDiscarded(ctx, item.ID, restored.RowVersion); err == nil { t.Fatal("terminal snapshot must reject second resolution") }
	pending, err := repo.ListPending(ctx, "local-admin")
	if err != nil { t.Fatal(err) }
	if len(pending) != 0 { t.Fatalf("resolved snapshot remained pending: %+v", pending) }
}

func TestRecoveryGoldenFixtureHasRequiredConflictCases(t *testing.T) {
	_, filename, _, ok := runtime.Caller(0)
	if !ok { t.Fatal("resolve test path") }
	path := filepath.Clean(filepath.Join(filepath.Dir(filename), "../../../../specs/golden/recovery-snapshot-transitions.json"))
	body, err := os.ReadFile(path)
	if err != nil { t.Fatal(err) }
	var fixture struct { Cases []struct { Name string `json:"name"`; Expected struct { Allowed bool `json:"allowed"`; Reason string `json:"reason"` } `json:"expected"` } `json:"cases"` }
	if err := json.Unmarshal(body, &fixture); err != nil { t.Fatal(err) }
	seen := map[string]bool{}
	for _, item := range fixture.Cases { seen[item.Name] = true }
	for _, name := range []string{"restore_pending", "discard_pending", "stale_restore", "restore_twice", "discard_after_restore"} {
		if !seen[name] { t.Fatalf("missing recovery golden case %s", name) }
	}
}
