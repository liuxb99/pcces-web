#!/usr/bin/env python3
"""Executable Phase 3 parity gate for local and CI validation."""
from __future__ import annotations

import re
import sys
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]
MATRIX = ROOT / "docs/development/phase-3-mrs-traceability-matrix.md"
REQUIRED_FILES = [
    "api/mrs_catalog.py",
    "api/mrs_operations.py",
    "api/mrs_history_apply.py",
    "api/mrs_precision_policy.py",
    "api/mrs_excel_export.py",
    "api/mrs_project_state.py",
    "api/resource_budget_lineage.py",
    "pcces-go/internal/storage/sqlite/mrs_catalog_repository.go",
    "pcces-go/internal/storage/sqlite/mrs_precision_policy_repository.go",
    "pcces-go/internal/storage/sqlite/mrs_project_state_repository.go",
    "pcces-go/internal/platform/httpapi/mrs_catalog_handlers.go",
]
FORBIDDEN = ("PARTIAL", "STUB", "TODO", "NOT_STARTED")


def fail(message: str) -> None:
    print(f"PHASE3_GATE_FAILED: {message}", file=sys.stderr)
    raise SystemExit(1)


def main() -> None:
    if not MATRIX.exists():
        fail(f"missing traceability matrix: {MATRIX.relative_to(ROOT)}")
    text = MATRIX.read_text(encoding="utf-8")
    ids = sorted(set(re.findall(r"P3-[A-Z0-9-]+", text)))
    if len(ids) < 20:
        fail(f"expected at least 20 Phase 3 feature IDs, found {len(ids)}")
    for token in FORBIDDEN:
        if token in text:
            fail(f"matrix contains forbidden status token {token}")
    verified = len(re.findall(r"\bVERIFIED\b", text))
    if verified < 20:
        fail(f"expected at least 20 VERIFIED entries, found {verified}")
    missing = [path for path in REQUIRED_FILES if not (ROOT / path).exists()]
    if missing:
        fail("missing required implementation files: " + ", ".join(missing))
    print(f"PHASE3_GATE_OK features={len(ids)} verified={verified} files={len(REQUIRED_FILES)}")


if __name__ == "__main__":
    main()
