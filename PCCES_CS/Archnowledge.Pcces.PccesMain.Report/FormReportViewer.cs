using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.PccesMain.Budget;
using Archnowledge.Pcces.REPClass;
using Archnowledge.Pcces.STDClass;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinSchedule;
using Infragistics.Win.UltraWinSchedule.CalendarCombo;
using Infragistics.Win.UltraWinTabControl;

namespace Archnowledge.Pcces.PccesMain.Report;

public class FormReportViewer : Form
{
	private const string CallFormHelp = "FormReportViewer";

	private DataTable NewDT = new DataTable();

	private string F_UserID;

	private string AppLocation = "";

	private int F_MainQty = 0;

	private int F_MainCst = 0;

	private int F_MainAmt = 0;

	private int F_AnaQty = 0;

	private int F_AnaCst = 0;

	private int F_AnaAmt = 0;

	private DataSet DS = new DataSet();

	private int iActivePage = 0;

	protected int li_len;

	protected string memoKind;

	protected ArrayList tmp_AL1;

	private string F_ReportPath = CommonMethods.ExtractFilePath(Application.ExecutablePath) + "Report\\";

	private PccesFormAction F_ActionName;

	private string F_ProjectCode;

	private string F_ProjectNameC;

	private string F_ProjectNameE;

	private string F_ProjectAddress;

	private string F_ProjectAccount1;

	private string F_ProjectAccount2;

	private string F_CompanyNameC;

	private string F_CompanyNameE;

	private string F_BidBud;

	private DataTable myTable = new DataTable("PccesAccess");

	private UltraTabControl ultraTabControl1;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private Panel panel1;

	private UltraButton A_Btn_Cncl;

	private UltraButton A_Btn_Next;

	private GroupBox groupBox2;

	private GroupBox groupBox3;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	private UltraLabel ultraLabel3;

	private UltraLabel ultraLabel4;

	private UltraLabel ultraLabel5;

	private UltraLabel ultraLabel6;

	private UltraLabel ultraLabel9;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage2;

	private UltraTabPageControl Tab_RPT1;

	private UltraTabPageControl Tab_0;

	private UltraTabPageControl Tab_1;

	private UltraTabPageControl Tab_3;

	private UltraTabPageControl Tab_4;

	private UltraTabControl Tab_RPT;

	private Panel panel7;

	private GroupBox groupBox4;

	private UltraButton ultraButton1;

	private UltraButton ultraButton2;

	private UltraLabel ultraLabel10;

	private UltraTabPageControl Tab_RPT3;

	private UltraTabPageControl Tab_RPT2;

	private Panel Pnl_00;

	private GroupBox groupBox5;

	private GroupBox groupBox6;

	private UltraOptionSet opRepeat;

	private UltraOptionSet opSort;

	private UltraTabControl ultraTabControl2;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage3;

	private UltraLabel ultraLabel7;

	private UltraLabel ultraLabel8;

	private UltraLabel ultraLabel11;

	private UltraLabel ultraLabel12;

	private UltraLabel ultraLabel13;

	private UltraLabel ultraLabel14;

	private UltraLabel ultraLabel15;

	private UltraLabel ultraLabel16;

	private UltraLabel ultraLabel17;

	private UltraLabel ultraLabel18;

	private UltraTabPageControl Tab_2;

	private UltraOptionSet opRPT_Type;

	private UltraOptionSet opTemplate;

	private UltraCheckEditor CB_remark;

	private UltraCheckEditor CB_pccescode;

	private UltraCheckEditor CB_price;

	private UltraCheckEditor CB_pubcode;

	private UltraTextEditor tb_Ana;

	private UltraTextEditor tb_Dei;

	private UltraTextEditor tb_Sum;

	private UltraTextEditor tb_Desc;

	private UltraTextEditor tb_Pic;

	private UltraComboEditor SetEnd;

	private UltraTextEditor cmp_Ename;

	private UltraTextEditor cmp_name;

	private UltraCheckEditor Price;

	private UltraTextEditor pricemark;

	private NumericUpDown aileael_DDL;

	private Panel Pnl_01;

	private Panel Pnl_02;

	private Panel Pnl_03;

	private Panel Pnl_04;

	private Panel Pnl_Sort;

	private Panel Pnl_Memo;

	private Panel Pnl_PntLevel;

	private UltraTabPageControl Tab_Memo_1;

	private UltraTabPageControl Tab_Memo_2;

	private UltraCalendarCombo txtPrintDate;

	private UltraCheckEditor chkPrintDate;

	private UltraCheckEditor chkAnaHalf;

	private Container components = null;

	private ucCrystalViewer UC_CRP0;

	private ucCrystalViewer UC_CRP1;

	private ucCrystalViewer UC_CRP2;

	private ucCrystalViewer UC_CRP3;

	private ucCrystalViewer UC_CRP4;

	private UltraCheckEditor CB_ExtraParam;

	private UltraButton BtnPageBreak;

	private UltraCheckEditor CB_IsIncWorkItem;

	private UltraCheckEditor CB_Ana_RepeatDetail;

	private UltraCheckEditor CB_Ana_SkipCommentItem;

	private UltraCheckEditor CB_Ana_SkipSubTotalItem;

	private UltraOptionSet opRPT_Type1;

	private UltraOptionSet opRPT_Way;

	private UltraCheckEditor CB_mainprice;

	private UltraOptionSet opRPT_Type2;

	private UltraOptionSet opRPT_Typebid;

	private Label lblAtt;

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

	public string _ProjectNameC
	{
		get
		{
			return F_ProjectNameC;
		}
		set
		{
			F_ProjectNameC = value;
		}
	}

	public string _ProjectNameE
	{
		get
		{
			return F_ProjectNameE;
		}
		set
		{
			F_ProjectNameE = value;
		}
	}

	public string _ProjectAddress
	{
		get
		{
			return F_ProjectAddress;
		}
		set
		{
			F_ProjectAddress = value;
		}
	}

	public string _ProjectAccount1
	{
		get
		{
			return F_ProjectAccount1;
		}
		set
		{
			F_ProjectAccount1 = value;
		}
	}

	public string _ProjectAccount2
	{
		get
		{
			return F_ProjectAccount2;
		}
		set
		{
			F_ProjectAccount2 = value;
		}
	}

	public string _CompanyNameC
	{
		get
		{
			return F_CompanyNameC;
		}
		set
		{
			F_CompanyNameC = value;
		}
	}

	public string _CompanyNameE
	{
		get
		{
			return F_CompanyNameE;
		}
		set
		{
			F_CompanyNameE = value;
		}
	}

	public FormReportViewer()
	{
		InitializeComponent();
	}

	private void FormReportViewer_Load(object sender, EventArgs e)
	{
		F_BidBud = CommonMethods.GetActionNameString(F_ActionName);
		InitialControls();
		SettingDecimal();
		ReadIniSettings();
		CorrectRatio();
	}

	private void CorrectRatio()
	{
		double ratio = CommonMethods.GetWindowRatio(base.Handle);
		if (ratio != 1.0)
		{
			opRPT_Type.Font = new Font(opRPT_Type.Font.Name, (float)((double)opRPT_Type.Font.Size * ratio));
			opTemplate.Font = new Font(opTemplate.Font.Name, (float)((double)opTemplate.Font.Size * ratio));
			chkPrintDate.Font = new Font(chkPrintDate.Font.Name, (float)((double)chkPrintDate.Font.Size * ratio));
			Price.Font = new Font(Price.Font.Name, (float)((double)Price.Font.Size * ratio));
			txtPrintDate.Font = new Font(txtPrintDate.Font.Name, (float)((double)txtPrintDate.Font.Size * ratio));
			Pnl_Memo.Font = new Font(Pnl_Memo.Font.Name, (float)((double)Pnl_Memo.Font.Size * ratio));
			Pnl_Sort.Font = new Font(Pnl_Sort.Font.Name, (float)((double)Pnl_Sort.Font.Size * ratio));
			groupBox2.Font = new Font(groupBox2.Font.Name, (float)((double)groupBox2.Font.Size * ratio));
			ultraTabControl2.Font = new Font(ultraTabControl2.Font.Name, (float)((double)ultraTabControl2.Font.Size * ratio));
		}
	}

