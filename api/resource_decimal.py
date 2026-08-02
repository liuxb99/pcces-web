"""Exact-decimal resource and breakdown core for Phase 2/3."""
from __future__ import annotations

from datetime import datetime, timezone
from decimal import Decimal
from typing import Callable

from flask import Blueprint, jsonify, request
from sqlalchemy import Column, DateTime, Integer, MetaData, Numeric, String, Table, and_, select

from api.decimal_math import multiply, quantize, sum_values

metadata = MetaData()
resources_decimal = Table("resources_decimal", metadata,
    Column("id", String(100), primary_key=True), Column("code", String(100), nullable=False, unique=True, index=True),
    Column("name", String(500), nullable=False), Column("unit", String(50)),
    Column("unit_price", Numeric(28, 8), nullable=False, default=Decimal("0")),
    Column("price_scale", Integer, nullable=False, default=4), Column("created_at", DateTime(timezone=True), nullable=False),
    Column("updated_at", DateTime(timezone=True), nullable=False), Column("row_version", Integer, nullable=False, default=1))
resource_breakdowns_decimal = Table("resource_breakdowns_decimal", metadata,
    Column("id", String(100), primary_key=True), Column("resource_id", String(100), nullable=False, index=True),
    Column("code", String(100), nullable=False), Column("name", String(500), nullable=False), Column("unit", String(50)),
    Column("quantity", Numeric(28, 8), nullable=False, default=Decimal("0")),
    Column("unit_price", Numeric(28, 8), nullable=False, default=Decimal("0")),
    Column("amount", Numeric(28, 8), nullable=False, default=Decimal("0")),
    Column("quantity_scale", Integer, nullable=False, default=4), Column("price_scale", Integer, nullable=False, default=4),
    Column("amount_scale", Integer, nullable=False, default=2), Column("created_at", DateTime(timezone=True), nullable=False),
    Column("updated_at", DateTime(timezone=True), nullable=False), Column("row_version", Integer, nullable=False, default=1))

class ResourceDecimalService:
    def __init__(self, engine): self.engine = engine
    def create_schema(self): metadata.create_all(self.engine)
    def save_resource(self, resource_id, body):
        now=datetime.now(timezone.utc)
        with self.engine.begin() as conn:
            current=conn.execute(select(resources_decimal).where(resources_decimal.c.id==resource_id)).mappings().first()
            requested=int(body.get("row_version",0))
            if current and requested!=int(current["row_version"]): return {"code":"CONFLICT","current_row_version":int(current["row_version"])},409
            scale=int(body.get("price_scale",current["price_scale"] if current else 4));price=quantize(str(body.get("unit_price",current["unit_price"] if current else "0")),scale)
            values={"code":body.get("code") or (current["code"] if current else ""),"name":body.get("name") or (current["name"] if current else ""),"unit":body.get("unit",current["unit"] if current else None),"unit_price":Decimal(price),"price_scale":scale,"updated_at":now,"row_version":1 if not current else int(current["row_version"])+1}
            if not values["code"] or not values["name"]: return {"code":"INVALID_ARGUMENT","detail":"code and name are required"},400
            if current:
                result=conn.execute(resources_decimal.update().where(and_(resources_decimal.c.id==resource_id,resources_decimal.c.row_version==requested)).values(**values))
                if result.rowcount!=1:return {"code":"CONFLICT"},409
            else: conn.execute(resources_decimal.insert().values(id=resource_id,created_at=now,**values))
        return self.get_resource(resource_id),200
    def save_breakdown(self, breakdown_id, body):
        now=datetime.now(timezone.utc)
        with self.engine.begin() as conn:
            current=conn.execute(select(resource_breakdowns_decimal).where(resource_breakdowns_decimal.c.id==breakdown_id)).mappings().first();requested=int(body.get("row_version",0))
            if current and requested!=int(current["row_version"]):return {"code":"CONFLICT","current_row_version":int(current["row_version"])},409
            qs=int(body.get("quantity_scale",current["quantity_scale"] if current else 4));ps=int(body.get("price_scale",current["price_scale"] if current else 4));ats=int(body.get("amount_scale",current["amount_scale"] if current else 2))
            q=quantize(str(body.get("quantity",current["quantity"] if current else "0")),qs);p=quantize(str(body.get("unit_price",current["unit_price"] if current else "0")),ps);a=multiply(q,p,ats)
            values={"resource_id":body.get("resource_id") or (current["resource_id"] if current else ""),"code":body.get("code") or (current["code"] if current else ""),"name":body.get("name") or (current["name"] if current else ""),"unit":body.get("unit",current["unit"] if current else None),"quantity":Decimal(q),"unit_price":Decimal(p),"amount":Decimal(a),"quantity_scale":qs,"price_scale":ps,"amount_scale":ats,"updated_at":now,"row_version":1 if not current else int(current["row_version"])+1}
            if not values["resource_id"] or not values["code"] or not values["name"]:return {"code":"INVALID_ARGUMENT","detail":"resource_id, code and name are required"},400
            if current:
                result=conn.execute(resource_breakdowns_decimal.update().where(and_(resource_breakdowns_decimal.c.id==breakdown_id,resource_breakdowns_decimal.c.row_version==requested)).values(**values))
                if result.rowcount!=1:return {"code":"CONFLICT"},409
            else:conn.execute(resource_breakdowns_decimal.insert().values(id=breakdown_id,created_at=now,**values))
        self.recalculate_resource(values["resource_id"]);return self.get_breakdown(breakdown_id),200
    def recalculate_resource(self, resource_id):
        with self.engine.begin() as conn:
            resource=conn.execute(select(resources_decimal).where(resources_decimal.c.id==resource_id)).mappings().first()
            if not resource:return {"code":"NOT_FOUND"},404
            rows=conn.execute(select(resource_breakdowns_decimal).where(resource_breakdowns_decimal.c.resource_id==resource_id)).mappings().all();total=sum_values([str(row["amount"]) for row in rows],int(resource["price_scale"]))
            conn.execute(resources_decimal.update().where(resources_decimal.c.id==resource_id).values(unit_price=Decimal(total),updated_at=datetime.now(timezone.utc),row_version=int(resource["row_version"])+1))
        return self.get_resource(resource_id),200
    def get_resource(self, resource_id):
        with self.engine.connect() as conn:row=conn.execute(select(resources_decimal).where(resources_decimal.c.id==resource_id)).mappings().first()
        if not row:return None
        return {"id":row["id"],"code":row["code"],"name":row["name"],"unit":row["unit"],"unit_price":quantize(str(row["unit_price"]),int(row["price_scale"])),"price_scale":row["price_scale"],"row_version":row["row_version"]}
    def get_breakdown(self, breakdown_id):
        with self.engine.connect() as conn:row=conn.execute(select(resource_breakdowns_decimal).where(resource_breakdowns_decimal.c.id==breakdown_id)).mappings().first()
        if not row:return None
        return {"id":row["id"],"resource_id":row["resource_id"],"code":row["code"],"name":row["name"],"unit":row["unit"],"quantity":quantize(str(row["quantity"]),int(row["quantity_scale"])),"unit_price":quantize(str(row["unit_price"]),int(row["price_scale"])),"amount":quantize(str(row["amount"]),int(row["amount_scale"])),"quantity_scale":row["quantity_scale"],"price_scale":row["price_scale"],"amount_scale":row["amount_scale"],"row_version":row["row_version"]}

