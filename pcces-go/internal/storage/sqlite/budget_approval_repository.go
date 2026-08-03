package sqlite

import (
	"context"
	"database/sql"
	"encoding/json"
	"fmt"
	"time"

	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

type BudgetApprovalState struct {
	ProjectCode string `json:"project_code"`
	Status      string `json:"status"`
	SubmittedBy string `json:"submitted_by,omitempty"`
	ReviewedBy  string `json:"reviewed_by,omitempty"`
	Comment     string `json:"comment,omitempty"`
	RowVersion  int64  `json:"row_version"`
	UpdatedAt   string `json:"updated_at,omitempty"`
}

type BudgetItemLock struct {
	ProjectCode string `json:"project_code"`
	ItemID      string `json:"item_id"`
	Locked      bool   `json:"locked"`
	Reason      string `json:"reason,omitempty"`
	LockedBy    string `json:"locked_by,omitempty"`
	UpdatedAt   string `json:"updated_at,omitempty"`
}

type BudgetWorkflowAudit struct {
	ID          string `json:"id"`
	ProjectCode string `json:"project_code"`
	ItemID      string `json:"item_id,omitempty"`
	EventType   string `json:"event_type"`
	ActorID     string `json:"actor_id"`
	FromStatus  string `json:"from_status,omitempty"`
	ToStatus    string `json:"to_status,omitempty"`
	PayloadJSON string `json:"payload_json"`
	CreatedAt   string `json:"created_at"`
}

type BudgetApprovalRepository struct{ store *Store }

func NewBudgetApprovalRepository(store *Store) *BudgetApprovalRepository {
	return &BudgetApprovalRepository{store: store}
}

func (r *BudgetApprovalRepository) State(ctx context.Context, projectCode string) (BudgetApprovalState, error) {
	var s BudgetApprovalState
	err := r.store.db.QueryRowContext(ctx, `SELECT project_code,status,COALESCE(submitted_by,''),COALESCE(reviewed_by,''),COALESCE(comment,''),row_version,updated_at FROM budget_approval_states WHERE project_code=?`, projectCode).Scan(&s.ProjectCode, &s.Status, &s.SubmittedBy, &s.ReviewedBy, &s.Comment, &s.RowVersion, &s.UpdatedAt)
	if err == sql.ErrNoRows {
		return BudgetApprovalState{ProjectCode: projectCode, Status: "DRAFT"}, nil
	}
	return s, err
}

func roleAllowed(command, role string) bool {
	if command == "SUBMIT" {
		return role == "editor" || role == "reviewer" || role == "admin"
	}
	return role == "reviewer" || role == "admin"
}

func nextApproval(status, command string) (string, bool) {
	transitions := map[string]map[string]string{
		"SUBMIT":  {"DRAFT": "SUBMITTED", "RETURNED": "SUBMITTED"},
		"APPROVE": {"SUBMITTED": "APPROVED"},
		"RETURN":  {"SUBMITTED": "RETURNED"},
		"REOPEN":  {"APPROVED": "DRAFT"},
	}
	next, ok := transitions[command][status]
	return next, ok
}

func (r *BudgetApprovalRepository) Transition(ctx context.Context, projectCode, command, actorID, role, comment string, expected int64) (BudgetApprovalState, error) {
	if !roleAllowed(command, role) {
		return BudgetApprovalState{}, errx.New(errx.CodeForbidden, "workflow role denied", "P2-G4")
	}
	current, err := r.State(ctx, projectCode)
	if err != nil {
		return BudgetApprovalState{}, err
	}
	if current.RowVersion != expected {
		return BudgetApprovalState{}, errx.New(errx.CodeConflict, "approval row version conflict", "P2-G4")
	}
	next, ok := nextApproval(current.Status, command)
	if !ok {
		return BudgetApprovalState{}, errx.New(errx.CodeInvalidArgument, "invalid approval transition", "P2-G4")
	}
	tx, err := r.store.db.BeginTx(ctx, nil)
	if err != nil {
		return BudgetApprovalState{}, err
	}
	defer tx.Rollback()
	now := time.Now().UTC().Format(time.RFC3339Nano)
	submitted, reviewed := current.SubmittedBy, current.ReviewedBy
	if command == "SUBMIT" {
		submitted = actorID
	} else {
		reviewed = actorID
	}
	if expected == 0 && current.Status == "DRAFT" {
		_, err = tx.ExecContext(ctx, `INSERT INTO budget_approval_states(project_code,status,submitted_by,reviewed_by,comment,row_version,updated_at) VALUES(?,?,?,?,?,?,?)`, projectCode, next, submitted, reviewed, comment, 1, now)
	} else {
		res, e := tx.ExecContext(ctx, `UPDATE budget_approval_states SET status=?,submitted_by=?,reviewed_by=?,comment=?,row_version=row_version+1,updated_at=? WHERE project_code=? AND row_version=?`, next, submitted, reviewed, comment, now, projectCode, expected)
		err = e
		if err == nil {
			n, _ := res.RowsAffected()
			if n != 1 {
				return BudgetApprovalState{}, errx.New(errx.CodeConflict, "approval row version conflict", "P2-G4")
			}
		}
	}
	if err != nil {
		return BudgetApprovalState{}, err
	}
	payload, _ := json.Marshal(map[string]string{"comment": comment})
	_, err = tx.ExecContext(ctx, `INSERT INTO budget_workflow_audit(id,project_code,event_type,actor_id,from_status,to_status,payload_json,created_at) VALUES(?,?,?,?,?,?,?,?)`, fmt.Sprintf("%d", time.Now().UnixNano()), projectCode, command, actorID, current.Status, next, string(payload), now)
	if err != nil {
		return BudgetApprovalState{}, err
	}
	locked := 0
	reason := ""
	if next == "APPROVED" {
		locked = 1
		reason = "approved budget"
	}
	if next == "APPROVED" || next == "RETURNED" || next == "DRAFT" {
		_, err = tx.ExecContext(ctx, `INSERT INTO budget_project_locks(project_code,locked,reason,locked_by,updated_at) VALUES(?,?,?,?,?) ON CONFLICT(project_code) DO UPDATE SET locked=excluded.locked,reason=excluded.reason,locked_by=excluded.locked_by,updated_at=excluded.updated_at`, projectCode, locked, reason, actorID, now)
		if err != nil {
			return BudgetApprovalState{}, err
		}
	}
	if err = tx.Commit(); err != nil {
		return BudgetApprovalState{}, err
	}
	return r.State(ctx, projectCode)
}

func (r *BudgetApprovalRepository) SetItemLock(ctx context.Context, projectCode, itemID string, locked bool, actorID, role, reason string) (BudgetItemLock, error) {
	if role != "reviewer" && role != "admin" {
		return BudgetItemLock{}, errx.New(errx.CodeForbidden, "reviewer permission required", "P2-G4")
	}
	now := time.Now().UTC().Format(time.RFC3339Nano)
	value := 0
	if locked {
		value = 1
	}
	_, err := r.store.db.ExecContext(ctx, `INSERT INTO budget_item_locks(project_code,item_id,locked,reason,locked_by,updated_at) VALUES(?,?,?,?,?,?) ON CONFLICT(project_code,item_id) DO UPDATE SET locked=excluded.locked,reason=excluded.reason,locked_by=excluded.locked_by,updated_at=excluded.updated_at`, projectCode, itemID, value, reason, actorID, now)
	if err != nil {
		return BudgetItemLock{}, err
	}
	payload, _ := json.Marshal(map[string]string{"reason": reason})
	event := "ITEM_UNLOCK"
	if locked {
		event = "ITEM_LOCK"
	}
	_, err = r.store.db.ExecContext(ctx, `INSERT INTO budget_workflow_audit(id,project_code,item_id,event_type,actor_id,payload_json,created_at) VALUES(?,?,?,?,?,?,?)`, fmt.Sprintf("%d", time.Now().UnixNano()), projectCode, itemID, event, actorID, string(payload), now)
	return BudgetItemLock{ProjectCode: projectCode, ItemID: itemID, Locked: locked, Reason: reason, LockedBy: actorID, UpdatedAt: now}, err
}

func (r *BudgetApprovalRepository) AssertWritable(ctx context.Context, projectCode, itemID string) error {
	state, err := r.State(ctx, projectCode)
	if err != nil {
		return err
	}
	if state.Status == "SUBMITTED" || state.Status == "APPROVED" {
		return errx.New(errx.CodeConflict, "budget approval state is read-only", "P2-G4")
	}
	var locked int
	err = r.store.db.QueryRowContext(ctx, `SELECT locked FROM budget_project_locks WHERE project_code=?`, projectCode).Scan(&locked)
	if err != nil && err != sql.ErrNoRows {
		return err
	}
	if locked == 1 {
		return errx.New(errx.CodeConflict, "budget project is locked", "P2-G4")
	}
	if itemID != "" {
		err = r.store.db.QueryRowContext(ctx, `SELECT locked FROM budget_item_locks WHERE project_code=? AND item_id=?`, projectCode, itemID).Scan(&locked)
		if err != nil && err != sql.ErrNoRows {
			return err
		}
		if locked == 1 {
			return errx.New(errx.CodeConflict, "budget item is locked", "P2-G4")
		}
	}
	return nil
}

func (r *BudgetApprovalRepository) Audits(ctx context.Context, projectCode string) ([]BudgetWorkflowAudit, error) {
	rows, err := r.store.db.QueryContext(ctx, `SELECT id,project_code,COALESCE(item_id,''),event_type,actor_id,COALESCE(from_status,''),COALESCE(to_status,''),payload_json,created_at FROM budget_workflow_audit WHERE project_code=? ORDER BY created_at DESC`, projectCode)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	var result []BudgetWorkflowAudit
	for rows.Next() {
		var a BudgetWorkflowAudit
		if err = rows.Scan(&a.ID, &a.ProjectCode, &a.ItemID, &a.EventType, &a.ActorID, &a.FromStatus, &a.ToStatus, &a.PayloadJSON, &a.CreatedAt); err != nil {
			return nil, err
		}
		result = append(result, a)
	}
	return result, rows.Err()
}
