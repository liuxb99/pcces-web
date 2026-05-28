using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.STDClass;
using AxThreed;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinStatusBar;
using Infragistics.Win.UltraWinToolbars;

namespace Archnowledge.Pcces.PccesMain.Compare;

public class FormCompareItm : Form
{
	private ImageList iglst_splt_Btn;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Top;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Bottom;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Left;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Right;

	private ImageList imageList2;

	private Panel LeftPanel;

	private OnlineList onlineList1;

	public FunctionButtons functionButtons1;

	private Panel pnl_spliter;

	private UltraButton Btn_Splt;

	private AxSSPanel ssp_Lower;

	private AxSSPanel ssp_Bottom;

	private AxSSPanel ssp_Upper;

	private AxSSPanel ssp_Top;

	private Panel panel1;

	private UltraLabel ultraLabel10;

	private UltraLabel ultraLabel1;

	private Panel PNL_UPPER;

	private UltraButton BtnExecute;

	private PictureBox pictureBox2;

	private PictureBox pictureBox1;

	private UltraLabel ultraLabel9;

	private UltraComboEditor dpBase;

	private UltraLabel ultraLabel5;

	private UltraLabel ultraLabel4;

	private Panel panel2;

	private UltraStatusBar ultraStatusBar1;

	private UltraLabel ultraLabel3;

	private UltraLabel ultraLabel2;

	private UltraComboEditor dpCmpItem;

	private UltraLabel ultraLabel11;

	private Panel Pnl_Spliter_Hor;

	private AxSSPanel ssp_Left;

	private AxSSPanel ssp_Lefter;

	private AxSSPanel ssp_Right;

	private AxSSPanel ssp_Righter;

	private UltraButton Btn_SpltHor;

	private GridBudget gridBudget1;

	private ImageList iglst_splt_Btn2;

	private C1FlexGrid GridCmp;

	private UltraLabel ultraLabel6;

	private PictureBox pictureBox3;

	private UltraOptionSet Op1;

	private UltraLabel ultraLabel7;

	private PictureBox pictureBox4;

	private SaveFileDialog saveFileDialog1;

	private UltraToolbarsManager ultraToolbarsManager1;

	private IContainer components;

	private int iidx = -1;

	private FormStatus FORM_STATUS = FormStatus.Iinitial;

	private string[] ls_Val = new string[10];

	private decimal ScopeS = 0m;

	private decimal[] Scope = new decimal[10];

	private bool F_IsRunCompare = false;

	private int F_MainQty = 0;

	private int F_MainCst = 0;

	private int F_MainAmt = 0;

	private int F_AnaQty = 0;

	private int F_AnaCst = 0;

	private int F_AnaAmt = 0;

	private bool F_HasRegistered;

	private string F_UserID;

	private string F_UserName = "";

	private string F_FunctionName = "CompareItem";

	private string F_ServerName = "localhost";

	private LeftPanelMode PanelMode = LeftPanelMode.Open;

	private LeftPanelMode MidPanelMode = LeftPanelMode.Open;

	private int GridCols = 15;

	private object[,] GridColsSquence;

	private PccesFormAction F_ActionName = PccesFormAction.BUD;

	private decimal F_dec_TempValue = 0m;

	private DataTable DT1 = new DataTable();

	private DataTable DT_DP = new DataTable();

	private string F_KeyWord = "";

	public decimal _dec_TempValue
	{
		get
		{
			return F_dec_TempValue;
		}
		set
		{
			F_dec_TempValue = value;
		}
	}

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

	public string _UserName
	{
		get
		{
			return F_UserName;
		}
		set
		{
			F_UserName = value;
		}
	}

	public string _FunctionName
	{
		get
		{
			return F_FunctionName;
		}
		set
		{
			F_FunctionName = value;
		}
	}

