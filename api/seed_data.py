"""PCCES 起始示範資料 — 資料庫為空時自動建立"""

from datetime import datetime, timezone
from sqlalchemy.orm import Session

from api.models import User, Project, BudgetItem, Resource, ResourceBreakdownItem, BudgetItemKind
from api.models import Contract, ContractItem, ContractIssue, ContractIssueItem
from api.models import ContractSettlement, ContractSettlementItem
from api.models import ContractFinalAcceptance, ContractFinalAcceptanceItem
from api.models import MrsBaseCategory, MrsBaseItem, MrsBaseBreakdownItem, MrsBaseBookmark
from api.models import SystemParameter, CodeTable, CodeItem, Organization, FeatureFlag

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
            # amount 由 _recalc_seed 統一計算，此處不預先設定
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
        # amount 由 _recalc_seed 統一計算
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

    # 7. 建立分包合約示範資料
    # 查詢第1層預算工項（直接工程費底下的 W 類型細項）
    all_w_items = db.query(BudgetItem).filter(
        BudgetItem.project_id == project.id,
        BudgetItem.kind == BudgetItemKind.W
    ).all()

    # 建立 2 份示範合約
    contract1 = Contract(
        project_id=project.id,
        contract_no="SC-DEMO001-001",
        c_name="結構體工程分包",
        contractor="XX 營造有限公司",
        contract_amount=0,  # 由工項加總
        status="active",
        start_date="2024-01-15",
        end_date="2024-08-30",
        remark="結構體工程分包合約",
    )
    db.add(contract1)
    db.flush()

    contract2 = Contract(
        project_id=project.id,
        contract_no="SC-DEMO001-002",
        c_name="裝修工程分包",
        contractor="YY 工程有限公司",
        contract_amount=0,
        status="draft",
        start_date="2024-03-01",
        end_date="2024-10-15",
        remark="裝修工程分包合約",
    )
    db.add(contract2)
    db.flush()

    # 為合約1 加入部分工項
    struct_items = [i for i in all_w_items if i.c_name and ("鋼筋" in i.c_name or "混凝土" in i.c_name or "模板" in i.c_name)]
    for bi in struct_items:
        ci = ContractItem(
            contract_id=contract1.id,
            budget_item_id=bi.id,
            item_no=bi.item_no,
            print_no=bi.print_no,
            c_name=bi.c_name,
            c_unit=bi.c_unit,
            contract_qty=bi.quantity * 0.9,
            unit_price=bi.unit_price * 0.95,
            completed_qty=bi.quantity * 0.6,
        )
        ci.amount = round((ci.contract_qty or 0) * (ci.unit_price or 0), 2)
        ci.completed_amount = round((ci.completed_qty or 0) * (ci.unit_price or 0), 2)
        db.add(ci)
    contract1.contract_amount = round(
        sum((ci.contract_qty or 0) * (ci.unit_price or 0) for ci in db.query(ContractItem).filter(ContractItem.contract_id == contract1.id).all()), 2
    )
    db.flush()

    # 為合約2 加入部分工項
    finish_items = [i for i in all_w_items if i.c_name and ("隔間" in i.c_name or "天花板" in i.c_name or "地坪" in i.c_name)]
    for bi in finish_items:
        ci = ContractItem(
            contract_id=contract2.id,
            budget_item_id=bi.id,
            item_no=bi.item_no,
            print_no=bi.print_no,
            c_name=bi.c_name,
            c_unit=bi.c_unit,
            contract_qty=bi.quantity,
            unit_price=bi.unit_price,
        )
        ci.amount = round((ci.contract_qty or 0) * (ci.unit_price or 0), 2)
        db.add(ci)
    contract2.contract_amount = round(
        sum((ci.contract_qty or 0) * (ci.unit_price or 0) for ci in db.query(ContractItem).filter(ContractItem.contract_id == contract2.id).all()), 2
    )
    db.flush()

    # 為合約1 建立一期計價
    issue1 = ContractIssue(
        contract_id=contract1.id,
        issue_no=1,
        c_name="第1期計價 — 結構體工程",
        status="approved",
        issue_date="2024-04-30",
        created_by=demo_user.id,
    )
    db.add(issue1)
    db.flush()

    # 為期別 1 建立計價明細
    c1_items = db.query(ContractItem).filter(ContractItem.contract_id == contract1.id).all()
    for ci in c1_items:
        ii = ContractIssueItem(
            issue_id=issue1.id,
            contract_item_id=ci.id,
            c_name=ci.c_name,
            c_unit=ci.c_unit,
            contract_qty=ci.contract_qty,
            unit_price=ci.unit_price,
            prev_completed_qty=0,
            this_completed_qty=ci.contract_qty * 0.5,
        )
        ii.total_completed_qty = (ii.prev_completed_qty or 0) + (ii.this_completed_qty or 0)
        ii.this_amount = round((ii.this_completed_qty or 0) * (ii.unit_price or 0), 2)
        ii.cumulative_amount = round((ii.total_completed_qty or 0) * (ii.unit_price or 0), 2)
        ii.remain_qty = max(0, (ii.contract_qty or 0) - ii.total_completed_qty)
        if ii.contract_qty and ii.contract_qty > 0:
            ii.progress_rate = round((ii.total_completed_qty / ii.contract_qty) * 100, 2)
        db.add(ii)
    # 更新期別主檔金額
    issue_items = db.query(ContractIssueItem).filter(ContractIssueItem.issue_id == issue1.id).all()
    issue1.total_amount = round(sum(i.this_amount or 0 for i in issue_items), 2)
    issue1.cumulative_amount = round(sum(i.cumulative_amount or 0 for i in issue_items), 2)
    total_contract_amt = sum((i.contract_qty or 0) * (i.unit_price or 0) for i in issue_items)
    if total_contract_amt > 0:
        issue1.progress_rate = round((issue1.cumulative_amount / total_contract_amt) * 100, 2)
    # 更新合約累計期別金額
    contract1.total_issue_amount = issue1.total_amount
    db.flush()

    # 為合約1 建立結算單
    settlement1 = ContractSettlement(
        contract_id=contract1.id,
        settlement_no="ST-DEMO001-001",
        c_name="結構體工程結算",
        settlement_date="2024-09-15",
        contract_amount=contract1.contract_amount or 0,
        status="draft",
        created_by=demo_user.id,
    )
    db.add(settlement1)
    db.flush()

    # 為合約1 建立終驗單
    acceptance1 = ContractFinalAcceptance(
        contract_id=contract1.id,
        acceptance_no="FA-DEMO001-001",
        c_name="結構體工程終驗",
        acceptance_date="2024-09-30",
        inspector="王檢驗",
        result="conditional_pass",
        defect_description="部分鋼筋保護層不足，須補強",
        status="draft",
        created_by=demo_user.id,
    )
    db.add(acceptance1)
    db.flush()

    # 為終驗單加入明細
    for ci in c1_items:
        ai = ContractFinalAcceptanceItem(
            acceptance_id=acceptance1.id,
            budget_item_id=ci.budget_item_id,
            c_name=ci.c_name,
            c_unit=ci.c_unit,
            contract_qty=ci.contract_qty or 0,
            actual_qty=ci.completed_qty or 0,
            accepted_qty=(ci.completed_qty or 0) * 0.95,
            rejected_qty=(ci.completed_qty or 0) * 0.05,
        )
        db.add(ai)
    db.flush()

    # 8. MrsBase 公共單價庫示範資料
    # 檢查是否已有 MrsBase 資料
    if db.query(MrsBaseItem).count() == 0:
        # 8a. 建立分類
        cat_s混凝土 = MrsBaseCategory(code="CONC", c_name="混凝土工程", sort_order=1)
        db.add(cat_s混凝土)
        db.flush()
        cat_s鋼筋 = MrsBaseCategory(code="REBAR", c_name="鋼筋工程", sort_order=2)
        db.add(cat_s鋼筋)
        db.flush()
        cat_s模板 = MrsBaseCategory(code="FORM", c_name="模板工程", sort_order=3)
        db.add(cat_s模板)
        db.flush()
        cat_s裝修 = MrsBaseCategory(code="FINISH", c_name="裝修工程", sort_order=4)
        db.add(cat_s裝修)
        db.flush()
        cat_s機電 = MrsBaseCategory(code="MEP", c_name="機電工程", sort_order=5)
        db.add(cat_s機電)
        db.flush()

        # 8b. 建立項目
        mrs_items = [
            # (code, c_name, c_unit, unit_price, cost_kind, category, is_analysis)
            ("CONC-001", "210kgf/cm² 預拌混凝土", "m³", 1850, "料", cat_s混凝土.id, False),
            ("CONC-002", "280kgf/cm² 預拌混凝土", "m³", 2100, "料", cat_s混凝土.id, False),
            ("CONC-003", "350kgf/cm² 預拌混凝土", "m³", 2450, "料", cat_s混凝土.id, False),
            ("CONC-004", "混凝土澆置", "m³", 350, "工", cat_s混凝土.id, False),
            ("CONC-005", "混凝土養護", "㎡", 45, "工", cat_s混凝土.id, False),
            ("REBAR-001", "SD280 鋼筋（加工及紮紮）", "噸", 26500, "料", cat_s鋼筋.id, True),
            ("REBAR-002", "SD420 鋼筋（加工及紮紮）", "噸", 28500, "料", cat_s鋼筋.id, True),
            ("REBAR-003", "鋼筋彎紮加工", "噸", 4500, "工", cat_s鋼筋.id, False),
            ("FORM-001", "一般模板（組立及拆除）", "㎡", 680, "料", cat_s模板.id, True),
            ("FORM-002", "清水模板（組立及拆除）", "㎡", 950, "料", cat_s模板.id, False),
            ("FINISH-001", "水泥砂漿粉刷", "㎡", 320, "料", cat_s裝修.id, False),
            ("FINISH-002", "磁磚舖貼", "㎡", 850, "料", cat_s裝修.id, False),
            ("FINISH-003", "天花板輕鋼架", "㎡", 620, "料", cat_s裝修.id, False),
            ("MEP-001", "PVC 電線管 1\"", "m", 85, "料", cat_s機電.id, False),
            ("MEP-002", "電力纜線 14mm²", "m", 165, "料", cat_s機電.id, False),
            ("MEP-003", "給水 PVC 管 1\"", "m", 120, "料", cat_s機電.id, False),
        ]
        created_items = {}
        for code, c_name, c_unit, unit_price, cost_kind, cat_id, is_analysis in mrs_items:
            item = MrsBaseItem(
                category_id=cat_id,
                code=code,
                c_name=c_name,
                c_unit=c_unit,
                unit_price=unit_price,
                cost_kind=cost_kind,
                is_analysis=is_analysis,
                created_by=demo_user.id,
            )
            db.add(item)
            db.flush()
            created_items[code] = item

        # 8c. 為啟用分析的項目建立工料機組成
        # REBAR-001: SD280 鋼筋
        if "REBAR-001" in created_items:
            ri = created_items["REBAR-001"]
            bd1 = MrsBaseBreakdownItem(
                item_id=ri.id, code="L001", c_name="鋼筋工", c_unit="工",
                quantity=0.012, unit_price=3800, amount=round(0.012*3800, 2),
                category="labor")
            db.add(bd1)
            bd2 = MrsBaseBreakdownItem(
                item_id=ri.id, code="REBAR-SD280", c_name="SD280 鋼筋", c_unit="噸",
                quantity=1.0, unit_price=24500, amount=24500,
                category="material")
            db.add(bd2)
            bd3 = MrsBaseBreakdownItem(
                item_id=ri.id, code="E003", c_name="吊車", c_unit="天",
                quantity=0.003, unit_price=18000, amount=round(0.003*18000, 2),
                category="equipment")
            db.add(bd3)
            total = (bd1.amount or 0) + (bd2.amount or 0) + (bd3.amount or 0)
            ri.unit_price = round(total, 2)

        # REBAR-002: SD420 鋼筋
        if "REBAR-002" in created_items:
            ri = created_items["REBAR-002"]
            bd1 = MrsBaseBreakdownItem(
                item_id=ri.id, code="L001", c_name="鋼筋工", c_unit="工",
                quantity=0.015, unit_price=3800, amount=round(0.015*3800, 2),
                category="labor")
            db.add(bd1)
            bd2 = MrsBaseBreakdownItem(
                item_id=ri.id, code="REBAR-SD420", c_name="SD420 鋼筋", c_unit="噸",
                quantity=1.0, unit_price=26000, amount=26000,
                category="material")
            db.add(bd2)
            bd3 = MrsBaseBreakdownItem(
                item_id=ri.id, code="E003", c_name="吊車", c_unit="天",
                quantity=0.004, unit_price=18000, amount=round(0.004*18000, 2),
                category="equipment")
            db.add(bd3)
            total = (bd1.amount or 0) + (bd2.amount or 0) + (bd3.amount or 0)
            ri.unit_price = round(total, 2)

        # FORM-001: 一般模板
        if "FORM-001" in created_items:
            ri = created_items["FORM-001"]
            bd1 = MrsBaseBreakdownItem(
                item_id=ri.id, code="L002", c_name="模板工", c_unit="工",
                quantity=0.05, unit_price=3500, amount=round(0.05*3500, 2),
                category="labor")
            db.add(bd1)
            bd2 = MrsBaseBreakdownItem(
                item_id=ri.id, code="PLYWOOD", c_name="模板用合板", c_unit="片",
                quantity=0.3, unit_price=420, amount=round(0.3*420, 2),
                category="material")
            db.add(bd2)
            bd3 = MrsBaseBreakdownItem(
                item_id=ri.id, code="LUMBER", c_name="角材", c_unit="式",
                quantity=1.0, unit_price=180, amount=180,
                category="material")
            db.add(bd3)
            total = (bd1.amount or 0) + (bd2.amount or 0) + (bd3.amount or 0)
            ri.unit_price = round(total, 2)

        # 8d. 為 demo 使用者建立書籤
        bm_items = [created_items.get("CONC-002"), created_items.get("REBAR-001"), created_items.get("FORM-001")]
        for bm_item in bm_items:
            if bm_item:
                bm = MrsBaseBookmark(user_id=demo_user.id, item_id=bm_item.id)
                db.add(bm)

        db.flush()

    # 9. 系統維護種子資料（系統參數、代碼表、組織機構）
    seed_sysmaintain_data(db)

    # 9b. 功能開關種子資料
    seed_feature_flags(db)

    # 10. 遞迴計算所有 B/Z 類型項目的金額（加總子項）
    _recalc_seed(db, project.id)

    db.commit()
    return True


