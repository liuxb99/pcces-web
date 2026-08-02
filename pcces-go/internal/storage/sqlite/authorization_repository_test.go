package sqlite

import (
	"context"
	"path/filepath"
	"testing"

	"github.com/liuxb99/pcces-web/pcces-go/internal/domain/authorization"
	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

func TestAuthorizationDecisionAndOptimisticLocking(t *testing.T) {
	ctx := context.Background()
	store, err := Open(ctx, filepath.Join(t.TempDir(), "authz.db"))
	if err != nil {
		t.Fatalf("open store: %v", err)
	}
	defer store.Close()

	repo := NewAuthorizationRepository(store)
	decision, err := repo.Decide(ctx, "local-admin", "BUD")
	if err != nil {
		t.Fatalf("decide default capability: %v", err)
	}
	if !decision.Allowed || decision.FunctionCode != "F003" || decision.ModuleCode != "BUDGET" {
		t.Fatalf("unexpected default decision: %+v", decision)
	}

	if err := repo.SetFunctionGrant(ctx, authorization.GrantRequest{
		ActorID: "local-admin", FunctionCode: "F003", Granted: false, RowVersion: 1,
	}); err != nil {
		t.Fatalf("revoke F003: %v", err)
	}
	decision, err = repo.Decide(ctx, "local-admin", "BUD")
	if err != nil {
		t.Fatalf("decide after revoke: %v", err)
	}
	if decision.Allowed || decision.Reason != "FUNCTION_NOT_GRANTED" {
		t.Fatalf("expected function denial, got %+v", decision)
	}

	err = repo.SetFunctionGrant(ctx, authorization.GrantRequest{
		ActorID: "local-admin", FunctionCode: "F003", Granted: true, RowVersion: 1,
	})
	if appErr, ok := errx.As(err); !ok || appErr.Code != errx.CodeConflict {
		t.Fatalf("expected stale row_version conflict, got %v", err)
	}

	if err := repo.SetModuleEntitlement(ctx, authorization.EntitlementRequest{
		ActorID: "local-admin", ModuleCode: "BUDGET", Enabled: false, RowVersion: 1,
	}); err != nil {
		t.Fatalf("disable budget module: %v", err)
	}
	decision, err = repo.Decide(ctx, "local-admin", "BUD")
	if err != nil {
		t.Fatalf("decide after module disable: %v", err)
	}
	if decision.Allowed || decision.Reason != "MODULE_NOT_ENTITLED" {
		t.Fatalf("expected module denial, got %+v", decision)
	}
}

func TestAuthorizationAuditEvents(t *testing.T) {
	ctx := context.Background()
	store, err := Open(ctx, filepath.Join(t.TempDir(), "audit.db"))
	if err != nil {
		t.Fatalf("open store: %v", err)
	}
	defer store.Close()

	repo := NewAuthorizationRepository(store)
	if err := repo.SetFunctionGrant(ctx, authorization.GrantRequest{
		ActorID: "local-admin", FunctionCode: "F010", Granted: false, RowVersion: 1,
	}); err != nil {
		t.Fatalf("change function grant: %v", err)
	}

	var count int
	if err := store.DB().QueryRowContext(ctx, `SELECT COUNT(*) FROM audit_events WHERE event_type='AUTHZ_FUNCTION_GRANT_CHANGED' AND resource_id='F010'`).Scan(&count); err != nil {
		t.Fatalf("query audit events: %v", err)
	}
	if count != 1 {
		t.Fatalf("expected one audit event, got %d", count)
	}
}
