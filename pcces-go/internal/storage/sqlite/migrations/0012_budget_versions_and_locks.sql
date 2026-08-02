CREATE TABLE IF NOT EXISTS budget_versions (
  id TEXT PRIMARY KEY,
  project_code TEXT NOT NULL,
  label TEXT NOT NULL,
  status TEXT NOT NULL,
  snapshot_json TEXT NOT NULL,
  created_by TEXT NOT NULL,
  created_at TEXT NOT NULL
);
CREATE INDEX IF NOT EXISTS idx_budget_versions_project ON budget_versions(project_code, created_at DESC);

CREATE TABLE IF NOT EXISTS budget_project_locks (
  project_code TEXT PRIMARY KEY,
  locked INTEGER NOT NULL DEFAULT 0,
  reason TEXT,
  locked_by TEXT,
  updated_at TEXT NOT NULL
);
