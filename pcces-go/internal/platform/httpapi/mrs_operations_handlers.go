package httpapi

import (
	"encoding/json"
	"net/http"
	"time"

	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func (s *Server) mrsOperationsRoutes(){
	s.mux.HandleFunc("GET /api/mrs/usage-summary",s.getMRSUsageSummary)
	s.mux.HandleFunc("POST /api/mrs/analysis-recipes/{id}/versions",s.createMRSRecipeVersion)
	s.mux.HandleFunc("GET /api/mrs/analysis-recipes/{id}/versions",s.listMRSRecipeVersions)
	s.mux.HandleFunc("GET /api/mrs/analysis-recipe-versions/{leftID}/diff/{rightID}",s.diffMRSRecipeVersions)
	s.mux.HandleFunc("GET /api/mrs/catalog/{id}/lineage",s.getMRSPriceLineage)
	s.mux.HandleFunc("POST /api/mrs/import-jobs",s.createMRSImportJob)
	s.mux.HandleFunc("GET /api/mrs/import-jobs/{id}",s.getMRSImportJob)
	s.mux.HandleFunc("POST /api/mrs/import-jobs/{id}/run",s.runMRSImportJob)
	s.mux.HandleFunc("POST /api/mrs/import-jobs/{id}/cancel",s.cancelMRSImportJob)
}
func (s *Server) getMRSUsageSummary(w http.ResponseWriter,r *http.Request){item,err:=sqlite.NewMRSOperationsRepository(s.store).UsageSummary(r.Context());respond(w,item,err)}
func (s *Server) createMRSRecipeVersion(w http.ResponseWriter,r *http.Request){var body struct{ID string `json:"id"`;Label string `json:"label"`;ActorID string `json:"actor_id"`};if err:=decodeJSON(r,&body);err!=nil{writeError(w,err);return};if body.ID==""{body.ID="mrsv-"+time.Now().UTC().Format("20060102150405.000000000")};item,err:=sqlite.NewMRSOperationsRepository(s.store).CreateRecipeVersion(r.Context(),body.ID,r.PathValue("id"),body.Label,body.ActorID);respond(w,item,err)}
func (s *Server) listMRSRecipeVersions(w http.ResponseWriter,r *http.Request){items,err:=sqlite.NewMRSOperationsRepository(s.store).ListRecipeVersions(r.Context(),r.PathValue("id"));respond(w,items,err)}
func (s *Server) diffMRSRecipeVersions(w http.ResponseWriter,r *http.Request){item,err:=sqlite.NewMRSOperationsRepository(s.store).DiffRecipeVersions(r.Context(),r.PathValue("leftID"),r.PathValue("rightID"));respond(w,item,err)}
func (s *Server) getMRSPriceLineage(w http.ResponseWriter,r *http.Request){item,err:=sqlite.NewMRSOperationsRepository(s.store).PriceLineage(r.Context(),r.PathValue("id"));respond(w,item,err)}
func (s *Server) createMRSImportJob(w http.ResponseWriter,r *http.Request){var body struct{ID string `json:"id"`;Format string `json:"format"`;Payload string `json:"payload"`;Overwrite bool `json:"overwrite"`;ActorID string `json:"actor_id"`};if err:=decodeJSON(r,&body);err!=nil{writeError(w,err);return};if body.ID==""{body.ID="mrsj-"+time.Now().UTC().Format("20060102150405.000000000")};var rows []map[string]any;if err:=json.Unmarshal([]byte(body.Payload),&rows);err!=nil{writeError(w,err);return};item,err:=sqlite.NewMRSOperationsRepository(s.store).CreateImportJob(r.Context(),body.ID,body.Format,body.Payload,body.ActorID,body.Overwrite,len(rows));respond(w,item,err)}
func (s *Server) getMRSImportJob(w http.ResponseWriter,r *http.Request){item,err:=sqlite.NewMRSOperationsRepository(s.store).GetImportJob(r.Context(),r.PathValue("id"));respond(w,item,err)}
func (s *Server) runMRSImportJob(w http.ResponseWriter,r *http.Request){item,err:=sqlite.NewMRSOperationsRepository(s.store).RunImportJob(r.Context(),r.PathValue("id"));respond(w,item,err)}
func (s *Server) cancelMRSImportJob(w http.ResponseWriter,r *http.Request){item,err:=sqlite.NewMRSOperationsRepository(s.store).CancelImportJob(r.Context(),r.PathValue("id"));respond(w,item,err)}
