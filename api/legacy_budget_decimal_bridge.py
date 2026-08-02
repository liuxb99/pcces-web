"""Legacy budget endpoint compatibility bridge.

The public URLs and response shape remain compatible with the existing React
client, while all writes are mirrored into ``budget_items_decimal`` in the same
SQLAlchemy transaction.  The bridge is installed by replacing the legacy Flask
view functions at the canonical entrypoint, avoiding invasive edits to the
monolithic ``api.index`` module.
"""
from __future__ import annotations

import json
from decimal import Decimal
from typing import Any

from flask import jsonify, request
from sqlalchemy import and_, delete, select

from api.budget_decimal import budget_items_decimal
from api.budget_kind_engine import calculate_budget_kind
from api.models import BudgetItem, BudgetItemKind, Project, User, UserRole


def _decimal_id(item_id: int) -> str:
    return f"legacy-{item_id}"


def _legacy_id(decimal_id: str | None) -> int | None:
    if not decimal_id:
        return None
    value = str(decimal_id)
    return int(value[7:]) if value.startswith("legacy-") and value[7:].isdigit() else None


def _kind_text(value: Any) -> str:
    return value.value if hasattr(value, "value") else str(value)


def _project_access(session, project_id: int, user_id: int):
    project = session.query(Project).filter(Project.id == project_id).first()
    if project is None:
        return None, (jsonify({"detail": "專案不存在"}), 404)
    user = session.query(User).filter(User.id == user_id).first()
    if not (user and user.role == UserRole.ADMIN.value) and project.owner_id != user_id:
        return None, (jsonify({"detail": "無權限操作此專案"}), 403)
    return project, None


def _input_for_kind(kind: str, data: dict[str, Any], item: BudgetItem | None = None) -> dict[str, Any]:
    explicit = data.get("calculation_input")
    if isinstance(explicit, dict):
        return explicit
    formula = data.get("formula") if "formula" in data else getattr(item, "formula", None)
    if formula:
        try:
            parsed = json.loads(formula)
            if isinstance(parsed, dict):
                return parsed
        except (TypeError, ValueError):
            pass
    if kind == "L":
        return {
            "quantity": str(data.get("quantity", getattr(item, "quantity", 0) or 0)),
            "unit_price": str(data.get("unit_price", getattr(item, "unit_price", 0) or 0)),
        }
    if kind in {"B", "Z"}:
        return {"children": []}
    raise ValueError(f"{kind} requires calculation_input or JSON formula")


def _calculate(kind: str, data: dict[str, Any], item: BudgetItem | None = None) -> str:
    scale = int(data.get("decimal_amount", getattr(item, "decimal_amount", 2) if item else 2))
    if kind in {"B", "Z"}:
        return "0." + ("0" * scale) if scale else "0"
    return calculate_budget_kind(kind, _input_for_kind(kind, data, item), scale).result


def _legacy_dict(item: BudgetItem, decimal_row=None) -> dict[str, Any]:
    result = {column.name: getattr(item, column.name) for column in item.__table__.columns}
    for key, value in list(result.items()):
        if hasattr(value, "isoformat"):
            result[key] = value.isoformat()
        elif hasattr(value, "value"):
            result[key] = value.value
    if decimal_row is not None:
        result["quantity"] = str(decimal_row["quantity"])
        result["unit_price"] = str(decimal_row["unit_price"])
        result["amount"] = str(decimal_row["amount"])
        result["row_version"] = int(decimal_row["row_version"])
        result["decimal_core"] = True
    return result


