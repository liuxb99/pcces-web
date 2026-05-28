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

# ─── 系統維護：系統參數 ───

class SystemParameter(Base):
    """系統參數（對應原始 FormSys_E/F/G）"""
    __tablename__ = "system_parameters"

    id = Column(Integer, primary_key=True, autoincrement=True)
    category = Column(String(50), nullable=False, index=True)   # E / F / G
    code = Column(String(100), nullable=False)
    c_name = Column(String(300), nullable=False)
    c_value = Column(Text, nullable=True)
    c_default = Column(Text, nullable=True)
    sort_order = Column(Integer, default=0)
    is_active = Column(Boolean, default=True)
    memo = Column(Text, nullable=True)
    created_at = Column(DateTime, default=_now)
    updated_at = Column(DateTime, default=_now, onupdate=_now)


# ─── 系統維護：代碼表 ───

class CodeTable(Base):
    """代碼主表（部門編碼、公物編碼等）"""
    __tablename__ = "code_tables"

    id = Column(Integer, primary_key=True, autoincrement=True)
    table_code = Column(String(50), unique=True, nullable=False, index=True)
    table_name = Column(String(300), nullable=False)
    memo = Column(Text, nullable=True)
    is_active = Column(Boolean, default=True)
    created_at = Column(DateTime, default=_now)
    updated_at = Column(DateTime, default=_now, onupdate=_now)

    # 關聯
    items = relationship("CodeItem", backref="table", passive_deletes=True,
                        order_by="CodeItem.sort_order, CodeItem.code")


class CodeItem(Base):
    """代碼表細項（支援樹狀結構）"""
    __tablename__ = "code_items"

    id = Column(Integer, primary_key=True, autoincrement=True)
    table_id = Column(Integer, ForeignKey("code_tables.id", ondelete="CASCADE"), nullable=False)
    parent_id = Column(Integer, ForeignKey("code_items.id", ondelete="SET NULL"), nullable=True)
    code = Column(String(50), nullable=False)
    c_name = Column(String(300), nullable=False)
    sort_order = Column(Integer, default=0)
    is_active = Column(Boolean, default=True)
    ext_data = Column(JSON, nullable=True)          # 擴充欄位（公物編碼額外資料）
    memo = Column(Text, nullable=True)
    created_at = Column(DateTime, default=_now)
    updated_at = Column(DateTime, default=_now, onupdate=_now)

    # 樹狀父子關係
    children = relationship("CodeItem", backref="parent", remote_side=[id])


# ─── 系統維護：組織機構 ───

class FeatureFlag(Base):
    """功能開關（對應原始 SysPlugin 的插件啟用/停用）"""
    __tablename__ = "feature_flags"

    id = Column(Integer, primary_key=True, autoincrement=True)
    flag_key = Column(String(100), unique=True, index=True, nullable=False)   # 功能代號，如 "budget_compare"
    display_name = Column(String(300), nullable=False)                        # 顯示名稱，如 "工項比較"
    description = Column(Text, nullable=True)                                 # 功能說明
    category = Column(String(50), default="general")                          # 分類：general / budget / mrs / contract / invoice / report / admin
    is_enabled = Column(Boolean, default=True)                                # 是否啟用
    is_system = Column(Boolean, default=False)                                # 系統核心功能（不可停用）
    sort_order = Column(Integer, default=0)                                   # 排序
    created_at = Column(DateTime, default=_now)
    updated_at = Column(DateTime, default=_now, onupdate=_now)


