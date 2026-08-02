PRAGMA foreign_keys = ON;

CREATE TABLE IF NOT EXISTS budget_items_decimal (
    id TEXT PRIMARY KEY,
    project_code TEXT NOT NULL,
    parent_id TEXT REFERENCES budget_items_decimal(id) ON DELETE SET NULL,
    item_no TEXT,
    name TEXT NOT NULL,
    kind TEXT NOT NULL,
    quantity TEXT NOT NULL DEFAULT '0.0000',
    unit_price TEXT NOT NULL DEFAULT '0.0000',
    amount TEXT NOT NULL DEFAULT '0.00',
    quantity_scale INTEGER NOT NULL DEFAULT 4,
    price_scale INTEGER NOT NULL DEFAULT 4,
    amount_scale INTEGER NOT NULL DEFAULT 2,
    created_at TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now')),
    updated_at TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now')),
    row_version INTEGER NOT NULL DEFAULT 1
);

CREATE INDEX IF NOT EXISTS idx_budget_items_decimal_project ON budget_items_decimal(project_code);
CREATE INDEX IF NOT EXISTS idx_budget_items_decimal_parent ON budget_items_decimal(parent_id);

INSERT OR IGNORE INTO schema_migrations(version) VALUES ('0007_budget_decimal_core');
