"""Canonical PCCES Web API entrypoint.

This module wraps the legacy monolithic application while Phase 0 routes are
being extracted. Deployment must import ``app`` from here so every API request
passes the same authentication and capability infrastructure.
"""

from __future__ import annotations

from flask import jsonify, request
from sqlalchemy import select

from api.authorization import AuthorizationService, build_authorization_blueprint
from api.index import app, decode_token, engine
from api.models import Base, User
from api.route_policy import action_for_request, initialize_authorization

_PUBLIC_ENDPOINTS = {
    "/api/health",
    "/api/auth/login",
    "/api/auth/register",
}


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


# The legacy module historically initialized tables only in selected runtime
# paths.  The canonical entrypoint must be import-safe for WSGI and tests.
Base.metadata.create_all(engine)
authorization_service = AuthorizationService(engine)
with engine.connect() as connection:
    existing_user_ids = [row[0] for row in connection.execute(select(User.id))]
initialize_authorization(authorization_service, existing_user_ids)


@app.before_request
def enforce_canonical_authentication_and_capability():
    """Enforce authentication and the shared route-to-action catalog.

    OPTIONS remains public so browser CORS preflight requests are not blocked.
    Every mapped business route is checked at this canonical boundary, which
    prevents callers from bypassing Function Code guards through a direct URL.
    """

    if request.method == "OPTIONS":
        return None
    if not request.path.startswith("/api/"):
        return None
    if request.path in _PUBLIC_ENDPOINTS:
        return None

    user_id = resolve_user_id()
    if user_id is None:
        return jsonify({
            "code": "UNAUTHORIZED",
            "detail": "authentication required",
            "feature_id": "P0-S3",
        }), 401

    action_code = action_for_request(request.path, request.method)
    if action_code is None:
        return None

    decision = authorization_service.decide(user_id, action_code)
    if not decision.allowed:
        return jsonify({
            "code": "FORBIDDEN",
            "action_code": action_code,
            "reason": decision.reason,
            "feature_id": "P0-S3",
        }), 403
    return None


if "authorization" not in app.blueprints:
    app.register_blueprint(
        build_authorization_blueprint(authorization_service, resolve_user_id)
    )

__all__ = ["app", "authorization_service", "resolve_user_id"]
