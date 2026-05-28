using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.PccesMain.ArchControls;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinStatusBar;
using Infragistics.Win.UltraWinToolbars;

namespace Archnowledge.Pcces.PccesMain.SysMaintain;

public class FormSys_B : UserControl
{
	private IContainer components;

	private UltraToolbarsManager ultraToolbarsManager1;

	private Panel FormSys_B_Fill_Panel;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Left;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Right;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Top;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Bottom;

	private Panel panel1;

	private UltraLabel ultraLabel1;

	private UltraLabel lbMainNameE;

	private UltraLabel lbMainName;

	private UltraLabel lbMainCode;

	private UltraButton btnNew;

	private Panel panel2;

	public GridMrsBase gridMainUnit;

	private UltraTextEditor tbMainNameE;

	private UltraTextEditor tbMainName;

	private UltraTextEditor tbMainCode;

	private OpenFileDialog openImportXMLFile;

	private UltraStatusBar StatusBar;

	private ImageList imageList2;

	private UltraTextEditor tbMainZipCode;

	private UltraLabel lbZipCodeAndAddress;

	private UltraTextEditor tbMainTel;

	private UltraLabel lbMainTel;

	private UltraTextEditor tbMainAddress;

	private UltraButton btnImport;

	private int AuthorityMessageCount = 0;

	private string KeyWord;

	private string MainCode;

	private string MainName;

	private string MainNameE;

	private string UserID;

	private MainUnit mainUnit = new MainUnit();

