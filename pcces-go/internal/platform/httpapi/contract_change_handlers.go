package httpapi

import (
	"net/http"

	"github.com/google/uuid"
	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func (s *Server) contractChangeRoutes() {
	s.mux.HandleFunc("POST /api/contracts/{contractID}/changes", s.createContractChange)
	s.mux.HandleFunc("GET /api/contracts/changes/{changeID}", s.getContractChange)
	s.mux.HandleFunc("POST /api/contracts/{contractID}/change-cases", s.createContractChangeCase)
	s.mux.HandleFunc("GET /api/contracts/change-cases/{caseID}", s.getContractChangeCase)
	s.mux.HandleFunc("POST /api/contracts/change-cases/{caseID}/transition", s.transitionContractChangeCase)
}

func (s *Server) createContractChange(w http.ResponseWriter, r *http.Request) {
	var body struct {
		ChangeNo string                           `json:"change_no"`
		Reason   string                           `json:"reason"`
		Actor    string                           `json:"actor"`
		Items    []sqlite.ContractChangeItemInput `json:"items"`
	}
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	if body.Actor == "" {
		body.Actor = "api"
	}
	item, err := sqlite.NewContractChangeRepository(s.store).Create(r.Context(), sqlite.ContractChangeRequest{ID: uuid.NewString(), ContractID: r.PathValue("contractID"), ChangeNo: body.ChangeNo, Reason: body.Reason, Actor: body.Actor, Items: body.Items})
	respondStatus(w, http.StatusCreated, item, err)
}
func (s *Server) getContractChange(w http.ResponseWriter, r *http.Request) {
	item, err := sqlite.NewContractChangeRepository(s.store).Get(r.Context(), r.PathValue("changeID"))
	respond(w, item, err)
}
func (s *Server) createContractChangeCase(w http.ResponseWriter, r *http.Request) {
	var b struct {
		ChangeNo       string                           `json:"change_no"`
		Reason         string                           `json:"reason"`
		Responsibility string                           `json:"responsibility"`
		EffectiveDate  string                           `json:"effective_date"`
		Actor          string                           `json:"actor"`
		Items          []sqlite.ContractChangeItemInput `json:"items"`
	}
	if e := decodeJSON(r, &b); e != nil {
		writeError(w, e)
		return
	}
	if b.Actor == "" {
		b.Actor = "api"
	}
	item, e := sqlite.NewContractChangeCaseRepository(s.store).Create(r.Context(), sqlite.ContractChangeCaseRequest{ID: uuid.NewString(), ContractID: r.PathValue("contractID"), ChangeNo: b.ChangeNo, Reason: b.Reason, Responsibility: b.Responsibility, EffectiveDate: b.EffectiveDate, Actor: b.Actor, Items: b.Items})
	respondStatus(w, http.StatusCreated, item, e)
}
func (s *Server) getContractChangeCase(w http.ResponseWriter, r *http.Request) {
	item, e := sqlite.NewContractChangeCaseRepository(s.store).Get(r.Context(), r.PathValue("caseID"))
	respond(w, item, e)
}
func (s *Server) transitionContractChangeCase(w http.ResponseWriter, r *http.Request) {
	var b struct {
		Status     string `json:"status"`
		RowVersion int64  `json:"row_version"`
		Actor      string `json:"actor"`
	}
	if e := decodeJSON(r, &b); e != nil {
		writeError(w, e)
		return
	}
	if b.Actor == "" {
		b.Actor = "api"
	}
	item, e := sqlite.NewContractChangeCaseRepository(s.store).Transition(r.Context(), r.PathValue("caseID"), b.Status, b.RowVersion, b.Actor)
	respond(w, item, e)
}
