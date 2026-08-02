package httpapi

import (
	"net/http"

	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func (s *Server) recalculateProjectCostStructure(w http.ResponseWriter, r *http.Request) {
	var body struct {
		BudgetItems []sqlite.BudgetSnapshotItem `json:"budget_items"`
		Scale       int32                       `json:"scale"`
		ActorID     string                      `json:"actor_id"`
	}
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	item, err := sqlite.NewProjectCostStructureRunRepository(s.store).Recalculate(r.Context(), r.PathValue("projectCode"), body.BudgetItems, body.Scale, body.ActorID)
	respond(w, item, err)
}

func (s *Server) getProjectCostStructureRun(w http.ResponseWriter, r *http.Request) {
	item, err := sqlite.NewProjectCostStructureRunRepository(s.store).Get(r.Context(), r.PathValue("runID"))
	respond(w, item, err)
}
