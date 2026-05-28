"""PCCES 網頁版 — Flask 主程式"""

import os
import json
import tempfile
from datetime import datetime, timedelta, timezone
from functools import wraps
import secrets
from hashlib import pbkdf2_hmac

import jwt
from flask import Flask, request, jsonify, send_file, send_from_directory
from flask_cors import CORS
from sqlalchemy import create_engine, func, select, delete
from sqlalchemy.orm import Session, sessionmaker

from api.models import Base, User, Project, BudgetItem, Resource, ResourceBreakdownItem, BudgetItemKind, UserRole

# ─── 設定（支援環境變數覆蓋） ───
import os
SECRET_KEY = os.environ.get("PCCES_SECRET_KEY", "pcces-web-secret-key-change-in-production")
_db_url_env = os.environ.get("PCCES_DATABASE_URL", "")
DATABASE_URL = _db_url_env if _db_url_env else f"sqlite:///{tempfile.gettempdir()}/pcces.db"
ALGORITHM = os.environ.get("PCCES_JWT_ALGORITHM", "HS256")
ACCESS_TOKEN_EXPIRE_MINUTES = int(os.environ.get("PCCES_TOKEN_EXPIRE_MINUTES", "480"))
REPORT_DIR = os.environ.get("PCCES_REPORT_DIR", "reports")

app = Flask(__name__)
CORS(app, resources={r"/api/*": {"origins": "*"}})

# 資料庫
engine = create_engine(DATABASE_URL, echo=False)
SessionLocal = sessionmaker(bind=engine)


def init_db():
    Base.metadata.create_all(engine)
    _migrate_schema()


def _migrate_schema():
    """SQLite 遷移輔助：為既有表格補上新增的欄位（若不存在）"""
    from sqlalchemy import inspect
    inspector = inspect(engine)

    # ── resources 表新增欄位 ──
    cols = {c["name"] for c in inspector.get_columns("resources")}
    additions = {
        "is_analysis": "INTEGER DEFAULT 0",
        "labor_rate": "FLOAT DEFAULT 0",
        "material_rate": "FLOAT DEFAULT 0",
        "equipment_rate": "FLOAT DEFAULT 0",
        "misc_rate": "FLOAT DEFAULT 0",
    }
    for col, type_def in additions.items():
        if col not in cols:
            with engine.connect() as conn:
                conn.execute(f"ALTER TABLE resources ADD COLUMN {col} {type_def}")
                conn.commit()

    # ── resource_breakdown_items 表（若不存在則 create_all 已處理） ──


def get_db():
    db = SessionLocal()
    try:
        yield db
    finally:
        db.close()


# ─── JWT 輔助函數 ───
def get_password_hash(password: str) -> str:
    """使用 PBKDF2-SHA256 加鹽雜湊密碼"""
    salt = secrets.token_hex(16)
    key = pbkdf2_hmac("sha256", password.encode(), salt.encode(), 100000).hex()
    return f"{salt}${key}"


def verify_password(plain: str, stored: str) -> bool:
    """驗證密碼"""
    try:
        salt, key = stored.split("$")
        check = pbkdf2_hmac("sha256", plain.encode(), salt.encode(), 100000).hex()
        return check == key
    except (ValueError, AttributeError):
        return False


def create_token(user_id: int, username: str) -> str:
    payload = {
        "sub": str(user_id),
        "username": username,
        "exp": datetime.now(timezone.utc) + timedelta(minutes=ACCESS_TOKEN_EXPIRE_MINUTES),
    }
    return jwt.encode(payload, SECRET_KEY, algorithm=ALGORITHM)


def decode_token(token: str) -> dict:
    try:
        return jwt.decode(token, SECRET_KEY, algorithms=[ALGORITHM])
    except jwt.PyJWTError:
        return None


def require_auth(f):
    """免登入模式：有 token 就解析使用者，無 token 就用預設 guest"""
    @wraps(f)
    def decorated(*args, **kwargs):
        auth = request.headers.get("Authorization", "")
        if auth.startswith("Bearer "):
            payload = decode_token(auth[7:])
            if payload:
                kwargs["user_id"] = int(payload["sub"])
                return f(*args, **kwargs)
        # 無 token 或 token 無效 → 以 guest 身份操作
        kwargs["user_id"] = 1
        return f(*args, **kwargs)
    return decorated


# ─── 工具函數 ───
def round_value(value: float, decimals: int = 2) -> float:
    factor = 10 ** decimals
    if value < 0:
        return float(int(value * factor - 0.5)) / factor
    return float(int(value * factor + 0.5)) / factor


