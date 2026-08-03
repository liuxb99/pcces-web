package sqlite

import (
	"context"
	"database/sql"
	"encoding/json"
	"fmt"
	"sort"
	"time"

	"github.com/liuxb99/pcces-web/pcces-go/internal/domain/money"
	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

type MRSUsageItem struct {
	CatalogItemID   string   `json:"catalog_item_id"`
	Code            string   `json:"code"`
	Name            string   `json:"name"`
	Category        string   `json:"category"`
	RecipeIDs       []string `json:"recipe_ids"`
	UsageCount      int      `json:"usage_count"`
	TotalQuantity   string   `json:"total_quantity"`
	EstimatedAmount string   `json:"estimated_amount"`
}
type MRSUsageSummary struct {
	CatalogItems    int            `json:"catalog_items"`
	RecipeLinks     int            `json:"recipe_links"`
	EstimatedAmount string         `json:"estimated_amount"`
	Items           []MRSUsageItem `json:"items"`
}
type MRSRecipeVersion struct {
	ID        string         `json:"id"`
	RecipeID  string         `json:"recipe_id"`
	Label     string         `json:"label"`
	UnitPrice string         `json:"unit_price"`
	CreatedBy string         `json:"created_by"`
	CreatedAt string         `json:"created_at"`
	DeepLink  string         `json:"deep_link"`
	Snapshot  map[string]any `json:"snapshot"`
}
type MRSImportJob struct {
	ID              string           `json:"id"`
	Format          string           `json:"format"`
	Payload         string           `json:"payload"`
	Status          string           `json:"status"`
	CreatedBy       string           `json:"created_by"`
	CreatedAt       string           `json:"created_at"`
	UpdatedAt       string           `json:"updated_at"`
	DeepLink        string           `json:"deep_link"`
	Overwrite       bool             `json:"overwrite"`
	CancelRequested bool             `json:"cancel_requested"`
	TotalRows       int              `json:"total_rows"`
	ProcessedRows   int              `json:"processed_rows"`
	ImportedRows    int              `json:"imported_rows"`
	SkippedRows     int              `json:"skipped_rows"`
	Errors          []map[string]any `json:"errors"`
}
type MRSOperationsRepository struct{ store *Store }

func NewMRSOperationsRepository(store *Store) *MRSOperationsRepository {
	return &MRSOperationsRepository{store: store}
}

func (r *MRSOperationsRepository) UsageSummary(ctx context.Context) (MRSUsageSummary, error) {
	rows, err := r.store.db.QueryContext(ctx, `SELECT c.catalog_item_id,c.quantity,i.code,i.name,i.category,i.current_price,c.recipe_id FROM mrs_analysis_components c JOIN mrs_catalog_items i ON i.id=c.catalog_item_id ORDER BY i.code`)
	if err != nil {
		return MRSUsageSummary{}, err
	}
	defer rows.Close()
	group := map[string]*MRSUsageItem{}
	for rows.Next() {
		var id, q, code, name, category, price, recipe string
		if err = rows.Scan(&id, &q, &code, &name, &category, &price, &recipe); err != nil {
			return MRSUsageSummary{}, err
		}
		item := group[id]
		if item == nil {
			item = &MRSUsageItem{CatalogItemID: id, Code: code, Name: name, Category: category, TotalQuantity: "0.0000", EstimatedAmount: "0.00"}
			group[id] = item
		}
		item.RecipeIDs = append(item.RecipeIDs, recipe)
		item.UsageCount++
		item.TotalQuantity, err = money.Sum([]string{item.TotalQuantity, q}, 4)
		if err != nil {
			return MRSUsageSummary{}, err
		}
		amount, e := money.Multiply(q, price, 2)
		if e != nil {
			return MRSUsageSummary{}, e
		}
		item.EstimatedAmount, err = money.Sum([]string{item.EstimatedAmount, amount}, 2)
		if err != nil {
			return MRSUsageSummary{}, err
		}
	}
	out := MRSUsageSummary{EstimatedAmount: "0.00"}
	keys := make([]string, 0, len(group))
	for k := range group {
		keys = append(keys, k)
	}
	sort.Strings(keys)
	for _, k := range keys {
		item := *group[k]
		out.Items = append(out.Items, item)
		out.CatalogItems++
		out.RecipeLinks += item.UsageCount
		out.EstimatedAmount, err = money.Sum([]string{out.EstimatedAmount, item.EstimatedAmount}, 2)
		if err != nil {
			return MRSUsageSummary{}, err
		}
	}
	return out, rows.Err()
}
func (r *MRSOperationsRepository) CreateRecipeVersion(ctx context.Context, id, recipe, label, actor string) (MRSRecipeVersion, error) {
	calc, err := NewMRSCatalogRepository(r.store).CalculateRecipe(ctx, recipe)
	if err != nil {
		return MRSRecipeVersion{}, err
	}
	payload, err := json.Marshal(calc)
	if err != nil {
		return MRSRecipeVersion{}, err
	}
	now := time.Now().UTC().Format(time.RFC3339Nano)
	if label == "" {
		label = "MRS recipe version"
	}
	_, err = r.store.db.ExecContext(ctx, `INSERT INTO mrs_recipe_versions(id,recipe_id,label,unit_price,snapshot_json,created_by,created_at) VALUES(?,?,?,?,?,?,?)`, id, recipe, label, calc.UnitPrice, string(payload), actor, now)
	if err != nil {
		return MRSRecipeVersion{}, err
	}
	return r.GetRecipeVersion(ctx, id)
}
func (r *MRSOperationsRepository) GetRecipeVersion(ctx context.Context, id string) (MRSRecipeVersion, error) {
	var v MRSRecipeVersion
	var payload string
	err := r.store.db.QueryRowContext(ctx, `SELECT id,recipe_id,label,unit_price,snapshot_json,created_by,created_at FROM mrs_recipe_versions WHERE id=?`, id).Scan(&v.ID, &v.RecipeID, &v.Label, &v.UnitPrice, &payload, &v.CreatedBy, &v.CreatedAt)
	if err == sql.ErrNoRows {
		return v, errx.New(errx.CodeNotFound, "MRS recipe version not found", "P3-G-MRS")
	}
	if err != nil {
		return v, err
	}
	if err = json.Unmarshal([]byte(payload), &v.Snapshot); err != nil {
		return v, err
	}
	v.DeepLink = fmt.Sprintf("/app/mrs-operations?recipe=%s&version=%s", v.RecipeID, v.ID)
	return v, nil
}
func (r *MRSOperationsRepository) ListRecipeVersions(ctx context.Context, recipe string) ([]MRSRecipeVersion, error) {
	rows, err := r.store.db.QueryContext(ctx, `SELECT id FROM mrs_recipe_versions WHERE recipe_id=? ORDER BY created_at DESC`, recipe)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	var out []MRSRecipeVersion
	for rows.Next() {
		var id string
		if err = rows.Scan(&id); err != nil {
			return nil, err
		}
		v, e := r.GetRecipeVersion(ctx, id)
		if e != nil {
			return nil, e
		}
		out = append(out, v)
	}
	return out, rows.Err()
}
func (r *MRSOperationsRepository) PriceLineage(ctx context.Context, item string) (map[string]any, error) {
	catalog, err := NewMRSCatalogRepository(r.store).GetItem(ctx, item)
	if err != nil {
		return nil, err
	}
	events := []map[string]any{}
	h, err := r.store.db.QueryContext(ctx, `SELECT id,old_price,new_price,source,effective_date,created_at FROM mrs_price_history WHERE catalog_item_id=? ORDER BY created_at`, item)
	if err != nil {
		return nil, err
	}
	for h.Next() {
		var id, newPrice, created string
		var oldPrice, source, effective sql.NullString
		if err = h.Scan(&id, &oldPrice, &newPrice, &source, &effective, &created); err != nil {
			h.Close()
			return nil, err
		}
		events = append(events, map[string]any{"type": "PRICE_HISTORY", "id": id, "old_price": mrsNullable(oldPrice), "new_price": newPrice, "source": mrsNullable(source), "effective_date": mrsNullable(effective), "created_at": created})
	}
	h.Close()
	q, err := r.store.db.QueryContext(ctx, `SELECT id,vendor,quoted_price,source_document,effective_date,created_at FROM mrs_price_quotes WHERE catalog_item_id=? ORDER BY created_at`, item)
	if err != nil {
		return nil, err
	}
	for q.Next() {
		var id, vendor, price, created string
		var source, effective sql.NullString
		if err = q.Scan(&id, &vendor, &price, &source, &effective, &created); err != nil {
			q.Close()
			return nil, err
		}
		events = append(events, map[string]any{"type": "SUPPLIER_QUOTE", "id": id, "vendor": vendor, "price": price, "source_document": mrsNullable(source), "effective_date": mrsNullable(effective), "created_at": created})
	}
	q.Close()
	sort.Slice(events, func(i, j int) bool { return fmt.Sprint(events[i]["created_at"]) < fmt.Sprint(events[j]["created_at"]) })
	return map[string]any{"catalog_item": catalog, "events": events, "deep_link": fmt.Sprintf("/app/mrs-operations?item=%s&lineage=1", item)}, nil
}
func (r *MRSOperationsRepository) CreateImportJob(ctx context.Context, id, format, payload, actor string, overwrite bool, total int) (MRSImportJob, error) {
	now := time.Now().UTC().Format(time.RFC3339Nano)
	_, err := r.store.db.ExecContext(ctx, `INSERT INTO mrs_import_jobs(id,format,payload,overwrite,status,total_rows,processed_rows,imported_rows,skipped_rows,errors_json,cancel_requested,created_by,created_at,updated_at) VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?)`, id, format, payload, overwrite, "PENDING", total, 0, 0, 0, "[]", 0, actor, now, now)
	if err != nil {
		return MRSImportJob{}, err
	}
	return r.GetImportJob(ctx, id)
}
func (r *MRSOperationsRepository) CancelImportJob(ctx context.Context, id string) (MRSImportJob, error) {
	job, err := r.GetImportJob(ctx, id)
	if err != nil {
		return job, err
	}
	if job.Status != "PENDING" && job.Status != "RUNNING" {
		return job, errx.New(errx.CodeConflict, "import job is terminal", "P3-G-MRS")
	}
	_, err = r.store.db.ExecContext(ctx, `UPDATE mrs_import_jobs SET cancel_requested=1,status='CANCELLED',updated_at=? WHERE id=?`, time.Now().UTC().Format(time.RFC3339Nano), id)
	if err != nil {
		return job, err
	}
	return r.GetImportJob(ctx, id)
}
func (r *MRSOperationsRepository) GetImportJob(ctx context.Context, id string) (MRSImportJob, error) {
	var v MRSImportJob
	var errors string
	err := r.store.db.QueryRowContext(ctx, `SELECT id,format,payload,overwrite,status,total_rows,processed_rows,imported_rows,skipped_rows,errors_json,cancel_requested,created_by,created_at,updated_at FROM mrs_import_jobs WHERE id=?`, id).Scan(&v.ID, &v.Format, &v.Payload, &v.Overwrite, &v.Status, &v.TotalRows, &v.ProcessedRows, &v.ImportedRows, &v.SkippedRows, &errors, &v.CancelRequested, &v.CreatedBy, &v.CreatedAt, &v.UpdatedAt)
	if err == sql.ErrNoRows {
		return v, errx.New(errx.CodeNotFound, "MRS import job not found", "P3-G-MRS")
	}
	if err != nil {
		return v, err
	}
	if err = json.Unmarshal([]byte(errors), &v.Errors); err != nil {
		return v, err
	}
	v.DeepLink = fmt.Sprintf("/app/mrs-operations?job=%s", id)
	return v, nil
}
func mrsNullable(v sql.NullString) any {
	if v.Valid {
		return v.String
	}
	return nil
}
