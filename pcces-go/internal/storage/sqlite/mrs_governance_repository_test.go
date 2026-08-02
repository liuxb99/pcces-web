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
	if _,err=catalog.SaveItem(ctx,MRSCatalogItem{ID:"M1",Code:"M-1",Name:"Material updated",Category:"MATERIAL",CurrentPrice:"120",PriceScale:2,Enabled:true},"7","1");err!=nil{t.Fatal(err)}
	stored,err:=repo.GetRelease(ctx,"REL1");if err!=nil{t.Fatal(err)};if stored.Snapshot[0].Name!="Material"||stored.Snapshot[0].CurrentPrice!="100"{t.Fatalf("release snapshot mutated: %+v",stored.Snapshot[0])}
	releases,err:=repo.ListReleases(ctx);if err!=nil||len(releases)!=1||releases[0].DeepLink!="/app/mrs-governance?release=REL1"{t.Fatalf("releases=%+v err=%v",releases,err)}
	release,err=repo.TransitionRelease(ctx,"REL1","submit","7","",release.RowVersion);if err!=nil{t.Fatal(err)}
	release,err=repo.TransitionRelease(ctx,"REL1","APPROVE","8","ok",release.RowVersion);if err!=nil{t.Fatal(err)}
	release,err=repo.TransitionRelease(ctx,"REL1","PUBLISH","8","",release.RowVersion);if err!=nil{t.Fatal(err)};if release.Status!="PUBLISHED"{t.Fatalf("release=%+v",release)}
	from,to:="2026-01-01","2026-06-30";validity,err:=repo.SetValidity(ctx,"M1",&from,&to,"active","7",0);if err!=nil{t.Fatal(err)};if validity.RowVersion!=1||validity.Status!="ACTIVE"{t.Fatalf("validity=%+v",validity)}
	alerts,err:=repo.ExpiryAlerts(ctx,"2026-08-02");if err!=nil||len(alerts)!=1||alerts[0]["status"]!="EXPIRED"{t.Fatalf("alerts=%+v err=%v",alerts,err)}
	reason:="approved basis";freeze,err:=repo.SetRecipeFreeze(ctx,"R1",version.ID,true,&reason,"8",0);if err!=nil{t.Fatal(err)};if !freeze.Frozen||freeze.VersionID!="V1"{t.Fatalf("freeze=%+v",freeze)}
	audit,err:=repo.ListAudit(ctx);if err!=nil{t.Fatal(err)};if len(audit)!=7{t.Fatalf("audit=%+v",audit)};if audit[0].EventType!="RECIPE_FREEZE_SET"||audit[0].Payload["version_id"]!="V1"{t.Fatalf("latest audit=%+v",audit[0])}
	if _,err=repo.TransitionRelease(ctx,"REL1","SUBMIT","7","",release.RowVersion);err==nil{t.Fatal("published release must be terminal")}
}

func TestMRSGovernanceRejectsInvalidAndStaleMutations(t *testing.T){
	store:=newTestStore(t);ctx:=context.Background();catalog:=NewMRSCatalogRepository(store)
	if _,err:=catalog.SaveItem(ctx,MRSCatalogItem{ID:"M1",Code:"M-1",Name:"Material",Category:"MATERIAL",CurrentPrice:"100",PriceScale:2,Enabled:true},"7","");err!=nil{t.Fatal(err)}
	if _,err:=catalog.SaveRecipe(ctx,"R1","R-1","Recipe",nil,2,[]MRSAnalysisComponent{{ID:"C1",CatalogItemID:"M1",Quantity:"1",QuantityScale:2}},0);err!=nil{t.Fatal(err)}
	version,err:=NewMRSOperationsRepository(store).CreateRecipeVersion(ctx,"V1","R1","baseline","7");if err!=nil{t.Fatal(err)}
	repo:=NewMRSGovernanceRepository(store)
	from,to:="2026-12-31","2026-01-01";if _,err=repo.SetValidity(ctx,"M1",&from,&to,"ACTIVE","7",0);err==nil{t.Fatal("invalid date range must fail")}
	if _,err=repo.SetValidity(ctx,"M1",nil,nil,"UNKNOWN","7",0);err==nil{t.Fatal("invalid status must fail")}
	validity,err:=repo.SetValidity(ctx,"M1",nil,nil,"ACTIVE","7",0);if err!=nil{t.Fatal(err)}
	if _,err=repo.SetValidity(ctx,"M1",nil,nil,"SUSPENDED","7",validity.RowVersion-1);err==nil{t.Fatal("stale validity update must fail")}
	freeze,err:=repo.SetRecipeFreeze(ctx,"R1",version.ID,true,nil,"8",0);if err!=nil{t.Fatal(err)}
	if _,err=repo.SetRecipeFreeze(ctx,"R1",version.ID,false,nil,"8",freeze.RowVersion-1);err==nil{t.Fatal("stale freeze update must fail")}
	audit,err:=repo.ListAudit(ctx);if err!=nil{t.Fatal(err)};if len(audit)!=2{t.Fatalf("failed mutations must not create audit rows: %+v",audit)}
}
