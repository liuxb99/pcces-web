#!/usr/bin/env python3
"""PostgreSQL smoke test — verifies basic CRUD operations."""
from __future__ import annotations

import os
import sys

sys.path.insert(0, os.path.dirname(os.path.dirname(os.path.abspath(__file__))))

from sqlalchemy import create_engine, text
from sqlalchemy.orm import Session
from api.models import Base, User, Project, UserRole


def get_engine():
    url = os.environ.get("DATABASE_URL", "postgresql://pcces:pcces123@localhost:5432/pcces")
    return create_engine(url, echo=False)


def main():
    engine = get_engine()

    # Verify tables
    inspector = __import__("sqlalchemy").inspect(engine)
    tables = inspector.get_table_names()
    required = ["users", "projects"]
    missing = [t for t in required if t not in tables]
    if missing:
        print(f"FAIL: missing {missing}")
        sys.exit(1)
    print(f"Tables OK: {len(tables)}")

    # CRUD test
    with Session(engine) as session:
        session.execute(text("DELETE FROM budget_items"))
        session.execute(text("DELETE FROM projects"))
        session.execute(text("DELETE FROM users"))
        session.commit()

        user = User(username="smoke_test", password_hash="x", display_name="Smoke", role=UserRole.ADMIN.value)
        session.add(user)
        session.flush()

        project = Project(code="SMOKE-1", name="Smoke Project", owner_id=user.id)
        session.add(project)
        session.commit()

        u = session.query(User).filter_by(username="smoke_test").first()
        p = session.query(Project).filter_by(code="SMOKE-1").first()
        assert u and p and p.owner_id == u.id, "CRUD assertion failed"

        session.delete(p)
        session.delete(u)
        session.commit()

    print("Smoke test PASSED")


if __name__ == "__main__":
    main()
