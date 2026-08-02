package sqlite

import (
	"context"
	"testing"
)

func TestMRSIntelligenceQuotesSnapshotsAndImpact(t *testing.T) {
	store := newTestStore(t)
	ctx := context.Background()
	catalog := NewMRSCatalogRepository(store)
	material := MRSCatalogItem{ID:"M1",Code:"M-001",Name:"水泥",Category:"MATERIAL",CurrentPrice:"180.00",PriceScale:2,Enabled:true}
	labor := MRSCatalogItem{ID:"L1",Code:"L-001",Name:"技工",Category:"LABOR",CurrentPrice:"2500.00",PriceScale:2,Enabled:true}
	if _,err:=catalog.SaveItem(ctx,material,"7","");err!=nil{t.Fatal(err)}
	if _,err:=catalog.SaveItem(ctx,labor,"7","");err!=nil{t.Fatal(err)}
	components:=[]MRSAnalysisComponent{{ID:"C1",CatalogItemID:"M1",Quantity:"2.50",QuantityScale:2},{ID:"C2",CatalogItemID:"L1",Quantity:"0.10",QuantityScale:2}}
	if _,err:=catalog.SaveRecipe(ctx,"R1","A-001","混凝土分析",nil,2,components,0);err!=nil{t.Fatal(err)}

	repo:=NewMRSIntelligenceRepository(store)
	if _,err:=repo.AddQuote(ctx,MRSQuote{ID:"Q1",CatalogItemID:"M1",Vendor:"甲商",QuotedPrice:"175.125",PriceScale:2,CreatedBy:"7"});err!=nil{t.Fatal(err)}
	if _,err:=repo.AddQuote(ctx,MRSQuote{ID:"Q2",CatalogItemID:"M1",Vendor:"乙商",QuotedPrice:"190.00",PriceScale:2,CreatedBy:"7"});err!=nil{t.Fatal(err)}
	comparison,err:=repo.CompareQuotes(ctx,"M1");if err!=nil{t.Fatal(err)}
	if comparison["lowest_quote"]!="175.13"||comparison["highest_quote"]!="190.00"||comparison["spread"]!="14.87"{t.Fatalf("comparison=%+v",comparison)}

	snapshot,err:=repo.SnapshotRecipe(ctx,"S1","R1","7");if err!=nil{t.Fatal(err)}
	if snapshot.UnitPrice!="700.00"||snapshot.DeepLink==""{t.Fatalf("snapshot=%+v",snapshot)}
	count,err:=repo.SnapshotCount(ctx,"R1");if err!=nil||count!=1{t.Fatalf("count=%d err=%v",count,err)}

	impact,err:=repo.Impact(ctx,"I1","M1","180.00","200.00","7");if err!=nil{t.Fatal(err)}
	if impact.AffectedCount!=1||impact.TotalComponentDelta!="50.00"{t.Fatalf("impact=%+v",impact)}
	if impact.AffectedRecipes[0]["old_amount"]!="450.00"||impact.AffectedRecipes[0]["new_amount"]!="500.00"{t.Fatalf("recipes=%+v",impact.AffectedRecipes)}
}
