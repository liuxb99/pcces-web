"""Phase 4 budget combine-bid sessions with explicit conflict strategies."""
from __future__ import annotations

import json
from datetime import datetime, timezone
from decimal import Decimal
from uuid import uuid4

from flask import Blueprint, jsonify, request
from sqlalchemy import Column, DateTime, Integer, MetaData, String, Table, Text, select

metadata = MetaData()
combine_bid_sessions = Table(
    "combine_bid_sessions", metadata,
    Column("id", String(100), primary_key=True),
    Column("target_project_code", String(100), nullable=False, index=True),
    Column("strategy", String(30), nullable=False),
    Column("status", String(20), nullable=False),
    Column("sources_json", Text, nullable=False),
    Column("conflicts_json", Text, nullable=False),
    Column("result_json", Text, nullable=False),
    Column("created_by", String(100), nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
    Column("row_version", Integer, nullable=False, default=1),
)

STRATEGIES = {"BLOCK", "KEEP_FIRST", "KEEP_LAST", "SUM_QUANTITY", "RENAME"}


def _norm(item: dict, source_project: str, source_index: int, item_index: int) -> dict:
    code = str(item.get("code", "")).strip().upper()
    if not code:
        raise ValueError("every combine-bid item requires code")
    quantity = Decimal(str(item.get("quantity", "0")))
    unit_price = Decimal(str(item.get("unit_price", "0")))
    amount = Decimal(str(item.get("amount", quantity * unit_price)))
    return {
        "id": str(item.get("id") or f"{source_project}:{item_index+1}"),
        "code": code,
        "name": str(item.get("name", "")).strip(),
        "unit": str(item.get("unit", "")).strip(),
        "quantity": str(quantity),
        "unit_price": str(unit_price),
        "amount": str(amount),
        "source_project_code": source_project,
        "source_item_id": str(item.get("id") or f"ROW-{item_index+1}"),
        "source_index": source_index,
    }


def combine_sources(sources: list[dict], strategy: str) -> dict:
    strategy = strategy.strip().upper()
    if strategy not in STRATEGIES:
        raise ValueError("strategy must be BLOCK, KEEP_FIRST, KEEP_LAST, SUM_QUANTITY or RENAME")
    if len(sources) < 2:
        raise ValueError("at least two source budgets are required")
    merged: dict[str, dict] = {}
    ordered: list[str] = []
    conflicts: list[dict] = []
    rename_counts: dict[str, int] = {}
    for source_index, source in enumerate(sources):
        project = str(source.get("project_code", "")).strip()
        if not project:
            raise ValueError("source project_code is required")
        items = list(source.get("items") or [])
        for item_index, raw in enumerate(items):
            item = _norm(raw, project, source_index, item_index)
            code = item["code"]
            if code not in merged:
                merged[code] = item
                ordered.append(code)
                continue
            current = merged[code]
            conflict = {
                "code": code,
                "existing_source": current["source_project_code"],
                "incoming_source": project,
                "resolution": strategy,
            }
            conflicts.append(conflict)
            if strategy == "BLOCK":
                continue
            if strategy == "KEEP_FIRST":
                continue
            if strategy == "KEEP_LAST":
                merged[code] = item
                continue
            if strategy == "SUM_QUANTITY":
                if current["name"] != item["name"] or current["unit"] != item["unit"] or Decimal(current["unit_price"]) != Decimal(item["unit_price"]):
                    conflict["resolution"] = "BLOCKED_INCOMPATIBLE_SUM"
                    continue
                quantity = Decimal(current["quantity"]) + Decimal(item["quantity"])
                current["quantity"] = str(quantity)
                current["amount"] = str(quantity * Decimal(current["unit_price"]))
                current.setdefault("source_links", []).append({"project_code": project, "item_id": item["source_item_id"]})
                continue
            rename_counts[code] = rename_counts.get(code, 1) + 1
            new_code = f"{code}-{rename_counts[code]}"
            while new_code in merged:
                rename_counts[code] += 1
                new_code = f"{code}-{rename_counts[code]}"
            item["code"] = new_code
            merged[new_code] = item
            ordered.append(new_code)
            conflict["renamed_to"] = new_code
    blocked = [x for x in conflicts if x["resolution"] in {"BLOCK", "BLOCKED_INCOMPATIBLE_SUM"}]
    return {
        "status": "BLOCKED" if blocked else "READY",
        "strategy": strategy,
        "conflicts": conflicts,
        "blocking_conflicts": blocked,
        "items": [merged[code] for code in ordered],
    }


class BudgetCombineBidService:
    def __init__(self, engine):
        self.engine = engine
        metadata.create_all(engine)

    def create(self, body: dict, actor: str) -> dict:
        target = str(body.get("target_project_code", "")).strip()
        if not target:
            raise ValueError("target_project_code is required")
        sources = list(body.get("sources") or [])
        result = combine_sources(sources, str(body.get("strategy", "BLOCK")))
        session_id = str(uuid4())
        now = datetime.now(timezone.utc)
        with self.engine.begin() as conn:
            conn.execute(combine_bid_sessions.insert().values(
                id=session_id,
                target_project_code=target,
                strategy=result["strategy"],
                status=result["status"],
                sources_json=json.dumps(sources, ensure_ascii=False, sort_keys=True),
                conflicts_json=json.dumps(result["conflicts"], ensure_ascii=False, sort_keys=True),
                result_json=json.dumps(result["items"], ensure_ascii=False, sort_keys=True),
                created_by=actor,
                created_at=now,
                row_version=1,
            ))
        return self.get(session_id)

    def get(self, session_id: str) -> dict:
        with self.engine.connect() as conn:
            row = conn.execute(select(combine_bid_sessions).where(combine_bid_sessions.c.id == session_id)).mappings().first()
        if not row:
            raise LookupError("combine-bid session not found")
        item = dict(row)
        item["sources"] = json.loads(item.pop("sources_json"))
        item["conflicts"] = json.loads(item.pop("conflicts_json"))
        item["items"] = json.loads(item.pop("result_json"))
        item["created_at"] = item["created_at"].isoformat()
        item["deep_link"] = f"/app/conversions/combine-bid?session={session_id}"
        return item


def build_budget_combine_bid_blueprint(service: BudgetCombineBidService, resolve_user_id):
    bp = Blueprint("budget_combine_bid", __name__, url_prefix="/api/conversions")

    @bp.post("/combine-bid/preflight")
    def preflight():
        if resolve_user_id() is None:
            return jsonify({"code": "UNAUTHORIZED"}), 401
        body = request.get_json(silent=True) or {}
        try:
            return jsonify(combine_sources(list(body.get("sources") or []), str(body.get("strategy", "BLOCK"))))
        except (ValueError, ArithmeticError) as exc:
            return jsonify({"code": "INVALID_ARGUMENT", "detail": str(exc)}), 400

    @bp.post("/combine-bid/sessions")
    def create_session():
        actor = resolve_user_id()
        if actor is None:
            return jsonify({"code": "UNAUTHORIZED"}), 401
        try:
            return jsonify(service.create(request.get_json(silent=True) or {}, str(actor))), 201
        except (ValueError, ArithmeticError) as exc:
            return jsonify({"code": "INVALID_ARGUMENT", "detail": str(exc)}), 400

    @bp.get("/combine-bid/sessions/<session_id>")
    def get_session(session_id: str):
        try:
            return jsonify(service.get(session_id))
        except LookupError as exc:
            return jsonify({"code": "NOT_FOUND", "detail": str(exc)}), 404

    return bp
