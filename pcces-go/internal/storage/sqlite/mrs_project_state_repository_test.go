package sqlite

import (
    "context"
    "path/filepath"
    "testing"
)

func TestMRSProjectStateLifecycleAndReadonly(t *testing.T){
    ctx:=context.Background();store,err:=Open(ctx,filepath.Join(t.TempDir(),"state.db"));if err!=nil{t.Fatal(err)};defer store.Close()
    repo:=NewMRSProjectStateRepository(store)
    initial,err:=repo.Get(ctx,"P1");if err!=nil{t.Fatal(err)}
    if initial.State!="DRAFT"||initial.EffectiveReadonly{t.Fatalf("unexpected default %#v",initial)}
    submitted,err:=repo.Save(ctx,"P1","SUBMITTED",false,false,"submit","u1",0);if err!=nil{t.Fatal(err)}
    approved,err:=repo.Save(ctx,"P1","APPROVED",false,false,"approved","u1",submitted.RowVersion);if err!=nil{t.Fatal(err)}
    if !approved.EffectiveReadonly{t.Fatal("approved project must be readonly")}
    if err=repo.AssertWritable(ctx,"P1");err==nil{t.Fatal("expected readonly guard")}
}

func TestMRSProjectStateRejectsInvalidTransitionsAndStaleVersions(t *testing.T){
    ctx:=context.Background();store,err:=Open(ctx,filepath.Join(t.TempDir(),"state2.db"));if err!=nil{t.Fatal(err)};defer store.Close()
    repo:=NewMRSProjectStateRepository(store)
    if _,err=repo.Save(ctx,"P1","APPROVED",false,false,"","u1",0);err==nil{t.Fatal("expected invalid transition")}
    first,err:=repo.Save(ctx,"P1","SUBMITTED",false,false,"","u1",0);if err!=nil{t.Fatal(err)}
    if _,err=repo.Save(ctx,"P1","DRAFT",false,false,"","u1",0);err==nil{t.Fatal("expected stale version")}
    if _,err=repo.Save(ctx,"P1","ARCHIVED",false,false,"","u1",first.RowVersion);err==nil{t.Fatal("expected invalid transition")}
}

func TestMRSProjectStateTemplateAndManualReadonly(t *testing.T){
    ctx:=context.Background();store,err:=Open(ctx,filepath.Join(t.TempDir(),"state3.db"));if err!=nil{t.Fatal(err)};defer store.Close()
    repo:=NewMRSProjectStateRepository(store)
    template,err:=repo.Save(ctx,"T1","DRAFT",true,false,"template","u1",0);if err!=nil{t.Fatal(err)}
    if !template.EffectiveReadonly{t.Fatal("template must be readonly")}
    manual,err:=repo.Save(ctx,"R1","DRAFT",false,true,"locked","u1",0);if err!=nil{t.Fatal(err)}
    if !manual.EffectiveReadonly{t.Fatal("manual readonly must be effective")}
}
