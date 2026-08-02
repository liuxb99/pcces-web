package sqlite

import (
	"context"
	"database/sql"

	"github.com/liuxb99/pcces-web/pcces-go/internal/domain/catalog"
	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

// CatalogRepository exposes the Phase 0 module, function-code and action catalogs.
type CatalogRepository struct {
	db *sql.DB
}

func NewCatalogRepository(store *Store) *CatalogRepository {
	return &CatalogRepository{db: store.DB()}
}

func (r *CatalogRepository) ListModules(ctx context.Context) ([]catalog.Module, error) {
	rows, err := r.db.QueryContext(ctx, `SELECT code, name, enabled, row_version FROM modules ORDER BY code`)
	if err != nil {
		return nil, errx.Wrap(errx.CodeDatabase, "list modules", "P0-G3", err)
	}
	defer rows.Close()

	var result []catalog.Module
	for rows.Next() {
		var item catalog.Module
		var enabled int
		if err := rows.Scan(&item.Code, &item.Name, &enabled, &item.RowVersion); err != nil {
			return nil, errx.Wrap(errx.CodeDatabase, "scan module", "P0-G3", err)
		}
		item.Enabled = enabled == 1
		result = append(result, item)
	}
	if err := rows.Err(); err != nil {
		return nil, errx.Wrap(errx.CodeDatabase, "iterate modules", "P0-G3", err)
	}
	return result, nil
}

func (r *CatalogRepository) ListFunctionCodes(ctx context.Context) ([]catalog.FunctionCode, error) {
	rows, err := r.db.QueryContext(ctx, `SELECT code, name, enabled, row_version FROM function_codes ORDER BY code`)
	if err != nil {
		return nil, errx.Wrap(errx.CodeDatabase, "list function codes", "P0-G3", err)
	}
	defer rows.Close()

	var result []catalog.FunctionCode
	for rows.Next() {
		var item catalog.FunctionCode
		var enabled int
		if err := rows.Scan(&item.Code, &item.Name, &enabled, &item.RowVersion); err != nil {
			return nil, errx.Wrap(errx.CodeDatabase, "scan function code", "P0-G3", err)
		}
		item.Enabled = enabled == 1
		result = append(result, item)
	}
	if err := rows.Err(); err != nil {
		return nil, errx.Wrap(errx.CodeDatabase, "iterate function codes", "P0-G3", err)
	}
	return result, nil
}

func (r *CatalogRepository) ListActions(ctx context.Context) ([]catalog.Action, error) {
	rows, err := r.db.QueryContext(ctx, `SELECT code, name, module_code, function_code, row_version FROM actions ORDER BY code`)
	if err != nil {
		return nil, errx.Wrap(errx.CodeDatabase, "list actions", "P0-G3", err)
	}
	defer rows.Close()

	var result []catalog.Action
	for rows.Next() {
		var item catalog.Action
		var functionCode sql.NullString
		if err := rows.Scan(&item.Code, &item.Name, &item.ModuleCode, &functionCode, &item.RowVersion); err != nil {
			return nil, errx.Wrap(errx.CodeDatabase, "scan action", "P0-G3", err)
		}
		if functionCode.Valid {
			value := functionCode.String
			item.FunctionCode = &value
		}
		result = append(result, item)
	}
	if err := rows.Err(); err != nil {
		return nil, errx.Wrap(errx.CodeDatabase, "iterate actions", "P0-G3", err)
	}
	return result, nil
}

func (r *CatalogRepository) Capability(ctx context.Context, actionCode string) (catalog.Capability, error) {
	var moduleEnabled int
	var functionEnabled sql.NullInt64
	err := r.db.QueryRowContext(ctx, `
		SELECT m.enabled, f.enabled
		FROM actions a
		JOIN modules m ON m.code = a.module_code
		LEFT JOIN function_codes f ON f.code = a.function_code
		WHERE a.code = ?`, actionCode).Scan(&moduleEnabled, &functionEnabled)
	if err == sql.ErrNoRows {
		return catalog.Capability{ActionCode: actionCode, Allowed: false, ReasonCode: "ACTION_NOT_FOUND"}, nil
	}
	if err != nil {
		return catalog.Capability{}, errx.Wrap(errx.CodeDatabase, "resolve action capability", "P0-G3", err)
	}
	if moduleEnabled != 1 {
		return catalog.Capability{ActionCode: actionCode, Allowed: false, ReasonCode: "MODULE_DISABLED"}, nil
	}
	if functionEnabled.Valid && functionEnabled.Int64 != 1 {
		return catalog.Capability{ActionCode: actionCode, Allowed: false, ReasonCode: "FUNCTION_DISABLED"}, nil
	}
	return catalog.Capability{ActionCode: actionCode, Allowed: true}, nil
}
