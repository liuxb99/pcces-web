CREATE TABLE IF NOT EXISTS conversion_long_jobs (
 id TEXT PRIMARY KEY,
 job_type TEXT NOT NULL,
 status TEXT NOT NULL,
 progress INTEGER NOT NULL,
 stage TEXT NOT NULL,
 payload_json TEXT NOT NULL,
 result_json TEXT,
 error_json TEXT,
 cancel_requested INTEGER NOT NULL DEFAULT 0,
 created_by TEXT NOT NULL,
 created_at TEXT NOT NULL,
 updated_at TEXT NOT NULL,
 row_version INTEGER NOT NULL DEFAULT 1
);
CREATE INDEX IF NOT EXISTS idx_conversion_long_jobs_status ON conversion_long_jobs(status, updated_at);
