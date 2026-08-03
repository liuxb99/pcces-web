package sqlite

import "testing"

func TestAuditPhase4RoundTripConsistent(t *testing.T) {
	source := []RoundTripAuditItem{{ID: "A", Code: "A01", Name: "Concrete", Unit: "M3", Quantity: "2", UnitPrice: "10", Amount: "20"}}
	imported := []RoundTripAuditItem{{SourceBudgetItemID: "A", Code: "A01", Name: "Concrete", Unit: "M3", Quantity: "2", UnitPrice: "10", Amount: "20"}}
	result, err := AuditPhase4RoundTrip(source, imported)
	if err != nil {
		t.Fatal(err)
	}
	if !result.Consistent || result.TotalDifference != "0.00" {
		t.Fatalf("unexpected result: %#v", result)
	}
}

func TestAuditPhase4RoundTripDetectsDifference(t *testing.T) {
	source := []RoundTripAuditItem{{ID: "A", Code: "A01", Amount: "20"}}
	imported := []RoundTripAuditItem{{SourceBudgetItemID: "A", Code: "A01", Amount: "30"}, {SourceBudgetItemID: "B", Code: "B01", Amount: "5"}}
	result, err := AuditPhase4RoundTrip(source, imported)
	if err != nil {
		t.Fatal(err)
	}
	if result.Consistent || len(result.AddedLineageIDs) != 1 || result.TotalDifference != "10.00" {
		t.Fatalf("unexpected result: %#v", result)
	}
}
