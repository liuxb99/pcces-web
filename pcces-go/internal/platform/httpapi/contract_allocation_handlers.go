package httpapi

import (
	"net/http"

	"github.com/google/uuid"
	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func (s *Server) contractAllocationRoutes() {
	s.mux.HandleFunc("GET /api/contracts/{contractID}/allocation-basis", s.getContractAllocationBasis)
	s.mux.HandleFunc("POST /api/contracts/{contractID}/items", s.addContractAllocationItems)
	s.mux.HandleFunc("POST /api/contracts/{parentID}/subcontracts/{childID}", s.linkSubcontract)
}

func (s *Server) getContractAllocationBasis(w http.ResponseWriter, r *http.Request) {
	item, err := sqlite.NewContractAllocationRepository(s.store).Basis(r.Context(), r.PathValue("contractID"))
	respond(w, item, err)
}

func (s *Server) addContractAllocationItems(w http.ResponseWriter, r *http.Request) {
	var body struct {
		RowVersion int64                           `json:"row_version"`
		Items      []sqlite.ContractAllocationItem `json:"items"`
	}
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	item, err := sqlite.NewContractAllocationRepository(s.store).AddItems(r.Context(), r.PathValue("contractID"), body.RowVersion, body.Items)
	respondStatus(w, http.StatusCreated, item, err)
}

func (s *Server) linkSubcontract(w http.ResponseWriter, r *http.Request) {
	var body struct {
		Actor string `json:"actor"`
	}
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	if body.Actor == "" {
		body.Actor = "api"
	}
	item, err := sqlite.NewContractAllocationRepository(s.store).LinkSubcontract(r.Context(), uuid.NewString(), r.PathValue("parentID"), r.PathValue("childID"), body.Actor)
	respondStatus(w, http.StatusCreated, item, err)
}
