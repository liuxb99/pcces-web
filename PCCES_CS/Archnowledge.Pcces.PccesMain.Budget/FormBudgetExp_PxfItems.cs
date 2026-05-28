using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.STDClass;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinStatusBar;
using Infragistics.Win.UltraWinTabControl;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormBudgetExp_PxfItems : Form
{
	private const string CallFormHelp = "FormBudgetExp_PxfItems";

	private Panel panel4;

	private GroupBox groupBox2;

	private UltraButton B_Btn_Ok;

	private Panel panel2;

	private IContainer components;

	private UltraLabel ultraLabel7;

	private UltraLabel ultraLabel6;

	private Panel panel3;

	private UltraTabControl Tab_Ctrl;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl Tab_A;

	private UltraTabPageControl Tab_B;

	private UltraStatusBar ultraStatusBar1;

	private UltraStatusBar ultraStatusBar2;

	private Panel panel5;

	private Panel panel6;

	public GridMrsBase Grid1;

	public GridMrsBase Grid2;

	private string F_ProjectCode = "";

	private PccesFormAction F_ActionName;

	private string F_UserID = "";

	private DataTable DT1 = new DataTable();

	private Panel panel1;

	private Panel panel7;

	private GroupBox groupBox1;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	private UltraLabel ultraLabel3;

	private UltraLabel ultraLabel4;

	private UltraLabel ultraLabel5;

	private UltraLabel ultraLabel8;

	private UltraLabel ultraLabel9;

	private GroupBox groupBox3;

	private UltraLabel ultraLabel10;

	private UltraLabel ultraLabel13;

	private UltraLabel ultraLabel14;

	private UltraLabel ultraLabel15;

	private UltraLabel ultraLabel16;

	private UltraLabel ultraLabel11;

	private UltraButton ultraButton1;

	private UltraLabel ultraLabel12;

	private UltraLabel ultraLabel17;

	private DataTable DT2 = new DataTable();

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

	public FormBudgetExp_PxfItems()
	{
		InitializeComponent();
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
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel3 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel4 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel5 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel6 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormBudgetExp_PxfItems));
		Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
		this.Tab_A = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.Grid1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.panel5 = new System.Windows.Forms.Panel();
		this.ultraStatusBar1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.panel1 = new System.Windows.Forms.Panel();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.ultraLabel17 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel9 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel8 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_B = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.Grid2 = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.panel6 = new System.Windows.Forms.Panel();
		this.ultraStatusBar2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.panel7 = new System.Windows.Forms.Panel();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.ultraLabel12 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel11 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel10 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel13 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel14 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel15 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel16 = new Infragistics.Win.Misc.UltraLabel();
		this.panel4 = new System.Windows.Forms.Panel();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.B_Btn_Ok = new Infragistics.Win.Misc.UltraButton();
		this.panel2 = new System.Windows.Forms.Panel();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.panel3 = new System.Windows.Forms.Panel();
		this.Tab_Ctrl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.Tab_A.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Grid1).BeginInit();
		this.panel1.SuspendLayout();
		this.groupBox1.SuspendLayout();
		this.Tab_B.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Grid2).BeginInit();
		this.panel7.SuspendLayout();
		this.groupBox3.SuspendLayout();
		this.panel4.SuspendLayout();
		this.panel2.SuspendLayout();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).BeginInit();
		this.Tab_Ctrl.SuspendLayout();
		base.SuspendLayout();
		this.Tab_A.Controls.Add(this.Grid1);
		this.Tab_A.Controls.Add(this.panel5);
		this.Tab_A.Controls.Add(this.ultraStatusBar1);
		this.Tab_A.Controls.Add(this.panel1);
		this.Tab_A.Location = new System.Drawing.Point(2, 25);
		this.Tab_A.Name = "Tab_A";
		this.Tab_A.Size = new System.Drawing.Size(588, 439);
		this.Grid1._ExcelFileName = "";
		this.Grid1._ExcelSheeName = "";
		this.Grid1._IsOpenExcelAfterExport = false;
		this.Grid1.AllowEditing = false;
		this.Grid1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
		this.Grid1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.Grid1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.Grid1.ColumnInfo = "8,1,0,0,0,110,Columns:0{Width:17;Name:\"RowIndicator\";AllowDragging:False;AllowResizing:False;AllowEditing:False;DataType:System.Int32;TextAlign:RightTop;TextAlignFixed:GeneralTop;}\t1{Width:80;Name:\"ItemNo\";Caption:\"項次\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t2{Width:200;Name:\"CName\";Caption:\"項目及說明\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t3{Width:90;Name:\"UnitName\";Caption:\"單位\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t4{Width:100;Name:\"EName\";Caption:\"英文名稱\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t5{Width:90;Name:\"EUnit\";Caption:\"英文單位\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t6{Width:150;Name:\"Memo\";Caption:\"備註\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t7{Width:50;Name:\"Level\";Caption:\"階層\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t";
		this.Grid1.Cursor = System.Windows.Forms.Cursors.Hand;
		this.Grid1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Grid1.ExtendLastCol = true;
		this.Grid1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.Grid1.ForeColor = System.Drawing.Color.Black;
		this.Grid1.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.Grid1.IsProcessUndo = false;
		this.Grid1.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.Grid1.Location = new System.Drawing.Point(0, 8);
		this.Grid1.Name = "Grid1";
		this.Grid1.Rows.Count = 1;
		this.Grid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
		this.Grid1.ShowCursor = true;
		this.Grid1.ShowToolTipOnNarrowColumn = true;
		this.Grid1.Size = new System.Drawing.Size(588, 248);
		this.Grid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection("Normal{Font:細明體, 11.25pt;BackColor:237, 243, 254;ForeColor:Black;TextAlign:GeneralBottom;Border:Flat,1,Silver,Both;}\tAlternate{BackColor:White;}\tFixed{BackColor:225, 247, 223;TextAlign:GeneralTop;Border:Raised,1,Black,Both;}\tHighlight{BackColor:102, 153, 255;TextAlign:GeneralCenter;ImageAlign:CenterCenter;Border:None,1,Black,Both;}\tFocus{BackColor:102, 153, 255;Margins:0, 0, 0, 0;TextAlign:GeneralCenter;Border:None,1,Black,Both;}\tSearch{BackColor:Highlight;ForeColor:HighlightText;}\tFrozen{BackColor:Beige;}\tEmptyArea{BackColor:232, 232, 232;Border:Flat,1,ControlDarkDark,Both;}\tGrandTotal{BackColor:Black;ForeColor:White;}\tSubtotal0{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal1{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal2{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal3{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal4{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal5{BackColor:ControlDarkDark;ForeColor:White;}\t");
		this.Grid1.TabIndex = 26;
		this.Grid1.UndoMax = 10;
		this.panel5.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel5.Location = new System.Drawing.Point(0, 0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(588, 8);
		this.panel5.TabIndex = 28;
		appearance1.BackColor = System.Drawing.SystemColors.Control;
		appearance1.FontData.SizeInPoints = 11f;
		this.ultraStatusBar1.Appearance = appearance1;
		this.ultraStatusBar1.Location = new System.Drawing.Point(0, 256);
		this.ultraStatusBar1.Name = "ultraStatusBar1";
		ultraStatusPanel1.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel1.Text = "資料筆數:";
		ultraStatusPanel1.Width = 200;
		ultraStatusPanel2.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel2.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		appearance2.TextHAlign = Infragistics.Win.HAlign.Right;
		ultraStatusPanel3.Appearance = appearance2;
		ultraStatusPanel3.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel3.Text = "客服電話:(02)3789-5219";
		ultraStatusPanel3.Width = 200;
		this.ultraStatusBar1.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[3] { ultraStatusPanel1, ultraStatusPanel2, ultraStatusPanel3 });
		this.ultraStatusBar1.Size = new System.Drawing.Size(588, 23);
		this.ultraStatusBar1.SupportThemes = false;
		this.ultraStatusBar1.TabIndex = 27;
		this.ultraStatusBar1.Text = "ultraStatusBar1";
		this.ultraStatusBar1.Click += new System.EventHandler(ultraStatusBar1_Click);
		this.panel1.BackColor = System.Drawing.Color.White;
		this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.panel1.Controls.Add(this.groupBox1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 279);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(588, 160);
		this.panel1.TabIndex = 29;
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.ultraLabel17);
		this.groupBox1.Controls.Add(this.ultraLabel9);
		this.groupBox1.Controls.Add(this.ultraLabel8);
		this.groupBox1.Controls.Add(this.ultraLabel5);
		this.groupBox1.Controls.Add(this.ultraLabel4);
		this.groupBox1.Controls.Add(this.ultraLabel3);
		this.groupBox1.Controls.Add(this.ultraLabel2);
		this.groupBox1.Controls.Add(this.ultraLabel1);
		this.groupBox1.Location = new System.Drawing.Point(9, 8);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(567, 140);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "詳細表不能匯出 PXF 限制如下";
		appearance3.ForeColor = System.Drawing.Color.Red;
		this.ultraLabel17.Appearance = appearance3;
		this.ultraLabel17.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel17.Location = new System.Drawing.Point(24, 22);
		this.ultraLabel17.Name = "ultraLabel17";
		this.ultraLabel17.Size = new System.Drawing.Size(216, 23);
		this.ultraLabel17.TabIndex = 9;
		this.ultraLabel17.Text = "下列內容長度需小於...";
		appearance4.ForeColor = System.Drawing.Color.Blue;
		this.ultraLabel9.Appearance = appearance4;
		this.ultraLabel9.Location = new System.Drawing.Point(320, 46);
		this.ultraLabel9.Name = "ultraLabel9";
		this.ultraLabel9.Size = new System.Drawing.Size(216, 23);
		this.ultraLabel9.TabIndex = 6;
		this.ultraLabel9.Text = "備註  \u300030 Bytes";
		appearance5.ForeColor = System.Drawing.Color.Green;
		this.ultraLabel8.Appearance = appearance5;
		this.ultraLabel8.Location = new System.Drawing.Point(320, 92);
		this.ultraLabel8.Name = "ultraLabel8";
		this.ultraLabel8.Size = new System.Drawing.Size(220, 23);
		this.ultraLabel8.TabIndex = 5;
		this.ultraLabel8.Text = "工作要項最大階層不可大於 6";
		appearance6.ForeColor = System.Drawing.Color.Green;
		this.ultraLabel5.Appearance = appearance6;
		this.ultraLabel5.Location = new System.Drawing.Point(320, 69);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(220, 23);
		this.ultraLabel5.TabIndex = 4;
		this.ultraLabel5.Text = "主項大類最大階層不可大於 5";
		appearance7.ForeColor = System.Drawing.Color.Blue;
		this.ultraLabel4.Appearance = appearance7;
		this.ultraLabel4.Location = new System.Drawing.Point(40, 115);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(216, 23);
		this.ultraLabel4.TabIndex = 3;
		this.ultraLabel4.Text = "英文單位          4 Bytes";
		appearance8.ForeColor = System.Drawing.Color.Blue;
		this.ultraLabel3.Appearance = appearance8;
		this.ultraLabel3.Location = new System.Drawing.Point(40, 92);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(216, 23);
		this.ultraLabel3.TabIndex = 2;
		this.ultraLabel3.Text = "英文項目及說明   50 Bytes";
		appearance9.ForeColor = System.Drawing.Color.Blue;
		this.ultraLabel2.Appearance = appearance9;
		this.ultraLabel2.Location = new System.Drawing.Point(40, 69);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(216, 23);
		this.ultraLabel2.TabIndex = 1;
		this.ultraLabel2.Text = "中文單位          4 Bytes";
		appearance10.ForeColor = System.Drawing.Color.Blue;
		this.ultraLabel1.Appearance = appearance10;
		this.ultraLabel1.Location = new System.Drawing.Point(40, 46);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(216, 23);
		this.ultraLabel1.TabIndex = 0;
		this.ultraLabel1.Text = "中文項目及說明  110 Bytes";
		this.Tab_B.Controls.Add(this.Grid2);
		this.Tab_B.Controls.Add(this.panel6);
		this.Tab_B.Controls.Add(this.ultraStatusBar2);
		this.Tab_B.Controls.Add(this.panel7);
		this.Tab_B.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_B.Name = "Tab_B";
		this.Tab_B.Size = new System.Drawing.Size(588, 439);
		this.Grid2._ExcelFileName = "";
		this.Grid2._ExcelSheeName = "";
		this.Grid2._IsOpenExcelAfterExport = false;
		this.Grid2.AllowEditing = false;
		this.Grid2.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
		this.Grid2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.Grid2.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.Grid2.ColumnInfo = "7,1,0,0,0,110,Columns:0{Width:17;Name:\"RowIndicator\";AllowDragging:False;AllowResizing:False;AllowEditing:False;DataType:System.Int32;TextAlign:RightTop;TextAlignFixed:GeneralTop;}\t1{Name:\"pccesCode\";Caption:\"工項代碼\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t2{Width:200;Name:\"CName\";Caption:\"工項名稱\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t3{Width:90;Name:\"UnitName\";Caption:\"單位\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t4{Width:100;Name:\"EName\";Caption:\"英文名稱\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t5{Name:\"EUnit\";Caption:\"英文單位\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t6{Width:200;Name:\"Memo\";Caption:\"備註\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t";
		this.Grid2.Cursor = System.Windows.Forms.Cursors.Hand;
		this.Grid2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Grid2.ExtendLastCol = true;
		this.Grid2.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.Grid2.ForeColor = System.Drawing.Color.Black;
		this.Grid2.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.Grid2.IsProcessUndo = false;
		this.Grid2.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.Grid2.Location = new System.Drawing.Point(0, 8);
		this.Grid2.Name = "Grid2";
		this.Grid2.Rows.Count = 1;
		this.Grid2.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
		this.Grid2.ShowCursor = true;
		this.Grid2.ShowToolTipOnNarrowColumn = true;
		this.Grid2.Size = new System.Drawing.Size(588, 248);
		this.Grid2.Styles = new C1.Win.C1FlexGrid.CellStyleCollection("Normal{Font:細明體, 11.25pt;BackColor:237, 243, 254;ForeColor:Black;TextAlign:GeneralBottom;Border:Flat,1,Silver,Both;}\tAlternate{BackColor:White;}\tFixed{BackColor:225, 247, 223;TextAlign:GeneralTop;Border:Raised,1,Black,Both;}\tHighlight{BackColor:102, 153, 255;TextAlign:GeneralCenter;ImageAlign:CenterCenter;Border:None,1,Black,Both;}\tFocus{BackColor:102, 153, 255;Margins:0, 0, 0, 0;TextAlign:GeneralCenter;Border:None,1,Black,Both;}\tSearch{BackColor:Highlight;ForeColor:HighlightText;}\tFrozen{BackColor:Beige;}\tEmptyArea{BackColor:232, 232, 232;Border:Flat,1,ControlDarkDark,Both;}\tGrandTotal{BackColor:Black;ForeColor:White;}\tSubtotal0{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal1{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal2{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal3{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal4{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal5{BackColor:ControlDarkDark;ForeColor:White;}\t");
		this.Grid2.TabIndex = 28;
		this.Grid2.UndoMax = 10;
		this.panel6.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel6.Location = new System.Drawing.Point(0, 0);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(588, 8);
		this.panel6.TabIndex = 30;
		appearance11.BackColor = System.Drawing.SystemColors.Control;
		appearance11.FontData.SizeInPoints = 11f;
		this.ultraStatusBar2.Appearance = appearance11;
		this.ultraStatusBar2.Location = new System.Drawing.Point(0, 256);
		this.ultraStatusBar2.Name = "ultraStatusBar2";
		this.ultraStatusBar2.Padding = new Infragistics.Win.UltraWinStatusBar.UIElementMargins(0, 2, 0, 0);
		ultraStatusPanel4.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel4.Text = "資料筆數:";
		ultraStatusPanel4.Width = 200;
		ultraStatusPanel5.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel5.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		appearance12.TextHAlign = Infragistics.Win.HAlign.Right;
		ultraStatusPanel6.Appearance = appearance12;
		ultraStatusPanel6.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel6.Text = "客服電話:(02)3789-5219";
		ultraStatusPanel6.Width = 200;
		this.ultraStatusBar2.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[3] { ultraStatusPanel4, ultraStatusPanel5, ultraStatusPanel6 });
		this.ultraStatusBar2.Size = new System.Drawing.Size(588, 23);
		this.ultraStatusBar2.SupportThemes = false;
		this.ultraStatusBar2.TabIndex = 29;
		this.ultraStatusBar2.Text = "ultraStatusBar2";
		this.panel7.BackColor = System.Drawing.Color.White;
		this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.panel7.Controls.Add(this.groupBox3);
		this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel7.Location = new System.Drawing.Point(0, 279);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(588, 160);
		this.panel7.TabIndex = 31;
		this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox3.Controls.Add(this.ultraLabel12);
		this.groupBox3.Controls.Add(this.ultraLabel11);
		this.groupBox3.Controls.Add(this.ultraLabel10);
		this.groupBox3.Controls.Add(this.ultraLabel13);
		this.groupBox3.Controls.Add(this.ultraLabel14);
		this.groupBox3.Controls.Add(this.ultraLabel15);
		this.groupBox3.Controls.Add(this.ultraLabel16);
		this.groupBox3.Location = new System.Drawing.Point(9, 8);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(567, 140);
		this.groupBox3.TabIndex = 1;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "專案工項不能匯出 PXF 限制如下";
		appearance13.ForeColor = System.Drawing.Color.Red;
		this.ultraLabel12.Appearance = appearance13;
		this.ultraLabel12.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel12.Location = new System.Drawing.Point(24, 22);
		this.ultraLabel12.Name = "ultraLabel12";
		this.ultraLabel12.Size = new System.Drawing.Size(216, 23);
		this.ultraLabel12.TabIndex = 8;
		this.ultraLabel12.Text = "下列內容長度需小於...";
		appearance14.ForeColor = System.Drawing.Color.Blue;
		this.ultraLabel11.Appearance = appearance14;
		this.ultraLabel11.Location = new System.Drawing.Point(320, 69);
		this.ultraLabel11.Name = "ultraLabel11";
		this.ultraLabel11.Size = new System.Drawing.Size(216, 23);
		this.ultraLabel11.TabIndex = 7;
		this.ultraLabel11.Text = "工項代碼  \u300011 Bytes";
		appearance15.ForeColor = System.Drawing.Color.Blue;
		this.ultraLabel10.Appearance = appearance15;
		this.ultraLabel10.Location = new System.Drawing.Point(320, 46);
		this.ultraLabel10.Name = "ultraLabel10";
		this.ultraLabel10.Size = new System.Drawing.Size(216, 23);
		this.ultraLabel10.TabIndex = 6;
		this.ultraLabel10.Text = "備註  \u3000    30 Bytes";
		appearance16.ForeColor = System.Drawing.Color.Blue;
		this.ultraLabel13.Appearance = appearance16;
		this.ultraLabel13.Location = new System.Drawing.Point(40, 115);
		this.ultraLabel13.Name = "ultraLabel13";
		this.ultraLabel13.Size = new System.Drawing.Size(216, 23);
		this.ultraLabel13.TabIndex = 3;
		this.ultraLabel13.Text = "英文單位          4 Bytes";
		appearance17.ForeColor = System.Drawing.Color.Blue;
		this.ultraLabel14.Appearance = appearance17;
		this.ultraLabel14.Location = new System.Drawing.Point(40, 92);
		this.ultraLabel14.Name = "ultraLabel14";
		this.ultraLabel14.Size = new System.Drawing.Size(216, 23);
		this.ultraLabel14.TabIndex = 2;
		this.ultraLabel14.Text = "英文項目及說明   50 Bytes";
		appearance18.ForeColor = System.Drawing.Color.Blue;
		this.ultraLabel15.Appearance = appearance18;
		this.ultraLabel15.Location = new System.Drawing.Point(40, 69);
		this.ultraLabel15.Name = "ultraLabel15";
		this.ultraLabel15.Size = new System.Drawing.Size(216, 23);
		this.ultraLabel15.TabIndex = 1;
		this.ultraLabel15.Text = "中文單位          4 Bytes";
		appearance19.ForeColor = System.Drawing.Color.Blue;
		this.ultraLabel16.Appearance = appearance19;
		this.ultraLabel16.Location = new System.Drawing.Point(40, 46);
		this.ultraLabel16.Name = "ultraLabel16";
		this.ultraLabel16.Size = new System.Drawing.Size(216, 23);
		this.ultraLabel16.TabIndex = 0;
		this.ultraLabel16.Text = "中文項目及說明  110 Bytes";
		this.panel4.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel4.Controls.Add(this.ultraButton1);
		this.panel4.Controls.Add(this.groupBox2);
		this.panel4.Controls.Add(this.B_Btn_Ok);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel4.Location = new System.Drawing.Point(0, 522);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(592, 44);
		this.panel4.TabIndex = 12;
		appearance20.Image = resources.GetObject("appearance20.Image");
		this.ultraButton1.Appearance = appearance20;
		this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton1.Location = new System.Drawing.Point(400, 10);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.ShowFocusRect = false;
		this.ultraButton1.ShowOutline = false;
		this.ultraButton1.Size = new System.Drawing.Size(88, 31);
		this.ultraButton1.SupportThemes = false;
		this.ultraButton1.TabIndex = 4;
		this.ultraButton1.Text = "匯出";
		this.ultraButton1.Visible = false;
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(592, 8);
		this.groupBox2.TabIndex = 3;
		this.groupBox2.TabStop = false;
		this.B_Btn_Ok.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance21.Image = resources.GetObject("appearance21.Image");
		appearance21.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.B_Btn_Ok.Appearance = appearance21;
		this.B_Btn_Ok.BackColor = System.Drawing.SystemColors.Control;
		this.B_Btn_Ok.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.B_Btn_Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.B_Btn_Ok.Font = new System.Drawing.Font("細明體", 11f);
		this.B_Btn_Ok.ImageSize = new System.Drawing.Size(20, 20);
		this.B_Btn_Ok.ImageTransparentColor = System.Drawing.Color.White;
		this.B_Btn_Ok.Location = new System.Drawing.Point(490, 10);
		this.B_Btn_Ok.Name = "B_Btn_Ok";
		this.B_Btn_Ok.ShowFocusRect = false;
		this.B_Btn_Ok.ShowOutline = false;
		this.B_Btn_Ok.Size = new System.Drawing.Size(88, 31);
		this.B_Btn_Ok.SupportThemes = false;
		this.B_Btn_Ok.TabIndex = 2;
		this.B_Btn_Ok.Text = "確定";
		this.panel2.BackColor = System.Drawing.Color.White;
		this.panel2.Controls.Add(this.ultraLabel7);
		this.panel2.Controls.Add(this.ultraLabel6);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(592, 56);
		this.panel2.TabIndex = 22;
		appearance22.BackColor = System.Drawing.Color.White;
		this.ultraLabel7.Appearance = appearance22;
		this.ultraLabel7.Location = new System.Drawing.Point(24, 33);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel7.TabIndex = 6;
		this.ultraLabel7.Text = "造成無法轉出 PXF 項目如下";
		appearance23.BackColor = System.Drawing.Color.White;
		this.ultraLabel6.Appearance = appearance23;
		this.ultraLabel6.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel6.Location = new System.Drawing.Point(8, 8);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel6.TabIndex = 5;
		this.ultraLabel6.Text = "無法轉出PXF";
		this.panel3.Controls.Add(this.Tab_Ctrl);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel3.Location = new System.Drawing.Point(0, 56);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(592, 466);
		this.panel3.TabIndex = 23;
		appearance24.BackColor = System.Drawing.Color.FromArgb(153, 204, 255);
		appearance24.BackColor2 = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance24.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		this.Tab_Ctrl.ActiveTabAppearance = appearance24;
		appearance25.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance25.BackColor2 = System.Drawing.Color.FromArgb(102, 153, 255);
		appearance25.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		this.Tab_Ctrl.Appearance = appearance25;
		this.Tab_Ctrl.BackColor = System.Drawing.Color.White;
		appearance26.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance26.BackColor2 = System.Drawing.Color.FromArgb(237, 243, 254);
		this.Tab_Ctrl.ClientAreaAppearance = appearance26;
		this.Tab_Ctrl.Controls.Add(this.ultraTabSharedControlsPage1);
		this.Tab_Ctrl.Controls.Add(this.Tab_A);
		this.Tab_Ctrl.Controls.Add(this.Tab_B);
		this.Tab_Ctrl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Tab_Ctrl.Location = new System.Drawing.Point(0, 0);
		this.Tab_Ctrl.Name = "Tab_Ctrl";
		this.Tab_Ctrl.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.Tab_Ctrl.Size = new System.Drawing.Size(592, 466);
		this.Tab_Ctrl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
		this.Tab_Ctrl.TabIndex = 0;
		ultraTab1.TabPage = this.Tab_A;
		ultraTab1.Text = "詳細表項目";
		appearance27.BackColor = System.Drawing.Color.FromArgb(153, 204, 255);
		appearance27.BackColor2 = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance27.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		appearance27.BorderColor3DBase = System.Drawing.Color.FromArgb(90, 145, 234);
		ultraTab2.ActiveAppearance = appearance27;
		ultraTab2.TabPage = this.Tab_B;
		ultraTab2.Text = " 專案工項 ";
		this.Tab_Ctrl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[2] { ultraTab1, ultraTab2 });
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(588, 439);
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		base.ClientSize = new System.Drawing.Size(592, 566);
		base.Controls.Add(this.panel3);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel4);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.Name = "FormBudgetExp_PxfItems";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "無法轉出PXF格式的項目";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormBudgetExp_PxfItems_KeyDown);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormBudgetExp_PxfItems_FormClosing);
		base.Load += new System.EventHandler(FormBudgetExp_PxfItems_Load);
		this.Tab_A.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Grid1).EndInit();
		this.panel1.ResumeLayout(false);
		this.groupBox1.ResumeLayout(false);
		this.Tab_B.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Grid2).EndInit();
		this.panel7.ResumeLayout(false);
		this.groupBox3.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).EndInit();
		this.Tab_Ctrl.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	private void FormBudgetExp_PxfItems_Load(object sender, EventArgs e)
	{
		GetCannotExpItem();
		BindToGrid();
		LoadingScreen();
	}

	private void LoadingScreen()
	{
		string Status = CommonMethods.GetIniValue("Exp_PxfItem", "WindowState");
		if (Status == "Maximized")
		{
			base.WindowState = FormWindowState.Maximized;
		}
		int iLoc_X = PubTools.Str2Int(CommonMethods.GetIniValue("Exp_PxfItem", "PK_LocationX"));
		int iLoc_Y = PubTools.Str2Int(CommonMethods.GetIniValue("Exp_PxfItem", "PK_LocationY"));
		int iSiz_W = PubTools.Str2Int(CommonMethods.GetIniValue("Exp_PxfItem", "PK_Width"));
		int iSiz_H = PubTools.Str2Int(CommonMethods.GetIniValue("Exp_PxfItem", "PK_Height"));
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

	private void BindToGrid()
	{
		Grid1.Rows.Count = DT1.Rows.Count + 1;
		for (int i = 0; i < DT1.Rows.Count; i++)
		{
			Grid1[i + 1, "ItemNo"] = DT1.Rows[i]["itemNo"];
			Grid1[i + 1, "CName"] = DT1.Rows[i]["cName"];
			Grid1[i + 1, "UnitName"] = DT1.Rows[i]["unitName"];
			Grid1[i + 1, "EName"] = DT1.Rows[i]["eName"];
			Grid1[i + 1, "eUnit"] = DT1.Rows[i]["eUnit"];
			Grid1[i + 1, "Memo"] = DT1.Rows[i]["memo"];
			Grid1[i + 1, "Level"] = DT1.Rows[i]["printNo"].ToString().Trim().Length / 4;
		}
		ultraStatusBar1.Panels[0].Text = "資料筆數:" + DT1.Rows.Count;
		Tab_A.Tab.Text = "詳細表項目(" + DT1.Rows.Count + ")";
		Grid2.Rows.Count = DT2.Rows.Count + 1;
		for (int i = 0; i < DT2.Rows.Count; i++)
		{
			Grid2[i + 1, "PccesCode"] = DT2.Rows[i]["pccesCode"];
			Grid2[i + 1, "CName"] = DT2.Rows[i]["cName"];
			Grid2[i + 1, "UnitName"] = DT2.Rows[i]["unitName"];
			Grid2[i + 1, "EName"] = DT2.Rows[i]["eName"];
			Grid2[i + 1, "eUnit"] = DT2.Rows[i]["eUnit"];
			Grid2[i + 1, "Memo"] = DT2.Rows[i]["memo"];
		}
		ultraStatusBar2.Panels[0].Text = "資料筆數:" + DT2.Rows.Count;
		Tab_B.Tab.Text = "專案工項(" + DT2.Rows.Count + ")";
	}

	private void GetCannotExpItem()
	{
		ArrayList lal_LogData = new ArrayList();
		lal_LogData.Add(F_UserID);
		lal_LogData.Add("抓出不能匯出PXF的項目");
		ModifyDB stdcom = new ModifyDB(F_ProjectCode, lal_LogData);
		string ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		string ls_Select = "select * from ";
		ls_Select = ((!(ps_srckind.ToUpper() == "BID")) ? (ls_Select + " BudItemA ") : (ls_Select + " BidItemA "));
		string text = ls_Select;
		ls_Select = text + " where (DataLength(Rtrim(Cname))>110 or DataLength(Rtrim(Ename))>50  or ((DataLength(Rtrim(PrintNo))>24 and rtrim(PrintNo) != " + "".PadLeft(32, '9') + ") or(DataLength(Rtrim(PrintNo))>20 and Upper(kind) != 'W' and rtrim(PrintNo) != " + "".PadLeft(32, '9') + " ))  or DataLength(Rtrim(Memo))>30 or DataLength(Rtrim(UnitName))>4 or DataLength(Rtrim(Eunit))>4 ) and ProjectCode = '" + F_ProjectCode + "' ";
		DT1 = stdcom.DBList(ls_Select);
		DT1.TableName = "ITEMA";
		ls_Select = "select * from ";
		ls_Select = ((!(ps_srckind.ToUpper() == "BID")) ? (ls_Select + " BudProjMrsA ") : (ls_Select + " BidProjMrsA "));
		ls_Select = ls_Select + " where (DataLength(Rtrim(Cname))>110 or DataLength(Rtrim(Ename))>50  or (DataLength(Rtrim(PccesCode)) > 11)  or DataLength(Rtrim(Memo))>30 or DataLength(Rtrim(UnitName))>4 or DataLength(Rtrim(Eunit))>4 ) and ProjectCode = '" + F_ProjectCode + "' ";
		DT2 = stdcom.DBList(ls_Select);
		DT2.TableName = "MRSA";
	}

	private void ultraStatusBar1_Click(object sender, EventArgs e)
	{
	}

	private void FormBudgetExp_PxfItems_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (base.WindowState != FormWindowState.Maximized)
		{
			CommonMethods.WriteIniValue("Exp_PxfItem", "PK_LocationX", base.Location.X.ToString());
			CommonMethods.WriteIniValue("Exp_PxfItem", "PK_LocationY", base.Location.Y.ToString());
			CommonMethods.WriteIniValue("Exp_PxfItem", "PK_Width", base.Size.Width.ToString());
			CommonMethods.WriteIniValue("Exp_PxfItem", "PK_Height", base.Size.Height.ToString());
		}
		CommonMethods.WriteIniValue("Exp_PxfItem", "WindowState", base.WindowState.ToString());
	}

	private void FormBudgetExp_PxfItems_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormBudgetExp_PxfItems");
		}
	}
}
