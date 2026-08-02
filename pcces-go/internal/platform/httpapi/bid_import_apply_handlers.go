package httpapi

import (
	"net/http"
	"strings"

	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func (s *Server) bidImportApplyRoutes() {
	s.mux.HandleFunc("POST /api/conversions/import-sessions/{sessionID}/apply", s.applyBidImportSession)
	s.mux.HandleFunc("GET /api/conversions/import-apply-runs/{runID}", s.getBidImportApplyRun)
}

func (s *Server) applyBidImportSession(w http.ResponseWriter, r *http.Request) {
	var body struct {
		TargetBudgetVersionID string `json:"target_budget_version_id"`
		Mode                  string `json:"mode"`
		ActorID               string `json:"actor_id"`
	}
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	body.ActorID = strings.TrimSpace(body.ActorID)
	if body.ActorID == "" {
		writeError(w, errx.New(errx.CodeInvalidArgument, "actor_id is required", "P4-BID-IMPORT-APPLY"))
		return
	}
	item, err := sqlite.NewBidImportApplyRepository(s.store).Apply(r.Context(), sqlite.BidImportApplyRequest{
		ImportSessionID:       r.PathValue("sessionID"),
		TargetBudgetVersionID: body.TargetBudgetVersionID,
		Mode:                  body.Mode,
		ActorID:               body.ActorID,
	})
	respond(w, item, err)
}

func (s *Server) getBidImportApplyRun(w http.ResponseWriter, r *http.Request) {
	item, err := sqlite.NewBidImportApplyRepository(s.store).Get(r.Context(), r.PathValue("runID"))
	respond(w, item, err)
}
