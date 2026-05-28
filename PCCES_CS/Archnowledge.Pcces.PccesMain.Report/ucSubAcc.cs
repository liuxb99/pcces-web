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
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace Archnowledge.Pcces.PccesMain.Report;

public class ucSubAcc : UserControl
{
	private UltraOptionSet OP1;

	private Panel Pnl_PntLevel;

	private NumericUpDown aileael_DDL;

	private UltraLabel ultraLabel9;

	private UltraLabel ultraLabel1;

	private UltraLabel lbl_Issue;

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

	private string ls_Queue;

	private int F_MainQty = 0;

	private int F_MainCst = 0;

	private int F_MainAmt = 0;

	private int F_AnaQty = 0;

	private int F_AnaCst = 0;

	private int F_AnaAmt = 0;

	private string F_RPT_Tail;

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

	public ucSubAcc()
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
		Infragistics.Win.ValueListItem valueListItem4 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem5 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem6 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		this.OP1 = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
		this.Pnl_PntLevel = new System.Windows.Forms.Panel();
		this.aileael_DDL = new System.Windows.Forms.NumericUpDown();
		this.ultraLabel9 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.lbl_Issue = new Infragistics.Win.Misc.UltraLabel();
		((System.ComponentModel.ISupportInitialize)this.OP1).BeginInit();
		this.Pnl_PntLevel.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.aileael_DDL).BeginInit();
		base.SuspendLayout();
		this.OP1.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
		this.OP1.CheckedIndex = 0;
		this.OP1.ItemAppearance = appearance1;
		this.OP1.ItemOrigin = new System.Drawing.Point(8, 0);
		valueListItem1.DataValue = "SubAcc01";
		valueListItem1.DisplayText = "&A.估驗款計價表";
		valueListItem2.DataValue = "SubAcc02";
		valueListItem2.DisplayText = "&B.估驗款計價明細(總表)";
		valueListItem3.DataValue = "SubAcc03";
		valueListItem3.DisplayText = "&C.估驗款計價明細(詳細表)";
		valueListItem4.DataValue = "SubAcc04";
		valueListItem4.DisplayText = "&D.扣款明細表";
		valueListItem5.DataValue = "SubAcc05";
		valueListItem5.DisplayText = "&E.各期估驗彙整表";
		valueListItem6.DataValue = "SubAcc07";
		valueListItem6.DisplayText = "&F.加款明細表";
		this.OP1.Items.Add(valueListItem1);
		this.OP1.Items.Add(valueListItem2);
		this.OP1.Items.Add(valueListItem3);
		this.OP1.Items.Add(valueListItem4);
		this.OP1.Items.Add(valueListItem5);
		this.OP1.Items.Add(valueListItem6);
		this.OP1.ItemSpacingHorizontal = 10;
		this.OP1.ItemSpacingVertical = 10;
		this.OP1.Location = new System.Drawing.Point(8, 0);
		this.OP1.Name = "OP1";
		this.OP1.Size = new System.Drawing.Size(232, 176);
		this.OP1.TabIndex = 1;
		this.OP1.Text = "&A.估驗款計價表";
		this.OP1.UseMnemonics = true;
		this.OP1.ValueChanged += new System.EventHandler(OP1_ValueChanged);
		this.Pnl_PntLevel.Controls.Add(this.aileael_DDL);
		this.Pnl_PntLevel.Controls.Add(this.ultraLabel9);
		this.Pnl_PntLevel.Location = new System.Drawing.Point(256, 36);
		this.Pnl_PntLevel.Name = "Pnl_PntLevel";
		this.Pnl_PntLevel.Size = new System.Drawing.Size(144, 36);
		this.Pnl_PntLevel.TabIndex = 23;
		this.Pnl_PntLevel.Visible = false;
		this.aileael_DDL.Location = new System.Drawing.Point(80, 5);
		this.aileael_DDL.Maximum = new decimal(new int[4] { 8, 0, 0, 0 });
		this.aileael_DDL.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.aileael_DDL.Name = "aileael_DDL";
		this.aileael_DDL.Size = new System.Drawing.Size(56, 25);
		this.aileael_DDL.TabIndex = 19;
		this.aileael_DDL.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		this.ultraLabel9.Location = new System.Drawing.Point(4, 7);
		this.ultraLabel9.Name = "ultraLabel9";
		this.ultraLabel9.Size = new System.Drawing.Size(76, 23);
		this.ultraLabel9.TabIndex = 17;
		this.ultraLabel9.Text = "列印層數:";
		this.ultraLabel1.Location = new System.Drawing.Point(256, 8);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(120, 23);
		this.ultraLabel1.TabIndex = 24;
		this.ultraLabel1.Text = "目前列印期別：";
		appearance2.ForeColor = System.Drawing.Color.Red;
		this.lbl_Issue.Appearance = appearance2;
		this.lbl_Issue.Location = new System.Drawing.Point(364, 7);
		this.lbl_Issue.Name = "lbl_Issue";
		this.lbl_Issue.Size = new System.Drawing.Size(48, 23);
		this.lbl_Issue.TabIndex = 25;
		this.lbl_Issue.Text = "【1】";
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.Controls.Add(this.lbl_Issue);
		base.Controls.Add(this.ultraLabel1);
		base.Controls.Add(this.Pnl_PntLevel);
		base.Controls.Add(this.OP1);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.Name = "ucSubAcc";
		base.Size = new System.Drawing.Size(472, 184);
		base.Load += new System.EventHandler(ucSubAcc_Load);
		((System.ComponentModel.ISupportInitialize)this.OP1).EndInit();
		this.Pnl_PntLevel.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.aileael_DDL).EndInit();
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
		DataSet DS = new DataSet();
		bool Print_Eng_Col = false;
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(PrnSubAcc) 列印報表");
		PubProject pubprjcom = new PubProject(tmp_AL1);
		DataTable myTable = pubprjcom.ListItem(" a.ProjectCode='" + F_ProjectCode.Trim() + "' ");
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
		if (OP1.CheckedIndex == 1)
		{
			Repclass RptCom = new Repclass(tmp_AL1);
			ldt_RptData = RptCom.SubAccRpt(ls_prjcode, ls_Queue);
			ldt_RptData.TableName = "AccInfo";
			DS.Tables.Add(ldt_RptData.Copy());
			ldt_RptData = RptCom.GetProjInfoRpt(ls_prjcode, ls_Queue);
			DS.Tables.Add(ldt_RptData.Copy());
			ldt_RptData = RptCom.SubAccMainRpt(ls_prjcode, ls_Queue, aileael_DDL.Value.ToString());
			Repclass.RepInfo MyRepInfo = RptCom.GetRepInfo(RepName);
			Filename = MyRepInfo.RptName;
			cName_Len = MyRepInfo.Cname_Length;
			eName_Len = MyRepInfo.Ename_Length;
			Memo_Len = MyRepInfo.Memo_Length;
			RptCom = null;
		}
		if (OP1.CheckedIndex == 2)
		{
			Repclass RptCom = new Repclass(tmp_AL1);
			ldt_RptData = RptCom.SubAccRpt(ls_prjcode, ls_Queue);
			ldt_RptData.TableName = "AccInfo";
			DS.Tables.Add(ldt_RptData.Copy());
			ldt_RptData = RptCom.GetProjInfoRpt(ls_prjcode, ls_Queue);
			DS.Tables.Add(ldt_RptData.Copy());
			RptCom.ps_memo = "0000";
			ldt_RptData = RptCom.SubAccDetialRpt(ls_prjcode, ls_Queue);
			Repclass.RepInfo MyRepInfo = RptCom.GetRepInfo(RepName);
			Filename = MyRepInfo.RptName;
			cName_Len = MyRepInfo.Cname_Length;
			eName_Len = MyRepInfo.Ename_Length;
			Memo_Len = MyRepInfo.Memo_Length;
			RptCom = null;
		}
		if (OP1.CheckedIndex == 3)
		{
			Repclass RptCom = new Repclass(tmp_AL1);
			RptCom.ps_Deduct = "0";
			ldt_RptData = RptCom.SubAccDeductRpt(ls_prjcode, ls_Queue);
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
		if (OP1.CheckedIndex == 0)
		{
			Repclass RptCom = new Repclass(tmp_AL1);
			ldt_RptData = RptCom.SubAccRpt(ls_prjcode, ls_Queue);
			ldt_RptData.TableName = "AccInfo";
			DS.Tables.Add(ldt_RptData.Copy());
			ldt_RptData = RptCom.GetProjInfoRpt(ls_prjcode, ls_Queue);
			DS.Tables.Add(ldt_RptData.Copy());
			ldt_RptData = RptCom.SubAccRpt(ls_prjcode, ls_Queue);
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
		if (OP1.CheckedIndex == 4)
		{
			Repclass RptCom = new Repclass(tmp_AL1);
			ldt_RptData = RptCom.SubAccListRpt(ls_prjcode);
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
		if (OP1.CheckedIndex == 5)
		{
			Repclass RptCom = new Repclass(tmp_AL1);
			RptCom.ps_Deduct = "1";
			ldt_RptData = RptCom.SubAccDeductRpt(ls_prjcode, ls_Queue);
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
				CommonMethods.LogFile("Pcces46", "M", "Report.ucSubAcc.cs" + ex.Message);
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
						CommonMethods.LogFile("Pcces46", "M", "Report.ucSubAcc.cs" + ex.Message);
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
							CommonMethods.LogFile("Pcces46", "M", "Report.ucSubAcc.cs" + ex.Message);
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
		dr2["表尾設定"] = F_RPT_Tail;
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

	private void OP1_ValueChanged(object sender, EventArgs e)
	{
		if (OP1.CheckedIndex == 1)
		{
			Pnl_PntLevel.Visible = true;
		}
		else
		{
			Pnl_PntLevel.Visible = false;
		}
		(base.ParentForm as FormInvoiceReport).Load_RptKind(PubTools.GetEnumFromStr(OP1.CheckedItem.DataValue.ToString()));
	}

	private void ucSubAcc_Load(object sender, EventArgs e)
	{
		SettingDecimal();
		ls_prjcode = F_ProjectCode;
		ls_Queue = F_Issue;
		(base.ParentForm as FormInvoiceReport).Load_RptKind(PubTools.GetEnumFromStr(OP1.CheckedItem.DataValue.ToString()));
		lbl_Issue.Text = "【" + F_Issue + "】";
	}

	public void ReloadReports()
	{
		(base.ParentForm as FormInvoiceReport).Load_RptKind(PubTools.GetEnumFromStr(OP1.CheckedItem.DataValue.ToString()));
		lbl_Issue.Text = "【" + F_Issue + "】";
	}
}
