package sqlite

import (
	"context"
	"database/sql"
	"encoding/json"
	"fmt"
	"strings"
	"time"

	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
	"github.com/shopspring/decimal"
)

type BudgetSnapshotItem struct {
	ID        string `json:"id"`
	Kind      string `json:"kind"`
	Quantity  string `json:"quantity"`
	UnitPrice string `json:"unit_price"`
	Amount    string `json:"amount"`
}

type ProjectCostStructureRun struct {
	ID                  string                `json:"id"`
	ProjectCode         string                `json:"project_code"`
	CostStructureTypeID string                `json:"cost_structure_type_id"`
	DirectCost          string                `json:"direct_cost"`
	Total               string                `json:"total"`
	Scale               int32                 `json:"scale"`
	BudgetSnapshot      []BudgetSnapshotItem  `json:"budget_snapshot"`
	Result              CostCalculationResult `json:"result"`
	CreatedBy           string                `json:"created_by"`
	CreatedAt           string                `json:"created_at"`
	RowVersion          int64                 `json:"row_version"`
	DeepLink            string                `json:"deep_link"`
}

type ProjectCostStructureRunRepository struct{ store *Store }

func NewProjectCostStructureRunRepository(store *Store) *ProjectCostStructureRunRepository {
	return &ProjectCostStructureRunRepository{store: store}
}

func aggregateBudget(items []BudgetSnapshotItem) (decimal.Decimal, error) {
	total := decimal.Zero
	for _, item := range items {
		kind := strings.ToUpper(strings.TrimSpace(item.Kind))
		if kind == "SECTION" || kind == "CHAPTER" || kind == "FOLDER" {
			continue
		}
		var amount decimal.Decimal
		var err error
		if strings.TrimSpace(item.Amount) != "" {
			amount, err = decimal.NewFromString(item.Amount)
		} else {
			qty, qtyErr := decimal.NewFromString(defaultString(item.Quantity, "0"))
			if qtyErr != nil {
				return decimal.Zero, qtyErr
			}
			price, priceErr := decimal.NewFromString(defaultString(item.UnitPrice, "0"))
			if priceErr != nil {
				return decimal.Zero, priceErr
			}
			amount = qty.Mul(price)
		}
		if err != nil {
			return decimal.Zero, err
		}
		total = total.Add(amount)
	}
	return total, nil
}

func (r *ProjectCostStructureRunRepository) Recalculate(ctx context.Context, projectCode string, items []BudgetSnapshotItem, scale int32, actor string) (ProjectCostStructureRun, error) {
	projectCode = strings.TrimSpace(projectCode)
	if projectCode == "" {
		return ProjectCostStructureRun{}, errx.New(errx.CodeInvalidArgument, "project_code is required", "P4-COST-006")
	}
	var typeID string
	if err := r.store.db.QueryRowContext(ctx, `SELECT cost_structure_type_id FROM project_cost_structures WHERE project_code=?`, projectCode).Scan(&typeID); err == sql.ErrNoRows {
		return ProjectCostStructureRun{}, errx.New(errx.CodeNotFound, "project cost structure not assigned", "P4-COST-006")
	} else if err != nil {
		return ProjectCostStructureRun{}, err
	}
	rows, err := r.store.db.QueryContext(ctx, `SELECT code,kind,rate,sequence FROM cost_structure_categories WHERE cost_structure_type_id=? AND enabled=1 ORDER BY sequence,code`, typeID)
	if err != nil {
		return ProjectCostStructureRun{}, err
	}
	defer rows.Close()
	lines := []CostCalculationLine{}
	for rows.Next() {
		var line CostCalculationLine
		if err := rows.Scan(&line.Code, &line.Kind, &line.Rate, &line.SortOrder); err != nil {
			return ProjectCostStructureRun{}, err
		}
		line.BaseKind = "SUBTOTAL"
		line.Sign = 1
		lines = append(lines, line)
	}
	if len(lines) == 0 {
		return ProjectCostStructureRun{}, errx.New(errx.CodeInvalidArgument, "assigned cost structure has no enabled categories", "P4-COST-006")
	}
	direct, err := aggregateBudget(items)
	if err != nil {
		return ProjectCostStructureRun{}, errx.Wrap(errx.CodeInvalidArgument, "invalid budget amount", "P4-COST-006", err)
	}
	result, err := CalculateCostStructure(lines, direct.String(), scale)
	if err != nil {
		return ProjectCostStructureRun{}, errx.Wrap(errx.CodeInvalidArgument, "calculate cost structure", "P4-COST-006", err)
	}
	budgetJSON, _ := json.Marshal(items)
	resultJSON, _ := json.Marshal(result)
	runID := fmt.Sprintf("CSR-%d", time.Now().UTC().UnixNano())
	now := time.Now().UTC().Format(time.RFC3339Nano)
	_, err = r.store.db.ExecContext(ctx, `INSERT INTO project_cost_structure_runs(id,project_code,cost_structure_type_id,direct_cost,total,scale,budget_snapshot_json,result_json,created_by,created_at,row_version) VALUES(?,?,?,?,?,?,?,?,?,?,1)`, runID, projectCode, typeID, result.DirectCost, result.Total, scale, string(budgetJSON), string(resultJSON), actor, now)
	if err != nil {
		return ProjectCostStructureRun{}, err
	}
	return r.Get(ctx, runID)
}

func (r *ProjectCostStructureRunRepository) Get(ctx context.Context, id string) (ProjectCostStructureRun, error) {
	var item ProjectCostStructureRun
	var budgetJSON, resultJSON string
	err := r.store.db.QueryRowContext(ctx, `SELECT id,project_code,cost_structure_type_id,direct_cost,total,scale,budget_snapshot_json,result_json,created_by,created_at,row_version FROM project_cost_structure_runs WHERE id=?`, id).Scan(&item.ID, &item.ProjectCode, &item.CostStructureTypeID, &item.DirectCost, &item.Total, &item.Scale, &budgetJSON, &resultJSON, &item.CreatedBy, &item.CreatedAt, &item.RowVersion)
	if err == sql.ErrNoRows {
		return item, errx.New(errx.CodeNotFound, "project cost structure run not found", "P4-COST-006")
	}
	if err != nil {
		return item, err
	}
	if err = json.Unmarshal([]byte(budgetJSON), &item.BudgetSnapshot); err != nil {
		return item, err
	}
	if err = json.Unmarshal([]byte(resultJSON), &item.Result); err != nil {
		return item, err
	}
	item.DeepLink = "/app/cost-structure?project=" + item.ProjectCode + "&run=" + item.ID
	return item, nil
}
