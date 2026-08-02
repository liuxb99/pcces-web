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

type BidImportApplyRequest struct {
	ImportSessionID       string `json:"import_session_id"`
	TargetBudgetVersionID string `json:"target_budget_version_id"`
	Mode                  string `json:"mode"`
	ActorID               string `json:"actor_id"`
}

type BidImportLineage struct {
	SourceBudgetItemID   string `json:"source_budget_item_id"`
	ImportedBudgetItemID string `json:"imported_budget_item_id"`
}

type BidImportApplyRun struct {
	ID                      string             `json:"id"`
	ImportSessionID         string             `json:"import_session_id"`
	TargetBudgetProjectCode string             `json:"target_budget_project_code"`
	TargetBudgetVersionID   string             `json:"target_budget_version_id"`
	Mode                    string             `json:"mode"`
	Status                  string             `json:"status"`
	InsertedCount           int                `json:"inserted_count"`
	ReplacedCount           int                `json:"replaced_count"`
	SkippedCount            int                `json:"skipped_count"`
	Lineage                 []BidImportLineage `json:"lineage"`
	CreatedBy               string             `json:"created_by"`
	CreatedAt               string             `json:"created_at"`
	DeepLink                string             `json:"deep_link"`
}

type BidImportApplyRepository struct{ store *Store }

func NewBidImportApplyRepository(store *Store) *BidImportApplyRepository {
	return &BidImportApplyRepository{store: store}
}