def model_to_dict(obj, exclude=None):
    """將 SQLAlchemy 模型轉為 dict"""
    if exclude is None:
        exclude = set()
    result = {}
    for col in obj.__table__.columns:
        if col.name not in exclude:
            val = getattr(obj, col.name)
            if isinstance(val, datetime):
                val = val.isoformat()
            result[col.name] = val
    return result


# ═══════════════════════════════════════════════
# 認證 API
# ═══════════════════════════════════════════════

@app.route("/api/health")
def health_check():
    return jsonify({"status": "ok", "version": "1.0.0"})


@app.route("/api/auth/register", methods=["POST"])
def register():
    data = request.get_json()
    if not data:
        return jsonify({"detail": "請提供資料"}), 400

    db = next(get_db())
    try:
        existing = db.query(User).filter(User.username == data["username"]).first()
        if existing:
            return jsonify({"detail": "帳號已存在"}), 400

        user = User(
            username=data["username"],
            password_hash=get_password_hash(data["password"]),
            display_name=data.get("display_name", data["username"]),
            email=data.get("email"),
            company=data.get("company"),
            department=data.get("department"),
        )
        db.add(user)
        db.commit()
        db.refresh(user)

        token = create_token(user.id, user.username)
        return jsonify({
            "access_token": token,
            "token_type": "bearer",
            "user": model_to_dict(user, exclude={"password_hash"}),
        }), 201
    finally:
        db.close()


@app.route("/api/auth/login", methods=["POST"])
def login():
    data = request.get_json()
    if not data:
        return jsonify({"detail": "請提供帳號密碼"}), 400

    db = next(get_db())
    try:
        user = db.query(User).filter(User.username == data["username"]).first()
        if not user or not verify_password(data["password"], user.password_hash):
            return jsonify({"detail": "帳號或密碼錯誤"}), 401
        if not user.is_active:
            return jsonify({"detail": "帳號已停用"}), 403

        token = create_token(user.id, user.username)
        return jsonify({
            "access_token": token,
            "token_type": "bearer",
            "user": model_to_dict(user, exclude={"password_hash"}),
        })
    finally:
        db.close()


# ═══════════════════════════════════════════════
# 專案 API
# ═══════════════════════════════════════════════

@app.route("/api/projects/stats")
@require_auth
def dashboard_stats(user_id):
    db = next(get_db())
    try:
        # 資料隔離：一般使用者只看自己的統計
        user = db.query(User).filter(User.id == user_id).first()
        is_admin = user and user.role == UserRole.ADMIN.value

        project_query = db.query(Project)
        if not is_admin:
            project_query = project_query.filter(Project.owner_id == user_id)

        total_projects = project_query.count() or 0
        active_projects = project_query.filter(Project.status == "active").count() or 0

        user_project_ids = [p.id for p in project_query.all()]
        if user_project_ids:
            total_items = db.query(func.count(BudgetItem.id)).filter(
                BudgetItem.project_id.in_(user_project_ids)
            ).scalar() or 0
            total_amount = db.query(func.coalesce(func.sum(BudgetItem.amount), 0)).filter(
                BudgetItem.project_id.in_(user_project_ids)
            ).scalar() or 0.0
            total_resources = db.query(func.count(Resource.id)).filter(
                Resource.project_id.in_(user_project_ids)
            ).scalar() or 0
        else:
            total_items = total_amount = total_resources = 0

        recent_query = db.query(Project).order_by(Project.updated_at.desc())
        if not is_admin:
            recent_query = recent_query.filter(Project.owner_id == user_id)
        recent = recent_query.limit(5).all()

        return jsonify({
            "total_projects": total_projects,
            "active_projects": active_projects,
            "total_budget_items": total_items,
            "total_budget_amount": float(total_amount),
            "total_resources": total_resources,
            "recent_projects": [model_to_dict(p) for p in recent],
        })
    finally:
        db.close()


@app.route("/api/projects/", methods=["GET"])
@require_auth
def list_projects(user_id):
    db = next(get_db())
    try:
        # 資料隔離：一般使用者只看自己專案，管理員可看全部
        user = db.query(User).filter(User.id == user_id).first()
        is_admin = user and user.role == UserRole.ADMIN.value
        query = db.query(Project).order_by(Project.updated_at.desc())
        if not is_admin:
            query = query.filter(Project.owner_id == user_id)
        projects = query.all()
        result = []
        for p in projects:
            d = model_to_dict(p)
            # 計算預算總額與項目數
            items = db.query(BudgetItem).filter(
                BudgetItem.project_id == p.id,
                BudgetItem.parent_id.is_(None)
            ).all()
            d["budget_total"] = sum((i.amount or 0) for i in items)
            d["item_count"] = db.query(func.count(BudgetItem.id)).filter(
                BudgetItem.project_id == p.id
            ).scalar() or 0
            result.append(d)
        return jsonify(result)
    finally:
        db.close()


