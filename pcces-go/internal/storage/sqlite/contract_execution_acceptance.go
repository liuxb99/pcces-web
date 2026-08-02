package sqlite

import (
	"context"
	"database/sql"
	"encoding/json"
	"strings"
	"time"

	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

func(r *ExecutionRepository)TransitionSettlement(ctx context.Context,id,target string,rowVersion int64,actor string)(map[string]any,error){
	target=strings.ToUpper(target);var current,contract string;var rv int64
	if e:=r.store.db.QueryRowContext(ctx,`SELECT contract_id,status,row_version FROM settlements_v2 WHERE id=?`,id).Scan(&contract,&current,&rv);e==sql.ErrNoRows{return nil,errx.New(errx.CodeNotFound,"settlement not found","P6-G")}else if e!=nil{return nil,e}
	if rv!=rowVersion{return nil,errx.New(errx.CodeConflict,"row version conflict","P6-G")}
	allowed:=map[string]map[string]bool{"DRAFT":{"SUBMITTED":true},"SUBMITTED":{"DRAFT":true,"APPROVED":true},"APPROVED":{"COMPLETED":true}}
	if !allowed[current][target]{return nil,errx.New(errx.CodeInvalidArgument,"invalid settlement transition","P6-G")}
	now:=time.Now().UTC().Format(time.RFC3339Nano);tx,e:=r.store.db.BeginTx(ctx,nil);if e!=nil{return nil,e};defer func(){_ = tx.Rollback()}()
	res,e:=tx.ExecContext(ctx,`UPDATE settlements_v2 SET status=?,approved_by=CASE WHEN ?='APPROVED' THEN ? ELSE approved_by END,approved_at=CASE WHEN ?='APPROVED' THEN ? ELSE approved_at END,row_version=row_version+1 WHERE id=? AND row_version=?`,target,target,actor,target,now,id,rowVersion);if e!=nil{return nil,e};n,_:=res.RowsAffected();if n!=1{return nil,errx.New(errx.CodeConflict,"row version conflict","P6-G")}
	if target=="COMPLETED"{if _,e=tx.ExecContext(ctx,`UPDATE contracts_v2 SET status='SETTLED',updated_at=?,row_version=row_version+1 WHERE id=?`,now,contract);e!=nil{return nil,e}}
	if e=tx.Commit();e!=nil{return nil,e};return r.GetSettlement(ctx,id)
}

func(r *ExecutionRepository)CreateAcceptance(ctx context.Context,id,contractID,inspectionDate,result,actor string,defects,improvements []map[string]any)(map[string]any,error){
	var settlementID string;if e:=r.store.db.QueryRowContext(ctx,`SELECT id FROM settlements_v2 WHERE contract_id=? AND status='COMPLETED'`,contractID).Scan(&settlementID);e==sql.ErrNoRows{return nil,errx.New(errx.CodeConflict,"completed settlement is required","P6-G")}else if e!=nil{return nil,e}
	d,_:=json.Marshal(defects);i,_:=json.Marshal(improvements);now:=time.Now().UTC().Format(time.RFC3339Nano)
	if _,e:=r.store.db.ExecContext(ctx,`INSERT INTO acceptances_v2(id,contract_id,settlement_id,status,inspection_date,result,defects_json,improvements_json,created_by,created_at,row_version) VALUES(?,?,?,'DRAFT',?,?,?,?,?,?,1)`,id,contractID,settlementID,inspectionDate,result,string(d),string(i),actor,now);e!=nil{return nil,e}
	return r.GetAcceptance(ctx,id)
}

func(r *ExecutionRepository)TransitionAcceptance(ctx context.Context,id,target string,rowVersion int64,actor string)(map[string]any,error){
	target=strings.ToUpper(target);var current,contract string;var rv int64
	if e:=r.store.db.QueryRowContext(ctx,`SELECT contract_id,status,row_version FROM acceptances_v2 WHERE id=?`,id).Scan(&contract,&current,&rv);e==sql.ErrNoRows{return nil,errx.New(errx.CodeNotFound,"acceptance not found","P6-G")}else if e!=nil{return nil,e}
	if rv!=rowVersion{return nil,errx.New(errx.CodeConflict,"row version conflict","P6-G")}
	allowed:=map[string]map[string]bool{"DRAFT":{"INSPECTED":true},"INSPECTED":{"IMPROVEMENT_REQUIRED":true,"COMPLETED":true},"IMPROVEMENT_REQUIRED":{"INSPECTED":true},"COMPLETED":{"ARCHIVED":true}}
	if !allowed[current][target]{return nil,errx.New(errx.CodeInvalidArgument,"invalid acceptance transition","P6-G")}
	now:=time.Now().UTC().Format(time.RFC3339Nano);tx,e:=r.store.db.BeginTx(ctx,nil);if e!=nil{return nil,e};defer func(){_ = tx.Rollback()}()
	res,e:=tx.ExecContext(ctx,`UPDATE acceptances_v2 SET status=?,completed_by=CASE WHEN ? IN ('COMPLETED','ARCHIVED') THEN ? ELSE completed_by END,completed_at=CASE WHEN ? IN ('COMPLETED','ARCHIVED') THEN ? ELSE completed_at END,row_version=row_version+1 WHERE id=? AND row_version=?`,target,target,actor,target,now,id,rowVersion);if e!=nil{return nil,e};n,_:=res.RowsAffected();if n!=1{return nil,errx.New(errx.CodeConflict,"row version conflict","P6-G")}
	if target=="ARCHIVED"{if _,e=tx.ExecContext(ctx,`UPDATE contracts_v2 SET status='ARCHIVED',updated_at=?,row_version=row_version+1 WHERE id=?`,now,contract);e!=nil{return nil,e}}
	if e=tx.Commit();e!=nil{return nil,e};return r.GetAcceptance(ctx,id)
}

func(r *ExecutionRepository)GetAcceptance(ctx context.Context,id string)(map[string]any,error){
	var contract,settlement,status,date,result,defects,improvements string;var rv int64
	if e:=r.store.db.QueryRowContext(ctx,`SELECT contract_id,settlement_id,status,COALESCE(inspection_date,''),COALESCE(result,''),defects_json,improvements_json,row_version FROM acceptances_v2 WHERE id=?`,id).Scan(&contract,&settlement,&status,&date,&result,&defects,&improvements,&rv);e==sql.ErrNoRows{return nil,errx.New(errx.CodeNotFound,"acceptance not found","P6-G")}else if e!=nil{return nil,e}
	var d,i []map[string]any;_ = json.Unmarshal([]byte(defects),&d);_ = json.Unmarshal([]byte(improvements),&i)
	return map[string]any{"id":id,"contract_id":contract,"settlement_id":settlement,"status":status,"inspection_date":date,"result":result,"defects":d,"improvements":i,"row_version":rv,"deep_link":"/app/contracts/"+contract+"/acceptance"},nil
}
