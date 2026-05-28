using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.CTRClass;
using Archnowledge.Pcces.DomainModule.BusinessLogical;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.DomainModule.LogicalBase;
using Archnowledge.Pcces.DomainModule.Sub;
using Archnowledge.Pcces.PccesMain.About;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.PccesMain.Budget;
using Archnowledge.Pcces.PccesMain.Budget.Option;
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
using Infragistics.Win.UltraWinTabControl;
using Infragistics.Win.UltraWinToolbars;

namespace Archnowledge.Pcces.PccesMain.SplitContract;

public class FormSplitContract : Form
{
	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Top;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Bottom;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Left;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Right;

	private Panel LeftPanel;

	private OnlineList onlineList1;

	public FunctionButtons functionButtons1;

	private Panel panel1;

	private UltraTabControl Tab_Ctrl;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl Tab_A;

	private Panel pnl_spliter;

	private UltraButton Btn_Splt;

	private AxSSPanel ssp_Lower;

	private AxSSPanel ssp_Bottom;

	private AxSSPanel ssp_Upper;

	private AxSSPanel ssp_Top;

	private Panel panel2;

	private UltraLabel lblProjectData;

	private UltraLabel ultraLabel10;

	private UltraButton BtnSwitchProject;

	private UltraLabel ultraLabel1;

	private UltraStatusBar ultraStatusBar1;

	private GridBudget gridBudget1;

	private ImageList iglst_splt_Btn;

	private ImageList _imageList2;

	private ImageList imageList2;

	private Panel panel7;

	private UltraLabel lblTotal;

	private UltraLabel ultraLabel8;

	private AxSSPanel axSSPanel2;

	private OpenFileDialog openFileDialog1;

	private SaveFileDialog saveFileDialog1;

	private UltraToolbarsManager ultraToolbarsManager1;

	private IContainer components;

	private Archnowledge.Pcces.BUDClass.ItemA dbItemA;

	private bool IsAuto = false;

	private bool HasOpenedBreakdownForm = false;

	private bool F_IsNeedToReloadAllData = false;

	private bool F_IsUseIR = true;

	private bool Is_SBID = false;

	private int F_SNo = -1;

	private string F_CurrentDBName = "";

	private bool HasApproved = false;

	private string sIniFileName = CommonMethods.ExtractFilePath(Application.ExecutablePath) + "PccesMain.ini";

	private string sAssemType = "1";

	private string sIsSymbol = "N";

	private string sSymbol = "";

	private int[] L1 = new int[9];

	private bool F_HasRegistered;

	private ArrayList ArrDecimal = new ArrayList();

	private PccesFormAction F_ActionName = PccesFormAction.SplitContract;

	private string F_KeyWord = "";

	private string F_ProjectCode;

	private string F_ProjectNameC;

	private string F_SubProjetCode = "";

	private int F_MainQty = 0;

	private int F_MainCst = 0;

	private int F_MainAmt = 0;

	private int F_AnaQty = 0;

	private int F_AnaCst = 0;

	private int F_AnaAmt = 0;

	private double F_OldTotalAmount = 0.0;

	private string F_UserID;

	private string F_UserName = "";

	private string F_FunctionName = "SplitContract";

	private string F_ServerName = "localhost";

	private int GridCols = 15;

	private int GridCols2 = 15;

	private object[,] GridColsSquence;

	private object[,] GridColsSquence2;

	private DataTable DT1 = new DataTable();

	private DataTable DT2_1 = new DataTable();

	private DataTable DT2_2 = new DataTable();

	private Archnowledge.Pcces.DomainModule.LogicalBase.Project theProject = new SubProject();

	private Archnowledge.Pcces.DomainModule.LogicalBase.ItemA theItemA = new SubItemA();

	private ProjMrsA theProjMrsA = new SubProjMrsA();

	private FormSys_G_Info1 FM_INFO = null;

	private int ProgressValue = 0;

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

	public string ProjectCode
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

