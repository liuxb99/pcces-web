using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.CTRClass;
using Archnowledge.Pcces.STDClass;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinSchedule;
using Infragistics.Win.UltraWinSchedule.CalendarCombo;

namespace Archnowledge.Pcces.PccesMain.Invoice;

public class FormSplitCnt_NewIssue : Form
{
	private const string CallFormHelp = "FormSplitCnt_NewIssue";

	private Panel panel1;

	private GroupBox groupBox1;

	private UltraButton A_Btn_Cncl;

	private Panel panel5;

	private UltraLabel ultraLabel6;

	private Panel panel2;

	private UltraLabel ultraLabel1;

	private UltraLabel lblIssue;

	private UltraLabel ultraLabel2;

	private UltraLabel ultraLabel3;

	private UltraLabel ultraLabel4;

	private UltraButton Btn_OK;

	private UltraNumericEditor txtThis_Prec;

	private UltraCalendarCombo dpEndDate;

	private UltraLabel ultraLabel5;

	private UltraCalendarCombo dpStartDate;

	private Container components = null;

	private string F_ProjectCode;

	private string F_SubProjetCode = "";

	private string F_UserID;

	private NumericUpDown nmProgress;

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

	public string _SubProjetCode
	{
		get
		{
			return F_SubProjetCode;
		}
		set
		{
			F_SubProjetCode = value;
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

	public FormSplitCnt_NewIssue()
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
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton1 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.Invoice.FormSplitCnt_NewIssue));
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton2 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		this.panel1 = new System.Windows.Forms.Panel();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.A_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.Btn_OK = new Infragistics.Win.Misc.UltraButton();
		this.panel5 = new System.Windows.Forms.Panel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.dpStartDate = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.dpEndDate = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.txtThis_Prec = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.lblIssue = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.nmProgress = new System.Windows.Forms.NumericUpDown();
		this.panel1.SuspendLayout();
		this.panel5.SuspendLayout();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dpStartDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dpEndDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtThis_Prec).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nmProgress).BeginInit();
		base.SuspendLayout();
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.Controls.Add(this.groupBox1);
		this.panel1.Controls.Add(this.A_Btn_Cncl);
		this.panel1.Controls.Add(this.Btn_OK);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 197);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(468, 44);
		this.panel1.TabIndex = 10;
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(468, 8);
		this.groupBox1.TabIndex = 3;
		this.groupBox1.TabStop = false;
		this.A_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Cncl.Appearance = appearance1;
		this.A_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Button;
		this.A_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.A_Btn_Cncl.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.A_Btn_Cncl.Location = new System.Drawing.Point(377, 10);
		this.A_Btn_Cncl.Name = "A_Btn_Cncl";
		this.A_Btn_Cncl.Size = new System.Drawing.Size(88, 28);
		this.A_Btn_Cncl.TabIndex = 2;
		this.A_Btn_Cncl.Text = "取消";
		this.Btn_OK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.Btn_OK.Appearance = appearance2;
		this.Btn_OK.BackColor = System.Drawing.SystemColors.Control;
		this.Btn_OK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Button;
		this.Btn_OK.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.Btn_OK.Location = new System.Drawing.Point(285, 10);
		this.Btn_OK.Name = "Btn_OK";
		this.Btn_OK.Size = new System.Drawing.Size(88, 28);
		this.Btn_OK.TabIndex = 1;
		this.Btn_OK.Text = "確定";
		this.Btn_OK.Click += new System.EventHandler(Btn_OK_Click);
		this.panel5.BackColor = System.Drawing.Color.White;
		this.panel5.Controls.Add(this.ultraLabel6);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel5.Location = new System.Drawing.Point(0, 0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(468, 40);
		this.panel5.TabIndex = 13;
		appearance3.BackColor = System.Drawing.Color.White;
		this.ultraLabel6.Appearance = appearance3;
		this.ultraLabel6.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel6.Location = new System.Drawing.Point(16, 12);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel6.TabIndex = 2;
		this.ultraLabel6.Text = "新增計價期別";
		this.panel2.Controls.Add(this.nmProgress);
		this.panel2.Controls.Add(this.dpStartDate);
		this.panel2.Controls.Add(this.ultraLabel5);
		this.panel2.Controls.Add(this.dpEndDate);
		this.panel2.Controls.Add(this.ultraLabel4);
		this.panel2.Controls.Add(this.txtThis_Prec);
		this.panel2.Controls.Add(this.ultraLabel3);
		this.panel2.Controls.Add(this.ultraLabel2);
		this.panel2.Controls.Add(this.lblIssue);
		this.panel2.Controls.Add(this.ultraLabel1);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.panel2.Location = new System.Drawing.Point(0, 40);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(468, 157);
		this.panel2.TabIndex = 14;
		dateButton1.Caption = "今天";
		this.dpStartDate.DateButtons.Add(dateButton1);
		this.dpStartDate.DayOfWeekCaptionStyle = Infragistics.Win.UltraWinSchedule.DayOfWeekCaptionStyle.ShortDescription;
		this.dpStartDate.Location = new System.Drawing.Point(104, 51);
		this.dpStartDate.Name = "dpStartDate";
		this.dpStartDate.NonAutoSizeHeight = 21;
		this.dpStartDate.Size = new System.Drawing.Size(152, 21);
		this.dpStartDate.TabIndex = 38;
		this.dpStartDate.TipStyle = Infragistics.Win.UltraWinSchedule.TipStyleDay.Holidays;
		this.dpStartDate.Value = resources.GetObject("dpStartDate.Value");
		appearance4.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel5.Appearance = appearance4;
		this.ultraLabel5.Location = new System.Drawing.Point(20, 50);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(76, 23);
		this.ultraLabel5.TabIndex = 37;
		this.ultraLabel5.Text = "起始日期:";
		dateButton2.Caption = "今天";
		this.dpEndDate.DateButtons.Add(dateButton2);
		this.dpEndDate.DayOfWeekCaptionStyle = Infragistics.Win.UltraWinSchedule.DayOfWeekCaptionStyle.ShortDescription;
		this.dpEndDate.Location = new System.Drawing.Point(104, 80);
		this.dpEndDate.Name = "dpEndDate";
		this.dpEndDate.NonAutoSizeHeight = 21;
		this.dpEndDate.Size = new System.Drawing.Size(152, 21);
		this.dpEndDate.TabIndex = 36;
		this.dpEndDate.TipStyle = Infragistics.Win.UltraWinSchedule.TipStyleDay.Holidays;
		this.dpEndDate.Value = resources.GetObject("dpEndDate.Value");
		appearance5.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel4.Appearance = appearance5;
		this.ultraLabel4.Location = new System.Drawing.Point(400, 112);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(24, 23);
		this.ultraLabel4.TabIndex = 7;
		this.ultraLabel4.Text = "%";
		this.txtThis_Prec.Location = new System.Drawing.Point(104, 109);
		this.txtThis_Prec.MaxValue = 100;
		this.txtThis_Prec.MinValue = -100;
		this.txtThis_Prec.Name = "txtThis_Prec";
		this.txtThis_Prec.PromptChar = ' ';
		this.txtThis_Prec.Size = new System.Drawing.Size(132, 24);
		this.txtThis_Prec.TabIndex = 6;
		appearance6.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel3.Appearance = appearance6;
		this.ultraLabel3.Location = new System.Drawing.Point(20, 111);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(76, 23);
		this.ultraLabel3.TabIndex = 4;
		this.ultraLabel3.Text = "本期進度:";
		appearance7.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel2.Appearance = appearance7;
		this.ultraLabel2.Location = new System.Drawing.Point(20, 80);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(76, 23);
		this.ultraLabel2.TabIndex = 2;
		this.ultraLabel2.Text = "結束日期:";
		appearance8.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblIssue.Appearance = appearance8;
		this.lblIssue.Location = new System.Drawing.Point(100, 20);
		this.lblIssue.Name = "lblIssue";
		this.lblIssue.Size = new System.Drawing.Size(120, 23);
		this.lblIssue.TabIndex = 1;
		this.lblIssue.Text = "[lblIssue]";
		appearance9.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel1.Appearance = appearance9;
		this.ultraLabel1.Location = new System.Drawing.Point(20, 20);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(76, 23);
		this.ultraLabel1.TabIndex = 0;
		this.ultraLabel1.Text = "估驗期數:";
		this.nmProgress.DecimalPlaces = 2;
		this.nmProgress.Font = new System.Drawing.Font("細明體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.nmProgress.Increment = new decimal(new int[4] { 5, 0, 0, 131072 });
		this.nmProgress.Location = new System.Drawing.Point(104, 108);
		this.nmProgress.Name = "nmProgress";
		this.nmProgress.Size = new System.Drawing.Size(288, 27);
		this.nmProgress.TabIndex = 39;
		this.nmProgress.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.CancelButton = this.A_Btn_Cncl;
		base.ClientSize = new System.Drawing.Size(468, 241);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel5);
		base.Controls.Add(this.panel1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormSplitCnt_NewIssue";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "新增期別";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormSplitCnt_NewIssue_KeyDown);
		base.Load += new System.EventHandler(FormSplitCnt_NewIssue_Load);
		this.panel1.ResumeLayout(false);
		this.panel5.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dpStartDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dpEndDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtThis_Prec).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nmProgress).EndInit();
		base.ResumeLayout(false);
	}

	private void FormSplitCnt_NewIssue_Load(object sender, EventArgs e)
	{
		LoadData();
	}

	private void LoadData()
	{
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(subacc_add) 新增估驗計價總檔");
		sub_acc acccom = new sub_acc(tmp_AL1);
		lblIssue.Text = acccom.Get_MaxQueue(F_SubProjetCode, F_ProjectCode);
		dpStartDate.Value = DateTime.Now;
		dpEndDate.Value = DateTime.Now;
		acccom = null;
	}

	private void Btn_OK_Click(object sender, EventArgs e)
	{
		if (PubTools.Str2DateTime(dpStartDate.Value) > PubTools.Str2DateTime(dpEndDate.Value))
		{
			MessageBox.Show(this, "啟始日應早於結束日", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		Cursor = Cursors.WaitCursor;
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(subacc_add) 新增估驗計價總檔");
		string ls_selectstr = "";
		ls_selectstr = ((F_ProjectCode.Trim().Length != 0) ? ("Select * from pubDecimal where projectCode='" + F_ProjectCode.Trim() + "'") : "Select * from pubDecimal where projectCode='mrsDefault'");
		ModifyDB StdCom1 = new ModifyDB(F_ProjectCode, tmp_AL1);
		DataTable ldt_mytable = StdCom1.DBList(ls_selectstr);
		StdCom1 = null;
		int li_QtyDec = 3;
		int li_CostDec = 2;
		int li_AmtDec = 0;
		if (ldt_mytable.Rows.Count > 0)
		{
			li_QtyDec = PubTools.Str2Int(ldt_mytable.Rows[0]["itemqty"].ToString());
			li_CostDec = PubTools.Str2Int(ldt_mytable.Rows[0]["itemcost"].ToString());
			li_AmtDec = PubTools.Str2Int(ldt_mytable.Rows[0]["itemamt"].ToString());
		}
		sub_acc acccom = new sub_acc(tmp_AL1);
		string ls_prjcode = F_ProjectCode;
		string ls_subproj = F_SubProjetCode;
		string ls_queue = acccom.Get_MaxQueue(ls_subproj, ls_prjcode);
		acccom.ps_prjcode = ls_prjcode;
		acccom.ps_subcode = ls_subproj;
		acccom.ps_queue = ls_queue;
		acccom.ps_date_rece = PubTools.ChgDateStr(dpStartDate.Text.Trim());
		acccom.ps_date_insp = PubTools.ChgDateStr(dpEndDate.Text.Trim());
		acccom.ps_this_prec = nmProgress.Value.ToString();
		acccom.InseItem();
		double ld_temp = Convert.ToDouble(nmProgress.Value);
		submfq mfqcom = new submfq(tmp_AL1);
		DataTable ldt_mfq = mfqcom.ListItem("", ls_queue.Trim(), ls_subproj.Trim(), ls_prjcode.Trim());
		foreach (DataRow dr in ldt_mfq.Rows)
		{
			double ld_itemqty = double.Parse(dr["itemqty"].ToString());
			double ld_itemcost = double.Parse(dr["itemcost"].ToString());
			double ld_tmoqty = PubTools.ARound(ld_temp * ld_itemqty / 100.0, li_QtyDec);
			mfqcom.ps_quantity = ld_tmoqty.ToString();
			dr["quantity"] = ld_tmoqty;
			double ld_tmoamt = PubTools.ARound(PubTools.ARound(ld_itemcost, li_CostDec) * PubTools.ARound(ld_tmoqty, li_QtyDec), li_AmtDec);
			if (dr["acc_prec"].ToString().Trim() == "100")
			{
				ld_tmoamt = 0.0;
			}
			mfqcom.ps_tom_amt = ld_tmoamt.ToString();
			dr["tom_amt"] = ld_tmoamt;
			mfqcom.ps_itemdes = dr["itemdes"].ToString();
			mfqcom.ps_itemno = dr["qucode"].ToString();
			mfqcom.ps_prjcode = dr["project"].ToString();
			mfqcom.ps_subcode = dr["sproj"].ToString();
			mfqcom.UpdItem();
			Application.DoEvents();
			Cursor = Cursors.WaitCursor;
		}
		mfqcom = null;
		ldt_mfq = acccom.ReTotal(ldt_mfq, ls_queue, ls_subproj, ls_prjcode);
		acccom = null;
		ldt_mfq = null;
		PubTools.WriteRoughlyLog(tmp_AL1);
		sub_acc AccCom = new sub_acc(tmp_AL1);
		AccCom.SetThisPrec(ls_queue, ls_subproj, ls_prjcode, PubTools.Str2Double(nmProgress.Value));
		AccCom = null;
		Cursor = Cursors.Default;
		base.DialogResult = DialogResult.OK;
	}

	private void FormSplitCnt_NewIssue_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormSplitCnt_NewIssue");
		}
	}
}
