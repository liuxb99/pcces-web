package sqlite

import (
	"context"
	"path/filepath"
	"testing"
)

func TestBudgetDecimalRepositoryRecalculatesTree(t *testing.T) {
	ctx := context.Background()
	store, err := Open(ctx, filepath.Join(t.TempDir(), "budget.db"))
	if err != nil { t.Fatal(err) }
	defer store.Close()
	repo := NewBudgetDecimalRepository(store)
	parent := "P"
	if _, err = repo.Save(ctx, BudgetDecimalItem{ID:parent,ProjectCode:"PRJ",Name:"總項",Kind:"B",Quantity:"0",UnitPrice:"0",Amount:"0",QuantityScale:4,PriceScale:4,AmountScale:2}); err != nil { t.Fatal(err) }
	for _, item := range []BudgetDecimalItem{
		{ID:"C1",ProjectCode:"PRJ",ParentID:&parent,Name:"材料",Kind:"L",Quantity:"2.5000",UnitPrice:"100.0050",Amount:"0",QuantityScale:4,PriceScale:4,AmountScale:2},
		{ID:"C2",ProjectCode:"PRJ",ParentID:&parent,Name:"人工",Kind:"L",Quantity:"1.0000",UnitPrice:"50.0050",Amount:"0",QuantityScale:4,PriceScale:4,AmountScale:2},
	} { if _, err = repo.Save(ctx,item); err != nil { t.Fatal(err) } }
	total, err := repo.RecalculateProject(ctx,"PRJ")
	if err != nil { t.Fatal(err) }
	if total != "300.02" { t.Fatalf("got %s", total) }
	root, err := repo.Get(ctx,parent)
	if err != nil { t.Fatal(err) }
	if root.Amount != "300.02" { t.Fatalf("root amount %s", root.Amount) }
}

func TestResourceDecimalRepositoryRollsUpBreakdownsAndConflicts(t *testing.T) {
	ctx:=context.Background()
	store,err:=Open(ctx,filepath.Join(t.TempDir(),"resource.db"));if err!=nil{t.Fatal(err)};defer store.Close()
	repo:=NewResourceDecimalRepository(store)
	resource,err:=repo.SaveResource(ctx,ResourceDecimal{ID:"R1",Code:"R001",Name:"混凝土",UnitPrice:"0",PriceScale:2});if err!=nil{t.Fatal(err)}
	if _,err=repo.SaveBreakdown(ctx,ResourceBreakdownDecimal{ID:"D1",ResourceID:"R1",Code:"MAT",Name:"材料",Quantity:"2.5000",UnitPrice:"100.0050",QuantityScale:4,PriceScale:4,AmountScale:2});err!=nil{t.Fatal(err)}
	if _,err=repo.SaveBreakdown(ctx,ResourceBreakdownDecimal{ID:"D2",ResourceID:"R1",Code:"LAB",Name:"人工",Quantity:"1.0000",UnitPrice:"50.0050",QuantityScale:4,PriceScale:4,AmountScale:2});err!=nil{t.Fatal(err)}
	current,err:=repo.GetResource(ctx,"R1");if err!=nil{t.Fatal(err)}
	if current.UnitPrice!="300.02"{t.Fatalf("price %s",current.UnitPrice)}
	_,err=repo.SaveResource(ctx,ResourceDecimal{ID:"R1",Code:"R001",Name:"混凝土",UnitPrice:"999",PriceScale:2,RowVersion:resource.RowVersion})
	if err==nil{t.Fatal("expected stale conflict")}
}
