package httpapi

import (
	"net/http"

	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func (s *Server) resourceOperationRoutes() {
	s.mux.HandleFunc("POST /api/decimal-resources/projects/{projectCode}/replace", s.replaceProjectResource)
	s.mux.HandleFunc("POST /api/decimal-resources/batch-prices", s.batchUpdateResourcePrices)
}

func (s *Server) replaceProjectResource(w http.ResponseWriter, r *http.Request) {
	var body struct {
		SourceResourceID string `json:"source_resource_id"`
		TargetResourceID string `json:"target_resource_id"`
		ActorID          string `json:"actor_id"`
	}
	if err := decodeJSON(r, &body); err != nil { writeError(w, err); return }
	item, err := sqlite.NewResourceBudgetLineageRepository(s.store).ReplaceResource(
		r.Context(), r.PathValue("projectCode"), body.SourceResourceID, body.TargetResourceID, body.ActorID,
	)
	respond(w, item, err)
}

func (s *Server) batchUpdateResourcePrices(w http.ResponseWriter, r *http.Request) {
	var body struct {
		Updates []sqlite.ResourcePriceUpdate `json:"updates"`
		Trigger string                       `json:"trigger"`
	}
	if err := decodeJSON(r, &body); err != nil { writeError(w, err); return }
	item, err := sqlite.NewResourceBudgetLineageRepository(s.store).BatchUpdatePrices(r.Context(), body.Updates, body.Trigger)
	respond(w, item, err)
}
