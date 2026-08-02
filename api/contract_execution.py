"""Phase 6 contract execution: invoice periods, settlement and acceptance."""
from __future__ import annotations

import json
from datetime import datetime, timezone
from decimal import Decimal, InvalidOperation
from uuid import uuid4

from flask import Blueprint, jsonify, request
from sqlalchemy import Column, DateTime, Integer, MetaData, Numeric, String, Table, Text, and_, func, select, update

from api.contract_core import contracts_v2, contract_items_v2

metadata=MetaData()
invoice_periods_v2=Table("invoice_periods_v2",metadata,
 Column("id",String(100),primary_key=True),Column("contract_id",String(100),nullable=False,index=True),Column("period_no",Integer,nullable=False),Column("status",String(30),nullable=False),
 Column("current_gross",Numeric(28,8),nullable=False),Column("previous_gross",Numeric(28,8),nullable=False),Column("cumulative_gross",Numeric(28,8),nullable=False),
 Column("deduction",Numeric(28,8),nullable=False),Column("retention",Numeric(28,8),nullable=False),Column("adjustment",Numeric(28,8),nullable=False),Column("net_payable",Numeric(28,8),nullable=False),
 Column("snapshot_json",Text,nullable=False),Column("created_by",String(100),nullable=False),Column("created_at",DateTime(timezone=True),nullable=False),Column("approved_by",String(100)),Column("approved_at",DateTime(timezone=True)),Column("row_version",Integer,nullable=False,default=1))
invoice_lines_v2=Table("invoice_lines_v2",metadata,
 Column("id",String(100),primary_key=True),Column("period_id",String(100),nullable=False,index=True),Column("contract_item_id",String(100),nullable=False,index=True),Column("current_quantity",Numeric(28,8),nullable=False),Column("current_amount",Numeric(28,8),nullable=False),Column("previous_quantity",Numeric(28,8),nullable=False),Column("previous_amount",Numeric(28,8),nullable=False),Column("cumulative_quantity",Numeric(28,8),nullable=False),Column("cumulative_amount",Numeric(28,8),nullable=False),Column("sort_order",Integer,nullable=False))
settlements_v2=Table("settlements_v2",metadata,
 Column("id",String(100),primary_key=True),Column("contract_id",String(100),nullable=False,unique=True),Column("status",String(30),nullable=False),Column("contract_amount",Numeric(28,8),nullable=False),Column("invoiced_amount",Numeric(28,8),nullable=False),Column("final_adjustment",Numeric(28,8),nullable=False),Column("final_amount",Numeric(28,8),nullable=False),Column("snapshot_json",Text,nullable=False),Column("created_by",String(100),nullable=False),Column("created_at",DateTime(timezone=True),nullable=False),Column("approved_by",String(100)),Column("approved_at",DateTime(timezone=True)),Column("row_version",Integer,nullable=False,default=1))
acceptances_v2=Table("acceptances_v2",metadata,
 Column("id",String(100),primary_key=True),Column("contract_id",String(100),nullable=False,unique=True),Column("settlement_id",String(100),nullable=False),Column("status",String(30),nullable=False),Column("inspection_date",String(30)),Column("result",String(100)),Column("defects_json",Text,nullable=False),Column("improvements_json",Text,nullable=False),Column("created_by",String(100),nullable=False),Column("created_at",DateTime(timezone=True),nullable=False),Column("completed_by",String(100)),Column("completed_at",DateTime(timezone=True)),Column("row_version",Integer,nullable=False,default=1))


def _d(v,name):
 try:return Decimal(str(v))
 except (InvalidOperation,ValueError,TypeError) as exc:raise ValueError(f"{name} must be decimal") from exc

