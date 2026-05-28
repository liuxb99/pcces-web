using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using System.Xml.Schema;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.PccesMain.BudgetChange;
using Archnowledge.Pcces.PccesMain.MrsBase;
using Archnowledge.Pcces.STDClass;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTabControl;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormBudgetThirdParty : Form
{
	private string ErrorStr;

	private DataSet DS_Read = new DataSet();

	private string F_CallFormName = "";

	private string UserID;

	private IContainer components;

	private UltraTabControl Tab_Ctrl;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl Tab_A;

	private UltraTabPageControl Tab_B;

	private Panel panel2;

	private Panel panel4;

	private GroupBox groupBox2;

	private UltraButton B_Btn_Cncl;

	private UltraButton B_Btn_Next;

	private UltraButton B_Btn_Prev;

	private Panel panel1;

	private UltraLabel ultraLabel7;

	private UltraLabel ultraLabel6;

	private UltraTextEditor txtPath;

	private UltraButton BtnChgDir;

	private UltraLabel ultraLabel17;

	private OpenFileDialog openFileDialog1;

	private Panel panel3;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	private Panel panel5;

	private GroupBox groupBox1;

	private UltraButton C_Cncl;

	private UltraButton C_Btn_Prev;

	private UltraButton C_Btn_OK;

	public GridMrsBase Grid1;

	public string _UserID
	{
		get
		{
			return UserID;
		}
		set
		{
			UserID = value;
		}
	}

	public string _CallFormName
	{
		get
		{
			return F_CallFormName;
		}
		set
		{
			F_CallFormName = value;
		}
	}

	public FormBudgetThirdParty()
	{
		InitializeComponent();
	}

	private void BtnChgDir_Click(object sender, EventArgs e)
	{
		openFileDialog1.RestoreDirectory = true;
		openFileDialog1.Filter = "3rd Party 廠商轉出之數量xml 格式(*.xml)|*.xml";
		if (openFileDialog1.ShowDialog() == DialogResult.OK)
		{
			txtPath.Text = openFileDialog1.FileName;
		}
	}

	public void ValidationHandler(object sender, ValidationEventArgs args)
	{
		object errorStr = ErrorStr;
		ErrorStr = string.Concat(errorStr, "( ", args.Severity, " ) ", args.Message, '\r');
	}

	private void B_Btn_Next_Click(object sender, EventArgs e)
	{
		if (CommonMethods.ExtractExtFileName(txtPath.Text.Trim()).ToUpper() != "XML")
		{
			MessageBox.Show(this, "挑選的檔案不是XML格式!", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		DS_Read.ReadXml(txtPath.Text);
		Tab_B.Tab.Selected = true;
		Grid1.Rows.Count = DS_Read.Tables[0].Rows.Count + 1;
		if (DS_Read.Tables[0].Rows.Count > 0)
		{
			for (int i = 0; i < DS_Read.Tables[0].Rows.Count; i++)
			{
				Grid1[i + 1, "pccesCode"] = DS_Read.Tables[0].Rows[i]["refItemCode"].ToString();
				Grid1[i + 1, "cName"] = DS_Read.Tables[0].Rows[i]["Description"].ToString();
				Grid1[i + 1, "Unit"] = DS_Read.Tables[0].Rows[i]["Unit"].ToString();
				Grid1[i + 1, "qty"] = DS_Read.Tables[0].Rows[i]["Quantity"].ToString();
			}
		}
	}

	private void C_Btn_OK_Click(object sender, EventArgs e)
	{
		DataSet DS1 = new DataSet("tempDS");
		DataTable DT1 = new DataTable("tempTable");
		for (int i = 1; i < Grid1.Cols.Count; i++)
		{
			DataColumn DC = new DataColumn(Grid1.Cols[i].Name, Grid1.Cols[i].DataType);
			DT1.Columns.Add(DC);
		}
		for (int i = 1; i < Grid1.Rows.Count; i++)
		{
			DataRow DR = DT1.NewRow();
			for (int j = 0; j < DT1.Columns.Count; j++)
			{
				if ((object)Grid1.Cols[DT1.Columns[j].ColumnName].DataType != Type.GetType("System.String") && Grid1[i, DT1.Columns[j].ColumnName] == null)
				{
					if (Grid1.Cols[DT1.Columns[j].ColumnName].Name == "CostDec" || Grid1.Cols[DT1.Columns[j].ColumnName].Name == "AmtDec")
					{
						DR[Grid1.Cols[DT1.Columns[j].ColumnName].Name] = DBNull.Value;
					}
					else
					{
						DR[Grid1.Cols[DT1.Columns[j].ColumnName].Name] = 0;
					}
				}
				else
				{
					DR[Grid1.Cols[DT1.Columns[j].ColumnName].Name] = Grid1[i, DT1.Columns[j].ColumnName];
				}
			}
			DT1.Rows.Add(DR);
		}
		DT1.Columns.Add("pubCode", Type.GetType("System.Int32"));
		DT1.Columns.Add("UnitName", Type.GetType("System.String"));
		DT1.Columns.Add("memo", Type.GetType("System.String"));
		DT1.Columns.Add("eName", Type.GetType("System.String"));
		DT1.Columns.Add("eUnit", Type.GetType("System.String"));
		DT1.Columns.Add("Analysis", Type.GetType("System.String"));
		DT1.Columns.Add("surName", Type.GetType("System.String"));
		DT1.Columns.Add("CostDec", Type.GetType("System.Int32"));
		DT1.Columns.Add("AmtDec", Type.GetType("System.Int32"));
		DT1.Columns.Add("PwrSet", Type.GetType("System.Int32"));
		DS1.Tables.Add(DT1);
		ArrayList aArr = new ArrayList();
		aArr.Add(UserID);
		aArr.Add("WinFORM 基本工料");
		string l_str = "Select * from mrsBaseA ";
		ModifyDB StdCom = new ModifyDB("", aArr);
		DataTable DT2 = StdCom.DBList(l_str);
		StdCom = null;
		MrsBaseA dbMrsBase = new MrsBaseA(UserID, aArr);
		dbMrsBase.ps_srckind = "MRS";
		DT2.CaseSensitive = true;
		for (int i = 0; i < DS1.Tables[0].Rows.Count; i++)
		{
			DataView DV = new DataView(DT2);
			DV.RowFilter = "PccesCode = '" + DS1.Tables[0].Rows[i]["PccesCode"].ToString().Trim() + "'";
			if (DV.Count == 0)
			{
				dbMrsBase.ps_pccesCode = DS1.Tables[0].Rows[i]["PccesCode"].ToString().Trim();
				dbMrsBase.ps_cName = DS1.Tables[0].Rows[i]["CName"].ToString().Trim();
				dbMrsBase.ps_unitName = DS1.Tables[0].Rows[i]["Unit"].ToString().Trim();
				DS1.Tables[0].Rows[i]["UnitName"] = DS1.Tables[0].Rows[i]["Unit"];
				dbMrsBase.InseItem();
				string sSQL = "Select pubcode from mrsBaseA where pccescode = '" + DS1.Tables[0].Rows[i]["PccesCode"].ToString().Trim() + "'";
				ModifyDB ModDB = new ModifyDB("", aArr);
				DataTable DTtemp = new DataTable();
				DTtemp = ModDB.DBList(sSQL);
				if (DTtemp.Rows.Count > 0)
				{
					DS1.Tables[0].Rows[i]["pubCode"] = PubTools.Str2Int(DTtemp.Rows[0]["pubcode"].ToString());
				}
				ModDB = null;
			}
			else
			{
				DS1.Tables[0].Rows[i]["pubCode"] = DV[0]["pubCode"];
			}
			DS1.Tables[0].Rows[i]["memo"] = "";
			DS1.Tables[0].Rows[i]["eName"] = "";
			DS1.Tables[0].Rows[i]["eUnit"] = "";
			DS1.Tables[0].Rows[i]["Analysis"] = "";
			DS1.Tables[0].Rows[i]["surName"] = "";
			DS1.Tables[0].Rows[i]["CostDec"] = DBNull.Value;
			DS1.Tables[0].Rows[i]["AmtDec"] = DBNull.Value;
			DS1.Tables[0].Rows[i]["PwrSet"] = DBNull.Value;
		}
		Form ActiveForm = base.Owner.ActiveMdiChild;
		if (F_CallFormName.ToUpper() == "frmBudget".ToUpper())
		{
			if (ActiveForm is frmBudget)
			{
				(ActiveForm as frmBudget)._PasteSource_SrcKind = "MRS";
				(ActiveForm as frmBudget)._PasteSource_Project = "";
				(ActiveForm as frmBudget)._ChangeQTY = "QTY";
				(ActiveForm as frmBudget).Th_MenuPaste(DS1);
			}
		}
		else if (F_CallFormName.ToUpper() == "FormBudgetChange".ToUpper())
		{
			if (ActiveForm is FormBudgetChange)
			{
				(ActiveForm as FormBudgetChange)._PasteSource_SrcKind = "MRS";
				(ActiveForm as FormBudgetChange)._PasteSource_Project = "";
				(ActiveForm as FormBudgetChange).Th_MenuPaste(DS1);
			}
		}
		else
		{
			(base.Owner as FormMrsBaseBreakdown)._PasteSource = "MRS";
			(base.Owner as FormMrsBaseBreakdown).Th_MenuPaste(DS1);
		}
		DS1 = null;
		DT1 = null;
		base.DialogResult = DialogResult.OK;
		Close();
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormBudgetThirdParty));
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		this.Tab_A = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel1 = new System.Windows.Forms.Panel();
		this.ultraLabel17 = new Infragistics.Win.Misc.UltraLabel();
		this.txtPath = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.BtnChgDir = new Infragistics.Win.Misc.UltraButton();
		this.panel4 = new System.Windows.Forms.Panel();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.B_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.B_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.B_Btn_Prev = new Infragistics.Win.Misc.UltraButton();
		this.panel2 = new System.Windows.Forms.Panel();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_B = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.Grid1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.panel5 = new System.Windows.Forms.Panel();
		this.C_Btn_OK = new Infragistics.Win.Misc.UltraButton();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.C_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.C_Btn_Prev = new Infragistics.Win.Misc.UltraButton();
		this.panel3 = new System.Windows.Forms.Panel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_Ctrl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
		this.Tab_A.SuspendLayout();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtPath).BeginInit();
		this.panel4.SuspendLayout();
		this.panel2.SuspendLayout();
		this.Tab_B.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Grid1).BeginInit();
		this.panel5.SuspendLayout();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).BeginInit();
		this.Tab_Ctrl.SuspendLayout();
		base.SuspendLayout();
		this.Tab_A.Controls.Add(this.panel1);
		this.Tab_A.Controls.Add(this.panel4);
		this.Tab_A.Controls.Add(this.panel2);
		this.Tab_A.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_A.Name = "Tab_A";
		this.Tab_A.Size = new System.Drawing.Size(560, 453);
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.Controls.Add(this.ultraLabel17);
		this.panel1.Controls.Add(this.txtPath);
		this.panel1.Controls.Add(this.BtnChgDir);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 56);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(560, 353);
		this.panel1.TabIndex = 13;
		this.ultraLabel17.Location = new System.Drawing.Point(33, 48);
		this.ultraLabel17.Name = "ultraLabel17";
		this.ultraLabel17.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel17.TabIndex = 18;
		this.ultraLabel17.Text = "欲轉入的電子檔:";
		appearance1.FontData.Name = "細明體";
		appearance1.FontData.SizeInPoints = 11f;
		this.txtPath.Appearance = appearance1;
		this.txtPath.Location = new System.Drawing.Point(33, 72);
		this.txtPath.Name = "txtPath";
		this.txtPath.Size = new System.Drawing.Size(448, 24);
		this.txtPath.TabIndex = 17;
		appearance2.FontData.Name = "Arial";
		appearance2.FontData.SizeInPoints = 8f;
		this.BtnChgDir.Appearance = appearance2;
		this.BtnChgDir.BackColor = System.Drawing.SystemColors.Control;
		this.BtnChgDir.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.BtnChgDir.Location = new System.Drawing.Point(480, 71);
		this.BtnChgDir.Name = "BtnChgDir";
		this.BtnChgDir.ShowFocusRect = false;
		this.BtnChgDir.ShowOutline = false;
		this.BtnChgDir.Size = new System.Drawing.Size(48, 24);
		this.BtnChgDir.SupportThemes = false;
		this.BtnChgDir.TabIndex = 16;
		this.BtnChgDir.Text = "瀏覽...";
		this.BtnChgDir.Click += new System.EventHandler(BtnChgDir_Click);
		this.panel4.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel4.Controls.Add(this.groupBox2);
		this.panel4.Controls.Add(this.B_Btn_Cncl);
		this.panel4.Controls.Add(this.B_Btn_Next);
		this.panel4.Controls.Add(this.B_Btn_Prev);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel4.Location = new System.Drawing.Point(0, 409);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(560, 44);
		this.panel4.TabIndex = 12;
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(560, 8);
		this.groupBox2.TabIndex = 3;
		this.groupBox2.TabStop = false;
		this.B_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance3.Image = resources.GetObject("appearance3.Image");
		appearance3.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.B_Btn_Cncl.Appearance = appearance3;
		this.B_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.B_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.B_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.B_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.B_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.B_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.B_Btn_Cncl.Location = new System.Drawing.Point(464, 9);
		this.B_Btn_Cncl.Name = "B_Btn_Cncl";
		this.B_Btn_Cncl.ShowFocusRect = false;
		this.B_Btn_Cncl.ShowOutline = false;
		this.B_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.B_Btn_Cncl.SupportThemes = false;
		this.B_Btn_Cncl.TabIndex = 2;
		this.B_Btn_Cncl.Text = "取消";
		this.B_Btn_Next.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance4.Image = resources.GetObject("appearance4.Image");
		appearance4.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance4.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.B_Btn_Next.Appearance = appearance4;
		this.B_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.B_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.B_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.B_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.B_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.B_Btn_Next.Location = new System.Drawing.Point(372, 9);
		this.B_Btn_Next.Name = "B_Btn_Next";
		this.B_Btn_Next.ShowFocusRect = false;
		this.B_Btn_Next.ShowOutline = false;
		this.B_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.B_Btn_Next.SupportThemes = false;
		this.B_Btn_Next.TabIndex = 1;
		this.B_Btn_Next.Text = "下一步";
		this.B_Btn_Next.Click += new System.EventHandler(B_Btn_Next_Click);
		this.B_Btn_Prev.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance5.Image = resources.GetObject("appearance5.Image");
		appearance5.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.B_Btn_Prev.Appearance = appearance5;
		this.B_Btn_Prev.BackColor = System.Drawing.SystemColors.Control;
		this.B_Btn_Prev.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.B_Btn_Prev.Font = new System.Drawing.Font("細明體", 11f);
		this.B_Btn_Prev.ImageSize = new System.Drawing.Size(20, 20);
		this.B_Btn_Prev.ImageTransparentColor = System.Drawing.Color.White;
		this.B_Btn_Prev.Location = new System.Drawing.Point(280, 9);
		this.B_Btn_Prev.Name = "B_Btn_Prev";
		this.B_Btn_Prev.ShowFocusRect = false;
		this.B_Btn_Prev.ShowOutline = false;
		this.B_Btn_Prev.Size = new System.Drawing.Size(88, 31);
		this.B_Btn_Prev.SupportThemes = false;
		this.B_Btn_Prev.TabIndex = 0;
		this.B_Btn_Prev.Text = "上一步";
		this.B_Btn_Prev.Visible = false;
		this.panel2.BackColor = System.Drawing.Color.White;
		this.panel2.Controls.Add(this.ultraLabel7);
		this.panel2.Controls.Add(this.ultraLabel6);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(560, 56);
		this.panel2.TabIndex = 2;
		appearance6.BackColor = System.Drawing.Color.White;
		this.ultraLabel7.Appearance = appearance6;
		this.ultraLabel7.Location = new System.Drawing.Point(48, 29);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel7.TabIndex = 6;
		this.ultraLabel7.Text = "請挑選你的數量轉入XML檔案";
		appearance7.BackColor = System.Drawing.Color.White;
		this.ultraLabel6.Appearance = appearance7;
		this.ultraLabel6.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel6.Location = new System.Drawing.Point(16, 8);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel6.TabIndex = 5;
		this.ultraLabel6.Text = "數量轉入XML格式挑選";
		this.Tab_B.Controls.Add(this.Grid1);
		this.Tab_B.Controls.Add(this.panel5);
		this.Tab_B.Controls.Add(this.panel3);
		this.Tab_B.Location = new System.Drawing.Point(0, 0);
		this.Tab_B.Name = "Tab_B";
		this.Tab_B.Size = new System.Drawing.Size(560, 453);
		this.Grid1._ExcelFileName = "";
		this.Grid1._ExcelSheeName = "";
		this.Grid1._IsOpenExcelAfterExport = false;
		this.Grid1.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
		this.Grid1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
		this.Grid1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.Grid1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
		this.Grid1.ColumnInfo = "5,1,0,0,0,110,Columns:0{Width:17;Name:\"RowIndicator\";AllowDragging:False;AllowResizing:False;AllowEditing:False;DataType:System.Int32;TextAlign:RightTop;TextAlignFixed:GeneralTop;}\t1{Width:110;Name:\"PccesCode\";Caption:\"工項代碼\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t2{Width:300;Name:\"CName\";Caption:\"工項名稱\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t3{Width:150;Name:\"Unit\";Caption:\"單位\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t4{Width:150;Name:\"qty\";Caption:\"數量\";DataType:System.Decimal;Format:\"###,###,###,##0\";TextAlign:GeneralBottom;TextAlignFixed:GeneralTop;}\t";
		this.Grid1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Grid1.ExtendLastCol = true;
		this.Grid1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.Grid1.ForeColor = System.Drawing.Color.Black;
		this.Grid1.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.Grid1.IsProcessUndo = false;
		this.Grid1.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.Grid1.Location = new System.Drawing.Point(0, 56);
		this.Grid1.Name = "Grid1";
		this.Grid1.Rows.Count = 1;
		this.Grid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.Grid1.ShowCursor = true;
		this.Grid1.ShowToolTipOnNarrowColumn = true;
		this.Grid1.Size = new System.Drawing.Size(560, 353);
		this.Grid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection("Normal{Font:細明體, 11.25pt;BackColor:237, 243, 254;ForeColor:Black;TextAlign:GeneralBottom;Border:Flat,1,Silver,Both;}\tAlternate{BackColor:White;}\tFixed{BackColor:225, 247, 223;TextAlign:GeneralTop;Border:Raised,1,Black,Both;}\tHighlight{BackColor:102, 153, 255;TextAlign:GeneralCenter;ImageAlign:CenterCenter;Border:None,1,Black,Both;Format:\"###,###,###,##0\";}\tFocus{Font:細明體, 10pt, style=Bold;BackColor:White;Margins:0, 0, 0, 0;TextAlign:GeneralCenter;Border:Double,1,96, 145, 234,Both;}\tSearch{BackColor:Highlight;ForeColor:HighlightText;}\tFrozen{BackColor:Beige;}\tEmptyArea{BackColor:232, 232, 232;Border:Flat,1,ControlDarkDark,Both;}\tGrandTotal{BackColor:Black;ForeColor:White;}\tSubtotal0{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal1{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal2{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal3{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal4{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal5{BackColor:ControlDarkDark;ForeColor:White;}\t");
		this.Grid1.TabIndex = 14;
		this.Grid1.UndoMax = 10;
		this.panel5.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel5.Controls.Add(this.C_Btn_OK);
		this.panel5.Controls.Add(this.groupBox1);
		this.panel5.Controls.Add(this.C_Cncl);
		this.panel5.Controls.Add(this.C_Btn_Prev);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel5.Location = new System.Drawing.Point(0, 409);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(560, 44);
		this.panel5.TabIndex = 13;
		this.C_Btn_OK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance8.Image = resources.GetObject("appearance8.Image");
		appearance8.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.C_Btn_OK.Appearance = appearance8;
		this.C_Btn_OK.BackColor = System.Drawing.SystemColors.Control;
		this.C_Btn_OK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.C_Btn_OK.Font = new System.Drawing.Font("細明體", 11f);
		this.C_Btn_OK.ImageSize = new System.Drawing.Size(20, 20);
		this.C_Btn_OK.ImageTransparentColor = System.Drawing.Color.White;
		this.C_Btn_OK.Location = new System.Drawing.Point(372, 9);
		this.C_Btn_OK.Name = "C_Btn_OK";
		this.C_Btn_OK.ShowFocusRect = false;
		this.C_Btn_OK.ShowOutline = false;
		this.C_Btn_OK.Size = new System.Drawing.Size(88, 31);
		this.C_Btn_OK.SupportThemes = false;
		this.C_Btn_OK.TabIndex = 4;
		this.C_Btn_OK.Text = "轉入";
		this.C_Btn_OK.Click += new System.EventHandler(C_Btn_OK_Click);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(560, 8);
		this.groupBox1.TabIndex = 3;
		this.groupBox1.TabStop = false;
		this.C_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance9.Image = resources.GetObject("appearance9.Image");
		appearance9.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.C_Cncl.Appearance = appearance9;
		this.C_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.C_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.C_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.C_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.C_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.C_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.C_Cncl.Location = new System.Drawing.Point(464, 9);
		this.C_Cncl.Name = "C_Cncl";
		this.C_Cncl.ShowFocusRect = false;
		this.C_Cncl.ShowOutline = false;
		this.C_Cncl.Size = new System.Drawing.Size(88, 31);
		this.C_Cncl.SupportThemes = false;
		this.C_Cncl.TabIndex = 2;
		this.C_Cncl.Text = "取消";
		this.C_Btn_Prev.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance10.Image = resources.GetObject("appearance10.Image");
		appearance10.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.C_Btn_Prev.Appearance = appearance10;
		this.C_Btn_Prev.BackColor = System.Drawing.SystemColors.Control;
		this.C_Btn_Prev.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.C_Btn_Prev.Font = new System.Drawing.Font("細明體", 11f);
		this.C_Btn_Prev.ImageSize = new System.Drawing.Size(20, 20);
		this.C_Btn_Prev.ImageTransparentColor = System.Drawing.Color.White;
		this.C_Btn_Prev.Location = new System.Drawing.Point(280, 9);
		this.C_Btn_Prev.Name = "C_Btn_Prev";
		this.C_Btn_Prev.ShowFocusRect = false;
		this.C_Btn_Prev.ShowOutline = false;
		this.C_Btn_Prev.Size = new System.Drawing.Size(88, 31);
		this.C_Btn_Prev.SupportThemes = false;
		this.C_Btn_Prev.TabIndex = 0;
		this.C_Btn_Prev.Text = "上一步";
		this.panel3.BackColor = System.Drawing.Color.White;
		this.panel3.Controls.Add(this.ultraLabel1);
		this.panel3.Controls.Add(this.ultraLabel2);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel3.Location = new System.Drawing.Point(0, 0);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(560, 56);
		this.panel3.TabIndex = 3;
		appearance11.BackColor = System.Drawing.Color.White;
		this.ultraLabel1.Appearance = appearance11;
		this.ultraLabel1.Location = new System.Drawing.Point(48, 29);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel1.TabIndex = 6;
		this.ultraLabel1.Text = "請確認你的資料內容正確";
		appearance12.BackColor = System.Drawing.Color.White;
		this.ultraLabel2.Appearance = appearance12;
		this.ultraLabel2.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel2.Location = new System.Drawing.Point(16, 8);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel2.TabIndex = 5;
		this.ultraLabel2.Text = "數量轉入";
		this.Tab_Ctrl.Controls.Add(this.ultraTabSharedControlsPage1);
		this.Tab_Ctrl.Controls.Add(this.Tab_A);
		this.Tab_Ctrl.Controls.Add(this.Tab_B);
		this.Tab_Ctrl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Tab_Ctrl.Location = new System.Drawing.Point(0, 0);
		this.Tab_Ctrl.Name = "Tab_Ctrl";
		this.Tab_Ctrl.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.Tab_Ctrl.Size = new System.Drawing.Size(560, 453);
		this.Tab_Ctrl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
		this.Tab_Ctrl.TabIndex = 12;
		ultraTab1.TabPage = this.Tab_A;
		ultraTab1.Text = "tab1";
		ultraTab2.TabPage = this.Tab_B;
		ultraTab2.Text = "tab2";
		this.Tab_Ctrl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[2] { ultraTab1, ultraTab2 });
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(560, 453);
		this.AutoScaleBaseSize = new System.Drawing.Size(6, 18);
		base.CancelButton = this.B_Btn_Cncl;
		base.ClientSize = new System.Drawing.Size(560, 453);
		base.Controls.Add(this.Tab_Ctrl);
		this.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.Name = "FormBudgetThirdParty";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "3rd Party 廠商數量轉入";
		this.Tab_A.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtPath).EndInit();
		this.panel4.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.Tab_B.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Grid1).EndInit();
		this.panel5.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).EndInit();
		this.Tab_Ctrl.ResumeLayout(false);
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
