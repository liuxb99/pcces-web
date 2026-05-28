using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.CTRClass;
using Archnowledge.Pcces.STDClass;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinSchedule;
using Infragistics.Win.UltraWinSchedule.CalendarCombo;

namespace Archnowledge.Pcces.PccesMain.Invoice;

public class FormInvoiceProgress : Form
{
	private const string CallFormHelp = "FormInvoiceProgress";

	private Panel panel16;

	private GroupBox groupBox6;

	private UltraButton D_Btn_Cncl;

	private UltraButton D_Btn_Next;

	private Panel panel1;

	private UltraLabel ultraLabel1;

	private Container components = null;

	private string F_UserID;

	private string F_ProjectCode;

	private string F_SubProjectCode;

	private string F_Issue;

	private decimal F_Progress;

	private DateTime F_StartDate;

	private DateTime F_EndDate;

	private UltraLabel ultraLabel4;

	private UltraCalendarCombo dpStartDate;

	private UltraLabel ultraLabel5;

	private UltraCalendarCombo dpEndDate;

	private UltraLabel ultraLabel2;

	private UltraLabel lblIssue;

	private UltraLabel ultraLabel3;

	private NumericUpDown nmProgress;

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

	public decimal _Progress
	{
		get
		{
			return F_Progress;
		}
		set
		{
			F_Progress = value;
			nmProgress.Value = F_Progress;
		}
	}

	public DateTime _StartDate
	{
		get
		{
			return F_StartDate;
		}
		set
		{
			F_StartDate = value;
			dpStartDate.Value = F_StartDate;
		}
	}

	public DateTime _EndDate
	{
		get
		{
			return F_EndDate;
		}
		set
		{
			F_EndDate = value;
			dpEndDate.Value = F_EndDate;
		}
	}

