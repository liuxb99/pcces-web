using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.PccesMain.MrsBase;
using Archnowledge.Pcces.STDClass;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTabControl;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormBudgetPickProjRes : Form
{
	private string F_CurrentDBName = "";

	private string F_CurrentSelectDBName = "";

	private string F_CurrentSelectDBDesc = "";

	private string F_KeyWord = "";

	private string F_UserID;

	private int F_MainQty = 0;

	private int F_MainCst = 0;

	private int F_MainAmt = 0;

	private int F_AnaQty = 0;

	private int F_AnaCst = 0;

	private int F_AnaAmt = 0;

	private int GridCols = 0;

	private object[,] GridColsSquence;

	private string F_ProjectCode = "";

	private DataTable DT1 = new DataTable();

	private DataTable DT_FPick = new DataTable();

	private PccesFormAction F_ActionName;

	private string CompanyDBName = string.Empty;

	private IContainer components;

	private UltraTabControl Tab_Ctrl;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl Tab_A;

	private UltraTabPageControl Tab_B;

	private UltraLabel ultraLabel1;

	private Panel panel4;

	private ImageList imageList1;

	private UltraButton ultraButton2;

	private UltraComboEditor cbFind;

	private UltraLabel ultraLabel2;

	private Panel panel5;

	private C1FlexGrid c1FlexGrid1;

	private Panel panel6;

	private UltraLabel ultraLabel5;

	private UltraLabel ultraLabel4;

	private UltraLabel ultraLabel6;

	private UltraLabel ultraLabel3;

	private Panel panel1;

	private UltraLabel lblProject;

	private Panel panel3;

	private GridBudget c1FlexGrid2;

	private ImageList imageList2;

	private UltraTabPageControl Tab_1;

	private Panel panel7;

	private UltraLabel ultraLabel7;

	private UltraLabel ultraLabel8;

	private Panel panel2;

	private UltraButton ultraButton1;

	private UltraButton ultraButton3;

	private Panel panel8;

	private GroupBox groupBox2;

	private UltraButton A_Btn_Cncl;

	private UltraButton A_Btn_Next;

	private Panel panel9;

	private GroupBox groupBox1;

	private UltraButton ultraButton4;

	private UltraButton ultraButton6;

	private UltraButton BtnGoHomeB;

	private UltraButton ultraButton7;

	private ImageList imageList3;

	public GridMrsBase GridUnit1;

	private UltraLabel lblDBName;

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

	public FormBudgetPickProjRes()
	{
		InitializeComponent();
		GridCols = c1FlexGrid2.Cols.Count;
		GridColsSquence = new object[GridCols, 8];
		CellStyle cs = c1FlexGrid2.Styles.Add("img");
		cs.DataType = typeof(Image);
		RememberColsProps();
	}

	private void SettingDecimal()
	{
		DataTable DTDecimal = new DataTable();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
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
	}

	private void FormBudgetPickProjRes_Load(object sender, EventArgs e)
	{
		Tab_Ctrl.ActiveTab = Tab_1.Tab;
		SysUser oSysUser = new SysUser();
		F_CurrentDBName = oSysUser.GetSysUserDatabaseName(F_UserID);
		SettingDecimal();
		if (F_CurrentDBName == CompanyDBName)
		{
			F_CurrentSelectDBName = F_CurrentDBName;
			LoadProjectData();
			lblDBName.Text = "目前資料庫：" + oSysUser.GetSysUserDatabaseDesc(F_UserID);
			ultraButton6.Visible = false;
			BtnGoHomeB.Visible = false;
			Tab_A.Tab.Selected = true;
		}
		else
		{
			LoadDBData();
		}
		LoadingScreen();
	}

	private void LoadingScreen()
	{
		string Status = CommonMethods.GetIniValue("PickProjRes", "WindowState");
		if (Status == "Maximized")
		{
			base.WindowState = FormWindowState.Maximized;
		}
		int iLoc_X = PubTools.Str2Int(CommonMethods.GetIniValue("PickProjRes", "PK_LocationX"));
		int iLoc_Y = PubTools.Str2Int(CommonMethods.GetIniValue("PickProjRes", "PK_LocationY"));
		int iSiz_W = PubTools.Str2Int(CommonMethods.GetIniValue("PickProjRes", "PK_Width"));
		int iSiz_H = PubTools.Str2Int(CommonMethods.GetIniValue("PickProjRes", "PK_Height"));
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

	private void LoadDBData()
	{
		GeneralManager oManager = new GeneralManager();
		DataSet dsSysPccesSlave;
		ExecResult ER = oManager.GetSysPccesSlave(F_UserID, out dsSysPccesSlave);
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
		CellStyle CSDatabaseName = GridUnit1.Styles.Add("MainColor");
		CSDatabaseName.ForeColor = Color.Blue;
		CSDatabaseName.Font = new Font(GridUnit1.Font, FontStyle.Bold);
		CellStyle CSError = GridUnit1.Styles.Add("ErrorColor");
		CSError.BackColor = Color.Tomato;
		GridUnit1.Rows.Count = 1;
		GridUnit1.Redraw = false;
		foreach (DataRow theRow in dtSysPccesSlave.Rows)
		{
			Row GridRow = GridUnit1.Rows.Add();
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
	}

	private void BindDataIntoGrid()
	{
		Archnowledge.Pcces.DomainModule.General.PubProject oPubProject = new Archnowledge.Pcces.DomainModule.General.PubProject();
		DataSet dsPubProject = oPubProject.GetPubProject(F_UserID, F_CurrentSelectDBName);
		c1FlexGrid1.Cols["projCName"].Style.WordWrap = true;
		c1FlexGrid1.Rows.Count = 0;
		GridUnit1.Redraw = false;
		foreach (DataRow theRow in dsPubProject.Tables[0].Rows)
		{
			string projectCode = theRow["projectCode"].ToString().Trim();
			if (!(projectCode == F_ProjectCode.Trim()) || !(F_CurrentSelectDBName == F_CurrentDBName))
			{
				Row GridRow = c1FlexGrid1.Rows.Add();
				if (theRow["bud"] != DBNull.Value && theRow["bud"].ToString().Trim() != "")
				{
					CellRange rg = c1FlexGrid1.GetCellRange(GridRow.Index, c1FlexGrid1.Cols["IsData"].SafeIndex);
					rg.Style = c1FlexGrid1.Styles["img"];
					rg.Image = imageList2.Images[0];
				}
				GridRow["ProjectCode"] = projectCode;
				GridRow["projCName"] = theRow["projCName"].ToString().Trim();
				GridRow["projEName"] = theRow["projEName"].ToString().Trim();
				GridRow["projAddress"] = theRow["projAddress"].ToString().Trim();
			}
		}
		GridUnit1.Redraw = true;
	}

	private void ultraButton2_Click(object sender, EventArgs e)
	{
		if (cbFind.Text == null || c1FlexGrid1.Rows.Count <= 1)
		{
			return;
		}
		int iStart = c1FlexGrid1.Row + 1;
		string sSearchText = cbFind.Text.Trim();
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
			iStart = c1FlexGrid1.Row + 1;
		}
		if (sSearchText.Trim() == "")
		{
			return;
		}
		for (int i = iStart; i < c1FlexGrid1.Rows.Count; i++)
		{
			for (int j = 1; j < c1FlexGrid1.Cols.Count; j++)
			{
				if (c1FlexGrid1[i, j] == null || c1FlexGrid1[i, j].ToString().ToUpper().IndexOf(sSearchText.ToUpper()) <= -1)
				{
					continue;
				}
				c1FlexGrid1.Row = i;
				c1FlexGrid1.Select();
				int iFondCount = 0;
				int iListCount = cbFind.Items.Count;
				for (int k = 0; k < iListCount; k++)
				{
					if (cbFind.Items[k].DisplayText.Trim() == sSearchText.Trim())
					{
						iFondCount++;
					}
				}
				if (iFondCount == 0)
				{
					cbFind.Items.Add(sSearchText, sSearchText);
				}
				return;
			}
		}
	}

	private void cbFind_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			ultraButton2_Click(sender, e);
		}
	}

	private void c1FlexGrid1_MouseDown(object sender, MouseEventArgs e)
	{
		int rowIndex = c1FlexGrid1.MouseRow;
		if (rowIndex > -1)
		{
			string sProjectCode = c1FlexGrid1[c1FlexGrid1.Row, "ProjectCode"].ToString().Trim();
			string sProjectName = c1FlexGrid1[c1FlexGrid1.Row, "projCName"].ToString().Trim();
			lblProject.Text = "挑選的專案:【" + sProjectCode + "】" + sProjectName;
			DBClass DBCLS = new DBClass();
			DBCLS._FS_UserID = F_UserID;
			if (!DBCLS.GetProjectAuthority(F_UserID, sProjectCode))
			{
				MessageBox.Show(this, "這個專案您沒有權限，無法開啟。", "專案權限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			Tab_B.Tab.Selected = true;
			base.FormBorderStyle = FormBorderStyle.Sizable;
			base.MaximizeBox = true;
			base.MinimizeBox = false;
			ArrayList aArr = new ArrayList();
			aArr.Clear();
			aArr.Add(F_UserID);
			aArr.Add("WinFORM 基本工料");
			MrsBaseA dbMrsBase = new MrsBaseA(F_UserID, aArr);
			dbMrsBase.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
			dbMrsBase.ps_projectcode = sProjectCode;
			DT_FPick = dbMrsBase.ListItem();
			BindDataToGrid();
		}
		ultraButton1.Visible = true;
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
			GridColsSquence[i, 7] = c1FlexGrid2.Cols[i].TextAlign;
		}
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
		c1FlexGrid2.Redraw = false;
		RememberColsProps();
		DataView DV1 = DT_FPick.DefaultView;
		DV1.Sort = " pccesCode ASC ";
		CellStyle CS1 = c1FlexGrid2.Styles.Add("AnalysisColor");
		CellStyle CS2 = c1FlexGrid2.Styles.Add("LEMColor");
		CellStyle CS3 = c1FlexGrid2.Styles.Add("WColor");
		CS1.ForeColor = Color.Red;
		CS2.ForeColor = Color.Teal;
		CS3.ForeColor = Color.Purple;
		c1FlexGrid2.Clear(ClearFlags.All);
		c1FlexGrid2.Select(0, 0);
		c1FlexGrid2.Rows.Count = DV1.Count + 1;
		SetGridColumn();
		string sItemClass = "";
		for (int i = 0; i < DV1.Count; i++)
		{
			sItemClass = DV1[i]["pccesCode"].ToString().Substring(0, 1);
			c1FlexGrid2[i + 1, "PccesCode"] = DV1[i]["pccesCode"].ToString().Trim();
			if (sItemClass == "L" || sItemClass == "E" || sItemClass == "M")
			{
				c1FlexGrid2.Rows[i + 1].Style = c1FlexGrid2.Styles["LEMColor"];
			}
			else if (sItemClass == "W")
			{
				c1FlexGrid2.Rows[i + 1].Style = c1FlexGrid2.Styles["WColor"];
			}
			c1FlexGrid2[i + 1, "CName"] = DV1[i]["cName"].ToString();
			if (DV1[i]["analysis"].ToString().Trim() == "1")
			{
				c1FlexGrid2[i + 1, "Analysis"] = true;
				c1FlexGrid2.Rows[i + 1].Style = c1FlexGrid2.Styles["AnalysisColor"];
				CellRange rg = c1FlexGrid2.GetCellRange(i + 1, c1FlexGrid2.Cols["AnaImg"].SafeIndex);
				rg.Style = c1FlexGrid2.Styles["img"];
				rg.Image = imageList2.Images[0];
			}
			else
			{
				c1FlexGrid2[i + 1, "Analysis"] = false;
			}
			c1FlexGrid2[i + 1, "IsCheck"] = false;
			c1FlexGrid2[i + 1, "resCode"] = DV1[i]["resCode"];
			c1FlexGrid2[i + 1, "PubCode"] = DV1[i]["pubCode"];
			c1FlexGrid2[i + 1, "eName"] = DV1[i]["eName"];
			c1FlexGrid2[i + 1, "Memo"] = DV1[i]["memo"];
			c1FlexGrid2[i + 1, "UnitName"] = DV1[i]["unitName"];
			c1FlexGrid2[i + 1, "resType"] = DV1[i]["resType"];
			c1FlexGrid2[i + 1, "LRate"] = DV1[i]["lRate"];
			c1FlexGrid2[i + 1, "ERate"] = DV1[i]["eRate"];
			c1FlexGrid2[i + 1, "MRate"] = DV1[i]["mRate"];
			c1FlexGrid2[i + 1, "WRate"] = DV1[i]["wRate"];
			c1FlexGrid2[i + 1, "Cost"] = DV1[i]["cost"];
			c1FlexGrid2[i + 1, "AnalysisQty"] = DV1[i]["analysisQty"];
			c1FlexGrid2[i + 1, "Rate"] = DV1[i]["rate"];
			c1FlexGrid2[i + 1, "CostKind"] = DV1[i]["costKind"];
			c1FlexGrid2[i + 1, "XNameC"] = DV1[i]["xNameC"];
			c1FlexGrid2[i + 1, "XNameE"] = DV1[i]["xNameE"];
			c1FlexGrid2[i + 1, "eUnit"] = DV1[i]["eUnit"];
			c1FlexGrid2[i + 1, "extendCode"] = DV1[i]["extendCode"];
			c1FlexGrid2[i + 1, "State"] = DV1[i]["state"];
			c1FlexGrid2[i + 1, "usrQty"] = DV1[i]["usrQty"];
			c1FlexGrid2[i + 1, "usrAmt"] = DV1[i]["usrAmt"];
			c1FlexGrid2[i + 1, "Show"] = "";
			c1FlexGrid2[i + 1, "Post"] = "";
		}
		c1FlexGrid2.Redraw = true;
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

	private void c1FlexGrid1_MouseMove(object sender, MouseEventArgs e)
	{
		c1FlexGrid1.Row = c1FlexGrid1.MouseRow;
	}

	private void ultraButton1_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.OK;
		DataSet DS1 = new DataSet("tempDS");
		DataTable DT1 = new DataTable("tempTable");
		for (int i = 1; i < c1FlexGrid2.Cols.Count; i++)
		{
			DataColumn DC = new DataColumn(c1FlexGrid2.Cols[i].Name, c1FlexGrid2.Cols[i].DataType);
			DT1.Columns.Add(DC);
		}
		for (int i = 1; i < c1FlexGrid2.Rows.Count; i++)
		{
			if (!(bool)c1FlexGrid2[i, "IsCheck"])
			{
				continue;
			}
			DataRow DR = DT1.NewRow();
			for (int j = 0; j < DT1.Columns.Count; j++)
			{
				if ((object)c1FlexGrid2.Cols[DT1.Columns[j].ColumnName].DataType != Type.GetType("System.String") && c1FlexGrid2[i, DT1.Columns[j].ColumnName] == null)
				{
					DR[c1FlexGrid2.Cols[DT1.Columns[j].ColumnName].Name] = 0;
				}
				else
				{
					DR[c1FlexGrid2.Cols[DT1.Columns[j].ColumnName].Name] = c1FlexGrid2[i, DT1.Columns[j].ColumnName];
				}
			}
			DT1.Rows.Add(DR);
		}
		DS1.Tables.Add(DT1);
		(base.Owner as FormMrsBaseBreakdown).Th_MenuPaste(DS1, c1FlexGrid1[c1FlexGrid1.Row, "ProjectCode"].ToString().Trim());
	}

	private void A_Btn_Next_Click(object sender, EventArgs e)
	{
		GridUnit1_Click(sender, e);
	}

	private void GridUnit1_MouseMove(object sender, MouseEventArgs e)
	{
		int rowIndex = GridUnit1.MouseRow;
		int colIndex = GridUnit1.MouseCol;
		if (rowIndex > 0 && colIndex > 0)
		{
			GridUnit1.Row = rowIndex;
			GridUnit1.Select();
		}
	}

	private void GridUnit1_Click(object sender, EventArgs e)
	{
		if (GridUnit1.MouseRow > 0 && GridUnit1.MouseCol > 0)
		{
			int rowIndex = GridUnit1.MouseRow;
			F_CurrentSelectDBDesc = GridUnit1[rowIndex, "dbDesc"].ToString();
			F_CurrentSelectDBName = GridUnit1[rowIndex, "dbName"].ToString();
			SysUser oSysUser = new SysUser();
			oSysUser.SetSysUserDatabaseName(F_UserID, F_CurrentSelectDBName);
			if (CheckDBVer())
			{
				ChgStru stdll = new ChgStru();
				stdll.F_UserID = F_UserID;
				stdll.ModifyDatabaseStructure(F_CurrentSelectDBName);
			}
			LoadProjectData();
			lblDBName.Text = "挑選的資料庫:" + F_CurrentSelectDBDesc;
			Tab_A.Tab.Selected = true;
		}
	}

	private bool CheckDBVer()
	{
		bool flag = false;
		string sBuild = PccesVersion.PccesAssemblyVersion;
		string DBVer = PccesVersion.GetDatabaseVersion(F_UserID);
		if (!DBVer.Equals(sBuild))
		{
			flag = true;
		}
		return flag;
	}

	private void LoadProjectData()
	{
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add(CommonMethods.GetFormTypeTitle(FormType.Budget));
		Archnowledge.Pcces.BUDClass.PubProject dbProject = new Archnowledge.Pcces.BUDClass.PubProject(aArr);
		DT1 = dbProject.ListItem("");
		BindDataIntoGrid();
	}

	private void FormBudgetPickProjRes_FormClosing(object sender, FormClosingEventArgs e)
	{
		SysUser oSysUser = new SysUser();
		oSysUser.SetSysUserDatabaseName(F_UserID, F_CurrentDBName);
		if (base.WindowState != FormWindowState.Maximized)
		{
			CommonMethods.WriteIniValue("PickProjRes", "PK_LocationX", base.Location.X.ToString());
			CommonMethods.WriteIniValue("PickProjRes", "PK_LocationY", base.Location.Y.ToString());
			CommonMethods.WriteIniValue("PickProjRes", "PK_Width", base.Size.Width.ToString());
			CommonMethods.WriteIniValue("PickProjRes", "PK_Height", base.Size.Height.ToString());
		}
		CommonMethods.WriteIniValue("PickProjRes", "WindowState", base.WindowState.ToString());
	}

	private void ultraButton6_Click(object sender, EventArgs e)
	{
		Tab_1.Tab.Selected = true;
	}

	private void ultraButton1_Click_1(object sender, EventArgs e)
	{
		Cursor = Cursors.WaitCursor;
		DataSet DS1 = new DataSet("tempDS");
		DataTable DT1 = new DataTable("tempTable");
		for (int i = 1; i < c1FlexGrid2.Cols.Count; i++)
		{
			DataColumn DC = new DataColumn(c1FlexGrid2.Cols[i].Name, c1FlexGrid2.Cols[i].DataType);
			DT1.Columns.Add(DC);
		}
		for (int i = 1; i < c1FlexGrid2.Rows.Count; i++)
		{
			if (!(bool)c1FlexGrid2[i, "IsCheck"])
			{
				continue;
			}
			DataRow DR = DT1.NewRow();
			for (int j = 0; j < DT1.Columns.Count; j++)
			{
				if ((object)c1FlexGrid2.Cols[DT1.Columns[j].ColumnName].DataType != Type.GetType("System.String") && c1FlexGrid2[i, DT1.Columns[j].ColumnName] == null)
				{
					DR[c1FlexGrid2.Cols[DT1.Columns[j].ColumnName].Name] = null;
				}
				else
				{
					DR[c1FlexGrid2.Cols[DT1.Columns[j].ColumnName].Name] = c1FlexGrid2[i, DT1.Columns[j].ColumnName];
				}
			}
			DT1.Rows.Add(DR);
		}
		DS1.Tables.Add(DT1);
		DataTable DT_PickTemp = new DataTable();
		DT_PickTemp.Columns.Add("ProjectCode", Type.GetType("System.String"));
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
		DT_PickTemp.Columns.Add("DBName", Type.GetType("System.String"));
		DT_PickTemp.Columns.Add("Kind", Type.GetType("System.String"));
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add("自專案挑選工項");
		ItemA ITMA = new ItemA(aArr);
		ITMA.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		ITMA.ps_projectCode = c1FlexGrid1[c1FlexGrid1.Row, "ProjectCode"].ToString().Trim();
		for (int i = 1; i < c1FlexGrid2.Rows.Count; i++)
		{
			if ((bool)c1FlexGrid2[i, "IsCheck"])
			{
				DataRow DR2 = DT_PickTemp.NewRow();
				DR2["ProjectCode"] = c1FlexGrid1[c1FlexGrid1.Row, "ProjectCode"].ToString().Trim();
				DR2["CName"] = c1FlexGrid2[i, "CName"].ToString().Trim();
				DR2["UnitName"] = c1FlexGrid2[i, "unitName"].ToString().Trim();
				DR2["cost"] = c1FlexGrid2[i, "Cost"].ToString().Trim();
				DR2["memo"] = c1FlexGrid2[i, "Memo"].ToString().Trim();
				DR2["EName"] = c1FlexGrid2[i, "EName"].ToString().Trim();
				DR2["eUnit"] = c1FlexGrid2[i, "EUnit"].ToString().Trim();
				DR2["PubCode"] = c1FlexGrid2[i, "PubCode"];
				DR2["PccesCode"] = c1FlexGrid2[i, "PccesCode"].ToString().Trim();
				DR2["DBName"] = F_CurrentSelectDBName;
				DR2["Kind"] = "W";
				DT_PickTemp.Rows.Add(DR2);
			}
		}
		SysUser oSysUser = new SysUser();
		oSysUser.SetSysUserDatabaseName(F_UserID, F_CurrentDBName);
		(base.Owner as FormMrsBaseBreakdown).Th_MenuPaste(DS1, DT_PickTemp, c1FlexGrid1[c1FlexGrid1.Row, "ProjectCode"].ToString().Trim());
		Cursor = Cursors.Default;
		base.DialogResult = DialogResult.OK;
	}

	private void ultraButton7_Click(object sender, EventArgs e)
	{
		Tab_A.Tab.Selected = true;
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormBudgetPickProjRes));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
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
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		this.Tab_1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.GridUnit1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.panel8 = new System.Windows.Forms.Panel();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.A_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.A_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.panel7 = new System.Windows.Forms.Panel();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel8 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_A = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.BtnGoHomeB = new Infragistics.Win.Misc.UltraButton();
		this.panel4 = new System.Windows.Forms.Panel();
		this.panel9 = new System.Windows.Forms.Panel();
		this.lblDBName = new Infragistics.Win.Misc.UltraLabel();
		this.ultraButton6 = new Infragistics.Win.Misc.UltraButton();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.ultraButton4 = new Infragistics.Win.Misc.UltraButton();
		this.panel5 = new System.Windows.Forms.Panel();
		this.c1FlexGrid1 = new C1.Win.C1FlexGrid.C1FlexGrid();
		this.panel6 = new System.Windows.Forms.Panel();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraButton2 = new Infragistics.Win.Misc.UltraButton();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.cbFind = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_B = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel3 = new System.Windows.Forms.Panel();
		this.c1FlexGrid2 = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.panel2 = new System.Windows.Forms.Panel();
		this.ultraButton7 = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton3 = new Infragistics.Win.Misc.UltraButton();
		this.panel1 = new System.Windows.Forms.Panel();
		this.lblProject = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_Ctrl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.imageList3 = new System.Windows.Forms.ImageList(this.components);
		this.Tab_1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.GridUnit1).BeginInit();
		this.panel8.SuspendLayout();
		this.panel7.SuspendLayout();
		this.Tab_A.SuspendLayout();
		this.panel4.SuspendLayout();
		this.panel9.SuspendLayout();
		this.panel5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid1).BeginInit();
		this.panel6.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cbFind).BeginInit();
		this.Tab_B.SuspendLayout();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid2).BeginInit();
		this.panel2.SuspendLayout();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).BeginInit();
		this.Tab_Ctrl.SuspendLayout();
		base.SuspendLayout();
		this.Tab_1.Controls.Add(this.GridUnit1);
		this.Tab_1.Controls.Add(this.panel8);
		this.Tab_1.Controls.Add(this.panel7);
		this.Tab_1.Location = new System.Drawing.Point(0, 0);
		this.Tab_1.Name = "Tab_1";
		this.Tab_1.Size = new System.Drawing.Size(692, 465);
		this.GridUnit1._ExcelFileName = "";
		this.GridUnit1._ExcelSheeName = "";
		this.GridUnit1._IsOpenExcelAfterExport = false;
		this.GridUnit1.AllowEditing = false;
		this.GridUnit1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
		this.GridUnit1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.GridUnit1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
		this.GridUnit1.ColumnInfo = "4,1,0,0,0,110,Columns:0{Width:17;Name:\"RowIndicator\";AllowDragging:False;AllowResizing:False;AllowEditing:False;DataType:System.Int32;TextAlign:RightTop;TextAlignFixed:GeneralTop;}\t1{Name:\"UsedDB\";Caption:\"使用中資料庫\";TextAlign:GeneralBottom;TextAlignFixed:GeneralTop;ImageAlign:CenterCenter;}\t2{Width:300;Name:\"dbDesc\";Caption:\"資料所屬機關\";AllowEditing:False;DataType:System.String;TextAlign:GeneralTop;TextAlignFixed:GeneralTop;}\t3{Width:90;Name:\"dbName\";Caption:\"資料庫別名\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t";
		this.GridUnit1.Cursor = System.Windows.Forms.Cursors.Hand;
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
		this.GridUnit1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
		this.GridUnit1.ShowCursor = true;
		this.GridUnit1.ShowToolTipOnNarrowColumn = true;
		this.GridUnit1.Size = new System.Drawing.Size(692, 373);
		this.GridUnit1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection("Normal{Font:細明體, 11.25pt;BackColor:237, 243, 254;ForeColor:Black;TextAlign:GeneralBottom;Border:Flat,1,Silver,Both;}\tAlternate{BackColor:White;}\tFixed{BackColor:225, 247, 223;TextAlign:GeneralTop;Border:Raised,1,Black,Both;}\tHighlight{BackColor:102, 153, 255;TextAlign:GeneralCenter;ImageAlign:CenterCenter;Border:None,1,Black,Both;}\tFocus{BackColor:102, 153, 255;Margins:0, 0, 0, 0;TextAlign:GeneralCenter;Border:None,1,Black,Both;}\tSearch{BackColor:Highlight;ForeColor:HighlightText;}\tFrozen{BackColor:Beige;}\tEmptyArea{BackColor:232, 232, 232;Border:Flat,1,ControlDarkDark,Both;}\tGrandTotal{BackColor:Black;ForeColor:White;}\tSubtotal0{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal1{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal2{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal3{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal4{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal5{BackColor:ControlDarkDark;ForeColor:White;}\t");
		this.GridUnit1.TabIndex = 21;
		this.GridUnit1.UndoMax = 10;
		this.GridUnit1.Click += new System.EventHandler(GridUnit1_Click);
		this.GridUnit1.MouseMove += new System.Windows.Forms.MouseEventHandler(GridUnit1_MouseMove);
		this.panel8.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel8.Controls.Add(this.groupBox2);
		this.panel8.Controls.Add(this.A_Btn_Cncl);
		this.panel8.Controls.Add(this.A_Btn_Next);
		this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel8.Location = new System.Drawing.Point(0, 421);
		this.panel8.Name = "panel8";
		this.panel8.Size = new System.Drawing.Size(692, 44);
		this.panel8.TabIndex = 18;
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(692, 8);
		this.groupBox2.TabIndex = 3;
		this.groupBox2.TabStop = false;
		this.A_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Cncl.Appearance = appearance1;
		this.A_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.A_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_Cncl.Location = new System.Drawing.Point(599, 10);
		this.A_Btn_Cncl.Name = "A_Btn_Cncl";
		this.A_Btn_Cncl.ShowFocusRect = false;
		this.A_Btn_Cncl.ShowOutline = false;
		this.A_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.A_Btn_Cncl.SupportThemes = false;
		this.A_Btn_Cncl.TabIndex = 2;
		this.A_Btn_Cncl.Text = "取消";
		this.A_Btn_Next.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Next.Appearance = appearance2;
		this.A_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_Next.Location = new System.Drawing.Point(507, 10);
		this.A_Btn_Next.Name = "A_Btn_Next";
		this.A_Btn_Next.ShowFocusRect = false;
		this.A_Btn_Next.ShowOutline = false;
		this.A_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.A_Btn_Next.SupportThemes = false;
		this.A_Btn_Next.TabIndex = 1;
		this.A_Btn_Next.Text = "下一步";
		this.A_Btn_Next.Visible = false;
		this.A_Btn_Next.Click += new System.EventHandler(A_Btn_Next_Click);
		this.panel7.BackColor = System.Drawing.Color.White;
		this.panel7.Controls.Add(this.ultraLabel7);
		this.panel7.Controls.Add(this.ultraLabel8);
		this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel7.Location = new System.Drawing.Point(0, 0);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(692, 48);
		this.panel7.TabIndex = 15;
		appearance3.BackColor = System.Drawing.Color.White;
		this.ultraLabel7.Appearance = appearance3;
		this.ultraLabel7.Location = new System.Drawing.Point(44, 27);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(620, 20);
		this.ultraLabel7.TabIndex = 3;
		this.ultraLabel7.Text = "請挑選要選用的資料庫來源(用滑鼠點選後會立即進入專案挑選)";
		appearance4.BackColor = System.Drawing.Color.White;
		this.ultraLabel8.Appearance = appearance4;
		this.ultraLabel8.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel8.Location = new System.Drawing.Point(12, 7);
		this.ultraLabel8.Name = "ultraLabel8";
		this.ultraLabel8.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel8.TabIndex = 2;
		this.ultraLabel8.Text = "工項來源";
		this.Tab_A.Controls.Add(this.BtnGoHomeB);
		this.Tab_A.Controls.Add(this.panel4);
		this.Tab_A.Controls.Add(this.ultraLabel1);
		this.Tab_A.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_A.Name = "Tab_A";
		this.Tab_A.Size = new System.Drawing.Size(692, 465);
		this.BtnGoHomeB.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance5.Cursor = System.Windows.Forms.Cursors.Arrow;
		appearance5.ForeColor = System.Drawing.Color.White;
		appearance5.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnGoHomeB.Appearance = appearance5;
		this.BtnGoHomeB.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.BtnGoHomeB.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Button;
		this.BtnGoHomeB.Font = new System.Drawing.Font("細明體", 9f);
		appearance6.Cursor = System.Windows.Forms.Cursors.Hand;
		appearance6.ForeColor = System.Drawing.Color.Yellow;
		this.BtnGoHomeB.HotTrackAppearance = appearance6;
		this.BtnGoHomeB.HotTracking = true;
		this.BtnGoHomeB.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnGoHomeB.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnGoHomeB.Location = new System.Drawing.Point(548, 20);
		this.BtnGoHomeB.Name = "BtnGoHomeB";
		this.BtnGoHomeB.ShowFocusRect = false;
		this.BtnGoHomeB.ShowOutline = false;
		this.BtnGoHomeB.Size = new System.Drawing.Size(140, 23);
		this.BtnGoHomeB.SupportThemes = false;
		this.BtnGoHomeB.TabIndex = 3;
		this.BtnGoHomeB.Text = "重新挑選「資料庫」";
		this.panel4.Controls.Add(this.panel9);
		this.panel4.Controls.Add(this.panel5);
		this.panel4.Controls.Add(this.ultraButton2);
		this.panel4.Controls.Add(this.cbFind);
		this.panel4.Controls.Add(this.ultraLabel2);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel4.Location = new System.Drawing.Point(0, 48);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(692, 417);
		this.panel4.TabIndex = 2;
		this.panel9.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel9.Controls.Add(this.lblDBName);
		this.panel9.Controls.Add(this.ultraButton6);
		this.panel9.Controls.Add(this.groupBox1);
		this.panel9.Controls.Add(this.ultraButton4);
		this.panel9.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel9.Location = new System.Drawing.Point(0, 373);
		this.panel9.Name = "panel9";
		this.panel9.Size = new System.Drawing.Size(692, 44);
		this.panel9.TabIndex = 19;
		appearance7.FontData.Name = "細明體";
		appearance7.FontData.SizeInPoints = 9f;
		appearance7.ForeColor = System.Drawing.Color.Blue;
		this.lblDBName.Appearance = appearance7;
		this.lblDBName.Location = new System.Drawing.Point(7, 13);
		this.lblDBName.Name = "lblDBName";
		this.lblDBName.Size = new System.Drawing.Size(497, 27);
		this.lblDBName.TabIndex = 13;
		this.lblDBName.Text = "挑選的資料庫:";
		this.ultraButton6.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance8.Image = resources.GetObject("appearance8.Image");
		appearance8.ImageHAlign = Infragistics.Win.HAlign.Left;
		appearance8.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton6.Appearance = appearance8;
		this.ultraButton6.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton6.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton6.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton6.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton6.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton6.Location = new System.Drawing.Point(509, 10);
		this.ultraButton6.Name = "ultraButton6";
		this.ultraButton6.ShowFocusRect = false;
		this.ultraButton6.ShowOutline = false;
		this.ultraButton6.Size = new System.Drawing.Size(88, 31);
		this.ultraButton6.SupportThemes = false;
		this.ultraButton6.TabIndex = 4;
		this.ultraButton6.Text = "上一步";
		this.ultraButton6.Click += new System.EventHandler(ultraButton6_Click);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(692, 8);
		this.groupBox1.TabIndex = 3;
		this.groupBox1.TabStop = false;
		this.ultraButton4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance9.Image = resources.GetObject("appearance9.Image");
		appearance9.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton4.Appearance = appearance9;
		this.ultraButton4.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton4.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton4.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ultraButton4.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton4.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton4.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton4.Location = new System.Drawing.Point(599, 10);
		this.ultraButton4.Name = "ultraButton4";
		this.ultraButton4.ShowFocusRect = false;
		this.ultraButton4.ShowOutline = false;
		this.ultraButton4.Size = new System.Drawing.Size(88, 31);
		this.ultraButton4.SupportThemes = false;
		this.ultraButton4.TabIndex = 2;
		this.ultraButton4.Text = "取消";
		this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel5.Controls.Add(this.c1FlexGrid1);
		this.panel5.Controls.Add(this.panel6);
		this.panel5.Location = new System.Drawing.Point(16, 40);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(660, 324);
		this.panel5.TabIndex = 14;
		this.c1FlexGrid1.AllowEditing = false;
		this.c1FlexGrid1.BackColor = System.Drawing.Color.White;
		this.c1FlexGrid1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
		this.c1FlexGrid1.ColumnInfo = "5,0,0,0,0,110,Columns:0{Width:25;Name:\"IsData\";AllowDragging:False;AllowEditing:False;TextAlign:RightCenter;ImageAlign:CenterCenter;}\t1{Width:107;Name:\"ProjectCode\";AllowDragging:False;AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;}\t2{Width:320;Name:\"projCName\";AllowDragging:False;AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;}\t3{Width:170;Name:\"projAddress\";AllowDragging:False;AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;}\t4{Name:\"projEName\";Visible:False;DataType:System.String;TextAlign:LeftCenter;}\t";
		this.c1FlexGrid1.Cursor = System.Windows.Forms.Cursors.Hand;
		this.c1FlexGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.c1FlexGrid1.ExtendLastCol = true;
		this.c1FlexGrid1.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
		this.c1FlexGrid1.ForeColor = System.Drawing.SystemColors.WindowText;
		this.c1FlexGrid1.Location = new System.Drawing.Point(0, 36);
		this.c1FlexGrid1.Name = "c1FlexGrid1";
		this.c1FlexGrid1.Rows.Fixed = 0;
		this.c1FlexGrid1.Rows.MinSize = 25;
		this.c1FlexGrid1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.c1FlexGrid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
		this.c1FlexGrid1.Size = new System.Drawing.Size(658, 286);
		this.c1FlexGrid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection("Normal{Font:細明體, 11.25pt;BackColor:White;Border:Flat,1,Transparent,Both;}\tFixed{BackColor:Control;ForeColor:ControlText;Border:Flat,1,ControlDark,Both;}\tHighlight{BackColor:102, 153, 255;}\tFocus{BackColor:204, 236, 255;}\tSearch{BackColor:Highlight;ForeColor:HighlightText;}\tFrozen{BackColor:Beige;}\tEmptyArea{BackColor:232, 232, 232;Border:Flat,1,ControlDarkDark,Both;}\tGrandTotal{BackColor:Black;ForeColor:White;}\tSubtotal0{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal1{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal2{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal3{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal4{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal5{BackColor:ControlDarkDark;ForeColor:White;}\t");
		this.c1FlexGrid1.TabIndex = 8;
		this.c1FlexGrid1.MouseLeave += new System.EventHandler(c1FlexGrid1_MouseLeave);
		this.c1FlexGrid1.MouseDown += new System.Windows.Forms.MouseEventHandler(c1FlexGrid1_MouseDown);
		this.c1FlexGrid1.MouseMove += new System.Windows.Forms.MouseEventHandler(c1FlexGrid1_MouseMove);
		this.c1FlexGrid1.MouseEnter += new System.EventHandler(c1FlexGrid1_MouseEnter);
		this.panel6.Controls.Add(this.ultraLabel5);
		this.panel6.Controls.Add(this.ultraLabel4);
		this.panel6.Controls.Add(this.ultraLabel6);
		this.panel6.Controls.Add(this.ultraLabel3);
		this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel6.Location = new System.Drawing.Point(0, 0);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(658, 36);
		this.panel6.TabIndex = 7;
		appearance10.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		appearance10.BackColor2 = System.Drawing.Color.FromArgb(225, 247, 223);
		appearance10.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance10.FontData.Name = "細明體";
		appearance10.FontData.SizeInPoints = 11f;
		appearance10.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance10.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel5.Appearance = appearance10;
		this.ultraLabel5.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ultraLabel5.Location = new System.Drawing.Point(452, 0);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(206, 36);
		this.ultraLabel5.TabIndex = 2;
		this.ultraLabel5.Text = "工程地址";
		appearance11.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		appearance11.BackColor2 = System.Drawing.Color.FromArgb(225, 247, 223);
		appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance11.FontData.Name = "細明體";
		appearance11.FontData.SizeInPoints = 11f;
		appearance11.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance11.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel4.Appearance = appearance11;
		this.ultraLabel4.Dock = System.Windows.Forms.DockStyle.Left;
		this.ultraLabel4.Location = new System.Drawing.Point(132, 0);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(320, 36);
		this.ultraLabel4.TabIndex = 1;
		this.ultraLabel4.Text = "工程名稱";
		appearance12.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		appearance12.BackColor2 = System.Drawing.Color.FromArgb(225, 247, 223);
		appearance12.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance12.FontData.Name = "細明體";
		appearance12.FontData.SizeInPoints = 11f;
		appearance12.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance12.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel6.Appearance = appearance12;
		this.ultraLabel6.Dock = System.Windows.Forms.DockStyle.Left;
		this.ultraLabel6.Location = new System.Drawing.Point(28, 0);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(104, 36);
		this.ultraLabel6.TabIndex = 3;
		this.ultraLabel6.Text = "工項代碼";
		appearance13.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		appearance13.BackColor2 = System.Drawing.Color.FromArgb(225, 247, 223);
		appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance13.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance13.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel3.Appearance = appearance13;
		this.ultraLabel3.Dock = System.Windows.Forms.DockStyle.Left;
		this.ultraLabel3.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(28, 36);
		this.ultraLabel3.TabIndex = 0;
		this.ultraButton2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance14.Image = 0;
		this.ultraButton2.Appearance = appearance14;
		this.ultraButton2.AutoSize = true;
		this.ultraButton2.BackColor = System.Drawing.Color.Transparent;
		this.ultraButton2.ButtonStyle = Infragistics.Win.UIElementButtonStyle.PopupBorderless;
		this.ultraButton2.ImageList = this.imageList1;
		this.ultraButton2.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton2.Location = new System.Drawing.Point(654, 7);
		this.ultraButton2.Name = "ultraButton2";
		this.ultraButton2.ShowFocusRect = false;
		this.ultraButton2.ShowOutline = false;
		this.ultraButton2.Size = new System.Drawing.Size(24, 24);
		this.ultraButton2.TabIndex = 13;
		this.ultraButton2.Click += new System.EventHandler(ultraButton2_Click);
		this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
		this.imageList1.ImageSize = new System.Drawing.Size(20, 20);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
		this.cbFind.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance15.FontData.SizeInPoints = 11f;
		this.cbFind.Appearance = appearance15;
		this.cbFind.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
		appearance16.BackColor = System.Drawing.Color.FromArgb(153, 204, 255);
		this.cbFind.ButtonAppearance = appearance16;
		this.cbFind.ButtonStyle = Infragistics.Win.UIElementButtonStyle.PopupBorderless;
		this.cbFind.Location = new System.Drawing.Point(519, 10);
		this.cbFind.Name = "cbFind";
		this.cbFind.Size = new System.Drawing.Size(137, 20);
		this.cbFind.TabIndex = 12;
		this.cbFind.Text = null;
		this.cbFind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(cbFind_KeyPress);
		this.ultraLabel2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.ultraLabel2.Location = new System.Drawing.Point(477, 13);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(40, 20);
		this.ultraLabel2.TabIndex = 11;
		this.ultraLabel2.Text = "尋找:";
		appearance17.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance17.BackColor2 = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance17.FontData.Name = "新細明體";
		appearance17.FontData.SizeInPoints = 12f;
		appearance17.ForeColor = System.Drawing.Color.White;
		appearance17.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance17.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel1.Appearance = appearance17;
		this.ultraLabel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.ultraLabel1.Font = new System.Drawing.Font("細明體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.ultraLabel1.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(692, 48);
		this.ultraLabel1.TabIndex = 1;
		this.ultraLabel1.Text = "專案挑選";
		this.Tab_B.Controls.Add(this.panel3);
		this.Tab_B.Controls.Add(this.panel1);
		this.Tab_B.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_B.Name = "Tab_B";
		this.Tab_B.Size = new System.Drawing.Size(692, 465);
		this.panel3.Controls.Add(this.c1FlexGrid2);
		this.panel3.Controls.Add(this.panel2);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel3.Location = new System.Drawing.Point(0, 44);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(692, 421);
		this.panel3.TabIndex = 3;
		this.c1FlexGrid2._ExcelFileName = "";
		this.c1FlexGrid2._ExcelSheeName = "";
		this.c1FlexGrid2._IsOpenExcelAfterExport = false;
		this.c1FlexGrid2.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
		this.c1FlexGrid2.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
		this.c1FlexGrid2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.c1FlexGrid2.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.c1FlexGrid2.ColumnInfo = "29,1,0,0,0,110,Columns:0{Width:20;Name:\"RowIndicator\";DataType:System.Int32;TextAlign:RightCenter;}\t1{Width:40;Name:\"IsCheck\";Caption:\"勾選\";DataType:System.Boolean;ImageAlign:CenterCenter;}\t2{Width:100;Name:\"PccesCode\";Caption:\"工項代碼\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;}\t3{Width:200;Name:\"CName\";Caption:\"工項名稱\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;}\t4{Width:40;Name:\"AnaImg\";Caption:\"分析\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;}\t5{Width:80;Name:\"UnitName\";Caption:\"單位\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;}\t6{Width:100;Name:\"Cost\";Caption:\"單價\";AllowEditing:False;DataType:System.Decimal;}\t7{Width:70;Name:\"LRate\";Caption:\"人工(%)\";AllowEditing:False;DataType:System.Decimal;TextAlign:LeftCenter;}\t8{Width:70;Name:\"ERate\";Caption:\"機具(%)\";AllowEditing:False;DataType:System.Decimal;}\t9{Width:70;Name:\"MRate\";Caption:\"材料(%)\";AllowEditing:False;DataType:System.Decimal;}\t10{Width:70;Name:\"WRate\";Caption:\"雜項(%)\";AllowEditing:False;DataType:System.Decimal;}\t11{Name:\"memo\";Caption:\"備註\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;}\t12{Width:150;Name:\"EName\";Caption:\"Description\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;}\t13{Width:100;Name:\"EUnit\";Caption:\"Unit\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;}\t14{Width:40;Name:\"Analysis\";Caption:\"分析\";AllowEditing:False;DataType:System.Boolean;ImageAlign:CenterCenter;}\t15{Width:50;Name:\"PubCode\";Caption:\"PubCode\";AllowEditing:False;DataType:System.Int32;TextAlign:RightCenter;}\t16{Name:\"AnalysisQty\";Caption:\"AnalysisQty\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;}\t17{Name:\"CostKind\";Caption:\"CostKind\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;}\t18{Name:\"extendCode\";Caption:\"extendCode\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;}\t19{Name:\"rate\";Caption:\"rate\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;}\t20{Name:\"resCode\";Caption:\"resCode\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;}\t21{Name:\"resType\";Caption:\"resType\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;}\t22{Name:\"xNameE\";Caption:\"xNameE\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;}\t23{Name:\"xNameC\";Caption:\"xNameC\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;}\t24{Name:\"State\";Caption:\"State\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;}\t25{Name:\"usrQty\";Caption:\"usrQty\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;}\t26{Name:\"usrAmt\";Caption:\"usrAmt\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;}\t27{Name:\"Show\";Caption:\"Show\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;}\t28{Name:\"Post\";Caption:\"Post\";AllowEditing:False;DataType:System.String;TextAlign:LeftCenter;}\t";
		this.c1FlexGrid2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.c1FlexGrid2.ExtendLastCol = true;
		this.c1FlexGrid2.ForeColor = System.Drawing.SystemColors.WindowText;
		this.c1FlexGrid2.Location = new System.Drawing.Point(0, 0);
		this.c1FlexGrid2.Name = "c1FlexGrid2";
		this.c1FlexGrid2.Rows.Count = 1;
		this.c1FlexGrid2.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.c1FlexGrid2.ShowToolTipOnNarrowColumn = true;
		this.c1FlexGrid2.Size = new System.Drawing.Size(692, 381);
		this.c1FlexGrid2.Styles = new C1.Win.C1FlexGrid.CellStyleCollection("Normal{Font:細明體, 11.25pt;BackColor:237, 243, 254;}\tAlternate{BackColor:White;}\tFixed{BackColor:225, 247, 223;ForeColor:ControlText;Border:Flat,1,ControlDark,Both;}\tHighlight{BackColor:102, 153, 255;ForeColor:Black;}\tFocus{BackColor:102, 153, 255;Border:None,1,Black,Both;}\tSearch{BackColor:Highlight;ForeColor:HighlightText;}\tFrozen{BackColor:Beige;}\tEmptyArea{BackColor:232, 232, 232;Border:Flat,1,ControlDarkDark,Both;}\tGrandTotal{BackColor:Black;ForeColor:White;}\tSubtotal0{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal1{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal2{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal3{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal4{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal5{BackColor:ControlDarkDark;ForeColor:White;}\t");
		this.c1FlexGrid2.TabIndex = 3;
		this.panel2.Controls.Add(this.ultraButton7);
		this.panel2.Controls.Add(this.ultraButton1);
		this.panel2.Controls.Add(this.ultraButton3);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 381);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(692, 40);
		this.panel2.TabIndex = 4;
		this.ultraButton7.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance18.Image = resources.GetObject("appearance18.Image");
		appearance18.ImageHAlign = Infragistics.Win.HAlign.Left;
		appearance18.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton7.Appearance = appearance18;
		this.ultraButton7.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton7.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton7.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton7.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton7.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton7.Location = new System.Drawing.Point(416, 5);
		this.ultraButton7.Name = "ultraButton7";
		this.ultraButton7.ShowFocusRect = false;
		this.ultraButton7.ShowOutline = false;
		this.ultraButton7.Size = new System.Drawing.Size(88, 31);
		this.ultraButton7.SupportThemes = false;
		this.ultraButton7.TabIndex = 10;
		this.ultraButton7.Text = "上一步";
		this.ultraButton7.Click += new System.EventHandler(ultraButton7_Click);
		this.ultraButton1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance19.BackColor = System.Drawing.SystemColors.ControlLightLight;
		appearance19.BackColor2 = System.Drawing.SystemColors.ControlLight;
		appearance19.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance19.Image = resources.GetObject("appearance19.Image");
		this.ultraButton1.Appearance = appearance19;
		this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton1.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton1.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton1.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton1.Location = new System.Drawing.Point(508, 5);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.ShowFocusRect = false;
		this.ultraButton1.ShowOutline = false;
		this.ultraButton1.Size = new System.Drawing.Size(88, 31);
		this.ultraButton1.SupportThemes = false;
		this.ultraButton1.TabIndex = 9;
		this.ultraButton1.Text = "確定";
		this.ultraButton1.Visible = false;
		this.ultraButton1.Click += new System.EventHandler(ultraButton1_Click_1);
		this.ultraButton3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance20.Image = resources.GetObject("appearance20.Image");
		this.ultraButton3.Appearance = appearance20;
		this.ultraButton3.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ultraButton3.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton3.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton3.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton3.Location = new System.Drawing.Point(600, 5);
		this.ultraButton3.Name = "ultraButton3";
		this.ultraButton3.ShowFocusRect = false;
		this.ultraButton3.ShowOutline = false;
		this.ultraButton3.Size = new System.Drawing.Size(88, 31);
		this.ultraButton3.SupportThemes = false;
		this.ultraButton3.TabIndex = 8;
		this.ultraButton3.Text = "取消";
		this.panel1.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.panel1.Controls.Add(this.lblProject);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(692, 44);
		this.panel1.TabIndex = 1;
		appearance21.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance21.BackColor2 = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance21.FontData.Name = "新細明體";
		appearance21.FontData.SizeInPoints = 12f;
		appearance21.ForeColor = System.Drawing.Color.White;
		appearance21.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance21.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblProject.Appearance = appearance21;
		this.lblProject.Dock = System.Windows.Forms.DockStyle.Top;
		this.lblProject.Font = new System.Drawing.Font("細明體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblProject.Location = new System.Drawing.Point(0, 0);
		this.lblProject.Name = "lblProject";
		this.lblProject.Size = new System.Drawing.Size(692, 48);
		this.lblProject.TabIndex = 1;
		this.lblProject.Text = "目前專案:";
		this.Tab_Ctrl.Controls.Add(this.ultraTabSharedControlsPage1);
		this.Tab_Ctrl.Controls.Add(this.Tab_A);
		this.Tab_Ctrl.Controls.Add(this.Tab_B);
		this.Tab_Ctrl.Controls.Add(this.Tab_1);
		this.Tab_Ctrl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Tab_Ctrl.Location = new System.Drawing.Point(0, 0);
		this.Tab_Ctrl.Name = "Tab_Ctrl";
		this.Tab_Ctrl.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.Tab_Ctrl.Size = new System.Drawing.Size(692, 465);
		this.Tab_Ctrl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
		this.Tab_Ctrl.TabIndex = 1;
		ultraTab1.TabPage = this.Tab_1;
		ultraTab1.Text = "tab3";
		ultraTab2.TabPage = this.Tab_A;
		ultraTab2.Text = "tab1";
		ultraTab3.TabPage = this.Tab_B;
		ultraTab3.Text = "tab2";
		this.Tab_Ctrl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[3] { ultraTab1, ultraTab2, ultraTab3 });
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(692, 465);
		this.imageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
		this.imageList2.ImageSize = new System.Drawing.Size(16, 16);
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.White;
		this.imageList3.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
		this.imageList3.ImageSize = new System.Drawing.Size(16, 16);
		this.imageList3.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList3.ImageStream");
		this.imageList3.TransparentColor = System.Drawing.Color.Magenta;
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.CancelButton = this.A_Btn_Cncl;
		base.ClientSize = new System.Drawing.Size(692, 465);
		base.Controls.Add(this.Tab_Ctrl);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.Name = "FormBudgetPickProjRes";
		base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
		this.Text = "挑選專案工作要項";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormBudgetPickProjRes_FormClosing);
		base.Load += new System.EventHandler(FormBudgetPickProjRes_Load);
		this.Tab_1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.GridUnit1).EndInit();
		this.panel8.ResumeLayout(false);
		this.panel7.ResumeLayout(false);
		this.Tab_A.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
		this.panel9.ResumeLayout(false);
		this.panel5.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid1).EndInit();
		this.panel6.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.cbFind).EndInit();
		this.Tab_B.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid2).EndInit();
		this.panel2.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
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
