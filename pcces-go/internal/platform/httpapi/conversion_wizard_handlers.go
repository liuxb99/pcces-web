package httpapi

import (
	"net/http"

	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func (s *Server) conversionWizardRoutes() {
	s.mux.HandleFunc("POST /api/conversions/preflight", s.conversionPreflight)
	s.mux.HandleFunc("POST /api/conversions/wizard-sessions", s.createConversionWizardSession)
	s.mux.HandleFunc("GET /api/conversions/wizard-sessions/{sessionID}", s.getConversionWizardSession)
}

func (s *Server) conversionPreflight(w http.ResponseWriter, r *http.Request) {
	var body sqlite.ConversionWizardRequest
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	respond(w, sqlite.BuildConversionPreflight(body.BudgetItems, body.Mode, body.Options), nil)
}

func (s *Server) createConversionWizardSession(w http.ResponseWriter, r *http.Request) {
	var body sqlite.ConversionWizardRequest
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	item, err := sqlite.NewConversionWizardRepository(s.store).Create(r.Context(), body)
	if err != nil {
		writeError(w, err)
		return
	}
	writeJSON(w, http.StatusCreated, item)
}

func (s *Server) getConversionWizardSession(w http.ResponseWriter, r *http.Request) {
	item, err := sqlite.NewConversionWizardRepository(s.store).Get(r.Context(), r.PathValue("sessionID"))
	respond(w, item, err)
}
