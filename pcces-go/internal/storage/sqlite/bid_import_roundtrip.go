package sqlite

import (
	"encoding/json"
	"encoding/xml"
	"fmt"
	"strings"
)

type BidImportItem struct {
	SourceBudgetItemID string `json:"source_budget_item_id"`
	ID                 string `json:"id"`
	Code               string `json:"code"`
	Name               string `json:"name"`
	Unit               string `json:"unit"`
	Quantity           string `json:"quantity"`
	UnitPrice          string `json:"unit_price"`
	Amount             string `json:"amount"`
}

type BidImportIssue struct {
	Code     string `json:"code"`
	Index    int    `json:"index,omitempty"`
	ItemCode string `json:"item_code,omitempty"`
}

type BidImportReport struct {
	Errors       []BidImportIssue `json:"errors"`
	Warnings     []BidImportIssue `json:"warnings"`
	ErrorCount   int              `json:"error_count"`
	WarningCount int              `json:"warning_count"`
	CanContinue  bool             `json:"can_continue"`
}

type BidImportParseResult struct {
	Format               string          `json:"format"`
	FormatVersion        string          `json:"format_version"`
	SourceBidProjectCode string          `json:"source_bid_project_code"`
	Items                []BidImportItem `json:"items"`
	Report               BidImportReport `json:"report"`
}

type bidImportJSON struct {
	ProjectCode string          `json:"project_code"`
	Items       []BidImportItem `json:"items"`
}

type bidImportXML struct {
	XMLName xml.Name
	Version string `xml:"version,attr"`
	Header  struct {
		ProjectCode string `xml:"ProjectCode"`
	} `xml:"Header"`
	Items struct {
		Rows []struct {
			SourceID  string `xml:"SourceItemId"`
			Code      string `xml:"Code"`
			Name      string `xml:"Name"`
			Unit      string `xml:"Unit"`
			Quantity  string `xml:"Quantity"`
			UnitPrice string `xml:"UnitPrice"`
			Amount    string `xml:"Amount"`
		} `xml:",any"`
	} `xml:",any"`
}

func ParseBidImport(payload, hintedFormat string) (BidImportParseResult, error) {
	text := strings.TrimSpace(payload)
	hint := strings.ToUpper(strings.TrimSpace(hintedFormat))
	result := BidImportParseResult{}
	if hint == "BID_JSON" || strings.HasPrefix(text, "{") {
		var doc bidImportJSON
		if err := json.Unmarshal([]byte(text), &doc); err != nil {
			return result, fmt.Errorf("invalid bid JSON: %w", err)
		}
		result.Format = "BID_JSON"
		result.FormatVersion = "2.0"
		result.SourceBidProjectCode = strings.TrimSpace(doc.ProjectCode)
		result.Items = doc.Items
	} else {
		var doc bidImportXML
		if err := xml.Unmarshal([]byte(text), &doc); err != nil {
			return result, fmt.Errorf("invalid bid XML: %w", err)
		}
		switch doc.XMLName.Local {
		case "PCCESBidExchange":
			result.Format, result.FormatVersion = "XML_NEW", defaultString(doc.Version, "2.0")
		case "PCCES":
			result.Format, result.FormatVersion = "XML_LEGACY", defaultString(doc.Version, "1.0")
		default:
			return result, fmt.Errorf("unsupported electronic bid format")
		}
		result.SourceBidProjectCode = strings.TrimSpace(doc.Header.ProjectCode)
		for i, row := range doc.Items.Rows {
			id := strings.TrimSpace(row.SourceID)
			if id == "" {
				id = fmt.Sprintf("ROW-%d", i+1)
			}
			result.Items = append(result.Items, BidImportItem{
				SourceBudgetItemID: strings.TrimSpace(row.SourceID), ID: id,
				Code: strings.ToUpper(strings.TrimSpace(row.Code)), Name: strings.TrimSpace(row.Name),
				Unit: strings.TrimSpace(row.Unit), Quantity: defaultString(row.Quantity, "0"),
				UnitPrice: defaultString(row.UnitPrice, "0"), Amount: defaultString(row.Amount, "0"),
			})
		}
	}
	for i := range result.Items {
		result.Items[i].Code = strings.ToUpper(strings.TrimSpace(result.Items[i].Code))
	}
	result.Report = BuildBidImportPreflight(result.Items)
	return result, nil
}

func BuildBidImportPreflight(items []BidImportItem) BidImportReport {
	report := BidImportReport{Errors: []BidImportIssue{}, Warnings: []BidImportIssue{}}
	if len(items) == 0 {
		report.Errors = append(report.Errors, BidImportIssue{Code: "EMPTY_BID"})
	}
	seen := map[string]bool{}
	for i, item := range items {
		code := strings.ToUpper(strings.TrimSpace(item.Code))
		if code == "" {
			report.Errors = append(report.Errors, BidImportIssue{Code: "MISSING_ITEM_CODE", Index: i})
		} else if seen[code] {
			report.Errors = append(report.Errors, BidImportIssue{Code: "DUPLICATE_ITEM_CODE", ItemCode: code})
		}
		seen[code] = true
		if strings.TrimSpace(item.Name) == "" {
			report.Warnings = append(report.Warnings, BidImportIssue{Code: "MISSING_ITEM_NAME", Index: i})
		}
		if strings.TrimSpace(item.SourceBudgetItemID) == "" && strings.TrimSpace(item.ID) == "" {
			report.Warnings = append(report.Warnings, BidImportIssue{Code: "MISSING_ROUNDTRIP_LINEAGE", Index: i})
		}
	}
	report.ErrorCount = len(report.Errors)
	report.WarningCount = len(report.Warnings)
	report.CanContinue = report.ErrorCount == 0
	return report
}
