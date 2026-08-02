package sqlite

import (
	"context"
	"database/sql"
	"strings"
	"time"

	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

type CostStructureType struct {
	ID          string `json:"id"`
	Code        string `json:"code"`
	Name        string `json:"name"`
	Description string `json:"description"`
	Source      string `json:"source"`
	Version     string `json:"version"`
	Enabled     bool   `json:"enabled"`
	CreatedBy   string `json:"created_by"`
	CreatedAt   string `json:"created_at"`
	UpdatedAt   string `json:"updated_at"`
	RowVersion  int64  `json:"row_version"`
}

type ProjectCostStructure struct {
	ProjectCode         string `json:"project_code"`
	CostStructureTypeID string `json:"cost_structure_type_id"`
	Issue               string `json:"issue"`
	AssignedBy          string `json:"assigned_by"`
	AssignedAt          string `json:"assigned_at"`
	RowVersion          int64  `json:"row_version"`
	TypeCode            string `json:"type_code"`
	TypeName            string `json:"type_name"`
	TypeVersion         string `json:"type_version"`
	DeepLink            string `json:"deep_link"`
}

type CostStructureRepository struct {
	store *Store
}

func NewCostStructureRepository(store *Store) *CostStructureRepository {
	return &CostStructureRepository{store: store}
}

func (r *CostStructureRepository) ListTypes(ctx context.Context, enabledOnly bool) ([]CostStructureType, error) {
	query := `SELECT id, code, name, description, source, version, enabled, created_by, created_at, updated_at, row_version FROM cost_structure_types`
	if enabledOnly {
		query += ` WHERE enabled = 1`
	}
	query += ` ORDER BY code`

	rows, err := r.store.db.QueryContext(ctx, query)
	if err != nil {
		return nil, err
	}
	defer rows.Close()

	items := []CostStructureType{}
	for rows.Next() {
		var item CostStructureType
		var enabled int
		if err := rows.Scan(
			&item.ID, &item.Code, &item.Name, &item.Description, &item.Source,
			&item.Version, &enabled, &item.CreatedBy, &item.CreatedAt,
			&item.UpdatedAt, &item.RowVersion,
		); err != nil {
			return nil, err
		}
		item.Enabled = enabled != 0
		items = append(items, item)
	}
	return items, rows.Err()
}

func (r *CostStructureRepository) SaveType(ctx context.Context, item CostStructureType) (CostStructureType, error) {
	item.ID = strings.TrimSpace(item.ID)
	item.Code = strings.ToUpper(strings.TrimSpace(item.Code))
	item.Name = strings.TrimSpace(item.Name)
	if item.ID == "" || item.Code == "" || item.Name == "" {
		return CostStructureType{}, errx.New(errx.CodeInvalidArgument, "id, code and name are required", "P4-COST-001")
	}
	if item.Source == "" {
		item.Source = "LEGACY"
	}
	item.Source = strings.ToUpper(strings.TrimSpace(item.Source))
	if item.Version == "" {
		item.Version = "1"
	}

	now := time.Now().UTC().Format(time.RFC3339Nano)
	tx, err := r.store.db.BeginTx(ctx, nil)
	if err != nil {
		return CostStructureType{}, err
	}
	defer tx.Rollback()

	var current int64
	err = tx.QueryRowContext(ctx, `SELECT row_version FROM cost_structure_types WHERE id = ?`, item.ID).Scan(&current)
	switch {
	case err == sql.ErrNoRows:
		if item.RowVersion != 0 {
			return CostStructureType{}, errx.New(errx.CodeConflict, "stale row_version", "P4-COST-001")
		}
		_, err = tx.ExecContext(ctx, `
			INSERT INTO cost_structure_types
			(id, code, name, description, source, version, enabled, created_by, created_at, updated_at, row_version)
			VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, 1)`,
			item.ID, item.Code, item.Name, item.Description, item.Source, item.Version,
			costStructureBoolInt(item.Enabled), item.CreatedBy, now, now,
		)
	case err != nil:
		return CostStructureType{}, err
	default:
		if current != item.RowVersion {
			return CostStructureType{}, errx.New(errx.CodeConflict, "stale row_version", "P4-COST-001")
		}
		var result sql.Result
		result, err = tx.ExecContext(ctx, `
			UPDATE cost_structure_types
			SET code = ?, name = ?, description = ?, source = ?, version = ?, enabled = ?, updated_at = ?, row_version = row_version + 1
			WHERE id = ? AND row_version = ?`,
			item.Code, item.Name, item.Description, item.Source, item.Version,
			costStructureBoolInt(item.Enabled), now, item.ID, item.RowVersion,
		)
		if err == nil {
			rowsAffected, rowsErr := result.RowsAffected()
			if rowsErr != nil {
				return CostStructureType{}, rowsErr
			}
			if rowsAffected != 1 {
				return CostStructureType{}, errx.New(errx.CodeConflict, "stale row_version", "P4-COST-001")
			}
		}
	}
	if err != nil {
		return CostStructureType{}, err
	}
	if err := tx.Commit(); err != nil {
		return CostStructureType{}, err
	}
	return r.GetType(ctx, item.ID)
}

func (r *CostStructureRepository) GetType(ctx context.Context, id string) (CostStructureType, error) {
	var item CostStructureType
	var enabled int
	err := r.store.db.QueryRowContext(ctx, `
		SELECT id, code, name, description, source, version, enabled, created_by, created_at, updated_at, row_version
		FROM cost_structure_types WHERE id = ?`, id,
	).Scan(
		&item.ID, &item.Code, &item.Name, &item.Description, &item.Source,
		&item.Version, &enabled, &item.CreatedBy, &item.CreatedAt,
		&item.UpdatedAt, &item.RowVersion,
	)
	if err == sql.ErrNoRows {
		return item, errx.New(errx.CodeNotFound, "cost structure type not found", "P4-COST-001")
	}
	if err != nil {
		return item, err
	}
	item.Enabled = enabled != 0
	return item, nil
}

func (r *CostStructureRepository) AssignProject(
	ctx context.Context,
	projectCode string,
	typeID string,
	issue string,
	actor string,
	rowVersion int64,
) (ProjectCostStructure, error) {
	projectCode = strings.TrimSpace(projectCode)
	typeID = strings.TrimSpace(typeID)
	issue = strings.ToUpper(strings.TrimSpace(issue))
	if issue == "" {
		issue = "BUD"
	}
	if projectCode == "" || typeID == "" {
		return ProjectCostStructure{}, errx.New(errx.CodeInvalidArgument, "project_code and cost_structure_type_id are required", "P4-COST-003")
	}
	if issue != "BUD" && issue != "BID" {
		return ProjectCostStructure{}, errx.New(errx.CodeInvalidArgument, "issue must be BUD or BID", "P4-COST-003")
	}

	tx, err := r.store.db.BeginTx(ctx, nil)
	if err != nil {
		return ProjectCostStructure{}, err
	}
	defer tx.Rollback()

	var enabled int
	err = tx.QueryRowContext(ctx, `SELECT enabled FROM cost_structure_types WHERE id = ?`, typeID).Scan(&enabled)
	if err == sql.ErrNoRows || enabled == 0 {
		return ProjectCostStructure{}, errx.New(errx.CodeNotFound, "enabled cost structure type not found", "P4-COST-003")
	}
	if err != nil {
		return ProjectCostStructure{}, err
	}

	var current int64
	err = tx.QueryRowContext(ctx, `SELECT row_version FROM project_cost_structures WHERE project_code = ?`, projectCode).Scan(&current)
	now := time.Now().UTC().Format(time.RFC3339Nano)
	switch {
	case err == sql.ErrNoRows:
		if rowVersion != 0 {
			return ProjectCostStructure{}, errx.New(errx.CodeConflict, "stale row_version", "P4-COST-003")
		}
		_, err = tx.ExecContext(ctx, `
			INSERT INTO project_cost_structures
			(project_code, cost_structure_type_id, issue, assigned_by, assigned_at, row_version)
			VALUES (?, ?, ?, ?, ?, 1)`, projectCode, typeID, issue, actor, now)
	case err != nil:
		return ProjectCostStructure{}, err
	default:
		if current != rowVersion {
			return ProjectCostStructure{}, errx.New(errx.CodeConflict, "stale row_version", "P4-COST-003")
		}
		var result sql.Result
		result, err = tx.ExecContext(ctx, `
			UPDATE project_cost_structures
			SET cost_structure_type_id = ?, issue = ?, assigned_by = ?, assigned_at = ?, row_version = row_version + 1
			WHERE project_code = ? AND row_version = ?`, typeID, issue, actor, now, projectCode, rowVersion)
		if err == nil {
			rowsAffected, rowsErr := result.RowsAffected()
			if rowsErr != nil {
				return ProjectCostStructure{}, rowsErr
			}
			if rowsAffected != 1 {
				return ProjectCostStructure{}, errx.New(errx.CodeConflict, "stale row_version", "P4-COST-003")
			}
		}
	}
	if err != nil {
		return ProjectCostStructure{}, err
	}
	if err := tx.Commit(); err != nil {
		return ProjectCostStructure{}, err
	}
	return r.GetProject(ctx, projectCode)
}

func (r *CostStructureRepository) GetProject(ctx context.Context, projectCode string) (ProjectCostStructure, error) {
	var item ProjectCostStructure
	err := r.store.db.QueryRowContext(ctx, `
		SELECT p.project_code, p.cost_structure_type_id, p.issue, p.assigned_by, p.assigned_at,
		       p.row_version, t.code, t.name, t.version
		FROM project_cost_structures p
		JOIN cost_structure_types t ON t.id = p.cost_structure_type_id
		WHERE p.project_code = ?`, projectCode,
	).Scan(
		&item.ProjectCode, &item.CostStructureTypeID, &item.Issue, &item.AssignedBy,
		&item.AssignedAt, &item.RowVersion, &item.TypeCode, &item.TypeName, &item.TypeVersion,
	)
	if err == sql.ErrNoRows {
		return item, errx.New(errx.CodeNotFound, "project cost structure not found", "P4-COST-003")
	}
	if err != nil {
		return item, err
	}
	item.DeepLink = "/app/cost-structure?project=" + projectCode
	return item, nil
}

func costStructureBoolInt(value bool) int {
	if value {
		return 1
	}
	return 0
}
