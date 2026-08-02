CREATE TABLE IF NOT EXISTS conversion_export_jobs (
    id TEXT PRIMARY KEY,
    wizard_session_id TEXT NOT NULL,
    source_budget_version_id TEXT NOT NULL,
    target_project_code TEXT NOT NULL,
    format TEXT NOT NULL,
    status TEXT NOT NULL,
    filename TEXT NOT NULL,
    content_type TEXT NOT NULL,
    size_bytes INTEGER NOT NULL,
    sha256 TEXT NOT NULL,
    artifact BLOB NOT NULL,
    metadata_json TEXT NOT NULL,
    created_by TEXT NOT NULL,
    created_at TEXT NOT NULL,
    row_version INTEGER NOT NULL DEFAULT 1
);
CREATE INDEX IF NOT EXISTS idx_conversion_export_jobs_wizard ON conversion_export_jobs(wizard_session_id);
