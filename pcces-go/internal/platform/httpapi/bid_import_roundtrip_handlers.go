package httpapi

import (
	"net/http"

	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func (s *Server) bidImportRoundTripRoutes() {
	s.mux.HandleFunc("POST /api/conversions/import-preflight", s.bidImportPreflight)
	s.mux.HandleFunc("POST /api/conversions/import-sessions", s.createBidImportSession)
	s.mux.HandleFunc("GET /api/conversions/import-sessions/{sessionID}", s.getBidImportSession)
}

func (s *Server) bidImportPreflight(w http.ResponseWriter, r *http.Request) {
	var body struct {
		Payload string `json:"payload"`
		Format  string `json:"format"`
	}
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	result, err := sqlite.ParseBidImport(body.Payload, body.Format)
	respond(w, result, err)
}

func (s *Server) createBidImportSession(w http.ResponseWriter, r *http.Request) {
	var body sqlite.BidImportSessionRequest
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	item, err := sqlite.NewBidImportSessionRepository(s.store).Create(r.Context(), body)
	respond(w, item, err)
}

func (s *Server) getBidImportSession(w http.ResponseWriter, r *http.Request) {
	item, err := sqlite.NewBidImportSessionRepository(s.store).Get(r.Context(), r.PathValue("sessionID"))
	respond(w, item, err)
}
