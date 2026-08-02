PRAGMA foreign_keys = ON;

CREATE TABLE IF NOT EXISTS local_settings (
    key TEXT PRIMARY KEY,
    value TEXT NOT NULL,
    value_type TEXT NOT NULL DEFAULT 'string',
    description TEXT,
    created_at TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now')),
    updated_at TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now')),
    row_version INTEGER NOT NULL DEFAULT 1
);

CREATE TABLE IF NOT EXISTS recovery_snapshots (
    id TEXT PRIMARY KEY,
    actor_id TEXT NOT NULL,
    context_id TEXT,
    project_code TEXT,
    action_code TEXT,
    payload TEXT NOT NULL,
    payload_hash TEXT NOT NULL,
    reason TEXT NOT NULL,
    created_at TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now')),
    restored_at TEXT,
    discarded_at TEXT,
    row_version INTEGER NOT NULL DEFAULT 1
);

CREATE INDEX IF NOT EXISTS idx_recovery_actor_created
    ON recovery_snapshots(actor_id, created_at DESC);
CREATE INDEX IF NOT EXISTS idx_recovery_project_action
    ON recovery_snapshots(project_code, action_code, created_at DESC);

INSERT OR IGNORE INTO local_settings(key, value, value_type, description) VALUES
('autosave.enabled', 'true', 'bool', 'Enable local autosave snapshots'),
('autosave.interval_seconds', '60', 'int', 'Autosave interval in seconds'),
('backup.keep_count', '10', 'int', 'Maximum automatic backups to retain'),
('sqlite.busy_timeout_ms', '5000', 'int', 'SQLite busy timeout'),
('sqlite.integrity_check_on_start', 'false', 'bool', 'Run integrity check during startup');

INSERT OR IGNORE INTO schema_migrations(version) VALUES ('0003_settings_recovery');
