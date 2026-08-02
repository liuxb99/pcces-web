package httpapi

import (
	"net/http"

	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func (s *Server) resourceProjectReferenceRoutes() {
	s.mux.HandleFunc("POST /api/decimal-resources/projects/{projectCode}/references", s.createResourceProjectReference)
	s.mux.HandleFunc("GET /api/decimal-resources/projects/{projectCode}/references", s.listResourceProjectReferences)
}

func (s *Server) createResourceProjectReference(w http.ResponseWriter, r *http.Request) {
	var body struct {
		SourceProjectCode string `json:"source_project_code"`
		SourceResourceID  string `json:"source_resource_id"`
		TargetResourceID  string `json:"target_resource_id"`
		ReferenceType     string `json:"reference_type"`
		ActorID           string `json:"actor_id"`
	}
	if err := decodeJSON(r, &body); err != nil { writeError(w, err); return }
	item, err := sqlite.NewResourceProjectReferenceRepository(s.store).Import(
		r.Context(), r.PathValue("projectCode"), body.SourceProjectCode,
		body.SourceResourceID, body.TargetResourceID, body.ReferenceType, body.ActorID,
	)
	if err != nil { writeError(w, err); return }
	writeJSON(w, http.StatusCreated, item)
}

func (s *Server) listResourceProjectReferences(w http.ResponseWriter, r *http.Request) {
	items, err := sqlite.NewResourceProjectReferenceRepository(s.store).ListTarget(r.Context(), r.PathValue("projectCode"))
	respond(w, items, err)
}
