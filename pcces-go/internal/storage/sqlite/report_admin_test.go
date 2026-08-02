package sqlite

import (
	"context"
	"testing"
)

func TestReportJobAndTypedSetting(t *testing.T){
	store:=newTestStore(t);ctx:=context.Background();repo:=NewReportAdminRepository(store)
	job,err:=repo.CreateReportJob(ctx,"J1","CONTRACT","P1","CV1","PDF","u",map[string]any{"title":"Contract","rows":[]any{}},map[string]any{})
	if err!=nil{t.Fatal(err)}
	if job["status"]!="QUEUED"{t.Fatalf("unexpected job %#v",job)}
	done,err:=repo.RenderReport(ctx,"J1","A1",1);if err!=nil{t.Fatal(err)}
	if done["status"]!="COMPLETED"{t.Fatalf("unexpected done %#v",done)}
	content,ctype,_,err:=repo.ReportArtifact(ctx,"A1","u");if err!=nil{t.Fatal(err)}
	if len(content)==0||ctype!="application/pdf"{t.Fatalf("bad artifact %s %d",ctype,len(content))}
	setting,err:=repo.SetSetting(ctx,"autosave.interval_seconds",float64(60),0,"u");if err!=nil{t.Fatal(err)}
	if setting["row_version"].(int64)!=1{t.Fatalf("bad setting %#v",setting)}
}
