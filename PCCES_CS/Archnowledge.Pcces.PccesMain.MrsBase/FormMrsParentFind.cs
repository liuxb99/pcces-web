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
using Archnowledge.Pcces.PccesMain.Budget;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace Archnowledge.Pcces.PccesMain.MrsBase;

public class FormMrsParentFind : Form
{
	private const string CallFormHelp = "FormMrsParentFind";

	private Panel panel1;

	private Panel panel2;

	private Panel panel3;

	private UltraButton ultraButton3;

	private IContainer components;

	public GridMrsBase gridMrsBase1;

	private ImageList imageList2;

	private UltraLabel lblItem;

	private string F_UserID;

	private FormStatus FORM_STATUS = FormStatus.Iinitial;

	private int GridCols = 15;

	private object[,] GridColsSquence;

	private DataTable DT1 = new DataTable();

	private string F_ProjectCode;

	private PccesFormAction F_ActionName;

	private int F_MainQty = 0;

	private int F_MainCst = 0;

	private int F_MainAmt = 0;

	private int F_AnaQty = 0;

	private int F_AnaCst = 0;

	private int F_AnaAmt = 0;

	private int F_PubCode;

	private string F_PccesCode;

	private string F_CName;

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

	public string _PccesCode
	{
		get
		{
			return F_PccesCode;
		}
		set
		{
			F_PccesCode = value;
		}
	}

