package httpapi

import (
	"net/http"

	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func (s *Server) resourceOperationRoutes() {
	s.mux.HandleFunc("POST /api/decimal-resources/projects/{projectCode}/replace", s.replaceProjectResource)
	s.mux.HandleFunc("POST /api/decimal-resources/batch-prices", s.batchUpdateResourcePrices)
	s.mux.HandleFunc("GET /api/mrs/projects/{projectCode}/state", s.getMRSProjectState)
	s.mux.HandleFunc("PUT /api/mrs/projects/{projectCode}/state", s.putMRSProjectState)
}

func (s *Server) replaceProjectResource(w http.ResponseWriter, r *http.Request) {
	var body struct {
		SourceResourceID string `json:"source_resource_id"`
		TargetResourceID string `json:"target_resource_id"`
		ActorID          string `json:"actor_id"`
	}
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
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
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	item, err := sqlite.NewResourceBudgetLineageRepository(s.store).BatchUpdatePrices(r.Context(), body.Updates, body.Trigger)
	respond(w, item, err)
}

func (s *Server) getMRSProjectState(w http.ResponseWriter, r *http.Request) {
	item, err := sqlite.NewMRSProjectStateRepository(s.store).Get(r.Context(), r.PathValue("projectCode"))
	respond(w, item, err)
}
func (s *Server) putMRSProjectState(w http.ResponseWriter, r *http.Request) {
	var body struct {
		State      string `json:"state"`
		Template   bool   `json:"template"`
		Readonly   bool   `json:"readonly"`
		Reason     string `json:"reason"`
		ActorID    string `json:"actor_id"`
		RowVersion int64  `json:"row_version"`
	}
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	item, err := sqlite.NewMRSProjectStateRepository(s.store).Save(r.Context(), r.PathValue("projectCode"), body.State, body.Template, body.Readonly, body.Reason, body.ActorID, body.RowVersion)
	respond(w, item, err)
}
