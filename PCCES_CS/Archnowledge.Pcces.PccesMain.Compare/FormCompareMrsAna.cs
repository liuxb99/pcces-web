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
using Infragistics.Win.UltraWinStatusBar;

namespace Archnowledge.Pcces.PccesMain.Compare;

public class FormCompareMrsAna : Form
{
	private const string CallFormHelp = "FormCompareMrsAna";

	private Panel panel6;

	private GroupBox groupBox3;

	private UltraButton D_Btn_Fnsh;

	private Panel panel1;

	private Panel panel2;

	private Panel panel3;

	private Splitter splitter1;

	private Panel panel4;

	private UltraStatusBar ultraStatusBar1;

	private UltraStatusBar ultraStatusBar2;

	public GridMrsBase gridMrsBase1;

	public GridMrsBase gridMrsBase2;

	private UltraLabel lblCName;

	private Label label2;

	private UltraLabel lblPccesCode;

	private Label label1;

	private UltraLabel lblUnit;

	private Label label12;

	private ImageList imageList2;

	private IContainer components;

	private string sAnalysisQty1 = "0";

	private string sAnalysisQty2 = "0";

	private PccesFormAction F_ActionName;

	private string F_ProjectCode1;

	private string F_ProjectCode2;

	private string F_PccesCode;

	private string F_UserID;

	private DataTable DT_Lower1 = new DataTable();

	private DataTable DT_Lower2 = new DataTable();

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

	public string _ProjectCode1
	{
		get
		{
			return F_ProjectCode1;
		}
		set
		{
			F_ProjectCode1 = value;
		}
	}

