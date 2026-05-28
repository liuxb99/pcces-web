using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.STDClass;

namespace Archnowledge.Pcces.PccesMain;

public class DBClass
{
	protected OleDbConnection DbConn;

	protected OleDbDataAdapter DbAdpt;

	protected OleDbCommand DbComm;

	private string F_ConnStr = ConfigurationManager.AppSettings["Conn"];

	private string F_SQLConnectionString;

	private string F_Issue;

	private string F_FS_UserID = "";

	private string F_FS_FormName = "";

	private string F_FS_SettingValue = "";

	public string _Issue
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

	public string _FS_UserID
	{
		get
		{
			return F_FS_UserID;
		}
		set
		{
			F_FS_UserID = value;
		}
	}

	public string _FS_FormName
	{
		get
		{
			return F_FS_FormName;
		}
		set
		{
			F_FS_FormName = value;
		}
	}

	public string _FS_SettingValue
	{
		get
		{
			return F_FS_SettingValue;
		}
		set
		{
			F_FS_SettingValue = value;
		}
	}

	public string ConnectionString
	{
		get
		{
			if (F_ConnStr == null || F_ConnStr.Length == 0)
			{
				F_ConnStr = "Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=pcces;Data Source=(local)";
			}
			return F_ConnStr;
		}
	}

	public DBClass()
	{
		DbConn = new OleDbConnection(ConnectionString);
		if (ConfigurationManager.ConnectionStrings["Pcces"] != null)
		{
			F_SQLConnectionString = ConfigurationManager.ConnectionStrings["Pcces"].ConnectionString;
		}
	}

	public bool SetUsualItem(int pubCode)
	{
		string sConn = GetMultiUserConnection2();
		string SQL = "Update Mrsbasea set show='1' Where pubCode =" + pubCode + " ";
		int AffectedRows = SqlUtility.ExecSQL(sConn, SQL);
		if (AffectedRows > 0)
		{
			return true;
		}
		return false;
	}

	public DataTable GetFuncParent()
	{
		DataTable DT_GetFuncParent = new DataTable("FuncParent");
		string sSQL = "Select * From FuncList Where Len(RTrim(FuncID)) = 4 And Visible = '1' Order By SortOrder, FuncID";
		DbAdpt = new OleDbDataAdapter(sSQL, DbConn);
		DbAdpt.Fill(DT_GetFuncParent);
		return DT_GetFuncParent;
	}

	public DataTable GetFuncChild()
	{
		DataTable DT_GetFuncChild = new DataTable("FuncChild");
		string sSQL = "Select * From FuncList Where Len(RTrim(FuncID)) <> 4 And Visible = '1'  Order By SortOrder, FuncID";
		DbAdpt = new OleDbDataAdapter(sSQL, DbConn);
		DbAdpt.Fill(DT_GetFuncChild);
		return DT_GetFuncChild;
	}

	public int UpdateAutoNumA_CommonName(string itemCode, string commonName)
	{
		int iEffect = 0;
		try
		{
			string sSQL = "Update AutoNumA Set commonName = N'" + commonName + "' Where itemCode='" + itemCode + "'";
			DbConn.Open();
			DbComm = new OleDbCommand(sSQL, DbConn);
			iEffect = DbComm.ExecuteNonQuery();
		}
		finally
		{
			DbConn.Close();
		}
		return iEffect;
	}

	public int UpdateAutoNumA_12_CommonName(string itemCode, string commonName)
	{
		int iEffect = 0;
		try
		{
			string sSQL = "Update AutoNumA_12 Set commonName = N'" + commonName + "' Where itemCode='" + itemCode + "'";
			DbConn.Open();
			DbComm = new OleDbCommand(sSQL, DbConn);
			iEffect = DbComm.ExecuteNonQuery();
		}
		finally
		{
			DbConn.Close();
		}
		return iEffect;
	}

	public int UpdateAutoNumB_12_L(DataTable DT_12_L)
	{
		int iEffect = 0;
		try
		{
			string sSQL = "Delete AutoNumB_12_L";
			DbConn.Open();
			DbComm = new OleDbCommand(sSQL, DbConn);
			iEffect = DbComm.ExecuteNonQuery();
			for (int i = 0; i < DT_12_L.Rows.Count; i++)
			{
				sSQL = "INSERT INTO AutoNumB_12_L(CodeSection, Code, [Content], commonName) VALUES('" + DT_12_L.Rows[i]["CodeSection"].ToString() + "','" + DT_12_L.Rows[i]["Code"].ToString() + "',N'" + DT_12_L.Rows[i]["Content"].ToString() + "',N'" + DT_12_L.Rows[i]["commonName"].ToString() + "')";
				DbComm.CommandText = sSQL;
				iEffect += DbComm.ExecuteNonQuery();
			}
		}
		finally
		{
			DbConn.Close();
		}
		return iEffect;
	}

	public DataTable GetAutoNumA1()
	{
		DataTable DT_GetAutoNumA1 = new DataTable("AutoNumA1");
		string sSQL = "Select RTrim(itemCode) as itemCode, RTrim(cName)  as cName from AutoNumA Where RTrim(WinFormFlag) ='1' ";
		DbAdpt = new OleDbDataAdapter(sSQL, DbConn);
		DbAdpt.Fill(DT_GetAutoNumA1);
		return DT_GetAutoNumA1;
	}

	public DataTable GetAutoNumA2()
	{
		DataTable DT_GetAutoNumA2 = new DataTable("AutoNumA2");
		string sSQL = "Select RTrim(A.itemCode) as itemCode,  RTrim(A.cName collate Chinese_Taiwan_Stroke_CI_AS) + IsNull(RTrim(A.IsShow collate Chinese_Taiwan_Stroke_CI_AS),'') as cName, A.parent as parent1,B.surName,A.commonName from AutoNumA A Left Join AutoNumY B On A.itemCode = B.ItemCode Where RTrim(WinFormFlag) ='2' and A.parent <> 'E' and A.parent <> 'L'";
		string sSQL2 = "Select RTrim(pccesCode) as itemCode,  RTrim(cName) as cName, '00' as parent1, commonName from MrsBaseA Where DATALENGTH(RTRIM(pccesCode)) >= 5 And substring(pccesCode,1,2) = '00' Order By pccesCode";
		DbAdpt = new OleDbDataAdapter(sSQL, DbConn);
		DbAdpt.Fill(DT_GetAutoNumA2);
		DbAdpt = new OleDbDataAdapter(sSQL2, DbConn);
		DbAdpt.Fill(DT_GetAutoNumA2);
		return DT_GetAutoNumA2;
	}

	public DataTable GetAutoNumA2_12()
	{
		DataTable DT_GetAutoNumA2 = new DataTable("AutoNumA2");
		string sSQL1 = "Select RTrim(A.itemCode) as itemCode,  RTrim(A.cName collate Chinese_Taiwan_Stroke_CI_AS) as cName, A.parent as parent1,B.surName, A.commonName from AutoNumA_12 A Left Join AutoNumY B On A.itemCode = B.ItemCode Where RTrim(WinFormFlag) ='2'";
		DbAdpt = new OleDbDataAdapter(sSQL1, DbConn);
		DbAdpt.Fill(DT_GetAutoNumA2);
		return DT_GetAutoNumA2;
	}

	public DataTable GetAutoNumB_12_L()
	{
		DataTable DT_GetAutoNumB_12_L = new DataTable("AutoNumB12_L");
		string sSQL1 = "Select * From AutoNumB_12_L";
		DbAdpt = new OleDbDataAdapter(sSQL1, DbConn);
		DbAdpt.Fill(DT_GetAutoNumB_12_L);
		return DT_GetAutoNumB_12_L;
	}

	public string GetSurName(string itemCode)
	{
		DataTable dt = new DataTable();
		string sName = "";
		string sSQL = "Select RTrim(A.itemCode) as itemCode,  RTrim(A.cName collate Chinese_Taiwan_Stroke_CI_AS) + IsNull(RTrim(A.IsShow collate Chinese_Taiwan_Stroke_CI_AS),'') as cName, A.parent as parent1,B.surName from AutoNumA A Left Join AutoNumY B On A.itemCode = B.ItemCode Where RTrim(WinFormFlag) ='2' and A.itemCode = '" + itemCode.Trim() + "'";
		DbAdpt = new OleDbDataAdapter(sSQL, DbConn);
		DbAdpt.Fill(dt);
		if (dt.Rows.Count > 0)
		{
			sName = dt.Rows[0]["surName"].ToString().Trim();
		}
		return sName;
	}

	public string GetExt(string itemCode)
	{
		DataTable dt = new DataTable();
		string sExt = "";
		string sSQL = "Select RTrim(A.Ext) as Ext from AutoNumA A Where A.itemCode = '" + itemCode.Trim() + "'";
		DbAdpt = new OleDbDataAdapter(sSQL, DbConn);
		DbAdpt.Fill(dt);
		if (dt.Rows.Count > 0)
		{
			sExt = dt.Rows[0]["Ext"].ToString().Trim();
		}
		return sExt;
	}

	public DataTable GetAutoNumA2(string DEPT_ID)
	{
		DataTable DT_GetAutoNumA2 = new DataTable("AutoNumA2");
		string sSQL = "Select RTrim(itemCode) as itemCode,  RTrim(cName collate Chinese_Taiwan_Stroke_CI_AS) + IsNull(RTrim(IsShow collate Chinese_Taiwan_Stroke_CI_AS),'') as cName, parent as parent1, commonName from AutoNumA Where (WinFormFlag = '" + DEPT_ID + "') Or (  WinFormFlag in ('2') And itemCode not in (Select itemCode From AutoNumA Where WinFormFlag = '" + DEPT_ID + "'))";
		string sSQL2 = "Select RTrim(pccesCode) as itemCode,  RTrim(cName) as cName, '00' as parent1, commonName from MrsBaseA Where DATALENGTH(RTRIM(pccesCode)) >= 5 And substring(pccesCode,1,2) = '00' Order By pccesCode";
		DbAdpt = new OleDbDataAdapter(sSQL, DbConn);
		DbAdpt.Fill(DT_GetAutoNumA2);
		DbAdpt = new OleDbDataAdapter(sSQL2, DbConn);
		DbAdpt.Fill(DT_GetAutoNumA2);
		return DT_GetAutoNumA2;
	}

	public DataTable GetAutoNumA_Cust(string DEPT_ID)
	{
		DataTable DT_GetAutoNumA_Cust = new DataTable("AutoNumA_Cust");
		string sSQL = "Select RTrim(itemCode) as itemCode, RTrim(cName collate Chinese_Taiwan_Stroke_CI_AS) + IsNull(RTrim(IsShow collate Chinese_Taiwan_Stroke_CI_AS),'') as cName, parent as parent1, commonName from AutoNumA Where RTrim(WinFormFlag) ='" + DEPT_ID + "' ";
		DbAdpt = new OleDbDataAdapter(sSQL, DbConn);
		DbAdpt.Fill(DT_GetAutoNumA_Cust);
		return DT_GetAutoNumA_Cust;
	}

	public int SaveCustomAutoNum(string ChapCode, DataTable DT_SAV)
	{
		string sSQLUpd = "";
		string sSQLIns = "";
		for (int i = DT_SAV.Rows.Count - 1; i >= 0; i--)
		{
			if (DT_SAV.Rows[i]["ActType"].ToString().Trim().ToUpper() == "UPD")
			{
				if (DT_SAV.Rows[i]["Code"].ToString() != "RM")
				{
					object obj = sSQLUpd;
					sSQLUpd = string.Concat(obj, "Update AutoNumB Set MinRow =", DT_SAV.Rows[i]["Min"].ToString(), ", MaxRow =", DT_SAV.Rows[i]["Max"].ToString(), ", SelfRow=", DT_SAV.Rows[i]["SelfRow"].ToString(), " Where RowID = ", DT_SAV.Rows[i]["RowID"].ToString(), '\r');
				}
				else
				{
					object obj = sSQLUpd;
					sSQLUpd = string.Concat(obj, "Update AutoNumB Set MinRow = 0, MaxRow =0, SelfRow=", DT_SAV.Rows[i]["SelfRow"].ToString(), " Where RowID = ", DT_SAV.Rows[i]["RowID"].ToString(), '\r');
				}
			}
			else if (DT_SAV.Rows[i]["ActType"].ToString().Trim().ToUpper() == "NEW")
			{
				if (DT_SAV.Rows[i]["Code"].ToString() != "RM")
				{
					object obj = sSQLIns;
					sSQLIns = string.Concat(obj, "Insert Into AutoNumB(ChapCode, Code, CodeSection, MinRow, MaxRow, SelfRow, IsCustom, Version) Values('", ChapCode, "','','", DT_SAV.Rows[i]["Code"].ToString(), "',", DT_SAV.Rows[i]["Min"].ToString(), ",", DT_SAV.Rows[i]["Max"].ToString(), ",", DT_SAV.Rows[i]["SelfRow"].ToString(), ",'N','0')", '\r');
				}
				else
				{
					object obj = sSQLIns;
					sSQLIns = string.Concat(obj, "Insert Into AutoNumB(ChapCode, Code, CodeSection, MinRow, MaxRow, SelfRow, IsCustom, Version) Values('", ChapCode, "','','", DT_SAV.Rows[i]["Code"].ToString(), "',0,0,", DT_SAV.Rows[i]["SelfRow"].ToString(), ",'N','0')", '\r');
				}
			}
			if (i % 50 == 0)
			{
				ExecuteCommand(sSQLUpd);
				sSQLUpd = "";
			}
		}
		if (sSQLUpd != "")
		{
			ExecuteCommand(sSQLUpd);
			sSQLUpd = "";
		}
		if (sSQLIns != "")
		{
			ExecuteCommand(sSQLIns);
			sSQLIns = "";
		}
		return 1;
	}

	public string GetUserUseDataBaseSetAutoNum(string sUserID)
	{
		string RetV = "";
		string sSNO = "";
		DataTable DTtmp = new DataTable();
		SysUser oSysUser = new SysUser();
		string CurrentDBName = oSysUser.GetSysUserDatabaseName(sUserID);
		string sSQL2 = "Select sno From UserDefind Where Kind='DataBase' And cString='" + CurrentDBName + "' ";
		DbAdpt = new OleDbDataAdapter(sSQL2, DbConn);
		DbAdpt.Fill(DTtmp);
		if (DTtmp.Rows.Count > 0)
		{
			sSNO = DTtmp.Rows[0]["sno"].ToString().Trim();
		}
		if (sSNO.Trim() != "")
		{
			string sSQL3 = "Select cString From UserDefind Where sno='" + sSNO + "' And Kind='DataAuto'";
			DbAdpt = new OleDbDataAdapter(sSQL3, DbConn);
			DTtmp.Clear();
			DbAdpt.Fill(DTtmp);
			if (DTtmp.Rows.Count > 0)
			{
				RetV = DTtmp.Rows[0]["cString"].ToString();
			}
		}
		return RetV;
	}

	public bool GetIsUserUseDataBaseSetAutoNum(string sUserID)
	{
		bool RetV = false;
		string sSNO = "";
		DataTable DTtmp = new DataTable();
		SysUser oSysUser = new SysUser();
		string CurrentDBName = oSysUser.GetSysUserDatabaseName(sUserID);
		string sSQL2 = "Select sno From UserDefind Where Kind='DataBase' And cString='" + CurrentDBName + "' ";
		DbAdpt = new OleDbDataAdapter(sSQL2, DbConn);
		DbAdpt.Fill(DTtmp);
		if (DTtmp.Rows.Count > 0)
		{
			sSNO = DTtmp.Rows[0]["sno"].ToString().Trim();
		}
		if (sSNO.Trim() != "")
		{
			string sSQL3 = "Select cString From UserDefind Where sno='" + sSNO + "' And Kind='DataAuto'";
			DbAdpt = new OleDbDataAdapter(sSQL3, DbConn);
			DTtmp.Clear();
			DbAdpt.Fill(DTtmp);
			if (DTtmp.Rows.Count > 0)
			{
				RetV = true;
			}
		}
		return RetV;
	}

	public string GetUserDefine_String(string sSQL, string sFieldName)
	{
		string sConn = GetMultiUserConnection2();
		object Value = SqlUtility.ExecScale(sConn, sSQL);
		if (Value != DBNull.Value && Value != null)
		{
			return Value.ToString();
		}
		return "";
	}

	public DataTable GetUserDefine(string sSQL)
	{
		string sConn = GetMultiUserConnection2();
		DataSet ds = SqlUtility.ExecDataSet(sConn, sSQL);
		if (ds.Tables.Count > 0)
		{
			return ds.Tables[0];
		}
		return null;
	}

	public int ExecuteCommand(string sSQL)
	{
		int iRetV = -1;
		string sConn = GetMultiUserConnection2();
		return SqlUtility.ExecSQL(sConn, sSQL);
	}

