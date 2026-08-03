package httpapi

import (
	"net/http"

	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func (s *Server) mrsPrecisionPolicyRoutes() {
	s.mux.HandleFunc("GET /api/mrs/projects/{projectCode}/precision-policy", s.getMRSPrecisionPolicy)
	s.mux.HandleFunc("PUT /api/mrs/projects/{projectCode}/precision-policy", s.putMRSPrecisionPolicy)
	s.mux.HandleFunc("POST /api/mrs/projects/{projectCode}/precision-policy/calculate", s.calculateMRSPrecision)
}

func (s *Server) getMRSPrecisionPolicy(w http.ResponseWriter, r *http.Request) {
	item, err := sqlite.NewMRSPrecisionPolicyRepository(s.store).Get(r.Context(), r.PathValue("projectCode"))
	respond(w, item, err)
}
func (s *Server) putMRSPrecisionPolicy(w http.ResponseWriter, r *http.Request) {
	var body sqlite.MRSPrecisionPolicy
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	body.ProjectCode = r.PathValue("projectCode")
	actor := r.URL.Query().Get("actor_id")
	item, err := sqlite.NewMRSPrecisionPolicyRepository(s.store).Save(r.Context(), body, actor)
	respond(w, item, err)
}
func (s *Server) calculateMRSPrecision(w http.ResponseWriter, r *http.Request) {
	var body struct {
		Level     string `json:"level"`
		Quantity  string `json:"quantity"`
		UnitPrice string `json:"unit_price"`
	}
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	item, err := sqlite.NewMRSPrecisionPolicyRepository(s.store).Calculate(r.Context(), r.PathValue("projectCode"), body.Level, body.Quantity, body.UnitPrice)
	respond(w, item, err)
}
