"""Deterministic B/L/F/S/U/Z budget calculation engine with trace output."""

from __future__ import annotations

from dataclasses import dataclass, asdict
from decimal import Decimal
from typing import Any

from api.decimal_math import multiply, quantize, sum_values


@dataclass(frozen=True)
class TraceStep:
    operation: str
    inputs: dict[str, Any]
    result: str


@dataclass(frozen=True)
class CalculationTrace:
    kind: str
    scale: int
    steps: list[TraceStep]
    result: str

    def to_dict(self) -> dict[str, Any]:
        return {
            "kind": self.kind,
            "scale": self.scale,
            "steps": [asdict(step) for step in self.steps],
            "result": self.result,
        }


def _decimal(value: Any) -> Decimal:
    return Decimal(str(value))


def calculate_budget_kind(kind: str, payload: dict[str, Any], scale: int) -> CalculationTrace:
    normalized = kind.upper().strip()
    if scale < 0 or scale > 8:
        raise ValueError("scale must be between 0 and 8")
    steps: list[TraceStep] = []

    if normalized == "L":
        quantity = str(payload.get("quantity", "0"))
        unit_price = str(payload.get("unit_price", "0"))
        result = multiply(quantity, unit_price, scale)
        steps.append(TraceStep("MULTIPLY", {"quantity": quantity, "unit_price": unit_price}, result))
    elif normalized in {"B", "Z"}:
        children = [str(value) for value in payload.get("children", [])]
        result = sum_values(children, scale)
        steps.append(TraceStep("SUM_CHILDREN", {"children": children}, result))
    elif normalized == "F":
        base = str(payload.get("base", "0"))
        rate = str(payload.get("rate", "0"))
        result = multiply(base, rate, scale)
        steps.append(TraceStep("MULTIPLY_BASE_RATE", {"base": base, "rate": rate}, result))
    elif normalized == "S":
        remaining = _decimal(payload.get("base", "0"))
        if remaining < 0:
            raise ValueError("tiered base cannot be negative")
        previous_limit = Decimal("0")
        subtotal = Decimal("0")
        tiers = payload.get("tiers", [])
        if not isinstance(tiers, list) or not tiers:
            raise ValueError("tiers are required for S items")
        for tier in tiers:
            rate = _decimal(tier.get("rate", "0"))
            up_to_raw = tier.get("up_to")
            if up_to_raw is None:
                quantity = remaining
            else:
                up_to = _decimal(up_to_raw)
                if up_to < previous_limit:
                    raise ValueError("tiers must be ordered by up_to")
                capacity = up_to - previous_limit
                quantity = min(remaining, capacity)
                previous_limit = up_to
            tier_amount = quantity * rate
            subtotal += tier_amount
            step_result = quantize(str(tier_amount), scale)
            steps.append(TraceStep("TIER", {"quantity": str(quantity), "rate": str(rate), "up_to": up_to_raw}, step_result))
            remaining -= quantity
            if remaining <= 0:
                break
        if remaining > 0:
            raise ValueError("tier schedule does not cover base")
        result = quantize(str(subtotal), scale)
    elif normalized == "U":
        signed_values: list[str] = []
        terms = payload.get("terms", [])
        for term in terms:
            sign = int(term.get("sign", 1))
            if sign not in {-1, 1}:
                raise ValueError("term sign must be -1 or 1")
            signed_values.append(str(_decimal(term.get("amount", "0")) * sign))
        result = sum_values(signed_values, scale)
        steps.append(TraceStep("SIGNED_SUM", {"terms": terms}, result))
    else:
        raise ValueError(f"unsupported budget item kind: {kind}")

    return CalculationTrace(normalized, scale, steps, result)
