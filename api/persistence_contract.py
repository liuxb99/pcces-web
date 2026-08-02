"""Phase 0 persistence primitives for exact values, audit and CAS updates."""

from __future__ import annotations

import json
from datetime import datetime, timezone
from decimal import Decimal

from sqlalchemy import Column, DateTime, Integer, MetaData, Numeric, String, Table, Text, and_, select

from api.decimal_math import parse_decimal, quantize

metadata = MetaData()

decimal_records = Table(
    "p0_decimal_records",
    metadata,
    Column("id", String(100), primary_key=True),
    Column("value", Numeric(28, 8), nullable=False),
    Column("created_at", DateTime(timezone=True), nullable=False),
    Column("updated_at", DateTime(timezone=True), nullable=False),
    Column("row_version", Integer, nullable=False, default=1),
)

p0_audit_events = Table(
    "p0_audit_events",
    metadata,
    Column("id", Integer, primary_key=True, autoincrement=True),
    Column("actor_id", String(100), nullable=True),
    Column("feature_id", String(64), nullable=False),
    Column("action_code", String(64), nullable=True),
    Column("event_type", String(100), nullable=False),
    Column("resource_type", String(100), nullable=True),
    Column("resource_id", String(100), nullable=True),
    Column("payload", Text, nullable=True),
    Column("created_at", DateTime(timezone=True), nullable=False),
)


class PersistenceService:
    def __init__(self, engine):
        self.engine = engine

    def create_schema(self) -> None:
        metadata.create_all(self.engine)

    def create_decimal(self, record_id: str, value: str, actor_id: str | None = None) -> dict:
        now = datetime.now(timezone.utc)
        decimal_value = parse_decimal(quantize(value, 8))
        with self.engine.begin() as conn:
            conn.execute(decimal_records.insert().values(
                id=record_id,
                value=decimal_value,
                created_at=now,
                updated_at=now,
                row_version=1,
            ))
            self._append_audit(conn, actor_id, "P0-S2", "DECIMAL_CREATE", "decimal_record", record_id, {"value": quantize(decimal_value, 8)})
        return self.get_decimal(record_id)

    def update_decimal(self, record_id: str, value: str, row_version: int, actor_id: str | None = None) -> tuple[dict, int]:
        now = datetime.now(timezone.utc)
        decimal_value = parse_decimal(quantize(value, 8))
        with self.engine.begin() as conn:
            result = conn.execute(decimal_records.update().where(and_(
                decimal_records.c.id == record_id,
                decimal_records.c.row_version == row_version,
            )).values(value=decimal_value, updated_at=now, row_version=row_version + 1))
            if result.rowcount != 1:
                return {"code": "CONFLICT", "detail": "stale row_version"}, 409
            self._append_audit(conn, actor_id, "P0-S2", "DECIMAL_UPDATE", "decimal_record", record_id, {
                "value": quantize(decimal_value, 8),
                "previous_row_version": row_version,
                "row_version": row_version + 1,
            })
        return self.get_decimal(record_id), 200

    def get_decimal(self, record_id: str) -> dict:
        with self.engine.connect() as conn:
            row = conn.execute(select(decimal_records).where(decimal_records.c.id == record_id)).mappings().one()
        item = dict(row)
        item["value"] = quantize(Decimal(item["value"]), 8)
        return item

    def list_audit(self, resource_id: str) -> list[dict]:
        with self.engine.connect() as conn:
            rows = conn.execute(select(p0_audit_events).where(
                p0_audit_events.c.resource_id == resource_id
            ).order_by(p0_audit_events.c.id)).mappings().all()
        return [dict(row) for row in rows]

    @staticmethod
    def _append_audit(conn, actor_id, feature_id, event_type, resource_type, resource_id, payload) -> None:
        conn.execute(p0_audit_events.insert().values(
            actor_id=actor_id,
            feature_id=feature_id,
            action_code=None,
            event_type=event_type,
            resource_type=resource_type,
            resource_id=resource_id,
            payload=json.dumps(payload, ensure_ascii=False, sort_keys=True),
            created_at=datetime.now(timezone.utc),
        ))
