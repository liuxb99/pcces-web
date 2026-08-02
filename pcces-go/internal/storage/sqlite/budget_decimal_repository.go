package sqlite

import (
	"context"
	"database/sql"
	"time"

	"github.com/liuxb99/pcces-web/pcces-go/internal/domain/money"
	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

type BudgetDecimalItem struct {
	ID, ProjectCode, Name, Kind, Quantity, UnitPrice, Amount string
	ParentID, ItemNo                                           *string
	QuantityScale, PriceScale, AmountScale                     int
	RowVersion                                                  int64
}

type ResourceDecimal struct {
	ID, Code, Name, UnitPrice string
	Unit                      *string
	PriceScale                int
	RowVersion                int64
}

type ResourceBreakdownDecimal struct {
	ID, ResourceID, Code, Name, Quantity, UnitPrice, Amount string
	Unit                                                     *string
	QuantityScale, PriceScale, AmountScale                   int
	RowVersion                                                int64
}

type BudgetDecimalRepository struct{ store *Store }

func NewBudgetDecimalRepository(store *Store) *BudgetDecimalRepository { return &BudgetDecimalRepository{store: store} }

func (r *BudgetDecimalRepository) Save(ctx context.Context, item BudgetDecimalItem) (BudgetDecimalItem, error) {
	if item.ID == "" || item.ProjectCode == "" || item.Name == "" {
		return BudgetDecimalItem{}, errx.New(errx.CodeInvalidArgument, "id, project_code and name are required", "P2-G1")
	}
	q, err := money.Quantize(item.Quantity, item.QuantityScale); if err != nil { return BudgetDecimalItem{}, err }
	p, err := money.Quantize(item.UnitPrice, item.PriceScale); if err != nil { return BudgetDecimalItem{}, err }
	a, err := money.CalculateBudgetLeaf(q, p, item.AmountScale); if err != nil { return BudgetDecimalItem{}, err }
	if item.Kind == "B" { a, _ = money.Quantize(item.Amount, item.AmountScale) }
	now := time.Now().UTC().Format(time.RFC3339Nano)
	if item.RowVersion == 0 {
		_, err = r.store.db.ExecContext(ctx, `INSERT INTO budget_items_decimal(id,project_code,parent_id,item_no,name,kind,quantity,unit_price,amount,quantity_scale,price_scale,amount_scale,created_at,updated_at,row_version) VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,1)`, item.ID,item.ProjectCode,item.ParentID,item.ItemNo,item.Name,item.Kind,q,p,a,item.QuantityScale,item.PriceScale,item.AmountScale,now,now)
	} else {
		res, execErr := r.store.db.ExecContext(ctx, `UPDATE budget_items_decimal SET project_code=?,parent_id=?,item_no=?,name=?,kind=?,quantity=?,unit_price=?,amount=?,quantity_scale=?,price_scale=?,amount_scale=?,updated_at=?,row_version=row_version+1 WHERE id=? AND row_version=?`, item.ProjectCode,item.ParentID,item.ItemNo,item.Name,item.Kind,q,p,a,item.QuantityScale,item.PriceScale,item.AmountScale,now,item.ID,item.RowVersion)
		if execErr == nil { n,_:=res.RowsAffected(); if n!=1 { execErr=errx.New(errx.CodeConflict,"budget row_version conflict","P2-G1") } }
		err = execErr
	}
	if err != nil { return BudgetDecimalItem{}, errx.Wrap(errx.CodeDatabase,"save decimal budget item","P2-G1",err) }
	return r.Get(ctx,item.ID)
}

func (r *BudgetDecimalRepository) Get(ctx context.Context, id string) (BudgetDecimalItem,error) {
	var item BudgetDecimalItem
	err:=r.store.db.QueryRowContext(ctx,`SELECT id,project_code,parent_id,item_no,name,kind,quantity,unit_price,amount,quantity_scale,price_scale,amount_scale,row_version FROM budget_items_decimal WHERE id=?`,id).Scan(&item.ID,&item.ProjectCode,&item.ParentID,&item.ItemNo,&item.Name,&item.Kind,&item.Quantity,&item.UnitPrice,&item.Amount,&item.QuantityScale,&item.PriceScale,&item.AmountScale,&item.RowVersion)
	if err==sql.ErrNoRows { return item,errx.New(errx.CodeNotFound,"budget item not found","P2-G1") }
	return item,err
}

func (r *BudgetDecimalRepository) RecalculateProject(ctx context.Context, projectCode string) (string,error) {
	rows,err:=r.store.db.QueryContext(ctx,`SELECT id,parent_id,kind,quantity,unit_price,amount_scale FROM budget_items_decimal WHERE project_code=?`,projectCode); if err!=nil{return "",err}
	defer rows.Close()
	type node struct{id string; parent *string; kind,q,p string; scale int}
	nodes:=map[string]node{}; children:=map[string][]string{}; roots:=[]string{}
	for rows.Next(){var n node;if err:=rows.Scan(&n.id,&n.parent,&n.kind,&n.q,&n.p,&n.scale);err!=nil{return "",err};nodes[n.id]=n;if n.parent==nil{roots=append(roots,n.id)}else{children[*n.parent]=append(children[*n.parent],n.id)}}
	var visit func(string)(string,error)
	visit=func(id string)(string,error){n:=nodes[id]; var amount string; var e error;if len(children[id])>0||n.kind=="B"{vals:=[]string{};for _,child:=range children[id]{v,er:=visit(child);if er!=nil{return "",er};vals=append(vals,v)};amount,e=money.CalculateBudgetRollup(vals,n.scale)}else{amount,e=money.CalculateBudgetLeaf(n.q,n.p,n.scale)};if e!=nil{return "",e};_,e=r.store.db.ExecContext(ctx,`UPDATE budget_items_decimal SET amount=?,updated_at=strftime('%Y-%m-%dT%H:%M:%fZ','now'),row_version=row_version+1 WHERE id=? AND amount<>?`,amount,id,amount);return amount,e}
	totals:=[]string{};for _,id:=range roots{v,e:=visit(id);if e!=nil{return "",e};totals=append(totals,v)};return money.CalculateBudgetRollup(totals,2)
}

type ResourceDecimalRepository struct{ store *Store }
func NewResourceDecimalRepository(store *Store)*ResourceDecimalRepository{return &ResourceDecimalRepository{store:store}}

func (r *ResourceDecimalRepository) SaveResource(ctx context.Context,item ResourceDecimal)(ResourceDecimal,error){
	price,err:=money.Quantize(item.UnitPrice,item.PriceScale);if err!=nil{return item,err};now:=time.Now().UTC().Format(time.RFC3339Nano)
	if item.RowVersion==0{_,err=r.store.db.ExecContext(ctx,`INSERT INTO resources_decimal(id,code,name,unit,unit_price,price_scale,created_at,updated_at,row_version) VALUES(?,?,?,?,?,?,?,?,1)`,item.ID,item.Code,item.Name,item.Unit,price,item.PriceScale,now,now)}else{res,e:=r.store.db.ExecContext(ctx,`UPDATE resources_decimal SET code=?,name=?,unit=?,unit_price=?,price_scale=?,updated_at=?,row_version=row_version+1 WHERE id=? AND row_version=?`,item.Code,item.Name,item.Unit,price,item.PriceScale,now,item.ID,item.RowVersion);err=e;if err==nil{n,_:=res.RowsAffected();if n!=1{err=errx.New(errx.CodeConflict,"resource row_version conflict","P3-G1")}}};if err!=nil{return item,err};return r.GetResource(ctx,item.ID)
}
func (r *ResourceDecimalRepository) GetResource(ctx context.Context,id string)(ResourceDecimal,error){var item ResourceDecimal;err:=r.store.db.QueryRowContext(ctx,`SELECT id,code,name,unit,unit_price,price_scale,row_version FROM resources_decimal WHERE id=?`,id).Scan(&item.ID,&item.Code,&item.Name,&item.Unit,&item.UnitPrice,&item.PriceScale,&item.RowVersion);return item,err}
func (r *ResourceDecimalRepository) SaveBreakdown(ctx context.Context,item ResourceBreakdownDecimal)(ResourceBreakdownDecimal,error){q,e:=money.Quantize(item.Quantity,item.QuantityScale);if e!=nil{return item,e};p,e:=money.Quantize(item.UnitPrice,item.PriceScale);if e!=nil{return item,e};a,e:=money.CalculateBudgetLeaf(q,p,item.AmountScale);if e!=nil{return item,e};now:=time.Now().UTC().Format(time.RFC3339Nano);if item.RowVersion==0{_,e=r.store.db.ExecContext(ctx,`INSERT INTO resource_breakdowns_decimal(id,resource_id,code,name,unit,quantity,unit_price,amount,quantity_scale,price_scale,amount_scale,created_at,updated_at,row_version) VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,1)`,item.ID,item.ResourceID,item.Code,item.Name,item.Unit,q,p,a,item.QuantityScale,item.PriceScale,item.AmountScale,now,now)}else{res,x:=r.store.db.ExecContext(ctx,`UPDATE resource_breakdowns_decimal SET resource_id=?,code=?,name=?,unit=?,quantity=?,unit_price=?,amount=?,quantity_scale=?,price_scale=?,amount_scale=?,updated_at=?,row_version=row_version+1 WHERE id=? AND row_version=?`,item.ResourceID,item.Code,item.Name,item.Unit,q,p,a,item.QuantityScale,item.PriceScale,item.AmountScale,now,item.ID,item.RowVersion);e=x;if e==nil{n,_:=res.RowsAffected();if n!=1{e=errx.New(errx.CodeConflict,"breakdown row_version conflict","P3-G1")}}};if e!=nil{return item,e};_,e=r.RecalculateResource(ctx,item.ResourceID);if e!=nil{return item,e};return r.GetBreakdown(ctx,item.ID)}
func (r *ResourceDecimalRepository) GetBreakdown(ctx context.Context,id string)(ResourceBreakdownDecimal,error){var item ResourceBreakdownDecimal;e:=r.store.db.QueryRowContext(ctx,`SELECT id,resource_id,code,name,unit,quantity,unit_price,amount,quantity_scale,price_scale,amount_scale,row_version FROM resource_breakdowns_decimal WHERE id=?`,id).Scan(&item.ID,&item.ResourceID,&item.Code,&item.Name,&item.Unit,&item.Quantity,&item.UnitPrice,&item.Amount,&item.QuantityScale,&item.PriceScale,&item.AmountScale,&item.RowVersion);return item,e}
func (r *ResourceDecimalRepository) RecalculateResource(ctx context.Context,id string)(ResourceDecimal,error){item,e:=r.GetResource(ctx,id);if e!=nil{return item,e};rows,e:=r.store.db.QueryContext(ctx,`SELECT amount FROM resource_breakdowns_decimal WHERE resource_id=?`,id);if e!=nil{return item,e};defer rows.Close();vals:=[]string{};for rows.Next(){var v string;if e=rows.Scan(&v);e!=nil{return item,e};vals=append(vals,v)};total,e:=money.CalculateBudgetRollup(vals,item.PriceScale);if e!=nil{return item,e};_,e=r.store.db.ExecContext(ctx,`UPDATE resources_decimal SET unit_price=?,updated_at=strftime('%Y-%m-%dT%H:%M:%fZ','now'),row_version=row_version+1 WHERE id=?`,total,id);if e!=nil{return item,e};return r.GetResource(ctx,id)}
