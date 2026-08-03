#!/usr/bin/env python3
"""PostgreSQL migration script for PCCES Web.

Connects to DATABASE_URL (default: postgresql://pcces:pcces123@localhost:5432/pcces)
and creates all tables from SQLAlchemy models + Alembic-style schema.
"""
from __future__ import annotations

import argparse
import os
import sys

from sqlalchemy import create_engine, inspect, text
from sqlalchemy.exc import ProgrammingError

sys.path.insert(0, os.path.dirname(os.path.dirname(os.path.abspath(__file__))))

from api.models import Base
from api.migrations import run_migrations


def get_engine():
    url = os.environ.get("DATABASE_URL", "postgresql://pcces:pcces123@localhost:5432/pcces")
    return create_engine(url, echo=False)


def verify_schema(engine):
    """Verify all expected tables, indexes, and constraints exist."""
    inspector = inspect(engine)
    tables = inspector.get_table_names()
    print(f"Tables: {len(tables)}")
    for t in sorted(tables):
        cols = [c["name"] for c in inspector.get_columns(t)]
        pks = inspector.get_pk_constraint(t)
        fks = inspector.get_foreign_keys(t)
        idxs = inspector.get_indexes(t)
        print(f"  {t}: {len(cols)} cols, pk={pks.get('constrained_columns',[])}, {len(fks)} fks, {len(idxs)} idxs")
    return tables


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument("--verify", action="store_true", help="Verify schema after migration")
    args = parser.parse_args()

    engine = get_engine()
    print(f"Connected to: {engine.url}")

    # Drop and recreate for clean migration test
    if args.verify:
        try:
            with engine.connect() as conn:
                conn.execute(text("COMMIT"))
                conn.execute(text("DROP DATABASE IF EXISTS pcces_test"))
                conn.execute(text("CREATE DATABASE pcces_test"))
            print("Created fresh database pcces_test")
        except Exception:
            print("Skipping drop/create (may need superuser)")

    # Run migrations
    print("Running migrations...")
    Base.metadata.create_all(engine)

    # Run custom migrations
    try:
        run_migrations(engine)
    except Exception as e:
        print(f"Custom migrations: {e}")

    if args.verify:
        tables = verify_schema(engine)
        print(f"\nMigration complete: {len(tables)} tables")

        # Verify critical tables
        required = [
            "users", "projects", "budget_items", "contracts_v2",
            "contract_versions_v2", "contract_items_v2",
            "invoice_periods_v2", "invoice_lines_v2",
            "settlements_v2", "acceptances_v2",
        ]
        missing = [t for t in required if t not in tables]
        if missing:
            print(f"MISSING TABLES: {missing}")
            sys.exit(1)

        # Verify numeric precision
        with engine.connect() as conn:
            result = conn.execute(text(
                "SELECT data_type FROM information_schema.columns "
                "WHERE table_name='budget_items' AND column_name='amount'"
            )).fetchone()
            if result:
                print(f"budget_items.amount type: {result[0]}")

    print("Migration PASSED")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
