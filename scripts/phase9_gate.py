#!/usr/bin/env python3
"""Phase 9 structural and final-completion gate."""
from __future__ import annotations

import argparse
import re
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]
MATRIX = ROOT / "docs/development/phase5-9-traceability-matrix.md"
REQUIRED = [
    "api/contract_core.py", "api/contract_allocation.py", "api/contract_governance.py",
    "api/contract_change_governance.py", "api/contract_execution.py", "api/report_center.py",
    "api/admin_console.py", "web-pcces/frontend/src/api/roadmapClient.ts",
    "pcces-go/internal/storage/sqlite/contract_core.go",
    "pcces-go/internal/storage/sqlite/contract_execution.go",
    "pcces-go/internal/storage/sqlite/report_admin.go",
]
ROUTES = {
    "api/app.py": ["contract_execution", "report_center", "admin_console"],
    "pcces-go/internal/platform/httpapi/authorization_handlers.go": ["contractExecutionRoutes", "reportAdminRoutes"],
}


def main() -> int:
    parser = argparse.ArgumentParser()
    parser.add_argument("--require-complete", action="store_true")
    args = parser.parse_args()
    errors: list[str] = []
    for rel in REQUIRED:
        if not (ROOT / rel).is_file():
            errors.append(f"missing required implementation: {rel}")
    for rel, tokens in ROUTES.items():
        text = (ROOT / rel).read_text(encoding="utf-8")
        for token in tokens:
            if token not in text:
                errors.append(f"missing canonical registration {token} in {rel}")
    matrix = MATRIX.read_text(encoding="utf-8")
    rows = re.findall(r"^\| P[5-9]-\d+ .*\| (VERIFIED|INTEGRATION_TESTING|OPEN) \|$", matrix, flags=re.M)
    if len(rows) < 20:
        errors.append(f"traceability matrix too small: {len(rows)} rows")
    if args.require_complete:
        incomplete = [status for status in rows if status != "VERIFIED"]
        if incomplete:
            errors.append(f"final gate blocked: {len(incomplete)} rows are not VERIFIED")
    if errors:
        print("PHASE9 GATE FAILED")
        for error in errors:
            print(f"- {error}")
        return 1
    print(f"PHASE9 STRUCTURAL GATE PASSED: {len(rows)} traceability rows")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
