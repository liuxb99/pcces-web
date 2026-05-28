using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;

namespace Archnowledge.Pcces.PccesMain.MrsBase.PickFromOther;

public class ClsPickFromOtherDB
{
	private string F_ExistProcessType = "1";

	private string F_DBProcessType = "1";

	private DBClass DBCLS = new DBClass();

	private int F_PrgsMin = 0;

	private int F_PrgsMax = 0;

	private int F_PrgsCurr = 0;

	private DataTable F_DT_SrcForProc;

	private DataTable F_DT_MrsBaseA = new DataTable();

	private DataTable F_DT_MrsBaseC = new DataTable();

	private string F_UserID = "";

	private string F_CurrentDBName = "";

	private PccesFormAction F_ActionName;

	public string _ExistProcessType
	{
		get
		{
			return F_ExistProcessType;
		}
		set
		{
			F_ExistProcessType = value;
		}
	}

	public string _DBProcessType
	{
		get
		{
			return F_DBProcessType;
		}
		set
		{
			F_DBProcessType = value;
		}
	}

	public int _PrgsMin
	{
		get
		{
			return F_PrgsMin;
		}
		set
		{
			F_PrgsMin = value;
		}
	}

	public int _PrgsMax
	{
		get
		{
			return F_PrgsMax;
		}
		set
		{
			F_PrgsMax = value;
		}
	}

	public int _PrgsCurr => F_PrgsCurr;

	public DataTable _DT_SrcForProc
	{
		set
		{
			F_DT_SrcForProc = value;
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
			DBCLS._FS_UserID = F_UserID;
		}
	}

