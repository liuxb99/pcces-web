using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.DatabaseAccess;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.DomainModule.DatabaseUpgrade;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.DomainModule.MrsBase;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.PccesMain.Budget;
using Archnowledge.Pcces.PccesMain.Library;
using Archnowledge.Pcces.STDClass;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinStatusBar;
using Infragistics.Win.UltraWinToolbars;

namespace Archnowledge.Pcces.PccesMain.SysMaintain;

public class FormSys_G : UserControl
{
	private IContainer components = null;

	private UltraToolbarsManager ultraToolbarsManager1;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Top;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Bottom;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Left;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Right;

	private Panel panel1;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel4;

	private UltraLabel ultraLabel3;

	private UltraButton btnCreateDB;

	private Panel panel2;

	public GridMrsBase gridDatabases;

	private ImageList imageList2;

	private UltraTextEditor tbDBName;

	private UltraTextEditor tbDBOrganization;

	private UltraStatusBar ultraStatusBar1;

	private UltraButton btnPickOrganizationCode;

	private UltraLabel ultraLabel2;

	private UltraTextEditor tb_dbInv;

	private CheckBox cbImportCostStructure;

	private CheckBox cbCompanyDB;

	private UltraButton restoreBuild103;

	private UltraButton ultraButton1;

	private string PreviousKeyword;

	private string UserID;

	private bool F_IsAutoNumCustom = false;

	private int F_CurrentFocusRowIndex = 0;

	private string[] CostStructureSelectedTypes = null;

	private FormStatus FORM_STATUS = FormStatus.Iinitial;

