CREATE TABLE IF NOT EXISTS conversion_export_artifact_versions (
 id TEXT PRIMARY KEY,
 job_id TEXT NOT NULL,
 version_no INTEGER NOT NULL,
 format TEXT NOT NULL,
 status TEXT NOT NULL,
 filename TEXT NOT NULL,
 content_type TEXT NOT NULL,
 size_bytes INTEGER NOT NULL,
 sha256 TEXT NOT NULL,
 artifact BLOB NOT NULL,
 validation_json TEXT NOT NULL,
 error_message TEXT NOT NULL DEFAULT '',
 created_by TEXT NOT NULL,
 created_at TEXT NOT NULL,
 UNIQUE(job_id, version_no)
);
CREATE INDEX IF NOT EXISTS idx_conversion_export_artifact_job ON conversion_export_artifact_versions(job_id, version_no);
