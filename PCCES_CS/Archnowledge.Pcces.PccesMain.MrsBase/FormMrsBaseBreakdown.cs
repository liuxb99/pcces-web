using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.DomainModule.Bid;
using Archnowledge.Pcces.DomainModule.Budget;
using Archnowledge.Pcces.DomainModule.BusinessLogical;
using Archnowledge.Pcces.DomainModule.Coms;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.DomainModule.LogicalBase;
using Archnowledge.Pcces.DomainModule.MrsBase;
using Archnowledge.Pcces.DomainModule.Sub;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.PccesMain.Budget;
using Archnowledge.Pcces.PccesMain.Budget.BudgetChange;
using Archnowledge.Pcces.PccesMain.Library;
using Archnowledge.Pcces.PccesMain.ShellLib;
using Archnowledge.Pcces.PccesMain.SysMaintain;
using Archnowledge.Pcces.STDClass;
using Aspose.Cells;
using AxThreed;
using C1.C1Excel;
using C1.Win.C1Command;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinProgressBar;
using Infragistics.Win.UltraWinStatusBar;
using Infragistics.Win.UltraWinTabControl;
using Infragistics.Win.UltraWinToolbars;

namespace Archnowledge.Pcces.PccesMain.MrsBase;

public class FormMrsBaseBreakdown : Form
{
	private const string FileIni = "OptionSet.ini";

	private const string CallFormHelp = "FormMrsBaseBreakdown";

	private IContainer components;

	private Panel panel2;

	private UltraButton ultraButton3;

	private ImageList imageList1;

	private C1CommandLink c1CommandLink13;

	private C1CommandLink c1CommandLink11;

	private C1CommandLink c1CommandLink10;

	private C1XLBook c1XLBook1;

	private Panel panel3;

	private AxSSPanel axSSPanel1;

	private UltraLabel lblLevelNo;

	public GridMrsBase gridMrsBase1;

	private ImageList imageList2;

	private UltraToolbarsManager ultraToolbarsManager1;

	private UltraToolbarsDockArea _FormMrsBaseBreakdown_Toolbars_Dock_Area_Left;

	private UltraToolbarsDockArea _FormMrsBaseBreakdown_Toolbars_Dock_Area_Right;

	private UltraToolbarsDockArea _FormMrsBaseBreakdown_Toolbars_Dock_Area_Top;

	private UltraToolbarsDockArea _FormMrsBaseBreakdown_Toolbars_Dock_Area_Bottom;

	private ImageList imageList3;

	private Panel panel4;

	private UltraButton ultraButton2;

	private UltraLabel lblAmount;

	private Label label13;

	private UltraLabel lblPrice;

	private Label label14;

	private UltraLabel lblAnalysisQty;

	private UltraTextEditor txtAnalysisQty;

	private Label label11;

	private Label lblWRate;

	private Label label6;

	private Label lblMRate;

	private Label label5;

	private Label lblERate;

	private Label Label100;

	private Label lblLRate;

	private Label label3;

	private UltraLabel lblUnit;

	private Label label12;

	private UltraLabel lblCName;

	private UltraLabel lblPccesCode;

	private Label label2;

	private Label label1;

	private Panel panel1;

	private UltraButton BtnLevelUp;

	private UltraCheckEditor chkReCalcu;

	private UltraButton BtnAdjust;

	private UltraTextEditor txtPrice;

	private Label label7;

	private SaveFileDialog saveFileDialog1;

	private UltraButton ultraButton1;

	private UltraButton ultraButton4;

	private UltraButton ultraButton5;

	private Panel pnlAdvance;

	private OpenFileDialog openFileDialog1;

	private UltraButton ultraButton6;

	private UltraProgressBar ultraProgressBar1;

	private Panel pnlInfo;

	private UltraTabControl TabCtrl_Info;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private GridBudget c1FlexGrid1;

	private UltraTabPageControl Tab_A;

	private UltraTabPageControl Tab_B;

	private Panel panel6;

	private UltraButton ultraButton7;

	private UltraLabel ultraLabel1;

	private Panel panel7;

	private UltraButton ultraButton8;

	private UltraLabel ultraLabel2;

	private Panel panel8;

	private UltraButton BtnSaveIR;

	public GridMrsBase gridMrsBase2;

	private UltraButton ultraButton9;

	private UltraButton ultraButton10;

	private System.Windows.Forms.ToolTip toolTip1;

	private UltraStatusBar StatusBar2;

	private UltraStatusBar StatusBar1;

	private UltraStatusBar ultraStatusBar1;

	private Splitter splitter1;

	private UltraCombo cbHistoryWorkRate;

	private UltraCombo cboSubItemQtyAmt;

	private ContextMenuStrip contextMenuStripGridBase2;

	private ToolStripMenuItem toolStripMenuItemBudgetChangeHistory;

	private int iAfterChangeCol = 0;

	private bool IsGoIntoBeforeEdit = false;

	private Recost RC1;

	private int iTextBeamPos = 0;

	private Control Cntrl1;

	private FormSymbol Frm = new FormSymbol();

	private DataTable DT_IR_Temp = new DataTable();

	private string F_CurrentDBName = "";

	private bool F_IsSBID = false;

	private string sInsertCallerMenu = "";

	private string srcProjectCode = "";

	private ArrayList GlobalSelItems = new ArrayList();

	private string MoveUpDownFlag = "";

	private bool IsAllowRepeatItem = false;

	private string sBindFlag = "NORMAL";

	private DataSet DS1 = new DataSet();

	private DataTable DT1 = new DataTable();

	private DataTable DT_MultiDBTransfer = new DataTable();

	private ArrayList InsertList = new ArrayList();

	private string F_PasteSource = "BREAKDOWN";

	private string F_UserID;

	private bool F_IsUseIR = false;

	private PccesFormAction F_ActionName;

	private string F_ProjectCode = "";

	private string F_CallerFormName = "";

	private FormStatus FORM_STATUS = FormStatus.Iinitial;

	private string AppLocation = "";

	private int CurrentRow = 0;

	private int F_Issue = -1;

	private int F_NewChildPubCode = -1;

	private decimal F_NewChildCost = 0m;

	private decimal F_NewChildRate = 0m;

	private string FORM_TITLE = CommonMethods.GetFormTypeTitle(FormType.MrsBaseAnalysis);

	private int GridCols = 15;

	private object[,] GridColsSquence;

	private DataTable DT_Upper = new DataTable();

	private DataTable dtProjMrsB = new DataTable();

	private int parentPubCode = 0;

	private int iLayer = 1;

	private SortedList saLayer = new SortedList();

	private SortedList saLayerCostDec = new SortedList();

	private ArrayList aaLayer = new ArrayList();

	private int iCostDigital = 0;

	private string F_chgCount;

	private string F_CallType = "";

	private bool F_IsLockAn = false;

	private bool F_IsLockAnalysisQtyL = false;

	private bool F_IsLockAnalysisQtyE = false;

	private bool F_IsLockAnalysisQtyM = false;

	private bool F_IsLockAnalysisQtyW = false;

	private int F_MainQty = 0;

	private int F_MainCst = 0;

	private int F_MainAmt = 0;

	private int F_AnaQty = 0;

	private int F_AnaCst = 0;

	private int F_AnaAmt = 0;

	private int iQty = 0;

	private int iCst = 0;

	private int iAmt = 0;

	private DataTable ldt_Analysis = new DataTable();

	private bool F_Istemplate = false;

	private bool F_IsSurName = false;

	private string F_DoubleET = "";

	private double F_DoubleETCost = 0.0;

	private DataSet dsPwrSet;

	private bool LastRowIsOne4Item = false;

	private string CompanyDBName = string.Empty;

	private bool inBeforeEdit = false;

	private bool ContractApproved = false;

	private bool EnableCOMS = SysConfig.SysComsEnable;

	private bool F_IsLocked = false;

	private bool EnableNewCalculateCost = false;

	private MrsCalculate theMrsCalculate = null;

	private FormSys_G_Info1 FM_INFO = null;

	public object[,] _GridColsSquenceInAnalysis
	{
		get
		{
			return GridColsSquence;
		}
		set
		{
			GridColsSquence = value;
		}
	}

	public bool _IsLocked
	{
		get
		{
			return F_IsLocked;
		}
		set
		{
			F_IsLocked = value;
		}
	}

	public int _Issue
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

	public bool _IsSBID
	{
		get
		{
			return F_IsSBID;
		}
		set
		{
			F_IsSBID = value;
		}
	}

