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

public class ucSubChg : UserControl
{
	private Panel Pnl_PntLevel;

	private NumericUpDown aileael_DDL;

	private UltraLabel ultraLabel9;

	private UltraOptionSet OP1;

	private UltraCheckEditor ChkChg;

	private UltraLabel lbl_Issue;

	private UltraLabel ultraLabel1;

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

	private string F_RPT_Tail;

	private int F_MainQty = 0;

	private int F_MainCst = 0;

	private int F_MainAmt = 0;

	private int F_AnaQty = 0;

	private int F_AnaCst = 0;

	private int F_AnaAmt = 0;

	private DataTable myTable1 = new DataTable("PccesAccess");

	protected int li_len;

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

	public ucSubChg()
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
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		this.Pnl_PntLevel = new System.Windows.Forms.Panel();
		this.aileael_DDL = new System.Windows.Forms.NumericUpDown();
		this.ultraLabel9 = new Infragistics.Win.Misc.UltraLabel();
		this.OP1 = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
		this.ChkChg = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.lbl_Issue = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.Pnl_PntLevel.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.aileael_DDL).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.OP1).BeginInit();
		base.SuspendLayout();
		this.Pnl_PntLevel.Controls.Add(this.aileael_DDL);
		this.Pnl_PntLevel.Controls.Add(this.ultraLabel9);
		this.Pnl_PntLevel.Location = new System.Drawing.Point(264, 31);
		this.Pnl_PntLevel.Name = "Pnl_PntLevel";
		this.Pnl_PntLevel.Size = new System.Drawing.Size(144, 36);
		this.Pnl_PntLevel.TabIndex = 25;
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
		this.OP1.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
		this.OP1.CheckedIndex = 0;
		this.OP1.ItemAppearance = appearance1;
		this.OP1.ItemOrigin = new System.Drawing.Point(8, 0);
		valueListItem1.DataValue = "SubChg01";
		valueListItem1.DisplayText = "&A.契約變更紀錄";
		valueListItem2.DataValue = "SubChg02";
		valueListItem2.DisplayText = "&B.契約變更明細(總表)";
		valueListItem3.DataValue = "SubChg03";
		valueListItem3.DisplayText = "&C.契約變更明細(詳細表)";
		valueListItem4.DataValue = "SubChg04";
		valueListItem4.DisplayText = "&D.契約變更單價分析表";
		this.OP1.Items.Add(valueListItem1);
		this.OP1.Items.Add(valueListItem2);
		this.OP1.Items.Add(valueListItem3);
		this.OP1.Items.Add(valueListItem4);
		this.OP1.ItemSpacingHorizontal = 10;
		this.OP1.ItemSpacingVertical = 10;
		this.OP1.Location = new System.Drawing.Point(16, 3);
		this.OP1.Name = "OP1";
		this.OP1.Size = new System.Drawing.Size(232, 117);
		this.OP1.TabIndex = 24;
		this.OP1.Text = "&A.契約變更紀錄";
		this.OP1.UseMnemonics = true;
		this.OP1.ValueChanged += new System.EventHandler(OP1_ValueChanged);
		this.ChkChg.Location = new System.Drawing.Point(264, 72);
		this.ChkChg.Name = "ChkChg";
		this.ChkChg.Size = new System.Drawing.Size(200, 20);
		this.ChkChg.TabIndex = 26;
		this.ChkChg.Text = "只列印變更的項目";
		appearance2.ForeColor = System.Drawing.Color.Red;
		this.lbl_Issue.Appearance = appearance2;
		this.lbl_Issue.Location = new System.Drawing.Point(375, 6);
		this.lbl_Issue.Name = "lbl_Issue";
		this.lbl_Issue.Size = new System.Drawing.Size(48, 23);
		this.lbl_Issue.TabIndex = 28;
		this.lbl_Issue.Text = "【1】";
		this.ultraLabel1.Location = new System.Drawing.Point(263, 6);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(120, 23);
		this.ultraLabel1.TabIndex = 27;
		this.ultraLabel1.Text = "目前列印次別：";
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.Controls.Add(this.lbl_Issue);
		base.Controls.Add(this.ultraLabel1);
		base.Controls.Add(this.ChkChg);
		base.Controls.Add(this.Pnl_PntLevel);
		base.Controls.Add(this.OP1);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.Name = "ucSubChg";
		base.Size = new System.Drawing.Size(488, 128);
		base.Load += new System.EventHandler(ucSubChg_Load);
		this.Pnl_PntLevel.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.aileael_DDL).EndInit();
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
		bool Print_Eng_Col = false;
		DataSet DS = new DataSet();
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(PrnSubChg) 列印報表");
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
		if (OP1.CheckedIndex == 1)
		{
			Repclass RptCom = new Repclass(tmp_AL1);
			ldt_RptData = RptCom.GetProjInfoRpt(ls_prjcode, ls_Queue);
			DS.Tables.Add(ldt_RptData.Copy());
			ldt_RptData = RptCom.SubChgMainRpt(ls_prjcode, aileael_DDL.Value.ToString(), ls_Queue, !ChkChg.Checked);
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
			ldt_RptData = RptCom.SubChgDetialRpt(ls_prjcode, ls_Queue, !ChkChg.Checked);
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
			ldt_RptData = RptCom.SubChgRpt(ls_prjcode, ls_Queue);
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
		DataTable NewDT = new DataTable();
		Class1 cl1 = new Class1(F_UserID);
		if (OP1.CheckedIndex == 0 || OP1.CheckedIndex == 1 || OP1.CheckedIndex == 2)
		{
			NewDT = ldt_RptData.Copy();
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
					CommonMethods.LogFile("Pcces46", "M", "Report.ucSubChg.cs" + ex.Message);
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
							CommonMethods.LogFile("Pcces46", "M", "Report.ucSubChg.cs" + ex.Message);
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
								CommonMethods.LogFile("Pcces46", "M", "Report.ucSubChg.cs" + ex.Message);
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
		}
		else if (OP1.CheckedIndex == 3)
		{
			myTable1 = Analysis_Rep();
			DataSet DSKK = new DataSet();
			li_len = 40;
			NewDT = ExtraWork().Copy();
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
		DataInfo.Columns.Add("是否列印日期", Type.GetType("System.String"));
		DataInfo.Columns.Add("列印日期", Type.GetType("System.String"));
		DataInfo.Columns.Add("DBName", Type.GetType("System.String"));
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
		dr2["是否列印日期"] = "Y";
		dr2["列印日期"] = $"{DateTime.Now:yyyy/MM/dd}";
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

	private DataTable Analysis_Rep()
	{
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("-單價分析 ");
		Repclass repcom = new Repclass(tmp_AL1);
		repcom.ps_sckind = CommonMethods.GetActionNameString(PccesFormAction.SplitContract);
		repcom.ps_analysisMark = "*";
		string memoKind = "1110";
		repcom.ps_memo = memoKind;
		repcom.ps_filter = "0";
		repcom.ps_showcost = "1";
		repcom.ps_PrintByPccesCode = "N";
		repcom.ps_Issue = F_Issue;
		DataTable myTable = repcom.AnalysisRpt(F_ProjectCode);
		myTable.Columns.Add("LastItem", Type.GetType("System.String"));
		string ls_code = "";
		bool flag = false;
		DataTable tmpdt = myTable.Copy();
		DataView tmpdv = tmpdt.DefaultView;
		tmpdv.Sort = "PccesCode,seq";
		myTable.Clear();
		string tmpItemNo = "";
		for (int i = 0; i < tmpdv.Count; i++)
		{
			if (ls_code != tmpdv[i]["PccesCode"].ToString() || (tmpItemNo != tmpdv[i]["ItemNo"].ToString() && tmpdv[i]["IsDetail"].ToString().Trim() == "ISDETAIL"))
			{
				ls_code = tmpdv[i]["PccesCode"].ToString();
				tmpItemNo = tmpdv[i]["ItemNo"].ToString();
			}
			if (tmpItemNo == tmpdv[i]["ItemNo"].ToString())
			{
				DataRow dr = myTable.NewRow();
				for (int j = 0; j < myTable.Columns.Count; j++)
				{
					dr[j] = tmpdv[i][j];
				}
				myTable.Rows.Add(dr);
			}
		}
		for (int i = myTable.Rows.Count; i > 0; i--)
		{
			if (ls_code != myTable.Rows[i - 1]["pccesCode"].ToString().Trim())
			{
				myTable.Rows[i - 1]["LastItem"] = "Y";
			}
			else
			{
				myTable.Rows[i - 1]["LastItem"] = "N";
			}
			ls_code = myTable.Rows[i - 1]["pccesCode"].ToString().Trim();
		}
		flag = false;
		myTable.CaseSensitive = true;
		DataView dv1 = myTable.DefaultView;
		dv1.Sort = "PccesCode";
		foreach (DataRow dr in myTable.Rows)
		{
			string ls_BPccesCode = dr["pccescodeB"].ToString().Trim();
			if (dr["analysisb"].ToString().Trim() == "1")
			{
				dv1.RowFilter = "PccesCode = '" + ls_BPccesCode + "' ";
				if (dv1.Count > 0)
				{
					DataRow dataRow;
					(dataRow = dr)["memob"] = string.Concat(dataRow["memob"], dv1[0]["ItemNo"].ToString().Trim());
				}
			}
		}
		repcom = null;
		PubTools.WriteRoughlyLog(tmp_AL1);
		return myTable;
	}

	private DataTable ExtraWork()
	{
		myTable1.Columns.Add("cname_line", Type.GetType("System.Int16"));
		myTable1.Columns.Add("ename_line", Type.GetType("System.Int16"));
		myTable1.Columns.Add("line_len", Type.GetType("System.Int16"));
		string gTableName = Guid.NewGuid().ToString();
		Class1 cl1 = new Class1(F_UserID);
		DataTable NewDT = myTable1.Copy();
		NewDT.Clear();
		NewDT.Columns.Add("NewPage", Type.GetType("System.String"));
		NewDT.Columns.Add("ItemNum", Type.GetType("System.Int16"));
		NewDT.Columns.Add("ColNo", Type.GetType("System.Int16"));
		int RowNo = 0;
		bool flag = false;
		foreach (DataRow dr in myTable1.Rows)
		{
			int x = 1;
			string ColName = "";
			ColName = "cNameB";
			int Memo_len = 0;
			Memo_len = 14;
			string[] tmpm = new string[0];
			try
			{
				tmpm = cl1.GetCstring(dr["memoB"].ToString(), Memo_len);
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "Report.ucSubChg.cs" + ex.Message);
			}
			string[] tmpc = cl1.GetCstring(dr[ColName].ToString(), li_len);
			string[] tmpe = new string[0];
			if (dr["PccesCode"].ToString().Trim() == "02252Q1H01")
			{
				tmpe = new string[0];
			}
			int RowCount = 0;
			RowCount = ((tmpe.Length + tmpc.Length <= tmpm.Length) ? tmpm.Length : (tmpe.Length + tmpc.Length));
			for (int i = 0; i < tmpc.Length; i++)
			{
				DataRow ndr = NewDT.NewRow();
				for (int j = 0; j < myTable1.Columns.Count; j++)
				{
					ndr[j] = dr[j];
				}
				try
				{
					ndr["memoB"] = tmpm[x - 1];
				}
				catch
				{
					try
					{
						ndr["memoB"] = "";
					}
					catch (Exception ex)
					{
						CommonMethods.LogFile("Pcces46", "M", "Report.ucSubChg.cs" + ex.Message);
					}
				}
				ndr[ColName] = tmpc[i];
				if (false)
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
			for (; x - 1 < tmpm.Length; x++)
			{
				DataRow ndr = NewDT.NewRow();
				ndr["PccesCode"] = dr["PccesCode"];
				flag = false;
				ndr["PrintNo"] = dr["PrintNo"].ToString().Trim();
				ndr["ItemNo"] = dr["ItemNo"];
				ndr["ItemNum"] = x;
				ndr["NewPage"] = "N";
				ndr["ColNo"] = RowNo;
				ndr["memoB"] = tmpm[x - 1];
				RowNo++;
				NewDT.Rows.Add(ndr);
			}
		}
		flag = false;
		flag = false;
		if (NewDT.Rows.Count > 0)
		{
			string sItemNo = NewDT.Rows[0]["itemNo"].ToString().Trim();
			for (int i = 0; i < NewDT.Rows.Count; i++)
			{
				if (NewDT.Rows[i]["itemNo"].ToString().Trim() == "")
				{
					NewDT.Rows[i]["itemNo"] = sItemNo;
				}
				if (NewDT.Rows[i]["itemNo"].ToString().Trim() != "" && NewDT.Rows[i]["itemNo"].ToString().Trim() != sItemNo)
				{
					sItemNo = NewDT.Rows[i]["itemNo"].ToString().Trim();
				}
			}
		}
		DataTable DT_11 = NewDT.Clone();
		DataView DV111 = NewDT.DefaultView;
		for (int i = 0; i < DV111.Count; i++)
		{
			DataRow DR = DT_11.NewRow();
			for (int j = 0; j < DV111.Table.Columns.Count; j++)
			{
				DR[DV111.Table.Columns[j].ColumnName] = DV111[i][j];
			}
			DT_11.Rows.Add(DR);
		}
		NewDT.Clear();
		NewDT = DT_11.Copy();
		DataTable DT_Size = new DataTable("AnaSize");
		DT_Size.Columns.Add("PccesCode", Type.GetType("System.String"));
		DT_Size.Columns.Add("RowCount", Type.GetType("System.Int32"));
		DT_Size.Columns.Add("PageSize", Type.GetType("System.String"));
		DT_Size.Columns.Add("Seq", Type.GetType("System.Int32"));
		DT_Size.Columns.Add("ItemNo", Type.GetType("System.String"));
		string ssPccesCode = "";
		int iiCount = 0;
		int iiSeq = 0;
		for (int i = 0; i < NewDT.Rows.Count; i++)
		{
			flag = false;
			if ((NewDT.Rows[i]["pccesCode"].ToString().Trim() != ssPccesCode && ssPccesCode != "") || i == NewDT.Rows.Count - 1)
			{
				DataRow DR = DT_Size.NewRow();
				DR["PccesCode"] = ssPccesCode;
				DR["RowCount"] = ((i != NewDT.Rows.Count - 1) ? iiCount : (++iiCount));
				DR["PageSize"] = "";
				DR["Seq"] = ++iiSeq;
				DR["ItemNo"] = NewDT.Rows[i]["ItemNo"].ToString().Trim();
				DT_Size.Rows.Add(DR);
				iiCount = 0;
			}
			iiCount++;
			ssPccesCode = NewDT.Rows[i]["pccesCode"].ToString().Trim();
		}
		for (int i = 0; i < DT_Size.Rows.Count; i++)
		{
			try
			{
				if (DT_Size.Rows[i]["PageSize"].ToString() != "")
				{
					continue;
				}
				if (PubTools.Str2Int(DT_Size.Rows[i]["RowCount"]) <= 12)
				{
					if (PubTools.Str2Int(DT_Size.Rows[i + 1]["RowCount"]) <= 12)
					{
						DT_Size.Rows[i]["PageSize"] = "S";
						DT_Size.Rows[i + 1]["PageSize"] = "S";
					}
					else
					{
						DT_Size.Rows[i]["PageSize"] = "L";
						DT_Size.Rows[i + 1]["PageSize"] = "L";
					}
				}
				else
				{
					DT_Size.Rows[i]["PageSize"] = "L";
				}
			}
			catch
			{
				DT_Size.Rows[i]["PageSize"] = "L";
			}
		}
		flag = false;
		DT_Size.CaseSensitive = true;
		DataView DV_SIZE = DT_Size.DefaultView;
		DV_SIZE.Sort = "PccesCode";
		for (int i = 0; i < NewDT.Rows.Count; i++)
		{
			int iidex = DV_SIZE.Find(NewDT.Rows[i]["pccesCode"].ToString().Trim());
			NewDT.Rows[i]["papersize"] = DV_SIZE[iidex]["PageSize"];
		}
		DataTable DTAnaTemp = NewDT.Clone();
		flag = false;
		string sPccCod = NewDT.Rows[0]["PccesCode"].ToString().Trim();
		for (int i = 0; i < NewDT.Rows.Count; i++)
		{
			if (NewDT.Rows[i]["PccesCode"].ToString().Trim() == sPccCod)
			{
				DataRow DR2 = DTAnaTemp.NewRow();
				for (int j = 0; j < NewDT.Columns.Count; j++)
				{
					DR2[j] = NewDT.Rows[i][j];
				}
				DTAnaTemp.Rows.Add(DR2);
				if (i == NewDT.Rows.Count - 1 && NewDT.Rows[i]["papersize"].ToString() == "S")
				{
					DT_Size.CaseSensitive = true;
					DataView DV_SIZE2 = DT_Size.DefaultView;
					DV_SIZE2.Sort = "PccesCode";
					int iidex = DV_SIZE2.Find(NewDT.Rows[i - 1]["pccesCode"].ToString().Trim());
					int iiRows = PubTools.Str2Int(DV_SIZE2[iidex]["RowCount"]);
					int ReMains = 12 - iiRows;
					for (int k = 1; k <= ReMains; k++)
					{
						DataRow DR11 = DTAnaTemp.NewRow();
						for (int j = 0; j < NewDT.Columns.Count; j++)
						{
							if (NewDT.Columns[j].ColumnName == "cNameB" || NewDT.Columns[j].ColumnName == "unitNameB" || NewDT.Columns[j].ColumnName == "memoB")
							{
								DR11[j] = "";
							}
							else
							{
								DR11[j] = NewDT.Rows[i - 1][j];
							}
						}
						DTAnaTemp.Rows.Add(DR11);
					}
				}
			}
			else
			{
				if (NewDT.Rows[i - 1]["papersize"].ToString() == "S")
				{
					DT_Size.CaseSensitive = true;
					DataView DV_SIZE2 = DT_Size.DefaultView;
					DV_SIZE2.Sort = "PccesCode";
					int iidex = DV_SIZE2.Find(NewDT.Rows[i - 1]["pccesCode"].ToString().Trim());
					int iiRows = PubTools.Str2Int(DV_SIZE2[iidex]["RowCount"]);
					int ReMains = 12 - iiRows;
					for (int k = 1; k <= ReMains; k++)
					{
						DataRow DR2 = DTAnaTemp.NewRow();
						for (int j = 0; j < NewDT.Columns.Count; j++)
						{
							if (NewDT.Columns[j].ColumnName == "cNameB" || NewDT.Columns[j].ColumnName == "unitNameB" || NewDT.Columns[j].ColumnName == "memoB")
							{
								DR2[j] = "";
							}
							else
							{
								DR2[j] = NewDT.Rows[i - 1][j];
							}
						}
						DTAnaTemp.Rows.Add(DR2);
					}
				}
				if (sPccCod != "")
				{
					DataRow DR2 = DTAnaTemp.NewRow();
					for (int j = 0; j < NewDT.Columns.Count; j++)
					{
						DR2[j] = NewDT.Rows[i][j];
					}
					DTAnaTemp.Rows.Add(DR2);
				}
			}
			sPccCod = NewDT.Rows[i]["PccesCode"].ToString().Trim();
		}
		NewDT.Clear();
		NewDT = DTAnaTemp.Copy();
		flag = false;
		string ls_PrintNo = "";
		foreach (DataRow dr in NewDT.Rows)
		{
			if (ls_PrintNo != "" && ls_PrintNo != dr["PrintNo"].ToString().Trim() + dr["PccesCode"].ToString())
			{
				dr["NewPage"] = "Y";
			}
			else
			{
				dr["NewPage"] = "N";
			}
			ls_PrintNo = dr["PrintNo"].ToString().Trim() + dr["PccesCode"].ToString();
		}
		string ls_LastItem = "Y";
		for (int i = NewDT.Rows.Count - 1; i > -1; i--)
		{
			NewDT.Rows[i]["LastItem"] = ls_LastItem;
			ls_LastItem = NewDT.Rows[i]["NewPage"].ToString();
		}
		int PageLine = 28;
		int LineNo = 1;
		for (int l = 0; l < NewDT.Rows.Count; l++)
		{
			if (LineNo > PageLine)
			{
				LineNo = 1;
				int tmp = PubTools.Str2Int(NewDT.Rows[l]["ItemNum"].ToString());
				l = l - tmp + 1;
				NewDT.Rows[l]["NewPage"] = "Y";
			}
			NewDT.Rows[l]["ColNo"] = LineNo;
			LineNo++;
			if (l < NewDT.Rows.Count && NewDT.Rows[l]["LastItem"].ToString() == "Y")
			{
				LineNo = 1;
			}
		}
		return NewDT;
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
		if (OP1.CheckedIndex == 3)
		{
			ChkChg.Visible = false;
		}
		else
		{
			ChkChg.Visible = true;
		}
		(base.ParentForm as FormInvoiceReport).Load_RptKind(PubTools.GetEnumFromStr(OP1.CheckedItem.DataValue.ToString()));
	}

	private void ucSubChg_Load(object sender, EventArgs e)
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
