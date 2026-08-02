package sqlite

import (
	"context"
	"strings"
)

type ProjectResourceReference struct {
	LinkID string `json:"link_id"`
	ProjectCode string `json:"project_code"`
	ResourceID string `json:"resource_id"`
	BudgetItemID string `json:"budget_item_id"`
	ItemType string `json:"item_type"`
	Quantity string `json:"quantity"`
	UnitPrice string `json:"unit_price"`
	Amount string `json:"amount"`
	RowVersion int64 `json:"row_version"`
	DeepLink string `json:"deep_link"`
}

type ProjectResourceSummary struct {
	ResourceID string `json:"resource_id"`
	Code string `json:"code"`
	Name string `json:"name"`
	UnitPrice string `json:"unit_price"`
	DeepLink string `json:"deep_link"`
	ReferenceCount int64 `json:"reference_count"`
}

type ProjectResourcePage struct {
	Items []ProjectResourceSummary `json:"items"`
	Total int64 `json:"total"`
	Limit int `json:"limit"`
	Offset int `json:"offset"`
}

type ProjectResourceReferencePage struct {
	Items []ProjectResourceReference `json:"items"`
	Total int64 `json:"total"`
	Limit int `json:"limit"`
	Offset int `json:"offset"`
}

func normalizeResourceLinkPage(limit, offset int) (int, int) {
	if limit <= 0 { limit = 50 }
	if limit > 200 { limit = 200 }
	if offset < 0 { offset = 0 }
	return limit, offset
}

func (r *ResourceBudgetLineageRepository) ListProjectResources(ctx context.Context, projectCode, query string, limit, offset int) (ProjectResourcePage, error) {
	limit, offset = normalizeResourceLinkPage(limit, offset)
	q := strings.ToLower(strings.TrimSpace(query))
	rows, err := r.store.db.QueryContext(ctx, `SELECT l.resource_id,r.code,r.name,r.unit_price,COUNT(l.budget_item_id) FROM resource_budget_links l JOIN resources_decimal r ON r.id=l.resource_id WHERE l.project_code=? GROUP BY l.resource_id,r.code,r.name,r.unit_price ORDER BY r.code,l.resource_id`, projectCode)
	if err != nil { return ProjectResourcePage{}, err }
	defer rows.Close()
	all := make([]ProjectResourceSummary,0)
	for rows.Next(){var v ProjectResourceSummary;if err=rows.Scan(&v.ResourceID,&v.Code,&v.Name,&v.UnitPrice,&v.ReferenceCount);err!=nil{return ProjectResourcePage{},err};if q!=""&&!strings.Contains(strings.ToLower(v.Code),q)&&!strings.Contains(strings.ToLower(v.Name),q){continue};v.DeepLink="/app/project-resources?project="+projectCode+"&resource="+v.ResourceID;all=append(all,v)}
	if err=rows.Err();err!=nil{return ProjectResourcePage{},err}
	total:=int64(len(all));start:=offset;if start>len(all){start=len(all)};end:=start+limit;if end>len(all){end=len(all)}
	return ProjectResourcePage{Items:all[start:end],Total:total,Limit:limit,Offset:offset},nil
}

func (r *ResourceBudgetLineageRepository) ListResourceReferences(ctx context.Context, projectCode, resourceID string, limit, offset int) (ProjectResourceReferencePage,error){
	limit,offset=normalizeResourceLinkPage(limit,offset)
	var total int64
	if err:=r.store.db.QueryRowContext(ctx,`SELECT COUNT(*) FROM resource_budget_links WHERE project_code=? AND resource_id=?`,projectCode,resourceID).Scan(&total);err!=nil{return ProjectResourceReferencePage{},err}
	rows,err:=r.store.db.QueryContext(ctx,`SELECT l.id,l.project_code,l.resource_id,l.budget_item_id,b.kind,b.quantity,b.unit_price,b.amount,b.row_version FROM resource_budget_links l JOIN budget_items_decimal b ON b.id=l.budget_item_id WHERE l.project_code=? AND l.resource_id=? ORDER BY b.id LIMIT ? OFFSET ?`,projectCode,resourceID,limit,offset)
	if err!=nil{return ProjectResourceReferencePage{},err};defer rows.Close();items:=make([]ProjectResourceReference,0)
	for rows.Next(){var v ProjectResourceReference;if err=rows.Scan(&v.LinkID,&v.ProjectCode,&v.ResourceID,&v.BudgetItemID,&v.ItemType,&v.Quantity,&v.UnitPrice,&v.Amount,&v.RowVersion);err!=nil{return ProjectResourceReferencePage{},err};v.DeepLink="/app/budget/"+projectCode+"?item="+v.BudgetItemID;items=append(items,v)}
	return ProjectResourceReferencePage{Items:items,Total:total,Limit:limit,Offset:offset},rows.Err()
}

func (r *ResourceBudgetLineageRepository) Unlink(ctx context.Context,projectCode,resourceID,budgetItemID string)(bool,error){res,err:=r.store.db.ExecContext(ctx,`DELETE FROM resource_budget_links WHERE project_code=? AND resource_id=? AND budget_item_id=?`,projectCode,resourceID,budgetItemID);if err!=nil{return false,err};n,err:=res.RowsAffected();return n==1,err}
