"""Phase 7 snapshot-based report catalog, jobs and immutable artifacts."""
from __future__ import annotations

import csv, hashlib, io, json, zipfile
from datetime import datetime, timezone
from uuid import uuid4

from flask import Blueprint, Response, jsonify, request
from sqlalchemy import Column, DateTime, Integer, LargeBinary, MetaData, String, Table, Text, and_, select, update

metadata=MetaData()
report_definitions=Table("report_definitions",metadata,Column("code",String(100),primary_key=True),Column("name",String(300),nullable=False),Column("business_type",String(50),nullable=False),Column("template_version",String(30),nullable=False),Column("legacy_entry",String(500)),Column("schema_json",Text,nullable=False),Column("enabled",Integer,nullable=False,default=1))
report_jobs=Table("report_jobs",metadata,Column("id",String(100),primary_key=True),Column("definition_code",String(100),nullable=False),Column("project_code",String(100),nullable=False,index=True),Column("business_version_id",String(100),nullable=False,index=True),Column("format",String(20),nullable=False),Column("status",String(20),nullable=False),Column("progress",Integer,nullable=False),Column("parameters_json",Text,nullable=False),Column("snapshot_json",Text,nullable=False),Column("error_json",Text),Column("created_by",String(100),nullable=False),Column("created_at",DateTime(timezone=True),nullable=False),Column("updated_at",DateTime(timezone=True),nullable=False),Column("row_version",Integer,nullable=False,default=1))
report_artifacts=Table("report_artifacts",metadata,Column("id",String(100),primary_key=True),Column("job_id",String(100),nullable=False,index=True),Column("filename",String(300),nullable=False),Column("content_type",String(100),nullable=False),Column("size_bytes",Integer,nullable=False),Column("sha256",String(64),nullable=False),Column("content",LargeBinary,nullable=False),Column("created_at",DateTime(timezone=True),nullable=False))
report_download_audit=Table("report_download_audit",metadata,Column("id",String(100),primary_key=True),Column("artifact_id",String(100),nullable=False,index=True),Column("actor",String(100),nullable=False),Column("downloaded_at",DateTime(timezone=True),nullable=False))
DEFAULTS=[("BUDGET_SUMMARY","預算總表","BUDGET","1.0","Report/FormReportViewer.cs"),("BUDGET_DETAIL","預算詳細表","BUDGET","1.0","ucCrystalViewer.cs"),("MRS_ANALYSIS","單價分析表","MRS","1.0","DomainModule.ExportExcel"),("RESOURCE_STATS","資源統計表","MRS","1.0","FormBudgetRes.cs"),("CONTRACT","契約明細表","CONTRACT","1.0","ucSubCtr.cs"),("CHANGE","契約變更表","CHANGE","1.0","ucSubChg.cs"),("INVOICE","估驗計價表","INVOICE","1.0","FormInvoiceReport.cs"),("SETTLEMENT","結算表","SETTLEMENT","1.0","ucSubClose.cs"),("ACCEPTANCE","驗收表","ACCEPTANCE","1.0","ucSubFinal.cs")]

def _esc(v):return str(v if v is not None else "").replace("&","&amp;").replace("<","&lt;").replace(">","&gt;")
def _col(n):
 s=""
 while n:n,r=divmod(n-1,26);s=chr(65+r)+s
 return s

def _xlsx(snapshot):
 rows=list(snapshot.get("rows") or []);headers=sorted({k for row in rows for k in row});matrix=[headers]+[[row.get(h,"") for h in headers] for row in rows]
 xml_rows=[]
 for ri,row in enumerate(matrix,1):
  cells="".join(f'<c r="{_col(ci)}{ri}" t="inlineStr"><is><t>{_esc(v)}</t></is></c>' for ci,v in enumerate(row,1));xml_rows.append(f'<row r="{ri}">{cells}</row>')
 sheet='<?xml version="1.0" encoding="UTF-8" standalone="yes"?><worksheet xmlns="http://schemas.openxmlformats.org/spreadsheetml/2006/main"><sheetData>'+"".join(xml_rows)+'</sheetData></worksheet>'
 files={"[Content_Types].xml":'<?xml version="1.0"?><Types xmlns="http://schemas.openxmlformats.org/package/2006/content-types"><Default Extension="rels" ContentType="application/vnd.openxmlformats-package.relationships+xml"/><Default Extension="xml" ContentType="application/xml"/><Override PartName="/xl/workbook.xml" ContentType="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml"/><Override PartName="/xl/worksheets/sheet1.xml" ContentType="application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml"/></Types>',"_rels/.rels":'<?xml version="1.0"?><Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships"><Relationship Id="rId1" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument" Target="xl/workbook.xml"/></Relationships>',"xl/workbook.xml":'<?xml version="1.0"?><workbook xmlns="http://schemas.openxmlformats.org/spreadsheetml/2006/main" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships"><sheets><sheet name="報表" sheetId="1" r:id="rId1"/></sheets></workbook>',"xl/_rels/workbook.xml.rels":'<?xml version="1.0"?><Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships"><Relationship Id="rId1" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet" Target="worksheets/sheet1.xml"/></Relationships>',"xl/worksheets/sheet1.xml":sheet}
 out=io.BytesIO()
 with zipfile.ZipFile(out,"w",zipfile.ZIP_DEFLATED) as z:
  for name,data in files.items():z.writestr(name,data)
 return out.getvalue()

