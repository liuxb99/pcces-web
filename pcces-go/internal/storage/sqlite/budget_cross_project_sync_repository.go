package sqlite

import (
	"context"
	"database/sql"
	"encoding/json"
	"fmt"
	"time"

	"github.com/liuxb99/pcces-web/pcces-go/internal/domain/money"
)

type BudgetCrossProjectRun struct {
	ID string `json:"id"`
	SourceProjectCode string `json:"source_project_code"`
	TargetProjectCode string `json:"target_project_code"`
	Operation string `json:"operation"`
	Status string `json:"status"`
	Result map[string]any `json:"result"`
	CreatedBy string `json:"created_by"`
	CreatedAt string `json:"created_at"`
	DeepLink string `json:"deep_link"`
}

type BudgetCrossProjectSyncRepository struct{ store *Store }
func NewBudgetCrossProjectSyncRepository(store *Store)*BudgetCrossProjectSyncRepository{return &BudgetCrossProjectSyncRepository{store:store}}

func (r *BudgetCrossProjectSyncRepository) Propagate(ctx context.Context,id,sourceProject,targetProject,actor string)(BudgetCrossProjectRun,error){
	tx,err:=r.store.db.BeginTx(ctx,nil);if err!=nil{return BudgetCrossProjectRun{},err};defer tx.Rollback()
	rows,err:=tx.QueryContext(ctx,`SELECT id,source_item_id,target_item_id FROM budget_cross_project_refs WHERE source_project_code=? AND target_project_code=? AND enabled=1`,sourceProject,targetProject);if err!=nil{return BudgetCrossProjectRun{},err}
	type ref struct{id,source,target string};var refs []ref
	for rows.Next(){var v ref;if err=rows.Scan(&v.id,&v.source,&v.target);err!=nil{return BudgetCrossProjectRun{},err};refs=append(refs,v)};rows.Close()
	updated:=[]map[string]any{};broken:=[]map[string]any{}
	for _,v:=range refs{
		var sourcePrice string
		if err=tx.QueryRowContext(ctx,`SELECT unit_price FROM budget_items_decimal WHERE id=? AND project_code=?`,v.source,sourceProject).Scan(&sourcePrice);err!=nil{broken=append(broken,map[string]any{"reference_id":v.id,"code":"BROKEN_REFERENCE"});continue}
		var quantity string;var scale int;var version int64
		if err=tx.QueryRowContext(ctx,`SELECT quantity,amount_scale,row_version FROM budget_items_decimal WHERE id=? AND project_code=?`,v.target,targetProject).Scan(&quantity,&scale,&version);err!=nil{broken=append(broken,map[string]any{"reference_id":v.id,"code":"BROKEN_REFERENCE"});continue}
		amount,calcErr:=money.CalculateBudgetLeaf(quantity,sourcePrice,scale);if calcErr!=nil{return BudgetCrossProjectRun{},calcErr}
		res,execErr:=tx.ExecContext(ctx,`UPDATE budget_items_decimal SET unit_price=?,amount=?,row_version=row_version+1,updated_at=? WHERE id=? AND row_version=?`,sourcePrice,amount,time.Now().UTC().Format(time.RFC3339Nano),v.target,version);if execErr!=nil{return BudgetCrossProjectRun{},execErr};count,_:=res.RowsAffected();if count!=1{broken=append(broken,map[string]any{"reference_id":v.id,"code":"CONFLICT"});continue}
		updated=append(updated,map[string]any{"reference_id":v.id,"source_item_id":v.source,"target_item_id":v.target,"unit_price":sourcePrice,"amount":amount})
	}
	status:="COMPLETED";if len(broken)>0{status="COMPLETED_WITH_ERRORS"}
	result:=map[string]any{"updated":updated,"broken":broken,"updated_items":len(updated)}
	run,err:=r.insertRun(ctx,tx,id,sourceProject,targetProject,"PROPAGATE",status,result,actor);if err!=nil{return BudgetCrossProjectRun{},err};if err=tx.Commit();err!=nil{return BudgetCrossProjectRun{},err};return run,nil
}

func (r *BudgetCrossProjectSyncRepository) Diff(ctx context.Context,id,leftProject,rightProject,actor string)(BudgetCrossProjectRun,error){
	tx,err:=r.store.db.BeginTx(ctx,nil);if err!=nil{return BudgetCrossProjectRun{},err};defer tx.Rollback()
	load:=func(project string)(map[string]map[string]any,error){
		rows,e:=tx.QueryContext(ctx,`SELECT b.id,b.item_no,b.name,b.kind,b.quantity,b.unit_price,b.amount,COALESCE(s.item_class,'') FROM budget_items_decimal b LEFT JOIN budget_item_semantics s ON s.item_id=b.id WHERE b.project_code=?`,project);if e!=nil{return nil,e};defer rows.Close();out:=map[string]map[string]any{}
		for rows.Next(){var id,no,name,kind,q,p,a,class string;if e=rows.Scan(&id,&no,&name,&kind,&q,&p,&a,&class);e!=nil{return nil,e};key:=no;if key==""{key=id};out[key]=map[string]any{"id":id,"item_no":no,"name":name,"kind":kind,"item_class":class,"quantity":q,"unit_price":p,"amount":a}}
		return out,nil
	}
	left,err:=load(leftProject);if err!=nil{return BudgetCrossProjectRun{},err};right,err:=load(rightProject);if err!=nil{return BudgetCrossProjectRun{},err}
	added:=[]map[string]any{};removed:=[]map[string]any{};changed:=[]map[string]any{}
	for key,row:=range right{if _,ok:=left[key];!ok{added=append(added,row)}}
	for key,row:=range left{if _,ok:=right[key];!ok{removed=append(removed,row)}}
	for key,before:=range left{if after,ok:=right[key];ok{a,_:=json.Marshal(before);b,_:=json.Marshal(after);if string(a)!=string(b){changed=append(changed,map[string]any{"item_no":key,"before":before,"after":after})}}}
	result:=map[string]any{"left_project_code":leftProject,"right_project_code":rightProject,"added":added,"removed":removed,"changed":changed}
	run,err:=r.insertRun(ctx,tx,id,leftProject,rightProject,"MODE_DIFF","COMPLETED",result,actor);if err!=nil{return BudgetCrossProjectRun{},err};if err=tx.Commit();err!=nil{return BudgetCrossProjectRun{},err};return run,nil
}

func (r *BudgetCrossProjectSyncRepository) insertRun(ctx context.Context,tx *sql.Tx,id,source,target,operation,status string,result map[string]any,actor string)(BudgetCrossProjectRun,error){
	created:=time.Now().UTC().Format(time.RFC3339Nano);payload,_:=json.Marshal(result)
	_,err:=tx.ExecContext(ctx,`INSERT INTO budget_cross_project_runs(id,source_project_code,target_project_code,operation,status,result_json,created_by,created_at) VALUES(?,?,?,?,?,?,?,?)`,id,source,target,operation,status,string(payload),actor,created);if err!=nil{return BudgetCrossProjectRun{},err}
	return BudgetCrossProjectRun{ID:id,SourceProjectCode:source,TargetProjectCode:target,Operation:operation,Status:status,Result:result,CreatedBy:actor,CreatedAt:created,DeepLink:fmt.Sprintf("/app/projects/by-code/%s/budget-validation?sync=%s",target,id)},nil
}
