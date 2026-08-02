"""Phase 5 formal contract change orders with immutable before/after snapshots."""
from __future__ import annotations

import json
from datetime import datetime, timezone
from decimal import Decimal, InvalidOperation
from uuid import uuid4

from flask import Blueprint, jsonify, request
from sqlalchemy import Column, DateTime, Integer, MetaData, Numeric, String, Table, Text, and_, select, update

from api.contract_core import contract_items_v2, contracts_v2

metadata = MetaData()
contract_change_orders = Table(
    "contract_change_orders", metadata,
    Column("id", String(100), primary_key=True),
    Column("contract_id", String(100), nullable=False, index=True),
    Column("change_no", String(100), nullable=False),
    Column("reason", Text, nullable=False),
    Column("status", String(30), nullable=False),
    Column("before_amount", Numeric(28, 8), nullable=False),
    Column("delta_amount", Numeric(28, 8), nullable=False),
    Column("after_amount", Numeric(28, 8), nullable=False),
    Column("before_snapshot_json", Text, nullable=False),
    Column("after_snapshot_json", Text, nullable=False),
    Column("created_by", String(100), nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
    Column("row_version", Integer, nullable=False, default=1),
)
contract_change_items = Table(
    "contract_change_items", metadata,
    Column("id", String(100), primary_key=True),
    Column("change_order_id", String(100), nullable=False, index=True),
    Column("action", String(20), nullable=False),
    Column("contract_item_id", String(100)),
    Column("source_budget_item_id", String(100)),
    Column("item_no", String(100)),
    Column("name", String(500), nullable=False),
    Column("unit", String(100)),
    Column("quantity_delta", Numeric(28, 8), nullable=False),
    Column("unit_price", Numeric(28, 8), nullable=False),
    Column("amount_delta", Numeric(28, 8), nullable=False),
    Column("sort_order", Integer, nullable=False),
)


def _d(value, field):
    try: return Decimal(str(value))
    except (InvalidOperation, ValueError, TypeError) as exc: raise ValueError(f"{field} must be decimal") from exc


class ContractChangeService:
    def __init__(self, engine): self.engine=engine; metadata.create_all(engine)

    def create(self, contract_id, body, actor):
        change_no=str(body.get("change_no","")).strip(); reason=str(body.get("reason","")).strip(); changes=list(body.get("items") or [])
        if not change_no or not reason or not changes: raise ValueError("change_no, reason and items are required")
        with self.engine.connect() as conn:
            contract=conn.execute(select(contracts_v2).where(contracts_v2.c.id==contract_id)).mappings().first()
            items=[dict(r) for r in conn.execute(select(contract_items_v2).where(contract_items_v2.c.contract_id==contract_id).order_by(contract_items_v2.c.sort_order)).mappings()]
        if not contract: raise LookupError("contract not found")
        if str(contract["status"]).upper() not in {"APPROVED","LOCKED"}: raise PermissionError("formal change requires APPROVED or LOCKED contract")
        before_amount=_d(contract["contract_amount"],"before_amount"); by_id={str(i["id"]):i for i in items}; delta=Decimal("0"); normalized=[]
        for index, raw in enumerate(changes,1):
            action=str(raw.get("action","")).upper(); item_id=str(raw.get("contract_item_id","")).strip(); name=str(raw.get("name","")).strip()
            if action not in {"ADD","INCREASE","DECREASE","DELETE"}: raise ValueError("action must be ADD, INCREASE, DECREASE or DELETE")
            if action!="ADD" and item_id not in by_id: raise ValueError("contract_item_id not found")
            base=by_id.get(item_id,{}); name=name or str(base.get("name","")).strip()
            if not name: raise ValueError("name is required")
            qty=_d(raw.get("quantity_delta","0"),"quantity_delta"); price=_d(raw.get("unit_price",base.get("unit_price","0")),"unit_price")
            amount=_d(raw.get("amount_delta",qty*price),"amount_delta")
            if action in {"DECREASE","DELETE"}: amount=-abs(amount); qty=-abs(qty)
            else: amount=abs(amount); qty=abs(qty)
            delta+=amount; normalized.append((action,item_id,base,raw,name,qty,price,amount,index))
        after_amount=before_amount+delta
        if after_amount<0: raise ValueError("after_amount cannot be negative")
        change_id=str(uuid4()); now=datetime.now(timezone.utc); after_items=[dict(i) for i in items]
        with self.engine.begin() as conn:
            if conn.execute(select(contract_change_orders.c.id).where(and_(contract_change_orders.c.contract_id==contract_id,contract_change_orders.c.change_no==change_no))).first(): raise RuntimeError("change_no already exists")
            for action,item_id,base,raw,name,qty,price,amount,index in normalized:
                conn.execute(contract_change_items.insert().values(id=str(uuid4()),change_order_id=change_id,action=action,contract_item_id=item_id or None,source_budget_item_id=raw.get("source_budget_item_id") or base.get("source_budget_item_id"),item_no=raw.get("item_no") or base.get("item_no"),name=name,unit=raw.get("unit") or base.get("unit"),quantity_delta=qty,unit_price=price,amount_delta=amount,sort_order=index))
            conn.execute(contract_change_orders.insert().values(id=change_id,contract_id=contract_id,change_no=change_no,reason=reason,status="APPROVED",before_amount=before_amount,delta_amount=delta,after_amount=after_amount,before_snapshot_json=json.dumps(items,default=str,ensure_ascii=False),after_snapshot_json=json.dumps(after_items,default=str,ensure_ascii=False),created_by=actor,created_at=now,row_version=1))
            conn.execute(update(contracts_v2).where(contracts_v2.c.id==contract_id).values(contract_amount=after_amount,status="APPROVED",updated_at=now,row_version=contracts_v2.c.row_version+1))
        return self.get(change_id)

    def get(self, change_id):
        with self.engine.connect() as conn:
            row=conn.execute(select(contract_change_orders).where(contract_change_orders.c.id==change_id)).mappings().first()
            details=conn.execute(select(contract_change_items).where(contract_change_items.c.change_order_id==change_id).order_by(contract_change_items.c.sort_order)).mappings().all()
        if not row: raise LookupError("contract change not found")
        return {"id":row["id"],"contract_id":row["contract_id"],"change_no":row["change_no"],"reason":row["reason"],"status":row["status"],"before_amount":str(row["before_amount"]),"delta_amount":str(row["delta_amount"]),"after_amount":str(row["after_amount"]),"items":[{"action":d["action"],"contract_item_id":d["contract_item_id"],"name":d["name"],"quantity_delta":str(d["quantity_delta"]),"unit_price":str(d["unit_price"]),"amount_delta":str(d["amount_delta"])} for d in details],"deep_link":f"/app/contracts/{row['contract_id']}/changes/{row['id']}"}


def build_contract_change_blueprint(service, resolve_user_id):
    bp=Blueprint("contract_changes",__name__,url_prefix="/api/contracts")
    @bp.post("/<contract_id>/changes")
    def create_change(contract_id):
        actor=resolve_user_id()
        if actor is None:return jsonify({"code":"UNAUTHORIZED"}),401
        try:return jsonify(service.create(contract_id,request.get_json(silent=True) or {},str(actor))),201
        except LookupError as exc:return jsonify({"code":"NOT_FOUND","detail":str(exc)}),404
        except PermissionError as exc:return jsonify({"code":"READ_ONLY","detail":str(exc)}),409
        except RuntimeError as exc:return jsonify({"code":"CONFLICT","detail":str(exc)}),409
        except ValueError as exc:return jsonify({"code":"INVALID_ARGUMENT","detail":str(exc)}),400
    @bp.get("/changes/<change_id>")
    def get_change(change_id):
        try:return jsonify(service.get(change_id))
        except LookupError as exc:return jsonify({"code":"NOT_FOUND","detail":str(exc)}),404
    return bp
