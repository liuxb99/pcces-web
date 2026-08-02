#!/usr/bin/env python3
"""Production readiness smoke: migrations, deterministic outputs and backup restore."""
from __future__ import annotations

import hashlib
import io
import json
import os
import shutil
import tempfile
import zipfile
from pathlib import Path

from sqlalchemy import create_engine, text

from api.admin_console import AdminConsoleService
from api.migrations import run_migrations
from api.report_center import ReportCenterService


def main() -> int:
    with tempfile.TemporaryDirectory() as directory:
        root = Path(directory)
        database = root / "pcces.db"
        url = f"sqlite:///{database}"
        engine = create_engine(url)
        run_migrations(engine)
        with engine.connect() as connection:
            connection.execute(text("SELECT 1")).scalar_one()

        report = ReportCenterService(engine)
        snapshot = {"title": "Golden Invoice", "rows": [{"item": "A", "amount": "10.00"}]}
        pdf_job = report.create_job({"definition_code": "INVOICE", "project_code": "P1", "business_version_id": "I1", "format": "PDF", "snapshot": snapshot}, "gate")
        pdf_job = report.render(pdf_job["id"], pdf_job["row_version"], "gate")
        pdf, _, _ = report.download(pdf_job["artifact"]["id"], "gate")
        if not (pdf.startswith(b"%PDF") and b"xref" in pdf and pdf.endswith(b"%%EOF\n")):
            raise RuntimeError("PDF golden invariant failed")

        xlsx_job = report.create_job({"definition_code": "INVOICE", "project_code": "P1", "business_version_id": "I1", "format": "XLSX", "snapshot": snapshot}, "gate")
        xlsx_job = report.render(xlsx_job["id"], xlsx_job["row_version"], "gate")
        xlsx, _, _ = report.download(xlsx_job["artifact"]["id"], "gate")
        if not zipfile.is_zipfile(io.BytesIO(xlsx)):
            raise RuntimeError("XLSX golden invariant failed")
        with zipfile.ZipFile(io.BytesIO(xlsx)) as archive:
            if "xl/worksheets/sheet1.xml" not in archive.namelist():
                raise RuntimeError("XLSX worksheet missing")

        admin = AdminConsoleService(engine, url)
        backup = admin.backup("gate")
        if backup["status"] != "COMPLETED":
            raise RuntimeError(f"backup failed: {backup}")
        artifact = admin.backup_artifact(backup["id"])
        if hashlib.sha256(artifact).hexdigest() != backup["sha256"]:
            raise RuntimeError("backup hash mismatch")
        restored = root / "restored.db"
        restored.write_bytes(artifact)
        restored_engine = create_engine(f"sqlite:///{restored}")
        with restored_engine.connect() as connection:
            connection.execute(text("SELECT 1")).scalar_one()
            tables = connection.execute(text("SELECT COUNT(*) FROM sqlite_master WHERE type='table'")).scalar_one()
            if tables < 10:
                raise RuntimeError("restored database schema incomplete")
        restored_engine.dispose()
        engine.dispose()
        print(json.dumps({"migration": "PASS", "pdf": "PASS", "xlsx": "PASS", "backup_restore": "PASS", "tables": tables}))
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
