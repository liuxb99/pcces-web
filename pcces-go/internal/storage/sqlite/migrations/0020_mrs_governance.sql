CREATE TABLE IF NOT EXISTS mrs_catalog_releases (
  id TEXT PRIMARY KEY, label TEXT NOT NULL, status TEXT NOT NULL,
  snapshot_json TEXT NOT NULL, created_by TEXT NOT NULL, reviewed_by TEXT,
  review_comment TEXT, created_at TEXT NOT NULL, updated_at TEXT NOT NULL,
  row_version INTEGER NOT NULL DEFAULT 1
);
CREATE TABLE IF NOT EXISTS mrs_item_validity (
  catalog_item_id TEXT PRIMARY KEY, valid_from TEXT, valid_to TEXT,
  status TEXT NOT NULL, row_version INTEGER NOT NULL DEFAULT 1,
  updated_by TEXT NOT NULL, updated_at TEXT NOT NULL
);
CREATE TABLE IF NOT EXISTS mrs_recipe_freezes (
  recipe_id TEXT PRIMARY KEY, version_id TEXT NOT NULL, frozen INTEGER NOT NULL,
  reason TEXT, row_version INTEGER NOT NULL DEFAULT 1,
  updated_by TEXT NOT NULL, updated_at TEXT NOT NULL
);
CREATE TABLE IF NOT EXISTS mrs_governance_audit (
  id TEXT PRIMARY KEY, event_type TEXT NOT NULL, resource_type TEXT NOT NULL,
  resource_id TEXT NOT NULL, actor_id TEXT NOT NULL,
  payload_json TEXT NOT NULL, created_at TEXT NOT NULL
);
CREATE INDEX IF NOT EXISTS idx_mrs_releases_status ON mrs_catalog_releases(status, created_at);
CREATE INDEX IF NOT EXISTS idx_mrs_audit_resource ON mrs_governance_audit(resource_type, resource_id, created_at);
