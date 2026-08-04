"""AI-Engineering-OS 專用的 pcces-web 擴充入口。

啟動方式：
    python -m flask --app api.integration_app:app run --host 127.0.0.1 --port 5000

此模組不修改既有大型 api.index 路由；它匯入原 app 後，新增正式 PDF 與
不可變預算版本 API。
"""
from __future__ import annotations

import hashlib
import io
import json
from datetime import datetime, timezone

from flask import jsonify, request, send_file
from sqlalchemy import text

from api.index import app, engine, SessionLocal, require_auth, model_to_dict
from api.models import BudgetItem, Project


def _ensure_version_table() -> None:
    with engine.begin() as conn:
        conn.execute(text("""
            CREATE TABLE IF NOT EXISTS pcces_budget_versions (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                project_id INTEGER NOT NULL,
                version_no INTEGER NOT NULL,
                version_name TEXT NOT NULL,
                frozen INTEGER NOT NULL DEFAULT 0,
                prepared_by TEXT,
                note TEXT,
                snapshot_json TEXT NOT NULL,
                snapshot_sha256 TEXT NOT NULL,
                total_amount REAL NOT NULL DEFAULT 0,
                created_at TEXT NOT NULL,
                frozen_at TEXT,
                UNIQUE(project_id, version_no)
            )
        """))
        conn.execute(text("CREATE INDEX IF NOT EXISTS idx_pcces_budget_versions_project ON pcces_budget_versions(project_id, version_no)"))


def _tree(db, project_id: int, parent_id=None):
    query = db.query(BudgetItem).filter(BudgetItem.project_id == project_id)
    if parent_id is None:
        query = query.filter(BudgetItem.parent_id.is_(None))
    else:
        query = query.filter(BudgetItem.parent_id == parent_id)
    rows = query.order_by(BudgetItem.sort_order, BudgetItem.id).all()
    result = []
    for row in rows:
        item = model_to_dict(row)
        item["children"] = _tree(db, project_id, row.id)
        result.append(item)
    return result


def _flatten(nodes, level=0):
    out = []
    for node in nodes:
        current = dict(node)
        children = current.pop("children", []) or []
        current["level"] = level
        out.append(current)
        out.extend(_flatten(children, level + 1))
    return out


def _snapshot(db, project_id: int):
    project = db.query(Project).filter(Project.id == project_id).first()
    if not project:
        return None
    tree = _tree(db, project_id)
    flat = _flatten(tree)
    total = sum(float(item.get("amount") or 0) for item in flat if str(item.get("kind") or "") == "W")
    payload = {
        "schema_version": "pcces-budget-snapshot/1.0",
        "project": model_to_dict(project),
        "budget_tree": tree,
        "item_count": len(flat),
        "total_amount": round(total, 2),
        "created_at": datetime.now(timezone.utc).isoformat(),
    }
    canonical = json.dumps(payload, ensure_ascii=False, sort_keys=True, separators=(",", ":"))
    return payload, hashlib.sha256(canonical.encode("utf-8")).hexdigest()


_ensure_version_table()


