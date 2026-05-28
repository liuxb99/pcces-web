using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.CommonClass.Budget;
using Archnowledge.Pcces.CTRClass;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.PccesMain.BudgetChange;
using Archnowledge.Pcces.PccesMain.Invoice;
using Archnowledge.Pcces.PccesMain.Report;
using Archnowledge.Pcces.PccesMain.SplitContract;
using Archnowledge.Pcces.PccesMain.SubClose;
using Archnowledge.Pcces.PccesMain.SubFinal;
using Archnowledge.Pcces.STDClass;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinToolbars;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormBudgetProjectPick : Form
{
	private IContainer components;

	private Panel panel1;

	private UltraLabel ultraLabel2;

	private UltraButton ultraButton1;

	private ImageList imageList2;

	private UltraComboEditor cbFind;

	private ImageList imageList1;

	private Panel panel2;

	private GridBudget c1FlexGrid1;

	private Panel panel3;

	private UltraLabel ultraLabel5;

	private UltraLabel ultraLabel4;

	private UltraLabel ultraLabel3;

	private UltraLabel ultraLabel6;

	private UltraButton ultraButton2;

	private UltraLabel lblTitle;

	private frmBudget F_FormBudget = null;

	private UltraLabel lblUseDatabase;

	private UltraToolbarsDockArea _FormBudgetProjectPick_Toolbars_Dock_Area_Left;

	private UltraToolbarsDockArea _FormBudgetProjectPick_Toolbars_Dock_Area_Right;

	private UltraToolbarsDockArea _FormBudgetProjectPick_Toolbars_Dock_Area_Top;

	private UltraToolbarsDockArea _FormBudgetProjectPick_Toolbars_Dock_Area_Bottom;

	private string F_CurrentEditProjectCode = "";

	private string F_SelectedProjectCode = "";

	private string F_KeyWord = "";

	private DataTable DT_Temp = new DataTable();

	private DataTable DT1 = new DataTable();

	private FormBudget_PickType F_CallUpType = FormBudget_PickType.NewBudget;

	private bool F_HasRegistered;

	private PccesFormAction F_ActionName;

	private string F_UserID;

	private string F_IsAddOn;

	private bool F_Istemplate = false;

	private UltraToolbarsManager ultraToolbarsManager1;

	private string F_Mode;

	public string _CurrentEditProjectCode
	{
		set
		{
			F_CurrentEditProjectCode = value;
		}
	}

	public string _SelectedProjectCode => F_SelectedProjectCode;

	public string _Mode
	{
		get
		{
			return F_Mode;
		}
		set
		{
			F_Mode = value;
		}
	}

	public string _IsAddOn
	{
		get
		{
			return F_IsAddOn;
		}
		set
		{
			F_IsAddOn = value;
		}
	}

	public frmBudget _FormBudget => F_FormBudget;

	public bool _HasRegistered
	{
		get
		{
			return F_HasRegistered;
		}
		set
		{
			F_HasRegistered = value;
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

	public FormBudget_PickType CallUpType
	{
		get
		{
			return F_CallUpType;
		}
		set
		{
			F_CallUpType = value;
		}
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormBudgetProjectPick));
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("UltraToolbar1");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool1 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("PopupMenuTool1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelete");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelete");
		Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
		this.panel1 = new System.Windows.Forms.Panel();
		this.lblUseDatabase = new Infragistics.Win.Misc.UltraLabel();
		this.ultraButton2 = new Infragistics.Win.Misc.UltraButton();
		this.panel2 = new System.Windows.Forms.Panel();
		this.c1FlexGrid1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.panel3 = new System.Windows.Forms.Panel();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.cbFind = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.lblTitle = new Infragistics.Win.Misc.UltraLabel();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.ultraToolbarsManager1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid1).BeginInit();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cbFind).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).BeginInit();
		base.SuspendLayout();
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel1.Controls.Add(this.lblUseDatabase);
		this.panel1.Controls.Add(this.ultraButton2);
		this.panel1.Controls.Add(this.panel2);
		this.panel1.Controls.Add(this.cbFind);
		this.panel1.Controls.Add(this.ultraButton1);
		this.panel1.Controls.Add(this.ultraLabel2);
		this.panel1.Controls.Add(this.lblTitle);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(696, 448);
		this.panel1.TabIndex = 0;
		appearance1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance1.BackColor2 = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance1.ForeColor = System.Drawing.Color.Red;
		this.lblUseDatabase.Appearance = appearance1;
		this.lblUseDatabase.BackColor = System.Drawing.Color.White;
		this.lblUseDatabase.Font = new System.Drawing.Font("新細明體", 11.25f);
		this.lblUseDatabase.Location = new System.Drawing.Point(20, 416);
		this.lblUseDatabase.Name = "lblUseDatabase";
		this.lblUseDatabase.Size = new System.Drawing.Size(376, 23);
		this.lblUseDatabase.TabIndex = 27;
		this.lblUseDatabase.Text = "目前資料庫:";
		this.lblUseDatabase.Visible = false;
		appearance2.Image = resources.GetObject("appearance2.Image");
		this.ultraButton2.Appearance = appearance2;
		this.ultraButton2.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ultraButton2.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton2.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton2.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton2.Location = new System.Drawing.Point(588, 412);
		this.ultraButton2.Name = "ultraButton2";
		this.ultraButton2.ShowFocusRect = false;
		this.ultraButton2.ShowOutline = false;
		this.ultraButton2.Size = new System.Drawing.Size(92, 28);
		this.ultraButton2.SupportThemes = false;
		this.ultraButton2.TabIndex = 9;
		this.ultraButton2.Text = "取消";
		this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel2.Controls.Add(this.c1FlexGrid1);
		this.panel2.Controls.Add(this.panel3);
		this.panel2.Location = new System.Drawing.Point(20, 84);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(660, 324);
		this.panel2.TabIndex = 8;
		this.c1FlexGrid1._ExcelFileName = "";
		this.c1FlexGrid1._ExcelSheeName = "";
		this.c1FlexGrid1._IsOpenExcelAfterExport = false;
		this.c1FlexGrid1.AllowEditing = false;
		this.c1FlexGrid1.BackColor = System.Drawing.Color.White;
		this.c1FlexGrid1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
		this.c1FlexGrid1.ColumnInfo = "5,0,0,0,0,95,Columns:0{Width:25;Name:\"IsData\";AllowDragging:False;AllowEditing:False;TextAlign:RightCenter;ImageAlign:CenterCenter;}\t1{Width:107;Name:\"ProjectCode\";AllowDragging:False;AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;}\t2{Width:320;Name:\"projCName\";AllowDragging:False;AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;}\t3{Width:170;Name:\"projAddress\";AllowDragging:False;AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;}\t4{Name:\"projEName\";Visible:False;DataType:System.String;TextAlign:LeftCenter;}\t";
		this.ultraToolbarsManager1.SetContextMenuUltra(this.c1FlexGrid1, "PopupMenuTool1");
		this.c1FlexGrid1.Cursor = System.Windows.Forms.Cursors.Hand;
		this.c1FlexGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.c1FlexGrid1.ExtendLastCol = true;
		this.c1FlexGrid1.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
		this.c1FlexGrid1.ForeColor = System.Drawing.SystemColors.WindowText;
		this.c1FlexGrid1.Location = new System.Drawing.Point(0, 36);
		this.c1FlexGrid1.Name = "c1FlexGrid1";
		this.c1FlexGrid1.Rows.Fixed = 0;
		this.c1FlexGrid1.Rows.MinSize = 25;
		this.c1FlexGrid1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.c1FlexGrid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.c1FlexGrid1.ShowToolTipOnNarrowColumn = false;
		this.c1FlexGrid1.Size = new System.Drawing.Size(658, 286);
		this.c1FlexGrid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection("Normal{Font:細明體, 11pt;BackColor:White;Border:Flat,1,Transparent,Both;}\tFixed{BackColor:Control;ForeColor:ControlText;Border:Flat,1,ControlDark,Both;}\tHighlight{BackColor:102, 153, 255;}\tFocus{BackColor:204, 236, 255;}\tSearch{BackColor:Highlight;ForeColor:HighlightText;}\tFrozen{BackColor:Beige;}\tEmptyArea{BackColor:232, 232, 232;Border:Flat,1,ControlDarkDark,Both;}\tGrandTotal{BackColor:Black;ForeColor:White;}\tSubtotal0{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal1{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal2{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal3{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal4{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal5{BackColor:ControlDarkDark;ForeColor:White;}\t");
		this.c1FlexGrid1.TabIndex = 8;
		this.c1FlexGrid1.MouseDown += new System.Windows.Forms.MouseEventHandler(c1FlexGrid1_MouseDown);
		this.c1FlexGrid1.MouseMove += new System.Windows.Forms.MouseEventHandler(c1FlexGrid1_MouseMove);
		this.c1FlexGrid1.MouseEnter += new System.EventHandler(FormBudgetProjectPick_Load);
		this.panel3.Controls.Add(this.ultraLabel5);
		this.panel3.Controls.Add(this.ultraLabel4);
		this.panel3.Controls.Add(this.ultraLabel6);
		this.panel3.Controls.Add(this.ultraLabel3);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel3.Location = new System.Drawing.Point(0, 0);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(658, 36);
		this.panel3.TabIndex = 7;
		appearance3.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		appearance3.BackColor2 = System.Drawing.Color.FromArgb(225, 247, 223);
		appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance3.FontData.Name = "細明體";
		appearance3.FontData.SizeInPoints = 11f;
		appearance3.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance3.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel5.Appearance = appearance3;
		this.ultraLabel5.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ultraLabel5.Location = new System.Drawing.Point(452, 0);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(206, 36);
		this.ultraLabel5.TabIndex = 2;
		this.ultraLabel5.Text = "工程地址";
		appearance4.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		appearance4.BackColor2 = System.Drawing.Color.FromArgb(225, 247, 223);
		appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance4.FontData.Name = "細明體";
		appearance4.FontData.SizeInPoints = 11f;
		appearance4.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance4.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel4.Appearance = appearance4;
		this.ultraLabel4.Dock = System.Windows.Forms.DockStyle.Left;
		this.ultraLabel4.Location = new System.Drawing.Point(132, 0);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(320, 36);
		this.ultraLabel4.TabIndex = 1;
		this.ultraLabel4.Text = "工程名稱";
		appearance5.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		appearance5.BackColor2 = System.Drawing.Color.FromArgb(225, 247, 223);
		appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance5.FontData.Name = "細明體";
		appearance5.FontData.SizeInPoints = 11f;
		appearance5.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance5.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel6.Appearance = appearance5;
		this.ultraLabel6.Dock = System.Windows.Forms.DockStyle.Left;
		this.ultraLabel6.Location = new System.Drawing.Point(28, 0);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(104, 36);
		this.ultraLabel6.TabIndex = 3;
		this.ultraLabel6.Text = "工程代碼";
		appearance6.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		appearance6.BackColor2 = System.Drawing.Color.FromArgb(225, 247, 223);
		appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance6.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance6.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel3.Appearance = appearance6;
		this.ultraLabel3.Dock = System.Windows.Forms.DockStyle.Left;
		this.ultraLabel3.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(28, 36);
		this.ultraLabel3.TabIndex = 0;
		appearance7.FontData.SizeInPoints = 11f;
		this.cbFind.Appearance = appearance7;
		this.cbFind.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
		appearance8.BackColor = System.Drawing.Color.FromArgb(153, 204, 255);
		this.cbFind.ButtonAppearance = appearance8;
		this.cbFind.ButtonStyle = Infragistics.Win.UIElementButtonStyle.PopupBorderless;
		this.cbFind.Location = new System.Drawing.Point(520, 57);
		this.cbFind.Name = "cbFind";
		this.cbFind.Size = new System.Drawing.Size(137, 20);
		this.cbFind.TabIndex = 7;
		this.cbFind.Text = null;
		this.cbFind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(cbFind_KeyPress);
		this.cbFind.MouseEnter += new System.EventHandler(cbFind_MouseEnter);
		this.cbFind.MouseLeave += new System.EventHandler(cbFind_MouseLeave);
		appearance9.Image = 0;
		this.ultraButton1.Appearance = appearance9;
		this.ultraButton1.AutoSize = true;
		this.ultraButton1.BackColor = System.Drawing.Color.Transparent;
		this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.PopupBorderless;
		this.ultraButton1.ImageList = this.imageList1;
		this.ultraButton1.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton1.Location = new System.Drawing.Point(656, 55);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.ShowFocusRect = false;
		this.ultraButton1.ShowOutline = false;
		this.ultraButton1.Size = new System.Drawing.Size(24, 24);
		this.ultraButton1.TabIndex = 3;
		this.ultraButton1.Click += new System.EventHandler(ultraButton1_Click);
		this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
		this.imageList1.ImageSize = new System.Drawing.Size(20, 20);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
		this.ultraLabel2.Location = new System.Drawing.Point(484, 61);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(40, 20);
		this.ultraLabel2.TabIndex = 1;
		this.ultraLabel2.Text = "尋找:";
		appearance10.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance10.BackColor2 = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance10.FontData.Name = "新細明體";
		appearance10.FontData.SizeInPoints = 12f;
		appearance10.ForeColor = System.Drawing.Color.White;
		appearance10.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance10.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblTitle.Appearance = appearance10;
		this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
		this.lblTitle.Font = new System.Drawing.Font("細明體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblTitle.Location = new System.Drawing.Point(0, 0);
		this.lblTitle.Name = "lblTitle";
		this.lblTitle.Size = new System.Drawing.Size(694, 48);
		this.lblTitle.TabIndex = 0;
		this.lblTitle.Text = "PCCES Win 4.3  預算書專案挑選";
		this.imageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
		this.imageList2.ImageSize = new System.Drawing.Size(16, 16);
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.White;
		this.ultraToolbarsManager1.DockWithinContainer = this;
		this.ultraToolbarsManager1.ShowFullMenusDelay = 500;
		ultraToolbar1.DockedColumn = 0;
		ultraToolbar1.DockedRow = 0;
		ultraToolbar1.Text = "UltraToolbar1";
		ultraToolbar1.Visible = false;
		this.ultraToolbarsManager1.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[1] { ultraToolbar1 });
		popupMenuTool1.SharedProps.Caption = "PopupMenuTool1";
		popupMenuTool1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[1] { buttonTool1 });
		appearance11.Image = resources.GetObject("appearance11.Image");
		buttonTool2.SharedProps.AppearancesSmall.Appearance = appearance11;
		buttonTool2.SharedProps.Caption = "刪除標單";
		this.ultraToolbarsManager1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[2] { popupMenuTool1, buttonTool2 });
		this.ultraToolbarsManager1.Visible = false;
		this.ultraToolbarsManager1.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(ultraToolbarsManager1_ToolClick);
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.White;
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 0);
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Left.Name = "_FormBudgetProjectPick_Toolbars_Dock_Area_Left";
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 448);
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.White;
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(696, 0);
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Right.Name = "_FormBudgetProjectPick_Toolbars_Dock_Area_Right";
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 448);
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Right.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.White;
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Top.Name = "_FormBudgetProjectPick_Toolbars_Dock_Area_Top";
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(696, 0);
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Top.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.White;
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 448);
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Bottom.Name = "_FormBudgetProjectPick_Toolbars_Dock_Area_Bottom";
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(696, 0);
		this._FormBudgetProjectPick_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ultraToolbarsManager1;
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		this.BackColor = System.Drawing.Color.White;
		base.ClientSize = new System.Drawing.Size(696, 448);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this._FormBudgetProjectPick_Toolbars_Dock_Area_Left);
		base.Controls.Add(this._FormBudgetProjectPick_Toolbars_Dock_Area_Right);
		base.Controls.Add(this._FormBudgetProjectPick_Toolbars_Dock_Area_Top);
		base.Controls.Add(this._FormBudgetProjectPick_Toolbars_Dock_Area_Bottom);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.KeyPreview = true;
		base.Name = "FormBudgetProjectPick";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "預算書專案挑選";
		base.Load += new System.EventHandler(FormBudgetProjectPick_Load);
		base.Activated += new System.EventHandler(FormBudgetProjectPick_Activated);
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid1).EndInit();
		this.panel3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.cbFind).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).EndInit();
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

	public FormBudgetProjectPick()
	{
		InitializeComponent();
		CellStyle cs = c1FlexGrid1.Styles.Add("img");
		cs.DataType = typeof(Image);
	}

	private void FormBudgetProjectPick_Load(object sender, EventArgs e)
	{
		if (F_ActionName == PccesFormAction.BID)
		{
			lblTitle.Text = "PCCES Win 4.3  標單專案挑選";
		}
		if (F_ActionName == PccesFormAction.BudgetChange)
		{
			lblTitle.Text = "PCCES Win 4.3  契約變更專案挑選(已核定契約列表)";
		}
		if (F_ActionName == PccesFormAction.SplitContract)
		{
			lblTitle.Text = "PCCES Win 4.3  契約編輯專案挑選";
		}
		if (F_ActionName == PccesFormAction.Invoice)
		{
			lblTitle.Text = "PCCES Win 4.3  計價契約挑選(已核定契約列表)";
		}
		if (F_ActionName == PccesFormAction.SubClose)
		{
			lblTitle.Text = "PCCES Win 4.3  結算契約挑選(已核定契約列表)";
		}
		if (F_ActionName == PccesFormAction.SubFinal)
		{
			lblTitle.Text = "PCCES Win 4.3  決算契約挑選(已核定契約列表)";
		}
		if (F_ActionName == PccesFormAction.CNT)
		{
			lblTitle.Text = "PCCES Win 4.3  (投)標單專案挑選";
			lblTitle.Appearance.BackColor = Color.ForestGreen;
		}
		if (F_IsAddOn == "BID")
		{
			SysUser oSysUser = new SysUser();
			string DatabaseDesc = oSysUser.GetSysUserDatabaseDesc(F_UserID);
			if (DatabaseDesc.Trim() != "")
			{
				lblUseDatabase.Text = "目前資料庫:【" + DatabaseDesc.Trim() + "】";
				lblUseDatabase.Visible = true;
			}
			ultraToolbarsManager1.Visible = true;
			ultraToolbarsManager1.Tools["mnuDelete"].SharedProps.Visible = true;
		}
		else
		{
			ultraToolbarsManager1.Visible = false;
			ultraToolbarsManager1.Tools["mnuDelete"].SharedProps.Visible = false;
		}
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add(CommonMethods.GetFormTypeTitle(FormType.Budget));
		if (F_ActionName == PccesFormAction.Invoice)
		{
			subProject subProject1 = new subProject(aArr);
			DT1 = subProject1.ListItem(" flag = 'Y' ");
		}
		else if (F_ActionName == PccesFormAction.BudgetChange)
		{
			subProject subProject1 = new subProject(aArr);
			DT1 = subProject1.ListItem(" flag = 'Y' ");
		}
		else if (F_ActionName == PccesFormAction.SplitContract)
		{
			Archnowledge.Pcces.BUDClass.PubProject pubProject1 = new Archnowledge.Pcces.BUDClass.PubProject(aArr);
			DT1 = pubProject1.ListItemSub(" c.projectcode is not null or a.subflag = 'Y' ");
		}
		else if (F_ActionName == PccesFormAction.SubClose)
		{
			subProject subProject1 = new subProject(aArr);
			DT1 = subProject1.ListItem(" flag = 'Y' ");
		}
		else if (F_ActionName == PccesFormAction.SubFinal)
		{
			subProject subProject1 = new subProject(aArr);
			DT1 = subProject1.ListItem(" flag = 'Y' ");
		}
		else if (F_ActionName == PccesFormAction.SubChange)
		{
			subProject subProject1 = new subProject(aArr);
			DT1 = subProject1.ListItem(" flag = 'Y' ");
		}
		else if (F_ActionName == PccesFormAction.CNT)
		{
			Archnowledge.Pcces.BUDClass.PubProject dbProject = new Archnowledge.Pcces.BUDClass.PubProject(aArr);
			Archnowledge.Pcces.DomainModule.General.PubProject pubProject2 = new Archnowledge.Pcces.DomainModule.General.PubProject();
			DataSet dsProject = pubProject2.GetProjectList(F_UserID);
			DT1 = dsProject.Tables[0];
			DataView view = new DataView(DT1, "ProjectCode like '" + F_CurrentEditProjectCode + "%'", "", DataViewRowState.CurrentRows);
			DT1 = view.ToTable();
		}
		else
		{
			Archnowledge.Pcces.BUDClass.PubProject dbProject = new Archnowledge.Pcces.BUDClass.PubProject(aArr);
			if (F_IsAddOn == "BID")
			{
				DT1 = dbProject.ListItem(" a.subflag is null and b.projectCode is not null ");
			}
			else
			{
				Archnowledge.Pcces.DomainModule.General.PubProject pubProject2 = new Archnowledge.Pcces.DomainModule.General.PubProject();
				DataSet dsProject = pubProject2.GetProjectList(F_UserID);
				DT1 = dsProject.Tables[0];
			}
		}
		if (F_IsAddOn == "BID")
		{
			BindDataAddOnIntoGrid();
		}
		else if (F_ActionName == PccesFormAction.CNT)
		{
			BindDataIntoGridOnlyBid();
		}
		else
		{
			BindDataIntoGrid();
		}
	}

	private void BindDataIntoGridOnlyBid()
	{
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = F_UserID;
		DataTable DT_UsrProj = DBCLS.GetUserProjectList(F_UserID, "");
		DataView DV_IsrProj = DT_UsrProj.DefaultView;
		DV_IsrProj.RowFilter = "Bid is not null and ProjectCode Like '" + F_CurrentEditProjectCode + "%'";
		DV_IsrProj.Sort = " ProjectCode Asc ";
		c1FlexGrid1.Cols["projCName"].Style.WordWrap = true;
		c1FlexGrid1.Rows.Count = DT1.Rows.Count;
		CellStyle CS1 = c1FlexGrid1.Styles.Add("NoProjectAuth");
		CS1.ForeColor = Color.Gray;
		for (int i = 0; i < DT1.Rows.Count; i++)
		{
			if (F_ActionName == PccesFormAction.CNT)
			{
				if (DT1.Rows[i]["bid"].ToString().Trim() != "")
				{
					CellRange rg = c1FlexGrid1.GetCellRange(i, c1FlexGrid1.Cols["IsData"].SafeIndex);
					rg.Style = c1FlexGrid1.Styles["img"];
					if (PubTools.Str2DateTime(DT1.Rows[i]["CloseBidDate"]) != Convert.ToDateTime("1800/1/1"))
					{
						rg.Image = imageList2.Images[1];
					}
					else
					{
						rg.Image = imageList2.Images[0];
					}
				}
				else
				{
					c1FlexGrid1.Rows[i].Visible = false;
				}
			}
			if (F_ActionName == PccesFormAction.SplitContract)
			{
				if (DT1.Rows[i]["subflag"].ToString().Trim() != "Y")
				{
					c1FlexGrid1[i, "ProjectCode"] = DT1.Rows[i]["projectCode"].ToString().Trim();
					c1FlexGrid1[i, "projCName"] = DT1.Rows[i]["projCName"].ToString().Trim();
					c1FlexGrid1[i, "projEName"] = DT1.Rows[i]["projEName"].ToString().Trim();
					c1FlexGrid1[i, "projAddress"] = DT1.Rows[i]["projAddress"].ToString().Trim();
				}
				else
				{
					c1FlexGrid1[i, "ProjectCode"] = DT1.Rows[i]["projectCode"].ToString().Trim();
					c1FlexGrid1[i, "projCName"] = DT1.Rows[i]["dName"].ToString().Trim();
					c1FlexGrid1[i, "projEName"] = DT1.Rows[i]["projEName"].ToString().Trim();
					c1FlexGrid1[i, "projAddress"] = DT1.Rows[i]["dAddress"].ToString().Trim();
				}
			}
			else
			{
				c1FlexGrid1[i, "ProjectCode"] = DT1.Rows[i]["projectCode"].ToString().Trim();
				c1FlexGrid1[i, "projCName"] = DT1.Rows[i]["projCName"].ToString().Trim();
				c1FlexGrid1[i, "projEName"] = DT1.Rows[i]["projEName"].ToString().Trim();
				c1FlexGrid1[i, "projAddress"] = DT1.Rows[i]["projAddress"].ToString().Trim();
			}
			if (DV_IsrProj.Find(c1FlexGrid1[i, "ProjectCode"].ToString()) == -1)
			{
				c1FlexGrid1.Rows[i].Style = c1FlexGrid1.Styles["NoProjectAuth"];
			}
			c1FlexGrid1.AutoSizeRow(i);
		}
	}

	private void BindDataIntoGrid()
	{
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = F_UserID;
		DataTable DT_UsrProj = DBCLS.GetUserProjectList(F_UserID, "");
		DataView DV_IsrProj = DT_UsrProj.DefaultView;
		DV_IsrProj.Sort = " ProjectCode Asc ";
		c1FlexGrid1.Cols["projCName"].Style.WordWrap = true;
		c1FlexGrid1.Rows.Count = DT1.Rows.Count;
		CellStyle CS1 = c1FlexGrid1.Styles.Add("NoProjectAuth");
		CS1.ForeColor = Color.Gray;
		for (int i = 0; i < DT1.Rows.Count; i++)
		{
			if (F_ActionName == PccesFormAction.BUD)
			{
				if (DT1.Rows[i]["bud"].ToString().Trim() != "")
				{
					CellRange rg = c1FlexGrid1.GetCellRange(i, c1FlexGrid1.Cols["IsData"].SafeIndex);
					rg.Style = c1FlexGrid1.Styles["img"];
					rg.Image = imageList2.Images[0];
				}
				else
				{
					c1FlexGrid1.Rows[i].Visible = false;
				}
			}
			else if (F_ActionName == PccesFormAction.BID)
			{
				if (DT1.Rows[i]["bid"].ToString().Trim() != "")
				{
					CellRange rg = c1FlexGrid1.GetCellRange(i, c1FlexGrid1.Cols["IsData"].SafeIndex);
					rg.Style = c1FlexGrid1.Styles["img"];
					if (PubTools.Str2DateTime(DT1.Rows[i]["CloseBidDate"]) != Convert.ToDateTime("1800/1/1"))
					{
						rg.Image = imageList2.Images[1];
					}
					else
					{
						rg.Image = imageList2.Images[0];
					}
				}
				else
				{
					c1FlexGrid1.Rows[i].Visible = false;
				}
			}
			if (F_ActionName == PccesFormAction.SplitContract)
			{
				if (DT1.Rows[i]["subflag"].ToString().Trim() != "Y")
				{
					c1FlexGrid1[i, "ProjectCode"] = DT1.Rows[i]["projectCode"].ToString().Trim();
					c1FlexGrid1[i, "projCName"] = DT1.Rows[i]["projCName"].ToString().Trim();
					c1FlexGrid1[i, "projEName"] = DT1.Rows[i]["projEName"].ToString().Trim();
					c1FlexGrid1[i, "projAddress"] = DT1.Rows[i]["projAddress"].ToString().Trim();
				}
				else
				{
					c1FlexGrid1[i, "ProjectCode"] = DT1.Rows[i]["projectCode"].ToString().Trim();
					c1FlexGrid1[i, "projCName"] = DT1.Rows[i]["dName"].ToString().Trim();
					c1FlexGrid1[i, "projEName"] = DT1.Rows[i]["projEName"].ToString().Trim();
					c1FlexGrid1[i, "projAddress"] = DT1.Rows[i]["dAddress"].ToString().Trim();
				}
			}
			else
			{
				c1FlexGrid1[i, "ProjectCode"] = DT1.Rows[i]["projectCode"].ToString().Trim();
				c1FlexGrid1[i, "projCName"] = DT1.Rows[i]["projCName"].ToString().Trim();
				c1FlexGrid1[i, "projEName"] = DT1.Rows[i]["projEName"].ToString().Trim();
				c1FlexGrid1[i, "projAddress"] = DT1.Rows[i]["projAddress"].ToString().Trim();
			}
			if (DV_IsrProj.Find(c1FlexGrid1[i, "ProjectCode"].ToString()) == -1)
			{
				c1FlexGrid1.Rows[i].Style = c1FlexGrid1.Styles["NoProjectAuth"];
			}
			c1FlexGrid1.AutoSizeRow(i);
		}
	}

	private void BindDataAddOnIntoGrid()
	{
		CellStyle CSBID = c1FlexGrid1.Styles.Add("RecentBID");
		CSBID.BackColor = Color.Moccasin;
		string sBIDProj = CommonMethods.GetIniValue("RecentFile", "BIDProject");
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = F_UserID;
		DataTable DT_UsrProj = DBCLS.GetUserProjectList(F_UserID, "b.projectCode is not null");
		DataView DV_IsrProj = DT_UsrProj.DefaultView;
		DV_IsrProj.Sort = " ProjectCode Asc ";
		c1FlexGrid1.Cols["projCName"].Style.WordWrap = true;
		c1FlexGrid1.Rows.Count = DT1.Rows.Count;
		CellStyle CS1 = c1FlexGrid1.Styles.Add("NoProjectAuth");
		CS1.ForeColor = Color.Gray;
		for (int i = 0; i < DT1.Rows.Count; i++)
		{
			if (DT1.Rows[i]["bid"].ToString().Trim() != "")
			{
				CellRange rg = c1FlexGrid1.GetCellRange(i, c1FlexGrid1.Cols["IsData"].SafeIndex);
				rg.Style = c1FlexGrid1.Styles["img"];
				if (PubTools.Str2DateTime(DT1.Rows[i]["CloseBidDate"]) != Convert.ToDateTime("1800/1/1"))
				{
					rg.Image = imageList2.Images[1];
				}
				else
				{
					rg.Image = imageList2.Images[0];
				}
				c1FlexGrid1[i, "ProjectCode"] = DT1.Rows[i]["projectCode"].ToString().Trim();
				c1FlexGrid1[i, "projCName"] = DT1.Rows[i]["projCName"].ToString().Trim();
				c1FlexGrid1[i, "projEName"] = DT1.Rows[i]["projEName"].ToString().Trim();
				c1FlexGrid1[i, "projAddress"] = DT1.Rows[i]["projAddress"].ToString().Trim();
			}
			if (DV_IsrProj.Find(c1FlexGrid1[i, "ProjectCode"].ToString()) == -1)
			{
				c1FlexGrid1.Rows[i].Style = c1FlexGrid1.Styles["NoProjectAuth"];
			}
			if (DT1.Rows[i]["projectCode"].ToString().Trim() == sBIDProj)
			{
				CellRange rgBID = c1FlexGrid1.GetCellRange(i, 0, i, c1FlexGrid1.Cols["projAddress"].SafeIndex);
				rgBID.Style = CSBID;
			}
			c1FlexGrid1.AutoSizeRow(i);
		}
	}

	private void ultraButton1_Click(object sender, EventArgs e)
	{
		if (cbFind.Text == null || c1FlexGrid1.Rows.Count <= 1)
		{
			return;
		}
		int iStart = c1FlexGrid1.Row + 1;
		string sSearchText = cbFind.Text.Trim();
		if (!CommonMethods.CheckValidString(sSearchText))
		{
			return;
		}
		if (F_KeyWord != sSearchText.Trim())
		{
			iStart = 1;
			F_KeyWord = sSearchText.Trim();
		}
		else
		{
			iStart = c1FlexGrid1.Row + 1;
		}
		if (sSearchText.Trim() == "")
		{
			return;
		}
		for (int i = iStart; i < c1FlexGrid1.Rows.Count; i++)
		{
			for (int j = 1; j < c1FlexGrid1.Cols.Count; j++)
			{
				if (c1FlexGrid1[i, j] == null || c1FlexGrid1[i, j].ToString().ToUpper().IndexOf(sSearchText.ToUpper()) <= -1)
				{
					continue;
				}
				c1FlexGrid1.Row = i;
				c1FlexGrid1.Select();
				int iFondCount = 0;
				int iListCount = cbFind.Items.Count;
				for (int k = 0; k < iListCount; k++)
				{
					if (cbFind.Items[k].DisplayText.Trim() == sSearchText.Trim())
					{
						iFondCount++;
					}
				}
				if (iFondCount == 0)
				{
					cbFind.Items.Add(sSearchText, sSearchText);
				}
				return;
			}
		}
	}

	private void cbFind_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			ultraButton1_Click(sender, e);
		}
	}

	private void c1FlexGrid1_MouseMove(object sender, MouseEventArgs e)
	{
		c1FlexGrid1.Row = c1FlexGrid1.MouseRow;
	}

	private void OpenBudget(string sProjectCode, string sProjectName, string sMainProjectCode)
	{
		bool IsFormExist = false;
		Form[] mdiChildren = base.Owner.MdiChildren;
		foreach (Form frm in mdiChildren)
		{
			if (!(frm is FormPanel) && !(frm is FormPanel2) && !(frm is FormPanel3))
			{
				try
				{
					frm.Close();
					frm.Dispose();
				}
				catch (Exception ex)
				{
					CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudgetProjectPick.cs" + ex.Message);
				}
			}
		}
		if (IsFormExist)
		{
			return;
		}
		frmBudget FM_BDGT = new frmBudget();
		FM_BDGT.ProjectCode = sProjectCode;
		FM_BDGT.ProjectName = sProjectName;
		FM_BDGT._ActionName = F_ActionName;
		FM_BDGT.MdiParent = base.Owner;
		FM_BDGT._UserID = (base.Owner as frmPccesMain)._UserID;
		FM_BDGT._UserName = (base.Owner as frmPccesMain)._UserName;
		FM_BDGT._ServerName = (base.Owner as frmPccesMain)._ServerName;
		FM_BDGT._FunctionName = ((F_ActionName == PccesFormAction.BUD) ? "BUD" : "BID");
		FM_BDGT._MainProjectCode = sMainProjectCode;
		FM_BDGT._HasRegistered = HasRegistered();
		FM_BDGT.Show();
		F_FormBudget = FM_BDGT;
		base.DialogResult = DialogResult.OK;
		mdiChildren = base.Owner.MdiChildren;
		foreach (Form frm in mdiChildren)
		{
			if (frm is FormPanel2)
			{
				frm.Close();
				frm.Dispose();
			}
		}
		(base.Owner as frmPccesMain).LeftPanel.Width = 0;
		Close();
	}

	private void OpenBudgetWizard(string sProjectCode, string sProjectName, string sMainProjectCode)
	{
		bool IsFormExist = false;
		Form[] mdiChildren = base.Owner.MdiChildren;
		foreach (Form frm in mdiChildren)
		{
			if (!(frm is FormPanel) && !(frm is FormPanel2) && !(frm is FormPanel3))
			{
				try
				{
					frm.Close();
					frm.Dispose();
				}
				catch (Exception ex)
				{
					CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudgetProjectPick.cs" + ex.Message);
				}
			}
		}
		if (IsFormExist)
		{
			return;
		}
		ArrayList aArrb = new ArrayList();
		aArrb.Clear();
		aArrb.Add(F_UserID);
		ItemA ItemACom = new ItemA(aArrb);
		ItemACom.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		double org_Amount = ItemACom.GetAmount(sProjectCode);
		if (org_Amount == 0.0)
		{
			string ssWarning;
			if (F_ActionName != PccesFormAction.BUD)
			{
				ssWarning = "總價為 0 時，不可轉出投標單。";
				MessageBox.Show(this, ssWarning, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			ssWarning = "此專案目前總金額為 0 \n\n是否繼續執行[製作電子檔]。\n\n";
			if (MessageBox.Show(this, ssWarning, "警示", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
			{
				return;
			}
		}
		string IPStr = CommonMethods.GetIPAddress();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("預算--讀取預算書基本資料--" + sProjectCode + "(" + IPStr + ")");
		Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
		PROJ.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		DT1 = PROJ.ListItem("", sProjectCode);
		MainUnitCom MAIN_UCOM = new MainUnitCom(aArr);
		string sDeptName = MAIN_UCOM.Get_Main_Name(DT1.Rows[0]["mainCName"].ToString().Trim());
		if (sDeptName.Trim() == "")
		{
			sDeptName = MAIN_UCOM.Get_Main_Name(DT1.Rows[0]["mainCode"].ToString().Trim());
		}
		if (sDeptName.Trim() != "")
		{
			if (sDeptName.Trim() == DT1.Rows[0]["mainCode"].ToString().Trim())
			{
				MessageBox.Show(this, "請檢查主辦機關維護是否無此項" + DT1.Rows[0]["mainCode"].ToString().Trim() + "機關代碼\n\n若無請至【系統維護】-->【主辦單位維護】新增或匯入最新主辦機關資料", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			string sDeptEName = MAIN_UCOM.Get_Main_EName(DT1.Rows[0]["mainCode"].ToString().Trim());
			FormBudgetExp_Wzd FM_BDGT_EXP_WZD = new FormBudgetExp_Wzd();
			FM_BDGT_EXP_WZD._UserID = F_UserID;
			FM_BDGT_EXP_WZD._ActionName = F_ActionName;
			FM_BDGT_EXP_WZD._ProjectCode = sProjectCode;
			FM_BDGT_EXP_WZD._DeptName = sDeptName;
			FM_BDGT_EXP_WZD._DeptEName = sDeptEName;
			FM_BDGT_EXP_WZD._ProjectNameC = DT1.Rows[0]["projectNameC"].ToString().Trim();
			FM_BDGT_EXP_WZD._ProjectNameE = DT1.Rows[0]["projectNameE"].ToString().Trim();
			FM_BDGT_EXP_WZD._ProjectAddress = DT1.Rows[0]["projectAddress"].ToString().Trim();
			FM_BDGT_EXP_WZD._ProjectEngAddress = "";
			FM_BDGT_EXP_WZD._MainProjectCode = sMainProjectCode;
			FM_BDGT_EXP_WZD._AccountCode1 = DT1.Rows[0]["accountCode1"].ToString().Trim();
			FM_BDGT_EXP_WZD._AccountCode2 = DT1.Rows[0]["accountCode2"].ToString().Trim();
			Hide();
			FM_BDGT_EXP_WZD.ShowDialog(this);
			FM_BDGT_EXP_WZD.Close();
			FM_BDGT_EXP_WZD.Dispose();
			FM_BDGT_EXP_WZD = null;
			PROJ = null;
			MAIN_UCOM = null;
		}
		else
		{
			MessageBox.Show(this, "主辦機關無資料 \n\n 請至【標單資訊】-->【專案基本資訊】中挑選,並告知業主", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void OpenBudgetReport(string sProjectCode, string sProjectName, string sMainProjectCode)
	{
		string IPStr = CommonMethods.GetIPAddress();
		Cursor = Cursors.WaitCursor;
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add("預算書編輯--專案資料讀取--" + sProjectCode + "(" + IPStr + ")");
		Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
		PROJ.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		DataTable DT_PROJ = PROJ.ListItem("", sProjectCode);
		MainUnitCom MAIN_UCOM = new MainUnitCom(aArr);
		string sDeptNameC = "";
		if (DT_PROJ.Rows.Count > 0)
		{
			sDeptNameC = MAIN_UCOM.Get_Main_Name(DT_PROJ.Rows[0]["mainCName"].ToString().Trim());
		}
		if (sDeptNameC.Trim() == "")
		{
			sDeptNameC = MAIN_UCOM.Get_Main_Name(DT_PROJ.Rows[0]["mainCode"].ToString().Trim());
		}
		if (sDeptNameC.Trim() != "")
		{
			if (sDeptNameC.Trim() == DT_PROJ.Rows[0]["mainCode"].ToString().Trim())
			{
				MessageBox.Show(this, "請檢查主辦機關維護是否無此項" + DT_PROJ.Rows[0]["mainCode"].ToString().Trim() + "機關代碼\n\n若無請至【系統維護】-->【主辦單位維護】新增或匯入最新主辦機關資料", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			string sDeptNameE = MAIN_UCOM.Get_Main_EName(DT_PROJ.Rows[0]["mainCode"].ToString().Trim());
			FormReportViewer FM_RPT = new FormReportViewer();
			FM_RPT._UserID = F_UserID;
			FM_RPT._ActionName = F_ActionName;
			FM_RPT._ProjectCode = sProjectCode;
			FM_RPT._ProjectNameC = sProjectName;
			FM_RPT._ProjectNameE = DT_PROJ.Rows[0]["projectNameE"].ToString();
			FM_RPT._ProjectAddress = DT_PROJ.Rows[0]["projectAddress"].ToString();
			FM_RPT._ProjectAccount1 = DT_PROJ.Rows[0]["accountCode1"].ToString();
			FM_RPT._ProjectAccount2 = DT_PROJ.Rows[0]["accountCode2"].ToString();
			FM_RPT._CompanyNameC = sDeptNameC;
			FM_RPT._CompanyNameE = sDeptNameE;
			Cursor = Cursors.Default;
			FM_RPT.ShowDialog(this);
			FM_RPT.Close();
			FM_RPT.Dispose();
			FM_RPT = null;
			aArr = null;
			PROJ = null;
			MAIN_UCOM = null;
			DT_PROJ = null;
		}
		else
		{
			MessageBox.Show(this, "主辦機關無資料 \n\n 請至【標單資訊】-->【專案基本資訊】中挑選,並告知業主", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private bool HasRegistered()
	{
		return (CommonMethods.GetIniValue("Register", "RegID").Trim() != "") ? true : false;
	}

	private void SwitchBudget(string sProjectCode, string sProjectName, string sMainProjectCode)
	{
		F_Istemplate = GetIsTemplate(sProjectCode);
		string srckind = CommonMethods.GetActionNameString(F_ActionName);
		if (srckind == "BID")
		{
			F_Istemplate = false;
		}
		Form ActiveForm = base.Owner.ActiveMdiChild;
		if (ActiveForm is frmBudget)
		{
			(ActiveForm as frmBudget).ProjectCode = sProjectCode;
			(ActiveForm as frmBudget).ProjectName = sProjectName;
			(ActiveForm as frmBudget)._ActionName = F_ActionName;
			(ActiveForm as frmBudget)._MainProjectCode = sMainProjectCode;
			(ActiveForm as frmBudget)._Istemplate = F_Istemplate;
			base.DialogResult = DialogResult.OK;
			Close();
		}
	}

	private bool IsExistInBudProject(string sProjCode)
	{
		bool RetV = false;
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add(CommonMethods.GetFormTypeTitle(FormType.Budget));
		Archnowledge.Pcces.BUDClass.Project dbProject = new Archnowledge.Pcces.BUDClass.Project(aArr);
		dbProject.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		DT_Temp = dbProject.ListItem("", sProjCode);
		if (DT_Temp.Rows.Count > 0)
		{
			RetV = true;
		}
		dbProject = null;
		return RetV;
	}

	private void cbFind_MouseEnter(object sender, EventArgs e)
	{
		cbFind.ButtonAppearance.BackColor = Color.FromArgb(196, 210, 236);
		cbFind.BorderStyle = UIElementBorderStyle.Solid;
	}

	private void cbFind_MouseLeave(object sender, EventArgs e)
	{
		cbFind.ButtonAppearance.BackColor = Color.FromArgb(153, 204, 255);
		cbFind.BorderStyle = UIElementBorderStyle.None;
	}

	private void FormBudgetProjectPick_Activated(object sender, EventArgs e)
	{
		c1FlexGrid1.Select();
	}

	private void c1FlexGrid1_MouseDown(object sender, MouseEventArgs e)
	{
		if (c1FlexGrid1.Row == -1 || e.Button == MouseButtons.Right)
		{
			return;
		}
		string sProjectCode = c1FlexGrid1[c1FlexGrid1.Row, "ProjectCode"].ToString().Trim();
		string sProjectName = c1FlexGrid1[c1FlexGrid1.Row, "projCName"].ToString().Trim();
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = F_UserID;
		if (!DBCLS.GetProjectAuthority(F_UserID, sProjectCode))
		{
			MessageBox.Show(this, "這個專案您沒有權限，無法開啟。", "專案權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add("專案挑選--讀取主專案");
		Archnowledge.Pcces.BUDClass.Project ProjCom = new Archnowledge.Pcces.BUDClass.Project(aArr);
		ProjCom.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		string sMainProjectCode = ProjCom.GetMainProj(sProjectCode).Trim();
		if (sMainProjectCode == "-1")
		{
			sMainProjectCode = "";
		}
		Form ActiveForm = base.Owner.ActiveMdiChild;
		if (F_ActionName == PccesFormAction.CNT)
		{
			F_SelectedProjectCode = sProjectCode;
			base.DialogResult = DialogResult.OK;
		}
		if (F_ActionName == PccesFormAction.BUD || F_ActionName == PccesFormAction.BID)
		{
			if (IsExistInBudProject(sProjectCode))
			{
				if (F_CallUpType == FormBudget_PickType.NewBudget)
				{
					if (F_Mode == "2" && F_IsAddOn == "BID")
					{
						OpenBudgetWizard(sProjectCode, sProjectName, sMainProjectCode);
					}
					else if (F_Mode == "3" && F_IsAddOn == "BID")
					{
						OpenBudgetReport(sProjectCode, sProjectName, sMainProjectCode);
					}
					else
					{
						OpenBudget(sProjectCode, sProjectName, sMainProjectCode);
					}
				}
				else
				{
					SwitchBudget(sProjectCode, sProjectName, sMainProjectCode);
				}
				return;
			}
			FormBudgetProjectInfo FM_BDGT_PINFO = new FormBudgetProjectInfo();
			FM_BDGT_PINFO._UserID = F_UserID;
			FM_BDGT_PINFO._OpenMode = BudgetInfoForm_OpenMode.NewBudget;
			FM_BDGT_PINFO._ProjectCode = sProjectCode;
			FM_BDGT_PINFO._ProjectNameC = sProjectName;
			FM_BDGT_PINFO._ProjectNameE = c1FlexGrid1[c1FlexGrid1.Row, "projEName"].ToString();
			FM_BDGT_PINFO._ProjectAddress = c1FlexGrid1[c1FlexGrid1.Row, "projAddress"].ToString();
			FM_BDGT_PINFO._ActionName = F_ActionName;
			DialogResult theResult = FM_BDGT_PINFO.ShowDialog(this);
			FM_BDGT_PINFO.Close();
			FM_BDGT_PINFO.Dispose();
			FM_BDGT_PINFO = null;
			if (theResult == DialogResult.OK)
			{
				OpenBudget(sProjectCode, sProjectName, sMainProjectCode);
				Close();
			}
		}
		else if (F_ActionName == PccesFormAction.BudgetChange)
		{
			if (F_CallUpType == FormBudget_PickType.ProjectSwitch)
			{
				if (ActiveForm is FormBudgetChange)
				{
					(ActiveForm as FormBudgetChange)._ProjectCode = sProjectCode;
					(ActiveForm as FormBudgetChange)._ProjectNameC = sProjectName;
					(ActiveForm as FormBudgetChange)._UserID = F_UserID;
					base.DialogResult = DialogResult.OK;
					Close();
				}
			}
			else
			{
				FormBudgetChange FM_BDGT_CHNG = new FormBudgetChange();
				FM_BDGT_CHNG._ProjectCode = sProjectCode;
				FM_BDGT_CHNG._ProjectNameC = sProjectName;
				FM_BDGT_CHNG._UserID = (base.Owner as frmPccesMain)._UserID;
				FM_BDGT_CHNG._UserName = (base.Owner as frmPccesMain)._UserName;
				FM_BDGT_CHNG._ServerName = (base.Owner as frmPccesMain)._ServerName;
				FM_BDGT_CHNG._HasRegistered = HasRegistered();
				FM_BDGT_CHNG.MdiParent = base.Owner;
				FM_BDGT_CHNG.Show();
			}
			base.DialogResult = DialogResult.OK;
			(base.Owner as frmPccesMain).LeftPanel.Width = 0;
			Close();
		}
		else if (F_ActionName == PccesFormAction.SubChange)
		{
			if (F_CallUpType == FormBudget_PickType.ProjectSwitch)
			{
				if (ActiveForm is FormBudgetChange)
				{
					(ActiveForm as FormBudgetChange)._ProjectCode = sProjectCode;
					(ActiveForm as FormBudgetChange)._ProjectNameC = sProjectName;
					(ActiveForm as FormBudgetChange)._UserID = F_UserID;
					base.DialogResult = DialogResult.OK;
					Close();
				}
			}
			else
			{
				FormBudgetChange FM_BDGT_CHNG = new FormBudgetChange();
				FM_BDGT_CHNG._ProjectCode = sProjectCode;
				FM_BDGT_CHNG._ProjectNameC = sProjectName;
				FM_BDGT_CHNG._UserID = (base.Owner as frmPccesMain)._UserID;
				FM_BDGT_CHNG._UserName = (base.Owner as frmPccesMain)._UserName;
				FM_BDGT_CHNG._ServerName = (base.Owner as frmPccesMain)._ServerName;
				FM_BDGT_CHNG._HasRegistered = HasRegistered();
				FM_BDGT_CHNG.MdiParent = base.Owner;
				FM_BDGT_CHNG.Show();
			}
			base.DialogResult = DialogResult.OK;
			(base.Owner as frmPccesMain).LeftPanel.Width = 0;
			Close();
		}
		else if (F_ActionName == PccesFormAction.SplitContract)
		{
			if (F_CallUpType == FormBudget_PickType.ProjectSwitch)
			{
				if (ActiveForm is FormSplitContract)
				{
					(ActiveForm as FormSplitContract).ProjectCode = sProjectCode;
					(ActiveForm as FormSplitContract)._ProjectNameC = sProjectName;
					(ActiveForm as FormSplitContract)._UserID = F_UserID;
					base.DialogResult = DialogResult.OK;
					Close();
				}
			}
			else
			{
				FormSplitContract FM_SPLT_CNT = new FormSplitContract();
				FM_SPLT_CNT.ProjectCode = sProjectCode;
				FM_SPLT_CNT._ProjectNameC = sProjectName;
				FM_SPLT_CNT._UserID = (base.Owner as frmPccesMain)._UserID;
				FM_SPLT_CNT._UserName = (base.Owner as frmPccesMain)._UserName;
				FM_SPLT_CNT._ServerName = (base.Owner as frmPccesMain)._ServerName;
				FM_SPLT_CNT._HasRegistered = HasRegistered();
				FM_SPLT_CNT.MdiParent = base.Owner;
				FM_SPLT_CNT.Show();
			}
			base.DialogResult = DialogResult.OK;
			(base.Owner as frmPccesMain).LeftPanel.Width = 0;
			Close();
		}
		else if (F_ActionName == PccesFormAction.Invoice)
		{
			if (F_CallUpType == FormBudget_PickType.ProjectSwitch)
			{
				if (ActiveForm is FormInvoice)
				{
					(ActiveForm as FormInvoice)._ProjectCode = sProjectCode;
					(ActiveForm as FormInvoice)._ProjectNameC = sProjectName;
					(ActiveForm as FormInvoice)._UserID = F_UserID;
					base.DialogResult = DialogResult.OK;
					Close();
				}
			}
			else
			{
				FormInvoice FM_INVC = new FormInvoice();
				FM_INVC._ProjectCode = sProjectCode;
				FM_INVC._ProjectNameC = sProjectName;
				FM_INVC._UserID = (base.Owner as frmPccesMain)._UserID;
				FM_INVC._UserName = (base.Owner as frmPccesMain)._UserName;
				FM_INVC._ServerName = (base.Owner as frmPccesMain)._ServerName;
				FM_INVC._HasRegistered = HasRegistered();
				FM_INVC.MdiParent = base.Owner;
				FM_INVC.Show();
			}
			base.DialogResult = DialogResult.OK;
			(base.Owner as frmPccesMain).LeftPanel.Width = 0;
			Close();
		}
		else if (F_ActionName == PccesFormAction.SubClose)
		{
			if (F_CallUpType == FormBudget_PickType.ProjectSwitch)
			{
				if (ActiveForm is FormSubClose)
				{
					(ActiveForm as FormSubClose)._ProjectCode = sProjectCode;
					(ActiveForm as FormSubClose)._ProjectNameC = sProjectName;
					(ActiveForm as FormSubClose)._UserID = F_UserID;
					base.DialogResult = DialogResult.OK;
					Close();
				}
			}
			else
			{
				FormSubClose FM_CLS = new FormSubClose();
				FM_CLS._ProjectCode = sProjectCode;
				FM_CLS._ProjectNameC = sProjectName;
				FM_CLS._UserID = (base.Owner as frmPccesMain)._UserID;
				FM_CLS._UserName = (base.Owner as frmPccesMain)._UserName;
				FM_CLS._ServerName = (base.Owner as frmPccesMain)._ServerName;
				FM_CLS._HasRegistered = HasRegistered();
				FM_CLS.MdiParent = base.Owner;
				FM_CLS.Show();
			}
			base.DialogResult = DialogResult.OK;
			(base.Owner as frmPccesMain).LeftPanel.Width = 0;
			Close();
		}
		else
		{
			if (F_ActionName != PccesFormAction.SubFinal)
			{
				return;
			}
			if (F_CallUpType == FormBudget_PickType.ProjectSwitch)
			{
				if (ActiveForm is FormSubFinal)
				{
					(ActiveForm as FormSubFinal)._ProjectCode = sProjectCode;
					(ActiveForm as FormSubFinal)._ProjectNameC = sProjectName;
					(ActiveForm as FormSubFinal)._UserID = F_UserID;
					base.DialogResult = DialogResult.OK;
					Close();
				}
			}
			else
			{
				FormSubFinal FM_CLS2 = new FormSubFinal();
				FM_CLS2._ProjectCode = sProjectCode;
				FM_CLS2._ProjectNameC = sProjectName;
				FM_CLS2._UserID = (base.Owner as frmPccesMain)._UserID;
				FM_CLS2._UserName = (base.Owner as frmPccesMain)._UserName;
				FM_CLS2._ServerName = (base.Owner as frmPccesMain)._ServerName;
				FM_CLS2._HasRegistered = HasRegistered();
				FM_CLS2.MdiParent = base.Owner;
				FM_CLS2.Show();
			}
			base.DialogResult = DialogResult.OK;
			(base.Owner as frmPccesMain).LeftPanel.Width = 0;
			Close();
		}
	}

	private void ultraToolbarsManager1_ToolClick(object sender, ToolClickEventArgs e)
	{
		string key = e.Tool.Key;
		if (key != null && key == "mnuDelete")
		{
			DoDeleteThisBDGT("BID");
		}
	}

	private void DoDeleteThisBDGT(string srckind)
	{
		string sQuest = ((srckind == "BUD") ? "確定刪除此預算書 ?" : "確定刪除此投標單 ?");
		int iSels = SelectedItems();
		if (iSels <= 0)
		{
			MessageBox.Show(this, "請先選定要刪除的專案!!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else
		{
			if (MessageBox.Show(this, sQuest, "刪除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
			{
				return;
			}
			int iCannotDeleteItems = 0;
			for (int i = 0; i < c1FlexGrid1.Rows.Count; i++)
			{
				if (c1FlexGrid1.Rows[i].Selected)
				{
					ArrayList tmp_AL = new ArrayList();
					tmp_AL.Add(F_UserID);
					tmp_AL.Add("刪除預算書或標單");
					Archnowledge.Pcces.BUDClass.Project prjcom = new Archnowledge.Pcces.BUDClass.Project(tmp_AL);
					prjcom.ps_srckind = srckind;
					prjcom.DeleProj(c1FlexGrid1[i, "ProjectCode"].ToString().Trim());
					prjcom.DeleProjTmp(c1FlexGrid1[i, "ProjectCode"].ToString().Trim());
					PubTools.WriteRoughlyLog(tmp_AL);
					DBClass DBCLS = new DBClass();
					DBCLS._FS_UserID = F_UserID;
					string sSQL2 = "Delete From " + prjcom.ps_srckind + "PageBreak Where ProjectCode='" + c1FlexGrid1[i, "ProjectCode"].ToString().Trim() + "'";
					DBCLS.ExecuteCommand(sSQL2);
					string sSQL3 = "Delete From Bookmarks Where SrcKind='" + prjcom.ps_srckind + "' And ProjectCode='" + c1FlexGrid1[i, "ProjectCode"].ToString().Trim() + "'";
					DBCLS.ExecuteCommand(sSQL3);
					if (!IsExistInpubProject(c1FlexGrid1[i, "ProjectCode"].ToString().Trim()))
					{
						string sSQL4 = "Delete from pubProject where projectCode = '" + c1FlexGrid1[i, "ProjectCode"].ToString().Trim() + "'";
						DBCLS.ExecuteCommand(sSQL4);
					}
					tmp_AL = null;
					DBCLS = null;
				}
				else if (c1FlexGrid1.Rows[i].Selected)
				{
					iCannotDeleteItems++;
				}
			}
			FormBudgetProjectPick_Load(null, null);
		}
	}

	private bool IsExistInpubProject(string sProjCode)
	{
		bool RetV = false;
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add(CommonMethods.GetFormTypeTitle(FormType.Budget));
		Archnowledge.Pcces.BUDClass.Project dbProject = new Archnowledge.Pcces.BUDClass.Project(aArr);
		dbProject.ps_srckind = "BUD";
		DT_Temp = dbProject.ListItem("", sProjCode);
		if (DT_Temp.Rows.Count > 0)
		{
			RetV = true;
		}
		dbProject = null;
		return RetV;
	}

	private int SelectedItems()
	{
		int RetV = 0;
		for (int i = 0; i < c1FlexGrid1.Rows.Count; i++)
		{
			if (c1FlexGrid1.Rows[i].Selected)
			{
				RetV++;
			}
		}
		return RetV;
	}

	private bool GetIsTemplate(string sProjectCode)
	{
		string iNum = "";
		bool rtnStr = false;
		string sSQL = "Select Istemplate from budProject where projectCode = '" + sProjectCode + "'";
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add("取pccescode的值");
		ModifyDB ModDB = new ModifyDB(sProjectCode, aArr);
		DataTable DT = new DataTable();
		DT = ModDB.DBList(sSQL);
		if (DT.Rows.Count > 0)
		{
			iNum = DT.Rows[0]["Istemplate"].ToString().Trim();
		}
		rtnStr = iNum == "Y";
		ModDB = null;
		aArr = null;
		return rtnStr;
	}
}
