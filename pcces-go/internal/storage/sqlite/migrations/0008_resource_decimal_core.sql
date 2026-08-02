PRAGMA foreign_keys = ON;

CREATE TABLE IF NOT EXISTS resources_decimal (
    id TEXT PRIMARY KEY,
    code TEXT NOT NULL UNIQUE,
    name TEXT NOT NULL,
    unit TEXT,
    unit_price TEXT NOT NULL DEFAULT '0.0000',
    price_scale INTEGER NOT NULL DEFAULT 4 CHECK(price_scale BETWEEN 0 AND 8),
    created_at TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now')),
    updated_at TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now')),
    row_version INTEGER NOT NULL DEFAULT 1
);

CREATE TABLE IF NOT EXISTS resource_breakdowns_decimal (
    id TEXT PRIMARY KEY,
    resource_id TEXT NOT NULL REFERENCES resources_decimal(id) ON DELETE CASCADE,
    code TEXT NOT NULL,
    name TEXT NOT NULL,
    unit TEXT,
    quantity TEXT NOT NULL DEFAULT '0.0000',
    unit_price TEXT NOT NULL DEFAULT '0.0000',
    amount TEXT NOT NULL DEFAULT '0.00',
    quantity_scale INTEGER NOT NULL DEFAULT 4 CHECK(quantity_scale BETWEEN 0 AND 8),
    price_scale INTEGER NOT NULL DEFAULT 4 CHECK(price_scale BETWEEN 0 AND 8),
    amount_scale INTEGER NOT NULL DEFAULT 2 CHECK(amount_scale BETWEEN 0 AND 8),
    created_at TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now')),
    updated_at TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now')),
    row_version INTEGER NOT NULL DEFAULT 1
);

CREATE INDEX IF NOT EXISTS idx_resource_breakdowns_decimal_resource
ON resource_breakdowns_decimal(resource_id);

INSERT OR IGNORE INTO schema_migrations(version) VALUES ('0008_resource_decimal_core');