class ContractExecutionService:
 def __init__(self,engine):self.engine=engine;metadata.create_all(engine)

 def create_invoice(self,contract_id,body,actor):
  lines=list(body.get("items") or []);deduction=_d(body.get("deduction",0),"deduction");retention=_d(body.get("retention",0),"retention");adjustment=_d(body.get("adjustment",0),"adjustment")
  if not lines:raise ValueError("invoice items are required")
  with self.engine.begin() as conn:
   contract=conn.execute(select(contracts_v2).where(contracts_v2.c.id==contract_id)).mappings().first()
   if not contract:raise LookupError("contract not found")
   if str(contract["status"]).upper() not in {"APPROVED","LOCKED"}:raise PermissionError("invoice requires approved contract")
   if conn.execute(select(settlements_v2.c.id).where(and_(settlements_v2.c.contract_id==contract_id,settlements_v2.c.status.in_(["APPROVED","COMPLETED"])))).first():raise PermissionError("settled contract cannot create invoice")
   latest=conn.execute(select(invoice_periods_v2.c.period_no).where(invoice_periods_v2.c.contract_id==contract_id).order_by(invoice_periods_v2.c.period_no.desc())).first();period_no=(latest[0] if latest else 0)+1
   item_rows=conn.execute(select(contract_items_v2).where(contract_items_v2.c.contract_id==contract_id).order_by(contract_items_v2.c.sort_order)).mappings().all();by_id={r["id"]:r for r in item_rows}
   normalized=[];gross=Decimal("0")
   for idx,raw in enumerate(lines,1):
    item_id=str(raw.get("contract_item_id","")).strip();qty=_d(raw.get("current_quantity",0),"current_quantity")
    if item_id not in by_id:raise ValueError("contract_item_id not found")
    if qty<0:raise ValueError("current_quantity cannot be negative")
    prev=conn.execute(select(func.coalesce(func.sum(invoice_lines_v2.c.current_quantity),0),func.coalesce(func.sum(invoice_lines_v2.c.current_amount),0)).select_from(invoice_lines_v2.join(invoice_periods_v2,invoice_lines_v2.c.period_id==invoice_periods_v2.c.id)).where(and_(invoice_periods_v2.c.contract_id==contract_id,invoice_periods_v2.c.status=="APPROVED",invoice_lines_v2.c.contract_item_id==item_id))).first()
    prev_qty,prev_amt=_d(prev[0],"previous_quantity"),_d(prev[1],"previous_amount");contract_qty=_d(by_id[item_id]["quantity"],"quantity")
    if prev_qty+qty>contract_qty:raise ValueError("cumulative quantity exceeds contract quantity")
    amount=(qty*_d(by_id[item_id]["unit_price"],"unit_price"));gross+=amount;normalized.append((item_id,qty,amount,prev_qty,prev_amt,idx))
   previous=conn.execute(select(func.coalesce(func.sum(invoice_periods_v2.c.current_gross),0)).where(and_(invoice_periods_v2.c.contract_id==contract_id,invoice_periods_v2.c.status=="APPROVED"))).scalar_one();previous=_d(previous,"previous_gross")
   net=gross-deduction-retention+adjustment
   if net<0:raise ValueError("net_payable cannot be negative")
   pid,now=str(uuid4()),datetime.now(timezone.utc)
   snapshot={"contract_id":contract_id,"period_no":period_no,"contract_amount":str(contract["contract_amount"]),"items":[{k:str(v) if v is not None else None for k,v in r.items()} for r in item_rows]}
   conn.execute(invoice_periods_v2.insert().values(id=pid,contract_id=contract_id,period_no=period_no,status="DRAFT",current_gross=gross,previous_gross=previous,cumulative_gross=previous+gross,deduction=deduction,retention=retention,adjustment=adjustment,net_payable=net,snapshot_json=json.dumps(snapshot,ensure_ascii=False,sort_keys=True),created_by=actor,created_at=now,row_version=1))
   for item_id,qty,amount,pq,pa,idx in normalized:conn.execute(invoice_lines_v2.insert().values(id=str(uuid4()),period_id=pid,contract_item_id=item_id,current_quantity=qty,current_amount=amount,previous_quantity=pq,previous_amount=pa,cumulative_quantity=pq+qty,cumulative_amount=pa+amount,sort_order=idx))
  return self.get_invoice(pid)

 def transition_invoice(self,period_id,body,actor):
  target,expected=str(body.get("status","")).upper(),int(body.get("row_version",0));now=datetime.now(timezone.utc);allowed={"DRAFT":{"SUBMITTED"},"SUBMITTED":{"DRAFT","APPROVED"},"APPROVED":set()}
  with self.engine.begin() as conn:
   row=conn.execute(select(invoice_periods_v2).where(invoice_periods_v2.c.id==period_id)).mappings().first()
   if not row:raise LookupError("invoice period not found")
   current=str(row["status"]).upper()
   if int(row["row_version"])!=expected:raise RuntimeError("row version conflict")
   if target not in allowed.get(current,set()):raise ValueError(f"invalid invoice transition {current} -> {target}")
   if target=="APPROVED":
    newer=conn.execute(select(invoice_periods_v2.c.id).where(and_(invoice_periods_v2.c.contract_id==row["contract_id"],invoice_periods_v2.c.period_no>row["period_no"]))).first()
    if newer:raise PermissionError("cannot approve an earlier period after a later period exists")
   values={"status":target,"row_version":expected+1}
   if target=="APPROVED":values.update({"approved_by":actor,"approved_at":now})
   conn.execute(update(invoice_periods_v2).where(and_(invoice_periods_v2.c.id==period_id,invoice_periods_v2.c.row_version==expected)).values(**values))
  return self.get_invoice(period_id)

 def get_invoice(self,period_id):
  with self.engine.connect() as conn:
   row=conn.execute(select(invoice_periods_v2).where(invoice_periods_v2.c.id==period_id)).mappings().first();lines=conn.execute(select(invoice_lines_v2).where(invoice_lines_v2.c.period_id==period_id).order_by(invoice_lines_v2.c.sort_order)).mappings().all()
  if not row:raise LookupError("invoice period not found")
  return {"id":row["id"],"contract_id":row["contract_id"],"period_no":row["period_no"],"status":row["status"],"current_gross":str(row["current_gross"]),"previous_gross":str(row["previous_gross"]),"cumulative_gross":str(row["cumulative_gross"]),"deduction":str(row["deduction"]),"retention":str(row["retention"]),"adjustment":str(row["adjustment"]),"net_payable":str(row["net_payable"]),"snapshot":json.loads(row["snapshot_json"]),"approved_by":row["approved_by"],"row_version":row["row_version"],"items":[{k:str(v) if v is not None else None for k,v in x.items()} for x in lines],"deep_link":f"/app/contracts/{row['contract_id']}/invoices/{row['id']}"}

 def create_settlement(self,contract_id,body,actor):
  adjustment=_d(body.get("final_adjustment",0),"final_adjustment")
  with self.engine.begin() as conn:
   contract=conn.execute(select(contracts_v2).where(contracts_v2.c.id==contract_id)).mappings().first()
   if not contract:raise LookupError("contract not found")
   pending=conn.execute(select(invoice_periods_v2.c.id).where(and_(invoice_periods_v2.c.contract_id==contract_id,invoice_periods_v2.c.status!="APPROVED"))).first()
   if pending:raise PermissionError("all invoice periods must be approved")
   invoiced=_d(conn.execute(select(func.coalesce(func.sum(invoice_periods_v2.c.current_gross),0)).where(and_(invoice_periods_v2.c.contract_id==contract_id,invoice_periods_v2.c.status=="APPROVED"))).scalar_one(),"invoiced")
   final=invoiced+adjustment
   if final<0:raise ValueError("final_amount cannot be negative")
   sid,now=str(uuid4()),datetime.now(timezone.utc);snapshot={"contract_amount":str(contract["contract_amount"]),"invoiced_amount":str(invoiced)}
   conn.execute(settlements_v2.insert().values(id=sid,contract_id=contract_id,status="DRAFT",contract_amount=contract["contract_amount"],invoiced_amount=invoiced,final_adjustment=adjustment,final_amount=final,snapshot_json=json.dumps(snapshot,sort_keys=True),created_by=actor,created_at=now,row_version=1))
  return self.get_settlement(sid)

 def transition_settlement(self,sid,body,actor):
  target,expected=str(body.get("status","")).upper(),int(body.get("row_version",0));allowed={"DRAFT":{"SUBMITTED"},"SUBMITTED":{"DRAFT","APPROVED"},"APPROVED":{"COMPLETED"},"COMPLETED":set()};now=datetime.now(timezone.utc)
  with self.engine.begin() as conn:
   row=conn.execute(select(settlements_v2).where(settlements_v2.c.id==sid)).mappings().first()
   if not row:raise LookupError("settlement not found")
   current=str(row["status"]).upper()
   if int(row["row_version"])!=expected:raise RuntimeError("row version conflict")
   if target not in allowed.get(current,set()):raise ValueError(f"invalid settlement transition {current} -> {target}")
   values={"status":target,"row_version":expected+1}
   if target=="APPROVED":values.update({"approved_by":actor,"approved_at":now})
   conn.execute(update(settlements_v2).where(and_(settlements_v2.c.id==sid,settlements_v2.c.row_version==expected)).values(**values))
   if target=="COMPLETED":conn.execute(update(contracts_v2).where(contracts_v2.c.id==row["contract_id"]).values(status="SETTLED",updated_at=now,row_version=contracts_v2.c.row_version+1))
  return self.get_settlement(sid)

 def get_settlement(self,sid):
  with self.engine.connect() as conn:row=conn.execute(select(settlements_v2).where(settlements_v2.c.id==sid)).mappings().first()
  if not row:raise LookupError("settlement not found")
  return {k:(str(v) if isinstance(v,Decimal) else v.isoformat() if hasattr(v,"isoformat") else v) for k,v in row.items()}|{"snapshot":json.loads(row["snapshot_json"]),"deep_link":f"/app/contracts/{row['contract_id']}/settlement"}

 def create_acceptance(self,contract_id,body,actor):
  with self.engine.begin() as conn:
   settlement=conn.execute(select(settlements_v2).where(and_(settlements_v2.c.contract_id==contract_id,settlements_v2.c.status=="COMPLETED"))).mappings().first()
   if not settlement:raise PermissionError("completed settlement is required")
   aid,now=str(uuid4()),datetime.now(timezone.utc)
   conn.execute(acceptances_v2.insert().values(id=aid,contract_id=contract_id,settlement_id=settlement["id"],status="DRAFT",inspection_date=body.get("inspection_date"),result=body.get("result"),defects_json=json.dumps(body.get("defects") or [],ensure_ascii=False),improvements_json=json.dumps(body.get("improvements") or [],ensure_ascii=False),created_by=actor,created_at=now,row_version=1))
  return self.get_acceptance(aid)

 def transition_acceptance(self,aid,body,actor):
  target,expected=str(body.get("status","")).upper(),int(body.get("row_version",0));allowed={"DRAFT":{"INSPECTED"},"INSPECTED":{"IMPROVEMENT_REQUIRED","COMPLETED"},"IMPROVEMENT_REQUIRED":{"INSPECTED"},"COMPLETED":{"ARCHIVED"},"ARCHIVED":set()};now=datetime.now(timezone.utc)
  with self.engine.begin() as conn:
   row=conn.execute(select(acceptances_v2).where(acceptances_v2.c.id==aid)).mappings().first()
   if not row:raise LookupError("acceptance not found")
   current=str(row["status"]).upper()
   if int(row["row_version"])!=expected:raise RuntimeError("row version conflict")
   if target not in allowed.get(current,set()):raise ValueError(f"invalid acceptance transition {current} -> {target}")
   values={"status":target,"row_version":expected+1}
   if target in {"COMPLETED","ARCHIVED"}:values.update({"completed_by":actor,"completed_at":now})
   conn.execute(update(acceptances_v2).where(and_(acceptances_v2.c.id==aid,acceptances_v2.c.row_version==expected)).values(**values))
   if target=="ARCHIVED":conn.execute(update(contracts_v2).where(contracts_v2.c.id==row["contract_id"]).values(status="ARCHIVED",updated_at=now,row_version=contracts_v2.c.row_version+1))
  return self.get_acceptance(aid)

 def get_acceptance(self,aid):
  with self.engine.connect() as conn:row=conn.execute(select(acceptances_v2).where(acceptances_v2.c.id==aid)).mappings().first()
  if not row:raise LookupError("acceptance not found")
  return {"id":row["id"],"contract_id":row["contract_id"],"settlement_id":row["settlement_id"],"status":row["status"],"inspection_date":row["inspection_date"],"result":row["result"],"defects":json.loads(row["defects_json"]),"improvements":json.loads(row["improvements_json"]),"completed_by":row["completed_by"],"row_version":row["row_version"],"deep_link":f"/app/contracts/{row['contract_id']}/acceptance"}


