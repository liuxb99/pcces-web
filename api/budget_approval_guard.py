"""Enforce approval state and item locks on legacy budget endpoints."""
from __future__ import annotations

from flask import jsonify

from api.models import Project


def install_budget_approval_guard(app, service, session_factory) -> None:
    one_arg = ("create_budget_item", "recalc_budget")
    two_arg = ("update_budget_item", "delete_budget_item", "move_budget_item")

    def project_code(project_id):
        db = session_factory()
        try:
            row = db.query(Project).filter(Project.id == int(project_id)).first()
            return row.code if row else None
        finally:
            db.close()

    for endpoint in one_arg:
        if endpoint not in app.view_functions:
            continue
        original = app.view_functions[endpoint]
        def guarded(project_id, *args, _original=original, **kwargs):
            code = project_code(project_id)
            try:
                if code: service.assert_writable(code)
            except PermissionError as exc:
                return jsonify({"code":"LOCKED","detail":str(exc),"project_code":code}),423
            return _original(project_id, *args, **kwargs)
        app.view_functions[endpoint] = guarded

    for endpoint in two_arg:
        if endpoint not in app.view_functions:
            continue
        original = app.view_functions[endpoint]
        def guarded(project_id, item_id, *args, _original=original, **kwargs):
            code = project_code(project_id)
            try:
                if code: service.assert_writable(code, f"legacy-{item_id}")
            except PermissionError as exc:
                return jsonify({"code":"LOCKED","detail":str(exc),"project_code":code,
                                "item_id":f"legacy-{item_id}"}),423
            return _original(project_id, item_id, *args, **kwargs)
        app.view_functions[endpoint] = guarded