	public string _PasteSource
	{
		get
		{
			return F_PasteSource;
		}
		set
		{
			F_PasteSource = value;
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

	public bool _IsUseIR
	{
		get
		{
			return F_IsUseIR;
		}
		set
		{
			F_IsUseIR = value;
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

	public string CallerFormName
	{
		get
		{
			return F_CallerFormName;
		}
		set
		{
			F_CallerFormName = value;
		}
	}

	public int NewChildPubCode
	{
		set
		{
			F_NewChildPubCode = value;
		}
	}

	public decimal NewChildCost
	{
		set
		{
			F_NewChildCost = value;
		}
	}

	public decimal NewChildRate
	{
		set
		{
			F_NewChildRate = value;
		}
	}

	public int PubCode
	{
		get
		{
			return parentPubCode;
		}
		set
		{
			parentPubCode = value;
		}
	}

	public string _CallType
	{
		get
		{
			return F_CallType;
		}
		set
		{
			F_CallType = value;
		}
	}

	public int _iCostDigital
	{
		get
		{
			return iCostDigital;
		}
		set
		{
			iCostDigital = value;
		}
	}

	public string _chgCount
	{
		get
		{
			return F_chgCount;
		}
		set
		{
			F_chgCount = value;
		}
	}

	public bool _Istemplate
	{
		get
		{
			return F_Istemplate;
		}
		set
		{
			F_Istemplate = value;
		}
	}

	public bool _IsSurName
	{
		get
		{
			return F_IsSurName;
		}
		set
		{
			F_IsSurName = value;
		}
	}

	public bool _IsLockAn
	{
		get
		{
			return F_IsLockAn;
		}
		set
		{
			F_IsLockAn = value;
		}
	}

	public string _CompanyDBName
	{
		get
		{
			return CompanyDBName;
		}
		set
		{
			CompanyDBName = value;
		}
	}

	public bool _ContractApproved
	{
		get
		{
			return ContractApproved;
		}
		set
		{
			ContractApproved = value;
		}
	}

	public string _IsLockAnalysLEMWQty
	{
		get
		{
			return (F_IsLockAnalysisQtyL ? "1" : "0") + (F_IsLockAnalysisQtyE ? "1" : "0") + (F_IsLockAnalysisQtyM ? "1" : "0") + (F_IsLockAnalysisQtyW ? "1" : "0");
		}
		set
		{
			if (value.Length == 4)
			{
				F_IsLockAnalysisQtyL = value[0] == '1';
				F_IsLockAnalysisQtyE = value[1] == '1';
				F_IsLockAnalysisQtyM = value[2] == '1';
				F_IsLockAnalysisQtyW = value[3] == '1';
			}
		}
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.MrsBase.FormMrsBaseBreakdown));
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel3 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("", -1);
		Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ProjectCodeName", 0);
		Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Contractor", 1);
		Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("WorkRate", 2);
		Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StartDate", 3);
		Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FinishDate", 4);
		Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Type", 5);
		Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("", -1);
		Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.Appearance appearance56 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel4 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel5 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel6 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel7 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance57 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance58 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance59 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance60 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance61 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance62 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance63 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("UltraToolbar1");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar2 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("HotBar1");
		Infragistics.Win.Appearance appearance64 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool1 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("PopupMenuNew");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuEdit");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelete");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuTool_Up");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuTool_Down");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool5 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuTool_ReCal_Small");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool2 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuPop_Use");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool3 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuPop_SendBack");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool6 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuQTS_Caller");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool7 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuTool_QTS");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool8 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuUseAdjCost");
		Infragistics.Win.Appearance appearance65 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance66 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupControlContainerTool popupControlContainerTool1 = new Infragistics.Win.UltraWinToolbars.PopupControlContainerTool("PopupMenuCalculator");
		Infragistics.Win.Appearance appearance67 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool4 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("PopupMenu1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool9 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCut");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool10 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCopy");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool11 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuPaste");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool12 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCopyforDetail");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool5 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("PopupMenuNew");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool13 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuEdit");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool14 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCopyToNew");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool15 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelete");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool16 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuIRSet");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool17 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuIRCopy");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool18 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuIRPaste");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool19 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuAnalysis");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool6 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuPop_Use");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool7 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuPop_SendBack");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool20 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuQryParent");
		Infragistics.Win.UltraWinToolbars.PopupControlContainerTool popupControlContainerTool2 = new Infragistics.Win.UltraWinToolbars.PopupControlContainerTool("GetHistoryWorkRate");
		Infragistics.Win.UltraWinToolbars.PopupControlContainerTool popupControlContainerTool3 = new Infragistics.Win.UltraWinToolbars.PopupControlContainerTool("GetSubItemQtyAmt");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool21 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCut");
		Infragistics.Win.Appearance appearance68 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool22 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCopy");
		Infragistics.Win.Appearance appearance69 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool23 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuPaste");
		Infragistics.Win.Appearance appearance70 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool8 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("PopupMenuNew");
		Infragistics.Win.Appearance appearance71 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool24 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuNewItem");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool25 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuPickFromProj");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool26 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuPickItem");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool27 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuAnalysis");
		Infragistics.Win.Appearance appearance72 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool28 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuNewItem");
		Infragistics.Win.Appearance appearance73 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool29 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuPickItem");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool30 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuEdit");
		Infragistics.Win.Appearance appearance74 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool31 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelete");
		Infragistics.Win.Appearance appearance75 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool32 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuTool_Up");
		Infragistics.Win.Appearance appearance76 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance77 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool33 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuTool_Down");
		Infragistics.Win.Appearance appearance78 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool34 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuTool_ReCal_Small");
		Infragistics.Win.Appearance appearance79 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool35 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuTool_QTS");
		Infragistics.Win.Appearance appearance80 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool9 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuPop_Use");
		Infragistics.Win.Appearance appearance81 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool36 = new Infragistics.Win.UltraWinToolbars.ButtonTool("UseSingle");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool37 = new Infragistics.Win.UltraWinToolbars.ButtonTool("UseMulti");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool38 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuUseMrsCost");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool10 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuPop_SendBack");
		Infragistics.Win.Appearance appearance82 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool39 = new Infragistics.Win.UltraWinToolbars.ButtonTool("SendSingle");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool40 = new Infragistics.Win.UltraWinToolbars.ButtonTool("SendMulti");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool41 = new Infragistics.Win.UltraWinToolbars.ButtonTool("UseSingle");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool42 = new Infragistics.Win.UltraWinToolbars.ButtonTool("UseMulti");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool43 = new Infragistics.Win.UltraWinToolbars.ButtonTool("SendSingle");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool44 = new Infragistics.Win.UltraWinToolbars.ButtonTool("SendMulti");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool45 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuPickFromProj");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool46 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuIRSet");
		Infragistics.Win.Appearance appearance83 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool47 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuQTS_Caller");
		Infragistics.Win.Appearance appearance84 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool48 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCopyToNew");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool49 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuIRCopy");
		Infragistics.Win.Appearance appearance85 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool50 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuIRPaste");
		Infragistics.Win.Appearance appearance86 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool51 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuUseMrsCost");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool52 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuQryParent");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool53 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuUseAdjCost");
		Infragistics.Win.Appearance appearance87 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupControlContainerTool popupControlContainerTool4 = new Infragistics.Win.UltraWinToolbars.PopupControlContainerTool("GetHistoryWorkRate");
		Infragistics.Win.UltraWinToolbars.PopupControlContainerTool popupControlContainerTool5 = new Infragistics.Win.UltraWinToolbars.PopupControlContainerTool("GetSubItemQtyAmt");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool54 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCopyforDetail");
		Infragistics.Win.Appearance appearance88 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance89 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance90 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance91 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance92 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance93 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance94 = new Infragistics.Win.Appearance();
		this.Tab_A = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.c1FlexGrid1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.StatusBar1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.panel8 = new System.Windows.Forms.Panel();
		this.BtnSaveIR = new Infragistics.Win.Misc.UltraButton();
		this.panel6 = new System.Windows.Forms.Panel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraButton9 = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton7 = new Infragistics.Win.Misc.UltraButton();
		this.Tab_B = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.gridMrsBase2 = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.StatusBar2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.panel7 = new System.Windows.Forms.Panel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraButton10 = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton8 = new Infragistics.Win.Misc.UltraButton();
		this.cbHistoryWorkRate = new Infragistics.Win.UltraWinGrid.UltraCombo();
		this.cboSubItemQtyAmt = new Infragistics.Win.UltraWinGrid.UltraCombo();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.c1CommandLink10 = new C1.Win.C1Command.C1CommandLink();
		this.c1CommandLink11 = new C1.Win.C1Command.C1CommandLink();
		this.c1CommandLink13 = new C1.Win.C1Command.C1CommandLink();
		this.panel2 = new System.Windows.Forms.Panel();
		this.ultraButton6 = new Infragistics.Win.Misc.UltraButton();
		this.pnlAdvance = new System.Windows.Forms.Panel();
		this.ultraButton5 = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton4 = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.label7 = new System.Windows.Forms.Label();
		this.ultraButton3 = new Infragistics.Win.Misc.UltraButton();
		this.ultraProgressBar1 = new Infragistics.Win.UltraWinProgressBar.UltraProgressBar();
		this.panel3 = new System.Windows.Forms.Panel();
		this.gridMrsBase1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.splitter1 = new System.Windows.Forms.Splitter();
		this.pnlInfo = new System.Windows.Forms.Panel();
		this.TabCtrl_Info = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.ultraStatusBar1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.chkReCalcu = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.BtnLevelUp = new Infragistics.Win.Misc.UltraButton();
		this.lblLevelNo = new Infragistics.Win.Misc.UltraLabel();
		this.axSSPanel1 = new AxThreed.AxSSPanel();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.ultraToolbarsManager1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
		this.imageList3 = new System.Windows.Forms.ImageList(this.components);
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.panel4 = new System.Windows.Forms.Panel();
		this.BtnAdjust = new Infragistics.Win.Misc.UltraButton();
		this.lblPrice = new Infragistics.Win.Misc.UltraLabel();
		this.txtPrice = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.lblMRate = new System.Windows.Forms.Label();
		this.lblAmount = new Infragistics.Win.Misc.UltraLabel();
		this.ultraButton2 = new Infragistics.Win.Misc.UltraButton();
		this.label13 = new System.Windows.Forms.Label();
		this.label14 = new System.Windows.Forms.Label();
		this.lblAnalysisQty = new Infragistics.Win.Misc.UltraLabel();
		this.txtAnalysisQty = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.label11 = new System.Windows.Forms.Label();
		this.lblWRate = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.lblERate = new System.Windows.Forms.Label();
		this.Label100 = new System.Windows.Forms.Label();
		this.lblLRate = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.lblUnit = new Infragistics.Win.Misc.UltraLabel();
		this.label12 = new System.Windows.Forms.Label();
		this.lblCName = new Infragistics.Win.Misc.UltraLabel();
		this.lblPccesCode = new Infragistics.Win.Misc.UltraLabel();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.panel1 = new System.Windows.Forms.Panel();
		this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
		this.c1XLBook1 = new C1.C1Excel.C1XLBook();
		this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
		this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.toolStripMenuItemBudgetChangeHistory = new System.Windows.Forms.ToolStripMenuItem();
		this.contextMenuStripGridBase2 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.Tab_A.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid1).BeginInit();
		this.panel8.SuspendLayout();
		this.panel6.SuspendLayout();
		this.Tab_B.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridMrsBase2).BeginInit();
		this.panel7.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cbHistoryWorkRate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubItemQtyAmt).BeginInit();
		this.panel2.SuspendLayout();
		this.pnlAdvance.SuspendLayout();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridMrsBase1).BeginInit();
		this.pnlInfo.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TabCtrl_Info).BeginInit();
		this.TabCtrl_Info.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.axSSPanel1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).BeginInit();
		this.panel4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtPrice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAnalysisQty).BeginInit();
		this.panel1.SuspendLayout();
		this.contextMenuStripGridBase2.SuspendLayout();
		base.SuspendLayout();
		this.Tab_A.Controls.Add(this.c1FlexGrid1);
		this.Tab_A.Controls.Add(this.StatusBar1);
		this.Tab_A.Controls.Add(this.panel8);
		this.Tab_A.Controls.Add(this.panel6);
		this.Tab_A.Location = new System.Drawing.Point(0, 0);
		this.Tab_A.Name = "Tab_A";
		this.Tab_A.Size = new System.Drawing.Size(250, 283);
		this.c1FlexGrid1._ExcelFileName = "";
		this.c1FlexGrid1._ExcelSheeName = "";
		this.c1FlexGrid1._IsOpenExcelAfterExport = false;
		this.c1FlexGrid1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
		this.c1FlexGrid1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.c1FlexGrid1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.c1FlexGrid1.ColumnInfo = resources.GetString("c1FlexGrid1.ColumnInfo");
		this.c1FlexGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.c1FlexGrid1.ExtendLastCol = true;
		this.c1FlexGrid1.ForeColor = System.Drawing.SystemColors.WindowText;
		this.c1FlexGrid1.Location = new System.Drawing.Point(0, 24);
		this.c1FlexGrid1.Name = "c1FlexGrid1";
		this.c1FlexGrid1.Rows.Count = 1;
		this.c1FlexGrid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.c1FlexGrid1.ShowToolTipOnNarrowColumn = true;
		this.c1FlexGrid1.Size = new System.Drawing.Size(250, 202);
		this.c1FlexGrid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("c1FlexGrid1.Styles"));
		this.c1FlexGrid1.TabIndex = 5;
		appearance1.FontData.SizeInPoints = 9f;
		this.StatusBar1.Appearance = appearance1;
		this.StatusBar1.Location = new System.Drawing.Point(0, 226);
		this.StatusBar1.Name = "StatusBar1";
		this.StatusBar1.Padding = new Infragistics.Win.UltraWinStatusBar.UIElementMargins(1, 2, 1, 1);
		appearance2.TextVAlign = Infragistics.Win.VAlign.Middle;
		ultraStatusPanel1.Appearance = appearance2;
		ultraStatusPanel1.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel1.Key = "Rows";
		ultraStatusPanel1.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		ultraStatusPanel1.Text = "資料筆數:";
		ultraStatusPanel1.Width = 70;
		ultraStatusPanel2.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		this.StatusBar1.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[2] { ultraStatusPanel1, ultraStatusPanel2 });
		this.StatusBar1.Size = new System.Drawing.Size(250, 25);
		this.StatusBar1.TabIndex = 13;
		this.panel8.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
		this.panel8.Controls.Add(this.BtnSaveIR);
		this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel8.Location = new System.Drawing.Point(0, 251);
		this.panel8.Name = "panel8";
		this.panel8.Size = new System.Drawing.Size(250, 32);
		this.panel8.TabIndex = 7;
		this.BtnSaveIR.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance3.Image = resources.GetObject("appearance3.Image");
		appearance3.TextVAlign = Infragistics.Win.VAlign.Top;
		this.BtnSaveIR.Appearance = appearance3;
		this.BtnSaveIR.BackColor = System.Drawing.SystemColors.Control;
		this.BtnSaveIR.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.BtnSaveIR.Location = new System.Drawing.Point(80, 2);
		this.BtnSaveIR.Name = "BtnSaveIR";
		this.BtnSaveIR.Size = new System.Drawing.Size(96, 28);
		this.BtnSaveIR.SupportThemes = false;
		this.BtnSaveIR.TabIndex = 0;
		this.BtnSaveIR.Text = "IR 儲存";
		this.BtnSaveIR.Click += new System.EventHandler(BtnSaveIR_Click);
		this.panel6.Controls.Add(this.ultraLabel1);
		this.panel6.Controls.Add(this.ultraButton9);
		this.panel6.Controls.Add(this.ultraButton7);
		this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel6.Location = new System.Drawing.Point(0, 0);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(250, 24);
		this.panel6.TabIndex = 6;
		appearance4.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel1.Appearance = appearance4;
		this.ultraLabel1.BackColor = System.Drawing.SystemColors.Control;
		this.ultraLabel1.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.ultraLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ultraLabel1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel1.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Padding = new System.Drawing.Size(5, 0);
		this.ultraLabel1.Size = new System.Drawing.Size(208, 24);
		this.ultraLabel1.TabIndex = 1;
		this.ultraLabel1.Text = "IR 項目";
		appearance5.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance5.Image = resources.GetObject("appearance5.Image");
		appearance5.ImageHAlign = Infragistics.Win.HAlign.Center;
		this.ultraButton9.Appearance = appearance5;
		this.ultraButton9.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton9.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.ultraButton9.Dock = System.Windows.Forms.DockStyle.Right;
		this.ultraButton9.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ultraButton9.Location = new System.Drawing.Point(208, 0);
		this.ultraButton9.Name = "ultraButton9";
		this.ultraButton9.ShowFocusRect = false;
		this.ultraButton9.ShowOutline = false;
		this.ultraButton9.Size = new System.Drawing.Size(22, 24);
		this.ultraButton9.SupportThemes = false;
		this.ultraButton9.TabIndex = 2;
		this.toolTip1.SetToolTip(this.ultraButton9, "IR 列表匯出至EXCEL");
		this.ultraButton9.Click += new System.EventHandler(ultraButton9_Click);
		appearance6.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		this.ultraButton7.Appearance = appearance6;
		this.ultraButton7.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton7.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.ultraButton7.Dock = System.Windows.Forms.DockStyle.Right;
		this.ultraButton7.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ultraButton7.Location = new System.Drawing.Point(230, 0);
		this.ultraButton7.Name = "ultraButton7";
		this.ultraButton7.ShowFocusRect = false;
		this.ultraButton7.ShowOutline = false;
		this.ultraButton7.Size = new System.Drawing.Size(20, 24);
		this.ultraButton7.SupportThemes = false;
		this.ultraButton7.TabIndex = 0;
		this.ultraButton7.Text = "X";
		this.ultraButton7.Click += new System.EventHandler(ultraButton7_Click);
		this.Tab_B.Controls.Add(this.gridMrsBase2);
		this.Tab_B.Controls.Add(this.StatusBar2);
		this.Tab_B.Controls.Add(this.panel7);
		this.Tab_B.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_B.Name = "Tab_B";
		this.Tab_B.Size = new System.Drawing.Size(250, 283);
		this.gridMrsBase2._ExcelFileName = "";
		this.gridMrsBase2._ExcelSheeName = "";
		this.gridMrsBase2._IsOpenExcelAfterExport = false;
		this.gridMrsBase2.AllowEditing = false;
		this.gridMrsBase2.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
		this.gridMrsBase2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridMrsBase2.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
		this.gridMrsBase2.ColumnInfo = resources.GetString("gridMrsBase2.ColumnInfo");
		this.gridMrsBase2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridMrsBase2.ExtendLastCol = true;
		this.gridMrsBase2.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridMrsBase2.ForeColor = System.Drawing.Color.Black;
		this.gridMrsBase2.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.gridMrsBase2.IsProcessUndo = false;
		this.gridMrsBase2.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.gridMrsBase2.Location = new System.Drawing.Point(0, 24);
		this.gridMrsBase2.Name = "gridMrsBase2";
		this.gridMrsBase2.Rows.Count = 1;
		this.gridMrsBase2.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.gridMrsBase2.ShowCursor = true;
		this.gridMrsBase2.ShowToolTipOnNarrowColumn = true;
		this.gridMrsBase2.Size = new System.Drawing.Size(250, 231);
		this.gridMrsBase2.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridMrsBase2.Styles"));
		this.gridMrsBase2.TabIndex = 11;
		this.gridMrsBase2.UndoMax = 10;
		this.gridMrsBase2.MouseDown += new System.Windows.Forms.MouseEventHandler(gridMrsBase2_MouseDown);
		appearance7.FontData.SizeInPoints = 11f;
		this.StatusBar2.Appearance = appearance7;
		this.StatusBar2.Location = new System.Drawing.Point(0, 255);
		this.StatusBar2.Name = "StatusBar2";
		this.StatusBar2.Padding = new Infragistics.Win.UltraWinStatusBar.UIElementMargins(1, 2, 1, 1);
		appearance8.TextVAlign = Infragistics.Win.VAlign.Middle;
		ultraStatusPanel3.Appearance = appearance8;
		ultraStatusPanel3.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel3.Key = "Rows";
		ultraStatusPanel3.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		ultraStatusPanel3.Text = " 資料筆數:";
		this.StatusBar2.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[1] { ultraStatusPanel3 });
		this.StatusBar2.Size = new System.Drawing.Size(250, 28);
		this.StatusBar2.TabIndex = 12;
		this.panel7.Controls.Add(this.ultraLabel2);
		this.panel7.Controls.Add(this.ultraButton10);
		this.panel7.Controls.Add(this.ultraButton8);
		this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel7.Location = new System.Drawing.Point(0, 0);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(250, 24);
		this.panel7.TabIndex = 7;
		appearance9.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel2.Appearance = appearance9;
		this.ultraLabel2.BackColor = System.Drawing.SystemColors.Control;
		this.ultraLabel2.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.ultraLabel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ultraLabel2.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel2.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Padding = new System.Drawing.Size(5, 0);
		this.ultraLabel2.Size = new System.Drawing.Size(208, 24);
		this.ultraLabel2.TabIndex = 1;
		this.ultraLabel2.Text = "其他引用的單價分析項";
		appearance10.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance10.Image = resources.GetObject("appearance10.Image");
		appearance10.ImageHAlign = Infragistics.Win.HAlign.Center;
		this.ultraButton10.Appearance = appearance10;
		this.ultraButton10.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton10.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.ultraButton10.Dock = System.Windows.Forms.DockStyle.Right;
		this.ultraButton10.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ultraButton10.Location = new System.Drawing.Point(208, 0);
		this.ultraButton10.Name = "ultraButton10";
		this.ultraButton10.ShowFocusRect = false;
		this.ultraButton10.ShowOutline = false;
		this.ultraButton10.Size = new System.Drawing.Size(22, 24);
		this.ultraButton10.SupportThemes = false;
		this.ultraButton10.TabIndex = 2;
		this.toolTip1.SetToolTip(this.ultraButton10, "單價分析引用結果匯出至EXCEL");
		this.ultraButton10.Click += new System.EventHandler(ultraButton10_Click);
		appearance11.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		this.ultraButton8.Appearance = appearance11;
		this.ultraButton8.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton8.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.ultraButton8.Dock = System.Windows.Forms.DockStyle.Right;
		this.ultraButton8.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ultraButton8.Location = new System.Drawing.Point(230, 0);
		this.ultraButton8.Name = "ultraButton8";
		this.ultraButton8.ShowFocusRect = false;
		this.ultraButton8.ShowOutline = false;
		this.ultraButton8.Size = new System.Drawing.Size(20, 24);
		this.ultraButton8.SupportThemes = false;
		this.ultraButton8.TabIndex = 0;
		this.ultraButton8.Text = "X";
		this.ultraButton8.Click += new System.EventHandler(ultraButton8_Click);
		this.cbHistoryWorkRate.AutoEdit = false;
		this.cbHistoryWorkRate.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
		ultraGridColumn1.Header.Caption = "來源專案";
		ultraGridColumn1.Width = 90;
		ultraGridColumn2.Header.Caption = "廠商名稱";
		ultraGridColumn2.Width = 100;
		appearance12.TextHAlign = Infragistics.Win.HAlign.Right;
		ultraGridColumn3.CellAppearance = appearance12;
		ultraGridColumn3.Header.Caption = "功率";
		ultraGridColumn3.Width = 80;
		appearance13.TextHAlign = Infragistics.Win.HAlign.Center;
		ultraGridColumn4.CellAppearance = appearance13;
		ultraGridColumn4.Header.Caption = "開工日期";
		ultraGridColumn4.Width = 65;
		appearance14.TextHAlign = Infragistics.Win.HAlign.Center;
		ultraGridColumn5.CellAppearance = appearance14;
		ultraGridColumn5.Header.Caption = "完工日期";
		ultraGridColumn5.Width = 65;
		ultraGridColumn6.Header.Caption = "來源類型";
		ultraGridColumn6.Width = 60;
		ultraGridBand1.Columns.Add(ultraGridColumn1);
		ultraGridBand1.Columns.Add(ultraGridColumn2);
		ultraGridBand1.Columns.Add(ultraGridColumn3);
		ultraGridBand1.Columns.Add(ultraGridColumn4);
		ultraGridBand1.Columns.Add(ultraGridColumn5);
		ultraGridBand1.Columns.Add(ultraGridColumn6);
		ultraGridBand1.Override.TipStyleCell = Infragistics.Win.UltraWinGrid.TipStyle.Show;
		ultraGridBand1.Override.TipStyleScroll = Infragistics.Win.UltraWinGrid.TipStyle.Show;
		ultraGridBand1.UseRowLayout = true;
		this.cbHistoryWorkRate.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
		this.cbHistoryWorkRate.DisplayMember = "";
		this.cbHistoryWorkRate.Location = new System.Drawing.Point(95, 124);
		this.cbHistoryWorkRate.MaxDropDownItems = 20;
		this.cbHistoryWorkRate.Name = "cbHistoryWorkRate";
		this.cbHistoryWorkRate.Size = new System.Drawing.Size(272, 24);
		this.cbHistoryWorkRate.TabIndex = 19;
		this.cbHistoryWorkRate.Text = "請下拉，挑選工項數量";
		this.cbHistoryWorkRate.ValueMember = "WorkRate";
		this.cbHistoryWorkRate.AfterCloseUp += new System.EventHandler(cbHistoryWorkRate_AfterCloseUp);
		this.cboSubItemQtyAmt.AutoEdit = false;
		this.cboSubItemQtyAmt.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
		ultraGridBand2.Override.TipStyleCell = Infragistics.Win.UltraWinGrid.TipStyle.Show;
		ultraGridBand2.Override.TipStyleScroll = Infragistics.Win.UltraWinGrid.TipStyle.Show;
		ultraGridBand2.UseRowLayout = true;
		this.cboSubItemQtyAmt.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
		this.cboSubItemQtyAmt.DisplayMember = "";
		this.cboSubItemQtyAmt.Location = new System.Drawing.Point(256, 251);
		this.cboSubItemQtyAmt.MaxDropDownItems = 20;
		this.cboSubItemQtyAmt.Name = "cboSubItemQtyAmt";
		this.cboSubItemQtyAmt.Size = new System.Drawing.Size(272, 24);
		this.cboSubItemQtyAmt.TabIndex = 20;
		this.cboSubItemQtyAmt.Text = "請下拉，參考預算/估驗資訊";
		this.cboSubItemQtyAmt.ValueMember = "";
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
		this.imageList1.Images.SetKeyName(0, "");
		this.imageList1.Images.SetKeyName(1, "");
		this.imageList1.Images.SetKeyName(2, "");
		this.imageList1.Images.SetKeyName(3, "");
		this.panel2.Controls.Add(this.ultraButton6);
		this.panel2.Controls.Add(this.pnlAdvance);
		this.panel2.Controls.Add(this.label7);
		this.panel2.Controls.Add(this.ultraButton3);
		this.panel2.Controls.Add(this.ultraProgressBar1);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 498);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(785, 36);
		this.panel2.TabIndex = 1;
		this.panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(FormMrsBaseBreakdown_MouseDown);
		this.ultraButton6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		appearance15.Image = resources.GetObject("appearance15.Image");
		appearance15.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton6.Appearance = appearance15;
		this.ultraButton6.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.ultraButton6.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton6.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraButton6.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton6.Location = new System.Drawing.Point(557, 5);
		this.ultraButton6.Name = "ultraButton6";
		this.ultraButton6.ShowFocusRect = false;
		this.ultraButton6.ShowOutline = false;
		this.ultraButton6.Size = new System.Drawing.Size(128, 28);
		this.ultraButton6.SupportThemes = false;
		this.ultraButton6.TabIndex = 13;
		this.ultraButton6.Text = "檢查IR設定值";
		this.ultraButton6.Click += new System.EventHandler(ultraButton6_Click);
		this.pnlAdvance.Controls.Add(this.ultraButton5);
		this.pnlAdvance.Controls.Add(this.ultraButton4);
		this.pnlAdvance.Controls.Add(this.ultraButton1);
		this.pnlAdvance.Dock = System.Windows.Forms.DockStyle.Left;
		this.pnlAdvance.Location = new System.Drawing.Point(0, 0);
		this.pnlAdvance.Name = "pnlAdvance";
		this.pnlAdvance.Size = new System.Drawing.Size(252, 36);
		this.pnlAdvance.TabIndex = 10;
		appearance16.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton5.Appearance = appearance16;
		this.ultraButton5.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.ultraButton5.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.ultraButton5.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraButton5.Location = new System.Drawing.Point(79, 4);
		this.ultraButton5.Name = "ultraButton5";
		this.ultraButton5.ShowFocusRect = false;
		this.ultraButton5.ShowOutline = false;
		this.ultraButton5.Size = new System.Drawing.Size(68, 24);
		this.ultraButton5.SupportThemes = false;
		this.ultraButton5.TabIndex = 12;
		this.ultraButton5.Text = "匯入EXCEL";
		this.ultraButton5.Click += new System.EventHandler(ultraButton5_Click);
		appearance17.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton4.Appearance = appearance17;
		this.ultraButton4.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.ultraButton4.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.ultraButton4.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ultraButton4.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraButton4.Location = new System.Drawing.Point(149, 4);
		this.ultraButton4.Name = "ultraButton4";
		this.ultraButton4.ShowFocusRect = false;
		this.ultraButton4.ShowOutline = false;
		this.ultraButton4.Size = new System.Drawing.Size(84, 24);
		this.ultraButton4.SupportThemes = false;
		this.ultraButton4.TabIndex = 11;
		this.ultraButton4.Text = "刪除所有子項";
		this.ultraButton4.Click += new System.EventHandler(ultraButton4_Click);
		appearance18.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton1.Appearance = appearance18;
		this.ultraButton1.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.ultraButton1.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraButton1.Location = new System.Drawing.Point(8, 4);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.ShowFocusRect = false;
		this.ultraButton1.ShowOutline = false;
		this.ultraButton1.Size = new System.Drawing.Size(68, 24);
		this.ultraButton1.SupportThemes = false;
		this.ultraButton1.TabIndex = 10;
		this.ultraButton1.Text = "匯出EXCEL";
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(544, 4);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(71, 15);
		this.label7.TabIndex = 8;
		this.label7.Text = "AfterCol";
		this.label7.Visible = false;
		this.ultraButton3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		appearance19.BackColor = System.Drawing.SystemColors.ControlLightLight;
		appearance19.BackColor2 = System.Drawing.SystemColors.ControlLight;
		appearance19.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance19.Image = resources.GetObject("appearance19.Image");
		appearance19.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton3.Appearance = appearance19;
		this.ultraButton3.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton3.Cursor = System.Windows.Forms.Cursors.Hand;
		this.ultraButton3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ultraButton3.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraButton3.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton3.Location = new System.Drawing.Point(689, 5);
		this.ultraButton3.Name = "ultraButton3";
		this.ultraButton3.ShowFocusRect = false;
		this.ultraButton3.ShowOutline = false;
		this.ultraButton3.Size = new System.Drawing.Size(90, 28);
		this.ultraButton3.SupportThemes = false;
		this.ultraButton3.TabIndex = 5;
		this.ultraButton3.Text = "結  束";
		this.ultraButton3.Click += new System.EventHandler(ultraButton3_Click);
		this.ultraButton3.MouseDown += new System.Windows.Forms.MouseEventHandler(FormMrsBaseBreakdown_MouseDown);
		this.ultraProgressBar1.Location = new System.Drawing.Point(270, 8);
		this.ultraProgressBar1.Name = "ultraProgressBar1";
		this.ultraProgressBar1.Size = new System.Drawing.Size(280, 23);
		this.ultraProgressBar1.TabIndex = 14;
		this.ultraProgressBar1.Text = "[Formatted]";
		this.ultraProgressBar1.Visible = false;
		this.panel3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel3.Controls.Add(this.gridMrsBase1);
		this.panel3.Controls.Add(this.cbHistoryWorkRate);
		this.panel3.Controls.Add(this.splitter1);
		this.panel3.Controls.Add(this.pnlInfo);
		this.panel3.Controls.Add(this.ultraStatusBar1);
		this.panel3.Controls.Add(this.chkReCalcu);
		this.panel3.Controls.Add(this.BtnLevelUp);
		this.panel3.Controls.Add(this.lblLevelNo);
		this.panel3.Controls.Add(this.axSSPanel1);
		this.panel3.ForeColor = System.Drawing.Color.Black;
		this.panel3.Location = new System.Drawing.Point(4, 147);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(776, 338);
		this.panel3.TabIndex = 7;
		this.gridMrsBase1._ExcelFileName = "";
		this.gridMrsBase1._ExcelSheeName = "";
		this.gridMrsBase1._IsOpenExcelAfterExport = false;
		this.gridMrsBase1.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
		this.gridMrsBase1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
		this.gridMrsBase1.AutoResize = false;
		this.gridMrsBase1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridMrsBase1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.gridMrsBase1.ColumnInfo = resources.GetString("gridMrsBase1.ColumnInfo");
		this.ultraToolbarsManager1.SetContextMenuUltra(this.gridMrsBase1, "PopupMenu1");
		this.gridMrsBase1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridMrsBase1.ExtendLastCol = true;
		this.gridMrsBase1.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridMrsBase1.ForeColor = System.Drawing.SystemColors.WindowText;
		this.gridMrsBase1.IsProcessUndo = false;
		this.gridMrsBase1.Location = new System.Drawing.Point(0, 30);
		this.gridMrsBase1.Name = "gridMrsBase1";
		this.gridMrsBase1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.gridMrsBase1.ShowCursor = true;
		this.gridMrsBase1.ShowToolTipOnNarrowColumn = true;
		this.gridMrsBase1.Size = new System.Drawing.Size(520, 283);
		this.gridMrsBase1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridMrsBase1.Styles"));
		this.gridMrsBase1.TabIndex = 7;
		this.gridMrsBase1.UndoMax = 5;
		this.gridMrsBase1.AfterSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(gridMrsBase1_AfterSelChange);
		this.gridMrsBase1.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(gridMrsBase1_AfterRowColChange);
		this.gridMrsBase1.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(gridMrsBase1_StartEdit);
		this.gridMrsBase1.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(gridMrsBase1_AfterEdit);
		this.gridMrsBase1.KeyDown += new System.Windows.Forms.KeyEventHandler(gridMrsBase1_KeyDown);
		this.gridMrsBase1.MouseDown += new System.Windows.Forms.MouseEventHandler(FormMrsBaseBreakdown_MouseDown);
		this.gridMrsBase1.MouseMove += new System.Windows.Forms.MouseEventHandler(gridMrsBase1_MouseMove);
		this.gridMrsBase1.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(gridMrsBase1_BeforeEdit);
		this.gridMrsBase1.DoubleClick += new System.EventHandler(gridMrsBase1_DoubleClick);
		this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
		this.splitter1.Location = new System.Drawing.Point(520, 30);
		this.splitter1.Name = "splitter1";
		this.splitter1.Size = new System.Drawing.Size(4, 283);
		this.splitter1.TabIndex = 14;
		this.splitter1.TabStop = false;
		this.pnlInfo.Controls.Add(this.TabCtrl_Info);
		this.pnlInfo.Dock = System.Windows.Forms.DockStyle.Right;
		this.pnlInfo.Location = new System.Drawing.Point(524, 30);
		this.pnlInfo.Name = "pnlInfo";
		this.pnlInfo.Size = new System.Drawing.Size(250, 283);
		this.pnlInfo.TabIndex = 12;
		this.TabCtrl_Info.BackColor = System.Drawing.Color.White;
		this.TabCtrl_Info.Controls.Add(this.ultraTabSharedControlsPage1);
		this.TabCtrl_Info.Controls.Add(this.Tab_A);
		this.TabCtrl_Info.Controls.Add(this.Tab_B);
		this.TabCtrl_Info.Dock = System.Windows.Forms.DockStyle.Fill;
		this.TabCtrl_Info.Location = new System.Drawing.Point(0, 0);
		this.TabCtrl_Info.Name = "TabCtrl_Info";
		this.TabCtrl_Info.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.TabCtrl_Info.Size = new System.Drawing.Size(250, 283);
		this.TabCtrl_Info.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
		this.TabCtrl_Info.TabIndex = 0;
		ultraTab1.TabPage = this.Tab_A;
		ultraTab1.Text = "tab1";
		ultraTab2.TabPage = this.Tab_B;
		ultraTab2.Text = "tab2";
		this.TabCtrl_Info.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[2] { ultraTab1, ultraTab2 });
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(250, 283);
		appearance56.FontData.SizeInPoints = 9f;
		this.ultraStatusBar1.Appearance = appearance56;
		this.ultraStatusBar1.Location = new System.Drawing.Point(0, 313);
		this.ultraStatusBar1.Name = "ultraStatusBar1";
		ultraStatusPanel4.Width = 200;
		ultraStatusPanel5.Width = 200;
		ultraStatusPanel6.Width = 200;
		ultraStatusPanel7.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		this.ultraStatusBar1.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[4] { ultraStatusPanel4, ultraStatusPanel5, ultraStatusPanel6, ultraStatusPanel7 });
		this.ultraStatusBar1.Size = new System.Drawing.Size(774, 23);
		this.ultraStatusBar1.TabIndex = 13;
		this.chkReCalcu.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance57.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.chkReCalcu.Appearance = appearance57;
		this.chkReCalcu.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.chkReCalcu.Checked = true;
		this.chkReCalcu.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkReCalcu.Location = new System.Drawing.Point(556, 6);
		this.chkReCalcu.Name = "chkReCalcu";
		this.chkReCalcu.Size = new System.Drawing.Size(120, 22);
		this.chkReCalcu.TabIndex = 9;
		this.chkReCalcu.Text = "自動重新小計";
		this.toolTip1.SetToolTip(this.chkReCalcu, "快速鍵：Ctrl+W");
		this.BtnLevelUp.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance58.BackColor = System.Drawing.SystemColors.ControlLightLight;
		appearance58.BackColor2 = System.Drawing.SystemColors.ControlLight;
		appearance58.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance58.Image = resources.GetObject("appearance58.Image");
		appearance58.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnLevelUp.Appearance = appearance58;
		this.BtnLevelUp.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnLevelUp.Cursor = System.Windows.Forms.Cursors.Hand;
		this.BtnLevelUp.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.BtnLevelUp.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnLevelUp.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.BtnLevelUp.Location = new System.Drawing.Point(684, 1);
		this.BtnLevelUp.Name = "BtnLevelUp";
		this.BtnLevelUp.ShowFocusRect = false;
		this.BtnLevelUp.ShowOutline = false;
		this.BtnLevelUp.Size = new System.Drawing.Size(90, 28);
		this.BtnLevelUp.SupportThemes = false;
		this.BtnLevelUp.TabIndex = 8;
		this.BtnLevelUp.Text = "上一層";
		this.toolTip1.SetToolTip(this.BtnLevelUp, "快速鍵：Ctrl+Q");
		this.BtnLevelUp.Click += new System.EventHandler(BtnLevelUp_Click);
		this.lblLevelNo.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.lblLevelNo.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblLevelNo.Location = new System.Drawing.Point(8, 8);
		this.lblLevelNo.Name = "lblLevelNo";
		this.lblLevelNo.Size = new System.Drawing.Size(328, 16);
		this.lblLevelNo.TabIndex = 5;
		this.lblLevelNo.Text = "[第 N 層]";
		this.lblLevelNo.MouseDown += new System.Windows.Forms.MouseEventHandler(FormMrsBaseBreakdown_MouseDown);
		this.axSSPanel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.axSSPanel1.Location = new System.Drawing.Point(0, 0);
		this.axSSPanel1.Name = "axSSPanel1";
		this.axSSPanel1.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("axSSPanel1.OcxState");
		this.axSSPanel1.Size = new System.Drawing.Size(774, 30);
		this.axSSPanel1.TabIndex = 0;
		this.axSSPanel1.MouseDownEvent += new AxThreed.DSSPanelEvents_MouseDownEventHandler(axSSPanel1_MouseDownEvent);
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.White;
		this.imageList2.Images.SetKeyName(0, "");
		this.imageList2.Images.SetKeyName(1, "");
		appearance59.ImageVAlign = Infragistics.Win.VAlign.Top;
		appearance59.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraToolbarsManager1.Appearance = appearance59;
		appearance60.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance60.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.DockAreaAppearance = appearance60;
		this.ultraToolbarsManager1.DockWithinContainer = this;
		this.ultraToolbarsManager1.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraToolbarsManager1.LockToolbars = true;
		appearance61.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance61.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance61.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.MenuSettings.HotTrackAppearance = appearance61;
		appearance62.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance62.BackColor2 = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraToolbarsManager1.MenuSettings.IconAreaAppearance = appearance62;
		appearance63.BackColor = System.Drawing.Color.White;
		appearance63.BackColor2 = System.Drawing.Color.White;
		this.ultraToolbarsManager1.MenuSettings.ToolAppearance = appearance63;
		this.ultraToolbarsManager1.ShowFullMenusDelay = 500;
		this.ultraToolbarsManager1.ShowQuickCustomizeButton = false;
		this.ultraToolbarsManager1.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2003;
		ultraToolbar1.DockedColumn = 0;
		ultraToolbar1.DockedRow = 0;
		ultraToolbar1.FloatingLocation = new System.Drawing.Point(10, 20);
		ultraToolbar1.FloatingSize = new System.Drawing.Size(95, 30);
		ultraToolbar1.Text = "右鍵選單";
		ultraToolbar1.Visible = false;
		ultraToolbar2.DockedColumn = 0;
		ultraToolbar2.DockedRow = 0;
		ultraToolbar2.Settings.CaptionPlacement = Infragistics.Win.TextPlacement.BelowImage;
		appearance64.FontData.Name = "Arial";
		appearance64.ImageVAlign = Infragistics.Win.VAlign.Top;
		appearance64.TextVAlign = Infragistics.Win.VAlign.Bottom;
		ultraToolbar2.Settings.ToolAppearance = appearance64;
		ultraToolbar2.Settings.ToolDisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		ultraToolbar2.Text = "HotBar1";
		buttonTool3.InstanceProps.IsFirstInGroup = true;
		buttonTool5.InstanceProps.IsFirstInGroup = true;
		popupMenuTool2.InstanceProps.IsFirstInGroup = true;
		buttonTool6.InstanceProps.IsFirstInGroup = true;
		buttonTool8.InstanceProps.IsFirstInGroup = true;
		ultraToolbar2.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[11]
		{
			popupMenuTool1, buttonTool1, buttonTool2, buttonTool3, buttonTool4, buttonTool5, popupMenuTool2, popupMenuTool3, buttonTool6, buttonTool7,
			buttonTool8
		});
		this.ultraToolbarsManager1.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[2] { ultraToolbar1, ultraToolbar2 });
		appearance65.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance65.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		appearance65.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		this.ultraToolbarsManager1.ToolbarSettings.Appearance = appearance65;
		this.ultraToolbarsManager1.ToolbarSettings.FillEntireRow = Infragistics.Win.DefaultableBoolean.True;
		appearance66.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance66.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance66.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.ToolbarSettings.HotTrackAppearance = appearance66;
		this.ultraToolbarsManager1.ToolbarSettings.ToolDisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		popupControlContainerTool1.AllowTearaway = true;
		popupControlContainerTool1.DropDownArrowStyle = Infragistics.Win.UltraWinToolbars.DropDownArrowStyle.Standard;
		appearance67.Image = resources.GetObject("appearance35.Image");
		popupControlContainerTool1.SharedProps.AppearancesSmall.Appearance = appearance67;
		popupControlContainerTool1.SharedProps.Caption = "計算機";
		popupMenuTool4.SharedProps.Caption = "右鍵選單";
		buttonTool12.InstanceProps.IsFirstInGroup = true;
		popupMenuTool5.InstanceProps.IsFirstInGroup = true;
		buttonTool16.InstanceProps.IsFirstInGroup = true;
		buttonTool19.InstanceProps.IsFirstInGroup = true;
		popupMenuTool6.InstanceProps.IsFirstInGroup = true;
		buttonTool20.InstanceProps.IsFirstInGroup = true;
		popupControlContainerTool2.InstanceProps.IsFirstInGroup = true;
		popupMenuTool4.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[17]
		{
			buttonTool9, buttonTool10, buttonTool11, buttonTool12, popupMenuTool5, buttonTool13, buttonTool14, buttonTool15, buttonTool16, buttonTool17,
			buttonTool18, buttonTool19, popupMenuTool6, popupMenuTool7, buttonTool20, popupControlContainerTool2, popupControlContainerTool3
		});
		appearance68.Image = resources.GetObject("appearance36.Image");
		buttonTool21.SharedProps.AppearancesSmall.Appearance = appearance68;
		buttonTool21.SharedProps.Caption = "剪下";
		buttonTool21.SharedProps.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
		appearance69.Image = resources.GetObject("appearance37.Image");
		buttonTool22.SharedProps.AppearancesSmall.Appearance = appearance69;
		buttonTool22.SharedProps.Caption = "複製";
		buttonTool22.SharedProps.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
		appearance70.Image = resources.GetObject("appearance38.Image");
		buttonTool23.SharedProps.AppearancesSmall.Appearance = appearance70;
		buttonTool23.SharedProps.Caption = "貼上";
		buttonTool23.SharedProps.Shortcut = System.Windows.Forms.Shortcut.CtrlV;
		popupMenuTool8.Settings.SideStripWidth = -1;
		appearance71.Image = resources.GetObject("appearance39.Image");
		popupMenuTool8.SharedProps.AppearancesSmall.Appearance = appearance71;
		popupMenuTool8.SharedProps.Caption = "插入工項";
		popupMenuTool8.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		buttonTool25.InstanceProps.IsFirstInGroup = true;
		popupMenuTool8.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[3] { buttonTool24, buttonTool25, buttonTool26 });
		appearance72.Image = resources.GetObject("appearance40.Image");
		buttonTool27.SharedProps.AppearancesSmall.Appearance = appearance72;
		buttonTool27.SharedProps.Caption = "下層單價分析( Alt + Z)";
		appearance73.Image = resources.GetObject("appearance41.Image");
		buttonTool28.SharedProps.AppearancesSmall.Appearance = appearance73;
		buttonTool28.SharedProps.Caption = "新增工項";
		buttonTool29.SharedProps.Caption = "自基本資料庫挑選工項";
		buttonTool29.SharedProps.Shortcut = System.Windows.Forms.Shortcut.Ins;
		appearance74.Image = resources.GetObject("appearance42.Image");
		buttonTool30.SharedProps.AppearancesSmall.Appearance = appearance74;
		buttonTool30.SharedProps.Caption = "編輯工項";
		appearance75.Image = resources.GetObject("appearance43.Image");
		buttonTool31.SharedProps.AppearancesSmall.Appearance = appearance75;
		buttonTool31.SharedProps.Caption = "刪除工項";
		buttonTool31.SharedProps.Shortcut = System.Windows.Forms.Shortcut.Del;
		appearance76.Image = resources.GetObject("appearance44.Image");
		buttonTool32.SharedProps.AppearancesLarge.Appearance = appearance76;
		appearance77.Image = resources.GetObject("appearance45.Image");
		buttonTool32.SharedProps.AppearancesSmall.Appearance = appearance77;
		buttonTool32.SharedProps.Caption = "上移";
		appearance78.Image = resources.GetObject("appearance46.Image");
		buttonTool33.SharedProps.AppearancesSmall.Appearance = appearance78;
		buttonTool33.SharedProps.Caption = "下移";
		appearance79.Image = resources.GetObject("appearance47.Image");
		buttonTool34.SharedProps.AppearancesSmall.Appearance = appearance79;
		buttonTool34.SharedProps.Caption = "重新小計";
		appearance80.Image = resources.GetObject("appearance48.Image");
		buttonTool35.SharedProps.AppearancesSmall.Appearance = appearance80;
		buttonTool35.SharedProps.Caption = "接收QTS數量";
		appearance81.Image = resources.GetObject("appearance49.Image");
		popupMenuTool9.SharedProps.AppearancesSmall.Appearance = appearance81;
		popupMenuTool9.SharedProps.Caption = "引用基本資料庫";
		buttonTool38.InstanceProps.IsFirstInGroup = true;
		popupMenuTool9.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[3] { buttonTool36, buttonTool37, buttonTool38 });
		appearance82.Image = resources.GetObject("appearance50.Image");
		popupMenuTool10.SharedProps.AppearancesSmall.Appearance = appearance82;
		popupMenuTool10.SharedProps.Caption = "回傳資料庫";
		popupMenuTool10.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[2] { buttonTool39, buttonTool40 });
		buttonTool41.SharedProps.Caption = "單筆單價分析";
		buttonTool42.SharedProps.Caption = "相關單價分析";
		buttonTool43.SharedProps.Caption = "單筆單價分析";
		buttonTool44.SharedProps.Caption = "相關單價分析";
		buttonTool45.SharedProps.Caption = "自專案挑選工項";
		appearance83.Image = resources.GetObject("appearance51.Image");
		buttonTool46.SharedProps.AppearancesSmall.Appearance = appearance83;
		buttonTool46.SharedProps.Caption = "加總項目設定";
		buttonTool46.SharedProps.Shortcut = System.Windows.Forms.Shortcut.Alt1;
		appearance84.Image = resources.GetObject("appearance52.Image");
		buttonTool47.SharedProps.AppearancesSmall.Appearance = appearance84;
		buttonTool47.SharedProps.Caption = "工程數量計算";
		buttonTool48.SharedProps.Caption = "複製工項...";
		appearance85.Image = resources.GetObject("appearance53.Image");
		buttonTool49.SharedProps.AppearancesSmall.Appearance = appearance85;
		buttonTool49.SharedProps.Caption = "複製IR設定值";
		appearance86.Image = resources.GetObject("appearance54.Image");
		buttonTool50.SharedProps.AppearancesSmall.Appearance = appearance86;
		buttonTool50.SharedProps.Caption = "貼上IR設定值";
		buttonTool50.SharedProps.Enabled = false;
		buttonTool51.SharedProps.Caption = "選取項目單價";
		buttonTool52.SharedProps.Caption = "父項查詢...";
		appearance87.Image = resources.GetObject("appearance55.Image");
		buttonTool53.SharedProps.AppearancesSmall.Appearance = appearance87;
		buttonTool53.SharedProps.Caption = "調價";
		popupControlContainerTool4.Control = this.cbHistoryWorkRate;
		popupControlContainerTool4.SharedProps.Caption = "查詢歷史工率";
		popupControlContainerTool4.SharedProps.CustomizerCaption = "查詢歷史工率";
		popupControlContainerTool5.Control = this.cboSubItemQtyAmt;
		popupControlContainerTool5.SharedProps.Caption = "預算/估驗資訊";
		buttonTool54.SharedProps.Caption = "複製到詳細表";
		this.ultraToolbarsManager1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[33]
		{
			popupControlContainerTool1, popupMenuTool4, buttonTool21, buttonTool22, buttonTool23, popupMenuTool8, buttonTool27, buttonTool28, buttonTool29, buttonTool30,
			buttonTool31, buttonTool32, buttonTool33, buttonTool34, buttonTool35, popupMenuTool9, popupMenuTool10, buttonTool41, buttonTool42, buttonTool43,
			buttonTool44, buttonTool45, buttonTool46, buttonTool47, buttonTool48, buttonTool49, buttonTool50, buttonTool51, buttonTool52, buttonTool53,
			popupControlContainerTool4, popupControlContainerTool5, buttonTool54
		});
		this.ultraToolbarsManager1.BeforeToolbarListDropdown += new Infragistics.Win.UltraWinToolbars.BeforeToolbarListDropdownEventHandler(ultraToolbarsManager1_BeforeToolbarListDropdown);
		this.ultraToolbarsManager1.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(ultraToolbarsManager1_ToolClick);
		this.ultraToolbarsManager1.AfterToolActivate += new Infragistics.Win.UltraWinToolbars.ToolEventHandler(ultraToolbarsManager1_AfterToolActivate);
		this.imageList3.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList3.ImageStream");
		this.imageList3.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList3.Images.SetKeyName(0, "");
		this.imageList3.Images.SetKeyName(1, "");
		this.imageList3.Images.SetKeyName(2, "");
		this.imageList3.Images.SetKeyName(3, "");
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 44);
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Left.Name = "_FormMrsBaseBreakdown_Toolbars_Dock_Area_Left";
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 490);
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(785, 44);
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Right.Name = "_FormMrsBaseBreakdown_Toolbars_Dock_Area_Right";
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 490);
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Right.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Top.Name = "_FormMrsBaseBreakdown_Toolbars_Dock_Area_Top";
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(785, 44);
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Top.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 534);
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Bottom.Name = "_FormMrsBaseBreakdown_Toolbars_Dock_Area_Bottom";
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(785, 0);
		this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ultraToolbarsManager1;
		this.panel4.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel4.Controls.Add(this.BtnAdjust);
		this.panel4.Controls.Add(this.lblPrice);
		this.panel4.Controls.Add(this.txtPrice);
		this.panel4.Controls.Add(this.lblMRate);
		this.panel4.Controls.Add(this.lblAmount);
		this.panel4.Controls.Add(this.ultraButton2);
		this.panel4.Controls.Add(this.label13);
		this.panel4.Controls.Add(this.label14);
		this.panel4.Controls.Add(this.lblAnalysisQty);
		this.panel4.Controls.Add(this.txtAnalysisQty);
		this.panel4.Controls.Add(this.label11);
		this.panel4.Controls.Add(this.lblWRate);
		this.panel4.Controls.Add(this.label6);
		this.panel4.Controls.Add(this.label5);
		this.panel4.Controls.Add(this.lblERate);
		this.panel4.Controls.Add(this.Label100);
		this.panel4.Controls.Add(this.lblLRate);
		this.panel4.Controls.Add(this.label3);
		this.panel4.Controls.Add(this.lblUnit);
		this.panel4.Controls.Add(this.label12);
		this.panel4.Controls.Add(this.lblCName);
		this.panel4.Controls.Add(this.lblPccesCode);
		this.panel4.Controls.Add(this.label2);
		this.panel4.Controls.Add(this.label1);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel4.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.panel4.Location = new System.Drawing.Point(0, 0);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(785, 96);
		this.panel4.TabIndex = 9;
		this.panel4.MouseDown += new System.Windows.Forms.MouseEventHandler(FormMrsBaseBreakdown_MouseDown);
		appearance88.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance88.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnAdjust.Appearance = appearance88;
		this.BtnAdjust.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.BtnAdjust.Cursor = System.Windows.Forms.Cursors.Hand;
		this.BtnAdjust.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.BtnAdjust.Location = new System.Drawing.Point(526, 72);
		this.BtnAdjust.Name = "BtnAdjust";
		this.BtnAdjust.ShowFocusRect = false;
		this.BtnAdjust.ShowOutline = false;
		this.BtnAdjust.Size = new System.Drawing.Size(43, 22);
		this.BtnAdjust.SupportThemes = false;
		this.BtnAdjust.TabIndex = 43;
		this.BtnAdjust.Text = "調價";
		this.BtnAdjust.Visible = false;
		this.BtnAdjust.Click += new System.EventHandler(BtnAdjust_Click);
		appearance89.TextHAlign = Infragistics.Win.HAlign.Left;
		this.lblPrice.Appearance = appearance89;
		this.lblPrice.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.Dashed;
		this.lblPrice.Location = new System.Drawing.Point(426, 72);
		this.lblPrice.Name = "lblPrice";
		this.lblPrice.Size = new System.Drawing.Size(100, 22);
		this.lblPrice.TabIndex = 38;
		this.lblPrice.Text = "[lblPrice]";
		this.lblPrice.TextChanged += new System.EventHandler(lblPrice_TextChanged);
		this.txtPrice.AutoSize = true;
		this.txtPrice.FlatMode = true;
		this.txtPrice.Location = new System.Drawing.Point(480, 60);
		this.txtPrice.Name = "txtPrice";
		this.txtPrice.Size = new System.Drawing.Size(80, 22);
		this.txtPrice.SupportThemes = false;
		this.txtPrice.TabIndex = 42;
		this.txtPrice.Text = "txtAmount";
		this.txtPrice.Visible = false;
		this.txtPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtPrice_KeyPress);
		this.lblMRate.Location = new System.Drawing.Point(454, 47);
		this.lblMRate.Name = "lblMRate";
		this.lblMRate.Size = new System.Drawing.Size(118, 23);
		this.lblMRate.TabIndex = 31;
		this.lblMRate.Text = "00.00 %";
		this.lblMRate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		appearance90.TextHAlign = Infragistics.Win.HAlign.Left;
		this.lblAmount.Appearance = appearance90;
		this.lblAmount.Location = new System.Drawing.Point(660, 72);
		this.lblAmount.Name = "lblAmount";
		this.lblAmount.Size = new System.Drawing.Size(120, 22);
		this.lblAmount.TabIndex = 40;
		this.lblAmount.Text = "[lblAmount]";
		appearance91.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.ultraButton2.Appearance = appearance91;
		this.ultraButton2.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.ultraButton2.Cursor = System.Windows.Forms.Cursors.Hand;
		this.ultraButton2.Font = new System.Drawing.Font("Arial", 7f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraButton2.Location = new System.Drawing.Point(206, 72);
		this.ultraButton2.Name = "ultraButton2";
		this.ultraButton2.ShowFocusRect = false;
		this.ultraButton2.ShowOutline = false;
		this.ultraButton2.Size = new System.Drawing.Size(22, 22);
		this.ultraButton2.SupportThemes = false;
		this.ultraButton2.TabIndex = 41;
		this.ultraButton2.Text = "...";
		this.ultraButton2.Click += new System.EventHandler(ultraButton2_Click);
		this.label13.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label13.Location = new System.Drawing.Point(616, 75);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(52, 20);
		this.label13.TabIndex = 39;
		this.label13.Text = "複價：";
		this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label14.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label14.Location = new System.Drawing.Point(374, 75);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(52, 20);
		this.label14.TabIndex = 37;
		this.label14.Text = "單價：";
		this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		appearance92.BorderColor = System.Drawing.Color.FromArgb(64, 64, 64);
		appearance92.TextHAlign = Infragistics.Win.HAlign.Left;
		this.lblAnalysisQty.Appearance = appearance92;
		this.lblAnalysisQty.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.Dashed;
		this.lblAnalysisQty.Location = new System.Drawing.Point(88, 72);
		this.lblAnalysisQty.Name = "lblAnalysisQty";
		this.lblAnalysisQty.Size = new System.Drawing.Size(118, 22);
		this.lblAnalysisQty.TabIndex = 36;
		this.lblAnalysisQty.Text = "[lblAnalysisQty]";
		this.lblAnalysisQty.WrapText = false;
		this.txtAnalysisQty.AutoSize = true;
		this.txtAnalysisQty.FlatMode = true;
		this.txtAnalysisQty.Location = new System.Drawing.Point(152, 56);
		this.txtAnalysisQty.Name = "txtAnalysisQty";
		this.txtAnalysisQty.Size = new System.Drawing.Size(80, 22);
		this.txtAnalysisQty.SupportThemes = false;
		this.txtAnalysisQty.TabIndex = 35;
		this.txtAnalysisQty.Text = "txtAnalysisQty";
		this.txtAnalysisQty.Visible = false;
		this.txtAnalysisQty.Validating += new System.ComponentModel.CancelEventHandler(txtAnalysisQty_Validating);
		this.txtAnalysisQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtAnalysisQty_KeyPress);
		this.label11.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label11.Location = new System.Drawing.Point(8, 75);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(88, 20);
		this.label11.TabIndex = 34;
		this.label11.Text = "分析數量：";
		this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.lblWRate.Location = new System.Drawing.Point(696, 47);
		this.lblWRate.Name = "lblWRate";
		this.lblWRate.Size = new System.Drawing.Size(84, 23);
		this.lblWRate.TabIndex = 33;
		this.lblWRate.Text = "00.00 %";
		this.lblWRate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label6.AutoSize = true;
		this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label6.Location = new System.Drawing.Point(616, 50);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(87, 15);
		this.label6.TabIndex = 32;
		this.label6.Text = "雜項比例：";
		this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label5.AutoSize = true;
		this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label5.Location = new System.Drawing.Point(374, 50);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(87, 15);
		this.label5.TabIndex = 30;
		this.label5.Text = "材料比例：";
		this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.lblERate.Location = new System.Drawing.Point(288, 47);
		this.lblERate.Name = "lblERate";
		this.lblERate.Size = new System.Drawing.Size(84, 23);
		this.lblERate.TabIndex = 29;
		this.lblERate.Text = "00.00 %";
		this.lblERate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.Label100.AutoSize = true;
		this.Label100.ForeColor = System.Drawing.SystemColors.ControlText;
		this.Label100.Location = new System.Drawing.Point(208, 50);
		this.Label100.Name = "Label100";
		this.Label100.Size = new System.Drawing.Size(87, 15);
		this.Label100.TabIndex = 28;
		this.Label100.Text = "機具比例：";
		this.Label100.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.lblLRate.Location = new System.Drawing.Point(88, 47);
		this.lblLRate.Name = "lblLRate";
		this.lblLRate.Size = new System.Drawing.Size(108, 23);
		this.lblLRate.TabIndex = 27;
		this.lblLRate.Text = "00.00 %";
		this.lblLRate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label3.AutoSize = true;
		this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label3.Location = new System.Drawing.Point(8, 50);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(87, 15);
		this.label3.TabIndex = 26;
		this.label3.Text = "人工比例：";
		this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		appearance93.TextHAlign = Infragistics.Win.HAlign.Left;
		this.lblUnit.Appearance = appearance93;
		this.lblUnit.Location = new System.Drawing.Point(664, 28);
		this.lblUnit.Name = "lblUnit";
		this.lblUnit.Size = new System.Drawing.Size(97, 20);
		this.lblUnit.TabIndex = 22;
		this.lblUnit.Text = "[lblUnit]";
		this.label12.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label12.Location = new System.Drawing.Point(616, 28);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(52, 20);
		this.label12.TabIndex = 21;
		this.label12.Text = "單位：";
		this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		appearance94.BackColor = System.Drawing.Color.Transparent;
		this.lblCName.Appearance = appearance94;
		this.lblCName.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblCName.Location = new System.Drawing.Point(88, 4);
		this.lblCName.Name = "lblCName";
		this.lblCName.Size = new System.Drawing.Size(672, 23);
		this.lblCName.TabIndex = 12;
		this.lblCName.Text = "[lblCName]";
		this.lblCName.MouseDown += new System.Windows.Forms.MouseEventHandler(FormMrsBaseBreakdown_MouseDown);
		this.lblPccesCode.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblPccesCode.Location = new System.Drawing.Point(88, 26);
		this.lblPccesCode.Name = "lblPccesCode";
		this.lblPccesCode.Size = new System.Drawing.Size(468, 23);
		this.lblPccesCode.TabIndex = 11;
		this.lblPccesCode.Text = "[lblPccesCode]";
		this.lblPccesCode.MouseDown += new System.Windows.Forms.MouseEventHandler(FormMrsBaseBreakdown_MouseDown);
		this.label2.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.label2.Location = new System.Drawing.Point(8, 4);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(82, 23);
		this.label2.TabIndex = 10;
		this.label2.Text = "工項名稱：";
		this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label2.MouseDown += new System.Windows.Forms.MouseEventHandler(FormMrsBaseBreakdown_MouseDown);
		this.label1.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.label1.Location = new System.Drawing.Point(8, 26);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(82, 23);
		this.label1.TabIndex = 9;
		this.label1.Text = "工項代碼：";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(FormMrsBaseBreakdown_MouseDown);
		this.panel1.Controls.Add(this.panel4);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.panel1.Location = new System.Drawing.Point(0, 44);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(785, 96);
		this.panel1.TabIndex = 0;
		this.toolStripMenuItemBudgetChangeHistory.Name = "toolStripMenuItemBudgetChangeHistory";
		this.toolStripMenuItemBudgetChangeHistory.Size = new System.Drawing.Size(142, 22);
		this.toolStripMenuItemBudgetChangeHistory.Text = "查詢變更紀錄";
		this.toolStripMenuItemBudgetChangeHistory.Click += new System.EventHandler(Do_QryParentBudgetChangeHistory);
		this.contextMenuStripGridBase2.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.toolStripMenuItemBudgetChangeHistory });
		this.contextMenuStripGridBase2.Name = "contextMenuStripGridBase2";
		this.contextMenuStripGridBase2.Size = new System.Drawing.Size(143, 26);
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.ClientSize = new System.Drawing.Size(785, 534);
		base.Controls.Add(this.panel3);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.cboSubItemQtyAmt);
		base.Controls.Add(this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Left);
		base.Controls.Add(this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Right);
		base.Controls.Add(this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Top);
		base.Controls.Add(this._FormMrsBaseBreakdown_Toolbars_Dock_Area_Bottom);
		this.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		this.MaximumSize = new System.Drawing.Size(1200, 1500);
		base.MinimizeBox = false;
		this.MinimumSize = new System.Drawing.Size(793, 561);
		base.Name = "FormMrsBaseBreakdown";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "單價分析項目";
		base.Load += new System.EventHandler(FormMrsBaseBreakdown_Load);
		base.Activated += new System.EventHandler(FormMrsBaseBreakdown_Activated);
		base.MouseDown += new System.Windows.Forms.MouseEventHandler(FormMrsBaseBreakdown_MouseDown);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormMrsBaseBreakdown_FormClosing);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormMrsBaseBreakdown_KeyDown);
		this.Tab_A.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid1).EndInit();
		this.panel8.ResumeLayout(false);
		this.panel6.ResumeLayout(false);
		this.Tab_B.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridMrsBase2).EndInit();
		this.panel7.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.cbHistoryWorkRate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubItemQtyAmt).EndInit();
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		this.pnlAdvance.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridMrsBase1).EndInit();
		this.pnlInfo.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.TabCtrl_Info).EndInit();
		this.TabCtrl_Info.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.axSSPanel1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).EndInit();
		this.panel4.ResumeLayout(false);
		this.panel4.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtPrice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAnalysisQty).EndInit();
		this.panel1.ResumeLayout(false);
		this.contextMenuStripGridBase2.ResumeLayout(false);
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
			FM_INFO.SetValue(Message);
		}
	}

	public FormMrsBaseBreakdown()
	{
		InitializeComponent();
		GridCols = gridMrsBase1.Cols.Count;
		CellStyle csCb = gridMrsBase1.Styles.Add("ComboList");
		csCb.DataType = typeof(short);
		csCb.ComboList = "|0|1|2|3|4";
		csCb.ForeColor = Color.Navy;
		csCb.TextAlign = TextAlignEnum.LeftCenter;
		csCb.Font = new System.Drawing.Font(Font, FontStyle.Bold);
		PwrSet pwrSet = new PwrSet();
		dsPwrSet = pwrSet.GetEnabledPwrSet();
		string comboList = string.Empty;
		foreach (DataRow dr in dsPwrSet.Tables["PwrSet"].Rows)
		{
			comboList = comboList + ArchConvert.Obj2String(dr["PwrName"]) + "|";
		}
		CellStyle csCbPS = gridMrsBase1.Styles.Add("ComboListPS");
		csCbPS.DataType = typeof(string);
		csCbPS.ForeColor = Color.Navy;
		csCbPS.TextAlign = TextAlignEnum.LeftCenter;
		csCbPS.Font = new System.Drawing.Font(Font, FontStyle.Bold);
		csCbPS.ComboList = comboList.TrimEnd('|');
		CellStyle cs = gridMrsBase1.Styles.Add("img");
		cs.DataType = typeof(Image);
		CellStyle cs2 = gridMrsBase1.Styles.Add("EditMode");
		cs2.DataType = typeof(Image);
		cs2.ImageAlign = ImageAlignEnum.RightCenter;
		if (GridColsSquence == null)
		{
			GridColsSquence = new object[GridCols, 8];
		}
	}

	private void SaveReNumber(string sType)
	{
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add("WinFORM 基本工料--ListNo 重新給號");
		Archnowledge.Pcces.BUDClass.MrsBaseB MrsBaseB1 = new Archnowledge.Pcces.BUDClass.MrsBaseB(aArr);
		MrsBaseB1.ps_projectcode = F_ProjectCode;
		MrsBaseB1.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		MrsBaseB1.ps_parentCode = parentPubCode.ToString();
		MrsBaseB1.ps_Issue = F_chgCount;
		DataTable DT_ReListNo = new DataTable();
		DT_ReListNo.Columns.Add("ListNo", Type.GetType("System.String"));
		DT_ReListNo.Columns.Add("NewListNo", Type.GetType("System.String"));
		DT_ReListNo.Columns.Add("Source", Type.GetType("System.Int32"));
		for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
		{
			DataRow DR = DT_ReListNo.NewRow();
			if (sType == "")
			{
				DR["listNo"] = gridMrsBase1[i, "ListNo"];
			}
			else
			{
				DR["listNo"] = i.ToString();
			}
			DR["NewlistNo"] = i.ToString();
			if (gridMrsBase1[i, "Source"] != null)
			{
				if (gridMrsBase1[i, "Source"].ToString() == "日報統計")
				{
					DR["Source"] = 0;
				}
				else if (gridMrsBase1[i, "Source"].ToString() == "固定成本")
				{
					DR["Source"] = -1;
				}
				else
				{
					DR["Source"] = gridMrsBase1[i, "Source"];
				}
			}
			DT_ReListNo.Rows.Add(DR);
		}
		MrsBaseB1.ReSetListNo(DT_ReListNo);
		if (chkReCalcu.Checked)
		{
			DoMrsCalculate();
		}
		else
		{
			GetLowerData();
		}
	}

	private void tlMoveUp()
	{
		bool IsMoved = false;
		ultraToolbarsManager1.Enabled = false;
		ArrayList SelItems = new ArrayList();
		int iIdx = -1;
		for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
		{
			if (gridMrsBase1.Rows[i].Selected)
			{
				SelItems.Add(gridMrsBase1[i, "PSNo"]);
			}
		}
		for (int i = 0; i < SelItems.Count; i++)
		{
			iIdx = gridMrsBase1.FindRow((int)SelItems[i], 1, gridMrsBase1.Cols["PSNo"].SafeIndex, wrap: false);
			if (iIdx == 1)
			{
				break;
			}
			if (iIdx > -1)
			{
				gridMrsBase1.Rows[iIdx].Move(iIdx - 1);
				IsMoved = true;
			}
		}
		GlobalSelItems.Clear();
		for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
		{
			GlobalSelItems.Add(gridMrsBase1[i, "PSNo"]);
		}
		MoveUpDownFlag = "MOVED";
		if (IsMoved)
		{
			SaveReNumber("");
		}
		int iGoBackRow = 0;
		for (int i = 0; i < SelItems.Count; i++)
		{
			int iIndx = Get_RealRow2(SelItems[i].ToString());
			if (iGoBackRow == 0)
			{
				iGoBackRow = iIndx;
				gridMrsBase1.Row = iGoBackRow;
			}
			gridMrsBase1.Rows[iIndx].Selected = true;
		}
		ultraToolbarsManager1.Enabled = true;
		ultraToolbarsManager1.EndUpdate();
	}

	private void tlMoveDown()
	{
		bool IsMoved = false;
		ultraToolbarsManager1.Enabled = false;
		ArrayList SelItems = new ArrayList();
		SelItems.Clear();
		int iIdx = -1;
		for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
		{
			if (gridMrsBase1.Rows[i].Selected)
			{
				SelItems.Add(gridMrsBase1[i, "PSNo"]);
			}
		}
		for (int i = SelItems.Count - 1; i >= 0; i--)
		{
			iIdx = gridMrsBase1.FindRow((int)SelItems[i], 1, gridMrsBase1.Cols["PSNo"].SafeIndex, wrap: false);
			if (iIdx == gridMrsBase1.Rows.Count - 1)
			{
				break;
			}
			if (iIdx > -1)
			{
				gridMrsBase1.Rows[iIdx].Move(iIdx + 1);
				IsMoved = true;
			}
		}
		GlobalSelItems.Clear();
		for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
		{
			GlobalSelItems.Add(gridMrsBase1[i, "PSNo"]);
		}
		MoveUpDownFlag = "MOVED";
		if (IsMoved)
		{
			SaveReNumber("");
		}
		int iGoBackRow = 0;
		for (int i = 0; i < SelItems.Count; i++)
		{
			int iIndx = Get_RealRow2(SelItems[i].ToString());
			if (iGoBackRow == 0)
			{
				iGoBackRow = iIndx;
				gridMrsBase1.Row = iGoBackRow;
			}
			gridMrsBase1.Rows[iIndx].Selected = true;
		}
		ultraToolbarsManager1.Enabled = true;
		ultraToolbarsManager1.EndUpdate();
	}

	private void FormMrsBaseBreakdown_MouseDown(object sender, MouseEventArgs e)
	{
		if (sBindFlag == "BINDING")
		{
			return;
		}
		ChangeAnalysisState("");
		ChangeAnalysisState2("");
		int iX = gridMrsBase1.MouseRow;
		int iY = gridMrsBase1.MouseCol;
		ResetToolbar();
		if (e.Button == MouseButtons.Left)
		{
			if (gridMrsBase1.Col != 0 && FORM_STATUS != FormStatus.Edit && !gridMrsBase1.Cols[iY].AllowEditing)
			{
				gridMrsBase1.Col = 0;
			}
			if (gridMrsBase1.Row > 0)
			{
				string ItemKind = gridMrsBase1[gridMrsBase1.Row, "costKind"].ToString().Trim();
				if (ItemKind == "Z" || ItemKind == "#")
				{
					gridMrsBase1.Col = 0;
				}
				CurrentRow = gridMrsBase1.Row;
			}
			if (Cursor == Cursors.Hand && (bool)gridMrsBase1[iX, "Analysis"])
			{
				if (aaLayer.Count >= iLayer)
				{
					aaLayer[iLayer - 1] = iX;
				}
				else
				{
					aaLayer.Add(iX);
				}
				F_CallType = "ProjMrsB";
				GoNextLevelAnalysis(iX, iY);
			}
			if (gridMrsBase1.Cols[iY].Name == "pccesCode" && iX != -1)
			{
				OpenDocument(gridMrsBase1.Rows[iX][iY]);
			}
			return;
		}
		if (e.Button != MouseButtons.Right)
		{
			return;
		}
		if (gridMrsBase1.Rows.Count == 1 || iX == -1)
		{
			ultraToolbarsManager1.Tools["mnuCut"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuCopy"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuEdit"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuDelete"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuIRSet"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuIRCopy"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuIRPaste"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuAnalysis"].SharedProps.Enabled = false;
			return;
		}
		if (gridMrsBase1.SelectedItems > 1)
		{
			ultraToolbarsManager1.Tools["mnuAnalysis"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuPaste"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuIRSet"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuIRCopy"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuIRPaste"].SharedProps.Enabled = false;
			return;
		}
		gridMrsBase1.Row = gridMrsBase1.MouseRow;
		if (gridMrsBase1.Row == 0)
		{
			return;
		}
		ultraToolbarsManager1.Tools["mnuCopy"].SharedProps.Enabled = true;
		ultraToolbarsManager1.Tools["mnuAnalysis"].SharedProps.Enabled = (bool)gridMrsBase1[gridMrsBase1.Row, "Analysis"];
		if (!F_IsSBID)
		{
			bool enablePaste = !F_Istemplate && (F_ActionName == PccesFormAction.BUD || (F_ActionName == PccesFormAction.BID && !F_IsLockAn) || F_ActionName == PccesFormAction.MrsBase);
			IDataObject iData = Clipboard.GetDataObject();
			((ButtonTool)ultraToolbarsManager1.Tools["mnuPaste"]).SharedProps.Enabled = enablePaste && iData.GetDataPresent(DataFormats.Text);
			string ItemKind = gridMrsBase1[gridMrsBase1.Row, "CostKind"].ToString().Trim();
			if (F_IsUseIR)
			{
				switch (ItemKind)
				{
				case "Z":
				case "L":
				case "E":
				case "M":
					goto IL_05c2;
				}
				if (ItemKind == "%")
				{
					goto IL_05c2;
				}
			}
			ultraToolbarsManager1.Tools["mnuIRSet"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuIRCopy"].SharedProps.Enabled = false;
		}
		goto IL_064d;
		IL_064d:
		string pccesCode = gridMrsBase1[gridMrsBase1.Row, "pccesCode"].ToString();
		if (EnableCOMS && F_ActionName == PccesFormAction.BUD)
		{
			if (!(bool)gridMrsBase1[gridMrsBase1.Row, "Analysis"])
			{
				ultraToolbarsManager1.Tools["GetHistoryWorkRate"].SharedProps.Visible = true;
				HistoryWorkRate historyWorkRate = new HistoryWorkRate();
				string parentPccesCode = lblPccesCode.Text;
				DataSet DSHistoryWorkRate = historyWorkRate.GetHistoryWorkRate(parentPccesCode, pccesCode);
				cbHistoryWorkRate.Text = "請下拉，挑選工項數量";
				cbHistoryWorkRate.DataSource = DSHistoryWorkRate.Tables["HistoryWorkRate"];
				cbHistoryWorkRate.DataBind();
				cbHistoryWorkRate.DisplayLayout.Bands[0].Columns["ProjectCodeName"].PerformAutoResize(PerformAutoSizeType.AllRowsInBand);
				cbHistoryWorkRate.DisplayLayout.Bands[0].Columns["Contractor"].PerformAutoResize(PerformAutoSizeType.AllRowsInBand);
				cbHistoryWorkRate.Visible = true;
			}
			else
			{
				cbHistoryWorkRate.Visible = false;
				ultraToolbarsManager1.Tools["GetHistoryWorkRate"].SharedProps.Visible = false;
			}
			ultraToolbarsManager1.Tools["GetSubItemQtyAmt"].SharedProps.Enabled = pccesCode != string.Empty;
		}
		else
		{
			ultraToolbarsManager1.Tools["GetHistoryWorkRate"].SharedProps.Visible = false;
			ultraToolbarsManager1.Tools["GetSubItemQtyAmt"].SharedProps.Enabled = false;
		}
		return;
		IL_05c2:
		ultraToolbarsManager1.Tools["mnuIRSet"].SharedProps.Enabled = true;
		ultraToolbarsManager1.Tools["mnuIRCopy"].SharedProps.Enabled = true;
		goto IL_064d;
	}

	private void OpenDocument(object value)
	{
		if (value != null && value != DBNull.Value)
		{
			string PccesCode = ArchConvert.Obj2String(value).Trim();
			AddOnDownLoad addOnDownLoad = new AddOnDownLoad();
			addOnDownLoad.OpenDocument(PccesCode, F_UserID, F_ProjectCode);
		}
	}

	private void SetUpCboSubItemQtyAmt()
	{
		if (gridMrsBase1[gridMrsBase1.Row, "pccesCode"] != null)
		{
			string PccesCode = gridMrsBase1[gridMrsBase1.Row, "pccesCode"].ToString().Trim();
			ComsWebService theComsWebService = new ComsWebService(ProjectCode);
			theComsWebService.SetUpCboSubItemQtyAmt(cboSubItemQtyAmt, PccesCode);
		}
	}

	private void axSSPanel1_MouseDownEvent(object sender, DSSPanelEvents_MouseDownEvent e)
	{
		ChangeAnalysisState("");
	}

	public void Reload()
	{
		iLayer = 1;
		saLayer.Clear();
		saLayerCostDec.Clear();
		FormMrsBaseBreakdown_Load(this, EventArgs.Empty);
		GetUpperData();
		GetLowerData();
		gridMrsBase1.Focus();
		gridMrsBase1.Select();
		FORM_STATUS = FormStatus.Normal;
	}

	private bool Is75094900()
	{
		string sPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "75094900.dat");
		if (File.Exists(sPath))
		{
			return true;
		}
		return false;
	}

	private void FormMrsBaseBreakdown_Load(object sender, EventArgs e)
	{
		F_chgCount = F_Issue.ToString();
		AppLocation = AppDomain.CurrentDomain.BaseDirectory;
		pnlInfo.Width = 0;
		ultraToolbarsManager1.Tools["mnuIRPaste"].SharedProps.Enabled = false;
		if (SysConfig.SysEnablePwrSet)
		{
			gridMrsBase1.Cols["PwrSet"].Visible = true;
			gridMrsBase1.Cols["Account"].Visible = true;
		}
		else
		{
			gridMrsBase1.Cols["PwrSet"].Visible = false;
			gridMrsBase1.Cols["Account"].Visible = false;
		}
		if (Is75094900())
		{
			gridMrsBase1.Cols["ExtendCode"].Visible = true;
		}
		else
		{
			gridMrsBase1.Cols["ExtendCode"].Visible = false;
		}
		SysUser oSysUser = new SysUser();
		F_CurrentDBName = oSysUser.GetSysUserDatabaseName(F_UserID);
		LoadSettings();
		CheckExcelExport();
		string sHideCols = CommonMethods.GetDebugValue("Breakdown", "HideCols");
		HideCols(Convert.ToBoolean((sHideCols == "") ? "True" : sHideCols));
		string IsCheck = CommonMethods.GetIniValue("BreakDown", "AutoReCalcu");
		if (IsCheck == "1")
		{
			chkReCalcu.Checked = true;
		}
		int iLoc_X = PubTools.Str2Int(CommonMethods.GetIniValue("BreakDown", "LocationX"));
		int iLoc_Y = PubTools.Str2Int(CommonMethods.GetIniValue("BreakDown", "LocationY"));
		int iSiz_W = PubTools.Str2Int(CommonMethods.GetIniValue("BreakDown", "Width"));
		int iSiz_H = PubTools.Str2Int(CommonMethods.GetIniValue("BreakDown", "Height"));
		string Status = CommonMethods.GetIniValue("BreakDown", "WindowState");
		if (iLoc_X > 0 && iLoc_Y > 0)
		{
			base.Location = new Point(iLoc_X, iLoc_Y);
		}
		if (iSiz_W > 0)
		{
			base.Width = iSiz_W;
		}
		if (iSiz_H > 0)
		{
			base.Height = iSiz_H;
		}
		SettingDecimal();
		saLayer.Add(iLayer, parentPubCode);
		saLayerCostDec.Add(iLayer, iCostDigital);
		aaLayer.Add(1);
		if (F_ActionName == PccesFormAction.MrsBase)
		{
			((PopupMenuTool)ultraToolbarsManager1.Tools["mnuPop_Use"]).SharedProps.Visible = false;
			((PopupMenuTool)ultraToolbarsManager1.Tools["mnuPop_SendBack"]).SharedProps.Visible = false;
		}
		DetectQTS();
		if (!F_IsUseIR)
		{
			ultraToolbarsManager1.Tools["mnuIRSet"].SharedProps.Visible = false;
			ultraToolbarsManager1.Tools["mnuIRCopy"].SharedProps.Enabled = false;
		}
		else
		{
			ultraToolbarsManager1.Tools["mnuIRSet"].SharedProps.Visible = true;
			ultraToolbarsManager1.Tools["mnuIRCopy"].SharedProps.Enabled = true;
		}
		if (F_ActionName == PccesFormAction.BUD || F_ActionName == PccesFormAction.BID)
		{
			ultraToolbarsManager1.Tools["mnuPickFromProj"].SharedProps.Visible = true;
		}
		else
		{
			ultraToolbarsManager1.Tools["mnuPickFromProj"].SharedProps.Visible = false;
		}
		string FileINI = AppLocation + "OptionSet.ini";
		string sAllowRepeatItem = CommonMethods.IniReadValue(FileINI, "BreakDownData", "AllowRepeatItem");
		IsAllowRepeatItem = sAllowRepeatItem.ToUpper() == "TRUE";
		CurrentRow = 1;
		SetColsEditSymbol();
		if (!F_IsSBID)
		{
			ultraToolbarsManager1.Tools["PopupMenuNew"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuEdit"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuDelete"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuTool_Up"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuTool_Down"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuTool_ReCal_Small"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuPop_Use"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuQTS_Caller"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuIRSet"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuIRCopy"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuCopyToNew"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuCut"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuPaste"].SharedProps.Enabled = true;
			gridMrsBase1.Cols["Qty"].AllowEditing = true;
			gridMrsBase1.Cols["LockCost"].AllowEditing = true;
			gridMrsBase1.Cols["Cost"].AllowEditing = true;
			gridMrsBase1.Cols["Memo"].AllowEditing = true;
			ultraButton2.Enabled = true;
			BtnAdjust.Enabled = true;
		}
		if (F_ActionName != PccesFormAction.BUD)
		{
			gridMrsBase1.Cols["CostDec"].Visible = false;
			gridMrsBase1.Cols["AmtDec"].Visible = false;
			gridMrsBase1.Cols["fixPrice"].Visible = false;
			gridMrsBase1.Cols["ItemType"].Visible = false;
		}
		else
		{
			gridMrsBase1.Cols["CostDec"].Visible = true;
			gridMrsBase1.Cols["AmtDec"].Visible = true;
			gridMrsBase1.Cols["fixPrice"].Visible = true;
		}
		if (EnableCOMS && F_ActionName == PccesFormAction.BUD)
		{
			ultraToolbarsManager1.Tools["GetHistoryWorkRate"].SharedProps.Visible = F_ActionName == PccesFormAction.BUD;
			ultraToolbarsManager1.Tools["GetSubItemQtyAmt"].SharedProps.Visible = F_ActionName == PccesFormAction.BUD;
			if (F_ActionName == PccesFormAction.BUD || F_ActionName == PccesFormAction.MrsBase)
			{
				gridMrsBase1.Cols["Source"].Visible = true;
				gridMrsBase1.Cols["Ratio"].Visible = true;
				gridMrsBase1.Cols["GroupName"].Visible = true;
			}
			else
			{
				gridMrsBase1.Cols["Source"].Visible = false;
				gridMrsBase1.Cols["Ratio"].Visible = false;
				gridMrsBase1.Cols["GroupName"].Visible = false;
			}
			Archnowledge.Pcces.DomainModule.Coms.BudgetCtrl theBudgetCtrl = new Archnowledge.Pcces.DomainModule.Coms.BudgetCtrl();
			if (base.Owner is frmBudget)
			{
				bool DisabledByCOMS = false;
				if (SysConfig.SysChangeManagement && (base.Owner as frmBudget)._BudgetChangeCurrentVersion > 0)
				{
					DisabledByCOMS = true;
				}
				if (!DisabledByCOMS && SysConfig.SysComsEnable && SysConfig.SysEditAfterBudLem.ToUpper() == "DISABLE" && theBudgetCtrl.IsProjectAlreadySubPlan(ProjectCode, SysConfig.SysComsDB))
				{
					DisabledByCOMS = true;
				}
				if (DisabledByCOMS)
				{
					ultraToolbarsManager1.Tools["UseSingle"].SharedProps.Visible = false;
					ultraToolbarsManager1.Tools["UseMulti"].SharedProps.Visible = false;
					ultraToolbarsManager1.Tools["mnuTool_QTS"].SharedProps.Visible = false;
				}
			}
		}
		else
		{
			gridMrsBase1.Cols["Source"].Visible = false;
			gridMrsBase1.Cols["Ratio"].Visible = false;
			gridMrsBase1.Cols["GroupName"].Visible = false;
			ultraToolbarsManager1.Tools["GetHistoryWorkRate"].SharedProps.Visible = false;
			ultraToolbarsManager1.Tools["GetSubItemQtyAmt"].SharedProps.Visible = false;
		}
		RememberColsProps();
		Frm.OnUserRequest += UserReq;
		if (F_ActionName != PccesFormAction.SubChange && Status == "Maximized")
		{
			base.WindowState = FormWindowState.Maximized;
		}
		gridMrsBase1.ShowToolTipOnNarrowColumn = true;
		gridMrsBase2.ShowToolTipOnNarrowColumn = true;
		c1FlexGrid1.ShowToolTipOnNarrowColumn = true;
		ResetToolbar();
		ultraToolbarsManager1.Tools["mnuNewItem"].SharedProps.Enabled = F_CurrentDBName != CompanyDBName;
		Archnowledge.Pcces.DomainModule.General.PubProject thePubProject = new Archnowledge.Pcces.DomainModule.General.PubProject();
		EnableNewCalculateCost = thePubProject.GetPubProjectEnableNewCalculateCost(F_ProjectCode);
		if (F_ActionName == PccesFormAction.BUD && SysConfig.SysChangeManagement)
		{
			gridMrsBase2.ContextMenuStrip = contextMenuStripGridBase2;
		}
		GetUpperData();
		GetLowerData();
		gridMrsBase1.Focus();
		gridMrsBase1.Select();
		if (F_ActionName == PccesFormAction.BUD)
		{
			if (base.Owner != null && base.Owner is frmBudget)
			{
				ultraToolbarsManager1.Tools["mnuCopyforDetail"].SharedProps.Enabled = true;
			}
			else
			{
				ultraToolbarsManager1.Tools["mnuCopyforDetail"].SharedProps.Enabled = false;
			}
		}
		else
		{
			ultraToolbarsManager1.Tools["mnuCopyforDetail"].SharedProps.Enabled = false;
		}
	}

	private void CheckExcelExport()
	{
		string StrShowExcelExport = CommonMethods.GetIniValue("EXCEL", "ShowExcelExport").Trim();
		if (StrShowExcelExport != "" && Convert.ToBoolean(StrShowExcelExport))
		{
			pnlAdvance.Visible = true;
		}
		else
		{
			pnlAdvance.Visible = false;
		}
	}

	private void DetectQTS()
	{
		if (File.Exists("C:\\QTS\\Book1.dbf"))
		{
			ultraToolbarsManager1.Tools["mnuTool_QTS"].SharedProps.Visible = true;
		}
		else
		{
			ultraToolbarsManager1.Tools["mnuTool_QTS"].SharedProps.Visible = false;
		}
	}

	private void LoadSettings()
	{
		try
		{
			Application.DoEvents();
			GetIniSetting();
			Application.DoEvents();
			chkReCalcu.Checked = CommonMethods.GetIniValue("BreakDown", "AutoReCalcu") == "1";
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormMrsBaseBreakdown.cs" + ex.Message);
		}
	}

	private void GetIniSetting()
	{
		GridPropertySetting.LoadGridProperty(F_UserID, base.Name, gridMrsBase1);
		string Status = CommonMethods.IniReadValue(CommonMethods.ExtractFilePath(Application.ExecutablePath) + "PccesMain.ini", "BreakDown", "WindowState");
	}

	private void ultraButton2_Click(object sender, EventArgs e)
	{
		if (ultraButton2.Text == "...")
		{
			ultraButton2.Text = "V";
			txtAnalysisQty.Location = lblAnalysisQty.Location;
			txtAnalysisQty.Size = lblAnalysisQty.Size;
			txtAnalysisQty.Text = lblAnalysisQty.Text;
			lblAnalysisQty.Visible = false;
			txtAnalysisQty.Visible = true;
			txtAnalysisQty.Focus();
		}
		else
		{
			ultraButton2.Text = "...";
			lblAnalysisQty.Text = txtAnalysisQty.Text;
			lblAnalysisQty.Visible = true;
			txtAnalysisQty.Visible = false;
			SaveBreakdown("1");
		}
	}

	private void BtnLevelUp_Click(object sender, EventArgs e)
	{
		if (iLayer != 1)
		{
			gridMrsBase1.Col = 0;
			iLayer--;
			parentPubCode = Convert.ToInt32(saLayer[iLayer]);
			iCostDigital = Convert.ToInt32(saLayerCostDec[iLayer]);
			GetUpperData();
			GetLowerData();
			gridMrsBase1.Row = PubTools.Str2Int(aaLayer[iLayer - 1]);
		}
		else
		{
			if (base.Owner is frmBudget)
			{
				RememberColsProps();
				(base.Owner as frmBudget)._GridColsSquenceForAnalysis = GridColsSquence;
			}
			Close();
		}
	}

	private void ultraButton3_Click(object sender, EventArgs e)
	{
		if (base.Owner is frmBudget)
		{
			RememberColsProps();
			(base.Owner as frmBudget)._GridColsSquenceForAnalysis = GridColsSquence;
		}
		Close();
	}

	private void gridMrsBase1_DoubleClick(object sender, EventArgs e)
	{
		int rowIndex = gridMrsBase1.MouseRow;
		int colIndex = gridMrsBase1.MouseCol;
		if (gridMrsBase1.Cols[colIndex].Name.ToUpper() == "COST")
		{
			bool showMessage = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "BreakDownData", "NoMessage").ToUpper() != "TRUE";
			object Lock = gridMrsBase1[rowIndex, "Lock"];
			if (Lock != null && Lock != DBNull.Value && Convert.ToBoolean(Lock))
			{
				ArrayList aArr = new ArrayList();
				aArr.Clear();
				aArr.Add(F_UserID);
				aArr.Add("檢查前期單價分析母項是否已存在--" + F_ProjectCode + "(" + lblPccesCode.Text + ")");
				Archnowledge.Pcces.BUDClass.MrsBaseA MRSA = new Archnowledge.Pcces.BUDClass.MrsBaseA(F_UserID, aArr);
				MRSA.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
				MRSA.ps_projectcode = F_ProjectCode;
				bool IsExisted = MRSA.IsExistPccesCodeByVersion(F_ProjectCode, lblPccesCode.Text, (base.Owner as frmBudget)._BudgetChangeCurrentVersion - 1);
				string cstKind = ArchConvert.Obj2String(gridMrsBase1[rowIndex, "CostKind"]).Trim();
				if (IsExisted)
				{
					if (showMessage)
					{
						MessageBox.Show(this, "此工項為前一版次預算書之項目，不可編輯單價", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
					gridMrsBase1.Col = 0;
					inBeforeEdit = false;
					IsGoIntoBeforeEdit = false;
					return;
				}
				if ((IsExisted || !(cstKind == "$")) && !IsExisted && cstKind == string.Empty && MRSA.IsExistPccesCodeByVersion(F_ProjectCode, gridMrsBase1[rowIndex, "pccesCode"].ToString(), (base.Owner as frmBudget)._BudgetChangeCurrentVersion - 1))
				{
					if (showMessage)
					{
						MessageBox.Show(this, "此工項為前一版次預算書之項目，不可編輯單價", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
					gridMrsBase1.Col = 0;
					inBeforeEdit = false;
					IsGoIntoBeforeEdit = false;
					return;
				}
			}
		}
		Archnowledge.Pcces.CommonClass.DebugUtil.OutputDebugString("gridMrsBase1_DoubleClick  sBindFlag=" + sBindFlag);
		try
		{
			if (gridMrsBase1.Cols[colIndex].Name == "AnaImg" && (bool)gridMrsBase1[rowIndex, "Analysis"])
			{
				GoNextLevelAnalysis(rowIndex, colIndex);
			}
			else if (!(gridMrsBase1.Cols[colIndex].Name == "Rate") && !(gridMrsBase1.Cols[colIndex].Name == "CostKind"))
			{
				if (F_IsSBID || (F_ActionName == PccesFormAction.SplitContract && ContractApproved) || gridMrsBase1.Cols[colIndex].AllowEditing)
				{
					return;
				}
				F_DoubleET = "TRUE";
				ExecuteEditItem();
				F_DoubleET = "";
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormMrsBaseBreakdown.cs" + ex.Message);
		}
		ResetToolbar();
	}

	private void txtAnalysisQty_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			ChangeAnalysisState("Enter");
			SaveBreakdown("1");
		}
		else if (e.KeyChar == '\u001b')
		{
			ChangeAnalysisState("");
		}
	}

	private void tlBtnDelete_Click(object sender, EventArgs e)
	{
		ArrayList SelItems_Pub = new ArrayList();
		ArrayList SelItems_Lst = new ArrayList();
		for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
		{
			if (gridMrsBase1.Rows[i].Selected)
			{
				SelItems_Pub.Add(gridMrsBase1[i, "PubCode"]);
				SelItems_Lst.Add(gridMrsBase1[i, "ListNo"]);
			}
		}
		string sQuestionStr = "確定要刪除選取的這 " + SelItems_Pub.Count + " 筆?";
		if (MessageBox.Show(this, sQuestionStr, CommonMethods.GetFormTypeTitle(FormType.MrsBaseAnalysis), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
		{
			return;
		}
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add(CommonMethods.GetFormTypeTitle(FormType.MrsBase));
		Archnowledge.Pcces.BUDClass.MrsBaseB MrsBaseB1 = new Archnowledge.Pcces.BUDClass.MrsBaseB(aArr);
		MrsBaseB1.ps_projectcode = ProjectCode;
		MrsBaseB1.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		MrsBaseB1.ps_parentCode = parentPubCode.ToString();
		MrsBaseB1.ps_Issue = F_chgCount;
		for (int i = SelItems_Pub.Count - 1; i >= 0; i--)
		{
			try
			{
				int iRow = Get_RealRow1(SelItems_Pub[i].ToString(), SelItems_Lst[i].ToString());
				ComsWebService theComsWebService = new ComsWebService(F_ProjectCode);
				int sNo = ArchConvert.Obj2Int(gridMrsBase1[iRow, "sNo"]);
				if (!theComsWebService.AllowChangeBysNo(-1, sNo, silent: false))
				{
					gridMrsBaseDataBind();
					return;
				}
				MrsBaseB1.ps_pubCode = Convert.ToString((int)SelItems_Pub[i]);
				MrsBaseB1.ps_listNo = gridMrsBase1[iRow, "ListNo"].ToString();
				MrsBaseB1.DeleItemWithoutReListNo();
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormMrsBaseBreakdown.cs" + ex.Message);
				int iRow = Get_RealRow3(SelItems_Lst[i].ToString());
				Console.Write(ex.Message);
				string strTableB = "mrsBaseB";
				string strFn = CommonMethods.GetActionNameString(F_ActionName);
				if (strFn.ToUpper() == "BUD")
				{
					strTableB = "budProjMrsB";
				}
				else if (strFn.ToUpper() == "BID")
				{
					strTableB = "bidProjMrsB";
				}
				else if (strFn.ToUpper() == "SUB")
				{
					strTableB = "SubProjMrsB";
				}
				else if (strFn.ToUpper() == "SUBCHG")
				{
					strTableB = "SubChgProjMrsB";
				}
				DBClass DBCLS = new DBClass();
				DBCLS._FS_UserID = F_UserID;
				if (strTableB == "mrsBaseB")
				{
					DBCLS.ExecuteCommand("Delete " + strTableB + " Where ParentCode=" + Convert.ToString((int)SelItems_Pub[i]) + " and listNo=" + gridMrsBase1[iRow, "ListNo"].ToString() + "");
				}
				else if (strFn.ToUpper() != "SUBCHG")
				{
					DBCLS.ExecuteCommand("Delete " + strTableB + " Where ProjectCode ='" + F_ProjectCode + "' And ParentCode=" + parentPubCode + " and listNo=" + gridMrsBase1[iRow, "ListNo"].ToString() + "");
				}
				else
				{
					DBCLS.ExecuteCommand("Delete " + strTableB + " Where ProjectCode ='" + F_ProjectCode + "' And ParentCode=" + parentPubCode + " and listNo=" + gridMrsBase1[iRow, "ListNo"].ToString() + " and chgCount ='" + F_chgCount + "'");
				}
			}
		}
		DataTable ldt_tmp = MrsBaseB1.ListItem(parentPubCode);
		int li_no = 0;
		if (PubTools.GetAppSet_Bool("UseNewMrsB"))
		{
			ldt_tmp.Columns.Add("NewListNo", Type.GetType("System.Int32"));
			foreach (DataRow dr in ldt_tmp.Rows)
			{
				li_no++;
				dr["NewListNo"] = li_no;
			}
			MrsBaseB1.ReSetListNo(ldt_tmp);
		}
		else
		{
			foreach (DataRow dr in ldt_tmp.Rows)
			{
				li_no++;
				MrsBaseB1.ps_listNo = li_no.ToString();
				MrsBaseB1.ps_pubCode = dr["pubcode"].ToString();
				MrsBaseB1.UpdItem();
			}
		}
		GetLowerData();
		if (chkReCalcu.Checked)
		{
			DoMrsCalculate();
		}
	}

	private int Get_RealRow1(string sPubCode, string sListNo)
	{
		int RetV = -1;
		for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
		{
			if (gridMrsBase1[i, "PubCode"].ToString() == sPubCode && gridMrsBase1[i, "ListNo"].ToString() == sListNo)
			{
				RetV = i;
				break;
			}
		}
		return RetV;
	}

	private int Get_RealRow2(string sPubCode)
	{
		int RetV = -1;
		for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
		{
			if (gridMrsBase1[i, "PSNo"].ToString() == sPubCode)
			{
				RetV = i;
				break;
			}
		}
		return RetV;
	}

	private int Get_RealRow2(int iPubCode)
	{
		int RetV = -1;
		for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
		{
			if (gridMrsBase1[i, "PubCode"].ToString() == iPubCode.ToString().Trim())
			{
				RetV = i;
				break;
			}
		}
		return RetV;
	}

	private int Get_RealRow3(string sListNo)
	{
		int RetV = -1;
		for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
		{
			if (gridMrsBase1[i, "ListNo"].ToString() == sListNo)
			{
				RetV = i;
				break;
			}
		}
		return RetV;
	}

	private void tlBtnReCal_Small_Click(object sender, EventArgs e)
	{
		DoMrsCalculate();
		MessageBox.Show(this, "重新小計完成!!", FORM_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	private void tlBtnCalculator_Click(object sender, EventArgs e)
	{
		((PopupControlContainerTool)ultraToolbarsManager1.Tools["PopupMenuCalculator"]).ShowPopup();
	}

	private void tlBtnNew_Click(object sender, EventArgs e)
	{
		((PopupMenuTool)ultraToolbarsManager1.Tools["PopupMenuNew"]).ShowPopup();
	}

	private void DoMenuAction(string MenuID)
	{
		RememberColsProps();
		switch (MenuID)
		{
		case "PopupMenuCalculator":
			break;
		case "PopupMenu1":
			break;
		case "mnuCut":
			MoveUpDownFlag = "";
			MenuCut();
			break;
		case "mnuCopy":
			MoveUpDownFlag = "";
			MenuCopy();
			break;
		case "mnuPaste":
			MoveUpDownFlag = "";
			LoadDataSetFromMem();
			MenuPaste();
			break;
		case "PopupMenuNew":
			break;
		case "mnuCopyToNew":
			MoveUpDownFlag = "";
			CopyToNew();
			break;
		case "mnuEdit":
			ExecuteEditItem();
			break;
		case "mnuDelete":
			tlBtnDelete_Click(this, EventArgs.Empty);
			break;
		case "mnuAnalysis":
			GoNextLevelAnalysis(gridMrsBase1.Row, gridMrsBase1.Cols["Analysis"].SafeIndex);
			break;
		case "mnuNewItem":
			sInsertCallerMenu = "mnuNewItem";
			ExecuteNewItem();
			break;
		case "mnuPickItem":
			sInsertCallerMenu = "mnuPickItem";
			Execute_Addnew();
			break;
		case "mnuPickFromProj":
			sInsertCallerMenu = "mnuPickFromProj";
			Execute_ProjAddNew();
			break;
		case "mnuTool_Up":
			tlMoveUp();
			break;
		case "mnuTool_Down":
			tlMoveDown();
			break;
		case "mnuTool_ReCal_Small":
			DoMrsCalculate();
			break;
		case "UseSingle":
			Do_UseSingle();
			break;
		case "UseMulti":
			Do_UseMulti();
			break;
		case "SendSingle":
			Do_SendSingle();
			break;
		case "SendMulti":
			Do_SendMulti();
			break;
		case "mnuIRSet":
			Execute_IRSet();
			break;
		case "mnuTool_QTS":
			Execute_QTS();
			break;
		case "mnuQTS_Caller":
			Call_QTS();
			break;
		case "mnuIRCopy":
			Call_IRCopy();
			break;
		case "mnuIRPaste":
			Call_IRPaste();
			break;
		case "mnuUseMrsCost":
			Do_UseMrsCost();
			break;
		case "mnuQryParent":
			Do_QryParent();
			break;
		case "mnuUseAdjCost":
			BtnAdjust_Click(null, null);
			break;
		case "mnuCopyforDetail":
		{
			DataTable dtAnalysis = (base.Owner as frmBudget).dtClipboard.Clone();
			for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
			{
				if (gridMrsBase1.Rows[i].Selected)
				{
					DataRow DR1 = dtAnalysis.NewRow();
					DR1["CName"] = gridMrsBase1[i, "CName"];
					DR1["UnitName"] = gridMrsBase1[i, "UnitName"];
					DR1["Qty"] = gridMrsBase1[i, "Qty"];
					DR1["Cost"] = gridMrsBase1[i, "Cost"];
					DR1["Amount"] = gridMrsBase1[i, "Amount"];
					DR1["PccesCode"] = gridMrsBase1[i, "PccesCode"];
					DR1["Memo"] = gridMrsBase1[i, "Memo"];
					DR1["Kind"] = "W";
					DR1["Analysis"] = gridMrsBase1[i, "Analysis"];
					DR1["PubCode"] = gridMrsBase1[i, "PubCode"];
					dtAnalysis.Rows.Add(DR1);
				}
			}
			(base.Owner as frmBudget).Do_Ana_Copy(dtAnalysis);
			break;
		}
		}
	}

	private void Do_QryParent()
	{
		int iPubCode = (int)gridMrsBase1[gridMrsBase1.Row, "PubCode"];
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("WinFORM 基本工料--父項查詢");
		Archnowledge.Pcces.BUDClass.MrsBaseA dbMrsBase = new Archnowledge.Pcces.BUDClass.MrsBaseA(F_UserID, aArr);
		dbMrsBase.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		dbMrsBase.ps_projectcode = F_ProjectCode;
		DataTable DT_Parent = dbMrsBase.ListParentItem(iPubCode.ToString());
		if (DT_Parent.Rows.Count > 0)
		{
			DataView DV1 = DT_Parent.DefaultView;
			DV1.Sort = " pccesCode ASC ";
			CellStyle CS1 = gridMrsBase2.Styles.Add("AnalysisColor");
			CellStyle CS2 = gridMrsBase2.Styles.Add("LEMColor");
			CellStyle CS3 = gridMrsBase2.Styles.Add("WColor");
			CS1.ForeColor = Color.Red;
			CS2.ForeColor = Color.Teal;
			CS3.ForeColor = Color.Purple;
			gridMrsBase2.Select();
			gridMrsBase2.Rows.Count = DV1.Count + 1;
			string sItemClass = "";
			for (int i = 0; i < DV1.Count; i++)
			{
				sItemClass = DV1[i]["pccesCode"].ToString().Substring(0, 1);
				gridMrsBase2[i + 1, "PccesCode"] = DV1[i]["pccesCode"].ToString();
				if (sItemClass == "L" || sItemClass == "E" || sItemClass == "M")
				{
					gridMrsBase2.Rows[i + 1].Style = gridMrsBase2.Styles["LEMColor"];
				}
				else if (sItemClass == "W")
				{
					gridMrsBase2.Rows[i + 1].Style = gridMrsBase2.Styles["WColor"];
				}
				gridMrsBase2[i + 1, "CName"] = DV1[i]["cName"].ToString();
				if (DV1[i]["analysis"].ToString().Trim() == "1")
				{
					gridMrsBase2[i + 1, "Analysis"] = true;
					gridMrsBase2.Rows[i + 1].Style = gridMrsBase2.Styles["AnalysisColor"];
					CellRange rg = gridMrsBase2.GetCellRange(i + 1, gridMrsBase2.Cols["AnaImg"].SafeIndex);
					rg.Style = gridMrsBase2.Styles["img"];
					rg.Image = imageList2.Images[0];
				}
				else
				{
					gridMrsBase2[i + 1, "Analysis"] = false;
				}
				gridMrsBase2[i + 1, "UnitName"] = DV1[i]["unitName"];
				gridMrsBase2[i + 1, "Rate"] = DV1[i]["rate"];
				gridMrsBase2[i + 1, "CostKind"] = DV1[i]["costKind"];
				gridMrsBase2[i + 1, "LRate"] = DV1[i]["lRate"];
				gridMrsBase2[i + 1, "ERate"] = DV1[i]["eRate"];
				gridMrsBase2[i + 1, "MRate"] = DV1[i]["mRate"];
				gridMrsBase2[i + 1, "WRate"] = DV1[i]["wRate"];
				gridMrsBase2[i + 1, "XNameC"] = DV1[i]["xNameC"];
				gridMrsBase2[i + 1, "Memo"] = DV1[i]["memo"];
				gridMrsBase2[i + 1, "PubCode"] = DV1[i]["pubCode"];
				gridMrsBase2[i + 1, "Cost"] = DV1[i]["cost"];
				gridMrsBase2[i + 1, "usrQty"] = DV1[i]["usrQty"];
				gridMrsBase2[i + 1, "usrAmt"] = DV1[i]["usrAmt"];
			}
			StatusBar2.Panels[0].Text = " 資料筆數:" + DV1.Count;
			pnlInfo.Width = 250;
			Tab_B.Tab.Selected = true;
		}
		else
		{
			pnlInfo.Width = 0;
			MessageBox.Show(this, "查無父項資料!", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void Do_QryParentBudgetChangeHistory(object sender, EventArgs e)
	{
		FormBudgetWorkItemChangeHistory FBWorkItemChangeHistory = new FormBudgetWorkItemChangeHistory();
		FBWorkItemChangeHistory._ProjectCode = F_ProjectCode;
		FBWorkItemChangeHistory._UserID = F_UserID;
		FBWorkItemChangeHistory._PubCode = ArchConvert.Obj2Int(gridMrsBase2[gridMrsBase2.Row, "pubCode"]);
		FBWorkItemChangeHistory.ShowDialog();
	}

	private void gridMrsBase2_MouseDown(object sender, MouseEventArgs e)
	{
		int rowIndex = gridMrsBase2.MouseRow;
		int colIndex = gridMrsBase2.MouseCol;
		if (colIndex >= gridMrsBase2.Cols.Fixed && rowIndex >= gridMrsBase2.Rows.Fixed)
		{
			gridMrsBase2.Row = rowIndex;
		}
	}

	private void Do_UseMrsCost()
	{
		if (gridMrsBase1.SelectedItems <= 0)
		{
			MessageBox.Show(this, "請先選擇要用的項目。", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		else
		{
			if (MessageBox.Show(this, "確定要引用基本資料庫的單價?\n\n如果選取的項目是【單價分析項目】" + (SysConfig.SysComsEnable ? "或【已進入預算控管之不可修改固定單價工項】" : "") + "，該項目將不會執行引用。", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
			{
				return;
			}
			FormSys_G_Info1 FM_INFO = new FormSys_G_Info1();
			FM_INFO._InfoString = "引用基本資料庫的單價中，請稍候! ";
			FM_INFO.Show();
			Application.DoEvents();
			Cursor = Cursors.WaitCursor;
			DBClass DBCLS = new DBClass();
			DBCLS._FS_UserID = F_UserID;
			string IPStr = CommonMethods.GetIPAddress();
			ArrayList aArr = new ArrayList();
			aArr.Clear();
			aArr.Add(F_UserID);
			aArr.Add("引用基本資料庫的單價中--" + F_ProjectCode + "(" + IPStr + ")");
			Archnowledge.Pcces.BUDClass.MrsBaseA MRSA = new Archnowledge.Pcces.BUDClass.MrsBaseA(F_UserID, aArr);
			Archnowledge.Pcces.BUDClass.MrsBaseB MrsBaseB1 = new Archnowledge.Pcces.BUDClass.MrsBaseB(aArr);
			MRSA.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
			MRSA.ps_projectcode = F_ProjectCode;
			for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
			{
				if ((bool)gridMrsBase1.Rows[i]["Analysis"])
				{
					continue;
				}
				BudProjMrsA theMrsA = new BudProjMrsA();
				if (!gridMrsBase1.Rows[i].Selected)
				{
					continue;
				}
				string sPccesCode = gridMrsBase1.Rows[i]["PccesCode"].ToString().Trim();
				if (SysConfig.SysComsEnable)
				{
					bool DisabledByComs = false;
					if (SysConfig.SysChangeManagement && !theMrsA.CheckWorkItemPriceCanChange(F_ProjectCode, sPccesCode))
					{
						DisabledByComs = true;
					}
					if (!DisabledByComs && SysConfig.SysEditAfterBudLem.ToUpper() == "DISABLE")
					{
						Archnowledge.Pcces.DomainModule.Coms.BudgetCtrl theBudgetCtrl = new Archnowledge.Pcces.DomainModule.Coms.BudgetCtrl();
						if (!theBudgetCtrl.IsWorkItemCostCanChange(F_ProjectCode, SysConfig.SysComsDB, sPccesCode))
						{
							DisabledByComs = true;
						}
					}
					if (DisabledByComs)
					{
						continue;
					}
				}
				string sSQL = "Select cost From MrsBaseA Where PccesCode ='" + sPccesCode + "' ";
				string sCost = DBCLS.GetUserDefine_String(sSQL, "cost");
				MrsBaseB1.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
				MrsBaseB1.ps_projectcode = F_ProjectCode;
				MrsBaseB1.ps_parentCode = parentPubCode.ToString();
				MrsBaseB1.ps_Issue = F_chgCount;
				MrsBaseB1.ps_pubCode = gridMrsBase1[i, "PubCode"].ToString();
				MrsBaseB1.ps_cost = gridMrsBase1[i, "Cost"].ToString();
				MrsBaseB1.ps_listNo = gridMrsBase1[i, "ListNo"].ToString();
				MrsBaseB1.UpdItem();
				MRSA.ps_pccesCode = sPccesCode;
				MRSA.ps_Issue = F_chgCount;
				MRSA.ps_cost = sCost;
				MRSA.UpdItem();
			}
			GetLowerData();
			FM_INFO.Close();
			FM_INFO = null;
			Cursor = Cursors.Default;
			MessageBox.Show(this, "單價引用完畢。", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void Call_IRCopy()
	{
		Cursor = Cursors.WaitCursor;
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(SetTotal) 單價分析公式設定");
		int li_ParentCode = parentPubCode;
		int li_PubCode = PubTools.Str2Int(gridMrsBase1[gridMrsBase1.Row, "pubCode"]);
		int li_ListNo = PubTools.Str2Int(gridMrsBase1[gridMrsBase1.Row, "PSNo"]);
		Archnowledge.Pcces.BUDClass.MrsBaseC MrsCCom = new Archnowledge.Pcces.BUDClass.MrsBaseC(tmp_AL1);
		MrsCCom.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		MrsCCom.ps_projectcode = F_ProjectCode;
		MrsCCom.ps_chgCount = F_chgCount;
		DT_IR_Temp = MrsCCom.GetIRList(parentPubCode, li_PubCode, li_ListNo);
		if (DT_IR_Temp.Rows.Count > 0)
		{
			ultraToolbarsManager1.Tools["mnuIRPaste"].SharedProps.Enabled = true;
		}
		else
		{
			ultraToolbarsManager1.Tools["mnuIRPaste"].SharedProps.Enabled = false;
		}
		Cursor = Cursors.Default;
	}

	private void Call_IRPaste()
	{
		Cursor = Cursors.WaitCursor;
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(SetTotal) 單價分析公式設定");
		int li_ParentCode = parentPubCode;
		int li_PubCode = PubTools.Str2Int(gridMrsBase1[gridMrsBase1.Row, "pubCode"]);
		int li_ListNo = PubTools.Str2Int(gridMrsBase1[gridMrsBase1.Row, "PSNo"]);
		Archnowledge.Pcces.BUDClass.MrsBaseC MrsCCom = new Archnowledge.Pcces.BUDClass.MrsBaseC(tmp_AL1);
		MrsCCom.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		MrsCCom.ps_projectcode = F_ProjectCode;
		MrsCCom.DeleItemAll(li_ParentCode, li_PubCode, li_ListNo);
		for (int i = 0; i < DT_IR_Temp.Rows.Count; i++)
		{
			if (PubTools.Str2Int(DT_IR_Temp.Rows[i]["ItemListNo"]) < li_ListNo)
			{
				int iItemCode = PubTools.Str2Int(DT_IR_Temp.Rows[i]["itemCode"].ToString());
				int iItemListNo = PubTools.Str2Int(DT_IR_Temp.Rows[i]["ItemListNo"].ToString());
				MrsCCom.InseItem(li_ParentCode, li_PubCode, iItemCode, li_ListNo, iItemListNo);
			}
		}
		if (pnlInfo.Width != 0 && F_IsUseIR)
		{
			Execute_IRSet();
		}
		Cursor = Cursors.Default;
	}

	private void Call_QTS()
	{
		if (File.Exists("C:\\QTS\\MENU.XLS"))
		{
			ShellExecute SHExe = new ShellExecute();
			SHExe.OwnerHandle = base.Handle;
			SHExe.Path = "C:\\QTS\\MENU.XLS";
			if (!SHExe.Execute())
			{
				MessageBox.Show(this, "你未安裝 Excel, 無法使用「工程數量計算」功能", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			SHExe = null;
		}
		else
		{
			MessageBox.Show(this, "C:\\QTS\\MENU.XLS，檔案不存在。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void Execute_QTS()
	{
		string strPath = "C:\\QTS";
		try
		{
			string oconstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strPath + ";Extended Properties=dBASE III;Persist Security Info=False";
			string sqlstr = "select * from book1";
			OleDbConnection ocon = new OleDbConnection(oconstr);
			OleDbDataAdapter oda = new OleDbDataAdapter(sqlstr, ocon);
			DataTable dt = new DataTable();
			oda.Fill(dt);
			ocon = null;
			oda = null;
			ArrayList tmp_AL1 = new ArrayList();
			tmp_AL1.Add(F_UserID);
			tmp_AL1.Add("QTS 轉入");
			Archnowledge.Pcces.BUDClass.MrsBaseB MrsBaseBCom = new Archnowledge.Pcces.BUDClass.MrsBaseB(tmp_AL1);
			MrsBaseBCom.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
			MrsBaseBCom.ps_projectcode = F_ProjectCode;
			MrsBaseBCom.ps_parentCode = parentPubCode.ToString();
			int iGridCount = gridMrsBase1.Rows.Count;
			for (int i = 1; i < iGridCount && i <= dt.Rows.Count; i++)
			{
				try
				{
					Convert.ToDouble(dt.Rows[i - 1][0]);
				}
				catch
				{
					continue;
				}
				MrsBaseBCom.ps_pubCode = gridMrsBase1[i, "PubCode"].ToString().Trim();
				MrsBaseBCom.ps_qty = dt.Rows[i - 1][0].ToString().Trim();
				MrsBaseBCom.ps_listNo = gridMrsBase1[i, "ListNo"].ToString().Trim();
				MrsBaseBCom.ps_Issue = F_chgCount;
				MrsBaseBCom.UpdItem();
			}
			MrsBaseBCom = null;
			PubTools.WriteRoughlyLog(tmp_AL1);
			MessageBox.Show(this, "QTS 接收完成", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			DoMrsCalculate();
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormMrsBaseBreakdown.cs" + ex.Message);
			MessageBox.Show(this, "QTS 接收發生問題\n\n" + ex.Message, "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private bool IsLinkHasBroken(string sPccesCode, string sPunCode)
	{
		bool RetV = false;
		if (base.Owner is frmBudget)
		{
			DataTable srcDT = new DataTable();
			srcDT.Columns.Add("PccesCode", Type.GetType("System.String"));
			srcDT.Columns.Add("PubCode", Type.GetType("System.String"));
			DataRow DR = srcDT.NewRow();
			DR["PccesCode"] = lblPccesCode.Text.Trim();
			DR["PubCode"] = parentPubCode.ToString();
			srcDT.Rows.Add(DR);
			DataTable DT_Process = (base.Owner as frmBudget).FixPubCode(srcDT);
			for (int i = 0; i < DT_Process.Rows.Count; i++)
			{
				if (DT_Process.Rows[i]["PubCode"].ToString().Trim() != DT_Process.Rows[i]["resCode"].ToString().Trim() && DT_Process.Rows[i]["resCode"].ToString().Trim() != "")
				{
					if (DT_Process.Rows[i]["PubCode"].ToString().Trim() == parentPubCode.ToString())
					{
						parentPubCode = PubTools.Str2Int(DT_Process.Rows[i]["resCode"]);
					}
					RetV = true;
				}
			}
		}
		return RetV;
	}

	private void Do_UseSingle()
	{
		string ls_apubCode = parentPubCode.ToString();
		if (!CheckMrsAIsThing(ls_apubCode))
		{
			MessageBox.Show(this, "工項基本資料庫已無此單價分析資料\n\n如果要引用資料，請至基本資料庫重新建立。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return;
		}
		if (!CheckPccesCodeAndPubCode())
		{
			MessageBox.Show(this, "發現挑選單價分析之工項或單價分析內紅色項目曾經換碼，而與預算書內之工項衝突\n\n此狀況不允許引用基本資料庫，請校正後再執行。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		string sWarn = "嚴重警告：該項單價分析將被清空，並以【工項基本資料庫】取代。\n是否確定?";
		if (MessageBox.Show(this, sWarn, "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
		{
			string ls_srckind = CommonMethods.GetActionNameString(F_ActionName);
			string ls_projectcode = F_ProjectCode;
			ArrayList tmp_AL1 = new ArrayList();
			tmp_AL1.Add(F_UserID);
			tmp_AL1.Add("(GetPick) 單筆單價分析引用基本資料庫");
			ReSet2Mrs RST_2_MRS = new ReSet2Mrs(tmp_AL1);
			RST_2_MRS.ls_srckind = ls_srckind;
			RST_2_MRS.ls_apubCode = ls_apubCode;
			RST_2_MRS.ls_projectcode = ls_projectcode;
			RST_2_MRS.ls_Issue = F_chgCount;
			RST_2_MRS.Mrs2Proj();
			PubTools.WriteRoughlyLog(tmp_AL1);
			DoMrsCalculate();
		}
	}

	private void Do_UseMulti()
	{
		string ls_apubCode = parentPubCode.ToString();
		if (!CheckMrsAIsThing(ls_apubCode))
		{
			MessageBox.Show(this, "工項基本資料庫已無此單價分析資料\n\n如果要引用資料，請至基本資料庫重新建立。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return;
		}
		if (!CheckPccesCodeAndPubCode())
		{
			MessageBox.Show(this, "發現挑選單價分析之工項或單價分析內紅色項目曾經換碼，而與預算書內之工項衝突\n\n此狀況不允許引用基本資料庫，請校正後再執行。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		string sWarn = "嚴重警告：該項及其相關單價分析將被清空，並以【工項基本資料庫】取代。\n是否確定?";
		if (MessageBox.Show(this, sWarn, "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
		{
			string ls_srckind = CommonMethods.GetActionNameString(F_ActionName);
			string ls_projectcode = F_ProjectCode;
			ArrayList tmp_AL1 = new ArrayList();
			tmp_AL1.Add(F_UserID);
			tmp_AL1.Add("(GetPick) 引用多筆單價分析");
			ReSet2Mrs RST_2_MRS = new ReSet2Mrs(tmp_AL1);
			RST_2_MRS.ls_srckind = ls_srckind;
			RST_2_MRS.ls_apubCode = ls_apubCode;
			RST_2_MRS.ls_projectcode = ls_projectcode;
			RST_2_MRS.ls_Issue = (PubTools.Str2Int(F_chgCount) + 1).ToString();
			RST_2_MRS.AllMrs2Proj();
			PubTools.WriteRoughlyLog(tmp_AL1);
			DoMrsCalculate();
		}
	}

	private void Do_SendSingle()
	{
		if (IsLinkHasBroken(lblPccesCode.Text.Trim(), parentPubCode.ToString()))
		{
		}
		string sWarn = "嚴重警告：【工項基本資料庫】中該項 單價分析 將被清空，並以【本專案】取代。\n是否確定 ?";
		if (MessageBox.Show(this, sWarn, "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
		{
			string ls_srckind = CommonMethods.GetActionNameString(F_ActionName);
			string ls_projectcode = F_ProjectCode;
			string ls_apubCode = parentPubCode.ToString();
			ArrayList tmp_AL1 = new ArrayList();
			tmp_AL1.Add(F_UserID);
			tmp_AL1.Add("(GetPick) 單筆單價分析回傳基本資料庫");
			ReSet2Mrs RST_2_MRS = new ReSet2Mrs(tmp_AL1);
			RST_2_MRS.ls_srckind = ls_srckind;
			RST_2_MRS.ls_apubCode = ls_apubCode;
			RST_2_MRS.ls_projectcode = ls_projectcode;
			RST_2_MRS.ls_Issue = F_chgCount;
			RST_2_MRS.Proj2Mrs();
			PubTools.WriteRoughlyLog(tmp_AL1);
			DoMrsCalculate();
		}
	}

	private void Do_SendMulti()
	{
		if (IsLinkHasBroken(lblPccesCode.Text.Trim(), parentPubCode.ToString()))
		{
		}
		string sWarn = "嚴重警告：【工項基本資料庫】中該項及其相關單價分析將被清空，並以【本專案】取代。\n是否確定?";
		if (MessageBox.Show(this, sWarn, "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
		{
			string ls_srckind = CommonMethods.GetActionNameString(F_ActionName);
			string ls_projectcode = F_ProjectCode;
			string ls_apubCode = parentPubCode.ToString();
			ArrayList tmp_AL1 = new ArrayList();
			tmp_AL1.Add(F_UserID);
			tmp_AL1.Add("(GetPick) 回傳多筆單價分析");
			ReSet2Mrs RST_2_MRS = new ReSet2Mrs(tmp_AL1);
			RST_2_MRS.ls_srckind = ls_srckind;
			RST_2_MRS.ls_apubCode = ls_apubCode;
			RST_2_MRS.ls_projectcode = ls_projectcode;
			RST_2_MRS.ls_Issue = F_chgCount;
			RST_2_MRS.AllProj2Mrs();
			PubTools.WriteRoughlyLog(tmp_AL1);
			DoMrsCalculate();
		}
	}

	private bool CheckPccesCodeAndPubCode()
	{
		bool IsOK = true;
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("自基本資料庫插入工項至預算書--檢查PCCES及PUBCODE是否皆一致");
		CellStyle CS_Chk = gridMrsBase1.Styles.Add("CS_Chk");
		CS_Chk.BackColor = Color.LightPink;
		string sProjectCode = ProjectCode;
		ModifyDB stdClass = new ModifyDB(ProjectCode, tmp_AL1);
		DataTable dtMrsBaseA = stdClass.DBList("Select pccesCode, pubCode from MrsBaseA ");
		DataView dv = new DataView(dtMrsBaseA);
		dv.RowFilter = "pubCode = '" + parentPubCode + "' and pccesCode<>'" + lblPccesCode.Text.Trim() + "'";
		if (dv.Count > 0)
		{
			IsOK = false;
		}
		if (dtMrsBaseA.Rows.Count > 0 && gridMrsBase1.Rows.Count > 1)
		{
			for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
			{
				string PccesCode = gridMrsBase1[i, "pccesCode"].ToString().Trim();
				string pubCode = gridMrsBase1[i, "pubCode"].ToString().Trim();
				dv.RowFilter = "pubCode = '" + pubCode + "' and pccesCode<>'" + PccesCode + "'";
				if (dv.Count > 0)
				{
					CellRange rg = gridMrsBase1.GetCellRange(i, 1, i, gridMrsBase1.Cols.Count - 1);
					rg.Style = gridMrsBase1.Styles["CS_Chk"];
					IsOK = false;
				}
			}
		}
		dv.Dispose();
		dtMrsBaseA.Dispose();
		stdClass = null;
		tmp_AL1 = null;
		return IsOK;
	}

	private bool CheckMrsAIsThing(string l_strPub)
	{
		Archnowledge.Pcces.DomainModule.MrsBase.MrsBaseA theMrsBaseA = new Archnowledge.Pcces.DomainModule.MrsBase.MrsBaseA();
		return theMrsBaseA.WorkItemExistsByPubCode(parentPubCode);
	}

	private void Execute_IRSet()
	{
		pnlInfo.Width = ((pnlInfo.Width == 0) ? 250 : pnlInfo.Width);
		Tab_A.Tab.Selected = true;
		string ls_ParentCode = parentPubCode.ToString();
		string ls_PubCode = gridMrsBase1[gridMrsBase1.Row, "PubCode"].ToString();
		string ls_srcKind = CommonMethods.GetActionNameString(F_ActionName);
		string ls_ProjectCode = F_ProjectCode;
		ls_PubCode = ((!PubTools.GetAppSet_Bool("UseNewMrsB")) ? gridMrsBase1[gridMrsBase1.Row, "pubCode"].ToString().Trim() : gridMrsBase1[gridMrsBase1.Row, "ListNo"].ToString().Trim());
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(SetTotal) 單價分析公式設定");
		int li_ParentCode = PubTools.Str2Int(ls_ParentCode);
		int li_PubCode = PubTools.Str2Int(ls_PubCode);
		int li_ListNo = PubTools.Str2Int(gridMrsBase1[gridMrsBase1.Row, "ListNo"].ToString().Trim());
		Archnowledge.Pcces.BUDClass.MrsBaseB MrsBCom = new Archnowledge.Pcces.BUDClass.MrsBaseB(tmp_AL1);
		MrsBCom.ps_srckind = ls_srcKind;
		MrsBCom.ps_projectcode = ls_ProjectCode;
		MrsBCom.ps_Issue = F_chgCount;
		DataTable ldt_Tmp = MrsBCom.ListItem(li_ParentCode);
		foreach (DataRow dr in ldt_Tmp.Rows)
		{
			if (PubTools.GetAppSet_Bool("UseNewMrsB"))
			{
				if (PubTools.Str2Int(dr["ListNo"].ToString()) == li_PubCode)
				{
					li_ListNo = li_PubCode;
					li_PubCode = PubTools.Str2Int(dr["PubCode"].ToString());
					break;
				}
			}
			else if (PubTools.Str2Int(dr["PubCode"].ToString()) == li_PubCode)
			{
				li_ListNo = PubTools.Str2Int(dr["ListNo"].ToString());
			}
		}
		MrsBCom = null;
		string ls_ListNo = li_ListNo.ToString();
		ls_PubCode = li_PubCode.ToString();
		Archnowledge.Pcces.BUDClass.MrsBaseC MrsCCom = new Archnowledge.Pcces.BUDClass.MrsBaseC(tmp_AL1);
		MrsCCom.ps_srckind = ls_srcKind;
		MrsCCom.ps_projectcode = ls_ProjectCode;
		MrsCCom.ps_chgCount = F_chgCount;
		Cursor = Cursors.WaitCursor;
		if (PubTools.GetAppSet_Bool("UseNewMrsB"))
		{
			ldt_Analysis = MrsCCom.ListItem(li_ParentCode, li_PubCode, li_ListNo);
		}
		else
		{
			ldt_Analysis = MrsCCom.ListItem(li_ParentCode, li_PubCode);
		}
		MrsCCom = null;
		c1FlexGrid1.Rows.Count = ldt_Analysis.Rows.Count + 1;
		c1FlexGrid1.Visible = false;
		c1FlexGrid1.Redraw = false;
		double sel_Amount = 0.0;
		for (int i = 0; i < ldt_Analysis.Rows.Count; i++)
		{
			c1FlexGrid1[i + 1, "IsCheck"] = (bool)ldt_Analysis.Rows[i]["Chk"];
			c1FlexGrid1[i + 1, "ListNo"] = ldt_Analysis.Rows[i]["ListNo"].ToString().Trim();
			c1FlexGrid1[i + 1, "PccesCode"] = ldt_Analysis.Rows[i]["PccesCode"].ToString().Trim();
			c1FlexGrid1[i + 1, "cName"] = ldt_Analysis.Rows[i]["cName"].ToString().Trim();
			c1FlexGrid1[i + 1, "UnitName"] = ldt_Analysis.Rows[i]["UnitName"].ToString().Trim();
			c1FlexGrid1[i + 1, "PubCode"] = ldt_Analysis.Rows[i]["PubCode"].ToString().Trim();
			if ((bool)ldt_Analysis.Rows[i]["Chk"])
			{
				sel_Amount += PubTools.Str2Double(ldt_Analysis.Rows[i]["bamount"]);
			}
			if (i % 5 == 0)
			{
				Application.DoEvents();
			}
			Cursor = Cursors.WaitCursor;
		}
		StatusBar1.Visible = true;
		StatusBar1.Panels[0].Text = "資料筆數:" + (c1FlexGrid1.Rows.Count - 1);
		StatusBar1.Panels[1].Text = "加總=" + string.Format("{0:N" + F_AnaAmt + "}", sel_Amount);
		c1FlexGrid1.Redraw = true;
		c1FlexGrid1.Visible = true;
		Cursor = Cursors.Default;
	}

	private void Execute_ProjAddNew()
	{
		FormBudgetPickProjRes FM_BDGT_PKRES = new FormBudgetPickProjRes();
		FM_BDGT_PKRES._ActionName = F_ActionName;
		FM_BDGT_PKRES._ProjectCode = F_ProjectCode;
		FM_BDGT_PKRES._UserID = F_UserID;
		FM_BDGT_PKRES._CompanyDBName = CompanyDBName;
		FM_BDGT_PKRES.ShowDialog(this);
		FM_BDGT_PKRES.Close();
		FM_BDGT_PKRES = null;
	}

	private void Execute_Addnew()
	{
		FormMrsBaseBreakdown_Addnew BD_ADD = new FormMrsBaseBreakdown_Addnew();
		BD_ADD._CallFormName = base.Name;
		BD_ADD._UserID = F_UserID;
		BD_ADD._CompanyDBName = CompanyDBName;
		BD_ADD.ShowDialog(this);
		BD_ADD.Close();
		BD_ADD.Dispose();
		BD_ADD = null;
	}

	private void SaveSelectedRows()
	{
		DataSet DS1 = new DataSet("tempDS");
		DataTable DT1 = new DataTable("tempTable");
		for (int i = 1; i < gridMrsBase1.Cols.Count; i++)
		{
			if (gridMrsBase1.Cols[i].Name != "CostDec" && gridMrsBase1.Cols[i].Name != "AmtDec")
			{
				DataColumn DC = new DataColumn(gridMrsBase1.Cols[i].Name, gridMrsBase1.Cols[i].DataType);
				DT1.Columns.Add(DC);
			}
		}
		for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
		{
			if (!gridMrsBase1.Rows[i].Selected)
			{
				continue;
			}
			DataRow DR = DT1.NewRow();
			for (int j = 0; j < DT1.Columns.Count; j++)
			{
				if ((object)gridMrsBase1.Cols[DT1.Columns[j].ColumnName].DataType == Type.GetType("System.String") || gridMrsBase1[i, DT1.Columns[j].ColumnName] != null)
				{
					DR[gridMrsBase1.Cols[DT1.Columns[j].ColumnName].Name] = gridMrsBase1[i, DT1.Columns[j].ColumnName];
				}
			}
			DT1.Rows.Add(DR);
		}
		DS1.Tables.Add(DT1);
		Clipboard.SetDataObject(DS1.GetXml(), copy: false);
	}

	private void LoadDataSetFromMem()
	{
		bool IsValidClipContent = true;
		DataSet DS_Tmp = new DataSet("tempDS");
		DataTable DT1_Tmp = new DataTable("tempTable");
		DS_Tmp.Tables.Add(DT1_Tmp);
		string ClipString = "";
		IDataObject iData;
		try
		{
			iData = Clipboard.GetDataObject();
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormMrsBaseBreakdown.cs" + ex.Message);
			MessageBox.Show(this, ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			IsValidClipContent = false;
			return;
		}
		if (iData.GetDataPresent(DataFormats.Text))
		{
			ClipString = (string)iData.GetData(DataFormats.Text);
		}
		if (ClipString != "")
		{
			try
			{
				StringReader SR = new StringReader(ClipString);
				DS_Tmp.ReadXml(SR, XmlReadMode.InferSchema);
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormMrsBaseBreakdown.cs" + ex.Message);
				IsValidClipContent = false;
			}
			if (!IsValidClipContent)
			{
				MessageBox.Show(this, "剪貼簿內的資料已經被變更了, 現在無法執行貼上!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
		}
		DS1 = DS_Tmp;
	}

	public void Th_MenuPaste(DataSet custDS1)
	{
		DS1 = custDS1;
		MenuPaste();
	}

	public void Th_MenuPaste(DataSet custDS1, string SrcProject)
	{
		srcProjectCode = SrcProject;
		DS1 = custDS1;
		MenuPaste();
	}

	public void Th_MenuPaste(DataSet custDS1, DataTable custDT1, string SrcProject)
	{
		DS1 = custDS1;
		DT_MultiDBTransfer = custDT1.Copy();
		MenuPaste();
	}

	private void MenuPaste()
	{
		gridMrsBase1.Enabled = false;
		FormSys_G_Info1 FM_INFO = new FormSys_G_Info1();
		FM_INFO.TopMost = true;
		FM_INFO._InfoString = "項目插入中，請稍候! ";
		FM_INFO.Show();
		Application.DoEvents();
		Cursor = Cursors.WaitCursor;
		SetPopupMenuDisable();
		Archnowledge.Pcces.DomainModule.Coms.BudgetCtrl theBudgetCtrl = new Archnowledge.Pcces.DomainModule.Coms.BudgetCtrl();
		BudProjMrsA theMrsA = new BudProjMrsA();
		string SrcDBName = _CompanyDBName;
		string SrcProjectCode = ProjectCode;
		lock (this)
		{
			ArrayList aArr = new ArrayList();
			aArr.Add(F_UserID);
			aArr.Add("WinFORM 基本工料");
			DataTable srcDT = new DataTable();
			srcDT.Columns.Add("pubCode", Type.GetType("System.Int32"));
			if (DT_MultiDBTransfer.Rows.Count > 0)
			{
				for (int j = 0; j < DT_MultiDBTransfer.Rows.Count; j++)
				{
					if (SysConfig.SysComsEnable)
					{
						bool DisabledByCOMS = false;
						if (SysConfig.SysChangeManagement)
						{
							SrcDBName = DT_MultiDBTransfer.Rows[j]["DBName"].ToString();
							SrcProjectCode = DT_MultiDBTransfer.Rows[j]["ProjectCode"].ToString();
							if (!theMrsA.CheckSourceItemCanOverwrite(SrcDBName, SrcProjectCode, DT_MultiDBTransfer.Rows[j]["PccesCode"].ToString(), ProjectCode))
							{
								DisabledByCOMS = true;
							}
						}
						if (!DisabledByCOMS && SysConfig.SysEditAfterBudLem.ToUpper() == "DISABLE" && theBudgetCtrl.IsWorkItemInSubPlanCart(ProjectCode, SysConfig.SysComsDB, DT_MultiDBTransfer.Rows[j]["PccesCode"].ToString()))
						{
							DisabledByCOMS = true;
						}
						if (DisabledByCOMS)
						{
							continue;
						}
					}
					DataRow srcDR = srcDT.NewRow();
					srcDR["PubCode"] = DT_MultiDBTransfer.Rows[j]["PubCode"];
					srcDT.Rows.Add(srcDR);
				}
				string ssDBName = DT_MultiDBTransfer.Rows[0]["DBName"].ToString().Trim();
				string ssProjectCode = DT_MultiDBTransfer.Rows[0]["ProjectCode"].ToString().Trim();
				ReSet2Mrs RESET2 = new ReSet2Mrs(aArr);
				RESET2.ls_Issue = (PubTools.Str2Int(F_chgCount) + 1).ToString();
				DataSet trgDS = RESET2.GetDataSet(ssDBName, CommonMethods.GetActionNameString(F_ActionName), ssProjectCode, srcDT, 1);
				RESET2.ls_Issue = F_chgCount;
				RESET2.InputDataSet(F_CurrentDBName, CommonMethods.GetActionNameString(F_ActionName), F_ProjectCode, trgDS, 1, "");
			}
			InsertList.Clear();
			if (gridMrsBase1.SelectedItems > 1)
			{
				MessageBox.Show(this, "請先選定一筆資料當為貼上的基準\n貼上的資料將會放置在該選取項之後", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			int iNewItem_StartRow = gridMrsBase1.Row + 1;
			if (iNewItem_StartRow <= 0)
			{
				iNewItem_StartRow = 1;
			}
			bool IsValidClipContent = true;
			Archnowledge.Pcces.BUDClass.MrsBaseB MrsBaseB1 = new Archnowledge.Pcces.BUDClass.MrsBaseB(aArr);
			DataTable DT1 = new DataTable("tempTable");
			DT1 = DS1.Tables[0].Copy();
			if (DT1.Rows.Count > 0)
			{
				bool IsUseNewMrsB = PubTools.GetAppSet_Bool("UseNewMrsB");
				if (!IsUseNewMrsB || !IsAllowRepeatItem)
				{
					for (int i = 0; i < DS1.Tables[0].Rows.Count; i++)
					{
						int iFIND = Get_RealRow2(PubTools.Str2Int(DS1.Tables[0].Rows[i]["PubCode"]));
						if (iFIND > 0)
						{
							FM_INFO.Hide();
							MessageBox.Show(this, "已有相同資料存在, 無法完成貼上動作!!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							IsValidClipContent = false;
							break;
						}
					}
				}
				if (IsValidClipContent)
				{
					int InsertRow = gridMrsBase1.Row;
					if (InsertRow <= 0)
					{
						InsertRow = 0;
					}
					bool SkipUpdate = false;
					DataTable dtParentPccesCode = theMrsA.GetAllParentPccesCode(ProjectCode, lblPccesCode.Text.Trim());
					DataView dvParentPccesCode = new DataView(dtParentPccesCode);
					string ConflictPccesCodeMsg = string.Empty;
					for (int i = 0; i < DS1.Tables[0].Rows.Count; i++)
					{
						SkipUpdate = false;
						if (SysConfig.SysComsEnable)
						{
							if (SysConfig.SysChangeManagement && !theMrsA.CheckSourceItemCanOverwrite(SrcDBName, SrcProjectCode, DS1.Tables[0].Rows[i]["PccesCode"].ToString(), ProjectCode))
							{
								SkipUpdate = true;
								string text = ConflictPccesCodeMsg;
								ConflictPccesCodeMsg = text + "工項:" + DS1.Tables[0].Rows[i]["PccesCode"].ToString() + "(" + DS1.Tables[0].Rows[i]["CName"].ToString() + ")已被鎖定不能覆蓋,其單價分析結構將不會被引用覆蓋\n";
							}
							if (!SkipUpdate && SysConfig.SysEditAfterBudLem.ToUpper() == "DISABLE" && theBudgetCtrl.IsWorkItemInSubPlanCart(ProjectCode, SysConfig.SysComsDB, DS1.Tables[0].Rows[i]["PccesCode"].ToString()))
							{
								SkipUpdate = true;
								string text = ConflictPccesCodeMsg;
								ConflictPccesCodeMsg = text + "工項:" + DS1.Tables[0].Rows[i]["PccesCode"].ToString() + "(" + DS1.Tables[0].Rows[i]["CName"].ToString() + ")已分包規劃不能覆蓋,其單價分析結構將不會被引用覆蓋\n";
							}
						}
						dvParentPccesCode.RowFilter = "PccesCode='" + DS1.Tables[0].Rows[i]["PccesCode"].ToString() + "'";
						if (dvParentPccesCode.Count > 0)
						{
							string text = ConflictPccesCodeMsg;
							ConflictPccesCodeMsg = text + "工項:" + DS1.Tables[0].Rows[i]["PccesCode"].ToString() + "(" + DS1.Tables[0].Rows[i]["CName"].ToString() + ")將產生循環參考,被跳過不新增\n";
							continue;
						}
						gridMrsBase1.AddItem("", iNewItem_StartRow);
						for (int j = 0; j < DS1.Tables[0].Columns.Count; j++)
						{
							if (DS1.Tables[0].Columns[j].ColumnName.ToUpper() == "LISTNO")
							{
								gridMrsBase1[iNewItem_StartRow, DS1.Tables[0].Columns[j].ColumnName] = InsertRow + i;
								continue;
							}
							try
							{
								int k = 1;
								while (i < gridMrsBase1.Cols.Count)
								{
									if (DS1.Tables[0].Columns[j].ColumnName.ToUpper() == gridMrsBase1.Cols[k].Name.ToString().ToUpper())
									{
										gridMrsBase1[iNewItem_StartRow, k] = DS1.Tables[0].Rows[i][j].ToString();
										break;
									}
									k++;
								}
							}
							catch (Exception ex)
							{
								CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormMrsBaseBreakdown.cs" + ex.Message);
								Console.Write(ex.Message);
							}
						}
						MrsBaseB1.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
						MrsBaseB1.ps_projectcode = F_ProjectCode;
						MrsBaseB1.ps_parentCode = parentPubCode.ToString();
						MrsBaseB1.ps_Issue = F_chgCount;
						if (F_ActionName == PccesFormAction.BUD || F_ActionName == PccesFormAction.BID || F_ActionName == PccesFormAction.SplitContract || F_ActionName == PccesFormAction.SubChange)
						{
							try
							{
								MrsBaseB1.ps_qty = DS1.Tables[0].Rows[i]["Qty"].ToString();
							}
							catch
							{
								MrsBaseB1.ps_qty = "1";
							}
						}
						else
						{
							try
							{
								MrsBaseB1.ps_qty = DS1.Tables[0].Rows[i]["Qty"].ToString();
							}
							catch
							{
								MrsBaseB1.ps_qty = "1";
							}
						}
						MrsBaseB1.ps_listNo = (InsertRow + i + 1).ToString();
						if (IsUseNewMrsB)
						{
							MrsBaseB1.MoveListNo(parentPubCode, InsertRow + i + 1);
						}
						InsertList.Add(InsertRow + i + 1);
						if (sInsertCallerMenu == "mnuPickFromProj")
						{
							DataTable DT_MrsData = new DataTable();
							DT_MrsData.Columns.Add("pubCode", Type.GetType("System.Int32"));
							DataRow DR = DT_MrsData.NewRow();
							DR["pubCode"] = DS1.Tables[0].Rows[i]["PubCode"];
							DT_MrsData.Rows.Add(DR);
							ModifyDB StdCom = new ModifyDB(F_ProjectCode, aArr);
							ReSet2Mrs RSMRS = new ReSet2Mrs(aArr);
							RSMRS.ls_Issue = (PubTools.Str2Int(F_chgCount) + 1).ToString();
							if (!SkipUpdate)
							{
								DataSet lds = RSMRS.GetDataSet(StdCom.ls_UseDataBase, MrsBaseB1.ps_srckind, srcProjectCode, DT_MrsData, 1);
								RSMRS.ls_projectcode = F_ProjectCode;
								RSMRS.ls_Issue = F_chgCount;
								RSMRS.InputDataSet(StdCom.ls_UseDataBase, MrsBaseB1.ps_srckind, F_ProjectCode, lds, 1, "");
							}
						}
						Archnowledge.Pcces.BUDClass.MrsBaseA MrsComA = new Archnowledge.Pcces.BUDClass.MrsBaseA(F_UserID, aArr);
						MrsComA.ps_srckind = "MRS";
						int mrsPubCode = MrsComA.Get_Pubcode(DS1.Tables[0].Rows[i]["PccesCode"].ToString().Trim());
						MrsBaseB1.ps_cost = DS1.Tables[0].Rows[i]["Cost"].ToString();
						if (F_ProjectCode.Trim() != "")
						{
							MrsComA.ps_projectcode = F_ProjectCode;
							MrsComA.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
							MrsComA.ps_Issue = F_chgCount;
							BudProjMrsA budProjMrsA = new BudProjMrsA();
							DataSet dsBudProjMrsA = budProjMrsA.GetProjMrsAByPccesCode(F_ProjectCode.Trim(), DS1.Tables[0].Rows[i]["PccesCode"].ToString().Trim());
							decimal newCost = ArchConvert.Obj2Decimal(DS1.Tables[0].Rows[i]["Cost"]);
							if (dsBudProjMrsA.Tables[0].Rows.Count > 0)
							{
								decimal originalCost = ArchConvert.Obj2Decimal(dsBudProjMrsA.Tables[0].Rows[0]["cost"]);
								mrsPubCode = ArchConvert.Obj2Int(dsBudProjMrsA.Tables[0].Rows[0]["PubCode"]);
								bool LockWorkItem = false;
								bool SubPlanWorkItem = false;
								if (SysConfig.SysChangeManagement)
								{
									LockWorkItem = !budProjMrsA.CheckWorkItemPriceCanChange(F_ProjectCode.Trim(), DS1.Tables[0].Rows[i]["PccesCode"].ToString().Trim());
								}
								if (SysConfig.SysEditAfterBudLem.ToUpper() == "DISABLE" && !LockWorkItem)
								{
									SubPlanWorkItem = !theBudgetCtrl.IsWorkItemCostCanChange(ProjectCode, SysConfig.SysComsDB, ArchConvert.Obj2String(DS1.Tables[0].Rows[i]["PccesCode"]));
								}
								if (LockWorkItem || SubPlanWorkItem)
								{
									if (LockWorkItem)
									{
										MessageBox.Show("工程代碼:" + DS1.Tables[0].Rows[i]["PccesCode"].ToString().Trim() + "單價不可修改(為單價分析或已存在上一版預算書),所以直接引用原專案工項單價");
									}
									else
									{
										MessageBox.Show("工程代碼:" + DS1.Tables[0].Rows[i]["PccesCode"].ToString().Trim() + "已分包規劃,單價不可修改,所以直接引用原專案工項單價");
									}
									MrsBaseB1.ps_cost = null;
								}
								else if (ArchConvert.Obj2String(dsBudProjMrsA.Tables[0].Rows[0]["costKind"]) != "$" && newCost != originalCost)
								{
									DialogResult result = MessageBox.Show("已有相同資料存在，是否引用新單價", "標題", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
									if (result == DialogResult.No)
									{
										MrsBaseB1.ps_cost = null;
									}
								}
							}
						}
						MrsBaseB1.ps_pubCode = mrsPubCode.ToString();
						MrsBaseB1.InseItem();
					}
					if (ConflictPccesCodeMsg != string.Empty)
					{
						MessageBox.Show(ConflictPccesCodeMsg);
					}
					GetLowerData();
					if (InsertList.Count > 0)
					{
						gridMrsBase1.Row = 0;
						for (int k = 0; k < InsertList.Count; k++)
						{
							gridMrsBase1.Rows[Convert.ToInt32(InsertList[k])].Selected = true;
						}
					}
				}
			}
			FM_INFO.Close();
			FM_INFO.Dispose();
			gridMrsBase1.Enabled = true;
			gridMrsBase1.Refresh();
			if (chkReCalcu.Checked)
			{
				DoMrsCalculate();
			}
		}
		SetPopupMenuEnable();
		gridMrsBase1.Enabled = true;
	}

	private void MenuCopy()
	{
		SaveSelectedRows();
	}

	private void MenuCut()
	{
		SaveSelectedRows();
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add("單價分析--剪下");
		Archnowledge.Pcces.BUDClass.MrsBaseB MrsBaseB1 = new Archnowledge.Pcces.BUDClass.MrsBaseB(aArr);
		MrsBaseB1.ps_projectcode = F_ProjectCode;
		MrsBaseB1.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		MrsBaseB1.ps_parentCode = parentPubCode.ToString();
		MrsBaseB1.ps_Issue = F_chgCount;
		for (int i = gridMrsBase1.Rows.Count - 1; i >= 1; i--)
		{
			if (gridMrsBase1.Rows[i].Selected)
			{
				MrsBaseB1.ps_pubCode = gridMrsBase1[i, "PubCode"].ToString();
				MrsBaseB1.ps_listNo = gridMrsBase1[i, "ListNo"].ToString();
				MrsBaseB1.DeleItem();
			}
		}
		for (int i = gridMrsBase1.Rows.Count - 1; i >= 1; i--)
		{
			if (gridMrsBase1.Rows[i].Selected)
			{
				gridMrsBase1.Rows.Remove(i);
			}
		}
		SaveReNumber("Cut");
	}

	private void CopyToNew()
	{
		int iNewItem_StartRow = gridMrsBase1.Row + 1;
		FormMrsBaseEdit FM_EDIT = new FormMrsBaseEdit();
		FM_EDIT._UserID = F_UserID;
		FM_EDIT._EditMode = MrsBaseEditFormType.CopyToNew;
		FM_EDIT._MainCost = F_MainCst.ToString();
		FM_EDIT._ActionName = F_ActionName;
		FM_EDIT._ProjectCode = F_ProjectCode;
		FM_EDIT._chgCount = F_chgCount;
		FM_EDIT.Owner = this;
		FM_EDIT._PubCode = (int)gridMrsBase1[gridMrsBase1.Row, "PubCode"];
		FM_EDIT._IsLocked = F_IsLocked;
		DialogResult theResult = FM_EDIT.ShowDialog();
		FM_EDIT.Close();
		FM_EDIT.Dispose();
		FM_EDIT = null;
		if (DialogResult.OK == theResult && F_NewChildPubCode != -1)
		{
			ArrayList aArr = new ArrayList();
			aArr.Add(F_UserID);
			aArr.Add("WinFORM 基本工料");
			Archnowledge.Pcces.BUDClass.MrsBaseB MrsBaseB1 = new Archnowledge.Pcces.BUDClass.MrsBaseB(aArr);
			MrsBaseB1.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
			MrsBaseB1.ps_parentCode = parentPubCode.ToString();
			MrsBaseB1.ps_pubCode = F_NewChildPubCode.ToString();
			MrsBaseB1.ps_cost = F_NewChildCost.ToString();
			MrsBaseB1.ps_qty = "0";
			MrsBaseB1.ps_Issue = F_chgCount;
			MrsBaseB1.ps_listNo = iNewItem_StartRow.ToString();
			MrsBaseB1.ps_projectcode = F_ProjectCode;
			MrsBaseB1.MoveListNo(parentPubCode, iNewItem_StartRow);
			int iTransationState = MrsBaseB1.InseItem();
			if (iTransationState == 2)
			{
				MessageBox.Show(this, "已有相同工項代碼資料存在，無法再新增!!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				DoMrsCalculate();
			}
		}
	}

	private void ExecuteNewItem()
	{
		int iNewItem_StartRow = gridMrsBase1.Row + 1;
		FormMrsBaseEdit FM_EDIT = new FormMrsBaseEdit();
		FM_EDIT._UserID = F_UserID;
		FM_EDIT._EditMode = MrsBaseEditFormType.New;
		FM_EDIT._MainCost = F_MainCst.ToString();
		FM_EDIT._ActionName = F_ActionName;
		FM_EDIT._ProjectCode = F_ProjectCode;
		FM_EDIT._chgCount = F_chgCount;
		FM_EDIT.Owner = this;
		FM_EDIT._IsLocked = F_IsLocked;
		DialogResult theResult = FM_EDIT.ShowDialog();
		if (DialogResult.OK == theResult && F_NewChildPubCode != -1)
		{
			ArrayList aArr = new ArrayList();
			aArr.Add(F_UserID);
			aArr.Add("WinFORM 基本工料");
			Archnowledge.Pcces.BUDClass.MrsBaseB MrsBaseB1 = new Archnowledge.Pcces.BUDClass.MrsBaseB(aArr);
			MrsBaseB1.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
			MrsBaseB1.ps_parentCode = parentPubCode.ToString();
			MrsBaseB1.ps_pubCode = F_NewChildPubCode.ToString();
			MrsBaseB1.ps_cost = F_NewChildCost.ToString();
			MrsBaseB1.ps_qty = ((FM_EDIT._costKind == "Z") ? "1" : "0");
			MrsBaseB1.ps_listNo = iNewItem_StartRow.ToString();
			MrsBaseB1.ps_projectcode = F_ProjectCode;
			MrsBaseB1.ps_Issue = F_Issue.ToString();
			MrsBaseB1.MoveListNo(parentPubCode, iNewItem_StartRow);
			int iTransationState = MrsBaseB1.InseItem();
			if (iTransationState == 2)
			{
				MessageBox.Show(this, "已有相同工項代碼資料存在，無法再新增!!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			DoMrsCalculate();
		}
		FM_EDIT.Close();
		FM_EDIT.Dispose();
		FM_EDIT = null;
	}

	private void ExecuteEditItem()
	{
		if (gridMrsBase1.Row <= 0)
		{
			return;
		}
		int RowIndex = gridMrsBase1.Row;
		int ChildPubCode = ArchConvert.Obj2Int(gridMrsBase1[RowIndex, "PubCode"]);
		int ListNo = ArchConvert.Obj2Int(gridMrsBase1[RowIndex, "ListNo"]);
		FormMrsBaseEdit FM_EDIT = new FormMrsBaseEdit();
		FM_EDIT._UserID = F_UserID;
		FM_EDIT._EditMode = MrsBaseEditFormType.Edit;
		FM_EDIT._PubCode = ChildPubCode;
		FM_EDIT._MainCost = F_AnaCst.ToString();
		FM_EDIT._ActionName = F_ActionName;
		FM_EDIT._ProjectCode = F_ProjectCode;
		FM_EDIT._ParentCode = parentPubCode.ToString();
		FM_EDIT._chgCount = F_chgCount;
		FM_EDIT._IsLockAn = F_IsLockAn;
		FM_EDIT._Istemplate = F_Istemplate;
		if (gridMrsBase1[gridMrsBase1.Row, "sNo"] != null)
		{
			FM_EDIT._sNO = (int)gridMrsBase1[RowIndex, "sNo"];
		}
		FM_EDIT._CallerFormName = "FormBreakDown";
		FM_EDIT._ExternalCost = Convert.ToDouble(gridMrsBase1[RowIndex, "Cost"]);
		FM_EDIT._IsLocked = F_IsLocked;
		if (FM_EDIT.ShowDialog(this) == DialogResult.OK)
		{
			ArrayList aArr = new ArrayList();
			aArr.Add(F_UserID);
			aArr.Add("WinFORM 單價分析線上編輯更新");
			Archnowledge.Pcces.BUDClass.MrsBaseA MrsBaseA1 = new Archnowledge.Pcces.BUDClass.MrsBaseA(F_UserID, aArr);
			MrsBaseA1.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
			MrsBaseA1.ps_projectcode = F_ProjectCode;
			MrsBaseA1.ps_Issue = F_chgCount;
			DataTable DT_OneItem = MrsBaseA1.ListItem("PubCode=" + gridMrsBase1[gridMrsBase1.Row, "PubCode"].ToString());
			Archnowledge.Pcces.BUDClass.MrsBaseB MrsBaseB1 = new Archnowledge.Pcces.BUDClass.MrsBaseB(aArr);
			MrsBaseB1.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
			MrsBaseB1.ps_projectcode = F_ProjectCode;
			MrsBaseB1.ps_parentCode = parentPubCode.ToString();
			MrsBaseB1.ps_pubCode = gridMrsBase1[gridMrsBase1.Row, "PubCode"].ToString();
			MrsBaseB1.ps_cost = DT_OneItem.Rows[0]["Cost"].ToString();
			MrsBaseB1.ps_listNo = gridMrsBase1[gridMrsBase1.Row, "ListNo"].ToString();
			MrsBaseB1.ps_Issue = F_chgCount;
			MrsBaseB1.UpdItem();
			try
			{
				F_DoubleETCost = Convert.ToDouble(DT_OneItem.Rows[0]["Cost"]);
			}
			catch
			{
			}
			DT_OneItem = null;
			if (chkReCalcu.Checked)
			{
				DoMrsCalculate(ChildPubCode, ListNo, CalculateChangeType.ChangeCost);
			}
			else
			{
				GetLowerData();
			}
		}
		gridMrsBase1.Focus();
		FM_EDIT.Close();
		FM_EDIT.Dispose();
		FM_EDIT = null;
	}

	private void GoNextLevelAnalysis(int iRow, int iCol)
	{
		int CostDec = 0;
		if ((bool)gridMrsBase1[iRow, "Analysis"])
		{
			parentPubCode = (int)gridMrsBase1[gridMrsBase1.Selection.r1, "PubCode"];
			if (gridMrsBase1[gridMrsBase1.Selection.r1, "CostDec"].ToString().Trim() != "")
			{
				CostDec = PubTools.Str2Int(gridMrsBase1[gridMrsBase1.Selection.r1, "CostDec"].ToString());
			}
			iCostDigital = CostDec;
			if (saLayer.Count == iLayer)
			{
				iLayer++;
				saLayer.Add(iLayer, parentPubCode);
				saLayerCostDec.Add(iLayer, CostDec);
			}
			else
			{
				iLayer++;
				saLayer[iLayer] = parentPubCode;
				saLayerCostDec[iLayer] = CostDec;
			}
			GetUpperData();
			GetLowerData();
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
		dbDecimal = null;
		DTDecimal = null;
		aArr = null;
	}

	private void RememberColsProps()
	{
		for (int i = 0; i < GridCols; i++)
		{
			GridColsSquence[i, 0] = gridMrsBase1.Cols[i].Name;
			GridColsSquence[i, 1] = gridMrsBase1.Cols[i].Caption;
			GridColsSquence[i, 2] = gridMrsBase1.Cols[i].Width;
			GridColsSquence[i, 3] = gridMrsBase1.Cols[i].DataType;
			if (gridMrsBase1.Cols[i].Name == "AnaImg")
			{
				GridColsSquence[i, 3] = typeof(Image);
			}
			else
			{
				GridColsSquence[i, 3] = gridMrsBase1.Cols[i].DataType;
			}
			GridColsSquence[i, 4] = gridMrsBase1.Cols[i].Visible;
			GridColsSquence[i, 5] = gridMrsBase1.Cols[i].Format;
			GridColsSquence[i, 6] = gridMrsBase1.Cols[i].AllowEditing;
			switch (gridMrsBase1.Cols[i].Name.ToUpper())
			{
			case "QTY":
				if (F_AnaQty > 0)
				{
					GridColsSquence[i, 5] = "###,###,###,##0." + "0".PadLeft(F_AnaQty, '0');
				}
				else
				{
					GridColsSquence[i, 5] = "###,###,###,##0";
				}
				break;
			case "COST":
				if (F_AnaCst > 0)
				{
					GridColsSquence[i, 5] = "###,###,###,##0." + "0".PadLeft(F_AnaCst, '0');
				}
				else
				{
					GridColsSquence[i, 5] = "###,###,###,##0";
				}
				break;
			case "AMOUNT":
				if (F_AnaAmt > 0)
				{
					GridColsSquence[i, 5] = "###,###,###,##0." + "0".PadLeft(F_AnaAmt, '0');
				}
				else
				{
					GridColsSquence[i, 5] = "###,###,###,##0";
				}
				break;
			}
			GridColsSquence[i, 7] = gridMrsBase1.Cols[i].TextAlign;
		}
	}

	private void FormMrsBaseBreakdown_Activated(object sender, EventArgs e)
	{
		if (FORM_STATUS == FormStatus.Iinitial)
		{
			FORM_STATUS = FormStatus.Normal;
		}
	}

	private void ResetToolbar()
	{
		ultraToolbarsManager1.Tools["mnuCopyToNew"].SharedProps.Visible = F_ActionName == PccesFormAction.BUD || F_ActionName == PccesFormAction.MrsBase;
		if (F_ActionName == PccesFormAction.BUD)
		{
			ultraButton2.Enabled = !F_Istemplate;
			ultraButton4.Enabled = !F_Istemplate;
			ultraButton5.Enabled = !F_Istemplate;
			BtnAdjust.Enabled = !F_Istemplate;
			ultraToolbarsManager1.Enabled = true;
			ultraToolbarsManager1.Tools["mnuCut"].SharedProps.Enabled = !F_Istemplate;
			ultraToolbarsManager1.Tools["mnuCopyToNew"].SharedProps.Enabled = !F_Istemplate;
			ultraToolbarsManager1.Tools["mnuDelete"].SharedProps.Enabled = !F_Istemplate;
			ultraToolbarsManager1.Tools["mnuEdit"].SharedProps.Enabled = !F_Istemplate;
			ultraToolbarsManager1.Tools["mnuPaste"].SharedProps.Enabled = !F_Istemplate;
			ultraToolbarsManager1.Tools["mnuPop_Use"].SharedProps.Enabled = !F_Istemplate;
			ultraToolbarsManager1.Tools["mnuQTS_Caller"].SharedProps.Enabled = !F_Istemplate;
			ultraToolbarsManager1.Tools["mnuTool_Up"].SharedProps.Enabled = !F_Istemplate;
			ultraToolbarsManager1.Tools["mnuTool_Down"].SharedProps.Enabled = !F_Istemplate;
			ultraToolbarsManager1.Tools["mnuTool_ReCal_Small"].SharedProps.Enabled = !F_Istemplate;
			ultraToolbarsManager1.Tools["mnuUseAdjCost"].SharedProps.Enabled = !F_Istemplate;
			ultraToolbarsManager1.Tools["PopupMenuNew"].SharedProps.Enabled = !F_Istemplate;
			ultraToolbarsManager1.Tools["mnuIRSet"].SharedProps.Enabled = F_IsUseIR;
			BtnSaveIR.Enabled = !F_Istemplate;
		}
		else if (F_ActionName == PccesFormAction.BUDEXE)
		{
			ultraToolbarsManager1.Tools["mnuCopy"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuCut"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuDelete"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuEdit"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuIRSet"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuIRCopy"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuPaste"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuPop_Use"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuQTS_Caller"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuTool_Up"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuTool_Down"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuTool_ReCal_Small"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["PopupMenuNew"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuUseAdjCost"].SharedProps.Enabled = false;
			gridMrsBase1.Cols["Qty"].AllowEditing = false;
			gridMrsBase1.Cols["LockCost"].AllowEditing = false;
			gridMrsBase1.Cols["Cost"].AllowEditing = false;
			gridMrsBase1.Cols["Memo"].AllowEditing = false;
			ultraButton2.Enabled = false;
			BtnAdjust.Enabled = false;
		}
		else
		{
			if (F_ActionName != PccesFormAction.BID && F_ActionName != PccesFormAction.SplitContract)
			{
				return;
			}
			if (F_IsSBID || (ContractApproved && F_ActionName == PccesFormAction.SplitContract))
			{
				if (F_IsSBID)
				{
					axSSPanel1.BackColor = Color.FromArgb(255, 128, 0);
					lblLevelNo.BackColor = Color.FromArgb(255, 128, 0);
					chkReCalcu.BackColor = Color.FromArgb(255, 128, 0);
				}
				ultraToolbarsManager1.Tools["mnuCopy"].SharedProps.Enabled = false;
				ultraToolbarsManager1.Tools["mnuCut"].SharedProps.Enabled = false;
				ultraToolbarsManager1.Tools["mnuDelete"].SharedProps.Enabled = false;
				ultraToolbarsManager1.Tools["mnuEdit"].SharedProps.Enabled = false;
				ultraToolbarsManager1.Tools["mnuIRSet"].SharedProps.Enabled = false;
				ultraToolbarsManager1.Tools["mnuIRCopy"].SharedProps.Enabled = false;
				ultraToolbarsManager1.Tools["mnuPaste"].SharedProps.Enabled = false;
				ultraToolbarsManager1.Tools["mnuPop_Use"].SharedProps.Enabled = false;
				ultraToolbarsManager1.Tools["mnuQTS_Caller"].SharedProps.Enabled = false;
				ultraToolbarsManager1.Tools["mnuTool_Up"].SharedProps.Enabled = false;
				ultraToolbarsManager1.Tools["mnuTool_Down"].SharedProps.Enabled = false;
				ultraToolbarsManager1.Tools["mnuTool_ReCal_Small"].SharedProps.Enabled = false;
				ultraToolbarsManager1.Tools["PopupMenuNew"].SharedProps.Enabled = false;
				ultraToolbarsManager1.Tools["mnuUseAdjCost"].SharedProps.Enabled = false;
				gridMrsBase1.Cols["Qty"].AllowEditing = false;
				gridMrsBase1.Cols["LockCost"].AllowEditing = false;
				gridMrsBase1.Cols["Cost"].AllowEditing = false;
				gridMrsBase1.Cols["Memo"].AllowEditing = false;
				ultraButton2.Enabled = false;
				BtnAdjust.Enabled = false;
			}
			ultraToolbarsManager1.Tools["mnuCopyToNew"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuIRSet"].SharedProps.Enabled = F_IsUseIR;
			BtnSaveIR.Enabled = false;
			if (F_IsLockAn)
			{
				ultraToolbarsManager1.Tools["mnuCut"].SharedProps.Enabled = false;
				ultraToolbarsManager1.Tools["mnuCopyToNew"].SharedProps.Enabled = false;
				ultraToolbarsManager1.Tools["mnuDelete"].SharedProps.Enabled = false;
				ultraToolbarsManager1.Tools["mnuPaste"].SharedProps.Enabled = false;
				ultraToolbarsManager1.Tools["mnuQTS_Caller"].SharedProps.Enabled = false;
				ultraToolbarsManager1.Tools["mnuTool_Up"].SharedProps.Enabled = false;
				ultraToolbarsManager1.Tools["mnuTool_Down"].SharedProps.Enabled = false;
				ultraToolbarsManager1.Tools["mnuTool_ReCal_Small"].SharedProps.Enabled = true;
				ultraToolbarsManager1.Tools["PopupMenuNew"].SharedProps.Enabled = false;
				gridMrsBase1.Cols["Memo"].AllowEditing = false;
			}
		}
	}

	private void GetUpperData()
	{
		string AppLocation = AppDomain.CurrentDomain.BaseDirectory;
		string IsOldReCal = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "BDGT", "IsOldReCal");
		SettingDecimal();
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add(CommonMethods.GetFormTypeTitle(FormType.MrsBase));
		PriceAnalysis PA1 = new PriceAnalysis(aArr);
		PA1.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		PA1.ps_prjcode = ProjectCode;
		PA1.ps_Issue = F_chgCount;
		if (IsOldReCal.ToUpper() == "THIRD")
		{
			PA1.ps_SmallCalcuMode = "THIRD";
		}
		DT_Upper = PA1.getAnaly(parentPubCode);
		txtAnalysisQty.Visible = false;
		txtAnalysisQty.Text = DT_Upper.Rows[0]["analysisQty"].ToString();
		lblPccesCode.Text = DT_Upper.Rows[0]["pccesCode"].ToString();
		lblCName.Text = DT_Upper.Rows[0]["cName"].ToString();
		lblAnalysisQty.Text = DT_Upper.Rows[0]["analysisQty"].ToString();
		lblUnit.Text = DT_Upper.Rows[0]["unitName"].ToString();
		if (F_ActionName == PccesFormAction.BUD && !F_Istemplate)
		{
			F_Istemplate = ArchConvert.Obj2Bool(DT_Upper.Rows[0]["Lock"]);
		}
		if (iCostDigital < 0)
		{
			iCostDigital = 0;
		}
		if (F_AnaAmt < 0)
		{
			F_AnaAmt = 0;
		}
		string sParCst = iCostDigital.ToString();
		string sParAmt = F_AnaAmt.ToString();
		lblPrice.Text = string.Format("{0:N" + sParCst + "}", DT_Upper.Rows[0]["cost"]);
		lblAmount.Text = string.Format("{0:N" + sParAmt + "}", DT_Upper.Rows[0]["amount"]);
		lblLRate.Text = DT_Upper.Rows[0]["lRate"].ToString() + "%";
		lblERate.Text = DT_Upper.Rows[0]["eRate"].ToString() + "%";
		lblMRate.Text = DT_Upper.Rows[0]["mRate"].ToString() + "%";
		lblWRate.Text = DT_Upper.Rows[0]["wRate"].ToString() + "%";
		DT_Upper = null;
		aArr = null;
		PA1 = null;
	}

	private void GetLowerData()
	{
		FORM_STATUS = FormStatus.Normal;
		Cursor = Cursors.WaitCursor;
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add(CommonMethods.GetFormTypeTitle(FormType.MrsBaseAnalysis));
		PriceAnalysis PA1 = new PriceAnalysis(aArr);
		PA1.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		PA1.ps_prjcode = ProjectCode;
		PA1.ps_Issue = F_chgCount;
		dtProjMrsB = PA1.listAnaly(parentPubCode);
		DT1 = dtProjMrsB.Copy();
		gridMrsBaseDataBind();
		lblLevelNo.Text = "【第 " + iLayer + " 層，共 " + dtProjMrsB.Rows.Count + " 筆】";
		PA1 = null;
		aArr = null;
		dtProjMrsB = null;
		Cursor = Cursors.Default;
		ResetToolbar();
	}

	private void SetGridColumn()
	{
		for (int i = 0; i < GridCols; i++)
		{
			gridMrsBase1.Cols[i].Name = (string)GridColsSquence[i, 0];
			gridMrsBase1.Cols[i].Caption = (string)GridColsSquence[i, 1];
			gridMrsBase1.Cols[i].Width = (int)GridColsSquence[i, 2];
			gridMrsBase1.Cols[i].DataType = (Type)GridColsSquence[i, 3];
			gridMrsBase1.Cols[i].Visible = (bool)GridColsSquence[i, 4];
			gridMrsBase1.Cols[i].Format = (string)GridColsSquence[i, 5];
			gridMrsBase1.Cols[i].AllowEditing = (bool)GridColsSquence[i, 6];
			gridMrsBase1.Cols[i].TextAlign = (TextAlignEnum)GridColsSquence[i, 7];
			if (F_Istemplate && (gridMrsBase1.Cols[i].Name == "Qty" || gridMrsBase1.Cols[i].Name == "Cost" || gridMrsBase1.Cols[i].Name == "LockCost"))
			{
				gridMrsBase1.Cols[i].AllowEditing = false;
			}
		}
	}

	private void gridMrsBaseDataBind()
	{
		sBindFlag = "BINDING";
		Archnowledge.Pcces.CommonClass.DebugUtil.OutputDebugString("gridMrsBaseDataBind  sBindFlag=" + sBindFlag);
		lock (this)
		{
			Cursor = Cursors.WaitCursor;
			FORM_STATUS = FormStatus.Edit;
			gridMrsBase1.Redraw = false;
			gridMrsBase1.Visible = false;
			DataView DV1 = DT1.DefaultView;
			gridMrsBase1.Clear(ClearFlags.All);
			gridMrsBase1.Select();
			gridMrsBase1.Rows.Count = DV1.Count + 1;
			SetGridColumn();
			CellStyle CS1 = gridMrsBase1.Styles.Add("AnalysisColor");
			CS1.ForeColor = Color.Red;
			CellStyle CS2 = gridMrsBase1.Styles.Add("LEMColor");
			CS2.ForeColor = Color.Teal;
			CellStyle CS3 = gridMrsBase1.Styles.Add("WColor");
			CS3.ForeColor = Color.Purple;
			CellStyle CS4 = gridMrsBase1.Styles.Add("ZColor");
			CS4.ForeColor = Color.Teal;
			CS4.BackColor = Color.LemonChiffon;
			CellStyle CSC = gridMrsBase1.Styles.Add("CColor");
			CSC.ForeColor = Color.Black;
			CSC.Font = new System.Drawing.Font("細明體", 11f, FontStyle.Bold);
			CellStyle CS5 = gridMrsBase1.Styles.Add("DollarColor");
			CS5.ForeColor = Color.Green;
			CellStyle CS6 = gridMrsBase1.Styles.Add("PercentColor");
			CS6.ForeColor = Color.Blue;
			CellStyle CS7 = gridMrsBase1.Styles.Add("NoEditColor");
			CS7.BackColor = Color.Pink;
			CellStyle CSM = gridMrsBase1.Styles.Add("Minus");
			CSM.BackColor = Color.FromArgb(255, 80, 80);
			CellStyle CSBackGround = gridMrsBase1.Styles.Add("AnalysisChild");
			CSBackGround.BackColor = Color.LightGoldenrodYellow;
			CellStyle CSBackGroundfixPrice = gridMrsBase1.Styles.Add("AnalysisChildfixPrice");
			CSBackGroundfixPrice.BackColor = Color.LemonChiffon;
			CellStyle CSD = gridMrsBase1.Styles.Add("DocDownloaded");
			CSD.BackColor = Color.PaleGoldenrod;
			CellStyle csSource = gridMrsBase1.Styles.Add("Source");
			csSource.DataType = typeof(string);
			csSource.ForeColor = Color.Navy;
			csSource.TextAlign = TextAlignEnum.LeftCenter;
			csSource.Font = new System.Drawing.Font(Font, FontStyle.Bold);
			gridMrsBase1.Cols["Source"].Style = csSource;
			string sItemClass = "";
			string sItemKind = "";
			for (int i = 0; i < DV1.Count; i++)
			{
				sItemClass = ((DV1[i]["pccesCode"].ToString().Length > 0) ? DV1[i]["pccesCode"].ToString().Substring(0, 1) : "");
				sItemKind = ((DV1[i]["costKind"] == null) ? "" : ((DV1[i]["costKind"].ToString().Length > 0) ? DV1[i]["costKind"].ToString().Substring(0, 1) : ""));
				gridMrsBase1[i + 1, "PccesCode"] = DV1[i]["pccesCode"].ToString();
				if (sItemClass == "L" || sItemClass == "E" || sItemClass == "M")
				{
					gridMrsBase1.Rows[i + 1].Style = gridMrsBase1.Styles["LEMColor"];
				}
				else if (sItemClass == "W")
				{
					gridMrsBase1.Rows[i + 1].Style = gridMrsBase1.Styles["WColor"];
				}
				switch (sItemKind)
				{
				case "$":
					gridMrsBase1.Rows[i + 1].Style = gridMrsBase1.Styles["DollarColor"];
					break;
				case "%":
					gridMrsBase1.Rows[i + 1].Style = gridMrsBase1.Styles["PercentColor"];
					break;
				case "Z":
					gridMrsBase1.Rows[i + 1].Style = gridMrsBase1.Styles["ZColor"];
					break;
				case "#":
					gridMrsBase1.Rows[i + 1].Style = gridMrsBase1.Styles["CColor"];
					break;
				}
				switch (sItemKind)
				{
				default:
					if (!(sItemKind == "M"))
					{
						break;
					}
					goto case "%";
				case "%":
				case "L":
				case "E":
					gridMrsBase1.Rows[i + 1].Style = gridMrsBase1.Styles["NoEditColor"];
					break;
				}
				gridMrsBase1[i + 1, "CName"] = DV1[i]["cName"].ToString();
				if (DV1[i]["analysis"].ToString().Trim() == "1")
				{
					gridMrsBase1[i + 1, "Analysis"] = true;
					gridMrsBase1.Rows[i + 1].Style = gridMrsBase1.Styles["AnalysisColor"];
					CellRange rg = gridMrsBase1.GetCellRange(i + 1, gridMrsBase1.Cols["AnaImg"].SafeIndex);
					rg.Style = gridMrsBase1.Styles["img"];
					rg.Image = imageList2.Images[0];
				}
				else
				{
					gridMrsBase1[i + 1, "Analysis"] = false;
				}
				gridMrsBase1[i + 1, "ListNo"] = DV1[i]["listNo"];
				gridMrsBase1[i + 1, "UnitName"] = DV1[i]["unitName"];
				gridMrsBase1[i + 1, "CostKind"] = DV1[i]["costKind"];
				gridMrsBase1[i + 1, "PubCode"] = DV1[i]["pubCode"];
				gridMrsBase1[i + 1, "Memo"] = DV1[i]["memo"];
				gridMrsBase1[i + 1, "sNo"] = DV1[i]["sNo"];
				gridMrsBase1[i + 1, "surName"] = DV1[i]["surName"].ToString();
				gridMrsBase1[i + 1, "fixPrice"] = DV1[i]["fixPrice"].ToString().Trim() == "1";
				gridMrsBase1[i + 1, "Account"] = DV1[i]["Account"];
				if (Is75094900())
				{
					gridMrsBase1[i + 1, "ExtendCode"] = DV1[i]["ExtendCode"];
				}
				if (F_ActionName == PccesFormAction.BUD)
				{
					gridMrsBase1[i + 1, "Lock"] = ArchConvert.Obj2Bool(DV1[i]["Lock"]);
					gridMrsBase1[i + 1, "ItemType"] = ItemType.GetItemType(DV1[i]["IsCommonItem"].ToString());
				}
				CellRange RAccMode = gridMrsBase1.GetCellRange(i + 1, gridMrsBase1.Cols["PwrSet"].SafeIndex, i + 1, gridMrsBase1.Cols["PwrSet"].SafeIndex);
				RAccMode.Style = gridMrsBase1.Styles["ComboListPS"];
				if (DV1[i]["PwrSet"] != DBNull.Value)
				{
					gridMrsBase1[i + 1, "PwrSet"] = PwrSet.GetName(dsPwrSet, PubTools.Str2Int(DV1[i]["PwrSet"]));
				}
				else
				{
					gridMrsBase1[i + 1, "PwrSet"] = PwrSet.GetDefaultName(dsPwrSet);
				}
				bool flag = false;
				gridMrsBase1[i + 1, "QtyDec"] = ((!(DV1[i]["CostKind"].ToString() == "")) ? ((DV1[i]["bQtyDec"] == DBNull.Value) ? ((object)F_AnaQty) : DV1[i]["bQtyDec"]) : ((DV1[i]["QtyDec"] == DBNull.Value) ? ((object)F_AnaQty) : DV1[i]["QtyDec"]));
				if ((bool)gridMrsBase1[i + 1, "Analysis"])
				{
					gridMrsBase1[i + 1, "CostDec"] = ((!(DV1[i]["CostKind"].ToString() == "")) ? ((DV1[i]["bCostDec"] == DBNull.Value) ? ((object)F_MainCst) : DV1[i]["bCostDec"]) : ((DV1[i]["CostDec"] == DBNull.Value) ? ((object)F_AnaCst) : DV1[i]["CostDec"]));
				}
				else
				{
					gridMrsBase1[i + 1, "CostDec"] = ((!(DV1[i]["CostKind"].ToString() == "")) ? ((DV1[i]["bCostDec"] == DBNull.Value) ? ((object)F_AnaCst) : DV1[i]["bCostDec"]) : ((DV1[i]["CostDec"] == DBNull.Value) ? ((object)F_AnaCst) : DV1[i]["CostDec"]));
				}
				gridMrsBase1[i + 1, "AmtDec"] = ((!(DV1[i]["CostKind"].ToString() == "")) ? ((DV1[i]["bAmtDec"] == DBNull.Value) ? ((object)F_AnaAmt) : DV1[i]["bAmtDec"]) : ((DV1[i]["AmtDec"] == DBNull.Value) ? ((object)F_AnaAmt) : DV1[i]["AmtDec"]));
				CellRange RgQtyDec = gridMrsBase1.GetCellRange(i + 1, gridMrsBase1.Cols["QtyDec"].SafeIndex, i + 1, gridMrsBase1.Cols["QtyDec"].SafeIndex);
				CellRange RgCstDec = gridMrsBase1.GetCellRange(i + 1, gridMrsBase1.Cols["CostDec"].SafeIndex, i + 1, gridMrsBase1.Cols["CostDec"].SafeIndex);
				CellRange RgAmtDec = gridMrsBase1.GetCellRange(i + 1, gridMrsBase1.Cols["AmtDec"].SafeIndex, i + 1, gridMrsBase1.Cols["AmtDec"].SafeIndex);
				RgQtyDec.Style = gridMrsBase1.Styles["ComboList"];
				RgCstDec.Style = gridMrsBase1.Styles["ComboList"];
				RgAmtDec.Style = gridMrsBase1.Styles["ComboList"];
				if (F_AnaQty != PubTools.Str2Int(gridMrsBase1[i + 1, "QtyDec"]))
				{
					iQty++;
					int iiQty = PubTools.Str2Int(gridMrsBase1[i + 1, "QtyDec"]);
					CellStyle styQtyDec = gridMrsBase1.Styles.Add("QtyDec" + iQty);
					if (iiQty > 0)
					{
						styQtyDec.Format = "###,###,###,##0." + "0".PadLeft(iiQty, '0');
					}
					else
					{
						styQtyDec.Format = "###,###,###,##0";
					}
					gridMrsBase1.SetCellStyle(i + 1, gridMrsBase1.Cols["Qty"].SafeIndex, styQtyDec);
				}
				if (F_AnaCst != PubTools.Str2Int(gridMrsBase1[i + 1, "CostDec"]))
				{
					iCst++;
					int iiCst = PubTools.Str2Int(gridMrsBase1[i + 1, "CostDec"]);
					CellStyle styCstDec = gridMrsBase1.Styles.Add("CstDec" + iCst);
					if (iiCst > 0)
					{
						styCstDec.Format = "###,###,###,##0." + "0".PadLeft(iiCst, '0');
					}
					else
					{
						styCstDec.Format = "###,###,###,##0";
					}
					gridMrsBase1.SetCellStyle(i + 1, gridMrsBase1.Cols["Cost"].SafeIndex, styCstDec);
				}
				if (F_AnaAmt != PubTools.Str2Int(gridMrsBase1[i + 1, "AmtDec"]))
				{
					iAmt++;
					int iiAmt = PubTools.Str2Int(gridMrsBase1[i + 1, "AmtDec"]);
					CellStyle styAmtDec = gridMrsBase1.Styles.Add("AmtDec" + iAmt);
					if (iiAmt > 0)
					{
						styAmtDec.Format = "###,###,###,##0." + "0".PadLeft(iiAmt, '0');
					}
					else
					{
						styAmtDec.Format = "###,###,###,##0";
					}
					gridMrsBase1.SetCellStyle(i + 1, gridMrsBase1.Cols["Amount"].SafeIndex, styAmtDec);
				}
				if (F_ActionName == PccesFormAction.BUD || F_ActionName == PccesFormAction.MrsBase)
				{
					if (DV1[i]["Source"] != DBNull.Value)
					{
						if (ArchConvert.Obj2Int(DV1[i]["Source"]) == 0)
						{
							gridMrsBase1[i + 1, "Source"] = "日報統計";
						}
						else if (ArchConvert.Obj2Int(DV1[i]["Source"]) == -1)
						{
							gridMrsBase1[i + 1, "Source"] = "固定成本";
						}
						else
						{
							gridMrsBase1[i + 1, "Source"] = DV1[i]["Source"];
						}
					}
					gridMrsBase1[i + 1, "Ratio"] = DV1[i]["Ratio"];
					gridMrsBase1[i + 1, "GroupName"] = DV1[i]["GroupName"];
				}
				if (MoveUpDownFlag == "MOVED")
				{
					gridMrsBase1[i + 1, "PSNo"] = GlobalSelItems[i];
				}
				else
				{
					gridMrsBase1[i + 1, "PSNo"] = i + 1;
				}
				gridMrsBase1[i + 1, "LockCost"] = DV1[i]["LockCost"].ToString().Trim() == "1";
				if (F_ActionName == PccesFormAction.BUD || F_ActionName == PccesFormAction.BID || F_ActionName == PccesFormAction.SplitContract || F_ActionName == PccesFormAction.SubChange)
				{
					gridMrsBase1[i + 1, "usrQty"] = DV1[i]["usrQty"];
				}
				string ssCount = "";
				ArrayList cArr = new ArrayList();
				cArr.Clear();
				cArr.Add(F_UserID);
				cArr.Add("取得單價分析子是否有鎖定單價" + F_ProjectCode);
				ModifyDB stdCom = new ModifyDB(F_ProjectCode, cArr);
				if (!(bool)gridMrsBase1[i + 1, "LockCost"] && sItemClass == "W")
				{
					ssCount = ((F_ActionName != PccesFormAction.BUD) ? stdCom.DBGetValue("select count(*) from bidProjMrsB Where ProjectCode='" + F_ProjectCode + "' and parentCode=" + gridMrsBase1[i + 1, "PubCode"].ToString() + " and LockCost='1'") : stdCom.DBGetValue("select count(*) from budProjMrsB Where ProjectCode='" + F_ProjectCode + "' and parentCode=" + gridMrsBase1[i + 1, "PubCode"].ToString() + " and LockCost='1'"));
					if (PubTools.Str2Double(ssCount) > 0.0)
					{
						CellRange RgRowAnaLockCost = gridMrsBase1.GetCellRange(i + 1, gridMrsBase1.Cols["LockCost"].SafeIndex);
						RgRowAnaLockCost.Style = CSBackGround;
					}
				}
				if (!(bool)gridMrsBase1[i + 1, "fixPrice"] && sItemClass == "W")
				{
					ssCount = ((F_ActionName != PccesFormAction.BUD) ? stdCom.DBGetValue("select count(*) from bidProjMrsB Where ProjectCode='" + F_ProjectCode + "' and parentCode=" + gridMrsBase1[i + 1, "PubCode"].ToString() + " and fixPrice='1'") : stdCom.DBGetValue("select count(*) from budProjMrsB Where ProjectCode='" + F_ProjectCode + "' and parentCode=" + gridMrsBase1[i + 1, "PubCode"].ToString() + " and fixPrice='1'"));
					if (PubTools.Str2Double(ssCount) > 0.0)
					{
						CellRange RgRowAnaLockCost = gridMrsBase1.GetCellRange(i + 1, gridMrsBase1.Cols["fixPrice"].SafeIndex);
						RgRowAnaLockCost.Style = CSBackGroundfixPrice;
					}
				}
				if (sItemKind != "#")
				{
					if (PubTools.Str2Double(DV1[i]["bamount"]) < 0.0)
					{
						gridMrsBase1.Rows[i + 1].Style = gridMrsBase1.Styles["Minus"];
					}
					gridMrsBase1[i + 1, "Rate"] = DV1[i]["rate"];
					gridMrsBase1[i + 1, "LRate"] = DV1[i]["lRate"];
					gridMrsBase1[i + 1, "ERate"] = DV1[i]["eRate"];
					gridMrsBase1[i + 1, "MRate"] = DV1[i]["mRate"];
					gridMrsBase1[i + 1, "WRate"] = DV1[i]["wRate"];
					gridMrsBase1[i + 1, "Qty"] = DV1[i]["bqty"];
					gridMrsBase1[i + 1, "Cost"] = DV1[i]["bcost"];
					gridMrsBase1[i + 1, "Amount"] = DV1[i]["bamount"];
				}
				cArr = null;
				stdCom = null;
				try
				{
					ArrayList Arr = new ArrayList();
					Arr.Add(F_UserID);
					Arr.Add("判別是否已經下載過綱要規範" + F_ProjectCode);
					ModifyDB MDB = new ModifyDB(F_ProjectCode, Arr);
					string PccesCode = DT1.Rows[i]["pccesCode"].ToString();
					if (PccesCode.Trim() != "")
					{
						if (char.IsLetter(PccesCode, 0))
						{
							PccesCode = PccesCode.Substring(1, PccesCode.Length - 1);
						}
						string ChapterNo = PccesCode;
						if (PccesCode.Length >= 5)
						{
							ChapterNo = PccesCode.Substring(0, 5);
						}
						string sSQL = "SELECT COUNT(*) FROM AddOnDownLoad WHERE projectCode = '" + F_ProjectCode + "' AND ChapterNo LIKE '" + ChapterNo + "%'";
						if (MDB.DBCount(sSQL) > 0)
						{
							gridMrsBase1.SetCellStyle(i + 1, gridMrsBase1.Cols["pccesCode"].SafeIndex, CSD);
						}
					}
				}
				catch
				{
				}
			}
			gridMrsBase1.Redraw = true;
			gridMrsBase1.Visible = true;
			if (CurrentRow < gridMrsBase1.Rows.Count && gridMrsBase1.Rows.Count > 1)
			{
				if (CurrentRow > 0 && CurrentRow <= gridMrsBase1.Rows.Count)
				{
					gridMrsBase1.Row = CurrentRow;
				}
				else
				{
					gridMrsBase1.Row = 0;
				}
			}
			SetColsEditSymbol();
			if (F_IsSurName)
			{
				gridMrsBase1.Cols["surName"].Visible = true;
			}
			else
			{
				gridMrsBase1.Cols["surName"].Visible = false;
			}
			Cursor = Cursors.Default;
			MoveUpDownFlag = "";
			sBindFlag = "NORMAL";
			FORM_STATUS = FormStatus.Normal;
			Refresh();
		}
		if (Is22132814())
		{
			gridMrsBase1.Cols["CostDec"].AllowEditing = false;
			gridMrsBase1.Cols["AmtDec"].AllowEditing = false;
		}
		if (F_IsLockAn)
		{
			ultraToolbarsManager1.Tools["mnuCut"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuCopy"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuCopyToNew"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuPaste"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuDelete"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuIRSet"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuIRCopy"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["PopupMenuNew"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuTool_Up"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuTool_Down"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuTool_ReCal_Small"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuQTS_Caller"].SharedProps.Enabled = false;
			gridMrsBase1.Cols["Memo"].AllowEditing = false;
		}
	}

	private bool Is22132814()
	{
		string sPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "22132814.dat");
		if (File.Exists(sPath))
		{
			return true;
		}
		return false;
	}

	private void SaveBreakdown(string sMode)
	{
		Cursor = Cursors.WaitCursor;
		switch (sMode)
		{
		case "1":
		{
			ArrayList aArr = new ArrayList();
			aArr.Add(F_UserID);
			aArr.Add(CommonMethods.GetFormTypeTitle(FormType.MrsBase));
			Archnowledge.Pcces.BUDClass.MrsBaseA PB1 = new Archnowledge.Pcces.BUDClass.MrsBaseA(F_UserID, aArr);
			PB1.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
			PB1.ps_projectcode = ProjectCode;
			PB1.ps_pccesCode = lblPccesCode.Text.Trim();
			PB1.ps_Issue = F_chgCount;
			PB1.ps_analysisQty = lblAnalysisQty.Text;
			PB1.UpdItem();
			DoMrsCalculate();
			break;
		}
		case "2":
			MessageBox.Show("發現此訊息出現,請趕快叫程式設計師,不然電腦會爆炸!!");
			break;
		}
		Cursor = Cursors.Default;
	}

	private void AdjustPrice()
	{
		int tmp = PubTools.Str2Int(PubCode.ToString());
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("單價分析--給定複價自動回算");
		Recost ReCostCom = new Recost(tmp_AL1);
		ReCostCom.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		ReCostCom.ps_prjcode = F_ProjectCode;
		ReCostCom.ps_pubcode = tmp.ToString();
		ReCostCom.ps_Issue = F_chgCount;
		ReCostCom.SetCost(tmp, PubTools.Str2Double(lblPrice.Text));
		GetUpperData();
		GetLowerData();
	}

	private void DoMrsCalculate()
	{
		DoMrsCalculate(0, 0, CalculateChangeType.ChangeCost);
	}

	private void DoMrsCalculate(int ChildPubCode, int ListNo, CalculateChangeType ChangeType)
	{
		if ((F_ActionName == PccesFormAction.BID || F_ActionName == PccesFormAction.BUD || F_ActionName == PccesFormAction.MrsBase) && EnableNewCalculateCost)
		{
			FM_INFO = new FormSys_G_Info1();
			FM_INFO._InfoString = "重新小計中，請稍候! ";
			FM_INFO.Owner = this;
			FM_INFO.Show();
			FM_INFO.BringToFront();
			Application.DoEvents();
			Cursor = Cursors.WaitCursor;
			ExecResult ER = new ExecResult();
			try
			{
				if (theMrsCalculate == null)
				{
					int ChgCount = 0;
					int.TryParse(F_chgCount, out ChgCount);
					theMrsCalculate = new MrsCalculate(F_ActionName, F_ProjectCode, ChgCount);
				}
				if (ChildPubCode != 0)
				{
					if (ArchConvert.Obj2Bool(ConfigurationManager.AppSettings["EnableFullMrsCalculate"]))
					{
						ER = theMrsCalculate.Calculate(PubCode, ChildPubCode, ListNo, ChangeType, ProgressEventHandler);
					}
					else
					{
						List<int> ParentCodes = new List<int>();
						parentPubCode = Convert.ToInt32(saLayer[iLayer]);
						int i = iLayer;
						while (i <= saLayer.Count && i > 0)
						{
							ParentCodes.Add(Convert.ToInt32(saLayer[i]));
							i--;
						}
						ER = theMrsCalculate.Calculate(ParentCodes, ChildPubCode, ListNo, ChangeType, ProgressEventHandler);
					}
				}
				else
				{
					ER = theMrsCalculate.Calculate(PubCode, ProgressEventHandler);
				}
			}
			catch (Exception ex)
			{
				ER.ReturnCode = 1;
				ER.Message = ex.Message;
			}
			if (EnableCOMS)
			{
				BudProjMrsB theBudProjMrsB = new BudProjMrsB();
				theBudProjMrsB.UpdateBudProjMrsBGroupNameRatio(ProjectCode, PubCode);
			}
			Cursor = Cursors.Default;
			FM_INFO.Close();
			FM_INFO.Dispose();
			Application.DoEvents();
			if (ER.ReturnCode != 0)
			{
				MessageBox.Show("小計失敗 :" + ER.Message, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			GetUpperData();
			GetLowerData();
			return;
		}
		string srckind = "";
		FM_INFO = new FormSys_G_Info1();
		FM_INFO._InfoString = "重新小計中，請稍候! ";
		FM_INFO.Owner = this;
		FM_INFO.Show();
		FM_INFO.BringToFront();
		Application.DoEvents();
		Cursor = Cursors.WaitCursor;
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add(CommonMethods.GetFormTypeTitle(FormType.MrsBaseAnalysis));
		string AppLocation = AppDomain.CurrentDomain.BaseDirectory;
		if (RC1 != null)
		{
			RC1 = null;
		}
		if (RC1 == null)
		{
			RC1 = new Recost(aArr);
		}
		RC1.ps_IsProcessEvent = true;
		RC1.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		RC1.ps_prjcode = ProjectCode;
		RC1.ps_pubcode = parentPubCode.ToString();
		RC1.ps_Issue = F_chgCount;
		RC1.ps_ParentCostDec = iCostDigital.ToString();
		srckind = CommonMethods.GetActionNameString(F_ActionName);
		string IsOldReCal = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "BDGT", "IsOldReCal");
		string sIsForceInteger = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "BDGT", "IsForceInteger");
		RC1.ps_IsForceInteger = sIsForceInteger;
		string sType = GetReCalType();
		if (sType != "")
		{
			IsOldReCal = sType;
		}
		if (srckind == "BID")
		{
			Archnowledge.Pcces.BUDClass.Project projcom = new Archnowledge.Pcces.BUDClass.Project(aArr);
			projcom.ps_srckind = srckind;
			DataTable dt = projcom.ListItem_eight("", F_ProjectCode);
			if (dt.Rows.Count > 0 && dt.Rows[0]["ReCalType"].ToString().Trim() == "" && dt.Rows[0]["printMode"].ToString() != "")
			{
				string readPrintMode = dt.Rows[0]["printMode"].ToString().Trim();
				string tmpPrintMode = readPrintMode.Substring(37, 1);
				IsOldReCal = ((tmpPrintMode == "0") ? "FALSE" : ((!(tmpPrintMode == "1")) ? "THIRD" : "TRUE"));
			}
			projcom = null;
			dt = null;
		}
		decimal[] dTmp;
		if (IsOldReCal.ToUpper() == "FALSE")
		{
			dTmp = RC1.ReCalc2(1, 0m);
		}
		else if (IsOldReCal.ToUpper() == "TRUE")
		{
			dTmp = RC1.ReCalc2(1, 0m);
		}
		else if (IsOldReCal.ToUpper() == "THIRD")
		{
			RC1.ps_SmallCalcuMode = "THIRD";
			dTmp = RC1.ReCalc2(1, 0m);
		}
		else
		{
			dTmp = RC1.ReCalc2(1, 0m);
		}
		Cursor = Cursors.Default;
		FM_INFO.Close();
		FM_INFO.Dispose();
		Application.DoEvents();
		if (dTmp[0] == -1m)
		{
			MessageBox.Show(this, "重新小計失敗，\n單價分析結構錯誤。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		if (IsOldReCal.ToUpper() != "TRUE" && dTmp[0] == -3m)
		{
			MessageBox.Show(this, "重新小計失敗，\n差額產生，但未設定【雜項】無法計算。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		GetUpperData();
		GetLowerData();
		RC1 = null;
		aArr = null;
	}

	private string GetReCalType()
	{
		if (F_ProjectCode == "")
		{
			return "";
		}
		string Scrkind = CommonMethods.GetActionNameString(F_ActionName);
		if (Scrkind != "BUD" && Scrkind != "BID")
		{
			Scrkind = "BUD";
		}
		string iNum = "1";
		string rtnStr = "";
		string sSQL = "Select ReCalType from " + Scrkind + "Project where projectCode = '" + F_ProjectCode + "'";
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add("取pccescode的值");
		ModifyDB ModDB = new ModifyDB(F_ProjectCode, aArr);
		DataTable DT = new DataTable();
		DT = ModDB.DBList(sSQL);
		if (DT.Rows.Count > 0)
		{
			iNum = DT.Rows[0]["ReCalType"].ToString().Trim();
		}
		switch (iNum)
		{
		case "1":
			rtnStr = "FALSE";
			break;
		case "2":
			rtnStr = "TRUE";
			break;
		case "3":
			rtnStr = "THIRD";
			break;
		}
		ModDB = null;
		aArr = null;
		if (iNum == "")
		{
			CommonMethods.IniWriteValue(AppLocation + "OptionSet.ini", "BDGT", "IsOldReCal", "FALSE");
		}
		return rtnStr;
	}

	private void ChangeAnalysisState(string sState)
	{
		if (sState.ToUpper() == "ENTER")
		{
			ultraButton2.Text = "...";
			lblAnalysisQty.Text = txtAnalysisQty.Text;
			lblAnalysisQty.Visible = true;
			txtAnalysisQty.Visible = false;
		}
		else
		{
			ultraButton2.Text = "...";
			lblAnalysisQty.Visible = true;
			txtAnalysisQty.Visible = false;
		}
	}

	private void ChangeAnalysisState2(string sState)
	{
		if (sState.ToUpper() == "ENTER")
		{
			BtnAdjust.Text = "調價";
			lblPrice.Text = txtPrice.Text;
			lblPrice.Visible = true;
			txtPrice.Visible = false;
		}
		else
		{
			BtnAdjust.Text = "調價";
			lblPrice.Visible = true;
			txtPrice.Visible = false;
		}
	}

	private void ultraToolbarsManager1_ToolClick(object sender, ToolClickEventArgs e)
	{
		DoMenuAction(e.Tool.Key);
	}

	private void gridMrsBase1_AfterEdit(object sender, RowColEventArgs e)
	{
		if (F_DoubleET != "")
		{
			gridMrsBase1[e.Row, "Cost"] = F_DoubleETCost;
			return;
		}
		if ((F_Istemplate && gridMrsBase1.Cols[e.Col].Name != "PwrSet") || sBindFlag == "BINDING" || e.Col < 0 || e.Row < 0)
		{
			return;
		}
		string EditedColumnName = gridMrsBase1.Cols[e.Col].Name;
		string CostKind = gridMrsBase1[e.Row, "CostKind"].ToString();
		int RowIndex = e.Row;
		if (FORM_STATUS == FormStatus.Edit && EditedColumnName == "GroupName")
		{
			gridMrsBase1[RowIndex, "Source"] = null;
		}
		if (FORM_STATUS == FormStatus.Edit && EditedColumnName == "LockCost")
		{
			FormSys_G_Info1 FM_INFO = new FormSys_G_Info1();
			FM_INFO._InfoString = "項目鎖定中，請稍候! ";
			FM_INFO.Show();
			Application.DoEvents();
			Cursor = Cursors.WaitCursor;
			gridMrsBase1.Enabled = false;
			Application.DoEvents();
			string IPStr = CommonMethods.GetIPAddress();
			ArrayList aArr = new ArrayList();
			aArr.Clear();
			aArr.Add(F_UserID);
			aArr.Add("單價分析編輯後存檔之鎖定異動--" + F_ProjectCode + "(" + IPStr + ")");
			string sLockCheck = (((bool)gridMrsBase1[RowIndex, "LockCost"]) ? "1" : "0");
			Archnowledge.Pcces.BUDClass.MrsBaseB MrsBaseB1 = new Archnowledge.Pcces.BUDClass.MrsBaseB(aArr);
			MrsBaseB1.ps_Issue = F_chgCount;
			MrsBaseB1.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
			MrsBaseB1.LockCost(F_ProjectCode, parentPubCode.ToString(), gridMrsBase1[RowIndex, "pubCode"].ToString().Trim(), sLockCheck, "LockCost");
			FM_INFO.Close();
			FM_INFO.Dispose();
			CurrentRow = gridMrsBase1.Row;
			GetLowerData();
			if (base.Owner is frmBudget)
			{
				(base.Owner as frmBudget)._IsNeedToReloadAllData = true;
			}
			else if (base.Owner is FormBudgetRes)
			{
				(base.Owner as FormBudgetRes)._IsNeedToReloadAllData = true;
			}
			gridMrsBase1.Enabled = true;
			SetPopupMenuEnable();
			return;
		}
		if (FORM_STATUS == FormStatus.Edit && EditedColumnName == "fixPrice")
		{
			FormSys_G_Info1 FM_INFO = new FormSys_G_Info1();
			FM_INFO._InfoString = "項目標單固定單價處理中，請稍候! ";
			FM_INFO.Show();
			Application.DoEvents();
			Cursor = Cursors.WaitCursor;
			gridMrsBase1.Enabled = false;
			Application.DoEvents();
			string IPStr = CommonMethods.GetIPAddress();
			ArrayList aArr = new ArrayList();
			aArr.Clear();
			aArr.Add(F_UserID);
			aArr.Add("單價分析編輯後存檔之鎖定異動--" + F_ProjectCode + "(" + IPStr + ")");
			string sLockCheck = (((bool)gridMrsBase1[RowIndex, "fixPrice"]) ? "1" : "0");
			Archnowledge.Pcces.BUDClass.MrsBaseB MrsBaseB1 = new Archnowledge.Pcces.BUDClass.MrsBaseB(aArr);
			MrsBaseB1.ps_Issue = F_chgCount;
			MrsBaseB1.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
			MrsBaseB1.LockCost(F_ProjectCode, parentPubCode.ToString(), gridMrsBase1[RowIndex, "pubCode"].ToString().Trim(), sLockCheck, "fixPrice");
			MrsBaseB1.LockCost(F_ProjectCode, parentPubCode.ToString(), gridMrsBase1[RowIndex, "pubCode"].ToString().Trim(), sLockCheck, "LockCost");
			MrsBaseB1.UpdateMemofixprice(F_ProjectCode, parentPubCode.ToString(), gridMrsBase1[RowIndex, "pubCode"].ToString().Trim(), sLockCheck);
			FM_INFO.Close();
			FM_INFO.Dispose();
			CurrentRow = gridMrsBase1.Row;
			GetLowerData();
			if (base.Owner is frmBudget)
			{
				(base.Owner as frmBudget)._IsNeedToReloadAllData = true;
			}
			else if (base.Owner is FormBudgetRes)
			{
				(base.Owner as FormBudgetRes)._IsNeedToReloadAllData = true;
			}
			gridMrsBase1.Enabled = true;
			SetPopupMenuEnable();
			return;
		}
		if (gridMrsBase1[RowIndex, "sNo"] != null)
		{
			ComsWebService theComsWebService = new ComsWebService(F_ProjectCode);
			int sNo = ArchConvert.Obj2Int(gridMrsBase1[RowIndex, "sNo"]);
			if (!theComsWebService.AllowChangeBysNo(-1, sNo, silentOnWarning: false, silentOnModify: true))
			{
				bool allowChange = true;
				if (EditedColumnName.ToUpper() == "QTY" && !ArchConvert.Obj2Bool(CommonMethods.GetIniValue("COMS", "IsEditSkipQtyCheck")))
				{
					ExecResult ER = new ExecResult();
					SubPlanServiceHelper subPlanServiceHelper = new SubPlanServiceHelper();
					decimal qty = subPlanServiceHelper.GetBudLemDoneQtyRate(F_ProjectCode, sNo, IsMrs: true, out ER);
					if (ER.ReturnCode != 0)
					{
						MessageBox.Show("呼叫服務發生錯誤，訊息如下：\n" + ER.Message, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
					else if (ArchConvert.Obj2Decimal(gridMrsBase1[RowIndex, "Qty"]) < qty)
					{
						MessageBox.Show($"數量不可低於已發包量({qty})", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						allowChange = false;
					}
				}
				if (!allowChange)
				{
					gridMrsBaseDataBind();
					return;
				}
			}
		}
		int ChildPubCode;
		int ListNo;
		if (FORM_STATUS == FormStatus.Edit)
		{
			switch (EditedColumnName)
			{
			default:
				if (!(EditedColumnName == "ExtendCode"))
				{
					break;
				}
				goto case "Qty";
			case "Qty":
			case "Cost":
			case "Memo":
			case "Rate":
			case "CostKind":
			case "QtyDec":
			case "CostDec":
			case "AmtDec":
			case "surName":
			case "fixPrice":
			case "PwrSet":
			case "Account":
			case "Source":
			case "GroupName":
				try
				{
					ArrayList aArr = new ArrayList();
					aArr.Add(F_UserID);
					aArr.Add("WinFORM 單價分析線上編輯更新");
					Archnowledge.Pcces.BUDClass.MrsBaseB MrsBaseB1 = new Archnowledge.Pcces.BUDClass.MrsBaseB(aArr);
					MrsBaseB1.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
					MrsBaseB1.ps_projectcode = F_ProjectCode;
					MrsBaseB1.ps_parentCode = parentPubCode.ToString();
					MrsBaseB1.ps_pubCode = gridMrsBase1[gridMrsBase1.Row, "PubCode"].ToString();
					switch (CostKind)
					{
					default:
						if (!(CostKind == "E"))
						{
							MrsBaseB1.ps_qty = gridMrsBase1[RowIndex, "Qty"].ToString();
							break;
						}
						goto case "Z";
					case "Z":
					case "%":
					case "L":
					case "M":
						MrsBaseB1.ps_qty = "1";
						break;
					}
					MrsBaseB1.ps_cost = gridMrsBase1[RowIndex, "Cost"].ToString();
					MrsBaseB1.ps_listNo = gridMrsBase1[RowIndex, "ListNo"].ToString();
					MrsBaseB1.ps_Issue = F_chgCount;
					MrsBaseB1.ps_QtyDec = ((PubTools.Str2Int(gridMrsBase1[RowIndex, "QtyDec"]) == F_AnaQty) ? null : gridMrsBase1[RowIndex, "QtyDec"].ToString());
					MrsBaseB1.ps_AmtDec = ((PubTools.Str2Int(gridMrsBase1[RowIndex, "AmtDec"]) == F_AnaAmt) ? null : gridMrsBase1[RowIndex, "AmtDec"].ToString());
					MrsBaseB1.ps_CstDec = ((PubTools.Str2Int(gridMrsBase1[RowIndex, "CostDec"]) == F_AnaCst) ? null : gridMrsBase1[RowIndex, "CostDec"].ToString());
					MrsBaseB1.ps_fixPrice = ((gridMrsBase1[RowIndex, "fixPrice"] == null) ? "0" : (((bool)gridMrsBase1[RowIndex, "fixPrice"]) ? "1" : "0"));
					if (gridMrsBase1[RowIndex, "Source"] != null)
					{
						if (gridMrsBase1[RowIndex, "Source"].ToString() == "日報統計")
						{
							MrsBaseB1.ps_Source = "0";
						}
						else if (gridMrsBase1[RowIndex, "Source"].ToString() == "固定成本")
						{
							MrsBaseB1.ps_Source = "-1";
						}
						else
						{
							MrsBaseB1.ps_Source = ArchConvert.Obj2String(gridMrsBase1[RowIndex, "Source"]);
						}
					}
					MrsBaseB1.ps_GroupName = ArchConvert.Obj2String(gridMrsBase1[RowIndex, "GroupName"]);
					if ((bool)gridMrsBase1[RowIndex, "Analysis"])
					{
						MrsBaseB1.ps_CstDec = ((PubTools.Str2Int(gridMrsBase1[RowIndex, "CostDec"]) == F_MainCst) ? null : gridMrsBase1[RowIndex, "CostDec"].ToString());
					}
					MrsBaseB1.UpdItem();
					if (CostKind.Trim() == "")
					{
						aArr.Add(F_UserID);
						aArr.Add(CommonMethods.GetFormTypeTitle(FormType.MrsBase));
						PriceAnalysis PA1 = new PriceAnalysis(aArr);
						PA1.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
						PA1.ps_prjcode = ProjectCode;
						PA1.ps_Issue = F_chgCount;
						PA1.UpdateAnalyCostDec((int)gridMrsBase1[RowIndex, "PubCode"], MrsBaseB1.ps_CstDec, MrsBaseB1.ps_AmtDec);
						PA1 = null;
						if (F_ActionName != PccesFormAction.MrsBase)
						{
							Archnowledge.Pcces.BUDClass.ItemA dbItemA = new Archnowledge.Pcces.BUDClass.ItemA(aArr);
							dbItemA.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
							dbItemA.ps_projectCode = F_ProjectCode;
							dbItemA.ps_pubCode = gridMrsBase1[RowIndex, "PubCode"].ToString();
							dbItemA.ps_CstDec = ((PubTools.Str2Int(gridMrsBase1[RowIndex, "CostDec"]) == F_AnaCst) ? null : gridMrsBase1[RowIndex, "CostDec"].ToString());
							dbItemA.ps_AmtDec = ((PubTools.Str2Int(gridMrsBase1[RowIndex, "AmtDec"]) == F_AnaAmt) ? null : gridMrsBase1[RowIndex, "AmtDec"].ToString());
							dbItemA.UpdateAptoticDec();
							dbItemA = null;
						}
					}
					Archnowledge.Pcces.BUDClass.MrsBaseA MrsBaseA1 = new Archnowledge.Pcces.BUDClass.MrsBaseA(F_UserID, aArr);
					MrsBaseA1.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
					MrsBaseA1.ps_projectcode = F_ProjectCode;
					MrsBaseA1.ps_pccesCode = gridMrsBase1[RowIndex, "PccesCode"].ToString();
					MrsBaseA1.ps_pubCode = gridMrsBase1[RowIndex, "PubCode"].ToString();
					MrsBaseA1.ps_rate = gridMrsBase1[RowIndex, "Rate"].ToString();
					MrsBaseA1.ps_costKind = CostKind;
					MrsBaseA1.ps_memo = gridMrsBase1[RowIndex, "Memo"].ToString();
					MrsBaseA1.ps_Issue = F_chgCount;
					MrsBaseA1.ps_surName = gridMrsBase1[RowIndex, "surName"].ToString();
					MrsBaseA1.ps_QtyDec = ((PubTools.Str2Int(gridMrsBase1[RowIndex, "QtyDec"]) == F_AnaQty) ? null : gridMrsBase1[RowIndex, "QtyDec"].ToString());
					MrsBaseA1.ps_AmtDec = ((PubTools.Str2Int(gridMrsBase1[RowIndex, "AmtDec"]) == F_AnaAmt) ? null : gridMrsBase1[RowIndex, "AmtDec"].ToString());
					MrsBaseA1.ps_CstDec = ((PubTools.Str2Int(gridMrsBase1[RowIndex, "CostDec"]) == F_AnaCst) ? null : gridMrsBase1[RowIndex, "CostDec"].ToString());
					MrsBaseA1.ps_account = ((gridMrsBase1[RowIndex, "Account"] != null) ? gridMrsBase1[RowIndex, "Account"].ToString() : null);
					if (Is75094900())
					{
						MrsBaseA1.ps_extendCode = ((gridMrsBase1[RowIndex, "ExtendCode"] != null) ? gridMrsBase1[RowIndex, "ExtendCode"].ToString() : null);
					}
					if ((bool)gridMrsBase1[RowIndex, "Analysis"])
					{
						MrsBaseA1.ps_CstDec = ((PubTools.Str2Int(gridMrsBase1[RowIndex, "CostDec"]) == F_MainCst) ? null : gridMrsBase1[RowIndex, "CostDec"].ToString());
					}
					MrsBaseA1.UpdItem();
					if (EditedColumnName == "PwrSet")
					{
						ProjMrsA projMrsA = null;
						switch (F_ActionName)
						{
						case PccesFormAction.BID:
							projMrsA = new BidProjMrsA();
							break;
						case PccesFormAction.BUD:
							projMrsA = new BudProjMrsA();
							break;
						case PccesFormAction.SplitContract:
							projMrsA = new SubProjMrsA();
							break;
						}
						if (projMrsA != null)
						{
							int pwrSet = PwrSet.GetCode(dsPwrSet, ArchConvert.Obj2String(gridMrsBase1[RowIndex, "PwrSet"]));
							bool updateItemA = false;
							ExecResult ER = projMrsA.SetPwrSet(F_ProjectCode, ArchConvert.Obj2Int(gridMrsBase1[RowIndex, "PubCode"]), pwrSet, updateItemA);
							if (ER.ReturnCode != 0)
							{
								MessageBox.Show(ER.Message, "發包權限存取錯誤");
							}
						}
					}
					if (base.Owner is frmBudget)
					{
						(base.Owner as frmBudget)._IsAnConfirmReCal = "Y";
					}
					if (EditedColumnName.ToUpper() == "QTYDEC")
					{
						iQty++;
						CellStyle styQtyDec = gridMrsBase1.Styles.Add("QtyDec" + iQty);
						if (PubTools.Str2Int(gridMrsBase1[RowIndex, "QtyDec"]) > 0)
						{
							styQtyDec.Format = "###,###,###,##0." + "0".PadLeft(PubTools.Str2Int(gridMrsBase1[RowIndex, "QtyDec"]), '0');
						}
						else
						{
							styQtyDec.Format = "###,###,###,##0";
						}
						gridMrsBase1.SetCellStyle(RowIndex, gridMrsBase1.Cols["Qty"].SafeIndex, styQtyDec);
					}
					if (EditedColumnName.ToUpper() == "COSTDEC")
					{
						iCst++;
						CellStyle styCstDec = gridMrsBase1.Styles.Add("CstDec" + iCst);
						if (PubTools.Str2Int(gridMrsBase1[RowIndex, "CostDec"]) > 0)
						{
							styCstDec.Format = "###,###,###,##0." + "0".PadLeft(PubTools.Str2Int(gridMrsBase1[RowIndex, "CostDec"]), '0');
						}
						else
						{
							styCstDec.Format = "###,###,###,##0";
						}
						gridMrsBase1.SetCellStyle(RowIndex, gridMrsBase1.Cols["Cost"].SafeIndex, styCstDec);
					}
					if (EditedColumnName.ToUpper() == "AMTDEC")
					{
						iAmt++;
						CellStyle styAmtDec = gridMrsBase1.Styles.Add("AmtDec" + iAmt);
						if (PubTools.Str2Int(gridMrsBase1[RowIndex, "AmtDec"]) > 0)
						{
							styAmtDec.Format = "###,###,###,##0." + "0".PadLeft(PubTools.Str2Int(gridMrsBase1[RowIndex, "AmtDec"]), '0');
						}
						else
						{
							styAmtDec.Format = "###,###,###,##0";
						}
						gridMrsBase1.SetCellStyle(RowIndex, gridMrsBase1.Cols["Amount"].SafeIndex, styAmtDec);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("gridMrsBase1_AfterEdit Error : " + ex.Message);
				}
				break;
			}
			if (chkReCalcu.Checked && EditedColumnName != "fixPrice")
			{
				ChildPubCode = ArchConvert.Obj2Int(gridMrsBase1[RowIndex, "PubCode"]);
				ListNo = ArchConvert.Obj2Int(gridMrsBase1[RowIndex, "ListNo"]);
				if (!(EditedColumnName == "Qty"))
				{
					if (!(EditedColumnName == "Cost") || (bool)gridMrsBase1[RowIndex, "Analysis"])
					{
						switch (EditedColumnName)
						{
						case "Rate":
						case "CostKind":
						case "QtyDec":
						case "CostDec":
							goto IL_13dd;
						}
						if (!(EditedColumnName == "AmtDec"))
						{
							goto IL_1503;
						}
					}
					goto IL_13dd;
				}
				DoMrsCalculate(ChildPubCode, ListNo, CalculateChangeType.ChangeQty);
			}
			else if (IsAllowRepeatItem && EditedColumnName == "Cost" && (gridMrsBase1[RowIndex, "CostKind"] == null || gridMrsBase1[RowIndex, "CostKind"].ToString().Trim() == ""))
			{
				string sPccesCode = gridMrsBase1[RowIndex, "PccesCode"].ToString().Trim();
				double dCost = PubTools.Str2Double(gridMrsBase1[RowIndex, "Cost"]);
				for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
				{
					if (sPccesCode == gridMrsBase1[i, "PccesCode"].ToString().Trim())
					{
						gridMrsBase1[i, "Cost"] = dCost;
					}
				}
			}
			goto IL_1503;
		}
		goto IL_1566;
		IL_13dd:
		DoMrsCalculate(ChildPubCode, ListNo, CalculateChangeType.ChangeCost);
		goto IL_1503;
		IL_1503:
		if (EditedColumnName == "Source" && gridMrsBase1[RowIndex, "Source"].ToString() != "日報統計" && gridMrsBase1[RowIndex, "Source"].ToString() != "固定成本")
		{
			GetLowerData();
		}
		goto IL_1566;
		IL_1566:
		SetPopupMenuEnable();
	}

	private void gridMrsBase1_StartEdit(object sender, RowColEventArgs e)
	{
		try
		{
			FORM_STATUS = FormStatus.Edit;
			if (e.Col == gridMrsBase1.Cols["Cost"].SafeIndex)
			{
				if ((bool)gridMrsBase1[e.Row, "Analysis"])
				{
					gridMrsBase1[e.Row, "Cost"] = string.Format("{0:N" + F_MainCst + "}", gridMrsBase1[e.Row, "Cost"]);
				}
				else
				{
					gridMrsBase1[e.Row, "Cost"] = string.Format("{0:N" + gridMrsBase1[e.Row, "CostDec"].ToString() + "}", gridMrsBase1[e.Row, "Cost"]);
				}
			}
			switch (gridMrsBase1.Cols[e.Col].Name.ToUpper())
			{
			case "QTY":
				if (F_AnaQty > 0)
				{
					gridMrsBase1.Cols[e.Col].Format = "###,###,###,##0." + "0".PadLeft(F_AnaQty, '0');
				}
				else
				{
					gridMrsBase1.Cols[e.Col].Format = "###,###,###,##0";
				}
				break;
			case "COST":
				if (F_AnaCst > 0)
				{
					gridMrsBase1.Cols[e.Col].Format = "###,###,###,##0." + "0".PadLeft(F_AnaCst, '0');
				}
				else
				{
					gridMrsBase1.Cols[e.Col].Format = "###,###,###,##0";
				}
				break;
			case "AMOUNT":
				if (F_AnaAmt > 0)
				{
					gridMrsBase1.Cols[e.Col].Format = "###,###,###,##0." + "0".PadLeft(F_AnaAmt, '0');
				}
				else
				{
					gridMrsBase1.Cols[e.Col].Format = "###,###,###,##0";
				}
				break;
			}
			SetPopupMenuDisable();
		}
		catch (Exception ex)
		{
			MessageBox.Show("gridMrsBase1_StartEdit Exception: " + ex.Message);
		}
	}

	private void HideCols(bool IsHide)
	{
		if (IsHide)
		{
			gridMrsBase1.Cols["PubCode"].Visible = false;
			gridMrsBase1.Cols["Analysis"].Visible = false;
			gridMrsBase1.Cols["usrQty"].Visible = false;
			gridMrsBase1.Cols["PSNo"].Visible = false;
			if (F_ActionName == PccesFormAction.MrsBase)
			{
				gridMrsBase1.Cols["LockCost"].Visible = false;
			}
			else
			{
				gridMrsBase1.Cols["LockCost"].Visible = true;
			}
			try
			{
				gridMrsBase1.Cols["QtyDec"].Visible = false;
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormMrsBaseBreakdown.cs" + ex.Message);
			}
		}
	}

	private void SetPopupMenuDisable()
	{
		ultraToolbarsManager1.Enabled = false;
	}

	private void SetPopupMenuEnable()
	{
		ultraToolbarsManager1.Enabled = true;
	}

	private void gridMrsBase1_AfterRowColChange(object sender, RangeEventArgs e)
	{
		try
		{
			if (F_IsSBID || sBindFlag == "BINDING")
			{
				return;
			}
			iAfterChangeCol++;
			label7.Text = "AfterRowColChange:" + iAfterChangeCol;
			if (gridMrsBase1.Col != 0 && !gridMrsBase1.Cols[gridMrsBase1.MouseCol].AllowEditing && FORM_STATUS != FormStatus.Edit)
			{
				gridMrsBase1.Col = 0;
			}
			if (ultraToolbarsManager1.Enabled && gridMrsBase1.Row == 0)
			{
				ultraToolbarsManager1.Tools["mnuEdit"].SharedProps.Enabled = false;
				ultraToolbarsManager1.Tools["mnuDelete"].SharedProps.Enabled = false;
				ultraToolbarsManager1.Tools["mnuTool_Up"].SharedProps.Enabled = false;
				ultraToolbarsManager1.Tools["mnuTool_Down"].SharedProps.Enabled = false;
			}
			else if (ultraToolbarsManager1.Enabled)
			{
				if (F_IsLockAn)
				{
					return;
				}
				ultraToolbarsManager1.Tools["mnuEdit"].SharedProps.Enabled = true;
				ultraToolbarsManager1.Tools["mnuDelete"].SharedProps.Enabled = true;
				ultraToolbarsManager1.Tools["mnuTool_Up"].SharedProps.Enabled = true;
				ultraToolbarsManager1.Tools["mnuTool_Down"].SharedProps.Enabled = true;
			}
			if (pnlInfo.Width != 0 && Tab_A.Tab.Selected && F_IsUseIR)
			{
				string ItemKind = gridMrsBase1[gridMrsBase1.Row, "CostKind"].ToString().Trim();
				switch (ItemKind)
				{
				default:
					if (!(ItemKind == "%"))
					{
						ultraToolbarsManager1.Tools["mnuIRSet"].SharedProps.Enabled = false;
						ultraToolbarsManager1.Tools["mnuIRCopy"].SharedProps.Enabled = false;
						StatusBar1.Visible = false;
						break;
					}
					goto case "Z";
				case "Z":
				case "L":
				case "E":
				case "M":
					ultraToolbarsManager1.Tools["mnuIRSet"].SharedProps.Enabled = true;
					ultraToolbarsManager1.Tools["mnuIRCopy"].SharedProps.Enabled = true;
					Execute_IRSet();
					break;
				}
			}
			else
			{
				ultraToolbarsManager1.Tools["mnuIRSet"].SharedProps.Enabled = false;
				ultraToolbarsManager1.Tools["mnuIRCopy"].SharedProps.Enabled = false;
			}
			if (ultraToolbarsManager1.Tools["mnuIRSet"].SharedProps.Enabled)
			{
				c1FlexGrid1.Visible = true;
			}
			else
			{
				c1FlexGrid1.Visible = false;
			}
			if (F_IsLockAn)
			{
				ultraToolbarsManager1.Tools["mnuIRSet"].SharedProps.Enabled = false;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("gridMrsBase1_AfterRowColChange Exception: " + ex.Message);
		}
		ResetToolbar();
	}

	private void gridMrsBase1_MouseMove(object sender, MouseEventArgs e)
	{
		if (sBindFlag == "BINDING")
		{
			return;
		}
		int rowIndex = gridMrsBase1.MouseRow;
		int colIndex = gridMrsBase1.MouseCol;
		if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right && gridMrsBase1.Cols[colIndex].Name == "AnaImg")
		{
			if (rowIndex > 0 && (bool)gridMrsBase1[rowIndex, "Analysis"])
			{
				Cursor = Cursors.Hand;
			}
		}
		else
		{
			Cursor = Cursors.Default;
		}
	}

	private void SetColsEditSymbol()
	{
		try
		{
			for (int i = 1; i < gridMrsBase1.Cols.Count; i++)
			{
				if (gridMrsBase1.Cols[i].AllowEditing)
				{
					CellRange rg = gridMrsBase1.GetCellRange(0, i);
					rg.Style = gridMrsBase1.Styles["EditMode"];
					rg.Image = imageList2.Images[1];
				}
			}
			gridMrsBase1.Refresh();
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormMrsBaseBreakdown.cs" + ex.Message);
		}
	}

	private void FormMrsBaseBreakdown_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (base.WindowState != FormWindowState.Maximized)
		{
			CommonMethods.WriteIniValue("BreakDown", "LocationX", base.Location.X.ToString());
			CommonMethods.WriteIniValue("BreakDown", "LocationY", base.Location.Y.ToString());
			CommonMethods.WriteIniValue("BreakDown", "Width", base.Size.Width.ToString());
			CommonMethods.WriteIniValue("BreakDown", "Height", base.Size.Height.ToString());
		}
		CommonMethods.WriteIniValue("BreakDown", "WindowState", base.WindowState.ToString());
		if (chkReCalcu != null)
		{
			CommonMethods.WriteIniValue("BreakDown", "AutoReCalcu", chkReCalcu.Checked ? "1" : "0");
		}
		if (Frm != null)
		{
			Frm.Close();
		}
		GridPropertySetting.SaveGridProperty(F_UserID, base.Name, gridMrsBase1);
	}

	private void ultraToolbarsManager1_BeforeToolbarListDropdown(object sender, BeforeToolbarListDropdownEventArgs e)
	{
		if (!(sBindFlag == "BINDING"))
		{
			e.Cancel = true;
		}
	}

	private void txtAnalysisQty_Validating(object sender, CancelEventArgs e)
	{
		if (!CommonMethods.CheckValidString((sender as UltraTextEditor).Text))
		{
			e.Cancel = true;
		}
	}

	private void gridMrsBase1_KeyDown(object sender, KeyEventArgs e)
	{
		try
		{
			if (e.Control && e.KeyCode == Keys.Return)
			{
				int iRow = gridMrsBase1.Row;
				ExecuteEditItem();
				gridMrsBase1.Row = iRow;
				Refresh();
			}
			if (e.Alt && e.KeyCode == Keys.Z && ultraToolbarsManager1.Tools["mnuAnalysis"].SharedProps.Enabled)
			{
				GoNextLevelAnalysis(gridMrsBase1.Row, gridMrsBase1.Cols["Analysis"].SafeIndex);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("gridMrsBase1_KeyDown Exception: " + ex.Message);
		}
	}

	private void BtnAdjust_Click(object sender, EventArgs e)
	{
		string l_Message = "調價方法是以調整【數量】方式來達到調價目的，\n\n若您的業主不允許異動【數量】時，請勿以此方式調價!!\n\n您確定要調價嗎?";
		if (MessageBox.Show(this, l_Message, "警示", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes && PubTools.Str2Double(lblPrice.Text) != 0.0)
		{
			if (BtnAdjust.Text == "調價")
			{
				BtnAdjust.Text = "V";
				txtPrice.Location = lblPrice.Location;
				txtPrice.Size = lblPrice.Size;
				txtPrice.Text = lblPrice.Text;
				lblPrice.Visible = false;
				txtPrice.Visible = true;
				txtPrice.Focus();
			}
			else
			{
				BtnAdjust.Text = "調價";
				lblPrice.Text = txtPrice.Text;
				lblPrice.Visible = true;
				txtPrice.Visible = false;
				AdjustPrice();
			}
		}
	}

	private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			ChangeAnalysisState2("Enter");
			AdjustPrice();
		}
		else if (e.KeyChar == '\u001b')
		{
			ChangeAnalysisState2("");
		}
	}

	private void lblPrice_TextChanged(object sender, EventArgs e)
	{
		if (PubTools.Str2Double(lblPrice.Text) == 0.0)
		{
			BtnAdjust.Enabled = false;
		}
		else if (F_IsSBID)
		{
			BtnAdjust.Enabled = false;
		}
		else
		{
			BtnAdjust.Enabled = true;
		}
	}

	private void ultraButton1_Click(object sender, EventArgs e)
	{
		MessageBox.Show(this, "Excel匯出、匯入動作僅限於本專案使用", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		string sFilter = "Microsoft Excel 97/2000 files (*.xls)|*.xls";
		saveFileDialog1.Filter = sFilter;
		saveFileDialog1.RestoreDirectory = true;
		saveFileDialog1.FileName = lblPccesCode.Text.Trim();
		if (saveFileDialog1.ShowDialog() == DialogResult.OK)
		{
			ExpToEXCEL(saveFileDialog1.FileName);
			MessageBox.Show(this, "輸出完成", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void ExpToEXCEL(string sFile)
	{
		try
		{
			ArrayList aArr = new ArrayList();
			aArr.Add(F_UserID);
			aArr.Add("WinFORM 單價分析轉出子項");
			ModifyDB StdCom = new ModifyDB(F_ProjectCode, aArr);
			string ls_fn = CommonMethods.GetActionNameString(F_ActionName);
			DataTable DT_B = new DataTable();
			DataTable DT_C = new DataTable();
			if (ls_fn == "MRS")
			{
				DT_B = StdCom.DBList("Select * from MrsBaseB Where ParentCode=" + parentPubCode + " ");
				DT_C = StdCom.DBList("Select distinct * from MrsBaseC Where ParentCode=" + parentPubCode + " ");
			}
			else
			{
				DT_B = StdCom.DBList("Select * from " + ls_fn + "ProjMrsB Where ProjectCode='" + F_ProjectCode + "' and ParentCode=" + parentPubCode + " ");
				DT_C = StdCom.DBList("Select distinct * from " + ls_fn + "ProjMrsC Where ProjectCode='" + F_ProjectCode + "' and ParentCode=" + parentPubCode + " ");
			}
			Aspose.Cells.License license = new Aspose.Cells.License();
			license.SetLicense("Aspose.Custom.lic");
			Excel myExcel = new Excel();
			myExcel.Worksheets.Add();
			Worksheet mySheet = myExcel.Worksheets[0];
			Cells myCells = mySheet.Cells;
			mySheet.Name = "MrsB";
			if (ls_fn == "MRS")
			{
				for (int j = 0; j < DT_B.Columns.Count; j++)
				{
					mySheet.Cells[0, j].PutValue(DT_B.Columns[j].Caption);
				}
			}
			else
			{
				for (int j = 1; j < DT_B.Columns.Count; j++)
				{
					mySheet.Cells[0, j - 1].PutValue(DT_B.Columns[j].Caption);
				}
			}
			if (ls_fn == "MRS")
			{
				for (int i = 0; i < DT_B.Rows.Count; i++)
				{
					for (int j = 0; j < DT_B.Columns.Count; j++)
					{
						mySheet.Cells[i + 1, j].PutValue(DT_B.Rows[i][j]);
					}
				}
			}
			else
			{
				for (int i = 0; i < DT_B.Rows.Count; i++)
				{
					for (int j = 1; j < DT_B.Columns.Count; j++)
					{
						mySheet.Cells[i + 1, j - 1].PutValue(DT_B.Rows[i][j]);
					}
				}
			}
			myExcel.Worksheets.Add();
			Worksheet mySheet2 = myExcel.Worksheets[1];
			Cells myCells2 = mySheet2.Cells;
			mySheet2.Name = "MrsC";
			if (ls_fn == "MRS")
			{
				for (int j = 0; j < DT_C.Columns.Count; j++)
				{
					mySheet2.Cells[0, j].PutValue(DT_C.Columns[j].Caption);
				}
			}
			else
			{
				for (int j = 1; j < DT_C.Columns.Count; j++)
				{
					mySheet2.Cells[0, j - 1].PutValue(DT_C.Columns[j].Caption);
				}
			}
			if (ls_fn == "MRS")
			{
				for (int i = 0; i < DT_C.Rows.Count; i++)
				{
					for (int j = 0; j < DT_C.Columns.Count; j++)
					{
						mySheet2.Cells[i + 1, j].PutValue(DT_C.Rows[i][j]);
					}
				}
			}
			else
			{
				for (int i = 0; i < DT_C.Rows.Count; i++)
				{
					for (int j = 1; j < DT_C.Columns.Count; j++)
					{
						mySheet2.Cells[i + 1, j - 1].PutValue(DT_C.Rows[i][j]);
					}
				}
			}
			myExcel.Save(sFile);
			StdCom = null;
			DT_B = null;
			DT_C = null;
			myExcel = null;
		}
		catch (Exception ex)
		{
			Console.Write(ex.Message);
		}
	}

	private void ultraButton4_Click(object sender, EventArgs e)
	{
		if (MessageBox.Show(this, "確定要刪除所有分析子項嗎?", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			ArrayList aArr = new ArrayList();
			aArr.Add(F_UserID);
			aArr.Add(CommonMethods.GetFormTypeTitle(FormType.MrsBase));
			Archnowledge.Pcces.BUDClass.MrsBaseB MrsBaseB1 = new Archnowledge.Pcces.BUDClass.MrsBaseB(aArr);
			MrsBaseB1.ps_projectcode = ProjectCode;
			MrsBaseB1.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
			MrsBaseB1.ps_parentCode = parentPubCode.ToString();
			MrsBaseB1.ps_Issue = F_chgCount;
			MrsBaseB1.DeleItems();
		}
		GetLowerData();
		if (chkReCalcu.Checked)
		{
			DoMrsCalculate();
		}
	}

	private void ultraButton5_Click(object sender, EventArgs e)
	{
		string sMessageBox = "";
		string sFilter = "Microsoft Excel 97/2000 files (*.xls)|*.xls";
		openFileDialog1.Filter = sFilter;
		openFileDialog1.RestoreDirectory = true;
		if (openFileDialog1.ShowDialog() == DialogResult.OK)
		{
			Aspose.Cells.License license = new Aspose.Cells.License();
			license.SetLicense("Aspose.Custom.lic");
			Excel myExcel = new Excel();
			myExcel.Open(openFileDialog1.FileName);
			Worksheet mySheet = myExcel.Worksheets[0];
			DataTable MrsBData = new DataTable();
			sMessageBox = Import(openFileDialog1.FileName, ref MrsBData, "MrsB");
			DataTable MrsCData = new DataTable();
			sMessageBox = Import(openFileDialog1.FileName, ref MrsCData, "MrsC");
			if (sMessageBox != "")
			{
				MessageBox.Show(this, sMessageBox, "錯誤", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);
			}
		}
		if (chkReCalcu.Checked)
		{
			DoMrsCalculate();
		}
	}

	private string Import(string ExcelFilePath, ref DataTable DailyBaseDT, string sheetName)
	{
		string Message = "";
		if (ExcelFilePath != "")
		{
			Aspose.Cells.License license = new Aspose.Cells.License();
			license.SetLicense("Aspose.Custom.lic");
			Excel myExcel = new Excel();
			myExcel.Open(ExcelFilePath);
			try
			{
				Worksheet mySheet0 = myExcel.Worksheets[sheetName];
				DailyBaseDT = mySheet0.Cells.ExportDataTable(0, 0, 100, 23);
				Message = DoImport(ref DailyBaseDT, sheetName);
			}
			catch (Exception ex)
			{
				Message = ex.Message;
			}
		}
		return Message;
	}

	private string DoImport(ref DataTable DailyBaseDT, string sheetName)
	{
		string Message = "";
		string ls_selectstr = "";
		string l_strName = "";
		string l_strValue = "";
		for (int i = 1; i < DailyBaseDT.Rows.Count && DailyBaseDT.Rows[i][0] != DBNull.Value && DailyBaseDT.Rows[i][1] != DBNull.Value; i++)
		{
			ArrayList aArr = new ArrayList();
			aArr.Add(F_UserID);
			aArr.Add("WinFORM 單價分析轉出子項");
			ModifyDB StdCom = new ModifyDB(F_ProjectCode, aArr);
			string ls_fn = CommonMethods.GetActionNameString(F_ActionName);
			if (ls_fn == "MRS")
			{
				if (DailyBaseDT.Rows[i][0].ToString() != "")
				{
					l_strName = "projectCode,parentCode,";
					l_strValue = "'" + ProjectCode + "','" + PubCode + "',";
					if (DailyBaseDT.Rows[i][1].ToString() != "")
					{
						l_strName = l_strName + DailyBaseDT.Rows[0][1].ToString().Trim() + ",";
						l_strValue = l_strValue + "'" + DailyBaseDT.Rows[i][1].ToString().Trim() + "',";
					}
					if (DailyBaseDT.Rows[i][2].ToString() != "")
					{
						l_strName = l_strName + DailyBaseDT.Rows[0][2].ToString().Trim() + ",";
						l_strValue = l_strValue + "'" + DailyBaseDT.Rows[i][2].ToString().Trim() + "',";
					}
					if (DailyBaseDT.Rows[i][3].ToString() != "")
					{
						l_strName = l_strName + DailyBaseDT.Rows[0][3].ToString().Trim() + ",";
						l_strValue = l_strValue + "'" + DailyBaseDT.Rows[i][3].ToString().Trim() + "',";
					}
					if (DailyBaseDT.Rows[i][4].ToString() != "")
					{
						l_strName = l_strName + DailyBaseDT.Rows[0][4].ToString().Trim() + ",";
						l_strValue = l_strValue + "'" + DailyBaseDT.Rows[i][4].ToString().Trim() + "',";
					}
					if (DailyBaseDT.Rows[i][5].ToString() != "")
					{
						l_strName = l_strName + DailyBaseDT.Rows[0][5].ToString().Trim() + ",";
						l_strValue = l_strValue + "'" + DailyBaseDT.Rows[i][5].ToString().Trim() + "',";
					}
					if (DailyBaseDT.Rows[i][6].ToString() != "")
					{
						l_strName = l_strName + DailyBaseDT.Rows[0][6].ToString().Trim() + ",";
						l_strValue = l_strValue + "'" + DailyBaseDT.Rows[i][6].ToString().Trim() + "',";
					}
					if (DailyBaseDT.Rows[i][7].ToString() != "")
					{
						l_strName = l_strName + DailyBaseDT.Rows[0][7].ToString().Trim() + ",";
						l_strValue = l_strValue + "'" + DailyBaseDT.Rows[i][7].ToString().Trim() + "',";
					}
					if (DailyBaseDT.Rows[i][8].ToString() != "")
					{
						l_strName = l_strName + DailyBaseDT.Rows[0][8].ToString().Trim() + ",";
						l_strValue = l_strValue + "'" + DailyBaseDT.Rows[i][8].ToString().Trim() + "',";
					}
					if (DailyBaseDT.Rows[i][9].ToString() != "")
					{
						l_strName = l_strName + DailyBaseDT.Rows[0][9].ToString().Trim() + ",";
						l_strValue = l_strValue + "'" + DailyBaseDT.Rows[i][9].ToString().Trim() + "',";
					}
					if (DailyBaseDT.Rows[i][10].ToString() != "")
					{
						l_strName = l_strName + DailyBaseDT.Rows[0][10].ToString().Trim() + ",";
						l_strValue = l_strValue + "'" + DailyBaseDT.Rows[i][10].ToString().Trim() + "',";
					}
					if (DailyBaseDT.Rows[i][11].ToString() != "")
					{
						l_strName = l_strName + DailyBaseDT.Rows[0][11].ToString().Trim() + ",";
						l_strValue = l_strValue + "'" + DailyBaseDT.Rows[i][11].ToString().Trim() + "',";
					}
					if (DailyBaseDT.Rows[i][12].ToString() != "")
					{
						l_strName = l_strName + DailyBaseDT.Rows[0][12].ToString().Trim() + ",";
						l_strValue = l_strValue + "'" + DailyBaseDT.Rows[i][12].ToString().Trim() + "',";
					}
					if (DailyBaseDT.Rows[i][13].ToString() != "")
					{
						l_strName = l_strName + DailyBaseDT.Rows[0][13].ToString().Trim() + ",";
						l_strValue = l_strValue + "'" + DailyBaseDT.Rows[i][13].ToString().Trim() + "',";
					}
					if (DailyBaseDT.Rows[i][14].ToString() != "")
					{
						l_strName = l_strName + DailyBaseDT.Rows[0][14].ToString().Trim() + ",";
						l_strValue = l_strValue + "'" + DailyBaseDT.Rows[i][14].ToString().Trim() + "',";
					}
					if (DailyBaseDT.Rows[i][15].ToString() != "")
					{
						l_strName = l_strName + DailyBaseDT.Rows[0][15].ToString().Trim() + ",";
						l_strValue = l_strValue + "'" + DailyBaseDT.Rows[i][15].ToString().Trim() + "',";
					}
					if (DailyBaseDT.Rows[i][16].ToString() != "")
					{
						l_strName = l_strName + DailyBaseDT.Rows[0][16].ToString().Trim() + ",";
						l_strValue = l_strValue + "'" + DailyBaseDT.Rows[i][16].ToString().Trim() + "',";
					}
					if (DailyBaseDT.Rows[i][17].ToString() != "")
					{
						l_strName = l_strName + DailyBaseDT.Rows[0][17].ToString().Trim() + ",";
						l_strValue = l_strValue + "'" + DailyBaseDT.Rows[i][17].ToString().Trim() + "',";
					}
					if (DailyBaseDT.Rows[i][18].ToString() != "")
					{
						l_strName = l_strName + DailyBaseDT.Rows[0][18].ToString().Trim() + ",";
						l_strValue = l_strValue + "'" + DailyBaseDT.Rows[i][18].ToString().Trim() + "',";
					}
					if (DailyBaseDT.Rows[i][19].ToString() != "")
					{
						l_strName = l_strName + DailyBaseDT.Rows[0][19].ToString().Trim() + ",";
						l_strValue = l_strValue + "'" + DailyBaseDT.Rows[i][19].ToString().Trim() + "',";
					}
					if (DailyBaseDT.Rows[i][20].ToString() != "")
					{
						l_strName = l_strName + DailyBaseDT.Rows[0][20].ToString().Trim() + ",";
						l_strValue = l_strValue + "'" + DailyBaseDT.Rows[i][20].ToString().Trim() + "',";
					}
					ls_selectstr = ((!(sheetName == "MrsB")) ? ("Insert into MrsBaseC (" + l_strName.Substring(0, l_strName.Length - 1) + ") values (" + l_strValue.Substring(0, l_strValue.Length - 1) + ")") : ("Insert into MrsBaseB (" + l_strName.Substring(0, l_strName.Length - 1) + ") values (" + l_strValue.Substring(0, l_strValue.Length - 1) + ")"));
					StdCom.DBInse(ls_selectstr);
				}
			}
			else if (DailyBaseDT.Rows[i][0].ToString() != "")
			{
				l_strName = "projectCode,parentCode,";
				l_strValue = "'" + ProjectCode + "','" + PubCode + "',";
				if (DailyBaseDT.Rows[i][1].ToString() != "")
				{
					l_strName = l_strName + DailyBaseDT.Rows[0][1].ToString().Trim() + ",";
					l_strValue = l_strValue + "'" + DailyBaseDT.Rows[i][1].ToString().Trim() + "',";
				}
				if (DailyBaseDT.Rows[i][2].ToString() != "")
				{
					l_strName = l_strName + DailyBaseDT.Rows[0][2].ToString().Trim() + ",";
					l_strValue = l_strValue + "'" + DailyBaseDT.Rows[i][2].ToString().Trim() + "',";
				}
				if (DailyBaseDT.Rows[i][3].ToString() != "")
				{
					l_strName = l_strName + DailyBaseDT.Rows[0][3].ToString().Trim() + ",";
					l_strValue = l_strValue + "'" + DailyBaseDT.Rows[i][3].ToString().Trim() + "',";
				}
				if (DailyBaseDT.Rows[i][4].ToString() != "")
				{
					l_strName = l_strName + DailyBaseDT.Rows[0][4].ToString().Trim() + ",";
					l_strValue = l_strValue + "'" + DailyBaseDT.Rows[i][4].ToString().Trim() + "',";
				}
				if (DailyBaseDT.Rows[i][5].ToString() != "")
				{
					l_strName = l_strName + DailyBaseDT.Rows[0][5].ToString().Trim() + ",";
					l_strValue = l_strValue + "'" + DailyBaseDT.Rows[i][5].ToString().Trim() + "',";
				}
				if (DailyBaseDT.Rows[i][6].ToString() != "")
				{
					l_strName = l_strName + DailyBaseDT.Rows[0][6].ToString().Trim() + ",";
					l_strValue = l_strValue + "'" + DailyBaseDT.Rows[i][6].ToString().Trim() + "',";
				}
				if (DailyBaseDT.Rows[i][7].ToString() != "")
				{
					l_strName = l_strName + DailyBaseDT.Rows[0][7].ToString().Trim() + ",";
					l_strValue = l_strValue + "'" + DailyBaseDT.Rows[i][7].ToString().Trim() + "',";
				}
				if (DailyBaseDT.Rows[i][8].ToString() != "")
				{
					l_strName = l_strName + DailyBaseDT.Rows[0][8].ToString().Trim() + ",";
					l_strValue = l_strValue + "'" + DailyBaseDT.Rows[i][8].ToString().Trim() + "',";
				}
				if (DailyBaseDT.Rows[i][9].ToString() != "")
				{
					l_strName = l_strName + DailyBaseDT.Rows[0][9].ToString().Trim() + ",";
					l_strValue = l_strValue + "'" + DailyBaseDT.Rows[i][9].ToString().Trim() + "',";
				}
				if (DailyBaseDT.Rows[i][10].ToString() != "")
				{
					l_strName = l_strName + DailyBaseDT.Rows[0][10].ToString().Trim() + ",";
					l_strValue = l_strValue + "'" + DailyBaseDT.Rows[i][10].ToString().Trim() + "',";
				}
				if (DailyBaseDT.Rows[i][11].ToString() != "")
				{
					l_strName = l_strName + DailyBaseDT.Rows[0][11].ToString().Trim() + ",";
					l_strValue = l_strValue + "'" + DailyBaseDT.Rows[i][11].ToString().Trim() + "',";
				}
				if (DailyBaseDT.Rows[i][12].ToString() != "")
				{
					l_strName = l_strName + DailyBaseDT.Rows[0][12].ToString().Trim() + ",";
					l_strValue = l_strValue + "'" + DailyBaseDT.Rows[i][12].ToString().Trim() + "',";
				}
				if (DailyBaseDT.Rows[i][13].ToString() != "")
				{
					l_strName = l_strName + DailyBaseDT.Rows[0][13].ToString().Trim() + ",";
					l_strValue = l_strValue + "'" + DailyBaseDT.Rows[i][13].ToString().Trim() + "',";
				}
				if (DailyBaseDT.Rows[i][14].ToString() != "")
				{
					l_strName = l_strName + DailyBaseDT.Rows[0][14].ToString().Trim() + ",";
					l_strValue = l_strValue + "'" + DailyBaseDT.Rows[i][14].ToString().Trim() + "',";
				}
				if (DailyBaseDT.Rows[i][15].ToString() != "")
				{
					l_strName = l_strName + DailyBaseDT.Rows[0][15].ToString().Trim() + ",";
					l_strValue = l_strValue + "'" + DailyBaseDT.Rows[i][15].ToString().Trim() + "',";
				}
				if (DailyBaseDT.Rows[i][16].ToString() != "")
				{
					l_strName = l_strName + DailyBaseDT.Rows[0][16].ToString().Trim() + ",";
					l_strValue = l_strValue + "'" + DailyBaseDT.Rows[i][16].ToString().Trim() + "',";
				}
				if (DailyBaseDT.Rows[i][17].ToString() != "")
				{
					l_strName = l_strName + DailyBaseDT.Rows[0][17].ToString().Trim() + ",";
					l_strValue = l_strValue + "'" + DailyBaseDT.Rows[i][17].ToString().Trim() + "',";
				}
				if (DailyBaseDT.Rows[i][18].ToString() != "")
				{
					l_strName = l_strName + DailyBaseDT.Rows[0][18].ToString().Trim() + ",";
					l_strValue = l_strValue + "'" + DailyBaseDT.Rows[i][18].ToString().Trim() + "',";
				}
				if (DailyBaseDT.Rows[i][19].ToString() != "")
				{
					l_strName = l_strName + DailyBaseDT.Rows[0][19].ToString().Trim() + ",";
					l_strValue = l_strValue + "'" + DailyBaseDT.Rows[i][19].ToString().Trim() + "',";
				}
				if (DailyBaseDT.Rows[i][20].ToString() != "")
				{
					l_strName = l_strName + DailyBaseDT.Rows[0][20].ToString().Trim() + ",";
					l_strValue = l_strValue + "'" + DailyBaseDT.Rows[i][20].ToString().Trim() + "',";
				}
				ls_selectstr = "Insert into " + ls_fn + "Proj" + sheetName + "(" + l_strName.Substring(0, l_strName.Length - 1) + ") values (" + l_strValue.Substring(0, l_strValue.Length - 1) + ")";
				StdCom.DBInse(ls_selectstr);
			}
		}
		return Message;
	}

	private void ultraButton6_Click(object sender, EventArgs e)
	{
		Cursor = Cursors.WaitCursor;
		if (MessageBox.Show(this, "確定要檢查嗎?", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			ultraProgressBar1.Visible = true;
			ultraProgressBar1.Maximum = gridMrsBase1.Rows.Count - 1;
			ultraProgressBar1.Minimum = 0;
			ultraProgressBar1.Value = 0;
			int iFound = 0;
			DataTable DT_CheckIR = new DataTable();
			CellStyle cs_IR = gridMrsBase1.Styles.Add("IR_Err");
			cs_IR.Font = new System.Drawing.Font("Arial", 15f, FontStyle.Bold);
			cs_IR.ForeColor = Color.Red;
			ArrayList tmp_AL1 = new ArrayList();
			tmp_AL1 = new ArrayList();
			tmp_AL1.Add(F_UserID);
			tmp_AL1.Add("(SetTotal) 單價分析公式設定");
			int li_ParentCode = parentPubCode;
			int li_PubCode = -1;
			int li_ListNo = -1;
			Archnowledge.Pcces.BUDClass.MrsBaseC MrsCCom = new Archnowledge.Pcces.BUDClass.MrsBaseC(tmp_AL1);
			MrsCCom.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
			MrsCCom.ps_chgCount = F_chgCount;
			MrsCCom.ps_projectcode = F_ProjectCode;
			for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
			{
				li_PubCode = PubTools.Str2Int(gridMrsBase1[i, "pubCode"]);
				li_ListNo = PubTools.Str2Int(gridMrsBase1[i, "PSNo"]);
				DT_CheckIR = MrsCCom.GetIRList(parentPubCode, li_PubCode, li_ListNo);
				if (DT_CheckIR.Rows.Count > 0)
				{
					string sTheSame = "";
					for (int j = 0; j < DT_CheckIR.Rows.Count; j++)
					{
						if (sTheSame == DT_CheckIR.Rows[j]["ItemListNo"].ToString())
						{
							CellRange cg = gridMrsBase1.GetCellRange(i, gridMrsBase1.Cols["ListNo"].SafeIndex, i, gridMrsBase1.Cols["ListNo"].SafeIndex);
							cg.Style = cs_IR;
							iFound++;
							break;
						}
						if (PubTools.Str2Int(DT_CheckIR.Rows[j]["ItemListNo"]) >= li_ListNo)
						{
							CellRange cg = gridMrsBase1.GetCellRange(i, gridMrsBase1.Cols["ListNo"].SafeIndex, i, gridMrsBase1.Cols["ListNo"].SafeIndex);
							cg.Style = cs_IR;
							iFound++;
							break;
						}
						sTheSame = DT_CheckIR.Rows[j]["ItemListNo"].ToString();
					}
				}
				ultraProgressBar1.Value++;
				if (i % 10 == 0)
				{
					Application.DoEvents();
				}
			}
			if (iFound > 0)
			{
				Cursor = Cursors.Default;
				MessageBox.Show(this, "找到 " + iFound + " 筆資料有問題\n請逐筆檢查 [序號] 欄字型變成 [紅色] 的項目\n\n檢查時，開啟IR 設定後，如果沒有發現異樣也請按[確定]鈕關閉。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				Cursor = Cursors.Default;
				MessageBox.Show(this, "檢查完畢，未發現 IR 項目有誤。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		Cursor = Cursors.Default;
		ultraProgressBar1.Visible = false;
	}

	private void UserReq(object sender, EventArgs e)
	{
		UserRequestEventArgs ee = (UserRequestEventArgs)e;
		DispatchString(ee.Request.ToString());
	}

	private void DispatchString(string ssString)
	{
		try
		{
			Cntrl1 = base.ActiveControl;
			iTextBeamPos = (Cntrl1 as System.Windows.Forms.TextBox).SelectionStart;
			if ((Cntrl1 as System.Windows.Forms.TextBox).SelectedText.Length > 1)
			{
				(Cntrl1 as System.Windows.Forms.TextBox).Text = (Cntrl1 as System.Windows.Forms.TextBox).Text.Replace((Cntrl1 as System.Windows.Forms.TextBox).SelectedText, ssString);
			}
			else
			{
				int iPos = iTextBeamPos;
				int iLen = Cntrl1.Text.Length;
				string Str1 = Cntrl1.Text.Substring(0, iPos);
				string Str2 = Cntrl1.Text.Substring(iPos);
				Cntrl1.Text = Str1 + ssString + Str2;
			}
			iTextBeamPos++;
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormMrsBaseBreakdown.cs" + ex.Message);
			Console.Write(ex.Message);
		}
	}

	private void FormMrsBaseBreakdown_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Control && e.KeyCode == Keys.F1)
		{
			Frm.Show();
			Frm.BringToFront();
		}
		if (e.Control && e.KeyCode == Keys.O)
		{
			ultraButton1_Click(sender, EventArgs.Empty);
		}
		if (e.Control && e.KeyCode == Keys.I)
		{
			ultraButton5_Click(sender, EventArgs.Empty);
		}
		if (e.Control && e.KeyCode == Keys.Q)
		{
			BtnLevelUp_Click(null, null);
		}
		if (e.Control && e.KeyCode == Keys.W)
		{
			chkReCalcu.Checked = !chkReCalcu.Checked;
		}
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormMrsBaseBreakdown");
		}
		if (e.KeyCode == Keys.Escape)
		{
			Hide();
		}
	}

	private void ultraButton7_Click(object sender, EventArgs e)
	{
		pnlInfo.Width = 0;
	}

	private void ultraButton8_Click(object sender, EventArgs e)
	{
		pnlInfo.Width = 0;
	}

	private void BtnSaveIR_Click(object sender, EventArgs e)
	{
		DataTable AnalysisDT = ldt_Analysis.Copy();
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(SetTotal) 單價分析公式設定");
		Archnowledge.Pcces.BUDClass.MrsBaseC MrsCCom = new Archnowledge.Pcces.BUDClass.MrsBaseC(tmp_AL1);
		MrsCCom.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		MrsCCom.ps_projectcode = F_ProjectCode;
		MrsCCom.ps_chgCount = F_chgCount;
		int li_PubListNo = PubTools.Str2Int(gridMrsBase1[gridMrsBase1.Row, "ListNo"].ToString());
		int li_ParentCode = parentPubCode;
		int li_PubCode = PubTools.Str2Int(gridMrsBase1[gridMrsBase1.Row, "PubCode"].ToString());
		if (PubTools.GetAppSet_Bool("UseNewMrsB"))
		{
			MrsCCom.DeleItemAll(li_ParentCode, li_PubCode, li_PubListNo);
		}
		else
		{
			MrsCCom.DeleItemAll(li_ParentCode, li_PubCode);
		}
		for (int i = 1; i < c1FlexGrid1.Rows.Count; i++)
		{
			AnalysisDT.Rows[i - 1]["Chk"] = (bool)c1FlexGrid1.Rows[i]["IsCheck"];
		}
		foreach (DataRow dr in AnalysisDT.Rows)
		{
			if ((bool)dr["Chk"])
			{
				int li_ItemCode = PubTools.Str2Int(dr["pubCode"].ToString());
				int li_ItemListNo = PubTools.Str2Int(dr["ListNo"].ToString());
				MrsCCom.InseItem(li_ParentCode, li_PubCode, li_ItemCode, li_PubListNo, li_ItemListNo);
			}
		}
		MrsCCom = null;
		PubTools.WriteRoughlyLog(tmp_AL1);
		MessageBox.Show(this, "IR 儲存完畢", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	private void ultraButton9_Click(object sender, EventArgs e)
	{
		string sFilter = "Microsoft Excel 97/2000 files (*.xls)|*.xls";
		saveFileDialog1.Filter = sFilter;
		saveFileDialog1.RestoreDirectory = true;
		saveFileDialog1.FileName = "IR 項目列表";
		if (saveFileDialog1.ShowDialog() == DialogResult.OK)
		{
			c1FlexGrid1._ExcelFileName = saveFileDialog1.FileName;
			c1FlexGrid1._ExcelSheeName = "IR 項目列表";
			c1FlexGrid1._IsOpenExcelAfterExport = true;
			c1FlexGrid1.ExecuteExport(c1GridExportType.Excel);
		}
	}

	private void ultraButton10_Click(object sender, EventArgs e)
	{
		string sFilter = "Microsoft Excel 97/2000 files (*.xls)|*.xls";
		saveFileDialog1.Filter = sFilter;
		saveFileDialog1.RestoreDirectory = true;
		saveFileDialog1.FileName = "單價分析引用結果列表";
		if (saveFileDialog1.ShowDialog() == DialogResult.OK)
		{
			gridMrsBase2._ExcelFileName = saveFileDialog1.FileName;
			gridMrsBase2._ExcelSheeName = "單價分析引用結果列表";
			gridMrsBase2._IsOpenExcelAfterExport = true;
			gridMrsBase2.ExecuteExport(c1GridExportType.Excel);
		}
	}

	private void gridMrsBase1_AfterSelChange(object sender, RangeEventArgs e)
	{
		double sel_Amount = 0.0;
		for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
		{
			if (gridMrsBase1.Rows[i].Selected)
			{
				sel_Amount += PubTools.Str2Double(gridMrsBase1[i, "Amount"]);
			}
		}
		ultraStatusBar1.Panels[2].Text = "加總=" + string.Format("{0:N" + F_AnaAmt + "}", sel_Amount);
	}

	private void gridMrsBase1_BeforeEdit(object sender, RowColEventArgs e)
	{
		if (IsGoIntoBeforeEdit)
		{
			return;
		}
		IsGoIntoBeforeEdit = true;
		int colIndex = e.Col;
		int rowIndex = e.Row;
		if (inBeforeEdit && F_Istemplate && gridMrsBase1.Cols[colIndex].Name != "PwrSet")
		{
			IsGoIntoBeforeEdit = false;
			return;
		}
		inBeforeEdit = true;
		if (gridMrsBase1.Cols[colIndex].Name.ToUpper() == "PWRSET")
		{
			bool rowIsOne4Item = ArchConvert.Obj2String(gridMrsBase1[rowIndex, "UnitName"]) == "式" && ArchConvert.Obj2Decimal(gridMrsBase1[rowIndex, "Qty"]) == 1m && !ArchConvert.Obj2Bool(gridMrsBase1[rowIndex, "Analysis"]);
			if (LastRowIsOne4Item != rowIsOne4Item)
			{
				CellStyle csCbPS = gridMrsBase1.Styles["ComboListPS"];
				string comboList = string.Empty;
				foreach (DataRow dr in dsPwrSet.Tables["PwrSet"].Rows)
				{
					if (ArchConvert.Obj2String(gridMrsBase1[rowIndex, "UnitName"]) != "式" || ArchConvert.Obj2Decimal(gridMrsBase1[rowIndex, "Qty"]) != 1m || ArchConvert.Obj2Int(dr["PwrCode"]) != 3)
					{
						comboList = comboList + ArchConvert.Obj2String(dr["PwrName"]) + "|";
					}
				}
				csCbPS.ComboList = comboList;
				LastRowIsOne4Item = rowIsOne4Item;
			}
		}
		if (F_Istemplate)
		{
			if (gridMrsBase1.Cols[colIndex].Name == "PwrSet")
			{
				e.Cancel = false;
				inBeforeEdit = false;
				IsGoIntoBeforeEdit = false;
			}
			else
			{
				e.Cancel = true;
				gridMrsBase1.Col = 0;
				inBeforeEdit = false;
				IsGoIntoBeforeEdit = false;
			}
			return;
		}
		if (gridMrsBase1.Cols[colIndex].Name.ToUpper() == "COST")
		{
			bool showMessage = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "BreakDownData", "NoMessage").ToUpper() != "TRUE";
			object analysis = gridMrsBase1[rowIndex, "Analysis"];
			if (analysis != null && analysis != DBNull.Value && Convert.ToBoolean(analysis))
			{
				if (showMessage)
				{
					MessageBox.Show(this, "不可編輯單價分析項目的單價", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				e.Cancel = true;
				gridMrsBase1.Col = 0;
				inBeforeEdit = false;
				IsGoIntoBeforeEdit = false;
				return;
			}
			object Lock = gridMrsBase1[rowIndex, "Lock"];
			if (Lock != null && Lock != DBNull.Value && Convert.ToBoolean(Lock))
			{
				ArrayList aArr = new ArrayList();
				aArr.Clear();
				aArr.Add(F_UserID);
				aArr.Add("檢查前期單價分析母項是否已存在--" + F_ProjectCode + "(" + lblPccesCode.Text + ")");
				Archnowledge.Pcces.BUDClass.MrsBaseA MRSA = new Archnowledge.Pcces.BUDClass.MrsBaseA(F_UserID, aArr);
				MRSA.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
				MRSA.ps_projectcode = F_ProjectCode;
				bool IsExisted = MRSA.IsExistPccesCodeByVersion(F_ProjectCode, lblPccesCode.Text, (base.Owner as frmBudget)._BudgetChangeCurrentVersion - 1);
				string cstKind = ArchConvert.Obj2String(gridMrsBase1[rowIndex, "CostKind"]).Trim();
				if (IsExisted)
				{
					if (showMessage)
					{
						MessageBox.Show(this, "此工項為前一版次預算書之項目，不可編輯單價", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
					e.Cancel = true;
					gridMrsBase1.Col = 0;
					inBeforeEdit = false;
					IsGoIntoBeforeEdit = false;
					return;
				}
				if ((IsExisted || !(cstKind == "$")) && !IsExisted && cstKind == string.Empty && MRSA.IsExistPccesCodeByVersion(F_ProjectCode, gridMrsBase1[rowIndex, "pccesCode"].ToString(), (base.Owner as frmBudget)._BudgetChangeCurrentVersion - 1))
				{
					if (showMessage)
					{
						MessageBox.Show(this, "此工項為前一版次預算書之項目，不可編輯單價", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
					e.Cancel = true;
					gridMrsBase1.Col = 0;
					inBeforeEdit = false;
					IsGoIntoBeforeEdit = false;
					return;
				}
			}
			string CostKind = ArchConvert.Obj2String(gridMrsBase1[rowIndex, "CostKind"]).Trim();
			if (CostKind != string.Empty && CostKind != "$")
			{
				string l_Message = string.Empty;
				if (CostKind == "%")
				{
					l_Message = "此為以上項目小計百分比，不可編輯單價!!";
				}
				if (CostKind == "L")
				{
					l_Message = "此為人工項目小計百分比，不可編輯單價!!";
				}
				if (CostKind == "E")
				{
					l_Message = "此為機具項目小計百分比，不可編輯單價!!";
				}
				if (CostKind == "M")
				{
					l_Message = "此為材料項目小計百分比，不可編輯單價!!";
				}
				if (CostKind == "Z")
				{
					l_Message = "此為小計項，不可編輯單價!!";
				}
				if (showMessage)
				{
					MessageBox.Show(this, l_Message, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				e.Cancel = true;
				gridMrsBase1.Col = 0;
				inBeforeEdit = false;
				IsGoIntoBeforeEdit = false;
				return;
			}
			if (SysConfig.SysComsEnable && SysConfig.SysEditAfterBudLem.ToUpper() == "DISABLE")
			{
				Archnowledge.Pcces.DomainModule.Coms.BudgetCtrl theBudgetCtrl = new Archnowledge.Pcces.DomainModule.Coms.BudgetCtrl();
				if (!theBudgetCtrl.IsWorkItemCostCanChange(ProjectCode, SysConfig.SysComsDB, ArchConvert.Obj2String(gridMrsBase1[rowIndex, "PccesCode"])))
				{
					e.Cancel = true;
					gridMrsBase1.Col = 0;
					inBeforeEdit = false;
					IsGoIntoBeforeEdit = false;
					return;
				}
			}
		}
		if (gridMrsBase1.Cols[colIndex].Name == "Source")
		{
			string groupName = gridMrsBase1[rowIndex, "GroupName"].ToString();
			if (groupName != string.Empty)
			{
				string comboList = "日報統計|固定成本";
				for (int row = 1; row < gridMrsBase1.Rows.Count; row++)
				{
					if (row != rowIndex && gridMrsBase1[row, "GroupName"].ToString() == groupName && gridMrsBase1[row, "Source"] != null && gridMrsBase1[row, "Source"].ToString() == "日報統計")
					{
						comboList = comboList + "|" + gridMrsBase1[row, "listNo"];
					}
				}
				gridMrsBase1.ComboList = comboList;
			}
			else
			{
				gridMrsBase1.ComboList = null;
			}
		}
		else
		{
			gridMrsBase1.ComboList = null;
		}
		if (colIndex == gridMrsBase1.Cols["Qty"].SafeIndex && F_IsLockAn)
		{
			string pccesCodeHead = ((gridMrsBase1[rowIndex, gridMrsBase1.Cols["PccesCode"].SafeIndex] != null) ? gridMrsBase1[rowIndex, gridMrsBase1.Cols["PccesCode"].SafeIndex].ToString().ToUpper().Substring(0, 1) : "#");
			if ("LEMW".IndexOf(pccesCodeHead) <= -1)
			{
				e.Cancel = true;
				gridMrsBase1.Col = 0;
				inBeforeEdit = false;
				IsGoIntoBeforeEdit = false;
				return;
			}
			if ((pccesCodeHead == "L" && F_IsLockAnalysisQtyL) || (pccesCodeHead == "E" && F_IsLockAnalysisQtyE) || (pccesCodeHead == "M" && F_IsLockAnalysisQtyM) || (pccesCodeHead == "W" && F_IsLockAnalysisQtyW))
			{
				e.Cancel = true;
				gridMrsBase1.Col = 0;
				inBeforeEdit = false;
				IsGoIntoBeforeEdit = false;
				return;
			}
		}
		string ItemKind = gridMrsBase1[gridMrsBase1.Row, "costKind"].ToString().Trim();
		if (ItemKind == "Z" || ItemKind == "#")
		{
			e.Cancel = true;
			gridMrsBase1.Col = 0;
			inBeforeEdit = false;
			IsGoIntoBeforeEdit = false;
		}
		else
		{
			inBeforeEdit = false;
		}
	}

	private void cbHistoryWorkRate_AfterCloseUp(object sender, EventArgs e)
	{
		if (cbHistoryWorkRate.Value != null)
		{
			gridMrsBase1[gridMrsBase1.Row, "Qty"] = cbHistoryWorkRate.Value;
			BudProjMrsB budProjMrsB = new BudProjMrsB();
			DataSet DSBudProjMrsB = budProjMrsB.GetUnitPriceAnalysisByParentCode(ProjectCode, parentPubCode.ToString());
			DataView DVBudProjMrsB = new DataView(DSBudProjMrsB.Tables[0]);
			DVBudProjMrsB.RowFilter = "listNo = '" + gridMrsBase1[gridMrsBase1.Row, "ListNo"].ToString() + "'";
			if (DVBudProjMrsB.Count > 0)
			{
				DVBudProjMrsB[0]["qty"] = cbHistoryWorkRate.Value;
				budProjMrsB.UpdateProjMrsB(DSBudProjMrsB);
			}
		}
	}

	private void ultraToolbarsManager1_AfterToolActivate(object sender, ToolEventArgs e)
	{
		if (e.Tool.Key == "GetSubItemQtyAmt")
		{
			SetUpCboSubItemQtyAmt();
		}
	}
}
