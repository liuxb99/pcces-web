using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.STDClass;
using C1.Win.C1Sizer;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinSchedule;
using Infragistics.Win.UltraWinSchedule.CalendarCombo;

namespace Archnowledge.Pcces.PccesMain.SysMaintain;

public class FormSys_C_Edit : Form
{
	private const string CallFormHelp = "FormSys_C_Edit";

	private Container components = null;

	private Panel panel1;

	private Panel panel2;

	private Panel panel3;

	private UltraLabel lblCaption;

	private UltraButton Btn_Cncl;

	private UltraButton Btn_OK;

	private C1Sizer c1Sizer1;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	private UltraLabel ultraLabel3;

	private UltraLabel ultraLabel4;

	private UltraLabel ultraLabel5;

	private UltraLabel ultraLabel6;

	private UltraLabel ultraLabel7;

	private UltraLabel ultraLabel8;

	private UltraTextEditor txtInvoice_No;

	private UltraTextEditor txtTitle;

	private UltraTextEditor txtShortitle;

	private UltraTextEditor txtAddress;

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

	private UltraLabel ultraLabel25;

	private UltraLabel ultraLabel26;

	private UltraLabel ultraLabel27;

	private UltraLabel ultraLabel28;

	private UltraLabel ultraLabel29;

	private UltraLabel ultraLabel30;

	private UltraLabel ultraLabel31;

	private UltraLabel ultraLabel32;

	private UltraLabel ultraLabel33;

	private UltraLabel ultraLabel34;

	private UltraTextEditor txtBoss;

	private UltraTextEditor txtTel_Boss;

	private UltraTextEditor txtTel_Liai;

	private UltraTextEditor txtLiaison;

	private UltraTextEditor txtCapital;

	private UltraTextEditor txtBankNo;

	private UltraTextEditor txtBankID;

	private UltraTextEditor txtVendor_SCode;

	private UltraTextEditor txtTrade_License;

	private UltraTextEditor txtVendor_Tmp;

	private UltraTextEditor txtFax;

	private UltraTextEditor txtTel_Liai2;

	private UltraTextEditor txtETitle;

	private UltraTextEditor txtEMail;

	private UltraTextEditor txtFAddress;

	private UltraCalendarCombo dpDPunish_End;

	private UltraCalendarCombo dpDRegister;

	private UltraCalendarCombo dpDTrade_License;

	private UltraTextEditor txtFLicense;

	private UltraTextEditor txtUnion_License;

	private UltraComboEditor dpOrg_Type;

	private UltraComboEditor dpLevel_Business;

	private UltraComboEditor dpMail_Area_No;

	private UltraTextEditor txtVendor_Rsea;

	private UltraTextEditor txtProfession;

	private UltraTextEditor txtLiai_Position;

	private UltraOptionSet opRight_Bid;

	private UltraCalendarCombo dpDComp_License;

	private UltraComboEditor dpArea;

	private string F_UserID;

	private string F_EditMode = "";

	private string F_Invoice_No = "";

	private UltraCalendarCombo dpOpenTime;

	private UltraLabel ultraLabel35;

	private UltraTextEditor txtBoss_ID;

	private UltraLabel ultraLabel36;

	private DataTable DT1 = new DataTable();

	private bool EnableCOMS = SysConfig.SysComsEnable;

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

	public string _Invoice_No
	{
		get
		{
			return F_Invoice_No;
		}
		set
		{
			F_Invoice_No = value;
		}
	}

	public bool _IsArchCOMS
	{
		get
		{
			return EnableCOMS;
		}
		set
		{
			EnableCOMS = value;
		}
	}

