package httpapi

import (
	"net/http"

	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func (s *Server) bidLifecycleRoutes() {
	s.mux.HandleFunc("POST /api/decimal-budget/bud-to-bid", s.convertBudToBid)
	s.mux.HandleFunc("POST /api/decimal-budget/projects/{projectCode}/bid-price-versions", s.createBidPriceVersion)
	s.mux.HandleFunc("GET /api/decimal-budget/bid-price-versions/{id}", s.getBidPriceVersion)
	s.mux.HandleFunc("POST /api/decimal-budget/bid-price-versions/{id}/rollback", s.rollbackBidPriceVersion)
}

func (s *Server) convertBudToBid(w http.ResponseWriter, r *http.Request) {
	var body struct {
		RunID             string `json:"run_id"`
		SourceProjectCode string `json:"source_project_code"`
		TargetProjectCode string `json:"target_project_code"`
		Actor             string `json:"actor"`
		Overwrite         bool   `json:"overwrite"`
	}
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	item, err := sqlite.NewBidLifecycleRepository(s.store).Convert(r.Context(), body.RunID, body.SourceProjectCode, body.TargetProjectCode, body.Actor, body.Overwrite)
	if err != nil {
		writeError(w, err)
		return
	}
	writeJSON(w, http.StatusCreated, item)
}
func (s *Server) createBidPriceVersion(w http.ResponseWriter, r *http.Request) {
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
	item, err := sqlite.NewBidLifecycleRepository(s.store).CreateVersion(r.Context(), body.ID, r.PathValue("projectCode"), body.Label, body.Status, body.Actor)
	if err != nil {
		writeError(w, err)
		return
	}
	writeJSON(w, http.StatusCreated, item)
}
func (s *Server) getBidPriceVersion(w http.ResponseWriter, r *http.Request) {
	item, err := sqlite.NewBidLifecycleRepository(s.store).GetVersion(r.Context(), r.PathValue("id"))
	respond(w, item, err)
}
func (s *Server) rollbackBidPriceVersion(w http.ResponseWriter, r *http.Request) {
	var body struct {
		RunID string `json:"run_id"`
		Actor string `json:"actor"`
	}
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	item, err := sqlite.NewBidLifecycleRepository(s.store).Rollback(r.Context(), r.PathValue("id"), body.RunID, body.Actor)
	respond(w, item, err)
}
