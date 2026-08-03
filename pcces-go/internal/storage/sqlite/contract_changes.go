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

type ContractChangeItemInput struct {
	Action             string `json:"action"`
	ContractItemID     string `json:"contract_item_id"`
	SourceBudgetItemID string `json:"source_budget_item_id"`
	ItemNo             string `json:"item_no"`
	Name               string `json:"name"`
	Unit               string `json:"unit"`
	QuantityDelta      string `json:"quantity_delta"`
	UnitPrice          string `json:"unit_price"`
	AmountDelta        string `json:"amount_delta"`
}

type ContractChangeRequest struct {
	ID, ContractID, ChangeNo, Reason, Actor string
	Items                                   []ContractChangeItemInput
}

type ContractChangeRepository struct{ store *Store }

func NewContractChangeRepository(store *Store) *ContractChangeRepository {
	return &ContractChangeRepository{store: store}
}

func parseSigned(value, field string) (float64, error) {
	v, err := strconv.ParseFloat(strings.TrimSpace(value), 64)
	if err != nil {
		return 0, errx.New(errx.CodeInvalidArgument, field+" must be decimal", "P5-G-CHANGE")
	}
	return v, nil
}

func (r *ContractChangeRepository) Create(ctx context.Context, req ContractChangeRequest) (map[string]any, error) {
	if req.ID == "" || req.ContractID == "" || req.ChangeNo == "" || req.Reason == "" || req.Actor == "" || len(req.Items) == 0 {
		return nil, errx.New(errx.CodeInvalidArgument, "required contract change fields are missing", "P5-G-CHANGE")
	}
	var status, amount string
	if err := r.store.db.QueryRowContext(ctx, `SELECT status,contract_amount FROM contracts_v2 WHERE id=?`, req.ContractID).Scan(&status, &amount); err == sql.ErrNoRows {
		return nil, errx.New(errx.CodeNotFound, "contract not found", "P5-G-CHANGE")
	} else if err != nil {
		return nil, err
	}
	if status != "APPROVED" && status != "LOCKED" {
		return nil, errx.New(errx.CodeConflict, "formal change requires APPROVED or LOCKED contract", "P5-G-CHANGE")
	}
	before, err := parseSigned(amount, "before_amount")
	if err != nil {
		return nil, err
	}
	delta := 0.0
	for _, item := range req.Items {
		action := strings.ToUpper(strings.TrimSpace(item.Action))
		switch action {
		case "ADD", "INCREASE", "DECREASE", "DELETE":
		default:
			return nil, errx.New(errx.CodeInvalidArgument, "invalid contract change action", "P5-G-CHANGE")
		}
		if action != "ADD" && item.ContractItemID == "" {
			return nil, errx.New(errx.CodeInvalidArgument, "contract_item_id is required", "P5-G-CHANGE")
		}
		v, err := parseSigned(item.AmountDelta, "amount_delta")
		if err != nil {
			return nil, err
		}
		if action == "DECREASE" || action == "DELETE" {
			v = -contractChangeAbs(v)
		} else {
			v = contractChangeAbs(v)
		}
		delta += v
	}
	after := before + delta
	if after < 0 {
		return nil, errx.New(errx.CodeInvalidArgument, "after_amount cannot be negative", "P5-G-CHANGE")
	}
	beforeItems, err := r.contractItems(ctx, req.ContractID)
	if err != nil {
		return nil, err
	}
	snapshot, _ := json.Marshal(beforeItems)
	tx, err := r.store.db.BeginTx(ctx, nil)
	if err != nil {
		return nil, err
	}
	defer func() { _ = tx.Rollback() }()
	now := time.Now().UTC().Format(time.RFC3339Nano)
	if _, err = tx.ExecContext(ctx, `INSERT INTO contract_change_orders(id,contract_id,change_no,reason,status,before_amount,delta_amount,after_amount,before_snapshot_json,after_snapshot_json,created_by,created_at,row_version) VALUES(?,?,?,?,?,?,?,?,?,?,?,?,1)`, req.ID, req.ContractID, req.ChangeNo, req.Reason, "APPROVED", fmt.Sprintf("%.8f", before), fmt.Sprintf("%.8f", delta), fmt.Sprintf("%.8f", after), string(snapshot), string(snapshot), req.Actor, now); err != nil {
		return nil, err
	}
	for i, item := range req.Items {
		action := strings.ToUpper(strings.TrimSpace(item.Action))
		qty, err := parseSigned(defaultString(item.QuantityDelta, "0"), "quantity_delta")
		if err != nil {
			return nil, err
		}
		price, err := parseSigned(defaultString(item.UnitPrice, "0"), "unit_price")
		if err != nil {
			return nil, err
		}
		amt, err := parseSigned(item.AmountDelta, "amount_delta")
		if err != nil {
			return nil, err
		}
		if action == "DECREASE" || action == "DELETE" {
			qty, amt = -contractChangeAbs(qty), -contractChangeAbs(amt)
		} else {
			qty, amt = contractChangeAbs(qty), contractChangeAbs(amt)
		}
		if _, err = tx.ExecContext(ctx, `INSERT INTO contract_change_items(id,change_order_id,action,contract_item_id,source_budget_item_id,item_no,name,unit,quantity_delta,unit_price,amount_delta,sort_order) VALUES(?,?,?,?,?,?,?,?,?,?,?,?)`, fmt.Sprintf("%s-%d", req.ID, i+1), req.ID, action, contractChangeNullable(item.ContractItemID), contractChangeNullable(item.SourceBudgetItemID), contractChangeNullable(item.ItemNo), item.Name, contractChangeNullable(item.Unit), fmt.Sprintf("%.8f", qty), fmt.Sprintf("%.8f", price), fmt.Sprintf("%.8f", amt), i+1); err != nil {
			return nil, err
		}
	}
	if _, err = tx.ExecContext(ctx, `UPDATE contracts_v2 SET contract_amount=?,status='APPROVED',updated_at=?,row_version=row_version+1 WHERE id=?`, fmt.Sprintf("%.8f", after), now, req.ContractID); err != nil {
		return nil, err
	}
	if err = tx.Commit(); err != nil {
		return nil, err
	}
	return r.Get(ctx, req.ID)
}