	public string _ServerName
	{
		get
		{
			return F_ServerName;
		}
		set
		{
			F_ServerName = value;
		}
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Compare.FormCompareItm));
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Tool1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuOpenPanel");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExport");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool1 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnu_lblFind");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool1 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_Go");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar2 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Tool2");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool2 = new Infragistics.Win.UltraWinToolbars.LabelTool("lblShowItem");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool2 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnuCbo_Show");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool3 = new Infragistics.Win.UltraWinToolbars.LabelTool("lblRatio");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool3 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnuCbo_Differ");
		Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool4 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnu_lblFind");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool4 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo1");
		Infragistics.Win.ValueList valueList1 = new Infragistics.Win.ValueList(0);
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_Go");
		Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool1 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Popup1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool5 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuOpenPanel");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool6 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExport");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool5 = new Infragistics.Win.UltraWinToolbars.LabelTool("lblShowItem");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool5 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnuCbo_Show");
		Infragistics.Win.ValueList valueList2 = new Infragistics.Win.ValueList(0);
		Infragistics.Win.ValueListItem valueListItem6 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem7 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem8 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool6 = new Infragistics.Win.UltraWinToolbars.LabelTool("lblRatio");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool6 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnuCbo_Differ");
		Infragistics.Win.ValueList valueList3 = new Infragistics.Win.ValueList(0);
		Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem9 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem10 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem11 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem12 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem13 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
		this.iglst_splt_Btn = new System.Windows.Forms.ImageList(this.components);
		this.ultraToolbarsManager1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
		this._FormSys_B_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.LeftPanel = new System.Windows.Forms.Panel();
		this.onlineList1 = new Archnowledge.Pcces.PccesMain.ArchControls.OnlineList();
		this.functionButtons1 = new Archnowledge.Pcces.PccesMain.ArchControls.FunctionButtons();
		this.pnl_spliter = new System.Windows.Forms.Panel();
		this.Btn_Splt = new Infragistics.Win.Misc.UltraButton();
		this.ssp_Lower = new AxThreed.AxSSPanel();
		this.ssp_Bottom = new AxThreed.AxSSPanel();
		this.ssp_Upper = new AxThreed.AxSSPanel();
		this.ssp_Top = new AxThreed.AxSSPanel();
		this.panel1 = new System.Windows.Forms.Panel();
		this.ultraLabel10 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.PNL_UPPER = new System.Windows.Forms.Panel();
		this.Op1 = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.pictureBox4 = new System.Windows.Forms.PictureBox();
		this.GridCmp = new C1.Win.C1FlexGrid.C1FlexGrid();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.pictureBox3 = new System.Windows.Forms.PictureBox();
		this.dpCmpItem = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.BtnExecute = new Infragistics.Win.Misc.UltraButton();
		this.pictureBox2 = new System.Windows.Forms.PictureBox();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.ultraLabel9 = new Infragistics.Win.Misc.UltraLabel();
		this.dpBase = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.ultraLabel11 = new Infragistics.Win.Misc.UltraLabel();
		this.gridBudget1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraStatusBar1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.Pnl_Spliter_Hor = new System.Windows.Forms.Panel();
		this.Btn_SpltHor = new Infragistics.Win.Misc.UltraButton();
		this.ssp_Righter = new AxThreed.AxSSPanel();
		this.ssp_Right = new AxThreed.AxSSPanel();
		this.ssp_Lefter = new AxThreed.AxSSPanel();
		this.ssp_Left = new AxThreed.AxSSPanel();
		this.iglst_splt_Btn2 = new System.Windows.Forms.ImageList(this.components);
		this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).BeginInit();
		this.LeftPanel.SuspendLayout();
		this.pnl_spliter.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ssp_Lower).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Bottom).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Upper).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Top).BeginInit();
		this.panel1.SuspendLayout();
		this.PNL_UPPER.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Op1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.GridCmp).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dpCmpItem).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dpBase).BeginInit();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridBudget1).BeginInit();
		this.Pnl_Spliter_Hor.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ssp_Righter).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Right).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Lefter).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Left).BeginInit();
		base.SuspendLayout();
		this.iglst_splt_Btn.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("iglst_splt_Btn.ImageStream");
		this.iglst_splt_Btn.TransparentColor = System.Drawing.Color.Transparent;
		this.iglst_splt_Btn.Images.SetKeyName(0, "");
		this.iglst_splt_Btn.Images.SetKeyName(1, "");
		this.iglst_splt_Btn.Images.SetKeyName(2, "");
		this.iglst_splt_Btn.Images.SetKeyName(3, "");
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
		this.ultraToolbarsManager1.ShowQuickCustomizeButton = false;
		this.ultraToolbarsManager1.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2003;
		ultraToolbar1.DockedColumn = 0;
		ultraToolbar1.DockedRow = 0;
		ultraToolbar1.Settings.AllowCustomize = Infragistics.Win.DefaultableBoolean.False;
		ultraToolbar1.Settings.AllowDockTop = Infragistics.Win.DefaultableBoolean.True;
		ultraToolbar1.Text = "Tool1";
		buttonTool1.InstanceProps.IsFirstInGroup = true;
		buttonTool2.InstanceProps.IsFirstInGroup = true;
		labelTool1.InstanceProps.IsFirstInGroup = true;
		ultraToolbar1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[5] { buttonTool1, buttonTool2, labelTool1, comboBoxTool1, buttonTool3 });
		ultraToolbar2.DockedColumn = 0;
		ultraToolbar2.DockedRow = 1;
		ultraToolbar2.Text = "Tool2";
		labelTool3.InstanceProps.IsFirstInGroup = true;
		ultraToolbar2.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[4] { labelTool2, comboBoxTool2, labelTool3, comboBoxTool3 });
		ultraToolbar2.Visible = false;
		this.ultraToolbarsManager1.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[2] { ultraToolbar1, ultraToolbar2 });
		appearance20.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance20.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.ToolbarSettings.Appearance = appearance20;
		appearance21.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance21.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance21.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.ToolbarSettings.HotTrackAppearance = appearance21;
		labelTool4.SharedProps.Caption = "尋找:";
		labelTool4.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		comboBoxTool4.DropDownStyle = Infragistics.Win.DropDownStyle.DropDown;
		comboBoxTool4.SharedProps.Caption = "輸入關鍵字";
		comboBoxTool4.SharedProps.Width = 200;
		comboBoxTool4.ValueList = valueList1;
		appearance22.Image = resources.GetObject("appearance22.Image");
		buttonTool4.SharedProps.AppearancesSmall.Appearance = appearance22;
		buttonTool4.SharedProps.Caption = "執行";
		popupMenuTool1.SharedProps.Caption = "右鍵功能表";
		buttonTool5.SharedProps.Caption = "隱藏比對條件";
		buttonTool5.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool6.SharedProps.Caption = "匯出";
		buttonTool6.SharedProps.CustomizerCaption = "匯出Excel 格式 的比對結果 ";
		buttonTool6.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool6.SharedProps.ToolTipText = "匯出Excel 格式 的比對結果 ";
		labelTool5.SharedProps.Caption = "顯示項目:";
		labelTool5.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		comboBoxTool5.DropDownStyle = Infragistics.Win.DropDownStyle.DropDown;
		comboBoxTool5.SharedProps.Caption = "全部顯示";
		comboBoxTool5.SharedProps.Width = 200;
		valueListItem6.DataValue = "0";
		valueListItem6.DisplayText = "全部顯示";
		valueListItem7.DataValue = "2";
		valueListItem7.DisplayText = "特有項目";
		valueListItem8.DataValue = "1";
		valueListItem8.DisplayText = "差異項目";
		valueList2.ValueListItems.Add(valueListItem6);
		valueList2.ValueListItems.Add(valueListItem7);
		valueList2.ValueListItems.Add(valueListItem8);
		comboBoxTool5.ValueList = valueList2;
		labelTool6.SharedProps.Caption = "差異百分比:";
		labelTool6.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		comboBoxTool6.DropDownStyle = Infragistics.Win.DropDownStyle.DropDown;
		comboBoxTool6.SharedProps.Caption = "差異百分比";
		comboBoxTool6.SharedProps.Width = 200;
		comboBoxTool6.ValueList = valueList3;
		this.ultraToolbarsManager1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[10] { labelTool4, comboBoxTool4, buttonTool4, popupMenuTool1, buttonTool5, buttonTool6, labelTool5, comboBoxTool5, labelTool6, comboBoxTool6 });
		this.ultraToolbarsManager1.ToolKeyPress += new Infragistics.Win.UltraWinToolbars.ToolKeyPressEventHandler(ultraToolbarsManager1_ToolKeyPress);
		this.ultraToolbarsManager1.BeforeToolbarListDropdown += new Infragistics.Win.UltraWinToolbars.BeforeToolbarListDropdownEventHandler(ultraToolbarsManager1_BeforeToolbarListDropdown);
		this.ultraToolbarsManager1.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(ultraToolbarsManager1_ToolClick);
		this.ultraToolbarsManager1.AfterToolCloseup += new Infragistics.Win.UltraWinToolbars.ToolDropdownEventHandler(ultraToolbarsManager1_AfterToolCloseup);
		this._FormSys_B_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
		this._FormSys_B_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
		this._FormSys_B_Toolbars_Dock_Area_Top.Name = "_FormSys_B_Toolbars_Dock_Area_Top";
		this._FormSys_B_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(782, 27);
		this._FormSys_B_Toolbars_Dock_Area_Top.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 563);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Name = "_FormSys_B_Toolbars_Dock_Area_Bottom";
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(782, 0);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
		this._FormSys_B_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 27);
		this._FormSys_B_Toolbars_Dock_Area_Left.Name = "_FormSys_B_Toolbars_Dock_Area_Left";
		this._FormSys_B_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 536);
		this._FormSys_B_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
		this._FormSys_B_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(782, 27);
		this._FormSys_B_Toolbars_Dock_Area_Right.Name = "_FormSys_B_Toolbars_Dock_Area_Right";
		this._FormSys_B_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 536);
		this._FormSys_B_Toolbars_Dock_Area_Right.ToolbarsManager = this.ultraToolbarsManager1;
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.White;
		this.imageList2.Images.SetKeyName(0, "");
		this.imageList2.Images.SetKeyName(1, "");
		this.LeftPanel.Controls.Add(this.onlineList1);
		this.LeftPanel.Controls.Add(this.functionButtons1);
		this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
		this.LeftPanel.Location = new System.Drawing.Point(0, 27);
		this.LeftPanel.Name = "LeftPanel";
		this.LeftPanel.Size = new System.Drawing.Size(160, 536);
		this.LeftPanel.TabIndex = 8;
		this.onlineList1._FunctionName = "";
		this.onlineList1._HasRegistered = false;
		this.onlineList1._ServerName = "localhost";
		this.onlineList1._TRY_Flag = "";
		this.onlineList1._UserID = "";
		this.onlineList1._UserName = "";
		this.onlineList1.AutoSize = true;
		this.onlineList1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.onlineList1.Dock = System.Windows.Forms.DockStyle.Top;
		this.onlineList1.Location = new System.Drawing.Point(0, 0);
		this.onlineList1.Name = "onlineList1";
		this.onlineList1.Size = new System.Drawing.Size(160, 256);
		this.onlineList1.TabIndex = 4;
		this.functionButtons1._ActiveFunction = "";
		this.functionButtons1._CurrOpenMode = Archnowledge.Pcces.CommonClass.FunctionOpenMode.Budget;
		this.functionButtons1._ServerName = "localhost";
		this.functionButtons1._UserID = "PccesAdmin";
		this.functionButtons1._UserName = "";
		this.functionButtons1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.functionButtons1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.functionButtons1.Location = new System.Drawing.Point(0, 0);
		this.functionButtons1.Name = "functionButtons1";
		this.functionButtons1.Size = new System.Drawing.Size(160, 536);
		this.functionButtons1.TabIndex = 3;
		this.pnl_spliter.BackColor = System.Drawing.Color.LightGray;
		this.pnl_spliter.Controls.Add(this.Btn_Splt);
		this.pnl_spliter.Controls.Add(this.ssp_Lower);
		this.pnl_spliter.Controls.Add(this.ssp_Bottom);
		this.pnl_spliter.Controls.Add(this.ssp_Upper);
		this.pnl_spliter.Controls.Add(this.ssp_Top);
		this.pnl_spliter.Dock = System.Windows.Forms.DockStyle.Left;
		this.pnl_spliter.Location = new System.Drawing.Point(160, 27);
		this.pnl_spliter.Name = "pnl_spliter";
		this.pnl_spliter.Size = new System.Drawing.Size(7, 536);
		this.pnl_spliter.TabIndex = 9;
		appearance23.BorderColor = System.Drawing.Color.Transparent;
		appearance23.BorderColor3DBase = System.Drawing.Color.Transparent;
		appearance23.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance16.ImageBackground");
		this.Btn_Splt.Appearance = appearance23;
		this.Btn_Splt.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Borderless;
		this.Btn_Splt.Cursor = System.Windows.Forms.Cursors.Hand;
		this.Btn_Splt.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Btn_Splt.ImageSize = new System.Drawing.Size(7, 57);
		this.Btn_Splt.Location = new System.Drawing.Point(0, 228);
		this.Btn_Splt.Name = "Btn_Splt";
		this.Btn_Splt.ShapeImage = (System.Drawing.Image)resources.GetObject("Btn_Splt.ShapeImage");
		this.Btn_Splt.ShowFocusRect = false;
		this.Btn_Splt.ShowOutline = false;
		this.Btn_Splt.Size = new System.Drawing.Size(7, 83);
		this.Btn_Splt.TabIndex = 7;
		this.Btn_Splt.MouseLeave += new System.EventHandler(Btn_Splt_MouseLeave);
		this.Btn_Splt.Click += new System.EventHandler(Btn_Splt_Click);
		this.Btn_Splt.MouseEnter += new System.EventHandler(Btn_Splt_MouseEnter);
		this.ssp_Lower.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.ssp_Lower.Location = new System.Drawing.Point(0, 311);
		this.ssp_Lower.Name = "ssp_Lower";
		this.ssp_Lower.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Lower.OcxState");
		this.ssp_Lower.Size = new System.Drawing.Size(7, 222);
		this.ssp_Lower.TabIndex = 6;
		this.ssp_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.ssp_Bottom.Location = new System.Drawing.Point(0, 533);
		this.ssp_Bottom.Name = "ssp_Bottom";
		this.ssp_Bottom.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Bottom.OcxState");
		this.ssp_Bottom.Size = new System.Drawing.Size(7, 3);
		this.ssp_Bottom.TabIndex = 5;
		this.ssp_Upper.Dock = System.Windows.Forms.DockStyle.Top;
		this.ssp_Upper.Location = new System.Drawing.Point(0, 3);
		this.ssp_Upper.Name = "ssp_Upper";
		this.ssp_Upper.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Upper.OcxState");
		this.ssp_Upper.Size = new System.Drawing.Size(7, 225);
		this.ssp_Upper.TabIndex = 3;
		this.ssp_Top.Dock = System.Windows.Forms.DockStyle.Top;
		this.ssp_Top.Location = new System.Drawing.Point(0, 0);
		this.ssp_Top.Name = "ssp_Top";
		this.ssp_Top.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Top.OcxState");
		this.ssp_Top.Size = new System.Drawing.Size(7, 3);
		this.ssp_Top.TabIndex = 2;
		this.panel1.Controls.Add(this.ultraLabel10);
		this.panel1.Controls.Add(this.ultraLabel1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(167, 27);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(615, 30);
		this.panel1.TabIndex = 10;
		appearance24.ForeColor = System.Drawing.Color.White;
		appearance24.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel10.Appearance = appearance24;
		this.ultraLabel10.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel10.Font = new System.Drawing.Font("細明體", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel10.Location = new System.Drawing.Point(6, 7);
		this.ultraLabel10.Name = "ultraLabel10";
		this.ultraLabel10.Size = new System.Drawing.Size(74, 19);
		this.ultraLabel10.TabIndex = 14;
		this.ultraLabel10.Text = "比對條件";
		this.ultraLabel1.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel1.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.ultraLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ultraLabel1.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(615, 30);
		this.ultraLabel1.TabIndex = 0;
		this.PNL_UPPER.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.PNL_UPPER.Controls.Add(this.Op1);
		this.PNL_UPPER.Controls.Add(this.ultraLabel7);
		this.PNL_UPPER.Controls.Add(this.pictureBox4);
		this.PNL_UPPER.Controls.Add(this.GridCmp);
		this.PNL_UPPER.Controls.Add(this.ultraLabel6);
		this.PNL_UPPER.Controls.Add(this.pictureBox3);
		this.PNL_UPPER.Controls.Add(this.dpCmpItem);
		this.PNL_UPPER.Controls.Add(this.ultraLabel2);
		this.PNL_UPPER.Controls.Add(this.BtnExecute);
		this.PNL_UPPER.Controls.Add(this.pictureBox2);
		this.PNL_UPPER.Controls.Add(this.pictureBox1);
		this.PNL_UPPER.Controls.Add(this.ultraLabel9);
		this.PNL_UPPER.Controls.Add(this.dpBase);
		this.PNL_UPPER.Controls.Add(this.ultraLabel5);
		this.PNL_UPPER.Controls.Add(this.ultraLabel4);
		this.PNL_UPPER.Dock = System.Windows.Forms.DockStyle.Top;
		this.PNL_UPPER.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.PNL_UPPER.Location = new System.Drawing.Point(167, 57);
		this.PNL_UPPER.Name = "PNL_UPPER";
		this.PNL_UPPER.Size = new System.Drawing.Size(615, 200);
		this.PNL_UPPER.TabIndex = 11;
		this.Op1.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
		this.Op1.ItemAppearance = appearance25;
		valueListItem9.DataValue = "BUD";
		valueListItem9.DisplayText = "預算書";
		valueListItem10.DataValue = "BID";
		valueListItem10.DisplayText = "標單";
		this.Op1.Items.Add(valueListItem9);
		this.Op1.Items.Add(valueListItem10);
		this.Op1.ItemSpacingHorizontal = 10;
		this.Op1.ItemSpacingVertical = 10;
		this.Op1.Location = new System.Drawing.Point(64, 32);
		this.Op1.Name = "Op1";
		this.Op1.Size = new System.Drawing.Size(220, 32);
		this.Op1.TabIndex = 23;
		this.Op1.ValueChanged += new System.EventHandler(Op1_ValueChanged);
		appearance26.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance26.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel7.Appearance = appearance26;
		this.ultraLabel7.Location = new System.Drawing.Point(49, 12);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(75, 23);
		this.ultraLabel7.TabIndex = 22;
		this.ultraLabel7.Text = "選擇類別";
		this.pictureBox4.Image = (System.Drawing.Image)resources.GetObject("pictureBox4.Image");
		this.pictureBox4.Location = new System.Drawing.Point(12, 4);
		this.pictureBox4.Name = "pictureBox4";
		this.pictureBox4.Size = new System.Drawing.Size(40, 36);
		this.pictureBox4.TabIndex = 21;
		this.pictureBox4.TabStop = false;
		this.GridCmp.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GridCmp.BackColor = System.Drawing.Color.LightGray;
		this.GridCmp.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.GridCmp.ColumnInfo = resources.GetString("GridCmp.ColumnInfo");
		this.GridCmp.ExtendLastCol = true;
		this.GridCmp.ForeColor = System.Drawing.SystemColors.WindowText;
		this.GridCmp.Location = new System.Drawing.Point(348, 36);
		this.GridCmp.Name = "GridCmp";
		this.GridCmp.Rows.Count = 4;
		this.GridCmp.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.GridCmp.Size = new System.Drawing.Size(256, 156);
		this.GridCmp.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("GridCmp.Styles"));
		this.GridCmp.TabIndex = 19;
		appearance27.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance27.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel6.Appearance = appearance27;
		this.ultraLabel6.Location = new System.Drawing.Point(352, 12);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(200, 23);
		this.ultraLabel6.TabIndex = 18;
		this.ultraLabel6.Text = "勾選比對案(至多勾選10筆)";
		this.pictureBox3.Image = (System.Drawing.Image)resources.GetObject("pictureBox3.Image");
		this.pictureBox3.Location = new System.Drawing.Point(312, 4);
		this.pictureBox3.Name = "pictureBox3";
		this.pictureBox3.Size = new System.Drawing.Size(36, 32);
		this.pictureBox3.TabIndex = 17;
		this.pictureBox3.TabStop = false;
		this.dpCmpItem.AutoSize = true;
		this.dpCmpItem.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
		valueListItem11.DataValue = "0";
		valueListItem11.DisplayText = "數量";
		valueListItem12.DataValue = "2";
		valueListItem12.DisplayText = "複價";
		valueListItem13.DataValue = "1";
		valueListItem13.DisplayText = "單價";
		this.dpCmpItem.Items.Add(valueListItem11);
		this.dpCmpItem.Items.Add(valueListItem12);
		this.dpCmpItem.Items.Add(valueListItem13);
		this.dpCmpItem.Location = new System.Drawing.Point(116, 164);
		this.dpCmpItem.Name = "dpCmpItem";
		this.dpCmpItem.Size = new System.Drawing.Size(96, 24);
		this.dpCmpItem.TabIndex = 15;
		this.dpCmpItem.Text = null;
		this.dpCmpItem.ValueChanged += new System.EventHandler(dpCmpItem_ValueChanged);
		appearance28.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel2.Appearance = appearance28;
		this.ultraLabel2.Location = new System.Drawing.Point(36, 166);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(75, 23);
		this.ultraLabel2.TabIndex = 14;
		this.ultraLabel2.Text = "比對項目:";
		appearance29.Image = resources.GetObject("appearance11.Image");
		this.BtnExecute.Appearance = appearance29;
		this.BtnExecute.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnExecute.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnExecute.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.BtnExecute.Location = new System.Drawing.Point(224, 160);
		this.BtnExecute.Name = "BtnExecute";
		this.BtnExecute.ShowFocusRect = false;
		this.BtnExecute.ShowOutline = false;
		this.BtnExecute.Size = new System.Drawing.Size(96, 31);
		this.BtnExecute.SupportThemes = false;
		this.BtnExecute.TabIndex = 13;
		this.BtnExecute.Text = "確定比對";
		this.BtnExecute.Click += new System.EventHandler(BtnExecute_Click);
		this.pictureBox2.Image = (System.Drawing.Image)resources.GetObject("pictureBox2.Image");
		this.pictureBox2.Location = new System.Drawing.Point(12, 130);
		this.pictureBox2.Name = "pictureBox2";
		this.pictureBox2.Size = new System.Drawing.Size(36, 32);
		this.pictureBox2.TabIndex = 12;
		this.pictureBox2.TabStop = false;
		this.pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
		this.pictureBox1.Location = new System.Drawing.Point(12, 60);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(40, 36);
		this.pictureBox1.TabIndex = 11;
		this.pictureBox1.TabStop = false;
		appearance30.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance30.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel9.Appearance = appearance30;
		this.ultraLabel9.Location = new System.Drawing.Point(49, 136);
		this.ultraLabel9.Name = "ultraLabel9";
		this.ultraLabel9.Size = new System.Drawing.Size(172, 23);
		this.ultraLabel9.TabIndex = 9;
		this.ultraLabel9.Text = "設定比對內容";
		this.dpBase.AutoSize = true;
		this.dpBase.DropDownListWidth = 400;
		this.dpBase.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
		this.dpBase.Location = new System.Drawing.Point(95, 98);
		this.dpBase.Name = "dpBase";
		this.dpBase.Size = new System.Drawing.Size(201, 24);
		this.dpBase.TabIndex = 5;
		this.dpBase.Text = null;
		appearance31.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel5.Appearance = appearance31;
		this.ultraLabel5.Location = new System.Drawing.Point(16, 98);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(88, 23);
		this.ultraLabel5.TabIndex = 1;
		this.ultraLabel5.Text = "基準標案:";
		appearance32.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance32.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel4.Appearance = appearance32;
		this.ultraLabel4.Location = new System.Drawing.Point(49, 68);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(200, 23);
		this.ultraLabel4.TabIndex = 0;
		this.ultraLabel4.Text = "挑選基準案";
		this.panel2.Controls.Add(this.ultraLabel11);
		this.panel2.Controls.Add(this.gridBudget1);
		this.panel2.Controls.Add(this.ultraLabel3);
		this.panel2.Controls.Add(this.ultraStatusBar1);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Location = new System.Drawing.Point(167, 264);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(615, 299);
		this.panel2.TabIndex = 13;
		appearance33.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel11.Appearance = appearance33;
		this.ultraLabel11.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.ultraLabel11.Font = new System.Drawing.Font("細明體", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel11.Location = new System.Drawing.Point(6, 6);
		this.ultraLabel11.Name = "ultraLabel11";
		this.ultraLabel11.Size = new System.Drawing.Size(162, 19);
		this.ultraLabel11.TabIndex = 15;
		this.ultraLabel11.Text = "比對結果";
		this.gridBudget1._ExcelFileName = "";
		this.gridBudget1._ExcelSheeName = "";
		this.gridBudget1._IsOpenExcelAfterExport = false;
		this.gridBudget1.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
		this.gridBudget1.AllowEditing = false;
		this.gridBudget1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridBudget1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
		this.gridBudget1.ColumnInfo = resources.GetString("gridBudget1.ColumnInfo");
		this.gridBudget1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridBudget1.ExtendLastCol = true;
		this.gridBudget1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridBudget1.ForeColor = System.Drawing.Color.Black;
		this.gridBudget1.Location = new System.Drawing.Point(0, 30);
		this.gridBudget1.Name = "gridBudget1";
		this.gridBudget1.Rows.Count = 1;
		this.gridBudget1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
		this.gridBudget1.ShowCursor = true;
		this.gridBudget1.ShowToolTipOnNarrowColumn = true;
		this.gridBudget1.Size = new System.Drawing.Size(615, 243);
		this.gridBudget1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridBudget1.Styles"));
		this.gridBudget1.TabIndex = 12;
		this.gridBudget1.Tree.Column = 1;
		this.gridBudget1.Tree.LineColor = System.Drawing.Color.Gray;
		this.gridBudget1.Resize += new System.EventHandler(gridBudget1_Resize);
		this.ultraLabel3.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.ultraLabel3.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.ultraLabel3.Dock = System.Windows.Forms.DockStyle.Top;
		this.ultraLabel3.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(615, 30);
		this.ultraLabel3.TabIndex = 11;
		appearance34.FontData.SizeInPoints = 11f;
		appearance34.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraStatusBar1.Appearance = appearance34;
		this.ultraStatusBar1.Location = new System.Drawing.Point(0, 273);
		this.ultraStatusBar1.Name = "ultraStatusBar1";
		appearance35.TextVAlign = Infragistics.Win.VAlign.Bottom;
		ultraStatusPanel1.Appearance = appearance35;
		ultraStatusPanel1.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel1.Key = "RowsCount";
		ultraStatusPanel1.Text = "資料筆數：";
		ultraStatusPanel1.Width = 200;
		ultraStatusPanel2.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel2.Key = "ProgressBar";
		ultraStatusPanel2.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		this.ultraStatusBar1.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[2] { ultraStatusPanel1, ultraStatusPanel2 });
		this.ultraStatusBar1.Size = new System.Drawing.Size(615, 26);
		this.ultraStatusBar1.TabIndex = 10;
		this.ultraStatusBar1.Text = "ultraStatusBar1";
		this.Pnl_Spliter_Hor.Controls.Add(this.Btn_SpltHor);
		this.Pnl_Spliter_Hor.Controls.Add(this.ssp_Righter);
		this.Pnl_Spliter_Hor.Controls.Add(this.ssp_Right);
		this.Pnl_Spliter_Hor.Controls.Add(this.ssp_Lefter);
		this.Pnl_Spliter_Hor.Controls.Add(this.ssp_Left);
		this.Pnl_Spliter_Hor.Dock = System.Windows.Forms.DockStyle.Top;
		this.Pnl_Spliter_Hor.Location = new System.Drawing.Point(167, 257);
		this.Pnl_Spliter_Hor.Name = "Pnl_Spliter_Hor";
		this.Pnl_Spliter_Hor.Size = new System.Drawing.Size(615, 7);
		this.Pnl_Spliter_Hor.TabIndex = 18;
		appearance36.BorderColor = System.Drawing.Color.Transparent;
		appearance36.BorderColor3DBase = System.Drawing.Color.Transparent;
		appearance36.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance6.ImageBackground");
		this.Btn_SpltHor.Appearance = appearance36;
		this.Btn_SpltHor.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Borderless;
		this.Btn_SpltHor.Cursor = System.Windows.Forms.Cursors.Hand;
		this.Btn_SpltHor.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Btn_SpltHor.ImageSize = new System.Drawing.Size(7, 57);
		this.Btn_SpltHor.Location = new System.Drawing.Point(284, 0);
		this.Btn_SpltHor.Name = "Btn_SpltHor";
		this.Btn_SpltHor.ShapeImage = (System.Drawing.Image)resources.GetObject("Btn_SpltHor.ShapeImage");
		this.Btn_SpltHor.ShowFocusRect = false;
		this.Btn_SpltHor.ShowOutline = false;
		this.Btn_SpltHor.Size = new System.Drawing.Size(60, 7);
		this.Btn_SpltHor.TabIndex = 8;
		this.Btn_SpltHor.MouseLeave += new System.EventHandler(Btn_SpltHor_MouseLeave);
		this.Btn_SpltHor.Click += new System.EventHandler(Btn_SpltHor_Click);
		this.Btn_SpltHor.MouseEnter += new System.EventHandler(Btn_SpltHor_MouseEnter);
		this.ssp_Righter.Dock = System.Windows.Forms.DockStyle.Right;
		this.ssp_Righter.Location = new System.Drawing.Point(344, 0);
		this.ssp_Righter.Name = "ssp_Righter";
		this.ssp_Righter.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Righter.OcxState");
		this.ssp_Righter.Size = new System.Drawing.Size(268, 7);
		this.ssp_Righter.TabIndex = 7;
		this.ssp_Right.Dock = System.Windows.Forms.DockStyle.Right;
		this.ssp_Right.Location = new System.Drawing.Point(612, 0);
		this.ssp_Right.Name = "ssp_Right";
		this.ssp_Right.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Right.OcxState");
		this.ssp_Right.Size = new System.Drawing.Size(3, 7);
		this.ssp_Right.TabIndex = 6;
		this.ssp_Lefter.Dock = System.Windows.Forms.DockStyle.Left;
		this.ssp_Lefter.Location = new System.Drawing.Point(3, 0);
		this.ssp_Lefter.Name = "ssp_Lefter";
		this.ssp_Lefter.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Lefter.OcxState");
		this.ssp_Lefter.Size = new System.Drawing.Size(281, 7);
		this.ssp_Lefter.TabIndex = 5;
		this.ssp_Left.Dock = System.Windows.Forms.DockStyle.Left;
		this.ssp_Left.Location = new System.Drawing.Point(0, 0);
		this.ssp_Left.Name = "ssp_Left";
		this.ssp_Left.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Left.OcxState");
		this.ssp_Left.Size = new System.Drawing.Size(3, 7);
		this.ssp_Left.TabIndex = 4;
		this.iglst_splt_Btn2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("iglst_splt_Btn2.ImageStream");
		this.iglst_splt_Btn2.TransparentColor = System.Drawing.Color.Transparent;
		this.iglst_splt_Btn2.Images.SetKeyName(0, "");
		this.iglst_splt_Btn2.Images.SetKeyName(1, "");
		this.iglst_splt_Btn2.Images.SetKeyName(2, "");
		this.iglst_splt_Btn2.Images.SetKeyName(3, "");
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		base.ClientSize = new System.Drawing.Size(782, 563);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.Pnl_Spliter_Hor);
		base.Controls.Add(this.PNL_UPPER);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.pnl_spliter);
		base.Controls.Add(this.LeftPanel);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Left);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Right);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Bottom);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Top);
		base.KeyPreview = true;
		base.Name = "FormCompareItm";
		this.Text = "歷史工程單位造價";
		base.Load += new System.EventHandler(FormCompareItm_Load);
		base.Activated += new System.EventHandler(FormCompareItm_Activated);
		base.Resize += new System.EventHandler(FormCompareItm_Resize);
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).EndInit();
		this.LeftPanel.ResumeLayout(false);
		this.LeftPanel.PerformLayout();
		this.pnl_spliter.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ssp_Lower).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Bottom).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Upper).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Top).EndInit();
		this.panel1.ResumeLayout(false);
		this.PNL_UPPER.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Op1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.GridCmp).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dpCmpItem).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dpBase).EndInit();
		this.panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridBudget1).EndInit();
		this.Pnl_Spliter_Hor.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ssp_Righter).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Right).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Lefter).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Left).EndInit();
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

	public FormCompareItm()
	{
		InitializeComponent();
		GridCols = gridBudget1.Cols.Count;
		GridColsSquence = new object[GridCols, 10];
		CellStyle cs = gridBudget1.Styles.Add("img");
		cs.DataType = typeof(Image);
		HideCols(IsHide: true);
		RememberColsProps();
	}

	private void SettingDecimal()
	{
		DataTable DTDecimal = new DataTable();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add(CommonMethods.GetFormTypeTitle(FormType.Budget));
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
			F_MainQty = 3;
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
			gridBudget1.Cols["ProjectCode"].Visible = false;
			gridBudget1.Cols["PccesCode"].Visible = false;
			gridBudget1.Cols["PubCode"].Visible = false;
			gridBudget1.Cols["AnaImg"].Visible = false;
			gridBudget1.Cols["Analysis"].Visible = false;
		}
	}

	private void RememberColsProps()
	{
		for (int i = 0; i < GridCols; i++)
		{
			GridColsSquence[i, 0] = gridBudget1.Cols[i].Name;
			GridColsSquence[i, 1] = gridBudget1.Cols[i].Caption;
			GridColsSquence[i, 2] = gridBudget1.Cols[i].Width;
			if (gridBudget1.Cols[i].Name == "AnaImg")
			{
				GridColsSquence[i, 3] = typeof(Image);
			}
			else
			{
				GridColsSquence[i, 3] = gridBudget1.Cols[i].DataType;
			}
			GridColsSquence[i, 4] = gridBudget1.Cols[i].Visible;
			GridColsSquence[i, 5] = gridBudget1.Cols[i].Format;
			GridColsSquence[i, 6] = gridBudget1.Cols[i].AllowEditing;
			if ((object)gridBudget1.Cols[i].DataType == Type.GetType("System.Decimal"))
			{
				GridColsSquence[i, 5] = "###,###,###,###,###,##0.000";
			}
			GridColsSquence[i, 7] = gridBudget1.Cols[i].TextAlign;
			GridColsSquence[i, 8] = gridBudget1.Cols[i].AllowDragging;
			GridColsSquence[i, 9] = gridBudget1.Cols[i].AllowResizing;
		}
	}

	private void SetGridColumn()
	{
		for (int i = 0; i < GridCols; i++)
		{
			gridBudget1.Cols[i].Name = (string)GridColsSquence[i, 0];
			gridBudget1.Cols[i].Caption = (string)GridColsSquence[i, 1];
			gridBudget1.Cols[i].Width = (int)GridColsSquence[i, 2];
			gridBudget1.Cols[i].DataType = (Type)GridColsSquence[i, 3];
			gridBudget1.Cols[i].Visible = (bool)GridColsSquence[i, 4];
			gridBudget1.Cols[i].Format = (string)GridColsSquence[i, 5];
			gridBudget1.Cols[i].AllowEditing = (bool)GridColsSquence[i, 6];
			gridBudget1.Cols[i].TextAlign = (TextAlignEnum)GridColsSquence[i, 7];
			gridBudget1.Cols[i].AllowDragging = (bool)GridColsSquence[i, 8];
			gridBudget1.Cols[i].AllowResizing = (bool)GridColsSquence[i, 9];
		}
	}

	private void FormCompareItm_Resize(object sender, EventArgs e)
	{
		int TotalH = pnl_spliter.Height;
		int iHeight = (TotalH - 3 - 3 - 57) / 2;
		ssp_Upper.Height = iHeight;
		ssp_Lower.Height = iHeight;
		int TotalW = Pnl_Spliter_Hor.Width;
		int iWidth = (TotalW - 3 - 3 - 57) / 2;
		ssp_Lefter.Width = iWidth;
		ssp_Righter.Width = iWidth;
	}

	private void Btn_Splt_Click(object sender, EventArgs e)
	{
		if (LeftPanel.Width == 0)
		{
			LeftPanel.Width = 160;
			PanelMode = LeftPanelMode.Open;
			Btn_Splt.Appearance.ImageBackground = iglst_splt_Btn.Images[0];
		}
		else
		{
			LeftPanel.Width = 0;
			PanelMode = LeftPanelMode.Close;
			Btn_Splt.Appearance.ImageBackground = iglst_splt_Btn.Images[2];
		}
		FormCompareItm_Resize(this, EventArgs.Empty);
	}

	private void Btn_SpltHor_Click(object sender, EventArgs e)
	{
		if (PNL_UPPER.Height == 0)
		{
			PNL_UPPER.Height = 200;
			MidPanelMode = LeftPanelMode.Open;
			Btn_SpltHor.Appearance.ImageBackground = iglst_splt_Btn2.Images[0];
			ultraToolbarsManager1.Tools["mnuOpenPanel"].SharedProps.Caption = "隱藏比對條件";
		}
		else
		{
			PNL_UPPER.Height = 0;
			MidPanelMode = LeftPanelMode.Close;
			Btn_SpltHor.Appearance.ImageBackground = iglst_splt_Btn2.Images[2];
			ultraToolbarsManager1.Tools["mnuOpenPanel"].SharedProps.Caption = "設定比對條件";
		}
	}

	private void FormCompareItm_Load(object sender, EventArgs e)
	{
		FORM_STATUS = FormStatus.Load;
		base.ParentForm.Text = "PCCES Win 4.3 【歷史工程單位造價】";
		functionButtons1._UserID = F_UserID;
		functionButtons1._UserName = F_UserName;
		functionButtons1._ServerName = F_ServerName;
		functionButtons1._CurrOpenMode = FunctionOpenMode.Common;
		functionButtons1._ActiveFunction = "COMPAREITEM";
		onlineList1._UserID = F_UserID;
		onlineList1._UserName = F_UserName;
		onlineList1._ServerName = F_ServerName;
		onlineList1._FunctionName = F_FunctionName;
		onlineList1._HasRegistered = F_HasRegistered;
		onlineList1.Connect();
		SettingDecimal();
		ControlsClear();
		LoadData();
		BindToDropDown();
		FormCompareItm_Resize(null, null);
		dpCmpItem.SelectedIndex = 0;
		FORM_STATUS = FormStatus.Normal;
	}

	private void ControlsClear()
	{
		dpBase.Items.Clear();
		dpBase.Text = "";
		((ComboBoxTool)ultraToolbarsManager1.Tools["mnuCbo_Show"]).SelectedIndex = 0;
	}

	private void LoadData()
	{
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(Chk_Cost1) 經費審查比對");
		if (F_ActionName == PccesFormAction.None)
		{
			Archnowledge.Pcces.BUDClass.Project ProjCom = new Archnowledge.Pcces.BUDClass.Project(tmp_AL1);
			ProjCom.ps_srckind = "BUD";
			DT_DP = ProjCom.ListItem();
			ProjCom.ps_srckind = "BID";
			DataTable DT_TTMP = ProjCom.ListItem();
			for (int j = 0; j < DT_TTMP.Rows.Count; j++)
			{
				DataRow DR = DT_DP.NewRow();
				for (int i = 0; i < DT_TTMP.Columns.Count; i++)
				{
					if (!(DT_TTMP.Columns[i].ColumnName == "IsType"))
					{
						DR[DT_TTMP.Columns[i].ColumnName] = DT_TTMP.Rows[j][DT_TTMP.Columns[i].ColumnName];
					}
				}
				DT_DP.Rows.Add(DR);
			}
		}
		else
		{
			Archnowledge.Pcces.BUDClass.Project ProjCom = new Archnowledge.Pcces.BUDClass.Project(tmp_AL1);
			ProjCom.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
			DT_DP = ProjCom.ListItem();
		}
	}

	private void BindToDropDown()
	{
		string sProjectCode = "";
		string sProjectNameC = "";
		dpBase.Items.Clear();
		GridCmp.Rows.Count = DT_DP.Rows.Count + 1;
		for (int i = 0; i < DT_DP.Rows.Count; i++)
		{
			sProjectCode = DT_DP.Rows[i]["projectCode"].ToString().Trim();
			sProjectNameC = DT_DP.Rows[i]["projectNameC"].ToString().Trim();
			dpBase.Items.Add(sProjectCode, "(" + sProjectCode + ")" + sProjectNameC);
			GridCmp[i + 1, "Check"] = false;
			GridCmp[i + 1, "ProjectCode"] = sProjectCode;
			GridCmp[i + 1, "ProjectNameC"] = sProjectNameC;
		}
	}

	private void ultraToolbarsManager1_AfterToolCloseup(object sender, ToolDropdownEventArgs e)
	{
		if (e.Tool.Key == "mnuCbo_Show")
		{
			if (F_IsRunCompare)
			{
				BindToGrid();
			}
			else
			{
				MessageBox.Show(this, "請先執行比對，再來作切換", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private void ultraToolbarsManager1_ToolClick(object sender, ToolClickEventArgs e)
	{
		switch (e.Tool.Key)
		{
		case "mnu_Go":
			Do_ToolBarFind();
			break;
		case "mnuOpenPanel":
			Do_OpenPanel();
			break;
		case "mnuExport":
			Do_Export();
			break;
		}
	}

	private void Do_OpenPanel()
	{
		Btn_SpltHor_Click(this, EventArgs.Empty);
		dpBase.Focus();
	}

	private void Do_Export()
	{
		string sFilter = "Microsoft Excel 97/2000 files (*.xls)|*.xls";
		saveFileDialog1.Filter = sFilter;
		saveFileDialog1.RestoreDirectory = true;
		saveFileDialog1.FileName = "歷史單位造價";
		if (saveFileDialog1.ShowDialog() == DialogResult.OK)
		{
			gridBudget1._ExcelFileName = saveFileDialog1.FileName;
			gridBudget1._ExcelSheeName = "歷史單位造價";
			gridBudget1._IsOpenExcelAfterExport = true;
			gridBudget1.ExecuteExport(c1GridExportType.Excel);
		}
	}

	private void Do_ToolBarFind()
	{
		if (gridBudget1.Rows.Count <= 1)
		{
			return;
		}
		int iStart = gridBudget1.Row + 1;
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
			iStart = gridBudget1.Row + 1;
		}
		if (sSearchText.Trim() == "")
		{
			return;
		}
		for (int i = iStart; i < gridBudget1.Rows.Count; i++)
		{
			for (int j = 1; j < gridBudget1.Cols.Count; j++)
			{
				if (gridBudget1[i, j] == null || gridBudget1[i, j].ToString().IndexOf(sSearchText) <= -1)
				{
					continue;
				}
				gridBudget1.Row = i;
				gridBudget1.Select();
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

	private void ultraToolbarsManager1_ToolKeyPress(object sender, ToolKeyPressEventArgs e)
	{
		if (e.KeyChar == '\r' && e.Tool.Key == "mnu_Cbo1")
		{
			Do_ToolBarFind();
		}
	}

	private void BtnExecute_Click(object sender, EventArgs e)
	{
		Cursor = Cursors.WaitCursor;
		if (Do_Compare())
		{
			BindToGrid();
			ProcessCols();
			F_IsRunCompare = true;
		}
		Cursor = Cursors.Default;
	}

	private bool Do_Compare()
	{
		bool RetV = true;
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Clear();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(Chk_Cost1) 歷史造價比對");
		if (dpBase.Value == null)
		{
			MessageBox.Show(this, "請先挑選基準標案", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
		string ls_ValS = dpBase.Value.ToString().Trim();
		iidx = -1;
		ls_Val[0] = (ls_Val[1] = (ls_Val[2] = (ls_Val[3] = (ls_Val[4] = (ls_Val[5] = (ls_Val[6] = (ls_Val[7] = (ls_Val[8] = (ls_Val[9] = "")))))))));
		for (int i = 1; i < GridCmp.Rows.Count; i++)
		{
			if (iidx >= 9)
			{
				break;
			}
			if ((bool)GridCmp[i, "Check"])
			{
				iidx++;
				ls_Val[iidx] = GridCmp[i, "ProjectCode"].ToString();
			}
		}
		if (iidx == -1)
		{
			MessageBox.Show(this, "請勾選比對案", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("WIN FORM");
		Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
		PROJ.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		for (int i = 0; i < ls_Val.Length; i++)
		{
			if (!(ls_Val[i].Trim() != ""))
			{
				continue;
			}
			DT1 = PROJ.ListItem("", ls_Val[i]);
			ref decimal reference = ref Scope[i];
			reference = PubTools.Str2Decimal(DT1.Rows[0]["projectScope"]);
			if (Scope[i] == 0m)
			{
				FormCompareItm_Scope FM_SCOPE = new FormCompareItm_Scope();
				FM_SCOPE._UserID = F_UserID;
				FM_SCOPE._ProjectCode = ls_Val[i];
				FM_SCOPE._ProjectNameC = DT1.Rows[0]["projectNameC"].ToString();
				FM_SCOPE._ActionName = F_ActionName;
				FM_SCOPE.Owner = this;
				if (FM_SCOPE.ShowDialog() == DialogResult.OK)
				{
					ref decimal reference2 = ref Scope[i];
					reference2 = F_dec_TempValue;
				}
				FM_SCOPE.Close();
				FM_SCOPE.Dispose();
				FM_SCOPE = null;
			}
		}
		DT1 = PROJ.ListItem("", dpBase.Value.ToString().Trim());
		ScopeS = PubTools.Str2Decimal(DT1.Rows[0]["projectScope"]);
		if (ScopeS == 0m)
		{
			FormCompareItm_Scope FM_SCOPE = new FormCompareItm_Scope();
			FM_SCOPE._UserID = F_UserID;
			FM_SCOPE._ProjectCode = dpBase.Value.ToString().Trim();
			FM_SCOPE._ProjectNameC = DT1.Rows[0]["projectNameC"].ToString();
			FM_SCOPE._ActionName = F_ActionName;
			FM_SCOPE.Owner = this;
			if (FM_SCOPE.ShowDialog() == DialogResult.OK)
			{
				ScopeS = F_dec_TempValue;
			}
			FM_SCOPE.Close();
			FM_SCOPE.Dispose();
			FM_SCOPE = null;
		}
		HisPrice hisCom = new HisPrice(tmp_AL1);
		string sSrcKind = CommonMethods.GetActionNameString(F_ActionName);
		DT1 = hisCom.chkhisData(ls_ValS, ls_Val[0], ls_Val[1], ls_Val[2], ls_Val[3], sSrcKind, ls_Val[4], ls_Val[5], ls_Val[6], ls_Val[7], ls_Val[8], ls_Val[9]);
		return RetV;
	}

	private void BindToGrid()
	{
		decimal dec_Summary = 0m;
		ultraToolbarsManager1.BeginUpdate();
		ultraToolbarsManager1.Enabled = false;
		gridBudget1.Redraw = false;
		RememberColsProps();
		CellStyle CS1 = gridBudget1.Styles.Add("AnalysisColor");
		CellStyle CS2 = gridBudget1.Styles.Add("MainColor");
		CellStyle CS9 = gridBudget1.Styles.Add("IsSharedColor");
		CS1.ForeColor = Color.Red;
		CS2.ForeColor = Color.Blue;
		CS9.ForeColor = Color.Plum;
		gridBudget1.Clear(ClearFlags.All);
		gridBudget1.Select(0, 0);
		DataView DV1 = DT1.DefaultView;
		DV1.RowFilter = GetFilterString();
		DV1.Sort = "PrintNo";
		int iRows = DV1.Count + 1;
		gridBudget1.Rows.Count = iRows;
		gridBudget1.Select(0, 0);
		SetGridColumn();
		ultraStatusBar1.Panels[0].Text = "資料筆數：" + DV1.Count;
		for (int i = 0; i < DV1.Count; i++)
		{
			if (DV1[i]["analysis"].ToString().Trim() == "1")
			{
				gridBudget1[i + 1, "Analysis"] = true;
				gridBudget1.Rows[i + 1].Style = gridBudget1.Styles["AnalysisColor"];
				CellRange rg = gridBudget1.GetCellRange(i + 1, gridBudget1.Cols["AnaImg"].SafeIndex);
				rg.Style = gridBudget1.Styles["img"];
				rg.Image = imageList2.Images[0];
			}
			else
			{
				gridBudget1[i + 1, "Analysis"] = false;
			}
			gridBudget1[i + 1, "ItemNo"] = DV1[i]["itemNo"].ToString();
			gridBudget1[i + 1, "ProjectCode"] = DV1[i]["projectCode"].ToString();
			gridBudget1[i + 1, "PubCode"] = DV1[i]["PubCode"].ToString();
			gridBudget1[i + 1, "PccesCode"] = DV1[i]["pccesCode"].ToString();
			gridBudget1[i + 1, "CName"] = DV1[i]["cName"].ToString();
			gridBudget1[i + 1, "UnitName"] = DV1[i]["unitName"].ToString();
			if (dpCmpItem.SelectedIndex == 0)
			{
				gridBudget1[i + 1, "ChkCostS"] = ((ScopeS > 0m) ? (PubTools.Str2Decimal(DV1[i]["chkQtyS"]) / ScopeS) : 0m);
				gridBudget1[i + 1, "ChkCost1"] = ((Scope[0] > 0m) ? (PubTools.Str2Decimal(DV1[i]["chkQty1"]) / Scope[0]) : 0m);
				gridBudget1[i + 1, "ChkCost2"] = ((Scope[1] > 0m) ? (PubTools.Str2Decimal(DV1[i]["chkQty2"]) / Scope[1]) : 0m);
				gridBudget1[i + 1, "ChkCost3"] = ((Scope[2] > 0m) ? (PubTools.Str2Decimal(DV1[i]["chkQty3"]) / Scope[2]) : 0m);
				gridBudget1[i + 1, "ChkCost4"] = ((Scope[3] > 0m) ? (PubTools.Str2Decimal(DV1[i]["chkQty4"]) / Scope[3]) : 0m);
				gridBudget1[i + 1, "ChkCost5"] = ((Scope[4] > 0m) ? (PubTools.Str2Decimal(DV1[i]["chkQty5"]) / Scope[4]) : 0m);
				gridBudget1[i + 1, "ChkCost6"] = ((Scope[5] > 0m) ? (PubTools.Str2Decimal(DV1[i]["chkQty6"]) / Scope[5]) : 0m);
				gridBudget1[i + 1, "ChkCost7"] = ((Scope[6] > 0m) ? (PubTools.Str2Decimal(DV1[i]["chkQty7"]) / Scope[6]) : 0m);
				gridBudget1[i + 1, "ChkCost8"] = ((Scope[7] > 0m) ? (PubTools.Str2Decimal(DV1[i]["chkQty8"]) / Scope[7]) : 0m);
				gridBudget1[i + 1, "ChkCost9"] = ((Scope[8] > 0m) ? (PubTools.Str2Decimal(DV1[i]["chkQty9"]) / Scope[8]) : 0m);
				gridBudget1[i + 1, "ChkCost0"] = ((Scope[9] > 0m) ? (PubTools.Str2Decimal(DV1[i]["chkQty0"]) / Scope[9]) : 0m);
				dec_Summary = (decimal)gridBudget1[i + 1, "ChkCostS"] + (decimal)gridBudget1[i + 1, "ChkCost1"] + (decimal)gridBudget1[i + 1, "ChkCost2"] + (decimal)gridBudget1[i + 1, "ChkCost3"] + (decimal)gridBudget1[i + 1, "ChkCost4"] + (decimal)gridBudget1[i + 1, "ChkCost5"] + (decimal)gridBudget1[i + 1, "ChkCost6"] + (decimal)gridBudget1[i + 1, "ChkCost7"] + (decimal)gridBudget1[i + 1, "ChkCost8"] + (decimal)gridBudget1[i + 1, "ChkCost9"] + (decimal)gridBudget1[i + 1, "ChkCost0"];
				gridBudget1[i + 1, "AvgCost"] = dec_Summary / (decimal)(iidx + 2);
			}
			if (dpCmpItem.SelectedIndex == 2)
			{
				gridBudget1[i + 1, "ChkCostS"] = ((ScopeS > 0m) ? PubTools.Str2Decimal(DV1[i]["chkCostS"]) : 0m);
				gridBudget1[i + 1, "ChkCost1"] = ((Scope[0] > 0m) ? PubTools.Str2Decimal(DV1[i]["chkCost1"]) : 0m);
				gridBudget1[i + 1, "ChkCost2"] = ((Scope[1] > 0m) ? PubTools.Str2Decimal(DV1[i]["chkCost2"]) : 0m);
				gridBudget1[i + 1, "ChkCost3"] = ((Scope[2] > 0m) ? PubTools.Str2Decimal(DV1[i]["chkCost3"]) : 0m);
				gridBudget1[i + 1, "ChkCost4"] = ((Scope[3] > 0m) ? PubTools.Str2Decimal(DV1[i]["chkCost4"]) : 0m);
				gridBudget1[i + 1, "ChkCost5"] = ((Scope[4] > 0m) ? PubTools.Str2Decimal(DV1[i]["chkCost5"]) : 0m);
				gridBudget1[i + 1, "ChkCost6"] = ((Scope[5] > 0m) ? PubTools.Str2Decimal(DV1[i]["chkCost6"]) : 0m);
				gridBudget1[i + 1, "ChkCost7"] = ((Scope[6] > 0m) ? PubTools.Str2Decimal(DV1[i]["chkCost7"]) : 0m);
				gridBudget1[i + 1, "ChkCost8"] = ((Scope[7] > 0m) ? PubTools.Str2Decimal(DV1[i]["chkCost8"]) : 0m);
				gridBudget1[i + 1, "ChkCost9"] = ((Scope[8] > 0m) ? PubTools.Str2Decimal(DV1[i]["chkCost9"]) : 0m);
				gridBudget1[i + 1, "ChkCost0"] = ((Scope[9] > 0m) ? PubTools.Str2Decimal(DV1[i]["chkCost0"]) : 0m);
				dec_Summary = (decimal)gridBudget1[i + 1, "ChkCostS"] + (decimal)gridBudget1[i + 1, "ChkCost1"] + (decimal)gridBudget1[i + 1, "ChkCost2"] + (decimal)gridBudget1[i + 1, "ChkCost3"] + (decimal)gridBudget1[i + 1, "ChkCost4"] + (decimal)gridBudget1[i + 1, "ChkCost5"] + (decimal)gridBudget1[i + 1, "ChkCost6"] + (decimal)gridBudget1[i + 1, "ChkCost7"] + (decimal)gridBudget1[i + 1, "ChkCost8"] + (decimal)gridBudget1[i + 1, "ChkCost9"] + (decimal)gridBudget1[i + 1, "ChkCost0"];
				gridBudget1[i + 1, "AvgCost"] = dec_Summary / (decimal)(iidx + 2);
			}
			if (dpCmpItem.SelectedIndex == 1)
			{
				gridBudget1[i + 1, "ChkCostS"] = ((ScopeS > 0m) ? (PubTools.Str2Decimal(DV1[i]["chkAmtS"]) / ScopeS) : 0m);
				gridBudget1[i + 1, "ChkCost1"] = ((Scope[0] > 0m) ? (PubTools.Str2Decimal(DV1[i]["chkAmt1"]) / Scope[0]) : 0m);
				gridBudget1[i + 1, "ChkCost2"] = ((Scope[1] > 0m) ? (PubTools.Str2Decimal(DV1[i]["chkAmt2"]) / Scope[1]) : 0m);
				gridBudget1[i + 1, "ChkCost3"] = ((Scope[2] > 0m) ? (PubTools.Str2Decimal(DV1[i]["chkAmt3"]) / Scope[2]) : 0m);
				gridBudget1[i + 1, "ChkCost4"] = ((Scope[3] > 0m) ? (PubTools.Str2Decimal(DV1[i]["chkAmt4"]) / Scope[3]) : 0m);
				gridBudget1[i + 1, "ChkCost5"] = ((Scope[4] > 0m) ? (PubTools.Str2Decimal(DV1[i]["chkAmt5"]) / Scope[4]) : 0m);
				gridBudget1[i + 1, "ChkCost6"] = ((Scope[5] > 0m) ? (PubTools.Str2Decimal(DV1[i]["chkAmt6"]) / Scope[5]) : 0m);
				gridBudget1[i + 1, "ChkCost7"] = ((Scope[6] > 0m) ? (PubTools.Str2Decimal(DV1[i]["chkAmt7"]) / Scope[6]) : 0m);
				gridBudget1[i + 1, "ChkCost8"] = ((Scope[7] > 0m) ? (PubTools.Str2Decimal(DV1[i]["chkAmt8"]) / Scope[7]) : 0m);
				gridBudget1[i + 1, "ChkCost9"] = ((Scope[8] > 0m) ? (PubTools.Str2Decimal(DV1[i]["chkAmt9"]) / Scope[8]) : 0m);
				gridBudget1[i + 1, "ChkCost0"] = ((Scope[9] > 0m) ? (PubTools.Str2Decimal(DV1[i]["chkAmt0"]) / Scope[9]) : 0m);
				dec_Summary = (decimal)gridBudget1[i + 1, "ChkCostS"] + (decimal)gridBudget1[i + 1, "ChkCost1"] + (decimal)gridBudget1[i + 1, "ChkCost2"] + (decimal)gridBudget1[i + 1, "ChkCost3"] + (decimal)gridBudget1[i + 1, "ChkCost4"] + (decimal)gridBudget1[i + 1, "ChkCost5"] + (decimal)gridBudget1[i + 1, "ChkCost6"] + (decimal)gridBudget1[i + 1, "ChkCost7"] + (decimal)gridBudget1[i + 1, "ChkCost8"] + (decimal)gridBudget1[i + 1, "ChkCost9"] + (decimal)gridBudget1[i + 1, "ChkCost0"];
				gridBudget1[i + 1, "AvgCost"] = dec_Summary / (decimal)(iidx + 2);
			}
			gridBudget1.Rows[i + 1].IsNode = true;
			gridBudget1.Rows[i + 1].Node.Level = DV1[i]["printNo"].ToString().Trim().Length / 4;
			if (DV1[i]["printNo"].ToString().Trim() == "".PadLeft(32, '9'))
			{
				gridBudget1.Rows[i + 1].Node.Level = 1;
			}
			if (DV1[i]["Kind"].ToString().Trim() == "B" || DV1[i]["Kind"].ToString().Trim() == "F" || DV1[i]["Kind"].ToString().Trim() == "S" || DV1[i]["Kind"].ToString().Trim() == "Z" || DV1[i]["Kind"].ToString().Trim() == "U")
			{
				gridBudget1.Rows[i + 1].Style = gridBudget1.Styles["MainColor"];
			}
		}
		gridBudget1.Redraw = true;
		gridBudget1.Invalidate();
		ultraToolbarsManager1.Enabled = true;
		ultraToolbarsManager1.EndUpdate();
	}

	private void ProcessCols()
	{
		gridBudget1.Rows[0].Height = 40;
		gridBudget1.Cols["ChkCostS"].Caption = "基準案\n" + dpBase.Value.ToString();
		gridBudget1.Cols["ChkCost1"].Caption = "比對[1]\n" + ls_Val[0];
		gridBudget1.Cols["ChkCost1"].Visible = ((ls_Val[0].Trim() != "") ? true : false);
		gridBudget1.Cols["ChkCost2"].Caption = "比對[2]\n" + ls_Val[1];
		gridBudget1.Cols["ChkCost2"].Visible = ((ls_Val[1].Trim() != "") ? true : false);
		gridBudget1.Cols["ChkCost3"].Caption = "比對[3]\n" + ls_Val[2];
		gridBudget1.Cols["ChkCost3"].Visible = ((ls_Val[2].Trim() != "") ? true : false);
		gridBudget1.Cols["ChkCost4"].Caption = "比對[4]\n" + ls_Val[3];
		gridBudget1.Cols["ChkCost4"].Visible = ((ls_Val[3].Trim() != "") ? true : false);
		gridBudget1.Cols["ChkCost5"].Caption = "比對[5]\n" + ls_Val[4];
		gridBudget1.Cols["ChkCost5"].Visible = ((ls_Val[4].Trim() != "") ? true : false);
		gridBudget1.Cols["ChkCost6"].Caption = "比對[6]\n" + ls_Val[5];
		gridBudget1.Cols["ChkCost6"].Visible = ((ls_Val[5].Trim() != "") ? true : false);
		gridBudget1.Cols["ChkCost7"].Caption = "比對[7]\n" + ls_Val[6];
		gridBudget1.Cols["ChkCost7"].Visible = ((ls_Val[6].Trim() != "") ? true : false);
		gridBudget1.Cols["ChkCost8"].Caption = "比對[8]\n" + ls_Val[7];
		gridBudget1.Cols["ChkCost8"].Visible = ((ls_Val[7].Trim() != "") ? true : false);
		gridBudget1.Cols["ChkCost9"].Caption = "比對[9]\n" + ls_Val[8];
		gridBudget1.Cols["ChkCost9"].Visible = ((ls_Val[8].Trim() != "") ? true : false);
		gridBudget1.Cols["ChkCost0"].Caption = "比對[10]\n" + ls_Val[9];
		gridBudget1.Cols["ChkCost0"].Visible = ((ls_Val[9].Trim() != "") ? true : false);
		gridBudget1.Cols.Frozen = 8;
	}

	private string GetFilterString()
	{
		string tmp = "";
		switch (((ComboBoxTool)ultraToolbarsManager1.Tools["mnuCbo_Show"]).SelectedIndex)
		{
		case 0:
			tmp = "";
			break;
		case 1:
			switch (dpCmpItem.SelectedIndex)
			{
			case 0:
				if (ls_Val[0].Trim() != "")
				{
					tmp += " chkqty1 is null or";
				}
				if (ls_Val[1].Trim() != "")
				{
					tmp += " chkqty2 is null or";
				}
				if (ls_Val[2].Trim() != "")
				{
					tmp += " chkqty3 is null or";
				}
				if (ls_Val[3].Trim() != "")
				{
					tmp += " chkqty4 is null or";
				}
				if (ls_Val[4].Trim() != "")
				{
					tmp += " chkqty5 is null or";
				}
				if (ls_Val[5].Trim() != "")
				{
					tmp += " chkqty6 is null or";
				}
				if (ls_Val[6].Trim() != "")
				{
					tmp += " chkqty7 is null or";
				}
				if (ls_Val[7].Trim() != "")
				{
					tmp += " chkqty8 is null or";
				}
				if (ls_Val[8].Trim() != "")
				{
					tmp += " chkqty9 is null or";
				}
				if (ls_Val[9].Trim() != "")
				{
					tmp += " chkqty0 is null or";
				}
				if (tmp.Trim().Length > 0)
				{
					tmp = tmp.Substring(0, tmp.Length - 2);
				}
				break;
			case 1:
				if (ls_Val[0].Trim() != "")
				{
					tmp += " chkcost1 is null or";
				}
				if (ls_Val[1].Trim() != "")
				{
					tmp += " chkcost2 is null or";
				}
				if (ls_Val[2].Trim() != "")
				{
					tmp += " chkcost3 is null or";
				}
				if (ls_Val[3].Trim() != "")
				{
					tmp += " chkcost4 is null or";
				}
				if (ls_Val[4].Trim() != "")
				{
					tmp += " chkcost5 is null or";
				}
				if (ls_Val[5].Trim() != "")
				{
					tmp += " chkcost6 is null or";
				}
				if (ls_Val[6].Trim() != "")
				{
					tmp += " chkcost7 is null or";
				}
				if (ls_Val[7].Trim() != "")
				{
					tmp += " chkcost8 is null or";
				}
				if (ls_Val[8].Trim() != "")
				{
					tmp += " chkcost9 is null or";
				}
				if (ls_Val[9].Trim() != "")
				{
					tmp += " chkcost0 is null or";
				}
				if (tmp.Trim().Length > 0)
				{
					tmp = tmp.Substring(0, tmp.Length - 2);
				}
				break;
			case 2:
				if (ls_Val[0].Trim() != "")
				{
					tmp += " chkamt1 is null or";
				}
				if (ls_Val[1].Trim() != "")
				{
					tmp += " chkamt2 is null or";
				}
				if (ls_Val[2].Trim() != "")
				{
					tmp += " chkamt3 is null or";
				}
				if (ls_Val[3].Trim() != "")
				{
					tmp += " chkamt4 is null or";
				}
				if (ls_Val[4].Trim() != "")
				{
					tmp += " chkamt5 is null or";
				}
				if (ls_Val[5].Trim() != "")
				{
					tmp += " chkamt6 is null or";
				}
				if (ls_Val[6].Trim() != "")
				{
					tmp += " chkamt7 is null or";
				}
				if (ls_Val[7].Trim() != "")
				{
					tmp += " chkamt8 is null or";
				}
				if (ls_Val[8].Trim() != "")
				{
					tmp += " chkamt9 is null or";
				}
				if (ls_Val[9].Trim() != "")
				{
					tmp += " chkamt0 is null or";
				}
				if (tmp.Trim().Length > 0)
				{
					tmp = tmp.Substring(0, tmp.Length - 2);
				}
				break;
			}
			break;
		case 2:
			switch (dpCmpItem.SelectedIndex)
			{
			case 0:
				if (ls_Val[0].Trim() != "")
				{
					tmp += " chkqty1 is not null or";
				}
				if (ls_Val[1].Trim() != "")
				{
					tmp += " chkqty2 is not null or";
				}
				if (ls_Val[2].Trim() != "")
				{
					tmp += " chkqty3 is not null or";
				}
				if (ls_Val[3].Trim() != "")
				{
					tmp += " chkqty4 is not null or";
				}
				if (ls_Val[4].Trim() != "")
				{
					tmp += " chkqty5 is not null or";
				}
				if (ls_Val[5].Trim() != "")
				{
					tmp += " chkqty6 is not null or";
				}
				if (ls_Val[6].Trim() != "")
				{
					tmp += " chkqty7 is not null or";
				}
				if (ls_Val[7].Trim() != "")
				{
					tmp += " chkqty8 is not null or";
				}
				if (ls_Val[8].Trim() != "")
				{
					tmp += " chkqty9 is not null or";
				}
				if (ls_Val[9].Trim() != "")
				{
					tmp += " chkqty0 is not null or";
				}
				if (tmp.Trim().Length > 0)
				{
					tmp = tmp.Substring(0, tmp.Length - 2);
				}
				break;
			case 1:
				if (ls_Val[0].Trim() != "")
				{
					tmp += " chkcost1 is not null or";
				}
				if (ls_Val[1].Trim() != "")
				{
					tmp += " chkcost2 is not null or";
				}
				if (ls_Val[2].Trim() != "")
				{
					tmp += " chkcost3 is not null or";
				}
				if (ls_Val[3].Trim() != "")
				{
					tmp += " chkcost4 is not null or";
				}
				if (ls_Val[4].Trim() != "")
				{
					tmp += " chkcost5 is not null or";
				}
				if (ls_Val[5].Trim() != "")
				{
					tmp += " chkcost6 is not null or";
				}
				if (ls_Val[6].Trim() != "")
				{
					tmp += " chkcost7 is not null or";
				}
				if (ls_Val[7].Trim() != "")
				{
					tmp += " chkcost8 is not null or";
				}
				if (ls_Val[8].Trim() != "")
				{
					tmp += " chkcost9 is not null or";
				}
				if (ls_Val[9].Trim() != "")
				{
					tmp += " chkcost0 is not null or";
				}
				if (tmp.Trim().Length > 0)
				{
					tmp = tmp.Substring(0, tmp.Length - 2);
				}
				break;
			case 2:
				if (ls_Val[0].Trim() != "")
				{
					tmp += " chkamt1 is not null or";
				}
				if (ls_Val[1].Trim() != "")
				{
					tmp += " chkamt2 is not null or";
				}
				if (ls_Val[2].Trim() != "")
				{
					tmp += " chkamt3 is not null or";
				}
				if (ls_Val[3].Trim() != "")
				{
					tmp += " chkamt4 is not null or";
				}
				if (ls_Val[4].Trim() != "")
				{
					tmp += " chkamt5 is not null or";
				}
				if (ls_Val[5].Trim() != "")
				{
					tmp += " chkamt6 is not null or";
				}
				if (ls_Val[6].Trim() != "")
				{
					tmp += " chkamt7 is not null or";
				}
				if (ls_Val[7].Trim() != "")
				{
					tmp += " chkamt8 is not null or";
				}
				if (ls_Val[8].Trim() != "")
				{
					tmp += " chkamt9 is not null or";
				}
				if (ls_Val[9].Trim() != "")
				{
					tmp += " chkamt0 is not null or";
				}
				if (tmp.Trim().Length > 0)
				{
					tmp = tmp.Substring(0, tmp.Length - 2);
				}
				break;
			}
			break;
		}
		return tmp;
	}

	private void Btn_Splt_MouseEnter(object sender, EventArgs e)
	{
		if (PanelMode == LeftPanelMode.Open)
		{
			Btn_Splt.Appearance.ImageBackground = iglst_splt_Btn.Images[1];
		}
		else
		{
			Btn_Splt.Appearance.ImageBackground = iglst_splt_Btn.Images[3];
		}
	}

	private void Btn_Splt_MouseLeave(object sender, EventArgs e)
	{
		if (PanelMode == LeftPanelMode.Open)
		{
			Btn_Splt.Appearance.ImageBackground = iglst_splt_Btn.Images[0];
		}
		else
		{
			Btn_Splt.Appearance.ImageBackground = iglst_splt_Btn.Images[2];
		}
	}

	private void Btn_SpltHor_MouseEnter(object sender, EventArgs e)
	{
		if (MidPanelMode == LeftPanelMode.Open)
		{
			Btn_SpltHor.Appearance.ImageBackground = iglst_splt_Btn2.Images[1];
		}
		else
		{
			Btn_SpltHor.Appearance.ImageBackground = iglst_splt_Btn2.Images[3];
		}
	}

	private void Btn_SpltHor_MouseLeave(object sender, EventArgs e)
	{
		if (MidPanelMode == LeftPanelMode.Open)
		{
			Btn_SpltHor.Appearance.ImageBackground = iglst_splt_Btn2.Images[0];
		}
		else
		{
			Btn_SpltHor.Appearance.ImageBackground = iglst_splt_Btn2.Images[2];
		}
	}

	private void FormCompareItm_Activated(object sender, EventArgs e)
	{
		base.ParentForm.Text = "PCCES Win 4.3 【歷史工程單位造價】";
	}

	private void ultraToolbarsManager1_BeforeToolbarListDropdown(object sender, BeforeToolbarListDropdownEventArgs e)
	{
		e.Cancel = true;
	}

	private void dpCmpItem_ValueChanged(object sender, EventArgs e)
	{
		if (FORM_STATUS == FormStatus.Normal)
		{
			BtnExecute_Click(sender, e);
		}
	}

	private void Op1_ValueChanged(object sender, EventArgs e)
	{
		if (Op1.CheckedIndex == 0)
		{
			F_ActionName = PccesFormAction.BUD;
		}
		else if (Op1.CheckedIndex == 1)
		{
			F_ActionName = PccesFormAction.BID;
		}
		else
		{
			F_ActionName = PccesFormAction.None;
		}
		ControlsClear();
		LoadData();
		BindToDropDown();
		gridBudget1.Rows.Count = 1;
	}

	private void gridBudget1_Resize(object sender, EventArgs e)
	{
		FormCompareItm_Resize(sender, e);
	}
}
