package sqlite

import (
	"context"
	"encoding/json"
	"fmt"
	"time"

	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

func (r *MRSOperationsRepository) RunImportJob(ctx context.Context,id string)(MRSImportJob,error){
	job,err:=r.GetImportJob(ctx,id);if err!=nil{return job,err}
	if job.Status!="PENDING"&&job.Status!="RUNNING"{return job,errx.New(errx.CodeConflict,"import job is terminal","P3-G-MRS")}
	if job.CancelRequested{return r.CancelImportJob(ctx,id)}
	if job.Format!="JSON"{return job,errx.New(errx.CodeInvalidArgument,"Go local import job currently requires JSON","P3-G-MRS")}
	var rows []map[string]any;if err=json.Unmarshal([]byte(job.Payload),&rows);err!=nil{return job,errx.Wrap(errx.CodeInvalidArgument,"decode MRS import payload","P3-G-MRS",err)}
	_,err=r.store.db.ExecContext(ctx,`UPDATE mrs_import_jobs SET status='RUNNING',updated_at=? WHERE id=?`,time.Now().UTC().Format(time.RFC3339Nano),id);if err!=nil{return job,err}
	catalog:=NewMRSCatalogRepository(r.store);imported,skipped:=0,0;errors:=[]map[string]any{}
	for index,row:=range rows{
		current,checkErr:=r.GetImportJob(ctx,id);if checkErr!=nil{return current,checkErr};if current.CancelRequested{_,_ = r.store.db.ExecContext(ctx,`UPDATE mrs_import_jobs SET status='CANCELLED',processed_rows=?,updated_at=? WHERE id=?`,index,time.Now().UTC().Format(time.RFC3339Nano),id);return r.GetImportJob(ctx,id)}
		itemID:=textValue(row["id"]);if itemID==""{itemID=textValue(row["code"])};if itemID==""{errors=append(errors,map[string]any{"row":index+1,"detail":"id or code is required"});continue}
		existing,e:=catalog.GetItem(ctx,itemID);if e==nil&&!job.Overwrite{skipped++;continue}
		version:=int64(0);if e==nil{version=existing.RowVersion}
		item:=MRSCatalogItem{ID:itemID,Code:textValue(row["code"]),Name:textValue(row["name"]),Category:textValue(row["category"]),CurrentPrice:textValue(row["current_price"]),PriceScale:intValue(row["price_scale"],4),Enabled:boolValue(row["enabled"],true),RowVersion:version}
		if unit:=textValue(row["unit"]);unit!=""{item.Unit=&unit};if source:=textValue(row["source"]);source!=""{item.Source=&source}
		if _,saveErr:=catalog.SaveItem(ctx,item,job.CreatedBy,textValue(row["effective_date"]));saveErr!=nil{errors=append(errors,map[string]any{"row":index+1,"detail":saveErr.Error()})}else{imported++}
	}
	status:="COMPLETED";if len(errors)>0{status="COMPLETED_WITH_ERRORS"};payload,_:=json.Marshal(errors);_,err=r.store.db.ExecContext(ctx,`UPDATE mrs_import_jobs SET status=?,processed_rows=?,imported_rows=?,skipped_rows=?,errors_json=?,updated_at=? WHERE id=?`,status,len(rows),imported,skipped,string(payload),time.Now().UTC().Format(time.RFC3339Nano),id);if err!=nil{return job,err};return r.GetImportJob(ctx,id)
}

func textValue(value any)string{if value==nil{return ""};return fmt.Sprint(value)}
func intValue(value any,fallback int)int{if value==nil{return fallback};var out int;if _,err:=fmt.Sscan(fmt.Sprint(value),&out);err!=nil{return fallback};return out}
func boolValue(value any,fallback bool)bool{if value==nil{return fallback};switch fmt.Sprint(value){case "true","1","yes","TRUE":return true;case "false","0","no","FALSE":return false};return fallback}
