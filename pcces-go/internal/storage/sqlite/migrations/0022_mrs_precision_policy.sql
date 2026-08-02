PRAGMA foreign_keys = ON;

CREATE TABLE IF NOT EXISTS mrs_precision_policies (
    project_code TEXT PRIMARY KEY,
    main_quantity_scale INTEGER NOT NULL,
    main_price_scale INTEGER NOT NULL,
    main_amount_scale INTEGER NOT NULL,
    analysis_quantity_scale INTEGER NOT NULL,
    analysis_price_scale INTEGER NOT NULL,
    analysis_amount_scale INTEGER NOT NULL,
    row_version INTEGER NOT NULL DEFAULT 1,
    updated_by TEXT NOT NULL,
    updated_at TEXT NOT NULL
);

INSERT OR IGNORE INTO schema_migrations(version) VALUES ('0022_mrs_precision_policy');
