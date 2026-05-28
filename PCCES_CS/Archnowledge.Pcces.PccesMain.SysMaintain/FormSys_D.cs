using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.PccesMain.MrsBase;
using Archnowledge.Pcces.STDClass;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinSchedule;
using Infragistics.Win.UltraWinSchedule.CalendarCombo;
using Infragistics.Win.UltraWinStatusBar;
using Infragistics.Win.UltraWinToolbars;

namespace Archnowledge.Pcces.PccesMain.SysMaintain;

public class FormSys_D : UserControl
{
	private UltraToolbarsManager ultraToolbarsManager1;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Top;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Bottom;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Left;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Right;

	private Panel panel1;

	private UltraLabel ultraLabel4;

	private UltraLabel ultraLabel3;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	private UltraLabel ultraLabel5;

	private Panel panel2;

	public GridMrsBase GridUnit1;

	private IContainer components;

	private UltraCalendarCombo txtDate;

	private UltraTextEditor txtPrice;

	private UltraButton Btn2;

	private UltraButton Btn1;

	private UltraButton BtnAddnew;

	private UltraTextEditor txtInvoice;

	private UltraTextEditor txtPccesCode;

	private UltraStatusBar ultraStatusBar1;

	private SaveFileDialog saveFileDialog1;

	private UltraTextEditor txtPccesCName;

	private UltraTextEditor txtTitle;

	private bool EnableCOMS = SysConfig.SysComsEnable;

	private string F_KeyWord = "";

	private string F_PubCode = "";

	private string F_ProjectCode = "";

	private DataTable DT1 = new DataTable();

	private DataTable DT_Sublet = new DataTable();

	private Label lblunitName;

	private string F_UserID;

	private string F_DBName;

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

	public string _PubCode
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

	public string _PccesCode
	{
		set
		{
			txtPccesCode.Text = value;
		}
	}

	public string _PccesCName
	{
		set
		{
			txtPccesCName.Text = value;
		}
	}

