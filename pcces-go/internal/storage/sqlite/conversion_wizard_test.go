package sqlite

import (
	"context"
	"path/filepath"
	"testing"
)

func strptr(value string) *string { return &value }

func TestConversionPreflightBlocksDuplicateIDs(t *testing.T) {
	report := BuildConversionPreflight([]ConversionWizardItem{
		{ID: "1", Code: "A", Name: "工程", Quantity: strptr("1"), UnitPrice: strptr("2")},
		{ID: "1", Code: "A", Name: ""},
	}, "CREATE", map[string]any{"format": "BID_JSON"})
	if report.CanContinue || report.ErrorCount == 0 || report.WarningCount < 3 {
		t.Fatalf("unexpected report %#v", report)
	}
}

func TestConversionWizardSessionPersistsReadyAndBlockedReports(t *testing.T) {
	ctx := context.Background()
	store, err := Open(ctx, filepath.Join(t.TempDir(), "wizard.db"))
	if err != nil {
		t.Fatal(err)
	}
	defer store.Close()
	repo := NewConversionWizardRepository(store)
	ready, err := repo.Create(ctx, ConversionWizardRequest{
		SourceProjectCode: "BUD-1", SourceBudgetVersionID: "V1", TargetProjectCode: "BID-1",
		Mode: "CREATE", ActorID: "u1", Options: map[string]any{"format": "XML_NEW"},
		BudgetItems: []ConversionWizardItem{{ID: "1", Code: "A", Name: "工程", Quantity: strptr("1"), UnitPrice: strptr("2")}},
	})
	if err != nil {
		t.Fatal(err)
	}
	if ready.Status != "READY" || !ready.CanContinue || ready.Options["format"] != "XML_NEW" {
		t.Fatalf("unexpected ready session %#v", ready)
	}
	blocked, err := repo.Create(ctx, ConversionWizardRequest{
		SourceProjectCode: "BUD-1", SourceBudgetVersionID: "V1", TargetProjectCode: "BID-2",
		Mode: "APPEND", ActorID: "u1", Options: map[string]any{"format": "BAD"},
		BudgetItems: []ConversionWizardItem{{ID: "1", Code: "A", Name: "工程", Quantity: strptr("1"), UnitPrice: strptr("2")}},
	})
	if err != nil {
		t.Fatal(err)
	}
	if blocked.Status != "BLOCKED" || blocked.CanContinue {
		t.Fatalf("unexpected blocked session %#v", blocked)
	}
}
