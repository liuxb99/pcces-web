"""Submission/approval gate backed by the canonical budget self-check."""
from __future__ import annotations

from flask import jsonify


def install_budget_submission_gate(app, validation_service, resolve_user_id) -> None:
    endpoint = "budget_approval.transition"
    if endpoint not in app.view_functions:
        raise RuntimeError("budget approval transition endpoint is not registered")
    original = app.view_functions[endpoint]

    def guarded(project_code: str, command: str):
        normalized = command.upper()
        if normalized in {"SUBMIT", "APPROVE"}:
            actor = resolve_user_id()
            if actor is None:
                return jsonify({"code": "UNAUTHORIZED"}), 401
            result = validation_service.check(project_code, str(actor), True)
            if not result["passed"]:
                return jsonify({
                    "code": "SELF_CHECK_FAILED",
                    "detail": "budget self-check contains blocking issues",
                    "self_check": result,
                }), 422
        return original(project_code, command)

    app.view_functions[endpoint] = guarded