	public string _ProjectCode2
	{
		get
		{
			return F_ProjectCode2;
		}
		set
		{
			F_ProjectCode2 = value;
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

	public FormCompareMrsAna()
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
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.Compare.FormCompareMrsAna));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel3 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel4 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel5 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel6 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		this.panel6 = new System.Windows.Forms.Panel();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.D_Btn_Fnsh = new Infragistics.Win.Misc.UltraButton();
		this.panel1 = new System.Windows.Forms.Panel();
		this.panel4 = new System.Windows.Forms.Panel();
		this.gridMrsBase2 = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.ultraStatusBar2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.splitter1 = new System.Windows.Forms.Splitter();
		this.panel3 = new System.Windows.Forms.Panel();
		this.gridMrsBase1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.ultraStatusBar1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.panel2 = new System.Windows.Forms.Panel();
		this.lblUnit = new Infragistics.Win.Misc.UltraLabel();
		this.label12 = new System.Windows.Forms.Label();
		this.lblPccesCode = new Infragistics.Win.Misc.UltraLabel();
		this.label1 = new System.Windows.Forms.Label();
		this.lblCName = new Infragistics.Win.Misc.UltraLabel();
		this.label2 = new System.Windows.Forms.Label();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.panel6.SuspendLayout();
		this.panel1.SuspendLayout();
		this.panel4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridMrsBase2).BeginInit();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridMrsBase1).BeginInit();
		this.panel2.SuspendLayout();
		base.SuspendLayout();
		this.panel6.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel6.Controls.Add(this.groupBox3);
		this.panel6.Controls.Add(this.D_Btn_Fnsh);
		this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel6.Location = new System.Drawing.Point(0, 512);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(782, 44);
		this.panel6.TabIndex = 11;
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox3.Location = new System.Drawing.Point(0, 0);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(782, 8);
		this.groupBox3.TabIndex = 3;
		this.groupBox3.TabStop = false;
		this.D_Btn_Fnsh.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Fnsh.Appearance = appearance1;
		this.D_Btn_Fnsh.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Fnsh.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.D_Btn_Fnsh.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.D_Btn_Fnsh.Font = new System.Drawing.Font("細明體", 11f);
		this.D_Btn_Fnsh.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Fnsh.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Fnsh.Location = new System.Drawing.Point(682, 10);
		this.D_Btn_Fnsh.Name = "D_Btn_Fnsh";
		this.D_Btn_Fnsh.ShowFocusRect = false;
		this.D_Btn_Fnsh.ShowOutline = false;
		this.D_Btn_Fnsh.Size = new System.Drawing.Size(88, 31);
		this.D_Btn_Fnsh.SupportThemes = false;
		this.D_Btn_Fnsh.TabIndex = 1;
		this.D_Btn_Fnsh.Text = "確定";
		this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.panel1.Controls.Add(this.panel4);
		this.panel1.Controls.Add(this.splitter1);
		this.panel1.Controls.Add(this.panel3);
		this.panel1.Controls.Add(this.panel2);
		this.panel1.Location = new System.Drawing.Point(8, 8);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(768, 500);
		this.panel1.TabIndex = 12;
		this.panel4.Controls.Add(this.gridMrsBase2);
		this.panel4.Controls.Add(this.ultraStatusBar2);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel4.Location = new System.Drawing.Point(0, 282);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(768, 218);
		this.panel4.TabIndex = 3;
		this.gridMrsBase2._ExcelFileName = "";
		this.gridMrsBase2._ExcelSheeName = "";
		this.gridMrsBase2._IsOpenExcelAfterExport = false;
		this.gridMrsBase2.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
		this.gridMrsBase2.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
		this.gridMrsBase2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridMrsBase2.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.gridMrsBase2.ColumnInfo = "21,1,0,0,0,110,Columns:0{Width:17;Name:\"RowIndicator\";AllowDragging:False;AllowResizing:False;AllowEditing:False;DataType:System.Int32;TextAlign:RightCenter;}\t1{Width:40;Name:\"ListNo\";Caption:\"序號\";AllowEditing:False;DataType:System.Int32;TextAlign:RightCenter;}\t2{Width:180;Name:\"CName\";Caption:\"細項名稱\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;}\t3{Width:75;Name:\"UnitName\";Caption:\"單位\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;ImageAlign:CenterCenter;}\t4{Width:40;Name:\"AnaImg\";Caption:\"分析\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;ImageAlign:CenterCenter;}\t5{Width:100;Name:\"Qty\";Caption:\"數量\";DataType:System.Decimal;}\t6{Width:55;Name:\"Lock\";Caption:\"鎖定\";DataType:System.Boolean;ImageAlign:CenterCenter;}\t7{Width:100;Name:\"Cost\";Caption:\"單價\";DataType:System.Decimal;TextAlign:GeneralTop;TextAlignFixed:GeneralTop;ImageAlign:RightCenter;}\t8{Width:120;Name:\"Amount\";Caption:\"複價\";AllowEditing:False;DataType:System.Decimal;}\t9{Width:100;Name:\"pccesCode\";Caption:\"細項代碼\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;}\t10{Width:60;Name:\"Rate\";Caption:\"百分比\";DataType:System.Decimal;Format:\"##0.00\";TextAlign:RightCenter;}\t11{Width:70;Name:\"CostKind\";Caption:\"工項類別\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;}\t12{Width:75;Name:\"LRate\";Caption:\"人工(%)\";AllowEditing:False;DataType:System.Decimal;}\t13{Width:75;Name:\"ERate\";Caption:\"機具(%)\";AllowEditing:False;DataType:System.Decimal;}\t14{Width:70;Name:\"MRate\";Caption:\"材料(%)\";AllowEditing:False;DataType:System.Decimal;}\t15{Width:70;Name:\"WRate\";Caption:\"雜項(%)\";AllowEditing:False;DataType:System.Decimal;}\t16{Width:200;Name:\"Memo\";Caption:\"細項備註\";DataType:System.String;TextAlign:LeftCenter;}\t17{Width:29;Name:\"PubCode\";Caption:\"PubCode\";AllowEditing:False;DataType:System.Int32;TextAlign:RightCenter;}\t18{Width:40;Name:\"Analysis\";Caption:\"分析\";AllowEditing:False;DataType:System.Boolean;TextAlign:LeftCenter;ImageAlign:CenterCenter;}\t19{Name:\"usrQty\";Caption:\"usrQty\";DataType:System.Decimal;}\t20{Name:\"PSNo\";Caption:\"PSNo\";DataType:System.Int32;TextAlign:RightCenter;}\t";
		this.gridMrsBase2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridMrsBase2.ExtendLastCol = true;
		this.gridMrsBase2.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridMrsBase2.ForeColor = System.Drawing.SystemColors.WindowText;
		this.gridMrsBase2.IsProcessUndo = false;
		this.gridMrsBase2.Location = new System.Drawing.Point(0, 0);
		this.gridMrsBase2.Name = "gridMrsBase2";
		this.gridMrsBase2.Rows.Count = 1;
		this.gridMrsBase2.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.gridMrsBase2.ShowCursor = true;
		this.gridMrsBase2.ShowToolTipOnNarrowColumn = true;
		this.gridMrsBase2.Size = new System.Drawing.Size(768, 192);
		this.gridMrsBase2.Styles = new C1.Win.C1FlexGrid.CellStyleCollection("Normal{Font:細明體, 11pt;BackColor:237, 243, 254;}\tAlternate{BackColor:White;}\tFixed{BackColor:225, 247, 223;ForeColor:ControlText;Border:Flat,1,ControlDark,Both;}\tHighlight{BackColor:102, 153, 255;ForeColor:Black;}\tFocus{Font:細明體, 10pt, style=Bold;BackColor:White;ForeColor:Black;Border:Double,1,96, 145, 234,Both;}\tSearch{BackColor:255, 255, 128;ForeColor:HighlightText;}\tFrozen{BackColor:Beige;}\tEmptyArea{BackColor:232, 232, 232;Border:Flat,1,Transparent,Both;}\tGrandTotal{BackColor:Black;ForeColor:White;}\tSubtotal0{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal1{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal2{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal3{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal4{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal5{BackColor:ControlDarkDark;ForeColor:White;}\t");
		this.gridMrsBase2.TabIndex = 12;
		this.gridMrsBase2.UndoMax = 5;
		appearance2.FontData.SizeInPoints = 11f;
		appearance2.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraStatusBar2.Appearance = appearance2;
		this.ultraStatusBar2.Location = new System.Drawing.Point(0, 192);
		this.ultraStatusBar2.Name = "ultraStatusBar2";
		this.ultraStatusBar2.Padding = new Infragistics.Win.UltraWinStatusBar.UIElementMargins(0, 2, 0, 0);
		appearance3.TextVAlign = Infragistics.Win.VAlign.Bottom;
		ultraStatusPanel1.Appearance = appearance3;
		ultraStatusPanel1.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel1.Key = "RowsCount";
		ultraStatusPanel1.Text = "資料筆數：";
		ultraStatusPanel1.Width = 200;
		ultraStatusPanel2.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel2.Key = "ProgressBar";
		ultraStatusPanel2.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		ultraStatusPanel3.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel3.Key = "AnalysisQty";
		ultraStatusPanel3.Width = 200;
		this.ultraStatusBar2.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[3] { ultraStatusPanel1, ultraStatusPanel2, ultraStatusPanel3 });
		this.ultraStatusBar2.Size = new System.Drawing.Size(768, 26);
		this.ultraStatusBar2.TabIndex = 10;
		this.ultraStatusBar2.Text = "ultraStatusBar2";
		this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
		this.splitter1.Location = new System.Drawing.Point(0, 272);
		this.splitter1.Name = "splitter1";
		this.splitter1.Size = new System.Drawing.Size(768, 10);
		this.splitter1.TabIndex = 2;
		this.splitter1.TabStop = false;
		this.panel3.Controls.Add(this.gridMrsBase1);
		this.panel3.Controls.Add(this.ultraStatusBar1);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel3.Location = new System.Drawing.Point(0, 64);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(768, 208);
		this.panel3.TabIndex = 1;
		this.gridMrsBase1._ExcelFileName = "";
		this.gridMrsBase1._ExcelSheeName = "";
		this.gridMrsBase1._IsOpenExcelAfterExport = false;
		this.gridMrsBase1.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
		this.gridMrsBase1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
		this.gridMrsBase1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridMrsBase1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.gridMrsBase1.ColumnInfo = "21,1,0,0,0,110,Columns:0{Width:17;Name:\"RowIndicator\";AllowDragging:False;AllowResizing:False;AllowEditing:False;DataType:System.Int32;TextAlign:RightCenter;}\t1{Width:40;Name:\"ListNo\";Caption:\"序號\";AllowEditing:False;DataType:System.Int32;TextAlign:RightCenter;}\t2{Width:180;Name:\"CName\";Caption:\"細項名稱\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;}\t3{Width:75;Name:\"UnitName\";Caption:\"單位\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;ImageAlign:CenterCenter;}\t4{Width:40;Name:\"AnaImg\";Caption:\"分析\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;ImageAlign:CenterCenter;}\t5{Width:100;Name:\"Qty\";Caption:\"數量\";DataType:System.Decimal;}\t6{Width:55;Name:\"Lock\";Caption:\"鎖定\";DataType:System.Boolean;ImageAlign:CenterCenter;}\t7{Width:100;Name:\"Cost\";Caption:\"單價\";DataType:System.Decimal;TextAlign:GeneralTop;TextAlignFixed:GeneralTop;ImageAlign:RightCenter;}\t8{Width:120;Name:\"Amount\";Caption:\"複價\";AllowEditing:False;DataType:System.Decimal;}\t9{Width:100;Name:\"pccesCode\";Caption:\"細項代碼\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;}\t10{Width:60;Name:\"Rate\";Caption:\"百分比\";DataType:System.Decimal;Format:\"##0.00\";TextAlign:RightCenter;}\t11{Width:70;Name:\"CostKind\";Caption:\"工項類別\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;}\t12{Width:75;Name:\"LRate\";Caption:\"人工(%)\";AllowEditing:False;DataType:System.Decimal;}\t13{Width:75;Name:\"ERate\";Caption:\"機具(%)\";AllowEditing:False;DataType:System.Decimal;}\t14{Width:70;Name:\"MRate\";Caption:\"材料(%)\";AllowEditing:False;DataType:System.Decimal;}\t15{Width:70;Name:\"WRate\";Caption:\"雜項(%)\";AllowEditing:False;DataType:System.Decimal;}\t16{Width:200;Name:\"Memo\";Caption:\"細項備註\";DataType:System.String;TextAlign:LeftCenter;}\t17{Width:29;Name:\"PubCode\";Caption:\"PubCode\";AllowEditing:False;DataType:System.Int32;TextAlign:RightCenter;}\t18{Width:40;Name:\"Analysis\";Caption:\"分析\";AllowEditing:False;DataType:System.Boolean;TextAlign:LeftCenter;ImageAlign:CenterCenter;}\t19{Name:\"usrQty\";Caption:\"usrQty\";DataType:System.Decimal;}\t20{Name:\"PSNo\";Caption:\"PSNo\";DataType:System.Int32;TextAlign:RightCenter;}\t";
		this.gridMrsBase1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridMrsBase1.ExtendLastCol = true;
		this.gridMrsBase1.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridMrsBase1.ForeColor = System.Drawing.SystemColors.WindowText;
		this.gridMrsBase1.IsProcessUndo = false;
		this.gridMrsBase1.Location = new System.Drawing.Point(0, 0);
		this.gridMrsBase1.Name = "gridMrsBase1";
		this.gridMrsBase1.Rows.Count = 1;
		this.gridMrsBase1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.gridMrsBase1.ShowCursor = true;
		this.gridMrsBase1.ShowToolTipOnNarrowColumn = true;
		this.gridMrsBase1.Size = new System.Drawing.Size(768, 182);
		this.gridMrsBase1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection("Normal{Font:細明體, 11pt;BackColor:237, 243, 254;}\tAlternate{BackColor:White;}\tFixed{BackColor:225, 247, 223;ForeColor:ControlText;Border:Flat,1,ControlDark,Both;}\tHighlight{BackColor:102, 153, 255;ForeColor:Black;}\tFocus{Font:細明體, 10pt, style=Bold;BackColor:White;ForeColor:Black;Border:Double,1,96, 145, 234,Both;}\tSearch{BackColor:255, 255, 128;ForeColor:HighlightText;}\tFrozen{BackColor:Beige;}\tEmptyArea{BackColor:232, 232, 232;Border:Flat,1,Transparent,Both;}\tGrandTotal{BackColor:Black;ForeColor:White;}\tSubtotal0{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal1{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal2{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal3{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal4{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal5{BackColor:ControlDarkDark;ForeColor:White;}\t");
		this.gridMrsBase1.TabIndex = 11;
		this.gridMrsBase1.UndoMax = 5;
		appearance4.FontData.SizeInPoints = 11f;
		appearance4.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraStatusBar1.Appearance = appearance4;
		this.ultraStatusBar1.Location = new System.Drawing.Point(0, 182);
		this.ultraStatusBar1.Name = "ultraStatusBar1";
		appearance5.TextVAlign = Infragistics.Win.VAlign.Bottom;
		ultraStatusPanel4.Appearance = appearance5;
		ultraStatusPanel4.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel4.Key = "RowsCount";
		ultraStatusPanel4.Text = "資料筆數：";
		ultraStatusPanel4.Width = 200;
		ultraStatusPanel5.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel5.Key = "ProgressBar";
		ultraStatusPanel5.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		ultraStatusPanel6.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel6.Key = "AnalysisQty";
		ultraStatusPanel6.Width = 200;
		this.ultraStatusBar1.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[3] { ultraStatusPanel4, ultraStatusPanel5, ultraStatusPanel6 });
		this.ultraStatusBar1.Size = new System.Drawing.Size(768, 26);
		this.ultraStatusBar1.TabIndex = 10;
		this.ultraStatusBar1.Text = "ultraStatusBar1";
		this.panel2.BackColor = System.Drawing.Color.White;
		this.panel2.Controls.Add(this.lblUnit);
		this.panel2.Controls.Add(this.label12);
		this.panel2.Controls.Add(this.lblPccesCode);
		this.panel2.Controls.Add(this.label1);
		this.panel2.Controls.Add(this.lblCName);
		this.panel2.Controls.Add(this.label2);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(768, 64);
		this.panel2.TabIndex = 0;
		appearance6.TextHAlign = Infragistics.Win.HAlign.Left;
		this.lblUnit.Appearance = appearance6;
		this.lblUnit.Location = new System.Drawing.Point(663, 34);
		this.lblUnit.Name = "lblUnit";
		this.lblUnit.Size = new System.Drawing.Size(97, 20);
		this.lblUnit.TabIndex = 24;
		this.lblUnit.Text = "[lblUnit]";
		this.label12.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label12.Location = new System.Drawing.Point(615, 34);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(52, 20);
		this.label12.TabIndex = 23;
		this.label12.Text = "單位：";
		this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.lblPccesCode.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblPccesCode.Location = new System.Drawing.Point(88, 34);
		this.lblPccesCode.Name = "lblPccesCode";
		this.lblPccesCode.Size = new System.Drawing.Size(468, 23);
		this.lblPccesCode.TabIndex = 16;
		this.lblPccesCode.Text = "[lblPccesCode]";
		this.label1.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.label1.Location = new System.Drawing.Point(8, 34);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(82, 23);
		this.label1.TabIndex = 15;
		this.label1.Text = "工項代碼：";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		appearance7.BackColor = System.Drawing.Color.Transparent;
		this.lblCName.Appearance = appearance7;
		this.lblCName.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblCName.Location = new System.Drawing.Point(88, 9);
		this.lblCName.Name = "lblCName";
		this.lblCName.Size = new System.Drawing.Size(672, 23);
		this.lblCName.TabIndex = 14;
		this.lblCName.Text = "[lblCName]";
		this.label2.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.label2.Location = new System.Drawing.Point(8, 9);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(82, 23);
		this.label2.TabIndex = 13;
		this.label2.Text = "工項名稱：";
		this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.imageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
		this.imageList2.ImageSize = new System.Drawing.Size(16, 16);
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.White;
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.CancelButton = this.D_Btn_Fnsh;
		base.ClientSize = new System.Drawing.Size(782, 556);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.panel6);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.Name = "FormCompareMrsAna";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "經費審查比對(單價分析比對)";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormCompareMrsAna_KeyDown);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormCompareMrsAna_FormClosing);
		base.Load += new System.EventHandler(FormCompareMrsAna_Load);
		this.panel6.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridMrsBase2).EndInit();
		this.panel3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridMrsBase1).EndInit();
		this.panel2.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	private void FormCompareMrsAna_Load(object sender, EventArgs e)
	{
		lblCName.Text = "";
		lblPccesCode.Text = "";
		lblUnit.Text = "";
		HideCols(IsHide: true);
		lblPccesCode.Text = F_PccesCode;
		int iPubCode1 = -1;
		int iPubCode2 = -1;
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add("經費審查比對--單價分析");
		MrsBaseA MrsA = new MrsBaseA(F_UserID, aArr);
		MrsA.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		PriceAnalysis PA1 = new PriceAnalysis(aArr);
		PA1.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		MrsA.ps_projectcode = F_ProjectCode1;
		iPubCode1 = MrsA.Get_Pubcode(F_PccesCode);
		DataTable DT_Main1 = MrsA.ListItem(" pubCode =" + iPubCode1);
		if (DT_Main1.Rows.Count > 0)
		{
			sAnalysisQty1 = DT_Main1.Rows[0]["AnalysisQty"].ToString();
		}
		MrsA.ps_projectcode = F_ProjectCode2;
		DataTable DT_Main2 = MrsA.ListItem(" pubCode =" + MrsA.Get_Pubcode(F_PccesCode));
		if (DT_Main2.Rows.Count > 0)
		{
			sAnalysisQty2 = DT_Main2.Rows[0]["AnalysisQty"].ToString();
		}
		if (DT_Main1.Rows.Count > 0)
		{
			lblCName.Text = DT_Main1.Rows[0]["cName"].ToString();
			lblUnit.Text = DT_Main1.Rows[0]["unitName"].ToString();
		}
		else if (DT_Main2.Rows.Count > 0)
		{
			lblCName.Text = DT_Main2.Rows[0]["cName"].ToString();
			lblUnit.Text = DT_Main2.Rows[0]["unitName"].ToString();
		}
		PA1.ps_prjcode = F_ProjectCode1;
		DT_Lower1 = PA1.listAnaly(iPubCode1);
		if (F_ProjectCode2 == "MRS")
		{
			MrsA.ps_srckind = "MRS";
		}
		else
		{
			MrsA.ps_projectcode = F_ProjectCode2;
		}
		iPubCode2 = MrsA.Get_Pubcode(F_PccesCode);
		if (F_ProjectCode2 == "MRS")
		{
			PA1.ps_srckind = "MRS";
			PA1.ps_prjcode = "";
		}
		else
		{
			PA1.ps_prjcode = F_ProjectCode2;
		}
		DT_Lower2 = PA1.listAnaly(iPubCode2);
		BindToGrid1();
		BindToGrid2();
		LoadingScreen();
	}

	private void LoadingScreen()
	{
		string Status = CommonMethods.GetIniValue("CompareMrsAna", "WindowState");
		if (Status == "Maximized")
		{
			base.WindowState = FormWindowState.Maximized;
		}
		int iLoc_X = PubTools.Str2Int(CommonMethods.GetIniValue("CompareMrsAna", "PK_LocationX"));
		int iLoc_Y = PubTools.Str2Int(CommonMethods.GetIniValue("CompareMrsAna", "PK_LocationY"));
		int iSiz_W = PubTools.Str2Int(CommonMethods.GetIniValue("CompareMrsAna", "PK_Width"));
		int iSiz_H = PubTools.Str2Int(CommonMethods.GetIniValue("CompareMrsAna", "PK_Height"));
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

	private void BindToGrid1()
	{
		CellStyle CS1 = gridMrsBase1.Styles.Add("AnalysisColor");
		CS1.ForeColor = Color.Red;
		CellStyle CS2 = gridMrsBase1.Styles.Add("LEMColor");
		CS2.ForeColor = Color.Teal;
		CellStyle CS3 = gridMrsBase1.Styles.Add("WColor");
		CS3.ForeColor = Color.Purple;
		CellStyle CS4 = gridMrsBase1.Styles.Add("ZColor");
		CS4.ForeColor = Color.Teal;
		CS4.BackColor = Color.LemonChiffon;
		CellStyle CSC = gridMrsBase1.Styles.Add("CColor");
		CSC.ForeColor = Color.Black;
		CSC.Font = new Font("細明體", 11f, FontStyle.Bold);
		CellStyle CS5 = gridMrsBase1.Styles.Add("DollarColor");
		CS5.ForeColor = Color.Green;
		CellStyle CS6 = gridMrsBase1.Styles.Add("PercentColor");
		CS6.ForeColor = Color.Blue;
		CellStyle CSM = gridMrsBase1.Styles.Add("Minus");
		CSM.BackColor = Color.FromArgb(255, 80, 80);
		string sItemClass = "";
		string sItemKind = "";
		gridMrsBase1.Rows.Count = DT_Lower1.Rows.Count + 1;
		for (int i = 0; i < DT_Lower1.Rows.Count; i++)
		{
			sItemClass = ((DT_Lower1.Rows[i]["pccesCode"].ToString().Length > 0) ? DT_Lower1.Rows[i]["pccesCode"].ToString().Substring(0, 1) : "");
			sItemKind = ((DT_Lower1.Rows[i]["costKind"] == null) ? "" : ((DT_Lower1.Rows[i]["costKind"].ToString().Length > 0) ? DT_Lower1.Rows[i]["costKind"].ToString().Substring(0, 1) : ""));
			gridMrsBase1[i + 1, "PccesCode"] = DT_Lower1.Rows[i]["pccesCode"].ToString();
			if (sItemClass == "L" || sItemClass == "E" || sItemClass == "M")
			{
				gridMrsBase1.Rows[i + 1].Style = gridMrsBase1.Styles["LEMColor"];
			}
			else if (sItemClass == "W")
			{
				gridMrsBase1.Rows[i + 1].Style = gridMrsBase1.Styles["WColor"];
			}
			switch (sItemKind)
			{
			case "$":
				gridMrsBase1.Rows[i + 1].Style = gridMrsBase1.Styles["DollarColor"];
				break;
			case "%":
				gridMrsBase1.Rows[i + 1].Style = gridMrsBase1.Styles["PercentColor"];
				break;
			case "Z":
				gridMrsBase1.Rows[i + 1].Style = gridMrsBase1.Styles["ZColor"];
				break;
			case "#":
				gridMrsBase1.Rows[i + 1].Style = gridMrsBase1.Styles["CColor"];
				break;
			}
			gridMrsBase1[i + 1, "CName"] = DT_Lower1.Rows[i]["cName"].ToString();
			if (DT_Lower1.Rows[i]["analysis"].ToString().Trim() == "1")
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
			gridMrsBase1[i + 1, "ListNo"] = DT_Lower1.Rows[i]["listNo"];
			gridMrsBase1[i + 1, "UnitName"] = DT_Lower1.Rows[i]["unitName"];
			gridMrsBase1[i + 1, "CostKind"] = DT_Lower1.Rows[i]["costKind"];
			gridMrsBase1[i + 1, "PubCode"] = DT_Lower1.Rows[i]["pubCode"];
			gridMrsBase1[i + 1, "Memo"] = DT_Lower1.Rows[i]["memo"];
			if (sItemKind != "#")
			{
				gridMrsBase1[i + 1, "Rate"] = DT_Lower1.Rows[i]["rate"];
				gridMrsBase1[i + 1, "LRate"] = DT_Lower1.Rows[i]["lRate"];
				gridMrsBase1[i + 1, "ERate"] = DT_Lower1.Rows[i]["eRate"];
				gridMrsBase1[i + 1, "MRate"] = DT_Lower1.Rows[i]["mRate"];
				gridMrsBase1[i + 1, "WRate"] = DT_Lower1.Rows[i]["wRate"];
				gridMrsBase1[i + 1, "Qty"] = DT_Lower1.Rows[i]["bqty"];
				gridMrsBase1[i + 1, "Cost"] = DT_Lower1.Rows[i]["bcost"];
				gridMrsBase1[i + 1, "Amount"] = DT_Lower1.Rows[i]["bamount"];
				if (PubTools.Str2Double(DT_Lower1.Rows[i]["bamount"]) < 0.0)
				{
					gridMrsBase1.Rows[i + 1].Style = gridMrsBase1.Styles["Minus"];
				}
			}
			gridMrsBase1[i + 1, "Lock"] = DT_Lower1.Rows[i]["LockCost"].ToString().Trim() == "1";
			if (F_ActionName == PccesFormAction.BUD || F_ActionName == PccesFormAction.BID)
			{
				gridMrsBase1[i + 1, "usrQty"] = DT_Lower1.Rows[i]["usrQty"];
			}
		}
		ultraStatusBar1.Panels[0].Text = "資料筆數:" + DT_Lower1.Rows.Count;
		ultraStatusBar1.Panels[1].Text = "專案代碼:" + F_ProjectCode1;
		ultraStatusBar1.Panels[2].Text = "分析數量:" + sAnalysisQty1;
	}

	private void BindToGrid2()
	{
		CellStyle CS1 = gridMrsBase2.Styles.Add("AnalysisColor");
		CS1.ForeColor = Color.Red;
		CellStyle CS2 = gridMrsBase2.Styles.Add("LEMColor");
		CS2.ForeColor = Color.Teal;
		CellStyle CS3 = gridMrsBase2.Styles.Add("WColor");
		CS3.ForeColor = Color.Purple;
		CellStyle CS4 = gridMrsBase2.Styles.Add("ZColor");
		CS4.ForeColor = Color.Teal;
		CS4.BackColor = Color.LemonChiffon;
		CellStyle CSC = gridMrsBase2.Styles.Add("CColor");
		CSC.ForeColor = Color.Black;
		CSC.Font = new Font("細明體", 11f, FontStyle.Bold);
		CellStyle CS5 = gridMrsBase2.Styles.Add("DollarColor");
		CS5.ForeColor = Color.Green;
		CellStyle CS6 = gridMrsBase2.Styles.Add("PercentColor");
		CS6.ForeColor = Color.Blue;
		CellStyle CSM = gridMrsBase2.Styles.Add("Minus");
		CSM.BackColor = Color.FromArgb(255, 80, 80);
		string sItemClass = "";
		string sItemKind = "";
		gridMrsBase2.Rows.Count = DT_Lower2.Rows.Count + 1;
		for (int i = 0; i < DT_Lower2.Rows.Count; i++)
		{
			sItemClass = ((DT_Lower2.Rows[i]["pccesCode"].ToString().Length > 0) ? DT_Lower2.Rows[i]["pccesCode"].ToString().Substring(0, 1) : "");
			sItemKind = ((DT_Lower2.Rows[i]["costKind"] == null) ? "" : ((DT_Lower2.Rows[i]["costKind"].ToString().Length > 0) ? DT_Lower2.Rows[i]["costKind"].ToString().Substring(0, 1) : ""));
			gridMrsBase2[i + 1, "PccesCode"] = DT_Lower2.Rows[i]["pccesCode"].ToString();
			if (sItemClass == "L" || sItemClass == "E" || sItemClass == "M")
			{
				gridMrsBase2.Rows[i + 1].Style = gridMrsBase2.Styles["LEMColor"];
			}
			else if (sItemClass == "W")
			{
				gridMrsBase2.Rows[i + 1].Style = gridMrsBase2.Styles["WColor"];
			}
			switch (sItemKind)
			{
			case "$":
				gridMrsBase2.Rows[i + 1].Style = gridMrsBase2.Styles["DollarColor"];
				break;
			case "%":
				gridMrsBase2.Rows[i + 1].Style = gridMrsBase2.Styles["PercentColor"];
				break;
			case "Z":
				gridMrsBase2.Rows[i + 1].Style = gridMrsBase2.Styles["ZColor"];
				break;
			case "#":
				gridMrsBase2.Rows[i + 1].Style = gridMrsBase2.Styles["CColor"];
				break;
			}
			gridMrsBase2[i + 1, "CName"] = DT_Lower2.Rows[i]["cName"].ToString();
			if (DT_Lower2.Rows[i]["analysis"].ToString().Trim() == "1")
			{
				gridMrsBase2[i + 1, "Analysis"] = true;
				gridMrsBase2.Rows[i + 1].Style = gridMrsBase2.Styles["AnalysisColor"];
				CellRange rg = gridMrsBase2.GetCellRange(i + 1, gridMrsBase2.Cols["AnaImg"].SafeIndex);
				rg.Style = gridMrsBase2.Styles["img"];
				rg.Image = imageList2.Images[0];
			}
			else
			{
				gridMrsBase2[i + 1, "Analysis"] = false;
			}
			gridMrsBase2[i + 1, "ListNo"] = DT_Lower2.Rows[i]["listNo"];
			gridMrsBase2[i + 1, "UnitName"] = DT_Lower2.Rows[i]["unitName"];
			gridMrsBase2[i + 1, "CostKind"] = DT_Lower2.Rows[i]["costKind"];
			gridMrsBase2[i + 1, "PubCode"] = DT_Lower2.Rows[i]["pubCode"];
			gridMrsBase2[i + 1, "Memo"] = DT_Lower2.Rows[i]["memo"];
			if (sItemKind != "#")
			{
				gridMrsBase2[i + 1, "Rate"] = DT_Lower2.Rows[i]["rate"];
				gridMrsBase2[i + 1, "LRate"] = DT_Lower2.Rows[i]["lRate"];
				gridMrsBase2[i + 1, "ERate"] = DT_Lower2.Rows[i]["eRate"];
				gridMrsBase2[i + 1, "MRate"] = DT_Lower2.Rows[i]["mRate"];
				gridMrsBase2[i + 1, "WRate"] = DT_Lower2.Rows[i]["wRate"];
				gridMrsBase2[i + 1, "Qty"] = DT_Lower2.Rows[i]["bqty"];
				gridMrsBase2[i + 1, "Cost"] = DT_Lower2.Rows[i]["bcost"];
				gridMrsBase2[i + 1, "Amount"] = DT_Lower2.Rows[i]["bamount"];
				if (PubTools.Str2Double(DT_Lower2.Rows[i]["bamount"]) < 0.0)
				{
					gridMrsBase2.Rows[i + 1].Style = gridMrsBase2.Styles["Minus"];
				}
			}
			gridMrsBase2[i + 1, "Lock"] = DT_Lower2.Rows[i]["LockCost"].ToString().Trim() == "1";
			if (F_ActionName == PccesFormAction.BUD || F_ActionName == PccesFormAction.BID)
			{
				gridMrsBase2[i + 1, "usrQty"] = DT_Lower2.Rows[i]["usrQty"];
			}
		}
		ultraStatusBar2.Panels[0].Text = "資料筆數:" + DT_Lower2.Rows.Count;
		ultraStatusBar2.Panels[1].Text = "專案代碼:" + ((F_ProjectCode2 == "MRS") ? "工項基本資料庫" : F_ProjectCode2);
		ultraStatusBar2.Panels[2].Text = "分析數量:" + sAnalysisQty2;
	}

	private void HideCols(bool IsHide)
	{
		if (IsHide)
		{
			gridMrsBase1.Cols["PubCode"].Visible = false;
			gridMrsBase1.Cols["Analysis"].Visible = false;
			gridMrsBase1.Cols["usrQty"].Visible = false;
			gridMrsBase1.Cols["PSNo"].Visible = false;
			gridMrsBase1.Cols["Lock"].Visible = false;
			gridMrsBase2.Cols["PubCode"].Visible = false;
			gridMrsBase2.Cols["Analysis"].Visible = false;
			gridMrsBase2.Cols["usrQty"].Visible = false;
			gridMrsBase2.Cols["PSNo"].Visible = false;
			gridMrsBase2.Cols["Lock"].Visible = false;
		}
	}

	private void FormCompareMrsAna_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (base.WindowState != FormWindowState.Maximized)
		{
			CommonMethods.WriteIniValue("CompareMrsAna", "PK_LocationX", base.Location.X.ToString());
			CommonMethods.WriteIniValue("CompareMrsAna", "PK_LocationY", base.Location.Y.ToString());
			CommonMethods.WriteIniValue("CompareMrsAna", "PK_Width", base.Size.Width.ToString());
			CommonMethods.WriteIniValue("CompareMrsAna", "PK_Height", base.Size.Height.ToString());
		}
		CommonMethods.WriteIniValue("CompareMrsAna", "WindowState", base.WindowState.ToString());
	}

	private void FormCompareMrsAna_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormCompareMrsAna");
		}
	}
}
