CREATE TABLE IF NOT EXISTS resource_budget_links (
    id TEXT PRIMARY KEY,
    project_code TEXT NOT NULL,
    resource_id TEXT NOT NULL,
    budget_item_id TEXT NOT NULL,
    created_at TEXT NOT NULL,
    UNIQUE(project_code, resource_id, budget_item_id),
    FOREIGN KEY(resource_id) REFERENCES resources_decimal(id) ON DELETE CASCADE,
    FOREIGN KEY(budget_item_id) REFERENCES budget_items_decimal(id) ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS idx_resource_budget_links_resource
    ON resource_budget_links(resource_id);
CREATE INDEX IF NOT EXISTS idx_resource_budget_links_project
    ON resource_budget_links(project_code);

CREATE TABLE IF NOT EXISTS resource_price_lineage (
    id TEXT PRIMARY KEY,
    project_code TEXT NOT NULL,
    resource_id TEXT NOT NULL,
    budget_item_id TEXT NOT NULL,
    old_unit_price TEXT NOT NULL,
    new_unit_price TEXT NOT NULL,
    old_amount TEXT NOT NULL,
    new_amount TEXT NOT NULL,
    trigger TEXT NOT NULL,
    trace_json TEXT NOT NULL,
    created_at TEXT NOT NULL
);

CREATE INDEX IF NOT EXISTS idx_resource_price_lineage_project
    ON resource_price_lineage(project_code, created_at DESC);
CREATE INDEX IF NOT EXISTS idx_resource_price_lineage_resource
    ON resource_price_lineage(resource_id, created_at DESC);
