"""Phase 8 typed settings, users/groups, backup runs and health/audit console."""
from __future__ import annotations

import hashlib, json, os, sqlite3, tempfile
from datetime import datetime, timezone
from uuid import uuid4

from flask import Blueprint, Response, jsonify, request
from sqlalchemy import Column, DateTime, Integer, LargeBinary, MetaData, String, Table, Text, and_, select, update

metadata=MetaData()
setting_definitions=Table("setting_definitions",metadata,Column("key",String(150),primary_key=True),Column("category",String(50),nullable=False),Column("value_type",String(30),nullable=False),Column("default_json",Text,nullable=False),Column("constraints_json",Text,nullable=False),Column("version",Integer,nullable=False),Column("description",Text))
setting_values=Table("setting_values",metadata,Column("key",String(150),primary_key=True),Column("value_json",Text,nullable=False),Column("updated_by",String(100),nullable=False),Column("updated_at",DateTime(timezone=True),nullable=False),Column("row_version",Integer,nullable=False,default=1))
admin_groups=Table("admin_groups",metadata,Column("id",String(100),primary_key=True),Column("code",String(100),nullable=False,unique=True),Column("name",String(300),nullable=False),Column("created_by",String(100),nullable=False),Column("created_at",DateTime(timezone=True),nullable=False))
admin_group_members=Table("admin_group_members",metadata,Column("group_id",String(100),primary_key=True),Column("user_id",String(100),primary_key=True),Column("created_at",DateTime(timezone=True),nullable=False))
backup_runs=Table("backup_runs",metadata,Column("id",String(100),primary_key=True),Column("status",String(30),nullable=False),Column("database_url",Text,nullable=False),Column("sha256",String(64)),Column("size_bytes",Integer),Column("artifact",LargeBinary),Column("precheck_json",Text,nullable=False),Column("smoke_json",Text),Column("created_by",String(100),nullable=False),Column("created_at",DateTime(timezone=True),nullable=False),Column("completed_at",DateTime(timezone=True)),Column("row_version",Integer,nullable=False,default=1))
admin_audit=Table("admin_audit",metadata,Column("id",String(100),primary_key=True),Column("actor",String(100),nullable=False),Column("action",String(150),nullable=False),Column("target",String(300)),Column("payload_json",Text,nullable=False),Column("created_at",DateTime(timezone=True),nullable=False))
DEFAULT_SETTINGS=[("autosave.interval_seconds","general","integer",30,{"min":5,"max":3600},"自動儲存間隔"),("calculation.amount_decimals","budget","integer",2,{"min":0,"max":8},"金額精度"),("reports.retention_days","report","integer",365,{"min":1,"max":3650},"報表保留天數"),("external.proxy_url","integration","string","",{"max_length":500},"外部服務 Proxy"),("modules.addon_enabled","integration","boolean",False,{},"Add-on 啟用")]

