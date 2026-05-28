using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.DatabaseAccess;
using Archnowledge.Pcces.DomainModule.Coms;
using Archnowledge.Pcces.PccesMain.Library;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTabControl;

namespace Archnowledge.Pcces.PccesMain.Budget.BudgetChange;

public class FormDataExport_Wzd : Form
{
	private string F_ProjectCode;

	private string F_DB;

	private string F_UserID;

	private string s_server = "";

	private string s_uid = "";

	private string s_pwd = "";

	private string s_dbname = "";

	private string FileINI = "";

	private Panel panel1;

	private UltraButton Next1;

	private UltraButton Cancel1;

	private Panel panel2;

	private Panel panel3;

	private UltraButton Next2;

	private UltraButton Cancel2;

	private UltraButton Prev2;

	private PictureBox pictureBox1;

	private UltraLabel Label1;

	private UltraLabel Label2;

	private UltraLabel Label3;

	private UltraLabel Label4;

	private UltraLabel Label5;

	private TextBox user_id;

	private UltraLabel Label6;

	private UltraLabel Label7;

	private TextBox password;

	private Panel panel4;

	private Panel panel5;

	private UltraLabel Label8;

	private UltraLabel Label9;

	private UltraLabel Label10;

	private UltraLabel Label11;

	private UltraButton Finish;

	private UltraTabPageControl Tab2;

	private UltraTabPageControl Tab3;

	private UltraTabPageControl Tab4;

	private UltraTabPageControl Tab1;

	private UltraTabControl Tab_ctrl;

	private UltraTabSharedControlsPage Tab_Control_Page;

	private UltraComboEditor server_id;

	private UltraComboEditor db_name;

	private Container components = null;

	private Button GetDB;

	private ComboBox c_version;

	private Label label12;

	private Label lbRemoteVersion;

	private Label label13;

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

	public string _F_DB
	{
		get
		{
			return F_DB;
		}
		set
		{
			F_DB = value;
		}
	}

	private string RemoteConnectionString => ConnectionStringUtility.GetSqlConnectionString(IsNTAuthoricate: false, s_server, s_dbname, s_uid, s_pwd, 30);

	public FormDataExport_Wzd()
	{
		InitializeComponent();
	}

