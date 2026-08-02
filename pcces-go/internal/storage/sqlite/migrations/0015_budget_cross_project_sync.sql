CREATE TABLE IF NOT EXISTS budget_cross_project_runs (
    id TEXT PRIMARY KEY,
    source_project_code TEXT NOT NULL,
    target_project_code TEXT NOT NULL,
    operation TEXT NOT NULL,
    status TEXT NOT NULL,
    result_json TEXT NOT NULL,
    created_by TEXT NOT NULL,
    created_at TEXT NOT NULL
);
CREATE INDEX IF NOT EXISTS idx_budget_cross_project_runs_source ON budget_cross_project_runs(source_project_code, created_at DESC);
CREATE INDEX IF NOT EXISTS idx_budget_cross_project_runs_target ON budget_cross_project_runs(target_project_code, created_at DESC);
