package money

import (
	"encoding/json"
	"os"
	"path/filepath"
	"runtime"
	"testing"
)

type budgetFixture struct {
	Cases []struct {
		Name        string   `json:"name"`
		Kind        string   `json:"kind"`
		Quantity    string   `json:"quantity"`
		UnitPrice   string   `json:"unit_price"`
		Children    []string `json:"children"`
		AmountScale int      `json:"amount_scale"`
		Expected    string   `json:"expected_amount"`
	} `json:"cases"`
}

func TestBudgetGoldenCalculations(t *testing.T) {
	_, filename, _, _ := runtime.Caller(0)
	path := filepath.Clean(filepath.Join(filepath.Dir(filename), "../../../../specs/golden/budget-decimal-calculations.json"))
	body, err := os.ReadFile(path)
	if err != nil {
		t.Fatal(err)
	}
	var fixture budgetFixture
	if err := json.Unmarshal(body, &fixture); err != nil {
		t.Fatal(err)
	}
	for _, tc := range fixture.Cases {
		if tc.Kind == "RESOURCE" {
			continue
		}
		t.Run(tc.Name, func(t *testing.T) {
			var got string
			var err error
			if tc.Kind == "B" {
				got, err = CalculateBudgetRollup(tc.Children, tc.AmountScale)
			} else {
				got, err = CalculateBudgetLeaf(tc.Quantity, tc.UnitPrice, tc.AmountScale)
			}
			if err != nil {
				t.Fatal(err)
			}
			if got != tc.Expected {
				t.Fatalf("got %s want %s", got, tc.Expected)
			}
		})
	}
}
