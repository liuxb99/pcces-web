package sqlite

import (
	"encoding/csv"
	"encoding/json"
	"encoding/xml"
	"fmt"
	"io"
	"strings"
)

type LegacyExchangeItem struct {
	SourceBudgetItemID string `json:"source_budget_item_id"`
	ID                 string `json:"id"`
	Code               string `json:"code"`
	Name               string `json:"name"`
	Unit               string `json:"unit"`
	Quantity           string `json:"quantity"`
	UnitPrice          string `json:"unit_price"`
	Amount             string `json:"amount"`
}

type LegacyExchangeResult struct {
	Format            string               `json:"format"`
	FormatVersion     string               `json:"format_version"`
	SourceProjectCode string               `json:"source_project_code"`
	Items             []LegacyExchangeItem `json:"items"`
	Report            BidImportReport      `json:"report"`
}

type zmdDocument struct {
	ProjectCode string               `json:"project_code"`
	Version     string               `json:"version"`
	Items       []LegacyExchangeItem `json:"items"`
	Details     []LegacyExchangeItem `json:"details"`
}

type pxDocument struct {
	XMLName xml.Name
	Version string `xml:"version,attr"`
	Header struct {
		ProjectCode string `xml:"ProjectCode"`
	} `xml:"Header"`
	Items struct {
		Rows []struct {
			SourceID, Code, Name, Unit, Quantity, UnitPrice, Amount string
		} `xml:",any"`
	} `xml:",any"`
}

func normalizeLegacyItem(item LegacyExchangeItem, index int) LegacyExchangeItem {
	if strings.TrimSpace(item.SourceBudgetItemID) == "" {
		item.SourceBudgetItemID = strings.TrimSpace(item.ID)
	}
	if strings.TrimSpace(item.SourceBudgetItemID) == "" {
		item.SourceBudgetItemID = fmt.Sprintf("ROW-%d", index)
	}
	item.ID = item.SourceBudgetItemID
	item.Code = strings.ToUpper(strings.TrimSpace(item.Code))
	item.Name = strings.TrimSpace(item.Name)
	item.Unit = strings.TrimSpace(item.Unit)
	if strings.TrimSpace(item.Quantity) == "" { item.Quantity = "0" }
	if strings.TrimSpace(item.UnitPrice) == "" { item.UnitPrice = "0" }
	if strings.TrimSpace(item.Amount) == "" { item.Amount = "0" }
	return item
}

func ParseLegacyExchange(payload, format string) (LegacyExchangeResult, error) {
	format = strings.ToUpper(strings.TrimSpace(format))
	result := LegacyExchangeResult{Format: format}
	switch format {
	case "ZMD":
		var doc zmdDocument
		if err := json.Unmarshal([]byte(payload), &doc); err != nil { return result, err }
		result.SourceProjectCode, result.FormatVersion = strings.TrimSpace(doc.ProjectCode), defaultString(doc.Version, "1.0")
		result.Items = doc.Items
		if len(result.Items) == 0 { result.Items = doc.Details }
	case "MDB":
		r := csv.NewReader(strings.NewReader(payload))
		header, err := r.Read(); if err != nil { return result, fmt.Errorf("MDB interchange header: %w", err) }
		index := map[string]int{}; for i, name := range header { index[strings.ToLower(strings.TrimSpace(name))] = i }
		for rowIndex := 1; ; rowIndex++ {
			row, err := r.Read(); if err == io.EOF { break }; if err != nil { return result, err }
			get := func(name string) string { if i, ok := index[name]; ok && i < len(row) { return row[i] }; return "" }
			if rowIndex == 1 { result.SourceProjectCode = strings.TrimSpace(get("project_code")) }
			result.Items = append(result.Items, LegacyExchangeItem{ID:get("id"), Code:get("code"), Name:get("name"), Unit:get("unit"), Quantity:get("quantity"), UnitPrice:get("unit_price"), Amount:get("amount")})
		}
		result.FormatVersion = "CSV-1.0"
	case "PX":
		var doc pxDocument
		if err := xml.Unmarshal([]byte(payload), &doc); err != nil { return result, err }
		if doc.XMLName.Local != "PX" && doc.XMLName.Local != "PCCESExchange" { return result, fmt.Errorf("invalid PX root element") }
		result.SourceProjectCode, result.FormatVersion = strings.TrimSpace(doc.Header.ProjectCode), defaultString(doc.Version, "1.0")
		for _, row := range doc.Items.Rows { result.Items = append(result.Items, LegacyExchangeItem{ID:row.SourceID, Code:row.Code, Name:row.Name, Unit:row.Unit, Quantity:row.Quantity, UnitPrice:row.UnitPrice, Amount:row.Amount}) }
	default:
		return result, fmt.Errorf("format must be ZMD, MDB or PX")
	}
	for i := range result.Items { result.Items[i] = normalizeLegacyItem(result.Items[i], i+1) }
	converted := make([]BidImportItem, 0, len(result.Items))
	for _, item := range result.Items { converted = append(converted, BidImportItem{SourceBudgetItemID:item.SourceBudgetItemID, ID:item.ID, Code:item.Code, Name:item.Name, Unit:item.Unit, Quantity:item.Quantity, UnitPrice:item.UnitPrice, Amount:item.Amount}) }
	result.Report = BuildBidImportPreflight(converted)
	return result, nil
}
