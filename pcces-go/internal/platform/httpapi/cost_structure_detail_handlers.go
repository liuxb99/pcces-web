package httpapi

import (
	"net/http"

	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func (s *Server) costStructureDetailRoutes() {
	s.mux.HandleFunc("POST /api/cost-structures/types/{typeID}/import", s.importCostStructureDefinition)
	s.mux.HandleFunc("GET /api/cost-structures/types/{typeID}/categories", s.listCostStructureCategories)
	s.mux.HandleFunc("GET /api/cost-structures/projects/{projectCode}/items/{itemID}/cost-property", s.getBudgetItemCostProperty)
	s.mux.HandleFunc("PUT /api/cost-structures/projects/{projectCode}/items/{itemID}/cost-property", s.putBudgetItemCostProperty)
}

func (s *Server) importCostStructureDefinition(w http.ResponseWriter, r *http.Request) {
	var body sqlite.CostStructureImportRequest
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	item, err := sqlite.NewCostStructureDetailRepository(s.store).ImportDefinition(r.Context(), r.PathValue("typeID"), body)
	respond(w, item, err)
}

func (s *Server) listCostStructureCategories(w http.ResponseWriter, r *http.Request) {
	items, err := sqlite.NewCostStructureDetailRepository(s.store).ListCategories(r.Context(), r.PathValue("typeID"))
	respond(w, items, err)
}

func (s *Server) getBudgetItemCostProperty(w http.ResponseWriter, r *http.Request) {
	item, err := sqlite.NewCostStructureDetailRepository(s.store).GetItemProperty(r.Context(), r.PathValue("projectCode"), r.PathValue("itemID"))
	respond(w, item, err)
}

func (s *Server) putBudgetItemCostProperty(w http.ResponseWriter, r *http.Request) {
	var body sqlite.BudgetItemCostProperty
	if err := decodeJSON(r, &body); err != nil {
		writeError(w, err)
		return
	}
	body.ProjectCode = r.PathValue("projectCode")
	body.BudgetItemID = r.PathValue("itemID")
	item, err := sqlite.NewCostStructureDetailRepository(s.store).SaveItemProperty(r.Context(), body)
	respond(w, item, err)
}
