package sqlite

import (
	"context"
	"errors"
	"path/filepath"
	"testing"

	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

func TestDecimalRepositoryPersistenceAuditAndConflict(t *testing.T) {
	ctx := context.Background()
	store, err := Open(ctx, filepath.Join(t.TempDir(), "decimal.db"))
	if err != nil {
		t.Fatalf("open store: %v", err)
	}
	defer store.Close()
	repo := NewDecimalRepository(store)

	created, err := repo.Create(ctx, "amount-1", "1.005", "actor-1")
	if err != nil {
		t.Fatalf("create: %v", err)
	}
	if created.Value != "1.00500000" || created.RowVersion != 1 {
		t.Fatalf("unexpected created record: %+v", created)
	}
	updated, err := repo.Update(ctx, "amount-1", "100.125", "actor-2", 1)
	if err != nil {
		t.Fatalf("update: %v", err)
	}
	if updated.Value != "100.12500000" || updated.RowVersion != 2 {
		t.Fatalf("unexpected updated record: %+v", updated)
	}
	_, err = repo.Update(ctx, "amount-1", "101", "actor-3", 1)
	var appErr *errx.Error
	if !errors.As(err, &appErr) || appErr.Code != errx.CodeConflict {
		t.Fatalf("expected conflict, got %v", err)
	}
	var auditCount int
	if err := store.DB().QueryRowContext(ctx, `SELECT COUNT(*) FROM p0_audit_events WHERE resource_id='amount-1'`).Scan(&auditCount); err != nil {
		t.Fatalf("count audit: %v", err)
	}
	if auditCount != 2 {
		t.Fatalf("audit count=%d want 2", auditCount)
	}
}

func TestDecimalMigrationContract(t *testing.T) {
	ctx := context.Background()
	store, err := Open(ctx, filepath.Join(t.TempDir(), "migration.db"))
	if err != nil {
		t.Fatalf("open store: %v", err)
	}
	defer store.Close()
	for _, column := range []string{"created_at", "updated_at", "row_version"} {
		var count int
		if err := store.DB().QueryRowContext(ctx, `SELECT COUNT(*) FROM pragma_table_info('p0_decimal_records') WHERE name=?`, column).Scan(&count); err != nil {
			t.Fatalf("inspect %s: %v", column, err)
		}
		if count != 1 {
			t.Fatalf("missing required column %s", column)
		}
	}
}
