package sqlite

import "testing"

func combineFixtures() []CombineBidSource {
	return []CombineBidSource{
		{ProjectCode: "B1", Items: []CombineBidItem{{ID: "A", Code: "x", Name: "Concrete", Unit: "M3", Quantity: "2", UnitPrice: "10", Amount: "20"}}},
		{ProjectCode: "B2", Items: []CombineBidItem{{ID: "B", Code: "X", Name: "Concrete", Unit: "M3", Quantity: "3", UnitPrice: "10", Amount: "30"}}},
	}
}

func TestCombineBidBlocksConflict(t *testing.T) {
	result, err := CombineBidSources(combineFixtures(), "BLOCK")
	if err != nil {
		t.Fatal(err)
	}
	if result.Status != "BLOCKED" || len(result.BlockingConflicts) != 1 || result.Items[0].SourceProjectCode != "B1" {
		t.Fatalf("unexpected result %#v", result)
	}
}

func TestCombineBidSumsCompatibleItems(t *testing.T) {
	result, err := CombineBidSources(combineFixtures(), "SUM_QUANTITY")
	if err != nil {
		t.Fatal(err)
	}
	if result.Status != "READY" || result.Items[0].Quantity != "5" || result.Items[0].Amount != "50" {
		t.Fatalf("unexpected result %#v", result)
	}
	bad := combineFixtures()
	bad[1].Items[0].UnitPrice = "11"
	result, err = CombineBidSources(bad, "SUM_QUANTITY")
	if err != nil {
		t.Fatal(err)
	}
	if result.Status != "BLOCKED" || result.Conflicts[0].Resolution != "BLOCKED_INCOMPATIBLE_SUM" {
		t.Fatalf("unexpected incompatible result %#v", result)
	}
}

func TestCombineBidRenamePreservesBoth(t *testing.T) {
	result, err := CombineBidSources(combineFixtures(), "RENAME")
	if err != nil {
		t.Fatal(err)
	}
	if len(result.Items) != 2 || result.Items[1].Code != "X-2" {
		t.Fatalf("unexpected result %#v", result)
	}
}
