PRAGMA foreign_keys = ON;

CREATE TABLE IF NOT EXISTS contracts_v2 (
  id TEXT PRIMARY KEY,
  project_code TEXT NOT NULL,
  budget_version_id TEXT NOT NULL,
  contract_no TEXT NOT NULL,
  name TEXT NOT NULL,
  contractor TEXT,
  status TEXT NOT NULL,
  contract_amount TEXT NOT NULL,
  created_by TEXT NOT NULL,
  created_at TEXT NOT NULL,
  updated_at TEXT NOT NULL,
  row_version INTEGER NOT NULL DEFAULT 1,
  UNIQUE(project_code, contract_no)
);
CREATE INDEX IF NOT EXISTS idx_contracts_v2_project ON contracts_v2(project_code);
CREATE INDEX IF NOT EXISTS idx_contracts_v2_budget_version ON contracts_v2(budget_version_id);

CREATE TABLE IF NOT EXISTS contract_items_v2 (
  id TEXT PRIMARY KEY,
  contract_id TEXT NOT NULL REFERENCES contracts_v2(id) ON DELETE CASCADE,
  source_budget_item_id TEXT NOT NULL,
  item_no TEXT,
  name TEXT NOT NULL,
  unit TEXT,
  quantity TEXT NOT NULL,
  unit_price TEXT NOT NULL,
  amount TEXT NOT NULL,
  sort_order INTEGER NOT NULL,
  created_at TEXT NOT NULL,
  UNIQUE(contract_id, source_budget_item_id)
);
CREATE INDEX IF NOT EXISTS idx_contract_items_v2_contract ON contract_items_v2(contract_id);
CREATE INDEX IF NOT EXISTS idx_contract_items_v2_source ON contract_items_v2(source_budget_item_id);

INSERT OR IGNORE INTO schema_migrations(version) VALUES ('0038_contract_core');
