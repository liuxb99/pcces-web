package sqlite

import (
	"context"
	"encoding/json"
	"path/filepath"
	"testing"
)

func TestResourceProjectReferenceCopiesImmutableSnapshot(t *testing.T){
	ctx:=context.Background();store,err:=Open(ctx,filepath.Join(t.TempDir(),"refs.db"));if err!=nil{t.Fatal(err)};defer store.Close()
	resources:=NewResourceDecimalRepository(store)
	if _,err=resources.SaveResource(ctx,ResourceDecimal{ID:"SRC",Code:"M01234567890",Name:"鋼筋",UnitPrice:"31.2500",PriceScale:4});err!=nil{t.Fatal(err)}
	repo:=NewResourceProjectReferenceRepository(store)
	ref,err:=repo.Import(ctx,"CHILD","PARENT","SRC","COPY","parent","u1");if err!=nil{t.Fatal(err)}
	if ref.ReferenceType!="PARENT"||ref.SourceProjectCode!="PARENT"{t.Fatalf("unexpected ref %#v",ref)}
	copy,err:=resources.GetResource(ctx,"COPY");if err!=nil{t.Fatal(err)}
	if copy.UnitPrice!="31.2500"{t.Fatalf("copied price %s",copy.UnitPrice)}
	var snapshot map[string]any;if err=json.Unmarshal([]byte(ref.SnapshotJSON),&snapshot);err!=nil{t.Fatal(err)}
	if snapshot["id"]!="SRC"{t.Fatalf("snapshot %#v",snapshot)}
	listed,err:=repo.ListTarget(ctx,"CHILD");if err!=nil||len(listed)!=1{t.Fatalf("listed=%#v err=%v",listed,err)}
}

func TestResourceProjectReferenceRejectsInvalidAndDuplicateTargets(t *testing.T){
	ctx:=context.Background();store,err:=Open(ctx,filepath.Join(t.TempDir(),"refs.db"));if err!=nil{t.Fatal(err)};defer store.Close()
	resources:=NewResourceDecimalRepository(store);_,_ = resources.SaveResource(ctx,ResourceDecimal{ID:"SRC",Code:"E012345678901",Name:"機具",UnitPrice:"10",PriceScale:4})
	repo:=NewResourceProjectReferenceRepository(store)
	if _,err=repo.Import(ctx,"C","P","SRC","X","LIVE","u");err==nil{t.Fatal("expected invalid type")}
	if _,err=repo.Import(ctx,"C","P","missing","X","PARENT","u");err==nil{t.Fatal("expected missing source")}
	if _,err=repo.Import(ctx,"C","P","SRC","X","HISTORICAL","u");err!=nil{t.Fatal(err)}
	if _,err=repo.Import(ctx,"C","P","SRC","X","HISTORICAL","u");err==nil{t.Fatal("expected duplicate target conflict")}
}
