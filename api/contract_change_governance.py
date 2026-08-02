"""Governed contract change workflow: draft, review, approval and atomic apply."""
from __future__ import annotations

import json
from datetime import datetime, timezone
from decimal import Decimal, InvalidOperation
from uuid import uuid4

from flask import Blueprint, jsonify, request
from sqlalchemy import Column, DateTime, Integer, MetaData, Numeric, String, Table, Text, and_, delete, select, update

from api.contract_core import contracts_v2, contract_items_v2

metadata = MetaData()
contract_change_cases = Table(
    "contract_change_cases", metadata,
    Column("id", String(100), primary_key=True),
    Column("contract_id", String(100), nullable=False, index=True),
    Column("change_no", String(100), nullable=False),
    Column("reason", Text, nullable=False),
    Column("responsibility", String(100)),
    Column("effective_date", String(30)),
    Column("status", String(30), nullable=False),
    Column("before_amount", Numeric(28, 8), nullable=False),
    Column("delta_amount", Numeric(28, 8), nullable=False),
    Column("after_amount", Numeric(28, 8), nullable=False),
    Column("before_snapshot_json", Text, nullable=False),
    Column("after_snapshot_json", Text, nullable=False),
    Column("created_by", String(100), nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
    Column("approved_by", String(100)),
    Column("approved_at", DateTime(timezone=True)),
    Column("applied_by", String(100)),
    Column("applied_at", DateTime(timezone=True)),
    Column("row_version", Integer, nullable=False, default=1),
)
contract_change_lines = Table(
    "contract_change_lines", metadata,
    Column("id", String(100), primary_key=True),
    Column("case_id", String(100), nullable=False, index=True),
    Column("action", String(30), nullable=False),
    Column("contract_item_id", String(100)),
    Column("source_budget_item_id", String(100)),
    Column("item_no", String(100)), Column("name", String(500)), Column("unit", String(100)),
    Column("quantity_delta", Numeric(28, 8), nullable=False),
    Column("unit_price", Numeric(28, 8), nullable=False),
    Column("amount_delta", Numeric(28, 8), nullable=False),
    Column("sort_order", Integer, nullable=False),
)
TRANSITIONS = {"DRAFT": {"SUBMITTED"}, "SUBMITTED": {"DRAFT", "APPROVED"}, "APPROVED": {"APPLIED"}, "APPLIED": set()}


def _d(value, field):
    try: return Decimal(str(value))
    except (InvalidOperation, ValueError, TypeError) as exc: raise ValueError(f"{field} must be decimal") from exc


def _item_dict(row):
    return {k: (str(v) if v is not None else None) for k, v in row.items()}


class ContractChangeGovernanceService:
    def __init__(self, engine): self.engine = engine; metadata.create_all(engine)

    def create(self, contract_id, body, actor):
        no, reason = str(body.get("change_no", "")).strip(), str(body.get("reason", "")).strip()
        lines = list(body.get("items") or [])
        if not no or not reason or not lines: raise ValueError("change_no, reason and items are required")
        with self.engine.begin() as conn:
            contract = conn.execute(select(contracts_v2).where(contracts_v2.c.id == contract_id)).mappings().first()
            if not contract: raise LookupError("contract not found")
            if str(contract["status"]).upper() not in {"APPROVED", "LOCKED"}: raise PermissionError("formal change requires approved or locked contract")
            if conn.execute(select(contract_change_cases.c.id).where(and_(contract_change_cases.c.contract_id == contract_id, contract_change_cases.c.change_no == no))).first(): raise RuntimeError("change_no already exists")
            current = conn.execute(select(contract_items_v2).where(contract_items_v2.c.contract_id == contract_id).order_by(contract_items_v2.c.sort_order)).mappings().all()
            by_id = {r["id"]: dict(r) for r in current}; after = [dict(r) for r in current]; delta = Decimal("0")
            normalized = []
            for index, raw in enumerate(lines, 1):
                action = str(raw.get("action", "")).upper(); item_id = str(raw.get("contract_item_id", "")).strip()
                if action not in {"ADD", "INCREASE", "DECREASE", "DELETE", "REPLACE"}: raise ValueError("invalid change action")
                if action != "ADD" and item_id not in by_id: raise ValueError("contract_item_id not found")
                qty = _d(raw.get("quantity_delta", 0), "quantity_delta"); amount = _d(raw.get("amount_delta", 0), "amount_delta"); price = _d(raw.get("unit_price", 0), "unit_price")
                if action in {"DECREASE", "DELETE"}: qty, amount = -abs(qty), -abs(amount)
                elif action in {"ADD", "INCREASE"}: qty, amount = abs(qty), abs(amount)
                delta += amount
                normalized.append((action, item_id or None, raw, qty, price, amount, index))
            before_amount = _d(contract["contract_amount"], "contract_amount"); after_amount = before_amount + delta
            if after_amount < 0: raise ValueError("after_amount cannot be negative")
            case_id, now = str(uuid4()), datetime.now(timezone.utc)
            before_snapshot = {"contract": _item_dict(contract), "items": [_item_dict(r) for r in current]}
            conn.execute(contract_change_cases.insert().values(id=case_id, contract_id=contract_id, change_no=no, reason=reason, responsibility=body.get("responsibility"), effective_date=body.get("effective_date"), status="DRAFT", before_amount=before_amount, delta_amount=delta, after_amount=after_amount, before_snapshot_json=json.dumps(before_snapshot, ensure_ascii=False, sort_keys=True), after_snapshot_json="{}", created_by=actor, created_at=now, row_version=1))
            for action, item_id, raw, qty, price, amount, index in normalized:
                conn.execute(contract_change_lines.insert().values(id=str(uuid4()), case_id=case_id, action=action, contract_item_id=item_id, source_budget_item_id=raw.get("source_budget_item_id"), item_no=raw.get("item_no"), name=raw.get("name"), unit=raw.get("unit"), quantity_delta=qty, unit_price=price, amount_delta=amount, sort_order=index))
        return self.get(case_id)

    def transition(self, case_id, body, actor):
        target, expected = str(body.get("status", "")).upper(), int(body.get("row_version", 0)); now = datetime.now(timezone.utc)
        with self.engine.begin() as conn:
            row = conn.execute(select(contract_change_cases).where(contract_change_cases.c.id == case_id)).mappings().first()
            if not row: raise LookupError("contract change case not found")
            current = str(row["status"]).upper()
            if int(row["row_version"]) != expected: raise RuntimeError("row version conflict")
            if target not in TRANSITIONS.get(current, set()): raise ValueError(f"invalid change transition {current} -> {target}")
            values = {"status": target, "row_version": expected + 1}
            if target == "APPROVED": values.update({"approved_by": actor, "approved_at": now})
            if target == "APPLIED":
                self._apply(conn, row, actor, now); values.update({"applied_by": actor, "applied_at": now})
            result = conn.execute(update(contract_change_cases).where(and_(contract_change_cases.c.id == case_id, contract_change_cases.c.row_version == expected)).values(**values))
            if result.rowcount != 1: raise RuntimeError("row version conflict")
        return self.get(case_id)

    def _apply(self, conn, case, actor, now):
        contract = conn.execute(select(contracts_v2).where(contracts_v2.c.id == case["contract_id"])).mappings().first()
        if not contract or str(contract["status"]).upper() not in {"APPROVED", "LOCKED"}: raise PermissionError("contract is not eligible for change apply")
        lines = conn.execute(select(contract_change_lines).where(contract_change_lines.c.case_id == case["id"]).order_by(contract_change_lines.c.sort_order)).mappings().all()
        max_sort = conn.execute(select(contract_items_v2.c.sort_order).where(contract_items_v2.c.contract_id == case["contract_id"]).order_by(contract_items_v2.c.sort_order.desc())).first(); next_sort = (max_sort[0] if max_sort else 0) + 1
        for line in lines:
            action = line["action"]
            if action == "ADD":
                conn.execute(contract_items_v2.insert().values(id=str(uuid4()), contract_id=case["contract_id"], source_budget_item_id=line["source_budget_item_id"] or f"CHANGE:{line['id']}", item_no=line["item_no"], name=line["name"] or "變更新增項", unit=line["unit"], quantity=line["quantity_delta"], unit_price=line["unit_price"], amount=line["amount_delta"], sort_order=next_sort, created_at=now)); next_sort += 1
            elif action == "DELETE": conn.execute(delete(contract_items_v2).where(and_(contract_items_v2.c.id == line["contract_item_id"], contract_items_v2.c.contract_id == case["contract_id"])))
            else:
                item = conn.execute(select(contract_items_v2).where(and_(contract_items_v2.c.id == line["contract_item_id"], contract_items_v2.c.contract_id == case["contract_id"]))).mappings().first()
                if not item: raise ValueError("target contract item no longer exists")
                qty = _d(item["quantity"], "quantity") + _d(line["quantity_delta"], "quantity_delta"); amount = _d(item["amount"], "amount") + _d(line["amount_delta"], "amount_delta")
                if qty < 0 or amount < 0: raise ValueError("change would make item negative")
                values = {"quantity": qty, "amount": amount}
                if action == "REPLACE": values.update({"name": line["name"] or item["name"], "unit": line["unit"] or item["unit"], "unit_price": line["unit_price"]})
                conn.execute(update(contract_items_v2).where(contract_items_v2.c.id == item["id"]).values(**values))
        items = conn.execute(select(contract_items_v2).where(contract_items_v2.c.contract_id == case["contract_id"]).order_by(contract_items_v2.c.sort_order)).mappings().all()
        actual = sum((_d(r["amount"], "amount") for r in items), Decimal("0"))
        if actual != _d(case["after_amount"], "after_amount"): raise RuntimeError("applied item total does not equal approved after_amount")
        conn.execute(update(contracts_v2).where(contracts_v2.c.id == case["contract_id"]).values(contract_amount=actual, status="APPROVED", updated_at=now, row_version=contracts_v2.c.row_version + 1))
        snapshot = {"items": [_item_dict(r) for r in items], "contract_amount": str(actual), "applied_by": actor}
        conn.execute(update(contract_change_cases).where(contract_change_cases.c.id == case["id"]).values(after_snapshot_json=json.dumps(snapshot, ensure_ascii=False, sort_keys=True)))

    def get(self, case_id):
        with self.engine.connect() as conn:
            row = conn.execute(select(contract_change_cases).where(contract_change_cases.c.id == case_id)).mappings().first()
            lines = conn.execute(select(contract_change_lines).where(contract_change_lines.c.case_id == case_id).order_by(contract_change_lines.c.sort_order)).mappings().all()
        if not row: raise LookupError("contract change case not found")
        return {"id": row["id"], "contract_id": row["contract_id"], "change_no": row["change_no"], "reason": row["reason"], "responsibility": row["responsibility"], "effective_date": row["effective_date"], "status": row["status"], "before_amount": str(row["before_amount"]), "delta_amount": str(row["delta_amount"]), "after_amount": str(row["after_amount"]), "before_snapshot": json.loads(row["before_snapshot_json"]), "after_snapshot": json.loads(row["after_snapshot_json"]), "approved_by": row["approved_by"], "approved_at": row["approved_at"].isoformat() if row["approved_at"] else None, "applied_by": row["applied_by"], "applied_at": row["applied_at"].isoformat() if row["applied_at"] else None, "row_version": row["row_version"], "items": [_item_dict(r) for r in lines], "deep_link": f"/app/contracts/{row['contract_id']}/changes/{row['id']}"}


def build_contract_change_governance_blueprint(service, resolve_user_id):
    bp = Blueprint("contract_change_governance", __name__, url_prefix="/api/contracts")
    @bp.post("/<contract_id>/change-cases")
    def create_case(contract_id):
        actor = resolve_user_id()
        if actor is None: return jsonify({"code":"UNAUTHORIZED"}), 401
        try: return jsonify(service.create(contract_id, request.get_json(silent=True) or {}, str(actor))), 201
        except LookupError as exc: return jsonify({"code":"NOT_FOUND","detail":str(exc)}), 404
        except PermissionError as exc: return jsonify({"code":"NOT_ELIGIBLE","detail":str(exc)}), 409
        except RuntimeError as exc: return jsonify({"code":"CONFLICT","detail":str(exc)}), 409
        except ValueError as exc: return jsonify({"code":"INVALID_ARGUMENT","detail":str(exc)}), 400
    @bp.get("/change-cases/<case_id>")
    def get_case(case_id):
        try: return jsonify(service.get(case_id))
        except LookupError as exc: return jsonify({"code":"NOT_FOUND","detail":str(exc)}), 404
    @bp.post("/change-cases/<case_id>/transition")
    def transition(case_id):
        actor = resolve_user_id()
        if actor is None: return jsonify({"code":"UNAUTHORIZED"}), 401
        try: return jsonify(service.transition(case_id, request.get_json(silent=True) or {}, str(actor)))
        except LookupError as exc: return jsonify({"code":"NOT_FOUND","detail":str(exc)}), 404
        except PermissionError as exc: return jsonify({"code":"NOT_ELIGIBLE","detail":str(exc)}), 409
        except RuntimeError as exc: return jsonify({"code":"CONFLICT","detail":str(exc)}), 409
        except ValueError as exc: return jsonify({"code":"INVALID_TRANSITION","detail":str(exc)}), 400
    return bp
