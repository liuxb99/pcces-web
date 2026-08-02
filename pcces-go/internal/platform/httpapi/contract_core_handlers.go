package httpapi

import (
	"net/http"

	"github.com/google/uuid"
	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func (s *Server) contractCoreRoutes() {
	s.mux.HandleFunc("GET /api/contracts/eligibility", s.contractEligibility)
	s.mux.HandleFunc("POST /api/contracts", s.createContractCore)
	s.mux.HandleFunc("GET /api/contracts/{contractID}", s.getContractCore)
}

func (s *Server) contractEligibility(w http.ResponseWriter, r *http.Request) {
	item, err := sqlite.NewContractCoreRepository(s.store).Eligibility(r.Context(), r.URL.Query().Get("project_code"), r.URL.Query().Get("budget_version_id"))
	respond(w, item, err)
}

func (s *Server) createContractCore(w http.ResponseWriter, r *http.Request) {
	var body struct {
		ProjectCode     string                         `json:"project_code"`
		BudgetVersionID string                         `json:"budget_version_id"`
		ContractNo      string                         `json:"contract_no"`
		Name            string                         `json:"name"`
		Contractor      string                         `json:"contractor"`
		ContractAmount  string                         `json:"contract_amount"`
		Actor           string                         `json:"actor"`
		Items           []sqlite.ContractItemInput     `json:"items"`
	}
	if err := decodeJSON(r, &body); err != nil { writeError(w, err); return }
	if body.Actor == "" { body.Actor = "api" }
	item, err := sqlite.NewContractCoreRepository(s.store).Create(r.Context(), sqlite.ContractCreateRequest{ID:uuid.NewString(),ProjectCode:body.ProjectCode,BudgetVersionID:body.BudgetVersionID,ContractNo:body.ContractNo,Name:body.Name,Contractor:body.Contractor,Actor:body.Actor,ContractAmount:body.ContractAmount,Items:body.Items})
	respondStatus(w, http.StatusCreated, item, err)
}

func (s *Server) getContractCore(w http.ResponseWriter, r *http.Request) {
	item, err := sqlite.NewContractCoreRepository(s.store).Get(r.Context(), r.PathValue("contractID"))
	respond(w, item, err)
}
