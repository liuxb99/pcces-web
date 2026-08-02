PRAGMA foreign_keys = ON;

CREATE TABLE IF NOT EXISTS schema_migrations (
    version TEXT PRIMARY KEY,
    applied_at TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now'))
);

CREATE TABLE IF NOT EXISTS feature_catalog (
    feature_id TEXT PRIMARY KEY,
    name TEXT NOT NULL,
    module_code TEXT NOT NULL,
    legacy_source TEXT,
    status TEXT NOT NULL DEFAULT 'NOT_STARTED',
    created_at TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now')),
    updated_at TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now')),
    row_version INTEGER NOT NULL DEFAULT 1
);

CREATE TABLE IF NOT EXISTS function_codes (
    code TEXT PRIMARY KEY,
    name TEXT NOT NULL,
    enabled INTEGER NOT NULL DEFAULT 1 CHECK (enabled IN (0,1)),
    created_at TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now')),
    updated_at TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now')),
    row_version INTEGER NOT NULL DEFAULT 1
);

CREATE TABLE IF NOT EXISTS modules (
    code TEXT PRIMARY KEY,
    name TEXT NOT NULL,
    enabled INTEGER NOT NULL DEFAULT 1 CHECK (enabled IN (0,1)),
    created_at TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now')),
    updated_at TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now')),
    row_version INTEGER NOT NULL DEFAULT 1
);

CREATE TABLE IF NOT EXISTS actions (
    code TEXT PRIMARY KEY,
    name TEXT NOT NULL,
    module_code TEXT NOT NULL REFERENCES modules(code),
    function_code TEXT REFERENCES function_codes(code),
    created_at TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now')),
    updated_at TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now')),
    row_version INTEGER NOT NULL DEFAULT 1
);

CREATE TABLE IF NOT EXISTS work_contexts (
    id TEXT PRIMARY KEY,
    actor_id TEXT NOT NULL,
    action_code TEXT NOT NULL REFERENCES actions(code),
    project_code TEXT,
    resource_type TEXT,
    resource_id TEXT,
    dirty INTEGER NOT NULL DEFAULT 0 CHECK (dirty IN (0,1)),
    draft_payload TEXT,
    created_at TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now')),
    updated_at TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now')),
    row_version INTEGER NOT NULL DEFAULT 1
);

CREATE INDEX IF NOT EXISTS idx_work_context_actor ON work_contexts(actor_id);
CREATE INDEX IF NOT EXISTS idx_work_context_project_action ON work_contexts(project_code, action_code);

INSERT OR IGNORE INTO modules(code, name) VALUES
('BUDGET', '預算編製'),
('BID', '投標單'),
('COMMON', '共用資料'),
('INVOICE', '契約履約');

INSERT OR IGNORE INTO function_codes(code, name) VALUES
('F001', '系統維護'),
('F002', '基本資料庫維護'),
('F003', '預算書編製'),
('F004', '投標單填寫'),
('F005', '專案目錄'),
('F006', '系統外掛'),
('F007', '單價分析比對'),
('F008', '歷史工程比對'),
('F009', '契約編製'),
('F010', '估驗記錄'),
('F011', '契約變更'),
('F012', '結算作業');

INSERT OR IGNORE INTO actions(code, name, module_code, function_code) VALUES
('BUD', '預算編製', 'BUDGET', 'F003'),
('BID', '投標單填寫', 'BID', 'F004'),
('PROJECT_CATALOG', '專案目錄', 'COMMON', 'F005'),
('SPLIT_CONTRACT', '契約編製', 'INVOICE', 'F009'),
('INVOICE', '估驗記錄', 'INVOICE', 'F010'),
('BUDGET_CHANGE', '契約變更', 'INVOICE', 'F011'),
('SUB_CLOSE', '結算作業', 'INVOICE', 'F012'),
('SUB_FINAL', '驗收作業', 'INVOICE', NULL);

INSERT OR IGNORE INTO schema_migrations(version) VALUES ('0001_phase0');
