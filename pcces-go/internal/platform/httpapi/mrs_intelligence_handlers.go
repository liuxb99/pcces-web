package httpapi

import (
	"net/http"

	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func (s *Server) mrsIntelligenceRoutes() {
	s.mux.HandleFunc("POST /api/mrs/catalog/{id}/quotes", s.addMRSQuote)
	s.mux.HandleFunc("GET /api/mrs/catalog/{id}/quote-comparison", s.compareMRSQuotes)
	s.mux.HandleFunc("POST /api/mrs/analysis-recipes/{id}/snapshots", s.snapshotMRSRecipe)
	s.mux.HandleFunc("POST /api/mrs/catalog/{id}/impact", s.calculateMRSImpact)
}

func (s *Server) addMRSQuote(w http.ResponseWriter, r *http.Request) {
	var body struct {
		sqlite.MRSQuote
		ActorID string `json:"actor_id"`
	}
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	body.ID = r.PathValue("id") + "-quote-" + body.Vendor
	body.CatalogItemID = r.PathValue("id")
	body.CreatedBy = body.ActorID
	item, err := sqlite.NewMRSIntelligenceRepository(s.store).AddQuote(r.Context(), body.MRSQuote)
	respond(w, item, err)
}

func (s *Server) compareMRSQuotes(w http.ResponseWriter, r *http.Request) {
	item, err := sqlite.NewMRSIntelligenceRepository(s.store).CompareQuotes(r.Context(), r.PathValue("id"))
	respond(w, item, err)
}

func (s *Server) snapshotMRSRecipe(w http.ResponseWriter, r *http.Request) {
	var body struct {
		ID      string `json:"id"`
		ActorID string `json:"actor_id"`
	}
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	if body.ID == "" {
		body.ID = r.PathValue("id") + "-snapshot"
	}
	item, err := sqlite.NewMRSIntelligenceRepository(s.store).SnapshotRecipe(r.Context(), body.ID, r.PathValue("id"), body.ActorID)
	respond(w, item, err)
}

func (s *Server) calculateMRSImpact(w http.ResponseWriter, r *http.Request) {
	var body struct {
		ID       string `json:"id"`
		OldPrice string `json:"old_price"`
		NewPrice string `json:"new_price"`
		ActorID  string `json:"actor_id"`
	}
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	if body.ID == "" {
		body.ID = r.PathValue("id") + "-impact"
	}
	item, err := sqlite.NewMRSIntelligenceRepository(s.store).Impact(r.Context(), body.ID, r.PathValue("id"), body.OldPrice, body.NewPrice, body.ActorID)
	respond(w, item, err)
}
