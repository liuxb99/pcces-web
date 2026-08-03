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

type MRSCatalogRelease struct {
	ID, Label, Status, CreatedBy, CreatedAt, UpdatedAt, DeepLink string
	ReviewedBy, ReviewComment                                    *string
	RowVersion                                                   int64
	Snapshot                                                     []MRSCatalogItem
}
type MRSValidity struct {
	CatalogItemID        string
	ValidFrom, ValidTo   *string
	Status               string
	RowVersion           int64
	UpdatedBy, UpdatedAt string
}
type MRSRecipeFreeze struct {
	RecipeID, VersionID  string
	Frozen               bool
	Reason               *string
	RowVersion           int64
	UpdatedBy, UpdatedAt string
}
type MRSGovernanceAudit struct {
	ID, EventType, ResourceType, ResourceID, ActorID, CreatedAt string
	Payload                                                     map[string]any
}
type MRSGovernanceRepository struct{ store *Store }

func NewMRSGovernanceRepository(store *Store) *MRSGovernanceRepository {
	return &MRSGovernanceRepository{store: store}
}

func (r *MRSGovernanceRepository) CreateRelease(ctx context.Context, id, label, actor string) (MRSCatalogRelease, error) {
	rows, err := r.store.db.QueryContext(ctx, `SELECT id,code,name,category,unit,current_price,price_scale,source,enabled,row_version,created_at,updated_at FROM mrs_catalog_items ORDER BY code`)
	if err != nil {
		return MRSCatalogRelease{}, err
	}
	defer rows.Close()
	var items []MRSCatalogItem
	for rows.Next() {
		var v MRSCatalogItem
		if err = rows.Scan(&v.ID, &v.Code, &v.Name, &v.Category, &v.Unit, &v.CurrentPrice, &v.PriceScale, &v.Source, &v.Enabled, &v.RowVersion, &v.CreatedAt, &v.UpdatedAt); err != nil {
			return MRSCatalogRelease{}, err
		}
		items = append(items, v)
	}
	if err = rows.Err(); err != nil {
		return MRSCatalogRelease{}, err
	}
	payload, err := json.Marshal(items)
	if err != nil {
		return MRSCatalogRelease{}, err
	}
	now := time.Now().UTC().Format(time.RFC3339Nano)
	if label == "" {
		label = "MRS Catalog Release"
	}
	tx, err := r.store.db.BeginTx(ctx, nil)
	if err != nil {
		return MRSCatalogRelease{}, err
	}
	defer tx.Rollback()
	if _, err = tx.ExecContext(ctx, `INSERT INTO mrs_catalog_releases(id,label,status,snapshot_json,created_by,created_at,updated_at,row_version) VALUES(?,?, 'DRAFT', ?, ?, ?, ?, 1)`, id, label, string(payload), actor, now, now); err != nil {
		return MRSCatalogRelease{}, err
	}
	if err = r.audit(ctx, tx, "RELEASE_CREATED", "CATALOG_RELEASE", id, actor, map[string]any{"item_count": len(items)}); err != nil {
		return MRSCatalogRelease{}, err
	}
	if err = tx.Commit(); err != nil {
		return MRSCatalogRelease{}, err
	}
	return r.GetRelease(ctx, id)
}
func (r *MRSGovernanceRepository) GetRelease(ctx context.Context, id string) (MRSCatalogRelease, error) {
	var v MRSCatalogRelease
	var snapshot string
	err := r.store.db.QueryRowContext(ctx, `SELECT id,label,status,snapshot_json,created_by,reviewed_by,review_comment,created_at,updated_at,row_version FROM mrs_catalog_releases WHERE id=?`, id).Scan(&v.ID, &v.Label, &v.Status, &snapshot, &v.CreatedBy, &v.ReviewedBy, &v.ReviewComment, &v.CreatedAt, &v.UpdatedAt, &v.RowVersion)
	if err == sql.ErrNoRows {
		return v, errx.New(errx.CodeNotFound, "MRS release not found", "P3-G-MRS-GOV")
	}
	if err != nil {
		return v, err
	}
	if err = json.Unmarshal([]byte(snapshot), &v.Snapshot); err != nil {
		return v, err
	}
	v.DeepLink = fmt.Sprintf("/app/mrs-governance?release=%s", id)
	return v, nil
}
func (r *MRSGovernanceRepository) ListReleases(ctx context.Context) ([]MRSCatalogRelease, error) {
	rows, err := r.store.db.QueryContext(ctx, `SELECT id FROM mrs_catalog_releases ORDER BY created_at DESC,id DESC`)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	var out []MRSCatalogRelease
	for rows.Next() {
		var id string
		if err = rows.Scan(&id); err != nil {
			return nil, err
		}
		v, getErr := r.GetRelease(ctx, id)
		if getErr != nil {
			return nil, getErr
		}
		out = append(out, v)
	}
	return out, rows.Err()
}
func (r *MRSGovernanceRepository) TransitionRelease(ctx context.Context, id, command, actor, comment string, rowVersion int64) (MRSCatalogRelease, error) {
	v, err := r.GetRelease(ctx, id)
	if err != nil {
		return v, err
	}
	command = strings.ToUpper(strings.TrimSpace(command))
	next := map[string]map[string]string{"DRAFT": {"SUBMIT": "SUBMITTED"}, "SUBMITTED": {"APPROVE": "APPROVED", "RETURN": "RETURNED"}, "RETURNED": {"SUBMIT": "SUBMITTED"}, "APPROVED": {"PUBLISH": "PUBLISHED"}}[v.Status][command]
	if next == "" {
		return v, errx.New(errx.CodeInvalidArgument, "invalid MRS release transition", "P3-G-MRS-GOV")
	}
	if v.RowVersion != rowVersion {
		return v, errx.New(errx.CodeConflict, "MRS release conflict", "P3-G-MRS-GOV")
	}
	now := time.Now().UTC().Format(time.RFC3339Nano)
	tx, err := r.store.db.BeginTx(ctx, nil)
	if err != nil {
		return v, err
	}
	defer tx.Rollback()
	reviewed := v.ReviewedBy
	if command == "APPROVE" || command == "RETURN" || command == "PUBLISH" {
		reviewed = &actor
	}
	res, err := tx.ExecContext(ctx, `UPDATE mrs_catalog_releases SET status=?,reviewed_by=?,review_comment=?,updated_at=?,row_version=row_version+1 WHERE id=? AND row_version=?`, next, reviewed, nullable(comment), now, id, rowVersion)
	if err != nil {
		return v, err
	}
	n, _ := res.RowsAffected()
	if n != 1 {
		return v, errx.New(errx.CodeConflict, "MRS release conflict", "P3-G-MRS-GOV")
	}
	if err = r.audit(ctx, tx, "RELEASE_"+command, "CATALOG_RELEASE", id, actor, map[string]any{"from": v.Status, "to": next, "comment": comment}); err != nil {
		return v, err
	}
	if err = tx.Commit(); err != nil {
		return v, err
	}
	return r.GetRelease(ctx, id)
}
func (r *MRSGovernanceRepository) SetValidity(ctx context.Context, item string, validFrom, validTo *string, status, actor string, rowVersion int64) (MRSValidity, error) {
	status = strings.ToUpper(strings.TrimSpace(status))
	if status == "" {
		status = "ACTIVE"
	}
	if status != "ACTIVE" && status != "SUSPENDED" && status != "EXPIRED" {
		return MRSValidity{}, errx.New(errx.CodeInvalidArgument, "invalid MRS validity status", "P3-G-MRS-GOV")
	}
	if validFrom != nil && validTo != nil && *validFrom > *validTo {
		return MRSValidity{}, errx.New(errx.CodeInvalidArgument, "valid_from must not be after valid_to", "P3-G-MRS-GOV")
	}
	var exists string
	if err := r.store.db.QueryRowContext(ctx, `SELECT id FROM mrs_catalog_items WHERE id=?`, item).Scan(&exists); err != nil {
		return MRSValidity{}, errx.New(errx.CodeNotFound, "MRS item not found", "P3-G-MRS-GOV")
	}
	now := time.Now().UTC().Format(time.RFC3339Nano)
	tx, err := r.store.db.BeginTx(ctx, nil)
	if err != nil {
		return MRSValidity{}, err
	}
	defer tx.Rollback()
	var current int64
	err = tx.QueryRowContext(ctx, `SELECT row_version FROM mrs_item_validity WHERE catalog_item_id=?`, item).Scan(&current)
	if err == sql.ErrNoRows {
		if rowVersion != 0 {
			return MRSValidity{}, errx.New(errx.CodeConflict, "MRS validity conflict", "P3-G-MRS-GOV")
		}
		_, err = tx.ExecContext(ctx, `INSERT INTO mrs_item_validity(catalog_item_id,valid_from,valid_to,status,row_version,updated_by,updated_at) VALUES(?,?,?,?,1,?,?)`, item, validFrom, validTo, status, actor, now)
	} else if err == nil {
		if current != rowVersion {
			return MRSValidity{}, errx.New(errx.CodeConflict, "MRS validity conflict", "P3-G-MRS-GOV")
		}
		var res sql.Result
		res, err = tx.ExecContext(ctx, `UPDATE mrs_item_validity SET valid_from=?,valid_to=?,status=?,row_version=row_version+1,updated_by=?,updated_at=? WHERE catalog_item_id=? AND row_version=?`, validFrom, validTo, status, actor, now, item, rowVersion)
		if err == nil {
			n, _ := res.RowsAffected()
			if n != 1 {
				return MRSValidity{}, errx.New(errx.CodeConflict, "MRS validity conflict", "P3-G-MRS-GOV")
			}
		}
	} else {
		return MRSValidity{}, err
	}
	if err != nil {
		return MRSValidity{}, err
	}
	if err = r.audit(ctx, tx, "ITEM_VALIDITY_SET", "CATALOG_ITEM", item, actor, map[string]any{"valid_from": validFrom, "valid_to": validTo, "status": status}); err != nil {
		return MRSValidity{}, err
	}
	if err = tx.Commit(); err != nil {
		return MRSValidity{}, err
	}
	return r.GetValidity(ctx, item)
}
func (r *MRSGovernanceRepository) GetValidity(ctx context.Context, item string) (MRSValidity, error) {
	var v MRSValidity
	err := r.store.db.QueryRowContext(ctx, `SELECT catalog_item_id,valid_from,valid_to,status,row_version,updated_by,updated_at FROM mrs_item_validity WHERE catalog_item_id=?`, item).Scan(&v.CatalogItemID, &v.ValidFrom, &v.ValidTo, &v.Status, &v.RowVersion, &v.UpdatedBy, &v.UpdatedAt)
	if err == sql.ErrNoRows {
		return MRSValidity{CatalogItemID: item, Status: "ACTIVE"}, nil
	}
	return v, err
}
func (r *MRSGovernanceRepository) ExpiryAlerts(ctx context.Context, asOf string) ([]map[string]any, error) {
	if asOf == "" {
		asOf = time.Now().UTC().Format("2006-01-02")
	}
	rows, err := r.store.db.QueryContext(ctx, `SELECT v.catalog_item_id,i.code,i.name,v.status,v.valid_to FROM mrs_item_validity v JOIN mrs_catalog_items i ON i.id=v.catalog_item_id WHERE v.status IN ('SUSPENDED','EXPIRED') OR (v.valid_to IS NOT NULL AND v.valid_to < ?) ORDER BY i.code`, asOf)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	var out []map[string]any
	for rows.Next() {
		var id, code, name, status string
		var validTo sql.NullString
		if err = rows.Scan(&id, &code, &name, &status, &validTo); err != nil {
			return nil, err
		}
		if validTo.Valid && validTo.String < asOf {
			status = "EXPIRED"
		}
		out = append(out, map[string]any{"catalog_item_id": id, "code": code, "name": name, "status": status, "valid_to": nullString(validTo.String)})
	}
	return out, rows.Err()
}
func (r *MRSGovernanceRepository) SetRecipeFreeze(ctx context.Context, recipe, version string, frozen bool, reason *string, actor string, rowVersion int64) (MRSRecipeFreeze, error) {
	var ok string
	if err := r.store.db.QueryRowContext(ctx, `SELECT id FROM mrs_recipe_versions WHERE id=? AND recipe_id=?`, version, recipe).Scan(&ok); err != nil {
		return MRSRecipeFreeze{}, errx.New(errx.CodeNotFound, "MRS recipe version not found", "P3-G-MRS-GOV")
	}
	now := time.Now().UTC().Format(time.RFC3339Nano)
	tx, err := r.store.db.BeginTx(ctx, nil)
	if err != nil {
		return MRSRecipeFreeze{}, err
	}
	defer tx.Rollback()
	var current int64
	err = tx.QueryRowContext(ctx, `SELECT row_version FROM mrs_recipe_freezes WHERE recipe_id=?`, recipe).Scan(&current)
	if err == sql.ErrNoRows {
		if rowVersion != 0 {
			return MRSRecipeFreeze{}, errx.New(errx.CodeConflict, "MRS recipe freeze conflict", "P3-G-MRS-GOV")
		}
		_, err = tx.ExecContext(ctx, `INSERT INTO mrs_recipe_freezes(recipe_id,version_id,frozen,reason,row_version,updated_by,updated_at) VALUES(?,?,?,?,1,?,?)`, recipe, version, frozen, reason, actor, now)
	} else if err == nil {
		if current != rowVersion {
			return MRSRecipeFreeze{}, errx.New(errx.CodeConflict, "MRS recipe freeze conflict", "P3-G-MRS-GOV")
		}
		var res sql.Result
		res, err = tx.ExecContext(ctx, `UPDATE mrs_recipe_freezes SET version_id=?,frozen=?,reason=?,row_version=row_version+1,updated_by=?,updated_at=? WHERE recipe_id=? AND row_version=?`, version, frozen, reason, actor, now, recipe, rowVersion)
		if err == nil {
			n, _ := res.RowsAffected()
			if n != 1 {
				return MRSRecipeFreeze{}, errx.New(errx.CodeConflict, "MRS recipe freeze conflict", "P3-G-MRS-GOV")
			}
		}
	} else {
		return MRSRecipeFreeze{}, err
	}
	if err != nil {
		return MRSRecipeFreeze{}, err
	}
	if err = r.audit(ctx, tx, "RECIPE_FREEZE_SET", "ANALYSIS_RECIPE", recipe, actor, map[string]any{"version_id": version, "frozen": frozen, "reason": reason}); err != nil {
		return MRSRecipeFreeze{}, err
	}
	if err = tx.Commit(); err != nil {
		return MRSRecipeFreeze{}, err
	}
	return r.GetRecipeFreeze(ctx, recipe)
}
func (r *MRSGovernanceRepository) GetRecipeFreeze(ctx context.Context, recipe string) (MRSRecipeFreeze, error) {
	var v MRSRecipeFreeze
	err := r.store.db.QueryRowContext(ctx, `SELECT recipe_id,version_id,frozen,reason,row_version,updated_by,updated_at FROM mrs_recipe_freezes WHERE recipe_id=?`, recipe).Scan(&v.RecipeID, &v.VersionID, &v.Frozen, &v.Reason, &v.RowVersion, &v.UpdatedBy, &v.UpdatedAt)
	if err == sql.ErrNoRows {
		return MRSRecipeFreeze{RecipeID: recipe}, nil
	}
	return v, err
}
func (r *MRSGovernanceRepository) ListAudit(ctx context.Context) ([]MRSGovernanceAudit, error) {
	rows, err := r.store.db.QueryContext(ctx, `SELECT id,event_type,resource_type,resource_id,actor_id,payload_json,created_at FROM mrs_governance_audit ORDER BY created_at DESC,id DESC`)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	var out []MRSGovernanceAudit
	for rows.Next() {
		var v MRSGovernanceAudit
		var payload string
		if err = rows.Scan(&v.ID, &v.EventType, &v.ResourceType, &v.ResourceID, &v.ActorID, &payload, &v.CreatedAt); err != nil {
			return nil, err
		}
		if err = json.Unmarshal([]byte(payload), &v.Payload); err != nil {
			return nil, err
		}
		out = append(out, v)
	}
	return out, rows.Err()
}

