"""Phase 4 deterministic round-trip audit for exported/imported budget items."""
from __future__ import annotations

from decimal import Decimal, InvalidOperation, ROUND_HALF_UP


def _q(value: object, scale: int) -> Decimal:
    try:
        number = Decimal(str(value if value is not None else "0"))
    except InvalidOperation as exc:
        raise ValueError(f"invalid decimal value: {value}") from exc
    return number.quantize(Decimal(1).scaleb(-scale), rounding=ROUND_HALF_UP)


def audit_roundtrip(source_items: list[dict], imported_items: list[dict], amount_scale: int = 2) -> dict:
    if amount_scale < 0 or amount_scale > 8:
        raise ValueError("amount_scale must be between 0 and 8")
    source = {str(x.get("source_budget_item_id") or x.get("id") or "").strip(): x for x in source_items}
    imported = {str(x.get("source_budget_item_id") or x.get("id") or "").strip(): x for x in imported_items}
    if "" in source or "" in imported:
        raise ValueError("every item must preserve a round-trip lineage id")
    if len(source) != len(source_items) or len(imported) != len(imported_items):
        raise ValueError("duplicate round-trip lineage id")
    missing = sorted(source.keys() - imported.keys())
    added = sorted(imported.keys() - source.keys())
    differences = []
    source_total = Decimal("0")
    imported_total = Decimal("0")
    for item_id in sorted(source.keys() & imported.keys()):
        left, right = source[item_id], imported[item_id]
        left_amount = _q(left.get("amount", _q(left.get("quantity"), 8) * _q(left.get("unit_price"), 8)), amount_scale)
        right_amount = _q(right.get("amount", _q(right.get("quantity"), 8) * _q(right.get("unit_price"), 8)), amount_scale)
        source_total += left_amount
        imported_total += right_amount
        fields = {}
        for field in ("code", "name", "unit"):
            if str(left.get(field, "")).strip() != str(right.get(field, "")).strip():
                fields[field] = {"source": left.get(field), "imported": right.get(field)}
        for field in ("quantity", "unit_price", "amount"):
            if _q(left.get(field), amount_scale) != _q(right.get(field), amount_scale):
                fields[field] = {"source": str(_q(left.get(field), amount_scale)), "imported": str(_q(right.get(field), amount_scale))}
        if fields:
            differences.append({"source_budget_item_id": item_id, "fields": fields})
    source_total = _q(source_total, amount_scale)
    imported_total = _q(imported_total, amount_scale)
    return {
        "consistent": not missing and not added and not differences and source_total == imported_total,
        "amount_scale": amount_scale,
        "source_total": str(source_total),
        "imported_total": str(imported_total),
        "total_difference": str(_q(imported_total - source_total, amount_scale)),
        "missing_lineage_ids": missing,
        "added_lineage_ids": added,
        "item_differences": differences,
    }
