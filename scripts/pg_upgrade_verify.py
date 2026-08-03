#!/usr/bin/env python3
"""Verify an additive PostgreSQL upgrade preserves existing business data.

The fixture represents an earlier deployed schema: core user/project/budget data
exists while later contract/report/admin tables are absent. The current schema
provisioner must recreate every missing domain table without changing legacy
rows, and a second run must be idempotent.
"""
from __future__ import annotations

import json
import os
import sys

sys.path.insert(0, os.path.dirname(os.path.dirname(os.path.abspath(__file__))))

from sqlalchemy import create_engine, inspect, select, text
from sqlalchemy.orm import Session

from api.models import BudgetItem, Project, User, UserRole
from scripts.pg_schema_contract import expected_tables, provision_schema, verify_schema

MARKER_USER = "legacy_upgrade_user"
MARKER_PROJECT = "LEGACY-UPGRADE-P1"
MARKER_ITEM_NO = "LEG-001"


def engine_from_env():
    url = os.environ.get("DATABASE_URL")
    if not url or not url.startswith("postgresql"):
        raise RuntimeError("DATABASE_URL must point to PostgreSQL")
    return create_engine(url, pool_pre_ping=True)


def seed_legacy_rows(engine) -> dict[str, int]:
    with Session(engine) as session:
        user = session.execute(select(User).where(User.username == MARKER_USER)).scalar_one_or_none()
        if user is None:
            user = User(
                username=MARKER_USER,
                password_hash="legacy-fixture",
                display_name="Legacy Upgrade User",
                role=UserRole.ADMIN.value,
            )
            session.add(user)
            session.flush()

        project = session.execute(select(Project).where(Project.code == MARKER_PROJECT)).scalar_one_or_none()
        if project is None:
            project = Project(code=MARKER_PROJECT, name="Legacy Upgrade Project", owner_id=user.id)
            session.add(project)
            session.flush()

        item = session.execute(
            select(BudgetItem).where(
                BudgetItem.project_id == project.id,
                BudgetItem.item_no == MARKER_ITEM_NO,
            )
        ).scalar_one_or_none()
        if item is None:
            item = BudgetItem(
                project_id=project.id,
                item_no=MARKER_ITEM_NO,
                c_name="Legacy Concrete",
                c_unit="m3",
                quantity=2,
                unit_price=125,
                amount=250,
                kind="L",
            )
            session.add(item)
        session.commit()
        return {"user_id": int(user.id), "project_id": int(project.id), "item_id": int(item.id)}


def remove_later_schema(engine) -> list[str]:
    candidates = (
        "report_download_audit",
        "report_artifacts",
        "report_jobs",
        "report_definitions",
        "setting_values",
        "setting_definitions",
        "admin_group_members",
        "admin_groups",
        "contract_change_case_items_v2",
        "contract_change_cases_v2",
        "acceptance_defects_v2",
        "acceptances_v2",
        "settlements_v2",
        "invoice_lines_v2",
        "invoice_periods_v2",
        "contract_versions_v2",
        "contract_items_v2",
        "contracts_v2",
    )
    known = set(expected_tables())
    dropped: list[str] = []
    with engine.begin() as conn:
        for name in candidates:
            if name in known:
                conn.execute(text(f'DROP TABLE IF EXISTS "{name}" CASCADE'))
                dropped.append(name)
    if not dropped:
        raise RuntimeError("upgrade fixture did not remove any later-domain table")
    return dropped


def assert_legacy_rows(engine, marker: dict[str, int]) -> None:
    with Session(engine) as session:
        user = session.get(User, marker["user_id"])
        project = session.get(Project, marker["project_id"])
        item = session.get(BudgetItem, marker["item_id"])
        if user is None or user.username != MARKER_USER:
            raise RuntimeError("legacy user was lost or changed during upgrade")
        if project is None or project.code != MARKER_PROJECT:
            raise RuntimeError("legacy project was lost or changed during upgrade")
        if item is None or item.item_no != MARKER_ITEM_NO:
            raise RuntimeError("legacy budget item was lost or changed during upgrade")
        if item.c_name != "Legacy Concrete" or item.c_unit != "m3":
            raise RuntimeError("legacy budget descriptive fields changed during upgrade")
        if float(item.quantity) != 2 or float(item.unit_price) != 125 or float(item.amount) != 250:
            raise RuntimeError(
                "legacy budget values changed during upgrade: "
                f"quantity={item.quantity}, unit_price={item.unit_price}, amount={item.amount}"
            )


def main() -> int:
    engine = engine_from_env()
    provision_schema(engine)
    marker = seed_legacy_rows(engine)
    dropped = remove_later_schema(engine)

    provision_schema(engine)
    first = verify_schema(engine)
    assert_legacy_rows(engine, marker)
    tables_after_first = set(inspect(engine).get_table_names())

    provision_schema(engine)
    second = verify_schema(engine)
    assert_legacy_rows(engine, marker)
    tables_after_second = set(inspect(engine).get_table_names())
    if tables_after_first != tables_after_second:
        raise RuntimeError("schema changed on idempotent second migration run")

    print(json.dumps({"dropped_and_restored": dropped, "first": first, "second": second}, sort_keys=True))
    print("PostgreSQL upgrade PASSED")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
