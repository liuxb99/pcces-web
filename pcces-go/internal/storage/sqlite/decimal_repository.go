package sqlite

import (
	"context"
	"database/sql"
	"encoding/json"
	"time"

	"github.com/liuxb99/pcces-web/pcces-go/internal/domain/money"
	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

type DecimalRecord struct {
	ID         string `json:"id"`
	Value      string `json:"value"`
	CreatedAt  string `json:"created_at"`
	UpdatedAt  string `json:"updated_at"`
	RowVersion int64  `json:"row_version"`
}

type DecimalRepository struct{ store *Store }

func NewDecimalRepository(store *Store) *DecimalRepository { return &DecimalRepository{store: store} }

func (r *DecimalRepository) Create(ctx context.Context, id, value, actorID string) (DecimalRecord, error) {
	canonical, err := money.Quantize(value, 8)
	if err != nil || id == "" {
		return DecimalRecord{}, errx.New(errx.CodeInvalidArgument, "id and valid decimal value are required", "P0-G2")
	}
	now := time.Now().UTC().Format(time.RFC3339Nano)
	err = r.store.WithTx(ctx, func(tx *sql.Tx) error {
		if _, err := tx.ExecContext(ctx, `INSERT INTO p0_decimal_records(id,value,created_at,updated_at,row_version) VALUES(?,?,?,?,1)`, id, canonical, now, now); err != nil {
			return errx.Wrap(errx.CodeConflict, "create decimal record", "P0-G2", err)
		}
		return appendDecimalAudit(ctx, tx, actorID, "DECIMAL_CREATE", id, map[string]any{"value": canonical})
	})
	if err != nil {
		return DecimalRecord{}, err
	}
	return r.Get(ctx, id)
}

func (r *DecimalRepository) Update(ctx context.Context, id, value, actorID string, rowVersion int64) (DecimalRecord, error) {
	canonical, err := money.Quantize(value, 8)
	if err != nil || rowVersion <= 0 {
		return DecimalRecord{}, errx.New(errx.CodeInvalidArgument, "valid decimal value and row_version are required", "P0-G2")
	}
	now := time.Now().UTC().Format(time.RFC3339Nano)
	err = r.store.WithTx(ctx, func(tx *sql.Tx) error {
		result, err := tx.ExecContext(ctx, `UPDATE p0_decimal_records SET value=?,updated_at=?,row_version=row_version+1 WHERE id=? AND row_version=?`, canonical, now, id, rowVersion)
		if err != nil {
			return errx.Wrap(errx.CodeDatabase, "update decimal record", "P0-G2", err)
		}
		count, _ := result.RowsAffected()
		if count != 1 {
			return errx.New(errx.CodeConflict, "stale row_version", "P0-G2")
		}
		return appendDecimalAudit(ctx, tx, actorID, "DECIMAL_UPDATE", id, map[string]any{"value": canonical, "previous_row_version": rowVersion, "row_version": rowVersion + 1})
	})
	if err != nil {
		return DecimalRecord{}, err
	}
	return r.Get(ctx, id)
}

func (r *DecimalRepository) Get(ctx context.Context, id string) (DecimalRecord, error) {
	var item DecimalRecord
	err := r.store.db.QueryRowContext(ctx, `SELECT id,value,created_at,updated_at,row_version FROM p0_decimal_records WHERE id=?`, id).
		Scan(&item.ID, &item.Value, &item.CreatedAt, &item.UpdatedAt, &item.RowVersion)
	if err == sql.ErrNoRows {
		return DecimalRecord{}, errx.New(errx.CodeNotFound, "decimal record not found", "P0-G2")
	}
	if err != nil {
		return DecimalRecord{}, errx.Wrap(errx.CodeDatabase, "get decimal record", "P0-G2", err)
	}
	return item, nil
}

func appendDecimalAudit(ctx context.Context, tx *sql.Tx, actorID, eventType, resourceID string, payload any) error {
	encoded, err := json.Marshal(payload)
	if err != nil {
		return errx.Wrap(errx.CodeInternal, "encode audit payload", "P0-G2", err)
	}
	_, err = tx.ExecContext(ctx, `INSERT INTO p0_audit_events(actor_id,feature_id,event_type,resource_type,resource_id,payload) VALUES(?,?,?,?,?,?)`, actorID, "P0-G2", eventType, "decimal_record", resourceID, string(encoded))
	if err != nil {
		return errx.Wrap(errx.CodeDatabase, "append decimal audit", "P0-G2", err)
	}
	return nil
}
