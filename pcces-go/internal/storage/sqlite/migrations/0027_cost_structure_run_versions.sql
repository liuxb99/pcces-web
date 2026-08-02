CREATE TABLE IF NOT EXISTS cost_structure_run_versions (
    id TEXT PRIMARY KEY,
    project_code TEXT NOT NULL,
    run_id TEXT NOT NULL UNIQUE,
    budget_version_id TEXT NOT NULL,
    budget_status TEXT NOT NULL,
    direct_cost TEXT NOT NULL,
    total_cost TEXT NOT NULL,
    trace_json TEXT NOT NULL,
    created_by TEXT NOT NULL,
    created_at TEXT NOT NULL,
    row_version INTEGER NOT NULL DEFAULT 1
);
CREATE INDEX IF NOT EXISTS idx_cost_run_versions_project ON cost_structure_run_versions(project_code);
CREATE INDEX IF NOT EXISTS idx_cost_run_versions_budget ON cost_structure_run_versions(budget_version_id);
