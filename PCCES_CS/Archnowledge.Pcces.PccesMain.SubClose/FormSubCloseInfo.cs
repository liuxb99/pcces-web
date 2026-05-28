using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.CTRClass;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.STDClass;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using C1.Win.C1Sizer;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinSchedule;
using Infragistics.Win.UltraWinSchedule.CalendarCombo;

namespace Archnowledge.Pcces.PccesMain.SubClose;

public class FormSubCloseInfo : Form
{
	private const string CallFormHelp = "FormSubCloseInfo";

	private Panel panel1;

	private UltraButton D_Btn_Fnsh;

	private GroupBox groupBox1;

	private UltraButton A_Btn_Cncl;

	private Panel PanelMain;

	private C1Sizer c1Sizer1;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	private UltraLabel ultraLabel3;

	private UltraLabel ultraLabel5;

	private UltraLabel ultraLabel6;

	private UltraLabel ultraLabel7;

	private UltraLabel ultraLabel8;

	private UltraLabel ultraLabel9;

	private UltraLabel ultraLabel10;

	private UltraLabel ultraLabel11;

	private UltraLabel ultraLabel12;

	private UltraLabel ultraLabel13;

	private UltraLabel ultraLabel14;

	private UltraLabel ultraLabel15;

	private UltraLabel lb_ProjectDesc;

	private UltraLabel ultraLabel16;

	private UltraLabel ultraLabel17;

	private UltraLabel lb_PorjectCode;

	private UltraLabel lb_ProjectCode1;

	private UltraLabel ultraLabel18;

	private UltraLabel ultraLabel19;

	private UltraLabel lb_Address;

	private UltraLabel lb_Sublet;

	private UltraLabel lb_MainName;

	private UltraCalendarCombo ad_BudStart;

	private UltraCalendarCombo ad_RealStart;

	private UltraLabel ultraLabel20;

	private UltraLabel ultraLabel21;

	private UltraCalendarCombo ad_InputDate;

	private UltraCalendarCombo ad_BudEnd;

	private UltraCalendarCombo ad_RealEnd;

	private UltraLabel lb_WorkName;

	private UltraLabel lb_WorkMode;

	private UltraLabel ultraLabel22;

	private UltraLabel ultraLabel23;

	private UltraLabel ultraLabel24;

	private UltraLabel ultraLabel25;

	private UltraLabel ultraLabel26;

	private UltraLabel ultraLabel27;

	private UltraLabel ultraLabel28;

	private UltraLabel ultraLabel4;

	private UltraLabel ultraLabel29;

	private UltraLabel lb_OverDays;

	private UltraLabel ultraLabel30;

	private UltraLabel lb_ProjAmt;

	private UltraComboEditor ddl_BidKind;

	private UltraTextEditor tb_CloseNo;

	private UltraLabel ultraLabel31;

	private UltraLabel ultraLabel32;

	private UltraTextEditor tb_Memo1;

	private UltraTextEditor tb_Memo2;

	private UltraLabel ultraLabel33;

	private UltraLabel ultraLabel34;

	private UltraLabel ultraLabel35;

	private UltraLabel ultraLabel36;

	private UltraLabel ultraLabel37;

	private UltraLabel lb_Amt;

	private UltraLabel ultraLabel38;

	private UltraLabel ultraLabel39;

	private UltraLabel lb_CloseAmt;

	private UltraLabel ultraLabel40;

	private UltraCalendarCombo ad_SCloseDate;

	private UltraCalendarCombo ad_CloseDate;

	private Panel panel2;

	public GridMrsBase Grid1;

	private UltraLabel ultraLabel41;

	private UltraTextEditor tb_Memo3;

	private Panel PNL_1;

	private Panel PNL_3;

	private Panel PNL_2;

	private UltraNumericEditor tb_WorkDays;

	private UltraNumericEditor tb_Days;

	private UltraNumericEditor tb_AllOverDays;

	private UltraNumericEditor tb_unOverDays;

	private UltraNumericEditor tb_OtherAmt;

	private UltraNumericEditor tb_OverAmt;

	private UltraNumericEditor tb_Deduct;

	private IContainer components;

	private string F_UserID;

	private string F_ProjectCode;

	private string F_SubProjectCode = "";

	private string ls_prjcode;

	private string ls_subproj;

	private string ls_Queue = "9999";

	private bool lb_Lock = false;

	private DataRow dr;

	private UltraLabel ultraLabel42;

	protected double ContractAmt = 0.0;

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

