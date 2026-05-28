using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.DatabaseAccess;
using Archnowledge.Pcces.DomainModule.Budget;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.DomainModule.MrsBase;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.STDClass;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinStatusBar;
using Infragistics.Win.UltraWinTabControl;
using Infragistics.Win.UltraWinToolbars;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormPickProjWkItem_Wzd : Form
{
	private string F_CurrentDBName = "";

	private string F_CurrentSelectDBName = "";

	private string F_CurrentSelectDBDesc = "";

	private DataTable DT_Temp = new DataTable();

	private DataTable DT1 = new DataTable();

	private FormBudget_PickType F_CallUpType = FormBudget_PickType.NewBudget;

	private string userID;

	private PccesFormAction FormActionName;

	private string projectCode = "";

	private string F_ParentCode = "";

	private int GridCols = 15;

	private object[,] GridColsSquence;

	private int F_MainQty = 0;

	private int F_MainCst = 0;

	private int F_MainAmt = 0;

	private int F_AnaQty = 0;

	private int F_AnaCst = 0;

	private int F_AnaAmt = 0;

	private DataSet dsPwrSet;

	private bool F_IsCostStructure = false;

	private DataTable DT_FPick = new DataTable();

	private string CompanyDBName = string.Empty;

	private IContainer components;

	private UltraToolbarsManager ultraToolbarsManager1;

	private ImageList imageList1;

	private UltraTabControl Tabs_Ctrl;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl Tab_A;

	private UltraTabPageControl Tab_B;

	private Panel panel1;

	private UltraButton ultraButton3;

	private UltraButton ultraButton2;

	private Panel panel2;

	private C1FlexGrid c1FlexGrid1;

	private Panel panel3;

	private UltraLabel ultraLabel5;

	private UltraLabel ultraLabel4;

	private UltraLabel ultraLabel6;

	private UltraLabel ultraLabel3;

	private UltraComboEditor cbFind;

	private UltraButton ultraButton1;

	private UltraLabel ultraLabel2;

	private UltraLabel ultraLabel1;

	private Panel panel4;

	private UltraLabel ultraLabel7;

	private Panel panel5;

	private UltraButton ultraButton4;

	private UltraButton BtnPick;

	private C1FlexGrid c1FlexGrid2;

	private System.Windows.Forms.ToolTip toolTip1;

	private UltraButton ultraButton5;

	private UltraToolbarsDockArea _FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Left;

	private UltraToolbarsDockArea _FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Right;

	private UltraToolbarsDockArea _FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Top;

	private UltraToolbarsDockArea _FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Bottom;

	private Panel panel7;

	private UltraLabel ultraLabel8;

	private UltraLabel ultraLabel9;

	private Panel panel8;

	private GroupBox groupBox2;

	private UltraButton A_Btn_Cncl;

	private UltraButton A_Btn_Next;

	private UltraButton ultraButton6;

	private ImageList imageList3;

	private UltraLabel lblDBName;

	private UltraTabPageControl Tab_1;

	private UltraLabel lblProject;

	public GridMrsBase GridUnit1;

	private UltraStatusBar ultraStatusBar1;

	private ImageList imageList2;

	private LevelSwitchButton levelSwitchButton;

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

	public string _ParentCode
	{
		get
		{
			return F_ParentCode;
		}
		set
		{
			F_ParentCode = value;
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

	public FormBudget_PickType CallUpType
	{
		get
		{
			return F_CallUpType;
		}
		set
		{
			F_CallUpType = value;
		}
	}

	public bool _IsCostStructure
	{
		get
		{
			return F_IsCostStructure;
		}
		set
		{
			F_IsCostStructure = value;
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

	public FormPickProjWkItem_Wzd()
	{
		InitializeComponent();
		PwrSet pwrSet = new PwrSet();
		dsPwrSet = pwrSet.GetEnabledPwrSet();
		string sHideCols = CommonMethods.GetDebugValue("FormPickProjWkItem_Wzd", "HideCols");
		HideCols(Convert.ToBoolean((sHideCols == "") ? "True" : sHideCols));
		GridCols = c1FlexGrid2.Cols.Count;
		GridColsSquence = new object[GridCols, 8];
		CellStyle cs1 = GridUnit1.Styles.Add("img");
		cs1.DataType = typeof(Image);
		CellStyle cs2 = c1FlexGrid2.Styles.Add("img");
		cs2.DataType = typeof(Image);
	}

	private void HideCols(bool IsHide)
	{
		if (IsHide)
		{
			c1FlexGrid2.Cols["Kind"].Visible = false;
			c1FlexGrid2.Cols["PrintNo"].Visible = false;
			c1FlexGrid2.Cols["CanCheck"].Visible = false;
			c1FlexGrid2.Cols["SNo"].Visible = false;
			c1FlexGrid2.Cols["PubCode"].Visible = false;
			c1FlexGrid2.Cols["Memo"].Visible = false;
			c1FlexGrid2.Cols["EName"].Visible = false;
			c1FlexGrid2.Cols["eUnit"].Visible = false;
		}
	}

	private void FormPickProjWkItem_Wzd_Load(object sender, EventArgs e)
	{
		SysUser oSysUser = new SysUser();
		F_CurrentDBName = oSysUser.GetSysUserDatabaseName(userID);
		if (SysConfig.SysEnablePwrSet)
		{
			c1FlexGrid2.Cols["PwrSet"].Visible = true;
			c1FlexGrid2.Cols["Account"].Visible = true;
		}
		else
		{
			c1FlexGrid2.Cols["PwrSet"].Visible = false;
			c1FlexGrid2.Cols["Account"].Visible = false;
		}
		LoadDBData();
	}

	private void LoadDBData()
	{
		GeneralManager oManager = new GeneralManager();
		DataSet dsSysPccesSlave;
		DataSet dsPubProject;
		ExecResult ER = oManager.GetSysPccesSlaveIncludeProjectList(userID, IncludeOldVersion: false, out dsSysPccesSlave, out dsPubProject);
		if (ER.ReturnCode != 0)
		{
			MessageBox.Show(this, "資料庫有未知問題發生 : " + ER.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		else
		{
			BindToGrid(dsSysPccesSlave.Tables[0], dsPubProject.Tables[0]);
		}
	}

	private void BindToGrid(DataTable dtSysPccesSlave, DataTable dtPubProject)
	{
		CellStyle CSDatabaseName = GridUnit1.Styles.Add("MainColor");
		CSDatabaseName.ForeColor = Color.Blue;
		CSDatabaseName.Font = new Font(GridUnit1.Font, FontStyle.Bold);
		CellStyle CSError = GridUnit1.Styles.Add("ErrorColor");
		CSError.BackColor = Color.Tomato;
		GridUnit1.Rows.Count = 1;
		ultraStatusBar1.Panels[0].Text = "資料筆數：" + dtSysPccesSlave.Rows.Count;
		GridUnit1.Redraw = false;
		DataView dvPubProject = new DataView(dtPubProject);
		foreach (DataRow theRow in dtSysPccesSlave.Rows)
		{
			if (F_CurrentDBName == CompanyDBName && theRow["dbcName"].ToString().Trim() != CompanyDBName)
			{
				continue;
			}
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
			dvPubProject.RowFilter = "Database ='" + DatabaseName + "'";
			for (int i = 0; i < dvPubProject.Count; i++)
			{
				if (!(dvPubProject[i]["HasBud"].ToString() == "0"))
				{
					GridRow = GridUnit1.Rows.Add();
					GridRow["Flag"] = false;
					GridRow.IsNode = true;
					GridRow.Node.Level = 2;
					GridRow["ProjectCode"] = dvPubProject[i]["ProjectCode"].ToString().Trim();
					GridRow["ProjCName"] = dvPubProject[i]["projCName"].ToString().Trim();
				}
			}
		}
		foreach (Row GridRow in (IEnumerable)GridUnit1.Rows)
		{
			if (GridRow.Node != null && GridRow.Node.Level == 1)
			{
				GridRow.Node.Collapsed = true;
				GridRow["Counts"] = "(" + GridRow.Node.Children + ")";
			}
		}
		GridUnit1.Redraw = true;
	}

	private void BindDataIntoGrid()
	{
		c1FlexGrid1.Cols["projCName"].Style.WordWrap = true;
		c1FlexGrid1.Rows.Count = DT1.Rows.Count;
		for (int i = 0; i < DT1.Rows.Count; i++)
		{
			c1FlexGrid1.Rows[i].Visible = true;
			if (DT1.Rows[i]["projectCode"].ToString().Trim() == projectCode.Trim() && F_CurrentDBName == F_CurrentSelectDBName)
			{
				c1FlexGrid1.Rows[i].Visible = false;
				continue;
			}
			if (DT1.Rows[i]["bud"].ToString().Length <= 0 && DT1.Rows[i]["bid"].ToString().Length > 0)
			{
				c1FlexGrid1.Rows[i].Visible = false;
				continue;
			}
			if (DT1.Rows[i]["bud"].ToString().Trim() != "")
			{
				CellRange rg = c1FlexGrid1.GetCellRange(i, c1FlexGrid1.Cols["IsData"].SafeIndex);
				rg.Style = c1FlexGrid1.Styles["img"];
				rg.Image = imageList2.Images[0];
			}
			c1FlexGrid1[i, "ProjectCode"] = DT1.Rows[i]["projectCode"].ToString().Trim();
			c1FlexGrid1[i, "projCName"] = DT1.Rows[i]["projCName"].ToString().Trim();
			c1FlexGrid1[i, "projEName"] = DT1.Rows[i]["projEName"].ToString().Trim();
			c1FlexGrid1[i, "projAddress"] = DT1.Rows[i]["projAddress"].ToString().Trim();
			c1FlexGrid1.AutoSizeRow(i);
		}
	}

	private void ultraButton3_Click(object sender, EventArgs e)
	{
		int iFind = -1;
		int iStart = c1FlexGrid1.Row + 1;
		for (int i = iStart; i < c1FlexGrid1.Rows.Count; i++)
		{
			iFind = c1FlexGrid1[i, 1].ToString().IndexOf(cbFind.Text.Trim());
			if (iFind > -1)
			{
				iFind = i;
				break;
			}
			iFind = c1FlexGrid1[i, 2].ToString().IndexOf(cbFind.Text.Trim());
			if (iFind > -1)
			{
				iFind = i;
				break;
			}
			iFind = c1FlexGrid1[i, 3].ToString().IndexOf(cbFind.Text.Trim());
			if (iFind > -1)
			{
				iFind = i;
				break;
			}
		}
		if (iFind > -1)
		{
			c1FlexGrid1.Row = iFind;
			bool IsExist = false;
			for (int i = 0; i < cbFind.Items.Count; i++)
			{
				if (cbFind.Items[i].DisplayText.Trim() == cbFind.Text.Trim())
				{
					IsExist = true;
					break;
				}
			}
			if (!IsExist)
			{
				cbFind.Items.Add(cbFind.Text.Trim());
			}
		}
		else if (iFind == -1)
		{
			MessageBox.Show("已完成搜尋資料。找不到搜尋目標。", "搜尋", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void cbFind_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			ultraButton3_Click(this, EventArgs.Empty);
		}
	}

	private void c1FlexGrid1_MouseMove(object sender, MouseEventArgs e)
	{
		int rowIndex = c1FlexGrid1.MouseRow;
		c1FlexGrid1.Row = rowIndex;
	}

	private void c1FlexGrid1_MouseEnter(object sender, EventArgs e)
	{
		cbFind.ButtonAppearance.BackColor = Color.FromArgb(196, 210, 236);
		cbFind.BorderStyle = UIElementBorderStyle.Solid;
	}

	private void c1FlexGrid1_MouseLeave(object sender, EventArgs e)
	{
		cbFind.ButtonAppearance.BackColor = Color.FromArgb(153, 204, 255);
		cbFind.BorderStyle = UIElementBorderStyle.None;
	}

	private void SettingDecimal(string sProjectCode)
	{
		DataTable DTDecimal = new DataTable();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(userID);
		aArr.Add(CommonMethods.GetFormTypeTitle(FormType.Budget));
		Archnowledge.Pcces.BUDClass.PubDecimal dbDecimal = new Archnowledge.Pcces.BUDClass.PubDecimal(aArr);
		DTDecimal = dbDecimal.ListItem("", sProjectCode);
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
		DTDecimal = null;
		aArr = null;
		dbDecimal = null;
	}

	private void RememberColsProps()
	{
		for (int i = 0; i < GridCols; i++)
		{
			GridColsSquence[i, 0] = c1FlexGrid2.Cols[i].Name;
			GridColsSquence[i, 1] = c1FlexGrid2.Cols[i].Caption;
			GridColsSquence[i, 2] = c1FlexGrid2.Cols[i].Width;
			if (c1FlexGrid2.Cols[i].Name == "AnaImg")
			{
				GridColsSquence[i, 3] = typeof(Image);
			}
			else
			{
				GridColsSquence[i, 3] = c1FlexGrid2.Cols[i].DataType;
			}
			GridColsSquence[i, 4] = c1FlexGrid2.Cols[i].Visible;
			GridColsSquence[i, 5] = c1FlexGrid2.Cols[i].Format;
			GridColsSquence[i, 6] = c1FlexGrid2.Cols[i].AllowEditing;
			if (c1FlexGrid2.Cols[i].Name == "Qty")
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
			if (c1FlexGrid2.Cols[i].Name == "Cost")
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
			if (c1FlexGrid2.Cols[i].Name == "Amount")
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
			GridColsSquence[i, 7] = c1FlexGrid2.Cols[i].TextAlign;
		}
	}

	private void c1FlexGrid1_MouseDown(object sender, MouseEventArgs e)
	{
		if (GridUnit1.MouseRow <= -1)
		{
			return;
		}
		int rowIndex = GridUnit1.MouseRow;
		int colIndex = GridUnit1.MouseCol;
		if (rowIndex > 0 && colIndex > 1 && GridUnit1[GridUnit1.Row, "ProjectCode"] != null && !(GridUnit1[GridUnit1.Row, "ProjectCode"].ToString().Trim() == ""))
		{
			Node ParentNode = GridUnit1.Rows[GridUnit1.Row].Node.GetNode(NodeTypeEnum.Parent);
			int ParentRow = ParentNode.Row.SafeIndex;
			F_CurrentSelectDBDesc = GridUnit1[ParentRow, "dbDesc"].ToString();
			F_CurrentSelectDBName = GridUnit1[ParentRow, "dbName"].ToString();
			DBClass DBCLS = new DBClass();
			SysUser oSysUser = new SysUser();
			oSysUser.SetSysUserDatabaseName(userID, F_CurrentSelectDBName);
			if (CheckDBVer())
			{
				ChgStru stdll = new ChgStru();
				stdll.F_UserID = userID;
				stdll.ModifyDatabaseStructure(F_CurrentSelectDBName);
			}
			LoadProjectData();
			lblDBName.Text = "挑選的資料庫:" + F_CurrentSelectDBDesc;
			string sProjectCode = GridUnit1[GridUnit1.Row, "ProjectCode"].ToString().Trim();
			DBCLS._FS_UserID = userID;
			if (!DBCLS.GetProjectAuthority(userID, sProjectCode))
			{
				MessageBox.Show(this, "這個專案您沒有權限，無法開啟。", "專案權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			Tab_B.Tab.Selected = true;
			SettingDecimal(sProjectCode);
			RememberColsProps();
			base.FormBorderStyle = FormBorderStyle.Sizable;
			base.MaximizeBox = true;
			base.MinimizeBox = false;
			ArrayList aArr = new ArrayList();
			aArr.Clear();
			aArr.Add(userID);
			aArr.Add(CommonMethods.GetFormTypeTitle(FormType.Budget));
			ItemA dbItemA = new ItemA(aArr);
			dbItemA.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
			dbItemA.ps_projectCode = sProjectCode;
			DT_FPick = dbItemA.ListItem("", GridUnit1[GridUnit1.Row, "ProjectCode"].ToString());
			BindDataToGrid();
			DBCLS = null;
			aArr = null;
			dbItemA = null;
			lblProject.Text = "挑選的專案:【" + GridUnit1[GridUnit1.Row, "ProjectCode"].ToString().Trim() + "】" + GridUnit1[GridUnit1.Row, "projCName"].ToString();
			Tab_B.Tab.Selected = true;
		}
	}

	private bool CheckDBVer()
	{
		string sBuild = PccesVersion.PccesAssemblyVersion;
		ConnectionStringUtility connUtility = new ConnectionStringUtility(Archnowledge.Pcces.DatabaseAccess.DatabaseAccess.PccesConnectionString());
		string SelectedConnectionString = connUtility.GetSqlConnectionString(F_CurrentSelectDBName);
		string DBVer = PccesVersion.GetDatabaseVersion(SelectedConnectionString);
		if (!DBVer.Equals(sBuild))
		{
			return true;
		}
		return false;
	}

	private void SetGridColumn()
	{
		for (int i = 0; i < GridCols; i++)
		{
			c1FlexGrid2.Cols[i].Name = (string)GridColsSquence[i, 0];
			c1FlexGrid2.Cols[i].Caption = (string)GridColsSquence[i, 1];
			c1FlexGrid2.Cols[i].Width = (int)GridColsSquence[i, 2];
			c1FlexGrid2.Cols[i].DataType = (Type)GridColsSquence[i, 3];
			c1FlexGrid2.Cols[i].Visible = (bool)GridColsSquence[i, 4];
			c1FlexGrid2.Cols[i].Format = (string)GridColsSquence[i, 5];
			c1FlexGrid2.Cols[i].AllowEditing = (bool)GridColsSquence[i, 6];
			c1FlexGrid2.Cols[i].TextAlign = (TextAlignEnum)GridColsSquence[i, 7];
		}
	}

	private void BindDataToGrid()
	{
		int iLevel = 0;
		c1FlexGrid2.Rows.Count = DT_FPick.Rows.Count + 1;
		string sTmpStr = "";
		RememberColsProps();
		c1FlexGrid2.Clear(ClearFlags.All);
		CellStyle CS1 = c1FlexGrid2.Styles.Add("AnalysisColor");
		CellStyle CS2 = c1FlexGrid2.Styles.Add("MainColor");
		CellStyle CS9 = c1FlexGrid2.Styles.Add("IsSharedColor");
		CS1.ForeColor = Color.Red;
		CS2.ForeColor = Color.Blue;
		CS9.ForeColor = Color.Plum;
		SetGridColumn();
		string sKind = "";
		for (int i = 0; i < DT_FPick.Rows.Count; i++)
		{
			c1FlexGrid2.Rows[i + 1].IsNode = true;
			sKind = ((DT_FPick.Rows[i]["kind"].ToString().Length > 0) ? DT_FPick.Rows[i]["kind"].ToString().ToUpper().Trim() : "");
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
				c1FlexGrid2.Rows[i + 1].Style = c1FlexGrid2.Styles["MainColor"];
				break;
			}
			if (DT_FPick.Rows[i]["analysis"].ToString().Trim() == "1")
			{
				c1FlexGrid2.Rows[i + 1].Style = c1FlexGrid2.Styles["AnalysisColor"];
			}
			sTmpStr = DT_FPick.Rows[i]["PrintNo"].ToString().Trim();
			c1FlexGrid2[i + 1, "ItemNo"] = DT_FPick.Rows[i]["ItemNo"].ToString().Trim();
			c1FlexGrid2[i + 1, "CName"] = DT_FPick.Rows[i]["CName"].ToString().Trim();
			c1FlexGrid2[i + 1, "Kind"] = DT_FPick.Rows[i]["kind"].ToString().Trim();
			c1FlexGrid2[i + 1, "PrintNo"] = DT_FPick.Rows[i]["PrintNo"].ToString().Trim();
			c1FlexGrid2[i + 1, "SNo"] = DT_FPick.Rows[i]["sNo"].ToString().Trim();
			c1FlexGrid2[i + 1, "PubCode"] = DT_FPick.Rows[i]["PubCode"].ToString().Trim();
			c1FlexGrid2[i + 1, "PccesCode"] = DT_FPick.Rows[i]["PccesCode"].ToString().Trim();
			c1FlexGrid2[i + 1, "Qty"] = DT_FPick.Rows[i]["qty"].ToString().Trim();
			c1FlexGrid2[i + 1, "Cost"] = DT_FPick.Rows[i]["Cost"].ToString().Trim();
			c1FlexGrid2[i + 1, "UnitName"] = DT_FPick.Rows[i]["unitName"].ToString().Trim();
			c1FlexGrid2[i + 1, "Memo"] = DT_FPick.Rows[i]["memo"].ToString().Trim();
			c1FlexGrid2[i + 1, "EName"] = DT_FPick.Rows[i]["eName"].ToString().Trim();
			c1FlexGrid2[i + 1, "EUnit"] = DT_FPick.Rows[i]["eUnit"].ToString().Trim();
			c1FlexGrid2[i + 1, "surName"] = DT_FPick.Rows[i]["surName"].ToString().Trim();
			c1FlexGrid2[i + 1, "fixPrice"] = DT_FPick.Rows[i]["fixPrice"].ToString().Trim() == "1";
			c1FlexGrid2[i + 1, "Account"] = DT_FPick.Rows[i]["Account"].ToString().Trim();
			if (sKind == "W")
			{
				if (DT_FPick.Rows[i]["PwrSet"] != DBNull.Value)
				{
					c1FlexGrid2[i + 1, "PwrSet"] = PwrSet.GetName(dsPwrSet, PubTools.Str2Int(DT_FPick.Rows[i]["PwrSet"]));
				}
				else
				{
					c1FlexGrid2[i + 1, "PwrSet"] = PwrSet.GetDefaultName(dsPwrSet);
				}
			}
			else
			{
				c1FlexGrid2[i + 1, "PwrSet"] = "";
			}
			if (DT_FPick.Rows[i]["PrintNo"].ToString().Trim() == "".PadLeft(32, '9') || (i == DT_FPick.Rows.Count - 1 && sKind == "Z" && DT_FPick.Rows[i]["PrintNo"].ToString().Trim().Length == 4))
			{
				c1FlexGrid2[i + 1, "CanCheck"] = false;
			}
			else
			{
				c1FlexGrid2[i + 1, "CanCheck"] = !F_IsCostStructure || sKind == "W";
			}
			string st1 = DT_FPick.Rows[i]["PrintNo"].ToString().Trim();
			c1FlexGrid2[i + 1, "IsCheck"] = false;
			c1FlexGrid2.Rows[i + 1].Node.Level = Convert.ToInt32(DT_FPick.Rows[i]["PrintNo"].ToString().Trim().Length / 4);
			if (DT_FPick.Rows[i]["PrintNo"].ToString().Trim() == "".PadLeft(32, '9'))
			{
				c1FlexGrid2.Rows[i + 1].Node.Level = 1;
			}
			if (c1FlexGrid2.Rows[i + 1].Node.Level > iLevel)
			{
				iLevel = c1FlexGrid2.Rows[i + 1].Node.Level;
			}
		}
		levelSwitchButton.MaxLevel = iLevel;
	}

	private void ultraButton5_Click(object sender, EventArgs e)
	{
		Tab_1.Tab.Selected = true;
	}

	private void ultraToolbarsManager1_ToolClick(object sender, ToolClickEventArgs e)
	{
		switch (e.Tool.Key)
		{
		case "mnuSelAll":
			DoSelAll();
			break;
		case "mnuSelCancel":
			DoSelCancel();
			break;
		case "mnuSelReverse":
			DoSelReverse();
			break;
		}
	}

	private void DoSelAll()
	{
		for (int i = 1; i < c1FlexGrid2.Rows.Count; i++)
		{
			if ((bool)c1FlexGrid2[i, "CanCheck"])
			{
				c1FlexGrid2[i, "IsCheck"] = true;
			}
		}
	}

	private void DoSelCancel()
	{
		for (int i = 1; i < c1FlexGrid2.Rows.Count; i++)
		{
			c1FlexGrid2[i, "IsCheck"] = false;
		}
	}

	private void DoSelReverse()
	{
		for (int i = 1; i < c1FlexGrid2.Rows.Count; i++)
		{
			if ((bool)c1FlexGrid2[i, "CanCheck"])
			{
				if ((bool)c1FlexGrid2[i, "IsCheck"])
				{
					c1FlexGrid2[i, "IsCheck"] = false;
				}
				else
				{
					c1FlexGrid2[i, "IsCheck"] = true;
				}
			}
		}
	}

	private void c1FlexGrid2_AfterEdit(object sender, RowColEventArgs e)
	{
		if (!(bool)c1FlexGrid2[e.Row, "CanCheck"])
		{
			c1FlexGrid2[e.Row, "IsCheck"] = false;
		}
		if (c1FlexGrid2.Cols[c1FlexGrid2.MouseCol].Name != "IsCheck")
		{
			c1FlexGrid2[e.Row, "IsCheck"] = true;
		}
		try
		{
			bool check = (bool)c1FlexGrid2[e.Row, "IsCheck"];
			Node LastNode = c1FlexGrid2.Rows[c1FlexGrid2.Row].Node;
			while (LastNode != null && LastNode.Children > 0)
			{
				LastNode = LastNode.GetNode(NodeTypeEnum.LastChild);
			}
			for (int i = c1FlexGrid2.Row; i <= LastNode.Row.SafeIndex; i++)
			{
				c1FlexGrid2[i, "IsCheck"] = check;
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Budget.FormPickProjWkItem_Wzd.cs" + ex.Message);
		}
	}

	private void BtnPick_Click(object sender, EventArgs e)
	{
		Cursor = Cursors.WaitCursor;
		try
		{
			DataTable DT_PickTemp = new DataTable();
			DT_PickTemp.Columns.Add("ProjectCode", Type.GetType("System.String"));
			DT_PickTemp.Columns.Add("ItemNo", Type.GetType("System.String"));
			DT_PickTemp.Columns.Add("CName", Type.GetType("System.String"));
			DT_PickTemp.Columns.Add("unitName", Type.GetType("System.String"));
			DT_PickTemp.Columns.Add("cost", Type.GetType("System.Double"));
			DT_PickTemp.Columns.Add("memo", Type.GetType("System.String"));
			DT_PickTemp.Columns.Add("EName", Type.GetType("System.String"));
			DT_PickTemp.Columns.Add("eUnit", Type.GetType("System.String"));
			DT_PickTemp.Columns.Add("PrintNo", Type.GetType("System.String"));
			DT_PickTemp.Columns.Add("sNO", Type.GetType("System.String"));
			DT_PickTemp.Columns.Add("PubCode", Type.GetType("System.String"));
			DT_PickTemp.Columns.Add("PccesCode", Type.GetType("System.String"));
			DT_PickTemp.Columns.Add("Qty", Type.GetType("System.String"));
			DT_PickTemp.Columns.Add("Level", Type.GetType("System.Int32"));
			DT_PickTemp.Columns.Add("DBName", Type.GetType("System.String"));
			DT_PickTemp.Columns.Add("Kind", Type.GetType("System.String"));
			DT_PickTemp.Columns.Add("surName", Type.GetType("System.String"));
			DT_PickTemp.Columns.Add("fixPrice", Type.GetType("System.String"));
			DT_PickTemp.Columns.Add("PwrSet", Type.GetType("System.String"));
			DT_PickTemp.Columns.Add("Account", Type.GetType("System.String"));
			DT_PickTemp.Columns.Add("OverWriteWorkItem", Type.GetType("System.String"));
			DT_PickTemp.Columns.Add("CancelInsert", Type.GetType("System.String"));
			MrsBaseD mrsBaseD = new MrsBaseD();
			ExecResult ER = new ExecResult();
			string oldCname = "";
			string oldUnitName = "";
			string newPccesCode = "";
			string s = "";
			DBClass DBCls = new DBClass();
			DBCls._FS_UserID = userID;
			string sSrcKind = CommonMethods.GetActionNameString(FormActionName);
			string sMessage = "";
			for (int i = 1; i < c1FlexGrid2.Rows.Count; i++)
			{
				if (!(bool)c1FlexGrid2[i, "IsCheck"])
				{
					continue;
				}
				string thisPccesCode = ArchConvert.Obj2String(c1FlexGrid2[i, "PccesCode"]);
				bool flag = false;
				if (SysConfig.SysComsEnable && DBCls.IsPccesCodeExistsInMrsBaseA(thisPccesCode, projectCode, sSrcKind) && sSrcKind.Trim() == "")
				{
					string text = sMessage;
					sMessage = text + thisPccesCode + "\t" + c1FlexGrid2[i, "CName"].ToString() + "\n";
					continue;
				}
				ER = mrsBaseD.QueryPccesReplacing(thisPccesCode, out oldCname, out oldUnitName, out newPccesCode);
				if (newPccesCode.Length > 0)
				{
					s = "PccesCode:" + thisPccesCode + "\n項目：" + oldCname + "\n單位：" + oldUnitName + "\n\n在共通項目轉碼對照表中存在，但PccesCode已換為:\n" + newPccesCode + "\n\n是否要換碼？";
					ER = mrsBaseD.MrsBaseDoverwriteBudProjMrsA(thisPccesCode, projectCode);
					thisPccesCode = newPccesCode;
					c1FlexGrid2[i, "PccesCode"] = thisPccesCode;
				}
				DataRow DR = DT_PickTemp.NewRow();
				DR["ProjectCode"] = GridUnit1[GridUnit1.Row, "ProjectCode"];
				DR["ItemNo"] = c1FlexGrid2[i, "ItemNo"];
				DR["CName"] = c1FlexGrid2[i, "CName"];
				DR["UnitName"] = c1FlexGrid2[i, "unitName"];
				DR["cost"] = c1FlexGrid2[i, "Cost"];
				DR["EName"] = c1FlexGrid2[i, "EName"];
				DR["eUnit"] = c1FlexGrid2[i, "EUnit"];
				DR["PrintNo"] = c1FlexGrid2[i, "PrintNo"];
				DR["sNO"] = c1FlexGrid2[i, "sNO"];
				DR["PubCode"] = c1FlexGrid2[i, "PubCode"];
				DR["PccesCode"] = c1FlexGrid2[i, "PccesCode"];
				DR["Qty"] = c1FlexGrid2[i, "Qty"];
				DR["Level"] = c1FlexGrid2.Rows[i].Node.Level;
				DR["DBName"] = F_CurrentSelectDBName;
				DR["Kind"] = c1FlexGrid2[i, "Kind"];
				DR["surName"] = c1FlexGrid2[i, "surName"];
				if (c1FlexGrid2[i, "fixPrice"] != null && ArchConvert.Obj2Bool(c1FlexGrid2[i, "fixPrice"]))
				{
					DR["fixPrice"] = "1";
				}
				DR["memo"] = c1FlexGrid2[i, "Memo"];
				DR["PwrSet"] = c1FlexGrid2[i, "PwrSet"];
				DR["Account"] = c1FlexGrid2[i, "Account"];
				DR["OverWriteWorkItem"] = "0";
				DR["CancelInsert"] = "0";
				DT_PickTemp.Rows.Add(DR);
				DT_PickTemp.AcceptChanges();
			}
			if (DT_PickTemp.Rows.Count == 0)
			{
				MessageBox.Show("未勾選任何項目,請確認");
				Cursor = Cursors.Default;
				return;
			}
			bool IsValid = true;
			string sMessCheck = "";
			if (DT_PickTemp.Rows.Count > 0)
			{
				DataRow theRow = DT_PickTemp.Rows[0];
				int PreLevel = ArchConvert.Obj2Int(theRow["Level"]);
				string PreKind = ArchConvert.Obj2String(theRow["Kind"]);
				int RootLevel = PreLevel;
				for (int i = 0; i < DT_PickTemp.Rows.Count; i++)
				{
					theRow = DT_PickTemp.Rows[i];
					int Level = ArchConvert.Obj2Int(theRow["Level"]);
					string Kind = ArchConvert.Obj2String(theRow["Kind"]);
					if (PreLevel > Level || PreLevel - Level > 1)
					{
						IsValid = false;
						sMessCheck = "挑選的項目階層, 不可以有小於第一項階層且不可以跳階。";
						break;
					}
					if (PreKind == "W" && Kind == "W")
					{
						DT_PickTemp.Rows[i]["Level"] = PreLevel - RootLevel;
						continue;
					}
					PreKind = ArchConvert.Obj2String(theRow["Kind"]);
					PreLevel = ArchConvert.Obj2Int(theRow["Level"]);
					DT_PickTemp.Rows[i]["Level"] = ArchConvert.Obj2Int(theRow["Level"]) - RootLevel;
				}
			}
			if (!IsValid)
			{
				MessageBox.Show(this, sMessCheck, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			SysUser oSysUser = new SysUser();
			oSysUser.SetSysUserDatabaseName(userID, F_CurrentDBName);
			Archnowledge.Pcces.DatabaseAccess.DatabaseAccess.UseDatabase(F_CurrentDBName);
			BudProjMrsA theBudProjMrsA = new BudProjMrsA();
			BudItemA theBudItemA = new BudItemA();
			for (int i = 0; i < DT_PickTemp.Rows.Count; i++)
			{
				DataSet dsMrsA = theBudProjMrsA.GetProjMrsAByPccesCode(projectCode, DT_PickTemp.Rows[i]["PccesCode"].ToString().Trim());
				DataSet dsItemA = theBudItemA.GetItemAByPccesCode(projectCode, DT_PickTemp.Rows[i]["PccesCode"].ToString().Trim());
				try
				{
					bool IsLocked = !theBudProjMrsA.CheckSourceItemCanOverwrite(DT_PickTemp.Rows[0]["DBName"].ToString(), DT_PickTemp.Rows[0]["ProjectCode"].ToString().Trim(), DT_PickTemp.Rows[i]["PccesCode"].ToString().Trim(), projectCode);
					if (dsMrsA.Tables[0].Rows.Count <= 0)
					{
						continue;
					}
					if (!IsLocked)
					{
						if (MessageBox.Show(this, "注意!此工項代碼[" + DT_PickTemp.Rows[i]["PccesCode"].ToString().Trim() + "(" + DT_PickTemp.Rows[i]["CName"].ToString() + ")]已存在專案中，是否要以來源專案工項覆蓋掉現有專案工項?\n(是:以挑選工項覆蓋專案工項 否:直接引用原專案工項)", "訊問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
						{
							DT_PickTemp.Rows[i]["OverWriteWorkItem"] = "1";
							continue;
						}
						DT_PickTemp.Rows[i]["OverWriteWorkItem"] = "0";
						if (dsItemA.Tables[0].Rows.Count > 0)
						{
							DataRow Row = dsItemA.Tables[0].Rows[0];
							DT_PickTemp.Rows[i]["ProjectCode"] = Row["ProjectCode"];
							DT_PickTemp.Rows[i]["CName"] = Row["CName"];
							DT_PickTemp.Rows[i]["UnitName"] = Row["UnitName"];
							DT_PickTemp.Rows[i]["cost"] = Row["cost"];
							DT_PickTemp.Rows[i]["EName"] = Row["EName"];
							DT_PickTemp.Rows[i]["eUnit"] = Row["eUnit"];
							DT_PickTemp.Rows[i]["sNO"] = Row["sNO"];
							DT_PickTemp.Rows[i]["PubCode"] = Row["PubCode"];
							DT_PickTemp.Rows[i]["PccesCode"] = Row["PccesCode"];
							DT_PickTemp.Rows[i]["Qty"] = Row["Qty"];
							DT_PickTemp.Rows[i]["Kind"] = Row["Kind"];
							DT_PickTemp.Rows[i]["surName"] = Row["surName"];
							DT_PickTemp.Rows[i]["fixPrice"] = Row["fixPrice"];
							DT_PickTemp.Rows[i]["memo"] = Row["memo"];
							DT_PickTemp.Rows[i]["PwrSet"] = Row["PwrSet"];
						}
					}
					else if (MessageBox.Show(this, "此工項代碼[" + DT_PickTemp.Rows[i]["PccesCode"].ToString().Trim() + "(" + DT_PickTemp.Rows[i]["CName"].ToString() + ")]已存在專案中且已被鎖定不可自其他專案覆蓋，是否直接引用原專案工項?\n(是:直接引用原專案工項 否:取消此挑選工項的新增)", "訊問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						if (dsItemA.Tables[0].Rows.Count > 0)
						{
							DataRow Row = dsItemA.Tables[0].Rows[0];
							DT_PickTemp.Rows[i]["ProjectCode"] = Row["ProjectCode"];
							DT_PickTemp.Rows[i]["CName"] = Row["CName"];
							DT_PickTemp.Rows[i]["UnitName"] = Row["UnitName"];
							DT_PickTemp.Rows[i]["cost"] = Row["cost"];
							DT_PickTemp.Rows[i]["EName"] = Row["EName"];
							DT_PickTemp.Rows[i]["eUnit"] = Row["eUnit"];
							DT_PickTemp.Rows[i]["sNO"] = Row["sNO"];
							DT_PickTemp.Rows[i]["PubCode"] = Row["PubCode"];
							DT_PickTemp.Rows[i]["PccesCode"] = Row["PccesCode"];
							DT_PickTemp.Rows[i]["Qty"] = Row["Qty"];
							DT_PickTemp.Rows[i]["Kind"] = Row["Kind"];
							DT_PickTemp.Rows[i]["surName"] = Row["surName"];
							DT_PickTemp.Rows[i]["fixPrice"] = Row["fixPrice"];
							DT_PickTemp.Rows[i]["memo"] = Row["memo"];
							DT_PickTemp.Rows[i]["PwrSet"] = Row["PwrSet"];
							DT_PickTemp.Rows[i]["OverWriteWorkItem"] = "0";
						}
					}
					else
					{
						DT_PickTemp.Rows[i]["CancelInsert"] = "1";
					}
				}
				catch (Exception ex)
				{
					if (ex.Message.Contains("FuncGetAllChildPccesCode"))
					{
						MessageBox.Show("找不到函式dbo.FuncGetAllChildPccesCode,請檢查來源資料庫已升至最新版本");
					}
					else
					{
						MessageBox.Show("檢查來源資料庫專案挑選工項至目標專案時發生錯誤:" + ex.Message);
					}
					return;
				}
			}
			DataTable tmpdt = DT_PickTemp.Clone();
			foreach (DataRow row in DT_PickTemp.Rows)
			{
				if (row["CancelInsert"].ToString() == "0")
				{
					tmpdt.ImportRow(row);
				}
			}
			DT_PickTemp.Clear();
			DT_PickTemp = tmpdt.Copy();
			tmpdt.Dispose();
			Form ActiveForm = base.Owner.ActiveMdiChild;
			if (ActiveForm is frmBudget)
			{
				(ActiveForm as frmBudget)._PasteSource_SrcKind = CommonMethods.GetActionNameString(FormActionName);
				(ActiveForm as frmBudget)._PasteSource_Project = GridUnit1[GridUnit1.Row, "ProjectCode"].ToString().Trim();
				(ActiveForm as frmBudget).ItemPasteFromProjectItemPick(DT_PickTemp);
			}
			Cursor = Cursors.Default;
			base.DialogResult = DialogResult.OK;
			if (sMessage.Trim() != "")
			{
				MessageBox.Show(this, sMessage.Trim() + "\n\n已存在，不可重複新增!", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		catch (Exception ex2)
		{
			MessageBox.Show("BtnPick_Click Error = " + ex2.Message);
			Cursor = Cursors.Default;
		}
	}

	private string GetIsfixPrice(string PccesCode)
	{
		string rtnStr = "";
		string sSQL = "Select * from " + CommonMethods.GetActionNameString(FormActionName) + "ProjMrsA where projectCode = '" + projectCode + "' and PccesCode='" + PccesCode + "'";
		ArrayList aArr = new ArrayList();
		aArr.Add(userID);
		aArr.Add("取pccescode的值");
		ModifyDB ModDB = new ModifyDB(projectCode, aArr);
		DataTable DT = new DataTable();
		DT = ModDB.DBList(sSQL);
		if (DT.Rows.Count > 0 && DT.Rows[0]["fixPrice"].ToString().Trim() == "1" && DT.Rows[0]["costKind"].ToString().Trim() == "")
		{
			rtnStr = "1";
		}
		ModDB = null;
		aArr = null;
		return rtnStr;
	}

	private void c1FlexGrid2_KeyPress(object sender, KeyPressEventArgs e)
	{
		int iSelRows = c1FlexGrid2.Selection.r2 - c1FlexGrid2.Selection.r1;
		if (iSelRows <= 1)
		{
			return;
		}
		for (int i = c1FlexGrid2.Selection.r1; i <= c1FlexGrid2.Selection.r2; i++)
		{
			if ((bool)c1FlexGrid2[i, "CanCheck"])
			{
				c1FlexGrid2[i, "IsCheck"] = true;
			}
		}
	}

	private void ultraToolbarsManager1_BeforeToolbarListDropdown(object sender, BeforeToolbarListDropdownEventArgs e)
	{
		e.Cancel = true;
	}

	private void LoadProjectData()
	{
		ArrayList aArr = new ArrayList();
		aArr.Add(userID);
		aArr.Add(CommonMethods.GetFormTypeTitle(FormType.Budget));
		Archnowledge.Pcces.BUDClass.PubProject dbProject = new Archnowledge.Pcces.BUDClass.PubProject(aArr);
		DT1 = dbProject.ListItem("");
		dbProject = null;
		aArr = null;
		BindDataIntoGrid();
	}

	private void GridUnit1_MouseMove(object sender, MouseEventArgs e)
	{
		if (GridUnit1.MouseRow > 0 && GridUnit1.MouseCol > 0)
		{
			int rowIndex = GridUnit1.MouseRow;
			GridUnit1.Row = rowIndex;
			GridUnit1.Select();
		}
	}

	private void A_Btn_Next_Click(object sender, EventArgs e)
	{
		c1FlexGrid1_MouseDown(sender, null);
	}

	private void ultraButton6_Click(object sender, EventArgs e)
	{
		Tab_1.Tab.Selected = true;
	}

	private void FormPickProjWkItem_Wzd_FormClosing(object sender, FormClosingEventArgs e)
	{
		SysUser oSysUser = new SysUser();
		oSysUser.SetSysUserDatabaseName(userID, F_CurrentDBName);
		Archnowledge.Pcces.DatabaseAccess.DatabaseAccess.UseDatabase(F_CurrentDBName);
	}

	private void FormPickProjWkItem_Wzd_FormClosed(object sender, FormClosedEventArgs e)
	{
		imageList1 = null;
		Tabs_Ctrl = null;
		ultraTabSharedControlsPage1 = null;
		Tab_A = null;
		Tab_B = null;
		panel1 = null;
		ultraButton3 = null;
		ultraButton2 = null;
		panel2 = null;
		c1FlexGrid1 = null;
		panel3 = null;
		ultraLabel5 = null;
		ultraLabel4 = null;
		ultraLabel6 = null;
		ultraLabel3 = null;
		cbFind = null;
		ultraButton1 = null;
		ultraLabel2 = null;
		ultraLabel1 = null;
		panel4 = null;
		ultraLabel7 = null;
		panel5 = null;
		ultraButton4 = null;
		BtnPick = null;
		c1FlexGrid2 = null;
		toolTip1 = null;
		ultraButton5 = null;
		ultraToolbarsManager1 = null;
		_FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Left = null;
		_FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Right = null;
		_FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Top = null;
		_FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Bottom = null;
		DT_Temp = null;
		DT1 = null;
		GridColsSquence = null;
		panel7 = null;
		ultraLabel8 = null;
		ultraLabel9 = null;
		panel8 = null;
		groupBox2 = null;
		A_Btn_Cncl = null;
		A_Btn_Next = null;
		ultraButton6 = null;
		imageList3 = null;
		lblDBName = null;
		Tab_1 = null;
		lblProject = null;
		GridUnit1 = null;
		ultraStatusBar1 = null;
		imageList2 = null;
		DT_FPick = null;
		GC.Collect();
	}

	private void levelSwitchButton_LevelSwitchButtonsClicked()
	{
		c1FlexGrid2.Tree.Show(levelSwitchButton.SelectedLevel);
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormPickProjWkItem_Wzd));
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
		Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Popup1");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSelAll");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSelCancel");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSelReverse");
		Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool1 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("PopMenu");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSelAll");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool5 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSelReverse");
		Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool6 = new Infragistics.Win.UltraWinToolbars.ButtonTool("mnuSelCancel");
		this.Tab_1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.GridUnit1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.ultraStatusBar1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.panel8 = new System.Windows.Forms.Panel();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.A_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.A_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.panel7 = new System.Windows.Forms.Panel();
		this.ultraLabel8 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel9 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_A = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel1 = new System.Windows.Forms.Panel();
		this.lblDBName = new Infragistics.Win.Misc.UltraLabel();
		this.ultraButton6 = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton3 = new Infragistics.Win.Misc.UltraButton();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.ultraButton2 = new Infragistics.Win.Misc.UltraButton();
		this.panel2 = new System.Windows.Forms.Panel();
		this.c1FlexGrid1 = new C1.Win.C1FlexGrid.C1FlexGrid();
		this.panel3 = new System.Windows.Forms.Panel();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.cbFind = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_B = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.c1FlexGrid2 = new C1.Win.C1FlexGrid.C1FlexGrid();
		this.panel5 = new System.Windows.Forms.Panel();
		this.lblProject = new Infragistics.Win.Misc.UltraLabel();
		this.ultraButton5 = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton4 = new Infragistics.Win.Misc.UltraButton();
		this.BtnPick = new Infragistics.Win.Misc.UltraButton();
		this.panel4 = new System.Windows.Forms.Panel();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.Tabs_Ctrl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.ultraToolbarsManager1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
		this._FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this._FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
		this.imageList3 = new System.Windows.Forms.ImageList(this.components);
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.levelSwitchButton = new Archnowledge.Pcces.PccesMain.ArchControls.LevelSwitchButton();
		this.Tab_1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.GridUnit1).BeginInit();
		this.panel8.SuspendLayout();
		this.panel7.SuspendLayout();
		this.Tab_A.SuspendLayout();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid1).BeginInit();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cbFind).BeginInit();
		this.Tab_B.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid2).BeginInit();
		this.panel5.SuspendLayout();
		this.panel4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Tabs_Ctrl).BeginInit();
		this.Tabs_Ctrl.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraToolbarsManager1).BeginInit();
		base.SuspendLayout();
		this.Tab_1.Controls.Add(this.GridUnit1);
		this.Tab_1.Controls.Add(this.ultraStatusBar1);
		this.Tab_1.Controls.Add(this.panel8);
		this.Tab_1.Controls.Add(this.panel7);
		this.Tab_1.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_1.Name = "Tab_1";
		this.Tab_1.Size = new System.Drawing.Size(698, 447);
		this.GridUnit1._ExcelFileName = "";
		this.GridUnit1._ExcelSheeName = "";
		this.GridUnit1._IsOpenExcelAfterExport = false;
		this.GridUnit1.AllowEditing = false;
		this.GridUnit1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
		this.GridUnit1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.GridUnit1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
		this.GridUnit1.ColumnInfo = resources.GetString("GridUnit1.ColumnInfo");
		this.GridUnit1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GridUnit1.ExtendLastCol = true;
		this.GridUnit1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.GridUnit1.ForeColor = System.Drawing.Color.Black;
		this.GridUnit1.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.GridUnit1.IsProcessUndo = false;
		this.GridUnit1.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.GridUnit1.Location = new System.Drawing.Point(0, 48);
		this.GridUnit1.Name = "GridUnit1";
		this.GridUnit1.Rows.Count = 1;
		this.GridUnit1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.GridUnit1.ShowCursor = true;
		this.GridUnit1.ShowToolTipOnNarrowColumn = true;
		this.GridUnit1.Size = new System.Drawing.Size(698, 332);
		this.GridUnit1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("GridUnit1.Styles"));
		this.GridUnit1.TabIndex = 20;
		this.GridUnit1.Tree.Column = 1;
		this.GridUnit1.UndoMax = 10;
		this.GridUnit1.MouseDown += new System.Windows.Forms.MouseEventHandler(c1FlexGrid1_MouseDown);
		this.GridUnit1.MouseMove += new System.Windows.Forms.MouseEventHandler(GridUnit1_MouseMove);
		appearance1.BackColor = System.Drawing.SystemColors.Control;
		appearance1.FontData.Name = "細明體";
		appearance1.FontData.SizeInPoints = 11f;
		this.ultraStatusBar1.Appearance = appearance1;
		this.ultraStatusBar1.Location = new System.Drawing.Point(0, 380);
		this.ultraStatusBar1.Name = "ultraStatusBar1";
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
		this.ultraStatusBar1.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[3] { ultraStatusPanel1, ultraStatusPanel2, ultraStatusPanel3 });
		this.ultraStatusBar1.Size = new System.Drawing.Size(698, 23);
		this.ultraStatusBar1.SupportThemes = false;
		this.ultraStatusBar1.TabIndex = 21;
		this.ultraStatusBar1.Text = "ultraStatusBar1";
		this.panel8.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel8.Controls.Add(this.groupBox2);
		this.panel8.Controls.Add(this.A_Btn_Cncl);
		this.panel8.Controls.Add(this.A_Btn_Next);
		this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel8.Location = new System.Drawing.Point(0, 403);
		this.panel8.Name = "panel8";
		this.panel8.Size = new System.Drawing.Size(698, 44);
		this.panel8.TabIndex = 19;
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(698, 8);
		this.groupBox2.TabIndex = 3;
		this.groupBox2.TabStop = false;
		this.A_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance4.Image = resources.GetObject("appearance4.Image");
		appearance4.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Cncl.Appearance = appearance4;
		this.A_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.A_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_Cncl.Location = new System.Drawing.Point(605, 10);
		this.A_Btn_Cncl.Name = "A_Btn_Cncl";
		this.A_Btn_Cncl.ShowFocusRect = false;
		this.A_Btn_Cncl.ShowOutline = false;
		this.A_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.A_Btn_Cncl.SupportThemes = false;
		this.A_Btn_Cncl.TabIndex = 2;
		this.A_Btn_Cncl.Text = "取消";
		this.A_Btn_Next.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance5.Image = resources.GetObject("appearance5.Image");
		appearance5.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance5.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Next.Appearance = appearance5;
		this.A_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_Next.Location = new System.Drawing.Point(513, 10);
		this.A_Btn_Next.Name = "A_Btn_Next";
		this.A_Btn_Next.ShowFocusRect = false;
		this.A_Btn_Next.ShowOutline = false;
		this.A_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.A_Btn_Next.SupportThemes = false;
		this.A_Btn_Next.TabIndex = 1;
		this.A_Btn_Next.Text = "下一步";
		this.A_Btn_Next.Click += new System.EventHandler(A_Btn_Next_Click);
		this.panel7.BackColor = System.Drawing.Color.White;
		this.panel7.Controls.Add(this.ultraLabel8);
		this.panel7.Controls.Add(this.ultraLabel9);
		this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel7.Location = new System.Drawing.Point(0, 0);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(698, 48);
		this.panel7.TabIndex = 16;
		appearance6.BackColor = System.Drawing.Color.White;
		this.ultraLabel8.Appearance = appearance6;
		this.ultraLabel8.Location = new System.Drawing.Point(44, 27);
		this.ultraLabel8.Name = "ultraLabel8";
		this.ultraLabel8.Size = new System.Drawing.Size(620, 20);
		this.ultraLabel8.TabIndex = 3;
		this.ultraLabel8.Text = "請將要挑用的資料庫之前的加號(+)展開，再點選要挑用的專案即可";
		appearance7.BackColor = System.Drawing.Color.White;
		this.ultraLabel9.Appearance = appearance7;
		this.ultraLabel9.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel9.Location = new System.Drawing.Point(12, 7);
		this.ultraLabel9.Name = "ultraLabel9";
		this.ultraLabel9.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel9.TabIndex = 2;
		this.ultraLabel9.Text = "專案工項來源";
		this.Tab_A.Controls.Add(this.panel1);
		this.Tab_A.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_A.Name = "Tab_A";
		this.Tab_A.Size = new System.Drawing.Size(698, 447);
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel1.Controls.Add(this.lblDBName);
		this.panel1.Controls.Add(this.ultraButton6);
		this.panel1.Controls.Add(this.ultraButton3);
		this.panel1.Controls.Add(this.ultraButton2);
		this.panel1.Controls.Add(this.panel2);
		this.panel1.Controls.Add(this.cbFind);
		this.panel1.Controls.Add(this.ultraButton1);
		this.panel1.Controls.Add(this.ultraLabel2);
		this.panel1.Controls.Add(this.ultraLabel1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(698, 447);
		this.panel1.TabIndex = 2;
		appearance8.FontData.Name = "細明體";
		appearance8.FontData.SizeInPoints = 9f;
		appearance8.ForeColor = System.Drawing.Color.Blue;
		this.lblDBName.Appearance = appearance8;
		this.lblDBName.Location = new System.Drawing.Point(6, 418);
		this.lblDBName.Name = "lblDBName";
		this.lblDBName.Size = new System.Drawing.Size(486, 23);
		this.lblDBName.TabIndex = 12;
		this.lblDBName.Text = "挑選的資料庫:";
		this.ultraButton6.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance9.Image = resources.GetObject("appearance9.Image");
		appearance9.ImageHAlign = Infragistics.Win.HAlign.Left;
		appearance9.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton6.Appearance = appearance9;
		this.ultraButton6.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton6.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton6.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton6.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton6.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton6.Location = new System.Drawing.Point(500, 411);
		this.ultraButton6.Name = "ultraButton6";
		this.ultraButton6.ShowFocusRect = false;
		this.ultraButton6.ShowOutline = false;
		this.ultraButton6.Size = new System.Drawing.Size(88, 31);
		this.ultraButton6.SupportThemes = false;
		this.ultraButton6.TabIndex = 11;
		this.ultraButton6.Text = "上一步";
		this.ultraButton6.Click += new System.EventHandler(ultraButton6_Click);
		appearance10.Image = 0;
		this.ultraButton3.Appearance = appearance10;
		this.ultraButton3.BackColor = System.Drawing.Color.Transparent;
		this.ultraButton3.ButtonStyle = Infragistics.Win.UIElementButtonStyle.PopupBorderless;
		this.ultraButton3.ImageList = this.imageList1;
		this.ultraButton3.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton3.Location = new System.Drawing.Point(657, 55);
		this.ultraButton3.Name = "ultraButton3";
		this.ultraButton3.ShowFocusRect = false;
		this.ultraButton3.ShowOutline = false;
		this.ultraButton3.Size = new System.Drawing.Size(24, 24);
		this.ultraButton3.TabIndex = 10;
		this.ultraButton3.Click += new System.EventHandler(ultraButton3_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
		this.imageList1.Images.SetKeyName(0, "");
		appearance11.Image = resources.GetObject("appearance11.Image");
		this.ultraButton2.Appearance = appearance11;
		this.ultraButton2.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ultraButton2.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton2.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton2.Location = new System.Drawing.Point(591, 411);
		this.ultraButton2.Name = "ultraButton2";
		this.ultraButton2.ShowFocusRect = false;
		this.ultraButton2.ShowOutline = false;
		this.ultraButton2.Size = new System.Drawing.Size(88, 31);
		this.ultraButton2.SupportThemes = false;
		this.ultraButton2.TabIndex = 9;
		this.ultraButton2.Text = "取消";
		this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel2.Controls.Add(this.c1FlexGrid1);
		this.panel2.Controls.Add(this.panel3);
		this.panel2.Location = new System.Drawing.Point(20, 84);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(660, 324);
		this.panel2.TabIndex = 8;
		this.c1FlexGrid1.AllowEditing = false;
		this.c1FlexGrid1.BackColor = System.Drawing.Color.White;
		this.c1FlexGrid1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
		this.c1FlexGrid1.ColumnInfo = resources.GetString("c1FlexGrid1.ColumnInfo");
		this.c1FlexGrid1.Cursor = System.Windows.Forms.Cursors.Hand;
		this.c1FlexGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.c1FlexGrid1.ExtendLastCol = true;
		this.c1FlexGrid1.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
		this.c1FlexGrid1.ForeColor = System.Drawing.SystemColors.WindowText;
		this.c1FlexGrid1.Location = new System.Drawing.Point(0, 36);
		this.c1FlexGrid1.Name = "c1FlexGrid1";
		this.c1FlexGrid1.Rows.Count = 0;
		this.c1FlexGrid1.Rows.Fixed = 0;
		this.c1FlexGrid1.Rows.MinSize = 25;
		this.c1FlexGrid1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.c1FlexGrid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
		this.c1FlexGrid1.Size = new System.Drawing.Size(658, 286);
		this.c1FlexGrid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("c1FlexGrid1.Styles"));
		this.c1FlexGrid1.TabIndex = 8;
		this.c1FlexGrid1.MouseLeave += new System.EventHandler(c1FlexGrid1_MouseLeave);
		this.c1FlexGrid1.MouseDown += new System.Windows.Forms.MouseEventHandler(c1FlexGrid1_MouseDown);
		this.c1FlexGrid1.MouseMove += new System.Windows.Forms.MouseEventHandler(c1FlexGrid1_MouseMove);
		this.c1FlexGrid1.MouseEnter += new System.EventHandler(c1FlexGrid1_MouseEnter);
		this.panel3.Controls.Add(this.ultraLabel5);
		this.panel3.Controls.Add(this.ultraLabel4);
		this.panel3.Controls.Add(this.ultraLabel6);
		this.panel3.Controls.Add(this.ultraLabel3);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel3.Location = new System.Drawing.Point(0, 0);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(658, 36);
		this.panel3.TabIndex = 7;
		appearance12.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		appearance12.BackColor2 = System.Drawing.Color.FromArgb(225, 247, 223);
		appearance12.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance12.FontData.Name = "細明體";
		appearance12.FontData.SizeInPoints = 11f;
		appearance12.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance12.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel5.Appearance = appearance12;
		this.ultraLabel5.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ultraLabel5.Location = new System.Drawing.Point(452, 0);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(206, 36);
		this.ultraLabel5.TabIndex = 2;
		this.ultraLabel5.Text = "工程地址";
		appearance13.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		appearance13.BackColor2 = System.Drawing.Color.FromArgb(225, 247, 223);
		appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance13.FontData.Name = "細明體";
		appearance13.FontData.SizeInPoints = 11f;
		appearance13.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance13.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel4.Appearance = appearance13;
		this.ultraLabel4.Dock = System.Windows.Forms.DockStyle.Left;
		this.ultraLabel4.Location = new System.Drawing.Point(132, 0);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(320, 36);
		this.ultraLabel4.TabIndex = 1;
		this.ultraLabel4.Text = "工程名稱";
		appearance14.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		appearance14.BackColor2 = System.Drawing.Color.FromArgb(225, 247, 223);
		appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance14.FontData.Name = "細明體";
		appearance14.FontData.SizeInPoints = 11f;
		appearance14.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance14.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel6.Appearance = appearance14;
		this.ultraLabel6.Dock = System.Windows.Forms.DockStyle.Left;
		this.ultraLabel6.Location = new System.Drawing.Point(28, 0);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(104, 36);
		this.ultraLabel6.TabIndex = 3;
		this.ultraLabel6.Text = "工項代碼";
		appearance15.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		appearance15.BackColor2 = System.Drawing.Color.FromArgb(225, 247, 223);
		appearance15.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance15.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance15.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel3.Appearance = appearance15;
		this.ultraLabel3.Dock = System.Windows.Forms.DockStyle.Left;
		this.ultraLabel3.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(28, 36);
		this.ultraLabel3.TabIndex = 0;
		appearance16.FontData.SizeInPoints = 11f;
		this.cbFind.Appearance = appearance16;
		this.cbFind.AutoSize = true;
		this.cbFind.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
		appearance17.BackColor = System.Drawing.Color.FromArgb(153, 204, 255);
		this.cbFind.ButtonAppearance = appearance17;
		this.cbFind.ButtonStyle = Infragistics.Win.UIElementButtonStyle.PopupBorderless;
		this.cbFind.Location = new System.Drawing.Point(520, 57);
		this.cbFind.Name = "cbFind";
		this.cbFind.Size = new System.Drawing.Size(137, 20);
		this.cbFind.TabIndex = 7;
		this.cbFind.Text = null;
		this.cbFind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(cbFind_KeyPress);
		appearance18.Image = 0;
		this.ultraButton1.Appearance = appearance18;
		this.ultraButton1.BackColor = System.Drawing.Color.Transparent;
		this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.PopupBorderless;
		this.ultraButton1.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton1.Location = new System.Drawing.Point(656, 55);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.ShowFocusRect = false;
		this.ultraButton1.ShowOutline = false;
		this.ultraButton1.Size = new System.Drawing.Size(24, 24);
		this.ultraButton1.TabIndex = 3;
		this.ultraLabel2.Location = new System.Drawing.Point(484, 61);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(40, 20);
		this.ultraLabel2.TabIndex = 1;
		this.ultraLabel2.Text = "尋找:";
		appearance19.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance19.BackColor2 = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance19.FontData.Name = "新細明體";
		appearance19.FontData.SizeInPoints = 12f;
		appearance19.ForeColor = System.Drawing.Color.White;
		appearance19.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance19.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel1.Appearance = appearance19;
		this.ultraLabel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.ultraLabel1.Font = new System.Drawing.Font("細明體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.ultraLabel1.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(696, 48);
		this.ultraLabel1.TabIndex = 0;
		this.ultraLabel1.Text = "專案挑選";
		this.Tab_B.Controls.Add(this.c1FlexGrid2);
		this.Tab_B.Controls.Add(this.panel5);
		this.Tab_B.Controls.Add(this.panel4);
		this.Tab_B.Location = new System.Drawing.Point(0, 0);
		this.Tab_B.Name = "Tab_B";
		this.Tab_B.Size = new System.Drawing.Size(698, 447);
		this.c1FlexGrid2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.c1FlexGrid2.ColumnInfo = resources.GetString("c1FlexGrid2.ColumnInfo");
		this.ultraToolbarsManager1.SetContextMenuUltra(this.c1FlexGrid2, "PopMenu");
		this.c1FlexGrid2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.c1FlexGrid2.ExtendLastCol = true;
		this.c1FlexGrid2.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.c1FlexGrid2.ForeColor = System.Drawing.Color.Black;
		this.c1FlexGrid2.Location = new System.Drawing.Point(0, 50);
		this.c1FlexGrid2.Name = "c1FlexGrid2";
		this.c1FlexGrid2.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.c1FlexGrid2.Size = new System.Drawing.Size(698, 361);
		this.c1FlexGrid2.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("c1FlexGrid2.Styles"));
		this.c1FlexGrid2.TabIndex = 4;
		this.c1FlexGrid2.Tree.Column = 1;
		this.c1FlexGrid2.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.SimpleLeaf;
		this.c1FlexGrid2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(c1FlexGrid2_KeyPress);
		this.c1FlexGrid2.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(c1FlexGrid2_AfterEdit);
		this.panel5.Controls.Add(this.lblProject);
		this.panel5.Controls.Add(this.ultraButton5);
		this.panel5.Controls.Add(this.ultraButton4);
		this.panel5.Controls.Add(this.BtnPick);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel5.Location = new System.Drawing.Point(0, 411);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(698, 36);
		this.panel5.TabIndex = 3;
		appearance20.FontData.Name = "細明體";
		appearance20.FontData.SizeInPoints = 9f;
		appearance20.ForeColor = System.Drawing.Color.Blue;
		this.lblProject.Appearance = appearance20;
		this.lblProject.Location = new System.Drawing.Point(4, 7);
		this.lblProject.Name = "lblProject";
		this.lblProject.Size = new System.Drawing.Size(376, 23);
		this.lblProject.TabIndex = 13;
		this.lblProject.Text = "挑選的專案:";
		this.ultraButton5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance21.Image = resources.GetObject("appearance21.Image");
		appearance21.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton5.Appearance = appearance21;
		this.ultraButton5.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton5.HotTracking = true;
		this.ultraButton5.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton5.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton5.Location = new System.Drawing.Point(388, 3);
		this.ultraButton5.Name = "ultraButton5";
		this.ultraButton5.ShowFocusRect = false;
		this.ultraButton5.ShowOutline = false;
		this.ultraButton5.Size = new System.Drawing.Size(128, 31);
		this.ultraButton5.SupportThemes = false;
		this.ultraButton5.TabIndex = 11;
		this.ultraButton5.Text = "重新選擇專案";
		this.ultraButton5.Click += new System.EventHandler(ultraButton5_Click);
		this.ultraButton4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance22.Image = resources.GetObject("appearance22.Image");
		appearance22.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton4.Appearance = appearance22;
		this.ultraButton4.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton4.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ultraButton4.HotTracking = true;
		this.ultraButton4.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton4.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton4.Location = new System.Drawing.Point(607, 3);
		this.ultraButton4.Name = "ultraButton4";
		this.ultraButton4.ShowFocusRect = false;
		this.ultraButton4.ShowOutline = false;
		this.ultraButton4.Size = new System.Drawing.Size(88, 31);
		this.ultraButton4.SupportThemes = false;
		this.ultraButton4.TabIndex = 10;
		this.ultraButton4.Text = "取消";
		this.BtnPick.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance23.Image = resources.GetObject("appearance23.Image");
		appearance23.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnPick.Appearance = appearance23;
		this.BtnPick.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnPick.HotTracking = true;
		this.BtnPick.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnPick.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnPick.Location = new System.Drawing.Point(517, 3);
		this.BtnPick.Name = "BtnPick";
		this.BtnPick.ShowFocusRect = false;
		this.BtnPick.ShowOutline = false;
		this.BtnPick.Size = new System.Drawing.Size(88, 31);
		this.BtnPick.SupportThemes = false;
		this.BtnPick.TabIndex = 9;
		this.BtnPick.Text = "確定";
		this.BtnPick.Click += new System.EventHandler(BtnPick_Click);
		this.panel4.Controls.Add(this.levelSwitchButton);
		this.panel4.Controls.Add(this.ultraLabel7);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel4.Location = new System.Drawing.Point(0, 0);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(698, 50);
		this.panel4.TabIndex = 1;
		this.ultraLabel7.Location = new System.Drawing.Point(8, 7);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(184, 16);
		this.ultraLabel7.TabIndex = 0;
		this.ultraLabel7.Text = "勾選擇要加入的項目";
		this.Tabs_Ctrl.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.Tabs_Ctrl.Controls.Add(this.ultraTabSharedControlsPage1);
		this.Tabs_Ctrl.Controls.Add(this.Tab_A);
		this.Tabs_Ctrl.Controls.Add(this.Tab_B);
		this.Tabs_Ctrl.Controls.Add(this.Tab_1);
		this.Tabs_Ctrl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Tabs_Ctrl.Location = new System.Drawing.Point(0, 0);
		this.Tabs_Ctrl.Name = "Tabs_Ctrl";
		this.Tabs_Ctrl.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.Tabs_Ctrl.Size = new System.Drawing.Size(698, 447);
		this.Tabs_Ctrl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
		this.Tabs_Ctrl.TabIndex = 0;
		ultraTab1.TabPage = this.Tab_1;
		ultraTab1.Text = "tab3";
		ultraTab2.TabPage = this.Tab_A;
		ultraTab2.Text = "tab1";
		ultraTab3.TabPage = this.Tab_B;
		ultraTab3.Text = "tab2";
		this.Tabs_Ctrl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[3] { ultraTab1, ultraTab2, ultraTab3 });
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(698, 447);
		this.toolTip1.AutoPopDelay = 6000;
		this.toolTip1.InitialDelay = 500;
		this.toolTip1.ReshowDelay = 100;
		this.ultraToolbarsManager1.DockWithinContainer = this;
		this.ultraToolbarsManager1.ShowFullMenusDelay = 500;
		ultraToolbar1.DockedColumn = 0;
		ultraToolbar1.DockedRow = 0;
		ultraToolbar1.Text = "Popup1";
		ultraToolbar1.Visible = false;
		this.ultraToolbarsManager1.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[1] { ultraToolbar1 });
		buttonTool1.SharedProps.Caption = "選取全部工項";
		buttonTool2.SharedProps.Caption = "取消所有勾選項目";
		buttonTool3.SharedProps.Caption = "反像選取";
		popupMenuTool1.SharedProps.Caption = "右鍵選單";
		buttonTool6.InstanceProps.IsFirstInGroup = true;
		popupMenuTool1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[3] { buttonTool4, buttonTool5, buttonTool6 });
		this.ultraToolbarsManager1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[4] { buttonTool1, buttonTool2, buttonTool3, popupMenuTool1 });
		this.ultraToolbarsManager1.BeforeToolbarListDropdown += new Infragistics.Win.UltraWinToolbars.BeforeToolbarListDropdownEventHandler(ultraToolbarsManager1_BeforeToolbarListDropdown);
		this.ultraToolbarsManager1.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(ultraToolbarsManager1_ToolClick);
		this._FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Left.BackColor = System.Drawing.SystemColors.Control;
		this._FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
		this._FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 0);
		this._FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Left.Name = "_FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Left";
		this._FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 447);
		this._FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Right.BackColor = System.Drawing.SystemColors.Control;
		this._FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
		this._FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(698, 0);
		this._FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Right.Name = "_FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Right";
		this._FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 447);
		this._FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Right.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Top.BackColor = System.Drawing.SystemColors.Control;
		this._FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
		this._FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
		this._FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Top.Name = "_FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Top";
		this._FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(698, 0);
		this._FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Top.ToolbarsManager = this.ultraToolbarsManager1;
		this._FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this._FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.SystemColors.Control;
		this._FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
		this._FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
		this._FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 447);
		this._FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Bottom.Name = "_FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Bottom";
		this._FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(698, 0);
		this._FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ultraToolbarsManager1;
		this.imageList3.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList3.ImageStream");
		this.imageList3.TransparentColor = System.Drawing.Color.Magenta;
		this.imageList3.Images.SetKeyName(0, "");
		this.imageList3.Images.SetKeyName(1, "");
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.Magenta;
		this.imageList2.Images.SetKeyName(0, "");
		this.imageList2.Images.SetKeyName(1, "");
		this.levelSwitchButton.Location = new System.Drawing.Point(8, 27);
		this.levelSwitchButton.Name = "levelSwitchButton1";
		this.levelSwitchButton.Size = new System.Drawing.Size(166, 22);
		this.levelSwitchButton.TabIndex = 1;
		this.levelSwitchButton.LevelSwitchButtonsClicked += new Archnowledge.Pcces.PccesMain.ArchControls.LevelSwitchButton.LevelSwitchButtonClickHandler(levelSwitchButton_LevelSwitchButtonsClicked);
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		base.CancelButton = this.A_Btn_Cncl;
		base.ClientSize = new System.Drawing.Size(698, 447);
		base.Controls.Add(this.Tabs_Ctrl);
		base.Controls.Add(this._FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Left);
		base.Controls.Add(this._FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Right);
		base.Controls.Add(this._FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Top);
		base.Controls.Add(this._FormPickProjWkItem_Wzd_Toolbars_Dock_Area_Bottom);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.MinimizeBox = false;
		base.Name = "FormPickProjWkItem_Wzd";
		base.ShowIcon = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "自專案挑選工項";
		base.Load += new System.EventHandler(FormPickProjWkItem_Wzd_Load);
		base.FormClosed += new System.Windows.Forms.FormClosedEventHandler(FormPickProjWkItem_Wzd_FormClosed);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormPickProjWkItem_Wzd_FormClosing);
		this.Tab_1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.GridUnit1).EndInit();
		this.panel8.ResumeLayout(false);
		this.panel7.ResumeLayout(false);
		this.Tab_A.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid1).EndInit();
		this.panel3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.cbFind).EndInit();
		this.Tab_B.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid2).EndInit();
		this.panel5.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Tabs_Ctrl).EndInit();
		this.Tabs_Ctrl.ResumeLayout(false);
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
