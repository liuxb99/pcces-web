package sqlite

import "testing"

func TestCalculateCostStructureOrderingAndAdjustment(t *testing.T) {
	result, err := CalculateCostStructure([]CostCalculationLine{
		{Code: "MGT", Kind: "MANAGEMENT", BaseKind: "DIRECT", Rate: "5", Sign: 1, SortOrder: 10},
		{Code: "TAX", Kind: "TAX", BaseKind: "SUBTOTAL", Rate: "5", Sign: 1, SortOrder: 20},
		{Code: "DISC", Kind: "ADJUSTMENT", BaseKind: "FIXED", FixedAmount: "30", Sign: -1, SortOrder: 30},
	}, "1000", 2)
	if err != nil { t.Fatal(err) }
	if result.Total != "1072.50" { t.Fatalf("unexpected total %s", result.Total) }
	if result.Lines[1].BaseAmount != "1050.00" { t.Fatalf("unexpected tax base %s", result.Lines[1].BaseAmount) }
}

func TestCalculateCostStructureRejectsInvalidInput(t *testing.T) {
	_, err := CalculateCostStructure([]CostCalculationLine{
		{Code: "X", Kind: "TAX", Rate: "5", Sign: 1},
		{Code: "X", Kind: "TAX", Rate: "5", Sign: 1},
	}, "100", 2)
	if err == nil { t.Fatal("expected duplicate code error") }
	_, err = CalculateCostStructure([]CostCalculationLine{{Code: "X", Kind: "TAX", Rate: "5", Sign: 0}}, "100", 2)
	if err == nil { t.Fatal("expected invalid sign error") }
}
