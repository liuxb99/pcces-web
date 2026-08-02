"""Canonical PCCES Web API entrypoint."""

from __future__ import annotations

from flask import jsonify, request
from sqlalchemy import select

from api.authorization import AuthorizationService, build_authorization_blueprint
from api.budget_calculation_trace import BudgetTraceService, build_budget_trace_blueprint
from api.budget_decimal import BudgetDecimalService, build_budget_decimal_blueprint
from api.index import SessionLocal, app, decode_token, engine
from api.legacy_budget_decimal_bridge import install_legacy_budget_bridge
from api.migrations import run_migrations
from api.models import Base, User
from api.persistence_contract import PersistenceService
from api.recovery import RecoveryService, build_recovery_blueprint
from api.resource_decimal import ResourceDecimalService, build_resource_decimal_blueprint
from api.route_policy import action_for_request, initialize_authorization
from api.work_context import WorkContextService, build_work_context_blueprint

_PUBLIC_ENDPOINTS = {"/api/health", "/api/auth/login", "/api/auth/register"}


def resolve_user_id() -> int | None:
    auth = request.headers.get("Authorization", "")
    if not auth.startswith("Bearer "):
        return None
    payload = decode_token(auth[7:])
    if not payload:
        return None
    try:
        return int(payload["sub"])
    except (KeyError, TypeError, ValueError):
        return None


Base.metadata.create_all(engine)
run_migrations(engine)
authorization_service = AuthorizationService(engine)
with engine.connect() as connection:
    existing_user_ids = [row[0] for row in connection.execute(select(User.id))]
initialize_authorization(authorization_service, existing_user_ids)
work_context_service = WorkContextService(engine)
recovery_service = RecoveryService(engine, work_context_service)
persistence_service = PersistenceService(engine)
budget_decimal_service = BudgetDecimalService(engine)
budget_decimal_service.create_schema()
resource_decimal_service = ResourceDecimalService(engine)
resource_decimal_service.create_schema()
budget_trace_service = BudgetTraceService(engine)

# Preserve the existing public URLs and React client contract while routing all
# legacy budget CRUD/recalculation through the exact Decimal shadow tables.
install_legacy_budget_bridge(app, engine, SessionLocal)


@app.before_request
def enforce_canonical_authentication_and_capability():
    if request.method == "OPTIONS":
        return None
    if not request.path.startswith("/api/") or request.path in _PUBLIC_ENDPOINTS:
        return None
    user_id = resolve_user_id()
    if user_id is None:
        return jsonify({"code":"UNAUTHORIZED","detail":"authentication required","feature_id":"P0-S3"}), 401
    action_code = action_for_request(request.path, request.method)
    if action_code is None:
        return None
    decision = authorization_service.decide(user_id, action_code)
    if not decision.allowed:
        return jsonify({"code":"FORBIDDEN","action_code":action_code,"reason":decision.reason,"feature_id":"P0-S3"}), 403
    return None


if "authorization" not in app.blueprints:
    app.register_blueprint(build_authorization_blueprint(authorization_service, resolve_user_id))
if "work_context" not in app.blueprints:
    app.register_blueprint(build_work_context_blueprint(work_context_service, resolve_user_id))
if "recovery" not in app.blueprints:
    app.register_blueprint(build_recovery_blueprint(recovery_service, resolve_user_id))
if "budget_decimal" not in app.blueprints:
    app.register_blueprint(build_budget_decimal_blueprint(budget_decimal_service, resolve_user_id))
if "resource_decimal" not in app.blueprints:
    app.register_blueprint(build_resource_decimal_blueprint(resource_decimal_service, resolve_user_id))
if "budget_trace" not in app.blueprints:
    app.register_blueprint(build_budget_trace_blueprint(budget_trace_service, resolve_user_id))

__all__ = [
    "app", "authorization_service", "work_context_service", "recovery_service",
    "persistence_service", "budget_decimal_service", "resource_decimal_service",
    "budget_trace_service", "resolve_user_id"
]
