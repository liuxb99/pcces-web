package httpapi

import (
	"net/http"

	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func (s *Server) budgetCrossProjectRoutes() {
	s.mux.HandleFunc("POST /api/decimal-budget/cross-project-references/propagate", s.propagateCrossProjectBudget)
	s.mux.HandleFunc("POST /api/decimal-budget/projects/{leftProject}/mode-diff/{rightProject}", s.diffCrossProjectBudget)
}

func (s *Server) propagateCrossProjectBudget(w http.ResponseWriter, r *http.Request) {
	var body struct {
		ID                string `json:"id"`
		SourceProjectCode string `json:"source_project_code"`
		TargetProjectCode string `json:"target_project_code"`
		Actor             string `json:"actor"`
	}
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	result, err := sqlite.NewBudgetCrossProjectSyncRepository(s.store).Propagate(
		r.Context(), body.ID, body.SourceProjectCode, body.TargetProjectCode, body.Actor,
	)
	respond(w, result, err)
}

func (s *Server) diffCrossProjectBudget(w http.ResponseWriter, r *http.Request) {
	var body struct {
		ID    string `json:"id"`
		Actor string `json:"actor"`
	}
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	result, err := sqlite.NewBudgetCrossProjectSyncRepository(s.store).Diff(
		r.Context(), body.ID, r.PathValue("leftProject"), r.PathValue("rightProject"), body.Actor,
	)
	respond(w, result, err)
}
