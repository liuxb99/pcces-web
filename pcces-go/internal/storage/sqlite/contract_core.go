package sqlite

import (
	"context"
	"database/sql"
	"fmt"
	"strconv"
	"strings"
	"time"

	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

type ContractEligibility struct {
	ProjectCode     string   `json:"project_code"`
	BudgetVersionID string   `json:"budget_version_id"`
	Eligible        bool     `json:"eligible"`
	Reasons         []string `json:"reasons"`
}

type ContractItemInput struct {
	SourceBudgetItemID string `json:"source_budget_item_id"`
	ItemNo             string `json:"item_no"`
	Name               string `json:"name"`
	Unit               string `json:"unit"`
	Quantity           string `json:"quantity"`
	UnitPrice          string `json:"unit_price"`
	Amount             string `json:"amount"`
}

type ContractCreateRequest struct {
	ID, ProjectCode, BudgetVersionID, ContractNo, Name, Contractor, Actor string
	ContractAmount string
	Items          []ContractItemInput
}

type ContractCoreRepository struct{ store *Store }
func NewContractCoreRepository(store *Store) *ContractCoreRepository { return &ContractCoreRepository{store: store} }

func (r *ContractCoreRepository) Eligibility(ctx context.Context, projectCode, versionID string) (ContractEligibility, error) {
	result := ContractEligibility{ProjectCode: projectCode, BudgetVersionID: versionID, Reasons: []string{}}
	var project, status string
	err := r.store.db.QueryRowContext(ctx, `SELECT project_code,status FROM budget_versions WHERE id=?`, versionID).Scan(&project, &status)
	if err == sql.ErrNoRows { result.Reasons = append(result.Reasons, "BUDGET_VERSION_NOT_FOUND"); return result, nil }
	if err != nil { return result, err }
	if project != projectCode { result.Reasons = append(result.Reasons, "PROJECT_VERSION_MISMATCH") }
	switch strings.ToUpper(status) { case "APPROVED", "FROZEN", "ARCHIVED": default: result.Reasons = append(result.Reasons, "BUDGET_VERSION_NOT_APPROVED") }
	result.Eligible = len(result.Reasons) == 0
	return result, nil
}

func parseNonNegative(value, field string) (float64, error) {
	v, err := strconv.ParseFloat(strings.TrimSpace(value), 64)
	if err != nil || v < 0 { return 0, errx.New(errx.CodeInvalidArgument, field+" must be a non-negative decimal", "P5-G-CONTRACT") }
	return v, nil
}

func (r *ContractCoreRepository) Create(ctx context.Context, req ContractCreateRequest) (map[string]any, error) {
	if req.ID=="" || req.ProjectCode=="" || req.BudgetVersionID=="" || req.ContractNo=="" || req.Name=="" || req.Actor=="" { return nil, errx.New(errx.CodeInvalidArgument,"required contract fields are missing","P5-G-CONTRACT") }
	eligible, err := r.Eligibility(ctx, req.ProjectCode, req.BudgetVersionID); if err != nil { return nil, err }; if !eligible.Eligible { return nil, errx.New(errx.CodeConflict, strings.Join(eligible.Reasons, ","), "P5-G-CONTRACT") }
	if len(req.Items)==0 { return nil, errx.New(errx.CodeInvalidArgument,"contract items are required","P5-G-CONTRACT") }
	seen:=map[string]bool{}; total:=0.0
	for _, item:=range req.Items { if item.SourceBudgetItemID==""||item.Name=="" { return nil,errx.New(errx.CodeInvalidArgument,"source_budget_item_id and name are required","P5-G-CONTRACT") }; if seen[item.SourceBudgetItemID] { return nil,errx.New(errx.CodeInvalidArgument,"duplicate source_budget_item_id","P5-G-CONTRACT") }; seen[item.SourceBudgetItemID]=true; amount,e:=parseNonNegative(item.Amount,"amount");if e!=nil{return nil,e};total+=amount }
	declared,err:=parseNonNegative(req.ContractAmount,"contract_amount");if err!=nil{return nil,err};if fmt.Sprintf("%.8f",declared)!=fmt.Sprintf("%.8f",total){return nil,errx.New(errx.CodeInvalidArgument,"contract_amount must equal contract item total","P5-G-CONTRACT")}
	tx,err:=r.store.db.BeginTx(ctx,nil);if err!=nil{return nil,err};defer func(){_ = tx.Rollback()}();now:=time.Now().UTC().Format(time.RFC3339Nano)
	if _,err=tx.ExecContext(ctx,`INSERT INTO contracts_v2(id,project_code,budget_version_id,contract_no,name,contractor,status,contract_amount,created_by,created_at,updated_at,row_version) VALUES(?,?,?,?,?,?,?,?,?,?,?,1)`,req.ID,req.ProjectCode,req.BudgetVersionID,req.ContractNo,req.Name,req.Contractor,"DRAFT",fmt.Sprintf("%.8f",declared),req.Actor,now,now);err!=nil{return nil,err}
	for i,item:=range req.Items { if _,err=tx.ExecContext(ctx,`INSERT INTO contract_items_v2(id,contract_id,source_budget_item_id,item_no,name,unit,quantity,unit_price,amount,sort_order,created_at) VALUES(?,?,?,?,?,?,?,?,?,?,?)`,fmt.Sprintf("%s-%d",req.ID,i+1),req.ID,item.SourceBudgetItemID,item.ItemNo,item.Name,item.Unit,item.Quantity,item.UnitPrice,item.Amount,i+1,now);err!=nil{return nil,err} }
	if err=tx.Commit();err!=nil{return nil,err};return r.Get(ctx,req.ID)
}

func (r *ContractCoreRepository) Get(ctx context.Context,id string)(map[string]any,error){var project,version,no,name,contractor,status,amount string;var rowVersion int64;err:=r.store.db.QueryRowContext(ctx,`SELECT project_code,budget_version_id,contract_no,name,COALESCE(contractor,''),status,contract_amount,row_version FROM contracts_v2 WHERE id=?`,id).Scan(&project,&version,&no,&name,&contractor,&status,&amount,&rowVersion);if err==sql.ErrNoRows{return nil,errx.New(errx.CodeNotFound,"contract not found","P5-G-CONTRACT")};if err!=nil{return nil,err};rows,err:=r.store.db.QueryContext(ctx,`SELECT id,source_budget_item_id,COALESCE(item_no,''),name,COALESCE(unit,''),quantity,unit_price,amount FROM contract_items_v2 WHERE contract_id=? ORDER BY sort_order`,id);if err!=nil{return nil,err};defer rows.Close();items:=[]map[string]any{};for rows.Next(){var iid,source,itemNo,itemName,unit,qty,price,itemAmount string;if err=rows.Scan(&iid,&source,&itemNo,&itemName,&unit,&qty,&price,&itemAmount);err!=nil{return nil,err};items=append(items,map[string]any{"id":iid,"source_budget_item_id":source,"item_no":itemNo,"name":itemName,"unit":unit,"quantity":qty,"unit_price":price,"amount":itemAmount,"deep_link":"/app/projects/by-code/"+project+"/budget?item="+source})};return map[string]any{"id":id,"project_code":project,"budget_version_id":version,"contract_no":no,"name":name,"contractor":contractor,"status":status,"contract_amount":amount,"row_version":rowVersion,"items":items,"deep_link":"/app/contracts/"+id},rows.Err()}
