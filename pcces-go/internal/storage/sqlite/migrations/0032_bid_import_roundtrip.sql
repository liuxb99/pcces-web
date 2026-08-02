CREATE TABLE IF NOT EXISTS bid_import_sessions (
    id TEXT PRIMARY KEY,
    source_format TEXT NOT NULL,
    format_version TEXT NOT NULL,
    source_bid_project_code TEXT NOT NULL,
    target_budget_project_code TEXT NOT NULL,
    source_conversion_session_id TEXT,
    status TEXT NOT NULL,
    report_json TEXT NOT NULL,
    items_json TEXT NOT NULL,
    created_by TEXT NOT NULL,
    created_at TEXT NOT NULL,
    row_version INTEGER NOT NULL DEFAULT 1
);
CREATE INDEX IF NOT EXISTS idx_bid_import_sessions_target ON bid_import_sessions(target_budget_project_code, created_at);
