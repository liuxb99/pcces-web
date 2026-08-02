CREATE TABLE IF NOT EXISTS contract_versions_v2 (
    id TEXT PRIMARY KEY,
    contract_id TEXT NOT NULL,
    version_no INTEGER NOT NULL,
    status TEXT NOT NULL,
    snapshot_json TEXT NOT NULL,
    note TEXT,
    created_by TEXT NOT NULL,
    created_at TEXT NOT NULL,
    approved_by TEXT,
    approved_at TEXT,
    row_version INTEGER NOT NULL DEFAULT 1,
    UNIQUE(contract_id, version_no)
);
CREATE INDEX IF NOT EXISTS idx_contract_versions_contract ON contract_versions_v2(contract_id, version_no);
