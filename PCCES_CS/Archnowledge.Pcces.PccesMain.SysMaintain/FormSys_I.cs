using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.PccesMain.ArchControls;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinStatusBar;
using Infragistics.Win.UltraWinTabControl;

namespace Archnowledge.Pcces.PccesMain.SysMaintain;

public class FormSys_I : UserControl
{
	private DBClass DBCLS = new DBClass();

	private DataTable DT_Users = new DataTable();

	private DataTable DT_AllProj = new DataTable();

	private DataTable DT_UsrProj = new DataTable();

	private DataTable UserProject = new DataTable();

	private DataTable ProjectUser = new DataTable();

	private DataTable DT_ProjUsr = new DataTable();

	private string F_UserID;

	private FormStatus FORM_STATUS = FormStatus.Iinitial;

	private IContainer components;

	private Panel panel1;

	private Panel panel2;

	private Panel panel3;

	private Panel panel4;

	private Splitter splitter2;

	private Panel panel5;

	private UltraLabel ultraLabel1;

	public GridMrsBase GridUsers;

	private UltraLabel ultraLabel2;

	private UltraLabel ultraLabel3;

	private UltraButton BtnRemove;

	private UltraButton BtnAdd;

	private GridBudget gridProjectAll;

	private GridBudget gridProjectUsr;

	private GridBudget gridBudget1;

	private Panel panel7;

	private Splitter splitter1;

	private UltraTabControl Tab_Ctrl;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl Tab_A;

	private UltraTabPageControl Tab_B;

	private Panel panel6;

	private UltraStatusBar ultraStatusBar1;

	private Panel panel8;

	private UltraStatusBar ultraStatusBar2;

	private Panel panel9;

	private Panel panel10;

	private UltraLabel ultraLabel4;

	private Splitter splitter3;

	private Panel panel11;

	private Panel panel12;

	private UltraLabel ultraLabel5;

	private Panel panel13;

	private Splitter splitter4;

	private Panel panel14;

	private UltraLabel ultraLabel6;

	private GridBudget gridBudget4;

	private GridBudget GridProjects;

	private UltraButton BtnSwitchToProj;

	private UltraButton ultraButton4;

	public GridMrsBase gridUserAll;

	public GridMrsBase gridUserProject;

	private UltraButton BtnRemove2;

	private UltraButton BtnAdd2;

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

	public FormSys_I()
	{
		InitializeComponent();
	}

	private void BindToUsers()
	{
		DBClass DBCLS1 = new DBClass();
		DBCLS1._FS_UserID = F_UserID;
		DT_Users = DBCLS1.GetUserList();
		GridUsers.Rows.Count = DT_Users.Rows.Count + 1;
		ultraStatusBar1.Panels[0].Text = "使用者數：" + DT_Users.Rows.Count;
		for (int i = 0; i < DT_Users.Rows.Count; i++)
		{
			GridUsers[i + 1, "UserID"] = DT_Users.Rows[i]["UserID"].ToString().Trim();
			GridUsers[i + 1, "UserName"] = DT_Users.Rows[i]["UserName"].ToString().Trim();
			GridUsers[i + 1, "Power"] = DT_Users.Rows[i]["Power"].ToString().Trim() + "." + ((DT_Users.Rows[i]["Power"].ToString().Trim() == "1") ? "系統管理員" : "一般使用者");
			GridUsers[i + 1, "Password"] = DT_Users.Rows[i]["Pwd"].ToString().Trim();
		}
		DBCLS1 = null;
		GridUsers.AutoSizeCols();
	}

	private void BindToUserProject(string sUser)
	{
		PubProject pubProject = new PubProject();
		DataSet ds = pubProject.GetProjectList(sUser);
		DataView dvUserProject = ds.Tables[0].DefaultView;
		dvUserProject.RowFilter = "Auth='N'";
		gridProjectAll.Rows.Count = dvUserProject.Count + 1;
		for (int i = 0; i < dvUserProject.Count; i++)
		{
			gridProjectAll[i + 1, "ProjectCode"] = dvUserProject[i]["projectCode"].ToString().Trim();
			gridProjectAll[i + 1, "CName"] = dvUserProject[i]["projCName"].ToString().Trim();
			gridProjectAll[i + 1, "EName"] = dvUserProject[i]["projAddress"].ToString().Trim();
			gridProjectAll[i + 1, "Address"] = ds.Tables[0].Rows[i]["projAddress"].ToString().Trim();
		}
		dvUserProject.RowFilter = "Auth='Y'";
		gridProjectUsr.Rows.Count = dvUserProject.Count + 1;
		for (int i = 0; i < dvUserProject.Count; i++)
		{
			gridProjectUsr[i + 1, "ProjectCode"] = dvUserProject[i]["projectCode"].ToString().Trim();
			gridProjectUsr[i + 1, "CName"] = dvUserProject[i]["projCName"].ToString().Trim();
			gridProjectUsr[i + 1, "EName"] = dvUserProject[i]["projEName"].ToString().Trim();
			gridProjectUsr[i + 1, "Address"] = dvUserProject[i]["projAddress"].ToString().Trim();
			gridProjectUsr[i + 1, "BudEst"] = ArchConvert.Obj2Bool(dvUserProject[i]["BudEstAuth"]);
			gridProjectUsr[i + 1, "BudQuote"] = ArchConvert.Obj2Bool(dvUserProject[i]["BudQuoteAuth"]);
		}
	}

	private void FormSys_I_Load(object sender, EventArgs e)
	{
		UserProject.Columns.Add("ProjectCode", Type.GetType("System.String"));
		ProjectUser.Columns.Add("UserID", Type.GetType("System.String"));
		BindToUsers();
		if (GridUsers.Rows.Count > 1)
		{
			BindToUserProject(GridUsers[1, "UserID"].ToString().Trim());
		}
		BindToProjects();
		BindToAllUser();
		if (GridProjects.Rows.Count > 1)
		{
			BindToProjectUser(GridProjects[1, "ProjectCode"].ToString().Trim());
		}
		if (SysConfig.SysChangeManagement)
		{
			gridProjectUsr.Cols["BudEst"].Visible = true;
			gridProjectUsr.Cols["BudQuote"].Visible = true;
			BtnSwitchToProj.Visible = false;
		}
		else
		{
			gridProjectUsr.Cols["BudEst"].Visible = false;
			gridProjectUsr.Cols["BudQuote"].Visible = false;
		}
		FORM_STATUS = FormStatus.Normal;
	}

