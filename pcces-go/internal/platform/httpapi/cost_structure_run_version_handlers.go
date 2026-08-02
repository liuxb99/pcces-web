package httpapi

import (
	"net/http"

	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func (s *Server) linkCostStructureRunVersion(w http.ResponseWriter, r *http.Request) {
	var body sqlite.CostStructureRunVersion
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	body.ProjectCode = r.PathValue("projectCode")
	body.RunID = r.PathValue("runID")
	item, err := sqlite.NewCostStructureRunVersionRepository(s.store).Link(r.Context(), body)
	respond(w, item, err)
}

func (s *Server) compareCostStructureRunVersions(w http.ResponseWriter, r *http.Request) {
	query := r.URL.Query()
	item, err := sqlite.NewCostStructureRunVersionRepository(s.store).Compare(r.Context(), query.Get("left"), query.Get("right"))
	respond(w, item, err)
}
