from flask import Blueprint, jsonify, request

from api.resource_budget_lineage import ResourceBudgetLineageService


def build_resource_budget_lineage_blueprint(service: ResourceBudgetLineageService, resolve_user_id):
    bp = Blueprint("resource_budget_lineage", __name__, url_prefix="/api/decimal-resources")

    @bp.post("/<resource_id>/budget-links")
    def link(resource_id: str):
        if resolve_user_id() is None: return jsonify({"code":"UNAUTHORIZED"}),401
        body=request.get_json(silent=True) or {}
        try:
            result=service.link(str(body.get("project_code","")),resource_id,str(body.get("budget_item_id","")))
        except ValueError as exc:
            return jsonify({"code":"INVALID_ARGUMENT","detail":str(exc)}),400
        return jsonify(result),201

    @bp.post("/<resource_id>/propagate")
    def propagate(resource_id: str):
        if resolve_user_id() is None: return jsonify({"code":"UNAUTHORIZED"}),401
        body=request.get_json(silent=True) or {}
        try:
            rows=service.propagate(resource_id,str(body.get("trigger") or "RESOURCE_PRICE_CHANGED"))
        except ValueError as exc:
            return jsonify({"code":"NOT_FOUND","detail":str(exc)}),404
        return jsonify({"resource_id":resource_id,"updated_items":len(rows),"lineage":rows})

    @bp.get("/projects/<project_code>/lineage")
    def list_lineage(project_code: str):
        if resolve_user_id() is None: return jsonify({"code":"UNAUTHORIZED"}),401
        return jsonify(service.list_project(project_code))

    return bp
