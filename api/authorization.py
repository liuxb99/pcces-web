"""Phase 0 Web authorization component.

Provides the shared Function Code / Module / Action decision model for Flask
entrypoints without coupling the policy to one monolithic application module.
"""

from dataclasses import asdict, dataclass
from functools import wraps
from typing import Callable, Iterable

from flask import Blueprint, jsonify, request
from sqlalchemy import Boolean, Column, ForeignKey, Integer, MetaData, String, Table, and_, select

metadata = MetaData()

modules = Table(
    "modules", metadata,
    Column("code", String(32), primary_key=True),
    Column("name", String(200), nullable=False),
    Column("enabled", Boolean, nullable=False, default=True),
)
function_codes = Table(
    "function_codes", metadata,
    Column("code", String(32), primary_key=True),
    Column("name", String(200), nullable=False),
    Column("enabled", Boolean, nullable=False, default=True),
)
actions = Table(
    "actions", metadata,
    Column("code", String(64), primary_key=True),
    Column("name", String(200), nullable=False),
    Column("module_code", ForeignKey("modules.code"), nullable=False),
    Column("function_code", ForeignKey("function_codes.code"), nullable=True),
)
user_module_entitlements = Table(
    "user_module_entitlements", metadata,
    Column("user_id", Integer, primary_key=True),
    Column("module_code", ForeignKey("modules.code"), primary_key=True),
    Column("enabled", Boolean, nullable=False, default=False),
)
user_function_grants = Table(
    "user_function_grants", metadata,
    Column("user_id", Integer, primary_key=True),
    Column("function_code", ForeignKey("function_codes.code"), primary_key=True),
    Column("granted", Boolean, nullable=False, default=False),
)


@dataclass(frozen=True)
class Decision:
    user_id: int
    action_code: str
    module_code: str = ""
    function_code: str = ""
    module_enabled: bool = False
    function_grant: bool = False
    allowed: bool = False
    reason: str = ""


class AuthorizationService:
    def __init__(self, engine):
        self.engine = engine

    def create_schema(self) -> None:
        metadata.create_all(self.engine)

    def seed_catalog(self, module_rows: Iterable[dict], function_rows: Iterable[dict], action_rows: Iterable[dict]) -> None:
        with self.engine.begin() as conn:
            for table, rows in ((modules, module_rows), (function_codes, function_rows), (actions, action_rows)):
                for row in rows:
                    exists = conn.execute(select(table.c.code).where(table.c.code == row["code"])).first()
                    if exists is None:
                        conn.execute(table.insert().values(**row))

    def decide(self, user_id: int, action_code: str) -> Decision:
        with self.engine.connect() as conn:
            row = conn.execute(
                select(
                    actions.c.code,
                    actions.c.module_code,
                    actions.c.function_code,
                    modules.c.enabled.label("module_catalog_enabled"),
                    user_module_entitlements.c.enabled.label("module_entitled"),
                    function_codes.c.enabled.label("function_catalog_enabled"),
                    user_function_grants.c.granted.label("function_granted"),
                )
                .join(modules, modules.c.code == actions.c.module_code)
                .outerjoin(function_codes, function_codes.c.code == actions.c.function_code)
                .outerjoin(
                    user_module_entitlements,
                    and_(
                        user_module_entitlements.c.user_id == user_id,
                        user_module_entitlements.c.module_code == actions.c.module_code,
                    ),
                )
                .outerjoin(
                    user_function_grants,
                    and_(
                        user_function_grants.c.user_id == user_id,
                        user_function_grants.c.function_code == actions.c.function_code,
                    ),
                )
                .where(actions.c.code == action_code)
            ).mappings().first()

        if row is None:
            return Decision(user_id=user_id, action_code=action_code, reason="ACTION_NOT_FOUND")
        module_enabled = bool(row["module_catalog_enabled"] and row["module_entitled"])
        function_code = row["function_code"] or ""
        function_grant = True if not function_code else bool(row["function_catalog_enabled"] and row["function_granted"])
        reason = ""
        if not row["module_catalog_enabled"]:
            reason = "MODULE_DISABLED"
        elif not row["module_entitled"]:
            reason = "MODULE_NOT_ENTITLED"
        elif function_code and not row["function_catalog_enabled"]:
            reason = "FUNCTION_DISABLED"
        elif function_code and not row["function_granted"]:
            reason = "FUNCTION_NOT_GRANTED"
        return Decision(
            user_id=user_id,
            action_code=action_code,
            module_code=row["module_code"],
            function_code=function_code,
            module_enabled=module_enabled,
            function_grant=function_grant,
            allowed=module_enabled and function_grant,
            reason=reason,
        )

    def set_module_entitlement(self, user_id: int, module_code: str, enabled: bool) -> None:
        with self.engine.begin() as conn:
            conn.execute(user_module_entitlements.delete().where(and_(
                user_module_entitlements.c.user_id == user_id,
                user_module_entitlements.c.module_code == module_code,
            )))
            conn.execute(user_module_entitlements.insert().values(user_id=user_id, module_code=module_code, enabled=enabled))

    def set_function_grant(self, user_id: int, function_code: str, granted: bool) -> None:
        with self.engine.begin() as conn:
            conn.execute(user_function_grants.delete().where(and_(
                user_function_grants.c.user_id == user_id,
                user_function_grants.c.function_code == function_code,
            )))
            conn.execute(user_function_grants.insert().values(user_id=user_id, function_code=function_code, granted=granted))


def build_authorization_blueprint(service: AuthorizationService, resolve_user_id: Callable[[], int | None]) -> Blueprint:
    blueprint = Blueprint("authorization", __name__, url_prefix="/api")

    @blueprint.get("/capabilities/<action_code>")
    def capability(action_code: str):
        user_id = resolve_user_id()
        if user_id is None:
            return jsonify({"code": "UNAUTHORIZED", "detail": "authentication required"}), 401
        return jsonify(asdict(service.decide(user_id, action_code)))

    return blueprint


def require_capability(service: AuthorizationService, action_code: str, resolve_user_id: Callable[[], int | None]):
    def decorator(handler):
        @wraps(handler)
        def guarded(*args, **kwargs):
            user_id = resolve_user_id()
            if user_id is None:
                return jsonify({"code": "UNAUTHORIZED", "detail": "authentication required"}), 401
            decision = service.decide(user_id, action_code)
            if not decision.allowed:
                return jsonify({"code": "FORBIDDEN", "reason": decision.reason, "action_code": action_code}), 403
            request.authorization_decision = decision
            return handler(*args, **kwargs)
        return guarded
    return decorator