@app.route("/api/projects/", methods=["POST"])
@require_auth
def create_project(user_id):
    data = request.get_json()
    db = next(get_db())
    try:
        project = Project(
            code=data["code"],
            name=data["name"],
            name_en=data.get("name_en"),
            location=data.get("location"),
            account_code=data.get("account_code"),
            description=data.get("description"),
            scope=data.get("scope", 1.0),
            scope_unit=data.get("scope_unit", "式"),
            owner_id=user_id,
        )
        db.add(project)
        db.commit()
        db.refresh(project)
        return jsonify(model_to_dict(project)), 201
    finally:
        db.close()


@app.route("/api/projects/<int:project_id>", methods=["GET"])
@require_auth
def get_project(project_id, user_id):
    db = next(get_db())
    try:
        proj, err = _check_project_access(db, project_id, user_id)
        if err:
            return err
        d = model_to_dict(proj)
        items = db.query(BudgetItem).filter(
            BudgetItem.project_id == project_id,
            BudgetItem.parent_id.is_(None)
        ).all()
        d["budget_total"] = sum((i.amount or 0) for i in items)
        d["item_count"] = db.query(func.count(BudgetItem.id)).filter(
            BudgetItem.project_id == project_id
        ).scalar() or 0
        return jsonify(d)
    finally:
        db.close()


@app.route("/api/projects/<int:project_id>", methods=["PUT"])
@require_auth
def update_project(project_id, user_id):
    data = request.get_json()
    db = next(get_db())
    try:
        project = db.query(Project).filter(Project.id == project_id).first()
        if not project:
            return jsonify({"detail": "專案不存在"}), 404
        # 所有權檢查
        user = db.query(User).filter(User.id == user_id).first()
        is_admin = user and user.role == UserRole.ADMIN.value
        if not is_admin and project.owner_id != user_id:
            return jsonify({"detail": "無權限修改此專案"}), 403
        for key in ("name", "name_en", "location", "account_code", "description", "scope", "scope_unit", "status"):
            if key in data and data[key] is not None:
                setattr(project, key, data[key])
        db.commit()
        db.refresh(project)
        return jsonify(model_to_dict(project))
    finally:
        db.close()


@app.route("/api/projects/<int:project_id>", methods=["DELETE"])
@require_auth
def delete_project(project_id, user_id):
    db = next(get_db())
    try:
        project = db.query(Project).filter(Project.id == project_id).first()
        if not project:
            return jsonify({"detail": "專案不存在"}), 404
        # 所有權檢查
        user = db.query(User).filter(User.id == user_id).first()
        is_admin = user and user.role == UserRole.ADMIN.value
        if not is_admin and project.owner_id != user_id:
            return jsonify({"detail": "無權限刪除專案"}), 403
        db.delete(project)
        db.commit()
        return jsonify({"message": "專案已刪除"})
    finally:
        db.close()


# ═══════════════════════════════════════════════
# 預算項目 API（核心）
# ═══════════════════════════════════════════════

def _get_children_count(db: Session, parent_id: int) -> int:
    return db.query(func.count(BudgetItem.id)).filter(
        BudgetItem.parent_id == parent_id
    ).scalar() or 0


def _calc_amount(item: BudgetItem) -> float:
    return round_value((item.quantity or 0) * (item.unit_price or 0), item.decimal_amount)


def _recalc_children(db: Session, parent_id: int) -> float:
    """遞迴計算子項金額"""
    children = db.query(BudgetItem).filter(BudgetItem.parent_id == parent_id).all()
    total = 0.0
    for child in children:
        if child.kind in (BudgetItemKind.B, BudgetItemKind.Z):
            child.amount = _recalc_children(db, child.id)
        else:
            child.amount = _calc_amount(child)
        db.flush()
        total += child.amount or 0
    return round_value(total, 2)


def _check_project_access(db: Session, project_id: int, user_id: int):
    """檢查使用者是否有權限存取專案（回傳 (project, error_response)）"""
    project = db.query(Project).filter(Project.id == project_id).first()
    if not project:
        return None, (jsonify({"detail": "專案不存在"}), 404)
    user = db.query(User).filter(User.id == user_id).first()
    is_admin = user and user.role == UserRole.ADMIN.value
    if not is_admin and project.owner_id != user_id:
        return None, (jsonify({"detail": "無權限操作此專案"}), 403)
    return project, None


