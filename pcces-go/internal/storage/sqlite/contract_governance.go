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

type ContractGovernanceRepository struct{ store *Store }

func NewContractGovernanceRepository(store *Store) *ContractGovernanceRepository {
	return &ContractGovernanceRepository{store: store}
}

func allowedContractTransition(current, target string) bool {
	switch strings.ToUpper(current) {
	case "DRAFT":
		return target == "SUBMITTED"
	case "SUBMITTED":
		return target == "DRAFT" || target == "APPROVED"
	case "APPROVED":
		return target == "LOCKED"
	default:
		return false
	}
}

func (r *ContractGovernanceRepository) CreateVersion(ctx context.Context, id, contractID string, rowVersion int64, note, actor string) (map[string]any, error) {
	tx, err := r.store.db.BeginTx(ctx, nil)
	if err != nil {
		return nil, err
	}
	defer func() { _ = tx.Rollback() }()
	var status, project, version, no, name, contractor, amount string
	var currentRV int64
	err = tx.QueryRowContext(ctx, `SELECT status,project_code,budget_version_id,contract_no,name,COALESCE(contractor,''),contract_amount,row_version FROM contracts_v2 WHERE id=?`, contractID).Scan(&status, &project, &version, &no, &name, &contractor, &amount, &currentRV)
	if err == sql.ErrNoRows {
		return nil, errx.New(errx.CodeNotFound, "contract not found", "P5-G-GOV")
	}
	if err != nil {
		return nil, err
	}
	if currentRV != rowVersion {
		return nil, errx.New(errx.CodeConflict, "row version conflict", "P5-G-GOV")
	}
	if status == "APPROVED" || status == "LOCKED" {
		return nil, errx.New(errx.CodeConflict, "approved or locked contract cannot be overwritten; create a formal change issue", "P5-G-GOV")
	}
	itemsRows, err := tx.QueryContext(ctx, `SELECT id,source_budget_item_id,COALESCE(item_no,''),name,COALESCE(unit,''),quantity,unit_price,amount,sort_order FROM contract_items_v2 WHERE contract_id=? ORDER BY sort_order`, contractID)
	if err != nil {
		return nil, err
	}
	items := []map[string]any{}
	for itemsRows.Next() {
		var iid, source, itemNo, itemName, unit, qty, price, itemAmount string
		var sort int
		if err = itemsRows.Scan(&iid, &source, &itemNo, &itemName, &unit, &qty, &price, &itemAmount, &sort); err != nil {
			_ = itemsRows.Close()
			return nil, err
		}
		items = append(items, map[string]any{"id": iid, "source_budget_item_id": source, "item_no": itemNo, "name": itemName, "unit": unit, "quantity": qty, "unit_price": price, "amount": itemAmount, "sort_order": sort})
	}
	_ = itemsRows.Close()
	snapshot, _ := json.Marshal(map[string]any{"contract": map[string]any{"id": contractID, "project_code": project, "budget_version_id": version, "contract_no": no, "name": name, "contractor": contractor, "status": status, "contract_amount": amount, "row_version": currentRV}, "items": items})
	var latest int
	_ = tx.QueryRowContext(ctx, `SELECT COALESCE(MAX(version_no),0) FROM contract_versions_v2 WHERE contract_id=?`, contractID).Scan(&latest)
	now := time.Now().UTC().Format(time.RFC3339Nano)
	_, err = tx.ExecContext(ctx, `INSERT INTO contract_versions_v2(id,contract_id,version_no,status,snapshot_json,note,created_by,created_at,row_version) VALUES(?,?,?,?,?,?,?,?,1)`, id, contractID, latest+1, "DRAFT", string(snapshot), note, actor, now)
	if err != nil {
		return nil, err
	}
	if err = tx.Commit(); err != nil {
		return nil, err
	}
	return r.GetVersion(ctx, id)
}

func (r *ContractGovernanceRepository) Transition(ctx context.Context, id string, rowVersion int64, target, actor string) (map[string]any, error) {
	target = strings.ToUpper(strings.TrimSpace(target))
	tx, err := r.store.db.BeginTx(ctx, nil)
	if err != nil {
		return nil, err
	}
	defer func() { _ = tx.Rollback() }()
	var contractID, current string
	var currentRV int64
	err = tx.QueryRowContext(ctx, `SELECT contract_id,status,row_version FROM contract_versions_v2 WHERE id=?`, id).Scan(&contractID, &current, &currentRV)
	if err == sql.ErrNoRows {
		return nil, errx.New(errx.CodeNotFound, "contract version not found", "P5-G-GOV")
	}
	if err != nil {
		return nil, err
	}
	if currentRV != rowVersion {
		return nil, errx.New(errx.CodeConflict, "row version conflict", "P5-G-GOV")
	}
	if !allowedContractTransition(current, target) {
		return nil, errx.New(errx.CodeInvalidArgument, fmt.Sprintf("invalid contract version transition %s -> %s", current, target), "P5-G-GOV")
	}
	now := time.Now().UTC().Format(time.RFC3339Nano)
	approvedBy, approvedAt := any(nil), any(nil)
	if target == "APPROVED" {
		approvedBy, approvedAt = actor, now
	}
	res, err := tx.ExecContext(ctx, `UPDATE contract_versions_v2 SET status=?,approved_by=COALESCE(?,approved_by),approved_at=COALESCE(?,approved_at),row_version=row_version+1 WHERE id=? AND row_version=?`, target, approvedBy, approvedAt, id, rowVersion)
	if err != nil {
		return nil, err
	}
	count, _ := res.RowsAffected()
	if count != 1 {
		return nil, errx.New(errx.CodeConflict, "row version conflict", "P5-G-GOV")
	}
	if target == "APPROVED" || target == "LOCKED" {
		_, err = tx.ExecContext(ctx, `UPDATE contracts_v2 SET status=?,updated_at=?,row_version=row_version+1 WHERE id=?`, target, now, contractID)
		if err != nil {
			return nil, err
		}
	}
	if err = tx.Commit(); err != nil {
		return nil, err
	}
	return r.GetVersion(ctx, id)
}

func (r *ContractGovernanceRepository) GetVersion(ctx context.Context, id string) (map[string]any, error) {
	var contractID, status, snapshot, note, createdBy, createdAt string
	var approvedBy, approvedAt sql.NullString
	var versionNo int
	var rowVersion int64
	err := r.store.db.QueryRowContext(ctx, `SELECT contract_id,version_no,status,snapshot_json,COALESCE(note,''),created_by,created_at,approved_by,approved_at,row_version FROM contract_versions_v2 WHERE id=?`, id).Scan(&contractID, &versionNo, &status, &snapshot, &note, &createdBy, &createdAt, &approvedBy, &approvedAt, &rowVersion)
	if err == sql.ErrNoRows {
		return nil, errx.New(errx.CodeNotFound, "contract version not found", "P5-G-GOV")
	}
	if err != nil {
		return nil, err
	}
	var decoded map[string]any
	_ = json.Unmarshal([]byte(snapshot), &decoded)
	return map[string]any{"id": id, "contract_id": contractID, "version_no": versionNo, "status": status, "snapshot": decoded, "note": note, "created_by": createdBy, "created_at": createdAt, "approved_by": approvedBy.String, "approved_at": approvedAt.String, "row_version": rowVersion, "deep_link": "/app/contracts/" + contractID + "?version=" + id}, nil
}
