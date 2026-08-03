#!/usr/bin/env python3
"""Compare a restored PostgreSQL database with its source database."""
from __future__ import annotations

import hashlib
import json
import os
import sys
from pathlib import Path

from sqlalchemy import create_engine, inspect, text


def engine(url_name: str):
    url = os.environ.get(url_name)
    if not url or not url.startswith("postgresql"):
        raise RuntimeError(f"{url_name} must point to PostgreSQL")
    return create_engine(url, pool_pre_ping=True)


def table_manifest(db_engine) -> dict[str, dict[str, object]]:
    inspector = inspect(db_engine)
    result: dict[str, dict[str, object]] = {}
    with db_engine.connect() as conn:
        for table in sorted(inspector.get_table_names()):
            quoted = '"' + table.replace('"', '""') + '"'
            count = conn.execute(text(f"SELECT COUNT(*) FROM {quoted}")).scalar_one()
            result[table] = {
                "rows": int(count),
                "columns": sorted(column["name"] for column in inspector.get_columns(table)),
            }
        if "budget_items" in result:
            result["budget_items"]["amount_sum"] = str(
                conn.execute(text("SELECT COALESCE(SUM(amount), 0) FROM budget_items")).scalar_one()
            )
        if "contracts_v2" in result:
            result["contracts_v2"]["amount_sum"] = str(
                conn.execute(text("SELECT COALESCE(SUM(contract_amount), 0) FROM contracts_v2")).scalar_one()
            )
    return result


def main() -> int:
    source = table_manifest(engine("DATABASE_URL"))
    restored = table_manifest(engine("RESTORE_DATABASE_URL"))
    if source != restored:
        missing = sorted(set(source) - set(restored))
        extra = sorted(set(restored) - set(source))
        changed = sorted(name for name in set(source) & set(restored) if source[name] != restored[name])
        raise RuntimeError(
            f"restore mismatch: missing={missing}, extra={extra}, changed={changed}"
        )

    dump_path = Path(os.environ.get("PG_DUMP_FILE", "/tmp/pcces.dump"))
    if not dump_path.is_file() or dump_path.stat().st_size == 0:
        raise RuntimeError(f"dump file missing or empty: {dump_path}")
    digest = hashlib.sha256(dump_path.read_bytes()).hexdigest()
    print(json.dumps({"tables": len(source), "sha256": digest, "size_bytes": dump_path.stat().st_size}, sort_keys=True))
    print("PostgreSQL backup/restore PASSED")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