def seed_sysmaintain_data(db: Session):
    """寫入系統維護起始示範資料（若資料表為空）"""
    # ─── 1. 系統參數 ───
    if db.query(SystemParameter).count() == 0:
        params = [
            # 分類 E：機關基本資料
            ("E", "ORG_NAME", "機關名稱", "工程會", "工程會"),
            ("E", "ORG_CODE", "機關代碼", "12345678", "12345678"),
            ("E", "SYS_TITLE", "系統標題", "PCCES 公共工程經費估算系統", "PCCES 公共工程經費估算系統"),
            ("E", "CONTACT", "聯絡資訊", "工程會聯絡處", "工程會聯絡處"),
            # 分類 F：費率參數
            ("F", "PROFIT_RATE", "包商利潤率 (%)", "5", "5"),
            ("F", "TAX_RATE", "營業稅率 (%)", "5", "5"),
            ("F", "OVERHEAD_RATE", "間接費用率 (%)", "8", "8"),
            ("F", "INSURANCE_RATE", "保險費率 (%)", "0.5", "0.5"),
            # 分類 G：系統設定
            ("G", "PROJECT_TYPE", "預設工程分類", "建築工程", "建築工程"),
            ("G", "BUDGET_YEAR", "預算年度", "2025", "2025"),
            ("G", "CURRENCY", "幣別", "TWD", "TWD"),
            ("G", "DECIMAL_QTY", "數量小數位數", "2", "2"),
            ("G", "DECIMAL_PRICE", "單價小數位數", "2", "2"),
        ]
        for idx, (cat, code, name, value, default) in enumerate(params):
            db.add(SystemParameter(
                category=cat, code=code, c_name=name,
                c_value=value, c_default=default,
                sort_order=idx + 1, is_active=True,
            ))

    # ─── 2. 代碼表 ───
    if db.query(CodeTable).count() == 0:
        dept = CodeTable(table_code="DEPT", table_name="部門編碼", is_active=True)
        db.add(dept)
        db.flush()
        asset = CodeTable(table_code="ASSET", table_name="公物編碼", is_active=True)
        db.add(asset)
        db.flush()
        catg = CodeTable(table_code="CATG", table_name="工程分類", is_active=True)
        db.add(catg)
        db.flush()

        # 部門編碼子項
        dept_items = [
            ("GEN", "工務課", 1),
            ("MEC", "機電課", 2),
            ("ARC", "建築課", 3),
            ("ADM", "行政課", 4),
        ]
        for code, name, sort in dept_items:
            db.add(CodeItem(table_id=dept.id, code=code, c_name=name, sort_order=sort, is_active=True))

        # 公物編碼子項
        asset_items = [
            ("PC", "個人電腦", 1),
            ("PRT", "印表機", 2),
            ("FURN", "辦公家具", 3),
            ("VEH", "公務車輛", 4),
        ]
        for code, name, sort in asset_items:
            db.add(CodeItem(table_id=asset.id, code=code, c_name=name, sort_order=sort, is_active=True))

        # 工程分類子項（含父項）
        building = CodeItem(table_id=catg.id, code="BLOG", c_name="建築工程", sort_order=1, is_active=True)
        db.add(building)
        db.flush()
        civil = CodeItem(table_id=catg.id, code="CIVIL", c_name="土木工程", sort_order=2, is_active=True)
        db.add(civil)
        db.flush()
        mep = CodeItem(table_id=catg.id, code="MEP", c_name="機電工程", sort_order=3, is_active=True)
        db.add(mep)
        db.flush()

        # 建築工程子項
        for code, name, sort in [("RES", "住宅", 1), ("COM", "商辦", 2), ("FAC", "廠房", 3)]:
            db.add(CodeItem(table_id=catg.id, parent_id=building.id, code=code, c_name=name, sort_order=sort, is_active=True))
        # 土木工程子項
        for code, name, sort in [("ROAD", "道路", 1), ("BRG", "橋樑", 2), ("TUN", "隧道", 3)]:
            db.add(CodeItem(table_id=catg.id, parent_id=civil.id, code=code, c_name=name, sort_order=sort, is_active=True))
        # 機電工程子項
        for code, name, sort in [("ELEC", "電力", 1), ("HVAC", "空調", 2), ("PLUM", "給排水", 3)]:
            db.add(CodeItem(table_id=catg.id, parent_id=mep.id, code=code, c_name=name, sort_order=sort, is_active=True))

    # ─── 3. 組織機構 ───
    if db.query(Organization).count() == 0:
        root = Organization(
            code="PCCES_ROOT",
            c_name="工程會（示範）",
            org_type="機關",
            sort_order=1,
            is_active=True,
        )
        db.add(root)
        db.flush()

        dept1 = Organization(
            parent_id=root.id,
            code="DEPT_A",
            c_name="工務組",
            org_type="部門",
            sort_order=1,
            is_active=True,
            contact_person="張組長",
            contact_phone="02-1234-5678",
        )
        db.add(dept1)
        db.flush()

        dept2 = Organization(
            parent_id=root.id,
            code="DEPT_B",
            c_name="秘書室",
            org_type="部門",
            sort_order=2,
            is_active=True,
        )
        db.add(dept2)
        db.flush()

        # 工務組下屬課室
        for code, name, sort in [("SEC_RD", "道路課", 1), ("SEC_BL", "建築課", 2), ("SEC_MEP", "機電課", 3)]:
            db.add(Organization(
                parent_id=dept1.id,
                code=code, c_name=name,
                org_type="課室", sort_order=sort, is_active=True,
            ))
        # 秘書室下屬課室
        for code, name, sort in [("SEC_DOC", "文書課", 1), ("SEC_ACC", "會計課", 2)]:
            db.add(Organization(
                parent_id=dept2.id,
                code=code, c_name=name,
                org_type="課室", sort_order=sort, is_active=True,
            ))

    db.flush()