class Organization(Base):
    """組織機構（樹狀結構）"""
    __tablename__ = "organizations"

    id = Column(Integer, primary_key=True, autoincrement=True)
    parent_id = Column(Integer, ForeignKey("organizations.id", ondelete="SET NULL"), nullable=True)
    code = Column(String(50), unique=True, nullable=False, index=True)
    c_name = Column(String(300), nullable=False)
    org_type = Column(String(50), default="部門")       # 機關 / 部門 / 課室
    sort_order = Column(Integer, default=0)
    is_active = Column(Boolean, default=True)
    contact_person = Column(String(100), nullable=True)
    contact_phone = Column(String(50), nullable=True)
    address = Column(String(500), nullable=True)
    memo = Column(Text, nullable=True)
    created_at = Column(DateTime, default=_now)
    updated_at = Column(DateTime, default=_now, onupdate=_now)

    # 樹狀父子關係
    children = relationship("Organization", backref="parent", remote_side=[id])


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

    # MrsBase 引用（選用：此預算項目引用了哪個公共單價）
    mrs_base_item_id = Column(Integer, ForeignKey("mrs_base_items.id", ondelete="SET NULL"), nullable=True)


# ─── 資源 ───

# ─── 計價主檔 ───

class Invoice(Base):
    """計價主檔"""
    __tablename__ = "invoices"

    id = Column(Integer, primary_key=True, autoincrement=True)
    project_id = Column(Integer, ForeignKey("projects.id", ondelete="CASCADE"), nullable=False)
    invoice_no = Column(String(50), nullable=False)
    period_no = Column(Integer, nullable=False, default=1)
    c_name = Column(String(300), nullable=True)
    status = Column(String(20), default="draft")  # draft / submitted / approved
    total_amount = Column(Float, default=0)
    cumulative_amount = Column(Float, default=0)
    progress_rate = Column(Float, default=0)
    description = Column(Text, nullable=True)
    invoice_date = Column(String(20), nullable=True)  # YYYY-MM-DD
    remark = Column(Text, nullable=True)
    created_by = Column(Integer, ForeignKey("users.id"), nullable=True)
    created_at = Column(DateTime, default=_now)
    updated_at = Column(DateTime, default=_now, onupdate=_now)

    # 關係
    items = relationship("InvoiceItem", backref="invoice", passive_deletes=True,
                        order_by="InvoiceItem.sort_order, InvoiceItem.id")


class InvoiceItem(Base):
    """計價明細"""
    __tablename__ = "invoice_items"

    id = Column(Integer, primary_key=True, autoincrement=True)
    invoice_id = Column(Integer, ForeignKey("invoices.id", ondelete="CASCADE"), nullable=False)
    budget_item_id = Column(Integer, ForeignKey("budget_items.id", ondelete="SET NULL"), nullable=True)

    item_no = Column(String(50), nullable=True)
    print_no = Column(String(50), nullable=True)
    c_name = Column(String(500), nullable=True)
    c_unit = Column(String(50), nullable=True)

    contract_qty = Column(Float, default=0)
    unit_price = Column(Float, default=0)

    prev_completed_qty = Column(Float, default=0)
    this_completed_qty = Column(Float, default=0)
    total_completed_qty = Column(Float, default=0)
    remain_qty = Column(Float, default=0)

    this_amount = Column(Float, default=0)
    cumulative_amount = Column(Float, default=0)
    progress_rate = Column(Float, default=0)

    sort_order = Column(String(50), nullable=True)
    remark = Column(Text, nullable=True)
    created_at = Column(DateTime, default=_now)
    updated_at = Column(DateTime, default=_now, onupdate=_now)


class Contract(Base):
    """分包合約主檔"""
    __tablename__ = "contracts"

    id = Column(Integer, primary_key=True, autoincrement=True)
    project_id = Column(Integer, ForeignKey("projects.id", ondelete="CASCADE"), nullable=False)
    contract_no = Column(String(50), nullable=False)
    c_name = Column(String(300), nullable=False)
    contractor = Column(String(200), nullable=True)          # 承包商
    contract_amount = Column(Float, default=0)               # 合約金額
    total_paid_amount = Column(Float, default=0)             # 累計支付
    total_issue_amount = Column(Float, default=0)            # 累計期別金額
    settlement_amount = Column(Float, default=0)             # 結算金額
    status = Column(String(20), default="draft")             # draft/active/closed/finalized
    start_date = Column(String(20), nullable=True)
    end_date = Column(String(20), nullable=True)
    remark = Column(Text, nullable=True)
    created_at = Column(DateTime, default=_now)
    updated_at = Column(DateTime, default=_now, onupdate=_now)

    # 關係
    items = relationship("ContractItem", backref="contract", passive_deletes=True,
                        order_by="ContractItem.sort_order, ContractItem.id")
    issues = relationship("ContractIssue", backref="contract", passive_deletes=True,
                         order_by="ContractIssue.issue_no")
    settlements = relationship("ContractSettlement", backref="contract", passive_deletes=True,
                              order_by="ContractSettlement.id")
    acceptances = relationship("ContractFinalAcceptance", backref="contract", passive_deletes=True,
                              order_by="ContractFinalAcceptance.id")


