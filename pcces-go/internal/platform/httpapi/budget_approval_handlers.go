package httpapi

import (
	"fmt"
	"net/http"
	"strings"
	"time"

	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func (s *Server) budgetApprovalRoutes() {
	s.mux.HandleFunc("GET /api/decimal-budget/projects/{projectCode}/approval", s.getBudgetApproval)
	s.mux.HandleFunc("POST /api/decimal-budget/projects/{projectCode}/approval/{command}", s.transitionBudgetApproval)
	s.mux.HandleFunc("PUT /api/decimal-budget/projects/{projectCode}/items/{itemID}/lock", s.setBudgetItemLock)
	s.mux.HandleFunc("POST /api/decimal-budget/projects/{projectCode}/autosave-check", s.checkBudgetAutosave)
	s.mux.HandleFunc("GET /api/decimal-budget/projects/{projectCode}/workflow-audit", s.listBudgetWorkflowAudit)
}

func (s *Server) getBudgetApproval(w http.ResponseWriter, r *http.Request) {
	item, err := sqlite.NewBudgetApprovalRepository(s.store).State(r.Context(), r.PathValue("projectCode"))
	respond(w, item, err)
}

func (s *Server) transitionBudgetApproval(w http.ResponseWriter, r *http.Request) {
	var body struct {
		ActorID     string `json:"actor_id"`
		Role        string `json:"role"`
		Comment     string `json:"comment"`
		RowVersion  int64  `json:"row_version"`
		SelfCheckID string `json:"self_check_id"`
	}
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	command := strings.ToUpper(r.PathValue("command"))
	project := r.PathValue("projectCode")
	if command == "SUBMIT" || command == "APPROVE" {
		checkID := body.SelfCheckID
		if checkID == "" {
			checkID = fmt.Sprintf("approval-%d", time.Now().UTC().UnixNano())
		}
		check, err := sqlite.NewBudgetValidationRepository(s.store).Check(r.Context(), checkID, project, body.ActorID)
		if err != nil {
			writeError(w, err)
			return
		}
		if !check.Passed {
			writeJSON(w, http.StatusUnprocessableEntity, map[string]any{"code": "SELF_CHECK_FAILED", "detail": "budget self-check contains blocking issues", "self_check": check})
			return
		}
	}
	item, err := sqlite.NewBudgetApprovalRepository(s.store).Transition(r.Context(), project, command, body.ActorID, body.Role, body.Comment, body.RowVersion)
	respond(w, item, err)
}

func (s *Server) setBudgetItemLock(w http.ResponseWriter, r *http.Request) {
	var body struct {
		ActorID string `json:"actor_id"`
		Role    string `json:"role"`
		Locked  bool   `json:"locked"`
		Reason  string `json:"reason"`
	}
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	item, err := sqlite.NewBudgetApprovalRepository(s.store).SetItemLock(r.Context(), r.PathValue("projectCode"), r.PathValue("itemID"), body.Locked, body.ActorID, body.Role, body.Reason)
	respond(w, item, err)
}

func (s *Server) checkBudgetAutosave(w http.ResponseWriter, r *http.Request) {
	var body struct {
		ItemID            string `json:"item_id"`
		RowVersion        int64  `json:"row_version"`
		CurrentRowVersion int64  `json:"current_row_version"`
	}
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	repo := sqlite.NewBudgetApprovalRepository(s.store)
	if err := repo.AssertWritable(r.Context(), r.PathValue("projectCode"), body.ItemID); err != nil {
		writeError(w, err)
		return
	}
	if body.RowVersion != body.CurrentRowVersion {
		writeJSON(w, http.StatusConflict, map[string]any{"allowed": false, "code": "CONFLICT", "current_row_version": body.CurrentRowVersion})
		return
	}
	writeJSON(w, http.StatusOK, map[string]any{"allowed": true, "code": "OK", "current_row_version": body.CurrentRowVersion})
}

func (s *Server) listBudgetWorkflowAudit(w http.ResponseWriter, r *http.Request) {
	rows, err := sqlite.NewBudgetApprovalRepository(s.store).Audits(r.Context(), r.PathValue("projectCode"))
	respond(w, rows, err)
}
