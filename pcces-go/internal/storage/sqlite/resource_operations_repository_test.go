package sqlite

import (
	"context"
	"path/filepath"
	"testing"
)

func TestResourceReplacementMovesAndDeduplicatesLinks(t *testing.T) {
	ctx:=context.Background();store,err:=Open(ctx,filepath.Join(t.TempDir(),"replace.db"));if err!=nil{t.Fatal(err)};defer store.Close()
	budget:=NewBudgetDecimalRepository(store);resources:=NewResourceDecimalRepository(store);links:=NewResourceBudgetLineageRepository(store)
	for _,id:=range []string{"I1","I2"}{if _,err=budget.Save(ctx,BudgetDecimalItem{ID:id,ProjectCode:"P1",Name:id,Kind:"L",Quantity:"1",UnitPrice:"10",QuantityScale:2,PriceScale:2,AmountScale:2});err!=nil{t.Fatal(err)}}
	for _,id:=range []string{"R1","R2"}{if _,err=resources.SaveResource(ctx,ResourceDecimal{ID:id,Code:id,Name:id,UnitPrice:"10",PriceScale:2});err!=nil{t.Fatal(err)}}
	if err=links.Link(ctx,"P1","R1","I1");err!=nil{t.Fatal(err)};if err=links.Link(ctx,"P1","R1","I2");err!=nil{t.Fatal(err)};if err=links.Link(ctx,"P1","R2","I2");err!=nil{t.Fatal(err)}
	result,err:=links.ReplaceResource(ctx,"P1","R1","R2","7");if err!=nil{t.Fatal(err)}
	if result.MovedLinks!=1||result.DeduplicatedLinks!=1{t.Fatalf("unexpected result %#v",result)}
	page,err:=links.ListResourceReferences(ctx,"P1","R2",50,0);if err!=nil{t.Fatal(err)};if page.Total!=2{t.Fatalf("total=%d",page.Total)}
	source,err:=links.ListResourceReferences(ctx,"P1","R1",50,0);if err!=nil{t.Fatal(err)};if source.Total!=0{t.Fatalf("source total=%d",source.Total)}
}

func TestBatchResourcePricesPropagateAndRollbackOnConflict(t *testing.T){
	ctx:=context.Background();store,err:=Open(ctx,filepath.Join(t.TempDir(),"batch.db"));if err!=nil{t.Fatal(err)};defer store.Close()
	budget:=NewBudgetDecimalRepository(store);resources:=NewResourceDecimalRepository(store);links:=NewResourceBudgetLineageRepository(store)
	if _,err=budget.Save(ctx,BudgetDecimalItem{ID:"I1",ProjectCode:"P1",Name:"I1",Kind:"L",Quantity:"2",UnitPrice:"10",QuantityScale:2,PriceScale:2,AmountScale:2});err!=nil{t.Fatal(err)}
	if _,err=resources.SaveResource(ctx,ResourceDecimal{ID:"R1",Code:"R1",Name:"R1",UnitPrice:"10",PriceScale:2});err!=nil{t.Fatal(err)}
	if _,err=resources.SaveResource(ctx,ResourceDecimal{ID:"R2",Code:"R2",Name:"R2",UnitPrice:"20",PriceScale:2});err!=nil{t.Fatal(err)}
	if err=links.Link(ctx,"P1","R1","I1");err!=nil{t.Fatal(err)}
	result,err:=links.BatchUpdatePrices(ctx,[]ResourcePriceUpdate{{ResourceID:"R1",UnitPrice:"12.345",RowVersion:1},{ResourceID:"R2",UnitPrice:"25",RowVersion:1}},"");if err!=nil{t.Fatal(err)}
	if result.UpdatedResources!=2||result.UpdatedBudgetItems!=1{t.Fatalf("unexpected result %#v",result)}
	item,err:=budget.Get(ctx,"I1");if err!=nil{t.Fatal(err)};if item.UnitPrice!="12.35"||item.Amount!="24.70"{t.Fatalf("item %#v",item)}
	r1,err:=resources.GetResource(ctx,"R1");if err!=nil{t.Fatal(err)};if r1.UnitPrice!="12.35"{t.Fatalf("price=%s",r1.UnitPrice)}
	_,err=links.BatchUpdatePrices(ctx,[]ResourcePriceUpdate{{ResourceID:"R1",UnitPrice:"30",RowVersion:2},{ResourceID:"R2",UnitPrice:"40",RowVersion:99}},"");if err==nil{t.Fatal("expected conflict")}
	r1,err=resources.GetResource(ctx,"R1");if err!=nil{t.Fatal(err)};if r1.UnitPrice!="12.35"{t.Fatalf("rollback failed price=%s",r1.UnitPrice)}
}