	public FormSys_D()
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
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Tool1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelete");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool1 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnu_lblFind");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool1 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_Go");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExport");
		Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelete");
		Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool2 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnu_lblFind");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool2 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo1");
		Infragistics.Win.ValueList valueList1 = new Infragistics.Win.ValueList(0);
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool5 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_Go");
		Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool1 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Popup1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool6 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelete");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool7 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExport");
		Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.SysMaintain.FormSys_D));
		Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton1 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
		Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel3 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
		this.ultraToolbarsManager1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
		this.GridUnit1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this._FormSys_B_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.panel1 = new System.Windows.Forms.Panel();
		this.lblunitName = new System.Windows.Forms.Label();
		this.txtTitle = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtPccesCName = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtDate = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
		this.txtPrice = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.Btn2 = new Infragistics.Win.Misc.UltraButton();
		this.Btn1 = new Infragistics.Win.Misc.UltraButton();
		this.BtnAddnew = new Infragistics.Win.Misc.UltraButton();
		this.txtInvoice = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtPccesCode = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.ultraStatusBar1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.GridUnit1).BeginInit();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtTitle).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPccesCName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPrice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtInvoice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPccesCode).BeginInit();
		this.panel2.SuspendLayout();
		base.SuspendLayout();
		appearance1.FontData.Name = "Arial";
		appearance1.FontData.SizeInPoints = 9f;
		this.ultraToolbarsManager1.Appearance = appearance1;
		appearance2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance2.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.DockAreaAppearance = appearance2;
		this.ultraToolbarsManager1.DockWithinContainer = this;
		this.ultraToolbarsManager1.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraToolbarsManager1.LockToolbars = true;
		appearance15.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance15.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance15.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.MenuSettings.HotTrackAppearance = appearance15;
		appearance16.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance16.BackColor2 = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraToolbarsManager1.MenuSettings.IconAreaAppearance = appearance16;
		appearance17.BackColor = System.Drawing.Color.White;
		appearance17.BackColor2 = System.Drawing.Color.White;
		this.ultraToolbarsManager1.MenuSettings.ToolAppearance = appearance17;
		this.ultraToolbarsManager1.ShowFullMenusDelay = 500;
		this.ultraToolbarsManager1.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2003;
		ultraToolbar1.DockedColumn = 0;
		ultraToolbar1.DockedRow = 0;
		ultraToolbar1.Settings.AllowCustomize = Infragistics.Win.DefaultableBoolean.False;
		ultraToolbar1.Settings.AllowDockTop = Infragistics.Win.DefaultableBoolean.True;
		ultraToolbar1.Text = "Tool1";
		labelTool1.InstanceProps.IsFirstInGroup = true;
		buttonTool3.InstanceProps.IsFirstInGroup = true;
		ultraToolbar1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[5] { buttonTool1, labelTool1, comboBoxTool1, buttonTool2, buttonTool3 });
		this.ultraToolbarsManager1.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[1] { ultraToolbar1 });
		appearance18.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance18.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.ToolbarSettings.Appearance = appearance18;
		appearance19.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance19.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance19.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.ToolbarSettings.HotTrackAppearance = appearance19;
		appearance20.Image = resources.GetObject("appearance20.Image");
		buttonTool4.SharedProps.AppearancesSmall.Appearance = appearance20;
		buttonTool4.SharedProps.Caption = "刪除";
		buttonTool4.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		labelTool2.SharedProps.Caption = "尋找:";
		labelTool2.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		comboBoxTool2.DropDownStyle = Infragistics.Win.DropDownStyle.DropDown;
		comboBoxTool2.SharedProps.Caption = "輸入關鍵字";
		comboBoxTool2.SharedProps.Width = 200;
		comboBoxTool2.ValueList = valueList1;
		appearance21.Image = resources.GetObject("appearance21.Image");
		buttonTool5.SharedProps.AppearancesSmall.Appearance = appearance21;
		buttonTool5.SharedProps.Caption = "Go";
		popupMenuTool1.SharedProps.Caption = "右鍵功能表";
		popupMenuTool1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[1] { buttonTool6 });
		appearance22.Image = resources.GetObject("appearance22.Image");
		buttonTool7.SharedProps.AppearancesSmall.Appearance = appearance22;
		buttonTool7.SharedProps.Caption = "匯出...";
		buttonTool7.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		this.ultraToolbarsManager1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[6] { buttonTool4, labelTool2, comboBoxTool2, buttonTool5, popupMenuTool1, buttonTool7 });
		this.ultraToolbarsManager1.ToolKeyPress += new Infragistics.Win.UltraWinToolbars.ToolKeyPressEventHandler(ultraToolbarsManager1_ToolKeyPress);
		this.ultraToolbarsManager1.BeforeToolbarListDropdown += new Infragistics.Win.UltraWinToolbars.BeforeToolbarListDropdownEventHandler(ultraToolbarsManager1_BeforeToolbarListDropdown);
		this.ultraToolbarsManager1.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(ultraToolbarsManager1_ToolClick);
		this.ultraToolbarsManager1.AfterToolDeactivate += new Infragistics.Win.UltraWinToolbars.ToolEventHandler(ultraToolbarsManager1_AfterToolDeactivate);
		this.ultraToolbarsManager1.AfterToolActivate += new Infragistics.Win.UltraWinToolbars.ToolEventHandler(ultraToolbarsManager1_AfterToolActivate);
		this.GridUnit1._ExcelFileName = "";
		this.GridUnit1._ExcelSheeName = "";
		this.GridUnit1._IsOpenExcelAfterExport = false;
		this.GridUnit1.AllowEditing = false;
		this.GridUnit1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
		this.GridUnit1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.GridUnit1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
		this.GridUnit1.ColumnInfo = resources.GetString("GridUnit1.ColumnInfo");
		this.ultraToolbarsManager1.SetContextMenuUltra(this.GridUnit1, "Popup1");
		this.GridUnit1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GridUnit1.ExtendLastCol = true;
		this.GridUnit1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.GridUnit1.ForeColor = System.Drawing.Color.Black;
		this.GridUnit1.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.GridUnit1.IsProcessUndo = false;
		this.GridUnit1.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.GridUnit1.Location = new System.Drawing.Point(0, 0);
		this.GridUnit1.Name = "GridUnit1";
		this.GridUnit1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.GridUnit1.ShowCursor = true;
		this.GridUnit1.ShowToolTipOnNarrowColumn = true;
		this.GridUnit1.Size = new System.Drawing.Size(600, 341);
		this.GridUnit1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("GridUnit1.Styles"));
		this.GridUnit1.TabIndex = 8;
		this.GridUnit1.UndoMax = 10;
		this.GridUnit1.MouseDown += new System.Windows.Forms.MouseEventHandler(GridUnit1_MouseDown);
		this._FormSys_B_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
		this._FormSys_B_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
		this._FormSys_B_Toolbars_Dock_Area_Top.Name = "_FormSys_B_Toolbars_Dock_Area_Top";
		this._FormSys_B_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(600, 27);
		this._FormSys_B_Toolbars_Dock_Area_Top.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 512);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Name = "_FormSys_B_Toolbars_Dock_Area_Bottom";
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(600, 0);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
		this._FormSys_B_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 27);
		this._FormSys_B_Toolbars_Dock_Area_Left.Name = "_FormSys_B_Toolbars_Dock_Area_Left";
		this._FormSys_B_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 485);
		this._FormSys_B_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
		this._FormSys_B_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(600, 27);
		this._FormSys_B_Toolbars_Dock_Area_Right.Name = "_FormSys_B_Toolbars_Dock_Area_Right";
		this._FormSys_B_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 485);
		this._FormSys_B_Toolbars_Dock_Area_Right.ToolbarsManager = this.ultraToolbarsManager1;
		this.panel1.Controls.Add(this.lblunitName);
		this.panel1.Controls.Add(this.txtTitle);
		this.panel1.Controls.Add(this.txtPccesCName);
		this.panel1.Controls.Add(this.txtDate);
		this.panel1.Controls.Add(this.txtPrice);
		this.panel1.Controls.Add(this.ultraLabel2);
		this.panel1.Controls.Add(this.ultraLabel5);
		this.panel1.Controls.Add(this.Btn2);
		this.panel1.Controls.Add(this.Btn1);
		this.panel1.Controls.Add(this.BtnAddnew);
		this.panel1.Controls.Add(this.txtInvoice);
		this.panel1.Controls.Add(this.txtPccesCode);
		this.panel1.Controls.Add(this.ultraLabel4);
		this.panel1.Controls.Add(this.ultraLabel3);
		this.panel1.Controls.Add(this.ultraLabel1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.panel1.Location = new System.Drawing.Point(0, 27);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(600, 121);
		this.panel1.TabIndex = 8;
		this.lblunitName.Location = new System.Drawing.Point(268, 8);
		this.lblunitName.Name = "lblunitName";
		this.lblunitName.Size = new System.Drawing.Size(100, 23);
		this.lblunitName.TabIndex = 39;
		this.lblunitName.Text = "lblunitName";
		this.lblunitName.Visible = false;
		this.txtTitle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance23.BackColorDisabled = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance23.BackColorDisabled2 = System.Drawing.Color.FromArgb(224, 224, 224);
		this.txtTitle.Appearance = appearance23;
		this.txtTitle.AutoSize = true;
		this.txtTitle.Enabled = false;
		this.txtTitle.Location = new System.Drawing.Point(220, 60);
		this.txtTitle.Name = "txtTitle";
		this.txtTitle.Size = new System.Drawing.Size(108, 24);
		this.txtTitle.TabIndex = 38;
		this.txtPccesCName.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance24.BackColorDisabled = System.Drawing.Color.FromArgb(255, 224, 192);
		this.txtPccesCName.Appearance = appearance24;
		this.txtPccesCName.AutoSize = true;
		this.txtPccesCName.Enabled = false;
		this.txtPccesCName.Location = new System.Drawing.Point(220, 32);
		this.txtPccesCName.Name = "txtPccesCName";
		this.txtPccesCName.Size = new System.Drawing.Size(108, 24);
		this.txtPccesCName.TabIndex = 37;
		this.txtDate.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		dateButton1.Caption = "今天";
		this.txtDate.DateButtons.Add(dateButton1);
		this.txtDate.DayOfWeekCaptionStyle = Infragistics.Win.UltraWinSchedule.DayOfWeekCaptionStyle.ShortDescription;
		this.txtDate.Location = new System.Drawing.Point(472, 64);
		this.txtDate.Name = "txtDate";
		this.txtDate.NonAutoSizeHeight = 21;
		this.txtDate.Size = new System.Drawing.Size(112, 21);
		this.txtDate.TabIndex = 36;
		this.txtDate.TipStyle = Infragistics.Win.UltraWinSchedule.TipStyleDay.Holidays;
		this.txtDate.Value = resources.GetObject("txtDate.Value");
		this.txtPrice.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtPrice.AutoSize = true;
		this.txtPrice.Location = new System.Drawing.Point(472, 32);
		this.txtPrice.Name = "txtPrice";
		this.txtPrice.Size = new System.Drawing.Size(112, 24);
		this.txtPrice.TabIndex = 12;
		this.txtPrice.Validating += new System.ComponentModel.CancelEventHandler(txtPrice_Validating);
		this.ultraLabel2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance25.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance25.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel2.Appearance = appearance25;
		this.ultraLabel2.Location = new System.Drawing.Point(384, 33);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(84, 23);
		this.ultraLabel2.TabIndex = 11;
		this.ultraLabel2.Text = "單價:";
		this.ultraLabel5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance26.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance26.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel5.Appearance = appearance26;
		this.ultraLabel5.Location = new System.Drawing.Point(384, 63);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(84, 23);
		this.ultraLabel5.TabIndex = 10;
		this.ultraLabel5.Text = "報價日期:";
		this.Btn2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.Btn2.BackColor = System.Drawing.SystemColors.Control;
		this.Btn2.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.Btn2.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.Btn2.Location = new System.Drawing.Point(328, 59);
		this.Btn2.Name = "Btn2";
		this.Btn2.Size = new System.Drawing.Size(56, 24);
		this.Btn2.SupportThemes = false;
		this.Btn2.TabIndex = 9;
		this.Btn2.Text = "挑選...";
		this.Btn2.Click += new System.EventHandler(Btn2_Click);
		this.Btn1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.Btn1.BackColor = System.Drawing.SystemColors.Control;
		this.Btn1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.Btn1.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.Btn1.Location = new System.Drawing.Point(328, 31);
		this.Btn1.Name = "Btn1";
		this.Btn1.Size = new System.Drawing.Size(56, 24);
		this.Btn1.SupportThemes = false;
		this.Btn1.TabIndex = 8;
		this.Btn1.Text = "挑選...";
		this.Btn1.Click += new System.EventHandler(Btn1_Click);
		this.BtnAddnew.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance27.FontData.Name = "細明體";
		appearance27.FontData.SizeInPoints = 11f;
		appearance27.Image = resources.GetObject("appearance9.Image");
		appearance27.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnAddnew.Appearance = appearance27;
		this.BtnAddnew.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnAddnew.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.BtnAddnew.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnAddnew.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnAddnew.Location = new System.Drawing.Point(510, 89);
		this.BtnAddnew.Name = "BtnAddnew";
		this.BtnAddnew.ShowFocusRect = false;
		this.BtnAddnew.ShowOutline = false;
		this.BtnAddnew.Size = new System.Drawing.Size(75, 27);
		this.BtnAddnew.SupportThemes = false;
		this.BtnAddnew.TabIndex = 7;
		this.BtnAddnew.Text = "新增";
		this.BtnAddnew.Click += new System.EventHandler(BtnAddnew_Click);
		appearance28.BackColorDisabled = System.Drawing.Color.FromArgb(224, 224, 224);
		this.txtInvoice.Appearance = appearance28;
		this.txtInvoice.AutoSize = true;
		this.txtInvoice.Enabled = false;
		this.txtInvoice.Location = new System.Drawing.Point(91, 60);
		this.txtInvoice.Name = "txtInvoice";
		this.txtInvoice.Size = new System.Drawing.Size(128, 24);
		this.txtInvoice.TabIndex = 5;
		appearance29.BackColorDisabled = System.Drawing.Color.FromArgb(255, 224, 192);
		this.txtPccesCode.Appearance = appearance29;
		this.txtPccesCode.AutoSize = true;
		this.txtPccesCode.Enabled = false;
		this.txtPccesCode.Location = new System.Drawing.Point(91, 32);
		this.txtPccesCode.Name = "txtPccesCode";
		this.txtPccesCode.Size = new System.Drawing.Size(128, 24);
		this.txtPccesCode.TabIndex = 4;
		appearance30.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance30.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel4.Appearance = appearance30;
		this.ultraLabel4.Location = new System.Drawing.Point(6, 33);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(84, 23);
		this.ultraLabel4.TabIndex = 3;
		this.ultraLabel4.Text = "工項:";
		appearance31.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance31.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel3.Appearance = appearance31;
		this.ultraLabel3.Location = new System.Drawing.Point(6, 60);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(84, 23);
		this.ultraLabel3.TabIndex = 2;
		this.ultraLabel3.Text = "報價廠商:";
		appearance32.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel1.Appearance = appearance32;
		this.ultraLabel1.Location = new System.Drawing.Point(8, 8);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(120, 23);
		this.ultraLabel1.TabIndex = 0;
		this.ultraLabel1.Text = "新增工料行情";
		this.panel2.Controls.Add(this.GridUnit1);
		this.panel2.Controls.Add(this.ultraStatusBar1);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Location = new System.Drawing.Point(0, 148);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(600, 364);
		this.panel2.TabIndex = 9;
		appearance33.BackColor = System.Drawing.SystemColors.Control;
		appearance33.FontData.SizeInPoints = 11f;
		this.ultraStatusBar1.Appearance = appearance33;
		this.ultraStatusBar1.Location = new System.Drawing.Point(0, 341);
		this.ultraStatusBar1.Name = "ultraStatusBar1";
		ultraStatusPanel1.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel1.Text = "資料筆數:";
		ultraStatusPanel1.Width = 200;
		ultraStatusPanel2.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel2.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		appearance34.TextHAlign = Infragistics.Win.HAlign.Right;
		ultraStatusPanel3.Appearance = appearance34;
		ultraStatusPanel3.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel3.Text = "客服電話:(02)2708-8090";
		ultraStatusPanel3.Width = 200;
		this.ultraStatusBar1.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[3] { ultraStatusPanel1, ultraStatusPanel2, ultraStatusPanel3 });
		this.ultraStatusBar1.Size = new System.Drawing.Size(600, 23);
		this.ultraStatusBar1.SupportThemes = false;
		this.ultraStatusBar1.TabIndex = 9;
		this.ultraStatusBar1.Text = "ultraStatusBar1";
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Right);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Left);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Top);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Bottom);
		base.Name = "FormSys_D";
		base.Size = new System.Drawing.Size(600, 512);
		base.Load += new System.EventHandler(FormSys_D_Load);
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.GridUnit1).EndInit();
		this.panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtTitle).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPccesCName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPrice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtInvoice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPccesCode).EndInit();
		this.panel2.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	private void FormSys_D_Load(object sender, EventArgs e)
	{
		ReloadDara();
	}

	public void ReloadDara()
	{
		txtDate.Value = DateTime.Now;
		SysUser oSysUser = new SysUser();
		F_DBName = "目前資料庫：" + oSysUser.GetSysUserDatabaseName(F_UserID);
		LoadData();
		BindToGrid();
	}

	private void LoadData()
	{
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		if (EnableCOMS)
		{
			aArr.Add(F_UserID);
		}
		else
		{
			aArr.Add("PccAdmin");
		}
		aArr.Add("(OFFICE_COST) 公司工料行情");
		HisPrice hisCom = new HisPrice(aArr);
		if (EnableCOMS)
		{
			hisCom.ps_ComsEnable = true;
			hisCom.ps_DBName = F_DBName.Split('：')[1];
		}
		DT1 = hisCom.ListItem("");
		if (!DT1.Columns.Contains("SubletName"))
		{
			DT1.Columns.Add("SubletName");
		}
		Archnowledge.Pcces.BUDClass.Sublet SubletCom = new Archnowledge.Pcces.BUDClass.Sublet(aArr);
		SubletCom.IsArchCOMS = EnableCOMS;
		DT_Sublet = SubletCom.ListItem("");
	}

	private void BindToGrid()
	{
		GridUnit1.Rows.Count = DT1.Rows.Count + 1;
		ultraStatusBar1.Panels[0].Text = "資料筆數：" + DT1.Rows.Count;
		for (int i = 0; i < DT1.Rows.Count; i++)
		{
			string sSubletName = "";
			DataRow[] DR_SUBs = DT_Sublet.Select(" Invoice_No = '" + DT1.Rows[i]["Invoice"].ToString().Trim() + "' And Trim(Invoice_No) <> ''", "Invoice_No Asc");
			if (DR_SUBs.Length > 0)
			{
				sSubletName = DR_SUBs[0]["Title"].ToString().Trim();
			}
			GridUnit1[i + 1, "ProjectCode"] = DT1.Rows[i]["projectCode"].ToString().Trim();
			GridUnit1[i + 1, "SProj"] = DT1.Rows[i]["sProj"].ToString().Trim();
			GridUnit1[i + 1, "PubCode"] = DT1.Rows[i]["pubCode"].ToString().Trim();
			GridUnit1[i + 1, "PccesCode"] = DT1.Rows[i]["pccesCode"].ToString().Trim();
			GridUnit1[i + 1, "CName"] = DT1.Rows[i]["cName"].ToString().Trim();
			GridUnit1[i + 1, "UnitName"] = DT1.Rows[i]["unitName"].ToString().Trim();
			GridUnit1[i + 1, "Price"] = DT1.Rows[i]["price"].ToString().Trim();
			GridUnit1[i + 1, "AskDate"] = DT1.Rows[i]["askDate"].ToString().Trim();
			GridUnit1[i + 1, "Invoice"] = DT1.Rows[i]["Invoice"].ToString().Trim();
			GridUnit1[i + 1, "InvoiceName"] = "(" + DT1.Rows[i]["Invoice"].ToString().Trim() + ")" + sSubletName;
			DT1.Rows[i]["SubletName"] = "(" + DT1.Rows[i]["Invoice"].ToString().Trim() + ")" + sSubletName;
			GridUnit1[i + 1, "sNO"] = DT1.Rows[i]["sNO"].ToString().Trim();
		}
		GridUnit1.AutoSizeCols();
	}

	private void BtnAddnew_Click(object sender, EventArgs e)
	{
		if (!DBClass.ChkAuthority(F_UserID, "F00100030001"))
		{
			MessageBox.Show(this, DBClass.GetFuncName("F00100030001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add("PccAdmin");
		aArr.Add("(OFFICE_COST) 公司工料行情--新增");
		if (txtInvoice.Text.Trim() == "" || txtPccesCode.Text.Trim() == "")
		{
			if (txtInvoice.Text.Trim() == "")
			{
				MessageBox.Show(this, "報價廠商尚未挑選", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBox.Show(this, "工項未挑選", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return;
		}
		HisPrice hisCom = new HisPrice(aArr);
		hisCom.ps_projectCode = "";
		hisCom.ps_sProj = "";
		hisCom.ps_pubCode = F_PubCode;
		hisCom.ps_invoice = txtInvoice.Text.Trim();
		hisCom.ps_price = txtPrice.Text.Trim();
		hisCom.ps_askDate = $"{txtDate.Value:yyyyMMdd}";
		hisCom.ps_pccesCode = txtPccesCode.Text.Trim();
		hisCom.ps_cName = txtPccesCName.Text.Trim();
		hisCom.ps_unitName = lblunitName.Text.Trim();
		hisCom.ps_DBName = F_DBName;
		hisCom.InseItem();
		LoadData();
		BindToGrid();
	}

	private void ultraToolbarsManager1_ToolClick(object sender, ToolClickEventArgs e)
	{
		switch (e.Tool.Key)
		{
		case "mnuDelete":
			Do_Delete();
			break;
		case "mnu_Go":
			Do_ToolBarFind();
			break;
		case "mnuExport":
			Do_Export();
			break;
		}
	}

	private void Do_Export()
	{
		string sFilter = "Microsoft Excel 97/2000 files (*.xls)|*.xls";
		saveFileDialog1.Filter = sFilter;
		saveFileDialog1.RestoreDirectory = true;
		saveFileDialog1.FileName = "公司資料行情";
		if (saveFileDialog1.ShowDialog() == DialogResult.OK)
		{
			GridUnit1._ExcelFileName = saveFileDialog1.FileName;
			GridUnit1._ExcelSheeName = "公司資料行情";
			GridUnit1._IsOpenExcelAfterExport = true;
			GridUnit1.ExecuteExport(c1GridExportType.Excel);
		}
	}

	private void Do_Delete()
	{
		if (!DBClass.ChkAuthority(F_UserID, "F00100030002"))
		{
			MessageBox.Show(this, DBClass.GetFuncName("F00100030002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		string sQues = "是否確定要刪除 ?";
		if (MessageBox.Show(this, sQues, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			ArrayList aArr = new ArrayList();
			aArr.Clear();
			aArr.Add("PccAdmin");
			aArr.Add("公司工料行情--刪除");
			HisPrice hisCom = new HisPrice(aArr);
			for (int i = GridUnit1.Rows.Count - 1; i >= 1; i--)
			{
				if (GridUnit1.Rows[i].Selected)
				{
					hisCom.ps_projectCode = GridUnit1[i, "ProjectCode"].ToString().Trim();
					hisCom.ps_sProj = GridUnit1[i, "SProj"].ToString().Trim();
					hisCom.ps_pubCode = GridUnit1[i, "PubCode"].ToString().Trim();
					hisCom.ps_invoice = GridUnit1[i, "Invoice"].ToString().Trim();
					hisCom.ps_pccesCode = GridUnit1[i, "PccesCode"].ToString().Trim();
					hisCom.ps_sNO = GridUnit1[i, "sNO"].ToString().Trim();
					hisCom.DeleItem();
					PubTools.WriteRoughlyLog(aArr);
				}
			}
			LoadData();
			BindToGrid();
		}
		GridUnit1.RowSel = -1;
	}

	private void Do_ToolBarFind()
	{
		if (!DBClass.ChkAuthority(F_UserID, "F00100030003"))
		{
			MessageBox.Show(this, DBClass.GetFuncName("F00100030003") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else
		{
			if (GridUnit1.Rows.Count <= 1)
			{
				return;
			}
			int iStart = GridUnit1.Row + 1;
			string sSearchText = ((ComboBoxTool)ultraToolbarsManager1.Tools["mnu_Cbo1"]).Text.Trim();
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
				iStart = GridUnit1.Row + 1;
			}
			if (sSearchText.Trim() == "")
			{
				return;
			}
			for (int i = iStart; i < GridUnit1.Rows.Count; i++)
			{
				for (int j = 1; j < GridUnit1.Cols.Count; j++)
				{
					if (GridUnit1[i, j] == null || GridUnit1[i, j].ToString().IndexOf(sSearchText) <= -1)
					{
						continue;
					}
					GridUnit1.Row = i;
					GridUnit1.Select();
					GridUnit1.TopRow = i;
					int iFondCount = 0;
					int iListCount = ((ComboBoxTool)ultraToolbarsManager1.Tools["mnu_Cbo1"]).ValueList.ValueListItems.Count;
					for (int k = 0; k < iListCount; k++)
					{
						if (((ComboBoxTool)ultraToolbarsManager1.Tools["mnu_Cbo1"]).ValueList.ValueListItems[k].DisplayText.Trim() == sSearchText.Trim())
						{
							iFondCount++;
						}
					}
					if (iFondCount == 0)
					{
						((ComboBoxTool)ultraToolbarsManager1.Tools["mnu_Cbo1"]).ValueList.ValueListItems.Add(sSearchText, sSearchText);
					}
					return;
				}
			}
		}
	}

	private void Btn1_Click(object sender, EventArgs e)
	{
		FormMrsBaseBreakdown_Addnew FM_MRS_ADD = new FormMrsBaseBreakdown_Addnew();
		FM_MRS_ADD._CallFormName = base.Name;
		FM_MRS_ADD._UserID = F_UserID;
		if (FM_MRS_ADD.ShowDialog(this) == DialogResult.OK)
		{
			txtPccesCode.Text = (base.ParentForm as frmSysMaintain)._PccesCode_D;
			txtPccesCName.Text = (base.ParentForm as frmSysMaintain)._PccesName_D;
			lblunitName.Text = (base.ParentForm as frmSysMaintain)._PccesUnit_D;
			F_PubCode = (base.ParentForm as frmSysMaintain)._PubCode_D;
		}
		FM_MRS_ADD.Close();
		FM_MRS_ADD.Dispose();
		FM_MRS_ADD = null;
	}

	private void ultraToolbarsManager1_ToolKeyPress(object sender, ToolKeyPressEventArgs e)
	{
		if (e.KeyChar == '\r' && e.Tool.Key == "mnu_Cbo1")
		{
			Do_ToolBarFind();
		}
	}

	private void Btn2_Click(object sender, EventArgs e)
	{
		FormSys_D_Pick FM_SYS_D_PK = new FormSys_D_Pick();
		FM_SYS_D_PK._UserID = F_UserID;
		if (FM_SYS_D_PK.ShowDialog(this) == DialogResult.OK)
		{
			txtInvoice.Text = (base.ParentForm as frmSysMaintain)._Invoice_No;
			txtTitle.Text = (base.ParentForm as frmSysMaintain)._Title;
		}
		FM_SYS_D_PK.Close();
		FM_SYS_D_PK.Dispose();
		FM_SYS_D_PK = null;
	}

	private void ultraToolbarsManager1_AfterToolActivate(object sender, ToolEventArgs e)
	{
		if (e.Tool.Key == "mnu_Cbo1")
		{
			((ButtonTool)ultraToolbarsManager1.Tools["mnuDelete"]).SharedProps.Shortcut = Shortcut.None;
		}
		else
		{
			((ButtonTool)ultraToolbarsManager1.Tools["mnuDelete"]).SharedProps.Shortcut = Shortcut.Del;
		}
	}

	private void ultraToolbarsManager1_AfterToolDeactivate(object sender, ToolEventArgs e)
	{
		((ButtonTool)ultraToolbarsManager1.Tools["mnuDelete"]).SharedProps.Shortcut = Shortcut.Del;
	}

	private void GridUnit1_MouseDown(object sender, MouseEventArgs e)
	{
		int rowIndex = GridUnit1.MouseRow;
		int colIndex = GridUnit1.MouseCol;
		GridUnit1.Row = GridUnit1.MouseRow;
		if (GridUnit1.Row <= 0 || rowIndex <= 0 || colIndex <= 0)
		{
			ultraToolbarsManager1.Tools["mnuDelete"].SharedProps.Enabled = false;
		}
		else
		{
			ultraToolbarsManager1.Tools["mnuDelete"].SharedProps.Enabled = true;
		}
	}

	private void ultraToolbarsManager1_BeforeToolbarListDropdown(object sender, BeforeToolbarListDropdownEventArgs e)
	{
		e.Cancel = true;
	}

	private void txtPrice_Validating(object sender, CancelEventArgs e)
	{
		if (!CommonMethods.CheckValidString((sender as UltraTextEditor).Text))
		{
			e.Cancel = true;
		}
		double dPrice = 0.0;
		try
		{
			dPrice = Convert.ToDouble(txtPrice.Text);
		}
		catch (Exception ex)
		{
			if (!(txtPrice.Text.Trim() != ""))
			{
				return;
			}
			CommonMethods.LogFile("Pcces46", "M", "SysMaintain.FormSys_D.cs" + ex.Message);
			MessageBox.Show(this, "輸入的單價格式不正確，請重新輸入。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtPrice.Focus();
		}
		if (txtPrice.Text.Trim() != "" && Convert.ToDouble(txtPrice.Text) > 2147483647.0)
		{
			MessageBox.Show(this, "單價不可超過 2,147,483,647", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtPrice.Focus();
		}
	}
}
