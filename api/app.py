"""Canonical PCCES Web API entrypoint."""

from __future__ import annotations

from flask import jsonify, request
from sqlalchemy import select

from api.authorization import AuthorizationService, build_authorization_blueprint
from api.bid_lifecycle import BidLifecycleService, build_bid_lifecycle_blueprint
from api.budget_approval import BudgetApprovalService, build_budget_approval_blueprint
from api.budget_approval_guard import install_budget_approval_guard
from api.budget_bid_conversion import BudgetBidConversionService, build_budget_bid_conversion_blueprint
from api.budget_calculation_trace import BudgetTraceService, build_budget_trace_blueprint
from api.budget_cross_project_sync import BudgetCrossProjectSyncService, build_budget_cross_project_blueprint
from api.budget_decimal import BudgetDecimalService, build_budget_decimal_blueprint
from api.budget_lock_guard import install_budget_lock_guard
from api.budget_submission_gate import install_budget_submission_gate
from api.budget_validation import BudgetValidationService, build_budget_validation_blueprint
from api.budget_versioning import BudgetVersionService, build_budget_version_blueprint
from api.conversion_export_jobs import ConversionExportJobService, build_conversion_export_job_blueprint
from api.conversion_wizard import ConversionWizardService, build_conversion_wizard_blueprint
from api.cost_structure import CostStructureService, build_cost_structure_blueprint
from api.cost_structure_calculation import build_cost_structure_calculation_blueprint
from api.cost_structure_details import CostStructureDetailService, build_cost_structure_detail_blueprint
from api.cost_structure_project_run import ProjectCostStructureRunService, build_project_cost_structure_run_blueprint
from api.cost_structure_run_versions import CostStructureRunVersionService, build_cost_structure_run_version_blueprint
from api.index import SessionLocal, app, decode_token, engine
from api.legacy_budget_decimal_bridge import install_legacy_budget_bridge
from api.legacy_resource_decimal_bridge import install_legacy_resource_bridge
from api.migrations import run_migrations
from api.models import Base, User
from api.mrs_catalog import MRSCatalogService, build_mrs_catalog_blueprint
from api.mrs_code import MRSCodeService, build_mrs_code_blueprint
from api.mrs_exchange import MRSExchangeService, build_mrs_exchange_blueprint
from api.mrs_governance_paging import MRSGovernanceService, build_mrs_governance_blueprint
from api.mrs_history_apply import MRSHistoryApplyService, build_mrs_history_apply_blueprint
from api.mrs_intelligence import MRSIntelligenceService, build_mrs_intelligence_blueprint
from api.mrs_operations import MRSOperationsService, build_mrs_operations_blueprint
from api.persistence_contract import PersistenceService
from api.recovery import RecoveryService, build_recovery_blueprint
from api.resource_budget_lineage import ResourceBudgetLineageService
from api.resource_budget_lineage_api import build_resource_budget_lineage_blueprint
from api.resource_budget_links import ResourceBudgetLinkService, build_resource_budget_links_blueprint
from api.resource_decimal import ResourceDecimalService, build_resource_decimal_blueprint
from api.resource_dependency_graph import ResourceDependencyGraphService, build_resource_dependency_blueprint, install_resource_automation
from api.route_policy import action_for_request, initialize_authorization
from api.work_context import WorkContextService, build_work_context_blueprint

_PUBLIC_ENDPOINTS = {"/api/health", "/api/auth/login", "/api/auth/register"}


def resolve_user_id() -> int | None:
    auth = request.headers.get("Authorization", "")
    if not auth.startswith("Bearer "):
        return None
    payload = decode_token(auth[7:])
    if not payload:
        return None
    try:
        return int(payload["sub"])
    except (KeyError, TypeError, ValueError):
        return None


Base.metadata.create_all(engine)
run_migrations(engine)
authorization_service = AuthorizationService(engine)
with engine.connect() as connection:
    existing_user_ids = [row[0] for row in connection.execute(select(User.id))]
