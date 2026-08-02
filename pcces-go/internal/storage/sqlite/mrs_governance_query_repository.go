package sqlite

import (
	"context"
	"encoding/json"
	"strings"

	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

const mrsGovernanceMaxPageSize = 200

type MRSCatalogReleasePage struct {
	Items  []MRSCatalogRelease `json:"items"`
	Total  int64               `json:"total"`
	Limit  int                 `json:"limit"`
	Offset int                 `json:"offset"`
}

type MRSGovernanceAuditPage struct {
	Items  []MRSGovernanceAudit `json:"items"`
	Total  int64                `json:"total"`
	Limit  int                  `json:"limit"`
	Offset int                  `json:"offset"`
}

func normalizeGovernancePage(limit, offset int) (int, int) {
	if limit <= 0 {
		limit = 50
	}
	if limit > mrsGovernanceMaxPageSize {
		limit = mrsGovernanceMaxPageSize
	}
	if offset < 0 {
		offset = 0
	}
	return limit, offset
}

func normalizeReleaseStatus(status string) (string, error) {
	status = strings.ToUpper(strings.TrimSpace(status))
	if status == "" {
		return "", nil
	}
	switch status {
	case "DRAFT", "SUBMITTED", "RETURNED", "APPROVED", "PUBLISHED":
		return status, nil
	default:
		return "", errx.New(errx.CodeInvalidArgument, "invalid MRS release status", "P3-G-MRS-GOV")
	}
}

func (r *MRSGovernanceRepository) QueryReleases(ctx context.Context, status string, limit, offset int) (MRSCatalogReleasePage, error) {
	limit, offset = normalizeGovernancePage(limit, offset)
	status, err := normalizeReleaseStatus(status)
	if err != nil {
		return MRSCatalogReleasePage{}, err
	}
	where, args := "", []any{}
	if status != "" {
		where = " WHERE status=?"
		args = append(args, status)
	}
	var total int64
	if err = r.store.db.QueryRowContext(ctx, "SELECT COUNT(*) FROM mrs_catalog_releases"+where, args...).Scan(&total); err != nil {
		return MRSCatalogReleasePage{}, err
	}
	queryArgs := append(append([]any{}, args...), limit, offset)
	rows, err := r.store.db.QueryContext(ctx, "SELECT id FROM mrs_catalog_releases"+where+" ORDER BY created_at DESC,id DESC LIMIT ? OFFSET ?", queryArgs...)
	if err != nil {
		return MRSCatalogReleasePage{}, err
	}
	defer rows.Close()
	items := make([]MRSCatalogRelease, 0)
	for rows.Next() {
		var id string
		if err = rows.Scan(&id); err != nil {
			return MRSCatalogReleasePage{}, err
		}
		item, getErr := r.GetRelease(ctx, id)
		if getErr != nil {
			return MRSCatalogReleasePage{}, getErr
		}
		items = append(items, item)
	}
	if err = rows.Err(); err != nil {
		return MRSCatalogReleasePage{}, err
	}
	return MRSCatalogReleasePage{Items: items, Total: total, Limit: limit, Offset: offset}, nil
}

func (r *MRSGovernanceRepository) QueryAudit(ctx context.Context, resourceType, resourceID, eventType string, limit, offset int) (MRSGovernanceAuditPage, error) {
	limit, offset = normalizeGovernancePage(limit, offset)
	resourceType = strings.ToUpper(strings.TrimSpace(resourceType))
	resourceID = strings.TrimSpace(resourceID)
	eventType = strings.ToUpper(strings.TrimSpace(eventType))
	where := " WHERE 1=1"
	args := make([]any, 0, 3)
	if resourceType != "" {
		where += " AND resource_type=?"
		args = append(args, resourceType)
	}
	if resourceID != "" {
		where += " AND resource_id=?"
		args = append(args, resourceID)
	}
	if eventType != "" {
		where += " AND event_type=?"
		args = append(args, eventType)
	}
	var total int64
	if err := r.store.db.QueryRowContext(ctx, "SELECT COUNT(*) FROM mrs_governance_audit"+where, args...).Scan(&total); err != nil {
		return MRSGovernanceAuditPage{}, err
	}
	queryArgs := append(append([]any{}, args...), limit, offset)
	rows, err := r.store.db.QueryContext(ctx, "SELECT id,event_type,resource_type,resource_id,actor_id,payload_json,created_at FROM mrs_governance_audit"+where+" ORDER BY created_at DESC,id DESC LIMIT ? OFFSET ?", queryArgs...)
	if err != nil {
		return MRSGovernanceAuditPage{}, err
	}
	defer rows.Close()
	items := make([]MRSGovernanceAudit, 0)
	for rows.Next() {
		var item MRSGovernanceAudit
		var payload string
		if err = rows.Scan(&item.ID, &item.EventType, &item.ResourceType, &item.ResourceID, &item.ActorID, &payload, &item.CreatedAt); err != nil {
			return MRSGovernanceAuditPage{}, err
		}
		if err = json.Unmarshal([]byte(payload), &item.Payload); err != nil {
			return MRSGovernanceAuditPage{}, err
		}
		items = append(items, item)
	}
	if err = rows.Err(); err != nil {
		return MRSGovernanceAuditPage{}, err
	}
	return MRSGovernanceAuditPage{Items: items, Total: total, Limit: limit, Offset: offset}, nil
}

func (r *MRSGovernanceRepository) ListReleasesFiltered(ctx context.Context, status string) ([]MRSCatalogRelease, error) {
	page, err := r.QueryReleases(ctx, status, mrsGovernanceMaxPageSize, 0)
	return page.Items, err
}

func (r *MRSGovernanceRepository) ListAuditFiltered(ctx context.Context, resourceType, resourceID, eventType string) ([]MRSGovernanceAudit, error) {
	page, err := r.QueryAudit(ctx, resourceType, resourceID, eventType, mrsGovernanceMaxPageSize, 0)
	return page.Items, err
}