	public string _CurrentDBName
	{
		set
		{
			F_CurrentDBName = value;
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

	public bool ExecuteProcess(DataTable DT_SrcCollect)
	{
		bool RetV = true;
		try
		{
			ArrayList aArr = new ArrayList();
			aArr.Add(F_UserID);
			aArr.Add("WinFORM 基本工料");
			DataTable srcDT = new DataTable();
			srcDT.Columns.Add("pubCode", Type.GetType("System.Int32"));
			if (DT_SrcCollect.Rows.Count > 0)
			{
				for (int j = 0; j < DT_SrcCollect.Rows.Count; j++)
				{
					DataRow srcDR = srcDT.NewRow();
					srcDR["PubCode"] = DT_SrcCollect.Rows[j]["PubCode"];
					srcDT.Rows.Add(srcDR);
				}
				string ssDBName = DT_SrcCollect.Rows[0]["DBName"].ToString().Trim();
				string ssProjectCode = DT_SrcCollect.Rows[0]["ProjectCode"].ToString().Trim();
				ReSet2Mrs RESET2 = new ReSet2Mrs(aArr);
				DataSet trgDS = RESET2.GetDataSet(ssDBName, CommonMethods.GetActionNameString(F_ActionName), ssProjectCode, srcDT, 1);
				if (F_DBProcessType == "0")
				{
					RESET2.InputDataSet(F_CurrentDBName, CommonMethods.GetActionNameString(F_ActionName), "", trgDS, 0, "");
				}
				else
				{
					RESET2.InputDataSet(F_CurrentDBName, CommonMethods.GetActionNameString(F_ActionName), "", trgDS, 1, "");
				}
			}
			srcDT = null;
			aArr = null;
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "MrsBase.PickFromOther.ClsPickFromOtherDB.cs" + ex.Message);
			RetV = false;
		}
		return RetV;
	}

	public bool ExecuteProcess()
	{
		F_DT_MrsBaseA = DBCLS.GetDataBySpeciDBName("select A.*, B.listNo, B.qty as BQty, B.cost AS BCost, B.amount as BAmount, B.ItemNo as BItemNo  from mrsBaseA A Left join mrsBaseB B on A.pubCode = B.pubCode  Where 1=0 ", F_CurrentDBName);
		F_DT_MrsBaseA.Columns.Add("mrsType", Type.GetType("System.String"));
		F_DT_MrsBaseA.Columns.Add("mrsParent", Type.GetType("System.String"));
		F_DT_MrsBaseA.Columns.Add("mrsNewPubCode", Type.GetType("System.Int32"));
		F_DT_MrsBaseA.Columns.Add("mrsExist", Type.GetType("System.String"));
		F_DT_MrsBaseC = DBCLS.GetDataBySpeciDBName(" Select PubListNo, ItemListNo, (Select pccesCode from MrsBaseA Where pubCode = A.parentCode) as ParentCodePcces, (Select pccesCode from MrsBaseA Where pubCode = A.pubCode)    as PubCodePcces, (Select pccesCode from MrsBaseA Where pubCode = A.itemCode)   as itemCodePcces,  0 as New_ParentCode, 0 as New_PubCode, 0 as New_itemCode  From MrsBaseC A  Where 1=0 ", F_CurrentDBName);
		F_DT_MrsBaseC.Columns.Add("mrsExist", Type.GetType("System.String"));
		string sDbName = "";
		string sPccesCode = "";
		int iPubCode = -1;
		string sSQL = "";
		DataTable DT_Tmp1 = new DataTable();
		for (int i = 0; i < F_DT_SrcForProc.Rows.Count; i++)
		{
			if (F_DT_SrcForProc.Rows[i]["Analysis"].ToString().Trim() == "0")
			{
				sDbName = F_DT_SrcForProc.Rows[i]["DbName"].ToString().Trim();
				sPccesCode = F_DT_SrcForProc.Rows[i]["PccesCode"].ToString().Trim();
				sSQL = "Select * From MrsBaseA Where PccesCode ='" + sPccesCode + "'";
				DT_Tmp1 = DBCLS.GetDataBySpeciDBName(sSQL, sDbName);
				FillIntoDTMrsA(DT_Tmp1.Rows[0], "0", "");
			}
			else if (F_DT_SrcForProc.Rows[i]["Analysis"].ToString().Trim() == "1")
			{
				sDbName = F_DT_SrcForProc.Rows[i]["DbName"].ToString().Trim();
				sPccesCode = F_DT_SrcForProc.Rows[i]["PccesCode"].ToString().Trim();
				iPubCode = Convert.ToInt32(F_DT_SrcForProc.Rows[i]["PubCode"]);
				sSQL = "Select * From MrsBaseA Where PccesCode ='" + sPccesCode + "'";
				DT_Tmp1 = DBCLS.GetDataBySpeciDBName(sSQL, sDbName);
				FillIntoDTMrsA(DT_Tmp1.Rows[0], "1", sPccesCode);
				FillIntoDTMrsC(DT_Tmp1.Rows[0], sDbName);
				ProcessSub(iPubCode, sPccesCode, sDbName);
			}
		}
		SaveIntoSQL();
		return true;
	}

	private void ProcessSub(int iParentPubCode, string sParentPccesCode, string sDbName)
	{
		string sSQL = "select A.*, B.listNo, B.qty as BQty, B.cost AS BCost, B.amount as BAmount, B.ItemNo as BItemNo  from mrsBaseA A Left join mrsBaseB B on A.pubCode = B.pubCode  Where B.ParentCode = " + iParentPubCode;
		DataTable DT_Tmp2 = DBCLS.GetDataBySpeciDBName(sSQL, sDbName);
		for (int i = 0; i < DT_Tmp2.Rows.Count; i++)
		{
			string sPccesCode = DT_Tmp2.Rows[i]["PccesCode"].ToString().Trim();
			string sAnalysis = DT_Tmp2.Rows[i]["Analysis"].ToString().Trim();
			int iPubCode = Convert.ToInt32(DT_Tmp2.Rows[i]["pubCode"]);
			if (sAnalysis == "0")
			{
				FillIntoDTMrsA(DT_Tmp2.Rows[i], "0", sParentPccesCode);
			}
			else if (sAnalysis == "1")
			{
				FillIntoDTMrsA(DT_Tmp2.Rows[i], "1", sParentPccesCode);
				FillIntoDTMrsC(DT_Tmp2.Rows[i], sDbName);
				ProcessSub(iPubCode, sPccesCode, sDbName);
			}
		}
	}

	private void FillIntoDTMrsA(DataRow DR1, string sAnalysis, string sParentPcces)
	{
		DataRow DR2 = F_DT_MrsBaseA.NewRow();
		for (int j = 0; j < DR1.Table.Columns.Count; j++)
		{
			DR2[DR1.Table.Columns[j].ColumnName] = DR1[DR1.Table.Columns[j].ColumnName];
		}
		if (sAnalysis == "1")
		{
			DR2["mrsType"] = "Y";
		}
		if (sAnalysis == "0")
		{
			DR2["mrsType"] = "N";
		}
		DR2["mrsParent"] = sParentPcces;
		F_DT_MrsBaseA.Rows.Add(DR2);
	}

	private void FillIntoDTMrsC(DataRow DR1, string DBName)
	{
		string sPubCode = DR1["pubCode"].ToString().Trim();
		string sSQL = " Select PubListNo, ItemListNo, (Select pccesCode from MrsBaseA Where pubCode = A.parentCode) as ParentCodePcces, (Select pccesCode from MrsBaseA Where pubCode = A.pubCode)    as PubCodePcces, (Select pccesCode from MrsBaseA Where pubCode = A.itemCode)   as itemCodePcces,  0 as New_ParentCode, 0 as New_PubCode, 0 as New_itemCode  From MrsBaseC A  Where A.ParentCode = " + sPubCode;
		DataTable DT_TmpC = DBCLS.GetDataBySpeciDBName(sSQL, DBName);
		for (int i = 0; i < DT_TmpC.Rows.Count; i++)
		{
			DataRow DR_C = F_DT_MrsBaseC.NewRow();
			for (int j = 0; j < DT_TmpC.Columns.Count; j++)
			{
				DR_C[DT_TmpC.Columns[j].ColumnName] = DT_TmpC.Rows[i][DT_TmpC.Columns[j].ColumnName];
			}
			F_DT_MrsBaseC.Rows.Add(DR_C);
		}
	}

	private void SaveIntoSQL()
	{
		OleDbCommand oleCmd = new OleDbCommand();
		for (int i = 0; i < F_DT_MrsBaseA.Rows.Count; i++)
		{
			string sSQL = "Select pubCode From MrsBaseA Where PccesCode ='" + F_DT_MrsBaseA.Rows[i]["pccesCode"].ToString().Trim() + "' ";
			string sPubCode = DBCLS.GetUserDefine_String(sSQL, "pubCode");
			if (sPubCode != "")
			{
				F_DT_MrsBaseA.Rows[i]["mrsNewPubCode"] = Convert.ToInt32(sPubCode);
				F_DT_MrsBaseA.Rows[i]["mrsExist"] = "Y";
				if (!(F_ExistProcessType == "2"))
				{
				}
				continue;
			}
			oleCmd.CommandText = "Insert Into MrsBaseA(resCode,pccesCode,cName,eName,memo, unitName,resType,mRate,lRate,eRate, wRate,analysis,cost,analysisQty,rate, costKind,accountCode1,accountCode2,xNameE,xNameC, eUnit,extendCode,Post,ins_usr) values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?) ";
			oleCmd.Parameters.Clear();
			oleCmd.Parameters.Add("P1", OleDbType.Char, 20);
			oleCmd.Parameters.Add("P2", OleDbType.Char, 20);
			oleCmd.Parameters.Add("P3", OleDbType.VarChar, 200);
			oleCmd.Parameters.Add("P4", OleDbType.VarChar, 200);
			oleCmd.Parameters.Add("P5", OleDbType.VarChar, 200);
			oleCmd.Parameters.Add("P6", OleDbType.Char, 10);
			oleCmd.Parameters.Add("P7", OleDbType.Char, 4);
			oleCmd.Parameters.Add("P8", OleDbType.Double);
			oleCmd.Parameters.Add("P9", OleDbType.Double);
			oleCmd.Parameters.Add("P10", OleDbType.Double);
			oleCmd.Parameters.Add("P11", OleDbType.Double);
			oleCmd.Parameters.Add("P12", OleDbType.Char, 1);
			oleCmd.Parameters.Add("P13", OleDbType.Double);
			oleCmd.Parameters.Add("P14", OleDbType.Double);
			oleCmd.Parameters.Add("P15", OleDbType.Double);
			oleCmd.Parameters.Add("P16", OleDbType.Char, 1);
			oleCmd.Parameters.Add("P17", OleDbType.Char, 20);
			oleCmd.Parameters.Add("P18", OleDbType.Char, 20);
			oleCmd.Parameters.Add("P19", OleDbType.VarChar, 110);
			oleCmd.Parameters.Add("P20", OleDbType.VarChar, 110);
			oleCmd.Parameters.Add("P21", OleDbType.Char, 20);
			oleCmd.Parameters.Add("P22", OleDbType.Char, 20);
			oleCmd.Parameters.Add("P23", OleDbType.Char, 1);
			oleCmd.Parameters.Add("P24", OleDbType.Char, 10);
			oleCmd.Parameters["P1"].Value = F_DT_MrsBaseA.Rows[i]["resCode"].ToString().Trim();
			oleCmd.Parameters["P2"].Value = F_DT_MrsBaseA.Rows[i]["pccesCode"].ToString().Trim();
			oleCmd.Parameters["P3"].Value = F_DT_MrsBaseA.Rows[i]["cName"].ToString().Trim();
			oleCmd.Parameters["P4"].Value = F_DT_MrsBaseA.Rows[i]["eName"].ToString().Trim();
			oleCmd.Parameters["P5"].Value = F_DT_MrsBaseA.Rows[i]["memo"].ToString().Trim();
			oleCmd.Parameters["P6"].Value = F_DT_MrsBaseA.Rows[i]["unitName"].ToString().Trim();
			oleCmd.Parameters["P7"].Value = F_DT_MrsBaseA.Rows[i]["resType"].ToString().Trim();
			oleCmd.Parameters["P8"].Value = F_DT_MrsBaseA.Rows[i]["mRate"];
			oleCmd.Parameters["P9"].Value = F_DT_MrsBaseA.Rows[i]["lRate"];
			oleCmd.Parameters["P10"].Value = F_DT_MrsBaseA.Rows[i]["eRate"];
			oleCmd.Parameters["P11"].Value = F_DT_MrsBaseA.Rows[i]["wRate"];
			oleCmd.Parameters["P12"].Value = F_DT_MrsBaseA.Rows[i]["analysis"].ToString().Trim();
			oleCmd.Parameters["P13"].Value = F_DT_MrsBaseA.Rows[i]["cost"];
			oleCmd.Parameters["P14"].Value = F_DT_MrsBaseA.Rows[i]["analysisQty"];
			oleCmd.Parameters["P15"].Value = F_DT_MrsBaseA.Rows[i]["rate"];
			oleCmd.Parameters["P16"].Value = F_DT_MrsBaseA.Rows[i]["costKind"].ToString().Trim();
			oleCmd.Parameters["P17"].Value = F_DT_MrsBaseA.Rows[i]["accountCode1"].ToString().Trim();
			oleCmd.Parameters["P18"].Value = F_DT_MrsBaseA.Rows[i]["accountCode2"].ToString().Trim();
			oleCmd.Parameters["P19"].Value = F_DT_MrsBaseA.Rows[i]["xNameE"].ToString().Trim();
			oleCmd.Parameters["P20"].Value = F_DT_MrsBaseA.Rows[i]["xNameC"].ToString().Trim();
			oleCmd.Parameters["P21"].Value = F_DT_MrsBaseA.Rows[i]["eUnit"].ToString().Trim();
			oleCmd.Parameters["P22"].Value = F_DT_MrsBaseA.Rows[i]["extendCode"].ToString().Trim();
			oleCmd.Parameters["P23"].Value = F_DT_MrsBaseA.Rows[i]["Post"].ToString().Trim();
			oleCmd.Parameters["P24"].Value = F_UserID;
			DBCLS.ExecuteOleDbCommand(oleCmd);
			sSQL = "Select pubCode From MrsBaseA Where PccesCode ='" + F_DT_MrsBaseA.Rows[i]["pccesCode"].ToString().Trim() + "' ";
			sPubCode = DBCLS.GetUserDefine_String(sSQL, "pubCode");
			F_DT_MrsBaseA.Rows[i]["mrsNewPubCode"] = Convert.ToInt32(sPubCode);
			F_DT_MrsBaseA.Rows[i]["mrsExist"] = "N";
		}
		DataTable DTM_A = new DataTable();
		DTM_A = F_DT_MrsBaseA.Clone();
		DataView DV0 = F_DT_MrsBaseA.DefaultView;
		if (F_ExistProcessType == "2")
		{
			DV0.RowFilter = "mrsType = 'Y'";
		}
		else
		{
			DV0.RowFilter = "mrsType = 'Y' And mrsExist <> 'Y' ";
		}
		DV0.Sort = "pccesCode Asc";
		for (int i = 0; i < DV0.Count; i++)
		{
			DataRow DR_A = DTM_A.NewRow();
			for (int j = 0; j < DV0.Table.Columns.Count; j++)
			{
				DR_A[DV0.Table.Columns[j].ColumnName] = DV0[i][DV0.Table.Columns[j].ColumnName];
			}
			DTM_A.Rows.Add(DR_A);
		}
		F_DT_MrsBaseA.CaseSensitive = true;
		for (int i = 0; i < DTM_A.Rows.Count; i++)
		{
			DataView DV1 = F_DT_MrsBaseA.DefaultView;
			DV1.RowFilter = "mrsParent = '" + DTM_A.Rows[i]["pccesCode"].ToString().Trim() + "' And pccesCode <> mrsParent";
			DV1.Sort = "listNo Asc";
			OleDbCommand oleCmd2 = new OleDbCommand();
			for (int j = 0; j < DV1.Count; j++)
			{
				oleCmd2.CommandText = "Insert Into MrsBaseB(parentCode,pubCode,listNo,qty,cost,amount,ItemNo) values(?,?,?,?,?,?,?)";
				oleCmd2.Parameters.Clear();
				oleCmd2.Parameters.Add("P1", OleDbType.Integer);
				oleCmd2.Parameters.Add("P2", OleDbType.Integer);
				oleCmd2.Parameters.Add("P3", OleDbType.SmallInt);
				oleCmd2.Parameters.Add("P4", OleDbType.Double);
				oleCmd2.Parameters.Add("P5", OleDbType.Double);
				oleCmd2.Parameters.Add("P6", OleDbType.Double);
				oleCmd2.Parameters.Add("P7", OleDbType.VarChar);
				oleCmd2.Parameters["P1"].Value = DTM_A.Rows[i]["mrsNewPubCode"];
				oleCmd2.Parameters["P2"].Value = DV1[j]["mrsNewPubCode"];
				oleCmd2.Parameters["P3"].Value = DV1[j]["listNo"];
				oleCmd2.Parameters["P4"].Value = DV1[j]["BQty"];
				oleCmd2.Parameters["P5"].Value = DV1[j]["BCost"];
				oleCmd2.Parameters["P6"].Value = DV1[j]["BAmount"];
				oleCmd2.Parameters["P7"].Value = DV1[j]["listNo"].ToString().Trim();
				try
				{
					DBCLS.ExecuteOleDbCommand(oleCmd2);
				}
				catch (Exception ex)
				{
					CommonMethods.LogFile("Pcces46", "M", "MrsBase.PickFromOther.ClsPickFromOtherDB.cs" + ex.Message);
					Console.Write(ex.Message);
				}
			}
		}
		for (int i = 0; i < F_DT_MrsBaseC.Rows.Count; i++)
		{
			F_DT_MrsBaseC.Rows[i]["New_ParentCode"] = GetNewPubCodeFromA(F_DT_MrsBaseC.Rows[i]["ParentCodePcces"].ToString().Trim());
			F_DT_MrsBaseC.Rows[i]["New_PubCode"] = GetNewPubCodeFromA(F_DT_MrsBaseC.Rows[i]["PubCodePcces"].ToString().Trim());
			F_DT_MrsBaseC.Rows[i]["New_itemCode"] = GetNewPubCodeFromA(F_DT_MrsBaseC.Rows[i]["itemCodePcces"].ToString().Trim());
			F_DT_MrsBaseC.Rows[i]["mrsExist"] = GetExistFromA(F_DT_MrsBaseC.Rows[i]["ParentCodePcces"].ToString().Trim());
		}
		OleDbCommand oleCmd3 = new OleDbCommand();
		for (int i = 0; i < F_DT_MrsBaseC.Rows.Count; i++)
		{
			if (F_ExistProcessType == "2" || !(F_DT_MrsBaseC.Rows[i]["mrsExist"].ToString().Trim() == "Y"))
			{
				oleCmd3.CommandText = "Insert Into MrsBaseC(parentCode,pubCode,itemCode,PubListNo,ItemListNo) values(?,?,?,?,?) ";
				oleCmd3.Parameters.Clear();
				oleCmd3.Parameters.Add("P1", OleDbType.Integer);
				oleCmd3.Parameters.Add("P2", OleDbType.Integer);
				oleCmd3.Parameters.Add("P3", OleDbType.Integer);
				oleCmd3.Parameters.Add("P4", OleDbType.Integer);
				oleCmd3.Parameters.Add("P5", OleDbType.Integer);
				oleCmd3.Parameters["P1"].Value = F_DT_MrsBaseC.Rows[i]["New_ParentCode"];
				oleCmd3.Parameters["P2"].Value = F_DT_MrsBaseC.Rows[i]["New_PubCode"];
				oleCmd3.Parameters["P3"].Value = F_DT_MrsBaseC.Rows[i]["New_itemCode"];
				oleCmd3.Parameters["P4"].Value = F_DT_MrsBaseC.Rows[i]["PubListNo"];
				oleCmd3.Parameters["P5"].Value = F_DT_MrsBaseC.Rows[i]["ItemListNo"];
				try
				{
					DBCLS.ExecuteOleDbCommand(oleCmd3);
				}
				catch (Exception ex)
				{
					CommonMethods.LogFile("Pcces46", "M", "MrsBase.PickFromOther.ClsPickFromOtherDB.cs" + ex.Message);
					Console.Write(ex.Message);
				}
			}
		}
	}

	private string GetNewPubCodeFromA(string sPccesCode)
	{
		F_DT_MrsBaseA.CaseSensitive = true;
		DataView DV3 = F_DT_MrsBaseA.DefaultView;
		DV3.RowFilter = "pccesCode ='" + sPccesCode + "' ";
		return DV3[0]["mrsNewPubCode"].ToString().Trim();
	}

	private string GetExistFromA(string sPccesCode)
	{
		F_DT_MrsBaseA.CaseSensitive = true;
		DataView DV4 = F_DT_MrsBaseA.DefaultView;
		DV4.RowFilter = "pccesCode ='" + sPccesCode + "' ";
		return DV4[0]["mrsExist"].ToString().Trim();
	}
}
