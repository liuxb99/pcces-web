using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.DomainModule.Budget;
using Archnowledge.Pcces.DomainModule.Coms;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.STDClass;
using Infragistics.Win.UltraWinGrid;

namespace Archnowledge.Pcces.PccesMain.Library;

internal class ComsWebService
{
	private string ProjectCode = "";

	public ComsWebService(string ProjectCode)
	{
		this.ProjectCode = ProjectCode;
	}

	public ExecResult ExpandBudgetInCOMS(bool ForceEnable)
	{
		ExecResult ER = new ExecResult();
		if (SysConfig.SysComsEnable)
		{
			if (SysConfig.SysComsLoginID != "")
			{
				ProjectServiceHelper theProjectServiceHelper = new ProjectServiceHelper(ForceEnable);
				ER = theProjectServiceHelper.ExpandBudgetInCOMS(ProjectCode);
				if (ER.ReturnCode == 0)
				{
					ComsExpandBudget thePccesExpandBudget = new ComsExpandBudget(ProjectCode);
					thePccesExpandBudget.DoExecuteExpandBudget();
				}
			}
			else
			{
				ER.ReturnCode = 2;
				ER.Message = "未與營建管理系統結合，展開明細表失敗！";
			}
		}
		return ER;
	}

	public bool AllowChangeBysNo(int SNo, int CostsNo, bool silent)
	{
		return AllowChangeBysNo(SNo, CostsNo, silent, silent);
	}

