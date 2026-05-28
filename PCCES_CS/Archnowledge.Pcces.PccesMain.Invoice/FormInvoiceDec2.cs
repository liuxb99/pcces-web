using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.CTRClass;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.STDClass;
using C1.Win.C1FlexGrid;
using C1.Win.C1Sizer;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinSchedule;
using Infragistics.Win.UltraWinSchedule.CalendarCombo;
using Infragistics.Win.UltraWinToolbars;

namespace Archnowledge.Pcces.PccesMain.Invoice;

public class FormInvoiceDec2 : Form
{
	private UltraToolbarsManager ultraToolbarsManager1;

	private IContainer components;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Top;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Bottom;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Left;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Right;

	private Panel panel1;

	private GroupBox groupBox1;

	private GridBudget Grid1;

	private C1Sizer c1Sizer1;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	private UltraLabel ultraLabel3;

	private UltraLabel ultraLabel4;

	private UltraLabel ultraLabel5;

	private UltraCalendarCombo Dudect_Date;

	private UltraTextEditor txtDesc;

	private UltraNumericEditor txtQty;

	private UltraNumericEditor txtCost;

	private UltraLabel lblAmount;

	private UltraLabel ultraLabel6;

	private UltraLabel lblProjectCode;

	private UltraLabel ultraLabel7;

	private UltraLabel lbl_Issue;

	private UltraLabel ultraLabel8;

	private UltraCombo cboCUnit;

	private int MaxListNo = 0;

	private string FORM_STATUS = "INI";

	private DataTable dt;

	private string F_ProjectCode;

	private string F_SubProjectCode;

	private string F_Issue;

	private string F_UserID;

	private string F_Old_Dudect_Date = "";

	private string F_Old_Desc = "";

	private string F_Old_Unit = "";

	private string F_Old_Qty = "";

	private string F_Old_Cost = "";

	private string F_flag = "-";

	private string F_Old_Amount = "";

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

	public string _SubProjectCode
	{
		get
		{
			return F_SubProjectCode;
		}
		set
		{
			F_SubProjectCode = value;
		}
	}