	public string _CName
	{
		get
		{
			return F_CName;
		}
		set
		{
			F_CName = value;
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

	public PccesFormAction _ActionName
	{
		get
		{
			return F_ActionName;
		}
		set
		{
			F_ActionName = value;
		}
	}

	public int _PubCode
	{
		get
		{
			return F_PubCode;
		}
		set
		{
			F_PubCode = value;
		}
	}

	public FormMrsParentFind()
	{
		InitializeComponent();
		GridCols = gridMrsBase1.Cols.Count;
		GridColsSquence = new object[GridCols, 8];
		string sHideCols = CommonMethods.GetDebugValue("FormMrsParentFind", "HideCols");
		HideCols(Convert.ToBoolean((sHideCols == "") ? "True" : sHideCols));
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.MrsBase.FormMrsParentFind));
		this.panel1 = new System.Windows.Forms.Panel();
		this.lblItem = new Infragistics.Win.Misc.UltraLabel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.gridMrsBase1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.panel3 = new System.Windows.Forms.Panel();
		this.ultraButton3 = new Infragistics.Win.Misc.UltraButton();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridMrsBase1).BeginInit();
		this.panel3.SuspendLayout();
		base.SuspendLayout();
		this.panel1.Controls.Add(this.lblItem);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(464, 44);
		this.panel1.TabIndex = 0;
		appearance1.FontData.Name = "細明體";
		appearance1.ForeColor = System.Drawing.Color.White;
		appearance1.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblItem.Appearance = appearance1;
		this.lblItem.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.lblItem.Dock = System.Windows.Forms.DockStyle.Fill;
		this.lblItem.Location = new System.Drawing.Point(0, 0);
		this.lblItem.Name = "lblItem";
		this.lblItem.Size = new System.Drawing.Size(464, 44);
		this.lblItem.TabIndex = 5;
		this.lblItem.Text = " 父項查詢結果列表列表";
		this.panel2.Controls.Add(this.gridMrsBase1);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Location = new System.Drawing.Point(0, 44);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(464, 193);
		this.panel2.TabIndex = 1;
		this.gridMrsBase1._ExcelFileName = "";
		this.gridMrsBase1._ExcelSheeName = "";
		this.gridMrsBase1._IsOpenExcelAfterExport = false;
		this.gridMrsBase1.AllowEditing = false;
		this.gridMrsBase1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
		this.gridMrsBase1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridMrsBase1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
		this.gridMrsBase1.ColumnInfo = "19,1,0,0,0,110,Columns:0{Width:17;Name:\"RowIndicator\";AllowDragging:False;AllowResizing:False;AllowEditing:False;DataType:System.Int32;TextAlign:RightTop;TextAlignFixed:GeneralTop;}\t1{Width:99;Name:\"PccesCode\";Caption:\"工項代碼\";AllowEditing:False;DataType:System.String;TextAlign:LeftBottom;TextAlignFixed:GeneralTop;}\t2{Width:153;Name:\"CName\";Caption:\"工項名稱\";AllowEditing:False;DataType:System.String;TextAlign:LeftBottom;TextAlignFixed:GeneralTop;}\t3{Width:40;Name:\"AnaImg\";Caption:\"分析\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t4{Width:61;Name:\"UnitName\";Caption:\"單位\";AllowEditing:False;DataType:System.String;TextAlign:LeftBottom;TextAlignFixed:GeneralTop;}\t5{Name:\"usrQty\";Caption:\"數量\";AllowEditing:False;DataType:System.Decimal;TextAlign:GeneralBottom;TextAlignFixed:GeneralTop;}\t6{Width:100;Name:\"Cost\";Caption:\"單價\";DataType:System.Decimal;Format:\"###,###,###,##0\";TextAlign:GeneralBottom;TextAlignFixed:GeneralTop;}\t7{Name:\"usrAmt\";Caption:\"複價\";AllowEditing:False;DataType:System.Decimal;TextAlign:GeneralBottom;TextAlignFixed:GeneralTop;}\t8{Width:60;Name:\"Rate\";Caption:\"百分比\";AllowEditing:False;DataType:System.Decimal;TextAlign:RightBottom;TextAlignFixed:GeneralTop;}\t9{Width:40;Name:\"CostKind\";Caption:\"種類\";AllowEditing:False;DataType:System.String;TextAlign:LeftBottom;TextAlignFixed:GeneralTop;}\t10{Width:68;Name:\"LRate\";Caption:\"人工(%)\";DataType:System.Decimal;TextAlign:GeneralBottom;TextAlignFixed:GeneralTop;}\t11{Width:62;Name:\"eRate\";Caption:\"機具(%)\";DataType:System.Decimal;TextAlign:GeneralBottom;TextAlignFixed:GeneralTop;}\t12{Width:61;Name:\"mRate\";Caption:\"材料(%)\";DataType:System.Decimal;TextAlign:GeneralBottom;TextAlignFixed:GeneralTop;}\t13{Width:62;Name:\"wRate\";Caption:\"雜項(%)\";DataType:System.Decimal;TextAlign:GeneralBottom;TextAlignFixed:GeneralTop;}\t14{Width:45;Name:\"XNameC\";Caption:\"區域\";AllowEditing:False;DataType:System.String;TextAlign:LeftBottom;TextAlignFixed:GeneralTop;}\t15{Width:190;Name:\"Memo\";Caption:\"備註*\";DataType:System.String;TextAlign:LeftBottom;TextAlignFixed:GeneralTop;}\t16{Name:\"SNo\";Caption:\"SNo\";DataType:System.Int32;TextAlign:RightCenter;TextAlignFixed:GeneralTop;}\t17{Name:\"PubCode\";Caption:\"PubCode\";DataType:System.Int32;TextAlign:RightBottom;TextAlignFixed:GeneralTop;}\t18{Width:37;Name:\"Analysis\";Caption:\"分析\";AllowEditing:False;DataType:System.Boolean;TextAlign:LeftBottom;TextAlignFixed:GeneralTop;ImageAlign:CenterCenter;}\t";
		this.gridMrsBase1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridMrsBase1.ExtendLastCol = true;
		this.gridMrsBase1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridMrsBase1.ForeColor = System.Drawing.Color.Black;
		this.gridMrsBase1.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.gridMrsBase1.IsProcessUndo = false;
		this.gridMrsBase1.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.gridMrsBase1.Location = new System.Drawing.Point(0, 0);
		this.gridMrsBase1.Name = "gridMrsBase1";
		this.gridMrsBase1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
		this.gridMrsBase1.ShowCursor = true;
		this.gridMrsBase1.ShowToolTipOnNarrowColumn = true;
		this.gridMrsBase1.Size = new System.Drawing.Size(464, 193);
		this.gridMrsBase1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection("Normal{Font:細明體, 11.25pt;BackColor:237, 243, 254;ForeColor:Black;TextAlign:GeneralBottom;Border:Flat,1,Silver,Both;}\tAlternate{BackColor:White;}\tFixed{BackColor:225, 247, 223;TextAlign:GeneralTop;Border:Raised,1,Black,Both;}\tHighlight{BackColor:102, 153, 255;TextAlign:GeneralCenter;Border:None,1,Black,Both;}\tFocus{Font:Arial, 10.5pt, style=Bold;BackColor:102, 153, 255;Margins:0, 0, 0, 0;TextAlign:GeneralCenter;Border:None,1,Black,Both;}\tSearch{BackColor:Highlight;ForeColor:HighlightText;}\tFrozen{BackColor:Beige;}\tEmptyArea{BackColor:232, 232, 232;Border:Flat,1,ControlDarkDark,Both;}\tGrandTotal{BackColor:Black;ForeColor:White;}\tSubtotal0{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal1{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal2{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal3{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal4{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal5{BackColor:ControlDarkDark;ForeColor:White;}\t");
		this.gridMrsBase1.TabIndex = 9;
		this.gridMrsBase1.UndoMax = 10;
		this.gridMrsBase1.AfterSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(gridMrsBase1_AfterSelChange);
		this.gridMrsBase1.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(gridMrsBase1_AfterRowColChange);
		this.panel3.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel3.Controls.Add(this.ultraButton3);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel3.Location = new System.Drawing.Point(0, 237);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(464, 36);
		this.panel3.TabIndex = 2;
		this.ultraButton3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance2.BackColor = System.Drawing.SystemColors.ControlLightLight;
		appearance2.BackColor2 = System.Drawing.SystemColors.ControlLight;
		appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton3.Appearance = appearance2;
		this.ultraButton3.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton3.Cursor = System.Windows.Forms.Cursors.Hand;
		this.ultraButton3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ultraButton3.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraButton3.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton3.Location = new System.Drawing.Point(369, 4);
		this.ultraButton3.Name = "ultraButton3";
		this.ultraButton3.ShowFocusRect = false;
		this.ultraButton3.ShowOutline = false;
		this.ultraButton3.Size = new System.Drawing.Size(90, 28);
		this.ultraButton3.SupportThemes = false;
		this.ultraButton3.TabIndex = 7;
		this.ultraButton3.Text = "結  束";
		this.ultraButton3.Click += new System.EventHandler(ultraButton3_Click);
		this.imageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
		this.imageList2.ImageSize = new System.Drawing.Size(16, 16);
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.White;
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		base.CancelButton = this.ultraButton3;
		base.ClientSize = new System.Drawing.Size(464, 273);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel3);
		base.Controls.Add(this.panel1);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormMrsParentFind";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "父項查詢結果列表";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormMrsParentFind_KeyDown);
		base.Load += new System.EventHandler(FormMrsParentFind_Load);
		base.Activated += new System.EventHandler(FormMrsParentFind_Activated);
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridMrsBase1).EndInit();
		this.panel3.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	private void SetGridColumn()
	{
		for (int i = 0; i < GridCols; i++)
		{
			gridMrsBase1.Cols[i].Name = (string)GridColsSquence[i, 0];
			gridMrsBase1.Cols[i].Caption = (string)GridColsSquence[i, 1];
			gridMrsBase1.Cols[i].Width = (int)GridColsSquence[i, 2];
			gridMrsBase1.Cols[i].DataType = (Type)GridColsSquence[i, 3];
			gridMrsBase1.Cols[i].Visible = (bool)GridColsSquence[i, 4];
			gridMrsBase1.Cols[i].Format = (string)GridColsSquence[i, 5];
			gridMrsBase1.Cols[i].AllowEditing = (bool)GridColsSquence[i, 6];
			gridMrsBase1.Cols[i].TextAlign = (TextAlignEnum)GridColsSquence[i, 7];
		}
	}

	private void SettingDecimal()
	{
		DataTable DTDecimal = new DataTable();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("WinFORM 基本工料");
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
			F_MainQty = 0;
			F_MainCst = 0;
			F_MainAmt = 0;
			F_AnaQty = 3;
			F_AnaCst = 2;
			F_AnaAmt = 2;
		}
	}

	private void HideCols(bool IsHide)
	{
		if (IsHide)
		{
			gridMrsBase1.Cols["Analysis"].Visible = false;
			gridMrsBase1.Cols["SNo"].Visible = false;
			gridMrsBase1.Cols["PubCode"].Visible = false;
		}
	}

	private void RememberColsProps()
	{
		for (int i = 0; i < GridCols; i++)
		{
			GridColsSquence[i, 0] = gridMrsBase1.Cols[i].Name;
			GridColsSquence[i, 1] = gridMrsBase1.Cols[i].Caption;
			GridColsSquence[i, 2] = gridMrsBase1.Cols[i].Width;
			if (gridMrsBase1.Cols[i].Name == "AnaImg")
			{
				GridColsSquence[i, 3] = typeof(Image);
			}
			else
			{
				GridColsSquence[i, 3] = gridMrsBase1.Cols[i].DataType;
			}
			GridColsSquence[i, 4] = gridMrsBase1.Cols[i].Visible;
			GridColsSquence[i, 5] = gridMrsBase1.Cols[i].Format;
			GridColsSquence[i, 6] = gridMrsBase1.Cols[i].AllowEditing;
			if (gridMrsBase1.Cols[i].Name == "usrQty")
			{
				if (F_MainCst > 0)
				{
					GridColsSquence[i, 5] = "###,###,###,##0." + "0".PadLeft(F_MainQty, '0');
				}
				else
				{
					GridColsSquence[i, 5] = "###,###,###,##0";
				}
			}
			if (gridMrsBase1.Cols[i].Name == "Cost")
			{
				if (F_MainCst > 0)
				{
					GridColsSquence[i, 5] = "###,###,###,##0." + "0".PadLeft(F_AnaCst, '0');
				}
				else
				{
					GridColsSquence[i, 5] = "###,###,###,##0";
				}
			}
			if (gridMrsBase1.Cols[i].Name == "usrAmt")
			{
				if (F_MainCst > 0)
				{
					GridColsSquence[i, 5] = "###,###,###,##0." + "0".PadLeft(F_MainAmt, '0');
				}
				else
				{
					GridColsSquence[i, 5] = "###,###,###,##0";
				}
			}
			GridColsSquence[i, 7] = gridMrsBase1.Cols[i].TextAlign;
		}
	}

	private void BindToGrid()
	{
		FORM_STATUS = FormStatus.Edit;
		RememberColsProps();
		DataView DV1 = DT1.DefaultView;
		DV1.Sort = " pccesCode ASC ";
		CellStyle CS1 = gridMrsBase1.Styles.Add("AnalysisColor");
		CellStyle CS2 = gridMrsBase1.Styles.Add("LEMColor");
		CellStyle CS3 = gridMrsBase1.Styles.Add("WColor");
		CS1.ForeColor = Color.Red;
		CS2.ForeColor = Color.Teal;
		CS3.ForeColor = Color.Purple;
		gridMrsBase1.Clear(ClearFlags.All);
		gridMrsBase1.Select(0, 0);
		gridMrsBase1.Rows.Count = DV1.Count + 1;
		SetGridColumn();
		string sItemClass = "";
		for (int i = 0; i < DV1.Count; i++)
		{
			sItemClass = DV1[i]["pccesCode"].ToString().Substring(0, 1);
			gridMrsBase1[i + 1, "PccesCode"] = DV1[i]["pccesCode"].ToString();
			if (sItemClass == "L" || sItemClass == "E" || sItemClass == "M")
			{
				gridMrsBase1.Rows[i + 1].Style = gridMrsBase1.Styles["LEMColor"];
			}
			else if (sItemClass == "W")
			{
				gridMrsBase1.Rows[i + 1].Style = gridMrsBase1.Styles["WColor"];
			}
			gridMrsBase1[i + 1, "CName"] = DV1[i]["cName"].ToString();
			if (DV1[i]["analysis"].ToString().Trim() == "1")
			{
				gridMrsBase1[i + 1, "Analysis"] = true;
				gridMrsBase1.Rows[i + 1].Style = gridMrsBase1.Styles["AnalysisColor"];
				CellRange rg = gridMrsBase1.GetCellRange(i + 1, gridMrsBase1.Cols["AnaImg"].SafeIndex);
				rg.Style = gridMrsBase1.Styles["img"];
				rg.Image = imageList2.Images[0];
			}
			else
			{
				gridMrsBase1[i + 1, "Analysis"] = false;
			}
			gridMrsBase1[i + 1, "UnitName"] = DV1[i]["unitName"];
			gridMrsBase1[i + 1, "Rate"] = DV1[i]["rate"];
			gridMrsBase1[i + 1, "CostKind"] = DV1[i]["costKind"];
			gridMrsBase1[i + 1, "LRate"] = DV1[i]["lRate"];
			gridMrsBase1[i + 1, "ERate"] = DV1[i]["eRate"];
			gridMrsBase1[i + 1, "MRate"] = DV1[i]["mRate"];
			gridMrsBase1[i + 1, "WRate"] = DV1[i]["wRate"];
			gridMrsBase1[i + 1, "XNameC"] = DV1[i]["xNameC"];
			gridMrsBase1[i + 1, "Memo"] = DV1[i]["memo"];
			gridMrsBase1[i + 1, "PubCode"] = DV1[i]["pubCode"];
			gridMrsBase1[i + 1, "Cost"] = DV1[i]["cost"];
			gridMrsBase1[i + 1, "usrQty"] = DV1[i]["usrQty"];
			gridMrsBase1[i + 1, "Cost"] = DV1[i]["cost"];
			gridMrsBase1[i + 1, "usrAmt"] = DV1[i]["usrAmt"];
		}
		FORM_STATUS = FormStatus.Normal;
	}

	private void LoadData()
	{
		lblItem.Text = "查詢項:【" + F_PccesCode + "】" + F_CName;
		SettingDecimal();
		RememberColsProps();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("WinFORM 基本工料");
		MrsBaseA dbMrsBase = new MrsBaseA(F_UserID, aArr);
		dbMrsBase.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		dbMrsBase.ps_projectcode = F_ProjectCode;
		DT1 = dbMrsBase.ListParentItem(F_PubCode.ToString());
		BindToGrid();
	}

	public void ExecuteQry()
	{
		LoadData();
	}

	private void GotoSpecificRow()
	{
		int iFind = -1;
		if (base.Owner.Name == "frmMrsBase")
		{
			iFind = (base.Owner as frmMrsBase).gridMrsBase1.FindRow(gridMrsBase1[gridMrsBase1.Row, "PubCode"].ToString(), 1, (base.Owner as frmMrsBase).gridMrsBase1.Cols["PubCode"].SafeIndex, caseSensitive: false, fullMatch: true, wrap: true);
			if (iFind > -1)
			{
				(base.Owner as frmMrsBase).gridMrsBase1.Row = iFind;
				(base.Owner as frmMrsBase).gridMrsBase1.Select();
			}
		}
		else if (base.Owner.Name == "FormBudgetRes")
		{
			iFind = (base.Owner as FormBudgetRes).gridMrsBase1.FindRow(gridMrsBase1[gridMrsBase1.Row, "PubCode"].ToString(), 1, (base.Owner as FormBudgetRes).gridMrsBase1.Cols["PubCode"].SafeIndex, caseSensitive: false, fullMatch: true, wrap: true);
			if (iFind > -1)
			{
				(base.Owner as FormBudgetRes).gridMrsBase1.Row = iFind;
				(base.Owner as FormBudgetRes).gridMrsBase1.Select();
			}
		}
	}

	private void FormMrsParentFind_Load(object sender, EventArgs e)
	{
		LoadData();
	}

	private void ultraButton3_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void gridMrsBase1_AfterRowColChange(object sender, RangeEventArgs e)
	{
		if (FORM_STATUS == FormStatus.Normal)
		{
			GotoSpecificRow();
		}
	}

	private void gridMrsBase1_AfterSelChange(object sender, RangeEventArgs e)
	{
	}

	private void FormMrsParentFind_Activated(object sender, EventArgs e)
	{
		if (FORM_STATUS == FormStatus.Active)
		{
			FORM_STATUS = FormStatus.Normal;
		}
	}

	private void FormMrsParentFind_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormMrsParentFind");
		}
	}
}
