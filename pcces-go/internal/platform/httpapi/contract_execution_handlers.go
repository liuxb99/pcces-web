package httpapi

import (
	"net/http"

	"github.com/google/uuid"
	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func(s *Server)contractExecutionRoutes(){
	s.mux.HandleFunc("POST /api/contracts/{contractID}/invoice-periods",s.createExecutionInvoice)
	s.mux.HandleFunc("GET /api/contracts/invoice-periods/{periodID}",s.getExecutionInvoice)
	s.mux.HandleFunc("POST /api/contracts/invoice-periods/{periodID}/transition",s.transitionExecutionInvoice)
	s.mux.HandleFunc("POST /api/contracts/{contractID}/settlements",s.createExecutionSettlement)
	s.mux.HandleFunc("GET /api/contracts/settlements/{settlementID}",s.getExecutionSettlement)
}
func(s *Server)createExecutionInvoice(w http.ResponseWriter,r *http.Request){var b struct{Actor,Deduction,Retention,Adjustment string;Items []sqlite.InvoiceLineInput `json:"items"`};if e:=decodeJSON(r,&b);e!=nil{writeError(w,e);return};if b.Actor==""{b.Actor="api"};item,e:=sqlite.NewExecutionRepository(s.store).CreateInvoice(r.Context(),sqlite.InvoiceCreateRequest{ID:uuid.NewString(),ContractID:r.PathValue("contractID"),Actor:b.Actor,Deduction:b.Deduction,Retention:b.Retention,Adjustment:b.Adjustment,Items:b.Items});respondStatus(w,http.StatusCreated,item,e)}
func(s *Server)getExecutionInvoice(w http.ResponseWriter,r *http.Request){item,e:=sqlite.NewExecutionRepository(s.store).GetInvoice(r.Context(),r.PathValue("periodID"));respond(w,item,e)}
func(s *Server)transitionExecutionInvoice(w http.ResponseWriter,r *http.Request){var b struct{Status,Actor string;RowVersion int64 `json:"row_version"`};if e:=decodeJSON(r,&b);e!=nil{writeError(w,e);return};if b.Actor==""{b.Actor="api"};item,e:=sqlite.NewExecutionRepository(s.store).TransitionInvoice(r.Context(),r.PathValue("periodID"),b.Status,b.RowVersion,b.Actor);respond(w,item,e)}
func(s *Server)createExecutionSettlement(w http.ResponseWriter,r *http.Request){var b struct{FinalAdjustment string `json:"final_adjustment"`;Actor string `json:"actor"`};if e:=decodeJSON(r,&b);e!=nil{writeError(w,e);return};if b.Actor==""{b.Actor="api"};item,e:=sqlite.NewExecutionRepository(s.store).CreateSettlement(r.Context(),uuid.NewString(),r.PathValue("contractID"),b.FinalAdjustment,b.Actor);respondStatus(w,http.StatusCreated,item,e)}
func(s *Server)getExecutionSettlement(w http.ResponseWriter,r *http.Request){item,e:=sqlite.NewExecutionRepository(s.store).GetSettlement(r.Context(),r.PathValue("settlementID"));respond(w,item,e)}
