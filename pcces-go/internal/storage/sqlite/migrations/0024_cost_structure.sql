PRAGMA foreign_keys = ON;

CREATE TABLE IF NOT EXISTS cost_structure_types (
    id TEXT PRIMARY KEY,
    code TEXT NOT NULL UNIQUE,
    name TEXT NOT NULL,
    description TEXT NOT NULL DEFAULT '',
    source TEXT NOT NULL,
    version TEXT NOT NULL,
    enabled INTEGER NOT NULL DEFAULT 1,
    created_by TEXT NOT NULL,
    created_at TEXT NOT NULL,
    updated_at TEXT NOT NULL,
    row_version INTEGER NOT NULL DEFAULT 1
);
CREATE INDEX IF NOT EXISTS idx_cost_structure_types_enabled ON cost_structure_types(enabled, code);

CREATE TABLE IF NOT EXISTS project_cost_structures (
    project_code TEXT PRIMARY KEY,
    cost_structure_type_id TEXT NOT NULL,
    issue TEXT NOT NULL DEFAULT 'BUD',
    assigned_by TEXT NOT NULL,
    assigned_at TEXT NOT NULL,
    row_version INTEGER NOT NULL DEFAULT 1,
    FOREIGN KEY(cost_structure_type_id) REFERENCES cost_structure_types(id)
);
CREATE INDEX IF NOT EXISTS idx_project_cost_structures_type ON project_cost_structures(cost_structure_type_id);

INSERT OR IGNORE INTO schema_migrations(version) VALUES ('0024_cost_structure');
