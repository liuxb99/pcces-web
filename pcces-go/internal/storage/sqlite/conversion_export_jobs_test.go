package sqlite

import (
	"context"
	"encoding/xml"
	"path/filepath"
	"strings"
	"testing"
)

func TestConversionExportJobsXMLAndMetadata(t *testing.T) {
	ctx := context.Background()
	store, err := Open(ctx, filepath.Join(t.TempDir(), "export.db"))
	if err != nil {
		t.Fatal(err)
	}
	defer store.Close()
	repo := NewConversionExportJobRepository(store)
	job, err := repo.Create(ctx, ConversionExportRequest{
		WizardSessionID: "W1", SourceBudgetVersionID: "BV1", TargetProjectCode: "BID1",
		Format: "XML_NEW", ActorID: "u1",
		Items: []ConversionExportItem{{SourceBudgetItemID: "I1", Code: " a001 ", Name: "混凝土", Unit: "M3", Quantity: "2", UnitPrice: "100", Amount: "200"}},
	})
	if err != nil {
		t.Fatal(err)
	}
	if job.Status != "COMPLETED" || job.SizeBytes == 0 || len(job.SHA256) != 64 {
		t.Fatalf("unexpected job %#v", job)
	}
	content, contentType, filename, err := repo.Artifact(ctx, job.ID)
	if err != nil {
		t.Fatal(err)
	}
	if !strings.Contains(contentType, "application/xml") || !strings.HasSuffix(filename, ".xml") {
		t.Fatalf("unexpected artifact %s %s", contentType, filename)
	}
	var root struct{ XMLName xml.Name }
	if err := xml.Unmarshal(content, &root); err != nil {
		t.Fatal(err)
	}
	if root.XMLName.Local != "PCCESBidExchange" || !strings.Contains(string(content), "A001") {
		t.Fatalf("unexpected xml %s", content)
	}
}

func TestConversionExportJobsLegacyAndValidation(t *testing.T) {
	ctx := context.Background()
	store, err := Open(ctx, filepath.Join(t.TempDir(), "legacy.db"))
	if err != nil {
		t.Fatal(err)
	}
	defer store.Close()
	repo := NewConversionExportJobRepository(store)
	job, err := repo.Create(ctx, ConversionExportRequest{WizardSessionID: "W1", SourceBudgetVersionID: "BV1", TargetProjectCode: "BID1", Format: "XML_LEGACY", ActorID: "u1", Items: []ConversionExportItem{{ID: "I1", Code: "A1"}}})
	if err != nil {
		t.Fatal(err)
	}
	content, _, _, err := repo.Artifact(ctx, job.ID)
	if err != nil {
		t.Fatal(err)
	}
	if !strings.Contains(string(content), "<PCCES version=\"1.0\">") || !strings.Contains(string(content), "<Record sequence=\"1\">") {
		t.Fatalf("unexpected legacy xml %s", content)
	}
	if _, err = repo.Create(ctx, ConversionExportRequest{WizardSessionID: "W1", SourceBudgetVersionID: "BV1", TargetProjectCode: "BID1", Format: "XLSX", Items: []ConversionExportItem{{ID: "I1"}}}); err == nil {
		t.Fatal("expected unsupported format")
	}
}
