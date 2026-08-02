"""Canonical Web route-to-action authorization policy.

The mapping uses the same action codes as the Local Go SQLite catalog.  It is
kept outside the monolithic legacy entrypoint so Phase 0 can enforce one policy
at the canonical boundary while routes are extracted incrementally.
"""

from __future__ import annotations

from dataclasses import dataclass
from typing import Iterable

from sqlalchemy import select

from api.authorization import (
    AuthorizationService,
    actions,
    function_codes,
    modules,
    user_function_grants,
    user_module_entitlements,
)

MODULE_ROWS = (
    {"code": "BUDGET", "name": "預算編製", "enabled": True},
    {"code": "BID", "name": "投標單", "enabled": True},
    {"code": "COMMON", "name": "共用資料", "enabled": True},
    {"code": "INVOICE", "name": "契約履約", "enabled": True},
)

FUNCTION_ROWS = (
    {"code": "F001", "name": "系統維護", "enabled": True},
    {"code": "F002", "name": "基本資料庫維護", "enabled": True},
    {"code": "F003", "name": "預算書編製", "enabled": True},
    {"code": "F004", "name": "投標單填寫", "enabled": True},
    {"code": "F005", "name": "專案目錄", "enabled": True},
    {"code": "F006", "name": "系統外掛", "enabled": True},
    {"code": "F007", "name": "單價分析比對", "enabled": True},
    {"code": "F008", "name": "歷史工程比對", "enabled": True},
    {"code": "F009", "name": "契約編製", "enabled": True},
    {"code": "F010", "name": "估驗記錄", "enabled": True},
    {"code": "F011", "name": "契約變更", "enabled": True},
    {"code": "F012", "name": "結算作業", "enabled": True},
)

ACTION_ROWS = (
    {"code": "BUD", "name": "預算編製", "module_code": "BUDGET", "function_code": "F003"},
    {"code": "BID", "name": "投標單填寫", "module_code": "BID", "function_code": "F004"},
    {"code": "PROJECT_CATALOG", "name": "專案目錄", "module_code": "COMMON", "function_code": "F005"},
    {"code": "MRS", "name": "工料機與單價分析", "module_code": "COMMON", "function_code": "F007"},
    {"code": "REPORT", "name": "報表與資料輸出", "module_code": "COMMON", "function_code": "F006"},
    {"code": "SYSTEM_ADMIN", "name": "系統管理", "module_code": "COMMON", "function_code": "F001"},
    {"code": "SPLIT_CONTRACT", "name": "契約編製", "module_code": "INVOICE", "function_code": "F009"},
    {"code": "INVOICE", "name": "估驗記錄", "module_code": "INVOICE", "function_code": "F010"},
    {"code": "BUDGET_CHANGE", "name": "契約變更", "module_code": "INVOICE", "function_code": "F011"},
    {"code": "SUB_CLOSE", "name": "結算作業", "module_code": "INVOICE", "function_code": "F012"},
    {"code": "SUB_FINAL", "name": "驗收作業", "module_code": "INVOICE", "function_code": None},
)


@dataclass(frozen=True)
class RouteRule:
    prefix: str
    action_code: str
    methods: frozenset[str] | None = None

    def matches(self, path: str, method: str) -> bool:
        return path.startswith(self.prefix) and (self.methods is None or method in self.methods)


ROUTE_RULES: tuple[RouteRule, ...] = (
    RouteRule("/api/admin", "SYSTEM_ADMIN"),
    RouteRule("/api/system", "SYSTEM_ADMIN"),
    RouteRule("/api/settings", "SYSTEM_ADMIN"),
    RouteRule("/api/projects", "PROJECT_CATALOG"),
    RouteRule("/api/budget", "BUD"),
    RouteRule("/api/budgets", "BUD"),
    RouteRule("/api/bid", "BID"),
    RouteRule("/api/mrs", "MRS"),
    RouteRule("/api/resources", "MRS"),
    RouteRule("/api/reports", "REPORT"),
    RouteRule("/api/contracts", "SPLIT_CONTRACT"),
    RouteRule("/api/invoices", "INVOICE"),
    RouteRule("/api/budget-changes", "BUDGET_CHANGE"),
    RouteRule("/api/settlements", "SUB_CLOSE"),
    RouteRule("/api/final-acceptance", "SUB_FINAL"),
)


def action_for_request(path: str, method: str) -> str | None:
    normalized_method = method.upper()
    for rule in ROUTE_RULES:
        if rule.matches(path, normalized_method):
            return rule.action_code
    return None


def initialize_authorization(service: AuthorizationService, user_ids: Iterable[int]) -> None:
    """Create catalog and preserve existing authenticated-user behavior.

    Existing users receive explicit grants once.  They are no longer implicit:
    administrators can revoke either module entitlements or function grants and
    the canonical route guard will enforce the decision immediately.
    """

    service.create_schema()
    service.seed_catalog(MODULE_ROWS, FUNCTION_ROWS, ACTION_ROWS)
    with service.engine.begin() as conn:
        module_codes = [row[0] for row in conn.execute(select(modules.c.code))]
        function_code_values = [row[0] for row in conn.execute(select(function_codes.c.code))]
        for user_id in user_ids:
            for module_code in module_codes:
                exists = conn.execute(
                    select(user_module_entitlements.c.user_id).where(
                        user_module_entitlements.c.user_id == user_id,
                        user_module_entitlements.c.module_code == module_code,
                    )
                ).first()
                if exists is None:
                    conn.execute(user_module_entitlements.insert().values(
                        user_id=user_id, module_code=module_code, enabled=True,
                    ))
            for function_code in function_code_values:
                exists = conn.execute(
                    select(user_function_grants.c.user_id).where(
                        user_function_grants.c.user_id == user_id,
                        user_function_grants.c.function_code == function_code,
                    )
                ).first()
                if exists is None:
                    conn.execute(user_function_grants.insert().values(
                        user_id=user_id, function_code=function_code, granted=True,
                    ))