@app.route("/api/projects/<int:project_id>/budget/versions", methods=["POST"])
@require_auth
def engineering_create_budget_version(project_id, user_id):
    data = request.get_json() or {}
    db = SessionLocal()
    try:
        snap = _snapshot(db, project_id)
        if not snap:
            return jsonify({"detail": "專案不存在"}), 404
        payload, checksum = snap
        next_no = db.execute(text("SELECT COALESCE(MAX(version_no), 0) + 1 FROM pcces_budget_versions WHERE project_id=:pid"), {"pid": project_id}).scalar_one()
        now = datetime.now(timezone.utc).isoformat()
        frozen = bool(data.get("freeze", False))
        db.execute(text("""
            INSERT INTO pcces_budget_versions
            (project_id, version_no, version_name, frozen, prepared_by, note, snapshot_json,
             snapshot_sha256, total_amount, created_at, frozen_at)
            VALUES (:project_id, :version_no, :version_name, :frozen, :prepared_by, :note,
                    :snapshot_json, :snapshot_sha256, :total_amount, :created_at, :frozen_at)
        """), {
            "project_id": project_id,
            "version_no": int(next_no),
            "version_name": (data.get("version_name") or f"預算版本 R{int(next_no):04d}").strip(),
            "frozen": 1 if frozen else 0,
            "prepared_by": (data.get("prepared_by") or "").strip(),
            "note": (data.get("note") or "").strip(),
            "snapshot_json": json.dumps(payload, ensure_ascii=False),
            "snapshot_sha256": checksum,
            "total_amount": payload["total_amount"],
            "created_at": now,
            "frozen_at": now if frozen else None,
        })
        version_id = db.execute(text("SELECT last_insert_rowid()" )).scalar_one()
        db.commit()
        return jsonify({
            "id": int(version_id), "project_id": project_id, "version_no": int(next_no),
            "version_name": (data.get("version_name") or f"預算版本 R{int(next_no):04d}").strip(),
            "frozen": frozen, "snapshot_sha256": checksum,
            "total_amount": payload["total_amount"], "created_at": now,
        }), 201
    finally:
        db.close()


@app.route("/api/projects/<int:project_id>/budget/versions", methods=["GET"])
@require_auth
def engineering_list_budget_versions(project_id, user_id):
    db = SessionLocal()
    try:
        rows = db.execute(text("""
            SELECT id, project_id, version_no, version_name, frozen, prepared_by, note,
                   snapshot_sha256, total_amount, created_at, frozen_at
            FROM pcces_budget_versions WHERE project_id=:pid ORDER BY version_no DESC
        """), {"pid": project_id}).mappings().all()
        return jsonify([{**dict(row), "frozen": bool(row["frozen"])} for row in rows])
    finally:
        db.close()


@app.route("/api/projects/<int:project_id>/budget/versions/<int:version_no>", methods=["GET"])
@require_auth
def engineering_get_budget_version(project_id, version_no, user_id):
    db = SessionLocal()
    try:
        row = db.execute(text("""
            SELECT * FROM pcces_budget_versions WHERE project_id=:pid AND version_no=:v
        """), {"pid": project_id, "v": version_no}).mappings().first()
        if not row:
            return jsonify({"detail": "預算版本不存在"}), 404
        result = dict(row)
        result["frozen"] = bool(result["frozen"])
        result["snapshot"] = json.loads(result.pop("snapshot_json"))
        return jsonify(result)
    finally:
        db.close()


@app.route("/api/projects/<int:project_id>/budget/versions/<int:version_no>/freeze", methods=["POST"])
@require_auth
def engineering_freeze_budget_version(project_id, version_no, user_id):
    db = SessionLocal()
    try:
        now = datetime.now(timezone.utc).isoformat()
        result = db.execute(text("""
            UPDATE pcces_budget_versions SET frozen=1, frozen_at=:now
            WHERE project_id=:pid AND version_no=:v
        """), {"now": now, "pid": project_id, "v": version_no})
        if result.rowcount == 0:
            return jsonify({"detail": "預算版本不存在"}), 404
        db.commit()
        return jsonify({"project_id": project_id, "version_no": version_no, "frozen": True, "frozen_at": now})
    finally:
        db.close()