	public FormSubCloseInfo()
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
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.SubClose.FormSubCloseInfo));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton1 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
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
		Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton2 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
		Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton3 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
		Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton4 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
		Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton5 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
		Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance38 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance39 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance40 = new Infragistics.Win.Appearance();
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
		Infragistics.Win.Appearance appearance52 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance53 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance54 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance55 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton6 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
		Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton7 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
		Infragistics.Win.Appearance appearance56 = new Infragistics.Win.Appearance();
		this.panel1 = new System.Windows.Forms.Panel();
		this.D_Btn_Fnsh = new Infragistics.Win.Misc.UltraButton();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.A_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.PanelMain = new System.Windows.Forms.Panel();
		this.c1Sizer1 = new C1.Win.C1Sizer.C1Sizer();
		this.tb_WorkDays = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
		this.panel2 = new System.Windows.Forms.Panel();
		this.Grid1 = new Archnowledge.Pcces.PccesMain.ArchControls.GridMrsBase(this.components);
		this.ultraLabel42 = new Infragistics.Win.Misc.UltraLabel();
		this.ddl_BidKind = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.ad_BudStart = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel8 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel9 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel10 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel11 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel12 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel13 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel14 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel15 = new Infragistics.Win.Misc.UltraLabel();
		this.lb_ProjectDesc = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel16 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel17 = new Infragistics.Win.Misc.UltraLabel();
		this.lb_PorjectCode = new Infragistics.Win.Misc.UltraLabel();
		this.lb_ProjectCode1 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel18 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel19 = new Infragistics.Win.Misc.UltraLabel();
		this.lb_Address = new Infragistics.Win.Misc.UltraLabel();
		this.lb_Sublet = new Infragistics.Win.Misc.UltraLabel();
		this.lb_MainName = new Infragistics.Win.Misc.UltraLabel();
		this.ad_InputDate = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
		this.ad_RealStart = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
		this.ultraLabel20 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel21 = new Infragistics.Win.Misc.UltraLabel();
		this.ad_BudEnd = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
		this.ad_RealEnd = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
		this.lb_WorkName = new Infragistics.Win.Misc.UltraLabel();
		this.lb_WorkMode = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel22 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel23 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel24 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel25 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel26 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel27 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel28 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel29 = new Infragistics.Win.Misc.UltraLabel();
		this.lb_OverDays = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel30 = new Infragistics.Win.Misc.UltraLabel();
		this.lb_ProjAmt = new Infragistics.Win.Misc.UltraLabel();
		this.tb_CloseNo = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel31 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel32 = new Infragistics.Win.Misc.UltraLabel();
		this.tb_Memo1 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.tb_Memo2 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel33 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel34 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel35 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel36 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel37 = new Infragistics.Win.Misc.UltraLabel();
		this.lb_Amt = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel38 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel39 = new Infragistics.Win.Misc.UltraLabel();
		this.lb_CloseAmt = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel40 = new Infragistics.Win.Misc.UltraLabel();
		this.ad_SCloseDate = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
		this.ad_CloseDate = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
		this.ultraLabel41 = new Infragistics.Win.Misc.UltraLabel();
		this.tb_Memo3 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.PNL_1 = new System.Windows.Forms.Panel();
		this.tb_Days = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
		this.tb_AllOverDays = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
		this.tb_unOverDays = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
		this.tb_OtherAmt = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
		this.tb_OverAmt = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
		this.tb_Deduct = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
		this.PNL_2 = new System.Windows.Forms.Panel();
		this.PNL_3 = new System.Windows.Forms.Panel();
		this.panel1.SuspendLayout();
		this.PanelMain.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.c1Sizer1).BeginInit();
		this.c1Sizer1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.tb_WorkDays).BeginInit();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Grid1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ddl_BidKind).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ad_BudStart).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ad_InputDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ad_RealStart).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ad_BudEnd).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ad_RealEnd).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tb_CloseNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tb_Memo1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tb_Memo2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ad_SCloseDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ad_CloseDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tb_Memo3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tb_Days).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tb_AllOverDays).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tb_unOverDays).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tb_OtherAmt).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tb_OverAmt).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tb_Deduct).BeginInit();
		base.SuspendLayout();
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.Controls.Add(this.D_Btn_Fnsh);
		this.panel1.Controls.Add(this.groupBox1);
		this.panel1.Controls.Add(this.A_Btn_Cncl);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 533);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(792, 40);
		this.panel1.TabIndex = 11;
		this.D_Btn_Fnsh.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Fnsh.Appearance = appearance1;
		this.D_Btn_Fnsh.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Fnsh.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.D_Btn_Fnsh.Font = new System.Drawing.Font("細明體", 11f);
		this.D_Btn_Fnsh.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Fnsh.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Fnsh.Location = new System.Drawing.Point(601, 7);
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
		this.groupBox1.Size = new System.Drawing.Size(792, 8);
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
		this.A_Btn_Cncl.Location = new System.Drawing.Point(692, 7);
		this.A_Btn_Cncl.Name = "A_Btn_Cncl";
		this.A_Btn_Cncl.ShowFocusRect = false;
		this.A_Btn_Cncl.ShowOutline = false;
		this.A_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.A_Btn_Cncl.SupportThemes = false;
		this.A_Btn_Cncl.TabIndex = 2;
		this.A_Btn_Cncl.Text = "取消";
		this.PanelMain.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.PanelMain.Controls.Add(this.c1Sizer1);
		this.PanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
		this.PanelMain.Location = new System.Drawing.Point(0, 0);
		this.PanelMain.Name = "PanelMain";
		this.PanelMain.Size = new System.Drawing.Size(792, 533);
		this.PanelMain.TabIndex = 12;
		this.c1Sizer1.AllowDrop = true;
		this.c1Sizer1.Controls.Add(this.tb_WorkDays);
		this.c1Sizer1.Controls.Add(this.panel2);
		this.c1Sizer1.Controls.Add(this.ddl_BidKind);
		this.c1Sizer1.Controls.Add(this.ad_BudStart);
		this.c1Sizer1.Controls.Add(this.ultraLabel1);
		this.c1Sizer1.Controls.Add(this.ultraLabel2);
		this.c1Sizer1.Controls.Add(this.ultraLabel3);
		this.c1Sizer1.Controls.Add(this.ultraLabel5);
		this.c1Sizer1.Controls.Add(this.ultraLabel6);
		this.c1Sizer1.Controls.Add(this.ultraLabel7);
		this.c1Sizer1.Controls.Add(this.ultraLabel8);
		this.c1Sizer1.Controls.Add(this.ultraLabel9);
		this.c1Sizer1.Controls.Add(this.ultraLabel10);
		this.c1Sizer1.Controls.Add(this.ultraLabel11);
		this.c1Sizer1.Controls.Add(this.ultraLabel12);
		this.c1Sizer1.Controls.Add(this.ultraLabel13);
		this.c1Sizer1.Controls.Add(this.ultraLabel14);
		this.c1Sizer1.Controls.Add(this.ultraLabel15);
		this.c1Sizer1.Controls.Add(this.lb_ProjectDesc);
		this.c1Sizer1.Controls.Add(this.ultraLabel16);
		this.c1Sizer1.Controls.Add(this.ultraLabel17);
		this.c1Sizer1.Controls.Add(this.lb_PorjectCode);
		this.c1Sizer1.Controls.Add(this.lb_ProjectCode1);
		this.c1Sizer1.Controls.Add(this.ultraLabel18);
		this.c1Sizer1.Controls.Add(this.ultraLabel19);
		this.c1Sizer1.Controls.Add(this.lb_Address);
		this.c1Sizer1.Controls.Add(this.lb_Sublet);
		this.c1Sizer1.Controls.Add(this.lb_MainName);
		this.c1Sizer1.Controls.Add(this.ad_InputDate);
		this.c1Sizer1.Controls.Add(this.ad_RealStart);
		this.c1Sizer1.Controls.Add(this.ultraLabel20);
		this.c1Sizer1.Controls.Add(this.ultraLabel21);
		this.c1Sizer1.Controls.Add(this.ad_BudEnd);
		this.c1Sizer1.Controls.Add(this.ad_RealEnd);
		this.c1Sizer1.Controls.Add(this.lb_WorkName);
		this.c1Sizer1.Controls.Add(this.lb_WorkMode);
		this.c1Sizer1.Controls.Add(this.ultraLabel22);
		this.c1Sizer1.Controls.Add(this.ultraLabel23);
		this.c1Sizer1.Controls.Add(this.ultraLabel24);
		this.c1Sizer1.Controls.Add(this.ultraLabel25);
		this.c1Sizer1.Controls.Add(this.ultraLabel26);
		this.c1Sizer1.Controls.Add(this.ultraLabel27);
		this.c1Sizer1.Controls.Add(this.ultraLabel28);
		this.c1Sizer1.Controls.Add(this.ultraLabel4);
		this.c1Sizer1.Controls.Add(this.ultraLabel29);
		this.c1Sizer1.Controls.Add(this.lb_OverDays);
		this.c1Sizer1.Controls.Add(this.ultraLabel30);
		this.c1Sizer1.Controls.Add(this.lb_ProjAmt);
		this.c1Sizer1.Controls.Add(this.tb_CloseNo);
		this.c1Sizer1.Controls.Add(this.ultraLabel31);
		this.c1Sizer1.Controls.Add(this.ultraLabel32);
		this.c1Sizer1.Controls.Add(this.tb_Memo1);
		this.c1Sizer1.Controls.Add(this.tb_Memo2);
		this.c1Sizer1.Controls.Add(this.ultraLabel33);
		this.c1Sizer1.Controls.Add(this.ultraLabel34);
		this.c1Sizer1.Controls.Add(this.ultraLabel35);
		this.c1Sizer1.Controls.Add(this.ultraLabel36);
		this.c1Sizer1.Controls.Add(this.ultraLabel37);
		this.c1Sizer1.Controls.Add(this.lb_Amt);
		this.c1Sizer1.Controls.Add(this.ultraLabel38);
		this.c1Sizer1.Controls.Add(this.ultraLabel39);
		this.c1Sizer1.Controls.Add(this.lb_CloseAmt);
		this.c1Sizer1.Controls.Add(this.ultraLabel40);
		this.c1Sizer1.Controls.Add(this.ad_SCloseDate);
		this.c1Sizer1.Controls.Add(this.ad_CloseDate);
		this.c1Sizer1.Controls.Add(this.ultraLabel41);
		this.c1Sizer1.Controls.Add(this.tb_Memo3);
		this.c1Sizer1.Controls.Add(this.PNL_1);
		this.c1Sizer1.Controls.Add(this.tb_Days);
		this.c1Sizer1.Controls.Add(this.tb_AllOverDays);
		this.c1Sizer1.Controls.Add(this.tb_unOverDays);
		this.c1Sizer1.Controls.Add(this.tb_OtherAmt);
		this.c1Sizer1.Controls.Add(this.tb_OverAmt);
		this.c1Sizer1.Controls.Add(this.tb_Deduct);
		this.c1Sizer1.Controls.Add(this.PNL_2);
		this.c1Sizer1.Controls.Add(this.PNL_3);
		this.c1Sizer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.c1Sizer1.GridDefinition = "0.75046904315197:False:False;4.31519699812383:False:True;4.31519699812383:False:True;4.31519699812383:False:True;4.31519699812383:False:True;4.31519699812383:False:True;4.31519699812383:False:True;4.31519699812383:False:True;4.31519699812383:False:True;4.31519699812383:False:True;4.31519699812383:False:True;4.31519699812383:False:True;4.31519699812383:False:True;4.31519699812383:False:True;4.31519699812383:False:True;4.31519699812383:False:True;4.31519699812383:False:True;4.31519699812383:False:True;4.31519699812383:False:True;4.31519699812383:False:True;0.75046904315197:False:False;\t0.378787878787879:False:True;17.0454545454545:False:True;13.8888888888889:False:False;2.9040404040404:False:False;14.0151515151515:False:False;13.2575757575758:False:False;2.27272727272727:False:True;13.6363636363636:False:False;13.8888888888889:False:False;2.27272727272727:False:True;0.378787878787879:False:True;";
		this.c1Sizer1.Location = new System.Drawing.Point(0, 0);
		this.c1Sizer1.Name = "c1Sizer1";
		this.c1Sizer1.Size = new System.Drawing.Size(792, 533);
		this.c1Sizer1.TabIndex = 0;
		this.c1Sizer1.TabStop = false;
		this.tb_WorkDays.FormatString = "###,###,###,##0";
		this.tb_WorkDays.Location = new System.Drawing.Point(150, 228);
		this.tb_WorkDays.Name = "tb_WorkDays";
		this.tb_WorkDays.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
		this.tb_WorkDays.PromptChar = ' ';
		this.tb_WorkDays.Size = new System.Drawing.Size(110, 21);
		this.tb_WorkDays.TabIndex = 43;
		this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.panel2.Controls.Add(this.Grid1);
		this.panel2.Controls.Add(this.ultraLabel42);
		this.panel2.Location = new System.Drawing.Point(11, 363);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(522, 131);
		this.panel2.TabIndex = 39;
		this.Grid1._ExcelFileName = "";
		this.Grid1._ExcelSheeName = "";
		this.Grid1._IsOpenExcelAfterExport = false;
		this.Grid1.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
		this.Grid1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
		this.Grid1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.Grid1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
		this.Grid1.ColumnInfo = "6,1,0,0,0,110,Columns:0{Width:17;Name:\"RowIndicator\";AllowDragging:False;AllowResizing:False;AllowEditing:False;DataType:System.Int32;TextAlign:RightTop;TextAlignFixed:GeneralTop;}\t1{Name:\"RptZIP\";Caption:\"檔案名稱\";Visible:False;DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t2{Name:\"ChgCount\";Caption:\"變更次別\";DataType:System.Int32;TextAlign:RightCenter;TextAlignFixed:GeneralTop;}\t3{Width:180;Name:\"ChgTxtNo\";Caption:\"變更文號\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t4{Name:\"ChgAmt\";Caption:\"變更增減金額\";DataType:System.Decimal;Format:\"###,###,###,##0\";TextAlign:GeneralBottom;TextAlignFixed:GeneralTop;}\t5{Width:300;Name:\"RptURL\";Caption:\"網頁路徑\";Visible:False;DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t";
		this.Grid1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Grid1.ExtendLastCol = true;
		this.Grid1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.Grid1.ForeColor = System.Drawing.Color.Black;
		this.Grid1.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
		this.Grid1.IsProcessUndo = false;
		this.Grid1.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
		this.Grid1.Location = new System.Drawing.Point(0, 23);
		this.Grid1.Name = "Grid1";
		this.Grid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.Grid1.ShowCursor = true;
		this.Grid1.ShowToolTipOnNarrowColumn = true;
		this.Grid1.Size = new System.Drawing.Size(518, 104);
		this.Grid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection("Normal{Font:細明體, 11.25pt;BackColor:237, 243, 254;ForeColor:Black;TextAlign:GeneralBottom;Border:Flat,1,Silver,Both;}\tAlternate{BackColor:White;}\tFixed{BackColor:225, 247, 223;TextAlign:GeneralTop;Border:Raised,1,Black,Both;}\tHighlight{BackColor:102, 153, 255;TextAlign:GeneralCenter;ImageAlign:CenterCenter;Border:None,1,Black,Both;Format:\"###,###,###,##0\";}\tFocus{Font:細明體, 10pt, style=Bold;BackColor:102, 153, 255;Margins:0, 0, 0, 0;TextAlign:GeneralCenter;Border:Double,1,102, 153, 255,Both;}\tSearch{BackColor:Highlight;ForeColor:HighlightText;}\tFrozen{BackColor:Beige;}\tEmptyArea{BackColor:232, 232, 232;Border:Flat,1,ControlDarkDark,Both;}\tGrandTotal{BackColor:Black;ForeColor:White;}\tSubtotal0{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal1{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal2{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal3{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal4{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal5{BackColor:ControlDarkDark;ForeColor:White;}\t");
		this.Grid1.TabIndex = 26;
		this.Grid1.UndoMax = 10;
		appearance3.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel42.Appearance = appearance3;
		this.ultraLabel42.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.ultraLabel42.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.ultraLabel42.Dock = System.Windows.Forms.DockStyle.Top;
		this.ultraLabel42.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel42.Name = "ultraLabel42";
		this.ultraLabel42.Size = new System.Drawing.Size(518, 23);
		this.ultraLabel42.TabIndex = 27;
		this.ultraLabel42.Text = "契約變更列表";
		valueListItem1.DataValue = "未達公告金額";
		valueListItem1.DisplayText = "未達公告金額";
		valueListItem2.DataValue = "公告金額以上";
		valueListItem2.DisplayText = "公告金額以上";
		this.ddl_BidKind.Items.Add(valueListItem1);
		this.ddl_BidKind.Items.Add(valueListItem2);
		this.ddl_BidKind.Location = new System.Drawing.Point(649, 201);
		this.ddl_BidKind.Name = "ddl_BidKind";
		this.ddl_BidKind.Size = new System.Drawing.Size(132, 21);
		this.ddl_BidKind.TabIndex = 38;
		this.ddl_BidKind.Text = "未達公告金額";
		dateButton1.Caption = "今天";
		this.ad_BudStart.DateButtons.Add(dateButton1);
		this.ad_BudStart.DayOfWeekCaptionStyle = Infragistics.Win.UltraWinSchedule.DayOfWeekCaptionStyle.ShortDescription;
		this.ad_BudStart.Location = new System.Drawing.Point(150, 174);
		this.ad_BudStart.Name = "ad_BudStart";
		this.ad_BudStart.NonAutoSizeHeight = 21;
		this.ad_BudStart.NullDateLabel = "";
		this.ad_BudStart.Size = new System.Drawing.Size(137, 21);
		this.ad_BudStart.TabIndex = 36;
		this.ad_BudStart.TipStyle = Infragistics.Win.UltraWinSchedule.TipStyleDay.Holidays;
		this.ad_BudStart.Value = resources.GetObject("ad_BudStart.Value");
		this.ad_BudStart.WeekNumbersVisible = true;
		appearance4.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance4.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel1.Appearance = appearance4;
		this.ultraLabel1.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.ultraLabel1.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel1.Location = new System.Drawing.Point(11, 336);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(135, 23);
		this.ultraLabel1.TabIndex = 0;
		this.ultraLabel1.Text = "逾期天數及罰款金額:";
		appearance5.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel2.Appearance = appearance5;
		this.ultraLabel2.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.ultraLabel2.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel2.Location = new System.Drawing.Point(264, 255);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(23, 23);
		this.ultraLabel2.TabIndex = 0;
		this.ultraLabel2.Text = "天";
		appearance6.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel3.Appearance = appearance6;
		this.ultraLabel3.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.ultraLabel3.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel3.Location = new System.Drawing.Point(515, 255);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(18, 23);
		this.ultraLabel3.TabIndex = 0;
		this.ultraLabel3.Text = "天";
		appearance7.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance7.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel5.Appearance = appearance7;
		this.ultraLabel5.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
		this.ultraLabel5.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel5.Location = new System.Drawing.Point(11, 12);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(135, 23);
		this.ultraLabel5.TabIndex = 0;
		this.ultraLabel5.Text = "契約名稱:";
		appearance8.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance8.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel6.Appearance = appearance8;
		this.ultraLabel6.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
		this.ultraLabel6.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel6.Location = new System.Drawing.Point(11, 39);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(135, 23);
		this.ultraLabel6.TabIndex = 0;
		this.ultraLabel6.Text = "契約編號:";
		appearance9.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance9.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel7.Appearance = appearance9;
		this.ultraLabel7.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
		this.ultraLabel7.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel7.Location = new System.Drawing.Point(11, 66);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(135, 23);
		this.ultraLabel7.TabIndex = 0;
		this.ultraLabel7.Text = "工程編號:";
		appearance10.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance10.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel8.Appearance = appearance10;
		this.ultraLabel8.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.ultraLabel8.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel8.Location = new System.Drawing.Point(11, 93);
		this.ultraLabel8.Name = "ultraLabel8";
		this.ultraLabel8.Size = new System.Drawing.Size(135, 23);
		this.ultraLabel8.TabIndex = 0;
		this.ultraLabel8.Text = "主辦單位:";
		appearance11.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance11.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel9.Appearance = appearance11;
		this.ultraLabel9.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.ultraLabel9.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel9.Location = new System.Drawing.Point(11, 120);
		this.ultraLabel9.Name = "ultraLabel9";
		this.ultraLabel9.Size = new System.Drawing.Size(135, 23);
		this.ultraLabel9.TabIndex = 0;
		this.ultraLabel9.Text = "承包廠商:";
		appearance12.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance12.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel10.Appearance = appearance12;
		this.ultraLabel10.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.ultraLabel10.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel10.Location = new System.Drawing.Point(11, 147);
		this.ultraLabel10.Name = "ultraLabel10";
		this.ultraLabel10.Size = new System.Drawing.Size(135, 23);
		this.ultraLabel10.TabIndex = 0;
		this.ultraLabel10.Text = "施工地點:";
		appearance13.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance13.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel11.Appearance = appearance13;
		this.ultraLabel11.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.ultraLabel11.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel11.Location = new System.Drawing.Point(11, 174);
		this.ultraLabel11.Name = "ultraLabel11";
		this.ultraLabel11.Size = new System.Drawing.Size(135, 23);
		this.ultraLabel11.TabIndex = 0;
		this.ultraLabel11.Text = "預定開工日:";
		appearance14.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance14.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel12.Appearance = appearance14;
		this.ultraLabel12.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.ultraLabel12.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel12.Location = new System.Drawing.Point(11, 201);
		this.ultraLabel12.Name = "ultraLabel12";
		this.ultraLabel12.Size = new System.Drawing.Size(135, 23);
		this.ultraLabel12.TabIndex = 0;
		this.ultraLabel12.Text = "實際開工日:";
		appearance15.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance15.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel13.Appearance = appearance15;
		this.ultraLabel13.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.ultraLabel13.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel13.Location = new System.Drawing.Point(11, 228);
		this.ultraLabel13.Name = "ultraLabel13";
		this.ultraLabel13.Size = new System.Drawing.Size(135, 23);
		this.ultraLabel13.TabIndex = 0;
		this.ultraLabel13.Text = "履約期限:";
		appearance16.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance16.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel14.Appearance = appearance16;
		this.ultraLabel14.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.ultraLabel14.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel14.Location = new System.Drawing.Point(11, 255);
		this.ultraLabel14.Name = "ultraLabel14";
		this.ultraLabel14.Size = new System.Drawing.Size(135, 23);
		this.ultraLabel14.TabIndex = 0;
		this.ultraLabel14.Text = "不(免)計入工期天數:";
		appearance17.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance17.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel15.Appearance = appearance17;
		this.ultraLabel15.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.ultraLabel15.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel15.Location = new System.Drawing.Point(11, 309);
		this.ultraLabel15.Name = "ultraLabel15";
		this.ultraLabel15.Size = new System.Drawing.Size(135, 23);
		this.ultraLabel15.TabIndex = 0;
		this.ultraLabel15.Text = "准延天數及核准文號:";
		appearance18.ForeColor = System.Drawing.Color.Blue;
		appearance18.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance18.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lb_ProjectDesc.Appearance = appearance18;
		this.lb_ProjectDesc.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
		this.lb_ProjectDesc.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lb_ProjectDesc.Location = new System.Drawing.Point(150, 12);
		this.lb_ProjectDesc.Name = "lb_ProjectDesc";
		this.lb_ProjectDesc.Size = new System.Drawing.Size(631, 23);
		this.lb_ProjectDesc.TabIndex = 0;
		this.lb_ProjectDesc.Text = "[lb_ProjectDesc]";
		appearance19.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance19.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel16.Appearance = appearance19;
		this.ultraLabel16.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
		this.ultraLabel16.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel16.Location = new System.Drawing.Point(406, 39);
		this.ultraLabel16.Name = "ultraLabel16";
		this.ultraLabel16.Size = new System.Drawing.Size(127, 23);
		this.ultraLabel16.TabIndex = 0;
		this.ultraLabel16.Text = "結算文號:";
		appearance20.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance20.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel17.Appearance = appearance20;
		this.ultraLabel17.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
		this.ultraLabel17.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel17.Location = new System.Drawing.Point(406, 66);
		this.ultraLabel17.Name = "ultraLabel17";
		this.ultraLabel17.Size = new System.Drawing.Size(127, 23);
		this.ultraLabel17.TabIndex = 0;
		this.ultraLabel17.Text = "結算填報日期:";
		appearance21.ForeColor = System.Drawing.Color.Blue;
		appearance21.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance21.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lb_PorjectCode.Appearance = appearance21;
		this.lb_PorjectCode.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
		this.lb_PorjectCode.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lb_PorjectCode.Location = new System.Drawing.Point(150, 39);
		this.lb_PorjectCode.Name = "lb_PorjectCode";
		this.lb_PorjectCode.Size = new System.Drawing.Size(252, 23);
		this.lb_PorjectCode.TabIndex = 0;
		this.lb_PorjectCode.Text = "[lb_PorjectCode]";
		appearance22.ForeColor = System.Drawing.Color.Blue;
		appearance22.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance22.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lb_ProjectCode1.Appearance = appearance22;
		this.lb_ProjectCode1.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
		this.lb_ProjectCode1.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lb_ProjectCode1.Location = new System.Drawing.Point(150, 66);
		this.lb_ProjectCode1.Name = "lb_ProjectCode1";
		this.lb_ProjectCode1.Size = new System.Drawing.Size(252, 23);
		this.lb_ProjectCode1.TabIndex = 0;
		this.lb_ProjectCode1.Text = "[lb_ProjectCode1]";
		appearance23.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance23.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel18.Appearance = appearance23;
		this.ultraLabel18.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.ultraLabel18.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel18.Location = new System.Drawing.Point(406, 93);
		this.ultraLabel18.Name = "ultraLabel18";
		this.ultraLabel18.Size = new System.Drawing.Size(127, 23);
		this.ultraLabel18.TabIndex = 0;
		this.ultraLabel18.Text = "監造單位:";
		appearance24.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance24.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel19.Appearance = appearance24;
		this.ultraLabel19.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.ultraLabel19.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel19.Location = new System.Drawing.Point(406, 120);
		this.ultraLabel19.Name = "ultraLabel19";
		this.ultraLabel19.Size = new System.Drawing.Size(127, 23);
		this.ultraLabel19.TabIndex = 0;
		this.ultraLabel19.Text = "施工方法:";
		appearance25.ForeColor = System.Drawing.Color.Blue;
		appearance25.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance25.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lb_Address.Appearance = appearance25;
		this.lb_Address.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.lb_Address.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lb_Address.Location = new System.Drawing.Point(150, 147);
		this.lb_Address.Name = "lb_Address";
		this.lb_Address.Size = new System.Drawing.Size(631, 23);
		this.lb_Address.TabIndex = 0;
		this.lb_Address.Text = "[lb_Address]";
		appearance26.ForeColor = System.Drawing.Color.Blue;
		appearance26.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance26.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lb_Sublet.Appearance = appearance26;
		this.lb_Sublet.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.lb_Sublet.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lb_Sublet.Location = new System.Drawing.Point(150, 120);
		this.lb_Sublet.Name = "lb_Sublet";
		this.lb_Sublet.Size = new System.Drawing.Size(252, 23);
		this.lb_Sublet.TabIndex = 0;
		this.lb_Sublet.Text = "[lb_Sublet]";
		appearance27.ForeColor = System.Drawing.Color.Blue;
		appearance27.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance27.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lb_MainName.Appearance = appearance27;
		this.lb_MainName.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.lb_MainName.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lb_MainName.Location = new System.Drawing.Point(150, 93);
		this.lb_MainName.Name = "lb_MainName";
		this.lb_MainName.Size = new System.Drawing.Size(252, 23);
		this.lb_MainName.TabIndex = 0;
		this.lb_MainName.Text = "[lb_MainName]";
		dateButton2.Caption = "今天";
		this.ad_InputDate.DateButtons.Add(dateButton2);
		this.ad_InputDate.DayOfWeekCaptionStyle = Infragistics.Win.UltraWinSchedule.DayOfWeekCaptionStyle.ShortDescription;
		this.ad_InputDate.Location = new System.Drawing.Point(537, 66);
		this.ad_InputDate.Name = "ad_InputDate";
		this.ad_InputDate.NonAutoSizeHeight = 21;
		this.ad_InputDate.NullDateLabel = "";
		this.ad_InputDate.Size = new System.Drawing.Size(244, 21);
		this.ad_InputDate.TabIndex = 36;
		this.ad_InputDate.TipStyle = Infragistics.Win.UltraWinSchedule.TipStyleDay.Holidays;
		this.ad_InputDate.Value = resources.GetObject("ad_InputDate.Value");
		this.ad_InputDate.WeekNumbersVisible = true;
		dateButton3.Caption = "今天";
		this.ad_RealStart.DateButtons.Add(dateButton3);
		this.ad_RealStart.DayOfWeekCaptionStyle = Infragistics.Win.UltraWinSchedule.DayOfWeekCaptionStyle.ShortDescription;
		this.ad_RealStart.Location = new System.Drawing.Point(150, 201);
		this.ad_RealStart.Name = "ad_RealStart";
		this.ad_RealStart.NonAutoSizeHeight = 21;
		this.ad_RealStart.NullDateLabel = "";
		this.ad_RealStart.Size = new System.Drawing.Size(137, 21);
		this.ad_RealStart.TabIndex = 36;
		this.ad_RealStart.TipStyle = Infragistics.Win.UltraWinSchedule.TipStyleDay.Holidays;
		this.ad_RealStart.Value = resources.GetObject("ad_RealStart.Value");
		this.ad_RealStart.WeekNumbersVisible = true;
		appearance28.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance28.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel20.Appearance = appearance28;
		this.ultraLabel20.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.ultraLabel20.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel20.Location = new System.Drawing.Point(291, 174);
		this.ultraLabel20.Name = "ultraLabel20";
		this.ultraLabel20.Size = new System.Drawing.Size(111, 23);
		this.ultraLabel20.TabIndex = 0;
		this.ultraLabel20.Text = "預定完工日:";
		appearance29.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance29.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel21.Appearance = appearance29;
		this.ultraLabel21.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.ultraLabel21.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel21.Location = new System.Drawing.Point(291, 201);
		this.ultraLabel21.Name = "ultraLabel21";
		this.ultraLabel21.Size = new System.Drawing.Size(111, 23);
		this.ultraLabel21.TabIndex = 0;
		this.ultraLabel21.Text = "實際完工日:";
		dateButton4.Caption = "今天";
		this.ad_BudEnd.DateButtons.Add(dateButton4);
		this.ad_BudEnd.DayOfWeekCaptionStyle = Infragistics.Win.UltraWinSchedule.DayOfWeekCaptionStyle.ShortDescription;
		this.ad_BudEnd.Location = new System.Drawing.Point(406, 174);
		this.ad_BudEnd.Name = "ad_BudEnd";
		this.ad_BudEnd.NonAutoSizeHeight = 21;
		this.ad_BudEnd.NullDateLabel = "";
		this.ad_BudEnd.Size = new System.Drawing.Size(127, 21);
		this.ad_BudEnd.TabIndex = 36;
		this.ad_BudEnd.TipStyle = Infragistics.Win.UltraWinSchedule.TipStyleDay.Holidays;
		this.ad_BudEnd.Value = resources.GetObject("ad_BudEnd.Value");
		this.ad_BudEnd.WeekNumbersVisible = true;
		dateButton5.Caption = "今天";
		this.ad_RealEnd.DateButtons.Add(dateButton5);
		this.ad_RealEnd.DayOfWeekCaptionStyle = Infragistics.Win.UltraWinSchedule.DayOfWeekCaptionStyle.ShortDescription;
		this.ad_RealEnd.Location = new System.Drawing.Point(406, 201);
		this.ad_RealEnd.Name = "ad_RealEnd";
		this.ad_RealEnd.NonAutoSizeHeight = 21;
		this.ad_RealEnd.NullDateLabel = "";
		this.ad_RealEnd.Size = new System.Drawing.Size(127, 21);
		this.ad_RealEnd.TabIndex = 36;
		this.ad_RealEnd.TipStyle = Infragistics.Win.UltraWinSchedule.TipStyleDay.Holidays;
		this.ad_RealEnd.Value = resources.GetObject("ad_RealEnd.Value");
		this.ad_RealEnd.WeekNumbersVisible = true;
		appearance30.ForeColor = System.Drawing.Color.Blue;
		appearance30.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance30.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lb_WorkName.Appearance = appearance30;
		this.lb_WorkName.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.lb_WorkName.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lb_WorkName.Location = new System.Drawing.Point(537, 93);
		this.lb_WorkName.Name = "lb_WorkName";
		this.lb_WorkName.Size = new System.Drawing.Size(244, 23);
		this.lb_WorkName.TabIndex = 0;
		this.lb_WorkName.Text = "[lb_WorkName]";
		appearance31.ForeColor = System.Drawing.Color.Blue;
		appearance31.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance31.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lb_WorkMode.Appearance = appearance31;
		this.lb_WorkMode.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.lb_WorkMode.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lb_WorkMode.Location = new System.Drawing.Point(537, 120);
		this.lb_WorkMode.Name = "lb_WorkMode";
		this.lb_WorkMode.Size = new System.Drawing.Size(244, 23);
		this.lb_WorkMode.TabIndex = 0;
		this.lb_WorkMode.Text = "[lb_WorkMode]";
		appearance32.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance32.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel22.Appearance = appearance32;
		this.ultraLabel22.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.ultraLabel22.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel22.Location = new System.Drawing.Point(537, 174);
		this.ultraLabel22.Name = "ultraLabel22";
		this.ultraLabel22.Size = new System.Drawing.Size(108, 23);
		this.ultraLabel22.TabIndex = 0;
		this.ultraLabel22.Text = "契約總價:";
		appearance33.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance33.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel23.Appearance = appearance33;
		this.ultraLabel23.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.ultraLabel23.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel23.Location = new System.Drawing.Point(537, 201);
		this.ultraLabel23.Name = "ultraLabel23";
		this.ultraLabel23.Size = new System.Drawing.Size(108, 23);
		this.ultraLabel23.TabIndex = 0;
		this.ultraLabel23.Text = "採購金額:";
		appearance34.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel24.Appearance = appearance34;
		this.ultraLabel24.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.ultraLabel24.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel24.Location = new System.Drawing.Point(264, 228);
		this.ultraLabel24.Name = "ultraLabel24";
		this.ultraLabel24.Size = new System.Drawing.Size(23, 23);
		this.ultraLabel24.TabIndex = 0;
		this.ultraLabel24.Text = "天";
		appearance35.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance35.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel25.Appearance = appearance35;
		this.ultraLabel25.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.ultraLabel25.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel25.Location = new System.Drawing.Point(291, 228);
		this.ultraLabel25.Name = "ultraLabel25";
		this.ultraLabel25.Size = new System.Drawing.Size(111, 23);
		this.ultraLabel25.TabIndex = 0;
		this.ultraLabel25.Text = "履約逾期總天數:";
		appearance36.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance36.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel26.Appearance = appearance36;
		this.ultraLabel26.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.ultraLabel26.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel26.Location = new System.Drawing.Point(291, 255);
		this.ultraLabel26.Name = "ultraLabel26";
		this.ultraLabel26.Size = new System.Drawing.Size(111, 23);
		this.ultraLabel26.TabIndex = 0;
		this.ultraLabel26.Text = "不計違約金天數:";
		appearance37.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel27.Appearance = appearance37;
		this.ultraLabel27.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.ultraLabel27.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel27.Location = new System.Drawing.Point(515, 228);
		this.ultraLabel27.Name = "ultraLabel27";
		this.ultraLabel27.Size = new System.Drawing.Size(18, 23);
		this.ultraLabel27.TabIndex = 0;
		this.ultraLabel27.Text = "天";
		appearance38.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance38.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel28.Appearance = appearance38;
		this.ultraLabel28.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.ultraLabel28.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel28.Location = new System.Drawing.Point(537, 255);
		this.ultraLabel28.Name = "ultraLabel28";
		this.ultraLabel28.Size = new System.Drawing.Size(108, 23);
		this.ultraLabel28.TabIndex = 0;
		this.ultraLabel28.Text = "其他違約金:";
		appearance39.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance39.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel4.Appearance = appearance39;
		this.ultraLabel4.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.ultraLabel4.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel4.Location = new System.Drawing.Point(291, 282);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(111, 23);
		this.ultraLabel4.TabIndex = 0;
		this.ultraLabel4.Text = "應計違約金天數:";
		appearance40.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel29.Appearance = appearance40;
		this.ultraLabel29.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.ultraLabel29.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel29.Location = new System.Drawing.Point(515, 282);
		this.ultraLabel29.Name = "ultraLabel29";
		this.ultraLabel29.Size = new System.Drawing.Size(18, 23);
		this.ultraLabel29.TabIndex = 0;
		this.ultraLabel29.Text = "天";
		appearance41.ForeColor = System.Drawing.Color.Blue;
		appearance41.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance41.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lb_OverDays.Appearance = appearance41;
		this.lb_OverDays.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.lb_OverDays.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lb_OverDays.Location = new System.Drawing.Point(406, 282);
		this.lb_OverDays.Name = "lb_OverDays";
		this.lb_OverDays.Size = new System.Drawing.Size(105, 23);
		this.lb_OverDays.TabIndex = 0;
		this.lb_OverDays.Text = "[lb_OverDays]";
		appearance42.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance42.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel30.Appearance = appearance42;
		this.ultraLabel30.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.ultraLabel30.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel30.Location = new System.Drawing.Point(537, 282);
		this.ultraLabel30.Name = "ultraLabel30";
		this.ultraLabel30.Size = new System.Drawing.Size(108, 23);
		this.ultraLabel30.TabIndex = 0;
		this.ultraLabel30.Text = "逾期違約金:";
		appearance43.ForeColor = System.Drawing.Color.Blue;
		appearance43.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance43.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lb_ProjAmt.Appearance = appearance43;
		this.lb_ProjAmt.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.lb_ProjAmt.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lb_ProjAmt.Location = new System.Drawing.Point(649, 174);
		this.lb_ProjAmt.Name = "lb_ProjAmt";
		this.lb_ProjAmt.Size = new System.Drawing.Size(132, 23);
		this.lb_ProjAmt.TabIndex = 0;
		this.lb_ProjAmt.Text = "[lb_ProjAmt]";
		this.tb_CloseNo.Location = new System.Drawing.Point(537, 39);
		this.tb_CloseNo.Name = "tb_CloseNo";
		this.tb_CloseNo.Size = new System.Drawing.Size(244, 21);
		this.tb_CloseNo.TabIndex = 37;
		this.tb_CloseNo.Text = "[tb_CloseNo]";
		appearance44.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel31.Appearance = appearance44;
		this.ultraLabel31.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.ultraLabel31.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel31.Location = new System.Drawing.Point(763, 282);
		this.ultraLabel31.Name = "ultraLabel31";
		this.ultraLabel31.Size = new System.Drawing.Size(18, 23);
		this.ultraLabel31.TabIndex = 0;
		this.ultraLabel31.Text = "元";
		appearance45.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel32.Appearance = appearance45;
		this.ultraLabel32.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.ultraLabel32.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel32.Location = new System.Drawing.Point(763, 255);
		this.ultraLabel32.Name = "ultraLabel32";
		this.ultraLabel32.Size = new System.Drawing.Size(18, 23);
		this.ultraLabel32.TabIndex = 0;
		this.ultraLabel32.Text = "元";
		this.tb_Memo1.Location = new System.Drawing.Point(150, 309);
		this.tb_Memo1.Name = "tb_Memo1";
		this.tb_Memo1.Size = new System.Drawing.Size(631, 21);
		this.tb_Memo1.TabIndex = 37;
		this.tb_Memo1.Text = "[tb_Memo1]";
		this.tb_Memo2.Location = new System.Drawing.Point(150, 336);
		this.tb_Memo2.Name = "tb_Memo2";
		this.tb_Memo2.Size = new System.Drawing.Size(631, 21);
		this.tb_Memo2.TabIndex = 37;
		this.tb_Memo2.Text = "[tb_Memo2]";
		appearance46.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance46.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel33.Appearance = appearance46;
		this.ultraLabel33.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
		this.ultraLabel33.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel33.Location = new System.Drawing.Point(537, 363);
		this.ultraLabel33.Name = "ultraLabel33";
		this.ultraLabel33.Size = new System.Drawing.Size(108, 23);
		this.ultraLabel33.TabIndex = 0;
		this.ultraLabel33.Text = "結算合計:";
		appearance47.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance47.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel34.Appearance = appearance47;
		this.ultraLabel34.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
		this.ultraLabel34.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel34.Location = new System.Drawing.Point(537, 390);
		this.ultraLabel34.Name = "ultraLabel34";
		this.ultraLabel34.Size = new System.Drawing.Size(108, 23);
		this.ultraLabel34.TabIndex = 0;
		this.ultraLabel34.Text = "驗收扣款:";
		appearance48.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance48.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel35.Appearance = appearance48;
		this.ultraLabel35.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
		this.ultraLabel35.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel35.Location = new System.Drawing.Point(537, 417);
		this.ultraLabel35.Name = "ultraLabel35";
		this.ultraLabel35.Size = new System.Drawing.Size(108, 23);
		this.ultraLabel35.TabIndex = 0;
		this.ultraLabel35.Text = "驗收結算總價:";
		appearance49.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance49.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel36.Appearance = appearance49;
		this.ultraLabel36.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
		this.ultraLabel36.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel36.Location = new System.Drawing.Point(537, 444);
		this.ultraLabel36.Name = "ultraLabel36";
		this.ultraLabel36.Size = new System.Drawing.Size(108, 23);
		this.ultraLabel36.TabIndex = 0;
		this.ultraLabel36.Text = "開始驗收日期:";
		appearance50.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance50.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel37.Appearance = appearance50;
		this.ultraLabel37.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
		this.ultraLabel37.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel37.Location = new System.Drawing.Point(537, 471);
		this.ultraLabel37.Name = "ultraLabel37";
		this.ultraLabel37.Size = new System.Drawing.Size(108, 23);
		this.ultraLabel37.TabIndex = 0;
		this.ultraLabel37.Text = "驗收合格日期:";
		appearance51.ForeColor = System.Drawing.Color.Blue;
		appearance51.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance51.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lb_Amt.Appearance = appearance51;
		this.lb_Amt.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
		this.lb_Amt.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lb_Amt.Location = new System.Drawing.Point(649, 363);
		this.lb_Amt.Name = "lb_Amt";
		this.lb_Amt.Size = new System.Drawing.Size(110, 23);
		this.lb_Amt.TabIndex = 0;
		this.lb_Amt.Text = "[lb_ProjAmt]";
		appearance52.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel38.Appearance = appearance52;
		this.ultraLabel38.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
		this.ultraLabel38.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel38.Location = new System.Drawing.Point(763, 363);
		this.ultraLabel38.Name = "ultraLabel38";
		this.ultraLabel38.Size = new System.Drawing.Size(18, 23);
		this.ultraLabel38.TabIndex = 0;
		this.ultraLabel38.Text = "元";
		appearance53.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel39.Appearance = appearance53;
		this.ultraLabel39.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
		this.ultraLabel39.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel39.Location = new System.Drawing.Point(763, 390);
		this.ultraLabel39.Name = "ultraLabel39";
		this.ultraLabel39.Size = new System.Drawing.Size(18, 23);
		this.ultraLabel39.TabIndex = 0;
		this.ultraLabel39.Text = "元";
		appearance54.ForeColor = System.Drawing.Color.Blue;
		appearance54.TextHAlign = Infragistics.Win.HAlign.Left;
		appearance54.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lb_CloseAmt.Appearance = appearance54;
		this.lb_CloseAmt.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
		this.lb_CloseAmt.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lb_CloseAmt.Location = new System.Drawing.Point(649, 417);
		this.lb_CloseAmt.Name = "lb_CloseAmt";
		this.lb_CloseAmt.Size = new System.Drawing.Size(110, 23);
		this.lb_CloseAmt.TabIndex = 0;
		this.lb_CloseAmt.Text = "[lb_CloseAmt]";
		appearance55.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel40.Appearance = appearance55;
		this.ultraLabel40.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
		this.ultraLabel40.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel40.Location = new System.Drawing.Point(763, 417);
		this.ultraLabel40.Name = "ultraLabel40";
		this.ultraLabel40.Size = new System.Drawing.Size(18, 23);
		this.ultraLabel40.TabIndex = 0;
		this.ultraLabel40.Text = "元";
		dateButton6.Caption = "今天";
		this.ad_SCloseDate.DateButtons.Add(dateButton6);
		this.ad_SCloseDate.DayOfWeekCaptionStyle = Infragistics.Win.UltraWinSchedule.DayOfWeekCaptionStyle.ShortDescription;
		this.ad_SCloseDate.Location = new System.Drawing.Point(649, 444);
		this.ad_SCloseDate.Name = "ad_SCloseDate";
		this.ad_SCloseDate.NonAutoSizeHeight = 21;
		this.ad_SCloseDate.NullDateLabel = "";
		this.ad_SCloseDate.Size = new System.Drawing.Size(132, 21);
		this.ad_SCloseDate.TabIndex = 36;
		this.ad_SCloseDate.TipStyle = Infragistics.Win.UltraWinSchedule.TipStyleDay.Holidays;
		this.ad_SCloseDate.Value = resources.GetObject("ad_SCloseDate.Value");
		this.ad_SCloseDate.WeekNumbersVisible = true;
		dateButton7.Caption = "今天";
		this.ad_CloseDate.DateButtons.Add(dateButton7);
		this.ad_CloseDate.DayOfWeekCaptionStyle = Infragistics.Win.UltraWinSchedule.DayOfWeekCaptionStyle.ShortDescription;
		this.ad_CloseDate.Location = new System.Drawing.Point(649, 471);
		this.ad_CloseDate.Name = "ad_CloseDate";
		this.ad_CloseDate.NonAutoSizeHeight = 21;
		this.ad_CloseDate.NullDateLabel = "";
		this.ad_CloseDate.Size = new System.Drawing.Size(132, 21);
		this.ad_CloseDate.TabIndex = 36;
		this.ad_CloseDate.TipStyle = Infragistics.Win.UltraWinSchedule.TipStyleDay.Holidays;
		this.ad_CloseDate.Value = resources.GetObject("ad_CloseDate.Value");
		this.ad_CloseDate.WeekNumbersVisible = true;
		appearance56.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance56.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel41.Appearance = appearance56;
		this.ultraLabel41.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
		this.ultraLabel41.Font = new System.Drawing.Font("細明體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel41.Location = new System.Drawing.Point(11, 498);
		this.ultraLabel41.Name = "ultraLabel41";
		this.ultraLabel41.Size = new System.Drawing.Size(135, 23);
		this.ultraLabel41.TabIndex = 0;
		this.ultraLabel41.Text = "備註:";
		this.tb_Memo3.Location = new System.Drawing.Point(150, 498);
		this.tb_Memo3.Name = "tb_Memo3";
		this.tb_Memo3.Size = new System.Drawing.Size(631, 21);
		this.tb_Memo3.TabIndex = 37;
		this.tb_Memo3.Text = "[tb_Memo3]";
		this.PNL_1.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
		this.PNL_1.Location = new System.Drawing.Point(11, 12);
		this.PNL_1.Name = "PNL_1";
		this.PNL_1.Size = new System.Drawing.Size(770, 77);
		this.PNL_1.TabIndex = 40;
		this.tb_Days.FormatString = "###,###,###,##0";
		this.tb_Days.Location = new System.Drawing.Point(150, 255);
		this.tb_Days.Name = "tb_Days";
		this.tb_Days.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
		this.tb_Days.PromptChar = ' ';
		this.tb_Days.Size = new System.Drawing.Size(110, 21);
		this.tb_Days.TabIndex = 43;
		this.tb_AllOverDays.FormatString = "###,###,###,##0";
		this.tb_AllOverDays.Location = new System.Drawing.Point(406, 228);
		this.tb_AllOverDays.Name = "tb_AllOverDays";
		this.tb_AllOverDays.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
		this.tb_AllOverDays.PromptChar = ' ';
		this.tb_AllOverDays.Size = new System.Drawing.Size(105, 21);
		this.tb_AllOverDays.TabIndex = 43;
		this.tb_unOverDays.FormatString = "###,###,###,##0";
		this.tb_unOverDays.Location = new System.Drawing.Point(406, 255);
		this.tb_unOverDays.Name = "tb_unOverDays";
		this.tb_unOverDays.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
		this.tb_unOverDays.PromptChar = ' ';
		this.tb_unOverDays.Size = new System.Drawing.Size(105, 21);
		this.tb_unOverDays.TabIndex = 43;
		this.tb_OtherAmt.FormatString = "###,###,###,##0";
		this.tb_OtherAmt.Location = new System.Drawing.Point(649, 255);
		this.tb_OtherAmt.Name = "tb_OtherAmt";
		this.tb_OtherAmt.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
		this.tb_OtherAmt.PromptChar = ' ';
		this.tb_OtherAmt.Size = new System.Drawing.Size(110, 21);
		this.tb_OtherAmt.TabIndex = 43;
		this.tb_OverAmt.FormatString = "###,###,###,##0";
		this.tb_OverAmt.Location = new System.Drawing.Point(649, 282);
		this.tb_OverAmt.Name = "tb_OverAmt";
		this.tb_OverAmt.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
		this.tb_OverAmt.PromptChar = ' ';
		this.tb_OverAmt.Size = new System.Drawing.Size(110, 21);
		this.tb_OverAmt.TabIndex = 43;
		this.tb_Deduct.FormatString = "###,###,###,##0";
		this.tb_Deduct.Location = new System.Drawing.Point(649, 390);
		this.tb_Deduct.Name = "tb_Deduct";
		this.tb_Deduct.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
		this.tb_Deduct.PromptChar = ' ';
		this.tb_Deduct.Size = new System.Drawing.Size(110, 21);
		this.tb_Deduct.TabIndex = 43;
		this.PNL_2.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.PNL_2.Location = new System.Drawing.Point(11, 93);
		this.PNL_2.Name = "PNL_2";
		this.PNL_2.Size = new System.Drawing.Size(770, 266);
		this.PNL_2.TabIndex = 41;
		this.PNL_3.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
		this.PNL_3.Location = new System.Drawing.Point(11, 363);
		this.PNL_3.Name = "PNL_3";
		this.PNL_3.Size = new System.Drawing.Size(770, 166);
		this.PNL_3.TabIndex = 42;
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		base.ClientSize = new System.Drawing.Size(792, 573);
		base.Controls.Add(this.PanelMain);
		base.Controls.Add(this.panel1);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.KeyPreview = true;
		base.Name = "FormSubCloseInfo";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "結算總計資訊";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormSubCloseInfo_KeyDown);
		base.Load += new System.EventHandler(FormSubCloseInfo_Load);
		this.panel1.ResumeLayout(false);
		this.PanelMain.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.c1Sizer1).EndInit();
		this.c1Sizer1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.tb_WorkDays).EndInit();
		this.panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Grid1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ddl_BidKind).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ad_BudStart).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ad_InputDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ad_RealStart).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ad_BudEnd).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ad_RealEnd).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tb_CloseNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tb_Memo1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tb_Memo2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ad_SCloseDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ad_CloseDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tb_Memo3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tb_Days).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tb_AllOverDays).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tb_unOverDays).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tb_OtherAmt).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tb_OverAmt).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tb_Deduct).EndInit();
		base.ResumeLayout(false);
	}

	private void FormSubCloseInfo_Load(object sender, EventArgs e)
	{
		ClearCtrlText();
		GetData();
	}

	private void GetData()
	{
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(SubCloseInfo) 契約書結算內容維護");
		ls_prjcode = F_ProjectCode;
		ls_subproj = F_SubProjectCode;
		sub_info SubInfoCom = new sub_info(tmp_AL1);
		sub_acc AccCom = new sub_acc(tmp_AL1);
		lb_Lock = AccCom.GetLockMode(ls_Queue, ls_subproj, ls_prjcode);
		if (lb_Lock)
		{
			ad_InputDate.Enabled = false;
			ad_RealEnd.Enabled = false;
			ad_CloseDate.Enabled = false;
			tb_CloseNo.Enabled = false;
			tb_Days.Enabled = false;
			tb_Memo1.Enabled = false;
			tb_Memo2.Enabled = false;
			tb_Memo3.Enabled = false;
			tb_Deduct.Enabled = false;
		}
		DataTable ldt_Info = SubInfoCom.ListItem(ls_subproj, ls_prjcode);
		dr = ldt_Info.Rows[0];
		BindData();
	}

	private void ClearCtrlText()
	{
		tb_CloseNo.Text = "";
		tb_Memo1.Text = "";
		tb_Memo2.Text = "";
		tb_Memo3.Text = "";
		tb_WorkDays.Value = 0;
		tb_Days.Value = 0;
		tb_AllOverDays.Value = 0;
		tb_unOverDays.Value = 0;
		tb_OtherAmt.Value = 0;
		tb_OverAmt.Value = 0;
		tb_Deduct.Value = 0;
		ad_InputDate.Value = DateTime.Now;
		ad_BudStart.Value = DateTime.Now;
		ad_BudEnd.Value = DateTime.Now;
		ad_RealStart.Value = DateTime.Now;
		ad_RealEnd.Value = DateTime.Now;
		ad_SCloseDate.Value = DateTime.Now;
		ad_CloseDate.Value = DateTime.Now;
	}

	private void BindData()
	{
		lb_ProjectDesc.Text = dr["ProjectNameC"].ToString();
		lb_PorjectCode.Text = dr["InvoCode"].ToString();
		lb_ProjectCode1.Text = dr["ProjectCode"].ToString();
		lb_MainName.Text = dr["MainName"].ToString();
		lb_Sublet.Text = dr["owner"].ToString();
		lb_Address.Text = dr["ProjectAddress"].ToString();
		lb_WorkName.Text = dr["WorkUnit"].ToString();
		lb_WorkMode.Text = dr["WorkMode"].ToString();
		lb_ProjAmt.Text = PubTools.StrFormat(dr["ProjAmt"].ToString(), 0);
		ContractAmt = PubTools.Str2Double(dr["ProjAmt"].ToString());
		if (dr["ActEnd"].ToString() == "")
		{
			ad_RealEnd.Value = null;
		}
		else
		{
			ad_RealEnd.Value = PubTools.Str2DateTime(dr["ActEnd"].ToString());
		}
		if (dr["BudStart"].ToString() == "")
		{
			ad_BudStart.Value = null;
		}
		else
		{
			ad_BudStart.Value = PubTools.Str2DateTime(dr["BudStart"].ToString());
		}
		if (dr["ActStart"].ToString() == "")
		{
			ad_RealStart.Value = null;
		}
		else
		{
			ad_RealStart.Value = PubTools.Str2DateTime(dr["ActStart"].ToString());
		}
		if (dr["BudEnd"].ToString() == "")
		{
			ad_BudEnd.Value = null;
		}
		else
		{
			ad_BudEnd.Value = PubTools.Str2DateTime(dr["BudEnd"].ToString());
		}
		ad_BudStart.Enabled = false;
		ad_RealStart.Enabled = false;
		ad_BudEnd.Enabled = false;
		tb_CloseNo.Text = dr["CloseNo"].ToString();
		tb_Days.Text = PubTools.Str2Int(dr["Days"]).ToString();
		tb_Memo1.Text = dr["Memo1"].ToString();
		tb_Memo2.Text = dr["Memo2"].ToString();
		tb_Deduct.Text = PubTools.StrFormat(dr["Deduct"].ToString(), 0);
		tb_Memo3.Text = dr["Remark"].ToString();
		if (dr["InputDate"].ToString() == "")
		{
			ad_InputDate.Value = null;
		}
		else
		{
			ad_InputDate.Value = PubTools.Str2DateTime(dr["InputDate"].ToString());
		}
		if (dr["CloseDate"].ToString() == "")
		{
			ad_CloseDate.Value = null;
		}
		else
		{
			ad_CloseDate.Value = PubTools.Str2DateTime(dr["CloseDate"].ToString());
		}
		if (dr["SCloseDate"].ToString() == "")
		{
			ad_SCloseDate.Value = null;
		}
		else
		{
			ad_SCloseDate.Value = PubTools.Str2DateTime(dr["SCloseDate"].ToString());
		}
		ddl_BidKind.SelectedIndex = PubTools.Str2Int(dr["BidKind"]);
		tb_WorkDays.Text = PubTools.Str2Int(dr["WorkDays"]).ToString();
		tb_AllOverDays.Text = PubTools.Str2Int(dr["AllOverDays"]).ToString();
		tb_unOverDays.Text = PubTools.Str2Int(dr["unOverDays"]).ToString();
		lb_OverDays.Text = (PubTools.Str2Int(dr["AllOverDays"]) - PubTools.Str2Int(dr["unOverDays"])).ToString();
		tb_OtherAmt.Text = PubTools.StrFormat(dr["OtherAmt"], 0);
		tb_OverAmt.Text = PubTools.StrFormat(dr["OverAmt"], 0);
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(SubCloseInfo) 契約書結算內容維護");
		submfq MfqCom = new submfq(tmp_AL1);
		DataTable ldt_MFQ = MfqCom.ListItem("", ls_Queue, ls_subproj, ls_prjcode);
		MfqCom = null;
		double AccAmt = 0.0;
		for (int i = ldt_MFQ.Rows.Count; i > 0; i--)
		{
			dr = ldt_MFQ.Rows[i - 1];
			if (dr["Itemdes"].ToString().Trim() == "".PadLeft(32, '9'))
			{
				AccAmt = PubTools.Str2Double(dr["Acc_amt"].ToString());
				i = -1;
			}
			else if (dr["Kind"].ToString().ToUpper() == "Z" && dr["Itemdes"].ToString().Trim().Length == 4)
			{
				AccAmt = PubTools.Str2Double(dr["Acc_amt"].ToString());
				i = -1;
			}
		}
		lb_Amt.Text = PubTools.StrFormat(AccAmt, 0);
		double ld_Deduct = PubTools.Str2Double(tb_Deduct.Text);
		tb_Deduct.Text = PubTools.StrFormat(ld_Deduct, 0);
		lb_CloseAmt.Text = PubTools.StrFormat(AccAmt - ld_Deduct, 0);
		sub_ChgMain ChgMainCom = new sub_ChgMain(tmp_AL1);
		DataTable dt = ChgMainCom.ListItem("", ls_prjcode, ls_subproj);
		ChgMainCom = null;
		dt.Columns.Add("ChgAmt");
		foreach (DataRow dr1 in dt.Rows)
		{
			dr1["ChgAmt"] = PubTools.Str2Double(dr1["PostAmt"]) - PubTools.Str2Double(dr1["PreAmt"]);
			ContractAmt = PubTools.Str2Double(dr1["PostAmt"].ToString());
		}
		using (dt)
		{
			Grid1.Rows.Count = dt.Rows.Count + 1;
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				Grid1[i + 1, "ChgCount"] = dt.Rows[i]["ChgCount"];
				Grid1[i + 1, "ChgTxtNo"] = dt.Rows[i]["ChgTxtNo"];
				Grid1[i + 1, "ChgAmt"] = dt.Rows[i]["ChgAmt"];
			}
		}
	}

	private void D_Btn_Fnsh_Click(object sender, EventArgs e)
	{
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(SubCloseInfo) 契約書結算內容維護--存檔");
		ls_prjcode = F_ProjectCode;
		ls_subproj = F_SubProjectCode;
		sub_info SubInfoCom = new sub_info(tmp_AL1);
		SubInfoCom.ps_ProjectCode = ls_prjcode;
		SubInfoCom.ps_Sproj = ls_subproj;
		SubInfoCom.ps_CloseNo = tb_CloseNo.Text;
		SubInfoCom.ps_Days = tb_Days.Text;
		SubInfoCom.ps_Memo1 = tb_Memo1.Text;
		SubInfoCom.ps_Memo2 = tb_Memo2.Text;
		SubInfoCom.ps_Deduct = tb_Deduct.Text;
		SubInfoCom.ps_Remark = tb_Memo3.Text;
		SubInfoCom.ps_ActEnd = ad_RealEnd.Text;
		SubInfoCom.ps_InputDate = ad_InputDate.Text;
		SubInfoCom.ps_CloseDate = ad_CloseDate.Text;
		SubInfoCom.ps_SCloseDate = ad_SCloseDate.Text;
		SubInfoCom.ps_BidKind = ddl_BidKind.SelectedIndex.ToString();
		SubInfoCom.ps_WorkDays = tb_WorkDays.Text;
		SubInfoCom.ps_AllOverDays = tb_AllOverDays.Text;
		SubInfoCom.ps_unOverDays = tb_unOverDays.Text;
		SubInfoCom.ps_OtherAmt = tb_OtherAmt.Text;
		SubInfoCom.ps_OverAmt = tb_OverAmt.Text;
		SubInfoCom.UpdItem();
		base.DialogResult = DialogResult.OK;
	}

	private void FormSubCloseInfo_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormSubCloseInfo");
		}
	}
}
