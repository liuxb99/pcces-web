package sqlite

import (
	"context"
	"testing"
)

func TestBudgetCrossProjectPropagationAndDiff(t *testing.T){
	store:=newTestStore(t);ctx:=context.Background();budget:=NewBudgetDecimalRepository(store)
	for _,item:=range []BudgetDecimalItem{
		{ID:"S1",ProjectCode:"BUD-P",ItemNo:"001",Name:"Source",Kind:"L",Quantity:"1.00",UnitPrice:"12.50",Amount:"12.50",QuantityScale:2,PriceScale:2,AmountScale:2},
		{ID:"S2",ProjectCode:"BUD-P",ItemNo:"002",Name:"Removed",Kind:"L",Quantity:"1.00",UnitPrice:"5.00",Amount:"5.00",QuantityScale:2,PriceScale:2,AmountScale:2},
		{ID:"T1",ProjectCode:"BID-P",ItemNo:"001",Name:"Target",Kind:"L",Quantity:"3.00",UnitPrice:"0.00",Amount:"0.00",QuantityScale:2,PriceScale:2,AmountScale:2},
		{ID:"T3",ProjectCode:"BID-P",ItemNo:"003",Name:"Added",Kind:"L",Quantity:"1.00",UnitPrice:"7.00",Amount:"7.00",QuantityScale:2,PriceScale:2,AmountScale:2},
	}{if _,err:=budget.Save(ctx,item);err!=nil{t.Fatal(err)}}
	validation:=NewBudgetValidationRepository(store)
	if err:=validation.AddReference(ctx,"REF1","BUD-P","S1","BID-P","T1","editor");err!=nil{t.Fatal(err)}
	repo:=NewBudgetCrossProjectSyncRepository(store)
	run,err:=repo.Propagate(ctx,"RUN1","BUD-P","BID-P","editor");if err!=nil{t.Fatal(err)}
	if run.Status!="COMPLETED"{t.Fatalf("status=%s",run.Status)}
	target,err:=budget.Get(ctx,"T1");if err!=nil{t.Fatal(err)}
	if target.UnitPrice!="12.50"||target.Amount!="37.50"{t.Fatalf("target=%+v",target)}
	if run.DeepLink==""{t.Fatal("expected deep link")}
	diff,err:=repo.Diff(ctx,"RUN2","BUD-P","BID-P","reviewer");if err!=nil{t.Fatal(err)}
	if len(diff.Result["added"].([]map[string]any))!=1{t.Fatalf("added=%v",diff.Result["added"])}
	if len(diff.Result["removed"].([]map[string]any))!=1{t.Fatalf("removed=%v",diff.Result["removed"])}
	if len(diff.Result["changed"].([]map[string]any))!=1{t.Fatalf("changed=%v",diff.Result["changed"])}
}
