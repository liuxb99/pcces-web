CREATE TABLE IF NOT EXISTS project_cost_structure_runs (
    id TEXT PRIMARY KEY,
    project_code TEXT NOT NULL,
    cost_structure_type_id TEXT NOT NULL,
    direct_cost TEXT NOT NULL,
    total TEXT NOT NULL,
    scale INTEGER NOT NULL,
    budget_snapshot_json TEXT NOT NULL,
    result_json TEXT NOT NULL,
    created_by TEXT NOT NULL,
    created_at TEXT NOT NULL,
    row_version INTEGER NOT NULL DEFAULT 1
);
CREATE INDEX IF NOT EXISTS idx_project_cost_structure_runs_project ON project_cost_structure_runs(project_code, created_at DESC);
