PRAGMA foreign_keys = ON;
CREATE TABLE IF NOT EXISTS mrs_project_states (
    project_code TEXT PRIMARY KEY,
    state TEXT NOT NULL,
    template INTEGER NOT NULL DEFAULT 0,
    readonly INTEGER NOT NULL DEFAULT 0,
    reason TEXT,
    updated_by TEXT NOT NULL,
    updated_at TEXT NOT NULL,
    row_version INTEGER NOT NULL DEFAULT 1
);
INSERT OR IGNORE INTO schema_migrations(version) VALUES ('0022_mrs_project_state');
