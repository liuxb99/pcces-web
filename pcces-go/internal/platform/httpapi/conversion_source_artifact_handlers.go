package httpapi

import (
	"net/http"

	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func (s *Server) conversionSourceArtifactRoutes() {
	s.mux.HandleFunc("POST /api/conversions/source-artifacts", s.createConversionSourceArtifact)
	s.mux.HandleFunc("GET /api/conversions/source-artifacts/{id}", s.getConversionSourceArtifact)
	s.mux.HandleFunc("GET /api/conversions/source-artifacts/{id}/download", s.downloadConversionSourceArtifact)
	s.mux.HandleFunc("POST /api/conversions/error-catalogues", s.createConversionErrorCatalogue)
	s.mux.HandleFunc("GET /api/conversions/error-catalogues/{id}", s.getConversionErrorCatalogue)
	s.mux.HandleFunc("GET /api/conversions/error-catalogues/{id}/download", s.downloadConversionErrorCatalogue)
}

func (s *Server) createConversionSourceArtifact(w http.ResponseWriter, r *http.Request) {
	var body struct {
		ID, SessionType, SessionID, OriginalFilename, ContentType string
		Format, FormatVersion, Content, ActorID                  string
	}
	if err := decodeJSON(r, &body); err != nil { writeError(w, err); return }
	item, err := sqlite.NewConversionSourceArtifactRepository(s.store).CreateSource(r.Context(), sqlite.ConversionSourceArtifact{ID:body.ID,SessionType:body.SessionType,SessionID:body.SessionID,OriginalFilename:body.OriginalFilename,ContentType:body.ContentType,Format:body.Format,FormatVersion:body.FormatVersion,CreatedBy:body.ActorID}, []byte(body.Content))
	respondCreated(w, item, err)
}

func (s *Server) getConversionSourceArtifact(w http.ResponseWriter, r *http.Request) {
	item, err := sqlite.NewConversionSourceArtifactRepository(s.store).GetSource(r.Context(), r.PathValue("id")); respond(w,item,err)
}

func (s *Server) downloadConversionSourceArtifact(w http.ResponseWriter, r *http.Request) {
	content, contentType, filename, err := sqlite.NewConversionSourceArtifactRepository(s.store).SourceContent(r.Context(), r.PathValue("id")); if err != nil { writeError(w,err); return }
	w.Header().Set("Content-Type",contentType); w.Header().Set("Content-Disposition",`attachment; filename="`+filename+`"`); w.WriteHeader(http.StatusOK); _,_=w.Write(content)
}

func (s *Server) createConversionErrorCatalogue(w http.ResponseWriter, r *http.Request) {
	var body struct { SessionType, SessionID, ActorID string; Errors, Warnings []map[string]any }
	if err:=decodeJSON(r,&body);err!=nil{writeError(w,err);return}
	item,err:=sqlite.NewConversionSourceArtifactRepository(s.store).CreateCatalogue(r.Context(),body.SessionType,body.SessionID,body.Errors,body.Warnings,body.ActorID); respondCreated(w,item,err)
}

func (s *Server) getConversionErrorCatalogue(w http.ResponseWriter, r *http.Request) {
	item,err:=sqlite.NewConversionSourceArtifactRepository(s.store).GetCatalogue(r.Context(),r.PathValue("id")); respond(w,item,err)
}

func (s *Server) downloadConversionErrorCatalogue(w http.ResponseWriter, r *http.Request) {
	content,filename,err:=sqlite.NewConversionSourceArtifactRepository(s.store).CatalogueCSV(r.Context(),r.PathValue("id")); if err!=nil{writeError(w,err);return}
	w.Header().Set("Content-Type","text/csv; charset=utf-8"); w.Header().Set("Content-Disposition",`attachment; filename="`+filename+`"`); w.WriteHeader(http.StatusOK); _,_=w.Write(content)
}
