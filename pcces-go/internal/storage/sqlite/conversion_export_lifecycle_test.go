package sqlite

import (
	"archive/zip"
	"bytes"
	"testing"
)

func TestSerializeBidXLSX(t *testing.T) {
	payload, err := SerializeBidXLSX([]ConversionExportItem{{ID: "1", Code: "a1", Name: "Work"}}, "BID1", "V1")
	if err != nil {
		t.Fatal(err)
	}
	if _, err := zip.NewReader(bytes.NewReader(payload), int64(len(payload))); err != nil {
		t.Fatalf("invalid xlsx: %v", err)
	}
}

func TestValidateConversionXML(t *testing.T) {
	valid := []byte(`<?xml version="1.0"?><PCCESBidExchange version="2.0"><Header/><Items/></PCCESBidExchange>`)
	if ValidateConversionXML(valid, "XML_NEW")["valid"] != true {
		t.Fatal("expected valid xml")
	}
	if ValidateConversionXML([]byte(`<PCCES version="2.0"/>`), "XML_NEW")["valid"] != false {
		t.Fatal("expected invalid xml")
	}
}
