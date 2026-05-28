using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.CTRClass;
using Archnowledge.Pcces.STDClass;
using C1.Win.C1Sizer;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinSchedule;
using Infragistics.Win.UltraWinSchedule.CalendarCombo;

namespace Archnowledge.Pcces.PccesMain.SplitContract;

public class FormSplitCnt_Basic : Form
{
	private const string CallFormHelp = "FormSplitCnt_Basic";

	private IContainer components;

	private ArrayList tmp_AL1;

	private sub_info SubInfoCom;

	private string ls_prjcode;

	private string ls_subproj;

	private bool lb_Lock;

	private double ld_Amount;

	private DataRow dr;

	private string F_UserID = "";

	private string F_ProjectCode = "";

	private string F_ProjectName = "";

	private string F_SubProjectCode = "";

	private PccesFormAction F_ActionName;

	private bool F_HasApproved;

	private Panel panel1;

	private GroupBox groupBox1;

	private UltraButton A_Btn_Cncl;

	private Panel panel5;

	private UltraLabel ultraLabel6;

	private Panel panel2;

	private UltraButton D_Btn_Fnsh;

	private C1Sizer c1Sizer1;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	private UltraLabel ultraLabel3;

	private UltraLabel ultraLabel4;

	private UltraLabel ultraLabel5;

	private UltraLabel ultraLabel7;

	private UltraLabel ultraLabel8;

	private UltraLabel ultraLabel9;

	private UltraLabel ultraLabel10;

	private UltraLabel ultraLabel11;

	private UltraLabel ultraLabel12;

	private UltraLabel ultraLabel13;

	private UltraLabel ultraLabel14;

	private UltraLabel ultraLabel15;

	private UltraLabel ultraLabel16;

	private UltraLabel ultraLabel17;

	private UltraLabel ultraLabel18;

	private UltraLabel ultraLabel19;

	private UltraLabel ultraLabel20;

	private UltraLabel ultraLabel21;

	private UltraLabel ultraLabel22;

	private UltraLabel ultraLabel23;

	private UltraLabel ultraLabel24;

	private UltraTextEditor tb_res;

	private UltraTextEditor tb_Main;

	private UltraTextEditor tb_Vendor;

	private UltraTextEditor tb_account;

	private UltraTextEditor tb_add;

	private UltraTextEditor tb_invno;

	private UltraTextEditor tb_BudYear;

	private UltraTextEditor tb_Work;

	private UltraTextEditor tb_1;

	private UltraTextEditor tb_aldv;

	private UltraTextEditor tb_resmemo;

	private UltraLabel lb_cName;

	private UltraLabel lb_ProjectCode;

	private UltraCalendarCombo ad_budstart;

	private UltraCalendarCombo ad_actstart;

	private UltraCalendarCombo ad_UpdDate;

	private UltraCalendarCombo ad_budend;

	private UltraCalendarCombo ad_actend;

	private UltraComboEditor dll_AccMode;

	private GroupBox groupBox2;

	private Label label1;

	private UltraLabel ultraLabel25;

	private UltraLabel ultraLabel26;

	private RadioButton raDate;

	private RadioButton raRate;

	private NumericUpDown ndp_StartRate;

	private NumericUpDown ndp_EndRate;

	private System.Windows.Forms.ToolTip toolTip1;

	private UltraCalendarCombo ad_AdvStart;

	private UltraLabel ultraLabel27;

	private UltraTextEditor tb_amount;

	private UltraTextEditor tb_adv;

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

