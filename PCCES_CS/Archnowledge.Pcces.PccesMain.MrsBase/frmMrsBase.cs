using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.CommonClass.MrsBase;
using Archnowledge.Pcces.DomainModule.CostStructure;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.DomainModule.MrsBase;
using Archnowledge.Pcces.PccesMain._Customize.Z14AC1100;
using Archnowledge.Pcces.PccesMain.About;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.PccesMain.Budget;
using Archnowledge.Pcces.PccesMain.Budget.ItemNoset;
using Archnowledge.Pcces.PccesMain.Library;
using Archnowledge.Pcces.PccesMain.MrsBase.Bookmark;
using Archnowledge.Pcces.PccesMain.MrsBase.PickFromOther;
using Archnowledge.Pcces.PccesMain.ShellLib;
using Archnowledge.Pcces.PccesMain.SysMaintain;
using Archnowledge.Pcces.STDClass;
using Aspose.Cells;
using AxThreed;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinStatusBar;
using Infragistics.Win.UltraWinToolbars;
using Infragistics.Win.UltraWinTree;
using PCCES.CODECHECK;

namespace Archnowledge.Pcces.PccesMain.MrsBase;

public class frmMrsBase : Form
{
	private const string FileIni = "OptionSet.ini";

	private const string F_FunctionName = "MrsBase";

	private int iCurrentRowIndex = 0;

	protected Archnowledge.Pcces.DomainModule.CostStructure.CostStructure _CostStructure = new Archnowledge.Pcces.DomainModule.CostStructure.CostStructure();

	private IContainer components;

	public UltraToolbarsManager ultraToolbarsManager1;

	private string Start = "";

	private string FilterFlag = "";

	private int ThreadFlag = 0;

	private string UsedGrid = "DEFAULT";

	private string sGridSort = "ASC";

	private int iTextBeamPos = 0;

	private int iCount = 0;

	private int realpubCode = 0;

	private SortFlags C1Sort = SortFlags.None;

	private int iSortCol = 0;

	private object GridInit = new object();

	private bool __DEBUG = PubTools.Str2Boolean(CommonMethods.GetIniValue("DEBUG", "DEBUG"));

	private ArrayList ToolLists = new ArrayList();

	private ArrayList ToolParam = new ArrayList();

	private string sBindFlag = "";

	private string ExtraCri = "";

	private string F_TreeMenu = "CLOSE";

	private DataTable DT_Nodes = new DataTable();

	private DataTable DT_Leaves = new DataTable();

	private ArrayList DeletionList = new ArrayList();

	private bool IsShift = false;

	private bool IsCtrl = false;

	private bool IsAlt = false;

	private int iAuthorityMSG_Count = 0;

	private string F_NewAddItem_PccesCode = "";

	private string F_NewAddItem_PubCode = "";

	private string AppLocation = "";

	private string F_SettingPick = CommonMethods.ExtractFilePath(Application.ExecutablePath) + "MrsBase.ini";

	private LeftPanelMode PanelMode = LeftPanelMode.Open;

	private bool F_HasRegistered;

	private string F_UserID;

	private string F_UserName = "";

	private string F_ServerName = "localhost";

	private string F_Cstring;

	private ArrayList PickList = new ArrayList();

	private DataSet dsPwrSet = null;

	private DataTable DT_Leaves12 = new DataTable();

	private string F_iCount = "";

	private string F_CostType = "";

	private string F_CostUID = "";

	private DataTable DT_Auto = new DataTable();

	private int F_MainQty = 0;

	private int F_MainCst = 0;

	private int F_MainAmt = 0;

	private int F_AnaQty = 0;

	private int F_AnaCst = 0;

	private int F_AnaAmt = 0;

	private DataTable DTDrag = new DataTable();

	private DataTable DT1 = new DataTable();

	public DataView DV1;

	private Archnowledge.Pcces.BUDClass.MrsBaseA dbMrsBase;

	private ArrayList aArr = new ArrayList();

	private string FORM_STATUS = "INITIAL";

	private int GridCols = 15;

	private int Grid2Cols = 15;

	private object[,] GridColsSquence;

	private object[,] Grid2ColsSquence;

	private PccesFormAction F_ActionName = PccesFormAction.MrsBase;

	private Rectangle dragBoxFromMouseDown = Rectangle.Empty;

	private bool UseCostStructure = PubTools.GetAppSet_Bool("UseCostStructure");

	private CodeValidator cCV;

	private CodeFitter cCF;

	private DataTable dtResource = null;

	private DataTable dtAutoNumA;

	private DataTable dtAutoNumB;

	private FormSys_G_Info1 FM_INFO = null;

	private static bool importing3652 = false;

	private Panel frmMrsBase_Fill_Panel;

	private Panel panel3;

	private ImageList imageList1;

	private Panel LeftPanel;

	private Panel panel1;

	private Panel panel5;

	private UltraStatusBar ultraStatusBar1;

	private ImageList imageList2;

	public GridMrsBase gridMrsBase1;

	public FunctionButtons functionButtons1;

	private OnlineList onlineList1;

	private Panel pnl_spliter;

	private UltraToolbarsDockArea _frmMrsBase_Toolbars_Dock_Area_Top;

	private UltraToolbarsDockArea _frmMrsBase_Toolbars_Dock_Area_Bottom;

	private UltraToolbarsDockArea _frmMrsBase_Toolbars_Dock_Area_Left;

	private UltraToolbarsDockArea _frmMrsBase_Toolbars_Dock_Area_Right;

	private ImageList iglst_splt_Btn;

	private UltraButton Btn_Splt;

	private AxSSPanel ssp_GridCaption;

	private AxSSPanel ssp_Top;

	private AxSSPanel ssp_Upper;

	private AxSSPanel ssp_Lower;

	private AxSSPanel ssp_Bottom;

	private UltraLabel lblUseDatabase;

	private UltraButton ultraButton1;

	private UltraLabel ultraLabel1;

	private Panel panel6;

	private Panel panel7;

	private UltraLabel ultraLabel2;

	private UltraButton ultraButton2;

	public GridMrsBase gridMrsBase2;

	private ImageList imageList3;

	private Panel pnlParent;

	private Control Cntrl1;

	private FormSymbol Frm = new FormSymbol();

	private UltraCombo cboHisPrice;

	private Splitter splitter1;

	private UltraButton ultraButton9;

	private System.Windows.Forms.ToolTip toolTip1;

	private SaveFileDialog saveFileDialog1;

	private UltraButton BidbtnClose;

	private Panel PNL_TREE;

	private Panel panel8;

	private UltraTree ultraTree1;

	private Panel PNL_COST;

	internal UltraTree ultraTree2;

	private Panel panel4;

	private UltraButton ultraButton3;

	private UltraLabel lblCost;

	private Panel PNL;

	private RadioButton RdoYes;

	private RadioButton RdoNo;

	private RadioButton RdoAll;

	private RadioButton RdoNew;

	public string _NewAddItem_PccesCode
	{
		get
		{
			return F_NewAddItem_PccesCode;
		}
		set
		{
			F_NewAddItem_PccesCode = value;
		}
	}

	public string _NewAddItem_PubCode
	{
		get
		{
			return F_NewAddItem_PubCode;
		}
		set
		{
			F_NewAddItem_PubCode = value;
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

	public string _Cstring
	{
		get
		{
			return F_Cstring;
		}
		set
		{
			F_Cstring = value;
		}
	}

	public DataTable _DT_Auto
	{
		get
		{
			return DT_Auto;
		}
		set
		{
			DT_Auto = value;
		}
	}

	public frmMrsBase()
	{
		InitializeComponent();
		functionButtons1.ButtonOwner = LeftPanelStatus.MrsBase;
		PwrSet pwrSet = new PwrSet();
		dsPwrSet = pwrSet.GetEnabledPwrSet();
		string comboList = string.Empty;
		foreach (DataRow dr in dsPwrSet.Tables["PwrSet"].Rows)
		{
			comboList = comboList + ArchConvert.Obj2String(dr["PwrName"]) + "|";
		}
		CellStyle csCb = gridMrsBase1.Styles.Add("ComboList");
		csCb.DataType = typeof(string);
		csCb.ForeColor = Color.Navy;
		csCb.TextAlign = TextAlignEnum.LeftCenter;
		csCb.Font = new System.Drawing.Font(Font, FontStyle.Bold);
		csCb.ComboList = comboList.TrimEnd('|');
		GridCols = gridMrsBase1.Cols.Count;
		Grid2Cols = gridMrsBase2.Cols.Count;
		GridColsSquence = new object[GridCols, 10];
		Grid2ColsSquence = new object[Grid2Cols, 10];
		CellStyle cs = gridMrsBase1.Styles.Add("img");
		cs.DataType = typeof(Image);
		CellStyle cs2 = gridMrsBase1.Styles.Add("EditMode");
		cs2.DataType = typeof(Image);
		cs2.ImageAlign = ImageAlignEnum.RightCenter;
		CellStyle cs_2 = gridMrsBase2.Styles.Add("img");
		cs_2.DataType = typeof(Image);
		CellStyle cs3 = gridMrsBase2.Styles.Add("EditMode");
		cs3.DataType = typeof(Image);
		cs3.ImageAlign = ImageAlignEnum.RightCenter;
	}

	private void Th_BindGrid(string sCri)
	{
		ExtraCri = sCri;
		FORM_STATUS = "BINDING";
		gridMrsBaseDataBind(flag: false, "");
		FORM_STATUS = "NORMAL";
		GC.Collect();
		ultraToolbarsManager1.BeginUpdate();
		ultraToolbarsManager1.EndUpdate();
	}

	private void HideCols(bool IsHide)
	{
		if (IsHide)
		{
			gridMrsBase1.Cols["PubCode"].Visible = false;
			gridMrsBase1.Cols["Analysis"].Visible = false;
			gridMrsBase1.Cols["Show"].Visible = false;
			gridMrsBase2.Cols["Analysis"].Visible = false;
			gridMrsBase2.Cols["SNo"].Visible = false;
			gridMrsBase2.Cols["PubCode"].Visible = false;
		}
		if ((ultraToolbarsManager1.Tools["mnuViewsurName"] as StateButtonTool).Checked)
		{
			gridMrsBase1.Cols["surName"].Visible = false;
			gridMrsBase2.Cols["surName"].Visible = false;
		}
		else
		{
			F_iCount = "Inital";
			(ultraToolbarsManager1.Tools["mnuViewUnsurName"] as StateButtonTool).Checked = true;
			gridMrsBase1.Cols["surName"].Visible = true;
			gridMrsBase2.Cols["surName"].Visible = true;
		}
		if ((ultraToolbarsManager1.Tools["mnuViewcommonName"] as StateButtonTool).Checked)
		{
			gridMrsBase1.Cols["commonName"].Visible = false;
			gridMrsBase2.Cols["commonName"].Visible = false;
			return;
		}
		F_iCount = "Inital";
		(ultraToolbarsManager1.Tools["mnuViewUncommonName"] as StateButtonTool).Checked = true;
		gridMrsBase1.Cols["commonName"].Visible = true;
		gridMrsBase2.Cols["commonName"].Visible = true;
	}

	private void frmMrsBase_Load(object sender, EventArgs e)
	{
		AppLocation = AppDomain.CurrentDomain.BaseDirectory;
		string sIniFileName = AppLocation + "PccesMain.ini";
		string F_IsAddOn = CommonMethods.IniReadValue(sIniFileName, "AddOn", "OperationType");
		string sAllowRestore = CommonMethods.IniReadValue(CommonMethods.ExtractFilePath(Application.ExecutablePath) + "OptionSet.ini", "CommonData", "sAllowRestore");
		if (sAllowRestore.ToUpper() == "TRUE")
		{
			F_IsAddOn = "";
		}
		LoadSettings();
		string sHideCols = CommonMethods.GetDebugValue("MRS", "HideCols");
		HideCols(Convert.ToBoolean((sHideCols == "") ? "True" : sHideCols));
		bool EnablePwrSet = SysConfig.SysEnablePwrSet;
		gridMrsBase1.Cols["PwrSet"].Visible = EnablePwrSet;
		gridMrsBase1.Cols["Account"].Visible = EnablePwrSet;
		gridMrsBase2.Cols["PwrSet"].Visible = EnablePwrSet;
		gridMrsBase2.Cols["Account"].Visible = EnablePwrSet;
		gridMrsBase1.Cols["Rate"].Format = "N1";
		if (SysConfig.SysComsEnable)
		{
			gridMrsBase1.Cols["CheckDailyReportQty"].Visible = true;
		}
		base.ParentForm.Text = "PCCES Win 4.3 【基本資料庫維護】";
		pnlParent.Height = 0;
		functionButtons1._UserID = F_UserID;
		functionButtons1._UserName = F_UserName;
		functionButtons1._ServerName = F_ServerName;
		functionButtons1._CurrOpenMode = FunctionOpenMode.Common;
		functionButtons1._ActiveFunction = "MRSBASE";
		onlineList1._UserID = F_UserID;
		onlineList1._UserName = F_UserName;
		onlineList1._ServerName = F_ServerName;
		onlineList1._FunctionName = "MrsBase";
		onlineList1._HasRegistered = F_HasRegistered;
		onlineList1.Connect();
		SettingDecimal();
		LoadBookmarkFromDB();
		Frm.OnUserRequest += UserReq;
		ultraToolbarsManager1.Tools["MenuCostStructure"].SharedProps.Visible = UseCostStructure;
		ultraToolbarsManager1.Tools["mnuClearCost"].SharedProps.Visible = UseCostStructure;
		ultraToolbarsManager1.Tools["mnuTool_CostStructure"].SharedProps.Visible = UseCostStructure;
		if (UseCostStructure)
		{
			ProcessCostStructure();
		}
		PNL_COST.Width = 0;
		frmMrsBase_Resize(null, null);
		string sBuild = PccesVersion.PccesAssemblyVersion;
		if (sBuild.CompareTo("4.3.1000.211") < 0)
		{
			ultraToolbarsManager1.Tools["mnuCalculateCorrectness"].SharedProps.Visible = false;
			ultraToolbarsManager1.Tools["mnuCorrectItems"].SharedProps.Visible = false;
			ultraToolbarsManager1.Tools["mnuIncorrect"].SharedProps.Visible = false;
			ultraToolbarsManager1.Tools["mnuCorrectCName"].SharedProps.Visible = false;
			ultraToolbarsManager1.Tools["mnuExpNotCorrect"].SharedProps.Visible = false;
			ultraToolbarsManager1.Tools["mnuExpAllCorrect"].SharedProps.Visible = false;
		}
		GC.Collect();
	}

	private void LoadSettings()
	{
		try
		{
			GetIniSetting();
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormMrsBase.cs--LoadSettings" + ex.Message);
		}
	}

	private void GetIniSetting()
	{
		GridPropertySetting.LoadGridProperty(F_UserID, base.Name, gridMrsBase1);
		string sAllowIsTooltip = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "CommonData", "AllowIsTooltip");
		if (sAllowIsTooltip.ToUpper() == "TRUE")
		{
			gridMrsBase1.ShowToolTipOnNarrowColumn = false;
			gridMrsBase2.ShowToolTipOnNarrowColumn = false;
		}
		else
		{
			gridMrsBase1.ShowToolTipOnNarrowColumn = true;
			gridMrsBase2.ShowToolTipOnNarrowColumn = true;
		}
		F_TreeMenu = CommonMethods.GetIniValue("MrsBase", "TreeMenu");
	}

	private void SettingDecimal()
	{
		DataTable DTDecimal = new DataTable();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("基本工料--小數位數讀取");
		Archnowledge.Pcces.BUDClass.PubDecimal dbDecimal = new Archnowledge.Pcces.BUDClass.PubDecimal(aArr);
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
			F_MainQty = 0;
			F_MainCst = 2;
			F_MainAmt = 0;
			F_AnaQty = 3;
			F_AnaCst = 2;
			F_AnaAmt = 2;
		}
		dbDecimal = null;
		DTDecimal = null;
	}

	private void GridResetBack()
	{
		CellRange Rg1 = default(CellRange);
		CellRange Rg2 = new CellRange
		{
			r1 = 0,
			r2 = 0
		};
		Rg1.r1 = 1;
		Rg1.r2 = 1;
		if (gridMrsBase1.Rows.Count > 1)
		{
			gridMrsBase1.Select(Rg1);
		}
		else
		{
			gridMrsBase1.Select(Rg2);
		}
	}

	private void GridResetBack(int iRow)
	{
		CellRange Rg1 = new CellRange
		{
			r1 = iRow,
			r2 = iRow
		};
		gridMrsBase1.Select(Rg1);
	}

	private void BindingFormDatas()
	{
		Cursor = Cursors.WaitCursor;
		if (FORM_STATUS == "INITIAL")
		{
			((ComboBoxTool)ultraToolbarsManager1.Tools["Other_FilterType"]).SelectedIndex = 0;
		}
		Do_Filter();
		Cursor = Cursors.Default;
	}

	private void GetNewData(string sWhere, bool flag)
	{
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("WinFORM 基本工料");
		if (F_CostType == "" || RdoAll.Checked)
		{
			dbMrsBase = new Archnowledge.Pcces.BUDClass.MrsBaseA(F_UserID, aArr);
			dbMrsBase.ps_srckind = "MRS";
			dbMrsBase.ps_projectcode = "";
			if (sWhere.Trim() == "")
			{
				DT1 = dbMrsBase.ListItem();
			}
			else
			{
				DT1 = dbMrsBase.ListItem(sWhere, "");
			}
		}
		else
		{
			if (RdoNo.Checked)
			{
				sWhere = ((sWhere.Trim().Length <= 0) ? " B.CostUID is null " : (sWhere + " and  B.CostUID is null "));
			}
			else if (RdoYes.Checked && !flag)
			{
				sWhere = ((sWhere.Trim().Length <= 0) ? " B.CostUID is not null " : (sWhere + " and  B.CostUID is not null "));
			}
			DT1 = _CostStructure.ListItemCost(sWhere, F_CostType);
		}
		iCount = DT1.Rows.Count;
	}

	private void RememberColsProps()
	{
		for (int i = 0; i < GridCols; i++)
		{
			GridColsSquence[i, 0] = gridMrsBase1.Cols[i].Name;
			GridColsSquence[i, 1] = gridMrsBase1.Cols[i].Caption;
			GridColsSquence[i, 2] = gridMrsBase1.Cols[i].Width;
			GridColsSquence[i, 3] = gridMrsBase1.Cols[i].DataType;
			GridColsSquence[i, 4] = gridMrsBase1.Cols[i].Visible;
			GridColsSquence[i, 5] = gridMrsBase1.Cols[i].Format;
			GridColsSquence[i, 6] = gridMrsBase1.Cols[i].AllowEditing;
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
			GridColsSquence[i, 7] = gridMrsBase1.Cols[i].TextAlign;
			GridColsSquence[i, 8] = gridMrsBase1.Cols[i].AllowDragging;
			GridColsSquence[i, 9] = gridMrsBase1.Cols[i].AllowResizing;
		}
	}