def build_resource_decimal_blueprint(service: ResourceDecimalService, resolve_user_id: Callable[[], int|None]):
    from api.resource_project_reference import ResourceProjectReferenceService
    bp=Blueprint("resource_decimal",__name__,url_prefix="/api/decimal-resources");refs=ResourceProjectReferenceService(service.engine)
    def auth():return resolve_user_id() is not None
    @bp.put("/<resource_id>")
    def save_resource(resource_id):
        if not auth():return jsonify({"code":"UNAUTHORIZED"}),401
        result,status=service.save_resource(resource_id,request.get_json(silent=True) or {});return jsonify(result),status
    @bp.get("/<resource_id>")
    def get_resource(resource_id):
        if not auth():return jsonify({"code":"UNAUTHORIZED"}),401
        item=service.get_resource(resource_id);return (jsonify(item),200) if item else (jsonify({"code":"NOT_FOUND"}),404)
    @bp.put("/breakdowns/<breakdown_id>")
    def save_breakdown(breakdown_id):
        if not auth():return jsonify({"code":"UNAUTHORIZED"}),401
        result,status=service.save_breakdown(breakdown_id,request.get_json(silent=True) or {});return jsonify(result),status
    @bp.post("/<resource_id>/recalculate")
    def recalculate(resource_id):
        if not auth():return jsonify({"code":"UNAUTHORIZED"}),401
        result,status=service.recalculate_resource(resource_id);return jsonify(result),status
    @bp.post("/projects/<target_project_code>/references")
    def create_reference(target_project_code):
        actor=resolve_user_id()
        if actor is None:return jsonify({"code":"UNAUTHORIZED"}),401
        body=request.get_json(silent=True) or {}
        try:return jsonify(refs.import_reference(target_project_code,str(body.get("source_project_code","")),str(body.get("source_resource_id","")),str(body.get("target_resource_id","")),str(body.get("reference_type","")),str(actor))),201
        except LookupError as exc:return jsonify({"code":"NOT_FOUND","detail":str(exc)}),404
        except RuntimeError:return jsonify({"code":"CONFLICT","detail":"target resource already exists"}),409
        except ValueError as exc:return jsonify({"code":"INVALID_ARGUMENT","detail":str(exc)}),400
    @bp.get("/projects/<target_project_code>/references")
    def list_references(target_project_code):
        if not auth():return jsonify({"code":"UNAUTHORIZED"}),401
        return jsonify(refs.list_target(target_project_code))
    return bp
