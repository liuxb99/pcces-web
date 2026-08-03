package sqlite

import (
	"context"
	"encoding/json"
	"fmt"
	"time"
)

type ResourcePriceHistory struct {
	ID, ProjectCode, ResourceID, OldPrice, NewPrice, Source, CreatedAt string
	DeepLink                                                           string
}

type DependencyRecalculationRun struct {
	ID, ProjectCode, Scope                  string
	ResourceID                              *string
	Status, ResultJSON, CreatedAt, DeepLink string
}

type DependencyGraphRepository struct{ store *Store }

func NewDependencyGraphRepository(store *Store) *DependencyGraphRepository {
	return &DependencyGraphRepository{store: store}
}

func (r *DependencyGraphRepository) RecordPrice(ctx context.Context, projectCode, resourceID, oldPrice, newPrice, source string) (*ResourcePriceHistory, error) {
	if oldPrice == newPrice {
		return nil, nil
	}
	id := fmt.Sprintf("price-%d", time.Now().UnixNano())
	now := time.Now().UTC().Format(time.RFC3339Nano)
	_, err := r.store.db.ExecContext(ctx, `INSERT INTO resource_price_history(id,project_code,resource_id,old_price,new_price,source,created_at) VALUES(?,?,?,?,?,?,?)`, id, projectCode, resourceID, oldPrice, newPrice, source, now)
	if err != nil {
		return nil, err
	}
	return &ResourcePriceHistory{ID: id, ProjectCode: projectCode, ResourceID: resourceID, OldPrice: oldPrice, NewPrice: newPrice, Source: source, CreatedAt: now, DeepLink: "/app/projects/by-code/" + projectCode + "/traceability?history=" + id}, nil
}

func (r *DependencyGraphRepository) RecalculateResource(ctx context.Context, projectCode, resourceID string) (DependencyRecalculationRun, error) {
	rows, err := NewResourceBudgetLineageRepository(r.store).Propagate(ctx, resourceID, "DEPENDENCY_GRAPH_LOCAL")
	if err != nil {
		return DependencyRecalculationRun{}, err
	}
	return r.saveRun(ctx, projectCode, "RESOURCE", &resourceID, map[string]any{"updated_items": len(rows), "lineage": rows})
}

func (r *DependencyGraphRepository) RecalculateProject(ctx context.Context, projectCode string) (DependencyRecalculationRun, error) {
	rows, err := r.store.db.QueryContext(ctx, `SELECT DISTINCT resource_id FROM resource_budget_links WHERE project_code=?`, projectCode)
	if err != nil {
		return DependencyRecalculationRun{}, err
	}
	defer rows.Close()
	var resources []string
	for rows.Next() {
		var id string
		if err = rows.Scan(&id); err != nil {
			return DependencyRecalculationRun{}, err
		}
		resources = append(resources, id)
	}
	result := make([]map[string]any, 0, len(resources))
	total := 0
	for _, id := range resources {
		items, e := NewResourceBudgetLineageRepository(r.store).Propagate(ctx, id, "DEPENDENCY_GRAPH_PROJECT")
		if e != nil {
			return DependencyRecalculationRun{}, e
		}
		total += len(items)
		result = append(result, map[string]any{"resource_id": id, "updated_items": len(items), "lineage": items})
	}
	return r.saveRun(ctx, projectCode, "PROJECT", nil, map[string]any{"resources": len(resources), "updated_items": total, "results": result})
}

func (r *DependencyGraphRepository) saveRun(ctx context.Context, projectCode, scope string, resourceID *string, result any) (DependencyRecalculationRun, error) {
	id := fmt.Sprintf("run-%d", time.Now().UnixNano())
	now := time.Now().UTC().Format(time.RFC3339Nano)
	payload, err := json.Marshal(result)
	if err != nil {
		return DependencyRecalculationRun{}, err
	}
	_, err = r.store.db.ExecContext(ctx, `INSERT INTO dependency_recalculation_runs(id,project_code,scope,resource_id,status,result_json,created_at) VALUES(?,?,?,?,?,?,?)`, id, projectCode, scope, resourceID, "COMPLETED", string(payload), now)
	if err != nil {
		return DependencyRecalculationRun{}, err
	}
	return DependencyRecalculationRun{ID: id, ProjectCode: projectCode, Scope: scope, ResourceID: resourceID, Status: "COMPLETED", ResultJSON: string(payload), CreatedAt: now, DeepLink: "/app/projects/by-code/" + projectCode + "/traceability?run=" + id}, nil
}

func (r *DependencyGraphRepository) ListPriceHistory(ctx context.Context, projectCode string) ([]ResourcePriceHistory, error) {
	rows, err := r.store.db.QueryContext(ctx, `SELECT id,project_code,resource_id,old_price,new_price,source,created_at FROM resource_price_history WHERE project_code=? ORDER BY created_at DESC`, projectCode)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	var out []ResourcePriceHistory
	for rows.Next() {
		var v ResourcePriceHistory
		if err = rows.Scan(&v.ID, &v.ProjectCode, &v.ResourceID, &v.OldPrice, &v.NewPrice, &v.Source, &v.CreatedAt); err != nil {
			return nil, err
		}
		v.DeepLink = "/app/projects/by-code/" + projectCode + "/traceability?history=" + v.ID
		out = append(out, v)
	}
	return out, rows.Err()
}

func (r *DependencyGraphRepository) ListRuns(ctx context.Context, projectCode string) ([]DependencyRecalculationRun, error) {
	rows, err := r.store.db.QueryContext(ctx, `SELECT id,project_code,scope,resource_id,status,result_json,created_at FROM dependency_recalculation_runs WHERE project_code=? ORDER BY created_at DESC`, projectCode)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	var out []DependencyRecalculationRun
	for rows.Next() {
		var v DependencyRecalculationRun
		if err = rows.Scan(&v.ID, &v.ProjectCode, &v.Scope, &v.ResourceID, &v.Status, &v.ResultJSON, &v.CreatedAt); err != nil {
			return nil, err
		}
		v.DeepLink = "/app/projects/by-code/" + projectCode + "/traceability?run=" + v.ID
		out = append(out, v)
	}
	return out, rows.Err()
}
