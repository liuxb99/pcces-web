package httpapi

import (
	"net/http"

	"github.com/google/uuid"
	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func(s *Server)reportAdminRoutes(){
	s.mux.HandleFunc("POST /api/reports/jobs",s.createReportJob)
	s.mux.HandleFunc("GET /api/reports/jobs/{jobID}",s.getReportJob)
	s.mux.HandleFunc("POST /api/reports/jobs/{jobID}/render",s.renderReportJob)
	s.mux.HandleFunc("GET /api/reports/artifacts/{artifactID}/download",s.downloadReportArtifact)
	s.mux.HandleFunc("GET /api/admin/settings",s.listAdminSettings)
	s.mux.HandleFunc("PUT /api/admin/settings/{key}",s.putAdminSetting)
}
func(s *Server)createReportJob(w http.ResponseWriter,r *http.Request){var b struct{DefinitionCode string `json:"definition_code"`;ProjectCode string `json:"project_code"`;BusinessVersionID string `json:"business_version_id"`;Format string `json:"format"`;Actor string `json:"actor"`;Snapshot map[string]any `json:"snapshot"`;Parameters map[string]any `json:"parameters"`};if e:=decodeJSON(r,&b);e!=nil{writeError(w,e);return};if b.Actor==""{b.Actor="api"};item,e:=sqlite.NewReportAdminRepository(s.store).CreateReportJob(r.Context(),uuid.NewString(),b.DefinitionCode,b.ProjectCode,b.BusinessVersionID,b.Format,b.Actor,b.Snapshot,b.Parameters);respondStatus(w,http.StatusCreated,item,e)}
func(s *Server)getReportJob(w http.ResponseWriter,r *http.Request){item,e:=sqlite.NewReportAdminRepository(s.store).GetReportJob(r.Context(),r.PathValue("jobID"));respond(w,item,e)}
func(s *Server)renderReportJob(w http.ResponseWriter,r *http.Request){var b struct{RowVersion int64 `json:"row_version"`};if e:=decodeJSON(r,&b);e!=nil{writeError(w,e);return};item,e:=sqlite.NewReportAdminRepository(s.store).RenderReport(r.Context(),r.PathValue("jobID"),uuid.NewString(),b.RowVersion);respond(w,item,e)}
func(s *Server)downloadReportArtifact(w http.ResponseWriter,r *http.Request){content,ctype,name,e:=sqlite.NewReportAdminRepository(s.store).ReportArtifact(r.Context(),r.PathValue("artifactID"),"api");if e!=nil{writeError(w,e);return};w.Header().Set("Content-Type",ctype);w.Header().Set("Content-Disposition","attachment; filename=\""+name+"\"");w.WriteHeader(http.StatusOK);_,_=w.Write(content)}
func(s *Server)listAdminSettings(w http.ResponseWriter,r *http.Request){items,e:=sqlite.NewReportAdminRepository(s.store).ListSettings(r.Context());respond(w,items,e)}
func(s *Server)putAdminSetting(w http.ResponseWriter,r *http.Request){var b struct{Value any `json:"value"`;RowVersion int64 `json:"row_version"`;Actor string `json:"actor"`};if e:=decodeJSON(r,&b);e!=nil{writeError(w,e);return};if b.Actor==""{b.Actor="api"};item,e:=sqlite.NewReportAdminRepository(s.store).SetSetting(r.Context(),r.PathValue("key"),b.Value,b.RowVersion,b.Actor);respond(w,item,e)}
