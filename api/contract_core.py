"""Phase 5 contract eligibility, contract master and budget-item lineage."""
from __future__ import annotations

from datetime import datetime, timezone
from decimal import Decimal, InvalidOperation
from uuid import uuid4

from flask import Blueprint, jsonify, request
from sqlalchemy import Column, DateTime, Integer, MetaData, Numeric, String, Table, Text, and_, select

from api.budget_versioning import budget_versions

metadata = MetaData()
contracts_v2 = Table(
    "contracts_v2", metadata,
    Column("id", String(100), primary_key=True),
    Column("project_code", String(100), nullable=False, index=True),
    Column("budget_version_id", String(100), nullable=False, index=True),
    Column("contract_no", String(100), nullable=False),
    Column("name", String(500), nullable=False),
    Column("contractor", String(500)),
    Column("status", String(30), nullable=False),
    Column("contract_amount", Numeric(28, 8), nullable=False),
    Column("created_by", String(100), nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
    Column("updated_at", DateTime(timezone=True), nullable=False),
    Column("row_version", Integer, nullable=False, default=1),
)
contract_items_v2 = Table(
    "contract_items_v2", metadata,
    Column("id", String(100), primary_key=True),
    Column("contract_id", String(100), nullable=False, index=True),
    Column("source_budget_item_id", String(100), nullable=False, index=True),
    Column("item_no", String(100)),
    Column("name", String(500), nullable=False),
    Column("unit", String(100)),
    Column("quantity", Numeric(28, 8), nullable=False),
    Column("unit_price", Numeric(28, 8), nullable=False),
    Column("amount", Numeric(28, 8), nullable=False),
    Column("sort_order", Integer, nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
)

READ_ONLY_VERSION_STATUSES = {"APPROVED", "FROZEN", "ARCHIVED"}


def _d(value, field):
    try:
        result = Decimal(str(value))
    except (InvalidOperation, ValueError, TypeError) as exc:
        raise ValueError(f"{field} must be decimal") from exc
    if result < 0:
        raise ValueError(f"{field} cannot be negative")
    return result


class ContractCoreService:
    def __init__(self, engine):
        self.engine = engine
        metadata.create_all(engine)

    def eligibility(self, project_code: str, budget_version_id: str) -> dict:
        with self.engine.connect() as conn:
            version = conn.execute(select(budget_versions).where(budget_versions.c.id == budget_version_id)).mappings().first()
        reasons = []
        if not version:
            reasons.append("BUDGET_VERSION_NOT_FOUND")
        else:
            if version["project_code"] != project_code:
                reasons.append("PROJECT_VERSION_MISMATCH")
            if str(version["status"]).upper() not in READ_ONLY_VERSION_STATUSES:
                reasons.append("BUDGET_VERSION_NOT_APPROVED")
        return {"project_code": project_code, "budget_version_id": budget_version_id, "eligible": not reasons, "reasons": reasons}

    def create(self, body: dict, actor: str) -> dict:
        project_code = str(body.get("project_code", "")).strip()
        version_id = str(body.get("budget_version_id", "")).strip()
        contract_no = str(body.get("contract_no", "")).strip()
        name = str(body.get("name", "")).strip()
        items = list(body.get("items") or [])
        if not all((project_code, version_id, contract_no, name)):
            raise ValueError("project_code, budget_version_id, contract_no and name are required")
        eligibility = self.eligibility(project_code, version_id)
        if not eligibility["eligible"]:
            raise PermissionError(",".join(eligibility["reasons"]))
        if not items:
            raise ValueError("contract items are required")
        seen = set(); normalized = []; total = Decimal("0")
        for index, item in enumerate(items, 1):
            source_id = str(item.get("source_budget_item_id", "")).strip()
            item_name = str(item.get("name", "")).strip()
            if not source_id or not item_name:
                raise ValueError("source_budget_item_id and name are required")
            if source_id in seen:
                raise ValueError("duplicate source_budget_item_id")
            seen.add(source_id)
            quantity = _d(item.get("quantity", "0"), "quantity")
            unit_price = _d(item.get("unit_price", "0"), "unit_price")
            amount = _d(item.get("amount", quantity * unit_price), "amount")
            total += amount
            normalized.append((source_id, item_name, quantity, unit_price, amount, item, index))
        declared = _d(body.get("contract_amount", total), "contract_amount")
        if declared != total:
            raise ValueError("contract_amount must equal contract item total")
        contract_id = str(uuid4()); now = datetime.now(timezone.utc)
        with self.engine.begin() as conn:
            duplicate = conn.execute(select(contracts_v2.c.id).where(and_(contracts_v2.c.project_code == project_code, contracts_v2.c.contract_no == contract_no))).first()
            if duplicate:
                raise RuntimeError("contract_no already exists in project")
            conn.execute(contracts_v2.insert().values(id=contract_id, project_code=project_code, budget_version_id=version_id, contract_no=contract_no, name=name, contractor=body.get("contractor"), status="DRAFT", contract_amount=declared, created_by=actor, created_at=now, updated_at=now, row_version=1))
            for source_id, item_name, quantity, unit_price, amount, raw, index in normalized:
                conn.execute(contract_items_v2.insert().values(id=str(uuid4()), contract_id=contract_id, source_budget_item_id=source_id, item_no=raw.get("item_no"), name=item_name, unit=raw.get("unit"), quantity=quantity, unit_price=unit_price, amount=amount, sort_order=index, created_at=now))
        return self.get(contract_id)

    def get(self, contract_id: str) -> dict:
        with self.engine.connect() as conn:
            row = conn.execute(select(contracts_v2).where(contracts_v2.c.id == contract_id)).mappings().first()
            items = conn.execute(select(contract_items_v2).where(contract_items_v2.c.contract_id == contract_id).order_by(contract_items_v2.c.sort_order)).mappings().all()
        if not row:
            raise LookupError("contract not found")
        return {"id": row["id"], "project_code": row["project_code"], "budget_version_id": row["budget_version_id"], "contract_no": row["contract_no"], "name": row["name"], "contractor": row["contractor"], "status": row["status"], "contract_amount": str(row["contract_amount"]), "row_version": row["row_version"], "items": [{"id": item["id"], "source_budget_item_id": item["source_budget_item_id"], "item_no": item["item_no"], "name": item["name"], "unit": item["unit"], "quantity": str(item["quantity"]), "unit_price": str(item["unit_price"]), "amount": str(item["amount"]), "deep_link": f"/app/projects/by-code/{row['project_code']}/budget?item={item['source_budget_item_id']}"} for item in items], "deep_link": f"/app/contracts/{row['id']}"}


def build_contract_core_blueprint(service, resolve_user_id):
    bp = Blueprint("contract_core", __name__, url_prefix="/api/contracts")
    @bp.get("/eligibility")
    def eligibility():
        return jsonify(service.eligibility(str(request.args.get("project_code", "")), str(request.args.get("budget_version_id", ""))))
    @bp.post("")
    def create_contract():
        actor = resolve_user_id()
        if actor is None: return jsonify({"code":"UNAUTHORIZED"}), 401
        try: return jsonify(service.create(request.get_json(silent=True) or {}, str(actor))), 201
        except PermissionError as exc: return jsonify({"code":"NOT_ELIGIBLE","detail":str(exc)}), 409
        except RuntimeError as exc: return jsonify({"code":"CONFLICT","detail":str(exc)}), 409
        except ValueError as exc: return jsonify({"code":"INVALID_ARGUMENT","detail":str(exc)}), 400
    @bp.get("/<contract_id>")
    def get_contract(contract_id):
        try: return jsonify(service.get(contract_id))
        except LookupError as exc: return jsonify({"code":"NOT_FOUND","detail":str(exc)}), 404
    return bp
