CREATE TABLE IF NOT EXISTS budget_project_modes (
  project_code TEXT PRIMARY KEY,
  mode TEXT NOT NULL CHECK(mode IN ('BUD','BID')),
  row_version INTEGER NOT NULL DEFAULT 1,
  updated_by TEXT NOT NULL,
  updated_at TEXT NOT NULL
);
CREATE TABLE IF NOT EXISTS budget_item_semantics (
  item_id TEXT PRIMARY KEY,
  project_code TEXT NOT NULL,
  item_class TEXT NOT NULL CHECK(item_class IN ('A','B','C')),
  row_version INTEGER NOT NULL DEFAULT 1,
  updated_by TEXT NOT NULL,
  updated_at TEXT NOT NULL
);
CREATE INDEX IF NOT EXISTS idx_budget_item_semantics_project ON budget_item_semantics(project_code);
CREATE TABLE IF NOT EXISTS budget_cross_project_refs (
  id TEXT PRIMARY KEY,
  source_project_code TEXT NOT NULL,
  source_item_id TEXT NOT NULL,
  target_project_code TEXT NOT NULL,
  target_item_id TEXT NOT NULL,
  enabled INTEGER NOT NULL DEFAULT 1,
  created_by TEXT NOT NULL,
  created_at TEXT NOT NULL,
  UNIQUE(source_item_id,target_item_id)
);
CREATE TABLE IF NOT EXISTS budget_self_check_runs (
  id TEXT PRIMARY KEY,
  project_code TEXT NOT NULL,
  mode TEXT NOT NULL,
  blocking INTEGER NOT NULL,
  result_json TEXT NOT NULL,
  created_by TEXT NOT NULL,
  created_at TEXT NOT NULL
);
CREATE INDEX IF NOT EXISTS idx_budget_self_check_project ON budget_self_check_runs(project_code,created_at DESC);