	public FormInvoiceProgress()
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
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.Invoice.FormInvoiceProgress));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton1 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton2 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		this.panel16 = new System.Windows.Forms.Panel();
		this.groupBox6 = new System.Windows.Forms.GroupBox();
		this.D_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.D_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.panel1 = new System.Windows.Forms.Panel();
		this.dpStartDate = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.dpEndDate = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.lblIssue = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.nmProgress = new System.Windows.Forms.NumericUpDown();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.panel16.SuspendLayout();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dpStartDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dpEndDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nmProgress).BeginInit();
		base.SuspendLayout();
		this.panel16.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel16.Controls.Add(this.groupBox6);
		this.panel16.Controls.Add(this.D_Btn_Cncl);
		this.panel16.Controls.Add(this.D_Btn_Next);
		this.panel16.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel16.Location = new System.Drawing.Point(0, 180);
		this.panel16.Name = "panel16";
		this.panel16.Size = new System.Drawing.Size(352, 44);
		this.panel16.TabIndex = 20;
		this.groupBox6.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox6.Location = new System.Drawing.Point(0, 0);
		this.groupBox6.Name = "groupBox6";
		this.groupBox6.Size = new System.Drawing.Size(352, 8);
		this.groupBox6.TabIndex = 4;
		this.groupBox6.TabStop = false;
		this.D_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Cncl.Appearance = appearance1;
		this.D_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.D_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.D_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.D_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Cncl.Location = new System.Drawing.Point(184, 9);
		this.D_Btn_Cncl.Name = "D_Btn_Cncl";
		this.D_Btn_Cncl.ShowFocusRect = false;
		this.D_Btn_Cncl.ShowOutline = false;
		this.D_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.D_Btn_Cncl.SupportThemes = false;
		this.D_Btn_Cncl.TabIndex = 2;
		this.D_Btn_Cncl.Text = "取消";
		this.D_Btn_Next.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Next.Appearance = appearance2;
		this.D_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.D_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.D_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Next.Location = new System.Drawing.Point(90, 9);
		this.D_Btn_Next.Name = "D_Btn_Next";
		this.D_Btn_Next.ShowFocusRect = false;
		this.D_Btn_Next.ShowOutline = false;
		this.D_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.D_Btn_Next.SupportThemes = false;
		this.D_Btn_Next.TabIndex = 1;
		this.D_Btn_Next.Text = "確定";
		this.D_Btn_Next.Click += new System.EventHandler(D_Btn_Next_Click);
		this.panel1.BackColor = System.Drawing.Color.White;
		this.panel1.Controls.Add(this.dpStartDate);
		this.panel1.Controls.Add(this.ultraLabel5);
		this.panel1.Controls.Add(this.dpEndDate);
		this.panel1.Controls.Add(this.ultraLabel2);
		this.panel1.Controls.Add(this.lblIssue);
		this.panel1.Controls.Add(this.ultraLabel3);
		this.panel1.Controls.Add(this.ultraLabel4);
		this.panel1.Controls.Add(this.nmProgress);
		this.panel1.Controls.Add(this.ultraLabel1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(352, 180);
		this.panel1.TabIndex = 21;
		this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(panel1_Paint);
		dateButton1.Caption = "今天";
		this.dpStartDate.DateButtons.Add(dateButton1);
		this.dpStartDate.DayOfWeekCaptionStyle = Infragistics.Win.UltraWinSchedule.DayOfWeekCaptionStyle.ShortDescription;
		this.dpStartDate.Location = new System.Drawing.Point(104, 40);
		this.dpStartDate.Name = "dpStartDate";
		this.dpStartDate.NonAutoSizeHeight = 21;
		this.dpStartDate.Size = new System.Drawing.Size(152, 21);
		this.dpStartDate.TabIndex = 44;
		this.dpStartDate.TipStyle = Infragistics.Win.UltraWinSchedule.TipStyleDay.Holidays;
		this.dpStartDate.Value = resources.GetObject("dpStartDate.Value");
		appearance3.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel5.Appearance = appearance3;
		this.ultraLabel5.Location = new System.Drawing.Point(16, 40);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(76, 23);
		this.ultraLabel5.TabIndex = 43;
		this.ultraLabel5.Text = "起始日期:";
		dateButton2.Caption = "今天";
		this.dpEndDate.DateButtons.Add(dateButton2);
		this.dpEndDate.DayOfWeekCaptionStyle = Infragistics.Win.UltraWinSchedule.DayOfWeekCaptionStyle.ShortDescription;
		this.dpEndDate.Location = new System.Drawing.Point(104, 72);
		this.dpEndDate.Name = "dpEndDate";
		this.dpEndDate.NonAutoSizeHeight = 21;
		this.dpEndDate.Size = new System.Drawing.Size(152, 21);
		this.dpEndDate.TabIndex = 42;
		this.dpEndDate.TipStyle = Infragistics.Win.UltraWinSchedule.TipStyleDay.Holidays;
		this.dpEndDate.Value = resources.GetObject("dpEndDate.Value");
		appearance4.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel2.Appearance = appearance4;
		this.ultraLabel2.Location = new System.Drawing.Point(16, 72);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(76, 23);
		this.ultraLabel2.TabIndex = 41;
		this.ultraLabel2.Text = "結束日期:";
		appearance5.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblIssue.Appearance = appearance5;
		this.lblIssue.Location = new System.Drawing.Point(96, 13);
		this.lblIssue.Name = "lblIssue";
		this.lblIssue.Size = new System.Drawing.Size(120, 23);
		this.lblIssue.TabIndex = 40;
		this.lblIssue.Text = "[lblIssue]";
		appearance6.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel3.Appearance = appearance6;
		this.ultraLabel3.Location = new System.Drawing.Point(16, 13);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(76, 23);
		this.ultraLabel3.TabIndex = 39;
		this.ultraLabel3.Text = "估驗期數:";
		appearance7.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel4.Appearance = appearance7;
		this.ultraLabel4.Location = new System.Drawing.Point(320, 138);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(24, 23);
		this.ultraLabel4.TabIndex = 8;
		this.ultraLabel4.Text = "%";
		this.nmProgress.DecimalPlaces = 2;
		this.nmProgress.Font = new System.Drawing.Font("細明體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.nmProgress.Increment = new decimal(new int[4] { 5, 0, 0, 131072 });
		this.nmProgress.Location = new System.Drawing.Point(24, 136);
		this.nmProgress.Name = "nmProgress";
		this.nmProgress.Size = new System.Drawing.Size(288, 27);
		this.nmProgress.TabIndex = 1;
		this.nmProgress.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.nmProgress.Value = new decimal(new int[4] { 10, 0, 0, 0 });
		this.ultraLabel1.Location = new System.Drawing.Point(16, 104);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(256, 23);
		this.ultraLabel1.TabIndex = 0;
		this.ultraLabel1.Text = "請輸入本期進度";
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		base.CancelButton = this.D_Btn_Cncl;
		base.ClientSize = new System.Drawing.Size(352, 224);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.panel16);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.KeyPreview = true;
		base.Name = "FormInvoiceProgress";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "本期進度";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormInvoiceProgress_KeyDown);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormInvoiceProgress_FormClosing);
		base.Load += new System.EventHandler(FormInvoiceProgress_Load);
		this.panel16.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dpStartDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dpEndDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nmProgress).EndInit();
		base.ResumeLayout(false);
	}

	private void D_Btn_Next_Click(object sender, EventArgs e)
	{
		if (PubTools.Str2DateTime(dpStartDate.Value) > PubTools.Str2DateTime(dpEndDate.Value))
		{
			MessageBox.Show(this, "啟始日應早於結束日", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("設訂第 " + F_Issue + " 期進度");
		sub_acc AccCom = new sub_acc(tmp_AL1);
		string ls_prjcode = F_ProjectCode;
		string ls_subproj = F_SubProjectCode;
		string ls_queue = AccCom.Get_MaxQueue(ls_subproj, ls_prjcode);
		AccCom.ps_prjcode = ls_prjcode;
		AccCom.ps_subcode = ls_subproj;
		AccCom.ps_queue = ls_queue;
		AccCom.ps_date_rece = PubTools.ChgDateStr(dpStartDate.Text.Trim());
		AccCom.ps_date_insp = PubTools.ChgDateStr(dpEndDate.Text.Trim());
		AccCom.UpdItem();
		AccCom.SetThisPrec(F_Issue, F_SubProjectCode, F_ProjectCode, PubTools.Str2Double(nmProgress.Value.ToString()));
		AccCom = null;
		PubTools.WriteRoughlyLog(tmp_AL1);
		base.DialogResult = DialogResult.OK;
	}

	private void FormInvoiceProgress_Load(object sender, EventArgs e)
	{
		lblIssue.Text = F_Issue;
		LoadingScreen();
	}

	private void LoadingScreen()
	{
		string Status = CommonMethods.GetIniValue("InvoiceProgress", "WindowState");
		if (Status == "Maximized")
		{
			base.WindowState = FormWindowState.Maximized;
		}
		int iLoc_X = PubTools.Str2Int(CommonMethods.GetIniValue("InvoiceProgress", "PK_LocationX"));
		int iLoc_Y = PubTools.Str2Int(CommonMethods.GetIniValue("InvoiceProgress", "PK_LocationY"));
		int iSiz_W = PubTools.Str2Int(CommonMethods.GetIniValue("InvoiceProgress", "PK_Width"));
		int iSiz_H = PubTools.Str2Int(CommonMethods.GetIniValue("InvoiceProgress", "PK_Height"));
		if (iLoc_X > 0 && iLoc_Y > 0)
		{
			base.Location = new Point(iLoc_X, iLoc_Y);
		}
		if (iSiz_W > 0)
		{
			base.Width = iSiz_W;
		}
		if (iSiz_H > 0)
		{
			base.Height = 256;
		}
	}

	private void FormInvoiceProgress_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (base.WindowState != FormWindowState.Maximized)
		{
			CommonMethods.WriteIniValue("InvoiceProgress", "PK_LocationX", base.Location.X.ToString());
			CommonMethods.WriteIniValue("InvoiceProgress", "PK_LocationY", base.Location.Y.ToString());
			CommonMethods.WriteIniValue("InvoiceProgress", "PK_Width", base.Size.Width.ToString());
			CommonMethods.WriteIniValue("InvoiceProgress", "PK_Height", base.Size.Height.ToString());
		}
		CommonMethods.WriteIniValue("InvoiceProgress", "WindowState", base.WindowState.ToString());
	}

	private void FormInvoiceProgress_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormInvoiceProgress");
		}
	}

	private void panel1_Paint(object sender, PaintEventArgs e)
	{
	}
}