	private void BindToProjects()
	{
		PubProject pubProject = new PubProject();
		DataSet ds = pubProject.GetProjectList(F_UserID);
		DT_AllProj = ds.Tables[0];
		GridProjects.Rows.Count = DT_AllProj.Rows.Count + 1;
		for (int i = 0; i < DT_AllProj.Rows.Count; i++)
		{
			GridProjects[i + 1, "ProjectCode"] = DT_AllProj.Rows[i]["projectCode"].ToString().Trim();
			GridProjects[i + 1, "CName"] = DT_AllProj.Rows[i]["projCName"].ToString().Trim();
			GridProjects[i + 1, "EName"] = DT_AllProj.Rows[i]["projEName"].ToString().Trim();
			GridProjects[i + 1, "Address"] = DT_AllProj.Rows[i]["projAddress"].ToString().Trim();
		}
		ultraStatusBar2.Panels[0].Text = "專案總數：" + DT_AllProj.Rows.Count;
	}

	private void BindToAllUser()
	{
		DBClass DBCLS1 = new DBClass();
		DBCLS1._FS_UserID = F_UserID;
		DT_Users = DBCLS1.GetUserList();
		gridUserAll.Rows.Count = DT_Users.Rows.Count + 1;
		for (int i = 0; i < DT_Users.Rows.Count; i++)
		{
			gridUserAll[i + 1, "UserID"] = DT_Users.Rows[i]["UserID"].ToString().Trim();
			gridUserAll[i + 1, "UserName"] = DT_Users.Rows[i]["UserName"].ToString().Trim();
			gridUserAll[i + 1, "Power"] = DT_Users.Rows[i]["Power"].ToString().Trim() + "." + ((DT_Users.Rows[i]["Power"].ToString().Trim() == "1") ? "系統管理員" : "一般使用者");
			gridUserAll[i + 1, "Password"] = DT_Users.Rows[i]["Pwd"].ToString().Trim();
		}
		DBCLS1 = null;
		gridUserAll.AutoSizeCols();
	}

	private void BindToProjectUser(string sProjectCode)
	{
		BindToAllUser();
		DBCLS._FS_UserID = F_UserID;
		DT_ProjUsr = DBCLS.GetProjectUserList(sProjectCode);
		DataView dv = new DataView(DT_Users);
		gridUserProject.Rows.Count = DT_ProjUsr.Rows.Count + 1;
		for (int i = 0; i < DT_ProjUsr.Rows.Count; i++)
		{
			DataRow dr = DT_ProjUsr.Rows[i];
			string sUserID = dr["UserID"].ToString().Trim();
			int iFind = gridUserAll.FindRow(sUserID, 1, gridUserAll.Cols["UserID"].SafeIndex, caseSensitive: false, fullMatch: true, wrap: false);
			if (iFind > 0)
			{
				gridUserAll.Rows.Remove(iFind);
			}
			gridUserProject[i + 1, "UserID"] = sUserID;
			gridUserProject[i + 1, "UserName"] = dr["UserName"].ToString().Trim();
			dv.RowFilter = "UserID = '" + sUserID + "'";
			if (dv.Count > 0)
			{
				gridUserProject[i + 1, "Power"] = dr["Power"].ToString().Trim() + "." + ((dv[0]["Power"].ToString().Trim() == "1") ? "系統管理員" : "一般使用者");
			}
			else
			{
				gridUserProject[i + 1, "Power"] = dr["Power"].ToString().Trim() + ".一般使用者(*)";
			}
			gridUserProject[i + 1, "Password"] = dr["Pwd"].ToString().Trim();
		}
		dv.Dispose();
		dv = null;
		gridUserProject.AutoSizeCols();
	}

	private void panel4_Resize(object sender, EventArgs e)
	{
		BtnAdd.Left = panel4.Width / 2 - BtnAdd.Width - 2;
		BtnRemove.Left = panel4.Width / 2 + 2;
	}

	private void panel13_Resize(object sender, EventArgs e)
	{
		BtnAdd2.Left = panel13.Width / 2 - BtnAdd2.Width - 2;
		BtnRemove2.Left = panel13.Width / 2 + 2;
	}

	private void panel2_Resize(object sender, EventArgs e)
	{
		panel3.Height = (panel2.Height - panel4.Height) * 2 / 3;
		panel5.Height = (panel2.Height - panel4.Height) / 3;
	}

	private void panel11_Resize(object sender, EventArgs e)
	{
		panel12.Height = (panel11.Height - panel13.Height) * 2 / 3;
		panel14.Height = (panel11.Height - panel13.Height) / 3;
	}

	private void GridUsers_AfterSelChange(object sender, RangeEventArgs e)
	{
		if (FORM_STATUS == FormStatus.Normal && GridUsers.Row >= 0 && GridUsers[GridUsers.Row, "UserID"] != null)
		{
			string StrUserID = GridUsers[GridUsers.Row, "UserID"].ToString().Trim();
			BindToUserProject(StrUserID);
		}
	}

	private void GridProjects_AfterSelChange(object sender, RangeEventArgs e)
	{
		if (FORM_STATUS == FormStatus.Normal && GridProjects.Row >= 0 && GridProjects[GridProjects.Row, "ProjectCode"] != null)
		{
			string StrProjectCode = GridProjects[GridProjects.Row, "ProjectCode"].ToString().Trim();
			BindToProjectUser(StrProjectCode);
		}
	}

