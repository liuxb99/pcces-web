CREATE TABLE IF NOT EXISTS combine_bid_sessions (
    id TEXT PRIMARY KEY,
    target_project_code TEXT NOT NULL,
    strategy TEXT NOT NULL,
    status TEXT NOT NULL,
    sources_json TEXT NOT NULL,
    conflicts_json TEXT NOT NULL,
    result_json TEXT NOT NULL,
    created_by TEXT NOT NULL,
    created_at TEXT NOT NULL,
    row_version INTEGER NOT NULL DEFAULT 1
);
CREATE INDEX IF NOT EXISTS idx_combine_bid_sessions_target ON combine_bid_sessions(target_project_code, created_at);
INSERT OR IGNORE INTO schema_migrations(version) VALUES ('0034_combine_bid_sessions');