def _build_tree_dict(db: Session, project_id: int, parent_id=None):
    """遞迴建立預算樹狀結構（含 children 巢狀資料）"""
    query = db.query(BudgetItem).filter(BudgetItem.project_id == project_id)
    if parent_id is None:
        query = query.filter(BudgetItem.parent_id.is_(None))
    else:
        query = query.filter(BudgetItem.parent_id == parent_id)
    query = query.order_by(BudgetItem.sort_order, BudgetItem.id)
    items = query.all()
    result = []
    for item in items:
        d = model_to_dict(item)
        d["children"] = _build_tree_dict(db, project_id, item.id)
        result.append(d)
    return result


@app.route("/api/projects/<int:project_id>/budget/tree", methods=["GET"])
@require_auth
def get_budget_tree(project_id, user_id):
    db = next(get_db())
    try:
        proj, err = _check_project_access(db, project_id, user_id)
        if err:
            return err
        tree = _build_tree_dict(db, project_id)
        return jsonify(tree)
    finally:
        db.close()


@app.route("/api/projects/<int:project_id>/budget/", methods=["GET"])
@require_auth
def get_budget_list(project_id, user_id):
    db = next(get_db())
    try:
        proj, err = _check_project_access(db, project_id, user_id)
        if err:
            return err
        items = db.query(BudgetItem).filter(
            BudgetItem.project_id == project_id
        ).order_by(BudgetItem.id).all()
        return jsonify([model_to_dict(i) for i in items])
    finally:
        db.close()


@app.route("/api/projects/<int:project_id>/budget/", methods=["POST"])
@require_auth
def create_budget_item(project_id, user_id):
    data = request.get_json()
    db = next(get_db())
    try:
        proj, err = _check_project_access(db, project_id, user_id)
        if err:
            return err
        item = BudgetItem(
            project_id=project_id,
            parent_id=data.get("parent_id"),
            item_no=data.get("item_no"),
            print_no=data.get("print_no"),
            c_name=data.get("c_name"),
            e_name=data.get("e_name"),
            c_unit=data.get("c_unit"),
            e_unit=data.get("e_unit"),
            quantity=data.get("quantity", 0),
            unit_price=data.get("unit_price", 0),
            kind=BudgetItemKind(data.get("kind", "B")),
            formula=data.get("formula"),
            memo=data.get("memo"),
            sort_order=data.get("sort_order"),
            is_fixed_price=data.get("is_fixed_price", False),
            decimal_qty=data.get("decimal_qty", 2),
            decimal_price=data.get("decimal_price", 2),
            decimal_amount=data.get("decimal_amount", 2),
        )
        # B/Z 類型金額不計算（需透過 recalc 加總子項）
        kind_val = item.kind.value if hasattr(item.kind, 'value') else str(item.kind)
        if kind_val not in ('B', 'Z'):
            item.amount = _calc_amount(item)
        db.add(item)
        db.commit()
        db.refresh(item)
        return jsonify(model_to_dict(item)), 201
    finally:
        db.close()


@app.route("/api/projects/<int:project_id>/budget/<int:item_id>", methods=["PUT"])
@require_auth
def update_budget_item(project_id, item_id, user_id):
    data = request.get_json()
    db = next(get_db())
    try:
        proj, err = _check_project_access(db, project_id, user_id)
        if err:
            return err
        item = db.query(BudgetItem).filter(
            BudgetItem.id == item_id,
            BudgetItem.project_id == project_id
        ).first()
        if not item:
            return jsonify({"detail": "預算項目不存在"}), 404

        for key in ("parent_id", "item_no", "print_no", "c_name", "e_name",
                     "c_unit", "e_unit", "formula", "memo", "sort_order",
                     "is_fixed_price", "is_locked", "decimal_qty", "decimal_price",
                     "decimal_amount", "is_green_item"):
            if key in data and data[key] is not None:
                setattr(item, key, data[key])

        if "quantity" in data:
            item.quantity = data["quantity"]
        if "unit_price" in data:
            item.unit_price = data["unit_price"]
        if "kind" in data:
            item.kind = BudgetItemKind(data["kind"])

        # B/Z 類型金額不計算（需透過 recalc 加總子項）
        kind_val = item.kind.value if hasattr(item.kind, 'value') else str(item.kind)
        if kind_val not in ('B', 'Z'):
            item.amount = _calc_amount(item)
        db.commit()
        db.refresh(item)
        return jsonify(model_to_dict(item))
    finally:
        db.close()


@app.route("/api/projects/<int:project_id>/budget/<int:item_id>", methods=["DELETE"])
@require_auth
def delete_budget_item(project_id, item_id, user_id):
    db = next(get_db())
    try:
        proj, err = _check_project_access(db, project_id, user_id)
        if err:
            return err
        item = db.query(BudgetItem).filter(
            BudgetItem.id == item_id,
            BudgetItem.project_id == project_id
        ).first()
        if not item:
            return jsonify({"detail": "預算項目不存在"}), 404
        # 先遞迴刪除所有子孫項目，再刪除本身
        _delete_item_children(db, item_id)
        db.delete(item)
        db.commit()
        return jsonify({"message": "預算項目已刪除"})
    finally:
        db.close()


