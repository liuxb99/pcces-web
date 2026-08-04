"""Canonical Web route-to-action authorization policy."""

from __future__ import annotations

from dataclasses import dataclass
from typing import Iterable

from sqlalchemy import event, inspect, select

from api.authorization import (
    AuthorizationService,
    actions,
    function_codes,
    modules,
    user_function_grants,
    user_module_entitlements,
)
from api.models import User

MODULE_ROWS = (
    {"code": "BUDGET", "name": "預算編製", "enabled": True},
    {"code": "BID", "name": "投標單", "enabled": True},
    {"code": "COMMON", "name": "共用資料", "enabled": True},
    {"code": "INVOICE", "name": "契約履約", "enabled": True},
)
FUNCTION_ROWS = tuple(
    {"code": f"F{i:03d}", "name": name, "enabled": True}
    for i, name in enumerate((
        "系統維護", "基本資料庫維護", "預算書編製", "投標單填寫", "專案目錄", "系統外掛",
        "單價分析比對", "歷史工程比對", "契約編製", "估驗記錄", "契約變更", "結算作業",
    ), start=1)
)
ACTION_ROWS = (
    {"code":"BUD","name":"預算編製","module_code":"BUDGET","function_code":"F003"},
    {"code":"BID","name":"投標單填寫","module_code":"BID","function_code":"F004"},
    {"code":"PROJECT_CATALOG","name":"專案目錄","module_code":"COMMON","function_code":"F005"},
    {"code":"MRS","name":"工料機與單價分析","module_code":"COMMON","function_code":"F007"},
    {"code":"REPORT","name":"報表與資料輸出","module_code":"COMMON","function_code":"F006"},
    {"code":"SYSTEM_ADMIN","name":"系統管理","module_code":"COMMON","function_code":"F001"},
    {"code":"SPLIT_CONTRACT","name":"契約編製","module_code":"INVOICE","function_code":"F009"},
    {"code":"INVOICE","name":"估驗記錄","module_code":"INVOICE","function_code":"F010"},
    {"code":"BUDGET_CHANGE","name":"契約變更","module_code":"INVOICE","function_code":"F011"},
    {"code":"SUB_CLOSE","name":"結算作業","module_code":"INVOICE","function_code":"F012"},
    {"code":"SUB_FINAL","name":"驗收作業","module_code":"INVOICE","function_code":None},
)

@dataclass(frozen=True)
class RouteRule:
    prefix: str
    action_code: str
    methods: frozenset[str] | None = None
    def matches(self, path: str, method: str) -> bool:
        return path.startswith(self.prefix) and (self.methods is None or method in self.methods)

ROUTE_RULES: tuple[RouteRule, ...] = (
    RouteRule("/api/admin", "SYSTEM_ADMIN"), RouteRule("/api/system", "SYSTEM_ADMIN"),
    RouteRule("/api/settings", "SYSTEM_ADMIN"), RouteRule("/api/projects", "PROJECT_CATALOG"),
    RouteRule("/api/budget-changes", "BUDGET_CHANGE"), RouteRule("/api/decimal-budget", "BUD"),
    RouteRule("/api/budget", "BUD"),
    RouteRule("/api/budgets", "BUD"), RouteRule("/api/bid", "BID"),
    RouteRule("/api/dependency-graph", "MRS"),
    RouteRule("/api/decimal-resources", "MRS"), RouteRule("/api/mrs", "MRS"),
    RouteRule("/api/resources", "MRS"), RouteRule("/api/reports", "REPORT"),
    RouteRule("/api/contracts", "SPLIT_CONTRACT"), RouteRule("/api/invoices", "INVOICE"),
    RouteRule("/api/budget-changes", "BUDGET_CHANGE"), RouteRule("/api/settlements", "SUB_CLOSE"),
    RouteRule("/api/final-acceptance", "SUB_FINAL"),
)

def action_for_request(path: str, method: str) -> str | None:
    for rule in ROUTE_RULES:
        if rule.matches(path, method.upper()): return rule.action_code
    return None

def initialize_authorization(service: AuthorizationService, user_ids: Iterable[int]) -> None:
    service.create_schema()
    service.seed_catalog(MODULE_ROWS, FUNCTION_ROWS, ACTION_ROWS)
    with service.engine.begin() as conn:
        module_codes = [row[0] for row in conn.execute(select(modules.c.code))]
        function_code_values = [row[0] for row in conn.execute(select(function_codes.c.code))]
        for user_id in user_ids:
            for module_code in module_codes:
                exists = conn.execute(select(user_module_entitlements.c.user_id).where(
                    user_module_entitlements.c.user_id == user_id,
                    user_module_entitlements.c.module_code == module_code,
                )).first()
                if exists is None:
                    conn.execute(user_module_entitlements.insert().values(user_id=user_id,module_code=module_code,enabled=True))
            for function_code in function_code_values:
                exists = conn.execute(select(user_function_grants.c.user_id).where(
                    user_function_grants.c.user_id == user_id,
                    user_function_grants.c.function_code == function_code,
                )).first()
                if exists is None:
                    conn.execute(user_function_grants.insert().values(user_id=user_id,function_code=function_code,granted=True))


@event.listens_for(User, "after_insert")
def provision_new_user_authorization(_mapper, connection, target: User) -> None:
    """Grant default capabilities in the same transaction as registration."""
    schema = inspect(connection)
    required = ("modules", "function_codes", "user_module_entitlements", "user_function_grants")
    if not all(schema.has_table(name) for name in required):
        return
    module_codes = [row[0] for row in connection.execute(select(modules.c.code))]
    function_code_values = [row[0] for row in connection.execute(select(function_codes.c.code))]
    for module_code in module_codes:
        connection.execute(user_module_entitlements.insert().values(
            user_id=target.id, module_code=module_code, enabled=True,
        ))
    for function_code in function_code_values:
        connection.execute(user_function_grants.insert().values(
            user_id=target.id, function_code=function_code, granted=True,
        ))
