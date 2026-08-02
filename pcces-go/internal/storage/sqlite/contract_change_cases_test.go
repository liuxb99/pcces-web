package sqlite

import (
	"context"
	"testing"
)

func TestContractChangeCaseLifecycle(t *testing.T){
	store:=newTestStore(t);ctx:=context.Background()
	_,err:=store.db.Exec(`INSERT INTO contracts_v2(id,project_code,budget_version_id,contract_no,name,status,contract_amount,created_by,created_at,updated_at,row_version) VALUES('C1','P1','V1','C-1','Main','APPROVED','100.00000000','u','2026-08-02T00:00:00Z','2026-08-02T00:00:00Z',1)`);if err!=nil{t.Fatal(err)}
	_,err=store.db.Exec(`INSERT INTO contract_items_v2(id,contract_id,source_budget_item_id,name,quantity,unit_price,amount,sort_order,created_at) VALUES('I1','C1','B1','Concrete','10.00000000','10.00000000','100.00000000',1,'2026-08-02T00:00:00Z')`);if err!=nil{t.Fatal(err)}
	repo:=NewContractChangeCaseRepository(store)
	item,err:=repo.Create(ctx,ContractChangeCaseRequest{ID:"CC1",ContractID:"C1",ChangeNo:"CO-1",Reason:"scope",Actor:"u",Items:[]ContractChangeItemInput{{Action:"INCREASE",ContractItemID:"I1",QuantityDelta:"2",AmountDelta:"20",UnitPrice:"10"}}});if err!=nil{t.Fatal(err)}
	if item["status"]!="DRAFT"{t.Fatalf("bad draft %#v",item)}
	item,err=repo.Transition(ctx,"CC1","SUBMITTED",1,"u");if err!=nil{t.Fatal(err)}
	item,err=repo.Transition(ctx,"CC1","APPROVED",2,"a");if err!=nil{t.Fatal(err)}
	item,err=repo.Transition(ctx,"CC1","APPLIED",3,"operator");if err!=nil{t.Fatal(err)}
	if item["status"]!="APPLIED"{t.Fatalf("bad applied %#v",item)}
	var amount string;if err=store.db.QueryRow(`SELECT contract_amount FROM contracts_v2 WHERE id='C1'`).Scan(&amount);err!=nil{t.Fatal(err)}
	if amount!="120.00000000"{t.Fatalf("bad amount %s",amount)}
}