func (r *MRSGovernanceRepository) CountAuditFiltered(ctx context.Context, eventType, resourceID, actor string) (int, error) {
	var count int
	query := `SELECT COUNT(1) FROM mrs_governance_audit WHERE 1=1`
	args := []any{}
	if eventType != "" {
		query += ` AND event_type=?`
		args = append(args, eventType)
	}
	if resourceID != "" {
		query += ` AND resource_id=?`
		args = append(args, resourceID)
	}
	if actor != "" {
		query += ` AND actor_id=?`
		args = append(args, actor)
	}
	if err := r.store.db.QueryRowContext(ctx, query, args...).Scan(&count); err != nil {
		return 0, err
	}
	return count, nil
}

func (r *MRSGovernanceRepository) audit(ctx context.Context, tx *sql.Tx, event, resourceType, resourceID, actor string, payload any) error {
	b, err := json.Marshal(payload)
	if err != nil {
		return err
	}
	_, err = tx.ExecContext(ctx, `INSERT INTO mrs_governance_audit(id,event_type,resource_type,resource_id,actor_id,payload_json,created_at) VALUES(?,?,?,?,?,?,?)`, fmt.Sprintf("mrsa-%d", time.Now().UnixNano()), event, resourceType, resourceID, actor, string(b), time.Now().UTC().Format(time.RFC3339Nano))
	return err
}
func nullable(s string) any {
	if s == "" {
		return nil
	}
	return s
}