	public int ExecuteSqlCommand(SqlCommand odCmd)
	{
		int iRetV = -1;
		string sConn = GetMultiUserConnection2();
		using (SqlConnection myConnection = new SqlConnection(sConn))
		{
			odCmd.Connection = myConnection;
			myConnection.Open();
			iRetV = odCmd.ExecuteNonQuery();
			myConnection.Close();
		}
		return iRetV;
	}

	public int ExecuteOleDbCommand(OleDbCommand odCmd)
	{
		int iRetV = -1;
		string sConn = GetMultiUserConnection();
		DbConn.ConnectionString = sConn;
		odCmd.Connection = DbConn;
		DbConn.Open();
		iRetV = odCmd.ExecuteNonQuery();
		DbConn.Close();
		return iRetV;
	}

	public DataTable GetProjectUserList(string sProjectCode)
	{
		string sConn = GetMultiUserConnection2();
		string sSQL = "Select Distinct a.*, b.UserName, b.Power, b.Pwd   from ProjAuthority a left outer join SysUser b on a.UserID = b.UserID Where a.ProjectCode = '" + sProjectCode + "' ";
		DataSet ds = SqlUtility.ExecDataSet(sConn, sSQL);
		if (ds.Tables.Count > 0)
		{
			return ds.Tables[0];
		}
		return null;
	}

	public DataTable GetUserProjectList(string sUserID, string Wstr)
	{
		string sConn = GetMultiUserConnection2();
		string sSQL = "Select a.*,b.projectcode as bid,c.projectcode as bud, b.CloseBidDate from pubProject a  left outer join bidproject b on a.projectcode = b.projectcode  left outer join budproject c on a.projectcode = c.projectcode  left outer join ProjAuthority d on a.projectcode = d.projectcode  where d.UserID = '" + sUserID + "' ";
		DataSet ds = SqlUtility.ExecDataSet(sConn, sSQL);
		if (ds.Tables.Count > 0)
		{
			return ds.Tables[0];
		}
		return null;
	}

