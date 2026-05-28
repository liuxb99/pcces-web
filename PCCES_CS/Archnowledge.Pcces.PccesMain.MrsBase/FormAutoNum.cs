using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.CommonClass.MrsBase;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.PccesMain.PccesUpdateServices;
using Archnowledge.Pcces.PccesMain.SysMaintain;
using Archnowledge.Pcces.STDClass;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinToolbars;
using Infragistics.Win.UltraWinTree;

namespace Archnowledge.Pcces.PccesMain.MrsBase;

public class FormAutoNum : Form
{
	private AutoNumA_ExtFlag F_AutoNumExtFlag = AutoNumA_ExtFlag.None;

	private bool IsDoneDataBaseAutoNum = false;

	private bool IsChapCodeCustom = false;

	private string F_DEPT_ID = "0";

	private bool IsCustomAutoNum = false;

	private bool IsCustomEdit = false;

	private bool F_IsThisCodeTemp = false;

	private string F_TreeKey = "";

	private string F_surName = "";

	private string F_TreeBindFlag = "";

	private string F_CodeType = "M";

	private string F_AlternativeUnit = "";

	private int[] F_GoBackStep = new int[13];

	private string F_UserID;

	private int iTreeFind = -1;

	private string F_KeyWord = "";

	private SelectedCodeInfo[] myArray = new SelectedCodeInfo[13];

	private SelectedCodeInfo[] Sel_Info = new SelectedCodeInfo[13];

	private int iNowAssembleIndex = 0;

	private AutoNum_EditMode FORM_STATUS = AutoNum_EditMode.Initial;

	private CellStyle Style_Border;

	private CellStyle Style_Border_CanSel;

	private CellStyle Style_Border_Online;

	private CellStyle Style_OnlineCode;

	private CellStyle Style_CanSelectArea;

	private CellStyle Style_Selected;

	private CellStyle Style_Selected1;

	private CellStyle Style_Custom1;

	private CellStyle Style_TextFound;

	private System.Drawing.Printing.Margins _m = new System.Drawing.Printing.Margins(0, 0, 0, 0);

	private SolidBrush _bdrBrush;

	private int _bdrOutside;

	private int _bdrInside;

	private int GridCols = 20;

	private object[,] GridColsSquence;

	private DataTable DT_Nodes = new DataTable();

	private DataTable DT_Leaves = new DataTable();

	private DataTable DT_Leaves12 = new DataTable();

	private DataTable DT_Grid1 = new DataTable();

	private DataTable DT_Auto = new DataTable();

	private bool CustomizedAutoNum = false;

	private int CustomizedAutoNumEndCodeSection = 10;

	private string F_NewCustomCode = "";

	public string F_CustomCode = "";

	public string F_CustomCodeName = "";

	private int[] iMax = new int[11];

	private int[] iMin = new int[11];

	private int iNewRow = 0;

	private UltraToolbarsManager ultraToolbarsManager1;

	private IContainer components;

	private Panel panel1;

	private UltraButton ultraButton3;

	private Panel FormaAutoNum_Fill_Panel;

	private Panel panel2;

	private Panel panel3;

	private Splitter splitter1;

	private Panel panel4;

	private Panel panel5;

	private Splitter splitter2;

	private Panel panel6;

	private UltraLabel ultraLabel1;

	private UltraTree ultraTree1;

	private UltraLabel ultraLabel2;

	private UltraLabel ultraLabel3;

	private GridBudget c1FlexGrid2;

	private GridBudget c1FlexGrid1;

	private UltraToolbarsDockArea _FormAutoNum_Toolbars_Dock_Area_Left;

	private UltraToolbarsDockArea _FormAutoNum_Toolbars_Dock_Area_Right;

	private UltraToolbarsDockArea _FormAutoNum_Toolbars_Dock_Area_Top;

	private UltraToolbarsDockArea _FormAutoNum_Toolbars_Dock_Area_Bottom;

	private ImageList imageList1;

	private UltraButton BtnBack;

	private UltraButton BtnReload;

	private UltraButton ultraButton1;

	private UltraCheckEditor chkCustom;

	private UltraLabel lblUseCode;

	private FormAutoNumFind FM_AUTO_FND;

