package sqlite

import (
	"fmt"
	"math/big"
	"sort"
	"strings"
)

type RoundTripAuditItem struct {
	SourceBudgetItemID string `json:"source_budget_item_id"`
	ID                 string `json:"id"`
	Code               string `json:"code"`
	Name               string `json:"name"`
	Unit               string `json:"unit"`
	Quantity           string `json:"quantity"`
	UnitPrice          string `json:"unit_price"`
	Amount             string `json:"amount"`
}

type RoundTripItemDifference struct {
	SourceBudgetItemID string                       `json:"source_budget_item_id"`
	Fields             map[string]map[string]string `json:"fields"`
}

type RoundTripAuditResult struct {
	Consistent        bool                      `json:"consistent"`
	SourceTotal       string                    `json:"source_total"`
	ImportedTotal     string                    `json:"imported_total"`
	TotalDifference   string                    `json:"total_difference"`
	MissingLineageIDs []string                  `json:"missing_lineage_ids"`
	AddedLineageIDs   []string                  `json:"added_lineage_ids"`
	ItemDifferences   []RoundTripItemDifference `json:"item_differences"`
}

func auditRat(value string) (*big.Rat, error) {
	if strings.TrimSpace(value) == "" {
		value = "0"
	}
	v, ok := new(big.Rat).SetString(value)
	if !ok {
		return nil, fmt.Errorf("invalid decimal value: %s", value)
	}
	return v, nil
}

func ratText(v *big.Rat) string { return v.FloatString(2) }

func AuditPhase4RoundTrip(sourceItems, importedItems []RoundTripAuditItem) (RoundTripAuditResult, error) {
	result := RoundTripAuditResult{MissingLineageIDs: []string{}, AddedLineageIDs: []string{}, ItemDifferences: []RoundTripItemDifference{}}
	build := func(items []RoundTripAuditItem) (map[string]RoundTripAuditItem, error) {
		out := map[string]RoundTripAuditItem{}
		for _, item := range items {
			id := strings.TrimSpace(item.SourceBudgetItemID)
			if id == "" {
				id = strings.TrimSpace(item.ID)
			}
			if id == "" {
				return nil, fmt.Errorf("every item must preserve a round-trip lineage id")
			}
			if _, exists := out[id]; exists {
				return nil, fmt.Errorf("duplicate round-trip lineage id")
			}
			out[id] = item
		}
		return out, nil
	}
	source, err := build(sourceItems)
	if err != nil {
		return result, err
	}
	imported, err := build(importedItems)
	if err != nil {
		return result, err
	}
	for id := range source {
		if _, ok := imported[id]; !ok {
			result.MissingLineageIDs = append(result.MissingLineageIDs, id)
		}
	}
	for id := range imported {
		if _, ok := source[id]; !ok {
			result.AddedLineageIDs = append(result.AddedLineageIDs, id)
		}
	}
	sort.Strings(result.MissingLineageIDs)
	sort.Strings(result.AddedLineageIDs)
	sourceTotal, importedTotal := new(big.Rat), new(big.Rat)
	for id, left := range source {
		right, ok := imported[id]
		if !ok {
			continue
		}
		la, err := auditRat(left.Amount)
		if err != nil {
			return result, err
		}
		ra, err := auditRat(right.Amount)
		if err != nil {
			return result, err
		}
		sourceTotal.Add(sourceTotal, la)
		importedTotal.Add(importedTotal, ra)
		fields := map[string]map[string]string{}
		for name, pair := range map[string][2]string{"code": {left.Code, right.Code}, "name": {left.Name, right.Name}, "unit": {left.Unit, right.Unit}, "quantity": {left.Quantity, right.Quantity}, "unit_price": {left.UnitPrice, right.UnitPrice}, "amount": {left.Amount, right.Amount}} {
			if strings.TrimSpace(pair[0]) != strings.TrimSpace(pair[1]) {
				fields[name] = map[string]string{"source": pair[0], "imported": pair[1]}
			}
		}
		if len(fields) > 0 {
			result.ItemDifferences = append(result.ItemDifferences, RoundTripItemDifference{SourceBudgetItemID: id, Fields: fields})
		}
	}
	result.SourceTotal = ratText(sourceTotal)
	result.ImportedTotal = ratText(importedTotal)
	result.TotalDifference = ratText(new(big.Rat).Sub(importedTotal, sourceTotal))
	result.Consistent = len(result.MissingLineageIDs) == 0 && len(result.AddedLineageIDs) == 0 && len(result.ItemDifferences) == 0 && sourceTotal.Cmp(importedTotal) == 0
	return result, nil
}