def build_contract_execution_blueprint(service,resolve_user_id):
 bp=Blueprint("contract_execution",__name__,url_prefix="/api/contracts")
 def actor():
  value=resolve_user_id()
  if value is None:raise PermissionError("authentication required")
  return str(value)
 @bp.post("/<contract_id>/invoice-periods")
 def create_invoice(contract_id):
  try:return jsonify(service.create_invoice(contract_id,request.get_json(silent=True) or {},actor())),201
  except Exception as exc:return _error(exc)
 @bp.get("/invoice-periods/<period_id>")
 def get_invoice(period_id):
  try:return jsonify(service.get_invoice(period_id))
  except Exception as exc:return _error(exc)
 @bp.post("/invoice-periods/<period_id>/transition")
 def transition_invoice(period_id):
  try:return jsonify(service.transition_invoice(period_id,request.get_json(silent=True) or {},actor()))
  except Exception as exc:return _error(exc)
 @bp.post("/<contract_id>/settlements")
 def create_settlement(contract_id):
  try:return jsonify(service.create_settlement(contract_id,request.get_json(silent=True) or {},actor())),201
  except Exception as exc:return _error(exc)
 @bp.get("/settlements/<sid>")
 def get_settlement(sid):
  try:return jsonify(service.get_settlement(sid))
  except Exception as exc:return _error(exc)
 @bp.post("/settlements/<sid>/transition")
 def transition_settlement(sid):
  try:return jsonify(service.transition_settlement(sid,request.get_json(silent=True) or {},actor()))
  except Exception as exc:return _error(exc)
 @bp.post("/<contract_id>/acceptances")
 def create_acceptance(contract_id):
  try:return jsonify(service.create_acceptance(contract_id,request.get_json(silent=True) or {},actor())),201
  except Exception as exc:return _error(exc)
 @bp.get("/acceptances/<aid>")
 def get_acceptance(aid):
  try:return jsonify(service.get_acceptance(aid))
  except Exception as exc:return _error(exc)
 @bp.post("/acceptances/<aid>/transition")
 def transition_acceptance(aid):
  try:return jsonify(service.transition_acceptance(aid,request.get_json(silent=True) or {},actor()))
  except Exception as exc:return _error(exc)
 return bp

def _error(exc):
 if isinstance(exc,LookupError):return jsonify({"code":"NOT_FOUND","detail":str(exc)}),404
 if isinstance(exc,PermissionError):return jsonify({"code":"NOT_ELIGIBLE","detail":str(exc)}),409
 if isinstance(exc,RuntimeError):return jsonify({"code":"CONFLICT","detail":str(exc)}),409
 return jsonify({"code":"INVALID_ARGUMENT","detail":str(exc)}),400
