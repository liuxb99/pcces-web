package sqlite

import (
	"context"
	"testing"
)

func TestContractChangeRecalculatesAmount(t *testing.T) {
	store := newTestStore(t)
	ctx := context.Background()
	_, err := store.db.Exec(`INSERT INTO contracts_v2(id,project_code,budget_version_id,contract_no,name,status,contract_amount,created_by,created_at,updated_at,row_version) VALUES('C1','P1','V1','C-1','Main','APPROVED','100','u','2026-08-02T00:00:00Z','2026-08-02T00:00:00Z',1)`)
	if err != nil { t.Fatal(err) }
	_, err = store.db.Exec(`INSERT INTO contract_items_v2(id,contract_id,source_budget_item_id,name,quantity,unit_price,amount,sort_order,created_at) VALUES('I1','C1','B1','Concrete','10','10','100',1,'2026-08-02T00:00:00Z')`)
	if err != nil { t.Fatal(err) }
	result, err := NewContractChangeRepository(store).Create(ctx, ContractChangeRequest{ID:"CH1",ContractID:"C1",ChangeNo:"CH-1",Reason:"design",Actor:"u",Items:[]ContractChangeItemInput{{Action:"ADD",Name:"Drainage",QuantityDelta:"2",UnitPrice:"5",AmountDelta:"10"},{Action:"DECREASE",ContractItemID:"I1",Name:"Concrete",QuantityDelta:"1",UnitPrice:"10",AmountDelta:"10"}}})
	if err != nil { t.Fatal(err) }
	if result["after_amount"] != "100.00000000" { t.Fatalf("unexpected result: %#v", result) }
}
