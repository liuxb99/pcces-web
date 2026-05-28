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
using Archnowledge.Pcces.PccesMain.Report;
using Archnowledge.Pcces.PccesMain.SplitContract;
using Archnowledge.Pcces.STDClass;
using Aspose.Cells;
using AxThreed;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinStatusBar;
using Infragistics.Win.UltraWinToolbars;

namespace Archnowledge.Pcces.PccesMain.Invoice;

public class FormInvoice : Form
{
	private IContainer components;

	private UltraToolbarsManager ultraToolbarsManager1;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Top;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Bottom;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Left;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Right;

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

	private Panel panel4;

	private GridBudget gridBudget2;

	private Panel panel6;

	private UltraLabel ultraLabel7;

	private UltraStatusBar ultraStatusBar3;

	private Panel PNL_INV;

	private GridBudget Grid1;

	private Panel panel5;

	private UltraLabel ultraLabel5;

	private UltraLabel ultraLabel6;

	private UltraStatusBar ultraStatusBar2;

	private Panel panel3;

	private UltraLabel lblProjectData2;

	private UltraLabel ultraLabel3;

	private UltraButton ultraButton1;

	private UltraLabel ultraLabel4;

	private Panel panel2;

	private UltraLabel ultraLabel2;

	private UltraLabel lblThisIssue;

	private ImageList iglst_splt_Btn;

	private ImageList imageList2;

	private Splitter splitter1;

	private UltraButton ultraButton2;

	private OpenFileDialog openFileDialog1;

	private Panel panel7;

	private UltraLabel lblTotal;

	private UltraLabel ultraLabel1;

	private AxSSPanel axSSPanel2;

	private DataTable DT1 = new DataTable();

	private string FORM_STATUS = "INI";

	private PccesFormAction F_ActionName = PccesFormAction.Invoice;

	private int iAccMode = 0;

	private bool F_HasRegistered;

	private string F_ProjectCode;

	private string F_ProjectNameC;

	private string F_SubProjetCode = "";

	private DataTable DT2_1 = new DataTable();

	private DataTable DT2_2 = new DataTable();

	private object[,] GridColsSquence2;

	private ArrayList ArrDecimal = new ArrayList();

	private bool ReCflag = false;

	private int iCount = 0;

	private int iCountNum = 0;

	private string Firstflag = "";

	private DataTable DTEdit = new DataTable();

	private string F_UserID;

	private string F_UserName = "";

	private string F_FunctionName = "Invoice";

	private string F_ServerName = "localhost";

	private int F_MainQty = 3;

	private int F_MainCst = 0;

	private int F_MainAmt = 0;

	private int F_AnaQty = 3;

	private int F_AnaCst = 2;