class AdminConsoleService:
 def __init__(self,engine,database_url):
  self.engine,self.database_url=engine,database_url;metadata.create_all(engine)
  with engine.begin() as c:
   for key,cat,typ,default,constraints,desc in DEFAULT_SETTINGS:
    if not c.execute(select(setting_definitions.c.key).where(setting_definitions.c.key==key)).first():c.execute(setting_definitions.insert().values(key=key,category=cat,value_type=typ,default_json=json.dumps(default),constraints_json=json.dumps(constraints),version=1,description=desc))
 def _audit(self,c,actor,action,target,payload):c.execute(admin_audit.insert().values(id=str(uuid4()),actor=actor,action=action,target=target,payload_json=json.dumps(payload,ensure_ascii=False,sort_keys=True),created_at=datetime.now(timezone.utc)))
 def settings(self):
  with self.engine.connect() as c:defs=c.execute(select(setting_definitions).order_by(setting_definitions.c.key)).mappings().all();vals={r["key"]:r for r in c.execute(select(setting_values)).mappings().all()}
  return [{"key":d["key"],"category":d["category"],"value_type":d["value_type"],"value":json.loads(vals[d["key"]]["value_json"]) if d["key"] in vals else json.loads(d["default_json"]),"default":json.loads(d["default_json"]),"constraints":json.loads(d["constraints_json"]),"definition_version":d["version"],"row_version":vals[d["key"]]["row_version"] if d["key"] in vals else 0} for d in defs]
 def set_setting(self,key,body,actor):
  with self.engine.begin() as c:
   d=c.execute(select(setting_definitions).where(setting_definitions.c.key==key)).mappings().first()
   if not d:raise LookupError("setting definition not found")
   value=body.get("value");self._validate(value,d["value_type"],json.loads(d["constraints_json"]));existing=c.execute(select(setting_values).where(setting_values.c.key==key)).mappings().first();expected=int(body.get("row_version",0));now=datetime.now(timezone.utc)
   if existing:
    if existing["row_version"]!=expected:raise RuntimeError("row version conflict")
    c.execute(update(setting_values).where(and_(setting_values.c.key==key,setting_values.c.row_version==expected)).values(value_json=json.dumps(value),updated_by=actor,updated_at=now,row_version=expected+1))
   else:
    if expected!=0:raise RuntimeError("row version conflict")
    c.execute(setting_values.insert().values(key=key,value_json=json.dumps(value),updated_by=actor,updated_at=now,row_version=1))
   self._audit(c,actor,"SETTING_UPDATE",key,{"value":value})
  return next(x for x in self.settings() if x["key"]==key)
 def _validate(self,value,typ,constraints):
  if typ=="integer" and (isinstance(value,bool) or not isinstance(value,int)):raise ValueError("value must be integer")
  if typ=="boolean" and not isinstance(value,bool):raise ValueError("value must be boolean")
  if typ=="string" and not isinstance(value,str):raise ValueError("value must be string")
  if "min" in constraints and value<constraints["min"]:raise ValueError("value below minimum")
  if "max" in constraints and value>constraints["max"]:raise ValueError("value above maximum")
  if "max_length" in constraints and len(value)>constraints["max_length"]:raise ValueError("value too long")
 def create_group(self,body,actor):
  code,name=str(body.get("code","")).strip(),str(body.get("name","")).strip()
  if not code or not name:raise ValueError("code and name are required")
  gid,now=str(uuid4()),datetime.now(timezone.utc)
  with self.engine.begin() as c:c.execute(admin_groups.insert().values(id=gid,code=code,name=name,created_by=actor,created_at=now));self._audit(c,actor,"GROUP_CREATE",gid,body)
  return {"id":gid,"code":code,"name":name}
 def add_member(self,gid,user_id,actor):
  with self.engine.begin() as c:
   if not c.execute(select(admin_groups.c.id).where(admin_groups.c.id==gid)).first():raise LookupError("group not found")
   if not c.execute(select(admin_group_members.c.group_id).where(and_(admin_group_members.c.group_id==gid,admin_group_members.c.user_id==user_id))).first():c.execute(admin_group_members.insert().values(group_id=gid,user_id=user_id,created_at=datetime.now(timezone.utc)))
   self._audit(c,actor,"GROUP_MEMBER_ADD",gid,{"user_id":user_id})
  return {"group_id":gid,"user_id":user_id}
 def backup(self,actor):
  rid,now=str(uuid4()),datetime.now(timezone.utc);pre={"database_url_present":bool(self.database_url),"supported":self.database_url.startswith("sqlite:///")}
  with self.engine.begin() as c:c.execute(backup_runs.insert().values(id=rid,status="RUNNING",database_url=self.database_url,precheck_json=json.dumps(pre),created_by=actor,created_at=now,row_version=1))
  try:
   if not pre["supported"]:raise RuntimeError("automatic artifact backup currently supports sqlite only")
   path=self.database_url.replace("sqlite:///", "",1);content=open(path,"rb").read() if os.path.exists(path) else b"";digest=hashlib.sha256(content).hexdigest();smoke={"sqlite_header":content.startswith(b"SQLite format 3") or len(content)==0,"size_bytes":len(content)}
   with self.engine.begin() as c:c.execute(update(backup_runs).where(backup_runs.c.id==rid).values(status="COMPLETED",sha256=digest,size_bytes=len(content),artifact=content,smoke_json=json.dumps(smoke),completed_at=datetime.now(timezone.utc),row_version=2));self._audit(c,actor,"BACKUP_COMPLETE",rid,smoke)
  except Exception as exc:
   with self.engine.begin() as c:c.execute(update(backup_runs).where(backup_runs.c.id==rid).values(status="FAILED",smoke_json=json.dumps({"error":str(exc)}),completed_at=datetime.now(timezone.utc),row_version=2))
  return self.get_backup(rid)
 def get_backup(self,rid):
  with self.engine.connect() as c:r=c.execute(select(backup_runs).where(backup_runs.c.id==rid)).mappings().first()
  if not r:raise LookupError("backup run not found")
  return {"id":r["id"],"status":r["status"],"sha256":r["sha256"],"size_bytes":r["size_bytes"],"precheck":json.loads(r["precheck_json"]),"smoke":json.loads(r["smoke_json"]) if r["smoke_json"] else None,"row_version":r["row_version"],"download_url":f"/api/admin/backups/{rid}/download" if r["status"]=="COMPLETED" else None}
 def backup_artifact(self,rid):
  with self.engine.connect() as c:r=c.execute(select(backup_runs.c.artifact).where(backup_runs.c.id==rid)).first()
  if not r or r[0] is None:raise LookupError("backup artifact not found")
  return bytes(r[0])
 def health(self):
  with self.engine.connect() as c:c.execute(select(1)).scalar_one();tables=len(metadata.tables)
  return {"status":"ok","database":"reachable","admin_schema_tables":tables,"timestamp":datetime.now(timezone.utc).isoformat()}