def _delete_item_children(db: Session, parent_id: int):
    """遞迴刪除子項目"""
    children = db.query(BudgetItem).filter(BudgetItem.parent_id == parent_id).all()
    for child in children:
        _delete_item_children(db, child.id)
        db.delete(child)


@app.route("/api/projects/<int:project_id>/budget/<int:item_id>/move", methods=["POST"])
@require_auth
def move_budget_item(project_id, item_id, user_id):
    # 支援 query param 與 JSON body 兩種格式
    new_parent_id = request.args.get("new_parent_id")
    if new_parent_id is None:
        data = request.get_json() or {}
        new_parent_id = data.get("new_parent_id")
    if new_parent_id is not None:
        new_parent_id = int(new_parent_id) if str(new_parent_id) != 'null' else None
    db = next(get_db())
    try:
        proj, err = _check_project_access(db, project_id, user_id)
        if err:
            return err
        item = db.query(BudgetItem).filter(
            BudgetItem.id == item_id,
            BudgetItem.project_id == project_id
        ).first()
        if not item:
            return jsonify({"detail": "預算項目不存在"}), 404
        item.parent_id = new_parent_id
        db.commit()
        return jsonify({"message": "預算項目已移動"})
    finally:
        db.close()


@app.route("/api/projects/<int:project_id>/budget/recalc", methods=["POST"])
@require_auth
def recalc_budget(project_id, user_id):
    db = next(get_db())
    try:
        proj, err = _check_project_access(db, project_id, user_id)
        if err:
            return err
        root_items = db.query(BudgetItem).filter(
            BudgetItem.project_id == project_id,
            BudgetItem.parent_id.is_(None)
        ).all()
        for item in root_items:
            if item.kind == BudgetItemKind.B:
                item.amount = _recalc_children(db, item.id)
            else:
                item.amount = _calc_amount(item)
        db.commit()
        return jsonify({"message": "預算重新計算完成"})
    finally:
        db.close()


# ═══════════════════════════════════════════════
# 資源 API
# ═══════════════════════════════════════════════

@app.route("/api/projects/<int:project_id>/resources/", methods=["GET"])
@require_auth
def list_resources(project_id, user_id):
    db = next(get_db())
    try:
        proj, err = _check_project_access(db, project_id, user_id)
        if err:
            return err
        resources = db.query(Resource).filter(Resource.project_id == project_id).all()
        return jsonify([model_to_dict(r) for r in resources])
    finally:
        db.close()


@app.route("/api/projects/<int:project_id>/resources/", methods=["POST"])
@require_auth
def create_resource(project_id, user_id):
    data = request.get_json()
    db = next(get_db())
    try:
        proj, err = _check_project_access(db, project_id, user_id)
        if err:
            return err
        resource = Resource(
            project_id=project_id,
            code=data["code"],
            c_name=data["c_name"],
            e_name=data.get("e_name"),
            c_unit=data.get("c_unit", "式"),
            e_unit=data.get("e_unit"),
            unit_price=data.get("unit_price", 0),
            category=data.get("category", "material"),
            remark=data.get("remark"),
        )
        db.add(resource)
        db.commit()
        db.refresh(resource)
        return jsonify(model_to_dict(resource)), 201
    finally:
        db.close()


@app.route("/api/projects/<int:project_id>/resources/<int:resource_id>/price", methods=["PUT"])
@require_auth
def update_resource_price(project_id, resource_id, user_id):
    # 支援 query param 與 JSON body 兩種格式
    unit_price = request.args.get("unit_price")
    if unit_price is None:
        data = request.get_json() or {}
        unit_price = data.get("unit_price")
    db = next(get_db())
    try:
        proj, err = _check_project_access(db, project_id, user_id)
        if err:
            return err
        resource = db.query(Resource).filter(
            Resource.id == resource_id,
            Resource.project_id == project_id
        ).first()
        if not resource:
            return jsonify({"detail": "資源不存在"}), 404
        if unit_price is not None:
            resource.unit_price = float(unit_price)
        db.commit()
        db.refresh(resource)
        return jsonify(model_to_dict(resource))
    finally:
        db.close()