	private int F_AnaAmt = 2;

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Invoice.FormInvoice));
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel3 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel4 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance38 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance39 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance40 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.OptionSet optionSet1 = new Infragistics.Win.UltraWinToolbars.OptionSet("switch");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Tool2");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuPrint");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuFile_Digital");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuNewIssue");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDeleteIssue");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool5 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuEditIssue");
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
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool2 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnuFind2");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool1 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo2");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool7 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Go2");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar2 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Menu1");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool1 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuFile");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool2 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuEdit");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool3 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuView");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool4 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuTool");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool5 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuHelp");
		Infragistics.Win.Appearance appearance41 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance42 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool8 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelete");
		Infragistics.Win.Appearance appearance43 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool6 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Popup1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool9 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuNewIssue");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool10 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDeleteIssue");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool11 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuEditIssue");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool12 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuThisProgress");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool13 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuNewIssue");
		Infragistics.Win.Appearance appearance44 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool14 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuEditIssue");
		Infragistics.Win.Appearance appearance45 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool15 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDeleteIssue");
		Infragistics.Win.Appearance appearance46 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool2 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo2");
		Infragistics.Win.ValueList valueList1 = new Infragistics.Win.ValueList(0);
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool16 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Go2");
		Infragistics.Win.Appearance appearance47 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool7 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuFile");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool17 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuNewIssue");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool18 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDeleteIssue");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool19 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuFile_Digital");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool20 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuPrint");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool21 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuImport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool22 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool23 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuImportDaily");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool24 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExit");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool8 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuEdit");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool25 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuThisProgress");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool26 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuBasic");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool9 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuView");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool27 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuShowList");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool28 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSummaryList");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool29 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuEditIssue");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool30 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuStatictics");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool10 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuTool");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool31 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuReCal");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool32 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuThisProgress");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool33 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuBasic");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool34 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSummaryList");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool35 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuStatictics");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool36 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuReCal");
		Infragistics.Win.Appearance appearance48 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool3 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnuFind2");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool37 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExit");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool38 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuPrint");
		Infragistics.Win.Appearance appearance49 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool39 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuThisTotal");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool40 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuShowList");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool11 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuHelp");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool41 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuAbout");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool42 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuUpdateList");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool43 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuAbout");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool44 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool45 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuImport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool46 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuUpdateList");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool47 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuImportDaily");
		Infragistics.Win.Appearance appearance50 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool48 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuFile_Digital");
		Infragistics.Win.Appearance appearance51 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool4 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnuLevel");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool9 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_1", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool10 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_2", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool11 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_3", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool12 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_4", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool13 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_5", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool14 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_6", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool15 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_7", "switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool16 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLevel_8", "switch");
		Infragistics.Win.Appearance appearance52 = new Infragistics.Win.Appearance();
		this.panel4 = new System.Windows.Forms.Panel();
		this.gridBudget2 = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.panel7 = new System.Windows.Forms.Panel();
		this.lblTotal = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.axSSPanel2 = new AxThreed.AxSSPanel();
		this.panel6 = new System.Windows.Forms.Panel();
		this.lblThisIssue = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraStatusBar3 = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.PNL_INV = new System.Windows.Forms.Panel();
		this.Grid1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.panel5 = new System.Windows.Forms.Panel();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraButton2 = new Infragistics.Win.Misc.UltraButton();
		this.ultraStatusBar2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.panel3 = new System.Windows.Forms.Panel();
		this.lblProjectData2 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraToolbarsManager1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
		this._FormSys_B_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
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
		this.panel2 = new System.Windows.Forms.Panel();
		this.splitter1 = new System.Windows.Forms.Splitter();
		this.iglst_splt_Btn = new System.Windows.Forms.ImageList(this.components);
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
		this.panel4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridBudget2).BeginInit();
		this.panel7.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.axSSPanel2).BeginInit();
		this.panel6.SuspendLayout();
		this.PNL_INV.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Grid1).BeginInit();
		this.panel5.SuspendLayout();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).BeginInit();
		this.LeftPanel.SuspendLayout();
		this.pnl_spliter.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ssp_Lower).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Bottom).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Upper).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Top).BeginInit();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		base.SuspendLayout();
		this.panel4.Controls.Add(this.gridBudget2);
		this.panel4.Controls.Add(this.panel7);
		this.panel4.Controls.Add(this.panel6);
		this.panel4.Controls.Add(this.ultraStatusBar3);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel4.Location = new System.Drawing.Point(0, 152);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(641, 315);
		this.panel4.TabIndex = 12;
		this.gridBudget2._ExcelFileName = "";
		this.gridBudget2._ExcelSheeName = "";
		this.gridBudget2._IsOpenExcelAfterExport = false;
		this.gridBudget2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridBudget2.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
		this.gridBudget2.ColumnInfo = resources.GetString("gridBudget2.ColumnInfo");
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
		this.gridBudget2.Size = new System.Drawing.Size(641, 231);
		this.gridBudget2.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridBudget2.Styles"));
		this.gridBudget2.TabIndex = 8;
		this.gridBudget2.Tree.Column = 1;
		this.gridBudget2.Tree.LineColor = System.Drawing.Color.Gray;
		this.gridBudget2.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(gridBudget2_AfterRowColChange);
		this.gridBudget2.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(gridBudget2_StartEdit);
		this.gridBudget2.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(gridBudget2_AfterEdit);
		this.gridBudget2.Resize += new System.EventHandler(gridBudget2_Resize);
		this.gridBudget2.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(gridBudget2_BeforeEdit);
		this.panel7.Controls.Add(this.lblTotal);
		this.panel7.Controls.Add(this.ultraLabel1);
		this.panel7.Controls.Add(this.axSSPanel2);
		this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel7.Location = new System.Drawing.Point(0, 261);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(641, 28);
		this.panel7.TabIndex = 10;
		this.lblTotal.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance1.ForeColor = System.Drawing.Color.Blue;
		appearance1.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance1.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblTotal.Appearance = appearance1;
		this.lblTotal.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.lblTotal.Font = new System.Drawing.Font("Courier New", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblTotal.Location = new System.Drawing.Point(64, 5);
		this.lblTotal.Name = "lblTotal";
		this.lblTotal.Size = new System.Drawing.Size(486, 19);
		this.lblTotal.TabIndex = 14;
		appearance2.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		appearance2.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel1.Appearance = appearance2;
		this.ultraLabel1.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.ultraLabel1.Font = new System.Drawing.Font("Courier New", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.ultraLabel1.Location = new System.Drawing.Point(4, 5);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(74, 19);
		this.ultraLabel1.TabIndex = 13;
		this.ultraLabel1.Text = "總計：";
		this.axSSPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.axSSPanel2.Location = new System.Drawing.Point(0, 0);
		this.axSSPanel2.Name = "axSSPanel2";
		this.axSSPanel2.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("axSSPanel2.OcxState");
		this.axSSPanel2.Size = new System.Drawing.Size(641, 28);
		this.axSSPanel2.TabIndex = 1;
		this.panel6.Controls.Add(this.lblThisIssue);
		this.panel6.Controls.Add(this.ultraLabel2);
		this.panel6.Controls.Add(this.ultraLabel7);
		this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel6.Location = new System.Drawing.Point(0, 0);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(641, 30);
		this.panel6.TabIndex = 9;
		appearance3.ForeColor = System.Drawing.Color.White;
		appearance3.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblThisIssue.Appearance = appearance3;
		this.lblThisIssue.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.lblThisIssue.Font = new System.Drawing.Font("細明體", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblThisIssue.Location = new System.Drawing.Point(66, 7);
		this.lblThisIssue.Name = "lblThisIssue";
		this.lblThisIssue.Size = new System.Drawing.Size(224, 19);
		this.lblThisIssue.TabIndex = 17;
		this.lblThisIssue.Text = "【目前編輯期別：】";
		appearance4.ForeColor = System.Drawing.Color.White;
		appearance4.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel2.Appearance = appearance4;
		this.ultraLabel2.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel2.Font = new System.Drawing.Font("細明體", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel2.Location = new System.Drawing.Point(10, 7);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(66, 19);
		this.ultraLabel2.TabIndex = 16;
		this.ultraLabel2.Text = "合約明細";
		this.ultraLabel7.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel7.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.ultraLabel7.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ultraLabel7.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(641, 30);
		this.ultraLabel7.TabIndex = 0;
		appearance5.FontData.SizeInPoints = 11f;
		appearance5.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraStatusBar3.Appearance = appearance5;
		this.ultraStatusBar3.Location = new System.Drawing.Point(0, 289);
		this.ultraStatusBar3.Name = "ultraStatusBar3";
		appearance6.TextVAlign = Infragistics.Win.VAlign.Bottom;
		ultraStatusPanel1.Appearance = appearance6;
		ultraStatusPanel1.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel1.Key = "RowsCount";
		ultraStatusPanel1.Text = "資料筆數：";
		ultraStatusPanel1.Width = 200;
		ultraStatusPanel2.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel2.Key = "ProgressBar";
		ultraStatusPanel2.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		this.ultraStatusBar3.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[2] { ultraStatusPanel1, ultraStatusPanel2 });
		this.ultraStatusBar3.Size = new System.Drawing.Size(641, 26);
		this.ultraStatusBar3.TabIndex = 6;
		this.ultraStatusBar3.Text = "ultraStatusBar3";
		this.PNL_INV.Controls.Add(this.Grid1);
		this.PNL_INV.Controls.Add(this.panel5);
		this.PNL_INV.Controls.Add(this.ultraStatusBar2);
		this.PNL_INV.Dock = System.Windows.Forms.DockStyle.Top;
		this.PNL_INV.Location = new System.Drawing.Point(0, 0);
		this.PNL_INV.Name = "PNL_INV";
		this.PNL_INV.Size = new System.Drawing.Size(641, 144);
		this.PNL_INV.TabIndex = 3;
		this.Grid1._ExcelFileName = "";
		this.Grid1._ExcelSheeName = "";
		this.Grid1._IsOpenExcelAfterExport = false;
		this.Grid1.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.Rows;
		this.Grid1.AllowEditing = false;
		this.Grid1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.Grid1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
		this.Grid1.ColumnInfo = resources.GetString("Grid1.ColumnInfo");
		this.ultraToolbarsManager1.SetContextMenuUltra(this.Grid1, "Popup1");
		this.Grid1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Grid1.ExtendLastCol = true;
		this.Grid1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.Grid1.ForeColor = System.Drawing.Color.Black;
		this.Grid1.Location = new System.Drawing.Point(0, 30);
		this.Grid1.Name = "Grid1";
		this.Grid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
		this.Grid1.ShowCursor = true;
		this.Grid1.ShowSort = false;
		this.Grid1.ShowToolTipOnNarrowColumn = true;
		this.Grid1.Size = new System.Drawing.Size(641, 88);
		this.Grid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("Grid1.Styles"));
		this.Grid1.TabIndex = 1;
		this.Grid1.Tree.Column = 1;
		this.Grid1.Tree.LineColor = System.Drawing.Color.Gray;
		this.Grid1.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(Grid1_AfterRowColChange);
		this.Grid1.MouseDown += new System.Windows.Forms.MouseEventHandler(Grid1_MouseDown);
		this.panel5.Controls.Add(this.ultraLabel5);
		this.panel5.Controls.Add(this.ultraLabel6);
		this.panel5.Controls.Add(this.ultraButton2);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel5.Location = new System.Drawing.Point(0, 0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(641, 30);
		this.panel5.TabIndex = 6;
		appearance28.ForeColor = System.Drawing.Color.White;
		appearance28.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel5.Appearance = appearance28;
		this.ultraLabel5.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel5.Font = new System.Drawing.Font("細明體", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel5.Location = new System.Drawing.Point(10, 7);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(198, 19);
		this.ultraLabel5.TabIndex = 14;
		this.ultraLabel5.Text = "計價期別一覽表";
		this.ultraLabel6.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel6.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.ultraLabel6.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ultraLabel6.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(621, 30);
		this.ultraLabel6.TabIndex = 0;
		appearance29.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance29.ForeColor = System.Drawing.Color.White;
		this.ultraButton2.Appearance = appearance29;
		this.ultraButton2.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraButton2.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.ultraButton2.Dock = System.Windows.Forms.DockStyle.Right;
		this.ultraButton2.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ultraButton2.Location = new System.Drawing.Point(621, 0);
		this.ultraButton2.Name = "ultraButton2";
		this.ultraButton2.ShowFocusRect = false;
		this.ultraButton2.ShowOutline = false;
		this.ultraButton2.Size = new System.Drawing.Size(20, 30);
		this.ultraButton2.SupportThemes = false;
		this.ultraButton2.TabIndex = 15;
		this.ultraButton2.Text = "X";
		this.ultraButton2.Click += new System.EventHandler(ultraButton2_Click);
		appearance30.FontData.SizeInPoints = 11f;
		appearance30.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraStatusBar2.Appearance = appearance30;
		this.ultraStatusBar2.Location = new System.Drawing.Point(0, 118);
		this.ultraStatusBar2.Name = "ultraStatusBar2";
		appearance31.TextVAlign = Infragistics.Win.VAlign.Bottom;
		ultraStatusPanel3.Appearance = appearance31;
		ultraStatusPanel3.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel3.Key = "RowsCount";
		ultraStatusPanel3.Text = "資料筆數：";
		ultraStatusPanel3.Width = 200;
		ultraStatusPanel4.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel4.Key = "ProgressBar";
		ultraStatusPanel4.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		this.ultraStatusBar2.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[2] { ultraStatusPanel3, ultraStatusPanel4 });
		this.ultraStatusBar2.Size = new System.Drawing.Size(641, 26);
		this.ultraStatusBar2.TabIndex = 5;
		this.ultraStatusBar2.Text = "ultraStatusBar2";
		this.ultraStatusBar2.Visible = false;
		this.panel3.Controls.Add(this.lblProjectData2);
		this.panel3.Controls.Add(this.ultraLabel3);
		this.panel3.Controls.Add(this.ultraButton1);
		this.panel3.Controls.Add(this.ultraLabel4);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel3.Location = new System.Drawing.Point(0, 0);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(641, 30);
		this.panel3.TabIndex = 2;
		this.lblProjectData2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance32.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance32.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblProjectData2.Appearance = appearance32;
		this.lblProjectData2.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.lblProjectData2.Font = new System.Drawing.Font("細明體", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblProjectData2.Location = new System.Drawing.Point(80, 5);
		this.lblProjectData2.Name = "lblProjectData2";
		this.lblProjectData2.Size = new System.Drawing.Size(444, 20);
		this.lblProjectData2.TabIndex = 15;
		appearance33.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel3.Appearance = appearance33;
		this.ultraLabel3.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.ultraLabel3.Font = new System.Drawing.Font("細明體", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel3.Location = new System.Drawing.Point(10, 7);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(74, 19);
		this.ultraLabel3.TabIndex = 14;
		this.ultraLabel3.Text = "目前專案：";
		this.ultraButton1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance34.BackColor = System.Drawing.Color.Silver;
		appearance34.BackColor2 = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance34.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		appearance34.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraButton1.Appearance = appearance34;
		this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.ultraButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ultraButton1.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance35.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance35.BackColor2 = System.Drawing.Color.White;
		appearance35.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.ultraButton1.HotTrackAppearance = appearance35;
		this.ultraButton1.HotTracking = true;
		this.ultraButton1.Location = new System.Drawing.Point(546, 4);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.Size = new System.Drawing.Size(92, 24);
		this.ultraButton1.TabIndex = 12;
		this.ultraButton1.Text = "切換專案";
		this.ultraButton1.Click += new System.EventHandler(ultraButton1_Click);
		this.ultraLabel4.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.ultraLabel4.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.ultraLabel4.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ultraLabel4.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(641, 30);
		this.ultraLabel4.TabIndex = 0;
		appearance36.FontData.Name = "Arial";
		appearance36.FontData.SizeInPoints = 9f;
		this.ultraToolbarsManager1.Appearance = appearance36;
		appearance37.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance37.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.DockAreaAppearance = appearance37;
		this.ultraToolbarsManager1.DockWithinContainer = this;
		this.ultraToolbarsManager1.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraToolbarsManager1.LockToolbars = true;
		appearance38.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance38.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance38.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.MenuSettings.HotTrackAppearance = appearance38;
		appearance39.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance39.BackColor2 = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraToolbarsManager1.MenuSettings.IconAreaAppearance = appearance39;
		appearance40.BackColor = System.Drawing.Color.White;
		appearance40.BackColor2 = System.Drawing.Color.White;
		this.ultraToolbarsManager1.MenuSettings.ToolAppearance = appearance40;
		optionSet1.AllowAllUp = false;
		this.ultraToolbarsManager1.OptionSets.Add(optionSet1);
		this.ultraToolbarsManager1.ShowFullMenusDelay = 500;
		this.ultraToolbarsManager1.ShowQuickCustomizeButton = false;
		this.ultraToolbarsManager1.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2003;
		ultraToolbar1.DockedColumn = 0;
		ultraToolbar1.DockedRow = 1;
		ultraToolbar1.Settings.AllowCustomize = Infragistics.Win.DefaultableBoolean.True;
		ultraToolbar1.Text = "Tool2";
		buttonTool3.InstanceProps.IsFirstInGroup = true;
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
		ultraToolbar2.DockedRow = 0;
		ultraToolbar2.IsMainMenuBar = true;
		ultraToolbar2.Text = "Menu1";
		ultraToolbar2.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[5] { popupMenuTool1, popupMenuTool2, popupMenuTool3, popupMenuTool4, popupMenuTool5 });
		this.ultraToolbarsManager1.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[2] { ultraToolbar1, ultraToolbar2 });
		appearance41.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance41.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.ToolbarSettings.Appearance = appearance41;
		appearance42.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance42.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance42.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.ToolbarSettings.HotTrackAppearance = appearance42;
		appearance43.Image = resources.GetObject("appearance19.Image");
		buttonTool8.SharedProps.AppearancesSmall.Appearance = appearance43;
		buttonTool8.SharedProps.Caption = "刪除";
		buttonTool8.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		buttonTool8.SharedProps.Shortcut = System.Windows.Forms.Shortcut.Del;
		popupMenuTool6.SharedProps.Caption = "右鍵功能表";
		buttonTool10.InstanceProps.IsFirstInGroup = true;
		buttonTool11.InstanceProps.IsFirstInGroup = true;
		buttonTool12.InstanceProps.IsFirstInGroup = true;
		popupMenuTool6.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[4] { buttonTool9, buttonTool10, buttonTool11, buttonTool12 });
		appearance44.Image = resources.GetObject("appearance20.Image");
		buttonTool13.SharedProps.AppearancesSmall.Appearance = appearance44;
		buttonTool13.SharedProps.Caption = "新增期別";
		buttonTool13.SharedProps.Category = "計價";
		buttonTool13.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		appearance45.Image = resources.GetObject("appearance21.Image");
		buttonTool14.SharedProps.AppearancesSmall.Appearance = appearance45;
		buttonTool14.SharedProps.Caption = "查閱本期總計";
		buttonTool14.SharedProps.Category = "計價";
		buttonTool14.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		appearance46.Image = resources.GetObject("appearance22.Image");
		buttonTool15.SharedProps.AppearancesSmall.Appearance = appearance46;
		buttonTool15.SharedProps.Caption = "刪除期別";
		buttonTool15.SharedProps.Category = "計價";
		buttonTool15.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		comboBoxTool2.SharedProps.Caption = "第2頁的尋找";
		comboBoxTool2.SharedProps.Category = "計價";
		comboBoxTool2.SharedProps.Width = 200;
		comboBoxTool2.ValueList = valueList1;
		appearance47.Image = resources.GetObject("appearance23.Image");
		buttonTool16.SharedProps.AppearancesSmall.Appearance = appearance47;
		buttonTool16.SharedProps.Caption = "執行尋找";
		buttonTool16.SharedProps.Category = "計價";
		popupMenuTool7.SharedProps.Caption = "檔案(&F)";
		popupMenuTool7.SharedProps.Category = "計價";
		buttonTool19.InstanceProps.IsFirstInGroup = true;
		buttonTool21.InstanceProps.IsFirstInGroup = true;
		buttonTool23.InstanceProps.IsFirstInGroup = true;
		buttonTool24.InstanceProps.IsFirstInGroup = true;
		popupMenuTool7.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[8] { buttonTool17, buttonTool18, buttonTool19, buttonTool20, buttonTool21, buttonTool22, buttonTool23, buttonTool24 });
		popupMenuTool8.SharedProps.Caption = "編輯(&E)";
		popupMenuTool8.SharedProps.Category = "計價";
		buttonTool26.InstanceProps.IsFirstInGroup = true;
		popupMenuTool8.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[2] { buttonTool25, buttonTool26 });
		popupMenuTool9.SharedProps.Caption = "檢視(&V)";
		popupMenuTool9.SharedProps.Category = "計價";
		buttonTool28.InstanceProps.IsFirstInGroup = true;
		buttonTool29.InstanceProps.IsFirstInGroup = true;
		buttonTool30.InstanceProps.IsFirstInGroup = true;
		popupMenuTool9.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[4] { buttonTool27, buttonTool28, buttonTool29, buttonTool30 });
		popupMenuTool10.SharedProps.Caption = "工具(&T)";
		popupMenuTool10.SharedProps.Category = "計價";
		popupMenuTool10.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[1] { buttonTool31 });
		buttonTool32.SharedProps.Caption = "編輯本期進度計算...";
		buttonTool32.SharedProps.Category = "計價";
		buttonTool33.SharedProps.Caption = "編輯契約基本資料...";
		buttonTool33.SharedProps.Category = "計價";
		buttonTool34.SharedProps.Caption = "各期估驗彙整查詢...";
		buttonTool34.SharedProps.Category = "計價";
		buttonTool35.SharedProps.Caption = "統計圖表";
		buttonTool35.SharedProps.Category = "計價";
		appearance48.Image = resources.GetObject("appearance24.Image");
		buttonTool36.SharedProps.AppearancesSmall.Appearance = appearance48;
		buttonTool36.SharedProps.Caption = "重新總計";
		buttonTool36.SharedProps.Category = "計價";
		buttonTool36.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		labelTool3.SharedProps.Caption = "尋找:";
		labelTool3.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool37.SharedProps.Caption = "結束估驗記錄";
		appearance49.Image = resources.GetObject("appearance25.Image");
		buttonTool38.SharedProps.AppearancesSmall.Appearance = appearance49;
		buttonTool38.SharedProps.Caption = "報表列印...";
		buttonTool39.SharedProps.Caption = "查閱本期總計";
		buttonTool39.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool40.SharedProps.Caption = "顯示計價期別一覽表";
		popupMenuTool11.SharedProps.Caption = "說明(&H)";
		buttonTool42.InstanceProps.IsFirstInGroup = true;
		popupMenuTool11.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[2] { buttonTool41, buttonTool42 });
		buttonTool43.SharedProps.Caption = "關於PCCES...";
		buttonTool44.SharedProps.Caption = "匯出計價資料...";
		buttonTool45.SharedProps.Caption = "匯入計價資料...";
		buttonTool46.SharedProps.Caption = "最新消息...";
		appearance50.Image = resources.GetObject("appearance26.Image");
		buttonTool47.SharedProps.AppearancesSmall.Appearance = appearance50;
		buttonTool47.SharedProps.Caption = "載入月報資料(Excel)...";
		appearance51.Image = resources.GetObject("appearance27.Image");
		buttonTool48.SharedProps.AppearancesSmall.Appearance = appearance51;
		buttonTool48.SharedProps.Caption = "電子檔製作...";
		labelTool4.SharedProps.Caption = "階層:";
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
		this.ultraToolbarsManager1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[37]
		{
			buttonTool8, popupMenuTool6, buttonTool13, buttonTool14, buttonTool15, comboBoxTool2, buttonTool16, popupMenuTool7, popupMenuTool8, popupMenuTool9,
			popupMenuTool10, buttonTool32, buttonTool33, buttonTool34, buttonTool35, buttonTool36, labelTool3, buttonTool37, buttonTool38, buttonTool39,
			buttonTool40, popupMenuTool11, buttonTool43, buttonTool44, buttonTool45, buttonTool46, buttonTool47, buttonTool48, labelTool4, stateButtonTool9,
			stateButtonTool10, stateButtonTool11, stateButtonTool12, stateButtonTool13, stateButtonTool14, stateButtonTool15, stateButtonTool16
		});
		this.ultraToolbarsManager1.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(ultraToolbarsManager1_ToolClick);
		this._FormSys_B_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
		this._FormSys_B_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
		this._FormSys_B_Toolbars_Dock_Area_Top.Name = "_FormSys_B_Toolbars_Dock_Area_Top";
		this._FormSys_B_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(808, 52);
		this._FormSys_B_Toolbars_Dock_Area_Top.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 549);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Name = "_FormSys_B_Toolbars_Dock_Area_Bottom";
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(808, 0);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
		this._FormSys_B_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 52);
		this._FormSys_B_Toolbars_Dock_Area_Left.Name = "_FormSys_B_Toolbars_Dock_Area_Left";
		this._FormSys_B_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 497);
		this._FormSys_B_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
		this._FormSys_B_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(808, 52);
		this._FormSys_B_Toolbars_Dock_Area_Right.Name = "_FormSys_B_Toolbars_Dock_Area_Right";
		this._FormSys_B_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 497);
		this._FormSys_B_Toolbars_Dock_Area_Right.ToolbarsManager = this.ultraToolbarsManager1;
		this.LeftPanel.Controls.Add(this.onlineList1);
		this.LeftPanel.Controls.Add(this.functionButtons1);
		this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
		this.LeftPanel.Location = new System.Drawing.Point(0, 52);
		this.LeftPanel.Name = "LeftPanel";
		this.LeftPanel.Size = new System.Drawing.Size(160, 497);
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
		this.functionButtons1.Size = new System.Drawing.Size(160, 497);
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
		this.pnl_spliter.Size = new System.Drawing.Size(7, 497);
		this.pnl_spliter.TabIndex = 11;
		appearance52.BorderColor = System.Drawing.Color.Transparent;
		appearance52.BorderColor3DBase = System.Drawing.Color.Transparent;
		appearance52.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance13.ImageBackground");
		this.Btn_Splt.Appearance = appearance52;
		this.Btn_Splt.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Borderless;
		this.Btn_Splt.Cursor = System.Windows.Forms.Cursors.Hand;
		this.Btn_Splt.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Btn_Splt.ImageSize = new System.Drawing.Size(7, 57);
		this.Btn_Splt.Location = new System.Drawing.Point(0, 220);
		this.Btn_Splt.Name = "Btn_Splt";
		this.Btn_Splt.ShapeImage = (System.Drawing.Image)resources.GetObject("Btn_Splt.ShapeImage");
		this.Btn_Splt.ShowFocusRect = false;
		this.Btn_Splt.ShowOutline = false;
		this.Btn_Splt.Size = new System.Drawing.Size(7, 66);
		this.Btn_Splt.TabIndex = 7;
		this.Btn_Splt.Click += new System.EventHandler(Btn_Splt_Click);
		this.ssp_Lower.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.ssp_Lower.Location = new System.Drawing.Point(0, 286);
		this.ssp_Lower.Name = "ssp_Lower";
		this.ssp_Lower.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Lower.OcxState");
		this.ssp_Lower.Size = new System.Drawing.Size(7, 208);
		this.ssp_Lower.TabIndex = 6;
		this.ssp_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.ssp_Bottom.Location = new System.Drawing.Point(0, 494);
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
		this.panel1.Controls.Add(this.panel2);
		this.panel1.Controls.Add(this.panel3);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.panel1.Location = new System.Drawing.Point(167, 52);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(641, 497);
		this.panel1.TabIndex = 12;
		this.panel2.Controls.Add(this.panel4);
		this.panel2.Controls.Add(this.splitter1);
		this.panel2.Controls.Add(this.PNL_INV);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Location = new System.Drawing.Point(0, 30);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(641, 467);
		this.panel2.TabIndex = 4;
		this.splitter1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
		this.splitter1.Location = new System.Drawing.Point(0, 144);
		this.splitter1.Name = "splitter1";
		this.splitter1.Size = new System.Drawing.Size(641, 8);
		this.splitter1.TabIndex = 13;
		this.splitter1.TabStop = false;
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
		base.ClientSize = new System.Drawing.Size(808, 549);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.pnl_spliter);
		base.Controls.Add(this.LeftPanel);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Right);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Left);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Top);
		base.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Bottom);
		base.KeyPreview = true;
		base.Name = "FormInvoice";
		this.Text = "計價記錄";
		base.Load += new System.EventHandler(FormInvoice_Load);
		base.Resize += new System.EventHandler(FormInvoice_Resize);
		this.panel4.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridBudget2).EndInit();
		this.panel7.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.axSSPanel2).EndInit();
		this.panel6.ResumeLayout(false);
		this.PNL_INV.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Grid1).EndInit();
		this.panel5.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).EndInit();
		this.LeftPanel.ResumeLayout(false);
		this.LeftPanel.PerformLayout();
		this.pnl_spliter.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ssp_Lower).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Bottom).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Upper).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Top).EndInit();
		this.panel1.ResumeLayout(false);
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

	public FormInvoice()
	{
		InitializeComponent();
		GridColsSquence2 = new object[gridBudget2.Cols.Count, 8];
		CellStyle cs11 = gridBudget2.Styles.Add("EditMode");
		cs11.DataType = typeof(Image);
		cs11.ImageAlign = ImageAlignEnum.RightCenter;
		HideCols(IsHide: true);
	}

	private void HideCols(bool IsHide)
	{
		if (IsHide)
		{
			gridBudget2.Cols["Lock"].Visible = false;
			gridBudget2.Cols["Kind"].Visible = false;
			gridBudget2.Cols["AnaImg"].Visible = false;
			gridBudget2.Cols["PrintNo"].Visible = false;
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

	private void RememberColsProps2()
	{
		for (int i = 0; i < gridBudget2.Cols.Count; i++)
		{
			GridColsSquence2[i, 0] = gridBudget2.Cols[i].Name;
			GridColsSquence2[i, 1] = gridBudget2.Cols[i].Caption;
			GridColsSquence2[i, 2] = gridBudget2.Cols[i].Width;
			if (gridBudget2.Cols[i].Name == "AnaImg")
			{
				GridColsSquence2[i, 3] = typeof(Image);
			}
			else
			{
				GridColsSquence2[i, 3] = gridBudget2.Cols[i].DataType;
			}
			GridColsSquence2[i, 4] = gridBudget2.Cols[i].Visible;
			GridColsSquence2[i, 5] = gridBudget2.Cols[i].Format;
			GridColsSquence2[i, 6] = gridBudget2.Cols[i].AllowEditing;
			if (gridBudget2.Cols[i].Name == "Qty")
			{
				if (F_MainQty > 0)
				{
					GridColsSquence2[i, 5] = "###,###,###,##0." + "0".PadLeft(F_MainQty, '0');
				}
				else
				{
					GridColsSquence2[i, 5] = "###,###,###,##0";
				}
			}
			if (gridBudget2.Cols[i].Name == "Cost")
			{
				if (F_MainCst > 0)
				{
					GridColsSquence2[i, 5] = "###,###,###,##0." + "0".PadLeft(F_MainCst, '0');
				}
				else
				{
					GridColsSquence2[i, 5] = "###,###,###,##0";
				}
			}
			if (gridBudget2.Cols[i].Name == "Amount")
			{
				if (F_MainAmt > 0)
				{
					GridColsSquence2[i, 5] = "###,###,###,##0." + "0".PadLeft(F_MainAmt, '0');
				}
				else
				{
					GridColsSquence2[i, 5] = "###,###,###,##0";
				}
			}
			if (gridBudget2.Cols[i].Name == "this_qty")
			{
				GridColsSquence2[i, 5] = "###,###,###,##0." + "0".PadLeft(4, '0');
			}
			if (gridBudget2.Cols[i].Name == "this_amt")
			{
				if (F_MainAmt > 0)
				{
					GridColsSquence2[i, 5] = "###,###,###,##0." + "0".PadLeft(F_MainAmt, '0');
				}
				else
				{
					GridColsSquence2[i, 5] = "###,###,###,##0";
				}
			}
			if (gridBudget2.Cols[i].Name == "pre_qty")
			{
				GridColsSquence2[i, 5] = "###,###,###,##0." + "0".PadLeft(4, '0');
			}
			if (gridBudget2.Cols[i].Name == "pre_amt")
			{
				if (F_MainAmt > 0)
				{
					GridColsSquence2[i, 5] = "###,###,###,##0." + "0".PadLeft(F_MainAmt, '0');
				}
				else
				{
					GridColsSquence2[i, 5] = "###,###,###,##0";
				}
			}
			if (gridBudget2.Cols[i].Name == "acc_qty")
			{
				GridColsSquence2[i, 5] = "###,###,###,##0." + "0".PadLeft(4, '0');
			}
			if (gridBudget2.Cols[i].Name == "acc_amt")
			{
				if (F_MainAmt > 0)
				{
					GridColsSquence2[i, 5] = "###,###,###,##0." + "0".PadLeft(F_MainAmt, '0');
				}
				else
				{
					GridColsSquence2[i, 5] = "###,###,###,##0";
				}
			}
			GridColsSquence2[i, 7] = gridBudget2.Cols[i].TextAlign;
		}
	}

	private void SetGridColumn2()
	{
		for (int i = 0; i < gridBudget2.Cols.Count; i++)
		{
			gridBudget2.Cols[i].Name = (string)GridColsSquence2[i, 0];
			gridBudget2.Cols[i].Caption = (string)GridColsSquence2[i, 1];
			gridBudget2.Cols[i].Width = (int)GridColsSquence2[i, 2];
			gridBudget2.Cols[i].DataType = (Type)GridColsSquence2[i, 3];
			gridBudget2.Cols[i].Visible = (bool)GridColsSquence2[i, 4];
			gridBudget2.Cols[i].Format = (string)GridColsSquence2[i, 5];
			gridBudget2.Cols[i].AllowEditing = (bool)GridColsSquence2[i, 6];
			gridBudget2.Cols[i].TextAlign = (TextAlignEnum)GridColsSquence2[i, 7];
		}
	}

	private void DoMenuAction(string MenuID)
	{
		switch (MenuID)
		{
		case "mnuNewIssue":
			if (!DBClass.ChkAuthority(F_UserID, "F01000010001"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F01000010001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Execute_NewIssue();
			}
			break;
		case "mnuExit":
			if (!DBClass.ChkAuthority(F_UserID, "F01000010006"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F01000010006") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				CloseThisForm();
			}
			break;
		case "mnuReCal":
			if (!DBClass.ChkAuthority(F_UserID, "F01000040001"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F01000040001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				DoReCal("");
			}
			break;
		case "mnuDeleteIssue":
			if (!DBClass.ChkAuthority(F_UserID, "F01000010002"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F01000010002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Do_DeleteIssue();
			}
			break;
		case "mnuThisProgress":
			if (!DBClass.ChkAuthority(F_UserID, "F01000020001"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F01000020001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				ExeciteThisProgress();
			}
			break;
		case "mnuBasic":
			if (!DBClass.ChkAuthority(F_UserID, "F01000020002"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F01000020002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Execute_BASIC_CNT();
			}
			break;
		case "mnuEditIssue":
		case "mnuThisTotal":
			if (!DBClass.ChkAuthority(F_UserID, "F01000030002"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F01000030002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Execute_ThisTotal();
			}
			break;
		case "mnuSummaryList":
			if (!DBClass.ChkAuthority(F_UserID, "F01000030001"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F01000030001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Execite_SummaryList();
			}
			break;
		case "mnuShowList":
			Do_ShowList();
			break;
		case "mnuStatictics":
			if (!DBClass.ChkAuthority(F_UserID, "F01000030003"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F01000030003") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Execute_Statistics();
			}
			break;
		case "mnuPrint":
			if (!DBClass.ChkAuthority(F_UserID, "F01000010003"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F01000010003") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Execute_Print();
			}
			break;
		case "mnuAbout":
			if (!DBClass.ChkAuthority(F_UserID, "F01000050001"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F01000050001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Execute_About();
			}
			break;
		case "mnuExport":
			if (!DBClass.ChkAuthority(F_UserID, "F01000010005"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F01000010005") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Execute_Export();
			}
			break;
		case "mnuImport":
			if (!DBClass.ChkAuthority(F_UserID, "F01000010004"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F01000010004") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Execute_Import();
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
		case "mnuImportDaily":
			Do_DailyReportIn();
			break;
		case "mnuFile_Digital":
			Do_FileDigital("");
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

	private void Do_DailyReportIn()
	{
		openFileDialog1.RestoreDirectory = true;
		openFileDialog1.Filter = "月報EXCEL電子標單檔(*.xls)|*.xls";
		if (openFileDialog1.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		Aspose.Cells.License license = new Aspose.Cells.License();
		license.SetLicense("Aspose.Custom.lic");
		Excel myExcel = new Excel();
		myExcel.Open(openFileDialog1.FileName);
		Worksheet mySheet = myExcel.Worksheets[0];
		DataTable DT_XLS = new DataTable();
		DT_XLS.Columns.Add("ItemNo", Type.GetType("System.String"));
		DT_XLS.Columns.Add("CName", Type.GetType("System.String"));
		DT_XLS.Columns.Add("UnitName", Type.GetType("System.String"));
		DT_XLS.Columns.Add("CntQty", Type.GetType("System.Double"));
		DT_XLS.Columns.Add("Qty", Type.GetType("System.Double"));
		for (int i = 6; i < 65536 && mySheet.Cells[i, 0].Value != null && !(mySheet.Cells[i, 0].Value.ToString().Trim() == ""); i++)
		{
			DataRow DR = DT_XLS.NewRow();
			DR["ItemNo"] = mySheet.Cells[i, 0].Value.ToString().Trim();
			DR["CName"] = mySheet.Cells[i, 1].Value.ToString().Trim();
			DR["UnitName"] = mySheet.Cells[i, 2].Value.ToString().Trim();
			DR["CntQty"] = mySheet.Cells[i, 3].Value;
			DR["Qty"] = mySheet.Cells[i, 4].Value;
			DT_XLS.Rows.Add(DR);
		}
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(subctr) 契約書明細");
		submfq mfqcom = new submfq(tmp_AL1);
		mfqcom.ps_prjcode = F_ProjectCode;
		mfqcom.ps_subcode = F_SubProjetCode;
		mfqcom.ps_itemno = Grid1[Grid1.Row, "Queue"].ToString();
		for (int i = 0; i < DT_XLS.Rows.Count; i++)
		{
			for (int j = 1; j < gridBudget2.Rows.Count - 1; j++)
			{
				if (DT_XLS.Rows[i]["CName"].ToString() == gridBudget2[j, "CName"].ToString().Trim() && DT_XLS.Rows[i]["UnitName"].ToString() == gridBudget2[j, "UnitName"].ToString().Trim() && PubTools.Str2Double(DT_XLS.Rows[i]["CntQty"]) == PubTools.Str2Double(gridBudget2[j, "Qty"]))
				{
					mfqcom.ps_itemdes = gridBudget2[j, "PrintNo"].ToString().Trim();
					mfqcom.ps_quantity = DT_XLS.Rows[i]["Qty"].ToString();
					mfqcom.UpdItem();
				}
			}
		}
		mfqcom = null;
		myExcel = null;
		MessageBox.Show(this, "轉入完成!!", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		GetIssue_ContractData();
	}

	private void Execute_Import()
	{
		ArrayList IssueList = new ArrayList();
		for (int i = 1; i < Grid1.Rows.Count; i++)
		{
			string ls_queue = Grid1[Grid1.Row, "Queue"].ToString().Trim();
			if (ls_queue == "末期計價")
			{
				ls_queue = "9998";
			}
			IssueList.Add(ls_queue);
		}
		FormInvoiceImport FM_IMP = new FormInvoiceImport();
		FM_IMP._ProjectCode = F_ProjectCode;
		FM_IMP._UserID = F_UserID;
		FM_IMP._IssueList = IssueList;
		if (FM_IMP.ShowDialog(this) == DialogResult.OK)
		{
			GetIssueDataList();
		}
		FM_IMP.Close();
		FM_IMP.Dispose();
		FM_IMP = null;
	}

	private void Execute_Export()
	{
		string ls_queue = Grid1[Grid1.Row, "Queue"].ToString().Trim();
		if (ls_queue == "末期計價")
		{
			ls_queue = "9998";
		}
		FormInvoiceExport FM_EXP = new FormInvoiceExport();
		FM_EXP._UserID = F_UserID;
		FM_EXP._ProjectCode = F_ProjectCode;
		FM_EXP._SubProjectCode = F_SubProjetCode;
		FM_EXP._Issue = ls_queue;
		FM_EXP.ShowDialog();
		FM_EXP.Close();
		FM_EXP.Dispose();
		FM_EXP = null;
	}

	private void Execute_About()
	{
		FormAbout FMAB = new FormAbout();
		FMAB.ShowDialog();
		FMAB.Close();
		FMAB.Dispose();
		FMAB = null;
	}

	private void Execute_Print()
	{
		string ls_queue = Grid1[Grid1.Row, "Queue"].ToString().Trim();
		if (ls_queue == "末期計價")
		{
			ls_queue = "9998";
		}
		FormInvoiceReport FM_INV_RPT = new FormInvoiceReport();
		FM_INV_RPT._ActionName = F_ActionName;
		FM_INV_RPT._ProjectCode = F_ProjectCode;
		FM_INV_RPT._SubProjectCode = F_SubProjetCode;
		FM_INV_RPT._Issue = ls_queue;
		FM_INV_RPT._UserID = F_UserID;
		FM_INV_RPT.ShowDialog();
		FM_INV_RPT.Close();
		FM_INV_RPT.Dispose();
		FM_INV_RPT = null;
	}

	private void Execute_Statistics()
	{
		string ls_queue = Grid1[Grid1.Row, "Queue"].ToString().Trim();
		if (ls_queue == "末期計價")
		{
			ls_queue = "9998";
		}
		FormInvoiceGraphic FM_GP = new FormInvoiceGraphic();
		FM_GP._ProjectCode = F_ProjectCode;
		FM_GP._SubProjectCode = F_SubProjetCode;
		FM_GP._ProjectCName = F_ProjectNameC;
		FM_GP._UserID = F_UserID;
		FM_GP._Issue = ls_queue;
		FM_GP.ShowDialog(this);
		FM_GP.Close();
		FM_GP.Dispose();
		FM_GP = null;
	}

	private void Do_ShowList()
	{
		PNL_INV.Visible = true;
		ultraToolbarsManager1.Tools["mnuShowList"].SharedProps.Visible = false;
	}

	private void Execite_SummaryList()
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

	private void Execute_ThisTotal()
	{
		string ls_queue = Grid1[Grid1.Row, "Queue"].ToString().Trim();
		if (ls_queue == "末期計價")
		{
			ls_queue = "9998";
		}
		FormInvoiceSubAcInfo FM_INVSUB = new FormInvoiceSubAcInfo();
		FM_INVSUB._ProjectCode = F_ProjectCode;
		FM_INVSUB._SubProjectCode = F_SubProjetCode;
		FM_INVSUB._UserID = F_UserID;
		FM_INVSUB._Issue = ls_queue;
		FM_INVSUB._TotalPrec = Grid1[Grid1.Row, "total_prec"].ToString();
		FM_INVSUB.ShowDialog(this);
		GetIssueDataList();
	}

	private void Execute_BASIC_CNT()
	{
		FormSplitCnt_Basic FM_BASIC = new FormSplitCnt_Basic();
		FM_BASIC._ProjectCode = F_ProjectCode;
		FM_BASIC._ProjectName = F_ProjectNameC;
		FM_BASIC._SubProjectCode = F_SubProjetCode;
		FM_BASIC._ActionName = F_ActionName;
		FM_BASIC._UserID = F_UserID;
		FM_BASIC.Owner = this;
		FM_BASIC.ShowDialog();
		FM_BASIC.Close();
		FM_BASIC.Dispose();
		FM_BASIC = null;
		GetCntProcess();
	}

	private void ExeciteThisProgress()
	{
		string ls_queue = Grid1[Grid1.Row, "Queue"].ToString().Trim();
		if (ls_queue == "末期計價")
		{
			ls_queue = "9998";
		}
		FormInvoiceProgress FM_PRG = new FormInvoiceProgress();
		FM_PRG._ProjectCode = F_ProjectCode;
		FM_PRG._SubProjectCode = F_SubProjetCode;
		FM_PRG._UserID = F_UserID;
		FM_PRG._Issue = ls_queue;
		FM_PRG._Progress = PubTools.Str2Decimal(Grid1[Grid1.Row, "this_prec"]);
		FM_PRG._StartDate = PubTools.Str2DateTime(Grid1[Grid1.Row, "date_rece"]);
		FM_PRG._EndDate = PubTools.Str2DateTime(Grid1[Grid1.Row, "date_insp"]);
		if (FM_PRG.ShowDialog(this) == DialogResult.OK)
		{
			int idxGrid1 = Grid1.Row;
			GetIssueDataList();
			Grid1.Row = idxGrid1;
			DoReCal("NOMESSAGE");
		}
		FM_PRG.Close();
		FM_PRG.Dispose();
		FM_PRG = null;
	}

	private void Do_DeleteIssue()
	{
		string ls_queue = Grid1[Grid1.Row, "Queue"].ToString().Trim();
		if (ls_queue == "末期計價")
		{
			ls_queue = "9998";
		}
		if (Grid1.Rows.Count - 1 > PubTools.Str2Int(ls_queue))
		{
			MessageBox.Show(this, "有較大的期別存在，不可刪除此期別資料", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(subacc) 刪除-估驗計價");
		string sWarning = "確定要刪除選取的 第" + ls_queue + "期 計價期別?";
		if (ls_queue == "9998")
		{
			sWarning = "確定要刪除選取的  末期計價  計價期別?";
		}
		if (MessageBox.Show(this, sWarning, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			string ls_prjcode = F_ProjectCode;
			string ls_subproj = F_SubProjetCode;
			sub_acc acccom = new sub_acc(tmp_AL1);
			int lb_mode = acccom.DeleItem(ls_queue, ls_subproj, ls_prjcode);
			if (ls_queue == "9998")
			{
				acccom.SetLockMode("0", "9999", ls_subproj, ls_prjcode);
			}
		}
		GetIssueDataList();
		DTEdit.Clear();
		if (Grid1.Rows.Count == 1)
		{
			gridBudget2.Rows.Count = 1;
		}
	}

	private void DoReCal(string IsMessage)
	{
		try
		{
			if (ReCflag && MessageBox.Show(this, "是否將計價數量為【0】的項目存入並重新總計?", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				SaveGridDataTosubmfq();
				ReCflag = false;
			}
			string ls_Queue = Grid1[Grid1.Row, "Queue"].ToString().Trim();
			if (ls_Queue == "末期計價")
			{
				ls_Queue = "9998";
			}
			string ls_SQL = "Select Distinct chgCount From Submfq Where Project='" + F_ProjectCode + "' and itemno=" + ls_Queue + " ";
			ArrayList tmp_AL1 = new ArrayList();
			tmp_AL1 = new ArrayList();
			tmp_AL1.Add(F_UserID);
			tmp_AL1.Add("(subacc) 顯示-估驗計價");
			ModifyDB StdCom = new ModifyDB("", tmp_AL1);
			sub_acc AccCom = new sub_acc(tmp_AL1);
			AccCom._DTEdit = DTEdit;
			AccCom.ps_Issue = StdCom.DBGetValue(ls_SQL);
			DT2_2 = AccCom.ReTotal2(DT2_2, ls_Queue, F_SubProjetCode, F_ProjectCode);
			submfq MfqCom = new submfq(tmp_AL1);
			foreach (DataRow dr in DT2_2.Rows)
			{
				MfqCom.ps_quantity = dr["quantity"].ToString();
				MfqCom.ps_tom_amt = dr["tom_amt"].ToString();
				MfqCom.ps_itemdes = dr["itemdes"].ToString();
				MfqCom.ps_itemno = dr["qucode"].ToString();
				MfqCom.ps_prjcode = dr["project"].ToString();
				MfqCom.ps_subcode = dr["sproj"].ToString();
				MfqCom.UpdItem();
			}
			int idxGrid1 = Grid1.Row;
			GetIssueDataList();
			Grid1.Row = idxGrid1;
			if (IsMessage == "")
			{
				MessageBox.Show(this, "重新計算完畢!", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Invoice.FormInvoice.cs" + ex.Message);
			Console.Write(ex.Message);
			MessageBox.Show(ex.Message);
		}
	}

	private void Execute_NewIssue()
	{
		FORM_STATUS = "NEW_ISSUE";
		FormSplitCnt_NewIssue FM_NEW_ISS = new FormSplitCnt_NewIssue();
		FM_NEW_ISS._UserID = F_UserID;
		FM_NEW_ISS._ProjectCode = F_ProjectCode;
		FM_NEW_ISS._SubProjetCode = F_SubProjetCode;
		if (FM_NEW_ISS.ShowDialog(this) == DialogResult.OK)
		{
			GetIssueDataList();
			DoReCal("NOMESSAGE");
		}
		FM_NEW_ISS.Close();
		FM_NEW_ISS.Dispose();
		FM_NEW_ISS = null;
		FORM_STATUS = "NORMAL";
		DTEdit.Clear();
		Grid1.Row = Grid1.Rows.Count - 1;
	}

	private void CloseThisForm()
	{
		string sWarning = "確定要結束 ?";
		if (MessageBox.Show(this, sWarning, "計價記錄", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
		string ls_Queue = Grid1[Grid1.Row, "Queue"].ToString().Trim();
		if (ls_Queue == "末期計價")
		{
			ls_Queue = "9998";
		}
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
		FM_BDGT_EXP_WZD._queue = ls_Queue;
		FM_BDGT_EXP_WZD._chgCount = ls_Queue;
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

	private void GetIssueDataList()
	{
		lblProjectData2.Text = "【" + F_ProjectCode + "】" + F_ProjectNameC;
		string ls_prjcode = F_ProjectCode;
		string ls_subproj = F_SubProjetCode;
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(subacc) 顯示-估驗計價");
		PubTools.WriteRoughlyLog(tmp_AL1);
		sub_acc acccom = new sub_acc(tmp_AL1);
		DT2_1 = acccom.ListItem("", ls_subproj.Trim(), ls_prjcode.Trim());
		BindToGrid2_1();
	}

	private void BindToGrid2_1()
	{
		DataView DV = new DataView(DT2_1);
		int iRows = DT2_1.Rows.Count + 1;
		Grid1.Rows.Count = iRows;
		DV.RowFilter = "IsLastqueue = 'N' and queue = '9998'";
		if (DV.Count > 0)
		{
			Grid1.Rows.Count = iRows - 1;
		}
		if (DT2_1.Rows.Count <= 0)
		{
			gridBudget2.Rows.Count = 1;
		}
		for (int i = 0; i < DT2_1.Rows.Count; i++)
		{
			if (DT2_1.Rows[i]["queue"].ToString().Trim() == "9998")
			{
				if (DT2_1.Rows[i]["IsLastqueue"].ToString().Trim() == "Y")
				{
					Grid1[i + 1, "Queue"] = "末期計價";
					Grid1[i + 1, "date_rece"] = DT2_1.Rows[i]["date_rece"].ToString().Trim();
					Grid1[i + 1, "date_insp"] = DT2_1.Rows[i]["date_insp"].ToString().Trim();
					Grid1[i + 1, "this_prec"] = DT2_1.Rows[i]["this_prec"].ToString().Trim();
					Grid1[i + 1, "total_prec"] = DT2_1.Rows[i]["total_prec"].ToString().Trim();
				}
			}
			else
			{
				Grid1[i + 1, "Queue"] = DT2_1.Rows[i]["queue"].ToString().Trim();
				Grid1[i + 1, "date_rece"] = DT2_1.Rows[i]["date_rece"].ToString().Trim();
				Grid1[i + 1, "date_insp"] = DT2_1.Rows[i]["date_insp"].ToString().Trim();
				Grid1[i + 1, "this_prec"] = DT2_1.Rows[i]["this_prec"].ToString().Trim();
				Grid1[i + 1, "total_prec"] = DT2_1.Rows[i]["total_prec"].ToString().Trim();
			}
		}
		if (Grid1.Rows.Count > 1)
		{
			iCountNum = Grid1.Rows.Count - 1;
			if (Firstflag != "")
			{
				Grid1.Row = iCountNum;
				Firstflag = "";
			}
			GetIssue_ContractData();
		}
		ultraStatusBar2.Panels[0].Text = "資料筆數：" + DT2_1.Rows.Count;
		IssueModeCheck();
	}

	private void IssueModeCheck()
	{
		if (Grid1.Rows.Count <= 1)
		{
			ultraToolbarsManager1.Tools["mnuDeleteIssue"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuThisProgress"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuStatictics"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuReCal"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuPrint"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuThisTotal"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuExport"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuFile_Digital"].SharedProps.Enabled = false;
			lblThisIssue.Text = "";
		}
		else
		{
			ultraToolbarsManager1.Tools["mnuDeleteIssue"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuThisProgress"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuStatictics"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuReCal"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuPrint"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuThisTotal"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuExport"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuFile_Digital"].SharedProps.Enabled = true;
		}
	}

	private void GetIssue_ContractData()
	{
		if (Grid1.Rows.Count > 1 && Grid1[Grid1.Row, "Queue"] != null)
		{
			string sQueue = Grid1[Grid1.Row, "Queue"].ToString().Trim();
			if (sQueue == "末期計價")
			{
				sQueue = "9998";
			}
			ArrayList tmp_AL1 = new ArrayList();
			tmp_AL1 = new ArrayList();
			tmp_AL1.Add(F_UserID);
			tmp_AL1.Add("(subctr) 契約書明細");
			submfq mfqcom = new submfq(tmp_AL1);
			string ls_prjcode = F_ProjectCode;
			string ls_subproj = F_SubProjetCode;
			string ls_queue = sQueue;
			DT2_2 = mfqcom.ListItem("", ls_queue.Trim(), ls_subproj.Trim(), ls_prjcode.Trim());
			if (ls_queue != "9998")
			{
				lblThisIssue.Text = "【目前編輯期別：" + ls_queue + " 】";
			}
			else
			{
				lblThisIssue.Text = "【目前編輯期別：末期計價 】";
			}
			BindToGrid2();
		}
	}

	private void BindToGrid2()
	{
		ultraToolbarsManager1.BeginUpdate();
		ultraToolbarsManager1.Enabled = false;
		gridBudget2.Visible = false;
		int iLevel = 0;
		RememberColsProps2();
		gridBudget2.Clear(ClearFlags.All);
		CellStyle CS2 = gridBudget2.Styles.Add("MainColor");
		CS2.ForeColor = Color.Blue;
		gridBudget2.Select(0, 0);
		int iRows = DT2_2.Rows.Count + 1;
		gridBudget2.Rows.Count = iRows;
		SetGridColumn2();
		string sKind = "";
		double aTotal = 0.0;
		for (int i = 0; i < DT2_2.Rows.Count; i++)
		{
			sKind = ((DT2_2.Rows[i]["kind"].ToString().Length > 0) ? DT2_2.Rows[i]["kind"].ToString().ToUpper().Trim() : "");
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
				gridBudget2.Rows[i + 1].Style = gridBudget2.Styles["MainColor"];
				break;
			}
			gridBudget2[i + 1, "ItemNo"] = DT2_2.Rows[i]["ItemNo"];
			gridBudget2[i + 1, "CName"] = DT2_2.Rows[i]["cName"];
			gridBudget2[i + 1, "UnitName"] = DT2_2.Rows[i]["itemunit"];
			gridBudget2[i + 1, "Qty"] = DT2_2.Rows[i]["itemqty"];
			gridBudget2[i + 1, "Cost"] = DT2_2.Rows[i]["itemcost"];
			gridBudget2[i + 1, "this_qty"] = ((DT2_2.Rows[i]["quantity"] == DBNull.Value) ? ((object)0) : DT2_2.Rows[i]["quantity"]);
			gridBudget2[i + 1, "this_amt"] = ((DT2_2.Rows[i]["tom_amt"] == DBNull.Value) ? ((object)0) : DT2_2.Rows[i]["tom_amt"]);
			gridBudget2[i + 1, "acc_prec"] = ((DT2_2.Rows[i]["acc_prec"] == DBNull.Value) ? ((object)0) : DT2_2.Rows[i]["acc_prec"]);
			gridBudget2[i + 1, "pre_prec"] = ((DT2_2.Rows[i]["pre_prec"] == DBNull.Value) ? ((object)0) : DT2_2.Rows[i]["pre_prec"]);
			gridBudget2[i + 1, "pre_qty"] = ((DT2_2.Rows[i]["pre_qty"] == DBNull.Value) ? ((object)0) : DT2_2.Rows[i]["pre_qty"]);
			gridBudget2[i + 1, "pre_amt"] = ((DT2_2.Rows[i]["pre_amt"] == DBNull.Value) ? ((object)0) : DT2_2.Rows[i]["pre_amt"]);
			gridBudget2[i + 1, "acc_qty"] = ((DT2_2.Rows[i]["acc_qty"] == DBNull.Value) ? ((object)0) : DT2_2.Rows[i]["acc_qty"]);
			gridBudget2[i + 1, "acc_amt"] = ((DT2_2.Rows[i]["acc_amt"] == DBNull.Value) ? ((object)0) : DT2_2.Rows[i]["acc_amt"]);
			gridBudget2[i + 1, "kind"] = DT2_2.Rows[i]["kind"];
			gridBudget2[i + 1, "AccMode"] = DT2_2.Rows[i]["AccMode"];
			gridBudget2[i + 1, "PrintNo"] = DT2_2.Rows[i]["itemdes"];
			if (gridBudget2[i + 1, "Kind"] != null)
			{
				gridBudget2.Rows[i + 1].IsNode = true;
			}
			if (DT2_2.Rows[i]["itemdes"].ToString().Trim() == "".PadLeft(32, '9'))
			{
				gridBudget2.Rows[i + 1].Node.Level = 1;
				aTotal = PubTools.Str2Double(PubTools.ARound(DT2_2.Rows[i]["tom_amt"], F_MainAmt));
			}
			else
			{
				gridBudget2.Rows[i + 1].Node.Level = Convert.ToInt32(DT2_2.Rows[i]["itemdes"].ToString().Trim().Length / 4);
			}
			if (DT2_2.Rows[i]["AccMode"] != null)
			{
				if (DT2_2.Rows[i]["AccMode"].ToString() == "0")
				{
					gridBudget2[i + 1, "AccMode"] = "警告但可存檔";
				}
				else if (DT2_2.Rows[i]["AccMode"].ToString() == "1")
				{
					gridBudget2[i + 1, "AccMode"] = "警告且不可存檔";
				}
				else if (DT2_2.Rows[i]["AccMode"].ToString() == "2")
				{
					gridBudget2[i + 1, "AccMode"] = "略過";
				}
			}
			if (gridBudget2.Rows[i + 1].Node.Level > iLevel)
			{
				iLevel = gridBudget2.Rows[i + 1].Node.Level;
			}
		}
		SetColsEditSymbol(ref gridBudget2);
		string ls_queue = Grid1[Grid1.Row, "Queue"].ToString().Trim();
		if (ls_queue == "末期計價")
		{
			ls_queue = "9998";
		}
		if (Grid1.Rows.Count - 1 > PubTools.Str2Int(ls_queue))
		{
			gridBudget2.AllowEditing = false;
		}
		else
		{
			gridBudget2.AllowEditing = true;
		}
		SwitchToCorrectLevelStatus(iLevel);
		lblTotal.Text = string.Format("{0:N" + F_MainAmt + "}", aTotal);
		ultraStatusBar3.Panels[0].Text = "資料筆數：" + DT2_2.Rows.Count;
		gridBudget2.Visible = true;
		gridBudget2.Invalidate();
		ultraToolbarsManager1.Enabled = true;
		ultraToolbarsManager1.EndUpdate();
	}

	private void SetColsEditSymbol(ref GridBudget g1)
	{
		for (int i = 1; i < g1.Cols.Count; i++)
		{
			if (g1.Cols[i].AllowEditing)
			{
				CellRange rg = g1.GetCellRange(0, i);
				rg.Style = gridBudget2.Styles["EditMode"];
				rg.Image = imageList2.Images[1];
			}
		}
	}

	private void FormInvoice_Resize(object sender, EventArgs e)
	{
		int TotalH = pnl_spliter.Height;
		int iHeight = (TotalH - 3 - 3 - 57) / 2;
		ssp_Upper.Height = iHeight;
		ssp_Lower.Height = iHeight;
	}

	private void ultraToolbarsManager1_ToolClick(object sender, ToolClickEventArgs e)
	{
		DoMenuAction(e.Tool.Key);
	}

	private void FormInvoice_Load(object sender, EventArgs e)
	{
		Firstflag = "FIRST";
		ultraToolbarsManager1.Tools["mnuShowList"].SharedProps.Visible = false;
		base.ParentForm.Text = "PCCES Win 4.3 【計價記錄】";
		FormInvoice_Resize(null, null);
		functionButtons1._UserID = F_UserID;
		functionButtons1._UserName = F_UserName;
		functionButtons1._ServerName = F_ServerName;
		functionButtons1._CurrOpenMode = FunctionOpenMode.Invoice;
		functionButtons1._ActiveFunction = "INVOICE";
		onlineList1._UserID = F_UserID;
		onlineList1._UserName = F_UserName;
		onlineList1._ServerName = F_ServerName;
		onlineList1._FunctionName = F_FunctionName;
		onlineList1._HasRegistered = F_HasRegistered;
		onlineList1.Connect();
		SysUser oSysUser = new SysUser();
		ultraStatusBar3.Panels[1].Text = oSysUser.GetSysUserDatabaseDesc(F_UserID);
		SettingDecimal();
		GetIssueDataList();
		GetCntProcess();
	}

	private void GetCntProcess()
	{
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(subctr) 契約資料");
		sub_info SubInfo = new sub_info(tmp_AL1);
		DataTable tmp = SubInfo.ListItem(F_SubProjetCode, F_ProjectCode);
		try
		{
			iAccMode = PubTools.Str2Int(tmp.Rows[0]["AccMode"]);
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Invoice.FormInvoice.cs" + ex.Message);
			iAccMode = 0;
		}
		SubInfo = null;
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

	private void gridBudget2_AfterEdit(object sender, RowColEventArgs e)
	{
		string sQueue = Grid1[Grid1.Row, "Queue"].ToString();
		if (sQueue == "末期計價")
		{
			sQueue = "9998";
		}
		int li_AmtDec = F_MainAmt;
		int li_QtyDec = F_MainQty;
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(subctr) 契約書明細");
		submfq mfqcom = new submfq(tmp_AL1);
		mfqcom.ps_prjcode = F_ProjectCode;
		mfqcom.ps_subcode = F_SubProjetCode;
		mfqcom.ps_itemno = sQueue;
		mfqcom.ps_itemdes = gridBudget2[gridBudget2.Row, "PrintNo"].ToString().Trim();
		if (gridBudget2.Cols[e.Col].Name.ToUpper() == "THIS_QTY")
		{
			double tmp0 = PubTools.Str2Double(gridBudget2[gridBudget2.Row, "Qty"].ToString());
			double tmp1 = PubTools.Str2Double(gridBudget2[gridBudget2.Row, "this_qty"].ToString());
			double tmp2 = PubTools.Str2Double(gridBudget2[gridBudget2.Row, "pre_qty"].ToString());
			string tmp_AccMode = "";
			if (gridBudget2[gridBudget2.Row, "AccMode"].ToString() == "警告但可存檔")
			{
				tmp_AccMode = "0";
			}
			else if (gridBudget2[gridBudget2.Row, "AccMode"].ToString() == "警告且不可存檔")
			{
				tmp_AccMode = "1";
			}
			else if (gridBudget2[gridBudget2.Row, "AccMode"].ToString() == "略過")
			{
				tmp_AccMode = "2";
			}
			int l_AccMode = PubTools.Str2Int(tmp_AccMode);
			if (l_AccMode < 2 && tmp0 < tmp1 + tmp2)
			{
				MessageBox.Show(this, "【" + gridBudget2[gridBudget2.Row, "CName"].ToString().Trim() + "】計價數量超過契約數量！'", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				if (l_AccMode == 1)
				{
					gridBudget2[gridBudget2.Row, "this_qty"] = 0;
					ReCflag = true;
					return;
				}
			}
			mfqcom.ps_quantity = tmp1.ToString();
			DataView DV22 = DT2_2.DefaultView;
			DV22.Sort = "itemdes";
			int idx = DV22.Find(gridBudget2[gridBudget2.Row, "PrintNo"].ToString().Trim());
			if (idx > -1)
			{
				DT2_2.Rows[idx]["quantity"] = tmp1;
			}
		}
		if (gridBudget2.Cols[e.Col].Name.ToUpper() == "THIS_AMT")
		{
			double tmp0 = PubTools.Str2Double(gridBudget2[gridBudget2.Row, "Qty"].ToString());
			double tmp3 = PubTools.Str2Double(gridBudget2[gridBudget2.Row, "Cost"].ToString());
			double tmp1 = PubTools.Str2Double(gridBudget2[gridBudget2.Row, "this_amt"].ToString());
			double tmp2 = PubTools.Str2Double(gridBudget2[gridBudget2.Row, "pre_amt"].ToString());
			string tmp_AccMode = "";
			if (gridBudget2[gridBudget2.Row, "AccMode"].ToString() == "警告但可存檔")
			{
				tmp_AccMode = "0";
			}
			else if (gridBudget2[gridBudget2.Row, "AccMode"].ToString() == "警告且不可存檔")
			{
				tmp_AccMode = "1";
			}
			else if (gridBudget2[gridBudget2.Row, "AccMode"].ToString() == "略過")
			{
				tmp_AccMode = "2";
			}
			int l_AccMode = PubTools.Str2Int(tmp_AccMode);
			if (l_AccMode < 2 && tmp0 * tmp3 < tmp1 + tmp2)
			{
				MessageBox.Show(this, "【" + gridBudget2[gridBudget2.Row, "CName"].ToString().Trim() + "】計價數量超過契約數量！'", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				if (l_AccMode == 1)
				{
					return;
				}
			}
			mfqcom.ps_tom_amt = tmp1.ToString();
			DataView DV22 = DT2_2.DefaultView;
			DV22.Sort = "itemdes";
			int idx = DV22.Find(gridBudget2[gridBudget2.Row, "PrintNo"].ToString().Trim());
			if (idx > -1)
			{
				DT2_2.Rows[idx]["tom_amt"] = tmp1;
			}
		}
		if (gridBudget2.Cols[e.Col].Name.ToUpper() == "ACC_PREC")
		{
			double ld_pre_prec = PubTools.Str2Double(gridBudget2[gridBudget2.Row, "pre_prec"].ToString());
			double ld_acc_prec = PubTools.Str2Double(gridBudget2[gridBudget2.Row, "acc_prec"].ToString());
			double ld_Prec = ld_acc_prec - ld_pre_prec;
			int li_tmp = (int)ld_Prec;
			string tmp_AccMode = "";
			if (gridBudget2[gridBudget2.Row, "AccMode"].ToString() == "警告但可存檔")
			{
				tmp_AccMode = "0";
			}
			else if (gridBudget2[gridBudget2.Row, "AccMode"].ToString() == "警告且不可存檔")
			{
				tmp_AccMode = "1";
			}
			else if (gridBudget2[gridBudget2.Row, "AccMode"].ToString() == "略過")
			{
				tmp_AccMode = "2";
			}
			int l_AccMode = PubTools.Str2Int(tmp_AccMode);
			double ld_Qty = PubTools.Str2Double(gridBudget2[gridBudget2.Row, "Qty"].ToString());
			string ls_Unit = gridBudget2[gridBudget2.Row, "UnitName"].ToString().Trim();
			double ld_Cost = PubTools.Str2Double(gridBudget2[gridBudget2.Row, "Cost"].ToString());
			double ld_Quantity = 0.0;
			double ld_Amount = 0.0;
			if (ld_Qty == 1.0 && ls_Unit == "式")
			{
				ld_Quantity = 1.0;
				ld_Amount = PubTools.ARound(ld_Cost * ld_Prec / 100.0, li_AmtDec);
			}
			else
			{
				ld_Quantity = PubTools.ARound(ld_Qty * ld_Prec / 100.0, li_QtyDec);
				ld_Amount = PubTools.ARound(ld_Cost * ld_Quantity, li_AmtDec);
				ld_Quantity = PubTools.ARound(ld_Amount / ld_Cost, li_QtyDec);
			}
			DataView DV22 = DT2_2.DefaultView;
			DV22.Sort = "itemdes";
			int idx = DV22.Find(gridBudget2[gridBudget2.Row, "PrintNo"].ToString().Trim());
			if (idx > -1)
			{
				DT2_2.Rows[idx]["quantity"] = ld_Quantity;
				DT2_2.Rows[idx]["tom_amt"] = ld_Amount;
			}
			ld_Prec = PubTools.ARound(ld_Amount / (ld_Qty * ld_Cost) * 100.0, 4L);
			if (l_AccMode < 2 && ld_Prec > 100.0)
			{
				MessageBox.Show(this, "【" + gridBudget2[gridBudget2.Row, "CName"].ToString().Trim() + "】計價數量超過契約數量！'", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				if (l_AccMode == 1)
				{
					return;
				}
			}
			DT2_2.Rows[idx]["acc_prec"] = ld_Prec;
			mfqcom.ps_quantity = ld_Quantity.ToString();
			mfqcom.ps_tom_amt = ld_Amount.ToString();
		}
		mfqcom.UpdItem();
		mfqcom = null;
		if (gridBudget2[gridBudget2.Row, "Kind"].ToString() == "F")
		{
			if (DTEdit.Columns.IndexOf("PrintNo") < 0)
			{
				DTEdit.Columns.Add("PrintNo", Type.GetType("System.String"));
			}
			if (DTEdit.Columns.IndexOf("this_qty") < 0)
			{
				DTEdit.Columns.Add("this_qty", Type.GetType("System.Double"));
			}
			DataRow dr = DTEdit.NewRow();
			dr["PrintNo"] = gridBudget2[gridBudget2.Row, "PrintNo"].ToString().Trim();
			dr["this_qty"] = gridBudget2[gridBudget2.Row, "this_qty"].ToString().Trim();
			DTEdit.Rows.Add(dr);
		}
		ultraToolbarsManager1.Enabled = true;
	}

	private void SaveGridDataTosubmfq()
	{
		string sQueue = Grid1[Grid1.Row, "Queue"].ToString().Trim();
		if (sQueue == "末期計價")
		{
			sQueue = "9998";
		}
		ArrayList arr = new ArrayList();
		arr.Add(F_UserID);
		arr.Add("(subctr) 契約書明細");
		submfq mfqcom = new submfq(arr);
		mfqcom.ps_prjcode = F_ProjectCode;
		mfqcom.ps_subcode = F_SubProjetCode;
		mfqcom.ps_itemno = sQueue;
		for (int i = 0; i < gridBudget2.Rows.Count; i++)
		{
			mfqcom.ps_itemdes = gridBudget2[i + 1, "PrintNo"].ToString().Trim();
			mfqcom.ps_quantity = gridBudget2[i + 1, "this_qty"].ToString();
			mfqcom.ps_tom_amt = gridBudget2[i + 1, "this_amt"].ToString();
			mfqcom.UpdItem();
			if (gridBudget2[i + 1, "PrintNo"].ToString().Trim() == "".PadLeft(32, '9'))
			{
				break;
			}
		}
		string ls_queue = Grid1[Grid1.Row, "Queue"].ToString().Trim();
		if (ls_queue == "末期計價")
		{
			ls_queue = "9998";
		}
		string ls_prjcode = F_ProjectCode;
		string ls_subproj = F_SubProjetCode;
		DT2_2 = mfqcom.ListItem("", ls_queue.Trim(), ls_subproj.Trim(), ls_prjcode.Trim());
		mfqcom = null;
		arr = null;
	}

	private void gridBudget2_BeforeEdit(object sender, RowColEventArgs e)
	{
		if (gridBudget2.Cols[e.Col].Name.ToUpper() == "THIS_QTY" && gridBudget2[e.Row, "UnitName"].ToString().Trim() == "式" && gridBudget2[e.Row, "Qty"].ToString().Trim() == "1")
		{
			MessageBox.Show(this, "【1 式】項目請在本期款輸入", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			gridBudget2.Col = 0;
			e.Cancel = true;
		}
		if (gridBudget2.Cols[e.Col].Name.ToUpper() == "THIS_AMT" && !(gridBudget2[e.Row, "UnitName"].ToString().Trim() == "式") && !(gridBudget2[e.Row, "Qty"].ToString() == "1"))
		{
			iCount++;
			if (iCount <= 1)
			{
				MessageBox.Show(this, "非【1 式】項目請在本期數量入", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			iCount = 0;
			gridBudget2.Col = 0;
			e.Cancel = true;
		}
		if (gridBudget2.Cols[e.Col].Name.ToUpper() == "ACC_PREC")
		{
			if (gridBudget2[e.Row, "kind"].ToString().Trim() == "B")
			{
				MessageBox.Show(this, "由下層加總的主項大類不能輸入累計進度", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				gridBudget2.Col = 0;
				e.Cancel = true;
			}
			if (gridBudget2[e.Row, "kind"].ToString().Trim() == "Z")
			{
				MessageBox.Show(this, "加總項、小計項、總計項不能輸入累計進度", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				gridBudget2.Col = 0;
				e.Cancel = true;
			}
		}
	}

	private void gridBudget2_AfterRowColChange(object sender, RangeEventArgs e)
	{
		if (Grid1.Rows.Count > 1 && gridBudget2.Cols[gridBudget2.Col].Name.ToUpper() == "ACC_PREC")
		{
			if (gridBudget2[gridBudget2.Row, "kind"].ToString().Trim() == "B")
			{
				gridBudget2.Col = 0;
			}
			if (gridBudget2[gridBudget2.Row, "kind"].ToString().Trim() == "Z")
			{
				gridBudget2.Col = 0;
			}
		}
	}

	private void Grid1_AfterRowColChange(object sender, RangeEventArgs e)
	{
		if (!(FORM_STATUS == "NEW_ISSUE") && Grid1.Row > 0)
		{
			GetIssue_ContractData();
		}
	}

	private void ultraButton1_Click(object sender, EventArgs e)
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
			GetIssueDataList();
		}
		Cursor = Cursors.Default;
	}

	private void ultraButton2_Click(object sender, EventArgs e)
	{
		ultraToolbarsManager1.Tools["mnuShowList"].SharedProps.Visible = true;
		PNL_INV.Visible = false;
	}

	private void gridBudget2_StartEdit(object sender, RowColEventArgs e)
	{
		ultraToolbarsManager1.Enabled = false;
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

	private void Grid1_MouseDown(object sender, MouseEventArgs e)
	{
		if (Grid1.Row >= 0)
		{
			string ls_queue = Grid1[Grid1.Row, "Queue"].ToString().Trim();
			if (ls_queue == "末期計價")
			{
				ls_queue = "9998";
			}
			if (Grid1.Rows.Count - 1 > PubTools.Str2Int(ls_queue))
			{
				ultraToolbarsManager1.Tools["mnuThisProgress"].SharedProps.Enabled = false;
				ultraToolbarsManager1.Tools["mnuEditIssue"].SharedProps.Enabled = false;
			}
			else
			{
				ultraToolbarsManager1.Tools["mnuThisProgress"].SharedProps.Enabled = true;
				ultraToolbarsManager1.Tools["mnuEditIssue"].SharedProps.Enabled = true;
			}
		}
	}

	private void gridBudget2_Resize(object sender, EventArgs e)
	{
		FormInvoice_Resize(sender, null);
	}
}
