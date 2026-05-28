using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.STDClass;
using C1.Win.C1FlexGrid;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormBudgetSplit : Form
{
	private IContainer components;

	private string F_UserID;

	private string F_SPLT_STATUS = "INI";

	private DataTable DT_bud = new DataTable();

	private int GridCols = 15;

	private object[,] GridColsSquence;

	private int F_MainQty = 0;

	private int F_MainCst = 0;

	private int F_MainAmt = 0;

	private int F_AnaQty = 0;

	private int F_AnaCst = 0;

	private int F_AnaAmt = 0;

	private string F_ProjectCode = "";

	private string F_ProjectNameC = "";

	private string F_MainProjectCode = "";

	private Panel panel17;

	private UltraLabel lblTitle;

	private Panel panel18;

	private GridBudget c1FlexGrid2;

	private Panel panel16;

	private GroupBox groupBox6;

	private UltraButton D_Btn_Cncl;

	private UltraButton D_Btn_Next;

	public string _UserID
	{
		get
		{
			return F_UserID;
		}
		set
		{
			F_UserID = value;
		}
	}

	public string _ProjectCode
	{
		get
		{
			return F_ProjectCode;
		}
		set
		{
			F_ProjectCode = value;
		}
	}

	public string _ProjectNameC
	{
		get
		{
			return F_ProjectNameC;
		}
		set
		{
			F_ProjectNameC = value;
		}
	}

	public string _MainProjectCode
	{
		get
		{
			return F_MainProjectCode;
		}
		set
		{
			F_MainProjectCode = value;
		}
	}

	public FormBudgetSplit()
	{
		InitializeComponent();
		string sHideCols = CommonMethods.GetDebugValue("formNewProjectWizard", "HideCols");
		HideCols(Convert.ToBoolean((sHideCols == "") ? "True" : sHideCols));
		GridCols = c1FlexGrid2.Cols.Count;
		GridColsSquence = new object[GridCols, 10];
	}

	private void FormBudgetSplit_Load(object sender, EventArgs e)
	{
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Clear();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("讀取主專案名稱");
		Archnowledge.Pcces.BUDClass.Project ProjCom = new Archnowledge.Pcces.BUDClass.Project(tmp_AL1);
		ProjCom.ps_srckind = "bud";
		string sMainProjName = ProjCom.GetProjdes(F_MainProjectCode);
		lblTitle.Text = "主專案:【" + F_MainProjectCode + "】" + sMainProjName;
		SettingDecimal();
		RememberColsProps();
		LoadMainProjectData();
	}

	private void LoadMainProjectData()
	{
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		if (F_MainProjectCode.Length > 0)
		{
			tmp_AL1.Add("(Get_MainProjItems) 分標選自主專案的預算書");
		}
		else
		{
			tmp_AL1.Add("(Get_MainProjItems) 併標選自子專案的預算書");
		}
		if (F_MainProjectCode.Length > 0)
		{
			ItemA ItemACom = new ItemA(tmp_AL1);
			ItemACom.ps_srckind = "bud";
			DT_bud = ItemACom.SeleItem1("", F_ProjectCode, F_MainProjectCode);
			BindToGrid();
		}
	}

	private void HideCols(bool IsHide)
	{
		if (IsHide)
		{
			c1FlexGrid2.Cols["PrintNo"].Visible = false;
			c1FlexGrid2.Cols["CanCheck"].Visible = false;
			c1FlexGrid2.Cols["SNo"].Visible = false;
			c1FlexGrid2.Cols["PccesCode"].Visible = false;
			c1FlexGrid2.Cols["PubCode"].Visible = false;
			c1FlexGrid2.Cols["Kind"].Visible = false;
		}
	}

	private void SettingDecimal()
	{
		DataTable DTDecimal = new DataTable();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add(CommonMethods.GetFormTypeTitle(FormType.Budget));
		PubDecimal dbDecimal = new PubDecimal(aArr);
		DTDecimal = dbDecimal.ListItem("", F_ProjectCode);
		if (DTDecimal.Rows.Count > 0)
		{
			F_MainQty = Convert.ToInt32(DTDecimal.Rows[0]["itemQty"]);
			F_MainCst = Convert.ToInt32(DTDecimal.Rows[0]["itemCost"]);
			F_MainAmt = Convert.ToInt32(DTDecimal.Rows[0]["itemAmt"]);
			F_AnaQty = Convert.ToInt32(DTDecimal.Rows[0]["analysisQty"]);
			F_AnaCst = Convert.ToInt32(DTDecimal.Rows[0]["analysisCost"]);
			F_AnaAmt = Convert.ToInt32(DTDecimal.Rows[0]["analysisAmt"]);
		}
		else
		{
			F_MainQty = 3;
			F_MainCst = 0;
			F_MainAmt = 0;
			F_AnaQty = 3;
			F_AnaCst = 2;
			F_AnaAmt = 2;
		}
	}

	private void RememberColsProps()
	{
		string Status = CommonMethods.GetIniValue("Split", "WindowState");
		if (Status == "Maximized")
		{
			base.WindowState = FormWindowState.Maximized;
		}
		int iLoc_X = PubTools.Str2Int(CommonMethods.GetIniValue("Split", "PK_LocationX"));
		int iLoc_Y = PubTools.Str2Int(CommonMethods.GetIniValue("Split", "PK_LocationY"));
		int iSiz_W = PubTools.Str2Int(CommonMethods.GetIniValue("Split", "PK_Width"));
		int iSiz_H = PubTools.Str2Int(CommonMethods.GetIniValue("Split", "PK_Height"));
		if (iLoc_X > 0 && iLoc_Y > 0)
		{
			base.Location = new Point(iLoc_X, iLoc_Y);
		}
		if (iSiz_W > 0)
		{
			base.Width = iSiz_W;
		}
		if (iSiz_H > 0)
		{
			base.Height = iSiz_H;
		}
		for (int i = 0; i < GridCols; i++)
		{
			GridColsSquence[i, 0] = c1FlexGrid2.Cols[i].Name;
			GridColsSquence[i, 1] = c1FlexGrid2.Cols[i].Caption;
			GridColsSquence[i, 2] = c1FlexGrid2.Cols[i].Width;
			if (c1FlexGrid2.Cols[i].Name == "AnaImg")
			{
				GridColsSquence[i, 3] = typeof(Image);
			}
			else
			{
				GridColsSquence[i, 3] = c1FlexGrid2.Cols[i].DataType;
			}
			GridColsSquence[i, 4] = c1FlexGrid2.Cols[i].Visible;
			GridColsSquence[i, 5] = c1FlexGrid2.Cols[i].Format;
			GridColsSquence[i, 6] = c1FlexGrid2.Cols[i].AllowEditing;
			if (c1FlexGrid2.Cols[i].Name == "qty")
			{
				if (F_MainQty > 0)
				{
					GridColsSquence[i, 5] = "###,###,###,##0." + "0".PadLeft(F_MainQty, '0');
				}
				else
				{
					GridColsSquence[i, 5] = "###,###,###,##0";
				}
			}
			if (c1FlexGrid2.Cols[i].Name == "cost")
			{
				if (F_MainCst > 0)
				{
					GridColsSquence[i, 5] = "###,###,###,##0." + "0".PadLeft(F_MainCst, '0');
				}
				else
				{
					GridColsSquence[i, 5] = "###,###,###,##0";
				}
			}
			if (c1FlexGrid2.Cols[i].Name == "RemainQty")
			{
				if (F_MainQty > 0)
				{
					GridColsSquence[i, 5] = "###,###,###,##0." + "0".PadLeft(F_MainQty, '0');
				}
				else
				{
					GridColsSquence[i, 5] = "###,###,###,##0";
				}
			}
			if (c1FlexGrid2.Cols[i].Name == "RemainCost")
			{
				if (F_MainCst > 0)
				{
					GridColsSquence[i, 5] = "###,###,###,##0." + "0".PadLeft(F_MainCst, '0');
				}
				else
				{
					GridColsSquence[i, 5] = "###,###,###,##0";
				}
			}
			if (c1FlexGrid2.Cols[i].Name == "SplQty")
			{
				if (F_MainQty > 0)
				{
					GridColsSquence[i, 5] = "###,###,###,##0." + "0".PadLeft(F_MainQty, '0');
				}
				else
				{
					GridColsSquence[i, 5] = "###,###,###,##0";
				}
			}
			if (c1FlexGrid2.Cols[i].Name == "SplCost")
			{
				if (F_MainCst > 0)
				{
					GridColsSquence[i, 5] = "###,###,###,##0." + "0".PadLeft(F_MainCst, '0');
				}
				else
				{
					GridColsSquence[i, 5] = "###,###,###,##0";
				}
			}
			GridColsSquence[i, 7] = c1FlexGrid2.Cols[i].TextAlign;
			GridColsSquence[i, 8] = c1FlexGrid2.Cols[i].AllowDragging;
			GridColsSquence[i, 9] = c1FlexGrid2.Cols[i].AllowResizing;
		}
	}

	private void SetGridColumn()
	{
		for (int i = 0; i < GridCols; i++)
		{
			c1FlexGrid2.Cols[i].Name = (string)GridColsSquence[i, 0];
			c1FlexGrid2.Cols[i].Caption = (string)GridColsSquence[i, 1];
			c1FlexGrid2.Cols[i].Width = (int)GridColsSquence[i, 2];
			c1FlexGrid2.Cols[i].DataType = (Type)GridColsSquence[i, 3];
			c1FlexGrid2.Cols[i].Visible = (bool)GridColsSquence[i, 4];
			c1FlexGrid2.Cols[i].Format = (string)GridColsSquence[i, 5];
			c1FlexGrid2.Cols[i].AllowEditing = (bool)GridColsSquence[i, 6];
			c1FlexGrid2.Cols[i].TextAlign = (TextAlignEnum)GridColsSquence[i, 7];
			c1FlexGrid2.Cols[i].AllowDragging = (bool)GridColsSquence[i, 8];
			c1FlexGrid2.Cols[i].AllowResizing = (bool)GridColsSquence[i, 9];
		}
	}

	private void BindToGrid()
	{
		F_SPLT_STATUS = "EDT";
		c1FlexGrid2.Visible = false;
		RememberColsProps();
		CellStyle CS1 = c1FlexGrid2.Styles.Add("AnalysisColor");
		CellStyle CS9 = c1FlexGrid2.Styles.Add("IsSharedColor");
		CS1.ForeColor = Color.Red;
		CS9.ForeColor = Color.Plum;
		CellStyle CS_EDT = c1FlexGrid2.Styles.Add("EDT");
		CS_EDT.BackColor = Color.Orange;
		CellStyle CS_NOTEDT = c1FlexGrid2.Styles.Add("NOTEDT");
		CS_NOTEDT.BackColor = Color.LightGray;
		c1FlexGrid2.Clear(ClearFlags.All);
		c1FlexGrid2.Rows.Count = DT_bud.Rows.Count + 1;
		SetGridColumn();
		c1FlexGrid2.Cols["Unitname"].Style = c1FlexGrid2.Styles["NOTEDT"];
		c1FlexGrid2.Cols["qty"].Style = c1FlexGrid2.Styles["NOTEDT"];
		c1FlexGrid2.Cols["cost"].Style = c1FlexGrid2.Styles["NOTEDT"];
		c1FlexGrid2.Cols["RemainQty"].Style = c1FlexGrid2.Styles["NOTEDT"];
		c1FlexGrid2.Cols["RemainCost"].Style = c1FlexGrid2.Styles["NOTEDT"];
		for (int i = 0; i < DT_bud.Rows.Count; i++)
		{
			c1FlexGrid2[i + 1, "IsCheck"] = DT_bud.Rows[i]["chk"].ToString().Trim() == "1";
			c1FlexGrid2[i + 1, "ItemNo"] = DT_bud.Rows[i]["ItemNo"].ToString().Trim();
			c1FlexGrid2[i + 1, "CName"] = DT_bud.Rows[i]["CName"].ToString().Trim();
			c1FlexGrid2[i + 1, "Unitname"] = DT_bud.Rows[i]["unitName"].ToString().Trim();
			c1FlexGrid2[i + 1, "qty"] = DT_bud.Rows[i]["qty"].ToString().Trim();
			c1FlexGrid2[i + 1, "cost"] = DT_bud.Rows[i]["cost"].ToString().Trim();
			c1FlexGrid2[i + 1, "RemainQty"] = DT_bud.Rows[i]["RemainQty"].ToString().Trim();
			c1FlexGrid2[i + 1, "RemainCost"] = DT_bud.Rows[i]["RemainCost"].ToString().Trim();
			c1FlexGrid2[i + 1, "SplQty"] = DT_bud.Rows[i]["ThisQty"].ToString().Trim();
			c1FlexGrid2[i + 1, "SplCost"] = DT_bud.Rows[i]["ThisCost"].ToString().Trim();
			c1FlexGrid2[i + 1, "PrintNo"] = DT_bud.Rows[i]["PrintNo"].ToString().Trim();
			c1FlexGrid2[i + 1, "CanCheck"] = true;
			c1FlexGrid2[i + 1, "SNo"] = DT_bud.Rows[i]["SNo"].ToString().Trim();
			c1FlexGrid2[i + 1, "PccesCode"] = DT_bud.Rows[i]["PccesCode"].ToString().Trim();
			c1FlexGrid2[i + 1, "PubCode"] = DT_bud.Rows[i]["PubCode"].ToString().Trim();
			c1FlexGrid2[i + 1, "Kind"] = DT_bud.Rows[i]["Kind"].ToString().Trim();
			if ((DT_bud.Rows[i]["qty"].ToString().Trim() == "1" && DT_bud.Rows[i]["unitName"].ToString().Trim() == "式") || DT_bud.Rows[i]["kind"].ToString().Trim() == "B")
			{
				CellRange Rg = c1FlexGrid2.GetCellRange(i + 1, c1FlexGrid2.Cols["SplQty"].SafeIndex);
				Rg.Style = c1FlexGrid2.Styles["NOTEDT"];
			}
			else
			{
				CellRange Rg = c1FlexGrid2.GetCellRange(i + 1, c1FlexGrid2.Cols["SplCost"].SafeIndex);
				Rg.Style = c1FlexGrid2.Styles["NOTEDT"];
			}
			c1FlexGrid2.Rows[i + 1].IsNode = true;
			if (DT_bud.Rows[i]["PrintNo"].ToString().Trim() == "".PadLeft(32, '9'))
			{
				c1FlexGrid2.Rows[i + 1].Node.Level = 1;
			}
			else
			{
				c1FlexGrid2.Rows[i + 1].Node.Level = Convert.ToInt32(DT_bud.Rows[i]["PrintNo"].ToString().Trim().Length / 4);
			}
		}
		c1FlexGrid2.Cols.Frozen = 3;
		F_SPLT_STATUS = "NOR";
		c1FlexGrid2.Visible = true;
	}

	private void c1FlexGrid2_AfterSelChange(object sender, RangeEventArgs e)
	{
		if (F_SPLT_STATUS != "NOR")
		{
			return;
		}
		int rowIndex = c1FlexGrid2.MouseRow;
		int colIndex = c1FlexGrid2.MouseCol;
		if ((c1FlexGrid2[rowIndex, "qty"].ToString().Trim() == "1" && c1FlexGrid2[rowIndex, "unitName"].ToString().Trim() == "式") || c1FlexGrid2[rowIndex, "Kind"].ToString().Trim() == "B")
		{
			if (c1FlexGrid2.Cols[colIndex].Name != "SplCost")
			{
				c1FlexGrid2.Col = 0;
			}
		}
		else if (c1FlexGrid2.Cols[colIndex].Name != "SplQty")
		{
			c1FlexGrid2.Col = 0;
		}
	}

	private void D_Btn_Next_Click(object sender, EventArgs e)
	{
		Do_SaveCheckItem();
		base.DialogResult = DialogResult.OK;
		Close();
	}

	private void Do_SaveCheckItem()
	{
		for (int i = 1; i < c1FlexGrid2.Rows.Count; i++)
		{
			if ((bool)c1FlexGrid2[i, "IsCheck"])
			{
				DT_bud.Rows[i - 1]["chk"] = "1";
				DT_bud.Rows[i - 1]["ThisQty"] = PubTools.Str2Double(c1FlexGrid2[i, "SplQty"].ToString());
				DT_bud.Rows[i - 1]["ThisCost"] = PubTools.Str2Double(c1FlexGrid2[i, "SplCost"].ToString());
			}
			else
			{
				DT_bud.Rows[i - 1]["chk"] = "0";
			}
		}
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("儲存分標勾選");
		ItemA ItemACom = new ItemA(aArr);
		ItemACom.ps_srckind = "bud";
		ItemACom.CopyItemA(F_ProjectCode, DT_bud, F_MainProjectCode);
		ItemACom = null;
		PubTools.WriteRoughlyLog(aArr);
	}

	private void c1FlexGrid2_AfterEdit(object sender, RowColEventArgs e)
	{
		if (c1FlexGrid2.Cols[c1FlexGrid2.MouseCol].Name != "IsCheck")
		{
			c1FlexGrid2[e.Row, "IsCheck"] = true;
		}
		try
		{
			bool check = (bool)c1FlexGrid2[e.Row, "IsCheck"];
			Node LastNode = c1FlexGrid2.Rows[c1FlexGrid2.Row].Node;
			while (LastNode != null && LastNode.Children > 0)
			{
				LastNode = LastNode.GetNode(NodeTypeEnum.LastChild);
			}
			for (int i = c1FlexGrid2.Row; i <= LastNode.Row.SafeIndex; i++)
			{
				c1FlexGrid2[i, "IsCheck"] = check;
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudgetSplit.cs" + ex.Message);
		}
	}

	private void c1FlexGrid2_BeforeEdit(object sender, RowColEventArgs e)
	{
		if (F_SPLT_STATUS != "NOR" || c1FlexGrid2.Row <= 0 || c1FlexGrid2.MouseRow <= 0 || c1FlexGrid2.MouseCol <= 0)
		{
			return;
		}
		int rowIndex = c1FlexGrid2.MouseRow;
		int colIndex = c1FlexGrid2.MouseCol;
		if (c1FlexGrid2.Cols[colIndex].Name == "IsCheck")
		{
			return;
		}
		if ((c1FlexGrid2[rowIndex, "qty"].ToString().Trim() == "1" && c1FlexGrid2[rowIndex, "unitName"].ToString().Trim() == "式") || c1FlexGrid2[rowIndex, "Kind"].ToString().Trim() == "B")
		{
			if (c1FlexGrid2.Cols[colIndex].Name != "SplCost")
			{
				c1FlexGrid2.Col = 0;
				e.Cancel = true;
			}
		}
		else if (c1FlexGrid2.Cols[colIndex].Name != "SplQty")
		{
			c1FlexGrid2.Col = 0;
			e.Cancel = true;
		}
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormBudgetSplit));
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		this.panel17 = new System.Windows.Forms.Panel();
		this.lblTitle = new Infragistics.Win.Misc.UltraLabel();
		this.panel18 = new System.Windows.Forms.Panel();
		this.c1FlexGrid2 = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.panel16 = new System.Windows.Forms.Panel();
		this.groupBox6 = new System.Windows.Forms.GroupBox();
		this.D_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.D_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.panel17.SuspendLayout();
		this.panel18.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid2).BeginInit();
		this.panel16.SuspendLayout();
		base.SuspendLayout();
		this.panel17.Controls.Add(this.lblTitle);
		this.panel17.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel17.Location = new System.Drawing.Point(0, 0);
		this.panel17.Name = "panel17";
		this.panel17.Size = new System.Drawing.Size(772, 36);
		this.panel17.TabIndex = 17;
		appearance1.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance1.BackColor2 = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance1.FontData.Name = "新細明體";
		appearance1.FontData.SizeInPoints = 12f;
		appearance1.ForeColor = System.Drawing.Color.White;
		appearance1.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance1.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblTitle.Appearance = appearance1;
		this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
		this.lblTitle.Font = new System.Drawing.Font("細明體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblTitle.Location = new System.Drawing.Point(0, 0);
		this.lblTitle.Name = "lblTitle";
		this.lblTitle.Size = new System.Drawing.Size(772, 36);
		this.lblTitle.TabIndex = 1;
		this.lblTitle.Text = "主專案:";
		this.panel18.Controls.Add(this.c1FlexGrid2);
		this.panel18.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel18.Location = new System.Drawing.Point(0, 36);
		this.panel18.Name = "panel18";
		this.panel18.Size = new System.Drawing.Size(772, 469);
		this.panel18.TabIndex = 18;
		this.c1FlexGrid2._ExcelFileName = "";
		this.c1FlexGrid2._ExcelSheeName = "";
		this.c1FlexGrid2._IsOpenExcelAfterExport = false;
		this.c1FlexGrid2.BackColor = System.Drawing.Color.White;
		this.c1FlexGrid2.ColumnInfo = "16,0,0,0,0,110,Columns:0{Width:20;Name:\"IsCheck\";AllowDragging:False;AllowResizing:False;DataType:System.Boolean;TextAlignFixed:GeneralTop;ImageAlign:CenterCenter;}\t1{Width:85;Name:\"ItemNo\";Caption:\"項次\";AllowDragging:False;AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t2{Width:125;Name:\"CName\";Caption:\"選目及說明\";AllowDragging:False;AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t3{Width:55;Name:\"Unitname\";Caption:\"單位\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t4{Width:80;Name:\"qty\";Caption:\"預算數量\";DataType:System.Decimal;Format:\"###,###,###,###,###.00\";TextAlignFixed:GeneralTop;}\t5{Width:80;Name:\"cost\";Caption:\"預算單價\";DataType:System.Decimal;Format:\"###,###,###,###,###.00\";TextAlignFixed:GeneralTop;}\t6{Width:100;Name:\"SplQty\";Caption:\"分標數量\";DataType:System.Decimal;Format:\"###,###,###,###,###.00\";TextAlignFixed:GeneralTop;}\t7{Width:100;Name:\"SplCost\";Caption:\"分標金額\";DataType:System.Decimal;Format:\"###,###,###,###,###.00\";TextAlignFixed:GeneralTop;}\t8{Width:90;Name:\"RemainQty\";Caption:\"已分標數量\";DataType:System.Decimal;Format:\"###,###,###,###,###.00\";TextAlignFixed:GeneralTop;}\t9{Width:90;Name:\"RemainCost\";Caption:\"已分標金額\";DataType:System.Decimal;Format:\"###,###,###,###,###.00\";TextAlignFixed:GeneralTop;}\t10{Name:\"PrintNo\";Caption:\"PrintNo\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t11{Name:\"CanCheck\";Caption:\"CanCheck\";DataType:System.Boolean;TextAlignFixed:GeneralTop;ImageAlign:CenterCenter;}\t12{Name:\"SNo\";Caption:\"SNo\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t13{Name:\"PccesCode\";Caption:\"PccesCode\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t14{Name:\"PubCode\";Caption:\"PubCode\";DataType:System.Int32;TextAlign:RightCenter;TextAlignFixed:GeneralTop;}\t15{Name:\"Kind\";Caption:\"Kind\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t";
		this.c1FlexGrid2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.c1FlexGrid2.ExtendLastCol = true;
		this.c1FlexGrid2.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.c1FlexGrid2.ForeColor = System.Drawing.Color.Black;
		this.c1FlexGrid2.Location = new System.Drawing.Point(0, 0);
		this.c1FlexGrid2.Name = "c1FlexGrid2";
		this.c1FlexGrid2.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.c1FlexGrid2.ShowToolTipOnNarrowColumn = true;
		this.c1FlexGrid2.Size = new System.Drawing.Size(772, 469);
		this.c1FlexGrid2.Styles = new C1.Win.C1FlexGrid.CellStyleCollection("Normal{Font:細明體, 11pt;BackColor:White;ForeColor:Black;Border:Flat,1,Silver,Both;}\tFixed{BackColor:225, 247, 223;TextAlign:GeneralTop;Border:Raised,1,Black,Both;}\tHighlight{BackColor:102, 153, 255;ForeColor:White;}\tFocus{Font:細明體, 11.25pt;Border:None,1,Black,Both;}\tSearch{BackColor:Highlight;ForeColor:HighlightText;}\tFrozen{BackColor:Beige;}\tEmptyArea{BackColor:AppWorkspace;Border:Flat,1,ControlDarkDark,Both;}\tGrandTotal{BackColor:Black;ForeColor:White;}\tSubtotal0{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal1{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal2{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal3{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal4{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal5{BackColor:ControlDarkDark;ForeColor:White;}\t");
		this.c1FlexGrid2.TabIndex = 5;
		this.c1FlexGrid2.Tree.Column = 1;
		this.c1FlexGrid2.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.SimpleLeaf;
		this.c1FlexGrid2.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(c1FlexGrid2_AfterEdit);
		this.c1FlexGrid2.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(c1FlexGrid2_BeforeEdit);
		this.c1FlexGrid2.AfterSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(c1FlexGrid2_AfterSelChange);
		this.panel16.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel16.Controls.Add(this.groupBox6);
		this.panel16.Controls.Add(this.D_Btn_Cncl);
		this.panel16.Controls.Add(this.D_Btn_Next);
		this.panel16.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel16.Location = new System.Drawing.Point(0, 505);
		this.panel16.Name = "panel16";
		this.panel16.Size = new System.Drawing.Size(772, 44);
		this.panel16.TabIndex = 19;
		this.groupBox6.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox6.Location = new System.Drawing.Point(0, 0);
		this.groupBox6.Name = "groupBox6";
		this.groupBox6.Size = new System.Drawing.Size(772, 8);
		this.groupBox6.TabIndex = 4;
		this.groupBox6.TabStop = false;
		this.D_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Cncl.Appearance = appearance2;
		this.D_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.D_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.D_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.D_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Cncl.Location = new System.Drawing.Point(678, 9);
		this.D_Btn_Cncl.Name = "D_Btn_Cncl";
		this.D_Btn_Cncl.ShowFocusRect = false;
		this.D_Btn_Cncl.ShowOutline = false;
		this.D_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.D_Btn_Cncl.SupportThemes = false;
		this.D_Btn_Cncl.TabIndex = 2;
		this.D_Btn_Cncl.Text = "取消";
		this.D_Btn_Next.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance3.Image = resources.GetObject("appearance3.Image");
		appearance3.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Next.Appearance = appearance3;
		this.D_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.D_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.D_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Next.Location = new System.Drawing.Point(586, 9);
		this.D_Btn_Next.Name = "D_Btn_Next";
		this.D_Btn_Next.ShowFocusRect = false;
		this.D_Btn_Next.ShowOutline = false;
		this.D_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.D_Btn_Next.SupportThemes = false;
		this.D_Btn_Next.TabIndex = 1;
		this.D_Btn_Next.Text = "確定";
		this.D_Btn_Next.Click += new System.EventHandler(D_Btn_Next_Click);
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		base.CancelButton = this.D_Btn_Cncl;
		base.ClientSize = new System.Drawing.Size(772, 549);
		base.Controls.Add(this.panel18);
		base.Controls.Add(this.panel16);
		base.Controls.Add(this.panel17);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.Name = "FormBudgetSplit";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "主專案挑選";
		base.Load += new System.EventHandler(FormBudgetSplit_Load);
		this.panel17.ResumeLayout(false);
		this.panel18.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid2).EndInit();
		this.panel16.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}
}
