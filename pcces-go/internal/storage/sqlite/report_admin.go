package sqlite

import (
	"context"
	"crypto/sha256"
	"database/sql"
	"encoding/hex"
	"encoding/json"
	"fmt"
	"strings"
	"time"

	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

type ReportAdminRepository struct{ store *Store }
func NewReportAdminRepository(store *Store)*ReportAdminRepository{return &ReportAdminRepository{store:store}}

func(r *ReportAdminRepository)EnsureDefaults(ctx context.Context)error{
	defs:=[][5]string{{"BUDGET_SUMMARY","預算總表","BUDGET","1.0","FormReportViewer.cs"},{"CONTRACT","契約明細表","CONTRACT","1.0","ucSubCtr.cs"},{"INVOICE","估驗計價表","INVOICE","1.0","FormInvoiceReport.cs"},{"SETTLEMENT","結算表","SETTLEMENT","1.0","ucSubClose.cs"},{"ACCEPTANCE","驗收表","ACCEPTANCE","1.0","ucSubFinal.cs"}}
	for _,d:=range defs{if _,e:=r.store.db.ExecContext(ctx,`INSERT OR IGNORE INTO report_definitions(code,name,business_type,template_version,legacy_entry,schema_json,enabled) VALUES(?,?,?,?,?,'{"required":["title","rows"]}',1)`,d[0],d[1],d[2],d[3],d[4]);e!=nil{return e}}
	settings:=[][6]string{{"autosave.interval_seconds","general","integer","30","{\"min\":5,\"max\":3600}","自動儲存間隔"},{"reports.retention_days","report","integer","365","{\"min\":1,\"max\":3650}","報表保留天數"}}
	for _,d:=range settings{if _,e:=r.store.db.ExecContext(ctx,`INSERT OR IGNORE INTO setting_definitions(key,category,value_type,default_json,constraints_json,version,description) VALUES(?,?,?,?,?,1,?)`,d[0],d[1],d[2],d[3],d[4],d[5]);e!=nil{return e}}
	return nil
}

func(r *ReportAdminRepository)CreateReportJob(ctx context.Context,id,definition,project,version,format,actor string,snapshot,params map[string]any)(map[string]any,error){
	if id==""||definition==""||project==""||version==""||actor==""{return nil,errx.New(errx.CodeInvalidArgument,"required report fields are missing","P7-G")}
	format=strings.ToUpper(format);if format!="PDF"&&format!="CSV"&&format!="JSON"&&format!="XLSX"{return nil,errx.New(errx.CodeInvalidArgument,"unsupported report format","P7-G")}
	if e:=r.EnsureDefaults(ctx);e!=nil{return nil,e};var exists int;if e:=r.store.db.QueryRowContext(ctx,`SELECT COUNT(*) FROM report_definitions WHERE code=? AND enabled=1`,definition).Scan(&exists);e!=nil{return nil,e};if exists==0{return nil,errx.New(errx.CodeNotFound,"report definition not found","P7-G")}
	snap,_:=json.Marshal(snapshot);par,_:=json.Marshal(params);now:=time.Now().UTC().Format(time.RFC3339Nano)
	_,e:=r.store.db.ExecContext(ctx,`INSERT INTO report_jobs(id,definition_code,project_code,business_version_id,format,status,progress,parameters_json,snapshot_json,created_by,created_at,updated_at,row_version) VALUES(?,?,?,?,?,'QUEUED',0,?,?,?,?,?,1)`,id,definition,project,version,format,string(par),string(snap),actor,now,now);if e!=nil{return nil,e};return r.GetReportJob(ctx,id)
}

func(r *ReportAdminRepository)RenderReport(ctx context,jobID,artifactID string,rowVersion int64)(map[string]any,error){
	var format,definition,version,snapshot string;var rv int64;var status string
	if e:=r.store.db.QueryRowContext(ctx,`SELECT format,definition_code,business_version_id,snapshot_json,row_version,status FROM report_jobs WHERE id=?`,jobID).Scan(&format,&definition,&version,&snapshot,&rv,&status);e==sql.ErrNoRows{return nil,errx.New(errx.CodeNotFound,"report job not found","P7-G")}else if e!=nil{return nil,e}
	if rv!=rowVersion{return nil,errx.New(errx.CodeConflict,"row version conflict","P7-G")};if status!="QUEUED"&&status!="FAILED"{return nil,errx.New(errx.CodeInvalidArgument,"job is not renderable","P7-G")}
	content,ext,ctype,e:=buildReportArtifact(format,snapshot);if e!=nil{return nil,e}
	h:=sha256.Sum256(content);digest:=hex.EncodeToString(h[:]);name:=fmt.Sprintf("%s-%s.%s",strings.ToLower(definition),version,ext);now:=time.Now().UTC().Format(time.RFC3339Nano)
	tx,e:=r.store.db.BeginTx(ctx,nil);if e!=nil{return nil,e};defer func(){_ = tx.Rollback()}();if _,e=tx.ExecContext(ctx,`INSERT INTO report_artifacts(id,job_id,filename,content_type,size_bytes,sha256,content,created_at) VALUES(?,?,?,?,?,?,?,?)`,artifactID,jobID,name,ctype,len(content),digest,content,now);e!=nil{return nil,e};res,e:=tx.ExecContext(ctx,`UPDATE report_jobs SET status='COMPLETED',progress=100,error_json=NULL,updated_at=?,row_version=row_version+1 WHERE id=? AND row_version=?`,now,jobID,rowVersion);if e!=nil{return nil,e};n,_:=res.RowsAffected();if n!=1{return nil,errx.New(errx.CodeConflict,"row version conflict","P7-G")};if e=tx.Commit();e!=nil{return nil,e};return r.GetReportJob(ctx,jobID)
}

func(r *ReportAdminRepository)GetReportJob(ctx context,id string)(map[string]any,error){var def,project,version,format,status string;var progress int;var rv int64;if e:=r.store.db.QueryRowContext(ctx,`SELECT definition_code,project_code,business_version_id,format,status,progress,row_version FROM report_jobs WHERE id=?`,id).Scan(&def,&project,&version,&format,&status,&progress,&rv);e==sql.ErrNoRows{return nil,errx.New(errx.CodeNotFound,"report job not found","P7-G")}else if e!=nil{return nil,e};result:=map[string]any{"id":id,"definition_code":def,"project_code":project,"business_version_id":version,"format":format,"status":status,"progress":progress,"row_version":rv};var aid,name,digest string;if e:=r.store.db.QueryRowContext(ctx,`SELECT id,filename,sha256 FROM report_artifacts WHERE job_id=? ORDER BY created_at DESC LIMIT 1`,id).Scan(&aid,&name,&digest);e==nil{result["artifact"]=map[string]any{"id":aid,"filename":name,"sha256":digest,"download_url":"/api/reports/artifacts/"+aid+"/download"}};return result,nil}

func(r *ReportAdminRepository)ReportArtifact(ctx context,id,actor string)([]byte,string,string,error){var content []byte;var ctype,name string;if e:=r.store.db.QueryRowContext(ctx,`SELECT content,content_type,filename FROM report_artifacts WHERE id=?`,id).Scan(&content,&ctype,&name);e==sql.ErrNoRows{return nil,"","",errx.New(errx.CodeNotFound,"report artifact not found","P7-G")}else if e!=nil{return nil,"","",e};_,e:=r.store.db.ExecContext(ctx,`INSERT INTO report_download_audit(id,artifact_id,actor,downloaded_at) VALUES(?,?,?,?)`,fmt.Sprintf("%s-%d",id,time.Now().UnixNano()),id,actor,time.Now().UTC().Format(time.RFC3339Nano));return content,ctype,name,e}

func(r *ReportAdminRepository)ListSettings(ctx context)([]map[string]any,error){if e:=r.EnsureDefaults(ctx);e!=nil{return nil,e};rows,e:=r.store.db.QueryContext(ctx,`SELECT d.key,d.category,d.value_type,d.default_json,d.constraints_json,COALESCE(v.value_json,d.default_json),COALESCE(v.row_version,0) FROM setting_definitions d LEFT JOIN setting_values v ON v.key=d.key ORDER BY d.key`);if e!=nil{return nil,e};defer rows.Close();out:=[]map[string]any{};for rows.Next(){var key,cat,typ,def,constraints,value string;var rv int64;if e=rows.Scan(&key,&cat,&typ,&def,&constraints,&value,&rv);e!=nil{return nil,e};var parsed any;_ = json.Unmarshal([]byte(value),&parsed);out=append(out,map[string]any{"key":key,"category":cat,"value_type":typ,"value":parsed,"row_version":rv})};return out,rows.Err()}

func(r *ReportAdminRepository)SetSetting(ctx,key string,value any,rowVersion int64,actor string)(map[string]any,error){if e:=r.EnsureDefaults(ctx);e!=nil{return nil,e};var typ string;if e:=r.store.db.QueryRowContext(ctx,`SELECT value_type FROM setting_definitions WHERE key=?`,key).Scan(&typ);e==sql.ErrNoRows{return nil,errx.New(errx.CodeNotFound,"setting definition not found","P8-G")}else if e!=nil{return nil,e};switch typ{case "integer":if _,ok:=value.(float64);!ok{return nil,errx.New(errx.CodeInvalidArgument,"value must be integer","P8-G")};case "boolean":if _,ok:=value.(bool);!ok{return nil,errx.New(errx.CodeInvalidArgument,"value must be boolean","P8-G")};case "string":if _,ok:=value.(string);!ok{return nil,errx.New(errx.CodeInvalidArgument,"value must be string","P8-G")}}
	encoded,_:=json.Marshal(value);now:=time.Now().UTC().Format(time.RFC3339Nano);var current int64;e:=r.store.db.QueryRowContext(ctx,`SELECT row_version FROM setting_values WHERE key=?`,key).Scan(&current);if e==sql.ErrNoRows{if rowVersion!=0{return nil,errx.New(errx.CodeConflict,"row version conflict","P8-G")};_,e=r.store.db.ExecContext(ctx,`INSERT INTO setting_values(key,value_json,updated_by,updated_at,row_version) VALUES(?,?,?,?,1)`,key,string(encoded),actor,now)}else if e==nil{if current!=rowVersion{return nil,errx.New(errx.CodeConflict,"row version conflict","P8-G")};_,e=r.store.db.ExecContext(ctx,`UPDATE setting_values SET value_json=?,updated_by=?,updated_at=?,row_version=row_version+1 WHERE key=? AND row_version=?`,string(encoded),actor,now,key,rowVersion)};if e!=nil{return nil,e};items,e:=r.ListSettings(ctx);if e!=nil{return nil,e};for _,item:=range items{if item["key"]==key{return item,nil}};return nil,errx.New(errx.CodeNotFound,"setting not found","P8-G")}
