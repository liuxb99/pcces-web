package httpapi

import (
	"net/http"

	"github.com/google/uuid"
	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func (s *Server) contractGovernanceRoutes() {
	s.mux.HandleFunc("POST /api/contracts/{contractID}/versions", s.createContractVersion)
	s.mux.HandleFunc("GET /api/contract-versions/{versionID}", s.getContractVersion)
	s.mux.HandleFunc("POST /api/contract-versions/{versionID}/transition", s.transitionContractVersion)
}

func (s *Server) createContractVersion(w http.ResponseWriter, r *http.Request) {
	var body struct {
		RowVersion int64  `json:"row_version"`
		Note       string `json:"note"`
		Actor      string `json:"actor"`
	}
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	if body.Actor == "" {
		body.Actor = "api"
	}
	item, err := sqlite.NewContractGovernanceRepository(s.store).CreateVersion(r.Context(), uuid.NewString(), r.PathValue("contractID"), body.RowVersion, body.Note, body.Actor)
	respondStatus(w, http.StatusCreated, item, err)
}

func (s *Server) getContractVersion(w http.ResponseWriter, r *http.Request) {
	item, err := sqlite.NewContractGovernanceRepository(s.store).GetVersion(r.Context(), r.PathValue("versionID"))
	respond(w, item, err)
}

func (s *Server) transitionContractVersion(w http.ResponseWriter, r *http.Request) {
	var body struct {
		RowVersion int64  `json:"row_version"`
		Status     string `json:"status"`
		Actor      string `json:"actor"`
	}
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	if body.Actor == "" {
		body.Actor = "api"
	}
	item, err := sqlite.NewContractGovernanceRepository(s.store).Transition(r.Context(), r.PathValue("versionID"), body.RowVersion, body.Status, body.Actor)
	respond(w, item, err)
}
