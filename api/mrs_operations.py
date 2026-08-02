"""Phase 3 MRS operations: usage aggregation, recipe versions, source lineage and import jobs."""
from __future__ import annotations

import json
from datetime import datetime, timezone
from uuid import uuid4

from flask import Blueprint, jsonify, request
from sqlalchemy import Boolean, Column, DateTime, Integer, MetaData, String, Table, Text, select

from api.decimal_math import multiply, sum_values
from api.mrs_catalog import mrs_analysis_components, mrs_analysis_recipes, mrs_catalog_items, mrs_price_history
from api.mrs_intelligence import mrs_price_quotes

metadata = MetaData()
mrs_recipe_versions = Table("mrs_recipe_versions", metadata,
    Column("id", String(100), primary_key=True), Column("recipe_id", String(100), nullable=False, index=True),
    Column("label", String(300), nullable=False), Column("unit_price", String(100), nullable=False),
    Column("snapshot_json", Text, nullable=False), Column("created_by", String(100), nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False))
mrs_import_jobs = Table("mrs_import_jobs", metadata,
    Column("id", String(100), primary_key=True), Column("format", String(20), nullable=False),
    Column("payload", Text, nullable=False), Column("overwrite", Boolean, nullable=False),
    Column("status", String(30), nullable=False), Column("total_rows", Integer, nullable=False),
    Column("processed_rows", Integer, nullable=False), Column("imported_rows", Integer, nullable=False),
    Column("skipped_rows", Integer, nullable=False), Column("errors_json", Text, nullable=False),
    Column("cancel_requested", Boolean, nullable=False), Column("created_by", String(100), nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False), Column("updated_at", DateTime(timezone=True), nullable=False))

