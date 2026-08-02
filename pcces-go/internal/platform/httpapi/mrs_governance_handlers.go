package httpapi

import (
	"net/http"
	"time"

	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func (s *Server) mrsGovernanceRoutes(){
	s.mux.HandleFunc("POST /api/mrs/catalog-releases",s.createMRSCatalogRelease)
	s.mux.HandleFunc("GET /api/mrs/catalog-releases/{id}",s.getMRSCatalogRelease)
	s.mux.HandleFunc("POST /api/mrs/catalog-releases/{id}/{command}",s.transitionMRSCatalogRelease)
	s.mux.HandleFunc("GET /api/mrs/catalog/{id}/validity",s.getMRSValidity)
	s.mux.HandleFunc("PUT /api/mrs/catalog/{id}/validity",s.putMRSValidity)
	s.mux.HandleFunc("GET /api/mrs/expiry-alerts",s.getMRSExpiryAlerts)
	s.mux.HandleFunc("GET /api/mrs/analysis-recipes/{id}/freeze",s.getMRSRecipeFreeze)
	s.mux.HandleFunc("PUT /api/mrs/analysis-recipes/{id}/freeze",s.putMRSRecipeFreeze)
}
func (s *Server) createMRSCatalogRelease(w http.ResponseWriter,r *http.Request){var body struct{ID,Label,ActorID string};if err:=decodeJSON(r,&body);err!=nil{writeError(w,err);return};if body.ID==""{body.ID="mrsrel-"+time.Now().UTC().Format("20060102150405.000000000")};item,err:=sqlite.NewMRSGovernanceRepository(s.store).CreateRelease(r.Context(),body.ID,body.Label,body.ActorID);respond(w,item,err)}
func (s *Server) getMRSCatalogRelease(w http.ResponseWriter,r *http.Request){item,err:=sqlite.NewMRSGovernanceRepository(s.store).GetRelease(r.Context(),r.PathValue("id"));respond(w,item,err)}
func (s *Server) transitionMRSCatalogRelease(w http.ResponseWriter,r *http.Request){var body struct{ActorID,Comment string;RowVersion int64 `json:"row_version"`};if err:=decodeJSON(r,&body);err!=nil{writeError(w,err);return};item,err:=sqlite.NewMRSGovernanceRepository(s.store).TransitionRelease(r.Context(),r.PathValue("id"),r.PathValue("command"),body.ActorID,body.Comment,body.RowVersion);respond(w,item,err)}
func (s *Server) getMRSValidity(w http.ResponseWriter,r *http.Request){item,err:=sqlite.NewMRSGovernanceRepository(s.store).GetValidity(r.Context(),r.PathValue("id"));respond(w,item,err)}
func (s *Server) putMRSValidity(w http.ResponseWriter,r *http.Request){var body struct{ValidFrom,ValidTo *string;Status,ActorID string;RowVersion int64 `json:"row_version"`};if err:=decodeJSON(r,&body);err!=nil{writeError(w,err);return};item,err:=sqlite.NewMRSGovernanceRepository(s.store).SetValidity(r.Context(),r.PathValue("id"),body.ValidFrom,body.ValidTo,body.Status,body.ActorID,body.RowVersion);respond(w,item,err)}
func (s *Server) getMRSExpiryAlerts(w http.ResponseWriter,r *http.Request){items,err:=sqlite.NewMRSGovernanceRepository(s.store).ExpiryAlerts(r.Context(),r.URL.Query().Get("as_of"));respond(w,items,err)}
func (s *Server) getMRSRecipeFreeze(w http.ResponseWriter,r *http.Request){item,err:=sqlite.NewMRSGovernanceRepository(s.store).GetRecipeFreeze(r.Context(),r.PathValue("id"));respond(w,item,err)}
func (s *Server) putMRSRecipeFreeze(w http.ResponseWriter,r *http.Request){var body struct{VersionID string `json:"version_id"`;Frozen bool `json:"frozen"`;Reason *string `json:"reason"`;ActorID string `json:"actor_id"`;RowVersion int64 `json:"row_version"`};if err:=decodeJSON(r,&body);err!=nil{writeError(w,err);return};item,err:=sqlite.NewMRSGovernanceRepository(s.store).SetRecipeFreeze(r.Context(),r.PathValue("id"),body.VersionID,body.Frozen,body.Reason,body.ActorID,body.RowVersion);respond(w,item,err)}
