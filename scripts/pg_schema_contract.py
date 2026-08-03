#!/usr/bin/env python3
"""Strict PostgreSQL schema provisioning and verification helpers.

This module intentionally imports every canonical Web domain that owns SQLAlchemy
``MetaData``.  Import or DDL failures are fatal: CI must never report a migration
PASS after silently skipping a domain schema.
"""
from __future__ import annotations

import importlib
from collections.abc import Iterable

from sqlalchemy import MetaData, inspect

from api.models import Base
from api.migrations import applied_versions, run_migrations

SCHEMA_MODULES = (
    "api.authorization",
    "api.work_context",
    "api.recovery",
    "api.persistence_contract",
    "api.budget_decimal",
    "api.budget_versioning",
    "api.budget_approval",
    "api.budget_validation",
    "api.bid_lifecycle",
    "api.mrs_catalog",
    "api.mrs_operations",
    "api.mrs_governance",
    "api.mrs_intelligence",
    "api.mrs_project_state",
    "api.resource_decimal",
    "api.cost_structure",
    "api.cost_structure_details",
    "api.cost_structure_project_run",
    "api.cost_structure_run_versions",
    "api.conversion_wizard",
    "api.budget_bid_conversion",
    "api.conversion_export_jobs",
    "api.conversion_export_lifecycle",
    "api.bid_budget_roundtrip",
    "api.bid_budget_import_apply",
    "api.budget_combine_bid",
    "api.legacy_exchange_adapters",
    "api.conversion_source_artifacts",
    "api.conversion_long_jobs",
    "api.contract_core",
    "api.contract_allocation",
    "api.contract_governance",
    "api.contract_changes",
    "api.contract_change_governance",
    "api.contract_execution",
    "api.report_center",
    "api.admin_console",
)


def domain_metadata() -> list[tuple[str, MetaData]]:
    result: list[tuple[str, MetaData]] = [("api.models", Base.metadata)]
    seen: set[int] = {id(Base.metadata)}
    for module_name in SCHEMA_MODULES:
        module = importlib.import_module(module_name)
        value = getattr(module, "metadata", None)
        if not isinstance(value, MetaData):
            raise RuntimeError(f"{module_name} does not expose SQLAlchemy metadata")
        if id(value) not in seen:
            result.append((module_name, value))
            seen.add(id(value))
    return result


def expected_tables(registry: Iterable[tuple[str, MetaData]] | None = None) -> dict[str, object]:
    tables: dict[str, object] = {}
    for module_name, metadata in registry or domain_metadata():
        for name, table in metadata.tables.items():
            if name in tables and tables[name] is not table:
                # Duplicate table declarations are allowed only when structurally equal.
                old_columns = tuple(tables[name].columns.keys())
                new_columns = tuple(table.columns.keys())
                if old_columns != new_columns:
                    raise RuntimeError(
                        f"conflicting table declarations for {name}: "
                        f"{old_columns} != {new_columns} ({module_name})"
                    )
            else:
                tables[name] = table
    return tables


def provision_schema(engine) -> list[str]:
    registry = domain_metadata()  # Import failures must stop the build.
    Base.metadata.create_all(engine)
    migrated = run_migrations(engine)  # Migration failures must stop the build.
    for _, metadata in registry:
        metadata.create_all(engine)
    return migrated


def verify_schema(engine) -> dict[str, int]:
    registry = domain_metadata()
    expected = expected_tables(registry)
    inspector = inspect(engine)
    actual = set(inspector.get_table_names())
    missing = sorted(set(expected) - actual)
    if missing:
        raise RuntimeError(f"missing PostgreSQL tables: {missing}")

    checked_columns = checked_pks = checked_fks = 0
    for table_name, table in expected.items():
        actual_columns = {column["name"] for column in inspector.get_columns(table_name)}
        expected_columns = set(table.columns.keys())
        missing_columns = sorted(expected_columns - actual_columns)
        if missing_columns:
            raise RuntimeError(f"{table_name} missing columns: {missing_columns}")
        checked_columns += len(expected_columns)

        expected_pk = {column.name for column in table.primary_key.columns}
        actual_pk = set(inspector.get_pk_constraint(table_name).get("constrained_columns") or [])
        if expected_pk != actual_pk:
            raise RuntimeError(f"{table_name} primary key mismatch: {actual_pk} != {expected_pk}")
        checked_pks += 1

        expected_fk_targets = {
            fk.target_fullname for column in table.columns for fk in column.foreign_keys
        }
        actual_fk_targets = {
            f"{fk['referred_table']}.{column}"
            for fk in inspector.get_foreign_keys(table_name)
            for column in (fk.get("referred_columns") or [])
        }
        if not expected_fk_targets.issubset(actual_fk_targets):
            missing_fks = sorted(expected_fk_targets - actual_fk_targets)
            raise RuntimeError(f"{table_name} missing foreign keys: {missing_fks}")
        checked_fks += len(expected_fk_targets)

    versions = applied_versions(engine)
    if not versions:
        raise RuntimeError("web_schema_migrations contains no applied versions")

    return {
        "tables": len(expected),
        "columns": checked_columns,
        "primary_keys": checked_pks,
        "foreign_keys": checked_fks,
        "migration_versions": len(versions),
    }