	private void RememberColsProps2()
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
			if (gridMrsBase2.Cols[i].Name == "Cost")
			{
				if (F_MainCst > 0)
				{
					Grid2ColsSquence[i, 5] = "###,###,###,##0." + "0".PadLeft(F_MainCst, '0');
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

	private void UndoRedoStatus()
	{
		if (gridMrsBase1 != null && ultraToolbarsManager1 != null)
		{
			ultraToolbarsManager1.Tools["mnuUndo"].SharedProps.Enabled = gridMrsBase1.CanUndo;
		}
	}

	private void ultraButton4_Click(object sender, EventArgs e)
	{
		gridMrsBase1.DrawMode = DrawModeEnum.Normal;
		gridMrsBase1.Clear(ClearFlags.All);
	}

	private void ultraToolbarsManager1_ToolClick(object sender, ToolClickEventArgs e)
	{
		DoMenuAction(e.Tool.Key);
	}

	private void frmMrsBase_FormClosing(object sender, FormClosingEventArgs e)
	{
		try
		{
			GridPropertySetting.SaveGridProperty(F_UserID, base.Name, gridMrsBase1);
			GC.Collect();
			SaveBookmarkToDB();
			if (PNL_TREE.Width == 0)
			{
				CommonMethods.WriteIniValue("MrsBase", "TreeMenu", "CLOSE");
			}
			else
			{
				CommonMethods.WriteIniValue("MrsBase", "TreeMenu", "OPEN");
			}
			PopupMenuTool Addon = (PopupMenuTool)ultraToolbarsManager1.Tools["AddOn"];
			for (int i = 0; i < Addon.Tools.Count; i++)
			{
				Addon.Tools[i].Reset();
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormMrsBase.cs--frmMrsBase_Closing" + ex.Message);
			Console.Write(ex.Message);
		}
	}

	private void DoMenuAction(string MenuID)
	{
		Frm.Close();
		switch (MenuID)
		{
		case "mnuFile_Exit":
			CloseThisForm();
			break;
		case "mnuImpExcel":
			if (!DBClass.ChkAuthority(F_UserID, "F002000100010001"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F002000100010001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			DoImport(ImportType.Excel);
			break;
		case "mnuImpXML":
			if (!DBClass.ChkAuthority(F_UserID, "F002000100010002"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F002000100010002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			DoImport(ImportType.XML);
			break;
		case "mnuImportBasic":
		{
			if (!DBClass.ChkAuthority(F_UserID, "F002000100010002"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F002000100010002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			string sFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "OrganizationDatabases\\PccesBasicItem,108-12-31.xml");
			DoImport(ImportType.XML, IsAuto: true, sFile);
			break;
		}
		case "mnuExpExcel":
			if (!DBClass.ChkAuthority(F_UserID, "F002000100020001"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F002000100020001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			DoExport(ExportType.Excel);
			break;
		case "mnuExpXML":
			if (!DBClass.ChkAuthority(F_UserID, "F002000100020002"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F002000100020002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			DoExport(ExportType.XML);
			break;
		case "mnuUndo":
			Do_Undo();
			break;
		case "mnuRedo":
			gridMrsBase1.Redo();
			break;
		case "mnuCopy":
			if (!DBClass.ChkAuthority(F_UserID, "F00200040002"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00200040002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			ExecuteEditForm(MrsBaseEditFormType.CopyToNew);
			break;
		case "mnuFind":
			if (!DBClass.ChkAuthority(F_UserID, "F00200020003"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00200020003") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			EventExtend_Find();
			break;
		case "mnuView_ItemAll":
			if (!DBClass.ChkAuthority(F_UserID, "F00200030001"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00200030001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			((TextBoxTool)ultraToolbarsManager1.Tools["Other_QueryText"]).Text = "";
			Do_Filter();
			break;
		case "mnuView_ItemBDOnly":
			if (!DBClass.ChkAuthority(F_UserID, "F00200030002"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00200030002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			Do_Filter();
			break;
		case "mnuView_ItemBDNone":
			if (!DBClass.ChkAuthority(F_UserID, "F00200030003"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00200030003") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			Do_Filter();
			break;
		case "mnuchkViewWorK":
			if (!DBClass.ChkAuthority(F_UserID, "F002000300040001"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F002000300040001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			Do_Filter();
			break;
		case "mnuchkViewLabor":
			if (!DBClass.ChkAuthority(F_UserID, "F002000300040002"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F002000300040003") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			Do_Filter();
			break;
		case "mnuchkViewEquip":
			if (!DBClass.ChkAuthority(F_UserID, "F002000300040003"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F002000300040003") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			Do_Filter();
			break;
		case "mnuchkViewMaterial":
			if (!DBClass.ChkAuthority(F_UserID, "F002000300040004"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F002000300040004") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			Do_Filter();
			break;
		case "mnuchkViewWaste":
			if (!DBClass.ChkAuthority(F_UserID, "F002000300040005"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F002000300040005") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			Do_Filter();
			break;
		case "mnuCorrectItems":
			Do_Filter();
			break;
		case "mnuIncorrect":
			Do_Filter();
			break;
		case "mnuCorrectCName":
		{
			DBClass DBCls = new DBClass();
			DBCls._FS_UserID = F_UserID;
			int iCorectabledCount = int.Parse(DBCls.GetUserDefine_String("Select Count(*) as iCount from mrsBaseA Where (CorrectCName <> null Or CorrectCName <>'') Or (CorrectUnitName <> null Or CorrectUnitName <>'')", "iCount"));
			FormBudgetRes_CNameCorrect FM = new FormBudgetRes_CNameCorrect();
			FM.Owner = this;
			FM._FormActionName = PccesFormAction.MrsBase;
			FM._UserID = F_UserID;
			FM._TotalItemCount = gridMrsBase1.Rows.Count - 1;
			FM._CorrectableItemCount = iCorectabledCount;
			if (FM.ShowDialog() == DialogResult.OK)
			{
				if (!((StateButtonTool)ultraToolbarsManager1.Tools["mnuView_ItemAll"]).Checked)
				{
					((StateButtonTool)ultraToolbarsManager1.Tools["mnuView_ItemAll"]).Checked = true;
				}
				else
				{
					BindingFormDatas();
				}
				Application.DoEvents();
				updateCorrectConfirm();
			}
			break;
		}
		case "mnuchkViewUsual":
			Do_Filter();
			break;
		case "mnuWork_New":
			if (!DBClass.ChkAuthority(F_UserID, "F00200040001"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00200040001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			ExecuteEditForm(MrsBaseEditFormType.New);
			break;
		case "mnuWork_Edit":
			if (!DBClass.ChkAuthority(F_UserID, "F00200040003"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00200040003") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			ExecuteEditForm(MrsBaseEditFormType.Edit);
			break;
		case "mnuWork_Delete":
			if (!DBClass.ChkAuthority(F_UserID, "F00200020002"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00200020002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (!((TextBoxTool)ultraToolbarsManager1.Tools["Other_QueryText"]).IsInEditMode)
			{
				Delete_MrsItems();
			}
			break;
		case "mnuTool_DecSetting":
		{
			if (!DBClass.ChkAuthority(F_UserID, "F00200050001"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00200050001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			FormMrsBaseDecimal FMDec = new FormMrsBaseDecimal();
			FMDec._UserID = F_UserID;
			if (FMDec.ShowDialog() == DialogResult.OK)
			{
				SettingDecimal();
				Do_Filter();
			}
			FMDec.Close();
			FMDec.Dispose();
			FMDec = null;
			break;
		}
		case "mnuTool_Recalculate":
			if (!DBClass.ChkAuthority(F_UserID, "F00200050002"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00200050002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			ReCal_All();
			break;
		case "mnuTool_AddBookmark":
			if (!DBClass.ChkAuthority(F_UserID, "F00200050004"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00200050004") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			Add_Bookmark();
			break;
		case "mnuTool_ClearBookmarkAll":
			if (!DBClass.ChkAuthority(F_UserID, "F00200050005"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00200050005") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			Clear_Bookmark();
			break;
		case "mnuTool_ClearBookmarkSpeci":
			Clear_Bookmark_Speci();
			break;
		case "mnuTool_FindParent":
			if (!DBClass.ChkAuthority(F_UserID, "F00200050007"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00200050007") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			Find_Parent();
			break;
		case "mnuTool_SetAsUsualItem":
			if (!DBClass.ChkAuthority(F_UserID, "F00200050008"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00200050008") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			Add_UsualItem();
			break;
		case "mnuTool_CancelUsualItem":
			Cancel_UsualItem();
			break;
		case "mnuSwitchPrice_Nor":
			if (!DBClass.ChkAuthority(F_UserID, "F002000500030001"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F002000500030001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			GetNewDataBySection("北區");
			break;
		case "mnuSwitchPrice_Mid":
			if (!DBClass.ChkAuthority(F_UserID, "F002000500030002"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F002000500030002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			GetNewDataBySection("中區");
			break;
		case "mnuSwitchPrice_Sou":
			if (!DBClass.ChkAuthority(F_UserID, "F002000500030003"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F002000500030003") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			GetNewDataBySection("南區");
			break;
		case "mnuSwitchPrice_Est":
			if (!DBClass.ChkAuthority(F_UserID, "F002000500030004"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F002000500030004") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			GetNewDataBySection("東區");
			break;
		case "mnuSwitchPrice_Out":
			if (!DBClass.ChkAuthority(F_UserID, "F002000500030005"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F002000500030005") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			GetNewDataBySection("離島");
			break;
		case "mnuPccesAbout":
		{
			FormAbout FMAB = new FormAbout();
			FMAB.ShowDialog();
			FMAB.Close();
			FMAB.Dispose();
			FMAB = null;
			break;
		}
		case "Other_FilterExecute":
			Do_Filter();
			break;
		case "Other_ShowAllItem":
			Do_AllItem();
			break;
		case "MenuEdit_SelAll":
			if (!DBClass.ChkAuthority(F_UserID, "F00200020001"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00200020001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			SelectAll();
			break;
		case "MenuViewAnalysis":
			ExecuteBreakdownForm(gridMrsBase1);
			break;
		case "MenuViewAll":
			Do_Filter();
			break;
		case "mnuAutoNum":
			if (!DBClass.ChkAuthority(F_UserID, "F00200050006"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00200050006") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			ExecuteAutoNumForm();
			BindingFormDatas();
			break;
		case "mnuClearDB":
			if (!DBClass.ChkAuthority(F_UserID, "F00200050009"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00200050009") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			Do_ClearDB();
			break;
		case "mnuPickFromOtherDB":
			if (!DBClass.ChkAuthority(F_UserID, "F00200050010"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00200050010") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			Do_PickFromOtherDB();
			break;
		case "mnuViewTree":
			if (!DBClass.ChkAuthority(F_UserID, "F00200030005"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00200030005") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			Do_TreeView();
			if (!(ultraToolbarsManager1.Tools["mnuViewTree"] as StateButtonTool).Checked)
			{
				PNL_TREE.Width = 0;
			}
			break;
		case "mnu_Go":
			Do_ToolBarFind();
			break;
		case "mnuChangeCode":
			if (!DBClass.ChkAuthority(F_UserID, "F00200050011"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00200050011") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			ExecuteChangeCode();
			break;
		case "mnuToolApprove":
			if (!DBClass.ChkAuthority(F_UserID, "F00200050012"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00200050012") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			ExecuteApproveCode();
			break;
		case "mnuConCost":
			if (!DBClass.ChkAuthority(F_UserID, "F00200050013"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00200050013") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			Execute_ConCost();
			break;
		case "mnuCalcErr":
			Th_BindGrid(" And CalcError = '1' ");
			break;
		case "mnuViewUnApprove":
			Do_Filter();
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
		case "mnuCodeUpgrade":
			Do_CodeUpgrade();
			Do_Filter();
			break;
		case "mnuView_PickClass":
			Do_PickType();
			Do_Filter();
			break;
		case "mnu_PickClass":
			Do_PickClass();
			break;
		case "mnuView_PickClassItems":
			Do_PickClassItems();
			break;
		case "mnuPriceHigh":
			Do_changePrice("H");
			Do_Filter();
			break;
		case "mnuPriceMedium":
			Do_changePrice("M");
			Do_Filter();
			break;
		case "mnuPriceLow":
			Do_changePrice("L");
			Do_Filter();
			break;
		case "mnuViewUnsurName":
		case "mnuViewUncommonName":
			if (F_iCount == "")
			{
				Do_Filter();
			}
			else
			{
				F_iCount = "";
			}
			break;
		case "mnuViewsurName":
		case "mnuViewcommonName":
			Do_Filter();
			break;
		case "mnuClearCost":
			Clear_Cost();
			Do_Filter();
			break;
		case "mnuTool_CostStructure":
			FormCostInitial();
			break;
		case "mnuImport3652":
			Import3652();
			BindingFormDatas();
			break;
		case "mnuCalculateCorrectness":
			if (!((StateButtonTool)ultraToolbarsManager1.Tools["mnuView_ItemAll"]).Checked)
			{
				((StateButtonTool)ultraToolbarsManager1.Tools["mnuView_ItemAll"]).Checked = true;
			}
			else
			{
				BindingFormDatas();
			}
			Application.DoEvents();
			updateCorrectConfirm();
			break;
		case "mnuExpNotCorrect":
		{
			Cursor = Cursors.WaitCursor;
			DBClass DBCls = new DBClass();
			DBCls._FS_UserID = F_UserID;
			DataTable DT = DBCls.GetUserDefine("Select pccesCode, cName, Analysis, unitName, usrQty, cost, usrAmt, Correct, Confirm, CompareErrState, CorrectCName, CorrectUnitName from mrsBaseA Where Correct <> '是' Order By PccesCode");
			if (DT.Rows.Count == 0)
			{
				MessageBox.Show(this, "沒有資料可匯出!!", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
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
			for (int i = 1; i < DT.Rows.Count; i++)
			{
				sheetMrsBaseA.Cells[i, 0].PutValue(DT.Rows[i]["pccesCode"].ToString());
				sheetMrsBaseA.Cells[i, 1].PutValue(DT.Rows[i]["cName"].ToString());
				if (DT.Rows[i]["Analysis"] != DBNull.Value && DT.Rows[i]["Analysis"].ToString() == "1")
				{
					sheetMrsBaseA.Cells[i, 2].PutValue("V");
				}
				else
				{
					sheetMrsBaseA.Cells[i, 2].PutValue("");
				}
				sheetMrsBaseA.Cells[i, 3].PutValue(DT.Rows[i]["unitName"].ToString());
				if (DT.Rows[i]["usrQty"] != DBNull.Value)
				{
					sheetMrsBaseA.Cells[i, 4].PutValue(string.Format("{0:N3}", decimal.Parse(DT.Rows[i]["usrQty"].ToString())));
				}
				if (DT.Rows[i]["cost"] != DBNull.Value)
				{
					sheetMrsBaseA.Cells[i, 5].PutValue(string.Format("{0:N2}", decimal.Parse(DT.Rows[i]["cost"].ToString())));
				}
				if (DT.Rows[i]["usrAmt"] != DBNull.Value)
				{
					sheetMrsBaseA.Cells[i, 6].PutValue(string.Format("{0:N2}", decimal.Parse(DT.Rows[i]["usrAmt"].ToString())));
				}
				sheetMrsBaseA.Cells[i, 7].PutValue(DT.Rows[i]["Correct"].ToString());
				sheetMrsBaseA.Cells[i, 8].PutValue(DT.Rows[i]["Confirm"].ToString());
				sheetMrsBaseA.Cells[i, 9].PutValue(DT.Rows[i]["CompareErrState"].ToString());
				sheetMrsBaseA.Cells[i, 10].PutValue(DT.Rows[i]["CorrectCName"].ToString());
				sheetMrsBaseA.Cells[i, 11].PutValue(DT.Rows[i]["CorrectUnitName"].ToString());
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
			DBClass DBCls = new DBClass();
			DBCls._FS_UserID = F_UserID;
			DataTable DT = DBCls.GetUserDefine("Select pccesCode, cName, Analysis, unitName, usrQty, cost, usrAmt, Correct, Confirm, CompareErrState, CorrectCName, CorrectUnitName from mrsBaseA Where Correct = '是' Order By PccesCode");
			if (DT.Rows.Count == 0)
			{
				MessageBox.Show(this, "沒有資料可匯出!!", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
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
			for (int i = 1; i < DT.Rows.Count; i++)
			{
				sheetMrsBaseA.Cells[i, 0].PutValue(DT.Rows[i]["pccesCode"].ToString());
				sheetMrsBaseA.Cells[i, 1].PutValue(DT.Rows[i]["cName"].ToString());
				if (DT.Rows[i]["Analysis"] != DBNull.Value && DT.Rows[i]["Analysis"].ToString() == "1")
				{
					sheetMrsBaseA.Cells[i, 2].PutValue("V");
				}
				else
				{
					sheetMrsBaseA.Cells[i, 2].PutValue("");
				}
				sheetMrsBaseA.Cells[i, 3].PutValue(DT.Rows[i]["unitName"].ToString());
				if (DT.Rows[i]["usrQty"] != DBNull.Value)
				{
					sheetMrsBaseA.Cells[i, 4].PutValue(string.Format("{0:N3}", decimal.Parse(DT.Rows[i]["usrQty"].ToString())));
				}
				if (DT.Rows[i]["cost"] != DBNull.Value)
				{
					sheetMrsBaseA.Cells[i, 5].PutValue(string.Format("{0:N2}", decimal.Parse(DT.Rows[i]["cost"].ToString())));
				}
				if (DT.Rows[i]["usrAmt"] != DBNull.Value)
				{
					sheetMrsBaseA.Cells[i, 6].PutValue(string.Format("{0:N2}", decimal.Parse(DT.Rows[i]["usrAmt"].ToString())));
				}
				sheetMrsBaseA.Cells[i, 7].PutValue(DT.Rows[i]["Correct"].ToString());
				sheetMrsBaseA.Cells[i, 8].PutValue(DT.Rows[i]["Confirm"].ToString());
				sheetMrsBaseA.Cells[i, 9].PutValue(DT.Rows[i]["CompareErrState"].ToString());
				sheetMrsBaseA.Cells[i, 10].PutValue(DT.Rows[i]["CorrectCName"].ToString());
				sheetMrsBaseA.Cells[i, 11].PutValue(DT.Rows[i]["CorrectUnitName"].ToString());
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
		}
		UndoRedoStatus();
	}

	private void SetAllBorders(Style style, CellBorderType borderType)
	{
		style.Borders[BorderType.TopBorder].LineStyle = borderType;
		style.Borders[BorderType.RightBorder].LineStyle = borderType;
		style.Borders[BorderType.BottomBorder].LineStyle = borderType;
		style.Borders[BorderType.LeftBorder].LineStyle = borderType;
	}

	private void updateCorrectConfirm()
	{
		gridMrsBase1.Redraw = false;
		Cursor = Cursors.WaitCursor;
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
		FormProgress FM = new FormProgress();
		FM._Max = 100;
		FM._Min = 0;
		FM.Message = "自動編碼資料讀取中...";
		FM.TopMost = true;
		FM.Show();
		Application.DoEvents();
		DBClass dbC = new DBClass();
		dbC._FS_UserID = F_UserID;
		dtAutoNumB = dbC.GetAutoNumB();
		dtAutoNumA = dbC.GetAutoNumA();
		cCV = new CodeValidator(dtAutoNumA, dtAutoNumB);
		cCV._UserID = F_UserID;
		cCF = new CodeFitter();
		dWeightCorrectRatio = 0m;
		dWeightCorrectRatio = 0m;
		dCorrectTotal = 0m;
		dFitTotal = 0m;
		dTotal = 0m;
		iCorrect = 0m;
		iFit = 0m;
		DataView dvResource = dbMrsBase.ListItem().DefaultView;
		string sItemClass = "";
		FM.SetMessage("資料計算中...");
		FM.SetMax(dvResource.Count);
		Application.DoEvents();
		int i = 0;
		dvResource.Sort = "pccesCode Asc";
		for (; i < dvResource.Count; i++)
		{
			if (i % 100 == 0)
			{
				FM.SetMessage("(正在計算第 " + i + " 筆 / 共 " + dvResource.Count + " 筆)");
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
			else if (theRow["CName"] != null && theRow["CName"].ToString().Trim().IndexOf(strName.Trim(), 0) < 0)
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
			SqlCommand Cmd = new SqlCommand();
			Cmd.CommandText = "Update mrsBaseA Set Correct=@Correct, Confirm=@Confirm, CompareErrState=@CompareErrState, CorrectCName=@CorrectCName, CorrectUnitName=@CorrectUnitName Where pubCode=@pubCode";
			Cmd.Parameters.Add("@Correct", SqlDbType.NVarChar, 1);
			Cmd.Parameters.Add("@Confirm", SqlDbType.NVarChar, 1);
			Cmd.Parameters.Add("@CompareErrState", SqlDbType.NVarChar, 200);
			Cmd.Parameters.Add("@CorrectCName", SqlDbType.NVarChar, 200);
			Cmd.Parameters.Add("@CorrectUnitName", SqlDbType.NVarChar, 10);
			Cmd.Parameters.Add("@pubCode", SqlDbType.Int);
			Cmd.Parameters["@Correct"].Value = ((theRow["Correct"] == null) ? DBNull.Value : theRow["Correct"]);
			Cmd.Parameters["@Confirm"].Value = ((theRow["Confirm"] == null) ? DBNull.Value : theRow["Confirm"]);
			Cmd.Parameters["@CompareErrState"].Value = ((theRow["CompareErrState"] == null) ? DBNull.Value : theRow["CompareErrState"]);
			Cmd.Parameters["@CorrectCName"].Value = ((theRow["CorrectCName"] == null) ? DBNull.Value : theRow["CorrectCName"]);
			Cmd.Parameters["@CorrectUnitName"].Value = ((theRow["CorrectUnitName"] == null) ? DBNull.Value : theRow["CorrectUnitName"]);
			Cmd.Parameters["@pubCode"].Value = ((theRow["pubCode"] == null) ? DBNull.Value : theRow["pubCode"]);
			dbC.ExecuteSqlCommand(Cmd);
		}
		FM.Hide();
		FM.Dispose();
		Cursor = Cursors.Default;
		gridMrsBase1.Redraw = true;
		MessageBox.Show("OK");
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

	private void Do_CodeUpgrade()
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
		string StrAdp = "SELECT pubCode, pccesCode FROM MrsBaseA ";
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
			CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormMrsBase.cs--Do_CodeUpgrade" + ex.Message);
			OldTran.Rollback();
			Console.Write(ex.Message);
		}
		FM_INFO.Hide();
		FM_INFO.Dispose();
		FM_INFO = null;
		Cursor = Cursors.Default;
	}

	private void ExecuteApproveCode()
	{
		FormMrsBaseApprove FM_APPRV = new FormMrsBaseApprove();
		FM_APPRV._UserID = F_UserID;
		if (FM_APPRV.ShowDialog() == DialogResult.OK)
		{
			Do_Filter();
		}
		FM_APPRV.Close();
		FM_APPRV.Dispose();
		FM_APPRV = null;
	}

	private void Execute_ConCost()
	{
		FormConCost FM_COST = new FormConCost();
		FM_COST.Owner = this;
		FM_COST._UserID = F_UserID;
		FM_COST.ShowDialog();
		FM_COST.Close();
		FM_COST.Dispose();
		FM_COST = null;
		Do_Filter();
	}

	private void Do_ToolBarFind()
	{
		if (gridMrsBase1.Rows.Count > 1)
		{
			string sSearchText = ((ComboBoxTool)ultraToolbarsManager1.Tools["mnu_Cbo1"]).Text.Trim();
			Do_Find2(sSearchText, "", "");
		}
	}

	private void Do_TreeView()
	{
		PNL_TREE.Width = 200;
		PNL_TREE.Visible = true;
	}

	private void Do_PickFromOtherDB()
	{
		FormMrsBase_PickFromOtherDB FM_PKFRM = new FormMrsBase_PickFromOtherDB();
		FM_PKFRM._UserID = F_UserID;
		if (FM_PKFRM.ShowDialog() == DialogResult.OK)
		{
			BindingFormDatas();
		}
		FM_PKFRM.Dispose();
		FM_PKFRM = null;
	}

	private void Do_Undo()
	{
		gridMrsBase1.Undo();
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add("基本工料--Undo後儲存");
		Archnowledge.Pcces.BUDClass.MrsBaseA dbMrsBase = new Archnowledge.Pcces.BUDClass.MrsBaseA(F_UserID, aArr);
		dbMrsBase.ps_srckind = "MRS";
		dbMrsBase.ps_projectcode = "";
		dbMrsBase.ps_pccesCode = gridMrsBase1[gridMrsBase1.Row, "PccesCode"].ToString();
		string Data = null;
		if (gridMrsBase1[gridMrsBase1.Row, gridMrsBase1.Col] != null)
		{
			Data = gridMrsBase1[gridMrsBase1.Row, gridMrsBase1.Col].ToString();
		}
		if (gridMrsBase1.Cols[gridMrsBase1.Col].Name == "Cost")
		{
			dbMrsBase.ps_cost = Data;
		}
		if (gridMrsBase1.Cols[gridMrsBase1.Col].Name == "Memo")
		{
			dbMrsBase.ps_memo = Data;
		}
		if (gridMrsBase1.Cols[gridMrsBase1.Col].Name == "LRate")
		{
			dbMrsBase.ps_lRate = Data;
		}
		if (gridMrsBase1.Cols[gridMrsBase1.Col].Name == "ERate")
		{
			dbMrsBase.ps_eRate = Data;
		}
		if (gridMrsBase1.Cols[gridMrsBase1.Col].Name == "MRate")
		{
			dbMrsBase.ps_mRate = Data;
		}
		if (gridMrsBase1.Cols[gridMrsBase1.Col].Name == "WRate")
		{
			dbMrsBase.ps_wRate = Data;
		}
		dbMrsBase.UpdItem();
		dbMrsBase = null;
	}

	private void Do_ClearDB()
	{
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = F_UserID;
		string sCounter = "";
		try
		{
			sCounter = DBCLS.GetUserDefine_String("Select Count(*) as Counter From MrsBaseA Where ModLock <> '" + F_UserID + "' And (ModLock <>'' AND ModLock is not null)", "Counter");
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormMrsBase.cs--Do_ClearDB" + ex.Message);
			DBCLS.CheckMrsSchema("MRS");
		}
		int iCounter = Convert.ToInt32(sCounter);
		if (iCounter > 0)
		{
			DataRow DR = DBCLS.GetOccupieData("", "MRS");
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
			FM_MSG._Message = "目前基本資料庫尚有其他人正在編輯資料，現在不可以清空資料庫。";
			FM_MSG.ShowDialog(this);
			FM_MSG.Close();
			FM_MSG.Dispose();
			FM_MSG = null;
		}
		else
		{
			string sQry = "您正清空工項基本資料庫所有資料\n其單價分析及相關資將被刪除，\n\n假如你按一下「是」，您將不能復原此刪除操作。\n您確定您要清空這些資料嗎?";
			if (MessageBox.Show(this, sQry, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
			{
				ArrayList aArr = new ArrayList();
				aArr.Add(F_UserID);
				aArr.Add("清空資料庫");
				ModifyDB StdCom = new ModifyDB("", aArr);
				string ls_deletestr = "Delete from MrsBaseA " + '\r';
				ls_deletestr = ls_deletestr + "Delete from MrsBaseB " + '\r';
				int rtnval = StdCom.DBDele(ls_deletestr);
				StdCom = null;
				PubTools.WriteRoughlyLog(aArr);
				Do_Filter();
				gridMrsBase1.ClearUndo();
				UndoRedoStatus();
			}
			DBCLS = null;
		}
	}

	private void ExecuteAutoNumForm()
	{
		FormAutoNum FM_AUTO_NO = new FormAutoNum(F_UserID);
		FM_AUTO_NO.ShowDialog(this);
		FM_AUTO_NO.Dispose();
		FM_AUTO_NO = null;
	}

	private void DoImport(ImportType e)
	{
		FormMrsBase_ImpWizard FM_MRS_IMP = new FormMrsBase_ImpWizard();
		FM_MRS_IMP._ImportType = e;
		FM_MRS_IMP._dsPwrSet = dsPwrSet;
		FM_MRS_IMP._UserID = F_UserID;
		FM_MRS_IMP._ActionName = PccesFormAction.MrsBase;
		if (FM_MRS_IMP.ShowDialog(this) == DialogResult.OK)
		{
			Do_Filter();
		}
		FM_MRS_IMP.Close();
		FM_MRS_IMP.Dispose();
		FM_MRS_IMP = null;
	}

	private void DoImport(ImportType e, bool IsAuto, string SourceFile)
	{
		FormMrsBase_ImpWizard FM_MRS_IMP = new FormMrsBase_ImpWizard();
		FM_MRS_IMP._ImportType = e;
		FM_MRS_IMP._dsPwrSet = dsPwrSet;
		FM_MRS_IMP._UserID = F_UserID;
		FM_MRS_IMP._ActionName = PccesFormAction.MrsBase;
		FM_MRS_IMP._IsAutoExecute = IsAuto;
		FM_MRS_IMP._SourceFile = SourceFile;
		if (FM_MRS_IMP.ShowDialog(this) == DialogResult.OK)
		{
			Do_Filter();
		}
		FM_MRS_IMP.Close();
		FM_MRS_IMP.Dispose();
		FM_MRS_IMP = null;
	}

	private void DoExport(ExportType e)
	{
		DataTable DT_Exp = new DataTable();
		if (e == ExportType.Excel)
		{
			ArrayList aArr = new ArrayList();
			aArr.Clear();
			aArr.Add(F_UserID);
			aArr.Add("匯出基本工料");
			Recost Recost1 = new Recost(aArr);
			Recost1.ps_prjcode = "";
			Recost1.ps_srckind = "MRS";
			dbMrsBase.ps_srckind = "MRS";
			dbMrsBase.ps_projectcode = "";
			DT_Exp = dbMrsBase.ListItem();
			DT_Exp.Columns.Add("chk", Type.GetType("System.String"));
			DataColumn[] Keys = new DataColumn[1];
			DataColumn myColumn = DT_Exp.Columns["pubCode"];
			Keys[0] = myColumn;
			DT_Exp.PrimaryKey = Keys;
			int iSels = 0;
			int iDone = 0;
			if (UsedGrid == "DEFAULT")
			{
				iSels = gridMrsBase1.SelectedItems;
				iDone = 0;
				for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
				{
					if (iDone >= iSels)
					{
						break;
					}
					if (gridMrsBase1.Rows[i].Selected)
					{
						DataRow DR_Find = DT_Exp.Rows.Find((int)DV1[i - 1]["PubCode"]);
						if (DR_Find != null)
						{
							DR_Find["chk"] = "1";
							iDone++;
						}
						DR_Find = null;
					}
				}
			}
			else
			{
				iSels = gridMrsBase2.SelectedItems;
				iDone = 0;
				for (int i = 1; i < gridMrsBase2.Rows.Count; i++)
				{
					if (iDone >= iSels)
					{
						break;
					}
					if (gridMrsBase2.Rows[i].Selected)
					{
						DataRow DR_Find = DT_Exp.Rows.Find((int)gridMrsBase2[i, "PubCode"]);
						if (DR_Find != null)
						{
							DR_Find["chk"] = "1";
							iDone++;
						}
						DR_Find = null;
					}
				}
			}
			aArr = null;
			Recost1 = null;
			Keys = null;
			myColumn = null;
		}
		FormMrsBase_ExpWizard FM_MRS_EXP = new FormMrsBase_ExpWizard();
		FM_MRS_EXP._UserID = F_UserID;
		FM_MRS_EXP._ExportType = e;
		FM_MRS_EXP._dsPwrSet = dsPwrSet;
		FM_MRS_EXP._DT_ExpDatas = DT_Exp;
		FM_MRS_EXP._ActionName = F_ActionName;
		FM_MRS_EXP._ProjectCode = "";
		FM_MRS_EXP.ShowDialog(this);
		FM_MRS_EXP.Close();
		FM_MRS_EXP.Dispose();
		FM_MRS_EXP = null;
		DT_Exp = null;
	}

	private void SelectAll()
	{
		if (gridMrsBase1.Rows.Count > 1)
		{
			gridMrsBase1.Focus();
			for (int i = 0; i < gridMrsBase1.Rows.Count; i++)
			{
				gridMrsBase1.Rows[i].Selected = true;
			}
		}
	}

	private void CloseThisForm()
	{
		string sWarning = "確定要結束 ?";
		if (MessageBox.Show(this, sWarning, "基本資料庫維護", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			(base.ParentForm as frmPccesMain).LeftPanel.Width = 160;
			Close();
		}
	}

	private void ReCal_All()
	{
		Cursor = Cursors.WaitCursor;
		if (MessageBox.Show(this, "確定要執行全部重新小計?", "基本資料維護", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			ultraToolbarsManager1.Enabled = false;
			ultraToolbarsManager1.BeginUpdate();
			ArrayList aArr = new ArrayList();
			aArr.Clear();
			aArr.Add(F_UserID);
			aArr.Add("WinFORM 基本工料");
			Recost Recost1 = new Recost(aArr);
			Recost1.ps_prjcode = "";
			Recost1.ps_srckind = "MRS";
			dbMrsBase.ps_srckind = "MRS";
			dbMrsBase.ps_projectcode = "";
			DT1 = dbMrsBase.ListItem(" analysis = '1' ");
			ultraStatusBar1.Panels[1].ProgressBarInfo.Value = 0;
			ultraStatusBar1.Panels[1].ProgressBarInfo.Minimum = 0;
			ultraStatusBar1.Panels[1].ProgressBarInfo.Maximum = DT1.Rows.Count;
			for (int i = 0; i < DT1.Rows.Count; i++)
			{
				Recost1.ps_pubcode = DT1.Rows[i]["pubCode"].ToString();
				Recost1.ReCalc(1);
				ultraStatusBar1.Panels[1].ProgressBarInfo.Value++;
				Application.DoEvents();
			}
			BindingFormDatas();
			GridResetBack();
			ultraStatusBar1.Panels[1].ProgressBarInfo.Value = 0;
			ultraToolbarsManager1.Enabled = true;
			ultraToolbarsManager1.EndUpdate();
			Application.DoEvents();
			MessageBox.Show(this, "完成全部重新小計!", "基本資料維護", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		Cursor = Cursors.Default;
	}

	private void Find_Parent()
	{
		int iPubCode = (int)gridMrsBase1[gridMrsBase1.Row, "PubCode"];
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("WinFORM 基本工料--父項查詢");
		Archnowledge.Pcces.BUDClass.MrsBaseA dbMrsBase = new Archnowledge.Pcces.BUDClass.MrsBaseA(F_UserID, aArr);
		dbMrsBase.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		dbMrsBase.ps_projectcode = "";
		DataTable DT_Parent = dbMrsBase.ListParentItem(iPubCode.ToString());
		if (DT_Parent.Rows.Count > 0)
		{
			RememberColsProps2();
			DataView DV1 = DT_Parent.DefaultView;
			DV1.Sort = " pccesCode ASC ";
			CellStyle CS1 = gridMrsBase2.Styles.Add("AnalysisColor");
			CellStyle CS2 = gridMrsBase2.Styles.Add("LEMColor");
			CellStyle CS3 = gridMrsBase2.Styles.Add("WColor");
			CS1.ForeColor = Color.Red;
			CS2.ForeColor = Color.Teal;
			CS3.ForeColor = Color.Purple;
			gridMrsBase2.Clear(ClearFlags.All);
			gridMrsBase2.Select(0, 0);
			gridMrsBase2.Rows.Count = DV1.Count + 1;
			SetGrid2Column();
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
				gridMrsBase2[i + 1, "Cost"] = DV1[i]["cost"];
				gridMrsBase2[i + 1, "usrAmt"] = DV1[i]["usrAmt"];
				gridMrsBase2[i + 1, "surName"] = DV1[i]["surName"];
				gridMrsBase2[i + 1, "Account"] = DV1[i]["Account"];
				gridMrsBase2[i + 1, "commonName"] = DV1[i]["commonName"];
				if (DV1[i]["PwrSet"] != null)
				{
					gridMrsBase2[i + 1, "PwrSet"] = PwrSet.GetName(dsPwrSet, PubTools.Str2Int(DV1[i]["PwrSet"]));
				}
				else
				{
					gridMrsBase2[i + 1, "PwrSet"] = PwrSet.GetDefaultName(dsPwrSet);
				}
			}
			pnlParent.Height = 128;
			splitter1.Visible = true;
			ultraLabel2.Text = "(" + gridMrsBase1[gridMrsBase1.Row, "pccesCode"].ToString() + ") 父項查詢結果";
		}
		else
		{
			MessageBox.Show(this, "查無父項資料!", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void Add_UsualItem()
	{
		Cursor = Cursors.WaitCursor;
		int iDoneRow = 0;
		int iSelCount = gridMrsBase1.SelectedItems;
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add("設為常用工項");
		string selectstr = "";
		ModifyDB StdCom = new ModifyDB("", aArr);
		if (iSelCount > 0)
		{
			for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
			{
				if (gridMrsBase1.Rows[i].Selected)
				{
					iDoneRow++;
					selectstr = "update Mrsbasea set show='1' where pubcode=" + gridMrsBase1[i, "PubCode"].ToString().Trim();
					StdCom.DBUpd(selectstr);
				}
				if (iDoneRow >= iSelCount)
				{
					break;
				}
			}
			MessageBox.Show(this, "已完成設定選定的 " + iSelCount + " 筆資料為[常用工項]。", "基本資料維護", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		aArr = null;
		StdCom = null;
		Cursor = Cursors.Default;
	}

	private void Cancel_UsualItem()
	{
		Cursor = Cursors.WaitCursor;
		int iDoneRow = 0;
		int iSelCount = gridMrsBase1.SelectedItems;
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add("取消常用工項");
		string selectstr = "";
		ModifyDB StdCom = new ModifyDB("", aArr);
		if (iSelCount > 0)
		{
			for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
			{
				if (gridMrsBase1.Rows[i].Selected)
				{
					iDoneRow++;
					selectstr = "update Mrsbasea set show='0' where pubcode=" + gridMrsBase1[i, "PubCode"].ToString().Trim();
					StdCom.DBUpd(selectstr);
				}
				if (iDoneRow >= iSelCount)
				{
					break;
				}
			}
			MessageBox.Show(this, "已完成設定選定的 " + iSelCount + " 筆資料取消[常用工項]。", "基本資料維護", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		aArr = null;
		StdCom = null;
		Cursor = Cursors.Default;
		Do_Filter();
	}

	private bool IsSelectionCanDeleted(ref string sFlag)
	{
		DeletionList.Clear();
		sFlag = "";
		bool RetV = true;
		int iSel = gridMrsBase1.SelectedItems;
		int iCount = 0;
		int iCannotCount = 0;
		DataTable DT_CannotDelete = new DataTable();
		DT_CannotDelete.Columns.Add("UserID", Type.GetType("System.String"));
		DT_CannotDelete.Columns.Add("UserName", Type.GetType("System.String"));
		DT_CannotDelete.Columns.Add("PccesCode", Type.GetType("System.String"));
		DT_CannotDelete.Columns.Add("CName", Type.GetType("System.String"));
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = F_UserID;
		for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
		{
			if (gridMrsBase1.Rows[i].Selected)
			{
				string pubCode = DV1[i - 1]["PubCode"].ToString().Trim();
				if (!DBCLS.MrsBase_CanEdit(pubCode, "", "MRS"))
				{
					iCannotCount++;
					DataRow DR = DBCLS.GetOccupieData(pubCode, "", "MRS");
					DataRow DR2 = DT_CannotDelete.NewRow();
					DR2["UserID"] = DR["UserID"];
					DR2["UserName"] = DR["UserName"];
					DR2["PccesCode"] = DR["PccesCode"];
					DR2["CName"] = DR["CName"];
					DT_CannotDelete.Rows.Add(DR2);
				}
				else
				{
					DeletionList.Add(pubCode);
				}
				iCount++;
				if (iCount >= iSel)
				{
					sFlag = "";
					RetV = true;
					break;
				}
			}
		}
		if (iCannotCount > 0)
		{
			FormMrsBase_DeleteMessage FM_MSG = new FormMrsBase_DeleteMessage();
			FM_MSG._iSel = iSel;
			if (DT_CannotDelete.Rows.Count == 1 && iSel == 1)
			{
				FM_MSG._MessageIcon = MessageBoxIcon.Exclamation;
				FM_MSG._Message = "您選取的此筆資料，有其他人正在編輯中，\n現在你不可以刪除。";
			}
			else
			{
				FM_MSG._MessageIcon = MessageBoxIcon.Question;
				FM_MSG._Message = "您要刪除的資料範圍中，有 " + iCannotCount + " 筆資料正被其他人編輯中，那些資料你現在不能刪除。\n是否繼續刪除可刪除的資料 ?";
			}
			FM_MSG._DTCannotDelete = DT_CannotDelete;
			if (FM_MSG.ShowDialog(this) == DialogResult.Yes)
			{
				sFlag = "HasShown";
				RetV = true;
			}
			else
			{
				RetV = false;
			}
			FM_MSG.Close();
			FM_MSG.Dispose();
			FM_MSG = null;
		}
		DBCLS = null;
		return RetV;
	}

	private bool IsUseByOtherAnalysis(int iiPubCode)
	{
		bool RetV = false;
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = F_UserID;
		string sSQLCmd = "Select A.pccesCode, A.cName from MrsBaseA A Left Join MrsBaseB B on A.pubCode = B.ParentCode  where B.pubCode = " + iiPubCode;
		string sMessage = "不能刪除此工項，尚有以下單價分析引用到此項目...\n\n";
		DataTable DT_UseByOther = DBCLS.GetUserDefine(sSQLCmd);
		if (DT_UseByOther.Rows.Count > 0)
		{
			for (int i = 0; i < DT_UseByOther.Rows.Count; i++)
			{
				string text = sMessage;
				sMessage = text + "【" + DT_UseByOther.Rows[i]["pccesCode"].ToString().Trim() + "】" + DT_UseByOther.Rows[i]["cName"].ToString() + "\n";
			}
			MessageBox.Show(this, sMessage, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			RetV = true;
		}
		DBCLS = null;
		return RetV;
	}

	private void Delete_MrsItems()
	{
		if (gridMrsBase1.Row <= 0)
		{
			return;
		}
		int iiPubCode = PubTools.Str2Int(gridMrsBase1[gridMrsBase1.Row, "pubCode"]);
		if (IsUseByOtherAnalysis(iiPubCode))
		{
			return;
		}
		if (PNL.Visible && RdoYes.Checked)
		{
			string warning = "刪除會將工項自基本資料庫移除，如要將工項取消歸屬請選取消歸屬，確定刪除？";
			DialogResult result = MessageBox.Show(this, warning, "基本資料庫", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
			if (result == DialogResult.No)
			{
				return;
			}
		}
		SetPopupMenuDisable();
		if (gridMrsBase1.Selection.r1 == 0 && gridMrsBase1.Selection.r1 == gridMrsBase1.Selection.r2)
		{
			return;
		}
		Cursor = Cursors.WaitCursor;
		int iSelCount = gridMrsBase1.SelectedItems;
		int iDoneCount = 0;
		int iPubCode = -1;
		string sDelFlag = "";
		bool IsDoDelete = IsSelectionCanDeleted(ref sDelFlag);
		if (IsDoDelete && sDelFlag == "")
		{
			string sMessage = "您正要刪除 " + iSelCount + " 筆資料\n其單價分析及相關資將被刪除，\n\n假如你按一下「是」，您將不能復原此刪除操作。\n您確定您要刪除這些資料嗎?";
			if (MessageBox.Show(this, sMessage, "基本資料庫", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
			{
				aArr.Clear();
				aArr.Add(F_UserID);
				aArr.Add("WinFORM 基本工料");
				dbMrsBase = new Archnowledge.Pcces.BUDClass.MrsBaseA(F_UserID, aArr);
				dbMrsBase.ps_srckind = "MRS";
				dbMrsBase.ps_projectcode = "";
				ultraStatusBar1.Panels[1].ProgressBarInfo.Value = 1;
				ultraStatusBar1.Panels[1].ProgressBarInfo.Minimum = 1;
				ultraStatusBar1.Panels[1].ProgressBarInfo.Maximum = iSelCount;
				FormProgress FM_Prog = new FormProgress();
				FM_Prog.Message = "資料刪除中, 請稍候...";
				FM_Prog._Max = iSelCount;
				FM_Prog._Min = 0;
				FM_Prog.Owner = this;
				FM_Prog.Show();
				Application.DoEvents();
				int ModValue = iSelCount / 100;
				for (int i = gridMrsBase1.Rows.Count - 1; i >= 1; i--)
				{
					if (ModValue > 0 && i % ModValue == 0)
					{
						FM_Prog.SetProgressValue(iDoneCount);
						Application.DoEvents();
						Cursor = Cursors.WaitCursor;
					}
					if (gridMrsBase1.Rows[i].Selected)
					{
						Thread.Sleep(50);
						iPubCode = int.Parse(gridMrsBase1[i, "PubCode"].ToString());
						dbMrsBase.DeleItem(iPubCode.ToString().Trim());
						iDoneCount++;
						gridMrsBase1.RemoveItem(i);
						for (int k = 0; k < DV1.Count; k++)
						{
							if (iPubCode == PubTools.Str2Int(DV1[k]["pubCode"]))
							{
								DV1.Delete(k);
							}
						}
					}
					if (iDoneCount >= iSelCount)
					{
						break;
					}
				}
				FM_Prog.SetProgressValue(iSelCount);
				Application.DoEvents();
				Cursor = Cursors.Default;
				FM_Prog.Hide();
				gridMrsBase1_AfterScroll(this, null);
				ultraStatusBar1.Panels[1].ProgressBarInfo.Value = 1;
			}
		}
		else if (IsDoDelete && sDelFlag == "HasShown")
		{
			aArr.Clear();
			aArr.Add(F_UserID);
			aArr.Add("WinFORM 基本工料");
			dbMrsBase = new Archnowledge.Pcces.BUDClass.MrsBaseA(F_UserID, aArr);
			dbMrsBase.ps_srckind = "MRS";
			dbMrsBase.ps_projectcode = "";
			ultraStatusBar1.Panels[1].ProgressBarInfo.Value = 1;
			ultraStatusBar1.Panels[1].ProgressBarInfo.Minimum = 1;
			ultraStatusBar1.Panels[1].ProgressBarInfo.Maximum = iSelCount;
			for (int i = DeletionList.Count - 1; i > 0; i--)
			{
				if (gridMrsBase1.Rows[i].Selected)
				{
					iPubCode = Convert.ToInt32(DeletionList[i]);
					dbMrsBase.DeleItem(iPubCode.ToString().Trim());
					iDoneCount++;
					gridMrsBase1.RemoveItem(i);
					ultraStatusBar1.Panels[1].ProgressBarInfo.Value = iDoneCount;
					Application.DoEvents();
				}
			}
			gridMrsBase1_AfterScroll(this, null);
			ultraStatusBar1.Panels[1].ProgressBarInfo.Value = 1;
		}
		SetPopupMenuEnable();
		gridMrsBase1.ClearUndo();
		UndoRedoStatus();
		ultraStatusBar1.Panels[1].ProgressBarInfo.Minimum = 0;
		ultraStatusBar1.Panels[1].ProgressBarInfo.Value = 0;
		ultraStatusBar1.Panels[0].Text = "資料筆數：" + (gridMrsBase1.Rows.Count - 1);
		Cursor = Cursors.Default;
	}

	private void Do_Filter()
	{
		if (ThreadFlag != 0 || FilterFlag != "")
		{
			return;
		}
		string sSearchText = ((TextBoxTool)ultraToolbarsManager1.Tools["Other_QueryText"]).Text.Trim();
		if (!CommonMethods.CheckValidString(sSearchText))
		{
			return;
		}
		string sWhere = "";
		string sValue = "";
		try
		{
			sValue = ((ComboBoxTool)ultraToolbarsManager1.Tools["Other_FilterType"]).Value.ToString();
		}
		catch
		{
		}
		switch (sValue)
		{
		case "0":
			sWhere = " And pccesCode Like '" + sSearchText + "%' ";
			break;
		case "1":
			sWhere = " And (cName Like '%" + sSearchText + "%' or surName like '%" + sSearchText + "%' or commonName like N'%" + sSearchText + "%') ";
			break;
		case "2":
			sWhere = " And extendCode Like '%" + sSearchText + "%' ";
			break;
		case "3":
			sWhere = " And commonName Like '%" + sSearchText + "%' ";
			break;
		}
		ultraToolbarsManager1.BeginUpdate();
		if (PNL.Visible && ultraTree2.SelectedNodes.Count > 0 && RdoYes.Checked)
		{
			BindGridWithRdoYes();
		}
		else
		{
			Th_BindGrid(sWhere);
		}
		if (!(sValue == "1") || !(sSearchText.Trim() != ""))
		{
			return;
		}
		bool IsFoundinCommonName = false;
		for (int i = 0; i < DV1.Count; i++)
		{
			if (DV1[i]["commonName"].ToString().IndexOf(sSearchText.Trim()) > -1)
			{
				IsFoundinCommonName = true;
				break;
			}
		}
		if (IsFoundinCommonName)
		{
			(ultraToolbarsManager1.Tools["mnuViewcommonName"] as StateButtonTool).Checked = true;
			gridMrsBase1.Cols["commonName"].Visible = true;
			MessageBox.Show(this, "您篩選的關鍵字，有出現在俗名內，正式名稱請以工項名稱為準", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			IsFoundinCommonName = false;
		}
	}

	private void BindGridWithRdoYes()
	{
		if (ultraTree2.SelectedNodes[0].Key != "" && ultraTree2.SelectedNodes[0].Nodes.Count == 0)
		{
			gridMrsBaseDataBind(flag: true, ultraTree2.SelectedNodes[0].Key);
		}
		else if (ultraTree2.SelectedNodes[0].Level == 2 && ultraTree2.SelectedNodes[0].Nodes.Count > 0)
		{
			string key = "(";
			foreach (UltraTreeNode node in ultraTree2.SelectedNodes[0].Nodes)
			{
				key += ((node.Index == ultraTree2.SelectedNodes[0].Nodes.Count - 1) ? ("'" + node.Key + "'") : ("'" + node.Key + "', "));
			}
			key += ")";
			gridMrsBaseDataBind(flag: true, key);
		}
		else
		{
			gridMrsBaseDataBind(flag: true, "");
		}
	}

	private void Do_AllItem()
	{
		((StateButtonTool)ultraToolbarsManager1.Tools["mnuView_ItemAll"]).Checked = true;
		string sWhere = "";
		((TextBoxTool)ultraToolbarsManager1.Tools["Other_QueryText"]).Text = "";
		Th_BindGrid(sWhere);
	}

	private void Do_PickClass()
	{
		FormBDGT_ItemClass FM_ITMSET_Class = new FormBDGT_ItemClass();
		FM_ITMSET_Class._UserID = F_UserID;
		FM_ITMSET_Class.Owner = this;
		FM_ITMSET_Class._status = "search1";
		if (FM_ITMSET_Class.ShowDialog() == DialogResult.OK)
		{
			((StateButtonTool)ultraToolbarsManager1.Tools["mnuView_PickClass"]).Checked = true;
			Th_BindGrid("");
		}
		FM_ITMSET_Class.Close();
		FM_ITMSET_Class.Dispose();
		FM_ITMSET_Class = null;
	}

	private void Do_PickClassItems()
	{
		for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
		{
			if (gridMrsBase1.Rows[i].Selected)
			{
				PickList.Add(gridMrsBase1[i, "PubCode"].ToString().Trim());
			}
		}
		FormBDGT_ItemClass FM_ITMSET_Class = new FormBDGT_ItemClass();
		FM_ITMSET_Class._UserID = F_UserID;
		FM_ITMSET_Class._PickList = PickList;
		FM_ITMSET_Class.Owner = this;
		FM_ITMSET_Class._status = "PickList";
		FM_ITMSET_Class.ShowDialog();
		FM_ITMSET_Class.Close();
		FM_ITMSET_Class.Dispose();
		FM_ITMSET_Class = null;
	}

	private string Do_PickType()
	{
		string RetV = " and 1=1 ";
		DataTable DTClass = new DataTable();
		string sNum = CommonMethods.IniReadValue(F_SettingPick, "PickType", "PickName");
		string strpubCode = "";
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("(UserDefind_Show) 顯示常用字串資料");
		if (sNum.Length > 0)
		{
			string ls_selectstr = "select Distinct A.* from mrsA A inner join MrsY B on A.pubcode=B.pubcode where B.numberCode in (" + sNum + ")";
			ModifyDB StdCom = new ModifyDB("", aArr);
			DTClass = StdCom.DBList(ls_selectstr);
			StdCom = null;
			if (DTClass.Rows.Count > 0)
			{
				for (int i = 0; i < DTClass.Rows.Count; i++)
				{
					strpubCode = strpubCode + DTClass.Rows[i]["pubCode"].ToString().Trim() + ",";
				}
			}
			if (strpubCode.Length > 0)
			{
				strpubCode = strpubCode.Substring(0, strpubCode.Length - 1);
			}
		}
		if (strpubCode.Length > 0)
		{
			return " pubCode in (" + strpubCode + ") ";
		}
		return " 1 = 0 ";
	}

	private void Do_changePrice(string sType)
	{
		string sQuest = "確定將所挑選工項的單價引用參考【價格高】單價嗎 ?";
		sQuest = ((sType == "H") ? "確定將所挑選工項的單價引用參考【價格高】單價嗎 ?" : ((!(sType == "M")) ? "確定將所挑選工項的單價引用參考【價格低】單價嗎 ?" : "確定將所挑選工項的單價引用參考【價格中】單價嗎 ?"));
		if (MessageBox.Show(this, sQuest, "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
		{
			return;
		}
		string sPccesCode = "";
		string sCost = "";
		string sCostKind = "";
		bool IsAnalysis = false;
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("更新價格高中低");
		Archnowledge.Pcces.BUDClass.MrsBaseA dbMrsBase = new Archnowledge.Pcces.BUDClass.MrsBaseA(F_UserID, aArr);
		dbMrsBase.ps_srckind = "MRS";
		dbMrsBase.ps_projectcode = "";
		if (gridMrsBase1.SelectedItems == 1)
		{
			sCostKind = gridMrsBase1[gridMrsBase1.Row, "CostKind"].ToString().Trim();
			IsAnalysis = (bool)gridMrsBase1[gridMrsBase1.Row, "Analysis"];
			if (!(sCostKind != "") && IsAnalysis)
			{
				return;
			}
			sPccesCode = gridMrsBase1[gridMrsBase1.Row, "PccesCode"].ToString().Trim();
			dbMrsBase.ps_pccesCode = sPccesCode;
			switch (sType)
			{
			case "H":
				if (gridMrsBase1[gridMrsBase1.Row, "priceHigh"] != null)
				{
					sCost = gridMrsBase1[gridMrsBase1.Row, "priceHigh"].ToString().Trim();
				}
				break;
			case "M":
				if (gridMrsBase1[gridMrsBase1.Row, "priceMedium"] != null)
				{
					sCost = gridMrsBase1[gridMrsBase1.Row, "priceMedium"].ToString().Trim();
				}
				break;
			case "L":
				if (gridMrsBase1[gridMrsBase1.Row, "priceLow"] != null)
				{
					sCost = gridMrsBase1[gridMrsBase1.Row, "priceLow"].ToString().Trim();
				}
				break;
			}
			dbMrsBase.ps_cost = sCost;
			dbMrsBase.UpdItem();
		}
		else
		{
			if (gridMrsBase1.SelectedItems <= 1)
			{
				return;
			}
			int iDoneRow = 0;
			int iSelCount = gridMrsBase1.SelectedItems;
			for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
			{
				if (gridMrsBase1.Rows[i].Selected)
				{
					iDoneRow++;
					sCostKind = gridMrsBase1[i, "CostKind"].ToString().Trim();
					IsAnalysis = (bool)gridMrsBase1[i, "Analysis"];
					if (sCostKind != "" || !IsAnalysis)
					{
						sPccesCode = gridMrsBase1[i, "PccesCode"].ToString().Trim();
						dbMrsBase.ps_pccesCode = sPccesCode;
						switch (sType)
						{
						case "H":
							if (gridMrsBase1[gridMrsBase1.Row, "priceHigh"] != null)
							{
								sCost = gridMrsBase1[gridMrsBase1.Row, "priceHigh"].ToString().Trim();
							}
							break;
						case "M":
							if (gridMrsBase1[gridMrsBase1.Row, "priceMedium"] != null)
							{
								sCost = gridMrsBase1[gridMrsBase1.Row, "priceMedium"].ToString().Trim();
							}
							break;
						case "L":
							if (gridMrsBase1[gridMrsBase1.Row, "priceLow"] != null)
							{
								sCost = gridMrsBase1[gridMrsBase1.Row, "priceLow"].ToString().Trim();
							}
							break;
						}
						dbMrsBase.ps_cost = sCost;
						dbMrsBase.UpdItem();
					}
				}
				if (iDoneRow >= iSelCount)
				{
					break;
				}
			}
		}
	}

	private void Clear_Bookmark()
	{
		((ComboBoxTool)ultraToolbarsManager1.Tools["Other_cboBookmarks"]).ValueList.ValueListItems.Clear();
	}

	private void Clear_Cost()
	{
		for (int i = 0; i < gridMrsBase1.Rows.Count; i++)
		{
			if (gridMrsBase1.Rows[i].Selected)
			{
				_CostStructure.DeleteItem(F_CostType, F_CostUID, gridMrsBase1.Rows[i]["pubCode"].ToString().Trim());
			}
		}
	}

	private void ProgressEventHandler(string Message, ref int Progress)
	{
		if (FM_INFO != null)
		{
			FM_INFO.SetValue(Message, Progress);
		}
	}

	private void FormCostInitial()
	{
		CostStructureTypePicker CostStructureTypePicker = new CostStructureTypePicker();
		DialogResult result = CostStructureTypePicker.ShowDialog();
		if (result == DialogResult.OK)
		{
			string Message = "按下「確定」，系統將在目前的資料庫中建立成本架構，但會保留原來工項之設定，包括該工項之單價分析項。";
			if (MessageBox.Show(Message, "成本架構建立", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
			{
				FM_INFO = new FormSys_G_Info1();
				FM_INFO.Show();
				FM_INFO.BringToFront();
				int Progress = 0;
				ExecResult ER = Archnowledge.Pcces.PccesMain.SysMaintain.CostStructureImport.Import(F_UserID, OnlyStructure: false, CostStructureTypePicker.SelectedTypes, FM_INFO, ProgressEventHandler, ref Progress);
				FM_INFO.Close();
				FM_INFO = null;
				if (ER.ReturnCode != 0)
				{
					MessageBox.Show("建立失敗：" + ER.Message);
				}
				else
				{
					ProcessCostStructure();
					MessageBox.Show("建立成功");
				}
				BindingFormDatas();
			}
		}
		CostStructureTypePicker.Dispose();
		CostStructureTypePicker = null;
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
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = F_UserID;
		DBCLS.ExecuteCommand("Delete From Bookmarks Where SrcKind='MRS'");
		OleDbCommand odCmd1 = new OleDbCommand();
		odCmd1.CommandText = "Insert Into Bookmarks(ProjectCode, SrcKind, Code, CName) values('MRS','MRS',?,?)";
		odCmd1.Parameters.Add("P1", OleDbType.VarWChar, 20);
		odCmd1.Parameters.Add("P2", OleDbType.VarWChar, 200);
		odCmd1.Parameters["P1"].Direction = ParameterDirection.Input;
		odCmd1.Parameters["P2"].Direction = ParameterDirection.Input;
		int iCount = ((ComboBoxTool)ultraToolbarsManager1.Tools["Other_cboBookmarks"]).ValueList.ValueListItems.Count;
		for (int i = 0; i < iCount; i++)
		{
			string ssBookmarkText = ((ComboBoxTool)ultraToolbarsManager1.Tools["Other_cboBookmarks"]).ValueList.ValueListItems[i].DisplayText;
			int iPos = ssBookmarkText.IndexOf(":");
			string sV1 = ssBookmarkText.Substring(0, iPos);
			string sV2 = ssBookmarkText.Substring(iPos + 1);
			odCmd1.Parameters["P1"].Value = sV1;
			odCmd1.Parameters["P2"].Value = sV2;
			DBCLS.ExecuteOleDbCommand(odCmd1);
		}
		DBCLS = null;
	}

	private void LoadBookmarkFromDB()
	{
		((ComboBoxTool)ultraToolbarsManager1.Tools["Other_cboBookmarks"]).ValueList.ValueListItems.Clear();
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = F_UserID;
		DataTable DTBookmarks = DBCLS.GetUserDefine("Select * From Bookmarks Where SrcKind='MRS' Order By Code");
		string sPccesCode = "";
		string sCName = "";
		string sBookMark = "";
		for (int i = 0; i < DTBookmarks.Rows.Count; i++)
		{
			sPccesCode = DTBookmarks.Rows[i]["Code"].ToString();
			sCName = DTBookmarks.Rows[i]["CName"].ToString();
			sBookMark = sPccesCode + ":" + sCName;
			((ComboBoxTool)ultraToolbarsManager1.Tools["Other_cboBookmarks"]).ValueList.ValueListItems.Add(sBookMark);
		}
		DBCLS = null;
	}

	private void Add_Bookmark()
	{
		Cursor = Cursors.WaitCursor;
		SetPopupMenuDisable();
		string sPccesCode = "";
		string sCName = "";
		string sUnit = "";
		string sBookMark = "";
		if (gridMrsBase1.SelectedItems == 1)
		{
			sPccesCode = gridMrsBase1[gridMrsBase1.Row, "PccesCode"].ToString().PadRight(20);
			sCName = gridMrsBase1[gridMrsBase1.Row, "CName"].ToString().PadRight(30);
			sUnit = gridMrsBase1[gridMrsBase1.Row, "UnitName"].ToString().PadLeft(4);
			sBookMark = sPccesCode + ":" + sCName;
			((ComboBoxTool)ultraToolbarsManager1.Tools["Other_cboBookmarks"]).ValueList.ValueListItems.Add(sBookMark);
		}
		else if (gridMrsBase1.SelectedItems > 1)
		{
			int iDoneRow = 0;
			int iSelCount = gridMrsBase1.SelectedItems;
			for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
			{
				if (gridMrsBase1.Rows[i].Selected)
				{
					iDoneRow++;
					sPccesCode = gridMrsBase1[i, "PccesCode"].ToString().PadRight(20);
					sCName = gridMrsBase1[i, "CName"].ToString().PadRight(30);
					sUnit = gridMrsBase1[i, "UnitName"].ToString().PadLeft(4);
					sBookMark = sPccesCode + ":" + sCName;
					((ComboBoxTool)ultraToolbarsManager1.Tools["Other_cboBookmarks"]).ValueList.ValueListItems.Add(sBookMark);
				}
				if (iDoneRow >= iSelCount)
				{
					break;
				}
			}
		}
		SetPopupMenuEnable();
		Cursor = Cursors.Default;
	}

	private void EventExtend_Find()
	{
		bool IsAlreadyExist = false;
		Form[] ownedForms = base.OwnedForms;
		foreach (Form frm in ownedForms)
		{
			if (frm is FormMrsBaseFind)
			{
				IsAlreadyExist = true;
				frm.Show();
				break;
			}
		}
		if (IsAlreadyExist)
		{
			return;
		}
		FormMrsBaseFind FM_Find = new FormMrsBaseFind();
		FM_Find.Owner = this;
		for (int j = 1; j < gridMrsBase1.Cols.Count; j++)
		{
			if (gridMrsBase1.Cols[j].Visible)
			{
				FM_Find.cboFind_Cols.Items.Add(gridMrsBase1.Cols[j].Name, gridMrsBase1.Cols[j].Caption);
			}
		}
		FM_Find.cboFind_Locway.SelectedIndex = 1;
		FM_Find.cboFind_Cols.SelectedIndex = 0;
		FM_Find.Show();
	}

	private void GetNewDataBySection(string sSection)
	{
		Cursor = Cursors.WaitCursor;
		DT1 = dbMrsBase.ChangCost(sSection);
		Application.DoEvents();
		Do_Filter();
		Cursor = Cursors.Default;
	}

	private void ExecuteBreakdownForm(object Sender)
	{
		FormMrsBaseBreakdown frmBD = new FormMrsBaseBreakdown();
		frmBD.PubCode = ((Sender == gridMrsBase1) ? ((int)gridMrsBase1[gridMrsBase1.Row, "PubCode"]) : ((int)gridMrsBase2[gridMrsBase2.Row, "PubCode"]));
		frmBD._ActionName = F_ActionName;
		frmBD._CallType = "MrsA";
		frmBD._UserID = F_UserID;
		frmBD._iCostDigital = F_MainCst;
		frmBD._IsUseIR = true;
		frmBD.ShowDialog(this);
		frmBD.Close();
		frmBD.Dispose();
		frmBD = null;
		int iPos = ((Sender == gridMrsBase1) ? gridMrsBase1.Row : gridMrsBase2.Row);
		int iPubCode = ((Sender == gridMrsBase1) ? ((int)gridMrsBase1[gridMrsBase1.Row, "PubCode"]) : ((int)gridMrsBase2[gridMrsBase2.Row, "PubCode"]));
		realpubCode = iPubCode;
		Do_Filter();
		if (Sender == gridMrsBase1)
		{
			gridMrsBase1.ClearUndo();
		}
		UndoRedoStatus();
		SetPopupMenuEnable();
	}

	public void ReLoad_OneRow(int iPubCode, int gridRow)
	{
		realpubCode = iPubCode;
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("基本資料庫--單價分析編輯完後--重新讀取該筆資料");
		dbMrsBase = new Archnowledge.Pcces.BUDClass.MrsBaseA(F_UserID, aArr);
		dbMrsBase.ps_srckind = "MRS";
		dbMrsBase.ps_projectcode = "";
		DataTable DT_OneRow = dbMrsBase.ListItem("pubCode =" + iPubCode);
		if (DT_OneRow.Rows.Count <= 0)
		{
			return;
		}
		if (DT_OneRow.Rows[0]["analysis"].ToString().Trim() == "1")
		{
			gridMrsBase1.SetCellImage(gridRow, gridMrsBase1.Cols["AnaImg"].SafeIndex, imageList2.Images[0]);
			gridMrsBase1[gridRow, "Analysis"] = true;
			gridMrsBase1.Rows[gridRow].Style = gridMrsBase1.Styles["AnalysisColor"];
		}
		else
		{
			gridMrsBase1[gridRow, "Analysis"] = false;
			gridMrsBase1.SetCellImage(gridRow, gridMrsBase1.Cols["AnaImg"].SafeIndex, null);
			gridMrsBase1.Rows[gridRow].Style = gridMrsBase1.Styles["Black"];
			string sItemClass = "";
			string sCostKind = "";
			sItemClass = ((DT_OneRow.Rows[0]["pccesCode"].ToString().Length > 0) ? DT_OneRow.Rows[0]["pccesCode"].ToString().Substring(0, 1) : "");
			sCostKind = ((DT_OneRow.Rows[0]["costKind"].ToString().Length > 0) ? DT_OneRow.Rows[0]["costKind"].ToString().Substring(0, 1) : "");
			if (sItemClass == "L" || sItemClass == "E" || sItemClass == "M")
			{
				gridMrsBase1.Rows[gridRow].Style = gridMrsBase1.Styles["LEMColor"];
			}
			else if (sItemClass == "W")
			{
				gridMrsBase1.Rows[gridRow].Style = gridMrsBase1.Styles["WColor"];
			}
			switch (sCostKind)
			{
			case "$":
				gridMrsBase1.Rows[gridRow].Style = gridMrsBase1.Styles["DollarColor"];
				break;
			case "%":
				gridMrsBase1.Rows[gridRow].Style = gridMrsBase1.Styles["PercentColor"];
				break;
			default:
				if (!(sCostKind == "#"))
				{
					break;
				}
				goto case "Z";
			case "Z":
				gridMrsBase1.Rows[gridRow].Style = gridMrsBase1.Styles["ZColor"];
				break;
			}
		}
		gridMrsBase1[gridRow, "CName"] = DT_OneRow.Rows[0]["cName"];
		gridMrsBase1[gridRow, "UnitName"] = DT_OneRow.Rows[0]["unitName"];
		gridMrsBase1[gridRow, "Rate"] = DT_OneRow.Rows[0]["rate"];
		gridMrsBase1[gridRow, "CostKind"] = DT_OneRow.Rows[0]["costKind"];
		gridMrsBase1[gridRow, "LRate"] = DT_OneRow.Rows[0]["lRate"];
		gridMrsBase1[gridRow, "ERate"] = DT_OneRow.Rows[0]["eRate"];
		gridMrsBase1[gridRow, "MRate"] = DT_OneRow.Rows[0]["mRate"];
		gridMrsBase1[gridRow, "WRate"] = DT_OneRow.Rows[0]["wRate"];
		gridMrsBase1[gridRow, "XNameC"] = DT_OneRow.Rows[0]["xNameC"];
		gridMrsBase1[gridRow, "Memo"] = DT_OneRow.Rows[0]["memo"];
		gridMrsBase1[gridRow, "PubCode"] = DT_OneRow.Rows[0]["pubCode"];
		gridMrsBase1[gridRow, "Cost"] = DT_OneRow.Rows[0]["cost"];
		gridMrsBase1[gridRow, "PccesCode"] = DT_OneRow.Rows[0]["pccesCode"];
		if (DV1 == null || DV1.Table == null || !DV1.Table.Columns.Contains("pubCode"))
		{
			return;
		}
		DataRow[] drs = DV1.Table.Select("pubCode=" + iPubCode);
		if (drs.Length <= 0)
		{
			return;
		}
		foreach (DataColumn dcDV1 in DV1.Table.Columns)
		{
			if (DT_OneRow.Columns[dcDV1.ColumnName] != null && (object)DT_OneRow.Columns[dcDV1.ColumnName].DataType == dcDV1.DataType)
			{
				drs[0][dcDV1] = DT_OneRow.Rows[0][dcDV1.ColumnName];
			}
		}
	}

	private void ExecuteEditForm(MrsBaseEditFormType sEditMode)
	{
		if (gridMrsBase1.Row <= 0 && sEditMode != MrsBaseEditFormType.New)
		{
			return;
		}
		if (gridMrsBase1.Row > 0 && (bool)gridMrsBase1[gridMrsBase1.Row, "Analysis"] && sEditMode == MrsBaseEditFormType.CopyToNew)
		{
			string ipubCode = gridMrsBase1[gridMrsBase1.Row, "pubCode"].ToString();
			Recost RC1 = new Recost(aArr);
			RC1.ps_IsProcessEvent = true;
			RC1.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
			RC1.ps_prjcode = "";
			RC1.ps_pubcode = ipubCode;
			string AppLocation = AppDomain.CurrentDomain.BaseDirectory;
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
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = F_UserID;
		try
		{
			if (gridMrsBase1.Row > -1 && !DBCLS.MrsBase_CanEdit(gridMrsBase1[gridMrsBase1.Row, "PubCode"].ToString().Trim(), "", "Mrs"))
			{
				DataRow DR = DBCLS.GetOccupieData(gridMrsBase1[gridMrsBase1.Row, "PubCode"].ToString().Trim(), "", "MRS");
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
				return;
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormMrsBase.cs--ExecuteEditForm" + ex.Message);
		}
		int iRowSetBack = 0;
		if (sEditMode == MrsBaseEditFormType.New)
		{
			iRowSetBack = 1;
		}
		else
		{
			iRowSetBack = gridMrsBase1.RowSel;
		}
		FormMrsBaseEdit FM_EDIT = new FormMrsBaseEdit();
		FM_EDIT._OnLineServerName = onlineList1._ServerName;
		FM_EDIT._UserID = F_UserID;
		FM_EDIT._EditMode = sEditMode;
		FM_EDIT._CallerFormName = base.Name;
		FM_EDIT._ActionName = PccesFormAction.MrsBase;
		F_NewAddItem_PccesCode = "";
		if (sEditMode != MrsBaseEditFormType.New)
		{
			FM_EDIT._PubCode = (int)gridMrsBase1[gridMrsBase1.Row, "PubCode"];
		}
		FM_EDIT._MainCost = F_MainCst.ToString();
		if (DialogResult.OK == FM_EDIT.ShowDialog(this))
		{
			switch (sEditMode)
			{
			case MrsBaseEditFormType.New:
			{
				string sSearchText = ((TextBoxTool)ultraToolbarsManager1.Tools["Other_QueryText"]).Text.Trim();
				if (sSearchText.Trim() != "")
				{
					Do_Filter();
					break;
				}
				realpubCode = PubTools.Str2Int(F_NewAddItem_PubCode);
				BindingFormDatas();
				if (F_NewAddItem_PccesCode.Trim() != "")
				{
					int iFind = gridMrsBase1.FindRow(F_NewAddItem_PccesCode, 1, gridMrsBase1.Cols["PccesCode"].SafeIndex, caseSensitive: false, fullMatch: false, wrap: false);
					if (iFind > -1)
					{
						gridMrsBase1.Row = iFind;
						gridMrsBase1.Select();
					}
				}
				break;
			}
			case MrsBaseEditFormType.CopyToNew:
			{
				realpubCode = PubTools.Str2Int(F_NewAddItem_PubCode);
				BindingFormDatas();
				int iFind = gridMrsBase1.FindRow(F_NewAddItem_PccesCode, 1, gridMrsBase1.Cols["PccesCode"].SafeIndex, caseSensitive: false, fullMatch: false, wrap: false);
				if (iFind > -1)
				{
					gridMrsBase1.Row = iFind;
					gridMrsBase1.Select();
				}
				break;
			}
			default:
				ReLoad_OneRow((int)gridMrsBase1[gridMrsBase1.Row, "PubCode"], gridMrsBase1.Row);
				break;
			}
		}
		FM_EDIT.Close();
		FM_EDIT.Dispose();
		FM_EDIT = null;
		gridMrsBase1.ClearUndo();
		UndoRedoStatus();
		DBCLS = null;
		gridMrsBase1.Sort(C1Sort, iSortCol);
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
			gridMrsBase1.Cols[i].AllowDragging = (bool)GridColsSquence[i, 8];
			gridMrsBase1.Cols[i].AllowResizing = (bool)GridColsSquence[i, 9];
		}
	}

	private void SetGrid2Column()
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

	private void Disable_LeftButtons()
	{
		functionButtons1.DisableButtons();
	}

	private void Enable_LeftButtons()
	{
		functionButtons1.EnableButtons();
	}

	private void gridMrsBaseDataBind(bool flag, string sKey)
	{
		gridMrsBase1.Cols["PccesCode"].TextAlign = TextAlignEnum.LeftCenter;
		ThreadFlag++;
		if (ThreadFlag != 1)
		{
			return;
		}
		FilterFlag = "BINDING";
		iCurrentRowIndex = 0;
		string AAppLocation = AppDomain.CurrentDomain.BaseDirectory;
		string LoadMethod = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "MrsBase", "LoadMethod");
		sBindFlag = "BINDING";
		if (Start == "")
		{
			Start = "binding";
		}
		FormSys_G_Info1 FM_INFO = new FormSys_G_Info1();
		if (LoadMethod.ToUpper() == "FAST")
		{
			FM_INFO.TopMost = true;
			FM_INFO._InfoString = "基本資料庫維護載入中，請稍候! ";
			FM_INFO.Owner = this;
			FM_INFO.Show();
			FM_INFO.BringToFront();
			Application.DoEvents();
			Cursor = Cursors.WaitCursor;
		}
		gridMrsBase1.Redraw = false;
		Cursor = Cursors.WaitCursor;
		Disable_LeftButtons();
		int iRowNow = gridMrsBase1.Row;
		Application.DoEvents();
		Cursor = Cursors.WaitCursor;
		if (ExtraCri != "[PARENT]")
		{
			string sWhere = ViewFilterGenerate() + ExtraCri;
			if (flag)
			{
				if (RdoYes.Checked)
				{
					sWhere = ((sWhere.Length <= 0) ? (" B.CostUID = '" + sKey + "'") : ((!sKey.Contains("(")) ? (sWhere + " and B.CostUID = '" + sKey + "'") : (sWhere + " and  B.CostUID in " + sKey)));
				}
				if (RdoNew.Checked && sKey.Length > 0)
				{
					sWhere = ((sWhere.Length <= 0) ? ("  A.pccesCode in (" + sKey + ")") : (sWhere + " and  A.pccesCode in (" + sKey + ")"));
				}
			}
			gridMrsBase1.Enabled = false;
			GetNewData(sWhere, flag);
			gridMrsBase1.Enabled = true;
			Application.DoEvents();
			Cursor = Cursors.WaitCursor;
		}
		(base.ParentForm as frmPccesMain).DisableMain();
		RememberColsProps();
		DV1 = DT1.DefaultView;
		if (iSortCol >= 1)
		{
			string sssColName = gridMrsBase1.Cols[iSortCol].Name;
			if (sssColName.ToUpper() == "AnaImg".ToUpper())
			{
				sssColName = "Analysis";
			}
			else if (sssColName.ToUpper() == "ITEMTYPE")
			{
				sssColName = "IsCommonItem";
			}
			DV1.Sort = sssColName + " " + sGridSort;
		}
		else
		{
			DV1.Sort = "pccesCode Asc";
			sGridSort = "ASC";
		}
		CellStyle CS0 = gridMrsBase1.Styles.Add("Black");
		CellStyle CS1 = gridMrsBase1.Styles.Add("AnalysisColor");
		CellStyle CS2 = gridMrsBase1.Styles.Add("LEMColor");
		CellStyle CS3 = gridMrsBase1.Styles.Add("WColor");
		CellStyle CS4 = gridMrsBase1.Styles.Add("ZColor");
		CellStyle CS5 = gridMrsBase1.Styles.Add("DollarColor");
		CellStyle CS6 = gridMrsBase1.Styles.Add("PercentColor");
		CellStyle CS7 = gridMrsBase1.Styles.Add("PriceColor");
		CellStyle CS_Cost1 = gridMrsBase1.Styles.Add("Cost1");
		CellStyle CS_Cost2 = gridMrsBase1.Styles.Add("Cost2");
		CS_Cost1.Format = ((F_MainCst > 0) ? ("###,###,###,##0." + "0".PadLeft(F_MainCst, '0')) : "###,###,###,##0");
		CS_Cost2.Format = ((F_AnaCst > 0) ? ("###,###,###,##0." + "0".PadLeft(F_AnaCst, '0')) : "###,###,###,##0");
		CS0.ForeColor = Color.Black;
		CS1.ForeColor = Color.Red;
		CS2.ForeColor = Color.Teal;
		CS3.ForeColor = Color.Purple;
		CS4.ForeColor = Color.Teal;
		CS4.BackColor = Color.LemonChiffon;
		CS5.ForeColor = Color.Green;
		CS6.ForeColor = Color.Blue;
		CS7.BackColor = Color.FromArgb(255, 255, 192);
		if (gridMrsBase1.Row >= 1)
		{
			gridMrsBase1.Row = 0;
		}
		gridMrsBase1.Rows.Count = 1;
		gridMrsBase1.Select();
		gridMrsBase1.Rows.Count = DV1.Count + 1;
		SetGridColumn();
		ultraStatusBar1.Panels[0].Text = "資料筆數：" + DV1.Count;
		gridMrsBase1.Redraw = true;
		int iTop = gridMrsBase1.TopRow;
		int iBtm = gridMrsBase1.BottomRow;
		gridMrsBase1.Redraw = false;
		ultraStatusBar1.Panels[1].ProgressBarInfo.Minimum = 0;
		ultraStatusBar1.Panels[1].ProgressBarInfo.Maximum = iBtm - iTop + 1;
		ultraStatusBar1.Panels[1].ProgressBarInfo.Value = 0;
		ultraStatusBar1.Panels[1].ProgressBarInfo.ShowLabel = true;
		string sItemClass = "";
		string sCostKind = "";
		for (int i = iTop - 1; i < iBtm; i++)
		{
			if (DV1.Count == 0)
			{
				break;
			}
			if (i > DV1.Count)
			{
				break;
			}
			iCurrentRowIndex = i + 1;
			sItemClass = ((DV1[i]["pccesCode"].ToString().Length > 0) ? DV1[i]["pccesCode"].ToString().Substring(0, 1) : "");
			sCostKind = ((DV1[i]["costKind"].ToString().Length > 0) ? DV1[i]["costKind"].ToString().Substring(0, 1) : "");
			CellRange RAccMode = gridMrsBase1.GetCellRange(i + 1, gridMrsBase1.Cols["PwrSet"].SafeIndex, i + 1, gridMrsBase1.Cols["PwrSet"].SafeIndex);
			RAccMode.Style = gridMrsBase1.Styles["ComboList"];
			gridMrsBase1[i + 1, "PccesCode"] = DV1[i]["pccesCode"].ToString().Trim();
			if (sItemClass == "L" || sItemClass == "E" || sItemClass == "M")
			{
				gridMrsBase1.Rows[i + 1].Style = gridMrsBase1.Styles["LEMColor"];
			}
			else if (sItemClass == "W")
			{
				gridMrsBase1.Rows[i + 1].Style = gridMrsBase1.Styles["WColor"];
			}
			switch (sCostKind)
			{
			case "$":
				gridMrsBase1.Rows[i + 1].Style = gridMrsBase1.Styles["DollarColor"];
				break;
			case "%":
				gridMrsBase1.Rows[i + 1].Style = gridMrsBase1.Styles["PercentColor"];
				break;
			default:
				if (!(sCostKind == "#"))
				{
					break;
				}
				goto case "Z";
			case "Z":
				gridMrsBase1.Rows[i + 1].Style = gridMrsBase1.Styles["ZColor"];
				break;
			}
			gridMrsBase1[i + 1, "CName"] = DV1[i]["cName"].ToString().Trim();
			if (DV1[i]["analysis"].ToString().Trim() == "1")
			{
				gridMrsBase1.SetCellImage(i + 1, gridMrsBase1.Cols["AnaImg"].SafeIndex, imageList2.Images[0]);
				gridMrsBase1[i + 1, "Analysis"] = true;
				gridMrsBase1.Rows[i + 1].Style = gridMrsBase1.Styles["AnalysisColor"];
			}
			else
			{
				gridMrsBase1[i + 1, "Analysis"] = false;
			}
			gridMrsBase1[i + 1, "UnitName"] = DV1[i]["unitName"].ToString().Trim();
			gridMrsBase1[i + 1, "Rate"] = DV1[i]["rate"];
			gridMrsBase1[i + 1, "CostKind"] = DV1[i]["costKind"].ToString().Trim();
			gridMrsBase1[i + 1, "LRate"] = DV1[i]["lRate"];
			gridMrsBase1[i + 1, "ERate"] = DV1[i]["eRate"];
			gridMrsBase1[i + 1, "MRate"] = DV1[i]["mRate"];
			gridMrsBase1[i + 1, "WRate"] = DV1[i]["wRate"];
			gridMrsBase1[i + 1, "XNameC"] = DV1[i]["xNameC"].ToString().Trim();
			gridMrsBase1[i + 1, "Memo"] = DV1[i]["memo"].ToString().Trim();
			gridMrsBase1[i + 1, "PubCode"] = DV1[i]["pubCode"];
			gridMrsBase1[i + 1, "Cost"] = DV1[i]["cost"];
			gridMrsBase1[i + 1, "Show"] = DV1[i]["show"].ToString().Trim();
			gridMrsBase1[i + 1, "EName"] = DV1[i]["eName"].ToString().Trim();
			gridMrsBase1[i + 1, "EUnit"] = DV1[i]["eUnit"].ToString().Trim();
			gridMrsBase1[i + 1, "surName"] = DV1[i]["surName"].ToString().Trim();
			gridMrsBase1[i + 1, "Account"] = DV1[i]["Account"].ToString().Trim();
			gridMrsBase1[i + 1, "Correct"] = DV1[i]["Correct"].ToString().Trim();
			gridMrsBase1[i + 1, "Confirm"] = DV1[i]["Confirm"].ToString().Trim();
			gridMrsBase1[i + 1, "CompareErrState"] = DV1[i]["CompareErrState"].ToString().Trim();
			gridMrsBase1[i + 1, "CorrectCName"] = DV1[i]["CorrectCName"].ToString().Trim();
			gridMrsBase1[i + 1, "CorrectUnitName"] = DV1[i]["CorrectUnitName"].ToString().Trim();
			if (DV1[i]["PwrSet"] != null)
			{
				gridMrsBase1[i + 1, "PwrSet"] = PwrSet.GetName(dsPwrSet, PubTools.Str2Int(DV1[i]["PwrSet"]));
			}
			else
			{
				gridMrsBase1[i + 1, "PwrSet"] = PwrSet.GetDefaultName(dsPwrSet);
			}
			gridMrsBase1[i + 1, "CheckDailyReportQty"] = DV1[i]["CheckDailyReportQty"].ToString() == "Y";
			gridMrsBase1[i + 1, "ItemType"] = ItemType.GetItemType(DV1[i]["IsCommonItem"].ToString());
			gridMrsBase1[i + 1, "commonName"] = DV1[i]["commonName"].ToString().Trim();
			if (DV1[i]["analysis"].ToString().Trim() == "1")
			{
				gridMrsBase1.SetCellStyle(i + 1, gridMrsBase1.Cols["Cost"].SafeIndex, CS_Cost1);
			}
			else
			{
				gridMrsBase1.SetCellStyle(i + 1, gridMrsBase1.Cols["Cost"].SafeIndex, CS_Cost2);
			}
			if ((iBtm - iTop + 1) / 5 > 0 && (i % ((iBtm - iTop + 1) / 5) == 0 || i == iBtm - iTop + 1 - 1))
			{
				try
				{
					ultraStatusBar1.Panels[1].ProgressBarInfo.Value = i + 1;
					Application.DoEvents();
					Cursor = Cursors.WaitCursor;
				}
				catch
				{
					ultraStatusBar1.Panels[1].ProgressBarInfo.Value = 0;
					ultraStatusBar1.Panels[1].ProgressBarInfo.ShowLabel = false;
				}
				Cursor = Cursors.AppStarting;
			}
		}
		for (int i = 0; i < DV1.Count; i++)
		{
			gridMrsBase1[i + 1, "pubCode"] = DV1[i]["pubCode"];
		}
		CS_Cost1 = null;
		CS_Cost2 = null;
		CS0 = null;
		CS1 = null;
		CS2 = null;
		CS3 = null;
		CS4 = null;
		CS5 = null;
		CS6 = null;
		CS7 = null;
		gridMrsBase1.Redraw = true;
		ultraStatusBar1.Panels[1].ProgressBarInfo.Value = 0;
		ultraStatusBar1.Panels[1].ProgressBarInfo.ShowLabel = false;
		gridMrsBase1.Col = 0;
		SetColsEditSymbol();
		(base.ParentForm as frmPccesMain).EnableMain();
		Enable_LeftButtons();
		if (DV1.Count == 0)
		{
			ultraToolbarsManager1.Tools["mnuWork_Edit"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuWork_Delete"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuCopy"].SharedProps.Enabled = false;
		}
		else
		{
			ultraToolbarsManager1.Tools["mnuWork_Edit"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuWork_Delete"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuCopy"].SharedProps.Enabled = true;
		}
		if (LoadMethod.ToUpper() == "FAST")
		{
			FM_INFO.Close();
			FM_INFO.Dispose();
			Application.DoEvents();
			Cursor = Cursors.WaitCursor;
		}
		int realnumber = iCount;
		if (realpubCode != 0)
		{
			for (int i = 1; i < iCount; i++)
			{
				if (DV1[i]["pubCode"].ToString() == realpubCode.ToString())
				{
					iCount = i;
					break;
				}
			}
			if (realnumber != iCount)
			{
				gridMrsBase1.Row = iCount + 1;
			}
		}
		FM_INFO.Close();
		FM_INFO = null;
		Cursor = Cursors.Default;
		DT1 = null;
		GC.Collect();
		if (Start == "binding")
		{
			ultraToolbarsManager1.Enabled = true;
			Cursor = Cursors.Default;
			Start = "";
		}
		if ((ultraToolbarsManager1.Tools["mnuViewsurName"] as StateButtonTool).Checked)
		{
			gridMrsBase1.Cols["surName"].Visible = true;
			gridMrsBase2.Cols["surName"].Visible = true;
		}
		else
		{
			gridMrsBase1.Cols["surName"].Visible = false;
			gridMrsBase2.Cols["surName"].Visible = false;
		}
		if ((ultraToolbarsManager1.Tools["mnuViewcommonName"] as StateButtonTool).Checked)
		{
			gridMrsBase1.Cols["commonName"].Visible = true;
			gridMrsBase2.Cols["commonName"].Visible = true;
		}
		else
		{
			gridMrsBase1.Cols["commonName"].Visible = false;
			gridMrsBase2.Cols["commonName"].Visible = false;
		}
		sBindFlag = "";
		FilterFlag = "";
		ThreadFlag = 0;
	}

	private string ViewFilterGenerate()
	{
		string RetV = "";
		int flag = 0;
		string sWORK = "";
		string sUsual = "";
		string sPickType = "";
		int CriteriaCount = 0;
		RetV += " 1=1 ";
		if ((ultraToolbarsManager1.Tools["mnuView_ItemBDOnly"] as StateButtonTool).Checked)
		{
			RetV += " And Analysis ='1' ";
		}
		if ((ultraToolbarsManager1.Tools["mnuView_ItemBDNone"] as StateButtonTool).Checked)
		{
			RetV += " And Analysis <>'1' ";
		}
		if ((ultraToolbarsManager1.Tools["mnuViewUnApprove"] as StateButtonTool).Checked)
		{
			RetV += " And (Post Is Null Or Post <> '1') ";
		}
		if ((ultraToolbarsManager1.Tools["mnuCalcErr"] as StateButtonTool).Checked)
		{
			Th_BindGrid(" And CalcError = '1' ");
			flag++;
		}
		if ((ultraToolbarsManager1.Tools["mnuchkViewWorK"] as StateButtonTool).Checked)
		{
			if (CriteriaCount > 0)
			{
				sWORK += " OR ";
			}
			sWORK += " SUBSTRING(pccesCode,1,1) not in ('L','E','M','W','l','e','m','w') ";
			CriteriaCount++;
		}
		if ((ultraToolbarsManager1.Tools["mnuchkViewLabor"] as StateButtonTool).Checked)
		{
			if (CriteriaCount > 0)
			{
				sWORK += " OR ";
			}
			sWORK += " SUBSTRING(pccesCode,1,1) ='L' OR SUBSTRING(pccesCode,1,1) ='l' ";
			CriteriaCount++;
		}
		if ((ultraToolbarsManager1.Tools["mnuchkViewEquip"] as StateButtonTool).Checked)
		{
			if (CriteriaCount > 0)
			{
				sWORK += " OR ";
			}
			sWORK += " SUBSTRING(pccesCode,1,1) ='E' OR SUBSTRING(pccesCode,1,1) ='e' ";
			CriteriaCount++;
		}
		if ((ultraToolbarsManager1.Tools["mnuchkViewMaterial"] as StateButtonTool).Checked)
		{
			if (CriteriaCount > 0)
			{
				sWORK += " OR ";
			}
			sWORK += " SUBSTRING(pccesCode,1,1) ='M' OR SUBSTRING(pccesCode,1,1) ='m' ";
			CriteriaCount++;
		}
		if ((ultraToolbarsManager1.Tools["mnuchkViewWaste"] as StateButtonTool).Checked)
		{
			if (CriteriaCount > 0)
			{
				sWORK += " OR ";
			}
			sWORK += " SUBSTRING(pccesCode,1,1) ='W' OR SUBSTRING(pccesCode,1,1) ='w' ";
			CriteriaCount++;
		}
		if ((ultraToolbarsManager1.Tools["mnuchkViewUsual"] as StateButtonTool).Checked)
		{
			sUsual += " Show ='1' ";
			CriteriaCount++;
		}
		if ((ultraToolbarsManager1.Tools["mnuView_PickClass"] as StateButtonTool).Checked)
		{
			sPickType += Do_PickType();
			CriteriaCount++;
		}
		if ((ultraToolbarsManager1.Tools["mnuPriceExist"] as StateButtonTool).Checked)
		{
			sUsual += " xNameC <>'' ";
			CriteriaCount++;
		}
		if ((ultraToolbarsManager1.Tools["mnuCorrectItems"] as StateButtonTool).Checked)
		{
			sUsual += " Correct ='是' ";
			CriteriaCount++;
		}
		if ((ultraToolbarsManager1.Tools["mnuIncorrect"] as StateButtonTool).Checked)
		{
			sUsual += " Correct <>'是' ";
			CriteriaCount++;
		}
		if (RetV == " 1=1 " && sWORK == "" && flag == 0 && sUsual == "" && sPickType == "")
		{
			(ultraToolbarsManager1.Tools["mnuView_ItemAll"] as StateButtonTool).Checked = true;
		}
		if (sWORK != "")
		{
			RetV = RetV + " AND (" + sWORK + ") ";
		}
		if (sUsual != "")
		{
			RetV = RetV + " AND (" + sUsual + ") ";
		}
		if (sPickType != "")
		{
			RetV = RetV + " AND (" + sPickType + ") ";
		}
		return RetV;
	}

	private void frmMrsBase_Activated(object sender, EventArgs e)
	{
		if (FORM_STATUS != "NORMAL")
		{
			SysUser oSysUser = new SysUser();
			lblUseDatabase.Text = "目前資料庫：" + oSysUser.GetSysUserDatabaseDesc(F_UserID);
			RememberColsProps();
			BindingFormDatas();
			UndoRedoStatus();
			(base.ParentForm as frmPccesMain).LeftPanel.Width = 0;
			ultraTree1.Nodes.Clear();
			UltraTreeNode node = ultraTree1.Nodes.Add("ROOT", "預算工項綱要");
			ultraTree1.Nodes[0].Expanded = true;
			UserDefined userDefined = new UserDefined();
			string companyDB = userDefined.GetPccesCompanyDB();
			SysUser sysUser = new SysUser();
			string databaseName = sysUser.GetSysUserDatabaseName(F_UserID);
			if (databaseName != companyDB)
			{
				Get_NodesData();
				Get_LeavesData();
				PopulateLevel1(node);
			}
			else
			{
				AutoNum autoNum = new AutoNum();
				DataSet dsTreeNodes = autoNum.GetAutoNum(companyDB);
				node.Nodes.Clear();
				PopulateLevel(ref dsTreeNodes, node);
			}
			ultraTree1.PerformAction(UltraTreeAction.FirstNode, shift: false, control: false);
			ultraTree1.PerformAction(UltraTreeAction.NextNode, shift: false, control: false);
			if (F_TreeMenu == "OPEN")
			{
				PNL_TREE.Width = 200;
			}
			else
			{
				PNL_TREE.Width = 0;
			}
			ProcessAddOn();
			FORM_STATUS = "NORMAL";
			GC.Collect();
		}
	}

	private void PopulateLevel(ref DataSet dsTreeNodes, UltraTreeNode currentTreeNode)
	{
		UltraTreeNode node = null;
		DataView dvChildren = dsTreeNodes.Tables["AutoNumA"].DefaultView;
		dvChildren.RowFilter = "[parent]='" + currentTreeNode.Key + "'";
		dvChildren.Sort = "itemCode ASC";
		foreach (DataRowView drv in dvChildren)
		{
			node = currentTreeNode.Nodes.Add(ArchConvert.Obj2String(drv["itemCode"]), ArchConvert.Obj2String(drv["cName"]));
			PopulateLevel(ref dsTreeNodes, node);
		}
	}

	private void gridMrsBase1_DoubleClick(object sender, EventArgs e)
	{
		int rowIndex = gridMrsBase1.MouseRow;
		int colIndex = gridMrsBase1.MouseCol;
		if (gridMrsBase1.Row > 0)
		{
			if (gridMrsBase1.Cols[colIndex].Name == "AnaImg" && (bool)gridMrsBase1[rowIndex, "Analysis"])
			{
				ExecuteBreakdownForm(gridMrsBase1);
				ReFillRow(gridMrsBase1.Row, gridMrsBase1[gridMrsBase1.Row, "PubCode"].ToString());
			}
			else if (!gridMrsBase1.Cols[gridMrsBase1.MouseCol].AllowEditing)
			{
				ExecuteEditForm(MrsBaseEditFormType.Edit);
			}
		}
	}

	private void ReFillRow(int iRow, string sPubCode)
	{
		GetNewData(" pubCode = " + sPubCode, flag: false);
		if (DT1.Rows.Count > 0)
		{
			gridMrsBase1[iRow, "PccesCode"] = DT1.Rows[0]["pccesCode"];
			gridMrsBase1[iRow, "CName"] = DT1.Rows[0]["cName"];
			gridMrsBase1[iRow, "Analysis"] = DT1.Rows[0]["analysis"];
			gridMrsBase1[iRow, "UnitName"] = DT1.Rows[0]["unitName"];
			gridMrsBase1[iRow, "Rate"] = DT1.Rows[0]["rate"];
			gridMrsBase1[iRow, "CostKind"] = DT1.Rows[0]["CostKind"];
			gridMrsBase1[iRow, "LRate"] = DT1.Rows[0]["lRate"];
			gridMrsBase1[iRow, "ERate"] = DT1.Rows[0]["eRate"];
			gridMrsBase1[iRow, "MRate"] = DT1.Rows[0]["mRate"];
			gridMrsBase1[iRow, "WRate"] = DT1.Rows[0]["wRate"];
			gridMrsBase1[iRow, "XNameC"] = DT1.Rows[0]["xNameC"];
			gridMrsBase1[iRow, "Memo"] = DT1.Rows[0]["memo"];
			gridMrsBase1[iRow, "PubCode"] = DT1.Rows[0]["pubCode"];
			gridMrsBase1[iRow, "Cost"] = DT1.Rows[0]["cost"];
		}
	}

	private void ultraToolbarsManager1_AfterToolCloseup(object sender, ToolDropdownEventArgs e)
	{
		string SearchText = string.Empty;
		if (e.Tool.Key == "Other_cboBookmarks")
		{
			if (((ComboBoxTool)ultraToolbarsManager1.Tools["Other_cboBookmarks"]).Value == null)
			{
				return;
			}
			SearchText = ((ComboBoxTool)ultraToolbarsManager1.Tools["Other_cboBookmarks"]).Value.ToString().Substring(0, 20).Trim();
			for (int i = 0; i < DV1.Count; i++)
			{
				if (DV1[i]["PccesCode"].ToString() == SearchText)
				{
					gridMrsBase1.Row = i + 1;
					break;
				}
			}
			gridMrsBase1.Select();
		}
		else if (e.Tool.Key == "Other_FilterType")
		{
			SendKeys.Send("{TAB}");
		}
	}

	private void ultraToolbarsManager1_ToolKeyPress(object sender, ToolKeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			if (e.Tool.Key == "Other_QueryText")
			{
				Do_Filter();
			}
			if (e.Tool.Key == "mnu_Cbo1")
			{
				string sSearchText = ((TextBoxTool)ultraToolbarsManager1.Tools["mnu_Cbo1"]).Text.Trim();
				Do_Find2(sSearchText, "", "");
			}
		}
	}

	private void ultraToolbarsManager1_ToolValueChanged(object sender, ToolEventArgs e)
	{
		if (e.Tool.Key == "mnu_Cbo1")
		{
			string sSearchText = ((TextBoxTool)ultraToolbarsManager1.Tools["mnu_Cbo1"]).Text.Trim();
			Do_Find2(sSearchText, "", "");
		}
	}

	private void gridMrsBase1_MouseDown(object sender, MouseEventArgs e)
	{
		int rowIndex = gridMrsBase1.MouseRow;
		int colIndex = gridMrsBase1.MouseCol;
		if (sBindFlag == "BINDING")
		{
			return;
		}
		if (e.Button == MouseButtons.Right)
		{
			if (rowIndex <= 0 || colIndex <= 0)
			{
				((PopupMenuTool)ultraToolbarsManager1.Tools["Popup1"]).Tools[0].SharedProps.Enabled = false;
				((PopupMenuTool)ultraToolbarsManager1.Tools["Popup1"]).Tools[1].SharedProps.Enabled = false;
				((PopupMenuTool)ultraToolbarsManager1.Tools["Popup1"]).Tools[2].SharedProps.Enabled = false;
				((PopupMenuTool)ultraToolbarsManager1.Tools["Popup1"]).Tools[3].SharedProps.Enabled = false;
				((PopupMenuTool)ultraToolbarsManager1.Tools["Popup1"]).Tools[4].SharedProps.Enabled = false;
				((PopupMenuTool)ultraToolbarsManager1.Tools["Popup1"]).Tools[5].SharedProps.Enabled = false;
				((PopupMenuTool)ultraToolbarsManager1.Tools["Popup1"]).Tools[6].SharedProps.Enabled = false;
				((PopupMenuTool)ultraToolbarsManager1.Tools["Popup1"]).Tools[7].SharedProps.Enabled = false;
				((PopupMenuTool)ultraToolbarsManager1.Tools["Popup1"]).Tools[8].SharedProps.Enabled = false;
				((PopupMenuTool)ultraToolbarsManager1.Tools["Popup1"]).Tools[9].SharedProps.Enabled = false;
				((PopupMenuTool)ultraToolbarsManager1.Tools["Popup1"]).Tools[10].SharedProps.Enabled = false;
				return;
			}
			((PopupMenuTool)ultraToolbarsManager1.Tools["Popup1"]).Tools[0].SharedProps.Enabled = true;
			((PopupMenuTool)ultraToolbarsManager1.Tools["Popup1"]).Tools[1].SharedProps.Enabled = true;
			((PopupMenuTool)ultraToolbarsManager1.Tools["Popup1"]).Tools[2].SharedProps.Enabled = true;
			((PopupMenuTool)ultraToolbarsManager1.Tools["Popup1"]).Tools[3].SharedProps.Enabled = true;
			((PopupMenuTool)ultraToolbarsManager1.Tools["Popup1"]).Tools[4].SharedProps.Enabled = true;
			((PopupMenuTool)ultraToolbarsManager1.Tools["Popup1"]).Tools[5].SharedProps.Enabled = true;
			((PopupMenuTool)ultraToolbarsManager1.Tools["Popup1"]).Tools[6].SharedProps.Enabled = true;
			((PopupMenuTool)ultraToolbarsManager1.Tools["Popup1"]).Tools[7].SharedProps.Enabled = true;
			((PopupMenuTool)ultraToolbarsManager1.Tools["Popup1"]).Tools[8].SharedProps.Enabled = true;
			((PopupMenuTool)ultraToolbarsManager1.Tools["Popup1"]).Tools[9].SharedProps.Enabled = true;
			((PopupMenuTool)ultraToolbarsManager1.Tools["Popup1"]).Tools[10].SharedProps.Enabled = true;
			((PopupMenuTool)ultraToolbarsManager1.Tools["Popup1"]).Tools[0].SharedProps.Enabled = true;
			((PopupMenuTool)ultraToolbarsManager1.Tools["Popup1"]).Tools[1].SharedProps.Enabled = true;
			((PopupMenuTool)ultraToolbarsManager1.Tools["Popup1"]).Tools[6].SharedProps.Enabled = true;
			try
			{
				if (rowIndex > 0 && !(bool)gridMrsBase1[rowIndex, "Analysis"])
				{
					((PopupMenuTool)ultraToolbarsManager1.Tools["Popup1"]).Tools[7].SharedProps.Enabled = false;
				}
				if (rowIndex > 0 && gridMrsBase1[rowIndex, "show"].ToString().Trim() == "1")
				{
					ultraToolbarsManager1.Tools["mnuTool_CancelUsualItem"].SharedProps.Enabled = true;
				}
				else
				{
					ultraToolbarsManager1.Tools["mnuTool_CancelUsualItem"].SharedProps.Enabled = false;
				}
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormMrsBase.cs--gridMrsBase1_MouseDown" + ex.Message);
			}
			if (rowIndex <= -1)
			{
				return;
			}
			if (gridMrsBase1.SelectedItems <= 1)
			{
				ArrayList aArr = new ArrayList();
				aArr.Add(F_UserID);
				aArr.Add("是否為常用工項");
				ModifyDB StdCom = new ModifyDB("", aArr);
				string selectstr = "select * from Mrsbasea where pubcode=" + gridMrsBase1[gridMrsBase1.Row, "PubCode"].ToString().Trim();
				DataTable dt = StdCom.DBList(selectstr);
				if (dt.Rows.Count > 0)
				{
					if (dt.Rows[0]["show"].ToString().Trim() == "1")
					{
						ultraToolbarsManager1.Tools["mnuTool_SetAsUsualItem"].SharedProps.Enabled = false;
						ultraToolbarsManager1.Tools["mnuTool_CancelUsualItem"].SharedProps.Enabled = true;
					}
					else
					{
						ultraToolbarsManager1.Tools["mnuTool_SetAsUsualItem"].SharedProps.Enabled = true;
						ultraToolbarsManager1.Tools["mnuTool_CancelUsualItem"].SharedProps.Enabled = false;
					}
					StdCom = null;
				}
				GridResetBack(rowIndex);
				if (!(bool)gridMrsBase1[gridMrsBase1.Row, "Analysis"])
				{
					ultraToolbarsManager1.Tools["PopComboCost"].SharedProps.Enabled = true;
					DataTable DT_Temp = dbMrsBase.List_Cost(gridMrsBase1[rowIndex, "PccesCode"].ToString());
					DataRow DR = DT_Temp.NewRow();
					DR["Cost"] = ((gridMrsBase1[rowIndex, "Cost"] != null) ? gridMrsBase1[rowIndex, "Cost"] : ((object)0));
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
					ultraToolbarsManager1.Tools["PopComboCost"].SharedProps.Enabled = false;
				}
			}
			else if (gridMrsBase1.SelectedItems > 1)
			{
				if (rowIndex < gridMrsBase1.Selection.r1 || rowIndex > gridMrsBase1.Selection.r2)
				{
					GridResetBack(rowIndex);
				}
				else
				{
					SetPopupMenuDisable(1);
				}
			}
		}
		else if (e.Button == MouseButtons.Left)
		{
			if (!IsShift && !IsAlt && !IsCtrl && gridMrsBase1.Cols[colIndex].Name != "PwrSet")
			{
				gridMrsBase1.Row = rowIndex;
			}
			if (gridMrsBase1.Row <= 0 || rowIndex <= 0 || colIndex <= 0)
			{
				return;
			}
			if (gridMrsBase1.Col != 0 && FORM_STATUS != "EDIT" && (!gridMrsBase1.Cols[colIndex].AllowEditing || (bool)gridMrsBase1[gridMrsBase1.Row, "Analysis"]))
			{
				gridMrsBase1.Col = 0;
			}
		}
		Size dragSize = new Size(gridMrsBase1.Width / 5, base.Height);
		dragBoxFromMouseDown = new Rectangle(new Point(e.X - dragSize.Width / 2, 0), dragSize);
	}

	private void gridMrsBase1_BeforeMouseDown(object sender, BeforeMouseDownEventArgs e)
	{
		if (gridMrsBase1.SelectedItems > 1 && e.Button == MouseButtons.Left && !IsCtrl && !IsShift)
		{
			gridMrsBase1.DoDragDrop(gridMrsBase1, DragDropEffects.Move);
		}
	}

	private void SetPopupMenuEnable()
	{
		((PopupMenuTool)ultraToolbarsManager1.Tools["Popup1"]).Tools[0].SharedProps.Enabled = true;
		((PopupMenuTool)ultraToolbarsManager1.Tools["Popup1"]).Tools[1].SharedProps.Enabled = true;
		((PopupMenuTool)ultraToolbarsManager1.Tools["Popup1"]).Tools[5].SharedProps.Enabled = true;
		ultraToolbarsManager1.Enabled = true;
	}

	private void SetPopupMenuDisable(int iType)
	{
		((PopupMenuTool)ultraToolbarsManager1.Tools["Popup1"]).Tools[0].SharedProps.Enabled = false;
		((PopupMenuTool)ultraToolbarsManager1.Tools["Popup1"]).Tools[1].SharedProps.Enabled = false;
		((PopupMenuTool)ultraToolbarsManager1.Tools["Popup1"]).Tools[5].SharedProps.Enabled = false;
	}

	private void SetPopupMenuDisable()
	{
		ultraToolbarsManager1.Enabled = false;
	}

	private void gridMrsBase1_MouseUp(object sender, MouseEventArgs e)
	{
		int rowIndex = gridMrsBase1.MouseRow;
		int colIndex = gridMrsBase1.MouseCol;
		if (!(sBindFlag == "BINDING") && e.Button == MouseButtons.Left && rowIndex == 0)
		{
			gridMrsBase1.Sort(SortFlags.UseColSort, colIndex);
		}
	}

	private void gridMrsBase1_StartEdit(object sender, RowColEventArgs e)
	{
		if (e.Col > 0 && e.Row > 0)
		{
			C1.Win.C1FlexGrid.Row GridRow = gridMrsBase1.Rows[e.Row];
			FORM_STATUS = "EDIT";
			if (e.Col == gridMrsBase1.Cols["Cost"].SafeIndex)
			{
				gridMrsBase1[e.Row, "Cost"] = string.Format("{0:N" + F_MainCst + "}", GridRow["Cost"]);
			}
			DBClass DBCLS = new DBClass();
			DBCLS._FS_UserID = F_UserID;
			DBCLS.MrsBase_Lock(GridRow["PubCode"].ToString().Trim(), "", "MRS");
			DBCLS = null;
			SetPopupMenuDisable();
		}
	}

	private void gridMrsBase1_AfterEdit(object sender, RowColEventArgs e)
	{
		if (e.Col <= 0 || e.Row <= 0)
		{
			return;
		}
		C1.Win.C1FlexGrid.Row GridRow = gridMrsBase1.Rows[e.Row];
		string ColumnName = gridMrsBase1.Cols[e.Col].Name;
		if (FORM_STATUS == "EDIT")
		{
			ArrayList aArr = new ArrayList();
			aArr.Add(F_UserID);
			aArr.Add("WinFORM 基本工料");
			Archnowledge.Pcces.BUDClass.MrsBaseA dbMrsBase = new Archnowledge.Pcces.BUDClass.MrsBaseA(F_UserID, aArr);
			dbMrsBase.ps_srckind = "MRS";
			dbMrsBase.ps_projectcode = "";
			dbMrsBase.ps_pccesCode = GridRow["PccesCode"].ToString();
			int iPubCode = PubTools.Str2Int(GridRow["pubCode"]);
			if (ColumnName == "Cost")
			{
				if (gridMrsBase1[e.Row, e.Col] == null)
				{
					dbMrsBase.ps_cost = "0";
				}
				else
				{
					dbMrsBase.ps_cost = gridMrsBase1[e.Row, e.Col].ToString();
				}
				for (int k = 0; k < DV1.Count; k++)
				{
					if (iPubCode == PubTools.Str2Int(DV1[k]["pubCode"]))
					{
						DV1[k]["Cost"] = dbMrsBase.ps_cost;
					}
				}
			}
			if (ColumnName == "Memo")
			{
				if (gridMrsBase1[e.Row, e.Col] == null)
				{
					dbMrsBase.ps_memo = "";
				}
				else
				{
					dbMrsBase.ps_memo = gridMrsBase1[e.Row, e.Col].ToString();
				}
				for (int k = 0; k < DV1.Count; k++)
				{
					if (iPubCode == PubTools.Str2Int(DV1[k]["pubCode"]))
					{
						DV1[k]["Memo"] = dbMrsBase.ps_memo;
					}
				}
			}
			if (ColumnName == "LRate")
			{
				if (gridMrsBase1[e.Row, e.Col] == null)
				{
					dbMrsBase.ps_lRate = "0";
				}
				else
				{
					dbMrsBase.ps_lRate = gridMrsBase1[e.Row, e.Col].ToString();
				}
				for (int k = 0; k < DV1.Count; k++)
				{
					if (iPubCode == PubTools.Str2Int(DV1[k]["pubCode"]))
					{
						DV1[k]["LRate"] = dbMrsBase.ps_lRate;
					}
				}
			}
			if (ColumnName == "ERate")
			{
				if (gridMrsBase1[e.Row, e.Col] == null)
				{
					dbMrsBase.ps_eRate = "0";
				}
				else
				{
					dbMrsBase.ps_eRate = gridMrsBase1[e.Row, e.Col].ToString();
				}
				for (int k = 0; k < DV1.Count; k++)
				{
					if (iPubCode == PubTools.Str2Int(DV1[k]["pubCode"]))
					{
						DV1[k]["ERate"] = dbMrsBase.ps_eRate;
					}
				}
			}
			if (ColumnName == "MRate")
			{
				if (gridMrsBase1[e.Row, e.Col] == null)
				{
					dbMrsBase.ps_mRate = "0";
				}
				else
				{
					dbMrsBase.ps_mRate = gridMrsBase1[e.Row, e.Col].ToString();
				}
				for (int k = 0; k < DV1.Count; k++)
				{
					if (iPubCode == PubTools.Str2Int(DV1[k]["pubCode"]))
					{
						DV1[k]["MRate"] = dbMrsBase.ps_mRate;
					}
				}
			}
			if (ColumnName == "WRate")
			{
				if (gridMrsBase1[e.Row, e.Col] == null)
				{
					dbMrsBase.ps_lRate = "0";
				}
				else
				{
					dbMrsBase.ps_lRate = gridMrsBase1[e.Row, e.Col].ToString();
				}
				for (int k = 0; k < DV1.Count; k++)
				{
					if (iPubCode == PubTools.Str2Int(DV1[k]["pubCode"]))
					{
						DV1[k]["WRate"] = dbMrsBase.ps_lRate;
					}
				}
			}
			if (ColumnName == "CName")
			{
				if (gridMrsBase1[e.Row, e.Col] == null)
				{
					dbMrsBase.ps_cName = "";
				}
				else
				{
					dbMrsBase.ps_cName = gridMrsBase1[e.Row, e.Col].ToString();
				}
				for (int k = 0; k < DV1.Count; k++)
				{
					if (iPubCode == PubTools.Str2Int(DV1[k]["pubCode"]))
					{
						DV1[k]["CName"] = dbMrsBase.ps_cName;
					}
				}
			}
			if (ColumnName == "unitName")
			{
				if (gridMrsBase1[e.Row, e.Col] == null)
				{
					dbMrsBase.ps_unitName = "";
				}
				else
				{
					dbMrsBase.ps_unitName = gridMrsBase1[e.Row, e.Col].ToString();
				}
				for (int k = 0; k < DV1.Count; k++)
				{
					if (iPubCode == PubTools.Str2Int(DV1[k]["pubCode"]))
					{
						DV1[k]["unitName"] = dbMrsBase.ps_unitName;
					}
				}
			}
			if (ColumnName == "CostKind")
			{
				if (gridMrsBase1[e.Row, e.Col] == null)
				{
					dbMrsBase.ps_costKind = "";
				}
				else
				{
					dbMrsBase.ps_costKind = gridMrsBase1[e.Row, e.Col].ToString();
				}
				for (int k = 0; k < DV1.Count; k++)
				{
					if (iPubCode == PubTools.Str2Int(DV1[k]["pubCode"]))
					{
						DV1[k]["CostKind"] = dbMrsBase.ps_costKind;
					}
				}
			}
			if (ColumnName == "EName")
			{
				if (gridMrsBase1[e.Row, e.Col] == null)
				{
					dbMrsBase.ps_eName = "";
				}
				else
				{
					dbMrsBase.ps_eName = gridMrsBase1[e.Row, e.Col].ToString();
				}
				for (int k = 0; k < DV1.Count; k++)
				{
					if (iPubCode == PubTools.Str2Int(DV1[k]["pubCode"]))
					{
						DV1[k]["EName"] = dbMrsBase.ps_eName;
					}
				}
			}
			if (ColumnName == "EUnit")
			{
				if (gridMrsBase1[e.Row, e.Col] == null)
				{
					dbMrsBase.ps_eUnit = "";
				}
				else
				{
					dbMrsBase.ps_eUnit = gridMrsBase1[e.Row, e.Col].ToString();
				}
				for (int k = 0; k < DV1.Count; k++)
				{
					if (iPubCode == PubTools.Str2Int(DV1[k]["pubCode"]))
					{
						DV1[k]["EUnit"] = dbMrsBase.ps_eUnit;
					}
				}
			}
			if (ColumnName == "surName")
			{
				if (gridMrsBase1[e.Row, e.Col] == null)
				{
					dbMrsBase.ps_surName = "";
				}
				else
				{
					dbMrsBase.ps_surName = gridMrsBase1[e.Row, e.Col].ToString();
				}
				for (int k = 0; k < DV1.Count; k++)
				{
					if (iPubCode == PubTools.Str2Int(DV1[k]["pubCode"]))
					{
						DV1[k]["surName"] = dbMrsBase.ps_surName;
					}
				}
			}
			if (ColumnName == "commonName")
			{
				if (gridMrsBase1[e.Row, e.Col] == null)
				{
					dbMrsBase.ps_commonName = "";
				}
				else
				{
					dbMrsBase.ps_commonName = gridMrsBase1[e.Row, e.Col].ToString();
				}
				for (int k = 0; k < DV1.Count; k++)
				{
					if (iPubCode == PubTools.Str2Int(DV1[k]["pubCode"]))
					{
						DV1[k]["commonName"] = dbMrsBase.ps_commonName;
					}
				}
			}
			if (ColumnName == "PwrSet")
			{
				dbMrsBase.ps_pwrSet = PwrSet.GetCode(dsPwrSet, ArchConvert.Obj2String(gridMrsBase1[e.Row, e.Col])).ToString();
				for (int k = 0; k < DV1.Count; k++)
				{
					if (iPubCode == PubTools.Str2Int(DV1[k]["pubCode"]))
					{
						DV1[k]["PwrSet"] = dbMrsBase.ps_pwrSet;
					}
				}
			}
			if (ColumnName == "Account")
			{
				if (gridMrsBase1[e.Row, e.Col] == null)
				{
					dbMrsBase.ps_account = "";
				}
				else
				{
					dbMrsBase.ps_account = gridMrsBase1[e.Row, e.Col].ToString();
				}
				for (int k = 0; k < DV1.Count; k++)
				{
					if (iPubCode == PubTools.Str2Int(DV1[k]["pubCode"]))
					{
						DV1[k]["Account"] = dbMrsBase.ps_account;
					}
				}
			}
			if (ColumnName == "CheckDailyReportQty")
			{
				if (gridMrsBase1[e.Row, e.Col] != null)
				{
					dbMrsBase.CheckDailyReportQty = (((bool)gridMrsBase1[e.Row, e.Col]) ? "Y" : "N");
				}
				for (int k = 0; k < DV1.Count; k++)
				{
					if (iPubCode == PubTools.Str2Int(DV1[k]["pubCode"]))
					{
						DV1[k]["CheckDailyReportQty"] = dbMrsBase.CheckDailyReportQty;
					}
				}
			}
			dbMrsBase.UpdItem();
			dbMrsBase = null;
			DBClass DBCLS = new DBClass();
			DBCLS._FS_UserID = F_UserID;
			DBCLS.MrsBase_UnLock(gridMrsBase1[e.Row, "PubCode"].ToString().Trim(), "", "MRS");
			DBCLS = null;
		}
		SetPopupMenuEnable();
	}

	private void gridMrsBase1_LeaveCell(object sender, EventArgs e)
	{
		if (!gridMrsBase1.Cols[gridMrsBase1.MouseCol].AllowEditing)
		{
			FORM_STATUS = "NORMAL";
			SetPopupMenuEnable();
		}
		UndoRedoStatus();
	}

	private void gridMrsBase1_SelChange(object sender, EventArgs e)
	{
		if (!gridMrsBase1.Cols[gridMrsBase1.MouseCol].AllowEditing)
		{
			FORM_STATUS = "NORMAL";
		}
		if (PNL.Visible && RdoYes.Checked)
		{
			ultraToolbarsManager1.Tools["mnuClearCost"].SharedProps.Enabled = true;
		}
		else
		{
			ultraToolbarsManager1.Tools["mnuClearCost"].SharedProps.Enabled = false;
		}
	}

	private void gridMrsBase1_MouseMove(object sender, MouseEventArgs e)
	{
		int rowIndex = gridMrsBase1.MouseRow;
		int colIndex = gridMrsBase1.MouseCol;
		if (sBindFlag == "BINDING" || rowIndex < 0 || gridMrsBase1[rowIndex, "Analysis"] == null)
		{
			return;
		}
		try
		{
			if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right && gridMrsBase1.Cols[colIndex].Name == "AnaImg" && rowIndex > 0 && (bool)gridMrsBase1[rowIndex, "Analysis"])
			{
				Cursor = Cursors.Hand;
			}
			if (PNL.Visible && (e.Button & MouseButtons.Left) == MouseButtons.Left && ((dragBoxFromMouseDown != Rectangle.Empty && !dragBoxFromMouseDown.Contains(e.X, e.Y)) || !gridMrsBase1.DisplayRectangle.Contains(e.X, e.Y)))
			{
				gridMrsBase1.DoDragDrop(gridMrsBase1, DragDropEffects.Move);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("gridMrsBase1_MouseMove Error:" + ex.Message);
		}
	}

	private void gridMrsBase1_Click(object sender, EventArgs e)
	{
		if (gridMrsBase1.Row == 0)
		{
			return;
		}
		UsedGrid = "DEFAULT";
		cboHisPrice.Visible = false;
		if (gridMrsBase1.MouseRow <= 0 || gridMrsBase1.MouseCol <= 0)
		{
			return;
		}
		int rowIndex = gridMrsBase1.MouseRow;
		int colIndex = gridMrsBase1.MouseCol;
		if (Cursor == Cursors.Hand && (bool)gridMrsBase1[rowIndex, "Analysis"] && gridMrsBase1.Cols[colIndex].Name == "AnaImg")
		{
			Frm.Hide();
			ExecuteBreakdownForm(gridMrsBase1);
		}
		if (gridMrsBase1[gridMrsBase1.Row, "PubCode"] == null)
		{
			return;
		}
		try
		{
			realpubCode = (int)gridMrsBase1[gridMrsBase1.Row, "PubCode"];
		}
		catch
		{
		}
	}

	private void gridMrsBase1_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.L || e.KeyCode == Keys.E || e.KeyCode == Keys.M || e.KeyCode == Keys.W)
		{
			int iFind = -1;
			int iStart = gridMrsBase1.Row + 1;
			int iColLookup = gridMrsBase1.Cols["PccesCode"].SafeIndex;
			iFind = gridMrsBase1.FindRow(e.KeyCode.ToString(), iStart, iColLookup, caseSensitive: false, fullMatch: false, wrap: false);
			if (iFind > -1)
			{
				gridMrsBase1.Row = iFind;
			}
		}
		if (e.Control && e.KeyCode == Keys.A)
		{
			for (int i = 1; i < gridMrsBase1.Rows.Count; i++)
			{
				gridMrsBase1.Rows[i].Selected = true;
			}
		}
		if (e.Alt)
		{
			IsAlt = true;
		}
		if (e.Control)
		{
			IsCtrl = true;
		}
		if (e.Shift)
		{
			IsShift = true;
		}
	}

	private void frmMrsBase_Resize(object sender, EventArgs e)
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

	private void gridMrsBase1_BeforeEdit(object sender, RowColEventArgs e)
	{
		if (FORM_STATUS == "BINDING")
		{
			return;
		}
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = F_UserID;
		if (gridMrsBase1[e.Row, "PubCode"] == null)
		{
			return;
		}
		if (!DBCLS.MrsBase_CanEdit(gridMrsBase1[e.Row, "PubCode"].ToString().Trim(), "", "MRS"))
		{
			e.Cancel = true;
			iAuthorityMSG_Count++;
			if (iAuthorityMSG_Count <= 1)
			{
				DataRow DR = DBCLS.GetOccupieData(gridMrsBase1[e.Row, "PubCode"].ToString().Trim(), "", "MRS");
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
			}
			iAuthorityMSG_Count = 0;
			gridMrsBase1.Col = 0;
		}
		if ((bool)gridMrsBase1[e.Row, "Analysis"])
		{
			string columnName = gridMrsBase1.Cols[e.Col].Name.ToUpper();
			if (columnName != "PWRSET" && columnName != "ACCOUNT" && columnName != "CHECKDAILYREPORTQTY")
			{
				e.Cancel = true;
			}
		}
		if (!gridMrsBase1.Cols[e.Col].AllowEditing)
		{
			e.Cancel = true;
		}
		if (gridMrsBase1.Cols[e.Col].Name.ToUpper() == "COST" && !DBClass.ChkAuthority(F_UserID, "F00200070001"))
		{
			iAuthorityMSG_Count++;
			if (iAuthorityMSG_Count <= 1)
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00200070001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			iAuthorityMSG_Count = 0;
			e.Cancel = true;
			gridMrsBase1.Col = 0;
		}
		else if (gridMrsBase1.Cols[e.Col].Name.ToUpper() == "LRATE" && !DBClass.ChkAuthority(F_UserID, "F00200070002"))
		{
			iAuthorityMSG_Count++;
			if (iAuthorityMSG_Count <= 1)
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00200070002") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			iAuthorityMSG_Count = 0;
			e.Cancel = true;
			gridMrsBase1.Col = 0;
		}
		else if (gridMrsBase1.Cols[e.Col].Name.ToUpper() == "ERATE" && !DBClass.ChkAuthority(F_UserID, "F00200070003"))
		{
			iAuthorityMSG_Count++;
			if (iAuthorityMSG_Count <= 1)
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00200070003") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			iAuthorityMSG_Count = 0;
			e.Cancel = true;
			gridMrsBase1.Col = 0;
		}
		else if (gridMrsBase1.Cols[e.Col].Name.ToUpper() == "MRATE" && !DBClass.ChkAuthority(F_UserID, "F00200070004"))
		{
			iAuthorityMSG_Count++;
			if (iAuthorityMSG_Count <= 1)
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00200070004") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			iAuthorityMSG_Count = 0;
			e.Cancel = true;
			gridMrsBase1.Col = 0;
		}
		else if (gridMrsBase1.Cols[e.Col].Name.ToUpper() == "WRATE" && !DBClass.ChkAuthority(F_UserID, "F00200070005"))
		{
			iAuthorityMSG_Count++;
			if (iAuthorityMSG_Count <= 1)
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00200070005") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			iAuthorityMSG_Count = 0;
			e.Cancel = true;
			gridMrsBase1.Col = 0;
		}
		else if (gridMrsBase1.Cols[e.Col].Name.ToUpper() == "MEMO" && !DBClass.ChkAuthority(F_UserID, "F00200070006"))
		{
			iAuthorityMSG_Count++;
			if (iAuthorityMSG_Count <= 1)
			{
				MessageBox.Show(this, DBClass.GetFuncName("F00200070006") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			iAuthorityMSG_Count = 0;
			e.Cancel = true;
			gridMrsBase1.Col = 0;
		}
		else
		{
			DBCLS = null;
		}
	}

	private void ultraToolbarsManager1_AfterToolActivate(object sender, ToolEventArgs e)
	{
		if (e.Tool.Key == "Other_QueryText" || e.Tool.Key == "mnu_Cbo1")
		{
			((ButtonTool)ultraToolbarsManager1.Tools["mnuWork_Delete"]).SharedProps.Shortcut = Shortcut.None;
		}
		else
		{
			((ButtonTool)ultraToolbarsManager1.Tools["mnuWork_Delete"]).SharedProps.Shortcut = Shortcut.Del;
		}
	}

	private void ultraToolbarsManager1_AfterToolDeactivate(object sender, ToolEventArgs e)
	{
		if (ultraToolbarsManager1 != null)
		{
			((ButtonTool)ultraToolbarsManager1.Tools["mnuWork_Delete"]).SharedProps.Shortcut = Shortcut.Del;
		}
	}

	private void SetColsEditSymbol()
	{
		for (int i = 1; i < gridMrsBase1.Cols.Count; i++)
		{
			if (gridMrsBase1.Cols[i].AllowEditing)
			{
				CellRange rg = gridMrsBase1.GetCellRange(0, i);
				rg.Style = gridMrsBase1.Styles["EditMode"];
				rg.Image = imageList2.Images[2];
			}
		}
	}

	private void gridMrsBase1_AfterScroll(object sender, RangeEventArgs e)
	{
		if (iSortCol >= 1)
		{
			string sssColName = gridMrsBase1.Cols[iSortCol].Name;
			if (sssColName.ToUpper() == "AnaImg".ToUpper())
			{
				sssColName = "Analysis";
			}
			else if (sssColName.ToUpper() == "ITEMTYPE")
			{
				sssColName = "IsCommonItem";
			}
			DV1.Sort = sssColName + " " + sGridSort;
		}
		CellStyle CS0 = gridMrsBase1.Styles.Add("Black");
		CellStyle CS1 = gridMrsBase1.Styles.Add("AnalysisColor");
		CellStyle CS2 = gridMrsBase1.Styles.Add("LEMColor");
		CellStyle CS3 = gridMrsBase1.Styles.Add("WColor");
		CellStyle CS4 = gridMrsBase1.Styles.Add("ZColor");
		CellStyle CS5 = gridMrsBase1.Styles.Add("DollarColor");
		CellStyle CS6 = gridMrsBase1.Styles.Add("PercentColor");
		CellStyle CS7 = gridMrsBase1.Styles.Add("PriceColor");
		CellStyle CS_Cost1 = gridMrsBase1.Styles.Add("Cost1");
		CellStyle CS_Cost2 = gridMrsBase1.Styles.Add("Cost2");
		CS_Cost1.Format = ((F_MainCst > 0) ? ("###,###,###,##0." + "0".PadLeft(F_MainCst, '0')) : "###,###,###,##0");
		CS_Cost2.Format = ((F_AnaCst > 0) ? ("###,###,###,##0." + "0".PadLeft(F_AnaCst, '0')) : "###,###,###,##0");
		CS0.ForeColor = Color.Black;
		CS1.ForeColor = Color.Red;
		CS2.ForeColor = Color.Teal;
		CS3.ForeColor = Color.Purple;
		CS4.ForeColor = Color.Teal;
		CS4.BackColor = Color.LemonChiffon;
		CS5.ForeColor = Color.Green;
		CS6.ForeColor = Color.Blue;
		CS7.BackColor = Color.FromArgb(255, 255, 192);
		gridMrsBase1.Redraw = false;
		int iTop = gridMrsBase1.TopRow;
		int iBtm = gridMrsBase1.BottomRow;
		string sItemClass = "";
		string sCostKind = "";
		if (iTop > 0)
		{
			for (int i = iTop - 1; i < iBtm; i++)
			{
				iCurrentRowIndex = i + 1;
				sItemClass = ((DV1[i]["pccesCode"].ToString().Length > 0) ? DV1[i]["pccesCode"].ToString().Substring(0, 1) : "");
				sCostKind = ((DV1[i]["costKind"].ToString().Length > 0) ? DV1[i]["costKind"].ToString().Substring(0, 1) : "");
				gridMrsBase1[i + 1, "PccesCode"] = DV1[i]["pccesCode"].ToString().Trim();
				CellRange RAccMode = gridMrsBase1.GetCellRange(i + 1, gridMrsBase1.Cols["PwrSet"].SafeIndex, i + 1, gridMrsBase1.Cols["PwrSet"].SafeIndex);
				RAccMode.Style = gridMrsBase1.Styles["ComboList"];
				if (sItemClass == "L" || sItemClass == "E" || sItemClass == "M")
				{
					gridMrsBase1.Rows[i + 1].Style = gridMrsBase1.Styles["LEMColor"];
				}
				else if (sItemClass == "W")
				{
					gridMrsBase1.Rows[i + 1].Style = gridMrsBase1.Styles["WColor"];
				}
				switch (sCostKind)
				{
				case "$":
					gridMrsBase1.Rows[i + 1].Style = gridMrsBase1.Styles["DollarColor"];
					break;
				case "%":
					gridMrsBase1.Rows[i + 1].Style = gridMrsBase1.Styles["PercentColor"];
					break;
				default:
					if (!(sCostKind == "#"))
					{
						break;
					}
					goto case "Z";
				case "Z":
					gridMrsBase1.Rows[i + 1].Style = gridMrsBase1.Styles["ZColor"];
					break;
				}
				gridMrsBase1[i + 1, "CName"] = DV1[i]["cName"].ToString().Trim();
				if (DV1[i]["analysis"].ToString().Trim() == "1")
				{
					gridMrsBase1[i + 1, "Analysis"] = true;
					gridMrsBase1.SetCellImage(i + 1, gridMrsBase1.Cols["AnaImg"].SafeIndex, imageList2.Images[0]);
					gridMrsBase1.Rows[i + 1].Style = gridMrsBase1.Styles["AnalysisColor"];
				}
				else
				{
					gridMrsBase1[i + 1, "Analysis"] = false;
				}
				gridMrsBase1[i + 1, "UnitName"] = DV1[i]["unitName"].ToString().Trim();
				gridMrsBase1[i + 1, "Rate"] = DV1[i]["rate"];
				gridMrsBase1[i + 1, "CostKind"] = DV1[i]["costKind"].ToString().Trim();
				gridMrsBase1[i + 1, "LRate"] = DV1[i]["lRate"];
				gridMrsBase1[i + 1, "ERate"] = DV1[i]["eRate"];
				gridMrsBase1[i + 1, "MRate"] = DV1[i]["mRate"];
				gridMrsBase1[i + 1, "WRate"] = DV1[i]["wRate"];
				gridMrsBase1[i + 1, "XNameC"] = DV1[i]["xNameC"].ToString().Trim();
				gridMrsBase1[i + 1, "Memo"] = DV1[i]["memo"].ToString().Trim();
				gridMrsBase1[i + 1, "PubCode"] = DV1[i]["pubCode"];
				gridMrsBase1[i + 1, "Cost"] = DV1[i]["cost"];
				gridMrsBase1[i + 1, "Show"] = DV1[i]["show"].ToString().Trim();
				gridMrsBase1[i + 1, "EName"] = DV1[i]["eName"].ToString().Trim();
				gridMrsBase1[i + 1, "EUnit"] = DV1[i]["eUnit"].ToString().Trim();
				gridMrsBase1[i + 1, "surName"] = DV1[i]["surName"].ToString().Trim();
				gridMrsBase1[i + 1, "Account"] = DV1[i]["Account"].ToString().Trim();
				gridMrsBase1[i + 1, "Correct"] = DV1[i]["Correct"].ToString().Trim();
				gridMrsBase1[i + 1, "Confirm"] = DV1[i]["Confirm"].ToString().Trim();
				gridMrsBase1[i + 1, "CompareErrState"] = DV1[i]["CompareErrState"].ToString().Trim();
				gridMrsBase1[i + 1, "CorrectCName"] = DV1[i]["CorrectCName"].ToString().Trim();
				gridMrsBase1[i + 1, "CorrectUnitName"] = DV1[i]["CorrectUnitName"].ToString().Trim();
				if (DV1[i]["PwrSet"] != DBNull.Value)
				{
					gridMrsBase1[i + 1, "PwrSet"] = PwrSet.GetName(dsPwrSet, PubTools.Str2Int(DV1[i]["PwrSet"]));
				}
				else
				{
					gridMrsBase1[i + 1, "PwrSet"] = PwrSet.GetDefaultName(dsPwrSet);
				}
				gridMrsBase1[i + 1, "CheckDailyReportQty"] = DV1[i]["CheckDailyReportQty"].ToString() == "Y";
				gridMrsBase1[i + 1, "ItemType"] = ItemType.GetItemType(DV1[i]["IsCommonItem"].ToString());
				gridMrsBase1[i + 1, "commonName"] = DV1[i]["commonName"].ToString().Trim();
				if (DV1[i]["analysis"].ToString().Trim() == "1")
				{
					gridMrsBase1.SetCellStyle(i + 1, gridMrsBase1.Cols["Cost"].SafeIndex, CS_Cost1);
				}
				else
				{
					gridMrsBase1.SetCellStyle(i + 1, gridMrsBase1.Cols["Cost"].SafeIndex, CS_Cost2);
				}
			}
		}
		CS_Cost1 = null;
		CS_Cost2 = null;
		CS0 = null;
		CS1 = null;
		CS2 = null;
		CS3 = null;
		CS4 = null;
		CS5 = null;
		CS6 = null;
		CS7 = null;
		GC.Collect();
		gridMrsBase1.Redraw = true;
	}

	private void gridMrsBase1_AfterRowColChange(object sender, RangeEventArgs e)
	{
		iAuthorityMSG_Count = 0;
	}

	private void gridMrsBase1_KeyUp(object sender, KeyEventArgs e)
	{
		IsShift = false;
		IsAlt = false;
		IsCtrl = false;
	}

	private void ultraToolbarsManager1_BeforeToolbarListDropdown(object sender, BeforeToolbarListDropdownEventArgs e)
	{
		e.Cancel = true;
	}

	private void ultraButton1_Click_1(object sender, EventArgs e)
	{
		PNL_TREE.Width = 0;
		if (!(ultraToolbarsManager1.Tools["mnuViewTree"] as StateButtonTool).Checked)
		{
			PNL_TREE.Width = 0;
		}
		(ultraToolbarsManager1.Tools["mnuViewTree"] as StateButtonTool).Checked = false;
		F_CostType = "";
	}

	private void Get_NodesData()
	{
		DBClass DBClass1 = new DBClass();
		DT_Nodes = DBClass1.GetAutoNumA1();
		DBClass1 = null;
	}

	private void Get_LeavesData()
	{
		DBClass DBClass1 = new DBClass();
		DT_Leaves = DBClass1.GetAutoNumA2();
		DT_Leaves12 = DBClass1.GetAutoNumA2_12();
		DBClass1 = null;
	}

	private void PopulateLevel1(UltraTreeNode treeNode)
	{
		treeNode.Nodes.Clear();
		UltraTreeNode node = null;
		foreach (DataRow row in DT_Nodes.Rows)
		{
			string itemCode = row["itemCode"] as string;
			string cName = row["itemCode"].ToString().Trim() + " " + row["cName"].ToString().Trim();
			node = treeNode.Nodes.Add(itemCode, cName.Trim());
			PopulateLevel2(node);
		}
	}

	private void PopulateLevel2(UltraTreeNode treeNode)
	{
		if (treeNode.Level <= 1)
		{
			treeNode.Nodes.Clear();
			string filterExp = " substring(itemCode,1," + treeNode.Key.Length + ") = '" + treeNode.Key + "'";
			string sortExp = " itemCode ASC ";
			DataRow[] rows = null;
			rows = ((treeNode.Key.Length != 1) ? DT_Leaves.Select(filterExp, sortExp) : DT_Leaves12.Select("Parent1='" + treeNode.Key + "'", sortExp));
			UltraTreeNode node = null;
			string itemCode = "";
			string cName = "";
			DataRow[] array = rows;
			foreach (DataRow row in array)
			{
				itemCode = row["itemCode"] as string;
				cName = row["itemCode"].ToString().Trim() + " " + row["cName"].ToString().Trim();
				string AliasKey = itemCode + "_" + Guid.NewGuid().ToString();
				node = treeNode.Nodes.Add(AliasKey, cName);
				node.Tag = new ExtendedNodeInfo(typeof(string), "itemCode");
			}
		}
	}

	private void ultraTree1_Click(object sender, EventArgs e)
	{
		if (ultraTree1.SelectedNodes.Count > 0)
		{
			Do_Find2(ultraTree1.SelectedNodes[0].Key, "", "");
		}
	}

	private void ultraButton2_Click(object sender, EventArgs e)
	{
		pnlParent.Height = 0;
		splitter1.Visible = false;
	}

	private void GotoSpecificRow()
	{
		int iFind = -1;
		iFind = gridMrsBase1.FindRow(gridMrsBase2[gridMrsBase2.Row, "PubCode"].ToString(), 1, gridMrsBase1.Cols["PubCode"].SafeIndex, caseSensitive: false, fullMatch: true, wrap: true);
		if (iFind > -1)
		{
			gridMrsBase1.Row = iFind;
			gridMrsBase1.Select();
		}
	}

	private void ExecuteChangeCode()
	{
		if (gridMrsBase1.Row > 0)
		{
			FormMrsBaseChgCode FMCHGCOD = new FormMrsBaseChgCode();
			FMCHGCOD._UserID = F_UserID;
			FMCHGCOD._PccesCode = gridMrsBase1[gridMrsBase1.Row, "PccesCode"].ToString();
			FMCHGCOD._PubCode = (int)gridMrsBase1[gridMrsBase1.Row, "PubCode"];
			FMCHGCOD._CName = gridMrsBase1[gridMrsBase1.Row, "CName"].ToString();
			FMCHGCOD._ActionName = F_ActionName;
			FMCHGCOD.Owner = this;
			if (FMCHGCOD.ShowDialog() == DialogResult.OK)
			{
				ultraStatusBar1.Panels[0].Text = "資料筆數：" + (gridMrsBase1.Rows.Count - 1);
				SetPopupMenuEnable();
			}
			FMCHGCOD.Close();
			FMCHGCOD.Dispose();
			FMCHGCOD = null;
		}
	}

	private void ProcessAddOn()
	{
		string AppLocation = AppDomain.CurrentDomain.BaseDirectory;
		string FileINI = AppLocation + "Addon.ini";
		ToolLists.Clear();
		ToolParam.Clear();
		for (int i = 1; i <= 20; i++)
		{
			string sValue = CommonMethods.IniReadValue(FileINI, "MRSBASE", "TOOL" + i);
			if (sValue.Trim() != "")
			{
				ToolLists.Add(sValue.Substring(0, sValue.IndexOf(",")));
				ToolParam.Add(sValue.Substring(sValue.IndexOf(",") + 1));
			}
		}
		if (ToolLists.Count > 0)
		{
			PopupMenuTool Addon = (PopupMenuTool)ultraToolbarsManager1.Tools["AddOn"];
			Addon.Tools.Clear();
			ultraToolbarsManager1.Tools["AddOn"].SharedProps.Visible = true;
			ultraToolbarsManager1.Tools["AddOn"].SharedProps.Enabled = true;
			for (int i = 0; i < ToolLists.Count; i++)
			{
				ButtonTool BT = new ButtonTool(ToolLists[i].ToString());
				BT.SharedProps.Tag = i;
				BT.SharedProps.Caption = ToolLists[i].ToString();
				BT.ToolClick += AddOnClick;
				try
				{
					ultraToolbarsManager1.Tools.Remove(BT);
				}
				catch (Exception ex)
				{
					CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormMrsBase.cs--ProcessAddOn" + ex.Message);
				}
				ultraToolbarsManager1.Tools.Add(BT);
				Addon.Tools.AddTool(ToolLists[i].ToString());
			}
		}
		else
		{
			ultraToolbarsManager1.Tools["AddOn"].SharedProps.Visible = false;
			ultraToolbarsManager1.Tools["AddOn"].SharedProps.Enabled = false;
		}
	}

	private void AddOnClick(object sender, ToolClickEventArgs e)
	{
		int iMenuIndex = (int)e.Tool.SharedProps.Tag;
		string sCmd = ToolParam[iMenuIndex].ToString();
		if (sCmd.Substring(0, 1) == "[" && sCmd.Substring(sCmd.Length - 1, 1) == "]")
		{
			string sMethodName = sCmd.Substring(1, sCmd.Length - 2);
			string text = sMethodName;
			if (text != null && text == "Synchronize")
			{
				Tool_Synchronize();
			}
		}
		SysUser oSysUser = new SysUser();
		string ssDBName = oSysUser.GetSysUserDatabaseName(F_UserID);
		if (sCmd.IndexOf("%PJ") > -1)
		{
			sCmd = sCmd.Replace("%PJ", "");
		}
		if (sCmd.IndexOf("%DB") > -1)
		{
			sCmd = sCmd.Replace("%DB", ssDBName);
		}
		if (sCmd.IndexOf("%UID") > -1)
		{
			sCmd = sCmd.Replace("%UID", F_UserID);
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

	private void Tool_Synchronize()
	{
		FormSynchronize FM_SYNC = new FormSynchronize();
		FM_SYNC._UserID = F_UserID;
		FM_SYNC.ShowDialog();
		FM_SYNC.Close();
		FM_SYNC.Dispose();
		FM_SYNC = null;
		Do_Filter();
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

	private void frmMrsBase_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Control && e.KeyCode == Keys.F1)
		{
			Frm.Show();
			Frm.BringToFront();
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
			CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormMrsBase.cs--DispatchString" + ex.Message);
			Console.Write(ex.Message);
		}
	}

	private void timer1_Tick(object sender, EventArgs e)
	{
		GC.Collect();
	}

	private void frmMrsBase_FormClosed(object sender, FormClosedEventArgs e)
	{
		DeletionList = null;
		ToolParam = null;
		ToolLists = null;
		GridInit = null;
		Frm = null;
		Cntrl1 = null;
		frmMrsBase_Fill_Panel = null;
		panel3 = null;
		imageList1 = null;
		LeftPanel = null;
		components = null;
		panel1 = null;
		panel5 = null;
		ultraStatusBar1 = null;
		imageList2 = null;
		functionButtons1 = null;
		onlineList1 = null;
		pnl_spliter = null;
		ultraToolbarsManager1 = null;
		_frmMrsBase_Toolbars_Dock_Area_Top = null;
		_frmMrsBase_Toolbars_Dock_Area_Bottom = null;
		_frmMrsBase_Toolbars_Dock_Area_Left = null;
		_frmMrsBase_Toolbars_Dock_Area_Right = null;
		iglst_splt_Btn = null;
		Btn_Splt = null;
		ssp_GridCaption = null;
		ssp_Top = null;
		ssp_Upper = null;
		ssp_Lower = null;
		ssp_Bottom = null;
		lblUseDatabase = null;
		PNL_TREE = null;
		ultraButton1 = null;
		ultraLabel1 = null;
		ultraTree1 = null;
		panel6 = null;
		panel7 = null;
		ultraLabel2 = null;
		ultraButton2 = null;
		gridMrsBase2 = null;
		imageList3 = null;
		pnlParent = null;
		dbMrsBase = null;
		aArr = null;
		gridMrsBase1 = null;
		DT1 = null;
		DV1 = null;
		GridColsSquence = null;
		Grid2ColsSquence = null;
		cboHisPrice = null;
		splitter1 = null;
		ultraButton9 = null;
		toolTip1 = null;
		saveFileDialog1 = null;
		DT_Nodes = null;
		DT_Leaves = null;
		GC.Collect();
	}

	private void cboHisPrice_AfterCloseUp(object sender, EventArgs e)
	{
		if (cboHisPrice.SelectedRow == null)
		{
			return;
		}
		double PickCost = -999999.0;
		string area = cboHisPrice.SelectedRow.Cells[4].Text;
		try
		{
			PickCost = Convert.ToDouble(cboHisPrice.Value);
			if (cboHisPrice.Value == null)
			{
				PickCost = -999999.0;
			}
		}
		catch
		{
			PickCost = -999999.0;
		}
		if (PickCost != -999999.0)
		{
			gridMrsBase1[gridMrsBase1.Row, "Cost"] = PickCost;
			gridMrsBase1[gridMrsBase1.Row, "xNameC"] = area;
			ArrayList aArr = new ArrayList();
			aArr.Add(F_UserID);
			aArr.Add("WinFORM 基本工料");
			Archnowledge.Pcces.BUDClass.MrsBaseA dbMrsBase = new Archnowledge.Pcces.BUDClass.MrsBaseA(F_UserID, aArr);
			dbMrsBase.ps_srckind = "MRS";
			dbMrsBase.ps_projectcode = "";
			dbMrsBase.ps_pccesCode = gridMrsBase1[gridMrsBase1.Row, "PccesCode"].ToString();
			dbMrsBase.ps_cost = PickCost.ToString();
			dbMrsBase.ps_xNameC = area;
			dbMrsBase.UpdItem();
			dbMrsBase = null;
		}
		gridMrsBase1.Select();
	}

	private void gridMrsBase1_BeforeSort(object sender, SortColEventArgs e)
	{
		if (iSortCol != e.Col)
		{
			sGridSort = "DESC";
		}
		else
		{
			sGridSort = ((sGridSort == "ASC") ? "DESC" : "ASC");
		}
		iSortCol = e.Col;
		e.Cancel = true;
		Th_BindGrid("");
	}

	public void Do_Find2(string sText, string ssFiledName, string sFindKind)
	{
		bool IsSearchName = false;
		string sField = "pccesCode";
		for (int ii = 0; ii < sText.Length; ii++)
		{
			if (sText[ii] > '\u007f')
			{
				IsSearchName = true;
				break;
			}
		}
		sField = ((!IsSearchName) ? "pccesCode" : "cName");
		string[] sFields = new string[6] { "cName", "unitName", "Memo", "eUnit", "surName", "commonName" };
		if (ssFiledName.Trim() != "")
		{
			sField = ssFiledName.Trim();
		}
		if (sField.ToUpper() == "PCCESCODE" && sFindKind == "")
		{
			sFindKind = "PREFIX";
		}
		int iStart = gridMrsBase1.Row + 1;
		if (iStart == 0)
		{
			iStart = 1;
		}
		int iFind = -1;
		if (gridMrsBase1.Rows.Count == 1)
		{
			return;
		}
		string flgBreak = "";
		for (int i = iStart - 1; i < gridMrsBase1.Rows.Count - 1; i++)
		{
			flgBreak = "";
			if (sFindKind.ToUpper() == "PREFIX")
			{
				if (DV1[i][sField] != null && DV1[i][sField].ToString() != "" && DV1[i][sField].ToString().Length >= sText.Length && DV1[i][sField].ToString().Substring(0, sText.Length) == sText)
				{
					iFind = i;
					break;
				}
			}
			else
			{
				for (int j = 0; j < sFields.Length; j++)
				{
					if (DV1[i][sFields[j]] != null && DV1[i][sFields[j]].ToString() != "" && DV1[i][sFields[j]].ToString().Length >= sText.Length && DV1[i][sFields[j]].ToString().IndexOf(sText) > -1)
					{
						iFind = i;
						flgBreak = "break";
						break;
					}
				}
			}
			if (flgBreak != "")
			{
				break;
			}
		}
		if (iFind > -1)
		{
			gridMrsBase1.Row = iFind + 1;
		}
	}

	private void ultraButton9_Click(object sender, EventArgs e)
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

	private void gridMrsBase2_MouseMove(object sender, MouseEventArgs e)
	{
		if (sBindFlag == "BINDING" || gridMrsBase2.MouseRow < 0)
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

	private void gridMrsBase2_Click(object sender, EventArgs e)
	{
		UsedGrid = "PARENT";
		cboHisPrice.Visible = false;
		if (gridMrsBase2.MouseRow > 0 && gridMrsBase2.MouseCol > 0)
		{
			int rowIndex = gridMrsBase2.MouseRow;
			if (Cursor == Cursors.Hand && (bool)gridMrsBase2[rowIndex, "Analysis"])
			{
				Frm.Hide();
				ExecuteBreakdownForm(gridMrsBase2);
			}
		}
	}

	private void gridMrsBase1_Resize(object sender, EventArgs e)
	{
		if (gridMrsBase1.Rows.Count > 1 && !(sBindFlag != ""))
		{
			gridMrsBase1_AfterScroll(sender, null);
			frmMrsBase_Resize(sender, e);
		}
	}

	private void BidbtnClose_Click(object sender, EventArgs e)
	{
		(base.ParentForm as frmPccesMain).LeftPanel.Width = 160;
		Close();
	}

	private void ProcessCostStructure()
	{
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add("是否有成本架構資料庫");
		ModifyDB StdCom = new ModifyDB("", aArr);
		string selectstr = "select * from CostStructureType ";
		DataTable dt = StdCom.DBList(selectstr);
		PopupMenuTool CostStructure = (PopupMenuTool)ultraToolbarsManager1.Tools["MenuCostStructure"];
		CostStructure.Tools.Clear();
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			ButtonTool BT = new ButtonTool(dt.Rows[i]["TypeName"].ToString());
			BT.SharedProps.Tag = dt.Rows[i]["TypeID"].ToString();
			BT.SharedProps.Caption = dt.Rows[i]["TypeName"].ToString();
			BT.ToolClick += CostStructureClick;
			try
			{
				ultraToolbarsManager1.Tools.Remove(BT);
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormMrsBase.cs--ProcessCostStructure" + ex.Message);
			}
			ultraToolbarsManager1.Tools.Add(BT);
			CostStructure.Tools.AddTool(dt.Rows[i]["TypeName"].ToString());
		}
	}

	private void CostStructureClick(object sender, ToolClickEventArgs e)
	{
		PNL.Visible = true;
		F_CostType = e.Tool.SharedProps.Tag.ToString();
		if (F_CostType != "")
		{
			PNL_TREE.Visible = false;
			PNL_COST.Width = 200;
		}
		else
		{
			PNL_TREE.Visible = true;
			PNL_COST.Width = 0;
		}
		lblCost.Text = e.Tool.SharedProps.Caption.ToString() + "成本架構";
		DataTable CostDT = _CostStructure.ListItem("", F_CostType);
		DataTable CostDTParnet = _CostStructure.ListItemParent(1, F_CostType);
		if (CostDTParnet.Rows.Count > 0)
		{
			ultraTree2.Nodes.Clear();
			UltraTreeNode node = ultraTree2.Nodes.Add("ROOT", e.Tool.SharedProps.Caption.ToString());
			node.Control.AllowDrop = true;
			node.Control.DragEnter += ultraTree2_DragEnter;
			for (int i = 0; i < CostDTParnet.Rows.Count; i++)
			{
				PopCostStructureTree(node, CostDT, CostDTParnet.Rows[i]["ParentUID"].ToString().Trim());
			}
		}
		BindingFormDatas();
	}

	private void PopCostStructureTree(UltraTreeNode treeNode, DataTable DT, string ParentUID)
	{
		treeNode.Nodes.Clear();
		string filterExp = " ParentUID = '" + ParentUID + "'";
		string sortExp = " iSort ASC ";
		DataRow[] rows = null;
		rows = DT.Select(filterExp, sortExp);
		UltraTreeNode node = null;
		string itemCode = "";
		string cName = "";
		DataRow[] array = rows;
		foreach (DataRow row in array)
		{
			itemCode = row["CostUID"] as string;
			cName = row["cName"].ToString().Trim();
			node = treeNode.Nodes.Add(itemCode, cName);
			node.Tag = new ExtendedNodeInfo(typeof(string), "CostUID");
			PopCostStructureTree(node, DT, itemCode);
		}
		if (treeNode.Level == 0 || (treeNode.Level == 1 && treeNode.Index == 0))
		{
			treeNode.Expanded = true;
		}
	}

	private void ultraButton3_Click(object sender, EventArgs e)
	{
		PNL_COST.Width = 0;
		F_CostType = "";
		PNL.Visible = false;
	}

	private void ultraTree2_DragEnter(object sender, DragEventArgs e)
	{
		if (DTDrag.Columns.IndexOf("pubCode") < 0)
		{
			DTDrag.Columns.Add("pubCode", Type.GetType("System.Int32"));
		}
		UltraTree theTree = sender as UltraTree;
		Point DropPoint = new Point(e.X, e.Y);
		UltraTreeNode theDropNode = theTree.GetNodeFromPoint(DropPoint);
		DTDrag.Clear();
		for (int i = 0; i < gridMrsBase1.Rows.Count; i++)
		{
			if (gridMrsBase1.Rows[i].Selected)
			{
				DataRow dr = DTDrag.NewRow();
				dr["pubCode"] = gridMrsBase1.Rows[i]["pubCode"];
				DTDrag.Rows.Add(dr);
			}
		}
	}

	private void ultraTree2_DragDrop(object sender, DragEventArgs e)
	{
		UltraTree theTree = sender as UltraTree;
		Point PointInTree = theTree.PointToClient(new Point(e.X, e.Y));
		UltraTreeNode theDropNode = theTree.GetNodeFromPoint(PointInTree);
		if (theDropNode == null || DTDrag.Rows.Count <= 0)
		{
			return;
		}
		for (int i = 0; i < DTDrag.Rows.Count; i++)
		{
			if (theDropNode.Nodes.Count == 0)
			{
				_CostStructure.InseItem(F_CostType, theDropNode.Key, DTDrag.Rows[i]["pubCode"].ToString().Trim());
			}
		}
	}

	private void ultraTree2_DragOver(object sender, DragEventArgs e)
	{
		UltraTree theTree = sender as UltraTree;
		Point PointInTree = theTree.PointToClient(new Point(e.X, e.Y));
		UltraTreeNode theDropNode = theTree.GetNodeFromPoint(PointInTree);
		if (theDropNode != null && theDropNode.Nodes.Count == 0)
		{
			e.Effect = DragDropEffects.Move;
		}
		else
		{
			e.Effect = DragDropEffects.None;
		}
	}

	private void ultraTree2_AfterSelect(object sender, SelectEventArgs e)
	{
		if (ultraTree2.SelectedNodes.Count > 0 && e.NewSelections[0].Nodes.Count == 0)
		{
			RdoYes.Checked = true;
			BindGridWithRdoYes();
		}
	}

	private void RdoNew_Click(object sender, EventArgs e)
	{
		if (DT_Auto.Columns.IndexOf("pccesCode") < 0)
		{
			DT_Auto.Columns.Add("pccesCode", Type.GetType("System.String"));
		}
		string sPccesCode = "";
		if (DT_Auto.Rows.Count > 0)
		{
			for (int i = 0; i < DT_Auto.Rows.Count; i++)
			{
				sPccesCode = sPccesCode + "'" + DT_Auto.Rows[i]["pccesCode"].ToString().Trim() + "',";
			}
			if (sPccesCode.Length > 0)
			{
				sPccesCode = sPccesCode.Substring(0, sPccesCode.Length - 1);
			}
		}
		gridMrsBaseDataBind(flag: true, sPccesCode);
	}

	private void RdoNo_Click(object sender, EventArgs e)
	{
		Do_Filter();
	}

	private void RdoAll_Click(object sender, EventArgs e)
	{
		Do_Filter();
	}

	private void RdoYes_Click(object sender, EventArgs e)
	{
		Do_Filter();
	}

	private void ultraTree2_Click(object sender, EventArgs e)
	{
		try
		{
			F_CostUID = ultraTree2.SelectedNodes[0].Key;
		}
		catch
		{
			F_CostUID = "";
		}
	}

	private void Import3652()
	{
		string s = "是否確認要匯入共通項目？";
		if (MessageBox.Show(s, "基本工項", MessageBoxButtons.YesNo) == DialogResult.No || importing3652)
		{
			return;
		}
		importing3652 = true;
		CodeFitter cf = new CodeFitter();
		DataTable DT1 = dbMrsBase.ListItem();
		Archnowledge.Pcces.DomainModule.MrsBase.MrsBaseA mrsBaseA = new Archnowledge.Pcces.DomainModule.MrsBase.MrsBaseA();
		DataTable DT_Dict = new DataTable();
		DT_Dict.Columns.Add("pccesCode_Comm", Type.GetType("System.String"));
		DT_Dict.Columns.Add("cName_Comm", Type.GetType("System.String"));
		DT_Dict.Columns.Add("unitName_Comm", Type.GetType("System.String"));
		DT_Dict.Columns.Add("Import", Type.GetType("System.Boolean"));
		DT_Dict.Columns.Add("pccesCode_Mrs", Type.GetType("System.String"));
		DT_Dict.Columns.Add("cName_Mrs", Type.GetType("System.String"));
		DT_Dict.Columns.Add("unitName_Mrs", Type.GetType("System.String"));
		DT_Dict.Columns.Add("Method", Type.GetType("System.String"));
		DT_Dict.Columns.Add("ChangeCode", Type.GetType("System.String"));
		DT_Dict.Columns.Add("Memo", Type.GetType("System.String"));
		DT_Dict.Columns.Add("commonName", Type.GetType("System.String"));
		for (int i = 0; i < cf.MrsBasesCount; i++)
		{
			DataRow DR = DT_Dict.NewRow();
			DR["pccesCode_Comm"] = cf.GetMrsBaseData(i).pccesCode;
			DR["cName_Comm"] = cf.GetMrsBaseData(i).cName;
			DR["unitName_Comm"] = cf.GetMrsBaseData(i).unitName;
			DR["Memo"] = cf.GetMrsBaseData(i).memo;
			DR["Import"] = true;
			DR["pccesCode_Mrs"] = "";
			DR["cName_Mrs"] = "";
			DR["unitName_Mrs"] = "";
			DR["Method"] = "";
			DR["commonName"] = cf.GetMrsBaseData(i).commonName;
			DT_Dict.Rows.Add(DR);
		}
		Cursor = Cursors.WaitCursor;
		FormProgress FM_Prog = new FormProgress();
		FM_Prog.Message = "資料整理中, 請稍候...";
		FM_Prog._Min = 0;
		FM_Prog._Max = DT1.Rows.Count;
		FM_Prog.Owner = this;
		FM_Prog.Show();
		Application.DoEvents();
		int ModValue = DT1.Rows.Count / 20;
		for (int i = 0; i < DT1.Rows.Count; i++)
		{
			if (ModValue > 0 && i % ModValue == 0)
			{
				FM_Prog.SetProgressValue(i);
				Application.DoEvents();
				Cursor = Cursors.WaitCursor;
			}
			bool IsFound = false;
			for (int j = 0; j < DT_Dict.Rows.Count; j++)
			{
				if (DT1.Rows[i]["pccesCode"].ToString().Trim() == DT_Dict.Rows[j]["pccesCode_Comm"].ToString().Trim())
				{
					IsFound = true;
					DT_Dict.Rows[j]["pccesCode_Mrs"] = DT1.Rows[i]["pccesCode"];
					DT_Dict.Rows[j]["cName_Mrs"] = DT1.Rows[i]["cName"];
					DT_Dict.Rows[j]["unitName_Mrs"] = DT1.Rows[i]["unitName"];
					DT_Dict.Rows[j]["commonName"] = DT1.Rows[i]["commonName"];
					DT_Dict.Rows[j]["Import"] = false;
					break;
				}
			}
			if (!IsFound)
			{
				DataRow DR = DT_Dict.NewRow();
				DR["pccesCode_Comm"] = "";
				DR["cName_Comm"] = "";
				DR["unitName_Comm"] = "";
				DR["Import"] = false;
				DR["pccesCode_Mrs"] = DT1.Rows[i]["pccesCode"];
				DR["cName_Mrs"] = DT1.Rows[i]["cName"];
				DR["unitName_Mrs"] = DT1.Rows[i]["unitName"];
				DR["Method"] = "";
				DR["commonName"] = DT1.Rows[i]["commonName"];
				DT_Dict.Rows.Add(DR);
			}
		}
		FM_Prog.SetProgressValue(DT1.Rows.Count);
		Application.DoEvents();
		Cursor = Cursors.Default;
		FM_Prog.Hide();
		FormCommMrsImport FM = new FormCommMrsImport();
		FM.Owner = this;
		FM._ImpData = DT_Dict;
		if (FM.ShowDialog() == DialogResult.OK)
		{
			Application.DoEvents();
			FM_Prog._Min = 0;
			FM_Prog.SetMax(FM._ImpData.Rows.Count);
			FM_Prog.SetMessage("共通資料匯入中，請稍候...");
			FM_Prog.SetProgressValue(0);
			FM_Prog.Show();
			string pccesCode = "";
			string cName2 = "";
			string unitName2 = "";
			string memo = "";
			string replaceType = "";
			string changeCode = "";
			string oldPccesCode = "";
			string commonName = "";
			Cursor = Cursors.WaitCursor;
			for (int i = 0; i < FM._ImpData.Rows.Count; i++)
			{
				if (ModValue > 0 && i % ModValue == 0)
				{
					FM_Prog.SetProgressValue(i);
					Application.DoEvents();
					Cursor = Cursors.WaitCursor;
				}
				if ((bool)FM._ImpData.Rows[i]["Import"])
				{
					pccesCode = FM._ImpData.Rows[i]["pccesCode_Comm"].ToString();
					cName2 = FM._ImpData.Rows[i]["cName_Comm"].ToString();
					unitName2 = FM._ImpData.Rows[i]["unitName_Comm"].ToString();
					replaceType = FM._ImpData.Rows[i]["Method"].ToString();
					memo = FM._ImpData.Rows[i]["Memo"].ToString();
					changeCode = FM._ImpData.Rows[i]["ChangeCode"].ToString();
					oldPccesCode = FM._ImpData.Rows[i]["pccesCode_Mrs"].ToString();
					commonName = FM._ImpData.Rows[i]["commonName"].ToString();
					if (changeCode != "" && oldPccesCode != changeCode)
					{
						replaceType = "Change";
					}
					try
					{
						mrsBaseA.replacePccesOverwrite(pccesCode, cName2, unitName2, replaceType, memo, changeCode, oldPccesCode, commonName);
					}
					catch
					{
						MessageBox.Show(pccesCode);
					}
				}
				else
				{
					pccesCode = FM._ImpData.Rows[i]["pccesCode_Comm"].ToString();
				}
			}
			FM_Prog.SetProgressValue(FM._ImpData.Rows.Count);
			FM_Prog.Close();
			FM_Prog.Dispose();
			FM_Prog = null;
			Cursor = Cursors.Default;
			s = "匯入共通項目完成";
			MessageBox.Show(s, "基本工項", MessageBoxButtons.OK);
		}
		FM.Dispose();
		FM = null;
		importing3652 = false;
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("", -1);
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.MrsBase.frmMrsBase));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTree.Override _override1 = new Infragistics.Win.UltraWinTree.Override();
		Infragistics.Win.Appearance appearance38 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance39 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance40 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTree.Override _override2 = new Infragistics.Win.UltraWinTree.Override();
		Infragistics.Win.Appearance appearance41 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance42 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance43 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance44 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance45 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance46 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance47 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance48 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel3 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance49 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel4 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance50 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance51 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance52 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance53 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance54 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance55 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance56 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.OptionSet optionSet1 = new Infragistics.Win.UltraWinToolbars.OptionSet("Prices");
		Infragistics.Win.UltraWinToolbars.OptionSet optionSet2 = new Infragistics.Win.UltraWinToolbars.OptionSet("FilterBD");
		Infragistics.Win.UltraWinToolbars.OptionSet optionSet3 = new Infragistics.Win.UltraWinToolbars.OptionSet("surName");
		Infragistics.Win.UltraWinToolbars.OptionSet optionSet4 = new Infragistics.Win.UltraWinToolbars.OptionSet("Category");
		Infragistics.Win.UltraWinToolbars.OptionSet optionSet5 = new Infragistics.Win.UltraWinToolbars.OptionSet("commonName");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("MainMenu");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool1 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("MenuFile");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool2 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("MenuEdit");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool3 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("MenuView");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool4 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("MenuWorkEdit");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool5 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("MenuTool");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool6 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("MenuCostStructure");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool7 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("AddOn");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool8 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("MenuHelp");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar2 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("EditTools");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuWork_New");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCopy");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuWork_Edit");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuWork_Delete");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool5 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuUndo");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool6 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuTool_DecSetting");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool7 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuTool_Recalculate");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar3 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("OtherTools");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool1 = new Infragistics.Win.UltraWinToolbars.LabelTool("Other_lblFilter");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool1 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("Other_FilterType");
		Infragistics.Win.UltraWinToolbars.TextBoxTool textBoxTool1 = new Infragistics.Win.UltraWinToolbars.TextBoxTool("Other_QueryText");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool8 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Other_FilterExecute");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool9 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Other_ShowAllItem");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool2 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnu_lblFind");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool2 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool10 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_Go");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool11 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCalculateCorrectness");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool1 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuCorrectItems", "FilterBD");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool2 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuIncorrect", "FilterBD");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool12 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCorrectCName");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar4 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("InfoTools");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool9 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("MenuImport");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool10 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("MenuExport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool13 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuTool_CostStructure");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar5 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("ViewMenu");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool3 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuView_ItemAll", "FilterBD");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool4 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuView_ItemBDOnly", "FilterBD");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool5 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuView_ItemBDNone", "FilterBD");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool6 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuCalcErr", "FilterBD");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool7 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuViewUnApprove", "FilterBD");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool8 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuchkViewWorK", "FilterBD");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool9 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuchkViewLabor", "FilterBD");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool10 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuchkViewEquip", "FilterBD");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool11 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuchkViewMaterial", "FilterBD");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool12 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuchkViewWaste", "FilterBD");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool13 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuchkViewUsual", "FilterBD");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool14 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuView_PickClass", "Category");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool14 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_PickClass");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool15 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExpNotCorrect");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool16 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExpAllCorrect");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar6 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("BookmarkTool");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool3 = new Infragistics.Win.UltraWinToolbars.LabelTool("Other_lblBookmark");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool3 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("Other_cboBookmarks");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool17 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuImport3652");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool18 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuImportBasic");
		Infragistics.Win.Appearance appearance57 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance58 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance59 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool11 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("MenuFile");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool12 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("MenuImport");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool13 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("MenuExport");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool19 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuFile_Exit");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool14 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("MenuEdit");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool20 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuUndo");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool21 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MenuEdit_SelAll");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool22 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuWork_Delete");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool23 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuFind");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool15 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("MenuView");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool15 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuView_ItemAll", "FilterBD");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool16 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuView_ItemBDOnly", "FilterBD");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool17 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuView_ItemBDNone", "FilterBD");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool18 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuCalcErr", "FilterBD");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool19 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuViewUnApprove", "FilterBD");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool16 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("ShowItemType");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool20 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuViewTree", "");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool17 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("ShowSurName");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool18 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("MenuWorkEdit");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool24 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuWork_New");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool25 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCopy");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool26 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuWork_Edit");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool19 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Work_Use");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool20 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("MenuTool");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool27 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuTool_DecSetting");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool28 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuTool_Recalculate");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool29 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuTool_AddBookmark");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool21 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuTool_ClearBookmark");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool30 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuAutoNum");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool31 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuTool_FindParent");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool32 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuTool_SetAsUsualItem");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool33 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuTool_CancelUsualItem");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool34 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuClearDB");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool35 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuPickFromOtherDB");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool36 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuChangeCode");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool37 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuToolApprove");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool38 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuConCost");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool39 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuImport3652");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool40 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuImportBasic");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool22 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("MenuHelp");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool41 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuPccesAbout");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool42 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuUpdateList");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool23 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("MenuCostStructure");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool43 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuFile_Save");
		Infragistics.Win.Appearance appearance60 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool44 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuFile_Exit");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool45 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuFile_SaveAs");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool24 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("MenuImport");
		Infragistics.Win.Appearance appearance61 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool46 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuImpExcel");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool47 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuImpXML");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool25 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("MenuExport");
		Infragistics.Win.Appearance appearance62 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool48 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExpExcel");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool49 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExpXML");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool50 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuImpExcel");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool51 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuImpXML");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool52 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExpExcel");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool53 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExpXML");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool54 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuUndo");
		Infragistics.Win.Appearance appearance63 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool55 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuRedo");
		Infragistics.Win.Appearance appearance64 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool56 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCut");
		Infragistics.Win.Appearance appearance65 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool57 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCopy");
		Infragistics.Win.Appearance appearance66 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool58 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuPaste");
		Infragistics.Win.Appearance appearance67 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool59 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuFind");
		Infragistics.Win.Appearance appearance68 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool21 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuView_ItemAll", "FilterBD");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool22 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuView_ItemBDOnly", "FilterBD");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool23 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuView_ItemBDNone", "FilterBD");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool26 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("ShowItemType");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool24 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuchkViewWorK", "FilterBD");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool25 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuchkViewLabor", "FilterBD");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool26 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuchkViewEquip", "FilterBD");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool27 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuchkViewMaterial", "FilterBD");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool28 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuchkViewWaste", "FilterBD");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool29 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuchkViewWorK", "FilterBD");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool30 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuchkViewLabor", "FilterBD");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool31 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuchkViewEquip", "FilterBD");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool32 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuchkViewMaterial", "FilterBD");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool33 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuchkViewWaste", "FilterBD");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool34 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuchkViewUsual", "FilterBD");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool27 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Work_Use");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool60 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuWorkUse_OtherMrs");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool61 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuWorkUse_Investigation");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool62 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuWorkUse_OtherMrs");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool63 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuWorkUse_Investigation");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool64 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuTool_DecSetting");
		Infragistics.Win.Appearance appearance69 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool65 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuTool_Recalculate");
		Infragistics.Win.Appearance appearance70 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool28 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("SwitchPrice");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool35 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuSwitchPrice_All", "Prices");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool36 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuSwitchPrice_Nor", "Prices");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool37 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuSwitchPrice_Mid", "Prices");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool38 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuSwitchPrice_Sou", "Prices");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool39 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuSwitchPrice_Est", "Prices");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool40 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuSwitchPrice_Out", "Prices");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool66 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuTool_AddBookmark");
		Infragistics.Win.Appearance appearance71 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool67 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuTool_FindParent");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool68 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuTool_SetAsUsualItem");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool41 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuSwitchPrice_All", "Prices");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool42 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuSwitchPrice_Nor", "Prices");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool43 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuSwitchPrice_Mid", "Prices");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool44 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuSwitchPrice_Sou", "Prices");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool45 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuSwitchPrice_Est", "Prices");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool46 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuSwitchPrice_Out", "Prices");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool69 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuPccesHelp");
		Infragistics.Win.Appearance appearance72 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool70 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuPccesAbout");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool4 = new Infragistics.Win.UltraWinToolbars.LabelTool("Other_lblBreakdown");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool4 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("Other_ViewItemBreakCombo");
		Infragistics.Win.ValueList valueList1 = new Infragistics.Win.ValueList(0);
		Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool5 = new Infragistics.Win.UltraWinToolbars.LabelTool("Other_lblFilter");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool5 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("Other_FilterType");
		Infragistics.Win.ValueList valueList2 = new Infragistics.Win.ValueList(0);
		Infragistics.Win.ValueListItem valueListItem4 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem5 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem6 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem7 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.UltraWinToolbars.TextBoxTool textBoxTool2 = new Infragistics.Win.UltraWinToolbars.TextBoxTool("Other_QueryText");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool71 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Other_FilterExecute");
		Infragistics.Win.Appearance appearance73 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool6 = new Infragistics.Win.UltraWinToolbars.LabelTool("Other_lblBookmark");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool6 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("Other_cboBookmarks");
		Infragistics.Win.ValueList valueList3 = new Infragistics.Win.ValueList(0);
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool7 = new Infragistics.Win.UltraWinToolbars.LabelTool("Info_lblCurrDB");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool8 = new Infragistics.Win.UltraWinToolbars.LabelTool("Info_lblCurrDBInfo");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool29 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("MenuViewItems");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool72 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuWork_New");
		Infragistics.Win.Appearance appearance74 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool73 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuWork_Edit");
		Infragistics.Win.Appearance appearance75 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool74 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuWork_Delete");
		Infragistics.Win.Appearance appearance76 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool30 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Popup1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool75 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCopy");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool76 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuWork_Edit");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool77 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuWork_Delete");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool78 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuTool_AddBookmark");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool79 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuTool_SetAsUsualItem");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool80 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuTool_CancelUsualItem");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool81 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuTool_FindParent");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool82 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MenuViewAnalysis");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool83 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MenuViewAll");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool84 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuChangeCode");
		Infragistics.Win.UltraWinToolbars.PopupControlContainerTool popupControlContainerTool1 = new Infragistics.Win.UltraWinToolbars.PopupControlContainerTool("PopComboCost");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool85 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuClearCost");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool86 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MenuEdit_SelAll");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool87 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MenuViewAnalysis");
		Infragistics.Win.Appearance appearance77 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool88 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MenuViewAll");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool89 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuAutoNum");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool90 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuClearDB");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool91 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuPickFromOtherDB");
		Infragistics.Win.Appearance appearance78 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool92 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuTool_CancelUsualItem");
		Infragistics.Win.Appearance appearance79 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool93 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuTool_ClearBookmarkAll");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool94 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuTool_ClearBookmarkSpeci");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool31 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuTool_ClearBookmark");
		Infragistics.Win.Appearance appearance80 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool95 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuTool_ClearBookmarkAll");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool96 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuTool_ClearBookmarkSpeci");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool9 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnu_lblFind");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool7 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo1");
		Infragistics.Win.ValueList valueList4 = new Infragistics.Win.ValueList(0);
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool97 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_Go");
		Infragistics.Win.Appearance appearance81 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool98 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuChangeCode");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool32 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("AddOn");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool99 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuConCost");
		Infragistics.Win.Appearance appearance82 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool47 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuCalcErr", "FilterBD");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool48 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuViewUnApprove", "FilterBD");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool100 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuToolApprove");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool101 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuUpdateList");
		Infragistics.Win.UltraWinToolbars.PopupControlContainerTool popupControlContainerTool2 = new Infragistics.Win.UltraWinToolbars.PopupControlContainerTool("PopComboCost");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool102 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCodeUpgrade");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool103 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Other_ShowAllItem");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool49 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuView_PickClass", "Category");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool104 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_PickClass");
		Infragistics.Win.Appearance appearance83 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool50 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuView_PickClassItems", "FilterBD");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool33 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("ShowItemPrice");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool105 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuPriceHigh");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool106 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuPriceMedium");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool107 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuPriceLow");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool108 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuPriceHigh");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool109 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuPriceMedium");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool110 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuPriceLow");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool51 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuViewsurName", "surName");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool52 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuViewUnsurName", "surName");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool34 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("ShowSurName");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool53 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuViewsurName", "surName");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool54 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuViewUnsurName", "surName");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool55 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuViewcommonName", "commonName");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool56 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuViewUncommonName", "commonName");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool57 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuViewTree", "");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool111 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuClearCost");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool112 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuTool_CostStructure");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool113 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuImport3652");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool58 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuPriceExist", "FilterBD");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool59 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuViewcommonName", "commonName");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool60 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuViewUncommonName", "commonName");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool114 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuImportBasic");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool115 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCalculateCorrectness");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool61 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuCorrectItems", "FilterBD");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool62 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuIncorrect", "FilterBD");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool116 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCorrectCName");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool117 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExpNotCorrect");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool118 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuExpAllCorrect");
		this.cboHisPrice = new Infragistics.Win.UltraWinGrid.UltraCombo();
		this.LeftPanel = new System.Windows.Forms.Panel();
		this.onlineList1 = new Archnowledge.Pcces.PccesMain.ArchControls.OnlineList();
		this.functionButtons1 = new Archnowledge.Pcces.PccesMain.ArchControls.FunctionButtons();
		this.pnl_spliter = new System.Windows.Forms.Panel();
		this.Btn_Splt = new Infragistics.Win.Misc.UltraButton();
		this.iglst_splt_Btn = new System.Windows.Forms.ImageList(this.components);
		this.ssp_Lower = new AxThreed.AxSSPanel();
		this.ssp_Bottom = new AxThreed.AxSSPanel();
		this.ssp_Upper = new AxThreed.AxSSPanel();
		this.ssp_Top = new AxThreed.AxSSPanel();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.frmMrsBase_Fill_Panel = new System.Windows.Forms.Panel();
		this.panel3 = new System.Windows.Forms.Panel();
		this.panel6 = new System.Windows.Forms.Panel();
		this.panel5 = new System.Windows.Forms.Panel();
		this.BidbtnClose = new Infragistics.Win.Misc.UltraButton();
		this.gridMrsBase1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.ssp_GridCaption = new AxThreed.AxSSPanel();
		this.lblUseDatabase = new Infragistics.Win.Misc.UltraLabel();
		this.PNL = new System.Windows.Forms.Panel();
		this.RdoNew = new System.Windows.Forms.RadioButton();
		this.RdoYes = new System.Windows.Forms.RadioButton();
		this.RdoNo = new System.Windows.Forms.RadioButton();
		this.RdoAll = new System.Windows.Forms.RadioButton();
		this.PNL_COST = new System.Windows.Forms.Panel();
		this.ultraTree2 = new Infragistics.Win.UltraWinTree.UltraTree();
		this.panel4 = new System.Windows.Forms.Panel();
		this.ultraButton3 = new Infragistics.Win.Misc.UltraButton();
		this.lblCost = new Infragistics.Win.Misc.UltraLabel();
		this.PNL_TREE = new System.Windows.Forms.Panel();
		this.ultraTree1 = new Infragistics.Win.UltraWinTree.UltraTree();
		this.panel8 = new System.Windows.Forms.Panel();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.splitter1 = new System.Windows.Forms.Splitter();
		this.pnlParent = new System.Windows.Forms.Panel();
		this.gridMrsBase2 = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.panel7 = new System.Windows.Forms.Panel();
		this.ultraButton9 = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraButton2 = new Infragistics.Win.Misc.UltraButton();
		this.panel1 = new System.Windows.Forms.Panel();
		this.ultraStatusBar1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.ultraToolbarsManager1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
		this._frmMrsBase_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._frmMrsBase_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._frmMrsBase_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._frmMrsBase_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.imageList3 = new System.Windows.Forms.ImageList(this.components);
		this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
		((System.ComponentModel.ISupportInitialize)this.cboHisPrice).BeginInit();
		this.LeftPanel.SuspendLayout();
		this.pnl_spliter.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ssp_Lower).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Bottom).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Upper).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Top).BeginInit();
		this.frmMrsBase_Fill_Panel.SuspendLayout();
		this.panel3.SuspendLayout();
		this.panel6.SuspendLayout();
		this.panel5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridMrsBase1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_GridCaption).BeginInit();
		this.ssp_GridCaption.SuspendLayout();
		this.PNL.SuspendLayout();
		this.PNL_COST.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraTree2).BeginInit();
		this.panel4.SuspendLayout();
		this.PNL_TREE.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraTree1).BeginInit();
		this.panel8.SuspendLayout();
		this.pnlParent.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridMrsBase2).BeginInit();
		this.panel7.SuspendLayout();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).BeginInit();
		base.SuspendLayout();
		this.cboHisPrice.AutoEdit = false;
		this.cboHisPrice.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
		ultraGridBand1.Override.TipStyleCell = Infragistics.Win.UltraWinGrid.TipStyle.Show;
		ultraGridBand1.Override.TipStyleScroll = Infragistics.Win.UltraWinGrid.TipStyle.Show;
		ultraGridBand1.UseRowLayout = true;
		this.cboHisPrice.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
		this.cboHisPrice.DisplayMember = "";
		this.cboHisPrice.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.Office2003;
		this.cboHisPrice.Location = new System.Drawing.Point(56, 144);
		this.cboHisPrice.MaxDropDownItems = 20;
		this.cboHisPrice.Name = "cboHisPrice";
		this.cboHisPrice.Size = new System.Drawing.Size(272, 21);
		this.cboHisPrice.TabIndex = 17;
		this.cboHisPrice.Text = "請下拉，挑選工項價格";
		this.cboHisPrice.ValueMember = "";
		this.cboHisPrice.Visible = false;
		this.cboHisPrice.AfterCloseUp += new System.EventHandler(cboHisPrice_AfterCloseUp);
		this.LeftPanel.Controls.Add(this.onlineList1);
		this.LeftPanel.Controls.Add(this.functionButtons1);
		this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
		this.LeftPanel.Location = new System.Drawing.Point(0, 0);
		this.LeftPanel.Name = "LeftPanel";
		this.LeftPanel.Size = new System.Drawing.Size(160, 556);
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
		this.onlineList1.TabIndex = 3;
		this.functionButtons1._ActiveFunction = "";
		this.functionButtons1._CurrOpenMode = Archnowledge.Pcces.CommonClass.FunctionOpenMode.Budget;
		this.functionButtons1._ServerName = "localhost";
		this.functionButtons1._UserID = "PccesAdmin";
		this.functionButtons1._UserName = "";
		this.functionButtons1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.functionButtons1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.functionButtons1.Location = new System.Drawing.Point(0, 0);
		this.functionButtons1.Name = "functionButtons1";
		this.functionButtons1.Size = new System.Drawing.Size(160, 556);
		this.functionButtons1.TabIndex = 2;
		this.pnl_spliter.BackColor = System.Drawing.Color.LightGray;
		this.pnl_spliter.Controls.Add(this.Btn_Splt);
		this.pnl_spliter.Controls.Add(this.ssp_Lower);
		this.pnl_spliter.Controls.Add(this.ssp_Bottom);
		this.pnl_spliter.Controls.Add(this.ssp_Upper);
		this.pnl_spliter.Controls.Add(this.ssp_Top);
		this.pnl_spliter.Dock = System.Windows.Forms.DockStyle.Left;
		this.pnl_spliter.Location = new System.Drawing.Point(160, 0);
		this.pnl_spliter.Name = "pnl_spliter";
		this.pnl_spliter.Size = new System.Drawing.Size(7, 556);
		this.pnl_spliter.TabIndex = 1;
		appearance1.BorderColor = System.Drawing.Color.Transparent;
		appearance1.BorderColor3DBase = System.Drawing.Color.Transparent;
		appearance1.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance1.ImageBackground");
		this.Btn_Splt.Appearance = appearance1;
		this.Btn_Splt.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Borderless;
		this.Btn_Splt.Cursor = System.Windows.Forms.Cursors.Hand;
		this.Btn_Splt.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Btn_Splt.ImageList = this.iglst_splt_Btn;
		this.Btn_Splt.ImageSize = new System.Drawing.Size(7, 57);
		this.Btn_Splt.Location = new System.Drawing.Point(0, 232);
		this.Btn_Splt.Name = "Btn_Splt";
		this.Btn_Splt.ShapeImage = (System.Drawing.Image)resources.GetObject("Btn_Splt.ShapeImage");
		this.Btn_Splt.ShowFocusRect = false;
		this.Btn_Splt.ShowOutline = false;
		this.Btn_Splt.Size = new System.Drawing.Size(7, 65);
		this.Btn_Splt.TabIndex = 5;
		this.toolTip1.SetToolTip(this.Btn_Splt, "隱藏/顯示功能面板");
		this.Btn_Splt.MouseLeave += new System.EventHandler(Btn_Splt_MouseLeave);
		this.Btn_Splt.Click += new System.EventHandler(Btn_Splt_Click);
		this.Btn_Splt.MouseEnter += new System.EventHandler(Btn_Splt_MouseEnter);
		this.iglst_splt_Btn.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("iglst_splt_Btn.ImageStream");
		this.iglst_splt_Btn.TransparentColor = System.Drawing.Color.Transparent;
		this.iglst_splt_Btn.Images.SetKeyName(0, "");
		this.iglst_splt_Btn.Images.SetKeyName(1, "");
		this.iglst_splt_Btn.Images.SetKeyName(2, "");
		this.iglst_splt_Btn.Images.SetKeyName(3, "");
		this.ssp_Lower.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.ssp_Lower.Location = new System.Drawing.Point(0, 297);
		this.ssp_Lower.Name = "ssp_Lower";
		this.ssp_Lower.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Lower.OcxState");
		this.ssp_Lower.Size = new System.Drawing.Size(7, 256);
		this.ssp_Lower.TabIndex = 3;
		this.ssp_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.ssp_Bottom.Location = new System.Drawing.Point(0, 553);
		this.ssp_Bottom.Name = "ssp_Bottom";
		this.ssp_Bottom.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Bottom.OcxState");
		this.ssp_Bottom.Size = new System.Drawing.Size(7, 3);
		this.ssp_Bottom.TabIndex = 4;
		this.ssp_Upper.Dock = System.Windows.Forms.DockStyle.Top;
		this.ssp_Upper.Location = new System.Drawing.Point(0, 3);
		this.ssp_Upper.Name = "ssp_Upper";
		this.ssp_Upper.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Upper.OcxState");
		this.ssp_Upper.Size = new System.Drawing.Size(7, 229);
		this.ssp_Upper.TabIndex = 2;
		this.ssp_Top.Dock = System.Windows.Forms.DockStyle.Top;
		this.ssp_Top.Location = new System.Drawing.Point(0, 0);
		this.ssp_Top.Name = "ssp_Top";
		this.ssp_Top.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_Top.OcxState");
		this.ssp_Top.Size = new System.Drawing.Size(7, 3);
		this.ssp_Top.TabIndex = 1;
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
		this.imageList1.Images.SetKeyName(22, "");
		this.imageList1.Images.SetKeyName(23, "");
		this.imageList1.Images.SetKeyName(24, "");
		this.frmMrsBase_Fill_Panel.Controls.Add(this.panel3);
		this.frmMrsBase_Fill_Panel.Controls.Add(this.pnl_spliter);
		this.frmMrsBase_Fill_Panel.Controls.Add(this.LeftPanel);
		this.frmMrsBase_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
		this.frmMrsBase_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.frmMrsBase_Fill_Panel.Location = new System.Drawing.Point(0, 106);
		this.frmMrsBase_Fill_Panel.Name = "frmMrsBase_Fill_Panel";
		this.frmMrsBase_Fill_Panel.Size = new System.Drawing.Size(1125, 556);
		this.frmMrsBase_Fill_Panel.TabIndex = 0;
		this.panel3.Controls.Add(this.panel6);
		this.panel3.Controls.Add(this.splitter1);
		this.panel3.Controls.Add(this.pnlParent);
		this.panel3.Controls.Add(this.panel1);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel3.Location = new System.Drawing.Point(167, 0);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(958, 556);
		this.panel3.TabIndex = 2;
		this.panel6.Controls.Add(this.panel5);
		this.panel6.Controls.Add(this.PNL_COST);
		this.panel6.Controls.Add(this.PNL_TREE);
		this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel6.Location = new System.Drawing.Point(0, 133);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(958, 397);
		this.panel6.TabIndex = 6;
		this.panel5.Controls.Add(this.BidbtnClose);
		this.panel5.Controls.Add(this.gridMrsBase1);
		this.panel5.Controls.Add(this.ssp_GridCaption);
		this.panel5.Controls.Add(this.cboHisPrice);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel5.Location = new System.Drawing.Point(400, 0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(558, 397);
		this.panel5.TabIndex = 3;
		this.BidbtnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance2.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.BidbtnClose.Appearance = appearance2;
		this.BidbtnClose.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.BidbtnClose.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.BidbtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.BidbtnClose.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance3.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance3.BackColor2 = System.Drawing.Color.White;
		appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.BidbtnClose.HotTrackAppearance = appearance3;
		this.BidbtnClose.HotTracking = true;
		this.BidbtnClose.Location = new System.Drawing.Point(462, 4);
		this.BidbtnClose.Name = "BidbtnClose";
		this.BidbtnClose.ShowFocusRect = false;
		this.BidbtnClose.ShowOutline = false;
		this.BidbtnClose.Size = new System.Drawing.Size(88, 24);
		this.BidbtnClose.SupportThemes = false;
		this.BidbtnClose.TabIndex = 18;
		this.BidbtnClose.Text = "返回標單作業";
		this.BidbtnClose.Visible = false;
		this.BidbtnClose.Click += new System.EventHandler(BidbtnClose_Click);
		this.gridMrsBase1._ExcelFileName = "";
		this.gridMrsBase1._ExcelSheeName = "";
		this.gridMrsBase1._IsOpenExcelAfterExport = false;
		this.gridMrsBase1.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
		this.gridMrsBase1.AutoResize = false;
		this.gridMrsBase1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridMrsBase1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
		this.gridMrsBase1.ColumnInfo = resources.GetString("gridMrsBase1.ColumnInfo");
		this.ultraToolbarsManager1.SetContextMenuUltra(this.gridMrsBase1, "Popup1");
		this.gridMrsBase1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridMrsBase1.EditOptions = C1.Win.C1FlexGrid.EditFlags.None;
		this.gridMrsBase1.ExtendLastCol = true;
		this.gridMrsBase1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridMrsBase1.ForeColor = System.Drawing.Color.Black;
		this.gridMrsBase1.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.gridMrsBase1.IsProcessUndo = true;
		this.gridMrsBase1.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.gridMrsBase1.Location = new System.Drawing.Point(0, 30);
		this.gridMrsBase1.Name = "gridMrsBase1";
		this.gridMrsBase1.Rows.Count = 1;
		this.gridMrsBase1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.gridMrsBase1.ShowCursor = true;
		this.gridMrsBase1.ShowToolTipOnNarrowColumn = true;
		this.gridMrsBase1.Size = new System.Drawing.Size(558, 367);
		this.gridMrsBase1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridMrsBase1.Styles"));
		this.gridMrsBase1.TabIndex = 6;
		this.gridMrsBase1.UndoMax = 2;
		this.gridMrsBase1.Click += new System.EventHandler(gridMrsBase1_Click);
		this.gridMrsBase1.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(gridMrsBase1_AfterRowColChange);
		this.gridMrsBase1.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(gridMrsBase1_StartEdit);
		this.gridMrsBase1.BeforeMouseDown += new C1.Win.C1FlexGrid.BeforeMouseDownEventHandler(gridMrsBase1_BeforeMouseDown);
		this.gridMrsBase1.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(gridMrsBase1_AfterEdit);
		this.gridMrsBase1.AfterScroll += new C1.Win.C1FlexGrid.RangeEventHandler(gridMrsBase1_AfterScroll);
		this.gridMrsBase1.KeyDown += new System.Windows.Forms.KeyEventHandler(gridMrsBase1_KeyDown);
		this.gridMrsBase1.MouseDown += new System.Windows.Forms.MouseEventHandler(gridMrsBase1_MouseDown);
		this.gridMrsBase1.Resize += new System.EventHandler(gridMrsBase1_Resize);
		this.gridMrsBase1.SelChange += new System.EventHandler(gridMrsBase1_SelChange);
		this.gridMrsBase1.MouseUp += new System.Windows.Forms.MouseEventHandler(gridMrsBase1_MouseUp);
		this.gridMrsBase1.LeaveCell += new System.EventHandler(gridMrsBase1_LeaveCell);
		this.gridMrsBase1.MouseMove += new System.Windows.Forms.MouseEventHandler(gridMrsBase1_MouseMove);
		this.gridMrsBase1.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(gridMrsBase1_BeforeEdit);
		this.gridMrsBase1.BeforeSort += new C1.Win.C1FlexGrid.SortColEventHandler(gridMrsBase1_BeforeSort);
		this.gridMrsBase1.DoubleClick += new System.EventHandler(gridMrsBase1_DoubleClick);
		this.gridMrsBase1.KeyUp += new System.Windows.Forms.KeyEventHandler(gridMrsBase1_KeyUp);
		this.ssp_GridCaption.Controls.Add(this.lblUseDatabase);
		this.ssp_GridCaption.Controls.Add(this.PNL);
		this.ssp_GridCaption.Dock = System.Windows.Forms.DockStyle.Top;
		this.ssp_GridCaption.Location = new System.Drawing.Point(0, 0);
		this.ssp_GridCaption.Name = "ssp_GridCaption";
		this.ssp_GridCaption.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ssp_GridCaption.OcxState");
		this.ssp_GridCaption.Size = new System.Drawing.Size(558, 30);
		this.ssp_GridCaption.TabIndex = 0;
		this.lblUseDatabase.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.lblUseDatabase.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblUseDatabase.Location = new System.Drawing.Point(2, 8);
		this.lblUseDatabase.Name = "lblUseDatabase";
		this.lblUseDatabase.Size = new System.Drawing.Size(400, 20);
		this.lblUseDatabase.TabIndex = 7;
		this.lblUseDatabase.Text = "目前資料庫：";
		this.PNL.Anchor = System.Windows.Forms.AnchorStyles.Left;
		this.PNL.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.PNL.Controls.Add(this.RdoNew);
		this.PNL.Controls.Add(this.RdoYes);
		this.PNL.Controls.Add(this.RdoNo);
		this.PNL.Controls.Add(this.RdoAll);
		this.PNL.Location = new System.Drawing.Point(400, 4);
		this.PNL.Name = "PNL";
		this.PNL.Size = new System.Drawing.Size(266, 22);
		this.PNL.TabIndex = 19;
		this.PNL.Visible = false;
		this.RdoNew.AutoSize = true;
		this.RdoNew.Location = new System.Drawing.Point(201, 7);
		this.RdoNew.Name = "RdoNew";
		this.RdoNew.Size = new System.Drawing.Size(59, 16);
		this.RdoNew.TabIndex = 0;
		this.RdoNew.Text = "新編碼";
		this.RdoNew.UseVisualStyleBackColor = true;
		this.RdoNew.Click += new System.EventHandler(RdoNew_Click);
		this.RdoYes.AutoSize = true;
		this.RdoYes.Checked = true;
		this.RdoYes.Location = new System.Drawing.Point(141, 7);
		this.RdoYes.Name = "RdoYes";
		this.RdoYes.Size = new System.Drawing.Size(59, 16);
		this.RdoYes.TabIndex = 0;
		this.RdoYes.TabStop = true;
		this.RdoYes.Text = "已歸屬";
		this.RdoYes.UseVisualStyleBackColor = true;
		this.RdoYes.Click += new System.EventHandler(RdoYes_Click);
		this.RdoNo.AutoSize = true;
		this.RdoNo.Location = new System.Drawing.Point(78, 7);
		this.RdoNo.Name = "RdoNo";
		this.RdoNo.Size = new System.Drawing.Size(59, 16);
		this.RdoNo.TabIndex = 0;
		this.RdoNo.Text = "未歸屬";
		this.RdoNo.UseVisualStyleBackColor = true;
		this.RdoNo.Click += new System.EventHandler(RdoNo_Click);
		this.RdoAll.AutoSize = true;
		this.RdoAll.Location = new System.Drawing.Point(4, 7);
		this.RdoAll.Name = "RdoAll";
		this.RdoAll.Size = new System.Drawing.Size(71, 16);
		this.RdoAll.TabIndex = 0;
		this.RdoAll.TabStop = true;
		this.RdoAll.Text = "全部工項";
		this.RdoAll.UseVisualStyleBackColor = true;
		this.RdoAll.Click += new System.EventHandler(RdoAll_Click);
		this.PNL_COST.Controls.Add(this.ultraTree2);
		this.PNL_COST.Controls.Add(this.panel4);
		this.PNL_COST.Dock = System.Windows.Forms.DockStyle.Left;
		this.PNL_COST.Location = new System.Drawing.Point(200, 0);
		this.PNL_COST.Name = "PNL_COST";
		this.PNL_COST.Size = new System.Drawing.Size(200, 397);
		this.PNL_COST.TabIndex = 6;
		this.ultraTree2.AllowDrop = true;
		appearance37.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraTree2.Appearance = appearance37;
		this.ultraTree2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ultraTree2.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraTree2.HideSelection = false;
		this.ultraTree2.Indent = 15;
		this.ultraTree2.Location = new System.Drawing.Point(0, 24);
		this.ultraTree2.Name = "ultraTree2";
		_override1.AllowAutoDragExpand = Infragistics.Win.UltraWinTree.AllowAutoDragExpand.ExpandOnDragHover;
		_override1.SelectionType = Infragistics.Win.UltraWinTree.SelectType.Single;
		this.ultraTree2.Override = _override1;
		this.ultraTree2.Size = new System.Drawing.Size(200, 373);
		this.ultraTree2.TabIndex = 2;
		this.ultraTree2.Click += new System.EventHandler(ultraTree2_Click);
		this.ultraTree2.AfterSelect += new Infragistics.Win.UltraWinTree.AfterNodeSelectEventHandler(ultraTree2_AfterSelect);
		this.ultraTree2.DragEnter += new System.Windows.Forms.DragEventHandler(ultraTree2_DragEnter);
		this.ultraTree2.DragDrop += new System.Windows.Forms.DragEventHandler(ultraTree2_DragDrop);
		this.ultraTree2.DragOver += new System.Windows.Forms.DragEventHandler(ultraTree2_DragOver);
		this.panel4.Controls.Add(this.ultraButton3);
		this.panel4.Controls.Add(this.lblCost);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel4.Location = new System.Drawing.Point(0, 0);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(200, 24);
		this.panel4.TabIndex = 0;
		appearance38.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		this.ultraButton3.Appearance = appearance38;
		this.ultraButton3.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.ultraButton3.Dock = System.Windows.Forms.DockStyle.Right;
		this.ultraButton3.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ultraButton3.Location = new System.Drawing.Point(180, 0);
		this.ultraButton3.Name = "ultraButton3";
		this.ultraButton3.ShowFocusRect = false;
		this.ultraButton3.ShowOutline = false;
		this.ultraButton3.Size = new System.Drawing.Size(20, 24);
		this.ultraButton3.SupportThemes = false;
		this.ultraButton3.TabIndex = 0;
		this.ultraButton3.Text = "X";
		this.ultraButton3.Click += new System.EventHandler(ultraButton3_Click);
		appearance39.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.lblCost.Appearance = appearance39;
		this.lblCost.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.lblCost.Dock = System.Windows.Forms.DockStyle.Fill;
		this.lblCost.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblCost.Location = new System.Drawing.Point(0, 0);
		this.lblCost.Name = "lblCost";
		this.lblCost.Padding = new System.Drawing.Size(5, 0);
		this.lblCost.Size = new System.Drawing.Size(200, 24);
		this.lblCost.TabIndex = 1;
		this.lblCost.Text = "建築成本架構";
		this.PNL_TREE.Controls.Add(this.ultraTree1);
		this.PNL_TREE.Controls.Add(this.panel8);
		this.PNL_TREE.Dock = System.Windows.Forms.DockStyle.Left;
		this.PNL_TREE.Location = new System.Drawing.Point(0, 0);
		this.PNL_TREE.Name = "PNL_TREE";
		this.PNL_TREE.Size = new System.Drawing.Size(200, 397);
		this.PNL_TREE.TabIndex = 5;
		appearance40.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraTree1.Appearance = appearance40;
		this.ultraTree1.BorderStyle = Infragistics.Win.UIElementBorderStyle.Inset;
		this.ultraTree1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ultraTree1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraTree1.HideSelection = false;
		this.ultraTree1.Indent = 15;
		this.ultraTree1.Location = new System.Drawing.Point(0, 24);
		this.ultraTree1.Name = "ultraTree1";
		_override2.SelectionType = Infragistics.Win.UltraWinTree.SelectType.Single;
		this.ultraTree1.Override = _override2;
		this.ultraTree1.Size = new System.Drawing.Size(200, 373);
		this.ultraTree1.TabIndex = 2;
		this.ultraTree1.Click += new System.EventHandler(ultraTree1_Click);
		this.panel8.Controls.Add(this.ultraButton1);
		this.panel8.Controls.Add(this.ultraLabel1);
		this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel8.Location = new System.Drawing.Point(0, 0);
		this.panel8.Name = "panel8";
		this.panel8.Size = new System.Drawing.Size(200, 24);
		this.panel8.TabIndex = 0;
		appearance41.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		this.ultraButton1.Appearance = appearance41;
		this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.ultraButton1.Dock = System.Windows.Forms.DockStyle.Right;
		this.ultraButton1.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ultraButton1.Location = new System.Drawing.Point(180, 0);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.ShowFocusRect = false;
		this.ultraButton1.ShowOutline = false;
		this.ultraButton1.Size = new System.Drawing.Size(20, 24);
		this.ultraButton1.SupportThemes = false;
		this.ultraButton1.TabIndex = 0;
		this.ultraButton1.Text = "X";
		this.ultraButton1.Click += new System.EventHandler(ultraButton1_Click_1);
		appearance42.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel1.Appearance = appearance42;
		this.ultraLabel1.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.ultraLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ultraLabel1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel1.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Padding = new System.Drawing.Size(5, 0);
		this.ultraLabel1.Size = new System.Drawing.Size(200, 24);
		this.ultraLabel1.TabIndex = 1;
		this.ultraLabel1.Text = "工項目錄";
		this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
		this.splitter1.Location = new System.Drawing.Point(0, 128);
		this.splitter1.Name = "splitter1";
		this.splitter1.Size = new System.Drawing.Size(958, 5);
		this.splitter1.TabIndex = 7;
		this.splitter1.TabStop = false;
		this.splitter1.Visible = false;
		this.pnlParent.Controls.Add(this.gridMrsBase2);
		this.pnlParent.Controls.Add(this.panel7);
		this.pnlParent.Dock = System.Windows.Forms.DockStyle.Top;
		this.pnlParent.Location = new System.Drawing.Point(0, 0);
		this.pnlParent.Name = "pnlParent";
		this.pnlParent.Size = new System.Drawing.Size(958, 128);
		this.pnlParent.TabIndex = 5;
		this.gridMrsBase2._ExcelFileName = "";
		this.gridMrsBase2._ExcelSheeName = "";
		this.gridMrsBase2._IsOpenExcelAfterExport = false;
		this.gridMrsBase2.AllowEditing = false;
		this.gridMrsBase2.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
		this.gridMrsBase2.AutoResize = false;
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
		this.gridMrsBase2.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.gridMrsBase2.ShowCursor = true;
		this.gridMrsBase2.ShowToolTipOnNarrowColumn = true;
		this.gridMrsBase2.Size = new System.Drawing.Size(958, 104);
		this.gridMrsBase2.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridMrsBase2.Styles"));
		this.gridMrsBase2.TabIndex = 10;
		this.gridMrsBase2.UndoMax = 10;
		this.gridMrsBase2.Click += new System.EventHandler(gridMrsBase2_Click);
		this.gridMrsBase2.MouseMove += new System.Windows.Forms.MouseEventHandler(gridMrsBase2_MouseMove);
		this.panel7.Controls.Add(this.ultraButton9);
		this.panel7.Controls.Add(this.ultraLabel2);
		this.panel7.Controls.Add(this.ultraButton2);
		this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel7.Location = new System.Drawing.Point(0, 0);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(958, 24);
		this.panel7.TabIndex = 1;
		appearance43.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance43.Image = resources.GetObject("appearance43.Image");
		appearance43.ImageHAlign = Infragistics.Win.HAlign.Center;
		this.ultraButton9.Appearance = appearance43;
		this.ultraButton9.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraButton9.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.ultraButton9.Dock = System.Windows.Forms.DockStyle.Right;
		this.ultraButton9.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ultraButton9.Location = new System.Drawing.Point(916, 0);
		this.ultraButton9.Name = "ultraButton9";
		this.ultraButton9.ShowFocusRect = false;
		this.ultraButton9.ShowOutline = false;
		this.ultraButton9.Size = new System.Drawing.Size(22, 24);
		this.ultraButton9.SupportThemes = false;
		this.ultraButton9.TabIndex = 3;
		this.toolTip1.SetToolTip(this.ultraButton9, "查詢結果匯出EXCEL(不能當轉入用)");
		this.ultraButton9.Click += new System.EventHandler(ultraButton9_Click);
		appearance44.ForeColor = System.Drawing.Color.White;
		appearance44.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel2.Appearance = appearance44;
		this.ultraLabel2.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraLabel2.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.ultraLabel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ultraLabel2.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel2.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Padding = new System.Drawing.Size(5, 0);
		this.ultraLabel2.Size = new System.Drawing.Size(938, 24);
		this.ultraLabel2.TabIndex = 1;
		this.ultraLabel2.Text = "父項查詢結果列表";
		appearance45.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance45.ForeColor = System.Drawing.Color.White;
		this.ultraButton2.Appearance = appearance45;
		this.ultraButton2.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.ultraButton2.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.ultraButton2.Dock = System.Windows.Forms.DockStyle.Right;
		this.ultraButton2.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ultraButton2.Location = new System.Drawing.Point(938, 0);
		this.ultraButton2.Name = "ultraButton2";
		this.ultraButton2.ShowFocusRect = false;
		this.ultraButton2.ShowOutline = false;
		this.ultraButton2.Size = new System.Drawing.Size(20, 24);
		this.ultraButton2.SupportThemes = false;
		this.ultraButton2.TabIndex = 0;
		this.ultraButton2.Text = "X";
		this.toolTip1.SetToolTip(this.ultraButton2, "關閉父項查詢視窗");
		this.ultraButton2.Click += new System.EventHandler(ultraButton2_Click);
		this.panel1.Controls.Add(this.ultraStatusBar1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 530);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(958, 26);
		this.panel1.TabIndex = 2;
		appearance46.FontData.SizeInPoints = 11f;
		this.ultraStatusBar1.Appearance = appearance46;
		this.ultraStatusBar1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ultraStatusBar1.Location = new System.Drawing.Point(0, 0);
		this.ultraStatusBar1.Name = "ultraStatusBar1";
		ultraStatusPanel1.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		appearance47.BackColor = System.Drawing.Color.FromArgb(192, 192, 255);
		appearance47.BackColor2 = System.Drawing.Color.Navy;
		appearance47.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
		ultraStatusPanel1.ProgressBarInfo.Appearance = appearance47;
		ultraStatusPanel1.Text = "資料筆數：";
		ultraStatusPanel1.Width = 200;
		ultraStatusPanel2.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		appearance48.BackColor = System.Drawing.Color.LightSlateGray;
		appearance48.BackColor2 = System.Drawing.Color.DarkBlue;
		appearance48.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
		ultraStatusPanel2.ProgressBarInfo.FillAppearance = appearance48;
		ultraStatusPanel2.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		ultraStatusPanel2.Style = Infragistics.Win.UltraWinStatusBar.PanelStyle.Progress;
		ultraStatusPanel2.Width = 0;
		appearance49.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance49.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		ultraStatusPanel3.Appearance = appearance49;
		ultraStatusPanel3.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel3.MarqueeInfo.IsActive = true;
		ultraStatusPanel3.Style = Infragistics.Win.UltraWinStatusBar.PanelStyle.Marquee;
		ultraStatusPanel3.Width = 250;
		appearance50.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance50.TextVAlign = Infragistics.Win.VAlign.Bottom;
		ultraStatusPanel4.Appearance = appearance50;
		ultraStatusPanel4.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel4.Text = "客服電話:(02)2708-8090";
		ultraStatusPanel4.Width = 200;
		this.ultraStatusBar1.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[4] { ultraStatusPanel1, ultraStatusPanel2, ultraStatusPanel3, ultraStatusPanel4 });
		this.ultraStatusBar1.Size = new System.Drawing.Size(958, 26);
		this.ultraStatusBar1.SupportThemes = false;
		this.ultraStatusBar1.TabIndex = 0;
		this.ultraStatusBar1.Text = "ultraStatusBar1";
		this.ultraStatusBar1.PanelClick += new Infragistics.Win.UltraWinStatusBar.PanelClickEventHandler(ultraStatusBar1_PanelClick);
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.White;
		this.imageList2.Images.SetKeyName(0, "");
		this.imageList2.Images.SetKeyName(1, "");
		this.imageList2.Images.SetKeyName(2, "");
		appearance51.FontData.Name = "Arial";
		appearance51.FontData.SizeInPoints = 9f;
		this.ultraToolbarsManager1.Appearance = appearance51;
		appearance52.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance52.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.DockAreaAppearance = appearance52;
		this.ultraToolbarsManager1.DockWithinContainer = this;
		this.ultraToolbarsManager1.ImageListSmall = this.imageList1;
		this.ultraToolbarsManager1.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraToolbarsManager1.LockToolbars = true;
		appearance53.FontData.Name = "Arial";
		appearance53.FontData.SizeInPoints = 9f;
		this.ultraToolbarsManager1.MenuSettings.Appearance = appearance53;
		appearance54.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance54.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance54.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.MenuSettings.HotTrackAppearance = appearance54;
		appearance55.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance55.BackColor2 = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraToolbarsManager1.MenuSettings.IconAreaAppearance = appearance55;
		appearance56.BackColor = System.Drawing.Color.White;
		appearance56.BackColor2 = System.Drawing.Color.White;
		this.ultraToolbarsManager1.MenuSettings.ToolAppearance = appearance56;
		optionSet1.AllowAllUp = false;
		optionSet2.AllowAllUp = false;
		optionSet3.AllowAllUp = false;
		optionSet5.AllowAllUp = false;
		this.ultraToolbarsManager1.OptionSets.Add(optionSet1);
		this.ultraToolbarsManager1.OptionSets.Add(optionSet2);
		this.ultraToolbarsManager1.OptionSets.Add(optionSet3);
		this.ultraToolbarsManager1.OptionSets.Add(optionSet4);
		this.ultraToolbarsManager1.OptionSets.Add(optionSet5);
		this.ultraToolbarsManager1.ShowFullMenusDelay = 500;
		this.ultraToolbarsManager1.ShowQuickCustomizeButton = false;
		this.ultraToolbarsManager1.ShowShortcutsInToolTips = true;
		this.ultraToolbarsManager1.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2003;
		ultraToolbar1.DockedColumn = 0;
		ultraToolbar1.DockedRow = 0;
		ultraToolbar1.IsMainMenuBar = true;
		ultraToolbar1.Settings.AllowCustomize = Infragistics.Win.DefaultableBoolean.False;
		ultraToolbar1.Text = "功能選單";
		ultraToolbar1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[8] { popupMenuTool1, popupMenuTool2, popupMenuTool3, popupMenuTool4, popupMenuTool5, popupMenuTool6, popupMenuTool7, popupMenuTool8 });
		ultraToolbar2.DockedColumn = 0;
		ultraToolbar2.DockedRow = 1;
		ultraToolbar2.Text = "編輯";
		buttonTool1.InstanceProps.IsFirstInGroup = true;
		buttonTool5.InstanceProps.IsFirstInGroup = true;
		buttonTool6.InstanceProps.IsFirstInGroup = true;
		buttonTool7.InstanceProps.IsFirstInGroup = true;
		ultraToolbar2.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[7] { buttonTool1, buttonTool2, buttonTool3, buttonTool4, buttonTool5, buttonTool6, buttonTool7 });
		ultraToolbar3.DockedColumn = 0;
		ultraToolbar3.DockedRow = 3;
		ultraToolbar3.Text = "其他";
		labelTool1.InstanceProps.IsFirstInGroup = true;
		labelTool1.InstanceProps.Width = 44;
		textBoxTool1.InstanceProps.Width = 179;
		buttonTool9.InstanceProps.IsFirstInGroup = true;
		labelTool2.InstanceProps.IsFirstInGroup = true;
		buttonTool11.InstanceProps.IsFirstInGroup = true;
		stateButtonTool1.InstanceProps.IsFirstInGroup = true;
		stateButtonTool1.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool2.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		buttonTool12.InstanceProps.IsFirstInGroup = true;
		ultraToolbar3.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[12]
		{
			labelTool1, comboBoxTool1, textBoxTool1, buttonTool8, buttonTool9, labelTool2, comboBoxTool2, buttonTool10, buttonTool11, stateButtonTool1,
			stateButtonTool2, buttonTool12
		});
		ultraToolbar4.DockedColumn = 1;
		ultraToolbar4.DockedRow = 1;
		ultraToolbar4.Text = "資訊";
		ultraToolbar4.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[3] { popupMenuTool9, popupMenuTool10, buttonTool13 });
		ultraToolbar5.DockedColumn = 0;
		ultraToolbar5.DockedRow = 2;
		ultraToolbar5.Text = "檢視";
		stateButtonTool3.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool4.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool5.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool6.InstanceProps.IsFirstInGroup = true;
		stateButtonTool6.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool7.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool8.InstanceProps.IsFirstInGroup = true;
		stateButtonTool8.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool9.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool10.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool11.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool12.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool13.InstanceProps.IsFirstInGroup = true;
		stateButtonTool13.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool14.InstanceProps.IsFirstInGroup = true;
		stateButtonTool14.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		buttonTool15.InstanceProps.IsFirstInGroup = true;
		ultraToolbar5.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[15]
		{
			stateButtonTool3, stateButtonTool4, stateButtonTool5, stateButtonTool6, stateButtonTool7, stateButtonTool8, stateButtonTool9, stateButtonTool10, stateButtonTool11, stateButtonTool12,
			stateButtonTool13, stateButtonTool14, buttonTool14, buttonTool15, buttonTool16
		});
		ultraToolbar6.DockedColumn = 2;
		ultraToolbar6.DockedRow = 1;
		ultraToolbar6.Text = "書籤";
		ultraToolbar6.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[4] { labelTool3, comboBoxTool3, buttonTool17, buttonTool18 });
		this.ultraToolbarsManager1.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[6] { ultraToolbar1, ultraToolbar2, ultraToolbar3, ultraToolbar4, ultraToolbar5, ultraToolbar6 });
		this.ultraToolbarsManager1.ToolbarSettings.AllowCustomize = Infragistics.Win.DefaultableBoolean.False;
		this.ultraToolbarsManager1.ToolbarSettings.AllowDockBottom = Infragistics.Win.DefaultableBoolean.False;
		this.ultraToolbarsManager1.ToolbarSettings.AllowDockLeft = Infragistics.Win.DefaultableBoolean.False;
		this.ultraToolbarsManager1.ToolbarSettings.AllowDockRight = Infragistics.Win.DefaultableBoolean.False;
		this.ultraToolbarsManager1.ToolbarSettings.AllowDockTop = Infragistics.Win.DefaultableBoolean.False;
		this.ultraToolbarsManager1.ToolbarSettings.AllowFloating = Infragistics.Win.DefaultableBoolean.False;
		this.ultraToolbarsManager1.ToolbarSettings.AllowHiding = Infragistics.Win.DefaultableBoolean.False;
		appearance57.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance57.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		appearance57.FontData.Name = "Arial";
		appearance57.FontData.SizeInPoints = 9f;
		this.ultraToolbarsManager1.ToolbarSettings.Appearance = appearance57;
		appearance58.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance58.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance58.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.ToolbarSettings.HotTrackAppearance = appearance58;
		appearance59.BackColor = System.Drawing.Color.FromArgb(255, 128, 0);
		appearance59.BackColor2 = System.Drawing.Color.White;
		this.ultraToolbarsManager1.ToolbarSettings.PressedAppearance = appearance59;
		popupMenuTool11.SharedProps.Caption = "檔案(&F)";
		popupMenuTool11.SharedProps.Category = "檔案";
		popupMenuTool12.InstanceProps.IsFirstInGroup = true;
		buttonTool19.InstanceProps.IsFirstInGroup = true;
		popupMenuTool11.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[3] { popupMenuTool12, popupMenuTool13, buttonTool19 });
		popupMenuTool14.SharedProps.Caption = "編輯(&E)";
		popupMenuTool14.SharedProps.Category = "編輯";
		buttonTool20.InstanceProps.IsFirstInGroup = true;
		buttonTool21.InstanceProps.IsFirstInGroup = true;
		buttonTool23.InstanceProps.IsFirstInGroup = true;
		popupMenuTool14.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[4] { buttonTool20, buttonTool21, buttonTool22, buttonTool23 });
		popupMenuTool15.SharedProps.Caption = "檢視(&V)";
		popupMenuTool15.SharedProps.Category = "檢視";
		stateButtonTool15.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool16.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool17.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool18.InstanceProps.IsFirstInGroup = true;
		stateButtonTool18.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool19.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		popupMenuTool16.InstanceProps.IsFirstInGroup = true;
		stateButtonTool20.InstanceProps.IsFirstInGroup = true;
		stateButtonTool20.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		popupMenuTool17.InstanceProps.IsFirstInGroup = true;
		popupMenuTool15.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[8] { stateButtonTool15, stateButtonTool16, stateButtonTool17, stateButtonTool18, stateButtonTool19, popupMenuTool16, stateButtonTool20, popupMenuTool17 });
		popupMenuTool18.SharedProps.Caption = "工項編輯(&W)";
		popupMenuTool18.SharedProps.Category = "工項編輯";
		popupMenuTool19.InstanceProps.IsFirstInGroup = true;
		popupMenuTool18.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[4] { buttonTool24, buttonTool25, buttonTool26, popupMenuTool19 });
		popupMenuTool20.SharedProps.Caption = "工具(&T)";
		popupMenuTool20.SharedProps.Category = "工具";
		buttonTool27.InstanceProps.IsFirstInGroup = true;
		buttonTool29.InstanceProps.IsFirstInGroup = true;
		buttonTool30.InstanceProps.IsFirstInGroup = true;
		buttonTool31.InstanceProps.IsFirstInGroup = true;
		buttonTool32.InstanceProps.IsFirstInGroup = true;
		buttonTool34.InstanceProps.IsFirstInGroup = true;
		buttonTool35.InstanceProps.IsFirstInGroup = true;
		buttonTool38.InstanceProps.IsFirstInGroup = true;
		buttonTool39.InstanceProps.IsFirstInGroup = true;
		popupMenuTool20.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[15]
		{
			buttonTool27, buttonTool28, buttonTool29, popupMenuTool21, buttonTool30, buttonTool31, buttonTool32, buttonTool33, buttonTool34, buttonTool35,
			buttonTool36, buttonTool37, buttonTool38, buttonTool39, buttonTool40
		});
		popupMenuTool22.SharedProps.Caption = "說明(&H)";
		popupMenuTool22.SharedProps.Category = "說明";
		buttonTool41.InstanceProps.IsFirstInGroup = true;
		buttonTool42.InstanceProps.IsFirstInGroup = true;
		popupMenuTool22.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[2] { buttonTool41, buttonTool42 });
		popupMenuTool23.SharedProps.Caption = "成本架構(&C)";
		appearance60.Image = 0;
		buttonTool43.SharedProps.AppearancesSmall.Appearance = appearance60;
		buttonTool43.SharedProps.Caption = "存檔";
		buttonTool43.SharedProps.Category = "檔案";
		buttonTool43.SharedProps.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
		buttonTool43.SharedProps.Visible = false;
		buttonTool44.SharedProps.Caption = "結束基本資料維護(&X)";
		buttonTool44.SharedProps.Category = "檔案";
		buttonTool45.SharedProps.Caption = "另存新檔...";
		buttonTool45.SharedProps.Category = "檔案";
		appearance61.Image = resources.GetObject("appearance14.Image");
		popupMenuTool24.SharedProps.AppearancesSmall.Appearance = appearance61;
		popupMenuTool24.SharedProps.Caption = "匯入";
		popupMenuTool24.SharedProps.Category = "匯入";
		popupMenuTool24.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		popupMenuTool24.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[2] { buttonTool46, buttonTool47 });
		appearance62.Image = resources.GetObject("appearance15.Image");
		popupMenuTool25.SharedProps.AppearancesSmall.Appearance = appearance62;
		popupMenuTool25.SharedProps.Caption = "匯出";
		popupMenuTool25.SharedProps.Category = "匯出";
		popupMenuTool25.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		popupMenuTool25.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[2] { buttonTool48, buttonTool49 });
		buttonTool50.SharedProps.Caption = "Excel 格式";
		buttonTool50.SharedProps.Category = "匯入";
		buttonTool51.SharedProps.Caption = "XML 格式";
		buttonTool51.SharedProps.Category = "匯入";
		buttonTool52.SharedProps.Caption = "Excel 格式";
		buttonTool52.SharedProps.Category = "匯出";
		buttonTool53.SharedProps.Caption = "XML 格式";
		buttonTool53.SharedProps.Category = "匯出";
		appearance63.Image = resources.GetObject("appearance16.Image");
		buttonTool54.SharedProps.AppearancesSmall.Appearance = appearance63;
		buttonTool54.SharedProps.Caption = "復原";
		buttonTool54.SharedProps.Category = "編輯";
		buttonTool54.SharedProps.Shortcut = System.Windows.Forms.Shortcut.CtrlZ;
		appearance64.Image = resources.GetObject("appearance17.Image");
		buttonTool55.SharedProps.AppearancesSmall.Appearance = appearance64;
		buttonTool55.SharedProps.Caption = "回復復原";
		buttonTool55.SharedProps.Category = "編輯";
		buttonTool55.SharedProps.Shortcut = System.Windows.Forms.Shortcut.CtrlY;
		appearance65.Image = resources.GetObject("appearance18.Image");
		buttonTool56.SharedProps.AppearancesSmall.Appearance = appearance65;
		buttonTool56.SharedProps.Caption = "剪下";
		buttonTool56.SharedProps.Category = "編輯";
		buttonTool56.SharedProps.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
		appearance66.Image = resources.GetObject("appearance19.Image");
		buttonTool57.SharedProps.AppearancesSmall.Appearance = appearance66;
		buttonTool57.SharedProps.Caption = "複製工項";
		buttonTool57.SharedProps.Category = "編輯";
		buttonTool57.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		buttonTool57.SharedProps.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
		appearance67.Image = resources.GetObject("appearance20.Image");
		buttonTool58.SharedProps.AppearancesSmall.Appearance = appearance67;
		buttonTool58.SharedProps.Caption = "貼上";
		buttonTool58.SharedProps.Category = "編輯";
		buttonTool58.SharedProps.Shortcut = System.Windows.Forms.Shortcut.CtrlV;
		appearance68.Image = 12;
		buttonTool59.SharedProps.AppearancesSmall.Appearance = appearance68;
		buttonTool59.SharedProps.Caption = "尋找...";
		buttonTool59.SharedProps.Category = "編輯";
		buttonTool59.SharedProps.Shortcut = System.Windows.Forms.Shortcut.CtrlF;
		stateButtonTool21.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool21.OptionSetKey = "FilterBD";
		stateButtonTool21.SharedProps.Caption = "全部工項";
		stateButtonTool21.SharedProps.Category = "檢視";
		stateButtonTool21.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool22.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool22.OptionSetKey = "FilterBD";
		stateButtonTool22.SharedProps.Caption = "有單價分析工項";
		stateButtonTool22.SharedProps.Category = "檢視";
		stateButtonTool22.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool23.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool23.OptionSetKey = "FilterBD";
		stateButtonTool23.SharedProps.Caption = "無單價分析工項";
		stateButtonTool23.SharedProps.Category = "檢視";
		stateButtonTool23.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		popupMenuTool26.SharedProps.Caption = "顯示項目類別";
		popupMenuTool26.SharedProps.Category = "檢視";
		popupMenuTool26.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		stateButtonTool24.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool25.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool26.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool27.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool28.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		popupMenuTool26.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[5] { stateButtonTool24, stateButtonTool25, stateButtonTool26, stateButtonTool27, stateButtonTool28 });
		stateButtonTool29.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool29.OptionSetKey = "FilterBD";
		stateButtonTool29.SharedProps.Caption = "工項";
		stateButtonTool29.SharedProps.Category = "檢視";
		stateButtonTool29.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool30.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool30.OptionSetKey = "FilterBD";
		stateButtonTool30.SharedProps.Caption = "人工";
		stateButtonTool30.SharedProps.Category = "檢視";
		stateButtonTool30.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool30.SharedProps.ToolTipText = "只顯示人工項目";
		stateButtonTool31.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool31.OptionSetKey = "FilterBD";
		stateButtonTool31.SharedProps.Caption = "機具";
		stateButtonTool31.SharedProps.Category = "檢視";
		stateButtonTool31.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool32.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool32.OptionSetKey = "FilterBD";
		stateButtonTool32.SharedProps.Caption = "材料";
		stateButtonTool32.SharedProps.Category = "檢視";
		stateButtonTool32.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool33.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool33.OptionSetKey = "FilterBD";
		stateButtonTool33.SharedProps.Caption = "雜項";
		stateButtonTool33.SharedProps.Category = "檢視";
		stateButtonTool33.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool34.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool34.OptionSetKey = "FilterBD";
		stateButtonTool34.SharedProps.Caption = "常用工項";
		stateButtonTool34.SharedProps.Category = "檢視";
		stateButtonTool34.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		popupMenuTool27.DropDownArrowStyle = Infragistics.Win.UltraWinToolbars.DropDownArrowStyle.Segmented;
		popupMenuTool27.SharedProps.Caption = "引用";
		popupMenuTool27.SharedProps.Category = "工項編輯";
		popupMenuTool27.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		popupMenuTool27.SharedProps.Visible = false;
		popupMenuTool27.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[2] { buttonTool60, buttonTool61 });
		buttonTool62.SharedProps.Caption = "自其他基本資料庫...";
		buttonTool62.SharedProps.Category = "工項編輯";
		buttonTool63.SharedProps.Caption = "自營建物價調查資料庫...";
		buttonTool63.SharedProps.Category = "工項編輯";
		appearance69.Image = resources.GetObject("appearance22.Image");
		buttonTool64.SharedProps.AppearancesSmall.Appearance = appearance69;
		buttonTool64.SharedProps.Caption = "小數位數設定...";
		buttonTool64.SharedProps.Category = "工具";
		appearance70.Image = resources.GetObject("appearance23.Image");
		buttonTool65.SharedProps.AppearancesSmall.Appearance = appearance70;
		buttonTool65.SharedProps.Caption = "全部重新小計";
		buttonTool65.SharedProps.Category = "工具";
		buttonTool65.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		popupMenuTool28.SharedProps.Caption = "價格切換";
		popupMenuTool28.SharedProps.Category = "工具";
		popupMenuTool28.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		stateButtonTool35.Checked = true;
		stateButtonTool35.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool36.InstanceProps.IsFirstInGroup = true;
		stateButtonTool36.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool37.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool38.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool39.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool40.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		popupMenuTool28.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[6] { stateButtonTool35, stateButtonTool36, stateButtonTool37, stateButtonTool38, stateButtonTool39, stateButtonTool40 });
		appearance71.Image = 22;
		buttonTool66.SharedProps.AppearancesSmall.Appearance = appearance71;
		buttonTool66.SharedProps.Caption = "加入書籤";
		buttonTool66.SharedProps.Category = "工具";
		buttonTool67.SharedProps.Caption = "查詢父項";
		buttonTool67.SharedProps.Category = "工具";
		buttonTool68.SharedProps.Caption = "設為常用工項";
		buttonTool68.SharedProps.Category = "工具";
		stateButtonTool41.Checked = true;
		stateButtonTool41.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool41.OptionSetKey = "Prices";
		stateButtonTool41.SharedProps.Caption = "全區";
		stateButtonTool41.SharedProps.Category = "工具";
		stateButtonTool41.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool42.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool42.OptionSetKey = "Prices";
		stateButtonTool42.SharedProps.Caption = "北區";
		stateButtonTool42.SharedProps.Category = "工具";
		stateButtonTool42.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool43.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool43.OptionSetKey = "Prices";
		stateButtonTool43.SharedProps.Caption = "中區";
		stateButtonTool43.SharedProps.Category = "工具";
		stateButtonTool43.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool44.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool44.OptionSetKey = "Prices";
		stateButtonTool44.SharedProps.Caption = "南區";
		stateButtonTool44.SharedProps.Category = "工具";
		stateButtonTool44.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool45.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool45.OptionSetKey = "Prices";
		stateButtonTool45.SharedProps.Caption = "東區";
		stateButtonTool45.SharedProps.Category = "工具";
		stateButtonTool45.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool46.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool46.OptionSetKey = "Prices";
		stateButtonTool46.SharedProps.Caption = "離島";
		stateButtonTool46.SharedProps.Category = "工具";
		stateButtonTool46.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		appearance72.Image = 15;
		buttonTool69.SharedProps.AppearancesSmall.Appearance = appearance72;
		buttonTool69.SharedProps.Caption = "PCCES 說明...";
		buttonTool69.SharedProps.Category = "說明";
		buttonTool70.SharedProps.Caption = "關於 PCCES...";
		buttonTool70.SharedProps.Category = "說明";
		labelTool4.SharedProps.Caption = "工項與單價分析:";
		labelTool4.SharedProps.Category = "其他";
		labelTool4.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		comboBoxTool4.DropDownStyle = Infragistics.Win.DropDownStyle.DropDown;
		comboBoxTool4.SharedProps.Category = "其他";
		comboBoxTool4.SharedProps.Width = 100;
		comboBoxTool4.Text = "全部顯示";
		valueListItem1.DataValue = "0";
		valueListItem1.DisplayText = "全部顯示";
		valueListItem2.DataValue = "1";
		valueListItem2.DisplayText = "有單價分析";
		valueListItem3.DataValue = "2";
		valueListItem3.DisplayText = "無單價分析";
		valueList1.ValueListItems.Add(valueListItem1);
		valueList1.ValueListItems.Add(valueListItem2);
		valueList1.ValueListItems.Add(valueListItem3);
		comboBoxTool4.ValueList = valueList1;
		labelTool5.SharedProps.Caption = "篩選:";
		labelTool5.SharedProps.Category = "其他";
		labelTool5.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		comboBoxTool5.SharedProps.Category = "其他";
		comboBoxTool5.SharedProps.Width = 85;
		comboBoxTool5.Text = "工程會碼";
		valueListItem4.DataValue = "0";
		valueListItem4.DisplayText = "工程會碼";
		valueListItem5.DataValue = "1";
		valueListItem5.DisplayText = "工項名稱";
		valueListItem6.DataValue = "2";
		valueListItem6.DisplayText = "工項外碼";
		valueListItem7.DataValue = "3";
		valueListItem7.DisplayText = "俗名";
		valueList2.ValueListItems.Add(valueListItem4);
		valueList2.ValueListItems.Add(valueListItem5);
		valueList2.ValueListItems.Add(valueListItem6);
		valueList2.ValueListItems.Add(valueListItem7);
		comboBoxTool5.ValueList = valueList2;
		textBoxTool2.SharedProps.Category = "其他";
		textBoxTool2.SharedProps.Width = 180;
		appearance73.Image = resources.GetObject("appearance26.Image");
		buttonTool71.SharedProps.AppearancesSmall.Appearance = appearance73;
		buttonTool71.SharedProps.Caption = "執行篩選";
		buttonTool71.SharedProps.Category = "其他";
		labelTool6.SharedProps.Caption = "書籤:";
		labelTool6.SharedProps.Category = "其他";
		labelTool6.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		comboBoxTool6.SharedProps.Category = "其他";
		comboBoxTool6.SharedProps.ToolTipText = "書籤切換";
		comboBoxTool6.SharedProps.Width = 135;
		comboBoxTool6.ValueList = valueList3;
		labelTool7.SharedProps.Caption = "目前資料庫:";
		labelTool7.SharedProps.Category = "資訊";
		labelTool7.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		labelTool8.SharedProps.Category = "資訊";
		labelTool8.SharedProps.Width = 500;
		popupMenuTool29.SharedProps.Caption = "檢視工項";
		popupMenuTool29.SharedProps.Category = "檢視";
		popupMenuTool29.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		appearance74.Image = resources.GetObject("appearance27.Image");
		buttonTool72.SharedProps.AppearancesSmall.Appearance = appearance74;
		buttonTool72.SharedProps.Caption = "新增工項";
		buttonTool72.SharedProps.Category = "工項編輯";
		buttonTool72.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		buttonTool72.SharedProps.Shortcut = System.Windows.Forms.Shortcut.Ins;
		appearance75.Image = resources.GetObject("appearance28.Image");
		buttonTool73.SharedProps.AppearancesSmall.Appearance = appearance75;
		buttonTool73.SharedProps.Caption = "編輯工項";
		buttonTool73.SharedProps.Category = "工項編輯";
		buttonTool73.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		appearance76.Image = resources.GetObject("appearance29.Image");
		buttonTool74.SharedProps.AppearancesSmall.Appearance = appearance76;
		buttonTool74.SharedProps.Caption = "刪除工項";
		buttonTool74.SharedProps.Category = "工項編輯";
		buttonTool74.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		buttonTool74.SharedProps.Shortcut = System.Windows.Forms.Shortcut.Del;
		popupMenuTool30.SharedProps.Caption = "網格編輯表1";
		popupMenuTool30.SharedProps.Category = "Popup1";
		buttonTool76.InstanceProps.IsFirstInGroup = true;
		buttonTool78.InstanceProps.IsFirstInGroup = true;
		buttonTool79.InstanceProps.IsFirstInGroup = true;
		buttonTool81.InstanceProps.IsFirstInGroup = true;
		buttonTool82.InstanceProps.IsFirstInGroup = true;
		buttonTool83.InstanceProps.IsFirstInGroup = true;
		popupControlContainerTool1.InstanceProps.IsFirstInGroup = true;
		popupMenuTool30.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[12]
		{
			buttonTool75, buttonTool76, buttonTool77, buttonTool78, buttonTool79, buttonTool80, buttonTool81, buttonTool82, buttonTool83, buttonTool84,
			popupControlContainerTool1, buttonTool85
		});
		buttonTool86.SharedProps.Caption = "全選";
		buttonTool86.SharedProps.Category = "編輯";
		buttonTool86.SharedProps.Shortcut = System.Windows.Forms.Shortcut.CtrlA;
		appearance77.Image = resources.GetObject("appearance30.Image");
		buttonTool87.SharedProps.AppearancesSmall.Appearance = appearance77;
		buttonTool87.SharedProps.Caption = "下層單價分析";
		buttonTool87.SharedProps.Category = "檢視";
		buttonTool88.SharedProps.Caption = "顯示全部";
		buttonTool88.SharedProps.Category = "檢視";
		buttonTool89.SharedProps.Caption = "自動編碼";
		buttonTool90.SharedProps.Caption = "清空資料庫";
		appearance78.Image = 21;
		buttonTool91.SharedProps.AppearancesSmall.Appearance = appearance78;
		buttonTool91.SharedProps.Caption = "自其他資料庫選用";
		appearance79.Image = 24;
		buttonTool92.SharedProps.AppearancesSmall.Appearance = appearance79;
		buttonTool92.SharedProps.Caption = "取消常用工項";
		buttonTool92.SharedProps.Category = "工具";
		buttonTool92.SharedProps.Enabled = false;
		buttonTool93.SharedProps.Caption = "全部";
		buttonTool93.SharedProps.Category = "工具";
		buttonTool94.SharedProps.Caption = "指定項目...";
		buttonTool94.SharedProps.Category = "工具";
		appearance80.Image = 23;
		popupMenuTool31.SharedProps.AppearancesSmall.Appearance = appearance80;
		popupMenuTool31.SharedProps.Caption = "清空書籤";
		popupMenuTool31.SharedProps.Category = "工具";
		popupMenuTool31.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[2] { buttonTool95, buttonTool96 });
		labelTool9.SharedProps.Caption = "尋找:";
		labelTool9.SharedProps.Category = "編輯";
		labelTool9.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		comboBoxTool7.DropDownStyle = Infragistics.Win.DropDownStyle.DropDown;
		comboBoxTool7.SharedProps.Caption = "關鍵字輸入";
		comboBoxTool7.SharedProps.Category = "編輯";
		comboBoxTool7.SharedProps.Width = 200;
		comboBoxTool7.ValueList = valueList4;
		appearance81.Image = resources.GetObject("appearance34.Image");
		buttonTool97.SharedProps.AppearancesSmall.Appearance = appearance81;
		buttonTool97.SharedProps.Caption = "尋找(GO)";
		buttonTool97.SharedProps.Category = "編輯";
		buttonTool98.SharedProps.Caption = "單筆換碼";
		buttonTool98.SharedProps.Category = "工具";
		buttonTool98.SharedProps.Shortcut = System.Windows.Forms.Shortcut.F4;
		popupMenuTool32.SharedProps.Caption = "附加工具(&A)";
		appearance82.Image = resources.GetObject("appearance35.Image");
		buttonTool99.SharedProps.AppearancesSmall.Appearance = appearance82;
		buttonTool99.SharedProps.Caption = "公共工程價格資料庫引用";
		stateButtonTool47.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool47.OptionSetKey = "FilterBD";
		stateButtonTool47.SharedProps.Caption = "計算錯誤的項目";
		stateButtonTool47.SharedProps.Category = "檢視";
		stateButtonTool47.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool48.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool48.OptionSetKey = "FilterBD";
		stateButtonTool48.SharedProps.Caption = "顯示未核可工項";
		stateButtonTool48.SharedProps.Category = "檢視";
		stateButtonTool48.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool100.SharedProps.Caption = "核定工項...";
		buttonTool100.SharedProps.Category = "工具";
		buttonTool101.SharedProps.Caption = "最新消息...";
		popupControlContainerTool2.AllowTearaway = true;
		popupControlContainerTool2.Control = this.cboHisPrice;
		popupControlContainerTool2.SharedProps.Caption = "挑用單價";
		buttonTool102.SharedProps.Caption = "編碼更新(昇級)...";
		buttonTool102.SharedProps.Category = "工具";
		buttonTool103.SharedProps.Caption = "顯示所有項目";
		buttonTool103.SharedProps.Category = "其他";
		buttonTool103.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool49.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool49.OptionSetKey = "Category";
		stateButtonTool49.SharedProps.Caption = "類別";
		stateButtonTool49.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		appearance83.Image = resources.GetObject("appearance36.Image");
		buttonTool104.SharedProps.AppearancesSmall.Appearance = appearance83;
		buttonTool104.SharedProps.Caption = "執行挑選類別";
		buttonTool104.SharedProps.Category = "檢視";
		stateButtonTool50.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool50.OptionSetKey = "FilterBD";
		stateButtonTool50.SharedProps.Caption = "設定類別";
		stateButtonTool50.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		popupMenuTool33.SharedProps.Caption = "引用參考單價";
		popupMenuTool33.SharedProps.Category = "檢視";
		popupMenuTool33.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		popupMenuTool33.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[3] { buttonTool105, buttonTool106, buttonTool107 });
		buttonTool108.SharedProps.Caption = "價格高";
		buttonTool109.SharedProps.Caption = "價格中";
		buttonTool110.SharedProps.Caption = "價格低";
		stateButtonTool51.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool51.OptionSetKey = "surName";
		stateButtonTool51.SharedProps.Caption = "顯示別名欄位";
		stateButtonTool51.SharedProps.Category = "檢視";
		stateButtonTool51.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool52.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool52.OptionSetKey = "surName";
		stateButtonTool52.SharedProps.Caption = "隱藏別名欄位";
		stateButtonTool52.SharedProps.Category = "檢視";
		stateButtonTool52.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		popupMenuTool34.SharedProps.Caption = "別名/俗名欄位";
		popupMenuTool34.SharedProps.Category = "檢視";
		popupMenuTool34.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		stateButtonTool53.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool54.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool55.InstanceProps.IsFirstInGroup = true;
		stateButtonTool55.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool56.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		popupMenuTool34.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[4] { stateButtonTool53, stateButtonTool54, stateButtonTool55, stateButtonTool56 });
		stateButtonTool57.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool57.SharedProps.Caption = "工項目錄";
		stateButtonTool57.SharedProps.Category = "檢視";
		stateButtonTool57.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool111.SharedProps.Caption = "取消歸屬";
		buttonTool112.SharedProps.Caption = "成本架構建立";
		buttonTool112.SharedProps.Category = "工具";
		buttonTool112.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool113.SharedProps.Caption = "匯入共通項目";
		buttonTool113.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool58.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool58.OptionSetKey = "FilterBD";
		stateButtonTool58.SharedProps.Caption = "工項基本資料庫已存在之工項";
		stateButtonTool58.SharedProps.Category = "檢視";
		stateButtonTool58.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool59.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool59.OptionSetKey = "commonName";
		stateButtonTool59.SharedProps.Caption = "顯示俗名欄位";
		stateButtonTool59.SharedProps.Category = "檢視";
		stateButtonTool60.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool60.OptionSetKey = "commonName";
		stateButtonTool60.SharedProps.Caption = "隱藏俗名欄位";
		stateButtonTool60.SharedProps.Category = "檢視";
		buttonTool114.SharedProps.Caption = "單價分析架構";
		buttonTool114.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool115.SharedProps.Caption = "計算正確率";
		buttonTool115.SharedProps.Category = "其他";
		buttonTool115.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool61.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool61.OptionSetKey = "FilterBD";
		stateButtonTool61.SharedProps.Caption = "正確項";
		stateButtonTool61.SharedProps.Category = "其他";
		stateButtonTool61.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool62.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
		stateButtonTool62.OptionSetKey = "FilterBD";
		stateButtonTool62.SharedProps.Caption = "不正確項";
		stateButtonTool62.SharedProps.Category = "其他";
		stateButtonTool62.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool116.SharedProps.Caption = "名稱修正...";
		buttonTool116.SharedProps.Category = "其他";
		buttonTool116.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool117.SharedProps.Caption = "匯出不正確項";
		buttonTool117.SharedProps.Category = "其他";
		buttonTool117.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool118.SharedProps.Caption = "匯出正確項";
		buttonTool118.SharedProps.Category = "其他";
		buttonTool118.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		this.ultraToolbarsManager1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[111]
		{
			popupMenuTool11, popupMenuTool14, popupMenuTool15, popupMenuTool18, popupMenuTool20, popupMenuTool22, popupMenuTool23, buttonTool43, buttonTool44, buttonTool45,
			popupMenuTool24, popupMenuTool25, buttonTool50, buttonTool51, buttonTool52, buttonTool53, buttonTool54, buttonTool55, buttonTool56, buttonTool57,
			buttonTool58, buttonTool59, stateButtonTool21, stateButtonTool22, stateButtonTool23, popupMenuTool26, stateButtonTool29, stateButtonTool30, stateButtonTool31, stateButtonTool32,
			stateButtonTool33, stateButtonTool34, popupMenuTool27, buttonTool62, buttonTool63, buttonTool64, buttonTool65, popupMenuTool28, buttonTool66, buttonTool67,
			buttonTool68, stateButtonTool41, stateButtonTool42, stateButtonTool43, stateButtonTool44, stateButtonTool45, stateButtonTool46, buttonTool69, buttonTool70, labelTool4,
			comboBoxTool4, labelTool5, comboBoxTool5, textBoxTool2, buttonTool71, labelTool6, comboBoxTool6, labelTool7, labelTool8, popupMenuTool29,
			buttonTool72, buttonTool73, buttonTool74, popupMenuTool30, buttonTool86, buttonTool87, buttonTool88, buttonTool89, buttonTool90, buttonTool91,
			buttonTool92, buttonTool93, buttonTool94, popupMenuTool31, labelTool9, comboBoxTool7, buttonTool97, buttonTool98, popupMenuTool32, buttonTool99,
			stateButtonTool47, stateButtonTool48, buttonTool100, buttonTool101, popupControlContainerTool2, buttonTool102, buttonTool103, stateButtonTool49, buttonTool104, stateButtonTool50,
			popupMenuTool33, buttonTool108, buttonTool109, buttonTool110, stateButtonTool51, stateButtonTool52, popupMenuTool34, stateButtonTool57, buttonTool111, buttonTool112,
			buttonTool113, stateButtonTool58, stateButtonTool59, stateButtonTool60, buttonTool114, buttonTool115, stateButtonTool61, stateButtonTool62, buttonTool116, buttonTool117,
			buttonTool118
		});
		this.ultraToolbarsManager1.ToolKeyPress += new Infragistics.Win.UltraWinToolbars.ToolKeyPressEventHandler(ultraToolbarsManager1_ToolKeyPress);
		this.ultraToolbarsManager1.BeforeToolbarListDropdown += new Infragistics.Win.UltraWinToolbars.BeforeToolbarListDropdownEventHandler(ultraToolbarsManager1_BeforeToolbarListDropdown);
		this.ultraToolbarsManager1.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(ultraToolbarsManager1_ToolClick);
		this.ultraToolbarsManager1.AfterToolCloseup += new Infragistics.Win.UltraWinToolbars.ToolDropdownEventHandler(ultraToolbarsManager1_AfterToolCloseup);
		this.ultraToolbarsManager1.ToolValueChanged += new Infragistics.Win.UltraWinToolbars.ToolEventHandler(ultraToolbarsManager1_ToolValueChanged);
		this.ultraToolbarsManager1.AfterToolDeactivate += new Infragistics.Win.UltraWinToolbars.ToolEventHandler(ultraToolbarsManager1_AfterToolDeactivate);
		this.ultraToolbarsManager1.AfterToolActivate += new Infragistics.Win.UltraWinToolbars.ToolEventHandler(ultraToolbarsManager1_AfterToolActivate);
		this._frmMrsBase_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._frmMrsBase_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._frmMrsBase_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
		this._frmMrsBase_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
		this._frmMrsBase_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
		this._frmMrsBase_Toolbars_Dock_Area_Top.Name = "_frmMrsBase_Toolbars_Dock_Area_Top";
		this._frmMrsBase_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(1125, 106);
		this._frmMrsBase_Toolbars_Dock_Area_Top.ToolbarsManager = this.ultraToolbarsManager1;
		this._frmMrsBase_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._frmMrsBase_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._frmMrsBase_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
		this._frmMrsBase_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
		this._frmMrsBase_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 662);
		this._frmMrsBase_Toolbars_Dock_Area_Bottom.Name = "_frmMrsBase_Toolbars_Dock_Area_Bottom";
		this._frmMrsBase_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(1125, 0);
		this._frmMrsBase_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ultraToolbarsManager1;
		this._frmMrsBase_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._frmMrsBase_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._frmMrsBase_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
		this._frmMrsBase_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
		this._frmMrsBase_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 106);
		this._frmMrsBase_Toolbars_Dock_Area_Left.Name = "_frmMrsBase_Toolbars_Dock_Area_Left";
		this._frmMrsBase_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 556);
		this._frmMrsBase_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager1;
		this._frmMrsBase_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._frmMrsBase_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._frmMrsBase_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
		this._frmMrsBase_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
		this._frmMrsBase_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(1125, 106);
		this._frmMrsBase_Toolbars_Dock_Area_Right.Name = "_frmMrsBase_Toolbars_Dock_Area_Right";
		this._frmMrsBase_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 556);
		this._frmMrsBase_Toolbars_Dock_Area_Right.ToolbarsManager = this.ultraToolbarsManager1;
		this.imageList3.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList3.ImageStream");
		this.imageList3.TransparentColor = System.Drawing.Color.White;
		this.imageList3.Images.SetKeyName(0, "");
		this.imageList3.Images.SetKeyName(1, "");
		this.toolTip1.ShowAlways = true;
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		base.ClientSize = new System.Drawing.Size(1125, 662);
		base.Controls.Add(this.frmMrsBase_Fill_Panel);
		base.Controls.Add(this._frmMrsBase_Toolbars_Dock_Area_Right);
		base.Controls.Add(this._frmMrsBase_Toolbars_Dock_Area_Left);
		base.Controls.Add(this._frmMrsBase_Toolbars_Dock_Area_Top);
		base.Controls.Add(this._frmMrsBase_Toolbars_Dock_Area_Bottom);
		base.KeyPreview = true;
		base.Name = "frmMrsBase";
		this.Text = "基本資料庫";
		base.Load += new System.EventHandler(frmMrsBase_Load);
		base.Activated += new System.EventHandler(frmMrsBase_Activated);
		base.FormClosed += new System.Windows.Forms.FormClosedEventHandler(frmMrsBase_FormClosed);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(frmMrsBase_FormClosing);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(frmMrsBase_KeyDown);
		((System.ComponentModel.ISupportInitialize)this.cboHisPrice).EndInit();
		this.LeftPanel.ResumeLayout(false);
		this.LeftPanel.PerformLayout();
		this.pnl_spliter.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ssp_Lower).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Bottom).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Upper).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_Top).EndInit();
		this.frmMrsBase_Fill_Panel.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		this.panel6.ResumeLayout(false);
		this.panel5.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridMrsBase1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ssp_GridCaption).EndInit();
		this.ssp_GridCaption.ResumeLayout(false);
		this.PNL.ResumeLayout(false);
		this.PNL.PerformLayout();
		this.PNL_COST.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ultraTree2).EndInit();
		this.panel4.ResumeLayout(false);
		this.PNL_TREE.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ultraTree1).EndInit();
		this.panel8.ResumeLayout(false);
		this.pnlParent.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridMrsBase2).EndInit();
		this.panel7.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
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
}