	private void FormDataExport_Wzd_Load(object sender, EventArgs e)
	{
		string SQL = "";
		string PccesConnectionString = Archnowledge.Pcces.DatabaseAccess.DatabaseAccess.PccesConnectionString();
		ConnectionStringUtility connUtility = new ConnectionStringUtility(PccesConnectionString);
		string SourceConnectionString = connUtility.GetSqlConnectionString(F_DB);
		DataTable dtSchema = GetTableSchema(SourceConnectionString, "BudAnnex", "ProjectCode");
		c_version.Items.Clear();
		SQL = "select Version from BudExeProject where projectCode = '" + F_ProjectCode + "' order by Version";
		DataSet ds = SqlUtility.ExecDataSet(SourceConnectionString, SQL);
		DataTable dt = ds.Tables[0];
		if (dt.Rows.Count != 0)
		{
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				c_version.Items.Add(dt.Rows[i]["Version"].ToString().Trim());
			}
		}
		else
		{
			c_version.Items.Add("0");
		}
		c_version.SelectedIndex = 0;
		string AppLocation = CommonMethods.ExtractFilePath(Application.ExecutablePath);
		if (AppLocation.Substring(AppLocation.Length - 1) != "\\")
		{
			AppLocation += "\\";
		}
		FileINI = AppLocation + "Addon.ini";
		server_id.Items.Clear();
		s_server = CommonMethods.IniReadValue(FileINI, "SERVER", "SERVER1");
		if (s_server.Trim() != "")
		{
			server_id.Items.Add(s_server, s_server);
		}
		server_id.SelectedIndex = 0;
		s_uid = CommonMethods.IniReadValue(FileINI, "UID", "ID1");
		s_pwd = CommonMethods.IniReadValue(FileINI, "PWD", "PWD1");
		s_dbname = CommonMethods.IniReadValue(FileINI, "DB", "DBNAME1");
		db_name.Enabled = false;
		SetRemoteVersion(RemoteConnectionString);
	}

	private void Next1_Click(object sender, EventArgs e)
	{
		Label2.Text = "偵測中，請稍候...";
		Label3.Text = "";
		label12.Text = "";
		c_version.Visible = false;
		Application.DoEvents();
		if (!CheckDatabaseExist(s_server, s_dbname, s_uid, s_pwd))
		{
			Tab2.Tab.Selected = true;
		}
		else if (MessageBox.Show(this, "請注意：執行此功能時，請先確定此專案已經重新總計過了，以確保匯出的金額是正確的。\n\n確定執行資料匯出至 " + s_server + " (" + s_dbname + " ) 嗎? ", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			if (CheckPreDataExist())
			{
				Export();
				return;
			}
			Label2.Text = "資料匯出精靈可幫助您將資料移轉至伺服器";
			Label3.Text = "接下來它會逐步引導您完成資料轉換的工作";
			label12.Text = "請選擇要移轉的次別：";
			c_version.Visible = true;
		}
		else
		{
			Label2.Text = "資料匯出精靈可幫助您將資料移轉至伺服器";
			Label3.Text = "接下來它會逐步引導您完成資料轉換的工作";
			label12.Text = "請選擇要移轉的次別：";
			c_version.Visible = true;
		}
	}

	private void SetRemoteVersion(string TargetConnectionString)
	{
		string SQL = "select count(*) as rows from BudExeProject where projectCode = '" + F_ProjectCode + "'";
		int BudExeDataCount = GetDataCount(TargetConnectionString, SQL);
		SQL = "Select Count(*) as rows from BudItemA  where projectCode = '" + F_ProjectCode + "'";
		int BudDataCount = GetDataCount(TargetConnectionString, SQL);
		if (BudExeDataCount > 0)
		{
			lbRemoteVersion.Text = (BudExeDataCount - 1).ToString();
		}
		else if (BudDataCount > 0)
		{
			lbRemoteVersion.Text = "0";
		}
		else
		{
			lbRemoteVersion.Text = "無";
		}
	}

	public bool CheckPreDataExist()
	{
		int RemoteVersion = -1;
		if (lbRemoteVersion.Text != "無")
		{
			RemoteVersion = ArchConvert.Obj2Int(lbRemoteVersion.Text);
		}
		int SelectedVersion = int.Parse(c_version.SelectedItem.ToString().Trim());
		if (RemoteVersion == SelectedVersion - 1)
		{
			return true;
		}
		int AllowVersion = RemoteVersion + 1;
		if (RemoteVersion < SelectedVersion - 1)
		{
			MessageBox.Show("選擇的版本太大，你目前只可以匯出[ " + AllowVersion + " ]版", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else
		{
			MessageBox.Show("此動作將導致資料不完整!\n\n你目前只可以匯出[ " + AllowVersion + " ]版", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		return false;
	}

	private bool CheckDatabaseExist(string Server, string Database, string UserID, string Password)
	{
		bool ConnectionFlag = false;
		string ConnectionString = ConnectionStringUtility.GetSqlConnectionString(IsNTAuthoricate: false, Server, Database, UserID, Password, 30);
		SqlConnection theConnection = new SqlConnection(ConnectionString);
		try
		{
			theConnection.Open();
			ConnectionFlag = true;
		}
		catch (Exception ex)
		{
			MessageBox.Show("資料庫連接失敗，請輸入相關資訊再繼續執行! " + ex.Message, "訊息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		finally
		{
			if (theConnection.State == ConnectionState.Open)
			{
				theConnection.Close();
			}
			theConnection.Dispose();
			theConnection = null;
		}
		return ConnectionFlag;
	}

	private void Cancel1_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void Prev2_Click(object sender, EventArgs e)
	{
		Tab1.Tab.Selected = true;
	}

	private void Next2_Click(object sender, EventArgs e)
	{
		bool ConnectionFlag = false;
		if (server_id.Value.ToString().Trim() == "")
		{
			MessageBox.Show("請選擇伺服器!", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else if (user_id.Text.Trim() == "")
		{
			MessageBox.Show("請輸入使用者名稱!", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else if (db_name.Value.ToString().Trim() == "")
		{
			MessageBox.Show("請選擇資料庫!", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else
		{
			s_server = server_id.Value.ToString().Trim();
			s_pwd = password.Text.Trim();
			s_dbname = db_name.Value.ToString().Trim();
			s_uid = user_id.Text.Trim();
			CommonMethods.IniWriteValue(FileINI, "SERVER", "SERVER1", s_server);
			CommonMethods.IniWriteValue(FileINI, "UID", "ID1", s_uid);
			CommonMethods.IniWriteValue(FileINI, "PWD", "PWD1", s_pwd);
			CommonMethods.IniWriteValue(FileINI, "DB", "DBNAME1", s_dbname);
			ConnectionFlag = CheckDatabaseExist(s_server, s_dbname, s_uid, s_pwd);
		}
		if (ConnectionFlag)
		{
			Tab1.Tab.Selected = true;
			Tab1.Tab.Visible = true;
		}
	}

	private string GetLoginID(string TargetConnectionString)
	{
		try
		{
			string SQL = "Select * from ComsLogInPcces";
			object Value = SqlUtility.ExecScale(TargetConnectionString, SQL);
			if (Value != DBNull.Value && Value != null)
			{
				return Value.ToString();
			}
			Archnowledge.Common.DebugUtil.OutputDebugString("在 ComsLogInPcces 找不到資料。");
			return null;
		}
		catch
		{
		}
		return null;
	}

	private void Export()
	{
		Tab3.Tab.Selected = true;
		Application.DoEvents();
		string PccesConnectionString = Archnowledge.Pcces.DatabaseAccess.DatabaseAccess.PccesConnectionString();
		ConnectionStringUtility connUtility = new ConnectionStringUtility(PccesConnectionString);
		string SourceConnectionString = connUtility.GetSqlConnectionString(F_DB);
		string TargetConnectionString = RemoteConnectionString;
		if (CheckDataExist(TargetConnectionString))
		{
			MessageBox.Show("資料已存在!\n\n要取代原資料，請洽詢系統管理員!", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else
		{
			ExecResult ER = DoTransfer(SourceConnectionString, TargetConnectionString);
			if (ER.ReturnCode == 0)
			{
				Cursor = Cursors.WaitCursor;
				ProjectServiceHelper theProjectServiceHelper = new ProjectServiceHelper(ForceEnable: true);
				if (theProjectServiceHelper.CheckProjectCodeExist(F_ProjectCode, out ER))
				{
					ComsWebService theComsWebService = new ComsWebService(F_ProjectCode);
					ER = theComsWebService.ExpandBudgetInCOMS(ForceEnable: true);
				}
				else
				{
					string SQL = "Update BudProject Set isType =3 Where projectCode = '" + F_ProjectCode + "'";
					SqlUtility.ExecSQL(TargetConnectionString, SQL);
				}
			}
			if (ER.ReturnCode != 0)
			{
				MessageBox.Show(ER.Message, "警示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		Cursor = Cursors.Default;
		Tab4.Tab.Selected = true;
	}

	private void Cancel2_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void Finish_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void GetDB_Click(object sender, EventArgs e)
	{
		bool ConnectionFlag = false;
		if (server_id.Value.ToString().Trim() == "")
		{
			MessageBox.Show("請選擇伺服器!", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else if (user_id.Text.Trim() == "")
		{
			MessageBox.Show("請輸入使用者名稱!", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else
		{
			ConnectionFlag = true;
		}
		if (!ConnectionFlag)
		{
			return;
		}
		ConnectionFlag = false;
		try
		{
			string SQL = "Select name from master.dbo.sysdatabases order by name";
			string TargetConnectionString = ConnectionStringUtility.GetSqlConnectionString(IsNTAuthoricate: false, server_id.Value.ToString().Trim(), "master", user_id.Text.Trim(), password.Text.Trim(), 30);
			DataSet ds = SqlUtility.ExecDataSet(TargetConnectionString, SQL);
			db_name.Items.Clear();
			for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
			{
				if (ds.Tables[0].Rows[i]["name"] != DBNull.Value && ds.Tables[0].Rows[i]["name"].ToString().Trim() != "")
				{
					db_name.Items.Add(ds.Tables[0].Rows[i]["name"].ToString().Trim(), ds.Tables[0].Rows[i]["name"].ToString().Trim());
				}
			}
			db_name.SelectedIndex = 0;
			db_name.Enabled = true;
		}
		catch (Exception ex)
		{
			MessageBox.Show("連線失敗，請檢查伺服器、使用者名稱或密碼是否正確!" + ex.Message, "訊息", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	public bool CheckDataExist(string TargetConnectionString)
	{
		bool existflag = false;
		string SQL = "";
		int SelectedVersion = int.Parse(c_version.SelectedItem.ToString().Trim());
		SQL = "select count(*) as rows from BudExeProject where projectCode = '" + F_ProjectCode + "' and version = '" + SelectedVersion + "'";
		int BudExeDataCount = GetDataCount(TargetConnectionString, SQL);
		SQL = "Select Count(*) as rows from BudItemA  where projectCode = '" + F_ProjectCode + "'";
		int BudDataCount = GetDataCount(TargetConnectionString, SQL);
		int TargetVersionCount = ((BudExeDataCount != 0 || SelectedVersion != 0) ? BudExeDataCount : BudDataCount);
		if (TargetVersionCount > 0)
		{
			existflag = true;
		}
		return existflag;
	}

	public void DeleteProject(string TargetConnectionString, string Version)
	{
		string BudExePrefix = "Bud";
		if (Version != "")
		{
			BudExePrefix = "BudExe";
		}
		string SQL = "";
		SQL = "Delete From " + BudExePrefix + "Annex Where projectCode = '" + F_ProjectCode + "'";
		if (Version != "")
		{
			SQL = SQL + " and version = '" + Version + "'";
		}
		SqlUtility.ExecSQL(TargetConnectionString, SQL);
		SQL = "Delete From " + BudExePrefix + "CostKind Where projectCode = '" + F_ProjectCode + "'";
		if (Version != "")
		{
			SQL = SQL + " and version = '" + Version + "'";
		}
		SqlUtility.ExecSQL(TargetConnectionString, SQL);
		SQL = "Delete From " + BudExePrefix + "ItemA Where projectCode = '" + F_ProjectCode + "'";
		if (Version != "")
		{
			SQL = SQL + " and version = '" + Version + "'";
		}
		SqlUtility.ExecSQL(TargetConnectionString, SQL);
		SQL = "Delete From " + BudExePrefix + "ItemB Where projectCode = '" + F_ProjectCode + "'";
		if (Version != "")
		{
			SQL = SQL + " and version = '" + Version + "'";
		}
		SqlUtility.ExecSQL(TargetConnectionString, SQL);
		SQL = "Delete From " + BudExePrefix + "ItemC Where projectCode = '" + F_ProjectCode + "'";
		if (Version != "")
		{
			SQL = SQL + " and version = '" + Version + "'";
		}
		SqlUtility.ExecSQL(TargetConnectionString, SQL);
		SQL = "Delete From BudExePageBreak Where projectCode = '" + F_ProjectCode + "'";
		if (Version != "")
		{
			SQL = SQL + " and version = '" + Version + "'";
		}
		SqlUtility.ExecSQL(TargetConnectionString, SQL);
		SQL = "Delete From BudExePCalsCustomVar Where projectCode = '" + F_ProjectCode + "'";
		if (Version != "")
		{
			SQL = SQL + " and version = '" + Version + "'";
		}
		SqlUtility.ExecSQL(TargetConnectionString, SQL);
		SQL = "Delete From " + BudExePrefix + "Project Where projectCode = '" + F_ProjectCode + "'";
		if (Version != "")
		{
			SQL = SQL + " and version = '" + Version + "'";
		}
		SqlUtility.ExecSQL(TargetConnectionString, SQL);
		SQL = "Delete From " + BudExePrefix + "ProjMrsA Where projectCode = '" + F_ProjectCode + "'";
		if (Version != "")
		{
			SQL = SQL + " and version = '" + Version + "'";
		}
		SqlUtility.ExecSQL(TargetConnectionString, SQL);
		SQL = "Delete From " + BudExePrefix + "ProjMrsB Where projectCode = '" + F_ProjectCode + "'";
		if (Version != "")
		{
			SQL = SQL + " and version = '" + Version + "'";
		}
		SqlUtility.ExecSQL(TargetConnectionString, SQL);
		SQL = "Delete From " + BudExePrefix + "ProjMrsC Where projectCode = '" + F_ProjectCode + "'";
		if (Version != "")
		{
			SQL = SQL + " and version = '" + Version + "'";
		}
		SqlUtility.ExecSQL(TargetConnectionString, SQL);
	}

	private int GetDataCount(string ConnectionString, string SQL)
	{
		int Count = 0;
		object Value = SqlUtility.ExecScale(ConnectionString, SQL);
		if (Value != DBNull.Value)
		{
			Count = ArchConvert.Obj2Int(Value);
		}
		return Count;
	}

	public DataTable GetTableSchema(string ConnectionString, string TableName, string ColumnName)
	{
		DataTable SchemaTable = null;
		try
		{
			using SqlConnection myConnection = new SqlConnection(ConnectionString);
			myConnection.Open();
			string SQL = "Select * from " + TableName + " where " + ColumnName + "=''";
			SqlCommand cmd = new SqlCommand(SQL, myConnection);
			SqlDataReader rd = cmd.ExecuteReader();
			SchemaTable = rd.GetSchemaTable();
			rd.Close();
			return SchemaTable;
		}
		catch (Exception ex)
		{
			string Message = "ExecuteSPScale 失敗，錯誤訊息：" + ex.Message;
			Archnowledge.Common.DebugUtil.OutputDebugString(Message);
			Exception exp = new Exception(Message);
			throw exp;
		}
	}

	private string ReplaceReserveWord(string Value)
	{
		return Value.Replace("'", "''");
	}

	private void Transfer2DB(string TableName, string SourceConnectionString, string TargetConnectionString, bool SourceIsBudExe, bool TargetIsBudExe, string Version)
	{
		Transfer2DB(TableName, SourceConnectionString, TargetConnectionString, SourceIsBudExe, TargetIsBudExe, Version, AddPrefix: true);
	}

	private void Transfer2DB(string TableName, string SourceConnectionString, string TargetConnectionString, bool SourceIsBudExe, bool TargetIsBudExe, string Version, bool AddPrefix)
	{
		string SourceTableName = TableName;
		string TargetTableName = TableName;
		if (AddPrefix)
		{
			SourceTableName = "Bud" + TableName;
			if (SourceIsBudExe)
			{
				SourceTableName = "BudExe" + TableName;
			}
			TargetTableName = "Bud" + TableName;
			if (TargetIsBudExe)
			{
				TargetTableName = "BudExe" + TableName;
			}
		}
		string SourceSQL = " select * from " + SourceTableName + " where projectCode = '" + F_ProjectCode + "' ";
		if (SourceIsBudExe)
		{
			SourceSQL = SourceSQL + " and version = '" + Version + "' ";
		}
		DataTable TargetSchema = GetTableSchema(TargetConnectionString, TargetTableName, "ProjectCode");
		DataSet ds = SqlUtility.ExecDataSet(SourceConnectionString, SourceSQL);
		DataTable dtSource = ds.Tables[0];
		if (dtSource.Rows.Count <= 0)
		{
			return;
		}
		StringBuilder SB = new StringBuilder();
		StringBuilder PreSB = new StringBuilder();
		PreSB.Append("Insert into " + TargetTableName + " (");
		bool IsFirstColumn = true;
		foreach (DataRow theSchemaRow in TargetSchema.Rows)
		{
			string ColumnName = theSchemaRow["ColumnName"].ToString();
			if (!ArchConvert.Obj2Bool(theSchemaRow["IsIdentity"]) && dtSource.Columns.IndexOf(ColumnName) != -1)
			{
				if (IsFirstColumn)
				{
					IsFirstColumn = false;
				}
				else
				{
					PreSB.Append(",");
				}
				PreSB.Append(theSchemaRow["ColumnName"].ToString());
			}
		}
		PreSB.Append(") values (");
		try
		{
			foreach (DataRow theRow in dtSource.Rows)
			{
				SB.Length = 0;
				SB.Append(PreSB.ToString());
				IsFirstColumn = true;
				foreach (DataRow theSchemaRow in TargetSchema.Rows)
				{
					string ColumnName = theSchemaRow["ColumnName"].ToString();
					if (ArchConvert.Obj2Bool(theSchemaRow["IsIdentity"]) || dtSource.Columns.IndexOf(ColumnName) == -1)
					{
						continue;
					}
					if (!IsFirstColumn)
					{
						SB.Append(",");
					}
					else
					{
						IsFirstColumn = false;
					}
					if (theRow[ColumnName] != DBNull.Value)
					{
						string DataType = theSchemaRow["DataType"].ToString();
						if (DataType == "System.DateTime")
						{
							SB.Append("'" + ArchConvert.Obj2DateTimeString(theRow[ColumnName], "yyyy/MM/dd hh:mm:ss") + "'");
						}
						else if (DataType == "System.Boolean")
						{
							if (ArchConvert.Obj2Bool(theRow[ColumnName]))
							{
								SB.Append("1");
							}
							else
							{
								SB.Append("0");
							}
						}
						else
						{
							string Value = ReplaceReserveWord(ArchConvert.Obj2String(theRow[ColumnName]).Trim());
							SB.Append("'" + Value + "'");
						}
					}
					else
					{
						SB.Append("null");
					}
				}
				SB.Append(")");
				SqlUtility.ExecSQL(TargetConnectionString, SB.ToString());
			}
		}
		catch (Exception ex)
		{
			Archnowledge.Common.DebugUtil.OutputDebugString("SQL =" + SB.ToString() + ", Error=" + ex.Message);
			MessageBox.Show("TargetTableName=" + TargetTableName + "資料上載失敗，可能是版本不一致造成，Error=" + ex.Message, "訊息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			Exception exp = new Exception(ex.Message);
			throw exp;
		}
	}

	public ExecResult DoTransfer(string SourceConnectionString, string TargetConnectionString)
	{
		ExecResult ER = new ExecResult();
		string Version = int.Parse(c_version.SelectedItem.ToString().Trim()).ToString();
		bool IsBudExe = true;
		if (Version == (c_version.Items.Count - 1).ToString())
		{
			IsBudExe = false;
		}
		string SQL = "\r\nDelete From PubProject Where projectCode = '" + F_ProjectCode + "'\r\nDelete PubDecimal where  projectCode ='" + F_ProjectCode + "'";
		SqlUtility.ExecSQL(TargetConnectionString, SQL);
		DeleteProject(TargetConnectionString, "");
		DeleteProject(TargetConnectionString, Version);
		Transfer2DB("PubProject", SourceConnectionString, TargetConnectionString, SourceIsBudExe: false, TargetIsBudExe: false, Version, AddPrefix: false);
		Transfer2DB("PubDecimal", SourceConnectionString, TargetConnectionString, SourceIsBudExe: false, TargetIsBudExe: false, Version, AddPrefix: false);
		try
		{
			if (!IsBudExe)
			{
				if (Version == "0")
				{
					Transfer2DB("Project", SourceConnectionString, TargetConnectionString, IsBudExe, IsBudExe, Version);
					Transfer2DB("ProjMrsA", SourceConnectionString, TargetConnectionString, IsBudExe, IsBudExe, Version);
					Transfer2DB("ProjMrsB", SourceConnectionString, TargetConnectionString, IsBudExe, IsBudExe, Version);
					Transfer2DB("ProjMrsC", SourceConnectionString, TargetConnectionString, IsBudExe, IsBudExe, Version);
					Transfer2DB("Annex", SourceConnectionString, TargetConnectionString, IsBudExe, IsBudExe, Version);
					Transfer2DB("CostKind", SourceConnectionString, TargetConnectionString, IsBudExe, IsBudExe, Version);
					Transfer2DB("ItemA", SourceConnectionString, TargetConnectionString, IsBudExe, IsBudExe, Version);
					Transfer2DB("ItemB", SourceConnectionString, TargetConnectionString, IsBudExe, IsBudExe, Version);
					Transfer2DB("ItemC", SourceConnectionString, TargetConnectionString, IsBudExe, IsBudExe, Version);
					Transfer2DB("PageBreak", SourceConnectionString, TargetConnectionString, IsBudExe, IsBudExe, Version);
					Transfer2DB("PCalsCustomVar", SourceConnectionString, TargetConnectionString, IsBudExe, IsBudExe, Version);
				}
				else
				{
					string PreVersion = (int.Parse(c_version.SelectedItem.ToString().Trim()) - 1).ToString();
					DeleteProject(TargetConnectionString, PreVersion);
					Transfer2DB("Project", SourceConnectionString, TargetConnectionString, !IsBudExe, !IsBudExe, PreVersion);
					Transfer2DB("ProjMrsA", SourceConnectionString, TargetConnectionString, !IsBudExe, !IsBudExe, PreVersion);
					Transfer2DB("ProjMrsB", SourceConnectionString, TargetConnectionString, !IsBudExe, !IsBudExe, PreVersion);
					Transfer2DB("ProjMrsC", SourceConnectionString, TargetConnectionString, !IsBudExe, !IsBudExe, PreVersion);
					Transfer2DB("Annex", SourceConnectionString, TargetConnectionString, !IsBudExe, !IsBudExe, PreVersion);
					Transfer2DB("CostKind", SourceConnectionString, TargetConnectionString, !IsBudExe, !IsBudExe, PreVersion);
					Transfer2DB("ItemA", SourceConnectionString, TargetConnectionString, !IsBudExe, !IsBudExe, PreVersion);
					Transfer2DB("ItemB", SourceConnectionString, TargetConnectionString, !IsBudExe, !IsBudExe, PreVersion);
					Transfer2DB("ItemC", SourceConnectionString, TargetConnectionString, !IsBudExe, !IsBudExe, PreVersion);
					Transfer2DB("PageBreak", SourceConnectionString, TargetConnectionString, !IsBudExe, !IsBudExe, PreVersion);
					Transfer2DB("PCalsCustomVar", SourceConnectionString, TargetConnectionString, !IsBudExe, !IsBudExe, PreVersion);
					Transfer2DB("Project", SourceConnectionString, TargetConnectionString, IsBudExe, IsBudExe, Version);
					Transfer2DB("ProjMrsA", SourceConnectionString, TargetConnectionString, IsBudExe, IsBudExe, Version);
					Transfer2DB("ProjMrsB", SourceConnectionString, TargetConnectionString, IsBudExe, IsBudExe, Version);
					Transfer2DB("ProjMrsC", SourceConnectionString, TargetConnectionString, IsBudExe, IsBudExe, Version);
					Transfer2DB("Annex", SourceConnectionString, TargetConnectionString, IsBudExe, IsBudExe, Version);
					Transfer2DB("CostKind", SourceConnectionString, TargetConnectionString, IsBudExe, IsBudExe, Version);
					Transfer2DB("ItemA", SourceConnectionString, TargetConnectionString, IsBudExe, IsBudExe, Version);
					Transfer2DB("ItemB", SourceConnectionString, TargetConnectionString, IsBudExe, IsBudExe, Version);
					Transfer2DB("ItemC", SourceConnectionString, TargetConnectionString, IsBudExe, IsBudExe, Version);
					Transfer2DB("PageBreak", SourceConnectionString, TargetConnectionString, IsBudExe, IsBudExe, Version);
					Transfer2DB("PCalsCustomVar", SourceConnectionString, TargetConnectionString, IsBudExe, IsBudExe, Version);
					Transfer2DB("Project", SourceConnectionString, TargetConnectionString, !IsBudExe, !IsBudExe, Version);
				}
			}
			else if (Version == "0")
			{
				Transfer2DB("Project", SourceConnectionString, TargetConnectionString, IsBudExe, !IsBudExe, Version);
				Transfer2DB("ProjMrsA", SourceConnectionString, TargetConnectionString, IsBudExe, !IsBudExe, Version);
				Transfer2DB("ProjMrsB", SourceConnectionString, TargetConnectionString, IsBudExe, !IsBudExe, Version);
				Transfer2DB("ProjMrsC", SourceConnectionString, TargetConnectionString, IsBudExe, !IsBudExe, Version);
				Transfer2DB("Annex", SourceConnectionString, TargetConnectionString, IsBudExe, !IsBudExe, Version);
				Transfer2DB("CostKind", SourceConnectionString, TargetConnectionString, IsBudExe, !IsBudExe, Version);
				Transfer2DB("ItemA", SourceConnectionString, TargetConnectionString, IsBudExe, !IsBudExe, Version);
				Transfer2DB("ItemB", SourceConnectionString, TargetConnectionString, IsBudExe, !IsBudExe, Version);
				Transfer2DB("ItemC", SourceConnectionString, TargetConnectionString, IsBudExe, !IsBudExe, Version);
				Transfer2DB("PageBreak", SourceConnectionString, TargetConnectionString, IsBudExe, !IsBudExe, Version);
				Transfer2DB("PCalsCustomVar", SourceConnectionString, TargetConnectionString, IsBudExe, !IsBudExe, Version);
			}
			else
			{
				string PreVersion = (int.Parse(c_version.SelectedItem.ToString().Trim()) - 1).ToString();
				DeleteProject(TargetConnectionString, PreVersion);
				Transfer2DB("Project", SourceConnectionString, TargetConnectionString, IsBudExe, IsBudExe, PreVersion);
				Transfer2DB("ProjMrsA", SourceConnectionString, TargetConnectionString, IsBudExe, IsBudExe, PreVersion);
				Transfer2DB("ProjMrsB", SourceConnectionString, TargetConnectionString, IsBudExe, IsBudExe, PreVersion);
				Transfer2DB("ProjMrsC", SourceConnectionString, TargetConnectionString, IsBudExe, IsBudExe, PreVersion);
				Transfer2DB("Annex", SourceConnectionString, TargetConnectionString, IsBudExe, IsBudExe, PreVersion);
				Transfer2DB("CostKind", SourceConnectionString, TargetConnectionString, IsBudExe, IsBudExe, PreVersion);
				Transfer2DB("ItemA", SourceConnectionString, TargetConnectionString, IsBudExe, IsBudExe, PreVersion);
				Transfer2DB("ItemB", SourceConnectionString, TargetConnectionString, IsBudExe, IsBudExe, PreVersion);
				Transfer2DB("ItemC", SourceConnectionString, TargetConnectionString, IsBudExe, IsBudExe, PreVersion);
				Transfer2DB("PageBreak", SourceConnectionString, TargetConnectionString, IsBudExe, IsBudExe, PreVersion);
				Transfer2DB("PCalsCustomVar", SourceConnectionString, TargetConnectionString, IsBudExe, IsBudExe, PreVersion);
				Transfer2DB("Project", SourceConnectionString, TargetConnectionString, IsBudExe, IsBudExe, Version);
				Transfer2DB("Project", SourceConnectionString, TargetConnectionString, IsBudExe, !IsBudExe, Version);
				Transfer2DB("ProjMrsA", SourceConnectionString, TargetConnectionString, IsBudExe, !IsBudExe, Version);
				Transfer2DB("ProjMrsB", SourceConnectionString, TargetConnectionString, IsBudExe, !IsBudExe, Version);
				Transfer2DB("ProjMrsC", SourceConnectionString, TargetConnectionString, IsBudExe, !IsBudExe, Version);
				Transfer2DB("Annex", SourceConnectionString, TargetConnectionString, IsBudExe, !IsBudExe, Version);
				Transfer2DB("CostKind", SourceConnectionString, TargetConnectionString, IsBudExe, !IsBudExe, Version);
				Transfer2DB("ItemA", SourceConnectionString, TargetConnectionString, IsBudExe, !IsBudExe, Version);
				Transfer2DB("ItemB", SourceConnectionString, TargetConnectionString, IsBudExe, !IsBudExe, Version);
				Transfer2DB("ItemC", SourceConnectionString, TargetConnectionString, IsBudExe, !IsBudExe, Version);
				Transfer2DB("PageBreak", SourceConnectionString, TargetConnectionString, IsBudExe, !IsBudExe, Version);
				Transfer2DB("PCalsCustomVar", SourceConnectionString, TargetConnectionString, IsBudExe, !IsBudExe, Version);
			}
		}
		catch (Exception ex)
		{
			DeleteProject(TargetConnectionString, "");
			DeleteProject(TargetConnectionString, Version);
			ER.Message = "匯出失敗 : " + ex.Message;
			ER.ReturnCode = 1;
		}
		return ER;
	}

	private void InitializeComponent()
	{
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.BudgetChange.FormDataExport_Wzd));
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
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		this.Tab1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.label12 = new System.Windows.Forms.Label();
		this.c_version = new System.Windows.Forms.ComboBox();
		this.Label3 = new Infragistics.Win.Misc.UltraLabel();
		this.Label2 = new Infragistics.Win.Misc.UltraLabel();
		this.Label1 = new Infragistics.Win.Misc.UltraLabel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.panel1 = new System.Windows.Forms.Panel();
		this.Next1 = new Infragistics.Win.Misc.UltraButton();
		this.Cancel1 = new Infragistics.Win.Misc.UltraButton();
		this.Tab2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.GetDB = new System.Windows.Forms.Button();
		this.db_name = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.server_id = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.Label7 = new Infragistics.Win.Misc.UltraLabel();
		this.password = new System.Windows.Forms.TextBox();
		this.Label6 = new Infragistics.Win.Misc.UltraLabel();
		this.user_id = new System.Windows.Forms.TextBox();
		this.Label5 = new Infragistics.Win.Misc.UltraLabel();
		this.Label4 = new Infragistics.Win.Misc.UltraLabel();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.panel3 = new System.Windows.Forms.Panel();
		this.Prev2 = new Infragistics.Win.Misc.UltraButton();
		this.Next2 = new Infragistics.Win.Misc.UltraButton();
		this.Cancel2 = new Infragistics.Win.Misc.UltraButton();
		this.Tab3 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.Label8 = new Infragistics.Win.Misc.UltraLabel();
		this.panel4 = new System.Windows.Forms.Panel();
		this.Tab4 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.Label11 = new Infragistics.Win.Misc.UltraLabel();
		this.Label10 = new Infragistics.Win.Misc.UltraLabel();
		this.Label9 = new Infragistics.Win.Misc.UltraLabel();
		this.panel5 = new System.Windows.Forms.Panel();
		this.Finish = new Infragistics.Win.Misc.UltraButton();
		this.Tab_ctrl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.Tab_Control_Page = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.label13 = new System.Windows.Forms.Label();
		this.lbRemoteVersion = new System.Windows.Forms.Label();
		this.Tab1.SuspendLayout();
		this.panel1.SuspendLayout();
		this.Tab2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.db_name).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.server_id).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		this.panel3.SuspendLayout();
		this.Tab3.SuspendLayout();
		this.Tab4.SuspendLayout();
		this.panel5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Tab_ctrl).BeginInit();
		this.Tab_ctrl.SuspendLayout();
		base.SuspendLayout();
		this.Tab1.Controls.Add(this.lbRemoteVersion);
		this.Tab1.Controls.Add(this.label13);
		this.Tab1.Controls.Add(this.label12);
		this.Tab1.Controls.Add(this.c_version);
		this.Tab1.Controls.Add(this.Label3);
		this.Tab1.Controls.Add(this.Label2);
		this.Tab1.Controls.Add(this.Label1);
		this.Tab1.Controls.Add(this.panel2);
		this.Tab1.Controls.Add(this.panel1);
		this.Tab1.Location = new System.Drawing.Point(0, 0);
		this.Tab1.Name = "Tab1";
		this.Tab1.Size = new System.Drawing.Size(542, 306);
		this.label12.Location = new System.Drawing.Point(200, 176);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(160, 23);
		this.label12.TabIndex = 28;
		this.label12.Text = "請選擇要移轉的次別：";
		this.c_version.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.c_version.Location = new System.Drawing.Point(360, 174);
		this.c_version.Name = "c_version";
		this.c_version.Size = new System.Drawing.Size(130, 23);
		this.c_version.TabIndex = 27;
		appearance1.BackColor = System.Drawing.Color.White;
		this.Label3.Appearance = appearance1;
		this.Label3.Location = new System.Drawing.Point(200, 136);
		this.Label3.Name = "Label3";
		this.Label3.Size = new System.Drawing.Size(300, 24);
		this.Label3.TabIndex = 26;
		this.Label3.Text = "接下來它會逐步引導您完成資料轉換的工作";
		appearance2.BackColor = System.Drawing.Color.White;
		this.Label2.Appearance = appearance2;
		this.Label2.Location = new System.Drawing.Point(200, 96);
		this.Label2.Name = "Label2";
		this.Label2.Size = new System.Drawing.Size(300, 24);
		this.Label2.TabIndex = 25;
		this.Label2.Text = "資料匯出精靈可幫助您將資料移轉至伺服器";
		appearance3.BackColor = System.Drawing.Color.White;
		this.Label1.Appearance = appearance3;
		this.Label1.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.Label1.Location = new System.Drawing.Point(200, 40);
		this.Label1.Name = "Label1";
		this.Label1.Size = new System.Drawing.Size(200, 20);
		this.Label1.TabIndex = 24;
		this.Label1.Text = "歡迎使用資料匯出精靈";
		this.panel2.BackgroundImage = (System.Drawing.Image)resources.GetObject("panel2.BackgroundImage");
		this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(170, 262);
		this.panel2.TabIndex = 23;
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.Controls.Add(this.Next1);
		this.panel1.Controls.Add(this.Cancel1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Font = new System.Drawing.Font("新細明體", 11.25f);
		this.panel1.Location = new System.Drawing.Point(0, 262);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(542, 44);
		this.panel1.TabIndex = 22;
		this.Next1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance4.Image = resources.GetObject("appearance4.Image");
		appearance4.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance4.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.Next1.Appearance = appearance4;
		this.Next1.BackColor = System.Drawing.SystemColors.Control;
		this.Next1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.Next1.Font = new System.Drawing.Font("細明體", 11f);
		this.Next1.ImageSize = new System.Drawing.Size(20, 20);
		this.Next1.ImageTransparentColor = System.Drawing.Color.White;
		this.Next1.Location = new System.Drawing.Point(354, 8);
		this.Next1.Name = "Next1";
		this.Next1.ShowFocusRect = false;
		this.Next1.ShowOutline = false;
		this.Next1.Size = new System.Drawing.Size(88, 31);
		this.Next1.SupportThemes = false;
		this.Next1.TabIndex = 1;
		this.Next1.Text = "下一步";
		this.Next1.Click += new System.EventHandler(Next1_Click);
		this.Cancel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance5.Image = resources.GetObject("appearance5.Image");
		appearance5.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.Cancel1.Appearance = appearance5;
		this.Cancel1.BackColor = System.Drawing.SystemColors.Control;
		this.Cancel1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.Cancel1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.Cancel1.Font = new System.Drawing.Font("細明體", 11f);
		this.Cancel1.ImageSize = new System.Drawing.Size(20, 20);
		this.Cancel1.ImageTransparentColor = System.Drawing.Color.White;
		this.Cancel1.Location = new System.Drawing.Point(446, 8);
		this.Cancel1.Name = "Cancel1";
		this.Cancel1.ShowFocusRect = false;
		this.Cancel1.ShowOutline = false;
		this.Cancel1.Size = new System.Drawing.Size(88, 31);
		this.Cancel1.SupportThemes = false;
		this.Cancel1.TabIndex = 2;
		this.Cancel1.Text = "取消";
		this.Cancel1.Click += new System.EventHandler(Cancel1_Click);
		this.Tab2.Controls.Add(this.GetDB);
		this.Tab2.Controls.Add(this.db_name);
		this.Tab2.Controls.Add(this.server_id);
		this.Tab2.Controls.Add(this.Label7);
		this.Tab2.Controls.Add(this.password);
		this.Tab2.Controls.Add(this.Label6);
		this.Tab2.Controls.Add(this.user_id);
		this.Tab2.Controls.Add(this.Label5);
		this.Tab2.Controls.Add(this.Label4);
		this.Tab2.Controls.Add(this.pictureBox1);
		this.Tab2.Controls.Add(this.panel3);
		this.Tab2.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab2.Name = "Tab2";
		this.Tab2.Size = new System.Drawing.Size(542, 306);
		this.GetDB.BackColor = System.Drawing.Color.Gainsboro;
		this.GetDB.Location = new System.Drawing.Point(380, 164);
		this.GetDB.Name = "GetDB";
		this.GetDB.Size = new System.Drawing.Size(128, 27);
		this.GetDB.TabIndex = 39;
		this.GetDB.Text = "載入資料庫清單";
		this.GetDB.UseVisualStyleBackColor = false;
		this.GetDB.Click += new System.EventHandler(GetDB_Click);
		this.db_name.AutoSize = true;
		this.db_name.Location = new System.Drawing.Point(174, 167);
		this.db_name.Name = "db_name";
		this.db_name.Size = new System.Drawing.Size(200, 24);
		this.db_name.TabIndex = 38;
		this.db_name.Text = null;
		this.server_id.AutoSize = true;
		this.server_id.Location = new System.Drawing.Point(176, 70);
		this.server_id.Name = "server_id";
		this.server_id.Size = new System.Drawing.Size(199, 24);
		this.server_id.TabIndex = 37;
		this.server_id.Text = null;
		appearance6.BackColor = System.Drawing.Color.White;
		this.Label7.Appearance = appearance6;
		this.Label7.Font = new System.Drawing.Font("新細明體", 10f);
		this.Label7.Location = new System.Drawing.Point(86, 171);
		this.Label7.Name = "Label7";
		this.Label7.Size = new System.Drawing.Size(60, 20);
		this.Label7.TabIndex = 33;
		this.Label7.Text = "資料庫：";
		this.password.Location = new System.Drawing.Point(175, 135);
		this.password.Name = "password";
		this.password.PasswordChar = '*';
		this.password.Size = new System.Drawing.Size(200, 25);
		this.password.TabIndex = 32;
		appearance7.BackColor = System.Drawing.Color.White;
		this.Label6.Appearance = appearance7;
		this.Label6.Font = new System.Drawing.Font("新細明體", 10f);
		this.Label6.Location = new System.Drawing.Point(87, 139);
		this.Label6.Name = "Label6";
		this.Label6.Size = new System.Drawing.Size(60, 20);
		this.Label6.TabIndex = 31;
		this.Label6.Text = "密碼：";
		this.user_id.Location = new System.Drawing.Point(175, 103);
		this.user_id.Name = "user_id";
		this.user_id.Size = new System.Drawing.Size(200, 25);
		this.user_id.TabIndex = 30;
		appearance8.BackColor = System.Drawing.Color.White;
		this.Label5.Appearance = appearance8;
		this.Label5.Font = new System.Drawing.Font("新細明體", 10f);
		this.Label5.Location = new System.Drawing.Point(87, 107);
		this.Label5.Name = "Label5";
		this.Label5.Size = new System.Drawing.Size(88, 20);
		this.Label5.TabIndex = 29;
		this.Label5.Text = "使用者名稱：";
		appearance9.BackColor = System.Drawing.Color.White;
		this.Label4.Appearance = appearance9;
		this.Label4.Font = new System.Drawing.Font("新細明體", 10f);
		this.Label4.Location = new System.Drawing.Point(88, 72);
		this.Label4.Name = "Label4";
		this.Label4.Size = new System.Drawing.Size(60, 20);
		this.Label4.TabIndex = 27;
		this.Label4.Text = "伺服器：";
		this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
		this.pictureBox1.Location = new System.Drawing.Point(0, 0);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(542, 55);
		this.pictureBox1.TabIndex = 24;
		this.pictureBox1.TabStop = false;
		this.panel3.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel3.Controls.Add(this.Prev2);
		this.panel3.Controls.Add(this.Next2);
		this.panel3.Controls.Add(this.Cancel2);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel3.Font = new System.Drawing.Font("新細明體", 11.25f);
		this.panel3.Location = new System.Drawing.Point(0, 262);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(542, 44);
		this.panel3.TabIndex = 23;
		this.Prev2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance10.Image = resources.GetObject("appearance10.Image");
		appearance10.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.Prev2.Appearance = appearance10;
		this.Prev2.BackColor = System.Drawing.SystemColors.Control;
		this.Prev2.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.Prev2.Font = new System.Drawing.Font("細明體", 11f);
		this.Prev2.ImageSize = new System.Drawing.Size(20, 20);
		this.Prev2.ImageTransparentColor = System.Drawing.Color.White;
		this.Prev2.Location = new System.Drawing.Point(262, 8);
		this.Prev2.Name = "Prev2";
		this.Prev2.ShowFocusRect = false;
		this.Prev2.ShowOutline = false;
		this.Prev2.Size = new System.Drawing.Size(88, 31);
		this.Prev2.SupportThemes = false;
		this.Prev2.TabIndex = 3;
		this.Prev2.Text = "上一步";
		this.Prev2.Click += new System.EventHandler(Prev2_Click);
		this.Next2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance11.Image = resources.GetObject("appearance11.Image");
		appearance11.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance11.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.Next2.Appearance = appearance11;
		this.Next2.BackColor = System.Drawing.SystemColors.Control;
		this.Next2.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.Next2.Font = new System.Drawing.Font("細明體", 11f);
		this.Next2.ImageSize = new System.Drawing.Size(20, 20);
		this.Next2.ImageTransparentColor = System.Drawing.Color.White;
		this.Next2.Location = new System.Drawing.Point(354, 8);
		this.Next2.Name = "Next2";
		this.Next2.ShowFocusRect = false;
		this.Next2.ShowOutline = false;
		this.Next2.Size = new System.Drawing.Size(88, 31);
		this.Next2.SupportThemes = false;
		this.Next2.TabIndex = 1;
		this.Next2.Text = "下一步";
		this.Next2.Click += new System.EventHandler(Next2_Click);
		this.Cancel2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance12.Image = resources.GetObject("appearance12.Image");
		appearance12.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.Cancel2.Appearance = appearance12;
		this.Cancel2.BackColor = System.Drawing.SystemColors.Control;
		this.Cancel2.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.Cancel2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.Cancel2.Font = new System.Drawing.Font("細明體", 11f);
		this.Cancel2.ImageSize = new System.Drawing.Size(20, 20);
		this.Cancel2.ImageTransparentColor = System.Drawing.Color.White;
		this.Cancel2.Location = new System.Drawing.Point(446, 8);
		this.Cancel2.Name = "Cancel2";
		this.Cancel2.ShowFocusRect = false;
		this.Cancel2.ShowOutline = false;
		this.Cancel2.Size = new System.Drawing.Size(88, 31);
		this.Cancel2.SupportThemes = false;
		this.Cancel2.TabIndex = 2;
		this.Cancel2.Text = "取消";
		this.Cancel2.Click += new System.EventHandler(Cancel2_Click);
		this.Tab3.Controls.Add(this.Label8);
		this.Tab3.Controls.Add(this.panel4);
		this.Tab3.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab3.Name = "Tab3";
		this.Tab3.Size = new System.Drawing.Size(542, 306);
		appearance13.BackColor = System.Drawing.Color.White;
		appearance13.TextHAlign = Infragistics.Win.HAlign.Center;
		this.Label8.Appearance = appearance13;
		this.Label8.Location = new System.Drawing.Point(7, 104);
		this.Label8.Name = "Label8";
		this.Label8.Size = new System.Drawing.Size(528, 20);
		this.Label8.TabIndex = 24;
		this.Label8.Text = "資料移轉中，依專案大小所需時間不同，請稍候...";
		this.panel4.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel4.Font = new System.Drawing.Font("新細明體", 11.25f);
		this.panel4.Location = new System.Drawing.Point(0, 262);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(542, 44);
		this.panel4.TabIndex = 23;
		this.Tab4.Controls.Add(this.Label11);
		this.Tab4.Controls.Add(this.Label10);
		this.Tab4.Controls.Add(this.Label9);
		this.Tab4.Controls.Add(this.panel5);
		this.Tab4.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab4.Name = "Tab4";
		this.Tab4.Size = new System.Drawing.Size(542, 306);
		appearance14.BackColor = System.Drawing.Color.White;
		this.Label11.Appearance = appearance14;
		this.Label11.Location = new System.Drawing.Point(128, 152);
		this.Label11.Name = "Label11";
		this.Label11.Size = new System.Drawing.Size(236, 20);
		this.Label11.TabIndex = 27;
		this.Label11.Text = "若要結束精靈，請按一下[完成]。";
		appearance15.BackColor = System.Drawing.Color.White;
		this.Label10.Appearance = appearance15;
		this.Label10.Location = new System.Drawing.Point(128, 112);
		this.Label10.Name = "Label10";
		this.Label10.Size = new System.Drawing.Size(238, 20);
		this.Label10.TabIndex = 26;
		this.Label10.Text = "你已經成功將資料匯出到伺服器，";
		appearance16.BackColor = System.Drawing.Color.White;
		this.Label9.Appearance = appearance16;
		this.Label9.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.Label9.Location = new System.Drawing.Point(72, 64);
		this.Label9.Name = "Label9";
		this.Label9.Size = new System.Drawing.Size(70, 20);
		this.Label9.TabIndex = 25;
		this.Label9.Text = "恭禧您!";
		this.panel5.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel5.Controls.Add(this.Finish);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel5.Font = new System.Drawing.Font("新細明體", 11.25f);
		this.panel5.Location = new System.Drawing.Point(0, 262);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(542, 44);
		this.panel5.TabIndex = 24;
		appearance17.Image = resources.GetObject("appearance17.Image");
		appearance17.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.Finish.Appearance = appearance17;
		this.Finish.BackColor = System.Drawing.SystemColors.Control;
		this.Finish.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.Finish.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.Finish.Font = new System.Drawing.Font("細明體", 11f);
		this.Finish.ImageSize = new System.Drawing.Size(20, 20);
		this.Finish.ImageTransparentColor = System.Drawing.Color.White;
		this.Finish.Location = new System.Drawing.Point(356, 8);
		this.Finish.Name = "Finish";
		this.Finish.ShowFocusRect = false;
		this.Finish.ShowOutline = false;
		this.Finish.Size = new System.Drawing.Size(88, 31);
		this.Finish.SupportThemes = false;
		this.Finish.TabIndex = 2;
		this.Finish.Text = "完成";
		this.Finish.Click += new System.EventHandler(Finish_Click);
		this.Tab_ctrl.BackColor = System.Drawing.Color.White;
		this.Tab_ctrl.Controls.Add(this.Tab_Control_Page);
		this.Tab_ctrl.Controls.Add(this.Tab1);
		this.Tab_ctrl.Controls.Add(this.Tab2);
		this.Tab_ctrl.Controls.Add(this.Tab3);
		this.Tab_ctrl.Controls.Add(this.Tab4);
		this.Tab_ctrl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Tab_ctrl.Font = new System.Drawing.Font("新細明體", 11.25f);
		this.Tab_ctrl.Location = new System.Drawing.Point(0, 0);
		this.Tab_ctrl.Name = "Tab_ctrl";
		this.Tab_ctrl.SharedControlsPage = this.Tab_Control_Page;
		this.Tab_ctrl.Size = new System.Drawing.Size(542, 306);
		this.Tab_ctrl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
		this.Tab_ctrl.TabIndex = 1;
		ultraTab1.TabPage = this.Tab1;
		ultraTab1.Text = "tab1";
		ultraTab2.TabPage = this.Tab2;
		ultraTab2.Text = "tab2";
		ultraTab3.TabPage = this.Tab3;
		ultraTab3.Text = "tab3";
		ultraTab4.TabPage = this.Tab4;
		ultraTab4.Text = "tab4";
		this.Tab_ctrl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[4] { ultraTab1, ultraTab2, ultraTab3, ultraTab4 });
		this.Tab_Control_Page.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_Control_Page.Name = "Tab_Control_Page";
		this.Tab_Control_Page.Size = new System.Drawing.Size(542, 306);
		this.label13.Location = new System.Drawing.Point(226, 214);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(134, 23);
		this.label13.TabIndex = 29;
		this.label13.Text = "目前遠端版本為：";
		this.lbRemoteVersion.Location = new System.Drawing.Point(357, 214);
		this.lbRemoteVersion.Name = "lbRemoteVersion";
		this.lbRemoteVersion.Size = new System.Drawing.Size(160, 23);
		this.lbRemoteVersion.TabIndex = 30;
		this.lbRemoteVersion.Text = "無";
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		base.ClientSize = new System.Drawing.Size(542, 306);
		base.Controls.Add(this.Tab_ctrl);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormDataExport_Wzd";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "資料匯出精靈";
		base.Load += new System.EventHandler(FormDataExport_Wzd_Load);
		this.Tab1.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		this.Tab2.ResumeLayout(false);
		this.Tab2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.db_name).EndInit();
		((System.ComponentModel.ISupportInitialize)this.server_id).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		this.panel3.ResumeLayout(false);
		this.Tab3.ResumeLayout(false);
		this.Tab4.ResumeLayout(false);
		this.panel5.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Tab_ctrl).EndInit();
		this.Tab_ctrl.ResumeLayout(false);
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
