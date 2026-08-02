"""Phase 3 project-scoped main-item and analysis-item precision policy."""
from __future__ import annotations

from datetime import datetime, timezone
from decimal import Decimal

from flask import Blueprint, jsonify, request
from sqlalchemy import Column, DateTime, Integer, MetaData, String, Table, and_, select

from api.decimal_math import multiply, quantize

metadata = MetaData()
mrs_precision_policies = Table(
    "mrs_precision_policies", metadata,
    Column("project_code", String(100), primary_key=True),
    Column("main_quantity_scale", Integer, nullable=False),
    Column("main_price_scale", Integer, nullable=False),
    Column("main_amount_scale", Integer, nullable=False),
    Column("analysis_quantity_scale", Integer, nullable=False),
    Column("analysis_price_scale", Integer, nullable=False),
    Column("analysis_amount_scale", Integer, nullable=False),
    Column("row_version", Integer, nullable=False),
    Column("updated_by", String(100), nullable=False),
    Column("updated_at", DateTime(timezone=True), nullable=False),
)

DEFAULT_POLICY = {
    "main_quantity_scale": 2,
    "main_price_scale": 2,
    "main_amount_scale": 0,
    "analysis_quantity_scale": 4,
    "analysis_price_scale": 4,
    "analysis_amount_scale": 2,
}


class MRSPrecisionPolicyService:
    def __init__(self, engine):
        self.engine = engine
        metadata.create_all(engine)

    @staticmethod
    def _validate_scales(values: dict) -> dict:
        result = {}
        for key in DEFAULT_POLICY:
            value = int(values.get(key, DEFAULT_POLICY[key]))
            if value < 0 or value > 8:
                raise ValueError(f"{key} must be between 0 and 8")
            result[key] = value
        if (result["main_quantity_scale"], result["main_price_scale"], result["main_amount_scale"]) == (
            result["analysis_quantity_scale"], result["analysis_price_scale"], result["analysis_amount_scale"]
        ):
            raise ValueError("main and analysis precision policies must remain independently defined")
        return result

    def get(self, project_code: str) -> dict:
        with self.engine.connect() as conn:
            row = conn.execute(select(mrs_precision_policies).where(
                mrs_precision_policies.c.project_code == project_code
            )).mappings().first()
        if not row:
            return {"project_code": project_code, **DEFAULT_POLICY, "row_version": 0, "source": "LEGACY_DEFAULT"}
        result = dict(row)
        result["updated_at"] = row["updated_at"].isoformat()
        result["source"] = "PROJECT_OVERRIDE"
        return result

    def save(self, project_code: str, body: dict, actor: str) -> dict:
        if not project_code.strip():
            raise ValueError("project_code is required")
        scales = self._validate_scales(body)
        expected = int(body.get("row_version", 0))
        now = datetime.now(timezone.utc)
        with self.engine.begin() as conn:
            current = conn.execute(select(mrs_precision_policies).where(
                mrs_precision_policies.c.project_code == project_code
            )).mappings().first()
            if current:
                if int(current["row_version"]) != expected:
                    raise RuntimeError("CONFLICT")
                result = conn.execute(mrs_precision_policies.update().where(and_(
                    mrs_precision_policies.c.project_code == project_code,
                    mrs_precision_policies.c.row_version == expected,
                )).values(**scales, row_version=expected + 1, updated_by=actor, updated_at=now))
                if result.rowcount != 1:
                    raise RuntimeError("CONFLICT")
            else:
                if expected != 0:
                    raise RuntimeError("CONFLICT")
                conn.execute(mrs_precision_policies.insert().values(
                    project_code=project_code, **scales, row_version=1, updated_by=actor, updated_at=now
                ))
        return self.get(project_code)

    def calculate(self, project_code: str, level: str, quantity: str, unit_price: str) -> dict:
        policy = self.get(project_code)
        normalized = level.strip().upper()
        if normalized == "MAIN":
            qs, ps, ats = policy["main_quantity_scale"], policy["main_price_scale"], policy["main_amount_scale"]
        elif normalized == "ANALYSIS":
            qs, ps, ats = policy["analysis_quantity_scale"], policy["analysis_price_scale"], policy["analysis_amount_scale"]
        else:
            raise ValueError("level must be MAIN or ANALYSIS")
        q = quantize(str(quantity), qs)
        p = quantize(str(unit_price), ps)
        amount = multiply(q, p, ats)
        return {
            "project_code": project_code, "level": normalized,
            "quantity": q, "unit_price": p, "amount": amount,
            "quantity_scale": qs, "price_scale": ps, "amount_scale": ats,
            "policy_row_version": policy["row_version"],
            "trace": {"operation": "MRS_SPLIT_PRECISION_MULTIPLY", "input_quantity": str(quantity),
                      "input_unit_price": str(unit_price), "result": amount},
        }


def build_mrs_precision_policy_blueprint(service: MRSPrecisionPolicyService, resolve_user_id):
    bp = Blueprint("mrs_precision_policy", __name__, url_prefix="/api/mrs/projects")

    @bp.get("/<project_code>/precision-policy")
    def get_policy(project_code: str):
        if resolve_user_id() is None: return jsonify({"code": "UNAUTHORIZED"}), 401
        return jsonify(service.get(project_code))

    @bp.put("/<project_code>/precision-policy")
    def save_policy(project_code: str):
        actor = resolve_user_id()
        if actor is None: return jsonify({"code": "UNAUTHORIZED"}), 401
        try: return jsonify(service.save(project_code, request.get_json(silent=True) or {}, str(actor)))
        except RuntimeError: return jsonify({"code": "CONFLICT", "detail": "stale precision policy row_version"}), 409
        except (TypeError, ValueError) as exc: return jsonify({"code": "INVALID_ARGUMENT", "detail": str(exc)}), 400

    @bp.post("/<project_code>/precision-policy/calculate")
    def calculate(project_code: str):
        if resolve_user_id() is None: return jsonify({"code": "UNAUTHORIZED"}), 401
        body = request.get_json(silent=True) or {}
        try: return jsonify(service.calculate(project_code, str(body.get("level", "")),
                                                str(body.get("quantity", "0")), str(body.get("unit_price", "0"))))
        except (TypeError, ValueError, ArithmeticError) as exc:
            return jsonify({"code": "INVALID_ARGUMENT", "detail": str(exc)}), 400

    return bp
