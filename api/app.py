"""Canonical PCCES Web API entrypoint.

This module wraps the legacy monolithic application while Phase 0 routes are
being extracted. Deployment must import ``app`` from here so every API request
passes the same authentication and capability infrastructure.
"""

from __future__ import annotations

from flask import jsonify, request

from api.authorization import AuthorizationService, build_authorization_blueprint
from api.index import app, decode_token, engine

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


@app.before_request
def enforce_canonical_authentication():
    """Reject guest fallback for every non-public API route.

    OPTIONS remains public so browser CORS preflight requests are not blocked.
    Capability-specific authorization remains the responsibility of route
    guards; this hook guarantees that a caller cannot become user 1 merely by
    omitting or corrupting a token.
    """

    if request.method == "OPTIONS":
        return None
    if not request.path.startswith("/api/"):
        return None
    if request.path in _PUBLIC_ENDPOINTS:
        return None
    if resolve_user_id() is None:
        return jsonify({
            "code": "UNAUTHORIZED",
            "detail": "authentication required",
            "feature_id": "P0-S3",
        }), 401
    return None


authorization_service = AuthorizationService(engine)
authorization_service.create_schema()

if "authorization" not in app.blueprints:
    app.register_blueprint(
        build_authorization_blueprint(authorization_service, resolve_user_id)
    )

__all__ = ["app", "authorization_service", "resolve_user_id"]
