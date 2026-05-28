using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.DomainModule.CostStructure;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.DomainModule.MrsBase;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.PccesMain.Budget;
using Archnowledge.Pcces.PccesMain.Budget.ItemNoset;
using Archnowledge.Pcces.PccesMain.BudgetChange;
using Archnowledge.Pcces.PccesMain.SysMaintain;
using Archnowledge.Pcces.STDClass;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinStatusBar;
using Infragistics.Win.UltraWinTabControl;
using Infragistics.Win.UltraWinToolbars;
using Infragistics.Win.UltraWinTree;

namespace Archnowledge.Pcces.PccesMain.MrsBase;

public class FormMrsBaseBreakdown_Addnew : Form
{
	private const string FileIni = "OptionSet.ini";

	protected Archnowledge.Pcces.DomainModule.CostStructure.CostStructure _CostStructure = new Archnowledge.Pcces.DomainModule.CostStructure.CostStructure();

	private string TypeFilter = "";

	private string TextFilter = "";

	private string UserID;

	private string F_CallFormName = "";

	private string AppLocation = "";

	private string F_SettingPick = CommonMethods.ExtractFilePath(Application.ExecutablePath) + "MrsBase.ini";

	private string F_NowKey = "ROOT";

	private string F_ParentKey = "";

	private int F_MainQty = 0;

	private int F_MainCst = 0;

	private int F_MainAmt = 0;

	private int F_AnaQty = 0;

	private int F_AnaCst = 0;

	private int F_AnaAmt = 0;

	private int GridCols = 0;

	private object[,] GridColsSquence;

	private DataTable dtMrsBaseA = new DataTable();

	private DataView dvMrsBaseA;

	private Archnowledge.Pcces.BUDClass.MrsBaseA dbMrsBase;

	private ArrayList aArr = new ArrayList();

	private DataTable DT_Nodes = new DataTable();

	private DataTable DT_Leaves = new DataTable();

	private DataTable DT_Leaves12 = new DataTable();

	private string FORM_STATUS = "NORMAL";

	private string F_CurrentDBName = "";

	private string F_TempUseDB = "";

	private string F_flagString = "";

	private DataSet dsPwrSet;

	private string F_CostUID = "";

	private string F_CostType = "";

	private string CompanyDBName = string.Empty;

	private string ProjectCode = string.Empty;

	private bool F_ChangeCodeMode = false;

	private int SortColumnIndex = 0;

	private string SortDirection;

	private PccesFormAction F_ActionName = PccesFormAction.BUD;

	private IContainer components;

	private UltraToolbarsManager ultraToolbarsManager1;

	private Panel panel2;

	private Panel panel3;

	private Splitter splitter1;

	private Panel panel4;

	private Panel panel7;

	private Splitter splitter2;

	private Panel panel5;

	private Panel panel6;

	private UltraLabel ultraLabel1;

	private UltraTree ultraTree1;

	private UltraLabel ultraLabel2;

	private UltraLabel ultraLabel3;

	private Panel panel8;

	private UltraButton ultraButton1;

	private ImageList imageList2;

	private ImageList imageList1;

	private GridBudget gridMrsBase;

	private GridBudget c1FlexGrid2;

	private UltraButton BtnRemove;

	private UltraButton BtnAdd;

	private UltraStatusBar ultraStatusBar1;

	private UltraTabControl Tab_Ctrl;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Top;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Bottom;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Left;

	private UltraToolbarsDockArea _FormSys_B_Toolbars_Dock_Area_Right;

	private Panel panel1;

	private UltraButton BtnExecFlt;

	private TextBox txtFilter;

	private UltraLabel ultraLabel4;

	private UltraTabPageControl Tab_A;

	private UltraTabPageControl Tab_B;

	private Panel panel9;

	private UltraButton ultraButton2;

	private UltraButton ultraButton4;

	private Panel panel10;

	private UltraLabel ultraLabel6;

	private UltraLabel ultraLabel8;

	private UltraStatusBar ultraStatusBar2;

	public GridMrsBase GridUnit1;

	private UltraLabel lblDBName;

	private UltraButton ultraButton3;

	public PccesFormAction _ActionName
	{
		set
		{
			F_ActionName = value;
		}
	}

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

	public string _CallFormName
	{
		get
		{
			return F_CallFormName;
		}
		set
		{
			F_CallFormName = value;
		}
	}

	public string _CostUID
	{
		get
		{
			return F_CostUID;
		}
		set
		{
			F_CostUID = value;
		}
	}

	public string _CostType
	{
		get
		{
			return F_CostType;
		}
		set
		{
			F_CostType = value;
		}
	}

