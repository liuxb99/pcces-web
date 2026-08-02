package httpapi

import (
	"net/http"

	"github.com/google/uuid"
	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func (s *Server) contractChangeRoutes() {
	s.mux.HandleFunc("POST /api/contracts/{contractID}/changes", s.createContractChange)
	s.mux.HandleFunc("GET /api/contracts/changes/{changeID}", s.getContractChange)
}

func (s *Server) createContractChange(w http.ResponseWriter, r *http.Request) {
	var body struct {
		ChangeNo string                           `json:"change_no"`
		Reason   string                           `json:"reason"`
		Actor    string                           `json:"actor"`
		Items    []sqlite.ContractChangeItemInput `json:"items"`
	}
	if err := decodeJSON(r, &body); err != nil { writeError(w, err); return }
	if body.Actor == "" { body.Actor = "api" }
	item, err := sqlite.NewContractChangeRepository(s.store).Create(r.Context(), sqlite.ContractChangeRequest{ID:uuid.NewString(),ContractID:r.PathValue("contractID"),ChangeNo:body.ChangeNo,Reason:body.Reason,Actor:body.Actor,Items:body.Items})
	respondStatus(w, http.StatusCreated, item, err)
}

func (s *Server) getContractChange(w http.ResponseWriter, r *http.Request) {
	item, err := sqlite.NewContractChangeRepository(s.store).Get(r.Context(), r.PathValue("changeID"))
	respond(w, item, err)
}
