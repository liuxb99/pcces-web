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
    ("Report.WebDownload", "P7-REPORT"), ("ExportExcel", "P7-REPORT"), ("Crystal", "P7-REPORT"), ("Report", "P7-REPORT"), ("ShellLib", "P7-REPORT"),
    ("SysMaintain", "P8-ADMIN"), ("DatabaseUpgrade", "P8-ADMIN"), ("SysUser", "P8-ADMIN"),
    ("AddOnDownLoad", "P8-INTEGRATION"), ("AddOn", "P8-INTEGRATION"), ("Proxy", "P8-INTEGRATION"), ("Registration", "P8-INTEGRATION"), ("Update", "P8-INTEGRATION"),
    ("CODECHECK", "P3-MRS"),
    ("MrsBase", "P3-MRS"), ("CostStructure", "P4-COST"), ("Conversion", "P4-CONVERSION"), ("XMLClass", "P4-CONVERSION"), ("/XML/", "P4-CONVERSION"),
    ("BUDClass", "P2-BUDGET"), ("Budget", "P2-BUDGET"), ("Project", "P1-PROJECT"),
    ("ArchControls", "P0-PLATFORM"), ("PccesMain", "P0-PLATFORM"), ("CommonMethods", "P0-PLATFORM"),
    ("DBClass", "P0-PLATFORM"), ("PubTools", "P0-PLATFORM"), ("ModuleManager", "P0-PLATFORM"),
    ("PccesFormAction", "P0-PLATFORM"), ("ModifyDB", "P0-PLATFORM"), ("Common", "P0-PLATFORM"),
    ("DomainModule", "P0-PLATFORM"), ("Archnowledge.Pcces", "P0-PLATFORM"),
    ("AssemblyInfo", "P0-PLATFORM"), ("obj/", "P0-PLATFORM"), ("Class1.cs", "P0-PLATFORM"), ("Class2.cs", "P0-PLATFORM"), ("COM.cs", "P8-INTEGRATION"),
]
CLASS_RE = re.compile(r"\b(?:class|struct|enum|interface)\s+([A-Za-z_][A-Za-z0-9_]*)")


def family(path: str) -> str:
    normalized = "/" + path.replace("\\", "/")
    for token, feature in RULES:
        if token.lower() in normalized.lower():
            return feature
    return "UNKNOWN"


def scan() -> list[dict]:
    entries: list[dict] = []
    for file in sorted(SOURCE.rglob("*.cs")):
        rel = file.relative_to(ROOT).as_posix()
        text = file.read_text(encoding="utf-8", errors="replace")
        classes = CLASS_RE.findall(text) or [file.stem]
        generated = file.name.endswith(".Designer.cs") or "AssemblyInfo" in file.name or file.name.endswith(".resx.cs")
        feature = family(rel)
        for name in classes:
            entries.append({
                "source": rel,
                "node": name,
                "feature_family": feature,
                "decision": "GENERATED_SUPPORT" if generated else "REPLICATE",
                "status": "MAPPED" if feature != "UNKNOWN" else "UNKNOWN",
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
    by_family: dict[str, int] = {}
    for entry in entries:
        by_family[entry["feature_family"]] = by_family.get(entry["feature_family"], 0) + 1
    output.write_text(json.dumps({"count": len(entries), "by_family": by_family, "entries": entries}, ensure_ascii=False, indent=2), encoding="utf-8")
    unknown = [entry for entry in entries if entry["status"] == "UNKNOWN"]
    print(f"Legacy nodes: {len(entries)}; unknown: {len(unknown)}; output: {output}")
    if args.require_no_unknown and unknown:
        for entry in unknown[:100]:
            print(f"UNKNOWN {entry['source']}::{entry['node']}")
        return 1
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
