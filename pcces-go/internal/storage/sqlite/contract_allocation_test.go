package sqlite

import (
	"context"
	"testing"
)

func TestContractAllocationLimitsAndSubcontract(t *testing.T) {
	store := newTestStore(t)
	ctx := context.Background()
	_, err := store.db.Exec(`INSERT INTO budget_versions(id,project_code,label,status,snapshot_json,created_by,created_at) VALUES('V1','P1','Approved','APPROVED','[{"id":"B1","item_no":"001","name":"Concrete","unit":"m3","quantity":"10","unit_price":"100","amount":"1000"}]','u','2026-08-02T00:00:00Z')`)
	if err != nil {
		t.Fatal(err)
	}
	core := NewContractCoreRepository(store)
	_, err = core.Create(ctx, ContractCreateRequest{ID: "C1", ProjectCode: "P1", BudgetVersionID: "V1", ContractNo: "MAIN", Name: "Main", Actor: "u", ContractAmount: "400", Items: []ContractItemInput{{SourceBudgetItemID: "B1", Name: "Concrete", Quantity: "4", UnitPrice: "100", Amount: "400"}}})
	if err != nil {
		t.Fatal(err)
	}
	_, err = core.Create(ctx, ContractCreateRequest{ID: "C2", ProjectCode: "P1", BudgetVersionID: "V1", ContractNo: "SUB", Name: "Sub", Actor: "u", ContractAmount: "0", Items: []ContractItemInput{{SourceBudgetItemID: "X", Name: "Placeholder", Quantity: "0", UnitPrice: "0", Amount: "0"}}})
	if err != nil {
		t.Fatal(err)
	}
	repo := NewContractAllocationRepository(store)
	basis, err := repo.Basis(ctx, "C1")
	if err != nil {
		t.Fatal(err)
	}
	items := basis["items"].([]map[string]any)
	if items[0]["remaining_quantity"] != "6.00000000" {
		t.Fatalf("unexpected basis: %#v", basis)
	}
	_, err = repo.AddItems(ctx, "C2", 1, []ContractAllocationItem{{SourceBudgetItemID: "B1", Name: "Concrete", Quantity: "7", UnitPrice: "100", Amount: "700"}})
	if err == nil {
		t.Fatal("expected over-allocation conflict")
	}
	link, err := repo.LinkSubcontract(ctx, "L1", "C1", "C2", "u")
	if err != nil {
		t.Fatal(err)
	}
	if link["parent_contract_id"] != "C1" {
		t.Fatalf("unexpected link: %#v", link)
	}
}