initialize_authorization(authorization_service, existing_user_ids)
work_context_service = WorkContextService(engine)
recovery_service = RecoveryService(engine, work_context_service)
persistence_service = PersistenceService(engine)
budget_decimal_service = BudgetDecimalService(engine)
budget_decimal_service.create_schema()
budget_version_service = BudgetVersionService(engine)
budget_approval_service = BudgetApprovalService(engine, SessionLocal, budget_version_service)
budget_validation_service = BudgetValidationService(engine)
budget_cross_project_service = BudgetCrossProjectSyncService(engine)
bid_lifecycle_service = BidLifecycleService(engine)
budget_bid_conversion_service = BudgetBidConversionService(engine)
conversion_wizard_service = ConversionWizardService(engine)
conversion_export_job_service = ConversionExportJobService(engine)
mrs_catalog_service = MRSCatalogService(engine)
mrs_code_service = MRSCodeService()
mrs_exchange_service = MRSExchangeService(mrs_catalog_service)
mrs_intelligence_service = MRSIntelligenceService(engine)
mrs_operations_service = MRSOperationsService(engine, mrs_catalog_service, mrs_exchange_service)
mrs_governance_service = MRSGovernanceService(engine)
mrs_history_apply_service = MRSHistoryApplyService(engine)
resource_decimal_service = ResourceDecimalService(engine)
resource_decimal_service.create_schema()
resource_budget_lineage_service = ResourceBudgetLineageService(engine)
resource_budget_link_service = ResourceBudgetLinkService(engine)
resource_dependency_graph_service = ResourceDependencyGraphService(engine, SessionLocal)
budget_trace_service = BudgetTraceService(engine)
cost_structure_service = CostStructureService(engine)
cost_structure_detail_service = CostStructureDetailService(engine)
project_cost_structure_run_service = ProjectCostStructureRunService(engine)
cost_structure_run_version_service = CostStructureRunVersionService(engine)

install_legacy_budget_bridge(app, engine, SessionLocal)
for _endpoint in ("create_budget_item", "get_budget_list", "get_budget_tree", "recalc_budget"):
    _view = app.view_functions[_endpoint]
    app.view_functions[_endpoint] = lambda project_id, _view=_view: _view(project_id, resolve_user_id())
for _endpoint in ("update_budget_item", "delete_budget_item", "move_budget_item"):
    _view = app.view_functions[_endpoint]
    app.view_functions[_endpoint] = lambda project_id, item_id, _view=_view: _view(project_id, item_id, resolve_user_id())

install_legacy_resource_bridge(app, engine, SessionLocal)
for _endpoint in ("list_resources", "create_resource", "list_analysis_resources", "recalc_resource_analysis"):
    if _endpoint in app.view_functions:
        _view = app.view_functions[_endpoint]
        app.view_functions[_endpoint] = lambda project_id, _view=_view: _view(project_id, resolve_user_id())
for _endpoint in ("update_resource", "update_resource_price", "get_resource_breakdown", "create_resource_breakdown"):
    if _endpoint in app.view_functions:
        _view = app.view_functions[_endpoint]
        app.view_functions[_endpoint] = lambda project_id, resource_id, _view=_view: _view(project_id, resource_id, resolve_user_id())
if "delete_resource_breakdown" in app.view_functions:
    _view = app.view_functions["delete_resource_breakdown"]
    app.view_functions["delete_resource_breakdown"] = lambda project_id, resource_id, breakdown_id, _view=_view: _view(project_id, resource_id, breakdown_id, resolve_user_id())

install_resource_automation(app, resource_dependency_graph_service)
install_budget_lock_guard(app, budget_version_service, SessionLocal)
install_budget_approval_guard(app, budget_approval_service, SessionLocal)


@app.before_request
def enforce_canonical_authentication_and_capability():
    if request.method == "OPTIONS":
        return None
    if not request.path.startswith("/api/") or request.path in _PUBLIC_ENDPOINTS:
        return None
    user_id = resolve_user_id()
    if user_id is None:
        return jsonify({"code": "UNAUTHORIZED", "detail": "authentication required", "feature_id": "P0-S3"}), 401
    action_code = action_for_request(request.path, request.method)
    if action_code is None:
        return None
    decision = authorization_service.decide(user_id, action_code)
    if not decision.allowed:
        return jsonify({"code": "FORBIDDEN", "action_code": action_code, "reason": decision.reason, "feature_id": "P0-S3"}), 403
    return None


