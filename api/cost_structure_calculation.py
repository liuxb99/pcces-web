"""Phase 4 cost-structure initialization and deterministic fee calculation."""
from __future__ import annotations

from decimal import Decimal, ROUND_HALF_UP
from flask import Blueprint, jsonify, request

ALLOWED_KINDS = {"DIRECT", "INDIRECT", "MANAGEMENT", "TAX", "PERCENT", "ADJUSTMENT"}
ALLOWED_BASES = {"DIRECT", "SUBTOTAL", "PREVIOUS", "FIXED"}


def q(value: Decimal, scale: int) -> Decimal:
    unit = Decimal(1).scaleb(-scale)
    return value.quantize(unit, rounding=ROUND_HALF_UP)


def calculate_cost_structure(lines: list[dict], direct_cost: str, scale: int = 2) -> dict:
    if not 0 <= scale <= 8:
        raise ValueError("scale must be between 0 and 8")
    direct = Decimal(str(direct_cost))
    ordered = sorted(lines, key=lambda item: (int(item.get("sort_order", 0)), str(item.get("code", ""))))
    seen: set[str] = set()
    subtotal = direct
    previous = Decimal("0")
    results: list[dict] = []
    for raw in ordered:
        code = str(raw.get("code", "")).strip().upper()
        kind = str(raw.get("kind", "")).strip().upper()
        base_kind = str(raw.get("base_kind", "SUBTOTAL")).strip().upper()
        if not code or code in seen:
            raise ValueError("line code is required and must be unique")
        if kind not in ALLOWED_KINDS:
            raise ValueError(f"unsupported kind: {kind}")
        if base_kind not in ALLOWED_BASES:
            raise ValueError(f"unsupported base_kind: {base_kind}")
        seen.add(code)
        sign = int(raw.get("sign", 1))
        if sign not in {-1, 1}:
            raise ValueError("sign must be -1 or 1")
        rate = Decimal(str(raw.get("rate", "0")))
        fixed = Decimal(str(raw.get("fixed_amount", "0")))
        if base_kind == "DIRECT":
            base = direct
        elif base_kind == "PREVIOUS":
            base = previous
        elif base_kind == "FIXED":
            base = fixed
        else:
            base = subtotal
        amount = fixed if kind == "ADJUSTMENT" or base_kind == "FIXED" else base * rate / Decimal("100")
        amount = q(amount * sign, scale)
        subtotal = q(subtotal + amount, scale)
        previous = amount
        results.append({
            "code": code, "kind": kind, "base_kind": base_kind,
            "base_amount": format(q(base, scale), "f"),
            "rate": format(rate, "f"), "sign": sign,
            "amount": format(amount, "f"), "running_total": format(subtotal, "f"),
            "sort_order": int(raw.get("sort_order", 0)),
        })
    return {
        "direct_cost": format(q(direct, scale), "f"),
        "total": format(subtotal, "f"), "scale": scale,
        "lines": results,
        "calculation_trace": {
            "policy": "P4-COST-005",
            "rounding": "ROUND_HALF_UP",
            "order": [item["code"] for item in results],
        },
    }


def build_cost_structure_calculation_blueprint(resolve_user_id):
    bp = Blueprint("cost_structure_calculation", __name__, url_prefix="/api/cost-structures")

    @bp.post("/calculate")
    def calculate():
        if resolve_user_id() is None:
            return jsonify({"code": "UNAUTHORIZED"}), 401
        body = request.get_json(silent=True) or {}
        try:
            return jsonify(calculate_cost_structure(
                list(body.get("lines") or []), str(body.get("direct_cost", "0")), int(body.get("scale", 2))
            ))
        except (ValueError, ArithmeticError) as exc:
            return jsonify({"code": "INVALID_ARGUMENT", "detail": str(exc)}), 400

    return bp
