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
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinStatusBar;

namespace Archnowledge.Pcces.PccesMain.MrsBase;

public class FormMrsBaseApprove : Form
{
	private const string CallFormHelp = "FormMrsBaseApprove";

	private Panel panel1;

	private GroupBox groupBox1;

	private UltraButton A_Btn_Cncl;

	private UltraButton A_Btn_Next;

	private Panel panel5;

	private UltraLabel ultraLabel6;

	private UltraCheckEditor ultraCheckEditor1;

	private UltraStatusBar ultraStatusBar1;

	public GridMrsBase gridMrsBase1;

	private ImageList imageList2;

	private IContainer components;

	private FormStatus FORM_STATUS = FormStatus.Iinitial;

	private DataTable DT1 = new DataTable();

	private string F_UserID = "";

	private ArrayList aArr = new ArrayList();

	private string ExtraCri = "";

	private int GridCols = 15;

	private object[,] GridColsSquence;

	private int F_MainQty = 0;

	private int F_MainCst = 0;

	private int F_MainAmt = 0;

	private int F_AnaQty = 0;

	private int F_AnaCst = 0;

	private int F_AnaAmt = 0;

	private MrsBaseA dbMrsBase;

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

	public FormMrsBaseApprove()
	{
		InitializeComponent();
		GridCols = gridMrsBase1.Cols.Count;
		GridColsSquence = new object[GridCols, 10];
		CellStyle cs = gridMrsBase1.Styles.Add("img");
		cs.DataType = typeof(Image);
		CellStyle cs2 = gridMrsBase1.Styles.Add("EditMode");
		cs2.DataType = typeof(Image);
		cs2.ImageAlign = ImageAlignEnum.RightCenter;
	}

	private void HideCols(bool IsHide)
	{
		if (IsHide)
		{
			gridMrsBase1.Cols["PubCode"].Visible = false;
			gridMrsBase1.Cols["Analysis"].Visible = false;
			gridMrsBase1.Cols["Show"].Visible = false;
			gridMrsBase1.Cols["OrigPostMode"].Visible = false;
		}
	}

