CREATE TABLE IF NOT EXISTS resource_price_history (
    id TEXT PRIMARY KEY,
    project_code TEXT NOT NULL,
    resource_id TEXT NOT NULL,
    old_price TEXT NOT NULL,
    new_price TEXT NOT NULL,
    source TEXT NOT NULL,
    created_at TEXT NOT NULL
);
CREATE INDEX IF NOT EXISTS idx_resource_price_history_project ON resource_price_history(project_code, created_at DESC);

CREATE TABLE IF NOT EXISTS dependency_recalculation_runs (
    id TEXT PRIMARY KEY,
    project_code TEXT NOT NULL,
    scope TEXT NOT NULL,
    resource_id TEXT,
    status TEXT NOT NULL,
    result_json TEXT NOT NULL,
    created_at TEXT NOT NULL
);
CREATE INDEX IF NOT EXISTS idx_dependency_runs_project ON dependency_recalculation_runs(project_code, created_at DESC);