	public bool _ChangeCodeMode
	{
		get
		{
			return F_ChangeCodeMode;
		}
		set
		{
			F_ChangeCodeMode = value;
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

	public string _ProjectCode
	{
		get
		{
			return ProjectCode;
		}
		set
		{
			ProjectCode = value;
		}
	}

	public FormMrsBaseBreakdown_Addnew()
	{
		InitializeComponent();
		GridCols = gridMrsBase.Cols.Count;
		PwrSet pwrSet = new PwrSet();
		dsPwrSet = pwrSet.GetEnabledPwrSet();
		GridColsSquence = new object[GridCols, 8];
		gridMrsBase.Glyphs[GlyphEnum.Checked] = imageList2.Images[0];
		gridMrsBase.Glyphs[GlyphEnum.Unchecked] = imageList2.Images[1];
		c1FlexGrid2.Glyphs[GlyphEnum.Checked] = imageList2.Images[0];
		c1FlexGrid2.Glyphs[GlyphEnum.Unchecked] = imageList2.Images[1];
		HideCols(IsHide: true);
	}

	private void Get_NodesData()
	{
		DBClass DBClass1 = new DBClass();
		DT_Nodes = DBClass1.GetAutoNumA1();
		if (SysConfig.SysTreasureDragonAutoNum)
		{
			string[] itemCodes = new string[6] { "L", "E", "M", "W", "S", "T" };
			string[] cNames = new string[6] { "人工", "機具", "材料", "雜項", "工資", "連工帶料" };
			DataView dv = new DataView(DT_Nodes);
			for (int i = 0; i < itemCodes.Length; i++)
			{
				dv.RowFilter = "ItemCode='" + itemCodes[i] + "'";
				if (dv.Count == 0)
				{
					DataRow dr = DT_Nodes.NewRow();
					dr["ItemCode"] = itemCodes[i];
					dr["cName"] = cNames[i];
					DT_Nodes.Rows.Add(dr);
				}
			}
		}
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
		if (treeNode.Level <= 1 && !(treeNode.Key == "00"))
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
		if (ultraToolbarsManager1.OptionSets[0].SelectedTool == null)
		{
			((StateButtonTool)ultraToolbarsManager1.Tools["mnuListAll"]).Checked = true;
		}
		if (ultraTree1.SelectedNodes.Count > 0)
		{
			int iFIND = gridMrsBase.FindRow(ultraTree1.SelectedNodes[0].Key, 1, gridMrsBase.Cols["PccesCode"].SafeIndex, caseSensitive: false, fullMatch: false, wrap: false);
			if (iFIND > -1)
			{
				gridMrsBase.Row = iFIND;
			}
		}
	}

	private void HideCols(bool IsHide)
	{
		if (IsHide)
		{
			GridUnit1.Cols["Counts"].Visible = false;
			GridUnit1.Cols["ProjectCode"].Visible = false;
			GridUnit1.Cols["ProjCName"].Visible = false;
			GridUnit1.Cols["Flag"].Visible = false;
			GridUnit1.Cols["Level"].Visible = false;
			GridUnit1.Cols["Invoice"].Visible = false;
			gridMrsBase.Cols["PubCode"].Visible = false;
			gridMrsBase.Cols["AnalysisQty"].Visible = false;
			gridMrsBase.Cols["CostKind"].Visible = false;
			gridMrsBase.Cols["memo"].Visible = false;
			gridMrsBase.Cols["rate"].Visible = false;
			gridMrsBase.Cols["resCode"].Visible = false;
			gridMrsBase.Cols["resType"].Visible = false;
			gridMrsBase.Cols["xNameE"].Visible = false;
			gridMrsBase.Cols["xNameC"].Visible = false;
			gridMrsBase.Cols["State"].Visible = false;
			gridMrsBase.Cols["usrQty"].Visible = false;
			gridMrsBase.Cols["usrAmt"].Visible = false;
			gridMrsBase.Cols["Show"].Visible = false;
			gridMrsBase.Cols["Post"].Visible = false;
			gridMrsBase.Cols["PickSeq"].Visible = false;
			gridMrsBase.Cols["CostDec"].Visible = false;
			gridMrsBase.Cols["AmtDec"].Visible = false;
			c1FlexGrid2.Cols["PubCode"].Visible = false;
			c1FlexGrid2.Cols["AnalysisQty"].Visible = false;
			c1FlexGrid2.Cols["CostKind"].Visible = false;
			c1FlexGrid2.Cols["extendCode"].Visible = false;
			c1FlexGrid2.Cols["memo"].Visible = false;
			c1FlexGrid2.Cols["rate"].Visible = false;
			c1FlexGrid2.Cols["resCode"].Visible = false;
			c1FlexGrid2.Cols["resType"].Visible = false;
			c1FlexGrid2.Cols["xNameE"].Visible = false;
			c1FlexGrid2.Cols["xNameC"].Visible = false;
			c1FlexGrid2.Cols["State"].Visible = false;
			c1FlexGrid2.Cols["usrQty"].Visible = false;
			c1FlexGrid2.Cols["usrAmt"].Visible = false;
			c1FlexGrid2.Cols["Show"].Visible = false;
			c1FlexGrid2.Cols["Post"].Visible = false;
			c1FlexGrid2.Cols["PickSeq"].Visible = false;
			c1FlexGrid2.Cols["CostDec"].Visible = false;
			c1FlexGrid2.Cols["AmtDec"].Visible = false;
		}
	}

	private void LoadDBData()
	{
		GeneralManager oManager = new GeneralManager();
		DataSet dsSysPccesSlave;
		ExecResult ER = oManager.GetSysPccesSlave(UserID, out dsSysPccesSlave);
		if (ER.ReturnCode != 0)
		{
			MessageBox.Show(this, "資料庫有未知問題發生 : " + ER.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		else
		{
			BindToGrid(dsSysPccesSlave.Tables[0]);
		}
	}

	private void BindToGrid(DataTable dtSysPccesSlave)
	{
		FORM_STATUS = "Binding";
		CellStyle CSDatabaseName = GridUnit1.Styles.Add("MainColor");
		CSDatabaseName.ForeColor = Color.Blue;
		CSDatabaseName.Font = new Font(GridUnit1.Font, FontStyle.Bold);
		CellStyle CSError = GridUnit1.Styles.Add("ErrorColor");
		CSError.BackColor = Color.Tomato;
		GridUnit1.Rows.Count = 1;
		ultraStatusBar1.Panels[0].Text = "資料筆數：" + dtSysPccesSlave.Rows.Count;
		GridUnit1.Redraw = false;
		foreach (DataRow theRow in dtSysPccesSlave.Rows)
		{
			Row GridRow = GridUnit1.Rows.Add();
			if (theRow["ChkUse"].ToString().Trim() == "1")
			{
				CellRange rg = GridUnit1.GetCellRange(GridRow.Index, GridUnit1.Cols["IsActive"].SafeIndex);
				rg.Style = GridUnit1.Styles["img"];
				rg.Image = imageList2.Images[0];
			}
			else
			{
				CellRange rg = GridUnit1.GetCellRange(GridRow.Index, GridUnit1.Cols["IsActive"].SafeIndex);
				rg.Style = GridUnit1.Styles["img"];
				rg.Image = imageList2.Images[1];
			}
			GridRow.IsNode = true;
			GridRow.Node.Level = 1;
			GridRow.Node.Collapsed = true;
			CellRange rgDB1 = GridUnit1.GetCellRange(GridRow.Index, GridUnit1.Cols["dbDesc"].SafeIndex);
			CellRange rgDB2 = GridUnit1.GetCellRange(GridRow.Index, GridUnit1.Cols["dbName"].SafeIndex);
			CellStyle style = (rgDB2.Style = CSDatabaseName);
			rgDB1.Style = style;
			string DatabaseName = theRow["dbcName"].ToString().Trim();
			string DatabaseDesc = (string)(GridRow["dbDesc"] = theRow["dbcDesc"].ToString().Trim());
			GridRow["dbName"] = DatabaseName;
			if (DatabaseDesc.IndexOf("ERROR") > -1)
			{
				CellRange rgError = GridUnit1.GetCellRange(GridRow.Index, 1, GridRow.Index, GridUnit1.Cols.Count - 1);
				rgError.Style = CSError;
			}
		}
		GridUnit1.Redraw = true;
		FORM_STATUS = "NORMAL";
	}

	private void FormMrsBaseBreakdown_Addnew_Load(object sender, EventArgs e)
	{
		AppLocation = AppDomain.CurrentDomain.BaseDirectory;
		string sAllowIsTooltip = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "CommonData", "AllowIsTooltip");
		string Status = CommonMethods.GetIniValue("MrsBase", "WindowState");
		if (Status == "Maximized")
		{
			base.WindowState = FormWindowState.Maximized;
		}
		if (FORM_STATUS == "NORMAL")
		{
			SysUser oSysUser = new SysUser();
			F_CurrentDBName = oSysUser.GetSysUserDatabaseName(UserID);
			if (F_CallFormName == "frmBudget" && F_CurrentDBName != CompanyDBName)
			{
				LoadDBData();
				Tab_A.Tab.Selected = true;
			}
			else
			{
				F_TempUseDB = F_CurrentDBName;
				LoadTreeData();
				GetNewData();
				Tab_B.Tab.Selected = true;
				if (F_ChangeCodeMode && F_CurrentDBName == CompanyDBName)
				{
					Panel panel = panel5;
					Panel panel2 = panel7;
					bool flag = (splitter2.Visible = false);
					flag = (panel2.Visible = flag);
					panel.Visible = flag;
					gridMrsBase.SelectionMode = SelectionModeEnum.Row;
				}
			}
			int iLoc_X = PubTools.Str2Int(CommonMethods.GetIniValue("MrsBase", "PK_LocationX"));
			int iLoc_Y = PubTools.Str2Int(CommonMethods.GetIniValue("MrsBase", "PK_LocationY"));
			int iSiz_W = PubTools.Str2Int(CommonMethods.GetIniValue("MrsBase", "PK_Width"));
			int iSiz_H = PubTools.Str2Int(CommonMethods.GetIniValue("MrsBase", "PK_Height"));
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
			FORM_STATUS = "ACTIVE";
		}
		if (sAllowIsTooltip.ToUpper() == "TRUE")
		{
			gridMrsBase.ShowToolTipOnNarrowColumn = false;
			c1FlexGrid2.ShowToolTipOnNarrowColumn = false;
			GridUnit1.ShowToolTipOnNarrowColumn = false;
		}
		else
		{
			gridMrsBase.ShowToolTipOnNarrowColumn = true;
			c1FlexGrid2.ShowToolTipOnNarrowColumn = true;
			GridUnit1.ShowToolTipOnNarrowColumn = true;
		}
		if (SysConfig.SysEnablePwrSet)
		{
			gridMrsBase.Cols["PwrSet"].Visible = true;
			gridMrsBase.Cols["Account"].Visible = true;
			c1FlexGrid2.Cols["PwrSet"].Visible = true;
			c1FlexGrid2.Cols["Account"].Visible = true;
		}
		else
		{
			gridMrsBase.Cols["PwrSet"].Visible = false;
			gridMrsBase.Cols["Account"].Visible = false;
			c1FlexGrid2.Cols["PwrSet"].Visible = false;
			c1FlexGrid2.Cols["Account"].Visible = false;
		}
		gridMrsBase.Cols["ExtendCode"].Visible = F_ChangeCodeMode;
		InitializeRowStyle();
	}

	private void LoadTreeData()
	{
		SysUser oSysUser = new SysUser();
		oSysUser.SetSysUserDatabaseName(UserID, F_TempUseDB);
		if (CheckDBVer())
		{
			ChgStru stdll = new ChgStru();
			stdll.F_UserID = UserID;
			stdll.ModifyDatabaseStructure(F_TempUseDB);
		}
		SettingDecimal();
		ultraTree1.Nodes.Clear();
		UltraTreeNode node = ultraTree1.Nodes.Add("ROOT", "預算工項綱要");
		ultraTree1.Nodes[0].Expanded = true;
		SysUser sysUser = new SysUser();
		string databaseName = sysUser.GetSysUserDatabaseName(UserID);
		if (databaseName != CompanyDBName)
		{
			Get_NodesData();
			Get_LeavesData();
			PopulateLevel1(node);
		}
		else
		{
			AutoNum autoNum = new AutoNum();
			DataSet dsTreeNodes = autoNum.GetAutoNum(CompanyDBName);
			node.Nodes.Clear();
			PopulateLevel(ref dsTreeNodes, node);
		}
		if (F_CallFormName.ToUpper() == "FORMSYS_D".ToUpper())
		{
			panel7.Visible = false;
			panel5.Visible = false;
			splitter2.Visible = false;
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

	private bool CheckDBVer()
	{
		bool flag = false;
		string sBuild = PccesVersion.PccesAssemblyVersion;
		string DBVer = PccesVersion.GetDatabaseVersion(UserID);
		if (!DBVer.Equals(sBuild))
		{
			flag = true;
		}
		return flag;
	}

	private void SettingDecimal()
	{
		DataTable DTDecimal = new DataTable();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(UserID);
		aArr.Add("WinFORM 基本工料");
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
			F_MainCst = 0;
			F_MainAmt = 0;
			F_AnaQty = 3;
			F_AnaCst = 2;
			F_AnaAmt = 2;
		}
		DTDecimal = null;
		aArr = null;
		dbDecimal = null;
	}

	private void GetNewData()
	{
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(UserID);
		aArr.Add("WinFORM 基本工料");
		if (!checkCostStructure() || F_CostUID == "")
		{
			panel3.Visible = true;
			dbMrsBase = new Archnowledge.Pcces.BUDClass.MrsBaseA(UserID, aArr);
			dbMrsBase.ps_srckind = "MRS";
			dbMrsBase.ps_projectcode = "";
			Cursor = Cursors.WaitCursor;
			dtMrsBaseA = dbMrsBase.ListItem();
		}
		else
		{
			panel3.Visible = false;
			ModifyDB ModDB = new ModifyDB(UserID, aArr);
			Archnowledge.Pcces.DomainModule.CostStructure.CostStructure _CostStructure1 = new Archnowledge.Pcces.DomainModule.CostStructure.CostStructure();
			dtMrsBaseA = _CostStructure1.ListItemCost(" B.CostUID ='" + F_CostUID + "'", F_CostType);
			_CostStructure1 = null;
		}
		aArr = null;
		Cursor = Cursors.Default;
	}

	private bool checkCostStructure()
	{
		bool flag = true;
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(UserID);
		aArr.Add("(UserDefind_Show) 顯示常用字串資料");
		string ls_selectstr = "select count(*) from CostStructureType where TypeID = '" + F_CostType + "'";
		ModifyDB StdCom = new ModifyDB("", aArr);
		if (StdCom.DBCount(ls_selectstr) == 0)
		{
			flag = false;
		}
		StdCom = null;
		return flag;
	}

	private void InitializeRowStyle()
	{
		CellStyle csAnalysis = gridMrsBase.Styles.Add("AnalysisColor");
		CellStyle csLEM = gridMrsBase.Styles.Add("LEMColor");
		CellStyle csMiscellanea = gridMrsBase.Styles.Add("WColor");
		csAnalysis.ForeColor = Color.Red;
		csLEM.ForeColor = Color.Teal;
		csMiscellanea.ForeColor = Color.Purple;
	}

	private void BindToGridMrsBaseA()
	{
		lock (this)
		{
			dvMrsBaseA = dtMrsBaseA.DefaultView;
			if (SortColumnIndex >= 1)
			{
				string SortColumnName = gridMrsBase.Cols[SortColumnIndex].Name;
				if (SortColumnName == "ANAIMG")
				{
					SortColumnName = "Analysis";
				}
				dvMrsBaseA.Sort = SortColumnName + " " + SortDirection;
			}
			else
			{
				dvMrsBaseA.Sort = " pccesCode ASC ";
				SortDirection = "ASC";
			}
			string filter = string.Empty;
			filter = ((!checkCostStructure() || F_CostUID == "") ? ((TextFilter == string.Empty) ? ((F_NowKey == "ROOT") ? TypeFilter : ((TypeFilter == string.Empty) ? ((F_ParentKey == "E") ? ("(pccesCode like 'E00000" + F_NowKey + "%')") : ((!(F_ParentKey == "L")) ? ("(pccesCode like '" + F_NowKey + "%')") : ("(pccesCode like 'L" + F_NowKey + "%')"))) : ((F_ParentKey == "E") ? ("(pccesCode like 'E00000" + F_NowKey + "%') AND " + TypeFilter) : ((!(F_ParentKey == "L")) ? ("(pccesCode like '" + F_NowKey + "%') AND " + TypeFilter) : ("(pccesCode like 'L" + F_NowKey + "%') AND " + TypeFilter))))) : ((!(TypeFilter == string.Empty)) ? (TextFilter + " AND " + TypeFilter) : TextFilter)) : ((TextFilter == string.Empty) ? TypeFilter : ((!(TypeFilter == string.Empty)) ? (TextFilter + " AND " + TypeFilter) : TextFilter)));
			if (filter.IndexOf('*') > -1)
			{
				filter = filter.Replace("*", "[*]");
			}
			dvMrsBaseA.RowFilter = filter;
			gridMrsBase.Rows.Count = 1;
			gridMrsBase.Rows.Count = dvMrsBaseA.Count + 1;
			if (dvMrsBaseA.Count != 0)
			{
				DataToGrid();
				ultraStatusBar1.Panels[0].Text = "資料筆數:" + dvMrsBaseA.Count;
			}
		}
	}

	private void DataToGrid()
	{
		int topRow = gridMrsBase.TopRow;
		if (topRow < 1)
		{
			return;
		}
		int bottomRow = gridMrsBase.BottomRow;
		gridMrsBase.Redraw = false;
		for (int i = 0; i < dvMrsBaseA.Count; i++)
		{
			Row gridRow = gridMrsBase.Rows[i + 1];
			DataRowView drvMrsBaseA = dvMrsBaseA[i];
			string workItemClass = drvMrsBaseA["pccesCode"].ToString();
			if (workItemClass.StartsWith("L") || workItemClass.StartsWith("E") || workItemClass.StartsWith("M"))
			{
				gridRow.Style = gridMrsBase.Styles["LEMColor"];
			}
			else if (workItemClass.StartsWith("W"))
			{
				gridRow.Style = gridMrsBase.Styles["WColor"];
			}
			gridRow["CName"] = drvMrsBaseA["cName"].ToString();
			if (drvMrsBaseA["analysis"].ToString().Trim() == "1")
			{
				gridRow["Analysis"] = true;
				gridRow.Style = gridMrsBase.Styles["AnalysisColor"];
			}
			else
			{
				gridRow["Analysis"] = false;
			}
			gridRow["PccesCode"] = drvMrsBaseA["pccesCode"].ToString().Trim();
			gridRow["resCode"] = drvMrsBaseA["resCode"];
			gridRow["PubCode"] = drvMrsBaseA["pubCode"];
			gridRow["eName"] = drvMrsBaseA["eName"];
			gridRow["Memo"] = drvMrsBaseA["memo"];
			gridRow["UnitName"] = drvMrsBaseA["unitName"];
			gridRow["resType"] = drvMrsBaseA["resType"];
			gridRow["LRate"] = drvMrsBaseA["lRate"];
			gridRow["ERate"] = drvMrsBaseA["eRate"];
			gridRow["MRate"] = drvMrsBaseA["mRate"];
			gridRow["WRate"] = drvMrsBaseA["wRate"];
			gridRow["Cost"] = drvMrsBaseA["cost"];
			gridRow["AnalysisQty"] = drvMrsBaseA["analysisQty"];
			gridRow["Rate"] = drvMrsBaseA["rate"];
			gridRow["CostKind"] = drvMrsBaseA["costKind"];
			gridRow["XNameC"] = drvMrsBaseA["xNameC"];
			gridRow["XNameE"] = drvMrsBaseA["xNameE"];
			gridRow["eUnit"] = drvMrsBaseA["eUnit"];
			gridRow["ExtendCode"] = drvMrsBaseA["extendCode"];
			gridRow["State"] = drvMrsBaseA["state"];
			gridRow["usrQty"] = drvMrsBaseA["usrQty"];
			gridRow["usrAmt"] = drvMrsBaseA["usrAmt"];
			gridRow["Show"] = drvMrsBaseA["Show"];
			gridRow["Post"] = drvMrsBaseA["Post"];
			gridRow["PickSeq"] = 0;
			gridRow["surName"] = drvMrsBaseA["surName"];
			gridRow["CostDec"] = drvMrsBaseA["CostDec"];
			gridRow["AmtDec"] = drvMrsBaseA["AmtDec"];
			gridRow["Account"] = drvMrsBaseA["Account"];
			if (drvMrsBaseA["PwrSet"] != null)
			{
				gridRow["PwrSet"] = PwrSet.GetName(dsPwrSet, PubTools.Str2Int(drvMrsBaseA["PwrSet"]));
			}
			else
			{
				gridRow["PwrSet"] = PwrSet.GetDefaultName(dsPwrSet);
			}
		}
		gridMrsBase.Redraw = true;
	}

	private void gridMrsBase_AfterScroll(object sender, RangeEventArgs e)
	{
	}

	private void ultraButton2_Click(object sender, EventArgs e)
	{
		int iCount = 0;
		DBClass DBCls = new DBClass();
		DBCls._FS_UserID = UserID;
		string sSrcKind = CommonMethods.GetActionNameString(F_ActionName);
		string sMessage = "";
		for (int i = 1; i < gridMrsBase.Rows.Count; i++)
		{
			if (!gridMrsBase.Rows[i].Selected)
			{
				continue;
			}
			string sPccesCode = gridMrsBase[i, "PccesCode"].ToString();
			bool flag = false;
			if (SysConfig.SysComsEnable && DBCls.IsPccesCodeExistsInMrsBaseA(sPccesCode, ProjectCode, sSrcKind) && sSrcKind.Trim() == "")
			{
				string text = sMessage;
				sMessage = text + sPccesCode + "\t" + gridMrsBase[i, "CName"].ToString() + "\n";
			}
			else if (F_CallFormName.ToUpper() == "FormMrsBaseBreakdown".ToUpper())
			{
				bool IsUseNewMrsB = PubTools.GetAppSet_Bool("UseNewMrsB");
				string AppLocation = AppDomain.CurrentDomain.BaseDirectory;
				string FileINI = AppLocation + "OptionSet.ini";
				string sAllowRepeatItem = CommonMethods.IniReadValue(FileINI, "BreakDownData", "AllowRepeatItem");
				bool IsAllowRepeatItem = sAllowRepeatItem.ToUpper() == "TRUE";
				if (IsUseNewMrsB && IsAllowRepeatItem)
				{
					AddIntoGrid2(i);
				}
				else if (CheckIsAlreadyExist(gridMrsBase[i, "PubCode"].ToString().Trim()) <= -1)
				{
					AddIntoGrid2(i);
				}
			}
			else if (CheckIsAlreadyExist(gridMrsBase[i, "PubCode"].ToString().Trim()) <= -1)
			{
				AddIntoGrid2(i);
			}
		}
		iCount = c1FlexGrid2.Rows.Count - 1;
		ultraLabel3.Text = "已選用工項(" + iCount + ")";
		c1FlexGrid2.Row = c1FlexGrid2.Rows.Count - 1;
		if (!CheckPccesCodeAndPubCode())
		{
			ultraButton1.Enabled = false;
			MessageBox.Show(this, "發現挑選之工項(紅色項目)曾經換碼，而與預算書內之工項衝突，此狀況不允許插入該項目，\n\n請校正後再執行。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else
		{
			ultraButton1.Enabled = true;
		}
		if (sMessage.Trim() != "")
		{
			MessageBox.Show(this, sMessage.Trim() + "\n\n已存在，不可重複新增!", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private bool CheckPccesCodeAndPubCode()
	{
		bool IsOK = true;
		if (F_CallFormName.ToUpper() == "frmBudget".ToUpper())
		{
			ArrayList tmp_AL1 = new ArrayList();
			tmp_AL1.Add(UserID);
			tmp_AL1.Add("自基本資料庫插入工項至預算書--檢查PCCES及PUBCODE是否皆一致");
			CellStyle CS_Chk = c1FlexGrid2.Styles.Add("CS_Chk");
			CellStyle CS_ChkMrs = c1FlexGrid2.Styles.Add("CS_ChkMrs");
			CS_Chk.BackColor = Color.LightPink;
			CS_ChkMrs.BackColor = Color.LightPink;
			Form ActiveForm = base.Owner.ActiveMdiChild;
			if (ActiveForm is frmBudget)
			{
				string sSrcKind = CommonMethods.GetActionNameString((ActiveForm as frmBudget)._ActionName);
				string sProjectCode = (ActiveForm as frmBudget)._ProjectCode;
				ModifyDB stdClass = new ModifyDB(sProjectCode, tmp_AL1);
				DataTable DT_Check = stdClass.DBList("Select pccesCode, pubCode from " + sSrcKind + "ProjMrsA Where ProjectCode='" + sProjectCode + "' ");
				if (DT_Check.Rows.Count > 0 && c1FlexGrid2.Rows.Count > 1)
				{
					for (int i = 1; i < c1FlexGrid2.Rows.Count; i++)
					{
						if (c1FlexGrid2[i, "pubCode"] != null)
						{
							DataRow[] DR_Chk = DT_Check.Select("pubCode = '" + c1FlexGrid2[i, "pubCode"].ToString().Trim() + "' and pccesCode<>'" + c1FlexGrid2[i, "pccesCode"].ToString().Trim() + "' ");
							if (DR_Chk.Length > 0)
							{
								CellRange rg = c1FlexGrid2.GetCellRange(i, 1, i, c1FlexGrid2.Cols.Count - 1);
								rg.Style = c1FlexGrid2.Styles["CS_Chk"];
								IsOK = false;
							}
						}
					}
				}
				DT_Check = null;
				if (F_CurrentDBName.Trim().ToUpper() != F_TempUseDB.Trim().ToUpper())
				{
					DataTable DT_CheckMrs = stdClass.DBList("Select pccesCode, pubCode from [" + F_CurrentDBName + "].dbo.MrsBaseA ");
					DT_CheckMrs.CaseSensitive = true;
					if (DT_CheckMrs.Rows.Count > 0 && c1FlexGrid2.Rows.Count > 1)
					{
						for (int i = 1; i < c1FlexGrid2.Rows.Count; i++)
						{
							if (c1FlexGrid2[i, "pccesCode"] != null)
							{
								DataRow[] DR_ChkMrs = DT_CheckMrs.Select("pccesCode = '" + c1FlexGrid2[i, "pccesCode"].ToString().Trim() + "' and pubCode<>'" + c1FlexGrid2[i, "pubCode"].ToString().Trim() + "' ");
								if (DR_ChkMrs.Length > 0)
								{
									CellRange rg2 = c1FlexGrid2.GetCellRange(i, 1, i, c1FlexGrid2.Cols.Count - 1);
									rg2.Style = c1FlexGrid2.Styles["CS_ChkMrs"];
									IsOK = false;
								}
							}
						}
					}
					DT_CheckMrs = null;
				}
				stdClass = null;
			}
			tmp_AL1 = null;
		}
		return IsOK;
	}

	private int CheckIsAlreadyExist(string iPubCode)
	{
		int RetV = -1;
		for (int i = 1; i < c1FlexGrid2.Rows.Count; i++)
		{
			if (c1FlexGrid2[i, "PubCode"].ToString() == iPubCode)
			{
				RetV = i;
				break;
			}
		}
		return RetV;
	}

	private void AddIntoGrid2(int IndicateRow)
	{
		c1FlexGrid2.Rows.Count++;
		for (int i = 0; i < gridMrsBase.Cols.Count; i++)
		{
			c1FlexGrid2[c1FlexGrid2.Rows.Count - 1, gridMrsBase.Cols[i].Name] = gridMrsBase[IndicateRow, i];
		}
	}

	private void ultraButton4_Click(object sender, EventArgs e)
	{
		int iCount = 0;
		for (int i = c1FlexGrid2.Rows.Count - 1; i > 0; i--)
		{
			if (c1FlexGrid2.Rows[i].Selected)
			{
				c1FlexGrid2.RemoveItem(i);
			}
		}
		iCount = c1FlexGrid2.Rows.Count - 1;
		ultraLabel3.Text = "已選用工項(" + iCount + ")";
		if (!CheckPccesCodeAndPubCode())
		{
			ultraButton1.Enabled = false;
			MessageBox.Show(this, "發現挑選之工項(紅色項目)曾經換碼，而與預算書內之工項衝突，目前不允許插入該項目，\n\n請校正後再執行。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else
		{
			ultraButton1.Enabled = true;
		}
	}

	private void ultraButton1_Click(object sender, EventArgs e)
	{
		if (c1FlexGrid2.Rows.Count <= 1)
		{
			base.DialogResult = DialogResult.Cancel;
		}
		if (F_ChangeCodeMode)
		{
			Archnowledge.Pcces.DomainModule.MrsBase.MrsBaseA mrsBaseA = new Archnowledge.Pcces.DomainModule.MrsBase.MrsBaseA();
			if (gridMrsBase.Row < gridMrsBase.Rows.Count)
			{
				if (gridMrsBase.Rows[gridMrsBase.Row]["PccesCode"] == null)
				{
					throw new Exception("FormMrsBaseBreakdown_Addnew.cs => ultraButton1_Click()：PccesCode = NULL");
				}
				for (int i = 0; i < dtMrsBaseA.Rows.Count; i++)
				{
					if (dtMrsBaseA.Rows[i]["PccesCode"].ToString() == gridMrsBase.Rows[gridMrsBase.Row]["PccesCode"].ToString())
					{
						if (base.Owner is FormChangeToCompanyCode)
						{
							string[] workItemArray = new string[4]
							{
								dtMrsBaseA.Rows[i]["ExtendCode"].ToString(),
								dtMrsBaseA.Rows[i]["cName"].ToString(),
								dtMrsBaseA.Rows[i]["unitName"].ToString(),
								(dtMrsBaseA.Rows[i]["analysis"] != null && dtMrsBaseA.Rows[i]["analysis"].ToString() == "1") ? "1" : "0"
							};
							(base.Owner as FormChangeToCompanyCode)._companyWorkItemArray = workItemArray;
						}
						break;
					}
				}
			}
			else
			{
				MessageBox.Show("請選擇一筆工項");
			}
			return;
		}
		Form ActiveForm = base.Owner.ActiveMdiChild;
		if (F_CallFormName.ToUpper() == "FORMSYS_D".ToUpper())
		{
			if (gridMrsBase.Row <= 0)
			{
				string sWarning = "請先挑定一筆工項!";
				MessageBox.Show(this, sWarning, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (ActiveForm is frmSysMaintain)
			{
				(ActiveForm as frmSysMaintain)._PccesCode_D = gridMrsBase[gridMrsBase.Row, "PccesCode"].ToString().Trim();
				(ActiveForm as frmSysMaintain)._PccesName_D = gridMrsBase[gridMrsBase.Row, "CName"].ToString().Trim();
				(ActiveForm as frmSysMaintain)._PccesUnit_D = gridMrsBase[gridMrsBase.Row, "unitName"].ToString().Trim();
				(ActiveForm as frmSysMaintain)._PubCode_D = gridMrsBase[gridMrsBase.Row, "PubCode"].ToString().Trim();
			}
			base.DialogResult = DialogResult.OK;
			return;
		}
		DataSet dsTemp = new DataSet("tempDS");
		DataTable dtTemp = new DataTable("tempTable");
		for (int i = 1; i < c1FlexGrid2.Cols.Count; i++)
		{
			DataColumn DC = new DataColumn(c1FlexGrid2.Cols[i].Name, c1FlexGrid2.Cols[i].DataType);
			dtTemp.Columns.Add(DC);
		}
		MrsBaseD mrsBaseD = new MrsBaseD();
		ExecResult ER = new ExecResult();
		string oldCname = "";
		string oldUnitName = "";
		string newPccesCode = "";
		string s = "";
		for (int i = 1; i < c1FlexGrid2.Rows.Count; i++)
		{
			DataRow DR = dtTemp.NewRow();
			for (int j = 0; j < dtTemp.Columns.Count; j++)
			{
				if ((object)c1FlexGrid2.Cols[dtTemp.Columns[j].ColumnName].DataType != Type.GetType("System.String") && c1FlexGrid2[i, dtTemp.Columns[j].ColumnName] == null)
				{
					if (c1FlexGrid2.Cols[dtTemp.Columns[j].ColumnName].Name == "CostDec" || c1FlexGrid2.Cols[dtTemp.Columns[j].ColumnName].Name == "AmtDec")
					{
						DR[c1FlexGrid2.Cols[dtTemp.Columns[j].ColumnName].Name] = DBNull.Value;
					}
					else
					{
						DR[c1FlexGrid2.Cols[dtTemp.Columns[j].ColumnName].Name] = 0;
					}
				}
				else
				{
					DR[c1FlexGrid2.Cols[dtTemp.Columns[j].ColumnName].Name] = c1FlexGrid2[i, dtTemp.Columns[j].ColumnName];
				}
				if (c1FlexGrid2.Cols[dtTemp.Columns[j].ColumnName].Name == "PccesCode")
				{
					string thisPccesCode = c1FlexGrid2[i, dtTemp.Columns[j].ColumnName].ToString();
					ER = mrsBaseD.QueryPccesReplacing(thisPccesCode, out oldCname, out oldUnitName, out newPccesCode);
					if (newPccesCode.Length > 0)
					{
						ER = mrsBaseD.MrsBaseDoverwriteBudProjMrsA(thisPccesCode, _ProjectCode);
						thisPccesCode = newPccesCode;
					}
				}
			}
			DR["memo"] = DR["memo"].ToString().Replace("共通性項目", "").Replace("對照性項目", "");
			dtTemp.Rows.Add(DR);
		}
		dsTemp.Tables.Add(dtTemp);
		SetBackOrigDB();
		if (F_CallFormName.ToUpper() == "frmBudget".ToUpper())
		{
			if (ActiveForm is frmBudget)
			{
				(ActiveForm as frmBudget)._PasteSource_SrcKind = "MRS";
				(ActiveForm as frmBudget)._PasteSource_Project = "";
				(ActiveForm as frmBudget)._FromDBName = F_TempUseDB;
				(ActiveForm as frmBudget).Th_MenuPaste(dsTemp);
			}
		}
		else if (F_CallFormName.ToUpper() == "FormBudgetChange".ToUpper())
		{
			if (ActiveForm is FormBudgetChange)
			{
				(ActiveForm as FormBudgetChange)._PasteSource_SrcKind = "MRS";
				(ActiveForm as FormBudgetChange)._PasteSource_Project = "";
				(ActiveForm as FormBudgetChange)._FromDBName = F_TempUseDB;
				(ActiveForm as FormBudgetChange).Th_MenuPaste(dsTemp);
			}
		}
		else
		{
			(base.Owner as FormMrsBaseBreakdown)._PasteSource = "MRS";
			(base.Owner as FormMrsBaseBreakdown).Th_MenuPaste(dsTemp);
		}
		dsTemp = null;
		dtTemp = null;
	}

	private void SetBackOrigDB()
	{
		SysUser oSysUser = new SysUser();
		oSysUser.SetSysUserDatabaseName(UserID, F_CurrentDBName);
	}

	private void ultraTree1_AfterSelect(object sender, SelectEventArgs e)
	{
		Cursor = Cursors.WaitCursor;
		if (e.NewSelections[0].Key != F_NowKey)
		{
			if (e.NewSelections[0].Key.IndexOf("_") > -1)
			{
				F_NowKey = e.NewSelections[0].Key.Substring(0, e.NewSelections[0].Key.IndexOf("_"));
			}
			else
			{
				F_NowKey = e.NewSelections[0].Key;
			}
			if (e.NewSelections[0].Parent != null)
			{
				if (e.NewSelections[0].Key.IndexOf("_") > -1)
				{
					F_NowKey = e.NewSelections[0].Key.Substring(0, e.NewSelections[0].Key.IndexOf("_"));
				}
				else
				{
					F_ParentKey = e.NewSelections[0].Parent.Key;
				}
			}
			TextFilter = string.Empty;
			BindToGridMrsBaseA();
		}
		Cursor = Cursors.Default;
	}

	private void ultraTree1_KeyDown(object sender, KeyEventArgs e)
	{
		if (ultraTree1.SelectedNodes.Count > 0)
		{
			int iFIND = gridMrsBase.FindRow(ultraTree1.SelectedNodes[0].Key, 1, gridMrsBase.Cols["PccesCode"].SafeIndex, caseSensitive: false, fullMatch: false, wrap: false);
			if (iFIND > -1)
			{
				gridMrsBase.Row = iFIND;
			}
		}
	}

	private void panel5_Resize(object sender, EventArgs e)
	{
		BtnAdd.Left = panel5.Width / 2 - BtnAdd.Width - 5;
		BtnRemove.Left = panel5.Width / 2 + 5;
	}

	private void gridMrsBase_BeforeSort(object sender, SortColEventArgs e)
	{
		if (SortColumnIndex != e.Col)
		{
			SortDirection = "DESC";
		}
		else
		{
			SortDirection = ((SortDirection == "ASC") ? "DESC" : "ASC");
		}
		SortColumnIndex = e.Col;
		e.Cancel = true;
		BindToGridMrsBaseA();
	}

	private void gridMrsBase_DoubleClick(object sender, EventArgs e)
	{
		if (F_CallFormName.ToUpper() == "FORMSYS_D".ToUpper())
		{
			Form ActiveForm = base.Owner.ActiveMdiChild;
			if (gridMrsBase.Row <= 0)
			{
				string sWarning = "請先挑定一筆工項!";
				MessageBox.Show(this, sWarning, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (ActiveForm is frmSysMaintain)
			{
				(ActiveForm as frmSysMaintain)._PccesCode_D = gridMrsBase[gridMrsBase.Row, "PccesCode"].ToString().Trim();
				(ActiveForm as frmSysMaintain)._PccesName_D = gridMrsBase[gridMrsBase.Row, "CName"].ToString().Trim();
				(ActiveForm as frmSysMaintain)._PccesUnit_D = gridMrsBase[gridMrsBase.Row, "unitName"].ToString().Trim();
				(ActiveForm as frmSysMaintain)._PubCode_D = gridMrsBase[gridMrsBase.Row, "PubCode"].ToString().Trim();
			}
			base.DialogResult = DialogResult.OK;
		}
		else
		{
			ultraButton2_Click(this, EventArgs.Empty);
		}
	}

	private void txtFilter_Validating(object sender, CancelEventArgs e)
	{
		if (!CommonMethods.CheckValidString((sender as TextBox).Text))
		{
			e.Cancel = true;
		}
	}

	private void ultraToolbarsManager1_ToolClick(object sender, ToolClickEventArgs e)
	{
		switch (e.Tool.Key)
		{
		case "mnu_Go":
			Do_Filter();
			break;
		case "mnuUsual":
			Do_Usual();
			break;
		case "mnuListAll":
			TypeFilter = "";
			TextFilter = "";
			BindToGridMrsBaseA();
			break;
		case "mnuAnalysis":
			SpecialFilter();
			break;
		case "mnuGeneral":
			SpecialFilter();
			break;
		case "PickType":
			Do_PickClass();
			((StateButtonTool)ultraToolbarsManager1.Tools["mnuGroup"]).Checked = true;
			break;
		}
	}

	private void Do_PickClass()
	{
		FormBDGT_ItemClass FM_ITMSET_Class = new FormBDGT_ItemClass();
		FM_ITMSET_Class._UserID = UserID;
		FM_ITMSET_Class.Owner = this;
		FM_ITMSET_Class._status = "search2";
		if (FM_ITMSET_Class.ShowDialog() == DialogResult.OK)
		{
			TypeFilter = Do_PickType();
			BindToGridMrsBaseA();
		}
		FM_ITMSET_Class.Close();
		FM_ITMSET_Class.Dispose();
		FM_ITMSET_Class = null;
	}

	private string Do_PickType()
	{
		string RetV = string.Empty;
		DataTable DTClass = new DataTable();
		string sNum = CommonMethods.IniReadValue(F_SettingPick, "PickType", "PickName");
		string strpubCode = "";
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(UserID);
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
		return "pubCode = -1";
	}

	private void SpecialFilter()
	{
		TextFilter = string.Empty;
		if ((ultraToolbarsManager1.Tools["mnuAnalysis"] as StateButtonTool).Checked && (ultraToolbarsManager1.Tools["mnuGeneral"] as StateButtonTool).Checked)
		{
			TypeFilter = "";
			BindToGridMrsBaseA();
		}
		else if (!(ultraToolbarsManager1.Tools["mnuAnalysis"] as StateButtonTool).Checked && (ultraToolbarsManager1.Tools["mnuGeneral"] as StateButtonTool).Checked)
		{
			TypeFilter = " analysis <> '1' ";
			BindToGridMrsBaseA();
		}
		else if ((ultraToolbarsManager1.Tools["mnuAnalysis"] as StateButtonTool).Checked && !(ultraToolbarsManager1.Tools["mnuGeneral"] as StateButtonTool).Checked)
		{
			TypeFilter = " analysis = '1' ";
			BindToGridMrsBaseA();
		}
		else if (!(ultraToolbarsManager1.Tools["mnuAnalysis"] as StateButtonTool).Checked && !(ultraToolbarsManager1.Tools["mnuGeneral"] as StateButtonTool).Checked)
		{
			TypeFilter = "";
			BindToGridMrsBaseA();
		}
	}

	private void Do_Filter()
	{
		Cursor = Cursors.WaitCursor;
		string sKeyWord = ((ComboBoxTool)ultraToolbarsManager1.Tools["mnu_Cbo1"]).Text.Trim();
		TextFilter = " ( pccesCode Like '%" + sKeyWord + "%'  Or cName Like '%" + sKeyWord + "%'  Or unitName Like '%" + sKeyWord + "%'  Or eName Like '%" + sKeyWord + "%'  Or eUnit Like '%" + sKeyWord + "%'  ) ";
		BindToGridMrsBaseA();
		if (gridMrsBase.Rows.Count == 1)
		{
			MessageBox.Show(this, "查不到您輸入的關鍵字的資料。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		Cursor = Cursors.Default;
	}

	private void Do_Usual()
	{
		TextFilter = string.Empty;
		if (TypeFilter == " show = '1' ")
		{
			TypeFilter = "";
		}
		else
		{
			TypeFilter = " show = '1' ";
		}
		BindToGridMrsBaseA();
	}

	private void ultraToolbarsManager1_ToolKeyDown(object sender, ToolKeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return && e.Tool.Key == "mnu_Cbo1")
		{
			SendKeys.Send("{TAB}");
			SendKeys.Send("{ENTER}");
		}
	}

	private void FormMrsBaseBreakdown_Addnew_FormClosing(object sender, FormClosingEventArgs e)
	{
		SetBackOrigDB();
		if (base.WindowState != FormWindowState.Maximized)
		{
			CommonMethods.WriteIniValue("MrsBase", "PK_LocationX", base.Location.X.ToString());
			CommonMethods.WriteIniValue("MrsBase", "PK_LocationY", base.Location.Y.ToString());
			CommonMethods.WriteIniValue("MrsBase", "PK_Width", base.Size.Width.ToString());
			CommonMethods.WriteIniValue("MrsBase", "PK_Height", base.Size.Height.ToString());
		}
		CommonMethods.WriteIniValue("MrsBase", "WindowState", base.WindowState.ToString());
	}

	private void ultraButton4_Click_1(object sender, EventArgs e)
	{
		GridUnit1_MouseDown(sender, null);
	}

	private void ultraButton3_Click_1(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.Cancel;
	}

	private void GridUnit1_MouseDown(object sender, MouseEventArgs e)
	{
		if (GridUnit1.MouseRow > 0 && GridUnit1.MouseCol > 0)
		{
			int rowIndex = GridUnit1.MouseRow;
			if (F_flagString == "")
			{
				F_flagString = "IN";
				F_TempUseDB = GridUnit1[rowIndex, "dbName"].ToString();
				lblDBName.Text = "【" + GridUnit1[rowIndex, "dbName"].ToString() + "】" + GridUnit1[rowIndex, "dbDesc"].ToString();
				LoadTreeData();
				Tab_B.Tab.Selected = true;
				F_flagString = "";
			}
			GetNewData();
			if (checkCostStructure() && F_CostUID != string.Empty)
			{
				BindToGridMrsBaseA();
			}
		}
	}

	private void GridUnit1_MouseMove(object sender, MouseEventArgs e)
	{
		int rowIndex = GridUnit1.MouseRow;
		GridUnit1.Row = rowIndex;
		GridUnit1.Select();
	}

	private void FormMrsBaseBreakdown_Addnew_FormClosed(object sender, FormClosedEventArgs e)
	{
		panel2 = null;
		panel3 = null;
		splitter1 = null;
		panel4 = null;
		panel7 = null;
		splitter2 = null;
		panel5 = null;
		panel6 = null;
		ultraLabel1 = null;
		ultraTree1 = null;
		ultraLabel2 = null;
		ultraLabel3 = null;
		panel8 = null;
		ultraButton1 = null;
		imageList2 = null;
		imageList1 = null;
		gridMrsBase = null;
		c1FlexGrid2 = null;
		BtnRemove = null;
		BtnAdd = null;
		ultraButton3 = null;
		ultraStatusBar1 = null;
		Tab_Ctrl = null;
		ultraTabSharedControlsPage1 = null;
		ultraToolbarsManager1 = null;
		_FormSys_B_Toolbars_Dock_Area_Top = null;
		_FormSys_B_Toolbars_Dock_Area_Bottom = null;
		_FormSys_B_Toolbars_Dock_Area_Left = null;
		_FormSys_B_Toolbars_Dock_Area_Right = null;
		panel1 = null;
		BtnExecFlt = null;
		txtFilter = null;
		ultraLabel4 = null;
		Tab_A = null;
		Tab_B = null;
		panel9 = null;
		ultraButton2 = null;
		ultraButton4 = null;
		panel10 = null;
		ultraLabel6 = null;
		ultraLabel8 = null;
		ultraStatusBar2 = null;
		GridUnit1 = null;
		GridColsSquence = null;
		dtMrsBaseA = null;
		dbMrsBase = null;
		aArr = null;
		DT_Nodes = null;
		DT_Leaves = null;
		lblDBName = null;
		GC.Collect();
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.MrsBase.FormMrsBaseBreakdown_Addnew));
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel3 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.OptionSet optionSet1 = new Infragistics.Win.UltraWinToolbars.OptionSet("Toggle1");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Tool1");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool1 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnu_lblFilter");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool1 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_Go");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool1 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuUsual", "Toggle1");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool2 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuGroup", "Toggle1");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool3 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuListAll", "Toggle1");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool4 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuAnalysis", "Toggle1");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool5 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuGeneral", "Toggle1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("PickType");
		Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelete");
		Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool2 = new Infragistics.Win.UltraWinToolbars.LabelTool("mnu_lblFilter");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool2 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo1");
		Infragistics.Win.ValueList valueList1 = new Infragistics.Win.ValueList(0);
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_Go");
		Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool1 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Popup1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool5 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuEdit");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool6 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelete");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool7 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnu_Add");
		Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool8 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuEdit");
		Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool6 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuUsual", "Toggle1");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool7 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuAnalysis", "Toggle1");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool8 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuGeneral", "Toggle1");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool9 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuListAll", "Toggle1");
		Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool10 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("mnuGroup", "Toggle1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool9 = new Infragistics.Win.UltraWinToolbars.ButtonTool("PickType");
		Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel4 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTree.Override _override1 = new Infragistics.Win.UltraWinTree.Override();
		Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		this.Tab_A = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.GridUnit1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.ultraStatusBar2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.panel10 = new System.Windows.Forms.Panel();
		this.ultraLabel8 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.panel9 = new System.Windows.Forms.Panel();
		this.ultraButton2 = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton4 = new Infragistics.Win.Misc.UltraButton();
		this.Tab_B = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel1 = new System.Windows.Forms.Panel();
		this.BtnExecFlt = new Infragistics.Win.Misc.UltraButton();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.txtFilter = new System.Windows.Forms.TextBox();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this._FormSys_B_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.ultraToolbarsManager1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
		this._FormSys_B_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormSys_B_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.panel2 = new System.Windows.Forms.Panel();
		this.panel4 = new System.Windows.Forms.Panel();
		this.panel6 = new System.Windows.Forms.Panel();
		this.gridMrsBase = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.ultraStatusBar1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.panel5 = new System.Windows.Forms.Panel();
		this.BtnRemove = new Infragistics.Win.Misc.UltraButton();
		this.BtnAdd = new Infragistics.Win.Misc.UltraButton();
		this.splitter2 = new System.Windows.Forms.Splitter();
		this.panel7 = new System.Windows.Forms.Panel();
		this.c1FlexGrid2 = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.splitter1 = new System.Windows.Forms.Splitter();
		this.panel3 = new System.Windows.Forms.Panel();
		this.ultraTree1 = new Infragistics.Win.UltraWinTree.UltraTree();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.panel8 = new System.Windows.Forms.Panel();
		this.ultraButton3 = new Infragistics.Win.Misc.UltraButton();
		this.lblDBName = new Infragistics.Win.Misc.UltraLabel();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.Tab_Ctrl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.Tab_A.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.GridUnit1).BeginInit();
		this.panel10.SuspendLayout();
		this.panel9.SuspendLayout();
		this.Tab_B.SuspendLayout();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).BeginInit();
		this.panel2.SuspendLayout();
		this.panel4.SuspendLayout();
		this.panel6.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridMrsBase).BeginInit();
		this.panel5.SuspendLayout();
		this.panel7.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid2).BeginInit();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraTree1).BeginInit();
		this.panel8.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).BeginInit();
		this.Tab_Ctrl.SuspendLayout();
		base.SuspendLayout();
		this.Tab_A.Controls.Add(this.GridUnit1);
		this.Tab_A.Controls.Add(this.ultraStatusBar2);
		this.Tab_A.Controls.Add(this.panel10);
		this.Tab_A.Controls.Add(this.panel9);
		this.Tab_A.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_A.Name = "Tab_A";
		this.Tab_A.Size = new System.Drawing.Size(772, 557);
		this.GridUnit1._ExcelFileName = "";
		this.GridUnit1._ExcelSheeName = "";
		this.GridUnit1._IsOpenExcelAfterExport = false;
		this.GridUnit1.AllowEditing = false;
		this.GridUnit1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
		this.GridUnit1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.GridUnit1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
		this.GridUnit1.ColumnInfo = resources.GetString("GridUnit1.ColumnInfo");
		this.GridUnit1.Cursor = System.Windows.Forms.Cursors.Hand;
		this.GridUnit1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GridUnit1.ExtendLastCol = true;
		this.GridUnit1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.GridUnit1.ForeColor = System.Drawing.Color.Black;
		this.GridUnit1.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.GridUnit1.IsProcessUndo = false;
		this.GridUnit1.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.GridUnit1.Location = new System.Drawing.Point(0, 60);
		this.GridUnit1.Name = "GridUnit1";
		this.GridUnit1.Rows.Count = 1;
		this.GridUnit1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.GridUnit1.ShowCursor = true;
		this.GridUnit1.ShowToolTipOnNarrowColumn = true;
		this.GridUnit1.Size = new System.Drawing.Size(772, 438);
		this.GridUnit1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("GridUnit1.Styles"));
		this.GridUnit1.TabIndex = 23;
		this.GridUnit1.Tree.Column = 1;
		this.GridUnit1.UndoMax = 10;
		this.GridUnit1.MouseDown += new System.Windows.Forms.MouseEventHandler(GridUnit1_MouseDown);
		this.GridUnit1.MouseMove += new System.Windows.Forms.MouseEventHandler(GridUnit1_MouseMove);
		appearance1.BackColor = System.Drawing.SystemColors.Control;
		appearance1.FontData.Name = "細明體";
		appearance1.FontData.SizeInPoints = 11f;
		this.ultraStatusBar2.Appearance = appearance1;
		this.ultraStatusBar2.Location = new System.Drawing.Point(0, 498);
		this.ultraStatusBar2.Name = "ultraStatusBar2";
		ultraStatusPanel1.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel1.Text = "資料筆數:";
		ultraStatusPanel1.Width = 180;
		appearance2.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance2.ForeColor = System.Drawing.Color.Blue;
		ultraStatusPanel2.Appearance = appearance2;
		ultraStatusPanel2.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel2.MarqueeInfo.IsActive = true;
		ultraStatusPanel2.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		ultraStatusPanel2.Style = Infragistics.Win.UltraWinStatusBar.PanelStyle.Marquee;
		ultraStatusPanel2.Width = 101;
		appearance3.TextHAlign = Infragistics.Win.HAlign.Right;
		ultraStatusPanel3.Appearance = appearance3;
		ultraStatusPanel3.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel3.Text = "客服電話:(02)2716-5561";
		ultraStatusPanel3.Width = 200;
		this.ultraStatusBar2.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[3] { ultraStatusPanel1, ultraStatusPanel2, ultraStatusPanel3 });
		this.ultraStatusBar2.Size = new System.Drawing.Size(772, 23);
		this.ultraStatusBar2.SupportThemes = false;
		this.ultraStatusBar2.TabIndex = 22;
		this.ultraStatusBar2.Text = "ultraStatusBar2";
		this.panel10.BackColor = System.Drawing.Color.White;
		this.panel10.Controls.Add(this.ultraLabel8);
		this.panel10.Controls.Add(this.ultraLabel6);
		this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel10.Location = new System.Drawing.Point(0, 0);
		this.panel10.Name = "panel10";
		this.panel10.Size = new System.Drawing.Size(772, 60);
		this.panel10.TabIndex = 13;
		appearance4.BackColor = System.Drawing.Color.White;
		this.ultraLabel8.Appearance = appearance4;
		this.ultraLabel8.Location = new System.Drawing.Point(32, 34);
		this.ultraLabel8.Name = "ultraLabel8";
		this.ultraLabel8.Size = new System.Drawing.Size(620, 20);
		this.ultraLabel8.TabIndex = 4;
		this.ultraLabel8.Text = "請挑選要選用的資料庫來源(用滑鼠點選後會立即進入工項挑選)";
		appearance5.BackColor = System.Drawing.Color.White;
		this.ultraLabel6.Appearance = appearance5;
		this.ultraLabel6.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel6.Location = new System.Drawing.Point(16, 12);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel6.TabIndex = 2;
		this.ultraLabel6.Text = "請先挑選資料庫";
		this.panel9.Controls.Add(this.ultraButton2);
		this.panel9.Controls.Add(this.ultraButton4);
		this.panel9.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel9.Location = new System.Drawing.Point(0, 521);
		this.panel9.Name = "panel9";
		this.panel9.Size = new System.Drawing.Size(772, 36);
		this.panel9.TabIndex = 3;
		this.ultraButton2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance6.BackColor = System.Drawing.SystemColors.ControlLightLight;
		appearance6.BackColor2 = System.Drawing.SystemColors.ControlLight;
		appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance6.Image = resources.GetObject("appearance6.Image");
		appearance6.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton2.Appearance = appearance6;
		this.ultraButton2.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton2.Cursor = System.Windows.Forms.Cursors.Hand;
		this.ultraButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ultraButton2.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraButton2.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton2.Location = new System.Drawing.Point(680, 4);
		this.ultraButton2.Name = "ultraButton2";
		this.ultraButton2.ShowFocusRect = false;
		this.ultraButton2.ShowOutline = false;
		this.ultraButton2.Size = new System.Drawing.Size(90, 28);
		this.ultraButton2.SupportThemes = false;
		this.ultraButton2.TabIndex = 8;
		this.ultraButton2.Text = "結  束";
		this.ultraButton4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance7.BackColor = System.Drawing.SystemColors.ControlLightLight;
		appearance7.BackColor2 = System.Drawing.SystemColors.ControlLight;
		appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance7.Image = resources.GetObject("appearance7.Image");
		appearance7.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance7.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton4.Appearance = appearance7;
		this.ultraButton4.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton4.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraButton4.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton4.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton4.Location = new System.Drawing.Point(588, 4);
		this.ultraButton4.Name = "ultraButton4";
		this.ultraButton4.ShowFocusRect = false;
		this.ultraButton4.ShowOutline = false;
		this.ultraButton4.Size = new System.Drawing.Size(90, 28);
		this.ultraButton4.SupportThemes = false;
		this.ultraButton4.TabIndex = 7;
		this.ultraButton4.Text = "下一步";
		this.ultraButton4.Click += new System.EventHandler(ultraButton4_Click_1);
		this.Tab_B.Controls.Add(this.panel1);
		this.Tab_B.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Bottom);
		this.Tab_B.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Left);
		this.Tab_B.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Right);
		this.Tab_B.Controls.Add(this._FormSys_B_Toolbars_Dock_Area_Top);
		this.Tab_B.Controls.Add(this.panel2);
		this.Tab_B.Controls.Add(this.panel8);
		this.Tab_B.Location = new System.Drawing.Point(0, 0);
		this.Tab_B.Name = "Tab_B";
		this.Tab_B.Size = new System.Drawing.Size(772, 557);
		this.panel1.Controls.Add(this.BtnExecFlt);
		this.panel1.Controls.Add(this.txtFilter);
		this.panel1.Controls.Add(this.ultraLabel4);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 27);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(772, 2);
		this.panel1.TabIndex = 8;
		this.BtnExecFlt.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance8.BackColor = System.Drawing.SystemColors.ControlLightLight;
		appearance8.BackColor2 = System.Drawing.SystemColors.ControlLight;
		appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance8.FontData.SizeInPoints = 11f;
		appearance8.Image = 0;
		appearance8.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnExecFlt.Appearance = appearance8;
		this.BtnExecFlt.BackColor = System.Drawing.Color.Transparent;
		this.BtnExecFlt.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnExecFlt.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.BtnExecFlt.ImageList = this.imageList1;
		this.BtnExecFlt.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.BtnExecFlt.Location = new System.Drawing.Point(665, 3);
		this.BtnExecFlt.Name = "BtnExecFlt";
		this.BtnExecFlt.ShowFocusRect = false;
		this.BtnExecFlt.ShowOutline = false;
		this.BtnExecFlt.Size = new System.Drawing.Size(102, 29);
		this.BtnExecFlt.SupportThemes = false;
		this.BtnExecFlt.TabIndex = 2;
		this.BtnExecFlt.Text = "執行篩選";
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
		this.imageList1.Images.SetKeyName(0, "");
		this.txtFilter.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txtFilter.Location = new System.Drawing.Point(264, 6);
		this.txtFilter.Name = "txtFilter";
		this.txtFilter.Size = new System.Drawing.Size(400, 25);
		this.txtFilter.TabIndex = 1;
		appearance9.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel4.Appearance = appearance9;
		this.ultraLabel4.Location = new System.Drawing.Point(187, 9);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(81, 19);
		this.ultraLabel4.TabIndex = 0;
		this.ultraLabel4.Text = "資料篩選:";
		this._FormSys_B_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 521);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Name = "_FormSys_B_Toolbars_Dock_Area_Bottom";
		this._FormSys_B_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(772, 0);
		this._FormSys_B_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ultraToolbarsManager1;
		appearance10.FontData.Name = "Arial";
		appearance10.FontData.SizeInPoints = 9f;
		this.ultraToolbarsManager1.Appearance = appearance10;
		appearance11.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance11.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.DockAreaAppearance = appearance11;
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
		this.ultraToolbarsManager1.OptionSets.Add(optionSet1);
		this.ultraToolbarsManager1.ShowFullMenusDelay = 500;
		this.ultraToolbarsManager1.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2003;
		ultraToolbar1.DockedColumn = 0;
		ultraToolbar1.DockedRow = 0;
		ultraToolbar1.Settings.AllowCustomize = Infragistics.Win.DefaultableBoolean.False;
		ultraToolbar1.Settings.AllowDockTop = Infragistics.Win.DefaultableBoolean.True;
		ultraToolbar1.Text = "Tool1";
		labelTool1.InstanceProps.IsFirstInGroup = true;
		stateButtonTool1.InstanceProps.IsFirstInGroup = true;
		stateButtonTool2.InstanceProps.IsFirstInGroup = true;
		ultraToolbar1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[9] { labelTool1, comboBoxTool1, buttonTool1, stateButtonTool1, stateButtonTool2, stateButtonTool3, stateButtonTool4, stateButtonTool5, buttonTool2 });
		this.ultraToolbarsManager1.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[1] { ultraToolbar1 });
		appearance15.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance15.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.ToolbarSettings.Appearance = appearance15;
		appearance16.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance16.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance16.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.ToolbarSettings.HotTrackAppearance = appearance16;
		appearance17.BackColor = System.Drawing.Color.FromArgb(255, 128, 0);
		appearance17.BackColor2 = System.Drawing.Color.White;
		appearance17.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		this.ultraToolbarsManager1.ToolbarSettings.PressedAppearance = appearance17;
		appearance18.Image = resources.GetObject("appearance18.Image");
		buttonTool3.SharedProps.AppearancesSmall.Appearance = appearance18;
		buttonTool3.SharedProps.Caption = "刪除";
		buttonTool3.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		labelTool2.SharedProps.Caption = "資料篩選:";
		labelTool2.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		comboBoxTool2.DropDownStyle = Infragistics.Win.DropDownStyle.DropDown;
		comboBoxTool2.SharedProps.Caption = "輸入關鍵字";
		comboBoxTool2.SharedProps.Width = 100;
		comboBoxTool2.ValueList = valueList1;
		appearance19.Image = resources.GetObject("appearance19.Image");
		buttonTool4.SharedProps.AppearancesSmall.Appearance = appearance19;
		buttonTool4.SharedProps.Caption = "Go";
		popupMenuTool1.SharedProps.Caption = "右鍵功能表";
		buttonTool6.InstanceProps.IsFirstInGroup = true;
		popupMenuTool1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[2] { buttonTool5, buttonTool6 });
		appearance20.Image = resources.GetObject("appearance20.Image");
		buttonTool7.SharedProps.AppearancesSmall.Appearance = appearance20;
		buttonTool7.SharedProps.Caption = "新增";
		buttonTool7.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		appearance21.Image = resources.GetObject("appearance21.Image");
		buttonTool8.SharedProps.AppearancesSmall.Appearance = appearance21;
		buttonTool8.SharedProps.Caption = "編輯";
		buttonTool8.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		stateButtonTool6.OptionSetKey = "Toggle1";
		stateButtonTool6.SharedProps.Caption = "檢視常用工項";
		stateButtonTool6.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool7.OptionSetKey = "Toggle1";
		stateButtonTool7.SharedProps.Caption = "有單價分析";
		stateButtonTool7.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool8.OptionSetKey = "Toggle1";
		stateButtonTool8.SharedProps.Caption = "無單價分析";
		stateButtonTool8.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool9.OptionSetKey = "Toggle1";
		stateButtonTool9.SharedProps.Caption = "顯示所有項目";
		stateButtonTool9.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool10.OptionSetKey = "Toggle1";
		stateButtonTool10.SharedProps.Caption = "只顯示選定的類別";
		stateButtonTool10.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		stateButtonTool10.SharedProps.Enabled = false;
		stateButtonTool10.SharedProps.Visible = false;
		buttonTool9.SharedProps.Caption = "類別篩選";
		buttonTool9.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		this.ultraToolbarsManager1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[13]
		{
			buttonTool3, labelTool2, comboBoxTool2, buttonTool4, popupMenuTool1, buttonTool7, buttonTool8, stateButtonTool6, stateButtonTool7, stateButtonTool8,
			stateButtonTool9, stateButtonTool10, buttonTool9
		});
		this.ultraToolbarsManager1.ToolKeyDown += new Infragistics.Win.UltraWinToolbars.ToolKeyEventHandler(ultraToolbarsManager1_ToolKeyDown);
		this.ultraToolbarsManager1.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(ultraToolbarsManager1_ToolClick);
		this._FormSys_B_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
		this._FormSys_B_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 27);
		this._FormSys_B_Toolbars_Dock_Area_Left.Name = "_FormSys_B_Toolbars_Dock_Area_Left";
		this._FormSys_B_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 494);
		this._FormSys_B_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
		this._FormSys_B_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(772, 27);
		this._FormSys_B_Toolbars_Dock_Area_Right.Name = "_FormSys_B_Toolbars_Dock_Area_Right";
		this._FormSys_B_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 494);
		this._FormSys_B_Toolbars_Dock_Area_Right.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormSys_B_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormSys_B_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormSys_B_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
		this._FormSys_B_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormSys_B_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
		this._FormSys_B_Toolbars_Dock_Area_Top.Name = "_FormSys_B_Toolbars_Dock_Area_Top";
		this._FormSys_B_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(772, 27);
		this._FormSys_B_Toolbars_Dock_Area_Top.ToolbarsManager = this.ultraToolbarsManager1;
		this.panel2.Controls.Add(this.panel4);
		this.panel2.Controls.Add(this.splitter1);
		this.panel2.Controls.Add(this.panel3);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(772, 521);
		this.panel2.TabIndex = 1;
		this.panel4.Controls.Add(this.panel6);
		this.panel4.Controls.Add(this.panel5);
		this.panel4.Controls.Add(this.splitter2);
		this.panel4.Controls.Add(this.panel7);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel4.Location = new System.Drawing.Point(185, 0);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(587, 521);
		this.panel4.TabIndex = 2;
		this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel6.Controls.Add(this.gridMrsBase);
		this.panel6.Controls.Add(this.ultraStatusBar1);
		this.panel6.Controls.Add(this.ultraLabel2);
		this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel6.Location = new System.Drawing.Point(0, 0);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(587, 312);
		this.panel6.TabIndex = 6;
		this.gridMrsBase._ExcelFileName = "";
		this.gridMrsBase._ExcelSheeName = "";
		this.gridMrsBase._IsOpenExcelAfterExport = false;
		this.gridMrsBase.AllowEditing = false;
		this.gridMrsBase.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
		this.gridMrsBase.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
		this.gridMrsBase.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridMrsBase.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.gridMrsBase.ColumnInfo = resources.GetString("gridMrsBase.ColumnInfo");
		this.gridMrsBase.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridMrsBase.ExtendLastCol = true;
		this.gridMrsBase.ForeColor = System.Drawing.SystemColors.WindowText;
		this.gridMrsBase.Location = new System.Drawing.Point(0, 28);
		this.gridMrsBase.Name = "gridMrsBase";
		this.gridMrsBase.Rows.Count = 1;
		this.gridMrsBase.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.gridMrsBase.ShowCursor = true;
		this.gridMrsBase.ShowToolTipOnNarrowColumn = true;
		this.gridMrsBase.Size = new System.Drawing.Size(585, 257);
		this.gridMrsBase.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridMrsBase.Styles"));
		this.gridMrsBase.TabIndex = 1;
		this.gridMrsBase.AfterScroll += new C1.Win.C1FlexGrid.RangeEventHandler(gridMrsBase_AfterScroll);
		this.gridMrsBase.BeforeSort += new C1.Win.C1FlexGrid.SortColEventHandler(gridMrsBase_BeforeSort);
		this.gridMrsBase.DoubleClick += new System.EventHandler(gridMrsBase_DoubleClick);
		appearance22.BackColor = System.Drawing.SystemColors.Control;
		appearance22.FontData.SizeInPoints = 11f;
		this.ultraStatusBar1.Appearance = appearance22;
		this.ultraStatusBar1.Location = new System.Drawing.Point(0, 285);
		this.ultraStatusBar1.Name = "ultraStatusBar1";
		ultraStatusPanel4.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel4.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		ultraStatusPanel4.Text = "資料筆數:";
		ultraStatusPanel4.Width = 200;
		this.ultraStatusBar1.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[1] { ultraStatusPanel4 });
		this.ultraStatusBar1.Size = new System.Drawing.Size(585, 25);
		this.ultraStatusBar1.SupportThemes = false;
		this.ultraStatusBar1.TabIndex = 11;
		this.ultraStatusBar1.Text = "ultraStatusBar1";
		appearance23.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		appearance23.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 102);
		appearance23.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance23.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel2.Appearance = appearance23;
		this.ultraLabel2.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.ultraLabel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.ultraLabel2.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel2.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(585, 28);
		this.ultraLabel2.TabIndex = 0;
		this.ultraLabel2.Text = "基本資料庫";
		this.panel5.Controls.Add(this.BtnRemove);
		this.panel5.Controls.Add(this.BtnAdd);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel5.Location = new System.Drawing.Point(0, 312);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(587, 32);
		this.panel5.TabIndex = 5;
		this.panel5.Resize += new System.EventHandler(panel5_Resize);
		appearance24.FontData.Name = "Arial";
		appearance24.FontData.SizeInPoints = 9f;
		appearance24.Image = resources.GetObject("appearance24.Image");
		this.BtnRemove.Appearance = appearance24;
		this.BtnRemove.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.BtnRemove.Location = new System.Drawing.Point(305, 2);
		this.BtnRemove.Name = "BtnRemove";
		this.BtnRemove.ShowFocusRect = false;
		this.BtnRemove.ShowOutline = false;
		this.BtnRemove.Size = new System.Drawing.Size(68, 28);
		this.BtnRemove.SupportThemes = false;
		this.BtnRemove.TabIndex = 1;
		this.BtnRemove.Text = "移除";
		this.BtnRemove.Click += new System.EventHandler(ultraButton4_Click);
		appearance25.FontData.Name = "Arial";
		appearance25.FontData.SizeInPoints = 9f;
		appearance25.Image = resources.GetObject("appearance25.Image");
		this.BtnAdd.Appearance = appearance25;
		this.BtnAdd.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.BtnAdd.Location = new System.Drawing.Point(232, 2);
		this.BtnAdd.Name = "BtnAdd";
		this.BtnAdd.ShowFocusRect = false;
		this.BtnAdd.ShowOutline = false;
		this.BtnAdd.Size = new System.Drawing.Size(68, 28);
		this.BtnAdd.SupportThemes = false;
		this.BtnAdd.TabIndex = 0;
		this.BtnAdd.Text = "加入";
		this.BtnAdd.Click += new System.EventHandler(ultraButton2_Click);
		this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.splitter2.Location = new System.Drawing.Point(0, 344);
		this.splitter2.Name = "splitter2";
		this.splitter2.Size = new System.Drawing.Size(587, 5);
		this.splitter2.TabIndex = 4;
		this.splitter2.TabStop = false;
		this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel7.Controls.Add(this.c1FlexGrid2);
		this.panel7.Controls.Add(this.ultraLabel3);
		this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel7.Location = new System.Drawing.Point(0, 349);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(587, 172);
		this.panel7.TabIndex = 3;
		this.c1FlexGrid2._ExcelFileName = "";
		this.c1FlexGrid2._ExcelSheeName = "";
		this.c1FlexGrid2._IsOpenExcelAfterExport = false;
		this.c1FlexGrid2.AllowEditing = false;
		this.c1FlexGrid2.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
		this.c1FlexGrid2.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
		this.c1FlexGrid2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.c1FlexGrid2.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.c1FlexGrid2.ColumnInfo = resources.GetString("c1FlexGrid2.ColumnInfo");
		this.c1FlexGrid2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.c1FlexGrid2.ExtendLastCol = true;
		this.c1FlexGrid2.ForeColor = System.Drawing.SystemColors.WindowText;
		this.c1FlexGrid2.Location = new System.Drawing.Point(0, 28);
		this.c1FlexGrid2.Name = "c1FlexGrid2";
		this.c1FlexGrid2.Rows.Count = 1;
		this.c1FlexGrid2.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.c1FlexGrid2.ShowCursor = true;
		this.c1FlexGrid2.ShowToolTipOnNarrowColumn = true;
		this.c1FlexGrid2.Size = new System.Drawing.Size(585, 142);
		this.c1FlexGrid2.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("c1FlexGrid2.Styles"));
		this.c1FlexGrid2.TabIndex = 2;
		appearance26.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		appearance26.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 102);
		appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance26.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel3.Appearance = appearance26;
		this.ultraLabel3.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.ultraLabel3.Dock = System.Windows.Forms.DockStyle.Top;
		this.ultraLabel3.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel3.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(585, 28);
		this.ultraLabel3.TabIndex = 0;
		this.ultraLabel3.Text = "已選用工項";
		this.ultraLabel3.Visible = false;
		this.splitter1.Location = new System.Drawing.Point(180, 0);
		this.splitter1.Name = "splitter1";
		this.splitter1.Size = new System.Drawing.Size(5, 521);
		this.splitter1.TabIndex = 1;
		this.splitter1.TabStop = false;
		this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel3.Controls.Add(this.ultraTree1);
		this.panel3.Controls.Add(this.ultraLabel1);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel3.Location = new System.Drawing.Point(0, 0);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(180, 521);
		this.panel3.TabIndex = 0;
		appearance27.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraTree1.Appearance = appearance27;
		this.ultraTree1.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		this.ultraTree1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ultraTree1.HideSelection = false;
		this.ultraTree1.Indent = 15;
		this.ultraTree1.Location = new System.Drawing.Point(0, 28);
		this.ultraTree1.Name = "ultraTree1";
		_override1.SelectionType = Infragistics.Win.UltraWinTree.SelectType.Single;
		this.ultraTree1.Override = _override1;
		this.ultraTree1.Size = new System.Drawing.Size(178, 491);
		this.ultraTree1.TabIndex = 1;
		this.ultraTree1.Click += new System.EventHandler(ultraTree1_Click);
		this.ultraTree1.AfterSelect += new Infragistics.Win.UltraWinTree.AfterNodeSelectEventHandler(ultraTree1_AfterSelect);
		this.ultraTree1.KeyDown += new System.Windows.Forms.KeyEventHandler(ultraTree1_KeyDown);
		appearance28.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		appearance28.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 102);
		appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance28.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel1.Appearance = appearance28;
		this.ultraLabel1.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.ultraLabel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.ultraLabel1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel1.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(178, 28);
		this.ultraLabel1.TabIndex = 0;
		this.ultraLabel1.Text = "工程綱要";
		this.panel8.Controls.Add(this.ultraButton3);
		this.panel8.Controls.Add(this.lblDBName);
		this.panel8.Controls.Add(this.ultraButton1);
		this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel8.Location = new System.Drawing.Point(0, 521);
		this.panel8.Name = "panel8";
		this.panel8.Size = new System.Drawing.Size(772, 36);
		this.panel8.TabIndex = 2;
		this.ultraButton3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance29.Image = resources.GetObject("appearance29.Image");
		appearance29.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton3.Appearance = appearance29;
		this.ultraButton3.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton3.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ultraButton3.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton3.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton3.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton3.Location = new System.Drawing.Point(680, 2);
		this.ultraButton3.Name = "ultraButton3";
		this.ultraButton3.ShowFocusRect = false;
		this.ultraButton3.ShowOutline = false;
		this.ultraButton3.Size = new System.Drawing.Size(88, 31);
		this.ultraButton3.SupportThemes = false;
		this.ultraButton3.TabIndex = 10;
		this.ultraButton3.Text = "取消";
		this.lblDBName.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lblDBName.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblDBName.Location = new System.Drawing.Point(8, 8);
		this.lblDBName.Name = "lblDBName";
		this.lblDBName.Size = new System.Drawing.Size(568, 23);
		this.lblDBName.TabIndex = 9;
		this.lblDBName.Text = "[lblDBName]";
		this.ultraButton1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance30.BackColor = System.Drawing.SystemColors.ControlLightLight;
		appearance30.BackColor2 = System.Drawing.SystemColors.ControlLight;
		appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance30.Image = resources.GetObject("appearance30.Image");
		appearance30.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton1.Appearance = appearance30;
		this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton1.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.ultraButton1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraButton1.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton1.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton1.Location = new System.Drawing.Point(588, 4);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.ShowFocusRect = false;
		this.ultraButton1.ShowOutline = false;
		this.ultraButton1.Size = new System.Drawing.Size(90, 28);
		this.ultraButton1.SupportThemes = false;
		this.ultraButton1.TabIndex = 7;
		this.ultraButton1.Text = "確  定";
		this.ultraButton1.Click += new System.EventHandler(ultraButton1_Click);
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.Magenta;
		this.imageList2.Images.SetKeyName(0, "");
		this.imageList2.Images.SetKeyName(1, "");
		this.Tab_Ctrl.Controls.Add(this.ultraTabSharedControlsPage1);
		this.Tab_Ctrl.Controls.Add(this.Tab_A);
		this.Tab_Ctrl.Controls.Add(this.Tab_B);
		this.Tab_Ctrl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Tab_Ctrl.Location = new System.Drawing.Point(0, 0);
		this.Tab_Ctrl.Name = "Tab_Ctrl";
		this.Tab_Ctrl.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.Tab_Ctrl.Size = new System.Drawing.Size(772, 557);
		this.Tab_Ctrl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
		this.Tab_Ctrl.TabIndex = 12;
		ultraTab1.TabPage = this.Tab_A;
		ultraTab1.Text = "tab1";
		ultraTab2.TabPage = this.Tab_B;
		ultraTab2.Text = "tab2";
		this.Tab_Ctrl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[2] { ultraTab1, ultraTab2 });
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(772, 557);
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.CancelButton = this.ultraButton2;
		base.ClientSize = new System.Drawing.Size(772, 557);
		base.Controls.Add(this.Tab_Ctrl);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.Name = "FormMrsBaseBreakdown_Addnew";
		base.ShowIcon = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "挑選工項";
		base.Load += new System.EventHandler(FormMrsBaseBreakdown_Addnew_Load);
		base.FormClosed += new System.Windows.Forms.FormClosedEventHandler(FormMrsBaseBreakdown_Addnew_FormClosed);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormMrsBaseBreakdown_Addnew_FormClosing);
		this.Tab_A.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.GridUnit1).EndInit();
		this.panel10.ResumeLayout(false);
		this.panel9.ResumeLayout(false);
		this.Tab_B.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).EndInit();
		this.panel2.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
		this.panel6.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridMrsBase).EndInit();
		this.panel5.ResumeLayout(false);
		this.panel7.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid2).EndInit();
		this.panel3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ultraTree1).EndInit();
		this.panel8.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).EndInit();
		this.Tab_Ctrl.ResumeLayout(false);
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
