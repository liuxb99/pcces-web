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

var allowedCostKinds = map[string]bool{
	"DIRECT": true, "INDIRECT": true, "MANAGEMENT": true,
	"TAX": true, "PERCENT": true, "ADJUSTMENT": true,
}

type CostStructureCategory struct {
	ID                  string `json:"id"`
	CostStructureTypeID string `json:"cost_structure_type_id"`
	Code                string `json:"code"`
	Name                string `json:"name"`
	Kind                string `json:"kind"`
	Sequence            int    `json:"sequence"`
	Rate                string `json:"rate"`
	Enabled             bool   `json:"enabled"`
	RowVersion          int64  `json:"row_version"`
}

type CostStructureImportRequest struct {
	OnlyStructure bool                    `json:"only_structure"`
	ActorID       string                  `json:"actor_id"`
	Categories    []CostStructureCategory `json:"categories"`
}

type CostStructureImportResult struct {
	ID                  string `json:"id"`
	Status              string `json:"status"`
	CostStructureTypeID string `json:"cost_structure_type_id"`
	OnlyStructure       bool   `json:"only_structure"`
	TotalRows           int    `json:"total_rows"`
	ImportedRows        int    `json:"imported_rows"`
}

type BudgetItemCostProperty struct {
	ProjectCode    string `json:"project_code"`
	BudgetItemID   string `json:"budget_item_id"`
	CostCategoryID string `json:"cost_category_id"`
	CostKind       string `json:"cost_kind"`
	Sign           int    `json:"sign"`
	Rate           string `json:"rate"`
	UpdatedBy      string `json:"updated_by"`
	UpdatedAt      string `json:"updated_at"`
	RowVersion     int64  `json:"row_version"`
	CategoryCode   string `json:"category_code"`
	CategoryName   string `json:"category_name"`
	DeepLink       string `json:"deep_link"`
}

type CostStructureDetailRepository struct{ store *Store }

func NewCostStructureDetailRepository(store *Store) *CostStructureDetailRepository {
	return &CostStructureDetailRepository{store: store}
}

func normalizeCategory(typeID string, item CostStructureCategory, index int) (CostStructureCategory, error) {
	item.Code = strings.ToUpper(strings.TrimSpace(item.Code))
	item.Name = strings.TrimSpace(item.Name)
	item.Kind = strings.ToUpper(strings.TrimSpace(item.Kind))
	if item.Kind == "" {
		item.Kind = "DIRECT"
	}
	if item.Code == "" || item.Name == "" {
		return item, errx.New(errx.CodeInvalidArgument, "category code and name are required", "P4-COST-002")
	}
	if !allowedCostKinds[item.Kind] {
		return item, errx.New(errx.CodeInvalidArgument, "unsupported cost kind", "P4-COST-002")
	}
	if item.ID == "" {
		item.ID = typeID + ":" + item.Code
	}
	if item.Sequence == 0 {
		item.Sequence = index + 1
	}
	if item.Rate == "" {
		item.Rate = "0"
	}
	item.CostStructureTypeID = typeID
	item.RowVersion = 1
	return item, nil
}

