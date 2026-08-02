package httpapi

import (
	"net/http"

	"github.com/liuxb99/pcces-web/pcces-go/internal/domain/money"
	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func (s *Server) decimalCoreRoutes() {
	s.mux.HandleFunc("GET /api/decimal-budget/items/{id}", s.getDecimalBudgetItem)
	s.mux.HandleFunc("PUT /api/decimal-budget/items/{id}", s.putDecimalBudgetItem)
	s.mux.HandleFunc("POST /api/decimal-budget/projects/{projectCode}/recalculate", s.recalculateDecimalBudget)
	s.mux.HandleFunc("POST /api/decimal-budget/calculate", s.calculateBudgetKind)
	s.mux.HandleFunc("GET /api/decimal-budget/traces/{id}", s.getBudgetTrace)
	s.mux.HandleFunc("GET /api/decimal-budget/projects/{projectCode}/traces", s.listBudgetTraces)
	s.mux.HandleFunc("GET /api/decimal-resources/{id}", s.getDecimalResource)
	s.mux.HandleFunc("PUT /api/decimal-resources/{id}", s.putDecimalResource)
	s.mux.HandleFunc("PUT /api/decimal-resources/breakdowns/{id}", s.putDecimalBreakdown)
	s.mux.HandleFunc("POST /api/decimal-resources/{id}/recalculate", s.recalculateDecimalResource)
	s.mux.HandleFunc("POST /api/decimal-resources/{id}/budget-links", s.linkResourceBudgetItem)
	s.mux.HandleFunc("POST /api/decimal-resources/{id}/propagate", s.propagateResourcePrice)
	s.mux.HandleFunc("GET /api/decimal-resources/projects/{projectCode}/lineage", s.listResourcePriceLineage)
}

func (s *Server) getDecimalBudgetItem(w http.ResponseWriter, r *http.Request) { item,err:=sqlite.NewBudgetDecimalRepository(s.store).Get(r.Context(),r.PathValue("id"));respond(w,item,err) }
func (s *Server) putDecimalBudgetItem(w http.ResponseWriter, r *http.Request) { var body sqlite.BudgetDecimalItem;if err:=decodeJSON(r,&body);err!=nil{writeError(w,err);return};body.ID=r.PathValue("id");item,err:=sqlite.NewBudgetDecimalRepository(s.store).Save(r.Context(),body);respond(w,item,err) }
func (s *Server) recalculateDecimalBudget(w http.ResponseWriter, r *http.Request) { total,err:=sqlite.NewBudgetDecimalRepository(s.store).RecalculateProject(r.Context(),r.PathValue("projectCode"));respond(w,map[string]any{"project_code":r.PathValue("projectCode"),"total_amount":total},err) }

func (s *Server) calculateBudgetKind(w http.ResponseWriter,r *http.Request){
	var body struct{ID string `json:"id"`;ProjectCode string `json:"project_code"`;ItemID *string `json:"item_id"`;Kind string `json:"kind"`;Scale int `json:"scale"`;Input money.BudgetKindInput `json:"input"`}
	if err:=decodeJSON(r,&body);err!=nil{writeError(w,err);return}
	item,err:=sqlite.NewBudgetTraceRepository(s.store).Calculate(r.Context(),body.ID,body.ProjectCode,body.ItemID,body.Kind,body.Scale,body.Input)
	if err!=nil{writeError(w,err);return};writeJSON(w,http.StatusCreated,item)
}
func (s *Server) getBudgetTrace(w http.ResponseWriter,r *http.Request){item,err:=sqlite.NewBudgetTraceRepository(s.store).Get(r.Context(),r.PathValue("id"));respond(w,item,err)}
func (s *Server) listBudgetTraces(w http.ResponseWriter,r *http.Request){items,err:=sqlite.NewBudgetTraceRepository(s.store).ListProject(r.Context(),r.PathValue("projectCode"));respond(w,items,err)}

func (s *Server) getDecimalResource(w http.ResponseWriter, r *http.Request) { item,err:=sqlite.NewResourceDecimalRepository(s.store).GetResource(r.Context(),r.PathValue("id"));respond(w,item,err) }
func (s *Server) putDecimalResource(w http.ResponseWriter, r *http.Request) { var body sqlite.ResourceDecimal;if err:=decodeJSON(r,&body);err!=nil{writeError(w,err);return};body.ID=r.PathValue("id");item,err:=sqlite.NewResourceDecimalRepository(s.store).SaveResource(r.Context(),body);respond(w,item,err) }
func (s *Server) putDecimalBreakdown(w http.ResponseWriter, r *http.Request) { var body sqlite.ResourceBreakdownDecimal;if err:=decodeJSON(r,&body);err!=nil{writeError(w,err);return};body.ID=r.PathValue("id");item,err:=sqlite.NewResourceDecimalRepository(s.store).SaveBreakdown(r.Context(),body);respond(w,item,err) }
func (s *Server) recalculateDecimalResource(w http.ResponseWriter, r *http.Request) { item,err:=sqlite.NewResourceDecimalRepository(s.store).RecalculateResource(r.Context(),r.PathValue("id"));respond(w,item,err) }

func (s *Server) linkResourceBudgetItem(w http.ResponseWriter,r *http.Request){
	var body struct{ProjectCode string `json:"project_code"`;BudgetItemID string `json:"budget_item_id"`}
	if err:=decodeJSON(r,&body);err!=nil{writeError(w,err);return}
	err:=sqlite.NewResourceBudgetLineageRepository(s.store).Link(r.Context(),body.ProjectCode,r.PathValue("id"),body.BudgetItemID)
	if err!=nil{writeError(w,err);return}
	writeJSON(w,http.StatusCreated,map[string]string{"project_code":body.ProjectCode,"resource_id":r.PathValue("id"),"budget_item_id":body.BudgetItemID})
}
func (s *Server) propagateResourcePrice(w http.ResponseWriter,r *http.Request){
	var body struct{Trigger string `json:"trigger"`};_ = decodeJSON(r,&body)
	rows,err:=sqlite.NewResourceBudgetLineageRepository(s.store).Propagate(r.Context(),r.PathValue("id"),body.Trigger)
	respond(w,map[string]any{"resource_id":r.PathValue("id"),"updated_items":len(rows),"lineage":rows},err)
}
func (s *Server) listResourcePriceLineage(w http.ResponseWriter,r *http.Request){rows,err:=sqlite.NewResourceBudgetLineageRepository(s.store).ListProject(r.Context(),r.PathValue("projectCode"));respond(w,rows,err)}
