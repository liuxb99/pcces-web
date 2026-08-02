package httpapi

import (
	"net/http"

	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func (s *Server) budgetBidConversionRoutes() {
	s.mux.HandleFunc("POST /api/conversions/budget-to-bid", s.convertBudgetToBid)
	s.mux.HandleFunc("GET /api/conversions/sessions/{sessionID}", s.getBudgetBidConversionSession)
}

func (s *Server) convertBudgetToBid(w http.ResponseWriter, r *http.Request) {
	var body sqlite.BudgetBidConversionRequest
	if err := decodeJSON(r, &body); err != nil { writeError(w, err); return }
	item, err := sqlite.NewBudgetBidConversionRepository(s.store).Convert(r.Context(), body)
	respond(w, item, err)
}

func (s *Server) getBudgetBidConversionSession(w http.ResponseWriter, r *http.Request) {
	item, err := sqlite.NewBudgetBidConversionRepository(s.store).Get(r.Context(), r.PathValue("sessionID"))
	respond(w, item, err)
}
