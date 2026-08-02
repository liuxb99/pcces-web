package sqlite

import (
	"context"
	"testing"
)

func TestContractGovernanceLifecycle(t *testing.T) {
	store := newTestStore(t)
	ctx := context.Background()
	_, err := store.db.Exec(`INSERT INTO budget_versions(id,project_code,label,status,snapshot_json,created_by,created_at) VALUES('V1','P1','Approved','APPROVED','[]','u','2026-08-02T00:00:00Z')`)
	if err != nil { t.Fatal(err) }
	core := NewContractCoreRepository(store)
	_, err = core.Create(ctx, ContractCreateRequest{ID:"C1",ProjectCode:"P1",BudgetVersionID:"V1",ContractNo:"C-1",Name:"Main",Actor:"u",ContractAmount:"20",Items:[]ContractItemInput{{SourceBudgetItemID:"B1",Name:"Concrete",Quantity:"2",UnitPrice:"10",Amount:"20"}}})
	if err != nil { t.Fatal(err) }
	repo := NewContractGovernanceRepository(store)
	version, err := repo.CreateVersion(ctx, "CV1", "C1", 1, "baseline", "u")
	if err != nil { t.Fatal(err) }
	if version["status"] != "DRAFT" { t.Fatalf("unexpected version: %#v", version) }
	version, err = repo.Transition(ctx, "CV1", 1, "SUBMITTED", "reviewer")
	if err != nil { t.Fatal(err) }
	version, err = repo.Transition(ctx, "CV1", 2, "APPROVED", "approver")
	if err != nil { t.Fatal(err) }
	if version["approved_by"] != "approver" { t.Fatalf("approval audit missing: %#v", version) }
	version, err = repo.Transition(ctx, "CV1", 3, "LOCKED", "approver")
	if err != nil { t.Fatal(err) }
	if version["status"] != "LOCKED" { t.Fatalf("expected locked: %#v", version) }
	if _, err = repo.CreateVersion(ctx, "CV2", "C1", 3, "edit", "u"); err == nil { t.Fatal("expected approved contract read-only rejection") }
}
