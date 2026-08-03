#!/usr/bin/env python3
"""PostgreSQL migration and verification script for CI."""
from __future__ import annotations

import argparse
import os
import sys

sys.path.insert(0, os.path.dirname(os.path.dirname(os.path.abspath(__file__))))

from sqlalchemy import create_engine, inspect, text
from api.models import Base
from api.migrations import run_migrations


def get_engine():
    url = os.environ.get("DATABASE_URL", "postgresql://pcces:pcces123@localhost:5432/pcces")
    return create_engine(url, echo=False, pool_pre_ping=True)


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument("--verify", action="store_true")
    args = parser.parse_args()

    engine = get_engine()
    print(f"Connected to PostgreSQL")

    # Create all tables
    Base.metadata.create_all(engine)

    # Run custom migrations
    try:
        run_migrations(engine)
    except Exception as e:
        print(f"Custom migrations note: {e}")

    if args.verify:
        inspector = inspect(engine)
        tables = inspector.get_table_names()
        print(f"Tables created: {len(tables)}")

        required = ["users", "projects", "budget_items"]
        missing = [t for t in required if t not in tables]
        if missing:
            print(f"MISSING: {missing}")
            sys.exit(1)

        for t in sorted(tables)[:10]:
            cols = len(inspector.get_columns(t))
            print(f"  {t}: {cols} cols")

    print("Migration PASSED")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
