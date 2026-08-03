#!/usr/bin/env python3
"""Fail-closed PostgreSQL migration and schema verification."""
from __future__ import annotations

import argparse
import json
import os
import sys

sys.path.insert(0, os.path.dirname(os.path.dirname(os.path.abspath(__file__))))

from sqlalchemy import create_engine, text

from scripts.pg_schema_contract import provision_schema, verify_schema


def get_engine():
    url = os.environ.get("DATABASE_URL")
    if not url:
        raise RuntimeError("DATABASE_URL is required")
    if not url.startswith(("postgresql://", "postgresql+psycopg://", "postgresql+psycopg2://")):
        raise RuntimeError("PostgreSQL verification requires a PostgreSQL DATABASE_URL")
    return create_engine(url, echo=False, pool_pre_ping=True)


def main() -> int:
    parser = argparse.ArgumentParser()
    parser.add_argument("--verify", action="store_true")
    parser.add_argument("--expect-empty", action="store_true")
    args = parser.parse_args()

    engine = get_engine()
    with engine.connect() as conn:
        dialect = conn.dialect.name
        if dialect != "postgresql":
            raise RuntimeError(f"expected PostgreSQL, got {dialect}")
        if args.expect_empty:
            before = conn.execute(text(
                "SELECT COUNT(*) FROM information_schema.tables "
                "WHERE table_schema='public' AND table_type='BASE TABLE'"
            )).scalar_one()
            if before != 0:
                raise RuntimeError(f"database is not empty before migration: {before} tables")

    migrated = provision_schema(engine)
    result = verify_schema(engine) if args.verify else {}
    result["applied_now"] = len(migrated)
    print(json.dumps(result, ensure_ascii=False, sort_keys=True))
    print("PostgreSQL migration PASSED")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
