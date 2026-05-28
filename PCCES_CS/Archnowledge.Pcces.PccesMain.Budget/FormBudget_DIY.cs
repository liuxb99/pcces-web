using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.STDClass;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinProgressBar;
using Infragistics.Win.UltraWinTabControl;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormBudget_DIY : Form
{
	private const string CallFormHelp = "FormBudget_DIY";

	private UltraTabControl Tab_Ctrl;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl Tab_A;

	private UltraTabPageControl Tab_B;

	public Panel panel1;

	private GroupBox groupBox1;

	private UltraButton A_Btn_Cncl;

	private UltraButton A_Btn_Next;

	private UltraLabel ultraLabel1;

	private UltraTextEditor txtFileName;

	private UltraButton BtnChgDir;

	private UltraLabel lblWait;

	private Panel panel6;

	private GroupBox groupBox3;

	private UltraButton C_Btn_Cncl;

	private UltraButton C_Btn_Next;

	private UltraButton C_Btn_Prev;

	private Panel panel8;

	private GroupBox groupBox4;

	private UltraButton D_Btn_Fnsh;

	private UltraButton D_Btn_Prev;

	private UltraLabel ultraLabel14;

	private UltraLabel ultraLabel13;

	private UltraLabel ultraLabel12;

	private UltraLabel ultraLabel2;

	private OpenFileDialog openFileDialog1;

	private UltraProgressBar Prog1;

	private UltraTabPageControl Tab_C;

	private Container components = null;

	private string F_UserID;

	private string F_ProjectCode;

	private PccesFormAction F_ActionName;

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

	public FormBudget_DIY()
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormBudget_DIY));
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		this.Tab_A = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.BtnChgDir = new Infragistics.Win.Misc.UltraButton();
		this.txtFileName = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.panel1 = new System.Windows.Forms.Panel();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.A_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.A_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.Tab_B = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.Prog1 = new Infragistics.Win.UltraWinProgressBar.UltraProgressBar();
		this.panel6 = new System.Windows.Forms.Panel();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.C_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.C_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.C_Btn_Prev = new Infragistics.Win.Misc.UltraButton();
		this.lblWait = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_C = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.ultraLabel14 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel13 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel12 = new Infragistics.Win.Misc.UltraLabel();
		this.panel8 = new System.Windows.Forms.Panel();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.D_Btn_Fnsh = new Infragistics.Win.Misc.UltraButton();
		this.D_Btn_Prev = new Infragistics.Win.Misc.UltraButton();
		this.Tab_Ctrl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
		this.Tab_A.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtFileName).BeginInit();
		this.panel1.SuspendLayout();
		this.Tab_B.SuspendLayout();
		this.panel6.SuspendLayout();
		this.Tab_C.SuspendLayout();
		this.panel8.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).BeginInit();
		this.Tab_Ctrl.SuspendLayout();
		base.SuspendLayout();
		this.Tab_A.Controls.Add(this.ultraLabel2);
		this.Tab_A.Controls.Add(this.BtnChgDir);
		this.Tab_A.Controls.Add(this.txtFileName);
		this.Tab_A.Controls.Add(this.ultraLabel1);
		this.Tab_A.Controls.Add(this.panel1);
		this.Tab_A.Location = new System.Drawing.Point(0, 0);
		this.Tab_A.Name = "Tab_A";
		this.Tab_A.Size = new System.Drawing.Size(576, 407);
		appearance1.BackColor = System.Drawing.Color.White;
		this.ultraLabel2.Appearance = appearance1;
		this.ultraLabel2.Location = new System.Drawing.Point(20, 28);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(544, 20);
		this.ultraLabel2.TabIndex = 14;
		this.ultraLabel2.Text = "歡迎使用預算書 Excel 轉入精靈，接下來我們將引導您一步一步輸入資料";
		appearance2.FontData.Name = "Arial";
		appearance2.FontData.SizeInPoints = 8f;
		this.BtnChgDir.Appearance = appearance2;
		this.BtnChgDir.BackColor = System.Drawing.SystemColors.Control;
		this.BtnChgDir.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.BtnChgDir.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnChgDir.Location = new System.Drawing.Point(503, 235);
		this.BtnChgDir.Name = "BtnChgDir";
		this.BtnChgDir.ShowFocusRect = false;
		this.BtnChgDir.ShowOutline = false;
		this.BtnChgDir.Size = new System.Drawing.Size(48, 24);
		this.BtnChgDir.SupportThemes = false;
		this.BtnChgDir.TabIndex = 13;
		this.BtnChgDir.Text = "瀏覽...";
		this.BtnChgDir.Click += new System.EventHandler(BtnChgDir_Click);
		this.txtFileName.AutoSize = true;
		this.txtFileName.Location = new System.Drawing.Point(20, 236);
		this.txtFileName.Name = "txtFileName";
		this.txtFileName.Size = new System.Drawing.Size(484, 24);
		this.txtFileName.TabIndex = 12;
		this.ultraLabel1.Location = new System.Drawing.Point(20, 200);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(512, 23);
		this.ultraLabel1.TabIndex = 11;
		this.ultraLabel1.Text = "請挑選預算書 Excel 檔";
		this.panel1.AutoSize = true;
		this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.Controls.Add(this.groupBox1);
		this.panel1.Controls.Add(this.A_Btn_Cncl);
		this.panel1.Controls.Add(this.A_Btn_Next);
		this.panel1.Location = new System.Drawing.Point(0, 363);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(576, 43);
		this.panel1.TabIndex = 10;
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(576, 8);
		this.groupBox1.TabIndex = 3;
		this.groupBox1.TabStop = false;
		this.A_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance3.Image = resources.GetObject("appearance3.Image");
		appearance3.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Cncl.Appearance = appearance3;
		this.A_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.A_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_Cncl.Location = new System.Drawing.Point(480, 9);
		this.A_Btn_Cncl.Name = "A_Btn_Cncl";
		this.A_Btn_Cncl.ShowFocusRect = false;
		this.A_Btn_Cncl.ShowOutline = false;
		this.A_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.A_Btn_Cncl.SupportThemes = false;
		this.A_Btn_Cncl.TabIndex = 2;
		this.A_Btn_Cncl.Text = "取消";
		this.A_Btn_Next.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance4.Image = resources.GetObject("appearance4.Image");
		appearance4.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Next.Appearance = appearance4;
		this.A_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Next.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.A_Btn_Next.Location = new System.Drawing.Point(388, 9);
		this.A_Btn_Next.Name = "A_Btn_Next";
		this.A_Btn_Next.ShowFocusRect = false;
		this.A_Btn_Next.ShowOutline = false;
		this.A_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.A_Btn_Next.SupportThemes = false;
		this.A_Btn_Next.TabIndex = 1;
		this.A_Btn_Next.Text = "執行";
		this.A_Btn_Next.Click += new System.EventHandler(A_Btn_Next_Click);
		this.Tab_B.Controls.Add(this.Prog1);
		this.Tab_B.Controls.Add(this.panel6);
		this.Tab_B.Controls.Add(this.lblWait);
		this.Tab_B.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_B.Name = "Tab_B";
		this.Tab_B.Size = new System.Drawing.Size(576, 407);
		this.Prog1.Location = new System.Drawing.Point(20, 243);
		this.Prog1.Name = "Prog1";
		this.Prog1.Size = new System.Drawing.Size(536, 23);
		this.Prog1.SupportThemes = false;
		this.Prog1.TabIndex = 21;
		this.Prog1.Text = "[Formatted]";
		this.panel6.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel6.Controls.Add(this.groupBox3);
		this.panel6.Controls.Add(this.C_Btn_Cncl);
		this.panel6.Controls.Add(this.C_Btn_Next);
		this.panel6.Controls.Add(this.C_Btn_Prev);
		this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel6.Location = new System.Drawing.Point(0, 363);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(576, 44);
		this.panel6.TabIndex = 20;
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox3.Location = new System.Drawing.Point(0, 0);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(576, 8);
		this.groupBox3.TabIndex = 3;
		this.groupBox3.TabStop = false;
		appearance5.Image = resources.GetObject("appearance5.Image");
		appearance5.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.C_Btn_Cncl.Appearance = appearance5;
		this.C_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.C_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.C_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.C_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.C_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.C_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.C_Btn_Cncl.Location = new System.Drawing.Point(480, 9);
		this.C_Btn_Cncl.Name = "C_Btn_Cncl";
		this.C_Btn_Cncl.ShowFocusRect = false;
		this.C_Btn_Cncl.ShowOutline = false;
		this.C_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.C_Btn_Cncl.SupportThemes = false;
		this.C_Btn_Cncl.TabIndex = 2;
		this.C_Btn_Cncl.Text = "取消";
		this.C_Btn_Cncl.Visible = false;
		this.C_Btn_Cncl.Click += new System.EventHandler(C_Btn_Cncl_Click);
		appearance6.Image = resources.GetObject("appearance6.Image");
		appearance6.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance6.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.C_Btn_Next.Appearance = appearance6;
		this.C_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.C_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.C_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.C_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.C_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.C_Btn_Next.Location = new System.Drawing.Point(388, 9);
		this.C_Btn_Next.Name = "C_Btn_Next";
		this.C_Btn_Next.ShowFocusRect = false;
		this.C_Btn_Next.ShowOutline = false;
		this.C_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.C_Btn_Next.SupportThemes = false;
		this.C_Btn_Next.TabIndex = 1;
		this.C_Btn_Next.Text = "下一步";
		this.C_Btn_Next.Visible = false;
		appearance7.Image = resources.GetObject("appearance7.Image");
		appearance7.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.C_Btn_Prev.Appearance = appearance7;
		this.C_Btn_Prev.BackColor = System.Drawing.SystemColors.Control;
		this.C_Btn_Prev.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.C_Btn_Prev.Font = new System.Drawing.Font("細明體", 11f);
		this.C_Btn_Prev.ImageSize = new System.Drawing.Size(20, 20);
		this.C_Btn_Prev.ImageTransparentColor = System.Drawing.Color.White;
		this.C_Btn_Prev.Location = new System.Drawing.Point(296, 9);
		this.C_Btn_Prev.Name = "C_Btn_Prev";
		this.C_Btn_Prev.ShowFocusRect = false;
		this.C_Btn_Prev.ShowOutline = false;
		this.C_Btn_Prev.Size = new System.Drawing.Size(88, 31);
		this.C_Btn_Prev.SupportThemes = false;
		this.C_Btn_Prev.TabIndex = 0;
		this.C_Btn_Prev.Text = "上一步";
		this.C_Btn_Prev.Visible = false;
		this.lblWait.Location = new System.Drawing.Point(20, 28);
		this.lblWait.Name = "lblWait";
		this.lblWait.Size = new System.Drawing.Size(476, 20);
		this.lblWait.SupportThemes = false;
		this.lblWait.TabIndex = 19;
		this.lblWait.Text = "正在準備轉入的資料，這個動作會花些時間，請稍候。";
		this.Tab_C.Controls.Add(this.ultraLabel14);
		this.Tab_C.Controls.Add(this.ultraLabel13);
		this.Tab_C.Controls.Add(this.ultraLabel12);
		this.Tab_C.Controls.Add(this.panel8);
		this.Tab_C.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_C.Name = "Tab_C";
		this.Tab_C.Size = new System.Drawing.Size(576, 407);
		appearance8.BackColor = System.Drawing.Color.White;
		this.ultraLabel14.Appearance = appearance8;
		this.ultraLabel14.Location = new System.Drawing.Point(56, 93);
		this.ultraLabel14.Name = "ultraLabel14";
		this.ultraLabel14.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel14.TabIndex = 21;
		this.ultraLabel14.Text = "若要結束精靈，請按一下[完成]。";
		appearance9.BackColor = System.Drawing.Color.White;
		this.ultraLabel13.Appearance = appearance9;
		this.ultraLabel13.Location = new System.Drawing.Point(56, 60);
		this.ultraLabel13.Name = "ultraLabel13";
		this.ultraLabel13.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel13.TabIndex = 20;
		this.ultraLabel13.Text = "你已經成功匯入資料。";
		appearance10.BackColor = System.Drawing.Color.White;
		this.ultraLabel12.Appearance = appearance10;
		this.ultraLabel12.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel12.Location = new System.Drawing.Point(24, 28);
		this.ultraLabel12.Name = "ultraLabel12";
		this.ultraLabel12.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel12.TabIndex = 19;
		this.ultraLabel12.Text = "恭禧您!";
		this.panel8.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel8.Controls.Add(this.groupBox4);
		this.panel8.Controls.Add(this.D_Btn_Fnsh);
		this.panel8.Controls.Add(this.D_Btn_Prev);
		this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel8.Location = new System.Drawing.Point(0, 363);
		this.panel8.Name = "panel8";
		this.panel8.Size = new System.Drawing.Size(576, 44);
		this.panel8.TabIndex = 18;
		this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox4.Location = new System.Drawing.Point(0, 0);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(576, 8);
		this.groupBox4.TabIndex = 3;
		this.groupBox4.TabStop = false;
		appearance11.Image = resources.GetObject("appearance11.Image");
		appearance11.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Fnsh.Appearance = appearance11;
		this.D_Btn_Fnsh.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Fnsh.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.D_Btn_Fnsh.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.D_Btn_Fnsh.Font = new System.Drawing.Font("細明體", 11f);
		this.D_Btn_Fnsh.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Fnsh.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Fnsh.Location = new System.Drawing.Point(388, 9);
		this.D_Btn_Fnsh.Name = "D_Btn_Fnsh";
		this.D_Btn_Fnsh.ShowFocusRect = false;
		this.D_Btn_Fnsh.ShowOutline = false;
		this.D_Btn_Fnsh.Size = new System.Drawing.Size(88, 31);
		this.D_Btn_Fnsh.SupportThemes = false;
		this.D_Btn_Fnsh.TabIndex = 1;
		this.D_Btn_Fnsh.Text = "完成";
		appearance12.Image = resources.GetObject("appearance12.Image");
		appearance12.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Prev.Appearance = appearance12;
		this.D_Btn_Prev.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Prev.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.D_Btn_Prev.Font = new System.Drawing.Font("細明體", 11f);
		this.D_Btn_Prev.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Prev.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Prev.Location = new System.Drawing.Point(296, 9);
		this.D_Btn_Prev.Name = "D_Btn_Prev";
		this.D_Btn_Prev.ShowFocusRect = false;
		this.D_Btn_Prev.ShowOutline = false;
		this.D_Btn_Prev.Size = new System.Drawing.Size(88, 31);
		this.D_Btn_Prev.SupportThemes = false;
		this.D_Btn_Prev.TabIndex = 0;
		this.D_Btn_Prev.Text = "上一步";
		this.D_Btn_Prev.Click += new System.EventHandler(D_Btn_Prev_Click);
		this.Tab_Ctrl.BackColor = System.Drawing.Color.White;
		this.Tab_Ctrl.Controls.Add(this.ultraTabSharedControlsPage1);
		this.Tab_Ctrl.Controls.Add(this.Tab_A);
		this.Tab_Ctrl.Controls.Add(this.Tab_B);
		this.Tab_Ctrl.Controls.Add(this.Tab_C);
		this.Tab_Ctrl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Tab_Ctrl.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.Tab_Ctrl.Location = new System.Drawing.Point(0, 0);
		this.Tab_Ctrl.Name = "Tab_Ctrl";
		this.Tab_Ctrl.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.Tab_Ctrl.Size = new System.Drawing.Size(576, 407);
		this.Tab_Ctrl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
		this.Tab_Ctrl.TabIndex = 0;
		ultraTab1.TabPage = this.Tab_A;
		ultraTab1.Text = "tab1";
		ultraTab2.TabPage = this.Tab_B;
		ultraTab2.Text = "tab2";
		ultraTab3.TabPage = this.Tab_C;
		ultraTab3.Text = "tab3";
		this.Tab_Ctrl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[3] { ultraTab1, ultraTab2, ultraTab3 });
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(576, 407);
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.CancelButton = this.A_Btn_Cncl;
		base.ClientSize = new System.Drawing.Size(576, 407);
		base.Controls.Add(this.Tab_Ctrl);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormBudget_DIY";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "轉入數量計算格式檔";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormBudget_DIY_KeyDown);
		this.Tab_A.ResumeLayout(false);
		this.Tab_A.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtFileName).EndInit();
		this.panel1.ResumeLayout(false);
		this.Tab_B.ResumeLayout(false);
		this.panel6.ResumeLayout(false);
		this.Tab_C.ResumeLayout(false);
		this.panel8.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).EndInit();
		this.Tab_Ctrl.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	private void BtnChgDir_Click(object sender, EventArgs e)
	{
		string sFilter = "DIY 格式(*.xls)|*.xls";
		openFileDialog1.Filter = sFilter;
		openFileDialog1.RestoreDirectory = true;
		if (openFileDialog1.ShowDialog() == DialogResult.OK)
		{
			txtFileName.Text = openFileDialog1.FileName;
		}
	}

	private void A_Btn_Next_Click(object sender, EventArgs e)
	{
		Tab_B.Tab.Selected = true;
		Application.DoEvents();
		if (Do_Import_DIY())
		{
			Tab_C.Tab.Selected = true;
		}
		else
		{
			Tab_A.Tab.Selected = true;
		}
	}

	private void D_Btn_Prev_Click(object sender, EventArgs e)
	{
		Tab_A.Tab.Selected = true;
	}

	private bool Do_Import_DIY()
	{
		bool RetV = true;
		OleDbConnection oCon = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtFileName.Text.Trim() + ";Extended Properties=Excel 8.0;Persist Security Info=False");
		string SQLStr = "select *,(select count(項次代碼) from [Sheet1$] WHERE 項次代碼 = A.項次代碼 group by 項次代碼) AS ACOUNT from [Sheet1$] A order by 項次代碼";
		OleDbDataAdapter oDA = new OleDbDataAdapter(SQLStr, oCon);
		DataTable InputDt = new DataTable();
		try
		{
			oDA.Fill(InputDt);
			RetV = true;
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudget_DIY.cs" + ex.Message);
			Console.Write(ex.Message);
			string sWarning = "轉入來源的檔案格式不正確，請重新挑選!";
			MessageBox.Show(this, sWarning, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			Tab_A.Tab.Selected = true;
			return false;
		}
		if (InputDt.Columns.IndexOf("項次") < 0)
		{
			MessageBox.Show(this, "轉入來源的檔案格式不正確!無【項次】欄位!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
		if (InputDt.Columns.IndexOf("項目") < 0)
		{
			MessageBox.Show(this, "轉入來源的檔案格式不正確!無【項目】欄位!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
		if (InputDt.Columns.IndexOf("單位") < 0)
		{
			MessageBox.Show(this, "轉入來源的檔案格式不正確!無【單位】欄位!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
		if (InputDt.Columns.IndexOf("數量") < 0)
		{
			MessageBox.Show(this, "轉入來源的檔案格式不正確!無【數量】欄位!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
		if (InputDt.Columns.IndexOf("單價") < 0)
		{
			MessageBox.Show(this, "轉入來源的檔案格式不正確!無【單價】欄位!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
		if (InputDt.Columns.IndexOf("複價") < 0)
		{
			MessageBox.Show(this, "轉入來源的檔案格式不正確!無【複價】欄位!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
		if (InputDt.Columns.IndexOf("備註") < 0)
		{
			MessageBox.Show(this, "轉入來源的檔案格式不正確!無【備註】欄位!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
		if (InputDt.Columns.IndexOf("種類") < 0)
		{
			MessageBox.Show(this, "轉入來源的檔案格式不正確!無【種類】欄位!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
		if (InputDt.Columns.IndexOf("項次代碼") < 0)
		{
			MessageBox.Show(this, "轉入來源的檔案格式不正確!無【項次代碼】欄位!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
		if (InputDt.Columns.IndexOf("百分比") < 0)
		{
			MessageBox.Show(this, "轉入來源的檔案格式不正確!無【百分比】欄位!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
		bool lb_kind = false;
		bool lb_PrintNo = false;
		bool lb_Data = false;
		foreach (DataRow dr in InputDt.Rows)
		{
			string ls_kind = dr["種類"].ToString().Trim().ToUpper();
			if (ls_kind.Length == 0 || "BFLSZW".IndexOf(ls_kind) == -1)
			{
				lb_kind = true;
			}
			string ls_pintno = dr["項次代碼"].ToString().Trim();
			if (ls_pintno.Length == 0 || dr["ACOUNT"].ToString() != "1")
			{
				lb_PrintNo = true;
			}
			string ls_Data = dr["項目"].ToString().Trim() + dr["單位"].ToString().Trim();
			if (ls_Data.Length == 0)
			{
				lb_Data = true;
			}
		}
		if (lb_kind)
		{
			MessageBox.Show(this, "轉入來源的資料不正確!【種類】欄位資料有誤或未輸入!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
		if (lb_PrintNo)
		{
			MessageBox.Show(this, "轉入來源的資料不正確!【項次代碼】欄位資料有誤或未輸入!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
		if (lb_Data)
		{
			MessageBox.Show(this, "轉入來源的資料不正確!【項目】欄位資料有誤或未輸入!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
		InputDt.Columns.Add("PccesCode", Type.GetType("System.String"));
		InputDt.Columns.Add("PubCode", Type.GetType("System.Int64"));
		InputDt.Columns.Add("AddThis", Type.GetType("System.String"));
		string IPStr = CommonMethods.GetIPAddress();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("預算書EXCEL轉入--讀取基本工料" + F_ProjectCode + "(" + IPStr + ")");
		MrsBaseA MrsACom = new MrsBaseA(F_UserID, aArr);
		MrsACom.ps_srckind = "MRS";
		DataTable MrsDT = MrsACom.ListItem("");
		MrsDT.CaseSensitive = true;
		DataView MrsDV = MrsDT.DefaultView;
		MrsDV.Sort = "PccesCode";
		int iFlag = 0;
		string ls_PccesCode = "Z" + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');
		MrsDV.RowFilter = "substring(pccescode,1,5) = '" + ls_PccesCode + "'";
		if (MrsDV.Count > 0)
		{
			iFlag = PubTools.Str2Int(MrsDV[MrsDV.Count - 1]["pccescode"].ToString().Substring(5));
		}
		ItemA ItemACom = new ItemA(aArr);
		ItemACom.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		ItemACom.ps_projectCode = F_ProjectCode;
		int li_sNo = ItemACom.getMaxNo(F_ProjectCode);
		Prog1.Maximum = InputDt.Rows.Count;
		Prog1.Minimum = 0;
		foreach (DataRow dr in InputDt.Rows)
		{
			Prog1.Value++;
			Application.DoEvents();
			string ls_kind = dr["種類"].ToString().Trim().ToUpper();
			if (ls_kind == "W")
			{
				string ls_cName = dr["項目"].ToString().Trim();
				string ls_cUnit = dr["單位"].ToString().Trim();
				MrsDV.RowFilter = "cName='" + ls_cName + "' and UnitName='" + ls_cUnit + "'";
				if (MrsDV.Count == 0)
				{
					MrsACom.ps_srckind = "MRS";
					MrsACom.ps_projectcode = null;
					string ls_nCode = ls_PccesCode + (iFlag + 1).ToString().PadLeft(5, '0');
					DataRow ndr = MrsDT.NewRow();
					MrsACom.ps_pccesCode = ls_nCode;
					ndr["PccesCode"] = ls_nCode;
					MrsACom.ps_cName = ls_cName;
					ndr["cName"] = ls_cName;
					MrsACom.ps_unitName = ls_cUnit;
					ndr["UnitName"] = ls_cUnit;
					MrsACom.ps_cost = dr["單價"].ToString().Replace(",", "");
					ndr["cost"] = PubTools.Str2Double(dr["單價"].ToString().Replace(",", ""));
					try
					{
						MrsACom.ps_eName = dr["英文名稱"].ToString();
						ndr["eName"] = dr["英文名稱"].ToString();
					}
					catch (Exception ex)
					{
						CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudget_DIY.cs" + ex.Message);
						MrsACom.ps_eName = null;
					}
					try
					{
						MrsACom.ps_eUnit = dr["英文單位"].ToString();
						ndr["eUnit"] = dr["英文單位"].ToString();
					}
					catch (Exception ex)
					{
						CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudget_DIY.cs" + ex.Message);
						MrsACom.ps_eUnit = null;
					}
					MrsACom.ps_analysis = "0";
					ndr["analysis"] = "0";
					MrsACom.ps_costKind = "";
					ndr["costKind"] = "";
					MrsACom.ps_rate = "0";
					ndr["rate"] = 0;
					string ls_memo = dr["備註"].ToString();
					if (ls_memo.Length > 0)
					{
						if (ls_memo.Substring(0, 1) != "#")
						{
							ls_memo = "#," + ls_memo;
						}
					}
					else
					{
						ls_memo = "#" + ls_memo;
					}
					MrsACom.ps_memo = ls_memo;
					ndr["memo"] = ls_memo;
					MrsACom.InseItem();
					MrsACom.SetPost(ls_nCode, "0");
					int li_npubcode = MrsACom.Get_Pubcode(ls_nCode);
					iFlag++;
					ndr["PubCode"] = li_npubcode;
					MrsDT.Rows.Add(ndr);
					MrsDV.RowFilter = "cName='" + ls_cName + "' and UnitName='" + ls_cUnit + "'";
				}
				MrsACom.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
				MrsACom.ps_projectcode = F_ProjectCode;
				MrsACom.ps_pubCode = MrsDV[0]["pubCode"].ToString();
				MrsACom.ps_pccesCode = MrsDV[0]["pccesCode"].ToString();
				MrsACom.ps_cName = MrsDV[0]["cName"].ToString();
				MrsACom.ps_unitName = MrsDV[0]["unitName"].ToString();
				MrsACom.ps_cost = MrsDV[0]["cost"].ToString();
				MrsACom.ps_eName = MrsDV[0]["eName"].ToString();
				MrsACom.ps_eUnit = MrsDV[0]["eUnit"].ToString();
				MrsACom.ps_analysis = MrsDV[0]["analysis"].ToString();
				MrsACom.ps_costKind = MrsDV[0]["costKind"].ToString();
				MrsACom.ps_rate = MrsDV[0]["rate"].ToString();
				MrsACom.ps_memo = MrsDV[0]["memo"].ToString();
				MrsACom.ps_xNameC = MrsDV[0]["xNameC"].ToString();
				MrsACom.ps_accountCode1 = MrsDV[0]["accountCode1"].ToString();
				MrsACom.ps_accountCode2 = MrsDV[0]["accountCode2"].ToString();
				MrsACom.ps_analysisQty = MrsDV[0]["analysisQty"].ToString();
				MrsACom.ps_eRate = MrsDV[0]["eRate"].ToString();
				MrsACom.ps_extendCode = MrsDV[0]["extendCode"].ToString();
				MrsACom.ps_lRate = MrsDV[0]["lRate"].ToString();
				MrsACom.ps_mRate = MrsDV[0]["mRate"].ToString();
				MrsACom.ps_wRate = MrsDV[0]["wRate"].ToString();
				MrsACom.ps_xNameE = MrsDV[0]["xNameE"].ToString();
				MrsACom.ps_resType = MrsDV[0]["resType"].ToString();
				MrsACom.ps_resCode = MrsDV[0]["resCode"].ToString();
				MrsACom.InseItem();
				ItemACom.ps_pubCode = MrsDV[0]["pubCode"].ToString();
				ItemACom.ps_eRate = MrsDV[0]["eRate"].ToString();
				ItemACom.ps_lRate = MrsDV[0]["lRate"].ToString();
				ItemACom.ps_mRate = MrsDV[0]["mRate"].ToString();
				ItemACom.ps_wRate = MrsDV[0]["wRate"].ToString();
			}
			else
			{
				ItemACom.ps_pubCode = "0";
				ItemACom.ps_eRate = null;
				ItemACom.ps_lRate = null;
				ItemACom.ps_mRate = null;
				ItemACom.ps_wRate = null;
			}
			ItemACom.ps_amount = dr["複價"].ToString();
			ItemACom.ps_cName = dr["項目"].ToString();
			ItemACom.ps_cost = dr["單價"].ToString();
			try
			{
				ItemACom.ps_eName = dr["英文名稱"].ToString();
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudget_DIY.cs" + ex.Message);
				ItemACom.ps_eName = null;
			}
			try
			{
				ItemACom.ps_eUnit = dr["英文單位"].ToString();
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudget_DIY.cs" + ex.Message);
				ItemACom.ps_eUnit = null;
			}
			ItemACom.ps_itemNo = dr["項次"].ToString();
			ItemACom.ps_kind = dr["種類"].ToString();
			ItemACom.ps_levelNo = ((ItemACom.ps_printNo = dr["項次代碼"].ToString().Trim()).Length / 4).ToString();
			ItemACom.ps_memo = dr["備註"].ToString();
			ItemACom.ps_qty = dr["數量"].ToString();
			ItemACom.ps_rate = dr["百分比"].ToString();
			ItemACom.ps_sNo = (li_sNo + 1).ToString();
			ItemACom.ps_unitName = dr["單位"].ToString();
			ItemACom.InseItem();
			li_sNo++;
		}
		MrsACom = null;
		MrsBaseB mrscom = new MrsBaseB(aArr);
		mrscom.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		mrscom.ReAnalysis(F_ProjectCode);
		mrscom = null;
		DataTable dt = ItemACom.ListItem("", F_ProjectCode);
		ItemACom = null;
		PubTools.WriteRoughlyLog(aArr);
		return RetV;
	}

	private void C_Btn_Cncl_Click(object sender, EventArgs e)
	{
	}

	private void FormBudget_DIY_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormBudget_DIY");
		}
	}
}
