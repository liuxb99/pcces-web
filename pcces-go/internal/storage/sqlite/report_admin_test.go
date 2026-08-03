package sqlite

import (
	"archive/zip"
	"bytes"
	"context"
	"testing"
)

func TestReportJobAndTypedSetting(t *testing.T) {
	store := newTestStore(t)
	ctx := context.Background()
	repo := NewReportAdminRepository(store)
	job, err := repo.CreateReportJob(ctx, "J1", "CONTRACT", "P1", "CV1", "PDF", "u", map[string]any{"title": "Contract", "rows": []any{}}, map[string]any{})
	if err != nil {
		t.Fatal(err)
	}
	if job["status"] != "QUEUED" {
		t.Fatalf("unexpected job %#v", job)
	}
	done, err := repo.RenderReport(ctx, "J1", "A1", 1)
	if err != nil {
		t.Fatal(err)
	}
	if done["status"] != "COMPLETED" {
		t.Fatalf("unexpected done %#v", done)
	}
	content, ctype, _, err := repo.ReportArtifact(ctx, "A1", "u")
	if err != nil {
		t.Fatal(err)
	}
	if len(content) == 0 || ctype != "application/pdf" || !bytes.HasPrefix(content, []byte("%PDF")) {
		t.Fatalf("bad artifact %s %d", ctype, len(content))
	}
	xlsx, err := repo.CreateReportJob(ctx, "J2", "INVOICE", "P1", "IV1", "XLSX", "u", map[string]any{"title": "Invoice", "rows": []any{map[string]any{"amount": "10"}}}, map[string]any{})
	if err != nil {
		t.Fatal(err)
	}
	_, err = repo.RenderReport(ctx, "J2", "A2", xlsx["row_version"].(int64))
	if err != nil {
		t.Fatal(err)
	}
	content, ctype, _, err = repo.ReportArtifact(ctx, "A2", "u")
	if err != nil {
		t.Fatal(err)
	}
	if ctype != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" {
		t.Fatalf("bad xlsx type %s", ctype)
	}
	zr, err := zip.NewReader(bytes.NewReader(content), int64(len(content)))
	if err != nil {
		t.Fatal(err)
	}
	found := false
	for _, file := range zr.File {
		if file.Name == "xl/worksheets/sheet1.xml" {
			found = true
		}
	}
	if !found {
		t.Fatal("xlsx sheet missing")
	}
	setting, err := repo.SetSetting(ctx, "autosave.interval_seconds", float64(60), 0, "u")
	if err != nil {
		t.Fatal(err)
	}
	if setting["row_version"].(int64) != 1 {
		t.Fatalf("bad setting %#v", setting)
	}
	group, err := repo.CreateGroup(ctx, "G1", "ENG", "Engineering", "u")
	if err != nil {
		t.Fatal(err)
	}
	if group["code"] != "ENG" {
		t.Fatalf("bad group %#v", group)
	}
	if _, err = repo.AddGroupMember(ctx, "G1", "U1", "u"); err != nil {
		t.Fatal(err)
	}
}