@app.route("/api/projects/<int:project_id>/resources/<int:resource_id>", methods=["PUT"])
@require_auth
def update_resource(project_id, resource_id, user_id):
    """更新資源欄位（含單價分析相關欄位）"""
    data = request.get_json()
    db = next(get_db())
    try:
        proj, err = _check_project_access(db, project_id, user_id)
        if err:
            return err
        resource = db.query(Resource).filter(
            Resource.id == resource_id,
            Resource.project_id == project_id
        ).first()
        if not resource:
            return jsonify({"detail": "資源不存在"}), 404

        # 可更新的欄位
        for key in ("code", "c_name", "e_name", "c_unit", "e_unit", "category",
                     "remark", "is_public", "is_analysis"):
            if key in data and data[key] is not None:
                setattr(resource, key, data[key])

        if "unit_price" in data and data["unit_price"] is not None:
            resource.unit_price = float(data["unit_price"])
        if "labor_rate" in data and data["labor_rate"] is not None:
            resource.labor_rate = float(data["labor_rate"])
        if "material_rate" in data and data["material_rate"] is not None:
            resource.material_rate = float(data["material_rate"])
        if "equipment_rate" in data and data["equipment_rate"] is not None:
            resource.equipment_rate = float(data["equipment_rate"])
        if "misc_rate" in data and data["misc_rate"] is not None:
            resource.misc_rate = float(data["misc_rate"])

        db.commit()
        db.refresh(resource)
        return jsonify(model_to_dict(resource))
    finally:
        db.close()


@app.route("/api/projects/<int:project_id>/resources/analysis", methods=["GET"])
@require_auth
def list_resources_analysis(project_id, user_id):
    """列出啟用單價分析的資源（含各細項）"""
    db = next(get_db())
    try:
        proj, err = _check_project_access(db, project_id, user_id)
        if err:
            return err
        resources = db.query(Resource).filter(
            Resource.project_id == project_id,
            Resource.is_analysis == True
        ).all()
        result = []
        for r in resources:
            d = model_to_dict(r)
            items = db.query(ResourceBreakdownItem).filter(
                ResourceBreakdownItem.resource_id == r.id
            ).all()
            d["breakdown_items"] = [model_to_dict(i) for i in items]
            # 計算細項總金額
            d["breakdown_total"] = sum((i.amount or 0) for i in items)
            result.append(d)
        return jsonify(result)
    finally:
        db.close()


@app.route("/api/projects/<int:project_id>/resources/<int:resource_id>/breakdown", methods=["GET"])
@require_auth
def list_resource_breakdown(project_id, resource_id, user_id):
    """取得資源單價分析細項列表"""
    db = next(get_db())
    try:
        proj, err = _check_project_access(db, project_id, user_id)
        if err:
            return err
        resource = db.query(Resource).filter(
            Resource.id == resource_id,
            Resource.project_id == project_id
        ).first()
        if not resource:
            return jsonify({"detail": "資源不存在"}), 404
        items = db.query(ResourceBreakdownItem).filter(
            ResourceBreakdownItem.resource_id == resource_id
        ).order_by(ResourceBreakdownItem.id).all()
        return jsonify([model_to_dict(i) for i in items])
    finally:
        db.close()


@app.route("/api/projects/<int:project_id>/resources/<int:resource_id>/breakdown", methods=["POST"])
@require_auth
def create_resource_breakdown(project_id, resource_id, user_id):
    """新增資源單價分析細項"""
    data = request.get_json()
    db = next(get_db())
    try:
        proj, err = _check_project_access(db, project_id, user_id)
        if err:
            return err
        resource = db.query(Resource).filter(
            Resource.id == resource_id,
            Resource.project_id == project_id
        ).first()
        if not resource:
            return jsonify({"detail": "資源不存在"}), 404

        qty = float(data.get("quantity", 0))
        up = float(data.get("unit_price", 0))
        item = ResourceBreakdownItem(
            resource_id=resource_id,
            code=data.get("code", ""),
            c_name=data.get("c_name", ""),
            c_unit=data.get("c_unit", ""),
            quantity=qty,
            unit_price=up,
            amount=round(qty * up, 2),
            remark=data.get("remark"),
        )
        db.add(item)
        db.flush()

        # 若資源啟用單價分析，計算加總金額寫回 unit_price
        total = db.query(func.coalesce(func.sum(ResourceBreakdownItem.amount), 0)).filter(
            ResourceBreakdownItem.resource_id == resource_id
        ).scalar() or 0.0
        if resource.is_analysis:
            resource.unit_price = round(float(total), 2)

        db.commit()
        db.refresh(item)
        return jsonify(model_to_dict(item)), 201
    finally:
        db.close()


