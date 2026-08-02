CREATE TABLE IF NOT EXISTS budget_bid_conversion_sessions (
    id TEXT PRIMARY KEY,
    source_project_code TEXT NOT NULL,
    source_budget_version_id TEXT NOT NULL,
    target_bid_project_code TEXT NOT NULL,
    mode TEXT NOT NULL,
    status TEXT NOT NULL,
    options_json TEXT NOT NULL,
    source_snapshot_json TEXT NOT NULL,
    result_snapshot_json TEXT NOT NULL,
    created_by TEXT NOT NULL,
    created_at TEXT NOT NULL,
    row_version INTEGER NOT NULL DEFAULT 1
);
CREATE INDEX IF NOT EXISTS idx_budget_bid_conversion_source ON budget_bid_conversion_sessions(source_project_code, source_budget_version_id);
CREATE INDEX IF NOT EXISTS idx_budget_bid_conversion_target ON budget_bid_conversion_sessions(target_bid_project_code);
