using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.CommonClass.Budget;
using Archnowledge.Pcces.DatabaseAccess;
using Archnowledge.Pcces.DomainModule.Bid;
using Archnowledge.Pcces.DomainModule.BudExe;
using Archnowledge.Pcces.DomainModule.Budget;
using Archnowledge.Pcces.DomainModule.BusinessLogical;
using Archnowledge.Pcces.DomainModule.Coms;
using Archnowledge.Pcces.DomainModule.CostStructure;
using Archnowledge.Pcces.DomainModule.ExportExcel;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.DomainModule.LogicalBase;
using Archnowledge.Pcces.DomainModule.MrsBase;
using Archnowledge.Pcces.DomainModule.Sub;
using Archnowledge.Pcces.DomainModule.SubChg;
using Archnowledge.Pcces.PccesMain.About;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.PccesMain.Budget.BudgetChange;
using Archnowledge.Pcces.PccesMain.Budget.ItemNoset;
using Archnowledge.Pcces.PccesMain.Budget.Option;
using Archnowledge.Pcces.PccesMain.Library;
using Archnowledge.Pcces.PccesMain.MrsBase;
using Archnowledge.Pcces.PccesMain.MrsBase.Bookmark;
using Archnowledge.Pcces.PccesMain.ShellLib;
using Archnowledge.Pcces.PccesMain.SysMaintain;
using Archnowledge.Pcces.STDClass;
using Archnowledge.Pcces.XMLClass;
using AxThreed;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using C1.Win.C1Input;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinStatusBar;
using Infragistics.Win.UltraWinToolbars;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class frmBudget : Form
{
	private const string iniFile = "OptionSet.ini";

	private const int DRAGTOL = 5;

	private bool F_IsDirectOpenCNT = false;

	private decimal QtyBeforeEdit = 0m;

	private decimal CostBeforeEdit = 0m;

	private decimal QtyAfterEdit = 0m;

	private decimal CostAfterEdit = 0m;

	private decimal AddQtyBeforeEdit = 0m;

	private decimal AddQtyAfterEdit = 0m;

	private string sItemB_Err = "";

	private bool EnableCOMS = false;

	private bool F_IsHasConfirmReCal = false;

	private int iCount = 1;

	private string F_IsAnConfirmReCal = "N";

	private Archnowledge.Pcces.BUDClass.ItemA dbItemA;

	private bool IsDEBUG_MODE = false;

	private CellStyle CSAnaPrn;

	private int iTextBeamPos = 0;

	private int iXMLDecimalTimes = 0;

	public DataTable dtClipboard = new DataTable();

	private string currentDBName = "";

	private string F_FromDBName = "";

	private string AppLocation = AppDomain.CurrentDomain.BaseDirectory;

	private bool Is_SwitchProject = false;

	private bool IsSubmitBid = false;

	private ArrayList ToolLists = new ArrayList();

	private ArrayList ToolParam = new ArrayList();

	private DataSet DS1 = new DataSet();

	private bool IsAuto = false;

	private bool F_IsNeedToReloadAllData = false;

	private bool[] IsCollaspse;

	private string AdjustmentFlag = "";

	private string F_KeyWord = "";

	private bool HasOpenedBreakdownForm = false;

	private int iAuthorityMSG_Count = 0;

	private bool F_IsUseIR = false;

	private int F_SNo = -1;

	private bool F_IsGoBackOriginalRow = true;

	private bool IsTemplate = false;

	private int LastSelectSno = 0;

	private string F_IsNewProject = "";

	private int ifCount = 0;

	private string sIniFileName = CommonMethods.ExtractFilePath(Application.ExecutablePath) + "PccesMain.ini";

	private string F_Answer = "";

	private int F_NewChildPubCode = -1;

	private string F_surName = "";

	private decimal F_NewChildCost = 0m;

	private decimal F_NewChildRate = 0m;

	private ModiftyMode F_ModifyMode = ModiftyMode.None;

	private int GridCols = 15;

	private object[,] GridColsSquence;

	private object[,] GridColsSquenceForAnalysis;

	private LeftPanelMode PanelMode = LeftPanelMode.Open;

	private object GRID1 = new object();

	private FormStatus FORM_STATUS = FormStatus.Iinitial;

	private DRAGINFO DragInfo = default(DRAGINFO);

	private int[] L1 = new int[9];

	private PccesFormAction FormActionName;

	private string projectCode = "";

	private string projectName = "";

	private string sourceProjectCode = "";

	private bool F_HasRegistered;

	private string userID;

	private string userName = "";

	private string F_FunctionName = "Budget";

	private string serverName = "localhost";

	private DataSet dsParentProjMrsA;

	private bool AddParentBookList;

	private DataSet ParentItemA;

	private bool LastRowIsOne4Item = false;

	private bool ReadOnlyMode = false;

	private int MainItemQtyPrecision = 0;

	private int MainItemCostPrecison = 0;

	private int MainItemAmountPrecision = 0;

	private int MainItemAmountPrecisionDec = 0;

	private int AnalysisQtyPrecision = 0;

	private int AnalysisCostPrecision = 0;

	private int AnalysisAmountPrecision = 0;

	private int iQty = 0;

	private int iCst = 0;

	private int iAmt = 0;

	private DataSet dsItemA;

	private DataTable dtItemA;

	private DataTable dtProject;

	private bool checkData2GridSpace;

	private bool checkData2GridZero;

	private Archnowledge.Pcces.BUDClass.MrsBaseA dbMrsBase;

	private string F_PasteSource_SrcKind;

	private string F_ChangeQTY;

	private string F_NewAddItemFlag = "0";

	private bool F_IsProjectCode = true;

	private bool IsAwardOfBid = false;

	private string F_PasteSource_Project;

	private string F_IsBid = "";

	private DataSet dsPwrSet;

	private bool IsLocked = false;

	private bool IsLockedCnt = false;

	private bool IsLockAnalys = false;

	private string companyDBName;

	private int budgetChangeCurrentVersion = 0;

	private bool HideAmountIsZeroItems = false;

	private bool showOnlyChangedItem = false;

	private bool UseCostStructure = PubTools.GetAppSet_Bool("UseCostStructure");

	private BackgroundWorker backgroundWorker = new BackgroundWorker();

	private BudgetType.Types budgetType;

	private string parentProjectCode = string.Empty;

	private int changeManagementCurrentVersion = 0;

	private bool IsEditItemNo = false;

	private bool _needClose = false;

	private Archnowledge.Pcces.DomainModule.LogicalBase.Project theProject = null;

	private Archnowledge.Pcces.DomainModule.LogicalBase.ItemA theItemA = null;

	private Archnowledge.Pcces.DomainModule.LogicalBase.ItemB theItemB = null;

	private ProjMrsA theProjMrsA = null;

	private Archnowledge.Pcces.DomainModule.LogicalBase.CostKind theCostKind = null;

	private ItemNoSettingManager theItemNoSettingManager = null;

	private FormSys_G_Info1 FM_INFO = null;

	private int ProgressValue = 0;

	private IContainer components;

	public UltraToolbarsManager toolbarsManager;

	private Panel LeftPanel;

	private Panel MainPanel;

	public FunctionButtons functionButtons1;

	private UltraToolbarsDockArea _frmBudget_Toolbars_Dock_Area_Left;

	private UltraToolbarsDockArea _frmBudget_Toolbars_Dock_Area_Right;

	private UltraToolbarsDockArea _frmBudget_Toolbars_Dock_Area_Top;

	private UltraToolbarsDockArea _frmBudget_Toolbars_Dock_Area_Bottom;

	private ImageList imageList1;

	private Panel c;

	private Panel panel3;

	private AxSSPanel axSSPanel1;

	private UltraStatusBar statusBar;

	private OnlineList onlineList1;

	private ImageList imageList2;

	private AxSSPanel ssp_Top;

	private Panel pnl_spliter;

	private AxSSPanel ssp_Upper;

	private AxSSPanel ssp_Bottom;

	private AxSSPanel ssp_Lower;

	private UltraButton Btn_Splt;

	private UltraButton ultraButton2;

	private UltraButton BtnSwitchProject;

	private UltraLabel ultraLabel10;

	private UltraLabel lblProjectData;

	private ImageList iglst_splt_Btn;

	private SaveFileDialog saveFileDialog1;

	private Panel panel2;

	private UltraLabel ultraLabel2;

	private AxSSPanel axSSPanel2;

	private UltraLabel lblTotal;

	private System.Windows.Forms.Timer TM_BDGT_AutoSave;

	private UltraCombo cboHisPrice;

	private Control Cntrl1;

	private FormSymbol Frm = new FormSymbol();

	private System.Windows.Forms.Timer tmrReCalAll;

	private System.Windows.Forms.Timer timer1;

	private UltraButton BidbtnClose;

	private UltraButton BtnDownloadDoc;

	private UltraCombo cboSubItemQtyAmt;

	private UltraCombo cboItemChangeHistory;

	public GridBudget gridBudget;

	private UltraButton ultraButton1;

	public object[,] _GridColsSquenceForAnalysis
	{
		get
		{
			return GridColsSquenceForAnalysis;
		}
		set
		{
			GridColsSquenceForAnalysis = value;
		}
	}

	public int _BudgetChangeCurrentVersion => budgetChangeCurrentVersion;

	public bool NeedClose => _needClose;

	public bool _IsProjectCode
	{
		get
		{
			return F_IsProjectCode;
		}
		set
		{
			F_IsProjectCode = value;
		}
	}

	public bool _IsHasConfirmReCal
	{
		get
		{
			return F_IsHasConfirmReCal;
		}
		set
		{
			F_IsHasConfirmReCal = value;
		}
	}

	public string _IsAnConfirmReCal
	{
		get
		{
			return F_IsAnConfirmReCal;
		}
		set
		{
			F_IsAnConfirmReCal = value;
		}
	}

	public string _ProjectCode => projectCode;

	public bool _IsNeedToReloadAllData
	{
		get
		{
			return F_IsNeedToReloadAllData;
		}
		set
		{
			F_IsNeedToReloadAllData = value;
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

	public string _SurName
	{
		get
		{
			return F_surName;
		}
		set
		{
			F_surName = value;
		}
	}

	public bool _IsDirectOpenCNT
	{
		get
		{
			return F_IsDirectOpenCNT;
		}
		set
		{
			F_IsDirectOpenCNT = value;
		}
	}

	public DataSet _dsParentProjMrsA
	{
		get
		{
			return dsParentProjMrsA;
		}
		set
		{
			dsParentProjMrsA = value;
		}
	}

	public bool _AddParentBookList
	{
		get
		{
			return AddParentBookList;
		}
		set
		{
			AddParentBookList = value;
		}
	}

	private bool EnableContextMenu
	{
		set
		{
			if (value)
			{
				toolbarsManager.SetContextMenuUltra(gridBudget, "RightClickMenu");
			}
			else
			{
				toolbarsManager.SetContextMenuUltra(gridBudget, null);
			}
			Archnowledge.Common.DebugUtil.OutputDebugString("EnableContextMenu = " + value);
		}
	}

	public PccesFormAction _ActionName
	{
		get
		{
			return FormActionName;
		}
		set
		{
			FormActionName = value;
			if (FormActionName == PccesFormAction.BID)
			{
				Text = "標單填寫";
				gridBudget.Cols["ItemUnitPrice"].Visible = false;
				gridBudget.Cols["ItemUnitWeight"].Visible = false;
			}
			if (FormActionName == PccesFormAction.BUD)
			{
				theItemA = new BudItemA();
				theItemB = new BudItemB();
				theProject = new BudProject();
				theProjMrsA = new BudProjMrsA();
				theCostKind = new BudCostKind();
			}
			else if (FormActionName == PccesFormAction.BID)
			{
				theItemA = new BidItemA();
				theItemB = new BidItemB();
				theProject = new BidProject();
				theProjMrsA = new BidProjMrsA();
				theCostKind = new BidCostKind();
			}
			else if (FormActionName == PccesFormAction.BUDEXE)
			{
				theItemA = new BudExeItemA();
			}
			else if (FormActionName == PccesFormAction.SplitContract)
			{
				theItemA = new SubItemA();
			}
			else if (FormActionName == PccesFormAction.SubChange)
			{
				theItemA = new SubChgItemA();
			}
			else if (FormActionName == PccesFormAction.CNT)
			{
				theProject = new BudProject();
			}
		}
	}

	public string ProjectCode
	{
		get
		{
			return projectCode;
		}
		set
		{
			projectCode = value;
		}
	}

	public string _MainProjectCode
	{
		get
		{
			return sourceProjectCode;
		}
		set
		{
			sourceProjectCode = value;
		}
	}

	public string ProjectName
	{
		get
		{
			return projectName;
		}
		set
		{
			projectName = value;
		}
	}

	public string _ChangeQTY
	{
		get
		{
			return F_ChangeQTY;
		}
		set
		{
			F_ChangeQTY = value;
		}
	}

	public bool _IsLastBid
	{
		get
		{
			return IsAwardOfBid;
		}
		set
		{
			IsAwardOfBid = value;
		}
	}

	public bool _Istemplate
	{
		get
		{
			return IsTemplate;
		}
		set
		{
			IsTemplate = value;
		}
	}

	public int _NewChildPubCode
	{
		set
		{
			F_NewChildPubCode = value;
		}
	}

	public decimal _NewChildCost
	{
		set
		{
			F_NewChildCost = value;
		}
	}

	public decimal _NewChildRate
	{
		set
		{
			F_NewChildRate = value;
		}
	}

	public string _FormAnswer
	{
		get
		{
			return F_Answer;
		}
		set
		{
			F_Answer = value;
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
			return userID;
		}
		set
		{
			userID = value;
		}
	}

	public string _UserName
	{
		get
		{
			return userName;
		}
		set
		{
			userName = value;
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
			return serverName;
		}
		set
		{
			serverName = value;
		}
	}

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

	public string _IsNewProject
	{
		get
		{
			return F_Answer;
		}
		set
		{
			F_Answer = value;
		}
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

	public frmBudget()
	{
		InitializeComponent();
		backgroundWorker.WorkerSupportsCancellation = true;
		backgroundWorker.DoWork += backgroundWorker_DoWork;
		functionButtons1.ButtonOwner = LeftPanelStatus.Budget;
		CellStyle csCb = gridBudget.Styles.Add("ComboList");
		csCb.DataType = typeof(short);
		csCb.ComboList = "0|1|2|3|4";
		csCb.ForeColor = Color.Navy;
		csCb.TextAlign = TextAlignEnum.LeftCenter;
		csCb.Font = new Font(Font, FontStyle.Bold);
		CellStyle csCbW = gridBudget.Styles.Add("ComboListW");
		csCbW.DataType = typeof(short);
		csCbW.ComboList = "0|1|2|3|4";
		csCbW.ForeColor = Color.Navy;
		csCbW.TextAlign = TextAlignEnum.LeftCenter;
		csCbW.Font = new Font(Font, FontStyle.Bold);
		PwrSet pwrSet = new PwrSet();
		dsPwrSet = pwrSet.GetEnabledPwrSet();
		string comboList = string.Empty;
		foreach (DataRow dr in dsPwrSet.Tables["PwrSet"].Rows)
		{
			comboList = comboList + ArchConvert.Obj2String(dr["PwrName"]) + "|";
		}
		CellStyle csCbPS = gridBudget.Styles.Add("ComboListPS");
		csCbPS.DataType = typeof(string);
		csCbPS.ForeColor = Color.Navy;
		csCbPS.TextAlign = TextAlignEnum.LeftCenter;
		csCbPS.Font = new Font(Font, FontStyle.Bold);
		csCbPS.ComboList = comboList;
		CellStyle cellStyle = gridBudget.Styles.Add("img");
		cellStyle.DataType = typeof(Image);
		cellStyle = gridBudget.Styles.Add("EditMode");
		cellStyle.DataType = typeof(Image);
		cellStyle.ImageAlign = ImageAlignEnum.RightCenter;
		cellStyle = gridBudget.Styles.Normal;
		cellStyle.Border.Direction = BorderDirEnum.Vertical;
		cellStyle.TextAlign = TextAlignEnum.LeftCenter;
		cellStyle.WordWrap = false;
		cellStyle = gridBudget.Styles.Add("SourceNode");
		cellStyle.Font = new Font(gridBudget.Font, FontStyle.Bold);
		GridCols = gridBudget.Cols.Count;
		GridColsSquence = new object[GridCols, 10];
		FORM_STATUS = FormStatus.Iinitial;
		CSAnaPrn = gridBudget.Styles.Add("AnalysisAna");
		CSAnaPrn.BackColor = Color.LightGoldenrodYellow;
	}

	private void frmBudget_Load(object sender, EventArgs e)
	{
		toolbarsManager.Tools["ExportExecutiveBudgetChangeInfo"].SharedProps.Visible = false;
		_needClose = false;
		toolbarsManager.Tools["COMSCheckBudgetFromContract"].SharedProps.Visible = false;
		CheckFormOpened();
		if (FORM_STATUS == FormStatus.Binding)
		{
			return;
		}
		FM_INFO = new FormSys_G_Info1();
		if (FormActionName == PccesFormAction.BUD)
		{
			FM_INFO._InfoString = "【預算書編製】載入中，請稍候！";
		}
		else if (FormActionName == PccesFormAction.CNT)
		{
			FM_INFO._InfoString = "【契約書編製】載入中，請稍候！";
		}
		else
		{
			FM_INFO._InfoString = "【標單填寫】載入中，請稍候！";
		}
		FM_INFO.Show();
		Application.DoEvents();
		Cursor = Cursors.WaitCursor;
		dtProject = theProject.GetProject(projectCode).Tables[0];
		if (FormActionName == PccesFormAction.BID)
		{
			IsSubmitBid = IsSubmitBit();
		}
		SysUser sysUser = new SysUser();
		currentDBName = sysUser.GetSysUserDatabaseName(userID);
		UserDefined userDefined = new UserDefined();
		companyDBName = userDefined.GetPccesCompanyDB();
		EnableCOMS = SysConfig.SysComsEnable;
		if (EnableCOMS && (FormActionName == PccesFormAction.BUDEXE || FormActionName == PccesFormAction.BUD))
		{
			toolbarsManager.Tools["ExportExecutiveBudgetChangeInfo"].SharedProps.Visible = true;
		}
		base.ParentForm.Text = "PCCES Win 4.3 " + ((FormActionName == PccesFormAction.BUD) ? "【預算書編製】" : "【標單填寫】");
		statusBar.Panels[1].Text = "目前資料庫：" + sysUser.GetSysUserDatabaseDesc(userID);
		lblProjectData.Text = "【" + projectCode + "】" + projectName;
		LoadIniSetting();
		CheckExcelExport();
		GetDecimalSetting();
		InitColumnVisibility();
		InitFunctionButton();
		SetBidBarckgroudColor();
		if (!Is_SwitchProject)
		{
			onlineList1._UserID = userID;
			onlineList1._UserName = userName;
			onlineList1._ServerName = serverName;
			onlineList1._FunctionName = F_FunctionName;
			onlineList1._HasRegistered = HasRegistered();
			onlineList1.Connect();
			CreateClipboardDataTable();
		}
		toolbarsManager.BeginUpdate();
		string sIsEidtNumber = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "BDGT", "IsEidtNumber");
		IsEditItemNo = ArchConvert.Obj2Bool(sIsEidtNumber);
		initBudgetType();
		InitBudgetChange();
		InitChangeManagement();
		bool delayClose = false;
		if (SysConfig.SysSingleEditLockMode && budgetChangeCurrentVersion > 0)
		{
			BudProject theBudProject = new BudProject();
			theBudProject.GiveBackBudProjSingleEdit(projectCode, userID);
			string LockerInfo = theBudProject.GetBudCurrentEditor(projectCode);
			if (LockerInfo == string.Empty)
			{
				if (theBudProject.AcquireBudProjSingleEdit(projectCode, userID))
				{
					ReadOnlyMode = false;
					SetProjReadOnly(ReadOnlyMode);
				}
				else
				{
					DialogResult dr = MessageBox.Show("此專案已由他人鎖定編輯中,請問是否要繼續以唯讀模式開啟專案瀏覽?(選否則離開此專案)\n專案目前鎖定者:" + LockerInfo, "詢問", MessageBoxButtons.YesNo);
					if (dr == DialogResult.Yes)
					{
						ReadOnlyMode = true;
						SetProjReadOnly(ReadOnlyMode);
					}
					else
					{
						_needClose = true;
					}
				}
			}
			else
			{
				DialogResult dr = MessageBox.Show("此專案已由他人鎖定編輯中,請問是否要繼續以唯讀模式開啟專案瀏覽?(選否則離開此專案)\n專案目前鎖定者:" + LockerInfo, "詢問", MessageBoxButtons.YesNo);
				if (dr == DialogResult.Yes)
				{
					ReadOnlyMode = true;
					SetProjReadOnly(ReadOnlyMode);
				}
				else
				{
					_needClose = true;
				}
			}
		}
		InitToolbarStatus();
		SetupAddonToolBar();
		SetupRestoreSnapshotListCNT();
		SetupRestoreSnapshotList();
		LoadBookmark();
		toolbarsManager.EndUpdate();
		Application.DoEvents();
		Cursor = Cursors.WaitCursor;
		if (Is75094900())
		{
			gridBudget.Cols["ExtendCode"].AllowEditing = true;
		}
		LoadGridProperty();
		InitItemNoManager();
		LoadProjectData();
		ArrayList aArr = new ArrayList();
		aArr.Add(userID);
		aArr.Add("基本工料--抓取單價");
		dbMrsBase = new Archnowledge.Pcces.BUDClass.MrsBaseA(userID, aArr);
		if (F_IsDirectOpenCNT)
		{
			CommonMethods.WriteIniValue("RecentFile", "CNTProject", projectCode);
		}
		else
		{
			string recentProjectType = ((FormActionName == PccesFormAction.BUD) ? "BUDProject" : "BIDProject");
			CommonMethods.WriteIniValue("RecentFile", recentProjectType, projectCode);
		}
		Cursor = Cursors.Default;
		FM_INFO.Close();
		FM_INFO.Dispose();
		FM_INFO = null;
		Frm.OnUserRequest += UserReq;
		gridBudget.Enabled = true;
		GC.Collect();
		FORM_STATUS = FormStatus.Active;
		if (!F_IsProjectCode)
		{
			MessageBox.Show(this, "發現使用中的標單已有相同的專案代碼，系統自動改成【" + projectCode + "】", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		DelAmountItemB();
		if (FormActionName == PccesFormAction.BUD)
		{
			GetCurrentBDGT_Type();
		}
	}

	private string GetCurrentBDGT_Type()
	{
		string sBUD_TYPE = "BUD";
		toolbarsManager.Tools["TakeSnapshotCntFromBid"].SharedProps.Visible = false;
		if (FormActionName == PccesFormAction.BUD)
		{
			ArrayList aArr = new ArrayList();
			aArr.Add(userID);
			aArr.Add("預算編輯--讀取目前預算編輯類型(預算書或契約書)");
			Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
			PROJ.ps_projectCode = projectCode;
			PROJ.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
			sBUD_TYPE = PROJ.GetCurrentProjectActionName(projectCode);
			if (sBUD_TYPE == "")
			{
				sBUD_TYPE = "BUD";
			}
			if (sBUD_TYPE.ToUpper() == "CNT")
			{
				Color colorBudgetCNT = Color.FromArgb(200, 153, 193);
				base.ParentForm.Text = "PCCES Win 4.3 【契約書】";
				axSSPanel1.BackColor = colorBudgetCNT;
				axSSPanel2.BackColor = colorBudgetCNT;
				c.BackColor = colorBudgetCNT;
				ultraLabel10.BackColor = colorBudgetCNT;
				lblProjectData.BackColor = colorBudgetCNT;
				ultraLabel2.BackColor = colorBudgetCNT;
				lblTotal.BackColor = colorBudgetCNT;
				panel2.BackColor = colorBudgetCNT;
				toolbarsManager.Tools["TakeSnapshotCntFromBid"].SharedProps.Visible = true;
			}
			else
			{
				Color colorBudget = Color.FromArgb(153, 204, 102);
				base.ParentForm.Text = "PCCES Win 4.3 【預算書】";
				axSSPanel1.BackColor = colorBudget;
				axSSPanel2.BackColor = colorBudget;
				c.BackColor = colorBudget;
				ultraLabel10.BackColor = colorBudget;
				lblProjectData.BackColor = colorBudget;
				ultraLabel2.BackColor = colorBudget;
				lblTotal.BackColor = colorBudget;
				panel2.BackColor = colorBudget;
			}
			PROJ = null;
		}
		return sBUD_TYPE;
	}

	private bool SetCurrentBDGT_Type(string SrcKind)
	{
		bool retV = true;
		if (FormActionName == PccesFormAction.BUD)
		{
			ArrayList aArr = new ArrayList();
			aArr.Add(userID);
			aArr.Add("預算編輯--設定目前預算編輯類型(預算書或契約書)");
			Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
			PROJ.ps_projectCode = projectCode;
			PROJ.ps_srckind = SrcKind;
			PROJ.SetCurrentProjectActionName(projectCode);
			PROJ = null;
			if (SrcKind.ToUpper() == "CNT")
			{
				toolbarsManager.Tools["BackupProject"].SharedProps.Visible = false;
				toolbarsManager.Tools["RestoreProject"].SharedProps.Visible = false;
			}
			else
			{
				toolbarsManager.Tools["BackupProject"].SharedProps.Visible = true;
				toolbarsManager.Tools["RestoreProject"].SharedProps.Visible = true;
			}
		}
		return retV;
	}

	private bool IsCheckCNT()
	{
		bool retV = false;
		ArrayList aArr = new ArrayList();
		aArr.Add(userID);
		aArr.Add("預算編輯--讀取目前預算編輯類型(預算書或契約書)");
		Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
		PROJ.ps_projectCode = projectCode;
		PROJ.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
		string sBUD_CheckCNT = PROJ.GetIsCheckOutCntValue(projectCode);
		if (sBUD_CheckCNT == "Y")
		{
			retV = true;
		}
		return retV;
	}

	private void SetProjReadOnly(bool IsReadOnly)
	{
		LockOrUnlockToolbar(IsReadOnly);
		if (!IsLocked)
		{
			toolbarsManager.Tools["LockProject"].SharedProps.Enabled = !IsReadOnly;
		}
		else
		{
			toolbarsManager.Tools["LockProject"].SharedProps.Enabled = false;
		}
		if (!IsLocked)
		{
			toolbarsManager.Tools["UnlockProject"].SharedProps.Enabled = false;
		}
		else
		{
			toolbarsManager.Tools["UnlockProject"].SharedProps.Enabled = !IsReadOnly;
		}
		toolbarsManager.Tools["AddNewBudgetChangeVersion"].SharedProps.Enabled = !IsReadOnly;
		toolbarsManager.Tools["ViewBudgetChangeInfo"].SharedProps.Enabled = !IsReadOnly;
		toolbarsManager.Tools["COMSExpandBudget"].SharedProps.Enabled = !IsReadOnly;
		if (!IsLocked)
		{
			toolbarsManager.Tools["DeleteBudgetChangeVersion"].SharedProps.Enabled = !IsReadOnly;
		}
		else
		{
			toolbarsManager.Tools["DeleteBudgetChangeVersion"].SharedProps.Enabled = false;
		}
	}

	private void LoadIniSetting()
	{
		string iniFilePath = AppLocation + "OptionSet.ini";
		string showToolTipOnNarrowColumn = CommonMethods.IniReadValue(iniFilePath, "CommonData", "AllowIsTooltip");
		gridBudget.ShowToolTipOnNarrowColumn = !(showToolTipOnNarrowColumn.ToUpper() == "TRUE");
		string autoSaveProject = CommonMethods.IniReadValue(iniFilePath, "BDGT", "IsAutoSave");
		string autoSaveProjectDuration = CommonMethods.IniReadValue(iniFilePath, "BDGT", "AutoSaveDuration");
		TM_BDGT_AutoSave.Enabled = autoSaveProject.ToUpper() == "TRUE";
		if (PubTools.Str2Decimal(autoSaveProjectDuration) > 0m)
		{
			TM_BDGT_AutoSave.Interval = 60000 * PubTools.Str2Int(autoSaveProjectDuration);
		}
		string sIsEidtNumber = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "BDGT", "IsEidtNumber");
		IsEditItemNo = ArchConvert.Obj2Bool(sIsEidtNumber);
		LoadGreenItemSetting();
	}

	private void LoadGreenItemSetting()
	{
		string FileIni = "OptionSet.ini";
		string greenEnv = CommonMethods.IniReadValue(AppLocation + FileIni, "CommonData", "GreenEnv");
		string greenMethod = CommonMethods.IniReadValue(AppLocation + FileIni, "CommonData", "GreenMethod");
		string greenMaterial = CommonMethods.IniReadValue(AppLocation + FileIni, "CommonData", "GreenMaterial");
		string greenEnergy = CommonMethods.IniReadValue(AppLocation + FileIni, "CommonData", "GreenEnergy");
		gridBudget.Cols["IsGreenItem"].Caption = ((greenEnv == string.Empty) ? "綠色環境" : greenEnv);
		gridBudget.Cols["IsGreenMethod"].Caption = ((greenMethod == string.Empty) ? "綠色工法" : greenMethod);
		gridBudget.Cols["IsGreenMaterial"].Caption = ((greenMaterial == string.Empty) ? "綠色材料" : greenMaterial);
		gridBudget.Cols["IsGreenEnergy"].Caption = ((greenEnergy == string.Empty) ? "綠色能源" : greenEnergy);
	}

	private void CheckExcelExport()
	{
		string StrShowExcelExport = CommonMethods.GetIniValue("EXCEL", "ShowExcelExport").Trim();
		bool IsShowExcelExport = StrShowExcelExport != "" && Convert.ToBoolean(StrShowExcelExport);
		toolbarsManager.Tools["ExportDetailList"].SharedProps.Visible = false;
	}

	private void GetDecimalSetting()
	{
		Archnowledge.Pcces.DomainModule.General.PubDecimal pubDecimal = new Archnowledge.Pcces.DomainModule.General.PubDecimal();
		DataTable dtPubDecimal = pubDecimal.GetPubDecimal(projectCode).Tables[0];
		if (dtPubDecimal.Rows.Count > 0)
		{
			MainItemQtyPrecision = ArchConvert.Obj2Int(dtPubDecimal.Rows[0]["itemQty"]);
			MainItemCostPrecison = ArchConvert.Obj2Int(dtPubDecimal.Rows[0]["itemCost"]);
			MainItemAmountPrecision = ArchConvert.Obj2Int(dtPubDecimal.Rows[0]["itemAmt"]);
			AnalysisQtyPrecision = ArchConvert.Obj2Int(dtPubDecimal.Rows[0]["analysisQty"]);
			AnalysisCostPrecision = ArchConvert.Obj2Int(dtPubDecimal.Rows[0]["analysisCost"]);
			AnalysisAmountPrecision = ArchConvert.Obj2Int(dtPubDecimal.Rows[0]["analysisAmt"]);
		}
		else
		{
			MainItemQtyPrecision = 3;
			MainItemCostPrecison = 2;
			MainItemAmountPrecision = 0;
			AnalysisQtyPrecision = 3;
			AnalysisCostPrecision = 2;
			AnalysisAmountPrecision = 2;
		}
		if (MainItemAmountPrecision == 0 && dtPubDecimal.Rows.Count > 0 && ArchConvert.Obj2Bool(dtPubDecimal.Rows[0]["EnableItemAmt2"]))
		{
			MainItemAmountPrecisionDec = 2;
		}
		else
		{
			MainItemAmountPrecisionDec = MainItemAmountPrecision;
		}
	}

	private void InitColumnVisibility()
	{
		if (FormActionName != PccesFormAction.BUD)
		{
			gridBudget.Cols["CostDec"].Visible = false;
			gridBudget.Cols["AmtDec"].Visible = false;
			gridBudget.Cols["fixPrice"].Visible = false;
			gridBudget.Cols["IsGreenItem"].Visible = false;
			gridBudget.Cols["IsGreenMethod"].Visible = false;
			gridBudget.Cols["IsGreenMaterial"].Visible = false;
			gridBudget.Cols["IsGreenEnergy"].Visible = false;
			gridBudget.Cols["ItemType"].Visible = false;
		}
		bool EnablePwrSet = SysConfig.SysEnablePwrSet;
		gridBudget.Cols["PwrSet"].Visible = EnablePwrSet;
		gridBudget.Cols["Account"].Visible = EnablePwrSet;
		toolbarsManager.Tools["InsertWorkItemPickFromCostStructure"].SharedProps.Visible = UseCostStructure;
		toolbarsManager.Tools["EditCostStructureProperty"].SharedProps.Visible = UseCostStructure;
		gridBudget.Cols["CostUID"].Visible = UseCostStructure;
		gridBudget.Cols["CostUnit"].Visible = UseCostStructure;
		gridBudget.Cols["UnitCost"].Visible = UseCostStructure;
	}

	private void InitFunctionButton()
	{
		functionButtons1._UserID = userID;
		functionButtons1._UserName = userName;
		functionButtons1._ServerName = serverName;
		if (FormActionName == PccesFormAction.BUD)
		{
			functionButtons1._ActiveFunction = "BUD";
			functionButtons1._CurrOpenMode = FunctionOpenMode.Budget;
		}
		else
		{
			functionButtons1._ActiveFunction = "BID";
			functionButtons1._CurrOpenMode = FunctionOpenMode.Bid;
		}
	}

	private bool IsSubmitBit()
	{
		if (dtProject.Rows[0]["CloseBidDate"] != DBNull.Value)
		{
			DateTime CloseBidDate = ArchConvert.Obj2DateTime(dtProject.Rows[0]["CloseBidDate"]);
			return CloseBidDate > new DateTime(1800, 1, 2) && CloseBidDate < DateTime.Today;
		}
		return false;
	}

	private void SetBidBarckgroudColor()
	{
		if (IsSubmitBid)
		{
			Color colorSubmitBid = Color.FromArgb(255, 128, 0);
			base.ParentForm.Text = "PCCES Win 4.3 【投標單】";
			axSSPanel1.BackColor = colorSubmitBid;
			axSSPanel2.BackColor = colorSubmitBid;
			c.BackColor = colorSubmitBid;
			ultraLabel10.BackColor = colorSubmitBid;
			lblProjectData.BackColor = colorSubmitBid;
			ultraLabel2.BackColor = colorSubmitBid;
			lblTotal.BackColor = colorSubmitBid;
			panel2.BackColor = colorSubmitBid;
		}
		else
		{
			Color colorRequestBid = Color.FromArgb(153, 204, 102);
			base.ParentForm.Text = "PCCES Win 4.3 【標單填寫】";
			axSSPanel1.BackColor = colorRequestBid;
			axSSPanel2.BackColor = colorRequestBid;
			c.BackColor = colorRequestBid;
			ultraLabel10.BackColor = colorRequestBid;
			lblProjectData.BackColor = colorRequestBid;
			ultraLabel2.BackColor = colorRequestBid;
			lblTotal.BackColor = colorRequestBid;
			panel2.BackColor = colorRequestBid;
		}
		if (FormActionName == PccesFormAction.BUD)
		{
			base.ParentForm.Text = "PCCES Win 4.3 【預算書編製】";
		}
		if (IsAwardOfBid)
		{
			Color colorAwardOfBid = Color.Pink;
			base.ParentForm.Text = "PCCES Win 4.3 【決標單】";
			axSSPanel1.BackColor = colorAwardOfBid;
			axSSPanel2.BackColor = colorAwardOfBid;
			c.BackColor = colorAwardOfBid;
			ultraLabel10.BackColor = colorAwardOfBid;
			lblProjectData.BackColor = colorAwardOfBid;
			ultraLabel2.BackColor = colorAwardOfBid;
			lblTotal.BackColor = colorAwardOfBid;
			panel2.BackColor = colorAwardOfBid;
		}
	}

	private void SetAllColumnsNotAllowEditing()
	{
		for (int i = 0; i < gridBudget.Cols.Count; i++)
		{
			gridBudget.Cols[i].AllowEditing = false;
		}
	}

	private bool IsOwnerBidProject()
	{
		return FormActionName == PccesFormAction.BID && currentDBName == companyDBName && dtProject.Rows[0]["sourceDatabase"] != DBNull.Value && dtProject.Rows[0]["sourceProjectCode"] != DBNull.Value;
	}

	private void initBudgetType()
	{
		budgetType = (BudgetType.Types)ArchConvert.Obj2Int(dtProject.Rows[0]["IsType"]);
		IsLocked = ArchConvert.Obj2Bool(dtProject.Rows[0]["IsCheckOut"]);
		if (dtProject.Columns.IndexOf("IsCheckOutCnt") > -1)
		{
			IsLockedCnt = ArchConvert.Obj2Bool(dtProject.Rows[0]["IsCheckOutCnt"]);
		}
		else
		{
			IsLockedCnt = false;
		}
		IsLockAnalys = ArchConvert.Obj2Bool(dtProject.Rows[0]["IsLockAn"]);
		if (FormActionName == PccesFormAction.BID && IsLockAnalys)
		{
			toolbarsManager.Tools["ImportSelectedMrsBaseCostBreakdown"].SharedProps.Enabled = false;
		}
		Archnowledge.Pcces.DomainModule.Coms.BudgetCtrl theBudgetCtrl = new Archnowledge.Pcces.DomainModule.Coms.BudgetCtrl();
		if (budgetType != BudgetType.Types.Execution && SysConfig.SysComsEnable)
		{
			try
			{
				if (theBudgetCtrl.IsProjectComsExecuteBudget(projectCode, SysConfig.SysComsDB))
				{
					budgetType = BudgetType.Types.Execution;
					dtProject.Rows[0]["IsType"] = (int)budgetType;
					BudProjectDBHelper theProjectHelper = new BudProjectDBHelper();
					theProjectHelper.UpdateBudProjectIsType(projectCode, budgetType);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("FormBudget::initBudgetType()#1 檢查專案狀態有誤,請檢查COMS的整合設定是否正確:" + ex.Message);
			}
		}
		if (theBudgetCtrl.IsProjectFirstUpload(projectCode))
		{
			theProject.InitParentSno(projectCode);
		}
	}

	private void InitChangeManagement()
	{
		if ((!SysConfig.SysChangeManagement && !SysConfig.SysEnableCostEstAndQuotation) || FormActionName != PccesFormAction.BUD)
		{
			return;
		}
		if (budgetType == BudgetType.Types.Contract || budgetType == BudgetType.Types.CostEstimation || budgetType == BudgetType.Types.CostQuotationMerged)
		{
			toolbarsManager.Tools["DeleteThisProject"].SharedProps.Visible = false;
			if (budgetType == BudgetType.Types.CostEstimation)
			{
				toolbarsManager.Tools["EditBudgetChangeResponsibility"].SharedProps.Visible = true;
				toolbarsManager.Tools["InsertMainItem"].SharedProps.Visible = false;
				((PopupMenuTool)toolbarsManager.Tools["RightClickMenu"]).Tools["EditMainItem"].InstanceProps.IsFirstInGroup = true;
				gridBudget.Cols["BudgetChangeReason"].Visible = true;
				gridBudget.Cols["VersionHistory"].Visible = true;
				toolbarsManager.Tools["ExportBudgetCostEstAndQuoteReport"].SharedProps.Visible = true;
				toolbarsManager.Tools["DeleteBudItemAZeroQtyItem"].SharedProps.Visible = false;
			}
			else if (budgetType == BudgetType.Types.CostQuotationMerged)
			{
				toolbarsManager.Tools["ViewSourceCostQuoteProject"].SharedProps.Visible = true;
				toolbarsManager.Tools["ExportBudgetDesingChangeReport"].SharedProps.Visible = true;
			}
			string budgetTypeName = BudgetType.GetBudgetTypeText((int)budgetType);
			BudProjectCodeMapping budProjectCodeMapping = new BudProjectCodeMapping();
			DataSet dsBudProjectCodeMapping = budProjectCodeMapping.GetBudProjectCodeMappingByProjectCode(projectCode);
			DataRow drBudProjectCodeMapping = dsBudProjectCodeMapping.Tables["BudProjectCodeMapping"].Rows[0];
			parentProjectCode = drBudProjectCodeMapping["parentProjectCode"].ToString().Trim();
			changeManagementCurrentVersion = ArchConvert.Obj2Int(drBudProjectCodeMapping["Version"]);
			lblProjectData.Text = $"【{parentProjectCode}】{projectName} - 第 {changeManagementCurrentVersion} 期{budgetTypeName}";
			gridBudget.Cols["qty"].Caption = "追加數量";
		}
		else
		{
			toolbarsManager.Tools["ExportExecutiveBudgetSummaryReport"].SharedProps.Visible = true;
			toolbarsManager.Tools["ExportExecutiveBudgetDetailReport"].SharedProps.Visible = true;
		}
	}

	private void DisableExeBudgetFunc()
	{
		if (SysConfig.SysComsEnable && budgetType == BudgetType.Types.Execution)
		{
			Archnowledge.Pcces.DomainModule.Coms.BudgetCtrl theBudgetCtrl = new Archnowledge.Pcces.DomainModule.Coms.BudgetCtrl();
			string[] buttonList = new string[6] { "TakeSnapshot", "RestoreSnapshot", "BackupProject", "RestoreProject", "DeleteThisProject", "CombineBudget" };
			SetButtonListAvailibility(buttonList, Enabled: false);
			F_NewAddItemFlag = "2";
			bool DisabledByComs = false;
			if (SysConfig.SysChangeManagement && budgetChangeCurrentVersion > 0)
			{
				DisabledByComs = true;
			}
			if (!DisabledByComs && SysConfig.SysEditAfterBudLem.ToUpper() == "DISABLE" && theBudgetCtrl.IsProjectAlreadySubPlan(projectCode, SysConfig.SysComsDB))
			{
				DisabledByComs = true;
			}
			if (DisabledByComs)
			{
				string[] buttonList2 = new string[12]
				{
					"AdjustTotalAmount", "MakeAmortizedItem", "LoadTemplate", "ImportMrsBaseItemName", "ClearDetailListCost", "ReconstructConnectionWithMrsBase", "ImportAllMrsBaseItemCost", "ImportAllMrsBaseCostBreakdown", "ImportSelectedMrsBaseItemCost", "ImportSelectedMrsBaseCostBreakdown",
					"SetPrecision", "COMSLoadBudgetFromContract"
				};
				SetButtonListAvailibility(buttonList2, Enabled: false);
			}
		}
	}

	private void InitBudgetChange()
	{
		toolbarsManager.Tools["SingleLockEdit"].SharedProps.Visible = false;
		gridBudget.Cols["VersionHistory"].Visible = false;
		if (budgetType == BudgetType.Types.CostEstimation)
		{
			toolbarsManager.Tools["COMSExpandBudget"].SharedProps.Enabled = false;
			toolbarsManager.Tools["COMSLoadBudgetFromContract"].SharedProps.Enabled = false;
		}
		if (budgetType == BudgetType.Types.CostQuotationMerged)
		{
			toolbarsManager.Tools["COMSCheckBudgetFromContract"].SharedProps.Enabled = true;
			toolbarsManager.Tools["COMSCheckBudgetFromContract"].SharedProps.Visible = true;
			toolbarsManager.Tools["COMSExpandBudget"].SharedProps.Enabled = false;
			toolbarsManager.Tools["COMSLoadBudgetFromContract"].SharedProps.Enabled = false;
		}
		if (FormActionName == PccesFormAction.BUD && SysConfig.SysChangeManagement && (budgetType == BudgetType.Types.Normal || budgetType == BudgetType.Types.Execution || budgetType == BudgetType.Types.Award))
		{
			toolbarsManager.Tools["BudgetChange"].SharedProps.Visible = true;
			BudExeProject budExeProject = new BudExeProject();
			DataSet dsBudExeProject = budExeProject.GetProject(projectCode);
			if (dsBudExeProject.Tables[0].Rows.Count == 0)
			{
				budExeProject.AddProject(projectCode, 0, string.Empty, null, null, null, "", "", "", "", GetItemAAmount(), "", "", userID, DateTime.Now, 0, 0, 0, 0, 0, 0, 0, 0);
			}
			budgetChangeCurrentVersion = budExeProject.GetCurrentVersion(projectCode);
			bool enable = budgetChangeCurrentVersion != 0;
			gridBudget.Cols["QtyBeforeChange"].Visible = enable;
			gridBudget.Cols["AmountBeforeChange"].Visible = enable;
			gridBudget.Cols["BudgetChangeReason"].Visible = enable;
			gridBudget.Cols["VersionHistory"].Visible = enable;
			gridBudget.Cols["BudgetChangeAddQty"].Visible = enable;
			bool COMSExpandBudget = budExeProject.GetCOMSExpandBudget(projectCode);
			lblProjectData.Text = "【" + projectCode + "】" + projectName;
			if (enable)
			{
				UltraLabel ultraLabel = lblProjectData;
				ultraLabel.Text = ultraLabel.Text + " - 第 " + budgetChangeCurrentVersion + " 期變更";
			}
			UltraLabel ultraLabel2 = lblProjectData;
			ultraLabel2.Text = ultraLabel2.Text + (IsLocked ? " (已鎖定) " : "") + (COMSExpandBudget ? " (已公告至COMS) " : "");
			toolbarsManager.Tools["ShowOnlyChangedItems"].SharedProps.Visible = enable;
			toolbarsManager.Tools["mnuHideAmtZero"].SharedProps.Visible = enable;
			toolbarsManager.Tools["ListItemChangeHistory"].SharedProps.Visible = enable;
			toolbarsManager.Tools["EditBudgetChangeResponsibility"].SharedProps.Visible = false;
			toolbarsManager.Tools["ImportAllMrsBaseItemCost"].SharedProps.Visible = !enable;
			toolbarsManager.Tools["ImportAllMrsBaseCostBreakdown"].SharedProps.Visible = !enable;
			toolbarsManager.Tools["DeleteBudgetChangeVersion"].SharedProps.Enabled = budgetChangeCurrentVersion > 0 && !IsLocked;
			toolbarsManager.Tools["EditBudgetChangeResponsibility"].SharedProps.Enabled = budgetChangeCurrentVersion > 0;
			toolbarsManager.Tools["ViewBudgetChangeInfo"].SharedProps.Enabled = budgetChangeCurrentVersion > 0;
			toolbarsManager.Tools["ViewBudgetChangeHistory"].SharedProps.Enabled = budgetChangeCurrentVersion > 0;
			string[] disabledList = new string[4] { "ImportFromMrsBase", "ImportSelectedMrsBaseItemCost", "ImportSelectedMrsBaseCostBreakdown", "ClearDetailListCost" };
			SetButtonListAvailibility(disabledList, !enable);
			if (COMSExpandBudget)
			{
				toolbarsManager.Tools["UnlockProject"].SharedProps.Enabled = false;
			}
			DisableExeBudgetFunc();
			string[] buttonList4 = new string[1] { "SingleLockEdit" };
			if (SysConfig.SysSingleEditLockMode && budgetChangeCurrentVersion > 0)
			{
				SetButtonListVisibility(buttonList4, Visible: true);
			}
			else
			{
				SetButtonListVisibility(buttonList4, Visible: false);
			}
		}
		if (FormActionName == PccesFormAction.BUD && SysConfig.SysComsEnable && budgetType == BudgetType.Types.Execution && SysConfig.SysComsDB.Trim() != "" && (SysConfig.SysIsCheckAccQtyAmt.ToUpper() == "DISABLE" || SysConfig.SysIsCheckAccQtyAmt.ToUpper() == "WARNONLY"))
		{
			toolbarsManager.Tools["ExportComsAccAlertReport"].SharedProps.Visible = true;
			toolbarsManager.Tools["ExportBudgetAccDiffReport"].SharedProps.Visible = true;
		}
		else
		{
			toolbarsManager.Tools["ExportComsAccAlertReport"].SharedProps.Visible = false;
			toolbarsManager.Tools["ExportBudgetAccDiffReport"].SharedProps.Visible = false;
		}
	}

	private void SingleLockEdit()
	{
		BudProject theBudProject = new BudProject();
		string CurrentEditInfo = string.Empty;
		if (ReadOnlyMode)
		{
			CurrentEditInfo = theBudProject.GetBudCurrentEditor(projectCode);
			if (CurrentEditInfo == string.Empty)
			{
				if (theBudProject.AcquireBudProjSingleEdit(projectCode, userID))
				{
					ReadOnlyMode = false;
					SetProjReadOnly(ReadOnlyMode);
					MessageBox.Show("已取得鎖定編輯專案的權限");
					InitBudgetChange();
					LoadProjectData();
				}
				else
				{
					CurrentEditInfo = theBudProject.GetBudCurrentEditor(projectCode);
					MessageBox.Show("此專案目前被鎖定編輯中:" + CurrentEditInfo + "\n無法取得編輯權限");
				}
			}
			else
			{
				MessageBox.Show("此專案目前被鎖定編輯中:" + CurrentEditInfo + "\n無法取得編輯權限");
			}
		}
		else if (theBudProject.GiveBackBudProjSingleEdit(projectCode, userID))
		{
			ReadOnlyMode = true;
			SetProjReadOnly(ReadOnlyMode);
			MessageBox.Show("已交還專案鎖定編輯權限");
		}
		else
		{
			CurrentEditInfo = theBudProject.GetBudCurrentEditor(projectCode);
			if (!CurrentEditInfo.Contains(userID))
			{
				ReadOnlyMode = true;
				SetProjReadOnly(ReadOnlyMode);
				MessageBox.Show("已交還專案鎖定編輯權限");
			}
			else
			{
				MessageBox.Show("返還專案鎖定編輯權限失敗");
			}
		}
	}

	private void LoadGridProperty()
	{
		if (FormActionName == PccesFormAction.BUD)
		{
			GridPropertySetting.LoadGridProperty(userID, "FormBudget.BUD", gridBudget);
		}
		else
		{
			GridPropertySetting.LoadGridProperty(userID, "FormBudget.BID", gridBudget);
		}
	}

	private void InitItemNoManager()
	{
		theItemNoSettingManager = new ItemNoSettingManager(projectCode);
		theItemNoSettingManager.PrepareAssemItemNo();
	}

	private void LoadProjectData()
	{
		AddOnDownLoad addOnDownLoad = new AddOnDownLoad();
		ExecResult ER = addOnDownLoad.SyncFromHardDrive(userID, projectCode);
		if (ER.ReturnCode != 0)
		{
			MessageBox.Show(ER.Message);
		}
		theProject.InitParentSno(projectCode);
		dsItemA = theItemA.GetItemA(projectCode, 0);
		ParentItemA = theItemA.GetItemAByProjectCode(projectCode);
		dtItemA = dsItemA.Tables[0];
		if (FormActionName == PccesFormAction.BUD || FormActionName == PccesFormAction.BID)
		{
			DataSet dsItemB = theItemB.GetItemB(projectCode, 0);
			for (int i = 0; i < dsItemB.Tables[0].Rows.Count; i++)
			{
				if (dsItemB.Tables[0].Rows[i]["itemCode"].ToString().IndexOf("VAR") <= -1 || (dsItemB.Tables[0].Rows[i]["parentCodeSno"] != DBNull.Value && !(dsItemB.Tables[0].Rows[i]["parentCodeSno"].ToString() == "")))
				{
					continue;
				}
				DataRow[] DR_itemA = dsItemA.Tables[0].Select("printNo='" + dsItemB.Tables[0].Rows[i]["parentCode"].ToString() + "'");
				if (DR_itemA.Length > 0)
				{
					theItemB.UpdateParentCodeSno(projectCode, dsItemB.Tables[0].Rows[i]["parentCode"].ToString(), dsItemB.Tables[0].Rows[i]["itemCode"].ToString(), DR_itemA[0]["SNo"].ToString());
					if (FormActionName == PccesFormAction.BUD)
					{
						string text = sItemB_Err;
						sItemB_Err = text + "項目：【" + DR_itemA[0]["itemNo"].ToString() + "\t" + DR_itemA[0]["CName"].ToString() + "】 的『加總項目』設定有問題，請重新檢查!";
					}
					else
					{
						string text = sItemB_Err;
						sItemB_Err = text + "項目：【" + DR_itemA[0]["itemNo"].ToString() + "\t" + DR_itemA[0]["CName"].ToString() + "】 的『加總項目』設定有問題，請洽原設計單位!";
					}
				}
			}
		}
		if (FormActionName == PccesFormAction.BID)
		{
			DBClass DBCLS = new DBClass();
			try
			{
				DBCLS._FS_UserID = userID;
				string sCount = DBCLS.GetUserDefine_String("select COUNT(*) as iCount from bidItemB Where (parentCodeSno is null and itemCodeSno is null)  and RTrim(projectCode) = '" + projectCode + "' ", "iCount");
				if (sCount != "0")
				{
					string sSQL = "Update bidItemB  Set parentCodeSno = (Select sNo From bidItemA Where RTrim(projectCode)='" + projectCode + "' And printNo=bidItemB.parentCode),        itemCodeSno = (Select sNo From bidItemA Where RTrim(projectCode)='" + projectCode + "' And printNo=bidItemB.itemCode)   Where projectCode = '" + projectCode + "'";
					DBCLS.ExecuteCommand(sSQL);
				}
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudget.cs--LoadProjectData().檢查BidItemB" + ex.Message);
			}
			DBCLS = null;
		}
		if (FormActionName == PccesFormAction.BID)
		{
			DBClass DBCLS = new DBClass();
			try
			{
				DBCLS._FS_UserID = userID;
				string sCount = DBCLS.GetUserDefine_String("select COUNT(*) as iCount from bidItemC Where sNo is null and RTrim(projectCode) = '" + projectCode + "' ", "iCount");
				if (sCount != "0")
				{
					string sSQL = "update bidItemC  set sNo = (Select sNo from bidItemA Where RTrim(projectCode)='" + projectCode + "' And printNo = bidItemC.printNo)  Where projectCode = '" + projectCode + "'";
					DBCLS.ExecuteCommand(sSQL);
				}
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudget.cs--LoadProjectData().檢查BidItemC" + ex.Message);
			}
			DBCLS = null;
		}
		if (FormActionName == PccesFormAction.BUD)
		{
			DBClass DBCLS = new DBClass();
			try
			{
				DBCLS._FS_UserID = userID;
				DBCLS.ExecuteCommand("Delete BudPageBreak Where projectCode='" + projectCode + "' And  Sno not in (Select sno from buditema where projectCode='" + projectCode + "')");
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudget.cs--LoadProjectData().檢查BudPageBreak" + ex.Message);
			}
			DBCLS = null;
		}
		SetStatusBarItemCount(dtItemA.Rows.Count);
		Data2Grid();
		Cursor = Cursors.Default;
	}

	private void CreateClipboardDataTable()
	{
		dtClipboard.Columns.Add("ProjectCode", Type.GetType("System.String"));
		dtClipboard.Columns.Add("ItemNo", Type.GetType("System.String"));
		dtClipboard.Columns.Add("CName", Type.GetType("System.String"));
		dtClipboard.Columns.Add("UnitName", Type.GetType("System.String"));
		dtClipboard.Columns.Add("Qty", Type.GetType("System.Decimal"));
		dtClipboard.Columns.Add("LockCost", Type.GetType("System.Boolean"));
		dtClipboard.Columns.Add("Cost", Type.GetType("System.Decimal"));
		dtClipboard.Columns.Add("Amount", Type.GetType("System.Decimal"));
		dtClipboard.Columns.Add("PccesCode", Type.GetType("System.String"));
		dtClipboard.Columns.Add("Memo", Type.GetType("System.String"));
		dtClipboard.Columns.Add("EName", Type.GetType("System.String"));
		dtClipboard.Columns.Add("EUnit", Type.GetType("System.String"));
		dtClipboard.Columns.Add("Level", Type.GetType("System.Int32"));
		dtClipboard.Columns.Add("Kind", Type.GetType("System.String"));
		dtClipboard.Columns.Add("Analysis", Type.GetType("System.String"));
		dtClipboard.Columns.Add("SNo", Type.GetType("System.Int32"));
		dtClipboard.Columns.Add("Formula", Type.GetType("System.String"));
		dtClipboard.Columns.Add("PrintNo", Type.GetType("System.String"));
		dtClipboard.Columns.Add("OldPrintNo", Type.GetType("System.String"));
		dtClipboard.Columns.Add("PubCode", Type.GetType("System.Int32"));
		dtClipboard.Columns.Add("IsShared", Type.GetType("System.String"));
		dtClipboard.Columns.Add("IsCollaspse", Type.GetType("System.String"));
		dtClipboard.Columns.Add("DBName", Type.GetType("System.String"));
		dtClipboard.Columns.Add("surName", Type.GetType("System.String"));
		dtClipboard.Columns.Add("fixPrice", Type.GetType("System.String"));
		dtClipboard.Columns.Add("Account", Type.GetType("System.String"));
		dtClipboard.Columns.Add("PwrSet", Type.GetType("System.String"));
		dtClipboard.Columns.Add("Lock", Type.GetType("System.Boolean"));
	}

	private void InitToolbarStatus()
	{
		if (FormActionName == PccesFormAction.BUD)
		{
			toolbarsManager.Tools["CombineBid"].SharedProps.Visible = false;
			bool IsSubBudget = sourceProjectCode.Trim() != string.Empty;
			toolbarsManager.Tools["PickItemFromMainProject"].SharedProps.Visible = IsSubBudget;
			toolbarsManager.Tools["CombineBudget"].SharedProps.Visible = !IsSubBudget;
			toolbarsManager.Tools["EditCustomizedVariable"].SharedProps.Visible = true;
			toolbarsManager.Tools["ExportTaiwanRailwayCustomizedReport"].SharedProps.Visible = PubTools.GetAppSet_String("PID") == "Z14AC1100";
			toolbarsManager.Toolbars["COMS"].Visible = EnableCOMS;
			toolbarsManager.Toolbars["ItemAction"].Visible = EnableCOMS;
			toolbarsManager.Tools["GetSubItemQtyAmt"].SharedProps.Visible = EnableCOMS;
			if (budgetType == BudgetType.Types.CostEstimation)
			{
				string[] buttonList = new string[5] { "ImportFromMrsBase", "AdjustTotalAmount", "ImportMrsBaseItemCost", "ImportMrsBaseCostBreakdown", "ClearDetailListCost" };
				SetButtonListVisibility(buttonList, Visible: false);
			}
			toolbarsManager.Tools["mnuSelfExam"].SharedProps.Visible = true;
		}
		else if (FormActionName == PccesFormAction.BID)
		{
			toolbarsManager.Toolbars["COMS"].Visible = false;
			toolbarsManager.Toolbars["ItemAction"].Visible = false;
			ultraButton2.Text = "標單資訊";
			toolbarsManager.Tools["EditProjectInfo"].SharedProps.Caption = "標單資訊";
			toolbarsManager.Tools["Exit"].SharedProps.Caption = "結束標單編輯(&X)...";
			toolbarsManager.Tools["DeleteThisProject"].SharedProps.Caption = "刪除此投標單...";
			toolbarsManager.Tools["TakeSnapshot"].SharedProps.Caption = "儲存標單版本";
			toolbarsManager.Tools["RestoreSnapshot"].SharedProps.Caption = "回存舊版標單";
			BtnDownloadDoc.Visible = false;
			if (IsSubmitBid && !IsAwardOfBid)
			{
				string[] buttonList = new string[8] { "Recalculate", "AutoRecalculate", "AdjustTotalAmount", "EditMainItem", "MakeAmortizedItem", "BackupProject", "RestoreProject", "TakeSnapshot" };
				SetButtonListVisibility(buttonList, Visible: false);
			}
			else
			{
				string[] buttonList = new string[8] { "Recalculate", "AutoRecalculate", "AdjustTotalAmount", "EditMainItem", "MakeAmortizedItem", "BackupProject", "RestoreProject", "TakeSnapshot" };
				SetButtonListVisibility(buttonList, Visible: true);
				string[] buttonList2 = new string[3] { "TakeSnapshotCnt", "RestoreSnapshotCnt", "TakeSnapshotCntFromBid" };
				SetButtonListVisibility(buttonList2, Visible: false);
			}
			toolbarsManager.Tools["ImportMrsBaseCostBreakdown"].SharedProps.Enabled = !IsLockAnalys;
			toolbarsManager.Tools["ChangeToCompanyCode"].SharedProps.Visible = IsOwnerBidProject();
			toolbarsManager.Tools["LockProject"].SharedProps.Visible = false;
			toolbarsManager.Tools["UnLockProject"].SharedProps.Visible = false;
			toolbarsManager.Tools["mnuSelfExam"].SharedProps.Visible = false;
			SetOnlyCostAllowEditing();
		}
		if (FormActionName == PccesFormAction.BUD && GetCurrentBDGT_Type() == "CNT")
		{
			if (dtProject.Rows[0]["IsCheckOutCnt"].ToString().Trim() == "Y" || ReadOnlyMode)
			{
				LockOrUnlockToolbar(Locked: true);
			}
			else
			{
				toolbarsManager.Tools["UnlockProject"].SharedProps.Enabled = false;
				EnableContextMenu = true;
			}
			toolbarsManager.Tools["PreviewReport"].SharedProps.Enabled = false;
			toolbarsManager.Tools["BackupProject"].SharedProps.Visible = false;
			toolbarsManager.Tools["RestoreProject"].SharedProps.Visible = false;
		}
		else
		{
			if (dtProject.Rows[0]["IsCheckOut"].ToString().Trim() == "Y" || ReadOnlyMode)
			{
				LockOrUnlockToolbar(Locked: true);
			}
			else
			{
				toolbarsManager.Tools["UnlockProject"].SharedProps.Enabled = false;
				EnableContextMenu = true;
			}
			toolbarsManager.Tools["PreviewReport"].SharedProps.Enabled = true;
			toolbarsManager.Tools["BackupProject"].SharedProps.Visible = true;
			toolbarsManager.Tools["RestoreProject"].SharedProps.Visible = true;
		}
		(toolbarsManager.Tools["HideAliasColumn"] as StateButtonTool).Checked = true;
		gridBudget.Cols["surName"].Visible = false;
	}

	private void SetOnlyCostAllowEditing()
	{
		string[] buttonList = new string[31]
		{
			"EditCustomizedVariable", "EditMenu", "Cut", "Paste", "Copy", "EditBidSetting", "Outdent", "Indent", "MoveUp", "MoveDown",
			"InsertWorkItem", "InsertMainItem", "SetPrecision", "ReArrangeItemNo", "EditItemNoSetting", "Delete", "ImportMrsBaseItemName", "CombineBudget", "AutoInsertSubtotalItem", "EditAliasSettingForReport",
			"COMSLoadBudgetFromContract", "COMSExpandBudget", "PickItemFromMainProject", "ImportMrsBaseCostBreakdown", "ImportMrsBaseItemCost", "LoadTemplate", "ImportQtyFrom3rdPartyTool", "PreviewReport", "ExportTaiwanRailwayCustomizedReport", "GetSubItemQtyAmt",
			"CloneWorkItem"
		};
		SetButtonListVisibility(buttonList, Visible: false);
		SetAllColumnsNotAllowEditing();
		gridBudget.Cols["Cost"].AllowEditing = !IsSubmitBid;
		gridBudget.Cols["LockCost"].AllowEditing = !IsSubmitBid;
	}

	private void SetupAddonToolBar()
	{
		string AppLocation = AppDomain.CurrentDomain.BaseDirectory;
		string FileINI = AppLocation + "Addon.ini";
		ToolLists.Clear();
		ToolParam.Clear();
		string sValue = "";
		sValue = CommonMethods.IniReadValue(FileINI, "ENABLEDATAEXPORT", "ENABLE");
		if (sValue.ToLower() == "true")
		{
			toolbarsManager.Tools["ExportDataToServer"].SharedProps.Visible = true;
		}
		else
		{
			toolbarsManager.Tools["ExportDataToServer"].SharedProps.Visible = false;
		}
		for (int i = 1; i <= 20; i++)
		{
			if (FormActionName == PccesFormAction.BUD)
			{
				sValue = CommonMethods.IniReadValue(FileINI, "BUDGET", "TOOL" + i);
			}
			else if (FormActionName == PccesFormAction.BID)
			{
				sValue = CommonMethods.IniReadValue(FileINI, "BID", "TOOL" + i);
			}
			if (sValue.Trim() != "")
			{
				ToolLists.Add(sValue.Substring(0, sValue.IndexOf(",")));
				ToolParam.Add(sValue.Substring(sValue.IndexOf(",") + 1));
			}
		}
		if (ToolLists.Count > 0)
		{
			PopupMenuTool Addon = (PopupMenuTool)toolbarsManager.Tools["AddOn"];
			for (int i = Addon.Tools.Count - 1; i >= 0; i--)
			{
				Addon.Tools.Remove(Addon.Tools[i]);
			}
			toolbarsManager.Tools["AddOn"].SharedProps.Visible = true;
			toolbarsManager.Tools["AddOn"].SharedProps.Enabled = true;
			for (int i = 0; i < ToolLists.Count; i++)
			{
				ButtonTool BT = new ButtonTool(ToolLists[i].ToString());
				BT.SharedProps.Tag = i;
				BT.SharedProps.Caption = ToolLists[i].ToString();
				BT.ToolClick += AddOnClick;
				try
				{
					toolbarsManager.Tools.Remove(BT);
				}
				catch (Exception ex)
				{
					CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudget.cs--Execute_ReDBforIR" + ex.Message);
				}
				toolbarsManager.Tools.Add(BT);
				Addon.Tools.AddTool(ToolLists[i].ToString());
			}
		}
		else
		{
			toolbarsManager.Tools["AddOn"].SharedProps.Visible = false;
			toolbarsManager.Tools["AddOn"].SharedProps.Enabled = false;
		}
	}

	private void InitRestoreSnapshot()
	{
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(userID);
		aArr.Add("取出tmp專案" + projectCode);
		Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
		PROJ.ps_projectCode = projectCode;
		PROJ.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
		PROJ.DeleteItemTmp(projectCode);
	}

	private void SetupRestoreSnapshotList()
	{
		if (SysConfig.SysComsEnable && budgetType == BudgetType.Types.Execution)
		{
			return;
		}
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(userID);
		aArr.Add("取出tmp專案" + projectCode);
		Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
		PROJ.ps_projectCode = projectCode;
		PROJ.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
		DataTable dt = PROJ.ListItemTmp();
		if (dt.Rows.Count > 0)
		{
			PopupMenuTool ReStore = (PopupMenuTool)toolbarsManager.Tools["RestoreSnapshot"];
			if (FormActionName == PccesFormAction.BID)
			{
				ReStore = (PopupMenuTool)toolbarsManager.Tools["popupRestoreDbgt"];
			}
			for (int i = ReStore.Tools.Count - 1; i >= 0; i--)
			{
				ReStore.Tools.Remove(ReStore.Tools[i]);
			}
			toolbarsManager.Tools["RestoreSnapshot"].SharedProps.Visible = true;
			toolbarsManager.Tools["RestoreSnapshot"].SharedProps.Enabled = true;
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				ButtonTool BT = new ButtonTool(dt.Rows[i]["version"].ToString());
				BT.SharedProps.Tag = PubTools.Str2Int(dt.Rows[i]["version"].ToString());
				BT.SharedProps.Caption = "第 " + dt.Rows[i]["version"].ToString() + " 版 " + dt.Rows[i]["NewDate"].ToString() + "  " + dt.Rows[i]["memo"].ToString();
				BT.ToolClick += ReStoreClick;
				try
				{
					toolbarsManager.Tools.Remove(BT);
				}
				catch (Exception ex)
				{
					CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudget.cs--ProcessReStore" + ex.Message);
				}
				toolbarsManager.Tools.Add(BT);
				ReStore.Tools.AddTool(dt.Rows[i]["version"].ToString());
			}
		}
		else
		{
			toolbarsManager.Tools["RestoreSnapshot"].SharedProps.Visible = false;
			toolbarsManager.Tools["RestoreSnapshot"].SharedProps.Enabled = false;
		}
		aArr = null;
		PROJ = null;
		dt = null;
	}

	private void SetupRestoreSnapshotListCNT()
	{
		if (SysConfig.SysComsEnable && budgetType == BudgetType.Types.Execution)
		{
			return;
		}
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(userID);
		aArr.Add("取出tmp專案" + projectCode);
		Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
		PROJ.ps_projectCode = projectCode;
		PROJ.ps_srckind = "CNT";
		DataTable dt = PROJ.ListItemTmp();
		if (dt.Rows.Count > 0)
		{
			PopupMenuTool ReStore = (PopupMenuTool)toolbarsManager.Tools["RestoreSnapshotCNT"];
			for (int i = ReStore.Tools.Count - 1; i >= 0; i--)
			{
				ReStore.Tools.Remove(ReStore.Tools[i]);
			}
			toolbarsManager.Tools["RestoreSnapshotCNT"].SharedProps.Visible = true;
			toolbarsManager.Tools["RestoreSnapshotCNT"].SharedProps.Enabled = true;
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				int iVer = PubTools.Str2Int(dt.Rows[i]["version"].ToString()) - 50000;
				ButtonTool BT = new ButtonTool(iVer.ToString());
				BT.SharedProps.AppearancesSmall.Appearance.ForeColor = Color.Purple;
				BT.SharedProps.Tag = iVer + 50000;
				BT.SharedProps.Caption = "第 " + iVer + " 版 " + dt.Rows[i]["NewDate"].ToString() + "  " + dt.Rows[i]["memo"].ToString();
				BT.ToolClick += ReStoreClickCNT;
				try
				{
					toolbarsManager.Tools.Remove(BT);
				}
				catch (Exception ex)
				{
					CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudget.cs--ProcessReStore" + ex.Message);
				}
				toolbarsManager.Tools.Add(BT);
				ReStore.Tools.AddTool(iVer.ToString());
			}
		}
		else
		{
			toolbarsManager.Tools["RestoreSnapshotCNT"].SharedProps.Visible = false;
			toolbarsManager.Tools["RestoreSnapshotCNT"].SharedProps.Enabled = false;
		}
		aArr = null;
		PROJ = null;
		dt = null;
	}

	private void LoadBookmark()
	{
		((ComboBoxTool)toolbarsManager.Tools["BookmarkList"]).ValueList.ValueListItems.Clear();
		string sSrcKind = CommonMethods.GetActionNameString(FormActionName);
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = userID;
		DataTable DTBookmarks = DBCLS.GetUserDefine("Select * From Bookmarks Where ProjectCode='" + projectCode + "' And SrcKind='" + sSrcKind + "' Order By Code");
		string sSNo = "";
		string sCName = "";
		string sItemNo = "";
		string sBookMark = "";
		for (int i = 0; i < DTBookmarks.Rows.Count; i++)
		{
			sSNo = DTBookmarks.Rows[i]["Code"].ToString();
			sItemNo = DTBookmarks.Rows[i]["ItemNo"].ToString();
			sCName = DTBookmarks.Rows[i]["CName"].ToString();
			sBookMark = sSNo + ":" + sItemNo + "\u3000" + sCName;
			((ComboBoxTool)toolbarsManager.Tools["BookmarkList"]).ValueList.ValueListItems.Add(sBookMark);
		}
		DBCLS = null;
	}

	private void Th_ReCal_All(bool Auto)
	{
		IsAuto = Auto;
		Do_ReCal_All();
		Cursor = Cursors.Default;
	}

	private void BtnSwitchProject_Click(object sender, EventArgs e)
	{
		Execute_SwitchProject();
	}

	public bool DoClose()
	{
		Activate();
		DialogResult result = MessageBox.Show(this, "確定要結束【預算書編製】？", "PCCES Win 4.3 ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
		if (result != DialogResult.Yes)
		{
			return false;
		}
		if (SysConfig.SysSingleEditLockMode && budgetChangeCurrentVersion > 0 && !ReadOnlyMode)
		{
			BudProject theBudProject = new BudProject();
			theBudProject.GiveBackBudProjSingleEdit(projectCode, userID);
		}
		if (base.ParentForm.MdiChildren.Length == 1)
		{
			(base.ParentForm as frmPccesMain).functionButtons1.Width = 160;
		}
		onlineList1.Disconnect();
		return true;
	}

	private void frmBudget_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (AdjustmentFlag == "")
		{
			try
			{
				if (dtItemA.Columns.IndexOf("cName") >= 0)
				{
					DataView dvItemA = new DataView(dtItemA);
					dvItemA.RowFilter = "cName ='調價後差額'";
					if (dvItemA.Count > 0)
					{
						if (MessageBox.Show(this, "因總價調整後，產生一筆【調價後差額】項，你尚未處理\n是否確定要結束編輯?", "警示", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
						{
							(base.ParentForm as frmPccesMain)._FORM_STATUS = "BDGT_DONT_CLOSE";
							e.Cancel = true;
							AdjustmentFlag = "";
						}
						else
						{
							GC.Collect();
							AdjustmentFlag = "Leave";
							(base.ParentForm as frmPccesMain)._FORM_STATUS = "CLOSE";
							(base.ParentForm as frmPccesMain).LeftPanel.Width = 160;
						}
					}
					dvItemA.Dispose();
					dvItemA = null;
					SaveBookmarkToDB();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, "Err1:\nFormBudget::frmBudget_FormClosing()#1 " + ex.Message, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		onlineList1.Disconnect();
		try
		{
			if (FormActionName == PccesFormAction.BUD)
			{
				GridPropertySetting.SaveGridProperty(userID, "FormBudget.BUD", gridBudget);
			}
			else
			{
				GridPropertySetting.SaveGridProperty(userID, "FormBudget.BID", gridBudget);
			}
			DBClass DBCLS = new DBClass();
			DBCLS._FS_UserID = userID;
			DBCLS.MrsBase_UnLockAll(projectCode, CommonMethods.GetActionNameString(FormActionName));
			DBCLS.ItemA_UnLockAll(projectCode, CommonMethods.GetActionNameString(FormActionName));
			DBCLS = null;
			dbMrsBase = null;
		}
		catch (Exception ex)
		{
			MessageBox.Show(this, "Err2:\nFormBudget::frmBudget_FormClosing()#2 " + ex.Message, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		if (!IsSubmitBid && !F_IsHasConfirmReCal)
		{
			bool IsReCal = true;
			ArrayList aArr = new ArrayList();
			aArr.Clear();
			aArr.Add(userID);
			aArr.Add("是否重新總計的旗標" + projectCode);
			DataTable DTEight = new DataTable();
			Archnowledge.Pcces.BUDClass.Project dbEight = new Archnowledge.Pcces.BUDClass.Project(aArr);
			dbEight.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
			DTEight = dbEight.ListItem_eight("", projectCode);
			if (DTEight.Rows.Count > 0 && DTEight.Rows[0]["IsReCal"].ToString() == "Y")
			{
				IsReCal = false;
			}
			string sFind9999 = "";
			for (int i = 1; i < gridBudget.Rows.Count; i++)
			{
				if (gridBudget[i, "PrintNo"] != null && gridBudget[i, "PrintNo"].ToString().Trim() == "99999999999999999999999999999999")
				{
					sFind9999 = "found";
					break;
				}
			}
			if (!IsReCal)
			{
				if (sFind9999 == "" || IsLocked)
				{
					return;
				}
				if (iCount == 1)
				{
					iCount++;
					DialogResult Result = MessageBox.Show(this, "資料有異動過是否要重新總計", "警示", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
					if (Result == DialogResult.Yes)
					{
						Do_ReCal_All();
					}
				}
			}
		}
		CommonMethods.WriteIniValue("BidSet", "State", "");
		CommonMethods.WriteIniValue("BidSet", "StateAdd", "");
		if (backgroundWorker.IsBusy)
		{
			backgroundWorker.CancelAsync();
		}
		ReleaseSingleEditCtrlLock();
	}

	private void ReleaseSingleEditCtrlLock()
	{
		if (SysConfig.SysSingleEditLockMode && budgetChangeCurrentVersion > 0 && !ReadOnlyMode)
		{
			BudProject theBudProject = new BudProject();
			theBudProject.GiveBackBudProjSingleEdit(projectCode, userID);
		}
	}

	private void ReleaseSingleEditCtrlLock(string SwitchProjectCode)
	{
		if (SysConfig.SysSingleEditLockMode && budgetChangeCurrentVersion > 0 && !ReadOnlyMode)
		{
			BudProject theBudProject = new BudProject();
			theBudProject.GiveBackBudProjSingleEdit(SwitchProjectCode, userID);
			ReadOnlyMode = true;
		}
	}

	private bool HasRegistered()
	{
		return (CommonMethods.GetIniValue("Register", "RegID").Trim() != "") ? true : false;
	}

	private void gridBudget1_MouseDown(object sender, MouseEventArgs e)
	{
		Archnowledge.Common.DebugUtil.OutputDebugString("gridBudget1_MouseDown");
		string ColumnName = gridBudget.Cols[gridBudget.MouseCol].Name;
		if (IsLocked && ColumnName != "AnaImg" && e.Button == MouseButtons.Right)
		{
			gridBudget.Col = 0;
			EnableContextMenu = false;
		}
		if (gridBudget.Row <= 0 || gridBudget.MouseRow <= 0 || gridBudget.MouseCol <= 0 || IsLocked)
		{
			return;
		}
		int rowIndex = gridBudget.MouseRow;
		int colIndex = gridBudget.MouseCol;
		if (gridBudget.SelectedRowCount > 1 && gridBudget.Cols[colIndex].Name != "ItemNo")
		{
			return;
		}
		if (e.Button == MouseButtons.Right)
		{
			gridBudget.Row = rowIndex;
			if (gridBudget[gridBudget.Row, "Kind"] != null && gridBudget[gridBudget.Row, "Kind"].ToString().ToUpper() == "W" && !(bool)gridBudget[gridBudget.Row, "Analysis"])
			{
				toolbarsManager.Tools["PickCostFromHistoryPrice"].SharedProps.Enabled = true;
				dbMrsBase.ps_srckind = "MRS";
				dbMrsBase.ps_projectcode = "";
				DataTable DT_Temp = dbMrsBase.List_Cost(gridBudget[rowIndex, "PccesCode"].ToString());
				DataRow DR = DT_Temp.NewRow();
				DR["Cost"] = ((gridBudget[rowIndex, "Cost"] != null) ? gridBudget[rowIndex, "Cost"] : ((object)0));
				DR["Kind"] = "Org";
				DR["Area"] = "挑選前原單價";
				DR["Memo"] = "在下拉挑選單價前，原本的單價";
				DT_Temp.Rows.Add(DR);
				cboHisPrice.DataSource = DT_Temp;
				cboHisPrice.DataBind();
				cboHisPrice.DisplayLayout.Bands[0].Override.HeaderClickAction = HeaderClickAction.SortSingle;
				cboHisPrice.DisplayLayout.Bands[0].Columns[0].Header.Caption = "單價";
				cboHisPrice.DisplayLayout.Bands[0].Columns[1].Header.Caption = "KIND";
				cboHisPrice.DisplayLayout.Bands[0].Columns[1].Hidden = true;
				cboHisPrice.DisplayLayout.Bands[0].Columns[2].Header.Caption = "來源";
				cboHisPrice.DisplayLayout.Bands[0].Columns[3].Header.Caption = "說明";
				cboHisPrice.DisplayLayout.Bands[0].Columns[4].Hidden = true;
				cboHisPrice.DisplayLayout.Bands[0].Columns[5].Header.Caption = "工項編碼";
				cboHisPrice.DisplayLayout.Bands[0].Columns[6].Header.Caption = "工項名稱";
				cboHisPrice.DisplayLayout.Bands[0].Columns[0].Format = "N2";
				cboHisPrice.DisplayLayout.Bands[0].Columns[0].Width = 80;
				cboHisPrice.DisplayLayout.Bands[0].Columns[2].Width = 120;
				cboHisPrice.DisplayLayout.Bands[0].Columns[3].Width = 200;
				cboHisPrice.DisplayLayout.Bands[0].Columns[5].Width = 80;
				cboHisPrice.DisplayLayout.Bands[0].Columns[6].Width = 310;
				DT_Temp = null;
			}
			else
			{
				toolbarsManager.Tools["PickCostFromHistoryPrice"].SharedProps.Enabled = false;
			}
			string pccesCode = ((gridBudget[gridBudget.Row, "pccesCode"] == null || gridBudget[gridBudget.Row, "pccesCode"] == DBNull.Value) ? string.Empty : gridBudget[gridBudget.Row, "pccesCode"].ToString().Trim());
			toolbarsManager.Tools["GetSubItemQtyAmt"].SharedProps.Enabled = EnableCOMS && FormActionName == PccesFormAction.BUD && pccesCode != string.Empty;
			bool isBudgetWorkItem = FormActionName == PccesFormAction.BUD && ArchConvert.Obj2String(gridBudget[gridBudget.Row, "kind"]) == "W";
			toolbarsManager.Tools["ListItemChangeHistory"].SharedProps.Enabled = isBudgetWorkItem;
			toolbarsManager.Tools["EditBudgetChangeResponsibility"].SharedProps.Enabled = isBudgetWorkItem;
		}
		if (FormActionName == PccesFormAction.BUD && !showOnlyChangedItem && budgetType != BudgetType.Types.CostEstimation && budgetType != BudgetType.Types.CostQuotationMerged)
		{
			DragInfo.checkDrag = false;
			if (e.Button == MouseButtons.Left && gridBudget.MouseRow >= gridBudget.Rows.Fixed && gridBudget.MouseCol == 1 && gridBudget[rowIndex, "PrintNo"] != null && !(gridBudget[rowIndex, "PrintNo"].ToString().Trim() == "99999999999999999999999999999999"))
			{
				DragInfo.row = gridBudget.Row;
				DragInfo.mouseDown = new Point(e.X, e.Y);
				DragInfo.checkDrag = true;
				string Kind = ArchConvert.Obj2String(gridBudget[gridBudget.Row, "Kind"]).Trim();
				DragInfo.allowChange = !ArchConvert.Obj2Bool(gridBudget[gridBudget.Row, "Lock"]) && (Kind == "B" || AllowChangeBySNo(gridBudget[gridBudget.Row, "sNo"], silentOnWarning: true, silentOnModify: true));
			}
		}
	}

	private void gridBudget1_MouseMove(object sender, MouseEventArgs e)
	{
		if (IsLocked)
		{
			return;
		}
		int RowIndex = gridBudget.MouseRow;
		int ColIndex = gridBudget.MouseCol;
		if (RowIndex <= 0 || ColIndex <= 0)
		{
			return;
		}
		Row GridRow = gridBudget.Rows[RowIndex];
		string ColumnName = gridBudget.Cols[ColIndex].Name;
		string PrintNo = "";
		string Kind = "";
		if (GridRow["PrintNo"] != null && GridRow["Kind"] != null)
		{
			PrintNo = GridRow["PrintNo"].ToString().Trim();
			Kind = GridRow["Kind"].ToString().Trim();
		}
		if (!DragInfo.dragging)
		{
			if (gridBudget.MouseRow <= 0 || gridBudget.MouseCol <= 0)
			{
				return;
			}
			Cursor = Cursors.Default;
			if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right && ColumnName == "AnaImg" && GridRow["Analysis"] != null && ArchConvert.Obj2Bool(GridRow["Analysis"]))
			{
				Cursor = Cursors.Hand;
			}
			if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right && ColumnName == "PccesCode" && GridRow["IsDown"] != null && ArchConvert.Obj2Bool(GridRow["IsDown"]))
			{
				Cursor = Cursors.Hand;
			}
		}
		if (!(PrintNo == "") && !(Kind == "") && !IsTemplate && FormActionName == PccesFormAction.BUD && DragInfo.checkDrag && e.Button == MouseButtons.Left && Math.Abs(e.X - DragInfo.mouseDown.X) + Math.Abs(e.Y - DragInfo.mouseDown.Y) > 5)
		{
			gridBudget.SelectionMode = SelectionModeEnum.Row;
			DragInfo.dragging = true;
			CellStyle cs = gridBudget.Styles["SourceNode"];
			gridBudget.Cursor = Cursors.NoMove2D;
			gridBudget.SetCellStyle(DragInfo.row, 1, cs);
			Cursor c = (NoDropHere() ? Cursors.No : Cursors.NoMove2D);
			if (c != gridBudget.Cursor)
			{
				gridBudget.Cursor = c;
			}
		}
	}

	private bool NoDropHere()
	{
		try
		{
			int MouseRow = gridBudget.MouseRow;
			int MouseCol = gridBudget.MouseCol;
			if (MouseRow < gridBudget.Rows.Fixed)
			{
				return true;
			}
			if (MouseCol < gridBudget.Cols.Fixed)
			{
				return true;
			}
			Row theTargetRow = gridBudget.Rows[MouseRow];
			Row theSourceRow = gridBudget.Rows[DragInfo.row];
			if (theTargetRow["PrintNo"] == null)
			{
				return true;
			}
			if (theSourceRow["PrintNo"] == null)
			{
				return true;
			}
			if (!CheckLevelLimit(DragInfo.row, MouseRow))
			{
				return true;
			}
			if (theSourceRow["CostUID"] != null && theSourceRow["CostUID"].ToString() != string.Empty)
			{
				return true;
			}
			if (theTargetRow["CostUID"] != null && theTargetRow["CostUID"].ToString() != string.Empty)
			{
				string Kind = ArchConvert.Obj2String(theSourceRow["Kind"]);
				if (Kind != "W")
				{
					return true;
				}
				Node theFirstChildNode = theTargetRow.Node.GetNode(NodeTypeEnum.FirstChild);
				if (theFirstChildNode != null && theFirstChildNode.Row["CostUID"] != null && theFirstChildNode.Row["CostUID"].ToString() != string.Empty)
				{
					return true;
				}
			}
			string DstKind = theTargetRow["Kind"].ToString();
			string sPrintNoSrc = theSourceRow["PrintNo"].ToString().Trim();
			string sPrintNoDst = theTargetRow["PrintNo"].ToString().Trim();
			sPrintNoSrc = sPrintNoSrc.Substring(0, sPrintNoSrc.Length - 4);
			sPrintNoDst = sPrintNoDst.Substring(0, sPrintNoDst.Length - 4);
			if (sPrintNoSrc != sPrintNoDst && (DstKind != "B" || !DragInfo.allowChange))
			{
				return true;
			}
			if (sPrintNoDst.Length - 4 == sPrintNoSrc.Length && sPrintNoSrc == sPrintNoDst.Substring(0, sPrintNoSrc.Length))
			{
				return true;
			}
		}
		catch (Exception ex)
		{
			Archnowledge.Common.DebugUtil.OutputDebugString("NoDropHere Error=" + ex.Message);
			return true;
		}
		return false;
	}

	private void gridBudget1_MouseUp(object sender, MouseEventArgs e)
	{
		Archnowledge.Common.DebugUtil.OutputDebugString("gridBudget1_MouseUp");
		gridBudget.SelectionMode = SelectionModeEnum.ListBox;
		if (IsLocked)
		{
			return;
		}
		DragInfo.checkDrag = false;
		InitialToolbars();
		if (!DragInfo.dragging)
		{
			return;
		}
		DragInfo.dragging = false;
		gridBudget.SetCellStyle(DragInfo.row, 1, null);
		gridBudget.Cursor = Cursors.Default;
		Archnowledge.Common.DebugUtil.OutputDebugString("gridBudget1_MouseUp DragInfo.row=" + DragInfo.row);
		int TargetRow = gridBudget.Row;
		if (!NoDropHere() && DragInfo.row != gridBudget.Row)
		{
			Node ndSrc = gridBudget.Rows[DragInfo.row].Node;
			Node ndDst = gridBudget.Rows[TargetRow].Node;
			Node ndSrcPre = null;
			if (DragInfo.row > 1)
			{
				ndSrcPre = gridBudget.Rows[DragInfo.row - 1].Node;
			}
			int iSrcParentRow = 0;
			int iDstParentRow = 0;
			if (ndSrc.GetNode(NodeTypeEnum.Parent) != null)
			{
				iSrcParentRow = ndSrc.GetNode(NodeTypeEnum.Parent).Row.Index;
			}
			int TargetSno;
			int TargetSortOrder;
			if (gridBudget[TargetRow, "Kind"].ToString().Trim() == "B")
			{
				TargetSno = ArchConvert.Obj2Int(gridBudget[TargetRow, "sNo"]);
				TargetSortOrder = 9999999;
			}
			else
			{
				TargetSno = ArchConvert.Obj2Int(gridBudget.Rows[TargetRow]["ParentSno"]);
				TargetSortOrder = ArchConvert.Obj2Int(gridBudget.Rows[TargetRow]["SortOrder"]);
			}
			if (ndDst.GetNode(NodeTypeEnum.Parent) != null)
			{
				iDstParentRow = ndDst.GetNode(NodeTypeEnum.Parent).Row.Index;
			}
			int Sno = ArchConvert.Obj2Int(gridBudget.Rows[DragInfo.row]["sNo"]);
			ExecResult ER = theItemA.GridNodeMoveTo(projectCode, Sno, TargetSno, TargetSortOrder, updateItemNo: true);
			if (iSrcParentRow == 0 || iDstParentRow == 0)
			{
				LoadProjectData();
				SetGridFocusBySno(Sno, NeedAtTop: false);
			}
			else
			{
				ReloadGridAtRootSno(ArchConvert.Obj2Int(gridBudget.Rows[iSrcParentRow]["sNo"]));
				ReloadGridAtRootSno(ArchConvert.Obj2Int(gridBudget.Rows[iDstParentRow]["sNo"]));
				SetGridFocusBySno(Sno, NeedAtTop: false);
			}
			Data2Grid();
		}
	}

	private void ultraButton1_Click(object sender, EventArgs e)
	{
		if (LeftPanel.Width == 0)
		{
			LeftPanel.Width = 160;
		}
		else
		{
			LeftPanel.Width = 0;
		}
	}

	private void gridBudget1_StartEdit(object sender, RowColEventArgs e)
	{
		Archnowledge.Common.DebugUtil.OutputDebugString("gridBudget1_StartEdit Start (" + e.Col + "," + e.Row + ") " + F_ModifyMode);
		if (e.Col <= 0 || e.Row <= 0)
		{
			return;
		}
		Row GridRow = gridBudget.Rows[e.Row];
		string PrintNo = "";
		string Kind = "";
		if (GridRow["PrintNo"] != null && GridRow["Kind"] != null)
		{
			PrintNo = GridRow["PrintNo"].ToString().Trim();
			Kind = GridRow["Kind"].ToString().Trim();
		}
		if (PrintNo == "" || Kind == "")
		{
			Archnowledge.Common.DebugUtil.OutputDebugString("gridBudget1_StartEdit PrintNo ='' or Kind =''");
			e.Cancel = true;
			gridBudget.Col = 0;
			return;
		}
		toolbarsManager.BeginUpdate();
		FORM_STATUS = FormStatus.Edit;
		toolbarsManager.Tools["Delete"].SharedProps.Shortcut = Shortcut.None;
		if (GridRow["sNO"] != null)
		{
			F_ModifyMode = ModiftyMode.EditItem;
		}
		else
		{
			F_ModifyMode = ModiftyMode.NewItem;
		}
		try
		{
			if (Kind.ToUpper() == "W")
			{
				DBClass DBCLS = new DBClass();
				DBCLS._FS_UserID = userID;
				DBCLS.MrsBase_Lock(GridRow["PubCode"].ToString().Trim(), projectCode, CommonMethods.GetActionNameString(FormActionName));
				DBCLS = null;
			}
			else
			{
				DBClass DBCLS = new DBClass();
				DBCLS._FS_UserID = userID;
				DBCLS.ItemA_Lock(GridRow["SNo"].ToString().Trim(), projectCode, CommonMethods.GetActionNameString(FormActionName));
				DBCLS = null;
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudget.cs--gridBudget1_StartEdit" + ex.Message);
			MessageBox.Show("FormBudget::gridBudget1_StartEdit()#1 Error =" + ex.Message, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		toolbarsManager.Enabled = false;
		toolbarsManager.EndUpdate();
		if (gridBudget.Cols[e.Col].Name.ToUpper() == "ITEMNO")
		{
			C1TextBox GridTextBox = new C1TextBox();
			GridTextBox.BorderStyle = BorderStyle.None;
			GridTextBox.MaxLength = 20;
			GridTextBox.Value = gridBudget.Cols[e.Col][e.Row];
			gridBudget.Editor = GridTextBox;
		}
		Archnowledge.Common.DebugUtil.OutputDebugString("gridBudget1_StartEdit End (" + e.Col + "," + e.Row + ") " + F_ModifyMode);
		if (gridBudget.Cols[e.Col].Name.ToUpper() == "Qty".ToUpper() || gridBudget.Cols[e.Col].Name.ToUpper() == "Cost".ToUpper() || gridBudget.Cols[e.Col].Name.ToUpper() == "BudgetChangeAddQty".ToUpper())
		{
			gridBudget.ImeMode = ImeMode.Off;
		}
	}

	private void frmBudget_Activated(object sender, EventArgs e)
	{
		if (FORM_STATUS == FormStatus.Active)
		{
			frmBudget_Resize(sender, e);
			FORM_STATUS = FormStatus.Normal;
			Application.DoEvents();
			if (sItemB_Err.Trim() != "")
			{
				Thread th1 = new Thread(Show_ItemB_Err);
				th1.Start();
			}
			if (F_IsDirectOpenCNT)
			{
				F_IsDirectOpenCNT = false;
			}
			CheckMainLItem_IsReach_ResourceItemTenPercent();
		}
	}

	private void CheckMainLItem_IsReach_ResourceItemTenPercent()
	{
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = userID;
		Application.DoEvents();
		if (FormActionName == PccesFormAction.BUD)
		{
			DataTable DT_Details = DBCLS.GetUserDefine("Select itemNo, CName, Kind from budItemA Where Kind='L' and ProjectCode='" + ProjectCode + "' ");
			Application.DoEvents();
			DataTable DT_Resource = DBCLS.GetUserDefine("Select pccesCode, CName from budProjMrsA Where ProjectCode='" + ProjectCode + "' ");
			Application.DoEvents();
			int DetailsLCount = DT_Details.Rows.Count;
			int ResorceCount = (int)((double)DT_Resource.Rows.Count * 0.1);
			if ((DetailsLCount > ResorceCount) ? true : false)
			{
				Application.DoEvents();
				Thread t1 = new Thread(ShowExceedResourceCount);
				Application.DoEvents();
				Thread.Sleep(100);
				Application.DoEvents();
				t1.Start();
			}
		}
	}

	private void ShowExceedResourceCount()
	{
		MessageBox.Show(this, "注意：主項大類的單獨計價項目數量超過專案工項總數10%，\n\u3000\u3000\u3000請先修正其主項大類目種類。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
	}

	private void Show_ItemB_Err()
	{
		Thread.Sleep(1150);
		MessageBox.Show(this, sItemB_Err, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
	}

	private void frmBudget_Shown(object sender, EventArgs e)
	{
		if (IsOwnerBidProject() && theItemA.ExistsUnchangedCode(projectCode))
		{
			DialogResult result = MessageBox.Show(this, "專案中尚有未對應工項，是否執行業主碼換公司碼？", "注意", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
			if (result == DialogResult.Yes)
			{
				OpenChangeToCompanyCodeWindow();
			}
		}
	}

	private void ultraToolbarsManager1_ToolClick(object sender, ToolClickEventArgs e)
	{
		DoMenuAction(e.Tool.Key);
	}

	private void gridBudget1_AfterEdit(object sender, RowColEventArgs e)
	{
		QtyAfterEdit = ArchConvert.Obj2Decimal(gridBudget[e.Row, "qty"]);
		CostAfterEdit = ArchConvert.Obj2Decimal(gridBudget[e.Row, "cost"]);
		AddQtyAfterEdit = ArchConvert.Obj2Decimal(gridBudget[e.Row, "BudgetChangeAddQty"]);
		Archnowledge.Common.DebugUtil.OutputDebugString("gridBudget1_AfterEdit Start(" + e.Col + "," + e.Row + ")" + F_ModifyMode);
		if (projectCode.Trim() == "" || e.Col <= 0 || e.Row <= 0)
		{
			return;
		}
		Row GridRow = gridBudget.Rows[e.Row];
		string ColumnName = gridBudget.Cols[e.Col].Name;
		if (ColumnName.ToUpper() == "QTY" && SysConfig.SysComsEnable)
		{
			GridRow["QTY"] = PubTools.ARound(GridRow["QTY"], MainItemQtyPrecision);
		}
		string PrintNo = "";
		string Kind = "";
		int SNo = 0;
		if (GridRow["PrintNo"] != null && GridRow["Kind"] != null)
		{
			PrintNo = GridRow["PrintNo"].ToString().Trim();
			Kind = GridRow["Kind"].ToString().Trim();
		}
		if (GridRow["SNo"] != null)
		{
			SNo = ArchConvert.Obj2Int(GridRow["SNo"]);
		}
		if (PrintNo == "" || Kind == "")
		{
			Archnowledge.Common.DebugUtil.OutputDebugString("gridBudget1_AfterEdit PrintNo ='' or Kind =''");
			e.Cancel = true;
			return;
		}
		if (F_NewAddItemFlag == "0" && F_IsNewProject == "")
		{
			if (FormActionName == PccesFormAction.BUD)
			{
				if (GetCurrentBDGT_Type() == "CNT")
				{
					ExecuteCopyToTmpCNT("");
					SetupRestoreSnapshotListCNT();
				}
				else
				{
					ExecuteCopyToTmp("");
					SetupRestoreSnapshotList();
				}
			}
			else
			{
				ExecuteCopyToTmp("");
				SetupRestoreSnapshotList();
			}
			F_NewAddItemFlag = "1";
		}
		if (ColumnName == "LockCost" && !PrintNo.Contains("9999"))
		{
			SetItemALockCost(PrintNo, ArchConvert.Obj2Bool(GridRow["LockCost"]), SNo, e.Row);
			return;
		}
		switch (ColumnName)
		{
		case "fixPrice":
			if (F_IsBid == "" && (Kind == "W" || Kind == "L"))
			{
				FixBidPrice(GridRow, PrintNo, Kind);
			}
			else
			{
				GridRow["fixPrice"] = false;
			}
			return;
		case "IsGreenItem":
		case "IsGreenMethod":
		case "IsGreenMaterial":
		case "IsGreenEnergy":
			if (_ActionName == PccesFormAction.BUD && !PrintNo.Contains("9999"))
			{
				SetItemAIsGreenItem(PrintNo, ArchConvert.Obj2Bool(GridRow[ColumnName]), ColumnName, SNo, e.Row);
				return;
			}
			break;
		}
		toolbarsManager.BeginUpdate();
		toolbarsManager.Enabled = true;
		if (F_ModifyMode == ModiftyMode.NewItem)
		{
			GridRow.IsNode = true;
			int iGivenLevel = -1;
			iGivenLevel = ((e.Row <= 1) ? 1 : gridBudget.Rows[e.Row - 1].Node.Level);
			GridRow.Node.Level = iGivenLevel;
			gridBudget[e.Row, "Kind"] = "B";
		}
		string IPStr = CommonMethods.GetIPAddress();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(userID);
		aArr.Add("預算書項目編輯後存檔--" + projectCode + "(" + IPStr + ")");
		if (dbItemA != null)
		{
			dbItemA = null;
		}
		if (dbItemA == null)
		{
			dbItemA = new Archnowledge.Pcces.BUDClass.ItemA(aArr);
		}
		dbItemA.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
		dbItemA.ps_projectCode = projectCode;
		dbItemA.ps_printNo = GridRow["PrintNo"].ToString().Trim();
		dbItemA.ps_itemNo = GridRow["ItemNo"].ToString();
		if ((ColumnName.ToUpper() == "QTY" || ColumnName.ToUpper() == "COST") && !((StateButtonTool)toolbarsManager.Tools["AutoRecalculate"]).Checked)
		{
			CalcuParent(e.Row);
		}
		if (ColumnName == "BudgetChangeAddQty")
		{
			GridRow["Qty"] = ArchConvert.Obj2Decimal(GridRow["BudgetChangeAddQty"]) + ArchConvert.Obj2Decimal(GridRow["QtyBeforeChange"]);
		}
		if (ColumnName == "Qty")
		{
			GridRow["BudgetChangeAddQty"] = ArchConvert.Obj2Decimal(GridRow["Qty"]) - ArchConvert.Obj2Decimal(GridRow["QtyBeforeChange"]);
		}
		if (FormActionName == PccesFormAction.BUD && budgetType == BudgetType.Types.Execution)
		{
			if (!AllowChangeBySNo(SNo, silentOnWarning: false, silentOnModify: true))
			{
				bool allowChange = true;
				bool IsOne4Item = GridRow["UnitName"].ToString() == "式" && ArchConvert.Obj2Decimal(GridRow["Qty"]) == 1m && !ArchConvert.Obj2Bool(GridRow["Analysis"]);
				if (((!IsOne4Item && (ColumnName == "Qty" || ColumnName == "BudgetChangeAddQty")) || (IsOne4Item && ColumnName.ToUpper() == "COST")) && !SysConfig.SysComsSkipQtyCheck)
				{
					Archnowledge.Pcces.DomainModule.Coms.BudgetCtrl theBudgetCtrl = new Archnowledge.Pcces.DomainModule.Coms.BudgetCtrl();
					DataTable dt = theBudgetCtrl.GetComsSubQtyAmt(projectCode, SysConfig.SysComsDB, SNo);
					if (!dt.Columns.Contains("SubQty"))
					{
						MessageBox.Show("取得COMS已發包" + (IsOne4Item ? "金額" : "數量") + "時發生錯誤", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
					else
					{
						decimal SubAmtValue = ArchConvert.Obj2Decimal(dt.Rows[0]["SubAmt"]);
						if (IsOne4Item)
						{
							decimal OriAmt = 0m;
							DataView dvItemA = new DataView(dtItemA);
							dvItemA.RowFilter = "Sno=" + SNo;
							if (dvItemA.Count == 1)
							{
								OriAmt = ArchConvert.Obj2Decimal(dvItemA[0]["Cost"]);
							}
							if (OriAmt >= 0m)
							{
								if (ArchConvert.Obj2Decimal(GridRow["Cost"]) < ArchConvert.Obj2Decimal(dt.Rows[0]["SubAmt"]))
								{
									MessageBox.Show(string.Format("金額不可低於已發包金額({0})", dt.Rows[0]["SubAmt"].ToString()), "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
									allowChange = false;
								}
							}
							else if (ArchConvert.Obj2Decimal(GridRow["Cost"]) > ArchConvert.Obj2Decimal(dt.Rows[0]["SubAmt"]))
							{
								MessageBox.Show(string.Format("金額不可高於已發包金額({0})", dt.Rows[0]["SubAmt"].ToString()), "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								allowChange = false;
							}
						}
						else if (ArchConvert.Obj2Decimal(GridRow["Qty"]) < ArchConvert.Obj2Decimal(dt.Rows[0]["SubQty"]))
						{
							MessageBox.Show(string.Format("數量不可低於已發包量({0})", dt.Rows[0]["SubQty"].ToString()), "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							allowChange = false;
						}
					}
				}
				if (!allowChange)
				{
					Reload_OneRow(ArchConvert.Obj2Int(GridRow["sNo"]), e.Row, RangeUpdate: true);
					gridBudget.AfterSelChange -= gridBudget1_AfterSelChange;
					gridBudget.Row -= 1;
					gridBudget.AfterSelChange += gridBudget1_AfterSelChange;
					Cursor = Cursors.Default;
					return;
				}
			}
			if (QtyAfterEdit != QtyBeforeEdit || CostAfterEdit != CostBeforeEdit || AddQtyAfterEdit != AddQtyBeforeEdit)
			{
				aArr.Clear();
				aArr.Add(userID);
				aArr.Add("判斷數量或單價有異動,寫入旗標到budItemA.QtyCstChgFlgforCOMS--" + projectCode + "(" + IPStr + ")");
				string sSNO = gridBudget[e.Row, "sno"].ToString();
				ModifyDB StdCom = new ModifyDB(ProjectCode, aArr);
				string sSQL = "Update budItemA Set QtyCstChgFlgforCOMS='Y' Where projectCode='" + projectCode + "' and sno=" + sSNO;
				StdCom.DBUpd(sSQL);
			}
		}
		if (F_ModifyMode == ModiftyMode.NewItem)
		{
			dbItemA.ps_sNo = (dbItemA.getMaxNo(projectCode) + 1).ToString();
			dbItemA.ps_kind = GridRow["Kind"].ToString();
			dbItemA.InseItem();
			GetRowData_AfterNew(dbItemA.ps_sNo);
		}
		else if (F_ModifyMode == ModiftyMode.EditItem)
		{
			DataView dvItemA = new DataView(dtItemA);
			dvItemA.RowFilter = "Sno = " + SNo;
			if (dvItemA.Count > 0)
			{
				DataRow drItemA = dvItemA[0].Row;
				if (ArchConvert.Obj2Decimal(drItemA["Qty"]) != ArchConvert.Obj2Decimal(GridRow["Qty"]) || ArchConvert.Obj2Decimal(drItemA["Cost"]) != ArchConvert.Obj2Decimal(GridRow["Cost"]))
				{
					CheckIsReCal("Y");
				}
				drItemA["ItemNo"] = ArchConvert.Obj2String(GridRow["ItemNo"]).Trim();
				drItemA["CName"] = ArchConvert.Obj2String(GridRow["CName"]).Trim();
				drItemA["UnitName"] = ArchConvert.Obj2String(GridRow["UnitName"]).Trim();
				drItemA["Qty"] = ArchConvert.Obj2Decimal(GridRow["Qty"]);
				drItemA["Cost"] = ArchConvert.Obj2Decimal(GridRow["Cost"]);
				drItemA["Amount"] = ArchConvert.Obj2Decimal(GridRow["Amount"]);
				drItemA["Memo"] = ArchConvert.Obj2String(GridRow["Memo"]).Trim();
				drItemA["EName"] = ArchConvert.Obj2String(GridRow["EName"]);
				if (Is75094900())
				{
					if (ArchConvert.Obj2String(GridRow["Kind"]) == "W")
					{
						drItemA["ExtendCode"] = ArchConvert.Obj2String(GridRow["ExtendCode"]);
					}
					else
					{
						drItemA["EUnit"] = ArchConvert.Obj2String(GridRow["ExtendCode"]);
					}
				}
				else
				{
					drItemA["EUnit"] = ArchConvert.Obj2String(GridRow["EUnit"]);
				}
				drItemA["LevelNo"] = ArchConvert.Obj2Int(GridRow["LevelNo"]);
				drItemA["share"] = ((GridRow["IsShared"] == null) ? DBNull.Value : GridRow["IsShared"]);
				drItemA["surName"] = ((GridRow["surName"] == null) ? DBNull.Value : GridRow["surName"]);
				drItemA["CostDec"] = ((GridRow["CostDec"] == null) ? DBNull.Value : GridRow["CostDec"]);
				drItemA["AmtDec"] = ((GridRow["AmtDec"] == null) ? DBNull.Value : GridRow["AmtDec"]);
				drItemA["PwrSet"] = PwrSet.GetCode(dsPwrSet, ArchConvert.Obj2String(GridRow["PwrSet"]).Trim()).ToString();
				if (FormActionName == PccesFormAction.BUD)
				{
					drItemA["BudgetChangeReason"] = GridRow["BudgetChangeReason"];
				}
				ExecResult ER = theItemA.GetDatasetUpdate(dsItemA);
				if (ER.ReturnCode != 0)
				{
					MessageBox.Show("更新失敗！" + ER.Message);
				}
			}
			else if (SNo != 0)
			{
				MessageBox.Show("更新資料失敗，請重新載入此專案，系統開發人員。");
			}
			dvItemA.Dispose();
			if (ArchConvert.Obj2String(GridRow["Kind"]).Trim().ToUpper() == "W")
			{
				aArr.Clear();
				aArr.Add(userID);
				aArr.Add(CommonMethods.GetFormTypeTitle(FormType.Budget));
				Archnowledge.Pcces.BUDClass.MrsBaseA dbMrsBaseA = new Archnowledge.Pcces.BUDClass.MrsBaseA(userID, aArr);
				dbMrsBaseA.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
				dbMrsBaseA.ps_projectcode = projectCode;
				dbMrsBaseA.ps_pccesCode = ((GridRow["PccesCode"] != null) ? GridRow["PccesCode"].ToString() : null);
				dbMrsBaseA.ps_cName = ((GridRow["CName"] != null) ? GridRow["CName"].ToString() : null);
				dbMrsBaseA.ps_cost = ((GridRow["Cost"] != null) ? GridRow["Cost"].ToString() : null);
				dbMrsBaseA.ps_eName = ((GridRow["EName"] != null) ? GridRow["EName"].ToString() : null);
				dbMrsBaseA.ps_eUnit = ((GridRow["EUnit"] != null) ? GridRow["EUnit"].ToString() : null);
				dbMrsBaseA.ps_memo = ((GridRow["Memo"] != null) ? GridRow["Memo"].ToString() : null);
				dbMrsBaseA.ps_rate = null;
				dbMrsBaseA.ps_unitName = ((GridRow["UnitName"] != null) ? GridRow["UnitName"].ToString() : null);
				dbMrsBaseA.ps_surName = ((GridRow["surName"] != null) ? GridRow["surName"].ToString() : null);
				dbMrsBaseA.ps_CstDec = ((GridRow["CostDec"] == null) ? null : ((GridRow["CostDec"].ToString() == MainItemCostPrecison.ToString()) ? null : GridRow["CostDec"].ToString()));
				dbMrsBaseA.ps_AmtDec = ((GridRow["AmtDec"] == null) ? null : ((GridRow["AmtDec"].ToString() == MainItemAmountPrecision.ToString()) ? null : GridRow["AmtDec"].ToString()));
				dbMrsBaseA.ps_account = ((GridRow["Account"] != null) ? GridRow["Account"].ToString() : null);
				dbMrsBaseA.ps_extendCode = ((GridRow["ExtendCode"] != null) ? GridRow["ExtendCode"].ToString() : null);
				dbMrsBaseA.UpdItem();
				dbMrsBaseA = null;
				Archnowledge.Pcces.BUDClass.MrsBaseB dbMrsBaseB = new Archnowledge.Pcces.BUDClass.MrsBaseB(aArr);
				dbMrsBaseB.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
				dbMrsBaseB.ps_projectcode = projectCode;
				dbMrsBaseB.ps_pubCode = GridRow["PubCode"].ToString();
				dbMrsBaseB.ps_CstDec = ((GridRow["CostDec"] == null) ? null : ((GridRow["CostDec"].ToString() == MainItemCostPrecison.ToString()) ? null : GridRow["CostDec"].ToString()));
				dbMrsBaseB.ps_AmtDec = ((GridRow["AmtDec"] == null) ? null : ((GridRow["AmtDec"].ToString() == MainItemAmountPrecision.ToString()) ? null : GridRow["AmtDec"].ToString()));
				dbMrsBaseB.UpdateAptoticDec();
				dbMrsBaseB = null;
			}
			Reload_OneRow(ArchConvert.Obj2Int(GridRow["sNo"]), e.Row, RangeUpdate: true);
		}
		if (ColumnName.ToUpper() == "COSTDEC")
		{
			iCst++;
			CellStyle styCstDec = gridBudget.Styles.Add("CostDec" + iCst);
			if (PubTools.Str2Int(GridRow["CostDec"]) > 0)
			{
				styCstDec.Format = "###,###,###,##0." + "0".PadLeft(PubTools.Str2Int(GridRow["CostDec"]), '0');
			}
			else
			{
				styCstDec.Format = "###,###,###,##0";
			}
			if (GridRow["Kind"].ToString().Trim().ToUpper() != "Z")
			{
				gridBudget.SetCellStyle(e.Row, gridBudget.Cols["Cost"].SafeIndex, styCstDec);
			}
		}
		if (ColumnName.ToUpper() == "AMTDEC")
		{
			iAmt++;
			CellStyle styAmtDec = gridBudget.Styles.Add("AmtDec" + iAmt);
			if (PubTools.Str2Int(GridRow["AmtDec"]) > 0)
			{
				styAmtDec.Format = "###,###,###,##0." + "0".PadLeft(PubTools.Str2Int(GridRow["AmtDec"]), '0');
			}
			else
			{
				styAmtDec.Format = "###,###,###,##0";
			}
			gridBudget.SetCellStyle(e.Row, gridBudget.Cols["Amount"].SafeIndex, styAmtDec);
			gridBudget.SetCellStyle(e.Row, gridBudget.Cols["AmountBeforeChange"].SafeIndex, styAmtDec);
		}
		if (ColumnName == "PwrSet" && theItemA != null)
		{
			int pwrSet = PwrSet.GetCode(dsPwrSet, ArchConvert.Obj2String(GridRow["PwrSet"]).Trim());
			bool updateEntireProject = SysConfig.SysEnablePwrSetSync;
			ExecResult ER = theProjMrsA.SetPwrSet(projectCode, ArchConvert.Obj2Int(GridRow["pubCode"]), pwrSet, updateEntireProject);
			if (ER.ReturnCode != 0)
			{
				MessageBox.Show(ER.Message, "發包權限存取錯誤");
			}
			else if (updateEntireProject)
			{
				F_SNo = ArchConvert.Obj2Int(GridRow["sNo"]);
				LoadProjectData();
				F_SNo = -1;
			}
		}
		F_ModifyMode = ModiftyMode.None;
		if (((StateButtonTool)toolbarsManager.Tools["AutoRecalculate"]).Checked)
		{
			int iBackTo = e.Row;
			Th_ReCal_All(Auto: true);
			gridBudget.AfterSelChange -= gridBudget1_AfterSelChange;
			gridBudget.Row = iBackTo;
			gridBudget.AfterSelChange += gridBudget1_AfterSelChange;
		}
		if (Kind.ToUpper() == "W")
		{
			DBClass DBCLS = new DBClass();
			DBCLS._FS_UserID = userID;
			DBCLS.MrsBase_UnLock(GridRow["PubCode"].ToString().Trim(), projectCode, CommonMethods.GetActionNameString(FormActionName));
			DBCLS = null;
		}
		else
		{
			DBClass DBCLS = new DBClass();
			DBCLS._FS_UserID = userID;
			DBCLS.ItemA_UnLock(GridRow["SNo"].ToString().Trim(), projectCode, CommonMethods.GetActionNameString(FormActionName));
			DBCLS = null;
		}
		FORM_STATUS = FormStatus.Normal;
		toolbarsManager.Enabled = true;
		toolbarsManager.EndUpdate();
		((ButtonTool)toolbarsManager.Tools["Delete"]).SharedProps.Shortcut = Shortcut.Del;
		Archnowledge.Common.DebugUtil.OutputDebugString("gridBudget1_AfterEdit End(" + e.Col + "," + e.Row + ")" + F_ModifyMode);
	}

	private void SetItemALockCost(string PrintNo, bool Lock, int Sno, int EditRowIndex)
	{
		toolbarsManager.BeginUpdate();
		FM_INFO = new FormSys_G_Info1();
		FM_INFO._InfoString = "項目鎖定處理中，請稍候！";
		FM_INFO.Show();
		Application.DoEvents();
		Cursor = Cursors.WaitCursor;
		gridBudget.Enabled = false;
		Application.DoEvents();
		ExecResult ER = theItemA.SetItemALockCost(projectCode, PrintNo, Lock);
		if (ER.ReturnCode != 0)
		{
			MessageBox.Show(this, ER.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		if (gridBudget[EditRowIndex, "Kind"].ToString() == "B")
		{
			ReloadGridAtRootSno(Sno);
		}
		else
		{
			Reload_OneRow(Sno, EditRowIndex, RangeUpdate: false);
		}
		FM_INFO.Close();
		FM_INFO.Dispose();
		FM_INFO = null;
		gridBudget.Enabled = true;
		toolbarsManager.EndUpdate();
		Cursor = Cursors.Default;
	}

	private void SetItemAIsGreenItem(string PrintNo, bool IsGreenItem, string columnName, int Sno, int EditRowIndex)
	{
		toolbarsManager.BeginUpdate();
		FM_INFO = new FormSys_G_Info1();
		FM_INFO._InfoString = "綠色內涵項目處理中，請稍候！";
		FM_INFO.Show();
		FM_INFO.BringToFront();
		Application.DoEvents();
		Cursor = Cursors.WaitCursor;
		gridBudget.Enabled = false;
		Application.DoEvents();
		BudItemA budItemA = new BudItemA();
		ExecResult ER = budItemA.SetItemAIsGreenItem(projectCode, PrintNo, IsGreenItem, columnName);
		if (ER.ReturnCode != 0)
		{
			MessageBox.Show(this, ER.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		Reload_OneRow(Sno, EditRowIndex, RangeUpdate: false);
		FM_INFO.Close();
		FM_INFO.Dispose();
		FM_INFO = null;
		gridBudget.Enabled = true;
		toolbarsManager.EndUpdate();
		Cursor = Cursors.Default;
	}

	private void FixBidPrice(Row GridRow, string PrintNo, string Kind)
	{
		if (GridRow["fixPrice"] == null)
		{
			return;
		}
		F_IsBid = "start";
		toolbarsManager.BeginUpdate();
		FM_INFO = new FormSys_G_Info1();
		FM_INFO._InfoString = "項目標單固定單價處理中，請稍候！";
		FM_INFO.Show();
		Application.DoEvents();
		Cursor = Cursors.WaitCursor;
		gridBudget.Enabled = false;
		Application.DoEvents();
		string IPStr = CommonMethods.GetIPAddress();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(userID);
		aArr.Add("預算書匯出電子檔標單固定單價--" + projectCode + "(" + IPStr + ")");
		try
		{
			string sLockCheck = (ArchConvert.Obj2Bool(GridRow["fixPrice"]) ? "1" : "0");
			Archnowledge.Pcces.BUDClass.ItemA ItemACom = new Archnowledge.Pcces.BUDClass.ItemA(aArr);
			ItemACom.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
			ItemACom.ps_projectCode = projectCode;
			ItemACom.LockCost(projectCode, PrintNo, sLockCheck, "fixPrice");
			ItemACom.UpdateMemofixprice(projectCode, PrintNo, sLockCheck, "", Kind);
			ItemACom.LockCost(projectCode, PrintNo, sLockCheck, "LockCost");
			if (GridRow["Kind"].ToString() == "B")
			{
				ReloadGridAtRootSno(ArchConvert.Obj2Int(GridRow["sNo"]));
			}
			else
			{
				Reload_OneRow(ArchConvert.Obj2Int(GridRow["sNo"]), GridRow.Index, RangeUpdate: false);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(this, "FormBudget::gridBudget1_AfterEdit()#2 " + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		FM_INFO.Close();
		FM_INFO.Dispose();
		FM_INFO = null;
		F_IsBid = "";
		gridBudget.Enabled = true;
		toolbarsManager.EndUpdate();
		Cursor = Cursors.Default;
	}

	private bool AllowChangeBySNo(object rowSNo, bool silentOnWarning, bool silentOnModify)
	{
		ComsWebService theComsWebService = new ComsWebService(projectCode);
		int sNo = ArchConvert.Obj2Int(rowSNo);
		return theComsWebService.AllowChangeBysNo(sNo, -1, silentOnWarning, silentOnModify);
	}

	private bool AllowChangeCheckAccQtyBySNo(string pccesCode, int sNo, string unitName, decimal qty, decimal cost, bool silentOnWarning, bool silentOnModify)
	{
		return false;
	}

	private bool AllowChangeByAccQtyAmtByPccesCode_fordel(string pccesCode, string unitName, decimal diffqty, decimal diffcost, bool silentOnWarning, bool silentOnModify)
	{
		ComsWebService theComsWebService = new ComsWebService(projectCode);
		return theComsWebService.AllowChangeByAccQtyAmtByPccesCode_fordel(pccesCode, unitName, diffqty, diffcost, silentOnWarning, silentOnModify);
	}

	private void GetRowData_AfterNew(string sNO)
	{
		string IPStr = CommonMethods.GetIPAddress();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(userID);
		aArr.Add("預算書新增之後, 再載入該筆資料--" + projectCode + "(" + IPStr + ")");
		if (dbItemA != null)
		{
			dbItemA = null;
		}
		if (dbItemA == null)
		{
			dbItemA = new Archnowledge.Pcces.BUDClass.ItemA(aArr);
		}
		dbItemA.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
		dbItemA.ps_projectCode = projectCode;
		DataTable DT_Inner = dbItemA.ListItem(" sNo =" + sNO, projectCode);
		gridBudget[gridBudget.Row, "ItemNo"] = DT_Inner.Rows[0]["ItemNo"];
		gridBudget[gridBudget.Row, "CName"] = DT_Inner.Rows[0]["cName"];
		gridBudget[gridBudget.Row, "UnitName"] = DT_Inner.Rows[0]["unitName"];
		gridBudget[gridBudget.Row, "Qty"] = DT_Inner.Rows[0]["qty"];
		gridBudget[gridBudget.Row, "LockCost"] = DT_Inner.Rows[0]["LockCost"].ToString() == "1";
		gridBudget[gridBudget.Row, "Cost"] = DT_Inner.Rows[0]["cost"];
		gridBudget[gridBudget.Row, "Amount"] = DT_Inner.Rows[0]["amount"];
		gridBudget[gridBudget.Row, "PccesCode"] = DT_Inner.Rows[0]["pccesCode"];
		gridBudget[gridBudget.Row, "Memo"] = DT_Inner.Rows[0]["memo"];
		gridBudget[gridBudget.Row, "EName"] = DT_Inner.Rows[0]["eName"];
		gridBudget[gridBudget.Row, "EUnit"] = DT_Inner.Rows[0]["eUnit"];
		gridBudget[gridBudget.Row, "LevelNo"] = DT_Inner.Rows[0]["levelNo"];
		gridBudget[gridBudget.Row, "SNo"] = DT_Inner.Rows[0]["sno"];
		gridBudget[gridBudget.Row, "Kind"] = DT_Inner.Rows[0]["kind"];
		gridBudget[gridBudget.Row, "PrintNo"] = DT_Inner.Rows[0]["printNo"].ToString().Trim();
		gridBudget[gridBudget.Row, "Formula"] = DT_Inner.Rows[0]["Formula"];
		gridBudget[gridBudget.Row, "PubCode"] = DT_Inner.Rows[0]["pubCode"];
	}

	private void DoMenuAction(string MenuID)
	{
		Frm.Hide();
		switch (MenuID)
		{
		case "SwitchProject":
			if (FormActionName == PccesFormAction.BUD && !DBClass.ChkAuthority(userID, "F00300010001"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00300010001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (FormActionName == PccesFormAction.BID && !DBClass.ChkAuthority(userID, "F00400010001"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00400010001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Execute_SwitchProject();
			}
			break;
		case "ExportFileAndReport":
			Do_BudBidFileDigital();
			break;
		case "PreviewReport":
			if (FormActionName == PccesFormAction.BUD && !DBClass.ChkAuthority(userID, "F00300010003"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00300010003") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (FormActionName == PccesFormAction.BID && !DBClass.ChkAuthority(userID, "F00400010003"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00400010003") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				ExecuteReportForm();
			}
			break;
		case "Exit":
		{
			string sWarning = "確定要結束 ?";
			if (MessageBox.Show(this, sWarning, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				AdjustmentFlag = "ManualClose";
				(base.ParentForm as frmPccesMain).LeftPanel.Width = 160;
				(base.ParentForm as frmPccesMain).LoadingForm();
				Close();
			}
			break;
		}
		case "Cut":
			if (FormActionName == PccesFormAction.BUD && !DBClass.ChkAuthority(userID, "F00300020004"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00300020004") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (FormActionName == PccesFormAction.BID && !DBClass.ChkAuthority(userID, "F00400020004"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00400020004") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				CutItems();
			}
			break;
		case "Paste":
			if (FormActionName == PccesFormAction.BUD && !DBClass.ChkAuthority(userID, "F00300020006"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00300020006") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (FormActionName == PccesFormAction.BID && !DBClass.ChkAuthority(userID, "F00400020006"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00400020006") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Do_Edit_Paste();
			}
			break;
		case "Copy":
			if (FormActionName == PccesFormAction.BUD && !DBClass.ChkAuthority(userID, "F00300020005"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00300020005") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (FormActionName == PccesFormAction.BID && !DBClass.ChkAuthority(userID, "F00400020005"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00400020005") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Do_Edit_Copy();
			}
			break;
		case "EditMainItem":
			if (FormActionName == PccesFormAction.BUD && !DBClass.ChkAuthority(userID, "F00300040002"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00300040002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (FormActionName == PccesFormAction.BID && !DBClass.ChkAuthority(userID, "F00400040002"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00400040002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				EditItemsByKind();
			}
			break;
		case "EditWorkItem":
			if (FormActionName == PccesFormAction.BUD && !DBClass.ChkAuthority(userID, "F00300040004"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00300040004") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (FormActionName == PccesFormAction.BID && !DBClass.ChkAuthority(userID, "F00400040004"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00400040004") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				EditItemsByKind();
			}
			break;
		case "MakeAmortizedItem":
			if (FormActionName == PccesFormAction.BUD && !DBClass.ChkAuthority(userID, "F00300040006"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00300040006") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (FormActionName == PccesFormAction.BID && !DBClass.ChkAuthority(userID, "F00400040006"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00400040006") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				SetAsSharedItem();
			}
			break;
		case "Recalculate":
			if (FormActionName == PccesFormAction.BUD && !DBClass.ChkAuthority(userID, "F00300050001"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00300050001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				break;
			}
			if (FormActionName == PccesFormAction.BID && !DBClass.ChkAuthority(userID, "F00400050001"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00400050001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				break;
			}
			if (FormActionName == PccesFormAction.BUD && SysConfig.SysChangeManagement && (budgetType == BudgetType.Types.Normal || budgetType == BudgetType.Types.Execution || budgetType == BudgetType.Types.Award))
			{
				DBClass DBCls = new DBClass();
				DBCls._FS_UserID = _UserID;
				string sSQL = "update budItemA set CostDec = budExeItemA.CostDec, QtyDec = budExeItemA.QtyDec   From budExeItemA  Where budItemA.projectCode='" + projectCode + "' and budExeItemA.projectCode='" + projectCode + "'    And budItemA.sNo = budExeItemA.sNo    And budExeItemA.version = (Select MAX(version) From budExeItemA Where projectCode='" + projectCode + "')";
				DBCls.ExecuteCommand(sSQL);
				DBCls = null;
			}
			Th_ReCal_All(Auto: false);
			if (gridBudget[gridBudget.Row, "Kind"] != null && gridBudget[gridBudget.Row, "Kind"].ToString().Trim() == "W")
			{
				toolbarsManager.Tools["EditMainItem"].SharedProps.Enabled = false;
			}
			break;
		case "AutoRecalculate":
			if (FormActionName == PccesFormAction.BUD && !DBClass.ChkAuthority(userID, "F00300050002"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00300050002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (FormActionName == PccesFormAction.BID && !DBClass.ChkAuthority(userID, "F00400050002"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00400050002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			break;
		case "AdjustTotalAmount":
			if (FormActionName == PccesFormAction.BUD && !DBClass.ChkAuthority(userID, "F00300050003"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00300050003") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (FormActionName == PccesFormAction.BID && !DBClass.ChkAuthority(userID, "F00400050003"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00400050003") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Do_CostAdjust();
			}
			break;
		case "ReArrangeItemNo":
			if (FormActionName == PccesFormAction.BUD && !DBClass.ChkAuthority(userID, "F00300050004"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00300050004") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (FormActionName == PccesFormAction.BID && !DBClass.ChkAuthority(userID, "F00400050004"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00400050004") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Do_ItemReArrange(isSilence: false);
			}
			break;
		case "ImportMrsBaseItemName":
			if (FormActionName == PccesFormAction.BUD && !DBClass.ChkAuthority(userID, "F00300050005"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00300050005") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (FormActionName == PccesFormAction.BID && !DBClass.ChkAuthority(userID, "F00400050005"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00400050005") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Do_NameReArrange();
			}
			break;
		case "ImportAllMrsBaseItemCost":
			if (FormActionName == PccesFormAction.BUD && !DBClass.ChkAuthority(userID, "F00300050006"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00300050006") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (FormActionName == PccesFormAction.BID && !DBClass.ChkAuthority(userID, "F00400050006"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00400050006") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Do_UseItemPrice();
			}
			break;
		case "ImportSelectedMrsBaseItemCost":
			Do_UseSelItemPrice();
			break;
		case "ImportAllMrsBaseCostBreakdown":
			if (FormActionName == PccesFormAction.BUD && !DBClass.ChkAuthority(userID, "F00300050007"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00300050007") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (FormActionName == PccesFormAction.BID && !DBClass.ChkAuthority(userID, "F00400050007"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00400050007") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Do_UseBreakdown();
			}
			break;
		case "ImportSelectedMrsBaseCostBreakdown":
			Do_UseSelBreakdown();
			break;
		case "CombineBudget":
			if (FormActionName == PccesFormAction.BUD && !DBClass.ChkAuthority(userID, "F00300010005"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00300010005") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (FormActionName == PccesFormAction.BID && !DBClass.ChkAuthority(userID, "F00400010005"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00400010005") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Do_Combine();
			}
			break;
		case "CombineBid":
			Do_CombineBid();
			break;
		case "SetPrecision":
			if (FormActionName == PccesFormAction.BUD && !DBClass.ChkAuthority(userID, "F00300050008"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00300050008") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (FormActionName == PccesFormAction.BID && !DBClass.ChkAuthority(userID, "F00400050008"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00400050008") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				ExecuteDecimalSetting();
			}
			break;
		case "AboutPcces":
		{
			FormAbout FMAB = new FormAbout();
			FMAB.ShowDialog();
			FMAB.Dispose();
			FMAB = null;
			break;
		}
		case "MoveUp":
			if (FormActionName == PccesFormAction.BUD && !DBClass.ChkAuthority(userID, "F003000400080003"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F003000400080003") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (FormActionName == PccesFormAction.BID && !DBClass.ChkAuthority(userID, "F004000400080003"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F004000400080003") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				DoUp();
			}
			break;
		case "MoveDown":
			if (FormActionName == PccesFormAction.BUD && !DBClass.ChkAuthority(userID, "F003000400080004"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F003000400080004") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (FormActionName == PccesFormAction.BID && !DBClass.ChkAuthority(userID, "F003000400080004"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F004000400080004") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				DoDown();
			}
			break;
		case "Outdent":
			if (FormActionName == PccesFormAction.BUD && !DBClass.ChkAuthority(userID, "F003000400080001"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F003000400080001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (FormActionName == PccesFormAction.BID && !DBClass.ChkAuthority(userID, "F004000400080001"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F004000400080001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				DoOutdent();
			}
			break;
		case "Indent":
			if (FormActionName == PccesFormAction.BUD && !DBClass.ChkAuthority(userID, "F003000400080002"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F003000400080002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (FormActionName == PccesFormAction.BID && !DBClass.ChkAuthority(userID, "F004000400080002"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F004000400080002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				DoIndent();
			}
			break;
		case "EditCostBreakdown":
			if (FormActionName == PccesFormAction.BUD && !DBClass.ChkAuthority(userID, "F00300040005"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00300040005") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (FormActionName == PccesFormAction.BID && !DBClass.ChkAuthority(userID, "F00400040005"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00400040005") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				ExecuteBreakdownForm();
			}
			break;
		case "Delete":
			DeleteItem();
			break;
		case "AddNewWorkItem":
			if (FormActionName == PccesFormAction.BUD && !DBClass.ChkAuthority(userID, "F003000400030001"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F003000400030001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (FormActionName == PccesFormAction.BID && !DBClass.ChkAuthority(userID, "F004000400030001"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F004000400030001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				DoInsertWorkItems();
			}
			break;
		case "InsertWorkItemPickFromOtherBudget":
			if (FormActionName == PccesFormAction.BUD && !DBClass.ChkAuthority(userID, "F003000400030002"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F003000400030002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (FormActionName == PccesFormAction.BID && !DBClass.ChkAuthority(userID, "F004000400030002"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F004000400030002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				ExecutePickFromProj();
			}
			break;
		case "InsertWorkItemPickFromMrsBase":
			if (FormActionName == PccesFormAction.BUD && !DBClass.ChkAuthority(userID, "F003000400030003"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F003000400030003") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (FormActionName == PccesFormAction.BID && !DBClass.ChkAuthority(userID, "F004000400030003"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F004000400030003") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				ExecutePickFromMrs();
			}
			break;
		case "InsertMainItemSibling":
			if (FormActionName == PccesFormAction.BUD && !DBClass.ChkAuthority(userID, "F003000400010001"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F003000400010001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (FormActionName == PccesFormAction.BID && !DBClass.ChkAuthority(userID, "F004000400010001"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F004000400010001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				DoInsertMainItems(InsertChild: false);
			}
			break;
		case "InsertMainItemChildren":
			if (FormActionName == PccesFormAction.BUD && !DBClass.ChkAuthority(userID, "F003000400010002"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F003000400010002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (FormActionName == PccesFormAction.BID && !DBClass.ChkAuthority(userID, "F004000400010002"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F004000400010002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				DoInsertMainItems(InsertChild: true);
			}
			break;
		case "MaintainProjectResources":
			if (FormActionName == PccesFormAction.BUD && !DBClass.ChkAuthority(userID, "F00300030001"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00300030001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (FormActionName == PccesFormAction.BID && !DBClass.ChkAuthority(userID, "F00400030001"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00400030001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				ExecuteResForm();
			}
			break;
		case "EditProjectInfo":
			if (FormActionName == PccesFormAction.BUD && !DBClass.ChkAuthority(userID, "F00300030002"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00300030002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (FormActionName == PccesFormAction.BID && !DBClass.ChkAuthority(userID, "F00400030002"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00400030002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				DoMenuViewProjectInfo(1);
			}
			break;
		case "PickItemFromMainProject":
			if (FormActionName == PccesFormAction.BUD && !DBClass.ChkAuthority(userID, "F003000400030004"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F003000400030004") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (FormActionName == PccesFormAction.BID && !DBClass.ChkAuthority(userID, "F004000400030004"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F004000400030004") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				DoPickFromMain();
			}
			break;
		case "DeleteThisProject":
			if (FormActionName == PccesFormAction.BUD && !DBClass.ChkAuthority(userID, "F00300010006"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00300010006") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (FormActionName == PccesFormAction.BID && !DBClass.ChkAuthority(userID, "F00400010006"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00400010006") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				DoDeleteThisBDGT();
			}
			break;
		case "EditItemNoSetting":
			if (FormActionName == PccesFormAction.BUD && !DBClass.ChkAuthority(userID, "F00300050009"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00300050009") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (FormActionName == PccesFormAction.BID && !DBClass.ChkAuthority(userID, "F00400050009"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00400050009") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Execute_Option();
			}
			break;
		case "SearchKeyword":
			Do_ToolBarFind();
			break;
		case "CancelAmortizedItem":
			if (FormActionName == PccesFormAction.BUD && !DBClass.ChkAuthority(userID, "F00300040007"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00300040007") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (FormActionName == PccesFormAction.BID && !DBClass.ChkAuthority(userID, "F00400040007"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00400040007") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Do_CancelShare();
			}
			break;
		case "ExportDetailList":
			Do_Export();
			break;
		case "OpenMicrosoftCalculator":
			Execute_Calculator();
			break;
		case "ImportQtyFrom3rdPartyTool":
			Do_3rdParty();
			Data2Grid();
			break;
		case "EditCustomizedVariable":
			if (FormActionName == PccesFormAction.BUD && !DBClass.ChkAuthority(userID, "F00300040009"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00300040009") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (FormActionName == PccesFormAction.BID && !DBClass.ChkAuthority(userID, "F00400040009"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00400040009") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Execute_PCalsCustomVar();
			}
			break;
		case "EditPageBreakSetting":
			Execute_PageBreak();
			break;
		case "ExportTaiwanRailwayCustomizedReport":
			Do_FileDigital("Z14AC1100");
			break;
		case "BackupProject":
			Execute_Backup();
			break;
		case "RestoreProject":
			Execute_Restore();
			break;
		case "PccesNews":
		{
			FormUpdateInfo FM_UPDINFO = new FormUpdateInfo();
			FM_UPDINFO.ShowDialog();
			FM_UPDINFO.Dispose();
			FM_UPDINFO = null;
			break;
		}
		case "EditBidSetting":
			Execute_BidSet();
			break;
		case "LockCost":
			Do_CostLock("1");
			break;
		case "UnlockCost":
			Do_CostLock("0");
			break;
		case "ClearDetailListCost":
			Do_ClearCost();
			break;
		case "AddBookmark":
			Add_Bookmark();
			break;
		case "ClearAllBookmark":
			Clear_Bookmark();
			break;
		case "ClearSelectedBookmark":
			Clear_Bookmark_Speci();
			break;
		case "ReconstructConnectionWithMrsBase":
			DoDBRest();
			break;
		case "Level1":
			gridBudget.Tree.Show(1);
			break;
		case "Level2":
			gridBudget.Tree.Show(2);
			break;
		case "Level3":
			gridBudget.Tree.Show(3);
			break;
		case "Level4":
			gridBudget.Tree.Show(4);
			break;
		case "Level5":
			gridBudget.Tree.Show(5);
			break;
		case "Level6":
			gridBudget.Tree.Show(6);
			break;
		case "Level7":
			gridBudget.Tree.Show(7);
			break;
		case "Level8":
			gridBudget.Tree.Show(8);
			break;
		case "CloneWorkItem":
			ExecuteEditForm(MrsBaseEditFormType.CopyToNew);
			break;
		case "TakeSnapshot":
			ExecuteCopyToTmp("Y");
			SetupRestoreSnapshotList();
			break;
		case "TakeSnapshotCnt":
		{
			if (MessageBox.Show(this, "契約是否要從標單轉入?", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
			{
				ExecuteCopyToTmpCNT("Y");
				SetupRestoreSnapshotListCNT();
				break;
			}
			string SwitchProjectCode = projectCode;
			lock (this)
			{
				FormBudgetProjectPick FM_BDGT_PPK1 = new FormBudgetProjectPick();
				FM_BDGT_PPK1.CallUpType = FormBudget_PickType.ProjectSwitch;
				FM_BDGT_PPK1._ActionName = PccesFormAction.CNT;
				FM_BDGT_PPK1._UserID = userID;
				FM_BDGT_PPK1._CurrentEditProjectCode = projectCode;
				if (FM_BDGT_PPK1.ShowDialog(this) == DialogResult.OK)
				{
					string sPCode = FM_BDGT_PPK1._SelectedProjectCode;
					FormAskQuestion FM2 = new FormAskQuestion();
					FM2._SelectedProjectCode = sPCode;
					if (FM2.ShowDialog() == DialogResult.OK)
					{
						ExecuteCopyToTmpCNT("");
						SetupRestoreSnapshotListCNT();
						ArrayList aArr = new ArrayList();
						aArr.Clear();
						aArr.Add(userID);
						aArr.Add("自標單匯入--" + projectCode);
						Archnowledge.Pcces.BUDClass.Project PJ1 = new Archnowledge.Pcces.BUDClass.Project(aArr);
						PJ1.ps_srckind = "CNT";
						PJ1.DeleProj(projectCode);
						PJ1.ps_srckind = "BID";
						PJ1.CopyProjBidToBud(projectCode, sPCode);
						SetCurrentBDGT_Type("CNT");
						LoadProjectData();
					}
				}
				break;
			}
		}
		case "TakeSnapshotCntFromBid":
		{
			string SwitchProjectCode = projectCode;
			Cursor = Cursors.WaitCursor;
			lock (this)
			{
				FormBudgetProjectPick FM_BDGT_PPK1 = new FormBudgetProjectPick();
				FM_BDGT_PPK1.CallUpType = FormBudget_PickType.ProjectSwitch;
				FM_BDGT_PPK1._ActionName = PccesFormAction.CNT;
				FM_BDGT_PPK1._UserID = userID;
				FM_BDGT_PPK1._CurrentEditProjectCode = projectCode;
				if (FM_BDGT_PPK1.ShowDialog(this) == DialogResult.OK)
				{
					string sPCode = FM_BDGT_PPK1._SelectedProjectCode;
					FormAskQuestion FM2 = new FormAskQuestion();
					FM2._SelectedProjectCode = sPCode;
					if (FM2.ShowDialog() == DialogResult.OK)
					{
						ExecuteCopyToTmpCNT("");
						SetupRestoreSnapshotListCNT();
						ArrayList aArr = new ArrayList();
						aArr.Clear();
						aArr.Add(userID);
						aArr.Add("自標單匯入--" + projectCode);
						Archnowledge.Pcces.BUDClass.Project PJ1 = new Archnowledge.Pcces.BUDClass.Project(aArr);
						PJ1.ps_srckind = "CNT";
						PJ1.DeleProj(projectCode);
						PJ1.ps_srckind = "BID";
						PJ1.CopyProjBidToBud(projectCode, sPCode);
						SetCurrentBDGT_Type("CNT");
						LoadProjectData();
					}
				}
				break;
			}
		}
		case "LoadTemplate":
			Execute_BudIstemplate();
			break;
		case "EditAliasSettingForReport":
			Execute_IssurNameSet();
			break;
		case "EditProjectOption":
			Execute_OptionMain();
			break;
		case "HideAliasColumn":
			gridBudget.Cols["surName"].Visible = false;
			break;
		case "ShowAliasColumn":
			gridBudget.Cols["surName"].Visible = true;
			break;
		case "ManageSnapshot":
			Execute_SetMain();
			break;
		case "InsertWorkItemPickFromCostStructure":
			Execute_CostStructure();
			break;
		case "EditCostStructureProperty":
			Execute_CostProperty();
			break;
		case "COMSCheckBudgetFromContract":
			MessageBox.Show("注意：1.若更新項目為單價分析項，帶入會自動轉成一般工項。 2.不存在於業主契約中的項目不做任何更動。");
			if (MessageBox.Show(this, "帶入『業主契約的單價及名稱』，更新後不可還原，確定 ? ", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				COMSCheckBudgetFromContract();
				MessageBox.Show("更新完成，請執行重新總計。 [變更歷程]欄位紀錄更新狀態。");
			}
			break;
		case "COMSLoadBudgetFromContract":
			if (SysConfig.SysComsEnable)
			{
				if (MessageBox.Show(this, "確定要執行『從業主契約載入資料』？", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
				{
					break;
				}
				Cursor = Cursors.WaitCursor;
				string tmpProjectCode = "";
				string IPStr = CommonMethods.GetIPAddress();
				DataSet ds = new DataSet();
				ExecResult ER = new ExecResult();
				try
				{
					CtrServiceHelper theCtrServiceHelper = new CtrServiceHelper();
					ds = theCtrServiceHelper.GetCtr2DS(projectCode, out ER);
					if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
					{
						tmpProjectCode = ds.Tables["Project"].Rows[0]["projectCode"].ToString();
					}
				}
				catch (Exception ex)
				{
					ER.ReturnCode = 1;
					ER.Message = "呼叫服務發生錯誤，訊息如下：\n" + ex.Message;
				}
				if (ER.ReturnCode == 0)
				{
					if (ds.Tables["Items"].Rows.Count == 0 || ds.Tables["Project"].Rows.Count == 0)
					{
						if (ds.Tables["Project"].Rows.Count == 0)
						{
							MessageBox.Show(this, "Coms無此" + projectCode + "專案資料轉入!", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						}
						else
						{
							MessageBox.Show(this, "Coms無專案詳細資料轉入!", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						}
						break;
					}
					string sPrj_UID = "";
					if (ds.Tables["Project"].Rows.Count > 0)
					{
						sPrj_UID = ds.Tables["Project"].Rows[0]["Prj_UID"].ToString().Trim();
					}
					ArrayList aArr = new ArrayList();
					aArr.Clear();
					aArr.Add(userID);
					aArr.Add("COMS從業主契約載入資料--" + tmpProjectCode + "(" + IPStr + ")");
					Archnowledge.Pcces.BUDClass.Project PJ1 = new Archnowledge.Pcces.BUDClass.Project(aArr);
					Archnowledge.Pcces.BUDClass.ItemA dbItmA = new Archnowledge.Pcces.BUDClass.ItemA(aArr);
					dbItmA.ps_srckind = "BUD";
					DataTable dt = dbItmA.ListItem("", projectCode);
					if (dt.Rows.Count > 0)
					{
						if (MessageBox.Show(this, "有相同的專案是否覆蓋？", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
						{
							break;
						}
						PJ1.DeleProjComs(projectCode);
					}
					ModifyDB StdCom = new ModifyDB(ProjectCode, aArr);
					string sSQL = "delete CtrPairPcces where projectCode = '" + projectCode + "' and Prj_UID = '" + sPrj_UID + "'";
					StdCom.DBDele(sSQL);
					for (int j = 0; j < ds.Tables["Items"].Rows.Count; j++)
					{
						sSQL = "Insert into CtrPairPcces(Prj_UID,projectCode,uItem_UID,sNo) values('" + sPrj_UID + "','" + projectCode + "','" + ds.Tables["Items"].Rows[j]["Item_UID"].ToString() + "','" + ds.Tables["Items"].Rows[j]["Sno"].ToString() + "')";
						StdCom.DBInse(sSQL);
					}
					StdCom = null;
					PJ1.ps_srckind = "BUD";
					PJ1.ps_comsFlag = true;
					string sImpMessage = PJ1.InputXML(ds, "XM1");
					LoadProjectData();
					Cursor = Cursors.Default;
					MessageBox.Show(this, "資料載入完成!", "完成", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				else
				{
					MessageBox.Show("Error :" + ER.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
			else
			{
				MessageBox.Show("並未整合營建管理資訊系統");
			}
			break;
		case "COMSExpandBudget":
			if (!IsLocked && SysConfig.SysEnableLock2Coms)
			{
				MessageBox.Show("預算需鎖定才可展開明細表！\n請至工具 -> 鎖定，鎖定此預算書。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				if (budgetType != BudgetType.Types.Execution)
				{
					break;
				}
				if (SysConfig.SysComsEnable)
				{
					string msg = "確定要執行『展開明細表』？";
					if (budgetChangeCurrentVersion > 0)
					{
						msg += "\n執行後無法對此次預算變更作【解鎖】的動作！";
					}
					DialogResult dialogResult = MessageBox.Show(msg, "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (dialogResult != DialogResult.Yes)
					{
						break;
					}
					bool Lock2Coms = false;
					if (SysConfig.SysEnableLock2Coms)
					{
						ConfirmExpandDialog CED = new ConfirmExpandDialog();
						CED.ShowDialog();
						if (CED.DialogResult == DialogResult.OK)
						{
							Lock2Coms = true;
						}
					}
					else
					{
						Lock2Coms = true;
					}
					if (Lock2Coms)
					{
						BudProject project = new BudProject();
						DataSet DSProject = project.GetProject(projectCode);
						if (DSProject.Tables[0].Rows.Count > 0 && DSProject.Tables[0].Rows[0]["IsReCal"].ToString() == "Y" && MessageBox.Show(this, "資料有異動過是否要重新總計", "警示", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
						{
							Do_ReCal_All();
						}
						Cursor = Cursors.WaitCursor;
						ComsWebService theComsWebService = new ComsWebService(projectCode);
						ExecResult ER = theComsWebService.ExpandBudgetInCOMS(ForceEnable: false);
						if (ER.ReturnCode != 0)
						{
							MessageBox.Show(ER.Message);
						}
						Cursor = Cursors.Default;
					}
					InitBudgetChange();
				}
				else
				{
					MessageBox.Show("並未整合營建管理資訊系統");
				}
			}
			break;
		case "SingleLockEdit":
			SingleLockEdit();
			break;
		case "LockProject":
			if (FormActionName == PccesFormAction.BUD && !DBClass.ChkAuthority(userID, "F00300050010"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00300050010") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				break;
			}
			Lock();
			InitBudgetChange();
			break;
		case "UnlockProject":
			if (FormActionName == PccesFormAction.BUD && !DBClass.ChkAuthority(userID, "F00300050010"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00300050010") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				break;
			}
			UnLock();
			InitBudgetChange();
			break;
		case "AutoInsertSubtotalItem":
			OpenAutoInsertSubtotalItemForm();
			break;
		case "ChangeToCompanyCode":
			OpenChangeToCompanyCodeWindow();
			break;
		case "AddNewBudgetChangeVersion":
			AddNewBudgetChangeVersion();
			break;
		case "ViewBudgetChangeHistory":
		{
			FormBudgetChangeHistory formBudgetChangeHistory = new FormBudgetChangeHistory();
			formBudgetChangeHistory._ProjectCode = projectCode;
			formBudgetChangeHistory._UserID = userID;
			formBudgetChangeHistory._ProjectName = projectName;
			formBudgetChangeHistory.ShowDialog();
			formBudgetChangeHistory.Dispose();
			formBudgetChangeHistory = null;
			break;
		}
		case "ViewBudgetChangeInfo":
			ViewBudgetChangeInfo();
			break;
		case "DeleteBudgetChangeVersion":
			if (IsLocked)
			{
				MessageBox.Show("預算已鎖定，無法刪除此次變更！\n請至【工具】→【解鎖】，解除鎖定。");
			}
			else
			{
				DeleteBudgetChangeVersion();
			}
			break;
		case "ExportDataToServer":
			DoBudgetExport();
			break;
		case "ShowOnlyChangedItems":
			showOnlyChangedItem = (toolbarsManager.Tools["ShowOnlyChangedItems"] as StateButtonTool).Checked;
			LoadProjectData();
			SetShowOnlyChangedItemToolbarStatus();
			break;
		case "mnuHideAmtZero":
			HideAmountIsZeroItems = (toolbarsManager.Tools["mnuHideAmtZero"] as StateButtonTool).Checked;
			LoadProjectData();
			break;
		case "EditBudgetChangeResponsibility":
			OpenBudgetChangeResponsibilityDialog();
			break;
		case "ReloadFromCostEst":
			ReloadFromCostEst();
			break;
		case "ViewSourceCostQuoteProject":
		{
			FormCostEstProjectList formCostEstProjectList = new FormCostEstProjectList(projectCode, userID, (BudgetType.Types)0);
			formCostEstProjectList.ShowDialog();
			formCostEstProjectList.Dispose();
			formCostEstProjectList = null;
			break;
		}
		case "ExportBudgetCostEstAndQuoteReport":
			ProduceBudgetCostEstAndQuoteReport();
			break;
		case "ExportBudgetDesingChangeReport":
			ProduceBudgetDesingChangeReport();
			break;
		case "DeleteBudItemAZeroQtyItem":
			DeleteBudItemAZeroQtyItem();
			break;
		case "ExportExecutiveBudgetSummaryReport":
		{
			ArrayList Arl3 = new ArrayList();
			Arl3.Clear();
			Arl3.Add(userID);
			Arl3.Add("是否重新總計的旗標" + projectCode);
			DataTable DTEight3 = new DataTable();
			Archnowledge.Pcces.BUDClass.Project dbEight3 = new Archnowledge.Pcces.BUDClass.Project(Arl3);
			dbEight3.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
			DTEight3 = dbEight3.ListItem_eight("", projectCode);
			if (DTEight3.Rows.Count > 0)
			{
				if (DTEight3.Rows[0]["IsReCal"].ToString() == "Y")
				{
					MessageBox.Show("詳細表有變更過，請先重新總計在執行預算變更");
				}
				else
				{
					ProduceExecutiveBudgetSummaryReport();
				}
			}
			break;
		}
		case "ExportExecutiveBudgetDetailReport":
		{
			ArrayList Arl2 = new ArrayList();
			Arl2.Clear();
			Arl2.Add(userID);
			Arl2.Add("是否重新總計的旗標" + projectCode);
			DataTable DTEight2 = new DataTable();
			Archnowledge.Pcces.BUDClass.Project dbEight2 = new Archnowledge.Pcces.BUDClass.Project(Arl2);
			dbEight2.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
			DTEight2 = dbEight2.ListItem_eight("", projectCode);
			if (DTEight2.Rows.Count > 0)
			{
				if (DTEight2.Rows[0]["IsReCal"].ToString() == "Y")
				{
					MessageBox.Show("詳細表有變更過，請先重新總計在執行預算變更");
				}
				else
				{
					ProduceExecutiveBudgetDetailReport();
				}
			}
			break;
		}
		case "ExportExecutiveBudgetChangeInfo":
			BudgetChangeInfoReport();
			break;
		case "ExportComsAccAlertReport":
			ProduceComsAccAlertReport();
			break;
		case "ExportBudgetAccDiffReport":
			ProduceBudgetAccDiffReport();
			break;
		case "mnuSelfExam":
		{
			DataSet DS_SelfExam = theProject.GetProject(projectCode);
			FormBudgetSelfExam FM = new FormBudgetSelfExam();
			FM._FormActionName = FormActionName;
			FM._ProjectCode = projectCode;
			FM._SelfExamValue = DS_SelfExam.Tables[0].Rows[0]["SelfExam"].ToString();
			FM.Owner = this;
			if (FM.ShowDialog() == DialogResult.OK)
			{
				(theProject as BudProject).UpdateSelfExam(projectCode, FM._SelfExamValue);
			}
			break;
		}
		}
	}

	private void DoDBRest()
	{
		if (MessageBox.Show(this, "確定要執行資料庫重整?", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			FM_INFO = new FormSys_G_Info1();
			FM_INFO.TopMost = true;
			FM_INFO._InfoString = "資料庫重整中，請稍候!\n視『專案工項』項目多寡所需時間不同。";
			FM_INFO.Show();
			Application.DoEvents();
			Cursor = Cursors.WaitCursor;
			FixPubCode();
			LoadProjectData();
			FM_INFO.Close();
			FM_INFO.Dispose();
			FM_INFO = null;
			Application.DoEvents();
			MessageBox.Show(this, "資料庫重整完畢。", "完成", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void Clear_Bookmark()
	{
		((ComboBoxTool)toolbarsManager.Tools["BookmarkList"]).ValueList.ValueListItems.Clear();
	}

	private void Clear_Bookmark_Speci()
	{
		FormMrsBase_BookmarkRemove FM_BK_RMV = new FormMrsBase_BookmarkRemove();
		FM_BK_RMV.Owner = this;
		FM_BK_RMV.ShowDialog();
		FM_BK_RMV.Close();
		FM_BK_RMV.Dispose();
		FM_BK_RMV = null;
	}

	private void SaveBookmarkToDB()
	{
		string sSrcKind = CommonMethods.GetActionNameString(FormActionName);
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = userID;
		DBCLS.ExecuteCommand("Delete From Bookmarks Where ProjectCode='" + projectCode + "' And SrcKind='" + sSrcKind + "' ");
		OleDbCommand odCmd1 = new OleDbCommand();
		odCmd1.CommandText = "Insert Into Bookmarks(ProjectCode, SrcKind, Code, ItemNo, CName) values('" + projectCode + "','" + sSrcKind + "',?,?,?)";
		odCmd1.Parameters.Add("P1", OleDbType.VarWChar, 20);
		odCmd1.Parameters.Add("P2", OleDbType.VarWChar, 30);
		odCmd1.Parameters.Add("P3", OleDbType.VarWChar, 200);
		odCmd1.Parameters["P1"].Direction = ParameterDirection.Input;
		odCmd1.Parameters["P2"].Direction = ParameterDirection.Input;
		odCmd1.Parameters["P3"].Direction = ParameterDirection.Input;
		int iCount = ((ComboBoxTool)toolbarsManager.Tools["BookmarkList"]).ValueList.ValueListItems.Count;
		for (int i = 0; i < iCount; i++)
		{
			string ssBookmarkText = ((ComboBoxTool)toolbarsManager.Tools["BookmarkList"]).ValueList.ValueListItems[i].DisplayText;
			int iPos1 = ssBookmarkText.IndexOf(":");
			int iPos2 = ssBookmarkText.IndexOf("\u3000");
			string sV1 = ssBookmarkText.Substring(0, iPos1);
			string sV2 = ssBookmarkText.Substring(iPos1 + 1, iPos2 - iPos1 - 1);
			string sV3 = ssBookmarkText.Substring(iPos2 + 1);
			odCmd1.Parameters["P1"].Value = sV1;
			odCmd1.Parameters["P2"].Value = sV2;
			odCmd1.Parameters["P3"].Value = sV3;
			DBCLS.ExecuteOleDbCommand(odCmd1);
		}
		DBCLS = null;
	}

	private void Add_Bookmark()
	{
		Cursor = Cursors.WaitCursor;
		string sSNo = "";
		string sCName = "";
		string sItemNo = "";
		string sUnit = "";
		string sBookMark = "";
		if (gridBudget.SelectedRowCount == 1)
		{
			Row GridRow = gridBudget.Rows[gridBudget.Row];
			if (GridRow["SNo"] != null)
			{
				sSNo = GridRow["SNo"].ToString().PadRight(20);
			}
			if (GridRow["CName"] != null)
			{
				sCName = GridRow["CName"].ToString().PadRight(30);
			}
			if (GridRow["ItemNo"] != null)
			{
				sItemNo = GridRow["ItemNo"].ToString().PadRight(20);
			}
			if (GridRow["UnitName"] != null)
			{
				sUnit = GridRow["UnitName"].ToString().PadLeft(4);
			}
			if (sSNo != "" && sItemNo != "" && sCName != "")
			{
				sBookMark = sSNo + ":" + sItemNo + "\u3000" + sCName;
				int w = ((ComboBoxTool)toolbarsManager.Tools["BookmarkList"]).ValueList.ValueListItems.Count;
				bool changed = true;
				for (int i = 0; i < w; i++)
				{
					string sbookmarkExe = ((ComboBoxTool)toolbarsManager.Tools["BookmarkList"]).ValueList.ValueListItems[i].ToString().Trim();
					if (sBookMark.Trim() == sbookmarkExe)
					{
						changed = false;
					}
				}
				if (changed)
				{
					((ComboBoxTool)toolbarsManager.Tools["BookmarkList"]).ValueList.ValueListItems.Add(sBookMark);
				}
			}
		}
		else if (gridBudget.SelectedRowCount > 1)
		{
			int iDoneRow = 0;
			int iSelCount = gridBudget.SelectedRowCount;
			for (int i = 1; i < gridBudget.Rows.Count; i++)
			{
				if (gridBudget.Rows[i].Selected)
				{
					iDoneRow++;
					if (gridBudget[i, "SNo"] != null)
					{
						sSNo = gridBudget[i, "SNo"].ToString().PadRight(20);
					}
					if (gridBudget[i, "CName"] != null)
					{
						sCName = gridBudget[i, "CName"].ToString().PadRight(30);
					}
					if (gridBudget[i, "ItemNo"] != null)
					{
						sItemNo = gridBudget[i, "ItemNo"].ToString().PadRight(20);
					}
					if (gridBudget[i, "UnitName"] != null)
					{
						sUnit = gridBudget[i, "UnitName"].ToString().PadLeft(4);
					}
					if (sSNo != "" && sItemNo != "" && sCName != "")
					{
						sBookMark = sSNo + ":" + sItemNo + "\u3000" + sCName;
						int w = ((ComboBoxTool)toolbarsManager.Tools["BookmarkList"]).ValueList.ValueListItems.Count;
						bool changed = true;
						for (int x = 0; x < w; x++)
						{
							string sbookmarkExe = ((ComboBoxTool)toolbarsManager.Tools["BookmarkList"]).ValueList.ValueListItems[x].ToString().Trim();
							if (sBookMark.Trim() == sbookmarkExe)
							{
								changed = false;
							}
						}
						if (changed)
						{
							((ComboBoxTool)toolbarsManager.Tools["BookmarkList"]).ValueList.ValueListItems.Add(sBookMark);
						}
					}
				}
				if (iDoneRow >= iSelCount)
				{
					break;
				}
			}
		}
		Cursor = Cursors.Default;
	}

	private void Add_ParentBookmark()
	{
		DataSet dsparent = dsParentProjMrsA;
		if (MessageBox.Show(this, "是否只留父項查詢書籤?", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			Clear_Bookmark();
		}
		if (dsparent.Tables[0].Rows.Count == 1)
		{
			((ComboBoxTool)toolbarsManager.Tools["KeywordList"]).Text = dsparent.Tables[0].Rows[0][3].ToString();
			Do_ToolBarFind();
			Add_Bookmark();
		}
		else if (dsparent.Tables[0].Rows.Count > 1)
		{
			for (int i = 0; i < dsparent.Tables[0].Rows.Count; i++)
			{
				((ComboBoxTool)toolbarsManager.Tools["KeywordList"]).Text = dsparent.Tables[0].Rows[i][3].ToString();
				Do_ToolBarFind();
				Add_Bookmark();
			}
		}
		Cursor = Cursors.Default;
	}

	private void Do_ClearCost()
	{
		if (MessageBox.Show(this, "確定要清空詳細表單價?", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			DBClass DBCLS = new DBClass();
			DBCLS._FS_UserID = userID;
			string sSrcKind = "BUD";
			try
			{
				sSrcKind = ((FormActionName != PccesFormAction.BUD) ? "BID" : "BUD");
				string sSQL = "Update " + sSrcKind + "ItemA Set Cost = 0 Where ProjectCode='" + projectCode + "' " + '\r' + "Update " + sSrcKind + "ProjMrsA Set Cost = 0 Where ProjectCode='" + projectCode + "' " + '\r' + "Update " + sSrcKind + "ProjMrsB Set Cost = 0 Where ProjectCode='" + projectCode + "' " + '\r';
				DBCLS.ExecuteCommand(sSQL);
				LoadProjectData();
				MessageBox.Show(this, "清空詳細表單價完成。", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudget.cs--Do_ClearCost" + ex.Message);
				LoadProjectData();
				MessageBox.Show(this, "FormBudget::Do_ClearCost()#1 清空詳細表單價失敗。\n" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			DBCLS = null;
		}
	}

	private void Do_CostLock(string SetFlag)
	{
		toolbarsManager.BeginUpdate();
		FM_INFO = new FormSys_G_Info1();
		FM_INFO._InfoString = "項目鎖定處理中，請稍候! ";
		FM_INFO.Show();
		Application.DoEvents();
		Cursor = Cursors.WaitCursor;
		gridBudget.Enabled = false;
		Application.DoEvents();
		string IPStr = CommonMethods.GetIPAddress();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(userID);
		aArr.Add("預算書項目編輯後存檔之鎖定異動--" + projectCode + "(" + IPStr + ")");
		Archnowledge.Pcces.BUDClass.ItemA ItemACom = new Archnowledge.Pcces.BUDClass.ItemA(aArr);
		ItemACom.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
		ItemACom.ps_projectCode = projectCode;
		for (int i = 1; i < gridBudget.Rows.Count && gridBudget.Rows[i]["PrintNo"] != null && !(gridBudget.Rows[i]["PrintNo"].ToString().Trim() == ""); i++)
		{
			if (gridBudget.Rows[i].Selected)
			{
				gridBudget[i, "LockCost"] = SetFlag == "1";
				ItemACom.LockCost(projectCode, gridBudget[i, "PrintNo"].ToString().Trim(), SetFlag, "LockCost");
				if (gridBudget[i, "Kind"].ToString() == "B")
				{
					ReloadGridAtRootSno(ArchConvert.Obj2Int(gridBudget[i, "sNo"]));
				}
				else
				{
					Reload_OneRow(ArchConvert.Obj2Int(gridBudget.Rows[i]["Sno"]), i, RangeUpdate: false);
				}
				Application.DoEvents();
			}
		}
		FM_INFO.Close();
		FM_INFO.Dispose();
		FM_INFO = null;
		gridBudget.Enabled = true;
		toolbarsManager.Enabled = true;
		toolbarsManager.EndUpdate();
		Cursor = Cursors.Default;
	}

	private void Execute_BidSet()
	{
		FormBudgetBidSet FM_PG_BS = new FormBudgetBidSet();
		FM_PG_BS._UserID = userID;
		FM_PG_BS._ProjectCode = projectCode;
		FM_PG_BS._ActionName = FormActionName;
		FM_PG_BS.Owner = this;
		FM_PG_BS.ShowDialog();
		FM_PG_BS.Close();
		FM_PG_BS.Dispose();
		FM_PG_BS = null;
	}

	private void Execute_IssurNameSet()
	{
		FormBudgetSetSurName FM_PG_BS = new FormBudgetSetSurName();
		FM_PG_BS._ProjectCode = projectCode;
		FM_PG_BS.Owner = this;
		FM_PG_BS.ShowDialog();
		FM_PG_BS.Close();
		FM_PG_BS.Dispose();
		FM_PG_BS = null;
	}

	private void Execute_OptionMain()
	{
		FormBDGT_OptionMain FM_OP = new FormBDGT_OptionMain();
		FM_OP._UserID = userID;
		FM_OP._ProjectCode = projectCode;
		FM_OP._ActionName = CommonMethods.GetActionNameString(FormActionName);
		FM_OP.Owner = this;
		if (FM_OP.ShowDialog() == DialogResult.OK)
		{
			LoadIniSetting();
		}
		FM_OP.Close();
		FM_OP.Dispose();
		FM_OP = null;
	}

	private void Execute_SetMain()
	{
		FormBDGT_SetMain FM_OP = new FormBDGT_SetMain();
		FM_OP._UserID = userID;
		FM_OP._ProjectCode = projectCode;
		FM_OP._ActionName = CommonMethods.GetActionNameString(FormActionName);
		FM_OP.Owner = this;
		FM_OP.ShowDialog();
		FM_OP.Close();
		FM_OP.Dispose();
		FM_OP = null;
		SetupRestoreSnapshotListCNT();
		SetupRestoreSnapshotList();
	}

	private void Execute_CostStructure()
	{
		string sPrintNo = "";
		try
		{
			sPrintNo = gridBudget[gridBudget.Row, "PrintNo"].ToString().Trim();
		}
		catch
		{
		}
		FormBudgetCostStructurePicker FM_CP = new FormBudgetCostStructurePicker();
		FM_CP._UserID = userID;
		FM_CP._ProjectCode = projectCode;
		FM_CP._budPrintNo = sPrintNo;
		FM_CP._ActionName = CommonMethods.GetActionNameString(FormActionName);
		FM_CP.Owner = this;
		if (FM_CP.ShowDialog() == DialogResult.OK)
		{
			theProject.ReArrangePrintNo(projectCode, !IsEditItemNo);
			LoadProjectData();
			Do_ItemReArrange(isSilence: true);
		}
		FM_CP.Close();
		FM_CP.Dispose();
		FM_CP = null;
	}

	private void Execute_CostProperty()
	{
		string sCostUID = "";
		string sType = "";
		int sNo = -1;
		try
		{
			sCostUID = gridBudget[gridBudget.Row, "CostUID"].ToString().Trim();
			sType = gridBudget[gridBudget.Row, "TypeID"].ToString().Trim();
			sNo = Convert.ToInt32(gridBudget[gridBudget.Row, "sNo"]);
		}
		catch
		{
		}
		FormBudgetCostProperty FM_CP = new FormBudgetCostProperty();
		FM_CP._UserID = userID;
		FM_CP._ProjectCode = projectCode;
		FM_CP._CostUID = sCostUID;
		FM_CP._CostType = sType;
		FM_CP._sNO = sNo;
		FM_CP._ActionName = FormActionName;
		FM_CP.Owner = this;
		if (FM_CP.ShowDialog() == DialogResult.OK)
		{
			LoadProjectData();
		}
		FM_CP.Close();
		FM_CP.Dispose();
		FM_CP = null;
	}

	private void Execute_BudIstemplate()
	{
		FormBDGT_TemplateClass FM_Template = new FormBDGT_TemplateClass();
		FM_Template._UserID = userID;
		FM_Template._ProjectCode = projectCode;
		FM_Template.Owner = this;
		if (FM_Template.ShowDialog() == DialogResult.OK)
		{
			LoadProjectData();
		}
		FM_Template.Close();
		FM_Template.Dispose();
		FM_Template = null;
	}

	private void Execute_Backup()
	{
		FormBudgetCheckOut_Wzd FM_CHKOUT = new FormBudgetCheckOut_Wzd();
		FM_CHKOUT._ProjectCode = projectCode;
		FM_CHKOUT._UserID = userID;
		if (GetCurrentBDGT_Type().ToUpper() == "CNT")
		{
			FM_CHKOUT._IsContract = true;
		}
		FM_CHKOUT._ActionName = FormActionName;
		FM_CHKOUT.ShowDialog();
		FM_CHKOUT.Close();
		FM_CHKOUT.Dispose();
		FM_CHKOUT = null;
	}

	private void Execute_Restore()
	{
		FormBudgetCheckIn_Wzd FM_CHKIN = new FormBudgetCheckIn_Wzd();
		FM_CHKIN._ProjectCode = projectCode;
		FM_CHKIN._ActionName = FormActionName;
		FM_CHKIN._UserID = userID;
		if (FM_CHKIN.ShowDialog() == DialogResult.OK)
		{
			LoadProjectData();
		}
		FM_CHKIN.Close();
		FM_CHKIN.Dispose();
		FM_CHKIN = null;
		GetCurrentBDGT_Type();
	}

	private void Execute_PageBreak()
	{
		FormBudgetPageBreak FM_PG_BK = new FormBudgetPageBreak();
		FM_PG_BK._UserID = userID;
		FM_PG_BK._ProjectCode = projectCode;
		FM_PG_BK._ActionName = FormActionName;
		FM_PG_BK.Owner = this;
		FM_PG_BK.ShowDialog();
		FM_PG_BK.Close();
		FM_PG_BK = null;
	}

	private void Execute_PCalsCustomVar()
	{
		FormBudgetPCalsCustomVar FM_PCLS_CVAR = new FormBudgetPCalsCustomVar();
		FM_PCLS_CVAR._UserID = userID;
		FM_PCLS_CVAR._ProjectCode = projectCode;
		FM_PCLS_CVAR._ActionName = FormActionName;
		FM_PCLS_CVAR.Owner = this;
		FM_PCLS_CVAR.ShowDialog();
		FM_PCLS_CVAR.Close();
		FM_PCLS_CVAR.Dispose();
		FM_PCLS_CVAR = null;
		CheckIsReCal("Y");
	}

	private void Do_3rdParty()
	{
		FormBudgetThirdParty FM3 = new FormBudgetThirdParty();
		FM3._CallFormName = base.Name;
		FM3._UserID = userID;
		FM3.ShowDialog(this);
		FM3.Dispose();
		FM3 = null;
	}

	private void Do_Edit_Paste()
	{
		if (IsLocked)
		{
			MessageBox.Show("專案已鎖定，不可執行貼上");
			return;
		}
		Cursor = Cursors.WaitCursor;
		if (dtClipboard.Rows.Count > 0)
		{
			CheckIsReCal("Y");
			ItemPasteFromProjectItemPick(dtClipboard);
		}
	}

	private void CutItems()
	{
		if (!CheckCOMSCanDelete())
		{
			return;
		}
		if (IsLocked)
		{
			MessageBox.Show("專案已鎖定，不可執行剪下");
			return;
		}
		if (budgetChangeCurrentVersion > 0)
		{
			foreach (Row gridRow in (IEnumerable)gridBudget.Rows.Selected)
			{
				if (ArchConvert.Obj2Bool(gridRow["Lock"]))
				{
					MessageBox.Show("剪下不可包含前一版預算書的項目！請重新選取。");
					return;
				}
			}
		}
		Cursor = Cursors.WaitCursor;
		int CutCount = 0;
		int LastSelRow = gridBudget.RowSel;
		if (Do_Edit_Copy())
		{
			ExecResult ER = new ExecResult();
			int StartIndex = 0;
			for (int i = gridBudget.Rows.Count - 1; i >= 1; i--)
			{
				if (gridBudget.Rows[i].Selected)
				{
					if (i % 200 == 0)
					{
						Application.DoEvents();
						Cursor = Cursors.WaitCursor;
					}
					Row GridRow = gridBudget.Rows[i];
					int Sno = ArchConvert.Obj2Int(GridRow["sNo"]);
					ER = theItemA.DeleteItemBySno(projectCode, Sno, updateItemNo: true);
					if (ER.ReturnCode == 0)
					{
						CutCount++;
					}
					StartIndex = i;
				}
			}
			if (CutCount > 0)
			{
				int RootSno = 0;
				if (CutCount == 1)
				{
					RootSno = ArchConvert.Obj2Int(gridBudget[LastSelRow, "ParentSno"]);
				}
				theProject.ReArrangePrintNo(projectCode, RootSno, !IsEditItemNo);
				ReloadGridAtRootSno(RootSno);
			}
			CheckIsReCal("Y");
		}
		Cursor = Cursors.Default;
	}

	public bool Do_Ana_Copy(DataTable dtAnalysis)
	{
		if (IsLocked)
		{
			MessageBox.Show("專案已鎖定，不可執行複製");
			return false;
		}
		dtClipboard.Clear();
		ArrayList AR = new ArrayList();
		for (int i = 0; i < dtAnalysis.Rows.Count; i++)
		{
			DataRow DR_Clip = dtClipboard.NewRow();
			DR_Clip["ProjectCode"] = projectCode;
			DR_Clip["ItemNo"] = "";
			DR_Clip["CName"] = dtAnalysis.Rows[i]["CName"];
			DR_Clip["UnitName"] = dtAnalysis.Rows[i]["UnitName"];
			DR_Clip["Qty"] = dtAnalysis.Rows[i]["Qty"];
			DR_Clip["LockCost"] = false;
			DR_Clip["Cost"] = dtAnalysis.Rows[i]["Cost"];
			DR_Clip["Amount"] = dtAnalysis.Rows[i]["Amount"];
			DR_Clip["PccesCode"] = dtAnalysis.Rows[i]["PccesCode"];
			DR_Clip["Memo"] = dtAnalysis.Rows[i]["Memo"];
			DR_Clip["EName"] = dtAnalysis.Rows[i]["EName"];
			DR_Clip["EUnit"] = dtAnalysis.Rows[i]["EUnit"];
			DR_Clip["Level"] = 2;
			DR_Clip["Kind"] = dtAnalysis.Rows[i]["Kind"];
			DR_Clip["Analysis"] = dtAnalysis.Rows[i]["Analysis"];
			DR_Clip["SNo"] = -1;
			DR_Clip["Formula"] = "";
			DR_Clip["PrintNo"] = "";
			DR_Clip["OldPrintNo"] = "";
			DR_Clip["PubCode"] = dtAnalysis.Rows[i]["PubCode"];
			DR_Clip["IsShared"] = "";
			DR_Clip["Account"] = "";
			DR_Clip["Pwrset"] = "";
			DR_Clip["IsCollaspse"] = "";
			DR_Clip["surName"] = "";
			DR_Clip["DBName"] = currentDBName;
			dtClipboard.Rows.Add(DR_Clip);
			AR.Add(2);
		}
		bool IsValid = true;
		string sMessCheck = "";
		if (AR.Count > 0)
		{
			int FirstIndex = PubTools.Str2Int(AR[0]);
			int SeqIndex = PubTools.Str2Int(AR[0]);
			for (int i = 0; i < AR.Count; i++)
			{
				if (PubTools.Str2Int(AR[i]) < FirstIndex)
				{
					IsValid = false;
					sMessCheck = "挑選的項目階層, 不可以有小於第一項。";
					break;
				}
				if (Math.Abs(SeqIndex - PubTools.Str2Int(AR[i])) >= 2)
				{
					IsValid = false;
					sMessCheck = "不可以跳階。";
					break;
				}
				dtClipboard.Rows[i]["Level"] = PubTools.Str2Int(AR[i]) - FirstIndex;
				SeqIndex = PubTools.Str2Int(AR[i]);
			}
		}
		else
		{
			IsValid = false;
			sMessCheck = "並沒有選到任何有效的資料。";
		}
		if (!IsValid)
		{
			dtClipboard.Clear();
			toolbarsManager.Tools["Paste"].SharedProps.Enabled = false;
			toolbarsManager.Tools["Cut"].SharedProps.Enabled = true;
			toolbarsManager.Tools["Copy"].SharedProps.Enabled = true;
			MessageBox.Show(this, sMessCheck, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else
		{
			toolbarsManager.Tools["Paste"].SharedProps.Enabled = true;
			toolbarsManager.Tools["Cut"].SharedProps.Enabled = true;
			toolbarsManager.Tools["Copy"].SharedProps.Enabled = true;
		}
		return IsValid;
	}

	private bool Do_Edit_Copy()
	{
		if (IsLocked)
		{
			MessageBox.Show("專案已鎖定，不可執行複製");
			return false;
		}
		Cursor = Cursors.WaitCursor;
		dtClipboard.Clear();
		ArrayList AR = new ArrayList();
		for (int i = 1; i < gridBudget.Rows.Count; i++)
		{
			if (gridBudget.Rows[i].Selected && gridBudget[i, "SNo"] != null)
			{
				DataRow DR_Clip = dtClipboard.NewRow();
				DR_Clip["ProjectCode"] = projectCode;
				DR_Clip["ItemNo"] = gridBudget[i, "ItemNo"];
				DR_Clip["CName"] = gridBudget[i, "CName"];
				DR_Clip["UnitName"] = gridBudget[i, "UnitName"];
				DR_Clip["Qty"] = gridBudget[i, "Qty"];
				DR_Clip["LockCost"] = gridBudget[i, "LockCost"];
				DR_Clip["Cost"] = gridBudget[i, "Cost"];
				DR_Clip["Amount"] = gridBudget[i, "Amount"];
				DR_Clip["PccesCode"] = gridBudget[i, "PccesCode"];
				DR_Clip["Memo"] = gridBudget[i, "Memo"];
				DR_Clip["EName"] = gridBudget[i, "EName"];
				DR_Clip["EUnit"] = gridBudget[i, "EUnit"];
				DR_Clip["Level"] = gridBudget.Rows[i].Node.Level;
				DR_Clip["Kind"] = gridBudget[i, "Kind"];
				DR_Clip["Analysis"] = gridBudget[i, "Analysis"];
				DR_Clip["SNo"] = gridBudget[i, "SNo"];
				DR_Clip["Formula"] = gridBudget[i, "Formula"];
				DR_Clip["PrintNo"] = "";
				DR_Clip["OldPrintNo"] = "";
				DR_Clip["PubCode"] = ((gridBudget[i, "PubCode"] == null) ? ((object)0) : gridBudget[i, "PubCode"]);
				DR_Clip["IsShared"] = gridBudget[i, "IsShared"];
				DR_Clip["Account"] = gridBudget[i, "Account"];
				DR_Clip["Pwrset"] = gridBudget[i, "Pwrset"];
				DR_Clip["IsCollaspse"] = gridBudget[i, "IsCollaspse"];
				DR_Clip["surName"] = gridBudget[i, "surName"];
				if (gridBudget[i, "fixPrice"] != null && (bool)gridBudget[i, "fixPrice"])
				{
					DR_Clip["fixPrice"] = "1";
				}
				DR_Clip["DBName"] = currentDBName;
				dtClipboard.Rows.Add(DR_Clip);
				AR.Add(gridBudget.Rows[i].Node.Level);
			}
		}
		bool IsValid = true;
		string sMessCheck = "";
		if (AR.Count > 0)
		{
			int FirstIndex = PubTools.Str2Int(AR[0]);
			int SeqIndex = PubTools.Str2Int(AR[0]);
			for (int i = 0; i < AR.Count; i++)
			{
				if (PubTools.Str2Int(AR[i]) < FirstIndex)
				{
					IsValid = false;
					sMessCheck = "挑選的項目階層, 不可以有小於第一項。";
					break;
				}
				if (Math.Abs(SeqIndex - PubTools.Str2Int(AR[i])) >= 2)
				{
					IsValid = false;
					sMessCheck = "不可以跳階。";
					break;
				}
				dtClipboard.Rows[i]["Level"] = PubTools.Str2Int(AR[i]) - FirstIndex;
				SeqIndex = PubTools.Str2Int(AR[i]);
			}
		}
		else
		{
			IsValid = false;
			sMessCheck = "並沒有選到任何有效的資料。";
		}
		if (!IsValid)
		{
			dtClipboard.Clear();
			toolbarsManager.Tools["Paste"].SharedProps.Enabled = false;
			toolbarsManager.Tools["Cut"].SharedProps.Enabled = true;
			toolbarsManager.Tools["Copy"].SharedProps.Enabled = true;
			MessageBox.Show(this, sMessCheck, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else
		{
			toolbarsManager.Tools["Paste"].SharedProps.Enabled = true;
			toolbarsManager.Tools["Cut"].SharedProps.Enabled = true;
			toolbarsManager.Tools["Copy"].SharedProps.Enabled = true;
		}
		Cursor = Cursors.Default;
		return IsValid;
	}

	private void Execute_Calculator()
	{
		ShellExecute SHExe = new ShellExecute();
		SHExe.OwnerHandle = base.Handle;
		SHExe.Path = "Calc.exe";
		SHExe.Execute();
		SHExe = null;
	}

	private void Do_Export()
	{
		string sFilter = "Microsoft Excel 97/2000 files (*.xls)|*.xls";
		saveFileDialog1.Filter = sFilter;
		saveFileDialog1.RestoreDirectory = true;
		saveFileDialog1.FileName = projectCode + "_詳細表";
		if (saveFileDialog1.ShowDialog() == DialogResult.OK)
		{
			gridBudget.SaveExcel(saveFileDialog1.FileName, projectCode, FileFlags.IncludeFixedCells);
		}
	}

	private void Do_CancelShare()
	{
		string IPStr = CommonMethods.GetIPAddress();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(userID);
		aArr.Add("取消攤提" + projectCode + "(" + IPStr + ")");
		if (dbItemA != null)
		{
			dbItemA = null;
		}
		if (dbItemA == null)
		{
			dbItemA = new Archnowledge.Pcces.BUDClass.ItemA(aArr);
		}
		dbItemA.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
		dbItemA.ps_projectCode = projectCode;
		dbItemA.ps_sNo = gridBudget[gridBudget.Row, "SNo"].ToString();
		dbItemA.ps_share = "";
		dbItemA.UpdItem();
		Reload_OneRow(ArchConvert.Obj2Int(gridBudget[gridBudget.Row, "SNo"]), gridBudget.RowSel, RangeUpdate: false);
		BudProject theProject = null;
		theProject = new BudProject("Pcces");
		theProject.UpdateShareVDF1(projectCode, 0m, 0);
	}

	private void Do_ToolBarFind()
	{
		if (FormActionName == PccesFormAction.BUD && !DBClass.ChkAuthority(userID, "F00300020003"))
		{
			MessageBox.Show(this, DBClass.GetFuncName("F00300020003") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else if (FormActionName == PccesFormAction.BID && !DBClass.ChkAuthority(userID, "F00400020003"))
		{
			MessageBox.Show(this, DBClass.GetFuncName("F00400020003") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else
		{
			if (gridBudget.Rows.Count <= 1)
			{
				return;
			}
			int iStart = gridBudget.Row + 1;
			string sSearchText = ((ComboBoxTool)toolbarsManager.Tools["KeywordList"]).Text.Trim();
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
				iStart = gridBudget.Row + 1;
			}
			if (sSearchText.Trim() == "")
			{
				return;
			}
			for (int i = iStart; i < gridBudget.Rows.Count; i++)
			{
				for (int j = 1; j < gridBudget.Cols.Count; j++)
				{
					if (gridBudget[i, j] == null || gridBudget[i, j].ToString().IndexOf(sSearchText) <= -1)
					{
						continue;
					}
					gridBudget.Row = i;
					gridBudget.Select();
					gridBudget.TopRow = i;
					int iFondCount = 0;
					int iListCount = ((ComboBoxTool)toolbarsManager.Tools["KeywordList"]).ValueList.ValueListItems.Count;
					for (int k = 0; k < iListCount; k++)
					{
						if (((ComboBoxTool)toolbarsManager.Tools["KeywordList"]).ValueList.ValueListItems[k].DisplayText.Trim() == sSearchText.Trim())
						{
							iFondCount++;
						}
					}
					if (iFondCount == 0)
					{
						((ComboBoxTool)toolbarsManager.Tools["KeywordList"]).ValueList.ValueListItems.Add(sSearchText, sSearchText);
					}
					return;
				}
			}
		}
	}

	private void Execute_Option()
	{
		FormBudgetItemNo FM_BDGT_ITMNO = new FormBudgetItemNo();
		FM_BDGT_ITMNO._ActionName = FormActionName;
		FM_BDGT_ITMNO._UserID = userID;
		FM_BDGT_ITMNO._ProjectCode = projectCode;
		if (FM_BDGT_ITMNO.ShowDialog(this) == DialogResult.OK)
		{
			theItemNoSettingManager.PrepareAssemItemNo();
		}
		FM_BDGT_ITMNO.Dispose();
		FM_BDGT_ITMNO = null;
	}

	private void Execute_ItemNoSetting()
	{
		FormBDGT_ItemSetMaintain FM_ITMSET_MNTN = new FormBDGT_ItemSetMaintain();
		FM_ITMSET_MNTN.ShowDialog(this);
		FM_ITMSET_MNTN.Close();
		FM_ITMSET_MNTN.Dispose();
		FM_ITMSET_MNTN = null;
	}

	private void DoDeleteThisBDGT()
	{
		string sQuest = ((FormActionName == PccesFormAction.BUD) ? "確定刪除此預算書 ?" : "確定刪除此投標單 ?");
		if (MessageBox.Show(this, sQuest, "刪除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
		{
			return;
		}
		Archnowledge.Pcces.DomainModule.LogicalBase.Project project = ((FormActionName != PccesFormAction.BUD) ? ((Archnowledge.Pcces.DomainModule.LogicalBase.Project)new BidProject()) : ((Archnowledge.Pcces.DomainModule.LogicalBase.Project)new BudProject()));
		ExecResult ER = project.RemoveProject(projectCode);
		if (ER.ReturnCode == 0)
		{
			project = ((FormActionName != PccesFormAction.BUD) ? ((Archnowledge.Pcces.DomainModule.LogicalBase.Project)new BudProject()) : ((Archnowledge.Pcces.DomainModule.LogicalBase.Project)new BidProject()));
			if (!project.ProjectCodeExists(projectCode))
			{
				AddOnDownLoad addOnDownLoad = new AddOnDownLoad();
				ER = addOnDownLoad.RemoveAddOnDownloadFilesByProjectCode(projectCode, userID);
			}
		}
		if (ER.ReturnCode != 0)
		{
			MessageBox.Show(this, "刪除專案失敗！" + ER.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		(base.ParentForm as frmPccesMain).LeftPanel.Width = 160;
		Close();
	}

	private void Do_ItemReArrange(bool isSilence)
	{
		string sQuestion = "確定執行項次重整嗎？";
		if (isSilence || MessageBox.Show(this, sQuestion, "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			gridBudget.Enabled = false;
			FM_INFO = new FormSys_G_Info1();
			FM_INFO.TopMost = true;
			FM_INFO._InfoString = "項次重整中，請稍候！\n視『詳細表』項目多寡所需時間不同。";
			FM_INFO.Show();
			FM_INFO._MaxValue = gridBudget.Rows.Count;
			Application.DoEvents();
			Cursor = Cursors.WaitCursor;
			lock (this)
			{
				theProject.ReArrangePrintNo(projectCode, UpdateItemNo: true);
				LoadProjectData();
				Application.DoEvents();
				Cursor = Cursors.Default;
				gridBudget.Enabled = true;
				gridBudget.Refresh();
				FM_INFO.Close();
				FM_INFO.Dispose();
				FM_INFO = null;
				Application.DoEvents();
			}
			if (!isSilence)
			{
				MessageBox.Show(this, "項次重整完畢！", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private void Do_Combine()
	{
		FormBudgetCombine FM_BDGT_CMB = new FormBudgetCombine();
		FM_BDGT_CMB._UserID = userID;
		FM_BDGT_CMB._ActionName = FormActionName;
		FM_BDGT_CMB._ProjectCode = projectCode;
		if (FM_BDGT_CMB.ShowDialog(this) == DialogResult.OK)
		{
			LoadProjectData();
		}
		FM_BDGT_CMB.Dispose();
		FM_BDGT_CMB = null;
	}

	private void Do_CombineBid()
	{
		FormBudgetCombineBid FM_BDGT_CMB_BID = new FormBudgetCombineBid();
		FM_BDGT_CMB_BID._UserID = userID;
		FM_BDGT_CMB_BID._ProjectCode = projectCode;
		if (FM_BDGT_CMB_BID.ShowDialog(this) == DialogResult.OK)
		{
			LoadProjectData();
			CheckIsReCal("Y");
		}
		FM_BDGT_CMB_BID.Dispose();
		FM_BDGT_CMB_BID = null;
	}

	private void Do_UseBreakdown()
	{
		string sQStr = "嚴重警告：\n本專案 『所有單價分析』 將被清空，並轉入【工項基本資料庫】取代。\n\n此動作是將所有單價分析項子項的【數量】及【工項結構】從工項基本資料庫引用過來\n\n※注意：將覆蓋單價分析所有細項※\n\n所需花費時間會較久，確定要執行嗎?";
		if (MessageBox.Show(this, sQStr, "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
		{
			FixPubCode();
			string IPStr = CommonMethods.GetIPAddress();
			ArrayList aArr = new ArrayList();
			aArr.Clear();
			aArr.Add(userID);
			aArr.Add("引用單價分析--" + projectCode + "(" + IPStr + ")");
			Archnowledge.Pcces.BUDClass.ItemA ItemACom = new Archnowledge.Pcces.BUDClass.ItemA(aArr);
			ItemACom.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
			ItemACom.ps_projectCode = projectCode;
			Archnowledge.Pcces.BUDClass.MrsBaseB mrscom = new Archnowledge.Pcces.BUDClass.MrsBaseB(aArr);
			mrscom.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
			mrscom.ReAnalysis(projectCode);
			mrscom = null;
			PubTools.WriteRoughlyLog(aArr);
			Th_ReCal_All(Auto: false);
			LoadProjectData();
			ItemACom = null;
		}
	}

	private void Do_UseSelBreakdown()
	{
		string sQStr = "嚴重警告：\n選取項的 『單價分析』 將被清空，並轉入【工項基本資料庫】取代。\n\n此動作是將選取項的單價分析項子項的【數量】及【工項結構】從工項基本資料庫引用過來\n\n※注意：將覆蓋單價分析所有細項※\n\n所需花費時間會較久，確定要執行嗎?";
		if (MessageBox.Show(this, sQStr, "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
		{
			return;
		}
		DataTable DT_AfterCorrect = FixPubCode(GetSelectedWorkItems());
		string IPStr = CommonMethods.GetIPAddress();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(userID);
		aArr.Add("引用單價分析--" + projectCode + "(" + IPStr + ")");
		ReSet2Mrs RST_2_MRS = new ReSet2Mrs(aArr);
		RST_2_MRS.ls_srckind = CommonMethods.GetActionNameString(FormActionName);
		RST_2_MRS.ls_projectcode = projectCode;
		for (int i = 0; i < DT_AfterCorrect.Rows.Count; i++)
		{
			if (!(DT_AfterCorrect.Rows[i]["resCode"].ToString().Trim() == ""))
			{
				RST_2_MRS.ls_apubCode = DT_AfterCorrect.Rows[i]["resCode"].ToString().Trim();
				RST_2_MRS.Mrs2Proj();
			}
		}
		RST_2_MRS = null;
		PubTools.WriteRoughlyLog(aArr);
		Th_ReCal_All(Auto: false);
		LoadProjectData();
	}

	private void Do_UseItemPrice()
	{
		string sQStr = "嚴重警告：\n本專案 『所有工項單價』 被清空，並轉入【工項基本資料庫】取代。\n\n此動作是將所有工項的【單價】從【工項基本資料庫】引用過來，並不會變更任何數量或是單價分析結構\n\n所需花費時間會較久，確定要執行嗎?";
		if (MessageBox.Show(this, sQStr, "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
		{
			FixPubCode();
			string IPStr = CommonMethods.GetIPAddress();
			ArrayList aArr = new ArrayList();
			aArr.Clear();
			aArr.Add(userID);
			aArr.Add("引用工料價--" + projectCode + "(" + IPStr + ")");
			Archnowledge.Pcces.BUDClass.ItemA ItemACom = new Archnowledge.Pcces.BUDClass.ItemA(aArr);
			ItemACom.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
			ItemACom.ps_projectCode = projectCode;
			ItemACom.ReCost(projectCode);
			PubTools.WriteRoughlyLog(aArr);
			CheckIsReCal("Y");
			Th_ReCal_All(Auto: false);
			LoadProjectData();
			ItemACom = null;
			aArr = null;
		}
	}

	private void Do_UseSelItemPrice()
	{
		string sQStr = "嚴重警告：\n選取項的 『工項單價』 會被清空，並轉入【工項基本資料庫單價】取代。\n\n此動作是將選取項的【單價】從【工項基本資料庫】引用過來，並不會變更任何數量或是單價分析結構\n\n所需花費時間會較久，確定要執行嗎?";
		if (MessageBox.Show(this, sQStr, "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
		{
			return;
		}
		DataTable DT_AfterCorrect = FixPubCode(GetSelectedWorkItems());
		string IPStr = CommonMethods.GetIPAddress();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(userID);
		aArr.Add("引用工料價--" + projectCode + "(" + IPStr + ")");
		Archnowledge.Pcces.BUDClass.ItemA ItemACom = new Archnowledge.Pcces.BUDClass.ItemA(aArr);
		ItemACom.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
		ItemACom.ps_projectCode = projectCode;
		for (int i = 0; i < DT_AfterCorrect.Rows.Count; i++)
		{
			if (!(DT_AfterCorrect.Rows[i]["resCode"].ToString().Trim() == ""))
			{
				ItemACom.ReCost(projectCode, DT_AfterCorrect.Rows[i]["resCode"].ToString().Trim());
			}
		}
		PubTools.WriteRoughlyLog(aArr);
		Th_ReCal_All(Auto: false);
		LoadProjectData();
		ItemACom = null;
		aArr = null;
	}

	private void Do_NameReArrange()
	{
		string sQStr = "嚴重警告：本專案所有工項的中、英文名稱將被清空，並轉入【工項基本資料庫】取代。";
		if (MessageBox.Show(this, sQStr, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			string IPStr = CommonMethods.GetIPAddress();
			ArrayList aArr = new ArrayList();
			aArr.Clear();
			aArr.Add(userID);
			aArr.Add("名稱重整--" + projectCode + "(" + IPStr + ")");
			Archnowledge.Pcces.BUDClass.ItemA ItemACom = new Archnowledge.Pcces.BUDClass.ItemA(aArr);
			ItemACom.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
			ItemACom.ps_projectCode = projectCode;
			ItemACom.ReleMrs(projectCode);
			PubTools.WriteRoughlyLog(aArr);
			LoadProjectData();
			ItemACom = null;
			aArr = null;
		}
	}

	public void Do_BudBidFileDigital()
	{
		if (FormActionName == PccesFormAction.BUD && !DBClass.ChkAuthority(userID, "F00300010002"))
		{
			MessageBox.Show(this, DBClass.GetFuncName("F00300010002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		if (FormActionName == PccesFormAction.BID && !DBClass.ChkAuthority(userID, "F00400010002"))
		{
			MessageBox.Show(this, DBClass.GetFuncName("F00400010002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		if (GetCurrentBDGT_Type() == "CNT")
		{
			if (!IsCheckCNT())
			{
				MessageBox.Show(this, "請先確認契約已編修完成，鎖定契約後才可匯出電子檔!!\n[工具]-->[鎖定]", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			ExecuteCopyToTmpCNT("Y");
		}
		Do_FileDigital("");
	}

	private void Do_FileDigital(string sFLAG)
	{
		if (!CheckKind())
		{
			return;
		}
		double org_Amount = GetItemAAmount();
		if (org_Amount == 0.0)
		{
			string ssWarning;
			if (FormActionName != PccesFormAction.BUD)
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
		OpenExportExcelDialog(sFLAG, IsPreview: false);
	}

	private void OpenExportExcelDialog(string Flag, bool IsPreview)
	{
		DataSet dsProject = theProject.GetProject(projectCode);
		DataTable dtProject = dsProject.Tables[0];
		string mainCode = dtProject.Rows[0]["mainCode"].ToString().Trim();
		string mainCName = dtProject.Rows[0]["mainCName"].ToString().Trim();
		if (mainCode == string.Empty || mainCName == string.Empty)
		{
			if (FormActionName == PccesFormAction.BUD)
			{
				MessageBox.Show(this, "主辦機關無資料！\n 請至【預算資訊】-->【專案基本資訊】中挑選。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBox.Show(this, "主辦機關無資料！\n 請至【標單資訊】-->【專案基本資訊】中挑選，並告知業主。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return;
		}
		MainUnit mainUnit = new MainUnit();
		DataSet dsMainUnit = mainUnit.GetMainUnit(mainCode);
		if (dsMainUnit.Tables[0].Rows.Count == 0)
		{
			MessageBox.Show(this, "請檢查主辦機關維護是否無此項 " + mainCode + " 機關代碼。\n若無請至【系統維護】-->【主辦單位維護】新增或匯入最新主辦機關資料。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		if (Flag == "Z14AC1100")
		{
			string IPStr = CommonMethods.GetIPAddress();
			ArrayList aArr = new ArrayList();
			aArr.Clear();
			aArr.Add(userID);
			aArr.Add("預算--讀取預算書基本資料--" + projectCode + "(" + IPStr + ")");
			Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
			PROJ.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
			if (!PROJ.ChkPostMode(projectCode))
			{
				MessageBox.Show(this, "專案中，使用到的工作要項中，有尚未核可的項目，\n請先返回基本資料庫維護，將使用到的項目[核可]。\n目前不能執行電子檔匯出。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			PROJ = null;
		}
		string mainUnitName = dsMainUnit.Tables[0].Rows[0]["mainName"].ToString().Trim();
		string mainUnitEnglishName = dsMainUnit.Tables[0].Rows[0]["mainNameE"].ToString().Trim();
		FormBudgetExp_Wzd ExportExcelDialog = new FormBudgetExp_Wzd();
		ExportExcelDialog._UserID = userID;
		ExportExcelDialog._ActionName = FormActionName;
		ExportExcelDialog._ProjectCode = projectCode;
		ExportExcelDialog._DeptName = mainUnitName;
		ExportExcelDialog._DeptEName = mainUnitEnglishName;
		ExportExcelDialog._ProjectNameC = dtProject.Rows[0]["projectNameC"].ToString().Trim();
		ExportExcelDialog._ProjectNameE = dtProject.Rows[0]["projectNameE"].ToString().Trim();
		ExportExcelDialog._ProjectAddress = dtProject.Rows[0]["projectAddress"].ToString().Trim();
		ExportExcelDialog._ProjectEngAddress = "";
		ExportExcelDialog._MainProjectCode = sourceProjectCode;
		ExportExcelDialog._AccountCode1 = dtProject.Rows[0]["accountCode1"].ToString().Trim();
		ExportExcelDialog._AccountCode2 = dtProject.Rows[0]["accountCode2"].ToString().Trim();
		ExportExcelDialog._ProjectDescription = dtProject.Rows[0]["projectDescription"].ToString().Trim();
		ExportExcelDialog._ProjFLAG = Flag.Trim();
		ExportExcelDialog._IsSubmit = IsSubmitBid;
		ExportExcelDialog._Preview = IsPreview;
		ExportExcelDialog.ShowDialog(this);
		ExportExcelDialog.Dispose();
		ExportExcelDialog = null;
	}

	private bool CheckKind()
	{
		int Length = gridBudget.FindRow("99999999999999999999999999999999", 1, gridBudget.Cols["PrintNo"].SafeIndex, caseSensitive: true, fullMatch: false, wrap: false);
		string ZeroQtyList = string.Empty;
		string DescWorkItemList = string.Empty;
		for (int i = 1; i < Length; i++)
		{
			if (gridBudget[i, "Kind"].ToString() == "B" && ArchConvert.Obj2Int(gridBudget[i, "Qty"]) != 1)
			{
				try
				{
					ZeroQtyList = ZeroQtyList + "" + gridBudget[i, "CName"].ToString() + "\u3000";
				}
				catch (Exception ex)
				{
					MessageBox.Show("FormBudget::CheckKind()#1 " + ex.Message);
				}
			}
			if (ZeroQtyList.Length > 100)
			{
				ZeroQtyList = ZeroQtyList.Substring(0, 100) + "...等";
			}
			if (gridBudget[i, "Kind"].ToString() == "W" && ArchConvert.Obj2String(gridBudget[i, "PccesCode"]).StartsWith("#"))
			{
				try
				{
					DescWorkItemList = DescWorkItemList + " " + gridBudget[i, "CName"].ToString() + "\u3000";
				}
				catch (Exception ex)
				{
					MessageBox.Show("FormBudget::CheckKind()#2 " + ex.Message);
				}
			}
		}
		if (DescWorkItemList.Length > 100)
		{
			DescWorkItemList = DescWorkItemList.Substring(0, 100) + "...等";
		}
		if ((ZeroQtyList != string.Empty || DescWorkItemList != string.Empty) && MessageBox.Show(((ZeroQtyList != string.Empty) ? ("主項大類：\n" + ZeroQtyList + "\n數量不是1！\n") : "") + ((DescWorkItemList != string.Empty) ? ("工項：\n" + DescWorkItemList + "\n為說明項！\n") : "") + "是否要繼續? 選是則繼續輸出", "警示", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
		{
			return false;
		}
		return true;
	}

	private void Do_CostAdjust()
	{
		string IPStr = CommonMethods.GetIPAddress();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(userID);
		aArr.Add("總價調整--" + projectCode + "(" + IPStr + ")");
		if (dbItemA != null)
		{
			dbItemA = null;
		}
		if (dbItemA == null)
		{
			dbItemA = new Archnowledge.Pcces.BUDClass.ItemA(aArr);
		}
		dbItemA.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
		FormBudgetResetCost FM_BDGT_RSTCST = new FormBudgetResetCost();
		FM_BDGT_RSTCST._UserID = userID;
		FM_BDGT_RSTCST._ActionName = FormActionName;
		FM_BDGT_RSTCST._OldTotalAmount = dbItemA.GetOldAmount(projectCode);
		FM_BDGT_RSTCST._TotalAmount = GetItemAAmount();
		FM_BDGT_RSTCST._ProjectCode = projectCode;
		FM_BDGT_RSTCST.Owner = this;
		if (FM_BDGT_RSTCST.ShowDialog() == DialogResult.OK)
		{
			CheckIsReCal("Y");
			LoadProjectData();
			Th_ReCal_All(Auto: true);
		}
		FM_BDGT_RSTCST.Close();
		FM_BDGT_RSTCST.Dispose();
		FM_BDGT_RSTCST = null;
		dbItemA = null;
	}

	private void OpenAutoInsertSubtotalItemForm()
	{
		FormBudgetAutoInsertSubtotalItem formBudgetAutoInsertSubtotalItem = new FormBudgetAutoInsertSubtotalItem();
		formBudgetAutoInsertSubtotalItem._ProjectCode = projectCode;
		formBudgetAutoInsertSubtotalItem.Owner = this;
		if (formBudgetAutoInsertSubtotalItem.ShowDialog() == DialogResult.Yes)
		{
			theProject.UpdateProjectIsReCal(projectCode, "Y");
			LoadProjectData();
		}
		formBudgetAutoInsertSubtotalItem.Close();
		formBudgetAutoInsertSubtotalItem.Dispose();
		formBudgetAutoInsertSubtotalItem = null;
	}

	private void OpenChangeToCompanyCodeWindow()
	{
		FormChangeToCompanyCode formChangeToCompanyCode = new FormChangeToCompanyCode();
		formChangeToCompanyCode._userID = userID;
		formChangeToCompanyCode._projectCode = projectCode;
		formChangeToCompanyCode._projectName = projectName;
		if (formChangeToCompanyCode.ShowDialog(this) == DialogResult.OK)
		{
			LoadProjectData();
		}
		formChangeToCompanyCode.Dispose();
		formChangeToCompanyCode = null;
	}

	private void AddNewBudgetChangeVersion()
	{
		if (!IsLocked)
		{
			MessageBox.Show("預算需鎖定才可新增一期預算變更！\n請至工具 -> 鎖定，鎖定此預算書。");
			return;
		}
		FormBudgetChangeInfo formBudgetChangeInfo = new FormBudgetChangeInfo();
		formBudgetChangeInfo._projectCode = projectCode;
		formBudgetChangeInfo._userID = userID;
		formBudgetChangeInfo._version = budgetChangeCurrentVersion + 1;
		formBudgetChangeInfo._openMode = FormBudgetChangeInfo.Mode.New;
		if (formBudgetChangeInfo.ShowDialog() == DialogResult.OK)
		{
			MessageBox.Show("新增第 " + (budgetChangeCurrentVersion + 1) + " 期預算變更成功！");
			budgetChangeCurrentVersion++;
			LockOrUnlockToolbar(Locked: false);
			InitRestoreSnapshot();
			SetupRestoreSnapshotListCNT();
			SetupRestoreSnapshotList();
			InitBudgetChange();
			if (formBudgetChangeInfo.PickFromEstimateCost)
			{
				MessageBox.Show("由預估成本匯入，系統需要執行重新總計，以確保資料的正確性。");
				DoNewCalculate();
			}
			LoadProjectData();
		}
		formBudgetChangeInfo.Dispose();
		formBudgetChangeInfo = null;
	}

	private void ViewBudgetChangeInfo()
	{
		FormBudgetChangeInfo formBudgetChangeInfo = new FormBudgetChangeInfo();
		formBudgetChangeInfo._projectCode = projectCode;
		formBudgetChangeInfo._userID = userID;
		formBudgetChangeInfo._version = budgetChangeCurrentVersion;
		if (SysConfig.SysChangeManagement)
		{
			formBudgetChangeInfo._openMode = FormBudgetChangeInfo.Mode.Edit;
		}
		else
		{
			formBudgetChangeInfo._openMode = ((!IsLocked) ? FormBudgetChangeInfo.Mode.Edit : FormBudgetChangeInfo.Mode.ReadOnly);
		}
		formBudgetChangeInfo.ShowDialog();
		formBudgetChangeInfo.Dispose();
		formBudgetChangeInfo = null;
	}

	private void DeleteBudgetChangeVersion()
	{
		if (MessageBox.Show("是否要刪除此次預算變更？", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
		{
			return;
		}
		if (budgetChangeCurrentVersion == 0)
		{
			toolbarsManager.Tools["DeleteBudgetChangeVersion"].SharedProps.Enabled = false;
			MessageBox.Show("不可刪除原預算", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return;
		}
		BudExeProject budExeProject = new BudExeProject();
		ExecResult ER = budExeProject.RevertBudgetChange(projectCode, budgetChangeCurrentVersion);
		if (ER.ReturnCode != 0)
		{
			LoadProjectData();
			MessageBox.Show("刪除預算變更失敗，錯誤如下：\n" + ER.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return;
		}
		InitRestoreSnapshot();
		SetupRestoreSnapshotListCNT();
		SetupRestoreSnapshotList();
		InitBudgetChange();
		Th_ReCal_All(Auto: true);
		LockOrUnlockToolbar(Locked: true);
	}

	public void _Execute_Do_ReCal_All()
	{
		Do_ReCal_All();
	}

	private void Do_ReCal_All()
	{
		SetMemoItemCostToZero();
		if (FormActionName == PccesFormAction.BUD)
		{
			PwrSet pwrSet = new PwrSet();
			ExecResult ER = pwrSet.Synchronize(projectCode);
			if (ER.ReturnCode != 0)
			{
				MessageBox.Show("發包權限同步錯誤：\n" + ER.Message);
			}
		}
		try
		{
			bool EnableNewCalculateCost = false;
			Archnowledge.Pcces.DomainModule.General.PubProject thePubProject = new Archnowledge.Pcces.DomainModule.General.PubProject();
			EnableNewCalculateCost = thePubProject.GetPubProjectEnableNewCalculateCost(projectCode);
			bool HasDoneCalculated = false;
			if (!((!EnableNewCalculateCost) ? DoOldCalculate() : DoNewCalculate()))
			{
				return;
			}
			BudProjMrsA budProjMrsA = new BudProjMrsA();
			int iCostZeroItems = budProjMrsA.IsThereCostEquZeroItem(projectCode);
			if (iCostZeroItems > 0)
			{
				MessageBox.Show(this, "注意：偵測到專案工項有單價或數量為\"0\"項目\n\n請使用【檢視】-->【專案工項維護】-->【檢視】-->【單價或數量為\"0\"項目】幫你篩選出有單價或數量為\"0\"項目。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			DataTable DT_Dups = budProjMrsA.GetDuplicateItems(projectCode);
			int iRowsCount = DT_Dups.Rows.Count;
			if (iRowsCount > 0)
			{
				MessageBox.Show(this, "注意：偵測到工項名稱及單位完全一樣，但工項編碼出現2個以上，若要查詢哪些工項名稱重複\n\n請使用【檢視】-->【專案工項維護】-->【檢視】-->【工項名稱重複】幫你篩選出有重複之工項。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			int iAmtZero = 0;
			string sAmtZeroMessage = "";
			for (int i = 1; i < gridBudget.Rows.Count; i++)
			{
				if (gridBudget[i, "cName"] != null && !(gridBudget[i, "costKind"].ToString() == "#") && (gridBudget[i, "amount"] == null || ArchConvert.Obj2Decimal(gridBudget[i, "amount"].ToString()) == 0m))
				{
					iAmtZero++;
					string text = sAmtZeroMessage;
					sAmtZeroMessage = text + gridBudget[i, "itemNo"].ToString() + "\t" + gridBudget[i, "cName"].ToString() + "\t" + gridBudget[i, "unitName"].ToString() + "\n";
				}
			}
			if (iAmtZero > 0 && FormActionName != PccesFormAction.BID)
			{
				if (iAmtZero <= 40)
				{
					if (MessageBox.Show(this, "發現詳細表裡有 " + iAmtZero + " 項複價為\"0\"，是否要顯示詳細內容?", "警示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						MessageBox.Show(this, "您可以使用 Ctrl+C 將以下內容複製起來\n\n" + sAmtZeroMessage, "詳細表複價為0項目", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
				}
				else
				{
					MessageBox.Show(this, "發現詳細表裡有 " + iAmtZero + " 項複價為\"0\"，請重新檢查詳細表項目。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			if (FormActionName == PccesFormAction.BUD)
			{
				(theProject as BudProject).UpdateSelfExam(projectCode, "000000");
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("FormBudget::Do_ReCal_All()#1 Error : " + ex.Message);
			if (FM_INFO != null)
			{
				FM_INFO.Close();
				FM_INFO.Dispose();
				FM_INFO = null;
				Application.DoEvents();
			}
		}
	}

	private void SetMemoItemCostToZero()
	{
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(userID);
		aArr.Add("重新總計前, 先把說明項的單價設為0--" + projectCode + "");
		string srckind = CommonMethods.GetActionNameString(FormActionName);
		string sSQL1 = "Update " + srckind + "ItemA    Set " + srckind + "ItemA.cost = 0   From " + srckind + "ItemA A Join " + srckind + "ProjMrsA B on A.PccesCode=B.pccesCode and A.projectCode=B.projectCode  Where A.projectCode = '" + projectCode + "' and B.costKind='#'";
		ModifyDB ModDB = new ModifyDB(projectCode, aArr);
		int iEffect01 = ModDB.DBUpd(sSQL1);
		string sSQL2 = "Update " + srckind + "ProjMrsA    Set " + srckind + "ProjMrsA.cost = 0  Where projectCode = '" + projectCode + "' and costKind='#'";
		int iEffect2 = ModDB.DBUpd(sSQL2);
	}

	private bool DoNewCalculate()
	{
		bool retV = true;
		string AppLocation = AppDomain.CurrentDomain.BaseDirectory;
		string sIsAutoNumber = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "BDGT", "IsAutoNumber");
		string srckind = CommonMethods.GetActionNameString(FormActionName);
		if (AppLocation.Substring(AppLocation.Length - 1) != "\\")
		{
			AppLocation += "\\";
		}
		if (sIsAutoNumber.ToUpper() == "TRUE" && srckind.ToUpper() == "BUD")
		{
			Do_ItemReArrange(isSilence: false);
		}
		if (!IsAuto && MessageBox.Show(this, "確定要執行重新總計?", "訊問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
		{
			return false;
		}
		gridBudget.Enabled = false;
		FM_INFO = new FormSys_G_Info1();
		FM_INFO._InfoString = "重新總計中，請稍候! ";
		FM_INFO.Owner = this;
		FM_INFO._MaxValue = 0;
		FM_INFO.Show();
		FM_INFO.BringToFront();
		Application.DoEvents();
		Cursor = Cursors.WaitCursor;
		Application.DoEvents();
		ItemCalculate theItemCalculate = new ItemCalculate(FormActionName, projectCode, 0);
		ExecResult ER = theItemCalculate.CalculateAll(IncludeResource: true, IncludeMrs: true, ProgressEventHandler, ProgressEventHandlerInitMaxProgressValue);
		FM_INFO.Hide();
		if (ER.ReturnCode == 0 && !IsAuto)
		{
			Cursor = Cursors.Default;
			MessageBox.Show(this, "重新總計完成!", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			int iCount = GetProjMrsBaseData();
			if (iCount > 0)
			{
				string sMessage = "注意：偵測到單價分析子項有負數!若要查詢哪些工項為負數\n\n 請使用【檢視】-->【專案工項維護】-->【分析子項為負】幫你篩選出有負數之單價分析";
				MessageBox.Show(this, sMessage, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			int iZeroCount = GetProjAnalysisSubItemZero();
			if (iZeroCount > 0)
			{
				string sMessage = "注意：偵測到單價分析子項有單價或數量為0，若要查詢哪些工項為0\n\n 請使用【檢視】-->【專案工項維護】-->【單價或數量為 0 項目】幫你篩選出有為0之項目";
				MessageBox.Show(this, sMessage, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			theProject.UpdateProjectIsReCal(projectCode, "N");
		}
		else if (ER.ReturnCode != 0 && !IsAuto)
		{
			string sMessage = "重新總計失敗，請檢查後再執行!\n\n例如:\n(1)單價分析子項引用了與父項相同的工項\n     比如:【清除與掘除】的分析子項又引用了一次【清除與掘除】\n\n(2)單價分析子項沒有設定雜項。\n     因為產生差額要攤給雜項，有單價分析並未設定雜項。\n     可使用【檢視】-->【專案工項維護】-->【計算錯誤項目】幫你篩選出有狀況(2)之項目。\n     或是至【工具】-->【選項...】-->【計算方式】-->勾選【一律不作攤提】\n\nError : " + ER.Message;
			if (projectCode == "ArchEx001" && currentDBName.ToUpper() == "PCCES")
			{
				sMessage = "此為範例案，請先手動修正第【壹五.3】工項編碼為：[16221535A3]\n此單價分析項未設定雜項，以致沒有差額攤提對象。\n\n";
				for (int z = 1; z < gridBudget.Rows.Count - 1; z++)
				{
					if (gridBudget[z, "PccesCode"] != null && gridBudget[z, "PccesCode"].ToString().IndexOf("16221535A3") > -1)
					{
						gridBudget.Row = z;
						gridBudget.Select();
						break;
					}
				}
			}
			MessageBox.Show(this, sMessage, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		gridBudget.Enabled = true;
		gridBudget.Refresh();
		Application.DoEvents();
		LoadProjectData();
		Cursor = Cursors.Default;
		FM_INFO.Close();
		FM_INFO.Dispose();
		FM_INFO = null;
		Application.DoEvents();
		return retV;
	}

	private bool DoOldCalculate()
	{
		bool retV = true;
		string AppLocation = AppDomain.CurrentDomain.BaseDirectory;
		string sIsAutoNumber = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "BDGT", "IsAutoNumber");
		string srckind = CommonMethods.GetActionNameString(FormActionName);
		if (AppLocation.Substring(AppLocation.Length - 1) != "\\")
		{
			AppLocation += "\\";
		}
		if (sIsAutoNumber.ToUpper() == "TRUE" && srckind.ToUpper() == "BUD")
		{
			Do_ItemReArrange(isSilence: false);
		}
		if (!IsAuto && MessageBox.Show(this, "確定要執行重新總計?", "訊問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
		{
			return false;
		}
		gridBudget.Enabled = false;
		FM_INFO = new FormSys_G_Info1();
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
		aArr.Add(userID);
		aArr.Add("重新總計--" + projectCode + "(" + IPStr + ")");
		if (dbItemA != null)
		{
			dbItemA = null;
		}
		if (dbItemA == null)
		{
			dbItemA = new Archnowledge.Pcces.BUDClass.ItemA(aArr);
		}
		dbItemA.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
		Cursor = Cursors.WaitCursor;
		Application.DoEvents();
		string sType = GetReCalType();
		string IsOldReCal = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "BDGT", "IsOldReCal");
		DataSet DSProject = theProject.GetProject(projectCode);
		string roundAnalysisItemPrice = string.Empty;
		if (DSProject.Tables[0].Columns.Contains("roundAnalysisItemPrice") && DSProject.Tables[0].Rows[0]["roundAnalysisItemPrice"] != DBNull.Value)
		{
			roundAnalysisItemPrice = DSProject.Tables[0].Rows[0]["roundAnalysisItemPrice"].ToString();
		}
		if (roundAnalysisItemPrice == "1")
		{
			dbItemA.ps_IsForceInteger = "True";
		}
		if (sType != "")
		{
			IsOldReCal = sType;
		}
		tmrReCalAll.Enabled = true;
		int iResult = 1;
		if (srckind == "BID")
		{
			Archnowledge.Pcces.BUDClass.Project projcom = new Archnowledge.Pcces.BUDClass.Project(aArr);
			projcom.ps_srckind = srckind;
			DataTable dt = projcom.ListItem_eight("", projectCode);
			if (dt.Rows.Count > 0 && dt.Rows[0]["ReCalType"].ToString().Trim() == "" && dt.Rows[0]["printMode"].ToString() != "")
			{
				string readPrintMode = dt.Rows[0]["printMode"].ToString().Trim();
				string tmpPrintMode = readPrintMode.Substring(37, 1);
				IsOldReCal = ((tmpPrintMode == "0") ? "FALSE" : ((!(tmpPrintMode == "1")) ? "THIRD" : "TRUE"));
			}
			projcom = null;
			dt = null;
		}
		if (IsOldReCal.ToUpper() == "TRUE")
		{
			InserReCalType("2");
			iResult = dbItemA.ReCalcCost2(projectCode, mode: true, noShare: true);
		}
		else if (IsOldReCal.ToUpper() == "FALSE")
		{
			InserReCalType("1");
			iResult = dbItemA.ReCalcCost2(projectCode);
		}
		else if (IsOldReCal.ToUpper() == "THIRD")
		{
			InserReCalType("3");
			dbItemA.ps_SmallCalcuMode = "THIRD";
			iResult = dbItemA.ReCalcCost2(projectCode);
		}
		else
		{
			InserReCalType("3");
			iResult = dbItemA.ReCalcCost2(projectCode);
		}
		tmrReCalAll.Enabled = false;
		FM_INFO.Hide();
		if (iResult == 1 && !IsAuto)
		{
			theCostKind.AddCostKindByItemA(projectCode);
			CheckIsReCal("N");
			Cursor = Cursors.Default;
			dbItemA = null;
			MessageBox.Show(this, "重新總計完成!", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			int iCount = GetProjMrsBaseData();
			if (iCount > 0)
			{
				string sMessage = "注意：偵測到單價分析子項有負數!若要查詢哪些工項為負數\n\n 請使用【檢視】-->【專案工項維護】-->【分析子項為負】幫你篩選出有負數之單價分析";
				MessageBox.Show(this, sMessage, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			int iZeroCount = GetProjAnalysisSubItemZero();
			if (iZeroCount > 0)
			{
				string sMessage = "注意：偵測到單價分析子項有單價或數量為0，若要查詢哪些工項為0\n\n 請使用【檢視】-->【專案工項維護】-->【單價或數量為 0 項目】幫你篩選出有為0之項目";
				MessageBox.Show(this, sMessage, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		else if (iResult != 1 && !IsAuto)
		{
			string sMessage = "重新總計失敗，請檢查後再執行!\n\n例如:\n(1)單價分析子項引用了與父項相同的工項\n     比如:【清除與掘除】的分析子項又引用了一次【清除與掘除】\n\n(2)單價分析子項沒有設定雜項。\n     因為產生差額要攤給雜項，有單價分析並未設定雜項。\n     可使用【檢視】-->【專案工項維護】-->【計算錯誤項目】幫你篩選出有狀況(2)之項目。\n     或是至【工具】-->【選項...】-->【計算方式】-->勾選【一律不作攤提】";
			if (projectCode == "ArchEx001" && currentDBName.ToUpper() == "PCCES")
			{
				sMessage = "此為範例案，請先手動修正第【壹五.3】工項編碼為：[16221535A3]\n此單價分析項未設定雜項，以致沒有差額攤提對象。\n\n";
				for (int z = 1; z < gridBudget.Rows.Count - 1; z++)
				{
					if (gridBudget[z, "PccesCode"].ToString().IndexOf("16221535A3") > -1)
					{
						gridBudget.Row = z;
						gridBudget.Select();
						break;
					}
				}
			}
			MessageBox.Show(this, sMessage, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		gridBudget.Enabled = true;
		gridBudget.Refresh();
		Cursor = Cursors.WaitCursor;
		Application.DoEvents();
		LoadProjectData();
		Cursor = Cursors.Default;
		FM_INFO.Close();
		FM_INFO.Dispose();
		Application.DoEvents();
		return retV;
	}

	private void ExecuteResForm()
	{
		FormBudgetRes formBudgetRes = new FormBudgetRes();
		formBudgetRes._UserID = userID;
		formBudgetRes._ActionName = FormActionName;
		formBudgetRes._ProjectCode = projectCode;
		formBudgetRes._IsSBID = IsSubmitBid;
		formBudgetRes._Istemplate = IsTemplate;
		formBudgetRes._CurrentDBName = currentDBName;
		formBudgetRes._calledPccesCode = ((gridBudget[gridBudget.Row, "PccesCode"] == null) ? string.Empty : gridBudget[gridBudget.Row, "PccesCode"].ToString());
		formBudgetRes._budgetType = (int)budgetType;
		formBudgetRes._parentProjectCode = parentProjectCode;
		formBudgetRes.Owner = this;
		formBudgetRes._lblProjectData = "【" + projectCode + "】" + projectName;
		formBudgetRes._ParentItemA = ParentItemA;
		if (formBudgetRes.ShowDialog() == DialogResult.OK || F_IsNeedToReloadAllData || formBudgetRes._IsBudgetFormNeedToReload)
		{
			F_SNo = ArchConvert.Obj2Int(gridBudget[gridBudget.Row, "Sno"]);
			LoadProjectData();
			F_IsNeedToReloadAllData = false;
			F_SNo = -1;
		}
		_dsParentProjMrsA = formBudgetRes._dsParentProjMrsA;
		_AddParentBookList = formBudgetRes._AddParentBookList;
		if (AddParentBookList)
		{
			Add_ParentBookmark();
		}
		CheckIsReCal("Y");
		formBudgetRes.Close();
		formBudgetRes.Dispose();
		formBudgetRes = null;
	}

	private void DoMenuViewProjectInfo(int Idx)
	{
		FormBudgetProjectInfo formBudgetProjectInfo = new FormBudgetProjectInfo();
		formBudgetProjectInfo._UserID = userID;
		formBudgetProjectInfo._OpenMode = BudgetInfoForm_OpenMode.ViewInformation;
		formBudgetProjectInfo._ProjectCode = projectCode;
		formBudgetProjectInfo._ActionName = FormActionName;
		formBudgetProjectInfo._iShowUp_FirstIndex = Idx;
		if (formBudgetProjectInfo.ShowDialog(this) == DialogResult.OK && formBudgetProjectInfo._ChangeProjectScope && FormActionName == PccesFormAction.BUD)
		{
			dtProject = theProject.GetProject(projectCode).Tables[0];
			Data2Grid();
		}
		bool jumpToSysmaintain = formBudgetProjectInfo.jumpToSysmaintain;
		formBudgetProjectInfo.Dispose();
		formBudgetProjectInfo = null;
		if (jumpToSysmaintain)
		{
			functionButtons1.optionTabSelected = true;
			functionButtons1.BtnFunc1_Click(null, null);
		}
	}

	private void DoPickFromMain()
	{
		FormBudgetSplit FM_BDGT_SPLT = new FormBudgetSplit();
		FM_BDGT_SPLT._UserID = userID;
		FM_BDGT_SPLT._ProjectCode = projectCode;
		FM_BDGT_SPLT._ProjectNameC = projectName;
		FM_BDGT_SPLT._MainProjectCode = sourceProjectCode.Trim();
		if (FM_BDGT_SPLT.ShowDialog(this) == DialogResult.OK)
		{
			LoadProjectData();
		}
		FM_BDGT_SPLT.Close();
		FM_BDGT_SPLT.Dispose();
		FM_BDGT_SPLT = null;
	}

	private void ExecutePickFromProj()
	{
		if (gridBudget.RowSel <= 0)
		{
			MessageBox.Show("請先將焦點移至新增位置再執行自專案挑選工項");
			return;
		}
		FormPickProjWkItem_Wzd FM_PICK_PROJ_WK = new FormPickProjWkItem_Wzd();
		FM_PICK_PROJ_WK._ActionName = FormActionName;
		FM_PICK_PROJ_WK._ProjectCode = projectCode;
		FM_PICK_PROJ_WK._UserID = userID;
		FM_PICK_PROJ_WK._IsCostStructure = IsCostStructureRow(gridBudget.Row, thisRowOnly: false);
		FM_PICK_PROJ_WK._CompanyDBName = companyDBName;
		FM_PICK_PROJ_WK.ShowDialog(this);
		FM_PICK_PROJ_WK.Close();
		FM_PICK_PROJ_WK.Dispose();
		FM_PICK_PROJ_WK = null;
		CheckIsReCal("Y");
		theProject.ReArrangePrintNo(projectCode, !IsEditItemNo);
	}

	private void ExecutePickFromMrs()
	{
		if (gridBudget.RowSel <= 0)
		{
			MessageBox.Show("請先將焦點移至新增位置再執行自基本資料庫挑選工項");
			return;
		}
		string sCostUID = "";
		string sCostType = "";
		for (int i = gridBudget.Row; i > 0; i--)
		{
			if (gridBudget[i, "Kind"] != null && gridBudget[i, "Kind"].ToString().ToUpper() == "B")
			{
				if (gridBudget[i, "CostUID"] != null)
				{
					sCostUID = gridBudget[i, "CostUID"].ToString();
				}
				if (gridBudget[i, "TypeID"] != null)
				{
					sCostType = gridBudget[i, "TypeID"].ToString();
				}
				break;
			}
		}
		FormMrsBaseBreakdown_Addnew BD_ADD = new FormMrsBaseBreakdown_Addnew();
		BD_ADD._CallFormName = base.Name;
		BD_ADD._CostUID = sCostUID;
		BD_ADD._CostType = sCostType;
		BD_ADD._UserID = userID;
		BD_ADD._CompanyDBName = companyDBName;
		BD_ADD._ProjectCode = projectCode;
		BD_ADD._ActionName = _ActionName;
		if (BD_ADD.ShowDialog(this) != DialogResult.Cancel)
		{
			theProject.ReArrangePrintNo(projectCode, !IsEditItemNo);
			LoadProjectData();
		}
		BD_ADD.Close();
		BD_ADD.Dispose();
		BD_ADD = null;
	}

	private void DoInsertMainItems(bool InsertChild)
	{
		if (gridBudget.RowSel <= 0)
		{
			MessageBox.Show("請先將焦點移至新增位置再執行插入主項");
			return;
		}
		int RowIndex = gridBudget.Row;
		int iParentSno = 0;
		int iSortOrder = 0;
		string ParentPrint2Analysis = "";
		if (ArchConvert.Obj2String(gridBudget[RowIndex, "kind"]) == "B")
		{
			if (InsertChild)
			{
				iParentSno = ArchConvert.Obj2Int(gridBudget[RowIndex, "sNo"]);
				ParentPrint2Analysis = ArchConvert.Obj2String(gridBudget[RowIndex, "PrintToAnalysis"]);
				Node Nd = gridBudget.Rows[RowIndex].Node.GetNode(NodeTypeEnum.LastChild);
				iSortOrder = ((Nd == null) ? 1 : (ArchConvert.Obj2Int(gridBudget[Nd.Row.Index, "SortOrder"]) + 1));
			}
			else
			{
				iParentSno = ArchConvert.Obj2Int(gridBudget[RowIndex, "ParentSno"]);
				iSortOrder = ArchConvert.Obj2Int(gridBudget[RowIndex, "SortOrder"]);
				Node p = gridBudget.Rows[RowIndex].Node.GetNode(NodeTypeEnum.Parent);
				if (p != null)
				{
					ParentPrint2Analysis = ArchConvert.Obj2String(gridBudget[p.Row.Index, "PrintToAnalysis"]);
				}
			}
		}
		else
		{
			iParentSno = ArchConvert.Obj2Int(gridBudget[RowIndex, "ParentSno"]);
			iSortOrder = ArchConvert.Obj2Int(gridBudget[RowIndex, "SortOrder"]);
			if (gridBudget.Rows[RowIndex].IsNode && gridBudget.Rows[RowIndex].Node.Level > 0)
			{
				Node p2 = gridBudget.Rows[RowIndex].Node.GetNode(NodeTypeEnum.Parent);
				if (p2 != null)
				{
					ParentPrint2Analysis = ArchConvert.Obj2String(gridBudget[p2.Row.Index, "PrintToAnalysis"]);
				}
			}
			else
			{
				ParentPrint2Analysis = "";
			}
		}
		int iLastItemLevel = ((gridBudget.Rows[RowIndex].Node == null) ? 1 : (InsertChild ? (gridBudget.Rows[RowIndex].Node.Level + 1) : gridBudget.Rows[RowIndex].Node.Level));
		if (iLastItemLevel == 0)
		{
			iLastItemLevel = 1;
		}
		int iNewLines = 0;
		string sParentPrintToAnalysis = "";
		if (InsertChild && RowIndex > 0)
		{
			sParentPrintToAnalysis = ((gridBudget[RowIndex, "PrintToAnalysis"] != null) ? gridBudget[RowIndex, "PrintToAnalysis"].ToString() : "0");
		}
		string sKind = "B";
		int iChildCount = 0;
		if (gridBudget[RowIndex, "Kind"] != null && RowIndex > 0)
		{
			sKind = gridBudget[RowIndex, "Kind"].ToString();
			if (sKind.ToUpper() == "B")
			{
				Node LastNode = gridBudget.Rows[RowIndex].Node.GetNode(NodeTypeEnum.LastChild);
				while (LastNode != null && LastNode.Children > 0)
				{
					LastNode = LastNode.GetNode(NodeTypeEnum.LastChild);
				}
				if (LastNode != null)
				{
					iChildCount = LastNode.Row.SafeIndex - RowIndex;
				}
			}
		}
		bool IsNewLines = false;
		FormAskLines FM_ASK_LINE = new FormAskLines();
		if (gridBudget[RowIndex, "ItemNo"] != null && gridBudget[RowIndex, "CName"] != null)
		{
			FM_ASK_LINE._Question = "欲新增 【" + gridBudget[RowIndex, "ItemNo"].ToString().Trim() + "】 " + gridBudget[RowIndex, "CName"].ToString().Trim() + (InsertChild ? " 子階幾項?" : " 同階幾項?");
		}
		else
		{
			FM_ASK_LINE._Question = "欲新增 " + (InsertChild ? " 子階幾項?" : " 同階幾項?");
		}
		FM_ASK_LINE._Answer = "1";
		if (FM_ASK_LINE.ShowDialog(this) == DialogResult.OK)
		{
			iNewLines = PubTools.Str2Int(FM_ASK_LINE._Answer);
			if (iNewLines <= 0)
			{
				MessageBox.Show(this, "輸入數值錯誤, 無法新增!!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				CheckIsReCal("Y");
				IsNewLines = true;
			}
		}
		FM_ASK_LINE.Close();
		FM_ASK_LINE.Dispose();
		FM_ASK_LINE = null;
		if (IsNewLines)
		{
			if (F_NewAddItemFlag == "0" && F_IsNewProject == "")
			{
				if (FormActionName == PccesFormAction.BUD)
				{
					if (GetCurrentBDGT_Type() == "CNT")
					{
						ExecuteCopyToTmpCNT("");
						SetupRestoreSnapshotListCNT();
					}
					else
					{
						ExecuteCopyToTmp("");
						SetupRestoreSnapshotList();
					}
				}
				else
				{
					ExecuteCopyToTmp("");
					SetupRestoreSnapshotList();
				}
				F_NewAddItemFlag = "1";
			}
			ExecResult ER = new ExecResult();
			string[] sNo = new string[iNewLines];
			FM_INFO = new FormSys_G_Info1();
			FM_INFO._MaxValue = iNewLines;
			FM_INFO._MinValue = 0;
			FM_INFO._ProgressValue = 0;
			FM_INFO._InfoString = "項目插入中，請稍候! ";
			FM_INFO.Show();
			Application.DoEvents();
			Cursor = Cursors.WaitCursor;
			for (int j = 0; j < iNewLines; j++)
			{
				int SNo = 0;
				ER = theItemA.AddItemAByParent(projectCode, null, "0000", null, null, iLastItemLevel, "", "", "", "B", 0, 1, 0, null, 0, null, null, null, null, null, null, null, null, null, "", null, null, null, null, null, null, null, null, null, null, null, ParentPrint2Analysis, null, null, null, null, null, null, null, null, null, iParentSno, iSortOrder, out SNo);
				if (ER.ReturnCode == 0)
				{
					PageBreak thePageBreak = new BudPageBreak();
					thePageBreak.AddPageBreakIfExist(projectCode, SNo, "Y");
					sNo[j] = SNo.ToString();
					FM_INFO._ProgressValue = j + 1;
					continue;
				}
				break;
			}
			if (ER.ReturnCode == 0)
			{
				theProject.ReArrangePrintNo(projectCode, iParentSno, !IsEditItemNo);
				ReloadGridAtRootSno(iParentSno);
				CheckTotalAmount();
			}
			FM_INFO.Close();
			FM_INFO.Dispose();
			FM_INFO = null;
			F_SNo = iParentSno;
			LoadProjectData();
			F_SNo = -1;
		}
		theProject.ReArrangePrintNo(projectCode, !IsEditItemNo);
	}

	private void DoInsertWorkItems()
	{
		toolbarsManager.Tools["InsertWorkItem"].SharedProps.Enabled = false;
		toolbarsManager.Tools["InsertMainItem"].SharedProps.Enabled = false;
		int RowIndex = gridBudget.Row;
		int ColIndex = gridBudget.Col;
		if (RowIndex <= 0 || ColIndex <= 0)
		{
			return;
		}
		int iParentSno = 0;
		int iSortOrder = 0;
		if (ArchConvert.Obj2String(gridBudget[RowIndex, "kind"]) == "B")
		{
			iParentSno = ArchConvert.Obj2Int(gridBudget[RowIndex, "sNo"]);
			Node Nd = gridBudget.Rows[RowIndex].Node.GetNode(NodeTypeEnum.LastChild);
			iSortOrder = ((Nd == null) ? 1 : (ArchConvert.Obj2Int(gridBudget[Nd.Row.Index, "SortOrder"]) + 1));
		}
		else
		{
			iParentSno = ArchConvert.Obj2Int(gridBudget[RowIndex, "ParentSno"]);
			iSortOrder = ArchConvert.Obj2Int(gridBudget[RowIndex, "SortOrder"]);
		}
		string sParentPrintToAnalysis = ((gridBudget[RowIndex, "PrintToAnalysis"] != null) ? gridBudget[RowIndex, "PrintToAnalysis"].ToString() : "0");
		FormMrsBaseEdit FM_EDIT = new FormMrsBaseEdit();
		FM_EDIT._UserID = userID;
		FM_EDIT._EditMode = MrsBaseEditFormType.New;
		FM_EDIT._ActionName = FormActionName;
		FM_EDIT._ProjectCode = projectCode;
		FM_EDIT._Istemplate = IsTemplate;
		FM_EDIT._CallerFormName = "FormBudget";
		FM_EDIT._MainCost = MainItemCostPrecison.ToString();
		FM_EDIT._IsSubmitBid = IsSubmitBid;
		DialogResult theResult = FM_EDIT.ShowDialog(this);
		FM_EDIT.Close();
		FM_EDIT.Dispose();
		FM_EDIT = null;
		if (theResult == DialogResult.OK)
		{
			if (F_NewAddItemFlag == "0" && F_IsNewProject == "")
			{
				if (FormActionName == PccesFormAction.BUD)
				{
					if (GetCurrentBDGT_Type() == "CNT")
					{
						ExecuteCopyToTmpCNT("");
						SetupRestoreSnapshotListCNT();
					}
					else
					{
						ExecuteCopyToTmp("");
						SetupRestoreSnapshotList();
					}
				}
				else
				{
					ExecuteCopyToTmp("");
					SetupRestoreSnapshotList();
				}
				F_NewAddItemFlag = "1";
			}
			if (F_NewChildPubCode != -1)
			{
				ExecResult ER = new ExecResult();
				DataSet dsProjMrsA = theProjMrsA.GetProjMrsAByPubCode(projectCode, F_NewChildPubCode);
				int SNo = 0;
				if (dsProjMrsA.Tables.Count > 0 && dsProjMrsA.Tables[0].Rows.Count > 0)
				{
					DataRow theRow = dsProjMrsA.Tables[0].Rows[0];
					int PwrSetCode = PwrSet.GetCode(dsPwrSet, ArchConvert.Obj2String(theRow["PwrSet"]).Trim());
					ER = theItemA.AddItemAByParent(projectCode, null, "0000", F_NewChildPubCode, "", 0, theRow["cName"], theRow["EName"], theRow["UnitName"], "W", theRow["cost"], theRow["usrQty"], theRow["usrAmt"], theRow["Memo"], 0, null, null, null, null, theRow["EUnit"], null, null, null, null, "", null, null, null, theRow["PccesCode"], null, null, null, null, null, null, null, sParentPrintToAnalysis, theRow["surName"], false, null, null, null, null, null, null, PwrSetCode, iParentSno, iSortOrder, out SNo);
					if (ER.ReturnCode == 0)
					{
						PageBreak thePageBreak = new BudPageBreak();
						thePageBreak.AddPageBreakIfExist(projectCode, SNo, "Y");
					}
				}
				if (ER.ReturnCode == 0 && SNo != 0)
				{
					theProject.ReArrangePrintNo(projectCode, iParentSno, !IsEditItemNo);
					ReloadGridAtRootSno(iParentSno);
					SetGridFocusBySno(SNo, NeedAtTop: false);
				}
				CheckIsReCal("Y");
				if (ER.ReturnCode != 0)
				{
					MessageBox.Show(ER.Message);
					F_SNo = iParentSno;
					LoadProjectData();
					F_SNo = -1;
				}
				string TypeID = string.Empty;
				string CostUID = string.Empty;
				try
				{
					for (int i = gridBudget.Row; i > 0; i--)
					{
						if (gridBudget[i, "Kind"].ToString().ToUpper() == "B")
						{
							TypeID = ((gridBudget[i, "TypeID"] != null) ? gridBudget[i, "TypeID"].ToString() : string.Empty);
							CostUID = ((gridBudget[i, "CostUID"] != null) ? gridBudget[i, "CostUID"].ToString() : string.Empty);
							break;
						}
					}
				}
				catch
				{
				}
				if (ER.ReturnCode == 0 && CostUID != string.Empty && TypeID != string.Empty)
				{
					CostStructureMrsBase costStructure = new CostStructureMrsBase();
					costStructure.AddCostStructureMrsBase(TypeID, CostUID, gridBudget[RowIndex + 1, "PccesCode"].ToString());
				}
			}
		}
		toolbarsManager.Tools["InsertWorkItem"].SharedProps.Enabled = true;
		toolbarsManager.Tools["InsertMainItem"].SharedProps.Enabled = true;
		theProject.ReArrangePrintNo(projectCode, !IsEditItemNo);
		LoadProjectData();
	}

	private void SetAsSharedItem()
	{
		bool Allow = budgetType != BudgetType.Types.Execution;
		if (FormActionName == PccesFormAction.BUD && budgetType == BudgetType.Types.Execution)
		{
			if (SysConfig.SysChangeManagement && budgetChangeCurrentVersion == 0)
			{
				Allow = true;
			}
			if (!Allow && SysConfig.SysComsEnable && SysConfig.SysEditAfterBudLem.ToUpper() == "DISABLE")
			{
				Archnowledge.Pcces.DomainModule.Coms.BudgetCtrl theBudgetCtrl = new Archnowledge.Pcces.DomainModule.Coms.BudgetCtrl();
				if (!theBudgetCtrl.IsProjectAlreadySubPlan(projectCode, SysConfig.SysComsDB))
				{
					Allow = true;
				}
			}
		}
		BudProject theProject = null;
		theProject = new BudProject("Pcces");
		theProject.UpdateShareVDF1(projectCode, 0m, 0);
		if (Allow)
		{
			int Sno = ArchConvert.Obj2Int(gridBudget[gridBudget.RowSel, "SNo"]);
			decimal Qty = ArchConvert.Obj2Decimal(gridBudget[gridBudget.RowSel, "Qty"]);
			string Kind = ArchConvert.Obj2String(gridBudget[gridBudget.RowSel, "Kind"]);
			if ((!(Kind == "F") && !(Kind == "L")) || !(Qty == 1m))
			{
				MessageBox.Show("只有 公式計價項目或是獨立計價項，且數量為1 的可以做為攤提項,請檢查");
				return;
			}
			ExecResult ER = theItemA.UpdateShareItem(projectCode, Sno);
			if (Kind == "F")
			{
				theProject.UpdateShareVDF1(projectCode, 0m, Sno);
			}
			if (ER.ReturnCode != 0)
			{
				MessageBox.Show("設定總價調整攤提項執行失敗, 訊息:" + ER.Message);
				return;
			}
			Reload_OneRow(Sno, gridBudget.RowSel, RangeUpdate: false);
		}
		string IPStr = CommonMethods.GetIPAddress();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(userID);
		aArr.Add("設為攤提項--" + projectCode + "(" + IPStr + ")");
		if (dbItemA != null)
		{
			dbItemA = null;
		}
		if (dbItemA == null)
		{
			dbItemA = new Archnowledge.Pcces.BUDClass.ItemA(aArr);
		}
		dbItemA.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
		dbItemA.ps_projectCode = projectCode;
		for (int i = 1; i < gridBudget.Rows.Count; i++)
		{
			if (gridBudget[i, "SNo"] != null)
			{
				dbItemA.ps_sNo = gridBudget[i, "SNo"].ToString();
				if (i == gridBudget.Row)
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
		dbItemA = null;
		LoadProjectData();
	}

	private void SetDelSelItemDic(Dictionary<string, string> Dic, Row GridRow)
	{
		string PccesCode = ArchConvert.Obj2String(GridRow["PccesCode"]);
		bool IsOne4Item = ((GridRow["UnitName"].ToString() == "式" && ArchConvert.Obj2Decimal(GridRow["Qty"]) == 1m && !ArchConvert.Obj2Bool(GridRow["Analysis"])) ? true : false);
		decimal ItemQty = ArchConvert.Obj2Decimal(GridRow["Qty"]);
		decimal ItemCost = ArchConvert.Obj2Decimal(GridRow["Cost"]);
		string updvalue = "";
		if (Dic.ContainsKey(PccesCode))
		{
			string[] value = Dic[PccesCode].Split(';');
			updvalue = Dic[PccesCode];
			if (IsOne4Item)
			{
				decimal newcost = ArchConvert.Obj2Decimal(value[2]) + ItemCost;
				updvalue = value[0] + ";" + value[1] + ";" + newcost;
				Dic[PccesCode] = updvalue;
			}
			else
			{
				decimal newqty = ArchConvert.Obj2Decimal(value[1]) + ItemQty;
				updvalue = value[0] + ";" + newqty + ";" + value[2];
				Dic[PccesCode] = updvalue;
			}
		}
		else
		{
			updvalue = (IsOne4Item ? "1" : "0") + ";" + ItemQty + ";" + ItemCost;
			Dic.Add(PccesCode, updvalue);
		}
	}

	private bool CheckCOMSCanDelete()
	{
		bool allow = true;
		if (!SysConfig.SysComsEnable)
		{
			return true;
		}
		if (budgetType == BudgetType.Types.CostEstimation || budgetType == BudgetType.Types.CostQuotationMerged)
		{
			for (int i = 1; i < gridBudget.Rows.Count; i++)
			{
				if (gridBudget.Rows[i].Selected && gridBudget.Rows[i]["CostBeforeChange"] != DBNull.Value)
				{
					MessageBox.Show("預估成本及業主報價模式下不可刪除或剪下已存在原預算書項目的主項大類及工項！");
					return false;
				}
			}
		}
		Dictionary<string, string> PccesCodeDeductuinAmt = new Dictionary<string, string>();
		for (int i = 0; i < gridBudget.Rows.Count; i++)
		{
			if (!gridBudget.Rows[i].Selected || gridBudget.Rows[i]["Sno"] == null)
			{
				continue;
			}
			if (!AllowChangeBySNo(gridBudget.Rows[i]["sNo"], silentOnWarning: true, silentOnModify: true))
			{
				MessageBox.Show("項目" + gridBudget.Rows[i]["cName"].ToString() + "已進入分包規劃不可刪除或剪下!");
				return false;
			}
			if (gridBudget.Rows[i]["Kind"].ToString() == "W")
			{
				SetDelSelItemDic(PccesCodeDeductuinAmt, gridBudget.Rows[i]);
			}
			else
			{
				if (!(gridBudget.Rows[i]["Kind"].ToString() == "B"))
				{
					continue;
				}
				int level = ArchConvert.Obj2Int(gridBudget.Rows[i].Node.Level);
				for (int k = i + 1; k < gridBudget.Rows.Count && ArchConvert.Obj2Int(gridBudget.Rows[k].Node.Level) > level; k++)
				{
					if (!gridBudget.Rows[k].Selected && gridBudget.Rows[k]["Kind"].ToString() == "W")
					{
						SetDelSelItemDic(PccesCodeDeductuinAmt, gridBudget.Rows[k]);
					}
				}
			}
		}
		foreach (KeyValuePair<string, string> par in PccesCodeDeductuinAmt)
		{
			string[] value = par.Value.Split(';');
			string UnitName = ((value[0] == "1") ? "式" : "");
			decimal diffqty = ((UnitName == "式") ? 0m : (ArchConvert.Obj2Decimal(value[1]) * -1m));
			decimal diffcost = ((UnitName == "式") ? (ArchConvert.Obj2Decimal(value[2]) * -1m) : 0m);
			if (!AllowChangeByAccQtyAmtByPccesCode_fordel(par.Key, UnitName, diffqty, diffcost, silentOnWarning: true, silentOnModify: true))
			{
				MessageBox.Show("剪下或刪除後工項:" + par.Key + "之已計價" + ((UnitName == "式") ? "金額" : "數量") + "低於已計價的部分,不能執行剪下或刪除");
				return false;
			}
		}
		return allow;
	}

	private void DeleteItem()
	{
		int iLevel = 1;
		if (!CheckCOMSCanDelete())
		{
			return;
		}
		if (F_NewAddItemFlag == "0" && F_IsNewProject == "")
		{
			if (FormActionName == PccesFormAction.BUD)
			{
				if (GetCurrentBDGT_Type() == "CNT")
				{
					ExecuteCopyToTmpCNT("");
					SetupRestoreSnapshotListCNT();
				}
				else
				{
					ExecuteCopyToTmp("");
					SetupRestoreSnapshotList();
				}
			}
			else
			{
				ExecuteCopyToTmp("");
				SetupRestoreSnapshotList();
			}
			F_NewAddItemFlag = "1";
		}
		if (gridBudget[gridBudget.Row, "SNo"] == null)
		{
			return;
		}
		int iPrevPrintNoLen = 0;
		for (int i = 1; i < gridBudget.Rows.Count; i++)
		{
			if (!gridBudget.Rows[i].Selected)
			{
				continue;
			}
			if (iPrevPrintNoLen > 0 && iPrevPrintNoLen != gridBudget[i, "PrintNo"].ToString().Trim().Length)
			{
				if (DialogResult.No == MessageBox.Show(this, "您選擇的範圍資料，有跨不同階層，如果要執行刪除可能會造成階層不正確\n\n是否要刪除？", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
					return;
				}
				break;
			}
			iPrevPrintNoLen = gridBudget[i, "PrintNo"].ToString().Trim().Length;
		}
		int iSelRows = gridBudget.SelectedRowCount;
		if (iSelRows > dtItemA.Rows.Count)
		{
			for (int i = 1; i < gridBudget.Rows.Count; i++)
			{
				if (gridBudget[i, "Sno"] == null)
				{
					iSelRows = i - 1;
					break;
				}
			}
		}
		if (iSelRows == 0)
		{
			MessageBox.Show(this, "請先選定要刪除的項目！", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return;
		}
		if (MessageBox.Show(this, "確定要刪除選定的 " + iSelRows + " 筆資料？", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			lock (this)
			{
				gridBudget.Enabled = false;
				FormSys_G_Info1 FM_INFO = new FormSys_G_Info1();
				FM_INFO._MaxValue = gridBudget.Rows.Count + PubTools.Str2Int((double)gridBudget.Rows.Count * 0.1);
				FM_INFO._MinValue = 0;
				FM_INFO._ProgressValue = 0;
				FM_INFO._InfoString = "項目刪除中，請稍候! ";
				FM_INFO.Show();
				Application.DoEvents();
				int DeletedCount = 0;
				int LastSelectedRow = gridBudget.RowSel;
				gridBudget.Redraw = false;
				int StartIndex = 0;
				int lockItemCount = 0;
				ExecResult ER = new ExecResult();
				Cursor = Cursors.WaitCursor;
				for (int i = gridBudget.Rows.Count - 1; i > 0; i--)
				{
					Row GridRow = gridBudget.Rows[i];
					if (gridBudget.Rows[i].Selected && gridBudget[i, "Sno"] != null)
					{
						if (GridRow["Lock"] != null && Convert.ToBoolean(GridRow["Lock"]))
						{
							lockItemCount++;
						}
						else
						{
							lock (this)
							{
								int Sno = ArchConvert.Obj2Int(GridRow["sNo"]);
								ER = theItemA.DeleteItemBySno(projectCode, Sno, updateItemNo: false);
								if (ER.ReturnCode == 0)
								{
									DeletedCount++;
								}
								StartIndex = i;
							}
						}
					}
					FM_INFO._ProgressValue++;
					if (i % 5 == 0)
					{
						Application.DoEvents();
					}
				}
				if (DeletedCount > 0)
				{
					int RootSno = 0;
					if (DeletedCount == 1)
					{
						RootSno = ArchConvert.Obj2Int(gridBudget[LastSelectedRow, "ParentSno"]);
					}
					theProject.ReArrangePrintNo(projectCode, RootSno, !IsEditItemNo);
					ReloadGridAtRootSno(RootSno);
				}
				if (gridBudget.Rows.Count > 1 && gridBudget.Rows[1].IsNode && gridBudget[1, "PrintNo"].ToString().Trim() == "99999999999999999999999999999999")
				{
					lock (this)
					{
						int TSno = ArchConvert.Obj2Int(gridBudget[1, "sNo"]);
						theItemA.DeleteItemBySno(projectCode, TSno, updateItemNo: false);
						gridBudget.Rows[1].Node.RemoveNode();
					}
				}
				dbItemA = null;
				int iCount = 0;
				for (int i = 1; i < gridBudget.Rows.Count; i++)
				{
					if (gridBudget[i, "PrintNo"] != null)
					{
						iCount = gridBudget[i, "PrintNo"].ToString().Trim().Length / 4;
						if (gridBudget[i, "PrintNo"].ToString().Trim() == "99999999999999999999999999999999")
						{
							break;
						}
						if (iCount > iLevel)
						{
							iLevel = iCount;
						}
					}
				}
				CheckIsReCal("Y");
				SwitchToCorrectLevelStatus(iLevel);
				FM_INFO._ProgressValue = FM_INFO._MaxValue;
				Application.DoEvents();
				gridBudget.Redraw = true;
				int i9999 = gridBudget.FindRow("99999999999999999999999999999999", 1, gridBudget.Cols["PrintNo"].SafeIndex, caseSensitive: true, fullMatch: false, wrap: false);
				if (i9999 > -1)
				{
					SetStatusBarItemCount(i9999 + 1);
				}
				else
				{
					SetStatusBarItemCount(gridBudget.Rows.Count + 1);
				}
				FM_INFO.Close();
				FM_INFO.Dispose();
				FM_INFO = null;
				gridBudget.Enabled = true;
				if (gridBudget.Rows.Count == 1)
				{
					gridBudget.Rows.Count = 50;
				}
				gridBudget.Refresh();
				Cursor = Cursors.Default;
				Refresh();
				if (lockItemCount > 0)
				{
					if (lockItemCount == 1)
					{
						MessageBox.Show(string.Format("此項目屬於前一版預算書的項目，所以無法刪除", lockItemCount), "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
					else
					{
						MessageBox.Show($"有{lockItemCount}個項目屬於前一版預算書的項目，所以無法刪除", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
				}
				LoadProjectData();
			}
		}
		theProject.ReArrangePrintNo(projectCode, !IsEditItemNo);
	}

	private void ExecuteDecimalSetting()
	{
		FormBudgetDecimal FM_BDGT_DEC = new FormBudgetDecimal(projectCode);
		FM_BDGT_DEC._UserID = userID;
		if (DialogResult.OK == FM_BDGT_DEC.ShowDialog(this))
		{
			CheckIsReCal("Y");
			GetDecimalSetting();
			LoadProjectData();
		}
		FM_BDGT_DEC.Close();
		FM_BDGT_DEC.Dispose();
		FM_BDGT_DEC = null;
	}

	private void ProcessPrintToAnalysis(ref Node theNode)
	{
		Node parentNode = theNode.GetNode(NodeTypeEnum.Parent);
		if (parentNode != null)
		{
			string PrintToAnalysis = gridBudget[parentNode.Row.SafeIndex, "PrintToAnalysis"].ToString().Trim();
			gridBudget[theNode.Row.SafeIndex, "PrintToAnalysis"] = PrintToAnalysis;
			string PrintNo = gridBudget[theNode.Row.SafeIndex, "PrintNo"].ToString().Trim();
			theItemA.UpdateItemAPrintToAnalysisByParentPrintNo(projectCode, PrintNo, PrintToAnalysis);
		}
	}

	private bool IsCostStructureRow(int RowIndex, bool thisRowOnly)
	{
		if (RowIndex > 0 && RowIndex < gridBudget.Rows.Count)
		{
			Row row = gridBudget.Rows[RowIndex];
			if (!thisRowOnly && ArchConvert.Obj2String(row["Kind"]).ToUpper() != "B" && row.Node.GetNode(NodeTypeEnum.Parent) != null)
			{
				row = row.Node.GetNode(NodeTypeEnum.Parent).Row;
			}
			if (row != null)
			{
				object Value = row["CostUID"];
				if (Value != null && Value != DBNull.Value && Value.ToString() != string.Empty)
				{
					return true;
				}
			}
		}
		return false;
	}

	private void DoUp()
	{
		if (gridBudget[gridBudget.Row + 1, "Kind"] == null || IsCostStructureRow(gridBudget.Row, thisRowOnly: true))
		{
			return;
		}
		ExecResult ER = new ExecResult();
		int Sno = ArchConvert.Obj2Int(gridBudget.Rows[gridBudget.Row]["sNo"]);
		ER = theItemA.GridNodeMoveUpDown(projectCode, Sno, 1, !IsEditItemNo);
		if (ER.ReturnCode != 0)
		{
			if (ER.Message != "沒有可交換對象")
			{
				MessageBox.Show(ER.Message);
			}
		}
		else
		{
			ReloadGridAtRootSno(ArchConvert.Obj2Int(gridBudget.Rows[gridBudget.Row]["ParentSno"]));
			SetGridFocusBySno(Sno, NeedAtTop: false);
		}
		theProject.ReArrangePrintNo(projectCode, !IsEditItemNo);
		LoadProjectData();
	}

	private void DoDown()
	{
		if (gridBudget[gridBudget.Row + 2, "Kind"] == null || IsCostStructureRow(gridBudget.Row, thisRowOnly: true))
		{
			return;
		}
		ExecResult ER = new ExecResult();
		int Sno = ArchConvert.Obj2Int(gridBudget.Rows[gridBudget.Row]["sNo"]);
		ER = theItemA.GridNodeMoveUpDown(projectCode, Sno, 2, !IsEditItemNo);
		if (ER.ReturnCode != 0)
		{
			if (ER.Message != "沒有可交換對象")
			{
				MessageBox.Show(ER.Message);
			}
		}
		else
		{
			ReloadGridAtRootSno(ArchConvert.Obj2Int(gridBudget.Rows[gridBudget.Row]["ParentSno"]));
			SetGridFocusBySno(Sno, NeedAtTop: false);
		}
		theProject.ReArrangePrintNo(projectCode, !IsEditItemNo);
		LoadProjectData();
	}

	private void DoIndent()
	{
		if (budgetType == BudgetType.Types.CostEstimation || budgetType == BudgetType.Types.CostQuotationMerged)
		{
			return;
		}
		List<string> SelectedItems = new List<string>();
		for (int i = 1; i < gridBudget.Rows.Count; i++)
		{
			if (gridBudget.Rows[i].Selected)
			{
				SelectedItems.Add(gridBudget[i, "sNo"].ToString());
			}
		}
		for (int j = 0; j < SelectedItems.Count; j++)
		{
			for (int i = 1; i < gridBudget.Rows.Count; i++)
			{
				if (gridBudget[i, "sNo"].ToString() != SelectedItems[j])
				{
					continue;
				}
				if (!AllowChangeBySNo(gridBudget[i, "sNo"], silentOnWarning: false, silentOnModify: false) || ArchConvert.Obj2Bool(gridBudget[i, "Lock"]) || IsCostStructureRow(i, thisRowOnly: true) || gridBudget[gridBudget.Row, "PrintNo"] == null || gridBudget[i, "PrintNo"].ToString().Trim() == "")
				{
					return;
				}
				ExecResult ER = new ExecResult();
				int Sno = ArchConvert.Obj2Int(gridBudget[i, "sNo"]);
				ER = theItemA.GridNodeIndent(projectCode, Sno, !IsEditItemNo);
				if (ER.ReturnCode != 0)
				{
					MessageBox.Show(ER.Message);
					break;
				}
				int ReloadRootSno = ArchConvert.Obj2Int(gridBudget[i, "ParentSno"]);
				ReloadGridAtRootSno(ReloadRootSno);
				SetGridFocusBySno(Sno, NeedAtTop: false);
				break;
			}
		}
		theProject.ReArrangePrintNo(projectCode, !IsEditItemNo);
		LoadProjectData();
	}

	private void DoOutdent()
	{
		if (budgetType == BudgetType.Types.CostEstimation || budgetType == BudgetType.Types.CostQuotationMerged)
		{
			return;
		}
		List<string> SelectedItems = new List<string>();
		for (int i = 1; i < gridBudget.Rows.Count; i++)
		{
			if (gridBudget.Rows[i].Selected)
			{
				SelectedItems.Add(gridBudget[i, "sNo"].ToString());
			}
		}
		for (int j = 0; j < SelectedItems.Count; j++)
		{
			for (int i = 1; i < gridBudget.Rows.Count; i++)
			{
				if (gridBudget[i, "sNo"] == null || gridBudget[i, "sNo"].ToString() != SelectedItems[j] || !AllowChangeBySNo(gridBudget[i, "sNo"], silentOnWarning: false, silentOnModify: false) || ArchConvert.Obj2Bool(gridBudget[i, "Lock"]) || IsCostStructureRow(i, thisRowOnly: true) || gridBudget[i, "PrintNo"] == null || gridBudget[i, "PrintNo"].ToString().Trim() == "")
				{
					continue;
				}
				ExecResult ER = new ExecResult();
				int Sno = ArchConvert.Obj2Int(gridBudget[i, "sNo"]);
				ER = theItemA.GridNodeOutdent(projectCode, Sno, !IsEditItemNo);
				if (ER.ReturnCode != 0)
				{
					MessageBox.Show(ER.Message);
					break;
				}
				int ReloadRootSno = ArchConvert.Obj2Int(gridBudget[i, "ParentSno"]);
				if (ReloadRootSno != 0)
				{
					ReloadRootSno = ArchConvert.Obj2Int(gridBudget[GetRowIndexBySno(ReloadRootSno), "ParentSno"]);
				}
				ReloadGridAtRootSno(ReloadRootSno);
				SetGridFocusBySno(Sno, NeedAtTop: false);
				break;
			}
		}
		theProject.ReArrangePrintNo(projectCode, !IsEditItemNo);
		LoadProjectData();
	}

	private bool CheckLevelLimit(int sourceRowNumber, int targetRowNumber)
	{
		if (targetRowNumber == 0)
		{
			return true;
		}
		Row targetRow = gridBudget.Rows[targetRowNumber];
		int startLevel = gridBudget.Rows[sourceRowNumber].Node.Level;
		int maximumLevel = startLevel;
		for (int rowIndex = sourceRowNumber + 1; rowIndex < gridBudget.Rows.Count; rowIndex++)
		{
			int nodeLevel = gridBudget.Rows[rowIndex].Node.Level;
			if (nodeLevel <= startLevel)
			{
				break;
			}
			if (nodeLevel > maximumLevel)
			{
				maximumLevel = nodeLevel;
			}
		}
		return targetRow.Node.Level + (maximumLevel - startLevel + 1) <= 8;
	}

	private int FindIndexTargetRowIndex(int sourceRowNumber)
	{
		for (int rowIndex = sourceRowNumber - 1; rowIndex > 0; rowIndex--)
		{
			if (ArchConvert.Obj2String(gridBudget.Rows[rowIndex]["kind"]).Trim() == "B")
			{
				return rowIndex;
			}
		}
		return 0;
	}

	private bool MoveNode2Parent(int iRow, Node ndSrc)
	{
		if (iRow == gridBudget.Rows.Fixed)
		{
			return false;
		}
		if (gridBudget[iRow - 1, "Kind"] == null)
		{
			return false;
		}
		if (gridBudget[iRow - 1, "Kind"].ToString().Trim() == "B")
		{
			Node ParentNode = ndSrc.GetNode(NodeTypeEnum.Parent);
			if (ParentNode.Row.Index != iRow - 1)
			{
				Node ndDst = gridBudget.Rows[iRow - 1].Node;
				ndSrc.Move(NodeMoveEnum.ChildOf, ndDst);
				ndSrc.Move(NodeMoveEnum.Last);
				gridBudget.Select();
				return true;
			}
			return false;
		}
		return MoveNode2Parent(iRow - 1, ndSrc);
	}

	private int GetRealIndexBySNo(int sNO)
	{
		int RetV = -1;
		return gridBudget.FindRow(sNO, 1, gridBudget.Cols["SNo"].SafeIndex, wrap: false);
	}

	private void SetShowOnlyChangedItemToolbarStatus()
	{
		string[] buttonList = new string[12]
		{
			"Cut", "Copy", "Paste", "InsertMainItem", "InsertWorkItem", "Delete", "CloneWorkItem", "MoveUp", "MoveDown", "Indent",
			"Outdent", "RearrangeItemNo"
		};
		SetButtonListAvailibility(buttonList, !showOnlyChangedItem);
		toolbarsManager.Tools["Paste"].SharedProps.Enabled = dtClipboard.Rows.Count > 0 && !showOnlyChangedItem;
	}

	private void OpenBudgetChangeResponsibilityDialog()
	{
		FormBudgetChangeResponsibility formBudgetChangeResponsibility = new FormBudgetChangeResponsibility(projectCode, budgetChangeCurrentVersion, ArchConvert.Obj2Int(gridBudget[gridBudget.Row, "SNo"]));
		formBudgetChangeResponsibility.ItemNo = ArchConvert.Obj2String(gridBudget[gridBudget.Row, "ItemNo"]);
		formBudgetChangeResponsibility.ItemName = ArchConvert.Obj2String(gridBudget[gridBudget.Row, "cName"]);
		if (formBudgetChangeResponsibility.ShowDialog() == DialogResult.OK)
		{
			gridBudget[gridBudget.Row, "Qty"] = ArchConvert.Obj2Double(gridBudget[gridBudget.Row, "Qty"]) + (formBudgetChangeResponsibility.TotalQty - formBudgetChangeResponsibility.OriginalQty);
			gridBudget[gridBudget.Row, "BudgetChangeAddQty"] = ArchConvert.Obj2Decimal(gridBudget[gridBudget.Row, "Qty"]) - ArchConvert.Obj2Decimal(gridBudget[gridBudget.Row, "QtyBeforeChange"]);
			UpdateSelectedRow(gridBudget.Row, null);
		}
		formBudgetChangeResponsibility.Dispose();
		formBudgetChangeResponsibility = null;
	}

	private void ReloadFromCostEst()
	{
		DialogResult result = MessageBox.Show(this, "確定重新自預估成本載入？", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
		if (result != DialogResult.No)
		{
			BudProject budProject = new BudProject();
			ExecResult ER = budProject.ReloadFromCostEst(projectCode);
			if (ER.ReturnCode != 0)
			{
				MessageBox.Show(ER.Message);
			}
			else
			{
				LoadProjectData();
			}
		}
	}

	private void ProduceSourceCostQuoteReport()
	{
		saveFileDialog1.Filter = "Microsoft Excel (*.xls)|*.xls";
		saveFileDialog1.RestoreDirectory = true;
		saveFileDialog1.FileName = parentProjectCode + " 歷次報價詳細表";
		if (saveFileDialog1.ShowDialog() == DialogResult.OK)
		{
			BudgetCostQuotationMergedReport reporter = new BudgetCostQuotationMergedReport();
			ExecResult ER = reporter.ProduceSourceCostQuoteReport(saveFileDialog1.FileName, projectCode);
			if (ER.ReturnCode != 0)
			{
				MessageBox.Show(ER.Message);
			}
			else
			{
				MessageBox.Show("產出歷次報價成功！");
			}
		}
	}

	private void ProduceBudgetCostEstAndQuoteReport()
	{
		saveFileDialog1.Filter = "Microsoft Excel (*.xls)|*.xls";
		saveFileDialog1.RestoreDirectory = true;
		saveFileDialog1.FileName = string.Format("{0}第{1}期工程估價單{2}.xls", projectName, changeManagementCurrentVersion, (budgetType == BudgetType.Types.CostEstimation) ? "(成本)" : string.Empty);
		if (saveFileDialog1.ShowDialog() == DialogResult.OK)
		{
			BudgetCostEstimationReport report = new BudgetCostEstimationReport();
			ExecResult ER = report.ProduceBudgetCostEstimationReport((int)budgetType, saveFileDialog1.FileName, projectCode, projectName);
			if (ER.ReturnCode != 0)
			{
				MessageBox.Show(ER.Message);
			}
			else
			{
				MessageBox.Show("產出工程估價單成功！");
			}
		}
	}

	private void ProduceBudgetDesingChangeReport()
	{
		saveFileDialog1.Filter = "Microsoft Excel (*.xls)|*.xls";
		saveFileDialog1.RestoreDirectory = true;
		saveFileDialog1.FileName = $"{projectName}第{changeManagementCurrentVersion}期業主變更設計報價單.xls";
		if (saveFileDialog1.ShowDialog() == DialogResult.OK)
		{
			BudgetDesignChangeReport report = new BudgetDesignChangeReport();
			ExecResult ER = report.ProduceBudgetDesignChangeReport(saveFileDialog1.FileName, projectCode, parentProjectCode, ProjectName);
			if (ER.ReturnCode != 0)
			{
				MessageBox.Show(ER.Message);
			}
			else
			{
				MessageBox.Show("產出業主變更設計報價單成功！");
			}
		}
	}

	private void DeleteBudItemAZeroQtyItem()
	{
		if (MessageBox.Show("是否要刪除數量為零的工項？系統會先備份一版在預算書版本中", "請確認", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
		{
			return;
		}
		if (FormActionName == PccesFormAction.BUD)
		{
			if (GetCurrentBDGT_Type() == "CNT")
			{
				ExecuteCopyToTmpCNT("");
				SetupRestoreSnapshotListCNT();
			}
			else
			{
				ExecuteCopyToTmp("");
				SetupRestoreSnapshotList();
			}
		}
		else
		{
			ExecuteCopyToTmp("");
			SetupRestoreSnapshotList();
		}
		BudItemA budItemA = new BudItemA();
		ExecResult ER = budItemA.DeleteBudItemAZeroQtyItem(projectCode);
		if (ER.ReturnCode != 0)
		{
			MessageBox.Show(ER.Message);
		}
		theProject.ReArrangePrintNo(projectCode, !IsEditItemNo);
		ReloadGridAtRootSno(0);
		Th_ReCal_All(Auto: true);
	}

	private void ProduceExecutiveBudgetSummaryReport()
	{
		saveFileDialog1.Filter = "Microsoft Excel (*.xls)|*.xls";
		saveFileDialog1.RestoreDirectory = true;
		saveFileDialog1.FileName = "執行預算變更.xls";
		bool IsSetCostEmpty = false;
		if (MessageBox.Show(this, "是否不列印單價?", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
		{
			IsSetCostEmpty = true;
		}
		if (saveFileDialog1.ShowDialog() == DialogResult.OK)
		{
			ExecutiveBudgetSummaryReport report = new ExecutiveBudgetSummaryReport();
			ExecResult ER = report.ProduceExecutiveBudgetSummaryReport(saveFileDialog1.FileName, projectCode, projectName, budgetChangeCurrentVersion, IsSetCostEmpty);
			if (ER.ReturnCode != 0)
			{
				MessageBox.Show(ER.Message);
				return;
			}
			FormOpenExcel _OpenExcel = new FormOpenExcel();
			_OpenExcel.filepath = saveFileDialog1.FileName;
			_OpenExcel.ResetLable();
			_OpenExcel.ShowDialog();
			_OpenExcel.Close();
			_OpenExcel.Dispose();
		}
	}

	private void ProduceExecutiveBudgetDetailReport()
	{
		saveFileDialog1.Filter = "Microsoft Excel (*.xls)|*.xls";
		saveFileDialog1.RestoreDirectory = true;
		saveFileDialog1.FileName = "本次執行預算變更.xls";
		bool IsSetCostEmpty = false;
		if (MessageBox.Show(this, "是否不列印單價?", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
		{
			IsSetCostEmpty = true;
		}
		if (saveFileDialog1.ShowDialog() == DialogResult.OK)
		{
			ExecutiveBudgetDetailReport report = new ExecutiveBudgetDetailReport();
			ExecResult ER = report.ProduceExecutiveBudgetDetailReport(saveFileDialog1.FileName, projectCode, projectName, budgetChangeCurrentVersion, IsSetCostEmpty);
			if (ER.ReturnCode != 0)
			{
				MessageBox.Show(ER.Message);
				return;
			}
			FormOpenExcel _OpenExcel = new FormOpenExcel();
			_OpenExcel.filepath = saveFileDialog1.FileName;
			_OpenExcel.ResetLable();
			_OpenExcel.ShowDialog();
			_OpenExcel.Close();
			_OpenExcel.Dispose();
		}
	}

	private void ProduceComsAccAlertReport()
	{
		saveFileDialog1.Filter = "Microsoft Excel (*.xls)|*.xls";
		saveFileDialog1.RestoreDirectory = true;
		saveFileDialog1.FileName = "COMS已計價超預算異常報表.xls";
		if (saveFileDialog1.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		Archnowledge.Pcces.DomainModule.Coms.BudgetCtrl theBudgetCtrl = new Archnowledge.Pcces.DomainModule.Coms.BudgetCtrl();
		DataTable dt = theBudgetCtrl.GetComsAccAlertReport(projectCode);
		if (dt.Columns.Contains("工項編碼"))
		{
			DataTableToExcel xls = new DataTableToExcel();
			ExecResult ER = xls.GeneralDataTableToExcel(saveFileDialog1.FileName, AppDomain.CurrentDomain.BaseDirectory + "ReportTemplate\\Simple.xls", dt);
			if (ER.ReturnCode != 0)
			{
				MessageBox.Show(ER.Message);
				return;
			}
			FormOpenExcel _OpenExcel = new FormOpenExcel();
			_OpenExcel.filepath = saveFileDialog1.FileName;
			_OpenExcel.ResetLable();
			_OpenExcel.ShowDialog();
			_OpenExcel.Close();
			_OpenExcel.Dispose();
		}
		else
		{
			MessageBox.Show("COMS已計價超預算異常報表資料查詢失敗");
		}
	}

	private void ProduceBudgetAccDiffReport()
	{
		saveFileDialog1.Filter = "Microsoft Excel (*.xls)|*.xls";
		saveFileDialog1.RestoreDirectory = true;
		saveFileDialog1.FileName = "預算實作差異比較報表.xls";
		if (saveFileDialog1.ShowDialog() == DialogResult.OK)
		{
			ExecutiveBudgetDiff theExecutiveBudgetDiff = new ExecutiveBudgetDiff();
			ExecResult ER = theExecutiveBudgetDiff.ProduceExecutiveBudgetDetailReport(saveFileDialog1.FileName, projectCode);
			if (ER.ReturnCode != 0)
			{
				MessageBox.Show(ER.Message);
				return;
			}
			FormOpenExcel _OpenExcel = new FormOpenExcel();
			_OpenExcel.filepath = saveFileDialog1.FileName;
			_OpenExcel.ResetLable();
			_OpenExcel.ShowDialog();
			_OpenExcel.Close();
			_OpenExcel.Dispose();
		}
	}

	private void BudgetChangeInfoReport()
	{
		BudExeProject theBudExeProject = new BudExeProject();
		UserDefined userDefined = new UserDefined();
		BudgetChangeResponsibility budgetChangeResponsibility = new BudgetChangeResponsibility();
		DataSet dsBudExeProject = theBudExeProject.GetProject(projectCode);
		DataSet dsBudExeProjectResponsibility = budgetChangeResponsibility.GetBudgetChangeResponsibilityWithoutSno(projectCode);
		DataSet dsDepartment = userDefined.GetUserDefinedByKind("BudgetChangeResponsibility");
		saveFileDialog1.Filter = "Microsoft Excel (*.xls)|*.xls";
		saveFileDialog1.RestoreDirectory = true;
		saveFileDialog1.FileName = "預算變更資訊.xls";
		if (saveFileDialog1.ShowDialog() == DialogResult.OK)
		{
			BudgetChangeInfoReport report = new BudgetChangeInfoReport();
			ExecResult ER = report.ProduceExecutiveBudgetChangeInfoReport(saveFileDialog1.FileName, projectCode, dsBudExeProject, dsBudExeProjectResponsibility, dsDepartment);
			if (ER.ReturnCode != 0)
			{
				MessageBox.Show(ER.Message);
			}
			else
			{
				MessageBox.Show("產出執行預算成功！");
			}
		}
	}

	private void RememberColsProps()
	{
		IsCollaspse = new bool[gridBudget.Rows.Count];
		for (int i = 1; i < gridBudget.Rows.Count; i++)
		{
			if (gridBudget[i, "IsCollaspse"] == null)
			{
				IsCollaspse[i] = false;
			}
			else
			{
				IsCollaspse[i] = (bool)gridBudget[i, "IsCollaspse"];
			}
		}
		for (int i = 0; i < GridCols; GridColsSquence[i, 7] = gridBudget.Cols[i].TextAlign, GridColsSquence[i, 8] = gridBudget.Cols[i].AllowDragging, GridColsSquence[i, 9] = gridBudget.Cols[i].AllowResizing, i++)
		{
			GridColsSquence[i, 0] = gridBudget.Cols[i].Name;
			GridColsSquence[i, 1] = gridBudget.Cols[i].Caption;
			GridColsSquence[i, 2] = gridBudget.Cols[i].Width;
			if (gridBudget.Cols[i].Name == "AnaImg")
			{
				GridColsSquence[i, 3] = typeof(Image);
			}
			else
			{
				GridColsSquence[i, 3] = gridBudget.Cols[i].DataType;
			}
			GridColsSquence[i, 4] = gridBudget.Cols[i].Visible;
			GridColsSquence[i, 5] = gridBudget.Cols[i].Format;
			GridColsSquence[i, 6] = gridBudget.Cols[i].AllowEditing;
			string name = gridBudget.Cols[i].Name;
			int precision = 0;
			switch (name)
			{
			case "Qty":
			case "BudgetChangeAddQty":
			case "QtyBeforeChange":
				precision = MainItemQtyPrecision;
				break;
			case "Cost":
				precision = MainItemCostPrecison;
				break;
			case "Amount":
			case "AmountBeforeChange":
				precision = MainItemAmountPrecisionDec;
				break;
			}
			switch (name)
			{
			default:
				if (!(name == "BudgetChangeAddQty"))
				{
					continue;
				}
				break;
			case "Qty":
			case "Cost":
			case "Amount":
			case "QtyBeforeChange":
			case "AmountBeforeChange":
				break;
			}
			if (precision != 0)
			{
				GridColsSquence[i, 5] = "###,###,###,##0." + "0".PadLeft(precision, '0');
			}
			else
			{
				GridColsSquence[i, 5] = "###,###,###,##0";
			}
		}
	}

	private void Execute_SwitchProject()
	{
		string SwitchProjectCode = projectCode;
		Cursor = Cursors.WaitCursor;
		lock (this)
		{
			FormBudgetProjectPick FM_BDGT_PPK1 = new FormBudgetProjectPick();
			FM_BDGT_PPK1.CallUpType = FormBudget_PickType.ProjectSwitch;
			FM_BDGT_PPK1._ActionName = FormActionName;
			FM_BDGT_PPK1._UserID = userID;
			if (FM_BDGT_PPK1.ShowDialog(this) == DialogResult.OK)
			{
				ReleaseSingleEditCtrlLock(SwitchProjectCode);
				Is_SwitchProject = true;
				frmBudget_Load(this, EventArgs.Empty);
				Is_SwitchProject = false;
			}
			FM_BDGT_PPK1.Close();
			FM_BDGT_PPK1.Dispose();
			FM_BDGT_PPK1 = null;
		}
		Cursor = Cursors.Default;
	}

	private void PreSetLevel()
	{
		if (gridBudget.Rows.Count <= 1 || gridBudget.Rows[1].Node == null)
		{
			return;
		}
		int iMinusLevel = gridBudget.Rows[1].Node.Level - 1;
		if (iMinusLevel <= 0)
		{
			return;
		}
		for (int i = 1; i < gridBudget.Rows.Count; i++)
		{
			if (gridBudget.Rows[i].IsNode)
			{
				gridBudget.Rows[i].Node.Level = gridBudget.Rows[i].Node.Level - iMinusLevel;
			}
		}
	}

	private ExecResult ArrangePrintNoByGrid(bool All)
	{
		string sIsEidtNumber = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "BDGT", "IsEidtNumber");
		if (sIsEidtNumber.ToUpper() != "TRUE")
		{
			return ArrangePrintNoByGrid(All, RearrangeItemNo: true, 1, DragDropUpdate: false);
		}
		return ArrangePrintNoByGrid(All, RearrangeItemNo: false, 1, DragDropUpdate: false);
	}

	private ExecResult ArrangePrintNoByGrid(bool All, int StartRowIndex)
	{
		string sIsEidtNumber = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "BDGT", "IsEidtNumber");
		if (sIsEidtNumber.ToUpper() != "TRUE")
		{
			return ArrangePrintNoByGrid(All, RearrangeItemNo: true, StartRowIndex, DragDropUpdate: false);
		}
		return ArrangePrintNoByGrid(All, RearrangeItemNo: false, StartRowIndex, DragDropUpdate: false);
	}

	private ExecResult ArrangePrintNoByGrid(bool All, int StartRowIndex, bool DragDropUpdate)
	{
		string sIsEidtNumber = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "BDGT", "IsEidtNumber");
		if (sIsEidtNumber.ToUpper() != "TRUE")
		{
			return ArrangePrintNoByGrid(All, RearrangeItemNo: true, StartRowIndex, DragDropUpdate);
		}
		return ArrangePrintNoByGrid(All, RearrangeItemNo: false, StartRowIndex, DragDropUpdate);
	}

	private ExecResult ArrangePrintNoByGrid(bool All, bool RearrangeItemNo, int StartRowIndex, bool DragDropUpdate)
	{
		ExecResult ER = new ExecResult();
		if (false)
		{
			return ER;
		}
		PreSetLevel();
		int[] array = new int[9];
		int[] PrintNoLevelIndex = array;
		DataSet dsItemAforProntNo = theItemA.GetPrintNo(projectCode);
		DataView dvItemAforProntNo = new DataView(dsItemAforProntNo.Tables[0]);
		dvItemAforProntNo.Sort = "PrintNo";
		string CurPrintNo = "";
		string ParentPrintNo = "";
		int NextNum = 0;
		int CurrentLevel = 1;
		if (StartRowIndex < 1)
		{
			StartRowIndex = 1;
		}
		if (StartRowIndex > 1)
		{
			if (gridBudget.Rows[StartRowIndex]["PrintNo"] != null)
			{
				CurPrintNo = gridBudget.Rows[StartRowIndex]["PrintNo"].ToString().Trim();
				ParentPrintNo = ((CurPrintNo.Length != 4) ? CurPrintNo.Substring(0, CurPrintNo.Length - 4) : "");
				string LastNum = CurPrintNo.Substring(CurPrintNo.Length - 4);
				NextNum = int.Parse(LastNum);
				if (gridBudget.Rows[StartRowIndex].Node != null)
				{
					CurrentLevel = gridBudget.Rows[StartRowIndex].Node.Level;
				}
				StartRowIndex++;
			}
			else
			{
				ER.ReturnCode = 5;
				ER.Message = StartRowIndex + " 的 PrintNo 為 Null";
			}
		}
		if (ER.ReturnCode == 0)
		{
			try
			{
				bool StartChange = false;
				for (int i = StartRowIndex; i < gridBudget.Rows.Count; i++)
				{
					if (!gridBudget.Rows[i].IsNode || (gridBudget[i, "PrintNo"] != null && gridBudget[i, "PrintNo"].ToString() == "99999999999999999999999999999999"))
					{
						continue;
					}
					int RowLevel = 1;
					if (gridBudget.Rows[i].Node != null)
					{
						RowLevel = gridBudget.Rows[i].Node.Level;
					}
					if (RowLevel <= 0)
					{
						RowLevel = 1;
					}
					if (RowLevel > CurrentLevel)
					{
						ParentPrintNo = CurPrintNo;
						NextNum = 1;
						CurrentLevel++;
						PrintNoLevelIndex[CurrentLevel - 1] = 0;
					}
					else if (RowLevel < CurrentLevel)
					{
						int DownLevel = 4 * (CurrentLevel - RowLevel);
						if (ParentPrintNo.Length < DownLevel)
						{
							ER.Message = "ArrangePrintNoByGrid Error " + ArchConvert.Obj2String(gridBudget.Rows[i]["ItemNo"]) + "  " + ArchConvert.Obj2String(gridBudget.Rows[i]["cName"]) + " 之前的項次有問題，無法重排項次。";
							ER.ReturnCode = 1;
							break;
						}
						string LastNum = ParentPrintNo.Substring(ParentPrintNo.Length - DownLevel, 4);
						NextNum = int.Parse(LastNum) + 1;
						ParentPrintNo = ParentPrintNo.Substring(0, ParentPrintNo.Length - DownLevel);
						CurrentLevel = RowLevel;
					}
					else
					{
						NextNum++;
					}
					bool NeedChange = false;
					CurPrintNo = ParentPrintNo + NextNum.ToString().PadLeft(4, '0');
					if (All)
					{
						NeedChange = true;
					}
					else if (gridBudget[i, "PrintNo"] == null || gridBudget[i, "LevelNo"] == null || gridBudget[i, "PrintNo"].ToString() != CurPrintNo || (int)gridBudget[i, "LevelNo"] != RowLevel)
					{
						StartChange = true;
						NeedChange = true;
					}
					else
					{
						NeedChange = false;
						if (DragDropUpdate)
						{
							StartChange = false;
							DragDropUpdate = false;
						}
						if (StartChange)
						{
							break;
						}
					}
					if (NeedChange)
					{
						string Sno = ArchConvert.Obj2String(gridBudget[i, "Sno"]);
						if (Sno == "")
						{
							break;
						}
						dvItemAforProntNo.RowFilter = "Sno = " + Sno;
						if (dvItemAforProntNo.Count > 0)
						{
							gridBudget[i, "PrintNo"] = CurPrintNo;
							gridBudget[i, "LevelNo"] = RowLevel;
							if (ArchConvert.Obj2String(dvItemAforProntNo[0]["PrintNo"]) != CurPrintNo)
							{
								dvItemAforProntNo[0]["PrintNo"] = CurPrintNo;
							}
							if (ArchConvert.Obj2Int(dvItemAforProntNo[0]["LevelNo"]) != RowLevel)
							{
								dvItemAforProntNo[0]["LevelNo"] = RowLevel;
							}
							if (!RearrangeItemNo)
							{
								continue;
							}
							string Kind = ArchConvert.Obj2String(gridBudget[i, "Kind"]).Trim();
							string PccesCode = ArchConvert.Obj2String(gridBudget[i, "PccesCode"]).Trim();
							if (Kind != "Z" && (!(Kind == "W") || !(PccesCode != "") || PccesCode[0] != '#'))
							{
								string ItemNo;
								if (StartRowIndex == 1)
								{
									PrintNoLevelIndex[RowLevel - 1]++;
									string TempPrintNo = GetTempPrintNoByPrintNoLevelIndex(PrintNoLevelIndex, RowLevel);
									ItemNo = theItemNoSettingManager.GetItemNoByPrintNo(TempPrintNo, Kind);
								}
								else
								{
									ItemNo = theItemNoSettingManager.GetItemNoByPrintNo(CurPrintNo, Kind);
								}
								gridBudget[i, "ItemNo"] = ItemNo;
								if (ItemNo != "" && ArchConvert.Obj2String(dvItemAforProntNo[0]["ItemNo"]) != ItemNo)
								{
									dvItemAforProntNo[0]["ItemNo"] = ItemNo;
								}
							}
						}
						else
						{
							ER.Message = "ArrangePrintNoByGrid 錯誤，找不到 Sno = " + Sno;
							ER.ReturnCode = 1;
							MessageBox.Show(ER.Message);
						}
					}
					else
					{
						PrintNoLevelIndex[RowLevel - 1]++;
					}
				}
			}
			catch (Exception ex)
			{
				ER.Message = "ArrangePrintNoByGrid Error : " + ex.Message;
				ER.ReturnCode = 5;
			}
		}
		if (ER.ReturnCode == 0)
		{
			try
			{
				ER = CheckReArrangePrintNoIsValid(dsItemAforProntNo);
			}
			catch (Exception ex2)
			{
				ER.Message = "檢查PrintNo時發生意外錯誤 : " + ex2.Message;
				ER.ReturnCode = 1;
			}
		}
		if (ER.ReturnCode == 0)
		{
			try
			{
				ER = CheckItemWUnderW(dsItemAforProntNo);
				if (ER.ReturnCode == 0)
				{
					DataColumn[] PK = new DataColumn[1] { dsItemAforProntNo.Tables[0].Columns["PrintNo"] };
					dsItemAforProntNo.Tables[0].PrimaryKey = PK;
					ER = theItemA.GetDatasetUpdateForPrintNo(dsItemAforProntNo);
					dsItemAforProntNo.Tables[0].PrimaryKey = null;
					dsItemAforProntNo.AcceptChanges();
				}
			}
			catch (Exception ex)
			{
				ER.Message = "PrintNo 不是唯一，資料不正確 : " + ex.Message;
				ER.ReturnCode = 1;
			}
		}
		if (ER.ReturnCode != 0)
		{
			MessageBox.Show("重新載入資料，因為更新失敗 : " + ER.Message);
			RemoveInsertFailedRow();
		}
		gridBudget.Select();
		if (gridBudget.Rows.Count > 0)
		{
			gridBudget.AfterSelChange -= gridBudget1_AfterSelChange;
			gridBudget.Col = 0;
			gridBudget.Row = 0;
			gridBudget.AfterSelChange += gridBudget1_AfterSelChange;
		}
		return ER;
	}

	private string GetTempPrintNoByPrintNoLevelIndex(int[] PrintNoLevelIndex, int RowLevel)
	{
		if (RowLevel == 0)
		{
			return string.Empty;
		}
		return GetTempPrintNoByPrintNoLevelIndex(PrintNoLevelIndex, RowLevel - 1) + PrintNoLevelIndex[RowLevel - 1].ToString().PadLeft(4, '0');
	}

	private ExecResult CheckItemWUnderW(DataSet dsItemAforProntNo)
	{
		ExecResult ER = new ExecResult();
		DataView dvItemAforProntNo = new DataView(dsItemAforProntNo.Tables[0]);
		DataView dvCheckW = new DataView(dsItemAforProntNo.Tables[0]);
		dvItemAforProntNo.RowFilter = "Kind = 'W'";
		for (int i = 0; i < dvItemAforProntNo.Count; i++)
		{
			string PrintNo = dvItemAforProntNo[i]["PrintNo"].ToString();
			dvCheckW.RowFilter = "PrintNo like '" + PrintNo + "%'";
			if (dvCheckW.Count > 1)
			{
				ER.ReturnCode = 99;
				ER.Message = "PrintNo = " + PrintNo + " 的工作要項下，不可以包含其他工作要項";
				break;
			}
		}
		dvItemAforProntNo.Dispose();
		dvItemAforProntNo = null;
		dvCheckW.Dispose();
		dvCheckW = null;
		return ER;
	}

	private ExecResult CheckReArrangePrintNoIsValid(DataSet dsItemAforProntNo)
	{
		ExecResult ER = new ExecResult();
		ER.ReturnCode = 0;
		DataView dvItemAforProntNo = new DataView(dsItemAforProntNo.Tables[0]);
		dvItemAforProntNo.Sort = "PrintNo";
		string PrevPrintNo = "";
		string ErrMsg = string.Empty;
		for (int i = 0; i < dvItemAforProntNo.Count; i++)
		{
			string PrintNo = dvItemAforProntNo[i]["PrintNo"].ToString().Trim();
			if (PrintNo == PrevPrintNo)
			{
				ErrMsg = ErrMsg + "PrintNo重複:" + PrintNo + "\r";
			}
			if (PrintNo.Length == PrevPrintNo.Length && !PrintNo.Contains("999999"))
			{
				int diff = 0;
				diff = ((PrintNo.Length <= 4) ? (ArchConvert.Obj2Int(PrintNo) - ArchConvert.Obj2Int(PrevPrintNo)) : ((!(PrintNo.Substring(0, PrintNo.Length - 4) != PrevPrintNo.Substring(0, PrevPrintNo.Length - 4))) ? (ArchConvert.Obj2Int(PrintNo.Substring(PrintNo.Length - 4, 4)) - ArchConvert.Obj2Int(PrevPrintNo.Substring(PrevPrintNo.Length - 4, 4))) : (-1)));
				if (diff != 1)
				{
					ErrMsg = ErrMsg + "PrintNo未連續於:" + PrintNo + "\r";
				}
			}
			if (PrintNo.Length > PrevPrintNo.Length && PrintNo != PrevPrintNo + "0001" && !PrintNo.Contains("999999"))
			{
				string text = ErrMsg;
				ErrMsg = text + "下展一階PrintNo異常,於:" + PrintNo + "自PrintNo:" + PrevPrintNo + "往下跳\r";
			}
			string tmpPrintNo = PrevPrintNo;
			if (PrintNo.Length < PrevPrintNo.Length && !PrintNo.Contains("999999"))
			{
				while (PrintNo.Length < tmpPrintNo.Length)
				{
					tmpPrintNo = tmpPrintNo.Substring(0, tmpPrintNo.Length - 4);
				}
				int diff2 = 0;
				diff2 = ((tmpPrintNo.Length <= 4) ? (ArchConvert.Obj2Int(PrintNo) - ArchConvert.Obj2Int(tmpPrintNo)) : ((!(PrintNo.Substring(0, PrintNo.Length - 4) != tmpPrintNo.Substring(0, tmpPrintNo.Length - 4))) ? (ArchConvert.Obj2Int(PrintNo.Substring(PrintNo.Length - 4, 4)) - ArchConvert.Obj2Int(tmpPrintNo.Substring(tmpPrintNo.Length - 4, 4))) : (-1)));
				if (diff2 != 1)
				{
					string text = ErrMsg;
					ErrMsg = text + "PrintNo回縮未連續,於:" + PrintNo + ",前一個PrintNo為:" + PrevPrintNo + "\r";
				}
			}
			PrevPrintNo = PrintNo;
		}
		dvItemAforProntNo.Dispose();
		dvItemAforProntNo = null;
		if (ErrMsg != string.Empty)
		{
			ER.Message = "重整畫面時發生錯誤，請先刪除前一個動作產生的多餘項目(預算書最上方)，並執行項次重整後再繼續您的操作。\n若重整項次後重新操作此錯誤訊息仍持續出現請洽客服聯絡工程師處理問題資料(此問題將於下一個版次中處理)。";
			ER.ReturnCode = -99;
		}
		return ER;
	}

	private void RemoveInsertFailedRow()
	{
		theItemA.DeleteItemAPrintNo0000(projectCode);
		LoadProjectData();
	}

	private void SetStatusBarItemCount(int itemCount)
	{
		statusBar.Panels[0].Text = "資料筆數：" + itemCount;
	}

	private void SetGridColumn()
	{
		gridBudget.Redraw = false;
		for (int i = 0; i < GridCols; i++)
		{
			gridBudget.Cols[i].Name = (string)GridColsSquence[i, 0];
			gridBudget.Cols[i].Caption = (string)GridColsSquence[i, 1];
			gridBudget.Cols[i].Width = (int)GridColsSquence[i, 2];
			gridBudget.Cols[i].DataType = (Type)GridColsSquence[i, 3];
			gridBudget.Cols[i].Visible = (bool)GridColsSquence[i, 4];
			gridBudget.Cols[i].Format = (string)GridColsSquence[i, 5];
			gridBudget.Cols[i].AllowEditing = (bool)GridColsSquence[i, 6];
			gridBudget.Cols[i].TextAlign = (TextAlignEnum)GridColsSquence[i, 7];
			gridBudget.Cols[i].AllowDragging = (bool)GridColsSquence[i, 8];
			gridBudget.Cols[i].AllowResizing = (bool)GridColsSquence[i, 9];
		}
		gridBudget.Redraw = true;
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

	private bool Is75094900()
	{
		string sPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "75094900.dat");
		if (File.Exists(sPath))
		{
			return true;
		}
		return false;
	}

	private void Data2Grid()
	{
		if (!gridBudget.Cols.Contains("ParentSno"))
		{
			Column cParentSno = gridBudget.Cols.Add();
			cParentSno.Name = "ParentSno";
			cParentSno.Visible = false;
		}
		if (!gridBudget.Cols.Contains("SortOrder"))
		{
			Column cParentSno = gridBudget.Cols.Add();
			cParentSno.Name = "SortOrder";
			cParentSno.Visible = false;
		}
		bool ChangeManagement = SysConfig.SysChangeManagement;
		int iLevel = 0;
		if (FORM_STATUS == FormStatus.Binding)
		{
			return;
		}
		FORM_STATUS = FormStatus.Binding;
		Application.DoEvents();
		Cursor = Cursors.WaitCursor;
		bool IsThereSummary = false;
		int iRowNow = gridBudget.Row;
		toolbarsManager.BeginUpdate();
		toolbarsManager.Enabled = false;
		RememberColsProps();
		gridBudget.Rows.Count = 1;
		SetGridColumn();
		gridBudget.Redraw = false;
		iQty = (iCst = (iAmt = 0));
		CellStyle CS0 = gridBudget.Styles.Add("Transparent");
		CellStyle CS1 = gridBudget.Styles.Add("AnalysisColor");
		CellStyle CS2 = gridBudget.Styles.Add("MainColor");
		CellStyle CS9 = gridBudget.Styles.Add("IsSharedColor");
		CellStyle CSA = gridBudget.Styles.Add("Adjustment");
		CellStyle CSD = gridBudget.Styles.Add("DocDownloaded");
		CellStyle CSAnaErr = gridBudget.Styles.Add("AnalysisErr");
		CellStyle CSBackGround = gridBudget.Styles.Add("AnalysisChild");
		CellStyle CSBackGroundfixPrice = gridBudget.Styles.Add("AnalysisChildfixPrice");
		CellStyle csBudgetChange = gridBudget.Styles.Add("BudgetChange");
		CellStyle csBudgetChangeAnalysis = gridBudget.Styles.Add("BudgetChangeAnalysis");
		CellStyle csBudgetCheckSpace = gridBudget.Styles.Add("BudgetCheckSpace");
		CellStyle csBudgetCheckZero = gridBudget.Styles.Add("BudgetCheckZero");
		CS0.ForeColor = Color.Transparent;
		CS1.ForeColor = Color.Red;
		CS2.ForeColor = Color.Blue;
		CS9.ForeColor = Color.Green;
		CSA.BackColor = Color.OrangeRed;
		CSD.BackColor = Color.Gold;
		CSAnaErr.BackColor = Color.Violet;
		CSBackGround.BackColor = Color.LightGoldenrodYellow;
		CSBackGroundfixPrice.BackColor = Color.LemonChiffon;
		csBudgetChange.BackColor = Color.LightYellow;
		csBudgetChangeAnalysis.BackColor = Color.LightYellow;
		csBudgetChangeAnalysis.ForeColor = Color.Red;
		csBudgetCheckSpace.BackColor = Color.Pink;
		csBudgetCheckZero.BackColor = Color.DarkOrange;
		int iRows = dtItemA.Rows.Count + 1;
		int K = iRows % 50;
		gridBudget.Rows.Count = iRows + (50 - K);
		if (dtItemA.Rows.Count <= 0)
		{
			gridBudget.Select();
		}
		gridBudget.Visible = true;
		string ItemType = string.Empty;
		int iSummaryRow = -1;
		double ProjectScope = ArchConvert.Obj2Double(dtProject.Rows[0]["projectScope"]);
		double TotalAmount = GetItemAAmount();
		lblTotal.Text = string.Format("{0:N" + MainItemAmountPrecision + "}", TotalAmount);
		bool itemChanged = false;
		int girdRowIndex = 1;
		checkData2GridSpace = true;
		checkData2GridZero = true;
		for (int i = 0; i < dtItemA.Rows.Count; i++)
		{
			try
			{
				ItemType = ((dtItemA.Rows[i]["kind"].ToString().Length > 0) ? dtItemA.Rows[i]["kind"].ToString().ToUpper().Trim() : "");
				itemChanged = budgetChangeCurrentVersion > 0 && (dtItemA.Rows[i]["AmountBeforeChange"] == DBNull.Value || ArchConvert.Obj2Decimal(dtItemA.Rows[i]["AmountBeforeChange"]) != ArchConvert.Obj2Decimal(dtItemA.Rows[i]["amount"]) || ArchConvert.Obj2Decimal(dtItemA.Rows[i]["QtyBeforeChange"]) != ArchConvert.Obj2Decimal(dtItemA.Rows[i]["Qty"]));
				if (showOnlyChangedItem && !IsMainItem(ItemType) && !itemChanged)
				{
					continue;
				}
				if ((budgetType == BudgetType.Types.CostEstimation || budgetType == BudgetType.Types.CostQuotationMerged) && !gridBudget.Cols.Contains("CostBeforeChange"))
				{
					Column cCostBeforeChange = gridBudget.Cols.Add();
					cCostBeforeChange.Name = "CostBeforeChange";
					cCostBeforeChange.Visible = false;
				}
				Row GridRow = gridBudget.Rows[girdRowIndex];
				if (IsMainItem(ItemType))
				{
					GridRow.Style = gridBudget.Styles["MainColor"];
				}
				if (dtItemA.Rows[i]["analysis"].ToString().Trim() == "1")
				{
					GridRow["Analysis"] = true;
					GridRow.Style = gridBudget.Styles["AnalysisColor"];
					CellRange rg = gridBudget.GetCellRange(girdRowIndex, gridBudget.Cols["AnaImg"].SafeIndex);
					rg.Style = gridBudget.Styles["img"];
					rg.Image = imageList2.Images[0];
				}
				else
				{
					GridRow["Analysis"] = false;
				}
				if (SysConfig.SysComsEnable)
				{
					GridRow["Qty"] = PubTools.ARound(dtItemA.Rows[i]["qty"], MainItemQtyPrecision);
				}
				else
				{
					GridRow["Qty"] = dtItemA.Rows[i]["qty"];
				}
				GridRow["Cost"] = dtItemA.Rows[i]["cost"].ToString().Trim();
				GridRow["ItemNo"] = dtItemA.Rows[i]["ItemNo"].ToString().Trim();
				GridRow["CName"] = dtItemA.Rows[i]["cName"].ToString().Trim();
				GridRow["UnitName"] = dtItemA.Rows[i]["unitName"].ToString().Trim();
				if (ChangeManagement && dtItemA.Rows[i]["kind"].ToString() != "Z" && (ArchConvert.Obj2Decimal(dtItemA.Rows[i]["qty"]) == 0m || ArchConvert.Obj2Decimal(dtItemA.Rows[i]["cost"]) == 0m))
				{
					checkData2GridZero = false;
					GridRow.Style = gridBudget.Styles["BudgetCheckZero"];
				}
				if (ChangeManagement && (ArchConvert.Obj2String(dtItemA.Rows[i]["cName"]) == "" || (dtItemA.Rows[i]["kind"].ToString() == "W" && ArchConvert.Obj2String(dtItemA.Rows[i]["unitName"]) == "") || (ArchConvert.Obj2String(dtItemA.Rows[i]["ItemNo"]) == "" && dtItemA.Rows[i]["kind"].ToString() != "Z")))
				{
					checkData2GridSpace = false;
					GridRow.Style = gridBudget.Styles["BudgetCheckSpace"];
				}
				GridRow["LockCost"] = dtItemA.Rows[i]["LockCost"].ToString().Trim() == "1";
				object Amount = (GridRow["Amount"] = ((IsSubmitBid && PubTools.Str2Double(dtItemA.Rows[i]["amount"]) == 0.0) ? ((object)(PubTools.Str2Double(dtItemA.Rows[i]["cost"]) * PubTools.Str2Double(dtItemA.Rows[i]["qty"]))) : dtItemA.Rows[i]["amount"]));
				GridRow["ItemUnitPrice"] = ((ProjectScope == 0.0) ? 0.0 : Math.Round(PubTools.Str2Double(Amount) / ProjectScope, 1));
				GridRow["ItemUnitWeight"] = ((TotalAmount == 0.0) ? 0.0 : (Math.Round(PubTools.Str2Double(Amount) / TotalAmount, 4) * 100.0));
				GridRow["PccesCode"] = dtItemA.Rows[i]["pccesCode"].ToString().Trim();
				GridRow["Memo"] = dtItemA.Rows[i]["memo"].ToString().Trim();
				GridRow["EName"] = dtItemA.Rows[i]["eName"].ToString().Trim();
				if (Is75094900())
				{
					if (ItemType == "W")
					{
						GridRow["ExtendCode"] = dtItemA.Rows[i]["extendCode"].ToString().Trim();
						GridRow["EUnit"] = dtItemA.Rows[i]["eUnit"].ToString().Trim();
					}
					else
					{
						GridRow["ExtendCode"] = dtItemA.Rows[i]["eUnit"].ToString().Trim();
					}
				}
				else
				{
					GridRow["ExtendCode"] = dtItemA.Rows[i]["extendCode"].ToString().Trim();
					GridRow["EUnit"] = dtItemA.Rows[i]["eUnit"].ToString().Trim();
				}
				GridRow["LevelNo"] = dtItemA.Rows[i]["levelNo"].ToString().Trim();
				GridRow["SNo"] = dtItemA.Rows[i]["sno"];
				GridRow["Kind"] = dtItemA.Rows[i]["kind"].ToString().Trim().ToUpper();
				GridRow["PrintNo"] = dtItemA.Rows[i]["printNo"].ToString().Trim();
				GridRow["Formula"] = dtItemA.Rows[i]["Formula"].ToString().Trim();
				GridRow["PubCode"] = dtItemA.Rows[i]["pubCode"].ToString().Trim();
				GridRow["surName"] = dtItemA.Rows[i]["surName"].ToString().Trim();
				GridRow["PrintToAnalysis"] = dtItemA.Rows[i]["PrintToAnalysis"].ToString().Trim();
				GridRow["IsOldItem"] = "1";
				GridRow["fixPrice"] = dtItemA.Rows[i]["fixPrice"].ToString().Trim() == "1";
				GridRow["Account"] = dtItemA.Rows[i]["Account"].ToString().Trim();
				GridRow["CostUnit"] = dtItemA.Rows[i]["CostUnit"].ToString().Trim();
				GridRow["CostUID"] = dtItemA.Rows[i]["CostUID"].ToString().Trim();
				GridRow["TypeID"] = dtItemA.Rows[i]["TypeID"].ToString().Trim();
				GridRow["Costkind"] = dtItemA.Rows[i]["costKind"].ToString().Trim();
				GridRow["ParentSno"] = dtItemA.Rows[i]["ParentSno"];
				GridRow["SortOrder"] = dtItemA.Rows[i]["SortOrder"].ToString().Trim();
				if (FormActionName == PccesFormAction.BUD)
				{
					GridRow["QtyBeforeChange"] = dtItemA.Rows[i]["QtyBeforeChange"];
					GridRow["AmountBeforeChange"] = dtItemA.Rows[i]["AmountBeforeChange"];
					GridRow["BudgetChangeAddQty"] = ArchConvert.Obj2Decimal(dtItemA.Rows[i]["Qty"]) - ArchConvert.Obj2Decimal(dtItemA.Rows[i]["QtyBeforeChange"]);
					GridRow["Lock"] = ArchConvert.Obj2Bool(dtItemA.Rows[i]["Lock"]);
					if (ItemType == "W" && itemChanged)
					{
						GridRow.Style = (((bool)GridRow["Analysis"]) ? gridBudget.Styles["BudgetChangeAnalysis"] : gridBudget.Styles["BudgetChange"]);
					}
					GridRow["IsGreenItem"] = ArchConvert.Obj2Bool(dtItemA.Rows[i]["IsGreenItem"]);
					GridRow["IsGreenMethod"] = ArchConvert.Obj2Bool(dtItemA.Rows[i]["IsGreenMethod"]);
					GridRow["IsGreenMaterial"] = ArchConvert.Obj2Bool(dtItemA.Rows[i]["IsGreenMaterial"]);
					GridRow["IsGreenEnergy"] = ArchConvert.Obj2Bool(dtItemA.Rows[i]["IsGreenEnergy"]);
					GridRow["ItemType"] = Archnowledge.Pcces.DomainModule.MrsBase.ItemType.GetItemType(dtItemA.Rows[i]["IsCommonItem"].ToString());
					GridRow["BudgetChangeReason"] = ArchConvert.Obj2String(dtItemA.Rows[i]["BudgetChangeReason"]);
					GridRow["VersionHistory"] = ArchConvert.Obj2String(dtItemA.Rows[i]["VersionHistory"]);
					if (budgetType == BudgetType.Types.CostEstimation || budgetType == BudgetType.Types.CostQuotationMerged)
					{
						GridRow["CostBeforeChange"] = dtItemA.Rows[i]["CostBeforeChange"];
					}
				}
				if (ItemType == "Z")
				{
					gridBudget.SetCellStyle(girdRowIndex, gridBudget.Cols["Qty"].SafeIndex, CS0);
					gridBudget.SetCellStyle(girdRowIndex, gridBudget.Cols["Cost"].SafeIndex, CS0);
					gridBudget.SetCellStyle(girdRowIndex, gridBudget.Cols["QtyBeforeChange"].SafeIndex, CS0);
					gridBudget.SetCellStyle(girdRowIndex, gridBudget.Cols["BudgetChangeAddQty"].SafeIndex, CS0);
				}
				if (dtItemA.Rows[i]["PrintToAnalysis"].ToString().Trim() == "1")
				{
					CellRange RgRow2 = gridBudget.GetCellRange(girdRowIndex, 1, girdRowIndex, gridBudget.Cols.Count - 1);
					RgRow2.Style = CSAnaPrn;
				}
				if (!ArchConvert.Obj2Bool(GridRow["LockCost"]) && ItemType == "W" && dtItemA.Rows[i]["ChildrenLockCostNum"].ToString() != "0")
				{
					CellRange RgRowAnaLockCost = gridBudget.GetCellRange(girdRowIndex, gridBudget.Cols["LockCost"].SafeIndex);
					RgRowAnaLockCost.Style = CSBackGround;
				}
				if (!ArchConvert.Obj2Bool(GridRow["fixPrice"]) && ItemType == "W" && dtItemA.Rows[i]["ChildrenFixPriceNum"].ToString() != "0")
				{
					CellRange RgRowAnaLockCost = gridBudget.GetCellRange(girdRowIndex, gridBudget.Cols["fixPrice"].SafeIndex);
					RgRowAnaLockCost.Style = CSBackGroundfixPrice;
				}
				if ((FormActionName == PccesFormAction.BUD || FormActionName == PccesFormAction.BID) && dtItemA.Rows[i]["CostUnit"].ToString().Trim() != string.Empty)
				{
					double dec = 1.0;
					dec *= TryParseToDouble(dtItemA.Rows[i]["Property1"]);
					dec *= TryParseToDouble(dtItemA.Rows[i]["Property2"]);
					dec *= TryParseToDouble(dtItemA.Rows[i]["Property3"]);
					GridRow["UnitCost"] = PubTools.Str2Double(Amount) / dec;
				}
				int CostDec = ArchConvert.Obj2Int(dtItemA.Rows[i]["CostDec"]);
				int AmtDec = ArchConvert.Obj2Int(dtItemA.Rows[i]["AmtDec"]);
				GridRow["CostDec"] = CostDec;
				GridRow["AmtDec"] = AmtDec;
				string CostKind = dtItemA.Rows[i]["costKind"].ToString();
				if (ItemType == "B" || ItemType == "Z" || CostKind == "#" || dtItemA.Rows[i]["pccesCode"].ToString().StartsWith("#"))
				{
					gridBudget.SetCellStyle(girdRowIndex, gridBudget.Cols["CostDec"].SafeIndex, CS0);
					gridBudget.SetCellStyle(girdRowIndex, gridBudget.Cols["AmtDec"].SafeIndex, CS0);
				}
				else
				{
					gridBudget.SetCellStyle(girdRowIndex, gridBudget.Cols["CostDec"].SafeIndex, gridBudget.Styles["ComboList"]);
					gridBudget.SetCellStyle(girdRowIndex, gridBudget.Cols["AmtDec"].SafeIndex, gridBudget.Styles["ComboList"]);
				}
				gridBudget.SetCellStyle(girdRowIndex, gridBudget.Cols["PwrSet"].SafeIndex, gridBudget.Styles["ComboListPS"]);
				if (CostKind == "#" || dtItemA.Rows[i]["pccesCode"].ToString().StartsWith("#"))
				{
					gridBudget.SetCellStyle(girdRowIndex, gridBudget.Cols["Qty"].SafeIndex, CS0);
					gridBudget.SetCellStyle(girdRowIndex, gridBudget.Cols["Cost"].SafeIndex, CS0);
					gridBudget.SetCellStyle(girdRowIndex, gridBudget.Cols["Amount"].SafeIndex, CS0);
					gridBudget.SetCellStyle(girdRowIndex, gridBudget.Cols["QtyBeforeChange"].SafeIndex, CS0);
					gridBudget.SetCellStyle(girdRowIndex, gridBudget.Cols["BudgetChangeAddQty"].SafeIndex, CS0);
					gridBudget.SetCellStyle(girdRowIndex, gridBudget.Cols["AmountBeforeChange"].SafeIndex, CS0);
				}
				if (MainItemCostPrecison != CostDec && dtItemA.Rows[i]["CostDec"] != DBNull.Value)
				{
					CellStyle CostDecStyle = gridBudget.Styles.Add("CostDecStyle" + CostDec);
					if (CostDec > 0)
					{
						CostDecStyle.Format = "###,###,###,##0." + "0".PadLeft(CostDec, '0');
					}
					else
					{
						CostDecStyle.Format = "###,###,###,##0";
					}
					gridBudget.SetCellStyle(i + 1, gridBudget.Cols["Cost"].SafeIndex, CostDecStyle);
				}
				if (MainItemAmountPrecision != AmtDec && dtItemA.Rows[i]["AmtDec"] != DBNull.Value)
				{
					CellStyle AmyDecStyle = gridBudget.Styles.Add("AmtDec" + AmtDec);
					if (AmtDec > 0)
					{
						AmyDecStyle.Format = "###,###,###,##0." + "0".PadLeft(AmtDec, '0');
					}
					else
					{
						AmyDecStyle.Format = "###,###,###,##0";
					}
					gridBudget.SetCellStyle(girdRowIndex, gridBudget.Cols["Amount"].SafeIndex, AmyDecStyle);
					gridBudget.SetCellStyle(girdRowIndex, gridBudget.Cols["AmountBeforeChange"].SafeIndex, AmyDecStyle);
				}
				if (dtItemA.Rows[i]["PwrSet"] != DBNull.Value)
				{
					GridRow["PwrSet"] = PwrSet.GetName(dsPwrSet, PubTools.Str2Int(dtItemA.Rows[i]["PwrSet"]));
				}
				else
				{
					GridRow["PwrSet"] = PwrSet.GetDefaultName(dsPwrSet);
				}
				if (GridRow["Kind"] != null)
				{
					GridRow.IsNode = true;
				}
				if (i == dtItemA.Rows.Count - 1 && ItemType == "Z" && dtItemA.Rows[i]["printNo"].ToString().Trim().Length == 4)
				{
					string sSUM_PrintNO = dtItemA.Rows[i]["printNo"].ToString().Trim();
					string sSrc_KIND = CommonMethods.GetActionNameString(FormActionName);
					DBClass DBCLS = new DBClass();
					DBCLS._FS_UserID = userID;
					DBCLS.ExecuteCommand("Update " + sSrc_KIND + "ItemB Set ParentCode ='" + "".PadLeft(32, '9') + "' Where ParentCode = '" + sSUM_PrintNO + "' And ProjectCode='" + projectCode + "' ");
					DBCLS.ExecuteCommand("Update " + sSrc_KIND + "ItemA Set PrintNo ='" + "".PadLeft(32, '9') + "' Where PrintNo = '" + sSUM_PrintNO + "' And ProjectCode='" + projectCode + "' ");
					GridRow["PrintNo"] = "".PadLeft(32, '9');
					DBCLS = null;
				}
				string PrintNo = dtItemA.Rows[i]["PrintNo"].ToString().Trim();
				if (PrintNo == "".PadLeft(32, '9'))
				{
					GridRow.Node.Level = 1;
					IsThereSummary = true;
					iSummaryRow = girdRowIndex;
				}
				else if (PrintNo.Length == 4 && dtItemA.Rows[i]["Kind"].ToString().Trim() == "Z" && i == dtItemA.Rows.Count - 1)
				{
					GridRow.Node.Level = 1;
					IsThereSummary = true;
					iSummaryRow = girdRowIndex;
				}
				else
				{
					GridRow.Node.Level = Convert.ToInt32(PrintNo.Length / 4);
					if (GridRow.Node.Level != ArchConvert.Obj2Int(dtItemA.Rows[i]["LevelNo"]))
					{
						MessageBox.Show(ArchConvert.Obj2String(dtItemA.Rows[i]["ItemNo"]) + " 在資料庫中的 階層資訊不一致 (LevelNo 及 PrintNo不一致)，請確定顯示是否正確。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
				}
				if (GridRow.Node != null && GridRow.Node.Level > iLevel)
				{
					iLevel = GridRow.Node.Level;
				}
				GridRow["IsShared"] = dtItemA.Rows[i]["share"];
				if (dtItemA.Rows[i]["share"] != null && dtItemA.Rows[i]["share"].ToString().Trim() == "1")
				{
					GridRow.Style = gridBudget.Styles["IsSharedColor"];
				}
				if (dtItemA.Rows[i]["CName"] != null && dtItemA.Rows[i]["CName"].ToString().Trim() == "調價後差額")
				{
					GridRow.Style = gridBudget.Styles["Adjustment"];
				}
				if (dtItemA.Rows[i]["AddOnDownLoadNum"].ToString() != "0")
				{
					gridBudget.SetCellStyle(i + 1, gridBudget.Cols["pccesCode"].SafeIndex, CSD);
					GridRow["IsDown"] = true;
				}
				else
				{
					GridRow["IsDown"] = false;
				}
				if (HideAmountIsZeroItems)
				{
					if (PubTools.Str2Double(Amount) == 0.0)
					{
						GridRow.Visible = false;
					}
				}
				else
				{
					GridRow.Visible = true;
				}
				girdRowIndex++;
				goto IL_219d;
			}
			catch (Exception ex)
			{
				MessageBox.Show("FormBudget::Date2Grid()#1 Data2Grid Error : " + ex.Message);
				goto IL_219d;
			}
			IL_219d:
			if (i % 150 == 0)
			{
				Application.DoEvents();
				Cursor = Cursors.WaitCursor;
			}
		}
		if (dtItemA.Rows.Count > 0 && gridBudget[1, "PrintNo"] != null && gridBudget[1, "PrintNo"].ToString() != "99999999999999999999999999999999")
		{
			int iFstLvl = PubTools.Str2Int(gridBudget[1, "PrintNo"].ToString().Trim().Length / 4);
			if (iFstLvl != 1)
			{
				theProject.ReArrangePrintNo(projectCode, !IsEditItemNo);
				FORM_STATUS = FormStatus.Normal;
				LoadProjectData();
				return;
			}
		}
		if (Is22132814())
		{
			gridBudget.Cols["CostDec"].AllowEditing = false;
			gridBudget.Cols["AmtDec"].AllowEditing = false;
		}
		SetStatusBarItemCount(girdRowIndex - 1);
		SwitchToCorrectLevelStatus(iLevel);
		gridBudget.Redraw = true;
		if (!IsThereSummary && dtItemA.Rows.Count >= 1)
		{
			AddTotalAmount(IsThereSummary);
		}
		try
		{
			if (F_IsGoBackOriginalRow)
			{
				if (F_SNo != -1)
				{
					SetGridFocusBySno(F_SNo, NeedAtTop: false);
				}
				else
				{
					gridBudget.AfterSelChange -= gridBudget1_AfterSelChange;
					int iRow = iRowNow;
					if (iRow >= 1 && iRow < gridBudget.Rows.Count)
					{
						gridBudget.Row = iRow;
					}
					else
					{
						gridBudget.Row = 1;
					}
					gridBudget.Select();
					gridBudget.AfterSelChange += gridBudget1_AfterSelChange;
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(this, "Err8:\nFormBudget::Date2Grid()#2 " + ex.Message, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		gridBudget.Invalidate();
		FORM_STATUS = FormStatus.Normal;
		if (IsCollaspse.Length == gridBudget.Rows.Count)
		{
			for (int i = 1; i < gridBudget.Rows.Count; i++)
			{
				if (gridBudget.Rows[i].Node != null)
				{
					gridBudget.Rows[i].Node.Collapsed = IsCollaspse[i];
				}
			}
		}
		if (gridBudget.Row == 1 && dtItemA.Rows.Count == 0)
		{
			toolbarsManager.Tools["Cut"].SharedProps.Enabled = false;
			toolbarsManager.Tools["Copy"].SharedProps.Enabled = false;
			toolbarsManager.Tools["Paste"].SharedProps.Enabled = false;
			toolbarsManager.Tools["InsertMainItem"].SharedProps.Enabled = true;
			toolbarsManager.Tools["EditMainItem"].SharedProps.Enabled = false;
			toolbarsManager.Tools["InsertWorkItem"].SharedProps.Enabled = false;
			toolbarsManager.Tools["EditWorkItem"].SharedProps.Enabled = false;
			toolbarsManager.Tools["Delete"].SharedProps.Enabled = false;
			toolbarsManager.Tools["EditCostBreakdown"].SharedProps.Enabled = false;
			toolbarsManager.Tools["MakeAmortizedItem"].SharedProps.Enabled = false;
			toolbarsManager.Tools["CancelAmortizedItem"].SharedProps.Enabled = false;
			toolbarsManager.Tools["CloneWorkItem"].SharedProps.Enabled = false;
			toolbarsManager.Tools["ImportQtyFrom3rdPartyTool"].SharedProps.Enabled = false;
		}
		else if (gridBudget.Row == 1 && dtItemA.Rows.Count > 0)
		{
			toolbarsManager.Tools["Cut"].SharedProps.Enabled = true;
			toolbarsManager.Tools["Copy"].SharedProps.Enabled = true;
			toolbarsManager.Tools["Paste"].SharedProps.Enabled = false;
			toolbarsManager.Tools["InsertMainItem"].SharedProps.Enabled = true;
			toolbarsManager.Tools["EditMainItem"].SharedProps.Enabled = ((gridBudget[gridBudget.Row, "Kind"].ToString().Trim().ToUpper() != "W") ? true : false);
			toolbarsManager.Tools["InsertWorkItem"].SharedProps.Enabled = gridBudget[gridBudget.Row, "Kind"].ToString().Trim().ToUpper() == "B";
			toolbarsManager.Tools["EditWorkItem"].SharedProps.Enabled = gridBudget[gridBudget.Row, "Kind"].ToString().Trim().ToUpper() == "W";
			toolbarsManager.Tools["CloneWorkItem"].SharedProps.Enabled = gridBudget[gridBudget.Row, "Kind"].ToString().Trim().ToUpper() == "W";
			toolbarsManager.Tools["MakeAmortizedItem"].SharedProps.Enabled = false;
			toolbarsManager.Tools["CancelAmortizedItem"].SharedProps.Enabled = false;
			toolbarsManager.Tools["Delete"].SharedProps.Enabled = false;
			toolbarsManager.Tools["EditCostBreakdown"].SharedProps.Enabled = false;
			toolbarsManager.Tools["ImportQtyFrom3rdPartyTool"].SharedProps.Enabled = gridBudget[gridBudget.Row, "Kind"].ToString().Trim().ToUpper() == "B";
		}
		if (IsSubmitBid && IsThereSummary && iSummaryRow > -1 && PubTools.Str2Double(lblTotal.Text) == 0.0)
		{
			gridBudget[iSummaryRow, "Amount"] = gridBudget[iSummaryRow, "Cost"];
			lblTotal.Text = string.Format("{0:N" + MainItemAmountPrecision + "}", gridBudget[iSummaryRow, "Cost"]);
		}
		toolbarsManager.Enabled = true;
		if (FormActionName == PccesFormAction.BUD && IsTemplate)
		{
			SetTemplateControlAvailability();
		}
		SetColsEditSymbol();
		toolbarsManager.EndUpdate();
		Cursor = Cursors.Default;
		GC.Collect();
		if (iXMLDecimalTimes < 1 && FormActionName == PccesFormAction.BUD && (MainItemCostPrecison > 2 || MainItemAmountPrecision > 2 || AnalysisCostPrecision > 2 || AnalysisAmountPrecision > 2))
		{
			iXMLDecimalTimes++;
			FormBudgetExp_Wzd_Help1 FM_HELP1 = new FormBudgetExp_Wzd_Help1();
			FM_HELP1.ShowDialog();
			FM_HELP1.Close();
			FM_HELP1.Dispose();
			FM_HELP1 = null;
		}
		toolbarsManager.Tools["EditItemNoSetting"].SharedProps.Visible = true;
		toolbarsManager.Tools["EditItemNoSetting"].SharedProps.Enabled = true;
	}

	private bool IsMainItem(string ItemType)
	{
		int result;
		switch (ItemType)
		{
		default:
			result = ((ItemType == "U") ? 1 : 0);
			break;
		case "B":
		case "L":
		case "F":
		case "S":
		case "Z":
			result = 1;
			break;
		}
		return (byte)result != 0;
	}

	private void CheckTotalAmount()
	{
		bool IsThereSummary = false;
		for (int i = gridBudget.Rows.Count - 1; i > 1; i--)
		{
			if (gridBudget[i, "PrintNo"] != null)
			{
				string PrintNo = gridBudget[i, "PrintNo"].ToString();
				if (PrintNo == "".PadLeft(32, '9'))
				{
					IsThereSummary = true;
					break;
				}
			}
		}
		AddTotalAmount(IsThereSummary);
	}

	private void AddTotalAmount(bool IsThereSummary)
	{
		if (!IsThereSummary && dtItemA.Rows.Count >= 1)
		{
			gridBudget.Rows[dtItemA.Rows.Count + 1].IsNode = true;
			gridBudget.Rows[dtItemA.Rows.Count + 1].Node.Level = 1;
			gridBudget[dtItemA.Rows.Count + 1, "CName"] = "總計";
			gridBudget[dtItemA.Rows.Count + 1, "SNo"] = "999999";
			gridBudget[dtItemA.Rows.Count + 1, "Kind"] = "Z";
			gridBudget[dtItemA.Rows.Count + 1, "LevelNo"] = "1";
			gridBudget[dtItemA.Rows.Count + 1, "PrintNo"] = "".PadLeft(32, '9');
			DataRow newRow = dtItemA.NewRow();
			newRow["PrintNo"] = "".PadLeft(32, '9');
			newRow["cName"] = "總計";
			newRow["Qty"] = 1;
			newRow["ProjectCode"] = projectCode;
			newRow["sNo"] = 999999;
			newRow["Kind"] = "Z";
			int SNo = 0;
			theItemA.AddItemA(projectCode, 999999, "".PadLeft(32, '9'), 0, "", null, "總計", null, null, "Z", null, null, null, null, null, null, null, null, null, null, null, null, null, null, "", null, null, null, null, null, null, null, null, null, null, null, null, null, false, null, null, null, null, null, null, null, out SNo);
		}
	}

	private double TryParseToDouble(object obj)
	{
		try
		{
			Convert.ToDouble(obj);
		}
		catch
		{
			return 1.0;
		}
		return Convert.ToDouble(obj);
	}

	private void UpdateInsertedRows(int StartRow, int RowNumber, string[] sNo)
	{
		lock (this)
		{
			Cursor = Cursors.WaitCursor;
			CellStyle CS0 = gridBudget.Styles.Add("Transparent");
			CellStyle CS1 = gridBudget.Styles.Add("AnalysisColor");
			CellStyle CS2 = gridBudget.Styles.Add("MainColor");
			CellStyle CSD = gridBudget.Styles.Add("DocDownloaded");
			CS0.ForeColor = Color.Transparent;
			CS1.ForeColor = Color.Red;
			CS2.ForeColor = Color.Blue;
			CSD.BackColor = Color.Gold;
			double ProjectScope = 0.0;
			ArrayList bArr = new ArrayList();
			bArr.Add(userID);
			bArr.Add("取得工程規模--" + projectCode);
			Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(bArr);
			PROJ.ps_projectCode = projectCode;
			PROJ.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
			DataTable DT_Prj = PROJ.ListItem("", projectCode);
			if (DT_Prj.Rows.Count > 0)
			{
				ProjectScope = PubTools.Str2Double(DT_Prj.Rows[0]["projectScope"]);
			}
			ArrayList aArr = new ArrayList();
			aArr.Add(userID);
			aArr.Add("讀取預算書總價--" + projectCode);
			double TotalAmount = GetItemAAmount();
			string sKind = "";
			dbItemA = new Archnowledge.Pcces.BUDClass.ItemA(aArr);
			dbItemA.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
			dbItemA.ps_projectCode = projectCode;
			DataTable dtTempItemA = new DataTable();
			dtTempItemA = dbItemA.ListItem("a.sNo in (" + string.Join(", ", sNo) + ")", projectCode);
			dtTempItemA.PrimaryKey = new DataColumn[1] { dtTempItemA.Columns["sNo"] };
			gridBudget.Redraw = false;
			for (int i = StartRow; i < StartRow + RowNumber; i++)
			{
				Row GridRow = gridBudget.Rows[i];
				DataRow row = dtTempItemA.Rows.Find(GridRow["sNo"]);
				if (row != null)
				{
					try
					{
						sKind = ((GridRow["kind"].ToString().Length > 0) ? GridRow["kind"].ToString().ToUpper().Trim() : "");
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
							GridRow.Style = CS2;
							break;
						}
						if (row["analysis"] != DBNull.Value && row["analysis"].ToString() == "1")
						{
							GridRow["analysis"] = true;
							GridRow.Style = CS1;
							CellRange rg = gridBudget.GetCellRange(i, gridBudget.Cols["AnaImg"].SafeIndex);
							rg.Style = gridBudget.Styles["img"];
							rg.Image = imageList2.Images[0];
						}
						else
						{
							GridRow["analysis"] = false;
						}
						if (sKind == "W" && budgetChangeCurrentVersion > 0)
						{
							GridRow.Style = (((bool)GridRow["Analysis"]) ? gridBudget.Styles["BudgetChangeAnalysis"] : gridBudget.Styles["BudgetChange"]);
						}
						if (GridRow["PubCode"] == null)
						{
							GridRow["PubCode"] = row["PubCode"];
						}
						object Amount = GridRow["amount"];
						GridRow["ItemUnitPrice"] = ((ProjectScope == 0.0) ? 0.0 : Math.Round(PubTools.Str2Double(Amount) / ProjectScope, 1));
						GridRow["ItemUnitWeight"] = ((TotalAmount == 0.0) ? 0.0 : (Math.Round(PubTools.Str2Double(Amount) / TotalAmount, 4) * 100.0));
						if ((FormActionName == PccesFormAction.BUD || FormActionName == PccesFormAction.BID) && GridRow["CostUnit"] != null && GridRow["CostUnit"].ToString().Trim() != string.Empty)
						{
							double dec = 1.0;
							dec *= TryParseToDouble(row["Property1"]);
							dec *= TryParseToDouble(row["Property2"]);
							dec *= TryParseToDouble(row["Property3"]);
							GridRow["UnitCost"] = PubTools.Str2Double(Amount) / dec;
						}
						if (FormActionName == PccesFormAction.BUD)
						{
							GridRow["ItemType"] = ItemType.GetItemType(row["IsCommonItem"].ToString());
						}
						string CostKind = "";
						if (row["costKind"] != DBNull.Value)
						{
							CostKind = row["costKind"].ToString();
						}
						if (sKind == "B" || sKind == "Z" || CostKind == "#" || GridRow["PccesCode"].ToString().StartsWith("#"))
						{
							gridBudget.SetCellStyle(i, gridBudget.Cols["CostDec"].SafeIndex, CS0);
							gridBudget.SetCellStyle(i, gridBudget.Cols["AmtDec"].SafeIndex, CS0);
						}
						else
						{
							gridBudget.SetCellStyle(i, gridBudget.Cols["CostDec"].SafeIndex, gridBudget.Styles["ComboList"]);
							gridBudget.SetCellStyle(i, gridBudget.Cols["AmtDec"].SafeIndex, gridBudget.Styles["ComboList"]);
							gridBudget.SetCellStyle(i, gridBudget.Cols["PwrSet"].SafeIndex, gridBudget.Styles["ComboListPS"]);
						}
						if (row["costKind"] != DBNull.Value && row["costKind"].ToString() == "")
						{
							GridRow["QtyDec"] = ((!(row["kind"].ToString().ToUpper() == "W")) ? ((row["QtyDec"] == DBNull.Value) ? MainItemQtyPrecision : PubTools.Str2Int(row["QtyDec"])) : ((row["bQtyDec"] == DBNull.Value) ? MainItemQtyPrecision : PubTools.Str2Int(row["bQtyDec"])));
							GridRow["CostDec"] = ((!(row["kind"].ToString().ToUpper() == "W")) ? ((row["CostDec"] == DBNull.Value) ? MainItemCostPrecison : PubTools.Str2Int(row["CostDec"])) : ((row["bCostDec"] == DBNull.Value) ? MainItemCostPrecison : PubTools.Str2Int(row["bCostDec"])));
							GridRow["AmtDec"] = ((!(row["kind"].ToString().ToUpper() == "W")) ? ((row["AmtDec"] == DBNull.Value) ? MainItemAmountPrecision : PubTools.Str2Int(row["AmtDec"])) : ((row["bAmtDec"] == DBNull.Value) ? MainItemAmountPrecision : PubTools.Str2Int(row["bAmtDec"])));
						}
						else
						{
							GridRow["QtyDec"] = ((!(row["kind"].ToString().ToUpper() == "W")) ? ((row["bQtyDec"] == DBNull.Value) ? MainItemQtyPrecision : PubTools.Str2Int(row["bQtyDec"])) : ((row["QtyDec"] == DBNull.Value) ? MainItemQtyPrecision : PubTools.Str2Int(row["QtyDec"])));
							GridRow["CostDec"] = ((!(row["kind"].ToString().ToUpper() == "W")) ? ((row["bCostDec"] == DBNull.Value) ? MainItemCostPrecison : PubTools.Str2Int(row["bCostDec"])) : ((row["CostDec"] == DBNull.Value) ? MainItemCostPrecison : PubTools.Str2Int(row["CostDec"])));
							GridRow["AmtDec"] = ((!(row["kind"].ToString().ToUpper() == "W")) ? ((row["bAmtDec"] == DBNull.Value) ? MainItemAmountPrecision : PubTools.Str2Int(row["bAmtDec"])) : ((row["AmtDec"] == DBNull.Value) ? MainItemAmountPrecision : PubTools.Str2Int(row["AmtDec"])));
							if ((row["kind"].ToString().ToUpper() != "W" && MainItemQtyPrecision != PubTools.Str2Int(row["QtyDec"])) || (row["kind"].ToString().ToUpper() == "W" && MainItemQtyPrecision != PubTools.Str2Int(row["bQtyDec"])))
							{
								iQty++;
								int iiiQtyDec = ((!(row["kind"].ToString().ToUpper() == "W")) ? ((row["QtyDec"] == DBNull.Value) ? MainItemQtyPrecision : PubTools.Str2Int(row["QtyDec"])) : ((row["bQtyDec"] == DBNull.Value) ? MainItemQtyPrecision : PubTools.Str2Int(row["bQtyDec"])));
								CellStyle styQtyDec = gridBudget.Styles.Add("QtyDec" + iQty);
								if (iiiQtyDec > 0)
								{
									styQtyDec.Format = "###,###,###,##0." + "0".PadLeft(iiiQtyDec, '0');
								}
								else
								{
									styQtyDec.Format = "###,###,###,##0";
								}
								gridBudget.SetCellStyle(i, gridBudget.Cols["Qty"].SafeIndex, styQtyDec);
							}
							if (MainItemCostPrecison != PubTools.Str2Int(row["CostDec"]))
							{
								iCst++;
								int iiiCstDec = ((!(row["kind"].ToString().ToUpper() == "W")) ? ((row["CostDec"] == DBNull.Value) ? MainItemCostPrecison : PubTools.Str2Int(row["CostDec"])) : ((row["bCostDec"] == DBNull.Value) ? MainItemCostPrecison : PubTools.Str2Int(row["bCostDec"])));
								CellStyle styCstDec = gridBudget.Styles.Add("CstDec" + iCst);
								if (iiiCstDec > 0)
								{
									styCstDec.Format = "###,###,###,##0." + "0".PadLeft(iiiCstDec, '0');
								}
								else
								{
									styCstDec.Format = "###,###,###,##0";
								}
								gridBudget.SetCellStyle(i, gridBudget.Cols["Cost"].SafeIndex, styCstDec);
							}
							else if (row["CostDec"] == DBNull.Value)
							{
								iCst++;
								int iiiCstDec = ((!(row["kind"].ToString().ToUpper() == "W")) ? ((row["bCostDec"] == DBNull.Value) ? MainItemCostPrecison : PubTools.Str2Int(row["bCostDec"])) : ((row["bCostDec"] == DBNull.Value) ? MainItemCostPrecison : PubTools.Str2Int(row["bCostDec"])));
								CellStyle styCstDec = gridBudget.Styles.Add("CstDec" + iCst);
								if (iiiCstDec > 0)
								{
									styCstDec.Format = "###,###,###,##0." + "0".PadLeft(iiiCstDec, '0');
								}
								else
								{
									styCstDec.Format = "###,###,###,##0";
								}
								gridBudget.SetCellStyle(i, gridBudget.Cols["Cost"].SafeIndex, styCstDec);
							}
							if (MainItemAmountPrecision != PubTools.Str2Int(row["AmtDec"]))
							{
								iAmt++;
								int iiiAmtDec = ((!(row["kind"].ToString().ToUpper() == "W")) ? ((row["AmtDec"] == DBNull.Value) ? MainItemAmountPrecision : PubTools.Str2Int(row["AmtDec"])) : ((row["bAmtDec"] == DBNull.Value) ? MainItemAmountPrecision : PubTools.Str2Int(row["bAmtDec"])));
								CellStyle styAmtDec = gridBudget.Styles.Add("AmtDec" + iAmt);
								if (iiiAmtDec > 0)
								{
									styAmtDec.Format = "###,###,###,##0." + "0".PadLeft(iiiAmtDec, '0');
								}
								else
								{
									styAmtDec.Format = "###,###,###,##0";
								}
								gridBudget.SetCellStyle(i, gridBudget.Cols["Amount"].SafeIndex, styAmtDec);
							}
							else if (row["AmtDec"] == DBNull.Value)
							{
								iAmt++;
								int iiiAmtDec = ((!(row["kind"].ToString().ToUpper() == "W")) ? ((row["bAmtDec"] == DBNull.Value) ? MainItemAmountPrecision : PubTools.Str2Int(row["bAmtDec"])) : ((row["bAmtDec"] == DBNull.Value) ? MainItemAmountPrecision : PubTools.Str2Int(row["bAmtDec"])));
								CellStyle styAmtDec = gridBudget.Styles.Add("AmtDec" + iAmt);
								if (iiiAmtDec > 0)
								{
									styAmtDec.Format = "###,###,###,##0." + "0".PadLeft(iiiAmtDec, '0');
								}
								else
								{
									styAmtDec.Format = "###,###,###,##0";
								}
								gridBudget.SetCellStyle(i, gridBudget.Cols["Amount"].SafeIndex, styAmtDec);
							}
						}
						if (sKind == "W" && GridRow["pccesCode"].ToString().Trim().Length > 0 && GridRow["pccesCode"].ToString().Substring(0, 1) == "#")
						{
							CellRange rgQTY = gridBudget.GetCellRange(i, gridBudget.Cols["Qty"].SafeIndex);
							CellRange rgCST = gridBudget.GetCellRange(i, gridBudget.Cols["Cost"].SafeIndex);
							CellRange rgAMT = gridBudget.GetCellRange(i, gridBudget.Cols["Amount"].SafeIndex);
							CellStyle cellStyle = (rgAMT.Style = CS0);
							cellStyle = (rgCST.Style = cellStyle);
							rgQTY.Style = cellStyle;
						}
						if (GridRow["Kind"] != null)
						{
							GridRow.IsNode = true;
						}
						GridRow["IsOldItem"] = 1;
						ArrayList Arr = new ArrayList();
						Arr.Add(userID);
						Arr.Add("判別是否已經下載過綱要規範" + projectCode);
						ModifyDB MDB = new ModifyDB(projectCode, Arr);
						if (GridRow["pccesCode"] != null)
						{
							string PccesCode = GridRow["pccesCode"].ToString();
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
								string sSQL = "SELECT COUNT(*) FROM AddOnDownLoad WHERE projectCode = '" + projectCode + "' AND ChapterNo LIKE '" + ChapterNo + "%'";
								if (MDB.DBCount(sSQL) > 0)
								{
									gridBudget.SetCellStyle(i, gridBudget.Cols["pccesCode"].SafeIndex, CSD);
								}
							}
						}
						GridRow["fixPrice"] = row["fixPrice"].ToString().Trim() == "1";
						GridRow["BudgetChangeAddQty"] = row["Qty"];
						GridRow["IsGreenItem"] = false;
						GridRow["IsGreenMethod"] = false;
						GridRow["IsGreenMaterial"] = false;
						GridRow["IsGreenEnergy"] = false;
						GridRow["ItemType"] = ItemType.GetItemType(row["IsCommonItem"].ToString());
					}
					catch (Exception ex)
					{
						MessageBox.Show("FormBudget::UpdateInsertedRows()#1 UpdateInsertedRows Error : " + ex.Message);
						LoadProjectData();
					}
				}
				if (i % 150 == 0)
				{
					Application.DoEvents();
					Cursor = Cursors.WaitCursor;
				}
			}
			gridBudget.Redraw = true;
			gridBudget.Invalidate();
			SetStatusBarItemCount(dtItemA.Rows.Count + dtTempItemA.Rows.Count);
			int MaxLevel = 0;
			for (int i = 1; i <= 8; i++)
			{
				if (((StateButtonTool)toolbarsManager.Tools["Level" + i]).SharedProps.Enabled)
				{
					MaxLevel = i;
				}
			}
			int InsertWorkItemLevel = gridBudget.Rows[StartRow]["PrintNo"].ToString().Length / 4;
			if (InsertWorkItemLevel > MaxLevel)
			{
				SwitchToCorrectLevelStatus(InsertWorkItemLevel);
			}
			FORM_STATUS = FormStatus.Normal;
			Cursor = Cursors.Default;
			GC.Collect();
		}
	}

	private DataTable GetBxdProjMrsBHasProblem()
	{
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = userID;
		string sTableNameA = ((FormActionName == PccesFormAction.BUD) ? "budProjMrsA" : "bidProjMrsA");
		string sTableNameB = ((FormActionName == PccesFormAction.BUD) ? "budProjMrsB" : "bidProjMrsB");
		string sSQLCmd = "Select B.ParentCode, B.pubCode, A.pccesCode as PccesCodeB, C.pccesCode as PccesCodeA From " + sTableNameB + " B left join " + sTableNameA + " A on A.pubCode = B.pubCode and A.ProjectCode=B.ProjectCode  Left Join " + sTableNameA + " C on C.pubCode = B.parentCode and B.ProjectCode=C.ProjectCode  Where B.ProjectCode = '" + projectCode + "' ";
		DataTable DT_ProblemCode = DBCLS.GetUserDefine(sSQLCmd);
		DT_ProblemCode.CaseSensitive = true;
		DataView DV = DT_ProblemCode.DefaultView;
		DV.RowFilter = "PccesCodeB is Null";
		DataTable DT_Return = new DataTable();
		DT_Return.Columns.Add("PccesCode", Type.GetType("System.String"));
		for (int i = 0; i < DV.Count; i++)
		{
			DataRow DR = DT_Return.NewRow();
			DR["PccesCode"] = DV[i]["PccesCodeA"];
			DT_Return.Rows.Add(DR);
		}
		DBCLS = null;
		DT_ProblemCode = null;
		DV = null;
		return DT_Return;
	}

	public void ItemPasteFromProjectItemPick(DataTable DT_Pick)
	{
		toolbarsManager.Tools["InsertWorkItem"].SharedProps.Enabled = false;
		toolbarsManager.Tools["InsertMainItem"].SharedProps.Enabled = false;
		int RowIndex = gridBudget.Row;
		int Add2ItemACount = 0;
		int SuccessAddedCount = 0;
		int RootParentSno = 0;
		int CurrentParentSno = 0;
		int RootSortOrder = 0;
		int CurrentSortOrder = 0;
		if (ArchConvert.Obj2String(gridBudget[RowIndex, "kind"]) == "B")
		{
			RootParentSno = ArchConvert.Obj2Int(gridBudget[RowIndex, "sNo"]);
			Node Nd = gridBudget.Rows[RowIndex].Node.GetNode(NodeTypeEnum.LastChild);
			RootSortOrder = ((Nd == null) ? 1 : (ArchConvert.Obj2Int(gridBudget[Nd.Row.Index, "SortOrder"]) + 1));
		}
		else
		{
			RootParentSno = ArchConvert.Obj2Int(gridBudget[RowIndex, "ParentSno"]);
			RootSortOrder = ArchConvert.Obj2Int(gridBudget[RowIndex, "SortOrder"]);
		}
		CurrentParentSno = RootParentSno;
		CurrentSortOrder = RootSortOrder;
		Add2ItemACount = DT_Pick.Rows.Count;
		Row GridRow = gridBudget.Rows[RowIndex];
		string TypeID = "";
		string CostUID = "";
		if (ArchConvert.Obj2String(GridRow["Kind"]) != "B")
		{
			Node ParentNode = GridRow.Node.GetNode(NodeTypeEnum.Parent);
			if (ParentNode != null)
			{
				GridRow = ParentNode.Row;
			}
		}
		if (GridRow != null)
		{
			TypeID = ((GridRow["TypeID"] != null) ? GridRow["TypeID"].ToString() : "");
			CostUID = ((GridRow["CostUID"] != null) ? GridRow["CostUID"].ToString() : "");
		}
		if (CostUID != "")
		{
			DataView dvPick = new DataView(DT_Pick);
			dvPick.RowFilter = "Kind <> 'W'";
			int Count = dvPick.Count;
			dvPick.Dispose();
			dvPick = null;
			if (Count > 0)
			{
				MessageBox.Show("在成本架構下，只允許插入工項。", "錯誤");
				return;
			}
		}
		int iLastItemLevel = ((gridBudget.Rows[RowIndex].Node == null) ? 1 : (gridBudget.Rows[RowIndex].Node.Level + 1));
		if (gridBudget[RowIndex, "Kind"].ToString().Trim() != "B")
		{
			iLastItemLevel--;
		}
		string sParentPrintToAnalysis = ((gridBudget[RowIndex, "PrintToAnalysis"] != null) ? gridBudget[RowIndex, "PrintToAnalysis"].ToString() : "0");
		string[] sNo = new string[DT_Pick.Rows.Count];
		string[] pubCode = new string[DT_Pick.Rows.Count];
		if (DT_Pick.Rows.Count > 0)
		{
			if (F_NewAddItemFlag == "0" && F_IsNewProject == "")
			{
				if (FormActionName == PccesFormAction.BUD)
				{
					if (GetCurrentBDGT_Type() == "CNT")
					{
						ExecuteCopyToTmpCNT("");
						SetupRestoreSnapshotListCNT();
					}
					else
					{
						ExecuteCopyToTmp("");
						SetupRestoreSnapshotList();
					}
				}
				else
				{
					ExecuteCopyToTmp("");
					SetupRestoreSnapshotList();
				}
				F_NewAddItemFlag = "1";
			}
			ExecResult ER = new ExecResult();
			int SNo = 0;
			Dictionary<string, string> OverWriteSetting = new Dictionary<string, string>();
			for (int j = DT_Pick.Rows.Count - 1; j >= 0; j--)
			{
				DataRow theRow = DT_Pick.Rows[j];
				if (theRow["Kind"].ToString().Trim().ToUpper() == "W")
				{
					pubCode[j] = theRow["PubCode"].ToString().Trim();
					if (DT_Pick.Columns.Contains("OverWriteWorkItem"))
					{
						OverWriteSetting.Add(theRow["pubCode"].ToString(), "1");
					}
				}
				else
				{
					pubCode[j] = "0";
				}
			}
			string ssDBName = DT_Pick.Rows[0]["DBName"].ToString().Trim();
			string ssProjectCode = DT_Pick.Rows[0]["ProjectCode"].ToString().Trim();
			BudProjMrsManager budProjMrsManager = new BudProjMrsManager();
			DataSet CompleteWorkItem = ((!DT_Pick.Columns.Contains("OverWriteWorkItem")) ? budProjMrsManager.GetCompleteWorkItem(ssDBName, ssProjectCode, pubCode) : budProjMrsManager.GetCompleteWorkItem(ssDBName, ssProjectCode, pubCode, OverWriteSetting));
			Archnowledge.Pcces.DatabaseAccess.DatabaseAccess.UseDatabase(currentDBName);
			if (CompleteWorkItem.Tables.Count == 0)
			{
				MessageBox.Show("取得工項詳細資料時失敗,請確認挑選來源資料庫已升至最新版本");
				return;
			}
			budProjMrsManager.SetCompleteWorkItem(projectCode, CompleteWorkItem, userID, TypeID, CostUID, Overwrite: false);
			for (int j = 0; j < DT_Pick.Rows.Count; j++)
			{
				DataRow theRow = DT_Pick.Rows[j];
				int ShiftLevel = PubTools.Str2Int(theRow["Level"]);
				string ItemNo = "";
				string PccesCode = ArchConvert.Obj2String(theRow["PccesCode"]);
				if (theRow["Kind"].ToString() != "Z" && PccesCode != "" && PccesCode[0] != '#')
				{
					ItemNo = "";
				}
				int PwrSetCode = PwrSet.GetCode(dsPwrSet, ArchConvert.Obj2String(theRow["PwrSet"]).Trim());
				ER = theItemA.AddItemAByParent(projectCode, null, "0000", null, ItemNo, iLastItemLevel, theRow["cName"], theRow["EName"], theRow["UnitName"], theRow["Kind"], theRow["cost"], theRow["Qty"], GridRow["Amount"], theRow["Memo"], 0, null, null, null, null, theRow["EUnit"], null, null, null, null, "", null, null, null, theRow["PccesCode"], null, null, null, null, null, null, null, sParentPrintToAnalysis, theRow["surName"], false, null, null, null, null, null, null, PwrSetCode, CurrentParentSno, CurrentSortOrder, out SNo);
				if (ER.ReturnCode == 0)
				{
					SuccessAddedCount++;
					PageBreak thePageBreak = new BudPageBreak();
					thePageBreak.AddPageBreakIfExist(projectCode, SNo, "Y");
					if (ArchConvert.Obj2String(theRow["Kind"]).ToUpper() == "B")
					{
						CurrentParentSno = SNo;
						CurrentSortOrder = 1;
					}
					else
					{
						CurrentSortOrder++;
					}
					sNo[j] = SNo.ToString();
					if (ER.ReturnCode == 0 && CostUID != string.Empty && TypeID != string.Empty && PccesCode != "")
					{
						CostStructureMrsBase costStructure = new CostStructureMrsBase();
						costStructure.AddCostStructureMrsBase(TypeID, CostUID, PccesCode);
					}
					continue;
				}
				break;
			}
			if (ER.ReturnCode == 0)
			{
				if (Add2ItemACount != SuccessAddedCount)
				{
					MessageBox.Show("注意,自專案挑選新增詳細表項目共" + Add2ItemACount + "項,實際成功新增" + SuccessAddedCount + "項");
				}
				ER = theProject.ReArrangePrintNo(projectCode, RootParentSno, !IsEditItemNo);
				if (ER.ReturnCode == 0)
				{
					ReloadGridAtRootSno(RootParentSno);
				}
				else
				{
					MessageBox.Show("重整項次失敗:" + ER.Message + "\n將重整整個專案並全部重新載入...");
					theProject.ReArrangePrintNo(projectCode, 0, !IsEditItemNo);
				}
				Data2Grid();
			}
			CheckIsReCal("Y");
			if (ER.ReturnCode != 0)
			{
				MessageBox.Show(ER.Message);
			}
			CommonMethods.WriteIniValue("BidSet", "StateAdd", "TRUE");
		}
		toolbarsManager.Tools["InsertWorkItem"].SharedProps.Enabled = true;
		toolbarsManager.Tools["InsertMainItem"].SharedProps.Enabled = true;
	}

	public void Th_MenuPaste(DataSet custDS1)
	{
		DS1 = custDS1;
		if (DS1.Tables[0].Rows.Count > 0)
		{
			MenuPaste();
		}
	}

	public void MenuPaste()
	{
		toolbarsManager.Tools["InsertWorkItem"].SharedProps.Enabled = false;
		toolbarsManager.Tools["InsertMainItem"].SharedProps.Enabled = false;
		int RowIndex = gridBudget.Row;
		if (F_NewAddItemFlag == "0" && F_IsNewProject == "")
		{
			if (FormActionName == PccesFormAction.BUD)
			{
				if (GetCurrentBDGT_Type() == "CNT")
				{
					ExecuteCopyToTmpCNT("");
					SetupRestoreSnapshotListCNT();
				}
				else
				{
					ExecuteCopyToTmp("");
					SetupRestoreSnapshotList();
				}
			}
			else
			{
				ExecuteCopyToTmp("");
				SetupRestoreSnapshotList();
			}
			F_NewAddItemFlag = "1";
		}
		int SelectWorkItemCount = 0;
		int SuccessAddedCount = 0;
		int iParentSno = 0;
		int iSortOrder = 0;
		if (ArchConvert.Obj2String(gridBudget[RowIndex, "kind"]) == "B")
		{
			iParentSno = ArchConvert.Obj2Int(gridBudget[RowIndex, "sNo"]);
			Node Nd = gridBudget.Rows[RowIndex].Node.GetNode(NodeTypeEnum.LastChild);
			iSortOrder = ((Nd == null) ? 1 : (ArchConvert.Obj2Int(gridBudget[Nd.Row.Index, "SortOrder"]) + 1));
		}
		else
		{
			iParentSno = ArchConvert.Obj2Int(gridBudget[RowIndex, "ParentSno"]);
			iSortOrder = ArchConvert.Obj2Int(gridBudget[RowIndex, "SortOrder"]);
		}
		ArrayList ArrItemA = new ArrayList();
		gridBudget.Enabled = false;
		FM_INFO = new FormSys_G_Info1();
		FM_INFO._MaxValue = 100;
		FM_INFO._MinValue = 0;
		FM_INFO._ProgressValue = 0;
		FM_INFO._InfoString = "項目插入中，請稍候! ";
		FM_INFO.Show();
		Application.DoEvents();
		Cursor = Cursors.WaitCursor;
		int iLastItemLevel = ((gridBudget.Rows[RowIndex].Node == null) ? 1 : (gridBudget.Rows[RowIndex].Node.Level + 1));
		if (gridBudget[RowIndex, "Kind"].ToString().Trim() != "B")
		{
			iLastItemLevel--;
		}
		string sParentPrintToAnalysis = ((gridBudget[RowIndex, "PrintToAnalysis"] != null) ? gridBudget[RowIndex, "PrintToAnalysis"].ToString() : "0");
		DataTable dtSource = DS1.Tables[0];
		string[] sNo = new string[dtSource.Rows.Count];
		string[] pubCode = new string[dtSource.Rows.Count];
		for (int i = 0; i < dtSource.Rows.Count; i++)
		{
			pubCode[i] = dtSource.Rows[i]["PubCode"].ToString();
		}
		FM_INFO._ProgressValue = 25;
		MrsBaseManager mrsBaseManager = new MrsBaseManager();
		DataSet CompleteWorkItem = mrsBaseManager.GetCompleteWorkItem(F_FromDBName, pubCode);
		for (int i = 0; i < CompleteWorkItem.Tables["MrsA"].Rows.Count; i++)
		{
			CompleteWorkItem.Tables["MrsA"].Rows[i]["memo"] = CompleteWorkItem.Tables["MrsA"].Rows[i]["memo"].ToString().Replace("共通性項目", "").Replace("對照性項目", "");
		}
		Archnowledge.Pcces.DatabaseAccess.DatabaseAccess.UseDatabase(currentDBName);
		if (CompleteWorkItem.Tables.Count == 0)
		{
			MessageBox.Show("取得工項詳細資料時失敗,請確認挑選來源資料庫已升至最新版本");
			FM_INFO.Close();
			FM_INFO.Dispose();
			return;
		}
		FM_INFO._ProgressValue = 40;
		BudProjMrsManager budProjMrsManager = new BudProjMrsManager();
		budProjMrsManager.SetCompleteWorkItem(projectCode, CompleteWorkItem, userID, "", "", Overwrite: false);
		FM_INFO._ProgressValue = 65;
		ExecResult ER = new ExecResult();
		int SNo = 0;
		gridBudget.Redraw = false;
		SelectWorkItemCount = dtSource.Rows.Count;
		for (int j = 0; j < dtSource.Rows.Count; j++)
		{
			object UsrQty = 0;
			object UsrAmt = 0;
			object Cost = 0;
			DataRow theRow = dtSource.Rows[j];
			DataSet dsProjMrsA = theProjMrsA.GetProjMrsAByPccesCode(projectCode, dtSource.Rows[j]["PccesCode"].ToString().Trim());
			if (dsProjMrsA.Tables.Count > 0 && dsProjMrsA.Tables[0].Rows.Count > 0)
			{
				DataRow theProjMrsARow = dsProjMrsA.Tables[0].Rows[0];
				theRow["pubCode"] = theProjMrsARow["pubCode"];
				theRow["cName"] = theProjMrsARow["cName"];
				theRow["UnitName"] = theProjMrsARow["UnitName"];
				theRow["eName"] = theProjMrsARow["eName"];
				theRow["eUnit"] = theProjMrsARow["eUnit"];
				Cost = theProjMrsARow["Cost"];
			}
			else
			{
				Cost = theRow["Cost"];
			}
			if (F_ChangeQTY != null && F_ChangeQTY == "QTY")
			{
				UsrQty = theRow["Qty"];
			}
			string ItemNo = "";
			int PwrSetCode = PwrSet.GetCode(dsPwrSet, ArchConvert.Obj2String(theRow["PwrSet"]).Trim());
			ER = theItemA.AddItemAByParent(projectCode, null, "0000", null, ItemNo, iLastItemLevel, theRow["cName"], theRow["EName"], theRow["UnitName"], "W", Cost, UsrQty, UsrAmt, theRow["Memo"], 0, null, null, null, null, theRow["EUnit"], null, null, null, null, "", null, null, null, theRow["PccesCode"], null, null, theRow["CostDec"], theRow["AmtDec"], null, null, null, sParentPrintToAnalysis, theRow["surName"], false, null, null, null, null, null, null, PwrSetCode, iParentSno, iSortOrder, out SNo);
			if (ER.ReturnCode == 0)
			{
				SuccessAddedCount++;
				PageBreak thePageBreak = new BudPageBreak();
				thePageBreak.AddPageBreakIfExist(projectCode, SNo, "Y");
			}
			sNo[j] = SNo.ToString();
			if (j % 5 == 0)
			{
				Application.DoEvents();
			}
		}
		FM_INFO._ProgressValue = 85;
		if (ER.ReturnCode == 0 && SuccessAddedCount != 0)
		{
			if (SuccessAddedCount != SelectWorkItemCount)
			{
				MessageBox.Show("注意,挑選新增基本工項" + SelectWorkItemCount + "項,實際成功新增" + SuccessAddedCount + "項");
			}
			ER = theProject.ReArrangePrintNo(projectCode, iParentSno, !IsEditItemNo);
			if (ER.ReturnCode == 0)
			{
				ReloadGridAtRootSno(iParentSno);
			}
			else
			{
				MessageBox.Show("重整項次失敗:" + ER.Message + "\n將重整整個專案並全部重新載入...");
				theProject.ReArrangePrintNo(projectCode, 0, !IsEditItemNo);
				Data2Grid();
			}
		}
		else
		{
			MessageBox.Show("插入專案工項失敗,未插入任何項目");
		}
		CheckIsReCal("Y");
		if (ER.ReturnCode != 0)
		{
			MessageBox.Show(ER.Message);
		}
		FM_INFO._ProgressValue = 95;
		gridBudget.Redraw = true;
		CommonMethods.WriteIniValue("BidSet", "StateAdd", "TRUE");
		FM_INFO._ProgressValue = 100;
		gridBudget.Enabled = true;
		toolbarsManager.Tools["InsertWorkItem"].SharedProps.Enabled = true;
		toolbarsManager.Tools["InsertMainItem"].SharedProps.Enabled = true;
		gridBudget.Refresh();
		FM_INFO.Close();
		FM_INFO.Dispose();
		FM_INFO = null;
		GC.Collect();
	}

	private void button3_Click(object sender, EventArgs e)
	{
		for (int i = 1; i < gridBudget.Rows.Count; i++)
		{
			if (gridBudget[i, "LevelNo"] != null)
			{
				gridBudget.Rows[i].Node.Level = (int)gridBudget[i, "LevelNo"];
			}
		}
	}

	private void gridBudget1_KeyDown(object sender, KeyEventArgs e)
	{
		int RowIndex = gridBudget.Row;
		int ColIndex = gridBudget.Col;
		Archnowledge.Common.DebugUtil.OutputDebugString("gridBudget1_KeyDown(" + RowIndex + "," + ColIndex + ")" + F_ModifyMode);
		if (RowIndex <= 0 || ColIndex <= 0 || IsLocked)
		{
			return;
		}
		if (e.Control && e.KeyCode == Keys.Return)
		{
			gridBudget1_DoubleClick(sender, e);
		}
		if (e.Alt && e.KeyCode == Keys.Z && toolbarsManager.Tools["EditCostBreakdown"].SharedProps.Enabled)
		{
			ExecuteBreakdownForm();
		}
		if (e.Control && e.KeyCode == Keys.A)
		{
			for (int i = 1; i < gridBudget.Rows.Count; i++)
			{
				gridBudget.Rows[i].Selected = true;
			}
		}
		Row gridRow = gridBudget.Rows[RowIndex];
		if (e.KeyCode != Keys.F4 || FormActionName != PccesFormAction.BUD || IsTemplate || gridBudget.SelectedRowCount != 1 || gridRow["PccesCode"] == null || !(gridRow["PccesCode"].ToString() != "") || NotAllowEditingInCostEst(gridRow["PccesCode"].ToString()) || budgetType == BudgetType.Types.CostQuotation)
		{
			return;
		}
		object Lock = gridRow["Lock"];
		if (Lock != null && Lock != DBNull.Value && Convert.ToBoolean(Lock))
		{
			MessageBox.Show("此工項已存在前一版預算書，所以不可以換碼", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		bool LockItem = false;
		bool SubplanItem = false;
		if (SysConfig.SysChangeManagement && budgetType == BudgetType.Types.Execution)
		{
			BudProjMrsA theMrsA = new BudProjMrsA();
			if (!theMrsA.CheckSourceItemCanOverwrite(currentDBName, projectCode, gridRow["PccesCode"].ToString(), projectCode))
			{
				LockItem = true;
			}
		}
		if (!LockItem && SysConfig.SysComsEnable && SysConfig.SysEditAfterBudLem.ToUpper() == "DISABLE")
		{
			Archnowledge.Pcces.DomainModule.Coms.BudgetCtrl theBudgetCtrl = new Archnowledge.Pcces.DomainModule.Coms.BudgetCtrl();
			if (SysConfig.SysComsEnable && budgetType == BudgetType.Types.Execution && theBudgetCtrl.IsWorkItemInSubPlanCart(projectCode, SysConfig.SysComsDB, gridRow["PccesCode"].ToString()))
			{
				SubplanItem = true;
			}
		}
		if (LockItem || SubplanItem)
		{
			if (LockItem)
			{
				MessageBox.Show("此工項或其父項已存在前一版預算書，所以不可以換碼", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBox.Show("此工項或其父項已進入分包規劃，所以不可以換碼", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return;
		}
		FormMrsBaseChgCode FMCHGCOD = new FormMrsBaseChgCode();
		FMCHGCOD._UserID = userID;
		FMCHGCOD._PccesCode = gridRow["PccesCode"].ToString();
		FMCHGCOD._PubCode = (int)gridRow["PubCode"];
		FMCHGCOD._CName = gridRow["CName"].ToString();
		FMCHGCOD._ActionName = FormActionName;
		FMCHGCOD._ProjectCode = projectCode;
		FMCHGCOD.Owner = this;
		if (FMCHGCOD.ShowDialog() == DialogResult.OK)
		{
			F_SNo = ArchConvert.Obj2Int(gridRow["Sno"]);
			LoadProjectData();
			F_SNo = -1;
			CheckIsReCal("Y");
		}
		FMCHGCOD.Close();
		FMCHGCOD.Dispose();
		FMCHGCOD = null;
	}

	private void gridBudget1_AfterSelChange(object sender, RangeEventArgs e)
	{
		Archnowledge.Common.DebugUtil.OutputDebugString("gridBudget1_AfterSelChange Start(" + gridBudget.MouseRow + "," + gridBudget.MouseCol + ")" + F_ModifyMode);
		if (FORM_STATUS == FormStatus.Binding || IsLocked)
		{
			return;
		}
		iAuthorityMSG_Count = 0;
		int RowIndex = gridBudget.RowSel;
		int ColIndex = gridBudget.ColSel;
		if (RowIndex <= 0 || ColIndex <= 0)
		{
			return;
		}
		SetSelectedItemAmount();
		toolbarsManager.BeginUpdate();
		Row GridRow = gridBudget.Rows[RowIndex];
		string PrintNo = "";
		string Kind = "";
		if (gridBudget.Rows[RowIndex]["sNo"] != null)
		{
			int RowSno = ArchConvert.Obj2Int(GridRow["Sno"]);
			if (RowSno != LastSelectSno)
			{
				string OldPrintNo = GridRow["PrintNo"].ToString();
				Reload_OneRow(RowSno, RowIndex, RangeUpdate: false);
				if (OldPrintNo != GridRow["PrintNo"].ToString())
				{
					MessageBox.Show("檢測到畫面上的數據與數據庫不一致,將立即重新載入專案資料");
					F_SNo = RowSno;
					LoadProjectData();
					F_SNo = -1;
				}
			}
			LastSelectSno = RowSno;
			if (GridRow["PrintNo"] != null && GridRow["Kind"] != null)
			{
				PrintNo = GridRow["PrintNo"].ToString().Trim();
				Kind = GridRow["Kind"].ToString().Trim();
			}
		}
		toolbarsManager.Tools["Cut"].SharedProps.Enabled = true;
		toolbarsManager.Tools["Copy"].SharedProps.Enabled = true;
		toolbarsManager.Tools["CloneWorkItem"].SharedProps.Enabled = true;
		toolbarsManager.Tools["Paste"].SharedProps.Enabled = dtClipboard.Rows.Count > 0;
		toolbarsManager.Tools["InsertMainItemChildren"].SharedProps.Visible = true;
		toolbarsManager.Tools["InsertMainItem"].SharedProps.Enabled = true;
		toolbarsManager.Tools["InsertWorkItem"].SharedProps.Enabled = true;
		toolbarsManager.Tools["AddNewWorkItem"].SharedProps.Enabled = currentDBName != companyDBName;
		toolbarsManager.Tools["InsertMainItemChildren"].SharedProps.Enabled = true;
		toolbarsManager.Tools["CancelAmortizedItem"].SharedProps.Enabled = false;
		if (FORM_STATUS == FormStatus.Normal)
		{
			if (!gridBudget.Rows[RowIndex].IsNode)
			{
				toolbarsManager.Tools["EditMainItem"].SharedProps.Enabled = false;
				toolbarsManager.Tools["EditWorkItem"].SharedProps.Enabled = false;
				toolbarsManager.Tools["EditCostBreakdown"].SharedProps.Enabled = false;
				toolbarsManager.Tools["InsertMainItem"].SharedProps.Enabled = RowIndex == 1;
				toolbarsManager.Tools["InsertWorkItem"].SharedProps.Enabled = false;
				toolbarsManager.Tools["Delete"].SharedProps.Enabled = false;
				toolbarsManager.Tools["Cut"].SharedProps.Enabled = false;
				toolbarsManager.Tools["Copy"].SharedProps.Enabled = false;
				toolbarsManager.Tools["CloneWorkItem"].SharedProps.Enabled = false;
				toolbarsManager.Tools["Paste"].SharedProps.Enabled = false;
				toolbarsManager.Tools["LockCost"].SharedProps.Enabled = false;
				toolbarsManager.Tools["UnLockCost"].SharedProps.Enabled = false;
				toolbarsManager.Tools["ImportSelectedMrsBaseItemCost"].SharedProps.Enabled = false;
				toolbarsManager.Tools["ImportSelectedMrsBaseCostBreakdown"].SharedProps.Enabled = false;
				toolbarsManager.EndUpdate();
				return;
			}
			toolbarsManager.Tools["CloneWorkItem"].SharedProps.Enabled = false;
			toolbarsManager.Tools["EditMainItem"].SharedProps.Enabled = false;
			toolbarsManager.Tools["EditWorkItem"].SharedProps.Enabled = false;
			toolbarsManager.Tools["EditCostBreakdown"].SharedProps.Enabled = false;
			toolbarsManager.Tools["InsertWorkItem"].SharedProps.Enabled = false;
			toolbarsManager.Tools["MakeAmortizedItem"].SharedProps.Enabled = false;
			toolbarsManager.Tools["Delete"].SharedProps.Enabled = false;
			if (gridBudget[RowIndex, "Kind"] != null)
			{
				toolbarsManager.Tools["Cut"].SharedProps.Enabled = true;
				toolbarsManager.Tools["Copy"].SharedProps.Enabled = true;
				toolbarsManager.Tools["CloneWorkItem"].SharedProps.Enabled = true;
				toolbarsManager.Tools["Paste"].SharedProps.Enabled = dtClipboard.Rows.Count > 0;
				toolbarsManager.Tools["InsertWorkItem"].SharedProps.Enabled = true;
				toolbarsManager.Tools["AddNewWorkItem"].SharedProps.Enabled = currentDBName != companyDBName;
				toolbarsManager.Tools["Delete"].SharedProps.Enabled = true;
				toolbarsManager.Tools["EditWorkItem"].SharedProps.Enabled = true;
				toolbarsManager.Tools["EditMainItem"].SharedProps.Enabled = !IsSubmitBid;
			}
			if (Kind != "" && FormActionName == PccesFormAction.BUD)
			{
				if ((Kind == "L" || Kind == "F") && GridRow["CName"].ToString().IndexOf("營業稅") < 0 && GridRow["CName"].ToString().IndexOf("加值稅") < 0)
				{
					if (GridRow["IsShared"].ToString() == "1")
					{
						toolbarsManager.Tools["MakeAmortizedItem"].SharedProps.Enabled = false;
						toolbarsManager.Tools["CancelAmortizedItem"].SharedProps.Enabled = true;
					}
					else
					{
						toolbarsManager.Tools["MakeAmortizedItem"].SharedProps.Enabled = true;
						toolbarsManager.Tools["CancelAmortizedItem"].SharedProps.Enabled = false;
					}
				}
				else
				{
					switch (Kind)
					{
					default:
						if (!IsSubmitBid)
						{
							break;
						}
						goto case "B";
					case "B":
					case "Z":
					case "W":
					case "S":
					case "U":
						toolbarsManager.Tools["MakeAmortizedItem"].SharedProps.Enabled = false;
						break;
					}
				}
				toolbarsManager.Tools["Delete"].SharedProps.Enabled = !IsTemplate;
			}
			else if (Kind != "")
			{
				switch (Kind)
				{
				default:
					if (!(Kind == "U"))
					{
						break;
					}
					goto case "L";
				case "L":
				case "F":
				case "S":
					if (GridRow["IsShared"].ToString() == "1")
					{
						toolbarsManager.Tools["MakeAmortizedItem"].SharedProps.Enabled = false;
						if (!IsSubmitBid)
						{
							toolbarsManager.Tools["CancelAmortizedItem"].SharedProps.Enabled = true;
						}
						else
						{
							toolbarsManager.Tools["CancelAmortizedItem"].SharedProps.Enabled = false;
						}
					}
					else
					{
						toolbarsManager.Tools["MakeAmortizedItem"].SharedProps.Enabled = true;
						toolbarsManager.Tools["CancelAmortizedItem"].SharedProps.Enabled = false;
					}
					break;
				}
				if (IsSubmitBid)
				{
					toolbarsManager.Tools["MakeAmortizedItem"].SharedProps.Enabled = false;
				}
			}
			if (Kind == "W")
			{
				toolbarsManager.Tools["EditMainItem"].SharedProps.Enabled = false;
				toolbarsManager.Tools["InsertMainItemChildren"].SharedProps.Enabled = false;
			}
			if (Kind != "W")
			{
				toolbarsManager.Tools["EditWorkItem"].SharedProps.Enabled = false;
				toolbarsManager.Tools["EditCostBreakdown"].SharedProps.Enabled = false;
				toolbarsManager.Tools["CloneWorkItem"].SharedProps.Enabled = false;
			}
			if (Kind != "B")
			{
				switch (Kind)
				{
				default:
					if (!(Kind == "U"))
					{
						toolbarsManager.Tools["InsertMainItemChildren"].SharedProps.Enabled = false;
						break;
					}
					goto case "L";
				case "L":
				case "F":
				case "Z":
				case "S":
					toolbarsManager.Tools["InsertMainItemChildren"].SharedProps.Enabled = false;
					break;
				}
				if (Kind != "W")
				{
					toolbarsManager.Tools["InsertWorkItem"].SharedProps.Enabled = false;
					toolbarsManager.Tools["EditCostBreakdown"].SharedProps.Enabled = false;
				}
			}
			else if (PrintNo.Length / 4 >= 8)
			{
				toolbarsManager.Tools["InsertMainItemChildren"].SharedProps.Enabled = false;
			}
			string NineNine = "".PadLeft(32, '9');
			if (PrintNo == NineNine)
			{
				toolbarsManager.Tools["EditMainItem"].SharedProps.Enabled = true;
				if (FormActionName == PccesFormAction.BID && IsSubmitBid)
				{
					toolbarsManager.Tools["EditMainItem"].SharedProps.Enabled = false;
				}
				toolbarsManager.Tools["EditCostBreakdown"].SharedProps.Enabled = false;
				toolbarsManager.Tools["InsertWorkItem"].SharedProps.Enabled = false;
				toolbarsManager.Tools["EditWorkItem"].SharedProps.Enabled = false;
				toolbarsManager.Tools["CloneWorkItem"].SharedProps.Enabled = false;
				toolbarsManager.Tools["EditCostBreakdown"].SharedProps.Enabled = false;
				toolbarsManager.Tools["Delete"].SharedProps.Enabled = false;
				toolbarsManager.Tools["InsertMainItem"].SharedProps.Enabled = false;
			}
			if (gridBudget[RowIndex, "Analysis"] != null && ArchConvert.Obj2Bool(gridBudget[RowIndex, "Analysis"]))
			{
				toolbarsManager.Tools["EditCostBreakdown"].SharedProps.Enabled = true;
			}
			else
			{
				toolbarsManager.Tools["EditCostBreakdown"].SharedProps.Enabled = false;
			}
			if (FormActionName == PccesFormAction.BID || (FormActionName == PccesFormAction.BUD && budgetType == BudgetType.Types.CostQuotation))
			{
				toolbarsManager.Tools["Cut"].SharedProps.Enabled = false;
				toolbarsManager.Tools["Copy"].SharedProps.Enabled = false;
				toolbarsManager.Tools["Paste"].SharedProps.Enabled = false;
				toolbarsManager.Tools["CloneWorkItem"].SharedProps.Enabled = false;
				toolbarsManager.Tools["EditWorkItem"].SharedProps.Enabled = false;
				toolbarsManager.Tools["InsertMainItem"].SharedProps.Enabled = false;
				toolbarsManager.Tools["InsertWorkItem"].SharedProps.Enabled = false;
				if (gridBudget[RowIndex, "cname"] != null && gridBudget[RowIndex, "cname"].ToString() == "調價後差額")
				{
					toolbarsManager.Tools["Delete"].SharedProps.Visible = true;
					toolbarsManager.Tools["Delete"].SharedProps.Enabled = true;
				}
				else
				{
					toolbarsManager.Tools["Delete"].SharedProps.Visible = false;
					toolbarsManager.Tools["Delete"].SharedProps.Enabled = false;
				}
				if (PrintNo == NineNine && !IsSubmitBid)
				{
					toolbarsManager.Tools["Delete"].SharedProps.Visible = true;
					toolbarsManager.Tools["Delete"].SharedProps.Enabled = true;
				}
			}
			if (IsMainItem(Kind) && FormActionName == PccesFormAction.BUD && budgetType == BudgetType.Types.CostEstimation)
			{
				toolbarsManager.Tools["Cut"].SharedProps.Enabled = false;
				toolbarsManager.Tools["Copy"].SharedProps.Enabled = false;
			}
			if (ArchConvert.Obj2Bool(gridBudget.Rows[RowIndex]["LockCost"]))
			{
				toolbarsManager.Tools["LockCost"].SharedProps.Enabled = false;
				toolbarsManager.Tools["UnLockCost"].SharedProps.Enabled = true;
			}
			else
			{
				toolbarsManager.Tools["LockCost"].SharedProps.Enabled = true;
				toolbarsManager.Tools["UnLockCost"].SharedProps.Enabled = false;
			}
			CanEditCheck(gridBudget.MouseCol);
			if (FormActionName == PccesFormAction.BUD && budgetType == BudgetType.Types.Execution)
			{
				bool isItemLocked = (budgetChangeCurrentVersion > 0 && ArchConvert.Obj2Bool(gridBudget[RowIndex, "Lock"])) || gridBudget[RowIndex, "sNo"] == null;
				string[] disabledList = new string[4] { "ImportFromMrsBase", "ImportSelectedMrsBaseItemCost", "ImportSelectedMrsBaseCostBreakdown", "ClearDetailListCost" };
				if (!isItemLocked && SysConfig.SysComsEnable && SysConfig.SysEditAfterBudLem.ToUpper() == "DISABLE" && ArchConvert.Obj2String(gridBudget[RowIndex, "CostKind"]) == "" && !AllowChangeBySNo(ArchConvert.Obj2Int(gridBudget[RowIndex, "CostKind"]), silentOnWarning: true, silentOnModify: true))
				{
					isItemLocked = true;
				}
				SetButtonListAvailibility(disabledList, !isItemLocked);
				if (budgetChangeCurrentVersion > 0)
				{
					SetShowOnlyChangedItemToolbarStatus();
				}
			}
			if (FormActionName == PccesFormAction.BID)
			{
				toolbarsManager.Tools["ImportSelectedMrsBaseCostBreakdown"].SharedProps.Enabled = !IsLockAnalys;
			}
			if (gridBudget.Rows[RowIndex]["sNo"] == null)
			{
				toolbarsManager.Tools["ImportSelectedMrsBaseItemCost"].SharedProps.Enabled = false;
				toolbarsManager.Tools["ImportSelectedMrsBaseCostBreakdown"].SharedProps.Enabled = false;
			}
		}
		toolbarsManager.Enabled = true;
		toolbarsManager.EndUpdate();
	}

	private void SetSelectedItemAmount()
	{
		double selectedItemAmount = 0.0;
		int itemAmountPrecision = 2;
		for (int i = 1; i < gridBudget.Rows.Count; i++)
		{
			if (gridBudget.Rows[i].Selected && gridBudget[i, "Amount"] != null)
			{
				itemAmountPrecision = ArchConvert.Obj2Int(gridBudget[i, "AmtDec"]);
				selectedItemAmount += PubTools.ARound(PubTools.Str2Double(gridBudget[i, "Amount"]), itemAmountPrecision);
			}
		}
		statusBar.Panels[2].Text = "加總=" + string.Format("{0:N" + MainItemAmountPrecision + "}", selectedItemAmount);
	}

	private void BtnSwitchProject_Click_1(object sender, EventArgs e)
	{
		toolbarsManager.BeginUpdate();
		dtClipboard.Clear();
		toolbarsManager.Tools["Paste"].SharedProps.Enabled = false;
		toolbarsManager.Tools["Cut"].SharedProps.Enabled = true;
		toolbarsManager.Tools["Copy"].SharedProps.Enabled = true;
		Execute_SwitchProject();
		if (MainItemCostPrecison > 2 || MainItemAmountPrecision > 2 || AnalysisCostPrecision > 2 || AnalysisAmountPrecision > 2)
		{
			FormBudgetExp_Wzd_Help1 FM_HELP1 = new FormBudgetExp_Wzd_Help1();
			FM_HELP1.ShowDialog();
			FM_HELP1.Close();
			FM_HELP1.Dispose();
			FM_HELP1 = null;
		}
		toolbarsManager.EndUpdate();
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

	private void frmBudget_Resize(object sender, EventArgs e)
	{
		lock (this)
		{
			int TotalH = pnl_spliter.Height;
			int iHeight = (TotalH - 3 - 3 - 57) / 2;
			ssp_Upper.Height = iHeight;
			ssp_Lower.Height = iHeight;
		}
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
	}

	private void ExecuteReportForm()
	{
		OpenExportExcelDialog(string.Empty, IsPreview: true);
	}

	private void ExecuteBreakdownForm()
	{
		bool IsSurName = ((toolbarsManager.Tools["ShowAliasColumn"] as StateButtonTool).Checked ? true : false);
		F_IsUseIR = theProject.GetUseIR(projectCode);
		if (gridBudget[gridBudget.Row, "Analysis"] != null && (bool)gridBudget[gridBudget.Row, "Analysis"] && gridBudget[gridBudget.Row, "PubCode"] != null)
		{
			FormMrsBaseBreakdown frmBD = new FormMrsBaseBreakdown();
			frmBD.PubCode = (int)gridBudget[gridBudget.Row, "PubCode"];
			frmBD.ProjectCode = projectCode;
			frmBD._ActionName = FormActionName;
			frmBD._CallType = "ItemA";
			frmBD._UserID = userID;
			frmBD._IsUseIR = F_IsUseIR;
			frmBD._IsSBID = IsSubmitBid;
			frmBD._IsLocked = IsLocked;
			bool lockBreakdown = IsTemplate || (gridBudget[gridBudget.Row, "Lock"] != null && gridBudget[gridBudget.Row, "Lock"] != DBNull.Value && Convert.ToBoolean(gridBudget[gridBudget.Row, "Lock"])) || NotAllowEditingInCostEst(gridBudget[gridBudget.Row, "PccesCode"].ToString()) || !AllowChangeBySNo((int)gridBudget[gridBudget.Row, "sNo"], silentOnWarning: true, silentOnModify: true);
			bool IsChangeControl = false;
			if (budgetType == BudgetType.Types.CostEstimation || budgetType == BudgetType.Types.CostQuotationMerged)
			{
				IsChangeControl = ((gridBudget[gridBudget.Row, "CostBeforeChange"] != DBNull.Value) ? true : false);
			}
			frmBD._Istemplate = lockBreakdown || IsLocked || IsChangeControl;
			frmBD._CompanyDBName = companyDBName;
			if (FormActionName == PccesFormAction.BID)
			{
				frmBD._IsLockAn = GetIsLockAnalys();
				frmBD._IsLockAnalysLEMWQty = GetIsLockAnalysLEMWQty();
			}
			frmBD._IsSurName = IsSurName;
			if (ArchConvert.Obj2String(gridBudget[gridBudget.Row, "CostDec"]) != "")
			{
				frmBD._iCostDigital = Convert.ToInt32(gridBudget[gridBudget.Row, "CostDec"].ToString());
			}
			F_SNo = (int)gridBudget[gridBudget.Row, "sNO"];
			F_IsGoBackOriginalRow = true;
			frmBD.Owner = this;
			if (GridColsSquenceForAnalysis != null)
			{
				frmBD._GridColsSquenceInAnalysis = GridColsSquenceForAnalysis;
			}
			try
			{
				frmBD.ShowDialog();
			}
			catch (Exception)
			{
			}
			frmBD.Close();
			frmBD.Dispose();
			frmBD = null;
			int iPos = gridBudget.Row;
			int iSno = (int)gridBudget[gridBudget.Row, "SNo"];
			if (F_IsNeedToReloadAllData)
			{
				LoadProjectData();
				F_IsNeedToReloadAllData = false;
			}
			else
			{
				Reload_OneRow(iSno, iPos, RangeUpdate: false);
			}
			F_SNo = -1;
			CalcuParent(iPos);
			CheckIsReCal(F_IsAnConfirmReCal);
			HasOpenedBreakdownForm = false;
		}
	}

	private void ExecuteCopyToTmp(string sType)
	{
		if (SysConfig.SysComsEnable && budgetType == BudgetType.Types.Execution)
		{
			return;
		}
		if (sType != "")
		{
			string warning = string.Empty;
			if (FormActionName == PccesFormAction.BUD)
			{
				warning = "執行此功能會將這份預算書保存一版並記錄保存的時間\n\n若要將此份預算書回復，請至【工具】-->【回存舊版預算書\n\n是否要保存?";
			}
			else if (FormActionName == PccesFormAction.BID)
			{
				warning = "執行此功能會將這份標單保存一版並記錄保存的時間\n\n若要將此份標單回復，請至【工具】-->【回存舊版標單\n\n是否要保存?";
			}
			if (MessageBox.Show(this, warning, "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
			{
				return;
			}
		}
		int iCount = 0;
		int iMax = 0;
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(userID);
		aArr.Add("複製所有項目有回復原來的功能--" + projectCode);
		string l_str = string.Concat("select Max(version) as version from tmpProject where projectCode = '", projectCode, "'  and sKind = '", FormActionName, "'");
		ModifyDB StdCom = new ModifyDB(projectCode, aArr);
		DataTable ldt_mytable = StdCom.DBList(l_str);
		Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
		PROJ.ps_projectCode = projectCode;
		PROJ.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
		DataTable dt = PROJ.ListItemTmp();
		if (ldt_mytable.Rows.Count > 0)
		{
			iMax = PubTools.Str2Int(ldt_mytable.Rows[0]["version"].ToString());
			if (dt.Rows.Count > 49)
			{
				iCount = CheckVersion(dt);
				if (iCount == 0)
				{
					string warning = string.Empty;
					if (FormActionName == PccesFormAction.BUD)
					{
						warning = "您所儲存預算書版本已二十筆\n\n若要再儲存此份預算書\n\n請至【工具】-->【設定...】中刪除舊版預算書!!";
					}
					else if (FormActionName == PccesFormAction.BID)
					{
						warning = "您所儲存標單版本已二十筆\n\n若要再儲存此份標單\n\n請至【工具】-->【設定...】中刪除舊版標單!!";
					}
					MessageBox.Show(this, warning, "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					return;
				}
				PROJ.DeleProjTmp(projectCode, iCount.ToString());
			}
		}
		PROJ.CopyTmpProj(projectCode, (iMax + 1).ToString());
		try
		{
			string sBud = string.Concat("Insert Into tmpProject(ProjectCode, mainCode, projectNameC, projectNameE, projectAddress, accountCode1,projectCodeAlias, accountCode2, buyMode, workMode, expectDaily, projectScope, workUnit, projamt, AutoCalcCost, UseIR, CloseBidDate, IsQtyModifiable, BudStartYear, BudEndYear, ExpectStartDate,version,sKind,NewDate,shareVDF1, shareVDF1sNo) Select '", projectCode, "', mainCode, projectNameC, projectNameE, projectAddress, accountCode1,projectCodeAlias, accountCode2, buyMode, workMode, expectDaily, projectScope, workUnit, projamt, AutoCalcCost, UseIR, CloseBidDate, IsQtyModifiable, BudStartYear, BudEndYear, ExpectStartDate, '", (iMax + 1).ToString(), "' as version,'", FormActionName, "' as sKind,'", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), "' as NewDate,shareVDF1, shareVDF1sNo From ", CommonMethods.GetActionNameString(FormActionName), "Project Where ProjectCode ='", projectCode, "' ");
			StdCom.DBUpd(sBud);
			if (sType != "")
			{
				FormMemo FM = new FormMemo();
				FM._ProjectCode = projectCode;
				FM._UserID = userID;
				FM._iCount = (iMax + 1).ToString();
				FM._ActionName = FormActionName;
				if (DialogResult.OK == FM.ShowDialog(this))
				{
				}
				FM.Close();
				FM.Dispose();
				FM = null;
			}
			if (sType != "")
			{
				MessageBox.Show(this, "保存完成!!", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudget.cs--ExecuteCopyToTmp" + ex.Message);
		}
		if (PROJ.GetCurrentProjectActionName(projectCode).ToUpper() == "CNT" && MessageBox.Show(this, "目前是【契約書編輯】是否立即切換成【預算書編輯】?", "訊息", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			SetCurrentBDGT_Type("BUD");
			GetCurrentBDGT_Type();
			toolbarsManager.Tools["PreviewReport"].SharedProps.Enabled = true;
		}
		StdCom = null;
		PROJ = null;
		aArr = null;
	}

	private void ExecuteCopyToTmpCNT(string sType)
	{
		if (sType != "")
		{
			string sAlter = "契約書";
			string warning = "執行此功能會將這份" + sAlter + "保存成一版【契約書】並記錄保存的時間\n\n若要將此份" + sAlter + "回復，請至【工具】-->【回存舊版契約書】\n\n是否要保存?";
			if (MessageBox.Show(this, warning, "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
			{
				return;
			}
			if (GetCurrentBDGT_Type() != "CNT")
			{
				ExecuteCopyToTmp("");
				SetupRestoreSnapshotList();
			}
		}
		int iCount = 0;
		int iMax = 0;
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(userID);
		aArr.Add("複製所有項目有回復原來的功能--" + projectCode);
		string l_str = "select IsNull(Max(version), 50000) as version from tmpProject where projectCode = '" + projectCode + "'  and sKind = 'Cnt'";
		ModifyDB StdCom = new ModifyDB(projectCode, aArr);
		DataTable ldt_mytable = StdCom.DBList(l_str);
		Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
		PROJ.ps_projectCode = projectCode;
		PROJ.ps_srckind = "CNT";
		DataTable dt = PROJ.ListItemTmp();
		if (ldt_mytable.Rows.Count > 0)
		{
			iMax = PubTools.Str2Int(ldt_mytable.Rows[0]["version"].ToString());
			if (dt.Rows.Count > 49)
			{
				iCount = CheckVersion(dt);
				if (iCount == 0)
				{
					string warning = string.Empty;
					warning = "您所儲存『契約書』版本已20筆\n\n若要再儲存此份契約書\n\n請至【工具】-->【設定...】中刪除舊版契約書!!";
					MessageBox.Show(this, warning, "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					return;
				}
				PROJ.DeleProjTmp(projectCode, iCount.ToString());
			}
		}
		PROJ.CopyTmpProj(projectCode, (iMax + 1).ToString());
		try
		{
			string sBud = "Insert Into tmpProject(ProjectCode, mainCode, projectNameC, projectNameE, projectAddress, accountCode1,projectCodeAlias, accountCode2, buyMode, workMode, expectDaily, projectScope, workUnit, projamt, AutoCalcCost, UseIR, CloseBidDate, IsQtyModifiable, BudStartYear, BudEndYear, ExpectStartDate,version,sKind,NewDate,shareVDF1, shareVDF1sNo) Select '" + projectCode + "', mainCode, projectNameC, projectNameE, projectAddress, accountCode1,projectCodeAlias, accountCode2, buyMode, workMode, expectDaily, projectScope, workUnit, projamt, AutoCalcCost, UseIR, CloseBidDate, IsQtyModifiable, BudStartYear, BudEndYear, ExpectStartDate, '" + (iMax + 1) + "' as version,'CNT' as sKind,'" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' as NewDate,shareVDF1, shareVDF1sNo From " + CommonMethods.GetActionNameString(FormActionName) + "Project Where ProjectCode ='" + projectCode + "' ";
			StdCom.DBUpd(sBud);
			bool flag = false;
			FormMemo FM = new FormMemo();
			FM._ProjectCode = projectCode;
			FM._UserID = userID;
			FM._iCount = (iMax + 1).ToString();
			FM._ActionName = PccesFormAction.CNT;
			if (DialogResult.OK == FM.ShowDialog(this))
			{
			}
			FM.Close();
			FM.Dispose();
			FM = null;
			flag = false;
			MessageBox.Show(this, "保存完成!!", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudget.cs--ExecuteCopyToTmp" + ex.Message);
		}
		if (GetCurrentBDGT_Type().ToUpper() == "BUD" && MessageBox.Show(this, "目前是【預算書編輯】是否立即切換成【契約書編輯】?", "訊息", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			SetCurrentBDGT_Type("CNT");
			GetCurrentBDGT_Type();
			toolbarsManager.Tools["PreviewReport"].SharedProps.Enabled = false;
		}
		StdCom = null;
		PROJ = null;
		aArr = null;
	}

	private int CheckVersion(DataTable DT)
	{
		int iCount = 0;
		DataView DV = new DataView(DT);
		DV.Sort = "version";
		for (int i = 0; i < DV.Count; i++)
		{
			if (DV[i]["memo"] == DBNull.Value)
			{
				iCount = PubTools.Str2Int(DV[i]["version"]);
				break;
			}
		}
		return iCount;
	}

	private void Reload_OneRow(int iSno, int gridRow)
	{
		string IPStr = CommonMethods.GetIPAddress();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(userID);
		aArr.Add("預算書單價分析編輯完後重讀該筆資料--" + projectCode + "(" + IPStr + ")");
		if (dbItemA != null)
		{
			dbItemA = null;
		}
		if (dbItemA == null)
		{
			dbItemA = new Archnowledge.Pcces.BUDClass.ItemA(aArr);
		}
		dbItemA.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
		dbItemA.ps_projectCode = projectCode;
		DataTable DT_OneRow = dbItemA.ListItem(" sno=" + iSno, projectCode);
		if (DT_OneRow.Rows.Count > 0)
		{
			if (DT_OneRow.Rows[0]["analysis"].ToString().Trim() == "1")
			{
				gridBudget[gridRow, "Analysis"] = true;
				gridBudget.Rows[gridRow].Style = gridBudget.Styles["AnalysisColor"];
				CellRange rg = gridBudget.GetCellRange(gridRow, gridBudget.Cols["AnaImg"].SafeIndex);
				rg.Style = gridBudget.Styles["img"];
				rg.Image = imageList2.Images[0];
			}
			else
			{
				gridBudget[gridRow, "Analysis"] = false;
				gridBudget.Rows[gridRow].Style = gridBudget.Styles["Normal"];
				CellRange rg = gridBudget.GetCellRange(gridRow, gridBudget.Cols["AnaImg"].SafeIndex);
				rg.Style = gridBudget.Styles["img"];
				rg.Image = imageList2.Images[2];
			}
			gridBudget[gridRow, "ItemNo"] = DT_OneRow.Rows[0]["ItemNo"].ToString().Trim();
			gridBudget[gridRow, "CName"] = DT_OneRow.Rows[0]["cName"].ToString().Trim();
			gridBudget[gridRow, "UnitName"] = DT_OneRow.Rows[0]["unitName"].ToString().Trim();
			gridBudget[gridRow, "Qty"] = DT_OneRow.Rows[0]["qty"];
			gridBudget[gridRow, "Cost"] = DT_OneRow.Rows[0]["cost"];
			gridBudget[gridRow, "Amount"] = DT_OneRow.Rows[0]["amount"];
			gridBudget[gridRow, "PccesCode"] = DT_OneRow.Rows[0]["pccesCode"].ToString().Trim();
			gridBudget[gridRow, "Memo"] = DT_OneRow.Rows[0]["memo"].ToString().Trim();
			gridBudget[gridRow, "EName"] = DT_OneRow.Rows[0]["eName"].ToString().Trim();
			gridBudget[gridRow, "EUnit"] = DT_OneRow.Rows[0]["eUnit"].ToString().Trim();
			gridBudget[gridRow, "LevelNo"] = DT_OneRow.Rows[0]["levelNo"].ToString().Trim();
			gridBudget[gridRow, "SNo"] = DT_OneRow.Rows[0]["sno"];
			gridBudget[gridRow, "Kind"] = DT_OneRow.Rows[0]["kind"].ToString().Trim();
			gridBudget[gridRow, "PrintNo"] = DT_OneRow.Rows[0]["printNo"].ToString().Trim();
			gridBudget[gridRow, "Formula"] = DT_OneRow.Rows[0]["Formula"].ToString().Trim();
			gridBudget[gridRow, "PubCode"] = DT_OneRow.Rows[0]["pubCode"].ToString().Trim();
			gridBudget[gridRow, "PrintToAnalysis"] = DT_OneRow.Rows[0]["PrintToAnalysis"].ToString().Trim();
			gridBudget[gridRow, "surName"] = DT_OneRow.Rows[0]["surName"].ToString().Trim();
			gridBudget[gridRow, "Costkind"] = DT_OneRow.Rows[0]["costKind"].ToString().Trim();
			gridBudget[gridRow, "IsShared"] = DT_OneRow.Rows[0]["share"].ToString().Trim();
			gridBudget[gridRow, "LockCost"] = ArchConvert.Obj2Bool(DT_OneRow.Rows[0]["LockCost"]);
			if (ArchConvert.Obj2Bool(DT_OneRow.Rows[0]["LockCost"]))
			{
				toolbarsManager.Tools["LockCost"].SharedProps.Enabled = false;
				toolbarsManager.Tools["UnLockCost"].SharedProps.Enabled = true;
			}
			else
			{
				toolbarsManager.Tools["LockCost"].SharedProps.Enabled = true;
				toolbarsManager.Tools["UnLockCost"].SharedProps.Enabled = false;
			}
			if (FormActionName == PccesFormAction.BUD)
			{
				gridBudget[gridRow, "IsGreenItem"] = ArchConvert.Obj2Bool(DT_OneRow.Rows[0]["IsGreenItem"]);
				gridBudget[gridRow, "IsGreenMethod"] = ArchConvert.Obj2Bool(DT_OneRow.Rows[0]["IsGreenMethod"]);
				gridBudget[gridRow, "IsGreenMaterial"] = ArchConvert.Obj2Bool(DT_OneRow.Rows[0]["IsGreenMaterial"]);
				gridBudget[gridRow, "IsGreenEnergy"] = ArchConvert.Obj2Bool(DT_OneRow.Rows[0]["IsGreenEnergy"]);
			}
			toolbarsManager.BeginUpdate();
			string sKind = ((DT_OneRow.Rows[0]["kind"].ToString().Length > 0) ? DT_OneRow.Rows[0]["kind"].ToString().ToUpper().Trim() : "");
			if (sKind == "L")
			{
				if (DT_OneRow.Rows[0]["share"].ToString() == "1")
				{
					toolbarsManager.Tools["MakeAmortizedItem"].SharedProps.Enabled = false;
					toolbarsManager.Tools["CancelAmortizedItem"].SharedProps.Enabled = true;
					gridBudget.Rows[gridBudget.RowSel].Style = gridBudget.Styles["IsSharedColor"];
				}
				else
				{
					toolbarsManager.Tools["MakeAmortizedItem"].SharedProps.Enabled = true;
					toolbarsManager.Tools["CancelAmortizedItem"].SharedProps.Enabled = false;
					gridBudget.Rows[gridBudget.RowSel].Style = gridBudget.Styles["MainColor"];
				}
			}
			switch (sKind)
			{
			default:
				if (!(sKind == "U"))
				{
					break;
				}
				goto case "B";
			case "B":
			case "F":
			case "S":
			case "Z":
				gridBudget.Rows[gridRow].Style = gridBudget.Styles["MainColor"];
				break;
			}
			if (FormActionName == PccesFormAction.BUD && sKind == "W" && budgetChangeCurrentVersion > 0 && (gridBudget[gridRow, "QtyBeforeChange"] == null || ArchConvert.Obj2Double(gridBudget[gridRow, "QtyBeforeChange"]) != ArchConvert.Obj2Double(gridBudget[gridRow, "Qty"])))
			{
				gridBudget.Rows[gridRow].Style = (((bool)gridBudget[gridRow, "Analysis"]) ? gridBudget.Styles["BudgetChangeAnalysis"] : gridBudget.Styles["BudgetChange"]);
			}
			toolbarsManager.Enabled = true;
			toolbarsManager.EndUpdate();
		}
		dbItemA = null;
		aArr = null;
		DT_OneRow = null;
	}

	private void Reload_OneRow(int iSno, int gridRow, bool RangeUpdate)
	{
		DataView dv;
		if (!RangeUpdate)
		{
			DataTable DT_OneRow = theItemA.GetItemABySNo(projectCode, iSno).Tables[0];
			dv = new DataView(DT_OneRow);
		}
		else
		{
			dv = new DataView(dtItemA);
			dv.RowFilter = "sNo=" + iSno;
		}
		if (dv.Count != 1)
		{
			return;
		}
		if (dv[0]["analysis"].ToString().Trim() == "1")
		{
			gridBudget[gridRow, "Analysis"] = true;
			gridBudget.Rows[gridRow].Style = gridBudget.Styles["AnalysisColor"];
			CellRange rg = gridBudget.GetCellRange(gridRow, gridBudget.Cols["AnaImg"].SafeIndex);
			rg.Style = gridBudget.Styles["img"];
			rg.Image = imageList2.Images[0];
		}
		else
		{
			gridBudget[gridRow, "Analysis"] = false;
			if (gridBudget.Rows[gridRow].Style != null && (gridBudget.Rows[gridRow].Style.Name == "AnalysisColor" || gridBudget.Rows[gridRow].Style.Name == "BudgetChangeAnalysis" || gridBudget.Rows[gridRow].Style.Name == "BudgetCheckZero" || gridBudget.Rows[gridRow].Style.Name == "BudgetCheckSpace"))
			{
				gridBudget.Rows[gridRow].Style = gridBudget.Styles["Normal"];
				CellRange rg = gridBudget.GetCellRange(gridRow, gridBudget.Cols["AnaImg"].SafeIndex);
				rg.Style = gridBudget.Styles["img"];
				rg.Image = imageList2.Images[2];
			}
		}
		gridBudget[gridRow, "ItemNo"] = dv[0]["ItemNo"].ToString().Trim();
		gridBudget[gridRow, "CName"] = dv[0]["cName"].ToString().Trim();
		gridBudget[gridRow, "UnitName"] = dv[0]["unitName"].ToString().Trim();
		gridBudget[gridRow, "Qty"] = dv[0]["qty"];
		gridBudget[gridRow, "Cost"] = dv[0]["cost"];
		gridBudget[gridRow, "Amount"] = dv[0]["amount"];
		gridBudget[gridRow, "PccesCode"] = dv[0]["pccesCode"].ToString().Trim();
		gridBudget[gridRow, "Memo"] = dv[0]["memo"].ToString().Trim();
		gridBudget[gridRow, "EName"] = dv[0]["eName"].ToString().Trim();
		gridBudget[gridRow, "EUnit"] = dv[0]["eUnit"].ToString().Trim();
		gridBudget[gridRow, "LevelNo"] = dv[0]["levelNo"].ToString().Trim();
		gridBudget[gridRow, "SNo"] = dv[0]["sno"];
		gridBudget[gridRow, "Kind"] = dv[0]["kind"].ToString().Trim();
		gridBudget[gridRow, "PrintNo"] = dv[0]["printNo"].ToString().Trim();
		gridBudget[gridRow, "Formula"] = dv[0]["Formula"].ToString().Trim();
		gridBudget[gridRow, "PubCode"] = dv[0]["pubCode"].ToString().Trim();
		gridBudget[gridRow, "PrintToAnalysis"] = dv[0]["PrintToAnalysis"].ToString().Trim();
		gridBudget[gridRow, "surName"] = dv[0]["surName"].ToString().Trim();
		gridBudget[gridRow, "Costkind"] = dv[0]["costKind"].ToString().Trim();
		gridBudget[gridRow, "IsShared"] = dv[0]["share"].ToString().Trim();
		gridBudget[gridRow, "LockCost"] = ArchConvert.Obj2Bool(dv[0]["LockCost"]);
		gridBudget[gridRow, "ParentSno"] = ArchConvert.Obj2Int(dv[0]["ParentSno"]);
		gridBudget[gridRow, "SortOrder"] = ArchConvert.Obj2Int(dv[0]["SortOrder"]);
		gridBudget[gridRow, "CostDec"] = ArchConvert.Obj2Int(dv[0]["CostDec"]);
		gridBudget[gridRow, "AmtDec"] = ArchConvert.Obj2Int(dv[0]["AmtDec"]);
		if (ArchConvert.Obj2Bool(dv[0]["LockCost"]))
		{
			toolbarsManager.Tools["LockCost"].SharedProps.Enabled = false;
			toolbarsManager.Tools["UnLockCost"].SharedProps.Enabled = true;
		}
		else
		{
			toolbarsManager.Tools["LockCost"].SharedProps.Enabled = true;
			toolbarsManager.Tools["UnLockCost"].SharedProps.Enabled = false;
		}
		if (FormActionName == PccesFormAction.BUD)
		{
			gridBudget[gridRow, "QtyBeforeChange"] = dv[0]["QtyBeforeChange"];
			gridBudget[gridRow, "AmountBeforeChange"] = dv[0]["AmountBeforeChange"];
			gridBudget[gridRow, "BudgetChangeAddQty"] = ArchConvert.Obj2Decimal(dv[0]["Qty"]) - ArchConvert.Obj2Decimal(dv[0]["QtyBeforeChange"]);
			gridBudget[gridRow, "Lock"] = ArchConvert.Obj2Bool(dv[0]["Lock"]);
			gridBudget[gridRow, "IsGreenItem"] = ArchConvert.Obj2Bool(dv[0]["IsGreenItem"]);
			gridBudget[gridRow, "IsGreenMethod"] = ArchConvert.Obj2Bool(dv[0]["IsGreenMethod"]);
			gridBudget[gridRow, "IsGreenMaterial"] = ArchConvert.Obj2Bool(dv[0]["IsGreenMaterial"]);
			gridBudget[gridRow, "IsGreenEnergy"] = ArchConvert.Obj2Bool(dv[0]["IsGreenEnergy"]);
		}
		string sKind = ((dv[0]["kind"].ToString().Length > 0) ? dv[0]["kind"].ToString().ToUpper().Trim() : "");
		if (sKind == "B" || sKind == "Z" || ArchConvert.Obj2String(dv[0]["costKind"]) == "#" || dv[0]["pccesCode"].ToString().StartsWith("#"))
		{
			gridBudget.SetCellStyle(gridRow, gridBudget.Cols["CostDec"].SafeIndex, gridBudget.Styles["Transparent"]);
			gridBudget.SetCellStyle(gridRow, gridBudget.Cols["AmtDec"].SafeIndex, gridBudget.Styles["Transparent"]);
		}
		else
		{
			gridBudget.SetCellStyle(gridRow, gridBudget.Cols["CostDec"].SafeIndex, gridBudget.Styles["ComboList"]);
			gridBudget.SetCellStyle(gridRow, gridBudget.Cols["AmtDec"].SafeIndex, gridBudget.Styles["ComboList"]);
		}
		int CostDec = ArchConvert.Obj2Int(dv[0]["CostDec"]);
		int AmtDec = ArchConvert.Obj2Int(dv[0]["AmtDec"]);
		if (!gridBudget.Styles.Contains("CostDecStyle" + CostDec))
		{
			CellStyle CostDecStyle = gridBudget.Styles.Add("CostDecStyle" + CostDec);
			if (CostDec > 0)
			{
				CostDecStyle.Format = "###,###,###,##0." + "0".PadLeft(CostDec, '0');
			}
			else
			{
				CostDecStyle.Format = "###,###,###,##0";
			}
			if (gridBudget[gridRow, "PrintToAnalysis"].ToString() == "1")
			{
				CostDecStyle.BackColor = Color.LightGoldenrodYellow;
			}
		}
		if (gridBudget[gridRow, "PrintToAnalysis"].ToString() != "1")
		{
			gridBudget.SetCellStyle(gridRow, gridBudget.Cols["Cost"].SafeIndex, gridBudget.Styles["CostDecStyle" + CostDec]);
		}
		if (!gridBudget.Styles.Contains("AmtDec" + CostDec))
		{
			CellStyle AmyDecStyle = gridBudget.Styles.Add("AmtDec" + AmtDec);
			if (AmtDec > 0)
			{
				AmyDecStyle.Format = "###,###,###,##0." + "0".PadLeft(AmtDec, '0');
			}
			else
			{
				AmyDecStyle.Format = "###,###,###,##0";
			}
			if (gridBudget[gridRow, "PrintToAnalysis"].ToString() == "1")
			{
				AmyDecStyle.BackColor = Color.LightGoldenrodYellow;
			}
		}
		if (sKind == "L" || sKind == "F")
		{
			if (dv[0]["share"].ToString() == "1")
			{
				toolbarsManager.Tools["MakeAmortizedItem"].SharedProps.Enabled = false;
				toolbarsManager.Tools["CancelAmortizedItem"].SharedProps.Enabled = true;
				gridBudget.Rows[gridBudget.RowSel].Style = gridBudget.Styles["IsSharedColor"];
			}
			else
			{
				toolbarsManager.Tools["MakeAmortizedItem"].SharedProps.Enabled = true;
				toolbarsManager.Tools["CancelAmortizedItem"].SharedProps.Enabled = false;
				gridBudget.Rows[gridBudget.RowSel].Style = gridBudget.Styles["MainColor"];
			}
		}
		switch (sKind)
		{
		default:
			if (!(sKind == "U"))
			{
				break;
			}
			goto case "B";
		case "B":
		case "S":
		case "Z":
			if (gridBudget.Rows[gridRow].Style != null && gridBudget.Rows[gridRow].Style.Name != "MainColor")
			{
				gridBudget.Rows[gridRow].Style = gridBudget.Styles["MainColor"];
			}
			break;
		}
		if (SysConfig.SysChangeManagement && gridBudget.Rows[gridRow].Style != null && sKind == "W" && (gridBudget.Rows[gridRow].Style.Name == "BudgetChangeAnalysis" || gridBudget.Rows[gridRow].Style.Name == "BudgetCheckZero" || gridBudget.Rows[gridRow].Style.Name == "BudgetCheckSpace" || gridBudget.Rows[gridRow].Style.Name == "BudgetChange"))
		{
			gridBudget.Rows[gridRow].Style = gridBudget.Styles["Normal"];
		}
		if (FormActionName == PccesFormAction.BUD && sKind == "W" && budgetChangeCurrentVersion > 0 && (gridBudget[gridRow, "QtyBeforeChange"] == null || ArchConvert.Obj2Double(gridBudget[gridRow, "QtyBeforeChange"]) != ArchConvert.Obj2Double(gridBudget[gridRow, "Qty"]) || ArchConvert.Obj2Double(gridBudget[gridRow, "Amount"]) != ArchConvert.Obj2Double(gridBudget[gridRow, "AmountBeforeChange"])))
		{
			if (ArchConvert.Obj2Bool(gridBudget[gridRow, "Analysis"]))
			{
				gridBudget.Rows[gridRow].Style = gridBudget.Styles["BudgetChangeAnalysis"];
			}
			else
			{
				gridBudget.Rows[gridRow].Style = gridBudget.Styles["BudgetChange"];
			}
		}
		if (SysConfig.SysChangeManagement && sKind != "Z" && (ArchConvert.Obj2Decimal(dv[0]["qty"]) == 0m || ArchConvert.Obj2Decimal(dv[0]["cost"]) == 0m))
		{
			checkData2GridZero = false;
			gridBudget.Rows[gridRow].Style = gridBudget.Styles["BudgetCheckZero"];
		}
		if (SysConfig.SysChangeManagement && (ArchConvert.Obj2String(dv[0]["cName"]) == "" || (sKind == "W" && ArchConvert.Obj2String(dv[0]["unitName"]) == "") || (ArchConvert.Obj2String(dv[0]["ItemNo"]) == "" && sKind != "Z")))
		{
			checkData2GridSpace = false;
			gridBudget.Rows[gridRow].Style = gridBudget.Styles["BudgetCheckSpace"];
		}
	}

	private void EditItemsByKind()
	{
		bool checkFlag = false;
		ArrayList aArrTmp = new ArrayList();
		if (gridBudget[gridBudget.Row, "printNo"] != null)
		{
			ArrayList aArr = new ArrayList();
			aArr.Add(userID);
			aArr.Add("讀取是否有設攤提" + projectCode + "");
			string l_str = string.Concat("select * from ", FormActionName, "ItemA where projectCode = '", projectCode, "'  and printNo like '", gridBudget[gridBudget.Row, "printNo"], "%' and share = '1' ");
			ModifyDB StdCom = new ModifyDB(projectCode, aArr);
			DataTable ldt_mytable = StdCom.DBList(l_str);
			if (ldt_mytable.Rows.Count > 0)
			{
				l_str = string.Concat("Update ", FormActionName, "ItemA set ShareSno = '0' where projectCode = '", projectCode, "'  and printNo like '", gridBudget[gridBudget.Row, "printNo"], "%' and ShareSno is not null");
				StdCom.DBUpd(l_str);
				checkFlag = true;
			}
			StdCom = null;
			aArr = null;
		}
		bool IsChangeControl = false;
		if (budgetType == BudgetType.Types.CostEstimation || budgetType == BudgetType.Types.CostQuotationMerged)
		{
			IsChangeControl = ((gridBudget[gridBudget.Row, "CostBeforeChange"] != DBNull.Value) ? true : false);
		}
		if (gridBudget[gridBudget.Row, "Kind"] != null && gridBudget[gridBudget.Row, "Kind"].ToString() == "W")
		{
			DBClass DBCLS = new DBClass();
			DBCLS._FS_UserID = userID;
			if (!DBCLS.MrsBase_CanEdit(gridBudget[gridBudget.Row, "PubCode"].ToString().Trim(), projectCode, CommonMethods.GetActionNameString(FormActionName)))
			{
				DataRow DR = DBCLS.GetOccupieData(gridBudget[gridBudget.Row, "PubCode"].ToString().Trim(), projectCode, CommonMethods.GetActionNameString(FormActionName));
				DataTable DT_CannotDelete = new DataTable();
				DT_CannotDelete.Columns.Add("UserID", Type.GetType("System.String"));
				DT_CannotDelete.Columns.Add("UserName", Type.GetType("System.String"));
				DT_CannotDelete.Columns.Add("PccesCode", Type.GetType("System.String"));
				DT_CannotDelete.Columns.Add("CName", Type.GetType("System.String"));
				DataRow DR2 = DT_CannotDelete.NewRow();
				DR2["UserID"] = DR["UserID"];
				DR2["UserName"] = DR["UserName"];
				DR2["PccesCode"] = DR["PccesCode"];
				DR2["CName"] = DR["CName"];
				DT_CannotDelete.Rows.Add(DR2);
				FormMrsBase_DeleteMessage FM_MSG = new FormMrsBase_DeleteMessage();
				FM_MSG._MessageIcon = MessageBoxIcon.Exclamation;
				FM_MSG._iSel = 1;
				FM_MSG._DTCannotDelete = DT_CannotDelete;
				FM_MSG._Message = "這筆資料，目前有其他人正在編輯中。";
				FM_MSG.ShowDialog(this);
				FM_MSG.Close();
				FM_MSG.Dispose();
				FM_MSG = null;
				DT_CannotDelete = null;
				return;
			}
			FormMrsBaseEdit FM_EDIT = new FormMrsBaseEdit();
			FM_EDIT._UserID = userID;
			FM_EDIT._EditMode = MrsBaseEditFormType.Edit;
			FM_EDIT._CallerFormName = base.Name;
			FM_EDIT._ActionName = FormActionName;
			FM_EDIT._ProjectCode = projectCode;
			FM_EDIT._sNO = (int)gridBudget[gridBudget.Row, "SNo"];
			FM_EDIT._PubCode = (int)gridBudget[gridBudget.Row, "PubCode"];
			FM_EDIT._ItemPccesCode = gridBudget[gridBudget.Row, "pccesCode"].ToString();
			FM_EDIT._ItemcName = gridBudget[gridBudget.Row, "cName"].ToString();
			FM_EDIT._ItemUnitName = gridBudget[gridBudget.Row, "unitName"].ToString();
			FM_EDIT._IsSubmitBid = IsSubmitBid;
			bool IsLocked = ArchConvert.Obj2Bool(gridBudget[gridBudget.Row, "Lock"]);
			string costKind = DBCLS.GetMrsBaseACostKind(projectCode, gridBudget[gridBudget.Row, "pccesCode"].ToString().Trim(), CommonMethods.GetActionNameString(FormActionName));
			if (IsLocked && costKind != "")
			{
				IsLocked = false;
			}
			FM_EDIT._Istemplate = IsTemplate || NotAllowEditingInCostEst(gridBudget[gridBudget.Row, "PccesCode"].ToString()) || IsChangeControl || IsLocked;
			FM_EDIT._MainCost = ((ArchConvert.Obj2String(gridBudget[gridBudget.Row, "CostDec"]) == "") ? MainItemCostPrecison.ToString() : ArchConvert.Obj2String(gridBudget[gridBudget.Row, "CostDec"]));
			if (gridBudget[gridBudget.Row, "UnitName"].ToString().Trim() == "式" && ArchConvert.Obj2Int(gridBudget[gridBudget.Row, "Analysis"]) != 1)
			{
				BudProjMrsA theMrsA = new BudProjMrsA();
				if (theMrsA.CheckOne4WorkItemPriceCanChange(projectCode, gridBudget[gridBudget.Row, "PccesCode"].ToString()))
				{
					FM_EDIT._AllowEditCost = true;
				}
			}
			FM_EDIT._CallerFormName = "FormBudget";
			FM_EDIT._ExternalCost = Convert.ToDouble(gridBudget[gridBudget.Row, "Cost"]);
			if (DialogResult.OK == FM_EDIT.ShowDialog(this))
			{
				AfterWorkItemEdited();
				if (gridBudget[gridBudget.Row, "Qty"] == null || gridBudget[gridBudget.Row, "Qty"].ToString() == "")
				{
					gridBudget[gridBudget.Row, "Qty"] = 0;
				}
				CheckIsReCal("Y");
			}
			FM_EDIT.Close();
			FM_EDIT.Dispose();
			FM_EDIT = null;
			DBCLS = null;
		}
		else
		{
			if (gridBudget[gridBudget.Row, "Kind"] == null)
			{
				return;
			}
			DBClass DBCLS = new DBClass();
			DBCLS._FS_UserID = userID;
			if (!DBCLS.ItemA_CanEdit(gridBudget[gridBudget.Row, "SNo"].ToString().Trim(), projectCode, CommonMethods.GetActionNameString(FormActionName)))
			{
				DataRow DR = DBCLS.GetItemAOccupieData(gridBudget[gridBudget.Row, "SNo"].ToString().Trim(), projectCode, CommonMethods.GetActionNameString(FormActionName));
				DataTable DT_CannotDelete = new DataTable();
				DT_CannotDelete.Columns.Add("UserID", Type.GetType("System.String"));
				DT_CannotDelete.Columns.Add("UserName", Type.GetType("System.String"));
				DT_CannotDelete.Columns.Add("PccesCode", Type.GetType("System.String"));
				DT_CannotDelete.Columns.Add("CName", Type.GetType("System.String"));
				DataRow DR2 = DT_CannotDelete.NewRow();
				DR2["UserID"] = DR["UserID"];
				DR2["UserName"] = DR["UserName"];
				DR2["PccesCode"] = DR["PccesCode"];
				DR2["CName"] = DR["CName"];
				DT_CannotDelete.Rows.Add(DR2);
				FormMrsBase_DeleteMessage FM_MSG = new FormMrsBase_DeleteMessage();
				FM_MSG._MessageIcon = MessageBoxIcon.Exclamation;
				FM_MSG._iSel = 1;
				FM_MSG._DTCannotDelete = DT_CannotDelete;
				FM_MSG._Message = "這筆資料，目前有其他人正在編輯中。";
				FM_MSG._SrcKind = CommonMethods.GetActionNameString(FormActionName);
				FM_MSG.ShowDialog(this);
				FM_MSG.Close();
				FM_MSG.Dispose();
				FM_MSG = null;
				DT_CannotDelete = null;
				return;
			}
			try
			{
				FormBudgetEditMain FM_BDGT_EM = new FormBudgetEditMain();
				FM_BDGT_EM._UserID = userID;
				FM_BDGT_EM.ProjectCode = projectCode;
				FM_BDGT_EM._ActionName = FormActionName;
				FM_BDGT_EM.Item_sNo = (int)gridBudget[gridBudget.Row, "sNO"];
				FM_BDGT_EM.ChildCount = gridBudget.Rows[gridBudget.Row].Node.Children;
				FM_BDGT_EM.FormulaStr = ArchConvert.Obj2String(gridBudget[gridBudget.Row, "Formula"]);
				FM_BDGT_EM._Istemplate = IsTemplate || IsChangeControl;
				FM_BDGT_EM._IsCostStructure = IsCostStructureRow(gridBudget.Row, thisRowOnly: true);
				FM_BDGT_EM.ItemType = CommonMethods.GetBDGT_ItemType(gridBudget[gridBudget.Row, "Kind"].ToString());
				if (!checkFlag)
				{
					FM_BDGT_EM._ShareItems = GetShareItems(gridBudget.Row);
				}
				else
				{
					FM_BDGT_EM._ShareItems = aArrTmp;
				}
				FM_BDGT_EM._ShareItemSno = GetShareItemSNo(gridBudget[gridBudget.Row, "sNO"].ToString().Trim());
				FM_BDGT_EM._PrintToAnalysis = gridBudget[gridBudget.Row, "PrintToAnalysis"].ToString();
				FM_BDGT_EM._IsCanPrintToAnalysis = IS_CAN_PRINT_TO_ANA(gridBudget.Row);
				FM_BDGT_EM._PccesCode = ((gridBudget[gridBudget.Row, "PccesCode"] != null) ? gridBudget[gridBudget.Row, "PccesCode"].ToString() : "");
				if (SysConfig.SysComsEnable)
				{
					if (!IsChangeControl && !ArchConvert.Obj2Bool(gridBudget[gridBudget.Row, "Lock"]))
					{
						if (gridBudget[gridBudget.Row, "Kind"].ToString() == "L")
						{
							Archnowledge.Pcces.DomainModule.Coms.BudgetCtrl theCtrl = new Archnowledge.Pcces.DomainModule.Coms.BudgetCtrl();
							if (theCtrl.IsItemInSubPlanCart(projectCode, SysConfig.SysComsDB, (int)gridBudget[gridBudget.Row, "sNO"]) || ArchConvert.Obj2Bool(gridBudget[gridBudget.Row, "Lock"]))
							{
								FM_BDGT_EM._AllowRestrictEdit = true;
							}
						}
					}
					else
					{
						FM_BDGT_EM._AllowRestrictEdit = true;
					}
				}
				FM_BDGT_EM.Owner = this;
				if (FM_BDGT_EM.ShowDialog() == DialogResult.OK)
				{
					int iPos = gridBudget.Row;
					int iSno = (int)gridBudget[gridBudget.Row, "SNo"];
					dsItemA = theItemA.GetItemA(projectCode, 0);
					dtItemA = dsItemA.Tables[0];
					Reload_OneRow(iSno, iPos, RangeUpdate: false);
					CalcuParent(iPos);
					if (gridBudget[gridBudget.Row, "Kind"].ToString() == "B")
					{
						string sPrintToAnalysis = gridBudget[gridBudget.Row, "PrintToAnalysis"].ToString();
						if (sPrintToAnalysis == "1")
						{
							CellRange RgRow001 = gridBudget.GetCellRange(gridBudget.Row, 1, gridBudget.Row, gridBudget.Cols.Count - 1);
							RgRow001.Style = CSAnaPrn;
							Node LastNode = gridBudget.Rows[iPos].Node.GetNode(NodeTypeEnum.LastChild);
							int iLastIndex = LastNode.Row.SafeIndex;
							for (int k = iPos + 1; k <= iLastIndex; k++)
							{
								gridBudget[k, "PrintToAnalysis"] = sPrintToAnalysis;
								CellRange RgRow2 = gridBudget.GetCellRange(k, 1, k, gridBudget.Cols.Count - 1);
								RgRow2.Style = CSAnaPrn;
							}
						}
					}
				}
				FM_BDGT_EM.Close();
				FM_BDGT_EM.Dispose();
				FM_BDGT_EM = null;
			}
			catch (Exception ex)
			{
				if (IsDEBUG_MODE)
				{
					MessageBox.Show(this, "Err10:\nFormBudget::EditItemsByKind()#1 " + ex.Message, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			aArrTmp = null;
			DBCLS = null;
		}
	}

	private bool IS_CAN_PRINT_TO_ANA(int iRow)
	{
		bool RetV = true;
		if (gridBudget[iRow, "Kind"].ToString().Trim().ToUpper() != "B")
		{
			RetV = false;
		}
		else
		{
			Node nd = gridBudget.Rows[iRow].Node;
			Node LastNd = nd.GetNode(NodeTypeEnum.LastChild);
			if (LastNd != null)
			{
				for (int i = nd.Row.SafeIndex + 1; i <= LastNd.Row.SafeIndex; i++)
				{
					if (gridBudget[i, "Kind"].ToString().Trim().ToUpper() == "B")
					{
						RetV = false;
						break;
					}
				}
			}
		}
		return RetV;
	}

	private ArrayList GetShareItems(int iRow)
	{
		ArrayList RetV = new ArrayList();
		Node LastNode = gridBudget.Rows[iRow].Node.GetNode(NodeTypeEnum.LastChild);
		if (LastNode != null)
		{
			int iLastIndex = LastNode.Row.SafeIndex;
			for (int i = iRow; i <= iLastIndex; i++)
			{
				if (gridBudget[i, "Kind"].ToString().Trim() == "L")
				{
					string sItem = gridBudget[i, "sNO"].ToString() + "|【" + gridBudget[i, "ItemNo"].ToString().Trim() + "】" + gridBudget[i, "CName"].ToString().Trim();
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
		aArr.Add(userID);
		aArr.Add("預算書編輯--編輯主項大類(取得可攤提對項列表)");
		Archnowledge.Pcces.BUDClass.ItemA ITMA = new Archnowledge.Pcces.BUDClass.ItemA(aArr);
		ITMA.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
		DataTable DT_Shares = ITMA.GetCanShareItem(sPrintNo, projectCode);
		for (int i = 0; i < DT_Shares.Rows.Count; i++)
		{
			string sItem = DT_Shares.Rows[i]["sNo"].ToString().Trim() + "|【" + DT_Shares.Rows[i]["itemNo"].ToString().Trim() + "】" + DT_Shares.Rows[i]["cName"].ToString().Trim();
			RetV.Add(sItem);
		}
		aArr = null;
		ITMA = null;
		DT_Shares = null;
		return RetV;
	}

	private string GetShareItemSNo(string sItem_Sno)
	{
		string RetV = "";
		ArrayList aArr = new ArrayList();
		aArr.Add(userID);
		aArr.Add("取得該主項大煩的攤提項目的sNO");
		Archnowledge.Pcces.BUDClass.ItemA ITM_A = new Archnowledge.Pcces.BUDClass.ItemA(aArr);
		ITM_A.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
		ITM_A.ps_projectCode = projectCode;
		try
		{
			RetV = ITM_A.GetValue("ShareSno", sItem_Sno, projectCode);
		}
		catch (Exception ex)
		{
			MessageBox.Show("FormBudget::GetShareItemSNo()#1 Err:" + ex.Message, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		aArr = null;
		ITM_A = null;
		return RetV;
	}

	private void gridBudget1_DoubleClick(object sender, EventArgs e)
	{
		int RowIndex = gridBudget.Row;
		int ColIndex = gridBudget.Col;
		Archnowledge.Common.DebugUtil.OutputDebugString("gridBudget1_DoubleClick(" + RowIndex + "," + ColIndex + ")" + F_ModifyMode);
		if (gridBudget.Rows.Count <= 1 || IsLocked || F_ModifyMode != ModiftyMode.None)
		{
			return;
		}
		Archnowledge.Common.DebugUtil.OutputDebugString("gridBudget1_DoubleClick (" + ColIndex + "," + RowIndex + ")");
		if (!(projectCode.Trim() == "") && ColIndex > 0 && RowIndex > 0)
		{
			Row GridRow = gridBudget.Rows[RowIndex];
			string ColumnName = gridBudget.Cols[ColIndex].Name;
			string PrintNo = "";
			string Kind = "";
			if (GridRow["PrintNo"] != null && GridRow["Kind"] != null)
			{
				PrintNo = GridRow["PrintNo"].ToString().Trim();
				Kind = GridRow["Kind"].ToString().Trim();
			}
			if (PrintNo == "" || Kind == "")
			{
				Archnowledge.Common.DebugUtil.OutputDebugString("gridBudget1_DoubleClick PrintNo ='' or Kind =''");
			}
			else
			{
				EditItemsByKind();
			}
		}
	}

	private void AfterWorkItemEdited()
	{
		dsItemA = theItemA.GetItemA(projectCode, 0);
		dtItemA = dsItemA.Tables[0];
		for (int i = 1; i < gridBudget.Rows.Count; i++)
		{
			if (gridBudget[i, "PubCode"] != null && gridBudget[i, "PubCode"].ToString().Trim() == gridBudget[gridBudget.Row, "PubCode"].ToString().Trim())
			{
				Reload_OneRow(Convert.ToInt32(gridBudget[i, "Sno"]), i, RangeUpdate: true);
				CalcuParent(i);
			}
		}
	}

	private void gridBudget1_BeforeMouseDown(object sender, BeforeMouseDownEventArgs e)
	{
	}

	private void ultraButton2_Click_1(object sender, EventArgs e)
	{
		DoMenuViewProjectInfo(1);
	}

	private bool CanEditCheck(int eCol)
	{
		if (gridBudget.Row <= 0)
		{
			return false;
		}
		if (gridBudget.Col <= 0)
		{
			return false;
		}
		if (!gridBudget.Rows[gridBudget.Row].IsNode && gridBudget.Col != 1)
		{
			return false;
		}
		if (gridBudget.Col == 1 && !gridBudget.Rows[gridBudget.Row].IsNode)
		{
			gridBudget.Rows[gridBudget.Row].IsNode = true;
		}
		string sColName = gridBudget.Cols[eCol].Name.Trim().ToUpper();
		string sPrintNo = ((gridBudget[gridBudget.Row, "PrintNo"] != null && gridBudget[gridBudget.Row, "PrintNo"].ToString().Trim().Length > 0) ? gridBudget[gridBudget.Row, "PrintNo"].ToString().Trim() : "");
		string sMemo = ((gridBudget[gridBudget.Row, "Memo"] != null && gridBudget[gridBudget.Row, "Memo"].ToString().Trim().Length > 0) ? gridBudget[gridBudget.Row, "Memo"].ToString().Trim().Substring(0, 1) : "");
		string sKind = ((gridBudget[gridBudget.Row, "Kind"] != null) ? gridBudget[gridBudget.Row, "Kind"].ToString().Trim().ToUpper() : "");
		bool bAnalysis = gridBudget[gridBudget.Row, "Analysis"] != null && (bool)gridBudget[gridBudget.Row, "Analysis"];
		string sCostKind = ((gridBudget[gridBudget.Row, "Costkind"] != null) ? gridBudget[gridBudget.Row, "Costkind"].ToString().Trim().ToUpper() : "");
		string sPccesCode = ((gridBudget[gridBudget.Row, "PccesCode"] != null) ? gridBudget[gridBudget.Row, "PccesCode"].ToString().Trim().ToUpper() : "");
		bool Lock = gridBudget[gridBudget.Row, "Lock"] != null && gridBudget[gridBudget.Row, "Lock"] != DBNull.Value && Convert.ToBoolean(gridBudget[gridBudget.Row, "Lock"]);
		if (!Lock && SysConfig.SysComsEnable && SysConfig.SysEditAfterBudLem.ToUpper() == "DISABLE")
		{
			if (sKind == "W")
			{
				Archnowledge.Pcces.DomainModule.Coms.BudgetCtrl theBudgetCtrl = new Archnowledge.Pcces.DomainModule.Coms.BudgetCtrl();
				if (theBudgetCtrl.IsWorkItemInSubPlanCart(projectCode, SysConfig.SysComsDB, sPccesCode))
				{
					Lock = true;
				}
			}
			if (sKind == "L" && !AllowChangeBySNo(gridBudget[gridBudget.Row, "Sno"], silentOnWarning: true, silentOnModify: true))
			{
				Lock = true;
			}
		}
		switch (sColName)
		{
		default:
			if (!(sColName == "MEMO"))
			{
				break;
			}
			goto case "CNAME";
		case "CNAME":
		case "UNITNAME":
		case "ENAME":
		case "EUNIT":
			if (sKind == "W" && sMemo != "#")
			{
				return false;
			}
			if (Lock)
			{
				return false;
			}
			break;
		}
		if ((sColName == "COSTDEC" || sColName == "QTYDEC" || sColName == "AMTDEC") && Lock)
		{
			return false;
		}
		if (IsCostStructureRow(gridBudget.Row, thisRowOnly: true) && sColName == "CNAME")
		{
			return false;
		}
		if (sPrintNo.Trim() == "99999999999999999999999999999999")
		{
			return false;
		}
		switch (sColName)
		{
		case "PCCESCODE":
			return false;
		case "AMOUNT":
			return false;
		case "COST":
			switch (sKind)
			{
			default:
				if (!(sKind == "Z"))
				{
					break;
				}
				goto case "B";
			case "B":
			case "F":
			case "S":
			case "U":
				return false;
			}
			if (sKind == "W" && bAnalysis)
			{
				return false;
			}
			if (sCostKind == "#" || gridBudget[gridBudget.Row, "PccesCode"].ToString().StartsWith("#"))
			{
				return false;
			}
			if (sCostKind == "Z")
			{
				return false;
			}
			if (budgetType == BudgetType.Types.CostEstimation)
			{
				Lock = ((gridBudget[gridBudget.Row, "CostBeforeChange"] != DBNull.Value) ? true : false);
			}
			if (budgetType == BudgetType.Types.CostQuotationMerged)
			{
			}
			if (Lock)
			{
				string unitName = ((gridBudget[gridBudget.Row, "unitName"] != null) ? gridBudget[gridBudget.Row, "unitName"].ToString().Trim() : "");
				double Qty = ((gridBudget[gridBudget.Row, "Qty"] != null) ? ArchConvert.Obj2Double(gridBudget[gridBudget.Row, "Qty"]) : 0.0);
				if (SysConfig.SysChangeManagement)
				{
					if (Qty != 1.0 || !(unitName == "式") || bAnalysis)
					{
						return false;
					}
					return true;
				}
				return false;
			}
			if (SysConfig.SysComsEnable && budgetType == BudgetType.Types.Execution)
			{
				BudProjMrsA theMrsA = new BudProjMrsA();
				if (!theMrsA.CheckWorkItemPriceCanChange(projectCode, gridBudget[gridBudget.Row, "pccesCode"].ToString()))
				{
					return false;
				}
			}
			break;
		default:
			if (sColName.ToUpper() == "BUDGETCHANGEADDQTY")
			{
				string unitName = ((gridBudget[gridBudget.Row, "unitName"] != null) ? gridBudget[gridBudget.Row, "unitName"].ToString().Trim() : "");
				double Qty = ((gridBudget[gridBudget.Row, "Qty"] != null) ? ArchConvert.Obj2Double(gridBudget[gridBudget.Row, "Qty"]) : 0.0);
				if (Qty == 1.0 && unitName == "式" && !bAnalysis)
				{
					return false;
				}
			}
			else if (sColName == "QTY")
			{
				string unitName = ((gridBudget[gridBudget.Row, "unitName"] != null) ? gridBudget[gridBudget.Row, "unitName"].ToString().Trim() : "");
				double Qty = ((gridBudget[gridBudget.Row, "Qty"] != null) ? ArchConvert.Obj2Double(gridBudget[gridBudget.Row, "Qty"]) : 0.0);
				if (FormActionName == PccesFormAction.BUDEXE && Qty == 1.0 && unitName == "式" && !bAnalysis)
				{
					return false;
				}
			}
			break;
		}
		if (gridBudget.Row != 1 && gridBudget[gridBudget.Row - 1, "Kind"] == null)
		{
			return false;
		}
		if (FormActionName == PccesFormAction.BID && !(sColName == "COST") && !(sColName == "LOCKCOST"))
		{
			return false;
		}
		return true;
	}

	private void gridBudget1_BeforeEdit(object sender, RowColEventArgs e)
	{
		QtyBeforeEdit = ArchConvert.Obj2Decimal(gridBudget[e.Row, "qty"]);
		CostBeforeEdit = ArchConvert.Obj2Decimal(gridBudget[e.Row, "cost"]);
		AddQtyBeforeEdit = ArchConvert.Obj2Decimal(gridBudget[e.Row, "BudgetChangeAddQty"]);
		Archnowledge.Common.DebugUtil.OutputDebugString("gridBudget1_BeforeEdit (" + e.Row + "," + e.Col + ")" + FORM_STATUS);
		if (FORM_STATUS == FormStatus.Binding || gridBudget[e.Row, "PrintNo"] == null || e.Col <= 0 || e.Row <= 0)
		{
			return;
		}
		Row GridRow = gridBudget.Rows[e.Row];
		string ColumnName = gridBudget.Cols[e.Col].Name.ToUpper();
		string PrintNo = "";
		string Kind = "";
		if (GridRow["PrintNo"] != null && GridRow["Kind"] != null)
		{
			PrintNo = GridRow["PrintNo"].ToString().Trim();
			Kind = GridRow["Kind"].ToString().Trim();
		}
		if (PrintNo == "" || Kind == "")
		{
			Archnowledge.Common.DebugUtil.OutputDebugString("gridBudget1_BeforeEdit PrintNo ='' or Kind =''");
			e.Cancel = true;
			gridBudget.Col = 0;
			return;
		}
		if (ColumnName == "COST" && NotAllowEditingInCostEst(GridRow["PccesCode"].ToString()))
		{
			e.Cancel = true;
		}
		switch (ColumnName)
		{
		case "ITEMNO":
			if (!DBClass.ChkAuthority(userID, "F00300070001"))
			{
				iAuthorityMSG_Count++;
				if (iAuthorityMSG_Count <= 1)
				{
					MessageBox.Show(this, DBClass.GetFuncName("F00300070001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				iAuthorityMSG_Count = 0;
				e.Cancel = true;
				gridBudget.Col = 0;
				return;
			}
			break;
		case "CNAME":
			if (!DBClass.ChkAuthority(userID, "F00300070002"))
			{
				iAuthorityMSG_Count++;
				if (iAuthorityMSG_Count <= 1)
				{
					MessageBox.Show(this, DBClass.GetFuncName("F00300070002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				iAuthorityMSG_Count = 0;
				e.Cancel = true;
				gridBudget.Col = 0;
				return;
			}
			break;
		case "UNITNAME":
			if (!DBClass.ChkAuthority(userID, "F00300070003"))
			{
				iAuthorityMSG_Count++;
				if (iAuthorityMSG_Count <= 1)
				{
					MessageBox.Show(this, DBClass.GetFuncName("F00300070003") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				iAuthorityMSG_Count = 0;
				e.Cancel = true;
				gridBudget.Col = 0;
				return;
			}
			break;
		case "QTY":
			if (!DBClass.ChkAuthority(userID, "F00300070004"))
			{
				iAuthorityMSG_Count++;
				if (iAuthorityMSG_Count <= 1)
				{
					MessageBox.Show(this, DBClass.GetFuncName("F00300070004") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				iAuthorityMSG_Count = 0;
				e.Cancel = true;
				gridBudget.Col = 0;
				return;
			}
			break;
		case "COST":
			if (!DBClass.ChkAuthority(userID, "F00300070005"))
			{
				iAuthorityMSG_Count++;
				if (iAuthorityMSG_Count <= 1)
				{
					MessageBox.Show(this, DBClass.GetFuncName("F00300070005") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				iAuthorityMSG_Count = 0;
				e.Cancel = true;
				gridBudget.Col = 0;
				return;
			}
			break;
		case "MEMO":
			if (!DBClass.ChkAuthority(userID, "F00300070006"))
			{
				iAuthorityMSG_Count++;
				if (iAuthorityMSG_Count <= 1)
				{
					MessageBox.Show(this, DBClass.GetFuncName("F00300070006") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				iAuthorityMSG_Count = 0;
				e.Cancel = true;
				gridBudget.Col = 0;
				return;
			}
			break;
		case "ENAME":
			if (!DBClass.ChkAuthority(userID, "F00300070007"))
			{
				iAuthorityMSG_Count++;
				if (iAuthorityMSG_Count <= 1)
				{
					MessageBox.Show(this, DBClass.GetFuncName("F00300070007") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				iAuthorityMSG_Count = 0;
				e.Cancel = true;
				gridBudget.Col = 0;
				return;
			}
			break;
		case "EUNIT":
			if (!DBClass.ChkAuthority(userID, "F00300070008"))
			{
				iAuthorityMSG_Count++;
				if (iAuthorityMSG_Count <= 1)
				{
					MessageBox.Show(this, DBClass.GetFuncName("F00300070008") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				iAuthorityMSG_Count = 0;
				e.Cancel = true;
				gridBudget.Col = 0;
				return;
			}
			break;
		}
		if (!CanEditCheck(e.Col))
		{
			toolbarsManager.Enabled = true;
			e.Cancel = true;
			return;
		}
		try
		{
			iAuthorityMSG_Count++;
			if (iAuthorityMSG_Count <= 1)
			{
				DBClass DBCLS = new DBClass();
				DBCLS._FS_UserID = userID;
				if (Kind.ToUpper() == "W" && !DBCLS.MrsBase_CanEdit(GridRow["PubCode"].ToString().Trim(), projectCode, CommonMethods.GetActionNameString(FormActionName)))
				{
					DataRow DR = DBCLS.GetOccupieData(GridRow["PubCode"].ToString().Trim(), projectCode, CommonMethods.GetActionNameString(FormActionName));
					DataTable DT_CannotDelete = new DataTable();
					DT_CannotDelete.Columns.Add("UserID", Type.GetType("System.String"));
					DT_CannotDelete.Columns.Add("UserName", Type.GetType("System.String"));
					DT_CannotDelete.Columns.Add("PccesCode", Type.GetType("System.String"));
					DT_CannotDelete.Columns.Add("CName", Type.GetType("System.String"));
					DataRow DR2 = DT_CannotDelete.NewRow();
					DR2["UserID"] = DR["UserID"];
					DR2["UserName"] = DR["UserName"];
					DR2["PccesCode"] = DR["PccesCode"];
					DR2["CName"] = DR["CName"];
					DT_CannotDelete.Rows.Add(DR2);
					FormMrsBase_DeleteMessage FM_MSG = new FormMrsBase_DeleteMessage();
					FM_MSG._MessageIcon = MessageBoxIcon.Exclamation;
					FM_MSG._iSel = 1;
					FM_MSG._DTCannotDelete = DT_CannotDelete;
					FM_MSG._Message = "這筆資料，目前有其他人正在編輯中。";
					FM_MSG.ShowDialog(this);
					FM_MSG.Close();
					FM_MSG.Dispose();
					FM_MSG = null;
					DT_CannotDelete = null;
					e.Cancel = true;
					gridBudget.Col = 0;
					return;
				}
				if (Kind.ToUpper() != "W" && !DBCLS.ItemA_CanEdit(GridRow["SNo"].ToString().Trim(), projectCode, CommonMethods.GetActionNameString(FormActionName)))
				{
					DataRow DR = DBCLS.GetItemAOccupieData(GridRow["SNo"].ToString().Trim(), projectCode, CommonMethods.GetActionNameString(FormActionName));
					DataTable DT_CannotDelete = new DataTable();
					DT_CannotDelete.Columns.Add("UserID", Type.GetType("System.String"));
					DT_CannotDelete.Columns.Add("UserName", Type.GetType("System.String"));
					DT_CannotDelete.Columns.Add("PccesCode", Type.GetType("System.String"));
					DT_CannotDelete.Columns.Add("CName", Type.GetType("System.String"));
					DataRow DR2 = DT_CannotDelete.NewRow();
					DR2["UserID"] = DR["UserID"];
					DR2["UserName"] = DR["UserName"];
					DR2["PccesCode"] = DR["PccesCode"];
					DR2["CName"] = DR["CName"];
					DT_CannotDelete.Rows.Add(DR2);
					FormMrsBase_DeleteMessage FM_MSG = new FormMrsBase_DeleteMessage();
					FM_MSG._MessageIcon = MessageBoxIcon.Exclamation;
					FM_MSG._iSel = 1;
					FM_MSG._DTCannotDelete = DT_CannotDelete;
					FM_MSG._SrcKind = CommonMethods.GetActionNameString(FormActionName);
					FM_MSG._Message = "這筆資料，目前有其他人正在編輯中。";
					FM_MSG.ShowDialog(this);
					FM_MSG.Close();
					FM_MSG.Dispose();
					FM_MSG = null;
					DT_CannotDelete = null;
					e.Cancel = true;
					gridBudget.Col = 0;
					return;
				}
				DBCLS = null;
				iAuthorityMSG_Count = 0;
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudget.cs--gridBudget1_BeforeEdit" + ex.Message);
		}
		string IschkBDGT = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "BDGT", "NoMessage");
		if (IschkBDGT.ToUpper() != "TRUE" && ColumnName == "COST" && Kind != "L" && Kind != "W")
		{
			string l_Message = "此為主項大類是由子項加總，不可編輯單價!!";
			switch (Kind)
			{
			case "B":
				l_Message = "此為主項大類是由子項加總，不可編輯單價!!";
				break;
			case "F":
				l_Message = "此為公式計價項，不可直接編輯單價!!";
				break;
			case "S":
				l_Message = "此為分段計價項，不可直接編輯單價!!";
				break;
			case "Z":
				l_Message = "此為計項，不可直接編輯單價!!";
				break;
			}
			ifCount++;
			if (ifCount <= 1)
			{
				MessageBox.Show(this, l_Message, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			ifCount = 0;
			e.Cancel = true;
			gridBudget.Col = 0;
		}
		else
		{
			if (!(ColumnName.ToUpper() == "PWRSET"))
			{
				return;
			}
			bool rowIsOne4Item = ArchConvert.Obj2String(GridRow["UnitName"]) == "式" && ArchConvert.Obj2Decimal(GridRow["Qty"]) == 1m && !ArchConvert.Obj2Bool(GridRow["Analysis"]);
			if (LastRowIsOne4Item == rowIsOne4Item)
			{
				return;
			}
			CellStyle csCbPS = gridBudget.Styles["ComboListPS"];
			string comboList = string.Empty;
			foreach (DataRow dr in dsPwrSet.Tables["PwrSet"].Rows)
			{
				if (ArchConvert.Obj2String(GridRow["UnitName"]) != "式" || ArchConvert.Obj2Decimal(GridRow["Qty"]) != 1m || ArchConvert.Obj2Bool(GridRow["Analysis"]) || ArchConvert.Obj2Int(dr["PwrCode"]) != 3)
				{
					comboList = comboList + ArchConvert.Obj2String(dr["PwrName"]) + "|";
				}
			}
			csCbPS.ComboList = comboList;
			LastRowIsOne4Item = rowIsOne4Item;
		}
	}

	private bool NotAllowEditingInCostEst(string pccesCode)
	{
		return budgetType == BudgetType.Types.CostEstimation && theProjMrsA.WorkItemExists(parentProjectCode, pccesCode);
	}

	private void SetColsEditSymbol()
	{
		for (int i = 1; i < gridBudget.Cols.Count; i++)
		{
			if (gridBudget.Cols[i].AllowEditing)
			{
				CellRange rg = gridBudget.GetCellRange(0, i);
				rg.Style = gridBudget.Styles["EditMode"];
				rg.Image = imageList2.Images[1];
			}
		}
	}

	private void ultraToolbarsManager1_AfterToolDeactivate(object sender, ToolEventArgs e)
	{
		if (toolbarsManager != null)
		{
			((ButtonTool)toolbarsManager.Tools["Delete"]).SharedProps.Shortcut = Shortcut.Del;
		}
	}

	private void ultraToolbarsManager1_AfterToolActivate(object sender, ToolEventArgs e)
	{
		switch (e.Tool.Key)
		{
		case "GetSubItemQtyAmt":
			SetUpCboSubItemQtyAmt();
			break;
		case "ListItemChangeHistory":
			SetUpCboItemChangeHistory();
			break;
		case "KeywordList":
			((ButtonTool)toolbarsManager.Tools["Delete"]).SharedProps.Shortcut = Shortcut.None;
			break;
		default:
			((ButtonTool)toolbarsManager.Tools["Delete"]).SharedProps.Shortcut = Shortcut.Del;
			break;
		}
	}

	private void SetUpCboSubItemQtyAmt()
	{
		if (gridBudget[gridBudget.Row, "pccesCode"] != null)
		{
			string PccesCode = gridBudget[gridBudget.Row, "pccesCode"].ToString().Trim();
			ComsWebService theComsWebService = new ComsWebService(projectCode);
			theComsWebService.SetUpCboSubItemQtyAmt(cboSubItemQtyAmt, PccesCode);
		}
	}

	private void SetUpCboItemChangeHistory()
	{
		if (gridBudget[gridBudget.Row, "sNo"] == null)
		{
			return;
		}
		string sNo = gridBudget[gridBudget.Row, "sNo"].ToString().Trim();
		string unitName = gridBudget[gridBudget.Row, "UnitName"].ToString().Trim();
		decimal Qty = ArchConvert.Obj2Decimal(gridBudget[gridBudget.Row, "Qty"]);
		bool Analysis = ArchConvert.Obj2Bool(gridBudget[gridBudget.Row, "Analysis"]);
		BudExeItemA budExeItemA = new BudExeItemA();
		DataSet dsBudExeItemAHistory = budExeItemA.GetItemAHistory(projectCode, sNo);
		if (dsBudExeItemAHistory.Tables.Count > 0)
		{
			cboItemChangeHistory.Text = "請下拉，參考工項歷次變更記錄";
			cboItemChangeHistory.DataSource = dsBudExeItemAHistory.Tables[0];
			cboItemChangeHistory.DataBind();
			cboItemChangeHistory.DisplayLayout.Bands[0].Columns[0].Header.Caption = "版次";
			if (unitName == "式" && Qty == 1m && !Analysis)
			{
				cboItemChangeHistory.DisplayLayout.Bands[0].Columns[1].Header.Caption = "金額";
				cboItemChangeHistory.DisplayLayout.Bands[0].Columns[2].Header.Caption = "專案總金額";
			}
			else
			{
				cboItemChangeHistory.DisplayLayout.Bands[0].Columns[1].Header.Caption = "數量";
				cboItemChangeHistory.DisplayLayout.Bands[0].Columns[2].Header.Caption = "專案總數量";
			}
			cboItemChangeHistory.DisplayLayout.Bands[0].Columns[0].CellAppearance.TextHAlign = HAlign.Center;
			cboItemChangeHistory.DisplayLayout.Bands[0].Columns[1].CellAppearance.TextHAlign = HAlign.Right;
			cboItemChangeHistory.DisplayLayout.Bands[0].Columns[2].CellAppearance.TextHAlign = HAlign.Right;
			cboItemChangeHistory.DisplayLayout.Bands[0].Columns[0].Format = "N0";
			cboItemChangeHistory.DisplayLayout.Bands[0].Columns[1].Format = "N" + MainItemQtyPrecision;
			cboItemChangeHistory.DisplayLayout.Bands[0].Columns[2].Format = "N" + MainItemQtyPrecision;
			cboItemChangeHistory.Visible = true;
		}
	}

	private void InitialToolbars()
	{
		int RowIndex = gridBudget.Row;
		Row GridRow = gridBudget.Rows[RowIndex];
		if (!UseCostStructure)
		{
			return;
		}
		try
		{
			string Kind = "";
			if (GridRow["Kind"] != null)
			{
				Kind = GridRow["Kind"].ToString().Trim();
			}
			string PrintNo = "";
			if (GridRow["PrintNo"] != null)
			{
				PrintNo = GridRow["PrintNo"].ToString();
			}
			bool enableCostStructure = false;
			bool enableBType = true;
			toolbarsManager.Tools["EditCostStructureProperty"].SharedProps.Enabled = false;
			if (GridRow["CostUID"] != null && GridRow["CostUID"].ToString() != "")
			{
				enableCostStructure = true;
				enableBType = false;
				toolbarsManager.Tools["EditCostStructureProperty"].SharedProps.Enabled = true;
			}
			else if (GridRow["Kind"] != null && GridRow["Kind"].ToString().Trim() == "B")
			{
				enableCostStructure = true;
			}
			else if (PrintNo != "")
			{
				Node ParentNode = GridRow.Node.GetNode(NodeTypeEnum.Parent);
				if (ParentNode != null && ParentNode.Row["CostUID"] != null && ParentNode.Row["CostUID"].ToString() != "")
				{
					enableBType = false;
				}
			}
			toolbarsManager.Tools["InsertWorkItemPickFromCostStructure"].SharedProps.Enabled = enableCostStructure;
			toolbarsManager.Tools["InsertMainItemSibling"].SharedProps.Enabled = enableBType;
			if (Kind != "B")
			{
				toolbarsManager.Tools["InsertMainItemChildren"].SharedProps.Enabled = false;
			}
			else if (PrintNo.Length / 4 < 8)
			{
				toolbarsManager.Tools["InsertMainItemChildren"].SharedProps.Enabled = enableBType;
			}
			if (gridBudget[1, "sNo"] == null)
			{
				toolbarsManager.Tools["InsertWorkItemPickFromCostStructure"].SharedProps.Enabled = true;
				toolbarsManager.Tools["InsertMainItemSibling"].SharedProps.Enabled = true;
				toolbarsManager.Tools["InsertMainItemChildren"].SharedProps.Enabled = true;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("FormBudget::InitialToolbars()#1 InitialToolbars Error : " + ex.Message, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void gridBudget1_Click(object sender, EventArgs e)
	{
		int RowIndex = gridBudget.Row;
		int ColIndex = gridBudget.Col;
		Archnowledge.Common.DebugUtil.OutputDebugString("gridBudget1_Click (" + RowIndex + "," + ColIndex + ")");
		if (projectCode.Trim() == "")
		{
			return;
		}
		if (ColIndex <= 0 || RowIndex <= 0)
		{
			EnableContextMenu = false;
			return;
		}
		Row GridRow = gridBudget.Rows[RowIndex];
		string ColumnName = gridBudget.Cols[ColIndex].Name;
		if (IsLocked && ColumnName != "AnaImg")
		{
			return;
		}
		string PrintNo = "";
		string Kind = "";
		if (GridRow["PrintNo"] != null && GridRow["Kind"] != null)
		{
			PrintNo = GridRow["PrintNo"].ToString().Trim();
			Kind = GridRow["Kind"].ToString().Trim();
		}
		if (!IsTemplate)
		{
			EnableContextMenu = true;
		}
		if (PrintNo == "" || Kind == "")
		{
			Archnowledge.Common.DebugUtil.OutputDebugString("gridBudget1_Click PrintNo ='' or Kind =''");
			return;
		}
		cboHisPrice.Visible = false;
		if (GridRow["Analysis"] != null && ArchConvert.Obj2Bool(GridRow["Analysis"]) && ColumnName.ToUpper() == "AnaImg".ToUpper())
		{
			if (HasOpenedBreakdownForm)
			{
				return;
			}
			HasOpenedBreakdownForm = true;
			ExecuteBreakdownForm();
			gridBudget.AfterSelChange -= gridBudget1_AfterSelChange;
			gridBudget.Col = 0;
			gridBudget.AfterSelChange += gridBudget1_AfterSelChange;
		}
		if (ColumnName == "PccesCode")
		{
			string PccesCode = string.Empty;
			if (GridRow["PccesCode"] != null)
			{
				PccesCode = GridRow["PccesCode"].ToString().Trim();
			}
			AddOnDownLoad addOnDownLoad = new AddOnDownLoad();
			addOnDownLoad.OpenDocument(PccesCode, userID, projectCode);
		}
	}

	private void ultraToolbarsManager1_ToolKeyPress(object sender, ToolKeyPressEventArgs e)
	{
		if (e.KeyChar == '\r' && e.Tool.Key == "KeywordList")
		{
			Do_ToolBarFind();
		}
	}

	private void ultraToolbarsManager1_BeforeToolbarListDropdown(object sender, BeforeToolbarListDropdownEventArgs e)
	{
		e.Cancel = true;
	}

	private void gridBudget1_AfterCollapse(object sender, RowColEventArgs e)
	{
		try
		{
			if (e.Row >= 0 && e.Row < gridBudget.Rows.Count)
			{
				gridBudget[e.Row, "IsCollaspse"] = gridBudget.Rows[e.Row].Node.Collapsed;
			}
		}
		catch
		{
		}
	}

	private void AddOnClick(object sender, ToolClickEventArgs e)
	{
		int iMenuIndex = (int)e.Tool.SharedProps.Tag;
		string sCmd = ToolParam[iMenuIndex].ToString();
		if (!(sCmd.Substring(0, 1) == "[") || !(sCmd.Substring(sCmd.Length - 1, 1) == "]"))
		{
			SysUser oSysUser = new SysUser();
			string CurrentDBName = oSysUser.GetSysUserDatabaseName(userID);
			if (sCmd.IndexOf("%PJ") > -1)
			{
				sCmd = sCmd.Replace("%PJ", projectCode);
			}
			if (sCmd.IndexOf("%DB") > -1)
			{
				sCmd = sCmd.Replace("%DB", CurrentDBName);
			}
			if (sCmd.IndexOf("%UID") > -1)
			{
				sCmd = sCmd.Replace("%UID", userID);
			}
			string sPath = ((sCmd.IndexOf(" ") > -1) ? sCmd.Substring(0, sCmd.IndexOf(" ")) : sCmd);
			string sParameters = ((sCmd.IndexOf(" ") > -1) ? sCmd.Substring(sCmd.IndexOf(" ")) : "");
			ShellExecute SHExe = new ShellExecute();
			SHExe.OwnerHandle = base.Handle;
			SHExe.Path = sPath;
			SHExe.Parameters = sParameters;
			SHExe.Execute();
			SHExe = null;
		}
	}

	private void ReStoreClick(object sender, ToolClickEventArgs e)
	{
		int iMenuIndex = (int)e.Tool.SharedProps.Tag;
		string SrcKind = CommonMethods.GetActionNameString(FormActionName);
		string sTYPE = "預算";
		if (SrcKind.ToUpper() == "BUD")
		{
			sTYPE = "預算";
			if (GetCurrentBDGT_Type().ToUpper() == "CNT")
			{
				sTYPE = "契約";
			}
		}
		else if (SrcKind.ToUpper() == "BID")
		{
			sTYPE = "標單";
		}
		string sMess = "是否要先備份目前編輯中這份 [" + sTYPE + "] 資料？";
		DialogResult result = MessageBox.Show(this, sMess, "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
		if (result == DialogResult.Yes)
		{
			if (FormActionName == PccesFormAction.BUD)
			{
				if (GetCurrentBDGT_Type() == "CNT")
				{
					ExecuteCopyToTmpCNT("");
					SetupRestoreSnapshotListCNT();
				}
				else
				{
					ExecuteCopyToTmp("");
					SetupRestoreSnapshotList();
				}
			}
			else
			{
				ExecuteCopyToTmp("");
				SetupRestoreSnapshotList();
			}
		}
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(userID);
		aArr.Add("取出tmp專案" + projectCode);
		Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
		PROJ.ps_projectCode = projectCode;
		PROJ.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
		PROJ.DeleProjGetTmp(projectCode, iMenuIndex.ToString());
		LoadProjectData();
		aArr = null;
		PROJ = null;
		SetCurrentBDGT_Type(SrcKind.ToUpper());
		GetCurrentBDGT_Type();
		InitBudgetChange();
		MessageBox.Show(this, "回存完成!!", "詢問", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	private void RestoreCNTDirectly()
	{
		Application.DoEvents();
		string SrcKind = CommonMethods.GetActionNameString(FormActionName);
		string sTYPE = "契約";
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(userID);
		aArr.Add("取出tmp專案" + projectCode);
		Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
		ModifyDB StdCom = new ModifyDB(ProjectCode, aArr);
		string sSQL = "select IsNull(Max(version), 50000) as version from tmpProject where projectCode = '" + projectCode + "' and (memo is not null and memo <> '')";
		string iMenuIndex = StdCom.DBGetValue(sSQL);
		PROJ.ps_projectCode = projectCode;
		PROJ.ps_srckind = "CNT";
		PROJ.DeleProjGetTmp(projectCode, iMenuIndex.ToString());
		LoadProjectData();
		aArr = null;
		PROJ = null;
		SetCurrentBDGT_Type("CNT");
		GetCurrentBDGT_Type();
		InitBudgetChange();
	}

	private void ReStoreClickCNT(object sender, ToolClickEventArgs e)
	{
		int iMenuIndex = (int)e.Tool.SharedProps.Tag;
		string SrcKind = CommonMethods.GetActionNameString(FormActionName);
		string sTYPE = "契約";
		if (GetCurrentBDGT_Type().ToUpper() == "BUD")
		{
			sTYPE = "預算";
		}
		string sMess = "是否要先備份目前編輯中這份 [" + sTYPE + "] 資料？";
		DialogResult result = MessageBox.Show(this, sMess, "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
		if (result == DialogResult.Yes)
		{
			if (FormActionName == PccesFormAction.BUD)
			{
				if (GetCurrentBDGT_Type() == "CNT")
				{
					ExecuteCopyToTmpCNT("");
					SetupRestoreSnapshotListCNT();
				}
				else
				{
					ExecuteCopyToTmp("");
					SetupRestoreSnapshotList();
				}
			}
			else
			{
				ExecuteCopyToTmp("");
				SetupRestoreSnapshotList();
			}
		}
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(userID);
		aArr.Add("取出tmp專案" + projectCode);
		Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
		PROJ.ps_projectCode = projectCode;
		PROJ.ps_srckind = "CNT";
		PROJ.DeleProjGetTmp(projectCode, iMenuIndex.ToString());
		LoadProjectData();
		aArr = null;
		PROJ = null;
		SetCurrentBDGT_Type("CNT");
		GetCurrentBDGT_Type();
		InitBudgetChange();
		MessageBox.Show(this, "回存完成!!", "詢問", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	private void TM_BDGT_AutoSave_Tick(object sender, EventArgs e)
	{
		AutoSaveProject();
		Cursor = Cursors.Default;
	}

	private void AutoSaveProject()
	{
		if (!backgroundWorker.IsBusy)
		{
			backgroundWorker.RunWorkerAsync();
		}
	}

	private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
	{
		BackgroundWorker worker = sender as BackgroundWorker;
		e.Result = OutputBackupXML();
	}

	private bool OutputBackupXML()
	{
		string sXMLKind = "1";
		string ps_ShowCost = "";
		string sFolder = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "BDGT", "BackupPath");
		if (sFolder == "")
		{
			sFolder = AppLocation + "Backup\\";
		}
		ArrayList aArr = new ArrayList();
		aArr.Add(userID);
		aArr.Add("預算書 XML 轉出");
		Archnowledge.Pcces.BUDClass.Project projcom = new Archnowledge.Pcces.BUDClass.Project(aArr);
		projcom.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
		projcom.ps_ShowCost = "1";
		projcom.ps_ShowAnalysis = "1";
		sXMLKind = ((FormActionName != PccesFormAction.BUD) ? "2" : "1");
		ps_ShowCost = projcom.ps_ShowCost;
		projcom.ps_ShowAnalysis = "1";
		bool IsOutAnalysis = true;
		DataSet lds_temp = projcom.OutputXML(projectCode, "XM1");
		projcom = null;
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = userID;
		DataTable DT_PGBK = DBCLS.GetUserDefine("Select SNo,IsPageBreak from " + CommonMethods.GetActionNameString(FormActionName) + "PageBreak Where ProjectCode='" + projectCode + "' ");
		for (int z = 0; z < DT_PGBK.Rows.Count; z++)
		{
			if (DT_PGBK.Rows[z]["IsPageBreak"].ToString() == "Y")
			{
				int idx = GetDTDetailRowIndex(lds_temp.Tables["Items"], (int)DT_PGBK.Rows[z]["SNo"]);
				if (idx > -1)
				{
					DataRow dataRow;
					(dataRow = lds_temp.Tables["Items"].Rows[idx])["memo"] = string.Concat(dataRow["memo"], "[跳頁]");
				}
			}
		}
		DBCLS = new DBClass();
		DBCLS._FS_UserID = userID;
		DT_PGBK = DBCLS.GetUserDefine("Select SNo,PrintToAnalysis from " + CommonMethods.GetActionNameString(FormActionName) + "ItemA Where ProjectCode='" + projectCode + "' ");
		for (int z = 0; z < DT_PGBK.Rows.Count; z++)
		{
			if (DT_PGBK.Rows[z]["PrintToAnalysis"].ToString() == "1")
			{
				int idx = GetDTDetailRowIndex(lds_temp.Tables["Items"], (int)DT_PGBK.Rows[z]["SNo"]);
				if (idx > -1)
				{
					DataRow dataRow;
					(dataRow = lds_temp.Tables["Items"].Rows[idx])["memo"] = string.Concat(dataRow["memo"], "[印單]");
				}
			}
		}
		ChgXMLStru XMLCom = new ChgXMLStru();
		XMLCom._CheckoutFlag = "CKOut";
		if (!Directory.Exists(sFolder))
		{
			Directory.CreateDirectory(sFolder);
		}
		if (FormActionName == PccesFormAction.BUD)
		{
			XMLCom.OutputXML1(lds_temp, sFolder + "BUD_AUTOBAK_" + projectCode + ".PccesBak", outItem: true, IsOutAnalysis, outResource: true, sXMLKind, ps_ShowCost, FormActionName.ToString());
		}
		else
		{
			XMLCom.OutputXML1(lds_temp, sFolder + "BID_AUTOBAK_" + projectCode + ".PccesBak", outItem: true, IsOutAnalysis, outResource: true, sXMLKind, ps_ShowCost, FormActionName.ToString());
		}
		XMLCom = null;
		DBCLS = null;
		DT_PGBK = null;
		PubTools.WriteRoughlyLog(aArr);
		aArr = null;
		return true;
	}

	private int GetDTDetailRowIndex(DataTable DT_New, int iSNo)
	{
		int RetV = -1;
		for (int i = 0; i < DT_New.Rows.Count; i++)
		{
			if (PubTools.Str2Int(DT_New.Rows[i]["sNo"]) == iSNo)
			{
				RetV = i;
				break;
			}
		}
		return RetV;
	}

	private void ultraStatusBar1_PanelClick(object sender, PanelClickEventArgs e)
	{
		if (e.Panel.Index == 2)
		{
			ShellExecute SHExe = new ShellExecute();
			SHExe.OwnerHandle = base.Handle;
			SHExe.Path = "http://pcces.archnowledge.com/pccesfaq/";
			SHExe.Execute();
			SHExe = null;
		}
	}

	private void LockOrUnlockToolbar(bool Locked)
	{
		string[] ButtonList = new string[41]
		{
			"FileMenu", "EditMenu", "ViewMenu", "DetailEditMenu", "AddOn", "Recalculate", "AutoRecalculate", "AdjustTotalAmount", "ReArrangeItemNo", "ImportMrsBaseItemName",
			"ImportAllMrsBaseItemCost", "ImportAllMrsBaseCostBreakdown", "SetPrecision", "EditItemNoSetting", "BackupProject", "ImportMrsBaseItemCost", "ImportMrsBaseCostBreakdown", "AutoInsertSubtotalItem", "ClearDetailListCost", "ReconstructConnectionWithMrsBase",
			"TakeSnapshot", "RestoreSnapshot", "LoadTemplate", "EditProjectOption", "ManageSnapshot", "ImportQtyFrom3rdPartyTool", "RestoreProject", "ExportDetailList", "Cut", "Copy",
			"LockProject", "DeleteBudgetChangeVersion", "Outdent", "Indent", "MoveUp", "MoveDown", "EditWorkItem", "EditMainItem", "TakeSnapshotCnt", "popupRestoreDbgt",
			"TakeSnapshotCntFromBid"
		};
		SetButtonListAvailibility(ButtonList, !Locked);
		if (!SysConfig.SysChangeManagement)
		{
			toolbarsManager.Tools["mnuPopFile2"].SharedProps.Visible = Locked;
			toolbarsManager.Tools["FileMenu"].SharedProps.Visible = !Locked;
			if (GetCurrentBDGT_Type().ToUpper() == "CNT")
			{
				toolbarsManager.Tools["Exit"].SharedProps.Caption = "結束契約書編輯(&X)";
			}
			else
			{
				toolbarsManager.Tools["Exit"].SharedProps.Caption = "結束預算書編輯(&X)";
			}
		}
		if (!ReadOnlyMode)
		{
			toolbarsManager.Tools["UnlockProject"].SharedProps.Enabled = Locked;
		}
		else
		{
			toolbarsManager.Tools["UnlockProject"].SharedProps.Enabled = false;
		}
		for (int index = 0; index < toolbarsManager.Toolbars["ItemAction"].Tools.Count; index++)
		{
			toolbarsManager.Toolbars["ItemAction"].Tools[index].SharedProps.Enabled = !Locked;
		}
		DisableExeBudgetFunc();
		if (SysConfig.SysChangeManagement)
		{
			toolbarsManager.Tools["ShowOnlyChangedItems"].SharedProps.Enabled = true;
			toolbarsManager.Tools["mnuHideAmtZero"].SharedProps.Enabled = true;
		}
		if (SysConfig.SysChangeManagement)
		{
			toolbarsManager.Tools["FileMenu"].SharedProps.Enabled = true;
			toolbarsManager.Tools["DeleteThisProject"].SharedProps.Enabled = false;
			toolbarsManager.Tools["CombineBid"].SharedProps.Enabled = false;
			toolbarsManager.Tools["CombineBudget"].SharedProps.Enabled = false;
			toolbarsManager.Tools["EditAliasSettingForReport"].SharedProps.Enabled = false;
			toolbarsManager.Tools["EditBidSetting"].SharedProps.Enabled = false;
		}
		ultraButton2.Enabled = !Locked;
		foreach (Column c in (IEnumerable)gridBudget.Cols)
		{
			if (c.Name != "PwrSet")
			{
				c.AllowEditing = !Locked;
			}
		}
		EnableContextMenu = !Locked;
		IsLocked = Locked;
	}

	private void frmBudget_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Control && e.KeyCode == Keys.F1)
		{
			Frm.Show();
			Frm.BringToFront();
		}
		if (e.Control && e.KeyCode == Keys.F12)
		{
			Th_ReCal_All(Auto: false);
		}
		if (e.Control && e.KeyCode == Keys.O)
		{
			Execute_OptionMain();
		}
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
			iTextBeamPos = (Cntrl1 as TextBox).SelectionStart;
			if ((Cntrl1 as TextBox).SelectedText.Length > 1)
			{
				(Cntrl1 as TextBox).Text = (Cntrl1 as TextBox).Text.Replace((Cntrl1 as TextBox).SelectedText, ssString);
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
			CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudget.cs--DispatchString" + ex.Message);
			Console.Write(ex.Message);
			if (IsDEBUG_MODE)
			{
				MessageBox.Show(this, "Err15:\nFormBudget::DispatchString()#1 " + ex.Message, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
	}

	private void CalcuParent(int iRow)
	{
		try
		{
			gridBudget[iRow, "Amount"] = PubTools.Str2Double(gridBudget[iRow, "Qty"]) * PubTools.ARound(PubTools.Str2Double(gridBudget[iRow, "Cost"]), PubTools.Str2Int(gridBudget[iRow, "CostDec"]));
			Node CurrentNode = gridBudget.Rows[iRow].Node;
			Node ParentNode = CurrentNode.GetNode(NodeTypeEnum.Parent);
			int iPaLastChild = ParentNode.GetNode(NodeTypeEnum.LastChild).Row.SafeIndex;
			decimal dPaAmount = 0m;
			for (int i = ParentNode.Row.SafeIndex + 1; i <= iPaLastChild; i++)
			{
				if (gridBudget.Rows[i].Node.Level == ParentNode.Level + 1 && gridBudget[i, "Kind"].ToString() != "Z")
				{
					dPaAmount += PubTools.Str2Decimal(gridBudget[i, "Amount"]);
				}
			}
			ParentNode.Row["Amount"] = dPaAmount;
			ParentNode.Row["Cost"] = dPaAmount / PubTools.Str2Decimal(ParentNode.Row["Qty"]);
			if (ParentNode.GetNode(NodeTypeEnum.Parent) != null)
			{
				CalcuParent(ParentNode.Row.SafeIndex);
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudget.cs--CalcuParent" + ex.Message);
		}
	}

	private void ultraToolbarsManager1_AfterToolCloseup(object sender, ToolDropdownEventArgs e)
	{
		int iCol = -1;
		int iFind = -1;
		string SearchText = "";
		if (!(e.Tool.Key == "BookmarkList"))
		{
			return;
		}
		iCol = gridBudget.Cols["SNo"].SafeIndex;
		if (((ComboBoxTool)toolbarsManager.Tools["BookmarkList"]).Value != null)
		{
			SearchText = ((ComboBoxTool)toolbarsManager.Tools["BookmarkList"]).Value.ToString().Substring(0, 20).Trim();
			iFind = gridBudget.FindRow(SearchText, 1, iCol, caseSensitive: false, fullMatch: false, wrap: false);
			if (iFind > -1)
			{
				gridBudget.AfterSelChange -= gridBudget1_AfterSelChange;
				gridBudget.Row = gridBudget.Rows.Count - 1;
				gridBudget.Row = iFind;
				gridBudget.AfterSelChange += gridBudget1_AfterSelChange;
			}
			gridBudget.Select();
		}
	}

	public DataTable FixPubCode()
	{
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = userID;
		string sSQL = "Select PccesCode, PubCode From " + CommonMethods.GetActionNameString(FormActionName) + "ProjMrsA Where ProjectCode='" + projectCode + "' ";
		DataTable DT_Process = DBCLS.GetUserDefine(sSQL);
		DBCLS = null;
		return FixPubCode(DT_Process);
	}

	private int GetProjMrsBaseData()
	{
		DataSet ds = theProjMrsA.GetProjMrsAWithNegativeMrsB(projectCode, 0);
		return ds.Tables[0].Rows.Count;
	}

	private int GetProjAnalysisSubItemZero()
	{
		BudProjMrsA budProjMrsA = new BudProjMrsA();
		return budProjMrsA.IsThereCostEquZeroItem(projectCode);
	}

	public DataTable FixPubCode(DataTable srcDT)
	{
		DateTime T1 = DateTime.Now;
		DataTable srcDT2 = srcDT.Copy();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(userID);
		aArr.Add("單筆引用單價");
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = userID;
		SysUser oSysUser = new SysUser();
		string ssDBName = oSysUser.GetSysUserDatabaseName(userID);
		ReSet2Mrs RESET2 = new ReSet2Mrs(aArr);
		DataSet trgDS = RESET2.GetDataSet2(ssDBName, "MRS", "", srcDT, 1);
		DataSet trgDSP = RESET2.GetDataSet2(ssDBName, CommonMethods.GetActionNameString(FormActionName), projectCode, srcDT2, 1);
		trgDSP.Tables[0].CaseSensitive = true;
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
				sSQLCmd = "Update " + CommonMethods.GetActionNameString(FormActionName) + "ProjMrsA Set pubCode =" + trgDSP.Tables[0].Rows[i]["resCode"].ToString() + " Where ProjectCode ='" + projectCode + "' And PubCode=" + trgDSP.Tables[0].Rows[i]["PubCode"].ToString() + '\r';
				object obj = sSQLCmd;
				sSQLCmd = string.Concat(obj, "Update ", CommonMethods.GetActionNameString(FormActionName), "ProjMrsB Set ParentCode =", trgDSP.Tables[0].Rows[i]["resCode"].ToString(), " Where ProjectCode ='", projectCode, "' And ParentCode=", trgDSP.Tables[0].Rows[i]["PubCode"].ToString(), '\r');
				obj = sSQLCmd;
				sSQLCmd = string.Concat(obj, "Update ", CommonMethods.GetActionNameString(FormActionName), "ProjMrsB Set pubCode =", trgDSP.Tables[0].Rows[i]["resCode"].ToString(), " Where ProjectCode ='", projectCode, "' And PubCode=", trgDSP.Tables[0].Rows[i]["PubCode"].ToString(), '\r');
				obj = sSQLCmd;
				sSQLCmd = string.Concat(obj, "Update ", CommonMethods.GetActionNameString(FormActionName), "ProjMrsC Set ParentCode =", trgDSP.Tables[0].Rows[i]["resCode"].ToString(), " Where ProjectCode ='", projectCode, "' And ParentCode=", trgDSP.Tables[0].Rows[i]["PubCode"].ToString(), '\r');
				obj = sSQLCmd;
				sSQLCmd = string.Concat(obj, "Update ", CommonMethods.GetActionNameString(FormActionName), "ProjMrsC Set pubCode =", trgDSP.Tables[0].Rows[i]["resCode"].ToString(), " Where ProjectCode ='", projectCode, "' And PubCode=", trgDSP.Tables[0].Rows[i]["PubCode"].ToString(), '\r');
				obj = sSQLCmd;
				sSQLCmd = string.Concat(obj, "Update ", CommonMethods.GetActionNameString(FormActionName), "ProjMrsC Set itemCode =", trgDSP.Tables[0].Rows[i]["resCode"].ToString(), " Where ProjectCode ='", projectCode, "' And itemCode=", trgDSP.Tables[0].Rows[i]["PubCode"].ToString(), '\r');
				obj = sSQLCmd;
				sSQLCmd = string.Concat(obj, "Update ", CommonMethods.GetActionNameString(FormActionName), "ItemA Set pubCode =", trgDSP.Tables[0].Rows[i]["resCode"].ToString(), " Where ProjectCode ='", projectCode, "' And PubCode=", trgDSP.Tables[0].Rows[i]["PubCode"].ToString(), '\r');
				DBCLS.ExecuteCommand(sSQLCmd);
			}
		}
		DBCLS = null;
		trgDS = null;
		return trgDSP.Tables[0];
	}

	private DataTable GetSelectedWorkItems()
	{
		DataTable srcDT = new DataTable();
		srcDT.Columns.Add("PccesCode", Type.GetType("System.String"));
		srcDT.Columns.Add("PubCode", Type.GetType("System.Int32"));
		for (int i = 1; i < gridBudget.Rows.Count; i++)
		{
			if (gridBudget.Rows[i].Selected && gridBudget.Rows[i]["Kind"].ToString().Trim().ToUpper() == "W")
			{
				DataRow DR = srcDT.NewRow();
				DR["PccesCode"] = gridBudget.Rows[i]["PccesCode"];
				DR["PubCode"] = gridBudget.Rows[i]["PubCode"];
				srcDT.Rows.Add(DR);
			}
		}
		return srcDT;
	}

	private void button1_Click_2(object sender, EventArgs e)
	{
		DataTable srcDT = new DataTable();
		srcDT.Columns.Add("PccesCode", Type.GetType("System.String"));
		srcDT.Columns.Add("PubCode", Type.GetType("System.Int32"));
		for (int i = 1; i < gridBudget.Rows.Count; i++)
		{
			if (gridBudget.Rows[i].Selected && gridBudget.Rows[i]["Kind"].ToString().Trim().ToUpper() == "W")
			{
				DataRow DR = srcDT.NewRow();
				DR["PccesCode"] = gridBudget.Rows[i]["PccesCode"];
				DR["PubCode"] = gridBudget.Rows[i]["PubCode"];
				srcDT.Rows.Add(DR);
			}
		}
	}

	private void frmBudget_FormClosed(object sender, FormClosedEventArgs e)
	{
		LeftPanel = null;
		MainPanel = null;
		functionButtons1 = null;
		toolbarsManager = null;
		_frmBudget_Toolbars_Dock_Area_Left = null;
		_frmBudget_Toolbars_Dock_Area_Right = null;
		_frmBudget_Toolbars_Dock_Area_Top = null;
		_frmBudget_Toolbars_Dock_Area_Bottom = null;
		imageList1 = null;
		c = null;
		panel3 = null;
		axSSPanel1 = null;
		statusBar = null;
		gridBudget = null;
		onlineList1 = null;
		imageList2 = null;
		ssp_Top = null;
		pnl_spliter = null;
		ssp_Upper = null;
		ssp_Bottom = null;
		ssp_Lower = null;
		Btn_Splt = null;
		ultraButton2 = null;
		BtnSwitchProject = null;
		ultraLabel10 = null;
		lblProjectData = null;
		iglst_splt_Btn = null;
		saveFileDialog1 = null;
		panel2 = null;
		ultraLabel2 = null;
		axSSPanel2 = null;
		lblTotal = null;
		TM_BDGT_AutoSave = null;
		cboHisPrice = null;
		dbItemA = null;
		FM_INFO = null;
		CSAnaPrn = null;
		Cntrl1 = null;
		Frm = null;
		dtClipboard = null;
		ToolLists = null;
		ToolParam = null;
		DS1 = null;
		GridColsSquence = null;
		GRID1 = null;
		dtItemA = null;
		dbMrsBase = null;
		tmrReCalAll = null;
		timer1 = null;
		GC.Collect();
	}

	private void cboHisPrice_AfterCloseUp(object sender, EventArgs e)
	{
		if (cboHisPrice.SelectedRow != null)
		{
			double PickCost = -999999.0;
			try
			{
				PickCost = Convert.ToDouble(cboHisPrice.Value);
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudget.cs--cboHisPrice_AfterCloseUp" + ex.Message);
				PickCost = -999999.0;
			}
			if (PickCost != -999999.0)
			{
				gridBudget[gridBudget.Row, "Cost"] = PickCost;
				UpdateSelectedRow(gridBudget.Row, cboHisPrice.SelectedRow.Cells[4].Text);
			}
			gridBudget.Select();
		}
	}

	private void UpdateSelectedRow(int rowIndex, string location)
	{
		CalcuParent(rowIndex);
		Row GridRow = gridBudget.Rows[rowIndex];
		DataView dvItemA = new DataView(dtItemA);
		dvItemA.RowFilter = "Sno = " + ArchConvert.Obj2Int(GridRow["SNo"]);
		if (dvItemA.Count > 0)
		{
			DataRow drItemA = dvItemA[0].Row;
			drItemA["ItemNo"] = GridRow["ItemNo"].ToString().Trim();
			drItemA["Qty"] = GridRow["Qty"].ToString().Trim();
			drItemA["Cost"] = GridRow["Cost"].ToString().Trim();
			drItemA["Amount"] = GridRow["Amount"].ToString().Trim();
			ExecResult ER = theItemA.GetDatasetUpdate(dsItemA);
			if (ER.ReturnCode != 0)
			{
				MessageBox.Show("更新失敗！" + ER.Message);
			}
		}
		dvItemA.Dispose();
		string IPStr = CommonMethods.GetIPAddress();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		if (dbItemA != null)
		{
			dbItemA = null;
		}
		if (dbItemA == null)
		{
			dbItemA = new Archnowledge.Pcces.BUDClass.ItemA(aArr);
		}
		aArr.Add(userID);
		aArr.Add(CommonMethods.GetFormTypeTitle(FormType.Budget));
		Archnowledge.Pcces.BUDClass.MrsBaseA dbMrsBaseA = new Archnowledge.Pcces.BUDClass.MrsBaseA(userID, aArr);
		dbMrsBaseA.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
		dbMrsBaseA.ps_projectcode = projectCode;
		dbMrsBaseA.ps_cost = ((gridBudget[rowIndex, "Cost"] != null) ? gridBudget[rowIndex, "Cost"].ToString() : null);
		dbMrsBaseA.ps_pubCode = ((gridBudget[rowIndex, "PubCode"] != null) ? gridBudget[rowIndex, "PubCode"].ToString() : null);
		dbMrsBaseA.ps_pccesCode = ((gridBudget[rowIndex, "PccesCode"] != null) ? gridBudget[rowIndex, "PccesCode"].ToString() : null);
		if (location != null)
		{
			dbMrsBaseA.ps_xNameC = location;
		}
		dbMrsBaseA.UpdItem();
		dbMrsBaseA = null;
		int iSno = ArchConvert.Obj2Int(gridBudget[rowIndex, "SNo"]);
		Reload_OneRow(iSno, rowIndex, RangeUpdate: false);
		aArr = null;
	}

	private void tmrReCalAll_Tick(object sender, EventArgs e)
	{
		try
		{
			FM_INFO._MinValue = dbItemA.ps_Min;
			FM_INFO._MaxValue = dbItemA.ps_Max;
			FM_INFO._ProgressValue = dbItemA.ps_CurrentProgress;
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudget.cs--tmrReCalAll_Tick" + ex.Message);
		}
	}

	private void CheckIsReCal(string YesNo)
	{
		theProject.UpdateProjectIsReCal(projectCode, YesNo);
	}

	private void SwitchToCorrectLevelStatus(int iLvl)
	{
		if (iLvl <= 0 || iLvl >= 9)
		{
			return;
		}
		((StateButtonTool)toolbarsManager.Tools["Level" + iLvl]).Checked = true;
		for (int i = 1; i < 9; i++)
		{
			if (i <= iLvl)
			{
				((StateButtonTool)toolbarsManager.Tools["Level" + i]).SharedProps.Enabled = true;
			}
			else
			{
				((StateButtonTool)toolbarsManager.Tools["Level" + i]).SharedProps.Enabled = false;
			}
		}
	}

	private void ExecuteEditForm(MrsBaseEditFormType sEditMode)
	{
		int RowIndex = gridBudget.Row;
		if (RowIndex <= 0 && sEditMode != MrsBaseEditFormType.New)
		{
			return;
		}
		int iParentSno = 0;
		int iSortOrder = 0;
		if (ArchConvert.Obj2String(gridBudget[RowIndex, "kind"]) == "B")
		{
			iParentSno = ArchConvert.Obj2Int(gridBudget[RowIndex, "sNo"]);
			Node Nd = gridBudget.Rows[RowIndex].Node.GetNode(NodeTypeEnum.LastChild);
			iSortOrder = ((Nd == null) ? 1 : (ArchConvert.Obj2Int(gridBudget[Nd.Row.Index, "SortOrder"]) + 1));
		}
		else
		{
			iParentSno = ArchConvert.Obj2Int(gridBudget[RowIndex, "ParentSno"]);
			iSortOrder = ArchConvert.Obj2Int(gridBudget[RowIndex, "SortOrder"]);
		}
		string sParentPrintToAnalysis = ((gridBudget[RowIndex, "PrintToAnalysis"] != null) ? gridBudget[RowIndex, "PrintToAnalysis"].ToString() : "0");
		if (RowIndex > 0 && (bool)gridBudget[RowIndex, "Analysis"] && sEditMode == MrsBaseEditFormType.CopyToNew)
		{
			bool EnableNewCalculateCost = false;
			Archnowledge.Pcces.DomainModule.General.PubProject thePubProject = new Archnowledge.Pcces.DomainModule.General.PubProject();
			if (thePubProject.GetPubProjectEnableNewCalculateCost(projectCode))
			{
				DoNewMrsCalculate(gridBudget[RowIndex, "pubCode"].ToString());
			}
			else
			{
				DoOldMrsCalculate(gridBudget[RowIndex, "pubCode"].ToString());
			}
		}
		FormMrsBaseEdit FM_EDIT = new FormMrsBaseEdit();
		FM_EDIT._UserID = userID;
		FM_EDIT._EditMode = sEditMode;
		FM_EDIT._CallerFormName = "FormBudget";
		FM_EDIT._ActionName = PccesFormAction.BUD;
		FM_EDIT._ProjectCode = projectCode;
		FM_EDIT._Istemplate = IsTemplate;
		FM_EDIT._Mesbox = "Message";
		FM_EDIT._IsSubmitBid = IsSubmitBid;
		if (sEditMode != MrsBaseEditFormType.New)
		{
			FM_EDIT._PubCode = (int)gridBudget[RowIndex, "PubCode"];
		}
		FM_EDIT._MainCost = MainItemCostPrecison.ToString();
		if (gridBudget[RowIndex, "UnitName"].ToString() == "式" && ArchConvert.Obj2Decimal(gridBudget[RowIndex, "Qty"]) == 1m && !ArchConvert.Obj2Bool(gridBudget[RowIndex, "Analysis"]))
		{
			BudProjMrsA theMrsA = new BudProjMrsA();
			if (theMrsA.CheckOne4WorkItemPriceCanChange(projectCode, gridBudget[RowIndex, "PccesCode"].ToString()))
			{
				FM_EDIT._AllowEditCost = true;
			}
		}
		if (FM_EDIT.ShowDialog(this) == DialogResult.OK && sEditMode == MrsBaseEditFormType.CopyToNew)
		{
			ExecResult ER = new ExecResult();
			if (F_NewChildPubCode != -1)
			{
				DataSet dsProjMrsA = theProjMrsA.GetProjMrsAByPubCode(projectCode, F_NewChildPubCode);
				int SNo = 0;
				if (dsProjMrsA.Tables.Count > 0 && dsProjMrsA.Tables[0].Rows.Count > 0)
				{
					DataRow theRow = dsProjMrsA.Tables[0].Rows[0];
					ER = theItemA.AddItemAByParent(projectCode, null, "0000", F_NewChildPubCode, "", 0, theRow["cName"], theRow["EName"], theRow["UnitName"], "W", theRow["cost"], theRow["usrQty"], theRow["usrAmt"], theRow["Memo"], 0, null, null, null, null, theRow["EUnit"], null, null, null, null, "", null, null, null, theRow["PccesCode"], null, null, null, null, null, null, null, sParentPrintToAnalysis, theRow["surName"], false, null, null, null, null, null, null, null, iParentSno, iSortOrder, out SNo);
					if (ER.ReturnCode == 0)
					{
						PageBreak thePageBreak = new BudPageBreak();
						thePageBreak.AddPageBreakIfExist(projectCode, SNo, "Y");
					}
				}
				if (ER.ReturnCode == 0 && SNo != 0)
				{
					theProject.ReArrangePrintNo(projectCode, iParentSno, !IsEditItemNo);
					ReloadGridAtRootSno(iParentSno);
				}
				CheckIsReCal("Y");
				if (ER.ReturnCode != 0)
				{
					MessageBox.Show(ER.Message);
				}
				string TypeID = ((gridBudget[RowIndex, "TypeID"] != null) ? gridBudget[RowIndex, "TypeID"].ToString() : "");
				string CostUID = ((gridBudget[RowIndex, "CostUID"] != null) ? gridBudget[RowIndex, "CostUID"].ToString() : string.Empty);
				if (ER.ReturnCode == 0 && CostUID != string.Empty && TypeID != string.Empty)
				{
					CostStructureMrsBase costStructure = new CostStructureMrsBase();
					costStructure.AddCostStructureMrsBase(TypeID, CostUID, gridBudget[RowIndex + 1, "PccesCode"].ToString());
				}
			}
		}
		FM_EDIT.Close();
		FM_EDIT.Dispose();
		FM_EDIT = null;
	}

	private void DoNewMrsCalculate(string PubCode)
	{
		int CurrentPubCode = ArchConvert.Obj2Int(PubCode);
		MrsCalculate theMrsCalculate = null;
		theMrsCalculate = new MrsCalculate(FormActionName, projectCode, 0);
		ExecResult ER = theMrsCalculate.Calculate(CurrentPubCode, null);
	}

	private void DoOldMrsCalculate(string PubCode)
	{
		ArrayList aArr = new ArrayList();
		aArr.Add(userID);
		aArr.Add("WinFORM 基本工料");
		Recost RC1 = new Recost(aArr);
		RC1.ps_IsProcessEvent = true;
		RC1.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
		RC1.ps_prjcode = projectCode;
		RC1.ps_pubcode = PubCode;
		AppLocation = AppDomain.CurrentDomain.BaseDirectory;
		string IsOldReCal = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "BDGT", "IsOldReCal");
		if (IsOldReCal.ToUpper() == "FALSE")
		{
			decimal[] dTmp = RC1.ReCalc2(1, 0m);
		}
		else if (IsOldReCal.ToUpper() == "TRUE")
		{
			decimal[] dTmp = RC1.ReCalc2(1, 0m);
		}
		else if (IsOldReCal.ToUpper() == "THIRD")
		{
			RC1.ps_SmallCalcuMode = "THIRD";
			decimal[] dTmp = RC1.ReCalc2(1, 0m);
		}
		else
		{
			decimal[] dTmp = RC1.ReCalc2(1, 0m);
		}
	}

	private void timer1_Tick(object sender, EventArgs e)
	{
		string IsReload = "";
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(userID);
		aArr.Add("判斷是否要重新reload--" + projectCode);
		Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
		PROJ.ps_projectCode = projectCode;
		PROJ.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
		IsReload = PROJ.GetIsReload(projectCode);
		if (IsReload == "Y")
		{
			LoadProjectData();
			PROJ.ps_IsReload = "N";
			PROJ.UpdItem();
			timer1.Stop();
		}
		PROJ = null;
		aArr = null;
	}

	private void InserReCalType(string sType)
	{
		theProject.UpdateReCalType(projectCode, sType);
	}

	private string GetReCalType()
	{
		string ReCalType = "1";
		string returnString = string.Empty;
		ReCalType = theProject.GetReCalType(projectCode);
		switch (ReCalType)
		{
		case "1":
			returnString = "FALSE";
			break;
		case "2":
			returnString = "TRUE";
			break;
		case "3":
			returnString = "THIRD";
			break;
		}
		if (ReCalType == string.Empty)
		{
			CommonMethods.IniWriteValue(AppLocation + "OptionSet.ini", "BDGT", "IsOldReCal", "FALSE");
		}
		return returnString;
	}

	private bool GetIsLockAnalys()
	{
		return IsLockAnalys;
	}

	private string GetIsLockAnalysLEMWQty()
	{
		string printMode = "1111";
		BidProject bidproject = new BidProject();
		DataSet ds = bidproject.GetProject(projectCode);
		if (ds.Tables[0].Rows.Count > 0)
		{
			printMode = ArchConvert.Obj2String(ds.Tables[0].Rows[0]["PrintMode"]);
			if (printMode.Length <= 47)
			{
				printMode = "1111";
			}
			else
			{
				try
				{
					printMode = printMode.Substring(47, 4);
				}
				catch
				{
				}
			}
		}
		ds = null;
		bidproject = null;
		return printMode;
	}

	private void BidbtnClose_Click(object sender, EventArgs e)
	{
		(base.ParentForm as frmPccesMain).LeftPanel.Width = 160;
		Close();
	}

	private void BtnDownloadDoc_Click(object sender, EventArgs e)
	{
		FormDownloadDoc FDD = new FormDownloadDoc(projectCode, currentDBName, userID);
		if (FDD.ShowDialog() == DialogResult.OK)
		{
			LoadProjectData();
		}
		FDD.Close();
		FDD.Dispose();
		FDD = null;
	}

	private void DelAmountItemB()
	{
		string srckind = CommonMethods.GetActionNameString(FormActionName);
		string sSQL = "Delete " + srckind + "ItemB where projectCode = '" + projectCode + "' and parentCode='99999999999999999999999999999999'";
		ArrayList aArr = new ArrayList();
		aArr.Add(userID);
		aArr.Add("取pccescode的值");
		ModifyDB ModDB = new ModifyDB(projectCode, aArr);
		DataTable DT = new DataTable();
		int iCount = ModDB.DBDele(sSQL);
		if (iCount > 0)
		{
			MessageBox.Show(this, "依新規定【總計】由系統自動計算，不得自行設定公式\n\n原公式設定將自動清除!!", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		ModDB = null;
		aArr = null;
	}

	private void Lock()
	{
		if (SysConfig.SysChangeManagement && budgetChangeCurrentVersion > 0)
		{
			BudProject theBudProject = new BudProject();
			DataTable dt = theBudProject.GetProjectCheckResult(projectCode);
			if (dt.Rows.Count > 0)
			{
				string Msg = "";
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					Msg = Msg + dt.Rows[i]["ErrDesc"].ToString() + "\n";
					if (Msg.Length > 2048)
					{
						break;
					}
				}
				MessageBox.Show("無法鎖定!因與前一版次比對有鎖定項目異常,這會導致預算書資料有錯誤,請檢查以下錯誤訊息並通知相關人員處理\n" + Msg);
				return;
			}
		}
		if (MessageBox.Show("鎖定前會先執行重新總計，是否繼續？", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
		{
			return;
		}
		Th_ReCal_All(Auto: true);
		if (!ExecCOMSSubAccCheck())
		{
			return;
		}
		if (SysConfig.SysChangeManagement)
		{
			if (checkData2GridSpace)
			{
				if (checkData2GridZero || MessageBox.Show("有部分工項的單價或數量為0(橘色工項)，是否要檢查? \n 選[否]則繼續鎖定!", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
				{
					theProject.UpdateProjectIsReCal(projectCode, "N");
					SetLockStatus(status: true);
					DoMenuAction("COMSExpandBudget");
				}
			}
			else
			{
				MessageBox.Show("部分工項的項次、項目或單位為空白，請使用『工具』-->『項次編號設定』中，增加編號。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		else
		{
			theProject.UpdateProjectIsReCal(projectCode, "N");
			SetLockStatus(status: true);
		}
	}

	private void UnLock()
	{
		if (FormActionName == PccesFormAction.BUD && budgetChangeCurrentVersion > -1)
		{
			BudExeProject budExeProject = new BudExeProject();
			if (budExeProject.GetCOMSExpandBudget(projectCode))
			{
				MessageBox.Show("此預算書已經展開明細表至COMS，無法解除鎖定", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
		}
		SetLockStatus(status: false);
	}

	private void SetLockStatus(bool status)
	{
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(userID);
		aArr.Add("讀取預算書總價--" + projectCode);
		string IsLocked = ((!status) ? "N" : "Y");
		Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
		PROJ.ps_projectCode = projectCode;
		PROJ.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
		if (FormActionName == PccesFormAction.BUD && GetCurrentBDGT_Type() == "CNT")
		{
			PROJ.ps_IsCheckOutCnt = IsLocked;
		}
		else
		{
			PROJ.ps_IsCheckOut = IsLocked;
		}
		PROJ.UpdItem();
		if (budgetType == BudgetType.Types.CostEstimation || budgetType == BudgetType.Types.CostQuotationMerged)
		{
			BudProjectCodeMapping budProjectCodeMapping = new BudProjectCodeMapping();
			budProjectCodeMapping.SetBudProjectCodeMappingApproved(projectCode, status, userID);
		}
		else if (status && budgetChangeCurrentVersion >= 0)
		{
			BudExeProject budExeProject = new BudExeProject();
			budExeProject.ApproveBudgetChange(projectCode, budgetChangeCurrentVersion, userID);
		}
		LockOrUnlockToolbar(status);
	}

	private double GetItemAAmount()
	{
		decimal shareVDF1 = 0m;
		decimal total = 0m;
		total = (decimal)theItemA.GetItemAAmount(projectCode, 0);
		return (double)(total + shareVDF1);
	}

	private void DoBudgetExport()
	{
		FormDataExport_Wzd FM_DataExport = new FormDataExport_Wzd();
		FM_DataExport._UserID = userID;
		SysUser sysUser = new SysUser();
		FM_DataExport._F_DB = (currentDBName = sysUser.GetSysUserDatabaseName(userID));
		FM_DataExport._ProjectCode = projectCode;
		Cursor = Cursors.Default;
		FM_DataExport.ShowDialog(this);
	}

	private void SetTemplateControlAvailability()
	{
		string[] buttonList = new string[37]
		{
			"EditPageBreakSetting", "EditBidSetting", "CombineBudget", "CombineBid", "DeleteThisProject", "EditMenu", "EditProjectInfo", "DetailEditMenu", "ToolMenu", "Cut",
			"Copy", "Paste", "MoveUp", "MoveDown", "Outdent", "Indent", "ReArrangeItemNo", "Recalculate", "AdjustTotalAmount", "EditAliasSettingForReport",
			"BudgetChange", "SetPrecision", "RightClickMenu", "InsertMainItem", "EditMainItem", "InsertWorkItem", "EditWorkItem", "AddBookmark", "Delete", "EditCostBreakdown",
			"LockCost", "UnlockCost", "ImportFromMrsBase", "MakeAmortizedItem", "CancelAmortizedItem", "CloneWorkItem", "EditCostStructureProperty"
		};
		SetButtonListVisibility(buttonList, Visible: false);
		EnableContextMenu = false;
		toolbarsManager.Tools["Delete"].SharedProps.Enabled = false;
		SetAllColumnsNotAllowEditing();
	}

	private void SetButtonListAvailibility(string[] ButtonList, bool Enabled)
	{
		foreach (string ButtonName in ButtonList)
		{
			toolbarsManager.Tools[ButtonName].SharedProps.Enabled = Enabled;
		}
	}

	private void SetButtonListVisibility(string[] ButtonList, bool Visible)
	{
		foreach (string ButtonName in ButtonList)
		{
			toolbarsManager.Tools[ButtonName].SharedProps.Visible = Visible;
		}
	}

	private void COMSCheckBudgetFromContract()
	{
		DataSet ds = new DataSet();
		ExecResult ER = new ExecResult();
		ChangeManagementServiceHelper theChangeManagementServiceHelper = new ChangeManagementServiceHelper();
		BudProjectCodeMapping theBudProjectCodeMapping = new BudProjectCodeMapping();
		DataSet dsBudProjectCodeMapping = theBudProjectCodeMapping.GetBudProjectCodeMappingByProjectCode(projectCode);
		string ParentProjectcode = dsBudProjectCodeMapping.Tables[0].Rows[0]["ParentProjectCode"].ToString().Trim();
		ds = theChangeManagementServiceHelper.GetCtrPairPccesWithAmount(ParentProjectcode, projectCode, out ER);
	}

	private bool CheckFormOpened()
	{
		if (base.ParentForm != null)
		{
			Form[] mdiChildren = base.ParentForm.MdiChildren;
			foreach (Form frm in mdiChildren)
			{
				if (frm is frmBudget theBudget && theBudget != this)
				{
					Archnowledge.Common.DebugUtil.OutputDebugString("CheckFormOpened Error : frmBudget has existed. projectCode=" + projectCode);
					MessageBox.Show("系統偵測出問題，請重新開啟。", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return true;
				}
			}
		}
		return false;
	}

	private void FindGridTreeRange(int RootSno, out int RootIndex, out int RootTreeEndIndex)
	{
		int RootLevel = 0;
		RootIndex = 0;
		RootTreeEndIndex = 0;
		for (int RowIndex = 1; RowIndex < gridBudget.Rows.Count; RowIndex++)
		{
			if (RootIndex > 0 && ArchConvert.Obj2Int(gridBudget[RowIndex, "LevelNo"]) <= RootLevel)
			{
				RootTreeEndIndex = RowIndex - 1;
				break;
			}
			if (ArchConvert.Obj2Int(gridBudget[RowIndex, "SNo"]) == RootSno)
			{
				RootIndex = RowIndex;
				RootLevel = ArchConvert.Obj2Int(gridBudget.Rows[RowIndex]["LevelNo"]);
			}
		}
	}

	private int GetRowIndexBySno(int Sno)
	{
		int FindRowIndex = 0;
		for (int index = 1; index < gridBudget.Rows.Count; index++)
		{
			if (ArchConvert.Obj2Int(gridBudget[index, "SNo"]) == Sno)
			{
				FindRowIndex = index;
				break;
			}
		}
		return FindRowIndex;
	}

	private void FindDataTableTreeRange(int RootSno, out int RootIndex, out int RootTreeEndIndex)
	{
		int RootLevel = 0;
		RootIndex = 0;
		RootTreeEndIndex = 0;
		DataView dvItemA = new DataView(dtItemA);
		dvItemA.Sort = "printNo ASC";
		for (int RowIndex = 1; RowIndex < dvItemA.Count + 1; RowIndex++)
		{
			if (RootIndex > 0 && ArchConvert.Obj2Int(dvItemA[RowIndex - 1]["levelNo"]) <= RootLevel)
			{
				RootTreeEndIndex = RowIndex - 1;
				break;
			}
			if (ArchConvert.Obj2Int(dvItemA[RowIndex - 1]["sNo"]) == RootSno)
			{
				RootIndex = RowIndex;
				RootLevel = ArchConvert.Obj2Int(dvItemA[RowIndex - 1]["levelNo"]);
			}
		}
	}

	private void ReloadGridAtRootSno(int RootSno)
	{
		if (RootSno == 0)
		{
			LoadProjectData();
			return;
		}
		dsItemA = theItemA.GetItemA(projectCode, 0);
		dtItemA = dsItemA.Tables[0];
		int GridStart = 0;
		int GridEnd = 0;
		int DataStart = 0;
		int DataEnd = 0;
		int RowsAdded = 0;
		FindGridTreeRange(RootSno, out GridStart, out GridEnd);
		FindDataTableTreeRange(RootSno, out DataStart, out DataEnd);
		if (GridStart == DataStart)
		{
			gridBudget.AfterSelChange -= gridBudget1_AfterSelChange;
			RowsAdded = DataEnd - DataStart - (GridEnd - GridStart);
			if (RowsAdded > 0)
			{
				while (RowsAdded > 0)
				{
					gridBudget.Rows.Insert(GridStart);
					RowsAdded--;
				}
			}
			else if (RowsAdded < 0)
			{
				for (; RowsAdded < 0; RowsAdded++)
				{
					gridBudget.Rows.Remove(GridStart);
				}
			}
			gridBudget.AfterSelChange += gridBudget1_AfterSelChange;
			for (int i = DataStart; i < DataEnd + 1; i++)
			{
				Reload_OneRow(ArchConvert.Obj2Int(dtItemA.Rows[i - 1]["sNo"]), i, RangeUpdate: true);
			}
			for (int i = 1; i < gridBudget.Rows.Count; i++)
			{
				if (gridBudget.Rows[i]["Kind"] != null && !gridBudget.Rows[i].IsNode)
				{
					gridBudget.Rows[i].IsNode = true;
				}
				if (gridBudget.Rows[i].IsNode && gridBudget.Rows[i].Node.Level != ArchConvert.Obj2Int(gridBudget.Rows[i]["LevelNo"]))
				{
					gridBudget.Rows[i].Node.Level = ArchConvert.Obj2Int(gridBudget.Rows[i]["LevelNo"]);
				}
			}
			for (int j = GridStart; j < gridBudget.Rows.Count; j++)
			{
				if (gridBudget.Rows[j].IsNode && gridBudget.Rows[j]["Kind"].ToString() == "B")
				{
					gridBudget.Rows[j].Node.Collapsed = true;
					gridBudget.Rows[j].Node.Collapsed = false;
				}
			}
		}
		else
		{
			Data2Grid();
		}
	}

	private void SetGridFocusBySno(int Sno, bool NeedAtTop)
	{
		gridBudget.AfterSelChange -= gridBudget1_AfterSelChange;
		if (NeedAtTop)
		{
			gridBudget.Row = gridBudget.Rows.Count - 1;
		}
		gridBudget.Row = GetRowIndexBySno(Sno);
		gridBudget.AfterSelChange += gridBudget1_AfterSelChange;
	}

	private void gridBudget_Resize(object sender, EventArgs e)
	{
		frmBudget_Resize(sender, e);
	}

	private bool IsCheckAccQtyAmtPass(string PccesCode, string theConfigValue, bool IsAnalysis, string unitString, string ColName)
	{
		bool IsPass = false;
		ComsWebService CW = new ComsWebService(projectCode);
		DataSet DS_Resturn = CW.GetSubAccTotalByPccesCode(PccesCode);
		if (IsAnalysis)
		{
		}
		return IsPass;
	}

	private DataTable GetAnalysisTable(string F_UserID, string F_ProjectCode, string F_sno)
	{
		string ssSQL = "\r\ndeclare @DecItemQty int\r\ndeclare @DecItemCost int\r\ndeclare @DecItemAmt int\r\n \r\nSelect @DecItemQty=itemQty, @DecItemCost=itemCost, @DecItemAmt=itemAmt from PubDecimal where ProjectCode = '" + F_ProjectCode + "'\r\nif @DecItemQty is null\r\n\tSelect @DecItemQty = 3 \r\n\r\nif @DecItemCost is null\r\n\tSelect @DecItemCost = 0\r\n\r\nif @DecItemAmt is null\r\n\tSelect @DecItemAmt = 0\r\n\r\nSelect case(Upper(a.kind))   \r\n\tWhen 'W' Then b.pccesCode   \r\n\tElse a.pccesCode   \r\nEnd as pccescode,\r\ncase(Upper(a.kind))   \r\n\tWhen 'W' Then b.cName   \r\n\tElse a.cName   \r\nEnd as cName ,\r\ncase(Upper(a.kind))   \r\n\tWhen 'W' Then b.eName   \r\n\tElse a.eName   \r\nEnd as eName ,\r\ncase(Upper(a.kind))   \r\n\tWhen 'W' Then b.unitName   \r\n\tElse a.unitName   \r\nEnd as unitName ,\r\ncase(Upper(a.kind))   \r\n\tWhen 'W' Then b.eUnit   \r\n\tElse a.eUnit   \r\nEnd as eUnit ,\r\ncase(Upper(a.kind))   \r\n\tWhen 'W' Then b.rate   \r\n\tElse a.rate   \r\nEnd as rate ,\r\ncase   \r\n\tWhen Upper(a.kind)='W' and (b.CostKind IS NULL OR b.CostKind <> '$') Then b.cost   \r\n\tElse a.cost   \r\nEnd as cost ,\r\na.preCost,a.projectCode, a.sNo, RTrim(a.printNo) as PrintNo, a.pubCode, RTrim(a.itemNo) as ItemNo, a.levelNo, a.Flag, \r\na.kind, a.qty, a.amount, a.memo, a.setDecimal,a.CostUnit,a.Property1,a.Property2,a.Property3,a.CostUID,\r\na.TypeID, a.bidCode, a.share, a.dsctLock, a.Formula, a.SubProjectCode,a.ShareSno,a.ShareCost,a.LockCost, \r\na.ModLock,\r\nisnull(a.QtyDec,@DecItemQty) as QtyDec,\r\nisnull(a.CostDec,@DecItemCost) as CostDec,\r\nisnull(a.AmtDec,@DecItemAmt) as AmtDec,\r\na.PwrSet, a.PrintToAnalysis,a.printNo as ReportPrintNo, \r\na.fixPrice, a.flag, a.Lock, a.IsGreenItem, a.IsGreenMethod, a.IsGreenMaterial, a.IsGreenEnergy, a.BudgetChangeReason, b.IsCommonItem,\r\na.VersionHistory, cast(a.Sno as varchar(128)) as Lem_UID ,\r\n\tb.PccesCode as BudItemCode,b.PccesCode as BudPccesCode,a.qty as BudItemQty,a.cost as BudItemCost,a.unitName as BudItemUnit, B.analysis,B.analysis as BudAnalysis, B.analysisQty, 0 as BudListNo \r\nfrom budItemA A left join budProjMrsA B  \r\non A.pubCode = B.pubCode and A.ProjectCode=B.ProjectCode  Where A.ProjectCode='" + F_ProjectCode + "' And A.Kind<>'Z' and A.Sno=" + F_sno + "order by printNo ";
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add("建立炸開的table");
		ModifyDB ModDB = new ModifyDB(F_ProjectCode, aArr);
		DataTable dtPreBudLem = ModDB.DBList(ssSQL);
		dtPreBudLem.Columns.Add("BudItemName", Type.GetType("System.String"));
		dtPreBudLem.Columns.Add("BudLevel", Type.GetType("System.String"));
		dtPreBudLem.Columns.Add("BudResName", Type.GetType("System.String"));
		dtPreBudLem.Columns.Add("BudItemType", Type.GetType("System.String"));
		dtPreBudLem.Columns.Add("sNoB", Type.GetType("System.Int32"));
		dtPreBudLem.Columns.Add("PowerRate", Type.GetType("System.Decimal"));
		dtPreBudLem.Columns.Add("BudItemNo", Type.GetType("System.String"));
		dtPreBudLem.Columns.Add("ItemParentName", Type.GetType("System.String"));
		dtPreBudLem.Columns.Add("FullItemNo", Type.GetType("System.String"));
		Archnowledge.Pcces.DomainModule.General.PubDecimal pubDecimal = new Archnowledge.Pcces.DomainModule.General.PubDecimal();
		DataSet dsPubDecimal = pubDecimal.GetPubDecimal(F_ProjectCode);
		int itemQtyPrecision = 4;
		int itemCostPrecision = 4;
		int analysisQtyPrecision = 4;
		int analysisCostPrecision = 4;
		if (dsPubDecimal.Tables[0].Rows.Count > 0)
		{
			itemQtyPrecision = ArchConvert.Obj2Int(dsPubDecimal.Tables[0].Rows[0]["itemQty"]);
			itemCostPrecision = ArchConvert.Obj2Int(dsPubDecimal.Tables[0].Rows[0]["itemCost"]);
			analysisQtyPrecision = ArchConvert.Obj2Int(dsPubDecimal.Tables[0].Rows[0]["analysisQty"]);
			analysisCostPrecision = ArchConvert.Obj2Int(dsPubDecimal.Tables[0].Rows[0]["analysisCost"]);
		}
		return dtPreBudLem;
	}

	private bool ExecCOMSSubAccCheck()
	{
		bool retV = true;
		string IsCheckAccQtyAmt = SysConfig.SysIsCheckAccQtyAmt.ToUpper();
		if (IsCheckAccQtyAmt == "WARNONLY" || IsCheckAccQtyAmt == "DISABLE")
		{
			ArrayList aArr = new ArrayList();
			aArr.Clear();
			aArr.Add(userID);
			aArr.Add("讀取 budItemA.QtyCstChgFlgforCOMS = Y 的項目[" + projectCode + "](" + CommonMethods.GetIPAddress() + ")");
			ModifyDB StdCom = new ModifyDB(ProjectCode, aArr);
			string sSQL = "Select sNo, itemNo, pubCode, pccesCode, cName, unitName From budItemA Where QtyCstChgFlgforCOMS='Y' and projectCode='" + projectCode + "' ";
			DataTable DT_Cmps = StdCom.DBList(sSQL);
			string sWarningMessage = "";
			string sQtyOrAmt = "";
			for (int i = 0; i < DT_Cmps.Rows.Count; i++)
			{
				if (CompareWithCOMS(DT_Cmps.Rows[i]["sNo"].ToString(), DT_Cmps.Rows[i]["pubCode"].ToString(), DT_Cmps.Rows[i]["pccesCode"].ToString(), out sQtyOrAmt))
				{
					string sSQL2 = "Update budItemA Set QtyCstChgFlgforCOMS = null Where projectCode='" + projectCode + "' and sno=" + DT_Cmps.Rows[i]["sno"].ToString() + "";
					StdCom.DBUpd(sSQL2);
				}
				else
				{
					string text = sWarningMessage;
					sWarningMessage = text + DT_Cmps.Rows[i]["itemNo"].ToString() + "\t" + DT_Cmps.Rows[i]["pccesCode"].ToString() + "\t" + DT_Cmps.Rows[i]["cName"].ToString() + "\t" + DT_Cmps.Rows[i]["unitName"].ToString() + "\n";
				}
			}
			if (sWarningMessage != "" && IsCheckAccQtyAmt == "WARNONLY")
			{
				if (sQtyOrAmt == "Amount")
				{
					MessageBox.Show(this, "以下項目, 修改後低於已計價金額, 請注意!!\n\n" + sWarningMessage, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				else if (sQtyOrAmt == "Quantity")
				{
					MessageBox.Show(this, "以下項目, 修改後低於已計價量, 請注意!!\n\n" + sWarningMessage, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			else if (sWarningMessage != "" && IsCheckAccQtyAmt == "DISABLE")
			{
				if (sQtyOrAmt == "Amount")
				{
					MessageBox.Show(this, "以下項目, 修改後低於已計價金額, 請修正!!\n\n" + sWarningMessage, "警示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				else if (sQtyOrAmt == "Quantity")
				{
					MessageBox.Show(this, "以下項目, 修改後低於已計價量, 請修正!!\n\n" + sWarningMessage, "警示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			if (sWarningMessage != "" && MessageBox.Show(this, "想要查看明細資料嗎?", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				string sPath = Path.Combine(Path.GetTempPath(), "COMSSubAccCheck.xml");
				DataSet DS_DEBUG_R = new DataSet();
				DS_DEBUG_R.ReadXml(sPath);
				FormDEBUG FM = new FormDEBUG();
				FM.Owner = this;
				FM.DisplayDataSet = DS_DEBUG_R;
				FM.ShowDialog();
			}
			if (sWarningMessage != "" && IsCheckAccQtyAmt == "WARNONLY")
			{
				retV = true;
			}
			else if (sWarningMessage != "" && IsCheckAccQtyAmt == "DISABLE")
			{
				retV = false;
			}
			else if (sWarningMessage == "")
			{
				retV = true;
			}
		}
		return retV;
	}

	private bool CompareWithCOMS(string sNO, string pubCode, string pccesCode, out string QtyOrAmt)
	{
		QtyOrAmt = "";
		bool retV = true;
		DataTable dtPreBudLem = GetAnalysisTable(userID, projectCode, sNO);
		dtPreBudLem.Columns.Add("usrQty", Type.GetType("System.Decimal"));
		dtPreBudLem.Columns.Add("usrAmt", Type.GetType("System.Decimal"));
		DataRow theRow = dtPreBudLem.Rows[0];
		if (theRow["analysis"].ToString() == "1")
		{
			Expand(userID, projectCode, theRow, theRow, ref dtPreBudLem, 0, theRow["ItemNo"].ToString().Trim());
		}
		else
		{
			ArrayList aArr = new ArrayList();
			aArr.Add(userID);
			aArr.Add("建立炸開的table");
			ModifyDB ModDB = new ModifyDB(projectCode, aArr);
			if (theRow["BudPccesCode"].ToString().Trim() == "")
			{
				dtPreBudLem.Rows[0]["usrQty"] = 0;
			}
			else
			{
				dtPreBudLem.Rows[0]["usrQty"] = ModDB.DBGetValue("Select IsNull(usrQty,0) as usrQty From budProjMrsA Where ProjectCode='" + projectCode + "' and pccesCode='" + theRow["BudPccesCode"].ToString() + "'");
			}
			if (theRow["BudPccesCode"].ToString().Trim() == "")
			{
				dtPreBudLem.Rows[0]["usrAmt"] = 0;
			}
			else
			{
				dtPreBudLem.Rows[0]["usrAmt"] = ModDB.DBGetValue("Select IsNull(usrAmt,0) as usrAmt From budProjMrsA Where ProjectCode='" + projectCode + "' and pccesCode='" + theRow["BudPccesCode"].ToString() + "'");
			}
			ModDB = null;
		}
		ComsWebService CW = new ComsWebService(projectCode);
		DataSet DS_Cmp = CW.GetSubAccTotalByPccesCode(pccesCode);
		if (dtPreBudLem.Columns.IndexOf("svrPccesCode") == -1)
		{
			dtPreBudLem.Columns.Add("svrPccesCode", Type.GetType("System.String"));
		}
		if (dtPreBudLem.Columns.IndexOf("svrItemQty") == -1)
		{
			dtPreBudLem.Columns.Add("svrItemQty", Type.GetType("System.Decimal"));
		}
		if (dtPreBudLem.Columns.IndexOf("svrItemAmt") == -1)
		{
			dtPreBudLem.Columns.Add("svrItemAmt", Type.GetType("System.Decimal"));
		}
		for (int i = 0; i < DS_Cmp.Tables[0].Rows.Count; i++)
		{
			string sPCCES_CODE = DS_Cmp.Tables[0].Rows[i]["pccesCode"].ToString();
			DataRow[] DRs = dtPreBudLem.Select("BudPccesCode = '" + sPCCES_CODE + "'");
			if (DRs.Length <= 0)
			{
				continue;
			}
			DataRow[] COMS_DRs = DS_Cmp.Tables[0].Select("PccesCode = '" + sPCCES_CODE + "' ");
			if (COMS_DRs.Length <= 0)
			{
				continue;
			}
			decimal itemQty_COMS = ArchConvert.Obj2Decimal(COMS_DRs[0]["ItemQty"]);
			decimal itemAmt_COMS = ArchConvert.Obj2Decimal(COMS_DRs[0]["ItemAmt"]);
			DRs[0]["svrPccesCode"] = COMS_DRs[0]["PccesCode"];
			DRs[0]["svrItemQty"] = COMS_DRs[0]["ItemQty"];
			DRs[0]["svrItemAmt"] = COMS_DRs[0]["ItemAmt"];
			if (DRs[0]["sNo"].ToString() != "" && DRs[0]["usrQty"].ToString() == "")
			{
				if (DRs[0]["unitName"].ToString() == "式" && ArchConvert.Obj2Decimal(DRs[0]["BudItemQty"]) * ArchConvert.Obj2Decimal(DRs[0]["BudItemCost"]) < itemAmt_COMS)
				{
					retV = false;
					QtyOrAmt = "Amount";
				}
				else if (SysConfig.OneSetItemCheckBothQtyAndAmt && DRs[0]["unitName"].ToString() != "式" && (ArchConvert.Obj2Decimal(DRs[0]["BudItemQty"]) < itemQty_COMS || ArchConvert.Obj2Decimal(DRs[0]["BudItemQty"]) * ArchConvert.Obj2Decimal(DRs[0]["BudItemCost"]) < itemAmt_COMS))
				{
					retV = false;
					if (ArchConvert.Obj2Decimal(DRs[0]["BudItemQty"]) < itemQty_COMS)
					{
						QtyOrAmt = "Quantity";
					}
					else if (ArchConvert.Obj2Decimal(DRs[0]["BudItemQty"]) * ArchConvert.Obj2Decimal(DRs[0]["BudItemCost"]) < itemAmt_COMS)
					{
						QtyOrAmt = "Amount";
					}
				}
				else if (!SysConfig.OneSetItemCheckBothQtyAndAmt && DRs[0]["unitName"].ToString() != "式" && ArchConvert.Obj2Decimal(DRs[0]["BudItemQty"]) < itemQty_COMS)
				{
					retV = false;
					QtyOrAmt = "Quantity";
				}
			}
			else if (DRs[0]["unitName"].ToString() == "式" && ArchConvert.Obj2Decimal(DRs[0]["usrAmt"]) < itemAmt_COMS)
			{
				retV = false;
				QtyOrAmt = "Amount";
			}
			else if (SysConfig.OneSetItemCheckBothQtyAndAmt && DRs[0]["unitName"].ToString() != "式" && (ArchConvert.Obj2Decimal(DRs[0]["usrQty"]) < itemQty_COMS || ArchConvert.Obj2Decimal(DRs[0]["usrAmt"]) < itemAmt_COMS))
			{
				retV = false;
				if (ArchConvert.Obj2Decimal(DRs[0]["usrQty"]) < itemQty_COMS)
				{
					QtyOrAmt = "Quantity";
				}
				else if (ArchConvert.Obj2Decimal(DRs[0]["usrAmt"]) < itemAmt_COMS)
				{
					QtyOrAmt = "Amount";
				}
			}
			else if (!SysConfig.OneSetItemCheckBothQtyAndAmt && DRs[0]["unitName"].ToString() != "式" && ArchConvert.Obj2Decimal(DRs[0]["usrQty"]) < itemQty_COMS)
			{
				retV = false;
				QtyOrAmt = "Quantity";
			}
		}
		if (!retV)
		{
			string sPath = Path.Combine(Path.GetTempPath(), "COMSSubAccCheck.xml");
			DataSet DS_DEBUG = new DataSet();
			DS_DEBUG.Tables.Add(dtPreBudLem.Copy());
			DS_DEBUG.WriteXml(sPath);
			DS_DEBUG = null;
		}
		return retV;
	}

	private void ultraButton1_Click_1(object sender, EventArgs e)
	{
		ultraButton1.Text = FormActionName.ToString();
	}

	private void Expand(string F_UserID, string F_ProjectCode, DataRow ItemDR, DataRow rowParent, ref DataTable dtPreBudLem, int iRowIndex, string itemNoHeader)
	{
		int j = 0;
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add("建立炸開的table");
		string ParentPubCode = ArchConvert.Obj2String(rowParent["pubCode"]);
		string ssSQL = "Select '' as ItemNo, '' as PrintNo, A.CName, A.unitName, A.analysis,A.analysisQty, A.pubCode, A.costKind, A.memo, A.PwrSet, B.qty, B.Cost, B.Amount,B.listno,B.sNo   From budProjMrsA A Left Join budProjMrsB B on A.ProjectCode=B.ProjectCode and A.PubCode=B.PubCode  Where B.ProjectCode='" + F_ProjectCode + "' and B.ParentCode=" + ParentPubCode + " order by B.listno ";
		ModifyDB ModDB = new ModifyDB(F_ProjectCode, aArr);
		DataTable dtMrsB = new DataTable();
		dtMrsB = ModDB.DBList(ssSQL);
		dtMrsB.Columns.Add("BudItemName", Type.GetType("System.String"));
		dtMrsB.Columns.Add("BudItemCode", Type.GetType("System.String"));
		dtMrsB.Columns.Add("BudItemUnit", Type.GetType("System.String"));
		dtMrsB.Columns.Add("BudItemType", Type.GetType("System.String"));
		dtMrsB.Columns.Add("BudItemCost", Type.GetType("System.Decimal"));
		dtMrsB.Columns.Add("BudItemQty", Type.GetType("System.Decimal"));
		dtMrsB.Columns.Add("levelNo", Type.GetType("System.String"));
		dtMrsB.Columns.Add("Lem_UID", Type.GetType("System.String"));
		dtMrsB.Columns.Add("BudAnalysis", Type.GetType("System.String"));
		dtMrsB.Columns.Add("PowerRate", Type.GetType("System.Decimal"));
		dtMrsB.Columns.Add("ItemParentName", Type.GetType("System.String"));
		dtMrsB.Columns.Add("FullItemNo", Type.GetType("System.String"));
		dtMrsB.Columns.Add("AnalysisQty", Type.GetType("System.Decimal"));
		string sAnalysisQty = ModDB.DBGetValue("Select analysisQty From budProjMrsA Where ProjectCode='" + F_ProjectCode + "' and pubCode=" + ParentPubCode + "");
		double ParentAnalysisQty = ArchConvert.Obj2Double(sAnalysisQty);
		for (int i = 0; i < dtMrsB.Rows.Count; i++)
		{
			string SQL = "select * from budProjMrsA where PubCode=" + dtMrsB.Rows[i]["pubCode"].ToString() + " and ProjectCode='" + F_ProjectCode + "' ";
			DataTable DT_Temp = ModDB.DBList(SQL);
			DataRow rowPreBudLem = dtPreBudLem.NewRow();
			if (dtMrsB.Rows[i]["analysis"].ToString() == "1")
			{
				rowPreBudLem["PrintNo"] = rowParent["PrintNo"].ToString().Trim() + (i + 1).ToString().PadLeft(4, '0');
				rowPreBudLem["ItemNo"] = rowParent["ItemNo"].ToString().Trim();
				if (rowParent.Table.Columns.IndexOf("pccesCode") > -1)
				{
					rowPreBudLem["pccesCode"] = rowParent["pccesCode"].ToString().Trim();
					rowPreBudLem["BudItemCode"] = rowParent["pccesCode"].ToString().Trim();
				}
				else
				{
					rowPreBudLem["BudItemCode"] = rowParent["BudItemCode"].ToString().Trim();
				}
				rowPreBudLem["BudItemCost"] = rowParent["BudItemCost"].ToString().Trim();
				double Qty = ArchConvert.Obj2Double(dtMrsB.Rows[i]["qty"]);
				double ParentQty = ArchConvert.Obj2Double(rowParent["qty"]);
				rowPreBudLem["BudItemQty"] = Qty * ParentQty / ParentAnalysisQty;
				rowPreBudLem["PowerRate"] = Qty / ParentAnalysisQty;
				rowPreBudLem["BudItemName"] = rowParent["cName"].ToString().Trim();
				rowPreBudLem["CName"] = rowParent["CName"].ToString().Trim();
				rowPreBudLem["unitName"] = dtMrsB.Rows[i]["unitName"];
				rowPreBudLem["BudItemUnit"] = rowParent["BudItemUnit"].ToString().Trim();
				rowPreBudLem["qty"] = dtMrsB.Rows[i]["qty"];
				rowPreBudLem["analysis"] = dtMrsB.Rows[i]["analysis"];
				rowPreBudLem["BudAnalysis"] = dtMrsB.Rows[i]["analysis"];
				rowPreBudLem["Amount"] = dtMrsB.Rows[i]["Amount"];
				rowPreBudLem["memo"] = dtMrsB.Rows[i]["memo"];
				rowPreBudLem["BudListNo"] = dtMrsB.Rows[i]["listno"];
				rowPreBudLem["sNoB"] = dtMrsB.Rows[i]["sNo"];
				rowPreBudLem["BudLevel"] = itemNoHeader + "-" + j;
				rowPreBudLem["Lem_UID"] = rowParent["Lem_UID"].ToString().Trim() + "." + dtMrsB.Rows[i]["sNo"].ToString();
				rowPreBudLem["levelNo"] = ArchConvert.Obj2Int(rowParent["levelNo"]) + 1;
				rowPreBudLem["ItemParentName"] = ItemDR["CName"];
				rowPreBudLem["FullItemNo"] = rowParent["FullItemNo"].ToString() + "." + dtMrsB.Rows[i]["listNo"].ToString().Trim();
				rowPreBudLem["PwrSet"] = dtMrsB.Rows[i]["PwrSet"];
				rowPreBudLem["AnalysisQty"] = dtMrsB.Rows[i]["AnalysisQty"];
				string CostKind = "";
				double UsrAmt = 0.0;
				double UsrQty = 0.0;
				if (DT_Temp.Rows.Count > 0)
				{
					rowPreBudLem["BudPccesCode"] = DT_Temp.Rows[0]["PccesCode"];
					rowPreBudLem["BudResName"] = DT_Temp.Rows[0]["cName"];
					CostKind = ArchConvert.Obj2String(DT_Temp.Rows[0]["CostKind"]).Trim();
					UsrQty = ArchConvert.Obj2Double(DT_Temp.Rows[0]["UsrQty"]);
					UsrAmt = ArchConvert.Obj2Double(DT_Temp.Rows[0]["UsrAmt"]);
				}
				else
				{
					rowPreBudLem["BudPccesCode"] = "";
					rowPreBudLem["BudResName"] = "";
				}
				if (CostKind != "" && UsrQty != 0.0)
				{
					rowPreBudLem["cost"] = Math.Round(UsrAmt / UsrQty, 4, MidpointRounding.AwayFromZero);
				}
				else
				{
					rowPreBudLem["cost"] = dtMrsB.Rows[i]["cost"];
				}
				dtPreBudLem.Rows.Add(rowPreBudLem);
				dtMrsB.Rows[i]["Lem_UID"] = rowParent["Lem_UID"].ToString().Trim() + "." + dtMrsB.Rows[i]["sNo"].ToString();
				dtMrsB.Rows[i]["ItemNo"] = rowPreBudLem["BudLevel"].ToString().Trim();
				dtMrsB.Rows[i]["PrintNo"] = rowPreBudLem["PrintNo"].ToString().Trim();
				dtMrsB.Rows[i]["BudItemUnit"] = rowPreBudLem["unitName"].ToString().Trim();
				dtMrsB.Rows[i]["BudItemType"] = rowPreBudLem["kind"].ToString().Trim();
				dtMrsB.Rows[i]["BudItemCost"] = rowPreBudLem["cost"].ToString().Trim();
				dtMrsB.Rows[i]["BudItemQty"] = Qty * ParentQty / ParentAnalysisQty;
				dtMrsB.Rows[i]["qty"] = dtMrsB.Rows[i]["BudItemQty"];
				dtMrsB.Rows[i]["PowerRate"] = Qty / ParentAnalysisQty;
				dtMrsB.Rows[i]["levelNo"] = Convert.ToInt32(rowPreBudLem["levelNo"].ToString().Trim()) + 1;
				dtMrsB.Rows[i]["BudItemCode"] = DT_Temp.Rows[0]["pccesCode"].ToString().Trim();
				dtMrsB.Rows[i]["FullItemNo"] = rowPreBudLem["FullItemNo"];
				dtMrsB.Rows[i]["PwrSet"] = rowPreBudLem["PwrSet"];
				dtMrsB.Rows[i]["AnalysisQty"] = rowPreBudLem["AnalysisQty"];
				Expand(F_UserID, F_ProjectCode, ItemDR, dtMrsB.Rows[i], ref dtPreBudLem, iRowIndex + i, rowPreBudLem["BudLevel"].ToString().Trim());
			}
			else
			{
				rowPreBudLem["PrintNo"] = rowParent["PrintNo"].ToString().Trim() + (i + 1).ToString().PadLeft(4, '0');
				rowPreBudLem["ItemNo"] = rowParent["ItemNo"].ToString().Trim();
				rowPreBudLem["BudAnalysis"] = dtMrsB.Rows[i]["analysis"];
				rowPreBudLem["analysis"] = dtMrsB.Rows[i]["analysis"];
				if (rowParent.Table.Columns.IndexOf("pccesCode") > -1)
				{
					rowPreBudLem["BudItemCode"] = rowParent["pccesCode"].ToString().Trim();
				}
				else
				{
					rowPreBudLem["BudItemCode"] = rowParent["BudItemCode"].ToString().Trim();
				}
				rowPreBudLem["BudItemName"] = rowParent["cName"].ToString().Trim();
				rowPreBudLem["BudItemUnit"] = rowParent["BudItemUnit"].ToString().Trim();
				rowPreBudLem["BudItemType"] = rowParent["BudItemType"].ToString().Trim();
				rowPreBudLem["BudItemCost"] = rowParent["BudItemCost"].ToString().Trim();
				double Qty = ArchConvert.Obj2Double(dtMrsB.Rows[i]["qty"]);
				double ParentQty = ArchConvert.Obj2Double(rowParent["qty"]);
				rowPreBudLem["BudItemQty"] = Qty * ParentQty / ParentAnalysisQty;
				rowPreBudLem["PowerRate"] = Qty / ParentAnalysisQty;
				rowPreBudLem["levelNo"] = rowParent["levelNo"].ToString().Trim();
				rowPreBudLem["Lem_UID"] = rowParent["Lem_UID"].ToString().Trim() + "." + dtMrsB.Rows[i]["sNo"].ToString();
				rowPreBudLem["BudListNo"] = dtMrsB.Rows[i]["listno"];
				rowPreBudLem["CName"] = rowParent["CName"];
				rowPreBudLem["unitName"] = dtMrsB.Rows[i]["unitName"];
				rowPreBudLem["qty"] = dtMrsB.Rows[i]["BudItemQty"];
				rowPreBudLem["Amount"] = dtMrsB.Rows[i]["Amount"];
				rowPreBudLem["memo"] = dtMrsB.Rows[i]["memo"];
				rowPreBudLem["ItemParentName"] = ItemDR["CName"];
				rowPreBudLem["BudLevel"] = itemNoHeader + "-" + j;
				string CostKind = "";
				double UsrAmt = 0.0;
				double UsrQty = 0.0;
				if (DT_Temp.Rows.Count > 0)
				{
					rowPreBudLem["BudPccesCode"] = DT_Temp.Rows[0]["PccesCode"];
					rowPreBudLem["BudResName"] = DT_Temp.Rows[0]["cName"];
					CostKind = ArchConvert.Obj2String(DT_Temp.Rows[0]["CostKind"]).Trim();
					UsrQty = ArchConvert.Obj2Double(DT_Temp.Rows[0]["UsrQty"]);
					UsrAmt = ArchConvert.Obj2Double(DT_Temp.Rows[0]["UsrAmt"]);
				}
				else
				{
					rowPreBudLem["BudPccesCode"] = "";
					rowPreBudLem["BudResName"] = "";
				}
				if (CostKind != "" && UsrQty != 0.0)
				{
					rowPreBudLem["cost"] = Math.Round(UsrAmt / UsrQty, 4, MidpointRounding.AwayFromZero);
				}
				else
				{
					rowPreBudLem["cost"] = dtMrsB.Rows[i]["cost"];
				}
				rowPreBudLem["usrQty"] = ModDB.DBGetValue("Select usrQty From budProjMrsA Where ProjectCode='" + F_ProjectCode + "' and pccesCode='" + rowPreBudLem["BudPccesCode"].ToString() + "'");
				rowPreBudLem["usrAmt"] = ModDB.DBGetValue("Select usrAmt From budProjMrsA Where ProjectCode='" + F_ProjectCode + "' and pccesCode='" + rowPreBudLem["BudPccesCode"].ToString() + "'");
				rowPreBudLem["sNoB"] = dtMrsB.Rows[i]["sNo"];
				rowPreBudLem["FullItemNo"] = rowParent["FullItemNo"].ToString() + "." + dtMrsB.Rows[i]["listNo"].ToString().Trim();
				rowPreBudLem["PwrSet"] = dtMrsB.Rows[i]["PwrSet"];
				rowPreBudLem["AnalysisQty"] = 0;
				dtPreBudLem.Rows.Add(rowPreBudLem);
			}
		}
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("", -1);
		Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("", -1);
		Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand3 = new Infragistics.Win.UltraWinGrid.UltraGridBand("", -1);
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.frmBudget));
		Infragistics.Win.Appearance appearance41 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance42 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance43 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance44 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance45 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance46 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance47 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel3 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance48 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel4 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance49 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance50 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance51 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance52 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance53 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance54 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance55 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance56 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance57 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance58 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance59 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance60 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance61 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance62 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance63 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance64 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.OptionSet optionSet1 = new Infragistics.Win.UltraWinToolbars.OptionSet("surName");
		Infragistics.Win.UltraWinToolbars.OptionSet optionSet2 = new Infragistics.Win.UltraWinToolbars.OptionSet("Switch");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Main");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool1 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("FileMenu");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool2 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuPopFile2");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool3 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("EditMenu");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool4 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("ViewMenu");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool5 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("DetailEditMenu");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool6 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("ToolMenu");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool7 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("BudgetChange");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool8 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("HelpMenu");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool9 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("AddOn");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar2 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Edit");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("PreviewReport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ExportFileAndReport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Cut");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Copy");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool5 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Paste");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool6 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Outdent");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool7 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Indent");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool8 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MoveUp");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool9 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MoveDown");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool10 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ReArrangeItemNo");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool11 = new Infragistics.Win.UltraWinToolbars.ButtonTool("EditMainItem");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool12 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Recalculate");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool13 = new Infragistics.Win.UltraWinToolbars.ButtonTool("AdjustTotalAmount");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool14 = new Infragistics.Win.UltraWinToolbars.ButtonTool("SetPrecision");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool15 = new Infragistics.Win.UltraWinToolbars.ButtonTool("PickItemFromMainProject");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool16 = new Infragistics.Win.UltraWinToolbars.ButtonTool("EditWorkItem");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool17 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSelfExam");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar3 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("ItemAction");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool18 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ChangeToCompanyCode");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool19 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ExportDetailList");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool1 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("ShowOnlyChangedItems", "");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool2 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuHideAmtZero", "");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool20 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ReloadFromCostEst");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool21 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ViewSourceCostQuoteProject");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool22 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ExportCostQuoteReport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool23 = new Infragistics.Win.UltraWinToolbars.ButtonTool("DeleteBudItemAZeroQtyItem");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar4 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Search");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool1 = new Infragistics.Win.UltraWinToolbars.LabelTool("SearchText");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool1 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("KeywordList");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool24 = new Infragistics.Win.UltraWinToolbars.ButtonTool("SearchKeyword");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool2 = new Infragistics.Win.UltraWinToolbars.LabelTool("LevelText");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool3 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level1", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool4 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level2", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool5 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level3", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool6 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level4", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool7 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level5", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool8 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level6", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool9 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level7", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool10 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level8", "Switch");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool3 = new Infragistics.Win.UltraWinToolbars.LabelTool("BookmarkText");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool2 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("BookmarkList");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar5 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("COMS");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool25 = new Infragistics.Win.UltraWinToolbars.ButtonTool("COMSLoadBudgetFromContract");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool26 = new Infragistics.Win.UltraWinToolbars.ButtonTool("COMSExpandBudget");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool27 = new Infragistics.Win.UltraWinToolbars.ButtonTool("COMSCheckBudgetFromContract");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool28 = new Infragistics.Win.UltraWinToolbars.ButtonTool("SingleLockEdit");
		Infragistics.Win.Appearance appearance65 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance66 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool10 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("FileMenu");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool29 = new Infragistics.Win.UltraWinToolbars.ButtonTool("SwitchProject");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool30 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ExportFileAndReport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool31 = new Infragistics.Win.UltraWinToolbars.ButtonTool("PreviewReport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool32 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ExportExecutiveBudgetChangeInfo");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool33 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ExportExecutiveBudgetSummaryReport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool34 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ExportExecutiveBudgetDetailReport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool35 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ExportBudgetCostEstAndQuoteReport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool36 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ExportBudgetDesingChangeReport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool37 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ExportBudgetAccDiffReport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool38 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ExportComsAccAlertReport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool39 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ExportTaiwanRailwayCustomizedReport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool40 = new Infragistics.Win.UltraWinToolbars.ButtonTool("EditPageBreakSetting");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool41 = new Infragistics.Win.UltraWinToolbars.ButtonTool("EditBidSetting");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool42 = new Infragistics.Win.UltraWinToolbars.ButtonTool("EditAliasSettingForReport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool43 = new Infragistics.Win.UltraWinToolbars.ButtonTool("CombineBudget");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool44 = new Infragistics.Win.UltraWinToolbars.ButtonTool("CombineBid");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool45 = new Infragistics.Win.UltraWinToolbars.ButtonTool("DeleteThisProject");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool46 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Exit");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool11 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("EditMenu");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool47 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Cut");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool48 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Copy");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool49 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Paste");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool12 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("ViewMenu");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool50 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MaintainProjectResources");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool51 = new Infragistics.Win.UltraWinToolbars.ButtonTool("EditProjectInfo");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool13 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("AliasColumnVisibility");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool14 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("DetailEditMenu");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool52 = new Infragistics.Win.UltraWinToolbars.ButtonTool("EditMainItem");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool15 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("InsertMainItem");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool16 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("InsertWorkItem");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool53 = new Infragistics.Win.UltraWinToolbars.ButtonTool("EditWorkItem");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool54 = new Infragistics.Win.UltraWinToolbars.ButtonTool("EditCostBreakdown");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool55 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MakeAmortizedItem");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool56 = new Infragistics.Win.UltraWinToolbars.ButtonTool("CancelAmortizedItem");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool57 = new Infragistics.Win.UltraWinToolbars.ButtonTool("PickItemFromMainProject");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool58 = new Infragistics.Win.UltraWinToolbars.ButtonTool("EditCustomizedVariable");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool17 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("ToolMenu");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool59 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Recalculate");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool11 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("AutoRecalculate", "");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool60 = new Infragistics.Win.UltraWinToolbars.ButtonTool("AdjustTotalAmount");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool61 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ReArrangeItemNo");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool62 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ImportMrsBaseItemName");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool18 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("ImportMrsBaseItemCost");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool19 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("ImportMrsBaseCostBreakdown");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool63 = new Infragistics.Win.UltraWinToolbars.ButtonTool("AddBookmark");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool20 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("ClearBookmark");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool64 = new Infragistics.Win.UltraWinToolbars.ButtonTool("SetPrecision");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool65 = new Infragistics.Win.UltraWinToolbars.ButtonTool("EditItemNoSetting");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool66 = new Infragistics.Win.UltraWinToolbars.ButtonTool("AutoInsertSubtotalItem");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool67 = new Infragistics.Win.UltraWinToolbars.ButtonTool("OpenMicrosoftCalculator");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool68 = new Infragistics.Win.UltraWinToolbars.ButtonTool("BackupProject");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool69 = new Infragistics.Win.UltraWinToolbars.ButtonTool("RestoreProject");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool70 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ClearDetailListCost");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool71 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ReconstructConnectionWithMrsBase");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool21 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("popup_SaveBdgt");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool22 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("popupRestoreDbgt");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool72 = new Infragistics.Win.UltraWinToolbars.ButtonTool("LoadTemplate");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool73 = new Infragistics.Win.UltraWinToolbars.ButtonTool("EditProjectOption");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool74 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ManageSnapshot");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool75 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ImportQtyFrom3rdPartyTool");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool76 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSelfExam");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool77 = new Infragistics.Win.UltraWinToolbars.ButtonTool("LockProject");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool78 = new Infragistics.Win.UltraWinToolbars.ButtonTool("UnlockProject");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool23 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("HelpMenu");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool79 = new Infragistics.Win.UltraWinToolbars.ButtonTool("AboutPcces");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool80 = new Infragistics.Win.UltraWinToolbars.ButtonTool("PccesNews");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool81 = new Infragistics.Win.UltraWinToolbars.ButtonTool("SwitchProject");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool82 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ExportFileAndReport");
		Infragistics.Win.Appearance appearance67 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool83 = new Infragistics.Win.UltraWinToolbars.ButtonTool("PreviewReport");
		Infragistics.Win.Appearance appearance68 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool84 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Exit");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool85 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Cut");
		Infragistics.Win.Appearance appearance69 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool86 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Paste");
		Infragistics.Win.Appearance appearance70 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool87 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Copy");
		Infragistics.Win.Appearance appearance71 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool88 = new Infragistics.Win.UltraWinToolbars.ButtonTool("EditMainItem");
		Infragistics.Win.Appearance appearance72 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool89 = new Infragistics.Win.UltraWinToolbars.ButtonTool("EditWorkItem");
		Infragistics.Win.Appearance appearance73 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool90 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MakeAmortizedItem");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool91 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Recalculate");
		Infragistics.Win.Appearance appearance74 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool12 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("AutoRecalculate", "");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool92 = new Infragistics.Win.UltraWinToolbars.ButtonTool("AdjustTotalAmount");
		Infragistics.Win.Appearance appearance75 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool93 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ReArrangeItemNo");
		Infragistics.Win.Appearance appearance76 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool94 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ImportMrsBaseItemName");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool95 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ImportAllMrsBaseItemCost");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool96 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ImportAllMrsBaseCostBreakdown");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool97 = new Infragistics.Win.UltraWinToolbars.ButtonTool("CombineBudget");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool98 = new Infragistics.Win.UltraWinToolbars.ButtonTool("SetPrecision");
		Infragistics.Win.Appearance appearance77 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool99 = new Infragistics.Win.UltraWinToolbars.ButtonTool("AboutPcces");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool100 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MoveUp");
		Infragistics.Win.Appearance appearance78 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool101 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MoveDown");
		Infragistics.Win.Appearance appearance79 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool102 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Outdent");
		Infragistics.Win.Appearance appearance80 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool103 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Indent");
		Infragistics.Win.Appearance appearance81 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool24 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("RightClickMenu");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool104 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Cut");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool105 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Copy");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool106 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Paste");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool25 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("InsertMainItem");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool107 = new Infragistics.Win.UltraWinToolbars.ButtonTool("EditMainItem");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool108 = new Infragistics.Win.UltraWinToolbars.ButtonTool("CloneWorkItem");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool26 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("InsertWorkItem");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool109 = new Infragistics.Win.UltraWinToolbars.ButtonTool("EditWorkItem");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool110 = new Infragistics.Win.UltraWinToolbars.ButtonTool("EditCostStructureProperty");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool111 = new Infragistics.Win.UltraWinToolbars.ButtonTool("AddBookmark");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool112 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Delete");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool113 = new Infragistics.Win.UltraWinToolbars.ButtonTool("EditCostBreakdown");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool114 = new Infragistics.Win.UltraWinToolbars.ButtonTool("LockCost");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool115 = new Infragistics.Win.UltraWinToolbars.ButtonTool("UnlockCost");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool27 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("ImportFromMrsBase");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool116 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MakeAmortizedItem");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool117 = new Infragistics.Win.UltraWinToolbars.ButtonTool("CancelAmortizedItem");
		Infragistics.Win.UltraWinToolbars.PopupControlContainerTool popupControlContainerTool1 = new Infragistics.Win.UltraWinToolbars.PopupControlContainerTool("GetSubItemQtyAmt");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool118 = new Infragistics.Win.UltraWinToolbars.ButtonTool("EditBudgetChangeResponsibility");
		Infragistics.Win.UltraWinToolbars.PopupControlContainerTool popupControlContainerTool2 = new Infragistics.Win.UltraWinToolbars.PopupControlContainerTool("ListItemChangeHistory");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool119 = new Infragistics.Win.UltraWinToolbars.ButtonTool("EditCostBreakdown");
		Infragistics.Win.Appearance appearance82 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool120 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Delete");
		Infragistics.Win.Appearance appearance83 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool121 = new Infragistics.Win.UltraWinToolbars.ButtonTool("LockCost");
		Infragistics.Win.Appearance appearance84 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool122 = new Infragistics.Win.UltraWinToolbars.ButtonTool("UnlockCost");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool28 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("InsertWorkItem");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool123 = new Infragistics.Win.UltraWinToolbars.ButtonTool("AddNewWorkItem");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool124 = new Infragistics.Win.UltraWinToolbars.ButtonTool("InsertWorkItemPickFromOtherBudget");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool125 = new Infragistics.Win.UltraWinToolbars.ButtonTool("InsertWorkItemPickFromMrsBase");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool126 = new Infragistics.Win.UltraWinToolbars.ButtonTool("AddNewWorkItem");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool127 = new Infragistics.Win.UltraWinToolbars.ButtonTool("InsertWorkItemPickFromOtherBudget");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool128 = new Infragistics.Win.UltraWinToolbars.ButtonTool("InsertWorkItemPickFromMrsBase");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool29 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("InsertMainItem");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool129 = new Infragistics.Win.UltraWinToolbars.ButtonTool("InsertMainItemSibling");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool130 = new Infragistics.Win.UltraWinToolbars.ButtonTool("InsertMainItemChildren");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool131 = new Infragistics.Win.UltraWinToolbars.ButtonTool("InsertWorkItemPickFromCostStructure");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool132 = new Infragistics.Win.UltraWinToolbars.ButtonTool("InsertMainItemSibling");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool133 = new Infragistics.Win.UltraWinToolbars.ButtonTool("InsertMainItemChildren");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool134 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MaintainProjectResources");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool135 = new Infragistics.Win.UltraWinToolbars.ButtonTool("EditProjectInfo");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool136 = new Infragistics.Win.UltraWinToolbars.ButtonTool("PickItemFromMainProject");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool137 = new Infragistics.Win.UltraWinToolbars.ButtonTool("DeleteThisProject");
		Infragistics.Win.Appearance appearance85 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool138 = new Infragistics.Win.UltraWinToolbars.ButtonTool("EditItemNoSetting");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool4 = new Infragistics.Win.UltraWinToolbars.LabelTool("SearchText");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool3 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("KeywordList");
		Infragistics.Win.ValueList valueList1 = new Infragistics.Win.ValueList(0);
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool139 = new Infragistics.Win.UltraWinToolbars.ButtonTool("SearchKeyword");
		Infragistics.Win.Appearance appearance86 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool140 = new Infragistics.Win.UltraWinToolbars.ButtonTool("CancelAmortizedItem");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool141 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ExportDetailList");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool142 = new Infragistics.Win.UltraWinToolbars.ButtonTool("OpenMicrosoftCalculator");
		Infragistics.Win.Appearance appearance87 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool30 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("AddOn");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool143 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ImportQtyFrom3rdPartyTool");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool144 = new Infragistics.Win.UltraWinToolbars.ButtonTool("EditCustomizedVariable");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool145 = new Infragistics.Win.UltraWinToolbars.ButtonTool("EditPageBreakSetting");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool146 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ExportTaiwanRailwayCustomizedReport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool147 = new Infragistics.Win.UltraWinToolbars.ButtonTool("BackupProject");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool148 = new Infragistics.Win.UltraWinToolbars.ButtonTool("RestoreProject");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool149 = new Infragistics.Win.UltraWinToolbars.ButtonTool("PccesNews");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool150 = new Infragistics.Win.UltraWinToolbars.ButtonTool("EditBidSetting");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool151 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ClearDetailListCost");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool5 = new Infragistics.Win.UltraWinToolbars.LabelTool("BookmarkText");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool4 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("BookmarkList");
		Infragistics.Win.ValueList valueList2 = new Infragistics.Win.ValueList(0);
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool152 = new Infragistics.Win.UltraWinToolbars.ButtonTool("AddBookmark");
		Infragistics.Win.Appearance appearance88 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool31 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("ClearBookmark");
		Infragistics.Win.Appearance appearance89 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool153 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ClearAllBookmark");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool154 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ClearSelectedBookmark");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool155 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ClearAllBookmark");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool156 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ClearSelectedBookmark");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool157 = new Infragistics.Win.UltraWinToolbars.ButtonTool("CombineBid");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool32 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("ImportMrsBaseItemCost");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool158 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ImportAllMrsBaseItemCost");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool159 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ImportSelectedMrsBaseItemCost");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool33 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("ImportMrsBaseCostBreakdown");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool160 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ImportAllMrsBaseCostBreakdown");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool161 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ImportSelectedMrsBaseCostBreakdown");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool162 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ImportSelectedMrsBaseItemCost");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool163 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ImportSelectedMrsBaseCostBreakdown");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool164 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ReconstructConnectionWithMrsBase");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool34 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("ImportFromMrsBase");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool165 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ImportSelectedMrsBaseItemCost");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool166 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ImportSelectedMrsBaseCostBreakdown");
		Infragistics.Win.UltraWinToolbars.PopupControlContainerTool popupControlContainerTool3 = new Infragistics.Win.UltraWinToolbars.PopupControlContainerTool("PickCostFromHistoryPrice");
		Infragistics.Win.UltraWinToolbars.PopupControlContainerTool popupControlContainerTool4 = new Infragistics.Win.UltraWinToolbars.PopupControlContainerTool("PickCostFromHistoryPrice");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool6 = new Infragistics.Win.UltraWinToolbars.LabelTool("LevelText");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool13 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level1", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool14 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level2", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool15 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level3", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool16 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level4", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool17 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level5", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool18 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level6", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool19 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level7", "Switch");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool20 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Level8", "Switch");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool167 = new Infragistics.Win.UltraWinToolbars.ButtonTool("CloneWorkItem");
		Infragistics.Win.Appearance appearance90 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool168 = new Infragistics.Win.UltraWinToolbars.ButtonTool("COMSLoadBudgetFromContract");
		Infragistics.Win.Appearance appearance91 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool35 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("RestoreSnapshot");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool169 = new Infragistics.Win.UltraWinToolbars.ButtonTool("TakeSnapshot");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool170 = new Infragistics.Win.UltraWinToolbars.ButtonTool("LoadTemplate");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool171 = new Infragistics.Win.UltraWinToolbars.ButtonTool("EditAliasSettingForReport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool172 = new Infragistics.Win.UltraWinToolbars.ButtonTool("EditProjectOption");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool36 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("AliasColumnVisibility");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool21 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("ShowAliasColumn", "surName");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool22 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("HideAliasColumn", "surName");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool23 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("ShowAliasColumn", "surName");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool24 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("HideAliasColumn", "surName");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool173 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ManageSnapshot");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool174 = new Infragistics.Win.UltraWinToolbars.ButtonTool("InsertWorkItemPickFromCostStructure");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool175 = new Infragistics.Win.UltraWinToolbars.ButtonTool("EditCostStructureProperty");
		Infragistics.Win.Appearance appearance92 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool176 = new Infragistics.Win.UltraWinToolbars.ButtonTool("LockProject");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool177 = new Infragistics.Win.UltraWinToolbars.ButtonTool("UnlockProject");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool178 = new Infragistics.Win.UltraWinToolbars.ButtonTool("COMSExpandBudget");
		Infragistics.Win.Appearance appearance93 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool179 = new Infragistics.Win.UltraWinToolbars.ButtonTool("AutoInsertSubtotalItem");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool180 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ChangeToCompanyCode");
		Infragistics.Win.UltraWinToolbars.PopupControlContainerTool popupControlContainerTool5 = new Infragistics.Win.UltraWinToolbars.PopupControlContainerTool("GetSubItemQtyAmt");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool181 = new Infragistics.Win.UltraWinToolbars.ButtonTool("AddNewBudgetChangeVersion");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool37 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("BudgetChange");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool182 = new Infragistics.Win.UltraWinToolbars.ButtonTool("AddNewBudgetChangeVersion");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool183 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ViewBudgetChangeInfo");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool184 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ViewBudgetChangeHistory");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool185 = new Infragistics.Win.UltraWinToolbars.ButtonTool("DeleteBudgetChangeVersion");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool186 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ExportDataToServer");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool187 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ViewBudgetChangeHistory");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool188 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ExportDataToServer");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool25 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("ShowOnlyChangedItems", "");
		Infragistics.Win.Appearance appearance94 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupControlContainerTool popupControlContainerTool6 = new Infragistics.Win.UltraWinToolbars.PopupControlContainerTool("ListItemChangeHistory");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool189 = new Infragistics.Win.UltraWinToolbars.ButtonTool("EditBudgetChangeResponsibility");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool190 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ReloadFromCostEst");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool191 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ViewSourceCostQuoteProject");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool192 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ExportCostQuoteReport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool193 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ExportBudgetDesingChangeReport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool194 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ExportBudgetCostEstAndQuoteReport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool195 = new Infragistics.Win.UltraWinToolbars.ButtonTool("DeleteBudItemAZeroQtyItem");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool196 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ExportExecutiveBudgetSummaryReport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool197 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ExportExecutiveBudgetDetailReport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool198 = new Infragistics.Win.UltraWinToolbars.ButtonTool("DeleteBudgetChangeVersion");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool199 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ViewBudgetChangeInfo");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool200 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ExportExecutiveBudgetChangeInfo");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool201 = new Infragistics.Win.UltraWinToolbars.ButtonTool("COMSCheckBudgetFromContract");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool202 = new Infragistics.Win.UltraWinToolbars.ButtonTool("SingleLockEdit");
		Infragistics.Win.Appearance appearance95 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool203 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ExportComsAccAlertReport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool204 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ExportBudgetAccDiffReport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool205 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSelfExam");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool26 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuHideAmtZero", "");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool38 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("popup_SaveBdgt");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool206 = new Infragistics.Win.UltraWinToolbars.ButtonTool("TakeSnapshot");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool207 = new Infragistics.Win.UltraWinToolbars.ButtonTool("TakeSnapshotCnt");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool208 = new Infragistics.Win.UltraWinToolbars.ButtonTool("TakeSnapshotCntFromBid");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool39 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("popupRestoreDbgt");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool40 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("RestoreSnapshot");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool41 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("RestoreSnapshotCnt");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool209 = new Infragistics.Win.UltraWinToolbars.ButtonTool("TakeSnapshotCnt");
		Infragistics.Win.Appearance appearance96 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool42 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("RestoreSnapshotCnt");
		Infragistics.Win.Appearance appearance97 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool43 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuPopFile2");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool210 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ExportFileAndReport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool211 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Exit");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool212 = new Infragistics.Win.UltraWinToolbars.ButtonTool("TakeSnapshotCntFromBid");
		Infragistics.Win.Appearance appearance98 = new Infragistics.Win.Appearance();
		this.cboHisPrice = new Infragistics.Win.UltraWinGrid.UltraCombo();
		this.cboSubItemQtyAmt = new Infragistics.Win.UltraWinGrid.UltraCombo();
		this.cboItemChangeHistory = new Infragistics.Win.UltraWinGrid.UltraCombo();
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
		this.panel3 = new System.Windows.Forms.Panel();
		this.gridBudget = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.panel2 = new System.Windows.Forms.Panel();
		this.BidbtnClose = new Infragistics.Win.Misc.UltraButton();
		this.lblTotal = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.axSSPanel2 = new AxThreed.AxSSPanel();
		this.statusBar = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.c = new System.Windows.Forms.Panel();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.BtnDownloadDoc = new Infragistics.Win.Misc.UltraButton();
		this.lblProjectData = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel10 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraButton2 = new Infragistics.Win.Misc.UltraButton();
		this.BtnSwitchProject = new Infragistics.Win.Misc.UltraButton();
		this.axSSPanel1 = new AxThreed.AxSSPanel();
		this.toolbarsManager = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this._frmBudget_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._frmBudget_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._frmBudget_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._frmBudget_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.iglst_splt_Btn = new System.Windows.Forms.ImageList(this.components);
		this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
		this.TM_BDGT_AutoSave = new System.Windows.Forms.Timer(this.components);
		this.tmrReCalAll = new System.Windows.Forms.Timer(this.components);
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		((System.ComponentModel.ISupportInitialize)this.cboHisPrice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubItemQtyAmt).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboItemChangeHistory).BeginInit();
		this.LeftPanel.SuspendLayout();
		this.pnl_spliter.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ssp_Lower).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Bottom).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Upper).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Top).BeginInit();
		this.MainPanel.SuspendLayout();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridBudget).BeginInit();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.axSSPanel2).BeginInit();
		this.c.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.axSSPanel1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.toolbarsManager).BeginInit();
		base.SuspendLayout();
		this.cboHisPrice.AutoEdit = false;
		this.cboHisPrice.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
		ultraGridBand1.Override.TipStyleCell = Infragistics.Win.UltraWinGrid.TipStyle.Show;
		ultraGridBand1.Override.TipStyleScroll = Infragistics.Win.UltraWinGrid.TipStyle.Show;
		ultraGridBand1.UseRowLayout = true;
		this.cboHisPrice.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
		this.cboHisPrice.DisplayMember = "";
		this.cboHisPrice.Location = new System.Drawing.Point(226, 230);
		this.cboHisPrice.MaxDropDownItems = 20;
		this.cboHisPrice.Name = "cboHisPrice";
		this.cboHisPrice.Size = new System.Drawing.Size(272, 21);
		this.cboHisPrice.TabIndex = 18;
		this.cboHisPrice.Text = "請下拉，挑選工項價格";
		this.cboHisPrice.ValueMember = "";
		this.cboHisPrice.AfterCloseUp += new System.EventHandler(cboHisPrice_AfterCloseUp);
		this.cboSubItemQtyAmt.AutoEdit = false;
		this.cboSubItemQtyAmt.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
		ultraGridBand2.Override.TipStyleCell = Infragistics.Win.UltraWinGrid.TipStyle.Show;
		ultraGridBand2.Override.TipStyleScroll = Infragistics.Win.UltraWinGrid.TipStyle.Show;
		ultraGridBand2.UseRowLayout = true;
		this.cboSubItemQtyAmt.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
		this.cboSubItemQtyAmt.DisplayMember = "";
		this.cboSubItemQtyAmt.Location = new System.Drawing.Point(226, 278);
		this.cboSubItemQtyAmt.MaxDropDownItems = 20;
		this.cboSubItemQtyAmt.Name = "cboSubItemQtyAmt";
		this.cboSubItemQtyAmt.Size = new System.Drawing.Size(272, 21);
		this.cboSubItemQtyAmt.TabIndex = 19;
		this.cboSubItemQtyAmt.Text = "請下拉，參考預算/估驗資訊";
		this.cboSubItemQtyAmt.ValueMember = "";
		this.cboItemChangeHistory.AutoEdit = false;
		this.cboItemChangeHistory.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
		ultraGridBand3.Override.TipStyleCell = Infragistics.Win.UltraWinGrid.TipStyle.Show;
		ultraGridBand3.Override.TipStyleScroll = Infragistics.Win.UltraWinGrid.TipStyle.Show;
		ultraGridBand3.UseRowLayout = true;
		this.cboItemChangeHistory.DisplayLayout.BandsSerializer.Add(ultraGridBand3);
		this.cboItemChangeHistory.DisplayMember = "";
		this.cboItemChangeHistory.Location = new System.Drawing.Point(229, 290);
		this.cboItemChangeHistory.MaxDropDownItems = 20;
		this.cboItemChangeHistory.Name = "cboItemChangeHistory";
		this.cboItemChangeHistory.Size = new System.Drawing.Size(272, 21);
		this.cboItemChangeHistory.TabIndex = 20;
		this.cboItemChangeHistory.Text = "請下拉，參考工項歷次變更紀錄";
		this.cboItemChangeHistory.ValueMember = "";
		this.LeftPanel.Controls.Add(this.onlineList1);
		this.LeftPanel.Controls.Add(this.functionButtons1);
		this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
		this.LeftPanel.Location = new System.Drawing.Point(0, 106);
		this.LeftPanel.Name = "LeftPanel";
		this.LeftPanel.Size = new System.Drawing.Size(160, 512);
		this.LeftPanel.TabIndex = 0;
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
		this.functionButtons1._UserID = "黃文正";
		this.functionButtons1._UserName = "";
		this.functionButtons1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.functionButtons1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.functionButtons1.Location = new System.Drawing.Point(0, 0);
		this.functionButtons1.Name = "functionButtons1";
		this.functionButtons1.Size = new System.Drawing.Size(160, 512);
		this.functionButtons1.TabIndex = 3;
		this.pnl_spliter.BackColor = System.Drawing.Color.LightGray;
		this.pnl_spliter.Controls.Add(this.Btn_Splt);
		this.pnl_spliter.Controls.Add(this.ssp_Lower);
		this.pnl_spliter.Controls.Add(this.ssp_Bottom);
		this.pnl_spliter.Controls.Add(this.ssp_Upper);
		this.pnl_spliter.Controls.Add(this.ssp_Top);
		this.pnl_spliter.Dock = System.Windows.Forms.DockStyle.Left;
		this.pnl_spliter.Location = new System.Drawing.Point(160, 106);
		this.pnl_spliter.Name = "pnl_spliter";
		this.pnl_spliter.Size = new System.Drawing.Size(7, 512);
		this.pnl_spliter.TabIndex = 2;
		appearance1.BorderColor = System.Drawing.Color.Transparent;
		appearance1.BorderColor3DBase = System.Drawing.Color.Transparent;
		appearance1.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance1.ImageBackground");
		this.Btn_Splt.Appearance = appearance1;
		this.Btn_Splt.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Borderless;
		this.Btn_Splt.Cursor = System.Windows.Forms.Cursors.Hand;
		this.Btn_Splt.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Btn_Splt.ImageSize = new System.Drawing.Size(7, 57);
		this.Btn_Splt.Location = new System.Drawing.Point(0, 248);
		this.Btn_Splt.Name = "Btn_Splt";
		this.Btn_Splt.ShapeImage = (System.Drawing.Image)resources.GetObject("Btn_Splt.ShapeImage");
		this.Btn_Splt.ShowFocusRect = false;
		this.Btn_Splt.ShowOutline = false;
		this.Btn_Splt.Size = new System.Drawing.Size(7, 33);
		this.Btn_Splt.TabIndex = 7;
		this.Btn_Splt.MouseLeave += new System.EventHandler(Btn_Splt_MouseLeave);
		this.Btn_Splt.Click += new System.EventHandler(Btn_Splt_Click);
		this.Btn_Splt.MouseEnter += new System.EventHandler(Btn_Splt_MouseEnter);
		this.ssp_Lower.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.ssp_Lower.Location = new System.Drawing.Point(0, 281);
		this.ssp_Lower.Name = "ssp_Lower";
		this.ssp_Lower.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Lower.OcxState");
		this.ssp_Lower.Size = new System.Drawing.Size(7, 228);
		this.ssp_Lower.TabIndex = 6;
		this.ssp_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.ssp_Bottom.Location = new System.Drawing.Point(0, 509);
		this.ssp_Bottom.Name = "ssp_Bottom";
		this.ssp_Bottom.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Bottom.OcxState");
		this.ssp_Bottom.Size = new System.Drawing.Size(7, 3);
		this.ssp_Bottom.TabIndex = 5;
		this.ssp_Upper.Dock = System.Windows.Forms.DockStyle.Top;
		this.ssp_Upper.Location = new System.Drawing.Point(0, 3);
		this.ssp_Upper.Name = "ssp_Upper";
		this.ssp_Upper.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Upper.OcxState");
		this.ssp_Upper.Size = new System.Drawing.Size(7, 245);
		this.ssp_Upper.TabIndex = 3;
		this.ssp_Top.Dock = System.Windows.Forms.DockStyle.Top;
		this.ssp_Top.Location = new System.Drawing.Point(0, 0);
		this.ssp_Top.Name = "ssp_Top";
		this.ssp_Top.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Top.OcxState");
		this.ssp_Top.Size = new System.Drawing.Size(7, 3);
		this.ssp_Top.TabIndex = 2;
		this.MainPanel.Controls.Add(this.panel3);
		this.MainPanel.Controls.Add(this.statusBar);
		this.MainPanel.Controls.Add(this.c);
		this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.MainPanel.Location = new System.Drawing.Point(167, 106);
		this.MainPanel.Name = "MainPanel";
		this.MainPanel.Size = new System.Drawing.Size(917, 512);
		this.MainPanel.TabIndex = 3;
		this.panel3.Controls.Add(this.gridBudget);
		this.panel3.Controls.Add(this.panel2);
		this.panel3.Controls.Add(this.cboHisPrice);
		this.panel3.Controls.Add(this.cboSubItemQtyAmt);
		this.panel3.Controls.Add(this.cboItemChangeHistory);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel3.Location = new System.Drawing.Point(0, 30);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(917, 456);
		this.panel3.TabIndex = 1;
		this.gridBudget._ExcelFileName = "";
		this.gridBudget._ExcelSheeName = "";
		this.gridBudget._IsOpenExcelAfterExport = false;
		this.gridBudget.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridBudget.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.gridBudget.ColumnInfo = resources.GetString("gridBudget.ColumnInfo");
		this.toolbarsManager.SetContextMenuUltra(this.gridBudget, "PopMenu1");
		this.gridBudget.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridBudget.ExtendLastCol = true;
		this.gridBudget.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridBudget.ForeColor = System.Drawing.Color.Black;
		this.gridBudget.Location = new System.Drawing.Point(0, 0);
		this.gridBudget.Name = "gridBudget";
		this.gridBudget.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.gridBudget.ShowCursor = true;
		this.gridBudget.ShowSort = false;
		this.gridBudget.ShowToolTipOnNarrowColumn = true;
		this.gridBudget.Size = new System.Drawing.Size(917, 428);
		this.gridBudget.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridBudget.Styles"));
		this.gridBudget.TabIndex = 0;
		this.gridBudget.Tree.Column = 1;
		this.gridBudget.Tree.LineColor = System.Drawing.Color.Gray;
		this.gridBudget.Click += new System.EventHandler(gridBudget1_Click);
		this.gridBudget.AfterSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(gridBudget1_AfterSelChange);
		this.gridBudget.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(gridBudget1_StartEdit);
		this.gridBudget.BeforeMouseDown += new C1.Win.C1FlexGrid.BeforeMouseDownEventHandler(gridBudget1_BeforeMouseDown);
		this.gridBudget.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(gridBudget1_AfterEdit);
		this.gridBudget.KeyDown += new System.Windows.Forms.KeyEventHandler(gridBudget1_KeyDown);
		this.gridBudget.MouseDown += new System.Windows.Forms.MouseEventHandler(gridBudget1_MouseDown);
		this.gridBudget.Resize += new System.EventHandler(gridBudget_Resize);
		this.gridBudget.MouseUp += new System.Windows.Forms.MouseEventHandler(gridBudget1_MouseUp);
		this.gridBudget.AfterCollapse += new C1.Win.C1FlexGrid.RowColEventHandler(gridBudget1_AfterCollapse);
		this.gridBudget.MouseMove += new System.Windows.Forms.MouseEventHandler(gridBudget1_MouseMove);
		this.gridBudget.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(gridBudget1_BeforeEdit);
		this.gridBudget.DoubleClick += new System.EventHandler(gridBudget1_DoubleClick);
		this.panel2.Controls.Add(this.BidbtnClose);
		this.panel2.Controls.Add(this.lblTotal);
		this.panel2.Controls.Add(this.ultraLabel2);
		this.panel2.Controls.Add(this.axSSPanel2);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 428);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(917, 28);
		this.panel2.TabIndex = 1;
		this.BidbtnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance41.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.BidbtnClose.Appearance = appearance41;
		this.BidbtnClose.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.BidbtnClose.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.BidbtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.BidbtnClose.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance42.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance42.BackColor2 = System.Drawing.Color.White;
		appearance42.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.BidbtnClose.HotTrackAppearance = appearance42;
		this.BidbtnClose.HotTracking = true;
		this.BidbtnClose.Location = new System.Drawing.Point(818, 3);
		this.BidbtnClose.Name = "BidbtnClose";
		this.BidbtnClose.ShowFocusRect = false;
		this.BidbtnClose.ShowOutline = false;
		this.BidbtnClose.Size = new System.Drawing.Size(88, 24);
		this.BidbtnClose.SupportThemes = false;
		this.BidbtnClose.TabIndex = 15;
		this.BidbtnClose.Text = "返回標單作業";
		this.BidbtnClose.Visible = false;
		this.BidbtnClose.Click += new System.EventHandler(BidbtnClose_Click);
		this.lblTotal.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance43.ForeColor = System.Drawing.Color.Blue;
		appearance43.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance43.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblTotal.Appearance = appearance43;
		this.lblTotal.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.lblTotal.Font = new System.Drawing.Font("Courier New", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblTotal.Location = new System.Drawing.Point(64, 5);
		this.lblTotal.Name = "lblTotal";
		this.lblTotal.Size = new System.Drawing.Size(762, 19);
		this.lblTotal.TabIndex = 14;
		appearance44.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		appearance44.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel2.Appearance = appearance44;
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
		this.axSSPanel2.Size = new System.Drawing.Size(917, 28);
		this.axSSPanel2.TabIndex = 1;
		appearance45.FontData.SizeInPoints = 11f;
		appearance45.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.statusBar.Appearance = appearance45;
		this.statusBar.Location = new System.Drawing.Point(0, 486);
		this.statusBar.Name = "statusBar";
		appearance46.TextVAlign = Infragistics.Win.VAlign.Bottom;
		ultraStatusPanel1.Appearance = appearance46;
		ultraStatusPanel1.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel1.Key = "RowsCount";
		ultraStatusPanel1.Text = "資料筆數：";
		ultraStatusPanel1.Width = 180;
		ultraStatusPanel2.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel2.Key = "ProgressBar";
		appearance47.BackColor = System.Drawing.Color.FromArgb(192, 192, 255);
		appearance47.BackColor2 = System.Drawing.Color.Navy;
		appearance47.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
		ultraStatusPanel2.ProgressBarInfo.Appearance = appearance47;
		ultraStatusPanel2.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		appearance48.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance48.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		ultraStatusPanel3.Appearance = appearance48;
		ultraStatusPanel3.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel3.Width = 150;
		appearance49.TextHAlign = Infragistics.Win.HAlign.Right;
		ultraStatusPanel4.Appearance = appearance49;
		ultraStatusPanel4.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel4.Text = "客服電話:(02)2708-8090";
		ultraStatusPanel4.Width = 200;
		this.statusBar.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[4] { ultraStatusPanel1, ultraStatusPanel2, ultraStatusPanel3, ultraStatusPanel4 });
		this.statusBar.Size = new System.Drawing.Size(917, 26);
		this.statusBar.SupportThemes = false;
		this.statusBar.TabIndex = 3;
		this.statusBar.Text = "ultraStatusBar1";
		this.statusBar.PanelClick += new Infragistics.Win.UltraWinStatusBar.PanelClickEventHandler(ultraStatusBar1_PanelClick);
		this.c.Controls.Add(this.ultraButton1);
		this.c.Controls.Add(this.BtnDownloadDoc);
		this.c.Controls.Add(this.lblProjectData);
		this.c.Controls.Add(this.ultraLabel10);
		this.c.Controls.Add(this.ultraButton2);
		this.c.Controls.Add(this.BtnSwitchProject);
		this.c.Controls.Add(this.axSSPanel1);
		this.c.Dock = System.Windows.Forms.DockStyle.Top;
		this.c.Location = new System.Drawing.Point(0, 0);
		this.c.Name = "c";
		this.c.Size = new System.Drawing.Size(917, 30);
		this.c.TabIndex = 0;
		this.ultraButton1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance50.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraButton1.Appearance = appearance50;
		this.ultraButton1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.ultraButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ultraButton1.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance51.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance51.BackColor2 = System.Drawing.Color.White;
		appearance51.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.ultraButton1.HotTrackAppearance = appearance51;
		this.ultraButton1.HotTracking = true;
		this.ultraButton1.Location = new System.Drawing.Point(588, 4);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.ShowFocusRect = false;
		this.ultraButton1.ShowOutline = false;
		this.ultraButton1.Size = new System.Drawing.Size(76, 24);
		this.ultraButton1.SupportThemes = false;
		this.ultraButton1.TabIndex = 16;
		this.ultraButton1.Text = "test Call";
		this.ultraButton1.Visible = false;
		this.ultraButton1.Click += new System.EventHandler(ultraButton1_Click_1);
		this.BtnDownloadDoc.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance52.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.BtnDownloadDoc.Appearance = appearance52;
		this.BtnDownloadDoc.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.BtnDownloadDoc.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.BtnDownloadDoc.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.BtnDownloadDoc.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance53.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance53.BackColor2 = System.Drawing.Color.White;
		appearance53.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.BtnDownloadDoc.HotTrackAppearance = appearance53;
		this.BtnDownloadDoc.HotTracking = true;
		this.BtnDownloadDoc.Location = new System.Drawing.Point(670, 4);
		this.BtnDownloadDoc.Name = "BtnDownloadDoc";
		this.BtnDownloadDoc.ShowFocusRect = false;
		this.BtnDownloadDoc.ShowOutline = false;
		this.BtnDownloadDoc.Size = new System.Drawing.Size(82, 24);
		this.BtnDownloadDoc.SupportThemes = false;
		this.BtnDownloadDoc.TabIndex = 15;
		this.BtnDownloadDoc.Text = "綱要規範下載";
		this.BtnDownloadDoc.Click += new System.EventHandler(BtnDownloadDoc_Click);
		this.lblProjectData.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance54.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance54.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblProjectData.Appearance = appearance54;
		this.lblProjectData.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.lblProjectData.Font = new System.Drawing.Font("細明體", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblProjectData.Location = new System.Drawing.Point(80, 2);
		this.lblProjectData.Name = "lblProjectData";
		this.lblProjectData.Size = new System.Drawing.Size(632, 26);
		this.lblProjectData.TabIndex = 14;
		appearance55.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel10.Appearance = appearance55;
		this.ultraLabel10.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.ultraLabel10.Font = new System.Drawing.Font("細明體", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel10.Location = new System.Drawing.Point(4, 8);
		this.ultraLabel10.Name = "ultraLabel10";
		this.ultraLabel10.Size = new System.Drawing.Size(74, 19);
		this.ultraLabel10.TabIndex = 13;
		this.ultraLabel10.Text = "目前專案：";
		this.ultraButton2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance56.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraButton2.Appearance = appearance56;
		this.ultraButton2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraButton2.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.ultraButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ultraButton2.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance57.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance57.BackColor2 = System.Drawing.Color.White;
		appearance57.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.ultraButton2.HotTrackAppearance = appearance57;
		this.ultraButton2.HotTracking = true;
		this.ultraButton2.Location = new System.Drawing.Point(758, 4);
		this.ultraButton2.Name = "ultraButton2";
		this.ultraButton2.ShowFocusRect = false;
		this.ultraButton2.ShowOutline = false;
		this.ultraButton2.Size = new System.Drawing.Size(76, 24);
		this.ultraButton2.SupportThemes = false;
		this.ultraButton2.TabIndex = 12;
		this.ultraButton2.Text = "預算資訊";
		this.ultraButton2.Click += new System.EventHandler(ultraButton2_Click_1);
		this.BtnSwitchProject.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance58.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.BtnSwitchProject.Appearance = appearance58;
		this.BtnSwitchProject.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.BtnSwitchProject.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.BtnSwitchProject.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.BtnSwitchProject.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance59.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance59.BackColor2 = System.Drawing.Color.White;
		appearance59.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.BtnSwitchProject.HotTrackAppearance = appearance59;
		this.BtnSwitchProject.HotTracking = true;
		this.BtnSwitchProject.Location = new System.Drawing.Point(837, 4);
		this.BtnSwitchProject.Name = "BtnSwitchProject";
		this.BtnSwitchProject.ShowFocusRect = false;
		this.BtnSwitchProject.ShowOutline = false;
		this.BtnSwitchProject.Size = new System.Drawing.Size(76, 24);
		this.BtnSwitchProject.SupportThemes = false;
		this.BtnSwitchProject.TabIndex = 11;
		this.BtnSwitchProject.Text = "切換專案";
		this.BtnSwitchProject.Click += new System.EventHandler(BtnSwitchProject_Click_1);
		this.axSSPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.axSSPanel1.Location = new System.Drawing.Point(0, 0);
		this.axSSPanel1.Name = "axSSPanel1";
		this.axSSPanel1.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("axSSPanel1.OcxState");
		this.axSSPanel1.Size = new System.Drawing.Size(917, 30);
		this.axSSPanel1.TabIndex = 1;
		appearance60.FontData.Name = "Arial";
		appearance60.FontData.SizeInPoints = 9f;
		this.toolbarsManager.Appearance = appearance60;
		appearance61.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance61.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.toolbarsManager.DockAreaAppearance = appearance61;
		this.toolbarsManager.DockWithinContainer = this;
		this.toolbarsManager.ImageListSmall = this.imageList1;
		this.toolbarsManager.ImageTransparentColor = System.Drawing.Color.White;
		this.toolbarsManager.LockToolbars = true;
		appearance62.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance62.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance62.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.toolbarsManager.MenuSettings.HotTrackAppearance = appearance62;
		appearance63.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance63.BackColor2 = System.Drawing.Color.FromArgb(237, 243, 254);
		this.toolbarsManager.MenuSettings.IconAreaAppearance = appearance63;
		appearance64.BackColor = System.Drawing.Color.White;
		appearance64.BackColor2 = System.Drawing.Color.White;
		this.toolbarsManager.MenuSettings.ToolAppearance = appearance64;
		optionSet1.AllowAllUp = false;
		optionSet2.AllowAllUp = false;
		this.toolbarsManager.OptionSets.Add(optionSet1);
		this.toolbarsManager.OptionSets.Add(optionSet2);
		this.toolbarsManager.ShowFullMenusDelay = 500;
		this.toolbarsManager.ShowQuickCustomizeButton = false;
		this.toolbarsManager.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2003;
		ultraToolbar1.DockedColumn = 0;
		ultraToolbar1.DockedRow = 0;
		ultraToolbar1.IsMainMenuBar = true;
		ultraToolbar1.Settings.AllowCustomize = Infragistics.Win.DefaultableBoolean.False;
		ultraToolbar1.Text = "功能選單";
		ultraToolbar1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[9] { popupMenuTool1, popupMenuTool2, popupMenuTool3, popupMenuTool4, popupMenuTool5, popupMenuTool6, popupMenuTool7, popupMenuTool8, popupMenuTool9 });
		ultraToolbar2.DockedColumn = 0;
		ultraToolbar2.DockedRow = 1;
		ultraToolbar2.Text = "編輯";
		buttonTool1.InstanceProps.IsFirstInGroup = true;
		buttonTool3.InstanceProps.IsFirstInGroup = true;
		buttonTool7.InstanceProps.IsFirstInGroup = true;
		buttonTool8.InstanceProps.IsFirstInGroup = true;
		buttonTool12.InstanceProps.IsFirstInGroup = true;
		buttonTool15.InstanceProps.IsFirstInGroup = true;
		buttonTool16.InstanceProps.IsFirstInGroup = true;
		buttonTool17.InstanceProps.IsFirstInGroup = true;
		ultraToolbar2.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[17]
		{
			buttonTool1, buttonTool2, buttonTool3, buttonTool4, buttonTool5, buttonTool6, buttonTool7, buttonTool8, buttonTool9, buttonTool10,
			buttonTool11, buttonTool12, buttonTool13, buttonTool14, buttonTool15, buttonTool16, buttonTool17
		});
		ultraToolbar3.DockedColumn = 0;
		ultraToolbar3.DockedRow = 2;
		ultraToolbar3.Text = "項目動作";
		buttonTool18.InstanceProps.IsFirstInGroup = true;
		buttonTool19.InstanceProps.IsFirstInGroup = true;
		stateButtonTool1.InstanceProps.IsFirstInGroup = true;
		stateButtonTool2.InstanceProps.IsFirstInGroup = true;
		buttonTool20.InstanceProps.IsFirstInGroup = true;
		buttonTool21.InstanceProps.IsFirstInGroup = true;
		ultraToolbar3.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[8] { buttonTool18, buttonTool19, stateButtonTool1, stateButtonTool2, buttonTool20, buttonTool21, buttonTool22, buttonTool23 });
		ultraToolbar4.DockedColumn = 0;
		ultraToolbar4.DockedRow = 3;
		ultraToolbar4.Text = "尋找";
		labelTool2.InstanceProps.IsFirstInGroup = true;
		stateButtonTool3.Checked = true;
		labelTool3.InstanceProps.IsFirstInGroup = true;
		comboBoxTool2.InstanceProps.Width = 300;
		ultraToolbar4.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[14]
		{
			labelTool1, comboBoxTool1, buttonTool24, labelTool2, stateButtonTool3, stateButtonTool4, stateButtonTool5, stateButtonTool6, stateButtonTool7, stateButtonTool8,
			stateButtonTool9, stateButtonTool10, labelTool3, comboBoxTool2
		});
		ultraToolbar5.DockedColumn = 1;
		ultraToolbar5.DockedRow = 2;
		ultraToolbar5.FloatingLocation = new System.Drawing.Point(292, 345);
		ultraToolbar5.FloatingSize = new System.Drawing.Size(161, 26);
		ultraToolbar5.Text = "COMS 工具列";
		buttonTool25.InstanceProps.IsFirstInGroup = true;
		ultraToolbar5.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[4] { buttonTool25, buttonTool26, buttonTool27, buttonTool28 });
		this.toolbarsManager.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[5] { ultraToolbar1, ultraToolbar2, ultraToolbar3, ultraToolbar4, ultraToolbar5 });
		this.toolbarsManager.ToolbarSettings.AllowCustomize = Infragistics.Win.DefaultableBoolean.False;
		appearance65.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance65.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.toolbarsManager.ToolbarSettings.Appearance = appearance65;
		appearance66.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance66.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance66.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.toolbarsManager.ToolbarSettings.HotTrackAppearance = appearance66;
		popupMenuTool10.SharedProps.Caption = "檔案(&F)";
		popupMenuTool10.SharedProps.Category = "檔案";
		buttonTool30.InstanceProps.IsFirstInGroup = true;
		buttonTool41.InstanceProps.IsFirstInGroup = true;
		buttonTool43.InstanceProps.IsFirstInGroup = true;
		buttonTool46.InstanceProps.IsFirstInGroup = true;
		popupMenuTool10.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[18]
		{
			buttonTool29, buttonTool30, buttonTool31, buttonTool32, buttonTool33, buttonTool34, buttonTool35, buttonTool36, buttonTool37, buttonTool38,
			buttonTool39, buttonTool40, buttonTool41, buttonTool42, buttonTool43, buttonTool44, buttonTool45, buttonTool46
		});
		popupMenuTool11.SharedProps.Caption = "編輯(&E)";
		popupMenuTool11.SharedProps.Category = "編輯";
		buttonTool47.InstanceProps.IsFirstInGroup = true;
		popupMenuTool11.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[3] { buttonTool47, buttonTool48, buttonTool49 });
		popupMenuTool12.SharedProps.Caption = "檢視(&V)";
		popupMenuTool12.SharedProps.Category = "檢視";
		buttonTool51.InstanceProps.IsFirstInGroup = true;
		popupMenuTool13.InstanceProps.IsFirstInGroup = true;
		popupMenuTool12.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[3] { buttonTool50, buttonTool51, popupMenuTool13 });
		popupMenuTool14.SharedProps.Caption = "詳細表編輯(&D)";
		popupMenuTool14.SharedProps.Category = "詳細表編輯";
		popupMenuTool16.InstanceProps.IsFirstInGroup = true;
		buttonTool54.InstanceProps.IsFirstInGroup = true;
		buttonTool55.InstanceProps.IsFirstInGroup = true;
		buttonTool57.InstanceProps.IsFirstInGroup = true;
		buttonTool58.InstanceProps.IsFirstInGroup = true;
		popupMenuTool14.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[9] { buttonTool52, popupMenuTool15, popupMenuTool16, buttonTool53, buttonTool54, buttonTool55, buttonTool56, buttonTool57, buttonTool58 });
		popupMenuTool17.SharedProps.Caption = "工具(&T)";
		popupMenuTool17.SharedProps.Category = "工具";
		stateButtonTool11.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		buttonTool61.InstanceProps.IsFirstInGroup = true;
		buttonTool63.InstanceProps.IsFirstInGroup = true;
		buttonTool64.InstanceProps.IsFirstInGroup = true;
		buttonTool65.InstanceProps.IsFirstInGroup = true;
		buttonTool66.InstanceProps.IsFirstInGroup = true;
		buttonTool67.InstanceProps.IsFirstInGroup = true;
		buttonTool68.InstanceProps.IsFirstInGroup = true;
		buttonTool70.InstanceProps.IsFirstInGroup = true;
		popupMenuTool22.InstanceProps.IsFirstInGroup = true;
		buttonTool72.InstanceProps.IsFirstInGroup = true;
		buttonTool75.InstanceProps.IsFirstInGroup = true;
		buttonTool76.InstanceProps.IsFirstInGroup = true;
		buttonTool77.InstanceProps.IsFirstInGroup = true;
		popupMenuTool17.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[26]
		{
			buttonTool59, stateButtonTool11, buttonTool60, buttonTool61, buttonTool62, popupMenuTool18, popupMenuTool19, buttonTool63, popupMenuTool20, buttonTool64,
			buttonTool65, buttonTool66, buttonTool67, buttonTool68, buttonTool69, buttonTool70, buttonTool71, popupMenuTool21, popupMenuTool22, buttonTool72,
			buttonTool73, buttonTool74, buttonTool75, buttonTool76, buttonTool77, buttonTool78
		});
		popupMenuTool23.SharedProps.Caption = "說明(&H)";
		popupMenuTool23.SharedProps.Category = "說明";
		buttonTool79.InstanceProps.IsFirstInGroup = true;
		buttonTool80.InstanceProps.IsFirstInGroup = true;
		popupMenuTool23.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[2] { buttonTool79, buttonTool80 });
		buttonTool81.SharedProps.Caption = "切換專案...";
		buttonTool81.SharedProps.Category = "檔案";
		appearance67.Image = resources.GetObject("appearance9.Image");
		buttonTool82.SharedProps.AppearancesSmall.Appearance = appearance67;
		buttonTool82.SharedProps.Caption = "製作電子檔及列印報表...";
		buttonTool82.SharedProps.Category = "檔案";
		appearance68.Image = resources.GetObject("appearance10.Image");
		buttonTool83.SharedProps.AppearancesSmall.Appearance = appearance68;
		buttonTool83.SharedProps.Caption = "報表預覽...";
		buttonTool83.SharedProps.Category = "檔案";
		buttonTool84.SharedProps.Caption = "結束預算書編輯(&X)";
		buttonTool84.SharedProps.Category = "檔案";
		appearance69.Image = resources.GetObject("appearance11.Image");
		buttonTool85.SharedProps.AppearancesSmall.Appearance = appearance69;
		buttonTool85.SharedProps.Caption = "剪下";
		buttonTool85.SharedProps.Category = "編輯";
		appearance70.Image = resources.GetObject("appearance12.Image");
		buttonTool86.SharedProps.AppearancesSmall.Appearance = appearance70;
		buttonTool86.SharedProps.Caption = "貼上";
		buttonTool86.SharedProps.Category = "編輯";
		appearance71.Image = resources.GetObject("appearance13.Image");
		buttonTool87.SharedProps.AppearancesSmall.Appearance = appearance71;
		buttonTool87.SharedProps.Caption = "複製";
		buttonTool87.SharedProps.Category = "編輯";
		appearance72.Image = resources.GetObject("appearance14.Image");
		buttonTool88.SharedProps.AppearancesSmall.Appearance = appearance72;
		buttonTool88.SharedProps.Caption = "編輯主項大類...";
		buttonTool88.SharedProps.Category = "詳細表編輯";
		appearance73.Image = resources.GetObject("appearance15.Image");
		buttonTool89.SharedProps.AppearancesSmall.Appearance = appearance73;
		buttonTool89.SharedProps.Caption = "編輯工作要項...";
		buttonTool89.SharedProps.Category = "詳細表編輯";
		buttonTool90.SharedProps.Caption = "設為攤提項目";
		buttonTool90.SharedProps.Category = "詳細表編輯";
		appearance74.Image = resources.GetObject("appearance16.Image");
		buttonTool91.SharedProps.AppearancesSmall.Appearance = appearance74;
		buttonTool91.SharedProps.Caption = "重新總計";
		buttonTool91.SharedProps.Category = "工具";
		buttonTool91.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		stateButtonTool12.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool12.SharedProps.Caption = "自動重新總計";
		stateButtonTool12.SharedProps.Category = "工具";
		appearance75.Image = resources.GetObject("appearance17.Image");
		buttonTool92.SharedProps.AppearancesSmall.Appearance = appearance75;
		buttonTool92.SharedProps.Caption = "總價調整...";
		buttonTool92.SharedProps.Category = "工具";
		appearance76.Image = resources.GetObject("appearance18.Image");
		buttonTool93.SharedProps.AppearancesSmall.Appearance = appearance76;
		buttonTool93.SharedProps.Caption = "項次重整...";
		buttonTool93.SharedProps.Category = "工具";
		buttonTool94.SharedProps.Caption = "名稱重整...";
		buttonTool94.SharedProps.Category = "工具";
		buttonTool95.SharedProps.Caption = "所有工料單價";
		buttonTool95.SharedProps.Category = "工具";
		buttonTool96.SharedProps.Caption = "所有工料的單價分析";
		buttonTool96.SharedProps.Category = "工具";
		buttonTool97.SharedProps.Caption = "併標...";
		buttonTool97.SharedProps.Category = "工具";
		appearance77.Image = resources.GetObject("appearance19.Image");
		buttonTool98.SharedProps.AppearancesSmall.Appearance = appearance77;
		buttonTool98.SharedProps.Caption = "小數位數設定...";
		buttonTool98.SharedProps.Category = "工具";
		buttonTool99.SharedProps.Caption = "關於 PCCES...";
		buttonTool99.SharedProps.Category = "說明";
		appearance78.Image = resources.GetObject("appearance20.Image");
		buttonTool100.SharedProps.AppearancesSmall.Appearance = appearance78;
		buttonTool100.SharedProps.Caption = "上移";
		buttonTool100.SharedProps.Category = "動作";
		appearance79.Image = resources.GetObject("appearance21.Image");
		buttonTool101.SharedProps.AppearancesSmall.Appearance = appearance79;
		buttonTool101.SharedProps.Caption = "下移";
		buttonTool101.SharedProps.Category = "動作";
		appearance80.Image = resources.GetObject("appearance22.Image");
		buttonTool102.SharedProps.AppearancesSmall.Appearance = appearance80;
		buttonTool102.SharedProps.Caption = "凸排";
		buttonTool102.SharedProps.Category = "動作";
		appearance81.Image = resources.GetObject("appearance23.Image");
		buttonTool103.SharedProps.AppearancesSmall.Appearance = appearance81;
		buttonTool103.SharedProps.Caption = "縮排";
		buttonTool103.SharedProps.Category = "動作";
		buttonTool103.SharedProps.Shortcut = System.Windows.Forms.Shortcut.CtrlQ;
		popupMenuTool24.SharedProps.Caption = "預算書右鍵選單";
		popupMenuTool24.SharedProps.Category = "右鍵選單";
		popupMenuTool25.InstanceProps.IsFirstInGroup = true;
		popupMenuTool26.InstanceProps.IsFirstInGroup = true;
		buttonTool111.InstanceProps.IsFirstInGroup = true;
		buttonTool112.InstanceProps.IsFirstInGroup = true;
		buttonTool113.InstanceProps.IsFirstInGroup = true;
		buttonTool114.InstanceProps.IsFirstInGroup = true;
		popupMenuTool27.InstanceProps.IsFirstInGroup = true;
		buttonTool116.InstanceProps.IsFirstInGroup = true;
		popupControlContainerTool1.InstanceProps.IsFirstInGroup = true;
		buttonTool118.InstanceProps.IsFirstInGroup = true;
		popupMenuTool24.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[20]
		{
			buttonTool104, buttonTool105, buttonTool106, popupMenuTool25, buttonTool107, buttonTool108, popupMenuTool26, buttonTool109, buttonTool110, buttonTool111,
			buttonTool112, buttonTool113, buttonTool114, buttonTool115, popupMenuTool27, buttonTool116, buttonTool117, popupControlContainerTool1, buttonTool118, popupControlContainerTool2
		});
		appearance82.Image = resources.GetObject("appearance24.Image");
		buttonTool119.SharedProps.AppearancesSmall.Appearance = appearance82;
		buttonTool119.SharedProps.Caption = "單價分析 (Alt + Z)";
		buttonTool119.SharedProps.Category = "詳細表編輯";
		appearance83.Image = resources.GetObject("appearance25.Image");
		buttonTool120.SharedProps.AppearancesSmall.Appearance = appearance83;
		buttonTool120.SharedProps.Caption = "刪除";
		buttonTool120.SharedProps.Category = "右鍵選單";
		buttonTool120.SharedProps.Shortcut = System.Windows.Forms.Shortcut.Del;
		appearance84.Image = resources.GetObject("appearance26.Image");
		buttonTool121.SharedProps.AppearancesSmall.Appearance = appearance84;
		buttonTool121.SharedProps.Caption = "單價鎖定";
		buttonTool121.SharedProps.Category = "右鍵選單";
		buttonTool122.SharedProps.Caption = "取消單價鎖定";
		buttonTool122.SharedProps.Category = "右鍵選單";
		popupMenuTool28.SharedProps.Caption = "插入工作要項";
		buttonTool124.InstanceProps.IsFirstInGroup = true;
		popupMenuTool28.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[3] { buttonTool123, buttonTool124, buttonTool125 });
		buttonTool126.SharedProps.Caption = "新增工作要項";
		buttonTool127.SharedProps.Caption = "自專案挑選工項";
		buttonTool128.SharedProps.Caption = "自基本資料庫挑選工項";
		popupMenuTool29.SharedProps.Caption = "插入主項大類";
		popupMenuTool29.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[3] { buttonTool129, buttonTool130, buttonTool131 });
		buttonTool132.SharedProps.Caption = "插入同階項目";
		buttonTool133.SharedProps.Caption = "插入子階項目";
		buttonTool134.SharedProps.Caption = "專案工項維護";
		buttonTool134.SharedProps.Category = "檢視";
		buttonTool135.SharedProps.Caption = "預算資訊";
		buttonTool135.SharedProps.Category = "檢視";
		buttonTool136.SharedProps.Caption = "由主專案挑選";
		buttonTool136.SharedProps.Category = "詳細表編輯";
		buttonTool136.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		appearance85.Image = resources.GetObject("appearance27.Image");
		buttonTool137.SharedProps.AppearancesSmall.Appearance = appearance85;
		buttonTool137.SharedProps.Caption = "刪除此預算書";
		buttonTool137.SharedProps.Category = "檔案";
		buttonTool138.SharedProps.Caption = "項次編號設定...";
		labelTool4.SharedProps.Caption = "尋找：";
		labelTool4.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		comboBoxTool3.DropDownStyle = Infragistics.Win.DropDownStyle.DropDown;
		comboBoxTool3.SharedProps.Caption = "輸入關鍵字";
		comboBoxTool3.SharedProps.Width = 200;
		comboBoxTool3.ValueList = valueList1;
		appearance86.Image = resources.GetObject("appearance28.Image");
		buttonTool139.SharedProps.AppearancesSmall.Appearance = appearance86;
		buttonTool139.SharedProps.Caption = "執行尋找";
		buttonTool140.SharedProps.Caption = "取消攤提";
		buttonTool141.SharedProps.Caption = "匯出";
		buttonTool141.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool141.SharedProps.Visible = false;
		appearance87.Image = resources.GetObject("appearance29.Image");
		buttonTool142.SharedProps.AppearancesSmall.Appearance = appearance87;
		buttonTool142.SharedProps.Caption = "計算機";
		buttonTool142.SharedProps.Category = "工具";
		popupMenuTool30.SharedProps.Caption = "附加工具(&A)";
		popupMenuTool30.SharedProps.Visible = false;
		buttonTool143.SharedProps.Caption = "3rd Party 數量轉入";
		buttonTool143.SharedProps.Category = "工具";
		buttonTool144.SharedProps.Caption = "自訂變數項...";
		buttonTool144.SharedProps.Category = "詳細表編輯";
		buttonTool145.SharedProps.Caption = "報表跳頁設定...";
		buttonTool146.SharedProps.Caption = "製作材料處發包專用預算書...";
		buttonTool147.SharedProps.Caption = "備份...";
		buttonTool147.SharedProps.Category = "工具";
		buttonTool147.SharedProps.Shortcut = System.Windows.Forms.Shortcut.Alt8;
		buttonTool148.SharedProps.Caption = "回存...";
		buttonTool148.SharedProps.Category = "工具";
		buttonTool148.SharedProps.Shortcut = System.Windows.Forms.Shortcut.Alt9;
		buttonTool149.SharedProps.Caption = "最新消息...";
		buttonTool150.SharedProps.Caption = "發包設定...";
		buttonTool151.SharedProps.Caption = "清空詳細表單價";
		labelTool5.SharedProps.Caption = "書籤：";
		labelTool5.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		comboBoxTool4.SharedProps.Caption = "書籤下拉";
		comboBoxTool4.SharedProps.Width = 300;
		comboBoxTool4.ValueList = valueList2;
		appearance88.Image = resources.GetObject("appearance30.Image");
		buttonTool152.SharedProps.AppearancesSmall.Appearance = appearance88;
		buttonTool152.SharedProps.Caption = "加入書籤";
		appearance89.Image = resources.GetObject("appearance31.Image");
		popupMenuTool31.SharedProps.AppearancesSmall.Appearance = appearance89;
		popupMenuTool31.SharedProps.Caption = "清空書籤";
		popupMenuTool31.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[2] { buttonTool153, buttonTool154 });
		buttonTool155.SharedProps.Caption = "全部";
		buttonTool156.SharedProps.Caption = "指定項目...";
		buttonTool157.SharedProps.Caption = "標單併標...";
		popupMenuTool32.SharedProps.Caption = "引用單價";
		popupMenuTool32.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[2] { buttonTool158, buttonTool159 });
		popupMenuTool33.SharedProps.Caption = "引用單價分析及其單價";
		popupMenuTool33.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[2] { buttonTool160, buttonTool161 });
		buttonTool162.SharedProps.Caption = "選取項的工料單價";
		buttonTool163.SharedProps.Caption = "選取項的單價分析";
		buttonTool164.SharedProps.Caption = "資料庫重整";
		popupMenuTool34.SharedProps.Caption = "引用";
		popupControlContainerTool3.InstanceProps.IsFirstInGroup = true;
		popupMenuTool34.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[3] { buttonTool165, buttonTool166, popupControlContainerTool3 });
		popupControlContainerTool4.Control = this.cboHisPrice;
		popupControlContainerTool4.SharedProps.Caption = "單價挑用";
		labelTool6.SharedProps.Caption = "階層：";
		labelTool6.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool13.Checked = true;
		stateButtonTool13.OptionSetKey = "Switch";
		stateButtonTool13.SharedProps.Caption = "1";
		stateButtonTool13.SharedProps.Category = "LevelSwitch";
		stateButtonTool13.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool14.OptionSetKey = "Switch";
		stateButtonTool14.SharedProps.Caption = "2";
		stateButtonTool14.SharedProps.Category = "LevelSwitch";
		stateButtonTool14.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool15.OptionSetKey = "Switch";
		stateButtonTool15.SharedProps.Caption = "3";
		stateButtonTool15.SharedProps.Category = "LevelSwitch";
		stateButtonTool15.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool16.OptionSetKey = "Switch";
		stateButtonTool16.SharedProps.Caption = "4";
		stateButtonTool16.SharedProps.Category = "LevelSwitch";
		stateButtonTool16.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool17.OptionSetKey = "Switch";
		stateButtonTool17.SharedProps.Caption = "5";
		stateButtonTool17.SharedProps.Category = "LevelSwitch";
		stateButtonTool17.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool18.OptionSetKey = "Switch";
		stateButtonTool18.SharedProps.Caption = "6";
		stateButtonTool18.SharedProps.Category = "LevelSwitch";
		stateButtonTool18.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool19.OptionSetKey = "Switch";
		stateButtonTool19.SharedProps.Caption = "7";
		stateButtonTool19.SharedProps.Category = "LevelSwitch";
		stateButtonTool19.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool20.OptionSetKey = "Switch";
		stateButtonTool20.SharedProps.Caption = "8";
		stateButtonTool20.SharedProps.Category = "LevelSwitch";
		stateButtonTool20.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		appearance90.Image = resources.GetObject("appearance32.Image");
		buttonTool167.SharedProps.AppearancesSmall.Appearance = appearance90;
		buttonTool167.SharedProps.Caption = "複製工項";
		buttonTool167.SharedProps.Category = "編輯";
		appearance91.Image = resources.GetObject("appearance33.Image");
		buttonTool168.SharedProps.AppearancesSmall.Appearance = appearance91;
		buttonTool168.SharedProps.Caption = "從業主契約載入資料";
		popupMenuTool35.SharedProps.Caption = "回存舊版「預算書」";
		buttonTool169.SharedProps.Caption = "儲存『預算書』版本";
		buttonTool170.SharedProps.Caption = "載入預算書範本...";
		buttonTool171.SharedProps.Caption = "工項名稱「別名」替換設定...";
		buttonTool172.SharedProps.Caption = "選項...";
		buttonTool172.SharedProps.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
		popupMenuTool36.SharedProps.Caption = "別名欄位";
		stateButtonTool21.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool22.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		popupMenuTool36.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[2] { stateButtonTool21, stateButtonTool22 });
		stateButtonTool23.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool23.OptionSetKey = "surName";
		stateButtonTool23.SharedProps.Caption = "顯示別名欄位";
		stateButtonTool23.SharedProps.Category = "檢視";
		stateButtonTool24.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool24.OptionSetKey = "surName";
		stateButtonTool24.SharedProps.Caption = "隱藏別名欄位";
		stateButtonTool24.SharedProps.Category = "檢視";
		buttonTool173.SharedProps.Caption = "設定...";
		buttonTool174.SharedProps.Caption = "自成本架構挑選";
		appearance92.Image = resources.GetObject("appearance34.Image");
		buttonTool175.SharedProps.AppearancesSmall.Appearance = appearance92;
		buttonTool175.SharedProps.Caption = "編輯成本架構屬性...";
		buttonTool175.SharedProps.Category = "詳細表編輯";
		buttonTool176.SharedProps.Caption = "鎖定";
		buttonTool176.SharedProps.Category = "工具";
		buttonTool177.SharedProps.Caption = "解鎖";
		buttonTool177.SharedProps.Category = "工具";
		appearance93.Image = resources.GetObject("appearance35.Image");
		buttonTool178.SharedProps.AppearancesSmall.Appearance = appearance93;
		buttonTool178.SharedProps.Caption = "展開明細表";
		buttonTool179.SharedProps.Caption = "自動增加小計項設定...";
		buttonTool179.SharedProps.Category = "工具";
		buttonTool180.SharedProps.Caption = "業主碼換公司碼";
		buttonTool180.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool180.SharedProps.Visible = false;
		popupControlContainerTool5.Control = this.cboSubItemQtyAmt;
		popupControlContainerTool5.SharedProps.Caption = "預算/估驗資訊";
		buttonTool181.SharedProps.Caption = "新增變更版次...";
		buttonTool181.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		popupMenuTool37.SharedProps.Caption = "預算變更(&C)";
		popupMenuTool37.SharedProps.Visible = false;
		popupMenuTool37.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[5] { buttonTool182, buttonTool183, buttonTool184, buttonTool185, buttonTool186 });
		buttonTool187.SharedProps.Caption = "預算變更歷史版次...";
		buttonTool188.SharedProps.Caption = "匯出資料至主機資料庫";
		appearance94.Image = resources.GetObject("appearance36.Image");
		stateButtonTool25.SharedProps.AppearancesSmall.Appearance = appearance94;
		stateButtonTool25.SharedProps.Caption = "只顯示變更項目";
		stateButtonTool25.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool25.SharedProps.Visible = false;
		popupControlContainerTool6.Control = this.cboItemChangeHistory;
		popupControlContainerTool6.SharedProps.Caption = "工項歷次變更紀錄";
		popupControlContainerTool6.SharedProps.Visible = false;
		buttonTool189.SharedProps.Caption = "變更責任歸屬";
		buttonTool189.SharedProps.Visible = false;
		buttonTool190.SharedProps.Caption = "重新自預估成本載入";
		buttonTool190.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool190.SharedProps.Visible = false;
		buttonTool191.SharedProps.Caption = "來源預估報價";
		buttonTool191.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool191.SharedProps.Visible = false;
		buttonTool192.SharedProps.Caption = "列印歷次報價";
		buttonTool192.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool192.SharedProps.Visible = false;
		buttonTool193.SharedProps.Caption = "列印業主變更設計報價單";
		buttonTool193.SharedProps.Visible = false;
		buttonTool194.SharedProps.Caption = "列印工程估價單";
		buttonTool194.SharedProps.Visible = false;
		buttonTool195.SharedProps.Caption = "刪除未變更項目";
		buttonTool195.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool195.SharedProps.Visible = false;
		buttonTool196.SharedProps.Caption = "列印執行預算變更";
		buttonTool196.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool196.SharedProps.Visible = false;
		buttonTool197.SharedProps.Caption = "本次執行預算變更列印";
		buttonTool197.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool197.SharedProps.Visible = false;
		buttonTool198.SharedProps.Caption = "刪除此次預算變更...";
		buttonTool198.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool198.SharedProps.Enabled = false;
		buttonTool199.SharedProps.Caption = "檢視變更資訊...";
		buttonTool199.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool200.SharedProps.Caption = "列印預算變更資訊";
		buttonTool200.SharedProps.Category = "檔案";
		buttonTool201.SharedProps.Caption = "匯入業主單價";
		buttonTool201.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		appearance95.Image = 7;
		buttonTool202.SharedProps.AppearancesSmall.Appearance = appearance95;
		buttonTool202.SharedProps.Caption = "編輯鎖定";
		buttonTool202.SharedProps.CustomizerCaption = "編輯鎖定";
		buttonTool203.SharedProps.Caption = "匯出預算低於已計價異常報表";
		buttonTool203.SharedProps.Category = "檔案";
		buttonTool203.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool203.SharedProps.Visible = false;
		buttonTool204.SharedProps.Caption = "列印預算實作差異比較報表";
		buttonTool204.SharedProps.Category = "檔案";
		buttonTool205.SharedProps.Caption = "自主檢查...";
		buttonTool205.SharedProps.Category = "工具";
		buttonTool205.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool26.SharedProps.Caption = "隱藏複價為0項目";
		stateButtonTool26.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool26.SharedProps.Visible = false;
		popupMenuTool38.SharedProps.Caption = "儲存版次";
		popupMenuTool38.SharedProps.Category = "工具";
		popupMenuTool38.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[3] { buttonTool206, buttonTool207, buttonTool208 });
		popupMenuTool39.SharedProps.Caption = "回存版次";
		popupMenuTool39.SharedProps.Category = "工具";
		popupMenuTool41.InstanceProps.IsFirstInGroup = true;
		popupMenuTool39.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[2] { popupMenuTool40, popupMenuTool41 });
		appearance96.ForeColor = System.Drawing.Color.Purple;
		buttonTool209.SharedProps.AppearancesSmall.Appearance = appearance96;
		buttonTool209.SharedProps.Caption = "儲存『契約書』版本";
		buttonTool209.SharedProps.Category = "工具";
		appearance97.ForeColor = System.Drawing.Color.Purple;
		popupMenuTool42.SharedProps.AppearancesSmall.Appearance = appearance97;
		popupMenuTool42.SharedProps.Caption = "回存舊版「契約書」";
		popupMenuTool42.SharedProps.Category = "工具";
		popupMenuTool43.SharedProps.Caption = "檔案(&F)";
		popupMenuTool43.SharedProps.Visible = false;
		buttonTool211.InstanceProps.IsFirstInGroup = true;
		popupMenuTool43.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[2] { buttonTool210, buttonTool211 });
		appearance98.ForeColor = System.Drawing.Color.Green;
		buttonTool212.SharedProps.AppearancesSmall.Appearance = appearance98;
		buttonTool212.SharedProps.Caption = "轉入(投)標單儲存成『契約書』版本";
		buttonTool212.SharedProps.Category = "工具";
		this.toolbarsManager.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[135]
		{
			popupMenuTool10, popupMenuTool11, popupMenuTool12, popupMenuTool14, popupMenuTool17, popupMenuTool23, buttonTool81, buttonTool82, buttonTool83, buttonTool84,
			buttonTool85, buttonTool86, buttonTool87, buttonTool88, buttonTool89, buttonTool90, buttonTool91, stateButtonTool12, buttonTool92, buttonTool93,
			buttonTool94, buttonTool95, buttonTool96, buttonTool97, buttonTool98, buttonTool99, buttonTool100, buttonTool101, buttonTool102, buttonTool103,
			popupMenuTool24, buttonTool119, buttonTool120, buttonTool121, buttonTool122, popupMenuTool28, buttonTool126, buttonTool127, buttonTool128, popupMenuTool29,
			buttonTool132, buttonTool133, buttonTool134, buttonTool135, buttonTool136, buttonTool137, buttonTool138, labelTool4, comboBoxTool3, buttonTool139,
			buttonTool140, buttonTool141, buttonTool142, popupMenuTool30, buttonTool143, buttonTool144, buttonTool145, buttonTool146, buttonTool147, buttonTool148,
			buttonTool149, buttonTool150, buttonTool151, labelTool5, comboBoxTool4, buttonTool152, popupMenuTool31, buttonTool155, buttonTool156, buttonTool157,
			popupMenuTool32, popupMenuTool33, buttonTool162, buttonTool163, buttonTool164, popupMenuTool34, popupControlContainerTool4, labelTool6, stateButtonTool13, stateButtonTool14,
			stateButtonTool15, stateButtonTool16, stateButtonTool17, stateButtonTool18, stateButtonTool19, stateButtonTool20, buttonTool167, buttonTool168, popupMenuTool35, buttonTool169,
			buttonTool170, buttonTool171, buttonTool172, popupMenuTool36, stateButtonTool23, stateButtonTool24, buttonTool173, buttonTool174, buttonTool175, buttonTool176,
			buttonTool177, buttonTool178, buttonTool179, buttonTool180, popupControlContainerTool5, buttonTool181, popupMenuTool37, buttonTool187, buttonTool188, stateButtonTool25,
			popupControlContainerTool6, buttonTool189, buttonTool190, buttonTool191, buttonTool192, buttonTool193, buttonTool194, buttonTool195, buttonTool196, buttonTool197,
			buttonTool198, buttonTool199, buttonTool200, buttonTool201, buttonTool202, buttonTool203, buttonTool204, buttonTool205, stateButtonTool26, popupMenuTool38,
			popupMenuTool39, buttonTool209, popupMenuTool42, popupMenuTool43, buttonTool212
		});
		this.toolbarsManager.ToolKeyPress += new Infragistics.Win.UltraWinToolbars.ToolKeyPressEventHandler(ultraToolbarsManager1_ToolKeyPress);
		this.toolbarsManager.BeforeToolbarListDropdown += new Infragistics.Win.UltraWinToolbars.BeforeToolbarListDropdownEventHandler(ultraToolbarsManager1_BeforeToolbarListDropdown);
		this.toolbarsManager.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(ultraToolbarsManager1_ToolClick);
		this.toolbarsManager.AfterToolCloseup += new Infragistics.Win.UltraWinToolbars.ToolDropdownEventHandler(ultraToolbarsManager1_AfterToolCloseup);
		this.toolbarsManager.AfterToolDeactivate += new Infragistics.Win.UltraWinToolbars.ToolEventHandler(ultraToolbarsManager1_AfterToolDeactivate);
		this.toolbarsManager.AfterToolActivate += new Infragistics.Win.UltraWinToolbars.ToolEventHandler(ultraToolbarsManager1_AfterToolActivate);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
		this.imageList1.Images.SetKeyName(0, "");
		this.imageList1.Images.SetKeyName(1, "");
		this.imageList1.Images.SetKeyName(2, "");
		this.imageList1.Images.SetKeyName(3, "");
		this.imageList1.Images.SetKeyName(4, "");
		this.imageList1.Images.SetKeyName(5, "");
		this.imageList1.Images.SetKeyName(6, "");
		this.imageList1.Images.SetKeyName(7, "");
		this.imageList1.Images.SetKeyName(8, "");
		this.imageList1.Images.SetKeyName(9, "");
		this.imageList1.Images.SetKeyName(10, "");
		this.imageList1.Images.SetKeyName(11, "");
		this.imageList1.Images.SetKeyName(12, "");
		this.imageList1.Images.SetKeyName(13, "");
		this.imageList1.Images.SetKeyName(14, "");
		this.imageList1.Images.SetKeyName(15, "");
		this.imageList1.Images.SetKeyName(16, "");
		this.imageList1.Images.SetKeyName(17, "");
		this.imageList1.Images.SetKeyName(18, "");
		this.imageList1.Images.SetKeyName(19, "");
		this.imageList1.Images.SetKeyName(20, "");
		this.imageList1.Images.SetKeyName(21, "");
		this._frmBudget_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._frmBudget_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._frmBudget_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
		this._frmBudget_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
		this._frmBudget_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 106);
		this._frmBudget_Toolbars_Dock_Area_Left.Name = "_frmBudget_Toolbars_Dock_Area_Left";
		this._frmBudget_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 512);
		this._frmBudget_Toolbars_Dock_Area_Left.ToolbarsManager = this.toolbarsManager;
		this._frmBudget_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._frmBudget_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._frmBudget_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
		this._frmBudget_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
		this._frmBudget_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(1084, 106);
		this._frmBudget_Toolbars_Dock_Area_Right.Name = "_frmBudget_Toolbars_Dock_Area_Right";
		this._frmBudget_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 512);
		this._frmBudget_Toolbars_Dock_Area_Right.ToolbarsManager = this.toolbarsManager;
		this._frmBudget_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._frmBudget_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._frmBudget_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
		this._frmBudget_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
		this._frmBudget_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
		this._frmBudget_Toolbars_Dock_Area_Top.Name = "_frmBudget_Toolbars_Dock_Area_Top";
		this._frmBudget_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(1084, 106);
		this._frmBudget_Toolbars_Dock_Area_Top.ToolbarsManager = this.toolbarsManager;
		this._frmBudget_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._frmBudget_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._frmBudget_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
		this._frmBudget_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
		this._frmBudget_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 618);
		this._frmBudget_Toolbars_Dock_Area_Bottom.Name = "_frmBudget_Toolbars_Dock_Area_Bottom";
		this._frmBudget_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(1084, 0);
		this._frmBudget_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.toolbarsManager;
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.White;
		this.imageList2.Images.SetKeyName(0, "");
		this.imageList2.Images.SetKeyName(1, "");
		this.imageList2.Images.SetKeyName(2, "");
		this.iglst_splt_Btn.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("iglst_splt_Btn.ImageStream");
		this.iglst_splt_Btn.TransparentColor = System.Drawing.Color.Transparent;
		this.iglst_splt_Btn.Images.SetKeyName(0, "");
		this.iglst_splt_Btn.Images.SetKeyName(1, "");
		this.iglst_splt_Btn.Images.SetKeyName(2, "");
		this.iglst_splt_Btn.Images.SetKeyName(3, "");
		this.TM_BDGT_AutoSave.Interval = 600000;
		this.TM_BDGT_AutoSave.Tick += new System.EventHandler(TM_BDGT_AutoSave_Tick);
		this.tmrReCalAll.Interval = 300;
		this.tmrReCalAll.Tick += new System.EventHandler(tmrReCalAll_Tick);
		this.timer1.Interval = 1000;
		this.timer1.Tick += new System.EventHandler(timer1_Tick);
		base.ClientSize = new System.Drawing.Size(1084, 618);
		base.Controls.Add(this.MainPanel);
		base.Controls.Add(this.pnl_spliter);
		base.Controls.Add(this.LeftPanel);
		base.Controls.Add(this._frmBudget_Toolbars_Dock_Area_Left);
		base.Controls.Add(this._frmBudget_Toolbars_Dock_Area_Right);
		base.Controls.Add(this._frmBudget_Toolbars_Dock_Area_Top);
		base.Controls.Add(this._frmBudget_Toolbars_Dock_Area_Bottom);
		base.KeyPreview = true;
		base.Name = "frmBudget";
		this.Text = "預算書編製";
		base.Load += new System.EventHandler(frmBudget_Load);
		base.Shown += new System.EventHandler(frmBudget_Shown);
		base.Activated += new System.EventHandler(frmBudget_Activated);
		base.FormClosed += new System.Windows.Forms.FormClosedEventHandler(frmBudget_FormClosed);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(frmBudget_FormClosing);
		base.Resize += new System.EventHandler(frmBudget_Resize);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(frmBudget_KeyDown);
		((System.ComponentModel.ISupportInitialize)this.cboHisPrice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubItemQtyAmt).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboItemChangeHistory).EndInit();
		this.LeftPanel.ResumeLayout(false);
		this.LeftPanel.PerformLayout();
		this.pnl_spliter.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ssp_Lower).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Bottom).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Upper).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Top).EndInit();
		this.MainPanel.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridBudget).EndInit();
		this.panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.axSSPanel2).EndInit();
		this.c.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.axSSPanel1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.toolbarsManager).EndInit();
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
}
