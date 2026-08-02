package sqlite

import (
	"context"
	"testing"
)

func TestMRSGovernanceReleaseValidityAndFreeze(t *testing.T){
	store:=newTestStore(t);ctx:=context.Background();catalog:=NewMRSCatalogRepository(store)
	if _,err:=catalog.SaveItem(ctx,MRSCatalogItem{ID:"M1",Code:"M-1",Name:"Material",Category:"MATERIAL",CurrentPrice:"100",PriceScale:2,Enabled:true},"7","");err!=nil{t.Fatal(err)}
	if _,err:=catalog.SaveRecipe(ctx,"R1","R-1","Recipe",nil,2,[]MRSAnalysisComponent{{ID:"C1",CatalogItemID:"M1",Quantity:"2",QuantityScale:2}},0);err!=nil{t.Fatal(err)}
	ops:=NewMRSOperationsRepository(store);version,err:=ops.CreateRecipeVersion(ctx,"V1","R1","baseline","7");if err!=nil{t.Fatal(err)}
	repo:=NewMRSGovernanceRepository(store);release,err:=repo.CreateRelease(ctx,"REL1","2026-08","7");if err!=nil{t.Fatal(err)};if release.Status!="DRAFT"||len(release.Snapshot)!=1{t.Fatalf("release=%+v",release)}
	release,err=repo.TransitionRelease(ctx,"REL1","SUBMIT","7","",release.RowVersion);if err!=nil{t.Fatal(err)}
	release,err=repo.TransitionRelease(ctx,"REL1","APPROVE","8","ok",release.RowVersion);if err!=nil{t.Fatal(err)}
	release,err=repo.TransitionRelease(ctx,"REL1","PUBLISH","8","",release.RowVersion);if err!=nil{t.Fatal(err)};if release.Status!="PUBLISHED"{t.Fatalf("release=%+v",release)}
	from,to:="2026-01-01","2026-06-30";validity,err:=repo.SetValidity(ctx,"M1",&from,&to,"ACTIVE","7",0);if err!=nil{t.Fatal(err)};if validity.RowVersion!=1{t.Fatalf("validity=%+v",validity)}
	alerts,err:=repo.ExpiryAlerts(ctx,"2026-08-02");if err!=nil||len(alerts)!=1||alerts[0]["status"]!="EXPIRED"{t.Fatalf("alerts=%+v err=%v",alerts,err)}
	reason:="approved basis";freeze,err:=repo.SetRecipeFreeze(ctx,"R1",version.ID,true,&reason,"8",0);if err!=nil{t.Fatal(err)};if !freeze.Frozen||freeze.VersionID!="V1"{t.Fatalf("freeze=%+v",freeze)}
	if _,err=repo.TransitionRelease(ctx,"REL1","SUBMIT","7","",release.RowVersion);err==nil{t.Fatal("published release must be terminal")}
}
