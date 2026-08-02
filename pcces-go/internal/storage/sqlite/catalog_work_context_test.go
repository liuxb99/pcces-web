package sqlite

import (
	"context"
	"errors"
	"path/filepath"
	"testing"

	"github.com/liuxb99/pcces-web/pcces-go/internal/domain/workcontext"
	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

func TestCatalogRepositorySeedsLegacyCatalogs(t *testing.T) {
	ctx := context.Background()
	store, err := Open(ctx, filepath.Join(t.TempDir(), "catalog.db"))
	if err != nil {
		t.Fatalf("open store: %v", err)
	}
	defer store.Close()

	repo := NewCatalogRepository(store)
	modules, err := repo.ListModules(ctx)
	if err != nil {
		t.Fatalf("list modules: %v", err)
	}
	if len(modules) != 4 {
		t.Fatalf("expected 4 modules, got %d", len(modules))
	}

	codes, err := repo.ListFunctionCodes(ctx)
	if err != nil {
		t.Fatalf("list function codes: %v", err)
	}
	if len(codes) != 12 {
		t.Fatalf("expected 12 function codes, got %d", len(codes))
	}

	capability, err := repo.Capability(ctx, "BUD")
	if err != nil {
		t.Fatalf("resolve capability: %v", err)
	}
	if !capability.Allowed {
		t.Fatalf("expected BUD action to be allowed, reason=%s", capability.ReasonCode)
	}

	if _, err := store.DB().ExecContext(ctx, `UPDATE modules SET enabled = 0 WHERE code = 'BUDGET'`); err != nil {
		t.Fatalf("disable module: %v", err)
	}
	capability, err = repo.Capability(ctx, "BUD")
	if err != nil {
		t.Fatalf("resolve disabled capability: %v", err)
	}
	if capability.Allowed || capability.ReasonCode != "MODULE_DISABLED" {
		t.Fatalf("expected MODULE_DISABLED, got %+v", capability)
	}
}

func TestWorkContextRepositoryUsesOptimisticLocking(t *testing.T) {
	ctx := context.Background()
	store, err := Open(ctx, filepath.Join(t.TempDir(), "context.db"))
	if err != nil {
		t.Fatalf("open store: %v", err)
	}
	defer store.Close()

	repo := NewWorkContextRepository(store)
	projectCode := "P-001"
	draft := `{"selection":[1,2]}`
	created, err := repo.Save(ctx, workcontext.SaveRequest{
		ID: "ctx-1", ActorID: "local-user", ActionCode: "BUD",
		ProjectCode: &projectCode, Dirty: true, DraftPayload: &draft,
	})
	if err != nil {
		t.Fatalf("create context: %v", err)
	}
	if created.RowVersion != 1 || !created.Dirty {
		t.Fatalf("unexpected created context: %+v", created)
	}

	updated, err := repo.Save(ctx, workcontext.SaveRequest{
		ID: "ctx-1", ActorID: "local-user", ActionCode: "BUD",
		ProjectCode: &projectCode, Dirty: false, RowVersion: created.RowVersion,
	})
	if err != nil {
		t.Fatalf("update context: %v", err)
	}
	if updated.RowVersion != 2 || updated.Dirty {
		t.Fatalf("unexpected updated context: %+v", updated)
	}

	_, err = repo.Save(ctx, workcontext.SaveRequest{
		ID: "ctx-1", ActorID: "local-user", ActionCode: "BUD",
		ProjectCode: &projectCode, Dirty: true, RowVersion: 1,
	})
	if err == nil {
		t.Fatal("expected stale row_version conflict")
	}
	var appErr *errx.Error
	if !errors.As(err, &appErr) || appErr.Code != errx.CodeConflict {
		t.Fatalf("expected PCCES_CONFLICT, got %v", err)
	}

	if err := repo.Delete(ctx, "ctx-1", updated.RowVersion); err != nil {
		t.Fatalf("delete context: %v", err)
	}
}
