package sqlite

import (
	"context"
	"database/sql"
	"encoding/json"
	"fmt"
	"time"

	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

type BudgetVersion struct {
	ID, ProjectCode, Label, Status, SnapshotJSON, CreatedBy, CreatedAt string
}

type BudgetLock struct {
	ProjectCode string `json:"project_code"`
	Locked bool `json:"locked"`
	Reason string `json:"reason,omitempty"`
	LockedBy string `json:"locked_by,omitempty"`
	UpdatedAt string `json:"updated_at,omitempty"`
}

type BudgetVersionRepository struct{ store *Store }
func NewBudgetVersionRepository(store *Store) *BudgetVersionRepository { return &BudgetVersionRepository{store:store} }

func (r *BudgetVersionRepository) Create(ctx context.Context, id, projectCode, label, status, actor string) (BudgetVersion,error) {
	if id==""||projectCode==""||actor=="" { return BudgetVersion{},errx.New(errx.CodeInvalidArgument,"id, project_code and actor are required","P2-G-VERSION") }
	rows,err:=r.store.db.QueryContext(ctx,`SELECT id,project_code,parent_id,item_no,name,kind,quantity,unit_price,amount,quantity_scale,price_scale,amount_scale,row_version FROM budget_items_decimal WHERE project_code=? ORDER BY id`,projectCode)
	if err!=nil{return BudgetVersion{},err};defer rows.Close()
	type item struct{ ID,ProjectCode string; ParentID sql.NullString; ItemNo,Name,Kind,Quantity,UnitPrice,Amount string; QuantityScale,PriceScale,AmountScale int; RowVersion int64 }
	var snapshot []map[string]any
	for rows.Next(){var v item;if err=rows.Scan(&v.ID,&v.ProjectCode,&v.ParentID,&v.ItemNo,&v.Name,&v.Kind,&v.Quantity,&v.UnitPrice,&v.Amount,&v.QuantityScale,&v.PriceScale,&v.AmountScale,&v.RowVersion);err!=nil{return BudgetVersion{},err};m:=map[string]any{"id":v.ID,"project_code":v.ProjectCode,"parent_id":nil,"item_no":v.ItemNo,"name":v.Name,"kind":v.Kind,"quantity":v.Quantity,"unit_price":v.UnitPrice,"amount":v.Amount,"quantity_scale":v.QuantityScale,"price_scale":v.PriceScale,"amount_scale":v.AmountScale,"row_version":v.RowVersion};if v.ParentID.Valid{m["parent_id"]=v.ParentID.String};snapshot=append(snapshot,m)}
	payload,err:=json.Marshal(snapshot);if err!=nil{return BudgetVersion{},err}
	if label==""{label=id};if status==""{status="DRAFT"};now:=time.Now().UTC().Format(time.RFC3339Nano)
	_,err=r.store.db.ExecContext(ctx,`INSERT INTO budget_versions(id,project_code,label,status,snapshot_json,created_by,created_at) VALUES(?,?,?,?,?,?,?)`,id,projectCode,label,status,string(payload),actor,now)
	if err!=nil{return BudgetVersion{},err}
	return BudgetVersion{ID:id,ProjectCode:projectCode,Label:label,Status:status,SnapshotJSON:string(payload),CreatedBy:actor,CreatedAt:now},nil
}

func (r *BudgetVersionRepository) Get(ctx context.Context,id string)(BudgetVersion,error){var v BudgetVersion;err:=r.store.db.QueryRowContext(ctx,`SELECT id,project_code,label,status,snapshot_json,created_by,created_at FROM budget_versions WHERE id=?`,id).Scan(&v.ID,&v.ProjectCode,&v.Label,&v.Status,&v.SnapshotJSON,&v.CreatedBy,&v.CreatedAt);if err==sql.ErrNoRows{return v,errx.New(errx.CodeNotFound,"budget version not found","P2-G-VERSION")};return v,err}
func (r *BudgetVersionRepository) List(ctx context.Context,projectCode string)([]BudgetVersion,error){rows,err:=r.store.db.QueryContext(ctx,`SELECT id,project_code,label,status,snapshot_json,created_by,created_at FROM budget_versions WHERE project_code=? ORDER BY created_at DESC`,projectCode);if err!=nil{return nil,err};defer rows.Close();var out []BudgetVersion;for rows.Next(){var v BudgetVersion;if err=rows.Scan(&v.ID,&v.ProjectCode,&v.Label,&v.Status,&v.SnapshotJSON,&v.CreatedBy,&v.CreatedAt);err!=nil{return nil,err};out=append(out,v)};return out,rows.Err()}

func (r *BudgetVersionRepository) SetLock(ctx context.Context,projectCode string,locked bool,actor,reason string)(BudgetLock,error){if projectCode==""{return BudgetLock{},errx.New(errx.CodeInvalidArgument,"project_code is required","P2-G-VERSION")};now:=time.Now().UTC().Format(time.RFC3339Nano);lockedInt:=0;lockedBy:="";if locked{lockedInt=1;lockedBy=actor};_,err:=r.store.db.ExecContext(ctx,`INSERT INTO budget_project_locks(project_code,locked,reason,locked_by,updated_at) VALUES(?,?,?,?,?) ON CONFLICT(project_code) DO UPDATE SET locked=excluded.locked,reason=excluded.reason,locked_by=excluded.locked_by,updated_at=excluded.updated_at`,projectCode,lockedInt,reason,lockedBy,now);if err!=nil{return BudgetLock{},err};return BudgetLock{ProjectCode:projectCode,Locked:locked,Reason:reason,LockedBy:lockedBy,UpdatedAt:now},nil}
func (r *BudgetVersionRepository) Lock(ctx context.Context,projectCode string)(BudgetLock,error){var v BudgetLock;var locked int;err:=r.store.db.QueryRowContext(ctx,`SELECT project_code,locked,COALESCE(reason,''),COALESCE(locked_by,''),updated_at FROM budget_project_locks WHERE project_code=?`,projectCode).Scan(&v.ProjectCode,&locked,&v.Reason,&v.LockedBy,&v.UpdatedAt);if err==sql.ErrNoRows{return BudgetLock{ProjectCode:projectCode},nil};v.Locked=locked!=0;return v,err}

func (r *BudgetVersionRepository) Restore(ctx context.Context,versionID,actor,newVersionID string)(BudgetVersion,error){version,err:=r.Get(ctx,versionID);if err!=nil{return BudgetVersion{},err};lock,err:=r.Lock(ctx,version.ProjectCode);if err!=nil{return BudgetVersion{},err};if lock.Locked{return BudgetVersion{},errx.New(errx.CodeConflict,"budget project is locked","P2-G-VERSION")}
	var snapshot []map[string]any;if err=json.Unmarshal([]byte(version.SnapshotJSON),&snapshot);err!=nil{return BudgetVersion{},err}
	tx,err:=r.store.db.BeginTx(ctx,nil);if err!=nil{return BudgetVersion{},err};defer func(){_ = tx.Rollback()}();if _,err=tx.ExecContext(ctx,`DELETE FROM budget_items_decimal WHERE project_code=?`,version.ProjectCode);err!=nil{return BudgetVersion{},err};now:=time.Now().UTC().Format(time.RFC3339Nano)
	for _,m:=range snapshot{rv:=int64(1);switch value:=m["row_version"].(type){case float64:rv=int64(value)+1;case int64:rv=value+1};_,err=tx.ExecContext(ctx,`INSERT INTO budget_items_decimal(id,project_code,parent_id,item_no,name,kind,quantity,unit_price,amount,quantity_scale,price_scale,amount_scale,created_at,updated_at,row_version) VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)`,m["id"],m["project_code"],m["parent_id"],m["item_no"],m["name"],m["kind"],m["quantity"],m["unit_price"],m["amount"],m["quantity_scale"],m["price_scale"],m["amount_scale"],now,now,rv);if err!=nil{return BudgetVersion{},fmt.Errorf("restore item: %w",err)}}
	if err=tx.Commit();err!=nil{return BudgetVersion{},err};return r.Create(ctx,newVersionID,version.ProjectCode,"RESTORE:"+version.Label,"RESTORED",actor)
}
