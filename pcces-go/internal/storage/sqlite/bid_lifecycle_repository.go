package sqlite

import (
	"context"
	"database/sql"
	"encoding/json"
	"fmt"
	"time"

	"github.com/liuxb99/pcces-web/pcces-go/internal/domain/money"
	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

type BidPriceVersion struct { ID string `json:"id"`; ProjectCode string `json:"project_code"`; Label string `json:"label"`; Status string `json:"status"`; TotalAmount string `json:"total_amount"`; Snapshot []BudgetDecimalItem `json:"snapshot"`; CreatedBy string `json:"created_by"`; CreatedAt string `json:"created_at"`; DeepLink string `json:"deep_link"` }
type BidLifecycleRepository struct{ store *Store }
func NewBidLifecycleRepository(store *Store)*BidLifecycleRepository{return &BidLifecycleRepository{store:store}}

func (r *BidLifecycleRepository) Convert(ctx context.Context,runID,source,target,actor string,overwrite bool)(map[string]any,error){
	if source==target{return nil,errx.New(errx.CodeInvalidArgument,"source and target projects must differ","P2-G-BID")}
	tx,err:=r.store.db.BeginTx(ctx,nil);if err!=nil{return nil,err};defer tx.Rollback()
	var count int;if err=tx.QueryRowContext(ctx,`SELECT COUNT(*) FROM budget_items_decimal WHERE project_code=?`,source).Scan(&count);err!=nil{return nil,err};if count==0{return nil,errx.New(errx.CodeInvalidArgument,"source budget has no items","P2-G-BID")}
	var targetCount int;if err=tx.QueryRowContext(ctx,`SELECT COUNT(*) FROM budget_items_decimal WHERE project_code=?`,target).Scan(&targetCount);err!=nil{return nil,err};if targetCount>0&&!overwrite{return nil,errx.New(errx.CodeConflict,"target BID already contains items","P2-G-BID")};if overwrite{if _,err=tx.ExecContext(ctx,`DELETE FROM budget_items_decimal WHERE project_code=?`,target);err!=nil{return nil,err}}
	rows,err:=tx.QueryContext(ctx,`SELECT id,parent_id,item_no,name,kind,quantity,unit_price,amount,quantity_scale,price_scale,amount_scale FROM budget_items_decimal WHERE project_code=? ORDER BY id`,source);if err!=nil{return nil,err};defer rows.Close()
	type row struct{id string;parent sql.NullString;no sql.NullString;name,kind,qty,price,amount string;qs,ps,as int};var items []row
	for rows.Next(){var v row;if err=rows.Scan(&v.id,&v.parent,&v.no,&v.name,&v.kind,&v.qty,&v.price,&v.amount,&v.qs,&v.ps,&v.as);err!=nil{return nil,err};items=append(items,v)}
	now:=time.Now().UTC().Format(time.RFC3339Nano);ids:=map[string]string{};for _,v:=range items{ids[v.id]="bid-"+target+"-"+v.id}
	for _,v:=range items{var parent any;if v.parent.Valid{parent=ids[v.parent.String]};var no any;if v.no.Valid{no=v.no.String};_,err=tx.ExecContext(ctx,`INSERT INTO budget_items_decimal(id,project_code,parent_id,item_no,name,kind,quantity,unit_price,amount,quantity_scale,price_scale,amount_scale,created_at,updated_at,row_version) VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,1)`,ids[v.id],target,parent,no,v.name,v.kind,v.qty,v.price,v.amount,v.qs,v.ps,v.as,now,now);if err!=nil{return nil,err}}
	_,_ = tx.ExecContext(ctx,`INSERT INTO budget_project_modes(project_code,mode,row_version,updated_by,updated_at) VALUES(?,?,?,?,?) ON CONFLICT(project_code) DO UPDATE SET mode='BID',row_version=row_version+1,updated_by=excluded.updated_by,updated_at=excluded.updated_at`,target,"BID",1,actor,now)
	result:=map[string]any{"id":runID,"operation":"BUD_TO_BID","status":"COMPLETED","source_project_code":source,"target_project_code":target,"copied_items":len(items),"deep_link":fmt.Sprintf("/app/projects/by-code/%s/bid-lifecycle?run=%s",target,runID)};payload,_:=json.Marshal(result)
	if _,err=tx.ExecContext(ctx,`INSERT INTO bid_conversion_runs(id,source_project_code,target_project_code,operation,status,result_json,created_by,created_at) VALUES(?,?,?,?,?,?,?,?)`,runID,source,target,"BUD_TO_BID","COMPLETED",string(payload),actor,now);err!=nil{return nil,err};if err=tx.Commit();err!=nil{return nil,err};return result,nil
}

func (r *BidLifecycleRepository) CreateVersion(ctx context.Context,id,project,label,status,actor string)(BidPriceVersion,error){
	rows,err:=r.store.db.QueryContext(ctx,`SELECT id,project_code,parent_id,item_no,name,kind,quantity,unit_price,amount,quantity_scale,price_scale,amount_scale,row_version FROM budget_items_decimal WHERE project_code=? ORDER BY id`,project);if err!=nil{return BidPriceVersion{},err};defer rows.Close();var items []BudgetDecimalItem;var amounts []string
	for rows.Next(){var v BudgetDecimalItem;if err=rows.Scan(&v.ID,&v.ProjectCode,&v.ParentID,&v.ItemNo,&v.Name,&v.Kind,&v.Quantity,&v.UnitPrice,&v.Amount,&v.QuantityScale,&v.PriceScale,&v.AmountScale,&v.RowVersion);err!=nil{return BidPriceVersion{},err};items=append(items,v);amounts=append(amounts,v.Amount)}
	if len(items)==0{return BidPriceVersion{},errx.New(errx.CodeInvalidArgument,"BID has no items","P2-G-BID")};total,err:=money.Sum(amounts,2);if err!=nil{return BidPriceVersion{},err};payload,_:=json.Marshal(items);now:=time.Now().UTC().Format(time.RFC3339Nano)
	_,err=r.store.db.ExecContext(ctx,`INSERT INTO bid_price_versions(id,project_code,label,status,total_amount,snapshot_json,created_by,created_at) VALUES(?,?,?,?,?,?,?,?)`,id,project,label,status,total,string(payload),actor,now);if err!=nil{return BidPriceVersion{},err};return BidPriceVersion{id,project,label,status,total,items,actor,now,fmt.Sprintf("/app/projects/by-code/%s/bid-lifecycle?version=%s",project,id)},nil
}

func (r *BidLifecycleRepository) GetVersion(ctx context.Context,id string)(BidPriceVersion,error){var v BidPriceVersion;var payload string;err:=r.store.db.QueryRowContext(ctx,`SELECT id,project_code,label,status,total_amount,snapshot_json,created_by,created_at FROM bid_price_versions WHERE id=?`,id).Scan(&v.ID,&v.ProjectCode,&v.Label,&v.Status,&v.TotalAmount,&payload,&v.CreatedBy,&v.CreatedAt);if err==sql.ErrNoRows{return v,errx.New(errx.CodeNotFound,"bid price version not found","P2-G-BID")};if err!=nil{return v,err};if err=json.Unmarshal([]byte(payload),&v.Snapshot);err!=nil{return v,err};v.DeepLink=fmt.Sprintf("/app/projects/by-code/%s/bid-lifecycle?version=%s",v.ProjectCode,v.ID);return v,nil}

func (r *BidLifecycleRepository) Rollback(ctx context.Context,versionID,runID,actor string)(map[string]any,error){v,err:=r.GetVersion(ctx,versionID);if err!=nil{return nil,err};tx,err:=r.store.db.BeginTx(ctx,nil);if err!=nil{return nil,err};defer tx.Rollback();if _,err=tx.ExecContext(ctx,`DELETE FROM budget_items_decimal WHERE project_code=?`,v.ProjectCode);err!=nil{return nil,err};now:=time.Now().UTC().Format(time.RFC3339Nano);for _,item:=range v.Snapshot{_,err=tx.ExecContext(ctx,`INSERT INTO budget_items_decimal(id,project_code,parent_id,item_no,name,kind,quantity,unit_price,amount,quantity_scale,price_scale,amount_scale,created_at,updated_at,row_version) VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)`,item.ID,item.ProjectCode,item.ParentID,item.ItemNo,item.Name,item.Kind,item.Quantity,item.UnitPrice,item.Amount,item.QuantityScale,item.PriceScale,item.AmountScale,now,now,item.RowVersion+1);if err!=nil{return nil,err}};result:=map[string]any{"id":runID,"operation":"BID_ROLLBACK","status":"COMPLETED","target_project_code":v.ProjectCode,"restored_version":versionID,"restored_items":len(v.Snapshot)};payload,_:=json.Marshal(result);_,err=tx.ExecContext(ctx,`INSERT INTO bid_conversion_runs(id,source_project_code,target_project_code,operation,status,result_json,created_by,created_at) VALUES(?,?,?,?,?,?,?,?)`,runID,v.ProjectCode,v.ProjectCode,"BID_ROLLBACK","COMPLETED",string(payload),actor,now);if err!=nil{return nil,err};if err=tx.Commit();err!=nil{return nil,err};return result,nil}
