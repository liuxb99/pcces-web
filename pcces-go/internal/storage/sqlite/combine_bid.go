package sqlite

import (
	"context"
	"database/sql"
	"encoding/json"
	"fmt"
	"strconv"
	"strings"
	"time"

	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

type CombineBidItem struct {
	ID                string `json:"id"`
	Code              string `json:"code"`
	Name              string `json:"name"`
	Unit              string `json:"unit"`
	Quantity          string `json:"quantity"`
	UnitPrice         string `json:"unit_price"`
	Amount            string `json:"amount"`
	SourceProjectCode string `json:"source_project_code,omitempty"`
	SourceItemID      string `json:"source_item_id,omitempty"`
}

type CombineBidSource struct {
	ProjectCode string           `json:"project_code"`
	Items       []CombineBidItem `json:"items"`
}

type CombineBidConflict struct {
	Code           string `json:"code"`
	ExistingSource string `json:"existing_source"`
	IncomingSource string `json:"incoming_source"`
	Resolution     string `json:"resolution"`
	RenamedTo      string `json:"renamed_to,omitempty"`
}

type CombineBidResult struct {
	Status            string               `json:"status"`
	Strategy          string               `json:"strategy"`
	Conflicts         []CombineBidConflict `json:"conflicts"`
	BlockingConflicts []CombineBidConflict `json:"blocking_conflicts"`
	Items             []CombineBidItem     `json:"items"`
}

type CombineBidSession struct {
	ID                string               `json:"id"`
	TargetProjectCode string               `json:"target_project_code"`
	Strategy          string               `json:"strategy"`
	Status            string               `json:"status"`
	Sources           []CombineBidSource   `json:"sources"`
	Conflicts         []CombineBidConflict `json:"conflicts"`
	Items             []CombineBidItem     `json:"items"`
	CreatedBy         string               `json:"created_by"`
	CreatedAt         string               `json:"created_at"`
	RowVersion        int64                `json:"row_version"`
	DeepLink          string               `json:"deep_link"`
}

func decimalText(v string) (float64, error) { return strconv.ParseFloat(strings.TrimSpace(v), 64) }

func CombineBidSources(sources []CombineBidSource, strategy string) (CombineBidResult, error) {
	strategy = strings.ToUpper(strings.TrimSpace(strategy))
	allowed := map[string]bool{"BLOCK": true, "KEEP_FIRST": true, "KEEP_LAST": true, "SUM_QUANTITY": true, "RENAME": true}
	if !allowed[strategy] {
		return CombineBidResult{}, errx.New(errx.CodeInvalidArgument, "invalid combine-bid strategy", "P4-COMBINE-001")
	}
	if len(sources) < 2 {
		return CombineBidResult{}, errx.New(errx.CodeInvalidArgument, "at least two source budgets are required", "P4-COMBINE-001")
	}
	merged := map[string]CombineBidItem{}
	order := []string{}
	conflicts := []CombineBidConflict{}
	renameCounts := map[string]int{}
	for _, source := range sources {
		project := strings.TrimSpace(source.ProjectCode)
		if project == "" {
			return CombineBidResult{}, errx.New(errx.CodeInvalidArgument, "source project_code is required", "P4-COMBINE-001")
		}
		for index, raw := range source.Items {
			item := raw
			item.Code = strings.ToUpper(strings.TrimSpace(item.Code))
			if item.Code == "" {
				return CombineBidResult{}, errx.New(errx.CodeInvalidArgument, "every item requires code", "P4-COMBINE-001")
			}
			if item.ID == "" {
				item.ID = fmt.Sprintf("%s:%d", project, index+1)
			}
			item.SourceProjectCode, item.SourceItemID = project, item.ID
			current, exists := merged[item.Code]
			if !exists {
				merged[item.Code] = item
				order = append(order, item.Code)
				continue
			}
			conflict := CombineBidConflict{Code: item.Code, ExistingSource: current.SourceProjectCode, IncomingSource: project, Resolution: strategy}
			conflicts = append(conflicts, conflict)
			switch strategy {
			case "KEEP_LAST":
				merged[item.Code] = item
			case "SUM_QUANTITY":
				if current.Name != item.Name || current.Unit != item.Unit || strings.TrimSpace(current.UnitPrice) != strings.TrimSpace(item.UnitPrice) {
					conflicts[len(conflicts)-1].Resolution = "BLOCKED_INCOMPATIBLE_SUM"
					continue
				}
				q1, e1 := decimalText(current.Quantity)
				q2, e2 := decimalText(item.Quantity)
				p, e3 := decimalText(current.UnitPrice)
				if e1 != nil || e2 != nil || e3 != nil {
					return CombineBidResult{}, errx.New(errx.CodeInvalidArgument, "invalid numeric value", "P4-COMBINE-001")
				}
				current.Quantity = strconv.FormatFloat(q1+q2, 'f', -1, 64)
				current.Amount = strconv.FormatFloat((q1+q2)*p, 'f', -1, 64)
				merged[item.Code] = current
			case "RENAME":
				renameCounts[item.Code]++
				if renameCounts[item.Code] < 2 {
					renameCounts[item.Code] = 2
				}
				newCode := fmt.Sprintf("%s-%d", item.Code, renameCounts[item.Code])
				for {
					if _, ok := merged[newCode]; !ok {
						break
					}
					renameCounts[item.Code]++
					newCode = fmt.Sprintf("%s-%d", item.Code, renameCounts[item.Code])
				}
				item.Code = newCode
				merged[newCode] = item
				order = append(order, newCode)
				conflicts[len(conflicts)-1].RenamedTo = newCode
			}
		}
	}
	blocking := []CombineBidConflict{}
	for _, c := range conflicts {
		if c.Resolution == "BLOCK" || c.Resolution == "BLOCKED_INCOMPATIBLE_SUM" {
			blocking = append(blocking, c)
		}
	}
	items := make([]CombineBidItem, 0, len(order))
	for _, code := range order {
		items = append(items, merged[code])
	}
	status := "READY"
	if len(blocking) > 0 {
		status = "BLOCKED"
	}
	return CombineBidResult{Status: status, Strategy: strategy, Conflicts: conflicts, BlockingConflicts: blocking, Items: items}, nil
}

type CombineBidRepository struct{ store *Store }

func NewCombineBidRepository(store *Store) *CombineBidRepository {
	return &CombineBidRepository{store: store}
}

func (r *CombineBidRepository) Create(ctx context.Context, target, strategy, actor string, sources []CombineBidSource) (CombineBidSession, error) {
	if strings.TrimSpace(target) == "" || strings.TrimSpace(actor) == "" {
		return CombineBidSession{}, errx.New(errx.CodeInvalidArgument, "target_project_code and actor_id are required", "P4-COMBINE-001")
	}
	result, err := CombineBidSources(sources, strategy)
	if err != nil {
		return CombineBidSession{}, err
	}
	id := fmt.Sprintf("CB-%d", time.Now().UTC().UnixNano())
	now := time.Now().UTC().Format(time.RFC3339Nano)
	sourcesJSON, _ := json.Marshal(sources)
	conflictsJSON, _ := json.Marshal(result.Conflicts)
	resultJSON, _ := json.Marshal(result.Items)
	_, err = r.store.db.ExecContext(ctx, `INSERT INTO combine_bid_sessions(id,target_project_code,strategy,status,sources_json,conflicts_json,result_json,created_by,created_at,row_version) VALUES(?,?,?,?,?,?,?,?,?,1)`, id, target, result.Strategy, result.Status, string(sourcesJSON), string(conflictsJSON), string(resultJSON), actor, now)
	if err != nil {
		return CombineBidSession{}, err
	}
	return r.Get(ctx, id)
}

func (r *CombineBidRepository) Get(ctx context.Context, id string) (CombineBidSession, error) {
	var s CombineBidSession
	var sourcesJSON, conflictsJSON, resultJSON string
	err := r.store.db.QueryRowContext(ctx, `SELECT id,target_project_code,strategy,status,sources_json,conflicts_json,result_json,created_by,created_at,row_version FROM combine_bid_sessions WHERE id=?`, id).Scan(&s.ID, &s.TargetProjectCode, &s.Strategy, &s.Status, &sourcesJSON, &conflictsJSON, &resultJSON, &s.CreatedBy, &s.CreatedAt, &s.RowVersion)
	if err == sql.ErrNoRows {
		return s, errx.New(errx.CodeNotFound, "combine-bid session not found", "P4-COMBINE-001")
	}
	if err != nil {
		return s, err
	}
	_ = json.Unmarshal([]byte(sourcesJSON), &s.Sources)
	_ = json.Unmarshal([]byte(conflictsJSON), &s.Conflicts)
	_ = json.Unmarshal([]byte(resultJSON), &s.Items)
	s.DeepLink = "/app/conversions/combine-bid?session=" + s.ID
	return s, nil
}
