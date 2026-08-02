PRAGMA foreign_keys = ON;

CREATE TABLE IF NOT EXISTS mrs_catalog_items (
  id TEXT PRIMARY KEY,
  code TEXT NOT NULL UNIQUE,
  name TEXT NOT NULL,
  category TEXT NOT NULL,
  unit TEXT,
  current_price TEXT NOT NULL,
  price_scale INTEGER NOT NULL,
  source TEXT,
  enabled INTEGER NOT NULL DEFAULT 1,
  row_version INTEGER NOT NULL DEFAULT 1,
  created_at TEXT NOT NULL,
  updated_at TEXT NOT NULL
);
CREATE INDEX IF NOT EXISTS idx_mrs_catalog_category ON mrs_catalog_items(category);

CREATE TABLE IF NOT EXISTS mrs_price_history (
  id TEXT PRIMARY KEY,
  catalog_item_id TEXT NOT NULL REFERENCES mrs_catalog_items(id) ON DELETE CASCADE,
  old_price TEXT,
  new_price TEXT NOT NULL,
  source TEXT,
  effective_date TEXT,
  created_by TEXT NOT NULL,
  created_at TEXT NOT NULL
);
CREATE INDEX IF NOT EXISTS idx_mrs_history_item ON mrs_price_history(catalog_item_id,created_at);

CREATE TABLE IF NOT EXISTS mrs_bookmarks (
  actor_id TEXT NOT NULL,
  catalog_item_id TEXT NOT NULL REFERENCES mrs_catalog_items(id) ON DELETE CASCADE,
  created_at TEXT NOT NULL,
  PRIMARY KEY(actor_id,catalog_item_id)
);

CREATE TABLE IF NOT EXISTS mrs_analysis_recipes (
  id TEXT PRIMARY KEY,
  code TEXT NOT NULL UNIQUE,
  name TEXT NOT NULL,
  unit TEXT,
  price_scale INTEGER NOT NULL,
  row_version INTEGER NOT NULL DEFAULT 1,
  created_at TEXT NOT NULL,
  updated_at TEXT NOT NULL
);
CREATE TABLE IF NOT EXISTS mrs_analysis_components (
  id TEXT PRIMARY KEY,
  recipe_id TEXT NOT NULL REFERENCES mrs_analysis_recipes(id) ON DELETE CASCADE,
  catalog_item_id TEXT NOT NULL REFERENCES mrs_catalog_items(id),
  quantity TEXT NOT NULL,
  quantity_scale INTEGER NOT NULL,
  sequence INTEGER NOT NULL
);

CREATE TABLE IF NOT EXISTS mrs_exchange_runs (
  id TEXT PRIMARY KEY,
  operation TEXT NOT NULL,
  format TEXT NOT NULL,
  status TEXT NOT NULL,
  result_json TEXT NOT NULL,
  created_by TEXT NOT NULL,
  created_at TEXT NOT NULL
);

INSERT OR IGNORE INTO schema_migrations(version) VALUES ('0017_mrs_catalog');
