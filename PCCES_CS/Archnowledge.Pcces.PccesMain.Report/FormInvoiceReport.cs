using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.REPClass;
using Archnowledge.Pcces.STDClass;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTabControl;

namespace Archnowledge.Pcces.PccesMain.Report;

public class FormInvoiceReport : Form
{
	private const string CallFormHelp = "FormInvoiceReport";

	private UltraTabControl Tab_Ctrl;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl Tab_A;

	private UltraTabPageControl Tab_B;

	private UltraTabPageControl Tab_C;

	private Panel panel1;

	private UltraButton A_Btn_Cncl;

	private UltraLabel ultraLabel10;

	private Panel panel7;

	private GroupBox groupBox4;

	private UltraButton ultraButton1;

	private UltraButton ultraButton2;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	private GroupBox groupBox3;

	private UltraComboEditor SetEnd;

	private UltraLabel ultraLabel6;

	private UltraLabel ultraLabel5;

	private UltraTextEditor cmp_Ename;

	private UltraLabel ultraLabel4;

	private UltraTextEditor cmp_name;

	private Panel panel2;

	private UltraOptionSet ReportList;

	private Panel PNL_RPT;

	private GroupBox groupBox5;

	private UltraLabel ultraLabel1;

	private UltraButton ultraButton3;

	private UltraButton A_Btn_OK;

	private Panel PNL_CRP;

	private UltraLabel lbl_NoRPT;

	private Container components = null;

	private ucSubCtr RPT1 = new ucSubCtr();

	private ucSubAcc RPT2 = new ucSubAcc();

	private ucSubChg RPT3 = new ucSubChg();

	private ucSubClose RPT4 = new ucSubClose();

	private ucSubFinal RPT5 = new ucSubFinal();

	private ucCrystalViewer CRVer;

	private PccesFormAction F_ActionName = PccesFormAction.SplitContract;

	private string F_ProjectCode;

	private string F_SubProjectCode;

	private string F_Issue;

	private Button button1;

	private GroupBox groupBox6;

	private UltraButton BtnChgDir;

	private UltraTextEditor txtPath;

	private UltraLabel ultraLabel8;

	private UltraTextEditor txtFileName;

	private UltraLabel ultraLabel2;

	private FolderBrowserDialog folderBrowserDialog1;

	private string F_UserID;

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

