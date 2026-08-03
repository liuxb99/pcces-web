package sqlite

import (
	"context"
	"testing"
)

func TestReportJobFailureAndRetry(t *testing.T) {
	store := newTestStore(t)
	ctx := context.Background()
	repo := NewReportAdminRepository(store)
	job, err := repo.CreateReportJob(ctx, "J-FAIL", "CONTRACT", "P1", "CV1", "PDF", "tester", map[string]any{"title": "Contract", "rows": []any{}}, map[string]any{})
	if err != nil { t.Fatal(err) }
	failed, err := repo.FailReportJob(ctx, "J-FAIL", job["row_version"].(int64), map[string]any{"message": "renderer unavailable"})
	if err != nil { t.Fatal(err) }
	if failed["status"] != "FAILED" { t.Fatalf("unexpected failed job: %#v", failed) }
	retried, err := repo.RetryReportJob(ctx, "J-FAIL", failed["row_version"].(int64))
	if err != nil { t.Fatal(err) }
	if retried["status"] != "QUEUED" || retried["progress"] != 0 { t.Fatalf("unexpected retried job: %#v", retried) }
	if _, err := repo.RetryReportJob(ctx, "J-FAIL", retried["row_version"].(int64)); err == nil { t.Fatal("expected retry rejection for non-failed job") }
}