	public FormSys_C_Edit()
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
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.SysMaintain.FormSys_C_Edit));
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem4 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem5 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem6 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem7 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem8 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem9 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem10 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem11 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem12 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton1 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
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
		Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance38 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance39 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton2 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
		Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton3 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
		Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton4 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
		Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton5 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
		Infragistics.Win.ValueListItem valueListItem13 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem14 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem15 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem16 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.Appearance appearance40 = new Infragistics.Win.Appearance();
		this.panel1 = new System.Windows.Forms.Panel();
		this.lblCaption = new Infragistics.Win.Misc.UltraLabel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.ultraLabel36 = new Infragistics.Win.Misc.UltraLabel();
		this.Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.Btn_OK = new Infragistics.Win.Misc.UltraButton();
		this.panel3 = new System.Windows.Forms.Panel();
		this.c1Sizer1 = new C1.Win.C1Sizer.C1Sizer();
		this.dpArea = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.opRight_Bid = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
		this.dpOrg_Type = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.dpDPunish_End = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
		this.txtInvoice_No = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.txtTitle = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.txtShortitle = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.txtAddress = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
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
		this.ultraLabel16 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel17 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel18 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel19 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel20 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel21 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel22 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel23 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel24 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel25 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel26 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel27 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel28 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel29 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel30 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel31 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel32 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel33 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel34 = new Infragistics.Win.Misc.UltraLabel();
		this.txtBoss = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtCapital = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtBankNo = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtBankID = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtVendor_SCode = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtTrade_License = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtVendor_Tmp = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtTel_Boss = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtFax = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtTel_Liai = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtTel_Liai2 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtETitle = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtEMail = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtFAddress = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.dpOpenTime = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
		this.dpDRegister = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
		this.dpDTrade_License = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
		this.dpDComp_License = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
		this.txtFLicense = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtUnion_License = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.dpLevel_Business = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.dpMail_Area_No = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.txtVendor_Rsea = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtProfession = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtLiaison = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtLiai_Position = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel35 = new Infragistics.Win.Misc.UltraLabel();
		this.txtBoss_ID = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.c1Sizer1).BeginInit();
		this.c1Sizer1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dpArea).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.opRight_Bid).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dpOrg_Type).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dpDPunish_End).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtInvoice_No).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTitle).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtShortitle).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAddress).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBoss).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCapital).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBankNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBankID).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVendor_SCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTrade_License).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVendor_Tmp).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTel_Boss).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtFax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTel_Liai).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTel_Liai2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtETitle).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEMail).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtFAddress).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dpOpenTime).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dpDRegister).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dpDTrade_License).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dpDComp_License).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtFLicense).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtUnion_License).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dpLevel_Business).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dpMail_Area_No).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVendor_Rsea).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtProfession).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtLiaison).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtLiai_Position).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBoss_ID).BeginInit();
		base.SuspendLayout();
		this.panel1.Controls.Add(this.lblCaption);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(790, 36);
		this.panel1.TabIndex = 0;
		appearance1.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance1.ForeColor = System.Drawing.Color.White;
		appearance1.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblCaption.Appearance = appearance1;
		this.lblCaption.Dock = System.Windows.Forms.DockStyle.Fill;
		this.lblCaption.Location = new System.Drawing.Point(0, 0);
		this.lblCaption.Name = "lblCaption";
		this.lblCaption.Size = new System.Drawing.Size(790, 36);
		this.lblCaption.TabIndex = 0;
		this.lblCaption.Text = " 系統維護--";
		this.panel2.Controls.Add(this.ultraLabel36);
		this.panel2.Controls.Add(this.Btn_Cncl);
		this.panel2.Controls.Add(this.Btn_OK);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 527);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(790, 36);
		this.panel2.TabIndex = 1;
		appearance2.ForeColor = System.Drawing.Color.Red;
		this.ultraLabel36.Appearance = appearance2;
		this.ultraLabel36.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel36.Location = new System.Drawing.Point(8, 13);
		this.ultraLabel36.Name = "ultraLabel36";
		this.ultraLabel36.Size = new System.Drawing.Size(400, 15);
		this.ultraLabel36.TabIndex = 5;
		this.ultraLabel36.Text = "PS:有 * 號的欄位是必填的。";
		this.Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance3.Image = resources.GetObject("appearance3.Image");
		appearance3.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.Btn_Cncl.Appearance = appearance3;
		this.Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.Btn_Cncl.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.Btn_Cncl.Location = new System.Drawing.Point(691, 3);
		this.Btn_Cncl.Name = "Btn_Cncl";
		this.Btn_Cncl.ShowFocusRect = false;
		this.Btn_Cncl.ShowOutline = false;
		this.Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.Btn_Cncl.SupportThemes = false;
		this.Btn_Cncl.TabIndex = 4;
		this.Btn_Cncl.Text = "取消";
		this.Btn_OK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance4.Image = resources.GetObject("appearance4.Image");
		appearance4.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.Btn_OK.Appearance = appearance4;
		this.Btn_OK.BackColor = System.Drawing.SystemColors.Control;
		this.Btn_OK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.Btn_OK.Font = new System.Drawing.Font("細明體", 11f);
		this.Btn_OK.ImageSize = new System.Drawing.Size(20, 20);
		this.Btn_OK.ImageTransparentColor = System.Drawing.Color.White;
		this.Btn_OK.Location = new System.Drawing.Point(599, 3);
		this.Btn_OK.Name = "Btn_OK";
		this.Btn_OK.ShowFocusRect = false;
		this.Btn_OK.ShowOutline = false;
		this.Btn_OK.Size = new System.Drawing.Size(88, 31);
		this.Btn_OK.SupportThemes = false;
		this.Btn_OK.TabIndex = 3;
		this.Btn_OK.Text = "確定";
		this.Btn_OK.Click += new System.EventHandler(Btn_OK_Click);
		this.panel3.AutoScroll = true;
		this.panel3.Controls.Add(this.c1Sizer1);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel3.Location = new System.Drawing.Point(0, 36);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(790, 491);
		this.panel3.TabIndex = 2;
		this.c1Sizer1.AllowDrop = true;
		this.c1Sizer1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.c1Sizer1.Controls.Add(this.dpArea);
		this.c1Sizer1.Controls.Add(this.opRight_Bid);
		this.c1Sizer1.Controls.Add(this.dpOrg_Type);
		this.c1Sizer1.Controls.Add(this.dpDPunish_End);
		this.c1Sizer1.Controls.Add(this.txtInvoice_No);
		this.c1Sizer1.Controls.Add(this.ultraLabel1);
		this.c1Sizer1.Controls.Add(this.ultraLabel2);
		this.c1Sizer1.Controls.Add(this.txtTitle);
		this.c1Sizer1.Controls.Add(this.ultraLabel3);
		this.c1Sizer1.Controls.Add(this.txtShortitle);
		this.c1Sizer1.Controls.Add(this.ultraLabel4);
		this.c1Sizer1.Controls.Add(this.txtAddress);
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
		this.c1Sizer1.Controls.Add(this.ultraLabel16);
		this.c1Sizer1.Controls.Add(this.ultraLabel17);
		this.c1Sizer1.Controls.Add(this.ultraLabel18);
		this.c1Sizer1.Controls.Add(this.ultraLabel19);
		this.c1Sizer1.Controls.Add(this.ultraLabel20);
		this.c1Sizer1.Controls.Add(this.ultraLabel21);
		this.c1Sizer1.Controls.Add(this.ultraLabel22);
		this.c1Sizer1.Controls.Add(this.ultraLabel23);
		this.c1Sizer1.Controls.Add(this.ultraLabel24);
		this.c1Sizer1.Controls.Add(this.ultraLabel25);
		this.c1Sizer1.Controls.Add(this.ultraLabel26);
		this.c1Sizer1.Controls.Add(this.ultraLabel27);
		this.c1Sizer1.Controls.Add(this.ultraLabel28);
		this.c1Sizer1.Controls.Add(this.ultraLabel29);
		this.c1Sizer1.Controls.Add(this.ultraLabel30);
		this.c1Sizer1.Controls.Add(this.ultraLabel31);
		this.c1Sizer1.Controls.Add(this.ultraLabel32);
		this.c1Sizer1.Controls.Add(this.ultraLabel33);
		this.c1Sizer1.Controls.Add(this.ultraLabel34);
		this.c1Sizer1.Controls.Add(this.txtBoss);
		this.c1Sizer1.Controls.Add(this.txtCapital);
		this.c1Sizer1.Controls.Add(this.txtBankNo);
		this.c1Sizer1.Controls.Add(this.txtBankID);
		this.c1Sizer1.Controls.Add(this.txtVendor_SCode);
		this.c1Sizer1.Controls.Add(this.txtTrade_License);
		this.c1Sizer1.Controls.Add(this.txtVendor_Tmp);
		this.c1Sizer1.Controls.Add(this.txtTel_Boss);
		this.c1Sizer1.Controls.Add(this.txtFax);
		this.c1Sizer1.Controls.Add(this.txtTel_Liai);
		this.c1Sizer1.Controls.Add(this.txtTel_Liai2);
		this.c1Sizer1.Controls.Add(this.txtETitle);
		this.c1Sizer1.Controls.Add(this.txtEMail);
		this.c1Sizer1.Controls.Add(this.txtFAddress);
		this.c1Sizer1.Controls.Add(this.dpOpenTime);
		this.c1Sizer1.Controls.Add(this.dpDRegister);
		this.c1Sizer1.Controls.Add(this.dpDTrade_License);
		this.c1Sizer1.Controls.Add(this.dpDComp_License);
		this.c1Sizer1.Controls.Add(this.txtFLicense);
		this.c1Sizer1.Controls.Add(this.txtUnion_License);
		this.c1Sizer1.Controls.Add(this.dpLevel_Business);
		this.c1Sizer1.Controls.Add(this.dpMail_Area_No);
		this.c1Sizer1.Controls.Add(this.txtVendor_Rsea);
		this.c1Sizer1.Controls.Add(this.txtProfession);
		this.c1Sizer1.Controls.Add(this.txtLiaison);
		this.c1Sizer1.Controls.Add(this.txtLiai_Position);
		this.c1Sizer1.Controls.Add(this.ultraLabel35);
		this.c1Sizer1.Controls.Add(this.txtBoss_ID);
		this.c1Sizer1.GridDefinition = "5.73770491803279:False:False;5.32786885245902:False:False;6.14754098360656:False:False;5.73770491803279:False:False;5.94262295081967:False:False;6.14754098360656:False:False;5.94262295081967:False:False;5.73770491803279:False:False;5.32786885245902:False:False;5.12295081967213:False:False;6.14754098360656:False:False;5.94262295081967:False:False;5.94262295081967:False:False;5.94262295081967:False:False;5.73770491803279:False:False;\t15.8428390367554:False:False;16.4765525982256:False:False;15.9695817490494:False:False;16.0963244613435:False:False;16.2230671736375:False:False;15.8428390367554:False:False;";
		this.c1Sizer1.Location = new System.Drawing.Point(0, 0);
		this.c1Sizer1.Name = "c1Sizer1";
		this.c1Sizer1.Size = new System.Drawing.Size(789, 488);
		this.c1Sizer1.TabIndex = 0;
		this.c1Sizer1.TabStop = false;
		this.c1Sizer1.Click += new System.EventHandler(c1Sizer1_Click);
		this.dpArea.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
		valueListItem1.DataValue = "北";
		valueListItem1.DisplayText = "北";
		valueListItem2.DataValue = "中";
		valueListItem2.DisplayText = "中";
		valueListItem3.DataValue = "南";
		valueListItem3.DisplayText = "南";
		valueListItem4.DataValue = "東";
		valueListItem4.DisplayText = "東";
		valueListItem5.DataValue = "離島";
		valueListItem5.DisplayText = "離島";
		this.dpArea.Items.Add(valueListItem1);
		this.dpArea.Items.Add(valueListItem2);
		this.dpArea.Items.Add(valueListItem3);
		this.dpArea.Items.Add(valueListItem4);
		this.dpArea.Items.Add(valueListItem5);
		this.dpArea.Location = new System.Drawing.Point(133, 323);
		this.dpArea.Name = "dpArea";
		this.dpArea.Size = new System.Drawing.Size(130, 24);
		this.dpArea.TabIndex = 5;
		this.dpArea.Text = null;
		this.opRight_Bid.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
		this.opRight_Bid.CheckedIndex = 0;
		this.opRight_Bid.FlatMode = true;
		this.opRight_Bid.ItemAppearance = appearance5;
		valueListItem6.DataValue = "1";
		valueListItem6.DisplayText = "有";
		valueListItem7.DataValue = "0";
		valueListItem7.DisplayText = "無";
		this.opRight_Bid.Items.Add(valueListItem6);
		this.opRight_Bid.Items.Add(valueListItem7);
		this.opRight_Bid.ItemSpacingHorizontal = 10;
		this.opRight_Bid.ItemSpacingVertical = 8;
		this.opRight_Bid.Location = new System.Drawing.Point(660, 357);
		this.opRight_Bid.Name = "opRight_Bid";
		this.opRight_Bid.Size = new System.Drawing.Size(125, 29);
		this.opRight_Bid.TabIndex = 4;
		this.opRight_Bid.Text = "有";
		this.dpOrg_Type.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
		valueListItem8.DataValue = "獨資";
		valueListItem8.DisplayText = "獨資";
		valueListItem9.DataValue = "公司";
		valueListItem9.DisplayText = "公司";
		valueListItem10.DataValue = "股份有限公司";
		valueListItem10.DisplayText = "股份有限公司";
		valueListItem11.DataValue = "合夥";
		valueListItem11.DisplayText = "合夥";
		valueListItem12.DataValue = "事業機關";
		valueListItem12.DisplayText = "事業機關";
		this.dpOrg_Type.Items.Add(valueListItem8);
		this.dpOrg_Type.Items.Add(valueListItem9);
		this.dpOrg_Type.Items.Add(valueListItem10);
		this.dpOrg_Type.Items.Add(valueListItem11);
		this.dpOrg_Type.Items.Add(valueListItem12);
		this.dpOrg_Type.Location = new System.Drawing.Point(133, 456);
		this.dpOrg_Type.Name = "dpOrg_Type";
		this.dpOrg_Type.Size = new System.Drawing.Size(130, 24);
		this.dpOrg_Type.TabIndex = 3;
		this.dpOrg_Type.Text = null;
		dateButton1.Caption = "今天";
		this.dpDPunish_End.DateButtons.Add(dateButton1);
		this.dpDPunish_End.Location = new System.Drawing.Point(133, 294);
		this.dpDPunish_End.Name = "dpDPunish_End";
		this.dpDPunish_End.NonAutoSizeHeight = 21;
		this.dpDPunish_End.NullDateLabel = "";
		this.dpDPunish_End.Size = new System.Drawing.Size(130, 21);
		this.dpDPunish_End.TabIndex = 2;
		this.dpDPunish_End.Value = resources.GetObject("dpDPunish_End.Value");
		this.txtInvoice_No.Location = new System.Drawing.Point(133, 4);
		this.txtInvoice_No.Name = "txtInvoice_No";
		this.txtInvoice_No.Size = new System.Drawing.Size(130, 24);
		this.txtInvoice_No.TabIndex = 1;
		this.txtInvoice_No.Text = "ultraTextEditor1";
		this.txtInvoice_No.Validating += new System.ComponentModel.CancelEventHandler(txtInvoice_No_Validating);
		appearance6.ForeColor = System.Drawing.Color.Red;
		appearance6.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance6.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel1.Appearance = appearance6;
		this.ultraLabel1.Location = new System.Drawing.Point(4, 4);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(125, 28);
		this.ultraLabel1.TabIndex = 0;
		this.ultraLabel1.Text = "*廠商統編:";
		appearance7.ForeColor = System.Drawing.Color.Red;
		appearance7.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance7.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel2.Appearance = appearance7;
		this.ultraLabel2.Location = new System.Drawing.Point(4, 36);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(125, 26);
		this.ultraLabel2.TabIndex = 0;
		this.ultraLabel2.Text = "*廠商名稱:";
		this.txtTitle.Location = new System.Drawing.Point(133, 36);
		this.txtTitle.Name = "txtTitle";
		this.txtTitle.Size = new System.Drawing.Size(652, 24);
		this.txtTitle.TabIndex = 1;
		this.txtTitle.Text = "ultraTextEditor1";
		this.txtTitle.Validating += new System.ComponentModel.CancelEventHandler(txtInvoice_No_Validating);
		this.txtTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		appearance8.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance8.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel3.Appearance = appearance8;
		this.ultraLabel3.Location = new System.Drawing.Point(267, 4);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(126, 28);
		this.ultraLabel3.TabIndex = 0;
		this.ultraLabel3.Text = "廠商簡稱:";
		this.txtShortitle.Location = new System.Drawing.Point(397, 4);
		this.txtShortitle.Name = "txtShortitle";
		this.txtShortitle.Size = new System.Drawing.Size(127, 24);
		this.txtShortitle.TabIndex = 1;
		this.txtShortitle.Text = "ultraTextEditor1";
		this.txtShortitle.Validating += new System.ComponentModel.CancelEventHandler(txtInvoice_No_Validating);
		this.txtShortitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		appearance9.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance9.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel4.Appearance = appearance9;
		this.ultraLabel4.Location = new System.Drawing.Point(4, 66);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(125, 30);
		this.ultraLabel4.TabIndex = 0;
		this.ultraLabel4.Text = "廠商地址:";
		this.txtAddress.Location = new System.Drawing.Point(133, 66);
		this.txtAddress.Name = "txtAddress";
		this.txtAddress.Size = new System.Drawing.Size(652, 24);
		this.txtAddress.TabIndex = 1;
		this.txtAddress.Text = "ultraTextEditor1";
		this.txtAddress.Validating += new System.ComponentModel.CancelEventHandler(txtInvoice_No_Validating);
		this.txtAddress.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		appearance10.ForeColor = System.Drawing.Color.Red;
		appearance10.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance10.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel5.Appearance = appearance10;
		this.ultraLabel5.Location = new System.Drawing.Point(4, 100);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(125, 28);
		this.ultraLabel5.TabIndex = 0;
		this.ultraLabel5.Text = "*負責人:";
		appearance11.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance11.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel6.Appearance = appearance11;
		this.ultraLabel6.Location = new System.Drawing.Point(528, 100);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(128, 28);
		this.ultraLabel6.TabIndex = 0;
		this.ultraLabel6.Text = "公司電話:";
		this.ultraLabel6.Click += new System.EventHandler(ultraLabel6_Click);
		appearance12.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance12.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel7.Appearance = appearance12;
		this.ultraLabel7.Location = new System.Drawing.Point(4, 132);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(125, 29);
		this.ultraLabel7.TabIndex = 0;
		this.ultraLabel7.Text = "公司傳真:";
		appearance13.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance13.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel8.Appearance = appearance13;
		this.ultraLabel8.Location = new System.Drawing.Point(267, 132);
		this.ultraLabel8.Name = "ultraLabel8";
		this.ultraLabel8.Size = new System.Drawing.Size(126, 29);
		this.ultraLabel8.TabIndex = 0;
		this.ultraLabel8.Text = "聯絡人:";
		appearance14.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance14.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel9.Appearance = appearance14;
		this.ultraLabel9.Location = new System.Drawing.Point(267, 165);
		this.ultraLabel9.Name = "ultraLabel9";
		this.ultraLabel9.Size = new System.Drawing.Size(126, 30);
		this.ultraLabel9.TabIndex = 0;
		this.ultraLabel9.Text = "聯絡人電話1:";
		appearance15.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance15.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel10.Appearance = appearance15;
		this.ultraLabel10.Location = new System.Drawing.Point(528, 165);
		this.ultraLabel10.Name = "ultraLabel10";
		this.ultraLabel10.Size = new System.Drawing.Size(128, 30);
		this.ultraLabel10.TabIndex = 0;
		this.ultraLabel10.Text = "聯絡人電話2:";
		appearance16.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance16.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel11.Appearance = appearance16;
		this.ultraLabel11.Location = new System.Drawing.Point(528, 132);
		this.ultraLabel11.Name = "ultraLabel11";
		this.ultraLabel11.Size = new System.Drawing.Size(128, 29);
		this.ultraLabel11.TabIndex = 0;
		this.ultraLabel11.Text = "職稱:";
		appearance17.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance17.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel12.Appearance = appearance17;
		this.ultraLabel12.Location = new System.Drawing.Point(4, 199);
		this.ultraLabel12.Name = "ultraLabel12";
		this.ultraLabel12.Size = new System.Drawing.Size(125, 29);
		this.ultraLabel12.TabIndex = 0;
		this.ultraLabel12.Text = "資本額:";
		appearance18.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance18.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel13.Appearance = appearance18;
		this.ultraLabel13.Location = new System.Drawing.Point(267, 199);
		this.ultraLabel13.Name = "ultraLabel13";
		this.ultraLabel13.Size = new System.Drawing.Size(126, 29);
		this.ultraLabel13.TabIndex = 0;
		this.ultraLabel13.Text = "成立時間:";
		appearance19.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance19.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel14.Appearance = appearance19;
		this.ultraLabel14.Location = new System.Drawing.Point(528, 199);
		this.ultraLabel14.Name = "ultraLabel14";
		this.ultraLabel14.Size = new System.Drawing.Size(128, 29);
		this.ultraLabel14.TabIndex = 0;
		this.ultraLabel14.Text = "發照日期:";
		appearance20.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance20.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel15.Appearance = appearance20;
		this.ultraLabel15.Location = new System.Drawing.Point(4, 232);
		this.ultraLabel15.Name = "ultraLabel15";
		this.ultraLabel15.Size = new System.Drawing.Size(125, 28);
		this.ultraLabel15.TabIndex = 0;
		this.ultraLabel15.Text = "銀行代碼:";
		appearance21.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance21.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel16.Appearance = appearance21;
		this.ultraLabel16.Location = new System.Drawing.Point(267, 232);
		this.ultraLabel16.Name = "ultraLabel16";
		this.ultraLabel16.Size = new System.Drawing.Size(126, 28);
		this.ultraLabel16.TabIndex = 0;
		this.ultraLabel16.Text = "廠商英文名稱:";
		appearance22.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance22.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel17.Appearance = appearance22;
		this.ultraLabel17.Location = new System.Drawing.Point(267, 264);
		this.ultraLabel17.Name = "ultraLabel17";
		this.ultraLabel17.Size = new System.Drawing.Size(126, 26);
		this.ultraLabel17.TabIndex = 0;
		this.ultraLabel17.Text = "電子郵件信箱:";
		appearance23.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance23.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel18.Appearance = appearance23;
		this.ultraLabel18.Location = new System.Drawing.Point(4, 264);
		this.ultraLabel18.Name = "ultraLabel18";
		this.ultraLabel18.Size = new System.Drawing.Size(125, 26);
		this.ultraLabel18.TabIndex = 0;
		this.ultraLabel18.Text = "銀行帳號:";
		appearance24.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance24.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel19.Appearance = appearance24;
		this.ultraLabel19.Location = new System.Drawing.Point(4, 294);
		this.ultraLabel19.Name = "ultraLabel19";
		this.ultraLabel19.Size = new System.Drawing.Size(125, 25);
		this.ultraLabel19.TabIndex = 0;
		this.ultraLabel19.Text = "處份截止日期:";
		appearance25.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance25.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel20.Appearance = appearance25;
		this.ultraLabel20.Location = new System.Drawing.Point(267, 294);
		this.ultraLabel20.Name = "ultraLabel20";
		this.ultraLabel20.Size = new System.Drawing.Size(126, 25);
		this.ultraLabel20.TabIndex = 0;
		this.ultraLabel20.Text = "登記合格日期:";
		appearance26.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance26.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel21.Appearance = appearance26;
		this.ultraLabel21.Location = new System.Drawing.Point(528, 294);
		this.ultraLabel21.Name = "ultraLabel21";
		this.ultraLabel21.Size = new System.Drawing.Size(128, 25);
		this.ultraLabel21.TabIndex = 0;
		this.ultraLabel21.Text = "發證日期:";
		appearance27.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance27.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel22.Appearance = appearance27;
		this.ultraLabel22.Location = new System.Drawing.Point(4, 323);
		this.ultraLabel22.Name = "ultraLabel22";
		this.ultraLabel22.Size = new System.Drawing.Size(125, 30);
		this.ultraLabel22.TabIndex = 0;
		this.ultraLabel22.Text = "所屬地區:";
		appearance28.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance28.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel23.Appearance = appearance28;
		this.ultraLabel23.Location = new System.Drawing.Point(267, 323);
		this.ultraLabel23.Name = "ultraLabel23";
		this.ultraLabel23.Size = new System.Drawing.Size(126, 30);
		this.ultraLabel23.TabIndex = 0;
		this.ultraLabel23.Text = "工廠地址:";
		appearance29.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance29.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel24.Appearance = appearance29;
		this.ultraLabel24.Location = new System.Drawing.Point(4, 357);
		this.ultraLabel24.Name = "ultraLabel24";
		this.ultraLabel24.Size = new System.Drawing.Size(125, 29);
		this.ultraLabel24.TabIndex = 0;
		this.ultraLabel24.Text = "廠商簡碼:";
		appearance30.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance30.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel25.Appearance = appearance30;
		this.ultraLabel25.Location = new System.Drawing.Point(267, 357);
		this.ultraLabel25.Name = "ultraLabel25";
		this.ultraLabel25.Size = new System.Drawing.Size(126, 29);
		this.ultraLabel25.TabIndex = 0;
		this.ultraLabel25.Text = "工廠登記證號:";
		appearance31.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance31.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel26.Appearance = appearance31;
		this.ultraLabel26.Location = new System.Drawing.Point(528, 357);
		this.ultraLabel26.Name = "ultraLabel26";
		this.ultraLabel26.Size = new System.Drawing.Size(128, 29);
		this.ultraLabel26.TabIndex = 0;
		this.ultraLabel26.Text = "投票權:";
		appearance32.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance32.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel27.Appearance = appearance32;
		this.ultraLabel27.Location = new System.Drawing.Point(4, 390);
		this.ultraLabel27.Name = "ultraLabel27";
		this.ultraLabel27.Size = new System.Drawing.Size(125, 29);
		this.ultraLabel27.TabIndex = 0;
		this.ultraLabel27.Text = "營業登記證:";
		appearance33.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance33.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel28.Appearance = appearance33;
		this.ultraLabel28.Location = new System.Drawing.Point(4, 423);
		this.ultraLabel28.Name = "ultraLabel28";
		this.ultraLabel28.Size = new System.Drawing.Size(125, 29);
		this.ultraLabel28.TabIndex = 0;
		this.ultraLabel28.Text = "表格編號:";
		appearance34.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance34.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel29.Appearance = appearance34;
		this.ultraLabel29.Location = new System.Drawing.Point(4, 456);
		this.ultraLabel29.Name = "ultraLabel29";
		this.ultraLabel29.Size = new System.Drawing.Size(125, 28);
		this.ultraLabel29.TabIndex = 0;
		this.ultraLabel29.Text = "組織型態:";
		appearance35.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance35.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel30.Appearance = appearance35;
		this.ultraLabel30.Location = new System.Drawing.Point(267, 390);
		this.ultraLabel30.Name = "ultraLabel30";
		this.ultraLabel30.Size = new System.Drawing.Size(126, 29);
		this.ultraLabel30.TabIndex = 0;
		this.ultraLabel30.Text = "公會會員證:";
		appearance36.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance36.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel31.Appearance = appearance36;
		this.ultraLabel31.Location = new System.Drawing.Point(267, 423);
		this.ultraLabel31.Name = "ultraLabel31";
		this.ultraLabel31.Size = new System.Drawing.Size(126, 29);
		this.ultraLabel31.TabIndex = 0;
		this.ultraLabel31.Text = "執業等級:";
		appearance37.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance37.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel32.Appearance = appearance37;
		this.ultraLabel32.Location = new System.Drawing.Point(267, 456);
		this.ultraLabel32.Name = "ultraLabel32";
		this.ultraLabel32.Size = new System.Drawing.Size(126, 28);
		this.ultraLabel32.TabIndex = 0;
		this.ultraLabel32.Text = "登記專長:";
		appearance38.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance38.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel33.Appearance = appearance38;
		this.ultraLabel33.Location = new System.Drawing.Point(528, 390);
		this.ultraLabel33.Name = "ultraLabel33";
		this.ultraLabel33.Size = new System.Drawing.Size(128, 29);
		this.ultraLabel33.TabIndex = 0;
		this.ultraLabel33.Text = "本處登記編號:";
		this.ultraLabel33.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		appearance39.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance39.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel34.Appearance = appearance39;
		this.ultraLabel34.Location = new System.Drawing.Point(528, 423);
		this.ultraLabel34.Name = "ultraLabel34";
		this.ultraLabel34.Size = new System.Drawing.Size(128, 29);
		this.ultraLabel34.TabIndex = 0;
		this.ultraLabel34.Text = "郵遞區號:";
		this.txtBoss.Location = new System.Drawing.Point(133, 100);
		this.txtBoss.Name = "txtBoss";
		this.txtBoss.Size = new System.Drawing.Size(130, 24);
		this.txtBoss.TabIndex = 1;
		this.txtBoss.Text = "ultraTextEditor1";
		this.txtBoss.Validating += new System.ComponentModel.CancelEventHandler(txtInvoice_No_Validating);
		this.txtBoss.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.txtCapital.Location = new System.Drawing.Point(133, 199);
		this.txtCapital.Name = "txtCapital";
		this.txtCapital.Size = new System.Drawing.Size(130, 24);
		this.txtCapital.TabIndex = 1;
		this.txtCapital.Text = "ultraTextEditor1";
		this.txtCapital.Validating += new System.ComponentModel.CancelEventHandler(txtInvoice_No_Validating);
		this.txtBankNo.Location = new System.Drawing.Point(133, 232);
		this.txtBankNo.Name = "txtBankNo";
		this.txtBankNo.Size = new System.Drawing.Size(130, 24);
		this.txtBankNo.TabIndex = 1;
		this.txtBankNo.Text = "ultraTextEditor1";
		this.txtBankNo.Validating += new System.ComponentModel.CancelEventHandler(txtInvoice_No_Validating);
		this.txtBankID.Location = new System.Drawing.Point(133, 264);
		this.txtBankID.Name = "txtBankID";
		this.txtBankID.Size = new System.Drawing.Size(130, 24);
		this.txtBankID.TabIndex = 1;
		this.txtBankID.Text = "ultraTextEditor1";
		this.txtBankID.Validating += new System.ComponentModel.CancelEventHandler(txtInvoice_No_Validating);
		this.txtVendor_SCode.Location = new System.Drawing.Point(133, 357);
		this.txtVendor_SCode.Name = "txtVendor_SCode";
		this.txtVendor_SCode.Size = new System.Drawing.Size(130, 24);
		this.txtVendor_SCode.TabIndex = 1;
		this.txtVendor_SCode.Text = "ultraTextEditor1";
		this.txtVendor_SCode.Validating += new System.ComponentModel.CancelEventHandler(txtInvoice_No_Validating);
		this.txtVendor_SCode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.txtTrade_License.Location = new System.Drawing.Point(133, 390);
		this.txtTrade_License.Name = "txtTrade_License";
		this.txtTrade_License.Size = new System.Drawing.Size(130, 24);
		this.txtTrade_License.TabIndex = 1;
		this.txtTrade_License.Text = "ultraTextEditor1";
		this.txtTrade_License.Validating += new System.ComponentModel.CancelEventHandler(txtInvoice_No_Validating);
		this.txtTrade_License.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.txtVendor_Tmp.Location = new System.Drawing.Point(133, 423);
		this.txtVendor_Tmp.Name = "txtVendor_Tmp";
		this.txtVendor_Tmp.Size = new System.Drawing.Size(130, 24);
		this.txtVendor_Tmp.TabIndex = 1;
		this.txtVendor_Tmp.Text = "ultraTextEditor1";
		this.txtVendor_Tmp.Validating += new System.ComponentModel.CancelEventHandler(txtInvoice_No_Validating);
		this.txtVendor_Tmp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.txtTel_Boss.Location = new System.Drawing.Point(660, 100);
		this.txtTel_Boss.Name = "txtTel_Boss";
		this.txtTel_Boss.Size = new System.Drawing.Size(125, 24);
		this.txtTel_Boss.TabIndex = 1;
		this.txtTel_Boss.Text = "ultraTextEditor1";
		this.txtTel_Boss.Validating += new System.ComponentModel.CancelEventHandler(txtInvoice_No_Validating);
		this.txtFax.Location = new System.Drawing.Point(133, 132);
		this.txtFax.Name = "txtFax";
		this.txtFax.Size = new System.Drawing.Size(130, 24);
		this.txtFax.TabIndex = 1;
		this.txtFax.Text = "ultraTextEditor1";
		this.txtFax.Validating += new System.ComponentModel.CancelEventHandler(txtInvoice_No_Validating);
		this.txtTel_Liai.Location = new System.Drawing.Point(397, 165);
		this.txtTel_Liai.Name = "txtTel_Liai";
		this.txtTel_Liai.Size = new System.Drawing.Size(127, 24);
		this.txtTel_Liai.TabIndex = 1;
		this.txtTel_Liai.Text = "ultraTextEditor1";
		this.txtTel_Liai.Validating += new System.ComponentModel.CancelEventHandler(txtInvoice_No_Validating);
		this.txtTel_Liai2.Location = new System.Drawing.Point(660, 165);
		this.txtTel_Liai2.Name = "txtTel_Liai2";
		this.txtTel_Liai2.Size = new System.Drawing.Size(125, 24);
		this.txtTel_Liai2.TabIndex = 1;
		this.txtTel_Liai2.Text = "ultraTextEditor1";
		this.txtTel_Liai2.Validating += new System.ComponentModel.CancelEventHandler(txtInvoice_No_Validating);
		this.txtETitle.Location = new System.Drawing.Point(397, 232);
		this.txtETitle.Name = "txtETitle";
		this.txtETitle.Size = new System.Drawing.Size(388, 24);
		this.txtETitle.TabIndex = 1;
		this.txtETitle.Text = "ultraTextEditor1";
		this.txtETitle.Validating += new System.ComponentModel.CancelEventHandler(txtInvoice_No_Validating);
		this.txtEMail.Location = new System.Drawing.Point(397, 264);
		this.txtEMail.Name = "txtEMail";
		this.txtEMail.Size = new System.Drawing.Size(388, 24);
		this.txtEMail.TabIndex = 1;
		this.txtEMail.Text = "ultraTextEditor1";
		this.txtEMail.Validating += new System.ComponentModel.CancelEventHandler(txtInvoice_No_Validating);
		this.txtFAddress.Location = new System.Drawing.Point(397, 323);
		this.txtFAddress.Name = "txtFAddress";
		this.txtFAddress.Size = new System.Drawing.Size(388, 24);
		this.txtFAddress.TabIndex = 1;
		this.txtFAddress.Text = "ultraTextEditor1";
		this.txtFAddress.Validating += new System.ComponentModel.CancelEventHandler(txtInvoice_No_Validating);
		this.txtFAddress.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		dateButton2.Caption = "今天";
		this.dpOpenTime.DateButtons.Add(dateButton2);
		this.dpOpenTime.Location = new System.Drawing.Point(397, 199);
		this.dpOpenTime.Name = "dpOpenTime";
		this.dpOpenTime.NonAutoSizeHeight = 21;
		this.dpOpenTime.NullDateLabel = "";
		this.dpOpenTime.Size = new System.Drawing.Size(127, 21);
		this.dpOpenTime.TabIndex = 2;
		this.dpOpenTime.Value = resources.GetObject("dpOpenTime.Value");
		dateButton3.Caption = "今天";
		this.dpDRegister.DateButtons.Add(dateButton3);
		this.dpDRegister.Location = new System.Drawing.Point(397, 294);
		this.dpDRegister.Name = "dpDRegister";
		this.dpDRegister.NonAutoSizeHeight = 21;
		this.dpDRegister.NullDateLabel = "";
		this.dpDRegister.Size = new System.Drawing.Size(127, 21);
		this.dpDRegister.TabIndex = 2;
		this.dpDRegister.Value = resources.GetObject("dpDRegister.Value");
		dateButton4.Caption = "今天";
		this.dpDTrade_License.DateButtons.Add(dateButton4);
		this.dpDTrade_License.Location = new System.Drawing.Point(660, 294);
		this.dpDTrade_License.Name = "dpDTrade_License";
		this.dpDTrade_License.NonAutoSizeHeight = 21;
		this.dpDTrade_License.NullDateLabel = "";
		this.dpDTrade_License.Size = new System.Drawing.Size(125, 21);
		this.dpDTrade_License.TabIndex = 2;
		this.dpDTrade_License.Value = resources.GetObject("dpDTrade_License.Value");
		dateButton5.Caption = "今天";
		this.dpDComp_License.DateButtons.Add(dateButton5);
		this.dpDComp_License.Location = new System.Drawing.Point(660, 199);
		this.dpDComp_License.Name = "dpDComp_License";
		this.dpDComp_License.NonAutoSizeHeight = 21;
		this.dpDComp_License.NullDateLabel = "";
		this.dpDComp_License.Size = new System.Drawing.Size(125, 21);
		this.dpDComp_License.TabIndex = 2;
		this.dpDComp_License.Value = resources.GetObject("dpDComp_License.Value");
		this.txtFLicense.Location = new System.Drawing.Point(397, 357);
		this.txtFLicense.Name = "txtFLicense";
		this.txtFLicense.Size = new System.Drawing.Size(127, 24);
		this.txtFLicense.TabIndex = 1;
		this.txtFLicense.Text = "ultraTextEditor1";
		this.txtFLicense.Validating += new System.ComponentModel.CancelEventHandler(txtInvoice_No_Validating);
		this.txtFLicense.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.txtUnion_License.Location = new System.Drawing.Point(397, 390);
		this.txtUnion_License.Name = "txtUnion_License";
		this.txtUnion_License.Size = new System.Drawing.Size(127, 24);
		this.txtUnion_License.TabIndex = 1;
		this.txtUnion_License.Text = "ultraTextEditor1";
		this.txtUnion_License.Validating += new System.ComponentModel.CancelEventHandler(txtInvoice_No_Validating);
		this.txtUnion_License.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.dpLevel_Business.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
		valueListItem13.DataValue = "甲";
		valueListItem14.DataValue = "乙";
		valueListItem15.DataValue = "丙";
		valueListItem15.DisplayText = "丙";
		valueListItem16.DataValue = "無";
		valueListItem16.DisplayText = "無";
		this.dpLevel_Business.Items.Add(valueListItem13);
		this.dpLevel_Business.Items.Add(valueListItem14);
		this.dpLevel_Business.Items.Add(valueListItem15);
		this.dpLevel_Business.Items.Add(valueListItem16);
		this.dpLevel_Business.Location = new System.Drawing.Point(397, 423);
		this.dpLevel_Business.Name = "dpLevel_Business";
		this.dpLevel_Business.Size = new System.Drawing.Size(127, 24);
		this.dpLevel_Business.TabIndex = 3;
		this.dpLevel_Business.Text = null;
		this.dpMail_Area_No.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
		this.dpMail_Area_No.Location = new System.Drawing.Point(660, 423);
		this.dpMail_Area_No.MaxDropDownItems = 15;
		this.dpMail_Area_No.Name = "dpMail_Area_No";
		this.dpMail_Area_No.Size = new System.Drawing.Size(125, 24);
		this.dpMail_Area_No.TabIndex = 3;
		this.dpMail_Area_No.Text = null;
		this.txtVendor_Rsea.Location = new System.Drawing.Point(660, 390);
		this.txtVendor_Rsea.Name = "txtVendor_Rsea";
		this.txtVendor_Rsea.Size = new System.Drawing.Size(125, 24);
		this.txtVendor_Rsea.TabIndex = 1;
		this.txtVendor_Rsea.Text = "ultraTextEditor1";
		this.txtVendor_Rsea.Validating += new System.ComponentModel.CancelEventHandler(txtInvoice_No_Validating);
		this.txtProfession.Location = new System.Drawing.Point(397, 456);
		this.txtProfession.Name = "txtProfession";
		this.txtProfession.Size = new System.Drawing.Size(127, 24);
		this.txtProfession.TabIndex = 1;
		this.txtProfession.Text = "ultraTextEditor1";
		this.txtProfession.Validating += new System.ComponentModel.CancelEventHandler(txtInvoice_No_Validating);
		this.txtProfession.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.txtLiaison.Location = new System.Drawing.Point(397, 132);
		this.txtLiaison.Name = "txtLiaison";
		this.txtLiaison.Size = new System.Drawing.Size(127, 24);
		this.txtLiaison.TabIndex = 1;
		this.txtLiaison.Text = "ultraTextEditor1";
		this.txtLiaison.Validating += new System.ComponentModel.CancelEventHandler(txtInvoice_No_Validating);
		this.txtLiaison.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.txtLiai_Position.Location = new System.Drawing.Point(660, 132);
		this.txtLiai_Position.Name = "txtLiai_Position";
		this.txtLiai_Position.Size = new System.Drawing.Size(125, 24);
		this.txtLiai_Position.TabIndex = 1;
		this.txtLiai_Position.Text = "ultraTextEditor1";
		this.txtLiai_Position.Validating += new System.ComponentModel.CancelEventHandler(txtInvoice_No_Validating);
		this.txtLiai_Position.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		appearance40.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance40.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel35.Appearance = appearance40;
		this.ultraLabel35.Location = new System.Drawing.Point(267, 100);
		this.ultraLabel35.Name = "ultraLabel35";
		this.ultraLabel35.Size = new System.Drawing.Size(126, 28);
		this.ultraLabel35.TabIndex = 0;
		this.ultraLabel35.Text = "負責人身份字號:";
		this.txtBoss_ID.Location = new System.Drawing.Point(397, 100);
		this.txtBoss_ID.Name = "txtBoss_ID";
		this.txtBoss_ID.Size = new System.Drawing.Size(127, 24);
		this.txtBoss_ID.TabIndex = 1;
		this.txtBoss_ID.Text = "txtBoss_ID";
		this.txtBoss_ID.Validating += new System.ComponentModel.CancelEventHandler(txtInvoice_No_Validating);
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.ClientSize = new System.Drawing.Size(790, 563);
		base.Controls.Add(this.panel3);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel1);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormSys_C_Edit";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "FormSys_C_Edit";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormSys_C_Edit_KeyDown);
		base.Load += new System.EventHandler(FormSys_C_Edit_Load);
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.c1Sizer1).EndInit();
		this.c1Sizer1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dpArea).EndInit();
		((System.ComponentModel.ISupportInitialize)this.opRight_Bid).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dpOrg_Type).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dpDPunish_End).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtInvoice_No).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTitle).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtShortitle).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAddress).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBoss).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCapital).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBankNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBankID).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVendor_SCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTrade_License).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVendor_Tmp).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTel_Boss).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtFax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTel_Liai).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTel_Liai2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtETitle).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEMail).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtFAddress).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dpOpenTime).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dpDRegister).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dpDTrade_License).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dpDComp_License).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtFLicense).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtUnion_License).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dpLevel_Business).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dpMail_Area_No).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVendor_Rsea).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtProfession).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtLiaison).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtLiai_Position).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBoss_ID).EndInit();
		base.ResumeLayout(false);
	}

	private void FormSys_C_Edit_Load(object sender, EventArgs e)
	{
		EnableCOMS = SysConfig.SysComsEnable;
		ClearControls_Text();
		if (F_EditMode == "EDIT")
		{
			Text = "廠商資料編輯";
			lblCaption.Text = " 系統維護--廠商資料編輯";
			LoadData();
		}
		else if (F_EditMode == "NEW")
		{
			Text = "廠商資料新增";
			lblCaption.Text = " 系統維護--廠商資料新增";
		}
	}

	private void LoadData()
	{
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("廠商資料--讀取");
		Archnowledge.Pcces.BUDClass.Sublet SubletCom = new Archnowledge.Pcces.BUDClass.Sublet(aArr);
		SubletCom.ps_invoice_no = F_Invoice_No;
		SubletCom._IsArchCOMS = EnableCOMS;
		DT1 = SubletCom.ListItem(" invoice_no ='" + F_Invoice_No + "' ");
		if (DT1.Rows.Count > 0)
		{
			txtInvoice_No.Text = DT1.Rows[0]["invoice_no"].ToString().Trim();
			txtTitle.Text = DT1.Rows[0]["title"].ToString().Trim();
			txtBoss.Text = DT1.Rows[0]["boss"].ToString().Trim();
			txtTel_Boss.Text = DT1.Rows[0]["tel_boss"].ToString().Trim();
			txtAddress.Text = DT1.Rows[0]["address"].ToString().Trim();
			txtLiaison.Text = DT1.Rows[0]["liaison"].ToString().Trim();
			txtTel_Liai.Text = DT1.Rows[0]["tel_liai"].ToString().Trim();
			txtFax.Text = DT1.Rows[0]["fax"].ToString().Trim();
			txtLiai_Position.Text = DT1.Rows[0]["liai_position"].ToString().Trim();
			txtCapital.Text = DT1.Rows[0]["capital"].ToString().Trim();
			txtBankNo.Text = DT1.Rows[0]["bankno"].ToString().Trim();
			txtBankID.Text = DT1.Rows[0]["bankid"].ToString().Trim();
			txtETitle.Text = DT1.Rows[0]["etitle"].ToString().Trim();
			txtEMail.Text = DT1.Rows[0]["email"].ToString().Trim();
			txtShortitle.Text = DT1.Rows[0]["shortitle"].ToString().Trim();
			txtFAddress.Text = DT1.Rows[0]["faddress"].ToString().Trim();
			txtVendor_SCode.Text = DT1.Rows[0]["vendor_scode"].ToString().Trim();
			txtFLicense.Text = DT1.Rows[0]["flicense"].ToString().Trim();
			txtTrade_License.Text = DT1.Rows[0]["trade_license"].ToString().Trim();
			txtUnion_License.Text = DT1.Rows[0]["union_license"].ToString().Trim();
			txtVendor_Rsea.Text = DT1.Rows[0]["vendor_rsea"].ToString().Trim();
			txtVendor_Tmp.Text = DT1.Rows[0]["vendor_tmp"].ToString().Trim();
			txtProfession.Text = DT1.Rows[0]["profession"].ToString().Trim();
			txtTel_Liai2.Text = DT1.Rows[0]["tel_liai2"].ToString().Trim();
			dpOpenTime.Value = DT1.Rows[0]["opentime"].ToString().Trim();
			dpDRegister.Value = DT1.Rows[0]["dregister"].ToString().Trim();
			dpDPunish_End.Value = DT1.Rows[0]["dpunish_end"].ToString().Trim();
			dpDComp_License.Value = DT1.Rows[0]["dcomp_license"].ToString().Trim();
			dpDTrade_License.Value = DT1.Rows[0]["dtrade_license"].ToString().Trim();
			dpArea.Value = DT1.Rows[0]["area"].ToString().Trim();
			dpOrg_Type.Value = DT1.Rows[0]["org_type"].ToString().Trim();
			dpLevel_Business.Value = DT1.Rows[0]["level_business"].ToString().Trim();
			dpMail_Area_No.Value = DT1.Rows[0]["mail_area_no"].ToString().Trim();
			opRight_Bid.CheckedIndex = ((!(DT1.Rows[0]["right_bid"].ToString().Trim() == "Y")) ? 1 : 0);
			txtBoss_ID.Text = DT1.Rows[0]["boss_id"].ToString().Trim();
		}
	}

	private void ClearControls_Text()
	{
		foreach (Control txtBox in c1Sizer1.Controls)
		{
			if (txtBox is UltraTextEditor)
			{
				(txtBox as UltraTextEditor).Text = "";
			}
		}
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = "PccAdmin";
		DataTable DT_ZIP = DBCLS.GetUserDefine("Select * from UserDefind Where kind ='ZipCode'");
		if (DT_ZIP.Rows.Count > 0)
		{
			for (int i = 0; i < DT_ZIP.Rows.Count; i++)
			{
				dpMail_Area_No.Items.Add(DT_ZIP.Rows[i]["sno"].ToString(), DT_ZIP.Rows[i]["cString"].ToString());
			}
		}
	}

	private void c1Sizer1_Click(object sender, EventArgs e)
	{
	}

	private void ultraLabel6_Click(object sender, EventArgs e)
	{
	}

	private void Btn_OK_Click(object sender, EventArgs e)
	{
		if (txtInvoice_No.Text.Trim() == "")
		{
			MessageBox.Show(this, "廠商統編不可空白", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtInvoice_No.Focus();
			return;
		}
		if (!(txtInvoice_No.Text.Trim().Substring(txtInvoice_No.Text.Trim().Length - 1).ToUpper() == "A"))
		{
			if (txtInvoice_No.Text.Trim().Substring(txtInvoice_No.Text.Trim().Length - 1).ToUpper() != "A" && txtInvoice_No.Text.Trim().Length < 8)
			{
				MessageBox.Show(this, "統一編號應為8碼，若希望系統忽略檢查請於末碼加上A", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtInvoice_No.Focus();
				return;
			}
			if (txtInvoice_No.Text.Trim().Length > 8 && !PubTools.Chk_Invoice_No(txtInvoice_No.Text.Trim()))
			{
				MessageBox.Show(this, "統編不符合規則，請重新輸入", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtInvoice_No.Focus();
				return;
			}
		}
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("廠商資料--異動");
		Archnowledge.Pcces.BUDClass.Sublet SubletCom = new Archnowledge.Pcces.BUDClass.Sublet(aArr);
		SubletCom.ps_invoice_no = txtInvoice_No.Text.Trim();
		SubletCom.ps_title = txtTitle.Text.Trim();
		SubletCom.ps_boss = txtBoss.Text.Trim();
		SubletCom.ps_boss_id = txtBoss_ID.Text.Trim();
		SubletCom.ps_tel_boss = txtTel_Boss.Text.Trim();
		SubletCom.ps_address = txtAddress.Text.Trim();
		SubletCom.ps_liaison = txtLiaison.Text.Trim();
		SubletCom.ps_tel_liai = txtTel_Liai.Text.Trim();
		SubletCom.ps_fax = txtFax.Text.Trim();
		SubletCom.ps_liai_position = txtLiai_Position.Text.Trim();
		SubletCom.ps_capital = txtCapital.Text.Trim();
		SubletCom.ps_bankno = txtBankNo.Text.Trim();
		SubletCom.ps_bankid = txtBankID.Text.Trim();
		SubletCom.ps_etitle = txtETitle.Text.Trim();
		SubletCom.ps_email = txtEMail.Text.Trim();
		SubletCom.ps_shortitle = txtShortitle.Text.Trim();
		SubletCom.ps_faddress = txtFAddress.Text.Trim();
		SubletCom.ps_vendor_scode = txtVendor_SCode.Text.Trim();
		SubletCom.ps_flicense = txtFLicense.Text.Trim();
		SubletCom.ps_trade_license = txtTrade_License.Text.Trim();
		SubletCom.ps_union_license = txtUnion_License.Text.Trim();
		SubletCom.ps_vendor_rsea = txtVendor_Rsea.Text.Trim();
		SubletCom.ps_vendor_tmp = txtVendor_Tmp.Text.Trim();
		SubletCom.ps_profession = txtProfession.Text.Trim();
		SubletCom.ps_tel_liai2 = txtTel_Liai2.Text.Trim();
		SubletCom.ps_opentime = $"{dpOpenTime.Value:yyyyMMdd}";
		SubletCom.ps_dregister = $"{dpDRegister.Value:yyyyMMdd}";
		SubletCom.ps_dpunish_end = $"{dpDPunish_End.Value:yyyyMMdd}";
		SubletCom.ps_dcomp_license = $"{dpDComp_License.Value:yyyyMMdd}";
		SubletCom.ps_dtrade_license = $"{dpDTrade_License.Value:yyyyMMdd}";
		SubletCom.ps_area = ((dpArea.Text != null) ? dpArea.Text.ToString() : "");
		SubletCom.ps_right_bid = ((opRight_Bid.CheckedIndex == 0) ? "Y" : "N");
		SubletCom.ps_level_business = ((dpLevel_Business.Text != null) ? dpLevel_Business.Text.ToString() : "");
		SubletCom.ps_mail_area_no = ((dpMail_Area_No.Text == null) ? "" : ((dpMail_Area_No.Text.ToString().Length >= 3) ? dpMail_Area_No.Text.Substring(0, 3) : ""));
		SubletCom.ps_org_type = ((dpOrg_Type.Text != null) ? dpOrg_Type.Text.ToString() : "");
		int iTrans = 0;
		if (F_EditMode == "NEW")
		{
			SubletCom.ps_ins_dt = DateTime.Now.ToString();
			SubletCom.ps_ins_usr = F_UserID;
			SubletCom._IsArchCOMS = EnableCOMS;
			iTrans = SubletCom.InseItem();
			if (iTrans == -1)
			{
				string sWarning = "廠商統編重複，請重新輸入!";
				MessageBox.Show(this, sWarning, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
		}
		else if (F_EditMode == "EDIT")
		{
			SubletCom.ps_ud_dt = DateTime.Now.ToString();
			SubletCom.ps_ud_usr = F_UserID;
			SubletCom._IsArchCOMS = EnableCOMS;
			iTrans = SubletCom.UpdItem();
			if (iTrans == -1)
			{
				string sWarning = "更新失敗，請檢查資料!";
				MessageBox.Show(this, sWarning, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
		}
		if (iTrans == -2)
		{
			string sWarning = "廠商統編錯誤，請檢查資料!";
			MessageBox.Show(this, sWarning, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else
		{
			PubTools.WriteRoughlyLog(aArr);
			base.DialogResult = DialogResult.OK;
		}
	}

	private void txtInvoice_No_Validating(object sender, CancelEventArgs e)
	{
		if (base.DialogResult == DialogResult.Cancel)
		{
			return;
		}
		if (!CommonMethods.CheckValidString((sender as UltraTextEditor).Text))
		{
			e.Cancel = true;
		}
		switch ((sender as Control).Name)
		{
		case "txtInvoice_No":
			if (!CommonMethods.IsStrByteLenValid(txtInvoice_No.Text, 10))
			{
				MessageBox.Show(this, "廠商統編的長度不可超過 10 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtInvoice_No.Focus();
			}
			if (txtInvoice_No.Text.Trim().Length > 0 && !(txtInvoice_No.Text.Trim().Substring(txtInvoice_No.Text.Trim().Length - 1).ToUpper() == "A") && txtInvoice_No.Text.Trim().Length < 8)
			{
				MessageBox.Show(this, "統一編號應為8碼，若希望系統忽略檢查請於末碼加上A", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			break;
		case "txtShortitle":
			if (!CommonMethods.IsStrByteLenValid(txtShortitle.Text, 10))
			{
				MessageBox.Show(this, "廠商簡稱的長度不可超過 10 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtShortitle.Focus();
			}
			break;
		case "txtTitle":
			if (!CommonMethods.IsStrByteLenValid(txtTitle.Text, 60))
			{
				MessageBox.Show(this, "廠商名稱的長度不可超過 60 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtTitle.Focus();
			}
			break;
		case "txtAddress":
			if (!CommonMethods.IsStrByteLenValid(txtAddress.Text, 60))
			{
				MessageBox.Show(this, "廠商地址的長度不可超過 60 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtAddress.Focus();
			}
			break;
		case "txtBoss":
			if (!CommonMethods.IsStrByteLenValid(txtBoss.Text, 10))
			{
				MessageBox.Show(this, "負責人的長度不可超過 10 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtBoss.Focus();
			}
			break;
		case "txtBoss_ID":
			if (!CommonMethods.IsStrByteLenValid(txtBoss_ID.Text, 10))
			{
				MessageBox.Show(this, "負責人身份證字號的長度不可超過 10 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtBoss_ID.Focus();
			}
			break;
		case "txtLiaison":
			if (!CommonMethods.IsStrByteLenValid(txtLiaison.Text, 10))
			{
				MessageBox.Show(this, "聯絡人的長度不可超過 10 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtLiaison.Focus();
			}
			break;
		case "txtTel_Boss":
			if (!CommonMethods.IsStrByteLenValid(txtTel_Boss.Text, 20))
			{
				MessageBox.Show(this, "公司電話的長度不可超過 20 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtTel_Boss.Focus();
			}
			break;
		case "txtTel_Liai":
			if (!CommonMethods.IsStrByteLenValid(txtTel_Liai.Text, 50))
			{
				MessageBox.Show(this, "聯絡人電話1的長度不可超過 50 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtTel_Liai.Focus();
			}
			break;
		case "txtTel_Liai2":
			if (!CommonMethods.IsStrByteLenValid(txtTel_Liai2.Text, 18))
			{
				MessageBox.Show(this, "聯絡人電話2的長度不可超過 18 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtTel_Liai2.Focus();
			}
			break;
		case "txtFax":
			if (!CommonMethods.IsStrByteLenValid(txtFax.Text, 12))
			{
				MessageBox.Show(this, "傳真的長度不可超過 12 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtFax.Focus();
			}
			break;
		case "txtLiai_Position":
			if (!CommonMethods.IsStrByteLenValid(txtLiai_Position.Text, 10))
			{
				MessageBox.Show(this, "職稱的長度不可超過 10 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtLiai_Position.Focus();
			}
			break;
		case "txtCapital":
			try
			{
				double xyz = Convert.ToDouble(txtCapital.Text.Trim());
				break;
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "SysMaintain.FormSys_C_Edit.cs" + ex.Message);
				MessageBox.Show(this, "資本額輸入錯誤。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtCapital.Focus();
				break;
			}
		case "txtBankNo":
			if (!CommonMethods.IsStrByteLenValid(txtBankNo.Text, 30))
			{
				MessageBox.Show(this, "銀行代碼的長度不可超過 30 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtBankNo.Focus();
			}
			break;
		case "txtBankID":
			if (!CommonMethods.IsStrByteLenValid(txtBankID.Text, 14))
			{
				MessageBox.Show(this, "銀行帳號的長度不可超過 14 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtBankID.Focus();
			}
			break;
		case "txtETitle":
			if (!CommonMethods.IsStrByteLenValid(txtETitle.Text, 60))
			{
				MessageBox.Show(this, "廠商英文名稱的長度不可超過 60 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtETitle.Focus();
			}
			break;
		case "txtEMail":
			if (!CommonMethods.IsStrByteLenValid(txtEMail.Text, 60))
			{
				MessageBox.Show(this, "電子郵件信箱的長度不可超過 60 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtEMail.Focus();
			}
			break;
		case "txtFAddress":
			if (!CommonMethods.IsStrByteLenValid(txtFAddress.Text, 80))
			{
				MessageBox.Show(this, "工廠地址的長度不可超過 80 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtFAddress.Focus();
			}
			break;
		case "txtFLicense":
			if (!CommonMethods.IsStrByteLenValid(txtFLicense.Text, 50))
			{
				MessageBox.Show(this, "工廠登記證號的長度不可超過 50 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtFLicense.Focus();
			}
			break;
		case "txtUnion_License":
			if (!CommonMethods.IsStrByteLenValid(txtUnion_License.Text, 50))
			{
				MessageBox.Show(this, "公會會員證的長度不可超過 50 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtUnion_License.Focus();
			}
			break;
		case "txtProfession":
			if (!CommonMethods.IsStrByteLenValid(txtProfession.Text, 10))
			{
				MessageBox.Show(this, "登記專長的長度不可超過 10 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtProfession.Focus();
			}
			break;
		case "txtVendor_SCode":
			if (!CommonMethods.IsStrByteLenValid(txtVendor_SCode.Text, 8))
			{
				MessageBox.Show(this, "廠商簡碼的長度不可超過 8 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtVendor_SCode.Focus();
			}
			break;
		case "txtTrade_License":
			if (!CommonMethods.IsStrByteLenValid(txtTrade_License.Text, 50))
			{
				MessageBox.Show(this, "營業登記證的長度不可超過 50 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtTrade_License.Focus();
			}
			break;
		case "txtVendor_Tmp":
			if (!CommonMethods.IsStrByteLenValid(txtVendor_Tmp.Text, 8))
			{
				MessageBox.Show(this, "表格編號的長度不可超過 8 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtVendor_Tmp.Focus();
			}
			break;
		case "txtVendor_Rsea":
			if (!CommonMethods.IsStrByteLenValid(txtVendor_Rsea.Text, 10))
			{
				MessageBox.Show(this, "本處登記編號的長度不可超過 10 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtVendor_Rsea.Focus();
			}
			break;
		}
	}

	private void FormSys_C_Edit_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormSys_C_Edit");
		}
	}
}
