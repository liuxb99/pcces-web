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

type BudgetTraceRecord struct {
	ID          string                  `json:"id"`
	ProjectCode string                  `json:"project_code"`
	ItemID      *string                 `json:"item_id,omitempty"`
	Kind        string                  `json:"kind"`
	Input       money.BudgetKindInput   `json:"input"`
	Steps       []money.BudgetTraceStep `json:"steps"`
	Result      string                  `json:"result"`
	CreatedAt   string                  `json:"created_at"`
}

type BudgetTraceRepository struct{ store *Store }

func NewBudgetTraceRepository(store *Store) *BudgetTraceRepository {
	return &BudgetTraceRepository{store: store}
}

func (r *BudgetTraceRepository) Calculate(ctx context.Context, id, projectCode string, itemID *string, kind string, scale int, input money.BudgetKindInput) (BudgetTraceRecord, error) {
	if id == "" || projectCode == "" {
		return BudgetTraceRecord{}, errx.New(errx.CodeInvalidArgument, "trace id and project_code are required", "P2-G2")
	}
	trace, err := money.CalculateBudgetKind(kind, input, scale)
	if err != nil {
		return BudgetTraceRecord{}, errx.Wrap(errx.CodeInvalidArgument, "calculate budget item", "P2-G2", err)
	}
	inputJSON, _ := json.Marshal(input)
	stepsJSON, _ := json.Marshal(trace.Steps)
	created := time.Now().UTC().Format(time.RFC3339Nano)
	_, err = r.store.db.ExecContext(ctx, `INSERT INTO budget_calculation_traces(id,project_code,item_id,kind,input_json,steps_json,result,created_at) VALUES(?,?,?,?,?,?,?,?)`, id, projectCode, itemID, trace.Kind, string(inputJSON), string(stepsJSON), trace.Result, created)
	if err != nil {
		return BudgetTraceRecord{}, errx.Wrap(errx.CodeConflict, "persist budget calculation trace", "P2-G2", err)
	}
	return r.Get(ctx, id)
}

func (r *BudgetTraceRepository) Get(ctx context.Context, id string) (BudgetTraceRecord, error) {
	var rec BudgetTraceRecord
	var inputJSON, stepsJSON string
	err := r.store.db.QueryRowContext(ctx, `SELECT id,project_code,item_id,kind,input_json,steps_json,result,created_at FROM budget_calculation_traces WHERE id=?`, id).Scan(&rec.ID, &rec.ProjectCode, &rec.ItemID, &rec.Kind, &inputJSON, &stepsJSON, &rec.Result, &rec.CreatedAt)
	if err == sql.ErrNoRows {
		return rec, errx.New(errx.CodeNotFound, "budget calculation trace not found", "P2-G2")
	}
	if err != nil {
		return rec, errx.Wrap(errx.CodeDatabase, "get budget calculation trace", "P2-G2", err)
	}
	if err = json.Unmarshal([]byte(inputJSON), &rec.Input); err != nil {
		return rec, fmt.Errorf("decode trace input: %w", err)
	}
	if err = json.Unmarshal([]byte(stepsJSON), &rec.Steps); err != nil {
		return rec, fmt.Errorf("decode trace steps: %w", err)
	}
	return rec, nil
}

func (r *BudgetTraceRepository) ListProject(ctx context.Context, projectCode string) ([]BudgetTraceRecord, error) {
	rows, err := r.store.db.QueryContext(ctx, `SELECT id FROM budget_calculation_traces WHERE project_code=? ORDER BY created_at DESC`, projectCode)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	out := []BudgetTraceRecord{}
	for rows.Next() {
		var id string
		if err = rows.Scan(&id); err != nil {
			return nil, err
		}
		rec, e := r.Get(ctx, id)
		if e != nil {
			return nil, e
		}
		out = append(out, rec)
	}
	return out, rows.Err()
}
