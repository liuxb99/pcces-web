package httpapi

import (
	"net/http"

	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func (s *Server) legacyExchangeAdapterRoutes() {
	s.mux.HandleFunc("POST /api/conversions/legacy-adapters/preflight", s.legacyAdapterPreflight)
	s.mux.HandleFunc("POST /api/conversions/legacy-adapters/sessions", s.createLegacyAdapterSession)
	s.mux.HandleFunc("GET /api/conversions/legacy-adapters/sessions/{id}", s.getLegacyAdapterSession)
}

func (s *Server) legacyAdapterPreflight(w http.ResponseWriter, r *http.Request) {
	var body struct { Format string `json:"format"`; Payload string `json:"payload"` }
	if err := decodeJSON(r, &body); err != nil { writeError(w, err); return }
	item, err := sqlite.ParseLegacyExchange(body.Payload, body.Format)
	respond(w, item, err)
}

func (s *Server) createLegacyAdapterSession(w http.ResponseWriter, r *http.Request) {
	var body struct {
		Format string `json:"format"`
		Payload string `json:"payload"`
		SourceFilename string `json:"source_filename"`
		TargetProjectCode string `json:"target_project_code"`
		ActorID string `json:"actor_id"`
	}
	if err := decodeJSON(r, &body); err != nil { writeError(w, err); return }
	item, err := sqlite.NewLegacyExchangeSessionRepository(s.store).Create(r.Context(), body.Format, body.Payload, body.SourceFilename, body.TargetProjectCode, body.ActorID)
	if err != nil { writeError(w, err); return }
	writeJSON(w, http.StatusCreated, item)
}

func (s *Server) getLegacyAdapterSession(w http.ResponseWriter, r *http.Request) {
	item, err := sqlite.NewLegacyExchangeSessionRepository(s.store).Get(r.Context(), r.PathValue("id"))
	respond(w, item, err)
}
