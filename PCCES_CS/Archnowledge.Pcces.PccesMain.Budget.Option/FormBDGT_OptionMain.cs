using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.DomainModule.Bid;
using Archnowledge.Pcces.DomainModule.Budget;
using Archnowledge.Pcces.DomainModule.General;
using Archnowledge.Pcces.DomainModule.LogicalBase;
using Archnowledge.Pcces.DomainModule.Sub;
using Archnowledge.Pcces.STDClass;
using C1.Win.C1Input;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTabControl;
using Infragistics.Win.UltraWinTabs;

namespace Archnowledge.Pcces.PccesMain.Budget.Option;

public class FormBDGT_OptionMain : Form
{
	private const string iniFile = "OptionSet.ini";

	private Container components = null;

	private Panel panel1;

	private Panel panel9;

	private GroupBox groupBox5;

	private UltraTabControl Tab_Ctrl;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl Tab_B;

	private UltraLabel ultraLabel10;

	private UltraButton btnOK;

	private UltraButton btnCancel;

	private FolderBrowserDialog changeBackupPathDialog;

	private UltraTabPageControl ultraTabPageControl1;

	private UltraTabPageControl ultraTabPageControl2;

	private Panel panel2;

	private C1PictureBox c1PictureBox1;

	private C1PictureBox c1PictureBox2;

	private UltraLabel ultraLabel3;

	private UltraTabPageControl ultraTabPageControl3;

	private UltraLabel ultraLabel5;

	private UltraLabel ultraLabel6;

	private C1PictureBox c1PictureBox3;

	private C1PictureBox c1PictureBox4;

	private Panel panel3;

	private UltraCheckEditor chk_AutoNum;

	private UltraCheckEditor chk_Number;

	private UltraCheckEditor chk_forDeleteNoUsedItem;

	private UltraLabel ultraLabel22;

	private UltraLabel lbCustomizedVariable;

	private UltraCheckEditor chkBDGT_PCals;

	private UltraCheckEditor chkBDGT_AutoSave;

	private NumericUpDown BDGT_Duration;

	private UltraLabel lbBackUpFilePath;

	private UltraLabel ultraLabel1;

	private UltraCheckEditor chk_DeleteAutoSave;

	private UltraButton btnChangeBackupPath;

	private UltraCheckEditor chkMrsBItem;

	private UltraCheckEditor chk_Ana_UseNewOpen;

	private UltraCheckEditor chkUseNewMrsB;

	private Panel panel4;

	private UltraLabel ultraLabel7;

	private UltraLabel ultraLabel8;

	private C1PictureBox c1PictureBox5;

	private C1PictureBox c1PictureBox6;

	private UltraLabel ultraLabel13;

	private UltraOptionSet rbCalculationMethod;

	private Panel panel5;

	private UltraLabel ultraLabel2;

	private UltraLabel ultraLabel4;

	private C1PictureBox c1PictureBox7;

	private C1PictureBox c1PictureBox8;

	private UltraCheckEditor cbShowToolTipOnNarrowColumn;

	private UltraTextEditor tbSponsor;

	private UltraButton btnPickSponsor;

	private UltraLabel ultraLabel11;

	private UltraButton ultraButton3;

	private UltraLabel ultraLabel12;

	private UltraLabel ultraLabel15;

	private UltraButton BtnRecover;

	private UltraLabel ultraLabel16;

	private UltraLabel ultraLabel17;

	private Panel panel6;

	private Panel panel7;

	private Panel panel8;

	private Panel panel10;

	private UltraLabel ultraLabel18;

	private UltraLabel ultraLabel9;

	private C1PictureBox c1PictureBox9;

	private C1PictureBox c1PictureBox10;

	private C1PictureBox c1PictureBox11;

	private C1PictureBox c1PictureBox12;

	private UltraLabel ultraLabel30;

	private UltraCheckEditor chkAnalyis;

	private UltraCheckEditor chkIsDetail;

	private UltraLabel ultraLabel14;

	private CheckBox chkforceInteger;

	private UltraLabel ultraLabel19;

	private Button btnInstruction;

	private UltraCheckEditor chkBDGT;

	private UltraLabel ultraLabel20;

	private CheckBox chkEnableNewCalculateCost;

	private CheckBox chkEnableFastCalculateAll;

	private UltraCheckEditor cbShowLargeAmountItems;

	private UltraCheckEditor cbShowGreenOptions;

	private string applicationDirectory = AppDomain.CurrentDomain.BaseDirectory;

	private string userID;

	private string backupFolder = string.Empty;

	private string projectCode;

	private string projectType = "bud";

	private string ownerID = string.Empty;

	private string ownerName = string.Empty;

	private Archnowledge.Pcces.DomainModule.LogicalBase.Project project = null;

	private PubProject thePubProject = new PubProject();

	private DataSet dsProject;

	public string _UserID
	{
		get
		{
			return userID;
		}
		set
		{
			userID = value;
		}
	}

	public string _ProjectCode
	{
		get
		{
			return projectCode;
		}
		set
		{
			projectCode = value;
		}
	}

	public string _ActionName
	{
		get
		{
			return projectType;
		}
		set
		{
			projectType = value;
		}
	}

	public string _MainCode
	{
		get
		{
			return ownerID;
		}
		set
		{
			ownerID = value;
		}
	}

