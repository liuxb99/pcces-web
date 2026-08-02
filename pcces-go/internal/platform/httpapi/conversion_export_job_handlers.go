package httpapi

import (
	"fmt"
	"net/http"

	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func (s *Server) conversionExportJobRoutes() {
	s.mux.HandleFunc("POST /api/conversions/export-jobs", s.createConversionExportJob)
	s.mux.HandleFunc("GET /api/conversions/export-jobs/{jobID}", s.getConversionExportJob)
	s.mux.HandleFunc("GET /api/conversions/export-jobs/{jobID}/download", s.downloadConversionExportJob)
}

func (s *Server) createConversionExportJob(w http.ResponseWriter, r *http.Request) {
	var body sqlite.ConversionExportRequest
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	item, err := sqlite.NewConversionExportJobRepository(s.store).Create(r.Context(), body)
	if err != nil {
		writeError(w, err)
		return
	}
	w.WriteHeader(http.StatusCreated)
	respond(w, item, nil)
}

func (s *Server) getConversionExportJob(w http.ResponseWriter, r *http.Request) {
	item, err := sqlite.NewConversionExportJobRepository(s.store).Get(r.Context(), r.PathValue("jobID"))
	respond(w, item, err)
}

func (s *Server) downloadConversionExportJob(w http.ResponseWriter, r *http.Request) {
	content, contentType, filename, err := sqlite.NewConversionExportJobRepository(s.store).Artifact(r.Context(), r.PathValue("jobID"))
	if err != nil {
		writeError(w, err)
		return
	}
	w.Header().Set("Content-Type", contentType)
	w.Header().Set("Content-Disposition", fmt.Sprintf("attachment; filename=%q", filename))
	w.WriteHeader(http.StatusOK)
	_, _ = w.Write(content)
}