	public bool AllowChangeBysNo(int SNo, int CostsNo, bool silentOnWarning, bool silentOnModify)
	{
		bool Allow = true;
		string IsEdit = SysConfig.SysEditAfterBudLem.ToUpper();
		if (IsEdit == "WARNONLY" || IsEdit == "DISABLE")
		{
			ExecResult ER = new ExecResult();
			if (SysConfig.SysComsEnable)
			{
				Archnowledge.Pcces.DomainModule.Coms.BudgetCtrl theBudgetCtrl = new Archnowledge.Pcces.DomainModule.Coms.BudgetCtrl();
				Allow = !theBudgetCtrl.IsItemInSubPlanCart(ProjectCode, SysConfig.SysComsDB, SNo);
			}
			if (ER.ReturnCode != 0)
			{
				if (!silentOnWarning || !silentOnModify)
				{
					MessageBox.Show("呼叫服務發生錯誤，訊息如下：\n" + ER.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				Allow = false;
			}
		}
		if (!Allow)
		{
			if (IsEdit == "WarnOnly")
			{
				if (!silentOnWarning)
				{
					MessageBox.Show("此項目已進入分包規劃，若修改則與分包規劃資料不符！", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				Allow = true;
			}
			else if (IsEdit == "Disable" && !silentOnModify)
			{
				MessageBox.Show("此項目已進入分包規劃不能修改！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		return Allow;
	}

	public bool AllowChangeByAccQtyAmtByPccesCode(string pccesCode, int sNo, string unitName, decimal qty, decimal cost, bool silentOnWarning, bool silentOnModify)
	{
		string Message = "";
		bool Allow = true;
		string IsCheckAccQtyAmt = SysConfig.SysIsCheckAccQtyAmt.ToUpper();
		if ((IsCheckAccQtyAmt == "WARNONLY" || IsCheckAccQtyAmt == "DISABLE") && SysConfig.SysComsEnable)
		{
			Archnowledge.Pcces.DomainModule.Coms.BudgetCtrl theBudgetCtrl = new Archnowledge.Pcces.DomainModule.Coms.BudgetCtrl();
			DataTable dtSubAcc = theBudgetCtrl.GetSubAccTotalByPccesCode(ProjectCode, SysConfig.SysComsDB, pccesCode);
			if (!dtSubAcc.Columns.Contains("ItemQty"))
			{
				if (!silentOnWarning || !silentOnModify)
				{
					MessageBox.Show("呼叫COMS預儲程序usp_coms_GetSubAccTotalByPccesCodeForPCCES時發生錯誤", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				return false;
			}
			if (dtSubAcc.Rows.Count == 0)
			{
				Allow = true;
			}
			else
			{
				BudProjMrsA budProjMrsA = new BudProjMrsA();
				budProjMrsA.UpdateBudProjMrsACalculateWorkItemUsrAmtUsrQty(ProjectCode, pccesCode);
				DataSet dsBudProjMrsA = budProjMrsA.GetProjMrsAByPccesCode(ProjectCode, pccesCode);
				BudItemA budItemA = new BudItemA();
				DataSet dsBudItemA = budItemA.GetItemABySNo(ProjectCode, sNo);
				decimal usrValue = 0m;
				decimal diff = 0m;
				decimal accValue = 0m;
				decimal PreQty = ArchConvert.Obj2Decimal(dsBudItemA.Tables[0].Rows[0]["Qty"]);
				decimal PreCost = ArchConvert.Obj2Decimal(dsBudItemA.Tables[0].Rows[0]["Cost"]);
				decimal AfterValue = 0m;
				if (dsBudProjMrsA.Tables[0].Rows.Count > 0 && dsBudItemA.Tables[0].Rows.Count > 0)
				{
					bool IsAnalysis = ArchConvert.Obj2Bool(dsBudProjMrsA.Tables[0].Rows[0]["Analysis"]);
					if (unitName == "式" && qty == 1m && !IsAnalysis)
					{
						accValue = ArchConvert.Obj2Decimal(dtSubAcc.Rows[0]["ItemAmt"]);
						usrValue = ArchConvert.Obj2Decimal(dsBudProjMrsA.Tables[0].Rows[0]["UsrAmt"]);
						decimal PreAmt = PreQty * PreCost;
						diff = qty * cost - PreAmt;
						AfterValue = usrValue + diff;
						Message = "變更後金額為:" + AfterValue + "，目前已計價金額為:" + accValue;
					}
					else
					{
						usrValue = ArchConvert.Obj2Decimal(dsBudProjMrsA.Tables[0].Rows[0]["UsrQty"]);
						diff = qty - PreQty;
						accValue = ArchConvert.Obj2Decimal(dtSubAcc.Rows[0]["ItemQty"]);
						AfterValue = usrValue + diff;
						Message = "變更後數量為:" + AfterValue + "，目前已計價數量為:" + accValue;
					}
					if (unitName == "式" && qty == 1m && !IsAnalysis)
					{
						if (usrValue > 0m)
						{
							Allow = AfterValue >= accValue;
						}
						if (usrValue < 0m)
						{
							Allow = AfterValue <= accValue;
						}
					}
					else
					{
						Allow = AfterValue >= accValue;
					}
				}
			}
		}
		if (!Allow)
		{
			if (IsCheckAccQtyAmt == "WARNONLY")
			{
				if (!silentOnWarning)
				{
					MessageBox.Show(Message + "\n 修改後" + (Message.Contains("金額") ? "金額" : "數量") + "和已計價" + (Message.Contains("金額") ? "金額" : "數量") + "衝突(修改後值低於正/負絕對值或造成正負轉換)！", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				Allow = true;
			}
			else if (IsCheckAccQtyAmt == "DISABLE" && !silentOnModify)
			{
				MessageBox.Show(Message + "\n 修改後" + (Message.Contains("金額") ? "金額" : "數量") + "和已計價" + (Message.Contains("金額") ? "金額" : "數量") + "衝突(修改後值低於正/負絕對值或造成正負轉換)，不能修改！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		return Allow;
	}

	private DataTable GetAnalysisTable(string F_UserID, string F_ProjectCode, string F_sno)
	{
		string ssSQL = "\r\ndeclare @DecItemQty int\r\ndeclare @DecItemCost int\r\ndeclare @DecItemAmt int\r\n \r\nSelect @DecItemQty=itemQty, @DecItemCost=itemCost, @DecItemAmt=itemAmt from PubDecimal where ProjectCode = '" + F_ProjectCode + "'\r\nif @DecItemQty is null\r\n\tSelect @DecItemQty = 3 \r\n\r\nif @DecItemCost is null\r\n\tSelect @DecItemCost = 0\r\n\r\nif @DecItemAmt is null\r\n\tSelect @DecItemAmt = 0\r\n\r\nSelect case(Upper(a.kind))   \r\n\tWhen 'W' Then b.pccesCode   \r\n\tElse a.pccesCode   \r\nEnd as pccescode,\r\ncase(Upper(a.kind))   \r\n\tWhen 'W' Then b.cName   \r\n\tElse a.cName   \r\nEnd as cName ,\r\ncase(Upper(a.kind))   \r\n\tWhen 'W' Then b.eName   \r\n\tElse a.eName   \r\nEnd as eName ,\r\ncase(Upper(a.kind))   \r\n\tWhen 'W' Then b.unitName   \r\n\tElse a.unitName   \r\nEnd as unitName ,\r\ncase(Upper(a.kind))   \r\n\tWhen 'W' Then b.eUnit   \r\n\tElse a.eUnit   \r\nEnd as eUnit ,\r\ncase(Upper(a.kind))   \r\n\tWhen 'W' Then b.rate   \r\n\tElse a.rate   \r\nEnd as rate ,\r\ncase   \r\n\tWhen Upper(a.kind)='W' and (b.CostKind IS NULL OR b.CostKind <> '$') Then b.cost   \r\n\tElse a.cost   \r\nEnd as cost ,\r\na.preCost,a.projectCode, a.sNo, RTrim(a.printNo) as PrintNo, a.pubCode, RTrim(a.itemNo) as ItemNo, a.levelNo, a.Flag, \r\na.kind, a.qty, a.amount, a.memo, a.setDecimal,a.CostUnit,a.Property1,a.Property2,a.Property3,a.CostUID,\r\na.TypeID, a.bidCode, a.share, a.dsctLock, a.Formula, a.SubProjectCode,a.ShareSno,a.ShareCost,a.LockCost, \r\na.ModLock,\r\nisnull(a.QtyDec,@DecItemQty) as QtyDec,\r\nisnull(a.CostDec,@DecItemCost) as CostDec,\r\nisnull(a.AmtDec,@DecItemAmt) as AmtDec,\r\na.PwrSet, a.PrintToAnalysis,a.printNo as ReportPrintNo, \r\na.fixPrice, a.flag, a.Lock, a.IsGreenItem, a.IsGreenMethod, a.IsGreenMaterial, a.IsGreenEnergy, a.BudgetChangeReason, b.IsCommonItem,\r\na.VersionHistory, cast(a.Sno as varchar(128)) as Lem_UID ,\r\n\tb.PccesCode as BudItemCode,b.PccesCode as BudPccesCode,a.qty as BudItemQty,a.cost as BudItemCost,a.unitName as BudItemUnit, B.analysis,B.analysis as BudAnalysis, B.analysisQty, 0 as BudListNo \r\nfrom budItemA A left join budProjMrsA B  \r\non A.pubCode = B.pubCode and A.ProjectCode=B.ProjectCode  Where A.ProjectCode='" + F_ProjectCode + "' And A.Kind<>'Z' and A.Sno=" + F_sno + "order by printNo ";
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add("建立炸開的table");
		ModifyDB ModDB = new ModifyDB(F_ProjectCode, aArr);
		DataTable dtPreBudLem = ModDB.DBList(ssSQL);
		dtPreBudLem.Columns.Add("BudItemName", Type.GetType("System.String"));
		dtPreBudLem.Columns.Add("BudLevel", Type.GetType("System.String"));
		dtPreBudLem.Columns.Add("BudResName", Type.GetType("System.String"));
		dtPreBudLem.Columns.Add("BudItemType", Type.GetType("System.String"));
		dtPreBudLem.Columns.Add("sNoB", Type.GetType("System.Int32"));
		dtPreBudLem.Columns.Add("PowerRate", Type.GetType("System.Decimal"));
		dtPreBudLem.Columns.Add("BudItemNo", Type.GetType("System.String"));
		dtPreBudLem.Columns.Add("ItemParentName", Type.GetType("System.String"));
		dtPreBudLem.Columns.Add("FullItemNo", Type.GetType("System.String"));
		PubDecimal pubDecimal = new PubDecimal();
		DataSet dsPubDecimal = pubDecimal.GetPubDecimal(F_ProjectCode);
		int itemQtyPrecision = 4;
		int itemCostPrecision = 4;
		int analysisQtyPrecision = 4;
		int analysisCostPrecision = 4;
		if (dsPubDecimal.Tables[0].Rows.Count > 0)
		{
			itemQtyPrecision = ArchConvert.Obj2Int(dsPubDecimal.Tables[0].Rows[0]["itemQty"]);
			itemCostPrecision = ArchConvert.Obj2Int(dsPubDecimal.Tables[0].Rows[0]["itemCost"]);
			analysisQtyPrecision = ArchConvert.Obj2Int(dsPubDecimal.Tables[0].Rows[0]["analysisQty"]);
			analysisCostPrecision = ArchConvert.Obj2Int(dsPubDecimal.Tables[0].Rows[0]["analysisCost"]);
		}
		return dtPreBudLem;
	}

	private void Expand(string F_UserID, string F_ProjectCode, DataRow ItemDR, DataRow rowParent, ref DataTable dtPreBudLem, int iRowIndex, string itemNoHeader, decimal deltaQty, decimal deltaAmt)
	{
		int j = 0;
		ArrayList aArr = new ArrayList();
		aArr.Add(F_UserID);
		aArr.Add("建立炸開的table");
		string ParentPubCode = ArchConvert.Obj2String(rowParent["pubCode"]);
		string ssSQL = "Select '' as ItemNo, '' as PrintNo, A.CName, A.unitName, A.analysis,A.analysisQty, A.pubCode, A.costKind, A.memo, A.PwrSet, B.qty, B.Cost, B.Amount,B.listno,B.sNo   From budProjMrsA A Left Join budProjMrsB B on A.ProjectCode=B.ProjectCode and A.PubCode=B.PubCode  Where B.ProjectCode='" + F_ProjectCode + "' and B.ParentCode=" + ParentPubCode + " order by B.listno ";
		ModifyDB ModDB = new ModifyDB(F_ProjectCode, aArr);
		DataTable dtMrsB = new DataTable();
		dtMrsB = ModDB.DBList(ssSQL);
		dtMrsB.Columns.Add("BudItemName", Type.GetType("System.String"));
		dtMrsB.Columns.Add("BudItemCode", Type.GetType("System.String"));
		dtMrsB.Columns.Add("BudItemUnit", Type.GetType("System.String"));
		dtMrsB.Columns.Add("BudItemType", Type.GetType("System.String"));
		dtMrsB.Columns.Add("BudItemCost", Type.GetType("System.Decimal"));
		dtMrsB.Columns.Add("BudItemQty", Type.GetType("System.Decimal"));
		dtMrsB.Columns.Add("levelNo", Type.GetType("System.String"));
		dtMrsB.Columns.Add("Lem_UID", Type.GetType("System.String"));
		dtMrsB.Columns.Add("BudAnalysis", Type.GetType("System.String"));
		dtMrsB.Columns.Add("PowerRate", Type.GetType("System.Decimal"));
		dtMrsB.Columns.Add("ItemParentName", Type.GetType("System.String"));
		dtMrsB.Columns.Add("FullItemNo", Type.GetType("System.String"));
		dtMrsB.Columns.Add("AnalysisQty", Type.GetType("System.Decimal"));
		string sAnalysisQty = ModDB.DBGetValue("Select analysisQty From budProjMrsA Where ProjectCode='" + F_ProjectCode + "' and pubCode=" + ParentPubCode + "");
		double ParentAnalysisQty = ArchConvert.Obj2Double(sAnalysisQty);
		for (int i = 0; i < dtMrsB.Rows.Count; i++)
		{
			string SQL = "select * from budProjMrsA where PubCode=" + dtMrsB.Rows[i]["pubCode"].ToString() + " and ProjectCode='" + F_ProjectCode + "' ";
			DataTable DT_Temp = ModDB.DBList(SQL);
			DataRow rowPreBudLem = dtPreBudLem.NewRow();
			if (dtMrsB.Rows[i]["analysis"].ToString() == "1")
			{
				rowPreBudLem["PrintNo"] = rowParent["PrintNo"].ToString().Trim() + (i + 1).ToString().PadLeft(4, '0');
				rowPreBudLem["ItemNo"] = rowParent["ItemNo"].ToString().Trim();
				if (rowParent.Table.Columns.IndexOf("pccesCode") > -1)
				{
					rowPreBudLem["pccesCode"] = rowParent["pccesCode"].ToString().Trim();
					rowPreBudLem["BudItemCode"] = rowParent["pccesCode"].ToString().Trim();
				}
				else
				{
					rowPreBudLem["BudItemCode"] = rowParent["BudItemCode"].ToString().Trim();
				}
				rowPreBudLem["BudItemCost"] = rowParent["BudItemCost"].ToString().Trim();
				double Qty = ArchConvert.Obj2Double(dtMrsB.Rows[i]["qty"]);
				double ParentQty = ArchConvert.Obj2Double(rowParent["qty"]);
				rowPreBudLem["BudItemQty"] = Qty * ParentQty / ParentAnalysisQty;
				rowPreBudLem["PowerRate"] = Qty / ParentAnalysisQty;
				rowPreBudLem["BudItemName"] = rowParent["cName"].ToString().Trim();
				rowPreBudLem["CName"] = rowParent["CName"].ToString().Trim();
				rowPreBudLem["unitName"] = dtMrsB.Rows[i]["unitName"];
				rowPreBudLem["BudItemUnit"] = rowParent["BudItemUnit"].ToString().Trim();
				rowPreBudLem["qty"] = dtMrsB.Rows[i]["qty"];
				rowPreBudLem["analysis"] = dtMrsB.Rows[i]["analysis"];
				rowPreBudLem["BudAnalysis"] = dtMrsB.Rows[i]["analysis"];
				rowPreBudLem["Amount"] = dtMrsB.Rows[i]["Amount"];
				rowPreBudLem["memo"] = dtMrsB.Rows[i]["memo"];
				rowPreBudLem["BudListNo"] = dtMrsB.Rows[i]["listno"];
				rowPreBudLem["sNoB"] = dtMrsB.Rows[i]["sNo"];
				rowPreBudLem["BudLevel"] = itemNoHeader + "-" + j;
				rowPreBudLem["Lem_UID"] = rowParent["Lem_UID"].ToString().Trim() + "." + dtMrsB.Rows[i]["sNo"].ToString();
				rowPreBudLem["levelNo"] = ArchConvert.Obj2Int(rowParent["levelNo"]) + 1;
				rowPreBudLem["ItemParentName"] = ItemDR["CName"];
				rowPreBudLem["FullItemNo"] = rowParent["FullItemNo"].ToString() + "." + dtMrsB.Rows[i]["listNo"].ToString().Trim();
				rowPreBudLem["PwrSet"] = dtMrsB.Rows[i]["PwrSet"];
				rowPreBudLem["AnalysisQty"] = dtMrsB.Rows[i]["AnalysisQty"];
				string CostKind = "";
				double UsrAmt = 0.0;
				double UsrQty = 0.0;
				if (DT_Temp.Rows.Count > 0)
				{
					rowPreBudLem["BudPccesCode"] = DT_Temp.Rows[0]["PccesCode"];
					rowPreBudLem["BudResName"] = DT_Temp.Rows[0]["cName"];
					CostKind = ArchConvert.Obj2String(DT_Temp.Rows[0]["CostKind"]).Trim();
					UsrQty = ArchConvert.Obj2Double(DT_Temp.Rows[0]["UsrQty"]);
					UsrAmt = ArchConvert.Obj2Double(DT_Temp.Rows[0]["UsrAmt"]);
				}
				else
				{
					rowPreBudLem["BudPccesCode"] = "";
					rowPreBudLem["BudResName"] = "";
				}
				if (CostKind != "" && UsrQty != 0.0)
				{
					rowPreBudLem["cost"] = Math.Round(UsrAmt / UsrQty, 4, MidpointRounding.AwayFromZero);
				}
				else
				{
					rowPreBudLem["cost"] = dtMrsB.Rows[i]["cost"];
				}
				dtPreBudLem.Rows.Add(rowPreBudLem);
				dtMrsB.Rows[i]["Lem_UID"] = rowParent["Lem_UID"].ToString().Trim() + "." + dtMrsB.Rows[i]["sNo"].ToString();
				dtMrsB.Rows[i]["ItemNo"] = rowPreBudLem["BudLevel"].ToString().Trim();
				dtMrsB.Rows[i]["PrintNo"] = rowPreBudLem["PrintNo"].ToString().Trim();
				dtMrsB.Rows[i]["BudItemUnit"] = rowPreBudLem["unitName"].ToString().Trim();
				dtMrsB.Rows[i]["BudItemType"] = rowPreBudLem["kind"].ToString().Trim();
				dtMrsB.Rows[i]["BudItemCost"] = rowPreBudLem["cost"].ToString().Trim();
				dtMrsB.Rows[i]["BudItemQty"] = Qty * ParentQty / ParentAnalysisQty;
				dtMrsB.Rows[i]["qty"] = dtMrsB.Rows[i]["BudItemQty"];
				dtMrsB.Rows[i]["PowerRate"] = Qty / ParentAnalysisQty;
				dtMrsB.Rows[i]["levelNo"] = Convert.ToInt32(rowPreBudLem["levelNo"].ToString().Trim()) + 1;
				dtMrsB.Rows[i]["BudItemCode"] = DT_Temp.Rows[0]["pccesCode"].ToString().Trim();
				dtMrsB.Rows[i]["FullItemNo"] = rowPreBudLem["FullItemNo"];
				dtMrsB.Rows[i]["PwrSet"] = rowPreBudLem["PwrSet"];
				dtMrsB.Rows[i]["AnalysisQty"] = rowPreBudLem["AnalysisQty"];
				decimal theQty = (decimal)((double)deltaQty * Qty / ParentAnalysisQty);
				decimal theAmt = deltaAmt;
				Expand(F_UserID, F_ProjectCode, ItemDR, dtMrsB.Rows[i], ref dtPreBudLem, iRowIndex + i, rowPreBudLem["BudLevel"].ToString().Trim(), theQty, theAmt);
			}
			else
			{
				rowPreBudLem["PrintNo"] = rowParent["PrintNo"].ToString().Trim() + (i + 1).ToString().PadLeft(4, '0');
				rowPreBudLem["ItemNo"] = rowParent["ItemNo"].ToString().Trim();
				rowPreBudLem["BudAnalysis"] = dtMrsB.Rows[i]["analysis"];
				rowPreBudLem["analysis"] = dtMrsB.Rows[i]["analysis"];
				if (rowParent.Table.Columns.IndexOf("pccesCode") > -1)
				{
					rowPreBudLem["BudItemCode"] = rowParent["pccesCode"].ToString().Trim();
				}
				else
				{
					rowPreBudLem["BudItemCode"] = rowParent["BudItemCode"].ToString().Trim();
				}
				rowPreBudLem["BudItemName"] = rowParent["cName"].ToString().Trim();
				rowPreBudLem["BudItemUnit"] = rowParent["BudItemUnit"].ToString().Trim();
				rowPreBudLem["BudItemType"] = rowParent["BudItemType"].ToString().Trim();
				rowPreBudLem["BudItemCost"] = rowParent["BudItemCost"].ToString().Trim();
				double Qty = ArchConvert.Obj2Double(dtMrsB.Rows[i]["qty"]);
				double ParentQty = ArchConvert.Obj2Double(rowParent["qty"]);
				rowPreBudLem["BudItemQty"] = Qty * ParentQty / ParentAnalysisQty;
				rowPreBudLem["PowerRate"] = Qty / ParentAnalysisQty;
				rowPreBudLem["levelNo"] = rowParent["levelNo"].ToString().Trim();
				rowPreBudLem["Lem_UID"] = rowParent["Lem_UID"].ToString().Trim() + "." + dtMrsB.Rows[i]["sNo"].ToString();
				rowPreBudLem["BudListNo"] = dtMrsB.Rows[i]["listno"];
				rowPreBudLem["CName"] = rowParent["CName"];
				rowPreBudLem["unitName"] = dtMrsB.Rows[i]["unitName"];
				rowPreBudLem["qty"] = dtMrsB.Rows[i]["BudItemQty"];
				rowPreBudLem["Amount"] = dtMrsB.Rows[i]["Amount"];
				rowPreBudLem["memo"] = dtMrsB.Rows[i]["memo"];
				rowPreBudLem["ItemParentName"] = ItemDR["CName"];
				rowPreBudLem["BudLevel"] = itemNoHeader + "-" + j;
				string CostKind = "";
				double UsrAmt = 0.0;
				double UsrQty = 0.0;
				if (DT_Temp.Rows.Count > 0)
				{
					rowPreBudLem["BudPccesCode"] = DT_Temp.Rows[0]["PccesCode"];
					rowPreBudLem["BudResName"] = DT_Temp.Rows[0]["cName"];
					CostKind = ArchConvert.Obj2String(DT_Temp.Rows[0]["CostKind"]).Trim();
					UsrQty = ArchConvert.Obj2Double(DT_Temp.Rows[0]["UsrQty"]);
					UsrAmt = ArchConvert.Obj2Double(DT_Temp.Rows[0]["UsrAmt"]);
				}
				else
				{
					rowPreBudLem["BudPccesCode"] = "";
					rowPreBudLem["BudResName"] = "";
				}
				if (CostKind != "" && UsrQty != 0.0)
				{
					rowPreBudLem["cost"] = Math.Round(UsrAmt / UsrQty, 4, MidpointRounding.AwayFromZero);
				}
				else
				{
					rowPreBudLem["cost"] = dtMrsB.Rows[i]["cost"];
				}
				rowPreBudLem["usrQty"] = ModDB.DBGetValue("Select usrQty From budProjMrsA Where ProjectCode='" + F_ProjectCode + "' and pccesCode='" + rowPreBudLem["BudPccesCode"].ToString() + "'");
				rowPreBudLem["usrAmt"] = ModDB.DBGetValue("Select usrAmt From budProjMrsA Where ProjectCode='" + F_ProjectCode + "' and pccesCode='" + rowPreBudLem["BudPccesCode"].ToString() + "'");
				decimal theQty = (decimal)((double)deltaQty * Qty / ParentAnalysisQty);
				decimal theAmt = deltaAmt;
				rowPreBudLem["difQty"] = theQty;
				rowPreBudLem["sNoB"] = dtMrsB.Rows[i]["sNo"];
				rowPreBudLem["FullItemNo"] = rowParent["FullItemNo"].ToString() + "." + dtMrsB.Rows[i]["listNo"].ToString().Trim();
				rowPreBudLem["PwrSet"] = dtMrsB.Rows[i]["PwrSet"];
				rowPreBudLem["AnalysisQty"] = 0;
				dtPreBudLem.Rows.Add(rowPreBudLem);
			}
		}
	}

	public bool AllowChangeByAccQtyAmtByPccesCode(string pccesCode, int sNo, string unitName, decimal qty, decimal cost, bool silentOnWarning, bool silentOnModify, string userID, bool IsAnalysisItem, decimal beforeQty, decimal beforeCost)
	{
		string Message = "";
		bool Allow = true;
		string IsCheckAccQtyAmt = SysConfig.SysIsCheckAccQtyAmt.ToUpper();
		decimal DeltaQty = qty - beforeQty;
		decimal DeltaCost = cost - beforeCost;
		if ((IsCheckAccQtyAmt == "WARNONLY" || IsCheckAccQtyAmt == "DISABLE") && SysConfig.SysComsEnable)
		{
			DataTable dtSubAcc = GetSubAccTotalByPccesCode(pccesCode).Tables[0];
			if (!IsAnalysisItem)
			{
				BudProjMrsA budProjMrsA = new BudProjMrsA();
				DataTable DT_MrsA = budProjMrsA.GetProjMrsAByPccesCode(ProjectCode, pccesCode).Tables[0];
				if (DT_MrsA.Rows[0]["unitName"].ToString() == "式" && qty == 1m)
				{
					if ((decimal)DT_MrsA.Rows[0]["usrAmt"] - DeltaCost < (decimal)dtSubAcc.Rows[0]["itemAmt"])
					{
						Allow = false;
					}
				}
				else if ((decimal)DT_MrsA.Rows[0]["usrQty"] - DeltaQty < (decimal)dtSubAcc.Rows[0]["itemQty"])
				{
					Allow = false;
				}
			}
			else
			{
				DataTable dtPreBudLem = GetAnalysisTable(userID, ProjectCode, sNo.ToString());
				dtPreBudLem.Columns.Add("usrQty", Type.GetType("System.Decimal"));
				dtPreBudLem.Columns.Add("usrAmt", Type.GetType("System.Decimal"));
				dtPreBudLem.Columns.Add("difQty", Type.GetType("System.Decimal"));
				DataRow theRow = dtPreBudLem.Rows[0];
				Expand(userID, ProjectCode, theRow, theRow, ref dtPreBudLem, 0, theRow["ItemNo"].ToString().Trim(), DeltaQty, DeltaCost);
				string ssAA = dtPreBudLem.Rows.ToString();
			}
		}
		if (!Allow)
		{
			if (IsCheckAccQtyAmt == "WARNONLY")
			{
				if (!silentOnWarning)
				{
					MessageBox.Show(Message + "\n 修改後" + (Message.Contains("金額") ? "金額" : "數量") + "和已計價" + (Message.Contains("金額") ? "金額" : "數量") + "衝突(修改後值低於正/負絕對值或造成正負轉換)！", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				Allow = true;
			}
			else if (IsCheckAccQtyAmt == "DISABLE" && !silentOnModify)
			{
				MessageBox.Show(Message + "\n 修改後" + (Message.Contains("金額") ? "金額" : "數量") + "和已計價" + (Message.Contains("金額") ? "金額" : "數量") + "衝突(修改後值低於正/負絕對值或造成正負轉換)，不能修改！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		return Allow;
	}

	public bool AllowChangeByAccQtyAmtByPccesCode_fordel(string pccesCode, string unitName, decimal diffqty, decimal diffcost, bool silentOnWarning, bool silentOnModify)
	{
		string Message = "";
		bool Allow = true;
		string IsCheckAccQtyAmt = SysConfig.SysIsCheckAccQtyAmt.ToUpper();
		if ((IsCheckAccQtyAmt == "WARNONLY" || IsCheckAccQtyAmt == "DISABLE") && SysConfig.SysComsEnable)
		{
			Archnowledge.Pcces.DomainModule.Coms.BudgetCtrl theBudgetCtrl = new Archnowledge.Pcces.DomainModule.Coms.BudgetCtrl();
			DataTable dtSubAcc = theBudgetCtrl.GetSubAccTotalByPccesCode(ProjectCode, SysConfig.SysComsDB, pccesCode);
			if (dtSubAcc.Columns.Contains("ItemQty"))
			{
				if (dtSubAcc.Rows.Count == 0)
				{
					Allow = true;
				}
				else
				{
					BudProjMrsA budProjMrsA = new BudProjMrsA();
					budProjMrsA.UpdateBudProjMrsACalculateWorkItemUsrAmtUsrQty(ProjectCode, pccesCode);
					DataSet dsBudProjMrsA = budProjMrsA.GetProjMrsAByPccesCode(ProjectCode, pccesCode);
					decimal AfterValue = 0m;
					decimal usrValue = 0m;
					decimal accValue = 0m;
					if (dsBudProjMrsA.Tables[0].Rows.Count > 0)
					{
						bool IsAnalysis = ArchConvert.Obj2Bool(dsBudProjMrsA.Tables[0].Rows[0]["Analysis"]);
						if (unitName == "式" && diffqty == 0m && !IsAnalysis)
						{
							accValue = ArchConvert.Obj2Decimal(dtSubAcc.Rows[0]["ItemAmt"]);
							usrValue = ArchConvert.Obj2Decimal(dsBudProjMrsA.Tables[0].Rows[0]["UsrAmt"]);
							AfterValue = usrValue + diffcost;
							Message = "變更後金額為:" + AfterValue + "，目前已計價金額為:" + accValue;
						}
						else
						{
							usrValue = ArchConvert.Obj2Decimal(dsBudProjMrsA.Tables[0].Rows[0]["UsrQty"]);
							accValue = ArchConvert.Obj2Decimal(dtSubAcc.Rows[0]["ItemQty"]);
							AfterValue = usrValue + diffqty;
							Message = "變更後數量為:" + AfterValue + "，目前已計價數量為:" + accValue;
						}
						if (unitName == "式" && diffqty == 0m && !IsAnalysis)
						{
							if (usrValue > 0m)
							{
								Allow = AfterValue >= accValue;
							}
							if (usrValue < 0m)
							{
								Allow = AfterValue <= accValue;
							}
						}
						else
						{
							Allow = AfterValue >= accValue;
						}
					}
				}
			}
			else
			{
				if (!silentOnWarning || !silentOnModify)
				{
					MessageBox.Show("呼叫COMS預儲程序usp_coms_GetSubAccTotalByPccesCodeForPCCES時發生錯誤", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				Allow = false;
			}
		}
		if (!Allow)
		{
			if (IsCheckAccQtyAmt == "WARNONLY")
			{
				if (!silentOnWarning)
				{
					MessageBox.Show(Message + "\n 修改後" + (Message.Contains("金額") ? "金額" : "數量") + "將低於已計價" + (Message.Contains("金額") ? "金額" : "數量") + "！", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				Allow = true;
			}
			else if (IsCheckAccQtyAmt == "DISABLE" && !silentOnModify)
			{
				MessageBox.Show(Message + "\n 修改後" + (Message.Contains("金額") ? "金額" : "數量") + "將低於已計價" + (Message.Contains("金額") ? "金額" : "數量") + "，不能修改！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		return Allow;
	}

	public void SetUpCboSubItemQtyAmt(UltraCombo cboSubItemQtyAmt, string PccesCode)
	{
		if (SysConfig.SysComsEnable)
		{
			ExecResult ER = new ExecResult();
			SubServiceHelper theSubServiceHelper = new SubServiceHelper();
			DataSet dsSubItemQtyAmt = theSubServiceHelper.GetSubItemQtyAmt(ProjectCode, PccesCode, out ER);
			if (ER.ReturnCode != 0)
			{
				MessageBox.Show("呼叫服務發生錯誤，訊息如下：\n" + ER.Message, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (dsSubItemQtyAmt.Tables.Count > 0)
			{
				cboSubItemQtyAmt.Text = "請下拉，參考預算/估驗資訊";
				cboSubItemQtyAmt.DataSource = dsSubItemQtyAmt.Tables[0];
				cboSubItemQtyAmt.DataBind();
				cboSubItemQtyAmt.DisplayLayout.Bands[0].Columns[1].Header.Caption = "";
				cboSubItemQtyAmt.DisplayLayout.Bands[0].Columns[2].Header.Caption = "數量";
				cboSubItemQtyAmt.DisplayLayout.Bands[0].Columns[3].Header.Caption = "單價";
				cboSubItemQtyAmt.DisplayLayout.Bands[0].Columns[4].Header.Caption = "複價";
				cboSubItemQtyAmt.DisplayLayout.Bands[0].Columns[0].Hidden = true;
				cboSubItemQtyAmt.DisplayLayout.Bands[0].Columns[5].Hidden = true;
				cboSubItemQtyAmt.DisplayLayout.Bands[0].Columns[6].Hidden = true;
				cboSubItemQtyAmt.DisplayLayout.Bands[0].Columns[7].Hidden = true;
				cboSubItemQtyAmt.DisplayLayout.Bands[0].Columns[2].Format = "N2";
				cboSubItemQtyAmt.DisplayLayout.Bands[0].Columns[3].Format = "N2";
				cboSubItemQtyAmt.DisplayLayout.Bands[0].Columns[4].Format = "N2";
				cboSubItemQtyAmt.Visible = true;
			}
		}
	}

	public void GetSubAccTotalByPccesCode(string PccesCode, out decimal itemQty, out decimal itemAmt)
	{
		itemQty = 0m;
		itemAmt = 0m;
		if (SysConfig.SysComsEnable)
		{
			ExecResult ER = new ExecResult();
			SubServiceHelper theSubServiceHelper = new SubServiceHelper();
			DataSet dsSubItemQtyAmt = theSubServiceHelper.GetSubAccTotalByPccesCode(ProjectCode, PccesCode, out ER);
			if (ER.ReturnCode != 0)
			{
				MessageBox.Show("呼叫服務發生錯誤，訊息如下：\n" + ER.Message, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (dsSubItemQtyAmt.Tables.Count > 0)
			{
				itemQty = Convert.ToDecimal(dsSubItemQtyAmt.Tables[0].Rows[0]["ItemQty"]);
				itemAmt = Convert.ToDecimal(dsSubItemQtyAmt.Tables[0].Rows[0]["ItemAmt"]);
			}
		}
	}

	public DataSet GetSubAccTotalByPccesCode(string PccesCode)
	{
		DataSet DS_RetV = new DataSet();
		if (SysConfig.SysComsEnable)
		{
			ExecResult ER = new ExecResult();
			SubServiceHelper theSubServiceHelper = new SubServiceHelper();
			DataSet dsSubItemQtyAmt = theSubServiceHelper.GetSubAccTotalByPccesCode(ProjectCode, PccesCode, out ER);
			if (ER.ReturnCode != 0)
			{
				MessageBox.Show("呼叫服務發生錯誤，訊息如下：\n" + ER.Message, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (dsSubItemQtyAmt.Tables.Count > 0)
			{
				DS_RetV = dsSubItemQtyAmt;
			}
		}
		return DS_RetV;
	}
}