def _pdf(text):
 safe=text.replace("\\","\\\\").replace("(","\\(").replace(")","\\)").encode("latin-1","replace")
 stream=b"BT /F1 10 Tf 40 800 Td ("+safe[:4000].replace(b"\n",b") Tj 0 -14 Td (")+b") Tj ET"
 objs=[b"<< /Type /Catalog /Pages 2 0 R >>",b"<< /Type /Pages /Kids [3 0 R] /Count 1 >>",b"<< /Type /Page /Parent 2 0 R /MediaBox [0 0 595 842] /Resources << /Font << /F1 5 0 R >> >> /Contents 4 0 R >>",b"<< /Length "+str(len(stream)).encode()+b" >>\nstream\n"+stream+b"\nendstream",b"<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica >>"]
 out=bytearray(b"%PDF-1.4\n");offsets=[0]
 for i,obj in enumerate(objs,1):offsets.append(len(out));out.extend(f"{i} 0 obj\n".encode()+obj+b"\nendobj\n")
 xref=len(out);out.extend(f"xref\n0 {len(objs)+1}\n0000000000 65535 f \n".encode())
 for off in offsets[1:]:out.extend(f"{off:010d} 00000 n \n".encode())
 out.extend(f"trailer << /Size {len(objs)+1} /Root 1 0 R >>\nstartxref\n{xref}\n%%EOF\n".encode());return bytes(out)

