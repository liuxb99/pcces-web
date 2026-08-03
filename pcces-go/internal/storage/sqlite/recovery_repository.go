package sqlite

import (
	"context"
	"crypto/sha256"
	"database/sql"
	"encoding/hex"
	"time"

	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

type RecoverySnapshot struct {
	ID          string  `json:"id"`
	ActorID     string  `json:"actor_id"`
	ContextID   *string `json:"context_id,omitempty"`
	ProjectCode *string `json:"project_code,omitempty"`
	ActionCode  *string `json:"action_code,omitempty"`
	Payload     string  `json:"payload"`
	PayloadHash string  `json:"payload_hash"`
	Reason      string  `json:"reason"`
	CreatedAt   string  `json:"created_at"`
	RestoredAt  *string `json:"restored_at,omitempty"`
	DiscardedAt *string `json:"discarded_at,omitempty"`
	RowVersion  int64   `json:"row_version"`
}

type RecoveryRepository struct{ store *Store }

func NewRecoveryRepository(store *Store) *RecoveryRepository {
	return &RecoveryRepository{store: store}
}

func (r *RecoveryRepository) Create(ctx context.Context, item RecoverySnapshot) (RecoverySnapshot, error) {
	if item.ID == "" || item.ActorID == "" || item.Payload == "" || item.Reason == "" {
		return RecoverySnapshot{}, errx.New(errx.CodeInvalidArgument, "recovery id, actor, payload and reason are required", "P0-G4")
	}
	digest := sha256.Sum256([]byte(item.Payload))
	item.PayloadHash = hex.EncodeToString(digest[:])
	_, err := r.store.db.ExecContext(ctx, `
		INSERT INTO recovery_snapshots(
			id, actor_id, context_id, project_code, action_code,
			payload, payload_hash, reason
		) VALUES(?, ?, ?, ?, ?, ?, ?, ?)`,
		item.ID, item.ActorID, item.ContextID, item.ProjectCode, item.ActionCode,
		item.Payload, item.PayloadHash, item.Reason)
	if err != nil {
		return RecoverySnapshot{}, errx.Wrap(errx.CodeConflict, "create recovery snapshot", "P0-G4", err)
	}
	return r.Get(ctx, item.ID)
}

func (r *RecoveryRepository) Get(ctx context.Context, id string) (RecoverySnapshot, error) {
	var item RecoverySnapshot
	err := r.store.db.QueryRowContext(ctx, `
		SELECT id, actor_id, context_id, project_code, action_code,
		       payload, payload_hash, reason, created_at, restored_at,
		       discarded_at, row_version
		FROM recovery_snapshots WHERE id = ?`, id).
		Scan(&item.ID, &item.ActorID, &item.ContextID, &item.ProjectCode, &item.ActionCode,
			&item.Payload, &item.PayloadHash, &item.Reason, &item.CreatedAt,
			&item.RestoredAt, &item.DiscardedAt, &item.RowVersion)
	if err == sql.ErrNoRows {
		return RecoverySnapshot{}, errx.New(errx.CodeNotFound, "recovery snapshot not found", "P0-G4")
	}
	if err != nil {
		return RecoverySnapshot{}, errx.Wrap(errx.CodeDatabase, "get recovery snapshot", "P0-G4", err)
	}
	return item, nil
}

func (r *RecoveryRepository) ListPending(ctx context.Context, actorID string) ([]RecoverySnapshot, error) {
	rows, err := r.store.db.QueryContext(ctx, `
		SELECT id, actor_id, context_id, project_code, action_code,
		       payload, payload_hash, reason, created_at, restored_at,
		       discarded_at, row_version
		FROM recovery_snapshots
		WHERE actor_id = ? AND restored_at IS NULL AND discarded_at IS NULL
		ORDER BY created_at DESC`, actorID)
	if err != nil {
		return nil, errx.Wrap(errx.CodeDatabase, "list recovery snapshots", "P0-G4", err)
	}
	defer rows.Close()
	items := make([]RecoverySnapshot, 0)
	for rows.Next() {
		var item RecoverySnapshot
		if err := rows.Scan(&item.ID, &item.ActorID, &item.ContextID, &item.ProjectCode, &item.ActionCode,
			&item.Payload, &item.PayloadHash, &item.Reason, &item.CreatedAt,
			&item.RestoredAt, &item.DiscardedAt, &item.RowVersion); err != nil {
			return nil, errx.Wrap(errx.CodeDatabase, "scan recovery snapshot", "P0-G4", err)
		}
		items = append(items, item)
	}
	return items, rows.Err()
}

func (r *RecoveryRepository) MarkRestored(ctx context.Context, id string, rowVersion int64) (RecoverySnapshot, error) {
	return r.mark(ctx, id, rowVersion, "restored_at")
}

func (r *RecoveryRepository) MarkDiscarded(ctx context.Context, id string, rowVersion int64) (RecoverySnapshot, error) {
	return r.mark(ctx, id, rowVersion, "discarded_at")
}

func (r *RecoveryRepository) mark(ctx context.Context, id string, rowVersion int64, column string) (RecoverySnapshot, error) {
	if rowVersion <= 0 {
		return RecoverySnapshot{}, errx.New(errx.CodeInvalidArgument, "row_version is required", "P0-G4")
	}
	now := time.Now().UTC().Format(time.RFC3339Nano)
	query := `UPDATE recovery_snapshots SET ` + column + ` = ?, row_version = row_version + 1 WHERE id = ? AND row_version = ? AND restored_at IS NULL AND discarded_at IS NULL`
	result, err := r.store.db.ExecContext(ctx, query, now, id, rowVersion)
	if err != nil {
		return RecoverySnapshot{}, errx.Wrap(errx.CodeDatabase, "update recovery snapshot", "P0-G4", err)
	}
	count, _ := result.RowsAffected()
	if count != 1 {
		return RecoverySnapshot{}, errx.New(errx.CodeConflict, "recovery snapshot state conflict", "P0-G4")
	}
	return r.Get(ctx, id)
}
