package sqlite

import (
	"context"
	"path/filepath"
	"testing"
)

func TestProjectResourceBidirectionalReferences(t *testing.T){
	ctx:=context.Background();store,err:=Open(ctx,filepath.Join(t.TempDir(),"links.db"));if err!=nil{t.Fatal(err)};defer store.Close()
	budget:=NewBudgetDecimalRepository(store)
	for _,item:=range []BudgetDecimalItem{{ID:"I1",ProjectCode:"P1",Name:"工項一",Kind:"L",Quantity:"2",UnitPrice:"10",QuantityScale:2,PriceScale:2,AmountScale:2},{ID:"I2",ProjectCode:"P1",Name:"工項二",Kind:"L",Quantity:"3",UnitPrice:"20",QuantityScale:2,PriceScale:2,AmountScale:2}}{if _,err=budget.Save(ctx,item);err!=nil{t.Fatal(err)}}
	resources:=NewResourceDecimalRepository(store);if _,err=resources.SaveResource(ctx,ResourceDecimal{ID:"R1",Code:"M00001",Name:"水泥",UnitPrice:"12.34",PriceScale:2});err!=nil{t.Fatal(err)}
	repo:=NewResourceBudgetLineageRepository(store);if err=repo.Link(ctx,"P1","R1","I1");err!=nil{t.Fatal(err)};if err=repo.Link(ctx,"P1","R1","I2");err!=nil{t.Fatal(err)}
	page,err:=repo.ListProjectResources(ctx,"P1","水泥",50,0);if err!=nil{t.Fatal(err)};if page.Total!=1||page.Items[0].ReferenceCount!=2{t.Fatalf("page=%+v",page)}
	refs,err:=repo.ListResourceReferences(ctx,"P1","R1",1,0);if err!=nil{t.Fatal(err)};if refs.Total!=2||len(refs.Items)!=1||refs.Items[0].DeepLink!="/app/budget/P1?item=I1"{t.Fatalf("refs=%+v",refs)}
	removed,err:=repo.Unlink(ctx,"P1","R1","I1");if err!=nil||!removed{t.Fatalf("removed=%v err=%v",removed,err)}
	refs,err=repo.ListResourceReferences(ctx,"P1","R1",50,0);if err!=nil||refs.Total!=1||refs.Items[0].BudgetItemID!="I2"{t.Fatalf("refs=%+v err=%v",refs,err)}
	removed,err=repo.Unlink(ctx,"P1","R1","missing");if err!=nil||removed{t.Fatalf("missing unlink removed=%v err=%v",removed,err)}
}
