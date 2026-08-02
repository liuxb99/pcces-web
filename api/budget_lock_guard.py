"""Apply project lock policy to legacy budget write endpoints."""
from __future__ import annotations

from flask import jsonify

from api.models import Project


def install_budget_lock_guard(app, version_service, session_factory) -> None:
    write_endpoints = (
        "create_budget_item", "update_budget_item", "delete_budget_item",
        "move_budget_item", "recalc_budget",
    )
    for endpoint in write_endpoints:
        if endpoint not in app.view_functions:
            continue
        original = app.view_functions[endpoint]

        def guarded(*args, _original=original, **kwargs):
            project_id = kwargs.get("project_id") or (args[0] if args else None)
            db = session_factory()
            try:
                project = db.query(Project).filter(Project.id == int(project_id)).first() if project_id is not None else None
                if project:
                    state = version_service.lock_state(project.code)
                    if state["locked"]:
                        return jsonify({"code":"LOCKED","detail":"budget project is locked","project_code":project.code,"reason":state.get("reason")}),423
            finally:
                db.close()
            return _original(*args, **kwargs)

        app.view_functions[endpoint] = guarded