	private DataSet dsMainUnit = new DataSet();

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

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Tool1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelete");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool1 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnu_lblFind");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool1 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_Go");
		Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelete");
		Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool2 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnu_lblFind");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool2 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo1");
		Infragistics.Win.ValueList valueList1 = new Infragistics.Win.ValueList(0);
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_Go");
		Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool1 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Popup1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool5 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelete");
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.SysMaintain.FormSys_B));
		Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel3 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
		this.ultraToolbarsManager1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
		this.gridMainUnit = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.FormSys_B_Fill_Panel = new System.Windows.Forms.Panel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.StatusBar = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.panel1 = new System.Windows.Forms.Panel();
		this.btnImport = new Infragistics.Win.Misc.UltraButton();
		this.tbMainAddress = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.tbMainTel = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.lbMainTel = new Infragistics.Win.Misc.UltraLabel();
		this.tbMainZipCode = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.lbZipCodeAndAddress = new Infragistics.Win.Misc.UltraLabel();
		this.btnNew = new Infragistics.Win.Misc.UltraButton();
		this.tbMainNameE = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.tbMainName = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.tbMainCode = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.lbMainCode = new Infragistics.Win.Misc.UltraLabel();
		this.lbMainName = new Infragistics.Win.Misc.UltraLabel();
		this.lbMainNameE = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this._FormSys_B_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.openImportXMLFile = new System.Windows.Forms.OpenFileDialog();
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridMainUnit).BeginInit();
		this.FormSys_B_Fill_Panel.SuspendLayout();
		this.panel2.SuspendLayout();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.tbMainAddress).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbMainTel).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbMainZipCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbMainNameE).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbMainName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbMainCode).BeginInit();
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
		appearance13.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance13.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance13.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.MenuSettings.HotTrackAppearance = appearance13;
		appearance14.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance14.BackColor2 = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraToolbarsManager1.MenuSettings.IconAreaAppearance = appearance14;
		appearance15.BackColor = System.Drawing.Color.White;
		appearance15.BackColor2 = System.Drawing.Color.White;
		this.ultraToolbarsManager1.MenuSettings.ToolAppearance = appearance15;
		this.ultraToolbarsManager1.ShowFullMenusDelay = 500;
		this.ultraToolbarsManager1.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2003;
		ultraToolbar1.DockedColumn = 0;
		ultraToolbar1.DockedRow = 0;
		ultraToolbar1.Settings.AllowCustomize = Infragistics.Win.DefaultableBoolean.False;
		ultraToolbar1.Settings.AllowDockTop = Infragistics.Win.DefaultableBoolean.True;
		ultraToolbar1.Text = "Tool1";
		labelTool1.InstanceProps.IsFirstInGroup = true;
		ultraToolbar1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[4] { buttonTool1, labelTool1, comboBoxTool1, buttonTool2 });
		this.ultraToolbarsManager1.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[1] { ultraToolbar1 });
		appearance16.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance16.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.ToolbarSettings.Appearance = appearance16;
		appearance17.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance17.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance17.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.ToolbarSettings.HotTrackAppearance = appearance17;
		appearance18.Image = resources.GetObject("appearance18.Image");
		buttonTool3.SharedProps.AppearancesSmall.Appearance = appearance18;
		buttonTool3.SharedProps.Caption = "刪除";
		buttonTool3.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		buttonTool3.SharedProps.Shortcut = System.Windows.Forms.Shortcut.Del;
		labelTool2.SharedProps.Caption = "尋找:";
		labelTool2.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		comboBoxTool2.DropDownStyle = Infragistics.Win.DropDownStyle.DropDown;
		comboBoxTool2.SharedProps.Caption = "輸入關鍵字";
		comboBoxTool2.SharedProps.Width = 200;
		comboBoxTool2.ValueList = valueList1;
		appearance19.Image = resources.GetObject("appearance19.Image");
		buttonTool4.SharedProps.AppearancesSmall.Appearance = appearance19;
		buttonTool4.SharedProps.Caption = "Go";
		popupMenuTool1.SharedProps.Caption = "右鍵功能表";
		popupMenuTool1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[1] { buttonTool5 });
		this.ultraToolbarsManager1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[5] { buttonTool3, labelTool2, comboBoxTool2, buttonTool4, popupMenuTool1 });
		this.ultraToolbarsManager1.ToolKeyPress += new Infragistics.Win.UltraWinToolbars.ToolKeyPressEventHandler(ultraToolbarsManager1_ToolKeyPress);
		this.ultraToolbarsManager1.BeforeToolbarListDropdown += new Infragistics.Win.UltraWinToolbars.BeforeToolbarListDropdownEventHandler(ultraToolbarsManager1_BeforeToolbarListDropdown);
		this.ultraToolbarsManager1.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(ultraToolbarsManager1_ToolClick);
		this.ultraToolbarsManager1.AfterToolDeactivate += new Infragistics.Win.UltraWinToolbars.ToolEventHandler(ultraToolbarsManager1_AfterToolDeactivate);
		this.ultraToolbarsManager1.AfterToolActivate += new Infragistics.Win.UltraWinToolbars.ToolEventHandler(ultraToolbarsManager1_AfterToolActivate);
		this.gridMainUnit._ExcelFileName = "";
		this.gridMainUnit._ExcelSheeName = "";
		this.gridMainUnit._IsOpenExcelAfterExport = false;
		this.gridMainUnit.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
		this.gridMainUnit.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
		this.gridMainUnit.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridMainUnit.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
		this.gridMainUnit.ColumnInfo = resources.GetString("gridMainUnit.ColumnInfo");
		this.ultraToolbarsManager1.SetContextMenuUltra(this.gridMainUnit, "Popup1");
		this.gridMainUnit.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridMainUnit.ExtendLastCol = true;
		this.gridMainUnit.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridMainUnit.ForeColor = System.Drawing.Color.Black;
		this.gridMainUnit.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.gridMainUnit.IsProcessUndo = false;
		this.gridMainUnit.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.gridMainUnit.Location = new System.Drawing.Point(0, 0);
		this.gridMainUnit.Name = "gridMainUnit";
		this.gridMainUnit.Rows.Count = 1;
		this.gridMainUnit.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.gridMainUnit.ShowCursor = true;
		this.gridMainUnit.ShowToolTipOnNarrowColumn = true;
		this.gridMainUnit.Size = new System.Drawing.Size(600, 174);
		this.gridMainUnit.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridMainUnit.Styles"));
		this.gridMainUnit.TabIndex = 7;
		this.gridMainUnit.UndoMax = 10;
		this.gridMainUnit.AfterSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(MainUnitGrid_AfterSelChange);
		this.gridMainUnit.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(MainUnitGrid_AfterEdit);
		this.gridMainUnit.MouseDown += new System.Windows.Forms.MouseEventHandler(MainUnitGrid_MouseDown);
		this.gridMainUnit.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(MainUnitGrid_BeforeEdit);
		this.FormSys_B_Fill_Panel.Controls.Add(this.panel2);
		this.FormSys_B_Fill_Panel.Controls.Add(this.panel1);
		this.FormSys_B_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
		this.FormSys_B_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.FormSys_B_Fill_Panel.Location = new System.Drawing.Point(0, 27);
		this.FormSys_B_Fill_Panel.Name = "FormSys_B_Fill_Panel";
		this.FormSys_B_Fill_Panel.Size = new System.Drawing.Size(600, 377);
		this.FormSys_B_Fill_Panel.TabIndex = 0;
		this.panel2.Controls.Add(this.gridMainUnit);
		this.panel2.Controls.Add(this.StatusBar);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Location = new System.Drawing.Point(0, 180);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(600, 197);
		this.panel2.TabIndex = 1;
		this.panel2.Resize += new System.EventHandler(panel2_Resize);
		appearance20.BackColor = System.Drawing.SystemColors.Control;
		appearance20.FontData.SizeInPoints = 11f;
		this.StatusBar.Appearance = appearance20;
		this.StatusBar.Location = new System.Drawing.Point(0, 174);
		this.StatusBar.Name = "StatusBar";
		ultraStatusPanel1.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel1.Text = "資料筆數:";
		ultraStatusPanel1.Width = 200;
		ultraStatusPanel2.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel2.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		appearance21.TextHAlign = Infragistics.Win.HAlign.Right;
		ultraStatusPanel3.Appearance = appearance21;
		ultraStatusPanel3.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel3.Text = "客服電話:(02)2708-8090";
		ultraStatusPanel3.Width = 200;
		this.StatusBar.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[3] { ultraStatusPanel1, ultraStatusPanel2, ultraStatusPanel3 });
		this.StatusBar.Size = new System.Drawing.Size(600, 23);
		this.StatusBar.SupportThemes = false;
		this.StatusBar.TabIndex = 11;
		this.StatusBar.Text = "ultraStatusBar1";
		this.panel1.Controls.Add(this.btnImport);
		this.panel1.Controls.Add(this.tbMainAddress);
		this.panel1.Controls.Add(this.tbMainTel);
		this.panel1.Controls.Add(this.lbMainTel);
		this.panel1.Controls.Add(this.tbMainZipCode);
		this.panel1.Controls.Add(this.lbZipCodeAndAddress);
		this.panel1.Controls.Add(this.btnNew);
		this.panel1.Controls.Add(this.tbMainNameE);
		this.panel1.Controls.Add(this.tbMainName);
		this.panel1.Controls.Add(this.tbMainCode);
		this.panel1.Controls.Add(this.lbMainCode);
		this.panel1.Controls.Add(this.lbMainName);
		this.panel1.Controls.Add(this.lbMainNameE);
		this.panel1.Controls.Add(this.ultraLabel1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(600, 180);
		this.panel1.TabIndex = 0;
		this.btnImport.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance22.Image = resources.GetObject("appearance5.Image");
		appearance22.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnImport.Appearance = appearance22;
		this.btnImport.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnImport.Font = new System.Drawing.Font("細明體", 11f);
		this.btnImport.ImageSize = new System.Drawing.Size(20, 20);
		this.btnImport.ImageTransparentColor = System.Drawing.Color.White;
		this.btnImport.Location = new System.Drawing.Point(511, 144);
		this.btnImport.Name = "btnImport";
		this.btnImport.ShowFocusRect = false;
		this.btnImport.ShowOutline = false;
		this.btnImport.Size = new System.Drawing.Size(75, 28);
		this.btnImport.SupportThemes = false;
		this.btnImport.TabIndex = 14;
		this.btnImport.Text = "匯入";
		this.btnImport.Click += new System.EventHandler(btnImport_Click);
		this.tbMainAddress.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.tbMainAddress.AutoSize = true;
		this.tbMainAddress.Location = new System.Drawing.Point(272, 116);
		this.tbMainAddress.Name = "tbMainAddress";
		this.tbMainAddress.Size = new System.Drawing.Size(314, 24);
		this.tbMainAddress.TabIndex = 11;
		this.tbMainTel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.tbMainTel.AutoSize = true;
		this.tbMainTel.Location = new System.Drawing.Point(204, 144);
		this.tbMainTel.Name = "tbMainTel";
		this.tbMainTel.Size = new System.Drawing.Size(200, 24);
		this.tbMainTel.TabIndex = 12;
		appearance23.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance23.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lbMainTel.Appearance = appearance23;
		this.lbMainTel.Location = new System.Drawing.Point(92, 144);
		this.lbMainTel.Name = "lbMainTel";
		this.lbMainTel.Size = new System.Drawing.Size(108, 23);
		this.lbMainTel.TabIndex = 11;
		this.lbMainTel.Text = "主辦單位電話:";
		this.tbMainZipCode.AutoSize = true;
		this.tbMainZipCode.Location = new System.Drawing.Point(204, 116);
		this.tbMainZipCode.MaxLength = 10;
		this.tbMainZipCode.Name = "tbMainZipCode";
		this.tbMainZipCode.Size = new System.Drawing.Size(64, 24);
		this.tbMainZipCode.TabIndex = 10;
		this.tbMainZipCode.Leave += new System.EventHandler(tbMainZipCode_Leave);
		appearance24.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance24.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lbZipCodeAndAddress.Appearance = appearance24;
		this.lbZipCodeAndAddress.Location = new System.Drawing.Point(12, 116);
		this.lbZipCodeAndAddress.Name = "lbZipCodeAndAddress";
		this.lbZipCodeAndAddress.Size = new System.Drawing.Size(188, 23);
		this.lbZipCodeAndAddress.TabIndex = 9;
		this.lbZipCodeAndAddress.Text = "主辦單位區號地址:";
		this.btnNew.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance25.Image = resources.GetObject("appearance8.Image");
		appearance25.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnNew.Appearance = appearance25;
		this.btnNew.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnNew.Font = new System.Drawing.Font("細明體", 11f);
		this.btnNew.ImageSize = new System.Drawing.Size(20, 20);
		this.btnNew.ImageTransparentColor = System.Drawing.Color.White;
		this.btnNew.Location = new System.Drawing.Point(434, 144);
		this.btnNew.Name = "btnNew";
		this.btnNew.ShowFocusRect = false;
		this.btnNew.ShowOutline = false;
		this.btnNew.Size = new System.Drawing.Size(75, 28);
		this.btnNew.SupportThemes = false;
		this.btnNew.TabIndex = 7;
		this.btnNew.Text = "新增";
		this.btnNew.Click += new System.EventHandler(btnNew_Click);
		this.tbMainNameE.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.tbMainNameE.AutoSize = true;
		this.tbMainNameE.Location = new System.Drawing.Point(204, 88);
		this.tbMainNameE.Name = "tbMainNameE";
		this.tbMainNameE.Size = new System.Drawing.Size(382, 24);
		this.tbMainNameE.TabIndex = 9;
		this.tbMainNameE.Validating += new System.ComponentModel.CancelEventHandler(tbMainCode_Validating);
		this.tbMainNameE.Leave += new System.EventHandler(tbMainCode_Leave);
		this.tbMainNameE.Enter += new System.EventHandler(tbMainCode_Enter);
		this.tbMainName.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.tbMainName.AutoSize = true;
		this.tbMainName.Location = new System.Drawing.Point(204, 60);
		this.tbMainName.Name = "tbMainName";
		this.tbMainName.Size = new System.Drawing.Size(382, 24);
		this.tbMainName.TabIndex = 8;
		this.tbMainName.Validating += new System.ComponentModel.CancelEventHandler(tbMainCode_Validating);
		this.tbMainName.Leave += new System.EventHandler(tbMainCode_Leave);
		this.tbMainName.Enter += new System.EventHandler(tbMainCode_Enter);
		this.tbMainCode.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.tbMainCode.AutoSize = true;
		this.tbMainCode.Location = new System.Drawing.Point(204, 32);
		this.tbMainCode.Name = "tbMainCode";
		this.tbMainCode.Size = new System.Drawing.Size(382, 24);
		this.tbMainCode.TabIndex = 7;
		this.tbMainCode.Validating += new System.ComponentModel.CancelEventHandler(tbMainCode_Validating);
		this.tbMainCode.Leave += new System.EventHandler(tbMainCode_Leave);
		this.tbMainCode.Enter += new System.EventHandler(tbMainCode_Enter);
		appearance26.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance26.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lbMainCode.Appearance = appearance26;
		this.lbMainCode.Location = new System.Drawing.Point(12, 33);
		this.lbMainCode.Name = "lbMainCode";
		this.lbMainCode.Size = new System.Drawing.Size(188, 23);
		this.lbMainCode.TabIndex = 3;
		this.lbMainCode.Text = "主辦單位編號:";
		appearance27.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance27.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lbMainName.Appearance = appearance27;
		this.lbMainName.Location = new System.Drawing.Point(12, 60);
		this.lbMainName.Name = "lbMainName";
		this.lbMainName.Size = new System.Drawing.Size(188, 23);
		this.lbMainName.TabIndex = 2;
		this.lbMainName.Text = "主辦單位名稱(中文):";
		appearance28.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance28.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lbMainNameE.Appearance = appearance28;
		this.lbMainNameE.Location = new System.Drawing.Point(12, 89);
		this.lbMainNameE.Name = "lbMainNameE";
		this.lbMainNameE.Size = new System.Drawing.Size(188, 23);
		this.lbMainNameE.TabIndex = 1;
		this.lbMainNameE.Text = "主辦單位名稱(English):";
		appearance29.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel1.Appearance = appearance29;
		this.ultraLabel1.Location = new System.Drawing.Point(8, 8);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(120, 23);
		this.ultraLabel1.TabIndex = 0;
		this.ultraLabel1.Text = "新增主辦單位";
		this._FormSys_B_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
		this._FormSys_B_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 27);
		this._FormSys_B_Toolbars_Dock_Area_Left.Name = "_FormSys_B_Toolbars_Dock_Area_Left";
		this._FormSys_B_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 377);
		this._FormSys_B_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
		this._FormSys_B_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(600, 27);
		this._FormSys_B_Toolbars_Dock_Area_Right.Name = "_FormSys_B_Toolbars_Dock_Area_Right";
		this._FormSys_B_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 377);
		this._FormSys_B_Toolbars_Dock_Area_Right.ToolbarsManager = this.ultraToolbarsManager1;
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
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 404);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Name = "_FormSys_B_Toolbars_Dock_Area_Bottom";
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(600, 0);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ultraToolbarsManager1;
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.Magenta;
		this.imageList2.Images.SetKeyName(0, "");
		this.imageList2.Images.SetKeyName(1, "");
		this.imageList2.Images.SetKeyName(2, "");
		this.imageList2.Images.SetKeyName(3, "");
		this.imageList2.Images.SetKeyName(4, "");
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.Controls.Add(this.FormSys_B_Fill_Panel);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Left);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Right);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Top);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Bottom);
		base.Name = "FormSys_B";
		base.Size = new System.Drawing.Size(600, 404);
		base.Load += new System.EventHandler(FormSys_B_Load);
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridMainUnit).EndInit();
		this.FormSys_B_Fill_Panel.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.tbMainAddress).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbMainTel).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbMainZipCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbMainNameE).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbMainName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbMainCode).EndInit();
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

	public FormSys_B()
	{
		InitializeComponent();
		CellStyle cs1 = gridMainUnit.Styles.Add("EditMode");
		cs1.DataType = typeof(Image);
		cs1.ImageAlign = ImageAlignEnum.RightCenter;
	}

	private void FormSys_B_Load(object sender, EventArgs e)
	{
		ReloadData();
	}

	public void ReloadData()
	{
		LoadData();
		BindToGrid();
	}

	private void LoadData()
	{
		dsMainUnit = mainUnit.GetAllMainUnit();
		dsMainUnit.Tables[0].PrimaryKey = new DataColumn[1] { dsMainUnit.Tables[0].Columns["MainCode"] };
		StatusBar.Panels[0].Text = "資料筆數：" + dsMainUnit.Tables[0].Rows.Count;
	}

	private void BindToGrid()
	{
		DataTable dtMainUnit = dsMainUnit.Tables[0];
		gridMainUnit.Redraw = false;
		gridMainUnit.Rows.Count = dtMainUnit.Rows.Count + 1;
		for (int i = 0; i < dsMainUnit.Tables[0].Rows.Count; i++)
		{
			gridMainUnit[i + 1, "MainCode"] = dtMainUnit.Rows[i]["mainCode"].ToString().Trim();
			gridMainUnit[i + 1, "MainName"] = dtMainUnit.Rows[i]["mainName"].ToString().Trim();
			gridMainUnit[i + 1, "MainNameE"] = dtMainUnit.Rows[i]["mainNameE"].ToString().Trim();
			gridMainUnit[i + 1, "MainAddress"] = dtMainUnit.Rows[i]["MainAddress"].ToString().Trim();
			gridMainUnit[i + 1, "MainTel"] = dtMainUnit.Rows[i]["MainTel"].ToString().Trim();
			gridMainUnit[i + 1, "MainZipCode"] = dtMainUnit.Rows[i]["MainZipCode"].ToString().Trim();
		}
		SetColsEditSymbol();
		gridMainUnit.AutoSizeCols();
		gridMainUnit.Redraw = true;
	}

	private void btnNew_Click(object sender, EventArgs e)
	{
		if (!DBClass.ChkAuthority(UserID, "F00100010001"))
		{
			MessageBox.Show(this, DBClass.GetFuncName("F00100010001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		if (tbMainCode.Text.Trim() == "")
		{
			MessageBox.Show(this, "請先輸入單位代號！", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			tbMainCode.Focus();
			return;
		}
		if (mainUnit.Contains(tbMainCode.Text.Trim()))
		{
			MessageBox.Show(this, "相同代號資料已經存在，請重新輸入！", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		DataRow InsertRow = dsMainUnit.Tables[0].NewRow();
		InsertRow["mainCode"] = tbMainCode.Text.Trim();
		InsertRow["mainName"] = tbMainName.Text.Trim();
		InsertRow["mainNameE"] = tbMainNameE.Text.Trim();
		InsertRow["mainZipCode"] = tbMainZipCode.Text.Trim();
		InsertRow["mainAddress"] = tbMainAddress.Text.Trim();
		InsertRow["mainTel"] = tbMainTel.Text.Trim();
		dsMainUnit.Tables[0].Rows.Add(InsertRow);
		mainUnit.UpdateMainUnit(dsMainUnit);
		LoadData();
		BindToGrid();
		gridMainUnit.Row = gridMainUnit.FindRow(tbMainCode.Text, 1, 1, caseSensitive: false, fullMatch: false, wrap: false);
	}

	private void ultraToolbarsManager1_ToolClick(object sender, ToolClickEventArgs e)
	{
		switch (e.Tool.Key)
		{
		case "mnuDelete":
			DoDelete();
			break;
		case "mnu_Go":
			Do_ToolBarFind();
			break;
		}
	}

	private void DoDelete()
	{
		if (!DBClass.ChkAuthority(UserID, "F00100010002"))
		{
			MessageBox.Show(this, DBClass.GetFuncName("F00100010002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		if (MessageBox.Show(this, "是否確定要刪除？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			for (int i = gridMainUnit.Rows.Count - 1; i >= 1; i--)
			{
				if (gridMainUnit.Rows[i].Selected)
				{
					DataRow DeleteRow = dsMainUnit.Tables[0].Rows.Find(gridMainUnit[i, "MainCode"]);
					DeleteRow.Delete();
				}
			}
			mainUnit.UpdateMainUnit(dsMainUnit);
			LoadData();
			BindToGrid();
		}
		gridMainUnit.RowSel = -1;
	}

	private void Do_ToolBarFind()
	{
		if (!DBClass.ChkAuthority(UserID, "F00100010004"))
		{
			MessageBox.Show(this, DBClass.GetFuncName("F00100010004") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else
		{
			if (gridMainUnit.Rows.Count <= 1)
			{
				return;
			}
			int iStart = gridMainUnit.Row + 1;
			string sSearchText = ((ComboBoxTool)ultraToolbarsManager1.Tools["mnu_Cbo1"]).Text.Trim();
			if (!CommonMethods.CheckValidString(sSearchText))
			{
				return;
			}
			if (KeyWord != sSearchText.Trim())
			{
				iStart = 1;
				KeyWord = sSearchText.Trim();
			}
			else
			{
				iStart = gridMainUnit.Row + 1;
			}
			if (sSearchText.Trim() == "")
			{
				return;
			}
			for (int i = iStart; i < gridMainUnit.Rows.Count; i++)
			{
				for (int j = 1; j < gridMainUnit.Cols.Count; j++)
				{
					if (gridMainUnit[i, j] == null || gridMainUnit[i, j].ToString().IndexOf(sSearchText) <= -1)
					{
						continue;
					}
					gridMainUnit.Row = i;
					gridMainUnit.Select();
					gridMainUnit.TopRow = i;
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

	private void MainUnitGrid_AfterEdit(object sender, RowColEventArgs e)
	{
		DataRow UpdateRow = dsMainUnit.Tables[0].Rows.Find(gridMainUnit[e.Row, "MainCode"]);
		UpdateRow["mainName"] = gridMainUnit[e.Row, "MainName"].ToString().Trim();
		UpdateRow["mainNameE"] = gridMainUnit[e.Row, "MainNameE"].ToString().Trim();
		UpdateRow["mainZipCode"] = gridMainUnit[e.Row, "MainZipCode"].ToString().Trim();
		UpdateRow["mainAddress"] = gridMainUnit[e.Row, "MainAddress"].ToString().Trim();
		UpdateRow["mainTel"] = gridMainUnit[e.Row, "MainTel"].ToString().Trim();
		mainUnit.UpdateMainUnit(dsMainUnit);
	}

	private void MainUnitGrid_BeforeEdit(object sender, RowColEventArgs e)
	{
		if (gridMainUnit.Cols[e.Col].Name == "MainName" && !DBClass.ChkAuthority(UserID, "F001000100030001"))
		{
			AuthorityMessageCount++;
			if (AuthorityMessageCount <= 1)
			{
				MessageBox.Show(this, DBClass.GetFuncName("F001000100030001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			AuthorityMessageCount = 0;
			e.Cancel = true;
			gridMainUnit.Col = 0;
		}
		else if (gridMainUnit.Cols[e.Col].Name == "MainNameE" && !DBClass.ChkAuthority(UserID, "F001000100030002"))
		{
			AuthorityMessageCount++;
			if (AuthorityMessageCount <= 1)
			{
				MessageBox.Show(this, DBClass.GetFuncName("F001000100030002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			AuthorityMessageCount = 0;
			e.Cancel = true;
			gridMainUnit.Col = 0;
		}
		else
		{
			MainCode = gridMainUnit[e.Row, "MainCode"].ToString().Trim();
			MainName = gridMainUnit[e.Row, "MainName"].ToString().Trim();
			MainNameE = gridMainUnit[e.Row, "MainNameE"].ToString().Trim();
			if (!gridMainUnit.Cols[e.Col].AllowEditing)
			{
				e.Cancel = true;
				gridMainUnit.Col = 0;
			}
		}
	}

	private void MainUnitGrid_AfterSelChange(object sender, RangeEventArgs e)
	{
		if (!gridMainUnit.Cols[gridMainUnit.Col].AllowEditing)
		{
			gridMainUnit.Col = 0;
		}
	}

	private void ultraToolbarsManager1_ToolKeyPress(object sender, ToolKeyPressEventArgs e)
	{
		if (e.KeyChar == '\r' && e.Tool.Key == "mnu_Cbo1")
		{
			Do_ToolBarFind();
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

	private void panel2_Resize(object sender, EventArgs e)
	{
		gridMainUnit.AutoSizeCols();
	}

	private void MainUnitGrid_MouseDown(object sender, MouseEventArgs e)
	{
		int rowIndex = gridMainUnit.MouseRow;
		int colIndex = gridMainUnit.MouseCol;
		gridMainUnit.Row = rowIndex;
		gridMainUnit.Select();
		if (gridMainUnit.Row <= 0 || rowIndex <= 0 || colIndex <= 0)
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

	private void tbMainCode_Validating(object sender, CancelEventArgs e)
	{
		if (!CommonMethods.CheckValidString((sender as UltraTextEditor).Text))
		{
			e.Cancel = true;
		}
		if (!CommonMethods.IsStrByteLenValid(tbMainCode.Text, 10))
		{
			MessageBox.Show(this, "主辦單位編號的長度不可超過 20 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			tbMainCode.Focus();
		}
		else if (tbMainName.Text.Trim().Length > 50)
		{
			MessageBox.Show(this, "主辦單位名稱(中文)的長度不可超過 50 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			tbMainName.Focus();
		}
		else if (!CommonMethods.IsStrByteLenValid(tbMainNameE.Text, 200))
		{
			MessageBox.Show(this, "主辦單位名稱(English)的長度不可超過 200 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			tbMainNameE.Focus();
		}
	}

	private void tbMainCode_Enter(object sender, EventArgs e)
	{
		((ButtonTool)ultraToolbarsManager1.Tools["mnuDelete"]).SharedProps.Shortcut = Shortcut.None;
	}

	private void tbMainCode_Leave(object sender, EventArgs e)
	{
		((ButtonTool)ultraToolbarsManager1.Tools["mnuDelete"]).SharedProps.Shortcut = Shortcut.Del;
	}

	private void tbMainZipCode_Leave(object sender, EventArgs e)
	{
		string inputZipCode = (sender as UltraTextEditor).Text;
		if (!(inputZipCode.Trim() == string.Empty) && !double.TryParse(inputZipCode, out var _))
		{
			MessageBox.Show(this, "請輸入數字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			(sender as UltraTextEditor).Focus();
		}
	}

	private void btnImport_Click(object sender, EventArgs e)
	{
		openImportXMLFile.RestoreDirectory = true;
		openImportXMLFile.Filter = "主辦機關電子檔(*.xml)|*.xml";
		if (openImportXMLFile.ShowDialog() == DialogResult.OK)
		{
			string FilePath = openImportXMLFile.FileName;
			string warning = "挑選之主辦機關電子檔格式有誤，請重新挑選。";
			DataSet dsOwnerList = new DataSet();
			try
			{
				dsOwnerList.ReadXml(FilePath);
			}
			catch (Exception ex)
			{
				DebugUtil.OutputDebugString(ex.Message);
				MessageBox.Show(this, warning, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			DataColumnCollection SponsorListColumns = dsOwnerList.Tables[0].Columns;
			if (!SponsorListColumns.Contains("MainCode") || !SponsorListColumns.Contains("MainName") || !SponsorListColumns.Contains("MainNameE") || !SponsorListColumns.Contains("MainAddress") || !SponsorListColumns.Contains("MainTel") || !SponsorListColumns.Contains("MainZipCode"))
			{
				MessageBox.Show(this, warning, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (dsOwnerList.Tables[0].Rows.Count > 0)
			{
				FormSys_G_Info1 FM_INFO = new FormSys_G_Info1();
				FM_INFO.TopMost = true;
				FM_INFO._InfoString = "資料匯入中，請稍候！";
				FM_INFO.Show();
				Application.DoEvents();
				mainUnit.UpdateMainUnit(dsOwnerList);
				LoadData();
				BindToGrid();
				FM_INFO.Close();
				MessageBox.Show(this, "資料匯入完成。", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private void SetColsEditSymbol()
	{
		for (int i = 1; i < gridMainUnit.Cols.Count; i++)
		{
			if (gridMainUnit.Cols[i].AllowEditing)
			{
				CellRange cellRange = gridMainUnit.GetCellRange(0, i);
				cellRange.Style = gridMainUnit.Styles["EditMode"];
				cellRange.Image = imageList2.Images[2];
			}
		}
	}
}