@app.route("/api/projects/<int:project_id>/budget/versions/compare", methods=["GET"])
@require_auth
def engineering_compare_budget_versions(project_id, user_id):
    a = request.args.get("a", type=int)
    b = request.args.get("b", type=int)
    if not a or not b:
        return jsonify({"detail": "請提供 a 與 b 版本號"}), 400
    db = SessionLocal()
    try:
        rows = db.execute(text("""
            SELECT version_no, snapshot_json, total_amount, snapshot_sha256
            FROM pcces_budget_versions WHERE project_id=:pid AND version_no IN (:a,:b)
        """), {"pid": project_id, "a": a, "b": b}).mappings().all()
        by_no = {int(row["version_no"]): row for row in rows}
        if a not in by_no or b not in by_no:
            return jsonify({"detail": "比較版本不存在"}), 404
        def keyed(row):
            snap = json.loads(row["snapshot_json"])
            return {str(i.get("id")): i for i in _flatten(snap.get("budget_tree", []))}
        left, right = keyed(by_no[a]), keyed(by_no[b])
        changes = []
        for key in sorted(set(left) | set(right)):
            x, y = left.get(key), right.get(key)
            if x == y:
                continue
            changes.append({"budget_item_id": key, "before": x, "after": y})
        return jsonify({
            "project_id": project_id, "version_a": a, "version_b": b,
            "total_a": by_no[a]["total_amount"], "total_b": by_no[b]["total_amount"],
            "total_diff": round(float(by_no[b]["total_amount"] or 0)-float(by_no[a]["total_amount"] or 0), 2),
            "changes": changes,
        })
    finally:
        db.close()


@app.route("/api/projects/<int:project_id>/reports/pdf", methods=["GET"])
@require_auth
def engineering_budget_pdf(project_id, user_id):
    try:
        from reportlab.lib import colors
        from reportlab.lib.pagesizes import A4, landscape
        from reportlab.lib.styles import getSampleStyleSheet
        from reportlab.pdfbase import pdfmetrics
        from reportlab.pdfbase.cidfonts import UnicodeCIDFont
        from reportlab.platypus import SimpleDocTemplate, Table, TableStyle, Paragraph, Spacer
    except ImportError:
        return jsonify({"detail": "尚未安裝 reportlab，無法產生 PDF"}), 503
    db = SessionLocal()
    try:
        snap = _snapshot(db, project_id)
        if not snap:
            return jsonify({"detail": "專案不存在"}), 404
        payload, checksum = snap
        project = payload["project"]
        rows = _flatten(payload["budget_tree"])
        pdfmetrics.registerFont(UnicodeCIDFont("STSong-Light"))
        buffer = io.BytesIO()
        doc = SimpleDocTemplate(buffer, pagesize=landscape(A4), rightMargin=24, leftMargin=24, topMargin=24, bottomMargin=24)
        styles = getSampleStyleSheet()
        title = styles["Title"]
        title.fontName = "STSong-Light"
        normal = styles["Normal"]
        normal.fontName = "STSong-Light"
        story = [Paragraph(f"{project.get('name','')} 工程預算書", title), Spacer(1, 12), Paragraph(f"快照 SHA256：{checksum}", normal), Spacer(1, 12)]
        table_data = [["項次", "PCCES碼", "項目名稱", "單位", "數量", "單價", "金額"]]
        for row in rows:
            table_data.append([
                str(row.get("print_no") or row.get("item_no") or ""),
                str(row.get("pcces_code") or row.get("item_no") or ""),
                ("　" * int(row.get("level") or 0)) + str(row.get("c_name") or ""),
                str(row.get("c_unit") or ""),
                f"{float(row.get('quantity') or 0):,.3f}",
                f"{float(row.get('unit_price') or 0):,.2f}",
                f"{float(row.get('amount') or 0):,.2f}",
            ])
        table_data.append(["", "", "預算總額", "", "", "", f"{payload['total_amount']:,.2f}"])
        table = Table(table_data, repeatRows=1, colWidths=[60, 80, 260, 45, 75, 80, 90])
        table.setStyle(TableStyle([
            ("FONTNAME", (0,0), (-1,-1), "STSong-Light"),
            ("BACKGROUND", (0,0), (-1,0), colors.HexColor("#D9EAF7")),
            ("GRID", (0,0), (-1,-1), 0.4, colors.grey),
            ("ALIGN", (3,1), (-1,-1), "RIGHT"),
            ("VALIGN", (0,0), (-1,-1), "MIDDLE"),
            ("FONTSIZE", (0,0), (-1,-1), 8),
        ]))
        story.append(table)
        doc.build(story)
        buffer.seek(0)
        return send_file(buffer, as_attachment=True, download_name=f"PCCES_工程預算書_{project_id}.pdf", mimetype="application/pdf")
    finally:
        db.close()
