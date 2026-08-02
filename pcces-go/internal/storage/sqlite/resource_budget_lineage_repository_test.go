package sqlite

import (
	"context"
	"path/filepath"
	"testing"
)

func TestResourceBudgetLineagePropagatesExactPrice(t *testing.T) {
	ctx := context.Background()
	store, err := Open(ctx, filepath.Join(t.TempDir(), "lineage.db"))
	if err != nil { t.Fatal(err) }
	defer store.Close()

	budget := NewBudgetDecimalRepository(store)
	if _, err = budget.Save(ctx, BudgetDecimalItem{ID:"I1",ProjectCode:"P1",Name:"工項",Kind:"L",Quantity:"3.0000",UnitPrice:"10.0000",QuantityScale:4,PriceScale:4,AmountScale:2}); err != nil { t.Fatal(err) }
	resources := NewResourceDecimalRepository(store)
	if _, err = resources.SaveResource(ctx, ResourceDecimal{ID:"R1",Code:"R1",Name:"材料",UnitPrice:"12.3456",PriceScale:4}); err != nil { t.Fatal(err) }

	repo := NewResourceBudgetLineageRepository(store)
	if err = repo.Link(ctx,"P1","R1","I1"); err != nil { t.Fatal(err) }
	if err = repo.Link(ctx,"P1","R1","I1"); err != nil { t.Fatal(err) }
	rows, err := repo.Propagate(ctx,"R1","RESOURCE_PRICE_CHANGED")
	if err != nil { t.Fatal(err) }
	if len(rows) != 1 { t.Fatalf("lineage count %d",len(rows)) }
	item, err := budget.Get(ctx,"I1")
	if err != nil { t.Fatal(err) }
	if item.UnitPrice != "12.3456" || item.Amount != "37.04" { t.Fatalf("price=%s amount=%s",item.UnitPrice,item.Amount) }

	listed, err := repo.ListProject(ctx,"P1")
	if err != nil { t.Fatal(err) }
	if len(listed) != 1 || listed[0].Trigger != "RESOURCE_PRICE_CHANGED" { t.Fatalf("unexpected lineage %#v",listed) }
}

func TestResourceBudgetLineageRequiresExplicitExistingEndpoints(t *testing.T) {
	ctx:=context.Background()
	store,err:=Open(ctx,filepath.Join(t.TempDir(),"missing.db"));if err!=nil{t.Fatal(err)};defer store.Close()
	repo:=NewResourceBudgetLineageRepository(store)
	if err=repo.Link(ctx,"P1","missing","missing");err==nil{t.Fatal("expected missing endpoint error")}
	if _,err=repo.Propagate(ctx,"missing","");err==nil{t.Fatal("expected missing resource error")}
}
