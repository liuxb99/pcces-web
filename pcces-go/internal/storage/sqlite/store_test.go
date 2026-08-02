package sqlite

import (
	"context"
	"path/filepath"
	"testing"
)

func TestOpenMigratesAndChecksIntegrity(t *testing.T) {
	t.Parallel()

	ctx := context.Background()
	path := filepath.Join(t.TempDir(), "pcces-test.db")
	store, err := Open(ctx, path)
	if err != nil {
		t.Fatalf("Open() error = %v", err)
	}
	defer store.Close()

	if err := store.IntegrityCheck(ctx); err != nil {
		t.Fatalf("IntegrityCheck() error = %v", err)
	}

	for _, table := range []string{"feature_catalog", "function_codes", "modules", "actions", "work_contexts"} {
		var name string
		err := store.DB().QueryRowContext(ctx,
			"SELECT name FROM sqlite_master WHERE type='table' AND name=?", table,
		).Scan(&name)
		if err != nil {
			t.Fatalf("table %q missing: %v", table, err)
		}
	}

	var count int
	if err := store.DB().QueryRowContext(ctx, "SELECT COUNT(*) FROM function_codes").Scan(&count); err != nil {
		t.Fatalf("count function codes: %v", err)
	}
	if count < 12 {
		t.Fatalf("function code count = %d, want at least 12", count)
	}
}