def build_admin_console_blueprint(service,resolve_user_id):
 bp=Blueprint("admin_console",__name__,url_prefix="/api/admin")
 def actor():
  x=resolve_user_id()
  if x is None:raise PermissionError("authentication required")
  return str(x)
 @bp.get("/settings")
 def settings():return jsonify(service.settings())
 @bp.put("/settings/<path:key>")
 def set_setting(key):
  try:return jsonify(service.set_setting(key,request.get_json(silent=True) or {},actor()))
  except Exception as e:return _err(e)
 @bp.post("/groups")
 def group():
  try:return jsonify(service.create_group(request.get_json(silent=True) or {},actor())),201
  except Exception as e:return _err(e)
 @bp.put("/groups/<gid>/members/<uid>")
 def member(gid,uid):
  try:return jsonify(service.add_member(gid,uid,actor()))
  except Exception as e:return _err(e)
 @bp.post("/backups")
 def backup():
  try:return jsonify(service.backup(actor())),201
  except Exception as e:return _err(e)
 @bp.get("/backups/<rid>")
 def get_backup(rid):
  try:return jsonify(service.get_backup(rid))
  except Exception as e:return _err(e)
 @bp.get("/backups/<rid>/download")
 def download(rid):
  try:return Response(service.backup_artifact(rid),content_type="application/octet-stream",headers={"Content-Disposition":f'attachment; filename="pcces-backup-{rid}.db"'})
  except Exception as e:return _err(e)
 @bp.get("/health")
 def health():return jsonify(service.health())
 return bp

def _err(e):
 if isinstance(e,LookupError):return jsonify({"code":"NOT_FOUND","detail":str(e)}),404
 if isinstance(e,PermissionError):return jsonify({"code":"UNAUTHORIZED","detail":str(e)}),401
 if isinstance(e,RuntimeError):return jsonify({"code":"CONFLICT","detail":str(e)}),409
 return jsonify({"code":"INVALID_ARGUMENT","detail":str(e)}),400
