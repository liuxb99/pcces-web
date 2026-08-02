"""Immutable source attachments and downloadable conversion error catalogues."""
from __future__ import annotations

import csv
import hashlib
import io
import json
from datetime import datetime, timezone
from uuid import uuid4

from sqlalchemy import Column, DateTime, Integer, LargeBinary, MetaData, String, Table, Text, select

metadata = MetaData()
conversion_source_artifacts = Table(
    "conversion_source_artifacts", metadata,
    Column("id", String(100), primary_key=True),
    Column("session_type", String(40), nullable=False, index=True),
    Column("session_id", String(100), nullable=False, index=True),
    Column("original_filename", String(500), nullable=False),
    Column("content_type", String(150), nullable=False),
    Column("format", String(30), nullable=False),
    Column("format_version", String(30), nullable=False),
    Column("size_bytes", Integer, nullable=False),
    Column("sha256", String(64), nullable=False),
    Column("content", LargeBinary, nullable=False),
    Column("created_by", String(100), nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
    Column("row_version", Integer, nullable=False, default=1),
)
conversion_error_catalogues = Table(
    "conversion_error_catalogues", metadata,
    Column("id", String(100), primary_key=True),
    Column("session_type", String(40), nullable=False, index=True),
    Column("session_id", String(100), nullable=False, index=True),
    Column("error_count", Integer, nullable=False),
    Column("warning_count", Integer, nullable=False),
    Column("catalogue_json", Text, nullable=False),
    Column("created_by", String(100), nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
    Column("row_version", Integer, nullable=False, default=1),
)

class ConversionSourceArtifactService:
    def __init__(self, engine):
        self.engine = engine
        metadata.create_all(engine)

    def create_source(self, body: dict, actor: str) -> dict:
        session_type = str(body.get("session_type", "")).strip().upper()
        session_id = str(body.get("session_id", "")).strip()
        filename = str(body.get("original_filename", "")).strip()
        content_type = str(body.get("content_type", "application/octet-stream")).strip()
        fmt = str(body.get("format", "UNKNOWN")).strip().upper()
        version = str(body.get("format_version", "UNKNOWN")).strip()
        raw = body.get("content", "")
        content = raw.encode("utf-8") if isinstance(raw, str) else bytes(raw)
        if not session_type or not session_id or not filename or not content:
            raise ValueError("session_type, session_id, original_filename and content are required")
        artifact_id, now = str(uuid4()), datetime.now(timezone.utc)
        digest = hashlib.sha256(content).hexdigest()
        with self.engine.begin() as conn:
            conn.execute(conversion_source_artifacts.insert().values(
                id=artifact_id, session_type=session_type, session_id=session_id,
                original_filename=filename, content_type=content_type, format=fmt,
                format_version=version, size_bytes=len(content), sha256=digest,
                content=content, created_by=actor, created_at=now, row_version=1,
            ))
        return self.get_source(artifact_id)

    def get_source(self, artifact_id: str) -> dict:
        with self.engine.connect() as conn:
            row = conn.execute(select(conversion_source_artifacts).where(conversion_source_artifacts.c.id == artifact_id)).mappings().first()
        if not row:
            raise LookupError("conversion source artifact not found")
        return {
            "id": row["id"], "session_type": row["session_type"], "session_id": row["session_id"],
            "original_filename": row["original_filename"], "content_type": row["content_type"],
            "format": row["format"], "format_version": row["format_version"],
            "size_bytes": row["size_bytes"], "sha256": row["sha256"],
            "created_by": row["created_by"], "created_at": row["created_at"].isoformat(),
            "row_version": row["row_version"],
            "download_url": f"/api/conversions/source-artifacts/{row['id']}/download",
        }

    def source_content(self, artifact_id: str) -> tuple[bytes, str, str]:
        with self.engine.connect() as conn:
            row = conn.execute(select(
                conversion_source_artifacts.c.content,
                conversion_source_artifacts.c.content_type,
                conversion_source_artifacts.c.original_filename,
            ).where(conversion_source_artifacts.c.id == artifact_id)).first()
        if not row:
            raise LookupError("conversion source artifact not found")
        return bytes(row[0]), row[1], row[2]

    def create_catalogue(self, body: dict, actor: str) -> dict:
        session_type = str(body.get("session_type", "")).strip().upper()
        session_id = str(body.get("session_id", "")).strip()
        errors = list(body.get("errors") or [])
        warnings = list(body.get("warnings") or [])
        if not session_type or not session_id:
            raise ValueError("session_type and session_id are required")
        catalogue_id, now = str(uuid4()), datetime.now(timezone.utc)
        payload = {"errors": errors, "warnings": warnings}
        with self.engine.begin() as conn:
            conn.execute(conversion_error_catalogues.insert().values(
                id=catalogue_id, session_type=session_type, session_id=session_id,
                error_count=len(errors), warning_count=len(warnings),
                catalogue_json=json.dumps(payload, ensure_ascii=False, sort_keys=True),
                created_by=actor, created_at=now, row_version=1,
            ))
        return self.get_catalogue(catalogue_id)

    def get_catalogue(self, catalogue_id: str) -> dict:
        with self.engine.connect() as conn:
            row = conn.execute(select(conversion_error_catalogues).where(conversion_error_catalogues.c.id == catalogue_id)).mappings().first()
        if not row:
            raise LookupError("conversion error catalogue not found")
        return {
            "id": row["id"], "session_type": row["session_type"], "session_id": row["session_id"],
            "error_count": row["error_count"], "warning_count": row["warning_count"],
            "catalogue": json.loads(row["catalogue_json"]), "created_by": row["created_by"],
            "created_at": row["created_at"].isoformat(), "row_version": row["row_version"],
            "download_url": f"/api/conversions/error-catalogues/{row['id']}/download",
        }

    def catalogue_csv(self, catalogue_id: str) -> tuple[bytes, str]:
        item = self.get_catalogue(catalogue_id)
        output = io.StringIO(newline="")
        writer = csv.writer(output)
        writer.writerow(["severity", "code", "index", "item_code", "detail"])
        for severity in ("errors", "warnings"):
            for issue in item["catalogue"][severity]:
                writer.writerow([
                    severity[:-1].upper(), issue.get("code", ""), issue.get("index", ""),
                    issue.get("item_code", ""), issue.get("detail", ""),
                ])
        return output.getvalue().encode("utf-8-sig"), f"conversion-errors-{catalogue_id}.csv"
