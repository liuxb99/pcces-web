package money

import (
	"encoding/json"
	"os"
	"path/filepath"
	"runtime"
	"testing"
)

type budgetKindFixture struct { Cases []struct {
	Name string `json:"name"`; Kind string `json:"kind"`; Scale int `json:"scale"`; Quantity string `json:"quantity"`; UnitPrice string `json:"unit_price"`; Children []string `json:"children"`; Base string `json:"base"`; Rate string `json:"rate"`; Tiers []BudgetTier `json:"tiers"`; Terms []BudgetTerm `json:"terms"`; Expected string `json:"expected"`; Steps []string `json:"steps"`
} `json:"cases"` }

func TestBudgetKindGoldenCases(t *testing.T){
	_,filename,_,_:=runtime.Caller(0);path:=filepath.Clean(filepath.Join(filepath.Dir(filename),"../../../../specs/golden/budget-item-kind-calculations.json"));body,err:=os.ReadFile(path);if err!=nil{t.Fatal(err)}
	var fixture budgetKindFixture;if err=json.Unmarshal(body,&fixture);err!=nil{t.Fatal(err)}
	for _,tc:=range fixture.Cases{t.Run(tc.Name,func(t *testing.T){trace,err:=CalculateBudgetKind(tc.Kind,BudgetKindInput{Quantity:tc.Quantity,UnitPrice:tc.UnitPrice,Children:tc.Children,Base:tc.Base,Rate:tc.Rate,Tiers:tc.Tiers,Terms:tc.Terms},tc.Scale);if err!=nil{t.Fatal(err)};if trace.Result!=tc.Expected{t.Fatalf("got %s want %s",trace.Result,tc.Expected)};if len(trace.Steps)!=len(tc.Steps){t.Fatalf("steps got %d want %d",len(trace.Steps),len(tc.Steps))};for i,want:=range tc.Steps{if trace.Steps[i].Operation!=want{t.Fatalf("step %d got %s want %s",i,trace.Steps[i].Operation,want)}}})}
}
