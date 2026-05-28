using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.CommonClass.MrsBase;
using Archnowledge.Pcces.STDClass;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinProgressBar;
using Infragistics.Win.UltraWinTabControl;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormMrsCodeChange_ImpWizard : Form
{
	private const string CallFormHelp = "FormMrsBase_ImpWizard";

	private UltraTabControl TabCtrl;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl Tab_A;

	private Panel panel1;

	private GroupBox groupBox1;

	private UltraButton A_Btn_Cncl;

	private UltraButton A_Btn_Next;

	private UltraButton A_Btn_Prev;

	private RadioButton RB2;

	private RadioButton RB1;

	private UltraLabel ultraLabel2;

	private UltraLabel ultraLabel1;

	private UltraTabPageControl Tab_B;

	private Panel panel3;

	private UltraLabel ultraLabel5;

	private UltraButton ultraButton4;

	private Panel panel2;

	private GroupBox groupBox2;

	private UltraButton B_Btn_Cncl;

	private UltraButton B_Btn_Next;

	private UltraButton B_Btn_Prev;

	private Panel panel5;

	private UltraLabel ultraLabel7;

	private UltraLabel ultraLabel6;

	private UltraTabPageControl Tab_C;

	private UltraLabel lblWait;

	private UltraProgressBar Prog1;

	private Panel panel7;

	private GroupBox groupBox4;

	private UltraButton C_Btn_Cncl;

	private UltraButton C_Btn_Next;

	private UltraButton C_Btn_Prev;

	private UltraLabel lblProg1;

	private Panel panel4;

	private UltraLabel ultraLabel9;

	private UltraTabPageControl Tab_D;

	private UltraLabel ultraLabel14;

	private UltraLabel ultraLabel13;

	private UltraLabel ultraLabel12;

	private Panel panel6;

	private GroupBox groupBox3;

	private UltraButton D_Btn_Fnsh;

	private UltraButton D_Btn_Prev;

	private Timer timer1;

	private OpenFileDialog openFileDialog1;

	private UltraTextEditor txtImpDirFile;

	private Label lblWanring;

	private IContainer components;

	private PccesFormAction F_ActionName;

	private string F_ProjectCode = "";

	private ImportType F_ImportType;

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

	public ImportType _ImportType
	{
		get
		{
			return F_ImportType;
		}
		set
		{
			F_ImportType = value;
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

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormMrsCodeChange_ImpWizard));
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
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		this.Tab_A = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.lblWanring = new System.Windows.Forms.Label();
		this.panel1 = new System.Windows.Forms.Panel();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.A_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.A_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.A_Btn_Prev = new Infragistics.Win.Misc.UltraButton();
		this.RB2 = new System.Windows.Forms.RadioButton();
		this.RB1 = new System.Windows.Forms.RadioButton();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_B = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel3 = new System.Windows.Forms.Panel();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraButton4 = new Infragistics.Win.Misc.UltraButton();
		this.txtImpDirFile = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.panel2 = new System.Windows.Forms.Panel();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.B_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.B_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.B_Btn_Prev = new Infragistics.Win.Misc.UltraButton();
		this.panel5 = new System.Windows.Forms.Panel();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_C = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.lblWait = new Infragistics.Win.Misc.UltraLabel();
		this.Prog1 = new Infragistics.Win.UltraWinProgressBar.UltraProgressBar();
		this.panel7 = new System.Windows.Forms.Panel();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.C_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.C_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.C_Btn_Prev = new Infragistics.Win.Misc.UltraButton();
		this.lblProg1 = new Infragistics.Win.Misc.UltraLabel();
		this.panel4 = new System.Windows.Forms.Panel();
		this.ultraLabel9 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_D = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.ultraLabel14 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel13 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel12 = new Infragistics.Win.Misc.UltraLabel();
		this.panel6 = new System.Windows.Forms.Panel();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.D_Btn_Fnsh = new Infragistics.Win.Misc.UltraButton();
		this.D_Btn_Prev = new Infragistics.Win.Misc.UltraButton();
		this.TabCtrl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
		this.Tab_A.SuspendLayout();
		this.panel1.SuspendLayout();
		this.Tab_B.SuspendLayout();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtImpDirFile).BeginInit();
		this.panel2.SuspendLayout();
		this.panel5.SuspendLayout();
		this.Tab_C.SuspendLayout();
		this.panel7.SuspendLayout();
		this.panel4.SuspendLayout();
		this.Tab_D.SuspendLayout();
		this.panel6.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TabCtrl).BeginInit();
		this.TabCtrl.SuspendLayout();
		base.SuspendLayout();
		this.Tab_A.Controls.Add(this.lblWanring);
		this.Tab_A.Controls.Add(this.panel1);
		this.Tab_A.Controls.Add(this.RB2);
		this.Tab_A.Controls.Add(this.RB1);
		this.Tab_A.Controls.Add(this.ultraLabel2);
		this.Tab_A.Controls.Add(this.ultraLabel1);
		this.Tab_A.Location = new System.Drawing.Point(0, 0);
		this.Tab_A.Name = "Tab_A";
		this.Tab_A.Size = new System.Drawing.Size(516, 369);
		this.lblWanring.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.lblWanring.ForeColor = System.Drawing.Color.Red;
		this.lblWanring.Location = new System.Drawing.Point(44, 228);
		this.lblWanring.Name = "lblWanring";
		this.lblWanring.Size = new System.Drawing.Size(420, 23);
		this.lblWanring.TabIndex = 11;
		this.lblWanring.Text = "* 此匯入功能只作工作要項的換碼，不作資料轉入。";
		this.lblWanring.Visible = false;
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.Controls.Add(this.groupBox1);
		this.panel1.Controls.Add(this.A_Btn_Cncl);
		this.panel1.Controls.Add(this.A_Btn_Next);
		this.panel1.Controls.Add(this.A_Btn_Prev);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 325);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(516, 44);
		this.panel1.TabIndex = 9;
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(516, 8);
		this.groupBox1.TabIndex = 3;
		this.groupBox1.TabStop = false;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Cncl.Appearance = appearance1;
		this.A_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.A_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_Cncl.Location = new System.Drawing.Point(416, 9);
		this.A_Btn_Cncl.Name = "A_Btn_Cncl";
		this.A_Btn_Cncl.ShowFocusRect = false;
		this.A_Btn_Cncl.ShowOutline = false;
		this.A_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.A_Btn_Cncl.SupportThemes = false;
		this.A_Btn_Cncl.TabIndex = 2;
		this.A_Btn_Cncl.Text = "取消";
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Next.Appearance = appearance2;
		this.A_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_Next.Location = new System.Drawing.Point(324, 9);
		this.A_Btn_Next.Name = "A_Btn_Next";
		this.A_Btn_Next.ShowFocusRect = false;
		this.A_Btn_Next.ShowOutline = false;
		this.A_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.A_Btn_Next.SupportThemes = false;
		this.A_Btn_Next.TabIndex = 1;
		this.A_Btn_Next.Text = "下一步";
		this.A_Btn_Next.Click += new System.EventHandler(A_Btn_Next_Click);
		appearance3.Image = resources.GetObject("appearance3.Image");
		appearance3.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Prev.Appearance = appearance3;
		this.A_Btn_Prev.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Prev.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Prev.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Prev.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Prev.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_Prev.Location = new System.Drawing.Point(232, 9);
		this.A_Btn_Prev.Name = "A_Btn_Prev";
		this.A_Btn_Prev.ShowFocusRect = false;
		this.A_Btn_Prev.ShowOutline = false;
		this.A_Btn_Prev.Size = new System.Drawing.Size(88, 31);
		this.A_Btn_Prev.SupportThemes = false;
		this.A_Btn_Prev.TabIndex = 0;
		this.A_Btn_Prev.Text = "上一步";
		this.A_Btn_Prev.Visible = false;
		this.RB2.BackColor = System.Drawing.Color.White;
		this.RB2.Checked = true;
		this.RB2.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.RB2.Location = new System.Drawing.Point(48, 154);
		this.RB2.Name = "RB2";
		this.RB2.Size = new System.Drawing.Size(276, 24);
		this.RB2.TabIndex = 7;
		this.RB2.TabStop = true;
		this.RB2.Text = "更換或刪除基本資料庫中之舊碼";
		this.RB1.BackColor = System.Drawing.Color.White;
		this.RB1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.RB1.Location = new System.Drawing.Point(48, 74);
		this.RB1.Name = "RB1";
		this.RB1.Size = new System.Drawing.Size(272, 24);
		this.RB1.TabIndex = 3;
		this.RB1.Text = "於基本資料庫保留舊碼";
		appearance4.BackColor = System.Drawing.Color.White;
		this.ultraLabel2.Appearance = appearance4;
		this.ultraLabel2.Location = new System.Drawing.Point(43, 52);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel2.TabIndex = 2;
		this.ultraLabel2.Text = "舊碼曾於其他預算書使用之處理方式?";
		appearance5.BackColor = System.Drawing.Color.White;
		this.ultraLabel1.Appearance = appearance5;
		this.ultraLabel1.Location = new System.Drawing.Point(8, 16);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(500, 20);
		this.ultraLabel1.TabIndex = 1;
		this.ultraLabel1.Text = "歡迎使用基本資料匯入精靈，接下來我們將引導您一步一步匯入資料";
		this.Tab_B.Controls.Add(this.panel3);
		this.Tab_B.Controls.Add(this.panel2);
		this.Tab_B.Controls.Add(this.panel5);
		this.Tab_B.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_B.Name = "Tab_B";
		this.Tab_B.Size = new System.Drawing.Size(516, 369);
		this.panel3.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel3.Controls.Add(this.ultraLabel5);
		this.panel3.Controls.Add(this.ultraButton4);
		this.panel3.Controls.Add(this.txtImpDirFile);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel3.Location = new System.Drawing.Point(0, 60);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(516, 265);
		this.panel3.TabIndex = 14;
		this.ultraLabel5.Location = new System.Drawing.Point(11, 48);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel5.TabIndex = 4;
		this.ultraLabel5.Text = "來源檔的目錄及檔名:";
		appearance6.FontData.Name = "Arial";
		appearance6.FontData.SizeInPoints = 8f;
		this.ultraButton4.Appearance = appearance6;
		this.ultraButton4.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton4.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.ultraButton4.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraButton4.Location = new System.Drawing.Point(459, 71);
		this.ultraButton4.Name = "ultraButton4";
		this.ultraButton4.ShowFocusRect = false;
		this.ultraButton4.ShowOutline = false;
		this.ultraButton4.Size = new System.Drawing.Size(48, 24);
		this.ultraButton4.SupportThemes = false;
		this.ultraButton4.TabIndex = 1;
		this.ultraButton4.Text = "瀏覽...";
		this.ultraButton4.Click += new System.EventHandler(ultraButton4_Click);
		appearance7.FontData.Name = "細明體";
		appearance7.FontData.SizeInPoints = 11f;
		this.txtImpDirFile.Appearance = appearance7;
		this.txtImpDirFile.Location = new System.Drawing.Point(10, 72);
		this.txtImpDirFile.Name = "txtImpDirFile";
		this.txtImpDirFile.Size = new System.Drawing.Size(450, 24);
		this.txtImpDirFile.TabIndex = 0;
		this.panel2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel2.Controls.Add(this.groupBox2);
		this.panel2.Controls.Add(this.B_Btn_Cncl);
		this.panel2.Controls.Add(this.B_Btn_Next);
		this.panel2.Controls.Add(this.B_Btn_Prev);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 325);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(516, 44);
		this.panel2.TabIndex = 13;
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(516, 8);
		this.groupBox2.TabIndex = 3;
		this.groupBox2.TabStop = false;
		appearance8.Image = resources.GetObject("appearance8.Image");
		appearance8.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.B_Btn_Cncl.Appearance = appearance8;
		this.B_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.B_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.B_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.B_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.B_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.B_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.B_Btn_Cncl.Location = new System.Drawing.Point(416, 9);
		this.B_Btn_Cncl.Name = "B_Btn_Cncl";
		this.B_Btn_Cncl.ShowFocusRect = false;
		this.B_Btn_Cncl.ShowOutline = false;
		this.B_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.B_Btn_Cncl.SupportThemes = false;
		this.B_Btn_Cncl.TabIndex = 2;
		this.B_Btn_Cncl.Text = "取消";
		appearance9.Image = resources.GetObject("appearance9.Image");
		appearance9.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance9.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.B_Btn_Next.Appearance = appearance9;
		this.B_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.B_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.B_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.B_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.B_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.B_Btn_Next.Location = new System.Drawing.Point(324, 9);
		this.B_Btn_Next.Name = "B_Btn_Next";
		this.B_Btn_Next.ShowFocusRect = false;
		this.B_Btn_Next.ShowOutline = false;
		this.B_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.B_Btn_Next.SupportThemes = false;
		this.B_Btn_Next.TabIndex = 1;
		this.B_Btn_Next.Text = "下一步";
		this.B_Btn_Next.Click += new System.EventHandler(B_Btn_Next_Click);
		appearance10.Image = resources.GetObject("appearance10.Image");
		appearance10.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.B_Btn_Prev.Appearance = appearance10;
		this.B_Btn_Prev.BackColor = System.Drawing.SystemColors.Control;
		this.B_Btn_Prev.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.B_Btn_Prev.Font = new System.Drawing.Font("細明體", 11f);
		this.B_Btn_Prev.ImageSize = new System.Drawing.Size(20, 20);
		this.B_Btn_Prev.ImageTransparentColor = System.Drawing.Color.White;
		this.B_Btn_Prev.Location = new System.Drawing.Point(232, 9);
		this.B_Btn_Prev.Name = "B_Btn_Prev";
		this.B_Btn_Prev.ShowFocusRect = false;
		this.B_Btn_Prev.ShowOutline = false;
		this.B_Btn_Prev.Size = new System.Drawing.Size(88, 31);
		this.B_Btn_Prev.SupportThemes = false;
		this.B_Btn_Prev.TabIndex = 0;
		this.B_Btn_Prev.Text = "上一步";
		this.B_Btn_Prev.Click += new System.EventHandler(B_Btn_Prev_Click);
		this.panel5.BackColor = System.Drawing.Color.White;
		this.panel5.Controls.Add(this.ultraLabel7);
		this.panel5.Controls.Add(this.ultraLabel6);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel5.Location = new System.Drawing.Point(0, 0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(516, 60);
		this.panel5.TabIndex = 12;
		appearance11.BackColor = System.Drawing.Color.White;
		this.ultraLabel7.Appearance = appearance11;
		this.ultraLabel7.Location = new System.Drawing.Point(47, 34);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel7.TabIndex = 3;
		this.ultraLabel7.Text = "請挑選匯入的資料來源檔所存放位置";
		appearance12.BackColor = System.Drawing.Color.White;
		this.ultraLabel6.Appearance = appearance12;
		this.ultraLabel6.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel6.Location = new System.Drawing.Point(16, 12);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel6.TabIndex = 2;
		this.ultraLabel6.Text = "資料匯入來源檔案挑選";
		this.Tab_C.Controls.Add(this.lblWait);
		this.Tab_C.Controls.Add(this.Prog1);
		this.Tab_C.Controls.Add(this.panel7);
		this.Tab_C.Controls.Add(this.lblProg1);
		this.Tab_C.Controls.Add(this.panel4);
		this.Tab_C.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_C.Name = "Tab_C";
		this.Tab_C.Size = new System.Drawing.Size(516, 369);
		this.lblWait.Location = new System.Drawing.Point(16, 81);
		this.lblWait.Name = "lblWait";
		this.lblWait.Size = new System.Drawing.Size(476, 20);
		this.lblWait.TabIndex = 17;
		this.lblWait.Text = "正在準備匯入的資料，這個動作會花些時間，請稍候。";
		appearance13.BackColor = System.Drawing.Color.White;
		appearance13.BackColor2 = System.Drawing.Color.White;
		appearance13.FontData.Name = "細明體";
		appearance13.FontData.SizeInPoints = 11f;
		this.Prog1.Appearance = appearance13;
		appearance14.BackColor = System.Drawing.Color.FromArgb(192, 192, 255);
		appearance14.BackColor2 = System.Drawing.Color.Navy;
		appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
		this.Prog1.FillAppearance = appearance14;
		this.Prog1.Location = new System.Drawing.Point(20, 128);
		this.Prog1.Name = "Prog1";
		this.Prog1.Size = new System.Drawing.Size(476, 23);
		this.Prog1.SupportThemes = false;
		this.Prog1.TabIndex = 16;
		this.Prog1.Text = "[Formatted]";
		this.Prog1.Visible = false;
		this.panel7.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel7.Controls.Add(this.groupBox4);
		this.panel7.Controls.Add(this.C_Btn_Cncl);
		this.panel7.Controls.Add(this.C_Btn_Next);
		this.panel7.Controls.Add(this.C_Btn_Prev);
		this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel7.Location = new System.Drawing.Point(0, 325);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(516, 44);
		this.panel7.TabIndex = 15;
		this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox4.Location = new System.Drawing.Point(0, 0);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(516, 8);
		this.groupBox4.TabIndex = 3;
		this.groupBox4.TabStop = false;
		appearance15.Image = resources.GetObject("appearance15.Image");
		appearance15.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.C_Btn_Cncl.Appearance = appearance15;
		this.C_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.C_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.C_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.C_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.C_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.C_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.C_Btn_Cncl.Location = new System.Drawing.Point(416, 9);
		this.C_Btn_Cncl.Name = "C_Btn_Cncl";
		this.C_Btn_Cncl.ShowFocusRect = false;
		this.C_Btn_Cncl.ShowOutline = false;
		this.C_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.C_Btn_Cncl.SupportThemes = false;
		this.C_Btn_Cncl.TabIndex = 2;
		this.C_Btn_Cncl.Text = "取消";
		this.C_Btn_Cncl.Visible = false;
		appearance16.Image = resources.GetObject("appearance16.Image");
		appearance16.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance16.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.C_Btn_Next.Appearance = appearance16;
		this.C_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.C_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.C_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.C_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.C_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.C_Btn_Next.Location = new System.Drawing.Point(324, 9);
		this.C_Btn_Next.Name = "C_Btn_Next";
		this.C_Btn_Next.ShowFocusRect = false;
		this.C_Btn_Next.ShowOutline = false;
		this.C_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.C_Btn_Next.SupportThemes = false;
		this.C_Btn_Next.TabIndex = 1;
		this.C_Btn_Next.Text = "下一步";
		this.C_Btn_Next.Visible = false;
		appearance17.Image = resources.GetObject("appearance17.Image");
		appearance17.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.C_Btn_Prev.Appearance = appearance17;
		this.C_Btn_Prev.BackColor = System.Drawing.SystemColors.Control;
		this.C_Btn_Prev.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.C_Btn_Prev.Font = new System.Drawing.Font("細明體", 11f);
		this.C_Btn_Prev.ImageSize = new System.Drawing.Size(20, 20);
		this.C_Btn_Prev.ImageTransparentColor = System.Drawing.Color.White;
		this.C_Btn_Prev.Location = new System.Drawing.Point(232, 9);
		this.C_Btn_Prev.Name = "C_Btn_Prev";
		this.C_Btn_Prev.ShowFocusRect = false;
		this.C_Btn_Prev.ShowOutline = false;
		this.C_Btn_Prev.Size = new System.Drawing.Size(88, 31);
		this.C_Btn_Prev.SupportThemes = false;
		this.C_Btn_Prev.TabIndex = 0;
		this.C_Btn_Prev.Text = "上一步";
		this.C_Btn_Prev.Visible = false;
		this.lblProg1.Location = new System.Drawing.Point(16, 104);
		this.lblProg1.Name = "lblProg1";
		this.lblProg1.Size = new System.Drawing.Size(144, 20);
		this.lblProg1.TabIndex = 14;
		this.lblProg1.Text = "正在轉入基本資料";
		this.lblProg1.Visible = false;
		this.panel4.BackColor = System.Drawing.Color.White;
		this.panel4.Controls.Add(this.ultraLabel9);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel4.Location = new System.Drawing.Point(0, 0);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(516, 60);
		this.panel4.TabIndex = 13;
		appearance18.BackColor = System.Drawing.Color.White;
		this.ultraLabel9.Appearance = appearance18;
		this.ultraLabel9.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel9.Location = new System.Drawing.Point(16, 12);
		this.ultraLabel9.Name = "ultraLabel9";
		this.ultraLabel9.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel9.TabIndex = 2;
		this.ultraLabel9.Text = "資料匯入中...";
		this.Tab_D.Controls.Add(this.ultraLabel14);
		this.Tab_D.Controls.Add(this.ultraLabel13);
		this.Tab_D.Controls.Add(this.ultraLabel12);
		this.Tab_D.Controls.Add(this.panel6);
		this.Tab_D.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_D.Name = "Tab_D";
		this.Tab_D.Size = new System.Drawing.Size(516, 369);
		appearance19.BackColor = System.Drawing.Color.White;
		this.ultraLabel14.Appearance = appearance19;
		this.ultraLabel14.Location = new System.Drawing.Point(36, 116);
		this.ultraLabel14.Name = "ultraLabel14";
		this.ultraLabel14.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel14.TabIndex = 13;
		this.ultraLabel14.Text = "若要結束精靈，請按一下[完成]。";
		appearance20.BackColor = System.Drawing.Color.White;
		this.ultraLabel13.Appearance = appearance20;
		this.ultraLabel13.Location = new System.Drawing.Point(36, 64);
		this.ultraLabel13.Name = "ultraLabel13";
		this.ultraLabel13.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel13.TabIndex = 12;
		this.ultraLabel13.Text = "你已經成功匯入資料。";
		appearance21.BackColor = System.Drawing.Color.White;
		this.ultraLabel12.Appearance = appearance21;
		this.ultraLabel12.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel12.Location = new System.Drawing.Point(20, 20);
		this.ultraLabel12.Name = "ultraLabel12";
		this.ultraLabel12.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel12.TabIndex = 11;
		this.ultraLabel12.Text = "恭禧您!";
		this.panel6.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel6.Controls.Add(this.groupBox3);
		this.panel6.Controls.Add(this.D_Btn_Fnsh);
		this.panel6.Controls.Add(this.D_Btn_Prev);
		this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel6.Location = new System.Drawing.Point(0, 325);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(516, 44);
		this.panel6.TabIndex = 10;
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox3.Location = new System.Drawing.Point(0, 0);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(516, 8);
		this.groupBox3.TabIndex = 3;
		this.groupBox3.TabStop = false;
		appearance22.Image = resources.GetObject("appearance22.Image");
		appearance22.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Fnsh.Appearance = appearance22;
		this.D_Btn_Fnsh.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Fnsh.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.D_Btn_Fnsh.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.D_Btn_Fnsh.Font = new System.Drawing.Font("細明體", 11f);
		this.D_Btn_Fnsh.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Fnsh.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Fnsh.Location = new System.Drawing.Point(324, 9);
		this.D_Btn_Fnsh.Name = "D_Btn_Fnsh";
		this.D_Btn_Fnsh.ShowFocusRect = false;
		this.D_Btn_Fnsh.ShowOutline = false;
		this.D_Btn_Fnsh.Size = new System.Drawing.Size(88, 31);
		this.D_Btn_Fnsh.SupportThemes = false;
		this.D_Btn_Fnsh.TabIndex = 1;
		this.D_Btn_Fnsh.Text = "完成";
		appearance23.Image = resources.GetObject("appearance23.Image");
		appearance23.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Prev.Appearance = appearance23;
		this.D_Btn_Prev.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Prev.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.D_Btn_Prev.Font = new System.Drawing.Font("細明體", 11f);
		this.D_Btn_Prev.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Prev.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Prev.Location = new System.Drawing.Point(232, 9);
		this.D_Btn_Prev.Name = "D_Btn_Prev";
		this.D_Btn_Prev.ShowFocusRect = false;
		this.D_Btn_Prev.ShowOutline = false;
		this.D_Btn_Prev.Size = new System.Drawing.Size(88, 31);
		this.D_Btn_Prev.SupportThemes = false;
		this.D_Btn_Prev.TabIndex = 0;
		this.D_Btn_Prev.Text = "上一步";
		this.D_Btn_Prev.Visible = false;
		this.D_Btn_Prev.Click += new System.EventHandler(D_Btn_Prev_Click);
		this.TabCtrl.BackColor = System.Drawing.Color.White;
		this.TabCtrl.Controls.Add(this.ultraTabSharedControlsPage1);
		this.TabCtrl.Controls.Add(this.Tab_A);
		this.TabCtrl.Controls.Add(this.Tab_B);
		this.TabCtrl.Controls.Add(this.Tab_C);
		this.TabCtrl.Controls.Add(this.Tab_D);
		this.TabCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.TabCtrl.Location = new System.Drawing.Point(0, 0);
		this.TabCtrl.Name = "TabCtrl";
		this.TabCtrl.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.TabCtrl.Size = new System.Drawing.Size(516, 369);
		this.TabCtrl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
		this.TabCtrl.TabIndex = 1;
		ultraTab1.TabPage = this.Tab_A;
		ultraTab1.Text = "tab1";
		ultraTab2.TabPage = this.Tab_B;
		ultraTab2.Text = "tab2";
		ultraTab3.TabPage = this.Tab_C;
		ultraTab3.Text = "tab3";
		ultraTab4.TabPage = this.Tab_D;
		ultraTab4.Text = "tab4";
		this.TabCtrl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[4] { ultraTab1, ultraTab2, ultraTab3, ultraTab4 });
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(516, 369);
		this.timer1.Enabled = true;
		this.timer1.Interval = 1000;
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		base.CancelButton = this.A_Btn_Cncl;
		base.ClientSize = new System.Drawing.Size(516, 369);
		base.Controls.Add(this.TabCtrl);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.Name = "FormMrsCodeChange_ImpWizard";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "匯入";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormMrsBase_ImpWizard_KeyDown);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormMrsBase_ImpWizard_FormClosing);
		base.Load += new System.EventHandler(FormMrsBase_ImpWizard_Load);
		this.Tab_A.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		this.Tab_B.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtImpDirFile).EndInit();
		this.panel2.ResumeLayout(false);
		this.panel5.ResumeLayout(false);
		this.Tab_C.ResumeLayout(false);
		this.panel7.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
		this.Tab_D.ResumeLayout(false);
		this.panel6.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.TabCtrl).EndInit();
		this.TabCtrl.ResumeLayout(false);
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

	public FormMrsCodeChange_ImpWizard()
	{
		InitializeComponent();
	}

	private void ultraButton4_Click(object sender, EventArgs e)
	{
		string sFilter = "Microsoft Excel 97/2000 files (*.xls)|*.xls";
		openFileDialog1.Filter = sFilter;
		openFileDialog1.RestoreDirectory = true;
		if (openFileDialog1.ShowDialog() == DialogResult.OK)
		{
			txtImpDirFile.Text = openFileDialog1.FileName;
		}
	}

	private void A_Btn_Next_Click(object sender, EventArgs e)
	{
		Tab_B.Tab.Selected = true;
	}

	private void B_Btn_Next_Click(object sender, EventArgs e)
	{
		if (txtImpDirFile.Text.Trim() == "")
		{
			MessageBox.Show(this, "請先選定來源檔的目錄及檔名!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtImpDirFile.Focus();
			return;
		}
		if (!File.Exists(txtImpDirFile.Text.Trim()))
		{
			MessageBox.Show(this, "挑選的檔案不存在!", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtImpDirFile.Focus();
			return;
		}
		Tab_C.Tab.Selected = true;
		Application.DoEvents();
		Cursor = Cursors.WaitCursor;
		ExecuteImport();
		Cursor = Cursors.Default;
		Tab_D.Tab.Selected = true;
	}

	private void B_Btn_Prev_Click(object sender, EventArgs e)
	{
		Tab_A.Tab.Selected = true;
	}

	private void D_Btn_Prev_Click(object sender, EventArgs e)
	{
		Tab_B.Tab.Selected = true;
	}

	private void ExecuteImport()
	{
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("WinFORM 基本工料 匯入");
		Output_Com OUT_COM = new Output_Com(aArr);
		ArrayList TempArray = GetMrsBaseDS_FromExcelFile(txtImpDirFile.Text.Trim());
		DataSet DS_MrsBaseA = (DataSet)TempArray[0];
		DataSet DS_MrsBaseB = (DataSet)TempArray[1];
		if (RB2.Checked)
		{
			OUT_COM.InExcelChangeCode(DS_MrsBaseA, DS_MrsBaseB, CommonMethods.GetActionNameString(F_ActionName), F_ProjectCode, flag: true);
		}
		else if (RB1.Checked)
		{
			OUT_COM.InExcelChangeCode(DS_MrsBaseA, DS_MrsBaseB, CommonMethods.GetActionNameString(F_ActionName), F_ProjectCode, flag: false);
		}
	}

	private ArrayList GetMrsBaseDS_FromExcelFile(string sourceFile)
	{
		DataSet mrsXML = new DataSet();
		DataSet temp = new DataSet();
		temp.Tables.Add("基本工項");
		mrsXML.Tables.Add("基本工項");
		string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sourceFile + ";Extended Properties=Excel 8.0;Persist Security Info=False";
		string selectString1 = "SELECT * FROM [基本工項$]";
		OleDbConnection myConnection1 = new OleDbConnection(connectionString);
		try
		{
			myConnection1.Open();
			OleDbDataAdapter odAdpt1 = new OleDbDataAdapter();
			odAdpt1.SelectCommand = new OleDbCommand(selectString1, myConnection1);
			odAdpt1.Fill(temp.Tables["基本工項"]);
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormMrsBase_ImpWizard.cs" + ex.Message);
			string sErr = "使用的匯入檔案格式有誤\n請先確認你所使用的EXCEL第一個頁次名稱是基本工項\n第二個頁次名稱是分析工項";
			MessageBox.Show(this, sErr, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		finally
		{
			myConnection1.Close();
		}
		int iCount = 19;
		mrsXML.Tables["基本工項"].Columns.Add("NewCode");
		mrsXML.Tables["基本工項"].Columns.Add("OldCode");
		mrsXML.Tables["基本工項"].Columns.Add("cName");
		mrsXML.Tables["基本工項"].Columns.Add("eName");
		mrsXML.Tables["基本工項"].Columns.Add("cUnit");
		mrsXML.Tables["基本工項"].Columns.Add("eUnit");
		mrsXML.Tables["基本工項"].Columns.Add("Spec");
		mrsXML.Tables["基本工項"].Columns.Add("Analysis");
		mrsXML.Tables["基本工項"].Columns.Add("Cost");
		mrsXML.Tables["基本工項"].Columns.Add("eRate");
		mrsXML.Tables["基本工項"].Columns.Add("lRate");
		mrsXML.Tables["基本工項"].Columns.Add("mRate");
		mrsXML.Tables["基本工項"].Columns.Add("Memo");
		mrsXML.Tables["基本工項"].Columns.Add("UpdDT");
		mrsXML.Tables["基本工項"].Columns.Add("extendCode");
		mrsXML.Tables["基本工項"].Columns.Add("kind");
		mrsXML.Tables["基本工項"].Columns.Add("Rate");
		mrsXML.Tables["基本工項"].Columns.Add("changFlag");
		if (temp.Tables["基本工項"].Columns.IndexOf("別名") >= 0)
		{
			mrsXML.Tables["基本工項"].Columns.Add("surName");
		}
		else
		{
			iCount = 18;
		}
		DataRow myRow = mrsXML.Tables["基本工項"].NewRow();
		myRow[0] = "基本工項新碼";
		myRow[1] = "基本工項代碼";
		myRow[2] = "工項中文名稱";
		myRow[3] = "工項英文名稱";
		myRow[4] = "單位";
		myRow[5] = "英文單位";
		myRow[6] = "規格";
		myRow[7] = "單價分析";
		myRow[8] = "單價";
		myRow[9] = "機具百分率";
		myRow[10] = "人工百分率";
		myRow[11] = "材料百分率";
		myRow[12] = "備註欄";
		myRow[13] = "登錄時間";
		myRow[14] = "工項外碼";
		myRow[15] = "項目種類";
		myRow[16] = "百分比";
		myRow[17] = "單價變動旗標";
		if (temp.Tables["基本工項"].Columns.IndexOf("別名") >= 0)
		{
			myRow[18] = "別名";
		}
		mrsXML.Tables["基本工項"].Rows.Add(myRow);
		for (int i = 0; i < temp.Tables["基本工項"].Rows.Count; i++)
		{
			DataRow entryRow = mrsXML.Tables["基本工項"].NewRow();
			for (int j = 0; j < iCount; j++)
			{
				if (j == 9 || j == 10 || j == 11)
				{
					entryRow[j] = PubTools.Str2Decimal(temp.Tables["基本工項"].Rows[i][myRow[j].ToString()]) * 100m;
				}
				else
				{
					entryRow[j] = temp.Tables["基本工項"].Rows[i][myRow[j].ToString()];
				}
			}
			mrsXML.Tables["基本工項"].Rows.Add(entryRow);
		}
		DataSet anaXML = new DataSet();
		temp.Tables.Add("分析工項");
		anaXML.Tables.Add("分析工項");
		string selectString2 = "SELECT * FROM [分析工項$]";
		OleDbConnection myConnection2 = new OleDbConnection(connectionString);
		try
		{
			myConnection2.Open();
			OleDbDataAdapter odAdpt2 = new OleDbDataAdapter();
			odAdpt2.SelectCommand = new OleDbCommand(selectString2, myConnection2);
			odAdpt2.Fill(temp.Tables["分析工項"]);
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "MrsBase.FormMrsBase_ImpWizard.cs" + ex.Message);
			string sErr = "使用的匯入檔案格式有誤\n請先確認你所使用的EXCEL第一個頁次名稱是基本工項\n第二個頁次名稱是分析工項";
			MessageBox.Show(this, sErr, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		finally
		{
			myConnection2.Close();
		}
		anaXML.Tables["分析工項"].Columns.Add("ParentName");
		anaXML.Tables["分析工項"].Columns.Add("ChildName");
		anaXML.Tables["分析工項"].Columns.Add("ParentCode");
		anaXML.Tables["分析工項"].Columns.Add("ChildParentName");
		anaXML.Tables["分析工項"].Columns.Add("MinResQty");
		anaXML.Tables["分析工項"].Columns.Add("Qty");
		anaXML.Tables["分析工項"].Columns.Add("ManResQty");
		anaXML.Tables["分析工項"].Columns.Add("Cost");
		anaXML.Tables["分析工項"].Columns.Add("Amount");
		anaXML.Tables["分析工項"].Columns.Add("Memo");
		anaXML.Tables["分析工項"].Columns.Add("ListNo");
		anaXML.Tables["分析工項"].Columns.Add("TmpListNo");
		anaXML.Tables["分析工項"].Columns.Add("Rate");
		DataRow anaRow = anaXML.Tables["分析工項"].NewRow();
		anaRow[0] = "基本工項名稱";
		anaRow[1] = "分析工項名稱";
		anaRow[2] = "基本工項代碼";
		anaRow[3] = "分析工項代碼";
		anaRow[4] = "最低資源數量";
		anaRow[5] = "數量";
		anaRow[6] = "最高資源數量";
		anaRow[7] = "分析工項單價";
		anaRow[8] = "分析工項複價";
		anaRow[9] = "備註";
		anaRow[10] = "分析順序";
		anaRow[11] = "分析順序暫存欄";
		anaRow[12] = "百分比";
		anaXML.Tables["分析工項"].Rows.Add(anaRow);
		for (int k = 0; k < temp.Tables["分析工項"].Rows.Count; k++)
		{
			DataRow insertRow = anaXML.Tables["分析工項"].NewRow();
			for (int l = 0; l < temp.Tables["分析工項"].Columns.Count; l++)
			{
				if (l <= 12)
				{
					insertRow[l] = temp.Tables["分析工項"].Rows[k][l];
				}
			}
			anaXML.Tables["分析工項"].Rows.Add(insertRow);
		}
		ArrayList DS_Array = new ArrayList();
		DS_Array.Add(mrsXML);
		DS_Array.Add(anaXML);
		return DS_Array;
	}

	private void FormMrsBase_ImpWizard_Load(object sender, EventArgs e)
	{
		if (F_ImportType == ImportType.XML)
		{
			B_Btn_Prev.Visible = false;
			Tab_B.Tab.Selected = true;
		}
		if (F_ActionName != PccesFormAction.MrsBase)
		{
			lblWanring.Visible = true;
		}
		LoadingScreen();
	}

	private void LoadingScreen()
	{
		string Status = CommonMethods.GetIniValue("MrsBase_Imp", "WindowState");
		if (Status == "Maximized")
		{
			base.WindowState = FormWindowState.Maximized;
		}
		int iLoc_X = PubTools.Str2Int(CommonMethods.GetIniValue("MrsBase_Imp", "PK_LocationX"));
		int iLoc_Y = PubTools.Str2Int(CommonMethods.GetIniValue("MrsBase_Imp", "PK_LocationY"));
		int iSiz_W = PubTools.Str2Int(CommonMethods.GetIniValue("MrsBase_Imp", "PK_Width"));
		int iSiz_H = PubTools.Str2Int(CommonMethods.GetIniValue("MrsBase_Imp", "PK_Height"));
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

	private void FormMrsBase_ImpWizard_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (base.WindowState != FormWindowState.Maximized)
		{
			CommonMethods.WriteIniValue("MrsBase_Imp", "PK_LocationX", base.Location.X.ToString());
			CommonMethods.WriteIniValue("MrsBase_Imp", "PK_LocationY", base.Location.Y.ToString());
			CommonMethods.WriteIniValue("MrsBase_Imp", "PK_Width", base.Size.Width.ToString());
			CommonMethods.WriteIniValue("MrsBase_Imp", "PK_Height", base.Size.Height.ToString());
		}
		CommonMethods.WriteIniValue("MrsBase_Imp", "WindowState", base.WindowState.ToString());
	}

	private void FormMrsBase_ImpWizard_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormMrsBase_ImpWizard");
		}
	}
}