class MRSOperationsService:
    def __init__(self, engine, catalog_service, exchange_service):
        self.engine, self.catalog, self.exchange = engine, catalog_service, exchange_service
        metadata.create_all(engine)

    def usage_summary(self):
        with self.engine.connect() as conn:
            rows=conn.execute(select(mrs_analysis_components.c.catalog_item_id,mrs_analysis_components.c.quantity,
                mrs_catalog_items.c.code,mrs_catalog_items.c.name,mrs_catalog_items.c.category,
                mrs_catalog_items.c.current_price,mrs_analysis_recipes.c.id.label("recipe_id"))
                .join(mrs_catalog_items,mrs_catalog_items.c.id==mrs_analysis_components.c.catalog_item_id)
                .join(mrs_analysis_recipes,mrs_analysis_recipes.c.id==mrs_analysis_components.c.recipe_id)
                .order_by(mrs_catalog_items.c.code)).mappings().all()
        grouped={}
        for row in rows:
            item=grouped.setdefault(row["catalog_item_id"],{"catalog_item_id":row["catalog_item_id"],"code":row["code"],"name":row["name"],"category":row["category"],"recipe_ids":[],"usage_count":0,"total_quantity":"0.0000","estimated_amount":"0.00"})
            item["recipe_ids"].append(row["recipe_id"]);item["usage_count"]+=1
            item["total_quantity"]=sum_values([item["total_quantity"],row["quantity"]],4)
            item["estimated_amount"]=sum_values([item["estimated_amount"],multiply(row["quantity"],row["current_price"],2)],2)
        items=list(grouped.values())
        return {"catalog_items":len(items),"recipe_links":sum(x["usage_count"] for x in items),"estimated_amount":sum_values([x["estimated_amount"] for x in items],2),"items":items}

    def create_recipe_version(self, recipe_id, label, actor):
        snapshot=self.catalog.calculate_recipe(recipe_id);version_id,now=str(uuid4()),datetime.now(timezone.utc)
        with self.engine.begin() as conn:
            conn.execute(mrs_recipe_versions.insert().values(id=version_id,recipe_id=recipe_id,label=label or "MRS recipe version",unit_price=snapshot["unit_price"],snapshot_json=json.dumps(snapshot,ensure_ascii=False,sort_keys=True),created_by=actor,created_at=now))
        return self.get_recipe_version(version_id)

    def get_recipe_version(self, version_id):
        with self.engine.connect() as conn: row=conn.execute(select(mrs_recipe_versions).where(mrs_recipe_versions.c.id==version_id)).mappings().first()
        if not row: raise LookupError("recipe version not found")
        result=dict(row);result["snapshot"]=json.loads(row["snapshot_json"]);result["created_at"]=row["created_at"].isoformat();result["deep_link"]=f"/app/mrs-operations?recipe={row['recipe_id']}&version={row['id']}";return result

    def list_recipe_versions(self, recipe_id):
        with self.engine.connect() as conn: rows=conn.execute(select(mrs_recipe_versions.c.id).where(mrs_recipe_versions.c.recipe_id==recipe_id).order_by(mrs_recipe_versions.c.created_at.desc())).all()
        return [self.get_recipe_version(row[0]) for row in rows]

    def diff_recipe_versions(self,left_id,right_id):
        left,right=self.get_recipe_version(left_id),self.get_recipe_version(right_id)
        a={x["catalog_item_id"]:x for x in left["snapshot"]["components"]};b={x["catalog_item_id"]:x for x in right["snapshot"]["components"]}
        added=[];removed=[];changed=[]
        for key in sorted(set(a)|set(b)):
            if key not in a: added.append(b[key])
            elif key not in b: removed.append(a[key])
            elif a[key]!=b[key]: changed.append({"catalog_item_id":key,"before":a[key],"after":b[key]})
        return {"left_version_id":left_id,"right_version_id":right_id,"left_unit_price":left["unit_price"],"right_unit_price":right["unit_price"],"difference":sum_values([right["unit_price"],f"-{left['unit_price']}"],2),"added":added,"removed":removed,"changed":changed}

    def price_lineage(self,item_id):
        item=self.catalog.get_item(item_id)
        with self.engine.connect() as conn:
            history=[dict(r) for r in conn.execute(select(mrs_price_history).where(mrs_price_history.c.catalog_item_id==item_id).order_by(mrs_price_history.c.created_at)).mappings().all()]
            quotes=[dict(r) for r in conn.execute(select(mrs_price_quotes).where(mrs_price_quotes.c.catalog_item_id==item_id).order_by(mrs_price_quotes.c.created_at)).mappings().all()]
        events=[]
        for row in history: events.append({"type":"PRICE_HISTORY","id":row["id"],"old_price":row["old_price"],"new_price":row["new_price"],"source":row["source"],"effective_date":row["effective_date"],"created_at":row["created_at"].isoformat()})
        for row in quotes: events.append({"type":"SUPPLIER_QUOTE","id":row["id"],"vendor":row["vendor"],"quoted_price":row["quoted_price"],"source_document":row["source_document"],"effective_date":row["effective_date"],"created_at":row["created_at"].isoformat()})
        events.sort(key=lambda x:x["created_at"])
        return {"catalog_item":item,"events":events,"deep_link":f"/app/mrs-operations?item={item_id}&lineage=1"}

    def create_import_job(self,payload,fmt,overwrite,actor):
        rows=self._parse_rows(payload,fmt.lower());job_id,now=str(uuid4()),datetime.now(timezone.utc)
        with self.engine.begin() as conn: conn.execute(mrs_import_jobs.insert().values(id=job_id,format=fmt.upper(),payload=payload,overwrite=overwrite,status="PENDING",total_rows=len(rows),processed_rows=0,imported_rows=0,skipped_rows=0,errors_json="[]",cancel_requested=False,created_by=actor,created_at=now,updated_at=now))
        return self.get_import_job(job_id)

    def run_import_job(self,job_id):
        job=self.get_import_job(job_id)
        if job["status"] not in {"PENDING","RUNNING"}: raise ValueError("import job is terminal")
        if job["cancel_requested"]: return self._finish_job(job_id,"CANCELLED",0,0,0,[])
        with self.engine.begin() as conn: conn.execute(mrs_import_jobs.update().where(mrs_import_jobs.c.id==job_id).values(status="RUNNING",updated_at=datetime.now(timezone.utc)))
        result=self.exchange.import_payload(job["payload"],job["format"],job["created_by"],job["overwrite"])
        return self._finish_job(job_id,"COMPLETED" if not result["errors"] else "COMPLETED_WITH_ERRORS",job["total_rows"],result["imported"],result["skipped"],result["errors"])

    def cancel_import_job(self,job_id):
        job=self.get_import_job(job_id)
        if job["status"] not in {"PENDING","RUNNING"}: raise ValueError("import job is terminal")
        with self.engine.begin() as conn: conn.execute(mrs_import_jobs.update().where(mrs_import_jobs.c.id==job_id).values(cancel_requested=True,status="CANCELLED",updated_at=datetime.now(timezone.utc)))
        return self.get_import_job(job_id)

    def get_import_job(self,job_id):
        with self.engine.connect() as conn: row=conn.execute(select(mrs_import_jobs).where(mrs_import_jobs.c.id==job_id)).mappings().first()
        if not row: raise LookupError("import job not found")
        result=dict(row);result["errors"]=json.loads(row["errors_json"]);result["created_at"]=row["created_at"].isoformat();result["updated_at"]=row["updated_at"].isoformat();result["deep_link"]=f"/app/mrs-operations?job={job_id}";return result

    def _finish_job(self,job_id,status,processed,imported,skipped,errors):
        with self.engine.begin() as conn: conn.execute(mrs_import_jobs.update().where(mrs_import_jobs.c.id==job_id).values(status=status,processed_rows=processed,imported_rows=imported,skipped_rows=skipped,errors_json=json.dumps(errors,ensure_ascii=False),updated_at=datetime.now(timezone.utc)))
        return self.get_import_job(job_id)

    @staticmethod
    def _parse_rows(payload,fmt):
        if fmt=="json":
            rows=json.loads(payload)
            if not isinstance(rows,list): raise ValueError("import payload must contain a list")
            return rows
        if fmt=="csv":
            import csv,io
            return list(csv.DictReader(io.StringIO(payload)))
        raise ValueError("format must be json or csv")

