package httpapi

import (
	"net/http"
	"strconv"

	"github.com/liuxb99/pcces-web/pcces-go/internal/domain/money"
	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func (s *Server) decimalCoreRoutes() {
	s.mux.HandleFunc("GET /api/decimal-budget/items/{id}", s.getDecimalBudgetItem)
	s.mux.HandleFunc("PUT /api/decimal-budget/items/{id}", s.putDecimalBudgetItem)
	s.mux.HandleFunc("POST /api/decimal-budget/projects/{projectCode}/recalculate", s.recalculateDecimalBudget)
	s.mux.HandleFunc("POST /api/decimal-budget/calculate", s.calculateBudgetKind)
	s.mux.HandleFunc("GET /api/decimal-budget/traces/{id}", s.getBudgetTrace)
	s.mux.HandleFunc("GET /api/decimal-budget/projects/{projectCode}/traces", s.listBudgetTraces)
	s.mux.HandleFunc("GET /api/decimal-budget/projects/{projectCode}/versions", s.listBudgetVersions)
	s.mux.HandleFunc("POST /api/decimal-budget/projects/{projectCode}/versions", s.createBudgetVersion)
	s.mux.HandleFunc("GET /api/decimal-budget/versions/{id}", s.getBudgetVersion)
	s.mux.HandleFunc("POST /api/decimal-budget/versions/{id}/restore", s.restoreBudgetVersion)
	s.mux.HandleFunc("GET /api/decimal-budget/projects/{projectCode}/lock", s.getBudgetLock)
	s.mux.HandleFunc("PUT /api/decimal-budget/projects/{projectCode}/lock", s.setBudgetLock)
	s.mux.HandleFunc("GET /api/decimal-resources/{id}", s.getDecimalResource)
	s.mux.HandleFunc("PUT /api/decimal-resources/{id}", s.putDecimalResource)
	s.mux.HandleFunc("PUT /api/decimal-resources/breakdowns/{id}", s.putDecimalBreakdown)
	s.mux.HandleFunc("POST /api/decimal-resources/{id}/recalculate", s.recalculateDecimalResource)
	s.mux.HandleFunc("POST /api/decimal-resources/{id}/budget-links", s.linkResourceBudgetItem)
	s.mux.HandleFunc("POST /api/decimal-resources/{id}/propagate", s.propagateResourcePrice)
	s.mux.HandleFunc("GET /api/decimal-resources/projects/{projectCode}/lineage", s.listResourcePriceLineage)
	s.mux.HandleFunc("GET /api/decimal-resources/projects/{projectCode}/resources", s.listProjectResources)
	s.mux.HandleFunc("GET /api/decimal-resources/projects/{projectCode}/resources/{resourceID}/references", s.listProjectResourceReferences)
	s.mux.HandleFunc("DELETE /api/decimal-resources/projects/{projectCode}/resources/{resourceID}/references/{budgetItemID}", s.unlinkProjectResourceReference)
	s.mux.HandleFunc("POST /api/dependency-graph/projects/{projectCode}/recalculate", s.recalculateDependencyProject)
	s.mux.HandleFunc("POST /api/dependency-graph/projects/{projectCode}/resources/{id}/recalculate", s.recalculateDependencyResource)
	s.mux.HandleFunc("GET /api/dependency-graph/projects/{projectCode}/price-history", s.listDependencyPriceHistory)
	s.mux.HandleFunc("GET /api/dependency-graph/projects/{projectCode}/runs", s.listDependencyRuns)
}

func queryInt(r *http.Request, key string, defaultValue int) int {
	value := r.URL.Query().Get(key)
	if value == "" {
		return defaultValue
	}
	parsed, err := strconv.Atoi(value)
	if err != nil {
		return defaultValue
	}
	return parsed
}
func (s *Server) getDecimalBudgetItem(w http.ResponseWriter, r *http.Request) {
	item, err := sqlite.NewBudgetDecimalRepository(s.store).Get(r.Context(), r.PathValue("id"))
	respond(w, item, err)
}
func (s *Server) putDecimalBudgetItem(w http.ResponseWriter, r *http.Request) {
	var body sqlite.BudgetDecimalItem
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	body.ID = r.PathValue("id")
	lock, err := sqlite.NewBudgetVersionRepository(s.store).Lock(r.Context(), body.ProjectCode)
	if err != nil {
		writeError(w, err)
		return
	}
	if lock.Locked {
		writeError(w, errx.New(errx.CodeConflict, "budget project is locked", "P2-G-VERSION"))
		return
	}
	item, err := sqlite.NewBudgetDecimalRepository(s.store).Save(r.Context(), body)
	respond(w, item, err)
}
func (s *Server) recalculateDecimalBudget(w http.ResponseWriter, r *http.Request) {
	projectCode := r.PathValue("projectCode")
	lock, err := sqlite.NewBudgetVersionRepository(s.store).Lock(r.Context(), projectCode)
	if err != nil {
		writeError(w, err)
		return
	}
	if lock.Locked {
		writeError(w, errx.New(errx.CodeConflict, "budget project is locked", "P2-G-VERSION"))
		return
	}
	total, err := sqlite.NewBudgetDecimalRepository(s.store).RecalculateProject(r.Context(), projectCode)
	respond(w, map[string]any{"project_code": projectCode, "total_amount": total}, err)
}
func (s *Server) createBudgetVersion(w http.ResponseWriter, r *http.Request) {
	var body struct {
		ID     string `json:"id"`
		Label  string `json:"label"`
		Status string `json:"status"`
		Actor  string `json:"actor"`
	}
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	item, err := sqlite.NewBudgetVersionRepository(s.store).Create(r.Context(), body.ID, r.PathValue("projectCode"), body.Label, body.Status, body.Actor)
	if err != nil {
		writeError(w, err)
		return
	}
	writeJSON(w, http.StatusCreated, item)
}
func (s *Server) listBudgetVersions(w http.ResponseWriter, r *http.Request) {
	rows, err := sqlite.NewBudgetVersionRepository(s.store).List(r.Context(), r.PathValue("projectCode"))
	respond(w, rows, err)
}
func (s *Server) getBudgetVersion(w http.ResponseWriter, r *http.Request) {
	item, err := sqlite.NewBudgetVersionRepository(s.store).Get(r.Context(), r.PathValue("id"))
	respond(w, item, err)
}
func (s *Server) restoreBudgetVersion(w http.ResponseWriter, r *http.Request) {
	var body struct {
		Actor        string `json:"actor"`
		NewVersionID string `json:"new_version_id"`
	}
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	item, err := sqlite.NewBudgetVersionRepository(s.store).Restore(r.Context(), r.PathValue("id"), body.Actor, body.NewVersionID)
	respond(w, item, err)
}
func (s *Server) getBudgetLock(w http.ResponseWriter, r *http.Request) {
	item, err := sqlite.NewBudgetVersionRepository(s.store).Lock(r.Context(), r.PathValue("projectCode"))
	respond(w, item, err)
}
func (s *Server) setBudgetLock(w http.ResponseWriter, r *http.Request) {
	var body struct {
		Locked bool   `json:"locked"`
		Actor  string `json:"actor"`
		Reason string `json:"reason"`
	}
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	item, err := sqlite.NewBudgetVersionRepository(s.store).SetLock(r.Context(), r.PathValue("projectCode"), body.Locked, body.Actor, body.Reason)
	respond(w, item, err)
}
func (s *Server) calculateBudgetKind(w http.ResponseWriter, r *http.Request) {
	var body struct {
		ID          string                `json:"id"`
		ProjectCode string                `json:"project_code"`
		ItemID      *string               `json:"item_id"`
		Kind        string                `json:"kind"`
		Scale       int                   `json:"scale"`
		Input       money.BudgetKindInput `json:"input"`
	}
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	item, err := sqlite.NewBudgetTraceRepository(s.store).Calculate(r.Context(), body.ID, body.ProjectCode, body.ItemID, body.Kind, body.Scale, body.Input)
	if err != nil {
		writeError(w, err)
		return
	}
	writeJSON(w, http.StatusCreated, item)
}
func (s *Server) getBudgetTrace(w http.ResponseWriter, r *http.Request) {
	item, err := sqlite.NewBudgetTraceRepository(s.store).Get(r.Context(), r.PathValue("id"))
	respond(w, item, err)
}
func (s *Server) listBudgetTraces(w http.ResponseWriter, r *http.Request) {
	items, err := sqlite.NewBudgetTraceRepository(s.store).ListProject(r.Context(), r.PathValue("projectCode"))
	respond(w, items, err)
}
func (s *Server) getDecimalResource(w http.ResponseWriter, r *http.Request) {
	item, err := sqlite.NewResourceDecimalRepository(s.store).GetResource(r.Context(), r.PathValue("id"))
	respond(w, item, err)
}
func (s *Server) putDecimalResource(w http.ResponseWriter, r *http.Request) {
	var body sqlite.ResourceDecimal
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	body.ID = r.PathValue("id")
	item, err := sqlite.NewResourceDecimalRepository(s.store).SaveResource(r.Context(), body)
	respond(w, item, err)
}
func (s *Server) putDecimalBreakdown(w http.ResponseWriter, r *http.Request) {
	var body sqlite.ResourceBreakdownDecimal
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	body.ID = r.PathValue("id")
	item, err := sqlite.NewResourceDecimalRepository(s.store).SaveBreakdown(r.Context(), body)
	respond(w, item, err)
}
func (s *Server) recalculateDecimalResource(w http.ResponseWriter, r *http.Request) {
	item, err := sqlite.NewResourceDecimalRepository(s.store).RecalculateResource(r.Context(), r.PathValue("id"))
	respond(w, item, err)
}
func (s *Server) linkResourceBudgetItem(w http.ResponseWriter, r *http.Request) {
	var body struct {
		ProjectCode  string `json:"project_code"`
		BudgetItemID string `json:"budget_item_id"`
	}
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	err := sqlite.NewResourceBudgetLineageRepository(s.store).Link(r.Context(), body.ProjectCode, r.PathValue("id"), body.BudgetItemID)
	if err != nil {
		writeError(w, err)
		return
	}
	writeJSON(w, http.StatusCreated, map[string]string{"project_code": body.ProjectCode, "resource_id": r.PathValue("id"), "budget_item_id": body.BudgetItemID})
}
func (s *Server) propagateResourcePrice(w http.ResponseWriter, r *http.Request) {
	var body struct {
		Trigger string `json:"trigger"`
	}
	_ = decodeJSON(r, &body)
	rows, err := sqlite.NewResourceBudgetLineageRepository(s.store).Propagate(r.Context(), r.PathValue("id"), body.Trigger)
	respond(w, map[string]any{"resource_id": r.PathValue("id"), "updated_items": len(rows), "lineage": rows}, err)
}
func (s *Server) listResourcePriceLineage(w http.ResponseWriter, r *http.Request) {
	rows, err := sqlite.NewResourceBudgetLineageRepository(s.store).ListProject(r.Context(), r.PathValue("projectCode"))
	respond(w, rows, err)
}
func (s *Server) listProjectResources(w http.ResponseWriter, r *http.Request) {
	page, err := sqlite.NewResourceBudgetLineageRepository(s.store).ListProjectResources(r.Context(), r.PathValue("projectCode"), r.URL.Query().Get("q"), queryInt(r, "limit", 50), queryInt(r, "offset", 0))
	respond(w, page, err)
}
func (s *Server) listProjectResourceReferences(w http.ResponseWriter, r *http.Request) {
	page, err := sqlite.NewResourceBudgetLineageRepository(s.store).ListResourceReferences(r.Context(), r.PathValue("projectCode"), r.PathValue("resourceID"), queryInt(r, "limit", 50), queryInt(r, "offset", 0))
	respond(w, page, err)
}
func (s *Server) unlinkProjectResourceReference(w http.ResponseWriter, r *http.Request) {
	removed, err := sqlite.NewResourceBudgetLineageRepository(s.store).Unlink(r.Context(), r.PathValue("projectCode"), r.PathValue("resourceID"), r.PathValue("budgetItemID"))
	if err != nil {
		writeError(w, err)
		return
	}
	if !removed {
		writeError(w, errx.New(errx.CodeNotFound, "resource reference not found", "P3-G2"))
		return
	}
	w.WriteHeader(http.StatusNoContent)
}
func (s *Server) recalculateDependencyProject(w http.ResponseWriter, r *http.Request) {
	run, err := sqlite.NewDependencyGraphRepository(s.store).RecalculateProject(r.Context(), r.PathValue("projectCode"))
	respond(w, run, err)
}
func (s *Server) recalculateDependencyResource(w http.ResponseWriter, r *http.Request) {
	run, err := sqlite.NewDependencyGraphRepository(s.store).RecalculateResource(r.Context(), r.PathValue("projectCode"), r.PathValue("id"))
	respond(w, run, err)
}
func (s *Server) listDependencyPriceHistory(w http.ResponseWriter, r *http.Request) {
	rows, err := sqlite.NewDependencyGraphRepository(s.store).ListPriceHistory(r.Context(), r.PathValue("projectCode"))
	respond(w, rows, err)
}
func (s *Server) listDependencyRuns(w http.ResponseWriter, r *http.Request) {
	rows, err := sqlite.NewDependencyGraphRepository(s.store).ListRuns(r.Context(), r.PathValue("projectCode"))
	respond(w, rows, err)
}