@app.route("/api/projects/<int:project_id>/resources/<int:resource_id>/breakdown/<int:breakdown_id>", methods=["DELETE"])
@require_auth
def delete_resource_breakdown(project_id, resource_id, breakdown_id, user_id):
    """刪除資源單價分析細項，並重新計算總金額"""
    db = next(get_db())
    try:
        proj, err = _check_project_access(db, project_id, user_id)
        if err:
            return err
        resource = db.query(Resource).filter(
            Resource.id == resource_id,
            Resource.project_id == project_id
        ).first()
        if not resource:
            return jsonify({"detail": "資源不存在"}), 404
        item = db.query(ResourceBreakdownItem).filter(
            ResourceBreakdownItem.id == breakdown_id,
            ResourceBreakdownItem.resource_id == resource_id
        ).first()
        if not item:
            return jsonify({"detail": "細項不存在"}), 404

        db.delete(item)
        db.flush()

        # 重新計算單價 = 細項加總
        total = db.query(func.coalesce(func.sum(ResourceBreakdownItem.amount), 0)).filter(
            ResourceBreakdownItem.resource_id == resource_id
        ).scalar() or 0.0
        if resource.is_analysis:
            resource.unit_price = round(float(total), 2)

        db.commit()
        return jsonify({"message": "細項已刪除"})
    finally:
        db.close()


@app.route("/api/projects/<int:project_id>/resources/analysis/recalc", methods=["POST"])
@require_auth
def recalc_resource_analysis(project_id, user_id):
    """重新計算所有啟用分析的資源單價（細項加總）"""
    db = next(get_db())
    try:
        proj, err = _check_project_access(db, project_id, user_id)
        if err:
            return err
        resources = db.query(Resource).filter(
            Resource.project_id == project_id,
            Resource.is_analysis == True
        ).all()
        for r in resources:
            total = db.query(func.coalesce(func.sum(ResourceBreakdownItem.amount), 0)).filter(
                ResourceBreakdownItem.resource_id == r.id
            ).scalar() or 0.0
            r.unit_price = round(float(total), 2)
        db.commit()
        return jsonify({"message": "資源單價分析重新計算完成"})
    finally:
        db.close()


# ═══════════════════════════════════════════════
# 報表 API
# ═══════════════════════════════════════════════

@app.route("/api/projects/<int:project_id>/reports/summary")
@require_auth
def get_summary_report(project_id, user_id):
    db = next(get_db())
    try:
        proj, err = _check_project_access(db, project_id, user_id)
        if err:
            return err
        items = db.query(BudgetItem).filter(BudgetItem.project_id == project_id).all()
        root_items = [i for i in items if i.parent_id is None]
        total_amount = sum(i.amount or 0 for i in root_items)

        category_totals = {}
        for i in root_items:
            k = i.kind.value if hasattr(i.kind, 'value') else str(i.kind)
            category_totals[k] = category_totals.get(k, 0) + (i.amount or 0)

        return jsonify({
            "project_id": project_id,
            "total_amount": total_amount,
            "item_count": len(items),
            "root_count": len(root_items),
            "category_totals": category_totals,
            "generated_at": datetime.now().isoformat(),
        })
    finally:
        db.close()


