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

type CostStructureRunVersion struct {
	ID              string         `json:"id"`
	ProjectCode     string         `json:"project_code"`
	RunID           string         `json:"run_id"`
	BudgetVersionID string         `json:"budget_version_id"`
	BudgetStatus    string         `json:"budget_status"`
	DirectCost      string         `json:"direct_cost"`
	TotalCost       string         `json:"total_cost"`
	Trace           map[string]any `json:"trace"`
	CreatedBy       string         `json:"created_by"`
	CreatedAt       string         `json:"created_at"`
	RowVersion      int64          `json:"row_version"`
	DeepLink        string         `json:"deep_link"`
}

type CostStructureRunDiff struct {
	ProjectCode     string `json:"project_code"`
	Left            string `json:"left"`
	Right           string `json:"right"`
	DirectCostDelta string `json:"direct_cost_delta"`
	TotalCostDelta  string `json:"total_cost_delta"`
}

type CostStructureRunVersionRepository struct{ store *Store }

func NewCostStructureRunVersionRepository(store *Store) *CostStructureRunVersionRepository {
	return &CostStructureRunVersionRepository{store: store}
}

func (r *CostStructureRunVersionRepository) Link(ctx context.Context, item CostStructureRunVersion) (CostStructureRunVersion, error) {
	item.ProjectCode = strings.TrimSpace(item.ProjectCode)
	item.RunID = strings.TrimSpace(item.RunID)
	item.BudgetVersionID = strings.TrimSpace(item.BudgetVersionID)
	item.BudgetStatus = strings.ToUpper(strings.TrimSpace(item.BudgetStatus))
	if item.ProjectCode == "" || item.RunID == "" || item.BudgetVersionID == "" {
		return item, errx.New(errx.CodeInvalidArgument, "project_code, run_id and budget_version_id are required", "P4-COST-006")
	}
	if item.BudgetStatus == "APPROVED" || item.BudgetStatus == "FROZEN" || item.BudgetStatus == "ARCHIVED" {
		return item, errx.New(errx.CodeConflict, "approved or frozen budget version is read-only", "P4-COST-006")
	}
	if _, err := decimal.NewFromString(item.DirectCost); err != nil {
		return item, errx.New(errx.CodeInvalidArgument, "invalid direct_cost", "P4-COST-006")
	}
	if _, err := decimal.NewFromString(item.TotalCost); err != nil {
		return item, errx.New(errx.CodeInvalidArgument, "invalid total_cost", "P4-COST-006")
	}
	trace, err := json.Marshal(item.Trace)
	if err != nil {
		return item, err
	}
	if item.ID == "" {
		item.ID = fmt.Sprintf("CSRV-%d", time.Now().UTC().UnixNano())
	}
	item.CreatedAt = time.Now().UTC().Format(time.RFC3339Nano)
	_, err = r.store.db.ExecContext(ctx, `INSERT INTO cost_structure_run_versions(id,project_code,run_id,budget_version_id,budget_status,direct_cost,total_cost,trace_json,created_by,created_at,row_version) VALUES(?,?,?,?,?,?,?,?,?,?,1)`, item.ID, item.ProjectCode, item.RunID, item.BudgetVersionID, item.BudgetStatus, item.DirectCost, item.TotalCost, string(trace), item.CreatedBy, item.CreatedAt)
	if err != nil {
		return item, errx.New(errx.CodeConflict, "run already linked", "P4-COST-006")
	}
	return r.Get(ctx, item.RunID)
}

func (r *CostStructureRunVersionRepository) Get(ctx context.Context, runID string) (CostStructureRunVersion, error) {
	var item CostStructureRunVersion
	var trace string
	err := r.store.db.QueryRowContext(ctx, `SELECT id,project_code,run_id,budget_version_id,budget_status,direct_cost,total_cost,trace_json,created_by,created_at,row_version FROM cost_structure_run_versions WHERE run_id=?`, runID).Scan(&item.ID, &item.ProjectCode, &item.RunID, &item.BudgetVersionID, &item.BudgetStatus, &item.DirectCost, &item.TotalCost, &trace, &item.CreatedBy, &item.CreatedAt, &item.RowVersion)
	if err == sql.ErrNoRows {
		return item, errx.New(errx.CodeNotFound, "cost structure run version not found", "P4-COST-006")
	}
	if err != nil {
		return item, err
	}
	_ = json.Unmarshal([]byte(trace), &item.Trace)
	item.DeepLink = "/app/cost-structure?project=" + item.ProjectCode + "&run=" + item.RunID
	return item, nil
}

func (r *CostStructureRunVersionRepository) Compare(ctx context.Context, leftID, rightID string) (CostStructureRunDiff, error) {
	left, err := r.Get(ctx, leftID)
	if err != nil {
		return CostStructureRunDiff{}, err
	}
	right, err := r.Get(ctx, rightID)
	if err != nil {
		return CostStructureRunDiff{}, err
	}
	if left.ProjectCode != right.ProjectCode {
		return CostStructureRunDiff{}, errx.New(errx.CodeInvalidArgument, "runs must belong to the same project", "P4-COST-006")
	}
	ld, _ := decimal.NewFromString(left.DirectCost)
	rd, _ := decimal.NewFromString(right.DirectCost)
	lt, _ := decimal.NewFromString(left.TotalCost)
	rt, _ := decimal.NewFromString(right.TotalCost)
	return CostStructureRunDiff{ProjectCode: left.ProjectCode, Left: leftID, Right: rightID, DirectCostDelta: rd.Sub(ld).String(), TotalCostDelta: rt.Sub(lt).String()}, nil
}
