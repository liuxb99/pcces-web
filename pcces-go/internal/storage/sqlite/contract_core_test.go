package sqlite

import (
	"context"
	"testing"
)

func TestContractCoreEligibilityAndCreate(t *testing.T) {
	store := newTestStore(t)
	ctx := context.Background()
	_, err := store.db.Exec(`INSERT INTO budget_versions(id,project_code,label,status,snapshot_json,created_by,created_at) VALUES('V1','P1','Approved','APPROVED','[]','u','2026-08-02T00:00:00Z')`)
	if err != nil { t.Fatal(err) }
	repo := NewContractCoreRepository(store)
	eligible, err := repo.Eligibility(ctx, "P1", "V1")
	if err != nil || !eligible.Eligible { t.Fatalf("unexpected eligibility: %#v %v", eligible, err) }
	created, err := repo.Create(ctx, ContractCreateRequest{ID:"C1",ProjectCode:"P1",BudgetVersionID:"V1",ContractNo:"C-001",Name:"Main",Actor:"tester",ContractAmount:"20",Items:[]ContractItemInput{{SourceBudgetItemID:"B1",Name:"Concrete",Quantity:"2",UnitPrice:"10",Amount:"20"}}})
	if err != nil { t.Fatal(err) }
	items := created["items"].([]map[string]any)
	if items[0]["source_budget_item_id"] != "B1" { t.Fatalf("lineage missing: %#v", created) }
}