func (r *CostStructureDetailRepository) ImportDefinition(ctx context.Context, typeID string, req CostStructureImportRequest) (CostStructureImportResult, error) {
	typeID = strings.TrimSpace(typeID)
	if typeID == "" || len(req.Categories) == 0 {
		return CostStructureImportResult{}, errx.New(errx.CodeInvalidArgument, "type id and categories are required", "P4-COST-002")
	}
	seen := map[string]bool{}
	normalized := make([]CostStructureCategory, 0, len(req.Categories))
	for i, row := range req.Categories {
		item, err := normalizeCategory(typeID, row, i)
		if err != nil {
			return CostStructureImportResult{}, err
		}
		if seen[item.Code] {
			return CostStructureImportResult{}, errx.New(errx.CodeInvalidArgument, "duplicate category code", "P4-COST-002")
		}
		seen[item.Code] = true
		normalized = append(normalized, item)
	}

	tx, err := r.store.db.BeginTx(ctx, nil)
	if err != nil {
		return CostStructureImportResult{}, err
	}
	defer tx.Rollback()
	var exists int
	if err = tx.QueryRowContext(ctx, `SELECT 1 FROM cost_structure_types WHERE id=?`, typeID).Scan(&exists); err == sql.ErrNoRows {
		return CostStructureImportResult{}, errx.New(errx.CodeNotFound, "cost structure type not found", "P4-COST-002")
	} else if err != nil {
		return CostStructureImportResult{}, err
	}
	if _, err = tx.ExecContext(ctx, `DELETE FROM cost_structure_categories WHERE cost_structure_type_id=?`, typeID); err != nil {
		return CostStructureImportResult{}, err
	}
	for _, item := range normalized {
		if _, err = tx.ExecContext(ctx, `INSERT INTO cost_structure_categories(id,cost_structure_type_id,code,name,kind,sequence,rate,enabled,row_version) VALUES(?,?,?,?,?,?,?,?,1)`,
			item.ID, typeID, item.Code, item.Name, item.Kind, item.Sequence, item.Rate, boolInt(item.Enabled)); err != nil {
			return CostStructureImportResult{}, err
		}
	}
	now := time.Now().UTC().Format(time.RFC3339Nano)
	runID := fmt.Sprintf("CSI-%d", time.Now().UTC().UnixNano())
	errorsJSON, _ := json.Marshal([]any{})
	if _, err = tx.ExecContext(ctx, `INSERT INTO cost_structure_import_runs(id,cost_structure_type_id,only_structure,status,total_rows,imported_rows,errors_json,created_by,created_at) VALUES(?,?,?,?,?,?,?,?,?)`,
		runID, typeID, boolInt(req.OnlyStructure), "COMPLETED", len(normalized), len(normalized), string(errorsJSON), req.ActorID, now); err != nil {
		return CostStructureImportResult{}, err
	}
	if err = tx.Commit(); err != nil {
		return CostStructureImportResult{}, err
	}
	return CostStructureImportResult{ID: runID, Status: "COMPLETED", CostStructureTypeID: typeID, OnlyStructure: req.OnlyStructure, TotalRows: len(normalized), ImportedRows: len(normalized)}, nil
}

func (r *CostStructureDetailRepository) ListCategories(ctx context.Context, typeID string) ([]CostStructureCategory, error) {
	rows, err := r.store.db.QueryContext(ctx, `SELECT id,cost_structure_type_id,code,name,kind,sequence,rate,enabled,row_version FROM cost_structure_categories WHERE cost_structure_type_id=? ORDER BY sequence,code`, typeID)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	items := []CostStructureCategory{}
	for rows.Next() {
		var item CostStructureCategory
		var enabled int
		if err := rows.Scan(&item.ID, &item.CostStructureTypeID, &item.Code, &item.Name, &item.Kind, &item.Sequence, &item.Rate, &enabled, &item.RowVersion); err != nil {
			return nil, err
		}
		item.Enabled = enabled != 0
		items = append(items, item)
	}
	return items, rows.Err()
}

