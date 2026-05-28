"""PCCES 資料庫模型（純 SQLAlchemy，無外部依賴）"""

from datetime import datetime, timezone
from sqlalchemy import (
    String, Integer, Float, Boolean, Text, DateTime,
    ForeignKey, Enum as SAEnum, JSON, Column
)
from sqlalchemy.orm import DeclarativeBase, relationship


class Base(DeclarativeBase):
    pass


# ─── 枚舉 ───
import enum


class UserRole(str, enum.Enum):
    ADMIN = "admin"
    REVIEWER = "reviewer"
    EDITOR = "editor"
    VIEWER = "viewer"


class BudgetItemKind(str, enum.Enum):
    B = "B"  # 主要項目（自動加總子項）
    L = "L"  # 單價項目（直接輸入金額）
    F = "F"  # 公式計價
    S = "S"  # 分段計價
    Z = "Z"  # 小計/合計
    U = "U"  # 自訂公式
    W = "W"  # 工作項目（葉節點）


def _now():
    return datetime.now(timezone.utc)


# ─── 使用者 ───

class User(Base):
    __tablename__ = "users"

    id = Column(Integer, primary_key=True, autoincrement=True)
    username = Column(String(50), unique=True, index=True, nullable=False)
    password_hash = Column(String(255), nullable=False)
    display_name = Column(String(100), nullable=False)
    email = Column(String(200), nullable=True)
    company = Column(String(200), nullable=True)
    department = Column(String(200), nullable=True)
    phone = Column(String(50), nullable=True)
    role = Column(String(20), default=UserRole.EDITOR.value)
    is_active = Column(Boolean, default=True)
    created_at = Column(DateTime, default=_now)
    updated_at = Column(DateTime, default=_now, onupdate=_now)


# ─── 資源單價分析細項 ───

class ResourceBreakdownItem(Base):
    """資源單價分析細項（工、料、機、雜項組成）"""
    __tablename__ = "resource_breakdown_items"

    id = Column(Integer, primary_key=True, autoincrement=True)
    resource_id = Column(Integer, ForeignKey("resources.id", ondelete="CASCADE"), nullable=False)
    code = Column(String(50), nullable=False)           # PccesCode
    c_name = Column(String(300), nullable=False)         # 中文名稱
    c_unit = Column(String(50))                          # 單位
    quantity = Column(Float, default=0)                  # 數量
    unit_price = Column(Float, default=0)                # 單價
    amount = Column(Float, default=0)                    # 金額 = 數量 × 單價
    remark = Column(Text, nullable=True)                 # 備註
    created_at = Column(DateTime, default=_now)
    updated_at = Column(DateTime, default=_now, onupdate=_now)


# ─── 專案 ───

class Project(Base):
    __tablename__ = "projects"

    id = Column(Integer, primary_key=True, autoincrement=True)
    code = Column(String(50), unique=True, index=True, nullable=False)
    name = Column(String(300), nullable=False)
    name_en = Column(String(300), nullable=True)
    location = Column(String(500), nullable=True)
    account_code = Column(String(100), nullable=True)
    description = Column(Text, nullable=True)
    scope = Column(Float, default=1.0)
    scope_unit = Column(String(50), default="式")
    owner_id = Column(Integer, ForeignKey("users.id"), nullable=False)
    status = Column(String(20), default="active")
    created_at = Column(DateTime, default=_now)
    updated_at = Column(DateTime, default=_now, onupdate=_now)


# ─── 預算項目（樹狀結構） ───

class BudgetItem(Base):
    __tablename__ = "budget_items"

    id = Column(Integer, primary_key=True, autoincrement=True)
    project_id = Column(Integer, ForeignKey("projects.id", ondelete="CASCADE"), nullable=False)
    parent_id = Column(Integer, ForeignKey("budget_items.id", ondelete="SET NULL"), nullable=True)

    item_no = Column(String(50), nullable=True)
    print_no = Column(String(50), nullable=True)
    c_name = Column(String(500), nullable=True)
    e_name = Column(String(500), nullable=True)
    c_unit = Column(String(50), nullable=True)
    e_unit = Column(String(50), nullable=True)

    quantity = Column(Float, default=0)
    unit_price = Column(Float, default=0)
    amount = Column(Float, default=0)

    kind = Column(String(10), default="B")
    formula = Column(Text, nullable=True)
    memo = Column(Text, nullable=True)
    pcces_code = Column(String(50), nullable=True)
    account_code = Column(String(100), nullable=True)
    sort_order = Column(String(50), nullable=True)
    level_no = Column(Integer, default=0)
    is_fixed_price = Column(Boolean, default=False)
    is_locked = Column(Boolean, default=False)
    decimal_qty = Column(Integer, default=2)
    decimal_price = Column(Integer, default=2)
    decimal_amount = Column(Integer, default=2)
    is_green_item = Column(Boolean, default=False)
    is_green_method = Column(Boolean, default=False)
    is_green_material = Column(Boolean, default=False)
    is_green_energy = Column(Boolean, default=False)
    custom_fields = Column(JSON, default=dict)

    created_at = Column(DateTime, default=_now)
    updated_at = Column(DateTime, default=_now, onupdate=_now)

    # 父子關係（用於樹狀結構）
    # 注意：不要使用 delete-orphan，因為這是多對一關係的「多」方
    children = relationship("BudgetItem", backref="parent", remote_side=[id],
                           passive_deletes=True)


# ─── 資源 ───

class Resource(Base):
    __tablename__ = "resources"

    id = Column(Integer, primary_key=True, autoincrement=True)
    project_id = Column(Integer, ForeignKey("projects.id", ondelete="CASCADE"), nullable=False)
    code = Column(String(50), index=True, nullable=False)
    c_name = Column(String(300), nullable=False)
    e_name = Column(String(300), nullable=True)
    c_unit = Column(String(50))
    e_unit = Column(String(50), nullable=True)
    unit_price = Column(Float, default=0)
    category = Column(String(20), default="material")
    is_public = Column(Boolean, default=False)
    remark = Column(Text, nullable=True)
    # ── 單價分析欄位 ──
    is_analysis = Column(Boolean, default=False)       # 是否啟用單價分析
    labor_rate = Column(Float, default=0)               # LRate 人工比率 (%)
    material_rate = Column(Float, default=0)             # MRate 材料比率 (%)
    equipment_rate = Column(Float, default=0)            # ERate 設備比率 (%)
    misc_rate = Column(Float, default=0)                 # WRate 雜項比率 (%)
    created_at = Column(DateTime, default=_now)
    updated_at = Column(DateTime, default=_now, onupdate=_now)