class ContractItem(Base):
    """分包合約工項明細"""
    __tablename__ = "contract_items"

    id = Column(Integer, primary_key=True, autoincrement=True)
    contract_id = Column(Integer, ForeignKey("contracts.id", ondelete="CASCADE"), nullable=False)
    budget_item_id = Column(Integer, ForeignKey("budget_items.id"), nullable=True)

    item_no = Column(String(50), nullable=True)
    print_no = Column(String(50), nullable=True)
    c_name = Column(String(500), nullable=True)
    c_unit = Column(String(50), nullable=True)
    contract_qty = Column(Float, default=0)
    unit_price = Column(Float, default=0)
    amount = Column(Float, default=0)
    completed_qty = Column(Float, default=0)
    completed_amount = Column(Float, default=0)
    remark = Column(Text, nullable=True)
    sort_order = Column(Integer, default=0)


class ContractIssue(Base):
    """分包合約期別計價主檔"""
    __tablename__ = "contract_issues"

    id = Column(Integer, primary_key=True, autoincrement=True)
    contract_id = Column(Integer, ForeignKey("contracts.id", ondelete="CASCADE"), nullable=False)
    issue_no = Column(Integer, nullable=False)
    c_name = Column(String(300), nullable=True)
    status = Column(String(20), default="draft")             # draft/submitted/approved
    total_amount = Column(Float, default=0)
    cumulative_amount = Column(Float, default=0)
    progress_rate = Column(Float, default=0)
    remark = Column(Text, nullable=True)
    issue_date = Column(String(20), nullable=True)
    created_by = Column(Integer, ForeignKey("users.id"), nullable=True)
    created_at = Column(DateTime, default=_now)
    updated_at = Column(DateTime, default=_now, onupdate=_now)

    # 關係
    items = relationship("ContractIssueItem", backref="issue", passive_deletes=True,
                        order_by="ContractIssueItem.id")


class ContractIssueItem(Base):
    """分包合約期別計價明細"""
    __tablename__ = "contract_issue_items"

    id = Column(Integer, primary_key=True, autoincrement=True)
    issue_id = Column(Integer, ForeignKey("contract_issues.id", ondelete="CASCADE"), nullable=False)
    contract_item_id = Column(Integer, ForeignKey("contract_items.id", ondelete="CASCADE"), nullable=True)

    c_name = Column(String(500), nullable=True)
    c_unit = Column(String(50), nullable=True)
    contract_qty = Column(Float, default=0)
    unit_price = Column(Float, default=0)
    prev_completed_qty = Column(Float, default=0)
    this_completed_qty = Column(Float, default=0)
    total_completed_qty = Column(Float, default=0)
    remain_qty = Column(Float, default=0)
    this_amount = Column(Float, default=0)
    cumulative_amount = Column(Float, default=0)
    progress_rate = Column(Float, default=0)
    remark = Column(Text, nullable=True)
    created_at = Column(DateTime, default=_now)
    updated_at = Column(DateTime, default=_now, onupdate=_now)


