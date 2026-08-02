package sqlite

import "testing"

func TestParseBidImportNewXML(t *testing.T) {
	payload := `<PCCESBidExchange version="2.0"><Header><ProjectCode>BID-1</ProjectCode></Header><Items><Item><SourceItemId>A1</SourceItemId><Code>a-01</Code><Name>Concrete</Name><Unit>M3</Unit><Quantity>2</Quantity><UnitPrice>10</UnitPrice><Amount>20</Amount></Item></Items></PCCESBidExchange>`
	result, err := ParseBidImport(payload, "")
	if err != nil {
		t.Fatal(err)
	}
	if result.Format != "XML_NEW" || result.FormatVersion != "2.0" || result.SourceBidProjectCode != "BID-1" {
		t.Fatalf("unexpected detection: %#v", result)
	}
	if len(result.Items) != 1 || result.Items[0].Code != "A-01" || !result.Report.CanContinue {
		t.Fatalf("unexpected parse result: %#v", result)
	}
}

func TestParseBidImportLegacyXML(t *testing.T) {
	payload := `<PCCES version="1.0"><Header><ProjectCode>L1</ProjectCode></Header><Detail><Record><SourceItemId>X</SourceItemId><Code>x</Code><Name>N</Name></Record></Detail></PCCES>`
	result, err := ParseBidImport(payload, "")
	if err != nil {
		t.Fatal(err)
	}
	if result.Format != "XML_LEGACY" || result.Items[0].Code != "X" {
		t.Fatalf("unexpected legacy result: %#v", result)
	}
}

func TestBidImportPreflightBlocksDuplicateCode(t *testing.T) {
	report := BuildBidImportPreflight([]BidImportItem{{ID: "1", Code: "A"}, {ID: "2", Code: "a"}})
	if report.CanContinue || report.ErrorCount != 1 || report.Errors[0].Code != "DUPLICATE_ITEM_CODE" {
		t.Fatalf("unexpected report: %#v", report)
	}
}
