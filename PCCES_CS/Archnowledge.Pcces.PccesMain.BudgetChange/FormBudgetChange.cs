using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.CTRClass;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.PccesMain.About;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.PccesMain.Budget;
using Archnowledge.Pcces.PccesMain.MrsBase;
using Archnowledge.Pcces.PccesMain.Report;
using Archnowledge.Pcces.PccesMain.ShellLib;
using Archnowledge.Pcces.PccesMain.SysMaintain;
using Archnowledge.Pcces.STDClass;
using AxThreed;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinStatusBar;
using Infragistics.Win.UltraWinToolbars;

namespace Archnowledge.Pcces.PccesMain.BudgetChange;

public class FormBudgetChange : Form
{
	private Panel LeftPanel;

	private OnlineList onlineList1;

	public FunctionButtons functionButtons1;

	private Panel pnl_spliter;

	private UltraButton Btn_Splt;

	private AxSSPanel ssp_Lower;

	private AxSSPanel ssp_Bottom;

	private AxSSPanel ssp_Upper;

	private AxSSPanel ssp_Top;

	private Panel MainPanel;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Top;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Bottom;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Left;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Right;

	private ImageList iglst_splt_Btn;

	private Panel panel1;

	private UltraLabel ultraLabel1;

	private UltraStatusBar ultraStatusBar1;

	private GridBudget gridBudget1;

	private UltraButton BtnSwitchProject;

	private UltraLabel ultraLabel10;

	private UltraLabel lblProjectData;

	private UltraStatusBar ultraStatusBar2;

	private GridBudget gridBudget2;

	private Panel panel4;

	private Panel panel5;

	private UltraLabel ultraLabel5;

	private UltraLabel ultraLabel6;

	private UltraButton ultraButton2;

	private Splitter splitter1;

	private Panel panel6;

	private Panel panel7;

	private UltraLabel lblThisIssue;

	private UltraLabel ultraLabel7;

	private UltraLabel ultraLabel8;

	private FormSys_G_Info1 FM_INFO;

	private ImageList imageList2;

	private SaveFileDialog saveFileDialog1;

	private OpenFileDialog openFileDialog1;

	private Panel panel2;

	private UltraLabel lblTotal;

	private UltraLabel ultraLabel2;

	private AxSSPanel axSSPanel2;

	private UltraToolbarsManager ultraToolbarsManager1;

	private IContainer components;

	private bool IsDEBUG_MODE = false;

	private string F_CurrentDBName = "";

	private string F_FromDBName = "";

	private string F_PasteSource_Project;

	private string F_PasteSource_SrcKind;

	private DataSet DS1 = new DataSet();

	private bool Is_MultiRowSelect = false;

	private ItemA dbItemA;

	private MrsBaseA dbMrsBaseA;

	private string sIniFileName = CommonMethods.ExtractFilePath(Application.ExecutablePath) + "PccesMain.ini";

	private string sAssemType = "1";

	private string sIsSymbol = "N";

	private string sSymbol = "";

	private int DataRows_AfterBinding = 0;

	private bool F_IsNeedToReloadAllData = false;

	private int F_SNo = -1;

	private bool F_IsUseIR = true;

	private bool HasOpenedBreakdownForm = false;

	private int[] L1 = new int[9];

	private bool F_HasRegistered;

	private PccesFormAction F_ActionName = PccesFormAction.SubChange;

	private string F_KeyWord = "";

	private string F_ProjectCode;

	private string F_ProjectNameC;

	private string F_SubProjectCode = "";

	private int F_MainQty = 0;

	private int F_MainCst = 0;

	private int F_MainAmt = 0;

	private int F_AnaQty = 0;

	private int F_AnaCst = 0;

	private int F_AnaAmt = 0;

	private int GridCols = 15;

	private object[,] GridColsSquence;

	private DataTable DT1 = new DataTable();

	private DataTable DT2 = new DataTable();

	private string F_UserID;

	private string F_UserName = "";

	private int iCountNum = 0;

	private string Firstflag = "";

	private string F_FunctionName = "BudgetChange";

	private string F_chgCount = "0";

	private string F_ServerName = "localhost";

	public string _FromDBName
	{
		get
		{
			return F_FromDBName;
		}
		set
		{
			F_FromDBName = value;
		}
	}

	public string _PasteSource_Project
	{
		get
		{
			return F_PasteSource_Project;
		}
		set
		{
			F_PasteSource_Project = value;
		}
	}

