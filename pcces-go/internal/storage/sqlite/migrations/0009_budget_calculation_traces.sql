CREATE TABLE IF NOT EXISTS budget_calculation_traces (
    id TEXT PRIMARY KEY,
    project_code TEXT NOT NULL,
    item_id TEXT,
    kind TEXT NOT NULL CHECK (kind IN ('B','L','F','S','U','Z')),
    input_json TEXT NOT NULL,
    steps_json TEXT NOT NULL,
    result TEXT NOT NULL,
    created_at TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now'))
);
CREATE INDEX IF NOT EXISTS idx_budget_calculation_traces_project ON budget_calculation_traces(project_code, created_at DESC);
CREATE INDEX IF NOT EXISTS idx_budget_calculation_traces_item ON budget_calculation_traces(item_id, created_at DESC);
