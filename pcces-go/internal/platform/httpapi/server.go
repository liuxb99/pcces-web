package httpapi

import (
	"encoding/json"
	"errors"
	"io"
	"log/slog"
	"net/http"
	"strings"

	"github.com/liuxb99/pcces-web/pcces-go/internal/domain/workcontext"
	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

// Server exposes the Local Go application to a future desktop shell and CLI.
type Server struct {
	logger    *slog.Logger
	store     *sqlite.Store
	catalog   *sqlite.CatalogRepository
	contexts  *sqlite.WorkContextRepository
	settings  *sqlite.SettingsRepository
	recovery  *sqlite.RecoveryRepository
	mux       *http.ServeMux
}

func New(logger *slog.Logger, store *sqlite.Store) *Server {
	if logger == nil {
		logger = slog.Default()
	}
	s := &Server{
		logger:   logger,
		store:    store,
		catalog:  sqlite.NewCatalogRepository(store),
		contexts: sqlite.NewWorkContextRepository(store),
		settings: sqlite.NewSettingsRepository(store),
		recovery: sqlite.NewRecoveryRepository(store),
		mux:      http.NewServeMux(),
	}
	s.routes()
	return s
}

func (s *Server) Handler() http.Handler {
	return s.recoverer(s.requestID(s.mux))
}

func (s *Server) routes() {
	s.mux.HandleFunc("GET /api/health", s.health)
	s.mux.HandleFunc("GET /api/catalog/modules", s.listModules)
	s.mux.HandleFunc("GET /api/catalog/function-codes", s.listFunctionCodes)
	s.mux.HandleFunc("GET /api/catalog/actions", s.listActions)
	s.mux.HandleFunc("GET /api/capabilities/{actionCode}", s.capability)
	s.mux.HandleFunc("GET /api/work-contexts/{id}", s.getWorkContext)
	s.mux.HandleFunc("PUT /api/work-contexts/{id}", s.putWorkContext)
	s.mux.HandleFunc("DELETE /api/work-contexts/{id}", s.deleteWorkContext)

	s.mux.HandleFunc("GET /api/settings", s.listSettings)
	s.mux.HandleFunc("GET /api/settings/{key}", s.getSetting)
	s.mux.HandleFunc("PUT /api/settings/{key}", s.putSetting)

	s.mux.HandleFunc("GET /api/system/integrity", s.integrityCheck)
	s.mux.HandleFunc("POST /api/system/backups", s.createBackup)

	s.mux.HandleFunc("GET /api/recovery-snapshots", s.listRecoverySnapshots)
	s.mux.HandleFunc("GET /api/recovery-snapshots/{id}", s.getRecoverySnapshot)
	s.mux.HandleFunc("POST /api/recovery-snapshots", s.createRecoverySnapshot)
	s.mux.HandleFunc("POST /api/recovery-snapshots/{id}/restore", s.restoreRecoverySnapshot)
	s.mux.HandleFunc("POST /api/recovery-snapshots/{id}/discard", s.discardRecoverySnapshot)
}

func (s *Server) health(w http.ResponseWriter, _ *http.Request) {
	writeJSON(w, http.StatusOK, map[string]any{
		"status": "ok", "edition": "local-go", "database": "sqlite",
	})
}

func (s *Server) listModules(w http.ResponseWriter, r *http.Request) {
	items, err := s.catalog.ListModules(r.Context())
	respond(w, items, err)
}

func (s *Server) listFunctionCodes(w http.ResponseWriter, r *http.Request) {
	items, err := s.catalog.ListFunctionCodes(r.Context())
	respond(w, items, err)
}

func (s *Server) listActions(w http.ResponseWriter, r *http.Request) {
	items, err := s.catalog.ListActions(r.Context())
	respond(w, items, err)
}

func (s *Server) capability(w http.ResponseWriter, r *http.Request) {
	item, err := s.catalog.Capability(r.Context(), r.PathValue("actionCode"))
	respond(w, item, err)
}

func (s *Server) getWorkContext(w http.ResponseWriter, r *http.Request) {
	item, err := s.contexts.Get(r.Context(), r.PathValue("id"))
	respond(w, item, err)
}

func (s *Server) putWorkContext(w http.ResponseWriter, r *http.Request) {
	var body struct {
		ActorID      string  `json:"actor_id"`
		ActionCode   string  `json:"action_code"`
		ProjectCode  *string `json:"project_code"`
		ResourceType *string `json:"resource_type"`
		ResourceID   *string `json:"resource_id"`
		Dirty        bool    `json:"dirty"`
		DraftPayload *string `json:"draft_payload"`
		RowVersion   int64   `json:"row_version"`
	}
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	item, err := s.contexts.Save(r.Context(), workcontext.SaveRequest{
		ID: r.PathValue("id"), ActorID: body.ActorID, ActionCode: body.ActionCode,
		ProjectCode: body.ProjectCode, ResourceType: body.ResourceType,
		ResourceID: body.ResourceID, Dirty: body.Dirty,
		DraftPayload: body.DraftPayload, RowVersion: body.RowVersion,
	})
	respond(w, item, err)
}

func (s *Server) deleteWorkContext(w http.ResponseWriter, r *http.Request) {
	var body struct {
		RowVersion int64 `json:"row_version"`
	}
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	if err := s.contexts.Delete(r.Context(), r.PathValue("id"), body.RowVersion); err != nil {
		writeError(w, err)
		return
	}
	w.WriteHeader(http.StatusNoContent)
}

func (s *Server) listSettings(w http.ResponseWriter, r *http.Request) {
	items, err := s.settings.List(r.Context())
	respond(w, items, err)
}

func (s *Server) getSetting(w http.ResponseWriter, r *http.Request) {
	item, err := s.settings.Get(r.Context(), r.PathValue("key"))
	respond(w, item, err)
}

func (s *Server) putSetting(w http.ResponseWriter, r *http.Request) {
	var body struct {
		Value       string `json:"value"`
		ValueType   string `json:"value_type"`
		Description string `json:"description"`
		RowVersion  int64  `json:"row_version"`
	}
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	item, err := s.settings.Save(r.Context(), sqlite.Setting{
		Key: r.PathValue("key"), Value: body.Value, ValueType: body.ValueType,
		Description: body.Description, RowVersion: body.RowVersion,
	})
	respond(w, item, err)
}

func (s *Server) integrityCheck(w http.ResponseWriter, r *http.Request) {
	if err := s.store.IntegrityCheck(r.Context()); err != nil {
		writeError(w, err)
		return
	}
	writeJSON(w, http.StatusOK, map[string]any{"status": "ok", "database": s.store.Path()})
}

func (s *Server) createBackup(w http.ResponseWriter, r *http.Request) {
	var body struct {
		Destination string `json:"destination"`
	}
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	item, err := s.store.Backup(r.Context(), body.Destination)
	if err != nil {
		writeError(w, err)
		return
	}
	writeJSON(w, http.StatusCreated, item)
}

func (s *Server) listRecoverySnapshots(w http.ResponseWriter, r *http.Request) {
	actorID := strings.TrimSpace(r.URL.Query().Get("actor_id"))
	if actorID == "" {
		writeError(w, errx.New(errx.CodeInvalidArgument, "actor_id query parameter is required", "P0-G4"))
		return
	}
	items, err := s.recovery.ListPending(r.Context(), actorID)
	respond(w, items, err)
}

func (s *Server) getRecoverySnapshot(w http.ResponseWriter, r *http.Request) {
	item, err := s.recovery.Get(r.Context(), r.PathValue("id"))
	respond(w, item, err)
}

func (s *Server) createRecoverySnapshot(w http.ResponseWriter, r *http.Request) {
	var body sqlite.RecoverySnapshot
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	item, err := s.recovery.Create(r.Context(), body)
	if err != nil {
		writeError(w, err)
		return
	}
	writeJSON(w, http.StatusCreated, item)
}

func (s *Server) restoreRecoverySnapshot(w http.ResponseWriter, r *http.Request) {
	var body struct {
		RowVersion int64 `json:"row_version"`
	}
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	item, err := s.recovery.MarkRestored(r.Context(), r.PathValue("id"), body.RowVersion)
	respond(w, item, err)
}

func (s *Server) discardRecoverySnapshot(w http.ResponseWriter, r *http.Request) {
	var body struct {
		RowVersion int64 `json:"row_version"`
	}
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	item, err := s.recovery.MarkDiscarded(r.Context(), r.PathValue("id"), body.RowVersion)
	respond(w, item, err)
}

func respond(w http.ResponseWriter, payload any, err error) {
	if err != nil {
		writeError(w, err)
		return
	}
	writeJSON(w, http.StatusOK, payload)
}

func decodeJSON(r *http.Request, destination any) error {
	if !strings.HasPrefix(r.Header.Get("Content-Type"), "application/json") {
		return errx.New(errx.CodeInvalidArgument, "Content-Type must be application/json", "P0-G1")
	}
	decoder := json.NewDecoder(io.LimitReader(r.Body, 2<<20))
	decoder.DisallowUnknownFields()
	if err := decoder.Decode(destination); err != nil {
		return errx.Wrap(errx.CodeInvalidArgument, "invalid JSON request", "P0-G1", err)
	}
	return nil
}

func writeJSON(w http.ResponseWriter, status int, payload any) {
	w.Header().Set("Content-Type", "application/json")
	w.WriteHeader(status)
	_ = json.NewEncoder(w).Encode(payload)
}

func writeError(w http.ResponseWriter, err error) {
	status := http.StatusInternalServerError
	payload := errx.New(errx.CodeInternal, "internal error", "P0-G1")
	var appErr *errx.Error
	if errors.As(err, &appErr) {
		payload = appErr
		switch appErr.Code {
		case errx.CodeInvalidArgument:
			status = http.StatusBadRequest
		case errx.CodeUnauthorized:
			status = http.StatusUnauthorized
		case errx.CodeForbidden:
			status = http.StatusForbidden
		case errx.CodeNotFound:
			status = http.StatusNotFound
		case errx.CodeConflict:
			status = http.StatusConflict
		}
	}
	writeJSON(w, status, payload)
}

func (s *Server) recoverer(next http.Handler) http.Handler {
	return http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		defer func() {
			if recovered := recover(); recovered != nil {
				s.logger.Error("localhost API panic", "panic", recovered, "path", r.URL.Path)
				writeError(w, errx.New(errx.CodeInternal, "internal error", "P0-G1"))
			}
		}()
		next.ServeHTTP(w, r)
	})
}

func (s *Server) requestID(next http.Handler) http.Handler {
	return http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		if r.Header.Get("X-Request-ID") == "" {
			r.Header.Set("X-Request-ID", r.RemoteAddr)
		}
		w.Header().Set("X-Request-ID", r.Header.Get("X-Request-ID"))
		next.ServeHTTP(w, r)
	})
}
