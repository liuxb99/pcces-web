package money

import (
	"encoding/json"
	"os"
	"path/filepath"
	"runtime"
	"testing"
)

type sharedGoldenCase struct {
	ID       string          `json:"id"`
	Kind     string          `json:"kind"`
	Scale    int             `json:"scale"`
	Input    BudgetKindInput `json:"input"`
	Expected string          `json:"expected"`
}

type sharedGoldenFixture struct {
	EvidenceType  string             `json:"evidence_type"`
	LegacySources []string           `json:"legacy_sources"`
	Cases         []sharedGoldenCase `json:"cases"`
}

func TestSharedSourceDerivedFinancialGolden(t *testing.T) {
	_, current, _, ok := runtime.Caller(0)
	if !ok {
		t.Fatal("cannot resolve test source path")
	}
	fixturePath := filepath.Clean(filepath.Join(filepath.Dir(current), "..", "..", "..", "..", "tests", "golden", "core_financial.json"))
	content, err := os.ReadFile(fixturePath)
	if err != nil {
		t.Fatalf("read shared fixture: %v", err)
	}
	var fixture sharedGoldenFixture
	if err = json.Unmarshal(content, &fixture); err != nil {
		t.Fatalf("decode shared fixture: %v", err)
	}
	if fixture.EvidenceType != "SOURCE_DERIVED_GOLDEN" || len(fixture.LegacySources) == 0 {
		t.Fatalf("fixture lacks auditable legacy evidence: %#v", fixture)
	}
	for _, testCase := range fixture.Cases {
		t.Run(testCase.ID, func(t *testing.T) {
			trace, err := CalculateBudgetKind(testCase.Kind, testCase.Input, testCase.Scale)
			if err != nil {
				t.Fatalf("calculate: %v", err)
			}
			if trace.Result != testCase.Expected {
				t.Fatalf("result=%s expected=%s", trace.Result, testCase.Expected)
			}
			if len(trace.Steps) == 0 {
				t.Fatal("calculation trace is empty")
			}
		})
	}
}
