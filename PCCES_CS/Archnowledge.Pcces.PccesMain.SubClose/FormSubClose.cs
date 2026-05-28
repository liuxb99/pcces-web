using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.CTRClass;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.PccesMain.About;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.PccesMain.Budget;
using Archnowledge.Pcces.PccesMain.Invoice;
using Archnowledge.Pcces.PccesMain.Report;
using Archnowledge.Pcces.STDClass;
using AxThreed;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinStatusBar;
using Infragistics.Win.UltraWinToolbars;

namespace Archnowledge.Pcces.PccesMain.SubClose;

public class FormSubClose : Form
{
	private UltraToolbarsManager ultraToolbarsManager1;

	private IContainer components;

	private ImageList iglst_splt_Btn;

	private ImageList _imageList2;

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

	private Panel panel2;

	private UltraLabel lblProjectData;

	private UltraLabel ultraLabel10;

	private UltraButton BtnSwitchProject;

	private UltraLabel ultraLabel1;

	private GridBudget gridBudget1;

	private Panel panel7;

	private UltraLabel lblTotal;

	private UltraLabel ultraLabel8;

	private AxSSPanel axSSPanel2;

	private UltraStatusBar ultraStatusBar1;

	private string ls_Queue = "9999";

	private string ls_prjcode;

	private string ls_subproj;

	private string F_ProjectCode;

	private string F_ProjectNameC;

	private string F_SubProjetCode = "";

	private bool F_HasRegistered;

	private PccesFormAction F_ActionName = PccesFormAction.SubClose;

	private bool F_IsLock = false;

	private int F_MainQty = 0;

	private int F_MainCst = 0;

	private int F_MainAmt = 0;

	private int F_AnaQty = 0;

	private int F_AnaCst = 0;

	private int F_AnaAmt = 0;

	private string F_UserID;

	private string F_UserName = "";

	private string F_FunctionName = "SUBCLOSE";

	private string F_ServerName = "localhost";

	private int GridCols = 15;

	private object[,] GridColsSquence;

	private DataTable DT1 = new DataTable();

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

	public string _ProjectNameC
	{
		get
		{
			return F_ProjectNameC;
		}
		set
		{
			F_ProjectNameC = value;
		}
	}

