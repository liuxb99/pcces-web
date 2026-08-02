package sqlite

import (
	"context"
	"database/sql"
	"strconv"

	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

type Setting struct {
	Key         string `json:"key"`
	Value       string `json:"value"`
	ValueType   string `json:"value_type"`
	Description string `json:"description,omitempty"`
	RowVersion  int64  `json:"row_version"`
}

type SettingsRepository struct{ store *Store }

func NewSettingsRepository(store *Store) *SettingsRepository {
	return &SettingsRepository{store: store}
}

func (r *SettingsRepository) List(ctx context.Context) ([]Setting, error) {
	rows, err := r.store.db.QueryContext(ctx, `
		SELECT key, value, value_type, COALESCE(description, ''), row_version
		FROM local_settings ORDER BY key`)
	if err != nil {
		return nil, errx.Wrap(errx.CodeDatabase, "list local settings", "P0-G2", err)
	}
	defer rows.Close()
	items := make([]Setting, 0)
	for rows.Next() {
		var item Setting
		if err := rows.Scan(&item.Key, &item.Value, &item.ValueType, &item.Description, &item.RowVersion); err != nil {
			return nil, errx.Wrap(errx.CodeDatabase, "scan local setting", "P0-G2", err)
		}
		items = append(items, item)
	}
	return items, rows.Err()
}

func (r *SettingsRepository) Get(ctx context.Context, key string) (Setting, error) {
	var item Setting
	err := r.store.db.QueryRowContext(ctx, `
		SELECT key, value, value_type, COALESCE(description, ''), row_version
		FROM local_settings WHERE key = ?`, key).
		Scan(&item.Key, &item.Value, &item.ValueType, &item.Description, &item.RowVersion)
	if err == sql.ErrNoRows {
		return Setting{}, errx.New(errx.CodeNotFound, "setting not found", "P0-G2")
	}
	if err != nil {
		return Setting{}, errx.Wrap(errx.CodeDatabase, "get local setting", "P0-G2", err)
	}
	return item, nil
}

func (r *SettingsRepository) Save(ctx context.Context, item Setting) (Setting, error) {
	if item.Key == "" || item.ValueType == "" {
		return Setting{}, errx.New(errx.CodeInvalidArgument, "setting key and value_type are required", "P0-G2")
	}
	if err := validateSettingValue(item.ValueType, item.Value); err != nil {
		return Setting{}, err
	}
	if item.RowVersion == 0 {
		_, err := r.store.db.ExecContext(ctx, `
			INSERT INTO local_settings(key, value, value_type, description)
			VALUES(?, ?, ?, ?)`, item.Key, item.Value, item.ValueType, item.Description)
		if err != nil {
			return Setting{}, errx.Wrap(errx.CodeConflict, "create local setting", "P0-G2", err)
		}
	} else {
		res, err := r.store.db.ExecContext(ctx, `
			UPDATE local_settings
			SET value = ?, value_type = ?, description = ?,
			    updated_at = strftime('%Y-%m-%dT%H:%M:%fZ','now'), row_version = row_version + 1
			WHERE key = ? AND row_version = ?`, item.Value, item.ValueType, item.Description, item.Key, item.RowVersion)
		if err != nil {
			return Setting{}, errx.Wrap(errx.CodeDatabase, "update local setting", "P0-G2", err)
		}
		count, _ := res.RowsAffected()
		if count != 1 {
			return Setting{}, errx.New(errx.CodeConflict, "setting row version conflict", "P0-G2")
		}
	}
	return r.Get(ctx, item.Key)
}

func validateSettingValue(valueType, value string) error {
	switch valueType {
	case "string":
		return nil
	case "bool":
		if _, err := strconv.ParseBool(value); err != nil {
			return errx.New(errx.CodeInvalidArgument, "invalid bool setting value", "P0-G2")
		}
	case "int":
		if _, err := strconv.ParseInt(value, 10, 64); err != nil {
			return errx.New(errx.CodeInvalidArgument, "invalid int setting value", "P0-G2")
		}
	default:
		return errx.New(errx.CodeInvalidArgument, "unsupported setting value_type", "P0-G2")
	}
	return nil
}