	public string _NewCustomCode
	{
		get
		{
			return F_NewCustomCode;
		}
		set
		{
			F_NewCustomCode = value;
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

	public string _surName
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

	public FormAutoNum(string userId)
	{
		InitializeComponent();
		Style_Border = c1FlexGrid1.Styles.Add("Border");
		Style_Border_CanSel = c1FlexGrid1.Styles.Add("Border_CanSel");
		Style_Border_Online = c1FlexGrid1.Styles.Add("Border_Online");
		Style_OnlineCode = c1FlexGrid1.Styles.Add("OnlineCode");
		Style_CanSelectArea = c1FlexGrid1.Styles.Add("CanSelect");
		Style_Selected = c1FlexGrid1.Styles.Add("Selected");
		Style_Selected1 = c1FlexGrid1.Styles.Add("Selected1");
		Style_Custom1 = c1FlexGrid1.Styles.Add("Custom1");
		Style_TextFound = c1FlexGrid1.Styles.Add("TextFound");
		Style_Border.BackColor = Color.Transparent;
		Style_Border_CanSel.BackColor = Color.Transparent;
		Style_Border_Online.BackColor = Color.Transparent;
		Style_OnlineCode.BackColor = Color.Transparent;
		Style_OnlineCode.TextAlign = TextAlignEnum.RightCenter;
		Style_CanSelectArea.BackColor = Color.Transparent;
		Style_CanSelectArea.TextAlign = TextAlignEnum.RightCenter;
		Style_Selected.BackColor = Color.Transparent;
		Style_Selected.TextAlign = TextAlignEnum.RightCenter;
		Style_Selected1.BackColor = Color.Transparent;
		Style_Selected1.TextAlign = TextAlignEnum.GeneralCenter;
		Style_Custom1.ForeColor = Color.Magenta;
		Style_Custom1.BackColor = Color.Transparent;
		Style_Custom1.TextAlign = TextAlignEnum.LeftCenter;
		Style_TextFound.BackColor = Color.Gold;
		GridCols = c1FlexGrid1.Cols.Count;
		GridColsSquence = new object[GridCols, 8];
		CellStyle cs = c1FlexGrid1.Styles.Add("img");
		cs.DataType = typeof(Image);
		CustomizedAutoNum = PubTools.GetAppSet_Bool("CustomizedAutoNum");
		RememberColsProps();
		HideCols(IsHide: true);
		F_UserID = userId;
		BindTreeBox();
	}

	private void BindTreeBox()
	{
		F_TreeBindFlag = "BINDTREE";
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
		F_TreeBindFlag = "";
	}

	private void PopulateLevel(ref DataSet dsTreeNodes, UltraTreeNode currentTreeNode)
	{
		UltraTreeNode node = null;
		DataView dvChildren = dsTreeNodes.Tables["AutoNumA"].DefaultView;
		if (currentTreeNode.Key.ToUpper() == "ROOT")
		{
			dvChildren.RowFilter = "[parent] is null";
		}
		else
		{
			dvChildren.RowFilter = "[parent]='" + currentTreeNode.Key + "'";
		}
		dvChildren.Sort = "itemCode ASC";
		foreach (DataRowView drv in dvChildren)
		{
			node = currentTreeNode.Nodes.Add(ArchConvert.Obj2String(drv["itemCode"]), ArchConvert.Obj2String(drv["itemCode"]) + " " + ArchConvert.Obj2String(drv["cName"]) + ArchConvert.Obj2String(drv["IsShow"]));
			PopulateLevel(ref dsTreeNodes, node);
		}
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
		if (chkCustom.Checked)
		{
			DT_Leaves = DBClass1.GetAutoNumA2(F_DEPT_ID);
			DT_Leaves12 = DBClass1.GetAutoNumA2_12();
		}
		else
		{
			DT_Leaves = DBClass1.GetAutoNumA2();
			DT_Leaves12 = DBClass1.GetAutoNumA2_12();
		}
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
			if (itemCode.Length > 1)
			{
				if (itemCode.Substring(0, 2) == "00")
				{
					continue;
				}
			}
			else if (itemCode.Substring(0, 1) == "M" || itemCode.Substring(0, 1) == "W")
			{
				continue;
			}
			node = treeNode.Nodes.Add(itemCode, cName.Trim());
			PopulateLevel2(node);
		}
	}

	private void PopulateLevel2(UltraTreeNode treeNode)
	{
		if (treeNode.Level > 1)
		{
			return;
		}
		treeNode.Nodes.Clear();
		string filterExp = " parent1 = '" + treeNode.Key + "'";
		string sortExp = " itemCode ASC ";
		DataRow[] rows = null;
		rows = ((treeNode.Key.Length != 1 || CustomizedAutoNum) ? DT_Leaves.Select(filterExp, sortExp) : DT_Leaves12.Select(filterExp, sortExp));
		UltraTreeNode node = null;
		string itemCode = "";
		string cName = "";
		string commonName = "";
		DataRow[] array = rows;
		foreach (DataRow row in array)
		{
			itemCode = row["itemCode"] as string;
			cName = row["itemCode"].ToString().Trim() + " " + row["cName"].ToString().Trim();
			commonName = row["commonName"].ToString();
			if (treeNode.Key == "E" && itemCode == "10")
			{
				itemCode = "E10";
			}
			string AliasKey = itemCode + "_" + Guid.NewGuid().ToString();
			node = treeNode.Nodes.Add(AliasKey, cName);
			node.Tag = new ExtendedNodeInfo(typeof(string), "itemCode");
			((ExtendedNodeInfo)node.Tag).CommonName = commonName;
		}
	}

	private void HideCols(bool IsHide)
	{
		if (IsHide)
		{
			c1FlexGrid1.Cols["MinRow06"].Visible = false;
			c1FlexGrid1.Cols["MaxRow06"].Visible = false;
			c1FlexGrid1.Cols["SelfRow06"].Visible = false;
			c1FlexGrid1.Cols["IsCustom06"].Visible = false;
			c1FlexGrid1.Cols["RowID06"].Visible = false;
			c1FlexGrid1.Cols["CustomRowID06"].Visible = false;
			c1FlexGrid1.Cols["MinRow07"].Visible = false;
			c1FlexGrid1.Cols["MaxRow07"].Visible = false;
			c1FlexGrid1.Cols["SelfRow07"].Visible = false;
			c1FlexGrid1.Cols["IsCustom07"].Visible = false;
			c1FlexGrid1.Cols["RowID07"].Visible = false;
			c1FlexGrid1.Cols["CustomRowID07"].Visible = false;
			c1FlexGrid1.Cols["MinRow08"].Visible = false;
			c1FlexGrid1.Cols["MaxRow08"].Visible = false;
			c1FlexGrid1.Cols["SelfRow08"].Visible = false;
			c1FlexGrid1.Cols["IsCustom08"].Visible = false;
			c1FlexGrid1.Cols["RowID08"].Visible = false;
			c1FlexGrid1.Cols["CustomRowID08"].Visible = false;
			c1FlexGrid1.Cols["MinRow09"].Visible = false;
			c1FlexGrid1.Cols["MaxRow09"].Visible = false;
			c1FlexGrid1.Cols["SelfRow09"].Visible = false;
			c1FlexGrid1.Cols["IsCustom09"].Visible = false;
			c1FlexGrid1.Cols["RowID09"].Visible = false;
			c1FlexGrid1.Cols["CustomRowID09"].Visible = false;
			c1FlexGrid1.Cols["MinRow10"].Visible = false;
			c1FlexGrid1.Cols["MaxRow10"].Visible = false;
			c1FlexGrid1.Cols["SelfRow10"].Visible = false;
			c1FlexGrid1.Cols["IsCustom10"].Visible = false;
			c1FlexGrid1.Cols["RowID10"].Visible = false;
			c1FlexGrid1.Cols["CustomRowID10"].Visible = false;
			c1FlexGrid1.Cols["MinRow11"].Visible = false;
			c1FlexGrid1.Cols["MaxRow11"].Visible = false;
			c1FlexGrid1.Cols["SelfRow11"].Visible = false;
			c1FlexGrid1.Cols["IsCustom11"].Visible = false;
			c1FlexGrid1.Cols["RowID11"].Visible = false;
			c1FlexGrid1.Cols["CustomRowID11"].Visible = false;
			c1FlexGrid1.Cols["MinRow12"].Visible = false;
			c1FlexGrid1.Cols["MaxRow12"].Visible = false;
			c1FlexGrid1.Cols["SelfRow12"].Visible = false;
			c1FlexGrid1.Cols["IsCustom12"].Visible = false;
			c1FlexGrid1.Cols["RowID12"].Visible = false;
			c1FlexGrid1.Cols["CustomRowID12"].Visible = false;
			c1FlexGrid1.Cols["RowIDRM"].Visible = false;
			c1FlexGrid1.Cols["SelfRowRM"].Visible = false;
			c1FlexGrid2.Cols["IsCustom"].Visible = false;
		}
	}

	private void ultraTree1_Click(object sender, EventArgs e)
	{
		if (ultraTree1.SelectedNodes.Count > 0)
		{
			int iFIND = c1FlexGrid1.FindRow(ultraTree1.SelectedNodes[0].Key, 1, c1FlexGrid1.Cols["PccesCode"].SafeIndex, caseSensitive: false, fullMatch: false, wrap: false);
			if (iFIND > -1)
			{
				c1FlexGrid1.Row = iFIND;
			}
		}
	}

	private void FormaAutoNum_Load(object sender, EventArgs e)
	{
		Cursor = Cursors.WaitCursor;
		if (F_DEPT_ID == "")
		{
			F_DEPT_ID = "0";
		}
		IsCustomAutoNum = PubTools.GetAppSet_Bool("AutoNumCustom");
		IsCustomEdit = PubTools.GetAppSet_Bool("AutoNumEdit");
		if (IsCustomAutoNum)
		{
			FormSys_G_Info1 FM_INFO = new FormSys_G_Info1();
			FM_INFO._InfoString = "自動編碼資料庫維護載入中，請稍候! ";
			FM_INFO.Show();
			Application.DoEvents();
			Cursor = Cursors.WaitCursor;
			ArrayList aArr = new ArrayList();
			aArr.Clear();
			aArr.Add("PccAdmin");
			aArr.Add("資料庫檢查");
			Cursor = Cursors.Default;
			FM_INFO.Close();
			FM_INFO.Dispose();
			DBClass DBCLS = new DBClass();
			IsDoneDataBaseAutoNum = DBCLS.GetIsUserUseDataBaseSetAutoNum(F_UserID);
			F_DEPT_ID = DBCLS.GetUserUseDataBaseSetAutoNum(F_UserID);
			if (!IsDoneDataBaseAutoNum)
			{
				IsCustomAutoNum = false;
				IsCustomEdit = false;
			}
			if (IsCustomAutoNum)
			{
				chkCustom.Visible = true;
			}
			if (IsCustomEdit)
			{
				if (chkCustom.Checked)
				{
					ultraToolbarsManager1.Tools["mnuCustomCodeEdit"].SharedProps.Visible = true;
					ultraToolbarsManager1.Tools["mnuCustomMainCode"].SharedProps.Visible = true;
					ultraToolbarsManager1.Tools["mnuCustomMainDel"].SharedProps.Visible = true;
					ultraToolbarsManager1.Tools["mnuCustomInsertRow"].SharedProps.Visible = true;
					ultraToolbarsManager1.Tools["mnuCustomNewRow"].SharedProps.Visible = true;
				}
				else
				{
					ultraToolbarsManager1.Tools["mnuCustomCodeEdit"].SharedProps.Visible = false;
					ultraToolbarsManager1.Tools["mnuCustomMainCode"].SharedProps.Visible = false;
					ultraToolbarsManager1.Tools["mnuCustomMainDel"].SharedProps.Visible = false;
					ultraToolbarsManager1.Tools["mnuCustomInsertRow"].SharedProps.Visible = false;
					ultraToolbarsManager1.Tools["mnuCustomNewRow"].SharedProps.Visible = false;
				}
			}
			else
			{
				ultraToolbarsManager1.Tools["mnuCustomMainCode"].SharedProps.Visible = false;
				ultraToolbarsManager1.Tools["mnuCustomMainDel"].SharedProps.Visible = false;
				ultraToolbarsManager1.Tools["mnuCustomInsertRow"].SharedProps.Visible = false;
				ultraToolbarsManager1.Tools["mnuCustomNewRow"].SharedProps.Visible = false;
			}
			DBCLS = null;
			aArr = null;
		}
		else
		{
			ultraToolbarsManager1.Tools["mnuCustomMainCode"].SharedProps.Visible = false;
			ultraToolbarsManager1.Tools["mnuCustomMainDel"].SharedProps.Visible = false;
			ultraToolbarsManager1.Tools["mnuCustomInsertRow"].SharedProps.Visible = false;
			ultraToolbarsManager1.Tools["mnuCustomNewRow"].SharedProps.Visible = false;
		}
		base.WindowState = FormWindowState.Maximized;
		_bdrBrush = new SolidBrush(Color.Red);
		_bdrOutside = 1;
		_bdrInside = 0;
		c1FlexGrid1.DrawMode = DrawModeEnum.OwnerDraw;
		if (CustomizedAutoNum)
		{
			ultraToolbarsManager1.Tools["mnuLiveUpdate"].SharedProps.Visible = false;
		}
		else
		{
			GetUpdateVersion();
		}
		LoadingScreen();
		Cursor = Cursors.Default;
	}

	private void LoadingScreen()
	{
		string Status = CommonMethods.GetIniValue("AutoNum", "WindowState");
		if (Status == "Maximized")
		{
			base.WindowState = FormWindowState.Maximized;
		}
		int iLoc_X = PubTools.Str2Int(CommonMethods.GetIniValue("AutoNum", "PK_LocationX"));
		int iLoc_Y = PubTools.Str2Int(CommonMethods.GetIniValue("AutoNum", "PK_LocationY"));
		int iSiz_W = PubTools.Str2Int(CommonMethods.GetIniValue("AutoNum", "PK_Width"));
		int iSiz_H = PubTools.Str2Int(CommonMethods.GetIniValue("AutoNum", "PK_Height"));
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
	}

	private void ultraTree1_AfterSelect(object sender, SelectEventArgs e)
	{
		if (F_TreeBindFlag != "")
		{
			return;
		}
		ultraTree1.SelectedNodes[0].Expanded = true;
		string SelectedItemKey = "";
		if (ultraTree1.SelectedNodes[0].Key.Trim().IndexOf("_") > -1)
		{
			SelectedItemKey = ultraTree1.SelectedNodes[0].Key.Trim().Substring(0, ultraTree1.SelectedNodes[0].Key.Trim().IndexOf("_"));
		}
		if (SelectedItemKey.Length > 4 || CustomizedAutoNum)
		{
			ClearArray();
			if (F_TreeKey != SelectedItemKey.Trim())
			{
				string AutoNumAExt = GetAutoNumAExt(SelectedItemKey);
				if (AutoNumAExt == "12")
				{
					DT_Grid1 = GetAutoNumB_12M(SelectedItemKey.Trim());
					BindToGrid_12M();
				}
				else
				{
					DT_Grid1 = GetAutoNumB(SelectedItemKey.Trim());
					SetCustomData();
					BindToGrid1();
				}
			}
			F_TreeKey = SelectedItemKey.Trim();
		}
		else
		{
			if (SelectedItemKey.Length == 2 || SelectedItemKey.Length == 1 || SelectedItemKey.Length == 4)
			{
				ClearArray();
				if (F_TreeKey != SelectedItemKey.Trim())
				{
					DT_Grid1 = GetAutoNumB_12(SelectedItemKey.Trim());
					SetCustomData_12();
					BindToGrid1_12();
				}
				F_TreeKey = SelectedItemKey.Trim();
			}
			if (SelectedItemKey.Length == 3 && SelectedItemKey.Trim() == "E10")
			{
				ClearArray();
				if (F_TreeKey != SelectedItemKey.Trim())
				{
					DT_Grid1 = GetAutoNumB_12("10");
					SetCustomData_12();
					BindToGrid1_12();
				}
				F_TreeKey = "E10";
			}
		}
		DBClass DBClass1 = new DBClass();
		F_surName = DBClass1.GetSurName(F_TreeKey);
		DBClass1 = null;
		lblUseCode.Text = "(目前編輯中：" + ultraTree1.SelectedNodes[0].Text + ")";
	}

	private void ClearArray()
	{
		for (int i = 0; i < myArray.Length; i++)
		{
			myArray[i].Clear();
		}
	}

	public void GetAutoNumB_By_Find(string KeyCode, string Keyword)
	{
		bool IsFound = false;
		foreach (UltraTreeNode child1 in ultraTree1.Nodes[0].Nodes)
		{
			foreach (UltraTreeNode childNode in child1.Nodes)
			{
				int iIndex = childNode.Key.IndexOf("_");
				string sKeyCode = childNode.Key.Substring(0, iIndex);
				if (sKeyCode.Contains(KeyCode))
				{
					ultraTree1.ActiveNode = childNode;
					ultraTree1.ActiveNode.Parent.ExpandAll();
					ultraTree1.ActiveNode.Selected = true;
					ultraTree1.ActiveNode.BringIntoView();
					IsFound = true;
					break;
				}
			}
		}
		if (IsFound)
		{
			HighLightKeyword(Keyword);
		}
	}

	public void HighLightKeyword(string Keyword)
	{
		int iRows = c1FlexGrid1.Rows.Count;
		int iCols = c1FlexGrid1.Cols.Count;
		int iFound = 0;
		int iTopRow = 0;
		for (int i = 1; i < iRows; i++)
		{
			for (int j = 0; j < iCols; j++)
			{
				if (c1FlexGrid1[i, j] == null)
				{
					continue;
				}
				string CellText = c1FlexGrid1[i, j].ToString().Trim();
				if (CellText.ToUpper().IndexOf(Keyword.ToUpper()) > -1)
				{
					if (iTopRow == 0)
					{
						iTopRow = i;
					}
					CellRange FoundRg = c1FlexGrid1.GetCellRange(i, j, i, j);
					FoundRg.Style = Style_TextFound;
				}
			}
		}
		c1FlexGrid1.Top = iTopRow;
	}

	private void ultraTree1_Click_1(object sender, EventArgs e)
	{
		if (F_TreeKey.Trim() == "")
		{
			return;
		}
		if (F_TreeKey.Length > 4 || CustomizedAutoNum)
		{
			for (int i = 0; i < myArray.Length; i++)
			{
				myArray[i].Clear();
			}
			DBClass DB_CLASS = new DBClass();
			F_AlternativeUnit = DB_CLASS.GetUserDefine_String("Select AltUnit From AutoNumA Where itemCode ='" + F_TreeKey + "' ", "AltUnit").Trim();
			string AutoNumAExt = GetAutoNumAExt(F_TreeKey);
			if (F_CodeType == "M" && AutoNumAExt == "12")
			{
				F_AutoNumExtFlag = AutoNumA_ExtFlag.Code12;
			}
			else
			{
				F_AutoNumExtFlag = AutoNumA_ExtFlag.None;
			}
			if (AutoNumAExt == "12")
			{
				DT_Grid1 = GetAutoNumB_12M(F_TreeKey);
				BindToGrid_12M();
			}
			else
			{
				DT_Grid1 = GetAutoNumB(F_TreeKey);
				SetCustomData();
				BindToGrid1();
			}
			DB_CLASS = null;
			return;
		}
		try
		{
			if (ultraTree1.SelectedNodes.Count <= 0 || ultraTree1.SelectedNodes[0].Key == "")
			{
				return;
			}
			string SelectedItemKey = "";
			if (ultraTree1.SelectedNodes[0].Key.Trim().IndexOf("_") > -1)
			{
				SelectedItemKey = ultraTree1.SelectedNodes[0].Key.Trim().Substring(0, ultraTree1.SelectedNodes[0].Key.Trim().IndexOf("_"));
			}
			if (SelectedItemKey.Length == 2 || SelectedItemKey.Length == 4)
			{
				for (int i = 0; i < myArray.Length; i++)
				{
					myArray[i].Clear();
				}
				DT_Grid1 = GetAutoNumB_12(SelectedItemKey.Trim());
				SetCustomData_12();
				BindToGrid1_12();
				F_TreeKey = SelectedItemKey.Trim();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(this, "請先結束[自動編碼]程式, 再重新進入一次!!\n" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private string GetAutoNumAExt(string sCode)
	{
		string retV = "";
		DBClass DB_CLASS = new DBClass();
		DB_CLASS._FS_UserID = "PccAdmin";
		return DB_CLASS.GetUserDefine_String("Select Ext From AutoNumA Where itemCode='" + sCode + "' ", "Ext");
	}

	private DataTable GetAutoNumB(string sCode)
	{
		DataSet DS_GetB = new DataSet("AutoNumB");
		DBClass DB_CLASS = new DBClass();
		try
		{
			DB_CLASS._FS_UserID = "PccAdmin";
			string sFlag = "";
			sFlag = ((!(F_DEPT_ID != "0") || !chkCustom.Checked) ? DB_CLASS.GetUserDefine_String("Select WinFormFlag From AutoNumA Where itemCode='" + F_TreeKey + "' And WinformFlag = '2'", "WinFormFlag") : DB_CLASS.GetUserDefine_String("Select WinFormFlag From AutoNumA Where itemCode='" + F_TreeKey + "' And WinformFlag <> '2'", "WinFormFlag"));
			if (sFlag.Trim() == "1" || sFlag.Trim() == "2" || sFlag.Trim() == "")
			{
				IsChapCodeCustom = false;
			}
			else
			{
				IsChapCodeCustom = true;
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormaAutoNum.cs" + ex.Message);
			IsChapCodeCustom = false;
		}
		DS_GetB = (IsChapCodeCustom ? DB_CLASS.GetAutoNumB(sCode, F_DEPT_ID) : DB_CLASS.GetAutoNumB(sCode));
		bool IsExistZero = false;
		if (!PubTools.GetAppSet_Bool("AutoNumCustom"))
		{
			for (int i = 0; i < DS_GetB.Tables[0].Rows.Count; i++)
			{
				if (DS_GetB.Tables[0].Rows[i]["Code10"].ToString().Trim() == "0")
				{
					IsExistZero = true;
					if (F_AlternativeUnit == "")
					{
						DS_GetB.Tables[0].Rows[i]["Code10"] = "";
					}
				}
			}
		}
		if (!IsExistZero && F_AlternativeUnit != "")
		{
			for (int i = 0; i < DS_GetB.Tables[0].Rows.Count; i++)
			{
				if (DS_GetB.Tables[0].Rows[i]["Code10"].ToString().Trim() == "")
				{
					DS_GetB.Tables[0].Rows[i]["Code10"] = "0";
					break;
				}
			}
		}
		if (CustomizedAutoNum && DS_GetB.Tables[0].Columns.Contains("Code11") && DS_GetB.Tables[0].Rows.Count > 0 && DS_GetB.Tables[0].Rows[0]["Code11"].ToString().Trim() != string.Empty)
		{
			CustomizedAutoNumEndCodeSection = 11;
		}
		return DS_GetB.Tables[0];
	}

	private DataTable GetAutoNumB_12(string sCode)
	{
		DataSet DS_GetB = new DataSet("AutoNumB");
		DBClass DB_CLASS = new DBClass();
		try
		{
			DB_CLASS._FS_UserID = "PccAdmin";
			string sFlag = "";
			sFlag = ((!(F_DEPT_ID != "0") || !chkCustom.Checked) ? DB_CLASS.GetUserDefine_String("Select WinFormFlag From AutoNumA Where itemCode='" + F_TreeKey + "' And WinformFlag = '2'", "WinFormFlag") : DB_CLASS.GetUserDefine_String("Select WinFormFlag From AutoNumA Where itemCode='" + F_TreeKey + "' And WinformFlag <> '2'", "WinFormFlag"));
			if (sFlag.Trim() == "1" || sFlag.Trim() == "2" || sFlag.Trim() == "")
			{
				IsChapCodeCustom = false;
			}
			else
			{
				IsChapCodeCustom = true;
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormaAutoNum.cs" + ex.Message);
			IsChapCodeCustom = false;
		}
		DS_GetB = (IsChapCodeCustom ? DB_CLASS.GetAutoNumB(sCode, F_DEPT_ID) : DB_CLASS.GetAutoNumB_12(sCode));
		bool IsExistZero = false;
		for (int i = 0; i < DS_GetB.Tables[0].Rows.Count; i++)
		{
			if (DS_GetB.Tables[0].Rows[i]["Code10"].ToString().Trim() == "0")
			{
				IsExistZero = true;
			}
		}
		if (!IsExistZero)
		{
			for (int i = 0; i < DS_GetB.Tables[0].Rows.Count; i++)
			{
				if (DS_GetB.Tables[0].Rows[i]["Code10"].ToString().Trim() == "")
				{
					DS_GetB.Tables[0].Rows[i]["Code10"] = "0";
					break;
				}
			}
		}
		return DS_GetB.Tables[0];
	}

	private DataTable GetAutoNumB_12M(string sCode)
	{
		DataSet DS_GetB = new DataSet("AutoNumB");
		DBClass DB_CLASS = new DBClass();
		try
		{
			DB_CLASS._FS_UserID = "PccAdmin";
			string sFlag = "";
			sFlag = ((!(F_DEPT_ID != "0") || !chkCustom.Checked) ? DB_CLASS.GetUserDefine_String("Select WinFormFlag From AutoNumA Where itemCode='" + F_TreeKey + "' And WinformFlag = '2'", "WinFormFlag") : DB_CLASS.GetUserDefine_String("Select WinFormFlag From AutoNumA Where itemCode='" + F_TreeKey + "' And WinformFlag <> '2'", "WinFormFlag"));
			if (sFlag.Trim() == "1" || sFlag.Trim() == "2" || sFlag.Trim() == "")
			{
				IsChapCodeCustom = false;
			}
			else
			{
				IsChapCodeCustom = true;
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormaAutoNum.cs" + ex.Message);
			IsChapCodeCustom = false;
		}
		DS_GetB = (IsChapCodeCustom ? DB_CLASS.GetAutoNumB(sCode, F_DEPT_ID) : DB_CLASS.GetAutoNumB_12M(sCode));
		bool IsExistZero = false;
		for (int i = 0; i < DS_GetB.Tables[0].Rows.Count; i++)
		{
			if (DS_GetB.Tables[0].Rows[i]["Code10"].ToString().Trim() == "0")
			{
				IsExistZero = true;
			}
		}
		if (!IsExistZero)
		{
			for (int i = 0; i < DS_GetB.Tables[0].Rows.Count; i++)
			{
				if (DS_GetB.Tables[0].Rows[i]["Code10"].ToString().Trim() == "")
				{
					DS_GetB.Tables[0].Rows[i]["Code10"] = "0";
					break;
				}
			}
		}
		return DS_GetB.Tables[0];
	}

	private void SetCustomData()
	{
		DBClass DB_CLASS = new DBClass();
		string ssSQL = "Select * From AutoNumB Where ChapCode='" + F_TreeKey + "' ";
		if (!IsCustomAutoNum || !chkCustom.Checked)
		{
			return;
		}
		ssSQL = ssSQL + " And IsCustom = 'Y' And Version='" + F_DEPT_ID + "' ";
		DataTable DT_Cust = DB_CLASS.GetUserDefine(ssSQL);
		for (int i = 0; i < DT_Cust.Rows.Count; i++)
		{
			switch (DT_Cust.Rows[i]["CodeSection"].ToString().Trim())
			{
			case "06":
			{
				for (int j = 0; j < DT_Grid1.Rows.Count; j++)
				{
					if (DT_Grid1.Rows[j]["SelfRow06"].ToString() == DT_Cust.Rows[i]["SelfRow"].ToString())
					{
						DT_Grid1.Rows[j]["Code06"] = DT_Cust.Rows[i]["Code"];
						DT_Grid1.Rows[j]["Content06"] = DT_Cust.Rows[i]["Content"];
						DT_Grid1.Rows[j]["MinRow06"] = DT_Cust.Rows[i]["MinRow"];
						DT_Grid1.Rows[j]["MaxRow06"] = DT_Cust.Rows[i]["MaxRow"];
						DT_Grid1.Rows[j]["CustomRowID06"] = DT_Cust.Rows[i]["RowID"];
						if (IsCustomAutoNum)
						{
							DT_Grid1.Rows[j]["IsCustom06"] = "Y";
						}
					}
				}
				break;
			}
			case "07":
			{
				for (int j = 0; j < DT_Grid1.Rows.Count; j++)
				{
					if (DT_Grid1.Rows[j]["SelfRow07"].ToString() == DT_Cust.Rows[i]["SelfRow"].ToString())
					{
						DT_Grid1.Rows[j]["Code07"] = DT_Cust.Rows[i]["Code"];
						DT_Grid1.Rows[j]["Content07"] = DT_Cust.Rows[i]["Content"];
						DT_Grid1.Rows[j]["MinRow07"] = DT_Cust.Rows[i]["MinRow"];
						DT_Grid1.Rows[j]["MaxRow07"] = DT_Cust.Rows[i]["MaxRow"];
						DT_Grid1.Rows[j]["CustomRowID07"] = DT_Cust.Rows[i]["RowID"];
						if (IsCustomAutoNum)
						{
							DT_Grid1.Rows[j]["IsCustom07"] = "Y";
						}
					}
				}
				break;
			}
			case "08":
			{
				for (int j = 0; j < DT_Grid1.Rows.Count; j++)
				{
					if (DT_Grid1.Rows[j]["SelfRow08"].ToString() == DT_Cust.Rows[i]["SelfRow"].ToString())
					{
						DT_Grid1.Rows[j]["Code08"] = DT_Cust.Rows[i]["Code"];
						DT_Grid1.Rows[j]["Content08"] = DT_Cust.Rows[i]["Content"];
						DT_Grid1.Rows[j]["MinRow08"] = DT_Cust.Rows[i]["MinRow"];
						DT_Grid1.Rows[j]["MaxRow08"] = DT_Cust.Rows[i]["MaxRow"];
						DT_Grid1.Rows[j]["CustomRowID08"] = DT_Cust.Rows[i]["RowID"];
						if (IsCustomAutoNum)
						{
							DT_Grid1.Rows[j]["IsCustom08"] = "Y";
						}
					}
				}
				break;
			}
			case "09":
			{
				for (int j = 0; j < DT_Grid1.Rows.Count; j++)
				{
					if (DT_Grid1.Rows[j]["SelfRow09"].ToString() == DT_Cust.Rows[i]["SelfRow"].ToString())
					{
						DT_Grid1.Rows[j]["Code09"] = DT_Cust.Rows[i]["Code"];
						DT_Grid1.Rows[j]["Content09"] = DT_Cust.Rows[i]["Content"];
						DT_Grid1.Rows[j]["MinRow09"] = DT_Cust.Rows[i]["MinRow"];
						DT_Grid1.Rows[j]["MaxRow09"] = DT_Cust.Rows[i]["MaxRow"];
						DT_Grid1.Rows[j]["CustomRowID09"] = DT_Cust.Rows[i]["RowID"];
						if (IsCustomAutoNum)
						{
							DT_Grid1.Rows[j]["IsCustom09"] = "Y";
						}
					}
				}
				break;
			}
			case "10":
			{
				for (int j = 0; j < DT_Grid1.Rows.Count; j++)
				{
					if (DT_Grid1.Rows[j]["SelfRow10"].ToString() == DT_Cust.Rows[i]["SelfRow"].ToString())
					{
						DT_Grid1.Rows[j]["Code10"] = DT_Cust.Rows[i]["Code"];
						DT_Grid1.Rows[j]["Content10"] = DT_Cust.Rows[i]["Content"];
						DT_Grid1.Rows[j]["MinRow10"] = DT_Cust.Rows[i]["MinRow"];
						DT_Grid1.Rows[j]["MaxRow10"] = DT_Cust.Rows[i]["MaxRow"];
						DT_Grid1.Rows[j]["CustomRowID10"] = DT_Cust.Rows[i]["RowID"];
						if (IsCustomAutoNum)
						{
							DT_Grid1.Rows[j]["IsCustom10"] = "Y";
						}
					}
				}
				break;
			}
			}
		}
		int _iMin = 4;
		for (int i = 0; i < DT_Grid1.Rows.Count; i++)
		{
			if (DT_Grid1.Rows[i]["IsCustom06"].ToString() == "Y" && (int)DT_Grid1.Rows[i]["MinRow06"] > _iMin)
			{
				GoDown(i, (int)DT_Grid1.Rows[i]["MinRow06"], "06");
				GoUp(i, (int)DT_Grid1.Rows[i]["SelfRow06"] - 1, "06");
			}
			_iMin = (int)DT_Grid1.Rows[i]["MinRow06"];
		}
		_iMin = 4;
		for (int i = 0; i < DT_Grid1.Rows.Count; i++)
		{
			if (DT_Grid1.Rows[i]["IsCustom07"].ToString() == "Y" && (int)DT_Grid1.Rows[i]["MinRow07"] > _iMin)
			{
				GoDown(i, (int)DT_Grid1.Rows[i]["MinRow07"], "07");
				GoUp(i, (int)DT_Grid1.Rows[i]["SelfRow07"] - 1, "07");
			}
			_iMin = (int)DT_Grid1.Rows[i]["MinRow07"];
		}
		_iMin = 4;
		for (int i = 0; i < DT_Grid1.Rows.Count; i++)
		{
			if (DT_Grid1.Rows[i]["IsCustom08"].ToString() == "Y" && (int)DT_Grid1.Rows[i]["MinRow08"] > _iMin)
			{
				GoDown(i, (int)DT_Grid1.Rows[i]["MinRow08"], "08");
				GoUp(i, (int)DT_Grid1.Rows[i]["SelfRow08"] - 1, "08");
			}
			_iMin = (int)DT_Grid1.Rows[i]["MinRow08"];
		}
		_iMin = 4;
		for (int i = 0; i < DT_Grid1.Rows.Count; i++)
		{
			if (DT_Grid1.Rows[i]["IsCustom09"].ToString() == "Y" && (int)DT_Grid1.Rows[i]["MinRow09"] > _iMin)
			{
				GoDown(i, (int)DT_Grid1.Rows[i]["MinRow09"], "09");
				GoUp(i, (int)DT_Grid1.Rows[i]["SelfRow09"] - 1, "09");
			}
			_iMin = (int)DT_Grid1.Rows[i]["MinRow09"];
		}
		_iMin = 4;
		for (int i = 0; i < DT_Grid1.Rows.Count; i++)
		{
			if (DT_Grid1.Rows[i]["IsCustom10"].ToString() == "Y" && (int)DT_Grid1.Rows[i]["MinRow10"] > _iMin)
			{
				GoDown(i, (int)DT_Grid1.Rows[i]["MinRow10"], "10");
				GoUp(i, (int)DT_Grid1.Rows[i]["SelfRow10"] - 1, "10");
			}
			_iMin = (int)DT_Grid1.Rows[i]["MinRow10"];
		}
		DB_CLASS = null;
	}

	private void SetCustomData_12()
	{
		DBClass DB_CLASS = new DBClass();
		string ssSQL = "Select * From AutoNumB_12 Where ChapCode='" + F_TreeKey + "' ";
		if (!IsCustomAutoNum || !chkCustom.Checked)
		{
			return;
		}
		ssSQL = ssSQL + " And IsCustom = 'Y' And Version='" + F_DEPT_ID + "' ";
		DataTable DT_Cust = DB_CLASS.GetUserDefine(ssSQL);
		for (int i = 0; i < DT_Cust.Rows.Count; i++)
		{
			switch (DT_Cust.Rows[i]["CodeSection"].ToString().Trim())
			{
			case "06":
			{
				for (int j = 0; j < DT_Grid1.Rows.Count; j++)
				{
					if (DT_Grid1.Rows[j]["SelfRow06"].ToString() == DT_Cust.Rows[i]["SelfRow"].ToString())
					{
						DT_Grid1.Rows[j]["Code06"] = DT_Cust.Rows[i]["Code"];
						DT_Grid1.Rows[j]["Content06"] = DT_Cust.Rows[i]["Content"];
						DT_Grid1.Rows[j]["MinRow06"] = DT_Cust.Rows[i]["MinRow"];
						DT_Grid1.Rows[j]["MaxRow06"] = DT_Cust.Rows[i]["MaxRow"];
						DT_Grid1.Rows[j]["CustomRowID06"] = DT_Cust.Rows[i]["RowID"];
						if (IsCustomAutoNum)
						{
							DT_Grid1.Rows[j]["IsCustom06"] = "Y";
						}
					}
				}
				break;
			}
			case "07":
			{
				for (int j = 0; j < DT_Grid1.Rows.Count; j++)
				{
					if (DT_Grid1.Rows[j]["SelfRow07"].ToString() == DT_Cust.Rows[i]["SelfRow"].ToString())
					{
						DT_Grid1.Rows[j]["Code07"] = DT_Cust.Rows[i]["Code"];
						DT_Grid1.Rows[j]["Content07"] = DT_Cust.Rows[i]["Content"];
						DT_Grid1.Rows[j]["MinRow07"] = DT_Cust.Rows[i]["MinRow"];
						DT_Grid1.Rows[j]["MaxRow07"] = DT_Cust.Rows[i]["MaxRow"];
						DT_Grid1.Rows[j]["CustomRowID07"] = DT_Cust.Rows[i]["RowID"];
						if (IsCustomAutoNum)
						{
							DT_Grid1.Rows[j]["IsCustom07"] = "Y";
						}
					}
				}
				break;
			}
			case "08":
			{
				for (int j = 0; j < DT_Grid1.Rows.Count; j++)
				{
					if (DT_Grid1.Rows[j]["SelfRow08"].ToString() == DT_Cust.Rows[i]["SelfRow"].ToString())
					{
						DT_Grid1.Rows[j]["Code08"] = DT_Cust.Rows[i]["Code"];
						DT_Grid1.Rows[j]["Content08"] = DT_Cust.Rows[i]["Content"];
						DT_Grid1.Rows[j]["MinRow08"] = DT_Cust.Rows[i]["MinRow"];
						DT_Grid1.Rows[j]["MaxRow08"] = DT_Cust.Rows[i]["MaxRow"];
						DT_Grid1.Rows[j]["CustomRowID08"] = DT_Cust.Rows[i]["RowID"];
						if (IsCustomAutoNum)
						{
							DT_Grid1.Rows[j]["IsCustom08"] = "Y";
						}
					}
				}
				break;
			}
			case "09":
			{
				for (int j = 0; j < DT_Grid1.Rows.Count; j++)
				{
					if (DT_Grid1.Rows[j]["SelfRow09"].ToString() == DT_Cust.Rows[i]["SelfRow"].ToString())
					{
						DT_Grid1.Rows[j]["Code09"] = DT_Cust.Rows[i]["Code"];
						DT_Grid1.Rows[j]["Content09"] = DT_Cust.Rows[i]["Content"];
						DT_Grid1.Rows[j]["MinRow09"] = DT_Cust.Rows[i]["MinRow"];
						DT_Grid1.Rows[j]["MaxRow09"] = DT_Cust.Rows[i]["MaxRow"];
						DT_Grid1.Rows[j]["CustomRowID09"] = DT_Cust.Rows[i]["RowID"];
						if (IsCustomAutoNum)
						{
							DT_Grid1.Rows[j]["IsCustom09"] = "Y";
						}
					}
				}
				break;
			}
			case "10":
			{
				for (int j = 0; j < DT_Grid1.Rows.Count; j++)
				{
					if (DT_Grid1.Rows[j]["SelfRow10"].ToString() == DT_Cust.Rows[i]["SelfRow"].ToString())
					{
						DT_Grid1.Rows[j]["Code10"] = DT_Cust.Rows[i]["Code"];
						DT_Grid1.Rows[j]["Content10"] = DT_Cust.Rows[i]["Content"];
						DT_Grid1.Rows[j]["MinRow10"] = DT_Cust.Rows[i]["MinRow"];
						DT_Grid1.Rows[j]["MaxRow10"] = DT_Cust.Rows[i]["MaxRow"];
						DT_Grid1.Rows[j]["CustomRowID10"] = DT_Cust.Rows[i]["RowID"];
						if (IsCustomAutoNum)
						{
							DT_Grid1.Rows[j]["IsCustom10"] = "Y";
						}
					}
				}
				break;
			}
			case "11":
			{
				for (int j = 0; j < DT_Grid1.Rows.Count; j++)
				{
					if (DT_Grid1.Rows[j]["SelfRow11"].ToString() == DT_Cust.Rows[i]["SelfRow"].ToString())
					{
						DT_Grid1.Rows[j]["Code11"] = DT_Cust.Rows[i]["Code"];
						DT_Grid1.Rows[j]["Content11"] = DT_Cust.Rows[i]["Content"];
						DT_Grid1.Rows[j]["MinRow11"] = DT_Cust.Rows[i]["MinRow"];
						DT_Grid1.Rows[j]["MaxRow11"] = DT_Cust.Rows[i]["MaxRow"];
						DT_Grid1.Rows[j]["CustomRowID11"] = DT_Cust.Rows[i]["RowID"];
						if (IsCustomAutoNum)
						{
							DT_Grid1.Rows[j]["IsCustom11"] = "Y";
						}
					}
				}
				break;
			}
			}
		}
		int _iMin = 4;
		for (int i = 0; i < DT_Grid1.Rows.Count; i++)
		{
			if (DT_Grid1.Rows[i]["IsCustom06"].ToString() == "Y" && (int)DT_Grid1.Rows[i]["MinRow06"] > _iMin)
			{
				GoDown(i, (int)DT_Grid1.Rows[i]["MinRow06"], "06");
				GoUp(i, (int)DT_Grid1.Rows[i]["SelfRow06"] - 1, "06");
			}
			_iMin = (int)DT_Grid1.Rows[i]["MinRow06"];
		}
		_iMin = 4;
		for (int i = 0; i < DT_Grid1.Rows.Count; i++)
		{
			if (DT_Grid1.Rows[i]["IsCustom07"].ToString() == "Y" && (int)DT_Grid1.Rows[i]["MinRow07"] > _iMin)
			{
				GoDown(i, (int)DT_Grid1.Rows[i]["MinRow07"], "07");
				GoUp(i, (int)DT_Grid1.Rows[i]["SelfRow07"] - 1, "07");
			}
			_iMin = (int)DT_Grid1.Rows[i]["MinRow07"];
		}
		_iMin = 4;
		for (int i = 0; i < DT_Grid1.Rows.Count; i++)
		{
			if (DT_Grid1.Rows[i]["IsCustom08"].ToString() == "Y" && (int)DT_Grid1.Rows[i]["MinRow08"] > _iMin)
			{
				GoDown(i, (int)DT_Grid1.Rows[i]["MinRow08"], "08");
				GoUp(i, (int)DT_Grid1.Rows[i]["SelfRow08"] - 1, "08");
			}
			_iMin = (int)DT_Grid1.Rows[i]["MinRow08"];
		}
		_iMin = 4;
		for (int i = 0; i < DT_Grid1.Rows.Count; i++)
		{
			if (DT_Grid1.Rows[i]["IsCustom09"].ToString() == "Y" && (int)DT_Grid1.Rows[i]["MinRow09"] > _iMin)
			{
				GoDown(i, (int)DT_Grid1.Rows[i]["MinRow09"], "09");
				GoUp(i, (int)DT_Grid1.Rows[i]["SelfRow09"] - 1, "09");
			}
			_iMin = (int)DT_Grid1.Rows[i]["MinRow09"];
		}
		_iMin = 4;
		for (int i = 0; i < DT_Grid1.Rows.Count; i++)
		{
			if (DT_Grid1.Rows[i]["IsCustom10"].ToString() == "Y" && (int)DT_Grid1.Rows[i]["MinRow10"] > _iMin)
			{
				GoDown(i, (int)DT_Grid1.Rows[i]["MinRow10"], "10");
				GoUp(i, (int)DT_Grid1.Rows[i]["SelfRow10"] - 1, "10");
			}
			_iMin = (int)DT_Grid1.Rows[i]["MinRow10"];
		}
		_iMin = 4;
		for (int i = 0; i < DT_Grid1.Rows.Count; i++)
		{
			if (DT_Grid1.Rows[i]["IsCustom11"].ToString() == "Y" && (int)DT_Grid1.Rows[i]["MinRow11"] > _iMin)
			{
				GoDown(i, (int)DT_Grid1.Rows[i]["MinRow11"], "11");
				GoUp(i, (int)DT_Grid1.Rows[i]["SelfRow11"] - 1, "11");
			}
			_iMin = (int)DT_Grid1.Rows[i]["MinRow11"];
		}
		DB_CLASS = null;
	}

	private void GoDown(int iRow, int CurrMin, string Col)
	{
		for (int i = iRow + 1; i < DT_Grid1.Rows.Count; i++)
		{
			if ((int)DT_Grid1.Rows[i]["MinRow" + Col] < CurrMin)
			{
				DT_Grid1.Rows[i]["MinRow" + Col] = CurrMin;
			}
		}
	}

	private void GoUp(int iRow, int CurrSelf, string Col)
	{
		for (int i = iRow - 1; i >= 0; i--)
		{
			if ((int)DT_Grid1.Rows[i]["MaxRow" + Col] > CurrSelf)
			{
				DT_Grid1.Rows[i]["MaxRow" + Col] = CurrSelf;
			}
		}
	}

	private void BindToGrid1()
	{
		Cursor = Cursors.WaitCursor;
		c1FlexGrid1.Visible = false;
		c1FlexGrid1.Redraw = false;
		c1FlexGrid1.Cols["ResType"].Visible = true;
		RememberColsProps();
		c1FlexGrid1.Clear(ClearFlags.All);
		c1FlexGrid1.Select(0, 0);
		c1FlexGrid1.Rows.Count = DT_Grid1.Rows.Count + 1;
		SetGridColumn();
		for (int i = 0; i < DT_Grid1.Rows.Count; i++)
		{
			if (chkCustom.Checked)
			{
				c1FlexGrid1[2, "ResType"] = "S";
			}
			if (DT_Grid1.Rows[0]["resType"].ToString().Trim() != "")
			{
				if (CustomizedAutoNum && DT_Grid1.Rows[0]["resType"].ToString().Contains(","))
				{
					string[] resTypes = DT_Grid1.Rows[0]["resType"].ToString().Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);
					for (int j = 0; j < resTypes.Length; j++)
					{
						c1FlexGrid1[j + 1, "ResType"] = resTypes[j].Trim();
					}
				}
				else
				{
					c1FlexGrid1[1, "ResType"] = DT_Grid1.Rows[0]["resType"].ToString().Trim();
				}
			}
			c1FlexGrid1[i + 1, "Code06"] = DT_Grid1.Rows[i]["Code06"].ToString().Trim();
			c1FlexGrid1[i + 1, "Content06"] = DT_Grid1.Rows[i]["Content06"].ToString().Trim();
			c1FlexGrid1[i + 1, "MinRow06"] = DT_Grid1.Rows[i]["MinRow06"].ToString().Trim();
			c1FlexGrid1[i + 1, "MaxRow06"] = DT_Grid1.Rows[i]["MaxRow06"].ToString().Trim();
			c1FlexGrid1[i + 1, "SelfRow06"] = DT_Grid1.Rows[i]["SelfRow06"].ToString().Trim();
			if (IsCustomAutoNum)
			{
				c1FlexGrid1[i + 1, "RowID06"] = DT_Grid1.Rows[i]["RowID06"].ToString().Trim();
				c1FlexGrid1[i + 1, "CustomRowID06"] = DT_Grid1.Rows[i]["CustomRowID06"];
			}
			c1FlexGrid1[i + 1, "IsCustom06"] = "N";
			if (IsCustomAutoNum)
			{
				c1FlexGrid1[i + 1, "IsCustom06"] = DT_Grid1.Rows[i]["IsCustom06"].ToString().Trim();
				if (DT_Grid1.Rows[i]["IsCustom06"].ToString().Trim() == "Y")
				{
					CellRange CR1 = c1FlexGrid1.GetCellRange(i + 1, c1FlexGrid1.Cols["Content06"].SafeIndex);
					CR1.Style = Style_Custom1;
				}
			}
			c1FlexGrid1[i + 1, "Code07"] = DT_Grid1.Rows[i]["Code07"].ToString().Trim();
			c1FlexGrid1[i + 1, "Content07"] = DT_Grid1.Rows[i]["Content07"].ToString().Trim();
			c1FlexGrid1[i + 1, "MinRow07"] = DT_Grid1.Rows[i]["MinRow07"].ToString().Trim();
			c1FlexGrid1[i + 1, "MaxRow07"] = DT_Grid1.Rows[i]["MaxRow07"].ToString().Trim();
			c1FlexGrid1[i + 1, "SelfRow07"] = DT_Grid1.Rows[i]["SelfRow07"].ToString().Trim();
			if (IsCustomAutoNum)
			{
				c1FlexGrid1[i + 1, "RowID07"] = DT_Grid1.Rows[i]["RowID07"].ToString().Trim();
				c1FlexGrid1[i + 1, "CustomRowID07"] = DT_Grid1.Rows[i]["CustomRowID07"];
			}
			c1FlexGrid1[i + 1, "IsCustom07"] = "N";
			if (IsCustomAutoNum)
			{
				c1FlexGrid1[i + 1, "IsCustom07"] = DT_Grid1.Rows[i]["IsCustom07"].ToString().Trim();
				if (c1FlexGrid1[i + 1, "IsCustom07"].ToString() == "Y")
				{
					CellRange CR1 = c1FlexGrid1.GetCellRange(i + 1, c1FlexGrid1.Cols["Content07"].SafeIndex);
					CR1.Style = Style_Custom1;
				}
			}
			c1FlexGrid1[i + 1, "Code08"] = DT_Grid1.Rows[i]["Code08"].ToString().Trim();
			c1FlexGrid1[i + 1, "Content08"] = DT_Grid1.Rows[i]["Content08"].ToString().Trim();
			c1FlexGrid1[i + 1, "MinRow08"] = DT_Grid1.Rows[i]["MinRow08"].ToString().Trim();
			c1FlexGrid1[i + 1, "MaxRow08"] = DT_Grid1.Rows[i]["MaxRow08"].ToString().Trim();
			c1FlexGrid1[i + 1, "SelfRow08"] = DT_Grid1.Rows[i]["SelfRow08"].ToString().Trim();
			if (IsCustomAutoNum)
			{
				c1FlexGrid1[i + 1, "RowID08"] = DT_Grid1.Rows[i]["RowID08"].ToString().Trim();
				c1FlexGrid1[i + 1, "CustomRowID08"] = DT_Grid1.Rows[i]["CustomRowID08"];
			}
			c1FlexGrid1[i + 1, "IsCustom08"] = "N";
			if (IsCustomAutoNum)
			{
				c1FlexGrid1[i + 1, "IsCustom08"] = DT_Grid1.Rows[i]["IsCustom08"].ToString().Trim();
				if (c1FlexGrid1[i + 1, "IsCustom08"].ToString() == "Y")
				{
					CellRange CR1 = c1FlexGrid1.GetCellRange(i + 1, c1FlexGrid1.Cols["Content08"].SafeIndex);
					CR1.Style = Style_Custom1;
				}
			}
			c1FlexGrid1[i + 1, "Code09"] = DT_Grid1.Rows[i]["Code09"].ToString().Trim();
			c1FlexGrid1[i + 1, "Content09"] = DT_Grid1.Rows[i]["Content09"].ToString().Trim();
			c1FlexGrid1[i + 1, "MinRow09"] = DT_Grid1.Rows[i]["MinRow09"].ToString().Trim();
			c1FlexGrid1[i + 1, "MaxRow09"] = DT_Grid1.Rows[i]["MaxRow09"].ToString().Trim();
			c1FlexGrid1[i + 1, "SelfRow09"] = DT_Grid1.Rows[i]["SelfRow09"].ToString().Trim();
			if (IsCustomAutoNum)
			{
				c1FlexGrid1[i + 1, "RowID09"] = DT_Grid1.Rows[i]["RowID09"].ToString().Trim();
				c1FlexGrid1[i + 1, "CustomRowID09"] = DT_Grid1.Rows[i]["CustomRowID09"];
			}
			c1FlexGrid1[i + 1, "IsCustom09"] = "N";
			if (IsCustomAutoNum)
			{
				c1FlexGrid1[i + 1, "IsCustom09"] = DT_Grid1.Rows[i]["IsCustom09"].ToString().Trim();
				if (c1FlexGrid1[i + 1, "IsCustom09"].ToString() == "Y")
				{
					CellRange CR1 = c1FlexGrid1.GetCellRange(i + 1, c1FlexGrid1.Cols["Content09"].SafeIndex);
					CR1.Style = Style_Custom1;
				}
			}
			c1FlexGrid1[i + 1, "Code10"] = DT_Grid1.Rows[i]["Code10"].ToString().Trim();
			c1FlexGrid1[i + 1, "Content10"] = DT_Grid1.Rows[i]["Content10"].ToString().Trim();
			c1FlexGrid1[i + 1, "MinRow10"] = DT_Grid1.Rows[i]["MinRow10"].ToString().Trim();
			c1FlexGrid1[i + 1, "MaxRow10"] = DT_Grid1.Rows[i]["MaxRow10"].ToString().Trim();
			c1FlexGrid1[i + 1, "SelfRow10"] = DT_Grid1.Rows[i]["SelfRow10"].ToString().Trim();
			if (IsCustomAutoNum)
			{
				c1FlexGrid1[i + 1, "RowID10"] = DT_Grid1.Rows[i]["RowID10"].ToString().Trim();
				c1FlexGrid1[i + 1, "CustomRowID10"] = DT_Grid1.Rows[i]["CustomRowID10"];
			}
			c1FlexGrid1[i + 1, "IsCustom10"] = "N";
			if (IsCustomAutoNum)
			{
				c1FlexGrid1[i + 1, "IsCustom10"] = DT_Grid1.Rows[i]["IsCustom10"].ToString().Trim();
				if (c1FlexGrid1[i + 1, "IsCustom10"].ToString() == "Y")
				{
					CellRange CR1 = c1FlexGrid1.GetCellRange(i + 1, c1FlexGrid1.Cols["Content10"].SafeIndex);
					CR1.Style = Style_Custom1;
				}
			}
			if (CustomizedAutoNum && CustomizedAutoNumEndCodeSection == 11)
			{
				c1FlexGrid1[i + 1, "Code11"] = DT_Grid1.Rows[i]["Code11"].ToString().Trim();
				c1FlexGrid1[i + 1, "Content11"] = DT_Grid1.Rows[i]["Content11"].ToString().Trim();
				c1FlexGrid1[i + 1, "MinRow11"] = DT_Grid1.Rows[i]["MinRow11"].ToString().Trim();
				c1FlexGrid1[i + 1, "MaxRow11"] = DT_Grid1.Rows[i]["MaxRow11"].ToString().Trim();
				c1FlexGrid1[i + 1, "SelfRow11"] = DT_Grid1.Rows[i]["SelfRow11"].ToString().Trim();
				if (IsCustomAutoNum)
				{
					c1FlexGrid1[i + 1, "RowID11"] = DT_Grid1.Rows[i]["RowID11"].ToString().Trim();
					c1FlexGrid1[i + 1, "CustomRowID11"] = DT_Grid1.Rows[i]["CustomRowID11"];
				}
				c1FlexGrid1[i + 1, "IsCustom11"] = "N";
				if (IsCustomAutoNum)
				{
					c1FlexGrid1[i + 1, "IsCustom11"] = DT_Grid1.Rows[i]["IsCustom11"].ToString().Trim();
					if (c1FlexGrid1[i + 1, "IsCustom11"].ToString() == "Y")
					{
						CellRange CR1 = c1FlexGrid1.GetCellRange(i + 1, c1FlexGrid1.Cols["Content11"].SafeIndex);
						CR1.Style = Style_Custom1;
					}
				}
			}
			c1FlexGrid1[i + 1, "ContentRM"] = DT_Grid1.Rows[i]["ContentRM"].ToString().Trim();
			if (IsCustomAutoNum)
			{
				c1FlexGrid1[i + 1, "SelfRowRM"] = DT_Grid1.Rows[i]["SelfRowRM"].ToString().Trim();
				c1FlexGrid1[i + 1, "RowIDRM"] = DT_Grid1.Rows[i]["RowIDRM"].ToString().Trim();
			}
		}
		try
		{
			if (DT_Grid1.Rows[0]["resType"].ToString().Trim() != "")
			{
				F_CodeType = DT_Grid1.Rows[0]["resType"].ToString().Trim();
			}
			else
			{
				F_CodeType = "";
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormaAutoNum.cs" + ex.Message);
			Console.Write(ex.Message);
			F_CodeType = "M";
		}
		DrawValidLines(100);
		Cursor = Cursors.Default;
		Grid1StatusCtrl();
		if (F_CodeType == "" || F_CodeType == "M" || F_CodeType == "W" || CustomizedAutoNum)
		{
			StartEdit(6);
		}
		else if (F_CodeType == "E" || F_CodeType == "L")
		{
			StartEdit(7);
		}
		if (chkCustom.Visible && !chkCustom.Checked)
		{
			for (int i = 1; i < c1FlexGrid1.Rows.Count - 1; i++)
			{
				if (c1FlexGrid1[i, "Code06"].ToString().Trim() == "" && c1FlexGrid1[i, "Code07"].ToString().Trim() == "" && c1FlexGrid1[i, "Code08"].ToString().Trim() == "" && c1FlexGrid1[i, "Code09"].ToString().Trim() == "" && c1FlexGrid1[i, "Code10"].ToString().Trim() == "" && c1FlexGrid1[i, "ContentRM"].ToString().Trim() == "")
				{
					c1FlexGrid1.Rows[i].Visible = false;
				}
			}
		}
		c1FlexGrid1.Visible = true;
		c1FlexGrid1.Redraw = true;
		c1FlexGrid1.Enabled = true;
	}

	private void BindToGrid_12M()
	{
		Cursor = Cursors.WaitCursor;
		c1FlexGrid1.Visible = false;
		c1FlexGrid1.Redraw = false;
		c1FlexGrid1.Cols["ResType"].Visible = true;
		RememberColsProps();
		c1FlexGrid1.Clear(ClearFlags.All);
		c1FlexGrid1.Select(0, 0);
		c1FlexGrid1.Rows.Count = DT_Grid1.Rows.Count + 1;
		SetGridColumn();
		for (int i = 0; i < DT_Grid1.Rows.Count; i++)
		{
			if (chkCustom.Checked)
			{
				c1FlexGrid1[2, "ResType"] = "S";
			}
			if (DT_Grid1.Rows[0]["resType"].ToString().Trim() != "")
			{
				if (CustomizedAutoNum && DT_Grid1.Rows[0]["resType"].ToString().Contains(","))
				{
					string[] resTypes = DT_Grid1.Rows[0]["resType"].ToString().Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);
					for (int j = 0; j < resTypes.Length; j++)
					{
						c1FlexGrid1[j + 1, "ResType"] = resTypes[j].Trim();
					}
				}
				else
				{
					c1FlexGrid1[1, "ResType"] = DT_Grid1.Rows[0]["resType"].ToString().Trim();
				}
			}
			c1FlexGrid1[i + 1, "Code06"] = DT_Grid1.Rows[i]["Code06"].ToString().Trim();
			c1FlexGrid1[i + 1, "Content06"] = DT_Grid1.Rows[i]["Content06"].ToString().Trim();
			c1FlexGrid1[i + 1, "MinRow06"] = DT_Grid1.Rows[i]["MinRow06"].ToString().Trim();
			c1FlexGrid1[i + 1, "MaxRow06"] = DT_Grid1.Rows[i]["MaxRow06"].ToString().Trim();
			c1FlexGrid1[i + 1, "SelfRow06"] = DT_Grid1.Rows[i]["SelfRow06"].ToString().Trim();
			if (IsCustomAutoNum)
			{
				c1FlexGrid1[i + 1, "RowID06"] = DT_Grid1.Rows[i]["RowID06"].ToString().Trim();
				c1FlexGrid1[i + 1, "CustomRowID06"] = DT_Grid1.Rows[i]["CustomRowID06"];
			}
			c1FlexGrid1[i + 1, "IsCustom06"] = "N";
			if (IsCustomAutoNum)
			{
				c1FlexGrid1[i + 1, "IsCustom06"] = DT_Grid1.Rows[i]["IsCustom06"].ToString().Trim();
				if (DT_Grid1.Rows[i]["IsCustom06"].ToString().Trim() == "Y")
				{
					CellRange CR1 = c1FlexGrid1.GetCellRange(i + 1, c1FlexGrid1.Cols["Content06"].SafeIndex);
					CR1.Style = Style_Custom1;
				}
			}
			c1FlexGrid1[i + 1, "Code07"] = DT_Grid1.Rows[i]["Code07"].ToString().Trim();
			c1FlexGrid1[i + 1, "Content07"] = DT_Grid1.Rows[i]["Content07"].ToString().Trim();
			c1FlexGrid1[i + 1, "MinRow07"] = DT_Grid1.Rows[i]["MinRow07"].ToString().Trim();
			c1FlexGrid1[i + 1, "MaxRow07"] = DT_Grid1.Rows[i]["MaxRow07"].ToString().Trim();
			c1FlexGrid1[i + 1, "SelfRow07"] = DT_Grid1.Rows[i]["SelfRow07"].ToString().Trim();
			if (IsCustomAutoNum)
			{
				c1FlexGrid1[i + 1, "RowID07"] = DT_Grid1.Rows[i]["RowID07"].ToString().Trim();
				c1FlexGrid1[i + 1, "CustomRowID07"] = DT_Grid1.Rows[i]["CustomRowID07"];
			}
			c1FlexGrid1[i + 1, "IsCustom07"] = "N";
			if (IsCustomAutoNum)
			{
				c1FlexGrid1[i + 1, "IsCustom07"] = DT_Grid1.Rows[i]["IsCustom07"].ToString().Trim();
				if (c1FlexGrid1[i + 1, "IsCustom07"].ToString() == "Y")
				{
					CellRange CR1 = c1FlexGrid1.GetCellRange(i + 1, c1FlexGrid1.Cols["Content07"].SafeIndex);
					CR1.Style = Style_Custom1;
				}
			}
			c1FlexGrid1[i + 1, "Code08"] = DT_Grid1.Rows[i]["Code08"].ToString().Trim();
			c1FlexGrid1[i + 1, "Content08"] = DT_Grid1.Rows[i]["Content08"].ToString().Trim();
			c1FlexGrid1[i + 1, "MinRow08"] = DT_Grid1.Rows[i]["MinRow08"].ToString().Trim();
			c1FlexGrid1[i + 1, "MaxRow08"] = DT_Grid1.Rows[i]["MaxRow08"].ToString().Trim();
			c1FlexGrid1[i + 1, "SelfRow08"] = DT_Grid1.Rows[i]["SelfRow08"].ToString().Trim();
			if (IsCustomAutoNum)
			{
				c1FlexGrid1[i + 1, "RowID08"] = DT_Grid1.Rows[i]["RowID08"].ToString().Trim();
				c1FlexGrid1[i + 1, "CustomRowID08"] = DT_Grid1.Rows[i]["CustomRowID08"];
			}
			c1FlexGrid1[i + 1, "IsCustom08"] = "N";
			if (IsCustomAutoNum)
			{
				c1FlexGrid1[i + 1, "IsCustom08"] = DT_Grid1.Rows[i]["IsCustom08"].ToString().Trim();
				if (c1FlexGrid1[i + 1, "IsCustom08"].ToString() == "Y")
				{
					CellRange CR1 = c1FlexGrid1.GetCellRange(i + 1, c1FlexGrid1.Cols["Content08"].SafeIndex);
					CR1.Style = Style_Custom1;
				}
			}
			c1FlexGrid1[i + 1, "Code09"] = DT_Grid1.Rows[i]["Code09"].ToString().Trim();
			c1FlexGrid1[i + 1, "Content09"] = DT_Grid1.Rows[i]["Content09"].ToString().Trim();
			c1FlexGrid1[i + 1, "MinRow09"] = DT_Grid1.Rows[i]["MinRow09"].ToString().Trim();
			c1FlexGrid1[i + 1, "MaxRow09"] = DT_Grid1.Rows[i]["MaxRow09"].ToString().Trim();
			c1FlexGrid1[i + 1, "SelfRow09"] = DT_Grid1.Rows[i]["SelfRow09"].ToString().Trim();
			if (IsCustomAutoNum)
			{
				c1FlexGrid1[i + 1, "RowID09"] = DT_Grid1.Rows[i]["RowID09"].ToString().Trim();
				c1FlexGrid1[i + 1, "CustomRowID09"] = DT_Grid1.Rows[i]["CustomRowID09"];
			}
			c1FlexGrid1[i + 1, "IsCustom09"] = "N";
			if (IsCustomAutoNum)
			{
				c1FlexGrid1[i + 1, "IsCustom09"] = DT_Grid1.Rows[i]["IsCustom09"].ToString().Trim();
				if (c1FlexGrid1[i + 1, "IsCustom09"].ToString() == "Y")
				{
					CellRange CR1 = c1FlexGrid1.GetCellRange(i + 1, c1FlexGrid1.Cols["Content09"].SafeIndex);
					CR1.Style = Style_Custom1;
				}
			}
			c1FlexGrid1[i + 1, "Code10"] = DT_Grid1.Rows[i]["Code10"].ToString().Trim();
			c1FlexGrid1[i + 1, "Content10"] = DT_Grid1.Rows[i]["Content10"].ToString().Trim();
			c1FlexGrid1[i + 1, "MinRow10"] = DT_Grid1.Rows[i]["MinRow10"].ToString().Trim();
			c1FlexGrid1[i + 1, "MaxRow10"] = DT_Grid1.Rows[i]["MaxRow10"].ToString().Trim();
			c1FlexGrid1[i + 1, "SelfRow10"] = DT_Grid1.Rows[i]["SelfRow10"].ToString().Trim();
			if (IsCustomAutoNum)
			{
				c1FlexGrid1[i + 1, "RowID10"] = DT_Grid1.Rows[i]["RowID10"].ToString().Trim();
				c1FlexGrid1[i + 1, "CustomRowID10"] = DT_Grid1.Rows[i]["CustomRowID10"];
			}
			c1FlexGrid1[i + 1, "IsCustom10"] = "N";
			if (IsCustomAutoNum)
			{
				c1FlexGrid1[i + 1, "IsCustom10"] = DT_Grid1.Rows[i]["IsCustom10"].ToString().Trim();
				if (c1FlexGrid1[i + 1, "IsCustom10"].ToString() == "Y")
				{
					CellRange CR1 = c1FlexGrid1.GetCellRange(i + 1, c1FlexGrid1.Cols["Content10"].SafeIndex);
					CR1.Style = Style_Custom1;
				}
			}
			c1FlexGrid1[i + 1, "Code11"] = DT_Grid1.Rows[i]["Code11"].ToString().Trim();
			c1FlexGrid1[i + 1, "Content11"] = DT_Grid1.Rows[i]["Content11"].ToString().Trim();
			c1FlexGrid1[i + 1, "MinRow11"] = DT_Grid1.Rows[i]["MinRow11"].ToString().Trim();
			c1FlexGrid1[i + 1, "MaxRow11"] = DT_Grid1.Rows[i]["MaxRow11"].ToString().Trim();
			c1FlexGrid1[i + 1, "SelfRow11"] = DT_Grid1.Rows[i]["SelfRow11"].ToString().Trim();
			if (IsCustomAutoNum)
			{
				c1FlexGrid1[i + 1, "RowID11"] = DT_Grid1.Rows[i]["RowID11"].ToString().Trim();
				c1FlexGrid1[i + 1, "CustomRowID11"] = DT_Grid1.Rows[i]["CustomRowID11"];
			}
			c1FlexGrid1[i + 1, "IsCustom11"] = "N";
			if (IsCustomAutoNum)
			{
				c1FlexGrid1[i + 1, "IsCustom11"] = DT_Grid1.Rows[i]["IsCustom11"].ToString().Trim();
				if (c1FlexGrid1[i + 1, "IsCustom11"].ToString() == "Y")
				{
					CellRange CR1 = c1FlexGrid1.GetCellRange(i + 1, c1FlexGrid1.Cols["Content11"].SafeIndex);
					CR1.Style = Style_Custom1;
				}
			}
			c1FlexGrid1[i + 1, "Code12"] = DT_Grid1.Rows[i]["Code12"].ToString().Trim();
			c1FlexGrid1[i + 1, "Content12"] = DT_Grid1.Rows[i]["Content12"].ToString().Trim();
			c1FlexGrid1[i + 1, "MinRow12"] = DT_Grid1.Rows[i]["MinRow12"].ToString().Trim();
			c1FlexGrid1[i + 1, "MaxRow12"] = DT_Grid1.Rows[i]["MaxRow12"].ToString().Trim();
			c1FlexGrid1[i + 1, "SelfRow12"] = DT_Grid1.Rows[i]["SelfRow12"].ToString().Trim();
			if (IsCustomAutoNum)
			{
				c1FlexGrid1[i + 1, "RowID12"] = DT_Grid1.Rows[i]["RowID12"].ToString().Trim();
				c1FlexGrid1[i + 1, "CustomRowID12"] = DT_Grid1.Rows[i]["CustomRowID12"];
			}
			c1FlexGrid1[i + 1, "IsCustom12"] = "N";
			if (IsCustomAutoNum)
			{
				c1FlexGrid1[i + 1, "IsCustom12"] = DT_Grid1.Rows[i]["IsCustom12"].ToString().Trim();
				if (c1FlexGrid1[i + 1, "IsCustom12"].ToString() == "Y")
				{
					CellRange CR1 = c1FlexGrid1.GetCellRange(i + 1, c1FlexGrid1.Cols["Content12"].SafeIndex);
					CR1.Style = Style_Custom1;
				}
			}
			if (CustomizedAutoNum && CustomizedAutoNumEndCodeSection == 13)
			{
				c1FlexGrid1[i + 1, "Code13"] = DT_Grid1.Rows[i]["Code13"].ToString().Trim();
				c1FlexGrid1[i + 1, "Content13"] = DT_Grid1.Rows[i]["Content13"].ToString().Trim();
				c1FlexGrid1[i + 1, "MinRow13"] = DT_Grid1.Rows[i]["MinRow13"].ToString().Trim();
				c1FlexGrid1[i + 1, "MaxRow13"] = DT_Grid1.Rows[i]["MaxRow13"].ToString().Trim();
				c1FlexGrid1[i + 1, "SelfRow13"] = DT_Grid1.Rows[i]["SelfRow13"].ToString().Trim();
				if (IsCustomAutoNum)
				{
					c1FlexGrid1[i + 1, "RowID13"] = DT_Grid1.Rows[i]["RowID13"].ToString().Trim();
					c1FlexGrid1[i + 1, "CustomRowID13"] = DT_Grid1.Rows[i]["CustomRowID13"];
				}
				c1FlexGrid1[i + 1, "IsCustom13"] = "N";
				if (IsCustomAutoNum)
				{
					c1FlexGrid1[i + 1, "IsCustom13"] = DT_Grid1.Rows[i]["IsCustom11"].ToString().Trim();
					if (c1FlexGrid1[i + 1, "IsCustom13"].ToString() == "Y")
					{
						CellRange CR1 = c1FlexGrid1.GetCellRange(i + 1, c1FlexGrid1.Cols["Content13"].SafeIndex);
						CR1.Style = Style_Custom1;
					}
				}
			}
			c1FlexGrid1[i + 1, "ContentRM"] = DT_Grid1.Rows[i]["ContentRM"].ToString().Trim();
			if (IsCustomAutoNum)
			{
				c1FlexGrid1[i + 1, "SelfRowRM"] = DT_Grid1.Rows[i]["SelfRowRM"].ToString().Trim();
				c1FlexGrid1[i + 1, "RowIDRM"] = DT_Grid1.Rows[i]["RowIDRM"].ToString().Trim();
			}
		}
		try
		{
			if (DT_Grid1.Rows[0]["resType"].ToString().Trim() != "")
			{
				F_CodeType = DT_Grid1.Rows[0]["resType"].ToString().Trim();
			}
			else
			{
				F_CodeType = "";
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormaAutoNum.cs" + ex.Message);
			Console.Write(ex.Message);
			F_CodeType = "M";
		}
		DrawValidLines(100);
		Cursor = Cursors.Default;
		Grid1StatusCtrl();
		if (F_CodeType == "" || F_CodeType == "M" || F_CodeType == "W" || CustomizedAutoNum)
		{
			StartEdit(6);
		}
		else if (F_CodeType == "E" || F_CodeType == "L")
		{
			StartEdit(7);
		}
		if (chkCustom.Visible && !chkCustom.Checked)
		{
			for (int i = 1; i < c1FlexGrid1.Rows.Count - 1; i++)
			{
				if (c1FlexGrid1[i, "Code06"].ToString().Trim() == "" && c1FlexGrid1[i, "Code07"].ToString().Trim() == "" && c1FlexGrid1[i, "Code08"].ToString().Trim() == "" && c1FlexGrid1[i, "Code09"].ToString().Trim() == "" && c1FlexGrid1[i, "Code10"].ToString().Trim() == "" && c1FlexGrid1[i, "Code11"].ToString().Trim() == "" && c1FlexGrid1[i, "Code12"].ToString().Trim() == "" && c1FlexGrid1[i, "ContentRM"].ToString().Trim() == "")
				{
					c1FlexGrid1.Rows[i].Visible = false;
				}
			}
		}
		c1FlexGrid1.Visible = true;
		c1FlexGrid1.Redraw = true;
		c1FlexGrid1.Enabled = true;
	}

	private void BindToGrid1_12()
	{
		Cursor = Cursors.WaitCursor;
		c1FlexGrid1.Visible = false;
		c1FlexGrid1.Redraw = false;
		c1FlexGrid1.Cols["ResType"].Visible = true;
		RememberColsProps();
		c1FlexGrid1.Clear(ClearFlags.All);
		c1FlexGrid1.Select(0, 0);
		c1FlexGrid1.Rows.Count = DT_Grid1.Rows.Count + 1;
		SetGridColumn();
		for (int i = 0; i < DT_Grid1.Rows.Count; i++)
		{
			if (chkCustom.Checked)
			{
				c1FlexGrid1[2, "ResType"] = "S";
			}
			if (DT_Grid1.Rows[0]["resType"].ToString().Trim() != "")
			{
				c1FlexGrid1[1, "ResType"] = DT_Grid1.Rows[0]["resType"].ToString().Trim();
			}
			switch (i)
			{
			case 0:
				c1FlexGrid1[i + 1, "Code06"] = "0";
				c1FlexGrid1[i + 1, "Content06"] = "00000";
				break;
			case 1:
				c1FlexGrid1[i + 1, "Code06"] = "◆";
				c1FlexGrid1[i + 1, "Content06"] = "綱要編碼";
				break;
			default:
				c1FlexGrid1[i + 1, "Code06"] = "";
				c1FlexGrid1[i + 1, "Content06"] = "";
				break;
			}
			c1FlexGrid1[i + 1, "MinRow06"] = DT_Grid1.Rows[i]["MinRow06"].ToString().Trim();
			c1FlexGrid1[i + 1, "MaxRow06"] = DT_Grid1.Rows[i]["MaxRow06"].ToString().Trim();
			c1FlexGrid1[i + 1, "SelfRow06"] = DT_Grid1.Rows[i]["SelfRow06"].ToString().Trim();
			if (IsCustomAutoNum)
			{
				c1FlexGrid1[i + 1, "RowID06"] = DT_Grid1.Rows[i]["RowID06"].ToString().Trim();
				c1FlexGrid1[i + 1, "CustomRowID06"] = DT_Grid1.Rows[i]["CustomRowID06"];
			}
			c1FlexGrid1[i + 1, "IsCustom06"] = "N";
			if (IsCustomAutoNum)
			{
				c1FlexGrid1[i + 1, "IsCustom06"] = DT_Grid1.Rows[i]["IsCustom06"].ToString().Trim();
				if (DT_Grid1.Rows[i]["IsCustom06"].ToString().Trim() == "Y")
				{
					CellRange CR1 = c1FlexGrid1.GetCellRange(i + 1, c1FlexGrid1.Cols["Content06"].SafeIndex);
					CR1.Style = Style_Custom1;
				}
			}
			if (DT_Grid1.Rows[i]["Code06"].ToString().Trim().IndexOf(".") > -1)
			{
				c1FlexGrid1[i + 1, "Code07"] = "";
				c1FlexGrid1[i + 1, "Content07"] = DT_Grid1.Rows[i]["Code06"].ToString().Trim() + DT_Grid1.Rows[i]["Content06"].ToString().Trim();
			}
			else
			{
				c1FlexGrid1[i + 1, "Code07"] = DT_Grid1.Rows[i]["Code06"].ToString().Trim();
				c1FlexGrid1[i + 1, "Content07"] = DT_Grid1.Rows[i]["Content06"].ToString().Trim();
			}
			c1FlexGrid1[i + 1, "MinRow07"] = DT_Grid1.Rows[i]["MinRow06"].ToString().Trim();
			c1FlexGrid1[i + 1, "MaxRow07"] = DT_Grid1.Rows[i]["MaxRow06"].ToString().Trim();
			c1FlexGrid1[i + 1, "SelfRow07"] = DT_Grid1.Rows[i]["SelfRow06"].ToString().Trim();
			if (IsCustomAutoNum)
			{
				c1FlexGrid1[i + 1, "RowID07"] = DT_Grid1.Rows[i]["RowID06"].ToString().Trim();
				c1FlexGrid1[i + 1, "CustomRowID07"] = DT_Grid1.Rows[i]["CustomRowID06"];
			}
			c1FlexGrid1[i + 1, "IsCustom07"] = "N";
			if (IsCustomAutoNum)
			{
				c1FlexGrid1[i + 1, "IsCustom07"] = DT_Grid1.Rows[i]["IsCustom06"].ToString().Trim();
				if (c1FlexGrid1[i + 1, "IsCustom07"].ToString() == "Y")
				{
					CellRange CR1 = c1FlexGrid1.GetCellRange(i + 1, c1FlexGrid1.Cols["Content06"].SafeIndex);
					CR1.Style = Style_Custom1;
				}
			}
			c1FlexGrid1[i + 1, "Code08"] = DT_Grid1.Rows[i]["Code07"].ToString().Trim();
			c1FlexGrid1[i + 1, "Content08"] = DT_Grid1.Rows[i]["Content07"].ToString().Trim();
			c1FlexGrid1[i + 1, "MinRow08"] = DT_Grid1.Rows[i]["MinRow07"].ToString().Trim();
			c1FlexGrid1[i + 1, "MaxRow08"] = DT_Grid1.Rows[i]["MaxRow07"].ToString().Trim();
			c1FlexGrid1[i + 1, "SelfRow08"] = DT_Grid1.Rows[i]["SelfRow07"].ToString().Trim();
			if (IsCustomAutoNum)
			{
				c1FlexGrid1[i + 1, "RowID08"] = DT_Grid1.Rows[i]["RowID07"].ToString().Trim();
				c1FlexGrid1[i + 1, "CustomRowID08"] = DT_Grid1.Rows[i]["CustomRowID07"];
			}
			c1FlexGrid1[i + 1, "IsCustom08"] = "N";
			if (IsCustomAutoNum)
			{
				c1FlexGrid1[i + 1, "IsCustom08"] = DT_Grid1.Rows[i]["IsCustom07"].ToString().Trim();
				if (c1FlexGrid1[i + 1, "IsCustom08"].ToString() == "Y")
				{
					CellRange CR1 = c1FlexGrid1.GetCellRange(i + 1, c1FlexGrid1.Cols["Content07"].SafeIndex);
					CR1.Style = Style_Custom1;
				}
			}
			c1FlexGrid1[i + 1, "Code09"] = DT_Grid1.Rows[i]["Code08"].ToString().Trim();
			c1FlexGrid1[i + 1, "Content09"] = DT_Grid1.Rows[i]["Content08"].ToString().Trim();
			c1FlexGrid1[i + 1, "MinRow09"] = DT_Grid1.Rows[i]["MinRow08"].ToString().Trim();
			c1FlexGrid1[i + 1, "MaxRow09"] = DT_Grid1.Rows[i]["MaxRow08"].ToString().Trim();
			c1FlexGrid1[i + 1, "SelfRow09"] = DT_Grid1.Rows[i]["SelfRow08"].ToString().Trim();
			if (IsCustomAutoNum)
			{
				c1FlexGrid1[i + 1, "RowID09"] = DT_Grid1.Rows[i]["RowID08"].ToString().Trim();
				c1FlexGrid1[i + 1, "CustomRowID09"] = DT_Grid1.Rows[i]["CustomRowID08"];
			}
			c1FlexGrid1[i + 1, "IsCustom09"] = "N";
			if (IsCustomAutoNum)
			{
				c1FlexGrid1[i + 1, "IsCustom09"] = DT_Grid1.Rows[i]["IsCustom08"].ToString().Trim();
				if (c1FlexGrid1[i + 1, "IsCustom09"].ToString() == "Y")
				{
					CellRange CR1 = c1FlexGrid1.GetCellRange(i + 1, c1FlexGrid1.Cols["Content08"].SafeIndex);
					CR1.Style = Style_Custom1;
				}
			}
			c1FlexGrid1[i + 1, "Code10"] = DT_Grid1.Rows[i]["Code09"].ToString().Trim();
			c1FlexGrid1[i + 1, "Content10"] = DT_Grid1.Rows[i]["Content09"].ToString().Trim();
			c1FlexGrid1[i + 1, "MinRow10"] = DT_Grid1.Rows[i]["MinRow09"].ToString().Trim();
			c1FlexGrid1[i + 1, "MaxRow10"] = DT_Grid1.Rows[i]["MaxRow09"].ToString().Trim();
			c1FlexGrid1[i + 1, "SelfRow10"] = DT_Grid1.Rows[i]["SelfRow09"].ToString().Trim();
			if (IsCustomAutoNum)
			{
				c1FlexGrid1[i + 1, "RowID10"] = DT_Grid1.Rows[i]["RowID09"].ToString().Trim();
				c1FlexGrid1[i + 1, "CustomRowID10"] = DT_Grid1.Rows[i]["CustomRowID09"];
			}
			c1FlexGrid1[i + 1, "IsCustom10"] = "N";
			if (IsCustomAutoNum)
			{
				c1FlexGrid1[i + 1, "IsCustom10"] = DT_Grid1.Rows[i]["IsCustom09"].ToString().Trim();
				if (c1FlexGrid1[i + 1, "IsCustom10"].ToString() == "Y")
				{
					CellRange CR1 = c1FlexGrid1.GetCellRange(i + 1, c1FlexGrid1.Cols["Content09"].SafeIndex);
					CR1.Style = Style_Custom1;
				}
			}
			c1FlexGrid1[i + 1, "Code11"] = DT_Grid1.Rows[i]["Code10"].ToString().Trim();
			c1FlexGrid1[i + 1, "Content11"] = DT_Grid1.Rows[i]["Content10"].ToString().Trim();
			c1FlexGrid1[i + 1, "MinRow11"] = DT_Grid1.Rows[i]["MinRow10"].ToString().Trim();
			c1FlexGrid1[i + 1, "MaxRow11"] = DT_Grid1.Rows[i]["MaxRow10"].ToString().Trim();
			c1FlexGrid1[i + 1, "SelfRow11"] = DT_Grid1.Rows[i]["SelfRow10"].ToString().Trim();
			if (IsCustomAutoNum)
			{
				c1FlexGrid1[i + 1, "RowID11"] = DT_Grid1.Rows[i]["RowID10"].ToString().Trim();
				c1FlexGrid1[i + 1, "CustomRowID11"] = DT_Grid1.Rows[i]["CustomRowID10"];
			}
			c1FlexGrid1[i + 1, "IsCustom11"] = "N";
			if (IsCustomAutoNum)
			{
				c1FlexGrid1[i + 1, "IsCustom11"] = DT_Grid1.Rows[i]["IsCustom10"].ToString().Trim();
				if (c1FlexGrid1[i + 1, "IsCustom11"].ToString() == "Y")
				{
					CellRange CR1 = c1FlexGrid1.GetCellRange(i + 1, c1FlexGrid1.Cols["Content10"].SafeIndex);
					CR1.Style = Style_Custom1;
				}
			}
			c1FlexGrid1[i + 1, "Code12"] = DT_Grid1.Rows[i]["Code11"].ToString().Trim();
			c1FlexGrid1[i + 1, "Content12"] = DT_Grid1.Rows[i]["Content11"].ToString().Trim();
			c1FlexGrid1[i + 1, "MinRow12"] = DT_Grid1.Rows[i]["MinRow11"].ToString().Trim();
			c1FlexGrid1[i + 1, "MaxRow12"] = DT_Grid1.Rows[i]["MaxRow11"].ToString().Trim();
			c1FlexGrid1[i + 1, "SelfRow12"] = DT_Grid1.Rows[i]["SelfRow11"].ToString().Trim();
			if (IsCustomAutoNum)
			{
				c1FlexGrid1[i + 1, "RowID12"] = DT_Grid1.Rows[i]["RowID11"].ToString().Trim();
				c1FlexGrid1[i + 1, "CustomRowID12"] = DT_Grid1.Rows[i]["CustomRowID11"];
			}
			c1FlexGrid1[i + 1, "IsCustom12"] = "N";
			if (IsCustomAutoNum)
			{
				c1FlexGrid1[i + 1, "IsCustom12"] = DT_Grid1.Rows[i]["IsCustom11"].ToString().Trim();
				if (c1FlexGrid1[i + 1, "IsCustom12"].ToString() == "Y")
				{
					CellRange CR1 = c1FlexGrid1.GetCellRange(i + 1, c1FlexGrid1.Cols["Content11"].SafeIndex);
					CR1.Style = Style_Custom1;
				}
			}
			c1FlexGrid1[i + 1, "ContentRM"] = DT_Grid1.Rows[i]["ContentRM"].ToString().Trim();
			if (IsCustomAutoNum)
			{
				c1FlexGrid1[i + 1, "SelfRowRM"] = DT_Grid1.Rows[i]["SelfRowRM"].ToString().Trim();
				c1FlexGrid1[i + 1, "RowIDRM"] = DT_Grid1.Rows[i]["RowIDRM"].ToString().Trim();
			}
		}
		try
		{
			if (DT_Grid1.Rows[0]["resType"].ToString().Trim() != "")
			{
				F_CodeType = DT_Grid1.Rows[0]["resType"].ToString().Trim();
			}
			else
			{
				F_CodeType = "";
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormaAutoNum.cs" + ex.Message);
			Console.Write(ex.Message);
			F_CodeType = "M";
		}
		DrawValidLines(100);
		Cursor = Cursors.Default;
		Grid1StatusCtrl();
		if (F_CodeType == "" || F_CodeType == "M")
		{
			StartEdit(6);
		}
		if (F_CodeType == "E" || F_CodeType == "L")
		{
			StartEdit(6);
		}
		if (chkCustom.Visible && !chkCustom.Checked)
		{
			for (int i = 1; i < c1FlexGrid1.Rows.Count - 1; i++)
			{
				if (c1FlexGrid1[i, "Code06"].ToString().Trim() == "" && c1FlexGrid1[i, "Code07"].ToString().Trim() == "" && c1FlexGrid1[i, "Code08"].ToString().Trim() == "" && c1FlexGrid1[i, "Code09"].ToString().Trim() == "" && c1FlexGrid1[i, "Code10"].ToString().Trim() == "" && c1FlexGrid1[i, "Code11"].ToString().Trim() == "" && c1FlexGrid1[i, "Code12"].ToString().Trim() == "" && c1FlexGrid1[i, "ContentRM"].ToString().Trim() == "")
				{
					c1FlexGrid1.Rows[i].Visible = false;
				}
			}
		}
		c1FlexGrid1.Visible = true;
		c1FlexGrid1.Redraw = true;
		c1FlexGrid1.Enabled = true;
	}

	private void DrawValidLines(int iStopCodeIndex)
	{
		if (c1FlexGrid1.Rows.Count <= 1)
		{
			return;
		}
		int myMin = -1;
		myMin = Convert.ToInt32(c1FlexGrid1[1, "MinRow06"]);
		for (int i = 2; i < c1FlexGrid1.Rows.Count; i++)
		{
			if (myMin != Convert.ToInt32(c1FlexGrid1[i, "MinRow06"]))
			{
				CellRange rg = c1FlexGrid1.GetCellRange(i, c1FlexGrid1.Cols["Code06"].SafeIndex);
				rg.Style = Style_Border;
				myMin = Convert.ToInt32(c1FlexGrid1[i, "MinRow06"]);
			}
		}
		myMin = Convert.ToInt32(c1FlexGrid1[1, "MinRow07"]);
		for (int i = 2; i < c1FlexGrid1.Rows.Count; i++)
		{
			if (myMin != Convert.ToInt32(c1FlexGrid1[i, "MinRow07"]))
			{
				CellRange rg = c1FlexGrid1.GetCellRange(i, c1FlexGrid1.Cols["Code07"].SafeIndex, i, c1FlexGrid1.Cols["Content07"].SafeIndex);
				rg.Style = Style_Border;
				myMin = Convert.ToInt32(c1FlexGrid1[i, "MinRow07"]);
			}
		}
		myMin = Convert.ToInt32(c1FlexGrid1[1, "MinRow08"]);
		for (int i = 2; i < c1FlexGrid1.Rows.Count; i++)
		{
			if (myMin != Convert.ToInt32(c1FlexGrid1[i, "MinRow08"]))
			{
				CellRange rg = c1FlexGrid1.GetCellRange(i, c1FlexGrid1.Cols["Code08"].SafeIndex, i, c1FlexGrid1.Cols["Content08"].SafeIndex);
				rg.Style = Style_Border;
				myMin = Convert.ToInt32(c1FlexGrid1[i, "MinRow08"]);
			}
		}
		myMin = Convert.ToInt32(c1FlexGrid1[1, "MinRow09"]);
		for (int i = 2; i < c1FlexGrid1.Rows.Count; i++)
		{
			if (myMin != Convert.ToInt32(c1FlexGrid1[i, "MinRow09"]))
			{
				CellRange rg = c1FlexGrid1.GetCellRange(i, c1FlexGrid1.Cols["Code09"].SafeIndex, i, c1FlexGrid1.Cols["Content09"].SafeIndex);
				rg.Style = Style_Border;
				myMin = Convert.ToInt32(c1FlexGrid1[i, "MinRow09"]);
			}
		}
		myMin = Convert.ToInt32(c1FlexGrid1[1, "MinRow10"]);
		for (int i = 2; i < c1FlexGrid1.Rows.Count; i++)
		{
			if (myMin != Convert.ToInt32(c1FlexGrid1[i, "MinRow10"]))
			{
				CellRange rg = c1FlexGrid1.GetCellRange(i, c1FlexGrid1.Cols["Code10"].SafeIndex, i, c1FlexGrid1.Cols["Content10"].SafeIndex);
				rg.Style = Style_Border;
				myMin = Convert.ToInt32(c1FlexGrid1[i, "MinRow10"]);
			}
		}
		myMin = Convert.ToInt32(c1FlexGrid1[1, "MinRow11"]);
		for (int i = 2; i < c1FlexGrid1.Rows.Count; i++)
		{
			if (myMin != Convert.ToInt32(c1FlexGrid1[i, "MinRow11"]))
			{
				CellRange rg = c1FlexGrid1.GetCellRange(i, c1FlexGrid1.Cols["Code11"].SafeIndex, i, c1FlexGrid1.Cols["Content11"].SafeIndex);
				rg.Style = Style_Border;
				myMin = Convert.ToInt32(c1FlexGrid1[i, "MinRow11"]);
			}
		}
		myMin = Convert.ToInt32(c1FlexGrid1[1, "MinRow12"]);
		for (int i = 2; i < c1FlexGrid1.Rows.Count; i++)
		{
			if (myMin != Convert.ToInt32(c1FlexGrid1[i, "MinRow12"]))
			{
				CellRange rg = c1FlexGrid1.GetCellRange(i, c1FlexGrid1.Cols["Code12"].SafeIndex, i, c1FlexGrid1.Cols["Content12"].SafeIndex);
				rg.Style = Style_Border;
				myMin = Convert.ToInt32(c1FlexGrid1[i, "MinRow12"]);
			}
		}
	}

	private void RememberColsProps()
	{
		for (int i = 0; i < GridCols; i++)
		{
			GridColsSquence[i, 0] = c1FlexGrid1.Cols[i].Name;
			GridColsSquence[i, 1] = c1FlexGrid1.Cols[i].Caption;
			GridColsSquence[i, 2] = c1FlexGrid1.Cols[i].Width;
			if (c1FlexGrid1.Cols[i].Name == "AnaImg")
			{
				GridColsSquence[i, 3] = typeof(Image);
			}
			else
			{
				GridColsSquence[i, 3] = c1FlexGrid1.Cols[i].DataType;
			}
			GridColsSquence[i, 4] = c1FlexGrid1.Cols[i].Visible;
			GridColsSquence[i, 5] = c1FlexGrid1.Cols[i].Format;
			GridColsSquence[i, 6] = c1FlexGrid1.Cols[i].AllowEditing;
			GridColsSquence[i, 7] = c1FlexGrid1.Cols[i].TextAlign;
		}
	}

	private void SetGridColumn()
	{
		Style_Border = c1FlexGrid1.Styles.Add("Border");
		Style_Border_CanSel = c1FlexGrid1.Styles.Add("Border_CanSel");
		Style_Border_Online = c1FlexGrid1.Styles.Add("Border_Online");
		Style_OnlineCode = c1FlexGrid1.Styles.Add("OnlineCode");
		Style_CanSelectArea = c1FlexGrid1.Styles.Add("CanSelect");
		Style_Selected = c1FlexGrid1.Styles.Add("Selected");
		Style_Selected1 = c1FlexGrid1.Styles.Add("Selected1");
		Style_Custom1 = c1FlexGrid1.Styles.Add("Custom1");
		Style_Border.BackColor = Color.Transparent;
		Style_Border_CanSel.BackColor = Color.FromArgb(255, 191, 63);
		Style_Border_Online.BackColor = Color.FromArgb(250, 232, 175);
		Style_OnlineCode.BackColor = Color.FromArgb(250, 232, 175);
		Style_OnlineCode.TextAlign = TextAlignEnum.RightCenter;
		Style_CanSelectArea.BackColor = Color.FromArgb(255, 191, 63);
		Style_CanSelectArea.TextAlign = TextAlignEnum.RightCenter;
		Style_Selected.BackColor = Color.FromArgb(102, 153, 255);
		Style_Selected.TextAlign = TextAlignEnum.RightCenter;
		Style_Selected1.BackColor = Color.FromArgb(102, 153, 255);
		Style_Selected1.TextAlign = TextAlignEnum.GeneralCenter;
		Style_Custom1.ForeColor = Color.Magenta;
		Style_Custom1.BackColor = Color.Transparent;
		Style_Custom1.TextAlign = TextAlignEnum.LeftCenter;
		for (int i = 0; i < GridCols; i++)
		{
			c1FlexGrid1.Cols[i].Name = (string)GridColsSquence[i, 0];
			c1FlexGrid1.Cols[i].Caption = (string)GridColsSquence[i, 1];
			c1FlexGrid1.Cols[i].Width = (int)GridColsSquence[i, 2];
			c1FlexGrid1.Cols[i].DataType = (Type)GridColsSquence[i, 3];
			c1FlexGrid1.Cols[i].Visible = (bool)GridColsSquence[i, 4];
			c1FlexGrid1.Cols[i].Format = (string)GridColsSquence[i, 5];
			c1FlexGrid1.Cols[i].AllowEditing = (bool)GridColsSquence[i, 6];
			c1FlexGrid1.Cols[i].TextAlign = (TextAlignEnum)GridColsSquence[i, 7];
		}
	}

	private void c1FlexGrid1_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
	{
		CellStyle s = c1FlexGrid1.GetCellStyle(e.Row, e.Col);
		if (s != null && (s.Name == "Border" || s.Name == "Border_Online" || s.Name == "Border_CanSel"))
		{
			e.DrawCell();
			Graphics g = e.Graphics;
			System.Drawing.Printing.Margins m = GetBorderMargins(e.Row, e.Col);
			if (m.Top > 0)
			{
				Rectangle rc = e.Bounds;
				rc.Height = m.Top;
				g.FillRectangle(_bdrBrush, rc);
			}
		}
	}

	private System.Drawing.Printing.Margins GetBorderMargins(int row, int col)
	{
		System.Drawing.Printing.Margins m = _m;
		System.Drawing.Printing.Margins m2 = _m;
		System.Drawing.Printing.Margins m3 = _m;
		int num = (_m.Bottom = 0);
		num = (m3.Top = num);
		num = (m2.Right = num);
		m.Left = num;
		CellRange rg = c1FlexGrid1.GetCellRange(row, col);
		if (rg.Style == null || (!(rg.Style.Name == "Border") && !(rg.Style.Name == "Border_Online") && !(rg.Style.Name == "Border_CanSel")))
		{
			return _m;
		}
		_m.Top = _bdrOutside;
		if (row > c1FlexGrid1.Rows.Fixed)
		{
			rg.r1 = (rg.r2 = row - 1);
			if (rg.Style != null && (rg.Style.Name == "Border" || rg.Style.Name == "Border_Online" || rg.Style.Name == "Border_CanSel"))
			{
				_m.Top = 0;
			}
			rg.r1 = (rg.r2 = row);
		}
		_m.Left = _bdrOutside;
		if (col > c1FlexGrid1.Cols.Fixed)
		{
			rg.c1 = (rg.c2 = col - 1);
			if (rg.Style != null && (rg.Style.Name == "Border" || rg.Style.Name == "Border_Online" || rg.Style.Name == "Border_CanSel"))
			{
				_m.Left = 0;
			}
			rg.c1 = (rg.c2 = col);
		}
		_m.Bottom = _bdrOutside;
		if (row < c1FlexGrid1.Rows.Count - 1)
		{
			rg.r1 = (rg.r2 = row + 1);
			if (rg.Style != null && (rg.Style.Name == "Border" || rg.Style.Name == "Border_Online" || rg.Style.Name == "Border_CanSel"))
			{
				_m.Bottom = _bdrInside;
			}
			rg.r1 = (rg.r2 = row);
		}
		_m.Right = _bdrOutside;
		if (col < c1FlexGrid1.Cols.Count - 1)
		{
			rg.c1 = (rg.c2 = col + 1);
			if (rg.Style != null && (rg.Style.Name == "Border" || rg.Style.Name == "Border_Online" || rg.Style.Name == "Border_CanSel"))
			{
				_m.Right = _bdrInside;
			}
			rg.c1 = (rg.c2 = col);
		}
		return _m;
	}

	private void StartEdit(int iIdx)
	{
		if (c1FlexGrid1.Rows.Count <= 1)
		{
			return;
		}
		SetGridColumn();
		ShowHasChosenCode();
		for (int i = 1; i < c1FlexGrid1.Rows.Count; i++)
		{
			CellRange rg = c1FlexGrid1.GetCellRange(i, c1FlexGrid1.Cols["ResType"].SafeIndex);
			rg.Image = null;
			rg = c1FlexGrid1.GetCellRange(i, c1FlexGrid1.Cols["Code06"].SafeIndex);
			rg.Image = null;
			rg = c1FlexGrid1.GetCellRange(i, c1FlexGrid1.Cols["Code07"].SafeIndex);
			rg.Image = null;
			rg = c1FlexGrid1.GetCellRange(i, c1FlexGrid1.Cols["Code08"].SafeIndex);
			rg.Image = null;
			rg = c1FlexGrid1.GetCellRange(i, c1FlexGrid1.Cols["Code09"].SafeIndex);
			rg.Image = null;
			rg = c1FlexGrid1.GetCellRange(i, c1FlexGrid1.Cols["Code10"].SafeIndex);
			rg.Image = null;
			rg = c1FlexGrid1.GetCellRange(i, c1FlexGrid1.Cols["Code11"].SafeIndex);
			rg.Image = null;
			rg = c1FlexGrid1.GetCellRange(i, c1FlexGrid1.Cols["Code12"].SafeIndex);
			rg.Image = null;
		}
		FORM_STATUS = AutoNum_EditMode.StartedAssemble;
		if (iIdx >= 6 && iIdx <= 12)
		{
			if (myArray[0].Row < 0 && c1FlexGrid1[1, "ResType"] != null && c1FlexGrid1[1, "ResType"].ToString().Trim() != "")
			{
				c1FlexGrid1.Cols["ResType"].Style = Style_OnlineCode;
			}
			string ColName = $"Code{iIdx:00}";
			c1FlexGrid1.Cols[ColName].Style = Style_OnlineCode;
			DrawValidLines(iIdx);
			DrawValidArea(iIdx);
			DrawCorectAreaLines(iIdx);
			iNowAssembleIndex = iIdx;
		}
		if (("M".IndexOf(F_CodeType) > -1 && iNowAssembleIndex > 6) || ("LE".IndexOf(F_CodeType) > -1 && iNowAssembleIndex > 7) || ("E".IndexOf(F_CodeType) > -1 && iNowAssembleIndex > 6) || ("L".IndexOf(F_CodeType) > -1 && iNowAssembleIndex > 6) || (F_CodeType == "W" && iNowAssembleIndex > 6) || (CustomizedAutoNum && iNowAssembleIndex > 6))
		{
			BtnBack.Enabled = true;
			ultraToolbarsManager1.Tools["mnuPrevCode"].SharedProps.Enabled = true;
		}
		c1FlexGrid2.Row = -1;
		Grid1StatusCtrl();
	}

	private void ShowHasChosenCode()
	{
		if (myArray[0].Row > -1)
		{
			CellRange rgShow = c1FlexGrid1.GetCellRange(myArray[0].Row, myArray[0].Col, myArray[0].Row, myArray[0].Col);
			rgShow.Style = Style_Selected;
		}
		if (myArray[6].Row > -1)
		{
			CellRange rgShow = c1FlexGrid1.GetCellRange(myArray[6].Row, myArray[6].Col, myArray[6].Row, myArray[6].Col);
			CellRange rgShow2 = c1FlexGrid1.GetCellRange(myArray[6].Row, myArray[6].Col + 1, myArray[6].Row, myArray[6].Col + 1);
			rgShow.Style = Style_Selected;
			rgShow2.Style = Style_Selected1;
		}
		if (myArray[7].Row > -1)
		{
			CellRange rgShow = c1FlexGrid1.GetCellRange(myArray[7].Row, myArray[7].Col, myArray[7].Row, myArray[7].Col);
			CellRange rgShow2 = c1FlexGrid1.GetCellRange(myArray[7].Row, myArray[7].Col + 1, myArray[7].Row, myArray[7].Col + 1);
			rgShow.Style = Style_Selected;
			rgShow2.Style = Style_Selected1;
		}
		if (myArray[8].Row > -1)
		{
			CellRange rgShow = c1FlexGrid1.GetCellRange(myArray[8].Row, myArray[8].Col, myArray[8].Row, myArray[8].Col);
			CellRange rgShow2 = c1FlexGrid1.GetCellRange(myArray[8].Row, myArray[8].Col + 1, myArray[8].Row, myArray[8].Col + 1);
			rgShow.Style = Style_Selected;
			rgShow2.Style = Style_Selected1;
		}
		if (myArray[9].Row > -1)
		{
			CellRange rgShow = c1FlexGrid1.GetCellRange(myArray[9].Row, myArray[9].Col, myArray[9].Row, myArray[9].Col);
			CellRange rgShow2 = c1FlexGrid1.GetCellRange(myArray[9].Row, myArray[9].Col + 1, myArray[9].Row, myArray[9].Col + 1);
			rgShow.Style = Style_Selected;
			rgShow2.Style = Style_Selected1;
		}
		if (myArray[10].Row > -1)
		{
			CellRange rgShow = c1FlexGrid1.GetCellRange(myArray[10].Row, myArray[10].Col, myArray[10].Row, myArray[10].Col);
			CellRange rgShow2 = c1FlexGrid1.GetCellRange(myArray[10].Row, myArray[10].Col + 1, myArray[10].Row, myArray[10].Col + 1);
			rgShow.Style = Style_Selected;
			rgShow2.Style = Style_Selected1;
		}
		if (myArray[11].Row > -1)
		{
			CellRange rgShow = c1FlexGrid1.GetCellRange(myArray[11].Row, myArray[11].Col, myArray[11].Row, myArray[11].Col);
			CellRange rgShow2 = c1FlexGrid1.GetCellRange(myArray[11].Row, myArray[11].Col + 1, myArray[11].Row, myArray[11].Col + 1);
			rgShow.Style = Style_Selected;
			rgShow2.Style = Style_Selected1;
		}
	}

	private void GetValidRange(int preColumnIndex, int codeColumnIndex, ref int minRowIndex, ref int maxRowIndex)
	{
		SelectedCodeInfo preInfo = Sel_Info[preColumnIndex];
		int diffDataRowFromGridRow = preInfo.SelfRow - preInfo.Row;
		string minRowName = $"MinRow{codeColumnIndex:0#}";
		string maxRowName = $"MaxRow{codeColumnIndex:0#}";
		minRowIndex = (int)c1FlexGrid1[preInfo.MinRow - diffDataRowFromGridRow, minRowName] - diffDataRowFromGridRow;
		maxRowIndex = (int)c1FlexGrid1[preInfo.MaxRow - diffDataRowFromGridRow, maxRowName] - diffDataRowFromGridRow;
	}

	private int GetPreRefColumnIndex(int codeColumnIndex)
	{
		int preColumnIndex = codeColumnIndex - F_GoBackStep[codeColumnIndex] - 1;
		int minRowIndex = 1;
		int maxRowIndex = c1FlexGrid1.Rows.Count - 1;
		GetValidRange(preColumnIndex, codeColumnIndex, ref minRowIndex, ref maxRowIndex);
		string codeName = $"Code{codeColumnIndex:0#}";
		ArrayList codeList = new ArrayList();
		bool codeOverlap = false;
		for (int i = minRowIndex; i <= maxRowIndex; i++)
		{
			object code = c1FlexGrid1[i, codeName];
			if (code == null)
			{
				continue;
			}
			code = code.ToString().Trim();
			if (!(code.ToString() == string.Empty))
			{
				if (codeList.IndexOf(code) >= 0)
				{
					codeOverlap = true;
					break;
				}
				codeList.Add(code);
			}
		}
		if (codeOverlap)
		{
			SelectedCodeInfo preInfo = Sel_Info[preColumnIndex];
			int diffDataRowFromGridRow = preInfo.SelfRow - preInfo.Row;
			int minDataRowIndex = minRowIndex + diffDataRowFromGridRow;
			int maxDataRowIndex = maxRowIndex + diffDataRowFromGridRow;
			for (int backIndex = preColumnIndex; backIndex >= 6; backIndex = backIndex - F_GoBackStep[backIndex] - 1)
			{
				SelectedCodeInfo codeInfo = Sel_Info[backIndex];
				if (codeInfo.MinRow > minDataRowIndex || codeInfo.MaxRow < maxRowIndex)
				{
					preColumnIndex = backIndex;
					break;
				}
			}
		}
		return preColumnIndex;
	}

	private void DrawValidArea(int iIndex)
	{
		try
		{
			int minRowIndex = 1;
			int maxRowIndex = c1FlexGrid1.Rows.Count - 1;
			if (iIndex > 7 || (iIndex == 7 && F_CodeType != "L" && F_CodeType != "E"))
			{
				int preColumnIndex = GetPreRefColumnIndex(iIndex);
				GetValidRange(preColumnIndex, iIndex, ref minRowIndex, ref maxRowIndex);
			}
			for (int i = 1; i < maxRowIndex && (i <= 1 || chkCustom.Checked || CustomizedAutoNum); i++)
			{
				if (myArray[0].Row < 0 && c1FlexGrid1[i, "ResType"] != null && c1FlexGrid1[i, "ResType"].ToString().Trim() != "")
				{
					CellRange rg = c1FlexGrid1.GetCellRange(i, c1FlexGrid1.Cols["ResType"].SafeIndex);
					rg.Image = imageList1.Images[0];
					rg.Style = Style_CanSelectArea;
				}
			}
			string codeName = $"Code{iIndex:0#}";
			string contentName = $"Content{iIndex:0#}";
			for (int i = minRowIndex; i <= maxRowIndex; i++)
			{
				object code = c1FlexGrid1[i, codeName];
				object content = c1FlexGrid1[i, contentName];
				if ((code != null && code.ToString().Trim() != string.Empty) || (iIndex == 6 && content != null && content.ToString().Trim() == "綱要編碼"))
				{
					CellRange rg = c1FlexGrid1.GetCellRange(i, c1FlexGrid1.Cols[codeName].SafeIndex);
					rg.Style = Style_CanSelectArea;
					rg.Image = imageList1.Images[0];
				}
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormaAutoNum.cs" + ex.Message);
			Console.Write(ex.Message);
		}
	}

	private bool CheckValid(int iRow, string Col_Idx_Name)
	{
		bool RetV = false;
		int iCol = Convert.ToInt32(Col_Idx_Name);
		if (Sel_Info[iCol - 1].MinRow < (int)c1FlexGrid1[iRow, "MinRow" + Col_Idx_Name])
		{
			RetV = ((Sel_Info[iCol - 1].MaxRow >= (int)c1FlexGrid1[iRow, "MaxRow" + Col_Idx_Name]) ? true : false);
		}
		if (Sel_Info[iCol - 1].MinRow > (int)c1FlexGrid1[iRow, "MinRow" + Col_Idx_Name])
		{
			RetV = ((Sel_Info[iCol - 1].MaxRow <= (int)c1FlexGrid1[iRow, "MaxRow" + Col_Idx_Name]) ? true : false);
		}
		if (Sel_Info[iCol - 1].MinRow == (int)c1FlexGrid1[iRow, "MinRow" + Col_Idx_Name])
		{
			RetV = true;
		}
		if (Sel_Info[iCol - 1].MinRow < (int)c1FlexGrid1[iRow, "MinRow" + Col_Idx_Name] && Sel_Info[iCol - 1].MaxRow > (int)c1FlexGrid1[iRow, "MinRow" + Col_Idx_Name])
		{
			RetV = true;
		}
		if (Sel_Info[iCol - 1].MinRow < (int)c1FlexGrid1[iRow, "MaxRow" + Col_Idx_Name] && Sel_Info[iCol - 1].MaxRow > (int)c1FlexGrid1[iRow, "MaxRow" + Col_Idx_Name])
		{
			RetV = true;
		}
		return RetV;
	}

	private void c1FlexGrid2_SizeChanged(object sender, EventArgs e)
	{
		if (c1FlexGrid2.Cols["CodeName"] != null)
		{
			c1FlexGrid2.Cols["CodeName"].Width = c1FlexGrid2.Width - 100 - 100 - 20;
		}
	}

	private void c1FlexGrid1_MouseMove(object sender, MouseEventArgs e)
	{
		if (c1FlexGrid1.MouseRow < 0 || c1FlexGrid1.MouseCol < 0)
		{
			return;
		}
		int rowIndex = c1FlexGrid1.MouseRow;
		int colIndex = c1FlexGrid1.MouseCol;
		if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right && FORM_STATUS == AutoNum_EditMode.StartedAssemble && c1FlexGrid1[rowIndex, colIndex] != null && rowIndex > 0)
		{
			string aStr = c1FlexGrid1[rowIndex, colIndex].ToString().Trim();
			CellRange rg = c1FlexGrid1.GetCellRange(rowIndex, colIndex);
			if (aStr != "" && rg.Image != null)
			{
				Cursor = Cursors.Hand;
			}
			else
			{
				Cursor = Cursors.Default;
			}
		}
	}

	private void c1FlexGrid1_MouseDown(object sender, MouseEventArgs e)
	{
		int rowIndex = c1FlexGrid1.RowSel;
		int colIndex = c1FlexGrid1.ColSel;
		if (rowIndex != c1FlexGrid1.Rows.Count - 1)
		{
			ultraToolbarsManager1.Tools["mnuCustomNewRow"].SharedProps.Enabled = false;
		}
		else
		{
			ultraToolbarsManager1.Tools["mnuCustomNewRow"].SharedProps.Enabled = true;
		}
		if (c1FlexGrid1.Rows.Count <= 1)
		{
			ultraToolbarsManager1.Tools["mnuCustomCodeEdit"].SharedProps.Visible = false;
			ultraToolbarsManager1.Tools["mnuCustomCodeDel"].SharedProps.Visible = false;
			ultraToolbarsManager1.Tools["mnuCustomCodeDrawLine"].SharedProps.Visible = false;
			ultraToolbarsManager1.Tools["mnuCustomCodeDelLine"].SharedProps.Visible = false;
			ultraToolbarsManager1.Tools["mnuCustomMainCode"].SharedProps.Visible = false;
			ultraToolbarsManager1.Tools["mnuCustomMainDel"].SharedProps.Visible = false;
		}
		else
		{
			if (rowIndex <= 0 || colIndex < 0)
			{
				return;
			}
			c1FlexGrid1.Row = rowIndex;
			c1FlexGrid1.Col = colIndex;
			Point Pt = c1FlexGrid1.ScrollPosition;
			if (Cursor == Cursors.Hand)
			{
				ref SelectedCodeInfo reference15;
				switch (c1FlexGrid1.Cols[colIndex].Name)
				{
				case "ResType":
					try
					{
						Sel_Info[0].Code = c1FlexGrid1[rowIndex, "ResType"].ToString().Trim();
						Sel_Info[0].Row = rowIndex;
						Sel_Info[0].Col = colIndex;
						CellRange rg1 = c1FlexGrid1.GetCellRange(rowIndex, colIndex, rowIndex, colIndex);
						rg1.Style = Style_Selected;
						ref SelectedCodeInfo reference22 = ref myArray[0];
						reference22 = Sel_Info[0];
						StartEdit(iNowAssembleIndex);
						break;
					}
					catch (Exception ex)
					{
						CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormaAutoNum.cs" + ex.Message);
						break;
					}
				case "Code06":
				{
					Sel_Info[6].Code = c1FlexGrid1[rowIndex, "Code06"].ToString().Trim();
					Sel_Info[6].Content = c1FlexGrid1[rowIndex, "Content06"].ToString().Trim();
					Sel_Info[6].MinRow = Convert.ToInt32(c1FlexGrid1[rowIndex, "MinRow06"].ToString().Trim());
					Sel_Info[6].MaxRow = Convert.ToInt32(c1FlexGrid1[rowIndex, "MaxRow06"].ToString().Trim());
					Sel_Info[6].SelfRow = Convert.ToInt32(c1FlexGrid1[rowIndex, "SelfRow06"].ToString().Trim());
					Sel_Info[6].Row = rowIndex;
					Sel_Info[6].Col = colIndex;
					Sel_Info[6].IsCustom = c1FlexGrid1[rowIndex, "IsCustom06"].ToString().Trim();
					if (Sel_Info[6].IsCustom == "Y")
					{
						F_IsThisCodeTemp = true;
					}
					CellRange rg1 = c1FlexGrid1.GetCellRange(rowIndex, colIndex, rowIndex, colIndex);
					rg1.Style = Style_Selected;
					ref SelectedCodeInfo reference2 = ref myArray[6];
					reference2 = Sel_Info[6];
					switch (Sel_Info[6].Code.Trim().Length)
					{
					case 1:
					{
						F_GoBackStep[7] = 0;
						Pt.X = -138;
						int Diff = Grid1TotalWidth() - c1FlexGrid1.Width;
						if (Diff > Math.Abs(Pt.X))
						{
							c1FlexGrid1.ScrollPosition = Pt;
						}
						if (F_CodeType == "E" && Sel_Info[6].Code.Trim() == "0")
						{
							Sel_Info[6].Code = "00000";
							Sel_Info[6].Content = "";
							ref SelectedCodeInfo reference5 = ref myArray[6];
							reference5 = Sel_Info[6];
						}
						if (F_CodeType == "E" && Sel_Info[6].Code.Trim() == "◆")
						{
							FormAutosurName FM_SurName = new FormAutosurName();
							FM_SurName._UserID = F_UserID;
							FM_SurName._AutoEdit = "AutoNum";
							FM_SurName.Owner = this;
							DialogResult theResult = FM_SurName.ShowDialog();
							FM_SurName.Close();
							FM_SurName.Dispose();
							FM_SurName = null;
							if (theResult != DialogResult.OK)
							{
								break;
							}
							Sel_Info[6].Code = F_CustomCode;
							Sel_Info[6].Content = F_CustomCodeName;
							ref SelectedCodeInfo reference6 = ref myArray[6];
							reference6 = Sel_Info[6];
						}
						if (F_CodeType == "L" && Sel_Info[6].Code.Trim() == "0")
						{
							if (!CustomizedAutoNum)
							{
								Sel_Info[6].Code = "00000";
								Sel_Info[6].Content = "";
							}
							ref SelectedCodeInfo reference7 = ref myArray[6];
							reference7 = Sel_Info[6];
						}
						if (F_CodeType == "L" && Sel_Info[6].Code.Trim() == "◆")
						{
							FormAutosurName FM_SurName = new FormAutosurName();
							FM_SurName._UserID = F_UserID;
							FM_SurName._AutoEdit = "AutoNum";
							FM_SurName.Owner = this;
							if (FM_SurName.ShowDialog() == DialogResult.OK)
							{
								Sel_Info[6].Code = F_CustomCode;
								Sel_Info[6].Content = F_CustomCodeName;
								ref SelectedCodeInfo reference8 = ref myArray[6];
								reference8 = Sel_Info[6];
							}
							FM_SurName.Close();
							FM_SurName.Dispose();
							FM_SurName = null;
						}
						StartEdit(7);
						break;
					}
					case 2:
					{
						ref SelectedCodeInfo reference9 = ref Sel_Info[7];
						reference9 = Sel_Info[6];
						F_GoBackStep[8] = 1;
						Pt.X = -339;
						int Diff = Grid1TotalWidth() - c1FlexGrid1.Width;
						if (Diff > Math.Abs(Pt.X))
						{
							c1FlexGrid1.ScrollPosition = Pt;
						}
						if (F_CodeType == "E")
						{
							FormAutosurName FM_SurName = new FormAutosurName();
							FM_SurName._UserID = F_UserID;
							FM_SurName._AutoEdit = "AutoNum";
							FM_SurName.Owner = this;
							if (FM_SurName.ShowDialog() == DialogResult.OK)
							{
								Sel_Info[6].Code = F_CustomCode;
								Sel_Info[6].Content = F_CustomCodeName;
								ref SelectedCodeInfo reference10 = ref myArray[6];
								reference10 = Sel_Info[6];
								StartEdit(7);
							}
							FM_SurName.Close();
							FM_SurName.Dispose();
							FM_SurName = null;
						}
						else
						{
							StartEdit(8);
						}
						break;
					}
					case 3:
					{
						ref SelectedCodeInfo reference3 = ref Sel_Info[7];
						reference3 = Sel_Info[6];
						ref SelectedCodeInfo reference4 = ref Sel_Info[8];
						reference4 = Sel_Info[6];
						F_GoBackStep[9] = 2;
						Pt.X = -539;
						int Diff = Grid1TotalWidth() - c1FlexGrid1.Width;
						if (Diff > Math.Abs(Pt.X))
						{
							c1FlexGrid1.ScrollPosition = Pt;
						}
						StartEdit(9);
						break;
					}
					}
					break;
				}
				case "Code07":
				{
					Sel_Info[7].Code = c1FlexGrid1[rowIndex, "Code07"].ToString().Trim();
					Sel_Info[7].Content = c1FlexGrid1[rowIndex, "Content07"].ToString().Trim();
					Sel_Info[7].MinRow = Convert.ToInt32(c1FlexGrid1[rowIndex, "MinRow07"].ToString().Trim());
					Sel_Info[7].MaxRow = Convert.ToInt32(c1FlexGrid1[rowIndex, "MaxRow07"].ToString().Trim());
					Sel_Info[7].SelfRow = Convert.ToInt32(c1FlexGrid1[rowIndex, "SelfRow07"].ToString().Trim());
					Sel_Info[7].Row = rowIndex;
					Sel_Info[7].Col = colIndex;
					Sel_Info[7].IsCustom = c1FlexGrid1[rowIndex, "IsCustom07"].ToString().Trim();
					if (Sel_Info[7].IsCustom == "Y")
					{
						F_IsThisCodeTemp = true;
					}
					ref SelectedCodeInfo reference18 = ref myArray[7];
					reference18 = Sel_Info[7];
					switch (Sel_Info[7].Code.Trim().Length)
					{
					case 1:
					{
						F_GoBackStep[8] = 0;
						Pt.X = -339;
						int Diff = Grid1TotalWidth() - c1FlexGrid1.Width;
						if (Diff > Math.Abs(Pt.X))
						{
							c1FlexGrid1.ScrollPosition = Pt;
						}
						StartEdit(8);
						break;
					}
					case 2:
					{
						ref SelectedCodeInfo reference21 = ref Sel_Info[8];
						reference21 = Sel_Info[7];
						F_GoBackStep[9] = 1;
						if (F_CodeType == "E" || F_CodeType == "L")
						{
							Pt.X = -339;
						}
						else
						{
							Pt.X = -539;
						}
						int Diff = Grid1TotalWidth() - c1FlexGrid1.Width;
						if (Diff > Math.Abs(Pt.X))
						{
							c1FlexGrid1.ScrollPosition = Pt;
						}
						if (F_CodeType == "E" || F_CodeType == "L")
						{
							StartEdit(8);
						}
						else
						{
							StartEdit(9);
						}
						break;
					}
					case 3:
					{
						ref SelectedCodeInfo reference19 = ref Sel_Info[8];
						reference19 = Sel_Info[7];
						ref SelectedCodeInfo reference20 = ref Sel_Info[9];
						reference20 = Sel_Info[7];
						F_GoBackStep[10] = 2;
						Pt.X = -740;
						int Diff = Grid1TotalWidth() - c1FlexGrid1.Width;
						if (Diff > Math.Abs(Pt.X))
						{
							c1FlexGrid1.ScrollPosition = Pt;
						}
						StartEdit(10);
						break;
					}
					}
					break;
				}
				case "Code08":
				{
					Sel_Info[8].Code = c1FlexGrid1[rowIndex, "Code08"].ToString().Trim();
					Sel_Info[8].Content = c1FlexGrid1[rowIndex, "Content08"].ToString().Trim();
					Sel_Info[8].MinRow = Convert.ToInt32(c1FlexGrid1[rowIndex, "MinRow08"].ToString().Trim());
					Sel_Info[8].MaxRow = Convert.ToInt32(c1FlexGrid1[rowIndex, "MaxRow08"].ToString().Trim());
					Sel_Info[8].SelfRow = Convert.ToInt32(c1FlexGrid1[rowIndex, "SelfRow08"].ToString().Trim());
					Sel_Info[8].Row = rowIndex;
					Sel_Info[8].Col = colIndex;
					Sel_Info[8].IsCustom = c1FlexGrid1[rowIndex, "IsCustom08"].ToString().Trim();
					if (Sel_Info[8].IsCustom == "Y")
					{
						F_IsThisCodeTemp = true;
					}
					ref SelectedCodeInfo reference16 = ref myArray[8];
					reference16 = Sel_Info[8];
					switch (Sel_Info[8].Code.Trim().Length)
					{
					case 1:
					{
						F_GoBackStep[9] = 0;
						Pt.X = -539;
						int Diff = Grid1TotalWidth() - c1FlexGrid1.Width;
						if (Diff > Math.Abs(Pt.X))
						{
							c1FlexGrid1.ScrollPosition = Pt;
						}
						StartEdit(9);
						break;
					}
					case 2:
					{
						ref SelectedCodeInfo reference17 = ref Sel_Info[9];
						reference17 = Sel_Info[8];
						F_GoBackStep[10] = 1;
						Pt.X = -740;
						int Diff = Grid1TotalWidth() - c1FlexGrid1.Width;
						if (Diff > Math.Abs(Pt.X))
						{
							c1FlexGrid1.ScrollPosition = Pt;
						}
						StartEdit(10);
						break;
					}
					case 3:
						Do_AssembleCode();
						c1FlexGrid1.Enabled = false;
						ultraTree1_Click_1(sender, e);
						break;
					}
					break;
				}
				case "Code09":
				{
					Sel_Info[9].Code = c1FlexGrid1[rowIndex, "Code09"].ToString().Trim();
					Sel_Info[9].Content = c1FlexGrid1[rowIndex, "Content09"].ToString().Trim();
					Sel_Info[9].MinRow = Convert.ToInt32(c1FlexGrid1[rowIndex, "MinRow09"].ToString().Trim());
					Sel_Info[9].MaxRow = Convert.ToInt32(c1FlexGrid1[rowIndex, "MaxRow09"].ToString().Trim());
					Sel_Info[9].SelfRow = Convert.ToInt32(c1FlexGrid1[rowIndex, "SelfRow09"].ToString().Trim());
					Sel_Info[9].Row = rowIndex;
					Sel_Info[9].Col = colIndex;
					Sel_Info[9].IsCustom = c1FlexGrid1[rowIndex, "IsCustom09"].ToString().Trim();
					if (Sel_Info[9].IsCustom == "Y")
					{
						F_IsThisCodeTemp = true;
					}
					ref SelectedCodeInfo reference13 = ref myArray[9];
					reference13 = Sel_Info[9];
					switch (Sel_Info[9].Code.Trim().Length)
					{
					case 1:
					{
						F_GoBackStep[10] = 0;
						Pt.X = -740;
						int Diff = Grid1TotalWidth() - c1FlexGrid1.Width;
						if (Diff > Math.Abs(Pt.X))
						{
							c1FlexGrid1.ScrollPosition = Pt;
						}
						StartEdit(10);
						break;
					}
					case 2:
						if (F_CodeType == "E" || F_CodeType == "L" || F_AutoNumExtFlag == AutoNumA_ExtFlag.Code12)
						{
							ref SelectedCodeInfo reference14 = ref Sel_Info[10];
							reference14 = Sel_Info[9];
							F_GoBackStep[11] = 1;
							Pt.X = -740;
							int Diff = Grid1TotalWidth() - c1FlexGrid1.Width;
							if (Diff > Math.Abs(Pt.X))
							{
								c1FlexGrid1.ScrollPosition = Pt;
							}
							StartEdit(11);
						}
						else
						{
							Do_AssembleCode();
							c1FlexGrid1.Enabled = false;
							ultraTree1_Click_1(sender, e);
						}
						break;
					}
					break;
				}
				case "Code10":
				{
					Sel_Info[10].Code = c1FlexGrid1[rowIndex, "Code10"].ToString().Trim();
					Sel_Info[10].Content = c1FlexGrid1[rowIndex, "Content10"].ToString().Trim();
					Sel_Info[10].MinRow = Convert.ToInt32(c1FlexGrid1[rowIndex, "MinRow10"].ToString().Trim());
					Sel_Info[10].MaxRow = Convert.ToInt32(c1FlexGrid1[rowIndex, "MaxRow10"].ToString().Trim());
					Sel_Info[10].SelfRow = Convert.ToInt32(c1FlexGrid1[rowIndex, "SelfRow10"].ToString().Trim());
					Sel_Info[10].Row = rowIndex;
					Sel_Info[10].Col = colIndex;
					Sel_Info[10].IsCustom = c1FlexGrid1[rowIndex, "IsCustom10"].ToString().Trim();
					if (Sel_Info[10].IsCustom == "Y")
					{
						F_IsThisCodeTemp = true;
					}
					ref SelectedCodeInfo reference11 = ref myArray[10];
					reference11 = Sel_Info[10];
					bool num;
					if (!CustomizedAutoNum)
					{
						if (!(F_CodeType != "E") || !(F_CodeType != "L"))
						{
							goto IL_1646;
						}
						num = F_AutoNumExtFlag == AutoNumA_ExtFlag.None;
					}
					else
					{
						num = CustomizedAutoNumEndCodeSection == 10;
					}
					if (num)
					{
						Do_AssembleCode();
						c1FlexGrid1.Enabled = false;
						ultraTree1_Click_1(sender, e);
						break;
					}
					goto IL_1646;
				}
				case "Code11":
				{
					Sel_Info[11].Code = c1FlexGrid1[rowIndex, "Code11"].ToString().Trim();
					Sel_Info[11].Content = c1FlexGrid1[rowIndex, "Content11"].ToString().Trim();
					Sel_Info[11].MinRow = Convert.ToInt32(c1FlexGrid1[rowIndex, "MinRow11"].ToString().Trim());
					Sel_Info[11].MaxRow = Convert.ToInt32(c1FlexGrid1[rowIndex, "MaxRow11"].ToString().Trim());
					Sel_Info[11].SelfRow = Convert.ToInt32(c1FlexGrid1[rowIndex, "SelfRow11"].ToString().Trim());
					Sel_Info[11].Row = rowIndex;
					Sel_Info[11].Col = colIndex;
					Sel_Info[11].IsCustom = c1FlexGrid1[rowIndex, "IsCustom11"].ToString().Trim();
					if (Sel_Info[11].IsCustom == "Y")
					{
						F_IsThisCodeTemp = true;
					}
					ref SelectedCodeInfo reference12 = ref myArray[11];
					reference12 = Sel_Info[11];
					if (CustomizedAutoNum && CustomizedAutoNumEndCodeSection == 11)
					{
						Do_AssembleCode();
						c1FlexGrid1.Enabled = false;
						ultraTree1_Click_1(sender, e);
						break;
					}
					switch (Sel_Info[11].Code.Trim().Length)
					{
					case 1:
					{
						F_GoBackStep[12] = 0;
						Pt.X = -740;
						int Diff = Grid1TotalWidth() - c1FlexGrid1.Width;
						if (Diff > Math.Abs(Pt.X))
						{
							c1FlexGrid1.ScrollPosition = Pt;
						}
						StartEdit(12);
						break;
					}
					case 2:
						Do_AssembleCode();
						c1FlexGrid1.Enabled = false;
						ultraTree1_Click_1(sender, e);
						break;
					}
					break;
				}
				case "Code12":
					{
						Sel_Info[12].Code = c1FlexGrid1[rowIndex, "Code12"].ToString().Trim();
						Sel_Info[12].Content = c1FlexGrid1[rowIndex, "Content12"].ToString().Trim();
						Sel_Info[12].MinRow = Convert.ToInt32(c1FlexGrid1[rowIndex, "MinRow12"].ToString().Trim());
						Sel_Info[12].MaxRow = Convert.ToInt32(c1FlexGrid1[rowIndex, "MaxRow12"].ToString().Trim());
						Sel_Info[12].SelfRow = Convert.ToInt32(c1FlexGrid1[rowIndex, "SelfRow12"].ToString().Trim());
						Sel_Info[12].Row = rowIndex;
						Sel_Info[12].Col = colIndex;
						Sel_Info[12].IsCustom = c1FlexGrid1[rowIndex, "IsCustom12"].ToString().Trim();
						if (Sel_Info[12].IsCustom == "Y")
						{
							F_IsThisCodeTemp = true;
						}
						ref SelectedCodeInfo reference = ref myArray[12];
						reference = Sel_Info[12];
						Do_AssembleCode();
						c1FlexGrid1.Enabled = false;
						ultraTree1_Click_1(sender, e);
						break;
					}
					IL_1646:
					reference15 = ref myArray[10];
					reference15 = Sel_Info[10];
					switch (Sel_Info[10].Code.Trim().Length)
					{
					case 1:
					{
						F_GoBackStep[11] = 0;
						Pt.X = -740;
						int Diff = Grid1TotalWidth() - c1FlexGrid1.Width;
						if (Diff > Math.Abs(Pt.X))
						{
							c1FlexGrid1.ScrollPosition = Pt;
						}
						StartEdit(11);
						break;
					}
					case 2:
						Do_AssembleCode();
						c1FlexGrid1.Enabled = false;
						ultraTree1_Click_1(sender, e);
						break;
					}
					break;
				}
			}
			else if (IsCustomEdit && c1FlexGrid1.Cols[colIndex].Name != "ResType" && c1FlexGrid1.Cols[colIndex].Name != "ContentRM")
			{
				ultraToolbarsManager1.Tools["mnuCustomCodeDel"].SharedProps.Visible = true;
				ultraToolbarsManager1.Tools["mnuCustomCodeDrawLine"].SharedProps.Visible = true;
				ultraToolbarsManager1.Tools["mnuCustomCodeDelLine"].SharedProps.Visible = true;
				ultraToolbarsManager1.Tools["mnuCustomMainCode"].SharedProps.Visible = true;
				ultraToolbarsManager1.Tools["mnuCustomMainDel"].SharedProps.Visible = true;
				if (e.Button == MouseButtons.Right)
				{
					c1FlexGrid1.Row = rowIndex;
					c1FlexGrid1.Col = colIndex;
				}
				string CodeCol = c1FlexGrid1.Cols[c1FlexGrid1.Col].Name;
				CodeCol = CodeCol.Substring(CodeCol.Length - 2);
				string sIsCustom = c1FlexGrid1[c1FlexGrid1.Row, "IsCustom" + CodeCol].ToString().Trim();
				ultraToolbarsManager1.Tools["mnuCustomCodeDel"].SharedProps.Enabled = sIsCustom == "Y";
				ultraToolbarsManager1.Tools["mnuCustomCodeDrawLine"].SharedProps.Enabled = sIsCustom == "Y";
				ultraToolbarsManager1.Tools["mnuCustomCodeDelLine"].SharedProps.Enabled = sIsCustom == "Y";
				if (ultraToolbarsManager1.Tools["mnuCustomCodeDrawLine"].SharedProps.Enabled && c1FlexGrid1.Row <= 1)
				{
					ultraToolbarsManager1.Tools["mnuCustomCodeDrawLine"].SharedProps.Enabled = false;
				}
				if (ultraToolbarsManager1.Tools["mnuCustomCodeDelLine"].SharedProps.Enabled && c1FlexGrid1.Row <= 1)
				{
					ultraToolbarsManager1.Tools["mnuCustomCodeDelLine"].SharedProps.Enabled = false;
				}
			}
			else
			{
				ultraToolbarsManager1.Tools["mnuCustomCodeEdit"].SharedProps.Visible = false;
				ultraToolbarsManager1.Tools["mnuCustomCodeDel"].SharedProps.Visible = false;
				ultraToolbarsManager1.Tools["mnuCustomCodeDrawLine"].SharedProps.Visible = false;
				ultraToolbarsManager1.Tools["mnuCustomCodeDelLine"].SharedProps.Visible = false;
				ultraToolbarsManager1.Tools["mnuCustomMainCode"].SharedProps.Visible = false;
				ultraToolbarsManager1.Tools["mnuCustomMainDel"].SharedProps.Visible = false;
			}
		}
	}

	private void Do_AssembleCode()
	{
		string strCode = "";
		string strName = "";
		string strUnit = "";
		string strCodeName = "";
		string strIsCustom = "";
		c1FlexGrid1.Clear(ClearFlags.Style);
		SetGridColumn();
		ShowHasChosenCode();
		for (int i = 1; i < c1FlexGrid1.Rows.Count; i++)
		{
			CellRange rg = c1FlexGrid1.GetCellRange(i, c1FlexGrid1.Cols["Code10"].SafeIndex);
			rg.Image = null;
		}
		if ((F_CodeType != "E" && F_CodeType != "L") || CustomizedAutoNum)
		{
			if (F_AutoNumExtFlag == AutoNumA_ExtFlag.None)
			{
				strCode = ((myArray[0].Code != "") ? myArray[0].Code : "");
				strCode += F_TreeKey;
				strCode += myArray[6].Code;
				strCode += myArray[7].Code;
				strCode += myArray[8].Code;
				strCode += myArray[9].Code;
				strCode += myArray[10].Code;
				if (CustomizedAutoNum && CustomizedAutoNumEndCodeSection == 11)
				{
					strCode += myArray[11].Code;
				}
			}
			else if (F_AutoNumExtFlag == AutoNumA_ExtFlag.Code12)
			{
				strCode = ((myArray[0].Code != "") ? myArray[0].Code : "");
				strCode += F_TreeKey;
				strCode += myArray[6].Code;
				strCode += myArray[7].Code;
				strCode += myArray[8].Code;
				strCode += myArray[9].Code;
				strCode += myArray[10].Code;
				strCode += myArray[11].Code;
				strCode += myArray[12].Code;
				if (CustomizedAutoNum && CustomizedAutoNumEndCodeSection == 11)
				{
					strCode += myArray[13].Code;
				}
			}
		}
		else
		{
			strCode = ((myArray[0].Code != "") ? myArray[0].Code : "");
			strCode += myArray[6].Code;
			strCode += myArray[7].Code;
			strCode += myArray[8].Code;
			strCode += myArray[9].Code;
			strCode += myArray[10].Code;
			strCode += myArray[11].Code;
			strCode += myArray[12].Code;
		}
		strCodeName = ultraTree1.SelectedNodes[0].Text;
		strCodeName = strCodeName.Substring(strCodeName.IndexOf(" "));
		if ((F_CodeType != "E" && F_CodeType != "L") || CustomizedAutoNum)
		{
			if (CustomizedAutoNum)
			{
				strName = ((CustomizedAutoNumEndCodeSection != 10) ? (strName + (strCodeName.Contains("*") ? strCodeName.TrimEnd('*') : "")) : (strName + (strCodeName.Contains("*") ? "" : strCodeName)));
			}
			else if (strCodeName.Contains("*") || myArray[6].Code == "0")
			{
				strName += strCodeName.TrimEnd('*').Trim();
			}
			strName = AppendStringWithComma(strName, myArray[6].Content);
			strName = AppendStringWithComma(strName, myArray[7].Content);
			strName = AppendStringWithComma(strName, myArray[8].Content);
			if (F_AutoNumExtFlag == AutoNumA_ExtFlag.Code12)
			{
				strName = AppendStringWithComma(strName, myArray[9].Content);
				strName = AppendStringWithComma(strName, myArray[10].Content);
			}
		}
		else
		{
			strName = AppendStringWithComma(strName, myArray[6].Content);
			strName = AppendStringWithComma(strName, myArray[7].Content);
			strName = AppendStringWithComma(strName, myArray[8].Content);
			strName = AppendStringWithComma(strName, myArray[9].Content);
			strName = AppendStringWithComma(strName, myArray[10].Content);
			strName = AppendStringWithComma(strName, myArray[11].Content);
		}
		if (strCodeName.Contains("*"))
		{
			strName = ((strName.IndexOf(strCodeName.TrimEnd('*').Trim()) != 0) ? (strCodeName.TrimEnd('*').Trim() + "，" + strName) : strName);
			if (!CustomizedAutoNum && strCode.StartsWith("M"))
			{
				strName = "產品，" + strName;
			}
		}
		else if (myArray[6].Code == "0" || myArray[6].Code == "00")
		{
			if (!CustomizedAutoNum && strCode.StartsWith("M"))
			{
				strName = ((strCodeName.Trim() == strName.Trim()) ? ("產品，" + strCodeName.Trim()) : ((strName.Trim().IndexOf(strCodeName.Trim()) != 0) ? ("產品，" + strCodeName.Trim() + ((strName.Trim() != "") ? ("，" + strName) : "")) : ("產品，" + strName.Trim())));
			}
			else if (strCodeName.Trim() == strName.Trim())
			{
				strName = strCodeName.Trim();
			}
			else if (strName.Trim().IndexOf(strCodeName.Trim()) != 0)
			{
				strName = strCodeName.Trim() + ((strName.Trim() != "") ? ("，" + strName) : "");
			}
		}
		else if (!CustomizedAutoNum && strCode.StartsWith("M"))
		{
			strName = "產品，" + strName;
		}
		if (strName.Trim() == "")
		{
			strName = strCodeName;
		}
		else if (strName.Trim() == "產品，")
		{
			strName = strCodeName.Trim();
		}
		string sUnit1 = "";
		string sUnit2 = "";
		if ((F_CodeType != "E" && F_CodeType != "L") || CustomizedAutoNum)
		{
			if (F_AlternativeUnit != "")
			{
				strName = AppendStringWithComma(strName, myArray[9].Content);
				strName = AppendStringWithComma(strName, myArray[10].Content);
				strUnit = F_AlternativeUnit;
				strCode += "1";
			}
			else if (F_AutoNumExtFlag == AutoNumA_ExtFlag.None)
			{
				strName = AppendStringWithComma(strName, myArray[9].Content);
				if (myArray[10].Content.IndexOf(":") > -1)
				{
					sUnit1 = myArray[10].Content.Substring(0, myArray[10].Content.IndexOf(":"));
					sUnit2 = myArray[10].Content.Substring(myArray[10].Content.IndexOf(":") + 1);
					strUnit += sUnit1;
					strName = strName + "，" + sUnit2;
				}
				else if (myArray[10].Content.IndexOf("：") > -1)
				{
					sUnit1 = myArray[10].Content.Substring(0, myArray[10].Content.IndexOf("："));
					sUnit2 = myArray[10].Content.Substring(myArray[10].Content.IndexOf("：") + 1);
					strUnit += sUnit1;
					strName = strName + "，" + sUnit2;
				}
				else if (CustomizedAutoNum)
				{
					strUnit += myArray[CustomizedAutoNumEndCodeSection].Content;
					if (CustomizedAutoNumEndCodeSection == 11)
					{
						strName = AppendStringWithComma(strName, myArray[10].Content);
					}
				}
				else
				{
					strUnit += myArray[10].Content;
				}
			}
			else if (F_AutoNumExtFlag == AutoNumA_ExtFlag.Code12)
			{
				strName = AppendStringWithComma(strName, myArray[11].Content);
				if (myArray[12].Content.IndexOf(":") > -1)
				{
					sUnit1 = myArray[12].Content.Substring(0, myArray[12].Content.IndexOf(":"));
					sUnit2 = myArray[12].Content.Substring(myArray[12].Content.IndexOf(":") + 1);
					strUnit += sUnit1;
					strName = strName + "，" + sUnit2;
				}
				else if (myArray[12].Content.IndexOf("：") > -1)
				{
					sUnit1 = myArray[12].Content.Substring(0, myArray[12].Content.IndexOf("："));
					sUnit2 = myArray[12].Content.Substring(myArray[12].Content.IndexOf("：") + 1);
					strUnit += sUnit1;
					strName = strName + "，" + sUnit2;
				}
				else if (CustomizedAutoNum)
				{
					strUnit += myArray[CustomizedAutoNumEndCodeSection].Content;
					if (CustomizedAutoNumEndCodeSection == 11)
					{
						strName = AppendStringWithComma(strName, myArray[12].Content);
					}
				}
				else
				{
					strUnit += myArray[12].Content;
				}
			}
		}
		else
		{
			strUnit += myArray[12].Content;
		}
		if ("LE".IndexOf(F_CodeType) > -1 && !CustomizedAutoNum)
		{
			strCode = F_CodeType + strCode;
		}
		strIsCustom = ((!F_IsThisCodeTemp) ? "N" : "Y");
		c1FlexGrid2.AddItem(strCode + "\t" + strName + "\t" + strUnit + "\t" + F_surName + "\t" + strIsCustom);
		F_IsThisCodeTemp = false;
		c1FlexGrid2.Row = c1FlexGrid2.Rows.Count - 1;
		FORM_STATUS = AutoNum_EditMode.Normal;
		BtnBack.Enabled = false;
		ultraToolbarsManager1.Tools["mnuPrevCode"].SharedProps.Enabled = false;
	}

	private void BtnBack_Click(object sender, EventArgs e)
	{
		if (("M".IndexOf(F_CodeType) > -1 && iNowAssembleIndex > 6) || ("LE".IndexOf(F_CodeType) > -1 && iNowAssembleIndex > 7) || ("E".IndexOf(F_CodeType) > -1 && iNowAssembleIndex > 6) || ("L".IndexOf(F_CodeType) > -1 && iNowAssembleIndex > 6) || (F_CodeType == "W" && iNowAssembleIndex > 6) || (CustomizedAutoNum && iNowAssembleIndex > 6))
		{
			StartEdit(iNowAssembleIndex - F_GoBackStep[iNowAssembleIndex] - 1);
		}
		else
		{
			BtnBack.Enabled = false;
		}
	}

	private void ultraToolbarsManager1_ToolClick(object sender, ToolClickEventArgs e)
	{
		switch (e.Tool.Key)
		{
		case "mnuDelete":
			Do_DeleteTemp();
			break;
		case "mnuSendCode":
			Do_SendCode();
			break;
		case "mnuUpdateDB":
			break;
		case "mnuPrevCode":
			BtnBack_Click(this, EventArgs.Empty);
			break;
		case "mnuReload":
			ultraTree1_Click_1(this, EventArgs.Empty);
			break;
		case "mnuKeyWordFind":
			Execute_AutoNumAFind();
			break;
		case "mnuGo":
			Do_Find();
			break;
		case "mnuLiveUpdate":
			if (!DBClass.ChkAuthority(F_UserID, "F002000500060001"))
			{
				MessageBox.Show(this, DBClass.GetFuncName("F002000500060001") + "\n這個功能您沒有權限使用", "權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				Do_LiveUpdate();
			}
			break;
		case "mnuCustomCodeEdit":
			Execute_AutoNumCustomEdit();
			break;
		case "mnuCustomCodeDel":
			Execute_AutoNumCustomDel();
			break;
		case "mnuCustomCodeDrawLine":
			Do_AutoNumCustomDrawLine();
			break;
		case "mnuCustomCodeDelLine":
			Do_AutoNumCustomDelLine();
			break;
		case "mnuCustomMainCode":
			Execute_CreateMainCode();
			break;
		case "mnuCustomMainDel":
			Do_DelMainCode();
			break;
		case "mnuCustomInsertRow":
			Do_InsertNewRow();
			break;
		case "mnuCustomNewRow":
			Do_NewRow();
			break;
		case "mnuSurNameEdit":
			Do_SurNameEdit();
			break;
		}
	}

	private void Do_LiveUpdate()
	{
		DBClass DBCLS = new DBClass();
		FormAutoNum_LiveUpdate FM_LUPD = new FormAutoNum_LiveUpdate();
		FM_LUPD.Owner = this;
		FM_LUPD._IsCustomAutoNum = IsCustomAutoNum;
		FM_LUPD._IsCustomEdit = IsCustomEdit;
		FM_LUPD._AutoDeptID = DBCLS.GetUserUseDataBaseSetAutoNum(F_UserID);
		FM_LUPD.ShowDialog();
		FM_LUPD.Close();
		FM_LUPD.Dispose();
		FM_LUPD = null;
		F_TreeBindFlag = "BINDING";
		c1FlexGrid1.Rows.Count = 1;
		BindTreeBox();
		F_TreeBindFlag = "";
	}

	private void Do_CommnNameLiveUpdate()
	{
		Cursor = Cursors.WaitCursor;
		DBClass DBCls = new DBClass();
		try
		{
			Application.DoEvents();
			Update serviceRequest = new Update();
			Application.DoEvents();
			string webServiceRoute = CommonMethods.GetIniValue("DownloadInfo", "webServiceRoute");
			if (webServiceRoute == "")
			{
				webServiceRoute = "http://bisc.archnowledge.com/pccesupdateservices/Update.asmx";
			}
			serviceRequest.Url = webServiceRoute;
			if (CommonMethods.GetIniValue("ProxyInfo", "usingProxy").Trim().ToLower() == "true")
			{
				serviceRequest.Proxy = GetProxy();
			}
			Application.DoEvents();
			DataSet DS11 = serviceRequest.GetAutoNumA();
			FormProgress FM_Prg = new FormProgress();
			FM_Prg._Max = DS11.Tables[0].Rows.Count + DS11.Tables[1].Rows.Count + DS11.Tables[2].Rows.Count;
			FM_Prg._Min = 0;
			FM_Prg.Message = "自動編碼章名俗名更新中...";
			FM_Prg.Owner = this;
			FM_Prg.Show();
			Application.DoEvents();
			int progValue = 0;
			if (DS11.Tables[0].Rows.Count > 0)
			{
				for (int i = 0; i < DS11.Tables[0].Rows.Count; i++)
				{
					DataRow[] DR_A = DT_Leaves.Select("itemCode='" + DS11.Tables[0].Rows[i]["itemCode"].ToString().Trim() + "'");
					if (DR_A.Length > 0 && DR_A[0]["commonName"].ToString().Trim() != DS11.Tables[0].Rows[i]["commonName"].ToString().Trim())
					{
						progValue++;
						if (progValue % 100 == 0)
						{
							FM_Prg.SetProgressValue(progValue);
							Application.DoEvents();
							Cursor = Cursors.WaitCursor;
						}
						DBCls.UpdateAutoNumA_CommonName(DS11.Tables[0].Rows[i]["itemCode"].ToString().Trim(), DS11.Tables[0].Rows[i]["commonName"].ToString().Trim());
					}
				}
				for (int i = 0; i < DS11.Tables[1].Rows.Count; i++)
				{
					DataRow[] DR_A = DT_Leaves12.Select("itemCode='" + DS11.Tables[1].Rows[i]["itemCode"].ToString().Trim() + "'");
					if (DR_A.Length > 0 && DR_A[0]["commonName"].ToString().Trim() != DS11.Tables[1].Rows[i]["commonName"].ToString().Trim())
					{
						progValue++;
						if (progValue % 100 == 0)
						{
							FM_Prg.SetProgressValue(progValue);
							Application.DoEvents();
							Cursor = Cursors.WaitCursor;
						}
						DBCls.UpdateAutoNumA_12_CommonName(DS11.Tables[1].Rows[i]["itemCode"].ToString().Trim(), DS11.Tables[1].Rows[i]["commonName"].ToString().Trim());
					}
				}
				if (DS11.Tables.Count > 2 && DS11.Tables[2].Rows.Count > 0)
				{
					DBCls.UpdateAutoNumB_12_L(DS11.Tables[2]);
				}
				FM_Prg.SetProgressValue(FM_Prg._Max);
				Application.DoEvents();
				Cursor = Cursors.Default;
				FM_Prg.Hide();
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormaAutoNum.cs" + ex.Message);
		}
		Cursor = Cursors.Default;
	}

	private UltraTreeNode GetNodeByKey(string theKey)
	{
		UltraTreeNode retV = new UltraTreeNode();
		foreach (UltraTreeNode child1 in ultraTree1.Nodes[0].Nodes)
		{
			foreach (UltraTreeNode childNode in child1.Nodes)
			{
				int iIndex = childNode.Key.IndexOf("_");
				string sKeyCode = childNode.Key.Substring(0, iIndex);
				if (sKeyCode.Contains(theKey))
				{
					retV = childNode;
					break;
				}
			}
		}
		return retV;
	}

	private UltraTreeNode IterateNodes(UltraTreeNode node, string vKey)
	{
		UltraTreeNode retV = new UltraTreeNode();
		foreach (UltraTreeNode childNode in node.Nodes)
		{
			int iIndex = childNode.Key.IndexOf("_");
			string sKeyCode = childNode.Key.Substring(0, iIndex);
			if (sKeyCode.IndexOf(vKey) > -1)
			{
				retV = childNode;
				break;
			}
			retV = IterateNodes(childNode, vKey);
			if (retV.Text.Trim() != "")
			{
				break;
			}
		}
		return retV;
	}

	private void Do_Find()
	{
		Cursor = Cursors.WaitCursor;
		int iStart = 0;
		bool IsFound = false;
		string sSearchText = ((ComboBoxTool)ultraToolbarsManager1.Tools["mnu_Cbo1"]).Text.Trim();
		if (sSearchText.Trim() == "")
		{
			MessageBox.Show(this, "請先輸入關鍵字", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else
		{
			if (!CommonMethods.CheckValidString(sSearchText))
			{
				return;
			}
			if (F_KeyWord != sSearchText.Trim())
			{
				iStart = 0;
				F_KeyWord = sSearchText.Trim();
			}
			else
			{
				iStart = iTreeFind + 1;
			}
			DBClass DBCLSS = new DBClass();
			DataView DV1 = DBCLSS.GetAutoNumA2().DefaultView;
			DV1.Sort = "itemCode Asc";
			bool IsCommonNameFound = false;
			iTreeFind = -1;
			for (int i = iStart; i < DV1.Count; i++)
			{
				if (DV1[i]["cName"].ToString().IndexOf(F_KeyWord) > -1 || DV1[i]["surName"].ToString().IndexOf(F_KeyWord) > -1 || DV1[i]["commonName"].ToString().IndexOf(F_KeyWord) > -1)
				{
					iTreeFind = i;
					IsFound = true;
					if (DV1[i]["commonName"].ToString().IndexOf(F_KeyWord) > -1)
					{
						IsCommonNameFound = true;
					}
					break;
				}
			}
			DataView DV2 = DBCLSS.GetAutoNumA2_12().DefaultView;
			DV2.Sort = "itemCode Asc";
			int iTreeFind2 = -1;
			if (iTreeFind == -1)
			{
				for (int i = iStart; i < DV2.Count; i++)
				{
					if (DV2[i]["cName"].ToString().IndexOf(F_KeyWord) > -1 || DV2[i]["surName"].ToString().IndexOf(F_KeyWord) > -1 || DV2[i]["commonName"].ToString().IndexOf(F_KeyWord) > -1)
					{
						iTreeFind2 = i;
						IsFound = true;
						if (DV2[i]["commonName"].ToString().IndexOf(F_KeyWord) > -1)
						{
							IsCommonNameFound = true;
						}
						break;
					}
				}
			}
			if (!IsFound)
			{
				if (iStart == 0)
				{
					MessageBox.Show("已完成搜尋資料。找不到搜尋目標。", "尋找", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					return;
				}
				MessageBox.Show("尋找到達搜尋起點", "尋找", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				F_KeyWord = "";
				Do_Find();
				return;
			}
			if (iTreeFind > -1)
			{
				try
				{
					string sKey = DV1[iTreeFind]["itemCode"].ToString();
					ultraTree1.ActiveNode = GetNodeByKey(sKey);
					ultraTree1.ActiveNode.Selected = true;
					ultraTree1.ActiveNode.Parent.ExpandAll();
					ultraTree1.ActiveNode.BringIntoView();
					if (IsCommonNameFound)
					{
						MessageBox.Show(this, "您輸入之名稱為別名，正式名稱為本章篇名稱", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
				}
				catch (Exception ex)
				{
					CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormaAutoNum.cs" + ex.Message);
				}
			}
			if (iTreeFind2 > -1)
			{
				try
				{
					string sKey = DV2[iTreeFind2]["itemCode"].ToString();
					ultraTree1.ActiveNode = GetNodeByKey(sKey);
					ultraTree1.ActiveNode.Selected = true;
					ultraTree1.ActiveNode.Parent.ExpandAll();
					ultraTree1.ActiveNode.BringIntoView();
					if (IsCommonNameFound)
					{
						MessageBox.Show(this, "您輸入之名稱為別名，正式名稱為本章篇名稱", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
				}
				catch (Exception ex)
				{
					CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormaAutoNum.cs" + ex.Message);
				}
			}
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
			DBCLSS = null;
			DV1 = null;
			Cursor = Cursors.Default;
		}
	}

	private void Execute_AutoNumAFind()
	{
		bool IsExist = false;
		Form[] ownedForms = base.OwnedForms;
		foreach (Form frm in ownedForms)
		{
			if (frm is FormAutoNumFind)
			{
				IsExist = true;
				break;
			}
		}
		if (IsExist)
		{
			FM_AUTO_FND.Owner = this;
			FM_AUTO_FND.BringToFront();
			FM_AUTO_FND.Show();
			FM_AUTO_FND = null;
		}
		else if (!IsExist)
		{
			FM_AUTO_FND = new FormAutoNumFind();
			FM_AUTO_FND.Owner = this;
			FM_AUTO_FND.Show();
			FM_AUTO_FND.BringToFront();
			FM_AUTO_FND = null;
		}
	}

	private void Do_DeleteTemp()
	{
		if (MessageBox.Show(this, "確定要刪除?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
		{
			return;
		}
		for (int i = c1FlexGrid2.Rows.Count - 1; i > 0; i--)
		{
			if (c1FlexGrid2.Rows[i].Selected)
			{
				c1FlexGrid2.RemoveItem(i);
			}
		}
	}

	private void Do_SendCode()
	{
		if (DT_Auto.Columns.IndexOf("pccesCode") < 0)
		{
			DT_Auto.Columns.Add("pccesCode", Type.GetType("System.String"));
		}
		if (c1FlexGrid2.Rows.Count <= 1 || MessageBox.Show(this, "確定要將目前所有暫存編碼全部傳送至基本資料庫?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
		{
			return;
		}
		string sSaveResult = "";
		string sSaveResult_Summary = "";
		string[,] TempCodes = new string[1, 5];
		DBClass DB_CLASS = new DBClass();
		for (int i = c1FlexGrid2.Rows.Count - 1; i > 0; i--)
		{
			TempCodes[0, 0] = c1FlexGrid2[i, "Code"].ToString().Trim();
			TempCodes[0, 1] = c1FlexGrid2[i, "CodeName"].ToString().Trim();
			TempCodes[0, 2] = c1FlexGrid2[i, "Unit"].ToString().Trim();
			TempCodes[0, 3] = ((c1FlexGrid2[i, "IsCustom"].ToString().Trim() == "Y") ? "#" : "");
			TempCodes[0, 4] = c1FlexGrid2[i, "surName"].ToString().Trim();
			DataRow dr = DT_Auto.NewRow();
			dr["pccesCode"] = c1FlexGrid2[i, "Code"].ToString().Trim();
			DT_Auto.Rows.Add(dr);
			if (c1FlexGrid2[i, "IsCustom"].ToString().Trim() == "Y")
			{
				string[,] array;
				(array = TempCodes)[0, 0] = array[0, 0] + "#";
			}
			DB_CLASS._FS_UserID = F_UserID;
			sSaveResult = DB_CLASS.SaveAutoCodes(TempCodes);
			if (sSaveResult.Trim() == "")
			{
				c1FlexGrid2.RemoveItem(i);
			}
			else
			{
				sSaveResult_Summary = sSaveResult_Summary + sSaveResult.Trim() + "\n";
			}
		}
		if (sSaveResult_Summary.Trim() != "")
		{
			MessageBox.Show(this, "剛才的[傳送編碼]發生下列狀況，請重新傳送或確認後再傳送:\n" + sSaveResult_Summary, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void c1FlexGrid2_SelChange(object sender, EventArgs e)
	{
		if (c1FlexGrid2.Row > -1)
		{
			ultraToolbarsManager1.Tools["mnuDelete"].SharedProps.Enabled = true;
			ultraToolbarsManager1.Tools["mnuSendCode"].SharedProps.Enabled = true;
		}
		else
		{
			ultraToolbarsManager1.Tools["mnuDelete"].SharedProps.Enabled = false;
			ultraToolbarsManager1.Tools["mnuSendCode"].SharedProps.Enabled = false;
		}
	}

	private void DrawCorectAreaLines(int iIdx)
	{
		for (int i = 1; i < c1FlexGrid1.Rows.Count; i++)
		{
			CellStyle s = c1FlexGrid1.GetCellStyle(i, c1FlexGrid1.Cols["Content" + iIdx.ToString().PadLeft(2, '0')].SafeIndex);
			if (s != null && s.Name == "Border")
			{
				CellRange rg = c1FlexGrid1.GetCellRange(i, c1FlexGrid1.Cols["Code" + iIdx.ToString().PadLeft(2, '0')].SafeIndex);
				s = c1FlexGrid1.GetCellStyle(i, c1FlexGrid1.Cols["Code" + iIdx.ToString().PadLeft(2, '0')].SafeIndex);
				if (s.Name == "CanSelect")
				{
					rg.Style = Style_Border_CanSel;
				}
				else
				{
					rg.Style = Style_Border_Online;
				}
			}
		}
		c1FlexGrid1.Invalidate();
	}

	private void ultraButton3_Click(object sender, EventArgs e)
	{
		string sMessage = "";
		sMessage = ((c1FlexGrid2.Rows.Count <= 1) ? "確定要結束自動編碼嗎?" : "尚有編碼未傳送，確定要結束嗎?");
		if (MessageBox.Show(this, sMessage, "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.No)
		{
			Form ActiveForm = base.Owner.ActiveMdiChild;
			if (ActiveForm is frmMrsBase)
			{
				(ActiveForm as frmMrsBase)._DT_Auto = DT_Auto;
				base.DialogResult = DialogResult.Cancel;
				Close();
			}
			if (ActiveForm == null)
			{
				Close();
			}
		}
	}

	private void FormAutoNum_FormClosing(object sender, FormClosingEventArgs e)
	{
		ultraButton3.PerformClick();
		if (base.WindowState != FormWindowState.Maximized)
		{
			CommonMethods.WriteIniValue("AutoNum", "PK_LocationX", base.Location.X.ToString());
			CommonMethods.WriteIniValue("AutoNum", "PK_LocationY", base.Location.Y.ToString());
			CommonMethods.WriteIniValue("AutoNum", "PK_Width", base.Size.Width.ToString());
			CommonMethods.WriteIniValue("AutoNum", "PK_Height", base.Size.Height.ToString());
		}
		CommonMethods.WriteIniValue("AutoNum", "WindowState", base.WindowState.ToString());
	}

	private void ultraButton1_Click(object sender, EventArgs e)
	{
		Do_SendCode();
	}

	private void ultraToolbarsManager1_ToolKeyPress(object sender, ToolKeyPressEventArgs e)
	{
		if (e.KeyChar == '\r' && e.Tool.Key == "mnu_Cbo1")
		{
			Do_Find();
		}
	}

	private void ultraToolbarsManager1_BeforeToolbarListDropdown(object sender, BeforeToolbarListDropdownEventArgs e)
	{
		e.Cancel = true;
	}

	private void c1FlexGrid2_MouseDown(object sender, MouseEventArgs e)
	{
		int rowIndex = c1FlexGrid2.MouseRow;
		c1FlexGrid2.Row = rowIndex;
	}

	private int Grid1TotalWidth()
	{
		int RetV = 0;
		for (int i = 0; i < c1FlexGrid1.Cols.Count; i++)
		{
			if (c1FlexGrid1.Cols[i].Visible)
			{
				RetV += c1FlexGrid1.Cols[i].Width;
			}
		}
		return RetV;
	}

	private void Grid1StatusCtrl()
	{
		int keyLength = F_TreeKey.Length;
		if (keyLength <= 2 && F_CodeType != "E")
		{
			return;
		}
		if (F_CodeType == "" || F_CodeType == "M" || CustomizedAutoNum)
		{
			c1FlexGrid1.Cols["Code06"].Caption = Convert.ToString(++keyLength) + "碼";
			c1FlexGrid1.Cols["Content06"].Caption = Convert.ToString(keyLength) + "碼名稱";
			c1FlexGrid1.Cols["Code07"].Caption = Convert.ToString(++keyLength) + "碼";
			c1FlexGrid1.Cols["Content07"].Caption = Convert.ToString(keyLength) + "碼名稱";
			c1FlexGrid1.Cols["Code08"].Caption = Convert.ToString(++keyLength) + "碼";
			c1FlexGrid1.Cols["Content08"].Caption = Convert.ToString(keyLength) + "碼名稱";
			c1FlexGrid1.Cols["Code09"].Caption = Convert.ToString(++keyLength) + "碼";
			c1FlexGrid1.Cols["Content09"].Caption = Convert.ToString(keyLength) + "碼名稱";
			c1FlexGrid1.Cols["Code10"].Caption = Convert.ToString(++keyLength) + "碼";
			c1FlexGrid1.Cols["Content10"].Caption = Convert.ToString(keyLength) + "碼名稱";
			string AutoNumAExt = GetAutoNumAExt(F_TreeKey);
			if (AutoNumAExt == "12")
			{
				c1FlexGrid1.Cols["Code11"].Caption = Convert.ToString(++keyLength) + "碼";
				c1FlexGrid1.Cols["Content11"].Caption = Convert.ToString(keyLength) + "碼名稱";
				c1FlexGrid1.Cols["Code12"].Caption = Convert.ToString(++keyLength) + "碼";
				c1FlexGrid1.Cols["Content12"].Caption = Convert.ToString(keyLength) + "碼名稱";
				c1FlexGrid1.Cols["Code11"].Visible = true;
				c1FlexGrid1.Cols["Content11"].Visible = true;
				c1FlexGrid1.Cols["Code12"].Visible = true;
				c1FlexGrid1.Cols["Content12"].Visible = true;
			}
			else
			{
				c1FlexGrid1.Cols["Code11"].Visible = false;
				c1FlexGrid1.Cols["Content11"].Visible = false;
				c1FlexGrid1.Cols["Code12"].Visible = false;
				c1FlexGrid1.Cols["Content12"].Visible = false;
			}
			if (CustomizedAutoNum && CustomizedAutoNumEndCodeSection == 11)
			{
				c1FlexGrid1.Cols["Code11"].Visible = true;
				c1FlexGrid1.Cols["Content11"].Visible = true;
				c1FlexGrid1.Cols["Code11"].Caption = Convert.ToString(++keyLength) + "碼";
				c1FlexGrid1.Cols["Content11"].Caption = Convert.ToString(keyLength) + "碼名稱";
			}
		}
		else if (F_CodeType == "E" || F_CodeType == "L")
		{
			c1FlexGrid1.Cols["ResType"].Visible = false;
			c1FlexGrid1.Cols["Code06"].Caption = "2-6碼";
			c1FlexGrid1.Cols["Content06"].Caption = "2-6碼名稱";
			c1FlexGrid1.Cols["Code07"].Caption = "7-8碼";
			c1FlexGrid1.Cols["Content07"].Caption = "7-8碼名稱";
			c1FlexGrid1.Cols["Code08"].Caption = "9碼";
			c1FlexGrid1.Cols["Content08"].Caption = "9碼名稱";
			c1FlexGrid1.Cols["Code09"].Caption = "10碼";
			c1FlexGrid1.Cols["Content09"].Caption = "10碼名稱";
			c1FlexGrid1.Cols["Code10"].Caption = "11碼";
			c1FlexGrid1.Cols["Content10"].Caption = "11碼名稱";
			c1FlexGrid1.Cols["Code11"].Caption = "12碼";
			c1FlexGrid1.Cols["Content11"].Caption = "12碼名稱";
			c1FlexGrid1.Cols["Code12"].Caption = "13碼";
			c1FlexGrid1.Cols["Content12"].Caption = "13碼名稱";
			c1FlexGrid1.Cols["Code11"].Visible = true;
			c1FlexGrid1.Cols["Content11"].Visible = true;
			c1FlexGrid1.Cols["Code12"].Visible = true;
			c1FlexGrid1.Cols["Content12"].Visible = true;
		}
	}

	private void GetUpdateVersion()
	{
		if (!DBClass.ChkAuthority(F_UserID, "F002000500060001"))
		{
			return;
		}
		string sToDay = $"{DateTime.Today:d}";
		string sDate = CommonMethods.GetIniValue("AutoNumUpdateCheck", "CheckDate");
		bool IsShouldCheck = false;
		if (sDate.Trim() == "")
		{
			IsShouldCheck = true;
		}
		else
		{
			try
			{
				IsShouldCheck = !(Convert.ToDateTime(sDate) == DateTime.Today);
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormaAutoNum.cs" + ex.Message);
				IsShouldCheck = true;
			}
		}
		if (IsShouldCheck)
		{
			if (IsNeedToUpdate() && MessageBox.Show(this, "有較新的規則表可以更新，是否要立即執行線上更新?", "更新檢查", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				Do_LiveUpdate();
			}
			if (IsCommonNameNeedToUpdate() && MessageBox.Show(this, "規則表有俗名資料需要更新，建議立即更新，\n現在是否執行線上更新?", "更新檢查", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				Do_CommnNameLiveUpdate();
				BindTreeBox();
			}
		}
		CommonMethods.WriteIniValue("AutoNumUpdateCheck", "CheckDate", sToDay);
	}

	private bool IsCommonNameNeedToUpdate()
	{
		Cursor = Cursors.WaitCursor;
		bool RetV = false;
		try
		{
			Application.DoEvents();
			Update serviceRequest = new Update();
			Application.DoEvents();
			string webServiceRoute = CommonMethods.GetIniValue("DownloadInfo", "webServiceRoute");
			if (webServiceRoute == "")
			{
				webServiceRoute = "http://bisc.archnowledge.com/pccesupdateservices/Update.asmx";
			}
			serviceRequest.Url = webServiceRoute;
			if (CommonMethods.GetIniValue("ProxyInfo", "usingProxy").Trim().ToLower() == "true")
			{
				serviceRequest.Proxy = GetProxy();
			}
			Application.DoEvents();
			DataSet DS11 = serviceRequest.GetAutoNumA();
			if (DS11.Tables[0].Rows.Count > 0)
			{
				for (int i = 0; i < DS11.Tables[0].Rows.Count; i++)
				{
					DataRow[] DR_A = DT_Leaves.Select("itemCode='" + DS11.Tables[0].Rows[i]["itemCode"].ToString().Trim() + "'");
					if (DR_A.Length > 0 && DR_A[0]["commonName"].ToString().Trim() != DS11.Tables[0].Rows[i]["commonName"].ToString().Trim())
					{
						RetV = true;
						break;
					}
				}
				if (!RetV)
				{
					for (int i = 0; i < DS11.Tables[1].Rows.Count; i++)
					{
						DataRow[] DR_A = DT_Leaves12.Select("itemCode='" + DS11.Tables[1].Rows[i]["itemCode"].ToString() + "'");
						if (DR_A.Length > 0 && DR_A[0]["commonName"].ToString().Trim() != DS11.Tables[1].Rows[i]["commonName"].ToString().Trim())
						{
							RetV = true;
							break;
						}
					}
				}
				if (!RetV)
				{
					DBClass DBClass1 = new DBClass();
					DataTable DT_AutoNumB_12_L = DBClass1.GetAutoNumB_12_L();
					if (DT_AutoNumB_12_L.Rows.Count != DS11.Tables[2].Rows.Count)
					{
						RetV = true;
					}
					DBClass1 = null;
					DT_AutoNumB_12_L = null;
				}
				if (!RetV)
				{
					DBClass DBClass1 = new DBClass();
					DataTable DT_AutoNumB_12_L = DBClass1.GetAutoNumB_12_L();
					DT_AutoNumB_12_L.CaseSensitive = true;
					for (int i = 0; i < DS11.Tables[2].Rows.Count; i++)
					{
						DataRow[] DR_A = DT_AutoNumB_12_L.Select("Code='" + DS11.Tables[2].Rows[i]["Code"].ToString() + "'");
						if (DR_A.Length > 0 && DR_A[0]["commonName"].ToString().Trim() != DS11.Tables[2].Rows[i]["commonName"].ToString().Trim())
						{
							RetV = true;
							break;
						}
					}
					DBClass1 = null;
					DT_AutoNumB_12_L = null;
				}
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormaAutoNum.cs" + ex.Message);
		}
		Cursor = Cursors.Default;
		return RetV;
	}

	private bool IsNeedToUpdate()
	{
		Cursor = Cursors.WaitCursor;
		bool RetV = false;
		try
		{
			Application.DoEvents();
			Update serviceRequest = new Update();
			Application.DoEvents();
			string webServiceRoute = CommonMethods.GetIniValue("DownloadInfo", "webServiceRoute");
			if (webServiceRoute == "")
			{
				webServiceRoute = "http://bisc.archnowledge.com/pccesupdateservices/Update.asmx";
			}
			serviceRequest.Url = webServiceRoute;
			if (CommonMethods.GetIniValue("ProxyInfo", "usingProxy").Trim().ToLower() == "true")
			{
				serviceRequest.Proxy = GetProxy();
			}
			Application.DoEvents();
			DataSet DS11 = serviceRequest.AutoNumUpd();
			DataSet DSList = DS11.Clone();
			DBClass DBCLS = new DBClass();
			DataTable DT1 = DBCLS.GetUserDefine("Select * from AutoNumUpd");
			DT1.CaseSensitive = true;
			for (int i = 0; i < DS11.Tables[0].Rows.Count; i++)
			{
				DataView DV33 = DT1.DefaultView;
				DateTime DD1 = Convert.ToDateTime(DS11.Tables[0].Rows[i]["ReleaseDate"]);
				string sDate = DD1.Month + "/" + DD1.Day + "/" + DD1.Year;
				string sFLT = "ItemCode ='" + DS11.Tables[0].Rows[i]["ItemCode"].ToString().Trim() + "' And ReleaseDate >= #" + sDate + "# ";
				DV33.RowFilter = sFLT;
				if (DV33.Count == 0)
				{
					RetV = true;
					break;
				}
			}
			DBCLS = null;
			DT1 = null;
			Application.DoEvents();
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormaAutoNum.cs" + ex.Message);
		}
		Cursor = Cursors.Default;
		return RetV;
	}

	private WebProxy GetProxy()
	{
		WebProxy myProxy = new WebProxy();
		string port = CommonMethods.GetIniValue("ProxyInfo", "port");
		string account = CommonMethods.GetIniValue("ProxyInfo", "account");
		string password = CommonMethods.GetIniValue("ProxyInfo", "password");
		string address = CommonMethods.GetIniValue("ProxyInfo", "address");
		myProxy.Address = new Uri(address + ":" + port);
		myProxy.Credentials = new NetworkCredential(account, password);
		return myProxy;
	}

	private void Execute_AutoNumCustomEdit()
	{
		string CodeCol = c1FlexGrid1.Cols[c1FlexGrid1.Col].Name;
		CodeCol = CodeCol.Substring(CodeCol.Length - 2);
		FormAutoNumCustomEdit FMAUTOED = new FormAutoNumCustomEdit();
		FMAUTOED._CodeCol = CodeCol;
		FMAUTOED._ChapCode = F_TreeKey;
		FMAUTOED._CodeSection = CodeCol;
		FMAUTOED._SelfRow = (int)c1FlexGrid1[c1FlexGrid1.Row, "SelfRow" + CodeCol];
		FMAUTOED._MinRow = (int)c1FlexGrid1[c1FlexGrid1.Row, "MinRow" + CodeCol];
		FMAUTOED._MaxRow = (int)c1FlexGrid1[c1FlexGrid1.Row, "MaxRow" + CodeCol];
		FMAUTOED._CodeType = F_CodeType;
		FMAUTOED._DEPT_ID = F_DEPT_ID;
		if (FMAUTOED.ShowDialog(this) == DialogResult.OK)
		{
			ultraTree1_Click_1(this, EventArgs.Empty);
		}
		FMAUTOED.Close();
		FMAUTOED.Dispose();
		FMAUTOED = null;
	}

	private void Execute_AutoNumCustomDel()
	{
		if (MessageBox.Show(this, "確定要刪除這個自訂碼嗎?", "訊問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			string CodeCol = c1FlexGrid1.Cols[c1FlexGrid1.Col].Name;
			CodeCol = CodeCol.Substring(CodeCol.Length - 2);
			DBClass DBCLS = new DBClass();
			DBCLS.ExecuteCommand("Delete AutoNumB Where ChapCode='" + F_TreeKey + "'  And CodeSection = '" + CodeCol + "'  And SelfRow  = " + c1FlexGrid1[c1FlexGrid1.Row, "SelfRow" + CodeCol].ToString() + " And IsCustom = 'Y' And Version  = '" + F_DEPT_ID + "' ");
			ultraTree1_Click_1(this, EventArgs.Empty);
			DBCLS = null;
		}
	}

	private void Do_AutoNumCustomDrawLine()
	{
		string CodeCol = c1FlexGrid1.Cols[c1FlexGrid1.Col].Name;
		CodeCol = CodeCol.Substring(CodeCol.Length - 2);
		DBClass DBCLS = new DBClass();
		DBCLS.ExecuteCommand("Update AutoNumB Set MinRow = " + c1FlexGrid1[c1FlexGrid1.Row, "SelfRow" + CodeCol].ToString() + " Where ChapCode='" + F_TreeKey + "'  And CodeSection = '" + CodeCol + "'  And SelfRow  = " + c1FlexGrid1[c1FlexGrid1.Row, "SelfRow" + CodeCol].ToString() + " And IsCustom = 'Y' And Version  = '" + F_DEPT_ID + "' ");
		ultraTree1_Click_1(this, EventArgs.Empty);
		DBCLS = null;
	}

	private void Do_AutoNumCustomDelLine()
	{
		string CodeCol = c1FlexGrid1.Cols[c1FlexGrid1.Col].Name;
		CodeCol = CodeCol.Substring(CodeCol.Length - 2);
		DBClass DBCLS = new DBClass();
		DBCLS.ExecuteCommand("Update AutoNumB Set MinRow = " + c1FlexGrid1[c1FlexGrid1.Row - 1, "MinRow" + CodeCol].ToString() + " Where ChapCode='" + F_TreeKey + "'  And CodeSection = '" + CodeCol + "'  And SelfRow  = " + c1FlexGrid1[c1FlexGrid1.Row, "SelfRow" + CodeCol].ToString() + " And IsCustom = 'Y' And Version  = '" + F_DEPT_ID + "' ");
		ultraTree1_Click_1(this, EventArgs.Empty);
		DBCLS = null;
	}

	private void chkCustom_CheckedChanged(object sender, EventArgs e)
	{
		if (IsCustomEdit)
		{
			if (chkCustom.Checked)
			{
				ultraToolbarsManager1.Tools["mnuCustomCodeEdit"].SharedProps.Visible = true;
				ultraToolbarsManager1.Tools["mnuCustomMainCode"].SharedProps.Visible = true;
				ultraToolbarsManager1.Tools["mnuCustomMainDel"].SharedProps.Visible = true;
				ultraToolbarsManager1.Tools["mnuCustomInsertRow"].SharedProps.Visible = true;
				ultraToolbarsManager1.Tools["mnuCustomNewRow"].SharedProps.Visible = true;
			}
			else
			{
				ultraToolbarsManager1.Tools["mnuCustomCodeEdit"].SharedProps.Visible = false;
				ultraToolbarsManager1.Tools["mnuCustomMainCode"].SharedProps.Visible = false;
				ultraToolbarsManager1.Tools["mnuCustomMainDel"].SharedProps.Visible = false;
				ultraToolbarsManager1.Tools["mnuCustomInsertRow"].SharedProps.Visible = false;
				ultraToolbarsManager1.Tools["mnuCustomNewRow"].SharedProps.Visible = false;
			}
			BindTreeBox();
		}
		else
		{
			ultraToolbarsManager1.Tools["mnuCustomMainCode"].SharedProps.Visible = false;
			ultraToolbarsManager1.Tools["mnuCustomMainDel"].SharedProps.Visible = false;
			ultraToolbarsManager1.Tools["mnuCustomInsertRow"].SharedProps.Visible = false;
			ultraToolbarsManager1.Tools["mnuCustomNewRow"].SharedProps.Visible = false;
			c1FlexGrid1.Rows.Count = 1;
			BindTreeBox();
		}
		ultraTree1_Click_1(sender, e);
		if (!(F_TreeKey != ""))
		{
			return;
		}
		DBClass DBCLSS = new DBClass();
		DataView DV1 = DBCLSS.GetAutoNumA2().DefaultView;
		DV1.Sort = "itemCode Asc";
		int iTreeFind = -1;
		for (int i = 0; i < DV1.Count; i++)
		{
			if (DV1[i]["itemCode"].ToString().Trim() == F_TreeKey)
			{
				iTreeFind = i;
				break;
			}
		}
		if (iTreeFind > -1)
		{
			try
			{
				string sKey = DV1[iTreeFind]["itemCode"].ToString();
				ultraTree1.ActiveNode = ultraTree1.GetNodeByKey(sKey);
				ultraTree1.ActiveNode.Parent.ExpandAll();
				ultraTree1.ActiveNode.Selected = true;
				ultraTree1.ActiveNode.BringIntoView();
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormaAutoNum.cs" + ex.Message);
			}
		}
		DBCLSS = null;
	}

	private void Execute_CreateMainCode()
	{
		FormAutoNumCreateChapterCode FM_CRMAIN = new FormAutoNumCreateChapterCode();
		FM_CRMAIN._UserID = F_UserID;
		FM_CRMAIN._DEPT_ID = F_DEPT_ID;
		FM_CRMAIN.Owner = this;
		if (FM_CRMAIN.ShowDialog() == DialogResult.OK)
		{
			c1FlexGrid1.Rows.Count = 1;
			BindTreeBox();
			F_TreeBindFlag = "";
			if (F_NewCustomCode != "")
			{
				ultraTree1.ActiveNode = ultraTree1.GetNodeByKey(F_NewCustomCode);
				ultraTree1.ActiveNode.Parent.ExpandAll();
				ultraTree1.ActiveNode.Selected = true;
				ultraTree1.ActiveNode.BringIntoView();
				F_TreeKey = "";
				ultraTree1_AfterSelect(this, null);
			}
			F_NewCustomCode = "";
		}
		FM_CRMAIN.Close();
		FM_CRMAIN.Dispose();
		FM_CRMAIN = null;
	}

	private void Do_DelMainCode()
	{
		string sQuest = "確定要刪除【" + F_TreeKey + "】碼?";
		if (MessageBox.Show(this, sQuest, "訊問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			DBClass DBCLS = new DBClass();
			DBCLS._FS_UserID = "PccAdmin";
			string sSQL = "Delete From AutoNumA Where itemCode='" + F_TreeKey + "' " + '\r' + "Delete From AutoNumB Where ChapCode='" + F_TreeKey + "' And IsCustom='Y' And Version='" + F_DEPT_ID + "' ";
			DBCLS.ExecuteCommand(sSQL);
			c1FlexGrid1.Rows.Count = 1;
			lblUseCode.Text = "目前編輯中：";
			BindTreeBox();
			DBCLS = null;
		}
	}

	private void Do_InsertNewRow()
	{
		iNewRow = c1FlexGrid1.Row;
		int iSelfRow = PubTools.Str2Int(c1FlexGrid1[iNewRow, "SelfRow06"]);
		iMax[6] = PubTools.Str2Int(c1FlexGrid1[iNewRow, "MaxRow06"]);
		iMax[7] = PubTools.Str2Int(c1FlexGrid1[iNewRow, "MaxRow07"]);
		iMax[8] = PubTools.Str2Int(c1FlexGrid1[iNewRow, "MaxRow08"]);
		iMax[9] = PubTools.Str2Int(c1FlexGrid1[iNewRow, "MaxRow09"]);
		iMax[10] = PubTools.Str2Int(c1FlexGrid1[iNewRow, "MaxRow10"]);
		iMin[6] = PubTools.Str2Int(c1FlexGrid1[iNewRow, "MinRow06"]);
		iMin[7] = PubTools.Str2Int(c1FlexGrid1[iNewRow, "MinRow07"]);
		iMin[8] = PubTools.Str2Int(c1FlexGrid1[iNewRow, "MinRow08"]);
		iMin[9] = PubTools.Str2Int(c1FlexGrid1[iNewRow, "MinRow09"]);
		iMin[10] = PubTools.Str2Int(c1FlexGrid1[iNewRow, "MinRow10"]);
		int iSeq = iNewRow;
		c1FlexGrid1.Rows.Insert(c1FlexGrid1.Row);
		while (iSeq <= c1FlexGrid1.Rows.Count - 1)
		{
			c1FlexGrid1[iSeq, "SelfRow06"] = iSelfRow;
			c1FlexGrid1[iSeq, "SelfRow07"] = iSelfRow;
			c1FlexGrid1[iSeq, "SelfRow08"] = iSelfRow;
			c1FlexGrid1[iSeq, "SelfRow09"] = iSelfRow;
			c1FlexGrid1[iSeq, "SelfRow10"] = iSelfRow;
			c1FlexGrid1[iSeq, "SelfRowRM"] = iSelfRow;
			iSeq++;
			iSelfRow++;
		}
		for (int i = 6; i <= 10; i++)
		{
			ProcessInsert_MaxRow(i.ToString());
			ProcessInsert_MinRow(i.ToString());
		}
		SaveCustomAutoNum2DB();
		ultraTree1_Click_1(this, EventArgs.Empty);
	}

	private void ProcessInsert_MaxRow(string ColIndex)
	{
		string sCol = ColIndex.PadLeft(2, '0');
		int iiMax = iMax[PubTools.Str2Int(sCol)];
		for (int i = iNewRow; i < c1FlexGrid1.Rows.Count; i++)
		{
			if (c1FlexGrid1[i, "MaxRow" + sCol] == null)
			{
				c1FlexGrid1[i, "MaxRow" + sCol] = iiMax + 1;
			}
			else if (PubTools.Str2Int(c1FlexGrid1[i, "MaxRow" + sCol]) == iiMax)
			{
				c1FlexGrid1[i, "MaxRow" + sCol] = iiMax + 1;
			}
		}
		for (int i = iNewRow - 1; i >= 1; i--)
		{
			if (PubTools.Str2Int(c1FlexGrid1[i, "MaxRow" + sCol]) == iiMax)
			{
				c1FlexGrid1[i, "MaxRow" + sCol] = iiMax + 1;
			}
		}
	}

	private void ProcessInsert_MinRow(string ColIndex)
	{
		string sCol = ColIndex.PadLeft(2, '0');
		int iiMin = iMin[PubTools.Str2Int(sCol)];
		for (int i = iNewRow; i < c1FlexGrid1.Rows.Count; i++)
		{
			if (c1FlexGrid1[i, "MinRow" + sCol] == null)
			{
				c1FlexGrid1[i, "MinRow" + sCol] = iiMin;
				c1FlexGrid1[i, "Code" + sCol] = "";
				c1FlexGrid1[i, "Content" + sCol] = "";
				c1FlexGrid1[i, "IsCustom" + sCol] = "N";
			}
			else if (PubTools.Str2Int(c1FlexGrid1[i, "MinRow" + sCol]) == iiMin)
			{
				c1FlexGrid1[i, "MinRow" + sCol] = iiMin;
			}
		}
		for (int i = iNewRow - 1; i >= 1; i--)
		{
			if (PubTools.Str2Int(c1FlexGrid1[i, "MinRow" + sCol]) == iiMin)
			{
				c1FlexGrid1[i, "MinRow" + sCol] = iiMin;
			}
		}
	}

	private void Do_NewRow()
	{
		iNewRow = c1FlexGrid1.Row;
		int iSelfRow = PubTools.Str2Int(c1FlexGrid1[iNewRow, "SelfRow06"]);
		iMax[6] = PubTools.Str2Int(c1FlexGrid1[iNewRow, "MaxRow06"]);
		iMax[7] = PubTools.Str2Int(c1FlexGrid1[iNewRow, "MaxRow07"]);
		iMax[8] = PubTools.Str2Int(c1FlexGrid1[iNewRow, "MaxRow08"]);
		iMax[9] = PubTools.Str2Int(c1FlexGrid1[iNewRow, "MaxRow09"]);
		iMax[10] = PubTools.Str2Int(c1FlexGrid1[iNewRow, "MaxRow10"]);
		iMin[6] = PubTools.Str2Int(c1FlexGrid1[iNewRow, "MinRow06"]);
		iMin[7] = PubTools.Str2Int(c1FlexGrid1[iNewRow, "MinRow07"]);
		iMin[8] = PubTools.Str2Int(c1FlexGrid1[iNewRow, "MinRow08"]);
		iMin[9] = PubTools.Str2Int(c1FlexGrid1[iNewRow, "MinRow09"]);
		iMin[10] = PubTools.Str2Int(c1FlexGrid1[iNewRow, "MinRow10"]);
		c1FlexGrid1.Rows.Add();
		c1FlexGrid1[iNewRow + 1, "SelfRow06"] = iSelfRow + 1;
		c1FlexGrid1[iNewRow + 1, "SelfRow07"] = iSelfRow + 1;
		c1FlexGrid1[iNewRow + 1, "SelfRow08"] = iSelfRow + 1;
		c1FlexGrid1[iNewRow + 1, "SelfRow09"] = iSelfRow + 1;
		c1FlexGrid1[iNewRow + 1, "SelfRow10"] = iSelfRow + 1;
		c1FlexGrid1[iNewRow + 1, "SelfRowRM"] = iSelfRow + 1;
		iNewRow = c1FlexGrid1.Rows.Count - 1;
		for (int i = 6; i <= 10; i++)
		{
			string sCol = i.ToString().PadLeft(2, '0');
			ProcessNewRows_MaxRow(i.ToString());
			c1FlexGrid1[iNewRow, "MinRow" + sCol] = iMin[i];
			c1FlexGrid1[iNewRow, "Code" + sCol] = "";
			c1FlexGrid1[iNewRow, "Content" + sCol] = "";
			c1FlexGrid1[iNewRow, "IsCustom" + sCol] = "N";
		}
		SaveCustomAutoNum2DB();
		ultraTree1_Click_1(this, EventArgs.Empty);
	}

	private void ProcessNewRows_MaxRow(string ColIndex)
	{
		string sCol = ColIndex.PadLeft(2, '0');
		int iiMax = iMax[PubTools.Str2Int(sCol)];
		for (int i = iNewRow; i >= 1; i--)
		{
			if (c1FlexGrid1[i, "MaxRow" + sCol] == null)
			{
				c1FlexGrid1[i, "MaxRow" + sCol] = iiMax + 1;
			}
			else if (PubTools.Str2Int(c1FlexGrid1[i, "MaxRow" + sCol]) == iiMax)
			{
				c1FlexGrid1[i, "MaxRow" + sCol] = iiMax + 1;
			}
		}
	}

	private void SaveCustomAutoNum2DB()
	{
		DataTable DT_Save = new DataTable();
		DT_Save.Columns.Add("Code", Type.GetType("System.String"));
		DT_Save.Columns.Add("Min", Type.GetType("System.Int32"));
		DT_Save.Columns.Add("Max", Type.GetType("System.Int32"));
		DT_Save.Columns.Add("SelfRow", Type.GetType("System.Int32"));
		DT_Save.Columns.Add("RowID", Type.GetType("System.Int64"));
		DT_Save.Columns.Add("ActType", Type.GetType("System.String"));
		for (int i = 6; i <= 11; i++)
		{
			string sCol = i.ToString().PadLeft(2, '0');
			if (sCol == "11")
			{
				sCol = "RM";
			}
			for (int j = 1; j < c1FlexGrid1.Rows.Count; j++)
			{
				if (i == 11)
				{
					DataRow DR_Save = DT_Save.NewRow();
					DR_Save["Code"] = "RM";
					DR_Save["SelfRow"] = c1FlexGrid1[j, "SelfRow" + sCol];
					if (c1FlexGrid1[j, "RowID" + sCol] == null)
					{
						DR_Save["ActType"] = "NEW";
					}
					else
					{
						DR_Save["ActType"] = "UPD";
						DR_Save["RowID"] = c1FlexGrid1[j, "RowID" + sCol];
					}
					DT_Save.Rows.Add(DR_Save);
				}
				else if (c1FlexGrid1[j, "IsCustom" + sCol].ToString().Trim() != "Y")
				{
					DataRow DR_Save = DT_Save.NewRow();
					DR_Save["Code"] = sCol;
					DR_Save["Min"] = c1FlexGrid1[j, "MinRow" + sCol];
					DR_Save["Max"] = c1FlexGrid1[j, "MaxRow" + sCol];
					DR_Save["SelfRow"] = c1FlexGrid1[j, "SelfRow" + sCol];
					if (c1FlexGrid1[j, "RowID" + sCol] == null)
					{
						DR_Save["ActType"] = "NEW";
					}
					else
					{
						DR_Save["ActType"] = "UPD";
						DR_Save["RowID"] = c1FlexGrid1[j, "RowID" + sCol];
					}
					DT_Save.Rows.Add(DR_Save);
				}
				else
				{
					DataRow DR_Save = DT_Save.NewRow();
					DR_Save["Code"] = sCol;
					DR_Save["Min"] = c1FlexGrid1[j, "MinRow" + sCol];
					DR_Save["Max"] = c1FlexGrid1[j, "MaxRow" + sCol];
					DR_Save["SelfRow"] = c1FlexGrid1[j, "SelfRow" + sCol];
					DR_Save["ActType"] = "UPD";
					DR_Save["RowID"] = c1FlexGrid1[j, "RowID" + sCol];
					DT_Save.Rows.Add(DR_Save);
					DR_Save = DT_Save.NewRow();
					DR_Save["Code"] = sCol;
					DR_Save["Min"] = c1FlexGrid1[j, "MinRow" + sCol];
					DR_Save["Max"] = c1FlexGrid1[j, "MaxRow" + sCol];
					DR_Save["SelfRow"] = c1FlexGrid1[j, "SelfRow" + sCol];
					DR_Save["ActType"] = "UPD";
					DR_Save["RowID"] = c1FlexGrid1[j, "CustomRowID" + sCol];
					DT_Save.Rows.Add(DR_Save);
				}
			}
		}
		DBClass DBCLS = new DBClass();
		DBCLS.SaveCustomAutoNum(ultraTree1.SelectedNodes[0].Key.Trim(), DT_Save);
		DBCLS = null;
		DT_Save = null;
	}

	private void Do_SurNameEdit()
	{
		FormAutosurName FM_SurName = new FormAutosurName();
		FM_SurName._UserID = F_UserID;
		FM_SurName._TreeKey = F_TreeKey;
		FM_SurName.Owner = this;
		if (FM_SurName.ShowDialog() == DialogResult.OK)
		{
			DBClass DBClass1 = new DBClass();
			F_surName = DBClass1.GetSurName(F_TreeKey);
			DBClass1 = null;
		}
		FM_SurName.Close();
		FM_SurName.Dispose();
		FM_SurName = null;
	}

	private void ultraTree1_MouseDown(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			ultraTree1_Click_1(sender, EventArgs.Empty);
		}
		else if (!IsChapCodeCustom)
		{
			ultraToolbarsManager1.Tools["mnuCustomMainDel"].SharedProps.Enabled = false;
		}
		else
		{
			ultraToolbarsManager1.Tools["mnuCustomMainDel"].SharedProps.Enabled = true;
		}
		DBClass DBClass1 = new DBClass();
		F_surName = DBClass1.GetSurName(F_TreeKey);
		DBClass1 = null;
	}

	private void FormAutoNum_Activated(object sender, EventArgs e)
	{
		if (FORM_STATUS == AutoNum_EditMode.Initial && !IsDoneDataBaseAutoNum && PubTools.GetAppSet_Bool("AutoNumCustom"))
		{
			FORM_STATUS = AutoNum_EditMode.Normal;
			Thread t1 = new Thread(ShowNotCorres);
			Application.DoEvents();
			t1.Start();
			IsCustomAutoNum = false;
			IsCustomEdit = false;
		}
	}

	private void ShowNotCorres()
	{
		MessageBox.Show(this, "您尚未對這個資料庫做自動編碼的機關對應，無法使用自訂編碼的功能。\n\n建議先進入[系統維護]-->[資料庫管理及切換]-->[設定]\n完成此資料庫的自動編碼對應", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
	}

	private void FormAutoNum_FormClosed(object sender, FormClosedEventArgs e)
	{
		panel1 = null;
		ultraButton3 = null;
		ultraToolbarsManager1 = null;
		FormaAutoNum_Fill_Panel = null;
		panel2 = null;
		panel3 = null;
		splitter1 = null;
		panel4 = null;
		panel5 = null;
		splitter2 = null;
		panel6 = null;
		ultraLabel1 = null;
		ultraTree1 = null;
		ultraLabel2 = null;
		ultraLabel3 = null;
		c1FlexGrid2 = null;
		c1FlexGrid1 = null;
		_FormAutoNum_Toolbars_Dock_Area_Left = null;
		_FormAutoNum_Toolbars_Dock_Area_Right = null;
		_FormAutoNum_Toolbars_Dock_Area_Top = null;
		_FormAutoNum_Toolbars_Dock_Area_Bottom = null;
		imageList1 = null;
		BtnBack = null;
		BtnReload = null;
		ultraButton1 = null;
		chkCustom = null;
		lblUseCode = null;
		myArray = null;
		Sel_Info = null;
		GridColsSquence = null;
		DT_Nodes = null;
		DT_Leaves = null;
		DT_Grid1 = null;
		GC.Collect();
	}

	private string AppendStringWithComma(string sourceString, string appendString)
	{
		sourceString = sourceString.Trim();
		appendString = appendString.Trim();
		return sourceString = sourceString + ((sourceString != "" && appendString != "" && !sourceString.EndsWith("，")) ? "，" : "") + appendString;
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.MrsBase.FormAutoNum));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Tool1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelete");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSendCode");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar2 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Tool2");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool1 = new Infragistics.Win.UltraWinToolbars.LabelTool("lblFind");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool1 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuGo");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuKeyWordFind");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool1 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuUpdate");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool5 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSurNameEdit");
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar3 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Tool3");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool6 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuUpdateDB");
		Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool2 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Popup1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool7 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelete");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool8 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSendCode");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool9 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuDelete");
		Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool10 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSendCode");
		Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool11 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuUpdateDB");
		Infragistics.Win.UltraWinToolbars.LabelTool labelTool2 = new Infragistics.Win.UltraWinToolbars.LabelTool("lblFind");
		Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool2 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("mnu_Cbo1");
		Infragistics.Win.ValueList valueList1 = new Infragistics.Win.ValueList(0);
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool12 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuGo");
		Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool3 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("PopupUpper");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool13 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuPrevCode");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool14 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuReload");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool15 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCustomCodeEdit");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool16 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCustomCodeDel");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool17 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCustomCodeDrawLine");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool18 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCustomCodeDelLine");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool19 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCustomInsertRow");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool20 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCustomNewRow");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool21 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuPrevCode");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool22 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuReload");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool23 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuKeyWordFind");
		Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool4 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("mnuUpdate");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool24 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuLiveUpdate");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool25 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCustomMainCode");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool26 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCustomMainDel");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool27 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuManualUpdate");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool28 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuLiveUpdate");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool29 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCustomCodeEdit");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool30 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCustomCodeDel");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool31 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCustomCodeNewRow");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool32 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCustomCodeDrawLine");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool33 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCustomCodeDelLine");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool34 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCustomMainCode");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool35 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCustomMainDel");
		Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool5 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("TreePopup1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool36 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuLiveUpdate");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool37 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCustomMainCode");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool38 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCustomMainDel");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool39 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCustomInsertRow");
		Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool40 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuCustomNewRow");
		Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool41 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSurNameEdit");
		Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTree.UltraTreeNode ultraTreeNode1 = new Infragistics.Win.UltraWinTree.UltraTreeNode();
		Infragistics.Win.UltraWinTree.UltraTreeNode ultraTreeNode2 = new Infragistics.Win.UltraWinTree.UltraTreeNode();
		Infragistics.Win.UltraWinTree.UltraTreeNode ultraTreeNode3 = new Infragistics.Win.UltraWinTree.UltraTreeNode();
		Infragistics.Win.UltraWinTree.UltraTreeNode ultraTreeNode4 = new Infragistics.Win.UltraWinTree.UltraTreeNode();
		Infragistics.Win.UltraWinTree.UltraTreeNode ultraTreeNode5 = new Infragistics.Win.UltraWinTree.UltraTreeNode();
		Infragistics.Win.UltraWinTree.Override _override1 = new Infragistics.Win.UltraWinTree.Override();
		Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
		this.panel1 = new System.Windows.Forms.Panel();
		this.ultraButton3 = new Infragistics.Win.Misc.UltraButton();
		this.ultraToolbarsManager1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
		this.c1FlexGrid2 = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.c1FlexGrid1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.ultraTree1 = new Infragistics.Win.UltraWinTree.UltraTree();
		this.FormaAutoNum_Fill_Panel = new System.Windows.Forms.Panel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.panel3 = new System.Windows.Forms.Panel();
		this.panel6 = new System.Windows.Forms.Panel();
		this.lblUseCode = new Infragistics.Win.Misc.UltraLabel();
		this.chkCustom = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.BtnReload = new Infragistics.Win.Misc.UltraButton();
		this.BtnBack = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.splitter2 = new System.Windows.Forms.Splitter();
		this.panel5 = new System.Windows.Forms.Panel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.splitter1 = new System.Windows.Forms.Splitter();
		this.panel4 = new System.Windows.Forms.Panel();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this._FormAutoNum_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormAutoNum_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormAutoNum_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormAutoNum_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ultraTree1).BeginInit();
		this.FormaAutoNum_Fill_Panel.SuspendLayout();
		this.panel2.SuspendLayout();
		this.panel3.SuspendLayout();
		this.panel6.SuspendLayout();
		this.panel5.SuspendLayout();
		this.panel4.SuspendLayout();
		base.SuspendLayout();
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.Controls.Add(this.ultraButton3);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 500);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(868, 36);
		this.panel1.TabIndex = 10;
		this.ultraButton3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		appearance1.BackColor = System.Drawing.SystemColors.ControlLightLight;
		appearance1.BackColor2 = System.Drawing.SystemColors.ControlLight;
		appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton3.Appearance = appearance1;
		this.ultraButton3.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton3.Cursor = System.Windows.Forms.Cursors.Hand;
		this.ultraButton3.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraButton3.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton3.Location = new System.Drawing.Point(772, 4);
		this.ultraButton3.Name = "ultraButton3";
		this.ultraButton3.ShowFocusRect = false;
		this.ultraButton3.ShowOutline = false;
		this.ultraButton3.Size = new System.Drawing.Size(90, 28);
		this.ultraButton3.SupportThemes = false;
		this.ultraButton3.TabIndex = 6;
		this.ultraButton3.Text = "結  束";
		this.ultraButton3.Click += new System.EventHandler(ultraButton3_Click);
		appearance2.FontData.Name = "Arial";
		appearance2.FontData.SizeInPoints = 9f;
		this.ultraToolbarsManager1.Appearance = appearance2;
		appearance3.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance3.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.DockAreaAppearance = appearance3;
		this.ultraToolbarsManager1.DockWithinContainer = this;
		this.ultraToolbarsManager1.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraToolbarsManager1.LockToolbars = true;
		appearance13.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance13.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance13.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.MenuSettings.HotTrackAppearance = appearance13;
		appearance14.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance14.BackColor2 = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraToolbarsManager1.MenuSettings.IconAreaAppearance = appearance14;
		appearance15.BackColor = System.Drawing.Color.White;
		appearance15.BackColor2 = System.Drawing.Color.White;
		this.ultraToolbarsManager1.MenuSettings.ToolAppearance = appearance15;
		this.ultraToolbarsManager1.ShowFullMenusDelay = 500;
		this.ultraToolbarsManager1.ShowQuickCustomizeButton = false;
		this.ultraToolbarsManager1.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2003;
		ultraToolbar1.DockedColumn = 0;
		ultraToolbar1.DockedRow = 0;
		ultraToolbar1.Text = "Tool1";
		buttonTool2.InstanceProps.IsFirstInGroup = true;
		ultraToolbar1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[2] { buttonTool1, buttonTool2 });
		ultraToolbar2.DockedColumn = 1;
		ultraToolbar2.DockedRow = 0;
		ultraToolbar2.Text = "Tool2";
		labelTool1.InstanceProps.Width = 98;
		buttonTool4.InstanceProps.IsFirstInGroup = true;
		popupMenuTool1.InstanceProps.IsFirstInGroup = true;
		buttonTool5.InstanceProps.IsFirstInGroup = true;
		ultraToolbar2.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[6] { labelTool1, comboBoxTool1, buttonTool3, buttonTool4, popupMenuTool1, buttonTool5 });
		ultraToolbar3.DockedColumn = 1;
		ultraToolbar3.DockedRow = 0;
		ultraToolbar3.Text = "Tool3";
		ultraToolbar3.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[1] { buttonTool6 });
		ultraToolbar3.Visible = false;
		this.ultraToolbarsManager1.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[3] { ultraToolbar1, ultraToolbar2, ultraToolbar3 });
		appearance16.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance16.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 255);
		this.ultraToolbarsManager1.ToolbarSettings.Appearance = appearance16;
		appearance17.BackColor = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance17.BackColor2 = System.Drawing.Color.FromArgb(196, 210, 236);
		appearance17.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		this.ultraToolbarsManager1.ToolbarSettings.HotTrackAppearance = appearance17;
		popupMenuTool2.SharedProps.Caption = "右鍵功能選單";
		popupMenuTool2.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[2] { buttonTool7, buttonTool8 });
		appearance18.Image = resources.GetObject("appearance18.Image");
		buttonTool9.SharedProps.AppearancesSmall.Appearance = appearance18;
		buttonTool9.SharedProps.Caption = "刪除";
		buttonTool9.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		appearance19.Image = resources.GetObject("appearance19.Image");
		buttonTool10.SharedProps.AppearancesSmall.Appearance = appearance19;
		buttonTool10.SharedProps.Caption = "傳送編碼";
		buttonTool10.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		buttonTool11.SharedProps.Caption = "自動編碼更新";
		buttonTool11.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		labelTool2.SharedProps.Caption = "綱要編碼關鍵字尋找:";
		labelTool2.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		comboBoxTool2.DropDownStyle = Infragistics.Win.DropDownStyle.DropDown;
		comboBoxTool2.SharedProps.Caption = "尋找關鍵字";
		comboBoxTool2.SharedProps.Width = 150;
		comboBoxTool2.ValueList = valueList1;
		appearance20.Image = resources.GetObject("appearance20.Image");
		buttonTool12.SharedProps.AppearancesSmall.Appearance = appearance20;
		buttonTool12.SharedProps.Caption = "Go";
		popupMenuTool3.SharedProps.Caption = "上方右鍵選單";
		buttonTool14.InstanceProps.IsFirstInGroup = true;
		buttonTool15.InstanceProps.IsFirstInGroup = true;
		buttonTool17.InstanceProps.IsFirstInGroup = true;
		buttonTool19.InstanceProps.IsFirstInGroup = true;
		popupMenuTool3.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[8] { buttonTool13, buttonTool14, buttonTool15, buttonTool16, buttonTool17, buttonTool18, buttonTool19, buttonTool20 });
		buttonTool21.SharedProps.Caption = "前一碼";
		buttonTool22.SharedProps.Caption = "重新編碼";
		appearance21.Image = resources.GetObject("appearance21.Image");
		buttonTool23.SharedProps.AppearancesSmall.Appearance = appearance21;
		buttonTool23.SharedProps.Caption = "規則表關鍵字查詢";
		buttonTool23.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		popupMenuTool4.SharedProps.Caption = "工具";
		popupMenuTool4.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
		buttonTool25.InstanceProps.IsFirstInGroup = true;
		popupMenuTool4.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[3] { buttonTool24, buttonTool25, buttonTool26 });
		buttonTool27.SharedProps.Caption = "手動更新";
		buttonTool28.SharedProps.Caption = "線上更新...";
		buttonTool29.SharedProps.Caption = "自訂編碼...";
		buttonTool29.SharedProps.Shortcut = System.Windows.Forms.Shortcut.F12;
		buttonTool30.SharedProps.Caption = "刪除編碼...";
		buttonTool31.SharedProps.Caption = "加入空白列";
		buttonTool32.SharedProps.Caption = "畫規則線";
		buttonTool33.SharedProps.Caption = "刪除規則線";
		buttonTool34.SharedProps.Caption = "新增網要編碼...";
		appearance22.Image = resources.GetObject("appearance22.Image");
		buttonTool35.SharedProps.AppearancesSmall.Appearance = appearance22;
		buttonTool35.SharedProps.Caption = "刪除綱要編碼";
		popupMenuTool5.SharedProps.Caption = "樹狀圖右鍵選單";
		buttonTool37.InstanceProps.IsFirstInGroup = true;
		popupMenuTool5.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[3] { buttonTool36, buttonTool37, buttonTool38 });
		appearance23.Image = resources.GetObject("appearance23.Image");
		buttonTool39.SharedProps.AppearancesSmall.Appearance = appearance23;
		buttonTool39.SharedProps.Caption = "插入空白列";
		appearance24.Image = resources.GetObject("appearance24.Image");
		buttonTool40.SharedProps.AppearancesSmall.Appearance = appearance24;
		buttonTool40.SharedProps.Caption = "新增空白列";
		buttonTool41.SharedProps.Caption = "別名設定";
		buttonTool41.SharedProps.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
		buttonTool41.SharedProps.Shortcut = System.Windows.Forms.Shortcut.F12;
		this.ultraToolbarsManager1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[25]
		{
			popupMenuTool2, buttonTool9, buttonTool10, buttonTool11, labelTool2, comboBoxTool2, buttonTool12, popupMenuTool3, buttonTool21, buttonTool22,
			buttonTool23, popupMenuTool4, buttonTool27, buttonTool28, buttonTool29, buttonTool30, buttonTool31, buttonTool32, buttonTool33, buttonTool34,
			buttonTool35, popupMenuTool5, buttonTool39, buttonTool40, buttonTool41
		});
		this.ultraToolbarsManager1.ToolKeyPress += new Infragistics.Win.UltraWinToolbars.ToolKeyPressEventHandler(ultraToolbarsManager1_ToolKeyPress);
		this.ultraToolbarsManager1.BeforeToolbarListDropdown += new Infragistics.Win.UltraWinToolbars.BeforeToolbarListDropdownEventHandler(ultraToolbarsManager1_BeforeToolbarListDropdown);
		this.ultraToolbarsManager1.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(ultraToolbarsManager1_ToolClick);
		this.c1FlexGrid2._ExcelFileName = "";
		this.c1FlexGrid2._ExcelSheeName = "";
		this.c1FlexGrid2._IsOpenExcelAfterExport = false;
		this.c1FlexGrid2.AllowEditing = false;
		this.c1FlexGrid2.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
		this.c1FlexGrid2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.c1FlexGrid2.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.c1FlexGrid2.ColumnInfo = resources.GetString("c1FlexGrid2.ColumnInfo");
		this.ultraToolbarsManager1.SetContextMenuUltra(this.c1FlexGrid2, "Popup1");
		this.c1FlexGrid2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.c1FlexGrid2.ExtendLastCol = true;
		this.c1FlexGrid2.ForeColor = System.Drawing.SystemColors.WindowText;
		this.c1FlexGrid2.Location = new System.Drawing.Point(0, 28);
		this.c1FlexGrid2.Name = "c1FlexGrid2";
		this.c1FlexGrid2.Rows.Count = 1;
		this.c1FlexGrid2.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.c1FlexGrid2.ShowToolTipOnNarrowColumn = true;
		this.c1FlexGrid2.Size = new System.Drawing.Size(851, 138);
		this.c1FlexGrid2.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("c1FlexGrid2.Styles"));
		this.c1FlexGrid2.TabIndex = 3;
		this.c1FlexGrid2.MouseDown += new System.Windows.Forms.MouseEventHandler(c1FlexGrid2_MouseDown);
		this.c1FlexGrid2.SelChange += new System.EventHandler(c1FlexGrid2_SelChange);
		this.c1FlexGrid2.SizeChanged += new System.EventHandler(c1FlexGrid2_SizeChanged);
		this.c1FlexGrid1._ExcelFileName = "";
		this.c1FlexGrid1._ExcelSheeName = "";
		this.c1FlexGrid1._IsOpenExcelAfterExport = false;
		this.c1FlexGrid1.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
		this.c1FlexGrid1.AllowEditing = false;
		this.c1FlexGrid1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
		this.c1FlexGrid1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.c1FlexGrid1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.c1FlexGrid1.ColumnInfo = resources.GetString("c1FlexGrid1.ColumnInfo");
		this.ultraToolbarsManager1.SetContextMenuUltra(this.c1FlexGrid1, "PopupUpper");
		this.c1FlexGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.c1FlexGrid1.ExtendLastCol = true;
		this.c1FlexGrid1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.c1FlexGrid1.ForeColor = System.Drawing.SystemColors.WindowText;
		this.c1FlexGrid1.Location = new System.Drawing.Point(0, 28);
		this.c1FlexGrid1.Name = "c1FlexGrid1";
		this.c1FlexGrid1.Rows.Count = 1;
		this.c1FlexGrid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
		this.c1FlexGrid1.ShowToolTipOnNarrowColumn = true;
		this.c1FlexGrid1.Size = new System.Drawing.Size(657, 288);
		this.c1FlexGrid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("c1FlexGrid1.Styles"));
		this.c1FlexGrid1.TabIndex = 2;
		this.c1FlexGrid1.MouseDown += new System.Windows.Forms.MouseEventHandler(c1FlexGrid1_MouseDown);
		this.c1FlexGrid1.OwnerDrawCell += new C1.Win.C1FlexGrid.OwnerDrawCellEventHandler(c1FlexGrid1_OwnerDrawCell);
		this.c1FlexGrid1.MouseMove += new System.Windows.Forms.MouseEventHandler(c1FlexGrid1_MouseMove);
		appearance25.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraTree1.Appearance = appearance25;
		this.ultraTree1.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
		this.ultraToolbarsManager1.SetContextMenuUltra(this.ultraTree1, "TreePopup1");
		this.ultraTree1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ultraTree1.HideSelection = false;
		this.ultraTree1.Indent = 15;
		this.ultraTree1.Location = new System.Drawing.Point(0, 28);
		this.ultraTree1.Name = "ultraTree1";
		ultraTreeNode1.LeftImages.Add(resources.GetObject("ultraTreeNode1.LeftImages"));
		ultraTreeNode2.LeftImages.Add(resources.GetObject("ultraTreeNode2.LeftImages"));
		ultraTreeNode3.LeftImages.Add(resources.GetObject("ultraTreeNode3.LeftImages"));
		ultraTreeNode3.Text = "Node2";
		ultraTreeNode2.Nodes.AddRange(new Infragistics.Win.UltraWinTree.UltraTreeNode[1] { ultraTreeNode3 });
		ultraTreeNode2.Text = "Node1";
		ultraTreeNode5.Text = "Node4";
		ultraTreeNode4.Nodes.AddRange(new Infragistics.Win.UltraWinTree.UltraTreeNode[1] { ultraTreeNode5 });
		ultraTreeNode4.Text = "Node3";
		ultraTreeNode1.Nodes.AddRange(new Infragistics.Win.UltraWinTree.UltraTreeNode[2] { ultraTreeNode2, ultraTreeNode4 });
		ultraTreeNode1.RightImages.Add(resources.GetObject("ultraTreeNode1.RightImages"));
		ultraTreeNode1.Text = "Node0";
		this.ultraTree1.Nodes.AddRange(new Infragistics.Win.UltraWinTree.UltraTreeNode[1] { ultraTreeNode1 });
		_override1.SelectionType = Infragistics.Win.UltraWinTree.SelectType.Single;
		this.ultraTree1.Override = _override1;
		this.ultraTree1.Size = new System.Drawing.Size(186, 288);
		this.ultraTree1.TabIndex = 2;
		this.ultraTree1.AfterSelect += new Infragistics.Win.UltraWinTree.AfterNodeSelectEventHandler(ultraTree1_AfterSelect);
		this.ultraTree1.MouseDown += new System.Windows.Forms.MouseEventHandler(ultraTree1_MouseDown);
		this.FormaAutoNum_Fill_Panel.Controls.Add(this.panel2);
		this.FormaAutoNum_Fill_Panel.Controls.Add(this.panel1);
		this.FormaAutoNum_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
		this.FormaAutoNum_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.FormaAutoNum_Fill_Panel.Location = new System.Drawing.Point(0, 27);
		this.FormaAutoNum_Fill_Panel.Name = "FormaAutoNum_Fill_Panel";
		this.FormaAutoNum_Fill_Panel.Size = new System.Drawing.Size(868, 536);
		this.FormaAutoNum_Fill_Panel.TabIndex = 0;
		this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.panel2.Controls.Add(this.panel3);
		this.panel2.Controls.Add(this.splitter1);
		this.panel2.Controls.Add(this.panel4);
		this.panel2.Location = new System.Drawing.Point(8, 8);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(853, 492);
		this.panel2.TabIndex = 11;
		this.panel3.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel3.Controls.Add(this.panel6);
		this.panel3.Controls.Add(this.splitter2);
		this.panel3.Controls.Add(this.panel5);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel3.Location = new System.Drawing.Point(0, 0);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(853, 318);
		this.panel3.TabIndex = 0;
		this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel6.Controls.Add(this.lblUseCode);
		this.panel6.Controls.Add(this.chkCustom);
		this.panel6.Controls.Add(this.BtnReload);
		this.panel6.Controls.Add(this.BtnBack);
		this.panel6.Controls.Add(this.c1FlexGrid1);
		this.panel6.Controls.Add(this.ultraLabel2);
		this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel6.Location = new System.Drawing.Point(194, 0);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(659, 318);
		this.panel6.TabIndex = 2;
		appearance26.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		appearance26.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 102);
		this.lblUseCode.Appearance = appearance26;
		this.lblUseCode.BackColor = System.Drawing.Color.Transparent;
		this.lblUseCode.Location = new System.Drawing.Point(220, 3);
		this.lblUseCode.Name = "lblUseCode";
		this.lblUseCode.Size = new System.Drawing.Size(79, 16);
		this.lblUseCode.TabIndex = 6;
		this.lblUseCode.Text = "目前編輯中：";
		appearance27.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.chkCustom.Appearance = appearance27;
		this.chkCustom.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.chkCustom.Location = new System.Drawing.Point(76, 3);
		this.chkCustom.Name = "chkCustom";
		this.chkCustom.Size = new System.Drawing.Size(144, 20);
		this.chkCustom.TabIndex = 5;
		this.chkCustom.Text = "啟用自訂規則表";
		this.chkCustom.Visible = false;
		this.chkCustom.CheckedChanged += new System.EventHandler(chkCustom_CheckedChanged);
		this.BtnReload.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance28.TextVAlign = Infragistics.Win.VAlign.Top;
		this.BtnReload.Appearance = appearance28;
		this.BtnReload.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.BtnReload.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.BtnReload.Location = new System.Drawing.Point(578, 3);
		this.BtnReload.Name = "BtnReload";
		this.BtnReload.Size = new System.Drawing.Size(75, 23);
		this.BtnReload.SupportThemes = false;
		this.BtnReload.TabIndex = 4;
		this.BtnReload.Text = "重新編碼";
		this.BtnReload.Click += new System.EventHandler(ultraTree1_Click_1);
		this.BtnBack.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance29.TextVAlign = Infragistics.Win.VAlign.Top;
		this.BtnBack.Appearance = appearance29;
		this.BtnBack.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.BtnBack.Enabled = false;
		this.BtnBack.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.BtnBack.Location = new System.Drawing.Point(500, 3);
		this.BtnBack.Name = "BtnBack";
		this.BtnBack.Size = new System.Drawing.Size(75, 23);
		this.BtnBack.SupportThemes = false;
		this.BtnBack.TabIndex = 3;
		this.BtnBack.Text = "< 前一碼";
		this.BtnBack.Click += new System.EventHandler(BtnBack_Click);
		appearance30.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		appearance30.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 102);
		appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance30.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel2.Appearance = appearance30;
		this.ultraLabel2.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.None;
		this.ultraLabel2.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.ultraLabel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.ultraLabel2.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel2.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(657, 28);
		this.ultraLabel2.TabIndex = 1;
		this.ultraLabel2.Text = "規則表";
		this.splitter2.Location = new System.Drawing.Point(188, 0);
		this.splitter2.Name = "splitter2";
		this.splitter2.Size = new System.Drawing.Size(6, 318);
		this.splitter2.TabIndex = 1;
		this.splitter2.TabStop = false;
		this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel5.Controls.Add(this.ultraTree1);
		this.panel5.Controls.Add(this.ultraLabel1);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel5.Location = new System.Drawing.Point(0, 0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(188, 318);
		this.panel5.TabIndex = 0;
		appearance31.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		appearance31.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 102);
		appearance31.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance31.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel1.Appearance = appearance31;
		this.ultraLabel1.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.None;
		this.ultraLabel1.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.ultraLabel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.ultraLabel1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel1.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(186, 28);
		this.ultraLabel1.TabIndex = 1;
		this.ultraLabel1.Text = "綱要編碼";
		this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.splitter1.Location = new System.Drawing.Point(0, 318);
		this.splitter1.Name = "splitter1";
		this.splitter1.Size = new System.Drawing.Size(853, 6);
		this.splitter1.TabIndex = 1;
		this.splitter1.TabStop = false;
		this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel4.Controls.Add(this.ultraButton1);
		this.panel4.Controls.Add(this.c1FlexGrid2);
		this.panel4.Controls.Add(this.ultraLabel3);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel4.Location = new System.Drawing.Point(0, 324);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(853, 168);
		this.panel4.TabIndex = 2;
		this.ultraButton1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance32.TextVAlign = Infragistics.Win.VAlign.Top;
		this.ultraButton1.Appearance = appearance32;
		this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.ultraButton1.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraButton1.Location = new System.Drawing.Point(773, 2);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.Size = new System.Drawing.Size(75, 23);
		this.ultraButton1.SupportThemes = false;
		this.ultraButton1.TabIndex = 5;
		this.ultraButton1.Text = "傳送編碼";
		this.ultraButton1.Click += new System.EventHandler(ultraButton1_Click);
		appearance33.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		appearance33.BackColor2 = System.Drawing.Color.FromArgb(153, 204, 102);
		appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance33.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel3.Appearance = appearance33;
		this.ultraLabel3.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.None;
		this.ultraLabel3.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.ultraLabel3.Dock = System.Windows.Forms.DockStyle.Top;
		this.ultraLabel3.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel3.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(851, 28);
		this.ultraLabel3.TabIndex = 1;
		this.ultraLabel3.Text = "編碼暫存區";
		this._FormAutoNum_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormAutoNum_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormAutoNum_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
		this._FormAutoNum_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormAutoNum_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 27);
		this._FormAutoNum_Toolbars_Dock_Area_Left.Name = "_FormAutoNum_Toolbars_Dock_Area_Left";
		this._FormAutoNum_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 536);
		this._FormAutoNum_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormAutoNum_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormAutoNum_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormAutoNum_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
		this._FormAutoNum_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormAutoNum_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(868, 27);
		this._FormAutoNum_Toolbars_Dock_Area_Right.Name = "_FormAutoNum_Toolbars_Dock_Area_Right";
		this._FormAutoNum_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 536);
		this._FormAutoNum_Toolbars_Dock_Area_Right.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormAutoNum_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormAutoNum_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormAutoNum_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
		this._FormAutoNum_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormAutoNum_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
		this._FormAutoNum_Toolbars_Dock_Area_Top.Name = "_FormAutoNum_Toolbars_Dock_Area_Top";
		this._FormAutoNum_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(868, 27);
		this._FormAutoNum_Toolbars_Dock_Area_Top.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormAutoNum_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormAutoNum_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this._FormAutoNum_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
		this._FormAutoNum_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormAutoNum_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 563);
		this._FormAutoNum_Toolbars_Dock_Area_Bottom.Name = "_FormAutoNum_Toolbars_Dock_Area_Bottom";
		this._FormAutoNum_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(868, 0);
		this._FormAutoNum_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ultraToolbarsManager1;
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
		this.imageList1.Images.SetKeyName(0, "");
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.ClientSize = new System.Drawing.Size(868, 563);
		base.Controls.Add(this.FormaAutoNum_Fill_Panel);
		base.Controls.Add(this._FormAutoNum_Toolbars_Dock_Area_Left);
		base.Controls.Add(this._FormAutoNum_Toolbars_Dock_Area_Right);
		base.Controls.Add(this._FormAutoNum_Toolbars_Dock_Area_Top);
		base.Controls.Add(this._FormAutoNum_Toolbars_Dock_Area_Bottom);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.MinimizeBox = false;
		base.Name = "FormAutoNum";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "自動編碼(v2.0)";
		base.Load += new System.EventHandler(FormaAutoNum_Load);
		base.Activated += new System.EventHandler(FormAutoNum_Activated);
		base.FormClosed += new System.Windows.Forms.FormClosedEventHandler(FormAutoNum_FormClosed);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormAutoNum_FormClosing);
		this.panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ultraTree1).EndInit();
		this.FormaAutoNum_Fill_Panel.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		this.panel6.ResumeLayout(false);
		this.panel5.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
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
