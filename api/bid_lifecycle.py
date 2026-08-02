"""BUD→BID conversion, bid price versions, variance analysis and rollback."""
from __future__ import annotations

import json
from datetime import datetime, timezone
from decimal import Decimal, ROUND_HALF_UP
from uuid import uuid4

from flask import Blueprint, jsonify, request
from sqlalchemy import Column, DateTime, MetaData, String, Table, Text, and_, delete, select

from api.budget_decimal import budget_items_decimal

metadata = MetaData()
bid_price_versions = Table(
    "bid_price_versions", metadata,
    Column("id", String(100), primary_key=True),
    Column("project_code", String(100), nullable=False, index=True),
    Column("label", String(300), nullable=False),
    Column("status", String(30), nullable=False),
    Column("total_amount", String(100), nullable=False),
    Column("snapshot_json", Text, nullable=False),
    Column("created_by", String(100), nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
)
bid_conversion_runs = Table(
    "bid_conversion_runs", metadata,
    Column("id", String(100), primary_key=True),
    Column("source_project_code", String(100), nullable=False, index=True),
    Column("target_project_code", String(100), nullable=False, index=True),
    Column("operation", String(40), nullable=False),
    Column("status", String(30), nullable=False),
    Column("result_json", Text, nullable=False),
    Column("created_by", String(100), nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
)


def _money(value) -> Decimal:
    return Decimal(str(value or "0")).quantize(Decimal("0.01"), rounding=ROUND_HALF_UP)


class BidLifecycleService:
    def __init__(self, engine, validation_service):
        self.engine = engine
        self.validation = validation_service
        metadata.create_all(engine)

    def convert(self, source_project: str, target_project: str, actor: str, overwrite: bool = False) -> dict:
        if source_project == target_project:
            raise ValueError("source and target projects must differ")
        if self.validation.mode(source_project)["mode"] != "BUD":
            raise ValueError("source project must be BUD")
        now = datetime.now(timezone.utc)
        run_id = str(uuid4())
        with self.engine.begin() as conn:
            source = [dict(r) for r in conn.execute(select(budget_items_decimal).where(
                budget_items_decimal.c.project_code == source_project
            ).order_by(budget_items_decimal.c.id)).mappings().all()]
            if not source:
                raise ValueError("source budget has no items")
            target = [dict(r) for r in conn.execute(select(budget_items_decimal).where(
                budget_items_decimal.c.project_code == target_project
            )).mappings().all()]
            if target and not overwrite:
                raise ValueError("target BID already contains items")
            if overwrite:
                conn.execute(delete(budget_items_decimal).where(budget_items_decimal.c.project_code == target_project))
            id_map = {row["id"]: f"bid-{target_project}-{row['id']}" for row in source}
            for row in source:
                values = dict(row)
                values["id"] = id_map[row["id"]]
                values["project_code"] = target_project
                values["parent_id"] = id_map.get(row.get("parent_id"))
                values["row_version"] = 1
                values["created_at"] = now
                values["updated_at"] = now
                conn.execute(budget_items_decimal.insert().values(**values))
            result = {
                "id": run_id, "operation": "BUD_TO_BID", "status": "COMPLETED",
                "source_project_code": source_project, "target_project_code": target_project,
                "copied_items": len(source),
                "deep_link": f"/app/projects/by-code/{target_project}/bid-lifecycle?run={run_id}",
            }
            conn.execute(bid_conversion_runs.insert().values(
                id=run_id, source_project_code=source_project, target_project_code=target_project,
                operation="BUD_TO_BID", status="COMPLETED",
                result_json=json.dumps(result, ensure_ascii=False, sort_keys=True),
                created_by=actor, created_at=now,
            ))
        current = self.validation.mode(target_project)
        self.validation.set_mode(target_project, "BID", actor, str(current["row_version"]))
        return result

    def create_price_version(self, project_code: str, label: str, actor: str, status: str = "DRAFT") -> dict:
        if self.validation.mode(project_code)["mode"] != "BID":
            raise ValueError("price versions require BID mode")
        with self.engine.connect() as conn:
            rows = [self._row(r) for r in conn.execute(select(budget_items_decimal).where(
                budget_items_decimal.c.project_code == project_code
            ).order_by(budget_items_decimal.c.id)).mappings().all()]
        if not rows:
            raise ValueError("BID has no items")
        total = sum((_money(r["amount"]) for r in rows), Decimal("0.00"))
        version_id = str(uuid4()); now = datetime.now(timezone.utc)
        with self.engine.begin() as conn:
            conn.execute(bid_price_versions.insert().values(
                id=version_id, project_code=project_code, label=label or version_id,
                status=status.upper(), total_amount=f"{total:.2f}",
                snapshot_json=json.dumps(rows, ensure_ascii=False, sort_keys=True),
                created_by=actor, created_at=now,
            ))
        return self.get_version(version_id)

    def get_version(self, version_id: str) -> dict | None:
        with self.engine.connect() as conn:
            row = conn.execute(select(bid_price_versions).where(bid_price_versions.c.id == version_id)).mappings().first()
        if not row: return None
        return {"id": row["id"], "project_code": row["project_code"], "label": row["label"],
                "status": row["status"], "total_amount": row["total_amount"],
                "snapshot": json.loads(row["snapshot_json"]), "created_by": row["created_by"],
                "created_at": row["created_at"].isoformat(),
                "deep_link": f"/app/projects/by-code/{row['project_code']}/bid-lifecycle?version={row['id']}"}

    def list_versions(self, project_code: str) -> list[dict]:
        with self.engine.connect() as conn:
            ids = [r[0] for r in conn.execute(select(bid_price_versions.c.id).where(
                bid_price_versions.c.project_code == project_code
            ).order_by(bid_price_versions.c.created_at.desc()))]
        return [self.get_version(v) for v in ids]

    def variance(self, baseline_id: str, current_id: str) -> dict:
        left, right = self.get_version(baseline_id), self.get_version(current_id)
        if not left or not right: raise ValueError("price version not found")
        if left["project_code"] != right["project_code"]: raise ValueError("versions belong to different BID projects")
        base, current = _money(left["total_amount"]), _money(right["total_amount"])
        difference = current - base
        percentage = Decimal("0.00") if base == 0 else (difference / base * 100).quantize(Decimal("0.01"), rounding=ROUND_HALF_UP)
        return {"project_code": left["project_code"], "baseline_version": baseline_id,
                "current_version": current_id, "baseline_total": f"{base:.2f}",
                "current_total": f"{current:.2f}", "difference": f"{difference:.2f}",
                "percentage": f"{percentage:.2f}"}

    def rollback(self, version_id: str, actor: str) -> dict:
        version = self.get_version(version_id)
        if not version: raise ValueError("price version not found")
        project = version["project_code"]
        now = datetime.now(timezone.utc); run_id = str(uuid4())
        with self.engine.begin() as conn:
            conn.execute(delete(budget_items_decimal).where(budget_items_decimal.c.project_code == project))
            for row in version["snapshot"]:
                values = dict(row)
                values.pop("created_at", None); values.pop("updated_at", None)
                values["row_version"] = int(values.get("row_version", 0)) + 1
                values["created_at"] = now; values["updated_at"] = now
                conn.execute(budget_items_decimal.insert().values(**values))
            result = {"id": run_id, "operation": "BID_ROLLBACK", "status": "COMPLETED",
                      "target_project_code": project, "restored_version": version_id,
                      "restored_items": len(version["snapshot"]),
                      "deep_link": f"/app/projects/by-code/{project}/bid-lifecycle?run={run_id}"}
            conn.execute(bid_conversion_runs.insert().values(
                id=run_id, source_project_code=project, target_project_code=project,
                operation="BID_ROLLBACK", status="COMPLETED",
                result_json=json.dumps(result, ensure_ascii=False, sort_keys=True),
                created_by=actor, created_at=now,
            ))
        return result

    @staticmethod
    def _row(row) -> dict:
        result = dict(row)
        for key in ("quantity", "unit_price", "amount"): result[key] = str(result[key])
        for key in ("created_at", "updated_at"):
            value = result.get(key); result[key] = value.isoformat() if value else None
        return result


def build_bid_lifecycle_blueprint(service: BidLifecycleService, resolve_user_id):
    bp = Blueprint("bid_lifecycle", __name__, url_prefix="/api/decimal-budget")
    def actor():
        value = resolve_user_id()
        if value is None: raise PermissionError("authentication required")
        return str(value)
    @bp.post("/bud-to-bid")
    def convert():
        b=request.get_json(silent=True) or {}
        try:return jsonify(service.convert(str(b.get("source_project_code","")),str(b.get("target_project_code","")),actor(),bool(b.get("overwrite",False)))),201
        except ValueError as exc:return jsonify({"code":"INVALID_ARGUMENT","detail":str(exc)}),400
    @bp.get("/projects/<project_code>/bid-price-versions")
    def versions(project_code): return jsonify(service.list_versions(project_code))
    @bp.post("/projects/<project_code>/bid-price-versions")
    def create_version(project_code):
        b=request.get_json(silent=True) or {}
        try:return jsonify(service.create_price_version(project_code,str(b.get("label") or "Bid price"),actor(),str(b.get("status") or "DRAFT"))),201
        except ValueError as exc:return jsonify({"code":"INVALID_ARGUMENT","detail":str(exc)}),400
    @bp.get("/bid-price-versions/<version_id>")
    def get_version(version_id):
        row=service.get_version(version_id);return (jsonify(row),200) if row else (jsonify({"code":"NOT_FOUND"}),404)
    @bp.get("/bid-price-versions/<baseline_id>/variance/<current_id>")
    def variance(baseline_id,current_id):
        try:return jsonify(service.variance(baseline_id,current_id))
        except ValueError as exc:return jsonify({"code":"INVALID_ARGUMENT","detail":str(exc)}),400
    @bp.post("/bid-price-versions/<version_id>/rollback")
    def rollback(version_id):
        try:return jsonify(service.rollback(version_id,actor()))
        except ValueError as exc:return jsonify({"code":"INVALID_ARGUMENT","detail":str(exc)}),400
    return bp
