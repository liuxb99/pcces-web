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

type BidImportSession struct {
	ID                        string          `json:"id"`
	SourceFormat              string          `json:"source_format"`
	FormatVersion             string          `json:"format_version"`
	SourceBidProjectCode      string          `json:"source_bid_project_code"`
	TargetBudgetProjectCode   string          `json:"target_budget_project_code"`
	SourceConversionSessionID string          `json:"source_conversion_session_id,omitempty"`
	Status                    string          `json:"status"`
	Report                    BidImportReport `json:"report"`
	Items                     []BidImportItem `json:"items"`
	CreatedBy                 string          `json:"created_by"`
	CreatedAt                 string          `json:"created_at"`
	RowVersion                int64           `json:"row_version"`
	RoundTripLineage          map[string]any  `json:"round_trip_lineage"`
	DeepLink                  string          `json:"deep_link"`
}

type BidImportSessionRequest struct {
	Payload                   string `json:"payload"`
	Format                    string `json:"format"`
	TargetBudgetProjectCode   string `json:"target_budget_project_code"`
	SourceConversionSessionID string `json:"source_conversion_session_id"`
	ActorID                   string `json:"actor_id"`
}

type BidImportSessionRepository struct{ store *Store }

func NewBidImportSessionRepository(store *Store) *BidImportSessionRepository {
	return &BidImportSessionRepository{store: store}
}

func (r *BidImportSessionRepository) Create(ctx context.Context, req BidImportSessionRequest) (BidImportSession, error) {
	target := strings.TrimSpace(req.TargetBudgetProjectCode)
	if target == "" {
		return BidImportSession{}, errx.New(errx.CodeInvalidArgument, "target budget project is required", "P4-IMPORT-001")
	}
	parsed, err := ParseBidImport(req.Payload, req.Format)
	if err != nil {
		return BidImportSession{}, errx.Wrap(errx.CodeInvalidArgument, "parse electronic bid", "P4-IMPORT-001", err)
	}
	status := "BLOCKED"
	if parsed.Report.CanContinue {
		status = "READY"
	}
	reportJSON, _ := json.Marshal(parsed.Report)
	itemsJSON, _ := json.Marshal(parsed.Items)
	id := fmt.Sprintf("IMP-%d", time.Now().UTC().UnixNano())
	now := time.Now().UTC().Format(time.RFC3339Nano)
	_, err = r.store.db.ExecContext(ctx, `INSERT INTO bid_import_sessions(id,source_format,format_version,source_bid_project_code,target_budget_project_code,source_conversion_session_id,status,report_json,items_json,created_by,created_at,row_version) VALUES(?,?,?,?,?,?,?,?,?,?,?,1)`,
		id, parsed.Format, parsed.FormatVersion, parsed.SourceBidProjectCode, target, nullIfBlank(req.SourceConversionSessionID), status, string(reportJSON), string(itemsJSON), strings.TrimSpace(req.ActorID), now)
	if err != nil {
		return BidImportSession{}, err
	}
	return r.Get(ctx, id)
}

func (r *BidImportSessionRepository) Get(ctx context.Context, id string) (BidImportSession, error) {
	var item BidImportSession
	var reportJSON, itemsJSON string
	var conversionID sql.NullString
	err := r.store.db.QueryRowContext(ctx, `SELECT id,source_format,format_version,source_bid_project_code,target_budget_project_code,source_conversion_session_id,status,report_json,items_json,created_by,created_at,row_version FROM bid_import_sessions WHERE id=?`, id).
		Scan(&item.ID, &item.SourceFormat, &item.FormatVersion, &item.SourceBidProjectCode, &item.TargetBudgetProjectCode, &conversionID, &item.Status, &reportJSON, &itemsJSON, &item.CreatedBy, &item.CreatedAt, &item.RowVersion)
	if err == sql.ErrNoRows {
		return item, errx.New(errx.CodeNotFound, "bid import session not found", "P4-IMPORT-001")
	}
	if err != nil {
		return item, err
	}
	item.SourceConversionSessionID = conversionID.String
	_ = json.Unmarshal([]byte(reportJSON), &item.Report)
	_ = json.Unmarshal([]byte(itemsJSON), &item.Items)
	links := make([]map[string]string, 0, len(item.Items))
	for _, row := range item.Items {
		sourceID := strings.TrimSpace(row.SourceBudgetItemID)
		if sourceID == "" {
			sourceID = strings.TrimSpace(row.ID)
		}
		links = append(links, map[string]string{"source_budget_item_id": sourceID, "imported_budget_item_id": row.ID})
	}
	item.RoundTripLineage = map[string]any{
		"source_conversion_session_id": item.SourceConversionSessionID,
		"source_bid_project_code":      item.SourceBidProjectCode,
		"target_budget_project_code":   item.TargetBudgetProjectCode,
		"item_links":                   links,
	}
	item.DeepLink = "/app/conversions/import?session=" + item.ID
	return item, nil
}

func nullIfBlank(value string) any {
	value = strings.TrimSpace(value)
	if value == "" {
		return nil
	}
	return value
}
