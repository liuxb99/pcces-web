PRAGMA foreign_keys = ON;

INSERT OR IGNORE INTO actions(code, name, module_code, function_code) VALUES
('MRS', '工料機與單價分析', 'COMMON', 'F007'),
('REPORT', '報表與資料輸出', 'COMMON', 'F006'),
('SYSTEM_ADMIN', '系統管理', 'COMMON', 'F001');

INSERT OR IGNORE INTO actor_function_codes(actor_id, function_code)
SELECT actor_id, code
FROM local_actors CROSS JOIN function_codes;

INSERT OR IGNORE INTO actor_module_entitlements(actor_id, module_code)
SELECT actor_id, code
FROM local_actors CROSS JOIN modules;

INSERT OR IGNORE INTO schema_migrations(version) VALUES ('0005_action_catalog_parity');