	private FormSys_G_Info1 ProgressDialog = null;

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
		Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Tool1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelete");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuChangeDB");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuRestore103");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("CreateOrganizationDatabase");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool1 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("PopSetting");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool1 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnu_lblFind");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool1 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool5 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_Go");
		Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool6 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelete");
		Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool2 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnu_lblFind");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool2 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo1");
		Infragistics.Win.ValueList valueList1 = new Infragistics.Win.ValueList(0);
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool7 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_Go");
		Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool2 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Popup1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool8 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelete");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool9 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuChangeDB");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool10 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuRestore103");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool11 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSetAutoNum");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool12 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuResetVer");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool13 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuChangeDB");
		Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool3 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("PopSetting");
		Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool14 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSetAutoNum");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool15 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuRestore103");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool16 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSetAutoNum");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool17 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuRestore103");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool18 = new Infragistics.Win.UltraWinToolbars.ButtonTool("CreateOrganizationDatabase");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool19 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuResetVer");
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.SysMaintain.FormSys_G));
		Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance38 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel3 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance39 = new Infragistics.Win.Appearance();
		this.ultraToolbarsManager1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
		this.gridDatabases = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this._FormSys_B_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.panel1 = new System.Windows.Forms.Panel();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.restoreBuild103 = new Infragistics.Win.Misc.UltraButton();
		this.cbCompanyDB = new System.Windows.Forms.CheckBox();
		this.cbImportCostStructure = new System.Windows.Forms.CheckBox();
		this.tb_dbInv = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.btnPickOrganizationCode = new Infragistics.Win.Misc.UltraButton();
		this.tbDBName = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.tbDBOrganization = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.btnCreateDB = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.ultraStatusBar1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridDatabases).BeginInit();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.tb_dbInv).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbDBName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbDBOrganization).BeginInit();
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
		appearance17.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance17.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance17.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.MenuSettings.HotTrackAppearance = appearance17;
		appearance18.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance18.BackColor2 = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraToolbarsManager1.MenuSettings.IconAreaAppearance = appearance18;
		appearance19.BackColor = System.Drawing.Color.White;
		appearance19.BackColor2 = System.Drawing.Color.White;
		this.ultraToolbarsManager1.MenuSettings.ToolAppearance = appearance19;
		this.ultraToolbarsManager1.ShowFullMenusDelay = 500;
		this.ultraToolbarsManager1.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2003;
		ultraToolbar1.DockedColumn = 0;
		ultraToolbar1.DockedRow = 0;
		ultraToolbar1.Settings.AllowCustomize = Infragistics.Win.DefaultableBoolean.False;
		ultraToolbar1.Settings.AllowDockTop = Infragistics.Win.DefaultableBoolean.True;
		ultraToolbar1.Text = "Tool1";
		buttonTool2.InstanceProps.IsFirstInGroup = true;
		buttonTool3.InstanceProps.IsFirstInGroup = true;
		buttonTool4.InstanceProps.IsFirstInGroup = true;
		popupMenuTool1.InstanceProps.IsFirstInGroup = true;
		labelTool1.InstanceProps.IsFirstInGroup = true;
		ultraToolbar1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[8] { buttonTool1, buttonTool2, buttonTool3, buttonTool4, popupMenuTool1, labelTool1, comboBoxTool1, buttonTool5 });
		this.ultraToolbarsManager1.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[1] { ultraToolbar1 });
		appearance20.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance20.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.ToolbarSettings.Appearance = appearance20;
		appearance21.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance21.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance21.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.ToolbarSettings.HotTrackAppearance = appearance21;
		appearance22.Image = resources.GetObject("appearance22.Image");
		buttonTool6.SharedProps.AppearancesSmall.Appearance = appearance22;
		buttonTool6.SharedProps.Caption = "刪除";
		buttonTool6.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		labelTool2.SharedProps.Caption = "尋找：";
		labelTool2.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		comboBoxTool2.DropDownStyle = Infragistics.Win.DropDownStyle.DropDown;
		comboBoxTool2.SharedProps.Caption = "輸入關鍵字";
		comboBoxTool2.SharedProps.Width = 200;
		comboBoxTool2.ValueList = valueList1;
		appearance23.Image = resources.GetObject("appearance23.Image");
		buttonTool7.SharedProps.AppearancesSmall.Appearance = appearance23;
		buttonTool7.SharedProps.Caption = "Go";
		popupMenuTool2.SharedProps.Caption = "右鍵功能表";
		buttonTool9.InstanceProps.IsFirstInGroup = true;
		buttonTool10.InstanceProps.IsFirstInGroup = true;
		buttonTool11.InstanceProps.IsFirstInGroup = true;
		buttonTool12.InstanceProps.IsFirstInGroup = true;
		popupMenuTool2.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[5] { buttonTool8, buttonTool9, buttonTool10, buttonTool11, buttonTool12 });
		appearance24.Image = resources.GetObject("appearance24.Image");
		buttonTool13.SharedProps.AppearancesSmall.Appearance = appearance24;
		buttonTool13.SharedProps.Caption = "設為使用中資料庫";
		buttonTool13.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		appearance25.Image = resources.GetObject("appearance25.Image");
		popupMenuTool3.SharedProps.AppearancesSmall.Appearance = appearance25;
		popupMenuTool3.SharedProps.Caption = "設定";
		popupMenuTool3.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		popupMenuTool3.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[2] { buttonTool14, buttonTool15 });
		buttonTool16.SharedProps.Caption = "設定資料庫對應的自動編碼...";
		buttonTool17.SharedProps.Caption = "還原資料庫版本103";
		buttonTool17.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool18.SharedProps.Caption = "建立各機關資料庫...";
		buttonTool18.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool19.SharedProps.Caption = "版本號設為 4.3.1000.190";
		this.ultraToolbarsManager1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[11]
		{
			buttonTool6, labelTool2, comboBoxTool2, buttonTool7, popupMenuTool2, buttonTool13, popupMenuTool3, buttonTool16, buttonTool17, buttonTool18,
			buttonTool19
		});
		this.ultraToolbarsManager1.ToolKeyPress += new Infragistics.Win.UltraWinToolbars.ToolKeyPressEventHandler(ultraToolbarsManager1_ToolKeyPress);
		this.ultraToolbarsManager1.BeforeToolbarListDropdown += new Infragistics.Win.UltraWinToolbars.BeforeToolbarListDropdownEventHandler(ultraToolbarsManager1_BeforeToolbarListDropdown);
		this.ultraToolbarsManager1.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(ultraToolbarsManager1_ToolClick);
		this.ultraToolbarsManager1.AfterToolDeactivate += new Infragistics.Win.UltraWinToolbars.ToolEventHandler(ultraToolbarsManager1_AfterToolDeactivate);
		this.ultraToolbarsManager1.AfterToolActivate += new Infragistics.Win.UltraWinToolbars.ToolEventHandler(ultraToolbarsManager1_AfterToolActivate);
		this.gridDatabases._ExcelFileName = "";
		this.gridDatabases._ExcelSheeName = "";
		this.gridDatabases._IsOpenExcelAfterExport = false;
		this.gridDatabases.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
		this.gridDatabases.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridDatabases.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
		this.gridDatabases.ColumnInfo = resources.GetString("gridDatabases.ColumnInfo");
		this.ultraToolbarsManager1.SetContextMenuUltra(this.gridDatabases, "Popup1");
		this.gridDatabases.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridDatabases.ExtendLastCol = true;
		this.gridDatabases.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridDatabases.ForeColor = System.Drawing.Color.Black;
		this.gridDatabases.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.gridDatabases.IsProcessUndo = false;
		this.gridDatabases.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.gridDatabases.Location = new System.Drawing.Point(0, 0);
		this.gridDatabases.Name = "gridDatabases";
		this.gridDatabases.Rows.Count = 1;
		this.gridDatabases.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.gridDatabases.ShowCursor = true;
		this.gridDatabases.ShowToolTipOnNarrowColumn = true;
		this.gridDatabases.Size = new System.Drawing.Size(572, 313);
		this.gridDatabases.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridDatabases.Styles"));
		this.gridDatabases.TabIndex = 9;
		this.gridDatabases.Tree.Column = 1;
		this.gridDatabases.UndoMax = 10;
		this.gridDatabases.Click += new System.EventHandler(gridDatabases_Click);
		this.gridDatabases.AfterSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(gridDatabases_AfterSelChange);
		this.gridDatabases.MouseDown += new System.Windows.Forms.MouseEventHandler(gridDatabases_MouseDown);
		this.gridDatabases.DoubleClick += new System.EventHandler(gridDatabases_DoubleClick);
		this._FormSys_B_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
		this._FormSys_B_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
		this._FormSys_B_Toolbars_Dock_Area_Top.Name = "_FormSys_B_Toolbars_Dock_Area_Top";
		this._FormSys_B_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(572, 27);
		this._FormSys_B_Toolbars_Dock_Area_Top.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 458);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Name = "_FormSys_B_Toolbars_Dock_Area_Bottom";
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(572, 0);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
		this._FormSys_B_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 27);
		this._FormSys_B_Toolbars_Dock_Area_Left.Name = "_FormSys_B_Toolbars_Dock_Area_Left";
		this._FormSys_B_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 431);
		this._FormSys_B_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
		this._FormSys_B_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(572, 27);
		this._FormSys_B_Toolbars_Dock_Area_Right.Name = "_FormSys_B_Toolbars_Dock_Area_Right";
		this._FormSys_B_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 431);
		this._FormSys_B_Toolbars_Dock_Area_Right.ToolbarsManager = this.ultraToolbarsManager1;
		this.panel1.Controls.Add(this.ultraButton1);
		this.panel1.Controls.Add(this.restoreBuild103);
		this.panel1.Controls.Add(this.cbCompanyDB);
		this.panel1.Controls.Add(this.cbImportCostStructure);
		this.panel1.Controls.Add(this.tb_dbInv);
		this.panel1.Controls.Add(this.ultraLabel2);
		this.panel1.Controls.Add(this.btnPickOrganizationCode);
		this.panel1.Controls.Add(this.tbDBName);
		this.panel1.Controls.Add(this.tbDBOrganization);
		this.panel1.Controls.Add(this.btnCreateDB);
		this.panel1.Controls.Add(this.ultraLabel3);
		this.panel1.Controls.Add(this.ultraLabel4);
		this.panel1.Controls.Add(this.ultraLabel1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 27);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(572, 95);
		this.panel1.TabIndex = 8;
		this.ultraButton1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance26.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton1.Appearance = appearance26;
		this.ultraButton1.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton1.Location = new System.Drawing.Point(343, 3);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.ShowFocusRect = false;
		this.ultraButton1.ShowOutline = false;
		this.ultraButton1.Size = new System.Drawing.Size(227, 28);
		this.ultraButton1.SupportThemes = false;
		this.ultraButton1.TabIndex = 20;
		this.ultraButton1.Text = "103版資料庫(含更舊版本)轉入";
		this.ultraButton1.Click += new System.EventHandler(ultraButton1_Click);
		this.restoreBuild103.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance27.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.restoreBuild103.Appearance = appearance27;
		this.restoreBuild103.BackColor = System.Drawing.SystemColors.Control;
		this.restoreBuild103.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.restoreBuild103.Location = new System.Drawing.Point(537, 7);
		this.restoreBuild103.Name = "restoreBuild103";
		this.restoreBuild103.ShowFocusRect = false;
		this.restoreBuild103.ShowOutline = false;
		this.restoreBuild103.Size = new System.Drawing.Size(35, 24);
		this.restoreBuild103.SupportThemes = false;
		this.restoreBuild103.TabIndex = 19;
		this.restoreBuild103.Text = "103";
		this.restoreBuild103.Click += new System.EventHandler(restoreBuild103_Click);
		this.cbCompanyDB.AutoSize = true;
		this.cbCompanyDB.Location = new System.Drawing.Point(267, 7);
		this.cbCompanyDB.Name = "cbCompanyDB";
		this.cbCompanyDB.Size = new System.Drawing.Size(106, 19);
		this.cbCompanyDB.TabIndex = 18;
		this.cbCompanyDB.Text = "公司資料庫";
		this.cbCompanyDB.UseVisualStyleBackColor = true;
		this.cbImportCostStructure.AutoSize = true;
		this.cbImportCostStructure.Location = new System.Drawing.Point(139, 7);
		this.cbImportCostStructure.Name = "cbImportCostStructure";
		this.cbImportCostStructure.Size = new System.Drawing.Size(122, 19);
		this.cbImportCostStructure.TabIndex = 17;
		this.cbImportCostStructure.Text = "匯入成本架構";
		this.cbImportCostStructure.UseVisualStyleBackColor = true;
		this.cbImportCostStructure.CheckedChanged += new System.EventHandler(cbImportCostStructure_CheckedChanged);
		this.tb_dbInv.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance28.FontData.Name = "細明體";
		appearance28.FontData.SizeInPoints = 11f;
		this.tb_dbInv.Appearance = appearance28;
		this.tb_dbInv.AutoSize = true;
		this.tb_dbInv.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.tb_dbInv.Location = new System.Drawing.Point(139, 94);
		this.tb_dbInv.MaxLength = 128;
		this.tb_dbInv.Name = "tb_dbInv";
		this.tb_dbInv.Size = new System.Drawing.Size(304, 24);
		this.tb_dbInv.TabIndex = 15;
		this.tb_dbInv.Visible = false;
		appearance29.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance29.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel2.Appearance = appearance29;
		this.ultraLabel2.Location = new System.Drawing.Point(15, 94);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(120, 23);
		this.ultraLabel2.TabIndex = 16;
		this.ultraLabel2.Text = "對應廠商統編:";
		this.ultraLabel2.Visible = false;
		this.btnPickOrganizationCode.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance30.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnPickOrganizationCode.Appearance = appearance30;
		this.btnPickOrganizationCode.BackColor = System.Drawing.SystemColors.Control;
		this.btnPickOrganizationCode.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnPickOrganizationCode.Location = new System.Drawing.Point(451, 33);
		this.btnPickOrganizationCode.Name = "btnPickOrganizationCode";
		this.btnPickOrganizationCode.ShowFocusRect = false;
		this.btnPickOrganizationCode.ShowOutline = false;
		this.btnPickOrganizationCode.Size = new System.Drawing.Size(120, 28);
		this.btnPickOrganizationCode.SupportThemes = false;
		this.btnPickOrganizationCode.TabIndex = 15;
		this.btnPickOrganizationCode.Text = "挑選機關代碼";
		this.btnPickOrganizationCode.Click += new System.EventHandler(btnPickOrganizationCode_Click);
		this.tbDBName.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance31.FontData.Name = "細明體";
		appearance31.FontData.SizeInPoints = 11f;
		this.tbDBName.Appearance = appearance31;
		this.tbDBName.AutoSize = true;
		this.tbDBName.Location = new System.Drawing.Point(139, 64);
		this.tbDBName.MaxLength = 128;
		this.tbDBName.Name = "tbDBName";
		this.tbDBName.Size = new System.Drawing.Size(304, 24);
		this.tbDBName.TabIndex = 14;
		this.tbDBName.Validating += new System.ComponentModel.CancelEventHandler(tb_dbDesc_Validating);
		this.tbDBOrganization.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance32.FontData.Name = "細明體";
		appearance32.FontData.SizeInPoints = 11f;
		this.tbDBOrganization.Appearance = appearance32;
		this.tbDBOrganization.AutoSize = true;
		this.tbDBOrganization.Location = new System.Drawing.Point(139, 36);
		this.tbDBOrganization.MaxLength = 200;
		this.tbDBOrganization.Name = "tbDBOrganization";
		this.tbDBOrganization.Size = new System.Drawing.Size(304, 24);
		this.tbDBOrganization.TabIndex = 13;
		this.tbDBOrganization.Validating += new System.ComponentModel.CancelEventHandler(tb_dbDesc_Validating);
		this.btnCreateDB.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance33.Image = resources.GetObject("appearance13.Image");
		appearance33.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnCreateDB.Appearance = appearance33;
		this.btnCreateDB.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnCreateDB.Font = new System.Drawing.Font("細明體", 11f);
		this.btnCreateDB.ImageSize = new System.Drawing.Size(20, 20);
		this.btnCreateDB.ImageTransparentColor = System.Drawing.Color.White;
		this.btnCreateDB.Location = new System.Drawing.Point(451, 62);
		this.btnCreateDB.Name = "btnCreateDB";
		this.btnCreateDB.ShowFocusRect = false;
		this.btnCreateDB.Size = new System.Drawing.Size(120, 28);
		this.btnCreateDB.SupportThemes = false;
		this.btnCreateDB.TabIndex = 8;
		this.btnCreateDB.Text = "新增(&A)";
		this.btnCreateDB.Click += new System.EventHandler(btnCreateDB_Click);
		appearance34.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance34.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel3.Appearance = appearance34;
		this.ultraLabel3.Location = new System.Drawing.Point(23, 67);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(112, 23);
		this.ultraLabel3.TabIndex = 5;
		this.ultraLabel3.Text = "資料庫名稱:";
		appearance35.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance35.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel4.Appearance = appearance35;
		this.ultraLabel4.Location = new System.Drawing.Point(23, 39);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(112, 23);
		this.ultraLabel4.TabIndex = 4;
		this.ultraLabel4.Text = "資料所屬機關:";
		appearance36.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel1.Appearance = appearance36;
		this.ultraLabel1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel1.Location = new System.Drawing.Point(13, 7);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(120, 23);
		this.ultraLabel1.TabIndex = 1;
		this.ultraLabel1.Text = "新增資料庫";
		this.panel2.Controls.Add(this.gridDatabases);
		this.panel2.Controls.Add(this.ultraStatusBar1);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Location = new System.Drawing.Point(0, 122);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(572, 336);
		this.panel2.TabIndex = 9;
		appearance37.BackColor = System.Drawing.SystemColors.Control;
		appearance37.FontData.Name = "細明體";
		appearance37.FontData.SizeInPoints = 11f;
		this.ultraStatusBar1.Appearance = appearance37;
		this.ultraStatusBar1.Location = new System.Drawing.Point(0, 313);
		this.ultraStatusBar1.Name = "ultraStatusBar1";
		ultraStatusPanel1.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel1.Text = "資料筆數:";
		ultraStatusPanel1.Width = 180;
		appearance38.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance38.ForeColor = System.Drawing.Color.Blue;
		ultraStatusPanel2.Appearance = appearance38;
		ultraStatusPanel2.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel2.MarqueeInfo.IsActive = true;
		ultraStatusPanel2.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		ultraStatusPanel2.Style = Infragistics.Win.UltraWinStatusBar.PanelStyle.Marquee;
		ultraStatusPanel2.Width = 101;
		appearance39.TextHAlign = Infragistics.Win.HAlign.Right;
		ultraStatusPanel3.Appearance = appearance39;
		ultraStatusPanel3.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel3.Text = "客服電話:(02)2708-8090";
		ultraStatusPanel3.Width = 200;
		this.ultraStatusBar1.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[3] { ultraStatusPanel1, ultraStatusPanel2, ultraStatusPanel3 });
		this.ultraStatusBar1.Size = new System.Drawing.Size(572, 23);
		this.ultraStatusBar1.SupportThemes = false;
		this.ultraStatusBar1.TabIndex = 17;
		this.ultraStatusBar1.Text = "ultraStatusBar1";
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.Magenta;
		this.imageList2.Images.SetKeyName(0, "");
		this.imageList2.Images.SetKeyName(1, "");
		this.imageList2.Images.SetKeyName(2, "");
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Right);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Left);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Top);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Bottom);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.Name = "FormSys_G";
		base.Size = new System.Drawing.Size(572, 458);
		base.Load += new System.EventHandler(FormSys_G_Load);
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridDatabases).EndInit();
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.tb_dbInv).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbDBName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbDBOrganization).EndInit();
		this.panel2.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	public FormSys_G()
	{
		InitializeComponent();
		FORM_STATUS = FormStatus.Active;
	}

	private void btnCreateDB_Click(object sender, EventArgs e)
	{
		string ls_dbName = tbDBName.Text.Trim();
		string ls_dbDesc = tbDBOrganization.Text.Trim();
		string ls_dbInv = tb_dbInv.Text.Trim();
		string lit_Message = "";
		SysUser oSysUser = new SysUser();
		string CurrentDatabaseName = oSysUser.GetSysUserDatabaseName(UserID);
		if (ls_dbName.Length == 0 || ls_dbDesc.Length == 0)
		{
			lit_Message = "資料所屬機關、資料庫名稱不可空白！";
			MessageBox.Show(this, lit_Message, "訊息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else
		{
			ProgressDialog = new FormSys_G_Info1();
			ProgressDialog._InfoString = "新增資料庫中...請稍候！";
			ProgressDialog.Show();
			ProgressDialog.BringToFront();
			Application.DoEvents();
			Cursor = Cursors.WaitCursor;
			ArrayList tmp_AL1 = new ArrayList();
			tmp_AL1.Add(UserID);
			tmp_AL1.Add("(ChangDatabase) 新增資料庫資料");
			MyDataBase DataCom = new MyDataBase(tmp_AL1);
			int Progress = 1;
			ExecResult ER = DataCom.InseItem(ls_dbName, ls_dbDesc, ls_dbInv, ProgressEventHandler, ref Progress);
			if (ER.ReturnCode == 0)
			{
				for (int i = 0; i < 3; i++)
				{
					ER = CostStructureImport.Import(UserID, !cbImportCostStructure.Checked, CostStructureSelectedTypes, ProgressDialog, ProgressEventHandler, ref Progress);
					if (ER.ReturnCode == 0)
					{
						if (cbCompanyDB.Checked)
						{
							UserDefined userDefined = new UserDefined();
							userDefined.SetPccesCompanyDB(ls_dbName);
						}
						break;
					}
				}
			}
			Cursor = Cursors.Default;
			DataCom = null;
			ProgressDialog.Close();
			ProgressDialog = null;
			if (ER.ReturnCode != 0)
			{
				MessageBox.Show(this, "建立失敗，請再試一下，訊息：" + ER.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				oSysUser.SetSysUserDatabaseName(UserID, CurrentDatabaseName);
				if (ER.ReturnCode == 1)
				{
					return;
				}
				try
				{
					GeneralManager oManager = new GeneralManager();
					ER = oManager.DeleteDatabase(ls_dbName);
					LoadData();
				}
				catch
				{
				}
			}
			else
			{
				PubTools.WriteRoughlyLog(tmp_AL1);
				MessageBox.Show(this, "資料庫建立完成！", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				tbDBName.Text = "";
				tbDBOrganization.Text = "";
				tb_dbInv.Text = "";
				LoadData();
			}
		}
		ultraToolbarsManager1.Tools["mnuDelete"].SharedProps.Enabled = false;
	}

	private void FormSys_G_Load(object sender, EventArgs e)
	{
		if (!ArchConvert.Obj2Bool(ConfigurationManager.AppSettings["EnableCompanyDB"]))
		{
			cbCompanyDB.Visible = false;
		}
		cbImportCostStructure.Visible = PubTools.GetAppSet_Bool("UseCostStructure");
		SetHeaderEditSymbol();
		string sPID = PubTools.GetAppSet_String("PID");
		if (sPID.Trim() == "Z14AC1100")
		{
			panel1.Height = 0;
		}
		F_IsAutoNumCustom = PubTools.GetAppSet_Bool("AutoNumCustom");
		if (!F_IsAutoNumCustom)
		{
			ultraToolbarsManager1.Tools["PopSetting"].SharedProps.Visible = false;
			ultraToolbarsManager1.Tools["mnuSetAutoNum"].SharedProps.Visible = false;
		}
		ultraToolbarsManager1.Tools["mnuRestore103"].SharedProps.Visible = true;
		ultraToolbarsManager1.Tools["mnuRestore103"].SharedProps.Enabled = false;
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add(UserID);
		tmp_AL1.Add("(ChangDatabase) 讀取資料庫資料");
		PubTools.WriteRoughlyLog(tmp_AL1);
		ProgressDialog = new FormSys_G_Info1();
		ProgressDialog.Show();
		ProgressDialog.BringToFront();
		Application.DoEvents();
		Cursor = Cursors.WaitCursor;
		LoadData();
		Cursor = Cursors.Default;
		ProgressDialog.Close();
		ProgressDialog.Dispose();
		ProgressDialog = null;
		FORM_STATUS = FormStatus.Normal;
		ultraToolbarsManager1.Tools["mnuDelete"].SharedProps.Enabled = false;
		restoreBuild103.Visible = false;
		if (DateTime.Now >= Convert.ToDateTime("2014-01-13"))
		{
			ultraToolbarsManager1.Tools["mnuResetVer"].SharedProps.Visible = false;
		}
		if (isSQL2KExist())
		{
			ultraButton1.Visible = true;
		}
		else
		{
			ultraButton1.Visible = false;
		}
	}

	private bool isSQL2KExist()
	{
		string ProgrmPath = "";
		ProgrmPath = ((8 != IntPtr.Size && string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))) ? Environment.GetEnvironmentVariable("ProgramFiles") : Environment.GetEnvironmentVariable("ProgramFiles(x86)"));
		string oSQL = Path.Combine(ProgrmPath, "Microsoft SQL Server\\80\\Tools\\Binn\\osql.exe");
		if (File.Exists(oSQL))
		{
			return true;
		}
		return false;
	}

	private void SetHeaderEditSymbol()
	{
		for (int i = 1; i < gridDatabases.Cols.Count; i++)
		{
			if (gridDatabases.Cols[i].AllowEditing)
			{
				CellRange rg = gridDatabases.GetCellRange(0, i);
				rg.Style = gridDatabases.Styles["EditMode"];
				if (imageList2.Images.Count > 1)
				{
					rg.Image = imageList2.Images[2];
				}
			}
		}
	}

	private void LoadData()
	{
		GeneralManager oManager = new GeneralManager();
		DataSet dsSysPccesSlave;
		DataSet dsPubProject;
		ExecResult ER = oManager.GetSysPccesSlaveIncludeProjectList(UserID, IncludeOldVersion: true, out dsSysPccesSlave, out dsPubProject);
		if (ER.ReturnCode != 0)
		{
			MessageBox.Show(this, "資料庫有未知問題發生：" + ER.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		else
		{
			BindToGrid(dsSysPccesSlave.Tables[0], dsPubProject.Tables[0]);
		}
	}

	private void BindToGrid(DataTable dtSysPccesSlave, DataTable dtPubProject)
	{
		FORM_STATUS = FormStatus.Edit;
		CellStyle CSDatabaseName = gridDatabases.Styles.Add("MainColor");
		CSDatabaseName.ForeColor = Color.Blue;
		CSDatabaseName.Font = new Font(gridDatabases.Font, FontStyle.Bold);
		CellStyle CSError = gridDatabases.Styles.Add("ErrorColor");
		CSError.BackColor = Color.Tomato;
		CellStyle CSCompanyDB = gridDatabases.Styles.Add("CompanyDBColor");
		CSCompanyDB.BackColor = Color.OldLace;
		CellStyle CSOldVersion = gridDatabases.Styles.Add("OldVersionColor");
		CSOldVersion.BackColor = Color.LightGray;
		gridDatabases.Rows.Count = 1;
		ultraStatusBar1.Panels[0].Text = "資料筆數：" + dtSysPccesSlave.Rows.Count;
		gridDatabases.Redraw = false;
		UserDefined userDefined = new UserDefined();
		string companyDB = userDefined.GetPccesCompanyDB();
		DataView dvPubProject = new DataView(dtPubProject);
		foreach (DataRow theRow in dtSysPccesSlave.Rows)
		{
			Row GridRow = gridDatabases.Rows.Add();
			if (theRow["ChkUse"].ToString().Trim() == "1")
			{
				GridRow["Flag"] = true;
				CellRange rg = gridDatabases.GetCellRange(GridRow.Index, gridDatabases.Cols["IsActive"].SafeIndex);
				rg.Style = gridDatabases.Styles["img"];
				rg.Image = imageList2.Images[0];
				F_CurrentFocusRowIndex = GridRow.Index;
			}
			else
			{
				GridRow["Flag"] = false;
				CellRange rg = gridDatabases.GetCellRange(GridRow.Index, gridDatabases.Cols["IsActive"].SafeIndex);
				rg.Style = gridDatabases.Styles["img"];
				rg.Image = imageList2.Images[1];
			}
			GridRow["NewVersionDB"] = theRow["NewVersionDB"];
			if ((int)theRow["NewVersionDB"] == 0)
			{
				CellRange rg = gridDatabases.GetCellRange(GridRow.Index, gridDatabases.Cols["Version"].SafeIndex);
				rg.Style = CSOldVersion;
			}
			GridRow.IsNode = true;
			GridRow.Node.Level = 1;
			GridRow.Node.Collapsed = true;
			GridRow["Counts"] = theRow["Counts"].ToString().Trim();
			CellRange rgDB1 = gridDatabases.GetCellRange(GridRow.Index, gridDatabases.Cols["dbDesc"].SafeIndex);
			CellRange rgDB2 = gridDatabases.GetCellRange(GridRow.Index, gridDatabases.Cols["dbName"].SafeIndex);
			CellStyle style = (rgDB2.Style = CSDatabaseName);
			rgDB1.Style = style;
			string DatabaseName = theRow["dbcName"].ToString().Trim();
			string DatabaseDesc = (string)(GridRow["dbDesc"] = theRow["dbcDesc"].ToString().Trim());
			GridRow["dbName"] = DatabaseName;
			GridRow["Version"] = theRow["Version"].ToString().Trim();
			GridRow["CreateDate"] = theRow["CreateDate"];
			if (DatabaseName == companyDB)
			{
				GridRow["CompanyDB"] = true;
				CellRange rgCompanyDB = gridDatabases.GetCellRange(GridRow.Index, 1, GridRow.Index, gridDatabases.Cols.Count - 1);
				rgCompanyDB.Style = CSCompanyDB;
				CheckBox checkBox = cbCompanyDB;
				bool flag = (cbCompanyDB.Enabled = false);
				checkBox.Checked = flag;
			}
			if (DatabaseDesc.IndexOf("ERROR") > -1)
			{
				CellRange rgError = gridDatabases.GetCellRange(GridRow.Index, 1, GridRow.Index, gridDatabases.Cols.Count - 1);
				rgError.Style = CSError;
			}
			dvPubProject.RowFilter = "Database ='" + DatabaseName + "'";
			for (int i = 0; i < dvPubProject.Count; i++)
			{
				GridRow = gridDatabases.Rows.Add();
				GridRow["Flag"] = false;
				GridRow.IsNode = true;
				GridRow.Node.Level = 2;
				GridRow["ProjectCode"] = dvPubProject[i]["ProjectCode"].ToString().Trim();
				GridRow["ProjCName"] = dvPubProject[i]["projCName"].ToString().Trim();
				GridRow["BudFileName"] = dvPubProject[i]["BudFileName"].ToString().Trim();
				GridRow["BidFileName"] = dvPubProject[i]["BidFileName"].ToString().Trim();
			}
		}
		foreach (Row GridRow in (IEnumerable)gridDatabases.Rows)
		{
			if (GridRow.Node != null && GridRow.Node.Level == 1)
			{
				GridRow.Node.Collapsed = true;
			}
		}
		gridDatabases.Redraw = true;
		FORM_STATUS = FormStatus.Normal;
	}

	private void ultraToolbarsManager1_ToolClick(object sender, ToolClickEventArgs e)
	{
		switch (e.Tool.Key)
		{
		case "mnuDelete":
			DeleteDatabase();
			break;
		case "mnu_Go":
			SearchByKeyword();
			break;
		case "mnuChangeDB":
			SwitchDatabase();
			break;
		case "mnuSetAutoNum":
			SetCorrespondingOrganization();
			break;
		case "mnuRestore103":
			Restore103();
			LoadData();
			break;
		case "CreateOrganizationDatabase":
			ShowCreateOrganizationDatabaseDialog();
			break;
		case "mnuResetVer":
			ResetVerionTo190();
			break;
		}
	}

	private void SetCorrespondingOrganization()
	{
		FormSys_G1 FM_G1 = new FormSys_G1();
		FM_G1._UserID = UserID;
		FM_G1._DataBaseDesc = gridDatabases[gridDatabases.Row, "dbDesc"].ToString().Trim();
		FM_G1._DataBaseName = gridDatabases[gridDatabases.Row, "dbName"].ToString().Trim();
		FM_G1.ShowDialog(this);
		FM_G1.Dispose();
		FM_G1 = null;
	}

	private void SearchByKeyword()
	{
		int startRowIndex = gridDatabases.Row + 1;
		ComboBoxTool ddlKeyword = (ComboBoxTool)ultraToolbarsManager1.Tools["mnu_Cbo1"];
		string Keyword = ddlKeyword.Text.Trim();
		if (gridDatabases.Rows.Count <= 1 || Keyword == string.Empty || !CommonMethods.CheckValidString(Keyword))
		{
			return;
		}
		if (PreviousKeyword != Keyword)
		{
			startRowIndex = 1;
			PreviousKeyword = Keyword;
		}
		else
		{
			startRowIndex = gridDatabases.Row + 1;
		}
		for (int row = startRowIndex; row < gridDatabases.Rows.Count; row++)
		{
			for (int column = 1; column < gridDatabases.Cols.Count; column++)
			{
				if (gridDatabases[row, column] == null || !gridDatabases[row, column].ToString().Contains(Keyword))
				{
					continue;
				}
				gridDatabases.Row = row;
				gridDatabases.Select();
				if (gridDatabases.Rows[row].Node.Level > 1)
				{
					gridDatabases.Rows[row].Node.EnsureVisible();
				}
				foreach (ValueListItem item in ddlKeyword.ValueList.ValueListItems)
				{
					if (item.DataValue.ToString() == Keyword)
					{
						return;
					}
				}
				ddlKeyword.ValueList.ValueListItems.Add(Keyword, Keyword);
				return;
			}
		}
	}

	private void SwitchDatabase()
	{
		if (gridDatabases.Row <= 0)
		{
			return;
		}
		if (gridDatabases[gridDatabases.Row, "dbDesc"] != null && gridDatabases[gridDatabases.Row, "dbDesc"].ToString().IndexOf("ERROR") > -1)
		{
			MessageBox.Show(this, "欲切換的資料庫有未知問題發生，無法切換，\n請洽網管人員或資訊人員。", "切換", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		string ls_dbDesc = "";
		string ls_dbName = "";
		Row GridRow = gridDatabases.Rows[gridDatabases.Row];
		int iLevel = GridRow.Node.Level;
		if (iLevel > 1)
		{
			Node ndSrc = gridDatabases.Rows[gridDatabases.Row].Node;
			int iParentRow = ndSrc.GetNode(NodeTypeEnum.Parent).Row.Index;
			GridRow = gridDatabases.Rows[iParentRow];
			ls_dbDesc = gridDatabases[iParentRow, "dbDesc"].ToString().Trim();
			ls_dbName = gridDatabases[iParentRow, "dbName"].ToString().Trim();
		}
		else
		{
			ls_dbDesc = gridDatabases[gridDatabases.Row, "dbDesc"].ToString().Trim();
			ls_dbName = gridDatabases[gridDatabases.Row, "dbName"].ToString().Trim();
		}
		SysUser oSysUser = new SysUser();
		string CurrentDatabaseName = oSysUser.GetSysUserDatabaseName(UserID);
		if (!(ls_dbName != CurrentDatabaseName))
		{
			return;
		}
		bool UpgradeOldVersion = false;
		bool NewVersionDB = true;
		bool.TryParse(GridRow["NewVersionDB"].ToString(), out NewVersionDB);
		ExecResult ER = new ExecResult();
		if (!NewVersionDB)
		{
			SysPccesSlave oPccesSlave = new SysPccesSlave();
			string PccesSlaveMirror = oPccesSlave.GetPccesSlaveMirror(ls_dbName);
			if (PccesSlaveMirror != "")
			{
				MessageBox.Show("[" + ls_dbDesc + "(" + ls_dbName + ")] 為舊版本的資料庫，但是已經執行過資料庫更新了。\n\n新的資料庫為 " + PccesSlaveMirror + "，請再切換一次資料庫。", "訊息");
				return;
			}
			if (MessageBox.Show("請注意：\n\n[" + ls_dbDesc + "(" + ls_dbName + ")] 為舊版本的資料庫，需要更新資料庫的版本，才可以正確執行。\n\n當系統執行資料庫更新時，會先對資料庫進行複製，再更新複製後的資料庫的內容，系統並不會更動 [" + ls_dbDesc + "(" + ls_dbName + ")]的內容。\n\n是否要繼續執行更新資料庫版本？", "切換", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.Cancel)
			{
				return;
			}
			UpgradeOldVersion = true;
		}
		ProgressDialog = new FormSys_G_Info1();
		ProgressDialog._InfoString = "資料庫切換中...請稍候!";
		ProgressDialog.Show();
		ProgressDialog.BringToFront();
		int Progress = 0;
		Cursor = Cursors.WaitCursor;
		if (UpgradeOldVersion)
		{
			string ConnectionString = ConfigurationManager.ConnectionStrings["Pcces"].ConnectionString;
			PccesBaseHelper baseHelper = new PccesBaseHelper(ConnectionString);
			string BackupPath = baseHelper.GetDatabasePath() + "\\";
			ER = DatabaseBackupRestore.BackupDatabase(baseHelper, BackupPath, ls_dbName, out var BackupFile, ProgressEventHandler, ref Progress);
			if (ER.ReturnCode == 0)
			{
				string NewDatabasename = "";
				BackupFile = BackupPath + BackupFile;
				ER = DatabaseBackupRestore.RestoreDatabase(baseHelper, BackupPath, BackupFile, ls_dbName, out NewDatabasename, ProgressEventHandler, ref Progress);
				if (ER.ReturnCode == 0)
				{
					SysPccesSlave oPccesSlave = new SysPccesSlave();
					oPccesSlave.SetPccesSlaveMirror(ls_dbName, NewDatabasename);
					ls_dbName = NewDatabasename;
					ls_dbDesc = GridRow["dbDesc"].ToString();
					oPccesSlave.AddSysPccesSlave(NewDatabasename, ls_dbDesc);
				}
			}
			if (File.Exists(BackupPath + BackupFile))
			{
				File.Delete(BackupPath + BackupFile);
			}
		}
		if (ER.ReturnCode == 0)
		{
			ArrayList tmp_AL1 = new ArrayList();
			tmp_AL1.Add(UserID);
			tmp_AL1.Add("(ChangDatabase) 切換資料庫資料");
			MyDataBase DataCom = new MyDataBase(tmp_AL1);
			try
			{
				ER = DataCom.SetUseDataBase(ls_dbName, ProgressEventHandler, ref Progress);
				if (ER.ReturnCode == 0)
				{
					SysConfig.ReInitComplete();
					string PccesConnectionString = ConfigurationManager.ConnectionStrings["Pcces"].ConnectionString;
					ConnectionStringUtility connUtility = new ConnectionStringUtility(PccesConnectionString);
					string CurrentConnectionString = connUtility.GetSqlConnectionString(ls_dbName);
					ConnectionManager.AddConnectionItemList("Pcces", "System.Data.SqlClient", CurrentConnectionString);
					if (!UpgradeOldVersion)
					{
						UserDefined oUserDefined = new UserDefined();
						string Version = oUserDefined.GetDBVersion(ls_dbName);
						int OldRowIndex = F_CurrentFocusRowIndex;
						F_CurrentFocusRowIndex = GridRow.Index;
						GridRow["Version"] = Version;
						CellRange rg = gridDatabases.GetCellRange(GridRow.Index, gridDatabases.Cols["IsActive"].SafeIndex);
						rg.Style = gridDatabases.Styles["img"];
						rg.Image = imageList2.Images[0];
						GridRow["Flag"] = true;
						GridRow = gridDatabases.Rows[OldRowIndex];
						rg = gridDatabases.GetCellRange(GridRow.Index, gridDatabases.Cols["IsActive"].SafeIndex);
						rg.Style = gridDatabases.Styles["img"];
						rg.Image = imageList2.Images[1];
						GridRow["Flag"] = false;
						if (GridRow["CompanyDB"] != null)
						{
							CellRange rgCompanyDB = gridDatabases.GetCellRange(GridRow.Index, 1, GridRow.Index, gridDatabases.Cols.Count - 1);
							rgCompanyDB.Style = gridDatabases.Styles["CompanyDBColor"];
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Do_ChangeDB Error : " + ex.Message);
			}
			finally
			{
				Cursor = Cursors.Default;
			}
			DataCom = null;
		}
		if (ProgressDialog != null)
		{
			ProgressDialog.Close();
			ProgressDialog = null;
		}
		if (ER.ReturnCode != 0)
		{
			MessageBox.Show(this, "切換資料庫，出現錯誤：" + ER.Message, "切換", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		if (UpgradeOldVersion)
		{
			LoadData();
		}
	}

	private void DeleteDatabase()
	{
		Row GridRow = gridDatabases.Rows[gridDatabases.Row];
		if (GridRow == null || GridRow["dbName"] == null || GridRow["dbDesc"] == null)
		{
			return;
		}
		string ls_dbName = GridRow["dbName"].ToString().Trim();
		string ls_dbDesc = GridRow["dbDesc"].ToString().Trim();
		bool DoDelete = true;
		bool NewVersionDB = false;
		bool.TryParse(GridRow["NewVersionDB"].ToString(), out NewVersionDB);
		if (!NewVersionDB && MessageBox.Show("【" + ls_dbName + "】" + ls_dbDesc + "\n\n 為舊版本的資料庫，若你刪除此資料庫，會導致舊版本的 PCCES 無法再讀取此資料庫。\n\n是否確定不再使用此資料庫，要刪除此資料庫？", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
		{
			DoDelete = false;
		}
		if (!DoDelete || MessageBox.Show("確定要刪除\n\n【" + ls_dbName + "】" + ls_dbDesc + "\n\n 資料庫嗎?", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
		{
			return;
		}
		ProgressDialog = new FormSys_G_Info1();
		ProgressDialog._InfoString = "資料庫刪除中...請稍候！";
		ProgressDialog.Show();
		ProgressDialog.BringToFront();
		Application.DoEvents();
		Cursor = Cursors.WaitCursor;
		ExecResult ER = new ExecResult();
		try
		{
			GeneralManager oManager = new GeneralManager();
			ER = oManager.DeleteDatabase(ls_dbName);
			if (ER.ReturnCode == 0 && GridRow["CompanyDB"] != null)
			{
				UserDefined userdefined = new UserDefined();
				userdefined.SetPccesCompanyDB("");
				cbCompanyDB.Enabled = true;
			}
			LoadData();
		}
		catch (Exception ex)
		{
			ER.ReturnCode = 1;
			ER.Message = ex.Message;
		}
		ProgressDialog.Close();
		ProgressDialog = null;
		Cursor = Cursors.Default;
		if (ER.ReturnCode == 0)
		{
			MessageBox.Show(this, "資料庫刪除成功！", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		else
		{
			MessageBox.Show(this, "資料庫刪除失敗：" + ER.Message, "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void ResetVerionTo190()
	{
		if (gridDatabases.Row < 1)
		{
			return;
		}
		string targetDBName = gridDatabases[gridDatabases.Row, "dbName"].ToString();
		string usedDBName = "";
		for (int i = 1; i < gridDatabases.Rows.Count; i++)
		{
			if (gridDatabases[i, "Flag"] != null && (bool)gridDatabases[i, "Flag"])
			{
				usedDBName = gridDatabases[i, "dbName"].ToString();
			}
		}
		UserDefined userDefined = new UserDefined();
		if (userDefined.SetDBVerTo190(targetDBName, usedDBName))
		{
			LoadData();
		}
	}

	private void Restore103()
	{
		_ = gridDatabases.Row;
		bool flag = 0 == 0;
		Row GridRow = gridDatabases.Rows[gridDatabases.Row];
		if (GridRow == null || GridRow["dbName"] == null)
		{
			return;
		}
		string ls_dbName = GridRow["dbName"].ToString().Trim();
		string s = "還原資料庫版本為build 103的意思是，若您有發生需要往前退版本的狀況，則可以就您的資料庫﹝" + GridRow["dbName"].ToString() + "﹞做還原資料庫版到103的動作。\r\n\r\n做完之後，請依照以下程序進行：\r\n1.立刻結束Pcces軟體，切勿點選任何其他功能。\r\n2.請移除目前軟體版本。\r\n3.請重新安裝4.3build103版軟體。\r\n4.若需要，再升級到您希望使用的更新版本。如此便完成整個退版本的程序。\r\n\r\n請問是否確定要進行？\r\n";
		if (MessageBox.Show(s, "還原資料庫版本build103", MessageBoxButtons.YesNo) != DialogResult.No)
		{
			Application.DoEvents();
			ExecResult ER = new ExecResult();
			try
			{
				GeneralManager oManager = new GeneralManager();
				ER = oManager.Restore103(ls_dbName);
			}
			catch (Exception ex)
			{
				ER.ReturnCode = 1;
				ER.Message = ex.Message;
			}
			if (ER.ReturnCode == 0)
			{
				MessageBox.Show(this, "資料庫還原成功，請立即關閉Pcces！", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				MessageBox.Show(this, "資料庫還原失敗：" + ER.Message, "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private void ShowCreateOrganizationDatabaseDialog()
	{
		OrganizationPicker organizationPicker = new OrganizationPicker();
		if (organizationPicker.ShowDialog() == DialogResult.OK)
		{
			CreateOrganizationDatabases(organizationPicker.SelectedOrganizations);
		}
		organizationPicker.Dispose();
		organizationPicker = null;
	}

	private void CreateOrganizationDatabases(string[][] Organizations)
	{
		SysUser oSysUser = new SysUser();
		string CurrentDatabaseName = oSysUser.GetSysUserDatabaseName(UserID);
		ProgressDialog = new FormSys_G_Info1();
		ProgressDialog.Show();
		ProgressDialog.BringToFront();
		Application.DoEvents();
		Cursor = Cursors.WaitCursor;
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add(UserID);
		tmp_AL1.Add("(ChangDatabase) 新增資料庫資料");
		MyDataBase DataCom = new MyDataBase(tmp_AL1);
		int Progress = 1;
		string FileDirectory = AppDomain.CurrentDomain.BaseDirectory + "OrganizationDatabases";
		bool Canceled = false;
		ExecResult ER = new ExecResult();
		foreach (string[] Organization in Organizations)
		{
			string databaseName;
			string organizationCode = (databaseName = Organization[0]);
			string databaseDescription = Organization[1];
			string organizationDBVersion = Organization[2];
			databaseDescription = databaseDescription + "(" + organizationDBVersion + ")";
			ProgressDialog._InfoString = $"新增【{databaseDescription}】資料庫...請稍候！";
			ER = DataCom.InseItem(databaseName, databaseDescription, string.Empty, organizationCode, organizationDBVersion, ProgressEventHandler, ref Progress);
			while (ER.ReturnCode == 1)
			{
				DatabaseNamingDialog dbNamingDailog = new DatabaseNamingDialog();
				dbNamingDailog.InvalidDatabaseName = databaseName;
				if (dbNamingDailog.ShowDialog() == DialogResult.OK)
				{
					ER = DataCom.InseItem(dbNamingDailog.NewDatabaseName, databaseDescription, string.Empty, organizationCode, organizationDBVersion, ProgressEventHandler, ref Progress);
					continue;
				}
				Canceled = true;
				break;
			}
			if (Canceled)
			{
				continue;
			}
			if (ER.ReturnCode == 0)
			{
				ER = CostStructureImport.Import(UserID, OnlyStructure: true, CostStructureSelectedTypes, ProgressDialog, ProgressEventHandler, ref Progress);
			}
			if (ER.ReturnCode != 0)
			{
				MessageBox.Show(this, $"【{databaseDescription}】建立失敗，訊息：{ER.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				oSysUser.SetSysUserDatabaseName(UserID, CurrentDatabaseName);
				try
				{
					GeneralManager oManager = new GeneralManager();
					ER = oManager.DeleteDatabase(databaseName);
				}
				catch
				{
				}
			}
			else
			{
				ProgressDialog.SetValue("匯入工料手冊");
				string FilePath = $"{FileDirectory}\\{Organization[0]},{Organization[1]},{Organization[2]}.xml";
				ImportXML(FilePath);
				PubTools.WriteRoughlyLog(tmp_AL1);
			}
		}
		if (ER.ReturnCode == 0)
		{
			MessageBox.Show(this, "機關資料庫建立完成！", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		Cursor = Cursors.Default;
		DataCom = null;
		ProgressDialog.Close();
		ProgressDialog = null;
		LoadData();
	}

	private void ImportXML(string FilePath)
	{
		DataSet dsImportXML = new DataSet();
		try
		{
			dsImportXML.ReadXml(FilePath);
			if (dsImportXML.Tables["mrsBaseA"].Columns.IndexOf("IsSkipImportMrsBase") <= -1)
			{
				dsImportXML.Tables["mrsBaseA"].Columns.Add("IsSkipImportMrsBase", Type.GetType("System.String"));
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(this, "匯入工料手冊失敗，轉入來源的檔案格式不正確！\n" + ex.Message, "匯入", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		MrsBaseManager mrsBaseManager = new MrsBaseManager();
		ExecResult ER = mrsBaseManager.ImportMrsBaseDataSet(dsImportXML);
		if (ER.ReturnCode != 0)
		{
			MessageBox.Show(ER.Message);
		}
	}

	private void gridDatabases_AfterSelChange(object sender, RangeEventArgs e)
	{
		int rowIndex = gridDatabases.MouseRow;
		try
		{
			if (rowIndex < 1)
			{
				ultraToolbarsManager1.Tools["mnuSetAutoNum"].SharedProps.Enabled = false;
				ultraToolbarsManager1.Tools["mnuRestore103"].SharedProps.Enabled = false;
			}
			else if (FORM_STATUS == FormStatus.Normal)
			{
				ultraToolbarsManager1.Tools["mnuSetAutoNum"].SharedProps.Enabled = true;
				ultraToolbarsManager1.Tools["mnuRestore103"].SharedProps.Enabled = true;
				ultraToolbarsManager1.Tools["mnuChangeDB"].SharedProps.Enabled = true;
				ultraToolbarsManager1.Tools["mnuDelete"].SharedProps.Enabled = true;
				if ((bool)gridDatabases[gridDatabases.Row, "Flag"])
				{
					ultraToolbarsManager1.Tools["mnuChangeDB"].SharedProps.Enabled = false;
					ultraToolbarsManager1.Tools["mnuDelete"].SharedProps.Enabled = false;
				}
				if (gridDatabases[gridDatabases.Row, "dbName"] != null && (gridDatabases[gridDatabases.Row, "dbName"].ToString().ToUpper().Trim() == "STDPCCES" || gridDatabases[gridDatabases.Row, "dbName"].ToString().ToUpper().Trim() == "PCCES" || IsPccesMaster(gridDatabases[gridDatabases.Row, "dbName"].ToString())))
				{
					ultraToolbarsManager1.Tools["mnuDelete"].SharedProps.Enabled = false;
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(this, "GridUnit1_AfterSelChange Error :" + ex.Message, "訊息", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private bool IsPccesMaster(string DatabaseName)
	{
		UserDefined userDefined = new UserDefined();
		string PccesMaster = userDefined.GetPccesMaster();
		return DatabaseName == PccesMaster;
	}

	private void gridDatabases_MouseDown(object sender, MouseEventArgs e)
	{
		int rowIndex = gridDatabases.MouseRow;
		int colIndex = gridDatabases.MouseCol;
		try
		{
			gridDatabases.Row = rowIndex;
			if (gridDatabases.Row <= 0 || rowIndex <= 0 || colIndex <= 0)
			{
				ultraToolbarsManager1.Tools["mnuDelete"].SharedProps.Enabled = false;
				ultraToolbarsManager1.Tools["mnuChangeDB"].SharedProps.Enabled = false;
				return;
			}
			ultraToolbarsManager1.Tools["mnuDelete"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuChangeDB"].SharedProps.Enabled = true;
			gridDatabases.Select();
			if (FORM_STATUS == FormStatus.Normal)
			{
				ultraToolbarsManager1.Tools["mnuChangeDB"].SharedProps.Enabled = true;
				ultraToolbarsManager1.Tools["mnuDelete"].SharedProps.Enabled = true;
				if ((bool)gridDatabases[gridDatabases.Row, "Flag"])
				{
					ultraToolbarsManager1.Tools["mnuChangeDB"].SharedProps.Enabled = false;
					ultraToolbarsManager1.Tools["mnuDelete"].SharedProps.Enabled = false;
				}
				if (gridDatabases[gridDatabases.Row, "dbName"] != null && (gridDatabases[gridDatabases.Row, "dbName"].ToString().ToUpper().Trim() == "STDPCCES" || gridDatabases[gridDatabases.Row, "dbName"].ToString().ToUpper().Trim() == "PCCES" || IsPccesMaster(gridDatabases[gridDatabases.Row, "dbName"].ToString())))
				{
					ultraToolbarsManager1.Tools["mnuDelete"].SharedProps.Enabled = false;
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(this, "GridUnit1_MouseDown Error :" + ex.Message, "訊息", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void gridDatabases_Click(object sender, EventArgs e)
	{
		int MouseRow = gridDatabases.MouseRow;
		gridDatabases.Row = MouseRow;
	}

	private void ultraToolbarsManager1_BeforeToolbarListDropdown(object sender, BeforeToolbarListDropdownEventArgs e)
	{
		e.Cancel = true;
	}

	private void tb_dbDesc_Validating(object sender, CancelEventArgs e)
	{
		if (!CommonMethods.CheckValidString((sender as UltraTextEditor).Text))
		{
			e.Cancel = true;
		}
		if (!CommonMethods.IsStrByteLenValid(tbDBOrganization.Text, 200))
		{
			MessageBox.Show(this, "資料所屬機關的長度不可超過 200 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			tbDBOrganization.Focus();
			return;
		}
		if (!CommonMethods.IsStrByteLenValid(tbDBName.Text, 128))
		{
			MessageBox.Show(this, "資料庫名稱的長度不可超過 128 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			tbDBName.Focus();
			return;
		}
		if (tbDBName.Text.Length > 0)
		{
			int iASCII = tbDBName.Text[0];
			if (iASCII < 65 || iASCII > 122)
			{
				MessageBox.Show(this, "資料庫名稱，開頭第一個字必須是英文字母。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				tbDBName.Focus();
				return;
			}
		}
		if (tb_dbInv.Text.Trim() != "")
		{
			try
			{
				Convert.ToInt32(tb_dbInv.Text.Trim());
			}
			catch
			{
				MessageBox.Show(this, "廠商統編，必須是阿拉伯數字。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				tb_dbInv.Focus();
				return;
			}
		}
		for (int i = 0; i < tbDBName.Text.Length; i++)
		{
			if (!CommonMethods.EngNumValid(tbDBName.Text[i]))
			{
				MessageBox.Show(this, "資料庫名稱，不可輸入非數字或英文字母的字", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				tbDBName.Focus();
				break;
			}
		}
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

	private void ultraToolbarsManager1_ToolKeyPress(object sender, ToolKeyPressEventArgs e)
	{
		if (e.KeyChar == '\r' && e.Tool.Key == "mnu_Cbo1")
		{
			SearchByKeyword();
		}
	}

	private void gridDatabases_DoubleClick(object sender, EventArgs e)
	{
		SwitchDatabase();
	}

	private void btnPickOrganizationCode_Click(object sender, EventArgs e)
	{
		FormBudgetDept_Pick FM_BDGT_DEPT_PK = new FormBudgetDept_Pick();
		FM_BDGT_DEPT_PK._UserID = UserID;
		FM_BDGT_DEPT_PK._OwnerName = "FormSys_G";
		if (FM_BDGT_DEPT_PK.ShowDialog(this) == DialogResult.OK)
		{
			tbDBName.Text = "A" + (base.ParentForm as frmSysMaintain)._MainCode_G;
			tbDBOrganization.Text = (base.ParentForm as frmSysMaintain)._MainName_G;
		}
		FM_BDGT_DEPT_PK.Close();
		FM_BDGT_DEPT_PK.Dispose();
		FM_BDGT_DEPT_PK = null;
	}

	private void cbImportCostStructure_CheckedChanged(object sender, EventArgs e)
	{
		if (cbImportCostStructure.Checked)
		{
			CostStructureTypePicker costStructureTypePicker = new CostStructureTypePicker();
			if (costStructureTypePicker.ShowDialog() == DialogResult.OK)
			{
				CostStructureSelectedTypes = costStructureTypePicker.SelectedTypes;
			}
			costStructureTypePicker.Dispose();
			costStructureTypePicker = null;
		}
	}

	private void ProgressEventHandler(string Message, ref int Progress)
	{
		if (ProgressDialog != null)
		{
			ProgressDialog.SetValue(Message, Progress);
		}
	}

	private void restoreBuild103_Click(object sender, EventArgs e)
	{
	}

	private void ultraButton1_Click(object sender, EventArgs e)
	{
		try
		{
			string sFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "2Kto2K5.exe");
			Process.Start(sFile);
		}
		catch (Exception ex)
		{
			MessageBox.Show(this, ex.Message, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}
}
