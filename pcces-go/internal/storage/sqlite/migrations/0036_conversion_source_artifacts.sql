CREATE TABLE IF NOT EXISTS conversion_source_artifacts (
    id TEXT PRIMARY KEY,
    session_type TEXT NOT NULL,
    session_id TEXT NOT NULL,
    original_filename TEXT NOT NULL,
    content_type TEXT NOT NULL,
    format TEXT NOT NULL,
    format_version TEXT NOT NULL,
    size_bytes INTEGER NOT NULL,
    sha256 TEXT NOT NULL,
    content BLOB NOT NULL,
    created_by TEXT NOT NULL,
    created_at TEXT NOT NULL,
    row_version INTEGER NOT NULL DEFAULT 1
);
CREATE INDEX IF NOT EXISTS idx_conversion_source_artifacts_session ON conversion_source_artifacts(session_type, session_id);

CREATE TABLE IF NOT EXISTS conversion_error_catalogues (
    id TEXT PRIMARY KEY,
    session_type TEXT NOT NULL,
    session_id TEXT NOT NULL,
    error_count INTEGER NOT NULL,
    warning_count INTEGER NOT NULL,
    catalogue_json TEXT NOT NULL,
    created_by TEXT NOT NULL,
    created_at TEXT NOT NULL,
    row_version INTEGER NOT NULL DEFAULT 1
);
CREATE INDEX IF NOT EXISTS idx_conversion_error_catalogues_session ON conversion_error_catalogues(session_type, session_id);

INSERT OR IGNORE INTO schema_migrations(version) VALUES ('0036_conversion_source_artifacts');
