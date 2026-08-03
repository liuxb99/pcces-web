package sqlite

import (
	"context"
	"database/sql"
	"encoding/json"
	"strings"
	"time"

	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

type ConversionWizardIssue struct {
	Code   string `json:"code"`
	Detail string `json:"detail,omitempty"`
	ItemID string `json:"item_id,omitempty"`
	Index  int    `json:"index,omitempty"`
}

type ConversionWizardReport struct {
	Errors       []ConversionWizardIssue `json:"errors"`
	Warnings     []ConversionWizardIssue `json:"warnings"`
	ErrorCount   int                     `json:"error_count"`
	WarningCount int                     `json:"warning_count"`
	CanContinue  bool                    `json:"can_continue"`
}

type ConversionWizardItem struct {
	ID        string  `json:"id"`
	Code      string  `json:"code"`
	Name      string  `json:"name"`
	Quantity  *string `json:"quantity"`
	UnitPrice *string `json:"unit_price"`
}

type ConversionWizardRequest struct {
	SourceProjectCode     string                 `json:"source_project_code"`
	SourceBudgetVersionID string                 `json:"source_budget_version_id"`
	TargetProjectCode     string                 `json:"target_project_code"`
	Mode                  string                 `json:"mode"`
	Options               map[string]any         `json:"options"`
	BudgetItems           []ConversionWizardItem `json:"budget_items"`
	ActorID               string                 `json:"actor_id"`
}

type ConversionWizardSession struct {
	ID                    string                 `json:"id"`
	SourceProjectCode     string                 `json:"source_project_code"`
	SourceBudgetVersionID string                 `json:"source_budget_version_id"`
	TargetProjectCode     string                 `json:"target_project_code"`
	Mode                  string                 `json:"mode"`
	Status                string                 `json:"status"`
	Options               map[string]any         `json:"options"`
	Report                ConversionWizardReport `json:"report"`
	CanContinue           bool                   `json:"can_continue"`
	CreatedBy             string                 `json:"created_by"`
	CreatedAt             string                 `json:"created_at"`
	RowVersion            int64                  `json:"row_version"`
	DeepLink              string                 `json:"deep_link"`
}

type ConversionWizardRepository struct{ store *Store }

func NewConversionWizardRepository(store *Store) *ConversionWizardRepository {
	return &ConversionWizardRepository{store: store}
}

func BuildConversionPreflight(items []ConversionWizardItem, mode string, options map[string]any) ConversionWizardReport {
	report := ConversionWizardReport{Errors: []ConversionWizardIssue{}, Warnings: []ConversionWizardIssue{}}
	seenIDs := map[string]bool{}
	seenCodes := map[string]bool{}
	if len(items) == 0 {
		report.Errors = append(report.Errors, ConversionWizardIssue{Code: "EMPTY_BUDGET", Detail: "budget contains no convertible items"})
	}
	for index, item := range items {
		id := strings.TrimSpace(item.ID)
		code := strings.ToUpper(strings.TrimSpace(item.Code))
		if id == "" {
			report.Errors = append(report.Errors, ConversionWizardIssue{Code: "MISSING_ITEM_ID", Index: index})
		} else if seenIDs[id] {
			report.Errors = append(report.Errors, ConversionWizardIssue{Code: "DUPLICATE_ITEM_ID", ItemID: id})
		}
		seenIDs[id] = true
		if code == "" {
			report.Errors = append(report.Errors, ConversionWizardIssue{Code: "MISSING_ITEM_CODE", ItemID: id})
		} else if seenCodes[code] {
			report.Warnings = append(report.Warnings, ConversionWizardIssue{Code: "DUPLICATE_ITEM_CODE", ItemID: id})
		}
		seenCodes[code] = true
		if strings.TrimSpace(item.Name) == "" {
			report.Warnings = append(report.Warnings, ConversionWizardIssue{Code: "MISSING_ITEM_NAME", ItemID: id})
		}
		if item.Quantity == nil {
			report.Warnings = append(report.Warnings, ConversionWizardIssue{Code: "MISSING_QUANTITY", ItemID: id})
		}
		if item.UnitPrice == nil {
			report.Warnings = append(report.Warnings, ConversionWizardIssue{Code: "MISSING_UNIT_PRICE", ItemID: id})
		}
	}
	format := "BID_JSON"
	if raw, ok := options["format"].(string); ok && strings.TrimSpace(raw) != "" {
		format = strings.ToUpper(strings.TrimSpace(raw))
	}
	allowed := map[string]bool{"BID_JSON": true, "XML_NEW": true, "XML_LEGACY": true, "XLSX": true}
	if !allowed[format] {
		report.Errors = append(report.Errors, ConversionWizardIssue{Code: "UNSUPPORTED_FORMAT", Detail: format})
	}
	if strings.EqualFold(mode, "APPEND") {
		if value, ok := options["deduplicate_by_code"].(bool); ok && !value {
			report.Warnings = append(report.Warnings, ConversionWizardIssue{Code: "APPEND_WITHOUT_DEDUPLICATION"})
		}
	}
	report.ErrorCount = len(report.Errors)
	report.WarningCount = len(report.Warnings)
	report.CanContinue = report.ErrorCount == 0
	return report
}

func (r *ConversionWizardRepository) Create(ctx context.Context, req ConversionWizardRequest) (ConversionWizardSession, error) {
	req.SourceProjectCode = strings.TrimSpace(req.SourceProjectCode)
	req.SourceBudgetVersionID = strings.TrimSpace(req.SourceBudgetVersionID)
	req.TargetProjectCode = strings.TrimSpace(req.TargetProjectCode)
	req.Mode = strings.ToUpper(strings.TrimSpace(req.Mode))
	if req.Mode == "" {
		req.Mode = "CREATE"
	}
	if req.SourceProjectCode == "" || req.SourceBudgetVersionID == "" || req.TargetProjectCode == "" {
		return ConversionWizardSession{}, errx.New(errx.CodeInvalidArgument, "source project, source version and target project are required", "P4-CONV-002")
	}
	if req.Mode != "CREATE" && req.Mode != "REPLACE" && req.Mode != "APPEND" {
		return ConversionWizardSession{}, errx.New(errx.CodeInvalidArgument, "mode must be CREATE, REPLACE or APPEND", "P4-CONV-003")
	}
	if req.Options == nil {
		req.Options = map[string]any{}
	}
	if _, ok := req.Options["format"]; !ok {
		req.Options["format"] = "BID_JSON"
	}
	if _, ok := req.Options["include_resources"]; !ok {
		req.Options["include_resources"] = true
	}
	if _, ok := req.Options["include_analysis"]; !ok {
		req.Options["include_analysis"] = true
	}
	if _, ok := req.Options["deduplicate_by_code"]; !ok {
		req.Options["deduplicate_by_code"] = true
	}
	report := BuildConversionPreflight(req.BudgetItems, req.Mode, req.Options)
	optionsJSON, _ := json.Marshal(req.Options)
	reportJSON, _ := json.Marshal(report)
	id := uniqueID("CW")
	status := "BLOCKED"
	if report.CanContinue {
		status = "READY"
	}
	now := time.Now().UTC().Format(time.RFC3339Nano)
	_, err := r.store.db.ExecContext(ctx, `INSERT INTO conversion_wizard_sessions(id,source_project_code,source_budget_version_id,target_project_code,mode,status,options_json,report_json,can_continue,created_by,created_at,row_version) VALUES(?,?,?,?,?,?,?,?,?,?,?,1)`,
		id, req.SourceProjectCode, req.SourceBudgetVersionID, req.TargetProjectCode, req.Mode, status, string(optionsJSON), string(reportJSON), boolToInt(report.CanContinue), req.ActorID, now)
	if err != nil {
		return ConversionWizardSession{}, err
	}
	return r.Get(ctx, id)
}

func (r *ConversionWizardRepository) Get(ctx context.Context, id string) (ConversionWizardSession, error) {
	var item ConversionWizardSession
	var optionsJSON, reportJSON string
	var canContinue int
	err := r.store.db.QueryRowContext(ctx, `SELECT id,source_project_code,source_budget_version_id,target_project_code,mode,status,options_json,report_json,can_continue,created_by,created_at,row_version FROM conversion_wizard_sessions WHERE id=?`, id).Scan(
		&item.ID, &item.SourceProjectCode, &item.SourceBudgetVersionID, &item.TargetProjectCode, &item.Mode, &item.Status, &optionsJSON, &reportJSON, &canContinue, &item.CreatedBy, &item.CreatedAt, &item.RowVersion)
	if err == sql.ErrNoRows {
		return item, errx.New(errx.CodeNotFound, "conversion wizard session not found", "P4-CONV-001")
	}
	if err != nil {
		return item, err
	}
	_ = json.Unmarshal([]byte(optionsJSON), &item.Options)
	_ = json.Unmarshal([]byte(reportJSON), &item.Report)
	item.CanContinue = canContinue != 0
	item.DeepLink = "/app/conversions/wizard?session=" + item.ID
	return item, nil
}