# ═══ 功能開關種子資料 ═══

SEED_FEATURE_FLAGS = [
    # (flag_key, display_name, description, category, is_enabled, is_system, sort_order)
    ("project_management", "專案管理", "專案基本資料管理", "general", True, True, 1),
    ("budget_editor", "預算編輯", "預算項目編輯與樹狀結構管理", "budget", True, True, 2),
    ("resource_management", "資源管理", "工料機資源管理與單價分析", "budget", True, False, 3),
    ("mrs_base", "公共單價庫", "公共工程單價資料庫查詢與管理", "mrs", True, False, 4),
    ("invoice_management", "計價管理", "工程計價單管理", "invoice", True, False, 5),
    ("contract_management", "分包合約", "分包合約管理", "contract", True, False, 6),
    ("settlement_management", "分包結算", "分包合約結算管理", "contract", True, False, 7),
    ("acceptance_management", "分包終驗", "分包合約終驗管理", "contract", True, False, 8),
    ("budget_compare", "工項比較", "跨專案預算項目比對", "compare", True, False, 9),
    ("mrs_price_compare", "單價比較", "公共單價庫價格比較", "compare", True, False, 10),
    ("report_analysis", "報表分析", "報表匯出與分析", "report", True, False, 11),
    ("system_maintenance", "系統維護", "系統維護中心（使用者/參數/代碼/組織）", "admin", True, True, 12),
]


