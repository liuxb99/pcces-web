package money

import (
	"encoding/json"
	"os"
	"path/filepath"
	"runtime"
	"testing"
)

type decimalFixture struct {
	Cases []decimalCase `json:"cases"`
}

type decimalCase struct {
	Name      string   `json:"name"`
	Operation string   `json:"operation"`
	Value     string   `json:"value"`
	Left      string   `json:"left"`
	Right     string   `json:"right"`
	Values    []string `json:"values"`
	Scale     int      `json:"scale"`
	Expected  string   `json:"expected"`
}

func TestDecimalGoldenCalculations(t *testing.T) {
	_, filename, _, ok := runtime.Caller(0)
	if !ok {
		t.Fatal("resolve test path")
	}
	path := filepath.Clean(filepath.Join(filepath.Dir(filename), "../../../../specs/golden/decimal-calculations.json"))
	body, err := os.ReadFile(path)
	if err != nil {
		t.Fatalf("read fixture: %v", err)
	}
	var fixture decimalFixture
	if err := json.Unmarshal(body, &fixture); err != nil {
		t.Fatalf("decode fixture: %v", err)
	}
	for _, testCase := range fixture.Cases {
		t.Run(testCase.Name, func(t *testing.T) {
			var actual string
			var err error
			switch testCase.Operation {
			case "quantize":
				actual, err = Quantize(testCase.Value, testCase.Scale)
			case "multiply":
				actual, err = Multiply(testCase.Left, testCase.Right, testCase.Scale)
			case "sum":
				actual, err = Sum(testCase.Values, testCase.Scale)
			default:
				t.Fatalf("unsupported operation %q", testCase.Operation)
			}
			if err != nil {
				t.Fatalf("calculate: %v", err)
			}
			if actual != testCase.Expected {
				t.Fatalf("got %q want %q", actual, testCase.Expected)
			}
		})
	}
}

func TestInvalidScale(t *testing.T) {
	if _, err := Quantize("1.23", MaxScale+1); err == nil {
		t.Fatal("expected invalid scale error")
	}
}
