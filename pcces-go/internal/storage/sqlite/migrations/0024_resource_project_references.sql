PRAGMA foreign_keys = ON;

CREATE TABLE IF NOT EXISTS resource_project_references (
    id TEXT PRIMARY KEY,
    target_project_code TEXT NOT NULL,
    source_project_code TEXT NOT NULL,
    source_resource_id TEXT NOT NULL,
    target_resource_id TEXT NOT NULL UNIQUE,
    reference_type TEXT NOT NULL CHECK(reference_type IN ('PARENT','HISTORICAL')),
    snapshot_json TEXT NOT NULL,
    created_by TEXT NOT NULL,
    created_at TEXT NOT NULL
);
CREATE INDEX IF NOT EXISTS idx_resource_project_refs_target
    ON resource_project_references(target_project_code, created_at DESC);

INSERT OR IGNORE INTO schema_migrations(version) VALUES ('0024_resource_project_references');
