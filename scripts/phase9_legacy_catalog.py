#!/usr/bin/env python3
"""Scan every Legacy C# source node and assign a deterministic Web feature family."""
from __future__ import annotations

import argparse
import json
import re
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]
SOURCE = ROOT / "PCCES_CS"
RULES = [
    ("SplitContract", "P5-CONTRACT"), ("DomainModule.SubChg", "P5-CHANGE"), ("DomainModule.Sub", "P5-CONTRACT"),
    ("BudgetChange", "P6-CHANGE"), ("Invoice", "P6-INVOICE"), ("SubClose", "P6-SETTLEMENT"), ("SubFinal", "P6-ACCEPTANCE"),
    ("Report", "P7-REPORT"), ("ExportExcel", "P7-REPORT"),
    ("SysMaintain", "P8-ADMIN"), ("DatabaseUpgrade", "P8-ADMIN"), ("SysUser", "P8-ADMIN"),
    ("MrsBase", "P3-MRS"), ("CostStructure", "P4-COST"), ("Conversion", "P4-CONVERSION"),
    ("Budget", "P2-BUDGET"), ("Project", "P1-PROJECT"), ("PccesMain", "P0-PLATFORM"),
    ("Common", "P0-PLATFORM"), ("DomainModule", "P0-PLATFORM"),
]
CLASS_RE = re.compile(r"\b(?:class|struct|enum|interface)\s+([A-Za-z_][A-Za-z0-9_]*)")


def family(path: str) -> str:
    for token, feature in RULES:
        if token.lower() in path.lower():
            return feature
    return "UNKNOWN"


def scan() -> list[dict]:
    entries: list[dict] = []
    for file in sorted(SOURCE.rglob("*.cs")):
        rel = file.relative_to(ROOT).as_posix()
        text = file.read_text(encoding="utf-8", errors="replace")
        classes = CLASS_RE.findall(text) or [file.stem]
        generated = file.name.endswith(".Designer.cs") or "AssemblyInfo" in file.name
        for name in classes:
            entries.append({
                "source": rel,
                "node": name,
                "feature_family": family(rel),
                "decision": "GENERATED_SUPPORT" if generated else "REPLICATE",
                "status": "MAPPED" if family(rel) != "UNKNOWN" else "UNKNOWN",
            })
    return entries


def main() -> int:
    parser = argparse.ArgumentParser()
    parser.add_argument("--output", default="build/phase9-legacy-catalog.json")
    parser.add_argument("--require-no-unknown", action="store_true")
    args = parser.parse_args()
    entries = scan()
    output = ROOT / args.output
    output.parent.mkdir(parents=True, exist_ok=True)
    output.write_text(json.dumps({"count": len(entries), "entries": entries}, ensure_ascii=False, indent=2), encoding="utf-8")
    unknown = [entry for entry in entries if entry["status"] == "UNKNOWN"]
    print(f"Legacy nodes: {len(entries)}; unknown: {len(unknown)}; output: {output}")
    if args.require_no_unknown and unknown:
        for entry in unknown[:50]:
            print(f"UNKNOWN {entry['source']}::{entry['node']}")
        return 1
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
