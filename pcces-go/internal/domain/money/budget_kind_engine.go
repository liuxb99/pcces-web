package money

import (
	"fmt"
	"math/big"
	"strings"
)

type BudgetTerm struct { Sign int `json:"sign"`; Amount string `json:"amount"` }
type BudgetTier struct { UpTo *string `json:"up_to,omitempty"`; Rate string `json:"rate"` }
type BudgetKindInput struct {
	Quantity string `json:"quantity,omitempty"`; UnitPrice string `json:"unit_price,omitempty"`
	Children []string `json:"children,omitempty"`; Base string `json:"base,omitempty"`; Rate string `json:"rate,omitempty"`
	Tiers []BudgetTier `json:"tiers,omitempty"`; Terms []BudgetTerm `json:"terms,omitempty"`
}
type BudgetTraceStep struct { Operation string `json:"operation"`; Inputs map[string]any `json:"inputs"`; Result string `json:"result"` }
type BudgetCalculationTrace struct { Kind string `json:"kind"`; Scale int `json:"scale"`; Steps []BudgetTraceStep `json:"steps"`; Result string `json:"result"` }

func rat(value string) (*big.Rat,error){ r,ok:=new(big.Rat).SetString(value); if !ok{return nil,fmt.Errorf("invalid decimal %q",value)}; return r,nil }
func minRat(a,b *big.Rat)*big.Rat{if a.Cmp(b)<=0{return new(big.Rat).Set(a)};return new(big.Rat).Set(b)}

func CalculateBudgetKind(kind string, in BudgetKindInput, scale int)(BudgetCalculationTrace,error){
	k:=strings.ToUpper(strings.TrimSpace(kind)); trace:=BudgetCalculationTrace{Kind:k,Scale:scale,Steps:[]BudgetTraceStep{}}
	var result string; var err error
	switch k {
	case "L": result,err=Multiply(in.Quantity,in.UnitPrice,scale); trace.Steps=append(trace.Steps,BudgetTraceStep{"MULTIPLY",map[string]any{"quantity":in.Quantity,"unit_price":in.UnitPrice},result})
	case "B","Z": result,err=Sum(in.Children,scale); trace.Steps=append(trace.Steps,BudgetTraceStep{"SUM_CHILDREN",map[string]any{"children":in.Children},result})
	case "F": result,err=Multiply(in.Base,in.Rate,scale); trace.Steps=append(trace.Steps,BudgetTraceStep{"MULTIPLY_BASE_RATE",map[string]any{"base":in.Base,"rate":in.Rate},result})
	case "U": vals:=make([]string,0,len(in.Terms)); for _,t:=range in.Terms{if t.Sign!=1&&t.Sign!=-1{return trace,fmt.Errorf("term sign must be -1 or 1")}; if t.Sign<0{vals=append(vals,"-"+strings.TrimPrefix(t.Amount,"+"))}else{vals=append(vals,t.Amount)}}; result,err=Sum(vals,scale); trace.Steps=append(trace.Steps,BudgetTraceStep{"SIGNED_SUM",map[string]any{"terms":in.Terms},result})
	case "S":
		remaining,e:=rat(in.Base); if e!=nil{return trace,e}; if remaining.Sign()<0{return trace,fmt.Errorf("tiered base cannot be negative")}; previous:=new(big.Rat); subtotal:=new(big.Rat)
		if len(in.Tiers)==0{return trace,fmt.Errorf("tiers are required for S items")}
		for _,tier:=range in.Tiers{rate,e:=rat(tier.Rate);if e!=nil{return trace,e};qty:=new(big.Rat).Set(remaining);var up any
			if tier.UpTo!=nil{limit,e:=rat(*tier.UpTo);if e!=nil{return trace,e};if limit.Cmp(previous)<0{return trace,fmt.Errorf("tiers must be ordered")};capacity:=new(big.Rat).Sub(limit,previous);qty=minRat(remaining,capacity);previous.Set(limit);up=*tier.UpTo}
			amount:=new(big.Rat).Mul(qty,rate);subtotal.Add(subtotal,amount);stepResult,_:=Quantize(amount.FloatString(12),scale);trace.Steps=append(trace.Steps,BudgetTraceStep{"TIER",map[string]any{"quantity":qty.FloatString(12),"rate":tier.Rate,"up_to":up},stepResult});remaining.Sub(remaining,qty);if remaining.Sign()<=0{break}}
		if remaining.Sign()>0{return trace,fmt.Errorf("tier schedule does not cover base")};result,err=Quantize(subtotal.FloatString(12),scale)
	default:return trace,fmt.Errorf("unsupported budget item kind: %s",kind)
	}
	if err!=nil{return trace,err};trace.Result=result;return trace,nil
}
