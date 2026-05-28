#define DEBUG
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.DirectoryServices;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.CommonClass.MrsBase;
using Archnowledge.Pcces.DomainModule.Bid;
using Archnowledge.Pcces.DomainModule.Budget;
using Archnowledge.Pcces.DomainModule.Coms;
using Archnowledge.Pcces.DomainModule.ExportExcel;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.DomainModule.LogicalBase;
using Archnowledge.Pcces.DomainModule.MrsBase;
using Archnowledge.Pcces.DomainModule.Sub;
using Archnowledge.Pcces.DomainModule.SubChg;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.PccesMain.BudgetChange;
using Archnowledge.Pcces.PccesMain.MrsBase;
using Archnowledge.Pcces.PccesMain.SplitContract;
using Archnowledge.Pcces.PccesMain.SysMaintain;
using Archnowledge.Pcces.STDClass;
using Aspose.Cells;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinStatusBar;
using Infragistics.Win.UltraWinToolbars;
using PCCES.CODECHECK;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormBudgetRes : Form
{
	private const string FileIni = "OptionSet.ini";

	private UltraToolbarsManager ultraToolbarsManager1;

	private IContainer components;

	private UltraToolbarsDockArea _FormBudgetRes_Toolbars_Dock_Area_Left;

	private UltraToolbarsDockArea _FormBudgetRes_Toolbars_Dock_Area_Right;

	private UltraToolbarsDockArea _FormBudgetRes_Toolbars_Dock_Area_Top;

	private UltraToolbarsDockArea _FormBudgetRes_Toolbars_Dock_Area_Bottom;

	private Panel panel1;

	public GridMrsBase gridMrsBase1;

	private UltraButton ultraButton3;

	private ImageList imageList2;

	private SaveFileDialog saveFileDialog1;

	private ImageList imageList1;

	private UltraStatusBar StatusBar;

	private Panel pnlParent;

	public GridMrsBase gridMrsBase2;

	private Panel panel7;

	private UltraLabel ultraLabel2;

	private UltraButton ultraButton2;

	private ImageList imageList3;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel3;

	private UltraLabel ultraLabel4;

	private UltraLabel ultraLabel5;

	private Splitter splitter1;

	private Panel panel2;

	private UltraButton ultraButton9;

	private System.Windows.Forms.ToolTip toolTip1;

	private UltraButton BtnReCalSmall;

	private DirectoryEntry directoryEntry1;

	private UltraButton btnAddBookList;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem mnuExportGrid2;

	private int iLEMW_RateErr = 0;

	private string F_CurrentDBName = "";

	private bool F_IsSBID = false;

	private bool F_IsNeedToReloadAllData = false;

	private bool F_IsBudgetFormNeedToReload = false;

	private bool IsCanEdit = true;

	private int RowChangeCol = 0;

	private bool IsKeyScroll = false;

	private bool IsPressCtrl = false;

	private FormStatus GRID2_STATUS = FormStatus.Normal;

	private int F_MnyRateType = 0;

	private decimal F_Rate1 = 0m;

	private decimal F_Rate2 = 0m;

	private string F_UserID;

	private string F_KeyWord = "";

	private FormStatus FORM_STATUS = FormStatus.Iinitial;

	private DataTable dtResource = null;

	private DataSet dsResource = null;

	private DataTable dtAutoNumB;

	private CodeValidator cCV;

	private CodeFitter cCF;

	private DataTable dtAutoNumA;

	private string projectCode;

	private bool IsTemplate = false;

	private string F_chgCount;

	private string F_calledPccesCode;

	private int F_MainQty = 0;

	private int F_MainCst = 0;

	private int F_MainAmt = 0;

	private int F_AnaQty = 0;

	private int F_AnaCst = 0;

	private int F_AnaAmt = 0;

	private int GridCols = 15;

	private int Grid2Cols = 15;

	private object[,] GridColsSquence;

	private object[,] Grid2ColsSquence;

	private string F_iCount = "";

	private bool IsReload = false;

	private DataSet dsPwrSet;

	private PccesFormAction FormActionName;

	private string ExtraSearchCriteria = string.Empty;

	private bool RunExtraSearchCriteria = false;

	private bool HasApproved = false;

	private ProjMrsA projMrsA = null;

	private Archnowledge.Pcces.DomainModule.LogicalBase.ItemA itemA = null;

	private int budgetType = 0;

	private string parentProjectCode = string.Empty;

	private string lblProjectData;

	private DataSet ParentItemA;

	private DataSet dsParentProjMrsA;

	private bool AddParentBookList = false;

	private bool FindParentFromBudget = false;

	private double Num = 1.0;

	public string _CurrentDBName
	{
		get
		{
			return F_CurrentDBName;
		}
		set
		{
			F_CurrentDBName = value;
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

	public bool _IsBudgetFormNeedToReload => F_IsBudgetFormNeedToReload;

	public int _MnyRateType
	{
		get
		{
			return F_MnyRateType;
		}
		set
		{
			F_MnyRateType = value;
		}
	}

	public decimal _Rate1
	{
		get
		{
			return F_Rate1;
		}
		set
		{
			F_Rate1 = value;
		}
	}

	public decimal _Rate2
	{
		get
		{
			return F_Rate2;
		}
		set
		{
			F_Rate2 = value;
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

	public PccesFormAction _ActionName
	{
		get
		{
			return FormActionName;
		}
		set
		{
			FormActionName = value;
		}
	}

	public string _ProjectCode
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

	public string _calledPccesCode
	{
		get
		{
			return F_calledPccesCode;
		}
		set
		{
			F_calledPccesCode = value;
		}
	}

	public int _budgetType
	{
		set
		{
			budgetType = value;
		}
	}

	public string _parentProjectCode
	{
		set
		{
			parentProjectCode = value;
		}
	}

	public bool _HasApproved
	{
		get
		{
			return HasApproved;
		}
		set
		{
			HasApproved = value;
		}
	}

	public string _lblProjectData
	{
		get
		{
			return lblProjectData;
		}
		set
		{
			lblProjectData = value;
		}
	}

	public DataSet _ParentItemA
	{
		get
		{
			return ParentItemA;
		}
		set
		{
			ParentItemA = value;
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

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.OptionSet optionSet1 = new Infragistics.Win.UltraWinToolbars.OptionSet("surName");
		Infragistics.Win.UltraWinToolbars.OptionSet optionSet2 = new Infragistics.Win.UltraWinToolbars.OptionSet("FilterDB");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Tool1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuEditItem");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool1 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("PopupView");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool2 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuTool");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool3 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("popupSendback");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuParent");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuParentFromBudget");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar2 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Tool2");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool4 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("MenuImport");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool5 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("MenuExport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExpNotCorrect");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool5 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExpAllCorrect");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool6 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExcelExp");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool6 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("MenuChangeCode");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar3 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Tools");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool1 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuViewAllItem", "FilterDB");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool2 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuViewAnalysis", "FilterDB");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool3 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuViewNoAnalysis", "FilterDB");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool4 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuItem", "FilterDB");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool5 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLabor", "FilterDB");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool6 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuEquip", "FilterDB");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool7 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuMaterial", "FilterDB");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool8 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuWaste", "FilterDB");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool9 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuCalcErr", "FilterDB");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool10 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuAnaMinus", "FilterDB");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar4 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Tool4");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool1 = new Infragistics.Win.UltraWinToolbars.LabelTool("Other_lblFilter");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool1 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("Other_FilterType");
		Infragistics.Win.UltraWinToolbars.TextBoxTool textBoxTool1 = new Infragistics.Win.UltraWinToolbars.TextBoxTool("Other_QueryText");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool7 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Other_FilterExecute");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool2 = new Infragistics.Win.UltraWinToolbars.LabelTool("lblFind");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool2 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool8 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuGo");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool9 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCalculateCorrectness");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool11 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuCorrectItems", "FilterDB");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool12 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuIncorrect", "FilterDB");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool10 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCorrectCName");
		Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool7 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("PopupView");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool13 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuViewAllItem", "FilterDB");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool14 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuViewAnalysis", "FilterDB");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool15 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuViewNoAnalysis", "FilterDB");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool8 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("PopupViewTyppe");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool11 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuMnyRate");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool16 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuCostIsZero", "");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool12 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuLockCost");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool13 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuUnLockCost");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool17 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuItemDup", "FilterDB");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool9 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuViewsurName");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool10 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("PopupUse");
		Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool11 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("PopContext");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool14 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuEditItem");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool15 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuAnalysis");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool16 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuParent");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool17 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuEditItem");
		Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool18 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuViewAllItem", "FilterDB");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool19 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuViewAnalysis", "FilterDB");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool20 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuViewNoAnalysis", "FilterDB");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool12 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("PopupViewTyppe");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool21 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuItem", "FilterDB");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool22 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLabor", "FilterDB");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool23 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuEquip", "FilterDB");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool24 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuMaterial", "FilterDB");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool25 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuWaste", "FilterDB");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool26 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuItem", "FilterDB");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool27 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuLabor", "FilterDB");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool28 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuEquip", "FilterDB");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool29 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuMaterial", "FilterDB");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool30 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuWaste", "FilterDB");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool18 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuAnalysis");
		Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool19 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuParent");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool20 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSend");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool31 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuCostIsZero", "");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool21 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuLockCost");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool22 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuUnLockCost");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool3 = new Infragistics.Win.UltraWinToolbars.LabelTool("lblFind");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool3 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo1");
		Infragistics.Win.ValueList valueList1 = new Infragistics.Win.ValueList(0);
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool23 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuGo");
		Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool13 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuTool");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool24 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuGetFromMrs");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool25 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuUseMrsCost");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool26 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDBReSet");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool27 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuFillRate");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool28 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuChangeCode");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool29 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuAutoNum");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool30 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSendBack");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool31 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuGetFromMrs");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool32 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuMnyRate");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool14 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("MenuImport");
		Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool33 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuImpExcel");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool34 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuImpXML");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool15 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("MenuExport");
		Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool35 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExpExcel");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool36 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExpXML");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool37 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuImpExcel");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool38 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuImpXML");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool39 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExpExcel");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool40 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExpXML");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool41 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDBReSet");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool42 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExcelExp");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool43 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuUseMrsCost");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool44 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuFillRate");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool45 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuChangeCode");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool32 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuCalcErr", "FilterDB");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool46 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCodeUpgrade");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool33 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuAnaMinus", "FilterDB");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool4 = new Infragistics.Win.UltraWinToolbars.LabelTool("Other_lblFilter");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool4 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("Other_FilterType");
		Infragistics.Win.ValueList valueList2 = new Infragistics.Win.ValueList(0);
		Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.UltraWinToolbars.TextBoxTool textBoxTool2 = new Infragistics.Win.UltraWinToolbars.TextBoxTool("Other_QueryText");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool47 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Other_FilterExecute");
		Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool48 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuAutoNum");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool16 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuViewsurName");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool34 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuViewItemSurName", "surName");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool35 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuViewItemUnSurName", "surName");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool36 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuViewItemSurName", "surName");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool37 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuViewItemUnSurName", "surName");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool17 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("MenuChangeCode");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool49 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExpExcelChange");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool50 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuImpExcelChange");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool51 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExpExcelChange");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool52 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuImpExcelChange");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool53 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuParentFromBudget");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool38 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuCorrectRate", "");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool39 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuIncorrect", "FilterDB");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool40 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuNotfit", "FilterDB");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool54 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCalculateCorrectness");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool41 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuItemDup", "FilterDB");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool55 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExpNotCorrect");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool56 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCorrectCName");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool18 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("popupSendback");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool57 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSendBack");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool58 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSendBack_NameUnit");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool59 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSendBack_Cost");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool60 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSendBack_NameUnit");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool61 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSendBack_Cost");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool42 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuCorrectItems", "FilterDB");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool62 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExpAllCorrect");
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormBudgetRes));
		Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
		this.ultraToolbarsManager1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
		this.gridMrsBase1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this._FormBudgetRes_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormBudgetRes_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormBudgetRes_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormBudgetRes_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.panel1 = new System.Windows.Forms.Panel();
		this.StatusBar = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.ultraButton3 = new Infragistics.Win.Misc.UltraButton();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.pnlParent = new System.Windows.Forms.Panel();
		this.gridMrsBase2 = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.panel7 = new System.Windows.Forms.Panel();
		this.btnAddBookList = new Infragistics.Win.Misc.UltraButton();
		this.BtnReCalSmall = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton9 = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraButton2 = new Infragistics.Win.Misc.UltraButton();
		this.imageList3 = new System.Windows.Forms.ImageList(this.components);
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.splitter1 = new System.Windows.Forms.Splitter();
		this.panel2 = new System.Windows.Forms.Panel();
		this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.directoryEntry1 = new System.DirectoryServices.DirectoryEntry();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.mnuExportGrid2 = new System.Windows.Forms.ToolStripMenuItem();
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridMrsBase1).BeginInit();
		this.panel1.SuspendLayout();
		this.pnlParent.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridMrsBase2).BeginInit();
		this.panel7.SuspendLayout();
		this.panel2.SuspendLayout();
		this.contextMenuStrip1.SuspendLayout();
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
		appearance11.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance11.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance11.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.MenuSettings.HotTrackAppearance = appearance11;
		appearance12.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance12.BackColor2 = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraToolbarsManager1.MenuSettings.IconAreaAppearance = appearance12;
		appearance13.BackColor = System.Drawing.Color.White;
		appearance13.BackColor2 = System.Drawing.Color.White;
		this.ultraToolbarsManager1.MenuSettings.ToolAppearance = appearance13;
		optionSet1.AllowAllUp = false;
		optionSet2.AllowAllUp = false;
		this.ultraToolbarsManager1.OptionSets.Add(optionSet1);
		this.ultraToolbarsManager1.OptionSets.Add(optionSet2);
		this.ultraToolbarsManager1.ShowFullMenusDelay = 500;
		this.ultraToolbarsManager1.ShowQuickCustomizeButton = false;
		this.ultraToolbarsManager1.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2003;
		ultraToolbar1.DockedColumn = 0;
		ultraToolbar1.DockedRow = 0;
		ultraToolbar1.Text = "Tool1";
		popupMenuTool1.InstanceProps.IsFirstInGroup = true;
		popupMenuTool3.InstanceProps.IsFirstInGroup = true;
		buttonTool2.InstanceProps.IsFirstInGroup = true;
		ultraToolbar1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[6] { buttonTool1, popupMenuTool1, popupMenuTool2, popupMenuTool3, buttonTool2, buttonTool3 });
		ultraToolbar2.DockedColumn = 1;
		ultraToolbar2.DockedRow = 0;
		ultraToolbar2.Text = "Tool2";
		popupMenuTool4.InstanceProps.IsFirstInGroup = true;
		buttonTool5.InstanceProps.IsFirstInGroup = true;
		buttonTool6.InstanceProps.IsFirstInGroup = true;
		popupMenuTool6.InstanceProps.IsFirstInGroup = true;
		ultraToolbar2.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[6] { popupMenuTool4, popupMenuTool5, buttonTool4, buttonTool5, buttonTool6, popupMenuTool6 });
		ultraToolbar3.DockedColumn = 0;
		ultraToolbar3.DockedRow = 1;
		ultraToolbar3.Text = "Tool3";
		stateButtonTool1.Checked = true;
		stateButtonTool1.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool2.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool3.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool4.InstanceProps.IsFirstInGroup = true;
		stateButtonTool4.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool5.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool6.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool7.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool8.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool9.InstanceProps.IsFirstInGroup = true;
		stateButtonTool10.InstanceProps.IsFirstInGroup = true;
		ultraToolbar3.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[10] { stateButtonTool1, stateButtonTool2, stateButtonTool3, stateButtonTool4, stateButtonTool5, stateButtonTool6, stateButtonTool7, stateButtonTool8, stateButtonTool9, stateButtonTool10 });
		ultraToolbar4.DockedColumn = 0;
		ultraToolbar4.DockedRow = 2;
		ultraToolbar4.Text = "Tool4";
		labelTool2.InstanceProps.IsFirstInGroup = true;
		labelTool2.InstanceProps.Width = 44;
		stateButtonTool12.InstanceProps.IsFirstInGroup = true;
		buttonTool10.InstanceProps.IsFirstInGroup = true;
		ultraToolbar4.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[11]
		{
			labelTool1, comboBoxTool1, textBoxTool1, buttonTool7, labelTool2, comboBoxTool2, buttonTool8, buttonTool9, stateButtonTool11, stateButtonTool12,
			buttonTool10
		});
		this.ultraToolbarsManager1.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[4] { ultraToolbar1, ultraToolbar2, ultraToolbar3, ultraToolbar4 });
		appearance14.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance14.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.ToolbarSettings.Appearance = appearance14;
		appearance15.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance15.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance15.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.ToolbarSettings.HotTrackAppearance = appearance15;
		appearance16.BackColor = System.Drawing.Color.FromArgb(255, 128, 0);
		appearance16.BackColor2 = System.Drawing.Color.White;
		this.ultraToolbarsManager1.ToolbarSettings.PressedAppearance = appearance16;
		popupMenuTool7.SharedProps.Caption = "檢視";
		popupMenuTool7.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		stateButtonTool13.Checked = true;
		stateButtonTool13.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool14.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool15.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		popupMenuTool8.InstanceProps.IsFirstInGroup = true;
		buttonTool11.InstanceProps.IsFirstInGroup = true;
		stateButtonTool16.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool17.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		popupMenuTool9.InstanceProps.IsFirstInGroup = true;
		popupMenuTool7.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[10] { stateButtonTool13, stateButtonTool14, stateButtonTool15, popupMenuTool8, buttonTool11, stateButtonTool16, buttonTool12, buttonTool13, stateButtonTool17, popupMenuTool9 });
		appearance17.Image = resources.GetObject("appearance17.Image");
		popupMenuTool10.SharedProps.AppearancesSmall.Appearance = appearance17;
		popupMenuTool10.SharedProps.Caption = "引用單價";
		popupMenuTool10.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		popupMenuTool10.SharedProps.Visible = false;
		popupMenuTool11.SharedProps.Caption = "右鍵功能表";
		buttonTool15.InstanceProps.IsFirstInGroup = true;
		buttonTool16.InstanceProps.IsFirstInGroup = true;
		popupMenuTool11.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[3] { buttonTool14, buttonTool15, buttonTool16 });
		appearance18.Image = resources.GetObject("appearance18.Image");
		buttonTool17.SharedProps.AppearancesSmall.Appearance = appearance18;
		buttonTool17.SharedProps.Caption = "編輯工項";
		buttonTool17.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		stateButtonTool18.Checked = true;
		stateButtonTool18.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool18.OptionSetKey = "FilterDB";
		stateButtonTool18.SharedProps.Caption = "全部工項";
		stateButtonTool18.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool19.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool19.OptionSetKey = "FilterDB";
		stateButtonTool19.SharedProps.Caption = "有單價分析工項";
		stateButtonTool19.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool20.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool20.OptionSetKey = "FilterDB";
		stateButtonTool20.SharedProps.Caption = "無單價分析工項";
		stateButtonTool20.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		popupMenuTool12.SharedProps.Caption = "顯示項目類別";
		stateButtonTool21.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool22.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool23.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool24.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool25.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		popupMenuTool12.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[5] { stateButtonTool21, stateButtonTool22, stateButtonTool23, stateButtonTool24, stateButtonTool25 });
		stateButtonTool26.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool26.OptionSetKey = "FilterDB";
		stateButtonTool26.SharedProps.Caption = "工項";
		stateButtonTool26.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool27.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool27.OptionSetKey = "FilterDB";
		stateButtonTool27.SharedProps.Caption = "人工";
		stateButtonTool27.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool28.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool28.OptionSetKey = "FilterDB";
		stateButtonTool28.SharedProps.Caption = "機具";
		stateButtonTool28.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool29.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool29.OptionSetKey = "FilterDB";
		stateButtonTool29.SharedProps.Caption = "材料";
		stateButtonTool29.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool30.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool30.OptionSetKey = "FilterDB";
		stateButtonTool30.SharedProps.Caption = "雜項";
		stateButtonTool30.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		appearance19.Image = resources.GetObject("appearance19.Image");
		buttonTool18.SharedProps.AppearancesSmall.Appearance = appearance19;
		buttonTool18.SharedProps.Caption = "單價分析";
		buttonTool19.SharedProps.Caption = "查詢父項";
		buttonTool19.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool20.SharedProps.Caption = "回傳資料庫";
		stateButtonTool31.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool31.SharedProps.Caption = "單價或數量為 \"0\" 項目";
		buttonTool21.SharedProps.Caption = "鎖定單價項目";
		buttonTool21.SharedProps.Visible = false;
		buttonTool22.SharedProps.Caption = "未鎖定單價項目";
		buttonTool22.SharedProps.Visible = false;
		labelTool3.SharedProps.Caption = "尋找:";
		labelTool3.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		comboBoxTool3.DropDownStyle = Infragistics.Win.DropDownStyle.DropDown;
		comboBoxTool3.SharedProps.Caption = "尋找關鍵字";
		comboBoxTool3.SharedProps.Width = 200;
		comboBoxTool3.ValueList = valueList1;
		appearance20.Image = resources.GetObject("appearance20.Image");
		buttonTool23.SharedProps.AppearancesSmall.Appearance = appearance20;
		buttonTool23.SharedProps.Caption = "GO";
		popupMenuTool13.SharedProps.Caption = "工具";
		popupMenuTool13.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		buttonTool26.InstanceProps.IsFirstInGroup = true;
		buttonTool27.InstanceProps.IsFirstInGroup = true;
		buttonTool29.InstanceProps.IsFirstInGroup = true;
		popupMenuTool13.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[6] { buttonTool24, buttonTool25, buttonTool26, buttonTool27, buttonTool28, buttonTool29 });
		buttonTool30.SharedProps.Caption = "回傳基本資料庫";
		buttonTool31.SharedProps.Caption = "引用基本資料庫 單價及單價分析";
		buttonTool32.SharedProps.Caption = "金額權重...";
		appearance21.Image = resources.GetObject("appearance21.Image");
		popupMenuTool14.SharedProps.AppearancesSmall.Appearance = appearance21;
		popupMenuTool14.SharedProps.Caption = "匯入";
		popupMenuTool14.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		popupMenuTool14.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[2] { buttonTool33, buttonTool34 });
		appearance22.Image = resources.GetObject("appearance22.Image");
		popupMenuTool15.SharedProps.AppearancesSmall.Appearance = appearance22;
		popupMenuTool15.SharedProps.Caption = "匯出";
		popupMenuTool15.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		popupMenuTool15.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[2] { buttonTool35, buttonTool36 });
		buttonTool37.SharedProps.Caption = "Excel 格式";
		buttonTool38.SharedProps.Caption = "XML 格式";
		buttonTool39.SharedProps.Caption = "Excel 格式";
		buttonTool40.SharedProps.Caption = "XML 格式";
		buttonTool41.SharedProps.Caption = "資料庫重整";
		buttonTool41.SharedProps.Shortcut = System.Windows.Forms.Shortcut.CtrlF1;
		buttonTool42.SharedProps.Caption = "匯出EXCEL";
		buttonTool42.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool42.SharedProps.Visible = false;
		buttonTool43.SharedProps.Caption = "引用基本資料庫 單價";
		buttonTool44.SharedProps.Caption = "快速填入各項比率...";
		buttonTool45.SharedProps.Caption = "單筆換碼...";
		buttonTool45.SharedProps.Shortcut = System.Windows.Forms.Shortcut.F4;
		stateButtonTool32.OptionSetKey = "FilterDB";
		stateButtonTool32.SharedProps.Caption = "計算錯誤項目";
		stateButtonTool32.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool46.SharedProps.Caption = "編碼更新(昇級)...";
		stateButtonTool33.OptionSetKey = "FilterDB";
		stateButtonTool33.SharedProps.Caption = "分析子項為負";
		stateButtonTool33.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		labelTool4.SharedProps.Caption = "篩選:";
		labelTool4.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		comboBoxTool4.SharedProps.Caption = "Other_FilterType";
		comboBoxTool4.SharedProps.Width = 85;
		comboBoxTool4.Text = "工程會碼";
		valueListItem1.DataValue = "0";
		valueListItem1.DisplayText = "工程會碼";
		valueListItem2.DataValue = "1";
		valueListItem2.DisplayText = "工項名稱";
		valueListItem3.DataValue = "2";
		valueListItem3.DisplayText = "工項外碼";
		valueList2.ValueListItems.Add(valueListItem1);
		valueList2.ValueListItems.Add(valueListItem2);
		valueList2.ValueListItems.Add(valueListItem3);
		comboBoxTool4.ValueList = valueList2;
		textBoxTool2.SharedProps.Caption = "Other_QueryText";
		textBoxTool2.SharedProps.Width = 180;
		appearance23.Image = resources.GetObject("appearance23.Image");
		buttonTool47.SharedProps.AppearancesSmall.Appearance = appearance23;
		buttonTool47.SharedProps.Caption = "Other_FilterExecute";
		buttonTool48.SharedProps.Caption = "自動編碼";
		popupMenuTool16.SharedProps.Caption = "別名欄位";
		popupMenuTool16.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		stateButtonTool34.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool35.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		popupMenuTool16.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[2] { stateButtonTool34, stateButtonTool35 });
		stateButtonTool36.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool36.OptionSetKey = "surName";
		stateButtonTool36.SharedProps.Caption = "顯示別名欄位";
		stateButtonTool36.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool37.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool37.OptionSetKey = "surName";
		stateButtonTool37.SharedProps.Caption = "隱藏別名欄位";
		stateButtonTool37.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		popupMenuTool17.SharedProps.Caption = "換碼工具";
		popupMenuTool17.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		popupMenuTool17.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[2] { buttonTool49, buttonTool50 });
		buttonTool51.SharedProps.Caption = "匯出EXCEL填公司碼";
		buttonTool51.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool52.SharedProps.Caption = "匯入換碼後EXCEL";
		buttonTool52.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool53.SharedProps.Caption = "查詢詳細表分佈數量";
		buttonTool53.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool38.SharedProps.Caption = "正確率檢查";
		stateButtonTool39.OptionSetKey = "FilterDB";
		stateButtonTool39.SharedProps.Caption = "不正確項";
		stateButtonTool39.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool40.OptionSetKey = "FilterDB";
		stateButtonTool40.SharedProps.Caption = "不符合項";
		stateButtonTool40.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool54.SharedProps.Caption = "計算正確率";
		buttonTool54.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool54.SharedProps.ToolTipText = "計算正確率時會先切換成【全部工項】";
		stateButtonTool41.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool41.OptionSetKey = "FilterDB";
		stateButtonTool41.SharedProps.Caption = "工項名稱及單位重複";
		buttonTool55.SharedProps.Caption = "匯出不正確項";
		buttonTool55.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool56.SharedProps.Caption = "名稱修正...";
		buttonTool56.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool56.SharedProps.ToolTipText = "名稱修正時會先切換成【全部工項】";
		popupMenuTool18.SharedProps.Caption = "回傳選取項";
		popupMenuTool18.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		popupMenuTool18.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[3] { buttonTool57, buttonTool58, buttonTool59 });
		buttonTool60.SharedProps.Caption = "回傳名稱及單位";
		buttonTool61.SharedProps.Caption = "回傳單價";
		stateButtonTool42.OptionSetKey = "FilterDB";
		stateButtonTool42.SharedProps.Caption = "正確項";
		stateButtonTool42.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool62.SharedProps.Caption = "匯出正確項";
		buttonTool62.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		this.ultraToolbarsManager1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[64]
		{
			popupMenuTool7, popupMenuTool10, popupMenuTool11, buttonTool17, stateButtonTool18, stateButtonTool19, stateButtonTool20, popupMenuTool12, stateButtonTool26, stateButtonTool27,
			stateButtonTool28, stateButtonTool29, stateButtonTool30, buttonTool18, buttonTool19, buttonTool20, stateButtonTool31, buttonTool21, buttonTool22, labelTool3,
			comboBoxTool3, buttonTool23, popupMenuTool13, buttonTool30, buttonTool31, buttonTool32, popupMenuTool14, popupMenuTool15, buttonTool37, buttonTool38,
			buttonTool39, buttonTool40, buttonTool41, buttonTool42, buttonTool43, buttonTool44, buttonTool45, stateButtonTool32, buttonTool46, stateButtonTool33,
			labelTool4, comboBoxTool4, textBoxTool2, buttonTool47, buttonTool48, popupMenuTool16, stateButtonTool36, stateButtonTool37, popupMenuTool17, buttonTool51,
			buttonTool52, buttonTool53, stateButtonTool38, stateButtonTool39, stateButtonTool40, buttonTool54, stateButtonTool41, buttonTool55, buttonTool56, popupMenuTool18,
			buttonTool60, buttonTool61, stateButtonTool42, buttonTool62
		});
		this.ultraToolbarsManager1.ToolKeyPress += new Infragistics.Win.UltraWinToolbars.ToolKeyPressEventHandler(ultraToolbarsManager1_ToolKeyPress);
		this.ultraToolbarsManager1.BeforeToolbarListDropdown += new Infragistics.Win.UltraWinToolbars.BeforeToolbarListDropdownEventHandler(ultraToolbarsManager1_BeforeToolbarListDropdown);
		this.ultraToolbarsManager1.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(ultraToolbarsManager1_ToolClick);
		this.ultraToolbarsManager1.ToolValueChanged += new Infragistics.Win.UltraWinToolbars.ToolEventHandler(ultraToolbarsManager1_ToolValueChanged);
		this.gridMrsBase1._ExcelFileName = "";
		this.gridMrsBase1._ExcelSheeName = "";
		this.gridMrsBase1._IsOpenExcelAfterExport = false;
		this.gridMrsBase1.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
		this.gridMrsBase1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
		this.gridMrsBase1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridMrsBase1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
		this.gridMrsBase1.ColumnInfo = resources.GetString("gridMrsBase1.ColumnInfo");
		this.ultraToolbarsManager1.SetContextMenuUltra(this.gridMrsBase1, "PopContext");
		this.gridMrsBase1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridMrsBase1.ExtendLastCol = true;
		this.gridMrsBase1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridMrsBase1.ForeColor = System.Drawing.Color.Black;
		this.gridMrsBase1.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.gridMrsBase1.IsProcessUndo = false;
		this.gridMrsBase1.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.gridMrsBase1.Location = new System.Drawing.Point(0, 0);
		this.gridMrsBase1.Name = "gridMrsBase1";
		this.gridMrsBase1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.gridMrsBase1.ShowCursor = true;
		this.gridMrsBase1.ShowToolTipOnNarrowColumn = true;
		this.gridMrsBase1.Size = new System.Drawing.Size(1084, 285);
		this.gridMrsBase1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridMrsBase1.Styles"));
		this.gridMrsBase1.TabIndex = 8;
		this.gridMrsBase1.UndoMax = 10;
		this.gridMrsBase1.Click += new System.EventHandler(gridMrsBase1_Click);
		this.gridMrsBase1.AfterSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(gridMrsBase1_AfterSelChange);
		this.gridMrsBase1.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(gridMrsBase1_AfterRowColChange);
		this.gridMrsBase1.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(gridMrsBase1_StartEdit);
		this.gridMrsBase1.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(gridMrsBase1_AfterEdit);
		this.gridMrsBase1.KeyDown += new System.Windows.Forms.KeyEventHandler(gridMrsBase1_KeyDown);
		this.gridMrsBase1.MouseDown += new System.Windows.Forms.MouseEventHandler(gridMrsBase1_MouseDown);
		this.gridMrsBase1.MouseUp += new System.Windows.Forms.MouseEventHandler(gridMrsBase1_MouseUp);
		this.gridMrsBase1.LeaveCell += new System.EventHandler(gridMrsBase1_LeaveCell);
		this.gridMrsBase1.MouseMove += new System.Windows.Forms.MouseEventHandler(gridMrsBase1_MouseMove);
		this.gridMrsBase1.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(gridMrsBase1_BeforeEdit);
		this.gridMrsBase1.DoubleClick += new System.EventHandler(gridMrsBase1_DoubleClick);
		this.gridMrsBase1.KeyUp += new System.Windows.Forms.KeyEventHandler(gridMrsBase1_KeyUp);
		this._FormBudgetRes_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormBudgetRes_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormBudgetRes_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
		this._FormBudgetRes_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormBudgetRes_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 77);
		this._FormBudgetRes_Toolbars_Dock_Area_Left.Name = "_FormBudgetRes_Toolbars_Dock_Area_Left";
		this._FormBudgetRes_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 486);
		this._FormBudgetRes_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormBudgetRes_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormBudgetRes_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormBudgetRes_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
		this._FormBudgetRes_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormBudgetRes_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(1086, 77);
		this._FormBudgetRes_Toolbars_Dock_Area_Right.Name = "_FormBudgetRes_Toolbars_Dock_Area_Right";
		this._FormBudgetRes_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 486);
		this._FormBudgetRes_Toolbars_Dock_Area_Right.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormBudgetRes_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormBudgetRes_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormBudgetRes_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
		this._FormBudgetRes_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormBudgetRes_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
		this._FormBudgetRes_Toolbars_Dock_Area_Top.Name = "_FormBudgetRes_Toolbars_Dock_Area_Top";
		this._FormBudgetRes_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(1086, 77);
		this._FormBudgetRes_Toolbars_Dock_Area_Top.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormBudgetRes_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormBudgetRes_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormBudgetRes_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
		this._FormBudgetRes_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormBudgetRes_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 563);
		this._FormBudgetRes_Toolbars_Dock_Area_Bottom.Name = "_FormBudgetRes_Toolbars_Dock_Area_Bottom";
		this._FormBudgetRes_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(1086, 0);
		this._FormBudgetRes_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ultraToolbarsManager1;
		this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel1.Controls.Add(this.gridMrsBase1);
		this.panel1.Controls.Add(this.StatusBar);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 210);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(1086, 317);
		this.panel1.TabIndex = 5;
		appearance24.BackColor = System.Drawing.Color.Silver;
		appearance24.FontData.SizeInPoints = 11f;
		this.StatusBar.Appearance = appearance24;
		this.StatusBar.Location = new System.Drawing.Point(0, 285);
		this.StatusBar.Name = "StatusBar";
		this.StatusBar.Padding = new Infragistics.Win.UltraWinStatusBar.UIElementMargins(1, 2, 1, 1);
		appearance25.TextVAlign = Infragistics.Win.VAlign.Middle;
		ultraStatusPanel1.Appearance = appearance25;
		ultraStatusPanel1.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel1.Key = "Rows";
		ultraStatusPanel1.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		ultraStatusPanel1.Text = " 資料筆數:";
		ultraStatusPanel2.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel2.Width = 200;
		this.StatusBar.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[2] { ultraStatusPanel1, ultraStatusPanel2 });
		this.StatusBar.Size = new System.Drawing.Size(1084, 30);
		this.StatusBar.TabIndex = 9;
		this.ultraButton3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		appearance26.BackColor = System.Drawing.SystemColors.ControlLightLight;
		appearance26.BackColor2 = System.Drawing.SystemColors.ControlLight;
		appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance26.Image = resources.GetObject("appearance5.Image");
		appearance26.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton3.Appearance = appearance26;
		this.ultraButton3.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton3.Cursor = System.Windows.Forms.Cursors.Hand;
		this.ultraButton3.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraButton3.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton3.Location = new System.Drawing.Point(990, 4);
		this.ultraButton3.Name = "ultraButton3";
		this.ultraButton3.ShowFocusRect = false;
		this.ultraButton3.ShowOutline = false;
		this.ultraButton3.Size = new System.Drawing.Size(90, 28);
		this.ultraButton3.SupportThemes = false;
		this.ultraButton3.TabIndex = 6;
		this.ultraButton3.Text = "結  束";
		this.ultraButton3.Click += new System.EventHandler(ultraButton3_Click);
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.Magenta;
		this.imageList2.Images.SetKeyName(0, "");
		this.imageList2.Images.SetKeyName(1, "");
		this.imageList2.Images.SetKeyName(2, "");
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.White;
		this.imageList1.Images.SetKeyName(0, "");
		this.imageList1.Images.SetKeyName(1, "");
		this.pnlParent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.pnlParent.Controls.Add(this.gridMrsBase2);
		this.pnlParent.Controls.Add(this.panel7);
		this.pnlParent.Dock = System.Windows.Forms.DockStyle.Top;
		this.pnlParent.Location = new System.Drawing.Point(0, 77);
		this.pnlParent.Name = "pnlParent";
		this.pnlParent.Size = new System.Drawing.Size(1086, 128);
		this.pnlParent.TabIndex = 6;
		this.gridMrsBase2._ExcelFileName = "";
		this.gridMrsBase2._ExcelSheeName = "";
		this.gridMrsBase2._IsOpenExcelAfterExport = false;
		this.gridMrsBase2.AllowEditing = false;
		this.gridMrsBase2.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
		this.gridMrsBase2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridMrsBase2.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
		this.gridMrsBase2.ColumnInfo = resources.GetString("gridMrsBase2.ColumnInfo");
		this.gridMrsBase2.ContextMenuStrip = this.contextMenuStrip1;
		this.gridMrsBase2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridMrsBase2.ExtendLastCol = true;
		this.gridMrsBase2.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridMrsBase2.ForeColor = System.Drawing.Color.Black;
		this.gridMrsBase2.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.gridMrsBase2.IsProcessUndo = false;
		this.gridMrsBase2.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.gridMrsBase2.Location = new System.Drawing.Point(0, 24);
		this.gridMrsBase2.Name = "gridMrsBase2";
		this.gridMrsBase2.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.gridMrsBase2.ShowCursor = true;
		this.gridMrsBase2.ShowToolTipOnNarrowColumn = true;
		this.gridMrsBase2.Size = new System.Drawing.Size(1084, 102);
		this.gridMrsBase2.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridMrsBase2.Styles"));
		this.gridMrsBase2.TabIndex = 10;
		this.gridMrsBase2.UndoMax = 10;
		this.gridMrsBase2.Click += new System.EventHandler(gridMrsBase2_Click);
		this.gridMrsBase2.MouseMove += new System.Windows.Forms.MouseEventHandler(gridMrsBase2_MouseMove);
		this.panel7.Controls.Add(this.btnAddBookList);
		this.panel7.Controls.Add(this.BtnReCalSmall);
		this.panel7.Controls.Add(this.ultraButton9);
		this.panel7.Controls.Add(this.ultraLabel2);
		this.panel7.Controls.Add(this.ultraButton2);
		this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel7.Location = new System.Drawing.Point(0, 0);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(1084, 24);
		this.panel7.TabIndex = 1;
		appearance27.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance27.Image = resources.GetObject("appearance6.Image");
		appearance27.ImageHAlign = Infragistics.Win.HAlign.Center;
		this.btnAddBookList.Appearance = appearance27;
		this.btnAddBookList.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.btnAddBookList.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.btnAddBookList.Dock = System.Windows.Forms.DockStyle.Right;
		this.btnAddBookList.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnAddBookList.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnAddBookList.Location = new System.Drawing.Point(998, 0);
		this.btnAddBookList.Name = "btnAddBookList";
		this.btnAddBookList.ShowFocusRect = false;
		this.btnAddBookList.ShowOutline = false;
		this.btnAddBookList.Size = new System.Drawing.Size(22, 24);
		this.btnAddBookList.SupportThemes = false;
		this.btnAddBookList.TabIndex = 6;
		this.toolTip1.SetToolTip(this.btnAddBookList, "父項查詢結果加到書籤");
		this.btnAddBookList.Click += new System.EventHandler(btnAddBookList_Click);
		appearance28.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance28.Image = resources.GetObject("appearance7.Image");
		appearance28.ImageHAlign = Infragistics.Win.HAlign.Center;
		this.BtnReCalSmall.Appearance = appearance28;
		this.BtnReCalSmall.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.BtnReCalSmall.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.BtnReCalSmall.Dock = System.Windows.Forms.DockStyle.Right;
		this.BtnReCalSmall.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.BtnReCalSmall.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.BtnReCalSmall.Location = new System.Drawing.Point(1020, 0);
		this.BtnReCalSmall.Name = "BtnReCalSmall";
		this.BtnReCalSmall.ShowFocusRect = false;
		this.BtnReCalSmall.ShowOutline = false;
		this.BtnReCalSmall.Size = new System.Drawing.Size(22, 24);
		this.BtnReCalSmall.SupportThemes = false;
		this.BtnReCalSmall.TabIndex = 5;
		this.toolTip1.SetToolTip(this.BtnReCalSmall, "查詢結果項目重新小計");
		this.BtnReCalSmall.Click += new System.EventHandler(BtnReCalSmall_Click);
		appearance29.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance29.Image = resources.GetObject("appearance8.Image");
		appearance29.ImageBackgroundOrigin = Infragistics.Win.ImageBackgroundOrigin.Container;
		appearance29.ImageHAlign = Infragistics.Win.HAlign.Center;
		this.ultraButton9.Appearance = appearance29;
		this.ultraButton9.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraButton9.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.ultraButton9.Dock = System.Windows.Forms.DockStyle.Right;
		this.ultraButton9.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ultraButton9.Location = new System.Drawing.Point(1042, 0);
		this.ultraButton9.Name = "ultraButton9";
		this.ultraButton9.ShowFocusRect = false;
		this.ultraButton9.ShowOutline = false;
		this.ultraButton9.Size = new System.Drawing.Size(22, 24);
		this.ultraButton9.SupportThemes = false;
		this.ultraButton9.TabIndex = 4;
		this.toolTip1.SetToolTip(this.ultraButton9, "查詢結果匯出EXCEL(不能當轉入用)");
		this.ultraButton9.Click += new System.EventHandler(ultraButton9_Click);
		appearance30.ForeColor = System.Drawing.Color.White;
		appearance30.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel2.Appearance = appearance30;
		this.ultraLabel2.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel2.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.ultraLabel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ultraLabel2.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel2.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Padding = new System.Drawing.Size(5, 0);
		this.ultraLabel2.Size = new System.Drawing.Size(1064, 24);
		this.ultraLabel2.TabIndex = 1;
		this.ultraLabel2.Text = "父項查詢結果列表";
		appearance31.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance31.ForeColor = System.Drawing.Color.White;
		this.ultraButton2.Appearance = appearance31;
		this.ultraButton2.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraButton2.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.ultraButton2.Dock = System.Windows.Forms.DockStyle.Right;
		this.ultraButton2.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ultraButton2.Location = new System.Drawing.Point(1064, 0);
		this.ultraButton2.Name = "ultraButton2";
		this.ultraButton2.ShowFocusRect = false;
		this.ultraButton2.ShowOutline = false;
		this.ultraButton2.Size = new System.Drawing.Size(20, 24);
		this.ultraButton2.SupportThemes = false;
		this.ultraButton2.TabIndex = 0;
		this.ultraButton2.Text = "X";
		this.toolTip1.SetToolTip(this.ultraButton2, "關閉父項查詢結果");
		this.ultraButton2.Click += new System.EventHandler(ultraButton2_Click);
		this.imageList3.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList3.ImageStream");
		this.imageList3.TransparentColor = System.Drawing.Color.White;
		this.imageList3.Images.SetKeyName(0, "");
		this.imageList3.Images.SetKeyName(1, "");
		this.ultraLabel1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.ultraLabel1.BackColor = System.Drawing.Color.GreenYellow;
		this.ultraLabel1.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.Solid;
		this.ultraLabel1.Location = new System.Drawing.Point(8, 9);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(56, 20);
		this.ultraLabel1.TabIndex = 16;
		this.ultraLabel3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.ultraLabel3.BackColor = System.Drawing.Color.Gold;
		this.ultraLabel3.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.Solid;
		this.ultraLabel3.Location = new System.Drawing.Point(348, 9);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(56, 20);
		this.ultraLabel3.TabIndex = 18;
		this.ultraLabel4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.ultraLabel4.Location = new System.Drawing.Point(64, 13);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(288, 23);
		this.ultraLabel4.TabIndex = 19;
		this.ultraLabel4.Text = "工項內任一人機料雜比率 > 100%";
		this.ultraLabel5.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.ultraLabel5.Location = new System.Drawing.Point(408, 13);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(304, 23);
		this.ultraLabel5.TabIndex = 20;
		this.ultraLabel5.Text = "工項的人機料雜比率總和≠100%";
		this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
		this.splitter1.Location = new System.Drawing.Point(0, 205);
		this.splitter1.Name = "splitter1";
		this.splitter1.Size = new System.Drawing.Size(1086, 5);
		this.splitter1.TabIndex = 26;
		this.splitter1.TabStop = false;
		this.panel2.Controls.Add(this.ultraLabel1);
		this.panel2.Controls.Add(this.ultraLabel3);
		this.panel2.Controls.Add(this.ultraLabel4);
		this.panel2.Controls.Add(this.ultraLabel5);
		this.panel2.Controls.Add(this.ultraButton3);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 527);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(1086, 36);
		this.panel2.TabIndex = 27;
		this.toolTip1.ShowAlways = true;
		this.contextMenuStrip1.Font = new System.Drawing.Font("微軟正黑體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.mnuExportGrid2 });
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.contextMenuStrip1.Size = new System.Drawing.Size(153, 50);
		this.mnuExportGrid2.Name = "mnuExportGrid2";
		this.mnuExportGrid2.Size = new System.Drawing.Size(152, 24);
		this.mnuExportGrid2.Text = "匯出結果";
		this.mnuExportGrid2.Click += new System.EventHandler(mnuExportGrid2_Click);
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.ClientSize = new System.Drawing.Size(1086, 563);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.splitter1);
		base.Controls.Add(this.pnlParent);
		base.Controls.Add(this._FormBudgetRes_Toolbars_Dock_Area_Left);
		base.Controls.Add(this._FormBudgetRes_Toolbars_Dock_Area_Right);
		base.Controls.Add(this._FormBudgetRes_Toolbars_Dock_Area_Top);
		base.Controls.Add(this._FormBudgetRes_Toolbars_Dock_Area_Bottom);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.KeyPreview = true;
		base.MinimizeBox = false;
		base.Name = "FormBudgetRes";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "專案工項維護";
		base.Load += new System.EventHandler(FormBudgetRes_Load);
		base.Activated += new System.EventHandler(FormBudgetRes_Activated);
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridMrsBase1).EndInit();
		this.panel1.ResumeLayout(false);
		this.pnlParent.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridMrsBase2).EndInit();
		this.panel7.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.contextMenuStrip1.ResumeLayout(false);
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

	public FormBudgetRes()
	{
		try
		{
			InitializeComponent();
			base.Width = (int)((double)Screen.PrimaryScreen.WorkingArea.Width * 0.85);
			base.Height = (int)((double)Screen.PrimaryScreen.WorkingArea.Height * 0.8);
			GridCols = gridMrsBase1.Cols.Count;
			Grid2Cols = gridMrsBase2.Cols.Count;
			GridColsSquence = new object[GridCols, 10];
			Grid2ColsSquence = new object[Grid2Cols, 10];
			CellStyle csCb = gridMrsBase1.Styles.Add("ComboList");
			csCb.DataType = typeof(short);
			csCb.ComboList = "|0|1|2|3|4";
			csCb.ForeColor = Color.Navy;
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
			CellStyle cs1 = gridMrsBase1.Styles.Add("EditMode");
			cs1.DataType = typeof(Image);
			cs1.ImageAlign = ImageAlignEnum.RightCenter;
			CellStyle cs2 = gridMrsBase2.Styles.Add("EditMode");
			cs2.DataType = typeof(Image);
			cs2.ImageAlign = ImageAlignEnum.RightCenter;
			string sHideCols = CommonMethods.GetDebugValue("FormBudgetRes", "HideCols");
			HideCols(Convert.ToBoolean((sHideCols == "") ? "True" : sHideCols));
			FORM_STATUS = FormStatus.Active;
		}
		catch (Exception ex)
		{
			MessageBox.Show("FormBudgetRes Error:" + ex.Message);
		}
	}

	private void SettingDecimal()
	{
		try
		{
			DataTable DTDecimal = new DataTable();
			ArrayList aArr = new ArrayList();
			aArr.Clear();
			aArr.Add(F_UserID);
			aArr.Add("專案工料" + projectCode);
			Archnowledge.Pcces.BUDClass.PubDecimal dbDecimal = new Archnowledge.Pcces.BUDClass.PubDecimal(aArr);
			DTDecimal = dbDecimal.ListItem("", projectCode);
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
		catch (Exception ex)
		{
			MessageBox.Show("SettingDecimal Error:" + ex.Message);
		}
	}

	private void HideCols(bool IsHide)
	{
		try
		{
			if (IsHide)
			{
				gridMrsBase1.Cols["Analysis"].Visible = false;
				gridMrsBase1.Cols["SNo"].Visible = false;
				gridMrsBase1.Cols["PubCode"].Visible = false;
				gridMrsBase1.Cols["QtyDec"].Visible = false;
				gridMrsBase1.Cols["CostDec"].Visible = false;
				gridMrsBase1.Cols["AmtDec"].Visible = false;
				gridMrsBase2.Cols["Analysis"].Visible = false;
				gridMrsBase2.Cols["SNo"].Visible = false;
				gridMrsBase2.Cols["PubCode"].Visible = false;
			}
			if ((ultraToolbarsManager1.Tools["mnuViewItemSurName"] as StateButtonTool).Checked)
			{
				gridMrsBase1.Cols["surName"].Visible = true;
				gridMrsBase2.Cols["surName"].Visible = true;
				return;
			}
			F_iCount = "Inital";
			(ultraToolbarsManager1.Tools["mnuViewItemUnSurName"] as StateButtonTool).Checked = true;
			gridMrsBase1.Cols["surName"].Visible = false;
			gridMrsBase2.Cols["surName"].Visible = false;
		}
		catch (Exception ex)
		{
			MessageBox.Show("HideCols Error:" + ex.Message);
		}
	}

	private void RememberColsProps()
	{
		try
		{
			for (int i = 0; i < GridCols; i++)
			{
				GridColsSquence[i, 0] = gridMrsBase1.Cols[i].Name;
				GridColsSquence[i, 1] = gridMrsBase1.Cols[i].Caption;
				GridColsSquence[i, 2] = gridMrsBase1.Cols[i].Width;
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
				if (gridMrsBase1.Cols[i].Name == "usrQty")
				{
					if (F_AnaQty > 0)
					{
						GridColsSquence[i, 5] = "###,###,###,##0." + "0".PadLeft(F_AnaQty, '0');
					}
					else
					{
						GridColsSquence[i, 5] = "###,###,###,##0";
					}
				}
				if (gridMrsBase1.Cols[i].Name == "Cost")
				{
					if (F_AnaCst > 0)
					{
						GridColsSquence[i, 5] = "###,###,###,##0." + "0".PadLeft(F_AnaCst, '0');
					}
					else
					{
						GridColsSquence[i, 5] = "###,###,###,##0";
					}
				}
				if (gridMrsBase1.Cols[i].Name == "usrAmt")
				{
					if (F_AnaAmt > 0)
					{
						GridColsSquence[i, 5] = "###,###,###,##0." + "0".PadLeft(F_AnaAmt, '0');
					}
					else
					{
						GridColsSquence[i, 5] = "###,###,###,##0";
					}
				}
				GridColsSquence[i, 7] = gridMrsBase1.Cols[i].TextAlign;
				GridColsSquence[i, 8] = gridMrsBase1.Cols[i].AllowDragging;
				GridColsSquence[i, 9] = gridMrsBase1.Cols[i].AllowResizing;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("RememberColsProps Error:" + ex.Message);
		}
	}

	private void RememberColsProps2()
	{
		try
		{
			for (int i = 0; i < Grid2Cols; i++)
			{
				Grid2ColsSquence[i, 0] = gridMrsBase2.Cols[i].Name;
				Grid2ColsSquence[i, 1] = gridMrsBase2.Cols[i].Caption;
				Grid2ColsSquence[i, 2] = gridMrsBase2.Cols[i].Width;
				if (gridMrsBase2.Cols[i].Name == "AnaImg")
				{
					Grid2ColsSquence[i, 3] = typeof(Image);
				}
				else
				{
					Grid2ColsSquence[i, 3] = gridMrsBase2.Cols[i].DataType;
				}
				Grid2ColsSquence[i, 4] = gridMrsBase2.Cols[i].Visible;
				Grid2ColsSquence[i, 5] = gridMrsBase2.Cols[i].Format;
				Grid2ColsSquence[i, 6] = gridMrsBase2.Cols[i].AllowEditing;
				string columnName = gridMrsBase2.Cols[i].Name;
				if (columnName == "usrQty" || columnName == "qtySubtotal")
				{
					if (F_AnaQty > 0)
					{
						Grid2ColsSquence[i, 5] = "###,###,###,##0." + "0".PadLeft(F_AnaQty, '0');
					}
					else
					{
						Grid2ColsSquence[i, 5] = "###,###,###,##0";
					}
				}
				if (columnName == "Cost")
				{
					if (F_AnaCst > 0)
					{
						Grid2ColsSquence[i, 5] = "###,###,###,##0." + "0".PadLeft(F_AnaCst, '0');
					}
					else
					{
						Grid2ColsSquence[i, 5] = "###,###,###,##0";
					}
				}
				if (columnName == "usrAmt")
				{
					if (F_AnaAmt > 0)
					{
						Grid2ColsSquence[i, 5] = "###,###,###,##0." + "0".PadLeft(F_AnaAmt, '0');
					}
					else
					{
						Grid2ColsSquence[i, 5] = "###,###,###,##0";
					}
				}
				Grid2ColsSquence[i, 7] = gridMrsBase2.Cols[i].TextAlign;
				Grid2ColsSquence[i, 8] = gridMrsBase2.Cols[i].AllowDragging;
				Grid2ColsSquence[i, 9] = gridMrsBase2.Cols[i].AllowResizing;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("RememberColsProps2 Error:" + ex.Message);
		}
	}

	private void SetGridColumn()
	{
		try
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
				gridMrsBase1.Cols[i].AllowDragging = (bool)GridColsSquence[i, 8];
				gridMrsBase1.Cols[i].AllowResizing = (bool)GridColsSquence[i, 9];
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("SetGridColumn Error:" + ex.Message);
		}
	}

	private void BindToGrid(string sCriti)
	{
		gridMrsBase1.Redraw = false;
		int iAnaProblem = 0;
		RememberColsProps();
		bool useRowfilter = true;
		if (projMrsA != null)
		{
			dsResource = projMrsA.GetResource(projectCode);
			dtResource = dsResource.Tables[0];
		}
		else if (sCriti != "[PARENT]")
		{
			GetProjMrsBaseData(sCriti);
			useRowfilter = false;
		}
		DataView dvResource = dtResource.DefaultView;
		if (useRowfilter)
		{
			dvResource.RowFilter = ViewFilterGenerate();
		}
		dvResource.Sort = " pccesCode ASC ";
		CellStyle CS1 = gridMrsBase1.Styles.Add("AnalysisColor");
		CellStyle CS2 = gridMrsBase1.Styles.Add("EMColor");
		CellStyle CSL = gridMrsBase1.Styles.Add("LColor");
		CellStyle CS3 = gridMrsBase1.Styles.Add("WColor");
		CellStyle CS4 = gridMrsBase1.Styles.Add("RateErr");
		CellStyle CS5 = gridMrsBase1.Styles.Add("RateWarning");
		CellStyle CSAnaErr = gridMrsBase1.Styles.Add("AnalysisErr");
		CellStyle CSWCost = gridMrsBase1.Styles.Add("WCost");
		CellStyle CSD = gridMrsBase1.Styles.Add("DocDownloaded");
		CS1.ForeColor = Color.Red;
		CS2.ForeColor = Color.Teal;
		CSL.ForeColor = Color.Teal;
		CSAnaErr.BackColor = Color.Violet;
		System.Drawing.Font LFont = new System.Drawing.Font("細明體", 11f, FontStyle.Bold);
		CSL.Font = LFont;
		CS3.ForeColor = Color.Purple;
		CS4.ForeColor = Color.Black;
		CS4.BackColor = Color.GreenYellow;
		CS5.ForeColor = Color.Black;
		CS5.BackColor = Color.Gold;
		CSWCost.ForeColor = Color.Transparent;
		CSD.BackColor = Color.PaleGoldenrod;
		gridMrsBase1.Clear(ClearFlags.All);
		gridMrsBase1.Select();
		gridMrsBase1.Rows.Count = dvResource.Count + 1;
		SetGridColumn();
		if ((ultraToolbarsManager1.Tools["mnuViewItemSurName"] as StateButtonTool).Checked)
		{
			gridMrsBase1.Cols["surName"].Visible = true;
			gridMrsBase2.Cols["surName"].Visible = true;
		}
		else
		{
			gridMrsBase1.Cols["surName"].Visible = false;
			gridMrsBase2.Cols["surName"].Visible = false;
		}
		iLEMW_RateErr = 0;
		gridMrsBase1.Redraw = false;
		for (int i = 0; i < dvResource.Count; i++)
		{
			double dRateSum = 0.0;
			bool IsCS4 = false;
			string sItemClass = "";
			string PccesCode = ArchConvert.Obj2String(dvResource[i]["pccesCode"]);
			if (PccesCode.Length > 0)
			{
				sItemClass = PccesCode.Substring(0, 1);
			}
			C1.Win.C1FlexGrid.Row theRow = gridMrsBase1.Rows[i + 1];
			theRow["PccesCode"] = PccesCode;
			if (sItemClass == "E" || sItemClass == "M")
			{
				theRow.Style = gridMrsBase1.Styles["EMColor"];
			}
			else if (sItemClass == "L")
			{
				theRow.Style = gridMrsBase1.Styles["EMColor"];
			}
			else if (sItemClass == "W")
			{
				theRow.Style = gridMrsBase1.Styles["WColor"];
			}
			theRow["CName"] = dvResource[i]["cName"];
			if (dvResource[i]["analysis"].ToString().Trim() == "1")
			{
				theRow["Analysis"] = true;
				theRow.Style = gridMrsBase1.Styles["AnalysisColor"];
				CellRange rg = gridMrsBase1.GetCellRange(i + 1, gridMrsBase1.Cols["AnaImg"].SafeIndex);
				rg.Style = gridMrsBase1.Styles["img"];
				rg.Image = imageList1.Images[0];
			}
			else
			{
				theRow["Analysis"] = false;
			}
			theRow["UnitName"] = dvResource[i]["unitName"];
			theRow["Rate"] = dvResource[i]["rate"];
			theRow["CostKind"] = dvResource[i]["costKind"];
			theRow["LRate"] = dvResource[i]["lRate"];
			if (PubTools.Str2Double(dvResource[i]["lRate"]) > 100.0)
			{
				IsCS4 = true;
				theRow.Style = CS4;
				iLEMW_RateErr++;
			}
			theRow["ERate"] = dvResource[i]["eRate"];
			if (PubTools.Str2Double(dvResource[i]["eRate"]) > 100.0)
			{
				IsCS4 = true;
				theRow.Style = CS4;
				iLEMW_RateErr++;
			}
			theRow["MRate"] = dvResource[i]["mRate"];
			if (PubTools.Str2Double(dvResource[i]["mRate"]) > 100.0)
			{
				IsCS4 = true;
				theRow.Style = CS4;
				iLEMW_RateErr++;
			}
			theRow["WRate"] = dvResource[i]["wRate"];
			if (PubTools.Str2Double(dvResource[i]["wRate"]) > 100.0)
			{
				IsCS4 = true;
				theRow.Style = CS4;
				iLEMW_RateErr++;
			}
			theRow["XNameC"] = dvResource[i]["xNameC"];
			theRow["Memo"] = dvResource[i]["memo"];
			theRow["PubCode"] = dvResource[i]["pubCode"];
			theRow["Cost"] = dvResource[i]["cost"];
			theRow["usrQty"] = dvResource[i]["usrQty"];
			theRow["LockCost"] = dvResource[i]["LockCost"].ToString().Trim() == "1";
			theRow["usrAmt"] = dvResource[i]["usrAmt"];
			theRow["Prec"] = dvResource[i]["Prec"];
			theRow["surName"] = dvResource[i]["surName"];
			theRow["fixPrice"] = dvResource[i]["fixPrice"].ToString().Trim() == "1";
			theRow["Account"] = dvResource[i]["Account"];
			theRow["extendCode"] = dvResource[i]["extendCode"];
			if (_ActionName == PccesFormAction.BUD)
			{
				theRow["Lock"] = ArchConvert.Obj2Bool(dvResource[i]["Lock"]);
				theRow["IsGreenItem"] = ArchConvert.Obj2Bool(dvResource[i]["IsGreenItem"]);
				theRow["IsGreenMethod"] = ArchConvert.Obj2Bool(dvResource[i]["IsGreenMethod"]);
				theRow["IsGreenMaterial"] = ArchConvert.Obj2Bool(dvResource[i]["IsGreenMaterial"]);
				theRow["IsGreenEnergy"] = ArchConvert.Obj2Bool(dvResource[i]["IsGreenEnergy"]);
				theRow["ItemType"] = ItemType.GetItemType(dvResource[i]["IsCommonItem"].ToString());
				if (dtResource.Columns.Contains("ItemQty"))
				{
					theRow["ItemQty"] = ArchConvert.Obj2Decimal(dvResource[i]["ItemQty"]);
					theRow["ItemAmt"] = ArchConvert.Obj2Decimal(dvResource[i]["ItemAmt"]);
				}
				theRow["correct"] = ArchConvert.Obj2String(dvResource[i]["correct"]);
				theRow["confirm"] = ArchConvert.Obj2String(dvResource[i]["confirm"]);
				theRow["CompareErrState"] = ArchConvert.Obj2String(dvResource[i]["CompareErrState"]);
				theRow["CorrectCName"] = ArchConvert.Obj2String(dvResource[i]["CorrectCName"]);
				theRow["CorrectUnitName"] = ArchConvert.Obj2String(dvResource[i]["CorrectUnitName"]);
			}
			dRateSum = PubTools.Str2Double(dvResource[i]["lRate"]) + PubTools.Str2Double(dvResource[i]["eRate"]) + PubTools.Str2Double(dvResource[i]["mRate"]) + PubTools.Str2Double(dvResource[i]["wRate"]);
			if (!IsCS4 && (dRateSum < 99.99 || dRateSum > 100.01) && sItemClass != "#" && dvResource[i]["costKind"].ToString().Trim() != "Z")
			{
				theRow.Style = CS5;
			}
			if (dvResource[i]["costKind"].ToString().Trim() != "")
			{
				CellRange Crg1 = gridMrsBase1.GetCellRange(i + 1, gridMrsBase1.Cols["Cost"].SafeIndex);
				Crg1.Style = CSWCost;
				theRow["Cost"] = null;
			}
			CellRange RAccMode = gridMrsBase1.GetCellRange(i + 1, gridMrsBase1.Cols["PwrSet"].SafeIndex, i + 1, gridMrsBase1.Cols["PwrSet"].SafeIndex);
			RAccMode.Style = gridMrsBase1.Styles["ComboListPS"];
			if (dvResource[i]["PwrSet"] != DBNull.Value)
			{
				theRow["PwrSet"] = PwrSet.GetName(dsPwrSet, PubTools.Str2Int(dvResource[i]["PwrSet"]));
			}
			else
			{
				theRow["PwrSet"] = PwrSet.GetDefaultName(dsPwrSet);
			}
			if (dtResource.Columns.Contains("AddOnDownLoadNum") && ArchConvert.Obj2Int(dvResource[i]["AddOnDownLoadNum"]) > 0)
			{
				gridMrsBase1.SetCellStyle(i + 1, gridMrsBase1.Cols["pccesCode"].SafeIndex, CSD);
			}
		}
		gridMrsBase1.Redraw = true;
		SetColsEditSymbol();
		F_MnyRateType = 0;
		SetPopupMenuEnable();
		gridMrsBase1.Select();
		if (iAnaProblem > 0)
		{
			MessageBox.Show(this, "檢查到有 " + iAnaProblem + " 筆單價分析項目有問題\n\n請檢查項目底色為[粉紫色]的單價分析項目。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		if (CheckProjMrsA())
		{
			MessageBox.Show(this, "檢查到開頭有【#】字號的工項代碼\n\n請檢查是否為說明項，若不是請修正!!", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		if (FormActionName == PccesFormAction.BID)
		{
			StatusBar.Panels[0].Text = " 資料筆數:" + dvResource.Count;
			return;
		}
		BudProject bp = new BudProject();
		bp.GetRates(projectCode, out var correctRate, out var weightedCorrectRate, out var confirmRate, out var _);
		StatusBar.Panels[0].Text = " 資料總筆數:" + dvResource.Count + ", 編碼正確率:" + correctRate.ToString("00.00") + "%, 加權正確率:" + weightedCorrectRate.ToString("00.00") + "%, 綱要編碼正確率:" + confirmRate.ToString("00.00") + "%";
		StatusBar.Panels[1].Text = "";
		gridMrsBase1.Redraw = true;
	}

	private bool PccesCodeIsProblem(string sPccesCode, ref DataTable DT_Check)
	{
		bool RetV = false;
		try
		{
			for (int i = 0; i < DT_Check.Rows.Count; i++)
			{
				if (sPccesCode.Trim() == DT_Check.Rows[i]["PccesCode"].ToString().Trim())
				{
					RetV = true;
					break;
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("BindToGrid Error:" + ex.Message);
		}
		return RetV;
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
					rg.Image = imageList1.Images[1];
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("SetColsEditSymbol Error:" + ex.Message);
		}
	}

	private void GetProjMrsBaseData(string sWhere)
	{
		try
		{
			ArrayList aArr = new ArrayList();
			aArr.Clear();
			aArr.Add(F_UserID);
			aArr.Add("WinFORM 基本工料");
			Archnowledge.Pcces.BUDClass.MrsBaseA dbMrsBaseA = new Archnowledge.Pcces.BUDClass.MrsBaseA(F_UserID, aArr);
			dbMrsBaseA.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
			dbMrsBaseA.ps_projectcode = projectCode;
			dbMrsBaseA.ps_Issue = F_chgCount;
			dtResource = dbMrsBaseA.ListItem(sWhere);
		}
		catch (Exception ex)
		{
			MessageBox.Show("GetProjMrsBaseData Error:" + ex.Message);
		}
	}

	private string ViewFilterGenerate()
	{
		string RetV = "";
		try
		{
			string sWORK = "";
			string sUsual = "";
			int CriteriaCount = 0;
			RetV += " 1=1 ";
			if ((ultraToolbarsManager1.Tools["mnuViewAnalysis"] as StateButtonTool).Checked)
			{
				RetV += " And Analysis ='1' ";
			}
			if ((ultraToolbarsManager1.Tools["mnuViewNoAnalysis"] as StateButtonTool).Checked)
			{
				RetV += " And Analysis <>'1' ";
			}
			if ((ultraToolbarsManager1.Tools["mnuItem"] as StateButtonTool).Checked)
			{
				if (CriteriaCount > 0)
				{
					sWORK += " OR ";
				}
				sWORK += " SUBSTRING(pccesCode,1,1) not in ('L','E','M','W','l','e','m','w') ";
				CriteriaCount++;
			}
			if ((ultraToolbarsManager1.Tools["mnuLabor"] as StateButtonTool).Checked)
			{
				if (CriteriaCount > 0)
				{
					sWORK += " OR ";
				}
				sWORK += " SUBSTRING(pccesCode,1,1) ='L' OR SUBSTRING(pccesCode,1,1) ='l' ";
				CriteriaCount++;
			}
			if ((ultraToolbarsManager1.Tools["mnuEquip"] as StateButtonTool).Checked)
			{
				if (CriteriaCount > 0)
				{
					sWORK += " OR ";
				}
				sWORK += " SUBSTRING(pccesCode,1,1) ='E' OR SUBSTRING(pccesCode,1,1) ='e' ";
				CriteriaCount++;
			}
			if ((ultraToolbarsManager1.Tools["mnuMaterial"] as StateButtonTool).Checked)
			{
				if (CriteriaCount > 0)
				{
					sWORK += " OR ";
				}
				sWORK += " SUBSTRING(pccesCode,1,1) ='M' OR SUBSTRING(pccesCode,1,1) ='m'  ";
				CriteriaCount++;
			}
			if ((ultraToolbarsManager1.Tools["mnuWaste"] as StateButtonTool).Checked)
			{
				if (CriteriaCount > 0)
				{
					sWORK += " OR ";
				}
				sWORK += " SUBSTRING(pccesCode,1,1) ='W' OR SUBSTRING(pccesCode,1,1) ='w'  ";
				CriteriaCount++;
			}
			if ((ultraToolbarsManager1.Tools["mnuCostIsZero"] as StateButtonTool).Checked)
			{
				sUsual += " ((Trim(costKind) <> '#' And usrAmt = 0)  Or (Cost = 0 And (costKind is null Or Trim(costKind) = '')))  Or ((Trim(costKind) <> '#' And usrQty = 0)  Or (usrQty = 0 And (costKind is null Or Trim(costKind) = '')))";
			}
			if ((ultraToolbarsManager1.Tools["mnuCalcErr"] as StateButtonTool).Checked)
			{
				sUsual += " CalcError= '1' ";
			}
			if ((ultraToolbarsManager1.Tools["mnuAnaMinus"] as StateButtonTool).Checked)
			{
				sUsual += " HasNegativeChild= '1' ";
			}
			if (F_MnyRateType != 0)
			{
				if (CriteriaCount > 0)
				{
					sWORK += ") AND (";
				}
				if (F_MnyRateType == 1)
				{
					sWORK = sWORK + " Prec > " + F_Rate1;
				}
				if (F_MnyRateType == 2)
				{
					sWORK = sWORK + " Prec < " + F_Rate1;
				}
				if (F_MnyRateType == 3)
				{
					string text = sWORK;
					sWORK = text + " (Prec > " + F_Rate1 + " AND Prec <" + F_Rate2 + ")";
				}
				CriteriaCount++;
			}
			if (ultraToolbarsManager1.Tools["mnuIncorrect"].SharedProps.Visible && (ultraToolbarsManager1.Tools["mnuIncorrect"] as StateButtonTool).Checked)
			{
				if (CriteriaCount > 0)
				{
					sWORK += " AND ";
				}
				sWORK += " Correct ='否'  ";
				CriteriaCount++;
			}
			if (ultraToolbarsManager1.Tools["mnuCorrectItems"].SharedProps.Visible && (ultraToolbarsManager1.Tools["mnuCorrectItems"] as StateButtonTool).Checked)
			{
				if (CriteriaCount > 0)
				{
					sWORK += " AND ";
				}
				sWORK += " Correct ='是'  ";
				CriteriaCount++;
			}
			if (ultraToolbarsManager1.Tools["mnuNotfit"].SharedProps.Visible && (ultraToolbarsManager1.Tools["mnuNotfit"] as StateButtonTool).Checked)
			{
				if (CriteriaCount > 0)
				{
					sWORK += " AND ";
				}
				sWORK += " Confirm = '否'  ";
				CriteriaCount++;
			}
			if (sWORK != "")
			{
				RetV = RetV + " AND (" + sWORK + ") ";
			}
			if (sUsual != "")
			{
				RetV = RetV + " AND (" + sUsual + ") ";
			}
			if (RunExtraSearchCriteria && ExtraSearchCriteria != string.Empty)
			{
				RetV = RetV + " AND (" + ExtraSearchCriteria + ") ";
			}
			RunExtraSearchCriteria = false;
		}
		catch (Exception ex)
		{
			MessageBox.Show("ViewFilterGenerate Error:" + ex.Message);
		}
		return RetV;
	}

	private void FormBudgetRes_Load(object sender, EventArgs e)
	{
		if (SysConfig.SysChangeManagement)
		{
			ultraToolbarsManager1.Tools["mnuParentFromBudget"].SharedProps.Visible = true;
		}
		else
		{
			ultraToolbarsManager1.Tools["mnuParentFromBudget"].SharedProps.Visible = true;
		}
		if (FormActionName == PccesFormAction.BUD)
		{
			projMrsA = new BudProjMrsA();
			itemA = new BudItemA();
		}
		else if (FormActionName == PccesFormAction.BID)
		{
			projMrsA = new BidProjMrsA();
			itemA = new SubItemA();
			ultraToolbarsManager1.Tools["mnuIncorrect"].SharedProps.Visible = false;
			ultraToolbarsManager1.Tools["mnuNotfit"].SharedProps.Visible = false;
			ultraToolbarsManager1.Tools["mnuCalculateCorrectness"].SharedProps.Visible = false;
			ultraToolbarsManager1.Tools["mnuCorrectItems"].SharedProps.Visible = false;
			ultraToolbarsManager1.Tools["mnuCorrectCName"].SharedProps.Visible = false;
			ultraToolbarsManager1.Tools["mnuExpAllCorrect"].SharedProps.Visible = false;
			ultraToolbarsManager1.Tools["mnuExpNotCorrect"].SharedProps.Visible = false;
			gridMrsBase1.Cols["CompareErrState"].Visible = false;
			gridMrsBase1.Cols["CorrectCName"].Visible = false;
			gridMrsBase1.Cols["CorrectUnitName"].Visible = false;
		}
		else if (FormActionName == PccesFormAction.SplitContract)
		{
			projMrsA = new SubProjMrsA();
			itemA = new SubItemA();
			gridMrsBase1.Enabled = !HasApproved;
		}
		else if (FormActionName == PccesFormAction.SubChange)
		{
			projMrsA = new SubChgProjMrsA();
			itemA = new SubChgItemA();
		}
		projMrsA.ProjMrsSync(projectCode, 0);
		bool isBudget = FormActionName == PccesFormAction.BUD;
		gridMrsBase1.Cols["fixPrice"].Visible = isBudget;
		gridMrsBase2.Cols["fixPrice"].Visible = isBudget;
		gridMrsBase1.Cols["IsGreenItem"].Visible = isBudget;
		gridMrsBase1.Cols["IsGreenMethod"].Visible = isBudget;
		gridMrsBase1.Cols["IsGreenMaterial"].Visible = isBudget;
		gridMrsBase1.Cols["IsGreenEnergy"].Visible = isBudget;
		gridMrsBase1.Cols["ItemType"].Visible = isBudget;
		gridMrsBase1.Cols["correct"].Visible = isBudget;
		gridMrsBase1.Cols["confirm"].Visible = isBudget;
		bool EnablePwrSet = SysConfig.SysEnablePwrSet;
		gridMrsBase1.Cols["PwrSet"].Visible = EnablePwrSet;
		gridMrsBase1.Cols["Account"].Visible = EnablePwrSet;
		gridMrsBase2.Cols["PwrSet"].Visible = EnablePwrSet;
		gridMrsBase2.Cols["Account"].Visible = EnablePwrSet;
		if (FormActionName == PccesFormAction.SplitContract || FormActionName == PccesFormAction.SubChange)
		{
			ultraToolbarsManager1.Tools["MenuImport"].SharedProps.Visible = false;
			ultraToolbarsManager1.Tools["MenuExport"].SharedProps.Visible = false;
		}
		if (PubTools.Str2Boolean(CommonMethods.GetIniValue("COMS", "IsChangeCode")))
		{
			ultraToolbarsManager1.Tools["MenuChangeCode"].SharedProps.Visible = true;
		}
		else
		{
			ultraToolbarsManager1.Tools["MenuChangeCode"].SharedProps.Visible = false;
		}
		pnlParent.Height = 0;
		SettingDecimal();
		if (F_IsSBID)
		{
			BackColor = Color.FromArgb(255, 128, 0);
			ultraToolbarsManager1.Tools["mnuEditItem"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuGetFromMrs"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuGetFromMrs"].SharedProps.Visible = false;
			ultraToolbarsManager1.Tools["MenuImport"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuChangeCode"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuUseMrsCost"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuFillRate"].SharedProps.Enabled = false;
			gridMrsBase1.Cols["LockCost"].AllowEditing = false;
			gridMrsBase1.Cols["Cost"].AllowEditing = false;
			gridMrsBase1.Cols["LRate"].AllowEditing = false;
			gridMrsBase1.Cols["ERate"].AllowEditing = false;
			gridMrsBase1.Cols["MRate"].AllowEditing = false;
			gridMrsBase1.Cols["WRate"].AllowEditing = false;
			gridMrsBase1.Cols["Memo"].AllowEditing = false;
			gridMrsBase1.Cols["Prec"].AllowEditing = false;
			gridMrsBase1.Cols["sNO"].AllowEditing = false;
			gridMrsBase1.Cols["PubCode"].AllowEditing = false;
		}
		else
		{
			ultraToolbarsManager1.Tools["mnuEditItem"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuGetFromMrs"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["MenuImport"].SharedProps.Enabled = true;
			gridMrsBase1.Cols["LockCost"].AllowEditing = true;
			gridMrsBase1.Cols["Cost"].AllowEditing = true;
			gridMrsBase1.Cols["LRate"].AllowEditing = true;
			gridMrsBase1.Cols["ERate"].AllowEditing = true;
			gridMrsBase1.Cols["MRate"].AllowEditing = true;
			gridMrsBase1.Cols["WRate"].AllowEditing = true;
			gridMrsBase1.Cols["Memo"].AllowEditing = true;
			gridMrsBase1.Cols["Prec"].AllowEditing = true;
			gridMrsBase1.Cols["sNO"].AllowEditing = false;
			gridMrsBase1.Cols["PubCode"].AllowEditing = false;
			if (SysConfig.SysComsEnable && budgetType == 2 && (SysConfig.SysIsCheckAccQtyAmt.ToUpper() == "DISABLE" || SysConfig.SysIsCheckAccQtyAmt.ToUpper() == "WARNONLY"))
			{
				gridMrsBase1.Cols["ItemQty"].Visible = true;
				gridMrsBase1.Cols["ItemAmt"].Visible = true;
			}
			else
			{
				gridMrsBase1.Cols["ItemQty"].Visible = false;
				gridMrsBase1.Cols["ItemAmt"].Visible = false;
			}
			if (FormActionName == PccesFormAction.BID)
			{
				ultraToolbarsManager1.Tools["mnuChangeCode"].SharedProps.Enabled = false;
			}
		}
		gridMrsBase1.Cols["sNO"].Visible = false;
		gridMrsBase1.Cols["PubCode"].Visible = false;
		if (IsTemplate)
		{
			SetTemplateControlAvailability();
		}
		RememberColsProps();
		BindToGrid("");
		checkDBReSet();
		((ComboBoxTool)ultraToolbarsManager1.Tools["Other_FilterType"]).SelectedIndex = 0;
		if (ArchConvert.Obj2Bool(CommonMethods.IniReadValue(AppDomain.CurrentDomain.BaseDirectory + "OptionSet.ini", "CommonData", "AllowIsTooltip")))
		{
			gridMrsBase1.ShowToolTipOnNarrowColumn = false;
			gridMrsBase2.ShowToolTipOnNarrowColumn = false;
		}
		else
		{
			gridMrsBase1.ShowToolTipOnNarrowColumn = true;
			gridMrsBase2.ShowToolTipOnNarrowColumn = true;
		}
		if (gridMrsBase1.Row > 0)
		{
			gridMrsBase1.Row = 1;
		}
		if (F_calledPccesCode != string.Empty)
		{
			gridMrsBase1.Select(gridMrsBase1.Rows.Count - 1, 1);
			gridMrsBase1.Select(gridMrsBase1.FindRow(F_calledPccesCode, 1, gridMrsBase1.Cols["PccesCode"].SafeIndex, caseSensitive: true, fullMatch: false, wrap: true), 1);
		}
		if (budgetType == 5)
		{
			ultraToolbarsManager1.Tools["mnuUseMrsCost"].SharedProps.Visible = false;
			ultraToolbarsManager1.Tools["mnuGetFromMrs"].SharedProps.Visible = false;
		}
		if (SysConfig.SysComsEnable)
		{
			if (SysConfig.SysChangeManagement)
			{
				if ((base.Owner as frmBudget)._BudgetChangeCurrentVersion > 0)
				{
					DisableExecuteBudgetFunction();
				}
			}
			else
			{
				Archnowledge.Pcces.DomainModule.Coms.BudgetCtrl theBudgetCtrl = new Archnowledge.Pcces.DomainModule.Coms.BudgetCtrl();
				if (theBudgetCtrl.IsProjectAlreadySubPlan(projectCode, SysConfig.SysComsDB))
				{
					DisableExecuteBudgetFunction();
				}
			}
		}
		LoadGreenItemSetting();
		if (SysConfig.SysChangeManagement)
		{
			btnAddBookList.Visible = true;
			gridMrsBase2.Cols["ItemNo"].Visible = true;
		}
		else
		{
			btnAddBookList.Visible = false;
			gridMrsBase2.Cols["ItemNo"].Visible = false;
		}
		if (FormActionName == PccesFormAction.BUD)
		{
			Check_mnuCorrectCName_CanBeEnabled();
		}
	}

	private void Check_mnuCorrectCName_CanBeEnabled()
	{
		int iNonCorrect = 0;
		if (gridMrsBase1.Rows.Count > 1)
		{
			for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
			{
				if (gridMrsBase1[i, "Correct"].ToString() == "" && gridMrsBase1[i, "costKind"].ToString() != "#" && gridMrsBase1[i, "costKind"].ToString().ToUpper() != "Z")
				{
					iNonCorrect++;
				}
			}
			if (iNonCorrect == 0)
			{
				ultraToolbarsManager1.Tools["mnuCorrectCName"].SharedProps.Enabled = true;
			}
			else
			{
				ultraToolbarsManager1.Tools["mnuCorrectCName"].SharedProps.Enabled = false;
			}
		}
		else
		{
			ultraToolbarsManager1.Tools["mnuCorrectCName"].SharedProps.Enabled = false;
		}
	}

	private void DisableExecuteBudgetFunction()
	{
		try
		{
			string[] menuItems = new string[10] { "mnuGetFromMrs", "mnuUseMrsCost", "mnuImpExcel", "mnuImpExcelChange", "mnuImpXML", "mnuExpExcelChange", "mnuDBReSet", "mnuChangeCode", "mnuFillRate", "mnuCodeUpgrade" };
			string[] array = menuItems;
			foreach (string menuItem in array)
			{
				ultraToolbarsManager1.Tools[menuItem].SharedProps.Enabled = false;
			}
			gridMrsBase1.Cols["fixPrice"].AllowEditing = false;
			gridMrsBase2.Cols["fixPrice"].AllowEditing = false;
			gridMrsBase1.Cols["IsGreenItem"].AllowEditing = false;
			gridMrsBase1.Cols["IsGreenMethod"].AllowEditing = false;
			gridMrsBase1.Cols["IsGreenMaterial"].AllowEditing = false;
			gridMrsBase1.Cols["IsGreenEnergy"].AllowEditing = false;
			gridMrsBase1.Cols["ItemType"].AllowEditing = false;
			gridMrsBase1.Cols["QtyDec"].AllowEditing = false;
			gridMrsBase1.Cols["CostDec"].AllowEditing = false;
			gridMrsBase1.Cols["AmtDec"].AllowEditing = false;
			gridMrsBase1.Cols["Prec"].AllowEditing = false;
			gridMrsBase1.Cols["sNO"].AllowEditing = false;
			gridMrsBase1.Cols["PubCode"].AllowEditing = false;
			gridMrsBase1.Cols["Cost"].AllowEditing = false;
			gridMrsBase1.Cols["LRate"].AllowEditing = false;
			gridMrsBase1.Cols["ERate"].AllowEditing = false;
			gridMrsBase1.Cols["MRate"].AllowEditing = false;
			gridMrsBase1.Cols["WRate"].AllowEditing = false;
			gridMrsBase1.Cols["Memo"].AllowEditing = false;
			gridMrsBase1.Cols["Prec"].AllowEditing = false;
			gridMrsBase1.Cols["sNO"].AllowEditing = false;
			gridMrsBase1.Cols["PubCode"].AllowEditing = false;
		}
		catch (Exception ex)
		{
			MessageBox.Show("SetPopupMenuDisable Error:" + ex.Message);
		}
	}

	private void SetTemplateControlAvailability()
	{
		try
		{
			string[] menuItems = new string[6] { "mnuEditItem", "mnuGetFromMrs", "MenuImport", "mnuChangeCode", "mnuUseMrsCost", "mnuFillRate" };
			string[] array = menuItems;
			foreach (string menuItem in array)
			{
				ultraToolbarsManager1.Tools[menuItem].SharedProps.Visible = false;
			}
			for (int j = 0; j < gridMrsBase1.Cols.Count; j++)
			{
				gridMrsBase1.Cols[j].AllowEditing = false;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("SetTemplateControlAvailability Error:" + ex.Message);
		}
	}

	private void checkDBReSet()
	{
		try
		{
			DBClass DBCLS = new DBClass();
			DBCLS._FS_UserID = F_UserID;
			string sCheckSQL1 = "select Count(*) as ICount From budProjMrsA A Left Join MrsBaseA B  on A.PccesCode = B.PccesCode  Where A.ProjectCode='" + projectCode + "' And A.PubCode <> B.PubCode";
			string sCount1 = DBCLS.GetUserDefine_String(sCheckSQL1, "ICount");
			string sCheckSQL2 = "select Count(*) as ICount From budProjMrsA A Left Join MrsBaseA B  on A.PubCode = B.PubCode  Where A.ProjectCode='" + projectCode + "' And A.PccesCode <> B.PccesCode ";
			string sCount2 = DBCLS.GetUserDefine_String(sCheckSQL2, "ICount");
			if (PubTools.Str2Int(sCount1) + PubTools.Str2Int(sCount2) > 0)
			{
				ultraToolbarsManager1.Tools["mnuGetFromMrs"].SharedProps.Enabled = false;
				ultraToolbarsManager1.Tools["mnuDBReSet"].SharedProps.Visible = true;
			}
			else
			{
				ultraToolbarsManager1.Tools["mnuGetFromMrs"].SharedProps.Enabled = true;
				ultraToolbarsManager1.Tools["mnuDBReSet"].SharedProps.Visible = false;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("checkDBReSet Error:" + ex.Message);
		}
	}

	private void LoadGreenItemSetting()
	{
		try
		{
			string AppLocation = AppDomain.CurrentDomain.BaseDirectory;
			string greenEnv = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "CommonData", "GreenEnv");
			string greenMethod = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "CommonData", "GreenMethod");
			string greenMaterial = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "CommonData", "GreenMaterial");
			string greenEnergy = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "CommonData", "GreenEnergy");
			gridMrsBase1.Cols["IsGreenItem"].Caption = ((greenEnv == string.Empty) ? "綠色環境" : greenEnv);
			gridMrsBase1.Cols["IsGreenMethod"].Caption = ((greenMethod == string.Empty) ? "綠色工法" : greenMethod);
			gridMrsBase1.Cols["IsGreenMaterial"].Caption = ((greenMaterial == string.Empty) ? "綠色材料" : greenMaterial);
			gridMrsBase1.Cols["IsGreenEnergy"].Caption = ((greenEnergy == string.Empty) ? "綠色能源" : greenEnergy);
		}
		catch (Exception ex)
		{
			MessageBox.Show("LoadGreenItemSetting Error:" + ex.Message);
		}
	}

	private void ultraToolbarsManager1_ToolClick(object sender, ToolClickEventArgs e)
	{
		DoMenuAction(e.Tool.Key);
	}

	private void DoMenuAction(string MenuID)
	{
		switch (MenuID)
		{
		case "PopupView":
			break;
		case "PopupUse":
			break;
		case "PopContext":
			break;
		case "mnuEditItem":
			Execute_WorkItemEdit();
			break;
		case "mnuIncorrect":
		{
			BindToGrid("");
			gridMrsBase1.Redraw = false;
			int iCountC5 = 0;
			for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
			{
				if (gridMrsBase1[i, "Correct"].ToString() == "是")
				{
					iCountC5++;
					gridMrsBase1.Rows[i].Visible = false;
				}
				else
				{
					gridMrsBase1.Rows[i].Visible = true;
				}
			}
			gridMrsBase1.Redraw = true;
			StatusBar.Panels[1].Text = "錯誤項：" + (gridMrsBase1.Rows.Count - 1 - iCountC5) + "筆";
			break;
		}
		case "mnuCorrectItems":
		{
			BindToGrid("");
			gridMrsBase1.Redraw = false;
			int iCountC3 = 0;
			for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
			{
				if (gridMrsBase1[i, "Correct"].ToString() == "是")
				{
					iCountC3++;
					gridMrsBase1.Rows[i].Visible = true;
				}
				else
				{
					gridMrsBase1.Rows[i].Visible = false;
				}
			}
			gridMrsBase1.Redraw = true;
			StatusBar.Panels[1].Text = "正確項：" + iCountC3 + "筆";
			break;
		}
		case "mnuNotfit":
		{
			BindToGrid("");
			gridMrsBase1.Redraw = false;
			int iCountC4 = 0;
			for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
			{
				if (gridMrsBase1[i, "Confirm"].ToString() == "是")
				{
					iCountC4++;
					gridMrsBase1.Rows[i].Visible = false;
				}
				else
				{
					gridMrsBase1.Rows[i].Visible = true;
				}
			}
			gridMrsBase1.Redraw = true;
			StatusBar.Panels[1].Text = "不符合項：" + (gridMrsBase1.Rows.Count - 1 - iCountC4) + "筆";
			break;
		}
		case "mnuCalculateCorrectness":
			((StateButtonTool)ultraToolbarsManager1.Tools["mnuViewAllItem"]).Checked = true;
			BindToGrid("");
			Application.DoEvents();
			updateCorrectConfirm();
			if (FormActionName == PccesFormAction.BUD)
			{
				Check_mnuCorrectCName_CanBeEnabled();
			}
			CheckMainLItem_IsReach_ResourceItemTenPercent();
			break;
		case "mnuViewAllItem":
			BindToGrid("");
			break;
		case "mnuViewAnalysis":
			BindToGrid("");
			break;
		case "mnuViewNoAnalysis":
			BindToGrid("");
			break;
		case "PopupViewTyppe":
			break;
		case "mnuItem":
			BindToGrid("");
			break;
		case "mnuLabor":
			BindToGrid("");
			break;
		case "mnuEquip":
			BindToGrid("");
			break;
		case "mnuMaterial":
			BindToGrid("");
			break;
		case "mnuWaste":
			BindToGrid("");
			break;
		case "mnuAnalysis":
			break;
		case "mnuParent":
			Find_Parent();
			break;
		case "mnuParentFromBudget":
			FindMrsInItemA();
			break;
		case "mnuSend":
			break;
		case "mnuCostIsZero":
			BindToGrid("");
			break;
		case "mnuLockCost":
			break;
		case "mnuUnLockCost":
			break;
		case "lblFind":
			break;
		case "mnu_Cbo1":
			break;
		case "mnuGo":
			Do_ToolBarFind();
			break;
		case "mnuExport":
			Do_Export();
			break;
		case "mnuSendBack":
			SendBackMrsBase();
			break;
		case "mnuSendBack_NameUnit":
			SendBackMrsBase_NameUnit();
			break;
		case "mnuGetFromMrs":
			GetFromMrsBase();
			break;
		case "mnuUseMrsCost":
			UseMrsCost();
			break;
		case "mnuMnyRate":
			FilterRate();
			break;
		case "mnuImpExcel":
			DoImport(ImportType.Excel);
			break;
		case "mnuImpExcelChange":
			DoImport();
			break;
		case "mnuImpXML":
			DoImport(ImportType.XML);
			break;
		case "mnuExpExcel":
			DoExport(ExportType.Excel);
			break;
		case "mnuExpExcelChange":
			DoExport(ExportType.Excel);
			break;
		case "mnuExpXML":
			DoExport(ExportType.XML);
			break;
		case "mnuCalcErr":
			BindToGrid("CalcError = '1'");
			break;
		case "mnuAnaMinus":
		{
			string sFun = CommonMethods.GetActionNameString(FormActionName);
			if (sFun.ToUpper() != "SUBCHG")
			{
				BindToGrid(" pubCode in (Select parentCode From " + sFun + "ProjMrsB Where ProjectCode ='" + projectCode + "' and (amount < 0 or cost < 0)) ");
			}
			else
			{
				BindToGrid(" pubCode in (Select parentCode From " + sFun + "ProjMrsB Where ProjectCode ='" + projectCode + "' and (amount < 0 or cost < 0) and chgCount = '" + F_chgCount + "') ");
			}
			break;
		}
		case "mnuDBReSet":
			DoDBReset();
			break;
		case "mnuExcelExp":
			DoExcelExp();
			break;
		case "mnuFillRate":
			ExecuteFillRate();
			break;
		case "mnuChangeCode":
			ExecuteChangeCode();
			break;
		case "mnuCodeUpgrade":
			Do_CodeUpgrade();
			BindToGrid("");
			break;
		case "Other_FilterExecute":
			Do_Filter();
			break;
		case "mnuAutoNum":
			ExecuteAutoNumForm();
			break;
		case "mnuViewItemSurName":
			Do_Filter();
			break;
		case "mnuViewItemUnSurName":
			if (F_iCount == "")
			{
				Do_Filter();
			}
			else
			{
				F_iCount = "";
			}
			break;
		case "mnuItemDup":
		{
			BindToGrid("");
			BudProjMrsA budProjMrsA = new BudProjMrsA();
			DataTable DT_Dups = budProjMrsA.GetDuplicateItems(projectCode);
			if (DT_Dups.Rows.Count > 0)
			{
				gridMrsBase1.Redraw = false;
				for (int k = 1; k < gridMrsBase1.Rows.Count; k++)
				{
					gridMrsBase1.Rows[k].Visible = false;
				}
				for (int j = 0; j < DT_Dups.Rows.Count; j++)
				{
					for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
					{
						if (gridMrsBase1[i, "pccesCode"].ToString().Trim().IndexOf(DT_Dups.Rows[j]["pccesCode"].ToString().Trim()) > -1 && gridMrsBase1[i, "cName"].ToString().Trim() == DT_Dups.Rows[j]["cName"].ToString().Trim() && gridMrsBase1[i, "unitName"].ToString().Trim() == DT_Dups.Rows[j]["unitName"].ToString().Trim())
						{
							gridMrsBase1.Rows[i].Visible = true;
						}
					}
				}
				gridMrsBase1.Sort(SortFlags.Ascending, gridMrsBase1.Cols["cName"].SafeIndex, gridMrsBase1.Cols["unitName"].SafeIndex);
				gridMrsBase1.Redraw = true;
			}
			else
			{
				MessageBox.Show(this, "沒有名稱重複的項目!!", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			break;
		}
		case "mnuExpNotCorrect":
		{
			Cursor = Cursors.WaitCursor;
			((StateButtonTool)ultraToolbarsManager1.Tools["mnuIncorrect"]).Checked = true;
			BindToGrid("");
			gridMrsBase1.Redraw = false;
			int iCountC001 = 0;
			for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
			{
				if (gridMrsBase1[i, "Correct"].ToString() == "是")
				{
					iCountC001++;
					gridMrsBase1.Rows[i].Visible = false;
				}
				else
				{
					gridMrsBase1.Rows[i].Visible = true;
				}
			}
			gridMrsBase1.Redraw = true;
			StatusBar.Panels[1].Text = "錯誤項：" + (gridMrsBase1.Rows.Count - 1 - iCountC001) + "筆";
			Application.DoEvents();
			Aspose.Cells.License license = new Aspose.Cells.License();
			license.SetLicense("Aspose.Custom.lic");
			Excel excel = new Excel();
			excel.Worksheets.Add();
			Worksheet sheetMrsBaseA = excel.Worksheets[0];
			sheetMrsBaseA.Name = "錯誤工項";
			string FontFace = "新細明體";
			int styleIndex = excel.Styles.Add();
			Style styleHeader = excel.Styles[styleIndex];
			styleHeader.Font.IsBold = true;
			styleHeader.Font.Color = Color.FromArgb(255, 0, 0);
			styleHeader.ForegroundColor = Color.FromArgb(0, 204, 255);
			styleHeader.Pattern = BackgroundType.Solid;
			styleHeader.Font.Size = 12;
			styleHeader.Font.Name = FontFace;
			SetAllBorders(styleHeader, CellBorderType.Thin);
			styleIndex = excel.Styles.Add();
			Style styleFirstColumn = excel.Styles[styleIndex];
			styleFirstColumn.Font.Size = 12;
			styleFirstColumn.Font.Name = FontFace;
			styleFirstColumn.ForegroundColor = Color.FromArgb(255, 204, 153);
			styleFirstColumn.Pattern = BackgroundType.Solid;
			styleFirstColumn.Number = 49;
			SetAllBorders(styleFirstColumn, CellBorderType.Thin);
			styleIndex = excel.Styles.Add();
			Style styleThirdColumn = excel.Styles[styleIndex];
			styleThirdColumn.Font.Size = 12;
			styleThirdColumn.Font.Name = FontFace;
			styleThirdColumn.ForegroundColor = Color.FromArgb(255, 255, 153);
			styleThirdColumn.Pattern = BackgroundType.Solid;
			SetAllBorders(styleThirdColumn, CellBorderType.Thin);
			styleIndex = excel.Styles.Add();
			Style styleOther = excel.Styles[styleIndex];
			styleOther.Font.Size = 12;
			styleOther.Font.Name = FontFace;
			styleOther.HorizontalAlignment = TextAlignmentType.Right;
			SetAllBorders(styleOther, CellBorderType.Thin);
			styleIndex = excel.Styles.Add();
			Style styleText = excel.Styles[styleIndex];
			styleText.Font.Size = 12;
			styleText.Font.Name = FontFace;
			styleText.Number = 49;
			SetAllBorders(styleText, CellBorderType.Thin);
			sheetMrsBaseA.Cells[0, 0].PutValue("工項代碼");
			sheetMrsBaseA.Cells[0, 1].PutValue("工項名稱");
			sheetMrsBaseA.Cells[0, 2].PutValue("分析");
			sheetMrsBaseA.Cells[0, 3].PutValue("單位");
			sheetMrsBaseA.Cells[0, 4].PutValue("數量");
			sheetMrsBaseA.Cells[0, 5].PutValue("單價");
			sheetMrsBaseA.Cells[0, 6].PutValue("複價");
			sheetMrsBaseA.Cells[0, 7].PutValue("編碼正確");
			sheetMrsBaseA.Cells[0, 8].PutValue("綱要編碼正確");
			sheetMrsBaseA.Cells[0, 9].PutValue("錯誤態樣");
			sheetMrsBaseA.Cells[0, 10].PutValue("正確工項名稱");
			sheetMrsBaseA.Cells[0, 11].PutValue("正確單位");
			sheetMrsBaseA.Cells[0, 0].Style = styleHeader;
			sheetMrsBaseA.Cells[0, 1].Style = styleHeader;
			sheetMrsBaseA.Cells[0, 2].Style = styleHeader;
			sheetMrsBaseA.Cells[0, 3].Style = styleHeader;
			sheetMrsBaseA.Cells[0, 4].Style = styleHeader;
			sheetMrsBaseA.Cells[0, 5].Style = styleHeader;
			sheetMrsBaseA.Cells[0, 6].Style = styleHeader;
			sheetMrsBaseA.Cells[0, 7].Style = styleHeader;
			sheetMrsBaseA.Cells[0, 8].Style = styleHeader;
			sheetMrsBaseA.Cells[0, 9].Style = styleHeader;
			sheetMrsBaseA.Cells[0, 10].Style = styleHeader;
			sheetMrsBaseA.Cells[0, 11].Style = styleHeader;
			for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
			{
				sheetMrsBaseA.Cells[i, 0].PutValue(gridMrsBase1[i, "pccesCode"].ToString());
				sheetMrsBaseA.Cells[i, 1].PutValue(gridMrsBase1[i, "cName"].ToString());
				if ((bool)gridMrsBase1[i, "Analysis"])
				{
					sheetMrsBaseA.Cells[i, 2].PutValue("V");
				}
				else
				{
					sheetMrsBaseA.Cells[i, 2].PutValue("");
				}
				sheetMrsBaseA.Cells[i, 3].PutValue(gridMrsBase1[i, "unitName"].ToString());
				if (gridMrsBase1[i, "usrQty"] != null)
				{
					sheetMrsBaseA.Cells[i, 4].PutValue(string.Format("{0:N3}", decimal.Parse(gridMrsBase1[i, "usrQty"].ToString())));
				}
				if (gridMrsBase1[i, "cost"] != null)
				{
					sheetMrsBaseA.Cells[i, 5].PutValue(string.Format("{0:N2}", decimal.Parse(gridMrsBase1[i, "cost"].ToString())));
				}
				if (gridMrsBase1[i, "usrAmt"] != null)
				{
					sheetMrsBaseA.Cells[i, 6].PutValue(string.Format("{0:N2}", decimal.Parse(gridMrsBase1[i, "usrAmt"].ToString())));
				}
				sheetMrsBaseA.Cells[i, 7].PutValue(gridMrsBase1[i, "Correct"].ToString());
				sheetMrsBaseA.Cells[i, 8].PutValue(gridMrsBase1[i, "Confirm"].ToString());
				sheetMrsBaseA.Cells[i, 9].PutValue(gridMrsBase1[i, "CompareErrState"].ToString());
				sheetMrsBaseA.Cells[i, 10].PutValue(gridMrsBase1[i, "CorrectCName"].ToString());
				sheetMrsBaseA.Cells[i, 11].PutValue(gridMrsBase1[i, "CorrectUnitName"].ToString());
				sheetMrsBaseA.Cells[i, 0].Style = styleFirstColumn;
				sheetMrsBaseA.Cells[i, 1].Style = styleText;
				sheetMrsBaseA.Cells[i, 2].Style = styleText;
				sheetMrsBaseA.Cells[i, 3].Style = styleText;
				sheetMrsBaseA.Cells[i, 4].Style = styleOther;
				sheetMrsBaseA.Cells[i, 5].Style = styleOther;
				sheetMrsBaseA.Cells[i, 6].Style = styleOther;
				sheetMrsBaseA.Cells[i, 7].Style = styleText;
				sheetMrsBaseA.Cells[i, 8].Style = styleText;
				sheetMrsBaseA.Cells[i, 9].Style = styleText;
				sheetMrsBaseA.Cells[i, 10].Style = styleText;
				sheetMrsBaseA.Cells[i, 11].Style = styleText;
			}
			sheetMrsBaseA.AutoFitColumn(0);
			sheetMrsBaseA.AutoFitColumn(1);
			sheetMrsBaseA.AutoFitColumn(2);
			sheetMrsBaseA.AutoFitColumn(3);
			sheetMrsBaseA.AutoFitColumn(4);
			sheetMrsBaseA.AutoFitColumn(5);
			sheetMrsBaseA.AutoFitColumn(6);
			sheetMrsBaseA.AutoFitColumn(7);
			sheetMrsBaseA.AutoFitColumn(8);
			sheetMrsBaseA.AutoFitColumn(9);
			sheetMrsBaseA.AutoFitColumn(10);
			sheetMrsBaseA.AutoFitColumn(11);
			string commonAppData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
			string sFile = Path.Combine(commonAppData, "Incorrect_" + DateTime.Now.ToString("yyyyMMddHHmmss")) + ".xls";
			excel.Save(sFile);
			Process.Start(sFile);
			Cursor = Cursors.Default;
			break;
		}
		case "mnuExpAllCorrect":
		{
			Cursor = Cursors.WaitCursor;
			((StateButtonTool)ultraToolbarsManager1.Tools["mnuCorrectItems"]).Checked = true;
			BindToGrid("");
			gridMrsBase1.Redraw = false;
			int iCountC001 = 0;
			for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
			{
				if (gridMrsBase1[i, "Correct"].ToString() == "是")
				{
					gridMrsBase1.Rows[i].Visible = true;
					continue;
				}
				iCountC001++;
				gridMrsBase1.Rows[i].Visible = false;
			}
			gridMrsBase1.Redraw = true;
			StatusBar.Panels[1].Text = "正確項：" + (gridMrsBase1.Rows.Count - 1 - iCountC001) + "筆";
			Application.DoEvents();
			Aspose.Cells.License license = new Aspose.Cells.License();
			license.SetLicense("Aspose.Custom.lic");
			Excel excel = new Excel();
			excel.Worksheets.Add();
			Worksheet sheetMrsBaseA = excel.Worksheets[0];
			sheetMrsBaseA.Name = "正確工項";
			string FontFace = "新細明體";
			int styleIndex = excel.Styles.Add();
			Style styleHeader = excel.Styles[styleIndex];
			styleHeader.Font.IsBold = true;
			styleHeader.Font.Color = Color.FromArgb(255, 0, 0);
			styleHeader.ForegroundColor = Color.FromArgb(0, 204, 255);
			styleHeader.Pattern = BackgroundType.Solid;
			styleHeader.Font.Size = 12;
			styleHeader.Font.Name = FontFace;
			SetAllBorders(styleHeader, CellBorderType.Thin);
			styleIndex = excel.Styles.Add();
			Style styleFirstColumn = excel.Styles[styleIndex];
			styleFirstColumn.Font.Size = 12;
			styleFirstColumn.Font.Name = FontFace;
			styleFirstColumn.ForegroundColor = Color.FromArgb(255, 204, 153);
			styleFirstColumn.Pattern = BackgroundType.Solid;
			styleFirstColumn.Number = 49;
			SetAllBorders(styleFirstColumn, CellBorderType.Thin);
			styleIndex = excel.Styles.Add();
			Style styleThirdColumn = excel.Styles[styleIndex];
			styleThirdColumn.Font.Size = 12;
			styleThirdColumn.Font.Name = FontFace;
			styleThirdColumn.ForegroundColor = Color.FromArgb(255, 255, 153);
			styleThirdColumn.Pattern = BackgroundType.Solid;
			SetAllBorders(styleThirdColumn, CellBorderType.Thin);
			styleIndex = excel.Styles.Add();
			Style styleOther = excel.Styles[styleIndex];
			styleOther.Font.Size = 12;
			styleOther.Font.Name = FontFace;
			styleOther.HorizontalAlignment = TextAlignmentType.Right;
			SetAllBorders(styleOther, CellBorderType.Thin);
			styleIndex = excel.Styles.Add();
			Style styleText = excel.Styles[styleIndex];
			styleText.Font.Size = 12;
			styleText.Font.Name = FontFace;
			styleText.Number = 49;
			SetAllBorders(styleText, CellBorderType.Thin);
			sheetMrsBaseA.Cells[0, 0].PutValue("工項代碼");
			sheetMrsBaseA.Cells[0, 1].PutValue("工項名稱");
			sheetMrsBaseA.Cells[0, 2].PutValue("分析");
			sheetMrsBaseA.Cells[0, 3].PutValue("單位");
			sheetMrsBaseA.Cells[0, 4].PutValue("數量");
			sheetMrsBaseA.Cells[0, 5].PutValue("單價");
			sheetMrsBaseA.Cells[0, 6].PutValue("複價");
			sheetMrsBaseA.Cells[0, 7].PutValue("編碼正確");
			sheetMrsBaseA.Cells[0, 8].PutValue("綱要編碼正確");
			sheetMrsBaseA.Cells[0, 9].PutValue("錯誤態樣");
			sheetMrsBaseA.Cells[0, 10].PutValue("正確工項名稱");
			sheetMrsBaseA.Cells[0, 11].PutValue("正確單位");
			sheetMrsBaseA.Cells[0, 0].Style = styleHeader;
			sheetMrsBaseA.Cells[0, 1].Style = styleHeader;
			sheetMrsBaseA.Cells[0, 2].Style = styleHeader;
			sheetMrsBaseA.Cells[0, 3].Style = styleHeader;
			sheetMrsBaseA.Cells[0, 4].Style = styleHeader;
			sheetMrsBaseA.Cells[0, 5].Style = styleHeader;
			sheetMrsBaseA.Cells[0, 6].Style = styleHeader;
			sheetMrsBaseA.Cells[0, 7].Style = styleHeader;
			sheetMrsBaseA.Cells[0, 8].Style = styleHeader;
			sheetMrsBaseA.Cells[0, 9].Style = styleHeader;
			sheetMrsBaseA.Cells[0, 10].Style = styleHeader;
			sheetMrsBaseA.Cells[0, 11].Style = styleHeader;
			int itheRow = 0;
			for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
			{
				if (gridMrsBase1.Rows[i].Visible)
				{
					itheRow++;
					sheetMrsBaseA.Cells[itheRow, 0].PutValue(gridMrsBase1[i, "pccesCode"].ToString());
					sheetMrsBaseA.Cells[itheRow, 1].PutValue(gridMrsBase1[i, "cName"].ToString());
					if ((bool)gridMrsBase1[itheRow, "Analysis"])
					{
						sheetMrsBaseA.Cells[itheRow, 2].PutValue("V");
					}
					else
					{
						sheetMrsBaseA.Cells[itheRow, 2].PutValue("");
					}
					sheetMrsBaseA.Cells[itheRow, 3].PutValue(gridMrsBase1[i, "unitName"].ToString());
					if (gridMrsBase1[i, "usrQty"] != null)
					{
						sheetMrsBaseA.Cells[itheRow, 4].PutValue(string.Format("{0:N3}", decimal.Parse(gridMrsBase1[i, "usrQty"].ToString())));
					}
					if (gridMrsBase1[i, "cost"] != null)
					{
						sheetMrsBaseA.Cells[itheRow, 5].PutValue(string.Format("{0:N2}", decimal.Parse(gridMrsBase1[i, "cost"].ToString())));
					}
					if (gridMrsBase1[i, "usrAmt"] != null)
					{
						sheetMrsBaseA.Cells[itheRow, 6].PutValue(string.Format("{0:N2}", decimal.Parse(gridMrsBase1[i, "usrAmt"].ToString())));
					}
					sheetMrsBaseA.Cells[itheRow, 7].PutValue(gridMrsBase1[i, "Correct"].ToString());
					sheetMrsBaseA.Cells[itheRow, 8].PutValue(gridMrsBase1[i, "Confirm"].ToString());
					sheetMrsBaseA.Cells[itheRow, 9].PutValue(gridMrsBase1[i, "CompareErrState"].ToString());
					sheetMrsBaseA.Cells[itheRow, 10].PutValue(gridMrsBase1[i, "CorrectCName"].ToString());
					sheetMrsBaseA.Cells[itheRow, 11].PutValue(gridMrsBase1[i, "CorrectUnitName"].ToString());
					sheetMrsBaseA.Cells[itheRow, 0].Style = styleFirstColumn;
					sheetMrsBaseA.Cells[itheRow, 1].Style = styleText;
					sheetMrsBaseA.Cells[itheRow, 2].Style = styleText;
					sheetMrsBaseA.Cells[itheRow, 3].Style = styleText;
					sheetMrsBaseA.Cells[itheRow, 4].Style = styleOther;
					sheetMrsBaseA.Cells[itheRow, 5].Style = styleOther;
					sheetMrsBaseA.Cells[itheRow, 6].Style = styleOther;
					sheetMrsBaseA.Cells[itheRow, 7].Style = styleText;
					sheetMrsBaseA.Cells[itheRow, 8].Style = styleText;
					sheetMrsBaseA.Cells[itheRow, 9].Style = styleText;
					sheetMrsBaseA.Cells[itheRow, 10].Style = styleText;
					sheetMrsBaseA.Cells[itheRow, 11].Style = styleText;
				}
			}
			sheetMrsBaseA.AutoFitColumn(0);
			sheetMrsBaseA.AutoFitColumn(1);
			sheetMrsBaseA.AutoFitColumn(2);
			sheetMrsBaseA.AutoFitColumn(3);
			sheetMrsBaseA.AutoFitColumn(4);
			sheetMrsBaseA.AutoFitColumn(5);
			sheetMrsBaseA.AutoFitColumn(6);
			sheetMrsBaseA.AutoFitColumn(7);
			sheetMrsBaseA.AutoFitColumn(8);
			sheetMrsBaseA.AutoFitColumn(9);
			sheetMrsBaseA.AutoFitColumn(10);
			sheetMrsBaseA.AutoFitColumn(11);
			string sFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Correct_" + DateTime.Now.ToString("yyyyMMddHHmmss")) + ".xls";
			excel.Save(sFile);
			Process.Start(sFile);
			Cursor = Cursors.Default;
			break;
		}
		case "mnuCorrectCName":
		{
			((StateButtonTool)ultraToolbarsManager1.Tools["mnuViewAllItem"]).Checked = true;
			BindToGrid("");
			Application.DoEvents();
			int iCorectabledCount = 0;
			for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
			{
				if (gridMrsBase1[i, "CorrectCName"].ToString() != "" || gridMrsBase1[i, "CorrectUnitName"].ToString() != "")
				{
					iCorectabledCount++;
				}
			}
			FormBudgetRes_CNameCorrect FM2 = new FormBudgetRes_CNameCorrect();
			FM2.Owner = this;
			FM2._FormActionName = FormActionName;
			FM2._ProjectCode = projectCode;
			FM2._UserID = F_UserID;
			FM2._TotalItemCount = gridMrsBase1.Rows.Count - 1;
			FM2._CorrectableItemCount = iCorectabledCount;
			if (FM2.ShowDialog() == DialogResult.OK)
			{
				F_IsBudgetFormNeedToReload = true;
				FormBudgetRes_Load(this, new EventArgs());
				updateCorrectConfirm();
				if (FormActionName == PccesFormAction.BUD)
				{
					Check_mnuCorrectCName_CanBeEnabled();
				}
			}
			break;
		}
		case "mnuSendBack_Cost":
		{
			string sMessage = "此功能只回傳固定單價項目，\n單價分析項及變動單價項都會被忽略\n\n確定要回傳單價?";
			if (MessageBox.Show(this, sMessage, "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
			{
				break;
			}
			FormProgress FM = new FormProgress();
			FM._Max = gridMrsBase1.Rows.Count;
			FM._Min = 0;
			FM.Message = "正在計算...";
			FM.TopMost = true;
			FM.Show();
			DBClass DBCls = new DBClass();
			DBCls._FS_UserID = F_UserID;
			string sSQL = "";
			int iSendBackCostCount = 0;
			for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
			{
				FM.SetProgressValue(i);
				Application.DoEvents();
				Cursor = Cursors.WaitCursor;
				if (gridMrsBase1.Rows[i].Selected && !(bool)gridMrsBase1[i, "analysis"] && !(gridMrsBase1[i, "costKind"].ToString().Trim() != ""))
				{
					sSQL = "Update mrsBaseA  Set cost = " + gridMrsBase1[i, "cost"].ToString() + " Where pccesCode='" + gridMrsBase1[i, "pccesCode"].ToString().Trim() + "' ";
					DBCls.ExecuteCommand(sSQL);
					iSendBackCostCount++;
				}
			}
			FM.Hide();
			FM.Dispose();
			FM = null;
			Cursor = Cursors.Default;
			MessageBox.Show(this, "完成回傳，共計 " + iSendBackCostCount + " 項", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			break;
		}
		}
	}

	private void CheckMainLItem_IsReach_ResourceItemTenPercent()
	{
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = F_UserID;
		Application.DoEvents();
		if (FormActionName == PccesFormAction.BUD)
		{
			DataTable DT_Details = DBCLS.GetUserDefine("Select itemNo, CName, Kind from budItemA Where Kind='L' and ProjectCode='" + projectCode + "' ");
			Application.DoEvents();
			DataTable DT_Resource = DBCLS.GetUserDefine("Select pccesCode, CName from budProjMrsA Where ProjectCode='" + projectCode + "' ");
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

	private void SetAllBorders(Style style, CellBorderType borderType)
	{
		style.Borders[BorderType.TopBorder].LineStyle = borderType;
		style.Borders[BorderType.RightBorder].LineStyle = borderType;
		style.Borders[BorderType.BottomBorder].LineStyle = borderType;
		style.Borders[BorderType.LeftBorder].LineStyle = borderType;
	}

	private void Do_Filter()
	{
		try
		{
			string sSearchText = ((TextBoxTool)ultraToolbarsManager1.Tools["Other_QueryText"]).Text.Trim();
			if (CommonMethods.CheckValidString(sSearchText))
			{
				string sWhere = "";
				switch (((ComboBoxTool)ultraToolbarsManager1.Tools["Other_FilterType"]).Value.ToString())
				{
				case "0":
					sWhere = "  pccesCode Like '" + sSearchText + "%' ";
					break;
				case "1":
					sWhere = "  cName Like '%" + sSearchText + "%' ";
					break;
				case "2":
					sWhere = "  extendCode Like '%" + sSearchText + "%' ";
					break;
				}
				ExtraSearchCriteria = sWhere;
				RunExtraSearchCriteria = true;
				BindToGrid(string.Empty);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Do_Filter Error:" + ex.Message);
		}
	}

	private void Do_CodeUpgrade()
	{
		try
		{
			string sQuestion = "是否執行編碼更新(替換/昇級)?\n\n新的編碼規則，已從原本10碼擴充至12碼。\n\n例如：\n執行更新前--> (0321011212)\u3000鋼筋，SR240，竹節鋼筋，D10mm，工廠交貨(Kg)\n執行更新後--> (032101121002)鋼筋，SR240，竹節鋼筋，D10mm，工廠交貨(Kg)\n\n若您確定執行，系統會將基本資料庫裡所有的工項編碼依上述範例之規則進行更新。\n執行後將無法還原，請慎重確認後再執行。";
			if (MessageBox.Show(this, sQuestion, "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
			{
				return;
			}
			ArrayList aArr = new ArrayList();
			aArr.Add(F_UserID);
			aArr.Add("執行編碼替換。");
			ModifyDB stdCom = new ModifyDB("", aArr);
			string sConn = stdCom.ls_connstr;
			OleDbConnection odConn1 = new OleDbConnection(sConn);
			odConn1.Open();
			string sFun = CommonMethods.GetActionNameString(FormActionName);
			string StrAdp = "SELECT projectCode, pubCode, pccesCode FROM " + sFun + "ProjMrsA Where projectCode='" + projectCode + "' ";
			OleDbDataAdapter OldAdp = new OleDbDataAdapter();
			OldAdp.SelectCommand = new OleDbCommand(StrAdp, odConn1);
			OleDbTransaction OldTran = odConn1.BeginTransaction();
			OldAdp.SelectCommand.Transaction = OldTran;
			OleDbCommandBuilder OldBuild = new OleDbCommandBuilder(OldAdp);
			DataTable D_T = new DataTable();
			OldAdp.Fill(D_T);
			string sPrefix = "";
			string sPccesCode = "";
			string sPart1 = "";
			string sPart2 = "";
			FormSys_G_Info1 FM_INFO = new FormSys_G_Info1();
			FM_INFO.TopMost = true;
			FM_INFO._InfoString = "工項編碼替換中，請稍候! ";
			FM_INFO._MaxValue = D_T.Rows.Count;
			FM_INFO._MinValue = 0;
			FM_INFO._ProgressValue = 0;
			FM_INFO.Owner = this;
			FM_INFO.Show();
			FM_INFO.BringToFront();
			Application.DoEvents();
			Cursor = Cursors.WaitCursor;
			try
			{
				for (int i = 0; i < D_T.Rows.Count; i++)
				{
					FM_INFO._ProgressValue++;
					Application.DoEvents();
					Cursor = Cursors.WaitCursor;
					sPrefix = D_T.Rows[i]["pccesCode"].ToString().Trim().Substring(0, 1);
					sPccesCode = D_T.Rows[i]["pccesCode"].ToString().Trim();
					if (sPrefix.ToUpper() == "L" || sPrefix.ToUpper() == "E" || sPrefix.ToUpper() == "M" || sPrefix.ToUpper() == "W")
					{
						if (sPccesCode.Length == 11)
						{
							sPart1 = sPccesCode.Substring(0, 10);
							sPart2 = sPccesCode.Substring(10, 1);
							D_T.Rows[i]["pccesCode"] = sPart1 + "00" + sPart2;
						}
					}
					else if (sPccesCode.Length == 10)
					{
						sPart1 = sPccesCode.Substring(0, 9);
						sPart2 = sPccesCode.Substring(9, 1);
						D_T.Rows[i]["pccesCode"] = sPart1 + "00" + sPart2;
					}
				}
				OldAdp.Update(D_T);
				OldTran.Commit();
				odConn1.Close();
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudgetRes.cs" + ex.Message);
				OldTran.Rollback();
				Console.Write(ex.Message);
			}
			FM_INFO.Hide();
			FM_INFO.Close();
			FM_INFO.Dispose();
			FM_INFO = null;
			Cursor = Cursors.Default;
			(base.Owner as frmBudget)._IsNeedToReloadAllData = true;
		}
		catch (Exception ex2)
		{
			MessageBox.Show("Do_CodeUpgrade Error:" + ex2.Message);
		}
	}

	private void ExecuteFillRate()
	{
		try
		{
			FormBudgetResFillRate FM_RateFill = new FormBudgetResFillRate();
			FM_RateFill._UserID = F_UserID;
			FM_RateFill._ProjectCode = projectCode;
			FM_RateFill._ActionName = FormActionName;
			if (FM_RateFill.ShowDialog() == DialogResult.OK)
			{
				BindToGrid("");
			}
			FM_RateFill.Close();
			FM_RateFill.Dispose();
			FM_RateFill = null;
		}
		catch (Exception ex)
		{
			MessageBox.Show("ExecuteFillRate Error:" + ex.Message);
		}
	}

	private void UseMrsCost()
	{
		try
		{
			if (gridMrsBase1.SelectedItems <= 0)
			{
				MessageBox.Show(this, "請先選擇要用的項目。", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				if (MessageBox.Show(this, "確定要引用基本資料庫的單價?\n\n如果選取的項目是【單價分析項目】 或是 【變動單價】，將不會執行引用。", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
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
				aArr.Add("引用基本資料庫的單價中--" + projectCode + "(" + IPStr + ")");
				Archnowledge.Pcces.BUDClass.MrsBaseA MRSA = new Archnowledge.Pcces.BUDClass.MrsBaseA(F_UserID, aArr);
				MRSA.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
				MRSA.ps_projectcode = projectCode;
				for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
				{
					if (!(bool)gridMrsBase1.Rows[i]["Analysis"] && gridMrsBase1.Rows[i].Selected && gridMrsBase1.Rows[i]["CostKind"].ToString().Trim() == "")
					{
						string sPccesCode = gridMrsBase1.Rows[i]["PccesCode"].ToString().Trim();
						string sSQL = "Select cost From MrsBaseA Where PccesCode ='" + sPccesCode + "' ";
						string sCost = DBCLS.GetUserDefine_String(sSQL, "cost");
						MRSA.ps_Issue = F_chgCount;
						MRSA.ps_pccesCode = sPccesCode;
						MRSA.ps_cost = sCost;
						MRSA.UpdItem();
					}
				}
				BindToGrid("");
				FM_INFO.Close();
				FM_INFO = null;
				Cursor = Cursors.Default;
				MessageBox.Show(this, "單價引用完畢。", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("UseMrsCost Error:" + ex.Message);
		}
	}

	private void DoExcelExp()
	{
		try
		{
			gridMrsBase1._ExcelFileName = Application.StartupPath.ToString() + "\\EXCEL_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";
			gridMrsBase1._IsOpenExcelAfterExport = true;
			gridMrsBase1.ExecuteExport(c1GridExportType.Excel);
		}
		catch (Exception ex)
		{
			MessageBox.Show("DoExcelExp Error:" + ex.Message);
		}
	}

	private void DoDBReset()
	{
		try
		{
			DBClass DBCLS = new DBClass();
			DBCLS._FS_UserID = F_UserID;
			string sSQL = "Select A.PccesCode as PccesCode1, A.CName as CName1, A.UnitName as UnitName1,        B.PccesCode as PccesCode2, B.CName as CName2, B.UnitName as UnitName2  From budProjMrsA A left Join MrsBaseA B on A.PccesCode = B.PccesCode  Where A.ProjectCode= '" + projectCode + "' And ( RTrim(A.CName) <> RTrim(B.CName) Or RTrim(A.UnitName) <> RTrim(B.UnitName) ) ";
			DataTable DS_DBCheck = DBCLS.GetUserDefine(sSQL);
			if (DS_DBCheck.Rows.Count > 0)
			{
				FormBudgetDBChkRslt FM_DBRt = new FormBudgetDBChkRslt();
				FM_DBRt._DT_DBChk = DS_DBCheck;
				if (FM_DBRt.ShowDialog() == DialogResult.Ignore)
				{
					DBReSet();
					ultraToolbarsManager1.Tools["mnuGetFromMrs"].SharedProps.Enabled = true;
				}
				FM_DBRt.Close();
				FM_DBRt.Dispose();
				FM_DBRt = null;
			}
			else
			{
				DBReSet();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("DoDBReset Error:" + ex.Message);
		}
	}

	private void DBReSet()
	{
		try
		{
			FormSys_G_Info1 FM_INFO = new FormSys_G_Info1();
			FM_INFO._InfoString = "專案工項資料庫重整中，請稍候! ";
			FM_INFO.Show();
			Application.DoEvents();
			Cursor = Cursors.WaitCursor;
			ArrayList aArr = new ArrayList();
			aArr.Add(F_UserID);
			aArr.Add("專案工項資料庫重整");
			Archnowledge.Pcces.BUDClass.MrsBaseA MrsA = new Archnowledge.Pcces.BUDClass.MrsBaseA(F_UserID, aArr);
			MrsA.ReSetPubCode(projectCode, CommonMethods.GetActionNameString(FormActionName));
			FM_INFO.Close();
			FM_INFO.Dispose();
			BindToGrid("");
			DBClass DBCLS = new DBClass();
			DBCLS._FS_UserID = F_UserID;
			string sCheckSQL = "select Count(*) as ICount From budProjMrsA A Left Join MrsBaseA B  on A.PccesCode = B.PccesCode  Where A.ProjectCode='" + projectCode + "' And A.PubCode <> B.PubCode";
			string sCount = DBCLS.GetUserDefine_String(sCheckSQL, "ICount");
			if (PubTools.Str2Int(sCount) > 0)
			{
				ultraToolbarsManager1.Tools["mnuGetFromMrs"].SharedProps.Enabled = false;
				ultraToolbarsManager1.Tools["mnuDBReSet"].SharedProps.Visible = true;
			}
			else
			{
				ultraToolbarsManager1.Tools["mnuGetFromMrs"].SharedProps.Enabled = true;
				ultraToolbarsManager1.Tools["mnuDBReSet"].SharedProps.Visible = false;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("DBReSet Error:" + ex.Message);
		}
	}

	private void FilterRate()
	{
		try
		{
			FormBudgetResDial1 FM_BDGT_D1 = new FormBudgetResDial1();
			FM_BDGT_D1.Owner = this;
			if (FM_BDGT_D1.ShowDialog() == DialogResult.OK)
			{
				BindToGrid("");
			}
			FM_BDGT_D1.Close();
			FM_BDGT_D1.Dispose();
			FM_BDGT_D1 = null;
		}
		catch (Exception ex)
		{
			MessageBox.Show("FilterRate Error:" + ex.Message);
		}
	}

	private void SendBackMrsBase()
	{
		if (gridMrsBase1.SelectedItems <= 0)
		{
			MessageBox.Show(this, "請先選擇要回傳的項目。", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return;
		}
		if (gridMrsBase1.SelectedItems > 1)
		{
			if (MessageBox.Show(this, "確定要回傳至基本資料庫? 將覆蓋同代碼資料！", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
			{
				return;
			}
		}
		else
		{
			if (MessageBox.Show(this, "確定要回傳至基本資料庫?", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
			{
				return;
			}
			string SBMB_PccesCode = gridMrsBase1[gridMrsBase1.Row, "pccesCode"].ToString().Trim();
			ArrayList aArr = new ArrayList();
			aArr.Clear();
			aArr.Add(F_UserID);
			aArr.Add("專案工項回傳至基本資料庫--查詢選定之工項(PccesCode)是否存在在基本資料庫" + projectCode + "(" + SBMB_PccesCode + ")");
			ModifyDB DBMrsA = new ModifyDB(projectCode, aArr);
			DataTable DT_MrsA = DBMrsA.DBList("Select PccesCode, cName, UnitName, Cost From MrsBaseA Where PccesCode='" + SBMB_PccesCode + "'");
			if (DT_MrsA.Rows.Count > 0)
			{
				string Message = "原資料內容：\n\n工項名稱：" + DT_MrsA.Rows[0]["cName"].ToString() + "\n單\u3000\u3000位：" + DT_MrsA.Rows[0]["unitName"].ToString() + "\n單\u3000\u3000價：" + string.Format("{0:N0}", PubTools.Str2Decimal(DT_MrsA.Rows[0]["Cost"].ToString())) + "\n\n確定要覆蓋?";
				if (MessageBox.Show(this, Message, "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
				{
					return;
				}
			}
		}
		if (base.Owner is frmBudget)
		{
			(base.Owner as frmBudget)._IsNeedToReloadAllData = true;
		}
		FormSys_G_Info1 FM_INFO = new FormSys_G_Info1();
		FM_INFO._InfoString = "資料回傳至基本資料庫中，請稍候! ";
		FM_INFO.Show();
		Application.DoEvents();
		Cursor = Cursors.WaitCursor;
		if (gridMrsBase1.Rows.Count > 1)
		{
			DataTable DT_Snd0 = new DataTable();
			DT_Snd0.Columns.Add("PccesCode", Type.GetType("System.String"));
			DT_Snd0.Columns.Add("pubCode", Type.GetType("System.String"));
			for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
			{
				if (gridMrsBase1.Rows[i].Selected && gridMrsBase1.Rows[i].Visible)
				{
					DataRow DR0 = DT_Snd0.NewRow();
					DR0["PccesCode"] = gridMrsBase1[i, "PccesCode"].ToString().Trim();
					DR0["pubCode"] = gridMrsBase1[i, "PubCode"].ToString().Trim();
					DT_Snd0.Rows.Add(DR0);
				}
			}
			DataTable DT_Process = new DataTable();
			if (base.Owner is frmBudget)
			{
				DT_Process = (base.Owner as frmBudget).FixPubCode(DT_Snd0);
			}
			if (base.Owner is FormSplitContract)
			{
				DT_Process = (base.Owner as FormSplitContract).FixPubCode(DT_Snd0);
			}
			if (base.Owner is FormBudgetChange)
			{
				DT_Process = (base.Owner as FormBudgetChange).FixPubCode(DT_Snd0);
			}
			DataTable DT_Snd1 = new DataTable();
			DT_Snd1.Columns.Add("pubCode", Type.GetType("System.String"));
			for (int i = 0; i < DT_Process.Rows.Count; i++)
			{
				DataRow DR1 = DT_Snd1.NewRow();
				DR1["PubCode"] = ((DT_Process.Rows[i]["resCode"].ToString().Trim() != "") ? DT_Process.Rows[i]["resCode"] : DT_Process.Rows[i]["PubCode"]);
				DT_Snd1.Rows.Add(DR1);
			}
			string IPStr = CommonMethods.GetIPAddress();
			ArrayList aArr = new ArrayList();
			aArr.Clear();
			aArr.Add(F_UserID);
			aArr.Add("專案工項回傳至基本資料庫--" + projectCode + "(" + IPStr + ")");
			string ssDBName = F_CurrentDBName;
			ReSet2Mrs RESET2 = new ReSet2Mrs(aArr);
			RESET2.ls_Issue = F_chgCount;
			DataSet trgDS = RESET2.GetDataSet(ssDBName, CommonMethods.GetActionNameString(FormActionName), projectCode, DT_Snd1, 1);
			RESET2.InputDataSet(ssDBName, "MRS", projectCode, trgDS, 1, "");
			FM_INFO.Close();
			FM_INFO = null;
			MessageBox.Show(this, "回傳完畢。", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = F_UserID;
		string sCheckSQL = "select Count(*) as ICount From budProjMrsA A Left Join MrsBaseA B  on A.PccesCode = B.PccesCode  Where A.ProjectCode='" + projectCode + "' And A.PubCode <> B.PubCode";
		string sCount = DBCLS.GetUserDefine_String(sCheckSQL, "ICount");
		if (PubTools.Str2Int(sCount) > 0)
		{
			ultraToolbarsManager1.Tools["mnuGetFromMrs"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuDBReSet"].SharedProps.Visible = true;
		}
		else
		{
			ultraToolbarsManager1.Tools["mnuGetFromMrs"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuDBReSet"].SharedProps.Visible = false;
		}
		BindToGrid("");
		Cursor = Cursors.Default;
	}

	private void SendBackMrsBase_NameUnit()
	{
		if (gridMrsBase1.SelectedItems <= 0)
		{
			MessageBox.Show(this, "請先選擇要回傳的項目。", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		else
		{
			if (gridMrsBase1.SelectedItems > 1 && MessageBox.Show(this, "確定要回傳選取項至基本資料庫? ", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
			{
				return;
			}
			DataTable DT_Choosen = new DataTable();
			DT_Choosen.Columns.Add("PccesCode", Type.GetType("System.String"));
			DT_Choosen.Columns.Add("CName", Type.GetType("System.String"));
			DT_Choosen.Columns.Add("UnitName", Type.GetType("System.String"));
			for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
			{
				if (gridMrsBase1.Rows[i].Selected && gridMrsBase1.Rows[i].Visible)
				{
					DataRow DR = DT_Choosen.NewRow();
					DR["PccesCode"] = gridMrsBase1[i, "pccesCode"];
					DR["CName"] = gridMrsBase1[i, "cName"];
					DR["UnitName"] = gridMrsBase1[i, "unitName"];
					DT_Choosen.Rows.Add(DR);
				}
			}
			FormProgress FM = new FormProgress();
			FM._Max = DT_Choosen.Rows.Count;
			FM._Min = 0;
			FM.Message = "正在計算...";
			FM.TopMost = true;
			FM.Show();
			DBClass DBCls = new DBClass();
			DBCls._FS_UserID = F_UserID;
			string sSQL = "";
			for (int i = 0; i < DT_Choosen.Rows.Count; i++)
			{
				FM.SetProgressValue(i);
				Application.DoEvents();
				Cursor = Cursors.WaitCursor;
				sSQL = "If Exists(Select * From mrsBaseA Where pccesCode='" + DT_Choosen.Rows[i]["pccesCode"].ToString() + "')    Update mrsBaseA Set cName='" + DT_Choosen.Rows[i]["cName"].ToString() + "', unitName='" + DT_Choosen.Rows[i]["unitName"].ToString() + "' Where pccesCode='" + DT_Choosen.Rows[i]["pccesCode"].ToString() + "'  else    Insert Into mrsBaseA(pccesCode, cName, unitName, post) values('" + DT_Choosen.Rows[i]["pccesCode"].ToString() + "','" + DT_Choosen.Rows[i]["cName"].ToString() + "','" + DT_Choosen.Rows[i]["unitName"].ToString() + "','1') ";
				DBCls.ExecuteCommand(sSQL);
			}
			FM.Hide();
			FM.Dispose();
			FM = null;
			Cursor = Cursors.Default;
			MessageBox.Show(this, "完成回傳，共計 " + DT_Choosen.Rows.Count + " 項", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void GetFromMrsBase()
	{
		try
		{
			if (gridMrsBase1.SelectedItems <= 0)
			{
				MessageBox.Show(this, "請先選擇要引用的項目。", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				if (MessageBox.Show(this, "確定要引用?", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
				{
					return;
				}
				if (base.Owner is frmBudget)
				{
					(base.Owner as frmBudget)._IsNeedToReloadAllData = true;
				}
				FormSys_G_Info1 FM_INFO = new FormSys_G_Info1();
				FM_INFO._InfoString = "引用基本資料庫中，請稍候! ";
				FM_INFO.Show();
				Application.DoEvents();
				Cursor = Cursors.WaitCursor;
				if (gridMrsBase1.Rows.Count > 1)
				{
					DataTable DT_Snd = new DataTable();
					DT_Snd.Columns.Add("PccesCode", Type.GetType("System.String"));
					DT_Snd.Columns.Add("pubCode", Type.GetType("System.String"));
					for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
					{
						if (gridMrsBase1.Rows[i].Selected && gridMrsBase1.Rows[i].Visible)
						{
							DataRow DR = DT_Snd.NewRow();
							DR["PccesCode"] = gridMrsBase1[i, "PccesCode"].ToString().Trim();
							DR["pubCode"] = gridMrsBase1[i, "PubCode"].ToString().Trim();
							DT_Snd.Rows.Add(DR);
						}
					}
					DataTable DT_Process = (base.Owner as frmBudget).FixPubCode(DT_Snd);
					try
					{
						string IPStr = CommonMethods.GetIPAddress();
						ArrayList aArr = new ArrayList();
						aArr.Clear();
						aArr.Add(F_UserID);
						aArr.Add("專案工項從基本資料庫引用--" + projectCode + "(" + IPStr + ")");
						string ssDBName = F_CurrentDBName;
						ReSet2Mrs RESET2 = new ReSet2Mrs(aArr);
						for (int i = 0; i < DT_Process.Rows.Count; i++)
						{
							if (!(DT_Process.Rows[i]["resCode"].ToString().Trim() == ""))
							{
								RESET2.ls_apubCode = DT_Process.Rows[i]["resCode"].ToString();
								RESET2.ls_projectcode = projectCode;
								RESET2.ls_srckind = CommonMethods.GetActionNameString(FormActionName);
								RESET2.ls_Issue = F_chgCount;
								RESET2.AllMrs2Proj();
							}
						}
						FM_INFO.Close();
						FM_INFO = null;
						MessageBox.Show(this, "引用完畢。", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
					catch (Exception ex)
					{
						CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudgetRes.cs" + ex.Message);
						FM_INFO.Close();
						FM_INFO = null;
						MessageBox.Show(this, "回傳有誤，請確認後再執行。", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
				}
				BindToGrid("");
				Cursor = Cursors.Default;
			}
		}
		catch (Exception ex2)
		{
			MessageBox.Show("GetFromMrsBase Error:" + ex2.Message);
		}
	}

	private void Do_Export()
	{
		try
		{
			string sFilter = "Microsoft Excel 97/2000 files (*.xls)|*.xls";
			saveFileDialog1.Filter = sFilter;
			saveFileDialog1.RestoreDirectory = true;
			saveFileDialog1.FileName = projectCode + "_專案工項";
			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				gridMrsBase1.SaveExcel(saveFileDialog1.FileName, projectCode, FileFlags.AsDisplayed);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Do_Export Error:" + ex.Message);
		}
	}

	private void Do_ToolBarFind()
	{
		try
		{
			if (gridMrsBase1.Rows.Count <= 1)
			{
				return;
			}
			int iStart = gridMrsBase1.Row + 1;
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
				iStart = gridMrsBase1.Row + 1;
			}
			if (sSearchText.Trim() == "")
			{
				return;
			}
			for (int i = iStart; i < gridMrsBase1.Rows.Count; i++)
			{
				for (int j = 1; j < gridMrsBase1.Cols.Count; j++)
				{
					if (gridMrsBase1[i, j] == null || gridMrsBase1[i, j].ToString().IndexOf(sSearchText) <= -1)
					{
						continue;
					}
					gridMrsBase1.Row = i;
					gridMrsBase1.Select();
					gridMrsBase1.TopRow = i;
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
		catch (Exception ex)
		{
			MessageBox.Show("Do_ToolBarFind Error:" + ex.Message);
		}
	}

	private void FindMrsInItemA()
	{
		int RowIndex = gridMrsBase1.Row;
		if (RowIndex <= 0)
		{
			return;
		}
		ultraLabel2.Text = "詳細表查詢結果列表";
		BtnReCalSmall.Visible = false;
		btnAddBookList.Visible = false;
		ultraButton9.Visible = false;
		FindParentFromBudget = true;
		if (gridMrsBase1.Rows.Count < 2)
		{
			return;
		}
		if (false)
		{
			BindToGrid("");
		}
		int PubCode = ArchConvert.Obj2Int(gridMrsBase1[RowIndex, "PubCode"]);
		DataSet dsMrsA = projMrsA.GetProjMrsAInItemA(projectCode, PubCode);
		if (dsMrsA != null && dsMrsA.Tables.Count > 0)
		{
			decimal TotalusrQty = 0m;
			foreach (DataRow theRow in dsMrsA.Tables[0].Rows)
			{
				int Sno = ArchConvert.Obj2Int(theRow["Sno"]);
				string FullItemNo = itemA.GetItemAFullItemNoBySno(projectCode, Sno);
				theRow["ItemNo"] = FullItemNo;
				TotalusrQty += ArchConvert.Obj2Decimal(theRow["usrQty"]);
			}
			DataRow newRow = dsMrsA.Tables[0].NewRow();
			newRow["cName"] = "總數";
			newRow["usrQty"] = TotalusrQty;
			newRow["PrintNo"] = "9999";
			dsMrsA.Tables[0].Rows.Add(newRow);
			if (dsMrsA.Tables[0].Rows.Count > 1)
			{
				GRID2_STATUS = FormStatus.Edit;
				RememberColsProps2();
				Data2GridParentFromBudget(dsMrsA);
				GRID2_STATUS = FormStatus.Normal;
				pnlParent.Height = 200;
				splitter1.Enabled = true;
			}
			else
			{
				MessageBox.Show(this, "查無此項資料！", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		FindParentFromBudget = false;
	}

	private void Data2GridParentFromBudget(DataSet dsParentProjMrsA)
	{
		CellStyle CS1 = gridMrsBase2.Styles.Add("AnalysisColor");
		CellStyle CS2 = gridMrsBase2.Styles.Add("LEMColor");
		CellStyle CS3 = gridMrsBase2.Styles.Add("WColor");
		CS1.ForeColor = Color.Red;
		CS2.ForeColor = Color.Teal;
		CS3.ForeColor = Color.Purple;
		int rowCount = dsParentProjMrsA.Tables[0].Rows.Count;
		gridMrsBase2.Clear(ClearFlags.All);
		gridMrsBase2.Select();
		gridMrsBase2.Rows.Count = rowCount + 1;
		SetGrid2Column();
		gridMrsBase2.Redraw = false;
		DataView dv = dsParentProjMrsA.Tables[0].DefaultView;
		dv.Sort = "PrintNo";
		for (int i = 0; i < dv.Count; i++)
		{
			C1.Win.C1FlexGrid.Row gridRow = gridMrsBase2.Rows[i + 1];
			DataRow drProjMrsA = dv[i].Row;
			string sItemClass = "";
			string PccesCode = ArchConvert.Obj2String(drProjMrsA["pccesCode"]);
			if (PccesCode.Length > 0)
			{
				sItemClass = PccesCode.Substring(0, 1);
			}
			gridRow["PccesCode"] = PccesCode;
			if (sItemClass == "L" || sItemClass == "E" || sItemClass == "M")
			{
				gridMrsBase2.Rows[i + 1].Style = gridMrsBase2.Styles["LEMColor"];
			}
			else if (sItemClass == "W")
			{
				gridMrsBase2.Rows[i + 1].Style = gridMrsBase2.Styles["WColor"];
			}
			gridRow["CName"] = drProjMrsA["cName"].ToString();
			if (drProjMrsA["analysis"].ToString().Trim() == "1")
			{
				gridRow["Analysis"] = true;
				gridMrsBase2.Rows[i + 1].Style = gridMrsBase2.Styles["AnalysisColor"];
				CellRange rg = gridMrsBase2.GetCellRange(i + 1, gridMrsBase2.Cols["AnaImg"].SafeIndex);
				rg.Style = gridMrsBase2.Styles["img"];
				rg.Image = imageList2.Images[0];
			}
			else
			{
				gridRow["Analysis"] = false;
			}
			gridRow["ItemNo"] = drProjMrsA["ItemNo"];
			gridRow["UnitName"] = drProjMrsA["unitName"];
			gridRow["Rate"] = drProjMrsA["rate"];
			gridRow["CostKind"] = drProjMrsA["costKind"];
			gridRow["LRate"] = drProjMrsA["lRate"];
			gridRow["ERate"] = drProjMrsA["eRate"];
			gridRow["MRate"] = drProjMrsA["mRate"];
			gridRow["WRate"] = drProjMrsA["wRate"];
			gridRow["XNameC"] = drProjMrsA["xNameC"];
			gridRow["Memo"] = drProjMrsA["memo"];
			gridRow["PubCode"] = drProjMrsA["pubCode"];
			gridRow["Cost"] = drProjMrsA["cost"];
			gridRow["usrQty"] = drProjMrsA["usrQty"];
			gridRow["LockCost"] = drProjMrsA["LockCost"].ToString().Trim() == "1";
			gridRow["usrAmt"] = drProjMrsA["usrAmt"];
			gridRow["analysisQty"] = drProjMrsA["analysisQty"];
			gridRow["itemDuty"] = drProjMrsA["itemDuty"];
			if (dsParentProjMrsA.Tables[0].Columns.IndexOf("qtySubtotal") != -1)
			{
				gridRow["qtySubtotal"] = drProjMrsA["qtySubtotal"];
			}
			gridRow["surName"] = drProjMrsA["surName"];
			gridRow["fixPrice"] = drProjMrsA["fixPrice"].ToString().Trim() == "1";
			gridRow["Account"] = drProjMrsA["Account"];
			if (drProjMrsA["PwrSet"] != DBNull.Value)
			{
				gridRow["PwrSet"] = PwrSet.GetName(dsPwrSet, PubTools.Str2Int(drProjMrsA["PwrSet"]));
			}
			else
			{
				gridRow["PwrSet"] = PwrSet.GetDefaultName(dsPwrSet);
			}
		}
		gridMrsBase2.Redraw = true;
	}

	private void Find_Parent()
	{
		try
		{
			ultraLabel2.Text = "父項查詢結果列表";
			BtnReCalSmall.Visible = true;
			ultraButton9.Visible = true;
			if (gridMrsBase1.Rows.Count >= 2)
			{
				if (false)
				{
					BindToGrid("");
				}
				int pubCode = ArchConvert.Obj2Int(gridMrsBase1[gridMrsBase1.Row, "PubCode"]);
				DataSet dsParentProjMrsA = projMrsA.GetParentProjMrsAByPubCode(projectCode, pubCode);
				if (dsParentProjMrsA.Tables[0].Rows.Count > 0)
				{
					GRID2_STATUS = FormStatus.Edit;
					RememberColsProps2();
					Data2GridParent(dsParentProjMrsA);
					GRID2_STATUS = FormStatus.Normal;
					pnlParent.Height = 200;
					splitter1.Enabled = true;
				}
				else
				{
					MessageBox.Show(this, "查無父項資料！", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					pnlParent.Height = 0;
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Find_Parent Error:" + ex.Message);
		}
	}

	private void Data2GridParent(DataSet dsParentProjMrsA)
	{
		CellStyle CS1 = gridMrsBase2.Styles.Add("AnalysisColor");
		CellStyle CS2 = gridMrsBase2.Styles.Add("LEMColor");
		CellStyle CS3 = gridMrsBase2.Styles.Add("WColor");
		CS1.ForeColor = Color.Red;
		CS2.ForeColor = Color.Teal;
		CS3.ForeColor = Color.Purple;
		int rowCount = dsParentProjMrsA.Tables[0].Rows.Count;
		gridMrsBase2.Clear(ClearFlags.All);
		gridMrsBase2.Select();
		gridMrsBase2.Rows.Count = rowCount + 1;
		SetGrid2Column();
		gridMrsBase2.Redraw = false;
		for (int i = 0; i < rowCount; i++)
		{
			C1.Win.C1FlexGrid.Row gridRow = gridMrsBase2.Rows[i + 1];
			DataRow drProjMrsA = dsParentProjMrsA.Tables[0].Rows[i];
			string sItemClass = "";
			string PccesCode = ArchConvert.Obj2String(drProjMrsA["pccesCode"]);
			if (PccesCode.Length > 0)
			{
				sItemClass = PccesCode.Substring(0, 1);
			}
			gridRow["PccesCode"] = PccesCode;
			if (sItemClass == "L" || sItemClass == "E" || sItemClass == "M")
			{
				gridMrsBase2.Rows[i + 1].Style = gridMrsBase2.Styles["LEMColor"];
			}
			else if (sItemClass == "W")
			{
				gridMrsBase2.Rows[i + 1].Style = gridMrsBase2.Styles["WColor"];
			}
			gridRow["CName"] = drProjMrsA["cName"].ToString();
			if (drProjMrsA["analysis"].ToString().Trim() == "1")
			{
				gridRow["Analysis"] = true;
				gridMrsBase2.Rows[i + 1].Style = gridMrsBase2.Styles["AnalysisColor"];
				CellRange rg = gridMrsBase2.GetCellRange(i + 1, gridMrsBase2.Cols["AnaImg"].SafeIndex);
				rg.Style = gridMrsBase2.Styles["img"];
				rg.Image = imageList2.Images[0];
			}
			else
			{
				gridRow["Analysis"] = false;
			}
			gridRow["UnitName"] = drProjMrsA["unitName"];
			gridRow["Rate"] = drProjMrsA["rate"];
			gridRow["CostKind"] = drProjMrsA["costKind"];
			gridRow["LRate"] = drProjMrsA["lRate"];
			gridRow["ERate"] = drProjMrsA["eRate"];
			gridRow["MRate"] = drProjMrsA["mRate"];
			gridRow["WRate"] = drProjMrsA["wRate"];
			gridRow["XNameC"] = drProjMrsA["xNameC"];
			gridRow["Memo"] = drProjMrsA["memo"];
			gridRow["PubCode"] = drProjMrsA["pubCode"];
			gridRow["Cost"] = drProjMrsA["cost"];
			gridRow["usrQty"] = drProjMrsA["usrQty"];
			gridRow["LockCost"] = drProjMrsA["LockCost"].ToString().Trim() == "1";
			gridRow["usrAmt"] = drProjMrsA["usrAmt"];
			gridRow["analysisQty"] = drProjMrsA["analysisQty"];
			gridRow["itemDuty"] = drProjMrsA["itemDuty"];
			gridRow["qtySubtotal"] = drProjMrsA["qtySubtotal"];
			gridRow["surName"] = drProjMrsA["surName"];
			gridRow["fixPrice"] = drProjMrsA["fixPrice"].ToString().Trim() == "1";
			gridRow["Account"] = drProjMrsA["Account"];
			if (drProjMrsA["PwrSet"] != DBNull.Value)
			{
				gridRow["PwrSet"] = PwrSet.GetName(dsPwrSet, PubTools.Str2Int(drProjMrsA["PwrSet"]));
			}
			else
			{
				gridRow["PwrSet"] = PwrSet.GetDefaultName(dsPwrSet);
			}
		}
		gridMrsBase2.Redraw = true;
	}

	private void Execute_WorkItemEdit()
	{
		try
		{
			if (gridMrsBase1.Row <= 0)
			{
				return;
			}
			C1.Win.C1FlexGrid.Row gridRow = gridMrsBase1.Rows[gridMrsBase1.Row];
			if (gridRow["PubCode"] != null)
			{
				FormMrsBaseEdit FM_EDIT = new FormMrsBaseEdit();
				FM_EDIT._UserID = F_UserID;
				FM_EDIT._EditMode = MrsBaseEditFormType.Edit;
				FM_EDIT._CallerFormName = base.Name;
				FM_EDIT._ActionName = FormActionName;
				FM_EDIT._ProjectCode = projectCode;
				FM_EDIT._Istemplate = IsTemplate || NotAllowEditingInCostEst(gridRow["PccesCode"].ToString());
				FM_EDIT._PubCode = (int)gridRow["PubCode"];
				FM_EDIT._ExternalCost = Convert.ToDouble(gridRow["Cost"]);
				FM_EDIT._IsLockAn = GetIsLockAnalys();
				FM_EDIT._CallerFormName = "FormBudget";
				FM_EDIT._MainCost = F_AnaCst.ToString();
				if (DialogResult.OK == FM_EDIT.ShowDialog(this))
				{
					AfterWorkItemEdited();
				}
				FM_EDIT.Close();
				FM_EDIT.Dispose();
				FM_EDIT = null;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Execute_WorkItemEdit Error:" + ex.Message);
		}
	}

	private bool NotAllowEditingInCostEst(string pccesCode)
	{
		BudProjMrsA budProjMrsA = new BudProjMrsA();
		try
		{
			return budgetType == 5 && budProjMrsA.WorkItemExists(parentProjectCode, pccesCode);
		}
		catch (Exception ex)
		{
			MessageBox.Show("NotAllowEditingInCostEst Error:" + ex.Message);
		}
		return budgetType == 5 && budProjMrsA.WorkItemExists(parentProjectCode, pccesCode);
	}

	private void ExecuteAutoNumForm()
	{
		try
		{
			FormAutoNum FM_AUTO_NO = new FormAutoNum(F_UserID);
			FM_AUTO_NO.ShowDialog(this);
			FM_AUTO_NO.Close();
			FM_AUTO_NO.Dispose();
			FM_AUTO_NO = null;
		}
		catch (Exception ex)
		{
			MessageBox.Show("ExecuteAutoNumForm Error:" + ex.Message);
		}
	}

	private void AfterWorkItemEdited()
	{
		try
		{
			Do_Filter();
		}
		catch (Exception ex)
		{
			MessageBox.Show("AfterWorkItemEdited Error:" + ex.Message);
		}
	}

	private void SetPopupMenuEnable()
	{
		try
		{
			ultraToolbarsManager1.BeginUpdate();
			ultraToolbarsManager1.Enabled = true;
			ultraToolbarsManager1.EndUpdate();
		}
		catch (Exception ex)
		{
			MessageBox.Show("SetPopupMenuEnable Error:" + ex.Message);
		}
	}

	private void SetPopupMenuDisable()
	{
		try
		{
			ultraToolbarsManager1.Enabled = false;
		}
		catch (Exception ex)
		{
			MessageBox.Show("SetPopupMenuDisable Error:" + ex.Message);
		}
	}

	private void ExecuteBreakdownForm(object Sender)
	{
		try
		{
			FormMrsBaseBreakdown frmBD = new FormMrsBaseBreakdown();
			frmBD.PubCode = ((Sender == gridMrsBase1) ? ((int)gridMrsBase1[gridMrsBase1.Row, "PubCode"]) : ((int)gridMrsBase2[gridMrsBase2.Row, "PubCode"]));
			frmBD._UserID = F_UserID;
			frmBD._ActionName = FormActionName;
			frmBD.ProjectCode = projectCode;
			frmBD._IsSBID = F_IsSBID;
			if (FormActionName == PccesFormAction.BID)
			{
				frmBD._IsLockAn = GetIsLockAnalys();
			}
			frmBD._Istemplate = IsTemplate;
			frmBD._Issue = PubTools.Str2Int(F_chgCount);
			frmBD._ContractApproved = HasApproved;
			frmBD.Owner = this;
			frmBD.ShowDialog();
			if (F_IsNeedToReloadAllData)
			{
				BindToGrid("");
			}
			SetPopupMenuEnable();
		}
		catch (Exception ex)
		{
			MessageBox.Show("ExecuteBreakdownForm Error:" + ex.Message);
		}
	}

	private bool GetIsLockAnalys()
	{
		bool rtnStr = false;
		try
		{
			string sSQL = "Select IsLockAn from bidProject where projectCode = '" + projectCode + "'";
			ArrayList aArr = new ArrayList();
			aArr.Add(F_UserID);
			aArr.Add("取pccescode的值");
			ModifyDB ModDB = new ModifyDB(projectCode, aArr);
			DataTable DT = new DataTable();
			DT = ModDB.DBList(sSQL);
			if (DT.Rows.Count > 0 && DT.Rows[0]["IsLockAn"].ToString().Trim() == "Y")
			{
				rtnStr = true;
			}
			ModDB = null;
			aArr = null;
		}
		catch (Exception ex)
		{
			MessageBox.Show("GetIsLockAnalys Error:" + ex.Message);
		}
		return rtnStr;
	}

	private void gridMrsBase1_AfterRowColChange(object sender, RangeEventArgs e)
	{
		try
		{
			if (F_IsSBID)
			{
				return;
			}
			IsCanEdit = true;
			ultraToolbarsManager1.Tools["mnuEditItem"].SharedProps.Enabled = true;
			if (gridMrsBase1.MouseRow < 1)
			{
				return;
			}
			if (gridMrsBase1.Row < 1)
			{
				ultraToolbarsManager1.Tools["mnuEditItem"].SharedProps.Enabled = false;
			}
			else
			{
				ultraToolbarsManager1.Tools["mnuEditItem"].SharedProps.Enabled = true;
			}
			if (IsKeyScroll && RowChangeCol > 0 && gridMrsBase1.Col == 0)
			{
				gridMrsBase1.Col = RowChangeCol;
			}
			string sColName = gridMrsBase1.Cols[gridMrsBase1.Col].Name.Trim().ToUpper();
			string sMemo = ((gridMrsBase1[gridMrsBase1.Row, "Memo"] != null && gridMrsBase1[gridMrsBase1.Row, "Memo"].ToString().Trim().Length > 0) ? gridMrsBase1[gridMrsBase1.Row, "Memo"].ToString().Trim().Substring(0, 1) : "");
			bool bAnalysis = gridMrsBase1[gridMrsBase1.Row, "Analysis"] != null && (bool)gridMrsBase1[gridMrsBase1.Row, "Analysis"];
			bool bAllowEditing = gridMrsBase1.Cols[gridMrsBase1.Col].AllowEditing;
			if (IsPressCtrl)
			{
				return;
			}
			switch (sColName)
			{
			default:
				if (!(sColName == "EUNIT"))
				{
					break;
				}
				goto case "CNAME";
			case "CNAME":
			case "UNITNAME":
			case "ENAME":
				if (sMemo != "#")
				{
					e.Cancel = true;
					IsCanEdit = false;
					return;
				}
				break;
			}
			if (sColName == "PCCESCODE")
			{
				e.Cancel = true;
				IsCanEdit = false;
			}
			else if (bAnalysis && sColName == "COST")
			{
				e.Cancel = true;
				IsCanEdit = false;
			}
			else if (!bAllowEditing)
			{
				e.Cancel = true;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("gridMrsBase1_AfterRowColChange Error:" + ex.Message);
		}
	}

	private void gridMrsBase1_MouseUp(object sender, MouseEventArgs e)
	{
		try
		{
			Debug.WriteLine("BudgetRes_MouseUp : (" + gridMrsBase1.MouseRow + "," + gridMrsBase1.MouseCol + ")");
			if (e.Button == MouseButtons.Left && gridMrsBase1.MouseRow == 0)
			{
				gridMrsBase1.Sort(SortFlags.UseColSort, gridMrsBase1.MouseCol);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("gridMrsBase1_MouseUp Error:" + ex.Message);
		}
	}

	private void FormBudgetRes_Activated(object sender, EventArgs e)
	{
		try
		{
			if (FORM_STATUS == FormStatus.Active && iLEMW_RateErr > 0)
			{
				FORM_STATUS = FormStatus.Normal;
				MessageBox.Show(this, "發現工項中，有任一人機料雜比率超過100的項目，請檢查底色為綠色的項目。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("FormBudgetRes_Activated Error:" + ex.Message);
		}
	}

	private void gridMrsBase1_AfterSelChange(object sender, RangeEventArgs e)
	{
		try
		{
			int RowIndex = gridMrsBase1.MouseRow;
			int ColIndex = gridMrsBase1.MouseCol;
			if (RowIndex <= 0 || ColIndex <= 0)
			{
				return;
			}
			C1.Win.C1FlexGrid.Row GridRow = gridMrsBase1.Rows[RowIndex];
			string ColumnName = gridMrsBase1.Cols[ColIndex].Name;
			if (GridRow["Analysis"] != null)
			{
				if (!gridMrsBase1.Cols[gridMrsBase1.MouseCol].AllowEditing)
				{
					FORM_STATUS = FormStatus.Normal;
				}
				if (ArchConvert.Obj2Bool(GridRow["Analysis"]))
				{
					ultraToolbarsManager1.Tools["mnuAnalysis"].SharedProps.Enabled = true;
				}
				else
				{
					ultraToolbarsManager1.Tools["mnuAnalysis"].SharedProps.Enabled = false;
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("gridMrsBase1_AfterSelChange Error:" + ex.Message);
		}
	}

	private void gridMrsBase1_LeaveCell(object sender, EventArgs e)
	{
		try
		{
			if (!gridMrsBase1.Cols[gridMrsBase1.MouseCol].AllowEditing)
			{
				FORM_STATUS = FormStatus.Normal;
				SetPopupMenuEnable();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("gridMrsBase1_LeaveCell Error:" + ex.Message);
		}
	}

	private void gridMrsBase1_StartEdit(object sender, RowColEventArgs e)
	{
		try
		{
			FORM_STATUS = FormStatus.Edit;
			if (e.Col == gridMrsBase1.Cols["Cost"].SafeIndex)
			{
				gridMrsBase1[e.Row, "Cost"] = string.Format("{0:N" + F_AnaCst + "}", gridMrsBase1[e.Row, "Cost"]);
			}
			SetPopupMenuDisable();
		}
		catch (Exception ex)
		{
			MessageBox.Show("gridMrsBase1_StartEdit Error:" + ex.Message);
		}
	}

	private void gridMrsBase1_AfterEdit(object sender, RowColEventArgs e)
	{
		try
		{
			SetPopupMenuEnable();
			if (FORM_STATUS == FormStatus.Edit && gridMrsBase1.Cols[e.Col].Name == "LockCost")
			{
				FormSys_G_Info1 FM_INFO = new FormSys_G_Info1();
				FM_INFO._InfoString = "項目鎖定中，請稍候! ";
				FM_INFO.Show();
				Application.DoEvents();
				Cursor = Cursors.WaitCursor;
				gridMrsBase1.Enabled = false;
				Application.DoEvents();
				DataTable dt = new DataTable();
				dt.Columns.Add("PubCode", Type.GetType("System.String"));
				dt.Columns.Add("mode", Type.GetType("System.String"));
				if (gridMrsBase1.SelectedItems > 1)
				{
					for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
					{
						if (gridMrsBase1.Rows[i].Selected && gridMrsBase1.Rows[i].Visible)
						{
							DataRow newRow = dt.NewRow();
							newRow[0] = gridMrsBase1[i, "PubCode"].ToString().Trim();
							newRow[1] = "0";
							dt.Rows.Add(newRow);
						}
					}
				}
				else
				{
					DataRow newRow = dt.NewRow();
					newRow[0] = gridMrsBase1[e.Row, "PubCode"].ToString().Trim();
					newRow[1] = "0";
					dt.Rows.Add(newRow);
				}
				string IPStr = CommonMethods.GetIPAddress();
				ArrayList aArr = new ArrayList();
				aArr.Clear();
				aArr.Add(F_UserID);
				aArr.Add("專案工項維護後存檔之鎖定異動--" + projectCode + "(" + IPStr + ")");
				string sLockCheck = (((bool)gridMrsBase1[e.Row, "LockCost"]) ? "1" : "0");
				Archnowledge.Pcces.BUDClass.MrsBaseA MrsBaseA1 = new Archnowledge.Pcces.BUDClass.MrsBaseA(F_UserID, aArr);
				MrsBaseA1.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
				MrsBaseA1.LockCost(projectCode, dt, sLockCheck, "LockCost");
				FM_INFO.Close();
				FM_INFO.Dispose();
				BindToGrid("");
				if (base.Owner is frmBudget)
				{
					(base.Owner as frmBudget)._IsNeedToReloadAllData = true;
				}
				gridMrsBase1.Enabled = true;
				SetPopupMenuEnable();
				return;
			}
			if (FORM_STATUS == FormStatus.Edit && gridMrsBase1.Cols[e.Col].Name == "fixPrice")
			{
				FormSys_G_Info1 FM_INFO = new FormSys_G_Info1();
				FM_INFO._InfoString = "項目標單固定單價處理中，請稍候!! ";
				FM_INFO.Show();
				Application.DoEvents();
				Cursor = Cursors.WaitCursor;
				gridMrsBase1.Enabled = false;
				Application.DoEvents();
				DataTable dt = new DataTable();
				dt.Columns.Add("PubCode", Type.GetType("System.String"));
				dt.Columns.Add("mode", Type.GetType("System.String"));
				DataRow ndr = dt.NewRow();
				ndr[0] = gridMrsBase1[e.Row, "PubCode"].ToString().Trim();
				ndr[1] = "0";
				dt.Rows.Add(ndr);
				string IPStr = CommonMethods.GetIPAddress();
				ArrayList aArr = new ArrayList();
				aArr.Clear();
				aArr.Add(F_UserID);
				aArr.Add("專案工項維護後存檔之鎖定異動--" + projectCode + "(" + IPStr + ")");
				string sLockCheck = (((bool)gridMrsBase1[e.Row, "fixPrice"]) ? "1" : "0");
				Archnowledge.Pcces.BUDClass.MrsBaseA MrsBaseA1 = new Archnowledge.Pcces.BUDClass.MrsBaseA(F_UserID, aArr);
				MrsBaseA1.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
				MrsBaseA1.LockCost(projectCode, dt, sLockCheck, "fixPrice");
				MrsBaseA1.UpdateMemofixprice(projectCode, sLockCheck);
				Archnowledge.Pcces.BUDClass.ItemA ItemACom = new Archnowledge.Pcces.BUDClass.ItemA(aArr);
				ItemACom.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
				ItemACom.ps_projectCode = projectCode;
				ItemACom.UpdateMemofixprice(projectCode, "", sLockCheck, gridMrsBase1[e.Row, "PubCode"].ToString().Trim(), "W");
				ItemACom = null;
				FM_INFO.Close();
				FM_INFO.Dispose();
				BindToGrid("");
				if (base.Owner is frmBudget)
				{
					(base.Owner as frmBudget)._IsNeedToReloadAllData = true;
				}
				gridMrsBase1.Enabled = true;
				SetPopupMenuEnable();
				return;
			}
			string columnName = gridMrsBase1.Cols[e.Col].Name;
			if (FORM_STATUS == FormStatus.Edit)
			{
				switch (columnName)
				{
				default:
					if (!(columnName == "IsGreenEnergy"))
					{
						break;
					}
					goto case "IsGreenItem";
				case "IsGreenItem":
				case "IsGreenMethod":
				case "IsGreenMaterial":
				{
					FormSys_G_Info1 FM_INFO = new FormSys_G_Info1();
					FM_INFO._InfoString = "綠色內涵處理中，請稍候!! ";
					FM_INFO.Show();
					Application.DoEvents();
					Cursor = Cursors.WaitCursor;
					gridMrsBase1.Enabled = false;
					Application.DoEvents();
					DataTable dtGreenItem = new DataTable("BudProjMrsA");
					dtGreenItem.Columns.Add("ProjectCode", Type.GetType("System.String"));
					dtGreenItem.Columns.Add("PubCode", Type.GetType("System.Int32"));
					dtGreenItem.Columns.Add("IsGreenItem", Type.GetType("System.Boolean"));
					DataSet dsGreenItem = new DataSet();
					dsGreenItem.Tables.Add(dtGreenItem);
					if (gridMrsBase1.SelectedItems > 1)
					{
						for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
						{
							if (gridMrsBase1.Rows[i].Selected && gridMrsBase1.Rows[i].Visible)
							{
								DataRow newRow = dtGreenItem.NewRow();
								newRow["ProjectCode"] = projectCode;
								newRow["PubCode"] = ArchConvert.Obj2String(gridMrsBase1[i, "PubCode"]);
								newRow["IsGreenItem"] = ArchConvert.Obj2Bool(gridMrsBase1[e.Row, columnName]);
								dtGreenItem.Rows.Add(newRow);
							}
						}
					}
					else
					{
						DataRow newRow = dtGreenItem.NewRow();
						newRow["ProjectCode"] = projectCode;
						newRow["PubCode"] = ArchConvert.Obj2String(gridMrsBase1[e.Row, "PubCode"]);
						newRow["IsGreenItem"] = ArchConvert.Obj2Bool(gridMrsBase1[e.Row, columnName]);
						dtGreenItem.Rows.Add(newRow);
					}
					BudProjMrsA budProjMrsA = new BudProjMrsA();
					budProjMrsA.GetDatasetUpdateGreenItem(dsGreenItem, columnName);
					FM_INFO.Close();
					FM_INFO.Dispose();
					BindToGrid("");
					if (base.Owner is frmBudget)
					{
						(base.Owner as frmBudget)._IsNeedToReloadAllData = true;
					}
					gridMrsBase1.Enabled = true;
					SetPopupMenuEnable();
					return;
				}
				}
			}
			if (FORM_STATUS == FormStatus.Edit)
			{
				ArrayList aArr = new ArrayList();
				aArr.Add(F_UserID);
				aArr.Add("WinFORM 基本工料");
				Archnowledge.Pcces.BUDClass.MrsBaseA dbMrsBase = new Archnowledge.Pcces.BUDClass.MrsBaseA(F_UserID, aArr);
				dbMrsBase.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
				dbMrsBase.ps_projectcode = projectCode;
				dbMrsBase.ps_pccesCode = gridMrsBase1[e.Row, "PccesCode"].ToString();
				if (gridMrsBase1.Cols[e.Col].Name == "Cost")
				{
					dbMrsBase.ps_cost = gridMrsBase1[e.Row, e.Col].ToString();
				}
				if (gridMrsBase1.Cols[e.Col].Name == "Memo")
				{
					dbMrsBase.ps_memo = gridMrsBase1[e.Row, e.Col].ToString();
				}
				if (gridMrsBase1.Cols[e.Col].Name == "LRate")
				{
					dbMrsBase.ps_lRate = gridMrsBase1[e.Row, e.Col].ToString();
				}
				if (gridMrsBase1.Cols[e.Col].Name == "ERate")
				{
					dbMrsBase.ps_eRate = gridMrsBase1[e.Row, e.Col].ToString();
				}
				if (gridMrsBase1.Cols[e.Col].Name == "MRate")
				{
					dbMrsBase.ps_mRate = gridMrsBase1[e.Row, e.Col].ToString();
				}
				if (gridMrsBase1.Cols[e.Col].Name == "WRate")
				{
					dbMrsBase.ps_wRate = gridMrsBase1[e.Row, e.Col].ToString();
				}
				if (gridMrsBase1.Cols[e.Col].Name == "extendCode")
				{
					dbMrsBase.ps_extendCode = gridMrsBase1[e.Row, e.Col].ToString();
				}
				if (gridMrsBase1.Cols[e.Col].Name == "PwrSet" && projMrsA != null)
				{
					int pwrSet = ((gridMrsBase1[e.Row, e.Col] == null) ? PwrSet.GetDefaultCode(dsPwrSet) : PwrSet.GetCode(dsPwrSet, ArchConvert.Obj2String(gridMrsBase1[e.Row, e.Col])));
					bool updateItemA = !SysConfig.SysEnablePwrSetSync;
					ExecResult ER = projMrsA.SetPwrSet(projectCode, ArchConvert.Obj2Int(gridMrsBase1[e.Row, "PubCode"]), pwrSet, updateItemA);
					if (ER.ReturnCode != 0)
					{
						MessageBox.Show(ER.Message, "發包權限存取錯誤");
					}
					F_IsBudgetFormNeedToReload = true;
				}
				if (gridMrsBase1.Cols[e.Col].Name == "Account")
				{
					if (gridMrsBase1[e.Row, e.Col] == null)
					{
						dbMrsBase.ps_account = "";
					}
					else
					{
						dbMrsBase.ps_account = gridMrsBase1[e.Row, e.Col].ToString();
					}
				}
				dbMrsBase.UpdItem();
				dbMrsBase = null;
				RowChangeCol = e.Col;
			}
			SetPopupMenuEnable();
		}
		catch (Exception ex)
		{
			MessageBox.Show("gridMrsBase1_AfterEdit Error:" + ex.Message);
		}
	}

	private void gridMrsBase1_MouseMove(object sender, MouseEventArgs e)
	{
		try
		{
			int RowIndex = gridMrsBase1.MouseRow;
			int ColIndex = gridMrsBase1.MouseCol;
			if (RowIndex > 0 && ColIndex > 0)
			{
				C1.Win.C1FlexGrid.Row GridRow = gridMrsBase1.Rows[RowIndex];
				string ColumnName = gridMrsBase1.Cols[ColIndex].Name;
				Cursor = Cursors.Default;
				if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right && ColumnName == "AnaImg" && GridRow["Analysis"] != null && ArchConvert.Obj2Bool(GridRow["Analysis"]))
				{
					Cursor = Cursors.Hand;
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("gridMrsBase1_MouseMove Error:" + ex.Message);
		}
	}

	private void gridMrsBase1_MouseDown(object sender, MouseEventArgs e)
	{
		if (e.Button != MouseButtons.Left)
		{
			return;
		}
		try
		{
			int RowIndex = gridMrsBase1.MouseRow;
			int ColIndex = gridMrsBase1.MouseCol;
			if (RowIndex > 0 && ColIndex > 0)
			{
				C1.Win.C1FlexGrid.Row GridRow = gridMrsBase1.Rows[RowIndex];
				string ColumnName = gridMrsBase1.Cols[ColIndex].Name;
				if (FORM_STATUS != FormStatus.Edit && (!gridMrsBase1.Cols[ColIndex].AllowEditing || ArchConvert.Obj2Bool(GridRow["Analysis"])))
				{
					gridMrsBase1.Col = 0;
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("gridMrsBase1_MouseDown Error:" + ex.Message);
		}
	}

	private void gridMrsBase1_Click(object sender, EventArgs e)
	{
		try
		{
			IsKeyScroll = false;
			int RowIndex = gridMrsBase1.MouseRow;
			int ColIndex = gridMrsBase1.MouseCol;
			if (RowIndex <= 0 || ColIndex <= 0)
			{
				return;
			}
			C1.Win.C1FlexGrid.Row gridRow = gridMrsBase1.Rows[RowIndex];
			if (Cursor == Cursors.Hand && (bool)gridRow["Analysis"])
			{
				ExecuteBreakdownForm(gridMrsBase1);
			}
			if (gridMrsBase1.Cols[gridMrsBase1.MouseCol].Name == "PccesCode")
			{
				string PccesCode = string.Empty;
				if (gridRow["PccesCode"] != null)
				{
					PccesCode = gridRow["PccesCode"].ToString().Trim();
				}
				AddOnDownLoad addOnDownLoad = new AddOnDownLoad();
				addOnDownLoad.OpenDocument(PccesCode, F_UserID, projectCode);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("gridMrsBase1_Click Error:" + ex.Message);
		}
	}

	private void ultraToolbarsManager1_ToolKeyPress(object sender, ToolKeyPressEventArgs e)
	{
		try
		{
			if (e.KeyChar == '\r' && e.Tool.Key == "mnu_Cbo1")
			{
				Do_ToolBarFind();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("ultraToolbarsManager1_ToolKeyPress Error:" + ex.Message);
		}
	}

	private void gridMrsBase1_DoubleClick(object sender, EventArgs e)
	{
		try
		{
			IsKeyScroll = false;
			if (!F_IsSBID)
			{
				Execute_WorkItemEdit();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("gridMrsBase1_DoubleClick Error:" + ex.Message);
		}
	}

	private void ultraToolbarsManager1_BeforeToolbarListDropdown(object sender, BeforeToolbarListDropdownEventArgs e)
	{
		try
		{
			e.Cancel = true;
		}
		catch (Exception ex)
		{
			MessageBox.Show("ultraToolbarsManager1_BeforeToolbarListDropdown Error:" + ex.Message);
		}
	}

	private void ultraButton2_Click(object sender, EventArgs e)
	{
		try
		{
			pnlParent.Height = 0;
			splitter1.Enabled = false;
		}
		catch (Exception ex)
		{
			MessageBox.Show("ultraButton2_Click Error:" + ex.Message);
		}
	}

	private void GotoSpecificRow()
	{
		try
		{
			int iFind = -1;
			iFind = gridMrsBase1.FindRow(gridMrsBase2[gridMrsBase2.Row, "PubCode"].ToString(), 1, gridMrsBase1.Cols["PubCode"].SafeIndex, caseSensitive: false, fullMatch: true, wrap: true);
			if (iFind > -1)
			{
				gridMrsBase1.Row = iFind;
				gridMrsBase1.Select();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("GotoSpecificRow Error:" + ex.Message);
		}
	}

	private void SetGrid2Column()
	{
		try
		{
			for (int i = 0; i < Grid2Cols; i++)
			{
				gridMrsBase2.Cols[i].Name = (string)Grid2ColsSquence[i, 0];
				gridMrsBase2.Cols[i].Caption = (string)Grid2ColsSquence[i, 1];
				gridMrsBase2.Cols[i].Width = (int)Grid2ColsSquence[i, 2];
				gridMrsBase2.Cols[i].DataType = (Type)Grid2ColsSquence[i, 3];
				gridMrsBase2.Cols[i].Visible = (bool)Grid2ColsSquence[i, 4];
				gridMrsBase2.Cols[i].Format = (string)Grid2ColsSquence[i, 5];
				gridMrsBase2.Cols[i].AllowEditing = (bool)Grid2ColsSquence[i, 6];
				gridMrsBase2.Cols[i].TextAlign = (TextAlignEnum)Grid2ColsSquence[i, 7];
				gridMrsBase2.Cols[i].AllowDragging = (bool)Grid2ColsSquence[i, 8];
				gridMrsBase2.Cols[i].AllowResizing = (bool)Grid2ColsSquence[i, 9];
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("SetGrid2Column Error:" + ex.Message);
		}
	}

	private void gridMrsBase1_KeyDown(object sender, KeyEventArgs e)
	{
		try
		{
			if (e.Control)
			{
				IsPressCtrl = true;
			}
			if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
			{
				IsKeyScroll = true;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("gridMrsBase1_KeyDown Error:" + ex.Message);
		}
	}

	private void gridMrsBase1_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			if (e.Control)
			{
				IsPressCtrl = false;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("gridMrsBase1_KeyUp Error:" + ex.Message);
		}
	}

	private void gridMrsBase1_BeforeEdit(object sender, RowColEventArgs e)
	{
		try
		{
			if (IsPressCtrl)
			{
				e.Cancel = true;
			}
			if (!IsCanEdit)
			{
				e.Cancel = true;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("gridMrsBase1_BeforeEdit Error:" + ex.Message);
		}
	}

	private void DoImport(ImportType e)
	{
		try
		{
			FormMrsBase_ImpWizard FM_MRS_IMP = new FormMrsBase_ImpWizard();
			FM_MRS_IMP._ImportType = e;
			FM_MRS_IMP._UserID = F_UserID;
			FM_MRS_IMP._ActionName = FormActionName;
			FM_MRS_IMP._dsPwrSet = dsPwrSet;
			FM_MRS_IMP._ProjectCode = projectCode;
			if (FM_MRS_IMP.ShowDialog(this) == DialogResult.OK)
			{
				BindToGrid("");
				F_IsBudgetFormNeedToReload = true;
			}
			FM_MRS_IMP.Close();
			FM_MRS_IMP.Dispose();
			FM_MRS_IMP = null;
		}
		catch (Exception ex)
		{
			MessageBox.Show("DoImport Error:" + ex.Message);
		}
	}

	private void DoImport()
	{
		try
		{
			FormMrsCodeChange_ImpWizard FM_MRS_IMP = new FormMrsCodeChange_ImpWizard();
			FM_MRS_IMP._UserID = F_UserID;
			FM_MRS_IMP._ActionName = FormActionName;
			FM_MRS_IMP._ProjectCode = projectCode;
			if (FM_MRS_IMP.ShowDialog(this) == DialogResult.OK)
			{
				BindToGrid("");
				IsReload = true;
			}
			FM_MRS_IMP.Close();
			FM_MRS_IMP.Dispose();
			FM_MRS_IMP = null;
		}
		catch (Exception ex)
		{
			MessageBox.Show("DoImport Error:" + ex.Message);
		}
	}

	private void DoExport(ExportType e)
	{
		try
		{
			DataTable DT_Exp = new DataTable();
			if (e == ExportType.Excel)
			{
				ArrayList aArr = new ArrayList();
				aArr.Clear();
				aArr.Add(F_UserID);
				aArr.Add("匯出專案基本工料");
				Recost Recost1 = new Recost(aArr);
				Recost1.ps_prjcode = projectCode;
				Recost1.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
				Archnowledge.Pcces.BUDClass.MrsBaseA dbMrsBase = new Archnowledge.Pcces.BUDClass.MrsBaseA(F_UserID, aArr);
				dbMrsBase.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
				dbMrsBase.ps_projectcode = projectCode;
				DT_Exp = dbMrsBase.ListItem();
				DT_Exp.Columns.Add("chk", Type.GetType("System.String"));
				DataColumn[] Keys = new DataColumn[1];
				DataColumn myColumn = DT_Exp.Columns["pubCode"];
				Keys[0] = myColumn;
				DT_Exp.PrimaryKey = Keys;
				int iSels = gridMrsBase1.SelectedItems;
				int iDone = 0;
				for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
				{
					if (iDone >= iSels)
					{
						break;
					}
					if (gridMrsBase1.Rows[i].Selected && gridMrsBase1.Rows[i].Visible)
					{
						DataRow DR_Find = DT_Exp.Rows.Find((int)gridMrsBase1[i, "PubCode"]);
						if (DR_Find != null)
						{
							DR_Find["chk"] = "1";
							iDone++;
						}
					}
				}
				if (iDone <= 0)
				{
					MessageBox.Show(this, "尚未選取匯出範圍，請先選取後再執行匯出!!", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
			}
			FormMrsBase_ExpWizard FM_MRS_EXP = new FormMrsBase_ExpWizard();
			FM_MRS_EXP._UserID = F_UserID;
			FM_MRS_EXP._ExportType = e;
			FM_MRS_EXP._dsPwrSet = dsPwrSet;
			FM_MRS_EXP._DT_ExpDatas = DT_Exp;
			FM_MRS_EXP._ActionName = FormActionName;
			FM_MRS_EXP._ProjectCode = projectCode;
			FM_MRS_EXP.ShowDialog(this);
		}
		catch (Exception ex)
		{
			MessageBox.Show("DoExport Error:" + ex.Message);
		}
	}

	private void ultraToolbarsManager1_ToolValueChanged(object sender, ToolEventArgs e)
	{
		try
		{
			if (!(e.Tool.Key == "mnu_Cbo1"))
			{
				return;
			}
			string sSearchText = ((TextBoxTool)ultraToolbarsManager1.Tools["mnu_Cbo1"]).Text.Trim();
			bool IsSearchName = false;
			for (int ii = 0; ii < sSearchText.Length; ii++)
			{
				if (sSearchText[ii] > '\u007f')
				{
					IsSearchName = true;
					break;
				}
			}
			int iFind = -1;
			int iStart = 1;
			int iColLookup = (IsSearchName ? gridMrsBase1.Cols["CName"].SafeIndex : gridMrsBase1.Cols["PccesCode"].SafeIndex);
			iFind = gridMrsBase1.FindRow(sSearchText.ToString(), iStart, iColLookup, caseSensitive: false, fullMatch: false, wrap: false);
			if (iFind > -1)
			{
				gridMrsBase1.Row = iFind;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("ultraToolbarsManager1_ToolValueChanged Error:" + ex.Message);
		}
	}

	private void ultraButton3_Click(object sender, EventArgs e)
	{
		try
		{
			base.DialogResult = (IsReload ? DialogResult.OK : DialogResult.Cancel);
		}
		catch (Exception ex)
		{
			MessageBox.Show("ultraButton3_Click Error:" + ex.Message);
		}
	}

	private void ExecuteChangeCode()
	{
		try
		{
			C1.Win.C1FlexGrid.Row gridRow = gridMrsBase1.Rows[gridMrsBase1.Row];
			if (gridMrsBase1.Row <= 0 || NotAllowEditingInCostEst(gridRow["PccesCode"].ToString()) || budgetType == 6)
			{
				return;
			}
			object Lock = gridRow["Lock"];
			if (Lock != null && Lock != DBNull.Value && Convert.ToBoolean(Lock))
			{
				MessageBox.Show("此工項已存在前一版預算書，所以不可以換碼", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			int pubCode = ArchConvert.Obj2Int(gridRow["pubCode"]);
			FormMrsBaseChgCode FMCHGCOD = new FormMrsBaseChgCode();
			FMCHGCOD._UserID = F_UserID;
			FMCHGCOD._PccesCode = gridRow["PccesCode"].ToString();
			FMCHGCOD._PubCode = (int)gridRow["PubCode"];
			FMCHGCOD._CName = gridRow["CName"].ToString();
			FMCHGCOD._ActionName = FormActionName;
			FMCHGCOD._ProjectCode = projectCode;
			FMCHGCOD.Owner = this;
			if (FMCHGCOD.ShowDialog() == DialogResult.OK)
			{
				(base.Owner as frmBudget)._IsNeedToReloadAllData = true;
				BindToGrid("");
				SetGridFocusByPubCode(pubCode);
				checkDBReSet();
			}
			FMCHGCOD.Close();
			FMCHGCOD.Dispose();
			FMCHGCOD = null;
		}
		catch (Exception ex)
		{
			MessageBox.Show("ExecuteChangeCode Error:" + ex.Message);
		}
	}

	private void SetGridFocusByPubCode(int pubCode)
	{
		for (int index = 1; index < gridMrsBase1.Rows.Count + 1; index++)
		{
			if (ArchConvert.Obj2Int(gridMrsBase1.Rows[index]["pubCode"]) == pubCode)
			{
				gridMrsBase1.AfterSelChange -= gridMrsBase1_AfterSelChange;
				gridMrsBase1.Row = index;
				gridMrsBase1.AfterSelChange += gridMrsBase1_AfterSelChange;
				break;
			}
		}
	}

	private void gridMrsBase2_MouseMove(object sender, MouseEventArgs e)
	{
		try
		{
			if (GRID2_STATUS == FormStatus.Edit || gridMrsBase2.MouseRow <= 0 || gridMrsBase2.MouseCol <= 0)
			{
				return;
			}
			int rowIndex = gridMrsBase2.MouseRow;
			int colIndex = gridMrsBase2.MouseCol;
			if (gridMrsBase2[rowIndex, "Analysis"] == null)
			{
				return;
			}
			if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right && gridMrsBase2.Cols[colIndex].Name == "AnaImg")
			{
				if (rowIndex > 0 && (bool)gridMrsBase2[rowIndex, "Analysis"])
				{
					Cursor = Cursors.Hand;
				}
			}
			else
			{
				Cursor = Cursors.Default;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("gridMrsBase2_MouseMove Error:" + ex.Message);
		}
	}

	private void gridMrsBase2_Click(object sender, EventArgs e)
	{
		try
		{
			if (gridMrsBase2.MouseRow > 0 && gridMrsBase2.MouseCol > 0)
			{
				int rowIndex = gridMrsBase2.MouseRow;
				if (Cursor == Cursors.Hand && (bool)gridMrsBase2[rowIndex, "Analysis"])
				{
					ExecuteBreakdownForm(gridMrsBase2);
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("gridMrsBase2_Click Error:" + ex.Message);
		}
	}

	private void ultraButton9_Click(object sender, EventArgs e)
	{
		try
		{
			if (SysConfig.SysChangeManagement)
			{
				int pubCode = ArchConvert.Obj2Int(gridMrsBase1[gridMrsBase1.Row, "PubCode"]);
				string PubPccesCode = ArchConvert.Obj2String(gridMrsBase1[gridMrsBase1.Row, "PccesCode"]);
				string PubCName = ArchConvert.Obj2String(gridMrsBase1[gridMrsBase1.Row, "CName"]);
				string PubUnitName = ArchConvert.Obj2String(gridMrsBase1[gridMrsBase1.Row, "unitName"]);
				DataSet dsParentProjMrsA = projMrsA.GetParentProjMrsAByPubCode(projectCode, pubCode);
				BudItemADBHelper theBudItemADBHelper = new BudItemADBHelper();
				DataSet dsBudItemAByPccesCode = theBudItemADBHelper.GetItemAByPccesCode(projectCode, PubPccesCode);
				DataTable parent = new DataTable();
				parent.Columns.Add("PrintNo", typeof(string));
				parent.Columns.Add("ItemNo", typeof(string));
				parent.Columns.Add("PccesCode", typeof(string));
				parent.Columns.Add("Sno", typeof(string));
				DataTable Myself = new DataTable();
				Myself.Columns.Add("PrintNo", typeof(string));
				Myself.Columns.Add("ItemNo", typeof(string));
				Myself.Columns.Add("PccesCode", typeof(string));
				Myself.Columns.Add("Sno", typeof(string));
				for (int j = 0; j < dsParentProjMrsA.Tables[0].Rows.Count; j++)
				{
					for (int i = 0; i < ParentItemA.Tables[0].Rows.Count; i++)
					{
						if (dsParentProjMrsA.Tables[0].Rows[j][3].ToString().Trim() == ParentItemA.Tables[0].Rows[i][4].ToString().Trim())
						{
							parent.Rows.Add(ParentItemA.Tables[0].Rows[i]["PrintNo"], "", ParentItemA.Tables[0].Rows[i]["PccesCode"], ParentItemA.Tables[0].Rows[i]["sNo"]);
							i = ParentItemA.Tables[0].Rows.Count;
						}
					}
				}
				for (int j = 0; j < dsParentProjMrsA.Tables[0].Rows.Count; j++)
				{
					if (parent == null || parent.Rows.Count <= 0)
					{
						continue;
					}
					while (parent.Rows[j][0].ToString().Length > 4)
					{
						for (int i = 0; i < ParentItemA.Tables[0].Rows.Count; i++)
						{
							if (parent.Rows[j][0].ToString() == ParentItemA.Tables[0].Rows[i][2].ToString().Trim())
							{
								parent.Rows[j][1] = ParentItemA.Tables[0].Rows[i][3].ToString().Trim() + "." + parent.Rows[j][1].ToString().Trim();
								parent.Rows[j][0] = parent.Rows[j][0].ToString().Substring(0, parent.Rows[j][0].ToString().Length - 4);
								i = ParentItemA.Tables[0].Rows.Count;
							}
						}
					}
				}
				for (int j = 0; j < dsBudItemAByPccesCode.Tables[0].Rows.Count; j++)
				{
					Myself.Rows.Add(dsBudItemAByPccesCode.Tables[0].Rows[j]["PrintNo"], "", dsBudItemAByPccesCode.Tables[0].Rows[j]["PccesCode"], dsBudItemAByPccesCode.Tables[0].Rows[j]["sNo"]);
				}
				for (int j = 0; j < dsBudItemAByPccesCode.Tables[0].Rows.Count; j++)
				{
					if (Myself == null || Myself.Rows.Count <= 0)
					{
						continue;
					}
					while (Myself.Rows[j][0].ToString().Length > 4)
					{
						for (int i = 0; i < ParentItemA.Tables[0].Rows.Count; i++)
						{
							if (Myself.Rows[j][0].ToString() == ParentItemA.Tables[0].Rows[i][2].ToString().Trim())
							{
								Myself.Rows[j][1] = ParentItemA.Tables[0].Rows[i][3].ToString().Trim() + "." + Myself.Rows[j][1].ToString().Trim();
								Myself.Rows[j][0] = Myself.Rows[j][0].ToString().Substring(0, Myself.Rows[j][0].ToString().Length - 4);
								i = ParentItemA.Tables[0].Rows.Count;
							}
						}
					}
				}
				saveFileDialog1.Filter = "Microsoft Excel (*.xls)|*.xls";
				saveFileDialog1.RestoreDirectory = true;
				saveFileDialog1.FileName = "父項查詢結果.xls";
				if (saveFileDialog1.ShowDialog() == DialogResult.OK)
				{
					BudgetResParentReport report = new BudgetResParentReport();
					ExecResult ER = report.ProduceExecutiveBudgetResParentReport(saveFileDialog1.FileName, dsParentProjMrsA, lblProjectData, PubPccesCode, PubCName, PubUnitName, parent, Myself, dsBudItemAByPccesCode);
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
			else
			{
				string sFilter = "Microsoft Excel 97/2000 files (*.xls)|*.xls";
				saveFileDialog1.Filter = sFilter;
				saveFileDialog1.RestoreDirectory = true;
				saveFileDialog1.FileName = "父項查詢結果";
				if (saveFileDialog1.ShowDialog() == DialogResult.OK)
				{
					gridMrsBase2._ExcelFileName = saveFileDialog1.FileName;
					gridMrsBase2._ExcelSheeName = "父項查詢結果";
					gridMrsBase2._IsOpenExcelAfterExport = true;
					gridMrsBase2.ExecuteExport(c1GridExportType.Excel);
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("ultraButton9_Click Error:" + ex.Message);
		}
	}

	private void BtnReCalSmall_Click(object sender, EventArgs e)
	{
		try
		{
			if (MessageBox.Show(this, "確定要執行父項項目的重新小計?", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				FormSys_G_Info1 FM_INFO = new FormSys_G_Info1();
				FM_INFO._InfoString = "重新小計中，請稍候! ";
				FM_INFO.Owner = this;
				FM_INFO._MinValue = 0;
				FM_INFO._MaxValue = gridMrsBase2.Rows.Count - 1;
				FM_INFO.Show();
				FM_INFO.BringToFront();
				Application.DoEvents();
				Cursor = Cursors.WaitCursor;
				ArrayList aArr = new ArrayList();
				aArr.Add(F_UserID);
				aArr.Add(CommonMethods.GetFormTypeTitle(FormType.MrsBaseAnalysis));
				Recost RC1 = new Recost(aArr);
				RC1.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
				RC1.ps_prjcode = projectCode;
				for (int i = 1; i < gridMrsBase2.Rows.Count; i++)
				{
					RC1.ps_pubcode = gridMrsBase2[i, "PubCode"].ToString();
					RC1.ReCalc2(1, 0m);
					FM_INFO._ProgressValue++;
				}
				Cursor = Cursors.Default;
				FM_INFO.Close();
				FM_INFO.Dispose();
				Application.DoEvents();
				Find_Parent();
				if (ExtraSearchCriteria != string.Empty)
				{
					RunExtraSearchCriteria = true;
				}
				int iPos = gridMrsBase1.Row;
				BindToGrid("");
				RunExtraSearchCriteria = false;
				gridMrsBase1.Row = iPos;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("BtnReCalSmall_Click Error:" + ex.Message);
		}
	}

	private void btnAddBookList_Click(object sender, EventArgs e)
	{
		try
		{
			int pubCode = ArchConvert.Obj2Int(gridMrsBase1[gridMrsBase1.Row, "PubCode"]);
			DataSet dsParentProjMrsA = projMrsA.GetParentProjMrsAByPubCode(projectCode, pubCode);
			DataView dvParentProjMrsA = new DataView(dsParentProjMrsA.Tables[0]);
			DataSet dsTargetProjMrsA = projMrsA.GetParentProjMrsAByPubCode(projectCode, pubCode);
			dsTargetProjMrsA.Tables[0].Rows.Clear();
			for (int i = 0; i < dvParentProjMrsA.Count; i++)
			{
				DataRow newRow = dsTargetProjMrsA.Tables[0].NewRow();
				DataRow sourceRow = null;
				sourceRow = dsParentProjMrsA.Tables[0].Rows[i];
				newRow.ItemArray = sourceRow.ItemArray;
				int SpubCode = ArchConvert.Obj2Int(dvParentProjMrsA[i]["parentCode"]);
				dsTargetProjMrsA = GetTopProjMrsA(SpubCode, dsTargetProjMrsA, newRow);
			}
			frmBudget formBudget = new frmBudget();
			_dsParentProjMrsA = dsTargetProjMrsA;
			_AddParentBookList = true;
			MessageBox.Show("已加至書籤");
		}
		catch (Exception ex)
		{
			MessageBox.Show("btnAddBookList_Click Error:" + ex.Message);
		}
	}

	private DataSet GetTopProjMrsA(int pubCode, DataSet dsTargetProjMrsA, DataRow newRow)
	{
		try
		{
			DataSet dsParentProjMrsA = projMrsA.GetParentProjMrsAByPubCode(projectCode, pubCode);
			DataView dvParentProjMrsA = new DataView(dsParentProjMrsA.Tables[0]);
			if (dsParentProjMrsA.Tables[0].Rows.Count == 0)
			{
				newRow["itemDuty"] = ArchConvert.Obj2Double(newRow["itemDuty"]) * Num;
				newRow["qtySubtotal"] = ArchConvert.Obj2Double(newRow["qtySubtotal"]) * Num;
				Num = 1.0;
				dsTargetProjMrsA.Tables[0].Rows.Add(newRow);
			}
			else
			{
				for (int i = 0; i < dvParentProjMrsA.Count; i++)
				{
					DataRow NewRow = dsTargetProjMrsA.Tables[0].NewRow();
					DataRow sourceRow = null;
					sourceRow = dsParentProjMrsA.Tables[0].Rows[i];
					NewRow.ItemArray = sourceRow.ItemArray;
					int SpubCode = ArchConvert.Obj2Int(dvParentProjMrsA[i]["parentCode"]);
					if (FindParentFromBudget)
					{
						Num *= ArchConvert.Obj2Double(newRow["itemDuty"]);
					}
					dsTargetProjMrsA = GetTopProjMrsA(SpubCode, dsTargetProjMrsA, NewRow);
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("GetTopProjMrsA Error:" + ex.Message);
		}
		return dsTargetProjMrsA;
	}

	private bool CheckProjMrsA()
	{
		bool rtnStr = false;
		try
		{
			string sKind = CommonMethods.GetActionNameString(FormActionName);
			string sSQL = "select * from " + sKind + "ProjMrsA where projectCode = '" + projectCode + "' and substring(pccescode,1,1) = '#'";
			ArrayList aArr = new ArrayList();
			aArr.Add(F_UserID);
			aArr.Add("取pccescode的值");
			ModifyDB ModDB = new ModifyDB(projectCode, aArr);
			DataTable DT = new DataTable();
			DT = ModDB.DBList(sSQL);
			if (DT.Rows.Count > 0)
			{
				rtnStr = true;
			}
			ModDB = null;
			aArr = null;
		}
		catch (Exception ex)
		{
			MessageBox.Show("CheckProjMrsA Error:" + ex.Message);
		}
		return rtnStr;
	}

	private bool updateCorrectConfirm()
	{
		if (FormActionName == PccesFormAction.BID)
		{
			MessageBox.Show("標單不支援正確率計算！");
			return false;
		}
		gridMrsBase1.Redraw = false;
		string strUnit = "";
		string strName = "";
		string strNameAlt = "";
		string strChapName = "";
		string strCompareErrState = "";
		string strChapCodeCorrect = "";
		int iMemoItemCount = 0;
		decimal dAmt = 0m;
		decimal iCorrect = 0m;
		decimal dWeightCorrectRatio = 0m;
		decimal dCorrectTotal = 0m;
		decimal dTotal = 0m;
		decimal iFit = 0m;
		decimal dWeightFitRatio = 0m;
		decimal dFitTotal = 0m;
		DBClass dbC = new DBClass();
		dtAutoNumB = dbC.GetAutoNumB();
		dtAutoNumA = dbC.GetAutoNumA();
		cCV = new CodeValidator(dtAutoNumA, dtAutoNumB);
		cCV._UserID = F_UserID;
		cCV._ProjectCode = projectCode;
		cCF = new CodeFitter();
		dWeightCorrectRatio = 0m;
		dWeightCorrectRatio = 0m;
		dCorrectTotal = 0m;
		dFitTotal = 0m;
		dTotal = 0m;
		iCorrect = 0m;
		iFit = 0m;
		DataView dvResource = dtResource.DefaultView;
		string sItemClass = "";
		int i = 0;
		FormProgress FM = new FormProgress();
		FM._Max = dvResource.Count;
		FM._Min = 0;
		FM.Message = "正在計算...";
		FM.TopMost = true;
		FM.Show();
		Cursor = Cursors.WaitCursor;
		dvResource.Sort = "pccesCode Asc";
		for (; i < dvResource.Count; i++)
		{
			if (i % 100 == 0)
			{
				StatusBar.Panels[0].Text = "(正在計算第 " + i + " 筆 / 共 " + dvResource.Count + " 筆)";
				FM.SetMessage(StatusBar.Panels[0].Text);
				FM.SetProgressValue(i);
				Application.DoEvents();
				Cursor = Cursors.WaitCursor;
			}
			string PccesCode = ArchConvert.Obj2String(dvResource[i]["pccesCode"]).Trim();
			if (PccesCode.Length > 0)
			{
				sItemClass = PccesCode.Substring(0, 1);
			}
			C1.Win.C1FlexGrid.Row theRow = gridMrsBase1.Rows[i + 1];
			if (dvResource[i]["costKind"].ToString() == "#" || dvResource[i]["costKind"].ToString().ToUpper() == "Z")
			{
				iMemoItemCount++;
				dvResource[i]["correct"] = "";
				theRow["Correct"] = "";
				dvResource[i]["CompareErrState"] = "";
				theRow["CompareErrState"] = "";
				continue;
			}
			strName = "";
			strNameAlt = "";
			strChapName = "";
			strUnit = "";
			strCompareErrState = "";
			strChapCodeCorrect = "";
			bool bRet = cCV.ValidateCode(PccesCode, out strName, out strUnit, out strCompareErrState, out strChapCodeCorrect, out strNameAlt, out strChapName);
			if (bRet)
			{
				bool IsUnitGood = strUnit.Trim() == dvResource[i]["unitName"].ToString().Trim();
				if (!IsUnitGood)
				{
					IsUnitGood = CheckUnitGoodAdvanced(strUnit.Trim(), dvResource[i]["unitName"].ToString().Trim());
				}
				bool IsNameGood = dvResource[i]["cName"].ToString().Trim().IndexOf(strName.Trim(), 0) == 0;
				bool IsStarNameGood = true;
				if (strNameAlt.Trim() != "")
				{
					IsStarNameGood = strNameAlt.Trim() != "" && dvResource[i]["cName"].ToString().Trim().IndexOf(strNameAlt.Trim(), 0) == 0;
				}
				bRet = ((IsUnitGood && (IsNameGood || IsStarNameGood)) ? true : false);
			}
			if (bRet)
			{
				dvResource[i]["correct"] = '是';
			}
			else
			{
				dvResource[i]["correct"] = '否';
			}
			theRow["Correct"] = (bRet ? "是" : "否");
			if (strName.Trim() == "")
			{
				if (strCompareErrState.IndexOf("細目碼錯誤") <= -1)
				{
				}
			}
			else if (theRow["CName"].ToString().Trim().IndexOf(strName.Trim(), 0) < 0)
			{
				if ((sItemClass == "E" || sItemClass == "L") && strCompareErrState.Trim() != "")
				{
					strCompareErrState += ((strCompareErrState.Trim() == "") ? "工項名稱錯誤" : "，工項名稱錯誤");
					if (strNameAlt.Trim() == "")
					{
						dvResource[i]["correct"] = '否';
						theRow["Correct"] = "否";
					}
				}
				else
				{
					dvResource[i]["CorrectCName"] = strName;
					theRow["CorrectCName"] = strName;
					strCompareErrState += ((strCompareErrState.Trim() == "") ? "工項名稱錯誤" : "，工項名稱錯誤");
					if (strNameAlt.Trim() == "")
					{
						dvResource[i]["correct"] = '否';
						theRow["Correct"] = "否";
					}
				}
			}
			else
			{
				dvResource[i]["CorrectCName"] = "";
				theRow["CorrectCName"] = "";
			}
			if (strUnit.Trim().Length > 4)
			{
				strUnit = strUnit.Substring(0, 4);
			}
			if (PccesCode.Substring(PccesCode.Length - 1) == "0" && strUnit.Trim() == "")
			{
				strCompareErrState += ((strCompareErrState.Trim() == "") ? "單位碼不應為0" : "，單位碼不應為0");
				dvResource[i]["correct"] = '否';
				theRow["Correct"] = "否";
			}
			else if (strUnit.Trim() == "")
			{
				if (strCompareErrState.IndexOf("細目碼錯誤") <= -1)
				{
					if (strCompareErrState.IndexOf("，工項名稱錯誤") > -1)
					{
						strCompareErrState = strCompareErrState.Replace("，工項名稱錯誤", "");
					}
					else if (strCompareErrState.IndexOf("工項名稱錯誤") > -1)
					{
						strCompareErrState = strCompareErrState.Replace("工項名稱錯誤", "");
					}
					if (strCompareErrState.IndexOf("綱要編碼錯誤") < 0)
					{
						strCompareErrState += ((strCompareErrState.Trim() == "") ? "細目碼錯誤" : "，細目碼錯誤");
					}
				}
			}
			else if (dvResource[i]["unitName"].ToString().Trim() != strUnit.Trim())
			{
				if (!CheckUnitGoodAdvanced(strUnit.Trim(), dvResource[i]["unitName"].ToString().Trim()))
				{
					dvResource[i]["CorrectUnitName"] = strUnit;
					theRow["CorrectUnitName"] = strUnit;
					strCompareErrState += ((strCompareErrState.Trim() == "") ? "單位錯誤" : "，單位錯誤");
				}
				else
				{
					dvResource[i]["CorrectUnitName"] = "";
					theRow["CorrectUnitName"] = "";
				}
			}
			else
			{
				dvResource[i]["CorrectUnitName"] = "";
				theRow["CorrectUnitName"] = "";
			}
			strCompareErrState = ReAssignErrorState(strCompareErrState);
			bool flag = true;
			dvResource[i]["CompareErrState"] = strCompareErrState;
			theRow["CompareErrState"] = strCompareErrState;
			if (strCompareErrState.Trim() != "")
			{
				dvResource[i]["correct"] = '否';
				theRow["Correct"] = "否";
				bRet = false;
			}
			dAmt = (string.IsNullOrEmpty(dvResource[i]["usrAmt"].ToString()) ? 0m : ArchConvert.Obj2Decimal(dvResource[i]["usrAmt"]));
			dTotal += ((dAmt > 0m) ? dAmt : 0m);
			dCorrectTotal += ((bRet && dAmt > 0m) ? dAmt : 0m);
			iCorrect += (decimal)(bRet ? 1 : 0);
			if (strChapCodeCorrect == "")
			{
				dvResource[i]["confirm"] = "是";
				theRow["Confirm"] = "是";
				dFitTotal += dAmt;
				++iFit;
			}
			else
			{
				dvResource[i]["confirm"] = strChapCodeCorrect;
				theRow["Confirm"] = strChapCodeCorrect;
			}
		}
		StatusBar.Panels[0].Text = "(正在儲存資料庫...)";
		FM.SetMessage(StatusBar.Panels[0].Text);
		FM.SetProgressValue(dvResource.Count);
		Application.DoEvents();
		Cursor = Cursors.WaitCursor;
		BudProjMrsA budProjMrsA = new BudProjMrsA();
		if (FormActionName == PccesFormAction.BID)
		{
			StatusBar.Panels[0].Text = " 資料筆數:" + dvResource.Count;
			return true;
		}
		budProjMrsA.GetDatasetUpdateCorrectConfirm(dsResource);
		dWeightCorrectRatio = ((dTotal > 0m) ? (dCorrectTotal / dTotal * 100m) : 0m);
		dWeightFitRatio = 0m;
		BudProject bp = new BudProject();
		decimal correctRate = ((dvResource.Count > 0) ? (iCorrect / (decimal)(dvResource.Count - iMemoItemCount) * 100m) : 0m);
		decimal confirmRate = ((dvResource.Count > 0) ? (iFit / (decimal)(dvResource.Count - iMemoItemCount) * 100m) : 0m);
		bp.UpdateRates(projectCode, correctRate, dWeightCorrectRatio, confirmRate, dWeightFitRatio);
		StatusBar.Panels[0].Text = " 資料筆數:" + dvResource.Count + ", 編碼正確率:" + $"{correctRate:N2}" + "%, 加權正確率:" + $"{dWeightCorrectRatio:N2}" + "%, 綱要編碼正確率:" + $"{confirmRate:N2}" + "%";
		FM.Hide();
		FM.Dispose();
		Cursor = Cursors.Default;
		gridMrsBase1.Redraw = true;
		return true;
	}

	private bool CheckUnitGoodAdvanced(string BaseUnit, string WaitToCheckUnit)
	{
		bool retV = false;
		int iItems = cCV.AlternativeUnit.GetLength(0);
		int iCheckItems = -1;
		for (int i = 0; i < iItems; i++)
		{
			if (!(cCV.AlternativeUnit[i, 0] == BaseUnit))
			{
				continue;
			}
			for (int j = 1; j < 6; j++)
			{
				if (cCV.AlternativeUnit[i, j] == WaitToCheckUnit)
				{
					retV = true;
					break;
				}
			}
		}
		return retV;
	}

	private string ReAssignErrorState(string sInput)
	{
		string retV = sInput;
		if (sInput == "工項名稱錯誤，單位錯誤")
		{
			retV = "工項名稱錯誤";
		}
		else
		{
			switch (sInput)
			{
			case "工項編碼長度不足，細目碼錯誤":
				retV = "細目碼錯誤";
				break;
			case "工項編碼長度不足，資源碼錯誤":
				retV = "細目碼錯誤";
				break;
			case "工項編碼長度不足，綱要編碼錯誤":
				retV = "綱要編碼錯誤";
				break;
			case "工項編碼長度不足，資源碼錯誤，細目碼錯誤":
				retV = "細目碼錯誤";
				break;
			case "綱要編碼錯誤，細目碼錯誤":
				retV = "綱要編碼錯誤";
				break;
			case "資源碼錯誤":
				retV = "細目碼錯誤";
				break;
			case "細目碼錯誤，工項名稱錯誤":
				retV = "細目碼錯誤";
				break;
			case "資源碼錯誤，細目碼錯誤":
				retV = "細目碼錯誤";
				break;
			case "不符編碼規則，細目碼錯誤":
				retV = "不符編碼規則";
				break;
			}
		}
		return retV;
	}

	private void mnuExportGrid2_Click(object sender, EventArgs e)
	{
		string sFullPathFile = Path.Combine(Path.GetTempPath(), DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".xls");
		gridMrsBase2._IsOpenExcelAfterExport = true;
		gridMrsBase2._ExcelFileName = sFullPathFile;
		gridMrsBase2.ExecuteExport(c1GridExportType.Excel);
	}
}
