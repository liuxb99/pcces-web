package sqlite

import "testing"

func TestParseLegacyExchangeZMD(t *testing.T) {
	result, err := ParseLegacyExchange(`{"project_code":"P1","version":"1.2","items":[{"id":"A","code":"a-1","name":"N"}]}`, "ZMD")
	if err != nil { t.Fatal(err) }
	if result.SourceProjectCode != "P1" || result.FormatVersion != "1.2" || result.Items[0].Code != "A-1" { t.Fatalf("unexpected result: %#v", result) }
}

func TestParseLegacyExchangeMDB(t *testing.T) {
	result, err := ParseLegacyExchange("project_code,id,code,name,unit,quantity,unit_price,amount\nP2,B,b-1,N,EA,1,2,2\n", "MDB")
	if err != nil { t.Fatal(err) }
	if result.FormatVersion != "CSV-1.0" || result.Items[0].Code != "B-1" { t.Fatalf("unexpected result: %#v", result) }
}

func TestParseLegacyExchangePX(t *testing.T) {
	result, err := ParseLegacyExchange(`<PX version="3.0"><Header><ProjectCode>P3</ProjectCode></Header><Items><Item><SourceItemId>C</SourceItemId><Code>c-1</Code><Name>N</Name></Item></Items></PX>`, "PX")
	if err != nil { t.Fatal(err) }
	if result.SourceProjectCode != "P3" || len(result.Items) != 1 || result.Items[0].Code != "C-1" { t.Fatalf("unexpected result: %#v", result) }
}
