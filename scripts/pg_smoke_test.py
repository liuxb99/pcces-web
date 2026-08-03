#!/usr/bin/env python3
"""PostgreSQL smoke test — verifies basic CRUD operations after migration."""
from __future__ import annotations

import os
import sys

from sqlalchemy import create_engine, text

sys.path.insert(0, os.path.dirname(os.path.dirname(os.path.abspath(__file__))))

from api.models import Base, User, Project, UserRole
from sqlalchemy.orm import Session


def get_engine():
    url = os.environ.get("DATABASE_URL", "postgresql://pcces:pcces123@localhost:5432/pcces")
    return create_engine(url, echo=False)


def main():
    engine = get_engine()
    print(f"Smoke test: {engine.url}")

    # Verify tables exist
    inspector = __import__("sqlalchemy").inspect(engine)
    tables = inspector.get_table_names()
    required = ["users", "projects"]
    missing = [t for t in required if t not in tables]
    if missing:
        print(f"FAIL: missing tables {missing}")
        sys.exit(1)

    # Create test user
    with Session(engine) as session:
        # Clean up
        session.execute(text("DELETE FROM budget_items"))
        session.execute(text("DELETE FROM projects"))
        session.execute(text("DELETE FROM users"))
        session.commit()

        # Insert
        user = User(username="test", password_hash="x", display_name="Test", role=UserRole.ADMIN.value)
        session.add(user)
        session.flush()

        project = Project(code="P-TEST", name="Smoke Project", owner_id=user.id)
        session.add(project)
        session.commit()

        # Verify
        u = session.query(User).filter_by(username="test").first()
        p = session.query(Project).filter_by(code="P-TEST").first()
        assert u is not None, "User not found"
        assert p is not None, "Project not found"
        assert p.owner_id == u.id, "Owner mismatch"

        # Clean up
        session.delete(p)
        session.delete(u)
        session.commit()

    # Verify cleanup
    with Session(engine) as session:
        assert session.query(User).count() == 0
        assert session.query(Project).count() == 0

    print("Smoke test PASSED")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
