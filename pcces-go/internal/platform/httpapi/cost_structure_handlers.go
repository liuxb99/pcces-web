package httpapi

import (
	"net/http"
	"strings"

	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func (s *Server) costStructureRoutes() {
	s.mux.HandleFunc("GET /api/cost-structures/types", s.listCostStructureTypes)
	s.mux.HandleFunc("PUT /api/cost-structures/types/{id}", s.putCostStructureType)
	s.mux.HandleFunc("GET /api/cost-structures/projects/{projectCode}", s.getProjectCostStructure)
	s.mux.HandleFunc("PUT /api/cost-structures/projects/{projectCode}", s.putProjectCostStructure)
	s.mux.HandleFunc("POST /api/cost-structures/calculate", s.calculateCostStructure)
	s.mux.HandleFunc("POST /api/cost-structures/projects/{projectCode}/recalculate", s.recalculateProjectCostStructure)
	s.mux.HandleFunc("GET /api/cost-structures/runs/{runID}", s.getProjectCostStructureRun)
}

func (s *Server) listCostStructureTypes(w http.ResponseWriter, r *http.Request) {
	enabledOnly := strings.ToLower(r.URL.Query().Get("enabled_only")) != "false"
	items, err := sqlite.NewCostStructureRepository(s.store).ListTypes(r.Context(), enabledOnly)
	respond(w, items, err)
}

func (s *Server) putCostStructureType(w http.ResponseWriter, r *http.Request) {
	var body sqlite.CostStructureType
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	body.ID = r.PathValue("id")
	item, err := sqlite.NewCostStructureRepository(s.store).SaveType(r.Context(), body)
	respond(w, item, err)
}

func (s *Server) getProjectCostStructure(w http.ResponseWriter, r *http.Request) {
	item, err := sqlite.NewCostStructureRepository(s.store).GetProject(r.Context(), r.PathValue("projectCode"))
	respond(w, item, err)
}

func (s *Server) putProjectCostStructure(w http.ResponseWriter, r *http.Request) {
	var body struct {
		CostStructureTypeID string `json:"cost_structure_type_id"`
		Issue               string `json:"issue"`
		ActorID             string `json:"actor_id"`
		RowVersion          int64  `json:"row_version"`
	}
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	item, err := sqlite.NewCostStructureRepository(s.store).AssignProject(r.Context(), r.PathValue("projectCode"), body.CostStructureTypeID, body.Issue, body.ActorID, body.RowVersion)
	respond(w, item, err)
}

func (s *Server) calculateCostStructure(w http.ResponseWriter, r *http.Request) {
	var body struct {
		DirectCost string                       `json:"direct_cost"`
		Scale      int32                        `json:"scale"`
		Lines      []sqlite.CostCalculationLine `json:"lines"`
	}
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	result, err := sqlite.CalculateCostStructure(body.Lines, body.DirectCost, body.Scale)
	respond(w, result, err)
}
