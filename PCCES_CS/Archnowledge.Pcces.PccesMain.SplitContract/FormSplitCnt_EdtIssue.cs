using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
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

public class FormSplitCnt_EdtIssue : Form
{
	private const string CallFormHelp = "FormSplitCnt_EdtIssue";

	private Panel panel5;

	private UltraLabel ultraLabel6;

	private Panel panel2;

	private GroupBox groupBox2;

	private UltraButton B_Btn_Cncl;

	private UltraButton B_Btn_Next;

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

	private UltraLabel lblProjectCode;

	private UltraLabel lblProjectNameC;

	private UltraLabel lblSubProjectCode;

	private UltraLabel lblSubProjectName;

	private UltraLabel lblIssue;

	private UltraCalendarCombo dpDate;

	private UltraNumericEditor txtThis_Prec;

	private UltraLabel ultraLabel15;

	private UltraButton ultraButton1;

	private UltraButton ultraButton2;

	private UltraLabel total1;

	private UltraLabel total2;

	private UltraLabel total3;

	private UltraLabel res1;

	private UltraLabel res3;

	private UltraLabel realpay1;

	private UltraLabel realpay2;

	private UltraLabel realpay3;

	private UltraNumericEditor res2;

	private UltraLabel ultraLabel16;

	private string FORM_STATUS = "INI";

	private Container components = null;

	private string F_ProjectCode;

	private string F_SubProjetCode = "";

	private int F_Issue;

	private DataTable DT1 = new DataTable();

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

	public int _Issue
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

