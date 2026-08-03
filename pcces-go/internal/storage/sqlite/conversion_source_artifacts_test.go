package sqlite

import (
	"context"
	"crypto/sha256"
	"encoding/hex"
	"strings"
	"testing"
)

func TestConversionSourceArtifactAndCatalogue(t *testing.T) {
	store := newTestStore(t)
	repo := NewConversionSourceArtifactRepository(store)
	content := []byte("<PX version='1.0'/>")
	item, err := repo.CreateSource(context.Background(), ConversionSourceArtifact{SessionType: "IMPORT", SessionID: "S1", OriginalFilename: "source.px", ContentType: "application/xml", Format: "PX", FormatVersion: "1.0", CreatedBy: "tester"}, content)
	if err != nil {
		t.Fatal(err)
	}
	h := sha256.Sum256(content)
	if item.SHA256 != hex.EncodeToString(h[:]) || item.SizeBytes != int64(len(content)) {
		t.Fatalf("unexpected metadata: %#v", item)
	}
	loaded, ctype, filename, err := repo.SourceContent(context.Background(), item.ID)
	if err != nil || string(loaded) != string(content) || ctype != "application/xml" || filename != "source.px" {
		t.Fatalf("unexpected source content")
	}
	cat, err := repo.CreateCatalogue(context.Background(), "IMPORT", "S1", []map[string]any{{"code": "DUPLICATE_ITEM_CODE", "item_code": "A1"}}, []map[string]any{{"code": "MISSING_ITEM_NAME", "index": 2}}, "tester")
	if err != nil {
		t.Fatal(err)
	}
	csvBytes, _, err := repo.CatalogueCSV(context.Background(), cat.ID)
	if err != nil || !strings.Contains(string(csvBytes), "DUPLICATE_ITEM_CODE") || !strings.Contains(string(csvBytes), "MISSING_ITEM_NAME") {
		t.Fatalf("unexpected catalogue CSV")
	}
}