class ContractSettlement(Base):
    """分包結算主檔"""
    __tablename__ = "contract_settlements"

    id = Column(Integer, primary_key=True, autoincrement=True)
    contract_id = Column(Integer, ForeignKey("contracts.id", ondelete="CASCADE"), nullable=False)
    settlement_no = Column(String(50), nullable=False)
    c_name = Column(String(300), nullable=True)
    settlement_date = Column(String(20), nullable=True)
    contract_amount = Column(Float, default=0)               # 原合約金額（快照）
    total_add_amount = Column(Float, default=0)              # 追加金額合計
    total_deduct_amount = Column(Float, default=0)           # 扣減金額合計
    settlement_amount = Column(Float, default=0)             # 結算總金額
    remark = Column(Text, nullable=True)
    status = Column(String(20), default="draft")             # draft/submitted/approved
    created_by = Column(Integer, ForeignKey("users.id"), nullable=True)
    created_at = Column(DateTime, default=_now)
    updated_at = Column(DateTime, default=_now, onupdate=_now)

    # 關係
    items = relationship("ContractSettlementItem", backref="settlement", passive_deletes=True,
                        order_by="ContractSettlementItem.id")


class ContractSettlementItem(Base):
    """分包結算明細"""
    __tablename__ = "contract_settlement_items"

    id = Column(Integer, primary_key=True, autoincrement=True)
    settlement_id = Column(Integer, ForeignKey("contract_settlements.id", ondelete="CASCADE"), nullable=False)
    budget_item_id = Column(Integer, ForeignKey("budget_items.id", ondelete="SET NULL"), nullable=True)

    c_name = Column(String(500), nullable=True)
    c_unit = Column(String(50), nullable=True)
    contract_qty = Column(Float, default=0)
    contract_unit_price = Column(Float, default=0)
    contract_amount = Column(Float, default=0)
    actual_qty = Column(Float, default=0)
    actual_unit_price = Column(Float, default=0)
    actual_amount = Column(Float, default=0)
    diff_amount = Column(Float, default=0)
    remark = Column(Text, nullable=True)
    created_at = Column(DateTime, default=_now)
    updated_at = Column(DateTime, default=_now, onupdate=_now)


class ContractFinalAcceptance(Base):
    """分包終驗主檔"""
    __tablename__ = "contract_final_acceptances"

    id = Column(Integer, primary_key=True, autoincrement=True)
    contract_id = Column(Integer, ForeignKey("contracts.id", ondelete="CASCADE"), nullable=False)
    acceptance_no = Column(String(50), nullable=False)
    c_name = Column(String(300), nullable=True)
    acceptance_date = Column(String(20), nullable=True)
    inspector = Column(String(100), nullable=True)
    result = Column(String(50), nullable=True)               # pass/conditional_pass/fail
    defect_description = Column(Text, nullable=True)
    remark = Column(Text, nullable=True)
    status = Column(String(20), default="draft")             # draft/submitted/approved
    created_by = Column(Integer, ForeignKey("users.id"), nullable=True)
    created_at = Column(DateTime, default=_now)
    updated_at = Column(DateTime, default=_now, onupdate=_now)

    # 關係
    items = relationship("ContractFinalAcceptanceItem", backref="acceptance", passive_deletes=True,
                        order_by="ContractFinalAcceptanceItem.id")


class ContractFinalAcceptanceItem(Base):
    """分包終驗明細"""
    __tablename__ = "contract_final_acceptance_items"

    id = Column(Integer, primary_key=True, autoincrement=True)
    acceptance_id = Column(Integer, ForeignKey("contract_final_acceptances.id", ondelete="CASCADE"), nullable=False)
    budget_item_id = Column(Integer, ForeignKey("budget_items.id", ondelete="SET NULL"), nullable=True)

    c_name = Column(String(500), nullable=True)
    c_unit = Column(String(50), nullable=True)
    contract_qty = Column(Float, default=0)
    actual_qty = Column(Float, default=0)
    accepted_qty = Column(Float, default=0)
    rejected_qty = Column(Float, default=0)
    remark = Column(Text, nullable=True)
    created_at = Column(DateTime, default=_now)
    updated_at = Column(DateTime, default=_now, onupdate=_now)


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


# ─── MrsBase：公共單價庫 ───