def seed_feature_flags(db: Session):
    """寫入預設功能開關種子資料（若資料表為空）"""
    if db.query(FeatureFlag).count() > 0:
        return
    for ff in SEED_FEATURE_FLAGS:
        flag = FeatureFlag(
            flag_key=ff[0],
            display_name=ff[1],
            description=ff[2],
            category=ff[3],
            is_enabled=ff[4],
            is_system=ff[5],
            sort_order=ff[6],
        )
        db.add(flag)
    db.flush()


def _recalc_budget_tree(db: Session, project_id: int, parent_id=None) -> float:
    """遞迴計算 B/Z 類型金額（加總子項），W 類型依 qty × price 計算

    回傳此節點下所有子項金額總和。
    """
    children = db.query(BudgetItem).filter(
        BudgetItem.project_id == project_id,
        BudgetItem.parent_id == parent_id
    ).all()
    total = 0.0
    for child in children:
        if child.kind in (BudgetItemKind.B, BudgetItemKind.Z):
            # B/Z 類型：遞迴加總子項金額
            child.amount = _recalc_budget_tree(db, project_id, child.id)
        else:
            # W/L/F/S/U 類型：依數量 × 單價計算
            child.amount = round(
                (child.quantity or 0) * (child.unit_price or 0),
                child.decimal_amount
            )
        db.flush()
        total += child.amount or 0
    return round(total, 2)


