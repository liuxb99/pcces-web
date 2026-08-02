"""BUD/BID modes, ItemA/B/C semantics, explicit cross-project refs and self-checks."""
from __future__ import annotations

from datetime import datetime, timezone
from uuid import uuid4
from flask import Blueprint, jsonify, request
from sqlalchemy import Boolean, Column, DateTime, MetaData, String, Table, Text, and_, select

from api.budget_decimal import budget_items_decimal

metadata = MetaData()
budget_project_modes = Table(
    "budget_project_modes", metadata,
    Column("project_code", String(100), primary_key=True),
    Column("mode", String(10), nullable=False),
    Column("row_version", String(30), nullable=False),
    Column("updated_by", String(100), nullable=False),
    Column("updated_at", DateTime(timezone=True), nullable=False),
)
budget_item_semantics = Table(
    "budget_item_semantics", metadata,
    Column("item_id", String(100), primary_key=True),
    Column("project_code", String(100), nullable=False, index=True),
    Column("item_class", String(10), nullable=False),
    Column("row_version", String(30), nullable=False),
    Column("updated_by", String(100), nullable=False),
    Column("updated_at", DateTime(timezone=True), nullable=False),
)
budget_cross_project_refs = Table(
    "budget_cross_project_refs", metadata,
    Column("id", String(100), primary_key=True),
    Column("source_project_code", String(100), nullable=False, index=True),
    Column("source_item_id", String(100), nullable=False, index=True),
    Column("target_project_code", String(100), nullable=False, index=True),
    Column("target_item_id", String(100), nullable=False, index=True),
    Column("enabled", Boolean, nullable=False),
    Column("created_by", String(100), nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
)
budget_self_check_runs = Table(
    "budget_self_check_runs", metadata,
    Column("id", String(100), primary_key=True),
    Column("project_code", String(100), nullable=False, index=True),
    Column("mode", String(10), nullable=False),
    Column("blocking", Boolean, nullable=False),
    Column("result_json", Text, nullable=False),
    Column("created_by", String(100), nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
)


class BudgetValidationService:
    MODES = {"BUD", "BID"}
    CLASSES = {"A", "B", "C"}

    def __init__(self, engine):
        self.engine = engine
        metadata.create_all(engine)

    def mode(self, project_code: str) -> dict:
        with self.engine.connect() as conn:
            row = conn.execute(select(budget_project_modes).where(budget_project_modes.c.project_code == project_code)).mappings().first()
        return dict(row) if row else {"project_code": project_code, "mode": "BUD", "row_version": "0"}

    def set_mode(self, project_code: str, mode: str, actor: str, row_version: str = "0") -> dict:
        mode = mode.upper()
        if mode not in self.MODES: raise ValueError("mode must be BUD or BID")
        now = datetime.now(timezone.utc)
        with self.engine.begin() as conn:
            current = conn.execute(select(budget_project_modes).where(budget_project_modes.c.project_code == project_code)).mappings().first()
            if current and str(current["row_version"]) != str(row_version): raise RuntimeError("CONFLICT")
            next_version = str(int(current["row_version"]) + 1 if current else 1)
            values = dict(mode=mode,row_version=next_version,updated_by=actor,updated_at=now)
            if current: conn.execute(budget_project_modes.update().where(budget_project_modes.c.project_code==project_code).values(**values))
            else: conn.execute(budget_project_modes.insert().values(project_code=project_code,**values))
        return self.mode(project_code)

    def set_item_class(self, project_code: str, item_id: str, item_class: str, actor: str, row_version: str = "0") -> dict:
        item_class=item_class.upper()
        if item_class not in self.CLASSES: raise ValueError("item_class must be A, B or C")
        now=datetime.now(timezone.utc)
        with self.engine.begin() as conn:
            item=conn.execute(select(budget_items_decimal.c.id).where(and_(budget_items_decimal.c.id==item_id,budget_items_decimal.c.project_code==project_code))).first()
            if not item: raise ValueError("budget item not found")
            current=conn.execute(select(budget_item_semantics).where(budget_item_semantics.c.item_id==item_id)).mappings().first()
            if current and str(current["row_version"]) != str(row_version): raise RuntimeError("CONFLICT")
            next_version=str(int(current["row_version"])+1 if current else 1)
            values=dict(project_code=project_code,item_class=item_class,row_version=next_version,updated_by=actor,updated_at=now)
            if current: conn.execute(budget_item_semantics.update().where(budget_item_semantics.c.item_id==item_id).values(**values))
            else: conn.execute(budget_item_semantics.insert().values(item_id=item_id,**values))
        return {"item_id":item_id,**values,"updated_at":now.isoformat()}

    def add_reference(self, source_project: str, source_item: str, target_project: str, target_item: str, actor: str) -> dict:
        if source_project == target_project and source_item == target_item: raise ValueError("self reference is not allowed")
        now=datetime.now(timezone.utc); ref_id=str(uuid4())
        with self.engine.begin() as conn:
            source=conn.execute(select(budget_items_decimal.c.id).where(and_(budget_items_decimal.c.id==source_item,budget_items_decimal.c.project_code==source_project))).first()
            target=conn.execute(select(budget_items_decimal.c.id).where(and_(budget_items_decimal.c.id==target_item,budget_items_decimal.c.project_code==target_project))).first()
            if not source or not target: raise ValueError("source and target items must exist")
            duplicate=conn.execute(select(budget_cross_project_refs.c.id).where(and_(budget_cross_project_refs.c.source_item_id==source_item,budget_cross_project_refs.c.target_item_id==target_item,budget_cross_project_refs.c.enabled==True))).first()
            if duplicate: return {"id":duplicate[0],"duplicate":True}
            conn.execute(budget_cross_project_refs.insert().values(id=ref_id,source_project_code=source_project,source_item_id=source_item,target_project_code=target_project,target_item_id=target_item,enabled=True,created_by=actor,created_at=now))
        return {"id":ref_id,"source_project_code":source_project,"source_item_id":source_item,"target_project_code":target_project,"target_item_id":target_item,"enabled":True,"created_at":now.isoformat()}

    def check(self, project_code: str, actor: str, blocking: bool = True) -> dict:
        mode=self.mode(project_code)["mode"]
        with self.engine.connect() as conn:
            items=[dict(r) for r in conn.execute(select(budget_items_decimal).where(budget_items_decimal.c.project_code==project_code)).mappings().all()]
            semantics={r["item_id"]:r["item_class"] for r in conn.execute(select(budget_item_semantics).where(budget_item_semantics.c.project_code==project_code)).mappings().all()}
            refs=[dict(r) for r in conn.execute(select(budget_cross_project_refs).where(and_(budget_cross_project_refs.c.source_project_code==project_code,budget_cross_project_refs.c.enabled==True))).mappings().all()]
        issues=[]; ids={r["id"] for r in items}; numbers={}
        for row in items:
            no=(row.get("item_no") or "").strip()
            if no: numbers.setdefault(no,[]).append(row["id"])
            if row.get("parent_id") and row["parent_id"] not in ids: issues.append(self._issue("BROKEN_PARENT",row["id"],True))
            cls=semantics.get(row["id"])
            if cls is None: issues.append(self._issue("ITEM_CLASS_MISSING",row["id"],True))
            if mode=="BID" and cls=="A" and str(row.get("unit_price","0")) in {"0","0.00","0E-8"}: issues.append(self._issue("BID_PRICE_REQUIRED",row["id"],True))
            if mode=="BUD" and row.get("kind") in {"F","S","U"} and not row.get("name"): issues.append(self._issue("CALCULATED_ITEM_NAME_REQUIRED",row["id"],False))
        for no, values in numbers.items():
            if len(values)>1: issues.append({"code":"DUPLICATE_ITEM_NO","item_ids":values,"blocking":True,"detail":no})
        for ref in refs:
            if ref["source_item_id"] not in ids: issues.append(self._issue("BROKEN_SOURCE_REFERENCE",ref["source_item_id"],True))
        result={"project_code":project_code,"mode":mode,"passed":not any(i["blocking"] for i in issues),"blocking_issues":sum(1 for i in issues if i["blocking"]),"warnings":sum(1 for i in issues if not i["blocking"]),"issues":issues}
        run_id=str(uuid4()); now=datetime.now(timezone.utc)
        import json
        with self.engine.begin() as conn:
            conn.execute(budget_self_check_runs.insert().values(id=run_id,project_code=project_code,mode=mode,blocking=blocking,result_json=json.dumps(result,ensure_ascii=False,sort_keys=True),created_by=actor,created_at=now))
        result.update({"id":run_id,"created_at":now.isoformat(),"deep_link":f"/app/projects/by-code/{project_code}/budget-validation?check={run_id}"})
        return result

    @staticmethod
    def _issue(code,item_id,blocking): return {"code":code,"item_id":item_id,"blocking":blocking}


def build_budget_validation_blueprint(service: BudgetValidationService, resolve_user_id):
    bp=Blueprint("budget_validation",__name__,url_prefix="/api/decimal-budget")
    def actor():
        value=resolve_user_id()
        if value is None: raise PermissionError("authentication required")
        return str(value)
    @bp.get("/projects/<project_code>/mode")
    def get_mode(project_code): return jsonify(service.mode(project_code))
    @bp.put("/projects/<project_code>/mode")
    def set_mode(project_code):
        b=request.get_json(silent=True) or {}
        try:return jsonify(service.set_mode(project_code,str(b.get("mode","")),actor(),str(b.get("row_version","0"))))
        except RuntimeError:return jsonify({"code":"CONFLICT"}),409
        except ValueError as e:return jsonify({"code":"INVALID_ARGUMENT","detail":str(e)}),400
    @bp.put("/projects/<project_code>/items/<item_id>/class")
    def set_class(project_code,item_id):
        b=request.get_json(silent=True) or {}
        try:return jsonify(service.set_item_class(project_code,item_id,str(b.get("item_class","")),actor(),str(b.get("row_version","0"))))
        except RuntimeError:return jsonify({"code":"CONFLICT"}),409
        except ValueError as e:return jsonify({"code":"INVALID_ARGUMENT","detail":str(e)}),400
    @bp.post("/cross-project-references")
    def add_ref():
        b=request.get_json(silent=True) or {}
        try:return jsonify(service.add_reference(str(b.get("source_project_code","")),str(b.get("source_item_id","")),str(b.get("target_project_code","")),str(b.get("target_item_id","")),actor())),201
        except ValueError as e:return jsonify({"code":"INVALID_ARGUMENT","detail":str(e)}),400
    @bp.post("/projects/<project_code>/self-check")
    def self_check(project_code):
        b=request.get_json(silent=True) or {}
        result=service.check(project_code,actor(),bool(b.get("blocking",True)))
        return jsonify(result),(200 if result["passed"] else 422)
    return bp
