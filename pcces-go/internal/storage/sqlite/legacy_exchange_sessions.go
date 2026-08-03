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

type LegacyExchangeSession struct {
	ID                string               `json:"id"`
	Format            string               `json:"format"`
	FormatVersion     string               `json:"format_version"`
	SourceFilename    string               `json:"source_filename"`
	SourceProjectCode string               `json:"source_project_code"`
	TargetProjectCode string               `json:"target_project_code"`
	Status            string               `json:"status"`
	Report            BidImportReport      `json:"report"`
	Items             []LegacyExchangeItem `json:"items"`
	CreatedBy         string               `json:"created_by"`
	CreatedAt         string               `json:"created_at"`
	RowVersion        int64                `json:"row_version"`
	DeepLink          string               `json:"deep_link"`
}

type LegacyExchangeSessionRepository struct{ store *Store }

func NewLegacyExchangeSessionRepository(store *Store) *LegacyExchangeSessionRepository {
	return &LegacyExchangeSessionRepository{store: store}
}

func (r *LegacyExchangeSessionRepository) Create(ctx context.Context, format, payload, filename, target, actor string) (LegacyExchangeSession, error) {
	filename, target, actor = strings.TrimSpace(filename), strings.TrimSpace(target), strings.TrimSpace(actor)
	if filename == "" || target == "" || actor == "" {
		return LegacyExchangeSession{}, errx.New(errx.CodeInvalidArgument, "source_filename, target_project_code and actor_id are required", "P4-LEGACY-ADAPTER")
	}
	result, err := ParseLegacyExchange(payload, format)
	if err != nil {
		return LegacyExchangeSession{}, errx.Wrap(errx.CodeInvalidArgument, "parse legacy exchange", "P4-LEGACY-ADAPTER", err)
	}
	status := "BLOCKED"
	if result.Report.CanContinue {
		status = "READY"
	}
	reportJSON, _ := json.Marshal(result.Report)
	itemsJSON, _ := json.Marshal(result.Items)
	id := fmt.Sprintf("LAD-%d", time.Now().UTC().UnixNano())
	now := time.Now().UTC().Format(time.RFC3339Nano)
	_, err = r.store.db.ExecContext(ctx, `INSERT INTO legacy_adapter_sessions(id,format,format_version,source_filename,source_project_code,target_project_code,status,report_json,items_json,created_by,created_at,row_version) VALUES(?,?,?,?,?,?,?,?,?,?,?,1)`, id, result.Format, result.FormatVersion, filename, result.SourceProjectCode, target, status, string(reportJSON), string(itemsJSON), actor, now)
	if err != nil {
		return LegacyExchangeSession{}, err
	}
	return r.Get(ctx, id)
}

func (r *LegacyExchangeSessionRepository) Get(ctx context.Context, id string) (LegacyExchangeSession, error) {
	var item LegacyExchangeSession
	var reportJSON, itemsJSON string
	err := r.store.db.QueryRowContext(ctx, `SELECT id,format,format_version,source_filename,source_project_code,target_project_code,status,report_json,items_json,created_by,created_at,row_version FROM legacy_adapter_sessions WHERE id=?`, id).Scan(&item.ID, &item.Format, &item.FormatVersion, &item.SourceFilename, &item.SourceProjectCode, &item.TargetProjectCode, &item.Status, &reportJSON, &itemsJSON, &item.CreatedBy, &item.CreatedAt, &item.RowVersion)
	if err == sql.ErrNoRows {
		return item, errx.New(errx.CodeNotFound, "legacy adapter session not found", "P4-LEGACY-ADAPTER")
	}
	if err != nil {
		return item, err
	}
	_ = json.Unmarshal([]byte(reportJSON), &item.Report)
	_ = json.Unmarshal([]byte(itemsJSON), &item.Items)
	item.DeepLink = "/app/conversions/legacy-adapters?session=" + item.ID
	return item, nil
}