def _apply_profit_rules(db: Session, project_id: int):
    """計算「利潤及營業稅」下包商利潤(5%) 與營業稅(5%) 的百分比金額

    包商利潤 = (直接工程費 + 間接工程費) × 5%
    營業稅   = (直接工程費 + 間接工程費 + 包商利潤) × 5%
    """
    # 查詢利潤父項目
    profit_parent = db.query(BudgetItem).filter(
        BudgetItem.project_id == project_id,
        BudgetItem.c_name == "利潤及營業稅",
        BudgetItem.kind == BudgetItemKind.Z,
    ).first()
    if not profit_parent:
        return

    # 取得工程費總額
    direct_cost = db.query(BudgetItem).filter(
        BudgetItem.project_id == project_id,
        BudgetItem.c_name == "直接工程費",
        BudgetItem.kind == BudgetItemKind.B,
    ).first()
    indirect_cost = db.query(BudgetItem).filter(
        BudgetItem.project_id == project_id,
        BudgetItem.c_name == "間接工程費",
        BudgetItem.kind == BudgetItemKind.B,
    ).first()

    direct_total = direct_cost.amount if direct_cost else 0
    indirect_total = indirect_cost.amount if indirect_cost else 0
    base = direct_total + indirect_total  # 工程費合計

    # 查詢子項
    profit_item = db.query(BudgetItem).filter(
        BudgetItem.project_id == project_id,
        BudgetItem.parent_id == profit_parent.id,
        BudgetItem.c_name.like("%包商利潤%"),
    ).first()
    tax_item = db.query(BudgetItem).filter(
        BudgetItem.project_id == project_id,
        BudgetItem.parent_id == profit_parent.id,
        BudgetItem.c_name.like("%營業稅%"),
    ).first()

    if profit_item:
        # 包商利潤 = 工程費合計 × 5%
        profit_item.amount = round(base * 0.05, 2)
        db.flush()
    profit_amt = profit_item.amount if profit_item else 0

    if tax_item:
        # 營業稅 = (工程費合計 + 包商利潤) × 5%
        tax_item.amount = round((base + profit_amt) * 0.05, 2)
        db.flush()

    # 更新利潤父項總額
    total_profit = (
        (profit_item.amount if profit_item else 0) +
        (tax_item.amount if tax_item else 0)
    )
    profit_parent.amount = round(total_profit, 2)
    db.flush()


def _recalc_seed(db: Session, project_id: int):
    """遞迴計算示範專案所有 B/Z 類型項目的金額（加總子項），
    並套用利潤類項目的百分比計算規則。
    """
    # 第一輪：標準遞迴計算
    _recalc_budget_tree(db, project_id)
    db.commit()

    # 第二輪：特殊處理利潤類項目
    _apply_profit_rules(db, project_id)
    db.commit()
