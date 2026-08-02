package sqlite

import (
	"context"
	"testing"
)

func TestMRSOperationsUsageVersionsLineageAndJobs(t *testing.T){
	store:=newTestStore(t);ctx:=context.Background();catalog:=NewMRSCatalogRepository(store)
	for _,item:=range []MRSCatalogItem{{ID:"M1",Code:"M-1",Name:"Material",Category:"MATERIAL",CurrentPrice:"100",PriceScale:2,Enabled:true},{ID:"L1",Code:"L-1",Name:"Labor",Category:"LABOR",CurrentPrice:"200",PriceScale:2,Enabled:true}}{if _,err:=catalog.SaveItem(ctx,item,"7","");err!=nil{t.Fatal(err)}}
	components:=[]MRSAnalysisComponent{{ID:"C1",CatalogItemID:"M1",Quantity:"2",QuantityScale:2},{ID:"C2",CatalogItemID:"L1",Quantity:"0.5",QuantityScale:2}}
	if _,err:=catalog.SaveRecipe(ctx,"R1","R-1","Recipe",nil,2,components,0);err!=nil{t.Fatal(err)}
	repo:=NewMRSOperationsRepository(store);usage,err:=repo.UsageSummary(ctx);if err!=nil{t.Fatal(err)};if usage.CatalogItems!=2||usage.EstimatedAmount!="300.00"{t.Fatalf("usage=%+v",usage)}
	v1,err:=repo.CreateRecipeVersion(ctx,"V1","R1","baseline","7");if err!=nil{t.Fatal(err)};if v1.UnitPrice!="300.00"{t.Fatalf("v1=%+v",v1)}
	item,err:=catalog.GetItem(ctx,"M1");if err!=nil{t.Fatal(err)};item.CurrentPrice="120";if _,err=catalog.SaveItem(ctx,item,"7","");err!=nil{t.Fatal(err)}
	v2,err:=repo.CreateRecipeVersion(ctx,"V2","R1","current","7");if err!=nil{t.Fatal(err)};if v2.UnitPrice!="340.00"{t.Fatalf("v2=%+v",v2)}
	versions,err:=repo.ListRecipeVersions(ctx,"R1");if err!=nil||len(versions)!=2{t.Fatalf("versions=%+v err=%v",versions,err)}
	intel:=NewMRSIntelligenceRepository(store);if _,err=intel.AddQuote(ctx,MRSQuote{ID:"Q1",CatalogItemID:"M1",Vendor:"Vendor",QuotedPrice:"115",PriceScale:2,CreatedBy:"7"});err!=nil{t.Fatal(err)}
	lineage,err:=repo.PriceLineage(ctx,"M1");if err!=nil{t.Fatal(err)};events:=lineage["events"].([]map[string]any);if len(events)<3{t.Fatalf("events=%+v",events)}
	job,err:=repo.CreateImportJob(ctx,"J1","JSON",`[{"id":"I1","code":"I-1","name":"Sand","category":"MATERIAL","current_price":"50","price_scale":2}]`,"7",false,1);if err!=nil{t.Fatal(err)};if job.Status!="PENDING"{t.Fatalf("job=%+v",job)}
	done,err:=repo.RunImportJob(ctx,"J1");if err!=nil{t.Fatal(err)};if done.Status!="COMPLETED"||done.ImportedRows!=1{t.Fatalf("done=%+v",done)}
	if _,err=repo.CreateImportJob(ctx,"J2","JSON","[]","7",false,0);err!=nil{t.Fatal(err)};cancelled,err:=repo.CancelImportJob(ctx,"J2");if err!=nil||cancelled.Status!="CANCELLED"{t.Fatalf("cancelled=%+v err=%v",cancelled,err)}
}
