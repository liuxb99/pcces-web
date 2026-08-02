"""Exact decimal arithmetic shared by PCCES Web calculations.

All external values are accepted and returned as canonical decimal strings.
Binary floating point is intentionally rejected from the public API.
"""

from __future__ import annotations

from decimal import Decimal, InvalidOperation, ROUND_HALF_UP, localcontext
from typing import Iterable

MAX_PRECISION = 28
MAX_SCALE = 8


class DecimalValueError(ValueError):
    pass


def parse_decimal(value: str | int | Decimal) -> Decimal:
    if isinstance(value, float):
        raise DecimalValueError("binary float is not accepted; use a decimal string")
    try:
        parsed = value if isinstance(value, Decimal) else Decimal(str(value).strip())
    except (InvalidOperation, ValueError, AttributeError) as exc:
        raise DecimalValueError("invalid decimal value") from exc
    if not parsed.is_finite():
        raise DecimalValueError("decimal value must be finite")
    return parsed


def quantize(value: str | int | Decimal, scale: int = 2) -> str:
    if scale < 0 or scale > MAX_SCALE:
        raise DecimalValueError(f"scale must be between 0 and {MAX_SCALE}")
    quantum = Decimal(1).scaleb(-scale)
    with localcontext() as context:
        context.prec = MAX_PRECISION + MAX_SCALE + 8
        result = parse_decimal(value).quantize(quantum, rounding=ROUND_HALF_UP)
    if result == 0:
        result = abs(result)
    return format(result, f".{scale}f")


def multiply(left: str | int | Decimal, right: str | int | Decimal, scale: int = 2) -> str:
    with localcontext() as context:
        context.prec = MAX_PRECISION + MAX_SCALE + 8
        product = parse_decimal(left) * parse_decimal(right)
    return quantize(product, scale)


def sum_values(values: Iterable[str | int | Decimal], scale: int = 2) -> str:
    with localcontext() as context:
        context.prec = MAX_PRECISION + MAX_SCALE + 8
        total = sum((parse_decimal(value) for value in values), Decimal(0))
    return quantize(total, scale)
