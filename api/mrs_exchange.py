"""MRS JSON/CSV import through the canonical catalog write path."""
from __future__ import annotations

import csv
import io
import json
from datetime import datetime, timezone
from uuid import uuid4

from flask import Blueprint, jsonify, request


class MRSExchangeService:
    def __init__(self, catalog_service):
        self.catalog = catalog_service

    def import_payload(self, payload: str, fmt: str, actor: str, overwrite: bool = False) -> dict:
        fmt = fmt.lower()
        if fmt == "json": rows = json.loads(payload)
        elif fmt == "csv": rows = list(csv.DictReader(io.StringIO(payload)))
        else: raise ValueError("format must be json or csv")
        if not isinstance(rows, list): raise ValueError("import payload must contain a list")
        imported, skipped, errors = 0, 0, []
        for index, row in enumerate(rows):
            try:
                item_id = str(row.get("id") or row.get("code") or "").strip()
                if not item_id: raise ValueError("id or code is required")
                existing = None
                try: existing = self.catalog.get_item(item_id)
                except LookupError: pass
                if existing and not overwrite:
                    skipped += 1; continue
                body = dict(row)
                body["row_version"] = existing["row_version"] if existing else 0
                body["enabled"] = str(body.get("enabled", "true")).lower() not in {"false", "0", "no"}
                body["price_scale"] = int(body.get("price_scale", 4))
                self.catalog.save_item(item_id, body, actor)
                imported += 1
            except Exception as exc:
                errors.append({"row": index + 1, "detail": str(exc)})
        run_id, now = str(uuid4()), datetime.now(timezone.utc)
        result = {"id": run_id, "operation": "IMPORT", "format": fmt.upper(), "status": "COMPLETED" if not errors else "COMPLETED_WITH_ERRORS",
                  "imported": imported, "skipped": skipped, "errors": errors, "created_at": now.isoformat()}
        with self.catalog.engine.begin() as conn:
            from api.mrs_catalog import mrs_exchange_runs
            conn.execute(mrs_exchange_runs.insert().values(id=run_id, operation="IMPORT", format=fmt.upper(), status=result["status"],
                result_json=json.dumps(result, ensure_ascii=False, sort_keys=True), created_by=actor, created_at=now))
        return result


def build_mrs_exchange_blueprint(service: MRSExchangeService, resolve_user_id):
    bp = Blueprint("mrs_exchange", __name__, url_prefix="/api/mrs")
    @bp.post("/catalog/import")
    def import_catalog():
        actor = resolve_user_id()
        if actor is None: return jsonify({"code":"UNAUTHORIZED"}),401
        body = request.get_json(silent=True) or {}
        try:
            result = service.import_payload(str(body.get("payload", "")), str(body.get("format", "json")), str(actor), bool(body.get("overwrite", False)))
            return jsonify(result), 200 if not result["errors"] else 207
        except (ValueError, json.JSONDecodeError) as exc:
            return jsonify({"code":"INVALID_ARGUMENT","detail":str(exc)}),400
    return bp
