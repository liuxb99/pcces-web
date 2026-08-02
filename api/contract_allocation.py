"""Phase 5 contract allocation limits, allocation basis and subcontract lineage."""
from __future__ import annotations

import json
from datetime import datetime, timezone
from decimal import Decimal, InvalidOperation
from uuid import uuid4

from flask import Blueprint, jsonify, request
from sqlalchemy import Column, DateTime, Integer, MetaData, Numeric, String, Table, and_, func, select

from api.budget_versioning import budget_versions
from api.contract_core import contract_items_v2, contracts_v2

metadata = MetaData()
subcontract_links_v2 = Table(
    "subcontract_links_v2", metadata,
    Column("id", String(100), primary_key=True),
    Column("parent_contract_id", String(100), nullable=False, index=True),
    Column("subcontract_id", String(100), nullable=False, unique=True, index=True),
    Column("created_by", String(100), nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
    Column("row_version", Integer, nullable=False, default=1),
)


def _decimal(value, field):
    try:
        result = Decimal(str(value))
    except (InvalidOperation, TypeError, ValueError) as exc:
        raise ValueError(f"{field} must be decimal") from exc
    if result < 0:
        raise ValueError(f"{field} cannot be negative")
    return result


def _snapshot_items(snapshot_json):
    payload = json.loads(snapshot_json or "[]")
    return {str(row.get("id", "")): row for row in payload if str(row.get("id", "")).strip()}


class ContractAllocationService:
    def __init__(self, engine):
        self.engine = engine
        metadata.create_all(engine)

    def _contract(self, conn, contract_id):
        row = conn.execute(select(contracts_v2).where(contracts_v2.c.id == contract_id)).mappings().first()
        if not row:
            raise LookupError("contract not found")
        return row

    def basis(self, contract_id: str) -> dict:
        with self.engine.connect() as conn:
            contract = self._contract(conn, contract_id)
            version = conn.execute(select(budget_versions).where(budget_versions.c.id == contract["budget_version_id"])).mappings().first()
            if not version:
                raise LookupError("budget version not found")
            snapshots = _snapshot_items(version["snapshot_json"])
            allocated = conn.execute(
                select(
                    contract_items_v2.c.source_budget_item_id,
                    func.coalesce(func.sum(contract_items_v2.c.quantity), 0).label("quantity"),
                    func.coalesce(func.sum(contract_items_v2.c.amount), 0).label("amount"),
                )
                .select_from(contract_items_v2.join(contracts_v2, contract_items_v2.c.contract_id == contracts_v2.c.id))
                .where(contracts_v2.c.budget_version_id == contract["budget_version_id"])
                .group_by(contract_items_v2.c.source_budget_item_id)
            ).mappings().all()
        allocated_map = {row["source_budget_item_id"]: row for row in allocated}
        items = []
        for source_id, source in snapshots.items():
            baseline_qty = _decimal(source.get("quantity", "0"), "baseline quantity")
            baseline_amount = _decimal(source.get("amount", "0"), "baseline amount")
            used = allocated_map.get(source_id, {})
            used_qty = Decimal(str(used.get("quantity", 0)))
            used_amount = Decimal(str(used.get("amount", 0)))
            items.append({
                "source_budget_item_id": source_id,
                "item_no": source.get("item_no"),
                "name": source.get("name", ""),
                "baseline_quantity": str(baseline_qty),
                "allocated_quantity": str(used_qty),
                "remaining_quantity": str(baseline_qty - used_qty),
                "baseline_amount": str(baseline_amount),
                "allocated_amount": str(used_amount),
                "remaining_amount": str(baseline_amount - used_amount),
            })
        return {"contract_id": contract_id, "budget_version_id": contract["budget_version_id"], "items": items}

    def add_items(self, contract_id: str, body: dict, actor: str) -> dict:
        requested = list(body.get("items") or [])
        expected = int(body.get("row_version", 0))
        if not requested:
            raise ValueError("items are required")
        with self.engine.begin() as conn:
            contract = self._contract(conn, contract_id)
            if contract["status"] != "DRAFT":
                raise PermissionError("only DRAFT contract can be allocated")
            if expected and expected != contract["row_version"]:
                raise RuntimeError("row_version conflict")
            version = conn.execute(select(budget_versions).where(budget_versions.c.id == contract["budget_version_id"])).mappings().first()
            if not version:
                raise LookupError("budget version not found")
            snapshots = _snapshot_items(version["snapshot_json"])
            allocated_rows = conn.execute(
                select(contract_items_v2.c.source_budget_item_id, func.sum(contract_items_v2.c.quantity).label("quantity"), func.sum(contract_items_v2.c.amount).label("amount"))
                .select_from(contract_items_v2.join(contracts_v2, contract_items_v2.c.contract_id == contracts_v2.c.id))
                .where(contracts_v2.c.budget_version_id == contract["budget_version_id"])
                .group_by(contract_items_v2.c.source_budget_item_id)
            ).mappings().all()
            allocated = {row["source_budget_item_id"]: row for row in allocated_rows}
            existing = set(conn.execute(select(contract_items_v2.c.source_budget_item_id).where(contract_items_v2.c.contract_id == contract_id)).scalars())
            normalized = []
            for index, item in enumerate(requested, 1):
                source_id = str(item.get("source_budget_item_id", "")).strip()
                if not source_id or source_id not in snapshots:
                    raise ValueError("source budget item does not exist in baseline")
                if source_id in existing:
                    raise ValueError("source budget item already exists in contract")
                source = snapshots[source_id]
                qty = _decimal(item.get("quantity", "0"), "quantity")
                amount = _decimal(item.get("amount", "0"), "amount")
                baseline_qty = _decimal(source.get("quantity", "0"), "baseline quantity")
                baseline_amount = _decimal(source.get("amount", "0"), "baseline amount")
                used = allocated.get(source_id, {})
                if qty > baseline_qty - Decimal(str(used.get("quantity", 0))):
                    raise ArithmeticError("allocated quantity exceeds remaining baseline")
                if amount > baseline_amount - Decimal(str(used.get("amount", 0))):
                    raise ArithmeticError("allocated amount exceeds remaining baseline")
                unit_price = _decimal(item.get("unit_price", source.get("unit_price", "0")), "unit_price")
                normalized.append((source_id, source, qty, unit_price, amount, index))
            now = datetime.now(timezone.utc)
            current_count = conn.execute(select(func.count()).select_from(contract_items_v2).where(contract_items_v2.c.contract_id == contract_id)).scalar_one()
            for source_id, source, qty, unit_price, amount, index in normalized:
                conn.execute(contract_items_v2.insert().values(
                    id=str(uuid4()), contract_id=contract_id, source_budget_item_id=source_id,
                    item_no=source.get("item_no"), name=source.get("name") or source.get("c_name") or source_id,
                    unit=source.get("unit") or source.get("c_unit"), quantity=qty, unit_price=unit_price,
                    amount=amount, sort_order=current_count + index, created_at=now,
                ))
            new_total = conn.execute(select(func.coalesce(func.sum(contract_items_v2.c.amount), 0)).where(contract_items_v2.c.contract_id == contract_id)).scalar_one()
            conn.execute(contracts_v2.update().where(and_(contracts_v2.c.id == contract_id, contracts_v2.c.row_version == contract["row_version"])).values(contract_amount=new_total, row_version=contract["row_version"] + 1, updated_at=now))
        return self.basis(contract_id)

    def link_subcontract(self, parent_contract_id: str, subcontract_id: str, actor: str) -> dict:
        if parent_contract_id == subcontract_id:
            raise ValueError("contract cannot be its own parent")
        with self.engine.begin() as conn:
            parent = self._contract(conn, parent_contract_id)
            child = self._contract(conn, subcontract_id)
            if parent["project_code"] != child["project_code"]:
                raise ValueError("parent and subcontract must belong to same project")
            if parent["budget_version_id"] != child["budget_version_id"]:
                raise ValueError("parent and subcontract must share budget baseline")
            duplicate = conn.execute(select(subcontract_links_v2.c.id).where(subcontract_links_v2.c.subcontract_id == subcontract_id)).first()
            if duplicate:
                raise RuntimeError("subcontract already linked")
            link_id = str(uuid4())
            conn.execute(subcontract_links_v2.insert().values(id=link_id, parent_contract_id=parent_contract_id, subcontract_id=subcontract_id, created_by=actor, created_at=datetime.now(timezone.utc), row_version=1))
        return {"id": link_id, "parent_contract_id": parent_contract_id, "subcontract_id": subcontract_id, "deep_link": f"/app/contracts/{parent_contract_id}?subcontract={subcontract_id}"}


def build_contract_allocation_blueprint(service, resolve_user_id):
    bp = Blueprint("contract_allocation", __name__, url_prefix="/api/contracts")

    @bp.get("/<contract_id>/allocation-basis")
    def allocation_basis(contract_id):
        try: return jsonify(service.basis(contract_id))
        except LookupError as exc: return jsonify({"code":"NOT_FOUND","detail":str(exc)}), 404

    @bp.post("/<contract_id>/items")
    def add_items(contract_id):
        actor = resolve_user_id()
        if actor is None: return jsonify({"code":"UNAUTHORIZED"}), 401
        try: return jsonify(service.add_items(contract_id, request.get_json(silent=True) or {}, str(actor))), 201
        except LookupError as exc: return jsonify({"code":"NOT_FOUND","detail":str(exc)}), 404
        except PermissionError as exc: return jsonify({"code":"READ_ONLY","detail":str(exc)}), 409
        except RuntimeError as exc: return jsonify({"code":"CONFLICT","detail":str(exc)}), 409
        except ArithmeticError as exc: return jsonify({"code":"ALLOCATION_EXCEEDED","detail":str(exc)}), 409
        except ValueError as exc: return jsonify({"code":"INVALID_ARGUMENT","detail":str(exc)}), 400

    @bp.post("/<parent_contract_id>/subcontracts/<subcontract_id>")
    def link_subcontract(parent_contract_id, subcontract_id):
        actor = resolve_user_id()
        if actor is None: return jsonify({"code":"UNAUTHORIZED"}), 401
        try: return jsonify(service.link_subcontract(parent_contract_id, subcontract_id, str(actor))), 201
        except LookupError as exc: return jsonify({"code":"NOT_FOUND","detail":str(exc)}), 404
        except RuntimeError as exc: return jsonify({"code":"CONFLICT","detail":str(exc)}), 409
        except ValueError as exc: return jsonify({"code":"INVALID_ARGUMENT","detail":str(exc)}), 400
    return bp
