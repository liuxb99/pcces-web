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

type ResourcePriceLineage struct {
	ID, ProjectCode, ResourceID, BudgetItemID string
	OldUnitPrice, NewUnitPrice                string
	OldAmount, NewAmount                      string
	Trigger, TraceJSON, CreatedAt             string
}

type ResourceBudgetLineageRepository struct{ store *Store }

func NewResourceBudgetLineageRepository(store *Store) *ResourceBudgetLineageRepository {
	return &ResourceBudgetLineageRepository{store: store}
}

func (r *ResourceBudgetLineageRepository) Link(ctx context.Context, projectCode, resourceID, budgetItemID string) error {
	if projectCode == "" || resourceID == "" || budgetItemID == "" {
		return errx.New(errx.CodeInvalidArgument, "project_code, resource_id and budget_item_id are required", "P3-G2")
	}
	var count int
	if err := r.store.db.QueryRowContext(ctx, `SELECT COUNT(*) FROM resources_decimal WHERE id=?`, resourceID).Scan(&count); err != nil || count != 1 {
		return errx.New(errx.CodeNotFound, "resource not found", "P3-G2")
	}
	if err := r.store.db.QueryRowContext(ctx, `SELECT COUNT(*) FROM budget_items_decimal WHERE id=? AND project_code=?`, budgetItemID, projectCode).Scan(&count); err != nil || count != 1 {
		return errx.New(errx.CodeNotFound, "budget item not found", "P3-G2")
	}
	id := projectCode + ":" + resourceID + ":" + budgetItemID
	_, err := r.store.db.ExecContext(ctx, `INSERT OR IGNORE INTO resource_budget_links(id,project_code,resource_id,budget_item_id,created_at) VALUES(?,?,?,?,?)`, id, projectCode, resourceID, budgetItemID, time.Now().UTC().Format(time.RFC3339Nano))
	return err
}

func (r *ResourceBudgetLineageRepository) Propagate(ctx context.Context, resourceID, trigger string) ([]ResourcePriceLineage, error) {
	if trigger == "" {
		trigger = "RESOURCE_PRICE_CHANGED"
	}
	tx, err := r.store.db.BeginTx(ctx, nil)
	if err != nil {
		return nil, err
	}
	defer func() { _ = tx.Rollback() }()

	var resourcePrice string
	if err = tx.QueryRowContext(ctx, `SELECT unit_price FROM resources_decimal WHERE id=?`, resourceID).Scan(&resourcePrice); err != nil {
		if err == sql.ErrNoRows {
			return nil, errx.New(errx.CodeNotFound, "resource not found", "P3-G2")
		}
		return nil, err
	}
	rows, err := tx.QueryContext(ctx, `SELECT project_code,budget_item_id FROM resource_budget_links WHERE resource_id=?`, resourceID)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	type link struct{ project, item string }
	var links []link
	for rows.Next() {
		var v link
		if err = rows.Scan(&v.project, &v.item); err != nil {
			return nil, err
		}
		links = append(links, v)
	}

	result := make([]ResourcePriceLineage, 0, len(links))
	for index, link := range links {
		var quantity, oldPrice, oldAmount string
		var priceScale, amountScale int
		var rowVersion int64
		err = tx.QueryRowContext(ctx, `SELECT quantity,unit_price,amount,price_scale,amount_scale,row_version FROM budget_items_decimal WHERE id=?`, link.item).Scan(&quantity, &oldPrice, &oldAmount, &priceScale, &amountScale, &rowVersion)
		if err == sql.ErrNoRows {
			continue
		}
		if err != nil {
			return nil, err
		}
		newPrice, err := money.Quantize(resourcePrice, priceScale)
		if err != nil {
			return nil, err
		}
		newAmount, err := money.CalculateBudgetLeaf(quantity, newPrice, amountScale)
		if err != nil {
			return nil, err
		}
		if _, err = tx.ExecContext(ctx, `UPDATE budget_items_decimal SET unit_price=?,amount=?,updated_at=?,row_version=row_version+1 WHERE id=? AND row_version=?`, newPrice, newAmount, time.Now().UTC().Format(time.RFC3339Nano), link.item, rowVersion); err != nil {
			return nil, err
		}
		trace, _ := json.Marshal(map[string]string{"operation": "RESOURCE_PRICE_PROPAGATION", "quantity": quantity, "resource_unit_price": newPrice, "result": newAmount})
		now := time.Now().UTC().Format(time.RFC3339Nano)
		lineage := ResourcePriceLineage{ID: fmt.Sprintf("%d-%d", time.Now().UnixNano(), index), ProjectCode: link.project, ResourceID: resourceID, BudgetItemID: link.item, OldUnitPrice: oldPrice, NewUnitPrice: newPrice, OldAmount: oldAmount, NewAmount: newAmount, Trigger: trigger, TraceJSON: string(trace), CreatedAt: now}
		_, err = tx.ExecContext(ctx, `INSERT INTO resource_price_lineage(id,project_code,resource_id,budget_item_id,old_unit_price,new_unit_price,old_amount,new_amount,trigger,trace_json,created_at) VALUES(?,?,?,?,?,?,?,?,?,?,?)`, lineage.ID, lineage.ProjectCode, lineage.ResourceID, lineage.BudgetItemID, lineage.OldUnitPrice, lineage.NewUnitPrice, lineage.OldAmount, lineage.NewAmount, lineage.Trigger, lineage.TraceJSON, lineage.CreatedAt)
		if err != nil {
			return nil, err
		}
		result = append(result, lineage)
	}
	if err = tx.Commit(); err != nil {
		return nil, err
	}
	return result, nil
}

func (r *ResourceBudgetLineageRepository) ListProject(ctx context.Context, projectCode string) ([]ResourcePriceLineage, error) {
	rows, err := r.store.db.QueryContext(ctx, `SELECT id,project_code,resource_id,budget_item_id,old_unit_price,new_unit_price,old_amount,new_amount,trigger,trace_json,created_at FROM resource_price_lineage WHERE project_code=? ORDER BY created_at DESC`, projectCode)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	var result []ResourcePriceLineage
	for rows.Next() {
		var v ResourcePriceLineage
		if err = rows.Scan(&v.ID, &v.ProjectCode, &v.ResourceID, &v.BudgetItemID, &v.OldUnitPrice, &v.NewUnitPrice, &v.OldAmount, &v.NewAmount, &v.Trigger, &v.TraceJSON, &v.CreatedAt); err != nil {
			return nil, err
		}
		result = append(result, v)
	}
	return result, rows.Err()
}
