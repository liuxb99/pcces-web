PRAGMA foreign_keys = ON;

CREATE TABLE IF NOT EXISTS mrs_recipe_versions (
    id TEXT PRIMARY KEY,
    recipe_id TEXT NOT NULL,
    label TEXT NOT NULL,
    unit_price TEXT NOT NULL,
    snapshot_json TEXT NOT NULL,
    created_by TEXT NOT NULL,
    created_at TEXT NOT NULL
);
CREATE INDEX IF NOT EXISTS idx_mrs_recipe_versions_recipe ON mrs_recipe_versions(recipe_id, created_at DESC);

CREATE TABLE IF NOT EXISTS mrs_import_jobs (
    id TEXT PRIMARY KEY,
    format TEXT NOT NULL,
    payload TEXT NOT NULL,
    overwrite INTEGER NOT NULL DEFAULT 0,
    status TEXT NOT NULL,
    total_rows INTEGER NOT NULL DEFAULT 0,
    processed_rows INTEGER NOT NULL DEFAULT 0,
    imported_rows INTEGER NOT NULL DEFAULT 0,
    skipped_rows INTEGER NOT NULL DEFAULT 0,
    errors_json TEXT NOT NULL DEFAULT '[]',
    cancel_requested INTEGER NOT NULL DEFAULT 0,
    created_by TEXT NOT NULL,
    created_at TEXT NOT NULL,
    updated_at TEXT NOT NULL
);
CREATE INDEX IF NOT EXISTS idx_mrs_import_jobs_status ON mrs_import_jobs(status, created_at DESC);

INSERT OR IGNORE INTO schema_migrations(version) VALUES ('0019_mrs_operations');
