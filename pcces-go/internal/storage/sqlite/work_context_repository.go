package sqlite

import (
	"context"
	"database/sql"
	"time"

	"github.com/liuxb99/pcces-web/pcces-go/internal/domain/workcontext"
	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

// WorkContextRepository persists Local Go dirty/draft state with optimistic locking.
type WorkContextRepository struct {
	store *Store
}

func NewWorkContextRepository(store *Store) *WorkContextRepository {
	return &WorkContextRepository{store: store}
}

func (r *WorkContextRepository) Get(ctx context.Context, id string) (*workcontext.Context, error) {
	row := r.store.DB().QueryRowContext(ctx, `
		SELECT id, actor_id, action_code, project_code, resource_type, resource_id,
		       dirty, draft_payload, created_at, updated_at, row_version
		FROM work_contexts WHERE id = ?`, id)
	item, err := scanWorkContext(row)
	if err == sql.ErrNoRows {
		return nil, errx.New(errx.CodeNotFound, "work context not found", "P0-G4")
	}
	if err != nil {
		return nil, errx.Wrap(errx.CodeDatabase, "get work context", "P0-G4", err)
	}
	return item, nil
}

func (r *WorkContextRepository) Save(ctx context.Context, req workcontext.SaveRequest) (*workcontext.Context, error) {
	if req.ID == "" || req.ActorID == "" || req.ActionCode == "" {
		return nil, errx.New(errx.CodeInvalidArgument, "id, actor_id and action_code are required", "P0-G4")
	}

	err := r.store.WithTx(ctx, func(tx *sql.Tx) error {
		var exists int
		var currentVersion int64
		err := tx.QueryRowContext(ctx, `SELECT 1, row_version FROM work_contexts WHERE id = ?`, req.ID).Scan(&exists, &currentVersion)
		switch err {
		case sql.ErrNoRows:
			_, err = tx.ExecContext(ctx, `
				INSERT INTO work_contexts(
					id, actor_id, action_code, project_code, resource_type, resource_id,
					dirty, draft_payload, row_version
				) VALUES (?, ?, ?, ?, ?, ?, ?, ?, 1)`,
				req.ID, req.ActorID, req.ActionCode, req.ProjectCode, req.ResourceType,
				req.ResourceID, boolToInt(req.Dirty), req.DraftPayload)
			if err != nil {
				return errx.Wrap(errx.CodeDatabase, "insert work context", "P0-G4", err)
			}
			return nil
		case nil:
			if req.RowVersion <= 0 || req.RowVersion != currentVersion {
				return errx.New(errx.CodeConflict, "work context row_version conflict", "P0-G4")
			}
			result, err := tx.ExecContext(ctx, `
				UPDATE work_contexts
				SET actor_id = ?, action_code = ?, project_code = ?, resource_type = ?,
				    resource_id = ?, dirty = ?, draft_payload = ?,
				    updated_at = strftime('%Y-%m-%dT%H:%M:%fZ','now'),
				    row_version = row_version + 1
				WHERE id = ? AND row_version = ?`,
				req.ActorID, req.ActionCode, req.ProjectCode, req.ResourceType,
				req.ResourceID, boolToInt(req.Dirty), req.DraftPayload, req.ID, req.RowVersion)
			if err != nil {
				return errx.Wrap(errx.CodeDatabase, "update work context", "P0-G4", err)
			}
			count, err := result.RowsAffected()
			if err != nil {
				return errx.Wrap(errx.CodeDatabase, "read work context update count", "P0-G4", err)
			}
			if count != 1 {
				return errx.New(errx.CodeConflict, "work context was modified concurrently", "P0-G4")
			}
			return nil
		default:
			return errx.Wrap(errx.CodeDatabase, "check work context", "P0-G4", err)
		}
	})
	if err != nil {
		return nil, err
	}
	return r.Get(ctx, req.ID)
}

func (r *WorkContextRepository) Delete(ctx context.Context, id string, rowVersion int64) error {
	result, err := r.store.DB().ExecContext(ctx, `DELETE FROM work_contexts WHERE id = ? AND row_version = ?`, id, rowVersion)
	if err != nil {
		return errx.Wrap(errx.CodeDatabase, "delete work context", "P0-G4", err)
	}
	count, err := result.RowsAffected()
	if err != nil {
		return errx.Wrap(errx.CodeDatabase, "read work context delete count", "P0-G4", err)
	}
	if count != 1 {
		return errx.New(errx.CodeConflict, "work context delete conflict", "P0-G4")
	}
	return nil
}

type rowScanner interface {
	Scan(dest ...any) error
}

func scanWorkContext(row rowScanner) (*workcontext.Context, error) {
	var item workcontext.Context
	var projectCode, resourceType, resourceID, draft sql.NullString
	var dirty int
	var createdAt, updatedAt string
	if err := row.Scan(
		&item.ID, &item.ActorID, &item.ActionCode, &projectCode, &resourceType,
		&resourceID, &dirty, &draft, &createdAt, &updatedAt, &item.RowVersion,
	); err != nil {
		return nil, err
	}
	item.ProjectCode = nullStringFromSQL(projectCode)
	item.ResourceType = nullStringFromSQL(resourceType)
	item.ResourceID = nullStringFromSQL(resourceID)
	item.DraftPayload = nullStringFromSQL(draft)
	item.Dirty = dirty == 1
	item.CreatedAt, _ = time.Parse(time.RFC3339Nano, createdAt)
	item.UpdatedAt, _ = time.Parse(time.RFC3339Nano, updatedAt)
	return &item, nil
}

func nullStringFromSQL(value sql.NullString) *string {
	if !value.Valid {
		return nil
	}
	result := value.String
	return &result
}

func boolToInt(value bool) int {
	if value {
		return 1
	}
	return 0
}
