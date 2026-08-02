package httpapi

import (
	"net/http"

	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func (s *Server) mrsCatalogRoutes(){
	s.mux.HandleFunc("GET /api/mrs/catalog/{id}",s.getMRSCatalogItem)
	s.mux.HandleFunc("PUT /api/mrs/catalog/{id}",s.putMRSCatalogItem)
	s.mux.HandleFunc("GET /api/mrs/catalog/{id}/price-history",s.listMRSPriceHistory)
	s.mux.HandleFunc("PUT /api/mrs/catalog/{id}/bookmark",s.putMRSBookmark)
	s.mux.HandleFunc("PUT /api/mrs/analysis-recipes/{id}",s.putMRSRecipe)
	s.mux.HandleFunc("GET /api/mrs/analysis-recipes/{id}/calculate",s.calculateMRSRecipe)
}

func (s *Server) getMRSCatalogItem(w http.ResponseWriter,r *http.Request){item,err:=sqlite.NewMRSCatalogRepository(s.store).GetItem(r.Context(),r.PathValue("id"));respond(w,item,err)}
func (s *Server) putMRSCatalogItem(w http.ResponseWriter,r *http.Request){
	var body struct{sqlite.MRSCatalogItem;ActorID string `json:"actor_id"`;EffectiveDate string `json:"effective_date"`}
	if err:=decodeJSON(r,&body);err!=nil{writeError(w,err);return};body.ID=r.PathValue("id")
	item,err:=sqlite.NewMRSCatalogRepository(s.store).SaveItem(r.Context(),body.MRSCatalogItem,body.ActorID,body.EffectiveDate);respond(w,item,err)
}
func (s *Server) listMRSPriceHistory(w http.ResponseWriter,r *http.Request){rows,err:=sqlite.NewMRSCatalogRepository(s.store).History(r.Context(),r.PathValue("id"));respond(w,rows,err)}
func (s *Server) putMRSBookmark(w http.ResponseWriter,r *http.Request){var body struct{ActorID string `json:"actor_id"`;Bookmarked bool `json:"bookmarked"`};if err:=decodeJSON(r,&body);err!=nil{writeError(w,err);return};err:=sqlite.NewMRSCatalogRepository(s.store).SetBookmark(r.Context(),body.ActorID,r.PathValue("id"),body.Bookmarked);respond(w,map[string]any{"actor_id":body.ActorID,"catalog_item_id":r.PathValue("id"),"bookmarked":body.Bookmarked},err)}
func (s *Server) putMRSRecipe(w http.ResponseWriter,r *http.Request){var body struct{Code string `json:"code"`;Name string `json:"name"`;Unit *string `json:"unit"`;PriceScale int `json:"price_scale"`;RowVersion int64 `json:"row_version"`;Components []sqlite.MRSAnalysisComponent `json:"components"`};if err:=decodeJSON(r,&body);err!=nil{writeError(w,err);return};item,err:=sqlite.NewMRSCatalogRepository(s.store).SaveRecipe(r.Context(),r.PathValue("id"),body.Code,body.Name,body.Unit,body.PriceScale,body.Components,body.RowVersion);respond(w,item,err)}
func (s *Server) calculateMRSRecipe(w http.ResponseWriter,r *http.Request){item,err:=sqlite.NewMRSCatalogRepository(s.store).CalculateRecipe(r.Context(),r.PathValue("id"));respond(w,item,err)}