	public string _MainName
	{
		get
		{
			return ownerName;
		}
		set
		{
			ownerName = value;
		}
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.Option.FormBDGT_OptionMain));
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
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
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
		this.ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel6 = new System.Windows.Forms.Panel();
		this.panel4 = new System.Windows.Forms.Panel();
		this.chkEnableFastCalculateAll = new System.Windows.Forms.CheckBox();
		this.ultraLabel20 = new Infragistics.Win.Misc.UltraLabel();
		this.chkEnableNewCalculateCost = new System.Windows.Forms.CheckBox();
		this.btnInstruction = new System.Windows.Forms.Button();
		this.ultraLabel19 = new Infragistics.Win.Misc.UltraLabel();
		this.chkforceInteger = new System.Windows.Forms.CheckBox();
		this.ultraLabel14 = new Infragistics.Win.Misc.UltraLabel();
		this.c1PictureBox12 = new C1.Win.C1Input.C1PictureBox();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel8 = new Infragistics.Win.Misc.UltraLabel();
		this.c1PictureBox5 = new C1.Win.C1Input.C1PictureBox();
		this.c1PictureBox6 = new C1.Win.C1Input.C1PictureBox();
		this.rbCalculationMethod = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
		this.ultraLabel13 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraTabPageControl3 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel7 = new System.Windows.Forms.Panel();
		this.panel5 = new System.Windows.Forms.Panel();
		this.cbShowLargeAmountItems = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.cbShowGreenOptions = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.ultraLabel30 = new Infragistics.Win.Misc.UltraLabel();
		this.c1PictureBox9 = new C1.Win.C1Input.C1PictureBox();
		this.ultraLabel9 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel18 = new Infragistics.Win.Misc.UltraLabel();
		this.cbShowToolTipOnNarrowColumn = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.tbSponsor = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.btnPickSponsor = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel11 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraButton3 = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel12 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel15 = new Infragistics.Win.Misc.UltraLabel();
		this.BtnRecover = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel16 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel17 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.c1PictureBox7 = new C1.Win.C1Input.C1PictureBox();
		this.c1PictureBox8 = new C1.Win.C1Input.C1PictureBox();
		this.Tab_B = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel8 = new System.Windows.Forms.Panel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.chkBDGT = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.c1PictureBox11 = new C1.Win.C1Input.C1PictureBox();
		this.chkBDGT_PCals = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.lbCustomizedVariable = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel10 = new Infragistics.Win.Misc.UltraLabel();
		this.c1PictureBox2 = new C1.Win.C1Input.C1PictureBox();
		this.c1PictureBox1 = new C1.Win.C1Input.C1PictureBox();
		this.chkBDGT_AutoSave = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.BDGT_Duration = new System.Windows.Forms.NumericUpDown();
		this.lbBackUpFilePath = new Infragistics.Win.Misc.UltraLabel();
		this.chk_forDeleteNoUsedItem = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel22 = new Infragistics.Win.Misc.UltraLabel();
		this.chk_Number = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chk_AutoNum = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chk_DeleteAutoSave = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.btnChangeBackupPath = new Infragistics.Win.Misc.UltraButton();
		this.ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel10 = new System.Windows.Forms.Panel();
		this.panel3 = new System.Windows.Forms.Panel();
		this.chkIsDetail = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chkAnalyis = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.c1PictureBox10 = new C1.Win.C1Input.C1PictureBox();
		this.chkMrsBItem = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chk_Ana_UseNewOpen = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chkUseNewMrsB = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.c1PictureBox3 = new C1.Win.C1Input.C1PictureBox();
		this.c1PictureBox4 = new C1.Win.C1Input.C1PictureBox();
		this.panel1 = new System.Windows.Forms.Panel();
		this.Tab_Ctrl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.panel9 = new System.Windows.Forms.Panel();
		this.btnOK = new Infragistics.Win.Misc.UltraButton();
		this.btnCancel = new Infragistics.Win.Misc.UltraButton();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.changeBackupPathDialog = new System.Windows.Forms.FolderBrowserDialog();
		this.ultraTabPageControl2.SuspendLayout();
		this.panel4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.c1PictureBox12).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.c1PictureBox5).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.c1PictureBox6).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rbCalculationMethod).BeginInit();
		this.ultraTabPageControl3.SuspendLayout();
		this.panel5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.c1PictureBox9).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbSponsor).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.c1PictureBox7).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.c1PictureBox8).BeginInit();
		this.Tab_B.SuspendLayout();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.c1PictureBox11).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.c1PictureBox2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.c1PictureBox1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.BDGT_Duration).BeginInit();
		this.ultraTabPageControl1.SuspendLayout();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.c1PictureBox10).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.c1PictureBox3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.c1PictureBox4).BeginInit();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).BeginInit();
		this.Tab_Ctrl.SuspendLayout();
		this.panel9.SuspendLayout();
		base.SuspendLayout();
		this.ultraTabPageControl2.Controls.Add(this.panel6);
		this.ultraTabPageControl2.Controls.Add(this.panel4);
		this.ultraTabPageControl2.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabPageControl2.Name = "ultraTabPageControl2";
		this.ultraTabPageControl2.Size = new System.Drawing.Size(630, 520);
		this.panel6.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.panel6.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel6.Location = new System.Drawing.Point(0, 0);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(8, 520);
		this.panel6.TabIndex = 36;
		this.panel4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.panel4.Controls.Add(this.chkEnableFastCalculateAll);
		this.panel4.Controls.Add(this.ultraLabel20);
		this.panel4.Controls.Add(this.chkEnableNewCalculateCost);
		this.panel4.Controls.Add(this.btnInstruction);
		this.panel4.Controls.Add(this.ultraLabel19);
		this.panel4.Controls.Add(this.chkforceInteger);
		this.panel4.Controls.Add(this.ultraLabel14);
		this.panel4.Controls.Add(this.c1PictureBox12);
		this.panel4.Controls.Add(this.ultraLabel7);
		this.panel4.Controls.Add(this.ultraLabel8);
		this.panel4.Controls.Add(this.c1PictureBox5);
		this.panel4.Controls.Add(this.c1PictureBox6);
		this.panel4.Controls.Add(this.rbCalculationMethod);
		this.panel4.Controls.Add(this.ultraLabel13);
		this.panel4.Location = new System.Drawing.Point(16, 8);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(608, 466);
		this.panel4.TabIndex = 35;
		this.chkEnableFastCalculateAll.Location = new System.Drawing.Point(40, 420);
		this.chkEnableFastCalculateAll.Name = "chkEnableFastCalculateAll";
		this.chkEnableFastCalculateAll.Size = new System.Drawing.Size(432, 24);
		this.chkEnableFastCalculateAll.TabIndex = 67;
		this.chkEnableFastCalculateAll.Text = "新版【重新總計】啟動渦輪增壓引擎";
		this.chkEnableFastCalculateAll.Visible = false;
		appearance1.ForeColor = System.Drawing.Color.Red;
		appearance1.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel20.Appearance = appearance1;
		this.ultraLabel20.BackColor = System.Drawing.Color.White;
		this.ultraLabel20.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel20.ForeColor = System.Drawing.Color.Blue;
		this.ultraLabel20.Location = new System.Drawing.Point(40, 363);
		this.ultraLabel20.Name = "ultraLabel20";
		this.ultraLabel20.Size = new System.Drawing.Size(488, 51);
		this.ultraLabel20.TabIndex = 66;
		this.ultraLabel20.Text = "單價分析重新小計並四捨五入取位得工項單價，偶而因取位造成零星工料成為負值，新版計算方式則採全進位，故零星工料將不再是負值。";
		this.chkEnableNewCalculateCost.Location = new System.Drawing.Point(40, 339);
		this.chkEnableNewCalculateCost.Name = "chkEnableNewCalculateCost";
		this.chkEnableNewCalculateCost.Size = new System.Drawing.Size(432, 24);
		this.chkEnableNewCalculateCost.TabIndex = 65;
		this.chkEnableNewCalculateCost.Text = "啟動新版【重新總計】方式";
		this.btnInstruction.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.btnInstruction.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.btnInstruction.Location = new System.Drawing.Point(400, 252);
		this.btnInstruction.Name = "btnInstruction";
		this.btnInstruction.Size = new System.Drawing.Size(64, 26);
		this.btnInstruction.TabIndex = 64;
		this.btnInstruction.Text = "說明...";
		this.btnInstruction.Click += new System.EventHandler(btnInstruction_Click);
		appearance2.ForeColor = System.Drawing.Color.Red;
		appearance2.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel19.Appearance = appearance2;
		this.ultraLabel19.BackColor = System.Drawing.Color.White;
		this.ultraLabel19.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel19.ForeColor = System.Drawing.Color.Blue;
		this.ultraLabel19.Location = new System.Drawing.Point(40, 280);
		this.ultraLabel19.Name = "ultraLabel19";
		this.ultraLabel19.Size = new System.Drawing.Size(488, 48);
		this.ultraLabel19.TabIndex = 63;
		this.ultraLabel19.Text = "若勾選此項目，則單價分析內之分析子項須有雜項可供攤提，若無雜項時此功能自動失效。";
		this.chkforceInteger.Location = new System.Drawing.Point(40, 256);
		this.chkforceInteger.Name = "chkforceInteger";
		this.chkforceInteger.Size = new System.Drawing.Size(432, 24);
		this.chkforceInteger.TabIndex = 62;
		this.chkforceInteger.Text = "單價分析後，合計單價強迫取位成整數";
		this.ultraLabel14.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance3.BackColor = System.Drawing.Color.FromArgb(221, 231, 238);
		appearance3.ForeColor = System.Drawing.Color.Navy;
		this.ultraLabel14.Appearance = appearance3;
		this.ultraLabel14.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.ultraLabel14.Font = new System.Drawing.Font("標楷體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel14.Location = new System.Drawing.Point(20, 220);
		this.ultraLabel14.Name = "ultraLabel14";
		this.ultraLabel14.Size = new System.Drawing.Size(560, 23);
		this.ultraLabel14.TabIndex = 61;
		this.ultraLabel14.Text = "\u3000特殊計算";
		this.c1PictureBox12.BackgroundImage = (System.Drawing.Image)resources.GetObject("c1PictureBox12.BackgroundImage");
		this.c1PictureBox12.Image = (System.Drawing.Image)resources.GetObject("c1PictureBox12.Image");
		this.c1PictureBox12.Location = new System.Drawing.Point(18, 0);
		this.c1PictureBox12.Name = "c1PictureBox12";
		this.c1PictureBox12.Size = new System.Drawing.Size(48, 59);
		this.c1PictureBox12.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
		this.c1PictureBox12.TabIndex = 60;
		this.c1PictureBox12.TabStop = false;
		this.ultraLabel7.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance4.BackColor = System.Drawing.Color.FromArgb(221, 231, 238);
		appearance4.ForeColor = System.Drawing.Color.Navy;
		this.ultraLabel7.Appearance = appearance4;
		this.ultraLabel7.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.ultraLabel7.Font = new System.Drawing.Font("標楷體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel7.Location = new System.Drawing.Point(20, 64);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(560, 23);
		this.ultraLabel7.TabIndex = 24;
		this.ultraLabel7.Text = "\u3000重新總計時，工作要項與單價分析精度不同產生差額處理方式";
		appearance5.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance5.FontData.SizeInPoints = 14f;
		appearance5.ForeColor = System.Drawing.Color.Navy;
		appearance5.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance5.ImageBackground");
		appearance5.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel8.Appearance = appearance5;
		this.ultraLabel8.Location = new System.Drawing.Point(85, 0);
		this.ultraLabel8.Name = "ultraLabel8";
		this.ultraLabel8.Size = new System.Drawing.Size(208, 59);
		this.ultraLabel8.TabIndex = 23;
		this.ultraLabel8.Text = "專案計算方式設定";
		this.c1PictureBox5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.c1PictureBox5.Image = (System.Drawing.Image)resources.GetObject("c1PictureBox5.Image");
		this.c1PictureBox5.Location = new System.Drawing.Point(376, 0);
		this.c1PictureBox5.Name = "c1PictureBox5";
		this.c1PictureBox5.Size = new System.Drawing.Size(227, 59);
		this.c1PictureBox5.TabIndex = 2;
		this.c1PictureBox5.TabStop = false;
		this.c1PictureBox6.Dock = System.Windows.Forms.DockStyle.Top;
		this.c1PictureBox6.Image = (System.Drawing.Image)resources.GetObject("c1PictureBox6.Image");
		this.c1PictureBox6.Location = new System.Drawing.Point(0, 0);
		this.c1PictureBox6.Name = "c1PictureBox6";
		this.c1PictureBox6.Size = new System.Drawing.Size(604, 59);
		this.c1PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.c1PictureBox6.TabIndex = 1;
		this.c1PictureBox6.TabStop = false;
		this.rbCalculationMethod.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.rbCalculationMethod.BackColor = System.Drawing.Color.White;
		this.rbCalculationMethod.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
		this.rbCalculationMethod.CheckedIndex = 0;
		this.rbCalculationMethod.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.rbCalculationMethod.ForeColor = System.Drawing.Color.Blue;
		this.rbCalculationMethod.ItemAppearance = appearance6;
		valueListItem1.DataValue = "TRUE";
		valueListItem1.DisplayText = "單價分析子項一定要有雜項作攤提";
		valueListItem2.DataValue = "FALSE";
		valueListItem2.DisplayText = "單價分析子項有雜項時則作攤提；沒雜項時則不攤提";
		valueListItem3.DataValue = "THIRD";
		valueListItem3.DisplayText = "一律不作攤提";
		this.rbCalculationMethod.Items.Add(valueListItem1);
		this.rbCalculationMethod.Items.Add(valueListItem2);
		this.rbCalculationMethod.Items.Add(valueListItem3);
		this.rbCalculationMethod.ItemSpacingVertical = 5;
		this.rbCalculationMethod.Location = new System.Drawing.Point(40, 96);
		this.rbCalculationMethod.Name = "rbCalculationMethod";
		this.rbCalculationMethod.Size = new System.Drawing.Size(524, 72);
		this.rbCalculationMethod.TabIndex = 0;
		this.rbCalculationMethod.Text = "單價分析子項一定要有雜項作攤提";
		this.rbCalculationMethod.ValueChanged += new System.EventHandler(rbCalculationMethod_ValueChanged);
		appearance7.ForeColor = System.Drawing.Color.Red;
		appearance7.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel13.Appearance = appearance7;
		this.ultraLabel13.BackColor = System.Drawing.Color.White;
		this.ultraLabel13.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel13.ForeColor = System.Drawing.Color.Blue;
		this.ultraLabel13.Location = new System.Drawing.Point(40, 176);
		this.ultraLabel13.Name = "ultraLabel13";
		this.ultraLabel13.Size = new System.Drawing.Size(368, 23);
		this.ultraLabel13.TabIndex = 24;
		this.ultraLabel13.Text = "＊注意：此選項專案不同時，有不同設定";
		this.ultraTabPageControl3.Controls.Add(this.panel7);
		this.ultraTabPageControl3.Controls.Add(this.panel5);
		this.ultraTabPageControl3.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabPageControl3.Name = "ultraTabPageControl3";
		this.ultraTabPageControl3.Size = new System.Drawing.Size(630, 520);
		this.panel7.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.panel7.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel7.Location = new System.Drawing.Point(0, 0);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(8, 520);
		this.panel7.TabIndex = 37;
		this.panel5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.panel5.BackColor = System.Drawing.Color.White;
		this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.panel5.Controls.Add(this.cbShowLargeAmountItems);
		this.panel5.Controls.Add(this.cbShowGreenOptions);
		this.panel5.Controls.Add(this.ultraLabel30);
		this.panel5.Controls.Add(this.c1PictureBox9);
		this.panel5.Controls.Add(this.ultraLabel9);
		this.panel5.Controls.Add(this.ultraLabel18);
		this.panel5.Controls.Add(this.cbShowToolTipOnNarrowColumn);
		this.panel5.Controls.Add(this.tbSponsor);
		this.panel5.Controls.Add(this.btnPickSponsor);
		this.panel5.Controls.Add(this.ultraLabel11);
		this.panel5.Controls.Add(this.ultraButton3);
		this.panel5.Controls.Add(this.ultraLabel12);
		this.panel5.Controls.Add(this.ultraLabel15);
		this.panel5.Controls.Add(this.BtnRecover);
		this.panel5.Controls.Add(this.ultraLabel16);
		this.panel5.Controls.Add(this.ultraLabel17);
		this.panel5.Controls.Add(this.ultraLabel2);
		this.panel5.Controls.Add(this.ultraLabel4);
		this.panel5.Controls.Add(this.c1PictureBox7);
		this.panel5.Controls.Add(this.c1PictureBox8);
		this.panel5.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.panel5.Location = new System.Drawing.Point(16, 8);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(608, 466);
		this.panel5.TabIndex = 36;
		this.cbShowLargeAmountItems.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.cbShowLargeAmountItems.Location = new System.Drawing.Point(315, 404);
		this.cbShowLargeAmountItems.Name = "cbShowLargeAmountItems";
		this.cbShowLargeAmountItems.Size = new System.Drawing.Size(219, 20);
		this.cbShowLargeAmountItems.TabIndex = 62;
		this.cbShowLargeAmountItems.Text = "顯示預算資訊大宗資材項目";
		this.cbShowLargeAmountItems.Visible = false;
		this.cbShowGreenOptions.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.cbShowGreenOptions.Location = new System.Drawing.Point(40, 404);
		this.cbShowGreenOptions.Name = "cbShowGreenOptions";
		this.cbShowGreenOptions.Size = new System.Drawing.Size(219, 20);
		this.cbShowGreenOptions.TabIndex = 61;
		this.cbShowGreenOptions.Text = "顯示預算資訊綠色內涵項目";
		this.ultraLabel30.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance8.FontData.Bold = Infragistics.Win.DefaultableBoolean.False;
		appearance8.FontData.SizeInPoints = 9f;
		appearance8.ForeColor = System.Drawing.Color.Green;
		this.ultraLabel30.Appearance = appearance8;
		this.ultraLabel30.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel30.Location = new System.Drawing.Point(56, 168);
		this.ultraLabel30.Name = "ultraLabel30";
		this.ultraLabel30.Size = new System.Drawing.Size(478, 16);
		this.ultraLabel30.TabIndex = 60;
		this.ultraLabel30.Text = "(新增專案時，預設此機關單位會自動帶入專案基本資訊內之主辦單位資料欄位)";
		this.c1PictureBox9.BackgroundImage = (System.Drawing.Image)resources.GetObject("c1PictureBox9.BackgroundImage");
		this.c1PictureBox9.Image = (System.Drawing.Image)resources.GetObject("c1PictureBox9.Image");
		this.c1PictureBox9.Location = new System.Drawing.Point(18, 0);
		this.c1PictureBox9.Name = "c1PictureBox9";
		this.c1PictureBox9.Size = new System.Drawing.Size(48, 59);
		this.c1PictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
		this.c1PictureBox9.TabIndex = 59;
		this.c1PictureBox9.TabStop = false;
		this.ultraLabel9.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance9.BackColor = System.Drawing.Color.FromArgb(221, 231, 238);
		appearance9.ForeColor = System.Drawing.Color.Navy;
		this.ultraLabel9.Appearance = appearance9;
		this.ultraLabel9.BackColor = System.Drawing.Color.White;
		this.ultraLabel9.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.ultraLabel9.Font = new System.Drawing.Font("標楷體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel9.Location = new System.Drawing.Point(22, 312);
		this.ultraLabel9.Name = "ultraLabel9";
		this.ultraLabel9.Size = new System.Drawing.Size(560, 23);
		this.ultraLabel9.TabIndex = 58;
		this.ultraLabel9.Text = "\u3000回復對話框設定值";
		this.ultraLabel18.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance10.BackColor = System.Drawing.Color.FromArgb(221, 231, 238);
		appearance10.ForeColor = System.Drawing.Color.Navy;
		this.ultraLabel18.Appearance = appearance10;
		this.ultraLabel18.BackColor = System.Drawing.Color.White;
		this.ultraLabel18.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.ultraLabel18.Font = new System.Drawing.Font("標楷體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel18.Location = new System.Drawing.Point(22, 200);
		this.ultraLabel18.Name = "ultraLabel18";
		this.ultraLabel18.Size = new System.Drawing.Size(560, 23);
		this.ultraLabel18.TabIndex = 57;
		this.ultraLabel18.Text = "\u3000清空線上註冊資訊";
		this.cbShowToolTipOnNarrowColumn.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.cbShowToolTipOnNarrowColumn.Location = new System.Drawing.Point(40, 104);
		this.cbShowToolTipOnNarrowColumn.Name = "cbShowToolTipOnNarrowColumn";
		this.cbShowToolTipOnNarrowColumn.Size = new System.Drawing.Size(547, 20);
		this.cbShowToolTipOnNarrowColumn.TabIndex = 56;
		this.cbShowToolTipOnNarrowColumn.Text = "在資料列上，當欄位不夠寬時不自動顯示提示標籤(Tooltip)";
		this.cbShowToolTipOnNarrowColumn.Visible = false;
		this.tbSponsor.AutoSize = true;
		this.tbSponsor.Location = new System.Drawing.Point(144, 139);
		this.tbSponsor.Name = "tbSponsor";
		this.tbSponsor.Size = new System.Drawing.Size(408, 24);
		this.tbSponsor.TabIndex = 55;
		appearance11.FontData.Name = "Arial";
		this.btnPickSponsor.Appearance = appearance11;
		this.btnPickSponsor.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnPickSponsor.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.btnPickSponsor.Location = new System.Drawing.Point(552, 139);
		this.btnPickSponsor.Name = "btnPickSponsor";
		this.btnPickSponsor.ShowFocusRect = false;
		this.btnPickSponsor.ShowOutline = false;
		this.btnPickSponsor.Size = new System.Drawing.Size(24, 24);
		this.btnPickSponsor.SupportThemes = false;
		this.btnPickSponsor.TabIndex = 54;
		this.btnPickSponsor.Text = "...";
		this.btnPickSponsor.Click += new System.EventHandler(btnPickSponsor_Click);
		this.ultraLabel11.Location = new System.Drawing.Point(38, 144);
		this.ultraLabel11.Name = "ultraLabel11";
		this.ultraLabel11.Size = new System.Drawing.Size(114, 23);
		this.ultraLabel11.TabIndex = 44;
		this.ultraLabel11.Text = "預設機關代號：";
		appearance12.FontData.Name = "細明體";
		appearance12.FontData.SizeInPoints = 11f;
		appearance12.Image = resources.GetObject("appearance12.Image");
		appearance12.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton3.Appearance = appearance12;
		this.ultraButton3.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton3.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraButton3.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton3.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton3.Location = new System.Drawing.Point(42, 232);
		this.ultraButton3.Name = "ultraButton3";
		this.ultraButton3.ShowFocusRect = false;
		this.ultraButton3.ShowOutline = false;
		this.ultraButton3.Size = new System.Drawing.Size(120, 27);
		this.ultraButton3.SupportThemes = false;
		this.ultraButton3.TabIndex = 47;
		this.ultraButton3.Text = "立即清空";
		appearance13.ForeColor = System.Drawing.Color.Red;
		this.ultraLabel12.Appearance = appearance13;
		this.ultraLabel12.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel12.Location = new System.Drawing.Point(170, 232);
		this.ultraLabel12.Name = "ultraLabel12";
		this.ultraLabel12.Size = new System.Drawing.Size(56, 23);
		this.ultraLabel12.TabIndex = 49;
		this.ultraLabel12.Text = "說明：";
		this.ultraLabel15.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.ultraLabel15.Location = new System.Drawing.Point(234, 232);
		this.ultraLabel15.Name = "ultraLabel15";
		this.ultraLabel15.Size = new System.Drawing.Size(356, 37);
		this.ultraLabel15.TabIndex = 50;
		this.ultraLabel15.Text = "如果你原本註冊資料不完整，想重新註冊，請執行[立即清空]來幫你清空原本的註冊資訊";
		appearance14.FontData.Name = "細明體";
		appearance14.FontData.SizeInPoints = 11f;
		appearance14.Image = resources.GetObject("appearance14.Image");
		appearance14.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnRecover.Appearance = appearance14;
		this.BtnRecover.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnRecover.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.BtnRecover.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnRecover.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnRecover.Location = new System.Drawing.Point(42, 352);
		this.BtnRecover.Name = "BtnRecover";
		this.BtnRecover.ShowFocusRect = false;
		this.BtnRecover.ShowOutline = false;
		this.BtnRecover.Size = new System.Drawing.Size(120, 27);
		this.BtnRecover.SupportThemes = false;
		this.BtnRecover.TabIndex = 45;
		this.BtnRecover.Text = "立即回復";
		appearance15.ForeColor = System.Drawing.Color.Red;
		this.ultraLabel16.Appearance = appearance15;
		this.ultraLabel16.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel16.Location = new System.Drawing.Point(170, 352);
		this.ultraLabel16.Name = "ultraLabel16";
		this.ultraLabel16.Size = new System.Drawing.Size(56, 23);
		this.ultraLabel16.TabIndex = 48;
		this.ultraLabel16.Text = "說明：";
		this.ultraLabel17.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.ultraLabel17.Location = new System.Drawing.Point(234, 352);
		this.ultraLabel17.Name = "ultraLabel17";
		this.ultraLabel17.Size = new System.Drawing.Size(356, 46);
		this.ultraLabel17.TabIndex = 46;
		this.ultraLabel17.Text = "如果某些對話框所記錄的位置超出你的螢幕解析度範圍，請執行[立即回復]來幫你還原至最初狀態";
		this.ultraLabel2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance16.BackColor = System.Drawing.Color.FromArgb(221, 231, 238);
		appearance16.ForeColor = System.Drawing.Color.Navy;
		this.ultraLabel2.Appearance = appearance16;
		this.ultraLabel2.BackColor = System.Drawing.Color.White;
		this.ultraLabel2.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.ultraLabel2.Font = new System.Drawing.Font("標楷體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel2.Location = new System.Drawing.Point(20, 64);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(560, 23);
		this.ultraLabel2.TabIndex = 24;
		this.ultraLabel2.Text = "\u3000一般參數設定";
		appearance17.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance17.FontData.SizeInPoints = 14f;
		appearance17.ForeColor = System.Drawing.Color.Navy;
		appearance17.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance17.ImageBackground");
		appearance17.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel4.Appearance = appearance17;
		this.ultraLabel4.BackColor = System.Drawing.Color.White;
		this.ultraLabel4.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel4.Location = new System.Drawing.Point(85, 0);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(208, 59);
		this.ultraLabel4.TabIndex = 23;
		this.ultraLabel4.Text = "一般選項設定";
		this.c1PictureBox7.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.c1PictureBox7.BackColor = System.Drawing.Color.White;
		this.c1PictureBox7.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.c1PictureBox7.Image = (System.Drawing.Image)resources.GetObject("c1PictureBox7.Image");
		this.c1PictureBox7.Location = new System.Drawing.Point(376, 0);
		this.c1PictureBox7.Name = "c1PictureBox7";
		this.c1PictureBox7.Size = new System.Drawing.Size(227, 59);
		this.c1PictureBox7.TabIndex = 2;
		this.c1PictureBox7.TabStop = false;
		this.c1PictureBox8.BackColor = System.Drawing.Color.White;
		this.c1PictureBox8.Dock = System.Windows.Forms.DockStyle.Top;
		this.c1PictureBox8.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.c1PictureBox8.Image = (System.Drawing.Image)resources.GetObject("c1PictureBox8.Image");
		this.c1PictureBox8.Location = new System.Drawing.Point(0, 0);
		this.c1PictureBox8.Name = "c1PictureBox8";
		this.c1PictureBox8.Size = new System.Drawing.Size(604, 59);
		this.c1PictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.c1PictureBox8.TabIndex = 1;
		this.c1PictureBox8.TabStop = false;
		this.Tab_B.Controls.Add(this.panel8);
		this.Tab_B.Controls.Add(this.panel2);
		this.Tab_B.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_B.Name = "Tab_B";
		this.Tab_B.Size = new System.Drawing.Size(630, 520);
		this.panel8.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.panel8.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel8.Location = new System.Drawing.Point(0, 0);
		this.panel8.Name = "panel8";
		this.panel8.Size = new System.Drawing.Size(8, 520);
		this.panel8.TabIndex = 37;
		this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.panel2.Controls.Add(this.chkBDGT);
		this.panel2.Controls.Add(this.c1PictureBox11);
		this.panel2.Controls.Add(this.chkBDGT_PCals);
		this.panel2.Controls.Add(this.lbCustomizedVariable);
		this.panel2.Controls.Add(this.ultraLabel3);
		this.panel2.Controls.Add(this.ultraLabel10);
		this.panel2.Controls.Add(this.c1PictureBox2);
		this.panel2.Controls.Add(this.c1PictureBox1);
		this.panel2.Controls.Add(this.chkBDGT_AutoSave);
		this.panel2.Controls.Add(this.BDGT_Duration);
		this.panel2.Controls.Add(this.lbBackUpFilePath);
		this.panel2.Controls.Add(this.chk_forDeleteNoUsedItem);
		this.panel2.Controls.Add(this.ultraLabel1);
		this.panel2.Controls.Add(this.ultraLabel22);
		this.panel2.Controls.Add(this.chk_Number);
		this.panel2.Controls.Add(this.chk_AutoNum);
		this.panel2.Controls.Add(this.chk_DeleteAutoSave);
		this.panel2.Controls.Add(this.btnChangeBackupPath);
		this.panel2.Location = new System.Drawing.Point(16, 8);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(608, 466);
		this.panel2.TabIndex = 34;
		this.chkBDGT.Location = new System.Drawing.Point(40, 253);
		this.chkBDGT.Name = "chkBDGT";
		this.chkBDGT.Size = new System.Drawing.Size(536, 20);
		this.chkBDGT.TabIndex = 62;
		this.chkBDGT.Text = "不再提示「主項大類是由子項加總不可填寫單價」之訊息";
		this.c1PictureBox11.BackgroundImage = (System.Drawing.Image)resources.GetObject("c1PictureBox11.BackgroundImage");
		this.c1PictureBox11.Image = (System.Drawing.Image)resources.GetObject("c1PictureBox11.Image");
		this.c1PictureBox11.Location = new System.Drawing.Point(18, 0);
		this.c1PictureBox11.Name = "c1PictureBox11";
		this.c1PictureBox11.Size = new System.Drawing.Size(48, 59);
		this.c1PictureBox11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
		this.c1PictureBox11.TabIndex = 61;
		this.c1PictureBox11.TabStop = false;
		this.chkBDGT_PCals.BackColor = System.Drawing.Color.White;
		this.chkBDGT_PCals.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.chkBDGT_PCals.Location = new System.Drawing.Point(40, 394);
		this.chkBDGT_PCals.Name = "chkBDGT_PCals";
		this.chkBDGT_PCals.Size = new System.Drawing.Size(216, 20);
		this.chkBDGT_PCals.TabIndex = 35;
		this.chkBDGT_PCals.Text = "啟用自訂變數項功能";
		this.chkBDGT_PCals.Visible = false;
		this.lbCustomizedVariable.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance18.BackColor = System.Drawing.Color.FromArgb(221, 231, 238);
		appearance18.ForeColor = System.Drawing.Color.Navy;
		appearance18.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lbCustomizedVariable.Appearance = appearance18;
		this.lbCustomizedVariable.BackColor = System.Drawing.Color.Transparent;
		this.lbCustomizedVariable.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.lbCustomizedVariable.Font = new System.Drawing.Font("標楷體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lbCustomizedVariable.Location = new System.Drawing.Point(20, 360);
		this.lbCustomizedVariable.Name = "lbCustomizedVariable";
		this.lbCustomizedVariable.Size = new System.Drawing.Size(560, 23);
		this.lbCustomizedVariable.TabIndex = 25;
		this.lbCustomizedVariable.Text = "\u3000自訂變數項 功能";
		this.lbCustomizedVariable.Visible = false;
		this.ultraLabel3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance19.BackColor = System.Drawing.Color.FromArgb(221, 231, 238);
		appearance19.ForeColor = System.Drawing.Color.Navy;
		this.ultraLabel3.Appearance = appearance19;
		this.ultraLabel3.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.ultraLabel3.Font = new System.Drawing.Font("標楷體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel3.Location = new System.Drawing.Point(20, 64);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(560, 23);
		this.ultraLabel3.TabIndex = 24;
		this.ultraLabel3.Text = "\u3000參數設定";
		appearance20.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance20.FontData.SizeInPoints = 14f;
		appearance20.ForeColor = System.Drawing.Color.Navy;
		appearance20.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance20.ImageBackground");
		appearance20.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel10.Appearance = appearance20;
		this.ultraLabel10.Location = new System.Drawing.Point(85, 0);
		this.ultraLabel10.Name = "ultraLabel10";
		this.ultraLabel10.Size = new System.Drawing.Size(208, 59);
		this.ultraLabel10.TabIndex = 23;
		this.ultraLabel10.Text = "預算書編製選項設定";
		this.c1PictureBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.c1PictureBox2.Image = (System.Drawing.Image)resources.GetObject("c1PictureBox2.Image");
		this.c1PictureBox2.Location = new System.Drawing.Point(376, 0);
		this.c1PictureBox2.Name = "c1PictureBox2";
		this.c1PictureBox2.Size = new System.Drawing.Size(227, 59);
		this.c1PictureBox2.TabIndex = 2;
		this.c1PictureBox2.TabStop = false;
		this.c1PictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.c1PictureBox1.Image = (System.Drawing.Image)resources.GetObject("c1PictureBox1.Image");
		this.c1PictureBox1.Location = new System.Drawing.Point(0, 0);
		this.c1PictureBox1.Name = "c1PictureBox1";
		this.c1PictureBox1.Size = new System.Drawing.Size(604, 59);
		this.c1PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.c1PictureBox1.TabIndex = 1;
		this.c1PictureBox1.TabStop = false;
		this.chkBDGT_AutoSave.BackColor = System.Drawing.Color.White;
		this.chkBDGT_AutoSave.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.chkBDGT_AutoSave.Location = new System.Drawing.Point(40, 120);
		this.chkBDGT_AutoSave.Name = "chkBDGT_AutoSave";
		this.chkBDGT_AutoSave.Size = new System.Drawing.Size(168, 20);
		this.chkBDGT_AutoSave.TabIndex = 0;
		this.chkBDGT_AutoSave.Text = "自動備份時間間隔";
		this.BDGT_Duration.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.BDGT_Duration.Location = new System.Drawing.Point(245, 115);
		this.BDGT_Duration.Maximum = new decimal(new int[4] { 120, 0, 0, 0 });
		this.BDGT_Duration.Name = "BDGT_Duration";
		this.BDGT_Duration.Size = new System.Drawing.Size(80, 25);
		this.BDGT_Duration.TabIndex = 1;
		this.BDGT_Duration.Value = new decimal(new int[4] { 10, 0, 0, 0 });
		this.lbBackUpFilePath.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance21.FontData.Bold = Infragistics.Win.DefaultableBoolean.False;
		appearance21.FontData.SizeInPoints = 9f;
		appearance21.ForeColor = System.Drawing.Color.Green;
		this.lbBackUpFilePath.Appearance = appearance21;
		this.lbBackUpFilePath.BackColor = System.Drawing.Color.White;
		this.lbBackUpFilePath.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.lbBackUpFilePath.Location = new System.Drawing.Point(53, 143);
		this.lbBackUpFilePath.Name = "lbBackUpFilePath";
		this.lbBackUpFilePath.Size = new System.Drawing.Size(388, 16);
		this.lbBackUpFilePath.TabIndex = 28;
		this.chk_forDeleteNoUsedItem.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.chk_forDeleteNoUsedItem.BackColor = System.Drawing.Color.White;
		this.chk_forDeleteNoUsedItem.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.chk_forDeleteNoUsedItem.Location = new System.Drawing.Point(40, 299);
		this.chk_forDeleteNoUsedItem.Name = "chk_forDeleteNoUsedItem";
		this.chk_forDeleteNoUsedItem.Size = new System.Drawing.Size(548, 20);
		this.chk_forDeleteNoUsedItem.TabIndex = 29;
		this.chk_forDeleteNoUsedItem.Text = "編製時，刪除工項後自動檢查該工項是否已經沒有被引用";
		this.chk_forDeleteNoUsedItem.Visible = false;
		appearance22.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel1.Appearance = appearance22;
		this.ultraLabel1.BackColor = System.Drawing.Color.White;
		this.ultraLabel1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel1.Location = new System.Drawing.Point(333, 116);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(144, 23);
		this.ultraLabel1.TabIndex = 3;
		this.ultraLabel1.Text = "(不支援Win98/ME)";
		this.ultraLabel22.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance23.FontData.Bold = Infragistics.Win.DefaultableBoolean.False;
		appearance23.FontData.SizeInPoints = 9f;
		appearance23.ForeColor = System.Drawing.Color.Green;
		this.ultraLabel22.Appearance = appearance23;
		this.ultraLabel22.BackColor = System.Drawing.Color.White;
		this.ultraLabel22.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel22.Location = new System.Drawing.Point(56, 323);
		this.ultraLabel22.Name = "ultraLabel22";
		this.ultraLabel22.Size = new System.Drawing.Size(524, 32);
		this.ultraLabel22.TabIndex = 30;
		this.ultraLabel22.Text = "(效能較差，但是當你再從基本工項資料庫引用同一編碼的工項時不會有困擾)建議，不勾選時，請先作一次重新總計再引用基本工項資料庫的工項";
		this.ultraLabel22.Visible = false;
		this.chk_Number.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.chk_Number.BackColor = System.Drawing.Color.White;
		this.chk_Number.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.chk_Number.Location = new System.Drawing.Point(40, 172);
		this.chk_Number.Name = "chk_Number";
		this.chk_Number.Size = new System.Drawing.Size(556, 20);
		this.chk_Number.TabIndex = 34;
		this.chk_Number.Text = "編製時，插入工項後不自動執行項次重整";
		this.chk_AutoNum.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.chk_AutoNum.BackColor = System.Drawing.Color.White;
		this.chk_AutoNum.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.chk_AutoNum.Location = new System.Drawing.Point(40, 212);
		this.chk_AutoNum.Name = "chk_AutoNum";
		this.chk_AutoNum.Size = new System.Drawing.Size(532, 20);
		this.chk_AutoNum.TabIndex = 34;
		this.chk_AutoNum.Text = "重新總計時，自動執行項次重整";
		this.chk_DeleteAutoSave.BackColor = System.Drawing.Color.White;
		this.chk_DeleteAutoSave.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.chk_DeleteAutoSave.Location = new System.Drawing.Point(488, 104);
		this.chk_DeleteAutoSave.Name = "chk_DeleteAutoSave";
		this.chk_DeleteAutoSave.Size = new System.Drawing.Size(111, 20);
		this.chk_DeleteAutoSave.TabIndex = 2;
		this.chk_DeleteAutoSave.Text = "刪除預算書項目之前，自動備份";
		this.chk_DeleteAutoSave.Visible = false;
		appearance24.TextVAlign = Infragistics.Win.VAlign.Top;
		this.btnChangeBackupPath.Appearance = appearance24;
		this.btnChangeBackupPath.BackColor = System.Drawing.Color.White;
		this.btnChangeBackupPath.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.btnChangeBackupPath.Location = new System.Drawing.Point(488, 128);
		this.btnChangeBackupPath.Name = "btnChangeBackupPath";
		this.btnChangeBackupPath.Size = new System.Drawing.Size(120, 23);
		this.btnChangeBackupPath.TabIndex = 32;
		this.btnChangeBackupPath.Text = "變更路徑(&C)...";
		this.btnChangeBackupPath.Visible = false;
		this.btnChangeBackupPath.Click += new System.EventHandler(btnChangeBackupPath_Click);
		this.ultraTabPageControl1.Controls.Add(this.panel10);
		this.ultraTabPageControl1.Controls.Add(this.panel3);
		this.ultraTabPageControl1.Location = new System.Drawing.Point(89, 1);
		this.ultraTabPageControl1.Name = "ultraTabPageControl1";
		this.ultraTabPageControl1.Size = new System.Drawing.Size(630, 520);
		this.panel10.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.panel10.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel10.Location = new System.Drawing.Point(0, 0);
		this.panel10.Name = "panel10";
		this.panel10.Size = new System.Drawing.Size(8, 520);
		this.panel10.TabIndex = 37;
		this.panel3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.panel3.BackColor = System.Drawing.Color.White;
		this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.panel3.Controls.Add(this.chkIsDetail);
		this.panel3.Controls.Add(this.chkAnalyis);
		this.panel3.Controls.Add(this.c1PictureBox10);
		this.panel3.Controls.Add(this.chkMrsBItem);
		this.panel3.Controls.Add(this.chk_Ana_UseNewOpen);
		this.panel3.Controls.Add(this.chkUseNewMrsB);
		this.panel3.Controls.Add(this.ultraLabel5);
		this.panel3.Controls.Add(this.ultraLabel6);
		this.panel3.Controls.Add(this.c1PictureBox3);
		this.panel3.Controls.Add(this.c1PictureBox4);
		this.panel3.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.panel3.Location = new System.Drawing.Point(16, 8);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(608, 466);
		this.panel3.TabIndex = 35;
		this.chkIsDetail.Location = new System.Drawing.Point(40, 200);
		this.chkIsDetail.Name = "chkIsDetail";
		this.chkIsDetail.Size = new System.Drawing.Size(528, 20);
		this.chkIsDetail.TabIndex = 62;
		this.chkIsDetail.Text = "列印單價分析項目，詳細表有相同單價分析則以詳細表出現的順序為主";
		this.chkAnalyis.Location = new System.Drawing.Point(40, 171);
		this.chkAnalyis.Name = "chkAnalyis";
		this.chkAnalyis.Size = new System.Drawing.Size(392, 20);
		this.chkAnalyis.TabIndex = 61;
		this.chkAnalyis.Text = "零星工料通常輸入%，勾選則若輸入單價將不再提醒";
		this.c1PictureBox10.BackgroundImage = (System.Drawing.Image)resources.GetObject("c1PictureBox10.BackgroundImage");
		this.c1PictureBox10.Image = (System.Drawing.Image)resources.GetObject("c1PictureBox10.Image");
		this.c1PictureBox10.Location = new System.Drawing.Point(18, 0);
		this.c1PictureBox10.Name = "c1PictureBox10";
		this.c1PictureBox10.Size = new System.Drawing.Size(48, 59);
		this.c1PictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
		this.c1PictureBox10.TabIndex = 60;
		this.c1PictureBox10.TabStop = false;
		this.chkMrsBItem.Location = new System.Drawing.Point(40, 136);
		this.chkMrsBItem.Name = "chkMrsBItem";
		this.chkMrsBItem.Size = new System.Drawing.Size(312, 20);
		this.chkMrsBItem.TabIndex = 29;
		this.chkMrsBItem.Text = "列印單價分析項目照流水號排序";
		this.chk_Ana_UseNewOpen.Location = new System.Drawing.Point(40, 232);
		this.chk_Ana_UseNewOpen.Name = "chk_Ana_UseNewOpen";
		this.chk_Ana_UseNewOpen.Size = new System.Drawing.Size(312, 20);
		this.chk_Ana_UseNewOpen.TabIndex = 28;
		this.chk_Ana_UseNewOpen.Text = "使用新的開啟單價分析方式";
		this.chk_Ana_UseNewOpen.Visible = false;
		this.chkUseNewMrsB.Location = new System.Drawing.Point(40, 104);
		this.chkUseNewMrsB.Name = "chkUseNewMrsB";
		this.chkUseNewMrsB.Size = new System.Drawing.Size(312, 20);
		this.chkUseNewMrsB.TabIndex = 27;
		this.chkUseNewMrsB.Text = "允許插入重複(相同編碼)的分析子項";
		this.ultraLabel5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance25.BackColor = System.Drawing.Color.FromArgb(221, 231, 238);
		appearance25.ForeColor = System.Drawing.Color.Navy;
		this.ultraLabel5.Appearance = appearance25;
		this.ultraLabel5.BackColor = System.Drawing.Color.White;
		this.ultraLabel5.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
		this.ultraLabel5.Font = new System.Drawing.Font("標楷體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel5.Location = new System.Drawing.Point(20, 64);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(560, 23);
		this.ultraLabel5.TabIndex = 24;
		this.ultraLabel5.Text = "\u3000單價分析參數設定";
		appearance26.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance26.FontData.SizeInPoints = 14f;
		appearance26.ForeColor = System.Drawing.Color.Navy;
		appearance26.ImageBackground = (System.Drawing.Image)resources.GetObject("appearance26.ImageBackground");
		appearance26.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel6.Appearance = appearance26;
		this.ultraLabel6.BackColor = System.Drawing.Color.White;
		this.ultraLabel6.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel6.Location = new System.Drawing.Point(85, 0);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(208, 59);
		this.ultraLabel6.TabIndex = 23;
		this.ultraLabel6.Text = "單價分析選項設定";
		this.c1PictureBox3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.c1PictureBox3.BackColor = System.Drawing.Color.White;
		this.c1PictureBox3.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.c1PictureBox3.Image = (System.Drawing.Image)resources.GetObject("c1PictureBox3.Image");
		this.c1PictureBox3.Location = new System.Drawing.Point(376, 0);
		this.c1PictureBox3.Name = "c1PictureBox3";
		this.c1PictureBox3.Size = new System.Drawing.Size(227, 59);
		this.c1PictureBox3.TabIndex = 2;
		this.c1PictureBox3.TabStop = false;
		this.c1PictureBox4.BackColor = System.Drawing.Color.White;
		this.c1PictureBox4.Dock = System.Windows.Forms.DockStyle.Top;
		this.c1PictureBox4.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.c1PictureBox4.Image = (System.Drawing.Image)resources.GetObject("c1PictureBox4.Image");
		this.c1PictureBox4.Location = new System.Drawing.Point(0, 0);
		this.c1PictureBox4.Name = "c1PictureBox4";
		this.c1PictureBox4.Size = new System.Drawing.Size(604, 59);
		this.c1PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.c1PictureBox4.TabIndex = 1;
		this.c1PictureBox4.TabStop = false;
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.Controls.Add(this.Tab_Ctrl);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(720, 522);
		this.panel1.TabIndex = 0;
		appearance27.BackColor = System.Drawing.Color.FromArgb(153, 204, 255);
		appearance27.BackColor2 = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance27.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
		appearance27.ForeColor = System.Drawing.Color.White;
		this.Tab_Ctrl.ActiveTabAppearance = appearance27;
		appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
		this.Tab_Ctrl.Appearance = appearance28;
		this.Tab_Ctrl.BackColor = System.Drawing.Color.White;
		this.Tab_Ctrl.Controls.Add(this.ultraTabSharedControlsPage1);
		this.Tab_Ctrl.Controls.Add(this.Tab_B);
		this.Tab_Ctrl.Controls.Add(this.ultraTabPageControl1);
		this.Tab_Ctrl.Controls.Add(this.ultraTabPageControl2);
		this.Tab_Ctrl.Controls.Add(this.ultraTabPageControl3);
		this.Tab_Ctrl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Tab_Ctrl.Location = new System.Drawing.Point(0, 0);
		this.Tab_Ctrl.Name = "Tab_Ctrl";
		this.Tab_Ctrl.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.Tab_Ctrl.Size = new System.Drawing.Size(720, 522);
		this.Tab_Ctrl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Flat;
		this.Tab_Ctrl.TabIndex = 29;
		this.Tab_Ctrl.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.LeftTop;
		ultraTab1.TabPage = this.ultraTabPageControl2;
		ultraTab1.Text = "計算方式";
		ultraTab2.TabPage = this.ultraTabPageControl3;
		ultraTab2.Text = "一般";
		ultraTab3.TabPage = this.Tab_B;
		ultraTab3.Text = "預算書編製";
		ultraTab4.TabPage = this.ultraTabPageControl1;
		ultraTab4.Text = "單價分析";
		this.Tab_Ctrl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[4] { ultraTab1, ultraTab2, ultraTab3, ultraTab4 });
		this.Tab_Ctrl.TextOrientation = Infragistics.Win.UltraWinTabs.TextOrientation.Horizontal;
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(630, 520);
		this.panel9.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel9.Controls.Add(this.btnOK);
		this.panel9.Controls.Add(this.btnCancel);
		this.panel9.Controls.Add(this.groupBox5);
		this.panel9.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel9.Location = new System.Drawing.Point(0, 478);
		this.panel9.Name = "panel9";
		this.panel9.Size = new System.Drawing.Size(720, 44);
		this.panel9.TabIndex = 22;
		this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance29.Image = resources.GetObject("appearance29.Image");
		appearance29.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnOK.Appearance = appearance29;
		this.btnOK.BackColor = System.Drawing.SystemColors.Control;
		this.btnOK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.btnOK.Font = new System.Drawing.Font("細明體", 11f);
		this.btnOK.ImageSize = new System.Drawing.Size(20, 20);
		this.btnOK.ImageTransparentColor = System.Drawing.Color.White;
		this.btnOK.Location = new System.Drawing.Point(532, 8);
		this.btnOK.Name = "btnOK";
		this.btnOK.ShowFocusRect = false;
		this.btnOK.ShowOutline = false;
		this.btnOK.Size = new System.Drawing.Size(88, 31);
		this.btnOK.SupportThemes = false;
		this.btnOK.TabIndex = 8;
		this.btnOK.Text = "確定";
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance30.Image = resources.GetObject("appearance30.Image");
		appearance30.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnCancel.Appearance = appearance30;
		this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
		this.btnCancel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.Font = new System.Drawing.Font("細明體", 11f);
		this.btnCancel.ImageSize = new System.Drawing.Size(20, 20);
		this.btnCancel.ImageTransparentColor = System.Drawing.Color.White;
		this.btnCancel.Location = new System.Drawing.Point(624, 8);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.ShowFocusRect = false;
		this.btnCancel.ShowOutline = false;
		this.btnCancel.Size = new System.Drawing.Size(88, 31);
		this.btnCancel.SupportThemes = false;
		this.btnCancel.TabIndex = 7;
		this.btnCancel.Text = "取消";
		this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox5.Location = new System.Drawing.Point(0, 0);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(720, 4);
		this.groupBox5.TabIndex = 3;
		this.groupBox5.TabStop = false;
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		base.ClientSize = new System.Drawing.Size(720, 522);
		base.Controls.Add(this.panel9);
		base.Controls.Add(this.panel1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.MinimizeBox = false;
		base.Name = "FormBDGT_OptionMain";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "選項";
		base.Load += new System.EventHandler(FormBDGT_OptionMain_Load);
		this.ultraTabPageControl2.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.c1PictureBox12).EndInit();
		((System.ComponentModel.ISupportInitialize)this.c1PictureBox5).EndInit();
		((System.ComponentModel.ISupportInitialize)this.c1PictureBox6).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rbCalculationMethod).EndInit();
		this.ultraTabPageControl3.ResumeLayout(false);
		this.panel5.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.c1PictureBox9).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbSponsor).EndInit();
		((System.ComponentModel.ISupportInitialize)this.c1PictureBox7).EndInit();
		((System.ComponentModel.ISupportInitialize)this.c1PictureBox8).EndInit();
		this.Tab_B.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.c1PictureBox11).EndInit();
		((System.ComponentModel.ISupportInitialize)this.c1PictureBox2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.c1PictureBox1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.BDGT_Duration).EndInit();
		this.ultraTabPageControl1.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.c1PictureBox10).EndInit();
		((System.ComponentModel.ISupportInitialize)this.c1PictureBox3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.c1PictureBox4).EndInit();
		this.panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).EndInit();
		this.Tab_Ctrl.ResumeLayout(false);
		this.panel9.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	public FormBDGT_OptionMain()
	{
		InitializeComponent();
	}

	private void FormBDGT_OptionMain_Load(object sender, EventArgs e)
	{
		if (projectType.ToUpper() == "BUD")
		{
			project = new BudProject();
			btnOK.Enabled = true;
		}
		if (projectType.ToUpper() == "SUB")
		{
			project = new SubProject();
			chkBDGT_AutoSave.Visible = false;
			BDGT_Duration.Visible = false;
			lbBackUpFilePath.Visible = false;
			lbCustomizedVariable.Visible = false;
			chkBDGT_PCals.Visible = false;
			ultraLabel1.Visible = false;
		}
		else if (projectType.ToUpper() == "BID")
		{
			project = new BidProject();
			Tab_Ctrl.Tabs[1].Visible = false;
			Tab_Ctrl.Tabs[2].Visible = false;
			Tab_Ctrl.Tabs[3].Visible = false;
		}
		dsProject = project.GetProject(projectCode);
		LoadBudgetSettings();
		LoadCommonSettings();
		LoadCostBreakDownSettings();
	}

	private void LoadBudgetSettings()
	{
		string IschkBDGT = CommonMethods.IniReadValue(applicationDirectory + "OptionSet.ini", "BDGT", "NoMessage");
		if (IschkBDGT.ToUpper() == "TRUE")
		{
			chkBDGT.Checked = true;
		}
		else
		{
			chkBDGT.Checked = false;
		}
		string sIsAutoSave = CommonMethods.IniReadValue(applicationDirectory + "OptionSet.ini", "BDGT", "IsAutoSave");
		if (sIsAutoSave.ToUpper() == "TRUE")
		{
			chkBDGT_AutoSave.Checked = true;
		}
		else
		{
			chkBDGT_AutoSave.Checked = false;
		}
		string autoSaveDuration = CommonMethods.IniReadValue(applicationDirectory + "OptionSet.ini", "BDGT", "AutoSaveDuration");
		BDGT_Duration.Value = PubTools.Str2Decimal(autoSaveDuration);
		string deleteAutoSave = CommonMethods.IniReadValue(applicationDirectory + "OptionSet.ini", "BDGT", "IsDeleteAutoSave");
		string isEidtNumber = CommonMethods.IniReadValue(applicationDirectory + "OptionSet.ini", "BDGT", "IsEidtNumber");
		if (isEidtNumber.ToUpper() == "TRUE")
		{
			chk_Number.Checked = true;
		}
		else
		{
			chk_Number.Checked = false;
		}
		int CalculationMethod = PubTools.Str2Int(dsProject.Tables[0].Rows[0]["ReCalType"]);
		rbCalculationMethod.CheckedIndex = CalculationMethod - 1;
		string backupPath = CommonMethods.IniReadValue(applicationDirectory + "OptionSet.ini", "BDGT", "BackupPath");
		if (backupPath == string.Empty)
		{
			backupFolder = applicationDirectory + "Backup\\";
		}
		else
		{
			backupFolder = backupPath;
		}
		lbBackUpFilePath.Text = "(備份的存放路徑是 " + backupFolder + ")";
		string isAutoNumber = CommonMethods.IniReadValue(applicationDirectory + "OptionSet.ini", "BDGT", "IsAutoNumber");
		if (isAutoNumber.ToUpper() == "TRUE")
		{
			chk_AutoNum.Checked = true;
		}
		else
		{
			chk_AutoNum.Checked = false;
		}
		DataRow drProject = dsProject.Tables[0].Rows[0];
		if (drProject["roundAnalysisItemPrice"] != DBNull.Value && drProject["roundAnalysisItemPrice"].ToString() == "1")
		{
			chkforceInteger.Checked = true;
		}
		else
		{
			chkforceInteger.Checked = false;
		}
		if (projectType.ToUpper() == "BUD")
		{
			chkBDGT_PCals.Checked = true;
		}
		chkEnableNewCalculateCost.Checked = thePubProject.GetPubProjectEnableNewCalculateCost(projectCode);
		SysConfigDBHelper theSysConfigHelper = new SysConfigDBHelper();
		string EnableFastCalculateAll = theSysConfigHelper.GetValueByName("EnableFastCalculateAll");
		chkEnableFastCalculateAll.Checked = ArchConvert.Obj2Bool(EnableFastCalculateAll);
	}

	private void LoadCommonSettings()
	{
		string showToolTipOnNarrowColumn = CommonMethods.IniReadValue(applicationDirectory + "OptionSet.ini", "CommonData", "AllowIsTooltip");
		if (showToolTipOnNarrowColumn.ToUpper() == "TRUE")
		{
			cbShowToolTipOnNarrowColumn.Checked = true;
		}
		else
		{
			cbShowToolTipOnNarrowColumn.Checked = false;
		}
		if (CommonMethods.IniReadValue(applicationDirectory + "OptionSet.ini", "CommonData", "ShowGreenOptions").ToUpper() == "TRUE")
		{
			cbShowGreenOptions.Checked = true;
		}
		else
		{
			cbShowGreenOptions.Checked = false;
		}
		ownerID = CommonMethods.IniReadValue(applicationDirectory + "OptionSet.ini", "CommonData", "DEPT_ID");
		MainUnit mainUnit = new MainUnit();
		DataSet dsMainUnit = mainUnit.GetMainUnit(ownerID);
		if (dsMainUnit.Tables[0].Rows.Count > 0)
		{
			DataRow Sponsor = dsMainUnit.Tables[0].Rows[0];
			tbSponsor.Text = Sponsor["MainCode"].ToString().Trim() + "：" + Sponsor["MainName"].ToString().Trim();
		}
	}

	private void LoadCostBreakDownSettings()
	{
		string allowRepeatItem = CommonMethods.IniReadValue(applicationDirectory + "OptionSet.ini", "BreakDownData", "AllowRepeatItem");
		string allowSortItem = CommonMethods.IniReadValue(applicationDirectory + "OptionSet.ini", "BreakDownData", "AllowSort");
		string isChkAnalysis = CommonMethods.IniReadValue(applicationDirectory + "OptionSet.ini", "BreakDownData", "NoMessage");
		string detailMaster = CommonMethods.IniReadValue(applicationDirectory + "OptionSet.ini", "BreakDownData", "DetailMaster");
		if (allowRepeatItem.ToUpper() == "TRUE")
		{
			chkUseNewMrsB.Checked = true;
		}
		else
		{
			chkUseNewMrsB.Checked = false;
		}
		if (allowSortItem.ToUpper() == "TRUE")
		{
			chkMrsBItem.Checked = true;
		}
		else
		{
			chkMrsBItem.Checked = false;
		}
		if (isChkAnalysis.ToUpper() == "TRUE")
		{
			chkAnalyis.Checked = true;
		}
		else
		{
			chkAnalyis.Checked = false;
		}
		if (detailMaster.ToUpper() == "TRUE")
		{
			chkIsDetail.Checked = true;
		}
		else
		{
			chkIsDetail.Checked = false;
		}
	}

	private void SaveBudgetSettings()
	{
		if (chkBDGT.Checked)
		{
			CommonMethods.IniWriteValue(applicationDirectory + "OptionSet.ini", "BDGT", "NoMessage", "TRUE");
		}
		else
		{
			CommonMethods.IniWriteValue(applicationDirectory + "OptionSet.ini", "BDGT", "NoMessage", "FALSE");
		}
		if (chkBDGT_AutoSave.Checked)
		{
			CommonMethods.IniWriteValue(applicationDirectory + "OptionSet.ini", "BDGT", "IsAutoSave", "TRUE");
		}
		else
		{
			CommonMethods.IniWriteValue(applicationDirectory + "OptionSet.ini", "BDGT", "IsAutoSave", "FALSE");
		}
		CommonMethods.IniWriteValue(applicationDirectory + "OptionSet.ini", "BDGT", "AutoSaveDuration", BDGT_Duration.Value.ToString());
		if (chk_DeleteAutoSave.Checked)
		{
			CommonMethods.IniWriteValue(applicationDirectory + "OptionSet.ini", "BDGT", "IsDeleteAutoSave", "TRUE");
		}
		else
		{
			CommonMethods.IniWriteValue(applicationDirectory + "OptionSet.ini", "BDGT", "IsDeleteAutoSave", "FALSE");
		}
		switch (rbCalculationMethod.CheckedIndex)
		{
		case 0:
			CommonMethods.IniWriteValue(applicationDirectory + "OptionSet.ini", "BDGT", "IsOldReCal", "FALSE");
			dsProject.Tables[0].Rows[0]["ReCalType"] = "1";
			break;
		case 1:
			CommonMethods.IniWriteValue(applicationDirectory + "OptionSet.ini", "BDGT", "IsOldReCal", "TRUE");
			dsProject.Tables[0].Rows[0]["ReCalType"] = "2";
			break;
		case 2:
			CommonMethods.IniWriteValue(applicationDirectory + "OptionSet.ini", "BDGT", "IsOldReCal", "THIRD");
			dsProject.Tables[0].Rows[0]["ReCalType"] = "3";
			break;
		}
		if (chk_Number.Checked)
		{
			CommonMethods.IniWriteValue(applicationDirectory + "OptionSet.ini", "BDGT", "IsEidtNumber", "TRUE");
		}
		else
		{
			CommonMethods.IniWriteValue(applicationDirectory + "OptionSet.ini", "BDGT", "IsEidtNumber", "FALSE");
		}
		CommonMethods.IniWriteValue(applicationDirectory + "OptionSet.ini", "BDGT", "BackupPath", backupFolder);
		if (chk_AutoNum.Checked)
		{
			CommonMethods.IniWriteValue(applicationDirectory + "OptionSet.ini", "BDGT", "IsAutoNumber", "TRUE");
		}
		else
		{
			CommonMethods.IniWriteValue(applicationDirectory + "OptionSet.ini", "BDGT", "IsAutoNumber", "FALSE");
		}
		DataRow drProject = dsProject.Tables[0].Rows[0];
		if (chkforceInteger.Checked)
		{
			drProject["roundAnalysisItemPrice"] = "1";
		}
		else
		{
			drProject["roundAnalysisItemPrice"] = "0";
		}
		if (projectType.ToUpper() == "BUD")
		{
			drProject["enableCustomizedVariable"] = "1";
		}
	}

	private void SaveCommonSettings()
	{
		CommonMethods.IniWriteValue(applicationDirectory + "OptionSet.ini", "CommonData", "DEPT_ID", ownerID);
		if (cbShowToolTipOnNarrowColumn.Checked)
		{
			CommonMethods.IniWriteValue(applicationDirectory + "OptionSet.ini", "CommonData", "AllowIsTooltip", "TRUE");
		}
		else
		{
			CommonMethods.IniWriteValue(applicationDirectory + "OptionSet.ini", "CommonData", "AllowIsTooltip", "FALSE");
		}
		if (cbShowGreenOptions.Checked)
		{
			CommonMethods.IniWriteValue(applicationDirectory + "OptionSet.ini", "CommonData", "ShowGreenOptions", "TRUE");
		}
		else
		{
			CommonMethods.IniWriteValue(applicationDirectory + "OptionSet.ini", "CommonData", "ShowGreenOptions", "FALSE");
		}
	}

	private void SaveCostBreakDownSettings()
	{
		bool IsRepeat = chkUseNewMrsB.Checked;
		bool IschkMrsBItem = chkMrsBItem.Checked;
		bool IschkAnalyis = chkAnalyis.Checked;
		bool IsDetailMaster = chkIsDetail.Checked;
		if (IsRepeat)
		{
			CommonMethods.IniWriteValue(applicationDirectory + "OptionSet.ini", "BreakDownData", "AllowRepeatItem", "TRUE");
		}
		else
		{
			CommonMethods.IniWriteValue(applicationDirectory + "OptionSet.ini", "BreakDownData", "AllowRepeatItem", "FALSE");
		}
		if (chk_Ana_UseNewOpen.Checked)
		{
			CommonMethods.IniWriteValue(applicationDirectory + "OptionSet.ini", "BreakDownData", "UseNewOpen", "TRUE");
		}
		else
		{
			CommonMethods.IniWriteValue(applicationDirectory + "OptionSet.ini", "BreakDownData", "UseNewOpen", "FALSE");
		}
		if (IschkMrsBItem)
		{
			CommonMethods.IniWriteValue(applicationDirectory + "OptionSet.ini", "BreakDownData", "AllowSort", "TRUE");
		}
		else
		{
			CommonMethods.IniWriteValue(applicationDirectory + "OptionSet.ini", "BreakDownData", "AllowSort", "FALSE");
		}
		if (IschkAnalyis)
		{
			CommonMethods.IniWriteValue(applicationDirectory + "OptionSet.ini", "BreakDownData", "NoMessage", "TRUE");
		}
		else
		{
			CommonMethods.IniWriteValue(applicationDirectory + "OptionSet.ini", "BreakDownData", "NoMessage", "FALSE");
		}
		if (IsDetailMaster)
		{
			CommonMethods.IniWriteValue(applicationDirectory + "OptionSet.ini", "BreakDownData", "DetailMaster", "TRUE");
		}
		else
		{
			CommonMethods.IniWriteValue(applicationDirectory + "OptionSet.ini", "BreakDownData", "DetailMaster", "FALSE");
		}
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		SaveBudgetSettings();
		SaveCommonSettings();
		SaveCostBreakDownSettings();
		project.GetDatasetUpdate(dsProject);
		thePubProject.UpdatePubProjectEnableNewCalculateCost(projectCode, chkEnableNewCalculateCost.Checked);
		SysConfigDBHelper theSysConfigHelper = new SysConfigDBHelper();
		theSysConfigHelper.SetValueByName("EnableFastCalculateAll", chkEnableFastCalculateAll.Checked.ToString());
		SysConfig.ReInitComplete();
	}

	private void btnChangeBackupPath_Click(object sender, EventArgs e)
	{
		if (changeBackupPathDialog.ShowDialog() == DialogResult.OK)
		{
			if (!changeBackupPathDialog.SelectedPath.EndsWith("\\"))
			{
				backupFolder = changeBackupPathDialog.SelectedPath + "\\";
			}
			else
			{
				backupFolder = changeBackupPathDialog.SelectedPath;
			}
			lbBackUpFilePath.Text = "(備份的存放路徑是 " + backupFolder + ")";
		}
	}

	private void btnPickSponsor_Click(object sender, EventArgs e)
	{
		FormBudgetDept_Pick FM_BDGT_DEPT_PK = new FormBudgetDept_Pick();
		FM_BDGT_DEPT_PK._UserID = userID;
		FM_BDGT_DEPT_PK._OwnerName = "OptionMain";
		if (FM_BDGT_DEPT_PK.ShowDialog(this) == DialogResult.OK)
		{
			tbSponsor.Text = ownerID + ":" + ownerName;
		}
		FM_BDGT_DEPT_PK.Close();
		FM_BDGT_DEPT_PK.Dispose();
		FM_BDGT_DEPT_PK = null;
	}

	private void btnInstruction_Click(object sender, EventArgs e)
	{
		FormBDGT_OptionMain_Help1 FM_OP = new FormBDGT_OptionMain_Help1();
		FM_OP.Owner = this;
		FM_OP.ShowDialog();
		FM_OP.Close();
		FM_OP.Dispose();
		FM_OP = null;
	}

	private void rbCalculationMethod_ValueChanged(object sender, EventArgs e)
	{
		chkforceInteger.Enabled = rbCalculationMethod.CheckedIndex != 2;
	}
}