func contractChangeAbs(v float64) float64 {
	if v < 0 {
		return -v
	}
	return v
}

func contractChangeNullable(v string) any {
	if strings.TrimSpace(v) == "" {
		return nil
	}
	return v
}

func (r *ContractChangeRepository) contractItems(ctx context.Context, contractID string) ([]map[string]any, error) {
	rows, err := r.store.db.QueryContext(ctx, `SELECT id,source_budget_item_id,COALESCE(item_no,''),name,COALESCE(unit,''),quantity,unit_price,amount FROM contract_items_v2 WHERE contract_id=? ORDER BY sort_order`, contractID)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	result := []map[string]any{}
	for rows.Next() {
		var id, source, no, name, unit, qty, price, amount string
		if err := rows.Scan(&id, &source, &no, &name, &unit, &qty, &price, &amount); err != nil {
			return nil, err
		}
		result = append(result, map[string]any{"id": id, "source_budget_item_id": source, "item_no": no, "name": name, "unit": unit, "quantity": qty, "unit_price": price, "amount": amount})
	}
	return result, rows.Err()
}

func (r *ContractChangeRepository) Get(ctx context.Context, id string) (map[string]any, error) {
	var contractID, no, reason, status, before, delta, after string
	if err := r.store.db.QueryRowContext(ctx, `SELECT contract_id,change_no,reason,status,before_amount,delta_amount,after_amount FROM contract_change_orders WHERE id=?`, id).Scan(&contractID, &no, &reason, &status, &before, &delta, &after); err == sql.ErrNoRows {
		return nil, errx.New(errx.CodeNotFound, "contract change not found", "P5-G-CHANGE")
	} else if err != nil {
		return nil, err
	}
	rows, err := r.store.db.QueryContext(ctx, `SELECT action,COALESCE(contract_item_id,''),name,quantity_delta,unit_price,amount_delta FROM contract_change_items WHERE change_order_id=? ORDER BY sort_order`, id)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	items := []map[string]any{}
	for rows.Next() {
		var action, itemID, name, qty, price, amt string
		if err := rows.Scan(&action, &itemID, &name, &qty, &price, &amt); err != nil {
			return nil, err
		}
		items = append(items, map[string]any{"action": action, "contract_item_id": itemID, "name": name, "quantity_delta": qty, "unit_price": price, "amount_delta": amt})
	}
	return map[string]any{"id": id, "contract_id": contractID, "change_no": no, "reason": reason, "status": status, "before_amount": before, "delta_amount": delta, "after_amount": after, "items": items, "deep_link": "/app/contracts/" + contractID + "/changes/" + id}, rows.Err()
}
