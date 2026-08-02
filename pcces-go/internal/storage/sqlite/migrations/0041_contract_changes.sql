CREATE TABLE IF NOT EXISTS contract_change_orders (
    id TEXT PRIMARY KEY,
    contract_id TEXT NOT NULL,
    change_no TEXT NOT NULL,
    reason TEXT NOT NULL,
    status TEXT NOT NULL,
    before_amount TEXT NOT NULL,
    delta_amount TEXT NOT NULL,
    after_amount TEXT NOT NULL,
    before_snapshot_json TEXT NOT NULL,
    after_snapshot_json TEXT NOT NULL,
    created_by TEXT NOT NULL,
    created_at TEXT NOT NULL,
    row_version INTEGER NOT NULL DEFAULT 1,
    UNIQUE(contract_id, change_no)
);
CREATE INDEX IF NOT EXISTS idx_contract_change_orders_contract ON contract_change_orders(contract_id);

CREATE TABLE IF NOT EXISTS contract_change_items (
    id TEXT PRIMARY KEY,
    change_order_id TEXT NOT NULL,
    action TEXT NOT NULL,
    contract_item_id TEXT,
    source_budget_item_id TEXT,
    item_no TEXT,
    name TEXT NOT NULL,
    unit TEXT,
    quantity_delta TEXT NOT NULL,
    unit_price TEXT NOT NULL,
    amount_delta TEXT NOT NULL,
    sort_order INTEGER NOT NULL
);
CREATE INDEX IF NOT EXISTS idx_contract_change_items_order ON contract_change_items(change_order_id, sort_order);
