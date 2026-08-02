CREATE TABLE IF NOT EXISTS conversion_wizard_sessions (
    id TEXT PRIMARY KEY,
    source_project_code TEXT NOT NULL,
    source_budget_version_id TEXT NOT NULL,
    target_project_code TEXT NOT NULL,
    mode TEXT NOT NULL,
    status TEXT NOT NULL,
    options_json TEXT NOT NULL,
    report_json TEXT NOT NULL,
    can_continue INTEGER NOT NULL,
    created_by TEXT NOT NULL,
    created_at TEXT NOT NULL,
    row_version INTEGER NOT NULL DEFAULT 1
);
CREATE INDEX IF NOT EXISTS idx_conversion_wizard_source ON conversion_wizard_sessions(source_project_code);