	public FormSplitCnt_EdtIssue()
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
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton1 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.SplitContract.FormSplitCnt_EdtIssue));
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
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
		this.panel5 = new System.Windows.Forms.Panel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.B_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.B_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.c1Sizer1 = new C1.Win.C1Sizer.C1Sizer();
		this.res2 = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel15 = new Infragistics.Win.Misc.UltraLabel();
		this.dpDate = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel8 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel9 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel10 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel11 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel12 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel13 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel14 = new Infragistics.Win.Misc.UltraLabel();
		this.lblProjectCode = new Infragistics.Win.Misc.UltraLabel();
		this.lblProjectNameC = new Infragistics.Win.Misc.UltraLabel();
		this.lblSubProjectCode = new Infragistics.Win.Misc.UltraLabel();
		this.lblSubProjectName = new Infragistics.Win.Misc.UltraLabel();
		this.lblIssue = new Infragistics.Win.Misc.UltraLabel();
		this.txtThis_Prec = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
		this.ultraButton2 = new Infragistics.Win.Misc.UltraButton();
		this.total1 = new Infragistics.Win.Misc.UltraLabel();
		this.total2 = new Infragistics.Win.Misc.UltraLabel();
		this.total3 = new Infragistics.Win.Misc.UltraLabel();
		this.res1 = new Infragistics.Win.Misc.UltraLabel();
		this.res3 = new Infragistics.Win.Misc.UltraLabel();
		this.realpay1 = new Infragistics.Win.Misc.UltraLabel();
		this.realpay2 = new Infragistics.Win.Misc.UltraLabel();
		this.realpay3 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel16 = new Infragistics.Win.Misc.UltraLabel();
		this.panel5.SuspendLayout();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.c1Sizer1).BeginInit();
		this.c1Sizer1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.res2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtThis_Prec).BeginInit();
		base.SuspendLayout();
		this.panel5.BackColor = System.Drawing.Color.White;
		this.panel5.Controls.Add(this.ultraLabel6);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel5.Location = new System.Drawing.Point(0, 0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(592, 32);
		this.panel5.TabIndex = 13;
		appearance1.BackColor = System.Drawing.Color.White;
		this.ultraLabel6.Appearance = appearance1;
		this.ultraLabel6.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel6.Location = new System.Drawing.Point(14, 9);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel6.TabIndex = 2;
		this.ultraLabel6.Text = "計價期別編輯";
		this.panel2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel2.Controls.Add(this.groupBox2);
		this.panel2.Controls.Add(this.B_Btn_Cncl);
		this.panel2.Controls.Add(this.B_Btn_Next);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 349);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(592, 44);
		this.panel2.TabIndex = 14;
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(592, 8);
		this.groupBox2.TabIndex = 3;
		this.groupBox2.TabStop = false;
		this.B_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.B_Btn_Cncl.Appearance = appearance2;
		this.B_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.B_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Button;
		this.B_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.B_Btn_Cncl.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.B_Btn_Cncl.Location = new System.Drawing.Point(492, 10);
		this.B_Btn_Cncl.Name = "B_Btn_Cncl";
		this.B_Btn_Cncl.Size = new System.Drawing.Size(88, 28);
		this.B_Btn_Cncl.TabIndex = 2;
		this.B_Btn_Cncl.Text = "取消";
		this.B_Btn_Next.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance3.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.B_Btn_Next.Appearance = appearance3;
		this.B_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.B_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Button;
		this.B_Btn_Next.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.B_Btn_Next.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.B_Btn_Next.Location = new System.Drawing.Point(400, 10);
		this.B_Btn_Next.Name = "B_Btn_Next";
		this.B_Btn_Next.Size = new System.Drawing.Size(88, 28);
		this.B_Btn_Next.TabIndex = 1;
		this.B_Btn_Next.Text = "確定";
		this.c1Sizer1.AllowDrop = true;
		this.c1Sizer1.Controls.Add(this.res2);
		this.c1Sizer1.Controls.Add(this.ultraButton1);
		this.c1Sizer1.Controls.Add(this.ultraLabel15);
		this.c1Sizer1.Controls.Add(this.dpDate);
		this.c1Sizer1.Controls.Add(this.ultraLabel1);
		this.c1Sizer1.Controls.Add(this.ultraLabel2);
		this.c1Sizer1.Controls.Add(this.ultraLabel3);
		this.c1Sizer1.Controls.Add(this.ultraLabel4);
		this.c1Sizer1.Controls.Add(this.ultraLabel5);
		this.c1Sizer1.Controls.Add(this.ultraLabel7);
		this.c1Sizer1.Controls.Add(this.ultraLabel8);
		this.c1Sizer1.Controls.Add(this.ultraLabel9);
		this.c1Sizer1.Controls.Add(this.ultraLabel10);
		this.c1Sizer1.Controls.Add(this.ultraLabel11);
		this.c1Sizer1.Controls.Add(this.ultraLabel12);
		this.c1Sizer1.Controls.Add(this.ultraLabel13);
		this.c1Sizer1.Controls.Add(this.ultraLabel14);
		this.c1Sizer1.Controls.Add(this.lblProjectCode);
		this.c1Sizer1.Controls.Add(this.lblProjectNameC);
		this.c1Sizer1.Controls.Add(this.lblSubProjectCode);
		this.c1Sizer1.Controls.Add(this.lblSubProjectName);
		this.c1Sizer1.Controls.Add(this.lblIssue);
		this.c1Sizer1.Controls.Add(this.txtThis_Prec);
		this.c1Sizer1.Controls.Add(this.ultraButton2);
		this.c1Sizer1.Controls.Add(this.total1);
		this.c1Sizer1.Controls.Add(this.total2);
		this.c1Sizer1.Controls.Add(this.total3);
		this.c1Sizer1.Controls.Add(this.res1);
		this.c1Sizer1.Controls.Add(this.res3);
		this.c1Sizer1.Controls.Add(this.realpay1);
		this.c1Sizer1.Controls.Add(this.realpay2);
		this.c1Sizer1.Controls.Add(this.realpay3);
		this.c1Sizer1.Controls.Add(this.ultraLabel16);
		this.c1Sizer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.c1Sizer1.GridDefinition = "7.57097791798107:False:False;6.62460567823344:False:False;7.88643533123028:False:False;6.30914826498423:False:False;6.94006309148265:False:False;7.25552050473186:False:False;6.94006309148265:False:False;6.62460567823344:False:False;7.25552050473186:False:False;7.25552050473186:False:False;5.99369085173502:False:False;6.94006309148265:False:False;\t1.68918918918919:False:True;13.5135135135135:False:True;24.8310810810811:False:False;3.37837837837838:False:True;24.6621621621622:False:False;24.8310810810811:False:False;1.68918918918919:False:True;";
		this.c1Sizer1.Location = new System.Drawing.Point(0, 32);
		this.c1Sizer1.Name = "c1Sizer1";
		this.c1Sizer1.Size = new System.Drawing.Size(592, 317);
		this.c1Sizer1.TabIndex = 15;
		this.c1Sizer1.TabStop = false;
		this.res2.Location = new System.Drawing.Point(277, 268);
		this.res2.Name = "res2";
		this.res2.PromptChar = ' ';
		this.res2.Size = new System.Drawing.Size(146, 21);
		this.res2.TabIndex = 40;
		this.res2.ValueChanged += new System.EventHandler(res2_ValueChanged);
		appearance4.FontData.Name = "細明體";
		appearance4.FontData.SizeInPoints = 9f;
		appearance4.TextVAlign = Infragistics.Win.VAlign.Top;
		this.ultraButton1.Appearance = appearance4;
		this.ultraButton1.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton1.Location = new System.Drawing.Point(277, 163);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.Size = new System.Drawing.Size(146, 22);
		this.ultraButton1.TabIndex = 39;
		this.ultraButton1.Text = "依進度重算";
		this.ultraButton1.Click += new System.EventHandler(ultraButton1_Click);
		appearance5.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel15.Appearance = appearance5;
		this.ultraLabel15.Location = new System.Drawing.Point(277, 163);
		this.ultraLabel15.Name = "ultraLabel15";
		this.ultraLabel15.Size = new System.Drawing.Size(146, 22);
		this.ultraLabel15.TabIndex = 38;
		this.ultraLabel15.Text = "%";
		dateButton1.Caption = "今天";
		this.dpDate.DateButtons.Add(dateButton1);
		this.dpDate.DayOfWeekCaptionStyle = Infragistics.Win.UltraWinSchedule.DayOfWeekCaptionStyle.ShortDescription;
		this.dpDate.Location = new System.Drawing.Point(102, 136);
		this.dpDate.Name = "dpDate";
		this.dpDate.NonAutoSizeHeight = 21;
		this.dpDate.Size = new System.Drawing.Size(171, 21);
		this.dpDate.TabIndex = 37;
		this.dpDate.TipStyle = Infragistics.Win.UltraWinSchedule.TipStyleDay.Holidays;
		this.dpDate.Value = resources.GetObject("dpDate.Value");
		this.dpDate.AfterCloseUp += new System.EventHandler(dpDate_AfterCloseUp);
		appearance6.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel1.Appearance = appearance6;
		this.ultraLabel1.Location = new System.Drawing.Point(18, 4);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(80, 24);
		this.ultraLabel1.TabIndex = 0;
		this.ultraLabel1.Text = "專案代碼:";
		appearance7.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel2.Appearance = appearance7;
		this.ultraLabel2.Location = new System.Drawing.Point(18, 32);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(80, 21);
		this.ultraLabel2.TabIndex = 0;
		this.ultraLabel2.Text = "專案名稱:";
		appearance8.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel3.Appearance = appearance8;
		this.ultraLabel3.Location = new System.Drawing.Point(18, 57);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(80, 25);
		this.ultraLabel3.TabIndex = 0;
		this.ultraLabel3.Text = "契約代碼:";
		appearance9.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel4.Appearance = appearance9;
		this.ultraLabel4.Location = new System.Drawing.Point(18, 86);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(80, 20);
		this.ultraLabel4.TabIndex = 0;
		this.ultraLabel4.Text = "契約名稱:";
		appearance10.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel5.Appearance = appearance10;
		this.ultraLabel5.Location = new System.Drawing.Point(18, 110);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(80, 22);
		this.ultraLabel5.TabIndex = 0;
		this.ultraLabel5.Text = "估驗期數:";
		appearance11.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel7.Appearance = appearance11;
		this.ultraLabel7.Location = new System.Drawing.Point(18, 136);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(80, 23);
		this.ultraLabel7.TabIndex = 0;
		this.ultraLabel7.Text = "結束日期:";
		appearance12.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel8.Appearance = appearance12;
		this.ultraLabel8.Location = new System.Drawing.Point(18, 163);
		this.ultraLabel8.Name = "ultraLabel8";
		this.ultraLabel8.Size = new System.Drawing.Size(80, 22);
		this.ultraLabel8.TabIndex = 0;
		this.ultraLabel8.Text = "本期進度:";
		appearance13.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel9.Appearance = appearance13;
		this.ultraLabel9.Location = new System.Drawing.Point(18, 241);
		this.ultraLabel9.Name = "ultraLabel9";
		this.ultraLabel9.Size = new System.Drawing.Size(80, 23);
		this.ultraLabel9.TabIndex = 0;
		this.ultraLabel9.Text = "工程款:";
		appearance14.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel10.Appearance = appearance14;
		this.ultraLabel10.Location = new System.Drawing.Point(18, 268);
		this.ultraLabel10.Name = "ultraLabel10";
		this.ultraLabel10.Size = new System.Drawing.Size(80, 19);
		this.ultraLabel10.TabIndex = 0;
		this.ultraLabel10.Text = "保留款:";
		appearance15.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel11.Appearance = appearance15;
		this.ultraLabel11.Location = new System.Drawing.Point(18, 291);
		this.ultraLabel11.Name = "ultraLabel11";
		this.ultraLabel11.Size = new System.Drawing.Size(80, 22);
		this.ultraLabel11.TabIndex = 0;
		this.ultraLabel11.Text = "實發款總計:";
		appearance16.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance16.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel12.Appearance = appearance16;
		this.ultraLabel12.Location = new System.Drawing.Point(102, 214);
		this.ultraLabel12.Name = "ultraLabel12";
		this.ultraLabel12.Size = new System.Drawing.Size(147, 23);
		this.ultraLabel12.TabIndex = 0;
		this.ultraLabel12.Text = "截至上期累計";
		appearance17.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance17.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel13.Appearance = appearance17;
		this.ultraLabel13.Location = new System.Drawing.Point(277, 214);
		this.ultraLabel13.Name = "ultraLabel13";
		this.ultraLabel13.Size = new System.Drawing.Size(146, 23);
		this.ultraLabel13.TabIndex = 0;
		this.ultraLabel13.Text = "本期";
		appearance18.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance18.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel14.Appearance = appearance18;
		this.ultraLabel14.Location = new System.Drawing.Point(427, 214);
		this.ultraLabel14.Name = "ultraLabel14";
		this.ultraLabel14.Size = new System.Drawing.Size(147, 23);
		this.ultraLabel14.TabIndex = 0;
		this.ultraLabel14.Text = "截至本期共計";
		appearance19.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblProjectCode.Appearance = appearance19;
		this.lblProjectCode.Location = new System.Drawing.Point(102, 4);
		this.lblProjectCode.Name = "lblProjectCode";
		this.lblProjectCode.Size = new System.Drawing.Size(472, 24);
		this.lblProjectCode.TabIndex = 0;
		this.lblProjectCode.Text = "[lblProjectCode]";
		appearance20.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblProjectNameC.Appearance = appearance20;
		this.lblProjectNameC.Location = new System.Drawing.Point(102, 32);
		this.lblProjectNameC.Name = "lblProjectNameC";
		this.lblProjectNameC.Size = new System.Drawing.Size(472, 21);
		this.lblProjectNameC.TabIndex = 0;
		this.lblProjectNameC.Text = "[lblProjectNameC]";
		appearance21.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblSubProjectCode.Appearance = appearance21;
		this.lblSubProjectCode.Location = new System.Drawing.Point(102, 57);
		this.lblSubProjectCode.Name = "lblSubProjectCode";
		this.lblSubProjectCode.Size = new System.Drawing.Size(472, 25);
		this.lblSubProjectCode.TabIndex = 0;
		this.lblSubProjectCode.Text = "[lblSubProjectCode]";
		appearance22.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblSubProjectName.Appearance = appearance22;
		this.lblSubProjectName.Location = new System.Drawing.Point(102, 86);
		this.lblSubProjectName.Name = "lblSubProjectName";
		this.lblSubProjectName.Size = new System.Drawing.Size(472, 20);
		this.lblSubProjectName.TabIndex = 0;
		this.lblSubProjectName.Text = "[lblSubProjectName]";
		appearance23.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblIssue.Appearance = appearance23;
		this.lblIssue.Location = new System.Drawing.Point(102, 110);
		this.lblIssue.Name = "lblIssue";
		this.lblIssue.Size = new System.Drawing.Size(147, 22);
		this.lblIssue.TabIndex = 0;
		this.lblIssue.Text = "[lblIssue]";
		this.txtThis_Prec.Location = new System.Drawing.Point(102, 163);
		this.txtThis_Prec.MaxValue = 100;
		this.txtThis_Prec.MinValue = -100;
		this.txtThis_Prec.Name = "txtThis_Prec";
		this.txtThis_Prec.PromptChar = ' ';
		this.txtThis_Prec.Size = new System.Drawing.Size(147, 21);
		this.txtThis_Prec.TabIndex = 16;
		this.txtThis_Prec.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtThis_Prec_KeyPress);
		this.txtThis_Prec.ValueChanged += new System.EventHandler(txtThis_Prec_ValueChanged);
		appearance24.FontData.Name = "細明體";
		appearance24.FontData.SizeInPoints = 9f;
		appearance24.TextVAlign = Infragistics.Win.VAlign.Top;
		this.ultraButton2.Appearance = appearance24;
		this.ultraButton2.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton2.Location = new System.Drawing.Point(427, 163);
		this.ultraButton2.Name = "ultraButton2";
		this.ultraButton2.Size = new System.Drawing.Size(147, 22);
		this.ultraButton2.TabIndex = 39;
		this.ultraButton2.Text = "重算保留款";
		appearance25.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance25.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.total1.Appearance = appearance25;
		this.total1.Location = new System.Drawing.Point(102, 241);
		this.total1.Name = "total1";
		this.total1.Size = new System.Drawing.Size(147, 23);
		this.total1.TabIndex = 0;
		this.total1.Text = "[total1]";
		appearance26.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance26.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.total2.Appearance = appearance26;
		this.total2.Location = new System.Drawing.Point(277, 241);
		this.total2.Name = "total2";
		this.total2.Size = new System.Drawing.Size(146, 23);
		this.total2.TabIndex = 0;
		this.total2.Text = "[total2]";
		appearance27.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance27.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.total3.Appearance = appearance27;
		this.total3.Location = new System.Drawing.Point(427, 241);
		this.total3.Name = "total3";
		this.total3.Size = new System.Drawing.Size(147, 23);
		this.total3.TabIndex = 0;
		this.total3.Text = "[total3]";
		appearance28.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance28.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.res1.Appearance = appearance28;
		this.res1.Location = new System.Drawing.Point(102, 268);
		this.res1.Name = "res1";
		this.res1.Size = new System.Drawing.Size(147, 19);
		this.res1.TabIndex = 0;
		this.res1.Text = "[res1]";
		appearance29.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance29.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.res3.Appearance = appearance29;
		this.res3.Location = new System.Drawing.Point(427, 268);
		this.res3.Name = "res3";
		this.res3.Size = new System.Drawing.Size(147, 19);
		this.res3.TabIndex = 0;
		this.res3.Text = "[res3]";
		appearance30.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance30.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.realpay1.Appearance = appearance30;
		this.realpay1.Location = new System.Drawing.Point(102, 291);
		this.realpay1.Name = "realpay1";
		this.realpay1.Size = new System.Drawing.Size(147, 22);
		this.realpay1.TabIndex = 0;
		this.realpay1.Text = "[realpay1]";
		appearance31.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance31.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.realpay2.Appearance = appearance31;
		this.realpay2.Location = new System.Drawing.Point(277, 291);
		this.realpay2.Name = "realpay2";
		this.realpay2.Size = new System.Drawing.Size(146, 22);
		this.realpay2.TabIndex = 0;
		this.realpay2.Text = "[realpay2]";
		appearance32.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance32.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.realpay3.Appearance = appearance32;
		this.realpay3.Location = new System.Drawing.Point(427, 291);
		this.realpay3.Name = "realpay3";
		this.realpay3.Size = new System.Drawing.Size(147, 22);
		this.realpay3.TabIndex = 0;
		this.realpay3.Text = "[realpay3]";
		appearance33.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance33.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel16.Appearance = appearance33;
		this.ultraLabel16.Location = new System.Drawing.Point(253, 163);
		this.ultraLabel16.Name = "ultraLabel16";
		this.ultraLabel16.Size = new System.Drawing.Size(20, 22);
		this.ultraLabel16.TabIndex = 0;
		this.ultraLabel16.Text = "%";
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.CancelButton = this.B_Btn_Cncl;
		base.ClientSize = new System.Drawing.Size(592, 393);
		base.Controls.Add(this.c1Sizer1);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel5);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.Name = "FormSplitCnt_EdtIssue";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "計價期別編輯";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormSplitCnt_EdtIssue_KeyDown);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormSplitCnt_EdtIssue_FormClosing);
		base.Load += new System.EventHandler(FormSplitCnt_EdtIssue_Load);
		base.Activated += new System.EventHandler(FormSplitCnt_EdtIssue_Activated);
		this.panel5.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.c1Sizer1).EndInit();
		this.c1Sizer1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.res2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtThis_Prec).EndInit();
		base.ResumeLayout(false);
	}

	private void FormSplitCnt_EdtIssue_Load(object sender, EventArgs e)
	{
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1 = new ArrayList();
		tmp_AL1.Add("PccAdmin");
		tmp_AL1.Add("(subacc_edit) 更新估驗計價總檔");
		sub_acc acccom = new sub_acc(tmp_AL1);
		DT1 = acccom.ListItem("", F_SubProjetCode, F_ProjectCode);
		acccom = null;
		DT1.Columns.Add("total1");
		DT1.Columns.Add("total3");
		DT1.Columns.Add("res1");
		DT1.Columns.Add("res3");
		DT1.Columns.Add("realpay1");
		DT1.Columns.Add("realpay3");
		DT1 = reTotal(DT1);
		ControlsInit();
		BindToControls();
		LoadingScreen();
	}

	private void LoadingScreen()
	{
		string Status = CommonMethods.GetIniValue("SplitCnt_EditIssue", "WindowState");
		if (Status == "Maximized")
		{
			base.WindowState = FormWindowState.Maximized;
		}
		int iLoc_X = PubTools.Str2Int(CommonMethods.GetIniValue("SplitCnt_EditIssue", "PK_LocationX"));
		int iLoc_Y = PubTools.Str2Int(CommonMethods.GetIniValue("SplitCnt_EditIssue", "PK_LocationY"));
		int iSiz_W = PubTools.Str2Int(CommonMethods.GetIniValue("SplitCnt_EditIssue", "PK_Width"));
		int iSiz_H = PubTools.Str2Int(CommonMethods.GetIniValue("SplitCnt_EditIssue", "PK_Height"));
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
			base.Height = iSiz_H;
		}
	}

	private void ControlsInit()
	{
		lblProjectCode.Text = F_ProjectCode;
		lblSubProjectCode.Text = F_SubProjetCode;
		lblIssue.Text = F_Issue.ToString();
	}

	private void BindToControls()
	{
		FORM_STATUS = "BIND";
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1 = new ArrayList();
		tmp_AL1.Add("PccAdmin");
		tmp_AL1.Add("(subacc) 編輯-估驗計價");
		PubProject prjcom = new PubProject(tmp_AL1);
		lblProjectNameC.Text = prjcom.GetValue("projCName", F_ProjectCode);
		prjcom = null;
		subProject subcom = new subProject(tmp_AL1);
		lblSubProjectName.Text = subcom.GetValue("projdes", F_SubProjetCode, F_ProjectCode);
		subcom = null;
		foreach (DataRow dr in DT1.Rows)
		{
			if (int.Parse(dr["queue"].ToString()) == F_Issue)
			{
				txtThis_Prec.Text = dr["this_prec"].ToString();
				dpDate.Value = DateTime.Parse(dr["date_insp"].ToString());
				total1.Text = string.Format("{0:N0}", dr["total1"]);
				res1.Text = string.Format("{0:N0}", dr["res1"]);
				realpay1.Text = string.Format("{0:N0}", dr["realpay1"]);
				if (dr["acctotal"].Equals(DBNull.Value))
				{
					total2.Text = "0";
				}
				else
				{
					total2.Text = string.Format("{0:N0}", dr["acctotal"]);
				}
				if (dr["reserve"].Equals(DBNull.Value))
				{
					res2.Text = "0";
				}
				else
				{
					res2.Text = string.Format("{0:N0}", dr["reserve"]);
				}
				if (dr["realpay"].Equals(DBNull.Value))
				{
					realpay2.Text = "0";
				}
				else
				{
					realpay2.Text = string.Format("{0:N0}", dr["realpay"]);
				}
				total3.Text = string.Format("{0:N0}", dr["total3"]);
				res3.Text = string.Format("{0:N0}", dr["res3"]);
				realpay3.Text = string.Format("{0:N0}", dr["realpay3"]);
			}
		}
		FORM_STATUS = "ACT";
	}

	private DataTable reTotal(DataTable ldt_mydt)
	{
		decimal ld_total = 0m;
		decimal ld_reserve = 0m;
		decimal ld_realpay = 0m;
		decimal ld_prec = 0m;
		foreach (DataRow dr in ldt_mydt.Rows)
		{
			dr["total1"] = ld_total;
			dr["res1"] = ld_reserve;
			dr["realpay1"] = ld_realpay;
			if (dr["acctotal"].Equals(DBNull.Value))
			{
				ld_total += 0m;
			}
			else
			{
				ld_total += decimal.Parse(dr["acctotal"].ToString());
			}
			if (dr["reserve"].Equals(DBNull.Value))
			{
				ld_reserve += 0m;
			}
			else
			{
				ld_reserve += decimal.Parse(dr["reserve"].ToString());
			}
			if (dr["realpay"].Equals(DBNull.Value))
			{
				ld_realpay += 0m;
			}
			else
			{
				ld_realpay += decimal.Parse(dr["realpay"].ToString());
			}
			if (dr["this_prec"].Equals(DBNull.Value))
			{
				ld_prec += 0m;
			}
			else
			{
				ld_prec += decimal.Parse(dr["this_prec"].ToString());
			}
			dr["total3"] = ld_total;
			dr["res3"] = ld_reserve;
			dr["realpay3"] = ld_realpay;
		}
		return ldt_mydt;
	}

	private void dpDate_AfterCloseUp(object sender, EventArgs e)
	{
		if (!(FORM_STATUS != "ACT"))
		{
			ArrayList tmp_AL1 = new ArrayList();
			tmp_AL1 = new ArrayList();
			tmp_AL1.Add("PccAdmin");
			tmp_AL1.Add("(subacc) 編輯-估驗計價");
			sub_acc acccom = new sub_acc(tmp_AL1);
			acccom.ps_prjcode = F_ProjectCode;
			acccom.ps_subcode = F_SubProjetCode;
			acccom.ps_queue = F_Issue.ToString();
			acccom.ps_date_insp = dpDate.Text;
			acccom.UpdItem();
			DT1 = acccom.ListItem("", F_SubProjetCode, F_ProjectCode);
			acccom = null;
			DT1.Columns.Add("total1");
			DT1.Columns.Add("total3");
			DT1.Columns.Add("res1");
			DT1.Columns.Add("res3");
			DT1.Columns.Add("realpay1");
			DT1.Columns.Add("realpay3");
			DT1 = reTotal(DT1);
			BindToControls();
		}
	}

	private void txtThis_Prec_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			txtThis_Prec_ValueChanged(this, EventArgs.Empty);
		}
	}

	private void txtThis_Prec_ValueChanged(object sender, EventArgs e)
	{
		if (!(FORM_STATUS != "ACT"))
		{
			ArrayList tmp_AL1 = new ArrayList();
			tmp_AL1 = new ArrayList();
			tmp_AL1.Add("PccAdmin");
			tmp_AL1.Add("(subacc) 編輯-估驗計價");
			sub_acc acccom = new sub_acc(tmp_AL1);
			acccom.ps_prjcode = F_ProjectCode;
			acccom.ps_subcode = F_SubProjetCode;
			acccom.ps_queue = F_Issue.ToString();
			acccom.ps_this_prec = txtThis_Prec.Value.ToString();
			acccom.UpdItem();
			DT1 = acccom.ListItem("", F_SubProjetCode, F_ProjectCode);
			acccom = null;
			DT1.Columns.Add("total1");
			DT1.Columns.Add("total3");
			DT1.Columns.Add("res1");
			DT1.Columns.Add("res3");
			DT1.Columns.Add("realpay1");
			DT1.Columns.Add("realpay3");
			DT1 = reTotal(DT1);
			BindToControls();
		}
	}

	private void res2_ValueChanged(object sender, EventArgs e)
	{
		if (!(FORM_STATUS != "ACT"))
		{
			ArrayList tmp_AL1 = new ArrayList();
			tmp_AL1 = new ArrayList();
			tmp_AL1.Add("PccAdmin");
			tmp_AL1.Add("(subacc) 編輯-估驗計價");
			sub_acc acccom = new sub_acc(tmp_AL1);
			acccom.ps_prjcode = F_ProjectCode;
			acccom.ps_subcode = F_SubProjetCode;
			acccom.ps_queue = F_Issue.ToString();
			acccom.ps_reserve = res2.Value.ToString();
			acccom.UpdItem();
			DT1 = acccom.ListItem("", F_SubProjetCode, F_ProjectCode);
			acccom = null;
			DT1.Columns.Add("total1");
			DT1.Columns.Add("total3");
			DT1.Columns.Add("res1");
			DT1.Columns.Add("res3");
			DT1.Columns.Add("realpay1");
			DT1.Columns.Add("realpay3");
			DT1 = reTotal(DT1);
			BindToControls();
		}
	}

	private void ultraButton1_Click(object sender, EventArgs e)
	{
		double ld_temp = Convert.ToDouble(txtThis_Prec.Value);
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1 = new ArrayList();
		tmp_AL1.Add("PccAdmin");
		tmp_AL1.Add("(subacc) 編輯-估驗計價");
		string ls_prjcode = F_ProjectCode;
		string ls_subproj = F_SubProjetCode;
		string ls_queue = F_Issue.ToString();
		submfq mfqcom = new submfq(tmp_AL1);
		DT1 = mfqcom.ListItem("", ls_queue.Trim(), ls_subproj.Trim(), ls_prjcode.Trim());
		foreach (DataRow dr in DT1.Rows)
		{
			double ld_itemqty = double.Parse(dr["itemqty"].ToString());
			double ld_itemcost = double.Parse(dr["itemcost"].ToString());
			double ld_tmoqty = PubTools.ARound(ld_temp * ld_itemqty / 100.0, 3L);
			mfqcom.ps_quantity = ld_tmoqty.ToString();
			dr["quantity"] = ld_tmoqty;
			double ld_tmoamt = PubTools.ARound(ld_itemcost * ld_tmoqty, 2L);
			mfqcom.ps_tom_amt = ld_tmoamt.ToString();
			dr["tom_amt"] = ld_tmoamt;
			mfqcom.ps_itemdes = dr["itemdes"].ToString();
			mfqcom.ps_itemno = dr["qucode"].ToString();
			mfqcom.ps_prjcode = dr["project"].ToString();
			mfqcom.ps_subcode = dr["sproj"].ToString();
			mfqcom.UpdItem();
		}
		mfqcom = null;
		sub_acc acccom = new sub_acc(tmp_AL1);
		DT1 = acccom.ReTotal(DT1, ls_queue, ls_subproj, ls_prjcode);
		acccom = null;
		DT1 = null;
		base.DialogResult = DialogResult.OK;
		Close();
	}

	private void FormSplitCnt_EdtIssue_Activated(object sender, EventArgs e)
	{
		if (FORM_STATUS == "INI")
		{
			FORM_STATUS = "ACT";
		}
	}

	private void FormSplitCnt_EdtIssue_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (base.WindowState != FormWindowState.Maximized)
		{
			CommonMethods.WriteIniValue("SplitCnt_EditIssue", "PK_LocationX", base.Location.X.ToString());
			CommonMethods.WriteIniValue("SplitCnt_EditIssue", "PK_LocationY", base.Location.Y.ToString());
			CommonMethods.WriteIniValue("SplitCnt_EditIssue", "PK_Width", base.Size.Width.ToString());
			CommonMethods.WriteIniValue("SplitCnt_EditIssue", "PK_Height", base.Size.Height.ToString());
		}
		CommonMethods.WriteIniValue("SplitCnt_EditIssue", "WindowState", base.WindowState.ToString());
	}

	private void FormSplitCnt_EdtIssue_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormSplitCnt_EdtIssue");
		}
	}
}
