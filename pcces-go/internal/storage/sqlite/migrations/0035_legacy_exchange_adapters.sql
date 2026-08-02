CREATE TABLE IF NOT EXISTS legacy_adapter_sessions (
    id TEXT PRIMARY KEY,
    format TEXT NOT NULL,
    format_version TEXT NOT NULL,
    source_filename TEXT NOT NULL,
    source_project_code TEXT NOT NULL,
    target_project_code TEXT NOT NULL,
    status TEXT NOT NULL,
    report_json TEXT NOT NULL,
    items_json TEXT NOT NULL,
    created_by TEXT NOT NULL,
    created_at TEXT NOT NULL,
    row_version INTEGER NOT NULL DEFAULT 1
);
CREATE INDEX IF NOT EXISTS idx_legacy_adapter_sessions_target ON legacy_adapter_sessions(target_project_code, created_at);
INSERT OR IGNORE INTO schema_migrations(version) VALUES ('0035_legacy_exchange_adapters');
