package httpapi

import (
	"net/http"

	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func (s *Server) decimalCoreRoutes() {
	s.mux.HandleFunc("GET /api/decimal-budget/items/{id}", s.getDecimalBudgetItem)
	s.mux.HandleFunc("PUT /api/decimal-budget/items/{id}", s.putDecimalBudgetItem)
	s.mux.HandleFunc("POST /api/decimal-budget/projects/{projectCode}/recalculate", s.recalculateDecimalBudget)
	s.mux.HandleFunc("GET /api/decimal-resources/{id}", s.getDecimalResource)
	s.mux.HandleFunc("PUT /api/decimal-resources/{id}", s.putDecimalResource)
	s.mux.HandleFunc("PUT /api/decimal-resources/breakdowns/{id}", s.putDecimalBreakdown)
	s.mux.HandleFunc("POST /api/decimal-resources/{id}/recalculate", s.recalculateDecimalResource)
}

func (s *Server) getDecimalBudgetItem(w http.ResponseWriter, r *http.Request) {
	item, err := sqlite.NewBudgetDecimalRepository(s.store).Get(r.Context(), r.PathValue("id"))
	respond(w, item, err)
}

func (s *Server) putDecimalBudgetItem(w http.ResponseWriter, r *http.Request) {
	var body sqlite.BudgetDecimalItem
	if err := decodeJSON(r, &body); err != nil { writeError(w, err); return }
	body.ID = r.PathValue("id")
	item, err := sqlite.NewBudgetDecimalRepository(s.store).Save(r.Context(), body)
	respond(w, item, err)
}

func (s *Server) recalculateDecimalBudget(w http.ResponseWriter, r *http.Request) {
	total, err := sqlite.NewBudgetDecimalRepository(s.store).RecalculateProject(r.Context(), r.PathValue("projectCode"))
	respond(w, map[string]any{"project_code": r.PathValue("projectCode"), "total_amount": total}, err)
}

func (s *Server) getDecimalResource(w http.ResponseWriter, r *http.Request) {
	item, err := sqlite.NewResourceDecimalRepository(s.store).GetResource(r.Context(), r.PathValue("id"))
	respond(w, item, err)
}

func (s *Server) putDecimalResource(w http.ResponseWriter, r *http.Request) {
	var body sqlite.ResourceDecimal
	if err := decodeJSON(r, &body); err != nil { writeError(w, err); return }
	body.ID = r.PathValue("id")
	item, err := sqlite.NewResourceDecimalRepository(s.store).SaveResource(r.Context(), body)
	respond(w, item, err)
}

func (s *Server) putDecimalBreakdown(w http.ResponseWriter, r *http.Request) {
	var body sqlite.ResourceBreakdownDecimal
	if err := decodeJSON(r, &body); err != nil { writeError(w, err); return }
	body.ID = r.PathValue("id")
	item, err := sqlite.NewResourceDecimalRepository(s.store).SaveBreakdown(r.Context(), body)
	respond(w, item, err)
}

func (s *Server) recalculateDecimalResource(w http.ResponseWriter, r *http.Request) {
	item, err := sqlite.NewResourceDecimalRepository(s.store).RecalculateResource(r.Context(), r.PathValue("id"))
	respond(w, item, err)
}
