package httpapi

import (
	"bytes"
	"encoding/json"
	"io"
	"net/http"
	"strings"

	"github.com/liuxb99/pcces-web/pcces-go/internal/domain/authorization"
	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func (s *Server) authorizationRoutes() {
	s.mux.HandleFunc("GET /api/actors/{actorID}/capabilities/{actionCode}", s.actorCapability)
	s.mux.HandleFunc("PUT /api/actors/{actorID}/function-codes/{functionCode}", s.putFunctionGrant)
	s.mux.HandleFunc("PUT /api/actors/{actorID}/modules/{moduleCode}", s.putModuleEntitlement)
	s.mux.HandleFunc("POST /api/work-contexts/{id}/save-draft", s.authorizeWorkContext(s.saveDraftWorkContext))
	s.mux.HandleFunc("POST /api/work-contexts/{id}/save", s.authorizeWorkContext(s.saveWorkContext))
	s.mux.HandleFunc("POST /api/work-contexts/{id}/discard", s.authorizeWorkContext(s.discardWorkContext))
	s.mux.HandleFunc("POST /api/work-contexts/{id}/cancel", s.authorizeWorkContext(s.cancelWorkContext))
	s.decimalCoreRoutes()
	s.budgetApprovalRoutes()
	s.budgetCrossProjectRoutes()
	s.bidLifecycleRoutes()
	s.mrsCatalogRoutes()
	s.mrsIntelligenceRoutes()
	s.mrsOperationsRoutes()
	s.mrsGovernanceRoutes()
	s.costStructureRoutes()
	s.costStructureDetailRoutes()
	s.budgetBidConversionRoutes()
	s.conversionWizardRoutes()
	s.conversionExportJobRoutes()
	s.bidImportRoundTripRoutes()
	s.bidImportApplyRoutes()
	s.combineBidRoutes()
	s.legacyExchangeAdapterRoutes()
}

func (s *Server) actorCapability(w http.ResponseWriter, r *http.Request) {
	item, err := sqlite.NewAuthorizationRepository(s.store).Decide(r.Context(), r.PathValue("actorID"), r.PathValue("actionCode"))
	respond(w, item, err)
}

func (s *Server) putFunctionGrant(w http.ResponseWriter, r *http.Request) {
	var body struct {
		Granted    bool  `json:"granted"`
		RowVersion int64 `json:"row_version"`
	}
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	err := sqlite.NewAuthorizationRepository(s.store).SetFunctionGrant(r.Context(), authorization.GrantRequest{ActorID: r.PathValue("actorID"), FunctionCode: r.PathValue("functionCode"), Granted: body.Granted, RowVersion: body.RowVersion})
	if err != nil {
		writeError(w, err)
		return
	}
	item, err := sqlite.NewAuthorizationRepository(s.store).GetActor(r.Context(), r.PathValue("actorID"))
	respond(w, item, err)
}

func (s *Server) putModuleEntitlement(w http.ResponseWriter, r *http.Request) {
	var body struct {
		Enabled    bool  `json:"enabled"`
		RowVersion int64 `json:"row_version"`
	}
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	err := sqlite.NewAuthorizationRepository(s.store).SetModuleEntitlement(r.Context(), authorization.EntitlementRequest{ActorID: r.PathValue("actorID"), ModuleCode: r.PathValue("moduleCode"), Enabled: body.Enabled, RowVersion: body.RowVersion})
	if err != nil {
		writeError(w, err)
		return
	}
	item, err := sqlite.NewAuthorizationRepository(s.store).GetActor(r.Context(), r.PathValue("actorID"))
	respond(w, item, err)
}

func (s *Server) authorizeWorkContext(next http.HandlerFunc) http.HandlerFunc {
	return func(w http.ResponseWriter, r *http.Request) {
		if !strings.HasPrefix(r.Header.Get("Content-Type"), "application/json") {
			writeError(w, errx.New(errx.CodeInvalidArgument, "Content-Type must be application/json", "P0-G1"))
			return
		}
		body, err := io.ReadAll(io.LimitReader(r.Body, 2<<20))
		if err != nil {
			writeError(w, errx.Wrap(errx.CodeInvalidArgument, "read authorization request", "P0-G3", err))
			return
		}
		r.Body = io.NopCloser(bytes.NewReader(body))
		var request struct {
			ActorID    string `json:"actor_id"`
			ActionCode string `json:"action_code"`
		}
		if err := json.Unmarshal(body, &request); err != nil {
			writeError(w, errx.Wrap(errx.CodeInvalidArgument, "invalid JSON request", "P0-G1", err))
			return
		}
		if strings.TrimSpace(request.ActorID) == "" || strings.TrimSpace(request.ActionCode) == "" {
			writeError(w, errx.New(errx.CodeInvalidArgument, "actor_id and action_code are required", "P0-G3"))
			return
		}
		decision, err := sqlite.NewAuthorizationRepository(s.store).Decide(r.Context(), request.ActorID, request.ActionCode)
		if err != nil {
			writeError(w, err)
			return
		}
		if !decision.Allowed {
			reason := decision.Reason
			if reason == "" {
				reason = "ACTION_FORBIDDEN"
			}
			writeError(w, errx.New(errx.CodeForbidden, reason, "P0-G3"))
			return
		}
		r.Body = io.NopCloser(bytes.NewReader(body))
		next(w, r)
	}
}