class MrsBaseCategory(Base):
    """公共單價分類（樹狀結構）"""
    __tablename__ = "mrs_base_categories"

    id = Column(Integer, primary_key=True, autoincrement=True)
    parent_id = Column(Integer, ForeignKey("mrs_base_categories.id"), nullable=True)
    code = Column(String(50), nullable=False, index=True)
    c_name = Column(String(300), nullable=False)
    sort_order = Column(Integer, default=0)
    level_no = Column(Integer, default=0)
    created_at = Column(DateTime, default=_now)
    updated_at = Column(DateTime, default=_now, onupdate=_now)

    # 父子關係（樹狀結構）
    children = relationship("MrsBaseCategory", backref="parent", remote_side=[id])


class MrsBaseItem(Base):
    """公共單價項目（跨專案共享的工項單價庫）"""
    __tablename__ = "mrs_base_items"

    id = Column(Integer, primary_key=True, autoincrement=True)
    category_id = Column(Integer, ForeignKey("mrs_base_categories.id"), nullable=False)
    code = Column(String(50), unique=True, index=True, nullable=False)
    pub_code = Column(String(50), nullable=True)         # 公共工程代碼
    c_name = Column(String(500), nullable=False)
    e_name = Column(String(500), nullable=True)
    c_unit = Column(String(50), nullable=False, default="式")
    e_unit = Column(String(50), nullable=True)
    unit_price = Column(Float, default=0)
    cost_kind = Column(String(10), default="料")         # 成本種類：1=工,2=料,3=機,4=雜
    item_type = Column(String(10), default="W")          # 項目類型：B/L/W…
    is_analysis = Column(Boolean, default=False)         # 是否啟用單價分析
    labor_rate = Column(Float, default=0)                # 人工比率 %
    material_rate = Column(Float, default=0)             # 材料比率 %
    equipment_rate = Column(Float, default=0)            # 設備比率 %
    misc_rate = Column(Float, default=0)                 # 雜項比率 %
    decimal_qty = Column(Integer, default=2)
    decimal_price = Column(Integer, default=2)
    decimal_amount = Column(Integer, default=2)
    memo = Column(Text, nullable=True)
    is_approved = Column(Boolean, default=False)         # 是否已審核
    approved_by = Column(Integer, ForeignKey("users.id"), nullable=True)
    approved_at = Column(DateTime, nullable=True)
    created_by = Column(Integer, ForeignKey("users.id"), nullable=False)
    created_at = Column(DateTime, default=_now)
    updated_at = Column(DateTime, default=_now, onupdate=_now)

    # 關聯
    breakdown_items = relationship("MrsBaseBreakdownItem", backref="item", passive_deletes=True,
                                   order_by="MrsBaseBreakdownItem.id")
    bookmarks = relationship("MrsBaseBookmark", backref="item", passive_deletes=True)


class MrsBaseBreakdownItem(Base):
    """公共單價工料機組成（單價分析細項）"""
    __tablename__ = "mrs_base_breakdown_items"

    id = Column(Integer, primary_key=True, autoincrement=True)
    item_id = Column(Integer, ForeignKey("mrs_base_items.id", ondelete="CASCADE"), nullable=False)
    code = Column(String(50), nullable=False)
    c_name = Column(String(300), nullable=False)
    c_unit = Column(String(50), nullable=False, default="式")
    quantity = Column(Float, default=0)
    unit_price = Column(Float, default=0)
    amount = Column(Float, default=0)                    # 金額 = 數量 × 單價
    category = Column(String(20), default="material")    # labor/material/equipment/misc
    remark = Column(Text, nullable=True)
    created_at = Column(DateTime, default=_now)
    updated_at = Column(DateTime, default=_now, onupdate=_now)


class MrsBaseBookmark(Base):
    """公共單價書籤"""
    __tablename__ = "mrs_base_bookmarks"

    id = Column(Integer, primary_key=True, autoincrement=True)
    user_id = Column(Integer, ForeignKey("users.id"), nullable=False)
    item_id = Column(Integer, ForeignKey("mrs_base_items.id", ondelete="CASCADE"), nullable=False)
    created_at = Column(DateTime, default=_now)
