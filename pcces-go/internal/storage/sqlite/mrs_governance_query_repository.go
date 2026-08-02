package sqlite

import (
	"context"
	"database/sql"
	"encoding/json"
	"strings"

	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

// ListReleasesFiltered returns governance releases using optional Legacy-style
// status filtering. Empty status preserves the original all-release query.
func (r *MRSGovernanceRepository) ListReleasesFiltered(ctx context.Context, status string) ([]MRSCatalogRelease, error) {
	status = strings.ToUpper(strings.TrimSpace(status))
	if status != "" && status != "DRAFT" && status != "SUBMITTED" && status != "RETURNED" && status != "APPROVED" && status != "PUBLISHED" {
		return nil, errx.New(errx.CodeInvalidArgument, "invalid MRS release status", "P3-G-MRS-GOV")
	}
	query := `SELECT id FROM mrs_catalog_releases`
	args := []any{}
	if status != "" {
		query += ` WHERE status=?`
		args = append(args, status)
	}
	query += ` ORDER BY created_at DESC,id DESC`
	rows, err := r.store.db.QueryContext(ctx, query, args...)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	out := []MRSCatalogRelease{}
	for rows.Next() {
		var id string
		if err = rows.Scan(&id); err != nil {
			return nil, err
		}
		item, getErr := r.GetRelease(ctx, id)
		if getErr != nil {
			return nil, getErr
		}
		out = append(out, item)
	}
	return out, rows.Err()
}

// ListAuditFiltered supports the Legacy audit drill-down by resource and event.
// All filters are optional and combine with AND semantics.
func (r *MRSGovernanceRepository) ListAuditFiltered(ctx context.Context, resourceType, resourceID, eventType string) ([]MRSGovernanceAudit, error) {
	resourceType = strings.ToUpper(strings.TrimSpace(resourceType))
	eventType = strings.ToUpper(strings.TrimSpace(eventType))
	resourceID = strings.TrimSpace(resourceID)

	query := `SELECT id,event_type,resource_type,resource_id,actor_id,payload_json,created_at FROM mrs_governance_audit WHERE 1=1`
	args := []any{}
	if resourceType != "" {
		query += ` AND resource_type=?`
		args = append(args, resourceType)
	}
	if resourceID != "" {
		query += ` AND resource_id=?`
		args = append(args, resourceID)
	}
	if eventType != "" {
		query += ` AND event_type=?`
		args = append(args, eventType)
	}
	query += ` ORDER BY created_at DESC,id DESC`

	rows, err := r.store.db.QueryContext(ctx, query, args...)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	out := []MRSGovernanceAudit{}
	for rows.Next() {
		var item MRSGovernanceAudit
		var payload string
		if err = rows.Scan(&item.ID, &item.EventType, &item.ResourceType, &item.ResourceID, &item.ActorID, &payload, &item.CreatedAt); err != nil {
			return nil, err
		}
		if err = json.Unmarshal([]byte(payload), &item.Payload); err != nil {
			return nil, err
		}
		out = append(out, item)
	}
	return out, rows.Err()
}

// CountAuditFiltered is kept internal for deterministic tests and future paging.
func (r *MRSGovernanceRepository) CountAuditFiltered(ctx context.Context, resourceType, resourceID, eventType string) (int64, error) {
	resourceType = strings.ToUpper(strings.TrimSpace(resourceType))
	eventType = strings.ToUpper(strings.TrimSpace(eventType))
	resourceID = strings.TrimSpace(resourceID)
	query := `SELECT COUNT(*) FROM mrs_governance_audit WHERE 1=1`
	args := []any{}
	if resourceType != "" { query += ` AND resource_type=?`; args = append(args, resourceType) }
	if resourceID != "" { query += ` AND resource_id=?`; args = append(args, resourceID) }
	if eventType != "" { query += ` AND event_type=?`; args = append(args, eventType) }
	var count int64
	err := r.store.db.QueryRowContext(ctx, query, args...).Scan(&count)
	if err == sql.ErrNoRows { return 0, nil }
	return count, err
}
