package sqlite

import (
	"context"
	"database/sql"
	"encoding/json"
	"fmt"
	"sort"
	"strings"
	"time"

	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

type BudgetBidConversionItem struct {
	SourceBudgetItemID string `json:"source_budget_item_id"`
	BidItemID          string `json:"bid_item_id"`
	Code               string `json:"code"`
	Name               string `json:"name"`
	Unit               string `json:"unit"`
	Quantity           string `json:"quantity"`
	UnitPrice          string `json:"unit_price"`
	Amount             string `json:"amount"`
	SortOrder          int    `json:"sort_order"`
}

type BudgetBidConversionRequest struct {
	SourceProjectCode     string           `json:"source_project_code"`
	SourceBudgetVersionID string           `json:"source_budget_version_id"`
	TargetBidProjectCode  string           `json:"target_bid_project_code"`
	Mode                  string           `json:"mode"`
	ActorID               string           `json:"actor_id"`
	Options               map[string]any   `json:"options"`
	BudgetItems           []map[string]any `json:"budget_items"`
}

type BudgetBidConversionSession struct {
	ID                    string                    `json:"id"`
	SourceProjectCode     string                    `json:"source_project_code"`
	SourceBudgetVersionID string                    `json:"source_budget_version_id"`
	TargetBidProjectCode  string                    `json:"target_bid_project_code"`
	Mode                  string                    `json:"mode"`
	Status                string                    `json:"status"`
	Options               map[string]any            `json:"options"`
	SourceSnapshot        []map[string]any          `json:"source_snapshot"`
	ResultSnapshot        []BudgetBidConversionItem `json:"result_snapshot"`
	CreatedBy             string                    `json:"created_by"`
	CreatedAt             string                    `json:"created_at"`
	RowVersion            int64                     `json:"row_version"`
	Lineage               map[string]string         `json:"lineage"`
	DeepLink              string                    `json:"deep_link"`
}

type BudgetBidConversionRepository struct{ store *Store }

func NewBudgetBidConversionRepository(store *Store) *BudgetBidConversionRepository {
	return &BudgetBidConversionRepository{store: store}
}

func (r *BudgetBidConversionRepository) Convert(ctx context.Context, req BudgetBidConversionRequest) (BudgetBidConversionSession, error) {
	req.SourceProjectCode = strings.TrimSpace(req.SourceProjectCode)
	req.SourceBudgetVersionID = strings.TrimSpace(req.SourceBudgetVersionID)
	req.TargetBidProjectCode = strings.TrimSpace(req.TargetBidProjectCode)
	req.Mode = strings.ToUpper(strings.TrimSpace(req.Mode))
	if req.Mode == "" {
		req.Mode = "CREATE"
	}
	if req.SourceProjectCode == "" || req.SourceBudgetVersionID == "" || req.TargetBidProjectCode == "" {
		return BudgetBidConversionSession{}, errx.New(errx.CodeInvalidArgument, "source project, source version and target bid project are required", "P4-CONV-005")
	}
	if req.Mode != "CREATE" && req.Mode != "REPLACE" && req.Mode != "APPEND" {
		return BudgetBidConversionSession{}, errx.New(errx.CodeInvalidArgument, "mode must be CREATE, REPLACE or APPEND", "P4-CONV-005")
	}
	if len(req.BudgetItems) == 0 {
		return BudgetBidConversionSession{}, errx.New(errx.CodeInvalidArgument, "budget_items are required", "P4-CONV-005")
	}
	seen := map[string]bool{}
	converted := make([]BudgetBidConversionItem, 0, len(req.BudgetItems))
	for i, raw := range req.BudgetItems {
		id := strings.TrimSpace(fmt.Sprint(raw["id"]))
		if id == "" || id == "<nil>" {
			id = fmt.Sprintf("ROW-%d", i+1)
		}
		if seen[id] {
			return BudgetBidConversionSession{}, errx.New(errx.CodeInvalidArgument, "duplicate source budget item id", "P4-CONV-005")
		}
		seen[id] = true
		converted = append(converted, BudgetBidConversionItem{
			SourceBudgetItemID: id,
			BidItemID:          req.TargetBidProjectCode + ":" + id,
			Code:               strings.ToUpper(strings.TrimSpace(fmt.Sprint(raw["code"]))),
			Name:               strings.TrimSpace(fmt.Sprint(raw["name"])),
			Unit:               strings.TrimSpace(fmt.Sprint(raw["unit"])),
			Quantity:           defaultString(strings.TrimSpace(fmt.Sprint(raw["quantity"])), "0"),
			UnitPrice:          defaultString(strings.TrimSpace(fmt.Sprint(raw["unit_price"])), "0"),
			Amount:             defaultString(strings.TrimSpace(fmt.Sprint(raw["amount"])), "0"),
			SortOrder:          i + 1,
		})
	}
	sort.SliceStable(converted, func(i, j int) bool {
		if converted[i].SortOrder == converted[j].SortOrder {
			return converted[i].Code < converted[j].Code
		}
		return converted[i].SortOrder < converted[j].SortOrder
	})
	optionsJSON, _ := json.Marshal(req.Options)
	sourceJSON, _ := json.Marshal(req.BudgetItems)
	resultJSON, _ := json.Marshal(converted)
	sessionID := fmt.Sprintf("B2B-%d", time.Now().UTC().UnixNano())
	now := time.Now().UTC().Format(time.RFC3339Nano)
	tx, err := r.store.db.BeginTx(ctx, nil)
	if err != nil {
		return BudgetBidConversionSession{}, err
	}
	defer tx.Rollback()
	if req.Mode == "CREATE" {
		var existing string
		err = tx.QueryRowContext(ctx, `SELECT id FROM budget_bid_conversion_sessions WHERE target_bid_project_code=? LIMIT 1`, req.TargetBidProjectCode).Scan(&existing)
		if err == nil {
			return BudgetBidConversionSession{}, errx.New(errx.CodeConflict, "target bid project already has a conversion", "P4-CONV-005")
		}
		if err != sql.ErrNoRows {
			return BudgetBidConversionSession{}, err
		}
	}
	_, err = tx.ExecContext(ctx, `INSERT INTO budget_bid_conversion_sessions(id,source_project_code,source_budget_version_id,target_bid_project_code,mode,status,options_json,source_snapshot_json,result_snapshot_json,created_by,created_at,row_version) VALUES(?,?,?,?,?,'COMPLETED',?,?,?,?,?,1)`,
		sessionID, req.SourceProjectCode, req.SourceBudgetVersionID, req.TargetBidProjectCode, req.Mode, string(optionsJSON), string(sourceJSON), string(resultJSON), req.ActorID, now)
	if err != nil {
		return BudgetBidConversionSession{}, err
	}
	if err = tx.Commit(); err != nil {
		return BudgetBidConversionSession{}, err
	}
	return r.Get(ctx, sessionID)
}

func (r *BudgetBidConversionRepository) Get(ctx context.Context, id string) (BudgetBidConversionSession, error) {
	var item BudgetBidConversionSession
	var optionsJSON, sourceJSON, resultJSON string
	err := r.store.db.QueryRowContext(ctx, `SELECT id,source_project_code,source_budget_version_id,target_bid_project_code,mode,status,options_json,source_snapshot_json,result_snapshot_json,created_by,created_at,row_version FROM budget_bid_conversion_sessions WHERE id=?`, id).
		Scan(&item.ID, &item.SourceProjectCode, &item.SourceBudgetVersionID, &item.TargetBidProjectCode, &item.Mode, &item.Status, &optionsJSON, &sourceJSON, &resultJSON, &item.CreatedBy, &item.CreatedAt, &item.RowVersion)
	if err == sql.ErrNoRows {
		return item, errx.New(errx.CodeNotFound, "conversion session not found", "P4-CONV-005")
	}
	if err != nil {
		return item, err
	}
	_ = json.Unmarshal([]byte(optionsJSON), &item.Options)
	_ = json.Unmarshal([]byte(sourceJSON), &item.SourceSnapshot)
	_ = json.Unmarshal([]byte(resultJSON), &item.ResultSnapshot)
	item.Lineage = map[string]string{"source_project_code": item.SourceProjectCode, "source_budget_version_id": item.SourceBudgetVersionID, "target_bid_project_code": item.TargetBidProjectCode, "session_id": item.ID}
	item.DeepLink = "/app/bid-conversion?session=" + item.ID
	return item, nil
}
