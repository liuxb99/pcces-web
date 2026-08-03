package sqlite

import (
	"context"
	"database/sql"
	"encoding/json"
	"time"

	errx "github.com/liuxb99/pcces-web/pcces-go/internal/platform/errors"
)

func (r *ReportAdminRepository) FailReportJob(ctx context.Context, jobID string, rowVersion int64, failure map[string]any) (map[string]any, error) {
	var status string
	var current int64
	if err := r.store.db.QueryRowContext(ctx, `SELECT status,row_version FROM report_jobs WHERE id=?`, jobID).Scan(&status, &current); err == sql.ErrNoRows {
		return nil, errx.New(errx.CodeNotFound, "report job not found", "P7-G")
	} else if err != nil {
		return nil, err
	}
	if current != rowVersion {
		return nil, errx.New(errx.CodeConflict, "row version conflict", "P7-G")
	}
	if status != "QUEUED" && status != "RUNNING" {
		return nil, errx.New(errx.CodeInvalidArgument, "job cannot fail from current status", "P7-G")
	}
	if failure == nil {
		failure = map[string]any{"message": "report rendering failed"}
	}
	encoded, _ := json.Marshal(failure)
	result, err := r.store.db.ExecContext(ctx, `UPDATE report_jobs SET status='FAILED',error_json=?,updated_at=?,row_version=row_version+1 WHERE id=? AND row_version=?`, string(encoded), time.Now().UTC().Format(time.RFC3339Nano), jobID, rowVersion)
	if err != nil {
		return nil, err
	}
	if affected, _ := result.RowsAffected(); affected != 1 {
		return nil, errx.New(errx.CodeConflict, "row version conflict", "P7-G")
	}
	return r.GetReportJob(ctx, jobID)
}

func (r *ReportAdminRepository) RetryReportJob(ctx context.Context, jobID string, rowVersion int64) (map[string]any, error) {
	var status string
	var current int64
	if err := r.store.db.QueryRowContext(ctx, `SELECT status,row_version FROM report_jobs WHERE id=?`, jobID).Scan(&status, &current); err == sql.ErrNoRows {
		return nil, errx.New(errx.CodeNotFound, "report job not found", "P7-G")
	} else if err != nil {
		return nil, err
	}
	if current != rowVersion {
		return nil, errx.New(errx.CodeConflict, "row version conflict", "P7-G")
	}
	if status != "FAILED" {
		return nil, errx.New(errx.CodeInvalidArgument, "only failed report jobs can retry", "P7-G")
	}
	result, err := r.store.db.ExecContext(ctx, `UPDATE report_jobs SET status='QUEUED',progress=0,error_json=NULL,updated_at=?,row_version=row_version+1 WHERE id=? AND row_version=?`, time.Now().UTC().Format(time.RFC3339Nano), jobID, rowVersion)
	if err != nil {
		return nil, err
	}
	if affected, _ := result.RowsAffected(); affected != 1 {
		return nil, errx.New(errx.CodeConflict, "row version conflict", "P7-G")
	}
	return r.GetReportJob(ctx, jobID)
}
