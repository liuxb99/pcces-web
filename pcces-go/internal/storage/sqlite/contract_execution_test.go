package sqlite

import (
	"context"
	"testing"
)

func TestExecutionInvoiceAndSettlement(t *testing.T){
	store:=newTestStore(t);ctx:=context.Background()
	_,err:=store.db.Exec(`INSERT INTO contracts_v2(id,project_code,budget_version_id,contract_no,name,status,contract_amount,created_by,created_at,updated_at,row_version) VALUES('C1','P1','V1','C-1','Main','APPROVED','100.00000000','u','2026-08-02T00:00:00Z','2026-08-02T00:00:00Z',1)`);if err!=nil{t.Fatal(err)}
	_,err=store.db.Exec(`INSERT INTO contract_items_v2(id,contract_id,source_budget_item_id,name,quantity,unit_price,amount,sort_order,created_at) VALUES('I1','C1','B1','Concrete','10.00000000','10.00000000','100.00000000',1,'2026-08-02T00:00:00Z')`);if err!=nil{t.Fatal(err)}
	repo:=NewExecutionRepository(store)
	invoice,err:=repo.CreateInvoice(ctx,InvoiceCreateRequest{ID:"P1",ContractID:"C1",Actor:"u",Deduction:"5",Retention:"5",Items:[]InvoiceLineInput{{ContractItemID:"I1",CurrentQuantity:"5"}}});if err!=nil{t.Fatal(err)}
	if invoice["net_payable"]!="40.00000000"{t.Fatalf("bad invoice %#v",invoice)}
	invoice,err=repo.TransitionInvoice(ctx,"P1","SUBMITTED",1,"u");if err!=nil{t.Fatal(err)}
	_,err=repo.TransitionInvoice(ctx,"P1","APPROVED",2,"a");if err!=nil{t.Fatal(err)}
	settlement,err:=repo.CreateSettlement(ctx,"S1","C1","10","u");if err!=nil{t.Fatal(err)}
	if settlement["final_amount"]!="60.00000000"{t.Fatalf("bad settlement %#v",settlement)}
}
