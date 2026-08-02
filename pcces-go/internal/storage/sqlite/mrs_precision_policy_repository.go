package sqlite

import (
	"context"
	"database/sql"
	"strings"
	"time"

	"github.com/liuxb99/pcces-web/pcces-go/internal/domain/money"
	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

type MRSPrecisionPolicy struct {
	ProjectCode string `json:"project_code"`
	MainQuantityScale int `json:"main_quantity_scale"`
	MainPriceScale int `json:"main_price_scale"`
	MainAmountScale int `json:"main_amount_scale"`
	AnalysisQuantityScale int `json:"analysis_quantity_scale"`
	AnalysisPriceScale int `json:"analysis_price_scale"`
	AnalysisAmountScale int `json:"analysis_amount_scale"`
	RowVersion int64 `json:"row_version"`
	UpdatedBy string `json:"updated_by,omitempty"`
	UpdatedAt string `json:"updated_at,omitempty"`
	Source string `json:"source"`
}

type MRSPrecisionCalculation struct {
	ProjectCode string `json:"project_code"`
	Level string `json:"level"`
	Quantity string `json:"quantity"`
	UnitPrice string `json:"unit_price"`
	Amount string `json:"amount"`
	QuantityScale int `json:"quantity_scale"`
	PriceScale int `json:"price_scale"`
	AmountScale int `json:"amount_scale"`
	PolicyRowVersion int64 `json:"policy_row_version"`
	Trace map[string]string `json:"trace"`
}

type MRSPrecisionPolicyRepository struct{ store *Store }
func NewMRSPrecisionPolicyRepository(store *Store)*MRSPrecisionPolicyRepository{return &MRSPrecisionPolicyRepository{store:store}}

func defaultMRSPrecisionPolicy(projectCode string) MRSPrecisionPolicy {
	return MRSPrecisionPolicy{ProjectCode:projectCode,MainQuantityScale:2,MainPriceScale:2,MainAmountScale:0,AnalysisQuantityScale:4,AnalysisPriceScale:4,AnalysisAmountScale:2,Source:"LEGACY_DEFAULT"}
}

func validateMRSPrecisionPolicy(p MRSPrecisionPolicy) error {
	values:=[]int{p.MainQuantityScale,p.MainPriceScale,p.MainAmountScale,p.AnalysisQuantityScale,p.AnalysisPriceScale,p.AnalysisAmountScale}
	for _,v:=range values{if v<0||v>8{return errx.New(errx.CodeInvalidArgument,"precision scales must be between 0 and 8","P3-PRECISION")}}
	if p.MainQuantityScale==p.AnalysisQuantityScale&&p.MainPriceScale==p.AnalysisPriceScale&&p.MainAmountScale==p.AnalysisAmountScale{return errx.New(errx.CodeInvalidArgument,"main and analysis precision policies must remain independently defined","P3-PRECISION")}
	return nil
}

func (r *MRSPrecisionPolicyRepository) Get(ctx context.Context,projectCode string)(MRSPrecisionPolicy,error){
	p:=defaultMRSPrecisionPolicy(projectCode)
	err:=r.store.db.QueryRowContext(ctx,`SELECT project_code,main_quantity_scale,main_price_scale,main_amount_scale,analysis_quantity_scale,analysis_price_scale,analysis_amount_scale,row_version,updated_by,updated_at FROM mrs_precision_policies WHERE project_code=?`,projectCode).Scan(&p.ProjectCode,&p.MainQuantityScale,&p.MainPriceScale,&p.MainAmountScale,&p.AnalysisQuantityScale,&p.AnalysisPriceScale,&p.AnalysisAmountScale,&p.RowVersion,&p.UpdatedBy,&p.UpdatedAt)
	if err==sql.ErrNoRows{return p,nil};if err!=nil{return MRSPrecisionPolicy{},err};p.Source="PROJECT_OVERRIDE";return p,nil
}

func (r *MRSPrecisionPolicyRepository) Save(ctx context.Context,p MRSPrecisionPolicy,actor string)(MRSPrecisionPolicy,error){
	p.ProjectCode=strings.TrimSpace(p.ProjectCode);if p.ProjectCode==""{return p,errx.New(errx.CodeInvalidArgument,"project_code is required","P3-PRECISION")};if err:=validateMRSPrecisionPolicy(p);err!=nil{return p,err}
	now:=time.Now().UTC().Format(time.RFC3339Nano)
	if p.RowVersion==0{_,err:=r.store.db.ExecContext(ctx,`INSERT INTO mrs_precision_policies(project_code,main_quantity_scale,main_price_scale,main_amount_scale,analysis_quantity_scale,analysis_price_scale,analysis_amount_scale,row_version,updated_by,updated_at) VALUES(?,?,?,?,?,?,?,1,?,?)`,p.ProjectCode,p.MainQuantityScale,p.MainPriceScale,p.MainAmountScale,p.AnalysisQuantityScale,p.AnalysisPriceScale,p.AnalysisAmountScale,actor,now);if err!=nil{return p,errx.Wrap(errx.CodeConflict,"create precision policy","P3-PRECISION",err)}}else{res,err:=r.store.db.ExecContext(ctx,`UPDATE mrs_precision_policies SET main_quantity_scale=?,main_price_scale=?,main_amount_scale=?,analysis_quantity_scale=?,analysis_price_scale=?,analysis_amount_scale=?,row_version=row_version+1,updated_by=?,updated_at=? WHERE project_code=? AND row_version=?`,p.MainQuantityScale,p.MainPriceScale,p.MainAmountScale,p.AnalysisQuantityScale,p.AnalysisPriceScale,p.AnalysisAmountScale,actor,now,p.ProjectCode,p.RowVersion);if err!=nil{return p,err};n,_:=res.RowsAffected();if n!=1{return p,errx.New(errx.CodeConflict,"precision policy row_version conflict","P3-PRECISION")}}
	return r.Get(ctx,p.ProjectCode)
}

func (r *MRSPrecisionPolicyRepository) Calculate(ctx context.Context,projectCode,level,quantity,unitPrice string)(MRSPrecisionCalculation,error){
	p,err:=r.Get(ctx,projectCode);if err!=nil{return MRSPrecisionCalculation{},err};level=strings.ToUpper(strings.TrimSpace(level));var qs,ps,as int
	switch level{case "MAIN":qs,ps,as=p.MainQuantityScale,p.MainPriceScale,p.MainAmountScale;case "ANALYSIS":qs,ps,as=p.AnalysisQuantityScale,p.AnalysisPriceScale,p.AnalysisAmountScale;default:return MRSPrecisionCalculation{},errx.New(errx.CodeInvalidArgument,"level must be MAIN or ANALYSIS","P3-PRECISION")}
	q,err:=money.Quantize(quantity,qs);if err!=nil{return MRSPrecisionCalculation{},err};price,err:=money.Quantize(unitPrice,ps);if err!=nil{return MRSPrecisionCalculation{},err};amount,err:=money.CalculateBudgetLeaf(q,price,as);if err!=nil{return MRSPrecisionCalculation{},err}
	return MRSPrecisionCalculation{ProjectCode:projectCode,Level:level,Quantity:q,UnitPrice:price,Amount:amount,QuantityScale:qs,PriceScale:ps,AmountScale:as,PolicyRowVersion:p.RowVersion,Trace:map[string]string{"operation":"MRS_SPLIT_PRECISION_MULTIPLY","input_quantity":quantity,"input_unit_price":unitPrice,"result":amount}},nil
}