	public bool UpdateUserProject(string sUserID, DataTable DT_Usr)
	{
		bool bRetV = false;
		try
		{
			string sSQL = "Delete From ProjAuthority Where UserID='" + sUserID + "' ";
			string sConn = GetMultiUserConnection2();
			SqlUtility.ExecSQL(sConn, sSQL);
			for (int i = 0; i < DT_Usr.Rows.Count; i++)
			{
				string ProjectCode = DT_Usr.Rows[i]["ProjectCode"].ToString().Trim();
				sSQL = "Insert Into ProjAuthority(ProjectCode, UserID) values('" + ProjectCode + "','" + sUserID + "')";
				SqlUtility.ExecSQL(sConn, sSQL);
			}
			bRetV = true;
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "DBClass.cs" + ex.Message);
		}
		return bRetV;
	}

	public bool UpdateProjectUser(string sProjectCode, DataTable DT_Prj)
	{
		bool bRetV = false;
		try
		{
			string sSQL = "Delete From ProjAuthority Where ProjectCode='" + sProjectCode + "' ";
			string sConn = GetMultiUserConnection2();
			SqlUtility.ExecSQL(sConn, sSQL);
			for (int i = 0; i < DT_Prj.Rows.Count; i++)
			{
				string UserID = DT_Prj.Rows[i]["UserID"].ToString().Trim();
				sSQL = "Insert Into ProjAuthority(ProjectCode, UserID) values('" + sProjectCode + "','" + UserID + "')";
				SqlUtility.ExecSQL(sConn, sSQL);
			}
			bRetV = true;
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "DBClass.cs" + ex.Message);
		}
		return bRetV;
	}

	public bool GetProjectAuthority(string sUser, string sProjectCode)
	{
		if (sUser == "PccAdmin")
		{
			return true;
		}
		DataTable DTRet = new DataTable();
		string sSQL = "Select Count(*) from ProjAuthority Where ProjectCode='" + sProjectCode + "' And UserID ='" + sUser + "' ";
		string sConn = GetMultiUserConnection2();
		object Value = SqlUtility.ExecScale(sConn, sSQL);
		if (Value != DBNull.Value && Value != null && (int)Value > 0)
		{
			return true;
		}
		return false;
	}

	public DataTable GetUserData(string sUserID)
	{
		DataTable DTRet = new DataTable();
		string sSQL = "Select * from SysUser Where UserID='" + sUserID + "' ";
		DbAdpt = new OleDbDataAdapter(sSQL, DbConn);
		DbAdpt.Fill(DTRet);
		return DTRet;
	}

	public int UpdateUserInfo(string sUserID, string sUserName, string sUserPwd)
	{
		int iRetV = -1;
		OleDbCommand odCmd = new OleDbCommand();
		odCmd.Connection = DbConn;
		DbConn.Open();
		string sSQL = "Update SysUser Set UserName ='" + sUserName + "', Pwd='" + sUserPwd + "'  Where UserID ='" + sUserID + "' ";
		odCmd.CommandText = sSQL;
		iRetV = odCmd.ExecuteNonQuery();
		DbConn.Close();
		return iRetV;
	}

	public static bool ChkAuthority(string sUserID, string sFuncID)
	{
		bool RetV = false;
		if (sUserID == "PccAdmin")
		{
			return true;
		}
		if (sUserID == "PccesUser")
		{
			return false;
		}
		string sSQL = "Select * From WinUserFuncs  Where UserID ='" + sUserID + "'    And FuncID ='" + sFuncID + "' ";
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = "PccAdmin";
		int iCount = DBCLS.GetUserDefine(sSQL).Rows.Count;
		if (iCount > 0)
		{
			RetV = true;
		}
		else
		{
			sSQL = "Select * From FuncList  Where FuncID ='" + sFuncID + "' ";
			DataTable DT1 = DBCLS.GetUserDefine(sSQL);
			if (DT1.Rows.Count > 0)
			{
				string sVisible = DT1.Rows[0]["Visible"].ToString().Trim();
				if (sVisible == "0")
				{
					RetV = true;
				}
			}
		}
		return RetV;
	}

	public static string GetFuncName(string sFuncID)
	{
		string RetV = "";
		string sSQL = "Select FuncName From FuncList Where FuncID='" + sFuncID + "' ";
		DBClass DBCLS = new DBClass();
		DataTable DT1 = new DataTable();
		DT1 = DBCLS.GetUserDefine(sSQL);
		if (DT1.Rows.Count > 0)
		{
			return DT1.Rows[0]["FuncName"].ToString().Trim();
		}
		return "";
	}

	public DataTable GetUserFuncs(string sUserID)
	{
		DataTable DTRet = new DataTable();
		string sSQL = "Select * from WinUserFuncs Where UserID = '" + sUserID + "' Order By FuncID ";
		DbAdpt = new OleDbDataAdapter(sSQL, DbConn);
		DbAdpt.Fill(DTRet);
		return DTRet;
	}

	public DataTable GetUserGroups(string sUserID)
	{
		DataTable DTRet = new DataTable();
		string sSQL = "Select A.GroupID, A.GroupName,  (Select GroupID From WinGroupUsers Where GroupID = A.GroupID And UserID = '" + sUserID + "') as GRP  From WinGroups A ";
		DbAdpt = new OleDbDataAdapter(sSQL, DbConn);
		DbAdpt.Fill(DTRet);
		return DTRet;
	}

	public DataTable GetUserList()
	{
		DataTable DTRet = new DataTable();
		string sSQL = "Select * from SysUser Order By UserID ";
		DbAdpt = new OleDbDataAdapter(sSQL, DbConn);
		DbAdpt.Fill(DTRet);
		return DTRet;
	}

	public int InsertUsers(string sUserID, string sUserName, string sPwd, string sPower)
	{
		int RetV = 0;
		string sSQL = "Select * From SysUser Where UserID = '" + sUserID + "'";
		OleDbDataAdapter odAdpt = new OleDbDataAdapter(sSQL, DbConn);
		DataTable DT1 = new DataTable();
		odAdpt.Fill(DT1);
		if (DT1.Rows.Count > 0)
		{
			RetV = -1;
		}
		else
		{
			OleDbCommand odCmd = new OleDbCommand();
			odCmd.Connection = DbConn;
			odCmd.CommandText = "Insert Into SysUser(UserID, UserName, Pwd, Power, DataBaseName2) values(?,?,?,?,?)";
			OleDbParameter lpa_val1 = new OleDbParameter("?P1", OleDbType.VarChar);
			lpa_val1.Direction = ParameterDirection.Input;
			lpa_val1.Value = sUserID.Trim();
			odCmd.Parameters.Add(lpa_val1);
			OleDbParameter lpa_val2 = new OleDbParameter("?P2", OleDbType.VarChar);
			lpa_val2.Direction = ParameterDirection.Input;
			lpa_val2.Value = sUserName.Trim();
			odCmd.Parameters.Add(lpa_val2);
			OleDbParameter lpa_val3 = new OleDbParameter("?P3", OleDbType.VarChar);
			lpa_val3.Direction = ParameterDirection.Input;
			lpa_val3.Value = sPwd.Trim();
			odCmd.Parameters.Add(lpa_val3);
			OleDbParameter lpa_val4 = new OleDbParameter("?P4", OleDbType.VarChar);
			lpa_val4.Direction = ParameterDirection.Input;
			lpa_val4.Value = sPower.Trim();
			odCmd.Parameters.Add(lpa_val4);
			OleDbParameter lpa_val5 = new OleDbParameter("?P5", OleDbType.VarChar);
			lpa_val5.Direction = ParameterDirection.Input;
			lpa_val5.Value = "PCCES";
			odCmd.Parameters.Add(lpa_val5);
			DbConn.Open();
			RetV = odCmd.ExecuteNonQuery();
			DbConn.Close();
		}
		return RetV;
	}

	public int SaveUsers(string sUserID, string sUserName, string sPwd, string sPower)
	{
		int RetV = 0;
		string sSQL = "Select * From SysUser Where UserID = '" + sUserID + "'";
		OleDbDataAdapter odAdpt = new OleDbDataAdapter(sSQL, DbConn);
		DataTable DT1 = new DataTable();
		odAdpt.Fill(DT1);
		if (DT1.Rows.Count > 0)
		{
			OleDbCommand odCmd = new OleDbCommand();
			odCmd.Connection = DbConn;
			odCmd.CommandText = "Update SysUser Set UserName ='" + sUserName + "',  Pwd ='" + sPwd + "',  Power ='" + sPower + "'  Where UserID ='" + sUserID + "' ";
			DbConn.Open();
			RetV = odCmd.ExecuteNonQuery();
			DbConn.Close();
		}
		return RetV;
	}

	public void SaveUserFuncs(string sUserID, DataTable DT_UsrFuncs)
	{
		string sSQL = "Delete WinUserFuncs Where UserID ='" + sUserID + "' ";
		OleDbCommand odCmd = new OleDbCommand();
		odCmd.Connection = DbConn;
		odCmd.CommandText = sSQL;
		DbConn.Open();
		odCmd.ExecuteNonQuery();
		DbConn.Close();
		odCmd.CommandText = "Insert Into WinUserFuncs(UserID, FuncID) values ('" + sUserID + "', ?)";
		DbConn.Open();
		for (int i = 0; i < DT_UsrFuncs.Rows.Count; i++)
		{
			odCmd.Parameters.Clear();
			OleDbParameter lpa_val1 = new OleDbParameter("?P1", OleDbType.VarChar);
			lpa_val1.Direction = ParameterDirection.Input;
			lpa_val1.Value = DT_UsrFuncs.Rows[i]["FuncID"].ToString().Trim();
			odCmd.Parameters.Add(lpa_val1);
			odCmd.ExecuteNonQuery();
		}
		DbConn.Close();
	}

	public void UpdateUserGroups(DataTable DT_GRPUsers, string sUserID)
	{
		OleDbCommand odCmd = new OleDbCommand();
		odCmd.Connection = DbConn;
		for (int i = 0; i < DT_GRPUsers.Rows.Count; i++)
		{
			if (DT_GRPUsers.Rows[i]["GRP"].ToString().Trim() != "")
			{
				string sSQL = "Select * from WinGroupUsers Where GroupID ='" + DT_GRPUsers.Rows[i]["GroupID"].ToString().Trim() + "'  And UserID ='" + sUserID + "'";
				if (GetUserDefine(sSQL).Rows.Count <= 0)
				{
					DbConn.Open();
					odCmd.CommandText = "Insert Into WinGroupUsers(GroupID, UserID) values('" + DT_GRPUsers.Rows[i]["GroupID"].ToString().Trim() + "', '" + sUserID + "')";
					odCmd.ExecuteNonQuery();
					UpdateUserFuncs_By_GroupChaged(sUserID);
					DbConn.Close();
				}
			}
			else
			{
				DbConn.Open();
				odCmd.CommandText = "Delete WinGroupUsers Where GroupID ='" + DT_GRPUsers.Rows[i]["GroupID"].ToString().Trim() + "'  And UserID ='" + sUserID + "'";
				odCmd.ExecuteNonQuery();
				UpdateUserFuncs_By_GroupChaged(sUserID);
				DbConn.Close();
			}
		}
		DbConn.Close();
	}

	public int DeleteUser(string sUserID)
	{
		int RetV = 0;
		OleDbCommand odCmd = new OleDbCommand();
		odCmd.Connection = DbConn;
		odCmd.CommandText = " Delete SysUser Where UserID='" + sUserID + "' " + '\r' + " Delete WinUserFuncs Where UserID='" + sUserID + "' " + '\r' + " Delete WinGroupUsers Where UserID='" + sUserID + "' ";
		DbConn.Open();
		RetV = odCmd.ExecuteNonQuery();
		DbConn.Close();
		return RetV;
	}

	public void UpdateGroupUsers(DataTable DT_GRPUsers, string sGroupID)
	{
		OleDbCommand odCmd = new OleDbCommand();
		odCmd.Connection = DbConn;
		DbConn.Open();
		for (int i = 0; i < DT_GRPUsers.Rows.Count; i++)
		{
			if (DT_GRPUsers.Rows[i]["GroupID"].ToString().Trim() != "")
			{
				string sSQL = "Select * from WinGroupUsers Where GroupID ='" + sGroupID + "'  And UserID ='" + DT_GRPUsers.Rows[i]["UserID"].ToString().Trim() + "'";
				if (GetUserDefine(sSQL).Rows.Count <= 0)
				{
					DbConn.Close();
					DbConn.Open();
					odCmd.CommandText = "Insert Into WinGroupUsers(GroupID, UserID) values('" + sGroupID + "', '" + DT_GRPUsers.Rows[i]["UserID"].ToString().Trim() + "')";
					odCmd.ExecuteNonQuery();
					UpdateUserFuncs_By_GroupChaged(DT_GRPUsers.Rows[i]["UserID"].ToString().Trim());
				}
			}
			else
			{
				DbConn.Close();
				DbConn.Open();
				odCmd.CommandText = "Delete WinGroupUsers Where GroupID ='" + sGroupID + "'  And UserID ='" + DT_GRPUsers.Rows[i]["UserID"].ToString().Trim() + "'";
				odCmd.ExecuteNonQuery();
				UpdateUserFuncs_By_GroupChaged(DT_GRPUsers.Rows[i]["UserID"].ToString().Trim());
			}
		}
		DbConn.Close();
	}

	private void UpdateUserFuncs_By_GroupChaged(string sUserID)
	{
		OleDbCommand odCmd = new OleDbCommand();
		odCmd.Connection = DbConn;
		odCmd.CommandText = "Delete WinUserFuncs Where UserID ='" + sUserID + "' ";
		odCmd.ExecuteNonQuery();
		odCmd.CommandText = " Insert Into WinUserFuncs(UserID, FuncID) Select Distinct '" + sUserID + "' as UserID, FuncID from WinGroupFuncs  Where GroupID in (Select GroupID from WinGroupUsers Where UserID = '" + sUserID + "')";
		odCmd.ExecuteNonQuery();
	}

	public DataTable GetGroupUsers(string sGroupID)
	{
		DataTable DTRet = new DataTable();
		string sSQL = "Select A.UserID, A.UserName,  (Select GroupID From WinGroupUsers Where UserID = A.UserID And GroupID = '" + sGroupID + "') as GroupID  from SysUser A ";
		DbAdpt = new OleDbDataAdapter(sSQL, DbConn);
		DbAdpt.Fill(DTRet);
		return DTRet;
	}

	public DataTable GetGroupList()
	{
		DataTable DTRet = new DataTable();
		string sSQL = "Select * from WinGroups Order By GroupID ";
		DbAdpt = new OleDbDataAdapter(sSQL, DbConn);
		DbAdpt.Fill(DTRet);
		return DTRet;
	}

	public DataTable GetGroupFuncs(string sGroupID)
	{
		DataTable DTRet = new DataTable();
		string sSQL = "Select * from WinGroupFuncs Where GroupID = '" + sGroupID + "' Order By FuncID ";
		DbAdpt = new OleDbDataAdapter(sSQL, DbConn);
		DbAdpt.Fill(DTRet);
		return DTRet;
	}

	public void SaveGroupFuncs(string sGroupID, DataTable DT_GRPFuncs)
	{
		string sSQL = "Delete WinGroupFuncs Where GroupID ='" + sGroupID + "' ";
		OleDbCommand odCmd = new OleDbCommand();
		odCmd.Connection = DbConn;
		odCmd.CommandText = sSQL;
		DbConn.Open();
		odCmd.ExecuteNonQuery();
		DbConn.Close();
		odCmd.CommandText = "Insert Into WinGroupFuncs(GroupID, FuncID) values ('" + sGroupID + "', ?)";
		DbConn.Open();
		for (int i = 0; i < DT_GRPFuncs.Rows.Count; i++)
		{
			odCmd.Parameters.Clear();
			OleDbParameter lpa_val1 = new OleDbParameter("?P1", OleDbType.VarChar);
			lpa_val1.Direction = ParameterDirection.Input;
			lpa_val1.Value = DT_GRPFuncs.Rows[i]["FuncID"].ToString().Trim();
			odCmd.Parameters.Add(lpa_val1);
			odCmd.ExecuteNonQuery();
		}
		DbConn.Close();
	}

	public int InsertGroups(string sGroupID, string sGroupName)
	{
		int RetV = 0;
		string sSQL = "Select * From WinGroups Where GroupID = '" + sGroupID + "'";
		OleDbDataAdapter odAdpt = new OleDbDataAdapter(sSQL, DbConn);
		DataTable DT1 = new DataTable();
		odAdpt.Fill(DT1);
		if (DT1.Rows.Count > 0)
		{
			RetV = -1;
		}
		else
		{
			OleDbCommand odCmd = new OleDbCommand();
			odCmd.Connection = DbConn;
			odCmd.CommandText = "Insert Into WinGroups(GroupID, GroupName) values(?,?)";
			OleDbParameter lpa_val1 = new OleDbParameter("?P1", OleDbType.VarChar);
			lpa_val1.Direction = ParameterDirection.Input;
			lpa_val1.Value = sGroupID.Trim();
			odCmd.Parameters.Add(lpa_val1);
			OleDbParameter lpa_val2 = new OleDbParameter("?P2", OleDbType.VarChar);
			lpa_val2.Direction = ParameterDirection.Input;
			lpa_val2.Value = sGroupName.Trim();
			odCmd.Parameters.Add(lpa_val2);
			DbConn.Open();
			RetV = odCmd.ExecuteNonQuery();
			DbConn.Close();
		}
		return RetV;
	}

	public int SaveGroups(string sGroupID, string sGroupName)
	{
		int RetV = 0;
		string sSQL = "Select * From WinGroups Where GroupID = '" + sGroupID + "'";
		OleDbDataAdapter odAdpt = new OleDbDataAdapter(sSQL, DbConn);
		DataTable DT1 = new DataTable();
		odAdpt.Fill(DT1);
		if (DT1.Rows.Count > 0)
		{
			OleDbCommand odCmd = new OleDbCommand();
			odCmd.Connection = DbConn;
			odCmd.CommandText = "Update WinGroups Set GroupName ='" + sGroupName + "' Where GroupID ='" + sGroupID + "' ";
			DbConn.Open();
			RetV = odCmd.ExecuteNonQuery();
			DbConn.Close();
		}
		return RetV;
	}

	public int DeleteGroup(string sGroupID)
	{
		int RetV = 0;
		OleDbCommand odCmd = new OleDbCommand();
		odCmd.Connection = DbConn;
		odCmd.CommandText = " Delete WinGroups Where GroupID ='" + sGroupID + "' " + '\r' + " Delete WinGroupFuncs Where GroupID ='" + sGroupID + "' " + '\r' + " Delete WinGroupUsers Where GroupID ='" + sGroupID + "' ";
		DbConn.Open();
		RetV = odCmd.ExecuteNonQuery();
		DbConn.Close();
		return RetV;
	}

	public DataTable GetItemNO_Names()
	{
		DataTable DTRet = new DataTable();
		string sSQL = "Select Distinct Kind From UserDefind Where sNO > 200000";
		DbAdpt = new OleDbDataAdapter(sSQL, DbConn);
		DbAdpt.Fill(DTRet);
		return DTRet;
	}

	public DataTable GetItemNoList(string sKeyword)
	{
		DataTable DTRet = new DataTable();
		string sSQL = "Select Kind, cString From UserDefind Where Kind ='" + sKeyword + "' Order By sNO";
		DbAdpt = new OleDbDataAdapter(sSQL, DbConn);
		DbAdpt.Fill(DTRet);
		return DTRet;
	}

	public int SaveItemName(string sKind)
	{
		int RetV = 0;
		DataTable DTRet = new DataTable();
		string sSQL = "Select Kind From UserDefind Where Kind ='" + sKind + "'";
		DbAdpt = new OleDbDataAdapter(sSQL, DbConn);
		DbAdpt.Fill(DTRet);
		if (DTRet.Rows.Count > 0)
		{
			return -1;
		}
		OleDbCommand odCmd = new OleDbCommand();
		odCmd.Connection = DbConn;
		DbConn.Open();
		odCmd.CommandText = "Insert Into UserDefind(kind, cString, sno) values(?,?,?)";
		OleDbParameter lpa_val1 = new OleDbParameter("?P1", OleDbType.VarChar);
		lpa_val1.Direction = ParameterDirection.Input;
		lpa_val1.Value = sKind.Trim();
		odCmd.Parameters.Add(lpa_val1);
		OleDbParameter lpa_val2 = new OleDbParameter("?P2", OleDbType.VarChar);
		lpa_val2.Direction = ParameterDirection.Input;
		lpa_val2.Value = sKind.Trim();
		odCmd.Parameters.Add(lpa_val2);
		OleDbParameter lpa_val3 = new OleDbParameter("?P3", OleDbType.Integer);
		lpa_val3.Direction = ParameterDirection.Input;
		lpa_val3.Value = 200001;
		odCmd.Parameters.Add(lpa_val3);
		RetV = odCmd.ExecuteNonQuery();
		DbConn.Close();
		return RetV;
	}

	public int DeleteItemName(string sKind)
	{
		int RetV = 0;
		string sSQL = "Delete from UserDefind Where kind='" + sKind + "' ";
		ArrayList aArr = new ArrayList();
		aArr.Add("PccesAdmin");
		aArr.Add("刪除項次編號");
		ModifyDB StdCom = new ModifyDB("", aArr);
		RetV = StdCom.DBDele(sSQL);
		StdCom = null;
		return RetV;
	}

	public void SaveItemNo(DataTable DT_Num, string sKind)
	{
		int RetV = 0;
		OleDbCommand odCmd = new OleDbCommand();
		odCmd.Connection = DbConn;
		DbConn.Open();
		odCmd.CommandText = "Delete UserDefind Where kind='" + sKind + "' And sno > 200000 ";
		RetV = odCmd.ExecuteNonQuery();
		odCmd.CommandText = "Insert Into UserDefind(kind, cString, sno) values(?,?,?)";
		for (int i = 0; i < DT_Num.Rows.Count; i++)
		{
			odCmd.Parameters.Clear();
			OleDbParameter lpa_val1 = new OleDbParameter("?P1", OleDbType.VarChar);
			lpa_val1.Direction = ParameterDirection.Input;
			lpa_val1.Value = sKind.Trim();
			odCmd.Parameters.Add(lpa_val1);
			OleDbParameter lpa_val2 = new OleDbParameter("?P2", OleDbType.VarChar);
			lpa_val2.Direction = ParameterDirection.Input;
			lpa_val2.Value = DT_Num.Rows[i]["cString"].ToString().Trim();
			odCmd.Parameters.Add(lpa_val2);
			OleDbParameter lpa_val3 = new OleDbParameter("?P3", OleDbType.Integer);
			lpa_val3.Direction = ParameterDirection.Input;
			lpa_val3.Value = DT_Num.Rows[i]["sno"];
			odCmd.Parameters.Add(lpa_val3);
			RetV = odCmd.ExecuteNonQuery();
		}
		DbConn.Close();
	}

	public DataTable GetItemNameForCombo()
	{
		DataTable DT1 = new DataTable();
		DataTable DT2 = new DataTable();
		DataTable DT3 = new DataTable();
		DT3.Columns.Add("Kind", Type.GetType("System.String"));
		DT3.Columns.Add("Sample", Type.GetType("System.String"));
		string sSQL1 = "Select Distinct Kind From UserDefind  Where sno > 200000 ";
		string sSQL2 = "";
		string sSample = "";
		DbAdpt = new OleDbDataAdapter(sSQL1, DbConn);
		DbAdpt.Fill(DT1);
		for (int i = 0; i < DT1.Rows.Count; i++)
		{
			DT2.Clear();
			sSQL2 = "Select cString From UserDefind Where Kind ='" + DT1.Rows[i]["Kind"].ToString().Trim() + "' Order By sno ";
			DbAdpt = new OleDbDataAdapter(sSQL2, DbConn);
			DbAdpt.Fill(DT2);
			sSample = "";
			for (int j = 0; j < DT2.Rows.Count; j++)
			{
				if (j < 3)
				{
					sSample = sSample + DT2.Rows[j]["cString"].ToString().Trim() + ",";
					continue;
				}
				sSample += "...";
				break;
			}
			DataRow DR = DT3.NewRow();
			DR["Kind"] = DT1.Rows[i]["Kind"].ToString().Trim();
			DR["Sample"] = sSample;
			DT3.Rows.Add(DR);
		}
		return DT3;
	}

	public string LoadSettings()
	{
		string SQL = "Select SettingValue From UserSettings Where UserID = '" + F_FS_UserID + "' And FormName='" + F_FS_FormName + "' ";
		object Value = SqlUtility.ExecScale(F_SQLConnectionString, SQL);
		if (Value != DBNull.Value && Value != null)
		{
			return Value.ToString();
		}
		return "";
	}

	public void SaveSettings()
	{
		StringBuilder SB = new StringBuilder();
		SB.Append("\r\nif not exists (Select * from UserSettings where UserID='" + F_FS_UserID + "' and FormName='" + F_FS_FormName + "')\r\n\tInsert UserSettings (UserID, FormName, SettingValue) values ('" + F_FS_UserID + "','" + F_FS_FormName + "','" + F_FS_SettingValue + "')\r\nelse \r\n\tUpdate UserSettings Set SettingValue= '" + F_FS_SettingValue + "'  Where UserID = '" + F_FS_UserID + "' And FormName = '" + F_FS_FormName + "' ");
		SqlUtility.ExecSQL(F_SQLConnectionString, SB.ToString());
	}

	public int GetFuncListCount()
	{
		DataTable DTRet = new DataTable();
		string sSQL = "Select * from FuncList";
		DbAdpt = new OleDbDataAdapter(sSQL, DbConn);
		DbAdpt.Fill(DTRet);
		return DTRet.Rows.Count;
	}

	public bool ImportFuncList(string FileName)
	{
		try
		{
			DataSet DSImp = new DataSet("FuncList");
			DSImp.ReadXml(FileName);
			DataTable DT_Rows = DSImp.Tables["Rows"].Copy();
			OleDbCommand odCmd = new OleDbCommand();
			odCmd.Connection = DbConn;
			DbConn.Open();
			odCmd.CommandText = "Delete [dbo].[FuncList]";
			odCmd.ExecuteNonQuery();
			if (DT_Rows.Rows.Count > 0)
			{
				for (int i = 0; i < DT_Rows.Rows.Count; i++)
				{
					odCmd.CommandText = "Insert Into FuncList(FuncID, FuncName, Remark, Visible, SortOrder)  values('" + DT_Rows.Rows[i]["FuncID"].ToString().Trim() + "', '" + DT_Rows.Rows[i]["FuncName"].ToString().Trim() + "', '" + DT_Rows.Rows[i]["Remark"].ToString().Trim() + "', '" + DT_Rows.Rows[i]["Visible"].ToString().Trim() + "', '" + DT_Rows.Rows[i]["SortOrder"].ToString().Trim() + "') ";
					odCmd.ExecuteNonQuery();
				}
			}
			DbConn.Close();
			return true;
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "DBClass.cs" + ex.Message);
			DbConn.Close();
			MessageBox.Show(ex.Message);
			return false;
		}
	}

	public bool ImportFuncListAddOn(string FuncID, string FuncName, string sNum)
	{
		try
		{
			OleDbCommand odCmd = new OleDbCommand();
			odCmd.Connection = DbConn;
			DbConn.Open();
			if (sNum == "0")
			{
				odCmd.CommandText = "Delete FuncList where FuncID like 'F0060%'";
				odCmd.ExecuteNonQuery();
			}
			odCmd.CommandText = "Insert Into FuncList(FuncID, FuncName, Remark, Visible, SortOrder)  values('" + FuncID + "', '" + FuncName + "','Function','1','06')";
			odCmd.ExecuteNonQuery();
			DbConn.Close();
			return true;
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "DBClass.cs" + ex.Message);
			DbConn.Close();
			MessageBox.Show(ex.Message);
			return false;
		}
	}

	public bool UpdateAutoNumC(DataTable DT_C)
	{
		bool RetV = true;
		DbConn.Open();
		OleDbTransaction odTrans = DbConn.BeginTransaction();
		OleDbCommand odCmd1 = new OleDbCommand();
		OleDbCommand odCmd2 = new OleDbCommand();
		odCmd1.Connection = DbConn;
		odCmd2.Connection = DbConn;
		odCmd1.Transaction = odTrans;
		odCmd2.Transaction = odTrans;
		try
		{
			for (int i = 0; i < DT_C.Rows.Count; i++)
			{
				odCmd1.CommandText = "Delete From AutoNumC Where DeptID='" + DT_C.Rows[i]["DeptID"].ToString().Trim() + "' ";
				odCmd1.ExecuteNonQuery();
				odCmd2.CommandText = "Insert Into AutoNumC(DeptID, DeptName) values(?,?)";
				odCmd2.Parameters.Clear();
				odCmd2.Parameters.Add("P1", OleDbType.VarChar, 20);
				odCmd2.Parameters.Add("P2", OleDbType.VarWChar, 200);
				odCmd2.Parameters["P1"].Value = DT_C.Rows[i]["DeptID"].ToString().Trim();
				odCmd2.Parameters["P2"].Value = DT_C.Rows[i]["DeptName"].ToString().Trim();
				odCmd2.ExecuteNonQuery();
			}
			odTrans.Commit();
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "DBClass.cs" + ex.Message);
			odTrans.Rollback();
			Console.Write(ex.Message);
			RetV = false;
		}
		return RetV;
	}

	public bool UpdateNew_AutoNum(string itemCode, DataSet DS_Auto, string newActionID, string changeCode)
	{
		string ActionID = DS_Auto.Tables["AutoNumUpd"].Rows[0]["ActionID"].ToString().Trim();
		ActionID = newActionID;
		string sChangeCode = DS_Auto.Tables["AutoNumUpd"].Rows[0]["changeCode"].ToString().Trim();
		sChangeCode = changeCode;
		bool RetV = true;
		DbConn.Open();
		OleDbTransaction odTrans = DbConn.BeginTransaction();
		OleDbCommand odCmd1 = new OleDbCommand();
		OleDbCommand odCmd2 = new OleDbCommand();
		OleDbCommand odCmd3 = new OleDbCommand();
		odCmd1.Connection = DbConn;
		odCmd2.Connection = DbConn;
		odCmd3.Connection = DbConn;
		odCmd1.Transaction = odTrans;
		odCmd2.Transaction = odTrans;
		odCmd3.Transaction = odTrans;
		int iCount = 0;
		if (sChangeCode == "")
		{
			string sExt = "";
			if (DS_Auto.Tables["AutoNumA"].Rows.Count > 0)
			{
				sExt = DS_Auto.Tables["AutoNumA"].Rows[0]["Ext"].ToString().Trim();
			}
			try
			{
				if (ActionID == "D")
				{
					odCmd2.CommandText = "Delete From AutoNumA Where RTrim(itemCode)='" + itemCode + "' ";
					odCmd2.ExecuteNonQuery();
					odCmd2.CommandText = "Delete From AutoNumB Where RTrim(ChapCode)='" + itemCode + "' ";
					odCmd2.ExecuteNonQuery();
					odCmd3.CommandText = "Delete From AutoNumB_12 Where RTrim(ChapCode)='" + itemCode + "' ";
					odCmd3.ExecuteNonQuery();
				}
				else if (ActionID == "B")
				{
					if (sExt.Trim() == "")
					{
						int iTBL_B = DS_Auto.Tables["AutoNumB"].Rows.Count;
						odCmd1.CommandText = "Delete From AutoNumB Where RTrim(ChapCode)='" + itemCode + "' ";
						odCmd1.ExecuteNonQuery();
						for (int i = 0; i < iTBL_B; i++)
						{
							odCmd1.CommandText = "Select Count(*) from AutoNumB WITH (NOLOCK) Where RTrim(ChapCode)='" + DS_Auto.Tables["AutoNumB"].Rows[i]["ChapCode"].ToString().Trim() + "'  And Code='" + DS_Auto.Tables["AutoNumB"].Rows[i]["Code"].ToString().Trim() + "'  And CodeSection='" + DS_Auto.Tables["AutoNumB"].Rows[i]["CodeSection"].ToString().Trim() + "'  And SelfRow=" + DS_Auto.Tables["AutoNumB"].Rows[i]["SelfRow"].ToString().Trim() + "  And IsCustom ='" + DS_Auto.Tables["AutoNumB"].Rows[i]["IsCustom"].ToString().Trim() + "'  And Version ='" + DS_Auto.Tables["AutoNumB"].Rows[i]["Version"].ToString().Trim() + "' ";
							iCount = (int)odCmd1.ExecuteScalar();
							if (iCount > 0)
							{
								odCmd1.CommandText = "Select SelfDefine from AutoNumB WITH (NOLOCK) Where ChapCode='" + DS_Auto.Tables["AutoNumB"].Rows[i]["ChapCode"].ToString().Trim() + "'  And Code='" + DS_Auto.Tables["AutoNumB"].Rows[i]["Code"].ToString().Trim() + "'  And CodeSection='" + DS_Auto.Tables["AutoNumB"].Rows[i]["CodeSection"].ToString().Trim() + "'  And SelfRow=" + DS_Auto.Tables["AutoNumB"].Rows[i]["SelfRow"].ToString().Trim() + " ";
								string sSelfDefine = Convert.ToString(odCmd1.ExecuteScalar());
								if (!(sSelfDefine.ToUpper().Trim() == "Y"))
								{
									odCmd2.CommandText = "Update AutoNumB Set MinRow=?, MaxRow=?, Content=?, resType=? Where RTrim(ChapCode)='" + DS_Auto.Tables["AutoNumB"].Rows[i]["ChapCode"].ToString().Trim() + "'    And Code='" + DS_Auto.Tables["AutoNumB"].Rows[i]["Code"].ToString().Trim() + "'    And CodeSection='" + DS_Auto.Tables["AutoNumB"].Rows[i]["CodeSection"].ToString().Trim() + "'    And SelfRow=" + DS_Auto.Tables["AutoNumB"].Rows[i]["SelfRow"].ToString() + "  And IsCustom ='" + DS_Auto.Tables["AutoNumB"].Rows[i]["IsCustom"].ToString().Trim() + "'    And Version ='" + DS_Auto.Tables["AutoNumB"].Rows[i]["Version"].ToString().Trim() + "' ";
									odCmd2.Parameters.Clear();
									odCmd2.Parameters.Add("P1", OleDbType.Integer);
									odCmd2.Parameters.Add("P2", OleDbType.Integer);
									odCmd2.Parameters.Add("P3", OleDbType.VarWChar, 200);
									odCmd2.Parameters.Add("P4", OleDbType.Char, 1);
									odCmd2.Parameters["P1"].Value = (int)DS_Auto.Tables["AutoNumB"].Rows[i]["MinRow"];
									odCmd2.Parameters["P2"].Value = (int)DS_Auto.Tables["AutoNumB"].Rows[i]["MaxRow"];
									odCmd2.Parameters["P3"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["Content"].ToString();
									odCmd2.Parameters["P4"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["resType"].ToString();
									odCmd2.ExecuteNonQuery();
								}
							}
							else
							{
								odCmd2.CommandText = "Insert Into AutoNumB(ChapCode, Code, CodeSection, MinRow, MaxRow, SelfRow, Content, resType, IsCustom, Version)  values(?,?,?,?,?,?,?,?,?,?,?) ";
								odCmd2.Parameters.Clear();
								odCmd2.Parameters.Add("P1", OleDbType.Char, 10);
								odCmd2.Parameters.Add("P2", OleDbType.VarChar, 3);
								odCmd2.Parameters.Add("P3", OleDbType.Char, 2);
								odCmd2.Parameters.Add("P4", OleDbType.Integer);
								odCmd2.Parameters.Add("P5", OleDbType.Integer);
								odCmd2.Parameters.Add("P6", OleDbType.Integer);
								odCmd2.Parameters.Add("P7", OleDbType.VarWChar, 200);
								odCmd2.Parameters.Add("P8", OleDbType.Char, 1);
								odCmd2.Parameters.Add("P9", OleDbType.Char, 1);
								odCmd2.Parameters.Add("P10", OleDbType.VarChar, 20);
								odCmd2.Parameters["P1"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["ChapCode"].ToString();
								odCmd2.Parameters["P2"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["Code"];
								odCmd2.Parameters["P3"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["CodeSection"];
								odCmd2.Parameters["P4"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["MinRow"];
								odCmd2.Parameters["P5"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["MaxRow"];
								odCmd2.Parameters["P6"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["SelfRow"];
								odCmd2.Parameters["P7"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["Content"];
								odCmd2.Parameters["P8"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["resType"];
								odCmd2.Parameters["P9"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["IsCustom"];
								odCmd2.Parameters["P10"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["Version"];
								odCmd2.ExecuteNonQuery();
							}
						}
					}
					else
					{
						int iTBL_B = DS_Auto.Tables["AutoNumB_12"].Rows.Count;
						odCmd1.CommandText = "Delete From AutoNumB_12 Where RTrim(ChapCode)='" + itemCode + "' ";
						odCmd1.ExecuteNonQuery();
						for (int i = 0; i < iTBL_B; i++)
						{
							odCmd1.CommandText = "Select Count(*) from AutoNumB_12 WITH (NOLOCK) Where RTrim(ChapCode)='" + DS_Auto.Tables["AutoNumB_12"].Rows[i]["ChapCode"].ToString().Trim() + "'  And Code='" + DS_Auto.Tables["AutoNumB_12"].Rows[i]["Code"].ToString().Trim() + "'  And CodeSection='" + DS_Auto.Tables["AutoNumB_12"].Rows[i]["CodeSection"].ToString().Trim() + "'  And SelfRow=" + DS_Auto.Tables["AutoNumB_12"].Rows[i]["SelfRow"].ToString().Trim() + "  And IsCustom ='" + DS_Auto.Tables["AutoNumB_12"].Rows[i]["IsCustom"].ToString().Trim() + "'  And Version ='" + DS_Auto.Tables["AutoNumB_12"].Rows[i]["Version"].ToString().Trim() + "' ";
							iCount = (int)odCmd1.ExecuteScalar();
							if (iCount > 0)
							{
								odCmd1.CommandText = "Select SelfDefine from AutoNumB_12 WITH (NOLOCK) Where ChapCode='" + DS_Auto.Tables["AutoNumB_12"].Rows[i]["ChapCode"].ToString().Trim() + "'  And Code='" + DS_Auto.Tables["AutoNumB_12"].Rows[i]["Code"].ToString().Trim() + "'  And CodeSection='" + DS_Auto.Tables["AutoNumB_12"].Rows[i]["CodeSection"].ToString().Trim() + "'  And SelfRow=" + DS_Auto.Tables["AutoNumB_12"].Rows[i]["SelfRow"].ToString().Trim() + " ";
								string sSelfDefine = Convert.ToString(odCmd1.ExecuteScalar());
								if (!(sSelfDefine.ToUpper().Trim() == "Y"))
								{
									odCmd2.CommandText = "Update AutoNumB_12 Set MinRow=?, MaxRow=?, Content=?, resType=? Where RTrim(ChapCode)='" + DS_Auto.Tables["AutoNumB_12"].Rows[i]["ChapCode"].ToString().Trim() + "'    And Code='" + DS_Auto.Tables["AutoNumB_12"].Rows[i]["Code"].ToString().Trim() + "'    And CodeSection='" + DS_Auto.Tables["AutoNumB_12"].Rows[i]["CodeSection"].ToString().Trim() + "'    And SelfRow=" + DS_Auto.Tables["AutoNumB_12"].Rows[i]["SelfRow"].ToString() + "  And IsCustom ='" + DS_Auto.Tables["AutoNumB_12"].Rows[i]["IsCustom"].ToString().Trim() + "'    And Version ='" + DS_Auto.Tables["AutoNumB_12"].Rows[i]["Version"].ToString().Trim() + "' ";
									odCmd2.Parameters.Clear();
									odCmd2.Parameters.Add("P1", OleDbType.Integer);
									odCmd2.Parameters.Add("P2", OleDbType.Integer);
									odCmd2.Parameters.Add("P3", OleDbType.VarWChar, 200);
									odCmd2.Parameters.Add("P4", OleDbType.Char, 1);
									odCmd2.Parameters["P1"].Value = (int)DS_Auto.Tables["AutoNumB_12"].Rows[i]["MinRow"];
									odCmd2.Parameters["P2"].Value = (int)DS_Auto.Tables["AutoNumB_12"].Rows[i]["MaxRow"];
									odCmd2.Parameters["P3"].Value = DS_Auto.Tables["AutoNumB_12"].Rows[i]["Content"].ToString();
									odCmd2.Parameters["P4"].Value = DS_Auto.Tables["AutoNumB_12"].Rows[i]["resType"].ToString();
									odCmd2.ExecuteNonQuery();
								}
							}
							else
							{
								odCmd2.CommandText = "Insert Into AutoNumB_12(ChapCode, Code, CodeSection, MinRow, MaxRow, SelfRow, Content, resType, IsCustom, Version)  values(?,?,?,?,?,?,?,?,?,?,?) ";
								odCmd2.Parameters.Clear();
								odCmd2.Parameters.Add("P1", OleDbType.Char, 10);
								odCmd2.Parameters.Add("P2", OleDbType.VarChar, 3);
								odCmd2.Parameters.Add("P3", OleDbType.Char, 2);
								odCmd2.Parameters.Add("P4", OleDbType.Integer);
								odCmd2.Parameters.Add("P5", OleDbType.Integer);
								odCmd2.Parameters.Add("P6", OleDbType.Integer);
								odCmd2.Parameters.Add("P7", OleDbType.VarWChar, 200);
								odCmd2.Parameters.Add("P8", OleDbType.Char, 1);
								odCmd2.Parameters.Add("P9", OleDbType.Char, 1);
								odCmd2.Parameters.Add("P10", OleDbType.VarChar, 20);
								odCmd2.Parameters["P1"].Value = DS_Auto.Tables["AutoNumB_12"].Rows[i]["ChapCode"].ToString();
								odCmd2.Parameters["P2"].Value = DS_Auto.Tables["AutoNumB_12"].Rows[i]["Code"];
								odCmd2.Parameters["P3"].Value = DS_Auto.Tables["AutoNumB_12"].Rows[i]["CodeSection"];
								odCmd2.Parameters["P4"].Value = DS_Auto.Tables["AutoNumB_12"].Rows[i]["MinRow"];
								odCmd2.Parameters["P5"].Value = DS_Auto.Tables["AutoNumB_12"].Rows[i]["MaxRow"];
								odCmd2.Parameters["P6"].Value = DS_Auto.Tables["AutoNumB_12"].Rows[i]["SelfRow"];
								odCmd2.Parameters["P7"].Value = DS_Auto.Tables["AutoNumB_12"].Rows[i]["Content"];
								odCmd2.Parameters["P8"].Value = DS_Auto.Tables["AutoNumB_12"].Rows[i]["resType"];
								odCmd2.Parameters["P9"].Value = DS_Auto.Tables["AutoNumB_12"].Rows[i]["IsCustom"];
								odCmd2.Parameters["P10"].Value = DS_Auto.Tables["AutoNumB_12"].Rows[i]["Version"];
								odCmd2.ExecuteNonQuery();
							}
						}
					}
				}
				else
				{
					odCmd1.CommandText = "Select Count(*) from AutoNumA WITH (NOLOCK) Where RTrim(itemCode)='" + itemCode + "' ";
					iCount = (int)odCmd1.ExecuteScalar();
					if (iCount > 0)
					{
						odCmd2.CommandText = "Update AutoNumA Set levelNo=?, cName=?, IsShow=?, parent=?, WinFormFlag=?, AltUnit=?, commonName=?, Ext=?  Where RTrim(itemCode)='" + itemCode + "' ";
						odCmd2.Parameters.Clear();
						odCmd2.Parameters.Add("P1", OleDbType.Char, 1);
						odCmd2.Parameters.Add("P2", OleDbType.VarWChar, 200);
						odCmd2.Parameters.Add("P3", OleDbType.Char, 1);
						odCmd2.Parameters.Add("P4", OleDbType.Char, 10);
						odCmd2.Parameters.Add("P5", OleDbType.Char, 10);
						odCmd2.Parameters.Add("P6", OleDbType.VarChar, 10);
						odCmd2.Parameters.Add("P7", OleDbType.VarWChar, 200);
						odCmd2.Parameters.Add("P8", OleDbType.VarChar, 10);
						odCmd2.Parameters["P1"].Value = DS_Auto.Tables["AutoNumA"].Rows[0]["levelNo"].ToString().Trim();
						odCmd2.Parameters["P2"].Value = DS_Auto.Tables["AutoNumA"].Rows[0]["cName"].ToString().Trim();
						odCmd2.Parameters["P3"].Value = DS_Auto.Tables["AutoNumA"].Rows[0]["IsShow"].ToString().Trim();
						odCmd2.Parameters["P4"].Value = DS_Auto.Tables["AutoNumA"].Rows[0]["parent"].ToString().Trim();
						if (DS_Auto.Tables["AutoNumA"].Rows[0]["Version"].ToString().Trim() == "0")
						{
							odCmd2.Parameters["P5"].Value = DS_Auto.Tables["AutoNumA"].Rows[0]["WinFormFlag"].ToString().Trim();
						}
						else
						{
							odCmd2.Parameters["P5"].Value = DS_Auto.Tables["AutoNumA"].Rows[0]["Version"].ToString().Trim();
						}
						odCmd2.Parameters["P6"].Value = DS_Auto.Tables["AutoNumA"].Rows[0]["AltUnit"].ToString().Trim();
						odCmd2.Parameters["P7"].Value = DS_Auto.Tables["AutoNumA"].Rows[0]["commonName"].ToString().Trim();
						odCmd2.Parameters["P8"].Value = DS_Auto.Tables["AutoNumA"].Rows[0]["Ext"].ToString().Trim();
						odCmd2.ExecuteNonQuery();
					}
					else
					{
						odCmd2.CommandText = "Insert Into AutoNumA(itemCode, levelNo, cName, IsShow, parent, WinFormFlag, AltUnit, commonName, Ext)  values(?,?,?,?,?,?,?,?,?)";
						odCmd2.Parameters.Clear();
						odCmd2.Parameters.Add("P1", OleDbType.Char, 20);
						odCmd2.Parameters.Add("P2", OleDbType.Char, 1);
						odCmd2.Parameters.Add("P3", OleDbType.VarWChar, 200);
						odCmd2.Parameters.Add("P4", OleDbType.Char, 1);
						odCmd2.Parameters.Add("P5", OleDbType.Char, 10);
						odCmd2.Parameters.Add("P6", OleDbType.Char, 10);
						odCmd2.Parameters.Add("P7", OleDbType.VarChar, 10);
						odCmd2.Parameters.Add("P8", OleDbType.VarWChar, 200);
						odCmd2.Parameters.Add("P9", OleDbType.VarChar, 10);
						odCmd2.Parameters["P1"].Value = DS_Auto.Tables["AutoNumA"].Rows[0]["itemCode"].ToString().Trim();
						odCmd2.Parameters["P2"].Value = DS_Auto.Tables["AutoNumA"].Rows[0]["levelNo"].ToString().Trim();
						odCmd2.Parameters["P3"].Value = DS_Auto.Tables["AutoNumA"].Rows[0]["cName"].ToString().Trim();
						odCmd2.Parameters["P4"].Value = DS_Auto.Tables["AutoNumA"].Rows[0]["IsShow"].ToString().Trim();
						odCmd2.Parameters["P5"].Value = DS_Auto.Tables["AutoNumA"].Rows[0]["parent"].ToString().Trim();
						if (DS_Auto.Tables["AutoNumA"].Rows[0]["Version"].ToString().Trim() == "0")
						{
							odCmd2.Parameters["P6"].Value = DS_Auto.Tables["AutoNumA"].Rows[0]["WinFormFlag"].ToString().Trim();
						}
						else
						{
							odCmd2.Parameters["P6"].Value = DS_Auto.Tables["AutoNumA"].Rows[0]["Version"].ToString().Trim();
						}
						odCmd2.Parameters["P7"].Value = DS_Auto.Tables["AutoNumA"].Rows[0]["AltUnit"].ToString().Trim();
						odCmd2.Parameters["P8"].Value = DS_Auto.Tables["AutoNumA"].Rows[0]["commonName"].ToString().Trim();
						odCmd2.Parameters["P9"].Value = DS_Auto.Tables["AutoNumA"].Rows[0]["Ext"].ToString().Trim();
						odCmd2.ExecuteNonQuery();
					}
					odCmd1.CommandText = "Delete From AutoNumB Where RTrim(ChapCode)='" + itemCode + "' ";
					odCmd1.ExecuteNonQuery();
					odCmd1.CommandText = "Delete From AutoNumB_12 Where RTrim(ChapCode)='" + itemCode + "' ";
					odCmd1.ExecuteNonQuery();
					if (sExt.Trim() == "")
					{
						int iTBL_B = DS_Auto.Tables["AutoNumB"].Rows.Count;
						odCmd1.CommandText = "Delete From AutoNumB Where RTrim(ChapCode)='" + itemCode + "' ";
						odCmd1.ExecuteNonQuery();
						for (int i = 0; i < iTBL_B; i++)
						{
							odCmd1.CommandText = "Select Count(*) from AutoNumB WITH (NOLOCK) Where RTrim(ChapCode)='" + DS_Auto.Tables["AutoNumB"].Rows[i]["ChapCode"].ToString().Trim() + "'  And Code='" + DS_Auto.Tables["AutoNumB"].Rows[i]["Code"].ToString().Trim() + "'  And CodeSection='" + DS_Auto.Tables["AutoNumB"].Rows[i]["CodeSection"].ToString().Trim() + "'  And SelfRow=" + DS_Auto.Tables["AutoNumB"].Rows[i]["SelfRow"].ToString().Trim() + "  And IsCustom ='" + DS_Auto.Tables["AutoNumB"].Rows[i]["IsCustom"].ToString().Trim() + "'  And Version ='" + DS_Auto.Tables["AutoNumB"].Rows[i]["Version"].ToString().Trim() + "' ";
							iCount = (int)odCmd1.ExecuteScalar();
							if (iCount > 0)
							{
								odCmd1.CommandText = "Select SelfDefine from AutoNumB WITH (NOLOCK) Where ChapCode='" + DS_Auto.Tables["AutoNumB"].Rows[i]["ChapCode"].ToString().Trim() + "'  And Code='" + DS_Auto.Tables["AutoNumB"].Rows[i]["Code"].ToString().Trim() + "'  And CodeSection='" + DS_Auto.Tables["AutoNumB"].Rows[i]["CodeSection"].ToString().Trim() + "'  And SelfRow=" + DS_Auto.Tables["AutoNumB"].Rows[i]["SelfRow"].ToString().Trim() + " ";
								string sSelfDefine = Convert.ToString(odCmd1.ExecuteScalar());
								if (!(sSelfDefine.ToUpper().Trim() == "Y"))
								{
									odCmd2.CommandText = "Update AutoNumB Set MinRow=?, MaxRow=?, Content=?, resType=? Where RTrim(ChapCode)='" + DS_Auto.Tables["AutoNumB"].Rows[i]["ChapCode"].ToString().Trim() + "'    And Code='" + DS_Auto.Tables["AutoNumB"].Rows[i]["Code"].ToString().Trim() + "'    And CodeSection='" + DS_Auto.Tables["AutoNumB"].Rows[i]["CodeSection"].ToString().Trim() + "'    And SelfRow=" + DS_Auto.Tables["AutoNumB"].Rows[i]["SelfRow"].ToString() + "  And IsCustom ='" + DS_Auto.Tables["AutoNumB"].Rows[i]["IsCustom"].ToString().Trim() + "'    And Version ='" + DS_Auto.Tables["AutoNumB"].Rows[i]["Version"].ToString().Trim() + "' ";
									odCmd2.Parameters.Clear();
									odCmd2.Parameters.Add("P1", OleDbType.Integer);
									odCmd2.Parameters.Add("P2", OleDbType.Integer);
									odCmd2.Parameters.Add("P3", OleDbType.VarWChar, 200);
									odCmd2.Parameters.Add("P4", OleDbType.Char, 1);
									odCmd2.Parameters["P1"].Value = (int)DS_Auto.Tables["AutoNumB"].Rows[i]["MinRow"];
									odCmd2.Parameters["P2"].Value = (int)DS_Auto.Tables["AutoNumB"].Rows[i]["MaxRow"];
									odCmd2.Parameters["P3"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["Content"].ToString();
									odCmd2.Parameters["P4"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["resType"].ToString();
									odCmd2.ExecuteNonQuery();
								}
							}
							else
							{
								odCmd2.CommandText = "Insert Into AutoNumB(ChapCode, Code, CodeSection, MinRow, MaxRow, SelfRow, Content, resType, IsCustom, Version)  values(?,?,?,?,?,?,?,?,?,?) ";
								odCmd2.Parameters.Clear();
								odCmd2.Parameters.Add("P1", OleDbType.Char, 10);
								odCmd2.Parameters.Add("P2", OleDbType.VarChar, 3);
								odCmd2.Parameters.Add("P3", OleDbType.Char, 2);
								odCmd2.Parameters.Add("P4", OleDbType.Integer);
								odCmd2.Parameters.Add("P5", OleDbType.Integer);
								odCmd2.Parameters.Add("P6", OleDbType.Integer);
								odCmd2.Parameters.Add("P7", OleDbType.VarWChar, 200);
								odCmd2.Parameters.Add("P8", OleDbType.Char, 1);
								odCmd2.Parameters.Add("P9", OleDbType.Char, 1);
								odCmd2.Parameters.Add("P10", OleDbType.VarChar, 20);
								odCmd2.Parameters["P1"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["ChapCode"].ToString();
								odCmd2.Parameters["P2"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["Code"];
								odCmd2.Parameters["P3"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["CodeSection"];
								odCmd2.Parameters["P4"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["MinRow"];
								odCmd2.Parameters["P5"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["MaxRow"];
								odCmd2.Parameters["P6"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["SelfRow"];
								odCmd2.Parameters["P7"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["Content"];
								odCmd2.Parameters["P8"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["resType"];
								odCmd2.Parameters["P9"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["IsCustom"];
								odCmd2.Parameters["P10"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["Version"];
								odCmd2.ExecuteNonQuery();
							}
						}
					}
					else
					{
						int iTBL_B = DS_Auto.Tables["AutoNumB_12"].Rows.Count;
						odCmd1.CommandText = "Delete From AutoNumB_12 Where RTrim(ChapCode)='" + itemCode + "' ";
						odCmd1.ExecuteNonQuery();
						for (int i = 0; i < iTBL_B; i++)
						{
							odCmd1.CommandText = "Select Count(*) from AutoNumB_12 WITH (NOLOCK) Where RTrim(ChapCode)='" + DS_Auto.Tables["AutoNumB_12"].Rows[i]["ChapCode"].ToString().Trim() + "'  And Code='" + DS_Auto.Tables["AutoNumB_12"].Rows[i]["Code"].ToString().Trim() + "'  And CodeSection='" + DS_Auto.Tables["AutoNumB_12"].Rows[i]["CodeSection"].ToString().Trim() + "'  And SelfRow=" + DS_Auto.Tables["AutoNumB_12"].Rows[i]["SelfRow"].ToString().Trim() + "  And IsCustom ='" + DS_Auto.Tables["AutoNumB_12"].Rows[i]["IsCustom"].ToString().Trim() + "'  And Version ='" + DS_Auto.Tables["AutoNumB_12"].Rows[i]["Version"].ToString().Trim() + "' ";
							iCount = (int)odCmd1.ExecuteScalar();
							if (iCount > 0)
							{
								odCmd1.CommandText = "Select SelfDefine from AutoNumB_12 WITH (NOLOCK) Where ChapCode='" + DS_Auto.Tables["AutoNumB_12"].Rows[i]["ChapCode"].ToString().Trim() + "'  And Code='" + DS_Auto.Tables["AutoNumB_12"].Rows[i]["Code"].ToString().Trim() + "'  And CodeSection='" + DS_Auto.Tables["AutoNumB_12"].Rows[i]["CodeSection"].ToString().Trim() + "'  And SelfRow=" + DS_Auto.Tables["AutoNumB_12"].Rows[i]["SelfRow"].ToString().Trim() + " ";
								string sSelfDefine = Convert.ToString(odCmd1.ExecuteScalar());
								if (!(sSelfDefine.ToUpper().Trim() == "Y"))
								{
									odCmd2.CommandText = "Update AutoNumB_12 Set MinRow=?, MaxRow=?, Content=?, resType=? Where RTrim(ChapCode)='" + DS_Auto.Tables["AutoNumB_12"].Rows[i]["ChapCode"].ToString().Trim() + "'    And Code='" + DS_Auto.Tables["AutoNumB_12"].Rows[i]["Code"].ToString().Trim() + "'    And CodeSection='" + DS_Auto.Tables["AutoNumB_12"].Rows[i]["CodeSection"].ToString().Trim() + "'    And SelfRow=" + DS_Auto.Tables["AutoNumB_12"].Rows[i]["SelfRow"].ToString() + "  And IsCustom ='" + DS_Auto.Tables["AutoNumB_12"].Rows[i]["IsCustom"].ToString().Trim() + "'    And Version ='" + DS_Auto.Tables["AutoNumB_12"].Rows[i]["Version"].ToString().Trim() + "' ";
									odCmd2.Parameters.Clear();
									odCmd2.Parameters.Add("P1", OleDbType.Integer);
									odCmd2.Parameters.Add("P2", OleDbType.Integer);
									odCmd2.Parameters.Add("P3", OleDbType.VarWChar, 200);
									odCmd2.Parameters.Add("P4", OleDbType.Char, 1);
									odCmd2.Parameters["P1"].Value = (int)DS_Auto.Tables["AutoNumB_12"].Rows[i]["MinRow"];
									odCmd2.Parameters["P2"].Value = (int)DS_Auto.Tables["AutoNumB_12"].Rows[i]["MaxRow"];
									odCmd2.Parameters["P3"].Value = DS_Auto.Tables["AutoNumB_12"].Rows[i]["Content"].ToString();
									odCmd2.Parameters["P4"].Value = DS_Auto.Tables["AutoNumB_12"].Rows[i]["resType"].ToString();
									odCmd2.ExecuteNonQuery();
								}
							}
							else
							{
								odCmd2.CommandText = "Insert Into AutoNumB_12(ChapCode, Code, CodeSection, MinRow, MaxRow, SelfRow, Content, resType, IsCustom, Version)  values(?,?,?,?,?,?,?,?,?,?) ";
								odCmd2.Parameters.Clear();
								odCmd2.Parameters.Add("P1", OleDbType.Char, 10);
								odCmd2.Parameters.Add("P2", OleDbType.VarChar, 3);
								odCmd2.Parameters.Add("P3", OleDbType.Char, 2);
								odCmd2.Parameters.Add("P4", OleDbType.Integer);
								odCmd2.Parameters.Add("P5", OleDbType.Integer);
								odCmd2.Parameters.Add("P6", OleDbType.Integer);
								odCmd2.Parameters.Add("P7", OleDbType.VarWChar, 200);
								odCmd2.Parameters.Add("P8", OleDbType.Char, 1);
								odCmd2.Parameters.Add("P9", OleDbType.Char, 1);
								odCmd2.Parameters.Add("P10", OleDbType.VarChar, 20);
								odCmd2.Parameters["P1"].Value = DS_Auto.Tables["AutoNumB_12"].Rows[i]["ChapCode"].ToString();
								odCmd2.Parameters["P2"].Value = DS_Auto.Tables["AutoNumB_12"].Rows[i]["Code"];
								odCmd2.Parameters["P3"].Value = DS_Auto.Tables["AutoNumB_12"].Rows[i]["CodeSection"];
								odCmd2.Parameters["P4"].Value = DS_Auto.Tables["AutoNumB_12"].Rows[i]["MinRow"];
								odCmd2.Parameters["P5"].Value = DS_Auto.Tables["AutoNumB_12"].Rows[i]["MaxRow"];
								odCmd2.Parameters["P6"].Value = DS_Auto.Tables["AutoNumB_12"].Rows[i]["SelfRow"];
								odCmd2.Parameters["P7"].Value = DS_Auto.Tables["AutoNumB_12"].Rows[i]["Content"];
								odCmd2.Parameters["P8"].Value = DS_Auto.Tables["AutoNumB_12"].Rows[i]["resType"];
								odCmd2.Parameters["P9"].Value = DS_Auto.Tables["AutoNumB_12"].Rows[i]["IsCustom"];
								odCmd2.Parameters["P10"].Value = DS_Auto.Tables["AutoNumB_12"].Rows[i]["Version"];
								odCmd2.ExecuteNonQuery();
							}
						}
					}
				}
				odCmd1.CommandText = "Select Count(*) from AutoNumUpd Where RTrim(itemCode)='" + itemCode + "' ";
				iCount = (int)odCmd1.ExecuteScalar();
				iCount = 0;
				if (iCount > 0)
				{
					odCmd2.CommandText = "Update AutoNumUpd Set itemCode = ?, ReleaseDate = ?, ActionID = ?  Where itemCode = '" + itemCode + "' ";
					odCmd2.Parameters.Clear();
					odCmd2.Parameters.Add("P1", OleDbType.VarWChar, 20);
					odCmd2.Parameters.Add("P2", OleDbType.DBTimeStamp);
					odCmd2.Parameters.Add("P3", OleDbType.VarChar, 20);
					odCmd2.Parameters["P1"].Value = DS_Auto.Tables["AutoNumUpd"].Rows[0]["itemCode"].ToString().Trim();
					odCmd2.Parameters["P2"].Value = DS_Auto.Tables["AutoNumUpd"].Rows[0]["ReleaseDate"];
					odCmd2.Parameters["P3"].Value = DS_Auto.Tables["AutoNumUpd"].Rows[0]["ActionID"];
					odCmd2.ExecuteNonQuery();
				}
				else
				{
					odCmd2.CommandText = "Insert Into AutoNumUpd(itemCode, ReleaseDate, ActionID) values(?,?,?) ";
					odCmd2.Parameters.Clear();
					odCmd2.Parameters.Add("P1", OleDbType.Char, 20);
					odCmd2.Parameters.Add("P2", OleDbType.DBTimeStamp);
					odCmd2.Parameters.Add("P3", OleDbType.VarChar, 20);
					odCmd2.Parameters["P1"].Value = DS_Auto.Tables["AutoNumUpd"].Rows[0]["itemCode"].ToString().Trim();
					odCmd2.Parameters["P2"].Value = DS_Auto.Tables["AutoNumUpd"].Rows[0]["ReleaseDate"];
					odCmd2.Parameters["P3"].Value = newActionID;
					odCmd2.ExecuteNonQuery();
				}
				odTrans.Commit();
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "DBClass.cs" + ex.Message);
				odTrans.Rollback();
				Console.Write(ex.Message);
				CommonMethods.LogFile("C:\\AutoNum", "D", ex.Message);
				RetV = false;
			}
			finally
			{
				DbConn.Close();
			}
		}
		else
		{
			try
			{
				if (ActionID == "D")
				{
					odCmd2.CommandText = "Delete From AutoNumA_12 Where RTrim(itemCode)='" + itemCode + "' ";
					odCmd2.ExecuteNonQuery();
					odCmd2.CommandText = "Delete From AutoNumB_12 Where RTrim(ChapCode)='" + itemCode + "' ";
					odCmd2.ExecuteNonQuery();
				}
				else if (ActionID == "B")
				{
					int iTBL_B = DS_Auto.Tables["AutoNumB"].Rows.Count;
					odCmd1.CommandText = "Delete From AutoNumB_12 Where RTrim(ChapCode)='" + itemCode + "' ";
					odCmd1.ExecuteNonQuery();
					for (int i = 0; i < iTBL_B; i++)
					{
						odCmd1.CommandText = "Select Count(*) from AutoNumB_12 WITH (NOLOCK) Where RTrim(ChapCode)='" + DS_Auto.Tables["AutoNumB"].Rows[i]["ChapCode"].ToString().Trim() + "'  And Code='" + DS_Auto.Tables["AutoNumB"].Rows[i]["Code"].ToString().Trim() + "'  And CodeSection='" + DS_Auto.Tables["AutoNumB"].Rows[i]["CodeSection"].ToString().Trim() + "'  And SelfRow=" + DS_Auto.Tables["AutoNumB"].Rows[i]["SelfRow"].ToString().Trim() + "  And IsCustom ='" + DS_Auto.Tables["AutoNumB"].Rows[i]["IsCustom"].ToString().Trim() + "'  And Version ='" + DS_Auto.Tables["AutoNumB"].Rows[i]["Version"].ToString().Trim() + "' ";
						iCount = (int)odCmd1.ExecuteScalar();
						if (iCount > 0)
						{
							odCmd1.CommandText = "Select SelfDefine from AutoNumB_12 WITH (NOLOCK) Where ChapCode='" + DS_Auto.Tables["AutoNumB"].Rows[i]["ChapCode"].ToString().Trim() + "'  And Code='" + DS_Auto.Tables["AutoNumB"].Rows[i]["Code"].ToString().Trim() + "'  And CodeSection='" + DS_Auto.Tables["AutoNumB"].Rows[i]["CodeSection"].ToString().Trim() + "'  And SelfRow=" + DS_Auto.Tables["AutoNumB"].Rows[i]["SelfRow"].ToString().Trim() + " ";
							string sSelfDefine = Convert.ToString(odCmd1.ExecuteScalar());
							if (!(sSelfDefine.ToUpper().Trim() == "Y"))
							{
								odCmd2.CommandText = "Update AutoNumB_12 Set MinRow=?, MaxRow=?, Content=?, resType=? Where RTrim(ChapCode)='" + DS_Auto.Tables["AutoNumB"].Rows[i]["ChapCode"].ToString().Trim() + "'    And Code='" + DS_Auto.Tables["AutoNumB"].Rows[i]["Code"].ToString().Trim() + "'    And CodeSection='" + DS_Auto.Tables["AutoNumB"].Rows[i]["CodeSection"].ToString().Trim() + "'    And SelfRow=" + DS_Auto.Tables["AutoNumB"].Rows[i]["SelfRow"].ToString() + "  And IsCustom ='" + DS_Auto.Tables["AutoNumB"].Rows[i]["IsCustom"].ToString().Trim() + "'    And Version ='" + DS_Auto.Tables["AutoNumB"].Rows[i]["Version"].ToString().Trim() + "' ";
								odCmd2.Parameters.Clear();
								odCmd2.Parameters.Add("P1", OleDbType.Integer);
								odCmd2.Parameters.Add("P2", OleDbType.Integer);
								odCmd2.Parameters.Add("P3", OleDbType.VarWChar, 200);
								odCmd2.Parameters.Add("P4", OleDbType.Char, 1);
								odCmd2.Parameters["P1"].Value = (int)DS_Auto.Tables["AutoNumB"].Rows[i]["MinRow"];
								odCmd2.Parameters["P2"].Value = (int)DS_Auto.Tables["AutoNumB"].Rows[i]["MaxRow"];
								odCmd2.Parameters["P3"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["Content"].ToString();
								odCmd2.Parameters["P4"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["resType"].ToString();
								odCmd2.ExecuteNonQuery();
							}
						}
						else
						{
							odCmd2.CommandText = "Insert Into AutoNumB_12(ChapCode, Code, CodeSection, MinRow, MaxRow, SelfRow, Content, resType, IsCustom, Version)  values(?,?,?,?,?,?,?,?,?,?,?) ";
							odCmd2.Parameters.Clear();
							odCmd2.Parameters.Add("P1", OleDbType.Char, 10);
							odCmd2.Parameters.Add("P2", OleDbType.VarChar, 3);
							odCmd2.Parameters.Add("P3", OleDbType.Char, 2);
							odCmd2.Parameters.Add("P4", OleDbType.Integer);
							odCmd2.Parameters.Add("P5", OleDbType.Integer);
							odCmd2.Parameters.Add("P6", OleDbType.Integer);
							odCmd2.Parameters.Add("P7", OleDbType.VarWChar, 200);
							odCmd2.Parameters.Add("P8", OleDbType.Char, 1);
							odCmd2.Parameters.Add("P9", OleDbType.Char, 1);
							odCmd2.Parameters.Add("P10", OleDbType.VarChar, 20);
							odCmd2.Parameters["P1"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["ChapCode"].ToString();
							odCmd2.Parameters["P2"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["Code"];
							odCmd2.Parameters["P3"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["CodeSection"];
							odCmd2.Parameters["P4"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["MinRow"];
							odCmd2.Parameters["P5"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["MaxRow"];
							odCmd2.Parameters["P6"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["SelfRow"];
							odCmd2.Parameters["P7"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["Content"];
							odCmd2.Parameters["P8"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["resType"];
							odCmd2.Parameters["P9"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["IsCustom"];
							odCmd2.Parameters["P10"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["Version"];
							odCmd2.ExecuteNonQuery();
						}
					}
				}
				else
				{
					odCmd1.CommandText = "Select Count(*) from AutoNumA_12 WITH (NOLOCK) Where RTrim(itemCode)='" + itemCode + "' ";
					iCount = (int)odCmd1.ExecuteScalar();
					if (iCount > 0)
					{
						odCmd2.CommandText = "Update AutoNumA_12 Set levelNo=?, cName=?, IsShow=?, parent=?, WinFormFlag=?, AltUnit=?  Where RTrim(itemCode)='" + itemCode + "' ";
						odCmd2.Parameters.Clear();
						odCmd2.Parameters.Add("P1", OleDbType.Char, 1);
						odCmd2.Parameters.Add("P2", OleDbType.Char, 40);
						odCmd2.Parameters.Add("P3", OleDbType.Char, 1);
						odCmd2.Parameters.Add("P4", OleDbType.Char, 10);
						odCmd2.Parameters.Add("P5", OleDbType.Char, 10);
						odCmd2.Parameters.Add("P6", OleDbType.VarChar, 10);
						odCmd2.Parameters["P1"].Value = DS_Auto.Tables["AutoNumA"].Rows[0]["levelNo"].ToString().Trim();
						odCmd2.Parameters["P2"].Value = DS_Auto.Tables["AutoNumA"].Rows[0]["cName"].ToString().Trim();
						odCmd2.Parameters["P3"].Value = DS_Auto.Tables["AutoNumA"].Rows[0]["IsShow"].ToString().Trim();
						odCmd2.Parameters["P4"].Value = DS_Auto.Tables["AutoNumA"].Rows[0]["parent"].ToString().Trim();
						if (DS_Auto.Tables["AutoNumA"].Rows[0]["Version"].ToString().Trim() == "0")
						{
							odCmd2.Parameters["P5"].Value = DS_Auto.Tables["AutoNumA"].Rows[0]["WinFormFlag"].ToString().Trim();
						}
						else
						{
							odCmd2.Parameters["P5"].Value = DS_Auto.Tables["AutoNumA"].Rows[0]["Version"].ToString().Trim();
						}
						if (itemCode == "0000")
						{
							odCmd2.Parameters["P5"].Value = "2";
						}
						odCmd2.Parameters["P6"].Value = DS_Auto.Tables["AutoNumA"].Rows[0]["AltUnit"].ToString().Trim();
						odCmd2.ExecuteNonQuery();
					}
					else
					{
						odCmd2.CommandText = "Insert Into AutoNumA_12(itemCode, levelNo, cName, IsShow, parent, WinFormFlag, AltUnit)  values(?,?,?,?,?,?,?)";
						odCmd2.Parameters.Clear();
						odCmd2.Parameters.Add("P1", OleDbType.Char, 20);
						odCmd2.Parameters.Add("P2", OleDbType.Char, 1);
						odCmd2.Parameters.Add("P3", OleDbType.Char, 40);
						odCmd2.Parameters.Add("P4", OleDbType.Char, 1);
						odCmd2.Parameters.Add("P5", OleDbType.Char, 10);
						odCmd2.Parameters.Add("P6", OleDbType.Char, 10);
						odCmd2.Parameters.Add("P7", OleDbType.VarChar, 10);
						odCmd2.Parameters["P1"].Value = DS_Auto.Tables["AutoNumA"].Rows[0]["itemCode"].ToString().Trim();
						odCmd2.Parameters["P2"].Value = DS_Auto.Tables["AutoNumA"].Rows[0]["levelNo"].ToString().Trim();
						odCmd2.Parameters["P3"].Value = DS_Auto.Tables["AutoNumA"].Rows[0]["cName"].ToString().Trim();
						odCmd2.Parameters["P4"].Value = DS_Auto.Tables["AutoNumA"].Rows[0]["IsShow"].ToString().Trim();
						odCmd2.Parameters["P5"].Value = DS_Auto.Tables["AutoNumA"].Rows[0]["parent"].ToString().Trim();
						if (DS_Auto.Tables["AutoNumA"].Rows[0]["Version"].ToString().Trim() == "0")
						{
							odCmd2.Parameters["P6"].Value = DS_Auto.Tables["AutoNumA"].Rows[0]["WinFormFlag"].ToString().Trim();
						}
						else
						{
							odCmd2.Parameters["P6"].Value = DS_Auto.Tables["AutoNumA"].Rows[0]["Version"].ToString().Trim();
						}
						if (itemCode == "0000")
						{
							odCmd2.Parameters["P6"].Value = "2";
						}
						odCmd2.Parameters["P7"].Value = DS_Auto.Tables["AutoNumA"].Rows[0]["AltUnit"].ToString().Trim();
						odCmd2.ExecuteNonQuery();
					}
					int iTBL_B = DS_Auto.Tables["AutoNumB"].Rows.Count;
					odCmd1.CommandText = "Delete From AutoNumB_12 Where RTrim(ChapCode)='" + itemCode + "' ";
					odCmd1.ExecuteNonQuery();
					for (int i = 0; i < iTBL_B; i++)
					{
						odCmd1.CommandText = "Select Count(*) from AutoNumB_12 WITH (NOLOCK) Where RTrim(ChapCode)='" + DS_Auto.Tables["AutoNumB"].Rows[i]["ChapCode"].ToString().Trim() + "'  And Code='" + DS_Auto.Tables["AutoNumB"].Rows[i]["Code"].ToString().Trim() + "'  And CodeSection='" + DS_Auto.Tables["AutoNumB"].Rows[i]["CodeSection"].ToString().Trim() + "'  And SelfRow=" + DS_Auto.Tables["AutoNumB"].Rows[i]["SelfRow"].ToString().Trim() + "  And IsCustom ='" + DS_Auto.Tables["AutoNumB"].Rows[i]["IsCustom"].ToString().Trim() + "'  And Version ='" + DS_Auto.Tables["AutoNumB"].Rows[i]["Version"].ToString().Trim() + "' ";
						iCount = (int)odCmd1.ExecuteScalar();
						if (iCount > 0)
						{
							odCmd1.CommandText = "Select SelfDefine from AutoNumB_12 WITH (NOLOCK) Where ChapCode='" + DS_Auto.Tables["AutoNumB"].Rows[i]["ChapCode"].ToString().Trim() + "'  And Code='" + DS_Auto.Tables["AutoNumB"].Rows[i]["Code"].ToString().Trim() + "'  And CodeSection='" + DS_Auto.Tables["AutoNumB"].Rows[i]["CodeSection"].ToString().Trim() + "'  And SelfRow=" + DS_Auto.Tables["AutoNumB"].Rows[i]["SelfRow"].ToString().Trim() + " ";
							string sSelfDefine = Convert.ToString(odCmd1.ExecuteScalar());
							if (!(sSelfDefine.ToUpper().Trim() == "Y"))
							{
								odCmd2.CommandText = "Update AutoNumB_12 Set MinRow=?, MaxRow=?, Content=?, resType=? Where RTrim(ChapCode)='" + DS_Auto.Tables["AutoNumB"].Rows[i]["ChapCode"].ToString().Trim() + "'    And Code='" + DS_Auto.Tables["AutoNumB"].Rows[i]["Code"].ToString().Trim() + "'    And CodeSection='" + DS_Auto.Tables["AutoNumB"].Rows[i]["CodeSection"].ToString().Trim() + "'    And SelfRow=" + DS_Auto.Tables["AutoNumB"].Rows[i]["SelfRow"].ToString() + "  And IsCustom ='" + DS_Auto.Tables["AutoNumB"].Rows[i]["IsCustom"].ToString().Trim() + "'    And Version ='" + DS_Auto.Tables["AutoNumB"].Rows[i]["Version"].ToString().Trim() + "' ";
								odCmd2.Parameters.Clear();
								odCmd2.Parameters.Add("P1", OleDbType.Integer);
								odCmd2.Parameters.Add("P2", OleDbType.Integer);
								odCmd2.Parameters.Add("P3", OleDbType.VarWChar, 200);
								odCmd2.Parameters.Add("P4", OleDbType.Char, 1);
								odCmd2.Parameters["P1"].Value = (int)DS_Auto.Tables["AutoNumB"].Rows[i]["MinRow"];
								odCmd2.Parameters["P2"].Value = (int)DS_Auto.Tables["AutoNumB"].Rows[i]["MaxRow"];
								odCmd2.Parameters["P3"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["Content"].ToString();
								odCmd2.Parameters["P4"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["resType"].ToString();
								odCmd2.ExecuteNonQuery();
							}
						}
						else
						{
							odCmd2.CommandText = "Insert Into AutoNumB_12(ChapCode, Code, CodeSection, MinRow, MaxRow, SelfRow, Content, resType, IsCustom, Version)  values(?,?,?,?,?,?,?,?,?,?) ";
							odCmd2.Parameters.Clear();
							odCmd2.Parameters.Add("P1", OleDbType.Char, 10);
							odCmd2.Parameters.Add("P2", OleDbType.VarChar, 3);
							odCmd2.Parameters.Add("P3", OleDbType.Char, 2);
							odCmd2.Parameters.Add("P4", OleDbType.Integer);
							odCmd2.Parameters.Add("P5", OleDbType.Integer);
							odCmd2.Parameters.Add("P6", OleDbType.Integer);
							odCmd2.Parameters.Add("P7", OleDbType.VarWChar, 200);
							odCmd2.Parameters.Add("P8", OleDbType.Char, 1);
							odCmd2.Parameters.Add("P9", OleDbType.Char, 1);
							odCmd2.Parameters.Add("P10", OleDbType.VarChar, 20);
							odCmd2.Parameters["P1"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["ChapCode"].ToString();
							odCmd2.Parameters["P2"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["Code"];
							odCmd2.Parameters["P3"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["CodeSection"];
							odCmd2.Parameters["P4"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["MinRow"];
							odCmd2.Parameters["P5"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["MaxRow"];
							odCmd2.Parameters["P6"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["SelfRow"];
							odCmd2.Parameters["P7"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["Content"];
							odCmd2.Parameters["P8"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["resType"];
							odCmd2.Parameters["P9"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["IsCustom"];
							odCmd2.Parameters["P10"].Value = DS_Auto.Tables["AutoNumB"].Rows[i]["Version"];
							odCmd2.ExecuteNonQuery();
						}
					}
				}
				odCmd1.CommandText = "Select Count(*) from AutoNumUpd Where RTrim(itemCode)='" + itemCode + "' ";
				iCount = (int)odCmd1.ExecuteScalar();
				iCount = 0;
				if (iCount > 0)
				{
					odCmd2.CommandText = "Update AutoNumUpd Set itemCode = ?, ReleaseDate = ?, ActionID = ?  Where itemCode = '" + itemCode + "' ";
					odCmd2.Parameters.Clear();
					odCmd2.Parameters.Add("P1", OleDbType.VarWChar, 20);
					odCmd2.Parameters.Add("P2", OleDbType.DBTimeStamp);
					odCmd2.Parameters.Add("P3", OleDbType.VarChar, 20);
					odCmd2.Parameters["P1"].Value = DS_Auto.Tables["AutoNumUpd"].Rows[0]["itemCode"].ToString().Trim();
					odCmd2.Parameters["P2"].Value = DS_Auto.Tables["AutoNumUpd"].Rows[0]["ReleaseDate"];
					odCmd2.Parameters["P3"].Value = DS_Auto.Tables["AutoNumUpd"].Rows[0]["ActionID"];
					odCmd2.ExecuteNonQuery();
				}
				else
				{
					odCmd2.CommandText = "Insert Into AutoNumUpd(itemCode, ReleaseDate, ActionID) values(?,?,?) ";
					odCmd2.Parameters.Clear();
					odCmd2.Parameters.Add("P1", OleDbType.Char, 20);
					odCmd2.Parameters.Add("P2", OleDbType.DBTimeStamp);
					odCmd2.Parameters.Add("P3", OleDbType.VarChar, 20);
					odCmd2.Parameters["P1"].Value = DS_Auto.Tables["AutoNumUpd"].Rows[0]["itemCode"].ToString().Trim();
					odCmd2.Parameters["P2"].Value = DS_Auto.Tables["AutoNumUpd"].Rows[0]["ReleaseDate"];
					odCmd2.Parameters["P3"].Value = newActionID;
					odCmd2.ExecuteNonQuery();
				}
				odTrans.Commit();
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "DBClass.cs" + ex.Message);
				odTrans.Rollback();
				Console.Write(ex.Message);
				CommonMethods.LogFile("C:\\AutoNum", "D", ex.Message);
				RetV = false;
			}
			finally
			{
				DbConn.Close();
			}
		}
		return RetV;
	}

	public DataTable GetAutoNumA_By_KeyWord(string sKeyword)
	{
		string sSQL = "SELECT Distinct B.ChapCode, A.cName  FROM AutoNumB B Left Join AutoNumA A On A.itemCode = B.ChapCode Where Content Like '%" + sKeyword + "%' ";
		DataTable DT_RetV = new DataTable();
		OleDbConnection myConnection = new OleDbConnection(ConfigurationManager.AppSettings["Conn"]);
		OleDbDataAdapter odAdpt = new OleDbDataAdapter(sSQL, myConnection);
		odAdpt.Fill(DT_RetV);
		return DT_RetV;
	}

	public DataTable GetAutoNumY_Alias(DataTable myDT, string sKeyword)
	{
		DataTable DT_Ret2 = myDT.Copy();
		string sSQL = "SELECT A.*, B.surName FROM AutoNumA A   LEFT Join AutoNumY B ON A.itemCode = B.ItemCode  WHERE B.surName Like '%" + sKeyword + "%' ";
		DataTable DT_RetV = new DataTable();
		OleDbConnection myConnection = new OleDbConnection(ConfigurationManager.AppSettings["Conn"]);
		OleDbDataAdapter odAdpt = new OleDbDataAdapter(sSQL, myConnection);
		odAdpt.Fill(DT_RetV);
		myDT.CaseSensitive = true;
		DataView DV1 = myDT.DefaultView;
		for (int i = 0; i < DT_RetV.Rows.Count; i++)
		{
			DV1.RowFilter = "ChapCode ='" + DT_RetV.Rows[i]["itemCode"].ToString() + "' ";
			if (DV1.Count == 0)
			{
				DataRow DR = DT_Ret2.NewRow();
				DR["ChapCode"] = DT_RetV.Rows[i]["itemCode"];
				DR["cName"] = DT_RetV.Rows[i]["cName"].ToString().Trim() + "(" + DT_RetV.Rows[i]["surName"].ToString() + ")";
				DT_Ret2.Rows.Add(DR);
			}
		}
		return DT_Ret2;
	}

	public DataTable GetAutoNum_CodeName(DataTable myDT, string sKeyword)
	{
		DataTable DT_Ret2 = myDT.Copy();
		string sSQL = "SELECT A.* FROM AutoNumA A  WHERE A.cName Like '%" + sKeyword + "%' ";
		DataTable DT_RetV = new DataTable();
		OleDbConnection myConnection = new OleDbConnection(ConfigurationManager.AppSettings["Conn"]);
		OleDbDataAdapter odAdpt = new OleDbDataAdapter(sSQL, myConnection);
		odAdpt.Fill(DT_RetV);
		myDT.CaseSensitive = true;
		DataView DV1 = myDT.DefaultView;
		for (int i = 0; i < DT_RetV.Rows.Count; i++)
		{
			DV1.RowFilter = "ChapCode ='" + DT_RetV.Rows[i]["itemCode"].ToString() + "' ";
			if (DV1.Count == 0)
			{
				DataRow DR = DT_Ret2.NewRow();
				DR["ChapCode"] = DT_RetV.Rows[i]["itemCode"];
				DR["cName"] = DT_RetV.Rows[i]["cName"].ToString().Trim();
				DT_Ret2.Rows.Add(DR);
			}
		}
		return DT_Ret2;
	}

	public DataTable GetAutoNumB()
	{
		DataTable DT_GetAutoNumB_Cust = new DataTable("AutoNumB_Cust");
		string sSQL = "\r\n(Select \r\n\tchapcode, code, codesection, minrow, maxrow, selfrow, content, (cast (restype as [varchar](10))) as restype, selfdefine, iscustom, version\r\nFrom AutoNumB Where Code <> '' And chapCode <>'27') union --abert 2020-07-03 排除垃圾資料 27-瀝青混凝土舖裝機, AutoNumB不該有27的資料\r\n(Select \r\n\tchapcode, code, codesection, minrow, maxrow, selfrow, content, (cast (restype as [varchar](10))) as restype, selfdefine, iscustom, version\r\nFrom AutoNumB_12 Where Code <>'')  \r\n                ";
		DbAdpt = new OleDbDataAdapter(sSQL, DbConn);
		DbAdpt.Fill(DT_GetAutoNumB_Cust);
		return DT_GetAutoNumB_Cust;
	}

	public DataTable GetAutoNumA()
	{
		DataTable DT_GetAutoNumA_Cust = new DataTable("AutoNumA_Cust");
		string sSQL = "Select * From AutoNumA Where levelNo = 2 And itemCode <>'27' ";
		DbAdpt = new OleDbDataAdapter(sSQL, DbConn);
		DbAdpt.Fill(DT_GetAutoNumA_Cust);
		return DT_GetAutoNumA_Cust;
	}

	public DataTable GetMrsBaseA()
	{
		DataTable DT_GetMrsBaseA = new DataTable("MrsBaseA");
		string sSQL = "select pccesCode , cName, unitName from MrsBaseA";
		DbAdpt = new OleDbDataAdapter(sSQL, DbConn);
		DbAdpt.Fill(DT_GetMrsBaseA);
		return DT_GetMrsBaseA;
	}

	public DataSet GetAutoNumB(string ChapCode)
	{
		OleDbConnection myConnection = new OleDbConnection(ConfigurationManager.AppSettings["Conn"]);
		OleDbDataAdapter myCommand = new OleDbDataAdapter("GetAutoNumB", myConnection);
		myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;
		OleDbParameter paramChapCode = new OleDbParameter("@ChapCode", OleDbType.Char, 20);
		paramChapCode.Value = ChapCode;
		myCommand.SelectCommand.Parameters.Add(paramChapCode);
		DataSet DS1 = new DataSet();
		myCommand.Fill(DS1);
		return DS1;
	}

	public DataSet GetAutoNumB_12(string ChapCode)
	{
		OleDbConnection myConnection = new OleDbConnection(ConfigurationManager.AppSettings["Conn"]);
		OleDbDataAdapter myCommand = new OleDbDataAdapter("GetAutoNumB_12", myConnection);
		myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;
		OleDbParameter paramChapCode = new OleDbParameter("@ChapCode", OleDbType.Char, 20);
		paramChapCode.Value = ChapCode;
		myCommand.SelectCommand.Parameters.Add(paramChapCode);
		DataSet DS1 = new DataSet();
		myCommand.Fill(DS1);
		return DS1;
	}

	public DataSet GetAutoNumB_12M(string ChapCode)
	{
		OleDbConnection myConnection = new OleDbConnection(ConfigurationManager.AppSettings["Conn"]);
		OleDbDataAdapter myCommand = new OleDbDataAdapter("GetAutoNumB_12M", myConnection);
		myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;
		OleDbParameter paramChapCode = new OleDbParameter("@ChapCode", OleDbType.Char, 20);
		paramChapCode.Value = ChapCode;
		myCommand.SelectCommand.Parameters.Add(paramChapCode);
		DataSet DS1 = new DataSet();
		myCommand.Fill(DS1);
		return DS1;
	}

	public DataSet GetAutoNumB(string ChapCode, string Version)
	{
		OleDbConnection myConnection = new OleDbConnection(ConfigurationManager.AppSettings["Conn"]);
		OleDbDataAdapter myCommand = new OleDbDataAdapter("GetAutoNumB", myConnection);
		myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;
		OleDbParameter paramChapCode = new OleDbParameter("@ChapCode", OleDbType.Char, 10);
		paramChapCode.Value = ChapCode;
		myCommand.SelectCommand.Parameters.Add(paramChapCode);
		OleDbParameter paramIsCustom = new OleDbParameter("@IsCustom", OleDbType.Char, 1);
		paramIsCustom.Value = 'Y';
		myCommand.SelectCommand.Parameters.Add(paramIsCustom);
		OleDbParameter paramVersion = new OleDbParameter("@Version", OleDbType.VarChar, 20);
		paramVersion.Value = Version;
		myCommand.SelectCommand.Parameters.Add(paramVersion);
		DataSet DS1 = new DataSet();
		myCommand.Fill(DS1);
		return DS1;
	}

	public string SaveAutoCodes(string[,] Codes)
	{
		string RetV = "";
		string sExist = "";
		string sFail = "";
		ArrayList al = new ArrayList();
		al.Add(F_FS_UserID);
		ModifyDB StdCom = new ModifyDB("", al);
		string ls_connstr = StdCom.ls_connstr;
		OleDbConnection myConnection = new OleDbConnection(ls_connstr);
		OleDbCommand odCmd = new OleDbCommand();
		odCmd.Connection = myConnection;
		myConnection.Open();
		for (int i = 0; i < Codes.GetLength(0); i++)
		{
			DataTable DT_Qry = GetUserDefine("Select * from MrsBaseA Where PccesCode = '" + Codes[i, 0].Trim() + "' ");
			if (DT_Qry.Rows.Count > 0)
			{
				sExist = sExist + Codes[i, 0].Trim() + "\n";
				continue;
			}
			string lRate = "0";
			string eRate = "0";
			string mRate = "0";
			string wRate = "0";
			if (Codes[i, 0].Length > 0)
			{
				switch (Codes[i, 0].Substring(0, 1).ToUpper())
				{
				case "L":
					lRate = "100";
					break;
				case "E":
					eRate = "100";
					break;
				case "M":
					mRate = "100";
					break;
				case "W":
					wRate = "100";
					break;
				}
			}
			odCmd.CommandText = $"Insert Into MrsBaseA(pccesCode, cName, unitName, cost, rate, lRate, eRate, mRate, wRate, memo, Post, surName) values(?,?,?,0,0,{lRate},{eRate},{mRate},{wRate},?,'1',?)";
			OleDbParameter lpa_val1 = new OleDbParameter("?P1", OleDbType.VarChar, 20);
			lpa_val1.Direction = ParameterDirection.Input;
			lpa_val1.Value = Codes[i, 0].Trim();
			odCmd.Parameters.Add(lpa_val1);
			OleDbParameter lpa_val2 = new OleDbParameter("?P2", OleDbType.VarWChar, 200);
			lpa_val2.Direction = ParameterDirection.Input;
			lpa_val2.Value = Codes[i, 1].Trim();
			odCmd.Parameters.Add(lpa_val2);
			OleDbParameter lpa_val3 = new OleDbParameter("?P3", OleDbType.Char, 10);
			lpa_val3.Direction = ParameterDirection.Input;
			lpa_val3.Value = Codes[i, 2].Trim();
			odCmd.Parameters.Add(lpa_val3);
			OleDbParameter lpa_val4 = new OleDbParameter("?P4", OleDbType.Char, 10);
			lpa_val4.Direction = ParameterDirection.Input;
			lpa_val4.Value = Codes[i, 3].Trim();
			odCmd.Parameters.Add(lpa_val4);
			OleDbParameter lpa_val5 = new OleDbParameter("?P5", OleDbType.VarWChar, 200);
			lpa_val5.Direction = ParameterDirection.Input;
			lpa_val5.Value = Codes[i, 4].Trim();
			odCmd.Parameters.Add(lpa_val5);
			int iRec = odCmd.ExecuteNonQuery();
			if (iRec <= 0)
			{
				sFail = sFail + Codes[i, 0].Trim() + "\n";
			}
		}
		myConnection.Close();
		if (sExist == "" && sFail == "")
		{
			RetV = "";
		}
		else if (sExist != "" && sFail == "")
		{
			RetV = "已存在的碼有：" + sExist;
		}
		else if (sExist == "" && sFail != "")
		{
			RetV = "新增失敗的碼有：" + sFail;
		}
		return RetV;
	}

	public string SaveAutoCodes2(string[,] Codes)
	{
		string RetV = "";
		string sTmp = "";
		string sExist = "";
		string sFail = "";
		OleDbConnection myConnection = new OleDbConnection(ConfigurationManager.AppSettings["Conn"]);
		OleDbDataAdapter myCommand = new OleDbDataAdapter("SaveCodes", myConnection);
		myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;
		OleDbParameter paramChapCode = new OleDbParameter("@ChapCode", OleDbType.Char, 11);
		OleDbParameter paramCName = new OleDbParameter("@CName", OleDbType.VarWChar, 200);
		OleDbParameter paramCUnit = new OleDbParameter("@CUnit", OleDbType.VarWChar, 20);
		OleDbParameter paramtrStatus = new OleDbParameter("@trStatus", OleDbType.VarWChar, 220);
		paramtrStatus.Direction = ParameterDirection.Output;
		for (int i = 0; i < Codes.GetLength(0); i++)
		{
			myCommand.SelectCommand.Parameters.Clear();
			paramChapCode.Value = Codes[i, 0].Trim();
			paramCName.Value = Codes[i, 1].Trim();
			paramCUnit.Value = Codes[i, 2].Trim();
			myCommand.SelectCommand.Parameters.Add(paramChapCode);
			myCommand.SelectCommand.Parameters.Add(paramCName);
			myCommand.SelectCommand.Parameters.Add(paramCUnit);
			myCommand.SelectCommand.Parameters.Add(paramtrStatus);
			myConnection.Open();
			myCommand.SelectCommand.ExecuteNonQuery();
			myConnection.Close();
			sTmp = paramtrStatus.Value.ToString();
			if (sTmp.Substring(0, 1) == "1")
			{
				sExist = sExist + sTmp.Substring(2).Trim() + "\n";
			}
			else if (sTmp.Substring(0, 1) == "2")
			{
				sFail = sFail + sTmp.Substring(2).Trim() + "\n";
			}
		}
		if (sExist == "" && sFail == "")
		{
			RetV = "";
		}
		else if (sExist != "" && sFail == "")
		{
			RetV = "已存在的碼有：" + sExist;
		}
		else if (sExist == "" && sFail != "")
		{
			RetV = "新增失敗的碼有：" + sFail;
		}
		return RetV;
	}

	private string GetSharedConnection()
	{
		string ls_connstr = PubTools.GetAppSet_String("Conn");
		if (ls_connstr.Length == 0)
		{
			ls_connstr = "Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=pcces;Data Source=.";
		}
		return ls_connstr;
	}

	private string GetSpecificConnection(string DbName)
	{
		string ls_connstr = PubTools.GetAppSet_String("Conn");
		OleDbConnection DbConn = new OleDbConnection(ls_connstr);
		DbConn.Open();
		string Std_Database = DbConn.Database;
		if (DbName != null && DbName.Trim().Length > 0)
		{
			int aa = ls_connstr.ToUpper().IndexOf("=" + Std_Database.ToUpper());
			if (aa > -1)
			{
				ls_connstr = ls_connstr.Substring(0, aa) + "=" + DbName + ls_connstr.Substring(aa + Std_Database.Length + 1);
			}
		}
		return ls_connstr;
	}

	public DataTable GetDataBySpeciDBName(string sSQL, string DBName)
	{
		DataTable DTRet = new DataTable();
		string sConn = GetSpecificConnection(DBName);
		DbConn.ConnectionString = sConn;
		DbAdpt = new OleDbDataAdapter(sSQL, DbConn);
		DbAdpt.Fill(DTRet);
		return DTRet;
	}

	private string GetMultiUserConnection2()
	{
		return GetMultiUserConnection2(F_FS_UserID);
	}

	public string GetMultiUserConnection2(string sUser)
	{
		ArrayList al = new ArrayList();
		al.Add(sUser);
		ModifyDB StdCom = new ModifyDB("", al);
		return StdCom.SQLConnectionString;
	}

	private string GetMultiUserConnection()
	{
		ArrayList al = new ArrayList();
		al.Add(F_FS_UserID);
		ModifyDB Stdcom = new ModifyDB("", al);
		return Stdcom.ls_connstr;
	}

	public string GetMultiUserConnection(string sUser)
	{
		ArrayList al = new ArrayList();
		al.Add(sUser);
		ModifyDB StdCom = new ModifyDB("", al);
		return StdCom.ls_connstr;
	}

	public string GetDBConnectionServer()
	{
		string ls_connstr = PubTools.GetAppSet_String("Conn");
		ConnectionStringUtility connectionUtility = new ConnectionStringUtility(ls_connstr);
		return connectionUtility.Server;
	}

	public void CheckMrsSchema(string SrcKind)
	{
		string sPrefix = "";
		if (SrcKind.ToUpper() == "BUD")
		{
			sPrefix = "BUD";
		}
		if (SrcKind.ToUpper() == "BID")
		{
			sPrefix = "BID";
		}
		if (SrcKind.ToUpper() == "MRS" || SrcKind.ToUpper() == "")
		{
			sPrefix = "";
		}
		string sSQL = "";
		sSQL = ((!(sPrefix.Trim() != "")) ? "Select * From MrsBaseA Where 1=0 " : ("Select * From " + sPrefix + "ProjMrsA Where 1=0 "));
		DataTable DTTmp = GetUserDefine(sSQL);
		if (DTTmp.Columns.IndexOf("ModLock") == -1)
		{
			SysUser oSysUser = new SysUser();
			string CurrentDBName = oSysUser.GetSysUserDatabaseName(F_FS_UserID);
			ChgStru ChgStru = new ChgStru();
			ChgStru.ModifyDatabaseStructure(CurrentDBName);
		}
	}

	public bool MrsBase_Lock(string pubCode, string ProjectCode, string SrcKind)
	{
		bool RetV = false;
		string sPrefix = "";
		if (SrcKind.ToUpper() == "BUD")
		{
			sPrefix = "BUD";
		}
		if (SrcKind.ToUpper() == "BID")
		{
			sPrefix = "BID";
		}
		if (SrcKind.ToUpper() == "MRS" || SrcKind.ToUpper() == "")
		{
			sPrefix = "";
		}
		string sSQL = "";
		sSQL = ((!(sPrefix.Trim() != "")) ? ("Update MrsBaseA set ModLock = '" + F_FS_UserID + "' Where pubCode=" + pubCode + " ") : ("Update " + sPrefix + "ProjMrsA set ModLock = '" + F_FS_UserID + "' Where pubCode=" + pubCode + " And projectCode='" + ProjectCode + "' "));
		int iAffect = ExecuteCommand(sSQL);
		if (iAffect > 0)
		{
			return true;
		}
		return false;
	}

	public bool MrsBase_UnLock(string pubCode, string ProjectCode, string SrcKind)
	{
		bool RetV = false;
		string sPrefix = "";
		if (SrcKind.ToUpper() == "BUD")
		{
			sPrefix = "BUD";
		}
		if (SrcKind.ToUpper() == "BID")
		{
			sPrefix = "BID";
		}
		if (SrcKind.ToUpper() == "MRS" || SrcKind.ToUpper() == "")
		{
			sPrefix = "";
		}
		string sSQL = "";
		sSQL = ((!(sPrefix.Trim() != "")) ? ("Update MrsBaseA set ModLock = '' Where pubCode=" + pubCode + " ") : ("Update " + sPrefix + "ProjMrsA set ModLock = '' Where pubCode=" + pubCode + " And projectCode='" + ProjectCode + "' "));
		int iAffect = ExecuteCommand(sSQL);
		if (iAffect > 0)
		{
			return true;
		}
		return false;
	}

	public string GetMrsBaseACostKind(string ProjectCode, string PccesCode, string SrcKind)
	{
		string sPrefix = "";
		if (SrcKind.ToUpper() == "BUD")
		{
			sPrefix = "BUD";
		}
		if (SrcKind.ToUpper() == "BID")
		{
			sPrefix = "BID";
		}
		if (SrcKind.ToUpper() == "MRS" || SrcKind.ToUpper() == "")
		{
			sPrefix = "";
		}
		string sSQL = "";
		sSQL = ((!(sPrefix.Trim() != "")) ? ("Select costKind from MrsBaseA Where pccesCode='" + PccesCode + "' ") : ("Select costKind from " + sPrefix + "ProjMrsA Where pccesCode='" + PccesCode + "' And projectCode='" + ProjectCode + "' "));
		return GetUserDefine_String(sSQL, "costKind");
	}

	public bool MrsBase_CanEdit(string pubCode, string ProjectCode, string SrcKind)
	{
		CheckMrsSchema(SrcKind);
		bool RetV = false;
		string sPrefix = "";
		if (SrcKind.ToUpper() == "BUD")
		{
			sPrefix = "BUD";
		}
		if (SrcKind.ToUpper() == "BID")
		{
			sPrefix = "BID";
		}
		if (SrcKind.ToUpper() == "MRS" || SrcKind.ToUpper() == "")
		{
			sPrefix = "";
		}
		string sSQL = "";
		sSQL = ((!(sPrefix.Trim() != "")) ? ("Select ModLock from MrsBaseA Where pubCode=" + pubCode + " ") : ("Select ModLock from " + sPrefix + "ProjMrsA Where pubCode=" + pubCode + " And projectCode='" + ProjectCode + "' "));
		string sLock = GetUserDefine_String(sSQL, "ModLock");
		if (sLock.Trim() == "" || sLock.Trim() == F_FS_UserID)
		{
			return true;
		}
		return false;
	}

	public bool MrsBase_UnLockAll(string ProjectCode, string SrcKind)
	{
		bool RetV = false;
		string sPrefix = "";
		if (SrcKind.ToUpper() == "BUD")
		{
			sPrefix = "BUD";
		}
		if (SrcKind.ToUpper() == "BID")
		{
			sPrefix = "BID";
		}
		if (SrcKind.ToUpper() == "MRS" || SrcKind.ToUpper() == "")
		{
			sPrefix = "";
		}
		string sSQL = "";
		sSQL = ((!(sPrefix.Trim() != "")) ? ("Update MrsBaseA set ModLock = '' Where ModLock='" + F_FS_UserID + "' ") : ("Update " + sPrefix + "ProjMrsA set ModLock = '' Where ModLock='" + F_FS_UserID + "' And projectCode='" + ProjectCode + "' "));
		int iAffect = ExecuteCommand(sSQL);
		if (iAffect > 0)
		{
			return true;
		}
		return false;
	}

	public DataRow GetOccupieData(string sPubCode, string ProjectCode, string SrcKind)
	{
		DataRow RetV = null;
		string sPrefix = "";
		if (SrcKind.ToUpper() == "BUD")
		{
			sPrefix = "BUD";
		}
		if (SrcKind.ToUpper() == "BID")
		{
			sPrefix = "BID";
		}
		if (SrcKind.ToUpper() == "MRS" || SrcKind.ToUpper() == "")
		{
			sPrefix = "";
		}
		string sSQL = "";
		sSQL = ((!(sPrefix.Trim() != "")) ? ("Select ModLock as UserID, '' as UserName, PccesCode, CName From MrsBaseA Where pubCode=" + sPubCode + " ") : ("Select ModLock as UserID, '' as UserName, PccesCode, CName From " + sPrefix + "ProjMrsA Where pubCode=" + sPubCode + " And projectCode='" + ProjectCode + "' "));
		DataTable DT_Get = GetUserDefine(sSQL);
		if (DT_Get.Rows.Count > 0)
		{
			string sUID = DT_Get.Rows[0]["UserID"].ToString().Trim();
			string sUName = GetUserDefine_String("Select UserName From SysUser Where UserID ='" + sUID + "' ", "UserName");
			DT_Get.Rows[0]["UserName"] = sUName;
			RetV = DT_Get.Rows[0];
		}
		return RetV;
	}

	public DataRow GetOccupieData(string ProjectCode, string SrcKind)
	{
		DataRow RetV = null;
		string sPrefix = "";
		if (SrcKind.ToUpper() == "BUD")
		{
			sPrefix = "BUD";
		}
		if (SrcKind.ToUpper() == "BID")
		{
			sPrefix = "BID";
		}
		if (SrcKind.ToUpper() == "MRS" || SrcKind.ToUpper() == "")
		{
			sPrefix = "";
		}
		string sSQL = "";
		sSQL = ((!(sPrefix.Trim() != "")) ? ("Select ModLock as UserID, '' as UserName, PccesCode, CName From MrsBaseA Where ModLock <> '" + F_FS_UserID + "' And (ModLock <> '' And ModLock is Not null)") : ("Select ModLock as UserID, '' as UserName, PccesCode, CName From " + sPrefix + "ProjMrsA Where projectCode='" + ProjectCode + "' And ModLock <> '" + F_FS_UserID + "' And (ModLock <> '' And ModLock is Not null)"));
		DataTable DT_Get = GetUserDefine(sSQL);
		if (DT_Get.Rows.Count > 0)
		{
			string sUID = DT_Get.Rows[0]["UserID"].ToString().Trim();
			string sUName = GetUserDefine_String("Select UserName From SysUser Where UserID ='" + sUID + "' ", "UserName");
			DT_Get.Rows[0]["UserName"] = sUName;
			RetV = DT_Get.Rows[0];
		}
		return RetV;
	}

	public DataRow GetItemAOccupieData(string sSNo, string ProjectCode, string SrcKind)
	{
		DataRow RetV = null;
		string sPrefix = "";
		if (SrcKind.ToUpper() == "BUD")
		{
			sPrefix = "BUD";
		}
		if (SrcKind.ToUpper() == "BID")
		{
			sPrefix = "BID";
		}
		string sSQL = "";
		if (sPrefix.Trim() != "")
		{
			sSQL = "Select ModLock as UserID, '' as UserName, ItemNo as PccesCode, CName From " + sPrefix + "ItemA Where sNO=" + sSNo + " And projectCode='" + ProjectCode + "' ";
		}
		DataTable DT_Get = GetUserDefine(sSQL);
		if (DT_Get.Rows.Count > 0)
		{
			string sUID = DT_Get.Rows[0]["UserID"].ToString().Trim();
			string sUName = GetUserDefine_String("Select UserName From SysUser Where UserID ='" + sUID + "' ", "UserName");
			DT_Get.Rows[0]["UserName"] = sUName;
			RetV = DT_Get.Rows[0];
		}
		return RetV;
	}

	private void CheckItemASchema(string SrcKind)
	{
		string sPrefix = "";
		if (SrcKind.ToUpper() == "BUD")
		{
			sPrefix = "BUD";
		}
		if (SrcKind.ToUpper() == "BID")
		{
			sPrefix = "BID";
		}
		if (SrcKind.ToUpper() == "SUB")
		{
			sPrefix = "SUB";
		}
		if (SrcKind.ToUpper() == "SUBCHG")
		{
			sPrefix = "SUBCHG";
		}
		string sSQL = "";
		if (sPrefix.Trim() != "")
		{
			sSQL = "Select * From " + sPrefix + "ItemA Where 1=0 ";
		}
		DataTable DTTmp = GetUserDefine(sSQL);
		if (DTTmp.Columns.IndexOf("ModLock") == -1)
		{
			SysUser oSysUser = new SysUser();
			string CurrentDBName = oSysUser.GetSysUserDatabaseName(F_FS_UserID);
			ChgStru ChgStru = new ChgStru();
			ChgStru.ModifyDatabaseStructure(CurrentDBName);
		}
	}

	public bool ItemA_Lock(string sNo, string ProjectCode, string SrcKind)
	{
		bool RetV = false;
		string sPrefix = "";
		if (SrcKind.ToUpper() == "BUD")
		{
			sPrefix = "BUD";
		}
		if (SrcKind.ToUpper() == "BID")
		{
			sPrefix = "BID";
		}
		if (SrcKind.ToUpper() == "SUB")
		{
			sPrefix = "SUB";
		}
		if (SrcKind.ToUpper() == "SUBCHG")
		{
			sPrefix = "SUBCHG";
		}
		string sSQL = "";
		if (sPrefix.Trim() != "")
		{
			sSQL = "Update " + sPrefix + "ItemA set ModLock = '";
		}
		sSQL = sSQL + F_FS_UserID.Trim() + "' Where sno=";
		sSQL = sSQL + sNo + " And projectCode='";
		sSQL = sSQL + ProjectCode + "' ";
		if (sPrefix == "SUBCHG")
		{
			sSQL = sSQL + " and chgCount=" + F_Issue.ToString() + " ";
		}
		int iAffect = ExecuteCommand(sSQL);
		if (iAffect > 0)
		{
			return true;
		}
		return false;
	}

	public int GetPrintNoCount(string ProjectCode, string SrcKind, string PrintNo)
	{
		int retV = 0;
		string sPrefix = "";
		if (SrcKind.ToUpper() == "BUD")
		{
			sPrefix = "BUD";
		}
		if (SrcKind.ToUpper() == "BID")
		{
			sPrefix = "BID";
		}
		if (SrcKind.ToUpper() == "SUB")
		{
			sPrefix = "SUB";
		}
		if (SrcKind.ToUpper() == "SUBCHG")
		{
			sPrefix = "SUBCHG";
		}
		string sSQL = "";
		if (sPrefix == "")
		{
			sPrefix = "BUD";
		}
		if (sPrefix.Trim() != "")
		{
			sSQL = "Select Count(*) as iCount from " + sPrefix + "ItemA Where ProjectCode='" + ProjectCode + "'  and PrintNo like '" + PrintNo + "%' and PrintNo <> '" + PrintNo + "' and Len(PrintNo) =" + (PrintNo.Length + 4) + "";
			retV = int.Parse(GetUserDefine_String(sSQL, "iCount"));
		}
		return retV;
	}

	public bool ItemA_UnLock(string sNo, string ProjectCode, string SrcKind)
	{
		bool RetV = false;
		string sPrefix = "";
		if (SrcKind.ToUpper() == "BUD")
		{
			sPrefix = "BUD";
		}
		if (SrcKind.ToUpper() == "BID")
		{
			sPrefix = "BID";
		}
		if (SrcKind.ToUpper() == "SUB")
		{
			sPrefix = "SUB";
		}
		if (SrcKind.ToUpper() == "SUBCHG")
		{
			sPrefix = "SUBCHG";
		}
		string sSQL = "";
		if (sPrefix.Trim() != "")
		{
			sSQL = "Update " + sPrefix + "ItemA set ModLock = '' Where sno=" + sNo + " And projectCode='" + ProjectCode + "' ";
		}
		int iAffect = ExecuteCommand(sSQL);
		if (iAffect > 0)
		{
			return true;
		}
		return false;
	}

	public bool IsPccesCodeExistsInMrsBaseA(string PccesCode, string ProjectCode, string SrcKind)
	{
		bool RetV = false;
		string sPrefix = "";
		if (SrcKind.ToUpper() == "BUD")
		{
			sPrefix = "BUD";
		}
		if (SrcKind.ToUpper() == "BID")
		{
			sPrefix = "BID";
		}
		if (SrcKind.ToUpper() == "SUB")
		{
			sPrefix = "SUB";
		}
		if (SrcKind.ToUpper() == "SUBCHG")
		{
			sPrefix = "SUBCHG";
		}
		string sSQL = "";
		if (sPrefix.Trim() != "")
		{
			sSQL = "Select cName from " + sPrefix + "ProjMrsA Where projectCode='" + ProjectCode + "' and PccesCode='" + PccesCode + "'";
		}
		string sCName = GetUserDefine_String(sSQL, "cName");
		if (sCName.Trim() != "")
		{
			RetV = true;
		}
		return RetV;
	}

	public bool ItemA_CanEdit(string sNo, string ProjectCode, string SrcKind)
	{
		CheckItemASchema(SrcKind);
		bool RetV = false;
		string sPrefix = "";
		if (SrcKind.ToUpper() == "BUD")
		{
			sPrefix = "BUD";
		}
		if (SrcKind.ToUpper() == "BID")
		{
			sPrefix = "BID";
		}
		if (SrcKind.ToUpper() == "SUB")
		{
			sPrefix = "SUB";
		}
		if (SrcKind.ToUpper() == "SUBCHG")
		{
			sPrefix = "SUBCHG";
		}
		string sSQL = "";
		if (sPrefix.Trim() != "")
		{
			sSQL = "Select ModLock from " + sPrefix + "ItemA Where sno=" + sNo + " And projectCode='" + ProjectCode + "' ";
		}
		string sLock = GetUserDefine_String(sSQL, "ModLock");
		if (sLock.Trim() == "" || sLock.Trim() == F_FS_UserID)
		{
			return true;
		}
		return false;
	}

	public bool ItemA_UnLockAll(string ProjectCode, string SrcKind)
	{
		bool RetV = false;
		string sPrefix = "";
		if (SrcKind.ToUpper() == "BUD")
		{
			sPrefix = "BUD";
		}
		if (SrcKind.ToUpper() == "BID")
		{
			sPrefix = "BID";
		}
		string sSQL = "";
		if (sPrefix.Trim() != "")
		{
			sSQL = "Update " + sPrefix + "ItemA set ModLock = '' Where ModLock='" + F_FS_UserID + "' And projectCode='" + ProjectCode + "' ";
		}
		int iAffect = ExecuteCommand(sSQL);
		if (iAffect > 0)
		{
			return true;
		}
		return false;
	}

	public DataTable GetSpeciDb_MrsBaseA(string DbName)
	{
		string sSQL = "Select * from mrsBaseA";
		DataTable DTRet = new DataTable();
		string sConn = GetSpecificConnection(DbName);
		DbConn.ConnectionString = sConn;
		DbAdpt = new OleDbDataAdapter(sSQL, DbConn);
		DbAdpt.Fill(DTRet);
		return DTRet;
	}

	public string GetInvoCode(string ProjectCode)
	{
		string retV = "";
		DataTable DT_GetSubInfo = new DataTable("SubInfo");
		string sSQL = "Select * From SubInfo Where ProjectCode = '" + ProjectCode + "'";
		DbAdpt = new OleDbDataAdapter(sSQL, DbConn);
		DbAdpt.Fill(DT_GetSubInfo);
		if (DT_GetSubInfo.Rows.Count > 0)
		{
			retV = DT_GetSubInfo.Rows[0]["InvoCode"].ToString();
		}
		return retV;
	}

	public string GetProjectDescription(string ProjectCode)
	{
		string retV = "";
		DataTable DT_GetBudProject = new DataTable("budProject");
		string sSQL = "Select * From budProject Where ProjectCode = '" + ProjectCode + "'";
		DbAdpt = new OleDbDataAdapter(sSQL, DbConn);
		DbAdpt.Fill(DT_GetBudProject);
		if (DT_GetBudProject.Rows.Count > 0)
		{
			retV = DT_GetBudProject.Rows[0]["projectDescription"].ToString();
		}
		return retV;
	}
}
