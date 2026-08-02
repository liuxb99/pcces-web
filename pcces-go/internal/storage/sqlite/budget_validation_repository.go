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

type BudgetValidationIssue struct { Code string `json:"code"`; ItemID string `json:"item_id,omitempty"`; Blocking bool `json:"blocking"`; Detail string `json:"detail,omitempty"` }
type BudgetValidationResult struct { ID,ProjectCode,Mode,CreatedAt,DeepLink string; Passed bool; BlockingIssues,Warnings int; Issues []BudgetValidationIssue }
type BudgetValidationRepository struct{ store *Store }
func NewBudgetValidationRepository(store *Store)*BudgetValidationRepository{return &BudgetValidationRepository{store:store}}

func (r *BudgetValidationRepository) Mode(ctx context.Context,project string)(string,int64,error){
	var mode string;var version int64
	err:=r.store.db.QueryRowContext(ctx,`SELECT mode,row_version FROM budget_project_modes WHERE project_code=?`,project).Scan(&mode,&version)
	if err==sql.ErrNoRows{return "BUD",0,nil};return mode,version,err
}
func (r *BudgetValidationRepository) SetMode(ctx context.Context,project,mode,actor string,rowVersion int64)(string,int64,error){
	mode=strings.ToUpper(mode);if mode!="BUD"&&mode!="BID"{return "",0,errx.New(errx.CodeInvalidArgument,"mode must be BUD or BID","P2-G-VALIDATE")}
	current,currentVersion,err:=r.Mode(ctx,project);_ = current;if err!=nil{return "",0,err};if currentVersion!=rowVersion{return "",0,errx.New(errx.CodeConflict,"budget mode row version conflict","P2-G-VALIDATE")}
	now:=time.Now().UTC().Format(time.RFC3339Nano)
	if currentVersion==0{_,err=r.store.db.ExecContext(ctx,`INSERT INTO budget_project_modes(project_code,mode,row_version,updated_by,updated_at) VALUES(?,?,?,?,?)`,project,mode,1,actor,now);return mode,1,err}
	res,err:=r.store.db.ExecContext(ctx,`UPDATE budget_project_modes SET mode=?,row_version=row_version+1,updated_by=?,updated_at=? WHERE project_code=? AND row_version=?`,mode,actor,now,project,rowVersion);if err!=nil{return "",0,err};n,_:=res.RowsAffected();if n!=1{return "",0,errx.New(errx.CodeConflict,"budget mode row version conflict","P2-G-VALIDATE")};return mode,rowVersion+1,nil
}
func (r *BudgetValidationRepository) SetItemClass(ctx context.Context,project,item,class,actor string,rowVersion int64) error{
	class=strings.ToUpper(class);if class!="A"&&class!="B"&&class!="C"{return errx.New(errx.CodeInvalidArgument,"item class must be A, B or C","P2-G-VALIDATE")}
	var count int;if err:=r.store.db.QueryRowContext(ctx,`SELECT COUNT(*) FROM budget_items_decimal WHERE id=? AND project_code=?`,item,project).Scan(&count);err!=nil||count!=1{return errx.New(errx.CodeNotFound,"budget item not found","P2-G-VALIDATE")}
	var current int64;err:=r.store.db.QueryRowContext(ctx,`SELECT row_version FROM budget_item_semantics WHERE item_id=?`,item).Scan(&current)
	now:=time.Now().UTC().Format(time.RFC3339Nano)
	if err==sql.ErrNoRows{if rowVersion!=0{return errx.New(errx.CodeConflict,"item class row version conflict","P2-G-VALIDATE")};_,err=r.store.db.ExecContext(ctx,`INSERT INTO budget_item_semantics(item_id,project_code,item_class,row_version,updated_by,updated_at) VALUES(?,?,?,?,?,?)`,item,project,class,1,actor,now);return err}
	if err!=nil{return err};if current!=rowVersion{return errx.New(errx.CodeConflict,"item class row version conflict","P2-G-VALIDATE")}
	_,err=r.store.db.ExecContext(ctx,`UPDATE budget_item_semantics SET item_class=?,row_version=row_version+1,updated_by=?,updated_at=? WHERE item_id=?`,class,actor,now,item);return err
}
func (r *BudgetValidationRepository) AddReference(ctx context.Context,id,sourceProject,sourceItem,targetProject,targetItem,actor string) error{
	if sourceProject==targetProject&&sourceItem==targetItem{return errx.New(errx.CodeInvalidArgument,"self reference is not allowed","P2-G-VALIDATE")}
	var count int;for _,v:=range [][2]string{{sourceItem,sourceProject},{targetItem,targetProject}}{if err:=r.store.db.QueryRowContext(ctx,`SELECT COUNT(*) FROM budget_items_decimal WHERE id=? AND project_code=?`,v[0],v[1]).Scan(&count);err!=nil||count!=1{return errx.New(errx.CodeNotFound,"source and target items must exist","P2-G-VALIDATE")}}
	_,err:=r.store.db.ExecContext(ctx,`INSERT OR IGNORE INTO budget_cross_project_refs(id,source_project_code,source_item_id,target_project_code,target_item_id,enabled,created_by,created_at) VALUES(?,?,?,?,?,?,?,?)`,id,sourceProject,sourceItem,targetProject,targetItem,1,actor,time.Now().UTC().Format(time.RFC3339Nano));return err
}
func (r *BudgetValidationRepository) Check(ctx context.Context,id,project,actor string)(BudgetValidationResult,error){
	mode,_,err:=r.Mode(ctx,project);if err!=nil{return BudgetValidationResult{},err}
	rows,err:=r.store.db.QueryContext(ctx,`SELECT b.id,b.parent_id,b.item_no,b.unit_price,COALESCE(s.item_class,'') FROM budget_items_decimal b LEFT JOIN budget_item_semantics s ON s.item_id=b.id WHERE b.project_code=?`,project);if err!=nil{return BudgetValidationResult{},err};defer rows.Close()
	type item struct{id,parent,no,price,class string};var items []item;ids:=map[string]bool{};numbers:=map[string][]string{}
	for rows.Next(){var v item;var parent sql.NullString;if err=rows.Scan(&v.id,&parent,&v.no,&v.price,&v.class);err!=nil{return BudgetValidationResult{},err};if parent.Valid{v.parent=parent.String};items=append(items,v);ids[v.id]=true;if strings.TrimSpace(v.no)!=""{numbers[v.no]=append(numbers[v.no],v.id)}}
	issues:=[]BudgetValidationIssue{}
	for _,v:=range items{if v.parent!=""&&!ids[v.parent]{issues=append(issues,BudgetValidationIssue{"BROKEN_PARENT",v.id,true,""})};if v.class==""{issues=append(issues,BudgetValidationIssue{"ITEM_CLASS_MISSING",v.id,true,""})};if mode=="BID"&&v.class=="A"&&(v.price=="0"||strings.HasPrefix(v.price,"0.00")){issues=append(issues,BudgetValidationIssue{"BID_PRICE_REQUIRED",v.id,true,""})}}
	for no,list:=range numbers{if len(list)>1{issues=append(issues,BudgetValidationIssue{"DUPLICATE_ITEM_NO","",true,no})}}
	result:=BudgetValidationResult{ID:id,ProjectCode:project,Mode:mode,Passed:true,Issues:issues,CreatedAt:time.Now().UTC().Format(time.RFC3339Nano),DeepLink:fmt.Sprintf("/app/projects/by-code/%s/budget-validation?check=%s",project,id)}
	for _,v:=range issues{if v.Blocking{result.BlockingIssues++;result.Passed=false}else{result.Warnings++}}
	payload,_:=json.Marshal(result);_,err=r.store.db.ExecContext(ctx,`INSERT INTO budget_self_check_runs(id,project_code,mode,blocking,result_json,created_by,created_at) VALUES(?,?,?,?,?,?,?)`,id,project,mode,1,string(payload),actor,result.CreatedAt);return result,err
}
