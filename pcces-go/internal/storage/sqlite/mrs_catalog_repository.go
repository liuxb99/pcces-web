package sqlite

import (
	"context"
	"database/sql"
	"encoding/json"
	"time"

	"github.com/liuxb99/pcces-web/pcces-go/internal/domain/money"
	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

type MRSCatalogItem struct {
	ID, Code, Name, Category, CurrentPrice string
	Unit, Source                           *string
	PriceScale                             int
	Enabled                                bool
	RowVersion                             int64
	CreatedAt, UpdatedAt                   string
}
type MRSPriceHistory struct {
	ID, CatalogItemID, OldPrice, NewPrice, CreatedBy, CreatedAt string
	Source, EffectiveDate                                       *string
}
type MRSAnalysisComponent struct {
	ID, RecipeID, CatalogItemID, Quantity string
	QuantityScale, Sequence               int
}
type MRSAnalysisResult struct {
	ID, Code, Name string
	Unit           *string
	PriceScale     int
	RowVersion     int64
	Components     []map[string]any
	UnitPrice      string
}
type MRSCatalogRepository struct{ store *Store }

func NewMRSCatalogRepository(store *Store) *MRSCatalogRepository {
	return &MRSCatalogRepository{store: store}
}

func (r *MRSCatalogRepository) SaveItem(ctx context.Context, item MRSCatalogItem, actor, effectiveDate string) (MRSCatalogItem, error) {
	if item.ID == "" || item.Code == "" || item.Name == "" {
		return item, errx.New(errx.CodeInvalidArgument, "id, code and name are required", "P3-G-MRS")
	}
	price, err := money.Quantize(item.CurrentPrice, item.PriceScale)
	if err != nil {
		return item, err
	}
	now := time.Now().UTC().Format(time.RFC3339Nano)
	tx, err := r.store.db.BeginTx(ctx, nil)
	if err != nil {
		return item, err
	}
	defer tx.Rollback()
	var currentPrice string
	var currentVersion int64
	err = tx.QueryRowContext(ctx, `SELECT current_price,row_version FROM mrs_catalog_items WHERE id=?`, item.ID).Scan(&currentPrice, &currentVersion)
	if err == sql.ErrNoRows {
		_, err = tx.ExecContext(ctx, `INSERT INTO mrs_catalog_items(id,code,name,category,unit,current_price,price_scale,source,enabled,row_version,created_at,updated_at) VALUES(?,?,?,?,?,?,?,?,?,1,?,?)`, item.ID, item.Code, item.Name, item.Category, item.Unit, price, item.PriceScale, item.Source, item.Enabled, now, now)
		if err == nil {
			_, err = tx.ExecContext(ctx, `INSERT INTO mrs_price_history(id,catalog_item_id,old_price,new_price,source,effective_date,created_by,created_at) VALUES(?,?,?,?,?,?,?,?)`, item.ID+"-h1", item.ID, nil, price, item.Source, effectiveDate, actor, now)
		}
	} else if err == nil {
		if currentVersion != item.RowVersion {
			return item, errx.New(errx.CodeConflict, "MRS row version conflict", "P3-G-MRS")
		}
		res, e := tx.ExecContext(ctx, `UPDATE mrs_catalog_items SET code=?,name=?,category=?,unit=?,current_price=?,price_scale=?,source=?,enabled=?,row_version=row_version+1,updated_at=? WHERE id=? AND row_version=?`, item.Code, item.Name, item.Category, item.Unit, price, item.PriceScale, item.Source, item.Enabled, now, item.ID, item.RowVersion)
		err = e
		if err == nil {
			n, _ := res.RowsAffected()
			if n != 1 {
				err = errx.New(errx.CodeConflict, "MRS row version conflict", "P3-G-MRS")
			}
		}
		if err == nil && currentPrice != price {
			_, err = tx.ExecContext(ctx, `INSERT INTO mrs_price_history(id,catalog_item_id,old_price,new_price,source,effective_date,created_by,created_at) VALUES(?,?,?,?,?,?,?,?)`, item.ID+"-h"+now, item.ID, currentPrice, price, item.Source, effectiveDate, actor, now)
		}
	}
	if err != nil {
		return item, err
	}
	if err = tx.Commit(); err != nil {
		return item, err
	}
	return r.GetItem(ctx, item.ID)
}
func (r *MRSCatalogRepository) GetItem(ctx context.Context, id string) (MRSCatalogItem, error) {
	var v MRSCatalogItem
	err := r.store.db.QueryRowContext(ctx, `SELECT id,code,name,category,unit,current_price,price_scale,source,enabled,row_version,created_at,updated_at FROM mrs_catalog_items WHERE id=?`, id).Scan(&v.ID, &v.Code, &v.Name, &v.Category, &v.Unit, &v.CurrentPrice, &v.PriceScale, &v.Source, &v.Enabled, &v.RowVersion, &v.CreatedAt, &v.UpdatedAt)
	if err == sql.ErrNoRows {
		return v, errx.New(errx.CodeNotFound, "MRS item not found", "P3-G-MRS")
	}
	return v, err
}
func (r *MRSCatalogRepository) History(ctx context.Context, id string) ([]MRSPriceHistory, error) {
	rows, err := r.store.db.QueryContext(ctx, `SELECT id,catalog_item_id,old_price,new_price,source,effective_date,created_by,created_at FROM mrs_price_history WHERE catalog_item_id=? ORDER BY created_at DESC`, id)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	var out []MRSPriceHistory
	for rows.Next() {
		var v MRSPriceHistory
		if err = rows.Scan(&v.ID, &v.CatalogItemID, &v.OldPrice, &v.NewPrice, &v.Source, &v.EffectiveDate, &v.CreatedBy, &v.CreatedAt); err != nil {
			return nil, err
		}
		out = append(out, v)
	}
	return out, rows.Err()
}
func (r *MRSCatalogRepository) SetBookmark(ctx context.Context, actor, item string, enabled bool) error {
	if _, err := r.GetItem(ctx, item); err != nil {
		return err
	}
	if _, err := r.store.db.ExecContext(ctx, `DELETE FROM mrs_bookmarks WHERE actor_id=? AND catalog_item_id=?`, actor, item); err != nil {
		return err
	}
	if enabled {
		_, err := r.store.db.ExecContext(ctx, `INSERT INTO mrs_bookmarks(actor_id,catalog_item_id,created_at) VALUES(?,?,?)`, actor, item, time.Now().UTC().Format(time.RFC3339Nano))
		return err
	}
	return nil
}
func (r *MRSCatalogRepository) SaveRecipe(ctx context.Context, id, code, name string, unit *string, scale int, components []MRSAnalysisComponent, rowVersion int64) (MRSAnalysisResult, error) {
	tx, err := r.store.db.BeginTx(ctx, nil)
	if err != nil {
		return MRSAnalysisResult{}, err
	}
	defer tx.Rollback()
	now := time.Now().UTC().Format(time.RFC3339Nano)
	var current int64
	err = tx.QueryRowContext(ctx, `SELECT row_version FROM mrs_analysis_recipes WHERE id=?`, id).Scan(&current)
	if err == sql.ErrNoRows {
		_, err = tx.ExecContext(ctx, `INSERT INTO mrs_analysis_recipes(id,code,name,unit,price_scale,row_version,created_at,updated_at) VALUES(?,?,?,?,?,1,?,?)`, id, code, name, unit, scale, now, now)
	} else if err == nil {
		if current != rowVersion {
			return MRSAnalysisResult{}, errx.New(errx.CodeConflict, "recipe row version conflict", "P3-G-MRS")
		}
		_, err = tx.ExecContext(ctx, `UPDATE mrs_analysis_recipes SET code=?,name=?,unit=?,price_scale=?,row_version=row_version+1,updated_at=? WHERE id=?`, code, name, unit, scale, now, id)
		if err == nil {
			_, err = tx.ExecContext(ctx, `DELETE FROM mrs_analysis_components WHERE recipe_id=?`, id)
		}
	}
	if err != nil {
		return MRSAnalysisResult{}, err
	}
	for i, c := range components {
		q, e := money.Quantize(c.Quantity, c.QuantityScale)
		if e != nil {
			return MRSAnalysisResult{}, e
		}
		_, e = tx.ExecContext(ctx, `INSERT INTO mrs_analysis_components(id,recipe_id,catalog_item_id,quantity,quantity_scale,sequence) VALUES(?,?,?,?,?,?)`, c.ID, id, c.CatalogItemID, q, c.QuantityScale, i)
		if e != nil {
			return MRSAnalysisResult{}, e
		}
	}
	if err = tx.Commit(); err != nil {
		return MRSAnalysisResult{}, err
	}
	return r.CalculateRecipe(ctx, id)
}
func (r *MRSCatalogRepository) CalculateRecipe(ctx context.Context, id string) (MRSAnalysisResult, error) {
	var out MRSAnalysisResult
	err := r.store.db.QueryRowContext(ctx, `SELECT id,code,name,unit,price_scale,row_version FROM mrs_analysis_recipes WHERE id=?`, id).Scan(&out.ID, &out.Code, &out.Name, &out.Unit, &out.PriceScale, &out.RowVersion)
	if err != nil {
		return out, err
	}
	rows, err := r.store.db.QueryContext(ctx, `SELECT c.catalog_item_id,c.quantity,i.code,i.name,i.current_price FROM mrs_analysis_components c JOIN mrs_catalog_items i ON i.id=c.catalog_item_id WHERE c.recipe_id=? ORDER BY c.sequence`, id)
	if err != nil {
		return out, err
	}
	defer rows.Close()
	var amounts []string
	for rows.Next() {
		var itemID, q, code, name, price string
		if err = rows.Scan(&itemID, &q, &code, &name, &price); err != nil {
			return out, err
		}
		amount, e := money.Multiply(q, price, out.PriceScale)
		if e != nil {
			return out, e
		}
		amounts = append(amounts, amount)
		out.Components = append(out.Components, map[string]any{"catalog_item_id": itemID, "code": code, "name": name, "quantity": q, "unit_price": price, "amount": amount})
	}
	out.UnitPrice, err = money.Sum(amounts, out.PriceScale)
	return out, err
}
func (r *MRSCatalogRepository) RecordExport(ctx context.Context, id, format, actor string, count int) error {
	payload, _ := json.Marshal(map[string]int{"count": count})
	_, err := r.store.db.ExecContext(ctx, `INSERT INTO mrs_exchange_runs(id,operation,format,status,result_json,created_by,created_at) VALUES(?,?,?,?,?,?,?)`, id, "EXPORT", format, "COMPLETED", string(payload), actor, time.Now().UTC().Format(time.RFC3339Nano))
	return err
}