def build_mrs_operations_blueprint(service,resolve_user_id):
    bp=Blueprint("mrs_operations",__name__,url_prefix="/api/mrs")
    def actor():
        value=resolve_user_id()
        if value is None: raise PermissionError("authentication required")
        return str(value)
    @bp.get("/usage-summary")
    def usage_summary(): return jsonify(service.usage_summary())
    @bp.post("/analysis-recipes/<recipe_id>/versions")
    def create_version(recipe_id):
        body=request.get_json(silent=True) or {}
        try:return jsonify(service.create_recipe_version(recipe_id,str(body.get("label","")),actor())),201
        except LookupError as exc:return jsonify({"code":"NOT_FOUND","detail":str(exc)}),404
    @bp.get("/analysis-recipes/<recipe_id>/versions")
    def list_versions(recipe_id):return jsonify(service.list_recipe_versions(recipe_id))
    @bp.get("/analysis-recipe-versions/<left_id>/diff/<right_id>")
    def diff_versions(left_id,right_id):
        try:return jsonify(service.diff_recipe_versions(left_id,right_id))
        except LookupError as exc:return jsonify({"code":"NOT_FOUND","detail":str(exc)}),404
    @bp.get("/catalog/<item_id>/lineage")
    def lineage(item_id):
        try:return jsonify(service.price_lineage(item_id))
        except LookupError as exc:return jsonify({"code":"NOT_FOUND","detail":str(exc)}),404
    @bp.post("/import-jobs")
    def create_job():
        body=request.get_json(silent=True) or {}
        try:return jsonify(service.create_import_job(str(body.get("payload","")),str(body.get("format","json")),bool(body.get("overwrite",False)),actor())),201
        except (ValueError,json.JSONDecodeError) as exc:return jsonify({"code":"INVALID_ARGUMENT","detail":str(exc)}),400
    @bp.get("/import-jobs/<job_id>")
    def get_job(job_id):
        try:return jsonify(service.get_import_job(job_id))
        except LookupError as exc:return jsonify({"code":"NOT_FOUND","detail":str(exc)}),404
    @bp.post("/import-jobs/<job_id>/run")
    def run_job(job_id):
        try:return jsonify(service.run_import_job(job_id))
        except (LookupError,ValueError) as exc:return jsonify({"code":"INVALID_STATE","detail":str(exc)}),409
    @bp.post("/import-jobs/<job_id>/cancel")
    def cancel_job(job_id):
        try:return jsonify(service.cancel_import_job(job_id))
        except (LookupError,ValueError) as exc:return jsonify({"code":"INVALID_STATE","detail":str(exc)}),409
    return bp
