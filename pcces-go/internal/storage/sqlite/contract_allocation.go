package sqlite

import (
	"context"
	"database/sql"
	"encoding/json"
	"fmt"
	"strconv"
	"strings"
	"time"

	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

type ContractAllocationItem struct {
	SourceBudgetItemID string `json:"source_budget_item_id"`
	ItemNo             string `json:"item_no"`
	Name               string `json:"name"`
	Unit               string `json:"unit"`
	Quantity           string `json:"quantity"`
	UnitPrice          string `json:"unit_price"`
	Amount             string `json:"amount"`
}

type ContractAllocationRepository struct{ store *Store }

func NewContractAllocationRepository(store *Store) *ContractAllocationRepository {
	return &ContractAllocationRepository{store: store}
}

func parseFloat(value string) (float64, error) {
	v, err := strconv.ParseFloat(strings.TrimSpace(value), 64)
	if err != nil || v < 0 {
		return 0, errx.New(errx.CodeInvalidArgument, "invalid non-negative decimal", "P5-G-ALLOC")
	}
	return v, nil
}

func (r *ContractAllocationRepository) contract(ctx context.Context, id string) (project, version, status string, rowVersion int64, err error) {
	err = r.store.db.QueryRowContext(ctx, `SELECT project_code,budget_version_id,status,row_version FROM contracts_v2 WHERE id=?`, id).Scan(&project, &version, &status, &rowVersion)
	if err == sql.ErrNoRows {
		err = errx.New(errx.CodeNotFound, "contract not found", "P5-G-ALLOC")
	}
	return
}

func (r *ContractAllocationRepository) Basis(ctx context.Context, contractID string) (map[string]any, error) {
	_, version, _, _, err := r.contract(ctx, contractID)
	if err != nil {
		return nil, err
	}
	var snapshotJSON string
	if err = r.store.db.QueryRowContext(ctx, `SELECT snapshot_json FROM budget_versions WHERE id=?`, version).Scan(&snapshotJSON); err != nil {
		return nil, err
	}
	var snapshot []map[string]any
	if err = json.Unmarshal([]byte(snapshotJSON), &snapshot); err != nil {
		return nil, err
	}
	rows, err := r.store.db.QueryContext(ctx, `SELECT ci.source_budget_item_id,COALESCE(SUM(CAST(ci.quantity AS REAL)),0),COALESCE(SUM(CAST(ci.amount AS REAL)),0) FROM contract_items_v2 ci JOIN contracts_v2 c ON c.id=ci.contract_id WHERE c.budget_version_id=? GROUP BY ci.source_budget_item_id`, version)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	allocated := map[string][2]float64{}
	for rows.Next() {
		var id string
		var q, a float64
		if err = rows.Scan(&id, &q, &a); err != nil {
			return nil, err
		}
		allocated[id] = [2]float64{q, a}
	}
	items := []map[string]any{}
	for _, row := range snapshot {
		id := fmt.Sprint(row["id"])
		if strings.TrimSpace(id) == "" {
			continue
		}
		bq, _ := strconv.ParseFloat(fmt.Sprint(row["quantity"]), 64)
		ba, _ := strconv.ParseFloat(fmt.Sprint(row["amount"]), 64)
		used := allocated[id]
		items = append(items, map[string]any{"source_budget_item_id": id, "item_no": row["item_no"], "name": row["name"], "baseline_quantity": fmt.Sprintf("%.8f", bq), "allocated_quantity": fmt.Sprintf("%.8f", used[0]), "remaining_quantity": fmt.Sprintf("%.8f", bq-used[0]), "baseline_amount": fmt.Sprintf("%.8f", ba), "allocated_amount": fmt.Sprintf("%.8f", used[1]), "remaining_amount": fmt.Sprintf("%.8f", ba-used[1])})
	}
	return map[string]any{"contract_id": contractID, "budget_version_id": version, "items": items}, nil
}

func (r *ContractAllocationRepository) AddItems(ctx context.Context, contractID string, rowVersion int64, items []ContractAllocationItem) (map[string]any, error) {
	_, version, status, current, err := r.contract(ctx, contractID)
	if err != nil {
		return nil, err
	}
	if status != "DRAFT" {
		return nil, errx.New(errx.CodeConflict, "only DRAFT contract can be allocated", "P5-G-ALLOC")
	}
	if rowVersion > 0 && rowVersion != current {
		return nil, errx.New(errx.CodeConflict, "row_version conflict", "P5-G-ALLOC")
	}
	if len(items) == 0 {
		return nil, errx.New(errx.CodeInvalidArgument, "items are required", "P5-G-ALLOC")
	}
	var snapshotJSON string
	if err = r.store.db.QueryRowContext(ctx, `SELECT snapshot_json FROM budget_versions WHERE id=?`, version).Scan(&snapshotJSON); err != nil {
		return nil, err
	}
	var snapshot []map[string]any
	if err = json.Unmarshal([]byte(snapshotJSON), &snapshot); err != nil {
		return nil, err
	}
	base := map[string]map[string]any{}
	for _, row := range snapshot {
		base[fmt.Sprint(row["id"])] = row
	}
	tx, err := r.store.db.BeginTx(ctx, nil)
	if err != nil {
		return nil, err
	}
	defer func() { _ = tx.Rollback() }()
	var count int
	if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM contract_items_v2 WHERE contract_id=?`, contractID).Scan(&count); err != nil {
		return nil, err
	}
	now := time.Now().UTC().Format(time.RFC3339Nano)
	for i, item := range items {
		source := strings.TrimSpace(item.SourceBudgetItemID)
		row, ok := base[source]
		if !ok {
			return nil, errx.New(errx.CodeInvalidArgument, "source budget item does not exist in baseline", "P5-G-ALLOC")
		}
		var exists int
		if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM contract_items_v2 WHERE contract_id=? AND source_budget_item_id=?`, contractID, source).Scan(&exists); err != nil {
			return nil, err
		}
		if exists > 0 {
			return nil, errx.New(errx.CodeInvalidArgument, "source budget item already exists in contract", "P5-G-ALLOC")
		}
		q, e := parseFloat(item.Quantity)
		if e != nil {
			return nil, e
		}
		a, e := parseFloat(item.Amount)
		if e != nil {
			return nil, e
		}
		bq, _ := strconv.ParseFloat(fmt.Sprint(row["quantity"]), 64)
		ba, _ := strconv.ParseFloat(fmt.Sprint(row["amount"]), 64)
		var uq, ua float64
		if err = tx.QueryRowContext(ctx, `SELECT COALESCE(SUM(CAST(ci.quantity AS REAL)),0),COALESCE(SUM(CAST(ci.amount AS REAL)),0) FROM contract_items_v2 ci JOIN contracts_v2 c ON c.id=ci.contract_id WHERE c.budget_version_id=? AND ci.source_budget_item_id=?`, version, source).Scan(&uq, &ua); err != nil {
			return nil, err
		}
		if q > bq-uq+1e-9 {
			return nil, errx.New(errx.CodeConflict, "allocated quantity exceeds remaining baseline", "P5-G-ALLOC")
		}
		if a > ba-ua+1e-9 {
			return nil, errx.New(errx.CodeConflict, "allocated amount exceeds remaining baseline", "P5-G-ALLOC")
		}
		name := item.Name
		if name == "" {
			name = fmt.Sprint(row["name"])
		}
		unit := item.Unit
		if unit == "" {
			unit = fmt.Sprint(row["unit"])
		}
		if _, err = tx.ExecContext(ctx, `INSERT INTO contract_items_v2(id,contract_id,source_budget_item_id,item_no,name,unit,quantity,unit_price,amount,sort_order,created_at) VALUES(?,?,?,?,?,?,?,?,?,?,?)`, fmt.Sprintf("%s-a%d", contractID, i+1), contractID, source, item.ItemNo, name, unit, item.Quantity, item.UnitPrice, item.Amount, count+i+1, now); err != nil {
			return nil, err
		}
	}
	var total float64
	if err = tx.QueryRowContext(ctx, `SELECT COALESCE(SUM(CAST(amount AS REAL)),0) FROM contract_items_v2 WHERE contract_id=?`, contractID).Scan(&total); err != nil {
		return nil, err
	}
	res, err := tx.ExecContext(ctx, `UPDATE contracts_v2 SET contract_amount=?,row_version=row_version+1,updated_at=? WHERE id=? AND row_version=?`, fmt.Sprintf("%.8f", total), now, contractID, current)
	if err != nil {
		return nil, err
	}
	affected, _ := res.RowsAffected()
	if affected != 1 {
		return nil, errx.New(errx.CodeConflict, "row_version conflict", "P5-G-ALLOC")
	}
	if err = tx.Commit(); err != nil {
		return nil, err
	}
	return r.Basis(ctx, contractID)
}

func (r *ContractAllocationRepository) LinkSubcontract(ctx context.Context, id, parentID, childID, actor string) (map[string]any, error) {
	if parentID == childID {
		return nil, errx.New(errx.CodeInvalidArgument, "contract cannot be its own parent", "P5-G-ALLOC")
	}
	pp, pv, _, _, err := r.contract(ctx, parentID)
	if err != nil {
		return nil, err
	}
	cp, cv, _, _, err := r.contract(ctx, childID)
	if err != nil {
		return nil, err
	}
	if pp != cp || pv != cv {
		return nil, errx.New(errx.CodeInvalidArgument, "parent and subcontract must share project and budget baseline", "P5-G-ALLOC")
	}
	now := time.Now().UTC().Format(time.RFC3339Nano)
	_, err = r.store.db.ExecContext(ctx, `INSERT INTO subcontract_links_v2(id,parent_contract_id,subcontract_id,created_by,created_at,row_version) VALUES(?,?,?,?,?,1)`, id, parentID, childID, actor, now)
	if err != nil {
		return nil, err
	}
	return map[string]any{"id": id, "parent_contract_id": parentID, "subcontract_id": childID, "deep_link": "/app/contracts/" + parentID + "?subcontract=" + childID}, nil
}
