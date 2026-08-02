package sqlite

import (
	"fmt"
	"sort"
	"strings"

	"github.com/shopspring/decimal"
)

type CostCalculationLine struct {
	Code        string `json:"code"`
	Kind        string `json:"kind"`
	BaseKind    string `json:"base_kind"`
	Rate        string `json:"rate"`
	FixedAmount string `json:"fixed_amount"`
	Sign        int    `json:"sign"`
	SortOrder   int    `json:"sort_order"`
}

type CostCalculationResultLine struct {
	Code         string `json:"code"`
	Kind         string `json:"kind"`
	BaseKind     string `json:"base_kind"`
	BaseAmount   string `json:"base_amount"`
	Rate         string `json:"rate"`
	Sign         int    `json:"sign"`
	Amount       string `json:"amount"`
	RunningTotal string `json:"running_total"`
	SortOrder    int    `json:"sort_order"`
}

type CostCalculationResult struct {
	DirectCost string                      `json:"direct_cost"`
	Total      string                      `json:"total"`
	Scale      int32                       `json:"scale"`
	Lines      []CostCalculationResultLine `json:"lines"`
	Trace      map[string]any              `json:"calculation_trace"`
}

func CalculateCostStructure(lines []CostCalculationLine, directCost string, scale int32) (CostCalculationResult, error) {
	if scale < 0 || scale > 8 {
		return CostCalculationResult{}, fmt.Errorf("scale must be between 0 and 8")
	}
	direct, err := decimal.NewFromString(directCost)
	if err != nil {
		return CostCalculationResult{}, fmt.Errorf("invalid direct_cost: %w", err)
	}
	sort.SliceStable(lines, func(i, j int) bool {
		if lines[i].SortOrder == lines[j].SortOrder {
			return strings.ToUpper(lines[i].Code) < strings.ToUpper(lines[j].Code)
		}
		return lines[i].SortOrder < lines[j].SortOrder
	})
	allowedKinds := map[string]bool{"DIRECT": true, "INDIRECT": true, "MANAGEMENT": true, "TAX": true, "PERCENT": true, "ADJUSTMENT": true}
	allowedBases := map[string]bool{"DIRECT": true, "SUBTOTAL": true, "PREVIOUS": true, "FIXED": true}
	seen := map[string]bool{}
	subtotal := direct.Round(scale)
	previous := decimal.Zero
	result := CostCalculationResult{DirectCost: direct.Round(scale).StringFixed(scale), Scale: scale, Lines: []CostCalculationResultLine{}}
	order := []string{}
	for _, raw := range lines {
		code := strings.ToUpper(strings.TrimSpace(raw.Code))
		kind := strings.ToUpper(strings.TrimSpace(raw.Kind))
		baseKind := strings.ToUpper(strings.TrimSpace(raw.BaseKind))
		if baseKind == "" {
			baseKind = "SUBTOTAL"
		}
		if code == "" || seen[code] {
			return CostCalculationResult{}, fmt.Errorf("line code is required and must be unique")
		}
		if !allowedKinds[kind] || !allowedBases[baseKind] {
			return CostCalculationResult{}, fmt.Errorf("unsupported kind or base_kind")
		}
		if raw.Sign != -1 && raw.Sign != 1 {
			return CostCalculationResult{}, fmt.Errorf("sign must be -1 or 1")
		}
		seen[code] = true
		rate, err := decimal.NewFromString(defaultString(raw.Rate, "0"))
		if err != nil {
			return CostCalculationResult{}, fmt.Errorf("invalid rate: %w", err)
		}
		fixed, err := decimal.NewFromString(defaultString(raw.FixedAmount, "0"))
		if err != nil {
			return CostCalculationResult{}, fmt.Errorf("invalid fixed_amount: %w", err)
		}
		base := subtotal
		switch baseKind {
		case "DIRECT":
			base = direct
		case "PREVIOUS":
			base = previous
		case "FIXED":
			base = fixed
		}
		amount := base.Mul(rate).Div(decimal.NewFromInt(100))
		if kind == "ADJUSTMENT" || baseKind == "FIXED" {
			amount = fixed
		}
		amount = amount.Mul(decimal.NewFromInt(int64(raw.Sign))).Round(scale)
		subtotal = subtotal.Add(amount).Round(scale)
		previous = amount
		result.Lines = append(result.Lines, CostCalculationResultLine{Code: code, Kind: kind, BaseKind: baseKind, BaseAmount: base.Round(scale).StringFixed(scale), Rate: rate.String(), Sign: raw.Sign, Amount: amount.StringFixed(scale), RunningTotal: subtotal.StringFixed(scale), SortOrder: raw.SortOrder})
		order = append(order, code)
	}
	result.Total = subtotal.StringFixed(scale)
	result.Trace = map[string]any{"policy": "P4-COST-005", "rounding": "ROUND_HALF_UP", "order": order}
	return result, nil
}

func defaultString(value, fallback string) string {
	if strings.TrimSpace(value) == "" {
		return fallback
	}
	return value
}
