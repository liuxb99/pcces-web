package sqlite

import (
 "context"
 "testing"
)

func TestConversionLongJobProgressAndCancel(t *testing.T){
 store:=newTestStore(t);ctx:=context.Background();repo:=NewConversionLongJobRepository(store)
 job,err:=repo.Create(ctx,"J1","EXPORT","u",map[string]any{"x":1});if err!=nil{t.Fatal(err)}
 job,err=repo.Advance(ctx,job.ID,job.RowVersion,40,"SERIALIZE","",nil,nil);if err!=nil{t.Fatal(err)}
 if job.Status!="RUNNING"||job.Progress!=40{t.Fatalf("unexpected job: %#v",job)}
 job,err=repo.Cancel(ctx,job.ID,job.RowVersion);if err!=nil{t.Fatal(err)}
 if job.Status!="CANCELLED"||!job.CancelRequested||job.Result!=nil{t.Fatalf("unexpected cancellation: %#v",job)}
}
func TestConversionLongJobRejectsRegression(t *testing.T){
 store:=newTestStore(t);ctx:=context.Background();repo:=NewConversionLongJobRepository(store)
 job,err:=repo.Create(ctx,"J2","IMPORT","u",nil);if err!=nil{t.Fatal(err)}
 job,err=repo.Advance(ctx,job.ID,job.RowVersion,50,"VALIDATE","",nil,nil);if err!=nil{t.Fatal(err)}
 if _,err=repo.Advance(ctx,job.ID,job.RowVersion,20,"WRITE","",nil,nil);err==nil{t.Fatal("expected regression error")}
}
