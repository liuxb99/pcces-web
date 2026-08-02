package httpapi

import (
	"net/http"

	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

type combineBidRequest struct {
	TargetProjectCode string                    `json:"target_project_code"`
	Strategy          string                    `json:"strategy"`
	Sources           []sqlite.CombineBidSource `json:"sources"`
	ActorID           string                    `json:"actor_id"`
}

func (s *Server) combineBidRoutes() {
	s.mux.HandleFunc("POST /api/conversions/combine-bid/preflight", s.combineBidPreflight)
	s.mux.HandleFunc("POST /api/conversions/combine-bid/sessions", s.createCombineBidSession)
	s.mux.HandleFunc("GET /api/conversions/combine-bid/sessions/{sessionID}", s.getCombineBidSession)
}

func (s *Server) combineBidPreflight(w http.ResponseWriter, r *http.Request) {
	var body combineBidRequest
	if err := decodeJSON(r, &body); err != nil { writeError(w, err); return }
	result, err := sqlite.CombineBidSources(body.Sources, body.Strategy)
	respond(w, result, err)
}

func (s *Server) createCombineBidSession(w http.ResponseWriter, r *http.Request) {
	var body combineBidRequest
	if err := decodeJSON(r, &body); err != nil { writeError(w, err); return }
	item, err := sqlite.NewCombineBidRepository(s.store).Create(r.Context(), body.TargetProjectCode, body.Strategy, body.ActorID, body.Sources)
	respond(w, item, err)
}

func (s *Server) getCombineBidSession(w http.ResponseWriter, r *http.Request) {
	item, err := sqlite.NewCombineBidRepository(s.store).Get(r.Context(), r.PathValue("sessionID"))
	respond(w, item, err)
}
