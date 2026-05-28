"""PCCES 起始示範資料 — 資料庫為空時自動建立"""

from datetime import datetime, timezone
from sqlalchemy.orm import Session

from api.models import User, Project, BudgetItem, Resource, ResourceBreakdownItem, BudgetItemKind

# 直接複製 main.py 的 hash 函式，避免循環 import
import secrets
from hashlib import pbkdf2_hmac


def _hash_password(password: str) -> str:
    salt = secrets.token_hex(16)
    key = pbkdf2_hmac("sha256", password.encode(), salt.encode(), 100000).hex()
    return f"{salt}${key}"


def seed_demo_data(db: Session) -> bool:
    """若資料庫為空則建立示範資料，回傳是否已寫入"""
    if db.query(Project).count() > 0:
        return False  # 已有資料，不重複寫入

    # 1. 建立示範使用者
    demo_user = User(
        username="demo",
        password_hash=_hash_password("demo123"),  # 使用 PBKDF2-SHA256 加鹽雜湊
        display_name="示範使用者",
        company="測試機關",
    )
    db.add(demo_user)
    db.flush()

    # 2. 建立示範專案
    project = Project(
        code="DEMO001",
        name="OO 大樓新建工程",
        name_en="OO Building Construction Project",
        location="台北市大安區",
        account_code="A123456",
        description="本工程為地上12層、地下3層之鋼筋混凝土建築物，總樓地板面積約12,000㎡。",
        scope=1.0,
        scope_unit="式",
        owner_id=demo_user.id,
    )
    db.add(project)
    db.flush()

    # 3. 建立預算樹狀結構
    # 第1層：直接工程費 (B)
    direct_cost = BudgetItem(
        project_id=project.id,
        c_name="直接工程費",
        kind=BudgetItemKind.B,
        print_no="0001",
    )
    db.add(direct_cost)
    db.flush()

    # 第2層：各分項工程 (B)
    items_data = [
        ("0001.01", "開挖及安全措施工程", 1, 2500000),
        ("0001.02", "結構體工程", 1, 18500000),
        ("0001.03", "裝修工程", 1, 6200000),
        ("0001.04", "機電設備工程", 1, 8300000),
        ("0001.05", "外部工程", 1, 3500000),
    ]
    for print_no, c_name, qty, price in items_data:
        item = BudgetItem(
            project_id=project.id,
            parent_id=direct_cost.id,
            c_name=c_name,
            kind=BudgetItemKind.B,
            print_no=print_no,
            quantity=qty,
            unit_price=price,
        )
        db.add(item)
        db.flush()

        # 第3層：各分項下的細項 (W)
        if "開挖" in c_name:
            sub_items = [
                ("0001.01.01", "擋土支撐工程", "式", 1, 800000),
                ("0001.01.02", "開挖及運棄", "m³", 4500, 320),
                ("0001.01.03", "安全觀測系統", "組", 6, 45000),
            ]
        elif "結構" in c_name:
            sub_items = [
                ("0001.02.01", "鋼筋工程", "噸", 850, 28500),
                ("0001.02.02", "混凝土工程", "m³", 5200, 2100),
                ("0001.02.03", "模板工程", "㎡", 8500, 680),
            ]
        elif "裝修" in c_name:
            sub_items = [
                ("0001.03.01", "內部隔間工程", "㎡", 3200, 850),
                ("0001.03.02", "天花板工程", "㎡", 2800, 620),
                ("0001.03.03", "地坪工程", "㎡", 2500, 780),
            ]
        elif "機電" in c_name:
            sub_items = [
                ("0001.04.01", "電力配線工程", "式", 1, 2800000),
                ("0001.04.02", "給排水工程", "式", 1, 1800000),
                ("0001.04.03", "空調工程", "式", 1, 2200000),
            ]
        else:
            sub_items = [
                ("0001.05.01", "景觀植栽工程", "式", 1, 1200000),
                ("0001.05.02", "圍牆及大門工程", "式", 1, 800000),
                ("0001.05.03", "道路及停車場", "式", 1, 950000),
            ]

        for sub_print, sub_name, unit, qty, price in sub_items:
            sub = BudgetItem(
                project_id=project.id,
                parent_id=item.id,
                c_name=sub_name,
                c_unit=unit,
                kind=BudgetItemKind.W,
                print_no=sub_print,
                quantity=qty,
                unit_price=price,
            )
            sub.amount = round(sub.quantity * sub.unit_price, sub.decimal_amount) if sub.kind != BudgetItemKind.B else 0.0
            db.add(sub)

    # 4. 第1層：間接工程費 (B)
    indirect_cost = BudgetItem(
        project_id=project.id,
        c_name="間接工程費",
        kind=BudgetItemKind.B,
        print_no="0002",
    )
    db.add(indirect_cost)
    db.flush()

    indirect_items = [
        ("0002.01", "品管費用", "式", 1, 1200000),
        ("0002.02", "勞工安全衛生費", "式", 1, 350000),
        ("0002.03", "環境保護費", "式", 1, 280000),
        ("0002.04", "工程管理費", "式", 1, 1800000),
    ]
    for print_no, c_name, unit, qty, price in indirect_items:
        item = BudgetItem(
            project_id=project.id,
            parent_id=indirect_cost.id,
            c_name=c_name,
            c_unit=unit,
            kind=BudgetItemKind.W,
            print_no=print_no,
            quantity=qty,
            unit_price=price,
        )
        item.amount = round(item.quantity * item.unit_price, item.decimal_amount)
        db.add(item)

    # 5. 第1層：利潤及營業稅 (Z — 小計)
    profit = BudgetItem(
        project_id=project.id,
        c_name="利潤及營業稅",
        kind=BudgetItemKind.Z,
        print_no="0003",
    )
    db.add(profit)
    db.flush()

    profit_items = [
        ("0003.01", "包商利潤（約 5%）", "式", 1, 0),
        ("0003.02", "營業稅（5%）", "式", 1, 0),
    ]
    for print_no, c_name, unit, qty, price in profit_items:
        item = BudgetItem(
            project_id=project.id,
            parent_id=profit.id,
            c_name=c_name,
            c_unit=unit,
            kind=BudgetItemKind.W,
            print_no=print_no,
            quantity=qty,
            unit_price=price,
        )
        db.add(item)

    # 6. 建立資源
    resources = [
        ("L001", "模板工", "工", "labor", 3500, "模板組立及拆除"),
        ("L002", "鋼筋工", "工", "labor", 3800, "鋼筋加工及綁紮"),
        ("L003", "混凝土工", "工", "labor", 3200, "混凝土澆置及養護"),
        ("M001", "鋼筋 SD420", "噸", "material", 26500, "SD420 竹節鋼筋"),
        ("M002", "預拌混凝土 3500psi", "m³", "material", 1850, "3500psi 混凝土"),
        ("M003", "模板用合板", "片", "material", 420, "厚度 12mm"),
        ("E001", "挖土機 0.6m³", "天", "equipment", 8500, "0.6m³ 級挖土機"),
        ("E002", "混凝土泵送車", "天", "equipment", 12000, "50m 泵送車"),
        ("E003", "吊車 25T", "天", "equipment", 18000, "25 噸移動式吊車"),
    ]
    for code, c_name, unit, category, price, remark in resources:
        res = Resource(
            project_id=project.id,
            code=code,
            c_name=c_name,
            c_unit=unit,
            category=category,
            unit_price=price,
            remark=remark,
        )
        db.add(res)

    db.flush()

    # 6b. 為部分資源啟用單價分析並建立示範細項
    # 查詢剛建立的資源
    res_map = {r.code: r for r in db.query(Resource).filter(Resource.project_id == project.id).all()}

    # ── 鋼筋 SD420 (M001)：分析細項 ──
    if "M001" in res_map:
        m001 = res_map["M001"]
        m001.is_analysis = True
        m001.labor_rate = 30.0
        m001.material_rate = 60.0
        m001.equipment_rate = 8.0
        m001.misc_rate = 2.0
        # 工
        item1 = ResourceBreakdownItem(resource_id=m001.id, code="L002", c_name="鋼筋工",
            c_unit="工", quantity=0.012, unit_price=3800, amount=round(0.012*3800, 2))
        db.add(item1)
        # 料
        item2 = ResourceBreakdownItem(resource_id=m001.id, code="M001", c_name="鋼筋 SD420",
            c_unit="噸", quantity=1.0, unit_price=25000, amount=25000)
        db.add(item2)
        # 機
        item3 = ResourceBreakdownItem(resource_id=m001.id, code="E003", c_name="吊車",
            c_unit="天", quantity=0.003, unit_price=18000, amount=round(0.003*18000, 2))
        db.add(item3)
        # 加總寫回單價
        total = item1.amount + item2.amount + item3.amount
        m001.unit_price = round(total, 2)

    # ── 模板用合板 (M003)：分析細項 ──
    if "M003" in res_map:
        m003 = res_map["M003"]
        m003.is_analysis = True
        m003.labor_rate = 50.0
        m003.material_rate = 40.0
        m003.equipment_rate = 5.0
        m003.misc_rate = 5.0
        # 工
        item1 = ResourceBreakdownItem(resource_id=m003.id, code="L001", c_name="模板工",
            c_unit="工", quantity=0.05, unit_price=3500, amount=round(0.05*3500, 2))
        db.add(item1)
        # 料
        item2 = ResourceBreakdownItem(resource_id=m003.id, code="M003", c_name="模板用合板",
            c_unit="片", quantity=1.0, unit_price=380, amount=380)
        db.add(item2)
        # 加總
        total = item1.amount + item2.amount
        m003.unit_price = round(total, 2)

    # ── 混凝土泵送車 (E002)：分析細項 ──
    if "E002" in res_map:
        e002 = res_map["E002"]
        e002.is_analysis = True
        e002.labor_rate = 25.0
        e002.material_rate = 15.0
        e002.equipment_rate = 55.0
        e002.misc_rate = 5.0
        # 機
        item1 = ResourceBreakdownItem(resource_id=e002.id, code="E002", c_name="混凝土泵送車",
            c_unit="天", quantity=1.0, unit_price=9000, amount=9000)
        db.add(item1)
        # 工
        item2 = ResourceBreakdownItem(resource_id=e002.id, code="L003", c_name="混凝土工",
            c_unit="工", quantity=0.5, unit_price=3200, amount=1600)
        db.add(item2)
        # 料（油料等）
        item3 = ResourceBreakdownItem(resource_id=e002.id, code="M999", c_name="燃油及耗材",
            c_unit="式", quantity=1.0, unit_price=1400, amount=1400)
        db.add(item3)
        # 加總
        total = item1.amount + item2.amount + item3.amount
        e002.unit_price = round(total, 2)

    # 7. 遞迴計算所有 B/Z 類型項目的金額（加總子項）
    _recalc_seed(db, project.id)

    db.commit()
    return True


def _recalc_seed(db: Session, project_id: int):
    """遞迴計算示範專案所有 B/Z 類型項目的金額（加總子項）"""
    from typing import Optional

    def _calc_amount(item: BudgetItem) -> float:
        return round((item.quantity or 0) * (item.unit_price or 0), item.decimal_amount)

    def _recalc(parent_id: Optional[int] = None) -> float:
        children = db.query(BudgetItem).filter(
            BudgetItem.project_id == project_id,
            BudgetItem.parent_id == parent_id
        ).all()
        total = 0.0
        for child in children:
            if child.kind in (BudgetItemKind.B, BudgetItemKind.Z):
                # B/Z 類型：遞迴加總子項金額
                child.amount = _recalc(child.id)
            else:
                # W/L/F/S/U 類型：依數量 × 單價計算
                child.amount = _calc_amount(child)
            db.flush()
            total += child.amount or 0
        return round(total, 2)

    _recalc(None)
    db.commit()