func (r *CostStructureDetailRepository) SaveItemProperty(ctx context.Context, item BudgetItemCostProperty) (BudgetItemCostProperty, error) {
	item.ProjectCode = strings.TrimSpace(item.ProjectCode)
	item.BudgetItemID = strings.TrimSpace(item.BudgetItemID)
	item.CostCategoryID = strings.TrimSpace(item.CostCategoryID)
	item.CostKind = strings.ToUpper(strings.TrimSpace(item.CostKind))
	if item.ProjectCode == "" || item.BudgetItemID == "" || item.CostCategoryID == "" {
		return item, errx.New(errx.CodeInvalidArgument, "project, item and category are required", "P4-COST-004")
	}
	if !allowedCostKinds[item.CostKind] {
		return item, errx.New(errx.CodeInvalidArgument, "unsupported cost kind", "P4-COST-004")
	}
	if item.Sign != -1 && item.Sign != 1 {
		return item, errx.New(errx.CodeInvalidArgument, "sign must be -1 or 1", "P4-COST-004")
	}
	if item.Rate == "" {
		item.Rate = "0"
	}
	tx, err := r.store.db.BeginTx(ctx, nil)
	if err != nil {
		return item, err
	}
	defer tx.Rollback()
	var enabled int
	if err = tx.QueryRowContext(ctx, `SELECT enabled FROM cost_structure_categories WHERE id=?`, item.CostCategoryID).Scan(&enabled); err == sql.ErrNoRows || enabled == 0 {
		return item, errx.New(errx.CodeNotFound, "enabled cost category not found", "P4-COST-004")
	} else if err != nil {
		return item, err
	}
	var current int64
	err = tx.QueryRowContext(ctx, `SELECT row_version FROM budget_item_cost_properties WHERE project_code=? AND budget_item_id=?`, item.ProjectCode, item.BudgetItemID).Scan(&current)
	now := time.Now().UTC().Format(time.RFC3339Nano)
	if err == sql.ErrNoRows {
		if item.RowVersion != 0 {
			return item, errx.New(errx.CodeConflict, "stale row_version", "P4-COST-004")
		}
		_, err = tx.ExecContext(ctx, `INSERT INTO budget_item_cost_properties(project_code,budget_item_id,cost_category_id,cost_kind,sign,rate,updated_by,updated_at,row_version) VALUES(?,?,?,?,?,?,?,?,1)`, item.ProjectCode, item.BudgetItemID, item.CostCategoryID, item.CostKind, item.Sign, item.Rate, item.UpdatedBy, now)
	} else if err != nil {
		return item, err
	} else {
		if current != item.RowVersion {
			return item, errx.New(errx.CodeConflict, "stale row_version", "P4-COST-004")
		}
		res, execErr := tx.ExecContext(ctx, `UPDATE budget_item_cost_properties SET cost_category_id=?,cost_kind=?,sign=?,rate=?,updated_by=?,updated_at=?,row_version=row_version+1 WHERE project_code=? AND budget_item_id=? AND row_version=?`, item.CostCategoryID, item.CostKind, item.Sign, item.Rate, item.UpdatedBy, now, item.ProjectCode, item.BudgetItemID, item.RowVersion)
		if execErr != nil {
			return item, execErr
		}
		n, _ := res.RowsAffected()
		if n != 1 {
			return item, errx.New(errx.CodeConflict, "stale row_version", "P4-COST-004")
		}
	}
	if err != nil {
		return item, err
	}
	if err = tx.Commit(); err != nil {
		return item, err
	}
	return r.GetItemProperty(ctx, item.ProjectCode, item.BudgetItemID)
}

func (r *CostStructureDetailRepository) GetItemProperty(ctx context.Context, projectCode, itemID string) (BudgetItemCostProperty, error) {
	var item BudgetItemCostProperty
	err := r.store.db.QueryRowContext(ctx, `SELECT p.project_code,p.budget_item_id,p.cost_category_id,p.cost_kind,p.sign,p.rate,p.updated_by,p.updated_at,p.row_version,c.code,c.name FROM budget_item_cost_properties p JOIN cost_structure_categories c ON c.id=p.cost_category_id WHERE p.project_code=? AND p.budget_item_id=?`, projectCode, itemID).Scan(&item.ProjectCode, &item.BudgetItemID, &item.CostCategoryID, &item.CostKind, &item.Sign, &item.Rate, &item.UpdatedBy, &item.UpdatedAt, &item.RowVersion, &item.CategoryCode, &item.CategoryName)
	if err == sql.ErrNoRows {
		return item, errx.New(errx.CodeNotFound, "budget item cost property not found", "P4-COST-004")
	}
	if err != nil {
		return item, err
	}
	item.DeepLink = "/app/budget/" + projectCode + "?item=" + itemID + "&panel=cost-property"
	return item, nil
}