	public DataTable _DT1
	{
		get
		{
			return DT1;
		}
		set
		{
			DT1 = value;
		}
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.SplitContract.FormSplitContract));
		Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.OptionSet optionSet1 = new Infragistics.Win.UltraWinToolbars.OptionSet("Switch");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Tool1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuPrint_CNT");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuFile_Digital");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuEditBDGT");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuGetCost");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool5 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuApprove");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool6 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuReItem");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool7 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuReCal");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool1 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnuLevel");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool1 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_1", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool2 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_2", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool3 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_3", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool4 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_4", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool5 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_5", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool6 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_6", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool7 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_7", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool8 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_8", "Switch");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool2 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnu_lblFind");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool1 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool8 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_Go");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar2 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Tool2");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool9 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuNewIssue");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool10 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuEditIssue");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool11 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDeleteIssue");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool12 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuEditBDGT");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool13 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCntDetail");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool3 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnu_lblFind");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool2 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo2");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool14 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Go2");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar3 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Menu1");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool1 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuFile_CNT");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool2 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuEdit_CNT");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool3 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuView_CNT");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool4 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuTool_CNT");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool5 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuHelp");
		Infragistics.Win.Appearance appearance38 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance39 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool15 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelete");
		Infragistics.Win.Appearance appearance40 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool4 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnu_lblFind");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool3 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo1");
		Infragistics.Win.ValueList valueList1 = new Infragistics.Win.ValueList(0);
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool16 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_Go");
		Infragistics.Win.Appearance appearance41 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool6 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Popup1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool17 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuNewIssue");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool18 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuEditIssue");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool19 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDeleteIssue");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool20 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuEditBDGT");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool21 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuGetCost");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool22 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuApprove");
		Infragistics.Win.Appearance appearance42 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool23 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuReCal");
		Infragistics.Win.Appearance appearance43 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool24 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuReItem");
		Infragistics.Win.Appearance appearance44 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool25 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuInvoice");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool26 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCntDetail");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool27 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuNewIssue");
		Infragistics.Win.Appearance appearance45 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool28 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuEditIssue");
		Infragistics.Win.Appearance appearance46 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool29 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDeleteIssue");
		Infragistics.Win.Appearance appearance47 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool4 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo2");
		Infragistics.Win.ValueList valueList2 = new Infragistics.Win.ValueList(0);
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool30 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Go2");
		Infragistics.Win.Appearance appearance48 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool7 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuFile_CNT");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool31 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSwitchProj_CNT");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool32 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuImport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool33 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool34 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuFile_Digital");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool35 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuPrint_CNT");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool36 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuProjectDel");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool37 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuClose_CNT");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool8 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuEdit_CNT");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool38 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuEditBDGT");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool39 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuGetCost");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool40 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelItem_BDGT");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool41 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuApprove");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool42 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuBASIC_CNT");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool43 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuToolCancel");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool9 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuView_CNT");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool44 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuViewRes");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool10 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuTool_CNT");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool45 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuReItem");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool46 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuReCal");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool47 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_AdjustTot_CNT");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool48 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuItemSet_BDGT");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool49 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCalcu");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool50 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuTool_Option");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool51 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSwitchProj_CNT");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool52 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuPrint_CNT");
		Infragistics.Win.Appearance appearance49 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool53 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuClose_CNT");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool54 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_AdjustTot_CNT");
		Infragistics.Win.Appearance appearance50 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool55 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuBASIC_CNT");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool56 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelItem_BDGT");
		Infragistics.Win.Appearance appearance51 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool11 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("PopCNT");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool57 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuEditMain");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool58 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelItem_BDGT");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool59 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDetailEdit_SetShare");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool60 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCancelShare");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool61 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuItemSet_BDGT");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool62 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuToolCancel");
		Infragistics.Win.Appearance appearance52 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool12 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuHelp");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool63 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuAbout");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool64 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuUpdateList");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool65 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuAbout");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool66 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuUpdateList");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool67 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuEditMain");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool68 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDetailEdit_SetShare");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool69 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCancelShare");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool70 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCalcu");
		Infragistics.Win.Appearance appearance53 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool5 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnuLevel");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool71 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuImport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool72 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool73 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuViewRes");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool74 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuFile_Digital");
		Infragistics.Win.Appearance appearance54 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool75 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuProjectDel");
		Infragistics.Win.Appearance appearance55 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool9 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_1", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool10 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_2", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool11 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_3", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool12 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_4", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool13 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_5", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool14 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_6", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool15 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_7", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool16 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_8", "Switch");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool76 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuTool_Option");
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.Appearance appearance56 = new Infragistics.Win.Appearance();
		this.Tab_A = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.gridBudget1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
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
		this.ultraToolbarsManager1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
		this._FormSys_B_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.LeftPanel = new System.Windows.Forms.Panel();
		this.onlineList1 = new Archnowledge.Pcces.PccesMain.ArchControls.OnlineList();
		this.functionButtons1 = new Archnowledge.Pcces.PccesMain.ArchControls.FunctionButtons();
		this.panel1 = new System.Windows.Forms.Panel();
		this.Tab_Ctrl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.pnl_spliter = new System.Windows.Forms.Panel();
		this.Btn_Splt = new Infragistics.Win.Misc.UltraButton();
		this.ssp_Lower = new AxThreed.AxSSPanel();
		this.ssp_Bottom = new AxThreed.AxSSPanel();
		this.ssp_Upper = new AxThreed.AxSSPanel();
		this.ssp_Top = new AxThreed.AxSSPanel();
		this.iglst_splt_Btn = new System.Windows.Forms.ImageList(this.components);
		this._imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
		this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
		this.Tab_A.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridBudget1).BeginInit();
		this.panel7.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.axSSPanel2).BeginInit();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).BeginInit();
		this.LeftPanel.SuspendLayout();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).BeginInit();
		this.Tab_Ctrl.SuspendLayout();
		this.pnl_spliter.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ssp_Lower).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Bottom).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Upper).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Top).BeginInit();
		base.SuspendLayout();
		this.Tab_A.Controls.Add(this.gridBudget1);
		this.Tab_A.Controls.Add(this.panel7);
		this.Tab_A.Controls.Add(this.ultraStatusBar1);
		this.Tab_A.Controls.Add(this.panel2);
		this.Tab_A.Location = new System.Drawing.Point(0, 0);
		this.Tab_A.Name = "Tab_A";
		this.Tab_A.Size = new System.Drawing.Size(625, 474);
		this.gridBudget1._ExcelFileName = "";
		this.gridBudget1._ExcelSheeName = "";
		this.gridBudget1._IsOpenExcelAfterExport = false;
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
		this.gridBudget1.Size = new System.Drawing.Size(625, 390);
		this.gridBudget1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridBudget1.Styles"));
		this.gridBudget1.TabIndex = 7;
		this.gridBudget1.Tree.Column = 1;
		this.gridBudget1.Tree.LineColor = System.Drawing.Color.Gray;
		this.gridBudget1.Click += new System.EventHandler(gridBudget1_Click);
		this.gridBudget1.AfterSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(gridBudget1_AfterSelChange);
		this.gridBudget1.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(gridBudget1_AfterEdit);
		this.gridBudget1.Resize += new System.EventHandler(gridBudget1_Resize);
		this.gridBudget1.MouseMove += new System.Windows.Forms.MouseEventHandler(gridBudget1_MouseMove);
		this.panel7.Controls.Add(this.lblTotal);
		this.panel7.Controls.Add(this.ultraLabel8);
		this.panel7.Controls.Add(this.axSSPanel2);
		this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel7.Location = new System.Drawing.Point(0, 420);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(625, 28);
		this.panel7.TabIndex = 8;
		this.lblTotal.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance25.ForeColor = System.Drawing.Color.Blue;
		appearance25.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance25.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblTotal.Appearance = appearance25;
		this.lblTotal.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.lblTotal.Font = new System.Drawing.Font("Courier New", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblTotal.Location = new System.Drawing.Point(64, 5);
		this.lblTotal.Name = "lblTotal";
		this.lblTotal.Size = new System.Drawing.Size(512, 19);
		this.lblTotal.TabIndex = 14;
		appearance26.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		appearance26.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel8.Appearance = appearance26;
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
		appearance27.FontData.SizeInPoints = 11f;
		appearance27.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraStatusBar1.Appearance = appearance27;
		this.ultraStatusBar1.Location = new System.Drawing.Point(0, 448);
		this.ultraStatusBar1.Name = "ultraStatusBar1";
		appearance28.TextVAlign = Infragistics.Win.VAlign.Bottom;
		ultraStatusPanel1.Appearance = appearance28;
		ultraStatusPanel1.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel1.Key = "RowsCount";
		ultraStatusPanel1.Text = "資料筆數：";
		ultraStatusPanel1.Width = 200;
		ultraStatusPanel2.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel2.Key = "ProgressBar";
		ultraStatusPanel2.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		this.ultraStatusBar1.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[2] { ultraStatusPanel1, ultraStatusPanel2 });
		this.ultraStatusBar1.Size = new System.Drawing.Size(625, 26);
		this.ultraStatusBar1.TabIndex = 6;
		this.ultraStatusBar1.Text = "ultraStatusBar1";
		this.panel2.Controls.Add(this.lblProjectData);
		this.panel2.Controls.Add(this.ultraLabel10);
		this.panel2.Controls.Add(this.BtnSwitchProject);
		this.panel2.Controls.Add(this.ultraLabel1);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(625, 30);
		this.panel2.TabIndex = 1;
		this.lblProjectData.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance29.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance29.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblProjectData.Appearance = appearance29;
		this.lblProjectData.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.lblProjectData.Font = new System.Drawing.Font("細明體", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblProjectData.Location = new System.Drawing.Point(80, 5);
		this.lblProjectData.Name = "lblProjectData";
		this.lblProjectData.Size = new System.Drawing.Size(428, 20);
		this.lblProjectData.TabIndex = 15;
		appearance30.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel10.Appearance = appearance30;
		this.ultraLabel10.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.ultraLabel10.Font = new System.Drawing.Font("細明體", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel10.Location = new System.Drawing.Point(10, 7);
		this.ultraLabel10.Name = "ultraLabel10";
		this.ultraLabel10.Size = new System.Drawing.Size(74, 19);
		this.ultraLabel10.TabIndex = 14;
		this.ultraLabel10.Text = "目前專案：";
		this.BtnSwitchProject.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance31.BackColor = System.Drawing.Color.Silver;
		appearance31.BackColor2 = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance31.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		appearance31.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.BtnSwitchProject.Appearance = appearance31;
		this.BtnSwitchProject.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.BtnSwitchProject.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.BtnSwitchProject.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance32.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance32.BackColor2 = System.Drawing.Color.White;
		appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.BtnSwitchProject.HotTrackAppearance = appearance32;
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
		appearance33.FontData.Name = "Arial";
		appearance33.FontData.SizeInPoints = 9f;
		this.ultraToolbarsManager1.Appearance = appearance33;
		appearance34.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance34.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.DockAreaAppearance = appearance34;
		this.ultraToolbarsManager1.DockWithinContainer = this;
		this.ultraToolbarsManager1.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraToolbarsManager1.LockToolbars = true;
		appearance35.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance35.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance35.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.MenuSettings.HotTrackAppearance = appearance35;
		appearance36.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance36.BackColor2 = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraToolbarsManager1.MenuSettings.IconAreaAppearance = appearance36;
		appearance37.BackColor = System.Drawing.Color.White;
		appearance37.BackColor2 = System.Drawing.Color.White;
		this.ultraToolbarsManager1.MenuSettings.ToolAppearance = appearance37;
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
		buttonTool1.InstanceProps.IsFirstInGroup = true;
		buttonTool3.InstanceProps.IsFirstInGroup = true;
		buttonTool5.InstanceProps.IsFirstInGroup = true;
		buttonTool6.InstanceProps.IsFirstInGroup = true;
		buttonTool7.InstanceProps.IsFirstInGroup = true;
		labelTool1.InstanceProps.IsFirstInGroup = true;
		stateButtonTool1.Checked = true;
		labelTool2.InstanceProps.IsFirstInGroup = true;
		ultraToolbar1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[19]
		{
			buttonTool1, buttonTool2, buttonTool3, buttonTool4, buttonTool5, buttonTool6, buttonTool7, labelTool1, stateButtonTool1, stateButtonTool2,
			stateButtonTool3, stateButtonTool4, stateButtonTool5, stateButtonTool6, stateButtonTool7, stateButtonTool8, labelTool2, comboBoxTool1, buttonTool8
		});
		ultraToolbar2.DockedColumn = 0;
		ultraToolbar2.DockedRow = 2;
		ultraToolbar2.Settings.AllowCustomize = Infragistics.Win.DefaultableBoolean.True;
		ultraToolbar2.Text = "Tool2";
		buttonTool12.InstanceProps.IsFirstInGroup = true;
		labelTool3.InstanceProps.IsFirstInGroup = true;
		ultraToolbar2.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[8] { buttonTool9, buttonTool10, buttonTool11, buttonTool12, buttonTool13, labelTool3, comboBoxTool2, buttonTool14 });
		ultraToolbar3.DockedColumn = 0;
		ultraToolbar3.DockedRow = 0;
		ultraToolbar3.IsMainMenuBar = true;
		ultraToolbar3.Text = "Menu1";
		ultraToolbar3.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[5] { popupMenuTool1, popupMenuTool2, popupMenuTool3, popupMenuTool4, popupMenuTool5 });
		this.ultraToolbarsManager1.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[3] { ultraToolbar1, ultraToolbar2, ultraToolbar3 });
		appearance38.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance38.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.ToolbarSettings.Appearance = appearance38;
		appearance39.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance39.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance39.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.ToolbarSettings.HotTrackAppearance = appearance39;
		appearance40.Image = resources.GetObject("appearance9.Image");
		buttonTool15.SharedProps.AppearancesSmall.Appearance = appearance40;
		buttonTool15.SharedProps.Caption = "刪除";
		buttonTool15.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		buttonTool15.SharedProps.Shortcut = System.Windows.Forms.Shortcut.Del;
		labelTool4.SharedProps.Caption = "尋找:";
		labelTool4.SharedProps.Category = "合約";
		labelTool4.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		comboBoxTool3.DropDownStyle = Infragistics.Win.DropDownStyle.DropDown;
		comboBoxTool3.SharedProps.Caption = "輸入關鍵字";
		comboBoxTool3.SharedProps.Category = "合約";
		comboBoxTool3.SharedProps.Width = 200;
		comboBoxTool3.ValueList = valueList1;
		appearance41.Image = resources.GetObject("appearance10.Image");
		buttonTool16.SharedProps.AppearancesSmall.Appearance = appearance41;
		buttonTool16.SharedProps.Caption = "Go";
		buttonTool16.SharedProps.Category = "合約";
		popupMenuTool6.SharedProps.Caption = "右鍵功能表";
		buttonTool19.InstanceProps.IsFirstInGroup = true;
		popupMenuTool6.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[3] { buttonTool17, buttonTool18, buttonTool19 });
		buttonTool20.SharedProps.Caption = "從預算挑選…";
		buttonTool20.SharedProps.Category = "合約";
		buttonTool20.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool21.SharedProps.Caption = "取回預算單價";
		buttonTool21.SharedProps.Category = "合約";
		buttonTool21.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		appearance42.Image = resources.GetObject("appearance11.Image");
		buttonTool22.SharedProps.AppearancesSmall.Appearance = appearance42;
		buttonTool22.SharedProps.Caption = "核定契約書";
		buttonTool22.SharedProps.Category = "合約";
		buttonTool22.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		appearance43.Image = resources.GetObject("appearance12.Image");
		buttonTool23.SharedProps.AppearancesSmall.Appearance = appearance43;
		buttonTool23.SharedProps.Caption = "重新總計";
		buttonTool23.SharedProps.Category = "合約";
		buttonTool23.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		appearance44.Image = resources.GetObject("appearance13.Image");
		buttonTool24.SharedProps.AppearancesSmall.Appearance = appearance44;
		buttonTool24.SharedProps.Caption = "項次重整";
		buttonTool24.SharedProps.Category = "合約";
		buttonTool24.SharedProps.CustomizerCaption = "項次重整...";
		buttonTool24.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		buttonTool25.SharedProps.Caption = "檢視估驗計價";
		buttonTool25.SharedProps.Category = "合約";
		buttonTool25.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool26.SharedProps.Caption = "檢視契約明細";
		buttonTool26.SharedProps.Category = "合約";
		buttonTool26.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		appearance45.Image = resources.GetObject("appearance14.Image");
		buttonTool27.SharedProps.AppearancesSmall.Appearance = appearance45;
		buttonTool27.SharedProps.Caption = "新增期別";
		buttonTool27.SharedProps.Category = "計價";
		buttonTool27.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		appearance46.Image = resources.GetObject("appearance15.Image");
		buttonTool28.SharedProps.AppearancesSmall.Appearance = appearance46;
		buttonTool28.SharedProps.Caption = "編輯期別";
		buttonTool28.SharedProps.Category = "計價";
		buttonTool28.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		appearance47.Image = resources.GetObject("appearance16.Image");
		buttonTool29.SharedProps.AppearancesSmall.Appearance = appearance47;
		buttonTool29.SharedProps.Caption = "刪除期別";
		buttonTool29.SharedProps.Category = "計價";
		buttonTool29.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		comboBoxTool4.SharedProps.Caption = "第2頁的尋找";
		comboBoxTool4.SharedProps.Category = "計價";
		comboBoxTool4.SharedProps.Width = 200;
		comboBoxTool4.ValueList = valueList2;
		appearance48.Image = resources.GetObject("appearance17.Image");
		buttonTool30.SharedProps.AppearancesSmall.Appearance = appearance48;
		buttonTool30.SharedProps.Caption = "執行尋找";
		buttonTool30.SharedProps.Category = "計價";
		popupMenuTool7.SharedProps.Caption = "檔案(&F)";
		popupMenuTool7.SharedProps.Category = "合約";
		buttonTool32.InstanceProps.IsFirstInGroup = true;
		buttonTool34.InstanceProps.IsFirstInGroup = true;
		buttonTool36.InstanceProps.IsFirstInGroup = true;
		popupMenuTool7.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[7] { buttonTool31, buttonTool32, buttonTool33, buttonTool34, buttonTool35, buttonTool36, buttonTool37 });
		popupMenuTool8.SharedProps.Caption = "編輯(&E)";
		popupMenuTool8.SharedProps.Category = "合約";
		buttonTool40.InstanceProps.IsFirstInGroup = true;
		buttonTool41.InstanceProps.IsFirstInGroup = true;
		buttonTool42.InstanceProps.IsFirstInGroup = true;
		popupMenuTool8.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[6] { buttonTool38, buttonTool39, buttonTool40, buttonTool41, buttonTool42, buttonTool43 });
		popupMenuTool9.SharedProps.Caption = "檢視(&V)";
		popupMenuTool9.SharedProps.Category = "合約";
		popupMenuTool9.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[1] { buttonTool44 });
		popupMenuTool10.SharedProps.Caption = "工具(&T)";
		popupMenuTool10.SharedProps.Category = "合約";
		buttonTool47.InstanceProps.IsFirstInGroup = true;
		buttonTool48.InstanceProps.IsFirstInGroup = true;
		buttonTool49.InstanceProps.IsFirstInGroup = true;
		buttonTool50.InstanceProps.IsFirstInGroup = true;
		popupMenuTool10.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[6] { buttonTool45, buttonTool46, buttonTool47, buttonTool48, buttonTool49, buttonTool50 });
		buttonTool51.SharedProps.Caption = "切換專案...";
		buttonTool51.SharedProps.Category = "合約";
		appearance49.Image = resources.GetObject("appearance18.Image");
		buttonTool52.SharedProps.AppearancesSmall.Appearance = appearance49;
		buttonTool52.SharedProps.Caption = "列印報表...";
		buttonTool52.SharedProps.Category = "合約";
		buttonTool52.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageOnlyOnToolbars;
		buttonTool53.SharedProps.Caption = "結束契約書編製...";
		buttonTool53.SharedProps.Category = "合約";
		appearance50.Image = resources.GetObject("appearance19.Image");
		buttonTool54.SharedProps.AppearancesSmall.Appearance = appearance50;
		buttonTool54.SharedProps.Caption = "總價調整...";
		buttonTool54.SharedProps.Category = "合約";
		buttonTool54.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		buttonTool55.SharedProps.Caption = "編輯契約基本資料...";
		buttonTool55.SharedProps.Category = "合約";
		appearance51.Image = resources.GetObject("appearance20.Image");
		buttonTool56.SharedProps.AppearancesSmall.Appearance = appearance51;
		buttonTool56.SharedProps.Caption = "刪除項目";
		buttonTool56.SharedProps.Category = "合約";
		buttonTool56.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		buttonTool56.SharedProps.Shortcut = System.Windows.Forms.Shortcut.CtrlDel;
		popupMenuTool11.SharedProps.Caption = "契約右選單";
		popupMenuTool11.SharedProps.Category = "合約";
		buttonTool59.InstanceProps.IsFirstInGroup = true;
		popupMenuTool11.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[4] { buttonTool57, buttonTool58, buttonTool59, buttonTool60 });
		buttonTool61.SharedProps.Caption = "項次編號設定...";
		buttonTool61.SharedProps.Category = "合約";
		appearance52.Image = resources.GetObject("appearance21.Image");
		buttonTool62.SharedProps.AppearancesSmall.Appearance = appearance52;
		buttonTool62.SharedProps.Caption = "取消契約書核定";
		buttonTool62.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		popupMenuTool12.SharedProps.Caption = "說明(&H)";
		buttonTool64.InstanceProps.IsFirstInGroup = true;
		popupMenuTool12.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[2] { buttonTool63, buttonTool64 });
		buttonTool65.SharedProps.Caption = "關於PCCES...";
		buttonTool66.SharedProps.Caption = "最新消息...";
		buttonTool67.SharedProps.Caption = "編輯主項大類..";
		buttonTool68.SharedProps.Caption = "設為攤提項目";
		buttonTool69.SharedProps.Caption = "取消攤提";
		appearance53.Image = resources.GetObject("appearance22.Image");
		buttonTool70.SharedProps.AppearancesSmall.Appearance = appearance53;
		buttonTool70.SharedProps.Caption = "計算機";
		labelTool5.SharedProps.Caption = "階層:";
		labelTool5.SharedProps.Category = "階層";
		labelTool5.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool71.SharedProps.Caption = "契約編制CNT匯入...";
		buttonTool71.SharedProps.Category = "合約";
		buttonTool72.SharedProps.Caption = "契約編制CNT匯出...";
		buttonTool72.SharedProps.Category = "合約";
		buttonTool73.SharedProps.Caption = "專案工項維護";
		buttonTool73.SharedProps.Category = "合約";
		buttonTool73.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		appearance54.Image = resources.GetObject("appearance23.Image");
		buttonTool74.SharedProps.AppearancesSmall.Appearance = appearance54;
		buttonTool74.SharedProps.Caption = "製作電子檔...";
		buttonTool74.SharedProps.Category = "合約";
		buttonTool74.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageOnlyOnToolbars;
		appearance55.Image = resources.GetObject("appearance24.Image");
		buttonTool75.SharedProps.AppearancesSmall.Appearance = appearance55;
		buttonTool75.SharedProps.Caption = "刪除專案";
		buttonTool75.SharedProps.Category = "合約";
		buttonTool75.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool9.Checked = true;
		stateButtonTool9.OptionSetKey = "Switch";
		stateButtonTool9.SharedProps.Caption = "1";
		stateButtonTool9.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool10.OptionSetKey = "Switch";
		stateButtonTool10.SharedProps.Caption = "2";
		stateButtonTool10.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool11.OptionSetKey = "Switch";
		stateButtonTool11.SharedProps.Caption = "3";
		stateButtonTool11.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool12.OptionSetKey = "Switch";
		stateButtonTool12.SharedProps.Caption = "4";
		stateButtonTool12.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool13.OptionSetKey = "Switch";
		stateButtonTool13.SharedProps.Caption = "5";
		stateButtonTool13.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool14.OptionSetKey = "Switch";
		stateButtonTool14.SharedProps.Caption = "6";
		stateButtonTool14.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool15.OptionSetKey = "Switch";
		stateButtonTool15.SharedProps.Caption = "7";
		stateButtonTool15.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool16.OptionSetKey = "Switch";
		stateButtonTool16.SharedProps.Caption = "8";
		stateButtonTool16.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool76.SharedProps.Caption = "選項...";
		buttonTool76.SharedProps.Category = "合約";
		this.ultraToolbarsManager1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[52]
		{
			buttonTool15, labelTool4, comboBoxTool3, buttonTool16, popupMenuTool6, buttonTool20, buttonTool21, buttonTool22, buttonTool23, buttonTool24,
			buttonTool25, buttonTool26, buttonTool27, buttonTool28, buttonTool29, comboBoxTool4, buttonTool30, popupMenuTool7, popupMenuTool8, popupMenuTool9,
			popupMenuTool10, buttonTool51, buttonTool52, buttonTool53, buttonTool54, buttonTool55, buttonTool56, popupMenuTool11, buttonTool61, buttonTool62,
			popupMenuTool12, buttonTool65, buttonTool66, buttonTool67, buttonTool68, buttonTool69, buttonTool70, labelTool5, buttonTool71, buttonTool72,
			buttonTool73, buttonTool74, buttonTool75, stateButtonTool9, stateButtonTool10, stateButtonTool11, stateButtonTool12, stateButtonTool13, stateButtonTool14, stateButtonTool15,
			stateButtonTool16, buttonTool76
		});
		this.ultraToolbarsManager1.ToolKeyPress += new Infragistics.Win.UltraWinToolbars.ToolKeyPressEventHandler(ultraToolbarsManager1_ToolKeyPress);
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
		this._FormSys_B_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(792, 79);
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
		this._FormSys_B_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 79);
		this._FormSys_B_Toolbars_Dock_Area_Left.Name = "_FormSys_B_Toolbars_Dock_Area_Left";
		this._FormSys_B_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 474);
		this._FormSys_B_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
		this._FormSys_B_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(792, 79);
		this._FormSys_B_Toolbars_Dock_Area_Right.Name = "_FormSys_B_Toolbars_Dock_Area_Right";
		this._FormSys_B_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 474);
		this._FormSys_B_Toolbars_Dock_Area_Right.ToolbarsManager = this.ultraToolbarsManager1;
		this.LeftPanel.Controls.Add(this.onlineList1);
		this.LeftPanel.Controls.Add(this.functionButtons1);
		this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
		this.LeftPanel.Location = new System.Drawing.Point(0, 79);
		this.LeftPanel.Name = "LeftPanel";
		this.LeftPanel.Size = new System.Drawing.Size(160, 474);
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
		this.functionButtons1.Size = new System.Drawing.Size(160, 474);
		this.functionButtons1.TabIndex = 3;
		this.panel1.Controls.Add(this.Tab_Ctrl);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.panel1.Location = new System.Drawing.Point(167, 79);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(625, 474);
		this.panel1.TabIndex = 9;
		this.Tab_Ctrl.Controls.Add(this.ultraTabSharedControlsPage1);
		this.Tab_Ctrl.Controls.Add(this.Tab_A);
		this.Tab_Ctrl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Tab_Ctrl.Location = new System.Drawing.Point(0, 0);
		this.Tab_Ctrl.Name = "Tab_Ctrl";
		this.Tab_Ctrl.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.Tab_Ctrl.Size = new System.Drawing.Size(625, 474);
		this.Tab_Ctrl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
		this.Tab_Ctrl.TabIndex = 0;
		ultraTab1.TabPage = this.Tab_A;
		ultraTab1.Text = "tab1";
		this.Tab_Ctrl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[1] { ultraTab1 });
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(625, 474);
		this.pnl_spliter.BackColor = System.Drawing.Color.LightGray;
		this.pnl_spliter.Controls.Add(this.Btn_Splt);
		this.pnl_spliter.Controls.Add(this.ssp_Lower);
		this.pnl_spliter.Controls.Add(this.ssp_Bottom);
		this.pnl_spliter.Controls.Add(this.ssp_Upper);
		this.pnl_spliter.Controls.Add(this.ssp_Top);
		this.pnl_spliter.Dock = System.Windows.Forms.DockStyle.Left;
		this.pnl_spliter.Location = new System.Drawing.Point(160, 79);
		this.pnl_spliter.Name = "pnl_spliter";
		this.pnl_spliter.Size = new System.Drawing.Size(7, 474);
		this.pnl_spliter.TabIndex = 10;
		appearance56.BorderColor = System.Drawing.Color.Transparent;
		appearance56.BorderColor3DBase = System.Drawing.Color.Transparent;
		appearance56.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance3.ImageBackground");
		this.Btn_Splt.Appearance = appearance56;
		this.Btn_Splt.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Borderless;
		this.Btn_Splt.Cursor = System.Windows.Forms.Cursors.Hand;
		this.Btn_Splt.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Btn_Splt.ImageSize = new System.Drawing.Size(7, 57);
		this.Btn_Splt.Location = new System.Drawing.Point(0, 220);
		this.Btn_Splt.Name = "Btn_Splt";
		this.Btn_Splt.ShapeImage = (System.Drawing.Image)resources.GetObject("Btn_Splt.ShapeImage");
		this.Btn_Splt.ShowFocusRect = false;
		this.Btn_Splt.ShowOutline = false;
		this.Btn_Splt.Size = new System.Drawing.Size(7, 43);
		this.Btn_Splt.TabIndex = 7;
		this.Btn_Splt.Click += new System.EventHandler(Btn_Splt_Click);
		this.ssp_Lower.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.ssp_Lower.Location = new System.Drawing.Point(0, 263);
		this.ssp_Lower.Name = "ssp_Lower";
		this.ssp_Lower.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Lower.OcxState");
		this.ssp_Lower.Size = new System.Drawing.Size(7, 208);
		this.ssp_Lower.TabIndex = 6;
		this.ssp_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.ssp_Bottom.Location = new System.Drawing.Point(0, 471);
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
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.White;
		this.imageList2.Images.SetKeyName(0, "");
		this.imageList2.Images.SetKeyName(1, "");
		this.imageList2.Images.SetKeyName(2, "");
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		base.ClientSize = new System.Drawing.Size(792, 553);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.pnl_spliter);
		base.Controls.Add(this.LeftPanel);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Right);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Left);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Top);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Bottom);
		base.KeyPreview = true;
		base.Name = "FormSplitContract";
		this.Text = "契約編輯";
		base.Load += new System.EventHandler(FormSplitContract_Load);
		base.Resize += new System.EventHandler(FormSplitContract_Resize);
		this.Tab_A.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridBudget1).EndInit();
		this.panel7.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.axSSPanel2).EndInit();
		this.panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).EndInit();
		this.LeftPanel.ResumeLayout(false);
		this.LeftPanel.PerformLayout();
		this.panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).EndInit();
		this.Tab_Ctrl.ResumeLayout(false);
		this.pnl_spliter.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ssp_Lower).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Bottom).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Upper).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Top).EndInit();
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

	private void ProgressEventHandler(string Message)
	{
		if (FM_INFO != null)
		{
			FM_INFO.SetValue(Message, ProgressValue++);
		}
	}

	private void ProgressEventHandlerInitMaxProgressValue(int MaxProgress)
	{
		if (FM_INFO != null)
		{
			ProgressValue = 0;
			FM_INFO._MaxValue = MaxProgress;
		}
	}

	public FormSplitContract()
	{
		InitializeComponent();
		CellStyle csCb = gridBudget1.Styles.Add("ComboList");
		csCb.DataType = typeof(string);
		csCb.ComboList = "警告但可存檔|警告且不可存檔|略過";
		csCb.ForeColor = Color.Navy;
		csCb.TextAlign = TextAlignEnum.LeftCenter;
		csCb.Font = new Font(Font, FontStyle.Bold);
		HideCols(IsHide: true);
		functionButtons1.ButtonOwner = LeftPanelStatus.Budget;
		CellStyle cs = gridBudget1.Styles.Add("img");
		cs.DataType = typeof(Image);
		CellStyle cs11 = gridBudget1.Styles.Add("EditMode");
		cs11.DataType = typeof(Image);
		cs11.ImageAlign = ImageAlignEnum.RightCenter;
		GridCols = gridBudget1.Cols.Count;
		GridColsSquence = new object[GridCols, 8];
		GridColsSquence2 = new object[GridCols2, 8];
	}

	private void HideCols(bool IsHide)
	{
		if (IsHide)
		{
			gridBudget1.Cols["LevelNo"].Visible = false;
			gridBudget1.Cols["Kind"].Visible = false;
			gridBudget1.Cols["Analysis"].Visible = false;
			gridBudget1.Cols["SNo"].Visible = false;
			gridBudget1.Cols["Formula"].Visible = false;
			gridBudget1.Cols["PubCode"].Visible = false;
			gridBudget1.Cols["IsShared"].Visible = false;
		}
	}

	private void SettingDecimal()
	{
		string IPStr = CommonMethods.GetIPAddress();
		DataTable DTDecimal = new DataTable();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("設定小數位數取位原則" + F_ProjectCode + "(" + IPStr + ")");
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
		ArrDecimal.Clear();
		ArrDecimal.Add(F_MainQty);
		ArrDecimal.Add(F_MainCst);
		ArrDecimal.Add(F_MainAmt);
		ArrDecimal.Add(F_AnaQty);
		ArrDecimal.Add(F_AnaCst);
		ArrDecimal.Add(F_AnaAmt);
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
			if (gridBudget1.Cols[i].Name == "Qty")
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
			if (gridBudget1.Cols[i].Name == "Amount")
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

	private void FormSplitContract_Resize(object sender, EventArgs e)
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

	private void DoMenuAction(string MenuID)
	{
		switch (MenuID)
		{
		case "mnuDelete":
			break;
		case "mnu_lblFind":
			break;
		case "mnu_Cbo1":
			break;
		case "mnu_Go":
			Do_ToolBarFind1();
			break;
		case "Popup1":
			break;
		case "mnuEditBDGT":
			if (!DBClass.ChkAuthority(F_UserID, "F00900020001"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00900020001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Execute_EditBDGT();
			}
			break;
		case "mnuGetCost":
			if (!DBClass.ChkAuthority(F_UserID, "F00900020002"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00900020002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Do_GetBackCost();
			}
			break;
		case "mnuApprove":
			if (!DBClass.ChkAuthority(F_UserID, "F00900020004"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00900020004") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Do_Approve();
			}
			break;
		case "mnuReCal":
			if (!DBClass.ChkAuthority(F_UserID, "F00900030002"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00900030002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Do_ReCal_All();
			}
			break;
		case "mnuReItem":
			if (!DBClass.ChkAuthority(F_UserID, "F00900030001"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00900030001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Do_ItemReArrange();
			}
			break;
		case "mnuCntDetail":
			Do_CntDetail();
			break;
		case "mnuClose_CNT":
			if (!DBClass.ChkAuthority(F_UserID, "F00900010003"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00900010003") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				CloseThisForm();
			}
			break;
		case "mnuBASIC_CNT":
			if (!DBClass.ChkAuthority(F_UserID, "F00900020005"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00900020005") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Execute_BASIC_CNT();
			}
			break;
		case "mnuSwitchProj_CNT":
			BtnSwitchProject_Click(this, EventArgs.Empty);
			break;
		case "mnu_AdjustTot_CNT":
			if (!DBClass.ChkAuthority(F_UserID, "F00900030003"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00900030003") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Execute_Adjust();
			}
			break;
		case "mnuDelItem_BDGT":
			if (!DBClass.ChkAuthority(F_UserID, "F00900020003"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00900020003") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Do_DelItem_BDGT();
			}
			break;
		case "mnuItemSet_BDGT":
			if (!DBClass.ChkAuthority(F_UserID, "F00900030004"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00900030004") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Execute_ItemNoSetting();
			}
			break;
		case "mnuPrint_CNT":
			if (!DBClass.ChkAuthority(F_UserID, "F00900010002"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00900010002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Execute_Print();
			}
			break;
		case "mnuToolCancel":
			if (!DBClass.ChkAuthority(F_UserID, "F00900030005"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00900030005") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Do_CancelApprove();
			}
			break;
		case "mnuAbout":
			if (!DBClass.ChkAuthority(F_UserID, "F00900040001"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00900040001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
		case "mnuEditMain":
			EditItemsByKind();
			break;
		case "mnuDetailEdit_SetShare":
			SetAsSharedItem();
			break;
		case "mnuCancelShare":
			Do_CancelShare();
			break;
		case "mnuCalcu":
			Execute_Calculator();
			break;
		case "mnuExport":
			Execute_Export();
			break;
		case "mnuImport":
			Do_Import();
			break;
		case "mnuViewRes":
			ExecuteResForm();
			break;
		case "mnuFile_Digital":
			Do_FileDigital("");
			break;
		case "mnuProjectDel":
			ExecuteProjectDel();
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
		case "mnuTool_Option":
			Execute_OptionMain();
			break;
		}
	}

	private void ultraToolbarsManager1_ToolClick(object sender, ToolClickEventArgs e)
	{
		DoMenuAction(e.Tool.Key);
	}

	private void Execute_Calculator()
	{
		ShellExecute SHExe = new ShellExecute();
		SHExe.OwnerHandle = base.Handle;
		SHExe.Path = "Calc.exe";
		SHExe.Execute();
		SHExe = null;
	}

	private void Execute_Export()
	{
		string sFilter = "CNT files (*.cnt)|*.cnt";
		string sName = "";
		saveFileDialog1.Filter = sFilter;
		saveFileDialog1.RestoreDirectory = true;
		if (saveFileDialog1.ShowDialog() == DialogResult.OK)
		{
			sName = saveFileDialog1.FileName;
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
	}

	private void Do_Import()
	{
		string sName = "";
		string F_NewProjectCode = "";
		openFileDialog1.RestoreDirectory = true;
		openFileDialog1.Filter = "電子標單契約編制檔 cnt 格式(*.cnt)|*.cnt";
		if (openFileDialog1.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		sName = openFileDialog1.FileName;
		FormSys_G_Info1 FM_INFO = new FormSys_G_Info1();
		FM_INFO._InfoString = "【契約編制】載入中，請稍候! ";
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
		string XML_MODE = "SUB";
		if (sFileName.Length >= 4)
		{
			string Str1 = CommonMethods.ExtractFileNoExtName(sFileName);
			sKey = ((Str1.Length < 4) ? Str1 : Str1.Substring(Str1.Length - 4));
		}
		DataSet DS1 = CommonMethods.ImportAccess(sFileName);
		if (DS1.Tables["Project"].Columns.IndexOf("CloseBidDate") < 0)
		{
			DS1.Tables["Project"].Columns.Add("CloseBidDate", Type.GetType("System.DateTime"));
			if (DS1.Tables["Project"].Rows.Count > 0)
			{
				DS1.Tables["Project"].Rows[0]["CloseBidDate"] = Convert.ToDateTime("1800/1/1");
			}
		}
		if (DS1.Tables["Project"].Columns.IndexOf("CheckOut") < 0)
		{
			DS1.Tables["Project"].Columns.Add("CheckOut", Type.GetType("System.String"));
			if (DS1.Tables["Project"].Rows.Count > 0)
			{
				DS1.Tables["Project"].Rows[0]["CheckOut"] = "N";
			}
		}
		if (DS1.Tables["Project"].Rows.Count > 0 && DS1.Tables["Project"].Rows[0]["CheckOut"].ToString().ToUpper() == "CKOUT")
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
		PROJ.ps_srckind = "SUB";
		ssKey2 = "SUB";
		Application.DoEvents();
		F_NewProjectCode = DS1.Tables["Project"].Rows[0]["projectcode"].ToString().Trim();
		string sProjectName = DS1.Tables["Project"].Rows[0]["projectNameC"].ToString().Trim();
		string sRet = PROJ.InputACCESS(DS1, XML_MODE);
		Application.DoEvents();
		if (sRet == "F")
		{
			FM_INFO.Close();
			FM_INFO.Dispose();
			if (MessageBox.Show(this, "有相同專案存在，是否刪除?", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				Application.DoEvents();
				FormSys_G_Info1 FM_INFO2 = new FormSys_G_Info1();
				FM_INFO2._InfoString = "【契約編制】載入中，請稍候! ";
				FM_INFO2.Show();
				Application.DoEvents();
				string ls_projectcode = DS1.Tables["Project"].Rows[0]["projectcode"].ToString();
				PROJ.DeleProjSub(ls_projectcode, flag: true);
				sRet = PROJ.InputACCESS(DS1, XML_MODE);
				LoadContract();
				BintToGrid1();
				((ButtonTool)ultraToolbarsManager1.Tools["mnuPrint_CNT"]).SharedProps.Enabled = true;
				((ButtonTool)ultraToolbarsManager1.Tools["mnuFile_Digital"]).SharedProps.Enabled = true;
				MessageBox.Show(this, " 轉入成功!", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				FM_INFO2.Close();
				FM_INFO2.Dispose();
			}
			Cursor = Cursors.Default;
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
			F_ProjectCode = F_NewProjectCode;
			F_ProjectNameC = sProjectName;
			LoadContract();
			BintToGrid1();
			((ButtonTool)ultraToolbarsManager1.Tools["mnuPrint_CNT"]).SharedProps.Enabled = true;
			((ButtonTool)ultraToolbarsManager1.Tools["mnuFile_Digital"]).SharedProps.Enabled = true;
			Cursor = Cursors.Default;
			MessageBox.Show(this, " 轉入成功!", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			try
			{
				DBClass DBCLS = new DBClass();
				DBCLS._FS_UserID = F_UserID;
				DBCLS.ExecuteCommand("Insert Into ProjAuthority(ProjectCode, UserID) values('" + F_NewProjectCode + "', '" + F_UserID + "')");
				return;
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "Project.formNewProjectWizard.cs" + ex.Message);
				return;
			}
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
		try
		{
			DBClass DBCLS = new DBClass();
			DBCLS._FS_UserID = F_UserID;
			DBCLS.ExecuteCommand("Insert Into ProjAuthority(ProjectCode, UserID) values('" + F_NewProjectCode + "', '" + F_UserID + "')");
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Project.formNewProjectWizard.cs" + ex.Message);
		}
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
		string sDeptName = "";
		if (DT2.Rows.Count > 0)
		{
			sDeptName = MAIN_UCOM.Get_Main_Name(DT2.Rows[0]["mainCName"].ToString().Trim());
		}
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

	private void Execute_OptionMain()
	{
		FormBDGT_OptionMain FM_OP = new FormBDGT_OptionMain();
		FM_OP._UserID = F_UserID;
		FM_OP._ProjectCode = F_ProjectCode;
		FM_OP._ActionName = "SUB";
		FM_OP.Owner = this;
		FM_OP.ShowDialog();
		FM_OP.Close();
		FM_OP.Dispose();
		FM_OP = null;
	}

	private void ExecuteResForm()
	{
		FormBudgetRes FM_BDGT_RES = new FormBudgetRes();
		FM_BDGT_RES._UserID = F_UserID;
		FM_BDGT_RES._ActionName = F_ActionName;
		FM_BDGT_RES._ProjectCode = F_ProjectCode;
		FM_BDGT_RES._IsSBID = Is_SBID;
		FM_BDGT_RES._CurrentDBName = F_CurrentDBName;
		FM_BDGT_RES._HasApproved = HasApproved;
		if (gridBudget1.Row > 0)
		{
			FM_BDGT_RES._calledPccesCode = ((gridBudget1[gridBudget1.Row, "PccesCode"] == null) ? string.Empty : gridBudget1[gridBudget1.Row, "PccesCode"].ToString());
		}
		FM_BDGT_RES.Owner = this;
		FM_BDGT_RES.ShowDialog();
		FM_BDGT_RES.Close();
		FM_BDGT_RES.Dispose();
		FM_BDGT_RES = null;
	}

	private void ExecuteProjectDel()
	{
		if (MessageBox.Show(this, "確定是否刪除?", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			ArrayList aArr = new ArrayList();
			aArr.Add(F_UserID);
			aArr.Add("WinFORM 基本工料");
			Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
			PROJ.ps_srckind = "SUB";
			PROJ.DeleProjSub(F_ProjectCode, flag: true);
			LoadContract();
			BintToGrid1();
			MessageBox.Show(this, " 刪除完畢!", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
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
		projcom.ps_srckind = "SUB";
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
		MessageBox.Show(this, "匯出完成!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	private void Do_CancelShare()
	{
		string IPStr = CommonMethods.GetIPAddress();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("取消攤提" + F_ProjectCode + "(" + IPStr + ")");
		if (dbItemA != null)
		{
			dbItemA = null;
		}
		if (dbItemA == null)
		{
			dbItemA = new Archnowledge.Pcces.BUDClass.ItemA(aArr);
		}
		dbItemA.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		dbItemA.ps_projectCode = F_ProjectCode;
		dbItemA.ps_sNo = gridBudget1[gridBudget1.Row, "SNo"].ToString();
		dbItemA.ps_share = "";
		dbItemA.UpdItem();
		LoadContract();
		BintToGrid1();
	}

	private void SetAsSharedItem()
	{
		string IPStr = CommonMethods.GetIPAddress();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("設為攤提項--" + F_ProjectCode + "(" + IPStr + ")");
		if (dbItemA != null)
		{
			dbItemA = null;
		}
		if (dbItemA == null)
		{
			dbItemA = new Archnowledge.Pcces.BUDClass.ItemA(aArr);
		}
		dbItemA.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		dbItemA.ps_projectCode = F_ProjectCode;
		for (int i = 1; i < gridBudget1.Rows.Count; i++)
		{
			if (gridBudget1[i, "SNo"] != null)
			{
				dbItemA.ps_sNo = gridBudget1[i, "SNo"].ToString();
				if (i == gridBudget1.Row)
				{
					dbItemA.ps_share = "1";
				}
				else
				{
					dbItemA.ps_share = "";
				}
				dbItemA.UpdItem();
			}
		}
		LoadContract();
		BintToGrid1();
	}

	private void EditItemsByKind()
	{
		if (gridBudget1[gridBudget1.Row, "Kind"] == null)
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
			FM_BDGT_EM.Item_sNo = (int)gridBudget1[gridBudget1.Row, "sNO"];
			FM_BDGT_EM.ChildCount = gridBudget1.Rows[gridBudget1.Row].Node.Children;
			FM_BDGT_EM.FormulaStr = gridBudget1[gridBudget1.Row, "Formula"].ToString();
			FM_BDGT_EM.ItemType = CommonMethods.GetBDGT_ItemType(gridBudget1[gridBudget1.Row, "Kind"].ToString());
			FM_BDGT_EM._ShareItems = GetShareItems(gridBudget1.Row);
			FM_BDGT_EM._ShareItemSno = GetShareItemSNo(gridBudget1[gridBudget1.Row, "sNO"].ToString().Trim());
			FM_BDGT_EM._PrintToAnalysis = "";
			FM_BDGT_EM._IsCanPrintToAnalysis = false;
			FM_BDGT_EM._PccesCode = ((gridBudget1[gridBudget1.Row, "PccesCode"] != null) ? gridBudget1[gridBudget1.Row, "PccesCode"].ToString() : "");
			FM_BDGT_EM.Owner = this;
			if (FM_BDGT_EM.ShowDialog() == DialogResult.OK)
			{
				int iPos = gridBudget1.Row;
				int iSno = (int)gridBudget1[gridBudget1.Row, "SNo"];
				ReLoad_OneRow(iSno, iPos);
				if (!(gridBudget1[gridBudget1.Row, "Kind"].ToString() == "B"))
				{
				}
			}
			FM_BDGT_EM.Close();
			FM_BDGT_EM.Dispose();
			FM_BDGT_EM = null;
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "SplitContract.FormSplitContract.cs" + ex.Message);
			MessageBox.Show(this, "Err10:\n" + ex.Message, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		DBCLS = null;
	}

	private ArrayList GetShareItems(int iRow)
	{
		ArrayList RetV = new ArrayList();
		Node LastNode = gridBudget1.Rows[iRow].Node.GetNode(NodeTypeEnum.LastChild);
		if (LastNode != null)
		{
			int iLastIndex = LastNode.Row.SafeIndex;
			for (int i = iRow; i <= iLastIndex; i++)
			{
				if (gridBudget1[i, "Kind"].ToString().Trim() == "L")
				{
					string sItem = gridBudget1[i, "sNO"].ToString() + "|【" + gridBudget1[i, "ItemNo"].ToString().Trim() + "】" + gridBudget1[i, "CName"].ToString().Trim();
					RetV.Add(sItem);
				}
			}
		}
		return RetV;
	}

	private string GetShareItemSNo(string sItem_Sno)
	{
		string RetV = "";
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add("取得該主項大煩的攤提項目的sNO");
		Archnowledge.Pcces.BUDClass.ItemA ITM_A = new Archnowledge.Pcces.BUDClass.ItemA(aArr);
		ITM_A.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		ITM_A.ps_projectCode = F_ProjectCode;
		try
		{
			RetV = ITM_A.GetValue("ShareSno", sItem_Sno, F_ProjectCode);
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "SplitContract.FormSplitContract.cs" + ex.Message);
			MessageBox.Show(this, "Err11:\n" + ex.Message, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		return RetV;
	}

	private void ExecuteBreakdownForm()
	{
		string IPStr = CommonMethods.GetIPAddress();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("顯示單價分析的FORM--" + F_ProjectCode + "(" + IPStr + ")");
		Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
		PROJ.ps_projectCode = F_ProjectCode;
		PROJ.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		F_IsUseIR = PROJ.GetUseIRSet(F_ProjectCode) == "1";
		if (gridBudget1[gridBudget1.Row, "Analysis"] != null && (bool)gridBudget1[gridBudget1.Row, "Analysis"] && gridBudget1[gridBudget1.Row, "PubCode"] != null)
		{
			FormMrsBaseBreakdown formMrsBaseBreakdown = new FormMrsBaseBreakdown();
			formMrsBaseBreakdown.PubCode = (int)gridBudget1[gridBudget1.Row, "PubCode"];
			formMrsBaseBreakdown.ProjectCode = F_ProjectCode;
			formMrsBaseBreakdown._ActionName = F_ActionName;
			formMrsBaseBreakdown._UserID = F_UserID;
			formMrsBaseBreakdown._IsUseIR = F_IsUseIR;
			formMrsBaseBreakdown._IsSBID = Is_SBID;
			formMrsBaseBreakdown._iCostDigital = F_MainCst;
			formMrsBaseBreakdown._ContractApproved = HasApproved;
			F_SNo = (int)gridBudget1[gridBudget1.Row, "sNO"];
			formMrsBaseBreakdown.Owner = this;
			formMrsBaseBreakdown.ShowDialog();
			formMrsBaseBreakdown.Close();
			formMrsBaseBreakdown.Dispose();
			formMrsBaseBreakdown = null;
			int iPos = gridBudget1.Row;
			int iSno = (int)gridBudget1[gridBudget1.Row, "SNo"];
			if (F_IsNeedToReloadAllData)
			{
				LoadContract();
				F_IsNeedToReloadAllData = false;
			}
			else
			{
				ReLoad_OneRow(iSno, iPos);
			}
			HasOpenedBreakdownForm = false;
		}
	}

	private void ReLoad_OneRow(int iSno, int gridRow)
	{
		string IPStr = CommonMethods.GetIPAddress();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("預算書單價分析編輯完後重讀該筆資料--" + F_ProjectCode + "(" + IPStr + ")");
		Archnowledge.Pcces.BUDClass.ItemA dbItemA = new Archnowledge.Pcces.BUDClass.ItemA(aArr);
		dbItemA.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		dbItemA.ps_projectCode = F_ProjectCode;
		DataTable DT_OneRow = dbItemA.ListItem(" sno=" + iSno, F_ProjectCode);
		if (DT_OneRow.Rows.Count <= 0)
		{
			return;
		}
		if (DT_OneRow.Rows[0]["analysis"].ToString().Trim() == "1")
		{
			gridBudget1[gridRow, "Analysis"] = true;
			gridBudget1.Rows[gridRow].Style = gridBudget1.Styles["AnalysisColor"];
			CellRange rg = gridBudget1.GetCellRange(gridRow, gridBudget1.Cols["AnaImg"].SafeIndex);
			rg.Style = gridBudget1.Styles["img"];
			rg.Image = imageList2.Images[0];
		}
		else
		{
			gridBudget1[gridRow, "Analysis"] = false;
			gridBudget1.Rows[gridRow].Style = gridBudget1.Styles["Normal"];
			CellRange rg = gridBudget1.GetCellRange(gridRow, gridBudget1.Cols["AnaImg"].SafeIndex);
			rg.Style = gridBudget1.Styles["img"];
			rg.Image = imageList2.Images[2];
		}
		gridBudget1[gridRow, "ItemNo"] = DT_OneRow.Rows[0]["ItemNo"].ToString().Trim();
		gridBudget1[gridRow, "CName"] = DT_OneRow.Rows[0]["cName"].ToString().Trim();
		gridBudget1[gridRow, "UnitName"] = DT_OneRow.Rows[0]["unitName"].ToString().Trim();
		gridBudget1[gridRow, "Qty"] = DT_OneRow.Rows[0]["qty"];
		gridBudget1[gridRow, "Cost"] = DT_OneRow.Rows[0]["cost"];
		gridBudget1[gridRow, "Amount"] = DT_OneRow.Rows[0]["amount"];
		gridBudget1[gridRow, "PccesCode"] = DT_OneRow.Rows[0]["pccesCode"].ToString().Trim();
		gridBudget1[gridRow, "Memo"] = DT_OneRow.Rows[0]["memo"].ToString().Trim();
		gridBudget1[gridRow, "EName"] = DT_OneRow.Rows[0]["eName"].ToString().Trim();
		gridBudget1[gridRow, "EUnit"] = DT_OneRow.Rows[0]["eUnit"].ToString().Trim();
		gridBudget1[gridRow, "LevelNo"] = DT_OneRow.Rows[0]["levelNo"].ToString().Trim();
		gridBudget1[gridRow, "SNo"] = DT_OneRow.Rows[0]["sno"];
		gridBudget1[gridRow, "Kind"] = DT_OneRow.Rows[0]["kind"].ToString().Trim();
		gridBudget1[gridRow, "PrintNo"] = DT_OneRow.Rows[0]["printNo"].ToString().Trim();
		gridBudget1[gridRow, "Formula"] = DT_OneRow.Rows[0]["Formula"].ToString().Trim();
		gridBudget1[gridRow, "PubCode"] = DT_OneRow.Rows[0]["pubCode"].ToString().Trim();
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
			gridBudget1.Rows[gridRow].Style = gridBudget1.Styles["MainColor"];
			break;
		}
	}

	private void Execute_About()
	{
		FormAbout FMAB = new FormAbout();
		FMAB.ShowDialog();
		FMAB.Close();
		FMAB.Dispose();
		FMAB = null;
	}

	private void Do_CancelApprove()
	{
		if (MessageBox.Show(this, "確定要取消此契約書核准?", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			ArrayList tmp_AL1 = new ArrayList();
			tmp_AL1 = new ArrayList();
			tmp_AL1.Add(F_UserID);
			tmp_AL1.Add("(subctr) 契約書明細-取消契約書核定");
			string ls_prjcode = F_ProjectCode;
			string ls_subproj = F_SubProjetCode;
			subProject subcom = new subProject(tmp_AL1);
			int li_mode = subcom.UnLockSproj(ls_subproj, ls_prjcode);
			HasApproved = li_mode != 1;
			if (li_mode == 1 && li_mode == 1)
			{
				((ButtonTool)ultraToolbarsManager1.Tools["mnuGetCost"]).SharedProps.Enabled = true;
				((ButtonTool)ultraToolbarsManager1.Tools["mnuApprove"]).SharedProps.Enabled = true;
				((ButtonTool)ultraToolbarsManager1.Tools["mnuReItem"]).SharedProps.Enabled = true;
				((ButtonTool)ultraToolbarsManager1.Tools["mnuEditBDGT"]).SharedProps.Enabled = true;
				((ButtonTool)ultraToolbarsManager1.Tools["mnuReCal"]).SharedProps.Enabled = true;
				((ButtonTool)ultraToolbarsManager1.Tools["mnu_AdjustTot_CNT"]).SharedProps.Enabled = true;
				((ButtonTool)ultraToolbarsManager1.Tools["mnuDelItem_BDGT"]).SharedProps.Enabled = true;
				((ButtonTool)ultraToolbarsManager1.Tools["mnuImport"]).SharedProps.Enabled = true;
				gridBudget1.Cols["Qty"].AllowEditing = true;
				gridBudget1.Cols["Cost"].AllowEditing = true;
				gridBudget1.Cols["Amount"].AllowEditing = true;
				gridBudget1.Cols["Lock"].AllowEditing = true;
				gridBudget1.Cols["AccMode"].AllowEditing = true;
				((ButtonTool)ultraToolbarsManager1.Tools["mnuPrint_CNT"]).SharedProps.Enabled = false;
				((ButtonTool)ultraToolbarsManager1.Tools["mnuFile_Digital"]).SharedProps.Enabled = false;
			}
			PubTools.WriteRoughlyLog(tmp_AL1);
			subcom = null;
			BintToGrid1();
			((ButtonTool)ultraToolbarsManager1.Tools["mnuToolCancel"]).SharedProps.Enabled = false;
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

	private void Do_ItemReArrange()
	{
		string sQuestion = "確定執行項次重整嗎?";
		if (MessageBox.Show(this, sQuestion, "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			gridBudget1.Enabled = false;
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
				Cursor = Cursors.WaitCursor;
				Application.DoEvents();
				Save2DT();
				SaveCNT();
				Application.DoEvents();
				Cursor = Cursors.Default;
				gridBudget1.Enabled = true;
				gridBudget1.Refresh();
				FM_INFO.Close();
				FM_INFO.Dispose();
				Application.DoEvents();
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
		for (int i = 1; i < gridBudget1.Rows.Count; i++)
		{
			if (gridBudget1.Rows[i].IsNode && gridBudget1[i, "Kind"] != null)
			{
				if (!(sSwitcher.ToUpper() == "ALL"))
				{
					if (sSwitcher.ToUpper() == "M")
					{
						if (gridBudget1[i, "Kind"].ToString().Trim() == "W")
						{
							continue;
						}
					}
					else if (sSwitcher.ToUpper() == "W" && gridBudget1[i, "Kind"].ToString().Trim() != "W")
					{
						continue;
					}
				}
				BDGT_ITEM1._ItemKind = gridBudget1[i, "Kind"].ToString().Trim();
				BDGT_ITEM1._PccesCode = ((gridBudget1[i, "PccesCode"] != null) ? gridBudget1[i, "PccesCode"].ToString().Trim() : "");
				gridBudget1[i, "ItemNo"] = BDGT_ITEM1.GetItemNoByPrintNo(gridBudget1[i, "PrintNo"].ToString().Trim());
			}
			if (i % 20 == 0)
			{
				Application.DoEvents();
				Cursor = Cursors.WaitCursor;
			}
		}
		Cursor = Cursors.Default;
	}

	private void DoAssembleCode()
	{
		string remOldPrintNo = "";
		string rmeNewPrintNo = "";
		L1[1] = 0;
		L1[2] = 0;
		L1[3] = 0;
		L1[4] = 0;
		L1[5] = 0;
		L1[6] = 0;
		L1[7] = 0;
		L1[8] = 0;
		bool IsItemBChanged = false;
		for (int i = 1; i <= gridBudget1.Rows.Count - 1; i++)
		{
			if (!gridBudget1.Rows[i].IsNode)
			{
				continue;
			}
			if (gridBudget1[i, "PrintNo"] != null)
			{
				string sPNT_NO = gridBudget1[i, "PrintNo"].ToString().Trim();
				if (sPNT_NO == "99999999999999999999999999999999")
				{
					continue;
				}
			}
			remOldPrintNo = gridBudget1[i, "PrintNo"].ToString().Trim();
			gridBudget1[i, "PrintNo"] = AssembleCode((gridBudget1.Rows[i].Node == null) ? 1 : gridBudget1.Rows[i].Node.Level);
			gridBudget1[i, "LevelNo"] = gridBudget1[i, "PrintNo"].ToString().Trim().Length / 4;
			rmeNewPrintNo = gridBudget1[i, "PrintNo"].ToString().Trim();
			if (rmeNewPrintNo != remOldPrintNo)
			{
				ChangeItemBTable_Step1(rmeNewPrintNo, remOldPrintNo);
				IsItemBChanged = true;
			}
		}
		if (IsItemBChanged)
		{
			ChangeItemBTable_Step2();
		}
	}

	private void ChangeItemBTable_Step1(string NewPrintNo, string OldPrintNo)
	{
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(subctr) 契約書明");
		ModifyDB StdCom = new ModifyDB(F_ProjectCode, tmp_AL1);
		string l_str = "select * from subItemB where projectCode = '" + F_ProjectCode + "' and parentCode = '" + OldPrintNo + "'";
		DataTable DT = StdCom.DBList(l_str);
		if (DT.Rows.Count > 0)
		{
			l_str = "Update subItemB set parentCode ='A" + NewPrintNo + "' where projectCode = '" + F_ProjectCode + "' and parentCode = '" + OldPrintNo + "'";
			StdCom.DBUpd(l_str);
		}
		l_str = "select * from subItemC where projectCode = '" + F_ProjectCode + "' and printNo = '" + OldPrintNo + "'";
		DT = StdCom.DBList(l_str);
		if (DT.Rows.Count > 0)
		{
			l_str = "Update subItemC set printNo ='A" + NewPrintNo + "' where projectCode = '" + F_ProjectCode + "' and printNo = '" + OldPrintNo + "'";
			StdCom.DBUpd(l_str);
		}
		StdCom = null;
		tmp_AL1 = null;
	}

	private void ChangeItemBTable_Step2()
	{
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(subctr) 契約書明");
		ModifyDB StdCom = new ModifyDB(F_ProjectCode, tmp_AL1);
		string sql = "UPDATE subItemB SET ParentCode=SUBSTRING(ParentCode,2,32) where projectcode='" + F_ProjectCode + "' and ParentCode !='" + "".PadLeft(32, '9') + "'  and SUBSTRING(ParentCode,1,1)='A' " + '\r';
		string text = sql;
		sql = text + "UPDATE subItemC SET printNo=SUBSTRING(printNo,2,32) where projectcode='" + F_ProjectCode + "' and printNo !='" + "".PadLeft(32, '9') + "'  and SUBSTRING(printNo,1,1)='A' ";
		StdCom.DBUpd(sql);
		StdCom = null;
		tmp_AL1 = null;
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

	private void Execute_ItemNoSetting()
	{
		FormBudgetItemNo FM_ITMSET = new FormBudgetItemNo();
		FM_ITMSET._ActionName = F_ActionName;
		FM_ITMSET._UserID = F_UserID;
		FM_ITMSET._ProjectCode = F_ProjectCode;
		FM_ITMSET.ShowDialog(this);
		FM_ITMSET.Close();
		FM_ITMSET.Dispose();
		FM_ITMSET = null;
	}

	private void Do_DelItem_BDGT()
	{
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(subctr) 契約書明細");
		sub_Ctr ctrcom = new sub_Ctr(tmp_AL1);
		ctrcom.ps_prjcode = F_ProjectCode;
		ctrcom.ps_subcode = F_SubProjetCode;
		if (MessageBox.Show(this, "確定要刪除選定的 " + gridBudget1.SelectedRowCount + " 筆資料?", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			for (int i = 1; i < gridBudget1.Rows.Count; i++)
			{
				if (gridBudget1.Rows[i].Selected)
				{
					string ls_PrintNo = gridBudget1[i, "PrintNo"].ToString().Trim();
					ctrcom.DeleItem(ls_PrintNo, F_SubProjetCode, F_ProjectCode);
				}
			}
		}
		LoadContract();
		BintToGrid1();
	}

	private void Execute_Adjust()
	{
		FormSplitCnt_ResetCost FM_ADJUST = new FormSplitCnt_ResetCost();
		FM_ADJUST._ProjectCode = F_ProjectCode;
		FM_ADJUST._UserID = F_UserID;
		FM_ADJUST._TotalAmount = PubTools.Str2Double(lblTotal.Text);
		FM_ADJUST._OldTotalAmount = F_OldTotalAmount;
		FM_ADJUST.Owner = this;
		if (FM_ADJUST.ShowDialog() == DialogResult.OK)
		{
			LoadContract();
			BintToGrid1();
		}
		FM_ADJUST.Close();
		FM_ADJUST.Dispose();
		FM_ADJUST = null;
	}

	private void Execute_BASIC_CNT()
	{
		FormSplitCnt_Basic FM_BASIC = new FormSplitCnt_Basic();
		FM_BASIC._ProjectCode = F_ProjectCode;
		FM_BASIC._ProjectName = F_ProjectNameC;
		FM_BASIC._SubProjectCode = F_SubProjetCode;
		FM_BASIC._UserID = F_UserID;
		FM_BASIC._HasApproved = HasApproved;
		FM_BASIC.Owner = this;
		if (FM_BASIC.ShowDialog() == DialogResult.OK)
		{
			LoadContract();
			BintToGrid1();
		}
		FM_BASIC.Close();
		FM_BASIC.Dispose();
		FM_BASIC = null;
	}

	private void CloseThisForm()
	{
		string sWarning = "確定要結束 ?";
		if (MessageBox.Show(this, sWarning, "契約書編製", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			(base.ParentForm as frmPccesMain).LeftPanel.Width = 160;
			Close();
		}
	}

	private void Do_CntDetail()
	{
		ultraToolbarsManager1.Toolbars["Menu1"].IsMainMenuBar = true;
		ultraToolbarsManager1.Toolbars["Menu1"].Visible = true;
		ultraToolbarsManager1.Toolbars["Menu1"].DockedRow = 0;
		ultraToolbarsManager1.Toolbars["Tool1"].Visible = true;
		ultraToolbarsManager1.Toolbars["Tool1"].DockedRow = 1;
		ultraToolbarsManager1.Toolbars["Tool2"].Visible = false;
		Tab_A.Tab.Selected = true;
	}

	private void Execute_EditBDGT()
	{
		FormSplitCnt_ItemPick FM_SPLT_PK = new FormSplitCnt_ItemPick();
		FM_SPLT_PK._UserID = F_UserID;
		FM_SPLT_PK._ActionName = F_ActionName;
		FM_SPLT_PK._ProjectCode = F_ProjectCode;
		FM_SPLT_PK._DT1 = DT1;
		if (FM_SPLT_PK.ShowDialog(this) == DialogResult.OK)
		{
			FixItemAPubCode();
			LoadContract();
			BintToGrid1();
			Do_ReCal_All();
		}
		FM_SPLT_PK.Close();
		FM_SPLT_PK.Dispose();
		FM_SPLT_PK = null;
	}

	public DataTable FixPubCode()
	{
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = F_UserID;
		SysUser oSysUser = new SysUser();
		string ssDBName = oSysUser.GetSysUserDatabaseName(F_UserID);
		string sSQL = "Select PccesCode, PubCode From " + CommonMethods.GetActionNameString(F_ActionName) + "ProjMrsA Where ProjectCode='" + F_ProjectCode + "' ";
		DataTable DT_Process = DBCLS.GetUserDefine(sSQL);
		DBCLS = null;
		return FixPubCode(DT_Process);
	}

	public DataTable FixItemAPubCode()
	{
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = F_UserID;
		SysUser oSysUser = new SysUser();
		string ssDBName = oSysUser.GetSysUserDatabaseName(F_UserID);
		string sSQL = "Select PccesCode, PubCode From " + CommonMethods.GetActionNameString(F_ActionName) + "ItemA Where ProjectCode='" + F_ProjectCode + "' ";
		DataTable DT_Process = DBCLS.GetUserDefine(sSQL);
		DBCLS = null;
		return FixItemAPubCode(DT_Process);
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
		DataSet trgDS = RESET2.GetDataSet2(ssDBName, "MRS", "", srcDT, 1);
		DataSet trgDSP = RESET2.GetDataSet2(ssDBName, CommonMethods.GetActionNameString(F_ActionName), F_ProjectCode, srcDT2, 1);
		trgDS.Tables[0].CaseSensitive = true;
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
				sSQLCmd = "Update " + CommonMethods.GetActionNameString(F_ActionName) + "ProjMrsA Set pubCode =" + trgDSP.Tables[0].Rows[i]["resCode"].ToString() + " Where ProjectCode ='" + F_ProjectCode + "' And PubCode=" + trgDSP.Tables[0].Rows[i]["PubCode"].ToString() + '\r';
				object obj = sSQLCmd;
				sSQLCmd = string.Concat(obj, "Update ", CommonMethods.GetActionNameString(F_ActionName), "ProjMrsB Set ParentCode =", trgDSP.Tables[0].Rows[i]["resCode"].ToString(), " Where ProjectCode ='", F_ProjectCode, "' And ParentCode=", trgDSP.Tables[0].Rows[i]["PubCode"].ToString(), '\r');
				obj = sSQLCmd;
				sSQLCmd = string.Concat(obj, "Update ", CommonMethods.GetActionNameString(F_ActionName), "ProjMrsB Set pubCode =", trgDSP.Tables[0].Rows[i]["resCode"].ToString(), " Where ProjectCode ='", F_ProjectCode, "' And PubCode=", trgDSP.Tables[0].Rows[i]["PubCode"].ToString(), '\r');
				obj = sSQLCmd;
				sSQLCmd = string.Concat(obj, "Update ", CommonMethods.GetActionNameString(F_ActionName), "ProjMrsC Set ParentCode =", trgDSP.Tables[0].Rows[i]["resCode"].ToString(), " Where ProjectCode ='", F_ProjectCode, "' And ParentCode=", trgDSP.Tables[0].Rows[i]["PubCode"].ToString(), '\r');
				obj = sSQLCmd;
				sSQLCmd = string.Concat(obj, "Update ", CommonMethods.GetActionNameString(F_ActionName), "ProjMrsC Set pubCode =", trgDSP.Tables[0].Rows[i]["resCode"].ToString(), " Where ProjectCode ='", F_ProjectCode, "' And PubCode=", trgDSP.Tables[0].Rows[i]["PubCode"].ToString(), '\r');
				obj = sSQLCmd;
				sSQLCmd = string.Concat(obj, "Update ", CommonMethods.GetActionNameString(F_ActionName), "ProjMrsC Set itemCode =", trgDSP.Tables[0].Rows[i]["resCode"].ToString(), " Where ProjectCode ='", F_ProjectCode, "' And itemCode=", trgDSP.Tables[0].Rows[i]["PubCode"].ToString(), '\r');
				obj = sSQLCmd;
				sSQLCmd = string.Concat(obj, "Update ", CommonMethods.GetActionNameString(F_ActionName), "ItemA Set pubCode =", trgDSP.Tables[0].Rows[i]["resCode"].ToString(), " Where ProjectCode ='", F_ProjectCode, "' And PubCode=", trgDSP.Tables[0].Rows[i]["PubCode"].ToString(), '\r');
				DBCLS.ExecuteCommand(sSQLCmd);
			}
		}
		DBCLS = null;
		trgDS = null;
		return trgDSP.Tables[0];
	}

	public DataTable FixItemAPubCode(DataTable srcDT)
	{
		DateTime T1 = DateTime.Now;
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("單筆引用單價");
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = F_UserID;
		SysUser oSysUser = new SysUser();
		string ssDBName = oSysUser.GetSysUserDatabaseName(F_UserID);
		ModifyDB StdCom = new ModifyDB("", aArr);
		string l_str = "Select PccesCode, PubCode  from " + CommonMethods.GetActionNameString(F_ActionName) + "ProjMrsA where projectCode = '" + F_ProjectCode + "'";
		DataTable trgDSP = StdCom.DBList(l_str);
		trgDSP.CaseSensitive = true;
		srcDT.CaseSensitive = true;
		string sSQLCmd = "";
		for (int i = 0; i < srcDT.Rows.Count; i++)
		{
			if (!(srcDT.Rows[i]["PccesCode"].ToString().Trim() == ""))
			{
				DataRow[] MrsDr = trgDSP.Select("PccesCode ='" + srcDT.Rows[i]["PccesCode"].ToString().Trim() + "'", "PccesCode");
				if (MrsDr.Length > 0)
				{
					object obj = sSQLCmd;
					sSQLCmd = string.Concat(obj, "Update ", CommonMethods.GetActionNameString(F_ActionName), "ItemA Set pubCode =", MrsDr[0]["PubCode"].ToString(), " Where ProjectCode ='", F_ProjectCode, "' And pccesCode='", srcDT.Rows[i]["PccesCode"].ToString().Trim(), "'", '\r');
				}
			}
		}
		if (sSQLCmd != "")
		{
			DBCLS.ExecuteCommand(sSQLCmd);
		}
		StdCom = null;
		DBCLS = null;
		return srcDT;
	}

	private void Do_GetBackCost()
	{
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(subctr) 契約書明細-取回預算單價");
		sub_Ctr ctrcom = new sub_Ctr(tmp_AL1);
		string ls_prjcode = F_ProjectCode;
		string ls_subproj = F_SubProjetCode;
		int temp = ctrcom.ReBudCost(ls_subproj, ls_prjcode);
		DT1 = ctrcom.ListItem("", ls_subproj, ls_prjcode, ArrDecimal);
		ctrcom = null;
		PubTools.WriteRoughlyLog(tmp_AL1);
		BintToGrid1();
		Do_ReCal_All();
	}

	private void Do_Approve()
	{
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(subctr) 契約書明細-契約書核定");
		string ls_prjcode = F_ProjectCode;
		string ls_subproj = F_SubProjetCode;
		sub_info SubInfoCom = new sub_info(tmp_AL1);
		DataTable ldt_Info = SubInfoCom.ListItem(ls_subproj, ls_prjcode);
		if (ldt_Info.Rows.Count <= 0)
		{
			MessageBox.Show(this, "請先填寫契約書基本資料，才能作契約書核定。\n\n[編輯]-->[編輯契約基本資料...]", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		if (ldt_Info.Rows.Count > 0 && ldt_Info.Rows[0]["owner"].ToString() == "")
		{
			MessageBox.Show(this, "請先填寫契約書基本資料，才能作契約書核定。\n\n[編輯]-->[編輯契約基本資料...]", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		Archnowledge.Pcces.BUDClass.ItemA ItemACom = new Archnowledge.Pcces.BUDClass.ItemA(tmp_AL1);
		ItemACom.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		double org_Amount = ItemACom.GetAmount(F_ProjectCode);
		if (org_Amount == 0.0)
		{
			string ssWarning = "此專案目前總金額為 0 \n\n請先重新總計後再核定此契約書。\n\n";
			MessageBox.Show(this, ssWarning, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		ItemACom = null;
		if (MessageBox.Show(this, "確定要核准此契約書?", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			Archnowledge.Pcces.BUDClass.Project proj = new Archnowledge.Pcces.BUDClass.Project(tmp_AL1);
			proj.ps_projectCode = F_ProjectCode;
			proj.ps_srckind = "BUD";
			DataTable dt = proj.ListItem("", F_ProjectCode);
			subProject subcom = new subProject(tmp_AL1);
			if (dt.Rows.Count > 0)
			{
				subcom.ps_mainCode = dt.Rows[0]["mainCode"].ToString();
				subcom.ps_mainCName = dt.Rows[0]["mainCName"].ToString();
				subcom.ps_projectNameC = dt.Rows[0]["projectNameC"].ToString();
				subcom.ps_projectNameE = dt.Rows[0]["projectNameE"].ToString();
				subcom.ps_projectAddress = dt.Rows[0]["projectAddress"].ToString();
			}
			int li_mode = subcom.LockSproj(ls_subproj, ls_prjcode);
			HasApproved = li_mode == 1;
			if (li_mode == 1 && li_mode == 1)
			{
				((ButtonTool)ultraToolbarsManager1.Tools["mnuGetCost"]).SharedProps.Enabled = false;
				((ButtonTool)ultraToolbarsManager1.Tools["mnuApprove"]).SharedProps.Enabled = false;
				((ButtonTool)ultraToolbarsManager1.Tools["mnuReItem"]).SharedProps.Enabled = false;
				((ButtonTool)ultraToolbarsManager1.Tools["mnuEditBDGT"]).SharedProps.Enabled = false;
				((ButtonTool)ultraToolbarsManager1.Tools["mnuReCal"]).SharedProps.Enabled = false;
				((ButtonTool)ultraToolbarsManager1.Tools["mnu_AdjustTot_CNT"]).SharedProps.Enabled = false;
				((ButtonTool)ultraToolbarsManager1.Tools["mnuDelItem_BDGT"]).SharedProps.Enabled = false;
				((ButtonTool)ultraToolbarsManager1.Tools["mnuImport"]).SharedProps.Enabled = false;
				((ButtonTool)ultraToolbarsManager1.Tools["mnuPrint_CNT"]).SharedProps.Enabled = true;
				((ButtonTool)ultraToolbarsManager1.Tools["mnuFile_Digital"]).SharedProps.Enabled = true;
				gridBudget1.Cols["Qty"].AllowEditing = false;
				gridBudget1.Cols["Cost"].AllowEditing = false;
				gridBudget1.Cols["Amount"].AllowEditing = false;
				gridBudget1.Cols["Lock"].AllowEditing = false;
				gridBudget1.Cols["AccMode"].AllowEditing = false;
			}
			PubTools.WriteRoughlyLog(tmp_AL1);
			LoadContract();
			BintToGrid1();
			if (subcom.IsCanUnLockSproj(ls_subproj, ls_prjcode))
			{
				((ButtonTool)ultraToolbarsManager1.Tools["mnuToolCancel"]).SharedProps.Enabled = true;
			}
			else
			{
				((ButtonTool)ultraToolbarsManager1.Tools["mnuToolCancel"]).SharedProps.Enabled = false;
			}
			subcom = null;
		}
	}

	private void Do_ReCal_All()
	{
		try
		{
			bool EnableNewCalculateCost = false;
			Archnowledge.Pcces.DomainModule.General.PubProject thePubProject = new Archnowledge.Pcces.DomainModule.General.PubProject();
			if (thePubProject.GetPubProjectEnableNewCalculateCost(F_ProjectCode))
			{
				DoNewCalculate();
			}
			else
			{
				DoOldCalculate();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Error : " + ex.Message);
			if (FM_INFO != null)
			{
				FM_INFO.Close();
				FM_INFO.Dispose();
				FM_INFO = null;
				Application.DoEvents();
			}
		}
	}

	private void DoNewCalculate()
	{
		if (IsAuto || MessageBox.Show(this, "確定要執行重新總計?", "訊問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			gridBudget1.Enabled = false;
			FM_INFO = new FormSys_G_Info1();
			FM_INFO._InfoString = "重新總計中，請稍候! ";
			FM_INFO.Owner = this;
			FM_INFO._MaxValue = 0;
			FM_INFO.Show();
			FM_INFO.BringToFront();
			Application.DoEvents();
			Cursor = Cursors.WaitCursor;
			Application.DoEvents();
			ItemCalculate theItemCalculate = new ItemCalculate(F_ActionName, ProjectCode, 0);
			ExecResult ER = theItemCalculate.CalculateAll(IncludeResource: true, IncludeMrs: true, ProgressEventHandler, ProgressEventHandlerInitMaxProgressValue);
			FM_INFO.Hide();
			if (ER.ReturnCode == 0 && !IsAuto)
			{
				Cursor = Cursors.Default;
				MessageBox.Show(this, "重新總計完成!", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				theProject.UpdateProjectIsReCal(ProjectCode, "N");
			}
			else if (ER.ReturnCode != 0 && !IsAuto)
			{
				string sMessage = "重新總計失敗，請檢查後再執行!\n\n例如:\n(1)單價分析子項引用了與父項相同的工項\n     比如:【清除與掘除】的分析子項又引用了一次【清除與掘除】\n\n(2)單價分析子項沒有設定雜項。\n     因為產生差額要攤給雜項，有單價分析並未設定雜項。\n     可使用【檢視】-->【專案工項維護】-->【計算錯誤項目】幫你篩選出有狀況(2)之項目。\n     或是至【工具】-->【選項...】-->【計算方式】-->勾選【一律不作攤提】\n\nError : " + ER.Message;
				MessageBox.Show(this, sMessage, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			gridBudget1.Enabled = true;
			gridBudget1.Refresh();
			Application.DoEvents();
			LoadContract();
			BintToGrid1();
			Cursor = Cursors.Default;
			FM_INFO.Close();
			FM_INFO.Dispose();
			FM_INFO = null;
			Application.DoEvents();
		}
	}

	private void DoOldCalculate()
	{
		if (!IsAuto && MessageBox.Show(this, "確定要執行重新總計?", "訊問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
		{
			return;
		}
		lock (this)
		{
			gridBudget1.Enabled = false;
			FormSys_G_Info1 FM_INFO = new FormSys_G_Info1();
			FM_INFO.TopMost = true;
			FM_INFO._InfoString = "重新總計中，請稍候! ";
			FM_INFO.Owner = this;
			FM_INFO.Show();
			FM_INFO.BringToFront();
			Application.DoEvents();
			Cursor = Cursors.WaitCursor;
			Application.DoEvents();
			string IPStr = CommonMethods.GetIPAddress();
			ArrayList aArr = new ArrayList();
			aArr.Clear();
			aArr.Add(F_UserID);
			aArr.Add("重新總計--" + F_ProjectCode + "(" + IPStr + ")");
			Archnowledge.Pcces.BUDClass.ItemA dbItemA = new Archnowledge.Pcces.BUDClass.ItemA(aArr);
			dbItemA.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
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
			if (iResult == 1 && !IsAuto)
			{
				FM_INFO.Hide();
				((ButtonTool)ultraToolbarsManager1.Tools["mnuApprove"]).SharedProps.Enabled = true;
				MessageBox.Show(this, "重新總計完成!", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else if (iResult != 1 && !IsAuto)
			{
				FM_INFO.Hide();
				string sMessage = "重新總計失敗，請檢查後再執行!\n\n例如:\n(1)單價分析子項引用了與父項相同的工項\n     比如:【清除與掘除】的分析子項又引用了一次【清除與掘除】\n\n(2)單價分析子項沒有設定雜項。\n     因為產生差額要攤給雜項，有單價分析並未設定雜項。\n     可使用【檢視】-->【專案工項維護】-->【計算錯誤項目】幫你篩選出有狀況(2)之項目。\n     或是至【工具】-->【選項...】-->【計算方式】-->勾選【一律不作攤提】";
				MessageBox.Show(this, sMessage, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			gridBudget1.Enabled = true;
			gridBudget1.Refresh();
			Cursor = Cursors.WaitCursor;
			Application.DoEvents();
			LoadContract();
			BintToGrid1();
			if (iResult != 1 && !IsAuto)
			{
				((ButtonTool)ultraToolbarsManager1.Tools["mnuApprove"]).SharedProps.Enabled = false;
			}
			Cursor = Cursors.Default;
			sub_Ctr SubCtrCom = new sub_Ctr(aArr);
			double ld_Amount = SubCtrCom.GetAmount("", F_ProjectCode);
			sub_info sub_info1 = new sub_info(aArr);
			sub_info1.ps_ProjectCode = F_ProjectCode;
			sub_info1.ps_Sproj = "";
			sub_info1.ps_ProjAmt = ld_Amount.ToString();
			sub_info1.UpdItem();
			FM_INFO.Close();
			FM_INFO.Dispose();
			Application.DoEvents();
		}
	}

	private bool Save2DT()
	{
		for (int i = 1; i < gridBudget1.Rows.Count; i++)
		{
			if (gridBudget1[i, "SNo"] != null)
			{
				DT1.Rows[i - 1]["ItemNo"] = gridBudget1[i, "ItemNo"].ToString();
				DT1.Rows[i - 1]["cName"] = gridBudget1[i, "CName"].ToString();
				DT1.Rows[i - 1]["unitName"] = gridBudget1[i, "UnitName"].ToString();
				DT1.Rows[i - 1]["qty"] = gridBudget1[i, "Qty"].ToString();
				DT1.Rows[i - 1]["cost"] = gridBudget1[i, "Cost"].ToString();
				DT1.Rows[i - 1]["amount"] = gridBudget1[i, "Amount"].ToString();
				DT1.Rows[i - 1]["memo"] = gridBudget1[i, "Memo"].ToString();
				DT1.Rows[i - 1]["eName"] = gridBudget1[i, "EName"].ToString();
				DT1.Rows[i - 1]["eUnit"] = gridBudget1[i, "EUnit"].ToString();
				DT1.Rows[i - 1]["levelNo"] = gridBudget1[i, "LevelNo"].ToString();
				DT1.Rows[i - 1]["sno"] = gridBudget1[i, "SNo"].ToString();
				DT1.Rows[i - 1]["kind"] = gridBudget1[i, "Kind"].ToString();
				DT1.Rows[i - 1]["printNo"] = gridBudget1[i, "PrintNo"].ToString().Trim();
				DT1.Rows[i - 1]["Formula"] = gridBudget1[i, "Formula"].ToString();
				DT1.Rows[i - 1]["pubCode"] = gridBudget1[i, "PubCode"].ToString();
				DT1.Rows[i - 1]["DsctLock"] = (((bool)gridBudget1[i, "Lock"]) ? "1" : "");
			}
		}
		return true;
	}

	private void FormSplitContract_Load(object sender, EventArgs e)
	{
		SettingDecimal();
		RememberColsProps();
		FormSplitContract_Resize(null, null);
		base.ParentForm.Text = "PCCES Win 4.3 【契約管理】";
		functionButtons1._UserID = F_UserID;
		functionButtons1._UserName = F_UserName;
		functionButtons1._ServerName = F_ServerName;
		functionButtons1._CurrOpenMode = FunctionOpenMode.Invoice;
		functionButtons1._ActiveFunction = "SPLIT_CONTRACT";
		onlineList1._UserID = F_UserID;
		onlineList1._UserName = F_UserName;
		onlineList1._ServerName = F_ServerName;
		onlineList1._FunctionName = F_FunctionName;
		onlineList1._HasRegistered = F_HasRegistered;
		onlineList1.Connect();
		SysUser oSysUser = new SysUser();
		ultraStatusBar1.Panels[1].Text = "目前資料庫：" + oSysUser.GetSysUserDatabaseDesc(F_UserID);
		F_CurrentDBName = oSysUser.GetSysUserDatabaseName(F_UserID);
		lblProjectData.Text = "【" + F_ProjectCode + "】" + F_ProjectNameC;
		ultraToolbarsManager1.Toolbars["Tool2"].Visible = false;
		Tab_A.Tab.Selected = true;
		gridBudget1.Cols["PrintNo"].Visible = false;
		LoadContract();
		BintToGrid1();
	}

	private void LoadContract()
	{
		lblProjectData.Text = "【" + F_ProjectCode + "】" + F_ProjectNameC;
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(subctr) 契約書明細");
		subProject subcom = new subProject(tmp_AL1);
		string ls_prjcode = F_ProjectCode;
		string ls_subproj = F_SubProjetCode;
		int li_mode = subcom.ChkLock(ls_subproj, ls_prjcode);
		if (subcom.IsCanUnLockSproj(ls_subproj, ls_prjcode))
		{
			((ButtonTool)ultraToolbarsManager1.Tools["mnuToolCancel"]).SharedProps.Enabled = true;
		}
		else
		{
			((ButtonTool)ultraToolbarsManager1.Tools["mnuToolCancel"]).SharedProps.Enabled = false;
		}
		subcom = null;
		HasApproved = li_mode == 1;
		if (li_mode == 1)
		{
			((ButtonTool)ultraToolbarsManager1.Tools["mnuGetCost"]).SharedProps.Enabled = false;
			((ButtonTool)ultraToolbarsManager1.Tools["mnuApprove"]).SharedProps.Enabled = false;
			((ButtonTool)ultraToolbarsManager1.Tools["mnuReItem"]).SharedProps.Enabled = false;
			((ButtonTool)ultraToolbarsManager1.Tools["mnuEditBDGT"]).SharedProps.Enabled = false;
			((ButtonTool)ultraToolbarsManager1.Tools["mnuReCal"]).SharedProps.Enabled = false;
			((ButtonTool)ultraToolbarsManager1.Tools["mnu_AdjustTot_CNT"]).SharedProps.Enabled = false;
			((ButtonTool)ultraToolbarsManager1.Tools["mnuDelItem_BDGT"]).SharedProps.Enabled = false;
			((ButtonTool)ultraToolbarsManager1.Tools["mnuImport"]).SharedProps.Enabled = false;
			((ButtonTool)ultraToolbarsManager1.Tools["mnuFile_Digital"]).SharedProps.Enabled = true;
			((ButtonTool)ultraToolbarsManager1.Tools["mnuPrint_CNT"]).SharedProps.Enabled = true;
			gridBudget1.Cols["Qty"].AllowEditing = false;
			gridBudget1.Cols["Cost"].AllowEditing = false;
			gridBudget1.Cols["Amount"].AllowEditing = false;
			gridBudget1.Cols["Lock"].AllowEditing = false;
			gridBudget1.Cols["AccMode"].AllowEditing = false;
		}
		else
		{
			((ButtonTool)ultraToolbarsManager1.Tools["mnuGetCost"]).SharedProps.Enabled = true;
			((ButtonTool)ultraToolbarsManager1.Tools["mnuApprove"]).SharedProps.Enabled = true;
			((ButtonTool)ultraToolbarsManager1.Tools["mnuReItem"]).SharedProps.Enabled = true;
			((ButtonTool)ultraToolbarsManager1.Tools["mnuEditBDGT"]).SharedProps.Enabled = true;
			((ButtonTool)ultraToolbarsManager1.Tools["mnuReCal"]).SharedProps.Enabled = true;
			((ButtonTool)ultraToolbarsManager1.Tools["mnu_AdjustTot_CNT"]).SharedProps.Enabled = true;
			((ButtonTool)ultraToolbarsManager1.Tools["mnuDelItem_BDGT"]).SharedProps.Enabled = true;
			((ButtonTool)ultraToolbarsManager1.Tools["mnuImport"]).SharedProps.Enabled = true;
			((ButtonTool)ultraToolbarsManager1.Tools["mnuFile_Digital"]).SharedProps.Enabled = false;
			((ButtonTool)ultraToolbarsManager1.Tools["mnuPrint_CNT"]).SharedProps.Enabled = false;
			gridBudget1.Cols["Qty"].AllowEditing = true;
			gridBudget1.Cols["Cost"].AllowEditing = true;
			gridBudget1.Cols["Amount"].AllowEditing = true;
			gridBudget1.Cols["Lock"].AllowEditing = true;
			gridBudget1.Cols["AccMode"].AllowEditing = true;
		}
		PubTools.WriteRoughlyLog(tmp_AL1);
		Archnowledge.Pcces.BUDClass.ItemA dbItemA = new Archnowledge.Pcces.BUDClass.ItemA(tmp_AL1);
		dbItemA.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		dbItemA.ps_projectCode = F_ProjectCode;
		DT1 = dbItemA.ListItem("", F_ProjectCode);
		CheckDisable(li_mode);
	}

	private void CheckDisable(int iMode)
	{
		if (DT1.Rows.Count == 0)
		{
			((ButtonTool)ultraToolbarsManager1.Tools["mnuGetCost"]).SharedProps.Enabled = false;
			((ButtonTool)ultraToolbarsManager1.Tools["mnuApprove"]).SharedProps.Enabled = false;
			((ButtonTool)ultraToolbarsManager1.Tools["mnuReItem"]).SharedProps.Enabled = false;
			((ButtonTool)ultraToolbarsManager1.Tools["mnuReCal"]).SharedProps.Enabled = false;
			((ButtonTool)ultraToolbarsManager1.Tools["mnu_AdjustTot_CNT"]).SharedProps.Enabled = false;
			((ButtonTool)ultraToolbarsManager1.Tools["mnuDelItem_BDGT"]).SharedProps.Enabled = false;
			((ButtonTool)ultraToolbarsManager1.Tools["mnuPrint_CNT"]).SharedProps.Enabled = false;
			((ButtonTool)ultraToolbarsManager1.Tools["mnuFile_Digital"]).SharedProps.Enabled = false;
			((ButtonTool)ultraToolbarsManager1.Tools["mnuImport"]).SharedProps.Enabled = true;
		}
	}

	private void BintToGrid1()
	{
		int iLevel = 0;
		ultraToolbarsManager1.BeginUpdate();
		ultraToolbarsManager1.Enabled = false;
		gridBudget1.Visible = false;
		bool IsThereSummary = false;
		if (!gridBudget1.Cols.Contains("QtyDec"))
		{
			Column C_QtyDec = gridBudget1.Cols.Add();
			C_QtyDec.Name = "QtyDec";
		}
		if (!gridBudget1.Cols.Contains("CostDec"))
		{
			Column C_CostDec = gridBudget1.Cols.Add();
			C_CostDec.Name = "CostDec";
		}
		if (!gridBudget1.Cols.Contains("AmtDec"))
		{
			Column C_AmtDec = gridBudget1.Cols.Add();
			C_AmtDec.Name = "AmtDec";
		}
		RememberColsProps();
		CellStyle CS1 = gridBudget1.Styles.Add("AnalysisColor");
		CellStyle CS9 = gridBudget1.Styles.Add("IsSharedColor");
		CellStyle CS10 = gridBudget1.Styles.Add("MainColor");
		CS1.ForeColor = Color.Red;
		CS10.ForeColor = Color.Blue;
		CS9.ForeColor = Color.Green;
		gridBudget1.Clear(ClearFlags.All);
		ultraStatusBar1.Panels[0].Text = "資料筆數：" + DT1.Rows.Count;
		int iRows = DT1.Rows.Count + 1;
		gridBudget1.Rows.Count = iRows;
		gridBudget1.Select(0, 0);
		SetGridColumn();
		string sKind = "";
		for (int i = 0; i < DT1.Rows.Count; i++)
		{
			CellRange RAccMode = gridBudget1.GetCellRange(i + 1, gridBudget1.Cols["AccMode"].SafeIndex, i + 1, gridBudget1.Cols["AccMode"].SafeIndex);
			sKind = ((DT1.Rows[i]["kind"].ToString().Length > 0) ? DT1.Rows[i]["kind"].ToString().ToUpper().Trim() : "");
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
			RAccMode.Style = gridBudget1.Styles["ComboList"];
			if (DT1.Rows[i]["analysis"].ToString().Trim() == "1")
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
			if (DT1.Rows[i]["AccMode"] != null)
			{
				if (DT1.Rows[i]["AccMode"].ToString() == "0")
				{
					gridBudget1[i + 1, "AccMode"] = "警告但可存檔";
				}
				else if (DT1.Rows[i]["AccMode"].ToString() == "1")
				{
					gridBudget1[i + 1, "AccMode"] = "警告且不可存檔";
				}
				else if (DT1.Rows[i]["AccMode"].ToString() == "2")
				{
					gridBudget1[i + 1, "AccMode"] = "略過";
				}
			}
			gridBudget1[i + 1, "ItemNo"] = DT1.Rows[i]["ItemNo"];
			gridBudget1[i + 1, "CName"] = DT1.Rows[i]["cName"];
			gridBudget1[i + 1, "UnitName"] = DT1.Rows[i]["unitName"];
			gridBudget1[i + 1, "Qty"] = DT1.Rows[i]["qty"];
			gridBudget1[i + 1, "Cost"] = DT1.Rows[i]["cost"];
			gridBudget1[i + 1, "Amount"] = DT1.Rows[i]["amount"];
			gridBudget1[i + 1, "Lock"] = DT1.Rows[i]["LockCost"].ToString().Trim() == "1";
			gridBudget1[i + 1, "Memo"] = DT1.Rows[i]["memo"].ToString();
			gridBudget1[i + 1, "EName"] = DT1.Rows[i]["eName"].ToString();
			gridBudget1[i + 1, "EUnit"] = DT1.Rows[i]["eUnit"].ToString();
			gridBudget1[i + 1, "LevelNo"] = DT1.Rows[i]["levelNo"];
			gridBudget1[i + 1, "SNo"] = DT1.Rows[i]["sno"];
			gridBudget1[i + 1, "Kind"] = DT1.Rows[i]["kind"];
			gridBudget1[i + 1, "PrintNo"] = DT1.Rows[i]["printNo"].ToString().Trim();
			gridBudget1[i + 1, "Formula"] = DT1.Rows[i]["Formula"];
			gridBudget1[i + 1, "PubCode"] = DT1.Rows[i]["pubCode"];
			gridBudget1[i + 1, "PccesCode"] = ((sKind != "W") ? "" : DT1.Rows[i]["PccesCode"]);
			gridBudget1[i + 1, "QtyDec"] = DT1.Rows[i]["QtyDec"];
			gridBudget1[i + 1, "CostDec"] = DT1.Rows[i]["CostDec"];
			gridBudget1[i + 1, "AmtDec"] = DT1.Rows[i]["AmtDec"];
			int CostDec = ArchConvert.Obj2Int(DT1.Rows[i]["CostDec"]);
			int AmtDec = ArchConvert.Obj2Int(DT1.Rows[i]["AmtDec"]);
			if (F_MainCst != CostDec && DT1.Rows[i]["CostDec"] != DBNull.Value)
			{
				if (!gridBudget1.Styles.Contains("CostDecStyle" + CostDec))
				{
					CellStyle CostDecStyle = gridBudget1.Styles.Add("CostDecStyle" + CostDec);
					if (CostDec > 0)
					{
						CostDecStyle.Format = "###,###,###,##0." + "0".PadLeft(CostDec, '0');
					}
					else
					{
						CostDecStyle.Format = "###,###,###,##0";
					}
				}
				gridBudget1.SetCellStyle(i + 1, gridBudget1.Cols["Cost"].SafeIndex, gridBudget1.Styles["CostDecStyle" + CostDec]);
			}
			if (F_MainAmt != AmtDec && DT1.Rows[i]["AmtDec"] != DBNull.Value)
			{
				if (!gridBudget1.Styles.Contains("AmtDec" + CostDec))
				{
					CellStyle AmyDecStyle = gridBudget1.Styles.Add("AmtDec" + AmtDec);
					if (AmtDec > 0)
					{
						AmyDecStyle.Format = "###,###,###,##0." + "0".PadLeft(AmtDec, '0');
					}
					else
					{
						AmyDecStyle.Format = "###,###,###,##0";
					}
				}
				gridBudget1.SetCellStyle(i + 1, gridBudget1.Cols["Amount"].SafeIndex, gridBudget1.Styles["AmtDec" + AmtDec]);
			}
			if (gridBudget1[i + 1, "Kind"] != null)
			{
				gridBudget1.Rows[i + 1].IsNode = true;
			}
			if (DT1.Rows[i]["PrintNo"].ToString().Trim() == "".PadLeft(32, '9'))
			{
				gridBudget1.Rows[i + 1].Node.Level = 1;
				IsThereSummary = true;
			}
			else if (DT1.Rows[i]["PrintNo"].ToString().Trim().Length == 4 && DT1.Rows[i]["Kind"].ToString().Trim() == "Z")
			{
				gridBudget1.Rows[i + 1].Node.Level = 1;
				IsThereSummary = true;
			}
			else
			{
				gridBudget1.Rows[i + 1].Node.Level = Convert.ToInt32(DT1.Rows[i]["PrintNo"].ToString().Trim().Length / 4);
			}
			if (gridBudget1.Rows[i + 1].Node.Level > iLevel)
			{
				iLevel = gridBudget1.Rows[i + 1].Node.Level;
			}
			gridBudget1[i + 1, "IsShared"] = DT1.Rows[i]["share"];
			if (DT1.Rows[i]["share"] != null && DT1.Rows[i]["share"].ToString().Trim() == "1")
			{
				gridBudget1.Rows[i + 1].Style = gridBudget1.Styles["IsSharedColor"];
			}
		}
		SwitchToCorrectLevelStatus(iLevel);
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(subctr) 契約書明細");
		sub_Ctr ctrcom = new sub_Ctr(tmp_AL1);
		if (!IsThereSummary && DT1.Rows.Count >= 1)
		{
			gridBudget1.Rows.Count = gridBudget1.Rows.Count + 1;
			gridBudget1.Rows[DT1.Rows.Count + 1].IsNode = true;
			gridBudget1.Rows[DT1.Rows.Count + 1].Node.Level = 1;
			gridBudget1[DT1.Rows.Count + 1, "CName"] = "總價(總計)";
			gridBudget1[DT1.Rows.Count + 1, "SNo"] = "999999";
			gridBudget1[DT1.Rows.Count + 1, "Kind"] = "Z";
			gridBudget1[DT1.Rows.Count + 1, "LevelNo"] = "1";
			gridBudget1[DT1.Rows.Count + 1, "PrintNo"] = "".PadLeft(32, '9');
			gridBudget1[DT1.Rows.Count + 1, "ItemNo"] = "";
			gridBudget1[DT1.Rows.Count + 1, "UnitName"] = "";
			gridBudget1[DT1.Rows.Count + 1, "Qty"] = "0";
			gridBudget1[DT1.Rows.Count + 1, "Cost"] = "0";
			gridBudget1[DT1.Rows.Count + 1, "Amount"] = "0";
			gridBudget1[DT1.Rows.Count + 1, "memo"] = "";
			gridBudget1[DT1.Rows.Count + 1, "EName"] = "";
			gridBudget1[DT1.Rows.Count + 1, "EUnit"] = "";
			gridBudget1[DT1.Rows.Count + 1, "LevelNo"] = "1";
			gridBudget1[DT1.Rows.Count + 1, "Formula"] = "";
			gridBudget1[DT1.Rows.Count + 1, "PubCode"] = "0";
			gridBudget1[DT1.Rows.Count + 1, "Lock"] = false;
			ctrcom.ps_prjcode = F_ProjectCode;
			ctrcom.ps_subcode = F_SubProjetCode;
			ctrcom.ps_sno = "999999";
			ctrcom.ps_cname = "總價(總計)";
			ctrcom.ps_kind = "Z";
			ctrcom.ps_printno = "".PadLeft(32, '9');
			ctrcom.ps_itemno = "";
			ctrcom.ps_pubcode = "";
			ctrcom.ps_levelno = "1";
			ctrcom.ps_ename = "";
			ctrcom.ps_unitname = "";
			ctrcom.ps_cost = "0";
			ctrcom.ps_qty = "0";
			ctrcom.ps_amount = "0";
			ctrcom.ps_memo = "";
			ctrcom.ps_setdecimal = "0";
			ctrcom.ps_lrate = "0";
			ctrcom.ps_erate = "0";
			ctrcom.ps_mrate = "0";
			ctrcom.ps_wrate = "0";
			ctrcom.ps_eunit = "";
			ctrcom.ps_rate = "0";
			ctrcom.ps_share = "";
			ctrcom.ps_dsctlock = "";
			ctrcom.ps_Formula = "";
			ctrcom.InseItem();
			DT1 = ctrcom.ListItem("", F_SubProjetCode, F_ProjectCode, ArrDecimal);
		}
		lblTotal.Text = string.Format("{0:N" + F_MainAmt + "}", ctrcom.GetAmount(F_SubProjetCode, F_ProjectCode));
		F_OldTotalAmount = ctrcom.GetOldAmount(F_SubProjetCode, F_ProjectCode);
		SetColsEditSymbol(ref gridBudget1);
		gridBudget1.Visible = true;
		gridBudget1.Invalidate();
		ultraToolbarsManager1.Enabled = true;
		ultraToolbarsManager1.EndUpdate();
		if (gridBudget1.Rows.Count <= 1)
		{
			ultraToolbarsManager1.Tools["mnuApprove"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuReCal"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuReItem"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuGetCost"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuImport"].SharedProps.Enabled = true;
		}
		else if (!HasApproved)
		{
			ultraToolbarsManager1.Tools["mnuApprove"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuReCal"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuReItem"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuGetCost"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuImport"].SharedProps.Enabled = true;
		}
		gridBudget1.Cols["QtyDec"].Visible = false;
		gridBudget1.Cols["CostDec"].Visible = false;
		gridBudget1.Cols["AmtDec"].Visible = false;
	}

	private void SetColsEditSymbol(ref GridBudget g1)
	{
		for (int i = 1; i < g1.Cols.Count; i++)
		{
			if (g1.Cols[i].AllowEditing)
			{
				CellRange rg = g1.GetCellRange(0, i);
				rg.Style = gridBudget1.Styles["EditMode"];
				rg.Image = imageList2.Images[1];
			}
		}
	}

	private void Do_ToolBarFind1()
	{
		if (gridBudget1.Rows.Count <= 1)
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

	private void ultraToolbarsManager1_ToolKeyPress(object sender, ToolKeyPressEventArgs e)
	{
		if (e.KeyChar == '\r' && e.Tool.Key == "mnu_Cbo1")
		{
			Do_ToolBarFind1();
		}
	}

	private void ultraToolbarsManager1_AfterToolActivate(object sender, ToolEventArgs e)
	{
		if (e.Tool.Key == "mnu_Cbo1")
		{
			((ButtonTool)ultraToolbarsManager1.Tools["mnuDeleteIssue"]).SharedProps.Shortcut = Shortcut.None;
		}
		else
		{
			((ButtonTool)ultraToolbarsManager1.Tools["mnuDeleteIssue"]).SharedProps.Shortcut = Shortcut.Del;
		}
	}

	private void ultraToolbarsManager1_AfterToolDeactivate(object sender, ToolEventArgs e)
	{
		((ButtonTool)ultraToolbarsManager1.Tools["mnuDeleteIssue"]).SharedProps.Shortcut = Shortcut.Del;
	}

	private void ultraToolbarsManager1_BeforeToolbarListDropdown(object sender, BeforeToolbarListDropdownEventArgs e)
	{
		e.Cancel = true;
	}

	private void BtnSwitchProject_Click(object sender, EventArgs e)
	{
		if (!DBClass.ChkAuthority(F_UserID, "F00900010001"))
		{
			MessageBox.Show(this, DBClass.GetFuncName("F00900010001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
			Tab_A.Tab.Selected = true;
			LoadContract();
			BintToGrid1();
		}
		Cursor = Cursors.Default;
	}

	private void gridBudget1_AfterEdit(object sender, RowColEventArgs e)
	{
		if (gridBudget1.Cols[e.Col].Name == "Lock")
		{
			ultraToolbarsManager1.BeginUpdate();
			FormSys_G_Info1 FM_INFO = new FormSys_G_Info1();
			FM_INFO._InfoString = "項目鎖定處理中，請稍候! ";
			FM_INFO.Show();
			Application.DoEvents();
			Cursor = Cursors.WaitCursor;
			gridBudget1.Enabled = false;
			Application.DoEvents();
			string IPStr = CommonMethods.GetIPAddress();
			ArrayList aArr = new ArrayList();
			aArr.Clear();
			aArr.Add(F_UserID);
			aArr.Add("預算書項目編輯後存檔之鎖定異動--" + F_ProjectCode + "(" + IPStr + ")");
			string sLockCheck = (((bool)gridBudget1[e.Row, "Lock"]) ? "1" : "0");
			Archnowledge.Pcces.BUDClass.ItemA ItemACom = new Archnowledge.Pcces.BUDClass.ItemA(aArr);
			ItemACom.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
			ItemACom.ps_projectCode = F_ProjectCode;
			ItemACom.LockCost(F_ProjectCode, gridBudget1[e.Row, "PrintNo"].ToString().Trim(), sLockCheck, "LockCost");
			LoadContract();
			FM_INFO.Close();
			FM_INFO.Dispose();
			gridBudget1.Enabled = true;
			ultraToolbarsManager1.Enabled = true;
			ultraToolbarsManager1.EndUpdate();
			Cursor = Cursors.Default;
			return;
		}
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(subctr) 契約書明細");
		if (dbItemA != null)
		{
			dbItemA = null;
		}
		if (dbItemA == null)
		{
			dbItemA = new Archnowledge.Pcces.BUDClass.ItemA(tmp_AL1);
		}
		dbItemA.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		dbItemA.ps_projectCode = F_ProjectCode;
		dbItemA.ps_sNo = gridBudget1[e.Row, "SNo"].ToString();
		dbItemA.ps_QtyDec = ((gridBudget1[e.Row, "QtyDec"].ToString().Trim() == "") ? null : gridBudget1[e.Row, "QtyDec"].ToString().Trim());
		dbItemA.ps_CstDec = ((gridBudget1[e.Row, "CostDec"].ToString().Trim() == "") ? null : gridBudget1[e.Row, "CostDec"].ToString().Trim());
		dbItemA.ps_AmtDec = ((gridBudget1[e.Row, "AmtDec"].ToString().Trim() == "") ? null : gridBudget1[e.Row, "AmtDec"].ToString().Trim());
		if (gridBudget1.Cols[e.Col].Name == "Qty")
		{
			dbItemA.ps_qty = gridBudget1[e.Row, e.Col].ToString();
			dbItemA.UpdItem();
			DT1.Rows[e.Row - 1]["Qty"] = dbItemA.ps_qty;
		}
		if (gridBudget1.Cols[e.Col].Name == "Lock")
		{
			dbItemA.ps_dsctLock = (((bool)gridBudget1[e.Row, e.Col]) ? "1" : "");
			dbItemA.UpdItem();
			DT1.Rows[e.Row - 1]["DsctLock"] = dbItemA.ps_dsctLock;
		}
		if (gridBudget1.Cols[e.Col].Name == "Cost")
		{
			dbItemA.ps_cost = gridBudget1[e.Row, e.Col].ToString();
			dbItemA.UpdItem();
			DT1.Rows[e.Row - 1]["Cost"] = dbItemA.ps_cost;
		}
		if (gridBudget1.Cols[e.Col].Name == "Memo")
		{
			dbItemA.ps_memo = gridBudget1[e.Row, e.Col].ToString();
			dbItemA.UpdItem();
			DT1.Rows[e.Row - 1]["Memo"] = dbItemA.ps_memo;
		}
		if (gridBudget1.Cols[e.Col].Name == "EName")
		{
			dbItemA.ps_eName = gridBudget1[e.Row, e.Col].ToString();
			dbItemA.UpdItem();
			DT1.Rows[e.Row - 1]["EName"] = dbItemA.ps_eName;
		}
		if (gridBudget1.Cols[e.Col].Name == "AccMode")
		{
			if (gridBudget1[e.Row, "AccMode"].ToString() == "警告但可存檔")
			{
				dbItemA.ps_AccMode = "0";
			}
			else if (gridBudget1[e.Row, "AccMode"].ToString() == "警告且不可存檔")
			{
				dbItemA.ps_AccMode = "1";
			}
			else if (gridBudget1[e.Row, "AccMode"].ToString() == "略過")
			{
				dbItemA.ps_AccMode = "2";
			}
			dbItemA.UpdItem();
		}
		if (gridBudget1[gridBudget1.Row, "Kind"].ToString().Trim().ToUpper() == "W")
		{
			MrsBaseA dbMrsBaseA = new MrsBaseA(F_UserID, tmp_AL1);
			dbMrsBaseA.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
			dbMrsBaseA.ps_projectcode = F_ProjectCode;
			dbMrsBaseA.ps_pccesCode = ((gridBudget1[gridBudget1.Row, "PccesCode"] != null) ? gridBudget1[gridBudget1.Row, "PccesCode"].ToString() : null);
			dbMrsBaseA.ps_cName = ((gridBudget1[gridBudget1.Row, "CName"] != null) ? gridBudget1[gridBudget1.Row, "CName"].ToString() : null);
			dbMrsBaseA.ps_cost = ((gridBudget1[gridBudget1.Row, "Cost"] != null) ? gridBudget1[gridBudget1.Row, "Cost"].ToString() : null);
			dbMrsBaseA.ps_eName = ((gridBudget1[gridBudget1.Row, "EName"] != null) ? gridBudget1[gridBudget1.Row, "EName"].ToString() : null);
			dbMrsBaseA.ps_eUnit = ((gridBudget1[gridBudget1.Row, "EUnit"] != null) ? gridBudget1[gridBudget1.Row, "EUnit"].ToString() : null);
			dbMrsBaseA.ps_memo = ((gridBudget1[gridBudget1.Row, "Memo"] != null) ? gridBudget1[gridBudget1.Row, "Memo"].ToString() : null);
			dbMrsBaseA.ps_rate = null;
			dbMrsBaseA.ps_unitName = ((gridBudget1[gridBudget1.Row, "UnitName"] != null) ? gridBudget1[gridBudget1.Row, "UnitName"].ToString() : null);
			dbMrsBaseA.UpdItem();
		}
	}

	private void SaveCNT()
	{
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(subctr) 契約書明細");
		sub_Ctr ctrcom = new sub_Ctr(tmp_AL1);
		ctrcom.ps_prjcode = F_ProjectCode;
		ctrcom.ps_subcode = F_SubProjetCode;
		for (int i = 0; i < DT1.Rows.Count; i++)
		{
			ctrcom.ps_sno = DT1.Rows[i]["sno"].ToString();
			ctrcom.ps_itemno = DT1.Rows[i]["itemNo"].ToString();
			ctrcom.ps_printno = DT1.Rows[i]["printNo"].ToString().Trim();
			ctrcom.ps_qty = DT1.Rows[i]["Qty"].ToString();
			ctrcom.ps_dsctlock = DT1.Rows[i]["DsctLock"].ToString().Trim();
			ctrcom.ps_cost = DT1.Rows[i]["cost"].ToString();
			ctrcom.ps_amount = DT1.Rows[i]["amount"].ToString();
			ctrcom.ps_memo = DT1.Rows[i]["memo"].ToString();
			ctrcom.ps_ename = DT1.Rows[i]["ename"].ToString();
			ctrcom.ps_eunit = DT1.Rows[i]["eunit"].ToString();
			ctrcom.ps_cname = DT1.Rows[i]["cName"].ToString();
			ctrcom.ps_pubcode = DT1.Rows[i]["pubCode"].ToString();
			ctrcom.ps_levelno = (DT1.Rows[i]["printNo"].ToString().Trim().Length / 4).ToString();
			ctrcom.ps_unitname = DT1.Rows[i]["unitName"].ToString();
			ctrcom.ps_kind = DT1.Rows[i]["kind"].ToString();
			ctrcom.ps_rate = DT1.Rows[i]["rate"].ToString();
			ctrcom.ps_share = DT1.Rows[i]["share"].ToString();
			ctrcom.ps_setdecimal = DT1.Rows[i]["setdecimal"].ToString();
			ctrcom.UpdItem();
		}
	}

	private void gridBudget1_Click(object sender, EventArgs e)
	{
		if (gridBudget1.MouseRow <= 0 || gridBudget1.MouseCol <= 0)
		{
			return;
		}
		int rowIndex = gridBudget1.MouseRow;
		int colIndex = gridBudget1.MouseCol;
		try
		{
			if (gridBudget1.Cols[colIndex].Name.ToUpper() == "AnaImg".ToUpper() && (bool)gridBudget1[rowIndex, "Analysis"] && !HasOpenedBreakdownForm)
			{
				HasOpenedBreakdownForm = true;
				ExecuteBreakdownForm();
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "SplitContract.FormSplitContract.cs" + ex.Message);
			MessageBox.Show(this, "Err13:\n" + ex.Message, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void gridBudget1_MouseMove(object sender, MouseEventArgs e)
	{
		if (gridBudget1.MouseRow <= 0 || gridBudget1.MouseCol <= 0)
		{
			return;
		}
		int rowIndex = gridBudget1.MouseRow;
		int colIndex = gridBudget1.MouseCol;
		if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right && gridBudget1.Cols[colIndex].Name == "AnaImg")
		{
			if (gridBudget1[rowIndex, "Analysis"] != null && rowIndex > 0 && (bool)gridBudget1[rowIndex, "Analysis"])
			{
				Cursor = Cursors.Hand;
			}
		}
		else
		{
			Cursor = Cursors.Default;
		}
	}

	private void gridBudget1_AfterSelChange(object sender, RangeEventArgs e)
	{
		if (gridBudget1.Row < 1)
		{
			ultraToolbarsManager1.Tools["mnuEditMain"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuDelItem_BDGT"].SharedProps.Enabled = false;
			return;
		}
		if (gridBudget1[gridBudget1.Row, "Kind"] != null)
		{
			string sKind = gridBudget1[gridBudget1.Row, "Kind"].ToString().ToUpper().Trim();
			switch (sKind)
			{
			case "L":
				ultraToolbarsManager1.Tools["mnuDetailEdit_SetShare"].SharedProps.Enabled = true;
				ultraToolbarsManager1.Tools["mnuCancelShare"].SharedProps.Enabled = true;
				break;
			default:
				if (!Is_SBID)
				{
					break;
				}
				goto case "B";
			case "B":
			case "Z":
			case "W":
			case "F":
			case "S":
			case "U":
				ultraToolbarsManager1.Tools["mnuDetailEdit_SetShare"].SharedProps.Enabled = false;
				ultraToolbarsManager1.Tools["mnuCancelShare"].SharedProps.Enabled = false;
				break;
			}
			if (sKind == "W")
			{
				ultraToolbarsManager1.Tools["mnuDelItem_BDGT"].SharedProps.Enabled = true;
				ultraToolbarsManager1.Tools["mnuEditMain"].SharedProps.Enabled = false;
			}
			else
			{
				ultraToolbarsManager1.Tools["mnuDelItem_BDGT"].SharedProps.Enabled = false;
				ultraToolbarsManager1.Tools["mnuEditMain"].SharedProps.Enabled = true;
			}
		}
		if (gridBudget1[gridBudget1.Row, "IsShared"] != null)
		{
			if (gridBudget1[gridBudget1.Row, "IsShared"].ToString().Trim() == "1")
			{
				ultraToolbarsManager1.Tools["mnuDetailEdit_SetShare"].SharedProps.Enabled = false;
				ultraToolbarsManager1.Tools["mnuCancelShare"].SharedProps.Enabled = true;
			}
			else
			{
				ultraToolbarsManager1.Tools["mnuCancelShare"].SharedProps.Enabled = false;
			}
		}
		else
		{
			ultraToolbarsManager1.Tools["mnuCancelShare"].SharedProps.Enabled = false;
		}
		if (HasApproved)
		{
			ultraToolbarsManager1.Tools["mnuEditMain"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuDelItem_BDGT"].SharedProps.Enabled = false;
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

	private void gridBudget1_Resize(object sender, EventArgs e)
	{
		FormSplitContract_Resize(sender, e);
	}
}