	public string _PasteSource_SrcKind
	{
		get
		{
			return F_PasteSource_SrcKind;
		}
		set
		{
			F_PasteSource_SrcKind = value;
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
			lblProjectData.Text = "【" + F_ProjectCode + "】" + F_ProjectNameC;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.BudgetChange.FormBudgetChange));
		Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel3 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel4 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel5 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel6 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.OptionSet optionSet1 = new Infragistics.Win.UltraWinToolbars.OptionSet("switch");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Tool1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuPrint");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuFile_Digital");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuAddNew");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuEdit");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool5 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelete");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool6 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuReCal");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool1 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnuLevel");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool1 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_1", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool2 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_2", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool3 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_3", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool4 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_4", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool5 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_5", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool6 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_6", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool7 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_7", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool8 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_8", "switch");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool2 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnu_lblFind");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool1 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool7 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_Go");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar2 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Tool2");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool3 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnu_lblFind2");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool2 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo2");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool8 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_Go2");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool9 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuViewList");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar3 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Menu1");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool1 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("MenuFile");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool2 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("MenuEdit");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool3 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("MenuView");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool4 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("MenuDetEdit");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool5 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("工具(T)");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool6 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuHelp");
		Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool10 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelete");
		Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool4 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnu_lblFind");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool3 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo1");
		Infragistics.Win.ValueList valueList1 = new Infragistics.Win.ValueList(0);
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool11 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_Go");
		Infragistics.Win.Appearance appearance38 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool7 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Popup1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool12 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuAddNew");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool13 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuEdit");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool14 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelete");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool15 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuAddNew");
		Infragistics.Win.Appearance appearance39 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool16 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuEdit");
		Infragistics.Win.Appearance appearance40 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool17 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuChange");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool5 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnu_lblFind2");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool4 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo2");
		Infragistics.Win.ValueList valueList2 = new Infragistics.Win.ValueList(0);
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool18 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_Go2");
		Infragistics.Win.Appearance appearance41 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool19 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuViewList");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool8 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("MenuFile");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool20 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuFile_Digital");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool21 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuPrint");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool22 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool23 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuImport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool24 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExit");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool9 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("MenuEdit");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool25 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuAddNew");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool26 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuEdit");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool27 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelete");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool10 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("MenuView");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool28 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuViewHide1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool29 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuViewRes");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool11 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("MenuDetEdit");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool12 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("PopupMain");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool30 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuEditMain");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool13 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuDetailEdit_NewWItm");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool31 = new Infragistics.Win.UltraWinToolbars.ButtonTool("PopMenu1_Delete");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool32 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuReCal");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool33 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuPrint");
		Infragistics.Win.Appearance appearance42 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool34 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExit");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool35 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuEditBASIC");
		Infragistics.Win.Appearance appearance43 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool36 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuViewHide1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool37 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuEDEdtItem");
		Infragistics.Win.Appearance appearance44 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool38 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuEDDelItem");
		Infragistics.Win.Appearance appearance45 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool14 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuPopIns");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool39 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSibling");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool40 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuChild");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool41 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSibling");
		Infragistics.Win.Appearance appearance46 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool42 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuChild");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool43 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuReCal");
		Infragistics.Win.Appearance appearance47 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool15 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("PopupMenu2");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool16 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("PopupMain");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool44 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuEditMain");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool17 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuDetailEdit_NewWItm");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool45 = new Infragistics.Win.UltraWinToolbars.ButtonTool("PopMenu1_Delete");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool18 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuHelp");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool46 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuAbout");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool47 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuAbout");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool19 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("PopupMain");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool48 = new Infragistics.Win.UltraWinToolbars.ButtonTool("PopMnuMainSibling");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool49 = new Infragistics.Win.UltraWinToolbars.ButtonTool("PopMnuMainChild");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool50 = new Infragistics.Win.UltraWinToolbars.ButtonTool("PopMnuMainSibling");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool51 = new Infragistics.Win.UltraWinToolbars.ButtonTool("PopMnuMainChild");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool52 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuEditMain");
		Infragistics.Win.Appearance appearance48 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool20 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuDetailEdit_NewWItm");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool53 = new Infragistics.Win.UltraWinToolbars.ButtonTool("PopMnuPickWK_Mrs");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool54 = new Infragistics.Win.UltraWinToolbars.ButtonTool("PopMnuPickWK_Mrs");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool55 = new Infragistics.Win.UltraWinToolbars.ButtonTool("PopMenu1_Delete");
		Infragistics.Win.Appearance appearance49 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool21 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("工具(T)");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool56 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuTool_ItemReArrange");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool57 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuOption");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool58 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCalcu");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool59 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCalcu");
		Infragistics.Win.Appearance appearance50 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool60 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuTool_ItemReArrange");
		Infragistics.Win.Appearance appearance51 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool61 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuOption");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool62 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool63 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuImport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool64 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuFile_Digital");
		Infragistics.Win.Appearance appearance52 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool65 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuViewRes");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool6 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnuLevel");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool9 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_1", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool10 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_2", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool11 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_3", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool12 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_4", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool13 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_5", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool14 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_6", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool15 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_7", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool16 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_8", "switch");
		this.gridBudget1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.ultraStatusBar1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.panel1 = new System.Windows.Forms.Panel();
		this.lblProjectData = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel10 = new Infragistics.Win.Misc.UltraLabel();
		this.BtnSwitchProject = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.gridBudget2 = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.ultraStatusBar2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.LeftPanel = new System.Windows.Forms.Panel();
		this.onlineList1 = new Archnowledge.Pcces.PccesMain.ArchControls.OnlineList();
		this.functionButtons1 = new Archnowledge.Pcces.PccesMain.ArchControls.FunctionButtons();
		this.pnl_spliter = new System.Windows.Forms.Panel();
		this.Btn_Splt = new Infragistics.Win.Misc.UltraButton();
		this.ssp_Lower = new AxThreed.AxSSPanel();
		this.ssp_Bottom = new AxThreed.AxSSPanel();
		this.ssp_Upper = new AxThreed.AxSSPanel();
		this.ssp_Top = new AxThreed.AxSSPanel();
		this.MainPanel = new System.Windows.Forms.Panel();
		this.panel6 = new System.Windows.Forms.Panel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.lblTotal = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.axSSPanel2 = new AxThreed.AxSSPanel();
		this.panel7 = new System.Windows.Forms.Panel();
		this.lblThisIssue = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel8 = new Infragistics.Win.Misc.UltraLabel();
		this.splitter1 = new System.Windows.Forms.Splitter();
		this.panel4 = new System.Windows.Forms.Panel();
		this.panel5 = new System.Windows.Forms.Panel();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraButton2 = new Infragistics.Win.Misc.UltraButton();
		this.ultraToolbarsManager1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
		this._FormSys_B_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.iglst_splt_Btn = new System.Windows.Forms.ImageList(this.components);
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
		this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
		((System.ComponentModel.ISupportInitialize)this.gridBudget1).BeginInit();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridBudget2).BeginInit();
		this.LeftPanel.SuspendLayout();
		this.pnl_spliter.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ssp_Lower).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Bottom).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Upper).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Top).BeginInit();
		this.MainPanel.SuspendLayout();
		this.panel6.SuspendLayout();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.axSSPanel2).BeginInit();
		this.panel7.SuspendLayout();
		this.panel4.SuspendLayout();
		this.panel5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).BeginInit();
		base.SuspendLayout();
		this.gridBudget1._ExcelFileName = "";
		this.gridBudget1._ExcelSheeName = "";
		this.gridBudget1._IsOpenExcelAfterExport = false;
		this.gridBudget1.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
		this.gridBudget1.AllowEditing = false;
		this.gridBudget1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridBudget1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
		this.gridBudget1.ColumnInfo = resources.GetString("gridBudget1.ColumnInfo");
		this.ultraToolbarsManager1.SetContextMenuUltra(this.gridBudget1, "Popup1");
		this.gridBudget1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridBudget1.ExtendLastCol = true;
		this.gridBudget1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridBudget1.ForeColor = System.Drawing.Color.Black;
		this.gridBudget1.Location = new System.Drawing.Point(0, 60);
		this.gridBudget1.Name = "gridBudget1";
		this.gridBudget1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
		this.gridBudget1.ShowCursor = true;
		this.gridBudget1.ShowSort = false;
		this.gridBudget1.ShowToolTipOnNarrowColumn = true;
		this.gridBudget1.Size = new System.Drawing.Size(615, 102);
		this.gridBudget1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridBudget1.Styles"));
		this.gridBudget1.TabIndex = 5;
		this.gridBudget1.Tree.Column = 1;
		this.gridBudget1.Tree.LineColor = System.Drawing.Color.Gray;
		this.gridBudget1.Click += new System.EventHandler(gridBudget1_Click);
		appearance15.FontData.SizeInPoints = 11f;
		appearance15.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraStatusBar1.Appearance = appearance15;
		this.ultraStatusBar1.Location = new System.Drawing.Point(0, 162);
		this.ultraStatusBar1.Name = "ultraStatusBar1";
		appearance16.TextVAlign = Infragistics.Win.VAlign.Bottom;
		ultraStatusPanel3.Appearance = appearance16;
		ultraStatusPanel3.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel3.Key = "RowsCount";
		ultraStatusPanel3.Text = "資料筆數：";
		ultraStatusPanel3.Width = 200;
		ultraStatusPanel4.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel4.Key = "ProgressBar";
		ultraStatusPanel4.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		this.ultraStatusBar1.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[2] { ultraStatusPanel3, ultraStatusPanel4 });
		this.ultraStatusBar1.Size = new System.Drawing.Size(615, 26);
		this.ultraStatusBar1.TabIndex = 4;
		this.ultraStatusBar1.Text = "ultraStatusBar1";
		this.ultraStatusBar1.Visible = false;
		this.panel1.Controls.Add(this.lblProjectData);
		this.panel1.Controls.Add(this.ultraLabel10);
		this.panel1.Controls.Add(this.BtnSwitchProject);
		this.panel1.Controls.Add(this.ultraLabel1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(615, 30);
		this.panel1.TabIndex = 0;
		this.lblProjectData.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance17.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance17.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblProjectData.Appearance = appearance17;
		this.lblProjectData.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.lblProjectData.Font = new System.Drawing.Font("細明體", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblProjectData.Location = new System.Drawing.Point(80, 5);
		this.lblProjectData.Name = "lblProjectData";
		this.lblProjectData.Size = new System.Drawing.Size(418, 20);
		this.lblProjectData.TabIndex = 15;
		appearance18.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel10.Appearance = appearance18;
		this.ultraLabel10.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.ultraLabel10.Font = new System.Drawing.Font("細明體", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel10.Location = new System.Drawing.Point(6, 7);
		this.ultraLabel10.Name = "ultraLabel10";
		this.ultraLabel10.Size = new System.Drawing.Size(74, 19);
		this.ultraLabel10.TabIndex = 14;
		this.ultraLabel10.Text = "目前專案：";
		this.BtnSwitchProject.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance19.BackColor = System.Drawing.Color.Silver;
		appearance19.BackColor2 = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance19.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		appearance19.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.BtnSwitchProject.Appearance = appearance19;
		this.BtnSwitchProject.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.BtnSwitchProject.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.BtnSwitchProject.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance20.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance20.BackColor2 = System.Drawing.Color.White;
		appearance20.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.BtnSwitchProject.HotTrackAppearance = appearance20;
		this.BtnSwitchProject.HotTracking = true;
		this.BtnSwitchProject.Location = new System.Drawing.Point(520, 4);
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
		this.ultraLabel1.Size = new System.Drawing.Size(615, 30);
		this.ultraLabel1.TabIndex = 0;
		this.gridBudget2._ExcelFileName = "";
		this.gridBudget2._ExcelSheeName = "";
		this.gridBudget2._IsOpenExcelAfterExport = false;
		this.gridBudget2.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
		this.gridBudget2.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
		this.gridBudget2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridBudget2.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
		this.gridBudget2.ColumnInfo = resources.GetString("gridBudget2.ColumnInfo");
		this.ultraToolbarsManager1.SetContextMenuUltra(this.gridBudget2, "PopupMenu2");
		this.gridBudget2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridBudget2.ExtendLastCol = true;
		this.gridBudget2.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridBudget2.ForeColor = System.Drawing.Color.Black;
		this.gridBudget2.Location = new System.Drawing.Point(0, 30);
		this.gridBudget2.Name = "gridBudget2";
		this.gridBudget2.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
		this.gridBudget2.ShowCursor = true;
		this.gridBudget2.ShowSort = false;
		this.gridBudget2.ShowToolTipOnNarrowColumn = true;
		this.gridBudget2.Size = new System.Drawing.Size(615, 231);
		this.gridBudget2.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridBudget2.Styles"));
		this.gridBudget2.TabIndex = 6;
		this.gridBudget2.Tree.Column = 1;
		this.gridBudget2.Tree.LineColor = System.Drawing.Color.Gray;
		this.gridBudget2.Click += new System.EventHandler(gridBudget2_Click);
		this.gridBudget2.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(gridBudget2_StartEdit);
		this.gridBudget2.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(gridBudget2_AfterEdit);
		this.gridBudget2.MouseDown += new System.Windows.Forms.MouseEventHandler(gridBudget2_MouseDown);
		this.gridBudget2.Resize += new System.EventHandler(gridBudget2_Resize);
		this.gridBudget2.MouseMove += new System.Windows.Forms.MouseEventHandler(gridBudget2_MouseMove);
		appearance21.FontData.SizeInPoints = 11f;
		appearance21.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraStatusBar2.Appearance = appearance21;
		this.ultraStatusBar2.Location = new System.Drawing.Point(0, 289);
		this.ultraStatusBar2.Name = "ultraStatusBar2";
		appearance22.TextVAlign = Infragistics.Win.VAlign.Bottom;
		ultraStatusPanel5.Appearance = appearance22;
		ultraStatusPanel5.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel5.Key = "RowsCount";
		ultraStatusPanel5.Text = "資料筆數：";
		ultraStatusPanel5.Width = 200;
		ultraStatusPanel6.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel6.Key = "ProgressBar";
		ultraStatusPanel6.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		this.ultraStatusBar2.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[2] { ultraStatusPanel5, ultraStatusPanel6 });
		this.ultraStatusBar2.Size = new System.Drawing.Size(615, 26);
		this.ultraStatusBar2.TabIndex = 5;
		this.ultraStatusBar2.Text = "ultraStatusBar2";
		this.LeftPanel.Controls.Add(this.onlineList1);
		this.LeftPanel.Controls.Add(this.functionButtons1);
		this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
		this.LeftPanel.Location = new System.Drawing.Point(0, 52);
		this.LeftPanel.Name = "LeftPanel";
		this.LeftPanel.Size = new System.Drawing.Size(160, 511);
		this.LeftPanel.TabIndex = 1;
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
		this.functionButtons1.Size = new System.Drawing.Size(160, 511);
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
		this.pnl_spliter.Size = new System.Drawing.Size(7, 511);
		this.pnl_spliter.TabIndex = 3;
		appearance23.BorderColor = System.Drawing.Color.Transparent;
		appearance23.BorderColor3DBase = System.Drawing.Color.Transparent;
		appearance23.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance17.ImageBackground");
		this.Btn_Splt.Appearance = appearance23;
		this.Btn_Splt.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Borderless;
		this.Btn_Splt.Cursor = System.Windows.Forms.Cursors.Hand;
		this.Btn_Splt.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Btn_Splt.ImageSize = new System.Drawing.Size(7, 57);
		this.Btn_Splt.Location = new System.Drawing.Point(0, 212);
		this.Btn_Splt.Name = "Btn_Splt";
		this.Btn_Splt.ShapeImage = (System.Drawing.Image)resources.GetObject("Btn_Splt.ShapeImage");
		this.Btn_Splt.ShowFocusRect = false;
		this.Btn_Splt.ShowOutline = false;
		this.Btn_Splt.Size = new System.Drawing.Size(7, 76);
		this.Btn_Splt.TabIndex = 7;
		this.Btn_Splt.Click += new System.EventHandler(Btn_Splt_Click);
		this.ssp_Lower.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.ssp_Lower.Location = new System.Drawing.Point(0, 288);
		this.ssp_Lower.Name = "ssp_Lower";
		this.ssp_Lower.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Lower.OcxState");
		this.ssp_Lower.Size = new System.Drawing.Size(7, 220);
		this.ssp_Lower.TabIndex = 6;
		this.ssp_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.ssp_Bottom.Location = new System.Drawing.Point(0, 508);
		this.ssp_Bottom.Name = "ssp_Bottom";
		this.ssp_Bottom.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Bottom.OcxState");
		this.ssp_Bottom.Size = new System.Drawing.Size(7, 3);
		this.ssp_Bottom.TabIndex = 5;
		this.ssp_Upper.Dock = System.Windows.Forms.DockStyle.Top;
		this.ssp_Upper.Location = new System.Drawing.Point(0, 3);
		this.ssp_Upper.Name = "ssp_Upper";
		this.ssp_Upper.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Upper.OcxState");
		this.ssp_Upper.Size = new System.Drawing.Size(7, 209);
		this.ssp_Upper.TabIndex = 3;
		this.ssp_Top.Dock = System.Windows.Forms.DockStyle.Top;
		this.ssp_Top.Location = new System.Drawing.Point(0, 0);
		this.ssp_Top.Name = "ssp_Top";
		this.ssp_Top.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Top.OcxState");
		this.ssp_Top.Size = new System.Drawing.Size(7, 3);
		this.ssp_Top.TabIndex = 2;
		this.MainPanel.Controls.Add(this.panel6);
		this.MainPanel.Controls.Add(this.splitter1);
		this.MainPanel.Controls.Add(this.panel4);
		this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.MainPanel.Location = new System.Drawing.Point(167, 52);
		this.MainPanel.Name = "MainPanel";
		this.MainPanel.Size = new System.Drawing.Size(615, 511);
		this.MainPanel.TabIndex = 4;
		this.panel6.Controls.Add(this.gridBudget2);
		this.panel6.Controls.Add(this.panel2);
		this.panel6.Controls.Add(this.panel7);
		this.panel6.Controls.Add(this.ultraStatusBar2);
		this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel6.Location = new System.Drawing.Point(0, 196);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(615, 315);
		this.panel6.TabIndex = 15;
		this.panel2.Controls.Add(this.lblTotal);
		this.panel2.Controls.Add(this.ultraLabel2);
		this.panel2.Controls.Add(this.axSSPanel2);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 261);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(615, 28);
		this.panel2.TabIndex = 11;
		this.lblTotal.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance24.ForeColor = System.Drawing.Color.Blue;
		appearance24.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance24.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblTotal.Appearance = appearance24;
		this.lblTotal.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.lblTotal.Font = new System.Drawing.Font("Courier New", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblTotal.Location = new System.Drawing.Point(64, 5);
		this.lblTotal.Name = "lblTotal";
		this.lblTotal.Size = new System.Drawing.Size(460, 19);
		this.lblTotal.TabIndex = 14;
		appearance25.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		appearance25.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel2.Appearance = appearance25;
		this.ultraLabel2.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.ultraLabel2.Font = new System.Drawing.Font("Courier New", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.ultraLabel2.Location = new System.Drawing.Point(4, 5);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(74, 19);
		this.ultraLabel2.TabIndex = 13;
		this.ultraLabel2.Text = "總計：";
		this.axSSPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.axSSPanel2.Location = new System.Drawing.Point(0, 0);
		this.axSSPanel2.Name = "axSSPanel2";
		this.axSSPanel2.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("axSSPanel2.OcxState");
		this.axSSPanel2.Size = new System.Drawing.Size(615, 28);
		this.axSSPanel2.TabIndex = 1;
		this.panel7.Controls.Add(this.lblThisIssue);
		this.panel7.Controls.Add(this.ultraLabel7);
		this.panel7.Controls.Add(this.ultraLabel8);
		this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel7.Location = new System.Drawing.Point(0, 0);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(615, 30);
		this.panel7.TabIndex = 10;
		appearance26.ForeColor = System.Drawing.Color.White;
		appearance26.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblThisIssue.Appearance = appearance26;
		this.lblThisIssue.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.lblThisIssue.Font = new System.Drawing.Font("細明體", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblThisIssue.Location = new System.Drawing.Point(101, 7);
		this.lblThisIssue.Name = "lblThisIssue";
		this.lblThisIssue.Size = new System.Drawing.Size(224, 19);
		this.lblThisIssue.TabIndex = 17;
		this.lblThisIssue.Text = "【目前編輯次別：】";
		appearance27.ForeColor = System.Drawing.Color.White;
		appearance27.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel7.Appearance = appearance27;
		this.ultraLabel7.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel7.Font = new System.Drawing.Font("細明體", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel7.Location = new System.Drawing.Point(10, 7);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(94, 19);
		this.ultraLabel7.TabIndex = 16;
		this.ultraLabel7.Text = "變更項目明細";
		this.ultraLabel8.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel8.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.ultraLabel8.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ultraLabel8.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel8.Name = "ultraLabel8";
		this.ultraLabel8.Size = new System.Drawing.Size(615, 30);
		this.ultraLabel8.TabIndex = 0;
		this.splitter1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
		this.splitter1.Location = new System.Drawing.Point(0, 188);
		this.splitter1.Name = "splitter1";
		this.splitter1.Size = new System.Drawing.Size(615, 8);
		this.splitter1.TabIndex = 14;
		this.splitter1.TabStop = false;
		this.panel4.Controls.Add(this.gridBudget1);
		this.panel4.Controls.Add(this.panel5);
		this.panel4.Controls.Add(this.panel1);
		this.panel4.Controls.Add(this.ultraStatusBar1);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel4.Location = new System.Drawing.Point(0, 0);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(615, 188);
		this.panel4.TabIndex = 2;
		this.panel5.Controls.Add(this.ultraLabel5);
		this.panel5.Controls.Add(this.ultraLabel6);
		this.panel5.Controls.Add(this.ultraButton2);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel5.Location = new System.Drawing.Point(0, 30);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(615, 30);
		this.panel5.TabIndex = 7;
		appearance28.ForeColor = System.Drawing.Color.White;
		appearance28.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel5.Appearance = appearance28;
		this.ultraLabel5.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel5.Font = new System.Drawing.Font("細明體", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel5.Location = new System.Drawing.Point(8, 7);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(198, 19);
		this.ultraLabel5.TabIndex = 14;
		this.ultraLabel5.Text = "變更次別一覽表";
		this.ultraLabel6.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel6.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.ultraLabel6.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ultraLabel6.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(595, 30);
		this.ultraLabel6.TabIndex = 0;
		appearance29.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance29.ForeColor = System.Drawing.Color.White;
		this.ultraButton2.Appearance = appearance29;
		this.ultraButton2.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraButton2.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.ultraButton2.Dock = System.Windows.Forms.DockStyle.Right;
		this.ultraButton2.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ultraButton2.Location = new System.Drawing.Point(595, 0);
		this.ultraButton2.Name = "ultraButton2";
		this.ultraButton2.ShowFocusRect = false;
		this.ultraButton2.ShowOutline = false;
		this.ultraButton2.Size = new System.Drawing.Size(20, 30);
		this.ultraButton2.SupportThemes = false;
		this.ultraButton2.TabIndex = 15;
		this.ultraButton2.Text = "X";
		this.ultraButton2.Click += new System.EventHandler(ultraButton2_Click);
		appearance30.FontData.Name = "Arial";
		appearance30.FontData.SizeInPoints = 9f;
		this.ultraToolbarsManager1.Appearance = appearance30;
		appearance31.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance31.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.DockAreaAppearance = appearance31;
		this.ultraToolbarsManager1.DockWithinContainer = this;
		this.ultraToolbarsManager1.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraToolbarsManager1.LockToolbars = true;
		appearance32.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance32.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance32.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.MenuSettings.HotTrackAppearance = appearance32;
		appearance33.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance33.BackColor2 = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraToolbarsManager1.MenuSettings.IconAreaAppearance = appearance33;
		appearance34.BackColor = System.Drawing.Color.White;
		appearance34.BackColor2 = System.Drawing.Color.White;
		this.ultraToolbarsManager1.MenuSettings.ToolAppearance = appearance34;
		optionSet1.AllowAllUp = false;
		this.ultraToolbarsManager1.OptionSets.Add(optionSet1);
		this.ultraToolbarsManager1.ShowFullMenusDelay = 500;
		this.ultraToolbarsManager1.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2003;
		ultraToolbar1.DockedColumn = 0;
		ultraToolbar1.DockedRow = 1;
		ultraToolbar1.Settings.AllowCustomize = Infragistics.Win.DefaultableBoolean.False;
		ultraToolbar1.Settings.AllowDockTop = Infragistics.Win.DefaultableBoolean.True;
		ultraToolbar1.Text = "Tool1";
		buttonTool3.InstanceProps.IsFirstInGroup = true;
		buttonTool5.InstanceProps.IsFirstInGroup = true;
		buttonTool6.InstanceProps.IsFirstInGroup = true;
		labelTool1.InstanceProps.IsFirstInGroup = true;
		stateButtonTool1.Checked = true;
		labelTool2.InstanceProps.IsFirstInGroup = true;
		ultraToolbar1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[18]
		{
			buttonTool1, buttonTool2, buttonTool3, buttonTool4, buttonTool5, buttonTool6, labelTool1, stateButtonTool1, stateButtonTool2, stateButtonTool3,
			stateButtonTool4, stateButtonTool5, stateButtonTool6, stateButtonTool7, stateButtonTool8, labelTool2, comboBoxTool1, buttonTool7
		});
		ultraToolbar2.DockedColumn = 0;
		ultraToolbar2.DockedRow = 1;
		ultraToolbar2.Text = "Tool2";
		buttonTool9.InstanceProps.IsFirstInGroup = true;
		ultraToolbar2.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[4] { labelTool3, comboBoxTool2, buttonTool8, buttonTool9 });
		ultraToolbar2.Visible = false;
		ultraToolbar3.DockedColumn = 0;
		ultraToolbar3.DockedRow = 0;
		ultraToolbar3.IsMainMenuBar = true;
		ultraToolbar3.Text = "Menu1";
		ultraToolbar3.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[6] { popupMenuTool1, popupMenuTool2, popupMenuTool3, popupMenuTool4, popupMenuTool5, popupMenuTool6 });
		this.ultraToolbarsManager1.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[3] { ultraToolbar1, ultraToolbar2, ultraToolbar3 });
		appearance35.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance35.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.ToolbarSettings.Appearance = appearance35;
		appearance36.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance36.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance36.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.ToolbarSettings.HotTrackAppearance = appearance36;
		appearance37.Image = resources.GetObject("appearance23.Image");
		buttonTool10.SharedProps.AppearancesSmall.Appearance = appearance37;
		buttonTool10.SharedProps.Caption = "刪除變更次別...";
		buttonTool10.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		buttonTool10.SharedProps.Shortcut = System.Windows.Forms.Shortcut.Del;
		labelTool4.SharedProps.Caption = "尋找:";
		labelTool4.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		comboBoxTool3.DropDownStyle = Infragistics.Win.DropDownStyle.DropDown;
		comboBoxTool3.SharedProps.Caption = "輸入關鍵字";
		comboBoxTool3.SharedProps.Width = 200;
		comboBoxTool3.ValueList = valueList1;
		appearance38.Image = resources.GetObject("appearance24.Image");
		buttonTool11.SharedProps.AppearancesSmall.Appearance = appearance38;
		buttonTool11.SharedProps.Caption = "Go";
		popupMenuTool7.SharedProps.Caption = "右鍵功能表";
		buttonTool14.InstanceProps.IsFirstInGroup = true;
		popupMenuTool7.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[3] { buttonTool12, buttonTool13, buttonTool14 });
		appearance39.Image = resources.GetObject("appearance25.Image");
		buttonTool15.SharedProps.AppearancesSmall.Appearance = appearance39;
		buttonTool15.SharedProps.Caption = "新增變更次別...";
		buttonTool15.SharedProps.Category = "檔案";
		buttonTool15.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		appearance40.Image = resources.GetObject("appearance26.Image");
		buttonTool16.SharedProps.AppearancesSmall.Appearance = appearance40;
		buttonTool16.SharedProps.Caption = "編輯變更次別...";
		buttonTool16.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		buttonTool17.SharedProps.Caption = "變更項目明細";
		buttonTool17.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		labelTool5.SharedProps.Caption = "尋找:";
		labelTool5.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		comboBoxTool4.SharedProps.Caption = "輸入關鍵字";
		comboBoxTool4.SharedProps.Width = 200;
		comboBoxTool4.ValueList = valueList2;
		appearance41.Image = resources.GetObject("appearance27.Image");
		buttonTool18.SharedProps.AppearancesSmall.Appearance = appearance41;
		buttonTool18.SharedProps.Caption = "Go2";
		buttonTool19.SharedProps.Caption = "檢視變更次別列表";
		buttonTool19.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		popupMenuTool8.SharedProps.Caption = "檔案(&F)";
		popupMenuTool8.SharedProps.Category = "檔案";
		buttonTool20.InstanceProps.IsFirstInGroup = true;
		buttonTool22.InstanceProps.IsFirstInGroup = true;
		buttonTool24.InstanceProps.IsFirstInGroup = true;
		popupMenuTool8.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[5] { buttonTool20, buttonTool21, buttonTool22, buttonTool23, buttonTool24 });
		popupMenuTool9.SharedProps.Caption = "編輯(&E)";
		popupMenuTool9.SharedProps.Category = "編輯";
		buttonTool26.InstanceProps.IsFirstInGroup = true;
		buttonTool27.InstanceProps.IsFirstInGroup = true;
		popupMenuTool9.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[3] { buttonTool25, buttonTool26, buttonTool27 });
		popupMenuTool10.SharedProps.Caption = "檢視(&V)";
		popupMenuTool10.SharedProps.Category = "檢視";
		popupMenuTool10.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[2] { buttonTool28, buttonTool29 });
		popupMenuTool11.SharedProps.Caption = "明細表編輯(&D)";
		popupMenuTool11.SharedProps.Category = "明細表編輯";
		popupMenuTool12.InstanceProps.IsFirstInGroup = true;
		popupMenuTool13.InstanceProps.IsFirstInGroup = true;
		buttonTool32.InstanceProps.IsFirstInGroup = true;
		popupMenuTool11.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[5] { popupMenuTool12, buttonTool30, popupMenuTool13, buttonTool31, buttonTool32 });
		appearance42.Image = resources.GetObject("appearance28.Image");
		buttonTool33.SharedProps.AppearancesSmall.Appearance = appearance42;
		buttonTool33.SharedProps.Caption = "報表列印...";
		buttonTool33.SharedProps.Category = "檔案";
		buttonTool34.SharedProps.Caption = "結束契約變更";
		buttonTool34.SharedProps.Category = "檔案";
		appearance43.Image = resources.GetObject("appearance29.Image");
		buttonTool35.SharedProps.AppearancesSmall.Appearance = appearance43;
		buttonTool35.SharedProps.Caption = "編輯變更次別資料...";
		buttonTool35.SharedProps.Category = "編輯";
		buttonTool36.SharedProps.Caption = "隱藏變更次別一覽表";
		buttonTool36.SharedProps.Category = "檢視";
		appearance44.Image = resources.GetObject("appearance30.Image");
		buttonTool37.SharedProps.AppearancesSmall.Appearance = appearance44;
		buttonTool37.SharedProps.Caption = "編輯項目";
		buttonTool37.SharedProps.Category = "明細表編輯";
		appearance45.Image = resources.GetObject("appearance31.Image");
		buttonTool38.SharedProps.AppearancesSmall.Appearance = appearance45;
		buttonTool38.SharedProps.Caption = "刪除項目";
		buttonTool38.SharedProps.Category = "明細表編輯";
		popupMenuTool14.SharedProps.Caption = "插入項目";
		popupMenuTool14.SharedProps.Category = "明細表編輯";
		popupMenuTool14.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[2] { buttonTool39, buttonTool40 });
		appearance46.Image = resources.GetObject("appearance32.Image");
		buttonTool41.SharedProps.AppearancesSmall.Appearance = appearance46;
		buttonTool41.SharedProps.Caption = "新增項目";
		buttonTool41.SharedProps.Category = "明細表編輯";
		buttonTool42.SharedProps.Caption = "子階項目";
		buttonTool42.SharedProps.Category = "明細表編輯";
		appearance47.Image = resources.GetObject("appearance33.Image");
		buttonTool43.SharedProps.AppearancesSmall.Appearance = appearance47;
		buttonTool43.SharedProps.Caption = "重新總計";
		buttonTool43.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		popupMenuTool15.SharedProps.Caption = "PopupMenu2";
		popupMenuTool17.InstanceProps.IsFirstInGroup = true;
		buttonTool45.InstanceProps.IsFirstInGroup = true;
		popupMenuTool15.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[4] { popupMenuTool16, buttonTool44, popupMenuTool17, buttonTool45 });
		popupMenuTool18.SharedProps.Caption = "說明(&H)";
		popupMenuTool18.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[1] { buttonTool46 });
		buttonTool47.SharedProps.Caption = "關於PCCES...";
		popupMenuTool19.SharedProps.Caption = "插入主項大類";
		popupMenuTool19.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[2] { buttonTool48, buttonTool49 });
		buttonTool50.SharedProps.Caption = "插入同階項目";
		buttonTool51.SharedProps.Caption = "插入子階項目";
		appearance48.Image = resources.GetObject("appearance34.Image");
		buttonTool52.SharedProps.AppearancesSmall.Appearance = appearance48;
		buttonTool52.SharedProps.Caption = "編輯主項大類...";
		popupMenuTool20.SharedProps.Caption = "插入工作要項";
		popupMenuTool20.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[1] { buttonTool53 });
		buttonTool54.SharedProps.Caption = "自基本資料庫挑選工項";
		appearance49.Image = resources.GetObject("appearance35.Image");
		buttonTool55.SharedProps.AppearancesSmall.Appearance = appearance49;
		buttonTool55.SharedProps.Caption = "刪除";
		popupMenuTool21.SharedProps.Caption = "工具(&T)";
		buttonTool58.InstanceProps.IsFirstInGroup = true;
		popupMenuTool21.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[3] { buttonTool56, buttonTool57, buttonTool58 });
		appearance50.Image = resources.GetObject("appearance36.Image");
		buttonTool59.SharedProps.AppearancesSmall.Appearance = appearance50;
		buttonTool59.SharedProps.Caption = "計算機";
		appearance51.Image = resources.GetObject("appearance37.Image");
		buttonTool60.SharedProps.AppearancesSmall.Appearance = appearance51;
		buttonTool60.SharedProps.Caption = "項次重整";
		buttonTool61.SharedProps.Caption = "項次編號設定...";
		buttonTool62.SharedProps.Caption = "契約變更匯出...";
		buttonTool63.SharedProps.Caption = "契約變更匯入...";
		appearance52.Image = resources.GetObject("appearance38.Image");
		buttonTool64.SharedProps.AppearancesSmall.Appearance = appearance52;
		buttonTool64.SharedProps.Caption = "電子檔製作...";
		buttonTool64.SharedProps.Category = "檔案";
		buttonTool65.SharedProps.Caption = "專案工項維護";
		buttonTool65.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		labelTool6.SharedProps.Caption = "階層:";
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
		this.ultraToolbarsManager1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[53]
		{
			buttonTool10, labelTool4, comboBoxTool3, buttonTool11, popupMenuTool7, buttonTool15, buttonTool16, buttonTool17, labelTool5, comboBoxTool4,
			buttonTool18, buttonTool19, popupMenuTool8, popupMenuTool9, popupMenuTool10, popupMenuTool11, buttonTool33, buttonTool34, buttonTool35, buttonTool36,
			buttonTool37, buttonTool38, popupMenuTool14, buttonTool41, buttonTool42, buttonTool43, popupMenuTool15, popupMenuTool18, buttonTool47, popupMenuTool19,
			buttonTool50, buttonTool51, buttonTool52, popupMenuTool20, buttonTool54, buttonTool55, popupMenuTool21, buttonTool59, buttonTool60, buttonTool61,
			buttonTool62, buttonTool63, buttonTool64, buttonTool65, labelTool6, stateButtonTool9, stateButtonTool10, stateButtonTool11, stateButtonTool12, stateButtonTool13,
			stateButtonTool14, stateButtonTool15, stateButtonTool16
		});
		this.ultraToolbarsManager1.BeforeToolbarListDropdown += new Infragistics.Win.UltraWinToolbars.BeforeToolbarListDropdownEventHandler(ultraToolbarsManager1_BeforeToolbarListDropdown);
		this.ultraToolbarsManager1.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(ultraToolbarsManager1_ToolClick);
		this.ultraToolbarsManager1.AfterToolDeactivate += new Infragistics.Win.UltraWinToolbars.ToolEventHandler(ultraToolbarsManager1_AfterToolDeactivate);
		this.ultraToolbarsManager1.AfterToolActivate += new Infragistics.Win.UltraWinToolbars.ToolEventHandler(ultraToolbarsManager1_AfterToolActivate);
		this._FormSys_B_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
		this._FormSys_B_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
		this._FormSys_B_Toolbars_Dock_Area_Top.Name = "_FormSys_B_Toolbars_Dock_Area_Top";
		this._FormSys_B_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(782, 52);
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
		this._FormSys_B_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 52);
		this._FormSys_B_Toolbars_Dock_Area_Left.Name = "_FormSys_B_Toolbars_Dock_Area_Left";
		this._FormSys_B_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 511);
		this._FormSys_B_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
		this._FormSys_B_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(782, 52);
		this._FormSys_B_Toolbars_Dock_Area_Right.Name = "_FormSys_B_Toolbars_Dock_Area_Right";
		this._FormSys_B_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 511);
		this._FormSys_B_Toolbars_Dock_Area_Right.ToolbarsManager = this.ultraToolbarsManager1;
		this.iglst_splt_Btn.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("iglst_splt_Btn.ImageStream");
		this.iglst_splt_Btn.TransparentColor = System.Drawing.Color.Transparent;
		this.iglst_splt_Btn.Images.SetKeyName(0, "");
		this.iglst_splt_Btn.Images.SetKeyName(1, "");
		this.iglst_splt_Btn.Images.SetKeyName(2, "");
		this.iglst_splt_Btn.Images.SetKeyName(3, "");
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.White;
		this.imageList2.Images.SetKeyName(0, "");
		this.imageList2.Images.SetKeyName(1, "");
		this.imageList2.Images.SetKeyName(2, "");
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		base.ClientSize = new System.Drawing.Size(782, 563);
		base.Controls.Add(this.MainPanel);
		base.Controls.Add(this.pnl_spliter);
		base.Controls.Add(this.LeftPanel);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Right);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Left);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Top);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Bottom);
		base.KeyPreview = true;
		base.Name = "FormBudgetChange";
		this.Text = "契約變更";
		base.Load += new System.EventHandler(FormBudgetChange_Load);
		base.SizeChanged += new System.EventHandler(FormBudgetChange_SizeChanged);
		((System.ComponentModel.ISupportInitialize)this.gridBudget1).EndInit();
		this.panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridBudget2).EndInit();
		this.LeftPanel.ResumeLayout(false);
		this.LeftPanel.PerformLayout();
		this.pnl_spliter.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ssp_Lower).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Bottom).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Upper).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Top).EndInit();
		this.MainPanel.ResumeLayout(false);
		this.panel6.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.axSSPanel2).EndInit();
		this.panel7.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
		this.panel5.ResumeLayout(false);
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

	public FormBudgetChange()
	{
		InitializeComponent();
		CellStyle csCb = gridBudget2.Styles.Add("ComboList");
		csCb.DataType = typeof(string);
		csCb.ComboList = "警告但可存檔|警告且不可存檔|略過";
		csCb.ForeColor = Color.Navy;
		csCb.TextAlign = TextAlignEnum.LeftCenter;
		csCb.Font = new Font(Font, FontStyle.Bold);
		GridCols = gridBudget2.Cols.Count;
		GridColsSquence = new object[GridCols, 8];
		HideCols(IsHide: true);
		CellStyle cs = gridBudget2.Styles.Add("img");
		cs.DataType = typeof(Image);
		CellStyle cs2 = gridBudget2.Styles.Add("EditMode");
		cs2.DataType = typeof(Image);
		cs2.ImageAlign = ImageAlignEnum.RightCenter;
		GridCols = gridBudget2.Cols.Count;
		GridColsSquence = new object[GridCols, 8];
		RememberColsProps();
	}

	private void HideCols(bool IsHide)
	{
		if (IsHide)
		{
			gridBudget1.Cols["chgDate"].Visible = false;
			gridBudget1.Cols["chgTxtNo"].Visible = false;
			gridBudget1.Cols["explain"].Visible = false;
			gridBudget1.Cols["content"].Visible = false;
			gridBudget1.Cols["extendDay"].Visible = false;
			gridBudget1.Cols["chgFinish"].Visible = false;
			gridBudget2.Cols["Lock"].Visible = false;
			gridBudget2.Cols["LevelNo"].Visible = false;
			gridBudget2.Cols["Kind"].Visible = false;
			gridBudget2.Cols["Analysis"].Visible = false;
			gridBudget2.Cols["SNo"].Visible = false;
			gridBudget2.Cols["Formula"].Visible = false;
			gridBudget2.Cols["PrintNo"].Visible = false;
			gridBudget2.Cols["PubCode"].Visible = false;
			gridBudget2.Cols["IsShared"].Visible = false;
			gridBudget2.Cols["OldPrintNo"].Visible = false;
			gridBudget2.Cols["PrintToAnalysis"].Visible = false;
		}
	}

	private void SettingDecimal()
	{
		DataTable DTDecimal = new DataTable();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add(CommonMethods.GetFormTypeTitle(FormType.Budget));
		Archnowledge.Pcces.BUDClass.PubDecimal dbDecimal = new Archnowledge.Pcces.BUDClass.PubDecimal(aArr);
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
			F_MainQty = 3;
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
			GridColsSquence[i, 0] = gridBudget2.Cols[i].Name;
			GridColsSquence[i, 1] = gridBudget2.Cols[i].Caption;
			GridColsSquence[i, 2] = gridBudget2.Cols[i].Width;
			if (gridBudget2.Cols[i].Name == "AnaImg")
			{
				GridColsSquence[i, 3] = typeof(Image);
			}
			else
			{
				GridColsSquence[i, 3] = gridBudget2.Cols[i].DataType;
			}
			GridColsSquence[i, 4] = gridBudget2.Cols[i].Visible;
			GridColsSquence[i, 5] = gridBudget2.Cols[i].Format;
			GridColsSquence[i, 6] = gridBudget2.Cols[i].AllowEditing;
			if (gridBudget2.Cols[i].Name == "Qty")
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
			if (gridBudget2.Cols[i].Name == "Cost")
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
			if (gridBudget2.Cols[i].Name == "Amount")
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
			if (gridBudget2.Cols[i].Name == "ChgQty")
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
			if (gridBudget2.Cols[i].Name == "ChgCost")
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
			if (gridBudget2.Cols[i].Name == "ChgAmount")
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
			GridColsSquence[i, 7] = gridBudget2.Cols[i].TextAlign;
		}
	}

	private void SetGridColumn()
	{
		for (int i = 0; i < GridCols; i++)
		{
			gridBudget2.Cols[i].Name = (string)GridColsSquence[i, 0];
			gridBudget2.Cols[i].Caption = (string)GridColsSquence[i, 1];
			gridBudget2.Cols[i].Width = (int)GridColsSquence[i, 2];
			gridBudget2.Cols[i].DataType = (Type)GridColsSquence[i, 3];
			gridBudget2.Cols[i].Visible = (bool)GridColsSquence[i, 4];
			gridBudget2.Cols[i].Format = (string)GridColsSquence[i, 5];
			gridBudget2.Cols[i].AllowEditing = (bool)GridColsSquence[i, 6];
			gridBudget2.Cols[i].TextAlign = (TextAlignEnum)GridColsSquence[i, 7];
		}
	}

	private void FormBudgetChange_SizeChanged(object sender, EventArgs e)
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

	private void FormBudgetChange_Load(object sender, EventArgs e)
	{
		Firstflag = "FIRST";
		SettingDecimal();
		base.ParentForm.Text = "PCCES Win 4.3 【契約變更】";
		FormBudgetChange_SizeChanged(null, null);
		functionButtons1._UserID = F_UserID;
		functionButtons1._UserName = F_UserName;
		functionButtons1._ServerName = F_ServerName;
		functionButtons1._CurrOpenMode = FunctionOpenMode.Invoice;
		functionButtons1._ActiveFunction = "BDGT_CHANGE";
		onlineList1._UserID = F_UserID;
		onlineList1._UserName = F_UserName;
		onlineList1._ServerName = F_ServerName;
		onlineList1._FunctionName = F_FunctionName;
		onlineList1._HasRegistered = F_HasRegistered;
		onlineList1.Connect();
		SysUser oSysUser = new SysUser();
		ultraStatusBar2.Panels[1].Text = "目前資料庫：" + oSysUser.GetSysUserDatabaseDesc(F_UserID);
		F_CurrentDBName = oSysUser.GetSysUserDatabaseName(F_UserID);
		LoadData();
		BindToGrid();
		Do_Change();
		lblProjectData.Text = "【" + F_ProjectCode + "】" + F_ProjectNameC;
	}

	private void LoadData()
	{
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(LET_CHG_SHOW) 顯示預算變更主檔");
		PubTools.WriteRoughlyLog(tmp_AL1);
		sub_ChgMain chgcom = new sub_ChgMain(tmp_AL1);
		DT1 = chgcom.ListItem("", F_ProjectCode, F_SubProjectCode);
	}

	private void BindToGrid()
	{
		gridBudget1.Rows.Count = DT1.Rows.Count + 1;
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("顯示預算變更主檔");
		if (dbItemA != null)
		{
			dbItemA = null;
		}
		if (dbItemA == null)
		{
			dbItemA = new ItemA(tmp_AL1);
		}
		dbItemA.ps_projectCode = F_ProjectCode;
		sub_ChgMain chgcom = new sub_ChgMain(tmp_AL1);
		chgcom.ps_projectCode = F_ProjectCode;
		chgcom.ps_sproj = "";
		for (int i = 0; i < DT1.Rows.Count; i++)
		{
			gridBudget1[i + 1, "chgCount"] = DT1.Rows[i]["chgCount"].ToString().Trim();
			gridBudget1[i + 1, "chgAgree"] = PubTools.Str2DateTime(DT1.Rows[i]["chgAgree"].ToString());
			gridBudget1[i + 1, "keyNote"] = DT1.Rows[i]["keyNote"].ToString().Trim();
			gridBudget1[i + 1, "chgDate"] = PubTools.Str2DateTime(DT1.Rows[i]["chgDate"].ToString());
			gridBudget1[i + 1, "chgTxtNo"] = DT1.Rows[i]["chgTxtNo"].ToString().Trim();
			gridBudget1[i + 1, "explain"] = DT1.Rows[i]["explain"].ToString().Trim();
			gridBudget1[i + 1, "content"] = DT1.Rows[i]["content"].ToString().Trim();
			gridBudget1[i + 1, "extendDay"] = DT1.Rows[i]["extendDay"].ToString().Trim();
			gridBudget1[i + 1, "chgFinish"] = PubTools.Str2DateTime(DT1.Rows[i]["chgFinish"].ToString());
			if (DT1.Rows[i]["chgCount"].ToString().Trim() == "1")
			{
				dbItemA.ps_srckind = "SUB";
			}
			else
			{
				dbItemA.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
				dbItemA.ps_Issue = (PubTools.Str2Int(DT1.Rows[i]["chgCount"].ToString().Trim()) - 1).ToString();
			}
			gridBudget1[i + 1, "preAmt"] = dbItemA.GetAmount(F_ProjectCode);
			chgcom.ps_chgCount = DT1.Rows[i]["chgCount"].ToString().Trim();
			chgcom.ps_preAmt = gridBudget1[i + 1, "preAmt"].ToString();
			dbItemA.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
			dbItemA.ps_Issue = DT1.Rows[i]["chgCount"].ToString().Trim();
			gridBudget1[i + 1, "postAmt"] = dbItemA.GetAmount(F_ProjectCode);
			chgcom.ps_postAmt = gridBudget1[i + 1, "postAmt"].ToString();
			chgcom.UpdItem();
		}
		ultraStatusBar1.Panels[0].Text = "資料筆數：" + DT1.Rows.Count;
		IssueModeCheck();
	}

	private void IssueModeCheck()
	{
		if (gridBudget1.Rows.Count <= 1)
		{
			ultraToolbarsManager1.Tools["mnuEdit"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuDelete"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuPrint"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuReCal"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuPopIns"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuEDEdtItem"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuEDDelItem"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuFile_Digital"].SharedProps.Enabled = false;
		}
		else
		{
			ultraToolbarsManager1.Tools["mnuEdit"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuDelete"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuPrint"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuReCal"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuPopIns"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuEDEdtItem"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuEDDelItem"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuFile_Digital"].SharedProps.Enabled = true;
		}
		if (PubTools.Str2Int(F_chgCount) != iCountNum)
		{
			ultraToolbarsManager1.Tools["mnuReCal"].SharedProps.Enabled = false;
		}
	}

	private void ultraToolbarsManager1_ToolClick(object sender, ToolClickEventArgs e)
	{
		Do_MenuAction(e.Tool.Key);
	}

	private void Do_MenuAction(string menuKey)
	{
		switch (menuKey)
		{
		case "mnu_Go":
			Do_ToolBarFind();
			break;
		case "mnuPrint":
			if (!DBClass.ChkAuthority(F_UserID, "F01100010001"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F01100010001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Execute_Print();
			}
			break;
		case "mnuExit":
			if (!DBClass.ChkAuthority(F_UserID, "F01100010002"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F01100010002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				CloseThisForm();
			}
			break;
		case "mnuAddNew":
			if (!DBClass.ChkAuthority(F_UserID, "F01100020001"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F01100020001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				ExecuteAddNew("NEW");
			}
			break;
		case "mnuEdit":
			if (!DBClass.ChkAuthority(F_UserID, "F01100020002"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F01100020002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				ExecuteAddNew("EDIT");
			}
			break;
		case "mnuDelete":
			if (!DBClass.ChkAuthority(F_UserID, "F01100020003"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F01100020003") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Do_Delete();
			}
			break;
		case "mnuChange":
			Do_Change();
			break;
		case "mnuViewHide1":
			if (!DBClass.ChkAuthority(F_UserID, "F01100030001"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F01100030001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Do_ShowList();
			}
			break;
		case "mnuSibling":
			if (!DBClass.ChkAuthority(F_UserID, "F011000400010001"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F011000400010001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				InsSibling();
			}
			break;
		case "mnuChild":
			if (!DBClass.ChkAuthority(F_UserID, "F011000400010002"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F011000400010002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				InsChild();
			}
			break;
		case "mnuEDDelItem":
			if (!DBClass.ChkAuthority(F_UserID, "F01100040003"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F01100040003") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				ItemDelete();
			}
			break;
		case "mnuEDEdtItem":
			if (!DBClass.ChkAuthority(F_UserID, "F01100040002"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F01100040002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				ItemEdit();
			}
			break;
		case "mnuReCal":
			if (!DBClass.ChkAuthority(F_UserID, "F01100040004"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F01100040004") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Do_ReCal();
			}
			break;
		case "mnuAbout":
			if (!DBClass.ChkAuthority(F_UserID, "F01100050001"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F01100050001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Execute_About();
			}
			break;
		case "PopMnuMainSibling":
			DoInsertMainItems("SIBILING");
			break;
		case "PopMnuMainChild":
			DoInsertMainItems("CHILD");
			break;
		case "mnuEditMain":
			EditItemsByKind();
			break;
		case "PopMnuPickWK_Mrs":
			ExecutePickFromMrs();
			break;
		case "PopMenu1_Delete":
			Delete_BDGT_Item();
			break;
		case "mnuCalcu":
			Execute_Calculator();
			break;
		case "mnuOption":
			Execute_Option();
			break;
		case "mnuTool_ItemReArrange":
			Do_ItemReArrange();
			break;
		case "mnuExport":
			Execute_Export();
			break;
		case "mnuImport":
			Do_Import();
			break;
		case "mnuFile_Digital":
			Do_FileDigital("");
			break;
		case "mnuViewRes":
			ExecuteResForm();
			break;
		case "mnuLevel_1":
			gridBudget2.Tree.Show(1);
			break;
		case "mnuLevel_2":
			gridBudget2.Tree.Show(2);
			break;
		case "mnuLevel_3":
			gridBudget2.Tree.Show(3);
			break;
		case "mnuLevel_4":
			gridBudget2.Tree.Show(4);
			break;
		case "mnuLevel_5":
			gridBudget2.Tree.Show(5);
			break;
		case "mnuLevel_6":
			gridBudget2.Tree.Show(6);
			break;
		case "mnuLevel_7":
			gridBudget2.Tree.Show(7);
			break;
		case "mnuLevel_8":
			gridBudget2.Tree.Show(8);
			break;
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
		PROJ.ps_chgCount = F_chgCount;
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
		FM_BDGT_EXP_WZD._chgCount = F_chgCount;
		FM_BDGT_EXP_WZD._ProjectCode = F_ProjectCode;
		FM_BDGT_EXP_WZD._DeptName = sDeptName;
		FM_BDGT_EXP_WZD._DeptEName = sDeptEName;
		FM_BDGT_EXP_WZD._ProjectNameC = DT2.Rows[0]["projectNameC"].ToString().Trim();
		FM_BDGT_EXP_WZD._ProjectNameE = DT2.Rows[0]["projectNameE"].ToString().Trim();
		FM_BDGT_EXP_WZD._ProjectAddress = DT1.Rows[0]["projectAddress"].ToString().Trim();
		FM_BDGT_EXP_WZD._ProjectEngAddress = "";
		FM_BDGT_EXP_WZD._AccountCode1 = "";
		FM_BDGT_EXP_WZD._AccountCode2 = "";
		FM_BDGT_EXP_WZD._ProjFLAG = sFLAG.Trim();
		if (sFLAG == "Z14AC1100" && !PROJ.ChkPostMode(F_ProjectCode))
		{
			MessageBox.Show(this, "專案中，使用到的工作要項中，有尚未核可的項目，\n請先返回基本資料庫維護，將使用到的項目[核可]。\n目前不能執行電子檔匯出。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		FM_BDGT_EXP_WZD.ShowDialog(this);
		DBCLS = null;
	}

	private void ExecuteResForm()
	{
		FormBudgetRes FM_BDGT_RES = new FormBudgetRes();
		FM_BDGT_RES._UserID = F_UserID;
		FM_BDGT_RES._ActionName = F_ActionName;
		FM_BDGT_RES._ProjectCode = F_ProjectCode;
		FM_BDGT_RES._chgCount = F_chgCount;
		FM_BDGT_RES._CurrentDBName = F_CurrentDBName;
		FM_BDGT_RES.Owner = this;
		if (FM_BDGT_RES.ShowDialog() == DialogResult.OK)
		{
		}
		if (F_IsNeedToReloadAllData)
		{
		}
		FM_BDGT_RES.Close();
		FM_BDGT_RES.Dispose();
		FM_BDGT_RES = null;
	}

	private void Execute_Option()
	{
		FormBudgetItemNo FM_BDGT_ITMNO = new FormBudgetItemNo();
		FM_BDGT_ITMNO._ActionName = F_ActionName;
		FM_BDGT_ITMNO._UserID = F_UserID;
		FM_BDGT_ITMNO._ProjectCode = F_ProjectCode;
		FM_BDGT_ITMNO.ShowDialog(this);
		FM_BDGT_ITMNO.Close();
		FM_BDGT_ITMNO.Dispose();
		FM_BDGT_ITMNO = null;
	}

	private void Execute_Export()
	{
		string sFilter = "CHG files (*.chg)|*.chg";
		string sName = "";
		saveFileDialog1.Filter = sFilter;
		saveFileDialog1.RestoreDirectory = true;
		if (saveFileDialog1.ShowDialog() == DialogResult.OK)
		{
			sName = saveFileDialog1.FileName;
		}
		if (sName == "")
		{
			string sWarning = "請先給定檔案名稱";
			MessageBox.Show(this, sWarning, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else
		{
			Exp_Bud_XML(flag: false, sName);
		}
	}

	private void Do_Import()
	{
		string sName = "";
		string F_NewProjectCode = "";
		openFileDialog1.RestoreDirectory = true;
		openFileDialog1.Filter = "電子標單檔 chg 格式(*.chg)|*.chg";
		if (openFileDialog1.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		sName = openFileDialog1.FileName;
		FormSys_G_Info1 FM_INFO = new FormSys_G_Info1();
		FM_INFO._InfoString = "【契約變更】載入中，請稍候! ";
		FM_INFO.Show();
		Application.DoEvents();
		Cursor = Cursors.WaitCursor;
		if (sName == "")
		{
			return;
		}
		MyZip MyZip1 = new MyZip();
		MyZip1.Open(sName, "ARCH13139409");
		FileList[] sAcc = MyZip1.GetFileList();
		MyZip1.Extract(Application.StartupPath + "\\Report\\");
		if (sAcc.Length <= 0)
		{
			MessageBox.Show(this, "電子檔損毀，請檢查後再執行匯入!", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		if (sAcc[0].FileName.ToUpper().IndexOf(".MDB") <= 0)
		{
			MessageBox.Show(this, "電子內容有誤，請檢查後再執行匯入!", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		Application.DoEvents();
		string sPath = CommonMethods.ExtractFilePath(Application.StartupPath + "\\Report\\");
		string sFileName = sPath + CommonMethods.ExtractFileName(sAcc[0].FileName.Trim());
		string sKey = "";
		string ls_IsCheckOutFile = "N";
		string XML_MODE = "XM1";
		if (sFileName.Length >= 4)
		{
			string Str1 = CommonMethods.ExtractFileNoExtName(sFileName);
			sKey = ((Str1.Length < 4) ? Str1 : Str1.Substring(Str1.Length - 4));
		}
		DataSet DS1 = CommonMethods.ImportAccess(sFileName);
		if (DS1.Tables["Project"].Columns.IndexOf("CloseBidDate") < 0)
		{
			DS1.Tables["Project"].Columns.Add("CloseBidDate", Type.GetType("System.DateTime"));
			DS1.Tables["Project"].Rows[0]["CloseBidDate"] = Convert.ToDateTime("1800/1/1");
		}
		if (DS1.Tables["Project"].Columns.IndexOf("CheckOut") < 0)
		{
			DS1.Tables["Project"].Columns.Add("CheckOut", Type.GetType("System.String"));
			DS1.Tables["Project"].Rows[0]["CheckOut"] = "N";
		}
		if (DS1.Tables["Project"].Rows[0]["CheckOut"].ToString().ToUpper() == "CKOUT")
		{
			ls_IsCheckOutFile = "Y";
		}
		string ssKey2 = "";
		try
		{
			ssKey2 = DS1.Tables["Project"].Rows[0]["srcKind"].ToString().ToUpper();
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Project.formNewProjectWizard.cs" + ex.Message);
			ssKey2 = sKey;
		}
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add("WinFORM 基本工料");
		Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
		PROJ.ps_srckind = "SUBCHG";
		ssKey2 = "SUBCHG";
		Application.DoEvents();
		F_NewProjectCode = DS1.Tables["Project"].Rows[0]["projectCode"].ToString().Trim();
		if (F_ProjectCode != F_NewProjectCode)
		{
			FM_INFO.Close();
			FM_INFO.Dispose();
			Cursor = Cursors.Default;
			MessageBox.Show(this, "不同專案無法匯入", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		string sRet = PROJ.InputACCESS(DS1, XML_MODE);
		Application.DoEvents();
		switch (sRet)
		{
		case "F1":
			FM_INFO.Close();
			FM_INFO.Dispose();
			Cursor = Cursors.Default;
			MessageBox.Show(this, "請先匯入契約編制專案", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		case "F2":
			FM_INFO.Close();
			FM_INFO.Dispose();
			Cursor = Cursors.Default;
			MessageBox.Show(this, "請先匯入契約變更前次的版本", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		case "F":
			FM_INFO.Close();
			FM_INFO.Dispose();
			Cursor = Cursors.Default;
			if (MessageBox.Show(this, "有相同版本的專案存在，是否刪除?", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				string ls_projectcode = DS1.Tables["Project"].Rows[0]["projectcode"].ToString();
				PROJ.DeleProjSub(ls_projectcode, flag: true);
				LoadData();
				BindToGrid();
				Do_Change();
			}
			return;
		}
		if (sRet.IndexOf("\\n") > -1)
		{
			sRet = sRet.Substring(0, sRet.IndexOf("\\n")) + "\n\n" + sRet.Substring(sRet.IndexOf("\\n") + 2);
		}
		if (sRet.IndexOf("【（") <= -1)
		{
			FM_INFO.Close();
			FM_INFO.Dispose();
			Cursor = Cursors.Default;
			MessageBox.Show(this, " 轉入成功!", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			LoadData();
			BindToGrid();
			Do_Change();
			return;
		}
		if (sRet.Trim() == "編碼錯誤！無法轉入！")
		{
			MessageBox.Show(this, sRet.Trim(), "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			Cursor = Cursors.Default;
			return;
		}
		if (sRet.Trim() == "無工程代碼！無法轉入！")
		{
			MessageBox.Show(this, sRet.Trim(), "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			Cursor = Cursors.Default;
			return;
		}
		string MessageBUD = ((sRet.Trim() == "") ? "\n 預算轉入成功!" : "\n 轉入成功!");
		string MessageBID = ((sRet.Trim() == "") ? "\n 執行預算轉入成功!" : "\n 執行預算轉入成功!");
		if (ssKey2.ToUpper() == "BUD")
		{
			MessageBox.Show(this, sRet.Trim() + MessageBUD, "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		else
		{
			MessageBox.Show(this, sRet.Trim() + MessageBID, "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		Cursor = Cursors.Default;
		FM_INFO.Close();
		FM_INFO.Dispose();
		int iPos1 = sRet.IndexOf("（");
		int iPos2 = sRet.IndexOf("）");
		string NewProjCode = sRet.Substring(iPos1 + 1, iPos2 - iPos1 - 1);
		F_NewProjectCode = NewProjCode;
		if (!(ls_IsCheckOutFile == "Y"))
		{
			return;
		}
		if (ssKey2.ToUpper() == "BID")
		{
			try
			{
				DBClass DBCLS = new DBClass();
				DBCLS._FS_UserID = F_UserID;
				DBCLS.ExecuteCommand("Update bidProject set CloseBidDate = null Where projectCode='" + F_NewProjectCode + "'");
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "Project.formNewProjectWizard.cs" + ex.Message);
				Console.Write(ex.Message);
			}
		}
		MessageBox.Show(this, "注意\n\n此電子檔是【簽出/簽入】專用\n非一般標準電子檔，\n應用上請特別留意。", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	private void Exp_Bud_XML(bool flag, string sName)
	{
		Cursor = Cursors.WaitCursor;
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add("契約變更 ACCESS 轉出");
		Archnowledge.Pcces.BUDClass.Project projcom = new Archnowledge.Pcces.BUDClass.Project(aArr);
		projcom.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		projcom.ps_ShowCost = "1";
		projcom.ps_ShowAnalysis = "1";
		string ls_ChgCount = gridBudget1[gridBudget1.Row, "chgCount"].ToString();
		projcom.ps_chgCount = ls_ChgCount;
		projcom.ps_srckind = "SUBCHG";
		DataSet lac_temp = projcom.OutputXML(F_ProjectCode, "XM1");
		projcom = null;
		if (flag)
		{
			PubTools.WriteRoughlyLog(aArr);
		}
		else
		{
			string MDBFile = Application.StartupPath + "\\Report\\" + sName;
			string MDBPath = Application.StartupPath + "\\Report\\";
			string GUIDCode = Guid.NewGuid().ToString();
			CommonMethods.CreateReport(lac_temp, sName, MDBPath + GUIDCode + ".mdb", MDBPath);
			MyZip MyZip1 = new MyZip();
			MyZip1.AddFiles(sName, new string[1] { MDBPath + GUIDCode + ".mdb" }, "ARCH13139409");
			GC.Collect();
		}
		Cursor = Cursors.Default;
	}

	public DataTable FixPubCode()
	{
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = F_UserID;
		SysUser oSysUser = new SysUser();
		string ssDBName = oSysUser.GetSysUserDatabaseName(F_UserID);
		string sSQL = "Select PccesCode, PubCode From " + CommonMethods.GetActionNameString(F_ActionName) + "ProjMrsA Where ProjectCode='" + F_ProjectCode + "' ";
		DataTable DT_Process = DBCLS.GetUserDefine(sSQL);
		return FixPubCode(DT_Process);
	}

	public DataTable FixPubCode(DataTable srcDT)
	{
		DateTime T1 = DateTime.Now;
		DataTable srcDT2 = srcDT.Copy();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("單筆引用單價");
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = F_UserID;
		SysUser oSysUser = new SysUser();
		string ssDBName = oSysUser.GetSysUserDatabaseName(F_UserID);
		ReSet2Mrs RESET2 = new ReSet2Mrs(aArr);
		RESET2.ls_Issue = (PubTools.Str2Int(F_chgCount) + 1).ToString();
		DataSet trgDS = RESET2.GetDataSet2(ssDBName, "MRS", "", srcDT, 1);
		DataSet trgDSP = RESET2.GetDataSet2(ssDBName, CommonMethods.GetActionNameString(F_ActionName), F_ProjectCode, srcDT2, 1);
		trgDS.CaseSensitive = true;
		for (int i = 0; i < trgDSP.Tables[0].Rows.Count; i++)
		{
			DataRow[] MrsDr = trgDS.Tables[0].Select("PccesCode ='" + trgDSP.Tables[0].Rows[i]["PccesCode"].ToString().Trim() + "'", "PccesCode");
			if (MrsDr.Length > 0)
			{
				trgDSP.Tables[0].Rows[i]["resCode"] = MrsDr[0]["PubCode"];
			}
		}
		string sSQLCmd = "";
		for (int i = 0; i < trgDSP.Tables[0].Rows.Count; i++)
		{
			if (trgDSP.Tables[0].Rows[i]["PubCode"].ToString() != trgDSP.Tables[0].Rows[i]["resCode"].ToString() && !(trgDSP.Tables[0].Rows[i]["resCode"].ToString().Trim() == ""))
			{
				sSQLCmd = "Update " + CommonMethods.GetActionNameString(F_ActionName) + "ProjMrsA Set pubCode =" + trgDSP.Tables[0].Rows[i]["resCode"].ToString() + " Where ProjectCode ='" + F_ProjectCode + "' And PubCode=" + trgDSP.Tables[0].Rows[i]["PubCode"].ToString() + "' and chgCount = '" + F_chgCount + '\r';
				object obj = sSQLCmd;
				sSQLCmd = string.Concat(obj, "Update ", CommonMethods.GetActionNameString(F_ActionName), "ProjMrsB Set ParentCode =", trgDSP.Tables[0].Rows[i]["resCode"].ToString(), " Where ProjectCode ='", F_ProjectCode, "' And ParentCode=", trgDSP.Tables[0].Rows[i]["PubCode"].ToString(), "' and chgCount = '", F_chgCount, '\r');
				obj = sSQLCmd;
				sSQLCmd = string.Concat(obj, "Update ", CommonMethods.GetActionNameString(F_ActionName), "ProjMrsB Set pubCode =", trgDSP.Tables[0].Rows[i]["resCode"].ToString(), " Where ProjectCode ='", F_ProjectCode, "' And PubCode=", trgDSP.Tables[0].Rows[i]["PubCode"].ToString(), "' and chgCount = '", F_chgCount, '\r');
				obj = sSQLCmd;
				sSQLCmd = string.Concat(obj, "Update ", CommonMethods.GetActionNameString(F_ActionName), "ProjMrsC Set ParentCode =", trgDSP.Tables[0].Rows[i]["resCode"].ToString(), " Where ProjectCode ='", F_ProjectCode, "' And ParentCode=", trgDSP.Tables[0].Rows[i]["PubCode"].ToString(), "' and chgCount = '", F_chgCount, '\r');
				obj = sSQLCmd;
				sSQLCmd = string.Concat(obj, "Update ", CommonMethods.GetActionNameString(F_ActionName), "ProjMrsC Set pubCode =", trgDSP.Tables[0].Rows[i]["resCode"].ToString(), " Where ProjectCode ='", F_ProjectCode, "' And PubCode=", trgDSP.Tables[0].Rows[i]["PubCode"].ToString(), "' and chgCount = '", F_chgCount, '\r');
				obj = sSQLCmd;
				sSQLCmd = string.Concat(obj, "Update ", CommonMethods.GetActionNameString(F_ActionName), "ProjMrsC Set itemCode =", trgDSP.Tables[0].Rows[i]["resCode"].ToString(), " Where ProjectCode ='", F_ProjectCode, "' And itemCode=", trgDSP.Tables[0].Rows[i]["PubCode"].ToString(), "' and chgCount = '", F_chgCount, '\r');
				obj = sSQLCmd;
				sSQLCmd = string.Concat(obj, "Update ", CommonMethods.GetActionNameString(F_ActionName), "ItemA Set pubCode =", trgDSP.Tables[0].Rows[i]["resCode"].ToString(), " Where ProjectCode ='", F_ProjectCode, "' And PubCode=", trgDSP.Tables[0].Rows[i]["PubCode"].ToString(), "' and chgCount = '", F_chgCount, '\r');
				DBCLS.ExecuteCommand(sSQLCmd);
			}
		}
		DBCLS = null;
		return trgDSP.Tables[0];
	}

	private string OutputPathFile(string sName)
	{
		return "";
	}

	private void Do_ItemReArrange()
	{
		string sQuestion = "確定執行項次重整嗎?";
		if (MessageBox.Show(this, sQuestion, "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			gridBudget2.Enabled = false;
			FormSys_G_Info1 FM_INFO = new FormSys_G_Info1();
			FM_INFO.TopMost = true;
			FM_INFO._InfoString = "項次重整中，請稍候!\n視『詳細表』項目多寡所需時間不同。";
			FM_INFO.Show();
			Application.DoEvents();
			Cursor = Cursors.WaitCursor;
			lock (this)
			{
				Cursor = Cursors.WaitCursor;
				Application.DoEvents();
				DoAssembleCode();
				Cursor = Cursors.WaitCursor;
				Application.DoEvents();
				Do_RealItemNoAssemble();
				Cursor = Cursors.WaitCursor;
				Application.DoEvents();
				SaveJustForItemNo();
				Cursor = Cursors.WaitCursor;
				Application.DoEvents();
				Do_Change();
				Application.DoEvents();
				Cursor = Cursors.Default;
				gridBudget1.Enabled = true;
				gridBudget2.Refresh();
				FM_INFO.Close();
				FM_INFO.Dispose();
				Application.DoEvents();
			}
			gridBudget2.Enabled = true;
			MessageBox.Show(this, "項次重整完畢 !!", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void SaveJustForItemNo()
	{
		string IPStr = CommonMethods.GetIPAddress();
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add("預算書存檔項次名稱--" + F_ProjectCode + "(" + IPStr + ")");
		if (dbItemA != null)
		{
			dbItemA = null;
		}
		if (dbItemA == null)
		{
			dbItemA = new ItemA(aArr);
		}
		dbItemA.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		dbItemA.ps_projectCode = F_ProjectCode;
		DataTable DT_ItemNo = new DataTable();
		DT_ItemNo.Columns.Add("ItemNo", Type.GetType("System.String"));
		DT_ItemNo.Columns.Add("SNo", Type.GetType("System.Int32"));
		for (int i = 1; i < gridBudget2.Rows.Count; i++)
		{
			if (gridBudget2[i, "SNo"] != null && gridBudget2[i, "SNo"].ToString().Trim() != "")
			{
				DataRow DR = DT_ItemNo.NewRow();
				DR["ItemNo"] = ((gridBudget2[i, "ItemNo"] == null) ? "" : gridBudget2[i, "ItemNo"].ToString().Trim());
				DR["SNo"] = PubTools.Str2Int(gridBudget2[i, "SNo"]);
				DT_ItemNo.Rows.Add(DR);
			}
		}
		dbItemA.UpdItemForItemNoChange(DT_ItemNo);
	}

	private void Execute_Calculator()
	{
		ShellExecute SHExe = new ShellExecute();
		SHExe.OwnerHandle = base.Handle;
		SHExe.Path = "Calc.exe";
		SHExe.Execute();
		SHExe = null;
	}

	public void Th_MenuPaste(DataSet custDS1)
	{
		DS1 = custDS1;
		MenuPaste();
	}

	public void MenuPaste()
	{
		gridBudget2.Enabled = false;
		FormSys_G_Info1 FM_INFO = new FormSys_G_Info1();
		FM_INFO._MaxValue = 100;
		FM_INFO._MinValue = 0;
		FM_INFO._ProgressValue = 0;
		FM_INFO._InfoString = "項目插入中，請稍候! ";
		FM_INFO.Show();
		Application.DoEvents();
		Cursor = Cursors.WaitCursor;
		Thread.Sleep(1);
		lock (this)
		{
			int iLastItemLevel = ((gridBudget2.Rows[gridBudget2.Row].Node == null) ? 1 : (gridBudget2.Rows[gridBudget2.Row].Node.Level + 1));
			if (gridBudget2[gridBudget2.Row, "Kind"].ToString().Trim() == "W")
			{
				iLastItemLevel--;
			}
			string sParentPrintToAnalysis = ((gridBudget2[gridBudget2.Row, "PrintToAnalysis"] != null) ? gridBudget2[gridBudget2.Row, "PrintToAnalysis"].ToString() : "0");
			DataTable DT_tmp = DS1.Tables[0].Copy();
			DataTable DT_Src = new DataTable();
			DT_Src.Columns.Add("PubCode", Type.GetType("System.Int32"));
			Application.DoEvents();
			for (int i = 0; i < DT_tmp.Rows.Count; i++)
			{
				DataRow DRSrc = DT_Src.NewRow();
				DRSrc["PubCode"] = DT_tmp.Rows[i]["PubCode"];
				DT_Src.Rows.Add(DRSrc);
				if (i % 5 == 0)
				{
					Application.DoEvents();
				}
			}
			FM_INFO._ProgressValue = 25;
			Application.DoEvents();
			DataTable MrsData = new DataTable();
			MrsData.Columns.Add("pubCode", Type.GetType("System.Int32"));
			string IPStr = CommonMethods.GetIPAddress();
			ArrayList aArr = new ArrayList();
			aArr.Add(F_UserID);
			aArr.Add("自基本資料庫挑選工項--" + F_ProjectCode + "(" + IPStr + ")");
			ReSet2Mrs RESET2 = new ReSet2Mrs(aArr);
			RESET2.ls_Issue = gridBudget1[gridBudget1.Row, "chgCount"].ToString();
			DataSet trgDS = RESET2.GetDataSet(F_FromDBName, "MRS", "", DT_Src, 1);
			FM_INFO._ProgressValue = 40;
			Application.DoEvents();
			RESET2.InputDataSet(F_CurrentDBName, CommonMethods.GetActionNameString(F_ActionName), F_ProjectCode, trgDS, 1, "");
			FM_INFO._ProgressValue = 65;
			Application.DoEvents();
			MrsBaseA MrsACom = new MrsBaseA(F_UserID, aArr);
			MrsACom.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
			MrsACom.ps_projectcode = F_ProjectCode;
			MrsACom.ps_Issue = gridBudget1[gridBudget1.Row, "chgCount"].ToString();
			if (dbItemA != null)
			{
				dbItemA = null;
			}
			if (dbItemA == null)
			{
				dbItemA = new ItemA(aArr);
			}
			dbItemA.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
			dbItemA.ps_projectCode = F_ProjectCode;
			dbItemA.ps_Issue = gridBudget1[gridBudget1.Row, "chgCount"].ToString();
			gridBudget2.Redraw = false;
			for (int j = DT_tmp.Rows.Count - 1; j >= 0; j--)
			{
				int iPrjMrsItm = MrsACom.GetCount(" pccesCode ='" + DT_tmp.Rows[j]["PccesCode"].ToString().Trim() + "'");
				string sUsrQty = "1";
				string sUsrAmt = "0";
				string sCost = "0";
				if (iPrjMrsItm > 0)
				{
					DataTable DT_Tmp_PrjMrs = MrsACom.ListItem(" pccesCode ='" + DT_tmp.Rows[j]["PccesCode"].ToString().Trim() + "'");
					if (DT_Tmp_PrjMrs.Rows.Count > 0)
					{
						DT_tmp.Rows[j]["pubCode"] = DT_Tmp_PrjMrs.Rows[0]["pubCode"];
						sUsrQty = DT_Tmp_PrjMrs.Rows[0]["usrQty"].ToString().Trim();
						sUsrAmt = DT_Tmp_PrjMrs.Rows[0]["usrAmt"].ToString().Trim();
						sCost = DT_Tmp_PrjMrs.Rows[0]["Cost"].ToString().Trim();
					}
				}
				else
				{
					sUsrQty = DT_tmp.Rows[j]["usrQty"].ToString().Trim();
					sUsrAmt = DT_tmp.Rows[j]["usrAmt"].ToString().Trim();
					sCost = DT_tmp.Rows[j]["Cost"].ToString().Trim();
				}
				string sRowContent = "";
				for (int i = 0; i < gridBudget2.Cols.Count; i++)
				{
					if (gridBudget2.Cols[i].Name == "RowIndicator")
					{
						sRowContent += "\t";
					}
					else if (gridBudget2.Cols[i].Name == "ItemNo")
					{
						sRowContent = sRowContent + j + "\t";
					}
					else if (gridBudget2.Cols[i].Name == "CName")
					{
						sRowContent = sRowContent + DT_tmp.Rows[j]["CName"].ToString().Trim() + "\t";
					}
					else if (gridBudget2.Cols[i].Name == "AnaImg")
					{
						sRowContent += "\t";
					}
					else if (gridBudget2.Cols[i].Name == "UnitName")
					{
						sRowContent = sRowContent + DT_tmp.Rows[j]["UnitName"].ToString().Trim() + "\t";
					}
					else if (gridBudget2.Cols[i].Name == "Qty")
					{
						sRowContent += "0\t";
					}
					else if (gridBudget2.Cols[i].Name == "Lock")
					{
						sRowContent += "false\t";
					}
					else if (gridBudget2.Cols[i].Name == "Cost")
					{
						sRowContent += "0\t";
					}
					else if (gridBudget2.Cols[i].Name == "Amount")
					{
						sRowContent += "0\t";
					}
					else if (gridBudget2.Cols[i].Name == "ChgQty")
					{
						sRowContent += "1\t";
					}
					else if (gridBudget2.Cols[i].Name == "ChgCost")
					{
						sRowContent = sRowContent + sCost + "\t";
					}
					else if (gridBudget2.Cols[i].Name == "ChgAmount")
					{
						sRowContent = sRowContent + sUsrAmt + "\t";
					}
					else if (gridBudget2.Cols[i].Name == "PccesCode")
					{
						sRowContent = sRowContent + DT_tmp.Rows[j]["PccesCode"].ToString().Trim() + "\t";
					}
					else if (gridBudget2.Cols[i].Name == "Memo")
					{
						sRowContent = sRowContent + DT_tmp.Rows[j]["memo"].ToString().Trim() + "\t";
					}
					else if (gridBudget2.Cols[i].Name == "EName")
					{
						sRowContent = sRowContent + DT_tmp.Rows[j]["eName"].ToString().Trim() + "\t";
					}
					else if (gridBudget2.Cols[i].Name == "EUnit")
					{
						sRowContent = sRowContent + DT_tmp.Rows[j]["eUnit"].ToString().Trim() + "\t";
					}
					else if (gridBudget2.Cols[i].Name == "LevelNo")
					{
						sRowContent += "\t";
					}
					else if (gridBudget2.Cols[i].Name == "Kind")
					{
						sRowContent += "W\t";
					}
					else if (gridBudget2.Cols[i].Name == "Analysis")
					{
						sRowContent = sRowContent + DT_tmp.Rows[j]["Analysis"].ToString().Trim() + "\t";
					}
					else if (gridBudget2.Cols[i].Name == "SNo")
					{
						sRowContent += "\t";
					}
					else if (gridBudget2.Cols[i].Name == "Formula")
					{
						sRowContent += "\t";
					}
					else if (gridBudget2.Cols[i].Name == "PrintNo")
					{
						sRowContent += "\t";
					}
					else if (gridBudget2.Cols[i].Name == "OldPrintNo")
					{
						sRowContent += "\t";
					}
					else if (gridBudget2.Cols[i].Name == "PubCode")
					{
						sRowContent = sRowContent + DT_tmp.Rows[j]["PubCode"].ToString().Trim() + "\t";
					}
					else if (gridBudget2.Cols[i].Name == "IsShared")
					{
						sRowContent += "\t";
					}
					else if (gridBudget2.Cols[i].Name == "PrintToAnalysis")
					{
						sRowContent = sRowContent + sParentPrintToAnalysis + "\t";
					}
					else if (gridBudget2.Cols[i].Name == "AccMode")
					{
						sRowContent += "0\t";
					}
				}
				gridBudget2.AddItem(sRowContent, gridBudget2.Row + 1);
				gridBudget2.Rows[gridBudget2.Row + 1].IsNode = true;
				gridBudget2.Rows[gridBudget2.Row + 1].Node.Level = iLastItemLevel;
				DataRow dr = MrsData.NewRow();
				dr["pubCode"] = DT_tmp.Rows[j]["PubCode"].ToString().Trim();
				MrsData.Rows.Add(dr);
				DoAssembleCode();
				if (j % 5 == 0)
				{
					Application.DoEvents();
				}
			}
			FM_INFO._ProgressValue = 85;
			Application.DoEvents();
			for (int i = 0; i < gridBudget2.Rows.Count; i++)
			{
				bool IsOriginalExist = false;
				if (gridBudget2.Rows[i].IsNode)
				{
					dbItemA.ps_printNo = gridBudget2[i, "PrintNo"].ToString().Trim();
					if (gridBudget2[i, "Kind"].ToString() == "W")
					{
						dbItemA.ps_itemNo = Convert.ToInt32(dbItemA.ps_printNo.Substring(dbItemA.ps_printNo.Length - 4, 4)).ToString();
					}
					else
					{
						dbItemA.ps_itemNo = ((gridBudget2[i, "ItemNo"] != null) ? gridBudget2[i, "ItemNo"].ToString() : null);
					}
					if (gridBudget2[i, "sNO"] == null)
					{
						dbItemA.ps_sNo = (dbItemA.getMaxNo(F_ProjectCode) + 1).ToString();
					}
					else if (gridBudget2[i, "sNO"].ToString() == "")
					{
						dbItemA.ps_sNo = (dbItemA.getMaxNo(F_ProjectCode) + 1).ToString();
					}
					else if (gridBudget2[i, "sNO"].ToString() != "")
					{
						IsOriginalExist = true;
						dbItemA.ps_sNo = gridBudget2[i, "sNO"].ToString().Trim();
					}
					dbItemA.ps_kind = gridBudget2[i, "Kind"].ToString();
					dbItemA.ps_cName = ((gridBudget2[i, "CName"] != null) ? gridBudget2[i, "CName"].ToString() : null);
					dbItemA.ps_amount = ((gridBudget2[i, "Amount"] != null) ? gridBudget2[i, "Amount"].ToString() : null);
					dbItemA.ps_bidCode = null;
					dbItemA.ps_cost = ((gridBudget2[i, "Cost"] != null) ? gridBudget2[i, "Cost"].ToString() : null);
					dbItemA.ps_eName = ((gridBudget2[i, "EName"] != null) ? gridBudget2[i, "EName"].ToString() : null);
					dbItemA.ps_eUnit = ((gridBudget2[i, "EUnit"] != null) ? gridBudget2[i, "EUnit"].ToString() : null);
					dbItemA.ps_Formula = ((gridBudget2[i, "Formula"] != null) ? gridBudget2[i, "Formula"].ToString() : null);
					dbItemA.ps_levelNo = ((gridBudget2[i, "LevelNo"] != null) ? gridBudget2[i, "LevelNo"].ToString() : null);
					dbItemA.ps_memo = ((gridBudget2[i, "Memo"] != null) ? gridBudget2[i, "Memo"].ToString() : null);
					dbItemA.ps_qty = ((gridBudget2[i, "Qty"] != null) ? gridBudget2[i, "Qty"].ToString() : null);
					dbItemA.ps_rate = null;
					dbItemA.ps_setDecimal = null;
					dbItemA.ps_share = ((gridBudget2[i, "IsShared"] != null) ? gridBudget2[i, "IsShared"].ToString() : null);
					dbItemA.ps_unitName = ((gridBudget2[i, "UnitName"] != null) ? gridBudget2[i, "UnitName"].ToString() : null);
					dbItemA.ps_pubCode = ((gridBudget2[i, "PubCode"] != null) ? gridBudget2[i, "PubCode"].ToString() : null);
					dbItemA.ps_PrintToAnalysis = ((gridBudget2[i, "PrintToAnalysis"] != null) ? gridBudget2[i, "PrintToAnalysis"].ToString() : "0");
					dbItemA.ps_Issue = gridBudget1[gridBudget1.Row, "chgCount"].ToString();
					dbItemA.ps_ChgCost = ((gridBudget2[i, "ChgCost"] != null) ? gridBudget2[i, "ChgCost"].ToString() : null);
					dbItemA.ps_ChgQty = ((gridBudget2[i, "ChgQty"] != null) ? gridBudget2[i, "ChgQty"].ToString() : null);
					dbItemA.ps_ChgAmount = ((gridBudget2[i, "ChgAmount"] != null) ? gridBudget2[i, "ChgAmount"].ToString() : null);
					dbItemA.ps_AccMode = null;
					if (IsOriginalExist)
					{
						dbItemA.UpdItem();
					}
					else
					{
						dbItemA.InseItem();
					}
					if (i % 5 == 0)
					{
						Application.DoEvents();
					}
				}
			}
			FM_INFO._ProgressValue = 95;
			Application.DoEvents();
			gridBudget2.Redraw = true;
			MrsACom.CopyMrsBase(MrsACom.ps_srckind, F_PasteSource_SrcKind, F_ProjectCode, F_PasteSource_Project, MrsData);
			Do_Change();
			RemeberOldPrintNo();
			ChagePrintNo("");
			FM_INFO._ProgressValue = 100;
			Application.DoEvents();
			gridBudget2.Enabled = true;
			gridBudget2.Refresh();
			FM_INFO.Close();
			FM_INFO.Dispose();
		}
	}

	private void ExecutePickFromMrs()
	{
		FormMrsBaseBreakdown_Addnew BD_ADD = new FormMrsBaseBreakdown_Addnew();
		BD_ADD._CallFormName = base.Name;
		BD_ADD._UserID = F_UserID;
		BD_ADD.ShowDialog(this);
		BD_ADD.Close();
		BD_ADD.Dispose();
		BD_ADD = null;
	}

	private void Delete_BDGT_Item()
	{
		bool IsCheckNoUsedItem = false;
		string AppLocation = AppDomain.CurrentDomain.BaseDirectory;
		string FileIni = AppLocation + "OptionSet.ini";
		string sIsOldReCal = CommonMethods.IniReadValue(AppLocation + FileIni, "BDGT", "IsOldReCal");
		IsCheckNoUsedItem = sIsOldReCal.ToUpper() == "TRUE";
		if (gridBudget2[gridBudget2.Row, "SNo"] == null)
		{
			return;
		}
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = F_UserID;
		if (!Is_MultiRowSelect && DBCLS.ItemA_CanEdit(gridBudget2[gridBudget2.Row, "SNo"].ToString().Trim(), F_ProjectCode, CommonMethods.GetActionNameString(F_ActionName)))
		{
			string sMessage = "您正要刪除資料\n其相關資將被刪除，\n\n假如你按一下「是」，您將不能復原此刪除操作。\n您確定您要刪除這些資料嗎?";
			string sMessage2 = "您要一併刪除子項資料嗎?\n";
			if (gridBudget1.Row <= 0)
			{
				return;
			}
			if (MessageBox.Show(this, sMessage, "刪除", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
			{
				gridBudget1.Enabled = false;
				FormSys_G_Info1 FM_INFO = new FormSys_G_Info1();
				FM_INFO._InfoString = "項目刪除中，請稍候! ";
				FM_INFO.Show();
				Application.DoEvents();
				Cursor = Cursors.WaitCursor;
				lock (this)
				{
					string IPStr = CommonMethods.GetIPAddress();
					ArrayList aArr = new ArrayList();
					aArr.Clear();
					aArr.Add(F_UserID);
					aArr.Add("詳細表--項目刪除" + F_ProjectCode + "(" + IPStr + ")");
					if (dbItemA != null)
					{
						dbItemA = null;
					}
					if (dbItemA == null)
					{
						dbItemA = new ItemA(aArr);
					}
					dbItemA.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
					dbItemA.ps_projectCode = F_ProjectCode;
					dbItemA.ps_Issue = gridBudget1[gridBudget1.Row, "chgCount"].ToString();
					int iChildern = gridBudget2.Rows[gridBudget2.Row].Node.Children;
					FM_INFO.Hide();
					if (iChildern > 0 && MessageBox.Show(this, sMessage2, "刪除", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
					{
						FM_INFO.Show();
						int iStart = gridBudget2.Row + iChildern;
						int iEnd = gridBudget2.Row;
						for (int i = iStart; i >= iEnd; i--)
						{
							dbItemA.ps_sNo = gridBudget2[i, "SNo"].ToString();
							dbItemA.ps_printNo = gridBudget2[i, "PrintNo"].ToString().Trim();
							dbItemA.DeleItem();
						}
					}
					else
					{
						FM_INFO.Hide();
						if (iChildern > 0)
						{
							MessageBox.Show(this, "有子項存在，無法刪除!", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						}
						else
						{
							dbItemA.ps_sNo = gridBudget2[gridBudget2.Row, "SNo"].ToString();
							dbItemA.ps_printNo = gridBudget2[gridBudget2.Row, "PrintNo"].ToString().Trim();
							dbItemA.DeleItem();
						}
					}
					Do_Change();
					DoAssembleCode();
					ChagePrintNo("DEL");
					SaveGridDataToItemA();
					if (IsCheckNoUsedItem)
					{
						dbItemA.ReMrsData(F_ProjectCode);
					}
					FM_INFO.Close();
					FM_INFO.Dispose();
					gridBudget1.Enabled = true;
					gridBudget1.Refresh();
					Cursor = Cursors.Default;
					Refresh();
				}
			}
		}
		DBCLS = null;
	}

	private void EditItemsByKind()
	{
		if (gridBudget2[gridBudget2.Row, "Kind"] == null)
		{
			return;
		}
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = F_UserID;
		try
		{
			FormBudgetEditMain FM_BDGT_EM = new FormBudgetEditMain();
			FM_BDGT_EM._UserID = F_UserID;
			FM_BDGT_EM.ProjectCode = F_ProjectCode;
			FM_BDGT_EM._ActionName = F_ActionName;
			FM_BDGT_EM.Item_sNo = (int)gridBudget2[gridBudget2.Row, "sNO"];
			FM_BDGT_EM.ChildCount = gridBudget2.Rows[gridBudget2.Row].Node.Children;
			FM_BDGT_EM.FormulaStr = gridBudget2[gridBudget2.Row, "Formula"].ToString();
			FM_BDGT_EM.ItemType = CommonMethods.GetBDGT_ItemType(gridBudget2[gridBudget2.Row, "Kind"].ToString());
			FM_BDGT_EM._ShareItems = GetShareItems(gridBudget2.Row);
			FM_BDGT_EM._ShareItemSno = GetShareItemSNo(gridBudget2[gridBudget2.Row, "sNO"].ToString().Trim());
			FM_BDGT_EM._PrintToAnalysis = "";
			FM_BDGT_EM._IsCanPrintToAnalysis = false;
			FM_BDGT_EM._PccesCode = ((gridBudget2[gridBudget2.Row, "PccesCode"] != null) ? gridBudget2[gridBudget2.Row, "PccesCode"].ToString() : "");
			FM_BDGT_EM._Issue = PubTools.Str2Int(gridBudget1[gridBudget1.Row, "chgCount"]);
			FM_BDGT_EM.Owner = this;
			if (FM_BDGT_EM.ShowDialog() == DialogResult.OK)
			{
				Do_Change();
				int iPos = gridBudget2.Row;
				int iSno = (int)gridBudget2[gridBudget2.Row, "SNo"];
				ReLoad_OneRow(iSno, iPos);
				if (!(gridBudget2[gridBudget2.Row, "Kind"].ToString() == "B"))
				{
				}
			}
			FM_BDGT_EM.Close();
			FM_BDGT_EM.Dispose();
			FM_BDGT_EM = null;
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudgetChange.cs" + ex.Message);
			if (IsDEBUG_MODE)
			{
				MessageBox.Show(this, "Err10:\n" + ex.Message, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		DBCLS = null;
	}

	private void DoInsertMainItems(string sMethod)
	{
		int iStartRow = 0;
		iStartRow = ((DataRows_AfterBinding != 0) ? (gridBudget2.Row + 1) : gridBudget1.Row);
		int iLastItemLevel = ((gridBudget2.Rows[gridBudget2.Row].Node == null) ? 1 : ((sMethod.ToUpper() == "CHILD") ? (gridBudget2.Rows[gridBudget2.Row].Node.Level + 1) : gridBudget2.Rows[gridBudget2.Row].Node.Level));
		if (iLastItemLevel == 0)
		{
			iLastItemLevel = 1;
		}
		int iNewLines = 0;
		string sParentPrintToAnalysis = ((gridBudget2[gridBudget1.Row, "PrintToAnalysis"] != null) ? gridBudget2[gridBudget1.Row, "PrintToAnalysis"].ToString() : "0");
		if (sMethod == "SIBILING")
		{
			sParentPrintToAnalysis = "";
		}
		string sKind = "";
		int iChildCount = 0;
		if (gridBudget2[gridBudget2.Row, "Kind"] != null)
		{
			sKind = gridBudget2[gridBudget2.Row, "Kind"].ToString();
			if (sKind.ToUpper() == "B")
			{
				Node LastNode = gridBudget2.Rows[gridBudget2.Row].Node.GetNode(NodeTypeEnum.LastChild);
				try
				{
					while (LastNode.Children > 0)
					{
						LastNode = LastNode.GetNode(NodeTypeEnum.LastChild);
					}
				}
				catch (Exception ex)
				{
					CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudgetChange.cs" + ex.Message);
				}
				try
				{
					iChildCount = LastNode.Row.SafeIndex - gridBudget2.Row;
				}
				catch (Exception ex)
				{
					CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudgetChange.cs" + ex.Message);
					iChildCount = 0;
				}
				iStartRow = gridBudget2.Row + iChildCount + 1;
			}
		}
		FormAskLines FM_ASK_LINE = new FormAskLines();
		if (gridBudget2[gridBudget2.Row, "ItemNo"] != null)
		{
			FM_ASK_LINE._Question = "欲新增 【" + gridBudget2[gridBudget2.Row, "ItemNo"].ToString().Trim() + "】 " + gridBudget2[gridBudget2.Row, "CName"].ToString().Trim() + ((sMethod.ToUpper() == "CHILD") ? " 子階幾項?" : " 同階幾項?");
		}
		else
		{
			FM_ASK_LINE._Question = "欲新增 " + ((sMethod.ToUpper() == "CHILD") ? " 子階幾項?" : " 同階幾項?");
		}
		FM_ASK_LINE._Answer = "1";
		if (FM_ASK_LINE.ShowDialog(this) == DialogResult.OK)
		{
			iNewLines = PubTools.Str2Int(FM_ASK_LINE._Answer);
		}
		FM_ASK_LINE.Close();
		FM_ASK_LINE.Dispose();
		FM_ASK_LINE = null;
		if (iNewLines <= 0)
		{
			MessageBox.Show(this, "輸入數值錯誤, 無法新增!!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		for (int j = 0; j < iNewLines; j++)
		{
			string sRowContent = "";
			for (int i = 0; i < gridBudget2.Cols.Count; i++)
			{
				if (gridBudget2.Cols[i].Name == "RowIndicator")
				{
					sRowContent += "\t";
				}
				else if (gridBudget2.Cols[i].Name == "ItemNo")
				{
					sRowContent += "\t";
				}
				else if (gridBudget2.Cols[i].Name == "CName")
				{
					sRowContent += "\t";
				}
				else if (gridBudget2.Cols[i].Name == "AnaImg")
				{
					sRowContent += "\t";
				}
				else if (gridBudget2.Cols[i].Name == "UnitName")
				{
					sRowContent += "\t";
				}
				else if (gridBudget2.Cols[i].Name == "Qty")
				{
					sRowContent += "0\t";
				}
				else if (gridBudget2.Cols[i].Name == "Lock")
				{
					sRowContent += "false\t";
				}
				else if (gridBudget2.Cols[i].Name == "Cost")
				{
					sRowContent += "0\t";
				}
				else if (gridBudget2.Cols[i].Name == "Amount")
				{
					sRowContent += "0\t";
				}
				else if (gridBudget2.Cols[i].Name == "ChgQty")
				{
					sRowContent += "0\t";
				}
				else if (gridBudget2.Cols[i].Name == "ChgCost")
				{
					sRowContent += "0\t";
				}
				else if (gridBudget2.Cols[i].Name == "ChgAmount")
				{
					sRowContent += "0\t";
				}
				else if (gridBudget2.Cols[i].Name == "PccesCode")
				{
					sRowContent += "\t";
				}
				else if (gridBudget2.Cols[i].Name == "Memo")
				{
					sRowContent += "\t";
				}
				else if (gridBudget2.Cols[i].Name == "AccMode")
				{
					sRowContent += "\t";
				}
				else if (gridBudget2.Cols[i].Name == "EName")
				{
					sRowContent += "\t";
				}
				else if (gridBudget2.Cols[i].Name == "EUnit")
				{
					sRowContent += "\t";
				}
				else if (gridBudget2.Cols[i].Name == "LevelNo")
				{
					sRowContent += "\t";
				}
				else if (gridBudget2.Cols[i].Name == "Kind")
				{
					sRowContent += "B\t";
				}
				else if (gridBudget2.Cols[i].Name == "Analysis")
				{
					sRowContent += "\t";
				}
				else if (gridBudget2.Cols[i].Name == "SNo")
				{
					sRowContent += "\t";
				}
				else if (gridBudget2.Cols[i].Name == "Formula")
				{
					sRowContent += "\t";
				}
				else if (gridBudget2.Cols[i].Name == "PrintNo")
				{
					sRowContent += "\t";
				}
				else if (gridBudget2.Cols[i].Name == "OldPrintNo")
				{
					sRowContent += "\t";
				}
				else if (gridBudget2.Cols[i].Name == "PubCode")
				{
					sRowContent += "\t";
				}
				else if (gridBudget2.Cols[i].Name == "IsShared")
				{
					sRowContent += "\t";
				}
				else if (gridBudget2.Cols[i].Name == "PrintToAnalysis")
				{
					sRowContent = sRowContent + sParentPrintToAnalysis + "\t";
				}
			}
			if (sKind.ToUpper() == "B")
			{
				gridBudget2.AddItem(sRowContent, gridBudget2.Row + iChildCount + 1);
				gridBudget2.Rows[gridBudget2.Row + iChildCount + 1].IsNode = true;
				gridBudget2.Rows[gridBudget2.Row + iChildCount + 1].Node.Level = iLastItemLevel;
			}
			else
			{
				gridBudget2.AddItem(sRowContent, gridBudget2.Row + 1);
				gridBudget2.Rows[gridBudget2.Row + 1].IsNode = true;
				gridBudget2.Rows[gridBudget2.Row + 1].Node.Level = iLastItemLevel;
			}
		}
		if (DataRows_AfterBinding == 0)
		{
			gridBudget2.RemoveItem(gridBudget2.Row);
		}
		DoAssembleCode();
		string IPStr = CommonMethods.GetIPAddress();
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add("項次重編--存檔--" + F_ProjectCode + "(" + IPStr + ")");
		BDGT_ItemReName BDGT_ITEM1 = new BDGT_ItemReName(aArr);
		sAssemType = CommonMethods.IniReadValue(sIniFileName, "AutoItemNo", "AssemType");
		BDGT_ITEM1._AssemType = sAssemType;
		BDGT_ITEM1._Separate = "";
		BDGT_ITEM1._StringList1 = CommonMethods.IniReadValue(sIniFileName, "AutoItemNo", "1");
		BDGT_ITEM1._StringList2 = CommonMethods.IniReadValue(sIniFileName, "AutoItemNo", "2");
		BDGT_ITEM1._StringList3 = CommonMethods.IniReadValue(sIniFileName, "AutoItemNo", "3");
		BDGT_ITEM1._StringList4 = CommonMethods.IniReadValue(sIniFileName, "AutoItemNo", "4");
		BDGT_ITEM1._StringList5 = CommonMethods.IniReadValue(sIniFileName, "AutoItemNo", "5");
		BDGT_ITEM1._StringList6 = CommonMethods.IniReadValue(sIniFileName, "AutoItemNo", "6");
		BDGT_ITEM1._StringList7 = CommonMethods.IniReadValue(sIniFileName, "AutoItemNo", "7");
		BDGT_ITEM1._StringList8 = CommonMethods.IniReadValue(sIniFileName, "AutoItemNo", "8");
		if (sKind.ToUpper() == "B")
		{
			for (int i = iStartRow; i < iStartRow + iNewLines; i++)
			{
				BDGT_ITEM1._ItemKind = gridBudget2[i, "Kind"].ToString().Trim();
				gridBudget2[i, "ItemNo"] = BDGT_ITEM1.GetItemNoByPrintNo2(gridBudget2[i, "PrintNo"].ToString().Trim());
			}
		}
		else
		{
			for (int i = iStartRow; i < iStartRow + iNewLines; i++)
			{
				BDGT_ITEM1._ItemKind = gridBudget2[i, "Kind"].ToString().Trim();
				gridBudget2[i, "ItemNo"] = BDGT_ITEM1.GetItemNoByPrintNo2(gridBudget2[i, "PrintNo"].ToString().Trim());
			}
		}
		ChagePrintNo("");
		Do_RealItemNoAssemble();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add(CommonMethods.GetFormTypeTitle(FormType.Budget));
		if (dbItemA != null)
		{
			dbItemA = null;
		}
		if (dbItemA == null)
		{
			dbItemA = new ItemA(aArr);
		}
		dbItemA.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		dbItemA.ps_projectCode = F_ProjectCode;
		dbItemA.ps_Issue = gridBudget1[gridBudget1.Row, "chgCount"].ToString();
		for (int i = 1; i < gridBudget2.Rows.Count; i++)
		{
			bool IsOriginalExist = false;
			if (gridBudget2.Rows[i].IsNode && gridBudget2[i, "Kind"] != null)
			{
				dbItemA.ps_printNo = gridBudget2[i, "PrintNo"].ToString().Trim();
				dbItemA.ps_itemNo = ((gridBudget2[i, "ItemNo"] != null) ? gridBudget2[i, "ItemNo"].ToString() : null);
				if (gridBudget2[i, "sNO"] == null)
				{
					dbItemA.ps_sNo = (dbItemA.getMaxNo(F_ProjectCode) + 1).ToString();
				}
				else if (gridBudget2[i, "sNO"].ToString() == "")
				{
					dbItemA.ps_sNo = (dbItemA.getMaxNo(F_ProjectCode) + 1).ToString();
				}
				else if (gridBudget2[i, "sNO"].ToString() != "")
				{
					IsOriginalExist = true;
					dbItemA.ps_sNo = gridBudget2[i, "sNO"].ToString().Trim();
				}
				dbItemA.ps_kind = gridBudget2[i, "Kind"].ToString();
				dbItemA.ps_cName = ((gridBudget2[i, "CName"] != null) ? gridBudget2[i, "CName"].ToString() : null);
				dbItemA.ps_amount = ((gridBudget2[i, "Amount"] != null) ? gridBudget2[i, "Amount"].ToString() : null);
				dbItemA.ps_bidCode = null;
				dbItemA.ps_cost = ((gridBudget2[i, "Cost"] != null) ? gridBudget2[i, "Cost"].ToString() : null);
				dbItemA.ps_eName = ((gridBudget2[i, "EName"] != null) ? gridBudget2[i, "EName"].ToString() : null);
				dbItemA.ps_eUnit = ((gridBudget2[i, "EUnit"] != null) ? gridBudget2[i, "EUnit"].ToString() : null);
				dbItemA.ps_Formula = ((gridBudget2[i, "Formula"] != null) ? gridBudget2[i, "Formula"].ToString() : null);
				dbItemA.ps_levelNo = ((gridBudget2[i, "LevelNo"] != null) ? gridBudget2[i, "LevelNo"].ToString() : null);
				dbItemA.ps_memo = ((gridBudget2[i, "Memo"] != null) ? gridBudget2[i, "Memo"].ToString() : null);
				dbItemA.ps_qty = ((gridBudget2[i, "Qty"] != null) ? gridBudget2[i, "Qty"].ToString() : null);
				dbItemA.ps_ChgQty = ((gridBudget2[i, "ChgQty"] != null) ? gridBudget2[i, "ChgQty"].ToString() : null);
				dbItemA.ps_ChgCost = ((gridBudget2[i, "ChgCost"] != null) ? gridBudget2[i, "ChgCost"].ToString() : null);
				dbItemA.ps_ChgAmount = "0";
				dbItemA.ps_rate = null;
				dbItemA.ps_setDecimal = null;
				dbItemA.ps_share = ((gridBudget2[i, "IsShared"] != null) ? gridBudget2[i, "IsShared"].ToString() : null);
				dbItemA.ps_unitName = ((gridBudget2[i, "UnitName"] != null) ? gridBudget2[i, "UnitName"].ToString() : null);
				dbItemA.ps_pubCode = ((gridBudget2[i, "PubCode"] != null) ? gridBudget2[i, "PubCode"].ToString() : null);
				dbItemA.ps_PrintToAnalysis = ((gridBudget2[i, "PrintToAnalysis"] != null) ? gridBudget2[i, "PrintToAnalysis"].ToString() : null);
				if (IsOriginalExist)
				{
					dbItemA.UpdItem();
				}
				else
				{
					dbItemA.InseItem();
				}
			}
		}
		Do_Change();
		RemeberOldPrintNo();
	}

	private void RemeberOldPrintNo()
	{
		for (int i = 1; i <= gridBudget2.Rows.Count - 1; i++)
		{
			if (gridBudget2.Rows[i].IsNode && gridBudget2[i, "PrintNo"] != null)
			{
				gridBudget2[i, "OldPrintNo"] = gridBudget2[i, "PrintNo"].ToString().Trim();
			}
		}
	}

	private void Do_RealItemNoAssemble()
	{
		string IPStr = CommonMethods.GetIPAddress();
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add("項次重編" + F_ProjectCode + "(" + IPStr + ")");
		BDGT_ItemReName BDGT_ITEM1 = new BDGT_ItemReName(aArr);
		sAssemType = CommonMethods.IniReadValue(sIniFileName, "AutoItemNo", "AssemType");
		sIsSymbol = CommonMethods.IniReadValue(sIniFileName, "AutoItemNo", "IsSymbol");
		sSymbol = CommonMethods.IniReadValue(sIniFileName, "AutoItemNo", "Symbol");
		BDGT_ITEM1._AssemType = sAssemType;
		BDGT_ITEM1._Separate = sSymbol;
		BDGT_ITEM1._IsSymbol = sIsSymbol;
		BDGT_ITEM1._StringList1 = CommonMethods.IniReadValue(sIniFileName, "AutoItemNo", "1");
		BDGT_ITEM1._StringList2 = CommonMethods.IniReadValue(sIniFileName, "AutoItemNo", "2");
		BDGT_ITEM1._StringList3 = CommonMethods.IniReadValue(sIniFileName, "AutoItemNo", "3");
		BDGT_ITEM1._StringList4 = CommonMethods.IniReadValue(sIniFileName, "AutoItemNo", "4");
		BDGT_ITEM1._StringList5 = CommonMethods.IniReadValue(sIniFileName, "AutoItemNo", "5");
		BDGT_ITEM1._StringList6 = CommonMethods.IniReadValue(sIniFileName, "AutoItemNo", "6");
		BDGT_ITEM1._StringList7 = CommonMethods.IniReadValue(sIniFileName, "AutoItemNo", "7");
		BDGT_ITEM1._StringList8 = CommonMethods.IniReadValue(sIniFileName, "AutoItemNo", "8");
		BDGT_ITEM1.CallStringArrayFirst();
		string sSwitcher = CommonMethods.GetIniValue("AutoItemNo", "Type");
		for (int i = 1; i < gridBudget2.Rows.Count; i++)
		{
			if (gridBudget2.Rows[i].IsNode && gridBudget2[i, "Kind"] != null)
			{
				if (!(sSwitcher.ToUpper() == "ALL"))
				{
					if (sSwitcher.ToUpper() == "M")
					{
						if (gridBudget2[i, "Kind"].ToString().Trim() == "W")
						{
							continue;
						}
					}
					else if (sSwitcher.ToUpper() == "W" && gridBudget2[i, "Kind"].ToString().Trim() != "W")
					{
						continue;
					}
				}
				BDGT_ITEM1._ItemKind = gridBudget2[i, "Kind"].ToString().Trim();
				BDGT_ITEM1._PccesCode = ((gridBudget2[i, "PccesCode"] != null) ? gridBudget2[i, "PccesCode"].ToString().Trim() : "");
				gridBudget2[i, "ItemNo"] = BDGT_ITEM1.GetItemNoByPrintNo(gridBudget2[i, "PrintNo"].ToString().Trim());
			}
			if (i % 20 == 0)
			{
				Application.DoEvents();
				Cursor = Cursors.WaitCursor;
			}
		}
		Cursor = Cursors.Default;
	}

	private void ChagePrintNo(string sACTION)
	{
		string IPStr = CommonMethods.GetIPAddress();
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add("變更PrintNo" + F_ProjectCode + "(" + IPStr + ")");
		ModifyDB StdCom = new ModifyDB(F_ProjectCode, aArr);
		string SqlStr = "";
		string ls_fn = "";
		if (dbItemA != null)
		{
			dbItemA = null;
		}
		if (dbItemA == null)
		{
			dbItemA = new ItemA(aArr);
		}
		dbItemA.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		ls_fn = "subChg";
		object obj;
		for (int i = 1; i <= gridBudget2.Rows.Count - 1; i++)
		{
			if (gridBudget2.Rows[i].IsNode && gridBudget2.Rows[i]["sNo"] != null)
			{
				string ls_printno = gridBudget2.Rows[i]["PrintNo"].ToString().Trim();
				string ls_oldprintno = ((gridBudget2.Rows[i]["OldPrintNo"] == null) ? "" : gridBudget2.Rows[i]["OldPrintNo"].ToString().Trim());
				if ((!(sACTION != "") || !(ls_printno == ls_oldprintno)) && (!(sACTION != "") || !(ls_oldprintno == "")) && ls_printno != "".PadLeft(32, '9'))
				{
					SqlStr = "Update " + ls_fn + "ItemA set printno='" + ls_printno + "',levelno=" + ls_printno.Length / 4;
					obj = SqlStr;
					SqlStr = string.Concat(obj, " where projectcode='", F_ProjectCode, "' and sno=", gridBudget2.Rows[i]["sNo"].ToString(), " and chgCount=", gridBudget1[gridBudget1.Row, "chgCount"].ToString(), " ", '\r');
					obj = SqlStr;
					SqlStr = string.Concat(obj, "Update ", ls_fn, "ItemB set ParentCode='A", ls_printno, "' where projectcode='", F_ProjectCode, "' and ParentCode='", ls_oldprintno, "' and chgCount=", gridBudget1[gridBudget1.Row, "chgCount"].ToString(), " ", '\r');
					obj = SqlStr;
					SqlStr = string.Concat(obj, "Update ", ls_fn, "ItemB set ItemCode='A", ls_printno, "' where projectcode='", F_ProjectCode, "' and ItemCode='", ls_oldprintno, "' and chgCount=", gridBudget1[gridBudget1.Row, "chgCount"].ToString(), " ", '\r');
					obj = SqlStr;
					SqlStr = string.Concat(obj, "Update ", ls_fn, "ItemC set printno='A", ls_printno, "' where projectcode='", F_ProjectCode, "' and printno='", ls_oldprintno, "' and chgCount=", gridBudget1[gridBudget1.Row, "chgCount"].ToString(), " ", '\r');
					StdCom.DBUpd(SqlStr);
				}
			}
		}
		SqlStr = "Update " + ls_fn + "ItemB set ParentCode=substring(ParentCode,2,32) where projectcode='" + F_ProjectCode + "' and ParentCode !='" + "".PadLeft(32, '9') + "'  and substring(ParentCode,1,1)='A'  and chgCount=" + gridBudget1[gridBudget1.Row, "chgCount"].ToString() + " " + '\r';
		obj = SqlStr;
		SqlStr = string.Concat(obj, "Update ", ls_fn, "ItemB set ItemCode=substring(ItemCode,2,32) where projectcode='", F_ProjectCode, "' and ItemCode !='", "".PadLeft(32, '9'), "' and substring(ItemCode,1,1)='A'  and chgCount=", gridBudget1[gridBudget1.Row, "chgCount"].ToString(), " ", '\r');
		obj = SqlStr;
		SqlStr = string.Concat(obj, "Update ", ls_fn, "ItemC set printno=substring(printno,2,32) where projectcode='", F_ProjectCode, "' and printno !='", "".PadLeft(32, '9'), "' and substring(printno,1,1)='A'  and chgCount=", gridBudget1[gridBudget1.Row, "chgCount"].ToString(), " ", '\r');
		StdCom.DBUpd(SqlStr);
		StdCom = null;
		PubTools.WriteRoughlyLog(aArr);
	}

	private void ReLoad_OneRow(int iSno, int gridRow)
	{
		string IPStr = CommonMethods.GetIPAddress();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("預算書單價分析編輯完後重讀該筆資料--" + F_ProjectCode + "(" + IPStr + ")");
		if (dbItemA != null)
		{
			dbItemA = null;
		}
		if (dbItemA == null)
		{
			dbItemA = new ItemA(aArr);
		}
		dbItemA.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		dbItemA.ps_projectCode = F_ProjectCode;
		dbItemA.ps_Issue = gridBudget1[gridBudget1.Row, "chgCount"].ToString();
		DataTable DT_OneRow = dbItemA.ListItem(" sno=" + iSno, F_ProjectCode);
		if (DT_OneRow.Rows.Count <= 0)
		{
			return;
		}
		if (DT_OneRow.Rows[0]["analysis"].ToString().Trim() == "1")
		{
			gridBudget2[gridRow, "Analysis"] = true;
			gridBudget2.Rows[gridRow].Style = gridBudget2.Styles["AnalysisColor"];
			CellRange rg = gridBudget2.GetCellRange(gridRow, gridBudget2.Cols["AnaImg"].SafeIndex);
			rg.Style = gridBudget2.Styles["img"];
			rg.Image = imageList2.Images[0];
		}
		else
		{
			gridBudget2[gridRow, "Analysis"] = false;
			gridBudget2.Rows[gridRow].Style = gridBudget2.Styles["Normal"];
			CellRange rg = gridBudget2.GetCellRange(gridRow, gridBudget2.Cols["AnaImg"].SafeIndex);
			rg.Style = gridBudget2.Styles["img"];
			rg.Image = imageList2.Images[2];
		}
		gridBudget2[gridRow, "ItemNo"] = DT_OneRow.Rows[0]["ItemNo"].ToString().Trim();
		gridBudget2[gridRow, "CName"] = DT_OneRow.Rows[0]["cName"].ToString().Trim();
		gridBudget2[gridRow, "UnitName"] = DT_OneRow.Rows[0]["unitName"].ToString().Trim();
		gridBudget2[gridRow, "PccesCode"] = DT_OneRow.Rows[0]["pccesCode"].ToString().Trim();
		gridBudget2[gridRow, "Memo"] = DT_OneRow.Rows[0]["memo"].ToString().Trim();
		gridBudget2[gridRow, "EName"] = DT_OneRow.Rows[0]["eName"].ToString().Trim();
		gridBudget2[gridRow, "EUnit"] = DT_OneRow.Rows[0]["eUnit"].ToString().Trim();
		gridBudget2[gridRow, "LevelNo"] = DT_OneRow.Rows[0]["levelNo"].ToString().Trim();
		gridBudget2[gridRow, "SNo"] = DT_OneRow.Rows[0]["sno"];
		gridBudget2[gridRow, "Kind"] = DT_OneRow.Rows[0]["kind"].ToString().Trim();
		gridBudget2[gridRow, "PrintNo"] = DT_OneRow.Rows[0]["printNo"].ToString().Trim();
		gridBudget2[gridRow, "Formula"] = DT_OneRow.Rows[0]["Formula"].ToString().Trim();
		gridBudget2[gridRow, "PubCode"] = DT_OneRow.Rows[0]["pubCode"].ToString().Trim();
		gridBudget2[gridRow, "ChgQty"] = DT_OneRow.Rows[0]["chgqty"];
		gridBudget2[gridRow, "ChgCost"] = DT_OneRow.Rows[0]["chgcost"];
		gridBudget2[gridRow, "ChgAmount"] = DT_OneRow.Rows[0]["chgamount"];
		if (DT_OneRow.Rows[0]["kind"].ToString().Trim() == "L")
		{
			gridBudget2[gridRow, "ChgAmount"] = PubTools.Str2Decimal(DT_OneRow.Rows[0]["chgqty"]) * PubTools.Str2Decimal(DT_OneRow.Rows[0]["chgcost"]);
		}
		string sKind = ((DT_OneRow.Rows[0]["kind"].ToString().Length > 0) ? DT_OneRow.Rows[0]["kind"].ToString().ToUpper().Trim() : "");
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
			gridBudget2.Rows[gridRow].Style = gridBudget2.Styles["MainColor"];
			break;
		}
		if (DT_OneRow.Rows[0]["qty"].ToString() != DT_OneRow.Rows[0]["chgqty"].ToString())
		{
			CellRange Crg1 = gridBudget2.GetCellRange(gridRow, gridBudget2.Cols["ChgQty"].SafeIndex);
			Crg1.Style = gridBudget2.Styles["ChgColor"];
		}
		if (DT_OneRow.Rows[0]["cost"].ToString() != DT_OneRow.Rows[0]["ChgCost"].ToString())
		{
			CellRange Crg1 = gridBudget2.GetCellRange(gridRow, gridBudget2.Cols["ChgCost"].SafeIndex);
			Crg1.Style = gridBudget2.Styles["ChgColor"];
		}
		if (DT_OneRow.Rows[0]["amount"].ToString() != DT_OneRow.Rows[0]["ChgAmount"].ToString())
		{
			CellRange Crg1 = gridBudget2.GetCellRange(gridRow, gridBudget2.Cols["ChgAmount"].SafeIndex);
			Crg1.Style = gridBudget2.Styles["ChgColor"];
		}
	}

	private ArrayList GetShareItems(int iRow)
	{
		ArrayList RetV = new ArrayList();
		Node LastNode = gridBudget2.Rows[iRow].Node.GetNode(NodeTypeEnum.LastChild);
		if (LastNode != null)
		{
			int iLastIndex = LastNode.Row.SafeIndex;
			for (int i = iRow; i <= iLastIndex; i++)
			{
				if (gridBudget2[i, "Kind"].ToString().Trim() == "L")
				{
					string sItem = gridBudget2[i, "sNO"].ToString() + "|【" + gridBudget2[i, "ItemNo"].ToString().Trim() + "】" + gridBudget2[i, "CName"].ToString().Trim();
					RetV.Add(sItem);
				}
			}
		}
		return RetV;
	}

	private ArrayList GetShareItems(string sPrintNo, int iRow)
	{
		ArrayList RetV = new ArrayList();
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add("預算書編輯--編輯主項大類(取得可攤提對項列表)");
		ItemA ITMA = new ItemA(aArr);
		ITMA.ps_Issue = gridBudget1[gridBudget1.Row, "chgCount"].ToString();
		DataTable DT_Shares = ITMA.GetCanShareItem(sPrintNo, F_ProjectCode);
		for (int i = 0; i < DT_Shares.Rows.Count; i++)
		{
			string sItem = DT_Shares.Rows[i]["sNo"].ToString().Trim() + "|【" + DT_Shares.Rows[i]["itemNo"].ToString().Trim() + "】" + DT_Shares.Rows[i]["cName"].ToString().Trim();
			RetV.Add(sItem);
		}
		return RetV;
	}

	private string GetShareItemSNo(string sItem_Sno)
	{
		string RetV = "";
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add("取得該主項大煩的攤提項目的sNO");
		ItemA ITM_A = new ItemA(aArr);
		ITM_A.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		ITM_A.ps_projectCode = F_ProjectCode;
		ITM_A.ps_Issue = gridBudget1[gridBudget1.Row, "chgCount"].ToString();
		try
		{
			RetV = ITM_A.GetValue("ShareSno", sItem_Sno, F_ProjectCode);
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudgetChange.cs" + ex.Message);
			MessageBox.Show(this, "Err11:\n" + ex.Message, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		return RetV;
	}

	private void Execute_About()
	{
		FormAbout FMAB = new FormAbout();
		FMAB.ShowDialog();
		FMAB.Close();
		FMAB.Dispose();
		FMAB = null;
	}

	private void DoAssembleCode()
	{
		RemeberOldPrintNo();
		L1[1] = 0;
		L1[2] = 0;
		L1[3] = 0;
		L1[4] = 0;
		L1[5] = 0;
		L1[6] = 0;
		L1[7] = 0;
		L1[8] = 0;
		for (int i = 1; i <= gridBudget2.Rows.Count - 1; i++)
		{
			if (!gridBudget2.Rows[i].IsNode)
			{
				continue;
			}
			if (gridBudget2[i, "PrintNo"] != null)
			{
				string sPNT_NO = gridBudget2[i, "PrintNo"].ToString().Trim();
				if (sPNT_NO == "99999999999999999999999999999999")
				{
					continue;
				}
			}
			gridBudget2[i, "PrintNo"] = AssembleCode((gridBudget2.Rows[i].Node == null) ? 1 : gridBudget2.Rows[i].Node.Level);
			gridBudget2[i, "LevelNo"] = gridBudget2[i, "PrintNo"].ToString().Trim().Length / 4;
		}
	}

	private string AssembleCode(int iLevel)
	{
		string Result = "";
		if (iLevel == 0)
		{
			iLevel = 1;
		}
		for (int i = 1; i <= iLevel; i++)
		{
			switch (i)
			{
			case 1:
				if (iLevel == 1)
				{
					L1[1]++;
					L1[2] = 0;
					L1[3] = 0;
					L1[4] = 0;
					L1[5] = 0;
					L1[6] = 0;
					L1[7] = 0;
					L1[8] = 0;
				}
				Result += L1[1].ToString().PadLeft(4, '0');
				break;
			case 2:
				if (iLevel == 2)
				{
					L1[2]++;
					L1[3] = 0;
					L1[4] = 0;
					L1[5] = 0;
					L1[6] = 0;
					L1[7] = 0;
					L1[8] = 0;
				}
				Result += L1[2].ToString().PadLeft(4, '0');
				break;
			case 3:
				if (iLevel == 3)
				{
					L1[3]++;
					L1[4] = 0;
					L1[5] = 0;
					L1[6] = 0;
					L1[7] = 0;
					L1[8] = 0;
				}
				Result += L1[3].ToString().PadLeft(4, '0');
				break;
			case 4:
				if (iLevel == 4)
				{
					L1[4]++;
					L1[5] = 0;
					L1[6] = 0;
					L1[7] = 0;
					L1[8] = 0;
				}
				Result += L1[4].ToString().PadLeft(4, '0');
				break;
			case 5:
				if (iLevel == 5)
				{
					L1[5]++;
					L1[6] = 0;
					L1[7] = 0;
					L1[8] = 0;
				}
				Result += L1[5].ToString().PadLeft(4, '0');
				break;
			case 6:
				if (iLevel == 6)
				{
					L1[6]++;
					L1[7] = 0;
					L1[8] = 0;
				}
				Result += L1[6].ToString().PadLeft(4, '0');
				break;
			case 7:
				if (iLevel == 7)
				{
					L1[7]++;
					L1[8] = 0;
				}
				Result += L1[7].ToString().PadLeft(4, '0');
				break;
			case 8:
				if (iLevel == 8)
				{
					L1[8]++;
				}
				Result += L1[8].ToString().PadLeft(4, '0');
				break;
			}
		}
		return Result;
	}

	private void Execute_Print()
	{
		FormInvoiceReport FM_INV_RPT = new FormInvoiceReport();
		FM_INV_RPT._ActionName = F_ActionName;
		FM_INV_RPT._ProjectCode = F_ProjectCode;
		FM_INV_RPT._SubProjectCode = F_SubProjectCode;
		FM_INV_RPT._Issue = gridBudget1[gridBudget1.Row, "chgCount"].ToString();
		FM_INV_RPT._UserID = F_UserID;
		FM_INV_RPT.ShowDialog();
		FM_INV_RPT.Close();
		FM_INV_RPT.Dispose();
		FM_INV_RPT = null;
	}

	private void Do_ReCal()
	{
		if (MessageBox.Show(this, "確定要執行重新總計?", "訊問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
		{
			return;
		}
		gridBudget2.Enabled = false;
		FM_INFO = new FormSys_G_Info1();
		FM_INFO._InfoString = "重新總計中，請稍候! ";
		FM_INFO.Owner = this;
		FM_INFO.Show();
		FM_INFO.BringToFront();
		Application.DoEvents();
		Cursor = Cursors.WaitCursor;
		SaveGridDataToItemA();
		string IPStr = CommonMethods.GetIPAddress();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("(LET_DETAIL_SHOW1) 預算變更明細--重新計算--" + F_ProjectCode + "(" + IPStr + ")");
		if (dbItemA != null)
		{
			dbItemA = null;
		}
		if (dbItemA == null)
		{
			dbItemA = new ItemA(aArr);
		}
		dbItemA.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		dbItemA.ps_Issue = gridBudget1[gridBudget1.Row, "chgCount"].ToString();
		Cursor = Cursors.WaitCursor;
		Application.DoEvents();
		string AppLocation = AppDomain.CurrentDomain.BaseDirectory;
		string IsOldReCal = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "BDGT", "IsOldReCal");
		int iResult = 1;
		if (IsOldReCal.ToUpper() == "TRUE")
		{
			iResult = dbItemA.ReCalcCost2(F_ProjectCode, mode: true, noShare: true);
		}
		else if (IsOldReCal.ToUpper() == "FALSE")
		{
			iResult = dbItemA.ReCalcCost2(F_ProjectCode);
		}
		else if (IsOldReCal.ToUpper() == "THIRD")
		{
			dbItemA.ps_SmallCalcuMode = "THIRD";
			iResult = dbItemA.ReCalcCost2(F_ProjectCode);
		}
		else
		{
			iResult = dbItemA.ReCalcCost2(F_ProjectCode);
		}
		if (iResult != 1)
		{
			FM_INFO.Hide();
			string sMessage = "重新總計失敗，請檢查後再執行!\n\n例如:\n(1)單價分析子項引用了與父項相同的工項\n     比如:【清除與掘除】的分析子項又引用了一次【清除與掘除】\n\n(2)單價分析子項沒有設定雜項。\n     因為產生差額要攤給雜項，有單價分析並未設定雜項。\n     可使用【檢視】-->【專案工項維護】-->【計算錯誤項目】幫你篩選出有狀況(2)之項目。\n     或是至【工具】-->【選項...】-->【計算方式】-->勾選【一律不作攤提】";
			if (F_ProjectCode == "ArchEx001" && F_CurrentDBName.ToUpper() == "PCCES")
			{
				sMessage = "此為範例案，請先手動修正第【壹五.3】工項編碼為：[16221535A3]\n此單價分析項未設定雜項，以致沒有差額攤提對象。\n\n";
				for (int z = 1; z < gridBudget1.Rows.Count - 1; z++)
				{
					if (gridBudget2[z, "PccesCode"].ToString().IndexOf("16221535A3") > -1)
					{
						gridBudget1.Row = z;
						gridBudget1.Select();
						break;
					}
				}
			}
			MessageBox.Show(this, sMessage, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else
		{
			MessageBox.Show(this, "重新計算完畢!", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		gridBudget2.Enabled = true;
		gridBudget2.Refresh();
		Cursor = Cursors.WaitCursor;
		Application.DoEvents();
		Do_Change();
		FM_INFO.Close();
		FM_INFO.Dispose();
		Application.DoEvents();
	}

	private void ItemEdit()
	{
		int iRow2 = gridBudget2.Row;
		FormBudgetEditItem FM_EDT = new FormBudgetEditItem();
		FM_EDT._ProjectCode = F_ProjectCode;
		FM_EDT._SubProjectCode = F_SubProjectCode;
		FM_EDT._UserID = F_UserID;
		FM_EDT._ChgCount = PubTools.Str2Int(gridBudget1[gridBudget1.Row, "chgCount"]);
		FM_EDT._DR_forUpd = GetCurrDR(gridBudget2.Row);
		FM_EDT._ChildCount = gridBudget2.Rows[iRow2].Node.Children;
		if (FM_EDT.ShowDialog(this) == DialogResult.OK)
		{
			ArrayList tmp_AL = new ArrayList();
			tmp_AL.Add(F_UserID);
			tmp_AL.Add("契約變更--抓取單一明細項");
			Sub_ChgItemA ChgCom = new Sub_ChgItemA(tmp_AL);
			DataTable DT_TMP2 = ChgCom.ListItem("", F_ProjectCode, F_SubProjectCode, gridBudget1[gridBudget1.Row, "chgCount"].ToString());
			if (DT_TMP2.Rows.Count > 0)
			{
				DataView DV11 = DT_TMP2.DefaultView;
				DV11.RowFilter = " sno=" + gridBudget2[iRow2, "SNo"].ToString();
				gridBudget2[iRow2, "ItemNo"] = DV11[0]["itemno"];
				gridBudget2[iRow2, "CName"] = DV11[0]["cName"];
				gridBudget2[iRow2, "UnitName"] = DV11[0]["unitName"];
				gridBudget2[iRow2, "Qty"] = DV11[0]["qty"];
				gridBudget2[iRow2, "Cost"] = DV11[0]["cost"];
				gridBudget2[iRow2, "Amount"] = DV11[0]["amount"];
				gridBudget2[iRow2, "ChgQty"] = DV11[0]["chgqty"];
				gridBudget2[iRow2, "ChgCost"] = DV11[0]["chgcost"];
				gridBudget2[iRow2, "ChgAmount"] = DV11[0]["chgamount"];
				gridBudget2[iRow2, "PccesCode"] = DV11[0]["pccesCode"];
				gridBudget2[iRow2, "Memo"] = DV11[0]["memo"];
				gridBudget2[iRow2, "EName"] = DV11[0]["eName"];
				gridBudget2[iRow2, "EUnit"] = DV11[0]["eUnit"];
				gridBudget2[iRow2, "LevelNo"] = DV11[0]["levelNo"];
				gridBudget2[iRow2, "SNo"] = DV11[0]["sno"];
				gridBudget2[iRow2, "Kind"] = DV11[0]["kind"];
				gridBudget2[iRow2, "PrintNo"] = DV11[0]["printNo"].ToString().Trim();
				gridBudget2[iRow2, "Formula"] = DV11[0]["Formula"];
				gridBudget2[iRow2, "PubCode"] = DV11[0]["pubCode"];
				if (gridBudget2[iRow2, "Kind"].ToString() == "B")
				{
					try
					{
						gridBudget2.Rows[iRow2].Style = gridBudget2.Styles["MainColor"];
					}
					catch (Exception ex)
					{
						CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudgetChange.cs" + ex.Message);
					}
				}
			}
		}
		FM_EDT.Close();
		FM_EDT.Dispose();
		FM_EDT = null;
	}

	private void ItemDelete()
	{
		if (MessageBox.Show(this, "確定要刪除嗎?", "訊問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
		{
			return;
		}
		string sIssue = gridBudget1[gridBudget1.Row, "chgCount"].ToString();
		string sSNO = gridBudget2[gridBudget2.Row, "SNo"].ToString();
		ArrayList tmp_AL = new ArrayList();
		tmp_AL.Add(F_UserID);
		tmp_AL.Add("契約變更--明細項刪除");
		Sub_ChgItemA ChgCom = new Sub_ChgItemA(tmp_AL);
		if (gridBudget2.Rows[gridBudget2.Row].Node.Children > 0)
		{
			if (MessageBox.Show(this, "你要刪除的項目尚有子項，如此會一併刪除子項，確定要執行嗎?", "訊問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				Cursor = Cursors.WaitCursor;
				int iStart = gridBudget2.Rows[gridBudget2.Row].Node.Row.SafeIndex;
				Node LastChild = gridBudget2.Rows[gridBudget2.Row].Node.GetNode(NodeTypeEnum.LastChild);
				int iEnd = LastChild.Row.SafeIndex;
				for (int i = iEnd; i >= iStart; i--)
				{
					sSNO = gridBudget2[i, "SNo"].ToString();
					ChgCom.DeleItem(F_ProjectCode, F_SubProjectCode, sIssue, sSNO);
					gridBudget2.RemoveItem(i);
				}
			}
		}
		else
		{
			Cursor = Cursors.WaitCursor;
			ChgCom.DeleItem(F_ProjectCode, F_SubProjectCode, sIssue, sSNO);
			gridBudget2.RemoveItem(gridBudget2.Row);
		}
		DoAssembleCode();
		SaveGridDataToItemA();
		LoadData2();
		BindToGrid2();
		Cursor = Cursors.Default;
	}

	private void InsSibling()
	{
		Cursor = Cursors.WaitCursor;
		int iChildCount = 0;
		Node LastNode;
		try
		{
			LastNode = ((gridBudget2.Rows[gridBudget2.Row].Node.Children <= 0) ? gridBudget2.Rows[gridBudget2.Row].Node.GetNode(NodeTypeEnum.LastSibling) : gridBudget2.Rows[gridBudget2.Row].Node.GetNode(NodeTypeEnum.LastChild));
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudgetChange.cs" + ex.Message);
			LastNode = gridBudget2.Rows[gridBudget2.Row].Node.GetNode(NodeTypeEnum.LastSibling);
			return;
		}
		try
		{
			int iLastIndex = LastNode.Row.SafeIndex;
			iChildCount = ((!(gridBudget2[iLastIndex, "PrintNo"].ToString().Trim() == "99999999999999999999999999999999") && !(gridBudget2[iLastIndex, "Kind"].ToString().Trim() == "Z")) ? (LastNode.Row.SafeIndex - gridBudget2.Row) : (LastNode.Row.SafeIndex - gridBudget2.Row - 1));
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudgetChange.cs" + ex.Message);
			iChildCount = 0;
		}
		int iStartRow = gridBudget2.Row + iChildCount + 1;
		int iNewLevel = gridBudget2.Rows[gridBudget2.Row].Node.Level;
		string sItem = "\t\t\t\t\t0\tfalse\t0\t0\t0\t0\t0\t\t\t\t\t" + iNewLevel + "\tL\tfalse\t\t\t\t\tfalse";
		gridBudget2.AddItem(sItem, iStartRow);
		gridBudget2.Rows[iStartRow].IsNode = true;
		gridBudget2.Rows[iStartRow].Node.Level = iNewLevel;
		DoAssembleCode();
		SaveGridDataToItemA();
		gridBudget2.Row = iStartRow;
		Cursor = Cursors.Default;
	}

	private void InsChild()
	{
		Cursor = Cursors.WaitCursor;
		int iChildCount = 0;
		Node LastNode;
		try
		{
			LastNode = gridBudget2.Rows[gridBudget2.Row].Node.GetNode(NodeTypeEnum.LastChild);
			if (LastNode.Children > 0)
			{
				LastNode = LastNode.GetNode(NodeTypeEnum.LastChild);
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudgetChange.cs" + ex.Message);
			LastNode = gridBudget2.Rows[gridBudget2.Row].Node;
		}
		try
		{
			int iLastIndex = LastNode.Row.SafeIndex;
			iChildCount = ((!(gridBudget2[iLastIndex, "PrintNo"].ToString().Trim() == "99999999999999999999999999999999") && !(gridBudget2[iLastIndex, "Kind"].ToString().Trim() == "Z")) ? (LastNode.Row.SafeIndex - gridBudget2.Row) : (LastNode.Row.SafeIndex - gridBudget2.Row - 1));
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudgetChange.cs" + ex.Message);
			iChildCount = 0;
		}
		int iStartRow = gridBudget2.Row + iChildCount + 1;
		int iNewLevel = gridBudget2.Rows[gridBudget2.Row].Node.Level + 1;
		string sItem = "\t\t\t\t\t0\tfalse\t0\t0\t0\t0\t0\t\t\t\t\t" + iNewLevel + "\tL\tfalse\t\t\t\t\tfalse";
		gridBudget2.AddItem(sItem, iStartRow);
		gridBudget2.Rows[iStartRow].IsNode = true;
		gridBudget2.Rows[iStartRow].Node.Level = iNewLevel;
		DoAssembleCode();
		SaveGridDataToItemA();
		gridBudget2.Row = iStartRow;
		Cursor = Cursors.Default;
	}

	private void Do_ShowList()
	{
		if (panel4.Visible)
		{
			ultraToolbarsManager1.Tools["mnuViewHide1"].SharedProps.Caption = "顯示變更次別一覽表";
			panel4.Visible = false;
		}
		else
		{
			ultraToolbarsManager1.Tools["mnuViewHide1"].SharedProps.Caption = "隱藏變更次別一覽表";
			panel4.Height = 188;
			panel4.Visible = true;
		}
	}

	private int GetMaxSNoFromGrid()
	{
		int iMax = -1;
		for (int i = 1; i < gridBudget2.Rows.Count; i++)
		{
			if (gridBudget2.Rows[i]["SNo"] != null && gridBudget2.Rows[i]["SNo"].ToString() != "" && PubTools.Str2Int(gridBudget2.Rows[i]["SNo"]) > iMax && PubTools.Str2Int(gridBudget2.Rows[i]["SNo"]) < 999999)
			{
				iMax = PubTools.Str2Int(gridBudget2.Rows[i]["SNo"]);
			}
		}
		return iMax;
	}

	private DataRow GetCurrDR(int iIndex)
	{
		DataView dv = new DataView(DT2);
		dv.RowFilter = "PrintNo = '" + gridBudget2[iIndex, "PrintNo"].ToString().Trim() + "'";
		DataTable DT_TMP = DT2.Clone();
		DataRow DR1 = DT_TMP.NewRow();
		DR1["projectCode"] = F_ProjectCode;
		DR1["sProj"] = F_SubProjectCode;
		DR1["sNo"] = ((gridBudget2[iIndex, "SNo"] != null) ? gridBudget2[iIndex, "SNo"] : ((object)(GetMaxSNoFromGrid() + 1)));
		DR1["printNo"] = gridBudget2[iIndex, "PrintNo"].ToString().Trim();
		DR1["pubCode"] = gridBudget2[iIndex, "PubCode"];
		DR1["itemNo"] = gridBudget2[iIndex, "ItemNo"];
		DR1["levelNo"] = gridBudget2[iIndex, "levelNo"];
		DR1["cName"] = gridBudget2[iIndex, "CName"];
		DR1["eName"] = gridBudget2[iIndex, "EName"];
		DR1["unitName"] = gridBudget2[iIndex, "UnitName"];
		DR1["kind"] = gridBudget2[iIndex, "Kind"];
		DR1["cost"] = gridBudget2[iIndex, "Cost"];
		DR1["qty"] = gridBudget2[iIndex, "Qty"];
		DR1["amount"] = gridBudget2[iIndex, "Amount"];
		DR1["memo"] = gridBudget2[iIndex, "Memo"];
		if (dv.Count > 0)
		{
			DR1["setDecimal"] = dv[0]["setDecimal"];
			DR1["mRate"] = dv[0]["mRate"];
			DR1["lRate"] = dv[0]["lRate"];
			DR1["eRate"] = dv[0]["eRate"];
			DR1["wRate"] = dv[0]["wRate"];
			DR1["rate"] = dv[0]["rate"];
			DR1["srcCode"] = dv[0]["srcCode"];
			DR1["share"] = dv[0]["share"];
			DR1["dsctLock"] = dv[0]["dsctLock"];
			DR1["shareSno"] = dv[0]["shareSno"];
		}
		else
		{
			DR1["setDecimal"] = 0;
			DR1["mRate"] = 0;
			DR1["lRate"] = 0;
			DR1["eRate"] = 0;
			DR1["wRate"] = 0;
			DR1["rate"] = 0;
			DR1["srcCode"] = 0;
			DR1["share"] = "";
			DR1["dsctLock"] = "";
		}
		DR1["eUnit"] = gridBudget2[iIndex, "EUnit"];
		DR1["Formula"] = gridBudget2[iIndex, "Formula"];
		DR1["chgqty"] = gridBudget2[iIndex, "ChgQty"];
		DR1["chgcost"] = gridBudget2[iIndex, "ChgCost"];
		DR1["chgAmount"] = ((gridBudget2[iIndex, "ChgAmount"] == null) ? ((object)0) : gridBudget2[iIndex, "ChgAmount"]);
		DR1["pccesCode"] = gridBudget2[iIndex, "PccesCode"];
		DR1["analysis"] = (((bool)gridBudget2[iIndex, "analysis"]) ? "1" : "");
		DR1["analysisqty"] = 0;
		return DR1;
	}

	private void SaveGridDataToItemA()
	{
		string sIssue = gridBudget1[gridBudget1.Row, "chgCount"].ToString();
		int iMaxSNo = GetMaxSNoFromGrid();
		ArrayList tmp_AL = new ArrayList();
		tmp_AL.Add(F_UserID);
		tmp_AL.Add("契約變更刪除");
		Sub_ChgItemA ChgCom = new Sub_ChgItemA(tmp_AL);
		for (int i = 1; i < gridBudget2.Rows.Count; i++)
		{
			if (gridBudget2.Rows[i]["SNo"] == null || gridBudget2.Rows[i]["SNo"].ToString() == "")
			{
				string f_ProjectCode = F_ProjectCode;
				string f_SubProjectCode = F_SubProjectCode;
				int num = ++iMaxSNo;
				ChgCom.InseItem(f_ProjectCode, f_SubProjectCode, sIssue, num.ToString(), GetCurrDR(i));
				gridBudget2.Rows[i]["SNo"] = iMaxSNo;
			}
			else
			{
				ChgCom.InseItem(F_ProjectCode, F_SubProjectCode, sIssue, gridBudget2.Rows[i]["SNo"].ToString(), GetCurrDR(i));
			}
		}
	}

	private void Do_Delete()
	{
		string sIssue = gridBudget1[gridBudget1.Row, "chgCount"].ToString();
		if (gridBudget1.Rows.Count - 1 > PubTools.Str2Int(sIssue))
		{
			MessageBox.Show(this, "有較大的次別存在，不可刪除此次別資料", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else if (MessageBox.Show(this, "確定要刪除第 " + sIssue + " 次契約變更嗎?", "訊問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			ArrayList tmp_AL = new ArrayList();
			tmp_AL.Add(F_UserID);
			tmp_AL.Add("契約變更刪除");
			sub_ChgMain SubChgCom = new sub_ChgMain(tmp_AL);
			SubChgCom.DeleItem(PubTools.Str2Int(sIssue), F_ProjectCode, F_SubProjectCode);
			SubChgCom = null;
			LoadData();
			BindToGrid();
			Do_Change();
		}
	}

	private void CloseThisForm()
	{
		string sWarning = "確定要結束 ?";
		if (MessageBox.Show(this, sWarning, "契約變更", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			(base.ParentForm as frmPccesMain).LeftPanel.Width = 160;
			Close();
		}
	}

	private void ExecuteAddNew(string sMode)
	{
		FormBudgetChange_Addnew FM_BDGT_CHG_ADD = new FormBudgetChange_Addnew();
		FM_BDGT_CHG_ADD._UserID = F_UserID;
		FM_BDGT_CHG_ADD._ProjectCode = _ProjectCode;
		FM_BDGT_CHG_ADD._ProjectNameC = _ProjectNameC;
		FM_BDGT_CHG_ADD._SubProjectCode = F_SubProjectCode;
		FM_BDGT_CHG_ADD._EditMode = sMode;
		if (sMode == "EDIT")
		{
			FM_BDGT_CHG_ADD._ChgCount = gridBudget1[gridBudget1.Row, "chgCount"].ToString().Trim();
		}
		if (FM_BDGT_CHG_ADD.ShowDialog(this) == DialogResult.OK)
		{
			LoadData();
			BindToGrid();
			ArrayList tmp_AL1 = new ArrayList();
			tmp_AL1 = new ArrayList();
			tmp_AL1.Add(F_UserID);
			tmp_AL1.Add("(LET_CHG_ADD) 新增預算變更主檔");
			sub_ChgMain chgcom = new sub_ChgMain(tmp_AL1);
			int getMaxNo = chgcom.getMaxNo(F_ProjectCode, F_SubProjectCode);
			gridBudget1.Row = getMaxNo;
			Do_Change();
		}
		FM_BDGT_CHG_ADD.Close();
		FM_BDGT_CHG_ADD.Dispose();
		FM_BDGT_CHG_ADD = null;
	}

	private void Do_ToolBarFind()
	{
		if (gridBudget1.Row <= 1)
		{
			return;
		}
		int iStart = gridBudget1.Row + 1;
		string sSearchText = ((ComboBoxTool)ultraToolbarsManager1.Tools["mnu_Cbo1"]).Text.Trim();
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

	private void Do_Change()
	{
		if (gridBudget1.Rows.Count <= 1)
		{
			gridBudget2.Rows.Count = 1;
			return;
		}
		iCountNum = gridBudget1.Rows.Count - 1;
		LoadData2();
		BindToGrid2();
	}

	private void LoadData2()
	{
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(LET_DETAIL_SHOW1) 顯示預算變更明細");
		Sub_ChgItemA ChgCom = new Sub_ChgItemA(tmp_AL1);
		PubTools.WriteRoughlyLog(tmp_AL1);
		string ls_ChgCount = "";
		sub_ChgMain chgcom = new sub_ChgMain(tmp_AL1);
		if (Firstflag != "")
		{
			gridBudget1.Row = iCountNum;
			ls_ChgCount = gridBudget1[iCountNum, "chgCount"].ToString();
			Firstflag = "";
		}
		else
		{
			ls_ChgCount = gridBudget1[gridBudget1.Row, "chgCount"].ToString();
		}
		F_chgCount = ls_ChgCount;
		if (dbItemA != null)
		{
			dbItemA = null;
		}
		if (dbItemA == null)
		{
			dbItemA = new ItemA(tmp_AL1);
		}
		dbItemA.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		dbItemA.ps_projectCode = F_ProjectCode;
		dbItemA.ps_Issue = ls_ChgCount;
		DT2 = dbItemA.ListItem("", F_ProjectCode);
		lblThisIssue.Text = "【目前編輯次別：" + ls_ChgCount + " 】";
	}

	private void BindToGrid2()
	{
		int iLevel = 0;
		ultraToolbarsManager1.BeginUpdate();
		ultraToolbarsManager1.Enabled = false;
		gridBudget2.Visible = false;
		RememberColsProps();
		CellStyle CS1 = gridBudget2.Styles.Add("AnalysisColor");
		CellStyle CS2 = gridBudget2.Styles.Add("MainColor");
		CellStyle CS8 = gridBudget2.Styles.Add("ChgColor");
		CellStyle CS9 = gridBudget2.Styles.Add("IsSharedColor");
		CS1.ForeColor = Color.Red;
		CS2.ForeColor = Color.Blue;
		CS8.BackColor = Color.SkyBlue;
		CS9.ForeColor = Color.Plum;
		gridBudget2.Clear(ClearFlags.All);
		int iRows = DT2.Rows.Count + 1;
		DataRows_AfterBinding = DT2.Rows.Count;
		gridBudget2.Rows.Count = iRows;
		gridBudget2.Select(0, 0);
		SetGridColumn();
		double aTotal = 0.0;
		for (int i = 0; i < DT2.Rows.Count; i++)
		{
			if (DT2.Rows[i]["analysis"].ToString().Trim() == "1")
			{
				gridBudget2[i + 1, "Analysis"] = true;
				gridBudget2.Rows[i + 1].Style = gridBudget2.Styles["AnalysisColor"];
				CellRange rg = gridBudget2.GetCellRange(i + 1, gridBudget2.Cols["AnaImg"].SafeIndex);
				rg.Style = gridBudget2.Styles["img"];
				rg.Image = imageList2.Images[0];
			}
			else
			{
				gridBudget2[i + 1, "Analysis"] = false;
			}
			CellRange RAccMode = gridBudget2.GetCellRange(i + 1, gridBudget2.Cols["AccMode"].SafeIndex, i + 1, gridBudget2.Cols["AccMode"].SafeIndex);
			RAccMode.Style = gridBudget2.Styles["ComboList"];
			string sKind = DT2.Rows[i]["Kind"].ToString().Trim().ToUpper();
			if (sKind != "W")
			{
				gridBudget2.Rows[i + 1].Style = gridBudget2.Styles["MainColor"];
			}
			if (DT2.Rows[i]["AccMode"] != null)
			{
				if (DT2.Rows[i]["AccMode"].ToString() == "0")
				{
					gridBudget2[i + 1, "AccMode"] = "警告但可存檔";
				}
				else if (DT2.Rows[i]["AccMode"].ToString() == "1")
				{
					gridBudget2[i + 1, "AccMode"] = "警告且不可存檔";
				}
				else if (DT2.Rows[i]["AccMode"].ToString() == "2")
				{
					gridBudget2[i + 1, "AccMode"] = "略過";
				}
			}
			gridBudget2[i + 1, "ItemNo"] = DT2.Rows[i]["itemno"];
			gridBudget2[i + 1, "CName"] = DT2.Rows[i]["cName"];
			gridBudget2[i + 1, "UnitName"] = DT2.Rows[i]["unitName"];
			gridBudget2[i + 1, "Qty"] = DT2.Rows[i]["qty"];
			gridBudget2[i + 1, "Cost"] = DT2.Rows[i]["cost"];
			gridBudget2[i + 1, "Amount"] = DT2.Rows[i]["amount"];
			gridBudget2[i + 1, "ChgQty"] = DT2.Rows[i]["chgqty"];
			gridBudget2[i + 1, "ChgCost"] = DT2.Rows[i]["chgcost"];
			gridBudget2[i + 1, "ChgAmount"] = ((DT2.Rows[i]["Kind"].ToString().Trim() != "B") ? ((object)(PubTools.ARound(PubTools.Str2Double(DT2.Rows[i]["chgqty"]), F_MainQty) * PubTools.ARound(PubTools.Str2Double(DT2.Rows[i]["chgcost"]), F_MainCst))) : DT2.Rows[i]["chgamount"]);
			gridBudget2[i + 1, "PccesCode"] = DT2.Rows[i]["pccesCode"];
			gridBudget2[i + 1, "Memo"] = DT2.Rows[i]["memo"];
			gridBudget2[i + 1, "EName"] = DT2.Rows[i]["eName"];
			gridBudget2[i + 1, "EUnit"] = DT2.Rows[i]["eUnit"];
			gridBudget2[i + 1, "LevelNo"] = DT2.Rows[i]["levelNo"];
			gridBudget2[i + 1, "SNo"] = DT2.Rows[i]["sno"];
			gridBudget2[i + 1, "Kind"] = DT2.Rows[i]["kind"];
			gridBudget2[i + 1, "PrintNo"] = DT2.Rows[i]["printNo"].ToString().Trim();
			gridBudget2[i + 1, "Formula"] = DT2.Rows[i]["Formula"];
			gridBudget2[i + 1, "PubCode"] = DT2.Rows[i]["pubCode"];
			if (DT2.Rows[i]["qty"].ToString() != DT2.Rows[i]["chgqty"].ToString())
			{
				CellRange Crg1 = gridBudget2.GetCellRange(i + 1, gridBudget2.Cols["ChgQty"].SafeIndex);
				Crg1.Style = CS8;
			}
			if (DT2.Rows[i]["cost"].ToString() != DT2.Rows[i]["ChgCost"].ToString())
			{
				CellRange Crg1 = gridBudget2.GetCellRange(i + 1, gridBudget2.Cols["ChgCost"].SafeIndex);
				Crg1.Style = CS8;
			}
			if (DT2.Rows[i]["amount"].ToString() != DT2.Rows[i]["ChgAmount"].ToString())
			{
				CellRange Crg1 = gridBudget2.GetCellRange(i + 1, gridBudget2.Cols["ChgAmount"].SafeIndex);
				Crg1.Style = CS8;
			}
			if (gridBudget2[i + 1, "Kind"] != null)
			{
				gridBudget2.Rows[i + 1].IsNode = true;
			}
			if (DT2.Rows[i]["PrintNo"].ToString().Trim() == "".PadLeft(32, '9'))
			{
				gridBudget2.Rows[i + 1].Node.Level = 1;
				aTotal = PubTools.Str2Double(PubTools.ARound(DT2.Rows[i]["ChgAmount"], F_MainAmt));
			}
			else
			{
				gridBudget2.Rows[i + 1].Node.Level = Convert.ToInt32(DT2.Rows[i]["PrintNo"].ToString().Trim().Length / 4);
			}
			if (gridBudget2.Rows[i + 1].Node.Level > iLevel)
			{
				iLevel = gridBudget2.Rows[i + 1].Node.Level;
			}
			gridBudget2[i + 1, "IsShared"] = DT2.Rows[i]["share"];
			if (DT2.Rows[i]["share"] != null && DT2.Rows[i]["share"].ToString().Trim() != "")
			{
				gridBudget2.Rows[i + 1].Style = gridBudget2.Styles["IsSharedColor"];
			}
		}
		string sIssue = gridBudget1[gridBudget1.Row, "chgCount"].ToString();
		if (gridBudget1.Rows.Count - 1 > PubTools.Str2Int(sIssue))
		{
			gridBudget2.AllowEditing = false;
		}
		else
		{
			gridBudget2.AllowEditing = true;
		}
		gridBudget2.Visible = true;
		gridBudget2.Invalidate();
		ultraToolbarsManager1.Enabled = true;
		ultraToolbarsManager1.EndUpdate();
		SwitchToCorrectLevelStatus(iLevel);
		SetColsEditSymbol();
		lblTotal.Text = string.Format("{0:N" + F_MainAmt + "}", aTotal);
		ultraStatusBar2.Panels[0].Text = "資料筆數：" + DT2.Rows.Count;
		LoadData();
		BindToGrid();
	}

	private void SetColsEditSymbol()
	{
		for (int i = 1; i < gridBudget2.Cols.Count; i++)
		{
			if (gridBudget2.Cols[i].AllowEditing)
			{
				CellRange rg = gridBudget2.GetCellRange(0, i);
				rg.Style = gridBudget2.Styles["EditMode"];
				rg.Image = imageList2.Images[1];
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

	private void ultraToolbarsManager1_BeforeToolbarListDropdown(object sender, BeforeToolbarListDropdownEventArgs e)
	{
		e.Cancel = true;
	}

	private void ultraButton2_Click(object sender, EventArgs e)
	{
		Do_ShowList();
	}

	private void BtnSwitchProject_Click(object sender, EventArgs e)
	{
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
			BindToGrid();
			Do_Change();
		}
		Cursor = Cursors.Default;
	}

	private void gridBudget1_Click(object sender, EventArgs e)
	{
		Do_Change();
	}

	private void gridBudget2_MouseDown(object sender, MouseEventArgs e)
	{
		CheckToolMode();
	}

	private void CheckToolMode()
	{
		if (gridBudget1.Row <= 0)
		{
			return;
		}
		string l_count = gridBudget1[gridBudget1.Row, "chgCount"].ToString();
		if (gridBudget2.Rows.Count <= 1)
		{
			ultraToolbarsManager1.Tools["mnuEDDelItem"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuEDEdtItem"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuSibling"].SharedProps.Enabled = false;
			return;
		}
		int rowIndex = gridBudget2.MouseRow;
		gridBudget2.Row = rowIndex;
		if (PubTools.Str2Double(gridBudget2[gridBudget2.Row, "Qty"]) == 0.0 && PubTools.Str2Double(gridBudget2[gridBudget2.Row, "Cost"]) == 0.0 && PubTools.Str2Double(gridBudget2[gridBudget2.Row, "Amount"]) == 0.0)
		{
			if (gridBudget2.Row == 0 || (gridBudget2[gridBudget2.Row, "Kind"].ToString() != "B" && gridBudget2[gridBudget2.Row, "Kind"].ToString() != "L"))
			{
				ultraToolbarsManager1.Tools["mnuEDDelItem"].SharedProps.Enabled = false;
				ultraToolbarsManager1.Tools["mnuEDEdtItem"].SharedProps.Enabled = false;
				ultraToolbarsManager1.Tools["mnuPopIns"].SharedProps.Enabled = false;
			}
			else
			{
				ultraToolbarsManager1.Tools["mnuEDDelItem"].SharedProps.Enabled = true;
				ultraToolbarsManager1.Tools["mnuEDEdtItem"].SharedProps.Enabled = true;
				ultraToolbarsManager1.Tools["mnuPopIns"].SharedProps.Enabled = true;
				ultraToolbarsManager1.Tools["mnuSibling"].SharedProps.Enabled = true;
			}
		}
		else
		{
			ultraToolbarsManager1.Tools["mnuEDDelItem"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuEDEdtItem"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuSibling"].SharedProps.Enabled = false;
		}
		if (gridBudget2[gridBudget2.Row, "PrintNo"].ToString().Trim().Length == 4)
		{
			ultraToolbarsManager1.Tools["mnuPopIns"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuChild"].SharedProps.Enabled = true;
		}
		else if (PubTools.Str2Double(gridBudget2[gridBudget2.Row, "Qty"]) == 0.0 && PubTools.Str2Double(gridBudget2[gridBudget2.Row, "Cost"]) == 0.0 && PubTools.Str2Double(gridBudget2[gridBudget2.Row, "Amount"]) == 0.0)
		{
			if (gridBudget2[gridBudget2.Row, "Kind"].ToString().Trim() == "B")
			{
				ultraToolbarsManager1.Tools["mnuPopIns"].SharedProps.Enabled = true;
				ultraToolbarsManager1.Tools["mnuChild"].SharedProps.Enabled = true;
			}
			else
			{
				ultraToolbarsManager1.Tools["mnuChild"].SharedProps.Enabled = false;
			}
		}
		else
		{
			ultraToolbarsManager1.Tools["mnuChild"].SharedProps.Enabled = false;
		}
		if (PubTools.Str2Decimal(gridBudget2[rowIndex, "Qty"]) == 0m && PubTools.Str2Decimal(gridBudget2[rowIndex, "Cost"]) == 0m && PubTools.Str2Decimal(gridBudget2[rowIndex, "Amount"]) == 0m)
		{
			ultraToolbarsManager1.Tools["PopMenu1_Delete"].SharedProps.Enabled = true;
		}
		else
		{
			ultraToolbarsManager1.Tools["PopMenu1_Delete"].SharedProps.Enabled = false;
		}
		if (gridBudget2[rowIndex, "Kind"].ToString() == "W")
		{
			ultraToolbarsManager1.Tools["PopMnuMainChild"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuEditMain"].SharedProps.Enabled = false;
		}
		else
		{
			ultraToolbarsManager1.Tools["PopMnuMainChild"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuEditMain"].SharedProps.Enabled = true;
		}
		if (PubTools.Str2Int(l_count) != iCountNum)
		{
			ultraToolbarsManager1.Tools["PopupMenu2"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["PopMenu1_Delete"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["PopMnuMainSibling"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["PopMnuMainChild"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuEditMain"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuDetailEdit_NewWItm"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["PopMnuPickWK_Mrs"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuReCal"].SharedProps.Enabled = false;
		}
		else
		{
			ultraToolbarsManager1.Tools["PopMnuMainSibling"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["PopMnuMainChild"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuDetailEdit_NewWItm"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["PopMnuPickWK_Mrs"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuReCal"].SharedProps.Enabled = true;
		}
	}

	private void gridBudget2_AfterEdit(object sender, RowColEventArgs e)
	{
		DataRow DR2 = GetCurrDR(gridBudget2.Row);
		switch (gridBudget2.Cols[e.Col].Name.ToUpper())
		{
		case "CHGQTY":
			DR2["chgQty"] = PubTools.Str2Double(gridBudget2[gridBudget2.Row, "ChgQty"]);
			if (PubTools.Str2Double(gridBudget2[gridBudget2.Row, "ChgCost"]) == 0.0)
			{
				gridBudget2[gridBudget2.Row, "ChgCost"] = gridBudget2[gridBudget2.Row, "Cost"];
				DR2["chgCost"] = PubTools.Str2Double(gridBudget2[gridBudget2.Row, "ChgCost"]);
			}
			break;
		case "CHGCOST":
			DR2["chgCost"] = PubTools.Str2Double(gridBudget2[gridBudget2.Row, "ChgCost"]);
			if (PubTools.Str2Double(gridBudget2[gridBudget2.Row, "ChgQty"]) == 0.0)
			{
				gridBudget2[gridBudget2.Row, "ChgQty"] = gridBudget2[gridBudget2.Row, "Qty"];
				DR2["chgQty"] = PubTools.Str2Double(gridBudget2[gridBudget2.Row, "ChgQty"]);
			}
			break;
		}
		ArrayList tmp_AL = new ArrayList();
		tmp_AL.Add(F_UserID);
		tmp_AL.Add("契約變更更新");
		if (dbItemA != null)
		{
			dbItemA = null;
		}
		if (dbItemA == null)
		{
			dbItemA = new ItemA(tmp_AL);
		}
		dbItemA.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		dbItemA.ps_projectCode = F_ProjectCode;
		dbItemA.ps_sNo = gridBudget2[gridBudget2.Row, "sNo"].ToString();
		dbItemA.ps_Issue = gridBudget1[gridBudget1.Row, "chgCount"].ToString();
		if (gridBudget2.Cols[e.Col].Name.ToUpper() == "CHGCOST")
		{
			dbItemA.ps_ChgCost = gridBudget2[gridBudget2.Row, "ChgCost"].ToString();
		}
		else if (gridBudget2.Cols[e.Col].Name.ToUpper() == "CHGQTY")
		{
			dbItemA.ps_ChgQty = gridBudget2[gridBudget2.Row, "ChgQty"].ToString();
		}
		if (gridBudget2.Cols[e.Col].Name.ToUpper() == "CHGQTY" || gridBudget2.Cols[e.Col].Name.ToUpper() == "CHGCOST")
		{
			gridBudget2[e.Row, "ChgAmount"] = PubTools.Str2Double(gridBudget2[e.Row, "ChgQty"]) * PubTools.Str2Double(gridBudget2[e.Row, "ChgCost"]);
			CalcuParent(e.Row);
		}
		if (gridBudget2.Cols[e.Col].Name == "AccMode")
		{
			if (gridBudget2[e.Row, "AccMode"].ToString() == "警告但可存檔")
			{
				dbItemA.ps_AccMode = "0";
			}
			else if (gridBudget2[e.Row, "AccMode"].ToString() == "警告且不可存檔")
			{
				dbItemA.ps_AccMode = "1";
			}
			else if (gridBudget2[e.Row, "AccMode"].ToString() == "略過")
			{
				dbItemA.ps_AccMode = "2";
			}
		}
		dbItemA.UpdItem();
		if (dbMrsBaseA != null)
		{
			dbMrsBaseA = null;
		}
		if (dbMrsBaseA == null)
		{
			dbMrsBaseA = new MrsBaseA(F_UserID, tmp_AL);
		}
		if (gridBudget2[gridBudget2.Row, "PccesCode"].ToString() != "")
		{
			dbMrsBaseA.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
			dbMrsBaseA.ps_projectcode = F_ProjectCode;
			dbMrsBaseA.ps_Issue = gridBudget1[gridBudget1.Row, "chgCount"].ToString();
			dbMrsBaseA.ps_pccesCode = gridBudget2[gridBudget2.Row, "PccesCode"].ToString();
			if (gridBudget2.Cols[e.Col].Name.ToUpper() == "CHGCOST")
			{
				dbMrsBaseA.ps_cost = gridBudget2[gridBudget2.Row, "ChgCost"].ToString();
			}
			dbMrsBaseA.UpdItem();
			dbMrsBaseA = null;
		}
		ultraToolbarsManager1.Enabled = true;
	}

	private void CalcuParent(int iRow)
	{
		try
		{
			gridBudget2[iRow, "ChgAmount"] = PubTools.Str2Double(gridBudget2[iRow, "ChgQty"]) * PubTools.Str2Double(gridBudget2[iRow, "ChgCost"]);
			Node nd1 = gridBudget2.Rows[iRow].Node;
			Node ndPa = nd1.GetNode(NodeTypeEnum.Parent);
			int iPaLastChild = ndPa.GetNode(NodeTypeEnum.LastChild).Row.SafeIndex;
			decimal dPaAmount = 0m;
			for (int i = ndPa.Row.SafeIndex + 1; i <= iPaLastChild; i++)
			{
				if (gridBudget2.Rows[i].Node.Level == ndPa.Level + 1 && gridBudget2[i, "Kind"].ToString() != "Z")
				{
					dPaAmount += PubTools.Str2Decimal(gridBudget2[i, "ChgAmount"]);
				}
			}
			gridBudget2[ndPa.Row.SafeIndex, "ChgAmount"] = dPaAmount;
			gridBudget2[ndPa.Row.SafeIndex, "ChgCost"] = dPaAmount / PubTools.Str2Decimal(gridBudget2[ndPa.Row.SafeIndex, "ChgQty"]);
			if (ndPa.GetNode(NodeTypeEnum.Parent) != null)
			{
				CalcuParent(ndPa.Row.SafeIndex);
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudget.cs--CalcuParent" + ex.Message);
		}
	}

	private void gridBudget2_StartEdit(object sender, RowColEventArgs e)
	{
		ultraToolbarsManager1.Enabled = false;
	}

	private void gridBudget2_MouseMove(object sender, MouseEventArgs e)
	{
		bool flag = false;
		if (gridBudget2.MouseRow <= 0 || gridBudget2.MouseCol <= 0)
		{
			return;
		}
		int rowIndex = gridBudget2.MouseRow;
		int colIndex = gridBudget1.MouseCol;
		if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right && gridBudget2.Cols[colIndex].Name == "AnaImg")
		{
			if (gridBudget2[rowIndex, "Analysis"] != null && rowIndex > 0 && (bool)gridBudget2[rowIndex, "Analysis"])
			{
				Cursor = Cursors.Hand;
			}
		}
		else
		{
			Cursor = Cursors.Default;
		}
	}

	private void gridBudget2_Click(object sender, EventArgs e)
	{
		if (gridBudget2.MouseRow <= 0 || gridBudget2.MouseCol <= 0)
		{
			return;
		}
		int rowIndex = gridBudget2.MouseRow;
		try
		{
			if (Cursor == Cursors.Hand && (bool)gridBudget2[rowIndex, "Analysis"] && !HasOpenedBreakdownForm)
			{
				HasOpenedBreakdownForm = true;
				ExecuteBreakdownForm();
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudgetChange.cs" + ex.Message);
			MessageBox.Show(this, "Err13:\n" + ex.Message, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void ExecuteBreakdownForm()
	{
		bool flag = false;
		string IPStr = CommonMethods.GetIPAddress();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("顯示單價分析的FORM--" + F_ProjectCode + "(" + IPStr + ")");
		Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
		PROJ.ps_projectCode = F_ProjectCode;
		PROJ.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		PROJ.ps_chgCount = F_chgCount;
		F_IsUseIR = PROJ.GetUseIRSet(F_ProjectCode) == "1";
		if (gridBudget2[gridBudget2.Row, "Analysis"] == null || !(bool)gridBudget2[gridBudget2.Row, "Analysis"] || gridBudget2[gridBudget2.Row, "PubCode"] == null)
		{
			return;
		}
		string AppLocation = AppDomain.CurrentDomain.BaseDirectory;
		string FileINI = AppLocation + "OptionSet.ini";
		string sAnaUseNewOpen = CommonMethods.IniReadValue(FileINI, "BreakDownData", "UseNewOpen");
		if (sAnaUseNewOpen.ToUpper() == "TRUE")
		{
			bool IsAlreadyExist = false;
			int FormIndex = -1;
			for (int i = 0; i < base.OwnedForms.Length; i++)
			{
				if (base.OwnedForms[i] is FormMrsBaseBreakdown)
				{
					IsAlreadyExist = true;
					FormIndex = i;
					break;
				}
			}
			if (!IsAlreadyExist)
			{
				FormMrsBaseBreakdown frmBD = new FormMrsBaseBreakdown();
				frmBD.PubCode = (int)gridBudget2[gridBudget2.Row, "PubCode"];
				frmBD.ProjectCode = F_ProjectCode;
				frmBD._ActionName = F_ActionName;
				frmBD._UserID = F_UserID;
				frmBD._IsUseIR = F_IsUseIR;
				frmBD._iCostDigital = F_MainCst;
				frmBD._IsSBID = false;
				frmBD._Issue = (int)gridBudget1[gridBudget1.Row, "chgCount"];
				F_SNo = (int)gridBudget2[gridBudget2.Row, "sNO"];
				frmBD.Owner = this;
				frmBD.Show();
			}
			else
			{
				(base.OwnedForms[FormIndex] as FormMrsBaseBreakdown).PubCode = (int)gridBudget2[gridBudget2.Row, "PubCode"];
				(base.OwnedForms[FormIndex] as FormMrsBaseBreakdown).ProjectCode = F_ProjectCode;
				(base.OwnedForms[FormIndex] as FormMrsBaseBreakdown)._ActionName = F_ActionName;
				(base.OwnedForms[FormIndex] as FormMrsBaseBreakdown)._UserID = F_UserID;
				(base.OwnedForms[FormIndex] as FormMrsBaseBreakdown)._IsUseIR = F_IsUseIR;
				(base.OwnedForms[FormIndex] as FormMrsBaseBreakdown)._iCostDigital = F_MainCst;
				(base.OwnedForms[FormIndex] as FormMrsBaseBreakdown)._IsSBID = false;
				(base.OwnedForms[FormIndex] as FormMrsBaseBreakdown)._Issue = (int)gridBudget1[gridBudget1.Row, "chgCount"];
				F_SNo = (int)gridBudget2[gridBudget2.Row, "sNO"];
				(base.OwnedForms[FormIndex] as FormMrsBaseBreakdown).Reload();
			}
		}
		else
		{
			FormMrsBaseBreakdown frmBD = new FormMrsBaseBreakdown();
			frmBD.PubCode = (int)gridBudget2[gridBudget2.Row, "PubCode"];
			frmBD.ProjectCode = F_ProjectCode;
			frmBD._ActionName = F_ActionName;
			frmBD._UserID = F_UserID;
			frmBD._IsUseIR = F_IsUseIR;
			frmBD._iCostDigital = F_MainCst;
			frmBD._IsSBID = false;
			frmBD._Issue = PubTools.Str2Int(gridBudget1[gridBudget1.Row, "chgCount"]);
			F_SNo = (int)gridBudget2[gridBudget2.Row, "sNO"];
			frmBD.Owner = this;
			frmBD.ShowDialog();
			int iPos = gridBudget2.Row;
			int iSno = (int)gridBudget2[gridBudget2.Row, "SNo"];
			if (F_IsNeedToReloadAllData)
			{
				Do_Change();
				F_IsNeedToReloadAllData = false;
			}
			else
			{
				ReLoad_OneRow(iSno, iPos);
			}
			HasOpenedBreakdownForm = false;
		}
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

	private void gridBudget2_Resize(object sender, EventArgs e)
	{
		FormBudgetChange_SizeChanged(sender, e);
	}
}
