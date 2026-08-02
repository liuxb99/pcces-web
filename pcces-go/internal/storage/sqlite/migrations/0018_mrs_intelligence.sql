PRAGMA foreign_keys = ON;

CREATE TABLE IF NOT EXISTS mrs_price_quotes (
    id TEXT PRIMARY KEY,
    catalog_item_id TEXT NOT NULL,
    vendor TEXT NOT NULL,
    quoted_price TEXT NOT NULL,
    price_scale INTEGER NOT NULL,
    source_document TEXT,
    effective_date TEXT,
    created_by TEXT NOT NULL,
    created_at TEXT NOT NULL,
    FOREIGN KEY(catalog_item_id) REFERENCES mrs_catalog_items(id) ON DELETE CASCADE
);
CREATE INDEX IF NOT EXISTS idx_mrs_price_quotes_item ON mrs_price_quotes(catalog_item_id, created_at DESC);

CREATE TABLE IF NOT EXISTS mrs_analysis_snapshots (
    id TEXT PRIMARY KEY,
    recipe_id TEXT NOT NULL,
    unit_price TEXT NOT NULL,
    snapshot_json TEXT NOT NULL,
    created_by TEXT NOT NULL,
    created_at TEXT NOT NULL,
    FOREIGN KEY(recipe_id) REFERENCES mrs_analysis_recipes(id) ON DELETE CASCADE
);
CREATE INDEX IF NOT EXISTS idx_mrs_analysis_snapshots_recipe ON mrs_analysis_snapshots(recipe_id, created_at DESC);

CREATE TABLE IF NOT EXISTS mrs_impact_runs (
    id TEXT PRIMARY KEY,
    catalog_item_id TEXT NOT NULL,
    old_price TEXT NOT NULL,
    new_price TEXT NOT NULL,
    result_json TEXT NOT NULL,
    created_by TEXT NOT NULL,
    created_at TEXT NOT NULL,
    FOREIGN KEY(catalog_item_id) REFERENCES mrs_catalog_items(id) ON DELETE CASCADE
);
CREATE INDEX IF NOT EXISTS idx_mrs_impact_runs_item ON mrs_impact_runs(catalog_item_id, created_at DESC);

INSERT OR IGNORE INTO schema_migrations(version) VALUES ('0018_mrs_intelligence');