	public FormInvoiceReport()
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
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.Report.FormInvoiceReport));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
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
		Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem4 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem5 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem6 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem7 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		this.Tab_A = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel1 = new System.Windows.Forms.Panel();
		this.button1 = new System.Windows.Forms.Button();
		this.A_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.A_Btn_OK = new Infragistics.Win.Misc.UltraButton();
		this.groupBox6 = new System.Windows.Forms.GroupBox();
		this.txtPath = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel8 = new Infragistics.Win.Misc.UltraLabel();
		this.txtFileName = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.BtnChgDir = new Infragistics.Win.Misc.UltraButton();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.SetEnd = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.cmp_Ename = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.cmp_name = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.ultraButton3 = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.panel2 = new System.Windows.Forms.Panel();
		this.ReportList = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
		this.lbl_NoRPT = new Infragistics.Win.Misc.UltraLabel();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.PNL_RPT = new System.Windows.Forms.Panel();
		this.Tab_B = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.ultraLabel10 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_C = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.PNL_CRP = new System.Windows.Forms.Panel();
		this.panel7 = new System.Windows.Forms.Panel();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton2 = new Infragistics.Win.Misc.UltraButton();
		this.Tab_Ctrl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
		this.Tab_A.SuspendLayout();
		this.panel1.SuspendLayout();
		this.groupBox6.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtPath).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtFileName).BeginInit();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.SetEnd).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cmp_Ename).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cmp_name).BeginInit();
		this.groupBox2.SuspendLayout();
		this.groupBox5.SuspendLayout();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ReportList).BeginInit();
		this.groupBox1.SuspendLayout();
		this.Tab_B.SuspendLayout();
		this.Tab_C.SuspendLayout();
		this.panel7.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).BeginInit();
		this.Tab_Ctrl.SuspendLayout();
		base.SuspendLayout();
		this.Tab_A.Controls.Add(this.panel1);
		this.Tab_A.Controls.Add(this.groupBox6);
		this.Tab_A.Controls.Add(this.groupBox3);
		this.Tab_A.Controls.Add(this.groupBox2);
		this.Tab_A.Controls.Add(this.groupBox1);
		this.Tab_A.Location = new System.Drawing.Point(0, 0);
		this.Tab_A.Name = "Tab_A";
		this.Tab_A.Size = new System.Drawing.Size(616, 603);
		this.panel1.Controls.Add(this.button1);
		this.panel1.Controls.Add(this.A_Btn_Cncl);
		this.panel1.Controls.Add(this.A_Btn_OK);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 561);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(616, 42);
		this.panel1.TabIndex = 10;
		this.button1.BackColor = System.Drawing.SystemColors.Control;
		this.button1.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.button1.Location = new System.Drawing.Point(8, 7);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(128, 30);
		this.button1.TabIndex = 3;
		this.button1.Text = "匯出ACCES資料檔 ▼";
		this.button1.Click += new System.EventHandler(button1_Click);
		this.A_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Cncl.Appearance = appearance1;
		this.A_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.A_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_Cncl.Location = new System.Drawing.Point(513, 7);
		this.A_Btn_Cncl.Name = "A_Btn_Cncl";
		this.A_Btn_Cncl.ShowFocusRect = false;
		this.A_Btn_Cncl.ShowOutline = false;
		this.A_Btn_Cncl.Size = new System.Drawing.Size(96, 31);
		this.A_Btn_Cncl.SupportThemes = false;
		this.A_Btn_Cncl.TabIndex = 2;
		this.A_Btn_Cncl.Text = "取消";
		this.A_Btn_Cncl.Click += new System.EventHandler(A_Btn_Cncl_Click);
		this.A_Btn_OK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_OK.Appearance = appearance2;
		this.A_Btn_OK.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_OK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_OK.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_OK.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_OK.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_OK.Location = new System.Drawing.Point(413, 7);
		this.A_Btn_OK.Name = "A_Btn_OK";
		this.A_Btn_OK.ShowFocusRect = false;
		this.A_Btn_OK.ShowOutline = false;
		this.A_Btn_OK.Size = new System.Drawing.Size(96, 31);
		this.A_Btn_OK.SupportThemes = false;
		this.A_Btn_OK.TabIndex = 1;
		this.A_Btn_OK.Text = "預覽報表";
		this.A_Btn_OK.Click += new System.EventHandler(A_Btn_Next_Click);
		this.groupBox6.Controls.Add(this.txtPath);
		this.groupBox6.Controls.Add(this.ultraLabel8);
		this.groupBox6.Controls.Add(this.txtFileName);
		this.groupBox6.Controls.Add(this.ultraLabel2);
		this.groupBox6.Controls.Add(this.BtnChgDir);
		this.groupBox6.Location = new System.Drawing.Point(7, 464);
		this.groupBox6.Name = "groupBox6";
		this.groupBox6.Size = new System.Drawing.Size(604, 90);
		this.groupBox6.TabIndex = 14;
		this.groupBox6.TabStop = false;
		this.groupBox6.Text = "Access匯出路徑";
		this.txtPath.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance3.FontData.Name = "細明體";
		appearance3.FontData.SizeInPoints = 11f;
		this.txtPath.Appearance = appearance3;
		this.txtPath.Location = new System.Drawing.Point(88, 90);
		this.txtPath.Name = "txtPath";
		this.txtPath.Size = new System.Drawing.Size(449, 24);
		this.txtPath.TabIndex = 19;
		this.ultraLabel8.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance4.TextHAlign = Infragistics.Win.HAlign.Right;
		this.ultraLabel8.Appearance = appearance4;
		this.ultraLabel8.AutoSize = true;
		this.ultraLabel8.Location = new System.Drawing.Point(8, 92);
		this.ultraLabel8.Name = "ultraLabel8";
		this.ultraLabel8.Size = new System.Drawing.Size(57, 16);
		this.ultraLabel8.TabIndex = 18;
		this.ultraLabel8.Text = "存放路徑:";
		this.txtFileName.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance5.FontData.Name = "細明體";
		appearance5.FontData.SizeInPoints = 11f;
		this.txtFileName.Appearance = appearance5;
		this.txtFileName.Location = new System.Drawing.Point(88, 60);
		this.txtFileName.Name = "txtFileName";
		this.txtFileName.Size = new System.Drawing.Size(449, 24);
		this.txtFileName.TabIndex = 17;
		this.ultraLabel2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance6.TextHAlign = Infragistics.Win.HAlign.Right;
		this.ultraLabel2.Appearance = appearance6;
		this.ultraLabel2.AutoSize = true;
		this.ultraLabel2.Location = new System.Drawing.Point(8, 63);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(57, 16);
		this.ultraLabel2.TabIndex = 16;
		this.ultraLabel2.Text = "檔案名稱:";
		this.BtnChgDir.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance7.FontData.Name = "Arial";
		appearance7.FontData.SizeInPoints = 8f;
		this.BtnChgDir.Appearance = appearance7;
		this.BtnChgDir.BackColor = System.Drawing.SystemColors.Control;
		this.BtnChgDir.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.BtnChgDir.Location = new System.Drawing.Point(535, 90);
		this.BtnChgDir.Name = "BtnChgDir";
		this.BtnChgDir.ShowFocusRect = false;
		this.BtnChgDir.ShowOutline = false;
		this.BtnChgDir.Size = new System.Drawing.Size(48, 24);
		this.BtnChgDir.SupportThemes = false;
		this.BtnChgDir.TabIndex = 12;
		this.BtnChgDir.Text = "瀏覽...";
		this.BtnChgDir.Click += new System.EventHandler(BtnChgDir_Click);
		this.groupBox3.Controls.Add(this.SetEnd);
		this.groupBox3.Controls.Add(this.ultraLabel6);
		this.groupBox3.Controls.Add(this.cmp_Ename);
		this.groupBox3.Controls.Add(this.cmp_name);
		this.groupBox3.Controls.Add(this.ultraLabel4);
		this.groupBox3.Controls.Add(this.ultraLabel5);
		this.groupBox3.Location = new System.Drawing.Point(7, 333);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(604, 125);
		this.groupBox3.TabIndex = 13;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "表頭及表尾";
		appearance8.FontData.Name = "細明體";
		appearance8.FontData.SizeInPoints = 11f;
		this.SetEnd.Appearance = appearance8;
		this.SetEnd.Location = new System.Drawing.Point(160, 88);
		this.SetEnd.Name = "SetEnd";
		this.SetEnd.Size = new System.Drawing.Size(432, 24);
		this.SetEnd.TabIndex = 22;
		this.SetEnd.Text = "[SetEnd]";
		this.ultraLabel6.Location = new System.Drawing.Point(8, 90);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(48, 23);
		this.ultraLabel6.TabIndex = 5;
		this.ultraLabel6.Text = "表尾:";
		appearance9.FontData.Name = "細明體";
		appearance9.FontData.SizeInPoints = 11f;
		this.cmp_Ename.Appearance = appearance9;
		this.cmp_Ename.Location = new System.Drawing.Point(160, 55);
		this.cmp_Ename.Name = "cmp_Ename";
		this.cmp_Ename.Size = new System.Drawing.Size(432, 24);
		this.cmp_Ename.TabIndex = 2;
		this.cmp_Ename.Text = "[cmp_Ename]";
		appearance10.FontData.Name = "細明體";
		appearance10.FontData.SizeInPoints = 11f;
		this.cmp_name.Appearance = appearance10;
		this.cmp_name.Location = new System.Drawing.Point(160, 23);
		this.cmp_name.Name = "cmp_name";
		this.cmp_name.Size = new System.Drawing.Size(432, 24);
		this.cmp_name.TabIndex = 0;
		this.cmp_name.Text = "[cmp_name]";
		this.ultraLabel4.Location = new System.Drawing.Point(8, 27);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(208, 16);
		this.ultraLabel4.TabIndex = 1;
		this.ultraLabel4.Text = "機關/公司名稱:";
		this.ultraLabel5.Location = new System.Drawing.Point(8, 58);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(224, 14);
		this.ultraLabel5.TabIndex = 3;
		this.ultraLabel5.Text = "機關/公司英文名稱:";
		this.groupBox2.Controls.Add(this.ultraButton3);
		this.groupBox2.Controls.Add(this.ultraLabel1);
		this.groupBox2.Controls.Add(this.groupBox5);
		this.groupBox2.Location = new System.Drawing.Point(6, 152);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(604, 176);
		this.groupBox2.TabIndex = 12;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "報表格式清單";
		appearance11.FontData.SizeInPoints = 9f;
		this.ultraButton3.Appearance = appearance11;
		this.ultraButton3.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton3.Location = new System.Drawing.Point(520, 144);
		this.ultraButton3.Name = "ultraButton3";
		this.ultraButton3.ShowFocusRect = false;
		this.ultraButton3.ShowOutline = false;
		this.ultraButton3.Size = new System.Drawing.Size(75, 24);
		this.ultraButton3.TabIndex = 3;
		this.ultraButton3.Text = "線上更新";
		this.ultraButton3.Click += new System.EventHandler(ultraButton3_Click);
		appearance12.ForeColor = System.Drawing.Color.OrangeRed;
		this.ultraLabel1.Appearance = appearance12;
		this.ultraLabel1.Location = new System.Drawing.Point(8, 130);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(352, 36);
		this.ultraLabel1.TabIndex = 2;
		this.ultraLabel1.Text = "如果未在報表清單中找到你要的格式，請執行右方【線上更新】按鈕，下載所需的報表格式";
		this.groupBox5.Controls.Add(this.panel2);
		this.groupBox5.Location = new System.Drawing.Point(8, 14);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(588, 108);
		this.groupBox5.TabIndex = 1;
		this.groupBox5.TabStop = false;
		this.panel2.AutoScroll = true;
		this.panel2.Controls.Add(this.ReportList);
		this.panel2.Controls.Add(this.lbl_NoRPT);
		this.panel2.Location = new System.Drawing.Point(8, 14);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(572, 86);
		this.panel2.TabIndex = 0;
		this.ReportList.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
		this.ReportList.ItemAppearance = appearance13;
		this.ReportList.ItemOrigin = new System.Drawing.Point(8, 5);
		valueListItem1.DataValue = "Default Item";
		valueListItem1.DisplayText = "Default Item";
		valueListItem2.DataValue = "ValueListItem1";
		valueListItem3.DataValue = "ValueListItem2";
		valueListItem4.DataValue = "ValueListItem3";
		valueListItem5.DataValue = "ValueListItem4";
		valueListItem6.DataValue = "ValueListItem5";
		valueListItem7.DataValue = "ValueListItem6";
		this.ReportList.Items.Add(valueListItem1);
		this.ReportList.Items.Add(valueListItem2);
		this.ReportList.Items.Add(valueListItem3);
		this.ReportList.Items.Add(valueListItem4);
		this.ReportList.Items.Add(valueListItem5);
		this.ReportList.Items.Add(valueListItem6);
		this.ReportList.Items.Add(valueListItem7);
		this.ReportList.ItemSpacingVertical = 6;
		this.ReportList.Location = new System.Drawing.Point(7, 4);
		this.ReportList.Name = "ReportList";
		this.ReportList.Size = new System.Drawing.Size(521, 75);
		this.ReportList.TabIndex = 0;
		this.ReportList.TextIndentation = 2;
		this.ReportList.UseMnemonics = true;
		appearance14.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance14.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lbl_NoRPT.Appearance = appearance14;
		this.lbl_NoRPT.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.lbl_NoRPT.Location = new System.Drawing.Point(208, 32);
		this.lbl_NoRPT.Name = "lbl_NoRPT";
		this.lbl_NoRPT.Size = new System.Drawing.Size(180, 23);
		this.lbl_NoRPT.TabIndex = 1;
		this.lbl_NoRPT.Text = "找不到報表格式";
		this.groupBox1.Controls.Add(this.PNL_RPT);
		this.groupBox1.Location = new System.Drawing.Point(7, 4);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(604, 146);
		this.groupBox1.TabIndex = 11;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "報表種類";
		this.PNL_RPT.AutoScroll = true;
		this.PNL_RPT.Location = new System.Drawing.Point(8, 21);
		this.PNL_RPT.Name = "PNL_RPT";
		this.PNL_RPT.Size = new System.Drawing.Size(584, 115);
		this.PNL_RPT.TabIndex = 0;
		this.Tab_B.Controls.Add(this.ultraLabel10);
		this.Tab_B.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_B.Name = "Tab_B";
		this.Tab_B.Size = new System.Drawing.Size(616, 603);
		appearance15.TextHAlign = Infragistics.Win.HAlign.Center;
		appearance15.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel10.Appearance = appearance15;
		this.ultraLabel10.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ultraLabel10.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel10.Location = new System.Drawing.Point(0, 0);
		this.ultraLabel10.Name = "ultraLabel10";
		this.ultraLabel10.Size = new System.Drawing.Size(616, 603);
		this.ultraLabel10.TabIndex = 1;
		this.ultraLabel10.Text = "報表處理中，這個動作會花上數分鐘，請耐心等侯";
		this.Tab_C.Controls.Add(this.PNL_CRP);
		this.Tab_C.Controls.Add(this.panel7);
		this.Tab_C.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_C.Name = "Tab_C";
		this.Tab_C.Size = new System.Drawing.Size(616, 603);
		this.PNL_CRP.Dock = System.Windows.Forms.DockStyle.Fill;
		this.PNL_CRP.Location = new System.Drawing.Point(0, 0);
		this.PNL_CRP.Name = "PNL_CRP";
		this.PNL_CRP.Size = new System.Drawing.Size(616, 561);
		this.PNL_CRP.TabIndex = 12;
		this.panel7.Controls.Add(this.groupBox4);
		this.panel7.Controls.Add(this.ultraButton1);
		this.panel7.Controls.Add(this.ultraButton2);
		this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel7.Location = new System.Drawing.Point(0, 561);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(616, 42);
		this.panel7.TabIndex = 11;
		this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox4.Location = new System.Drawing.Point(0, 0);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(616, 8);
		this.groupBox4.TabIndex = 3;
		this.groupBox4.TabStop = false;
		this.ultraButton1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance16.Image = resources.GetObject("appearance16.Image");
		appearance16.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton1.Appearance = appearance16;
		this.ultraButton1.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ultraButton1.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton1.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton1.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton1.Location = new System.Drawing.Point(522, 9);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.ShowFocusRect = false;
		this.ultraButton1.ShowOutline = false;
		this.ultraButton1.Size = new System.Drawing.Size(88, 31);
		this.ultraButton1.SupportThemes = false;
		this.ultraButton1.TabIndex = 2;
		this.ultraButton1.Text = "取消";
		this.ultraButton1.Click += new System.EventHandler(ultraButton1_Click);
		this.ultraButton2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance17.Image = resources.GetObject("appearance17.Image");
		appearance17.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton2.Appearance = appearance17;
		this.ultraButton2.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton2.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton2.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton2.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton2.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton2.Location = new System.Drawing.Point(430, 9);
		this.ultraButton2.Name = "ultraButton2";
		this.ultraButton2.ShowFocusRect = false;
		this.ultraButton2.ShowOutline = false;
		this.ultraButton2.Size = new System.Drawing.Size(88, 31);
		this.ultraButton2.SupportThemes = false;
		this.ultraButton2.TabIndex = 1;
		this.ultraButton2.Text = "上一頁";
		this.ultraButton2.Click += new System.EventHandler(ultraButton2_Click);
		this.Tab_Ctrl.Controls.Add(this.ultraTabSharedControlsPage1);
		this.Tab_Ctrl.Controls.Add(this.Tab_A);
		this.Tab_Ctrl.Controls.Add(this.Tab_B);
		this.Tab_Ctrl.Controls.Add(this.Tab_C);
		this.Tab_Ctrl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Tab_Ctrl.Location = new System.Drawing.Point(0, 0);
		this.Tab_Ctrl.Name = "Tab_Ctrl";
		this.Tab_Ctrl.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.Tab_Ctrl.Size = new System.Drawing.Size(616, 603);
		this.Tab_Ctrl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
		this.Tab_Ctrl.TabIndex = 0;
		ultraTab1.Key = "Tab_A";
		ultraTab1.TabPage = this.Tab_A;
		ultraTab1.Text = "tab1";
		ultraTab2.Key = "Tab_D";
		ultraTab2.TabPage = this.Tab_B;
		ultraTab2.Text = "tab2";
		ultraTab3.Key = "Tab_C";
		ultraTab3.TabPage = this.Tab_C;
		ultraTab3.Text = "tab3";
		this.Tab_Ctrl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[3] { ultraTab1, ultraTab2, ultraTab3 });
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(616, 603);
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.CancelButton = this.A_Btn_Cncl;
		base.ClientSize = new System.Drawing.Size(616, 603);
		base.Controls.Add(this.Tab_Ctrl);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormInvoiceReport";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "報表列印";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormInvoiceReport_KeyDown);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormInvoiceReport_FormClosing);
		base.Load += new System.EventHandler(FormInvoiceReport_Load);
		this.Tab_A.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		this.groupBox6.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtPath).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtFileName).EndInit();
		this.groupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.SetEnd).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cmp_Ename).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cmp_name).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox5.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ReportList).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.Tab_B.ResumeLayout(false);
		this.Tab_C.ResumeLayout(false);
		this.panel7.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).EndInit();
		this.Tab_Ctrl.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	private void FormInvoiceReport_Load(object sender, EventArgs e)
	{
		base.Height = 537;
		txtFileName.Text = F_ProjectCode + "_Access_" + $"{DateTime.Now:yyyyMMddHHmmss}" + ".MDB";
		txtPath.Text = "C:\\";
		LoadRPT_Tail();
		LoadReports();
		IniFormSetting();
	}

	private void LoadRPT_Tail()
	{
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(PrnSubCtr) 列印報表");
		UserDefind UserCom = new UserDefind(tmp_AL1);
		Archnowledge.Pcces.BUDClass.Project ProjCom = new Archnowledge.Pcces.BUDClass.Project(tmp_AL1);
		ProjCom.ps_srckind = "BUD";
		DataTable tmp = ProjCom.ListItem("", F_ProjectCode.Trim());
		ProjCom = null;
		if (tmp.Rows.Count == 1)
		{
			string ls_maincode = tmp.Rows[0]["mainCode"].ToString();
			cmp_name.Text = Class1.get_cmp_name(ls_maincode, F_UserID);
			cmp_Ename.Text = Class1.get_cmp_Ename(ls_maincode, F_UserID);
		}
		else
		{
			cmp_name.Text = "[工程主辦機關]";
		}
		SetEnd.Text = "";
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("報表預覽，讀取表尾簽核欄資料");
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
	}

	private void LoadReports()
	{
	}

	private void IniFormSetting()
	{
		ReportList.Height = ReportList.Items.Count * 25;
		if (F_ActionName == PccesFormAction.SplitContract)
		{
			RPT1._ProjectCode = F_ProjectCode;
			RPT1._SubProjectCode = F_SubProjectCode;
			RPT1._UserID = F_UserID;
			PNL_RPT.Controls.Add(RPT1);
		}
		else if (F_ActionName == PccesFormAction.Invoice)
		{
			RPT2._ProjectCode = F_ProjectCode;
			RPT2._SubProjectCode = F_SubProjectCode;
			RPT2._UserID = F_UserID;
			RPT2._Issue = F_Issue;
			PNL_RPT.Controls.Add(RPT2);
		}
		else if (F_ActionName == PccesFormAction.SubChange)
		{
			RPT3._ProjectCode = F_ProjectCode;
			RPT3._SubProjectCode = F_SubProjectCode;
			RPT3._UserID = F_UserID;
			RPT3._Issue = F_Issue;
			PNL_RPT.Controls.Add(RPT3);
		}
		else if (F_ActionName == PccesFormAction.SubClose)
		{
			RPT4._ProjectCode = F_ProjectCode;
			RPT4._SubProjectCode = F_SubProjectCode;
			RPT4._UserID = F_UserID;
			RPT4._Issue = F_Issue;
			PNL_RPT.Controls.Add(RPT4);
		}
		else if (F_ActionName == PccesFormAction.SubFinal)
		{
			RPT5._ProjectCode = F_ProjectCode;
			RPT5._SubProjectCode = F_SubProjectCode;
			RPT5._UserID = F_UserID;
			RPT5._Issue = F_Issue;
			PNL_RPT.Controls.Add(RPT5);
		}
	}

	public void Load_RptKind(RepKind MyKind)
	{
		ReportList.Items.Clear();
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(PrnSubCtr)讀取報表列表");
		RepListClass RptListCom = new RepListClass(tmp_AL1);
		DataTable dt_RepList = RptListCom.ListItem("", MyKind);
		RptListCom = null;
		int iix = 0;
		if (dt_RepList.Rows.Count > 0)
		{
			A_Btn_OK.Enabled = true;
			foreach (DataRow dr in dt_RepList.Rows)
			{
				ValueListItem li = new ValueListItem();
				li.DataValue = dr["RptFn"].ToString().Trim();
				int num = ++iix;
				li.DisplayText = "&" + num + "." + dr["RptTitle"].ToString().Trim();
				ReportList.Items.Add(li);
			}
			ReportList.CheckedIndex = 0;
		}
		else
		{
			A_Btn_OK.Enabled = false;
		}
		ReportList.Height = ReportList.Items.Count * 30;
		if (iix == 0)
		{
			lbl_NoRPT.Visible = true;
		}
		else
		{
			lbl_NoRPT.Visible = false;
		}
	}

	public string GetReportName()
	{
		return ReportList.CheckedItem.DataValue.ToString();
	}

	public string GetDBFName()
	{
		return "pccesAccess.mdb";
	}

	public void JumpToPage2()
	{
		base.MaximizeBox = true;
		base.MinimizeBox = false;
		base.WindowState = FormWindowState.Maximized;
		Tab_B.Tab.Selected = true;
		Application.DoEvents();
	}

	private void JumpToPage3()
	{
		try
		{
			Application.DoEvents();
			CRVer = new ucCrystalViewer();
			CRVer._ReportPath = CommonMethods.ExtractFilePath(Application.ExecutablePath) + "Report\\";
			CRVer._ReportName = GetReportName();
			CRVer._DBFName = GetDBFName();
			CRVer._CompWidth = Tab_B.Width;
			CRVer._CompHeight = Tab_B.Height;
			CRVer.Dock = DockStyle.Fill;
			PNL_CRP.Controls.Add(CRVer);
			CRVer.Execute();
			Tab_C.Tab.Selected = true;
			Application.DoEvents();
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Report.FormInvoiceReport.cs" + ex.Message);
			MessageBox.Show(this, ex.Message + "\n請確認你的報表格式檔已存在。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			base.WindowState = FormWindowState.Normal;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			Tab_A.Tab.Selected = true;
		}
	}

	private void A_Btn_Next_Click(object sender, EventArgs e)
	{
		string ssAccessFileName = "";
		if (A_Btn_OK.Text == "匯出資料")
		{
			if (txtFileName.Text.Trim() == "")
			{
				string sWarning = "請先給定檔案名稱";
				MessageBox.Show(this, sWarning, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtFileName.Focus();
				return;
			}
			if (txtPath.Text.Trim() == "")
			{
				string sWarning = "請先給定輸出路徑";
				MessageBox.Show(this, sWarning, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtPath.Focus();
				return;
			}
			if (!Directory.Exists(txtPath.Text.Trim()))
			{
				string sWarning = "你所指定的路徑並不存在，請重新挑選。";
				MessageBox.Show(this, sWarning, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtPath.Focus();
				return;
			}
			if (txtFileName.Text.Trim().ToUpper().IndexOf(".MDB") <= -1)
			{
				txtFileName.Text = txtFileName.Text.Trim() + ".mdb";
			}
			bool IsAddSlash = !(txtPath.Text.Trim().Substring(txtPath.Text.Trim().Length - 1, 1) == "\\");
			ssAccessFileName = txtPath.Text.Trim() + (IsAddSlash ? "\\" : "") + txtFileName.Text.Trim();
		}
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(PrnSubCtr) 列印報表");
		UserDefind UserCom = new UserDefind(tmp_AL1);
		UserCom.SetDefaultFooter(SetEnd.Text);
		if (F_ActionName == PccesFormAction.SplitContract)
		{
			RPT1._cmp_name = cmp_name.Text;
			RPT1._cmp_Ename = cmp_Ename.Text;
			RPT1._RPT_Tail = SetEnd.Text;
			RPT1._IsAccess = A_Btn_OK.Text == "匯出資料";
			RPT1._AccessFileName = ssAccessFileName;
			RPT1.GenerateData();
		}
		if (F_ActionName == PccesFormAction.Invoice)
		{
			RPT2._cmp_name = cmp_name.Text;
			RPT2._cmp_Ename = cmp_Ename.Text;
			RPT2._RPT_Tail = SetEnd.Text;
			RPT2._IsAccess = A_Btn_OK.Text == "匯出資料";
			RPT2._AccessFileName = ssAccessFileName;
			RPT2.GenerateData();
		}
		if (F_ActionName == PccesFormAction.SubChange)
		{
			RPT3._cmp_name = cmp_name.Text;
			RPT3._cmp_Ename = cmp_Ename.Text;
			RPT3._RPT_Tail = SetEnd.Text;
			RPT3._IsAccess = A_Btn_OK.Text == "匯出資料";
			RPT3._AccessFileName = ssAccessFileName;
			RPT3.GenerateData();
		}
		if (F_ActionName == PccesFormAction.SubClose)
		{
			RPT4._cmp_name = cmp_name.Text;
			RPT4._cmp_Ename = cmp_Ename.Text;
			RPT4._RPT_Tail = SetEnd.Text;
			RPT4._IsAccess = A_Btn_OK.Text == "匯出資料";
			RPT4._AccessFileName = ssAccessFileName;
			RPT4.GenerateData();
		}
		if (F_ActionName == PccesFormAction.SubFinal)
		{
			RPT5._cmp_name = cmp_name.Text;
			RPT5._cmp_Ename = cmp_Ename.Text;
			RPT5._RPT_Tail = SetEnd.Text;
			RPT5._IsAccess = A_Btn_OK.Text == "匯出資料";
			RPT5._AccessFileName = ssAccessFileName;
			RPT5.GenerateData();
		}
		if (A_Btn_OK.Text == "匯出資料")
		{
			MessageBox.Show(this, "Access資料檔轉出完成", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		else
		{
			JumpToPage3();
		}
		Cursor = Cursors.Default;
	}

	private void ultraButton1_Click(object sender, EventArgs e)
	{
		PNL_CRP.Controls.Remove(CRVer);
		CRVer.Dispose();
		CRVer = null;
	}

	private void ultraButton2_Click(object sender, EventArgs e)
	{
		try
		{
			PNL_CRP.Controls.Remove(CRVer);
			CRVer.Dispose();
			CRVer = null;
			base.WindowState = FormWindowState.Normal;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			Tab_A.Tab.Selected = true;
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Report.FormInvoiceReport.cs" + ex.Message);
			Console.WriteLine(ex.Message);
		}
	}

	private void ultraButton3_Click(object sender, EventArgs e)
	{
		FormInvReportCheck FM_INVRPT_CHK = new FormInvReportCheck();
		FM_INVRPT_CHK._UserID = F_UserID;
		FM_INVRPT_CHK._ReportKind = GetReportKindString();
		if (FM_INVRPT_CHK.ShowDialog() == DialogResult.OK)
		{
			if (F_ActionName == PccesFormAction.SplitContract)
			{
				RPT1.ReloadReports();
			}
			else if (F_ActionName == PccesFormAction.Invoice)
			{
				RPT2.ReloadReports();
			}
			else if (F_ActionName == PccesFormAction.BudgetChange)
			{
				RPT3.ReloadReports();
			}
			else if (F_ActionName == PccesFormAction.SubClose)
			{
				RPT4.ReloadReports();
			}
			else if (F_ActionName == PccesFormAction.SubFinal)
			{
				RPT5.ReloadReports();
			}
		}
		FM_INVRPT_CHK.Close();
		FM_INVRPT_CHK.Dispose();
		FM_INVRPT_CHK = null;
	}

	private string GetReportKindString()
	{
		string RetV = "SubCtr";
		if (F_ActionName == PccesFormAction.SplitContract)
		{
			RetV = "SubCtr";
		}
		if (F_ActionName == PccesFormAction.Invoice)
		{
			RetV = "SubAcc";
		}
		if (F_ActionName == PccesFormAction.SubChange)
		{
			RetV = "SubChg";
		}
		if (F_ActionName == PccesFormAction.SubClose)
		{
			RetV = "SubClose";
		}
		if (F_ActionName == PccesFormAction.SubFinal)
		{
			RetV = "SubFinal";
		}
		return RetV;
	}

	private void FormInvoiceReport_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (Tab_Ctrl.ActiveTab.Key == "Tab_C")
		{
			ultraButton2_Click(this, EventArgs.Empty);
		}
	}

	private void A_Btn_Cncl_Click(object sender, EventArgs e)
	{
	}

	private void button1_Click(object sender, EventArgs e)
	{
		if (base.Height == 537)
		{
			base.Height = 635;
			button1.Text = "匯出ACCES資料檔 ▲";
			A_Btn_OK.Text = "匯出資料";
		}
		else
		{
			base.Height = 537;
			button1.Text = "匯出ACCES資料檔 ▼";
			A_Btn_OK.Text = "預覽報表";
		}
	}

	private void BtnChgDir_Click(object sender, EventArgs e)
	{
		folderBrowserDialog1.Description = "請挑選你要輸出的路徑";
		if (txtPath.Text.Trim() != "")
		{
			folderBrowserDialog1.SelectedPath = txtPath.Text.Trim();
		}
		if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
		{
			txtPath.Text = folderBrowserDialog1.SelectedPath;
		}
	}

	private void FormInvoiceReport_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormInvoiceReport");
		}
	}
}
