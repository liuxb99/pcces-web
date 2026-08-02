"""Compatibility bridge for legacy resource APIs backed by exact Decimal rows."""
from __future__ import annotations

from decimal import Decimal

from flask import jsonify, request
from sqlalchemy import delete, select

from api.models import BudgetItem, Project, Resource, ResourceBreakdownItem
from api.resource_budget_lineage import ResourceBudgetLineageService
from api.resource_decimal import ResourceDecimalService, resource_breakdowns_decimal, resources_decimal


def _resource_id(value: int | str) -> str:
    return f"legacy-resource-{value}"


def _breakdown_id(value: int | str) -> str:
    return f"legacy-breakdown-{value}"


def _text(value) -> str:
    return format(Decimal(str(value or 0)), "f")


def install_legacy_resource_bridge(app, engine, session_factory) -> None:
    service = ResourceDecimalService(engine)
    lineage = ResourceBudgetLineageService(engine)

    def project_or_404(db, project_id):
        project = db.query(Project).filter(Project.id == project_id).first()
        return project

    def serialize(resource):
        shadow = service.get_resource(_resource_id(resource.id))
        result = {column.name: getattr(resource, column.name) for column in resource.__table__.columns}
        if shadow:
            result.update({"unit_price": shadow["unit_price"], "row_version": shadow["row_version"], "decimal_core": True})
        return result

    def list_resources(project_id, user_id):
        db = session_factory()
        try:
            if not project_or_404(db, project_id): return jsonify({"detail":"專案不存在"}),404
            rows = db.query(Resource).filter(Resource.project_id == project_id).all()
            return jsonify([serialize(row) for row in rows])
        finally: db.close()

    def create_resource(project_id, user_id):
        body = request.get_json(silent=True) or {}
        db = session_factory()
        try:
            project = project_or_404(db, project_id)
            if not project: return jsonify({"detail":"專案不存在"}),404
            row = Resource(project_id=project_id, code=body["code"], c_name=body["c_name"],
                           e_name=body.get("e_name"), c_unit=body.get("c_unit", "式"),
                           e_unit=body.get("e_unit"), unit_price=float(body.get("unit_price", 0)),
                           is_analysis=body.get("is_analysis", False))
            db.add(row); db.flush()
            result, status = service.save_resource(_resource_id(row.id), {
                "code": f"{project.code}:{row.code}", "name": row.c_name, "unit": row.c_unit,
                "unit_price": _text(body.get("unit_price", 0)), "price_scale": int(body.get("price_scale", 4)),
                "row_version": 0,
            })
            if status >= 400: raise ValueError(result)
            db.commit(); db.refresh(row)
            return jsonify(serialize(row)),201
        except Exception:
            db.rollback(); raise
        finally: db.close()

    def update_resource(project_id, resource_id, user_id):
        body = request.get_json(silent=True) or {}
        db = session_factory()
        try:
            project = project_or_404(db, project_id)
            row = db.query(Resource).filter(Resource.id == resource_id, Resource.project_id == project_id).first()
            if not project or not row: return jsonify({"detail":"資源不存在"}),404
            for key in ("code","c_name","e_name","c_unit","e_unit","is_analysis","labor_rate","material_rate","equipment_rate","misc_rate"):
                if key in body and hasattr(row, key): setattr(row, key, body[key])
            if "unit_price" in body: row.unit_price = float(body["unit_price"])
            current = service.get_resource(_resource_id(resource_id))
            result,status = service.save_resource(_resource_id(resource_id), {
                "code": f"{project.code}:{row.code}", "name": row.c_name, "unit": row.c_unit,
                "unit_price": _text(body.get("unit_price", current["unit_price"] if current else row.unit_price)),
                "price_scale": int(body.get("price_scale", current["price_scale"] if current else 4)),
                "row_version": int(body.get("row_version", current["row_version"] if current else 0)),
            })
            if status >= 400: return jsonify(result),status
            row.unit_price = float(result["unit_price"]); db.commit(); db.refresh(row)
            propagated = lineage.propagate(_resource_id(resource_id)) if body.get("propagate", True) else []
            return jsonify({**serialize(row), "propagated_items": len(propagated)})
        except Exception:
            db.rollback(); raise
        finally: db.close()

    def update_resource_price(project_id, resource_id, user_id):
        body = request.get_json(silent=True) or {}
        if "unit_price" not in body and request.args.get("unit_price") is not None:
            body["unit_price"] = request.args.get("unit_price")
        request._cached_json = (body, body)
        return update_resource(project_id, resource_id, user_id)

    def list_analysis_resources(project_id, user_id):
        db = session_factory()
        try:
            rows = db.query(Resource).filter(Resource.project_id == project_id, Resource.is_analysis == True).all()
            output=[]
            for row in rows:
                item=serialize(row)
                item["breakdown"]=[service.get_breakdown(_breakdown_id(x.id)) for x in db.query(ResourceBreakdownItem).filter(ResourceBreakdownItem.resource_id==row.id).all()]
                output.append(item)
            return jsonify(output)
        finally: db.close()

    def get_resource_breakdown(project_id, resource_id, user_id):
        db=session_factory()
        try:
            rows=db.query(ResourceBreakdownItem).filter(ResourceBreakdownItem.resource_id==resource_id).all()
            return jsonify([service.get_breakdown(_breakdown_id(row.id)) for row in rows])
        finally: db.close()

    def create_resource_breakdown(project_id, resource_id, user_id):
        body=request.get_json(silent=True) or {}; db=session_factory()
        try:
            resource=db.query(Resource).filter(Resource.id==resource_id,Resource.project_id==project_id).first()
            if not resource:return jsonify({"detail":"資源不存在"}),404
            row=ResourceBreakdownItem(resource_id=resource_id,code=body["code"],c_name=body["c_name"],c_unit=body.get("c_unit"),quantity=float(body.get("quantity",0)),unit_price=float(body.get("unit_price",0)))
            db.add(row);db.flush()
            result,status=service.save_breakdown(_breakdown_id(row.id),{"resource_id":_resource_id(resource_id),"code":row.code,"name":row.c_name,"unit":row.c_unit,"quantity":_text(row.quantity),"unit_price":_text(row.unit_price),"quantity_scale":int(body.get("quantity_scale",4)),"price_scale":int(body.get("price_scale",4)),"amount_scale":int(body.get("amount_scale",2)),"row_version":0})
            if status>=400:raise ValueError(result)
            row.amount=float(result["amount"]);resource.unit_price=float(service.get_resource(_resource_id(resource_id))["unit_price"]);db.commit();db.refresh(row)
            lineage.propagate(_resource_id(resource_id),"RESOURCE_BREAKDOWN_CHANGED")
            return jsonify(result),201
        except Exception: db.rollback();raise
        finally: db.close()

    def delete_resource_breakdown(project_id, resource_id, breakdown_id, user_id):
        db=session_factory()
        try:
            row=db.query(ResourceBreakdownItem).filter(ResourceBreakdownItem.id==breakdown_id,ResourceBreakdownItem.resource_id==resource_id).first()
            if not row:return jsonify({"detail":"分析細項不存在"}),404
            db.delete(row)
            with engine.begin() as conn: conn.execute(delete(resource_breakdowns_decimal).where(resource_breakdowns_decimal.c.id==_breakdown_id(breakdown_id)))
            result,status=service.recalculate_resource(_resource_id(resource_id))
            if status>=400:raise ValueError(result)
            resource=db.query(Resource).filter(Resource.id==resource_id).first();resource.unit_price=float(result["unit_price"]);db.commit()
            lineage.propagate(_resource_id(resource_id),"RESOURCE_BREAKDOWN_DELETED")
            return jsonify({"message":"分析細項已刪除","unit_price":result["unit_price"]})
        except Exception:db.rollback();raise
        finally:db.close()

    def recalc_resource_analysis(project_id, user_id):
        db=session_factory();results=[]
        try:
            resources=db.query(Resource).filter(Resource.project_id==project_id,Resource.is_analysis==True).all()
            for row in resources:
                result,status=service.recalculate_resource(_resource_id(row.id))
                if status>=400:raise ValueError(result)
                row.unit_price=float(result["unit_price"])
                propagated=lineage.propagate(_resource_id(row.id),"RESOURCE_ANALYSIS_RECALCULATED")
                results.append({"resource_id":row.id,"unit_price":result["unit_price"],"propagated_items":len(propagated)})
            db.commit();return jsonify({"message":"資源單價分析重新計算完成","resources":results})
        except Exception:db.rollback();raise
        finally:db.close()

    handlers={"list_resources":list_resources,"create_resource":create_resource,"update_resource":update_resource,"update_resource_price":update_resource_price,"list_analysis_resources":list_analysis_resources,"get_resource_breakdown":get_resource_breakdown,"create_resource_breakdown":create_resource_breakdown,"delete_resource_breakdown":delete_resource_breakdown,"recalc_resource_analysis":recalc_resource_analysis}
    for endpoint,handler in handlers.items():
        if endpoint in app.view_functions: app.view_functions[endpoint]=handler
