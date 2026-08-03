PRAGMA foreign_keys = ON;

INSERT OR IGNORE INTO local_settings(key, value, value_type, description) VALUES
('backup.auto_enabled', 'true', 'bool', 'Enable scheduled local database backups'),
('backup.interval_hours', '24', 'int', 'Scheduled backup interval in hours'),
('backup.directory', '', 'string', 'Automatic backup directory (empty uses database sibling backups folder)');

INSERT OR IGNORE INTO schema_migrations(version) VALUES ('0004_automatic_backup');
