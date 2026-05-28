using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.REPClass;
using Archnowledge.Pcces.STDClass;
using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;

namespace Archnowledge.Pcces.PccesMain.Report;

public class ucSubFinal : UserControl
{
	private UltraOptionSet OP1;

	private Container components = null;

	private bool F_IsAccess;

	private string F_AccessFileName;

	private string F_ProjectCode;

	private string F_SubProjectCode;

	private string F_Issue;

	private string F_UserID;

	private string F_cmp_name;

	private string F_cmp_Ename;

	private string ls_prjcode;

	private string ls_Queue = "10000";

	private string F_RPT_Tail;

	private int F_MainQty = 0;

	private int F_MainCst = 0;

	private int F_MainAmt = 0;

	private int F_AnaQty = 0;

	private int F_AnaCst = 0;

	private int F_AnaAmt = 0;

	public bool _IsAccess
	{
		get
		{
			return F_IsAccess;
		}
		set
		{
			F_IsAccess = value;
		}
	}

	public string _AccessFileName
	{
		get
		{
			return F_AccessFileName;
		}
		set
		{
			F_AccessFileName = value;
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

	public string _SubProjectCode
	{
		get
		{
			return F_SubProjectCode;
		}
		set
		{
			F_SubProjectCode = value;
		}
	}

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

	public string _cmp_name
	{
		get
		{
			return F_cmp_name;
		}
		set
		{
			F_cmp_name = value;
		}
	}

	public string _cmp_Ename
	{
		get
		{
			return F_cmp_Ename;
		}
		set
		{
			F_cmp_Ename = value;
		}
	}

	public string _RPT_Tail
	{
		get
		{
			return F_RPT_Tail;
		}
		set
		{
			F_RPT_Tail = value;
		}
	}

	public ucSubFinal()
	{
		InitializeComponent();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
		this.OP1 = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
		((System.ComponentModel.ISupportInitialize)this.OP1).BeginInit();
		base.SuspendLayout();
		this.OP1.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
		this.OP1.CheckedIndex = 0;
		this.OP1.ItemAppearance = appearance1;
		this.OP1.ItemOrigin = new System.Drawing.Point(8, 0);
		valueListItem1.DataValue = "SubFinal01";
		valueListItem1.DisplayText = "&A.工程決算驗收證明書";
		valueListItem2.DataValue = "SubFinal02";
		valueListItem2.DisplayText = "&B.工程決算驗收總表";
		valueListItem3.DataValue = "SubFinal03";
		valueListItem3.DisplayText = "&C.工程決算驗收總明細表";
		this.OP1.Items.Add(valueListItem1);
		this.OP1.Items.Add(valueListItem2);
		this.OP1.Items.Add(valueListItem3);
		this.OP1.ItemSpacingHorizontal = 10;
		this.OP1.ItemSpacingVertical = 10;
		this.OP1.Location = new System.Drawing.Point(16, 3);
		this.OP1.Name = "OP1";
		this.OP1.Size = new System.Drawing.Size(232, 93);
		this.OP1.TabIndex = 25;
		this.OP1.Text = "&A.工程決算驗收證明書";
		this.OP1.UseMnemonics = true;
		this.OP1.ValueChanged += new System.EventHandler(OP1_ValueChanged);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.Controls.Add(this.OP1);
		this.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.Name = "ucSubFinal";
		base.Size = new System.Drawing.Size(488, 104);
		base.Load += new System.EventHandler(ucSubFinal_Load);
		((System.ComponentModel.ISupportInitialize)this.OP1).EndInit();
		base.ResumeLayout(false);
	}

	private void SettingDecimal()
	{
		string IPStr = CommonMethods.GetIPAddress();
		DataTable DTDecimal = new DataTable();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("設定小數位數取位原則" + F_ProjectCode + "(" + IPStr + ")");
		PubDecimal dbDecimal = new PubDecimal(aArr);
		DTDecimal = dbDecimal.ListItem("", F_ProjectCode);
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
	}

	public void GenerateData()
	{
		base.ParentForm.Cursor = Cursors.WaitCursor;
		if (!F_IsAccess)
		{
			(base.ParentForm as FormInvoiceReport).JumpToPage2();
		}
		ls_Queue = "10000";
		bool Print_Eng_Col = false;
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(PrnSubChg) 列印報表");
		DataSet DS = new DataSet();
		PubProject pubprjcom = new PubProject(tmp_AL1);
		DataTable myTable = pubprjcom.ListItem(" a.ProjectCode='" + ls_prjcode.Trim() + "' ");
		pubprjcom = null;
		string projcetNamec = myTable.Rows[0]["ProjCName"].ToString();
		string projectAddress = myTable.Rows[0]["ProjAddress"].ToString();
		string accountCode1 = "";
		string projcetNamee = myTable.Rows[0]["ProjEName"].ToString();
		DataTable ldt_RptData = new DataTable();
		string Filename = "";
		int cName_Len = 0;
		int eName_Len = 0;
		int Memo_Len = 0;
		string RepName = (base.ParentForm as FormInvoiceReport).GetReportName();
		if (OP1.CheckedIndex == 0)
		{
			Repclass RptCom = new Repclass(tmp_AL1);
			ldt_RptData = RptCom.SubFinalRpt(ls_prjcode);
			Repclass.RepInfo MyRepInfo = RptCom.GetRepInfo(RepName);
			Filename = MyRepInfo.RptName;
			cName_Len = MyRepInfo.Cname_Length;
			eName_Len = MyRepInfo.Ename_Length;
			Memo_Len = MyRepInfo.Memo_Length;
			RptCom = null;
			if (ldt_RptData.Rows.Count < 1)
			{
				MessageBox.Show(this, "無資料可供列印！", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
		}
		if (OP1.CheckedIndex == 1)
		{
			Repclass RptCom = new Repclass(tmp_AL1);
			ldt_RptData = RptCom.SubFinalMainRpt(ls_prjcode);
			Repclass.RepInfo MyRepInfo = RptCom.GetRepInfo(RepName);
			Filename = MyRepInfo.RptName;
			cName_Len = MyRepInfo.Cname_Length;
			eName_Len = MyRepInfo.Ename_Length;
			Memo_Len = MyRepInfo.Memo_Length;
			RptCom = null;
			if (ldt_RptData.Rows.Count < 1)
			{
				MessageBox.Show(this, "無資料可供列印！", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
		}
		if (OP1.CheckedIndex == 2)
		{
			Repclass RptCom = new Repclass(tmp_AL1);
			ldt_RptData = RptCom.GetProjInfoRpt(ls_prjcode, ls_Queue);
			DS.Tables.Add(ldt_RptData.Copy());
			ldt_RptData = RptCom.SubFinalDetialRpt(ls_prjcode, ls_Queue);
			Repclass.RepInfo MyRepInfo = RptCom.GetRepInfo(RepName);
			Filename = MyRepInfo.RptName;
			cName_Len = MyRepInfo.Cname_Length;
			eName_Len = MyRepInfo.Ename_Length;
			Memo_Len = MyRepInfo.Memo_Length;
			RptCom = null;
			if (ldt_RptData.Rows.Count < 1)
			{
				MessageBox.Show(this, "無資料可供列印！", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
		}
		Class1 cl1 = new Class1(F_UserID);
		DataTable NewDT = ldt_RptData.Copy();
		NewDT.Clear();
		NewDT.Columns.Add("NewPage", Type.GetType("System.String"));
		NewDT.Columns.Add("ItemNum", Type.GetType("System.Int16"));
		NewDT.Columns.Add("ColNo", Type.GetType("System.Int16"));
		int RowNo = 0;
		foreach (DataRow dr in ldt_RptData.Rows)
		{
			int x = 1;
			string ColName = "Cname";
			string[] tmpm = new string[0];
			try
			{
				tmpm = cl1.GetCstring(dr["memo"].ToString(), Memo_Len);
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "Report.ucSubFinal.cs" + ex.Message);
			}
			string[] tmpc = cl1.GetCstring(dr[ColName].ToString(), cName_Len);
			string[] tmpe = new string[0];
			if (Print_Eng_Col)
			{
				tmpe = cl1.GetEstring(dr["ename"].ToString(), eName_Len);
			}
			int RowCount = 0;
			RowCount = ((tmpe.Length + tmpc.Length <= tmpm.Length) ? tmpm.Length : (tmpe.Length + tmpc.Length));
			for (int i = 0; i < tmpc.Length; i++)
			{
				DataRow ndr = NewDT.NewRow();
				for (int j = 0; j < ldt_RptData.Columns.Count; j++)
				{
					ndr[j] = dr[j];
				}
				try
				{
					ndr["memo"] = tmpm[x - 1];
				}
				catch
				{
					try
					{
						ndr["memo"] = "";
					}
					catch (Exception ex)
					{
						CommonMethods.LogFile("Pcces46", "M", "Report.ucSubFinal.cs" + ex.Message);
					}
				}
				if (Print_Eng_Col)
				{
					switch (x)
					{
					default:
						ndr["ItemNo"] = "";
						break;
					case 2:
						ndr["ItemNo"] = "";
						ndr["UnitName"] = ndr["eUnit"];
						break;
					case 1:
						break;
					}
				}
				ndr[ColName] = tmpc[i];
				bool lb_NewPage = false;
				if (OP1.CheckedIndex == 2 && x == 1 && dr["PrintNo"].ToString().Trim().Length == 4)
				{
					lb_NewPage = true;
				}
				if (lb_NewPage)
				{
					ndr["NewPage"] = "Y";
					RowNo = 0;
				}
				else if (i == 0 && RowCount + RowNo > 40)
				{
					ndr["NewPage"] = "Y";
					RowNo = 0;
				}
				else
				{
					ndr["NewPage"] = "N";
				}
				ndr["ItemNum"] = x;
				ndr["ColNo"] = RowNo;
				RowNo++;
				NewDT.Rows.Add(ndr);
				x++;
			}
			if (Print_Eng_Col)
			{
				for (int i = 0; i < tmpe.Length; i++)
				{
					DataRow ndr = NewDT.NewRow();
					for (int j = 0; j < ldt_RptData.Columns.Count; j++)
					{
						ndr[j] = dr[j];
					}
					try
					{
						ndr["memo"] = tmpm[x - 1];
					}
					catch
					{
						try
						{
							ndr["memo"] = "";
						}
						catch (Exception ex)
						{
							CommonMethods.LogFile("Pcces46", "M", "Report.ucSubFinal.cs" + ex.Message);
						}
					}
					if (x != 2)
					{
						ndr["UnitName"] = "";
					}
					else
					{
						ndr["UnitName"] = ndr["eUnit"];
					}
					ndr["ItemNo"] = "";
					ndr[ColName] = tmpe[i];
					ndr["NewPage"] = "N";
					ndr["ColNo"] = RowNo;
					ndr["ItemNum"] = x;
					RowNo++;
					NewDT.Rows.Add(ndr);
					x++;
				}
			}
			for (; x - 1 < tmpm.Length; x++)
			{
				DataRow ndr = NewDT.NewRow();
				ndr["PccesCode"] = dr["PccesCode"];
				ndr["ItemNum"] = x;
				ndr["NewPage"] = "N";
				ndr["ColNo"] = RowNo;
				ndr["memo"] = tmpm[x - 1];
				RowNo++;
				NewDT.Rows.Add(ndr);
			}
			if (Print_Eng_Col && x % 2 == 0)
			{
				DataRow ndr = NewDT.NewRow();
				ndr["PccesCode"] = dr["PccesCode"];
				ndr["ItemNum"] = x;
				ndr["NewPage"] = "N";
				ndr["ColNo"] = RowNo;
				RowNo++;
				NewDT.Rows.Add(ndr);
			}
		}
		NewDT.TableName = "PccesAccess";
		DS.Tables.Add(NewDT);
		DataTable DataInfo = new DataTable();
		DataInfo.Columns.Add("公司名稱", Type.GetType("System.String"));
		DataInfo.Columns.Add("英文抬頭", Type.GetType("System.String"));
		DataInfo.Columns.Add("工程名稱", Type.GetType("System.String"));
		DataInfo.Columns.Add("英文名稱", Type.GetType("System.String"));
		DataInfo.Columns.Add("施工地點", Type.GetType("System.String"));
		DataInfo.Columns.Add("會計科目", Type.GetType("System.String"));
		DataInfo.Columns.Add("工程編號", Type.GetType("System.String"));
		DataInfo.Columns.Add("表尾設定", Type.GetType("System.String"));
		DataInfo.Columns.Add("ItemQty", Type.GetType("System.Int16"));
		DataInfo.Columns.Add("ItemCost", Type.GetType("System.Int16"));
		DataInfo.Columns.Add("ItemAmt", Type.GetType("System.Int16"));
		DataInfo.Columns.Add("AnalysisQty", Type.GetType("System.Int16"));
		DataInfo.Columns.Add("AnalysisCost", Type.GetType("System.Int16"));
		DataInfo.Columns.Add("AnalysisAmt", Type.GetType("System.Int16"));
		DataRow dr2 = DataInfo.NewRow();
		dr2["公司名稱"] = F_cmp_name.Trim();
		dr2["英文抬頭"] = F_cmp_Ename.Trim();
		dr2["工程名稱"] = projcetNamec;
		dr2["英文名稱"] = projcetNamee;
		dr2["施工地點"] = projectAddress;
		dr2["會計科目"] = accountCode1;
		dr2["工程編號"] = ls_prjcode;
		dr2["表尾設定"] = F_RPT_Tail.Trim();
		dr2["ItemQty"] = F_MainQty;
		dr2["ItemCost"] = F_MainCst;
		dr2["ItemAmt"] = F_MainAmt;
		dr2["AnalysisQty"] = F_AnaQty;
		dr2["AnalysisCost"] = F_AnaCst;
		dr2["AnalysisAmt"] = F_AnaAmt;
		DataInfo.Rows.Add(dr2);
		DataInfo.TableName = "DataInfo";
		DS.Tables.Add(DataInfo);
		string sFTPDir = CommonMethods.ExtractFilePath(Application.ExecutablePath) + "Report\\";
		string gTableName = "pccesAccess";
		if (F_IsAccess)
		{
			cl1.CreateReport(DS, "PccesAccess", F_AccessFileName, sFTPDir);
		}
		else
		{
			cl1.CreateReport(DS, "PccesAccess", sFTPDir + gTableName + ".MDB", sFTPDir);
		}
		base.ParentForm.Cursor = Cursors.Default;
	}

	private void ucSubFinal_Load(object sender, EventArgs e)
	{
		SettingDecimal();
		ls_prjcode = F_ProjectCode;
		ls_Queue = F_Issue;
		(base.ParentForm as FormInvoiceReport).Load_RptKind(PubTools.GetEnumFromStr(OP1.CheckedItem.DataValue.ToString()));
	}

	public void ReloadReports()
	{
		(base.ParentForm as FormInvoiceReport).Load_RptKind(PubTools.GetEnumFromStr(OP1.CheckedItem.DataValue.ToString()));
	}

	private void OP1_ValueChanged(object sender, EventArgs e)
	{
		(base.ParentForm as FormInvoiceReport).Load_RptKind(PubTools.GetEnumFromStr(OP1.CheckedItem.DataValue.ToString()));
	}
}