def install_legacy_budget_bridge(app, engine, session_factory) -> None:
    """Replace legacy budget Flask endpoints with decimal-backed handlers."""

    def create_budget_item(project_id: int, user_id: int):
        data = request.get_json(silent=True) or {}
        session = session_factory()
        try:
            project, error = _project_access(session, project_id, user_id)
            if error:
                return error
            kind = str(data.get("kind", "B")).upper()
            amount = _calculate(kind, data)
            item = BudgetItem(
                project_id=project_id,
                parent_id=data.get("parent_id"), item_no=data.get("item_no"),
                print_no=data.get("print_no"), c_name=data.get("c_name"),
                e_name=data.get("e_name"), c_unit=data.get("c_unit"), e_unit=data.get("e_unit"),
                quantity=Decimal(str(data.get("quantity", 0))),
                unit_price=Decimal(str(data.get("unit_price", 0))), amount=Decimal(amount),
                kind=BudgetItemKind(kind), formula=data.get("formula"), memo=data.get("memo"),
                sort_order=data.get("sort_order"), is_fixed_price=data.get("is_fixed_price", False),
                decimal_qty=int(data.get("decimal_qty", 2)), decimal_price=int(data.get("decimal_price", 2)),
                decimal_amount=int(data.get("decimal_amount", 2)),
            )
            session.add(item)
            session.flush()
            now = item.created_at or item.updated_at
            session.execute(budget_items_decimal.insert().values(
                id=_decimal_id(item.id), project_code=project.code,
                parent_id=_decimal_id(item.parent_id) if item.parent_id else None,
                item_no=item.item_no, name=item.c_name or item.item_no or str(item.id), kind=kind,
                quantity=Decimal(str(item.quantity or 0)), unit_price=Decimal(str(item.unit_price or 0)),
                amount=Decimal(amount), quantity_scale=item.decimal_qty, price_scale=item.decimal_price,
                amount_scale=item.decimal_amount, created_at=now, updated_at=now, row_version=1,
            ))
            session.commit()
            row = session.execute(select(budget_items_decimal).where(budget_items_decimal.c.id == _decimal_id(item.id))).mappings().first()
            return jsonify(_legacy_dict(item, row)), 201
        except (ValueError, ArithmeticError) as exc:
            session.rollback()
            return jsonify({"code": "INVALID_ARGUMENT", "detail": str(exc)}), 400
        except Exception:
            session.rollback()
            raise
        finally:
            session.close()

    def update_budget_item(project_id: int, item_id: int, user_id: int):
        data = request.get_json(silent=True) or {}
        session = session_factory()
        try:
            _, error = _project_access(session, project_id, user_id)
            if error:
                return error
            item = session.query(BudgetItem).filter(BudgetItem.id == item_id, BudgetItem.project_id == project_id).first()
            if item is None:
                return jsonify({"detail": "預算項目不存在"}), 404
            row = session.execute(select(budget_items_decimal).where(budget_items_decimal.c.id == _decimal_id(item_id))).mappings().first()
            if row is None:
                return jsonify({"code": "DECIMAL_SHADOW_MISSING", "detail": "請先執行 Legacy Decimal Adapter"}), 409
            requested_version = int(data.get("row_version", row["row_version"]))
            if requested_version != int(row["row_version"]):
                return jsonify({"code": "CONFLICT", "current_row_version": int(row["row_version"])}), 409
            for key in ("parent_id", "item_no", "print_no", "c_name", "e_name", "c_unit", "e_unit", "formula", "memo", "sort_order", "is_fixed_price", "is_locked", "decimal_qty", "decimal_price", "decimal_amount", "is_green_item"):
                if key in data:
                    setattr(item, key, data[key])
            if "quantity" in data: item.quantity = Decimal(str(data["quantity"]))
            if "unit_price" in data: item.unit_price = Decimal(str(data["unit_price"]))
            if "kind" in data: item.kind = BudgetItemKind(str(data["kind"]).upper())
            kind = _kind_text(item.kind)
            amount = _calculate(kind, data, item)
            item.amount = Decimal(amount)
            result = session.execute(budget_items_decimal.update().where(and_(
                budget_items_decimal.c.id == _decimal_id(item_id),
                budget_items_decimal.c.row_version == requested_version,
            )).values(
                parent_id=_decimal_id(item.parent_id) if item.parent_id else None,
                item_no=item.item_no, name=item.c_name or item.item_no or str(item.id), kind=kind,
                quantity=Decimal(str(item.quantity or 0)), unit_price=Decimal(str(item.unit_price or 0)),
                amount=Decimal(amount), quantity_scale=item.decimal_qty, price_scale=item.decimal_price,
                amount_scale=item.decimal_amount, updated_at=item.updated_at,
                row_version=requested_version + 1,
            ))
            if result.rowcount != 1:
                session.rollback()
                return jsonify({"code": "CONFLICT", "detail": "decimal budget update conflict"}), 409
            session.commit()
            row = session.execute(select(budget_items_decimal).where(budget_items_decimal.c.id == _decimal_id(item_id))).mappings().first()
            return jsonify(_legacy_dict(item, row))
        except (ValueError, ArithmeticError) as exc:
            session.rollback()
            return jsonify({"code": "INVALID_ARGUMENT", "detail": str(exc)}), 400
        except Exception:
            session.rollback()
            raise
        finally:
            session.close()

    def get_budget_list(project_id: int, user_id: int):
        session = session_factory()
        try:
            project, error = _project_access(session, project_id, user_id)
            if error: return error
            items = session.query(BudgetItem).filter(BudgetItem.project_id == project_id).order_by(BudgetItem.id).all()
            rows = session.execute(select(budget_items_decimal).where(budget_items_decimal.c.project_code == project.code)).mappings().all()
            shadow = {_legacy_id(row["id"]): row for row in rows}
            return jsonify([_legacy_dict(item, shadow.get(item.id)) for item in items])
        finally:
            session.close()

    def get_budget_tree(project_id: int, user_id: int):
        response = get_budget_list(project_id, user_id)
        if not isinstance(response, tuple) and getattr(response, "status_code", 200) >= 400:
            return response
        payload = response.get_json() if hasattr(response, "get_json") else response[0].get_json()
        by_parent: dict[int | None, list[dict[str, Any]]] = {}
        for item in payload:
            item["children"] = []
            by_parent.setdefault(item.get("parent_id"), []).append(item)
        for parent_id, children in by_parent.items():
            if parent_id is not None:
                parent = next((candidate for candidate in payload if candidate["id"] == parent_id), None)
                if parent is not None: parent["children"] = children
        return jsonify(by_parent.get(None, []))

    def recalc_budget(project_id: int, user_id: int):
        session = session_factory()
        try:
            project, error = _project_access(session, project_id, user_id)
            if error: return error
            items = session.query(BudgetItem).filter(BudgetItem.project_id == project_id).all()
            by_id = {item.id: item for item in items}
            children: dict[int | None, list[BudgetItem]] = {}
            for item in items: children.setdefault(item.parent_id, []).append(item)
            traces = []
            def visit(item: BudgetItem) -> str:
                kind = _kind_text(item.kind)
                if kind in {"B", "Z"}:
                    child_values = [visit(child) for child in children.get(item.id, [])]
                    trace = calculate_budget_kind(kind, {"children": child_values}, item.decimal_amount)
                else:
                    payload = _input_for_kind(kind, {}, item)
                    trace = calculate_budget_kind(kind, payload, item.decimal_amount)
                item.amount = Decimal(trace.result)
                row_id = _decimal_id(item.id)
                current = session.execute(select(budget_items_decimal).where(budget_items_decimal.c.id == row_id)).mappings().first()
                if current is None:
                    raise ValueError(f"decimal shadow missing for item {item.id}")
                session.execute(budget_items_decimal.update().where(budget_items_decimal.c.id == row_id).values(
                    amount=Decimal(trace.result), quantity=Decimal(str(item.quantity or 0)),
                    unit_price=Decimal(str(item.unit_price or 0)), kind=kind,
                    updated_at=item.updated_at, row_version=int(current["row_version"]) + 1,
                ))
                traces.append({"item_id": item.id, **trace.to_dict()})
                return trace.result
            roots = children.get(None, [])
            totals = [visit(root) for root in roots]
            session.commit()
            total = calculate_budget_kind("Z", {"children": totals}, 2).result
            return jsonify({"message": "預算重新計算完成", "total_amount": total, "traces": traces})
        except (ValueError, ArithmeticError) as exc:
            session.rollback()
            return jsonify({"code": "INVALID_ARGUMENT", "detail": str(exc)}), 400
        except Exception:
            session.rollback()
            raise
        finally:
            session.close()

    def delete_budget_item(project_id: int, item_id: int, user_id: int):
        session = session_factory()
        try:
            _, error = _project_access(session, project_id, user_id)
            if error: return error
            item = session.query(BudgetItem).filter(BudgetItem.id == item_id, BudgetItem.project_id == project_id).first()
            if item is None: return jsonify({"detail": "預算項目不存在"}), 404
            descendants = []
            def collect(parent: int):
                for child in session.query(BudgetItem).filter(BudgetItem.parent_id == parent).all():
                    collect(child.id); descendants.append(child)
            collect(item_id)
            ids = [_decimal_id(child.id) for child in descendants] + [_decimal_id(item_id)]
            session.execute(delete(budget_items_decimal).where(budget_items_decimal.c.id.in_(ids)))
            for child in descendants: session.delete(child)
            session.delete(item)
            session.commit()
            return jsonify({"message": "預算項目已刪除"})
        except Exception:
            session.rollback(); raise
        finally:
            session.close()

    def move_budget_item(project_id: int, item_id: int, user_id: int):
        data = request.get_json(silent=True) or {}
        value = request.args.get("new_parent_id", data.get("new_parent_id"))
        new_parent_id = None if value in (None, "null", "") else int(value)
        session = session_factory()
        try:
            _, error = _project_access(session, project_id, user_id)
            if error: return error
            item = session.query(BudgetItem).filter(BudgetItem.id == item_id, BudgetItem.project_id == project_id).first()
            if item is None: return jsonify({"detail": "預算項目不存在"}), 404
            if new_parent_id == item_id: return jsonify({"code": "INVALID_ARGUMENT", "detail": "item cannot parent itself"}), 400
            item.parent_id = new_parent_id
            session.execute(budget_items_decimal.update().where(
                budget_items_decimal.c.id == _decimal_id(item_id)
            ).values(parent_id=_decimal_id(new_parent_id) if new_parent_id else None,
                     row_version=budget_items_decimal.c.row_version + 1))
            session.commit()
            return jsonify({"message": "預算項目已移動"})
        except Exception:
            session.rollback(); raise
        finally:
            session.close()

    replacements = {
        "create_budget_item": create_budget_item,
        "update_budget_item": update_budget_item,
        "get_budget_list": get_budget_list,
        "get_budget_tree": get_budget_tree,
        "recalc_budget": recalc_budget,
        "delete_budget_item": delete_budget_item,
        "move_budget_item": move_budget_item,
    }
    missing = [name for name in replacements if name not in app.view_functions]
    if missing:
        raise RuntimeError(f"legacy budget endpoints missing: {', '.join(missing)}")
    app.view_functions.update(replacements)