func (r *BidImportApplyRepository) Apply(ctx context.Context, req BidImportApplyRequest) (BidImportApplyRun, error) {
	req.ImportSessionID = strings.TrimSpace(req.ImportSessionID)
	req.TargetBudgetVersionID = strings.TrimSpace(req.TargetBudgetVersionID)
	req.Mode = strings.ToUpper(strings.TrimSpace(req.Mode))
	req.ActorID = strings.TrimSpace(req.ActorID)
	if req.ImportSessionID == "" || req.TargetBudgetVersionID == "" || req.ActorID == "" {
		return BidImportApplyRun{}, errx.New(errx.CodeInvalidArgument, "import_session_id, target_budget_version_id and actor_id are required", "P4-BID-IMPORT-APPLY")
	}
	if req.Mode == "" {
		req.Mode = "CREATE"
	}
	if req.Mode != "CREATE" && req.Mode != "REPLACE" && req.Mode != "APPEND" {
		return BidImportApplyRun{}, errx.New(errx.CodeInvalidArgument, "mode must be CREATE, REPLACE or APPEND", "P4-BID-IMPORT-APPLY")
	}
	tx, err := r.store.db.BeginTx(ctx, nil)
	if err != nil {
		return BidImportApplyRun{}, err
	}
	defer func() { _ = tx.Rollback() }()

	var targetProject, sessionStatus, itemsJSON string
	if err = tx.QueryRowContext(ctx, `SELECT target_budget_project_code,status,items_json FROM bid_import_sessions WHERE id=?`, req.ImportSessionID).
		Scan(&targetProject, &sessionStatus, &itemsJSON); err == sql.ErrNoRows {
		return BidImportApplyRun{}, errx.New(errx.CodeNotFound, "bid import session not found", "P4-BID-IMPORT-APPLY")
	} else if err != nil {
		return BidImportApplyRun{}, err
	}
	if sessionStatus != "READY" {
		return BidImportApplyRun{}, errx.New(errx.CodeConflict, "blocked import session cannot be applied", "P4-BID-IMPORT-APPLY")
	}

	var versionProject, versionStatus string
	if err = tx.QueryRowContext(ctx, `SELECT project_code,status FROM budget_versions WHERE id=?`, req.TargetBudgetVersionID).
		Scan(&versionProject, &versionStatus); err == sql.ErrNoRows {
		return BidImportApplyRun{}, errx.New(errx.CodeNotFound, "target budget version not found", "P4-BID-IMPORT-APPLY")
	} else if err != nil {
		return BidImportApplyRun{}, err
	}
	if versionProject != targetProject {
		return BidImportApplyRun{}, errx.New(errx.CodeInvalidArgument, "target budget version belongs to a different project", "P4-BID-IMPORT-APPLY")
	}
	switch strings.ToUpper(versionStatus) {
	case "APPROVED", "FROZEN", "ARCHIVED":
		return BidImportApplyRun{}, errx.New(errx.CodeConflict, "approved or frozen budget version is read-only", "P4-BID-IMPORT-APPLY")
	}

	var items []BidImportItem
	if err = json.Unmarshal([]byte(itemsJSON), &items); err != nil {
		return BidImportApplyRun{}, err
	}
	rows, err := tx.QueryContext(ctx, `SELECT id,COALESCE(item_no,'') FROM budget_items_decimal WHERE project_code=?`, targetProject)
	if err != nil {
		return BidImportApplyRun{}, err
	}
	existingIDs := []string{}
	existingCodes := map[string]bool{}
	for rows.Next() {
		var id, code string
		if err = rows.Scan(&id, &code); err != nil {
			_ = rows.Close()
			return BidImportApplyRun{}, err
		}
		existingIDs = append(existingIDs, id)
		existingCodes[strings.ToUpper(strings.TrimSpace(code))] = true
	}
	_ = rows.Close()
	if req.Mode == "CREATE" && len(existingIDs) > 0 {
		return BidImportApplyRun{}, errx.New(errx.CodeConflict, "target budget project already contains items", "P4-BID-IMPORT-APPLY")
	}
	replaced := 0
	if req.Mode == "REPLACE" {
		replaced = len(existingIDs)
		if _, err = tx.ExecContext(ctx, `DELETE FROM budget_items_decimal WHERE project_code=?`, targetProject); err != nil {
			return BidImportApplyRun{}, err
		}
		existingCodes = map[string]bool{}
	}

	now := time.Now().UTC().Format(time.RFC3339Nano)
	inserted, skipped := 0, 0
	lineage := []BidImportLineage{}
	for index, item := range items {
		code := strings.ToUpper(strings.TrimSpace(item.Code))
		if req.Mode == "APPEND" && existingCodes[code] {
			skipped++
			continue
		}
		id := fmt.Sprintf("%s:%s:%d", targetProject, req.ImportSessionID, index+1)
		name := strings.TrimSpace(item.Name)
		if name == "" {
			name = code
		}
		if _, err = tx.ExecContext(ctx, `INSERT INTO budget_items_decimal(id,project_code,parent_id,item_no,name,kind,quantity,unit_price,amount,quantity_scale,price_scale,amount_scale,created_at,updated_at,row_version) VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,1)`,
			id, targetProject, nil, code, name, "L", defaultString(item.Quantity, "0"), defaultString(item.UnitPrice, "0"), defaultString(item.Amount, "0"), 4, 4, 2, now, now); err != nil {
			return BidImportApplyRun{}, err
		}
		sourceID := strings.TrimSpace(item.SourceBudgetItemID)
		if sourceID == "" {
			sourceID = strings.TrimSpace(item.ID)
		}
		lineage = append(lineage, BidImportLineage{SourceBudgetItemID: sourceID, ImportedBudgetItemID: id})
		inserted++
		existingCodes[code] = true
	}

	snapshotRows, err := tx.QueryContext(ctx, `SELECT id,project_code,parent_id,item_no,name,kind,quantity,unit_price,amount,quantity_scale,price_scale,amount_scale,row_version FROM budget_items_decimal WHERE project_code=? ORDER BY item_no,id`, targetProject)
	if err != nil {
		return BidImportApplyRun{}, err
	}
	var snapshot []map[string]any
	for snapshotRows.Next() {
		var id, projectCode, itemNo, name, kind, quantity, unitPrice, amount string
		var parentID sql.NullString
		var quantityScale, priceScale, amountScale int
		var rowVersion int64
		if err = snapshotRows.Scan(&id, &projectCode, &parentID, &itemNo, &name, &kind, &quantity, &unitPrice, &amount, &quantityScale, &priceScale, &amountScale, &rowVersion); err != nil {
			_ = snapshotRows.Close()
			return BidImportApplyRun{}, err
		}
		entry := map[string]any{"id": id, "project_code": projectCode, "parent_id": nil, "item_no": itemNo, "name": name, "kind": kind, "quantity": quantity, "unit_price": unitPrice, "amount": amount, "quantity_scale": quantityScale, "price_scale": priceScale, "amount_scale": amountScale, "row_version": rowVersion}
		if parentID.Valid {
			entry["parent_id"] = parentID.String
		}
		snapshot = append(snapshot, entry)
	}
	_ = snapshotRows.Close()
	snapshotJSON, err := json.Marshal(snapshot)
	if err != nil {
		return BidImportApplyRun{}, err
	}
	if _, err = tx.ExecContext(ctx, `UPDATE budget_versions SET snapshot_json=? WHERE id=?`, string(snapshotJSON), req.TargetBudgetVersionID); err != nil {
		return BidImportApplyRun{}, err
	}
	lineageJSON, err := json.Marshal(lineage)
	if err != nil {
		return BidImportApplyRun{}, err
	}
	runID := fmt.Sprintf("BIA-%d", time.Now().UTC().UnixNano())
	if _, err = tx.ExecContext(ctx, `INSERT INTO bid_import_apply_runs(id,import_session_id,target_budget_project_code,target_budget_version_id,mode,status,inserted_count,replaced_count,skipped_count,lineage_json,created_by,created_at) VALUES(?,?,?,?,?,?,?,?,?,?,?,?)`,
		runID, req.ImportSessionID, targetProject, req.TargetBudgetVersionID, req.Mode, "COMPLETED", inserted, replaced, skipped, string(lineageJSON), req.ActorID, now); err != nil {
		return BidImportApplyRun{}, err
	}
	if err = tx.Commit(); err != nil {
		return BidImportApplyRun{}, err
	}
	return r.Get(ctx, runID)
}

func (r *BidImportApplyRepository) Get(ctx context.Context, id string) (BidImportApplyRun, error) {
	var item BidImportApplyRun
	var lineageJSON string
	err := r.store.db.QueryRowContext(ctx, `SELECT id,import_session_id,target_budget_project_code,target_budget_version_id,mode,status,inserted_count,replaced_count,skipped_count,lineage_json,created_by,created_at FROM bid_import_apply_runs WHERE id=?`, id).
		Scan(&item.ID, &item.ImportSessionID, &item.TargetBudgetProjectCode, &item.TargetBudgetVersionID, &item.Mode, &item.Status, &item.InsertedCount, &item.ReplacedCount, &item.SkippedCount, &lineageJSON, &item.CreatedBy, &item.CreatedAt)
	if err == sql.ErrNoRows {
		return item, errx.New(errx.CodeNotFound, "bid import apply run not found", "P4-BID-IMPORT-APPLY")
	}
	if err != nil {
		return item, err
	}
	_ = json.Unmarshal([]byte(lineageJSON), &item.Lineage)
	item.DeepLink = "/app/projects/by-code/" + item.TargetBudgetProjectCode + "/budget-versions?version=" + item.TargetBudgetVersionID
	return item, nil
}
