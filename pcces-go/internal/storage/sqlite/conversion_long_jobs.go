package sqlite

import (
	"context"
	"database/sql"
	"encoding/json"
	"fmt"
	"strings"
	"time"

	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

type ConversionLongJob struct {
	ID string `json:"id"`
	JobType string `json:"job_type"`
	Status string `json:"status"`
	Progress int `json:"progress"`
	Stage string `json:"stage"`
	Payload map[string]any `json:"payload"`
	Result map[string]any `json:"result,omitempty"`
	Error map[string]any `json:"error,omitempty"`
	CancelRequested bool `json:"cancel_requested"`
	CreatedBy string `json:"created_by"`
	CreatedAt string `json:"created_at"`
	UpdatedAt string `json:"updated_at"`
	RowVersion int64 `json:"row_version"`
}

type ConversionLongJobRepository struct{ store *Store }
func NewConversionLongJobRepository(store *Store) *ConversionLongJobRepository { return &ConversionLongJobRepository{store:store} }

func (r *ConversionLongJobRepository) Create(ctx context.Context,id,jobType,actor string,payload map[string]any)(ConversionLongJob,error){
	jobType=strings.ToUpper(strings.TrimSpace(jobType));if jobType!="IMPORT"&&jobType!="EXPORT"{return ConversionLongJob{},errx.New(errx.CodeInvalidArgument,"job_type must be IMPORT or EXPORT","P4-G-JOB")}
	b,_:=json.Marshal(payload);now:=time.Now().UTC().Format(time.RFC3339Nano)
	_,err:=r.store.db.ExecContext(ctx,`INSERT INTO conversion_long_jobs(id,job_type,status,progress,stage,payload_json,cancel_requested,created_by,created_at,updated_at,row_version) VALUES(?,?,?,?,?,?,?,?,?,?,1)`,id,jobType,"QUEUED",0,"QUEUED",string(b),0,actor,now,now)
	if err!=nil{return ConversionLongJob{},err};return r.Get(ctx,id)
}
func (r *ConversionLongJobRepository) Get(ctx context.Context,id string)(ConversionLongJob,error){
	var v ConversionLongJob;var payload,result,errorJSON sql.NullString;var cancel int
	err:=r.store.db.QueryRowContext(ctx,`SELECT id,job_type,status,progress,stage,payload_json,result_json,error_json,cancel_requested,created_by,created_at,updated_at,row_version FROM conversion_long_jobs WHERE id=?`,id).Scan(&v.ID,&v.JobType,&v.Status,&v.Progress,&v.Stage,&payload,&result,&errorJSON,&cancel,&v.CreatedBy,&v.CreatedAt,&v.UpdatedAt,&v.RowVersion)
	if err==sql.ErrNoRows{return v,errx.New(errx.CodeNotFound,"conversion job not found","P4-G-JOB")};if err!=nil{return v,err};v.CancelRequested=cancel!=0;_ = json.Unmarshal([]byte(payload.String),&v.Payload);if result.Valid{_ = json.Unmarshal([]byte(result.String),&v.Result)};if errorJSON.Valid{_ = json.Unmarshal([]byte(errorJSON.String),&v.Error)};return v,nil
}
func (r *ConversionLongJobRepository) Advance(ctx context.Context,id string,rowVersion int64,progress int,stage,status string,result,errorValue map[string]any)(ConversionLongJob,error){
	current,err:=r.Get(ctx,id);if err!=nil{return current,err};if current.Status=="COMPLETED"||current.Status=="FAILED"||current.Status=="CANCELLED"{return current,errx.New(errx.CodeConflict,"terminal job cannot advance","P4-G-JOB")};if rowVersion!=current.RowVersion{return current,errx.New(errx.CodeConflict,"stale conversion job row_version","P4-G-JOB")};if progress<current.Progress||progress>100{return current,errx.New(errx.CodeInvalidArgument,"progress must be monotonic and between 0 and 100","P4-G-JOB")};status=strings.ToUpper(strings.TrimSpace(status));if status==""{if progress==100{status="COMPLETED"}else{status="RUNNING"}};if current.CancelRequested{status="CANCELLED";progress=current.Progress};if status!="RUNNING"&&status!="COMPLETED"&&status!="FAILED"&&status!="CANCELLED"{return current,errx.New(errx.CodeInvalidArgument,"invalid job status","P4-G-JOB")};rb,_:=json.Marshal(result);eb,_:=json.Marshal(errorValue);now:=time.Now().UTC().Format(time.RFC3339Nano);res,err:=r.store.db.ExecContext(ctx,`UPDATE conversion_long_jobs SET status=?,progress=?,stage=?,result_json=?,error_json=?,updated_at=?,row_version=row_version+1 WHERE id=? AND row_version=?`,status,progress,defaultString(stage,status),nullableJSON(rb,result),nullableJSON(eb,errorValue),now,id,rowVersion);if err!=nil{return current,err};n,_:=res.RowsAffected();if n!=1{return current,errx.New(errx.CodeConflict,"conversion job update conflict","P4-G-JOB")};return r.Get(ctx,id)
}
func (r *ConversionLongJobRepository) Cancel(ctx context.Context,id string,rowVersion int64)(ConversionLongJob,error){
	current,err:=r.Get(ctx,id);if err!=nil{return current,err};if current.Status=="COMPLETED"||current.Status=="FAILED"||current.Status=="CANCELLED"{return current,nil};if rowVersion!=current.RowVersion{return current,errx.New(errx.CodeConflict,"stale conversion job row_version","P4-G-JOB")};now:=time.Now().UTC().Format(time.RFC3339Nano);res,err:=r.store.db.ExecContext(ctx,`UPDATE conversion_long_jobs SET status='CANCELLED',stage='CANCELLED',cancel_requested=1,result_json=NULL,error_json=NULL,updated_at=?,row_version=row_version+1 WHERE id=? AND row_version=?`,now,id,rowVersion);if err!=nil{return current,err};n,_:=res.RowsAffected();if n!=1{return current,fmt.Errorf("cancel conflict")};return r.Get(ctx,id)
}
func nullableJSON(b []byte,v map[string]any) any { if v==nil{return nil};return string(b) }
