package httpapi

import (
	"net/http"

	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func (s *Server) mrsCatalogRoutes(){
	s.mux.HandleFunc("GET /api/mrs/catalog/{id}",s.getMRSCatalogItem)
	s.mux.HandleFunc("PUT /api/mrs/catalog/{id}",s.putMRSCatalogItem)
	s.mux.HandleFunc("GET /api/mrs/catalog/{id}/price-history",s.listMRSPriceHistory)
	s.mux.HandleFunc("POST /api/mrs/catalog/{id}/price-history/{historyID}/apply",s.applyMRSPriceHistory)
	s.mux.HandleFunc("GET /api/mrs/catalog/{id}/bookmark",s.getMRSBookmark)
	s.mux.HandleFunc("PUT /api/mrs/catalog/{id}/bookmark",s.putMRSBookmark)
	s.mux.HandleFunc("GET /api/mrs/bookmarks",s.listMRSBookmarks)
	s.mux.HandleFunc("PUT /api/mrs/analysis-recipes/{id}",s.putMRSRecipe)
	s.mux.HandleFunc("GET /api/mrs/analysis-recipes/{id}/calculate",s.calculateMRSRecipe)
	s.mux.HandleFunc("POST /api/mrs/analysis-recipes/{id}/versions/{versionID}/apply-rates",s.applyMRSRateHistory)
	s.mux.HandleFunc("POST /api/mrs/code/validate",s.validateMRSCode)
	s.mux.HandleFunc("POST /api/mrs/code/fit",s.fitMRSCode)
	s.resourceOperationRoutes()
	s.resourceProjectReferenceRoutes()
}

func (s *Server) getMRSCatalogItem(w http.ResponseWriter,r *http.Request){item,err:=sqlite.NewMRSCatalogRepository(s.store).GetItem(r.Context(),r.PathValue("id"));respond(w,item,err)}
func (s *Server) putMRSCatalogItem(w http.ResponseWriter,r *http.Request){
	var body struct{sqlite.MRSCatalogItem;ActorID string `json:"actor_id"`;EffectiveDate string `json:"effective_date"`}
	if err:=decodeJSON(r,&body);err!=nil{writeError(w,err);return};body.ID=r.PathValue("id")
	item,err:=sqlite.NewMRSCatalogRepository(s.store).SaveItem(r.Context(),body.MRSCatalogItem,body.ActorID,body.EffectiveDate);respond(w,item,err)
}
func (s *Server) listMRSPriceHistory(w http.ResponseWriter,r *http.Request){rows,err:=sqlite.NewMRSCatalogRepository(s.store).History(r.Context(),r.PathValue("id"));respond(w,rows,err)}
func (s *Server) applyMRSPriceHistory(w http.ResponseWriter,r *http.Request){var body struct{ActorID string `json:"actor_id"`;RowVersion int64 `json:"row_version"`};if err:=decodeJSON(r,&body);err!=nil{writeError(w,err);return};item,err:=sqlite.NewMRSCatalogRepository(s.store).ApplyHistoricalPrice(r.Context(),r.PathValue("id"),r.PathValue("historyID"),body.ActorID,body.RowVersion);respond(w,item,err)}
func (s *Server) getMRSBookmark(w http.ResponseWriter,r *http.Request){actor:=r.URL.Query().Get("actor_id");bookmarked,err:=sqlite.NewMRSCatalogRepository(s.store).IsBookmarked(r.Context(),actor,r.PathValue("id"));respond(w,map[string]any{"actor_id":actor,"catalog_item_id":r.PathValue("id"),"bookmarked":bookmarked},err)}
func (s *Server) putMRSBookmark(w http.ResponseWriter,r *http.Request){var body struct{ActorID string `json:"actor_id"`;Bookmarked bool `json:"bookmarked"`};if err:=decodeJSON(r,&body);err!=nil{writeError(w,err);return};err:=sqlite.NewMRSCatalogRepository(s.store).SetBookmark(r.Context(),body.ActorID,r.PathValue("id"),body.Bookmarked);respond(w,map[string]any{"actor_id":body.ActorID,"catalog_item_id":r.PathValue("id"),"bookmarked":body.Bookmarked},err)}
func (s *Server) listMRSBookmarks(w http.ResponseWriter,r *http.Request){q:=r.URL.Query();items,err:=sqlite.NewMRSCatalogRepository(s.store).ListBookmarks(r.Context(),q.Get("actor_id"),q.Get("q"),q.Get("category"));respond(w,items,err)}
func (s *Server) putMRSRecipe(w http.ResponseWriter,r *http.Request){var body struct{Code string `json:"code"`;Name string `json:"name"`;Unit *string `json:"unit"`;PriceScale int `json:"price_scale"`;RowVersion int64 `json:"row_version"`;Components []sqlite.MRSAnalysisComponent `json:"components"`};if err:=decodeJSON(r,&body);err!=nil{writeError(w,err);return};item,err:=sqlite.NewMRSCatalogRepository(s.store).SaveRecipe(r.Context(),r.PathValue("id"),body.Code,body.Name,body.Unit,body.PriceScale,body.Components,body.RowVersion);respond(w,item,err)}
func (s *Server) calculateMRSRecipe(w http.ResponseWriter,r *http.Request){item,err:=sqlite.NewMRSCatalogRepository(s.store).CalculateRecipe(r.Context(),r.PathValue("id"));respond(w,item,err)}
func (s *Server) applyMRSRateHistory(w http.ResponseWriter,r *http.Request){var body struct{ActorID string `json:"actor_id"`;RowVersion int64 `json:"row_version"`};if err:=decodeJSON(r,&body);err!=nil{writeError(w,err);return};item,err:=sqlite.NewMRSCatalogRepository(s.store).ApplyHistoricalRates(r.Context(),r.PathValue("id"),r.PathValue("versionID"),body.RowVersion,body.ActorID);respond(w,item,err)}
func (s *Server) validateMRSCode(w http.ResponseWriter,r *http.Request){var body struct{Code string `json:"code"`;Unit string `json:"unit"`};if err:=decodeJSON(r,&body);err!=nil{writeError(w,err);return};respond(w,sqlite.ValidateMRSCode(body.Code,body.Unit),nil)}
func (s *Server) fitMRSCode(w http.ResponseWriter,r *http.Request){var body sqlite.MRSCodeFitRequest;if err:=decodeJSON(r,&body);err!=nil{writeError(w,err);return};respond(w,sqlite.FitMRSCode(body),nil)}
