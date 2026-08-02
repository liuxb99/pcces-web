PRAGMA foreign_keys = ON;

CREATE TABLE IF NOT EXISTS cost_structure_categories (
    id TEXT PRIMARY KEY,
    cost_structure_type_id TEXT NOT NULL,
    code TEXT NOT NULL,
    name TEXT NOT NULL,
    kind TEXT NOT NULL,
    sequence INTEGER NOT NULL,
    rate TEXT NOT NULL DEFAULT '0',
    enabled INTEGER NOT NULL DEFAULT 1,
    row_version INTEGER NOT NULL DEFAULT 1,
    UNIQUE(cost_structure_type_id, code),
    FOREIGN KEY(cost_structure_type_id) REFERENCES cost_structure_types(id)
);
CREATE INDEX IF NOT EXISTS idx_cost_structure_categories_type ON cost_structure_categories(cost_structure_type_id, sequence, code);

CREATE TABLE IF NOT EXISTS budget_item_cost_properties (
    project_code TEXT NOT NULL,
    budget_item_id TEXT NOT NULL,
    cost_category_id TEXT NOT NULL,
    cost_kind TEXT NOT NULL,
    sign INTEGER NOT NULL DEFAULT 1,
    rate TEXT NOT NULL DEFAULT '0',
    updated_by TEXT NOT NULL,
    updated_at TEXT NOT NULL,
    row_version INTEGER NOT NULL DEFAULT 1,
    PRIMARY KEY(project_code, budget_item_id),
    FOREIGN KEY(cost_category_id) REFERENCES cost_structure_categories(id)
);

CREATE TABLE IF NOT EXISTS cost_structure_import_runs (
    id TEXT PRIMARY KEY,
    cost_structure_type_id TEXT NOT NULL,
    only_structure INTEGER NOT NULL DEFAULT 0,
    status TEXT NOT NULL,
    total_rows INTEGER NOT NULL,
    imported_rows INTEGER NOT NULL,
    errors_json TEXT NOT NULL DEFAULT '[]',
    created_by TEXT NOT NULL,
    created_at TEXT NOT NULL
);

INSERT OR IGNORE INTO schema_migrations(version) VALUES ('0025_cost_structure_details');
