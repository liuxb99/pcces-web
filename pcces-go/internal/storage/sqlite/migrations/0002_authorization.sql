PRAGMA foreign_keys = ON;

CREATE TABLE IF NOT EXISTS local_actors (
    actor_id TEXT PRIMARY KEY,
    display_name TEXT NOT NULL,
    active INTEGER NOT NULL DEFAULT 1 CHECK (active IN (0,1)),
    created_at TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now')),
    updated_at TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now')),
    row_version INTEGER NOT NULL DEFAULT 1
);

CREATE TABLE IF NOT EXISTS actor_function_codes (
    actor_id TEXT NOT NULL REFERENCES local_actors(actor_id) ON DELETE CASCADE,
    function_code TEXT NOT NULL REFERENCES function_codes(code) ON DELETE CASCADE,
    granted INTEGER NOT NULL DEFAULT 1 CHECK (granted IN (0,1)),
    created_at TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now')),
    updated_at TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now')),
    row_version INTEGER NOT NULL DEFAULT 1,
    PRIMARY KEY (actor_id, function_code)
);

CREATE TABLE IF NOT EXISTS actor_module_entitlements (
    actor_id TEXT NOT NULL REFERENCES local_actors(actor_id) ON DELETE CASCADE,
    module_code TEXT NOT NULL REFERENCES modules(code) ON DELETE CASCADE,
    enabled INTEGER NOT NULL DEFAULT 1 CHECK (enabled IN (0,1)),
    created_at TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now')),
    updated_at TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now')),
    row_version INTEGER NOT NULL DEFAULT 1,
    PRIMARY KEY (actor_id, module_code)
);

CREATE TABLE IF NOT EXISTS audit_events (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    actor_id TEXT,
    feature_id TEXT,
    action_code TEXT,
    event_type TEXT NOT NULL,
    resource_type TEXT,
    resource_id TEXT,
    payload TEXT,
    created_at TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now'))
);

CREATE INDEX IF NOT EXISTS idx_actor_function_codes_actor ON actor_function_codes(actor_id);
CREATE INDEX IF NOT EXISTS idx_actor_module_entitlements_actor ON actor_module_entitlements(actor_id);
CREATE INDEX IF NOT EXISTS idx_audit_events_actor_created ON audit_events(actor_id, created_at);
CREATE INDEX IF NOT EXISTS idx_audit_events_resource ON audit_events(resource_type, resource_id);

INSERT OR IGNORE INTO local_actors(actor_id, display_name) VALUES ('local-admin', 'Local Administrator');

INSERT OR IGNORE INTO actor_function_codes(actor_id, function_code)
SELECT 'local-admin', code FROM function_codes;

INSERT OR IGNORE INTO actor_module_entitlements(actor_id, module_code)
SELECT 'local-admin', code FROM modules;

INSERT OR IGNORE INTO schema_migrations(version) VALUES ('0002_authorization');
