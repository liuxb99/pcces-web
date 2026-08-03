"""Legacy-compatible PCCES code validation and fitting for MRS resources."""
from __future__ import annotations

import re
from flask import Blueprint, jsonify, request

_CODE_PATTERN = re.compile(r"^[0-9A-Z]+$")
_UNIT_ALIASES = {
    "M": "M", "公尺": "M", "米": "M",
    "M2": "M2", "平方公尺": "M2", "平方米": "M2",
    "M3": "M3", "立方公尺": "M3", "立方米": "M3",
    "T": "T", "公噸": "T", "噸": "T",
    "KG": "KG", "公斤": "KG", "千克": "KG", "兛": "KG",
}


def canonical_unit(unit: str | None) -> str:
    value = str(unit or "").strip().replace("²", "2").replace("³", "3")
    return _UNIT_ALIASES.get(value.upper(), _UNIT_ALIASES.get(value, value))


def validate_code(code: str | None, unit: str | None = None) -> dict:
    original = str(code or "")
    normalized = original.strip().replace(" ", "").upper()
    errors: list[str] = []
    warnings: list[str] = []
    resource_type = ""
    chapter_code = ""
    if not normalized:
        errors.append("工項編碼不可空白")
    else:
        if not _CODE_PATTERN.fullmatch(normalized):
            errors.append("編碼僅允許英文字母與數字")
        first = normalized[0]
        if first.isdigit():
            resource_type = "WORK_ITEM"
            if len(normalized) < 10: errors.append("工項編碼長度不足")
            if len(normalized) >= 5: chapter_code = normalized[:5]
        elif first == "M":
            resource_type = "MATERIAL"
            if len(normalized) < 11: errors.append("材料編碼長度不足")
            if len(normalized) >= 6: chapter_code = normalized[1:6]
        elif first == "L":
            resource_type = "LABOR"
            if len(normalized) < 13: errors.append("人工編碼長度不足")
        elif first == "E":
            resource_type = "EQUIPMENT"
            if len(normalized) < 13: errors.append("機具編碼長度不足")
        elif first == "W":
            resource_type = "OTHER"
            if len(normalized) < 11: errors.append("雜項編碼長度不足")
        else:
            errors.append("非正常編碼(開頭不是L,E,M,W或數字)")
    canonical = canonical_unit(unit)
    if not canonical: warnings.append("單位未提供")
    return {
        "input_code": original, "normalized_code": normalized,
        "valid": not errors, "resource_type": resource_type,
        "chapter_code": chapter_code, "canonical_unit": canonical,
        "errors": errors, "warnings": warnings,
    }


def fit_code(code: str | None, unit: str | None = None, name: str | None = None) -> dict:
    original_code, original_unit = str(code or ""), str(unit or "")
    fitted = original_code.strip().replace(" ", "").upper()
    canonical = canonical_unit(original_unit)
    warnings: list[str] = []
    if original_code != fitted: warnings.append("編碼已正規化為大寫並移除空白")
    if original_unit.strip() != canonical: warnings.append("單位已轉換為Legacy標準單位")
    return {
        "original_code": original_code, "fitted_code": fitted,
        "original_unit": original_unit, "canonical_unit": canonical,
        "changed": original_code != fitted or original_unit.strip() != canonical,
        "warnings": warnings,
    }


def build_mrs_code_blueprint(resolve_user_id):
    bp = Blueprint("mrs_code", __name__, url_prefix="/api/mrs/code")

    def require_actor():
        if resolve_user_id() is None:
            raise PermissionError("authentication required")

    @bp.post("/validate")
    def validate():
        require_actor()
        body = request.get_json(silent=True) or {}
        return jsonify(validate_code(body.get("code"), body.get("unit")))

    @bp.post("/fit")
    def fit():
        require_actor()
        body = request.get_json(silent=True) or {}
        return jsonify(fit_code(body.get("code"), body.get("unit"), body.get("name")))

    return bp


class MRSCodeService:
    """Thin service wrapper for stateless PCCES code operations."""
    def __init__(self, engine=None):
        pass
