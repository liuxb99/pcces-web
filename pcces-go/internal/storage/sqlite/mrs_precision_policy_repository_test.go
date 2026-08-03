package sqlite

import (
	"context"
	"path/filepath"
	"testing"
)

func TestMRSPrecisionPolicySeparatesMainAndAnalysis(t *testing.T) {
	ctx := context.Background()
	store, err := Open(ctx, filepath.Join(t.TempDir(), "precision.db"))
	if err != nil {
		t.Fatal(err)
	}
	defer store.Close()
	repo := NewMRSPrecisionPolicyRepository(store)
	main, err := repo.Calculate(ctx, "P1", "MAIN", "1.23456", "12.34567")
	if err != nil {
		t.Fatal(err)
	}
	analysis, err := repo.Calculate(ctx, "P1", "ANALYSIS", "1.23456", "12.34567")
	if err != nil {
		t.Fatal(err)
	}
	if main.Quantity != "1.23" || main.UnitPrice != "12.35" || main.Amount != "15" {
		t.Fatalf("unexpected main %#v", main)
	}
	if analysis.Quantity != "1.2346" || analysis.UnitPrice != "12.3457" || analysis.Amount != "15.24" {
		t.Fatalf("unexpected analysis %#v", analysis)
	}
}

func TestMRSPrecisionPolicyOverrideAndConflict(t *testing.T) {
	ctx := context.Background()
	store, err := Open(ctx, filepath.Join(t.TempDir(), "precision.db"))
	if err != nil {
		t.Fatal(err)
	}
	defer store.Close()
	repo := NewMRSPrecisionPolicyRepository(store)
	p := MRSPrecisionPolicy{ProjectCode: "P1", MainQuantityScale: 3, MainPriceScale: 2, MainAmountScale: 1, AnalysisQuantityScale: 5, AnalysisPriceScale: 4, AnalysisAmountScale: 3}
	saved, err := repo.Save(ctx, p, "u1")
	if err != nil {
		t.Fatal(err)
	}
	if saved.RowVersion != 1 {
		t.Fatalf("row version %d", saved.RowVersion)
	}
	result, err := repo.Calculate(ctx, "P1", "ANALYSIS", "2.123456", "3.45678")
	if err != nil {
		t.Fatal(err)
	}
	if result.Quantity != "2.12346" || result.UnitPrice != "3.4568" || result.Amount != "7.340" {
		t.Fatalf("unexpected result %#v", result)
	}
	p.RowVersion = 0
	if _, err = repo.Save(ctx, p, "u2"); err == nil {
		t.Fatal("expected conflict")
	}
}

func TestMRSPrecisionPolicyRejectsCollapsedPolicy(t *testing.T) {
	ctx := context.Background()
	store, err := Open(ctx, filepath.Join(t.TempDir(), "precision.db"))
	if err != nil {
		t.Fatal(err)
	}
	defer store.Close()
	_, err = NewMRSPrecisionPolicyRepository(store).Save(ctx, MRSPrecisionPolicy{ProjectCode: "P1", MainQuantityScale: 2, MainPriceScale: 2, MainAmountScale: 2, AnalysisQuantityScale: 2, AnalysisPriceScale: 2, AnalysisAmountScale: 2}, "u1")
	if err == nil {
		t.Fatal("expected independent-policy validation error")
	}
}
