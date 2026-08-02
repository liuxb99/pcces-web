PRAGMA foreign_keys = ON;

CREATE TABLE IF NOT EXISTS subcontract_links_v2 (
    id TEXT PRIMARY KEY,
    parent_contract_id TEXT NOT NULL REFERENCES contracts_v2(id) ON DELETE CASCADE,
    subcontract_id TEXT NOT NULL UNIQUE REFERENCES contracts_v2(id) ON DELETE CASCADE,
    created_by TEXT NOT NULL,
    created_at TEXT NOT NULL,
    row_version INTEGER NOT NULL DEFAULT 1,
    CHECK (parent_contract_id <> subcontract_id)
);

CREATE INDEX IF NOT EXISTS idx_subcontract_links_parent ON subcontract_links_v2(parent_contract_id);
CREATE INDEX IF NOT EXISTS idx_subcontract_links_child ON subcontract_links_v2(subcontract_id);

INSERT OR IGNORE INTO schema_migrations(version) VALUES ('0039_contract_allocations');