	public string _Issue
	{
		get
		{
			return F_Issue;
		}
		set
		{
			F_Issue = value;
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

	public string _flag
	{
		get
		{
			return F_flag;
		}
		set
		{
			F_flag = value;
		}
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Tool2");
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuAdd");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuMod");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSave");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCancel");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool5 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDel");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool6 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExit");
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool7 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuAdd");
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.Invoice.FormInvoiceDec2));
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool8 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuMod");
		Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool9 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSave");
		Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool10 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCancel");
		Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool11 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDel");
		Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool12 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExit");
		Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinGrid.UltraGridLayout ultraGridLayout1 = new Infragistics.Win.UltraWinGrid.UltraGridLayout();
		Infragistics.Win.ValueList valueList1 = new Infragistics.Win.ValueList(86092282);
		Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton1 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
		Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
		this.ultraToolbarsManager1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
		this.Grid1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this._FormSys_B_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.panel1 = new System.Windows.Forms.Panel();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.c1Sizer1 = new C1.Win.C1Sizer.C1Sizer();
		this.cboCUnit = new Infragistics.Win.UltraWinGrid.UltraCombo();
		this.lblAmount = new Infragistics.Win.Misc.UltraLabel();
		this.txtQty = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
		this.txtDesc = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.Dudect_Date = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.txtCost = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.lblProjectCode = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.lbl_Issue = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel8 = new Infragistics.Win.Misc.UltraLabel();
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Grid1).BeginInit();
		this.panel1.SuspendLayout();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.c1Sizer1).BeginInit();
		this.c1Sizer1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cboCUnit).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtQty).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDesc).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Dudect_Date).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCost).BeginInit();
		base.SuspendLayout();
		appearance1.FontData.Name = "細明體";
		appearance1.FontData.SizeInPoints = 11f;
		this.ultraToolbarsManager1.Appearance = appearance1;
		appearance2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance2.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.DockAreaAppearance = appearance2;
		this.ultraToolbarsManager1.DockWithinContainer = this;
		this.ultraToolbarsManager1.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraToolbarsManager1.LockToolbars = true;
		appearance3.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance3.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance3.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.MenuSettings.HotTrackAppearance = appearance3;
		appearance4.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance4.BackColor2 = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraToolbarsManager1.MenuSettings.IconAreaAppearance = appearance4;
		appearance5.BackColor = System.Drawing.Color.White;
		appearance5.BackColor2 = System.Drawing.Color.White;
		this.ultraToolbarsManager1.MenuSettings.ToolAppearance = appearance5;
		this.ultraToolbarsManager1.ShowFullMenusDelay = 500;
		this.ultraToolbarsManager1.ShowQuickCustomizeButton = false;
		this.ultraToolbarsManager1.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2003;
		ultraToolbar1.DockedColumn = 0;
		ultraToolbar1.DockedRow = 0;
		ultraToolbar1.Settings.AllowCustomize = Infragistics.Win.DefaultableBoolean.True;
		appearance6.TextVAlign = Infragistics.Win.VAlign.Bottom;
		ultraToolbar1.Settings.Appearance = appearance6;
		ultraToolbar1.Text = "Tool2";
		buttonTool2.InstanceProps.IsFirstInGroup = true;
		buttonTool3.InstanceProps.IsFirstInGroup = true;
		buttonTool4.InstanceProps.IsFirstInGroup = true;
		buttonTool5.InstanceProps.IsFirstInGroup = true;
		buttonTool6.InstanceProps.IsFirstInGroup = true;
		ultraToolbar1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[6] { buttonTool1, buttonTool2, buttonTool3, buttonTool4, buttonTool5, buttonTool6 });
		this.ultraToolbarsManager1.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[1] { ultraToolbar1 });
		appearance7.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance7.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.ToolbarSettings.Appearance = appearance7;
		appearance8.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance8.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance8.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.ToolbarSettings.HotTrackAppearance = appearance8;
		appearance9.Image = resources.GetObject("appearance9.Image");
		buttonTool7.SharedProps.AppearancesSmall.Appearance = appearance9;
		buttonTool7.SharedProps.Caption = "新 增";
		buttonTool7.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		appearance10.Image = resources.GetObject("appearance10.Image");
		buttonTool8.SharedProps.AppearancesSmall.Appearance = appearance10;
		buttonTool8.SharedProps.Caption = "更 正";
		buttonTool8.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		appearance11.Image = resources.GetObject("appearance11.Image");
		buttonTool9.SharedProps.AppearancesSmall.Appearance = appearance11;
		buttonTool9.SharedProps.Caption = "存 檔";
		buttonTool9.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		appearance12.Image = resources.GetObject("appearance12.Image");
		buttonTool10.SharedProps.AppearancesSmall.Appearance = appearance12;
		buttonTool10.SharedProps.Caption = "取 消";
		buttonTool10.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		appearance13.Image = resources.GetObject("appearance13.Image");
		buttonTool11.SharedProps.AppearancesSmall.Appearance = appearance13;
		buttonTool11.SharedProps.Caption = "刪 除";
		buttonTool11.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		appearance14.Image = resources.GetObject("appearance14.Image");
		buttonTool12.SharedProps.AppearancesSmall.Appearance = appearance14;
		buttonTool12.SharedProps.Caption = "結 束";
		buttonTool12.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		this.ultraToolbarsManager1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[6] { buttonTool7, buttonTool8, buttonTool9, buttonTool10, buttonTool11, buttonTool12 });
		this.ultraToolbarsManager1.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(ultraToolbarsManager1_ToolClick);
		this.Grid1._ExcelFileName = "";
		this.Grid1._ExcelSheeName = "";
		this.Grid1._IsOpenExcelAfterExport = false;
		this.Grid1.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.Rows;
		this.Grid1.AllowEditing = false;
		this.Grid1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.Grid1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.Grid1.ColumnInfo = "8,1,0,0,0,110,Columns:0{Width:14;Name:\"RowIndicator\";AllowEditing:False;DataType:System.Int32;TextAlign:RightTop;TextAlignFixed:GeneralTop;}\t1{Name:\"ListNo\";Caption:\"ListNo\";DataType:System.Int32;TextAlign:RightCenter;TextAlignFixed:GeneralTop;}\t2{Width:90;Name:\"Dudect_Date\";Caption:\"扣款日期\";DataType:System.DateTime;Format:\"d\";TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t3{Width:141;Name:\"Desc\";Caption:\"扣款說明\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t4{Width:80;Name:\"unitName\";Caption:\"單位\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t5{Width:111;Name:\"Qty\";Caption:\"數量\";AllowEditing:False;DataType:System.Decimal;Format:\"###,###,###,##0.00\";TextAlign:GeneralTop;TextAlignFixed:GeneralTop;ImageAlign:CenterCenter;}\t6{Width:110;Name:\"Cost\";Caption:\"單價\";DataType:System.Decimal;Format:\"###,###,###,##0.00\";TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t7{Name:\"Amount\";Caption:\"複價\";DataType:System.Decimal;Format:\"###,###,###,##0.00\";TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t";
		this.ultraToolbarsManager1.SetContextMenuUltra(this.Grid1, "Popup1");
		this.Grid1.ExtendLastCol = true;
		this.Grid1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.Grid1.ForeColor = System.Drawing.Color.Black;
		this.Grid1.Location = new System.Drawing.Point(8, 168);
		this.Grid1.Name = "Grid1";
		this.Grid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
		this.Grid1.ShowCursor = true;
		this.Grid1.ShowSort = false;
		this.Grid1.ShowToolTipOnNarrowColumn = true;
		this.Grid1.Size = new System.Drawing.Size(680, 268);
		this.Grid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection("Normal{Font:細明體, 11.25pt;BackColor:237, 243, 254;ForeColor:Black;TextAlign:GeneralTop;Border:Flat,1,Silver,Both;}\tAlternate{BackColor:White;}\tFixed{BackColor:225, 247, 223;Border:Raised,1,Black,Both;}\tHighlight{BackColor:102, 153, 255;TextAlign:GeneralCenter;Format:\"###,###,###,##0\";}\tFocus{Font:細明體, 11.25pt;BackColor:102, 153, 255;TextAlign:GeneralCenter;Border:None,1,Black,Both;}\tSearch{Font:細明體, 9.75pt;BackColor:White;ForeColor:HighlightText;Border:Double,1,96, 145, 234,Both;}\tFrozen{BackColor:Beige;}\tEmptyArea{BackColor:232, 232, 232;Border:Flat,1,ControlDarkDark,Both;}\tGrandTotal{BackColor:Black;ForeColor:White;}\tSubtotal0{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal1{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal2{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal3{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal4{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal5{BackColor:ControlDarkDark;ForeColor:White;}\t");
		this.Grid1.TabIndex = 2;
		this.Grid1.Tree.Column = 1;
		this.Grid1.Tree.LineColor = System.Drawing.Color.Gray;
		this.Grid1.Click += new System.EventHandler(Grid1_Click);
		this._FormSys_B_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
		this._FormSys_B_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
		this._FormSys_B_Toolbars_Dock_Area_Top.Name = "_FormSys_B_Toolbars_Dock_Area_Top";
		this._FormSys_B_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(696, 29);
		this._FormSys_B_Toolbars_Dock_Area_Top.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 477);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Name = "_FormSys_B_Toolbars_Dock_Area_Bottom";
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(696, 0);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
		this._FormSys_B_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 29);
		this._FormSys_B_Toolbars_Dock_Area_Left.Name = "_FormSys_B_Toolbars_Dock_Area_Left";
		this._FormSys_B_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 448);
		this._FormSys_B_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
		this._FormSys_B_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(696, 29);
		this._FormSys_B_Toolbars_Dock_Area_Right.Name = "_FormSys_B_Toolbars_Dock_Area_Right";
		this._FormSys_B_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 448);
		this._FormSys_B_Toolbars_Dock_Area_Right.ToolbarsManager = this.ultraToolbarsManager1;
		this.panel1.BackColor = System.Drawing.Color.White;
		this.panel1.Controls.Add(this.Grid1);
		this.panel1.Controls.Add(this.groupBox1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 29);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(696, 448);
		this.panel1.TabIndex = 30;
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.c1Sizer1);
		this.groupBox1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.groupBox1.Location = new System.Drawing.Point(8, 8);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(680, 152);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.c1Sizer1.AllowDrop = true;
		this.c1Sizer1.Controls.Add(this.cboCUnit);
		this.c1Sizer1.Controls.Add(this.lblAmount);
		this.c1Sizer1.Controls.Add(this.txtQty);
		this.c1Sizer1.Controls.Add(this.txtDesc);
		this.c1Sizer1.Controls.Add(this.Dudect_Date);
		this.c1Sizer1.Controls.Add(this.ultraLabel1);
		this.c1Sizer1.Controls.Add(this.ultraLabel2);
		this.c1Sizer1.Controls.Add(this.ultraLabel3);
		this.c1Sizer1.Controls.Add(this.ultraLabel4);
		this.c1Sizer1.Controls.Add(this.ultraLabel5);
		this.c1Sizer1.Controls.Add(this.txtCost);
		this.c1Sizer1.Controls.Add(this.ultraLabel6);
		this.c1Sizer1.Controls.Add(this.lblProjectCode);
		this.c1Sizer1.Controls.Add(this.ultraLabel7);
		this.c1Sizer1.Controls.Add(this.lbl_Issue);
		this.c1Sizer1.Controls.Add(this.ultraLabel8);
		this.c1Sizer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.c1Sizer1.GridDefinition = "21.09375:False:False;21.09375:False:False;19.53125:False:False;22.65625:False:False;\t1.48367952522255:False:True;14.8367952522255:False:True;14.8367952522255:False:True;16.0237388724036:False:False;14.8367952522255:False:True;16.3204747774481:False:False;14.8367952522255:False:True;1.48367952522255:False:True;";
		this.c1Sizer1.Location = new System.Drawing.Point(3, 21);
		this.c1Sizer1.Name = "c1Sizer1";
		this.c1Sizer1.Size = new System.Drawing.Size(674, 128);
		this.c1Sizer1.TabIndex = 0;
		this.c1Sizer1.TabStop = false;
		this.cboCUnit.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.cboCUnit.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
		this.cboCUnit.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Dotted;
		this.cboCUnit.DisplayLayout.BorderStyleCaption = Infragistics.Win.UIElementBorderStyle.Dashed;
		this.cboCUnit.DisplayMember = "";
		ultraGridLayout1.AutoFitColumns = true;
		valueList1.Key = "cString";
		ultraGridLayout1.ValueLists.Add(valueList1);
		this.cboCUnit.Layouts.Add(ultraGridLayout1);
		this.cboCUnit.Location = new System.Drawing.Point(556, 35);
		this.cboCUnit.Name = "cboCUnit";
		this.cboCUnit.Size = new System.Drawing.Size(100, 24);
		this.cboCUnit.TabIndex = 40;
		this.cboCUnit.ValueMember = "";
		appearance15.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblAmount.Appearance = appearance15;
		this.lblAmount.Location = new System.Drawing.Point(556, 95);
		this.lblAmount.Name = "lblAmount";
		this.lblAmount.Size = new System.Drawing.Size(100, 29);
		this.lblAmount.TabIndex = 39;
		this.lblAmount.Text = "[lblAmount]";
		this.txtQty.Location = new System.Drawing.Point(122, 95);
		this.txtQty.Name = "txtQty";
		this.txtQty.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
		this.txtQty.PromptChar = ' ';
		this.txtQty.Size = new System.Drawing.Size(100, 24);
		this.txtQty.TabIndex = 38;
		this.txtQty.ValueChanged += new System.EventHandler(txtQty_ValueChanged);
		this.txtDesc.Location = new System.Drawing.Point(122, 66);
		this.txtDesc.Name = "txtDesc";
		this.txtDesc.Size = new System.Drawing.Size(534, 24);
		this.txtDesc.TabIndex = 37;
		this.txtDesc.Text = "[txtDesc]";
		dateButton1.Caption = "今天";
		this.Dudect_Date.DateButtons.Add(dateButton1);
		this.Dudect_Date.DayOfWeekCaptionStyle = Infragistics.Win.UltraWinSchedule.DayOfWeekCaptionStyle.ShortDescription;
		this.Dudect_Date.Location = new System.Drawing.Point(122, 35);
		this.Dudect_Date.Name = "Dudect_Date";
		this.Dudect_Date.NonAutoSizeHeight = 21;
		this.Dudect_Date.NullDateLabel = "";
		this.Dudect_Date.Size = new System.Drawing.Size(212, 21);
		this.Dudect_Date.TabIndex = 36;
		this.Dudect_Date.TipStyle = Infragistics.Win.UltraWinSchedule.TipStyleDay.Holidays;
		this.Dudect_Date.Value = resources.GetObject("Dudect_Date.Value");
		this.Dudect_Date.WeekNumbersVisible = true;
		appearance16.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance16.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel1.Appearance = appearance16;
		this.ultraLabel1.Location = new System.Drawing.Point(18, 35);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(100, 27);
		this.ultraLabel1.TabIndex = 0;
		this.ultraLabel1.Text = "扣款日期:";
		appearance17.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance17.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel2.Appearance = appearance17;
		this.ultraLabel2.Location = new System.Drawing.Point(18, 66);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(100, 25);
		this.ultraLabel2.TabIndex = 0;
		this.ultraLabel2.Text = "扣款說明:";
		appearance18.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance18.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel3.Appearance = appearance18;
		this.ultraLabel3.Location = new System.Drawing.Point(18, 95);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(100, 29);
		this.ultraLabel3.TabIndex = 0;
		this.ultraLabel3.Text = "數量:";
		appearance19.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance19.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel4.Appearance = appearance19;
		this.ultraLabel4.Location = new System.Drawing.Point(226, 95);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(108, 29);
		this.ultraLabel4.TabIndex = 0;
		this.ultraLabel4.Text = "單價:";
		appearance20.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance20.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel5.Appearance = appearance20;
		this.ultraLabel5.Location = new System.Drawing.Point(442, 95);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(110, 29);
		this.ultraLabel5.TabIndex = 0;
		this.ultraLabel5.Text = "複價:";
		this.txtCost.Location = new System.Drawing.Point(338, 95);
		this.txtCost.Name = "txtCost";
		this.txtCost.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
		this.txtCost.PromptChar = ' ';
		this.txtCost.Size = new System.Drawing.Size(100, 24);
		this.txtCost.TabIndex = 38;
		this.txtCost.ValueChanged += new System.EventHandler(txtCost_ValueChanged);
		appearance21.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance21.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel6.Appearance = appearance21;
		this.ultraLabel6.Location = new System.Drawing.Point(18, 4);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(100, 27);
		this.ultraLabel6.TabIndex = 0;
		this.ultraLabel6.Text = "專案代號:";
		appearance22.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance22.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblProjectCode.Appearance = appearance22;
		this.lblProjectCode.Location = new System.Drawing.Point(122, 4);
		this.lblProjectCode.Name = "lblProjectCode";
		this.lblProjectCode.Size = new System.Drawing.Size(212, 27);
		this.lblProjectCode.TabIndex = 0;
		this.lblProjectCode.Text = "[lblProjectCode]";
		appearance23.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance23.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel7.Appearance = appearance23;
		this.ultraLabel7.Location = new System.Drawing.Point(442, 4);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(110, 27);
		this.ultraLabel7.TabIndex = 0;
		this.ultraLabel7.Text = "計價期別:";
		appearance24.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance24.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lbl_Issue.Appearance = appearance24;
		this.lbl_Issue.Location = new System.Drawing.Point(556, 4);
		this.lbl_Issue.Name = "lbl_Issue";
		this.lbl_Issue.Size = new System.Drawing.Size(100, 27);
		this.lbl_Issue.TabIndex = 0;
		this.lbl_Issue.Text = "[lbl_Issue]";
		appearance25.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance25.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel8.Appearance = appearance25;
		this.ultraLabel8.Location = new System.Drawing.Point(442, 35);
		this.ultraLabel8.Name = "ultraLabel8";
		this.ultraLabel8.Size = new System.Drawing.Size(110, 27);
		this.ultraLabel8.TabIndex = 0;
		this.ultraLabel8.Text = "單位:";
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		base.ClientSize = new System.Drawing.Size(696, 477);
		base.ControlBox = false;
		base.Controls.Add(this.panel1);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Right);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Left);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Top);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Bottom);
		base.KeyPreview = true;
		base.MinimizeBox = false;
		base.Name = "FormInvoiceDec2";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "應扣金額編輯";
		base.Load += new System.EventHandler(FormInvoiceDec2_Load);
		base.Activated += new System.EventHandler(FormInvoiceDec2_Activated);
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Grid1).EndInit();
		this.panel1.ResumeLayout(false);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.c1Sizer1).EndInit();
		this.c1Sizer1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.cboCUnit).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtQty).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDesc).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Dudect_Date).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCost).EndInit();
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

	public FormInvoiceDec2()
	{
		InitializeComponent();
	}

	private void FormInvoiceDec2_Activated(object sender, EventArgs e)
	{
		if (FORM_STATUS == "INI")
		{
			FORM_STATUS = "NOR";
			LoadData();
			ModeCheck();
		}
	}

	private void FormInvoiceDec2_Load(object sender, EventArgs e)
	{
		Grid1.Cols["ListNo"].Visible = false;
		Dudect_Date.Value = DateTime.Now;
		txtDesc.Text = "";
		txtQty.Value = 0;
		txtCost.Value = 0;
		lblProjectCode.Text = F_ProjectCode;
		lbl_Issue.Text = "第 " + F_Issue + " 期";
		lblAmount.Text = "0";
		if (F_flag == "+")
		{
			Text = "應加金額編輯";
			ultraLabel1.Text = "加款日期：";
			ultraLabel2.Text = "加款說明：";
			Grid1.Cols["Dudect_Date"].Caption = "加款日期";
			Grid1.Cols["Desc"].Caption = "加款說明";
		}
	}

	private void ModeCheck()
	{
		if (FORM_STATUS == "NOR")
		{
			ultraToolbarsManager1.Tools["mnuAdd"].SharedProps.Enabled = true;
			if (dt.Rows.Count > 0)
			{
				ultraToolbarsManager1.Tools["mnuMod"].SharedProps.Enabled = true;
			}
			else
			{
				ultraToolbarsManager1.Tools["mnuMod"].SharedProps.Enabled = false;
			}
			ultraToolbarsManager1.Tools["mnuDel"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuExit"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuSave"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuCancel"].SharedProps.Enabled = false;
			Dudect_Date.Enabled = false;
			txtDesc.Enabled = false;
			txtQty.Enabled = false;
			txtCost.Enabled = false;
			cboCUnit.Enabled = false;
			Grid1.Enabled = true;
		}
		if (FORM_STATUS == "MOD" || FORM_STATUS == "NEW")
		{
			ultraToolbarsManager1.Tools["mnuAdd"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuMod"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuDel"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuExit"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuSave"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuCancel"].SharedProps.Enabled = true;
			Dudect_Date.Enabled = true;
			txtDesc.Enabled = true;
			txtQty.Enabled = true;
			txtCost.Enabled = true;
			cboCUnit.Enabled = true;
			Grid1.Enabled = false;
		}
	}

	private void LoadData()
	{
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("本期總計--扣款明細編輯");
		Sub_Dudect SubDudctCom = new Sub_Dudect(tmp_AL1);
		SubDudctCom.ps_flag = F_flag;
		dt = SubDudctCom.ListItem("", F_SubProjectCode, F_ProjectCode, F_Issue);
		MaxListNo = SubDudctCom.MaxListNo(F_SubProjectCode, F_ProjectCode, F_Issue);
		SubDudctCom = null;
		GetUnit_DataSet();
		BindData();
	}

	private void BindData()
	{
		Grid1.Rows.Count = dt.Rows.Count + 1;
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			Grid1[i + 1, "ListNo"] = dt.Rows[i]["ListNo"];
			Grid1[i + 1, "Dudect_Date"] = dt.Rows[i]["Dudect_Date"];
			Grid1[i + 1, "Desc"] = dt.Rows[i]["Desc"];
			Grid1[i + 1, "unitName"] = dt.Rows[i]["Unit"];
			Grid1[i + 1, "Qty"] = dt.Rows[i]["Qty"];
			Grid1[i + 1, "Cost"] = dt.Rows[i]["cost"];
			Grid1[i + 1, "Amount"] = PubTools.Str2Double(dt.Rows[i]["Qty"]) * PubTools.Str2Double(dt.Rows[i]["cost"]);
		}
		if (Grid1.Rows.Count <= 1)
		{
			Dudect_Date.Value = DateTime.Now;
			txtDesc.Text = "";
			txtQty.Value = 0;
			txtCost.Value = 0;
			lblAmount.Text = "0";
		}
	}

	private void ultraToolbarsManager1_ToolClick(object sender, ToolClickEventArgs e)
	{
		switch (e.Tool.Key)
		{
		case "mnuAdd":
			Do_Add();
			break;
		case "mnuMod":
			Do_Mod();
			break;
		case "mnuSave":
			Do_Save();
			break;
		case "mnuCancel":
			Do_Cancel();
			break;
		case "mnuDel":
			Do_Delete();
			break;
		case "mnuExit":
			Do_Exit();
			break;
		}
	}

	private void Do_Add()
	{
		F_Old_Dudect_Date = PubTools.Str2DateTime(Dudect_Date.Value).ToShortDateString();
		F_Old_Cost = PubTools.Str2Double(txtCost.Value).ToString();
		F_Old_Qty = PubTools.Str2Double(txtQty.Value).ToString();
		F_Old_Desc = txtDesc.Text;
		F_Old_Unit = cboCUnit.Text;
		F_Old_Amount = lblAmount.Text;
		Dudect_Date.Value = DateTime.Now;
		txtDesc.Text = "";
		cboCUnit.Text = "";
		txtQty.Value = 0;
		txtCost.Value = 0;
		lblAmount.Text = "0";
		FORM_STATUS = "NEW";
		ModeCheck();
	}

	private void Do_Mod()
	{
		F_Old_Dudect_Date = PubTools.Str2DateTime(Dudect_Date.Value).ToShortDateString();
		F_Old_Cost = PubTools.Str2Double(txtCost.Value).ToString();
		F_Old_Qty = PubTools.Str2Double(txtQty.Value).ToString();
		F_Old_Desc = txtDesc.Text;
		F_Old_Unit = cboCUnit.Text;
		F_Old_Amount = lblAmount.Text;
		FORM_STATUS = "MOD";
		ModeCheck();
	}

	private void Do_Save()
	{
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("本期總計--扣款明細編輯");
		Sub_Dudect SubDudctCom = new Sub_Dudect(tmp_AL1);
		SubDudctCom.ps_project = F_ProjectCode;
		SubDudctCom.ps_sproj = F_SubProjectCode;
		SubDudctCom.ps_queue = F_Issue;
		SubDudctCom.ps_Dudect_Date = PubTools.Str2DateTime(Dudect_Date.Text).ToShortDateString();
		SubDudctCom.ps_Unit = cboCUnit.Text;
		SubDudctCom.ps_desc = txtDesc.Text.Trim();
		SubDudctCom.ps_qty = txtQty.Value.ToString();
		SubDudctCom.ps_cost = txtCost.Value.ToString();
		SubDudctCom.ps_Amount = (PubTools.Str2Double(txtQty.Value) * PubTools.Str2Double(txtCost.Value)).ToString();
		SubDudctCom.ps_flag = F_flag;
		if (FORM_STATUS == "NEW")
		{
			SubDudctCom.ps_ListNo = (MaxListNo + 1).ToString();
			SubDudctCom.InseItem();
		}
		else
		{
			if (Grid1.Row < 0)
			{
				return;
			}
			SubDudctCom.ps_ListNo = Grid1[Grid1.Row, "ListNo"].ToString();
			SubDudctCom.UpdItem();
		}
		FORM_STATUS = "NOR";
		LoadData();
		ModeCheck();
	}

	private void Do_Cancel()
	{
		Dudect_Date.Value = PubTools.Str2DateTime(F_Old_Dudect_Date);
		txtDesc.Text = F_Old_Desc;
		cboCUnit.Text = F_Old_Unit;
		txtQty.Value = PubTools.Str2Double(F_Old_Qty);
		txtCost.Value = PubTools.Str2Double(F_Old_Cost);
		lblAmount.Text = F_Old_Amount;
		FORM_STATUS = "NOR";
		ModeCheck();
	}

	private void Do_Exit()
	{
		double Total = 0.0;
		for (int i = 1; i < Grid1.Rows.Count; i++)
		{
			Total += PubTools.Str2Double(Grid1.Rows[i]["Qty"]) * PubTools.Str2Double(Grid1.Rows[i]["Cost"]);
		}
		if (F_flag == "-")
		{
			(base.Owner as FormInvoiceSubAcInfo).__Duc2 = Total.ToString();
		}
		else
		{
			(base.Owner as FormInvoiceSubAcInfo).__Add2 = Total.ToString();
		}
		base.DialogResult = DialogResult.OK;
	}

	private void Do_Delete()
	{
		if (MessageBox.Show(this, "確定要刪除此筆資料?", "訊問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			ArrayList tmp_AL1 = new ArrayList();
			tmp_AL1.Add(F_UserID);
			tmp_AL1.Add("本期總計--扣款明細編輯");
			Sub_Dudect SubDudctCom = new Sub_Dudect(tmp_AL1);
			SubDudctCom.ps_project = F_ProjectCode;
			SubDudctCom.ps_sproj = F_SubProjectCode;
			SubDudctCom.ps_queue = F_Issue;
			SubDudctCom.ps_ListNo = Grid1[Grid1.Row, "ListNo"].ToString();
			SubDudctCom.DeleItem();
			LoadData();
		}
	}

	private void GetUnit_DataSet()
	{
		DataSet DS1 = new DataSet();
		DBClass DBClass1 = new DBClass();
		DBClass1._FS_UserID = "PccAdmin";
		DataTable DT_Temp = DBClass1.GetUserDefine("Select cString as 中文單位 from UserDefind Where kind='cUnit' Order By IsNull(Times,0) Desc");
		DataRow DR = DT_Temp.NewRow();
		DR["中文單位"] = "";
		DT_Temp.Rows.Add(DR);
		DT_Temp.TableName = "cUnit";
		DS1.Tables.Add(DT_Temp.Copy());
		cboCUnit.DataSource = DS1;
		cboCUnit.DataMember = "cUnit";
		cboCUnit.DataBind();
	}

	private void Grid1_Click(object sender, EventArgs e)
	{
		if (Grid1.Row >= 1)
		{
			Dudect_Date.Value = PubTools.Str2DateTime(Grid1[Grid1.Row, "Dudect_Date"]);
			txtDesc.Text = ((Grid1[Grid1.Row, "Desc"] == null) ? "" : Grid1[Grid1.Row, "Desc"].ToString());
			cboCUnit.Text = ((Grid1[Grid1.Row, "unitName"] == null) ? "" : Grid1[Grid1.Row, "unitName"].ToString());
			txtQty.Value = PubTools.Str2Double(Grid1[Grid1.Row, "Qty"]);
			txtCost.Value = PubTools.Str2Double(Grid1[Grid1.Row, "Cost"]);
			lblAmount.Text = PubTools.Str2Double(Grid1[Grid1.Row, "Amount"]).ToString();
		}
	}

	private void txtQty_ValueChanged(object sender, EventArgs e)
	{
		double dqty = 0.0;
		double dcost = 0.0;
		double dAmount = 0.0;
		dqty = PubTools.Str2Double(txtQty.Value);
		dcost = PubTools.Str2Double(txtCost.Value);
		lblAmount.Text = (dqty * dcost).ToString();
	}

	private void txtCost_ValueChanged(object sender, EventArgs e)
	{
		double dqty = 0.0;
		double dcost = 0.0;
		double dAmount = 0.0;
		dqty = PubTools.Str2Double(txtQty.Value);
		dcost = PubTools.Str2Double(txtCost.Value);
		lblAmount.Text = (dqty * dcost).ToString();
	}
}