	public string _ProjectName
	{
		get
		{
			return F_ProjectName;
		}
		set
		{
			F_ProjectName = value;
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

	public bool _HasApproved
	{
		get
		{
			return F_HasApproved;
		}
		set
		{
			F_HasApproved = value;
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

	public FormSplitCnt_Basic()
	{
		InitializeComponent();
	}

	private void ultraLabel20_Click(object sender, EventArgs e)
	{
	}

	private void D_Btn_Fnsh_Click(object sender, EventArgs e)
	{
		string ls_selectstr = "select * from SubItemA where projectcode='" + lb_ProjectCode.Text.Trim() + "'";
		if (raRate.Checked && ndp_StartRate.Value >= ndp_EndRate.Value)
		{
			MessageBox.Show(this, "扣回預付款比率設定有誤，請重新設定!!", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		SubInfoCom.ps_ProjectCode = lb_ProjectCode.Text;
		SubInfoCom.ps_Sproj = ls_subproj;
		SubInfoCom.ps_InvoCode = tb_invno.Text;
		SubInfoCom.ps_ProjectNameC = lb_cName.Text;
		SubInfoCom.ps_ProjectAddress = tb_add.Text;
		SubInfoCom.ps_ProjAmt = tb_amount.Text;
		SubInfoCom.ps_AccountNo = tb_account.Text;
		if (ad_actend.Value.ToString() == "")
		{
			SubInfoCom.ps_ActEnd = null;
		}
		else
		{
			SubInfoCom.ps_ActEnd = ad_actend.Value.ToString();
		}
		if (ad_actstart.Value.ToString() == "")
		{
			SubInfoCom.ps_ActStart = null;
		}
		else
		{
			SubInfoCom.ps_ActStart = ad_actstart.Value.ToString();
		}
		if (ad_budend.Value.ToString() == "")
		{
			SubInfoCom.ps_BudEnd = null;
		}
		else
		{
			SubInfoCom.ps_BudEnd = ad_budend.Value.ToString();
		}
		if (ad_budstart.Value.ToString() == "")
		{
			SubInfoCom.ps_BudStart = null;
		}
		else
		{
			SubInfoCom.ps_BudStart = ad_budstart.Value.ToString();
		}
		if (ad_UpdDate.Value.ToString() == "")
		{
			SubInfoCom.ps_UpdDT = null;
		}
		else
		{
			SubInfoCom.ps_UpdDT = ad_UpdDate.Value.ToString();
		}
		SubInfoCom.ps_BudYear = tb_BudYear.Text;
		SubInfoCom.ps_MainName = tb_Main.Text;
		SubInfoCom.ps_owner = tb_Vendor.Text;
		SubInfoCom.ps_ProjADV = tb_adv.Text;
		SubInfoCom.ps_ProjResMemo = tb_resmemo.Text;
		SubInfoCom.ps_ProjResRate = tb_res.Text;
		SubInfoCom.ps_WorkMode = tb_Work.Text;
		SubInfoCom.ps_WorkUnit = tb_1.Text;
		SubInfoCom.ps_ProjALDV = tb_aldv.Text;
		SubInfoCom.ps_AccMode = dll_AccMode.SelectedIndex.ToString();
		SubInfoCom.ps_ALDV_Way = (raDate.Checked ? "1" : "2");
		if (ad_AdvStart.Value.ToString() == "")
		{
			SubInfoCom.ps_ALDV_StartDate = null;
		}
		else
		{
			SubInfoCom.ps_ALDV_StartDate = ad_AdvStart.Value.ToString();
		}
		SubInfoCom.ps_ALDV_EndDate = null;
		SubInfoCom.ps_ALDV_StartRate = ndp_StartRate.Value.ToString();
		SubInfoCom.ps_ALDV_EndRate = ndp_EndRate.Value.ToString();
		SubInfoCom.UpdItem();
		string sSQL = "select * from SubInfo where projectcode='" + lb_ProjectCode.Text.Trim() + "' and Sproj='" + ls_subproj.Trim() + "' ";
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("WIN FORM--契約編輯計價數量");
		ModifyDB StdCom = new ModifyDB("", aArr);
		DataTable SubInfoDB = StdCom.DBList(sSQL);
		if (SubInfoDB.Rows.Count > 0 && (SubInfoDB.Rows[0]["flag"] == null || SubInfoDB.Rows[0]["flag"].ToString().Trim() == ""))
		{
			DataTable subDB = StdCom.DBList(ls_selectstr);
			if (subDB.Rows.Count > 0)
			{
				ls_selectstr = "Update SubItemA set AccMode = '" + dll_AccMode.SelectedIndex + "'where projectcode='" + lb_ProjectCode.Text.Trim() + "'";
				StdCom.DBUpd(ls_selectstr);
				ls_selectstr = "Update SubInfo set flag = 'T' where  projectcode='" + lb_ProjectCode.Text.Trim() + "' and Sproj='" + ls_subproj.Trim() + "' ";
				StdCom.DBUpd(ls_selectstr);
			}
		}
		StdCom = null;
		base.DialogResult = DialogResult.OK;
		Close();
	}

	private void FormSplitCnt_Basic_Load(object sender, EventArgs e)
	{
		tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(SubInfo) 契約書內容維護");
		ls_prjcode = F_ProjectCode;
		ls_subproj = F_SubProjectCode;
		sub_acc AccCom = new sub_acc(tmp_AL1);
		lb_Lock = AccCom.GetLockMode("9999", ls_subproj, ls_prjcode);
		AccCom = null;
		SubInfoCom = new sub_info(tmp_AL1);
		PubDecimal setcom = new PubDecimal(tmp_AL1);
		ArrayList alDec = setcom.Get_SetDec(ls_prjcode.Trim());
		sub_Ctr ctrcom = new sub_Ctr(tmp_AL1);
		DataTable ldt_sub = ctrcom.ListItem("", ls_subproj, ls_prjcode, alDec);
		DataTable ldt_Info = SubInfoCom.ListItem(ls_subproj, ls_prjcode);
		if (ldt_Info.Rows.Count == 0)
		{
			PubProject ProjectCom = new PubProject(tmp_AL1);
			DataTable ldt_Proj = ProjectCom.ListItem(" a.projectcode='" + ls_prjcode.Trim() + "' ");
			ProjectCom = null;
			if (ldt_sub.Rows.Count == 0)
			{
				ld_Amount = 0.0;
			}
			else
			{
				DataTable ldt_Ctr = ldt_sub;
				sub_Ctr SubCtrCom = new sub_Ctr(tmp_AL1);
				ld_Amount = SubCtrCom.GetAmount(ldt_Ctr);
				SubCtrCom = null;
			}
			SubInfoCom.ps_ProjectCode = ls_prjcode;
			SubInfoCom.ps_Sproj = ls_subproj;
			SubInfoCom.ps_ProjectNameC = ldt_Proj.Rows[0]["projCName"].ToString();
			SubInfoCom.ps_ProjectAddress = ldt_Proj.Rows[0]["projAddress"].ToString();
			SubInfoCom.ps_ProjAmt = ld_Amount.ToString();
			SubInfoCom.ps_MainName = ldt_Proj.Rows[0]["mainCName"].ToString();
			SubInfoCom.InseItem();
			ldt_Info = SubInfoCom.ListItem(ls_subproj, ls_prjcode);
		}
		dr = ldt_Info.Rows[0];
		if (F_HasApproved)
		{
			c1Sizer1.Enabled = false;
			D_Btn_Fnsh.Enabled = false;
		}
		BindData();
		if (F_ActionName == PccesFormAction.Invoice)
		{
			D_Btn_Fnsh.Enabled = false;
		}
	}

	private void BindData()
	{
		lb_ProjectCode.Text = dr["ProjectCode"].ToString();
		tb_invno.Text = dr["InvoCode"].ToString();
		lb_cName.Text = dr["ProjectNameC"].ToString();
		tb_add.Text = dr["ProjectAddress"].ToString();
		tb_BudYear.Text = dr["BudYear"].ToString();
		tb_Work.Text = dr["WorkMode"].ToString();
		tb_1.Text = dr["WorkUnit"].ToString();
		tb_Main.Text = dr["MainName"].ToString();
		tb_Vendor.Text = dr["owner"].ToString();
		tb_account.Text = dr["AccountNo"].ToString();
		tb_resmemo.Text = dr["ProjResMemo"].ToString();
		tb_res.Text = dr["ProjResRate"].ToString();
		tb_adv.Text = string.Format("{0:N2}", PubTools.Str2Decimal(dr["ProjADV"]));
		tb_amount.Text = string.Format("{0:N2}", PubTools.Str2Decimal(dr["ProjAmt"]));
		tb_aldv.Text = dr["ProjALDV"].ToString();
		string sALDV_Way = dr["ALDV_Way"].ToString();
		if (sALDV_Way.Trim() == "2")
		{
			raRate.Checked = true;
		}
		else
		{
			raDate.Checked = true;
		}
		raDate_CheckedChanged(this, EventArgs.Empty);
		raRate_CheckedChanged(this, EventArgs.Empty);
		ndp_StartRate.Value = PubTools.Str2Decimal(dr["ALDV_StartRate"]);
		ndp_EndRate.Value = PubTools.Str2Decimal(dr["ALDV_EndRate"]);
		ad_actend.Value = PubTools.Str2DateTime(dr["ActEnd"].ToString());
		ad_actstart.Value = PubTools.Str2DateTime(dr["ActStart"].ToString());
		ad_budend.Value = PubTools.Str2DateTime(dr["BudEnd"].ToString());
		ad_budstart.Value = PubTools.Str2DateTime(dr["BudStart"].ToString());
		ad_UpdDate.Value = PubTools.Str2DateTime(dr["UpdDT"].ToString());
		ad_AdvStart.Value = PubTools.Str2DateTime(dr["ALDV_StartDate"].ToString());
		if (ad_actend.Text == "1800/1/1")
		{
			ad_actend.Text = "";
		}
		if (ad_actstart.Text == "1800/1/1")
		{
			ad_actstart.Text = "";
		}
		if (ad_budend.Text == "1800/1/1")
		{
			ad_budend.Text = "";
		}
		if (ad_budstart.Text == "1800/1/1")
		{
			ad_budstart.Text = "";
		}
		if (ad_UpdDate.Text == "1800/1/1")
		{
			ad_UpdDate.Text = "";
		}
		if (ad_AdvStart.Text == "1800/1/1")
		{
			ad_AdvStart.Text = "";
		}
		dll_AccMode.SelectedIndex = PubTools.Str2Int(dr["AccMode"]);
		tb_add.Enabled = !lb_Lock;
		tb_BudYear.Enabled = !lb_Lock;
		tb_Work.Enabled = !lb_Lock;
		tb_1.Enabled = !lb_Lock;
		tb_Main.Enabled = !lb_Lock;
		tb_Vendor.Enabled = !lb_Lock;
		tb_account.Enabled = !lb_Lock;
		tb_resmemo.Enabled = !lb_Lock;
		tb_res.Enabled = !lb_Lock;
		tb_adv.Enabled = !lb_Lock;
		tb_amount.Enabled = !lb_Lock;
		tb_aldv.Enabled = !lb_Lock;
		ad_actend.Enabled = !lb_Lock;
		ad_actstart.Enabled = !lb_Lock;
		ad_budend.Enabled = !lb_Lock;
		ad_budstart.Enabled = !lb_Lock;
		ad_UpdDate.Enabled = !lb_Lock;
	}

	private void raDate_CheckedChanged(object sender, EventArgs e)
	{
		if (raDate.Checked)
		{
			ad_AdvStart.Enabled = true;
		}
		else
		{
			ad_AdvStart.Enabled = false;
		}
	}

	private void raRate_CheckedChanged(object sender, EventArgs e)
	{
		if (raRate.Checked)
		{
			ndp_StartRate.Enabled = true;
			ndp_EndRate.Enabled = true;
		}
		else
		{
			ndp_StartRate.Enabled = false;
			ndp_EndRate.Enabled = false;
		}
	}

	private void FormSplitCnt_Basic_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormSplitCnt_Basic");
		}
	}

	private void dll_AccMode_Click(object sender, EventArgs e)
	{
	}

	private void dll_AccMode_AfterDropDown(object sender, EventArgs e)
	{
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("WIN FORM--契約編輯計價數量");
		ModifyDB StdCom = new ModifyDB("", aArr);
		string ls_selectstr = "Update SubInfo set flag = '' where  projectcode='" + lb_ProjectCode.Text.Trim() + "' and Sproj='" + ls_subproj.Trim() + "' ";
		StdCom.DBUpd(ls_selectstr);
		StdCom = null;
		aArr = null;
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.SplitContract.FormSplitCnt_Basic));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton1 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
		Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton2 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
		Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton3 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
		Infragistics.Win.Appearance appearance38 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton4 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
		Infragistics.Win.Appearance appearance39 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton5 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
		Infragistics.Win.Appearance appearance40 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton6 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
		Infragistics.Win.Appearance appearance41 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance42 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance43 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance44 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance45 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance46 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance47 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance48 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance49 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance50 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance51 = new Infragistics.Win.Appearance();
		this.panel1 = new System.Windows.Forms.Panel();
		this.D_Btn_Fnsh = new Infragistics.Win.Misc.UltraButton();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.A_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel27 = new Infragistics.Win.Misc.UltraLabel();
		this.panel5 = new System.Windows.Forms.Panel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.c1Sizer1 = new C1.Win.C1Sizer.C1Sizer();
		this.tb_adv = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.tb_amount = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.ultraLabel26 = new Infragistics.Win.Misc.UltraLabel();
		this.ndp_EndRate = new System.Windows.Forms.NumericUpDown();
		this.ultraLabel25 = new Infragistics.Win.Misc.UltraLabel();
		this.ndp_StartRate = new System.Windows.Forms.NumericUpDown();
		this.raRate = new System.Windows.Forms.RadioButton();
		this.ad_AdvStart = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
		this.raDate = new System.Windows.Forms.RadioButton();
		this.label1 = new System.Windows.Forms.Label();
		this.ultraLabel20 = new Infragistics.Win.Misc.UltraLabel();
		this.tb_aldv = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel24 = new Infragistics.Win.Misc.UltraLabel();
		this.lb_ProjectCode = new Infragistics.Win.Misc.UltraLabel();
		this.lb_cName = new Infragistics.Win.Misc.UltraLabel();
		this.dll_AccMode = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.tb_res = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ad_budstart = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel8 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel9 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel10 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel11 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel12 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel13 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel14 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel15 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel16 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel17 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel18 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel19 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel21 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel22 = new Infragistics.Win.Misc.UltraLabel();
		this.ad_actstart = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
		this.ad_UpdDate = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
		this.ad_budend = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
		this.ad_actend = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
		this.ultraLabel23 = new Infragistics.Win.Misc.UltraLabel();
		this.tb_Main = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.tb_Vendor = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.tb_account = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.tb_add = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.tb_invno = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.tb_BudYear = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.tb_Work = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.tb_1 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.tb_resmemo = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.panel1.SuspendLayout();
		this.panel5.SuspendLayout();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.c1Sizer1).BeginInit();
		this.c1Sizer1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.tb_adv).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tb_amount).BeginInit();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ndp_EndRate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ndp_StartRate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ad_AdvStart).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tb_aldv).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dll_AccMode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tb_res).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ad_budstart).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ad_actstart).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ad_UpdDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ad_budend).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ad_actend).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tb_Main).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tb_Vendor).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tb_account).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tb_add).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tb_invno).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tb_BudYear).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tb_Work).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tb_1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tb_resmemo).BeginInit();
		base.SuspendLayout();
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.Controls.Add(this.D_Btn_Fnsh);
		this.panel1.Controls.Add(this.groupBox1);
		this.panel1.Controls.Add(this.A_Btn_Cncl);
		this.panel1.Controls.Add(this.ultraLabel27);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 458);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(782, 44);
		this.panel1.TabIndex = 10;
		this.D_Btn_Fnsh.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Fnsh.Appearance = appearance1;
		this.D_Btn_Fnsh.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Fnsh.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.D_Btn_Fnsh.Font = new System.Drawing.Font("細明體", 11f);
		this.D_Btn_Fnsh.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Fnsh.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Fnsh.Location = new System.Drawing.Point(591, 10);
		this.D_Btn_Fnsh.Name = "D_Btn_Fnsh";
		this.D_Btn_Fnsh.ShowFocusRect = false;
		this.D_Btn_Fnsh.ShowOutline = false;
		this.D_Btn_Fnsh.Size = new System.Drawing.Size(88, 31);
		this.D_Btn_Fnsh.SupportThemes = false;
		this.D_Btn_Fnsh.TabIndex = 4;
		this.D_Btn_Fnsh.Text = "確定";
		this.D_Btn_Fnsh.Click += new System.EventHandler(D_Btn_Fnsh_Click);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(782, 8);
		this.groupBox1.TabIndex = 3;
		this.groupBox1.TabStop = false;
		this.A_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Cncl.Appearance = appearance2;
		this.A_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.A_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_Cncl.Location = new System.Drawing.Point(682, 10);
		this.A_Btn_Cncl.Name = "A_Btn_Cncl";
		this.A_Btn_Cncl.ShowFocusRect = false;
		this.A_Btn_Cncl.ShowOutline = false;
		this.A_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.A_Btn_Cncl.SupportThemes = false;
		this.A_Btn_Cncl.TabIndex = 2;
		this.A_Btn_Cncl.Text = "取消";
		appearance3.ForeColor = System.Drawing.Color.Red;
		appearance3.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance3.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel27.Appearance = appearance3;
		this.ultraLabel27.Location = new System.Drawing.Point(8, 16);
		this.ultraLabel27.Name = "ultraLabel27";
		this.ultraLabel27.Size = new System.Drawing.Size(167, 24);
		this.ultraLabel27.TabIndex = 43;
		this.ultraLabel27.Text = "注意：\"*\" 是必填欄位";
		this.panel5.BackColor = System.Drawing.Color.White;
		this.panel5.Controls.Add(this.ultraLabel6);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel5.Location = new System.Drawing.Point(0, 0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(782, 32);
		this.panel5.TabIndex = 14;
		appearance4.BackColor = System.Drawing.Color.White;
		this.ultraLabel6.Appearance = appearance4;
		this.ultraLabel6.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel6.Location = new System.Drawing.Point(14, 8);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel6.TabIndex = 2;
		this.ultraLabel6.Text = "契約基本資料編輯";
		this.panel2.Controls.Add(this.c1Sizer1);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Location = new System.Drawing.Point(0, 32);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(782, 426);
		this.panel2.TabIndex = 15;
		this.c1Sizer1.AllowDrop = true;
		this.c1Sizer1.Controls.Add(this.tb_adv);
		this.c1Sizer1.Controls.Add(this.tb_amount);
		this.c1Sizer1.Controls.Add(this.groupBox2);
		this.c1Sizer1.Controls.Add(this.lb_ProjectCode);
		this.c1Sizer1.Controls.Add(this.lb_cName);
		this.c1Sizer1.Controls.Add(this.dll_AccMode);
		this.c1Sizer1.Controls.Add(this.tb_res);
		this.c1Sizer1.Controls.Add(this.ad_budstart);
		this.c1Sizer1.Controls.Add(this.ultraLabel1);
		this.c1Sizer1.Controls.Add(this.ultraLabel2);
		this.c1Sizer1.Controls.Add(this.ultraLabel3);
		this.c1Sizer1.Controls.Add(this.ultraLabel4);
		this.c1Sizer1.Controls.Add(this.ultraLabel7);
		this.c1Sizer1.Controls.Add(this.ultraLabel8);
		this.c1Sizer1.Controls.Add(this.ultraLabel9);
		this.c1Sizer1.Controls.Add(this.ultraLabel10);
		this.c1Sizer1.Controls.Add(this.ultraLabel11);
		this.c1Sizer1.Controls.Add(this.ultraLabel12);
		this.c1Sizer1.Controls.Add(this.ultraLabel13);
		this.c1Sizer1.Controls.Add(this.ultraLabel14);
		this.c1Sizer1.Controls.Add(this.ultraLabel15);
		this.c1Sizer1.Controls.Add(this.ultraLabel16);
		this.c1Sizer1.Controls.Add(this.ultraLabel17);
		this.c1Sizer1.Controls.Add(this.ultraLabel18);
		this.c1Sizer1.Controls.Add(this.ultraLabel19);
		this.c1Sizer1.Controls.Add(this.ultraLabel21);
		this.c1Sizer1.Controls.Add(this.ultraLabel22);
		this.c1Sizer1.Controls.Add(this.ad_actstart);
		this.c1Sizer1.Controls.Add(this.ad_UpdDate);
		this.c1Sizer1.Controls.Add(this.ad_budend);
		this.c1Sizer1.Controls.Add(this.ad_actend);
		this.c1Sizer1.Controls.Add(this.ultraLabel23);
		this.c1Sizer1.Controls.Add(this.tb_Main);
		this.c1Sizer1.Controls.Add(this.tb_Vendor);
		this.c1Sizer1.Controls.Add(this.tb_account);
		this.c1Sizer1.Controls.Add(this.tb_add);
		this.c1Sizer1.Controls.Add(this.tb_invno);
		this.c1Sizer1.Controls.Add(this.tb_BudYear);
		this.c1Sizer1.Controls.Add(this.tb_Work);
		this.c1Sizer1.Controls.Add(this.tb_1);
		this.c1Sizer1.Controls.Add(this.tb_resmemo);
		this.c1Sizer1.Controls.Add(this.ultraLabel5);
		this.c1Sizer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.c1Sizer1.GridDefinition = resources.GetString("c1Sizer1.GridDefinition");
		this.c1Sizer1.Location = new System.Drawing.Point(0, 0);
		this.c1Sizer1.Name = "c1Sizer1";
		this.c1Sizer1.Size = new System.Drawing.Size(782, 426);
		this.c1Sizer1.TabIndex = 0;
		this.c1Sizer1.TabStop = false;
		appearance5.FontData.Name = "細明體";
		appearance5.FontData.SizeInPoints = 11f;
		this.tb_adv.Appearance = appearance5;
		this.tb_adv.AutoSize = true;
		this.tb_adv.Location = new System.Drawing.Point(189, 228);
		this.tb_adv.Name = "tb_adv";
		this.tb_adv.Size = new System.Drawing.Size(167, 24);
		this.tb_adv.TabIndex = 43;
		appearance6.FontData.Name = "細明體";
		appearance6.FontData.SizeInPoints = 11f;
		this.tb_amount.Appearance = appearance6;
		this.tb_amount.AutoSize = true;
		this.tb_amount.Location = new System.Drawing.Point(189, 201);
		this.tb_amount.Name = "tb_amount";
		this.tb_amount.Size = new System.Drawing.Size(167, 24);
		this.tb_amount.TabIndex = 42;
		this.groupBox2.Controls.Add(this.ultraLabel26);
		this.groupBox2.Controls.Add(this.ndp_EndRate);
		this.groupBox2.Controls.Add(this.ultraLabel25);
		this.groupBox2.Controls.Add(this.ndp_StartRate);
		this.groupBox2.Controls.Add(this.raRate);
		this.groupBox2.Controls.Add(this.ad_AdvStart);
		this.groupBox2.Controls.Add(this.raDate);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Controls.Add(this.ultraLabel20);
		this.groupBox2.Controls.Add(this.tb_aldv);
		this.groupBox2.Controls.Add(this.ultraLabel24);
		this.groupBox2.Location = new System.Drawing.Point(189, 257);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(551, 109);
		this.groupBox2.TabIndex = 41;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "預付款扣回方式";
		appearance7.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance7.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel26.Appearance = appearance7;
		this.ultraLabel26.Location = new System.Drawing.Point(358, 74);
		this.ultraLabel26.Name = "ultraLabel26";
		this.ultraLabel26.Size = new System.Drawing.Size(20, 25);
		this.ultraLabel26.TabIndex = 46;
		this.ultraLabel26.Text = "%";
		this.ndp_EndRate.Location = new System.Drawing.Point(312, 75);
		this.ndp_EndRate.Name = "ndp_EndRate";
		this.ndp_EndRate.Size = new System.Drawing.Size(45, 25);
		this.ndp_EndRate.TabIndex = 45;
		this.ndp_EndRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.toolTip1.SetToolTip(this.ndp_EndRate, "計價進度達該比率後，停止扣回預付款");
		this.ndp_EndRate.Value = new decimal(new int[4] { 80, 0, 0, 0 });
		appearance8.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance8.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel25.Appearance = appearance8;
		this.ultraLabel25.Location = new System.Drawing.Point(272, 74);
		this.ultraLabel25.Name = "ultraLabel25";
		this.ultraLabel25.Size = new System.Drawing.Size(40, 25);
		this.ultraLabel25.TabIndex = 44;
		this.ultraLabel25.Text = "% ～";
		this.ndp_StartRate.Location = new System.Drawing.Point(227, 75);
		this.ndp_StartRate.Name = "ndp_StartRate";
		this.ndp_StartRate.Size = new System.Drawing.Size(45, 25);
		this.ndp_StartRate.TabIndex = 43;
		this.ndp_StartRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.toolTip1.SetToolTip(this.ndp_StartRate, "計價進度達該比率後，開始扣回預付款");
		this.ndp_StartRate.Value = new decimal(new int[4] { 20, 0, 0, 0 });
		this.raRate.Location = new System.Drawing.Point(131, 77);
		this.raRate.Name = "raRate";
		this.raRate.Size = new System.Drawing.Size(104, 24);
		this.raRate.TabIndex = 42;
		this.raRate.Text = "起扣比率";
		this.raRate.CheckedChanged += new System.EventHandler(raRate_CheckedChanged);
		appearance9.FontData.Name = "細明體";
		appearance9.FontData.SizeInPoints = 11f;
		this.ad_AdvStart.Appearance = appearance9;
		dateButton1.Caption = "今天";
		this.ad_AdvStart.DateButtons.Add(dateButton1);
		this.ad_AdvStart.DayOfWeekCaptionStyle = Infragistics.Win.UltraWinSchedule.DayOfWeekCaptionStyle.ShortDescription;
		this.ad_AdvStart.Location = new System.Drawing.Point(227, 48);
		this.ad_AdvStart.Name = "ad_AdvStart";
		this.ad_AdvStart.NonAutoSizeHeight = 21;
		this.ad_AdvStart.NullDateLabel = "";
		this.ad_AdvStart.Size = new System.Drawing.Size(144, 21);
		this.ad_AdvStart.TabIndex = 40;
		this.ad_AdvStart.TipStyle = Infragistics.Win.UltraWinSchedule.TipStyleDay.Holidays;
		this.ad_AdvStart.Value = resources.GetObject("ad_AdvStart.Value");
		this.ad_AdvStart.WeekNumbersVisible = true;
		this.raDate.Checked = true;
		this.raDate.Location = new System.Drawing.Point(131, 50);
		this.raDate.Name = "raDate";
		this.raDate.Size = new System.Drawing.Size(104, 24);
		this.raDate.TabIndex = 38;
		this.raDate.TabStop = true;
		this.raDate.Text = "起扣日期";
		this.raDate.CheckedChanged += new System.EventHandler(raDate_CheckedChanged);
		this.label1.Location = new System.Drawing.Point(43, 52);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(96, 16);
		this.label1.TabIndex = 41;
		this.label1.Text = "扣回預付款";
		appearance10.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance10.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel20.Appearance = appearance10;
		this.ultraLabel20.Location = new System.Drawing.Point(17, 22);
		this.ultraLabel20.Name = "ultraLabel20";
		this.ultraLabel20.Size = new System.Drawing.Size(185, 25);
		this.ultraLabel20.TabIndex = 0;
		this.ultraLabel20.Text = "每期扣回預付款比率：";
		this.ultraLabel20.Click += new System.EventHandler(ultraLabel20_Click);
		appearance11.FontData.Name = "細明體";
		appearance11.FontData.SizeInPoints = 11f;
		appearance11.TextHAlign = Infragistics.Win.HAlign.Right;
		this.tb_aldv.Appearance = appearance11;
		this.tb_aldv.AutoSize = true;
		this.tb_aldv.Location = new System.Drawing.Point(204, 19);
		this.tb_aldv.Name = "tb_aldv";
		this.tb_aldv.Size = new System.Drawing.Size(167, 24);
		this.tb_aldv.TabIndex = 37;
		this.tb_aldv.Text = "[tb_aldv]";
		appearance12.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance12.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel24.Appearance = appearance12;
		this.ultraLabel24.Location = new System.Drawing.Point(377, 20);
		this.ultraLabel24.Name = "ultraLabel24";
		this.ultraLabel24.Size = new System.Drawing.Size(20, 25);
		this.ultraLabel24.TabIndex = 0;
		this.ultraLabel24.Text = "%";
		appearance13.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lb_ProjectCode.Appearance = appearance13;
		this.lb_ProjectCode.Location = new System.Drawing.Point(573, 4);
		this.lb_ProjectCode.Name = "lb_ProjectCode";
		this.lb_ProjectCode.Size = new System.Drawing.Size(167, 24);
		this.lb_ProjectCode.TabIndex = 40;
		this.lb_ProjectCode.Text = "[lb_ProjectCode]";
		appearance14.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lb_cName.Appearance = appearance14;
		this.lb_cName.Location = new System.Drawing.Point(189, 32);
		this.lb_cName.Name = "lb_cName";
		this.lb_cName.Size = new System.Drawing.Size(551, 25);
		this.lb_cName.TabIndex = 39;
		this.lb_cName.Text = "[lb_cName]";
		appearance15.FontData.Name = "細明體";
		appearance15.FontData.SizeInPoints = 11f;
		this.dll_AccMode.Appearance = appearance15;
		this.dll_AccMode.AutoSize = true;
		valueListItem1.DataValue = "警告但可存檔";
		valueListItem1.DisplayText = "警告但可存檔";
		valueListItem2.DataValue = "警告且不可存檔";
		valueListItem2.DisplayText = "警告且不可存檔";
		valueListItem3.DataValue = "略過";
		valueListItem3.DisplayText = "略過";
		this.dll_AccMode.Items.Add(valueListItem1);
		this.dll_AccMode.Items.Add(valueListItem2);
		this.dll_AccMode.Items.Add(valueListItem3);
		this.dll_AccMode.Location = new System.Drawing.Point(573, 398);
		this.dll_AccMode.Name = "dll_AccMode";
		this.dll_AccMode.Size = new System.Drawing.Size(167, 24);
		this.dll_AccMode.TabIndex = 38;
		this.dll_AccMode.Text = "ultraComboEditor1";
		this.dll_AccMode.AfterDropDown += new System.EventHandler(dll_AccMode_AfterDropDown);
		this.dll_AccMode.Click += new System.EventHandler(dll_AccMode_Click);
		appearance16.FontData.Name = "細明體";
		appearance16.FontData.SizeInPoints = 11f;
		appearance16.TextHAlign = Infragistics.Win.HAlign.Right;
		this.tb_res.Appearance = appearance16;
		this.tb_res.AutoSize = true;
		this.tb_res.Location = new System.Drawing.Point(189, 370);
		this.tb_res.Name = "tb_res";
		this.tb_res.Size = new System.Drawing.Size(167, 24);
		this.tb_res.TabIndex = 37;
		this.tb_res.Text = "[tb_res]";
		appearance17.FontData.Name = "細明體";
		appearance17.FontData.SizeInPoints = 11f;
		this.ad_budstart.Appearance = appearance17;
		dateButton2.Caption = "今天";
		this.ad_budstart.DateButtons.Add(dateButton2);
		this.ad_budstart.DayOfWeekCaptionStyle = Infragistics.Win.UltraWinSchedule.DayOfWeekCaptionStyle.ShortDescription;
		this.ad_budstart.Location = new System.Drawing.Point(189, 147);
		this.ad_budstart.Name = "ad_budstart";
		this.ad_budstart.NonAutoSizeHeight = 21;
		this.ad_budstart.NullDateLabel = "";
		this.ad_budstart.Size = new System.Drawing.Size(167, 21);
		this.ad_budstart.TabIndex = 36;
		this.ad_budstart.TipStyle = Infragistics.Win.UltraWinSchedule.TipStyleDay.Holidays;
		this.ad_budstart.Value = resources.GetObject("ad_budstart.Value");
		this.ad_budstart.WeekNumbersVisible = true;
		appearance18.ForeColor = System.Drawing.Color.Red;
		appearance18.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance18.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel1.Appearance = appearance18;
		this.ultraLabel1.Location = new System.Drawing.Point(18, 4);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(167, 24);
		this.ultraLabel1.TabIndex = 0;
		this.ultraLabel1.Text = "*契約編號：";
		appearance19.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance19.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel2.Appearance = appearance19;
		this.ultraLabel2.Location = new System.Drawing.Point(18, 32);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(167, 25);
		this.ultraLabel2.TabIndex = 0;
		this.ultraLabel2.Text = "契約名稱：";
		appearance20.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance20.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel3.Appearance = appearance20;
		this.ultraLabel3.Location = new System.Drawing.Point(18, 61);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(167, 24);
		this.ultraLabel3.TabIndex = 0;
		this.ultraLabel3.Text = "施工地點：";
		appearance21.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance21.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel4.Appearance = appearance21;
		this.ultraLabel4.Location = new System.Drawing.Point(384, 201);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(185, 23);
		this.ultraLabel4.TabIndex = 0;
		this.ultraLabel4.Text = "會計科目：";
		appearance22.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance22.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel7.Appearance = appearance22;
		this.ultraLabel7.Location = new System.Drawing.Point(18, 117);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(167, 26);
		this.ultraLabel7.TabIndex = 0;
		this.ultraLabel7.Text = "主辦單位：";
		appearance23.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance23.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel8.Appearance = appearance23;
		this.ultraLabel8.Location = new System.Drawing.Point(18, 147);
		this.ultraLabel8.Name = "ultraLabel8";
		this.ultraLabel8.Size = new System.Drawing.Size(167, 23);
		this.ultraLabel8.TabIndex = 0;
		this.ultraLabel8.Text = "預定開工日：";
		appearance24.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance24.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel9.Appearance = appearance24;
		this.ultraLabel9.Location = new System.Drawing.Point(18, 174);
		this.ultraLabel9.Name = "ultraLabel9";
		this.ultraLabel9.Size = new System.Drawing.Size(167, 23);
		this.ultraLabel9.TabIndex = 0;
		this.ultraLabel9.Text = "實際開工日：";
		appearance25.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance25.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel10.Appearance = appearance25;
		this.ultraLabel10.Location = new System.Drawing.Point(18, 201);
		this.ultraLabel10.Name = "ultraLabel10";
		this.ultraLabel10.Size = new System.Drawing.Size(167, 23);
		this.ultraLabel10.TabIndex = 0;
		this.ultraLabel10.Text = "契約總價：";
		appearance26.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance26.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel11.Appearance = appearance26;
		this.ultraLabel11.Location = new System.Drawing.Point(18, 228);
		this.ultraLabel11.Name = "ultraLabel11";
		this.ultraLabel11.Size = new System.Drawing.Size(167, 25);
		this.ultraLabel11.TabIndex = 0;
		this.ultraLabel11.Text = "預付款：";
		appearance27.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance27.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel12.Appearance = appearance27;
		this.ultraLabel12.Location = new System.Drawing.Point(18, 370);
		this.ultraLabel12.Name = "ultraLabel12";
		this.ultraLabel12.Size = new System.Drawing.Size(167, 24);
		this.ultraLabel12.TabIndex = 0;
		this.ultraLabel12.Text = "每期估驗保留比率：";
		appearance28.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance28.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel13.Appearance = appearance28;
		this.ultraLabel13.Location = new System.Drawing.Point(18, 398);
		this.ultraLabel13.Name = "ultraLabel13";
		this.ultraLabel13.Size = new System.Drawing.Size(167, 24);
		this.ultraLabel13.TabIndex = 0;
		this.ultraLabel13.Text = "資料更新日期：";
		appearance29.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance29.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel14.Appearance = appearance29;
		this.ultraLabel14.Location = new System.Drawing.Point(384, 4);
		this.ultraLabel14.Name = "ultraLabel14";
		this.ultraLabel14.Size = new System.Drawing.Size(185, 24);
		this.ultraLabel14.TabIndex = 0;
		this.ultraLabel14.Text = "工程編號：";
		appearance30.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance30.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel15.Appearance = appearance30;
		this.ultraLabel15.Location = new System.Drawing.Point(384, 228);
		this.ultraLabel15.Name = "ultraLabel15";
		this.ultraLabel15.Size = new System.Drawing.Size(185, 25);
		this.ultraLabel15.TabIndex = 0;
		this.ultraLabel15.Text = "預算年度：";
		appearance31.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance31.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel16.Appearance = appearance31;
		this.ultraLabel16.Location = new System.Drawing.Point(384, 89);
		this.ultraLabel16.Name = "ultraLabel16";
		this.ultraLabel16.Size = new System.Drawing.Size(185, 24);
		this.ultraLabel16.TabIndex = 0;
		this.ultraLabel16.Text = "施工方式：";
		appearance32.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance32.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel17.Appearance = appearance32;
		this.ultraLabel17.Location = new System.Drawing.Point(384, 117);
		this.ultraLabel17.Name = "ultraLabel17";
		this.ultraLabel17.Size = new System.Drawing.Size(185, 26);
		this.ultraLabel17.TabIndex = 0;
		this.ultraLabel17.Text = "監造單位：";
		appearance33.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance33.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel18.Appearance = appearance33;
		this.ultraLabel18.Location = new System.Drawing.Point(384, 147);
		this.ultraLabel18.Name = "ultraLabel18";
		this.ultraLabel18.Size = new System.Drawing.Size(185, 23);
		this.ultraLabel18.TabIndex = 0;
		this.ultraLabel18.Text = "預定完工日：";
		appearance34.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance34.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel19.Appearance = appearance34;
		this.ultraLabel19.Location = new System.Drawing.Point(384, 174);
		this.ultraLabel19.Name = "ultraLabel19";
		this.ultraLabel19.Size = new System.Drawing.Size(185, 23);
		this.ultraLabel19.TabIndex = 0;
		this.ultraLabel19.Text = "實際完工日：";
		appearance35.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance35.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel21.Appearance = appearance35;
		this.ultraLabel21.Location = new System.Drawing.Point(384, 370);
		this.ultraLabel21.Name = "ultraLabel21";
		this.ultraLabel21.Size = new System.Drawing.Size(185, 24);
		this.ultraLabel21.TabIndex = 0;
		this.ultraLabel21.Text = "保留款說明：";
		appearance36.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance36.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel22.Appearance = appearance36;
		this.ultraLabel22.Location = new System.Drawing.Point(384, 398);
		this.ultraLabel22.Name = "ultraLabel22";
		this.ultraLabel22.Size = new System.Drawing.Size(185, 24);
		this.ultraLabel22.TabIndex = 0;
		this.ultraLabel22.Text = "計價數量超過設計數量：";
		appearance37.FontData.Name = "細明體";
		appearance37.FontData.SizeInPoints = 11f;
		this.ad_actstart.Appearance = appearance37;
		dateButton3.Caption = "今天";
		this.ad_actstart.DateButtons.Add(dateButton3);
		this.ad_actstart.DayOfWeekCaptionStyle = Infragistics.Win.UltraWinSchedule.DayOfWeekCaptionStyle.ShortDescription;
		this.ad_actstart.Location = new System.Drawing.Point(189, 174);
		this.ad_actstart.Name = "ad_actstart";
		this.ad_actstart.NonAutoSizeHeight = 21;
		this.ad_actstart.NullDateLabel = "";
		this.ad_actstart.Size = new System.Drawing.Size(167, 21);
		this.ad_actstart.TabIndex = 36;
		this.ad_actstart.TipStyle = Infragistics.Win.UltraWinSchedule.TipStyleDay.Holidays;
		this.ad_actstart.Value = resources.GetObject("ad_actstart.Value");
		this.ad_actstart.WeekNumbersVisible = true;
		appearance38.FontData.Name = "細明體";
		appearance38.FontData.SizeInPoints = 11f;
		this.ad_UpdDate.Appearance = appearance38;
		dateButton4.Caption = "今天";
		this.ad_UpdDate.DateButtons.Add(dateButton4);
		this.ad_UpdDate.DayOfWeekCaptionStyle = Infragistics.Win.UltraWinSchedule.DayOfWeekCaptionStyle.ShortDescription;
		this.ad_UpdDate.Location = new System.Drawing.Point(189, 398);
		this.ad_UpdDate.Name = "ad_UpdDate";
		this.ad_UpdDate.NonAutoSizeHeight = 21;
		this.ad_UpdDate.NullDateLabel = "";
		this.ad_UpdDate.Size = new System.Drawing.Size(167, 21);
		this.ad_UpdDate.TabIndex = 36;
		this.ad_UpdDate.TipStyle = Infragistics.Win.UltraWinSchedule.TipStyleDay.Holidays;
		this.ad_UpdDate.Value = resources.GetObject("ad_UpdDate.Value");
		this.ad_UpdDate.WeekNumbersVisible = true;
		appearance39.FontData.Name = "細明體";
		appearance39.FontData.SizeInPoints = 11f;
		this.ad_budend.Appearance = appearance39;
		dateButton5.Caption = "今天";
		this.ad_budend.DateButtons.Add(dateButton5);
		this.ad_budend.DayOfWeekCaptionStyle = Infragistics.Win.UltraWinSchedule.DayOfWeekCaptionStyle.ShortDescription;
		this.ad_budend.Location = new System.Drawing.Point(573, 147);
		this.ad_budend.Name = "ad_budend";
		this.ad_budend.NonAutoSizeHeight = 21;
		this.ad_budend.NullDateLabel = "";
		this.ad_budend.Size = new System.Drawing.Size(167, 21);
		this.ad_budend.TabIndex = 36;
		this.ad_budend.TipStyle = Infragistics.Win.UltraWinSchedule.TipStyleDay.Holidays;
		this.ad_budend.Value = resources.GetObject("ad_budend.Value");
		this.ad_budend.WeekNumbersVisible = true;
		appearance40.FontData.Name = "細明體";
		appearance40.FontData.SizeInPoints = 11f;
		this.ad_actend.Appearance = appearance40;
		dateButton6.Caption = "今天";
		this.ad_actend.DateButtons.Add(dateButton6);
		this.ad_actend.DayOfWeekCaptionStyle = Infragistics.Win.UltraWinSchedule.DayOfWeekCaptionStyle.ShortDescription;
		this.ad_actend.Location = new System.Drawing.Point(573, 174);
		this.ad_actend.Name = "ad_actend";
		this.ad_actend.NonAutoSizeHeight = 21;
		this.ad_actend.NullDateLabel = "";
		this.ad_actend.Size = new System.Drawing.Size(167, 21);
		this.ad_actend.TabIndex = 36;
		this.ad_actend.TipStyle = Infragistics.Win.UltraWinSchedule.TipStyleDay.Holidays;
		this.ad_actend.Value = resources.GetObject("ad_actend.Value");
		this.ad_actend.WeekNumbersVisible = true;
		appearance41.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance41.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel23.Appearance = appearance41;
		this.ultraLabel23.Location = new System.Drawing.Point(360, 370);
		this.ultraLabel23.Name = "ultraLabel23";
		this.ultraLabel23.Size = new System.Drawing.Size(20, 24);
		this.ultraLabel23.TabIndex = 0;
		this.ultraLabel23.Text = "%";
		appearance42.FontData.Name = "細明體";
		appearance42.FontData.SizeInPoints = 11f;
		this.tb_Main.Appearance = appearance42;
		this.tb_Main.AutoSize = true;
		this.tb_Main.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.tb_Main.Location = new System.Drawing.Point(189, 117);
		this.tb_Main.Name = "tb_Main";
		this.tb_Main.Size = new System.Drawing.Size(167, 24);
		this.tb_Main.TabIndex = 37;
		this.tb_Main.Text = "[tb_Main]";
		appearance43.FontData.Name = "細明體";
		appearance43.FontData.SizeInPoints = 11f;
		this.tb_Vendor.Appearance = appearance43;
		this.tb_Vendor.AutoSize = true;
		this.tb_Vendor.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.tb_Vendor.Location = new System.Drawing.Point(189, 89);
		this.tb_Vendor.Name = "tb_Vendor";
		this.tb_Vendor.Size = new System.Drawing.Size(167, 24);
		this.tb_Vendor.TabIndex = 37;
		this.tb_Vendor.Text = "[tb_Vendor]";
		appearance44.FontData.Name = "細明體";
		appearance44.FontData.SizeInPoints = 11f;
		this.tb_account.Appearance = appearance44;
		this.tb_account.AutoSize = true;
		this.tb_account.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.tb_account.Location = new System.Drawing.Point(573, 201);
		this.tb_account.Name = "tb_account";
		this.tb_account.Size = new System.Drawing.Size(167, 24);
		this.tb_account.TabIndex = 37;
		this.tb_account.Text = "[tb_account]";
		appearance45.FontData.Name = "細明體";
		appearance45.FontData.SizeInPoints = 11f;
		this.tb_add.Appearance = appearance45;
		this.tb_add.AutoSize = true;
		this.tb_add.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.tb_add.Location = new System.Drawing.Point(189, 61);
		this.tb_add.Name = "tb_add";
		this.tb_add.Size = new System.Drawing.Size(551, 24);
		this.tb_add.TabIndex = 37;
		this.tb_add.Text = "[tb_add]";
		appearance46.FontData.Name = "細明體";
		appearance46.FontData.SizeInPoints = 11f;
		this.tb_invno.Appearance = appearance46;
		this.tb_invno.AutoSize = true;
		this.tb_invno.Location = new System.Drawing.Point(189, 4);
		this.tb_invno.Name = "tb_invno";
		this.tb_invno.Size = new System.Drawing.Size(167, 24);
		this.tb_invno.TabIndex = 37;
		this.tb_invno.Text = "[tb_invno]";
		this.tb_invno.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		appearance47.FontData.Name = "細明體";
		appearance47.FontData.SizeInPoints = 11f;
		this.tb_BudYear.Appearance = appearance47;
		this.tb_BudYear.AutoSize = true;
		this.tb_BudYear.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.tb_BudYear.Location = new System.Drawing.Point(573, 228);
		this.tb_BudYear.Name = "tb_BudYear";
		this.tb_BudYear.Size = new System.Drawing.Size(167, 24);
		this.tb_BudYear.TabIndex = 37;
		this.tb_BudYear.Text = "[tb_BudYear]";
		appearance48.FontData.Name = "細明體";
		appearance48.FontData.SizeInPoints = 11f;
		this.tb_Work.Appearance = appearance48;
		this.tb_Work.AutoSize = true;
		this.tb_Work.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.tb_Work.Location = new System.Drawing.Point(573, 89);
		this.tb_Work.Name = "tb_Work";
		this.tb_Work.Size = new System.Drawing.Size(167, 24);
		this.tb_Work.TabIndex = 37;
		this.tb_Work.Text = "[tb_Work]";
		appearance49.FontData.Name = "細明體";
		appearance49.FontData.SizeInPoints = 11f;
		this.tb_1.Appearance = appearance49;
		this.tb_1.AutoSize = true;
		this.tb_1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.tb_1.Location = new System.Drawing.Point(573, 117);
		this.tb_1.Name = "tb_1";
		this.tb_1.Size = new System.Drawing.Size(167, 24);
		this.tb_1.TabIndex = 37;
		this.tb_1.Text = "[tb_1]";
		appearance50.FontData.Name = "細明體";
		appearance50.FontData.SizeInPoints = 11f;
		this.tb_resmemo.Appearance = appearance50;
		this.tb_resmemo.AutoSize = true;
		this.tb_resmemo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.tb_resmemo.Location = new System.Drawing.Point(573, 370);
		this.tb_resmemo.Name = "tb_resmemo";
		this.tb_resmemo.Size = new System.Drawing.Size(167, 24);
		this.tb_resmemo.TabIndex = 37;
		this.tb_resmemo.Text = "[tb_resmemo]";
		appearance51.ForeColor = System.Drawing.Color.Red;
		appearance51.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance51.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel5.Appearance = appearance51;
		this.ultraLabel5.Location = new System.Drawing.Point(18, 89);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(167, 24);
		this.ultraLabel5.TabIndex = 0;
		this.ultraLabel5.Text = "*承包廠商：";
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.CancelButton = this.A_Btn_Cncl;
		base.ClientSize = new System.Drawing.Size(782, 502);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel5);
		base.Controls.Add(this.panel1);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormSplitCnt_Basic";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "契約基本資料";
		base.Load += new System.EventHandler(FormSplitCnt_Basic_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormSplitCnt_Basic_KeyDown);
		this.panel1.ResumeLayout(false);
		this.panel5.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.c1Sizer1).EndInit();
		this.c1Sizer1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.tb_adv).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tb_amount).EndInit();
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ndp_EndRate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ndp_StartRate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ad_AdvStart).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tb_aldv).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dll_AccMode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tb_res).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ad_budstart).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ad_actstart).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ad_UpdDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ad_budend).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ad_actend).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tb_Main).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tb_Vendor).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tb_account).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tb_add).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tb_invno).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tb_BudYear).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tb_Work).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tb_1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tb_resmemo).EndInit();
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