class ReportCenterService:
 def __init__(self,engine):
  self.engine=engine;metadata.create_all(engine)
  with engine.begin() as c:
   for code,name,biz,ver,legacy in DEFAULTS:
    if not c.execute(select(report_definitions.c.code).where(report_definitions.c.code==code)).first():c.execute(report_definitions.insert().values(code=code,name=name,business_type=biz,template_version=ver,legacy_entry=legacy,schema_json=json.dumps({"required":["title","rows"]}),enabled=1))
 def catalog(self):
  with self.engine.connect() as c:rows=c.execute(select(report_definitions).where(report_definitions.c.enabled==1).order_by(report_definitions.c.code)).mappings().all()
  return [{**dict(r),"schema":json.loads(r["schema_json"])} for r in rows]
 def create_job(self,body,actor):
  code=str(body.get("definition_code","")).upper();project=str(body.get("project_code","")).strip();version=str(body.get("business_version_id","")).strip();fmt=str(body.get("format","PDF")).upper();snapshot=body.get("snapshot")
  if not code or not project or not version or not isinstance(snapshot,dict):raise ValueError("definition_code, project_code, business_version_id and snapshot are required")
  if fmt not in {"PDF","XLSX","CSV","JSON"}:raise ValueError("unsupported report format")
  with self.engine.begin() as c:
   if not c.execute(select(report_definitions.c.code).where(and_(report_definitions.c.code==code,report_definitions.c.enabled==1))).first():raise LookupError("report definition not found")
   jid,now=str(uuid4()),datetime.now(timezone.utc);c.execute(report_jobs.insert().values(id=jid,definition_code=code,project_code=project,business_version_id=version,format=fmt,status="QUEUED",progress=0,parameters_json=json.dumps(body.get("parameters") or {},ensure_ascii=False,sort_keys=True),snapshot_json=json.dumps(snapshot,ensure_ascii=False,sort_keys=True),created_by=actor,created_at=now,updated_at=now,row_version=1))
  return self.get_job(jid)
 def render(self,jid,row_version,actor):
  now=datetime.now(timezone.utc)
  with self.engine.begin() as c:
   row=c.execute(select(report_jobs).where(report_jobs.c.id==jid)).mappings().first()
   if not row:raise LookupError("report job not found")
   if row["row_version"]!=row_version:raise RuntimeError("row version conflict")
   if row["status"] not in {"QUEUED","FAILED"}:raise ValueError("job is not renderable")
   snap=json.loads(row["snapshot_json"]);payload,ext,ctype=self._render(row["format"],snap);aid=str(uuid4());filename=f"{row['definition_code'].lower()}-{row['business_version_id']}.{ext}"
   c.execute(report_artifacts.insert().values(id=aid,job_id=jid,filename=filename,content_type=ctype,size_bytes=len(payload),sha256=hashlib.sha256(payload).hexdigest(),content=payload,created_at=now));c.execute(update(report_jobs).where(and_(report_jobs.c.id==jid,report_jobs.c.row_version==row_version)).values(status="COMPLETED",progress=100,error_json=None,updated_at=now,row_version=row_version+1))
  return self.get_job(jid)
 def _render(self,fmt,snap):
  if fmt=="JSON":return json.dumps(snap,ensure_ascii=False,sort_keys=True,indent=2).encode(),"json","application/json; charset=utf-8"
  rows=list(snap.get("rows") or []);headers=sorted({k for r in rows for k in r});out=io.StringIO();w=csv.DictWriter(out,fieldnames=headers);w.writeheader();w.writerows(rows)
  if fmt=="CSV":return out.getvalue().encode("utf-8-sig"),"csv","text/csv; charset=utf-8"
  if fmt=="XLSX":return _xlsx(snap),"xlsx","application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
  return _pdf(f"PCCES REPORT\n{snap.get('title','')}\n\n"+out.getvalue()),"pdf","application/pdf"
 def get_job(self,jid):
  with self.engine.connect() as c:r=c.execute(select(report_jobs).where(report_jobs.c.id==jid)).mappings().first();a=c.execute(select(report_artifacts).where(report_artifacts.c.job_id==jid).order_by(report_artifacts.c.created_at.desc())).mappings().first()
  if not r:raise LookupError("report job not found")
  return {"id":r["id"],"definition_code":r["definition_code"],"project_code":r["project_code"],"business_version_id":r["business_version_id"],"format":r["format"],"status":r["status"],"progress":r["progress"],"parameters":json.loads(r["parameters_json"]),"snapshot":json.loads(r["snapshot_json"]),"error":json.loads(r["error_json"]) if r["error_json"] else None,"row_version":r["row_version"],"artifact":{"id":a["id"],"filename":a["filename"],"sha256":a["sha256"],"download_url":f"/api/reports/artifacts/{a['id']}/download"} if a else None}
 def download(self,aid,actor):
  with self.engine.begin() as c:
   r=c.execute(select(report_artifacts).where(report_artifacts.c.id==aid)).mappings().first()
   if not r:raise LookupError("report artifact not found")
   c.execute(report_download_audit.insert().values(id=str(uuid4()),artifact_id=aid,actor=actor,downloaded_at=datetime.now(timezone.utc)))
  return bytes(r["content"]),r["content_type"],r["filename"]

def build_report_center_blueprint(service,resolve_user_id):
 bp=Blueprint("report_center",__name__,url_prefix="/api/reports")
 @bp.get("/definitions")
 def definitions():return jsonify(service.catalog())
 @bp.post("/jobs")
 def create_job():
  actor=resolve_user_id()
  if actor is None:return jsonify({"code":"UNAUTHORIZED"}),401
  try:return jsonify(service.create_job(request.get_json(silent=True) or {},str(actor))),201
  except LookupError as e:return jsonify({"code":"NOT_FOUND","detail":str(e)}),404
  except ValueError as e:return jsonify({"code":"INVALID_ARGUMENT","detail":str(e)}),400
 @bp.get("/jobs/<jid>")
 def get_job(jid):
  try:return jsonify(service.get_job(jid))
  except LookupError as e:return jsonify({"code":"NOT_FOUND","detail":str(e)}),404
 @bp.post("/jobs/<jid>/render")
 def render(jid):
  actor=resolve_user_id()
  if actor is None:return jsonify({"code":"UNAUTHORIZED"}),401
  try:return jsonify(service.render(jid,int((request.get_json(silent=True) or {}).get("row_version",0)),str(actor)))
  except LookupError as e:return jsonify({"code":"NOT_FOUND","detail":str(e)}),404
  except RuntimeError as e:return jsonify({"code":"CONFLICT","detail":str(e)}),409
  except ValueError as e:return jsonify({"code":"INVALID_ARGUMENT","detail":str(e)}),400
 @bp.get("/artifacts/<aid>/download")
 def download(aid):
  actor=resolve_user_id()
  if actor is None:return jsonify({"code":"UNAUTHORIZED"}),401
  try:content,ctype,name=service.download(aid,str(actor));return Response(content,content_type=ctype,headers={"Content-Disposition":f'attachment; filename="{name}"'})
  except LookupError as e:return jsonify({"code":"NOT_FOUND","detail":str(e)}),404
 return bp