	private void SettingDecimal()
	{
		DataTable DTDecimal = new DataTable();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add(CommonMethods.GetFormTypeTitle(FormType.Budget));
		Archnowledge.Pcces.BUDClass.PubDecimal dbDecimal = new Archnowledge.Pcces.BUDClass.PubDecimal(aArr);
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

	private void ReadIniSettings()
	{
	}

	private void InitialControls()
	{
		cmp_name.Text = F_CompanyNameC;
		cmp_Ename.Text = F_CompanyNameE;
		SetEnd.Text = "";
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("報表預覽，讀取表尾簽核欄資料");
		UserDefind UserCom = new UserDefind(aArr);
		DataTable DT_Foot = UserCom.ListItem("RptFooter");
		for (int i = 0; i < DT_Foot.Rows.Count; i++)
		{
			SetEnd.Items.Add(i, DT_Foot.Rows[i]["cString"].ToString());
		}
		if (DT_Foot.Rows.Count > 0)
		{
			SetEnd.SelectedIndex = 0;
		}
		DT_Foot = UserCom.ListItem("DefaultFooter");
		if (DT_Foot.Rows.Count > 0)
		{
			SetEnd.Text = DT_Foot.Rows[0]["cString"].ToString();
		}
		txtPrintDate.Value = DateTime.Now;
		string __DEBUG = CommonMethods.GetIniValue("DEBUG", "DEBUG");
		if (__DEBUG == "TRUE")
		{
			CB_ExtraParam.Visible = true;
		}
	}

	private void SaveIniSettings()
	{
		string iniFileName = CommonMethods.ExtractFilePath(Application.ExecutablePath) + "ExcelExp.ini";
		switch (opRPT_Type.CheckedIndex)
		{
		case 1:
			CommonMethods.IniWriteValue(iniFileName, "SUMMARY", "LEVEL", aileael_DDL.Value.ToString());
			break;
		case 2:
			CommonMethods.IniWriteValue(iniFileName, "DETAIL", "IsDetMemo", CB_remark.Checked ? "Y" : "N");
			CommonMethods.IniWriteValue(iniFileName, "DETAIL", "IsDetPccesCode", CB_pccescode.Checked ? "Y" : "N");
			CommonMethods.IniWriteValue(iniFileName, "DETAIL", "IsDetAnaMark", CB_price.Checked ? "Y" : "N");
			CommonMethods.IniWriteValue(iniFileName, "DETAIL", "DetAnaMark", pricemark.Text.Trim());
			CommonMethods.IniWriteValue(iniFileName, "DETAIL", "IsDetExtCode", CB_pubcode.Checked ? "Y" : "N");
			CommonMethods.IniWriteValue(iniFileName, "DETAIL", "IsDetPrice", CB_mainprice.Checked ? "Y" : "N");
			break;
		case 3:
			CommonMethods.IniWriteValue(iniFileName, "BREAKDOWN", "IsAnaMemo", CB_remark.Checked ? "Y" : "N");
			CommonMethods.IniWriteValue(iniFileName, "BREAKDOWN", "IsAnaPccesCode", CB_pccescode.Checked ? "Y" : "N");
			CommonMethods.IniWriteValue(iniFileName, "BREAKDOWN", "IsAnaAnaMark", CB_price.Checked ? "Y" : "N");
			CommonMethods.IniWriteValue(iniFileName, "BREAKDOWN", "AnaMark", pricemark.Text.Trim());
			CommonMethods.IniWriteValue(iniFileName, "BREAKDOWN", "IsAnaExtCode", CB_pubcode.Checked ? "Y" : "N");
			CommonMethods.IniWriteValue(iniFileName, "BREAKDOWN", "IsAnaHalfPage", chkAnaHalf.Checked ? "Y" : "N");
			CommonMethods.IniWriteValue(iniFileName, "BREAKDOWN", "IsRepeatDetailAnalysis", CB_Ana_RepeatDetail.Checked ? "Y" : "N");
			CommonMethods.IniWriteValue(iniFileName, "BREAKDOWN", "SortOrder", (opSort.CheckedIndex == 0) ? "0" : "1");
			CommonMethods.IniWriteValue(iniFileName, "BREAKDOWN", "Repeat", (opRepeat.CheckedIndex == 0) ? "1" : "0");
			break;
		}
	}

	private void A_Btn_Next_Click(object sender, EventArgs e)
	{
		SaveIniSettings();
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(PrnSubCtr) 列印報表");
		UserDefind UserCom = new UserDefind(tmp_AL1);
		UserCom.SetDefaultFooter(SetEnd.Text);
		Cursor = Cursors.WaitCursor;
		DS.Tables.Clear();
		Tab_RPT2.Tab.Selected = true;
		base.MaximizeBox = true;
		base.MinimizeBox = false;
		Tab_RPT.Style = UltraTabControlStyle.Wizard;
		Application.DoEvents();
		Thread.Sleep(1000);
		ExecResult ER = ProcessReportData();
		if (ER.ReturnCode == 0)
		{
			base.WindowState = FormWindowState.Maximized;
			Tab_RPT3.Tab.Selected = true;
			Application.DoEvents();
			ShowReport();
		}
		else
		{
			MessageBox.Show(ER.Message, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		Cursor = Cursors.Default;
	}

	private void ultraButton2_Click(object sender, EventArgs e)
	{
		try
		{
			switch (iActivePage)
			{
			case 0:
				Pnl_00.Controls.Remove(UC_CRP0);
				UC_CRP0.Dispose();
				UC_CRP0 = null;
				break;
			case 1:
				Pnl_01.Controls.Remove(UC_CRP1);
				UC_CRP1.Dispose();
				UC_CRP1 = null;
				break;
			case 2:
				Pnl_02.Controls.Remove(UC_CRP2);
				UC_CRP2.Dispose();
				UC_CRP2 = null;
				break;
			case 3:
				Pnl_03.Controls.Remove(UC_CRP3);
				UC_CRP3.Dispose();
				UC_CRP3 = null;
				break;
			case 4:
				Pnl_04.Controls.Remove(UC_CRP4);
				UC_CRP4.Dispose();
				UC_CRP4 = null;
				break;
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Report.FormReportViewer.cs" + ex.Message);
		}
		Tab_RPT1.Tab.Selected = true;
		base.WindowState = FormWindowState.Normal;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
	}

	private void cboReportType_ValueChanged(object sender, EventArgs e)
	{
	}

	private void ShowReport()
	{
		if (opRPT_Way.CheckedIndex == 0)
		{
			if (opRPT_Type.CheckedIndex == 0)
			{
				Render_RPT_0();
			}
			if (opRPT_Type.CheckedIndex == 1)
			{
				Render_RPT_1();
			}
			if (opRPT_Type.CheckedIndex == 2)
			{
				Render_RPT_2();
			}
			if (opRPT_Type.CheckedIndex == 3)
			{
				Render_RPT_3();
			}
			if (opRPT_Type.CheckedIndex == 4)
			{
				Render_RPT_4();
			}
			if (opRPT_Type2.CheckedIndex == 0)
			{
				Render_RPT_4();
			}
			if (opRPT_Type1.CheckedIndex == 0)
			{
				Render_RPT_3();
			}
		}
		else
		{
			if (opRPT_Type.CheckedIndex == 1)
			{
				Render_RPT_1();
			}
			if (opRPT_Type.CheckedIndex == 2)
			{
				Render_RPT_2();
			}
			if (opRPT_Type.CheckedIndex == 3)
			{
				Render_RPT_3();
			}
		}
	}

	private string GetPostfix()
	{
		string RetV = "01";
		if (opRPT_Type2.CheckedIndex == 0)
		{
			RetV = "02";
		}
		if (opTemplate.CheckedIndex == 1)
		{
			RetV = ((opRPT_Type2.CheckedIndex != 0) ? "06" : "07");
		}
		return RetV;
	}

	private string GetReportName()
	{
		string RetV = "";
		string sPrefix = CommonMethods.GetActionNameString(F_ActionName).ToLower();
		if (!Price.Checked)
		{
			sPrefix = "BID";
		}
		if (opRPT_Way.CheckedIndex == 0)
		{
			if (opRPT_Type.CheckedIndex == 0)
			{
				RetV = sPrefix + "mas";
			}
			if (opRPT_Type.CheckedIndex == 1)
			{
				RetV = sPrefix + "sum";
			}
			if (opRPT_Type.CheckedIndex == 2)
			{
				RetV = sPrefix + "det";
			}
			if (opRPT_Type.CheckedIndex == 3)
			{
				RetV = sPrefix + "pri";
			}
			if (opRPT_Type.CheckedIndex == 4)
			{
				RetV = sPrefix + "res";
			}
			if (opRPT_Type1.CheckedIndex == 0)
			{
				RetV = sPrefix + "pri01h2";
			}
			if (opRPT_Type2.CheckedIndex == 0)
			{
				RetV = sPrefix + "res";
			}
			if (opRPT_Type1.CheckedIndex != 0)
			{
				if (opRPT_Type.CheckedIndex == 3 && GetPostfix() == "01" && chkAnaHalf.Checked)
				{
					return RetV + GetPostfix() + "h.rpt";
				}
				return RetV + GetPostfix() + ".rpt";
			}
			return RetV + ".rpt";
		}
		if (opRPT_Type.CheckedIndex == 1)
		{
			RetV = sPrefix + "sumHeng";
		}
		if (opRPT_Type.CheckedIndex == 2)
		{
			RetV = sPrefix + "detHeng";
		}
		if (opRPT_Type.CheckedIndex == 3)
		{
			RetV = sPrefix + "priHeng";
		}
		return RetV + GetPostfix() + ".rpt";
	}

	private string GetDBFName()
	{
		string RetV = "";
		return "pccesAccess.mdb";
	}

	private string GetParams()
	{
		string RetV = "";
		return "\"公司名稱=" + cmp_name.Text + "\",\"英文抬頭=" + cmp_Ename.Text + "\",\"工程名稱=" + F_ProjectNameC + "\",\"英文名稱=" + F_ProjectNameE + "\",\"施工地點=" + F_ProjectAddress + "\",\"會計科目=" + F_ProjectAccount1 + "\",\"工程編號=" + F_ProjectCode + "\",\"表尾設定=" + SetEnd.Text + "\"";
	}

	private bool SetParams()
	{
		DataTable DataInfo = new DataTable();
		DataInfo.Columns.Add("公司名稱", Type.GetType("System.String"));
		DataInfo.Columns.Add("英文抬頭", Type.GetType("System.String"));
		DataInfo.Columns.Add("工程名稱", Type.GetType("System.String"));
		DataInfo.Columns.Add("英文名稱", Type.GetType("System.String"));
		DataInfo.Columns.Add("施工地點", Type.GetType("System.String"));
		DataInfo.Columns.Add("會計科目", Type.GetType("System.String"));
		DataInfo.Columns.Add("會計科目2", Type.GetType("System.String"));
		DataInfo.Columns.Add("工程編號", Type.GetType("System.String"));
		DataInfo.Columns.Add("表尾設定", Type.GetType("System.String"));
		DataInfo.Columns.Add("ItemQty", Type.GetType("System.String"));
		DataInfo.Columns.Add("ItemCost", Type.GetType("System.String"));
		DataInfo.Columns.Add("ItemAmt", Type.GetType("System.String"));
		DataInfo.Columns.Add("AnalysisQty", Type.GetType("System.String"));
		DataInfo.Columns.Add("AnalysisCost", Type.GetType("System.String"));
		DataInfo.Columns.Add("AnalysisAmt", Type.GetType("System.String"));
		DataInfo.Columns.Add("是否列印日期", Type.GetType("System.String"));
		DataInfo.Columns.Add("列印日期", Type.GetType("System.String"));
		DataInfo.Columns.Add("DBName", Type.GetType("System.String"));
		if ((opTemplate.CheckedIndex == 1 && opRPT_Type.CheckedIndex == 3) || opRPT_Type1.CheckedIndex == 0)
		{
			DataInfo.Columns.Add("說明項編號", Type.GetType("System.String"));
			DataInfo.Columns.Add("小計項編號", Type.GetType("System.String"));
		}
		DataRow dr1 = DataInfo.NewRow();
		dr1["公司名稱"] = cmp_name.Text.Trim();
		dr1["英文抬頭"] = cmp_Ename.Text.Trim();
		dr1["工程名稱"] = F_ProjectNameC;
		dr1["英文名稱"] = F_ProjectNameE;
		dr1["施工地點"] = F_ProjectAddress;
		dr1["會計科目"] = F_ProjectAccount1;
		dr1["會計科目2"] = F_ProjectAccount2;
		dr1["工程編號"] = F_ProjectCode;
		dr1["表尾設定"] = SetEnd.Text;
		dr1["ItemQty"] = F_MainQty;
		dr1["ItemCost"] = F_MainCst;
		dr1["ItemAmt"] = F_MainAmt;
		dr1["AnalysisQty"] = F_AnaQty;
		dr1["AnalysisCost"] = F_AnaCst;
		dr1["AnalysisAmt"] = F_AnaAmt;
		dr1["是否列印日期"] = (chkPrintDate.Checked ? "Y" : "N");
		dr1["列印日期"] = $"{txtPrintDate.Value:yyyy/MM/dd}";
		if ((opTemplate.CheckedIndex == 1 && opRPT_Type.CheckedIndex == 3) || opRPT_Type1.CheckedIndex == 0)
		{
			dr1["說明項編號"] = (CB_Ana_SkipCommentItem.Checked ? "N" : "Y");
			dr1["小計項編號"] = (CB_Ana_SkipSubTotalItem.Checked ? "N" : "Y");
		}
		SysUser oSysUser = new SysUser();
		string DBName = oSysUser.GetSysUserDatabaseName(F_UserID);
		dr1["DBName"] = (CB_ExtraParam.Checked ? DBName : "");
		DataInfo.Rows.Add(dr1);
		DataInfo.TableName = "DataInfo";
		DS.Tables.Add(DataInfo);
		return true;
	}

	private void Render_RPT_0()
	{
		Pnl_00.Visible = false;
		UC_CRP0 = new ucCrystalViewer();
		UC_CRP0._ReportPath = F_ReportPath;
		UC_CRP0._ReportName = GetReportName();
		UC_CRP0._DBFName = GetDBFName();
		UC_CRP0._CompWidth = Tab_RPT.Width;
		UC_CRP0._CompHeight = Tab_RPT.Height;
		UC_CRP0._Params = GetParams();
		UC_CRP0.Dock = DockStyle.Fill;
		Pnl_00.Controls.Add(UC_CRP0);
		Tab_0.Tab.Selected = true;
		iActivePage = 0;
		UC_CRP0.Execute();
		Pnl_00.Visible = true;
	}

	private void Render_RPT_1()
	{
		Pnl_01.Visible = false;
		UC_CRP1 = new ucCrystalViewer();
		UC_CRP1._ReportPath = F_ReportPath;
		UC_CRP1._ReportName = GetReportName();
		UC_CRP1._DBFName = GetDBFName();
		UC_CRP1._CompWidth = Tab_RPT.Width;
		UC_CRP1._CompHeight = Tab_RPT.Height;
		UC_CRP1._Params = GetParams();
		UC_CRP1.Dock = DockStyle.Fill;
		Pnl_01.Controls.Add(UC_CRP1);
		Tab_1.Tab.Selected = true;
		iActivePage = 1;
		UC_CRP1.Execute();
		Pnl_01.Visible = true;
	}

	private void Render_RPT_2()
	{
		Pnl_02.Visible = false;
		UC_CRP2 = new ucCrystalViewer();
		UC_CRP2._ReportPath = F_ReportPath;
		UC_CRP2._ReportName = GetReportName();
		UC_CRP2._DBFName = GetDBFName();
		UC_CRP2._CompWidth = Tab_RPT.Width;
		UC_CRP2._CompHeight = Tab_RPT.Height;
		UC_CRP2._Params = GetParams();
		UC_CRP2.Dock = DockStyle.Fill;
		Pnl_02.Controls.Add(UC_CRP2);
		Tab_2.Tab.Selected = true;
		iActivePage = 2;
		UC_CRP2.Execute();
		Pnl_02.Visible = true;
	}

	private void Render_RPT_3()
	{
		Pnl_03.Visible = false;
		UC_CRP3 = new ucCrystalViewer();
		UC_CRP3._ReportPath = F_ReportPath;
		UC_CRP3._ReportName = GetReportName();
		UC_CRP3._DBFName = GetDBFName();
		UC_CRP3._CompWidth = Tab_RPT.Width;
		UC_CRP3._CompHeight = Tab_RPT.Height;
		UC_CRP3._Params = GetParams();
		UC_CRP3.Dock = DockStyle.Fill;
		Pnl_03.Controls.Add(UC_CRP3);
		Tab_3.Tab.Selected = true;
		iActivePage = 3;
		UC_CRP3.Execute();
		Pnl_03.Visible = true;
	}

	private void Render_RPT_4()
	{
		Pnl_04.Visible = false;
		UC_CRP4 = new ucCrystalViewer();
		UC_CRP4._ReportPath = F_ReportPath;
		UC_CRP4._ReportName = GetReportName();
		UC_CRP4._DBFName = GetDBFName();
		UC_CRP4._CompWidth = Tab_RPT.Width;
		UC_CRP4._CompHeight = Tab_RPT.Height;
		UC_CRP4._Params = GetParams();
		UC_CRP4.Dock = DockStyle.Fill;
		Pnl_04.Controls.Add(UC_CRP4);
		Tab_4.Tab.Selected = true;
		iActivePage = 4;
		UC_CRP4.Execute();
		Pnl_04.Visible = true;
	}

	private ExecResult ProcessReportData()
	{
		ExecResult ER = new ExecResult();
		tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(PRINT) 列印報表");
		if (opRPT_Way.CheckedIndex == 0)
		{
			if (opRPT_Type.CheckedIndex == 0)
			{
				myTable = Master_Rep();
			}
			else if (opRPT_Type.CheckedIndex == 1)
			{
				myTable = Sum_Rep();
				li_len = 52;
			}
			else if (opRPT_Type.CheckedIndex == 2)
			{
				myTable = Deti_Rep();
				li_len = 36;
			}
			else if (opRPT_Type.CheckedIndex == 3)
			{
				myTable = Analysis_Rep();
				li_len = 40;
			}
			else if (opRPT_Type.CheckedIndex == 4)
			{
				myTable = Resource_Rep();
				li_len = 34;
			}
			if (opRPT_Type1.CheckedIndex == 0)
			{
				myTable = Analysis_Rep();
				li_len = 40;
			}
			if (opRPT_Type2.CheckedIndex == 0)
			{
				myTable = Resource_Rep();
				li_len = 34;
			}
		}
		else if (opRPT_Type.CheckedIndex == 1)
		{
			myTable = Sum_Rep();
			li_len = 52;
		}
		else if (opRPT_Type.CheckedIndex == 2)
		{
			myTable = Deti_Rep();
			li_len = 52;
		}
		else if (opRPT_Type.CheckedIndex == 3)
		{
			myTable = Analysis_Rep();
			li_len = 30;
		}
		if (myTable.Rows.Count == 0)
		{
			Tab_RPT1.Tab.Selected = true;
			base.WindowState = FormWindowState.Normal;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			ER.ReturnCode = 1;
			ER.Message = "無資料可列印，請先確認資料完 或 重新總計。";
		}
		if (ER.ReturnCode == 0)
		{
			ER = ExtraWork();
		}
		return ER;
	}

	private ExecResult ExtraWork()
	{
		ExecResult ER = new ExecResult();
		SetParams();
		myTable.Columns.Add("cname_line", Type.GetType("System.Int16"));
		myTable.Columns.Add("ename_line", Type.GetType("System.Int16"));
		myTable.Columns.Add("line_len", Type.GetType("System.Int16"));
		string gTableName = Guid.NewGuid().ToString();
		Class1 cl1 = new Class1(F_UserID);
		DataTable NewDT = myTable.Copy();
		NewDT.Clear();
		NewDT.Columns.Add("NewPage", Type.GetType("System.String"));
		NewDT.Columns.Add("ItemNum", Type.GetType("System.Int16"));
		NewDT.Columns.Add("ColNo", Type.GetType("System.Int16"));
		NewDT.Columns.Add("BidShow", Type.GetType("System.String"));
		int RowNo = 0;
		if (opRPT_Type.CheckedIndex == 0)
		{
			if (myTable.Columns.IndexOf("ename") > -1)
			{
				foreach (DataRow dr in myTable.Rows)
				{
					int li_line = 0;
					li_line = ((opRPT_Type.CheckedIndex != 3 && opRPT_Type1.CheckedIndex != 0) ? cl1.fldline(dr["ename"].ToString(), li_len) : cl1.fldline(dr["eNameB"].ToString(), li_len));
					dr["ename_line"] = li_line;
					li_line = 0;
					li_line = ((opRPT_Type.CheckedIndex != 3 && opRPT_Type1.CheckedIndex != 0) ? cl1.cfldline(dr["cname"].ToString(), li_len) : cl1.cfldline(dr["cNameB"].ToString(), li_len));
					dr["cname_line"] = li_line;
					dr["line_len"] = li_len;
				}
			}
			myTable.TableName = "PccesAccess";
			DS.Tables.Add(myTable);
			return cl1.CreateReport(DS, "PccesAccess", F_ReportPath + GetDBFName(), F_ReportPath);
		}
		foreach (DataRow dr in myTable.Rows)
		{
			int x = 1;
			string ColName = "";
			ColName = ((opRPT_Type.CheckedIndex != 3 && opRPT_Type1.CheckedIndex != 0) ? "cname" : "cNameB");
			int Memo_len = 0;
			if (opRPT_Way.CheckedIndex == 0)
			{
				if (opRPT_Type.CheckedIndex == 1)
				{
					Memo_len = 100;
				}
				else if (opRPT_Type.CheckedIndex == 2)
				{
					Memo_len = 14;
				}
				else if (opRPT_Type.CheckedIndex == 3)
				{
					Memo_len = 14;
				}
				else if (opRPT_Type.CheckedIndex == 4)
				{
					Memo_len = 20;
				}
				else if (opRPT_Type2.CheckedIndex == 0)
				{
					Memo_len = 20;
				}
			}
			else if (opRPT_Type.CheckedIndex == 1)
			{
				Memo_len = 100;
			}
			else if (opRPT_Type.CheckedIndex == 2)
			{
				Memo_len = 14;
			}
			else if (opRPT_Type.CheckedIndex == 3)
			{
				Memo_len = 30;
			}
			else if (opRPT_Type.CheckedIndex == 4)
			{
				Memo_len = 20;
			}
			string[] tmpm = new string[0];
			if (opRPT_Type.CheckedIndex == 3 || opRPT_Type1.CheckedIndex == 0)
			{
				try
				{
					tmpm = cl1.GetCstring(dr["memoB"].ToString().Replace(" ", ""), Memo_len);
				}
				catch (Exception ex)
				{
					CommonMethods.LogFile("Pcces46", "M", "Report.FormReportViewer.cs" + ex.Message);
				}
			}
			else
			{
				try
				{
					tmpm = cl1.GetCstring(dr["memo"].ToString(), Memo_len);
				}
				catch (Exception ex)
				{
					CommonMethods.LogFile("Pcces46", "M", "Report.FormReportViewer.cs" + ex.Message);
				}
			}
			string[] tmpc = cl1.GetCstring(dr[ColName].ToString(), li_len);
			string[] tmpe = new string[0];
			if (dr["PccesCode"].ToString().Trim() == "02252Q1H01")
			{
				tmpe = new string[0];
			}
			if (opTemplate.CheckedIndex == 1)
			{
				tmpe = ((opRPT_Type.CheckedIndex != 3 && opRPT_Type1.CheckedIndex != 0) ? cl1.GetEstring(dr["ename"].ToString(), li_len) : cl1.GetEstring(dr["eNameB"].ToString(), li_len));
			}
			int RowCount = 0;
			RowCount = ((tmpe.Length + tmpc.Length <= tmpm.Length) ? tmpm.Length : (tmpe.Length + tmpc.Length));
			for (int i = 0; i < tmpc.Length; i++)
			{
				DataRow ndr = NewDT.NewRow();
				for (int j = 0; j < myTable.Columns.Count; j++)
				{
					ndr[j] = dr[j];
				}
				try
				{
					if (opRPT_Type.CheckedIndex == 3 || opRPT_Type1.CheckedIndex == 0)
					{
						ndr["memoB"] = tmpm[x - 1].Replace(" ", "");
					}
					else
					{
						ndr["memo"] = tmpm[x - 1];
					}
				}
				catch
				{
					try
					{
						if (opRPT_Type.CheckedIndex == 3 || opRPT_Type1.CheckedIndex == 0)
						{
							ndr["memoB"] = "";
						}
						else
						{
							ndr["memo"] = "";
						}
					}
					catch (Exception ex)
					{
						CommonMethods.LogFile("Pcces46", "M", "Report.FormReportViewer.cs" + ex.Message);
					}
				}
				if (opTemplate.CheckedIndex == 1)
				{
					switch (x)
					{
					default:
						if (opRPT_Type.CheckedIndex != 4 && opRPT_Type2.CheckedIndex != 0)
						{
							ndr["ItemNo"] = "";
						}
						break;
					case 2:
						if (opRPT_Type.CheckedIndex != 4 && opRPT_Type2.CheckedIndex != 0)
						{
							ndr["ItemNo"] = "";
						}
						if (opRPT_Type.CheckedIndex == 3 || opRPT_Type1.CheckedIndex == 0)
						{
							ndr["UnitNameB"] = ndr["eUnitB"];
						}
						else
						{
							ndr["UnitName"] = ndr["eUnit"];
						}
						break;
					case 1:
						break;
					}
				}
				ndr[ColName] = tmpc[i];
				bool lb_NewPage = false;
				if (opRPT_Type.CheckedIndex == 2 && x == 1 && dr["PrintNo"].ToString().Trim().Length == 4)
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
			if (opTemplate.CheckedIndex == 1)
			{
				for (int i = 0; i < tmpe.Length; i++)
				{
					DataRow ndr = NewDT.NewRow();
					for (int j = 0; j < myTable.Columns.Count; j++)
					{
						ndr[j] = dr[j];
					}
					try
					{
						if (opRPT_Type.CheckedIndex == 3 || opRPT_Type1.CheckedIndex == 0)
						{
							ndr["memoB"] = tmpm[x - 1].Replace(" ", "");
						}
						else
						{
							ndr["memo"] = tmpm[x - 1];
						}
					}
					catch
					{
						try
						{
							if (opRPT_Type.CheckedIndex == 3 || opRPT_Type1.CheckedIndex == 0)
							{
								ndr["memoB"] = "";
							}
							else
							{
								ndr["memo"] = "";
							}
						}
						catch (Exception ex)
						{
							CommonMethods.LogFile("Pcces46", "M", "Report.FormReportViewer.cs" + ex.Message);
						}
					}
					if (x != 2)
					{
						ndr["UnitName"] = "";
					}
					else if (opRPT_Type.CheckedIndex == 3 || opRPT_Type1.CheckedIndex == 0)
					{
						ndr["UnitNameB"] = ndr["eUnitB"];
					}
					else
					{
						ndr["UnitName"] = ndr["eUnit"];
					}
					if (opRPT_Type.CheckedIndex != 4 && opRPT_Type2.CheckedIndex != 0)
					{
						ndr["ItemNo"] = "";
					}
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
				if (opRPT_Type.CheckedIndex == 3 || opRPT_Type1.CheckedIndex == 0)
				{
					ndr["PrintNo"] = dr["PrintNo"].ToString().Trim();
					ndr["ReportPrintNo"] = dr["ReportPrintNo"].ToString().Trim();
					ndr["ItemNo"] = dr["ItemNo"];
				}
				ndr["ItemNum"] = x;
				ndr["NewPage"] = "N";
				ndr["ColNo"] = RowNo;
				if (opRPT_Type.CheckedIndex == 3 || opRPT_Type1.CheckedIndex == 0)
				{
					ndr["memoB"] = tmpm[x - 1];
				}
				else
				{
					ndr["memo"] = tmpm[x - 1];
				}
				RowNo++;
				NewDT.Rows.Add(ndr);
			}
			if (opTemplate.CheckedIndex == 1 && x % 2 == 0)
			{
				DataRow ndr = NewDT.NewRow();
				ndr["PccesCode"] = dr["PccesCode"];
				if (opRPT_Type.CheckedIndex == 3 || opRPT_Type1.CheckedIndex == 0)
				{
					ndr["ReportPrintNo"] = dr["ReportPrintNo"].ToString().Trim();
					ndr["PrintNo"] = dr["PrintNo"].ToString().Trim();
					ndr["ItemNo"] = dr["ItemNo"];
				}
				ndr["ItemNum"] = x;
				ndr["NewPage"] = "N";
				ndr["ColNo"] = RowNo;
				RowNo++;
				NewDT.Rows.Add(ndr);
			}
		}
		if (opRPT_Type.CheckedIndex == 3 && chkAnaHalf.Checked && opTemplate.CheckedIndex == 0)
		{
			if (opRPT_Type.CheckedIndex == 3 || opRPT_Type1.CheckedIndex == 0)
			{
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
				if (opSort.CheckedIndex == 0)
				{
					DV111.Sort = "PrintNo,itemNo";
				}
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
			}
			DataTable DT_Size = new DataTable("AnaSize");
			DT_Size.Columns.Add("PccesCode", Type.GetType("System.String"));
			DT_Size.Columns.Add("RowCount", Type.GetType("System.Int32"));
			DT_Size.Columns.Add("PageSize", Type.GetType("System.String"));
			DT_Size.Columns.Add("Seq", Type.GetType("System.Int32"));
			DT_Size.Columns.Add("ItemNo", Type.GetType("System.String"));
			DT_Size.Columns.Add("ReportPrintNo", Type.GetType("System.String"));
			string ssPccesCode = "";
			int iiCount = 0;
			int iiSeq = 0;
			for (int i = 0; i < NewDT.Rows.Count; i++)
			{
				if (opRepeat.CheckedIndex == 1)
				{
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
				else if (opRepeat.CheckedIndex == 0)
				{
					string tmpPrintNo = NewDT.Rows[i]["ReportPrintNo"].ToString().Trim().Substring(0, NewDT.Rows[i]["ReportPrintNo"].ToString().Trim().Length - 4);
					if ((tmpPrintNo != ssPccesCode && ssPccesCode != "") || i == NewDT.Rows.Count - 1)
					{
						DataRow DR = DT_Size.NewRow();
						DR["PccesCode"] = ssPccesCode;
						DR["RowCount"] = ((i != NewDT.Rows.Count - 1) ? iiCount : (++iiCount));
						DR["PageSize"] = "";
						DR["Seq"] = ++iiSeq;
						DR["ItemNo"] = NewDT.Rows[i - 1]["ItemNo"].ToString().Trim();
						DR["ReportPrintNo"] = NewDT.Rows[i - 1]["ReportPrintNo"].ToString().Trim().Substring(0, NewDT.Rows[i - 1]["ReportPrintNo"].ToString().Trim().Length - 4);
						DT_Size.Rows.Add(DR);
						iiCount = 0;
					}
					iiCount++;
					ssPccesCode = NewDT.Rows[i]["ReportPrintNo"].ToString().Trim().Substring(0, NewDT.Rows[i]["ReportPrintNo"].ToString().Trim().Length - 4);
				}
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
			if (opRepeat.CheckedIndex == 1)
			{
				DT_Size.CaseSensitive = true;
				DataView DV_SIZE = DT_Size.DefaultView;
				DV_SIZE.Sort = "PccesCode";
				for (int i = 0; i < NewDT.Rows.Count; i++)
				{
					int iidex = DV_SIZE.Find(NewDT.Rows[i]["pccesCode"].ToString().Trim());
					NewDT.Rows[i]["papersize"] = DV_SIZE[iidex]["PageSize"];
				}
			}
			else
			{
				DataView DV_SIZE = DT_Size.DefaultView;
				DV_SIZE.Sort = "ReportPrintNo";
				string _sPccesCode = "";
				for (int i = 0; i < NewDT.Rows.Count; i++)
				{
					string tmpPrintNo = NewDT.Rows[i]["ReportPrintNo"].ToString().Trim().Substring(0, NewDT.Rows[i]["ReportPrintNo"].ToString().Trim().Length - 4);
					if (_sPccesCode != tmpPrintNo)
					{
						int iidex = DV_SIZE.Find(NewDT.Rows[i]["ReportPrintNo"].ToString().Trim().Substring(0, NewDT.Rows[i]["ReportPrintNo"].ToString().Trim().Length - 4));
						if (iidex > -1)
						{
							NewDT.Rows[i]["papersize"] = DV_SIZE[iidex]["PageSize"];
						}
						else
						{
							NewDT.Rows[i]["papersize"] = "L";
						}
					}
					else
					{
						int iidex = DV_SIZE.Find(NewDT.Rows[i]["ReportPrintNo"].ToString().Trim().Substring(0, NewDT.Rows[i]["ReportPrintNo"].ToString().Trim().Length - 4));
						if (iidex > -1)
						{
							NewDT.Rows[i]["papersize"] = DV_SIZE[iidex]["PageSize"];
						}
						else
						{
							NewDT.Rows[i]["papersize"] = "L";
						}
					}
					_sPccesCode = NewDT.Rows[i]["ReportPrintNo"].ToString().Trim().Substring(0, NewDT.Rows[i]["ReportPrintNo"].ToString().Trim().Length - 4);
				}
			}
			DataTable DTAnaTemp = NewDT.Clone();
			if (opRepeat.CheckedIndex == 1)
			{
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
			}
			else if (opRepeat.CheckedIndex == 0)
			{
				string sPrintNo = NewDT.Rows[0]["ReportPrintNo"].ToString().Trim().Substring(0, NewDT.Rows[0]["ReportPrintNo"].ToString().Trim().Length - 4);
				for (int i = 0; i < NewDT.Rows.Count; i++)
				{
					string tmpPrintNo = NewDT.Rows[i]["ReportPrintNo"].ToString().Trim().Substring(0, NewDT.Rows[i]["ReportPrintNo"].ToString().Trim().Length - 4);
					if (tmpPrintNo == sPrintNo)
					{
						DataRow DR2 = DTAnaTemp.NewRow();
						for (int j = 0; j < NewDT.Columns.Count; j++)
						{
							DR2[j] = NewDT.Rows[i][j];
						}
						DTAnaTemp.Rows.Add(DR2);
						if (i == NewDT.Rows.Count - 1 && NewDT.Rows[i]["papersize"].ToString() == "S")
						{
							DataView DV_SIZE2 = DT_Size.DefaultView;
							DV_SIZE2.Sort = "ReportPrintNo";
							int iidex = DV_SIZE2.Find(NewDT.Rows[i - 1]["ReportPrintNo"].ToString().Trim().Substring(0, NewDT.Rows[i - 1]["ReportPrintNo"].ToString().Trim().Length - 4));
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
							DataView DV_SIZE2 = DT_Size.DefaultView;
							DV_SIZE2.Sort = "ReportPrintNo";
							int iidex = DV_SIZE2.Find(NewDT.Rows[i - 1]["ReportPrintNo"].ToString().Trim().Substring(0, NewDT.Rows[i - 1]["ReportPrintNo"].ToString().Trim().Length - 4));
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
						if (sPrintNo != "")
						{
							DataRow DR2 = DTAnaTemp.NewRow();
							for (int j = 0; j < NewDT.Columns.Count; j++)
							{
								DR2[j] = NewDT.Rows[i][j];
							}
							DTAnaTemp.Rows.Add(DR2);
						}
					}
					sPrintNo = NewDT.Rows[i]["ReportPrintNo"].ToString().Trim().Substring(0, NewDT.Rows[i]["ReportPrintNo"].ToString().Trim().Length - 4);
				}
			}
			NewDT.Clear();
			NewDT = DTAnaTemp.Copy();
		}
		else if (opRPT_Type.CheckedIndex == 3 && !chkAnaHalf.Checked && opTemplate.CheckedIndex == 0 && (opRPT_Type.CheckedIndex == 3 || opRPT_Type1.CheckedIndex == 0))
		{
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
			if (opSort.CheckedIndex == 0)
			{
				DV111.Sort = "PrintNo,itemNo";
			}
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
		}
		if (opRPT_Type1.CheckedIndex == 0 && (opRPT_Type.CheckedIndex == 3 || opRPT_Type1.CheckedIndex == 0))
		{
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
			if (opSort.CheckedIndex == 0)
			{
				DV111.Sort = "PrintNo,itemNo";
			}
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
		}
		if (opRPT_Type.CheckedIndex == 3 || opRPT_Type1.CheckedIndex == 0)
		{
			string ls_PrintNo = "";
			string ls_ItemNo = "";
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
				ls_ItemNo = dr["ItemNo"].ToString().Trim();
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
		}
		if (opRPT_Type.CheckedIndex == 2)
		{
			string IsMainPrice = "0";
			NewDT.Columns.Add("ShowMainPrice", Type.GetType("System.String"));
			if (CB_mainprice.Checked)
			{
				IsMainPrice = "1";
			}
			for (int i = 0; i < NewDT.Rows.Count; i++)
			{
				NewDT.Rows[i]["ShowMainPrice"] = IsMainPrice;
			}
			NewDT.Columns.Add("CustomPgBk", Type.GetType("System.String"));
			DBClass DBCLS = new DBClass();
			DBCLS._FS_UserID = F_UserID;
			DataTable DT_PGBK = DBCLS.GetUserDefine("Select SNo,IsPageBreak from " + CommonMethods.GetActionNameString(F_ActionName) + "PageBreak Where ProjectCode='" + F_ProjectCode + "' ");
			DataView DV_NewDT = NewDT.DefaultView;
			for (int z = 0; z < DT_PGBK.Rows.Count; z++)
			{
				if (DT_PGBK.Rows[z]["IsPageBreak"].ToString() == "Y")
				{
					int idx = GetDTDetailRowIndex(ref NewDT, (int)DT_PGBK.Rows[z]["SNo"]);
					if (idx > -1)
					{
						NewDT.Rows[idx]["CustomPgBk"] = "Y";
					}
				}
			}
		}
		if ((opTemplate.CheckedIndex == 1 && opRPT_Type.CheckedIndex == 3) || opRPT_Type1.CheckedIndex == 0)
		{
			for (int i = 0; i < NewDT.Rows.Count; i++)
			{
				string sCNameB = NewDT.Rows[i]["cNameB"].ToString().Trim();
				if (sCNameB == "合計" || sCNameB == "小計" || sCNameB == "計")
				{
					NewDT.Rows[i]["CostKindB"] = "Z";
				}
			}
		}
		string l_Bidshow = "0";
		if (opRPT_Typebid.CheckedIndex == 1)
		{
			l_Bidshow = "1";
		}
		if (opRPT_Type.CheckedIndex == 3 || opRPT_Type1.CheckedIndex == 0)
		{
			for (int i = 0; i < NewDT.Rows.Count; i++)
			{
				NewDT.Rows[i]["ItemNo"] = NewDT.Rows[i]["ItemNo"].ToString().Replace(" ", "");
				if (F_BidBud.ToUpper() == "BUD")
				{
					NewDT.Rows[i]["BidShow"] = l_Bidshow;
					if (PubTools.Str2Double(NewDT.Rows[i]["cost"].ToString()) > 0.0)
					{
						NewDT.Rows[i]["BidShow"] = "1";
					}
				}
				else
				{
					NewDT.Rows[i]["BidShow"] = "1";
				}
			}
		}
		else
		{
			for (int i = 0; i < NewDT.Rows.Count; i++)
			{
				if (F_BidBud.ToUpper() == "BUD")
				{
					NewDT.Rows[i]["BidShow"] = l_Bidshow;
					if (PubTools.Str2Double(NewDT.Rows[i]["cost"].ToString()) > 0.0)
					{
						NewDT.Rows[i]["BidShow"] = "1";
					}
				}
				else
				{
					NewDT.Rows[i]["BidShow"] = "1";
				}
			}
		}
		NewDT.TableName = "PccesAccess";
		DS.Tables.Add(NewDT);
		return cl1.CreateReport(DS, "PccesAccess", F_ReportPath + GetDBFName(), F_ReportPath);
	}

	private int GetDTDetailRowIndex(ref DataTable DT_New, int iSNo)
	{
		int RetV = -1;
		for (int i = 0; i < DT_New.Rows.Count; i++)
		{
			if (PubTools.Str2Int(DT_New.Rows[i]["sNo"]) == iSNo && PubTools.Str2Int(DT_New.Rows[i]["ItemNum"]) == 1)
			{
				RetV = i;
				break;
			}
		}
		return RetV;
	}

	private DataTable Master_Rep()
	{
		ArrayList arrayList;
		(arrayList = tmp_AL1)[1] = string.Concat(arrayList[1], "-專案內容 ");
		Repclass repcom = new Repclass(tmp_AL1);
		repcom.ps_sckind = CommonMethods.GetActionNameString(F_ActionName);
		myTable = repcom.MasterRpt(F_ProjectCode);
		if (opTemplate.CheckedIndex == 1)
		{
			myTable.Columns.Add("Pic", Type.GetType("System.Int32"));
			myTable.Columns.Add("Doc", Type.GetType("System.Int32"));
			myTable.Columns.Add("Sum", Type.GetType("System.Int32"));
			myTable.Columns.Add("Dei", Type.GetType("System.Int32"));
			myTable.Columns.Add("Ana", Type.GetType("System.Int32"));
			int li_Pic = PubTools.Str2Int(tb_Pic.Text);
			int li_Doc = PubTools.Str2Int(tb_Desc.Text);
			int li_Sum = PubTools.Str2Int(tb_Sum.Text);
			int li_Dei = PubTools.Str2Int(tb_Dei.Text);
			int li_Ana = PubTools.Str2Int(tb_Ana.Text);
			for (int i = myTable.Rows.Count - 1; i > -1; i--)
			{
				DataRow dr = myTable.Rows[i];
				if (i == 0)
				{
					dr["Pic"] = li_Pic;
					dr["Doc"] = li_Doc;
					dr["Sum"] = li_Sum;
					dr["Dei"] = li_Dei;
					dr["Ana"] = li_Ana;
				}
				else
				{
					myTable.Rows.Remove(dr);
				}
			}
		}
		repcom = null;
		PubTools.WriteRoughlyLog(tmp_AL1);
		return myTable;
	}

	private DataTable Sum_Rep()
	{
		tmp_AL1[0] = F_UserID;
		ArrayList arrayList;
		(arrayList = tmp_AL1)[1] = string.Concat(arrayList[1], "-總表 ");
		Repclass repcom = new Repclass(tmp_AL1);
		repcom.ps_sckind = CommonMethods.GetActionNameString(F_ActionName);
		repcom.ps_analysisMark = pricemark.Text.Trim();
		if (CB_remark.Checked)
		{
			memoKind = "1";
		}
		else
		{
			memoKind = "0";
		}
		if (CB_pccescode.Checked)
		{
			memoKind += "1";
		}
		else
		{
			memoKind += "0";
		}
		if (CB_price.Checked)
		{
			memoKind += "1";
		}
		else
		{
			memoKind += "0";
		}
		if (CB_pubcode.Checked)
		{
			memoKind += "1";
		}
		else
		{
			memoKind += "0";
		}
		repcom.ps_memo = memoKind;
		if (Price.Checked)
		{
			repcom.ps_showcost = "1";
		}
		else
		{
			repcom.ps_showcost = "0";
		}
		string ls_level = aileael_DDL.Value.ToString();
		repcom.ps_SummaryIncWorkItem = (CB_IsIncWorkItem.Checked ? "Y" : "N");
		myTable = repcom.MainRpt(F_ProjectCode, ls_level);
		repcom = null;
		foreach (DataRow dr in myTable.Rows)
		{
			if (dr["cName"].ToString().Trim() == "總計")
			{
				dr["cName"] = "總價(總計)";
			}
		}
		PubTools.WriteRoughlyLog(tmp_AL1);
		return myTable;
	}

	private DataTable Deti_Rep()
	{
		ArrayList arrayList;
		(arrayList = tmp_AL1)[1] = string.Concat(arrayList[1], "-詳細表 ");
		Repclass repcom = new Repclass(tmp_AL1);
		repcom.ps_sckind = CommonMethods.GetActionNameString(F_ActionName);
		repcom.ps_analysisMark = pricemark.Text.Trim();
		if (CB_remark.Checked)
		{
			memoKind = "1";
		}
		else
		{
			memoKind = "0";
		}
		if (CB_pccescode.Checked)
		{
			memoKind += "1";
		}
		else
		{
			memoKind += "0";
		}
		if (CB_price.Checked)
		{
			memoKind += "1";
		}
		else
		{
			memoKind += "0";
		}
		if (CB_pubcode.Checked)
		{
			memoKind += "1";
		}
		else
		{
			memoKind += "0";
		}
		repcom.ps_memo = memoKind;
		if (Price.Checked)
		{
			repcom.ps_showcost = "1";
		}
		else
		{
			repcom.ps_showcost = "0";
		}
		myTable = repcom.DetialRpt(F_ProjectCode);
		repcom = null;
		foreach (DataRow dr in myTable.Rows)
		{
			if (dr["cName"].ToString().Trim() == "總計")
			{
				dr["cName"] = "總價(總計)";
			}
			string ls_Itemno = dr["ItemNo"].ToString().Trim();
			int li_LevelNo = dr["PrintNo"].ToString().Trim().Length / 4;
			ls_Itemno = ((li_LevelNo > 0) ? ("".PadLeft(li_LevelNo - 1, ' ') + ls_Itemno) : ls_Itemno);
			dr["ItemNo"] = ls_Itemno;
		}
		PubTools.WriteRoughlyLog(tmp_AL1);
		return myTable;
	}

	private DataTable Analysis_Rep()
	{
		AppLocation = AppDomain.CurrentDomain.BaseDirectory;
		string IsOldReCal = CommonMethods.IniReadValue(AppLocation + "OptionSet.ini", "BDGT", "IsOldReCal");
		string l_Mode = "2";
		ArrayList arrayList;
		(arrayList = tmp_AL1)[1] = string.Concat(arrayList[1], "-單價分析 ");
		Repclass repcom = new Repclass(tmp_AL1);
		repcom.ps_sckind = CommonMethods.GetActionNameString(F_ActionName);
		repcom.ps_analysisMark = pricemark.Text.Trim();
		if (opRepeat.CheckedIndex == 0)
		{
			repcom._Repeat = true;
		}
		if (CB_remark.Checked)
		{
			memoKind = "1";
		}
		else
		{
			memoKind = "0";
		}
		if (CB_pccescode.Checked)
		{
			memoKind += "1";
		}
		else
		{
			memoKind += "0";
		}
		if (CB_price.Checked)
		{
			memoKind += "1";
		}
		else
		{
			memoKind += "0";
		}
		if (CB_pubcode.Checked)
		{
			memoKind += "1";
		}
		else
		{
			memoKind += "0";
		}
		repcom.ps_memo = memoKind;
		if (opRepeat.CheckedIndex == 0)
		{
			repcom.ps_filter = "1";
		}
		else
		{
			repcom.ps_filter = "0";
		}
		if (Price.Checked)
		{
			repcom.ps_showcost = "1";
		}
		else
		{
			repcom.ps_showcost = "0";
		}
		repcom._IsOldReCal = IsOldReCal;
		repcom.ps_ExecutePath = CommonMethods.ExtractFilePath(Application.ExecutablePath);
		repcom.ps_PrintByPccesCode = ((opSort.CheckedIndex == 1) ? "Y" : "N");
		repcom._Mode = l_Mode;
		myTable = repcom.AnalysisRpt(F_ProjectCode);
		myTable.Columns.Add("LastItem", Type.GetType("System.String"));
		string ls_code = "";
		if (opSort.CheckedIndex == 1)
		{
			DataTable tmpdt = myTable.Copy();
			DataView tmpdv = tmpdt.DefaultView;
			tmpdv.Sort = "PccesCode,seq";
			myTable.Clear();
			int RecNo = 0;
			string tmpItemNo = "";
			for (int i = 0; i < tmpdv.Count; i++)
			{
				if (ls_code != tmpdv[i]["PccesCode"].ToString())
				{
					ls_code = tmpdv[i]["PccesCode"].ToString();
					tmpItemNo = tmpdv[i]["ItemNo"].ToString();
					RecNo++;
				}
				if (!(tmpItemNo == tmpdv[i]["ItemNo"].ToString()))
				{
					continue;
				}
				DataRow dr = myTable.NewRow();
				for (int j = 0; j < myTable.Columns.Count; j++)
				{
					if (myTable.Columns[j].ColumnName.ToUpper() == "ITEMNO")
					{
						if (l_Mode == "1")
						{
							dr[j] = tmpdv[i][j];
						}
						else if (l_Mode == "2")
						{
							dr[j] = tmpdv[i][j];
						}
						else
						{
							dr[j] = RecNo.ToString().PadLeft(5, ' ');
						}
					}
					else if (myTable.Columns[j].ColumnName.ToUpper() == "PRINTNO")
					{
						dr[j] = RecNo.ToString().PadLeft(20, '0');
					}
					else
					{
						dr[j] = tmpdv[i][j];
					}
				}
				myTable.Rows.Add(dr);
			}
		}
		else if (opRepeat.CheckedIndex == 1)
		{
			DataTable tmpdt = myTable.Copy();
			DataView tmpdv = tmpdt.DefaultView;
			tmpdv.Sort = "PccesCode,seq";
			myTable.Clear();
			string tmpItemNo = "";
			for (int i = 0; i < tmpdv.Count; i++)
			{
				if (ls_code != tmpdv[i]["PccesCode"].ToString() || (CB_Ana_RepeatDetail.Checked && tmpItemNo != tmpdv[i]["ItemNo"].ToString() && tmpdv[i]["IsDetail"].ToString().Trim() == "ISDETAIL"))
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
			if (opTemplate.CheckedIndex == 1)
			{
				DataTable tmpdt2 = myTable.Copy();
				DataView tmpdv2 = tmpdt2.DefaultView;
				tmpdv2.Sort = "seq";
				myTable.Clear();
				for (int i = 0; i < tmpdv2.Count; i++)
				{
					DataRow dr = myTable.NewRow();
					for (int j = 0; j < myTable.Columns.Count; j++)
					{
						dr[j] = tmpdv2[i][j];
					}
					myTable.Rows.Add(dr);
				}
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
		if (opTemplate.CheckedIndex != 1)
		{
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
						(dataRow = dr)["memob"] = string.Concat(dataRow["memob"], "同", dv1[0]["ItemNo"].ToString().Trim());
					}
				}
			}
		}
		repcom = null;
		PubTools.WriteRoughlyLog(tmp_AL1);
		return myTable;
	}

	private DataTable Resource_Rep()
	{
		ArrayList arrayList;
		(arrayList = tmp_AL1)[1] = string.Concat(arrayList[1], "-資源總表 ");
		Repclass repcom = new Repclass(tmp_AL1);
		repcom.ps_sckind = CommonMethods.GetActionNameString(F_ActionName);
		repcom._ShowAnalysis = true;
		repcom._IsCrystalReport = true;
		if (Price.Checked)
		{
			repcom.ps_showcost = "1";
		}
		else
		{
			repcom.ps_showcost = "0";
		}
		myTable = repcom.ResourceRpt(F_ProjectCode);
		repcom = null;
		foreach (DataRow dr in myTable.Rows)
		{
			if (dr["cName"].ToString().Trim() == "總計")
			{
				dr["cName"] = "總價(總計)";
			}
		}
		PubTools.WriteRoughlyLog(tmp_AL1);
		return myTable;
	}

	private void chkAnalysis_CheckedChanged(object sender, EventArgs e)
	{
	}

	private void ultraCheckEditor1_CheckedChanged(object sender, EventArgs e)
	{
		if (Price.Checked)
		{
			opRPT_Typebid.Visible = false;
			lblAtt.Visible = false;
			return;
		}
		if (F_BidBud.ToUpper() == "BUD")
		{
			opRPT_Typebid.Visible = true;
		}
		MessageBox.Show(this, "※\t注意：本報表不得用為招標標單 \n\n 此項功能為供主辦機關或設計單位提供給其他單位參考之檔案(Word檔、PDF檔) \n\n", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		lblAtt.Visible = true;
	}

	private void groupBox1_Enter(object sender, EventArgs e)
	{
	}

	private void button1_Click(object sender, EventArgs e)
	{
	}

	private void opRPT_Type_ValueChanged(object sender, EventArgs e)
	{
		string iniFileName = CommonMethods.ExtractFilePath(Application.ExecutablePath) + "ExcelExp.ini";
		string readini = "";
		switch (opRPT_Type.CheckedIndex)
		{
		case 0:
			Pnl_Sort.Visible = false;
			Pnl_Memo.Visible = false;
			Pnl_PntLevel.Visible = false;
			CB_IsIncWorkItem.Visible = false;
			if (opTemplate.CheckedIndex == 1)
			{
				Tab_Memo_2.Tab.Selected = true;
			}
			else
			{
				Tab_Memo_1.Tab.Selected = true;
			}
			chkAnaHalf.Visible = false;
			BtnPageBreak.Visible = false;
			break;
		case 1:
			Pnl_Sort.Visible = false;
			Pnl_Memo.Visible = false;
			Pnl_PntLevel.Visible = true;
			CB_IsIncWorkItem.Visible = true;
			Tab_Memo_1.Tab.Selected = true;
			chkAnaHalf.Visible = false;
			BtnPageBreak.Visible = false;
			readini = CommonMethods.IniReadValue(iniFileName, "SUMMARY", "LEVEL");
			if (readini != "")
			{
				aileael_DDL.Value = PubTools.Str2Decimal(readini);
			}
			break;
		case 2:
			Pnl_Sort.Visible = false;
			Pnl_Memo.Visible = true;
			Pnl_PntLevel.Visible = false;
			CB_IsIncWorkItem.Visible = false;
			Tab_Memo_1.Tab.Selected = true;
			chkAnaHalf.Visible = false;
			BtnPageBreak.Visible = true;
			CB_remark.Checked = CommonMethods.IniReadValue(iniFileName, "DETAIL", "IsDetMemo") == "Y";
			CB_pccescode.Checked = CommonMethods.IniReadValue(iniFileName, "DETAIL", "IsDetPccesCode") == "Y";
			CB_price.Checked = CommonMethods.IniReadValue(iniFileName, "DETAIL", "IsDetAnaMark") == "Y";
			pricemark.Text = CommonMethods.IniReadValue(iniFileName, "DETAIL", "DetAnaMark");
			CB_pubcode.Checked = CommonMethods.IniReadValue(iniFileName, "DETAIL", "IsDetExtCode") == "Y";
			CB_mainprice.Checked = CommonMethods.IniReadValue(iniFileName, "DETAIL", "IsDetPrice") == "Y";
			break;
		case 3:
		{
			Pnl_Sort.Visible = true;
			Pnl_Memo.Visible = true;
			Pnl_PntLevel.Visible = false;
			CB_IsIncWorkItem.Visible = false;
			Tab_Memo_1.Tab.Selected = true;
			chkAnaHalf.Visible = true;
			BtnPageBreak.Visible = false;
			CB_remark.Checked = CommonMethods.IniReadValue(iniFileName, "BREAKDOWN", "IsAnaMemo") == "Y";
			CB_pccescode.Checked = CommonMethods.IniReadValue(iniFileName, "BREAKDOWN", "IsAnaPccesCode") == "Y";
			CB_price.Checked = CommonMethods.IniReadValue(iniFileName, "BREAKDOWN", "IsAnaAnaMark") == "Y";
			pricemark.Text = CommonMethods.IniReadValue(iniFileName, "BREAKDOWN", "AnaMark");
			CB_pubcode.Checked = CommonMethods.IniReadValue(iniFileName, "BREAKDOWN", "IsAnaExtCode") == "Y";
			chkAnaHalf.Checked = CommonMethods.IniReadValue(iniFileName, "BREAKDOWN", "IsAnaHalfPage") == "Y";
			opSort.CheckedIndex = PubTools.Str2Int(CommonMethods.IniReadValue(iniFileName, "BREAKDOWN", "SortOrder"));
			int iidex = PubTools.Str2Int(CommonMethods.IniReadValue(iniFileName, "BREAKDOWN", "Repeat"));
			opRepeat.CheckedIndex = ((iidex == 0) ? 1 : 0);
			if (opTemplate.CheckedIndex == 1)
			{
				CB_Ana_SkipCommentItem.Visible = true;
				CB_Ana_SkipSubTotalItem.Visible = true;
			}
			else
			{
				CB_Ana_SkipCommentItem.Visible = false;
				CB_Ana_SkipSubTotalItem.Visible = false;
			}
			break;
		}
		case 4:
			Pnl_Sort.Visible = false;
			Pnl_Memo.Visible = false;
			Pnl_PntLevel.Visible = false;
			CB_IsIncWorkItem.Visible = false;
			Tab_Memo_1.Tab.Selected = true;
			chkAnaHalf.Visible = false;
			BtnPageBreak.Visible = false;
			break;
		}
		if (opRPT_Type.CheckedIndex != 0)
		{
			opRPT_Type1.CheckedIndex = -1;
		}
	}

	private void ultraButton1_Click(object sender, EventArgs e)
	{
		try
		{
			switch (iActivePage)
			{
			case 0:
				Pnl_00.Controls.Remove(UC_CRP0);
				UC_CRP0.Dispose();
				UC_CRP0 = null;
				break;
			case 1:
				Pnl_01.Controls.Remove(UC_CRP1);
				UC_CRP1.Dispose();
				UC_CRP1 = null;
				break;
			case 2:
				Pnl_02.Controls.Remove(UC_CRP2);
				UC_CRP2.Dispose();
				UC_CRP2 = null;
				break;
			case 3:
				Pnl_03.Controls.Remove(UC_CRP3);
				UC_CRP3.Dispose();
				UC_CRP3 = null;
				break;
			case 4:
				Pnl_04.Controls.Remove(UC_CRP4);
				UC_CRP4.Dispose();
				UC_CRP4 = null;
				break;
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Report.FormReportViewer.cs" + ex.Message);
		}
	}

	private void A_Btn_Cncl_Click(object sender, EventArgs e)
	{
		try
		{
			DBClass DBCLS = new DBClass();
			DBCLS._FS_UserID = "PccAdmin";
			string sSQL = "DELETE From UserDefind Where Kind ='DefaultFooter'" + '\r' + " Insert Into UserDefind(Kind, cString, sno) values('DefaultFooter', '" + SetEnd.Text + "',1)";
			DBCLS.ExecuteCommand(sSQL);
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Report.FormReportViewer.cs" + ex.Message);
		}
	}

	private void chkPrintDate_CheckedChanged(object sender, EventArgs e)
	{
		txtPrintDate.Enabled = chkPrintDate.Checked;
	}

	private void FormReportViewer_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (ultraTabControl1.ActiveTab.Key == "Tab_RPT3")
		{
			ultraButton2_Click(this, EventArgs.Empty);
		}
	}

	private void cmp_Ename_Validating(object sender, CancelEventArgs e)
	{
		if (!CommonMethods.CheckValidString((sender as UltraTextEditor).Text))
		{
			e.Cancel = true;
		}
	}

	private void SetEnd_Validating(object sender, CancelEventArgs e)
	{
		if (!CommonMethods.CheckValidString(SetEnd.Text))
		{
			e.Cancel = true;
		}
	}

	private void opSort_ValueChanged(object sender, EventArgs e)
	{
		if (opSort.CheckedIndex == 1)
		{
			opRepeat.CheckedIndex = 1;
			opRepeat.Enabled = false;
		}
		else
		{
			opRepeat.Enabled = true;
		}
		if (opSort.CheckedIndex == 0 && opRepeat.CheckedIndex == 1)
		{
			CB_Ana_RepeatDetail.Enabled = true;
		}
		else
		{
			CB_Ana_RepeatDetail.Enabled = false;
		}
	}

	private void BtnPageBreak_Click(object sender, EventArgs e)
	{
		FormBudgetPageBreak FM_PG_BK = new FormBudgetPageBreak();
		FM_PG_BK._UserID = F_UserID;
		FM_PG_BK._ProjectCode = F_ProjectCode;
		FM_PG_BK._ActionName = F_ActionName;
		FM_PG_BK.Owner = this;
		FM_PG_BK.ShowDialog();
		FM_PG_BK.Close();
		FM_PG_BK.Dispose();
		FM_PG_BK = null;
	}

	private void opRepeat_ValueChanged(object sender, EventArgs e)
	{
		if (opSort.CheckedIndex == 0 && opRepeat.CheckedIndex == 1)
		{
			CB_Ana_RepeatDetail.Enabled = true;
		}
		else
		{
			CB_Ana_RepeatDetail.Enabled = false;
		}
	}

	private void Tab_RPT1_Paint(object sender, PaintEventArgs e)
	{
	}

	private void opRPT_Type_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Alt && e.KeyCode == Keys.F12)
		{
			opRPT_Type1.Visible = !opRPT_Type1.Visible;
		}
		if (e.Alt && e.KeyCode == Keys.F11)
		{
			opRPT_Way.Visible = !opRPT_Way.Visible;
		}
	}

	private void opRPT_Type1_ValueChanged(object sender, EventArgs e)
	{
		if (opRPT_Type1.CheckedIndex == 0)
		{
			opRPT_Type.CheckedIndex = -1;
		}
	}

	private void opRPT_Type_ValueChanged_1(object sender, EventArgs e)
	{
		string iniFileName = CommonMethods.ExtractFilePath(Application.ExecutablePath) + "ExcelExp.ini";
		string readini = "";
		switch (opRPT_Type.CheckedIndex)
		{
		case 0:
			Pnl_Sort.Visible = false;
			Pnl_Memo.Visible = false;
			Pnl_PntLevel.Visible = false;
			CB_IsIncWorkItem.Visible = false;
			if (opTemplate.CheckedIndex == 1)
			{
				Tab_Memo_2.Tab.Selected = true;
			}
			else
			{
				Tab_Memo_1.Tab.Selected = true;
			}
			chkAnaHalf.Visible = false;
			BtnPageBreak.Visible = false;
			opRPT_Type2.Visible = false;
			break;
		case 1:
			CB_mainprice.Visible = false;
			Pnl_Sort.Visible = false;
			Pnl_Memo.Visible = false;
			Pnl_PntLevel.Visible = true;
			CB_IsIncWorkItem.Visible = true;
			Tab_Memo_1.Tab.Selected = true;
			opRPT_Type2.Visible = false;
			chkAnaHalf.Visible = false;
			BtnPageBreak.Visible = false;
			readini = CommonMethods.IniReadValue(iniFileName, "SUMMARY", "LEVEL");
			if (readini != "")
			{
				aileael_DDL.Value = PubTools.Str2Decimal(readini);
			}
			break;
		case 2:
			CB_mainprice.Visible = true;
			Pnl_Sort.Visible = false;
			Pnl_Memo.Visible = true;
			Pnl_PntLevel.Visible = false;
			CB_IsIncWorkItem.Visible = false;
			Tab_Memo_1.Tab.Selected = true;
			chkAnaHalf.Visible = false;
			BtnPageBreak.Visible = true;
			opRPT_Type2.Visible = false;
			CB_remark.Checked = CommonMethods.IniReadValue(iniFileName, "DETAIL", "IsDetMemo") == "Y";
			CB_pccescode.Checked = CommonMethods.IniReadValue(iniFileName, "DETAIL", "IsDetPccesCode") == "Y";
			CB_price.Checked = CommonMethods.IniReadValue(iniFileName, "DETAIL", "IsDetAnaMark") == "Y";
			pricemark.Text = CommonMethods.IniReadValue(iniFileName, "DETAIL", "DetAnaMark");
			CB_pubcode.Checked = CommonMethods.IniReadValue(iniFileName, "DETAIL", "IsDetExtCode") == "Y";
			CB_mainprice.Checked = CommonMethods.IniReadValue(iniFileName, "DETAIL", "IsDetPrice") == "Y";
			break;
		case 3:
		{
			CB_mainprice.Visible = false;
			Pnl_Sort.Visible = true;
			Pnl_Memo.Visible = true;
			Pnl_PntLevel.Visible = false;
			CB_IsIncWorkItem.Visible = false;
			Tab_Memo_1.Tab.Selected = true;
			chkAnaHalf.Visible = true;
			BtnPageBreak.Visible = false;
			opRPT_Type2.Visible = false;
			CB_remark.Checked = CommonMethods.IniReadValue(iniFileName, "BREAKDOWN", "IsAnaMemo") == "Y";
			CB_pccescode.Checked = CommonMethods.IniReadValue(iniFileName, "BREAKDOWN", "IsAnaPccesCode") == "Y";
			CB_price.Checked = CommonMethods.IniReadValue(iniFileName, "BREAKDOWN", "IsAnaAnaMark") == "Y";
			pricemark.Text = CommonMethods.IniReadValue(iniFileName, "BREAKDOWN", "AnaMark");
			CB_pubcode.Checked = CommonMethods.IniReadValue(iniFileName, "BREAKDOWN", "IsAnaExtCode") == "Y";
			chkAnaHalf.Checked = CommonMethods.IniReadValue(iniFileName, "BREAKDOWN", "IsAnaHalfPage") == "Y";
			opSort.CheckedIndex = PubTools.Str2Int(CommonMethods.IniReadValue(iniFileName, "BREAKDOWN", "SortOrder"));
			int iidex = PubTools.Str2Int(CommonMethods.IniReadValue(iniFileName, "BREAKDOWN", "Repeat"));
			opRepeat.CheckedIndex = ((iidex == 0) ? 1 : 0);
			if (opTemplate.CheckedIndex == 1)
			{
				CB_Ana_SkipCommentItem.Visible = true;
				CB_Ana_SkipSubTotalItem.Visible = true;
			}
			else
			{
				CB_Ana_SkipCommentItem.Visible = false;
				CB_Ana_SkipSubTotalItem.Visible = false;
			}
			break;
		}
		case 4:
			CB_mainprice.Visible = false;
			Pnl_Sort.Visible = false;
			Pnl_Memo.Visible = false;
			Pnl_PntLevel.Visible = false;
			CB_IsIncWorkItem.Visible = false;
			Tab_Memo_1.Tab.Selected = true;
			chkAnaHalf.Visible = false;
			BtnPageBreak.Visible = false;
			opRPT_Type2.Visible = true;
			break;
		}
		if (opRPT_Type.CheckedIndex == 4)
		{
			opRPT_Type2.CheckedIndex = -1;
		}
		if (opRPT_Type.CheckedIndex == 3)
		{
			opRPT_Type1.CheckedIndex = -1;
		}
		if (opRPT_Type.CheckedIndex != 1 && opRPT_Way.CheckedIndex == 1)
		{
			if (opRPT_Type.CheckedIndex == 0)
			{
				MessageBox.Show(this, "目前無專案基本資料【橫式】報表。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				opRPT_Type.CheckedIndex = 1;
			}
			if (opRPT_Type.CheckedIndex == 4)
			{
				MessageBox.Show(this, "目前無資源統計表【橫式】報表。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				opRPT_Type.CheckedIndex = 1;
			}
		}
	}

	private void opRPT_Way_ValueChanged(object sender, EventArgs e)
	{
		if (opRPT_Way.CheckedIndex == 1)
		{
			if (opRPT_Type.CheckedIndex == 0)
			{
				MessageBox.Show(this, "目前無專案基本資料【橫式】報表。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				opRPT_Type.CheckedIndex = 1;
			}
			if (opRPT_Type.CheckedIndex == 4)
			{
				MessageBox.Show(this, "目前無資源統計表【橫式】報表。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				opRPT_Type.CheckedIndex = 1;
			}
		}
	}

	private void FormReportViewer_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormReportViewer");
		}
	}

	private void opRPT_Type2_ValueChanged(object sender, EventArgs e)
	{
		if (opRPT_Type2.CheckedIndex == 0)
		{
			opRPT_Type.CheckedIndex = -1;
		}
	}

	private void InitializeComponent()
	{
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton1 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.Report.FormReportViewer));
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem4 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem5 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem6 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem7 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem8 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem9 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem10 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem11 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem12 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem13 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem14 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem15 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem16 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem17 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab5 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab6 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab7 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab8 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab9 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab10 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		this.Tab_Memo_1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.Pnl_Memo = new System.Windows.Forms.Panel();
		this.CB_mainprice = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.pricemark = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.CB_remark = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.CB_pccescode = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.CB_price = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.CB_pubcode = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.Tab_Memo_2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.ultraLabel18 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel17 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel16 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel15 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel14 = new Infragistics.Win.Misc.UltraLabel();
		this.tb_Ana = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.tb_Dei = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.tb_Sum = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.tb_Desc = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.tb_Pic = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel13 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel12 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel11 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel8 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_0 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.Pnl_00 = new System.Windows.Forms.Panel();
		this.Tab_1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.Pnl_01 = new System.Windows.Forms.Panel();
		this.Tab_2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.Pnl_02 = new System.Windows.Forms.Panel();
		this.Tab_3 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.Pnl_03 = new System.Windows.Forms.Panel();
		this.Tab_4 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.Pnl_04 = new System.Windows.Forms.Panel();
		this.Tab_RPT1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.opRPT_Typebid = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
		this.txtPrintDate = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
		this.chkPrintDate = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.groupBox6 = new System.Windows.Forms.GroupBox();
		this.opRPT_Type2 = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
		this.opRPT_Way = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
		this.opRPT_Type1 = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
		this.CB_IsIncWorkItem = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.BtnPageBreak = new Infragistics.Win.Misc.UltraButton();
		this.chkAnaHalf = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.Pnl_PntLevel = new System.Windows.Forms.Panel();
		this.aileael_DDL = new System.Windows.Forms.NumericUpDown();
		this.ultraLabel9 = new Infragistics.Win.Misc.UltraLabel();
		this.opRPT_Type = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.opTemplate = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.ultraTabControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage3 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.Pnl_Sort = new System.Windows.Forms.Panel();
		this.CB_Ana_SkipSubTotalItem = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.CB_Ana_SkipCommentItem = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.CB_Ana_RepeatDetail = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.opSort = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.opRepeat = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.SetEnd = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.cmp_Ename = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.cmp_name = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.panel1 = new System.Windows.Forms.Panel();
		this.CB_ExtraParam = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.A_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.A_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.Price = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.Tab_RPT2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.ultraLabel10 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_RPT3 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.Tab_RPT = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage2 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.panel7 = new System.Windows.Forms.Panel();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton2 = new Infragistics.Win.Misc.UltraButton();
		this.ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.lblAtt = new System.Windows.Forms.Label();
		this.Tab_Memo_1.SuspendLayout();
		this.Pnl_Memo.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pricemark).BeginInit();
		this.Tab_Memo_2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.tb_Ana).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tb_Dei).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tb_Sum).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tb_Desc).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tb_Pic).BeginInit();
		this.Tab_0.SuspendLayout();
		this.Tab_1.SuspendLayout();
		this.Tab_2.SuspendLayout();
		this.Tab_3.SuspendLayout();
		this.Tab_4.SuspendLayout();
		this.Tab_RPT1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.opRPT_Typebid).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPrintDate).BeginInit();
		this.groupBox6.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.opRPT_Type2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.opRPT_Way).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.opRPT_Type1).BeginInit();
		this.Pnl_PntLevel.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.aileael_DDL).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.opRPT_Type).BeginInit();
		this.groupBox5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.opTemplate).BeginInit();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraTabControl2).BeginInit();
		this.ultraTabControl2.SuspendLayout();
		this.Pnl_Sort.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.opSort).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.opRepeat).BeginInit();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.SetEnd).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cmp_Ename).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cmp_name).BeginInit();
		this.panel1.SuspendLayout();
		this.Tab_RPT2.SuspendLayout();
		this.Tab_RPT3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Tab_RPT).BeginInit();
		this.Tab_RPT.SuspendLayout();
		this.panel7.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraTabControl1).BeginInit();
		this.ultraTabControl1.SuspendLayout();
		base.SuspendLayout();
		this.Tab_Memo_1.Controls.Add(this.Pnl_Memo);
		this.Tab_Memo_1.Location = new System.Drawing.Point(0, 0);
		this.Tab_Memo_1.Name = "Tab_Memo_1";
		this.Tab_Memo_1.Size = new System.Drawing.Size(332, 148);
		this.Pnl_Memo.Controls.Add(this.CB_mainprice);
		this.Pnl_Memo.Controls.Add(this.pricemark);
		this.Pnl_Memo.Controls.Add(this.CB_remark);
		this.Pnl_Memo.Controls.Add(this.ultraLabel1);
		this.Pnl_Memo.Controls.Add(this.CB_pccescode);
		this.Pnl_Memo.Controls.Add(this.CB_price);
		this.Pnl_Memo.Controls.Add(this.CB_pubcode);
		this.Pnl_Memo.Location = new System.Drawing.Point(4, 0);
		this.Pnl_Memo.Name = "Pnl_Memo";
		this.Pnl_Memo.Size = new System.Drawing.Size(318, 144);
		this.Pnl_Memo.TabIndex = 11;
		this.Pnl_Memo.Visible = false;
		this.CB_mainprice.Location = new System.Drawing.Point(12, 8);
		this.CB_mainprice.Name = "CB_mainprice";
		this.CB_mainprice.Size = new System.Drawing.Size(234, 20);
		this.CB_mainprice.TabIndex = 26;
		this.CB_mainprice.Text = "一般主項顯示數量單價複價";
		appearance1.FontData.Name = "細明體";
		appearance1.FontData.SizeInPoints = 11f;
		this.pricemark.Appearance = appearance1;
		this.pricemark.Location = new System.Drawing.Point(132, 92);
		this.pricemark.Name = "pricemark";
		this.pricemark.Size = new System.Drawing.Size(136, 24);
		this.pricemark.TabIndex = 9;
		this.pricemark.Text = "*";
		this.pricemark.Validating += new System.ComponentModel.CancelEventHandler(cmp_Ename_Validating);
		this.CB_remark.Location = new System.Drawing.Point(12, 48);
		this.CB_remark.Name = "CB_remark";
		this.CB_remark.TabIndex = 0;
		this.CB_remark.Text = "備註";
		this.ultraLabel1.Location = new System.Drawing.Point(8, 28);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(64, 23);
		this.ultraLabel1.TabIndex = 1;
		this.ultraLabel1.Text = "備註欄:";
		this.CB_pccescode.Location = new System.Drawing.Point(12, 72);
		this.CB_pccescode.Name = "CB_pccescode";
		this.CB_pccescode.TabIndex = 2;
		this.CB_pccescode.Text = "工項代碼";
		this.CB_price.Location = new System.Drawing.Point(12, 96);
		this.CB_price.Name = "CB_price";
		this.CB_price.TabIndex = 3;
		this.CB_price.Text = "單價分析標記";
		this.CB_price.CheckedChanged += new System.EventHandler(chkAnalysis_CheckedChanged);
		this.CB_pubcode.Location = new System.Drawing.Point(12, 120);
		this.CB_pubcode.Name = "CB_pubcode";
		this.CB_pubcode.TabIndex = 4;
		this.CB_pubcode.Text = "外碼";
		this.Tab_Memo_2.Controls.Add(this.ultraLabel18);
		this.Tab_Memo_2.Controls.Add(this.ultraLabel17);
		this.Tab_Memo_2.Controls.Add(this.ultraLabel16);
		this.Tab_Memo_2.Controls.Add(this.ultraLabel15);
		this.Tab_Memo_2.Controls.Add(this.ultraLabel14);
		this.Tab_Memo_2.Controls.Add(this.tb_Ana);
		this.Tab_Memo_2.Controls.Add(this.tb_Dei);
		this.Tab_Memo_2.Controls.Add(this.tb_Sum);
		this.Tab_Memo_2.Controls.Add(this.tb_Desc);
		this.Tab_Memo_2.Controls.Add(this.tb_Pic);
		this.Tab_Memo_2.Controls.Add(this.ultraLabel13);
		this.Tab_Memo_2.Controls.Add(this.ultraLabel12);
		this.Tab_Memo_2.Controls.Add(this.ultraLabel11);
		this.Tab_Memo_2.Controls.Add(this.ultraLabel8);
		this.Tab_Memo_2.Controls.Add(this.ultraLabel7);
		this.Tab_Memo_2.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_Memo_2.Name = "Tab_Memo_2";
		this.Tab_Memo_2.Size = new System.Drawing.Size(332, 148);
		this.ultraLabel18.Location = new System.Drawing.Point(182, 105);
		this.ultraLabel18.Name = "ultraLabel18";
		this.ultraLabel18.Size = new System.Drawing.Size(24, 23);
		this.ultraLabel18.TabIndex = 14;
		this.ultraLabel18.Text = "頁";
		this.ultraLabel17.Location = new System.Drawing.Point(182, 80);
		this.ultraLabel17.Name = "ultraLabel17";
		this.ultraLabel17.Size = new System.Drawing.Size(24, 23);
		this.ultraLabel17.TabIndex = 13;
		this.ultraLabel17.Text = "頁";
		this.ultraLabel16.Location = new System.Drawing.Point(182, 55);
		this.ultraLabel16.Name = "ultraLabel16";
		this.ultraLabel16.Size = new System.Drawing.Size(24, 23);
		this.ultraLabel16.TabIndex = 12;
		this.ultraLabel16.Text = "頁";
		this.ultraLabel15.Location = new System.Drawing.Point(182, 31);
		this.ultraLabel15.Name = "ultraLabel15";
		this.ultraLabel15.Size = new System.Drawing.Size(24, 23);
		this.ultraLabel15.TabIndex = 11;
		this.ultraLabel15.Text = "頁";
		this.ultraLabel14.Location = new System.Drawing.Point(182, 6);
		this.ultraLabel14.Name = "ultraLabel14";
		this.ultraLabel14.Size = new System.Drawing.Size(24, 23);
		this.ultraLabel14.TabIndex = 10;
		this.ultraLabel14.Text = "張";
		this.tb_Ana.Location = new System.Drawing.Point(106, 101);
		this.tb_Ana.Name = "tb_Ana";
		this.tb_Ana.Size = new System.Drawing.Size(76, 21);
		this.tb_Ana.TabIndex = 9;
		this.tb_Ana.Text = "0";
		this.tb_Dei.Location = new System.Drawing.Point(106, 76);
		this.tb_Dei.Name = "tb_Dei";
		this.tb_Dei.Size = new System.Drawing.Size(76, 21);
		this.tb_Dei.TabIndex = 8;
		this.tb_Dei.Text = "0";
		this.tb_Sum.Location = new System.Drawing.Point(106, 51);
		this.tb_Sum.Name = "tb_Sum";
		this.tb_Sum.Size = new System.Drawing.Size(76, 21);
		this.tb_Sum.TabIndex = 7;
		this.tb_Sum.Text = "0";
		this.tb_Desc.Location = new System.Drawing.Point(106, 26);
		this.tb_Desc.Name = "tb_Desc";
		this.tb_Desc.Size = new System.Drawing.Size(76, 21);
		this.tb_Desc.TabIndex = 6;
		this.tb_Desc.Text = "0";
		this.tb_Pic.Location = new System.Drawing.Point(106, 1);
		this.tb_Pic.Name = "tb_Pic";
		this.tb_Pic.Size = new System.Drawing.Size(76, 21);
		this.tb_Pic.TabIndex = 5;
		this.tb_Pic.Text = "0";
		appearance2.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel13.Appearance = appearance2;
		this.ultraLabel13.Location = new System.Drawing.Point(14, 105);
		this.ultraLabel13.Name = "ultraLabel13";
		this.ultraLabel13.Size = new System.Drawing.Size(100, 20);
		this.ultraLabel13.TabIndex = 4;
		this.ultraLabel13.Text = "單價分析表";
		appearance3.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel12.Appearance = appearance3;
		this.ultraLabel12.Location = new System.Drawing.Point(14, 80);
		this.ultraLabel12.Name = "ultraLabel12";
		this.ultraLabel12.Size = new System.Drawing.Size(100, 20);
		this.ultraLabel12.TabIndex = 3;
		this.ultraLabel12.Text = "詳細表";
		appearance4.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel11.Appearance = appearance4;
		this.ultraLabel11.Location = new System.Drawing.Point(14, 55);
		this.ultraLabel11.Name = "ultraLabel11";
		this.ultraLabel11.Size = new System.Drawing.Size(100, 20);
		this.ultraLabel11.TabIndex = 2;
		this.ultraLabel11.Text = "工程總表";
		appearance5.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel8.Appearance = appearance5;
		this.ultraLabel8.Location = new System.Drawing.Point(14, 30);
		this.ultraLabel8.Name = "ultraLabel8";
		this.ultraLabel8.Size = new System.Drawing.Size(100, 20);
		this.ultraLabel8.TabIndex = 1;
		this.ultraLabel8.Text = "說明書";
		appearance6.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel7.Appearance = appearance6;
		this.ultraLabel7.Location = new System.Drawing.Point(14, 6);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(100, 20);
		this.ultraLabel7.TabIndex = 0;
		this.ultraLabel7.Text = "附圖";
		this.Tab_0.Controls.Add(this.Pnl_00);
		this.Tab_0.Location = new System.Drawing.Point(2, 25);
		this.Tab_0.Name = "Tab_0";
		this.Tab_0.Size = new System.Drawing.Size(718, 503);
		this.Pnl_00.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Pnl_00.Location = new System.Drawing.Point(0, 0);
		this.Pnl_00.Name = "Pnl_00";
		this.Pnl_00.Size = new System.Drawing.Size(718, 503);
		this.Pnl_00.TabIndex = 3;
		this.Tab_1.Controls.Add(this.Pnl_01);
		this.Tab_1.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_1.Name = "Tab_1";
		this.Tab_1.Size = new System.Drawing.Size(718, 503);
		this.Pnl_01.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Pnl_01.Location = new System.Drawing.Point(0, 0);
		this.Pnl_01.Name = "Pnl_01";
		this.Pnl_01.Size = new System.Drawing.Size(718, 503);
		this.Pnl_01.TabIndex = 4;
		this.Tab_2.Controls.Add(this.Pnl_02);
		this.Tab_2.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.Tab_2.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_2.Name = "Tab_2";
		this.Tab_2.Size = new System.Drawing.Size(718, 503);
		this.Pnl_02.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Pnl_02.Location = new System.Drawing.Point(0, 0);
		this.Pnl_02.Name = "Pnl_02";
		this.Pnl_02.Size = new System.Drawing.Size(718, 503);
		this.Pnl_02.TabIndex = 5;
		this.Tab_3.Controls.Add(this.Pnl_03);
		this.Tab_3.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_3.Name = "Tab_3";
		this.Tab_3.Size = new System.Drawing.Size(718, 503);
		this.Pnl_03.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Pnl_03.Location = new System.Drawing.Point(0, 0);
		this.Pnl_03.Name = "Pnl_03";
		this.Pnl_03.Size = new System.Drawing.Size(718, 503);
		this.Pnl_03.TabIndex = 6;
		this.Tab_4.Controls.Add(this.Pnl_04);
		this.Tab_4.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_4.Name = "Tab_4";
		this.Tab_4.Size = new System.Drawing.Size(718, 503);
		this.Pnl_04.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Pnl_04.Location = new System.Drawing.Point(0, 0);
		this.Pnl_04.Name = "Pnl_04";
		this.Pnl_04.Size = new System.Drawing.Size(718, 503);
		this.Pnl_04.TabIndex = 6;
		this.Tab_RPT1.Controls.Add(this.lblAtt);
		this.Tab_RPT1.Controls.Add(this.opRPT_Typebid);
		this.Tab_RPT1.Controls.Add(this.txtPrintDate);
		this.Tab_RPT1.Controls.Add(this.chkPrintDate);
		this.Tab_RPT1.Controls.Add(this.groupBox6);
		this.Tab_RPT1.Controls.Add(this.groupBox5);
		this.Tab_RPT1.Controls.Add(this.groupBox3);
		this.Tab_RPT1.Controls.Add(this.groupBox2);
		this.Tab_RPT1.Controls.Add(this.panel1);
		this.Tab_RPT1.Controls.Add(this.Price);
		this.Tab_RPT1.Location = new System.Drawing.Point(0, 0);
		this.Tab_RPT1.Name = "Tab_RPT1";
		this.Tab_RPT1.Size = new System.Drawing.Size(722, 572);
		this.Tab_RPT1.Paint += new System.Windows.Forms.PaintEventHandler(Tab_RPT1_Paint);
		this.opRPT_Typebid.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
		this.opRPT_Typebid.CheckedIndex = 0;
		this.opRPT_Typebid.ItemAppearance = appearance7;
		valueListItem1.DataValue = "Default Item";
		valueListItem1.DisplayText = "單、複價印空白";
		valueListItem2.DataValue = "ValueListItem1";
		valueListItem2.DisplayText = "單、複價印\"0\"";
		this.opRPT_Typebid.Items.Add(valueListItem1);
		this.opRPT_Typebid.Items.Add(valueListItem2);
		this.opRPT_Typebid.Location = new System.Drawing.Point(456, 148);
		this.opRPT_Typebid.Name = "opRPT_Typebid";
		this.opRPT_Typebid.Size = new System.Drawing.Size(260, 20);
		this.opRPT_Typebid.TabIndex = 39;
		this.opRPT_Typebid.Text = "單、複價印空白";
		this.opRPT_Typebid.Visible = false;
		dateButton1.Caption = "今天";
		this.txtPrintDate.DateButtons.Add(dateButton1);
		this.txtPrintDate.DayOfWeekCaptionStyle = Infragistics.Win.UltraWinSchedule.DayOfWeekCaptionStyle.ShortDescription;
		this.txtPrintDate.Location = new System.Drawing.Point(539, 80);
		this.txtPrintDate.Name = "txtPrintDate";
		this.txtPrintDate.NonAutoSizeHeight = 21;
		this.txtPrintDate.NullDateLabel = "";
		this.txtPrintDate.Size = new System.Drawing.Size(132, 21);
		this.txtPrintDate.TabIndex = 38;
		this.txtPrintDate.TipStyle = Infragistics.Win.UltraWinSchedule.TipStyleDay.Holidays;
		this.txtPrintDate.Value = resources.GetObject("txtPrintDate.Value");
		this.txtPrintDate.WeekNumbersVisible = true;
		this.chkPrintDate.Checked = true;
		this.chkPrintDate.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkPrintDate.Location = new System.Drawing.Point(455, 80);
		this.chkPrintDate.Name = "chkPrintDate";
		this.chkPrintDate.Size = new System.Drawing.Size(84, 26);
		this.chkPrintDate.TabIndex = 37;
		this.chkPrintDate.Text = "列印日期";
		this.chkPrintDate.CheckedChanged += new System.EventHandler(chkPrintDate_CheckedChanged);
		this.groupBox6.Controls.Add(this.opRPT_Type2);
		this.groupBox6.Controls.Add(this.opRPT_Way);
		this.groupBox6.Controls.Add(this.opRPT_Type1);
		this.groupBox6.Controls.Add(this.CB_IsIncWorkItem);
		this.groupBox6.Controls.Add(this.BtnPageBreak);
		this.groupBox6.Controls.Add(this.chkAnaHalf);
		this.groupBox6.Controls.Add(this.Pnl_PntLevel);
		this.groupBox6.Controls.Add(this.opRPT_Type);
		this.groupBox6.Location = new System.Drawing.Point(8, 8);
		this.groupBox6.Name = "groupBox6";
		this.groupBox6.Size = new System.Drawing.Size(424, 148);
		this.groupBox6.TabIndex = 30;
		this.groupBox6.TabStop = false;
		this.groupBox6.Text = "報表類別";
		this.opRPT_Type2.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
		this.opRPT_Type2.ItemAppearance = appearance8;
		valueListItem3.DataValue = "Default Item";
		valueListItem3.DisplayText = "資源統計表(格式二)";
		this.opRPT_Type2.Items.Add(valueListItem3);
		this.opRPT_Type2.Location = new System.Drawing.Point(123, 117);
		this.opRPT_Type2.Name = "opRPT_Type2";
		this.opRPT_Type2.Size = new System.Drawing.Size(172, 20);
		this.opRPT_Type2.TabIndex = 28;
		this.opRPT_Type2.Visible = false;
		this.opRPT_Type2.ValueChanged += new System.EventHandler(opRPT_Type2_ValueChanged);
		this.opRPT_Way.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
		this.opRPT_Way.CheckedIndex = 0;
		this.opRPT_Way.ItemAppearance = appearance9;
		valueListItem4.DataValue = "Default Item";
		valueListItem4.DisplayText = "直式";
		valueListItem5.DataValue = "ValueListItem1";
		valueListItem5.DisplayText = "橫式";
		this.opRPT_Way.Items.Add(valueListItem4);
		this.opRPT_Way.Items.Add(valueListItem5);
		this.opRPT_Way.Location = new System.Drawing.Point(140, 20);
		this.opRPT_Way.Name = "opRPT_Way";
		this.opRPT_Way.Size = new System.Drawing.Size(164, 20);
		this.opRPT_Way.TabIndex = 27;
		this.opRPT_Way.Text = "直式";
		this.opRPT_Way.Visible = false;
		this.opRPT_Way.ValueChanged += new System.EventHandler(opRPT_Way_ValueChanged);
		this.opRPT_Type1.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
		this.opRPT_Type1.ItemAppearance = appearance10;
		valueListItem6.DataValue = "Default Item";
		valueListItem6.DisplayText = "單價分析表(格式二)";
		this.opRPT_Type1.Items.Add(valueListItem6);
		this.opRPT_Type1.Location = new System.Drawing.Point(124, 120);
		this.opRPT_Type1.Name = "opRPT_Type1";
		this.opRPT_Type1.Size = new System.Drawing.Size(164, 20);
		this.opRPT_Type1.TabIndex = 26;
		this.opRPT_Type1.Visible = false;
		this.opRPT_Type1.ValueChanged += new System.EventHandler(opRPT_Type1_ValueChanged);
		this.CB_IsIncWorkItem.Location = new System.Drawing.Point(248, 48);
		this.CB_IsIncWorkItem.Name = "CB_IsIncWorkItem";
		this.CB_IsIncWorkItem.TabIndex = 25;
		this.CB_IsIncWorkItem.Text = "包含工作要項";
		appearance11.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnPageBreak.Appearance = appearance11;
		this.BtnPageBreak.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.BtnPageBreak.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Button;
		this.BtnPageBreak.Font = new System.Drawing.Font("新細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.BtnPageBreak.Location = new System.Drawing.Point(122, 68);
		this.BtnPageBreak.Name = "BtnPageBreak";
		this.BtnPageBreak.ShowFocusRect = false;
		this.BtnPageBreak.ShowOutline = false;
		this.BtnPageBreak.Size = new System.Drawing.Size(88, 23);
		this.BtnPageBreak.SupportThemes = false;
		this.BtnPageBreak.TabIndex = 23;
		this.BtnPageBreak.Text = "跳頁設定...";
		this.BtnPageBreak.Visible = false;
		this.BtnPageBreak.Click += new System.EventHandler(BtnPageBreak_Click);
		this.chkAnaHalf.Checked = true;
		this.chkAnaHalf.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkAnaHalf.Location = new System.Drawing.Point(122, 93);
		this.chkAnaHalf.Name = "chkAnaHalf";
		this.chkAnaHalf.Size = new System.Drawing.Size(191, 20);
		this.chkAnaHalf.TabIndex = 22;
		this.chkAnaHalf.Text = "中文使用半頁格式";
		this.chkAnaHalf.Visible = false;
		this.Pnl_PntLevel.Controls.Add(this.aileael_DDL);
		this.Pnl_PntLevel.Controls.Add(this.ultraLabel9);
		this.Pnl_PntLevel.Location = new System.Drawing.Point(121, 41);
		this.Pnl_PntLevel.Name = "Pnl_PntLevel";
		this.Pnl_PntLevel.Size = new System.Drawing.Size(124, 36);
		this.Pnl_PntLevel.TabIndex = 21;
		this.aileael_DDL.Location = new System.Drawing.Point(80, 5);
		this.aileael_DDL.Maximum = new decimal(new int[4] { 6, 0, 0, 0 });
		this.aileael_DDL.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.aileael_DDL.Name = "aileael_DDL";
		this.aileael_DDL.Size = new System.Drawing.Size(40, 25);
		this.aileael_DDL.TabIndex = 19;
		this.aileael_DDL.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		this.ultraLabel9.Location = new System.Drawing.Point(4, 7);
		this.ultraLabel9.Name = "ultraLabel9";
		this.ultraLabel9.Size = new System.Drawing.Size(76, 23);
		this.ultraLabel9.TabIndex = 17;
		this.ultraLabel9.Text = "列印層數:";
		this.opRPT_Type.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
		this.opRPT_Type.CheckedIndex = 1;
		this.opRPT_Type.ItemAppearance = appearance12;
		valueListItem7.DataValue = "Default Item";
		valueListItem7.DisplayText = "專案基本資料";
		valueListItem8.DataValue = "ValueListItem1";
		valueListItem8.DisplayText = "總表";
		valueListItem9.DataValue = "ValueListItem2";
		valueListItem9.DisplayText = "詳細表";
		valueListItem10.DataValue = "ValueListItem3";
		valueListItem10.DisplayText = "單價分析表";
		valueListItem11.DataValue = "ValueListItem4";
		valueListItem11.DisplayText = "資源統計表";
		this.opRPT_Type.Items.Add(valueListItem7);
		this.opRPT_Type.Items.Add(valueListItem8);
		this.opRPT_Type.Items.Add(valueListItem9);
		this.opRPT_Type.Items.Add(valueListItem10);
		this.opRPT_Type.Items.Add(valueListItem11);
		this.opRPT_Type.ItemSpacingVertical = 4;
		this.opRPT_Type.Location = new System.Drawing.Point(8, 21);
		this.opRPT_Type.Name = "opRPT_Type";
		this.opRPT_Type.Size = new System.Drawing.Size(120, 120);
		this.opRPT_Type.TabIndex = 20;
		this.opRPT_Type.Text = "總表";
		this.opRPT_Type.KeyDown += new System.Windows.Forms.KeyEventHandler(opRPT_Type_KeyDown);
		this.opRPT_Type.ValueChanged += new System.EventHandler(opRPT_Type_ValueChanged_1);
		this.groupBox5.Controls.Add(this.opTemplate);
		this.groupBox5.Location = new System.Drawing.Point(444, 8);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(272, 72);
		this.groupBox5.TabIndex = 29;
		this.groupBox5.TabStop = false;
		this.groupBox5.Text = "樣板";
		this.opTemplate.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
		this.opTemplate.CheckedIndex = 0;
		this.opTemplate.ItemAppearance = appearance13;
		valueListItem12.DataValue = "Default Item";
		valueListItem12.DisplayText = "中文格式";
		valueListItem13.DataValue = "ValueListItem1";
		valueListItem13.DisplayText = "中英文格式";
		this.opTemplate.Items.Add(valueListItem12);
		this.opTemplate.Items.Add(valueListItem13);
		this.opTemplate.ItemSpacingVertical = 4;
		this.opTemplate.Location = new System.Drawing.Point(11, 21);
		this.opTemplate.Name = "opTemplate";
		this.opTemplate.Size = new System.Drawing.Size(120, 46);
		this.opTemplate.TabIndex = 28;
		this.opTemplate.Text = "中文格式";
		this.opTemplate.ValueChanged += new System.EventHandler(opRPT_Type_ValueChanged);
		this.groupBox3.Controls.Add(this.ultraTabControl2);
		this.groupBox3.Controls.Add(this.Pnl_Sort);
		this.groupBox3.Location = new System.Drawing.Point(8, 163);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(708, 173);
		this.groupBox3.TabIndex = 11;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "報表內容";
		appearance14.BorderColor = System.Drawing.Color.Black;
		this.ultraTabControl2.Appearance = appearance14;
		this.ultraTabControl2.Controls.Add(this.ultraTabSharedControlsPage3);
		this.ultraTabControl2.Controls.Add(this.Tab_Memo_1);
		this.ultraTabControl2.Controls.Add(this.Tab_Memo_2);
		this.ultraTabControl2.Location = new System.Drawing.Point(8, 16);
		this.ultraTabControl2.Name = "ultraTabControl2";
		this.ultraTabControl2.SharedControlsPage = this.ultraTabSharedControlsPage3;
		this.ultraTabControl2.Size = new System.Drawing.Size(332, 148);
		this.ultraTabControl2.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
		this.ultraTabControl2.TabIndex = 12;
		ultraTab1.TabPage = this.Tab_Memo_1;
		ultraTab1.Text = "tab1";
		ultraTab2.TabPage = this.Tab_Memo_2;
		ultraTab2.Text = "tab2";
		this.ultraTabControl2.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[2] { ultraTab1, ultraTab2 });
		this.ultraTabSharedControlsPage3.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage3.Name = "ultraTabSharedControlsPage3";
		this.ultraTabSharedControlsPage3.Size = new System.Drawing.Size(332, 148);
		this.Pnl_Sort.Controls.Add(this.CB_Ana_SkipSubTotalItem);
		this.Pnl_Sort.Controls.Add(this.CB_Ana_SkipCommentItem);
		this.Pnl_Sort.Controls.Add(this.CB_Ana_RepeatDetail);
		this.Pnl_Sort.Controls.Add(this.opSort);
		this.Pnl_Sort.Controls.Add(this.ultraLabel2);
		this.Pnl_Sort.Controls.Add(this.opRepeat);
		this.Pnl_Sort.Controls.Add(this.ultraLabel3);
		this.Pnl_Sort.Location = new System.Drawing.Point(336, 16);
		this.Pnl_Sort.Name = "Pnl_Sort";
		this.Pnl_Sort.Size = new System.Drawing.Size(364, 128);
		this.Pnl_Sort.TabIndex = 10;
		this.Pnl_Sort.Visible = false;
		this.CB_Ana_SkipSubTotalItem.Location = new System.Drawing.Point(239, 76);
		this.CB_Ana_SkipSubTotalItem.Name = "CB_Ana_SkipSubTotalItem";
		this.CB_Ana_SkipSubTotalItem.TabIndex = 20;
		this.CB_Ana_SkipSubTotalItem.Text = "小計項不編號";
		this.CB_Ana_SkipCommentItem.Location = new System.Drawing.Point(239, 55);
		this.CB_Ana_SkipCommentItem.Name = "CB_Ana_SkipCommentItem";
		this.CB_Ana_SkipCommentItem.TabIndex = 19;
		this.CB_Ana_SkipCommentItem.Text = "說明項不編號";
		this.CB_Ana_RepeatDetail.Location = new System.Drawing.Point(4, 102);
		this.CB_Ana_RepeatDetail.Name = "CB_Ana_RepeatDetail";
		this.CB_Ana_RepeatDetail.Size = new System.Drawing.Size(228, 20);
		this.CB_Ana_RepeatDetail.TabIndex = 18;
		this.CB_Ana_RepeatDetail.Text = "重複列印詳細表項目單價分析";
		this.opSort.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
		this.opSort.CheckedIndex = 0;
		this.opSort.ItemAppearance = appearance15;
		valueListItem14.DataValue = "Default Item";
		valueListItem14.DisplayText = "依項次代碼排序";
		valueListItem15.DataValue = "ValueListItem1";
		valueListItem15.DisplayText = "依工項代碼排序";
		this.opSort.Items.Add(valueListItem14);
		this.opSort.Items.Add(valueListItem15);
		this.opSort.ItemSpacingVertical = 1;
		this.opSort.Location = new System.Drawing.Point(80, 6);
		this.opSort.Name = "opSort";
		this.opSort.Size = new System.Drawing.Size(132, 50);
		this.opSort.TabIndex = 5;
		this.opSort.Text = "依項次代碼排序";
		this.opSort.ValueChanged += new System.EventHandler(opSort_ValueChanged);
		appearance16.TextHAlign = Infragistics.Win.HAlign.Right;
		this.ultraLabel2.Appearance = appearance16;
		this.ultraLabel2.Location = new System.Drawing.Point(-4, 9);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(84, 23);
		this.ultraLabel2.TabIndex = 7;
		this.ultraLabel2.Text = "排序方式:";
		this.opRepeat.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
		this.opRepeat.ItemAppearance = appearance17;
		valueListItem16.DataValue = "Default Item";
		valueListItem16.DisplayText = "重複列印";
		valueListItem17.DataValue = "ValueListItem1";
		valueListItem17.DisplayText = "不重複列印";
		this.opRepeat.Items.Add(valueListItem16);
		this.opRepeat.Items.Add(valueListItem17);
		this.opRepeat.ItemSpacingVertical = 1;
		this.opRepeat.Location = new System.Drawing.Point(80, 56);
		this.opRepeat.Name = "opRepeat";
		this.opRepeat.Size = new System.Drawing.Size(115, 48);
		this.opRepeat.TabIndex = 6;
		this.opRepeat.ValueChanged += new System.EventHandler(opRepeat_ValueChanged);
		appearance18.TextHAlign = Infragistics.Win.HAlign.Right;
		this.ultraLabel3.Appearance = appearance18;
		this.ultraLabel3.Location = new System.Drawing.Point(-4, 56);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(84, 23);
		this.ultraLabel3.TabIndex = 8;
		this.ultraLabel3.Text = "重複項目:";
		this.groupBox2.Controls.Add(this.SetEnd);
		this.groupBox2.Controls.Add(this.ultraLabel6);
		this.groupBox2.Controls.Add(this.ultraLabel5);
		this.groupBox2.Controls.Add(this.cmp_Ename);
		this.groupBox2.Controls.Add(this.ultraLabel4);
		this.groupBox2.Controls.Add(this.cmp_name);
		this.groupBox2.Location = new System.Drawing.Point(8, 340);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(708, 184);
		this.groupBox2.TabIndex = 10;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "表頭及表尾";
		appearance19.FontData.Name = "細明體";
		appearance19.FontData.SizeInPoints = 11f;
		this.SetEnd.Appearance = appearance19;
		this.SetEnd.Location = new System.Drawing.Point(12, 152);
		this.SetEnd.Name = "SetEnd";
		this.SetEnd.Size = new System.Drawing.Size(680, 24);
		this.SetEnd.TabIndex = 22;
		this.SetEnd.Text = "[SetEnd]";
		this.SetEnd.Validating += new System.ComponentModel.CancelEventHandler(SetEnd_Validating);
		this.ultraLabel6.Location = new System.Drawing.Point(12, 132);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(48, 23);
		this.ultraLabel6.TabIndex = 5;
		this.ultraLabel6.Text = "表尾:";
		this.ultraLabel5.Location = new System.Drawing.Point(8, 79);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(176, 14);
		this.ultraLabel5.TabIndex = 3;
		this.ultraLabel5.Text = "機關/公司英文名稱:";
		appearance20.FontData.Name = "細明體";
		appearance20.FontData.SizeInPoints = 11f;
		this.cmp_Ename.Appearance = appearance20;
		this.cmp_Ename.Location = new System.Drawing.Point(12, 99);
		this.cmp_Ename.Name = "cmp_Ename";
		this.cmp_Ename.Size = new System.Drawing.Size(680, 24);
		this.cmp_Ename.TabIndex = 2;
		this.cmp_Ename.Text = "[cmp_Ename]";
		this.cmp_Ename.Validating += new System.ComponentModel.CancelEventHandler(cmp_Ename_Validating);
		this.ultraLabel4.Location = new System.Drawing.Point(8, 24);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(180, 16);
		this.ultraLabel4.TabIndex = 1;
		this.ultraLabel4.Text = "機關/公司名稱:";
		appearance21.FontData.Name = "細明體";
		appearance21.FontData.SizeInPoints = 11f;
		this.cmp_name.Appearance = appearance21;
		this.cmp_name.Location = new System.Drawing.Point(12, 44);
		this.cmp_name.Name = "cmp_name";
		this.cmp_name.Size = new System.Drawing.Size(680, 24);
		this.cmp_name.TabIndex = 0;
		this.cmp_name.Text = "[cmp_name]";
		this.cmp_name.Validating += new System.ComponentModel.CancelEventHandler(cmp_Ename_Validating);
		this.panel1.Controls.Add(this.CB_ExtraParam);
		this.panel1.Controls.Add(this.A_Btn_Cncl);
		this.panel1.Controls.Add(this.A_Btn_Next);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 530);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(722, 42);
		this.panel1.TabIndex = 9;
		this.CB_ExtraParam.Location = new System.Drawing.Point(20, 12);
		this.CB_ExtraParam.Name = "CB_ExtraParam";
		this.CB_ExtraParam.TabIndex = 3;
		this.CB_ExtraParam.Text = "顯示路徑";
		this.CB_ExtraParam.Visible = false;
		this.A_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance22.Image = resources.GetObject("appearance22.Image");
		appearance22.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Cncl.Appearance = appearance22;
		this.A_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.A_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_Cncl.Location = new System.Drawing.Point(619, 7);
		this.A_Btn_Cncl.Name = "A_Btn_Cncl";
		this.A_Btn_Cncl.ShowFocusRect = false;
		this.A_Btn_Cncl.ShowOutline = false;
		this.A_Btn_Cncl.Size = new System.Drawing.Size(96, 31);
		this.A_Btn_Cncl.SupportThemes = false;
		this.A_Btn_Cncl.TabIndex = 2;
		this.A_Btn_Cncl.Text = "取消";
		this.A_Btn_Cncl.Click += new System.EventHandler(A_Btn_Cncl_Click);
		this.A_Btn_Next.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance23.Image = resources.GetObject("appearance23.Image");
		appearance23.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Next.Appearance = appearance23;
		this.A_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_Next.Location = new System.Drawing.Point(519, 7);
		this.A_Btn_Next.Name = "A_Btn_Next";
		this.A_Btn_Next.ShowFocusRect = false;
		this.A_Btn_Next.ShowOutline = false;
		this.A_Btn_Next.Size = new System.Drawing.Size(96, 31);
		this.A_Btn_Next.SupportThemes = false;
		this.A_Btn_Next.TabIndex = 1;
		this.A_Btn_Next.Text = "預覽報表";
		this.A_Btn_Next.Click += new System.EventHandler(A_Btn_Next_Click);
		this.Price.Checked = true;
		this.Price.CheckState = System.Windows.Forms.CheckState.Checked;
		this.Price.Location = new System.Drawing.Point(455, 108);
		this.Price.Name = "Price";
		this.Price.Size = new System.Drawing.Size(257, 20);
		this.Price.TabIndex = 16;
		this.Price.Text = "列印價格(不列印單價時則為標單)";
		this.Price.CheckedChanged += new System.EventHandler(ultraCheckEditor1_CheckedChanged);
		this.Tab_RPT2.Controls.Add(this.ultraLabel10);
		this.Tab_RPT2.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_RPT2.Name = "Tab_RPT2";
		this.Tab_RPT2.Size = new System.Drawing.Size(722, 572);
		this.ultraLabel10.Location = new System.Drawing.Point(132, 116);
		this.ultraLabel10.Name = "ultraLabel10";
		this.ultraLabel10.Size = new System.Drawing.Size(368, 23);
		this.ultraLabel10.TabIndex = 0;
		this.ultraLabel10.Text = "報表處理中，這個動作會花上數分鐘，請耐心等候";
		this.Tab_RPT3.Controls.Add(this.Tab_RPT);
		this.Tab_RPT3.Controls.Add(this.panel7);
		this.Tab_RPT3.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_RPT3.Name = "Tab_RPT3";
		this.Tab_RPT3.Size = new System.Drawing.Size(722, 572);
		appearance24.BackColor = System.Drawing.Color.FromArgb(153, 204, 255);
		appearance24.BackColor2 = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance24.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		this.Tab_RPT.ActiveTabAppearance = appearance24;
		appearance25.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance25.BackColor2 = System.Drawing.Color.FromArgb(102, 153, 255);
		appearance25.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		this.Tab_RPT.Appearance = appearance25;
		appearance26.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance26.BackColor2 = System.Drawing.Color.FromArgb(237, 243, 254);
		this.Tab_RPT.ClientAreaAppearance = appearance26;
		this.Tab_RPT.Controls.Add(this.ultraTabSharedControlsPage2);
		this.Tab_RPT.Controls.Add(this.Tab_0);
		this.Tab_RPT.Controls.Add(this.Tab_1);
		this.Tab_RPT.Controls.Add(this.Tab_2);
		this.Tab_RPT.Controls.Add(this.Tab_3);
		this.Tab_RPT.Controls.Add(this.Tab_4);
		this.Tab_RPT.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Tab_RPT.Location = new System.Drawing.Point(0, 0);
		this.Tab_RPT.Name = "Tab_RPT";
		this.Tab_RPT.SharedControlsPage = this.ultraTabSharedControlsPage2;
		this.Tab_RPT.Size = new System.Drawing.Size(722, 530);
		this.Tab_RPT.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
		this.Tab_RPT.TabIndex = 0;
		ultraTab3.TabPage = this.Tab_0;
		ultraTab3.Text = "專案基本資料";
		ultraTab4.TabPage = this.Tab_1;
		ultraTab4.Text = " 總表\u3000";
		ultraTab5.TabPage = this.Tab_2;
		ultraTab5.Text = " 詳細表 ";
		ultraTab6.TabPage = this.Tab_3;
		ultraTab6.Text = "單價分析表";
		ultraTab7.TabPage = this.Tab_4;
		ultraTab7.Text = "資源統計表";
		this.Tab_RPT.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[5] { ultraTab3, ultraTab4, ultraTab5, ultraTab6, ultraTab7 });
		this.ultraTabSharedControlsPage2.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage2.Name = "ultraTabSharedControlsPage2";
		this.ultraTabSharedControlsPage2.Size = new System.Drawing.Size(718, 503);
		this.panel7.Controls.Add(this.groupBox4);
		this.panel7.Controls.Add(this.ultraButton1);
		this.panel7.Controls.Add(this.ultraButton2);
		this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel7.Location = new System.Drawing.Point(0, 530);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(722, 42);
		this.panel7.TabIndex = 10;
		this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox4.Location = new System.Drawing.Point(0, 0);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(722, 8);
		this.groupBox4.TabIndex = 3;
		this.groupBox4.TabStop = false;
		this.ultraButton1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance27.Image = resources.GetObject("appearance27.Image");
		appearance27.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton1.Appearance = appearance27;
		this.ultraButton1.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ultraButton1.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton1.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton1.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton1.Location = new System.Drawing.Point(628, 9);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.ShowFocusRect = false;
		this.ultraButton1.ShowOutline = false;
		this.ultraButton1.Size = new System.Drawing.Size(88, 31);
		this.ultraButton1.SupportThemes = false;
		this.ultraButton1.TabIndex = 2;
		this.ultraButton1.Text = "取消";
		this.ultraButton1.Click += new System.EventHandler(ultraButton1_Click);
		this.ultraButton2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance28.Image = resources.GetObject("appearance28.Image");
		appearance28.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton2.Appearance = appearance28;
		this.ultraButton2.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton2.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton2.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton2.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton2.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton2.Location = new System.Drawing.Point(536, 9);
		this.ultraButton2.Name = "ultraButton2";
		this.ultraButton2.ShowFocusRect = false;
		this.ultraButton2.ShowOutline = false;
		this.ultraButton2.Size = new System.Drawing.Size(88, 31);
		this.ultraButton2.SupportThemes = false;
		this.ultraButton2.TabIndex = 1;
		this.ultraButton2.Text = "上一頁";
		this.ultraButton2.Click += new System.EventHandler(ultraButton2_Click);
		this.ultraTabControl1.Controls.Add(this.ultraTabSharedControlsPage1);
		this.ultraTabControl1.Controls.Add(this.Tab_RPT1);
		this.ultraTabControl1.Controls.Add(this.Tab_RPT3);
		this.ultraTabControl1.Controls.Add(this.Tab_RPT2);
		this.ultraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ultraTabControl1.Location = new System.Drawing.Point(0, 0);
		this.ultraTabControl1.Name = "ultraTabControl1";
		this.ultraTabControl1.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.ultraTabControl1.Size = new System.Drawing.Size(722, 572);
		this.ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
		this.ultraTabControl1.TabIndex = 0;
		ultraTab8.Key = "Tab_RPT1";
		ultraTab8.TabPage = this.Tab_RPT1;
		ultraTab8.Text = "條件設定頁";
		ultraTab9.Key = "Tab_RPT2";
		ultraTab9.TabPage = this.Tab_RPT2;
		ultraTab9.Text = "資料處理中";
		ultraTab10.Key = "Tab_RPT3";
		ultraTab10.TabPage = this.Tab_RPT3;
		ultraTab10.Text = "顯示結果";
		this.ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[3] { ultraTab8, ultraTab9, ultraTab10 });
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(722, 572);
		this.lblAtt.ForeColor = System.Drawing.Color.Red;
		this.lblAtt.Location = new System.Drawing.Point(460, 128);
		this.lblAtt.Name = "lblAtt";
		this.lblAtt.Size = new System.Drawing.Size(240, 16);
		this.lblAtt.TabIndex = 40;
		this.lblAtt.Text = "※\t注意：本報表不得用為招標標單";
		this.lblAtt.Visible = false;
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.CancelButton = this.A_Btn_Cncl;
		base.ClientSize = new System.Drawing.Size(722, 572);
		base.Controls.Add(this.ultraTabControl1);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormReportViewer";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "報表列印";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormReportViewer_KeyDown);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormReportViewer_FormClosing);
		base.Load += new System.EventHandler(FormReportViewer_Load);
		this.Tab_Memo_1.ResumeLayout(false);
		this.Pnl_Memo.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.pricemark).EndInit();
		this.Tab_Memo_2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.tb_Ana).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tb_Dei).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tb_Sum).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tb_Desc).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tb_Pic).EndInit();
		this.Tab_0.ResumeLayout(false);
		this.Tab_1.ResumeLayout(false);
		this.Tab_2.ResumeLayout(false);
		this.Tab_3.ResumeLayout(false);
		this.Tab_4.ResumeLayout(false);
		this.Tab_RPT1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.opRPT_Typebid).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPrintDate).EndInit();
		this.groupBox6.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.opRPT_Type2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.opRPT_Way).EndInit();
		((System.ComponentModel.ISupportInitialize)this.opRPT_Type1).EndInit();
		this.Pnl_PntLevel.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.aileael_DDL).EndInit();
		((System.ComponentModel.ISupportInitialize)this.opRPT_Type).EndInit();
		this.groupBox5.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.opTemplate).EndInit();
		this.groupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ultraTabControl2).EndInit();
		this.ultraTabControl2.ResumeLayout(false);
		this.Pnl_Sort.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.opSort).EndInit();
		((System.ComponentModel.ISupportInitialize)this.opRepeat).EndInit();
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.SetEnd).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cmp_Ename).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cmp_name).EndInit();
		this.panel1.ResumeLayout(false);
		this.Tab_RPT2.ResumeLayout(false);
		this.Tab_RPT3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Tab_RPT).EndInit();
		this.Tab_RPT.ResumeLayout(false);
		this.panel7.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ultraTabControl1).EndInit();
		this.ultraTabControl1.ResumeLayout(false);
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
