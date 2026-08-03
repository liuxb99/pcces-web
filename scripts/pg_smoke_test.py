#!/usr/bin/env python3
"""PostgreSQL smoke test for canonical user/project/budget persistence."""
from __future__ import annotations

import json
import os
import sys

sys.path.insert(0, os.path.dirname(os.path.dirname(os.path.abspath(__file__))))

from sqlalchemy import create_engine, delete, select
from sqlalchemy.orm import Session

from api.models import BudgetItem, Project, User, UserRole
from scripts.pg_schema_contract import verify_schema


def get_engine():
    url = os.environ.get("DATABASE_URL")
    if not url or not url.startswith("postgresql"):
        raise RuntimeError("DATABASE_URL must point to PostgreSQL")
    return create_engine(url, pool_pre_ping=True)


def main() -> int:
    engine = get_engine()
    schema = verify_schema(engine)

    with Session(engine) as session:
        # Delete only this test's rows; never clear unrelated integration fixtures.
        existing_project = session.execute(select(Project).where(Project.code == "SMOKE-1")).scalar_one_or_none()
        if existing_project is not None:
            session.execute(delete(BudgetItem).where(BudgetItem.project_id == existing_project.id))
            session.delete(existing_project)
        existing_user = session.execute(select(User).where(User.username == "smoke_test")).scalar_one_or_none()
        if existing_user is not None:
            session.delete(existing_user)
        session.commit()

        user = User(
            username="smoke_test",
            password_hash="x",
            display_name="Smoke",
            role=UserRole.ADMIN.value,
        )
        session.add(user)
        session.flush()
        project = Project(code="SMOKE-1", name="Smoke Project", owner_id=user.id)
        session.add(project)
        session.flush()
        item = BudgetItem(
            project_id=project.id,
            item_no="SMOKE-001",
            c_name="Concrete",
            c_unit="m3",
            quantity=2,
            unit_price=125,
            amount=250,
            kind="L",
        )
        session.add(item)
        session.commit()

        loaded = session.execute(
            select(BudgetItem).join(Project).where(Project.code == "SMOKE-1")
        ).scalar_one()
        if loaded.item_no != "SMOKE-001" or float(loaded.amount) != 250:
            raise RuntimeError("PostgreSQL budget CRUD assertion failed")

    print(json.dumps({"schema": schema, "budget_amount": "250"}, sort_keys=True))
    print("PostgreSQL smoke test PASSED")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
