package httpapi

import (
	"crypto/rand"
	"encoding/hex"
	"net/http"
	"strings"

	"github.com/liuxb99/pcces-web/pcces-go/internal/storage/sqlite"
)

func newConversionJobID() string { b:=make([]byte,16);_,_=rand.Read(b);return hex.EncodeToString(b) }
func (s *Server) conversionLongJobRoutes() {
	s.mux.HandleFunc("POST /api/conversions/jobs", s.createConversionLongJob)
	s.mux.HandleFunc("GET /api/conversions/jobs/{jobID}", s.getConversionLongJob)
	s.mux.HandleFunc("POST /api/conversions/jobs/{jobID}/advance", s.advanceConversionLongJob)
	s.mux.HandleFunc("POST /api/conversions/jobs/{jobID}/cancel", s.cancelConversionLongJob)
}
func (s *Server) createConversionLongJob(w http.ResponseWriter,r *http.Request){var body struct{JobType string `json:"job_type"`;Payload map[string]any `json:"payload"`;Actor string `json:"actor"`};if err:=decodeJSON(r,&body);err!=nil{writeError(w,err);return};if strings.TrimSpace(body.Actor)==""{body.Actor="api"};item,err:=sqlite.NewConversionLongJobRepository(s.store).Create(r.Context(),newConversionJobID(),body.JobType,body.Actor,body.Payload);respondStatus(w,http.StatusCreated,item,err)}
func (s *Server) getConversionLongJob(w http.ResponseWriter,r *http.Request){item,err:=sqlite.NewConversionLongJobRepository(s.store).Get(r.Context(),r.PathValue("jobID"));respond(w,item,err)}
func (s *Server) advanceConversionLongJob(w http.ResponseWriter,r *http.Request){var body struct{RowVersion int64 `json:"row_version"`;Progress int `json:"progress"`;Stage string `json:"stage"`;Status string `json:"status"`;Result map[string]any `json:"result"`;Error map[string]any `json:"error"`};if err:=decodeJSON(r,&body);err!=nil{writeError(w,err);return};item,err:=sqlite.NewConversionLongJobRepository(s.store).Advance(r.Context(),r.PathValue("jobID"),body.RowVersion,body.Progress,body.Stage,body.Status,body.Result,body.Error);respond(w,item,err)}
func (s *Server) cancelConversionLongJob(w http.ResponseWriter,r *http.Request){var body struct{RowVersion int64 `json:"row_version"`};if err:=decodeJSON(r,&body);err!=nil{writeError(w,err);return};item,err:=sqlite.NewConversionLongJobRepository(s.store).Cancel(r.Context(),r.PathValue("jobID"),body.RowVersion);respond(w,item,err)}
