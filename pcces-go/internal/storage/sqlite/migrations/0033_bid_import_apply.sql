CREATE TABLE IF NOT EXISTS bid_import_apply_runs (
    id TEXT PRIMARY KEY,
    import_session_id TEXT NOT NULL,
    target_budget_project_code TEXT NOT NULL,
    target_budget_version_id TEXT NOT NULL,
    mode TEXT NOT NULL,
    status TEXT NOT NULL,
    inserted_count INTEGER NOT NULL,
    replaced_count INTEGER NOT NULL,
    skipped_count INTEGER NOT NULL,
    lineage_json TEXT NOT NULL,
    created_by TEXT NOT NULL,
    created_at TEXT NOT NULL
);
CREATE INDEX IF NOT EXISTS idx_bid_import_apply_runs_session ON bid_import_apply_runs(import_session_id, created_at);
INSERT OR IGNORE INTO schema_migrations(version) VALUES ('0033_bid_import_apply');