	public string _SubProjetCode
	{
		get
		{
			return F_SubProjetCode;
		}
		set
		{
			F_SubProjetCode = value;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.SubClose.FormSubClose));
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.OptionSet optionSet1 = new Infragistics.Win.UltraWinToolbars.OptionSet("switch");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Tool1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuPrint");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuFile_Digital");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuApprove");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuUndoApprove");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool5 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuReGen");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool1 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuCalcu");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool6 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuIssueList");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool7 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSubCloseInfo");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool1 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnuLevel");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool1 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_1", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool2 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_2", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool3 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_3", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool4 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_4", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool5 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_5", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool6 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_6", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool7 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_7", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool8 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_8", "switch");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar2 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Menu1");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool2 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuFile_CNT");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool3 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuEdit_CNT");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool4 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuView_CNT");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool5 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuHelp");
		Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool6 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuFile_CNT");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool8 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSwitchProj");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool9 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuFile_Digital");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool10 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuPrint");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool11 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuClose");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool7 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuEdit_CNT");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool12 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuApprove");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool13 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuUndoApprove");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool8 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuCalcu");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool14 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuReGen");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool9 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuView_CNT");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool15 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuIssueList");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool16 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSubCloseInfo");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool10 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuTool_CNT");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool17 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSwitchProj");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool18 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuPrint");
		Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool19 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuClose");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool20 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuApprove");
		Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool11 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuCalcu");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool21 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCalcuInv");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool22 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCalcuCnt");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool23 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCalcuInput");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool24 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCalcuInv");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool25 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCalcuCnt");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool26 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuReGen");
		Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool27 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuUndoApprove");
		Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool28 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuIssueList");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool29 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSubCloseInfo");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool12 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuHelp");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool30 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuAbout");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool31 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuUpdateList");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool32 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuAbout");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool33 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCalcuInput");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool34 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuUpdateList");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool35 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuFile_Digital");
		Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool2 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnuLevel");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool9 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_1", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool10 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_2", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool11 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_3", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool12 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_4", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool13 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_5", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool14 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_6", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool15 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_7", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool16 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_8", "switch");
		Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
		this.iglst_splt_Btn = new System.Windows.Forms.ImageList(this.components);
		this._imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.ultraToolbarsManager1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
		this.gridBudget1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
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
		this.panel7 = new System.Windows.Forms.Panel();
		this.lblTotal = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel8 = new Infragistics.Win.Misc.UltraLabel();
		this.axSSPanel2 = new AxThreed.AxSSPanel();
		this.ultraStatusBar1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.panel2 = new System.Windows.Forms.Panel();
		this.lblProjectData = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel10 = new Infragistics.Win.Misc.UltraLabel();
		this.BtnSwitchProject = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridBudget1).BeginInit();
		this.LeftPanel.SuspendLayout();
		this.pnl_spliter.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ssp_Lower).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Bottom).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Upper).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Top).BeginInit();
		this.panel1.SuspendLayout();
		this.panel7.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.axSSPanel2).BeginInit();
		this.panel2.SuspendLayout();
		base.SuspendLayout();
		this.iglst_splt_Btn.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("iglst_splt_Btn.ImageStream");
		this.iglst_splt_Btn.TransparentColor = System.Drawing.Color.Transparent;
		this.iglst_splt_Btn.Images.SetKeyName(0, "");
		this.iglst_splt_Btn.Images.SetKeyName(1, "");
		this.iglst_splt_Btn.Images.SetKeyName(2, "");
		this.iglst_splt_Btn.Images.SetKeyName(3, "");
		this._imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("_imageList2.ImageStream");
		this._imageList2.TransparentColor = System.Drawing.Color.White;
		this._imageList2.Images.SetKeyName(0, "");
		this._imageList2.Images.SetKeyName(1, "");
		appearance1.FontData.Name = "Arial";
		appearance1.FontData.SizeInPoints = 9f;
		this.ultraToolbarsManager1.Appearance = appearance1;
		appearance2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance2.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.DockAreaAppearance = appearance2;
		this.ultraToolbarsManager1.DockWithinContainer = this;
		this.ultraToolbarsManager1.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraToolbarsManager1.LockToolbars = true;
		appearance12.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance12.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance12.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.MenuSettings.HotTrackAppearance = appearance12;
		appearance13.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance13.BackColor2 = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraToolbarsManager1.MenuSettings.IconAreaAppearance = appearance13;
		appearance14.BackColor = System.Drawing.Color.White;
		appearance14.BackColor2 = System.Drawing.Color.White;
		this.ultraToolbarsManager1.MenuSettings.ToolAppearance = appearance14;
		optionSet1.AllowAllUp = false;
		this.ultraToolbarsManager1.OptionSets.Add(optionSet1);
		this.ultraToolbarsManager1.ShowFullMenusDelay = 500;
		this.ultraToolbarsManager1.ShowQuickCustomizeButton = false;
		this.ultraToolbarsManager1.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2003;
		ultraToolbar1.DockedColumn = 0;
		ultraToolbar1.DockedRow = 1;
		ultraToolbar1.Settings.AllowCustomize = Infragistics.Win.DefaultableBoolean.False;
		ultraToolbar1.Settings.AllowDockTop = Infragistics.Win.DefaultableBoolean.True;
		ultraToolbar1.Text = "Tool1";
		buttonTool3.InstanceProps.IsFirstInGroup = true;
		buttonTool4.InstanceProps.IsFirstInGroup = true;
		buttonTool5.InstanceProps.IsFirstInGroup = true;
		popupMenuTool1.InstanceProps.IsFirstInGroup = true;
		buttonTool6.InstanceProps.IsFirstInGroup = true;
		buttonTool7.InstanceProps.IsFirstInGroup = true;
		labelTool1.InstanceProps.IsFirstInGroup = true;
		stateButtonTool1.Checked = true;
		ultraToolbar1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[17]
		{
			buttonTool1, buttonTool2, buttonTool3, buttonTool4, buttonTool5, popupMenuTool1, buttonTool6, buttonTool7, labelTool1, stateButtonTool1,
			stateButtonTool2, stateButtonTool3, stateButtonTool4, stateButtonTool5, stateButtonTool6, stateButtonTool7, stateButtonTool8
		});
		ultraToolbar2.DockedColumn = 0;
		ultraToolbar2.DockedRow = 0;
		ultraToolbar2.IsMainMenuBar = true;
		ultraToolbar2.Text = "Menu1";
		ultraToolbar2.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[4] { popupMenuTool2, popupMenuTool3, popupMenuTool4, popupMenuTool5 });
		this.ultraToolbarsManager1.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[2] { ultraToolbar1, ultraToolbar2 });
		appearance15.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance15.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.ToolbarSettings.Appearance = appearance15;
		appearance16.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance16.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance16.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.ToolbarSettings.HotTrackAppearance = appearance16;
		popupMenuTool6.SharedProps.Caption = "檔案(&F)";
		popupMenuTool6.SharedProps.Category = "合約";
		buttonTool9.InstanceProps.IsFirstInGroup = true;
		buttonTool11.InstanceProps.IsFirstInGroup = true;
		popupMenuTool6.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[4] { buttonTool8, buttonTool9, buttonTool10, buttonTool11 });
		popupMenuTool7.SharedProps.Caption = "編輯(&E)";
		popupMenuTool7.SharedProps.Category = "合約";
		popupMenuTool8.InstanceProps.IsFirstInGroup = true;
		buttonTool14.InstanceProps.IsFirstInGroup = true;
		popupMenuTool7.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[4] { buttonTool12, buttonTool13, popupMenuTool8, buttonTool14 });
		popupMenuTool9.SharedProps.Caption = "檢視(&V)";
		popupMenuTool9.SharedProps.Category = "合約";
		popupMenuTool9.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[2] { buttonTool15, buttonTool16 });
		popupMenuTool10.SharedProps.Caption = "工具(&T)";
		popupMenuTool10.SharedProps.Category = "合約";
		buttonTool17.SharedProps.Caption = "切換專案...";
		buttonTool17.SharedProps.Category = "合約";
		appearance17.Image = resources.GetObject("appearance17.Image");
		buttonTool18.SharedProps.AppearancesSmall.Appearance = appearance17;
		buttonTool18.SharedProps.Caption = "列印報表...";
		buttonTool18.SharedProps.Category = "合約";
		buttonTool18.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageOnlyOnToolbars;
		buttonTool19.SharedProps.Caption = "結束結算功能";
		buttonTool19.SharedProps.Category = "合約";
		appearance18.Image = resources.GetObject("appearance18.Image");
		buttonTool20.SharedProps.AppearancesSmall.Appearance = appearance18;
		buttonTool20.SharedProps.Caption = "核定結算";
		buttonTool20.SharedProps.Category = "編輯";
		buttonTool20.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		popupMenuTool11.SharedProps.Caption = "編輯結算數量/金額";
		popupMenuTool11.SharedProps.Category = "編輯";
		buttonTool23.InstanceProps.IsFirstInGroup = true;
		popupMenuTool11.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[3] { buttonTool21, buttonTool22, buttonTool23 });
		buttonTool24.SharedProps.Caption = "填入估驗數量/金額";
		buttonTool24.SharedProps.Category = "編輯";
		buttonTool25.SharedProps.Caption = "填入契約數量/金額";
		buttonTool25.SharedProps.Category = "編輯";
		appearance19.Image = resources.GetObject("appearance19.Image");
		buttonTool26.SharedProps.AppearancesSmall.Appearance = appearance19;
		buttonTool26.SharedProps.Caption = "重新產生資料";
		buttonTool26.SharedProps.Category = "編輯";
		buttonTool26.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		appearance20.Image = resources.GetObject("appearance20.Image");
		buttonTool27.SharedProps.AppearancesSmall.Appearance = appearance20;
		buttonTool27.SharedProps.Caption = "取消核定";
		buttonTool27.SharedProps.Category = "編輯";
		buttonTool27.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		buttonTool28.SharedProps.Caption = "各期估驗彙整查詢...";
		buttonTool28.SharedProps.Category = "檢視";
		buttonTool28.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		buttonTool29.SharedProps.Caption = "編輯結算總計資訊...";
		buttonTool29.SharedProps.Category = "檢視";
		buttonTool29.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		popupMenuTool12.SharedProps.Caption = "說明(&H)";
		popupMenuTool12.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[2] { buttonTool30, buttonTool31 });
		buttonTool32.SharedProps.Caption = "關於PCCES...";
		buttonTool33.SharedProps.Caption = "自行輸入數量/金額...";
		buttonTool33.SharedProps.Category = "編輯";
		buttonTool34.SharedProps.Caption = "最新消息...";
		appearance21.Image = resources.GetObject("appearance21.Image");
		buttonTool35.SharedProps.AppearancesSmall.Appearance = appearance21;
		buttonTool35.SharedProps.Caption = "製作電子檔...";
		buttonTool35.SharedProps.Category = "合約";
		buttonTool35.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageOnlyOnToolbars;
		labelTool2.SharedProps.Caption = "階層:";
		stateButtonTool9.Checked = true;
		stateButtonTool9.OptionSetKey = "switch";
		stateButtonTool9.SharedProps.Caption = "1";
		stateButtonTool9.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool10.OptionSetKey = "switch";
		stateButtonTool10.SharedProps.Caption = "2";
		stateButtonTool10.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool11.OptionSetKey = "switch";
		stateButtonTool11.SharedProps.Caption = "3";
		stateButtonTool11.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool12.OptionSetKey = "switch";
		stateButtonTool12.SharedProps.Caption = "4";
		stateButtonTool12.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool13.OptionSetKey = "switch";
		stateButtonTool13.SharedProps.Caption = "5";
		stateButtonTool13.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool14.OptionSetKey = "switch";
		stateButtonTool14.SharedProps.Caption = "6";
		stateButtonTool14.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool15.OptionSetKey = "switch";
		stateButtonTool15.SharedProps.Caption = "7";
		stateButtonTool15.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool16.OptionSetKey = "switch";
		stateButtonTool16.SharedProps.Caption = "8";
		stateButtonTool16.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		this.ultraToolbarsManager1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[29]
		{
			popupMenuTool6, popupMenuTool7, popupMenuTool9, popupMenuTool10, buttonTool17, buttonTool18, buttonTool19, buttonTool20, popupMenuTool11, buttonTool24,
			buttonTool25, buttonTool26, buttonTool27, buttonTool28, buttonTool29, popupMenuTool12, buttonTool32, buttonTool33, buttonTool34, buttonTool35,
			labelTool2, stateButtonTool9, stateButtonTool10, stateButtonTool11, stateButtonTool12, stateButtonTool13, stateButtonTool14, stateButtonTool15, stateButtonTool16
		});
		this.ultraToolbarsManager1.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(ultraToolbarsManager1_ToolClick);
		this.gridBudget1._ExcelFileName = "";
		this.gridBudget1._ExcelSheeName = "";
		this.gridBudget1._IsOpenExcelAfterExport = false;
		this.gridBudget1.AllowEditing = false;
		this.gridBudget1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridBudget1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
		this.gridBudget1.ColumnInfo = resources.GetString("gridBudget1.ColumnInfo");
		this.ultraToolbarsManager1.SetContextMenuUltra(this.gridBudget1, "PopCNT");
		this.gridBudget1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridBudget1.ExtendLastCol = true;
		this.gridBudget1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridBudget1.ForeColor = System.Drawing.Color.Black;
		this.gridBudget1.Location = new System.Drawing.Point(0, 30);
		this.gridBudget1.Name = "gridBudget1";
		this.gridBudget1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.gridBudget1.ShowCursor = true;
		this.gridBudget1.ShowSort = false;
		this.gridBudget1.ShowToolTipOnNarrowColumn = true;
		this.gridBudget1.Size = new System.Drawing.Size(625, 417);
		this.gridBudget1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridBudget1.Styles"));
		this.gridBudget1.TabIndex = 8;
		this.gridBudget1.Tree.Column = 1;
		this.gridBudget1.Tree.LineColor = System.Drawing.Color.Gray;
		this.gridBudget1.Resize += new System.EventHandler(gridBudget1_Resize);
		this._FormSys_B_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
		this._FormSys_B_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
		this._FormSys_B_Toolbars_Dock_Area_Top.Name = "_FormSys_B_Toolbars_Dock_Area_Top";
		this._FormSys_B_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(792, 52);
		this._FormSys_B_Toolbars_Dock_Area_Top.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 553);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Name = "_FormSys_B_Toolbars_Dock_Area_Bottom";
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(792, 0);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
		this._FormSys_B_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 52);
		this._FormSys_B_Toolbars_Dock_Area_Left.Name = "_FormSys_B_Toolbars_Dock_Area_Left";
		this._FormSys_B_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 501);
		this._FormSys_B_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
		this._FormSys_B_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(792, 52);
		this._FormSys_B_Toolbars_Dock_Area_Right.Name = "_FormSys_B_Toolbars_Dock_Area_Right";
		this._FormSys_B_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 501);
		this._FormSys_B_Toolbars_Dock_Area_Right.ToolbarsManager = this.ultraToolbarsManager1;
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.White;
		this.imageList2.Images.SetKeyName(0, "");
		this.imageList2.Images.SetKeyName(1, "");
		this.imageList2.Images.SetKeyName(2, "");
		this.LeftPanel.Controls.Add(this.onlineList1);
		this.LeftPanel.Controls.Add(this.functionButtons1);
		this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
		this.LeftPanel.Location = new System.Drawing.Point(0, 52);
		this.LeftPanel.Name = "LeftPanel";
		this.LeftPanel.Size = new System.Drawing.Size(160, 501);
		this.LeftPanel.TabIndex = 9;
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
		this.functionButtons1.Size = new System.Drawing.Size(160, 501);
		this.functionButtons1.TabIndex = 3;
		this.pnl_spliter.BackColor = System.Drawing.Color.LightGray;
		this.pnl_spliter.Controls.Add(this.Btn_Splt);
		this.pnl_spliter.Controls.Add(this.ssp_Lower);
		this.pnl_spliter.Controls.Add(this.ssp_Bottom);
		this.pnl_spliter.Controls.Add(this.ssp_Upper);
		this.pnl_spliter.Controls.Add(this.ssp_Top);
		this.pnl_spliter.Dock = System.Windows.Forms.DockStyle.Left;
		this.pnl_spliter.Location = new System.Drawing.Point(160, 52);
		this.pnl_spliter.Name = "pnl_spliter";
		this.pnl_spliter.Size = new System.Drawing.Size(7, 501);
		this.pnl_spliter.TabIndex = 11;
		appearance22.BorderColor = System.Drawing.Color.Transparent;
		appearance22.BorderColor3DBase = System.Drawing.Color.Transparent;
		appearance22.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance11.ImageBackground");
		this.Btn_Splt.Appearance = appearance22;
		this.Btn_Splt.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Borderless;
		this.Btn_Splt.Cursor = System.Windows.Forms.Cursors.Hand;
		this.Btn_Splt.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Btn_Splt.ImageSize = new System.Drawing.Size(7, 57);
		this.Btn_Splt.Location = new System.Drawing.Point(0, 220);
		this.Btn_Splt.Name = "Btn_Splt";
		this.Btn_Splt.ShapeImage = (System.Drawing.Image)resources.GetObject("Btn_Splt.ShapeImage");
		this.Btn_Splt.ShowFocusRect = false;
		this.Btn_Splt.ShowOutline = false;
		this.Btn_Splt.Size = new System.Drawing.Size(7, 70);
		this.Btn_Splt.TabIndex = 7;
		this.Btn_Splt.Click += new System.EventHandler(Btn_Splt_Click);
		this.ssp_Lower.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.ssp_Lower.Location = new System.Drawing.Point(0, 290);
		this.ssp_Lower.Name = "ssp_Lower";
		this.ssp_Lower.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Lower.OcxState");
		this.ssp_Lower.Size = new System.Drawing.Size(7, 208);
		this.ssp_Lower.TabIndex = 6;
		this.ssp_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.ssp_Bottom.Location = new System.Drawing.Point(0, 498);
		this.ssp_Bottom.Name = "ssp_Bottom";
		this.ssp_Bottom.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Bottom.OcxState");
		this.ssp_Bottom.Size = new System.Drawing.Size(7, 3);
		this.ssp_Bottom.TabIndex = 5;
		this.ssp_Upper.Dock = System.Windows.Forms.DockStyle.Top;
		this.ssp_Upper.Location = new System.Drawing.Point(0, 3);
		this.ssp_Upper.Name = "ssp_Upper";
		this.ssp_Upper.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Upper.OcxState");
		this.ssp_Upper.Size = new System.Drawing.Size(7, 217);
		this.ssp_Upper.TabIndex = 3;
		this.ssp_Top.Dock = System.Windows.Forms.DockStyle.Top;
		this.ssp_Top.Location = new System.Drawing.Point(0, 0);
		this.ssp_Top.Name = "ssp_Top";
		this.ssp_Top.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Top.OcxState");
		this.ssp_Top.Size = new System.Drawing.Size(7, 3);
		this.ssp_Top.TabIndex = 2;
		this.panel1.Controls.Add(this.gridBudget1);
		this.panel1.Controls.Add(this.panel7);
		this.panel1.Controls.Add(this.ultraStatusBar1);
		this.panel1.Controls.Add(this.panel2);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(167, 52);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(625, 501);
		this.panel1.TabIndex = 12;
		this.panel7.Controls.Add(this.lblTotal);
		this.panel7.Controls.Add(this.ultraLabel8);
		this.panel7.Controls.Add(this.axSSPanel2);
		this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel7.Location = new System.Drawing.Point(0, 447);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(625, 28);
		this.panel7.TabIndex = 10;
		this.lblTotal.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance23.ForeColor = System.Drawing.Color.Blue;
		appearance23.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance23.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblTotal.Appearance = appearance23;
		this.lblTotal.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.lblTotal.Font = new System.Drawing.Font("Courier New", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblTotal.Location = new System.Drawing.Point(64, 5);
		this.lblTotal.Name = "lblTotal";
		this.lblTotal.Size = new System.Drawing.Size(512, 19);
		this.lblTotal.TabIndex = 14;
		appearance24.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		appearance24.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel8.Appearance = appearance24;
		this.ultraLabel8.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.ultraLabel8.Font = new System.Drawing.Font("Courier New", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.ultraLabel8.Location = new System.Drawing.Point(4, 5);
		this.ultraLabel8.Name = "ultraLabel8";
		this.ultraLabel8.Size = new System.Drawing.Size(74, 19);
		this.ultraLabel8.TabIndex = 13;
		this.ultraLabel8.Text = "總計：";
		this.axSSPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.axSSPanel2.Location = new System.Drawing.Point(0, 0);
		this.axSSPanel2.Name = "axSSPanel2";
		this.axSSPanel2.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("axSSPanel2.OcxState");
		this.axSSPanel2.Size = new System.Drawing.Size(625, 28);
		this.axSSPanel2.TabIndex = 1;
		appearance25.FontData.SizeInPoints = 11f;
		appearance25.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraStatusBar1.Appearance = appearance25;
		this.ultraStatusBar1.Location = new System.Drawing.Point(0, 475);
		this.ultraStatusBar1.Name = "ultraStatusBar1";
		appearance26.TextVAlign = Infragistics.Win.VAlign.Bottom;
		ultraStatusPanel1.Appearance = appearance26;
		ultraStatusPanel1.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel1.Key = "RowsCount";
		ultraStatusPanel1.Text = "資料筆數：";
		ultraStatusPanel1.Width = 200;
		ultraStatusPanel2.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel2.Key = "ProgressBar";
		ultraStatusPanel2.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		this.ultraStatusBar1.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[2] { ultraStatusPanel1, ultraStatusPanel2 });
		this.ultraStatusBar1.Size = new System.Drawing.Size(625, 26);
		this.ultraStatusBar1.TabIndex = 9;
		this.ultraStatusBar1.Text = "ultraStatusBar1";
		this.panel2.Controls.Add(this.lblProjectData);
		this.panel2.Controls.Add(this.ultraLabel10);
		this.panel2.Controls.Add(this.BtnSwitchProject);
		this.panel2.Controls.Add(this.ultraLabel1);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(625, 30);
		this.panel2.TabIndex = 2;
		this.lblProjectData.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance27.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance27.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblProjectData.Appearance = appearance27;
		this.lblProjectData.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.lblProjectData.Font = new System.Drawing.Font("細明體", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblProjectData.Location = new System.Drawing.Point(80, 5);
		this.lblProjectData.Name = "lblProjectData";
		this.lblProjectData.Size = new System.Drawing.Size(428, 20);
		this.lblProjectData.TabIndex = 15;
		appearance28.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel10.Appearance = appearance28;
		this.ultraLabel10.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.ultraLabel10.Font = new System.Drawing.Font("細明體", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel10.Location = new System.Drawing.Point(10, 7);
		this.ultraLabel10.Name = "ultraLabel10";
		this.ultraLabel10.Size = new System.Drawing.Size(74, 19);
		this.ultraLabel10.TabIndex = 14;
		this.ultraLabel10.Text = "目前專案：";
		this.BtnSwitchProject.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance29.BackColor = System.Drawing.Color.Silver;
		appearance29.BackColor2 = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance29.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		appearance29.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.BtnSwitchProject.Appearance = appearance29;
		this.BtnSwitchProject.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.BtnSwitchProject.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.BtnSwitchProject.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance30.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance30.BackColor2 = System.Drawing.Color.White;
		appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.BtnSwitchProject.HotTrackAppearance = appearance30;
		this.BtnSwitchProject.HotTracking = true;
		this.BtnSwitchProject.Location = new System.Drawing.Point(530, 4);
		this.BtnSwitchProject.Name = "BtnSwitchProject";
		this.BtnSwitchProject.Size = new System.Drawing.Size(92, 24);
		this.BtnSwitchProject.TabIndex = 12;
		this.BtnSwitchProject.Text = "切換專案";
		this.BtnSwitchProject.Click += new System.EventHandler(BtnSwitchProject_Click);
		this.ultraLabel1.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.ultraLabel1.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.ultraLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ultraLabel1.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(625, 30);
		this.ultraLabel1.TabIndex = 0;
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		base.ClientSize = new System.Drawing.Size(792, 553);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.pnl_spliter);
		base.Controls.Add(this.LeftPanel);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Left);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Right);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Top);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Bottom);
		base.KeyPreview = true;
		base.Name = "FormSubClose";
		this.Text = "契約結算";
		base.Load += new System.EventHandler(FormSubClose_Load);
		base.Resize += new System.EventHandler(FormSubClose_Resize);
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridBudget1).EndInit();
		this.LeftPanel.ResumeLayout(false);
		this.LeftPanel.PerformLayout();
		this.pnl_spliter.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ssp_Lower).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Bottom).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Upper).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Top).EndInit();
		this.panel1.ResumeLayout(false);
		this.panel7.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.axSSPanel2).EndInit();
		this.panel2.ResumeLayout(false);
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

	public FormSubClose()
	{
		InitializeComponent();
		GridCols = gridBudget1.Cols.Count;
		GridColsSquence = new object[GridCols, 8];
	}

	private void HideCols(bool IsHide)
	{
		if (IsHide)
		{
			gridBudget1.Cols["LevelNo"].Visible = false;
			gridBudget1.Cols["PrintNo"].Visible = false;
			gridBudget1.Cols["Kind"].Visible = false;
			gridBudget1.Cols["SNo"].Visible = false;
		}
	}

	private void FormSubClose_Resize(object sender, EventArgs e)
	{
		int TotalH = pnl_spliter.Height;
		int iHeight = (TotalH - 3 - 3 - 57) / 2;
		ssp_Upper.Height = iHeight;
		ssp_Lower.Height = iHeight;
	}

	private void Btn_Splt_Click(object sender, EventArgs e)
	{
		if (LeftPanel.Width == 0)
		{
			LeftPanel.Width = 160;
			Btn_Splt.Appearance.ImageBackground = iglst_splt_Btn.Images[0];
		}
		else
		{
			LeftPanel.Width = 0;
			Btn_Splt.Appearance.ImageBackground = iglst_splt_Btn.Images[2];
		}
	}

	private void FormSubClose_Load(object sender, EventArgs e)
	{
		HideCols(IsHide: true);
		SettingDecimal();
		FormSubClose_Resize(null, null);
		RememberColsProps();
		base.ParentForm.Text = "PCCES Win 4.3 【契約結算】";
		functionButtons1._UserID = F_UserID;
		functionButtons1._UserName = F_UserName;
		functionButtons1._ServerName = F_ServerName;
		functionButtons1._CurrOpenMode = FunctionOpenMode.Invoice;
		functionButtons1._ActiveFunction = "SUBCLOSE";
		onlineList1._UserID = F_UserID;
		onlineList1._UserName = F_UserName;
		onlineList1._ServerName = F_ServerName;
		onlineList1._FunctionName = F_FunctionName;
		onlineList1._HasRegistered = F_HasRegistered;
		onlineList1.Connect();
		SysUser oSysUser = new SysUser();
		ultraStatusBar1.Panels[1].Text = "目前資料庫：" + oSysUser.GetSysUserDatabaseDesc(F_UserID);
		LoadData();
	}

	private void ultraToolbarsManager1_ToolClick(object sender, ToolClickEventArgs e)
	{
		DoMenuAction(e.Tool.Key);
	}

	private void LoadData()
	{
		lblProjectData.Text = "【" + F_ProjectCode + "】" + F_ProjectNameC;
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(SubClose) 契約結算");
		subProject subcom = new subProject(tmp_AL1);
		ls_prjcode = F_ProjectCode;
		ls_subproj = F_SubProjetCode;
		subcom = null;
		sub_acc AccCom = new sub_acc(tmp_AL1);
		F_IsLock = AccCom.GetLockMode(ls_Queue, ls_subproj, ls_prjcode);
		if (F_IsLock)
		{
			ultraToolbarsManager1.Tools["mnuApprove"].SharedProps.Visible = false;
			ultraToolbarsManager1.Tools["mnuUndoApprove"].SharedProps.Visible = true;
			ultraToolbarsManager1.Tools["mnuFile_Digital"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuPrint"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuReGen"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuCalcu"].SharedProps.Enabled = true;
		}
		else
		{
			ultraToolbarsManager1.Tools["mnuApprove"].SharedProps.Visible = true;
			ultraToolbarsManager1.Tools["mnuUndoApprove"].SharedProps.Visible = false;
			ultraToolbarsManager1.Tools["mnuFile_Digital"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuPrint"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuReGen"].SharedProps.Enabled = true;
		}
		AccCom.ps_prjcode = ls_prjcode;
		AccCom.ps_subcode = ls_subproj;
		AccCom.ps_queue = ls_Queue;
		AccCom.ps_date_insp = PubTools.ChgDateStr(DateTime.Now.ToString());
		AccCom.ps_date_rece = PubTools.ChgDateStr(DateTime.Now.ToString());
		AccCom.ps_this_prec = "0";
		AccCom.InseItem();
		AccCom = null;
		PubTools.WriteRoughlyLog(tmp_AL1);
		submfq MfqCom = new submfq(tmp_AL1);
		DT1 = MfqCom.ListCloseItem("", ls_Queue, ls_subproj, ls_prjcode);
		MfqCom = null;
		BindToGrid();
	}

	private void BindToGrid()
	{
		ultraToolbarsManager1.BeginUpdate();
		ultraToolbarsManager1.Enabled = false;
		int iLevel = 0;
		RememberColsProps();
		CellStyle CS1 = gridBudget1.Styles.Add("AnalysisColor");
		CellStyle CS9 = gridBudget1.Styles.Add("IsSharedColor");
		CellStyle CS10 = gridBudget1.Styles.Add("MainColor");
		CS1.ForeColor = Color.Red;
		CS10.ForeColor = Color.Blue;
		CS9.ForeColor = Color.Plum;
		gridBudget1.Clear(ClearFlags.All);
		gridBudget1.Select(0, 0);
		ultraStatusBar1.Panels[0].Text = "資料筆數：" + DT1.Rows.Count;
		int iRows = DT1.Rows.Count + 1;
		gridBudget1.Rows.Count = iRows;
		SetGridColumn();
		double aTotal = 0.0;
		for (int i = 0; i < DT1.Rows.Count; i++)
		{
			string sKind = DT1.Rows[i]["Kind"].ToString().Trim();
			switch (sKind)
			{
			default:
				if (!(sKind == "U"))
				{
					break;
				}
				goto case "B";
			case "B":
			case "L":
			case "F":
			case "S":
			case "Z":
				gridBudget1.Rows[i + 1].Style = gridBudget1.Styles["MainColor"];
				break;
			}
			gridBudget1[i + 1, "ItemNo"] = DT1.Rows[i]["ItemNo"].ToString().Trim();
			gridBudget1[i + 1, "CName"] = DT1.Rows[i]["cName"].ToString().Trim();
			gridBudget1[i + 1, "UnitName"] = DT1.Rows[i]["ItemUnit"].ToString().Trim();
			gridBudget1[i + 1, "ItemQty"] = DT1.Rows[i]["itemqty"];
			gridBudget1[i + 1, "ItemAmt"] = PubTools.Str2Double(DT1.Rows[i]["itemqty"]) * PubTools.Str2Double(DT1.Rows[i]["itemcost"]);
			gridBudget1[i + 1, "AccQty"] = DT1.Rows[i]["Acc_Qty"];
			gridBudget1[i + 1, "AccAmt"] = DT1.Rows[i]["Acc_Amt"];
			gridBudget1[i + 1, "Acc_Prec"] = string.Format("{0:N2}", DT1.Rows[i]["Acc_Prec"]) + "%";
			gridBudget1[i + 1, "Cost"] = DT1.Rows[i]["itemCost"];
			gridBudget1[i + 1, "Pre_Qty"] = DT1.Rows[i]["Pre_Qty"];
			gridBudget1[i + 1, "Pre_Amt"] = DT1.Rows[i]["Pre_Amt"];
			gridBudget1[i + 1, "ChgQty"] = DT1.Rows[i]["chgqty"];
			gridBudget1[i + 1, "ChgAmt"] = PubTools.Str2Double(DT1.Rows[i]["chgqty"]) * PubTools.Str2Double(DT1.Rows[i]["chgcost"]);
			gridBudget1[i + 1, "PrintNo"] = DT1.Rows[i]["itemdes"].ToString().Trim();
			double Diff = PubTools.Str2Double(PubTools.ARound(DT1.Rows[i]["Acc_Amt"], F_MainAmt)) - PubTools.Str2Double(PubTools.ARound(DT1.Rows[i]["itemqty"], F_MainQty)) * PubTools.Str2Double(PubTools.ARound(DT1.Rows[i]["itemcost"], F_MainCst));
			gridBudget1[i + 1, "Diff"] = Diff;
			if (Diff < 0.0)
			{
				CellRange cg = gridBudget1.GetCellRange(i + 1, gridBudget1.Cols["Diff"].SafeIndex, i + 1, gridBudget1.Cols["Diff"].SafeIndex);
				cg.Style = CS1;
			}
			gridBudget1.Rows[i + 1].IsNode = true;
			if (DT1.Rows[i]["itemdes"].ToString().Trim() == "".PadLeft(32, '9'))
			{
				gridBudget1.Rows[i + 1].Node.Level = 1;
				aTotal = PubTools.Str2Double(PubTools.ARound(DT1.Rows[i]["Acc_Amt"], F_MainAmt));
			}
			else
			{
				gridBudget1.Rows[i + 1].Node.Level = Convert.ToInt32(DT1.Rows[i]["itemdes"].ToString().Trim().Length / 4);
			}
			if (gridBudget1.Rows[i + 1].Node.Level > iLevel)
			{
				iLevel = gridBudget1.Rows[i + 1].Node.Level;
			}
		}
		SwitchToCorrectLevelStatus(iLevel);
		lblTotal.Text = string.Format("{0:N" + F_MainAmt + "}", aTotal);
		ultraToolbarsManager1.Enabled = true;
		ultraToolbarsManager1.EndUpdate();
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
		}
	}

	private void SettingDecimal()
	{
		DataTable DTDecimal = new DataTable();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add(CommonMethods.GetFormTypeTitle(FormType.MrsBaseAnalysis));
		Archnowledge.Pcces.BUDClass.PubDecimal dbDecimal = new Archnowledge.Pcces.BUDClass.PubDecimal(aArr);
		dbDecimal.ps_projectCode = F_ProjectCode;
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
			if (gridBudget1.Cols[i].Name == "ItemQty" || gridBudget1.Cols[i].Name == "AccQty" || gridBudget1.Cols[i].Name == "Pre_Qty" || gridBudget1.Cols[i].Name == "ChgQty")
			{
				if (F_MainQty > 0)
				{
					GridColsSquence[i, 5] = "###,###,###,##0." + "0".PadLeft(F_MainQty, '0');
				}
				else
				{
					GridColsSquence[i, 5] = "###,###,###,##0";
				}
			}
			if (gridBudget1.Cols[i].Name == "Cost")
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
			if (gridBudget1.Cols[i].Name == "ItemAmt" || gridBudget1.Cols[i].Name == "AccAmt" || gridBudget1.Cols[i].Name == "Diff" || gridBudget1.Cols[i].Name == "Pre_Amt" || gridBudget1.Cols[i].Name == "ChgAmt")
			{
				if (F_MainAmt > 0)
				{
					GridColsSquence[i, 5] = "###,###,###,##0." + "0".PadLeft(F_MainAmt, '0');
				}
				else
				{
					GridColsSquence[i, 5] = "###,###,###,##0";
				}
			}
			GridColsSquence[i, 7] = gridBudget1.Cols[i].TextAlign;
		}
	}

	private void DoMenuAction(string MenuID)
	{
		switch (MenuID)
		{
		case "mnuClose":
			if (!DBClass.ChkAuthority(F_UserID, "F01200010003"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F01200010003") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				CloseThisForm();
			}
			break;
		case "mnuSwitchProj":
			BtnSwitchProject_Click(this, EventArgs.Empty);
			break;
		case "mnuPrint":
			if (!DBClass.ChkAuthority(F_UserID, "F01200010002"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F01200010002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Execute_Print();
			}
			break;
		case "mnuApprove":
			if (!DBClass.ChkAuthority(F_UserID, "F01200020001"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F01200020001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Do_Approve();
			}
			break;
		case "mnuUndoApprove":
			Do_UndoApprove();
			break;
		case "mnuCalcuInv":
			if (!DBClass.ChkAuthority(F_UserID, "F012000200020001"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F012000200020001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Do_CalcuInv();
			}
			break;
		case "mnuCalcuCnt":
			if (!DBClass.ChkAuthority(F_UserID, "F012000200020002"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F012000200020002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Do_CalcuCnt();
			}
			break;
		case "mnuReGen":
			if (!DBClass.ChkAuthority(F_UserID, "F01200020003"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F01200020003") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				ReGenData();
			}
			break;
		case "mnuIssueList":
			if (!DBClass.ChkAuthority(F_UserID, "F01200030001"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F01200030001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Execute_IssueList();
			}
			break;
		case "mnuSubCloseInfo":
			if (!DBClass.ChkAuthority(F_UserID, "F01200030002"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F01200030002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Execute_SubCloseInfo();
			}
			break;
		case "mnuCalcuInput":
			if (!DBClass.ChkAuthority(F_UserID, "F012000200020003"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F012000200020003") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Execute_SubCloseInput();
			}
			break;
		case "mnuAbout":
			if (!DBClass.ChkAuthority(F_UserID, "F01200040001"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F01200040001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Execute_About();
			}
			break;
		case "mnuUpdateList":
		{
			FormUpdateInfo FM_UPDINFO = new FormUpdateInfo();
			FM_UPDINFO.ShowDialog();
			FM_UPDINFO.Close();
			FM_UPDINFO.Dispose();
			FM_UPDINFO = null;
			break;
		}
		case "mnuFile_Digital":
			Do_FileDigital("");
			break;
		case "mnuLevel_1":
			gridBudget1.Tree.Show(1);
			break;
		case "mnuLevel_2":
			gridBudget1.Tree.Show(2);
			break;
		case "mnuLevel_3":
			gridBudget1.Tree.Show(3);
			break;
		case "mnuLevel_4":
			gridBudget1.Tree.Show(4);
			break;
		case "mnuLevel_5":
			gridBudget1.Tree.Show(5);
			break;
		case "mnuLevel_6":
			gridBudget1.Tree.Show(6);
			break;
		case "mnuLevel_7":
			gridBudget1.Tree.Show(7);
			break;
		case "mnuLevel_8":
			gridBudget1.Tree.Show(8);
			break;
		}
	}

	private void Execute_About()
	{
		FormAbout FMAB = new FormAbout();
		FMAB.ShowDialog();
	}

	private void Execute_SubCloseInput()
	{
		FormSubCloseInput FM_CLS_INP = new FormSubCloseInput();
		FM_CLS_INP._UserID = F_UserID;
		FM_CLS_INP._ProjectCode = F_ProjectCode;
		FM_CLS_INP.Owner = this;
		if (FM_CLS_INP.ShowDialog() == DialogResult.OK)
		{
			LoadData();
		}
		FM_CLS_INP.Close();
		FM_CLS_INP.Dispose();
		FM_CLS_INP = null;
	}

	private void Execute_SubCloseInfo()
	{
		FormSubCloseInfo FM_SUBCLZ = new FormSubCloseInfo();
		FM_SUBCLZ._ProjectCode = F_ProjectCode;
		FM_SUBCLZ._SubProjectCode = F_SubProjetCode;
		FM_SUBCLZ._UserID = F_UserID;
		FM_SUBCLZ.ShowDialog(this);
	}

	private void Execute_IssueList()
	{
		FormInvoiceSummary FM_INVSUMM = new FormInvoiceSummary();
		FM_INVSUMM._ProjectCode = F_ProjectCode;
		FM_INVSUMM._SubProjectCode = F_SubProjetCode;
		FM_INVSUMM._UserID = F_UserID;
		FM_INVSUMM.ShowDialog(this);
		FM_INVSUMM.Close();
		FM_INVSUMM.Dispose();
		FM_INVSUMM = null;
	}

	private void Do_UndoApprove()
	{
		if (MessageBox.Show(this, "確定要取消結算?", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			ArrayList tmp_AL1 = new ArrayList();
			tmp_AL1.Add(F_UserID);
			tmp_AL1.Add("(SubClose) 契約結算--核定結算");
			sub_acc SubAccCom = new sub_acc(tmp_AL1);
			SubAccCom.SetLockMode("0", ls_Queue, ls_subproj, ls_prjcode);
			ultraToolbarsManager1.Tools["mnuApprove"].SharedProps.Visible = true;
			ultraToolbarsManager1.Tools["mnuUndoApprove"].SharedProps.Visible = false;
			ultraToolbarsManager1.Tools["mnuCalcu"].SharedProps.Visible = true;
			ultraToolbarsManager1.Tools["mnuReGen"].SharedProps.Visible = true;
			ultraToolbarsManager1.Tools["mnuFile_Digital"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuPrint"].SharedProps.Enabled = false;
			UpIsLastqueue("N");
			string sSQL = "Delete from subacc where project='" + F_ProjectCode + "' and sproj='" + F_SubProjetCode + "' and queue = '9998'";
			ModifyDB ModDB = new ModifyDB(F_ProjectCode, tmp_AL1);
			ModDB.DBUpd(sSQL);
		}
	}

	private void Do_Approve()
	{
		if (MessageBox.Show(this, "確定要核定結算?", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
		{
			return;
		}
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(SubClose) 契約結算--核定結算");
		sub_acc SubAccCom = new sub_acc(tmp_AL1);
		submfq MfqCom = new submfq(tmp_AL1);
		DataTable MfqDT = MfqCom.ListCloseItem("", ls_Queue, ls_subproj, ls_prjcode);
		MfqCom = null;
		double ld_ContractAmt = 0.0;
		double ld_AccAmt = 0.0;
		for (int i = MfqDT.Rows.Count; i > 0; i--)
		{
			DataRow dr = MfqDT.Rows[i - 1];
			if (dr["Itemdes"].ToString().Trim() == "".PadLeft(32, '9'))
			{
				ld_AccAmt = PubTools.Str2Double(dr["Acc_amt"].ToString());
				ld_ContractAmt = PubTools.Str2Double(dr["ChgCost"].ToString());
				i = -1;
			}
			else if (dr["Kind"].ToString().ToUpper() == "Z" && dr["Itemdes"].ToString().Trim().Length == 4)
			{
				ld_AccAmt = PubTools.Str2Double(dr["Acc_amt"].ToString());
				ld_ContractAmt = PubTools.Str2Double(dr["ChgCost"].ToString());
				i = -1;
			}
		}
		double CloseAmt = Math.Abs(ld_AccAmt - ld_ContractAmt);
		SubAccCom.SetLockMode("1", ls_Queue, ls_subproj, ls_prjcode);
		ultraToolbarsManager1.Tools["mnuApprove"].SharedProps.Visible = false;
		ultraToolbarsManager1.Tools["mnuUndoApprove"].SharedProps.Visible = true;
		ultraToolbarsManager1.Tools["mnuCalcu"].SharedProps.Visible = false;
		ultraToolbarsManager1.Tools["mnuReGen"].SharedProps.Visible = false;
		ultraToolbarsManager1.Tools["mnuFile_Digital"].SharedProps.Enabled = true;
		ultraToolbarsManager1.Tools["mnuPrint"].SharedProps.Enabled = true;
		InsertLastqueue();
		UpIsLastqueue("Y");
	}

	private void Do_CalcuInv()
	{
		Cursor = Cursors.WaitCursor;
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(SubClose) 契約結算--計算(填入估驗數量/金額)");
		submfq MfqCom = new submfq(tmp_AL1);
		DataTable MfqDT = MfqCom.ListItem("", ls_Queue, ls_subproj, ls_prjcode);
		foreach (DataRow dr in MfqDT.Rows)
		{
			MfqCom.ps_quantity = "0";
			MfqCom.ps_tom_amt = "0";
			MfqCom.ps_itemdes = dr["itemdes"].ToString();
			MfqCom.ps_itemno = dr["qucode"].ToString();
			MfqCom.ps_prjcode = dr["project"].ToString();
			MfqCom.ps_subcode = dr["sproj"].ToString();
			MfqCom.UpdItem();
		}
		MfqCom = null;
		LoadData();
		Cursor = Cursors.Default;
	}

	private void Do_CalcuCnt()
	{
		Cursor = Cursors.WaitCursor;
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(SubClose) 契約結算--計算(填入契約數量/金額)");
		submfq MfqCom = new submfq(tmp_AL1);
		DataTable MfqDT = MfqCom.ListCloseItem("", ls_Queue, ls_subproj, ls_prjcode);
		foreach (DataRow dr in MfqDT.Rows)
		{
			double ld_qty = PubTools.Str2Double(dr["chgqty"].ToString());
			double ld_cost = PubTools.Str2Double(dr["chgcost"].ToString());
			double ld_Amt = PubTools.ARound(PubTools.ARound(ld_qty, F_MainQty) * PubTools.ARound(ld_cost, F_MainCst), 2L);
			double ld_Accqty = PubTools.Str2Double(dr["Pre_Qty"].ToString());
			double ld_Acccost = PubTools.Str2Double(dr["Pre_Amt"].ToString());
			MfqCom.ps_quantity = (ld_qty - ld_Accqty).ToString();
			MfqCom.ps_tom_amt = (ld_Amt - ld_Acccost).ToString();
			MfqCom.ps_itemdes = dr["itemdes"].ToString();
			MfqCom.ps_itemno = dr["qucode"].ToString();
			MfqCom.ps_prjcode = dr["project"].ToString();
			MfqCom.ps_subcode = dr["sproj"].ToString();
			MfqCom.UpdItem();
		}
		MfqCom = null;
		LoadData();
		Cursor = Cursors.Default;
	}

	private void ReGenData()
	{
		string l_Message = "重新將估驗計價的資料載入，會將原有資料覆蓋，確定要執行嗎?";
		if (MessageBox.Show(this, l_Message, "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			Cursor = Cursors.WaitCursor;
			ArrayList tmp_AL1 = new ArrayList();
			tmp_AL1.Add(F_UserID);
			tmp_AL1.Add("(SubClose) 契約結算--重新產生資料");
			sub_acc AccCom = new sub_acc(tmp_AL1);
			AccCom.DeleItem(ls_Queue, ls_subproj, ls_prjcode);
			AccCom.ps_prjcode = ls_prjcode;
			AccCom.ps_subcode = ls_subproj;
			AccCom.ps_queue = ls_Queue;
			AccCom.ps_date_insp = PubTools.ChgDateStr(DateTime.Now.ToString());
			AccCom.ps_date_rece = PubTools.ChgDateStr(DateTime.Now.ToString());
			AccCom.ps_this_prec = "0";
			AccCom.InseItem();
			AccCom = null;
			LoadData();
			Cursor = Cursors.Default;
		}
	}

	private void Execute_Print()
	{
		FormInvoiceReport FM_INV_RPT = new FormInvoiceReport();
		FM_INV_RPT._ActionName = F_ActionName;
		FM_INV_RPT._ProjectCode = F_ProjectCode;
		FM_INV_RPT._SubProjectCode = F_SubProjetCode;
		FM_INV_RPT._UserID = F_UserID;
		FM_INV_RPT.ShowDialog();
		FM_INV_RPT.Close();
		FM_INV_RPT.Dispose();
		FM_INV_RPT = null;
	}

	private void CloseThisForm()
	{
		string sWarning = "確定要結束 ?";
		if (MessageBox.Show(this, sWarning, "契約結算", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			(base.ParentForm as frmPccesMain).LeftPanel.Width = 160;
			Close();
		}
	}

	private void Do_FileDigital(string sFLAG)
	{
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = F_UserID;
		ArrayList aArrb = new ArrayList();
		aArrb.Clear();
		aArrb.Add(F_UserID);
		string IPStr = CommonMethods.GetIPAddress();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("預算--讀取預算書基本資料--" + F_ProjectCode + "(" + IPStr + ")");
		Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
		PROJ.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		DataTable DT2 = PROJ.ListItem("", F_ProjectCode);
		MainUnitCom MAIN_UCOM = new MainUnitCom(aArr);
		string sDeptName = MAIN_UCOM.Get_Main_Name(DT2.Rows[0]["mainCName"].ToString().Trim());
		if (sDeptName.Trim() == "")
		{
			sDeptName = MAIN_UCOM.Get_Main_Name(DT2.Rows[0]["mainCode"].ToString().Trim());
		}
		string sDeptEName = MAIN_UCOM.Get_Main_EName(DT2.Rows[0]["mainCode"].ToString().Trim());
		FormBudgetExp_Wzd FM_BDGT_EXP_WZD = new FormBudgetExp_Wzd();
		FM_BDGT_EXP_WZD._UserID = F_UserID;
		FM_BDGT_EXP_WZD._ActionName = F_ActionName;
		FM_BDGT_EXP_WZD._ProjectCode = F_ProjectCode;
		FM_BDGT_EXP_WZD._DeptName = sDeptName;
		FM_BDGT_EXP_WZD._DeptEName = sDeptEName;
		FM_BDGT_EXP_WZD._ProjectNameC = DT2.Rows[0]["projectNameC"].ToString().Trim();
		FM_BDGT_EXP_WZD._ProjectNameE = DT2.Rows[0]["projectNameE"].ToString().Trim();
		FM_BDGT_EXP_WZD._ProjectAddress = DT2.Rows[0]["projectAddress"].ToString().Trim();
		FM_BDGT_EXP_WZD._ProjectEngAddress = "";
		FM_BDGT_EXP_WZD._AccountCode1 = "";
		FM_BDGT_EXP_WZD._AccountCode2 = "";
		FM_BDGT_EXP_WZD._ProjFLAG = sFLAG.Trim();
		FM_BDGT_EXP_WZD._queue = "9999";
		if (sFLAG == "Z14AC1100" && !PROJ.ChkPostMode(F_ProjectCode))
		{
			MessageBox.Show(this, "專案中，使用到的工作要項中，有尚未核可的項目，\n請先返回基本資料庫維護，將使用到的項目[核可]。\n目前不能執行電子檔匯出。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		FM_BDGT_EXP_WZD.ShowDialog(this);
		FM_BDGT_EXP_WZD.Close();
		FM_BDGT_EXP_WZD.Dispose();
		FM_BDGT_EXP_WZD = null;
		DBCLS = null;
	}

	private void BtnSwitchProject_Click(object sender, EventArgs e)
	{
		if (!DBClass.ChkAuthority(F_UserID, "F01200010001"))
		{
			MessageBox.Show(this, DBClass.GetFuncName("F01200010001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		Cursor = Cursors.WaitCursor;
		lock (this)
		{
			FormBudgetProjectPick FM_BDGT_PPK1 = new FormBudgetProjectPick();
			FM_BDGT_PPK1.CallUpType = FormBudget_PickType.ProjectSwitch;
			FM_BDGT_PPK1._ActionName = F_ActionName;
			FM_BDGT_PPK1._UserID = F_UserID;
			FM_BDGT_PPK1.ShowDialog(this);
			FM_BDGT_PPK1.Close();
			FM_BDGT_PPK1.Dispose();
			FM_BDGT_PPK1 = null;
			LoadData();
		}
		Cursor = Cursors.Default;
	}

	private void SwitchToCorrectLevelStatus(int iLvl)
	{
		if (iLvl <= 0 || iLvl >= 9)
		{
			return;
		}
		((StateButtonTool)ultraToolbarsManager1.Tools["mnuLevel_" + iLvl]).Checked = true;
		for (int i = 1; i < 9; i++)
		{
			if (i <= iLvl)
			{
				((StateButtonTool)ultraToolbarsManager1.Tools["mnuLevel_" + i]).SharedProps.Enabled = true;
			}
			else
			{
				((StateButtonTool)ultraToolbarsManager1.Tools["mnuLevel_" + i]).SharedProps.Enabled = false;
			}
		}
	}

	private void UpIsLastqueue(string sType)
	{
		string sSQL = "Update subacc set IsLastqueue='" + sType + "' where project='" + F_ProjectCode + "' and sproj='" + F_SubProjetCode + "'";
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add("更新IsLastqueue的值");
		ModifyDB ModDB = new ModifyDB(F_ProjectCode, aArr);
		ModDB.DBUpd(sSQL);
		ModDB = null;
	}

	private void InsertLastqueue()
	{
		string sSQL = "Select queue from subacc where project='" + F_ProjectCode + "' and sproj='" + F_SubProjetCode + "' and queue = '9998'";
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add("更新IsLastqueue的值");
		ModifyDB ModDB = new ModifyDB(F_ProjectCode, aArr);
		DataTable DT = ModDB.DBList(sSQL);
		if (DT.Rows.Count == 0)
		{
			ArrayList tmp_AL1 = new ArrayList();
			tmp_AL1.Add(F_UserID);
			tmp_AL1.Add("(SubClose) 契約結算");
			subProject subcom = new subProject(tmp_AL1);
			ls_prjcode = F_ProjectCode;
			ls_subproj = F_SubProjetCode;
			subcom = null;
			sub_acc AccCom = new sub_acc(tmp_AL1);
			AccCom.ps_prjcode = ls_prjcode;
			AccCom.ps_subcode = ls_subproj;
			AccCom.ps_queue = "9998";
			AccCom.ps_date_insp = PubTools.ChgDateStr(DateTime.Now.ToString());
			AccCom.ps_date_rece = PubTools.ChgDateStr(DateTime.Now.ToString());
			AccCom.ps_this_prec = "0";
			AccCom.InseItem();
			AccCom = null;
			PubTools.WriteRoughlyLog(tmp_AL1);
			tmp_AL1 = null;
		}
		aArr = null;
		ModDB = null;
	}

	private void gridBudget1_Resize(object sender, EventArgs e)
	{
		FormSubClose_Resize(sender, e);
	}
}
