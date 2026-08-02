package sqlite

import (
	"context"
	"testing"
)

func TestBidLifecycleConversionVersionAndRollback(t *testing.T){
	store:=newTestStore(t);ctx:=context.Background();budget:=NewBudgetDecimalRepository(store)
	if _,err:=budget.Save(ctx,BudgetDecimalItem{ID:"A",ProjectCode:"BUD-P",Name:"A",Kind:"L",Quantity:"2.00",UnitPrice:"10.00",Amount:"20.00",QuantityScale:2,PriceScale:2,AmountScale:2});err!=nil{t.Fatal(err)}
	repo:=NewBidLifecycleRepository(store)
	run,err:=repo.Convert(ctx,"RUN1","BUD-P","BID-P","editor",false);if err!=nil{t.Fatal(err)};if run["copied_items"].(int)!=1{t.Fatalf("run=%v",run)}
	version,err:=repo.CreateVersion(ctx,"V1","BID-P","baseline","SEALED","reviewer");if err!=nil{t.Fatal(err)};if version.TotalAmount!="20.00"{t.Fatalf("total=%s",version.TotalAmount)}
	item,err:=budget.Get(ctx,"bid-BID-P-A");if err!=nil{t.Fatal(err)};item.UnitPrice="12.50";if _,err=budget.Save(ctx,item);err!=nil{t.Fatal(err)}
	if _,err=repo.Rollback(ctx,"V1","RUN2","reviewer");err!=nil{t.Fatal(err)}
	item,err=budget.Get(ctx,"bid-BID-P-A");if err!=nil{t.Fatal(err)};if item.UnitPrice!="10.00"{t.Fatalf("price=%s",item.UnitPrice)}
	if version.DeepLink==""{t.Fatal("expected deep link")}
}

func TestBidLifecycleRejectsExistingTarget(t *testing.T){
	store:=newTestStore(t);ctx:=context.Background();budget:=NewBudgetDecimalRepository(store)
	for _,v:=range []BudgetDecimalItem{{ID:"A",ProjectCode:"BUD-P",Name:"A",Kind:"L",Quantity:"1",UnitPrice:"1",QuantityScale:2,PriceScale:2,AmountScale:2},{ID:"X",ProjectCode:"BID-P",Name:"X",Kind:"L",Quantity:"1",UnitPrice:"1",QuantityScale:2,PriceScale:2,AmountScale:2}}{if _,err:=budget.Save(ctx,v);err!=nil{t.Fatal(err)}}
	if _,err:=NewBidLifecycleRepository(store).Convert(ctx,"RUN","BUD-P","BID-P","editor",false);err==nil{t.Fatal("expected target conflict")}
}