def register(name, blueprint):
    if name not in app.blueprints:
        app.register_blueprint(blueprint)


register("authorization", build_authorization_blueprint(authorization_service, resolve_user_id))
register("work_context", build_work_context_blueprint(work_context_service, resolve_user_id))
register("recovery", build_recovery_blueprint(recovery_service, resolve_user_id))
register("budget_decimal", build_budget_decimal_blueprint(budget_decimal_service, resolve_user_id))
register("budget_versions", build_budget_version_blueprint(budget_version_service, resolve_user_id))
register("budget_approval", build_budget_approval_blueprint(budget_approval_service, resolve_user_id))
register("budget_validation", build_budget_validation_blueprint(budget_validation_service, resolve_user_id))
register("budget_cross_project", build_budget_cross_project_blueprint(budget_cross_project_service, resolve_user_id))
register("bid_lifecycle", build_bid_lifecycle_blueprint(bid_lifecycle_service, resolve_user_id))
register("budget_bid_conversion", build_budget_bid_conversion_blueprint(budget_bid_conversion_service, resolve_user_id))
register("conversion_wizard", build_conversion_wizard_blueprint(conversion_wizard_service, resolve_user_id))
register("conversion_export_jobs", build_conversion_export_job_blueprint(conversion_export_job_service, resolve_user_id))
register("mrs_catalog", build_mrs_catalog_blueprint(mrs_catalog_service, resolve_user_id))
register("mrs_code", build_mrs_code_blueprint(mrs_code_service, resolve_user_id))
register("mrs_exchange", build_mrs_exchange_blueprint(mrs_exchange_service, resolve_user_id))
register("mrs_intelligence", build_mrs_intelligence_blueprint(mrs_intelligence_service, resolve_user_id))
register("mrs_operations", build_mrs_operations_blueprint(mrs_operations_service, resolve_user_id))
register("mrs_governance", build_mrs_governance_blueprint(mrs_governance_service, resolve_user_id))
register("mrs_history_apply", build_mrs_history_apply_blueprint(mrs_history_apply_service, resolve_user_id))
register("resource_decimal", build_resource_decimal_blueprint(resource_decimal_service, resolve_user_id))
register("resource_budget_lineage", build_resource_budget_lineage_blueprint(resource_budget_lineage_service, resolve_user_id))
register("resource_budget_links", build_resource_budget_links_blueprint(resource_budget_link_service, resolve_user_id))
register("resource_dependency", build_resource_dependency_blueprint(resource_dependency_graph_service, resolve_user_id))
register("budget_trace", build_budget_trace_blueprint(budget_trace_service, resolve_user_id))
register("cost_structure", build_cost_structure_blueprint(cost_structure_service, resolve_user_id))
register("cost_structure_details", build_cost_structure_detail_blueprint(cost_structure_detail_service, resolve_user_id))
register("cost_structure_calculation", build_cost_structure_calculation_blueprint(resolve_user_id))
register("project_cost_structure_run", build_project_cost_structure_run_blueprint(project_cost_structure_run_service, resolve_user_id))
register("cost_structure_run_versions", build_cost_structure_run_version_blueprint(cost_structure_run_version_service, resolve_user_id))

install_budget_submission_gate(app, budget_validation_service, resolve_user_id)

__all__ = [
    "app", "authorization_service", "work_context_service", "recovery_service", "persistence_service",
    "budget_decimal_service", "budget_version_service", "budget_approval_service", "budget_validation_service",
    "budget_cross_project_service", "bid_lifecycle_service", "budget_bid_conversion_service",
    "conversion_wizard_service", "conversion_export_job_service", "mrs_catalog_service", "mrs_code_service",
    "mrs_exchange_service", "mrs_intelligence_service", "mrs_operations_service", "mrs_governance_service",
    "mrs_history_apply_service", "resource_decimal_service", "resource_budget_lineage_service",
    "resource_budget_link_service", "resource_dependency_graph_service", "budget_trace_service",
    "cost_structure_service", "cost_structure_detail_service", "project_cost_structure_run_service",
    "cost_structure_run_version_service", "resolve_user_id",
]