@app.route("/api/projects/<int:project_id>/reports/excel")
@require_auth
def export_excel(project_id, user_id):
    from openpyxl import Workbook
    from openpyxl.styles import Font, Alignment, Border, Side, PatternFill

    db = next(get_db())
    try:
        proj, err = _check_project_access(db, project_id, user_id)
        if err:
            return err
        project = db.query(Project).filter(Project.id == project_id).first()
        if not project:
            return jsonify({"detail": "專案不存在"}), 404

        items = db.query(BudgetItem).filter(
            BudgetItem.project_id == project_id
        ).order_by(BudgetItem.sort_order, BudgetItem.id).all()

        wb = Workbook()
        ws = wb.active
        ws.title = "預算總表"

        title_font = Font(name="微軟正黑體", size=16, bold=True)
        header_font = Font(name="微軟正黑體", size=11, bold=True, color="FFFFFF")
        normal_font = Font(name="微軟正黑體", size=10)
        header_fill = PatternFill(start_color="4472C4", end_color="4472C4", fill_type="solid")
        thin_border = Border(
            left=Side(style='thin'), right=Side(style='thin'),
            top=Side(style='thin'), bottom=Side(style='thin'),
        )

        ws.merge_cells('A1:H1')
        ws['A1'] = f"公共工程經費估算表 — {project.name}"
        ws['A1'].font = title_font
        ws['A1'].alignment = Alignment(horizontal='center')

        ws.merge_cells('A2:H2')
        ws['A2'] = f"專案編號: {project.code} | {datetime.now().strftime('%Y/%m/%d')}"
        ws['A2'].font = Font(name="微軟正黑體", size=10, color="666666")
        ws['A2'].alignment = Alignment(horizontal='center')

        headers = ["項次", "項目編號", "項目名稱", "單位", "數量", "單價", "複價", "備註"]
        for col, h in enumerate(headers, 1):
            cell = ws.cell(row=4, column=col, value=h)
            cell.font = header_font
            cell.fill = header_fill
            cell.alignment = Alignment(horizontal='center')
            cell.border = thin_border

        row = 5
        item_map = {i.id: i for i in items}

        def write_item(item, level=0):
            nonlocal row
            ws.cell(row=row, column=1, value=item.print_no or "").font = normal_font
            ws.cell(row=row, column=2, value=item.item_no or "").font = normal_font
            ws.cell(row=row, column=3, value=("  " * level) + (item.c_name or "")).font = normal_font
            ws.cell(row=row, column=4, value=item.c_unit or "").font = normal_font
            ws.cell(row=row, column=5, value=item.quantity or "").font = normal_font
            ws.cell(row=row, column=6, value=item.unit_price or "").font = normal_font
            ws.cell(row=row, column=7, value=item.amount or "").font = normal_font
            ws.cell(row=row, column=8, value=item.memo or "").font = normal_font
            for c in range(1, 9):
                ws.cell(row=row, column=c).border = thin_border
                ws.cell(row=row, column=c).alignment = Alignment(horizontal='center' if c <= 2 else 'left')
            row += 1

            # 子項
            children = [i for i in items if i.parent_id == item.id]
            for child in children:
                write_item(child, level + 1)

        # 只寫根節點
        root_items = [i for i in items if i.parent_id is None]
        for item in root_items:
            write_item(item)

        # 合計
        total = sum(i.amount or 0 for i in root_items)
        ws.cell(row=row, column=6, value="總計").font = Font(name="微軟正黑體", size=11, bold=True)
        ws.cell(row=row, column=7, value=total).font = Font(name="微軟正黑體", size=11, bold=True)
        ws.cell(row=row, column=7).number_format = '#,##0'
        for c in range(1, 9):
            ws.cell(row=row, column=c).border = thin_border

        ws.column_dimensions['A'].width = 12
        ws.column_dimensions['B'].width = 15
        ws.column_dimensions['C'].width = 50
        ws.column_dimensions['D'].width = 10
        ws.column_dimensions['E'].width = 12
        ws.column_dimensions['F'].width = 14
        ws.column_dimensions['G'].width = 16
        ws.column_dimensions['H'].width = 20

        os.makedirs(REPORT_DIR, exist_ok=True)
        filepath = os.path.join(REPORT_DIR, f"budget_{project_id}.xlsx")
        wb.save(filepath)

        return send_file(
            filepath,
            mimetype="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            as_attachment=True,
            download_name=f"PCCES_{project.code}_預算表.xlsx",
        )
    finally:
        db.close()


# ─── 前端靜態檔案服務 ───
# Vercel 會將 outputDirectory 的靜態檔案自動對應到根路徑
# 此 catch-all 僅處理 SPA fallback（其他靜態檔由 Vercel 直接 serve）
FRONTEND_DIST = os.path.join(os.path.dirname(__file__), 'static')


@app.route("/", defaults={"path": ""})
@app.route("/<path:path>")
def serve_frontend(path):
    """提供前端靜態檔案，非 API 路徑一律回傳 index.html（SPA fallback）"""
    # API 路徑跳過
    if path and path.startswith("api/"):
        return jsonify({"detail": "Not found"}), 404
    # 先嘗試找實際檔案
    if path:
        file_path = os.path.join(FRONTEND_DIST, path)
        if os.path.isfile(file_path):
            return send_from_directory(FRONTEND_DIST, path)
    # SPA fallback：所有其他路徑回傳 index.html
    index_path = os.path.join(FRONTEND_DIST, "index.html")
    if os.path.exists(index_path):
        return send_from_directory(FRONTEND_DIST, "index.html")
    return jsonify({"detail": "App not built"}), 503


from api.seed_data import seed_demo_data

# Vercel Serverless：在首次請求時初始化資料庫
@app.before_request
def _ensure_db():
    """確保資料庫表格存在 + 寫入示範資料（僅執行一次）"""
    if not hasattr(app, '_db_initialized'):
        init_db()
        db = next(get_db())
        try:
            seeded = seed_demo_data(db)
            if seeded:
                print("✅ 示範資料已寫入資料庫")
        finally:
            db.close()
        app._db_initialized = True


# ═══════════════════════════════════════════════
# 啟動
# ═══════════════════════════════════════════════

if __name__ == "__main__":
    app.run(host="0.0.0.0", port=8000, debug=True)
