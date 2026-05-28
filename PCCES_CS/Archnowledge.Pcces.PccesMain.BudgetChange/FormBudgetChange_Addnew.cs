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

namespace Archnowledge.Pcces.PccesMain.BudgetChange;

public class FormBudgetChange_Addnew : Form
{
	private const string CallFormHelp = "FormBudgetChange_Addnew";

	private Panel panel1;

	private Panel panel2;

	private Panel panel3;

	private C1Sizer c1Sizer1;

	private UltraLabel ultraLabel2;

	private UltraLabel ultraLabel3;

	private UltraLabel ultraLabel4;

	private UltraLabel ultraLabel5;

	private UltraTextEditor txtChgTxtNo;

	private UltraLabel ultraLabel6;

	private UltraLabel ultraLabel7;

	private UltraTextEditor txtExtendDay;

	private UltraLabel lblChgCount;

	private UltraTextEditor txtKeyNote;

	private UltraLabel ultraLabel8;

	private UltraLabel ultraLabel9;

	private UltraLabel ultraLabel10;

	private UltraTextEditor txtExplain;

	private UltraTextEditor txtContent;

	private UltraLabel lblFormCaption;

	private UltraCalendarCombo dpChgDate;

	private UltraCalendarCombo dpChgFinish;

	private UltraCalendarCombo dpChgAgree;

	private UltraButton Btn_Cncl;

	private UltraButton Btn_OK;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel11;

	private UltraLabel ultraLabel12;

	private UltraLabel lblPreAmt;

	private UltraLabel lblPostAmt;

	private UltraLabel lblDiffer;

	private Container components = null;

	private string F_UserID;

	private string F_EditMode;

	private string F_ProjectCode;

	private string F_ProjectNameC;

	private string F_SubProjectCode = "";

	private string F_ChgCount;

	private UltraLabel ultraLabel13;

	private NumericUpDown numericUpDown1;

	private DataTable DT1 = new DataTable();

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

	public string _EditMode
	{
		get
		{
			return F_EditMode;
		}
		set
		{
			F_EditMode = value;
		}
	}

	public string _ChgCount
	{
		get
		{
			return F_ChgCount;
		}
		set
		{
			F_ChgCount = value;
		}
	}