	private void SettingDecimal()
	{
		DataTable DTDecimal = new DataTable();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("基本工料--小數位數讀取");
		PubDecimal dbDecimal = new PubDecimal(aArr);
		DTDecimal = dbDecimal.ListItem("", "");
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
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.MrsBase.FormMrsBaseApprove));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel3 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		this.panel1 = new System.Windows.Forms.Panel();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.A_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.A_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.panel5 = new System.Windows.Forms.Panel();
		this.ultraCheckEditor1 = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraStatusBar1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.gridMrsBase1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.panel1.SuspendLayout();
		this.panel5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridMrsBase1).BeginInit();
		base.SuspendLayout();
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.Controls.Add(this.groupBox1);
		this.panel1.Controls.Add(this.A_Btn_Cncl);
		this.panel1.Controls.Add(this.A_Btn_Next);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 509);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(692, 44);
		this.panel1.TabIndex = 10;
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(692, 8);
		this.groupBox1.TabIndex = 3;
		this.groupBox1.TabStop = false;
		this.A_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Cncl.Appearance = appearance1;
		this.A_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.A_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_Cncl.Location = new System.Drawing.Point(596, 9);
		this.A_Btn_Cncl.Name = "A_Btn_Cncl";
		this.A_Btn_Cncl.ShowFocusRect = false;
		this.A_Btn_Cncl.ShowOutline = false;
		this.A_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.A_Btn_Cncl.SupportThemes = false;
		this.A_Btn_Cncl.TabIndex = 2;
		this.A_Btn_Cncl.Text = "取消";
		this.A_Btn_Next.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Next.Appearance = appearance2;
		this.A_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_Next.Location = new System.Drawing.Point(504, 9);
		this.A_Btn_Next.Name = "A_Btn_Next";
		this.A_Btn_Next.ShowFocusRect = false;
		this.A_Btn_Next.ShowOutline = false;
		this.A_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.A_Btn_Next.SupportThemes = false;
		this.A_Btn_Next.TabIndex = 1;
		this.A_Btn_Next.Text = "確定";
		this.A_Btn_Next.Click += new System.EventHandler(A_Btn_Next_Click);
		this.panel5.BackColor = System.Drawing.Color.White;
		this.panel5.Controls.Add(this.ultraCheckEditor1);
		this.panel5.Controls.Add(this.ultraLabel6);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel5.Location = new System.Drawing.Point(0, 0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(692, 60);
		this.panel5.TabIndex = 13;
		this.ultraCheckEditor1.Checked = true;
		this.ultraCheckEditor1.CheckState = System.Windows.Forms.CheckState.Checked;
		this.ultraCheckEditor1.Location = new System.Drawing.Point(17, 34);
		this.ultraCheckEditor1.Name = "ultraCheckEditor1";
		this.ultraCheckEditor1.Size = new System.Drawing.Size(231, 20);
		this.ultraCheckEditor1.TabIndex = 3;
		this.ultraCheckEditor1.Text = "只顯示未核可項目";
		this.ultraCheckEditor1.AfterCheckStateChanged += new Infragistics.Win.CheckEditor.AfterCheckStateChangedHandler(ultraCheckEditor1_AfterCheckStateChanged);
		appearance3.BackColor = System.Drawing.Color.White;
		this.ultraLabel6.Appearance = appearance3;
		this.ultraLabel6.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel6.Location = new System.Drawing.Point(16, 12);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel6.TabIndex = 2;
		this.ultraLabel6.Text = "請使用勾選來變更工項的核可狀態";
		appearance4.FontData.SizeInPoints = 11f;
		this.ultraStatusBar1.Appearance = appearance4;
		this.ultraStatusBar1.Location = new System.Drawing.Point(0, 483);
		this.ultraStatusBar1.Name = "ultraStatusBar1";
		ultraStatusPanel1.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		appearance5.BackColor = System.Drawing.Color.FromArgb(192, 192, 255);
		appearance5.BackColor2 = System.Drawing.Color.Navy;
		appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
		ultraStatusPanel1.ProgressBarInfo.Appearance = appearance5;
		ultraStatusPanel1.Text = "資料筆數：";
		ultraStatusPanel1.Width = 200;
		ultraStatusPanel2.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		appearance6.BackColor = System.Drawing.Color.LightSlateGray;
		appearance6.BackColor2 = System.Drawing.Color.DarkBlue;
		appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
		ultraStatusPanel2.ProgressBarInfo.FillAppearance = appearance6;
		ultraStatusPanel2.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		ultraStatusPanel2.Style = Infragistics.Win.UltraWinStatusBar.PanelStyle.Progress;
		ultraStatusPanel2.Width = 0;
		appearance7.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance7.TextVAlign = Infragistics.Win.VAlign.Bottom;
		ultraStatusPanel3.Appearance = appearance7;
		ultraStatusPanel3.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel3.Text = "客服電話:(02)2716-5561";
		ultraStatusPanel3.Width = 200;
		this.ultraStatusBar1.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[3] { ultraStatusPanel1, ultraStatusPanel2, ultraStatusPanel3 });
		this.ultraStatusBar1.Size = new System.Drawing.Size(692, 26);
		this.ultraStatusBar1.SupportThemes = false;
		this.ultraStatusBar1.TabIndex = 14;
		this.ultraStatusBar1.Text = "ultraStatusBar1";
		this.gridMrsBase1._ExcelFileName = "";
		this.gridMrsBase1._ExcelSheeName = "";
		this.gridMrsBase1._IsOpenExcelAfterExport = false;
		this.gridMrsBase1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
		this.gridMrsBase1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridMrsBase1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.gridMrsBase1.ColumnInfo = "21,1,0,0,0,110,Columns:0{Width:17;Name:\"RowIndicator\";AllowDragging:False;AllowResizing:False;AllowEditing:False;DataType:System.Int32;TextAlign:RightTop;TextAlignFixed:GeneralTop;}\t1{Width:90;Name:\"PostMode\";Caption:\"核可狀態\";DataType:System.Boolean;TextAlign:GeneralBottom;TextAlignFixed:GeneralTop;ImageAlign:CenterCenter;}\t2{Width:70;Name:\"OrigPostMode\";Caption:\"原始狀態\";DataType:System.Boolean;TextAlign:GeneralBottom;TextAlignFixed:GeneralTop;ImageAlign:CenterCenter;}\t3{Width:100;Name:\"PccesCode\";Caption:\"工項代碼\";AllowEditing:False;DataType:System.String;TextAlign:LeftBottom;TextAlignFixed:GeneralTop;}\t4{Width:160;Name:\"CName\";Caption:\"工項名稱\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t5{Width:61;Name:\"UnitName\";Caption:\"單位\";AllowEditing:False;DataType:System.String;TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t6{Width:40;Name:\"AnaImg\";Caption:\"分析\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;ImageAlign:CenterCenter;}\t7{Width:100;Name:\"Cost\";Caption:\"單價\";AllowEditing:False;DataType:System.Decimal;Format:\"###,###,###,##0\";TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t8{Width:60;Name:\"Rate\";Caption:\"百分比\";AllowEditing:False;DataType:System.Decimal;Format:\"###,###,###,##0\";TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t9{Width:40;Name:\"CostKind\";Caption:\"種類\";AllowEditing:False;DataType:System.String;TextAlign:LeftBottom;TextAlignFixed:GeneralTop;}\t10{Width:85;Name:\"LRate\";Caption:\"人工(%)\";AllowEditing:False;DataType:System.Decimal;TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t11{Width:85;Name:\"ERate\";Caption:\"機具(%)\";AllowEditing:False;DataType:System.Decimal;TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t12{Width:85;Name:\"MRate\";Caption:\"材料(%)\";AllowEditing:False;DataType:System.Decimal;TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t13{Width:85;Name:\"WRate\";Caption:\"雜項(%)\";AllowEditing:False;DataType:System.Decimal;TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t14{Width:45;Name:\"XNameC\";Caption:\"區域\";AllowEditing:False;DataType:System.String;TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t15{Width:190;Name:\"Memo\";Caption:\"備註\";AllowEditing:False;DataType:System.String;TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t16{Name:\"PubCode\";Caption:\"PubCode\";AllowEditing:False;DataType:System.Int32;TextAlign:RightBottom;TextAlignFixed:GeneralTop;}\t17{Width:200;Name:\"EName\";Caption:\"Description\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t18{Name:\"EUnit\";Caption:\"Unit\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t19{Width:37;Name:\"Analysis\";Caption:\"分析\";AllowEditing:False;DataType:System.Boolean;TextAlign:LeftBottom;TextAlignFixed:GeneralTop;ImageAlign:CenterCenter;}\t20{Name:\"Show\";Caption:\"Show\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t";
		this.gridMrsBase1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridMrsBase1.ExtendLastCol = true;
		this.gridMrsBase1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridMrsBase1.ForeColor = System.Drawing.Color.Black;
		this.gridMrsBase1.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.gridMrsBase1.IsProcessUndo = true;
		this.gridMrsBase1.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.gridMrsBase1.Location = new System.Drawing.Point(0, 60);
		this.gridMrsBase1.Name = "gridMrsBase1";
		this.gridMrsBase1.Rows.Count = 1;
		this.gridMrsBase1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.RowRange;
		this.gridMrsBase1.ShowCursor = true;
		this.gridMrsBase1.ShowToolTipOnNarrowColumn = true;
		this.gridMrsBase1.Size = new System.Drawing.Size(692, 423);
		this.gridMrsBase1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection("Normal{Font:細明體, 11.25pt;BackColor:237, 243, 254;ForeColor:Black;TextAlign:GeneralBottom;Border:Flat,1,Silver,Both;}\tAlternate{BackColor:White;}\tFixed{BackColor:225, 247, 223;TextAlign:GeneralTop;Border:Raised,1,Black,Both;}\tHighlight{BackColor:102, 153, 255;TextAlign:GeneralCenter;ImageAlign:CenterCenter;Border:None,1,Black,Both;}\tFocus{Font:細明體, 10pt, style=Bold;BackColor:White;Margins:0, 0, 0, 0;TextAlign:GeneralCenter;Border:Double,1,96, 145, 234,Both;}\tSearch{BackColor:Highlight;ForeColor:HighlightText;}\tFrozen{BackColor:Beige;}\tEmptyArea{BackColor:232, 232, 232;Border:Flat,1,ControlDarkDark,Both;}\tGrandTotal{BackColor:Black;ForeColor:White;}\tSubtotal0{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal1{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal2{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal3{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal4{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal5{BackColor:ControlDarkDark;ForeColor:White;}\t");
		this.gridMrsBase1.TabIndex = 15;
		this.gridMrsBase1.UndoMax = 2;
		this.imageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
		this.imageList2.ImageSize = new System.Drawing.Size(16, 16);
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.White;
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		base.CancelButton = this.A_Btn_Cncl;
		base.ClientSize = new System.Drawing.Size(692, 553);
		base.Controls.Add(this.gridMrsBase1);
		base.Controls.Add(this.ultraStatusBar1);
		base.Controls.Add(this.panel5);
		base.Controls.Add(this.panel1);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.Name = "FormMrsBaseApprove";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "工項核可";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormMrsBaseApprove_KeyDown);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormMrsBaseApprove_FormClosing);
		base.Load += new System.EventHandler(FormMrsBaseApprove_Load);
		base.Activated += new System.EventHandler(FormMrsBaseApprove_Activated);
		this.panel1.ResumeLayout(false);
		this.panel5.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridMrsBase1).EndInit();
		base.ResumeLayout(false);
	}

	private void FormMrsBaseApprove_Load(object sender, EventArgs e)
	{
		FORM_STATUS = FormStatus.Load;
		SettingDecimal();
		HideCols(IsHide: true);
		LoadingScreen();
	}

	private void LoadingScreen()
	{
		string Status = CommonMethods.GetIniValue("MrsBaseApprove", "WindowState");
		if (Status == "Maximized")
		{
			base.WindowState = FormWindowState.Maximized;
		}
		int iLoc_X = PubTools.Str2Int(CommonMethods.GetIniValue("MrsBaseApprove", "PK_LocationX"));
		int iLoc_Y = PubTools.Str2Int(CommonMethods.GetIniValue("MrsBaseApprove", "PK_LocationY"));
		int iSiz_W = PubTools.Str2Int(CommonMethods.GetIniValue("MrsBaseApprove", "PK_Width"));
		int iSiz_H = PubTools.Str2Int(CommonMethods.GetIniValue("MrsBaseApprove", "PK_Height"));
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
	}

	private void LoadData()
	{
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("WinFORM 基本工料");
		dbMrsBase = new MrsBaseA(F_UserID, aArr);
		dbMrsBase.ps_srckind = "MRS";
		dbMrsBase.ps_projectcode = "";
		if (ExtraCri.Trim() == "")
		{
			DT1 = dbMrsBase.ListItem();
		}
		else
		{
			DT1 = dbMrsBase.ListItem(ExtraCri);
		}
	}

	private void Th_BindGrid(string sCri)
	{
		ExtraCri = sCri;
		FORM_STATUS = FormStatus.Binding;
		BindToGrid();
		FORM_STATUS = FormStatus.Normal;
	}

	private void BindToGrid()
	{
		FORM_STATUS = FormStatus.Binding;
		ultraCheckEditor1.Enabled = false;
		gridMrsBase1.Enabled = false;
		gridMrsBase1.Redraw = false;
		Cursor = Cursors.WaitCursor;
		int iRowNow = gridMrsBase1.Row;
		Application.DoEvents();
		if (ExtraCri != "[PARENT]")
		{
			gridMrsBase1.Enabled = false;
			LoadData();
			gridMrsBase1.Enabled = true;
			Application.DoEvents();
		}
		RememberColsProps();
		DataView DV1 = DT1.DefaultView;
		CellStyle CS0 = gridMrsBase1.Styles.Add("Black");
		CellStyle CS1 = gridMrsBase1.Styles.Add("AnalysisColor");
		CellStyle CS2 = gridMrsBase1.Styles.Add("LEMColor");
		CellStyle CS3 = gridMrsBase1.Styles.Add("WColor");
		CellStyle CS4 = gridMrsBase1.Styles.Add("ZColor");
		CellStyle CS5 = gridMrsBase1.Styles.Add("DollarColor");
		CellStyle CS6 = gridMrsBase1.Styles.Add("PercentColor");
		CS0.ForeColor = Color.Black;
		CS1.ForeColor = Color.Red;
		CS2.ForeColor = Color.Teal;
		CS3.ForeColor = Color.Purple;
		CS4.ForeColor = Color.Teal;
		CS4.BackColor = Color.LemonChiffon;
		CS5.ForeColor = Color.Green;
		CS6.ForeColor = Color.Blue;
		gridMrsBase1.Clear(ClearFlags.All);
		gridMrsBase1.Select();
		gridMrsBase1.Rows.Count = DV1.Count + 1;
		SetGridColumn();
		ultraStatusBar1.Panels[0].Text = "資料筆數：" + DV1.Count;
		ultraStatusBar1.Panels[1].ProgressBarInfo.Minimum = 0;
		ultraStatusBar1.Panels[1].ProgressBarInfo.Maximum = DV1.Count;
		ultraStatusBar1.Panels[1].ProgressBarInfo.Value = 0;
		ultraStatusBar1.Panels[1].ProgressBarInfo.ShowLabel = true;
		gridMrsBase1.Redraw = true;
		string sItemClass = "";
		string sCostKind = "";
		for (int i = 0; i < DV1.Count; i++)
		{
			sItemClass = ((DV1[i]["pccesCode"].ToString().Length > 0) ? DV1[i]["pccesCode"].ToString().Substring(0, 1) : "");
			sCostKind = ((DV1[i]["costKind"].ToString().Length > 0) ? DV1[i]["costKind"].ToString().Substring(0, 1) : "");
			gridMrsBase1[i + 1, "PccesCode"] = DV1[i]["pccesCode"].ToString().Trim();
			if (sItemClass == "L" || sItemClass == "E" || sItemClass == "M")
			{
				gridMrsBase1.Rows[i + 1].Style = gridMrsBase1.Styles["LEMColor"];
			}
			else if (sItemClass == "W")
			{
				gridMrsBase1.Rows[i + 1].Style = gridMrsBase1.Styles["WColor"];
			}
			switch (sCostKind)
			{
			case "$":
				gridMrsBase1.Rows[i + 1].Style = gridMrsBase1.Styles["DollarColor"];
				break;
			case "%":
				gridMrsBase1.Rows[i + 1].Style = gridMrsBase1.Styles["PercentColor"];
				break;
			default:
				if (!(sCostKind == "#"))
				{
					break;
				}
				goto case "Z";
			case "Z":
				gridMrsBase1.Rows[i + 1].Style = gridMrsBase1.Styles["ZColor"];
				break;
			}
			gridMrsBase1[i + 1, "CName"] = DV1[i]["cName"].ToString().Trim();
			if (DV1[i]["analysis"].ToString().Trim() == "1")
			{
				gridMrsBase1[i + 1, "Analysis"] = true;
				gridMrsBase1.Rows[i + 1].Style = gridMrsBase1.Styles["AnalysisColor"];
				CellRange rg = gridMrsBase1.GetCellRange(i + 1, gridMrsBase1.Cols["AnaImg"].SafeIndex);
				rg.Style = gridMrsBase1.Styles["img"];
				rg.Style.ImageAlign = ImageAlignEnum.CenterCenter;
				rg.Image = imageList2.Images[0];
			}
			else
			{
				gridMrsBase1[i + 1, "Analysis"] = false;
			}
			if (DV1[i]["Post"] != DBNull.Value)
			{
				gridMrsBase1[i + 1, "PostMode"] = DV1[i]["Post"].ToString() == "1";
				gridMrsBase1[i + 1, "OrigPostMode"] = DV1[i]["Post"].ToString() == "1";
			}
			else
			{
				gridMrsBase1[i + 1, "PostMode"] = false;
				gridMrsBase1[i + 1, "OrigPostMode"] = false;
			}
			gridMrsBase1[i + 1, "UnitName"] = DV1[i]["unitName"].ToString().Trim();
			gridMrsBase1[i + 1, "Rate"] = DV1[i]["rate"];
			gridMrsBase1[i + 1, "CostKind"] = DV1[i]["costKind"].ToString().Trim();
			gridMrsBase1[i + 1, "LRate"] = DV1[i]["lRate"];
			gridMrsBase1[i + 1, "ERate"] = DV1[i]["eRate"];
			gridMrsBase1[i + 1, "MRate"] = DV1[i]["mRate"];
			gridMrsBase1[i + 1, "WRate"] = DV1[i]["wRate"];
			gridMrsBase1[i + 1, "XNameC"] = DV1[i]["xNameC"].ToString().Trim();
			gridMrsBase1[i + 1, "Memo"] = DV1[i]["memo"].ToString().Trim();
			gridMrsBase1[i + 1, "PubCode"] = DV1[i]["pubCode"];
			gridMrsBase1[i + 1, "Cost"] = DV1[i]["cost"];
			gridMrsBase1[i + 1, "Show"] = DV1[i]["show"].ToString().Trim();
			gridMrsBase1[i + 1, "EName"] = DV1[i]["eName"].ToString().Trim();
			gridMrsBase1[i + 1, "EUnit"] = DV1[i]["eUnit"].ToString().Trim();
			if (DV1.Count / 5 > 0 && (i % (DV1.Count / 5) == 0 || i == DV1.Count - 1))
			{
				gridMrsBase1.Redraw = !gridMrsBase1.Redraw;
				ultraStatusBar1.Panels[1].ProgressBarInfo.Value = i + 1;
				Application.DoEvents();
				Cursor = Cursors.AppStarting;
			}
		}
		gridMrsBase1.Redraw = true;
		SetColsEditSymbol();
		ultraStatusBar1.Panels[1].ProgressBarInfo.Value = 0;
		ultraStatusBar1.Panels[1].ProgressBarInfo.ShowLabel = false;
		ultraCheckEditor1.Enabled = true;
		gridMrsBase1.Enabled = true;
		Cursor = Cursors.Default;
		FORM_STATUS = FormStatus.Normal;
	}

	private void SetColsEditSymbol()
	{
		for (int i = 1; i < gridMrsBase1.Cols.Count; i++)
		{
			if (gridMrsBase1.Cols[i].AllowEditing)
			{
				CellRange rg = gridMrsBase1.GetCellRange(0, i);
				rg.Style = gridMrsBase1.Styles["EditMode"];
				rg.Image = imageList2.Images[2];
			}
		}
	}

	private void RememberColsProps()
	{
		for (int i = 0; i < GridCols; i++)
		{
			GridColsSquence[i, 0] = gridMrsBase1.Cols[i].Name;
			GridColsSquence[i, 1] = gridMrsBase1.Cols[i].Caption;
			GridColsSquence[i, 2] = gridMrsBase1.Cols[i].Width;
			GridColsSquence[i, 3] = gridMrsBase1.Cols[i].DataType;
			GridColsSquence[i, 4] = gridMrsBase1.Cols[i].Visible;
			GridColsSquence[i, 5] = gridMrsBase1.Cols[i].Format;
			GridColsSquence[i, 6] = gridMrsBase1.Cols[i].AllowEditing;
			if (gridMrsBase1.Cols[i].Name == "Cost")
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
			GridColsSquence[i, 7] = gridMrsBase1.Cols[i].TextAlign;
			GridColsSquence[i, 8] = gridMrsBase1.Cols[i].AllowDragging;
			GridColsSquence[i, 9] = gridMrsBase1.Cols[i].AllowResizing;
		}
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
			gridMrsBase1.Cols[i].AllowDragging = (bool)GridColsSquence[i, 8];
			gridMrsBase1.Cols[i].AllowResizing = (bool)GridColsSquence[i, 9];
		}
	}

	private void FormMrsBaseApprove_Activated(object sender, EventArgs e)
	{
		if (FORM_STATUS == FormStatus.Load)
		{
			RememberColsProps();
			Th_BindGrid(" Post is null or Post <> '1' OR RTrim(ins_usr) = '' ");
			FORM_STATUS = FormStatus.Active;
		}
	}

	private void ultraCheckEditor1_AfterCheckStateChanged(object sender, EventArgs e)
	{
		if (FORM_STATUS == FormStatus.Normal)
		{
			if (ultraCheckEditor1.Checked)
			{
				Th_BindGrid(" Post is null or Post <> '1' OR RTrim(ins_usr) = '' ");
			}
			else
			{
				Th_BindGrid("");
			}
		}
	}

	private void A_Btn_Next_Click(object sender, EventArgs e)
	{
		for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
		{
			if ((bool)gridMrsBase1[i, "PostMode"] != (bool)gridMrsBase1[i, "OrigPostMode"])
			{
				string ls_Post = (((bool)gridMrsBase1[i, "PostMode"]) ? "1" : "");
				dbMrsBase.SetPost(gridMrsBase1[i, "PccesCode"].ToString(), ls_Post);
			}
		}
		base.DialogResult = DialogResult.OK;
	}

	private void FormMrsBaseApprove_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (base.WindowState != FormWindowState.Maximized)
		{
			CommonMethods.WriteIniValue("MrsBaseApprove", "PK_LocationX", base.Location.X.ToString());
			CommonMethods.WriteIniValue("MrsBaseApprove", "PK_LocationY", base.Location.Y.ToString());
			CommonMethods.WriteIniValue("MrsBaseApprove", "PK_Width", base.Size.Width.ToString());
			CommonMethods.WriteIniValue("MrsBaseApprove", "PK_Height", base.Size.Height.ToString());
		}
		CommonMethods.WriteIniValue("MrsBaseApprove", "WindowState", base.WindowState.ToString());
	}

	private void FormMrsBaseApprove_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormMrsBaseApprove");
		}
	}
}
