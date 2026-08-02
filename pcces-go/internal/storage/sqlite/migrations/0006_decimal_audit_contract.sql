PRAGMA foreign_keys = ON;

CREATE TABLE IF NOT EXISTS p0_decimal_records (
    id TEXT PRIMARY KEY,
    value TEXT NOT NULL,
    created_at TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now')),
    updated_at TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now')),
    row_version INTEGER NOT NULL DEFAULT 1 CHECK (row_version >= 1)
);

CREATE TABLE IF NOT EXISTS p0_audit_events (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    actor_id TEXT,
    feature_id TEXT NOT NULL,
    action_code TEXT,
    event_type TEXT NOT NULL,
    resource_type TEXT,
    resource_id TEXT,
    payload TEXT,
    created_at TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now'))
);

CREATE INDEX IF NOT EXISTS idx_p0_audit_resource ON p0_audit_events(resource_type, resource_id, id);
CREATE INDEX IF NOT EXISTS idx_p0_audit_actor_created ON p0_audit_events(actor_id, created_at);

INSERT OR IGNORE INTO schema_migrations(version) VALUES ('0006_decimal_audit_contract');
