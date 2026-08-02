CREATE TABLE IF NOT EXISTS bid_price_versions (
    id TEXT PRIMARY KEY,
    project_code TEXT NOT NULL,
    label TEXT NOT NULL,
    status TEXT NOT NULL,
    total_amount TEXT NOT NULL,
    snapshot_json TEXT NOT NULL,
    created_by TEXT NOT NULL,
    created_at TEXT NOT NULL
);
CREATE INDEX IF NOT EXISTS idx_bid_price_versions_project ON bid_price_versions(project_code, created_at DESC);

CREATE TABLE IF NOT EXISTS bid_conversion_runs (
    id TEXT PRIMARY KEY,
    source_project_code TEXT NOT NULL,
    target_project_code TEXT NOT NULL,
    operation TEXT NOT NULL,
    status TEXT NOT NULL,
    result_json TEXT NOT NULL,
    created_by TEXT NOT NULL,
    created_at TEXT NOT NULL
);
CREATE INDEX IF NOT EXISTS idx_bid_conversion_runs_target ON bid_conversion_runs(target_project_code, created_at DESC);