	public FormBudgetChange_Addnew()
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.BudgetChange.FormBudgetChange_Addnew));
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton2 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
		Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton3 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
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
		this.panel1 = new System.Windows.Forms.Panel();
		this.lblFormCaption = new Infragistics.Win.Misc.UltraLabel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.Btn_OK = new Infragistics.Win.Misc.UltraButton();
		this.panel3 = new System.Windows.Forms.Panel();
		this.c1Sizer1 = new C1.Win.C1Sizer.C1Sizer();
		this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
		this.ultraLabel13 = new Infragistics.Win.Misc.UltraLabel();
		this.txtKeyNote = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.lblChgCount = new Infragistics.Win.Misc.UltraLabel();
		this.dpChgDate = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.txtChgTxtNo = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.dpChgFinish = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
		this.dpChgAgree = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.txtExtendDay = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel8 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel9 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel10 = new Infragistics.Win.Misc.UltraLabel();
		this.txtExplain = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtContent = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel11 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel12 = new Infragistics.Win.Misc.UltraLabel();
		this.lblPreAmt = new Infragistics.Win.Misc.UltraLabel();
		this.lblPostAmt = new Infragistics.Win.Misc.UltraLabel();
		this.lblDiffer = new Infragistics.Win.Misc.UltraLabel();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.c1Sizer1).BeginInit();
		this.c1Sizer1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.numericUpDown1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtKeyNote).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dpChgDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtChgTxtNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dpChgFinish).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dpChgAgree).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtExtendDay).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtExplain).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtContent).BeginInit();
		base.SuspendLayout();
		this.panel1.Controls.Add(this.lblFormCaption);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(782, 36);
		this.panel1.TabIndex = 0;
		appearance1.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance1.ForeColor = System.Drawing.Color.White;
		appearance1.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblFormCaption.Appearance = appearance1;
		this.lblFormCaption.Dock = System.Windows.Forms.DockStyle.Fill;
		this.lblFormCaption.Location = new System.Drawing.Point(0, 0);
		this.lblFormCaption.Name = "lblFormCaption";
		this.lblFormCaption.Size = new System.Drawing.Size(782, 36);
		this.lblFormCaption.TabIndex = 0;
		this.lblFormCaption.Text = " 新增預算書變更";
		this.panel2.Controls.Add(this.Btn_Cncl);
		this.panel2.Controls.Add(this.Btn_OK);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 527);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(782, 36);
		this.panel2.TabIndex = 1;
		this.Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.Btn_Cncl.Appearance = appearance2;
		this.Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.Btn_Cncl.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.Btn_Cncl.Location = new System.Drawing.Point(676, 3);
		this.Btn_Cncl.Name = "Btn_Cncl";
		this.Btn_Cncl.ShowFocusRect = false;
		this.Btn_Cncl.ShowOutline = false;
		this.Btn_Cncl.Size = new System.Drawing.Size(88, 28);
		this.Btn_Cncl.SupportThemes = false;
		this.Btn_Cncl.TabIndex = 4;
		this.Btn_Cncl.Text = "取消";
		this.Btn_OK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance3.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.Btn_OK.Appearance = appearance3;
		this.Btn_OK.BackColor = System.Drawing.SystemColors.Control;
		this.Btn_OK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.Btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.Btn_OK.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.Btn_OK.Location = new System.Drawing.Point(584, 3);
		this.Btn_OK.Name = "Btn_OK";
		this.Btn_OK.ShowFocusRect = false;
		this.Btn_OK.ShowOutline = false;
		this.Btn_OK.Size = new System.Drawing.Size(88, 28);
		this.Btn_OK.SupportThemes = false;
		this.Btn_OK.TabIndex = 3;
		this.Btn_OK.Text = "確定";
		this.Btn_OK.Click += new System.EventHandler(Btn_OK_Click);
		this.panel3.Controls.Add(this.c1Sizer1);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel3.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.panel3.Location = new System.Drawing.Point(0, 36);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(782, 491);
		this.panel3.TabIndex = 2;
		this.c1Sizer1.AllowDrop = true;
		this.c1Sizer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.c1Sizer1.Controls.Add(this.numericUpDown1);
		this.c1Sizer1.Controls.Add(this.ultraLabel13);
		this.c1Sizer1.Controls.Add(this.txtKeyNote);
		this.c1Sizer1.Controls.Add(this.lblChgCount);
		this.c1Sizer1.Controls.Add(this.dpChgDate);
		this.c1Sizer1.Controls.Add(this.ultraLabel2);
		this.c1Sizer1.Controls.Add(this.txtChgTxtNo);
		this.c1Sizer1.Controls.Add(this.ultraLabel3);
		this.c1Sizer1.Controls.Add(this.ultraLabel4);
		this.c1Sizer1.Controls.Add(this.ultraLabel5);
		this.c1Sizer1.Controls.Add(this.dpChgFinish);
		this.c1Sizer1.Controls.Add(this.dpChgAgree);
		this.c1Sizer1.Controls.Add(this.ultraLabel6);
		this.c1Sizer1.Controls.Add(this.ultraLabel7);
		this.c1Sizer1.Controls.Add(this.txtExtendDay);
		this.c1Sizer1.Controls.Add(this.ultraLabel8);
		this.c1Sizer1.Controls.Add(this.ultraLabel9);
		this.c1Sizer1.Controls.Add(this.ultraLabel10);
		this.c1Sizer1.Controls.Add(this.txtExplain);
		this.c1Sizer1.Controls.Add(this.txtContent);
		this.c1Sizer1.Controls.Add(this.ultraLabel1);
		this.c1Sizer1.Controls.Add(this.ultraLabel11);
		this.c1Sizer1.Controls.Add(this.ultraLabel12);
		this.c1Sizer1.Controls.Add(this.lblPreAmt);
		this.c1Sizer1.Controls.Add(this.lblPostAmt);
		this.c1Sizer1.Controls.Add(this.lblDiffer);
		this.c1Sizer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.c1Sizer1.GridDefinition = resources.GetString("c1Sizer1.GridDefinition");
		this.c1Sizer1.Location = new System.Drawing.Point(0, 0);
		this.c1Sizer1.Name = "c1Sizer1";
		this.c1Sizer1.Size = new System.Drawing.Size(782, 491);
		this.c1Sizer1.TabIndex = 0;
		this.c1Sizer1.TabStop = false;
		this.numericUpDown1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.numericUpDown1.Location = new System.Drawing.Point(534, 91);
		this.numericUpDown1.Name = "numericUpDown1";
		this.numericUpDown1.Size = new System.Drawing.Size(100, 23);
		this.numericUpDown1.TabIndex = 40;
		this.numericUpDown1.Value = new decimal(new int[4] { 100, 0, 0, 0 });
		this.numericUpDown1.Visible = false;
		appearance4.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance4.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel13.Appearance = appearance4;
		this.ultraLabel13.Location = new System.Drawing.Point(400, 91);
		this.ultraLabel13.Name = "ultraLabel13";
		this.ultraLabel13.Size = new System.Drawing.Size(130, 23);
		this.ultraLabel13.TabIndex = 39;
		this.ultraLabel13.Text = "未核可前暫估比率:";
		this.ultraLabel13.Visible = false;
		this.txtKeyNote.AutoSize = true;
		this.txtKeyNote.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.txtKeyNote.Location = new System.Drawing.Point(18, 147);
		this.txtKeyNote.MaxLength = 200;
		this.txtKeyNote.Multiline = true;
		this.txtKeyNote.Name = "txtKeyNote";
		this.txtKeyNote.Size = new System.Drawing.Size(744, 84);
		this.txtKeyNote.TabIndex = 38;
		this.txtKeyNote.Text = "txtKeyNote";
		appearance5.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblChgCount.Appearance = appearance5;
		this.lblChgCount.Location = new System.Drawing.Point(142, 4);
		this.lblChgCount.Name = "lblChgCount";
		this.lblChgCount.Size = new System.Drawing.Size(130, 25);
		this.lblChgCount.TabIndex = 37;
		this.lblChgCount.Text = "[times]";
		dateButton1.Caption = "今天";
		this.dpChgDate.DateButtons.Add(dateButton1);
		this.dpChgDate.DayOfWeekCaptionStyle = Infragistics.Win.UltraWinSchedule.DayOfWeekCaptionStyle.ShortDescription;
		this.dpChgDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.dpChgDate.Location = new System.Drawing.Point(142, 33);
		this.dpChgDate.Name = "dpChgDate";
		this.dpChgDate.NonAutoSizeHeight = 21;
		this.dpChgDate.Size = new System.Drawing.Size(254, 21);
		this.dpChgDate.TabIndex = 36;
		this.dpChgDate.TipStyle = Infragistics.Win.UltraWinSchedule.TipStyleDay.Holidays;
		this.dpChgDate.Value = resources.GetObject("dpChgDate.Value");
		appearance6.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance6.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel2.Appearance = appearance6;
		this.ultraLabel2.Location = new System.Drawing.Point(18, 4);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(120, 25);
		this.ultraLabel2.TabIndex = 1;
		this.ultraLabel2.Text = "變更次別:";
		this.txtChgTxtNo.AutoSize = true;
		this.txtChgTxtNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.txtChgTxtNo.Location = new System.Drawing.Point(534, 4);
		this.txtChgTxtNo.MaxLength = 50;
		this.txtChgTxtNo.Name = "txtChgTxtNo";
		this.txtChgTxtNo.Size = new System.Drawing.Size(228, 22);
		this.txtChgTxtNo.TabIndex = 0;
		this.txtChgTxtNo.Text = "txtChgTxtNo";
		appearance7.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance7.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel3.Appearance = appearance7;
		this.ultraLabel3.Location = new System.Drawing.Point(18, 33);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(120, 25);
		this.ultraLabel3.TabIndex = 1;
		this.ultraLabel3.Text = "變更日期:";
		appearance8.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance8.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel4.Appearance = appearance8;
		this.ultraLabel4.Location = new System.Drawing.Point(400, 4);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(130, 25);
		this.ultraLabel4.TabIndex = 1;
		this.ultraLabel4.Text = "變更文號:";
		appearance9.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance9.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel5.Appearance = appearance9;
		this.ultraLabel5.Location = new System.Drawing.Point(18, 62);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(120, 25);
		this.ultraLabel5.TabIndex = 1;
		this.ultraLabel5.Text = "本次延長工期:";
		dateButton2.Caption = "今天";
		this.dpChgFinish.DateButtons.Add(dateButton2);
		this.dpChgFinish.DayOfWeekCaptionStyle = Infragistics.Win.UltraWinSchedule.DayOfWeekCaptionStyle.ShortDescription;
		this.dpChgFinish.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.dpChgFinish.Location = new System.Drawing.Point(534, 33);
		this.dpChgFinish.Name = "dpChgFinish";
		this.dpChgFinish.NonAutoSizeHeight = 21;
		this.dpChgFinish.Size = new System.Drawing.Size(228, 21);
		this.dpChgFinish.TabIndex = 36;
		this.dpChgFinish.TipStyle = Infragistics.Win.UltraWinSchedule.TipStyleDay.Holidays;
		this.dpChgFinish.Value = resources.GetObject("dpChgFinish.Value");
		dateButton3.Caption = "今天";
		this.dpChgAgree.DateButtons.Add(dateButton3);
		this.dpChgAgree.DayOfWeekCaptionStyle = Infragistics.Win.UltraWinSchedule.DayOfWeekCaptionStyle.ShortDescription;
		this.dpChgAgree.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.dpChgAgree.Location = new System.Drawing.Point(534, 62);
		this.dpChgAgree.Name = "dpChgAgree";
		this.dpChgAgree.NonAutoSizeHeight = 21;
		this.dpChgAgree.Size = new System.Drawing.Size(228, 21);
		this.dpChgAgree.TabIndex = 36;
		this.dpChgAgree.TipStyle = Infragistics.Win.UltraWinSchedule.TipStyleDay.Holidays;
		this.dpChgAgree.Value = resources.GetObject("dpChgAgree.Value");
		appearance10.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance10.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel6.Appearance = appearance10;
		this.ultraLabel6.Location = new System.Drawing.Point(400, 33);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(130, 25);
		this.ultraLabel6.TabIndex = 1;
		this.ultraLabel6.Text = "變更後完工日期:";
		appearance11.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance11.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel7.Appearance = appearance11;
		this.ultraLabel7.Location = new System.Drawing.Point(400, 62);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(130, 25);
		this.ultraLabel7.TabIndex = 1;
		this.ultraLabel7.Text = "核可日期:";
		this.txtExtendDay.AutoSize = true;
		this.txtExtendDay.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.txtExtendDay.Location = new System.Drawing.Point(142, 62);
		this.txtExtendDay.Name = "txtExtendDay";
		this.txtExtendDay.Size = new System.Drawing.Size(254, 22);
		this.txtExtendDay.TabIndex = 0;
		this.txtExtendDay.Text = "txtExtendDay";
		appearance12.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel8.Appearance = appearance12;
		this.ultraLabel8.Location = new System.Drawing.Point(18, 118);
		this.ultraLabel8.Name = "ultraLabel8";
		this.ultraLabel8.Size = new System.Drawing.Size(120, 25);
		this.ultraLabel8.TabIndex = 1;
		this.ultraLabel8.Text = "主旨:";
		appearance13.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel9.Appearance = appearance13;
		this.ultraLabel9.Location = new System.Drawing.Point(18, 235);
		this.ultraLabel9.Name = "ultraLabel9";
		this.ultraLabel9.Size = new System.Drawing.Size(120, 25);
		this.ultraLabel9.TabIndex = 1;
		this.ultraLabel9.Text = "說明:";
		appearance14.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel10.Appearance = appearance14;
		this.ultraLabel10.Location = new System.Drawing.Point(18, 354);
		this.ultraLabel10.Name = "ultraLabel10";
		this.ultraLabel10.Size = new System.Drawing.Size(120, 25);
		this.ultraLabel10.TabIndex = 1;
		this.ultraLabel10.Text = "內容:";
		this.txtExplain.AutoSize = true;
		this.txtExplain.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.txtExplain.Location = new System.Drawing.Point(18, 264);
		this.txtExplain.MaxLength = 200;
		this.txtExplain.Multiline = true;
		this.txtExplain.Name = "txtExplain";
		this.txtExplain.Size = new System.Drawing.Size(744, 86);
		this.txtExplain.TabIndex = 38;
		this.txtExplain.Text = "txtExplain";
		this.txtContent.AutoSize = true;
		this.txtContent.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.txtContent.Location = new System.Drawing.Point(18, 383);
		this.txtContent.MaxLength = 200;
		this.txtContent.Multiline = true;
		this.txtContent.Name = "txtContent";
		this.txtContent.Size = new System.Drawing.Size(744, 73);
		this.txtContent.TabIndex = 38;
		this.txtContent.Text = "txtContent";
		this.txtContent.ValueChanged += new System.EventHandler(ultraTextEditor1_ValueChanged);
		appearance15.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance15.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel1.Appearance = appearance15;
		this.ultraLabel1.Location = new System.Drawing.Point(18, 460);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(120, 25);
		this.ultraLabel1.TabIndex = 1;
		this.ultraLabel1.Text = "前次變更契約總價:";
		appearance16.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance16.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel11.Appearance = appearance16;
		this.ultraLabel11.Location = new System.Drawing.Point(276, 460);
		this.ultraLabel11.Name = "ultraLabel11";
		this.ultraLabel11.Size = new System.Drawing.Size(120, 25);
		this.ultraLabel11.TabIndex = 1;
		this.ultraLabel11.Text = "變更後契約總價:";
		appearance17.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance17.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel12.Appearance = appearance17;
		this.ultraLabel12.Location = new System.Drawing.Point(534, 460);
		this.ultraLabel12.Name = "ultraLabel12";
		this.ultraLabel12.Size = new System.Drawing.Size(100, 25);
		this.ultraLabel12.TabIndex = 1;
		this.ultraLabel12.Text = "追加減金額:";
		appearance18.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblPreAmt.Appearance = appearance18;
		this.lblPreAmt.Location = new System.Drawing.Point(142, 460);
		this.lblPreAmt.Name = "lblPreAmt";
		this.lblPreAmt.Size = new System.Drawing.Size(130, 25);
		this.lblPreAmt.TabIndex = 37;
		this.lblPreAmt.Text = "123,456,789,012.00";
		appearance19.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblPostAmt.Appearance = appearance19;
		this.lblPostAmt.Location = new System.Drawing.Point(400, 460);
		this.lblPostAmt.Name = "lblPostAmt";
		this.lblPostAmt.Size = new System.Drawing.Size(130, 25);
		this.lblPostAmt.TabIndex = 37;
		this.lblPostAmt.Text = "123,456,789,012.00";
		appearance20.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblDiffer.Appearance = appearance20;
		this.lblDiffer.Location = new System.Drawing.Point(638, 460);
		this.lblDiffer.Name = "lblDiffer";
		this.lblDiffer.Size = new System.Drawing.Size(124, 25);
		this.lblDiffer.TabIndex = 37;
		this.lblDiffer.Text = "123,456,789.00";
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		this.BackColor = System.Drawing.Color.FromArgb(239, 243, 254);
		base.CancelButton = this.Btn_Cncl;
		base.ClientSize = new System.Drawing.Size(782, 563);
		base.Controls.Add(this.panel3);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel1);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.Name = "FormBudgetChange_Addnew";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "新增預算書變更";
		base.Load += new System.EventHandler(FormBudgetChange_Addnew_Load);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormBudgetChange_Addnew_FormClosing);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormBudgetChange_Addnew_KeyDown);
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.c1Sizer1).EndInit();
		this.c1Sizer1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.numericUpDown1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtKeyNote).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dpChgDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtChgTxtNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dpChgFinish).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dpChgAgree).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtExtendDay).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtExplain).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtContent).EndInit();
		base.ResumeLayout(false);
	}

	private void ultraTextEditor1_ValueChanged(object sender, EventArgs e)
	{
	}

	private void FormBudgetChange_Addnew_Load(object sender, EventArgs e)
	{
		ClearControls_Text();
		if (F_EditMode == "NEW")
		{
			lblFormCaption.Text = " 新增預算書變更 【" + F_ProjectCode + "】" + F_ProjectNameC;
		}
		else
		{
			lblFormCaption.Text = " 編輯預算書變更 【" + F_ProjectCode + "】" + F_ProjectNameC;
		}
		LoadData();
		LoadingScreen();
	}

	private void LoadingScreen()
	{
		string Status = CommonMethods.GetIniValue("Change_Addnew", "WindowState");
		if (Status == "Maximized")
		{
			base.WindowState = FormWindowState.Maximized;
		}
		int iLoc_X = PubTools.Str2Int(CommonMethods.GetIniValue("Change_Addnew", "PK_LocationX"));
		int iLoc_Y = PubTools.Str2Int(CommonMethods.GetIniValue("Change_Addnew", "PK_LocationY"));
		int iSiz_W = PubTools.Str2Int(CommonMethods.GetIniValue("Change_Addnew", "PK_Width"));
		int iSiz_H = PubTools.Str2Int(CommonMethods.GetIniValue("Change_Addnew", "PK_Height"));
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

	private void ClearControls_Text()
	{
		if (F_EditMode == "NEW")
		{
			c1Sizer1.Grid.Rows[c1Sizer1.Grid.Rows.Count - 1].Size = 0;
		}
		foreach (Control txtBox in c1Sizer1.Controls)
		{
			if (txtBox is UltraTextEditor)
			{
				(txtBox as UltraTextEditor).Text = "";
			}
		}
		dpChgAgree.Value = DateTime.Now;
		dpChgDate.Value = DateTime.Now;
		dpChgFinish.Value = DateTime.Now;
		txtExtendDay.Text = "0";
	}

	private void LoadData()
	{
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(LET_CHG_ADD) 新增預算變更主檔");
		sub_ChgMain chgcom = new sub_ChgMain(tmp_AL1);
		if (F_EditMode == "NEW")
		{
			int getMaxNo = chgcom.getMaxNo(F_ProjectCode, F_SubProjectCode);
			lblChgCount.Text = Convert.ToString(getMaxNo + 1);
			return;
		}
		lblChgCount.Text = F_ChgCount;
		DT1 = chgcom.ListItem(" chgCount=" + F_ChgCount + " ", F_ProjectCode, F_SubProjectCode);
		if (DT1.Rows.Count > 0)
		{
			txtChgTxtNo.Text = DT1.Rows[0]["chgTxtNo"].ToString().Trim();
			txtKeyNote.Text = DT1.Rows[0]["keyNote"].ToString().Trim();
			txtExplain.Text = DT1.Rows[0]["explain"].ToString().Trim();
			txtContent.Text = DT1.Rows[0]["content"].ToString().Trim();
			txtExtendDay.Text = DT1.Rows[0]["extendDay"].ToString().Trim();
			lblPreAmt.Text = string.Format("{0:N}", Convert.ToDouble(DT1.Rows[0]["preAmt"].ToString()));
			lblPostAmt.Text = string.Format("{0:N}", Convert.ToDouble(DT1.Rows[0]["postAmt"].ToString()));
			lblDiffer.Text = string.Format("{0:N}", PubTools.Str2Double(DT1.Rows[0]["postAmt"].ToString()) - PubTools.Str2Double(DT1.Rows[0]["preAmt"].ToString()));
			dpChgDate.Value = PubTools.Str2DateTime(DT1.Rows[0]["chgDate"].ToString());
			dpChgFinish.Value = PubTools.Str2DateTime(DT1.Rows[0]["chgFinish"].ToString());
			dpChgAgree.Value = PubTools.Str2DateTime(DT1.Rows[0]["chgAgree"].ToString());
		}
	}

	private void Btn_OK_Click(object sender, EventArgs e)
	{
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(LET_CHG_ADD) 新增預算變更主檔--確定新增");
		Archnowledge.Pcces.BUDClass.Project proj = new Archnowledge.Pcces.BUDClass.Project(tmp_AL1);
		proj.ps_projectCode = F_ProjectCode;
		proj.ps_srckind = "SUB";
		DataTable dt = proj.ListItem("", F_ProjectCode);
		if (CheckStringLength())
		{
			Cursor = Cursors.WaitCursor;
			sub_ChgMain chgcom = new sub_ChgMain(tmp_AL1);
			chgcom.ps_projectCode = F_ProjectCode;
			chgcom.ps_sproj = F_SubProjectCode;
			chgcom.ps_chgCount = lblChgCount.Text;
			chgcom.ps_chgAgree = $"{dpChgAgree.Value:yyyyMMdd}";
			chgcom.ps_chgDate = $"{dpChgDate.Value:yyyyMMdd}";
			chgcom.ps_chgFinish = $"{dpChgFinish.Value:yyyyMMdd}";
			chgcom.ps_chgTxtNo = txtChgTxtNo.Text;
			chgcom.ps_content = txtContent.Text;
			chgcom.ps_explain = txtExplain.Text;
			chgcom.ps_extendDay = txtExtendDay.Text;
			chgcom.ps_keynote = txtKeyNote.Text;
			chgcom.ps_postAmt = "0";
			chgcom.ps_preAmt = "0";
			if (dt.Rows.Count > 0)
			{
				chgcom.ps_mainCode = dt.Rows[0]["mainCode"].ToString();
				chgcom.ps_mainCName = dt.Rows[0]["mainCName"].ToString();
				chgcom.ps_projectNameC = dt.Rows[0]["projectNameC"].ToString();
				chgcom.ps_projectNameE = dt.Rows[0]["projectNameE"].ToString();
				chgcom.ps_projectAddress = dt.Rows[0]["projectAddress"].ToString();
			}
			if (F_EditMode == "NEW")
			{
				chgcom.InseItem();
			}
			else if (F_EditMode == "EDIT")
			{
				chgcom.UpdItem();
			}
			if (F_EditMode == "NEW")
			{
				chgcom.CreateDetail(F_ProjectCode, F_SubProjectCode);
			}
			Cursor = Cursors.Default;
		}
	}

	private bool CheckStringLength()
	{
		bool RetV = true;
		string sWarning = "";
		if (!Class1.IsValidStringLength(txtChgTxtNo.Text.Trim(), 50))
		{
			sWarning += "【變更文號輸入過長！！】\n";
		}
		if (!Class1.IsValidStringLength(txtContent.Text.Trim(), 200))
		{
			sWarning += "【內容輸入過長！！】\n";
		}
		if (!Class1.IsValidStringLength(txtExplain.Text.Trim(), 200))
		{
			sWarning += "【說明輸入過長！！】\n";
		}
		if (!Class1.IsValidStringLength(txtKeyNote.Text.Trim(), 200))
		{
			sWarning += "【主旨輸入過長！！】\n";
		}
		if (sWarning.Length > 0)
		{
			MessageBox.Show(this, sWarning, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			RetV = false;
		}
		return RetV;
	}

	private void FormBudgetChange_Addnew_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (base.WindowState != FormWindowState.Maximized)
		{
			CommonMethods.WriteIniValue("Change_Addnew", "PK_LocationX", base.Location.X.ToString());
			CommonMethods.WriteIniValue("Change_Addnew", "PK_LocationY", base.Location.Y.ToString());
			CommonMethods.WriteIniValue("Change_Addnew", "PK_Width", base.Size.Width.ToString());
			CommonMethods.WriteIniValue("Change_Addnew", "PK_Height", base.Size.Height.ToString());
		}
		CommonMethods.WriteIniValue("Change_Addnew", "WindowState", base.WindowState.ToString());
	}

	private void FormBudgetChange_Addnew_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormBudgetChange_Addnew");
		}
	}
}
