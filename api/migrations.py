"""Small tracked migration registry for the canonical Web entrypoint.

The existing application still contains legacy create_all calls. New Phase 0
infrastructure is registered here so application startup is idempotent and the
applied schema versions are observable.
"""

from __future__ import annotations

from datetime import datetime, timezone

from sqlalchemy import Column, DateTime, MetaData, String, Table, select

from api.authorization import metadata as authorization_metadata
from api.persistence_contract import metadata as persistence_metadata
from api.recovery import metadata as recovery_metadata
from api.work_context import metadata as work_context_metadata

metadata = MetaData()
schema_migrations = Table(
    "web_schema_migrations",
    metadata,
    Column("version", String(100), primary_key=True),
    Column("applied_at", DateTime(timezone=True), nullable=False),
)

MIGRATIONS = (
    ("0001_authorization", authorization_metadata),
    ("0002_work_context", work_context_metadata),
    ("0003_recovery", recovery_metadata),
    ("0004_decimal_audit_contract", persistence_metadata),
)


def run_migrations(engine) -> list[str]:
    metadata.create_all(engine)
    applied_now: list[str] = []
    for version, target_metadata in MIGRATIONS:
        with engine.begin() as conn:
            exists = conn.execute(select(schema_migrations.c.version).where(
                schema_migrations.c.version == version
            )).first()
            if exists is not None:
                continue
            target_metadata.create_all(conn)
            conn.execute(schema_migrations.insert().values(
                version=version,
                applied_at=datetime.now(timezone.utc),
            ))
            applied_now.append(version)
    return applied_now


def applied_versions(engine) -> list[str]:
    metadata.create_all(engine)
    with engine.connect() as conn:
        return [row[0] for row in conn.execute(select(schema_migrations.c.version).order_by(schema_migrations.c.version))]
