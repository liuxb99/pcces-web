CREATE TABLE IF NOT EXISTS budget_approval_states (
  project_code TEXT PRIMARY KEY,
  status TEXT NOT NULL,
  submitted_by TEXT,
  reviewed_by TEXT,
  comment TEXT,
  row_version INTEGER NOT NULL DEFAULT 0,
  updated_at TEXT NOT NULL
);
CREATE TABLE IF NOT EXISTS budget_item_locks (
  project_code TEXT NOT NULL,
  item_id TEXT NOT NULL,
  locked INTEGER NOT NULL,
  reason TEXT,
  locked_by TEXT,
  updated_at TEXT NOT NULL,
  PRIMARY KEY(project_code,item_id)
);
CREATE TABLE IF NOT EXISTS budget_workflow_audit (
  id TEXT PRIMARY KEY,
  project_code TEXT NOT NULL,
  item_id TEXT,
  event_type TEXT NOT NULL,
  actor_id TEXT NOT NULL,
  from_status TEXT,
  to_status TEXT,
  payload_json TEXT NOT NULL,
  created_at TEXT NOT NULL
);
CREATE INDEX IF NOT EXISTS idx_budget_workflow_audit_project ON budget_workflow_audit(project_code,created_at);