	private void BtnAdd_Click(object sender, EventArgs e)
	{
		for (int i = gridProjectAll.Rows.Count - 1; i >= 1; i--)
		{
			if (gridProjectAll.Rows[i].Selected)
			{
				if (CheckIsAlreadyExist(gridProjectAll[i, "ProjectCode"].ToString().Trim()) > -1)
				{
					MessageBox.Show(this, "有重複專案", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				else
				{
					AddIntoGrid2(i);
					gridProjectAll.Rows.Remove(i);
				}
			}
		}
		Do_ChangeUserProject();
	}

	private void BtnAdd2_Click(object sender, EventArgs e)
	{
		for (int i = gridUserAll.Rows.Count - 1; i >= 1; i--)
		{
			if (CheckIsAlreadyExist2(gridUserAll[i, "UserID"].ToString().Trim()) > -1)
			{
				MessageBox.Show(this, "有重複的使用者", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				AddIntoGrid2_2(i);
				gridUserAll.Rows.Remove(i);
			}
		}
		Do_ChangeProjectUser();
	}

	private int CheckIsAlreadyExist(string sProjectCode)
	{
		int RetV = -1;
		for (int i = 1; i < gridProjectUsr.Rows.Count; i++)
		{
			if (gridProjectUsr[i, "ProjectCode"].ToString() == sProjectCode)
			{
				RetV = i;
				break;
			}
		}
		return RetV;
	}

	private int CheckIsAlreadyExist2(string sUserID)
	{
		int RetV = -1;
		for (int i = 1; i < gridUserProject.Rows.Count; i++)
		{
			if (gridUserProject[i, "UserID"].ToString() == sUserID)
			{
				RetV = i;
				break;
			}
		}
		return RetV;
	}

	private void AddIntoGrid2(int IndicateRow)
	{
		gridProjectUsr.Rows.Count++;
		for (int i = 0; i < gridProjectAll.Cols.Count; i++)
		{
			string colName = gridProjectAll.Cols[i].Name;
			if (gridProjectUsr.Cols[colName] != null)
			{
				gridProjectUsr[gridProjectUsr.Rows.Count - 1, colName] = gridProjectAll[IndicateRow, colName];
			}
		}
	}

	private void AddIntoGrid2_2(int IndicateRow)
	{
		gridUserProject.Rows.Count++;
		for (int i = 0; i < gridUserAll.Cols.Count; i++)
		{
			gridUserProject[gridUserProject.Rows.Count - 1, gridUserAll.Cols[i].Name] = gridUserAll[IndicateRow, i];
		}
	}

	private void AddIntoGrid1(int IndicateRow)
	{
		gridProjectAll.Rows.Count++;
		for (int i = 0; i < gridProjectUsr.Cols.Count; i++)
		{
			string colName = gridProjectUsr.Cols[i].Name;
			if (gridProjectAll.Cols[colName] != null)
			{
				gridProjectAll[gridProjectAll.Rows.Count - 1, colName] = gridProjectUsr[IndicateRow, colName];
			}
		}
	}

	private void AddIntoGrid1_2(int IndicateRow)
	{
		gridUserAll.Rows.Count++;
		for (int i = 0; i < gridUserProject.Cols.Count; i++)
		{
			gridUserAll[gridUserAll.Rows.Count - 1, gridUserAll.Cols[i].Name] = gridUserProject[IndicateRow, i];
		}
	}

	private void BtnRemove_Click(object sender, EventArgs e)
	{
		for (int i = gridProjectUsr.Rows.Count - 1; i > 0; i--)
		{
			if (gridProjectUsr.Rows[i].Selected)
			{
				AddIntoGrid1(i);
				gridProjectUsr.RemoveItem(i);
			}
		}
		Do_ChangeUserProject();
	}

	private void BtnRemove2_Click(object sender, EventArgs e)
	{
		for (int i = gridUserProject.Rows.Count - 1; i > 0; i--)
		{
			if (gridUserProject.Rows[i].Selected)
			{
				AddIntoGrid1_2(i);
				gridUserProject.RemoveItem(i);
			}
		}
		Do_ChangeProjectUser();
	}

	private void GridUsers_BeforeSelChange(object sender, RangeEventArgs e)
	{
		if (FORM_STATUS == FormStatus.Normal && GridUsers.Row >= 0 && GridUsers[e.OldRange.r1, "UserID"] != null)
		{
			Do_ChangeUserProject();
		}
	}

	private void Do_ChangeUserProject()
	{
		string userID = GridUsers[GridUsers.Row, "UserID"].ToString().Trim();
		ProjAuthority projAuthority = new ProjAuthority();
		DataSet dsProjAuthority = projAuthority.GetProjAuthorityByUserID(userID);
		foreach (DataRow dr in dsProjAuthority.Tables[0].Rows)
		{
			dr.Delete();
		}
		for (int i = 1; i < gridProjectUsr.Rows.Count; i++)
		{
			DataRow dr = dsProjAuthority.Tables[0].NewRow();
			dr["ProjectCode"] = gridProjectUsr[i, "ProjectCode"].ToString().Trim();
			dr["UserID"] = userID;
			dr["BudEst"] = (ArchConvert.Obj2Bool(gridProjectUsr[i, "BudEst"]) ? "Y" : "N");
			dr["BudQuote"] = (ArchConvert.Obj2Bool(gridProjectUsr[i, "BudQuote"]) ? "Y" : "N");
			dsProjAuthority.Tables[0].Rows.Add(dr);
		}
		ExecResult ER = projAuthority.UpdateProjAuthority(dsProjAuthority);
		if (ER.ReturnCode != 0)
		{
			MessageBox.Show(this, userID + "的專案權限, 更新資料庫有誤, 請確認後再執行", "存檔", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void Do_ChangeProjectUser()
	{
		string StrProjectCode = GridProjects[GridProjects.Row, "ProjectCode"].ToString().Trim();
		ProjectUser.Clear();
		for (int i = 1; i < gridUserProject.Rows.Count; i++)
		{
			DataRow DR = ProjectUser.NewRow();
			DR["UserID"] = gridUserProject[i, "UserID"].ToString().Trim();
			ProjectUser.Rows.Add(DR);
		}
		DBCLS._FS_UserID = F_UserID;
		if (!DBCLS.UpdateProjectUser(StrProjectCode, ProjectUser))
		{
			MessageBox.Show(this, StrProjectCode + "的使用者權限, 更新至料庫有誤, 請確認後再執行", "存檔", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void gridProjectAll_DoubleClick(object sender, EventArgs e)
	{
		BtnAdd_Click(this, EventArgs.Empty);
	}

	private void BtnSwitchToProj_Click(object sender, EventArgs e)
	{
		GridProjects_AfterSelChange(sender, null);
		Tab_B.Tab.Selected = true;
	}

	private void ultraButton4_Click(object sender, EventArgs e)
	{
		GridUsers_AfterSelChange(sender, null);
		Tab_A.Tab.Selected = true;
	}

	private void GridProjects_BeforeSelChange(object sender, RangeEventArgs e)
	{
		if (FORM_STATUS == FormStatus.Normal && GridProjects.Row >= 0 && GridProjects[e.OldRange.r1, "ProjectCode"] != null)
		{
			Do_ChangeProjectUser();
		}
	}

	private void gridProjectUsr_AfterEdit(object sender, RowColEventArgs e)
	{
		if (e.Col > 0 && e.Row > 0)
		{
			Row GridRow = gridProjectUsr.Rows[e.Row];
			string columnName = gridProjectUsr.Cols[e.Col].Name;
			string projectCode = gridProjectUsr[e.Row, "ProjectCode"].ToString().Trim();
			string userID = GridUsers[GridUsers.Row, "UserID"].ToString().Trim();
			ProjAuthority projAuthoruty = new ProjAuthority();
			ExecResult ER = new ExecResult();
			if (columnName == "BudEst")
			{
				ER = projAuthoruty.SetBudEstAuth(projectCode, userID, ArchConvert.Obj2Bool(gridProjectUsr[e.Row, columnName]));
			}
			else if (columnName == "BudQuote")
			{
				projAuthoruty.SetBudQuoteAuth(projectCode, userID, ArchConvert.Obj2Bool(gridProjectUsr[e.Row, columnName]));
			}
			if (ER.ReturnCode != 0)
			{
				MessageBox.Show(ER.Message);
			}
		}
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.SysMaintain.FormSys_I));
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel3 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel4 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel5 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel6 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		this.Tab_A = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel7 = new System.Windows.Forms.Panel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.panel3 = new System.Windows.Forms.Panel();
		this.gridProjectAll = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.panel4 = new System.Windows.Forms.Panel();
		this.BtnRemove = new Infragistics.Win.Misc.UltraButton();
		this.BtnAdd = new Infragistics.Win.Misc.UltraButton();
		this.splitter2 = new System.Windows.Forms.Splitter();
		this.panel5 = new System.Windows.Forms.Panel();
		this.gridProjectUsr = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.gridBudget1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.splitter1 = new System.Windows.Forms.Splitter();
		this.panel1 = new System.Windows.Forms.Panel();
		this.GridUsers = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraStatusBar1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.panel6 = new System.Windows.Forms.Panel();
		this.BtnSwitchToProj = new Infragistics.Win.Misc.UltraButton();
		this.Tab_B = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel9 = new System.Windows.Forms.Panel();
		this.panel10 = new System.Windows.Forms.Panel();
		this.GridProjects = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.splitter3 = new System.Windows.Forms.Splitter();
		this.panel11 = new System.Windows.Forms.Panel();
		this.panel12 = new System.Windows.Forms.Panel();
		this.gridUserAll = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.panel13 = new System.Windows.Forms.Panel();
		this.BtnRemove2 = new Infragistics.Win.Misc.UltraButton();
		this.BtnAdd2 = new Infragistics.Win.Misc.UltraButton();
		this.splitter4 = new System.Windows.Forms.Splitter();
		this.panel14 = new System.Windows.Forms.Panel();
		this.gridUserProject = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.gridBudget4 = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.ultraStatusBar2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.panel8 = new System.Windows.Forms.Panel();
		this.ultraButton4 = new Infragistics.Win.Misc.UltraButton();
		this.Tab_Ctrl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.Tab_A.SuspendLayout();
		this.panel7.SuspendLayout();
		this.panel2.SuspendLayout();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridProjectAll).BeginInit();
		this.panel4.SuspendLayout();
		this.panel5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridProjectUsr).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridBudget1).BeginInit();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.GridUsers).BeginInit();
		this.panel6.SuspendLayout();
		this.Tab_B.SuspendLayout();
		this.panel9.SuspendLayout();
		this.panel10.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.GridProjects).BeginInit();
		this.panel11.SuspendLayout();
		this.panel12.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridUserAll).BeginInit();
		this.panel13.SuspendLayout();
		this.panel14.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridUserProject).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gridBudget4).BeginInit();
		this.panel8.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).BeginInit();
		this.Tab_Ctrl.SuspendLayout();
		base.SuspendLayout();
		this.Tab_A.Controls.Add(this.panel7);
		this.Tab_A.Controls.Add(this.ultraStatusBar1);
		this.Tab_A.Controls.Add(this.panel6);
		this.Tab_A.Location = new System.Drawing.Point(0, 0);
		this.Tab_A.Name = "Tab_A";
		this.Tab_A.Size = new System.Drawing.Size(644, 516);
		this.panel7.Controls.Add(this.panel2);
		this.panel7.Controls.Add(this.splitter1);
		this.panel7.Controls.Add(this.panel1);
		this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel7.Location = new System.Drawing.Point(0, 36);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(644, 457);
		this.panel7.TabIndex = 4;
		this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel2.Controls.Add(this.panel3);
		this.panel2.Controls.Add(this.panel4);
		this.panel2.Controls.Add(this.splitter2);
		this.panel2.Controls.Add(this.panel5);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Location = new System.Drawing.Point(273, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(371, 457);
		this.panel2.TabIndex = 2;
		this.panel2.Resize += new System.EventHandler(panel2_Resize);
		this.panel3.Controls.Add(this.gridProjectAll);
		this.panel3.Controls.Add(this.ultraLabel2);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel3.Location = new System.Drawing.Point(0, 0);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(369, 214);
		this.panel3.TabIndex = 0;
		this.gridProjectAll._ExcelFileName = "";
		this.gridProjectAll._ExcelSheeName = "";
		this.gridProjectAll._IsOpenExcelAfterExport = false;
		this.gridProjectAll.AllowEditing = false;
		this.gridProjectAll.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridProjectAll.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
		this.gridProjectAll.ColumnInfo = resources.GetString("gridProjectAll.ColumnInfo");
		this.gridProjectAll.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridProjectAll.ExtendLastCol = true;
		this.gridProjectAll.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridProjectAll.ForeColor = System.Drawing.Color.Black;
		this.gridProjectAll.Location = new System.Drawing.Point(0, 28);
		this.gridProjectAll.Name = "gridProjectAll";
		this.gridProjectAll.Rows.Count = 1;
		this.gridProjectAll.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.gridProjectAll.ShowCursor = true;
		this.gridProjectAll.ShowToolTipOnNarrowColumn = true;
		this.gridProjectAll.Size = new System.Drawing.Size(369, 186);
		this.gridProjectAll.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridProjectAll.Styles"));
		this.gridProjectAll.TabIndex = 3;
		this.gridProjectAll.DoubleClick += new System.EventHandler(gridProjectAll_DoubleClick);
		appearance1.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance1.BackColor2 = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance1.ForeColor = System.Drawing.Color.White;
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel2.Appearance = appearance1;
		this.ultraLabel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.ultraLabel2.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(369, 28);
		this.ultraLabel2.TabIndex = 2;
		this.ultraLabel2.Text = " 系統所有專案列表";
		this.panel4.Controls.Add(this.BtnRemove);
		this.panel4.Controls.Add(this.BtnAdd);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel4.Location = new System.Drawing.Point(0, 214);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(369, 32);
		this.panel4.TabIndex = 1;
		this.panel4.Resize += new System.EventHandler(panel4_Resize);
		appearance2.FontData.Name = "Arial";
		appearance2.FontData.SizeInPoints = 9f;
		appearance2.Image = resources.GetObject("appearance2.Image");
		this.BtnRemove.Appearance = appearance2;
		this.BtnRemove.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.BtnRemove.Location = new System.Drawing.Point(202, 3);
		this.BtnRemove.Name = "BtnRemove";
		this.BtnRemove.Size = new System.Drawing.Size(68, 28);
		this.BtnRemove.SupportThemes = false;
		this.BtnRemove.TabIndex = 3;
		this.BtnRemove.Text = "移除";
		this.BtnRemove.Click += new System.EventHandler(BtnRemove_Click);
		appearance3.FontData.Name = "Arial";
		appearance3.FontData.SizeInPoints = 9f;
		appearance3.Image = resources.GetObject("appearance3.Image");
		this.BtnAdd.Appearance = appearance3;
		this.BtnAdd.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.BtnAdd.Location = new System.Drawing.Point(129, 3);
		this.BtnAdd.Name = "BtnAdd";
		this.BtnAdd.Size = new System.Drawing.Size(68, 28);
		this.BtnAdd.SupportThemes = false;
		this.BtnAdd.TabIndex = 2;
		this.BtnAdd.Text = "加入";
		this.BtnAdd.Click += new System.EventHandler(BtnAdd_Click);
		this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.splitter2.Location = new System.Drawing.Point(0, 246);
		this.splitter2.Name = "splitter2";
		this.splitter2.Size = new System.Drawing.Size(369, 5);
		this.splitter2.TabIndex = 2;
		this.splitter2.TabStop = false;
		this.panel5.Controls.Add(this.gridProjectUsr);
		this.panel5.Controls.Add(this.ultraLabel3);
		this.panel5.Controls.Add(this.gridBudget1);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel5.Location = new System.Drawing.Point(0, 251);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(369, 204);
		this.panel5.TabIndex = 3;
		this.gridProjectUsr._ExcelFileName = "";
		this.gridProjectUsr._ExcelSheeName = "";
		this.gridProjectUsr._IsOpenExcelAfterExport = false;
		this.gridProjectUsr.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
		this.gridProjectUsr.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridProjectUsr.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
		this.gridProjectUsr.ColumnInfo = resources.GetString("gridProjectUsr.ColumnInfo");
		this.gridProjectUsr.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridProjectUsr.ExtendLastCol = true;
		this.gridProjectUsr.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridProjectUsr.ForeColor = System.Drawing.Color.Black;
		this.gridProjectUsr.Location = new System.Drawing.Point(0, 28);
		this.gridProjectUsr.Name = "gridProjectUsr";
		this.gridProjectUsr.Rows.Count = 1;
		this.gridProjectUsr.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.gridProjectUsr.ShowCursor = true;
		this.gridProjectUsr.ShowToolTipOnNarrowColumn = true;
		this.gridProjectUsr.Size = new System.Drawing.Size(369, 176);
		this.gridProjectUsr.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridProjectUsr.Styles"));
		this.gridProjectUsr.TabIndex = 4;
		this.gridProjectUsr.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(gridProjectUsr_AfterEdit);
		appearance4.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance4.BackColor2 = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance4.ForeColor = System.Drawing.Color.White;
		appearance4.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel3.Appearance = appearance4;
		this.ultraLabel3.Dock = System.Windows.Forms.DockStyle.Top;
		this.ultraLabel3.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(369, 28);
		this.ultraLabel3.TabIndex = 3;
		this.ultraLabel3.Text = " 使用者專案列表";
		this.gridBudget1._ExcelFileName = "";
		this.gridBudget1._ExcelSheeName = "";
		this.gridBudget1._IsOpenExcelAfterExport = false;
		this.gridBudget1.AllowEditing = false;
		this.gridBudget1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridBudget1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
		this.gridBudget1.ColumnInfo = resources.GetString("gridBudget1.ColumnInfo");
		this.gridBudget1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridBudget1.ExtendLastCol = true;
		this.gridBudget1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridBudget1.ForeColor = System.Drawing.Color.Black;
		this.gridBudget1.Location = new System.Drawing.Point(0, 0);
		this.gridBudget1.Name = "gridBudget1";
		this.gridBudget1.Rows.Count = 1;
		this.gridBudget1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.gridBudget1.ShowCursor = true;
		this.gridBudget1.ShowToolTipOnNarrowColumn = true;
		this.gridBudget1.Size = new System.Drawing.Size(369, 204);
		this.gridBudget1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridBudget1.Styles"));
		this.gridBudget1.TabIndex = 4;
		this.splitter1.Location = new System.Drawing.Point(268, 0);
		this.splitter1.Name = "splitter1";
		this.splitter1.Size = new System.Drawing.Size(5, 457);
		this.splitter1.TabIndex = 2;
		this.splitter1.TabStop = false;
		this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel1.Controls.Add(this.GridUsers);
		this.panel1.Controls.Add(this.ultraLabel1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(268, 457);
		this.panel1.TabIndex = 0;
		this.GridUsers._ExcelFileName = "";
		this.GridUsers._ExcelSheeName = "";
		this.GridUsers._IsOpenExcelAfterExport = false;
		this.GridUsers.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
		this.GridUsers.AllowEditing = false;
		this.GridUsers.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.GridUsers.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
		this.GridUsers.ColumnInfo = resources.GetString("GridUsers.ColumnInfo");
		this.GridUsers.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GridUsers.ExtendLastCol = true;
		this.GridUsers.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.GridUsers.ForeColor = System.Drawing.Color.Black;
		this.GridUsers.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.GridUsers.IsProcessUndo = false;
		this.GridUsers.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.GridUsers.Location = new System.Drawing.Point(0, 28);
		this.GridUsers.Name = "GridUsers";
		this.GridUsers.Rows.Count = 1;
		this.GridUsers.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
		this.GridUsers.ShowCursor = true;
		this.GridUsers.ShowToolTipOnNarrowColumn = true;
		this.GridUsers.Size = new System.Drawing.Size(266, 427);
		this.GridUsers.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("GridUsers.Styles"));
		this.GridUsers.TabIndex = 9;
		this.GridUsers.UndoMax = 10;
		this.GridUsers.BeforeSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(GridUsers_BeforeSelChange);
		this.GridUsers.AfterSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(GridUsers_AfterSelChange);
		appearance5.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance5.BackColor2 = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance5.ForeColor = System.Drawing.Color.White;
		appearance5.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel1.Appearance = appearance5;
		this.ultraLabel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.ultraLabel1.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(266, 28);
		this.ultraLabel1.TabIndex = 1;
		this.ultraLabel1.Text = " 使用者列表";
		appearance6.BackColor = System.Drawing.SystemColors.Control;
		appearance6.FontData.SizeInPoints = 11f;
		this.ultraStatusBar1.Appearance = appearance6;
		this.ultraStatusBar1.Location = new System.Drawing.Point(0, 493);
		this.ultraStatusBar1.Name = "ultraStatusBar1";
		ultraStatusPanel1.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel1.Text = "資料筆數:";
		ultraStatusPanel1.Width = 200;
		ultraStatusPanel2.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel2.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		appearance7.TextHAlign = Infragistics.Win.HAlign.Right;
		ultraStatusPanel3.Appearance = appearance7;
		ultraStatusPanel3.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel3.Text = "客服電話:(02)2708-8090";
		ultraStatusPanel3.Width = 200;
		this.ultraStatusBar1.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[3] { ultraStatusPanel1, ultraStatusPanel2, ultraStatusPanel3 });
		this.ultraStatusBar1.Size = new System.Drawing.Size(644, 23);
		this.ultraStatusBar1.SupportThemes = false;
		this.ultraStatusBar1.TabIndex = 19;
		this.ultraStatusBar1.Text = "ultraStatusBar2";
		this.panel6.Controls.Add(this.BtnSwitchToProj);
		this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel6.Location = new System.Drawing.Point(0, 0);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(644, 36);
		this.panel6.TabIndex = 5;
		appearance8.FontData.Name = "Arial";
		appearance8.FontData.SizeInPoints = 9f;
		appearance8.Image = resources.GetObject("appearance8.Image");
		this.BtnSwitchToProj.Appearance = appearance8;
		this.BtnSwitchToProj.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnSwitchToProj.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.BtnSwitchToProj.Location = new System.Drawing.Point(10, 4);
		this.BtnSwitchToProj.Name = "BtnSwitchToProj";
		this.BtnSwitchToProj.Size = new System.Drawing.Size(174, 28);
		this.BtnSwitchToProj.SupportThemes = false;
		this.BtnSwitchToProj.TabIndex = 3;
		this.BtnSwitchToProj.Text = "切換至專案管理模式";
		this.BtnSwitchToProj.Click += new System.EventHandler(BtnSwitchToProj_Click);
		this.Tab_B.Controls.Add(this.panel9);
		this.Tab_B.Controls.Add(this.ultraStatusBar2);
		this.Tab_B.Controls.Add(this.panel8);
		this.Tab_B.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_B.Name = "Tab_B";
		this.Tab_B.Size = new System.Drawing.Size(644, 516);
		this.panel9.Controls.Add(this.panel10);
		this.panel9.Controls.Add(this.splitter3);
		this.panel9.Controls.Add(this.panel11);
		this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel9.Location = new System.Drawing.Point(0, 36);
		this.panel9.Name = "panel9";
		this.panel9.Size = new System.Drawing.Size(644, 457);
		this.panel9.TabIndex = 21;
		this.panel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel10.Controls.Add(this.GridProjects);
		this.panel10.Controls.Add(this.ultraLabel4);
		this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel10.Location = new System.Drawing.Point(0, 0);
		this.panel10.Name = "panel10";
		this.panel10.Size = new System.Drawing.Size(268, 457);
		this.panel10.TabIndex = 1;
		this.GridProjects._ExcelFileName = "";
		this.GridProjects._ExcelSheeName = "";
		this.GridProjects._IsOpenExcelAfterExport = false;
		this.GridProjects.AllowEditing = false;
		this.GridProjects.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.GridProjects.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
		this.GridProjects.ColumnInfo = resources.GetString("GridProjects.ColumnInfo");
		this.GridProjects.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GridProjects.ExtendLastCol = true;
		this.GridProjects.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.GridProjects.ForeColor = System.Drawing.Color.Black;
		this.GridProjects.Location = new System.Drawing.Point(0, 28);
		this.GridProjects.Name = "GridProjects";
		this.GridProjects.Rows.Count = 1;
		this.GridProjects.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
		this.GridProjects.ShowCursor = true;
		this.GridProjects.ShowToolTipOnNarrowColumn = true;
		this.GridProjects.Size = new System.Drawing.Size(266, 427);
		this.GridProjects.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("GridProjects.Styles"));
		this.GridProjects.TabIndex = 4;
		this.GridProjects.BeforeSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(GridProjects_BeforeSelChange);
		this.GridProjects.AfterSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(GridProjects_AfterSelChange);
		appearance9.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance9.BackColor2 = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance9.ForeColor = System.Drawing.Color.White;
		appearance9.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel4.Appearance = appearance9;
		this.ultraLabel4.Dock = System.Windows.Forms.DockStyle.Top;
		this.ultraLabel4.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(266, 28);
		this.ultraLabel4.TabIndex = 1;
		this.ultraLabel4.Text = " 專案列表";
		this.splitter3.Dock = System.Windows.Forms.DockStyle.Right;
		this.splitter3.Location = new System.Drawing.Point(268, 0);
		this.splitter3.Name = "splitter3";
		this.splitter3.Size = new System.Drawing.Size(5, 457);
		this.splitter3.TabIndex = 3;
		this.splitter3.TabStop = false;
		this.panel11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel11.Controls.Add(this.panel12);
		this.panel11.Controls.Add(this.panel13);
		this.panel11.Controls.Add(this.splitter4);
		this.panel11.Controls.Add(this.panel14);
		this.panel11.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel11.Location = new System.Drawing.Point(273, 0);
		this.panel11.Name = "panel11";
		this.panel11.Size = new System.Drawing.Size(371, 457);
		this.panel11.TabIndex = 4;
		this.panel11.Resize += new System.EventHandler(panel11_Resize);
		this.panel12.Controls.Add(this.gridUserAll);
		this.panel12.Controls.Add(this.ultraLabel5);
		this.panel12.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel12.Location = new System.Drawing.Point(0, 0);
		this.panel12.Name = "panel12";
		this.panel12.Size = new System.Drawing.Size(369, 214);
		this.panel12.TabIndex = 0;
		this.gridUserAll._ExcelFileName = "";
		this.gridUserAll._ExcelSheeName = "";
		this.gridUserAll._IsOpenExcelAfterExport = false;
		this.gridUserAll.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
		this.gridUserAll.AllowEditing = false;
		this.gridUserAll.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridUserAll.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
		this.gridUserAll.ColumnInfo = resources.GetString("gridUserAll.ColumnInfo");
		this.gridUserAll.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridUserAll.ExtendLastCol = true;
		this.gridUserAll.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridUserAll.ForeColor = System.Drawing.Color.Black;
		this.gridUserAll.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.gridUserAll.IsProcessUndo = false;
		this.gridUserAll.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.gridUserAll.Location = new System.Drawing.Point(0, 28);
		this.gridUserAll.Name = "gridUserAll";
		this.gridUserAll.Rows.Count = 1;
		this.gridUserAll.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.gridUserAll.ShowCursor = true;
		this.gridUserAll.ShowToolTipOnNarrowColumn = true;
		this.gridUserAll.Size = new System.Drawing.Size(369, 186);
		this.gridUserAll.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridUserAll.Styles"));
		this.gridUserAll.TabIndex = 10;
		this.gridUserAll.UndoMax = 10;
		appearance10.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance10.BackColor2 = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance10.ForeColor = System.Drawing.Color.White;
		appearance10.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel5.Appearance = appearance10;
		this.ultraLabel5.Dock = System.Windows.Forms.DockStyle.Top;
		this.ultraLabel5.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(369, 28);
		this.ultraLabel5.TabIndex = 2;
		this.ultraLabel5.Text = " 系統所有使用者列表";
		this.panel13.Controls.Add(this.BtnRemove2);
		this.panel13.Controls.Add(this.BtnAdd2);
		this.panel13.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel13.Location = new System.Drawing.Point(0, 214);
		this.panel13.Name = "panel13";
		this.panel13.Size = new System.Drawing.Size(369, 32);
		this.panel13.TabIndex = 1;
		this.panel13.Resize += new System.EventHandler(panel13_Resize);
		appearance11.FontData.Name = "Arial";
		appearance11.FontData.SizeInPoints = 9f;
		appearance11.Image = resources.GetObject("appearance11.Image");
		this.BtnRemove2.Appearance = appearance11;
		this.BtnRemove2.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnRemove2.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.BtnRemove2.Location = new System.Drawing.Point(202, 3);
		this.BtnRemove2.Name = "BtnRemove2";
		this.BtnRemove2.Size = new System.Drawing.Size(68, 28);
		this.BtnRemove2.SupportThemes = false;
		this.BtnRemove2.TabIndex = 3;
		this.BtnRemove2.Text = "移除";
		this.BtnRemove2.Click += new System.EventHandler(BtnRemove2_Click);
		appearance12.FontData.Name = "Arial";
		appearance12.FontData.SizeInPoints = 9f;
		appearance12.Image = resources.GetObject("appearance12.Image");
		this.BtnAdd2.Appearance = appearance12;
		this.BtnAdd2.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnAdd2.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.BtnAdd2.Location = new System.Drawing.Point(129, 3);
		this.BtnAdd2.Name = "BtnAdd2";
		this.BtnAdd2.Size = new System.Drawing.Size(68, 28);
		this.BtnAdd2.SupportThemes = false;
		this.BtnAdd2.TabIndex = 2;
		this.BtnAdd2.Text = "加入";
		this.BtnAdd2.Click += new System.EventHandler(BtnAdd2_Click);
		this.splitter4.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.splitter4.Location = new System.Drawing.Point(0, 246);
		this.splitter4.Name = "splitter4";
		this.splitter4.Size = new System.Drawing.Size(369, 5);
		this.splitter4.TabIndex = 2;
		this.splitter4.TabStop = false;
		this.panel14.Controls.Add(this.gridUserProject);
		this.panel14.Controls.Add(this.ultraLabel6);
		this.panel14.Controls.Add(this.gridBudget4);
		this.panel14.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel14.Location = new System.Drawing.Point(0, 251);
		this.panel14.Name = "panel14";
		this.panel14.Size = new System.Drawing.Size(369, 204);
		this.panel14.TabIndex = 3;
		this.gridUserProject._ExcelFileName = "";
		this.gridUserProject._ExcelSheeName = "";
		this.gridUserProject._IsOpenExcelAfterExport = false;
		this.gridUserProject.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
		this.gridUserProject.AllowEditing = false;
		this.gridUserProject.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridUserProject.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
		this.gridUserProject.ColumnInfo = resources.GetString("gridUserProject.ColumnInfo");
		this.gridUserProject.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridUserProject.ExtendLastCol = true;
		this.gridUserProject.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridUserProject.ForeColor = System.Drawing.Color.Black;
		this.gridUserProject.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.gridUserProject.IsProcessUndo = false;
		this.gridUserProject.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.gridUserProject.Location = new System.Drawing.Point(0, 28);
		this.gridUserProject.Name = "gridUserProject";
		this.gridUserProject.Rows.Count = 1;
		this.gridUserProject.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.gridUserProject.ShowCursor = true;
		this.gridUserProject.ShowToolTipOnNarrowColumn = true;
		this.gridUserProject.Size = new System.Drawing.Size(369, 176);
		this.gridUserProject.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridUserProject.Styles"));
		this.gridUserProject.TabIndex = 10;
		this.gridUserProject.UndoMax = 10;
		appearance13.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance13.BackColor2 = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance13.ForeColor = System.Drawing.Color.White;
		appearance13.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraLabel6.Appearance = appearance13;
		this.ultraLabel6.Dock = System.Windows.Forms.DockStyle.Top;
		this.ultraLabel6.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(369, 28);
		this.ultraLabel6.TabIndex = 3;
		this.ultraLabel6.Text = " 專案使用者列表";
		this.gridBudget4._ExcelFileName = "";
		this.gridBudget4._ExcelSheeName = "";
		this.gridBudget4._IsOpenExcelAfterExport = false;
		this.gridBudget4.AllowEditing = false;
		this.gridBudget4.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridBudget4.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
		this.gridBudget4.ColumnInfo = resources.GetString("gridBudget4.ColumnInfo");
		this.gridBudget4.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridBudget4.ExtendLastCol = true;
		this.gridBudget4.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridBudget4.ForeColor = System.Drawing.Color.Black;
		this.gridBudget4.Location = new System.Drawing.Point(0, 0);
		this.gridBudget4.Name = "gridBudget4";
		this.gridBudget4.Rows.Count = 1;
		this.gridBudget4.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.gridBudget4.ShowCursor = true;
		this.gridBudget4.ShowToolTipOnNarrowColumn = true;
		this.gridBudget4.Size = new System.Drawing.Size(369, 204);
		this.gridBudget4.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridBudget4.Styles"));
		this.gridBudget4.TabIndex = 4;
		appearance14.BackColor = System.Drawing.SystemColors.Control;
		appearance14.FontData.SizeInPoints = 11f;
		this.ultraStatusBar2.Appearance = appearance14;
		this.ultraStatusBar2.Location = new System.Drawing.Point(0, 493);
		this.ultraStatusBar2.Name = "ultraStatusBar2";
		this.ultraStatusBar2.Padding = new Infragistics.Win.UltraWinStatusBar.UIElementMargins(0, 2, 0, 0);
		ultraStatusPanel4.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel4.Text = "資料筆數:";
		ultraStatusPanel4.Width = 200;
		ultraStatusPanel5.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel5.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		appearance15.TextHAlign = Infragistics.Win.HAlign.Right;
		ultraStatusPanel6.Appearance = appearance15;
		ultraStatusPanel6.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel6.Text = "客服電話:(02)2716-5561";
		ultraStatusPanel6.Width = 200;
		this.ultraStatusBar2.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[3] { ultraStatusPanel4, ultraStatusPanel5, ultraStatusPanel6 });
		this.ultraStatusBar2.Size = new System.Drawing.Size(644, 23);
		this.ultraStatusBar2.SupportThemes = false;
		this.ultraStatusBar2.TabIndex = 20;
		this.ultraStatusBar2.Text = "ultraStatusBar2";
		this.panel8.Controls.Add(this.ultraButton4);
		this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel8.Location = new System.Drawing.Point(0, 0);
		this.panel8.Name = "panel8";
		this.panel8.Size = new System.Drawing.Size(644, 36);
		this.panel8.TabIndex = 6;
		appearance16.FontData.Name = "Arial";
		appearance16.FontData.SizeInPoints = 9f;
		appearance16.Image = resources.GetObject("appearance16.Image");
		this.ultraButton4.Appearance = appearance16;
		this.ultraButton4.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.ultraButton4.Location = new System.Drawing.Point(10, 4);
		this.ultraButton4.Name = "ultraButton4";
		this.ultraButton4.Size = new System.Drawing.Size(174, 28);
		this.ultraButton4.SupportThemes = false;
		this.ultraButton4.TabIndex = 4;
		this.ultraButton4.Text = "切換至使用者管理模式";
		this.ultraButton4.Click += new System.EventHandler(ultraButton4_Click);
		this.Tab_Ctrl.Controls.Add(this.ultraTabSharedControlsPage1);
		this.Tab_Ctrl.Controls.Add(this.Tab_A);
		this.Tab_Ctrl.Controls.Add(this.Tab_B);
		this.Tab_Ctrl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Tab_Ctrl.Location = new System.Drawing.Point(0, 0);
		this.Tab_Ctrl.Name = "Tab_Ctrl";
		this.Tab_Ctrl.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.Tab_Ctrl.Size = new System.Drawing.Size(644, 516);
		this.Tab_Ctrl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
		this.Tab_Ctrl.TabIndex = 19;
		ultraTab1.TabPage = this.Tab_A;
		ultraTab1.Text = "tab1";
		ultraTab2.TabPage = this.Tab_B;
		ultraTab2.Text = "tab2";
		this.Tab_Ctrl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[2] { ultraTab1, ultraTab2 });
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(644, 516);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.Controls.Add(this.Tab_Ctrl);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.Name = "FormSys_I";
		base.Size = new System.Drawing.Size(644, 516);
		base.Load += new System.EventHandler(FormSys_I_Load);
		this.Tab_A.ResumeLayout(false);
		this.panel7.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridProjectAll).EndInit();
		this.panel4.ResumeLayout(false);
		this.panel5.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridProjectUsr).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridBudget1).EndInit();
		this.panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.GridUsers).EndInit();
		this.panel6.ResumeLayout(false);
		this.Tab_B.ResumeLayout(false);
		this.panel9.ResumeLayout(false);
		this.panel10.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.GridProjects).EndInit();
		this.panel11.ResumeLayout(false);
		this.panel12.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridUserAll).EndInit();
		this.panel13.ResumeLayout(false);
		this.panel14.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridUserProject).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gridBudget4).EndInit();
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
