using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.PccesMain.ShellLib;
using Archnowledge.Pcces.STDClass;
using Archnowledge.Pcces.XMLClass;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTabControl;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormBudgetCheckOut_Wzd : Form
{
	private UltraTabControl Tab_Ctrl;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private GroupBox groupBox5;

	private UltraButton A1_Btn_Cncl;

	private UltraButton A1_Btn_Next;

	private UltraLabel ultraLabel3;

	private UltraTextEditor tbBackupPath;

	private UltraLabel ultraLabel8;

	private UltraButton btnPickBackupFolder;

	private UltraTextEditor tbFileName;

	private UltraLabel ultraLabel10;

	private Panel panel4;

	private GroupBox groupBox2;

	private UltraButton B_Btn_Cncl;

	private UltraButton B_Btn_Next;

	private UltraButton B_Btn_Prev;

	private Panel panel1;

	private UltraLabel ultraLabel6;

	private UltraLabel ultraLabel7;

	private FolderBrowserDialog backupFolderBrowserDialog;

	private UltraTabPageControl Tab_A;

	private UltraTabPageControl Tab_B;

	private Panel panel8;

	private GroupBox groupBox4;

	private UltraButton D_Btn_Fnsh;

	private UltraLabel ultraLabel14;

	private UltraLabel ultraLabel13;

	private UltraLabel ultraLabel12;

	private UltraButton btnOpenFolder;

	private UltraLabel ultraLabel15;

	private UltraLabel lbOutputFilePath;

	private UltraLabel lbl1;

	private UltraTabPageControl Tab_D;

	private UltraTabPageControl Tab_C;

	private Panel panel2;

	private GroupBox groupBox1;

	private Container components = null;

	private UltraLabel lblPage3;

	private UltraLabel lblCaptionPage1_1;

	public Panel panel9;

	private string ProjectCode;

	private string UserID;

	private PccesFormAction FormActionName;

	private string iniFilePath = AppDomain.CurrentDomain.BaseDirectory + "PccesMain.ini";

	private bool F_IsContract = false;

	public string _ProjectCode
	{
		get
		{
			return ProjectCode;
		}
		set
		{
			ProjectCode = value;
		}
	}

	public string _UserID
	{
		get
		{
			return UserID;
		}
		set
		{
			UserID = value;
		}
	}

	public PccesFormAction _ActionName
	{
		get
		{
			return FormActionName;
		}
		set
		{
			FormActionName = value;
		}
	}

	public bool _IsContract
	{
		get
		{
			return F_IsContract;
		}
		set
		{
			F_IsContract = value;
		}
	}

	private void InitializeComponent()
	{
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormBudgetCheckOut_Wzd));
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
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		this.Tab_A = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.lbl1 = new Infragistics.Win.Misc.UltraLabel();
		this.lblCaptionPage1_1 = new Infragistics.Win.Misc.UltraLabel();
		this.panel9 = new System.Windows.Forms.Panel();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.A1_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.A1_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.Tab_B = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel1 = new System.Windows.Forms.Panel();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.panel4 = new System.Windows.Forms.Panel();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.B_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.B_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.B_Btn_Prev = new Infragistics.Win.Misc.UltraButton();
		this.tbBackupPath = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel8 = new Infragistics.Win.Misc.UltraLabel();
		this.btnPickBackupFolder = new Infragistics.Win.Misc.UltraButton();
		this.tbFileName = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel10 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_C = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.lblPage3 = new Infragistics.Win.Misc.UltraLabel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.Tab_D = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.lbOutputFilePath = new Infragistics.Win.Misc.UltraLabel();
		this.btnOpenFolder = new Infragistics.Win.Misc.UltraButton();
		this.ultraLabel15 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel14 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel13 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel12 = new Infragistics.Win.Misc.UltraLabel();
		this.panel8 = new System.Windows.Forms.Panel();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.D_Btn_Fnsh = new Infragistics.Win.Misc.UltraButton();
		this.Tab_Ctrl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.backupFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
		this.Tab_A.SuspendLayout();
		this.panel9.SuspendLayout();
		this.Tab_B.SuspendLayout();
		this.panel1.SuspendLayout();
		this.panel4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.tbBackupPath).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbFileName).BeginInit();
		this.Tab_C.SuspendLayout();
		this.panel2.SuspendLayout();
		this.Tab_D.SuspendLayout();
		this.panel8.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).BeginInit();
		this.Tab_Ctrl.SuspendLayout();
		base.SuspendLayout();
		this.Tab_A.Controls.Add(this.ultraLabel3);
		this.Tab_A.Controls.Add(this.lbl1);
		this.Tab_A.Controls.Add(this.lblCaptionPage1_1);
		this.Tab_A.Controls.Add(this.panel9);
		this.Tab_A.Location = new System.Drawing.Point(0, 0);
		this.Tab_A.Name = "Tab_A";
		this.Tab_A.Size = new System.Drawing.Size(544, 272);
		appearance1.BackColor = System.Drawing.Color.White;
		this.ultraLabel3.Appearance = appearance1;
		this.ultraLabel3.Location = new System.Drawing.Point(32, 82);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(56, 20);
		this.ultraLabel3.TabIndex = 25;
		this.ultraLabel3.Text = "說明:";
		appearance2.BackColor = System.Drawing.Color.White;
		this.lbl1.Appearance = appearance2;
		this.lbl1.Location = new System.Drawing.Point(56, 116);
		this.lbl1.Name = "lbl1";
		this.lbl1.Size = new System.Drawing.Size(464, 24);
		this.lbl1.TabIndex = 23;
		this.lbl1.Text = "此份[%%]備份後，還是可以繼續編輯。";
		appearance3.BackColor = System.Drawing.Color.White;
		this.lblCaptionPage1_1.Appearance = appearance3;
		this.lblCaptionPage1_1.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.lblCaptionPage1_1.Location = new System.Drawing.Point(16, 24);
		this.lblCaptionPage1_1.Name = "lblCaptionPage1_1";
		this.lblCaptionPage1_1.Size = new System.Drawing.Size(588, 20);
		this.lblCaptionPage1_1.TabIndex = 22;
		this.lblCaptionPage1_1.Text = "歡迎使用備份精靈，接下來我們將引導您一步一步備份資料。";
		this.panel9.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		this.panel9.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel9.Controls.Add(this.groupBox5);
		this.panel9.Controls.Add(this.A1_Btn_Cncl);
		this.panel9.Controls.Add(this.A1_Btn_Next);
		this.panel9.Location = new System.Drawing.Point(0, 228);
		this.panel9.Name = "panel9";
		this.panel9.Size = new System.Drawing.Size(544, 44);
		this.panel9.TabIndex = 21;
		this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox5.Location = new System.Drawing.Point(0, 0);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(544, 8);
		this.groupBox5.TabIndex = 3;
		this.groupBox5.TabStop = false;
		this.A1_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance4.Image = resources.GetObject("appearance4.Image");
		appearance4.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A1_Btn_Cncl.Appearance = appearance4;
		this.A1_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.A1_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A1_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.A1_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.A1_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.A1_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.A1_Btn_Cncl.Location = new System.Drawing.Point(448, 10);
		this.A1_Btn_Cncl.Name = "A1_Btn_Cncl";
		this.A1_Btn_Cncl.ShowFocusRect = false;
		this.A1_Btn_Cncl.ShowOutline = false;
		this.A1_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.A1_Btn_Cncl.SupportThemes = false;
		this.A1_Btn_Cncl.TabIndex = 2;
		this.A1_Btn_Cncl.Text = "取消";
		this.A1_Btn_Next.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance5.Image = resources.GetObject("appearance5.Image");
		appearance5.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance5.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A1_Btn_Next.Appearance = appearance5;
		this.A1_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.A1_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A1_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.A1_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.A1_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.A1_Btn_Next.Location = new System.Drawing.Point(356, 10);
		this.A1_Btn_Next.Name = "A1_Btn_Next";
		this.A1_Btn_Next.ShowFocusRect = false;
		this.A1_Btn_Next.ShowOutline = false;
		this.A1_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.A1_Btn_Next.SupportThemes = false;
		this.A1_Btn_Next.TabIndex = 1;
		this.A1_Btn_Next.Text = "下一步";
		this.A1_Btn_Next.Click += new System.EventHandler(A1_Btn_Next_Click);
		this.Tab_B.Controls.Add(this.panel1);
		this.Tab_B.Controls.Add(this.panel4);
		this.Tab_B.Controls.Add(this.tbBackupPath);
		this.Tab_B.Controls.Add(this.ultraLabel8);
		this.Tab_B.Controls.Add(this.btnPickBackupFolder);
		this.Tab_B.Controls.Add(this.tbFileName);
		this.Tab_B.Controls.Add(this.ultraLabel10);
		this.Tab_B.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_B.Name = "Tab_B";
		this.Tab_B.Size = new System.Drawing.Size(544, 272);
		this.panel1.Controls.Add(this.ultraLabel7);
		this.panel1.Controls.Add(this.ultraLabel6);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(544, 56);
		this.panel1.TabIndex = 22;
		appearance6.BackColor = System.Drawing.Color.White;
		this.ultraLabel7.Appearance = appearance6;
		this.ultraLabel7.Location = new System.Drawing.Point(40, 32);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel7.TabIndex = 5;
		this.ultraLabel7.Text = "你可以變更你所需要的檔名，並設定你要存放的目錄";
		appearance7.BackColor = System.Drawing.Color.White;
		this.ultraLabel6.Appearance = appearance7;
		this.ultraLabel6.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel6.Location = new System.Drawing.Point(16, 8);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel6.TabIndex = 4;
		this.ultraLabel6.Text = "輸出檔案及路徑";
		this.panel4.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel4.Controls.Add(this.groupBox2);
		this.panel4.Controls.Add(this.B_Btn_Cncl);
		this.panel4.Controls.Add(this.B_Btn_Next);
		this.panel4.Controls.Add(this.B_Btn_Prev);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel4.Location = new System.Drawing.Point(0, 228);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(544, 44);
		this.panel4.TabIndex = 21;
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(544, 8);
		this.groupBox2.TabIndex = 3;
		this.groupBox2.TabStop = false;
		this.B_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance8.Image = resources.GetObject("appearance8.Image");
		appearance8.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.B_Btn_Cncl.Appearance = appearance8;
		this.B_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.B_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.B_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.B_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.B_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.B_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.B_Btn_Cncl.Location = new System.Drawing.Point(448, 10);
		this.B_Btn_Cncl.Name = "B_Btn_Cncl";
		this.B_Btn_Cncl.ShowFocusRect = false;
		this.B_Btn_Cncl.ShowOutline = false;
		this.B_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.B_Btn_Cncl.SupportThemes = false;
		this.B_Btn_Cncl.TabIndex = 2;
		this.B_Btn_Cncl.Text = "取消";
		this.B_Btn_Next.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance9.Image = resources.GetObject("appearance9.Image");
		appearance9.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance9.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.B_Btn_Next.Appearance = appearance9;
		this.B_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.B_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.B_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.B_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.B_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.B_Btn_Next.Location = new System.Drawing.Point(356, 10);
		this.B_Btn_Next.Name = "B_Btn_Next";
		this.B_Btn_Next.ShowFocusRect = false;
		this.B_Btn_Next.ShowOutline = false;
		this.B_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.B_Btn_Next.SupportThemes = false;
		this.B_Btn_Next.TabIndex = 1;
		this.B_Btn_Next.Text = "下一步";
		this.B_Btn_Next.Click += new System.EventHandler(B_Btn_Next_Click);
		this.B_Btn_Prev.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance10.Image = resources.GetObject("appearance10.Image");
		appearance10.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.B_Btn_Prev.Appearance = appearance10;
		this.B_Btn_Prev.BackColor = System.Drawing.SystemColors.Control;
		this.B_Btn_Prev.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.B_Btn_Prev.Font = new System.Drawing.Font("細明體", 11f);
		this.B_Btn_Prev.ImageSize = new System.Drawing.Size(20, 20);
		this.B_Btn_Prev.ImageTransparentColor = System.Drawing.Color.White;
		this.B_Btn_Prev.Location = new System.Drawing.Point(264, 10);
		this.B_Btn_Prev.Name = "B_Btn_Prev";
		this.B_Btn_Prev.ShowFocusRect = false;
		this.B_Btn_Prev.ShowOutline = false;
		this.B_Btn_Prev.Size = new System.Drawing.Size(88, 31);
		this.B_Btn_Prev.SupportThemes = false;
		this.B_Btn_Prev.TabIndex = 0;
		this.B_Btn_Prev.Text = "上一步";
		this.B_Btn_Prev.Click += new System.EventHandler(B_Btn_Prev_Click);
		this.tbBackupPath.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		appearance11.FontData.Name = "細明體";
		appearance11.FontData.SizeInPoints = 11f;
		this.tbBackupPath.Appearance = appearance11;
		this.tbBackupPath.AutoSize = true;
		this.tbBackupPath.Location = new System.Drawing.Point(85, 147);
		this.tbBackupPath.Name = "tbBackupPath";
		this.tbBackupPath.Size = new System.Drawing.Size(404, 24);
		this.tbBackupPath.TabIndex = 20;
		this.ultraLabel8.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		appearance12.TextHAlign = Infragistics.Win.HAlign.Right;
		this.ultraLabel8.Appearance = appearance12;
		this.ultraLabel8.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraLabel8.Location = new System.Drawing.Point(0, 149);
		this.ultraLabel8.Name = "ultraLabel8";
		this.ultraLabel8.Size = new System.Drawing.Size(80, 23);
		this.ultraLabel8.TabIndex = 19;
		this.ultraLabel8.Text = "存放路徑:";
		this.btnPickBackupFolder.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		appearance13.FontData.Name = "Arial";
		appearance13.FontData.SizeInPoints = 8f;
		this.btnPickBackupFolder.Appearance = appearance13;
		this.btnPickBackupFolder.BackColor = System.Drawing.SystemColors.Control;
		this.btnPickBackupFolder.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.btnPickBackupFolder.Location = new System.Drawing.Point(489, 147);
		this.btnPickBackupFolder.Name = "btnPickBackupFolder";
		this.btnPickBackupFolder.ShowFocusRect = false;
		this.btnPickBackupFolder.ShowOutline = false;
		this.btnPickBackupFolder.Size = new System.Drawing.Size(48, 24);
		this.btnPickBackupFolder.SupportThemes = false;
		this.btnPickBackupFolder.TabIndex = 18;
		this.btnPickBackupFolder.Text = "瀏覽...";
		this.btnPickBackupFolder.Click += new System.EventHandler(btnPickBackupFolder_Click);
		this.tbFileName.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		appearance14.FontData.Name = "細明體";
		appearance14.FontData.SizeInPoints = 11f;
		this.tbFileName.Appearance = appearance14;
		this.tbFileName.AutoSize = true;
		this.tbFileName.Location = new System.Drawing.Point(85, 115);
		this.tbFileName.Name = "tbFileName";
		this.tbFileName.Size = new System.Drawing.Size(404, 24);
		this.tbFileName.TabIndex = 17;
		this.ultraLabel10.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		appearance15.TextHAlign = Infragistics.Win.HAlign.Right;
		this.ultraLabel10.Appearance = appearance15;
		this.ultraLabel10.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraLabel10.Location = new System.Drawing.Point(4, 118);
		this.ultraLabel10.Name = "ultraLabel10";
		this.ultraLabel10.Size = new System.Drawing.Size(76, 23);
		this.ultraLabel10.TabIndex = 16;
		this.ultraLabel10.Text = "檔案名稱:";
		this.Tab_C.Controls.Add(this.lblPage3);
		this.Tab_C.Controls.Add(this.panel2);
		this.Tab_C.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_C.Name = "Tab_C";
		this.Tab_C.Size = new System.Drawing.Size(544, 272);
		appearance16.BackColor = System.Drawing.Color.White;
		appearance16.TextHAlign = Infragistics.Win.HAlign.Center;
		this.lblPage3.Appearance = appearance16;
		this.lblPage3.Location = new System.Drawing.Point(8, 72);
		this.lblPage3.Name = "lblPage3";
		this.lblPage3.Size = new System.Drawing.Size(528, 20);
		this.lblPage3.TabIndex = 23;
		this.lblPage3.Text = "資料備份中，依專案大小所需時間不同，請稍候...";
		this.panel2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel2.Controls.Add(this.groupBox1);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 228);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(544, 44);
		this.panel2.TabIndex = 22;
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(544, 8);
		this.groupBox1.TabIndex = 3;
		this.groupBox1.TabStop = false;
		this.Tab_D.Controls.Add(this.lbOutputFilePath);
		this.Tab_D.Controls.Add(this.btnOpenFolder);
		this.Tab_D.Controls.Add(this.ultraLabel15);
		this.Tab_D.Controls.Add(this.ultraLabel14);
		this.Tab_D.Controls.Add(this.ultraLabel13);
		this.Tab_D.Controls.Add(this.ultraLabel12);
		this.Tab_D.Controls.Add(this.panel8);
		this.Tab_D.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_D.Name = "Tab_D";
		this.Tab_D.Size = new System.Drawing.Size(544, 272);
		this.lbOutputFilePath.Location = new System.Drawing.Point(32, 155);
		this.lbOutputFilePath.Name = "lbOutputFilePath";
		this.lbOutputFilePath.Size = new System.Drawing.Size(488, 69);
		this.lbOutputFilePath.TabIndex = 24;
		this.lbOutputFilePath.Text = "[]";
		appearance17.FontData.Name = "Arial";
		appearance17.FontData.SizeInPoints = 8f;
		this.btnOpenFolder.Appearance = appearance17;
		this.btnOpenFolder.BackColor = System.Drawing.SystemColors.Control;
		this.btnOpenFolder.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.btnOpenFolder.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.btnOpenFolder.Location = new System.Drawing.Point(192, 122);
		this.btnOpenFolder.Name = "btnOpenFolder";
		this.btnOpenFolder.ShowFocusRect = false;
		this.btnOpenFolder.ShowOutline = false;
		this.btnOpenFolder.Size = new System.Drawing.Size(88, 24);
		this.btnOpenFolder.SupportThemes = false;
		this.btnOpenFolder.TabIndex = 23;
		this.btnOpenFolder.Text = "開啟資料夾";
		this.btnOpenFolder.Click += new System.EventHandler(btnOpenFolder_Click);
		this.ultraLabel15.Location = new System.Drawing.Point(32, 128);
		this.ultraLabel15.Name = "ultraLabel15";
		this.ultraLabel15.Size = new System.Drawing.Size(168, 23);
		this.ultraLabel15.TabIndex = 22;
		this.ultraLabel15.Text = "輸出路徑及檔案名稱:";
		appearance18.BackColor = System.Drawing.Color.White;
		this.ultraLabel14.Appearance = appearance18;
		this.ultraLabel14.Location = new System.Drawing.Point(32, 76);
		this.ultraLabel14.Name = "ultraLabel14";
		this.ultraLabel14.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel14.TabIndex = 21;
		this.ultraLabel14.Text = "若要結束精靈，請按一下[完成]。";
		appearance19.BackColor = System.Drawing.Color.White;
		this.ultraLabel13.Appearance = appearance19;
		this.ultraLabel13.Location = new System.Drawing.Point(32, 48);
		this.ultraLabel13.Name = "ultraLabel13";
		this.ultraLabel13.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel13.TabIndex = 20;
		this.ultraLabel13.Text = "你已經成功匯出資料。";
		appearance20.BackColor = System.Drawing.Color.White;
		this.ultraLabel12.Appearance = appearance20;
		this.ultraLabel12.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel12.Location = new System.Drawing.Point(16, 16);
		this.ultraLabel12.Name = "ultraLabel12";
		this.ultraLabel12.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel12.TabIndex = 19;
		this.ultraLabel12.Text = "恭禧您!";
		this.panel8.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel8.Controls.Add(this.groupBox4);
		this.panel8.Controls.Add(this.D_Btn_Fnsh);
		this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel8.Location = new System.Drawing.Point(0, 228);
		this.panel8.Name = "panel8";
		this.panel8.Size = new System.Drawing.Size(544, 44);
		this.panel8.TabIndex = 18;
		this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox4.Location = new System.Drawing.Point(0, 0);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(544, 8);
		this.groupBox4.TabIndex = 3;
		this.groupBox4.TabStop = false;
		appearance21.Image = resources.GetObject("appearance21.Image");
		appearance21.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Fnsh.Appearance = appearance21;
		this.D_Btn_Fnsh.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Fnsh.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.D_Btn_Fnsh.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.D_Btn_Fnsh.Font = new System.Drawing.Font("細明體", 11f);
		this.D_Btn_Fnsh.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Fnsh.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Fnsh.Location = new System.Drawing.Point(448, 10);
		this.D_Btn_Fnsh.Name = "D_Btn_Fnsh";
		this.D_Btn_Fnsh.ShowFocusRect = false;
		this.D_Btn_Fnsh.ShowOutline = false;
		this.D_Btn_Fnsh.Size = new System.Drawing.Size(88, 31);
		this.D_Btn_Fnsh.SupportThemes = false;
		this.D_Btn_Fnsh.TabIndex = 1;
		this.D_Btn_Fnsh.Text = "完成";
		this.Tab_Ctrl.BackColor = System.Drawing.Color.White;
		this.Tab_Ctrl.Controls.Add(this.ultraTabSharedControlsPage1);
		this.Tab_Ctrl.Controls.Add(this.Tab_A);
		this.Tab_Ctrl.Controls.Add(this.Tab_B);
		this.Tab_Ctrl.Controls.Add(this.Tab_D);
		this.Tab_Ctrl.Controls.Add(this.Tab_C);
		this.Tab_Ctrl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Tab_Ctrl.Location = new System.Drawing.Point(0, 0);
		this.Tab_Ctrl.Name = "Tab_Ctrl";
		this.Tab_Ctrl.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.Tab_Ctrl.Size = new System.Drawing.Size(544, 272);
		this.Tab_Ctrl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
		this.Tab_Ctrl.TabIndex = 0;
		ultraTab1.TabPage = this.Tab_A;
		ultraTab1.Text = "tab1";
		appearance22.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		ultraTab2.Appearance = appearance22;
		ultraTab2.TabPage = this.Tab_B;
		ultraTab2.Text = "tab2";
		ultraTab3.TabPage = this.Tab_C;
		ultraTab3.Text = "tab3";
		ultraTab4.TabPage = this.Tab_D;
		ultraTab4.Text = "tab4";
		this.Tab_Ctrl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[4] { ultraTab1, ultraTab2, ultraTab3, ultraTab4 });
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(544, 272);
		this.AutoScaleBaseSize = new System.Drawing.Size(6, 18);
		base.CancelButton = this.A1_Btn_Cncl;
		base.ClientSize = new System.Drawing.Size(544, 272);
		base.Controls.Add(this.Tab_Ctrl);
		this.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormBudgetCheckOut_Wzd";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "備份";
		base.Load += new System.EventHandler(FormBudgetCheckOut_Wzd_Load);
		this.Tab_A.ResumeLayout(false);
		this.panel9.ResumeLayout(false);
		this.Tab_B.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.tbBackupPath).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbFileName).EndInit();
		this.Tab_C.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.Tab_D.ResumeLayout(false);
		this.panel8.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).EndInit();
		this.Tab_Ctrl.ResumeLayout(false);
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

	public FormBudgetCheckOut_Wzd()
	{
		InitializeComponent();
	}

	private void FormBudgetCheckOut_Wzd_Load(object sender, EventArgs e)
	{
		string TailString = "";
		if (FormActionName == PccesFormAction.BUD)
		{
			lbl1.Text = lbl1.Text.Replace("[%%]", "【預算書】");
			Text = "預算書備份";
			TailString = "bdgt";
		}
		else
		{
			lbl1.Text = lbl1.Text.Replace("[%%]", "【標單】");
			Text = "標單備份";
			TailString = "rbid";
		}
		if (F_IsContract)
		{
			lbl1.Text = lbl1.Text.Replace("【預算書】", "【契約書】");
			Text = "契約書備份";
			TailString = "cnt";
		}
		tbFileName.Text = "備份_" + ProjectCode + "_" + $"{DateTime.Now:yyyyMMdd_HHmm}" + "_" + TailString;
		tbBackupPath.Text = CommonMethods.IniReadValue(iniFilePath, "FormBudget", "ExportPath");
	}

	private void btnPickBackupFolder_Click(object sender, EventArgs e)
	{
		backupFolderBrowserDialog.Description = "請挑選你要輸出的路徑";
		if (backupFolderBrowserDialog.ShowDialog() == DialogResult.OK)
		{
			tbBackupPath.Text = backupFolderBrowserDialog.SelectedPath;
		}
	}

	private void B_Btn_Next_Click(object sender, EventArgs e)
	{
		if (tbFileName.Text.Trim() == "")
		{
			string sWarning = "請先給定檔案名稱。";
			MessageBox.Show(this, sWarning, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			tbFileName.Focus();
			return;
		}
		if (tbBackupPath.Text.Trim() == "")
		{
			string sWarning = "請先給定輸出路徑。";
			MessageBox.Show(this, sWarning, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			tbBackupPath.Focus();
			return;
		}
		if (!Directory.Exists(tbBackupPath.Text.Trim()))
		{
			string sWarning = "你所指定的路徑並不存在，請重新挑選。";
			MessageBox.Show(this, sWarning, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			tbBackupPath.Focus();
			return;
		}
		Tab_C.Tab.Selected = true;
		Application.DoEvents();
		Cursor = Cursors.WaitCursor;
		string sXMLKind = "2";
		string ps_ShowCost = "";
		Cursor = Cursors.WaitCursor;
		ArrayList aArr = new ArrayList();
		aArr.Add(UserID);
		aArr.Add("XML 簽出");
		Archnowledge.Pcces.BUDClass.Project projcom = new Archnowledge.Pcces.BUDClass.Project(aArr);
		projcom.ps_srckind = CommonMethods.GetActionNameString(FormActionName);
		if (FormActionName == PccesFormAction.BUD)
		{
			projcom.ps_ShowCost = "1";
			sXMLKind = "1";
		}
		else
		{
			projcom.ps_ShowCost = "1";
			sXMLKind = "2";
		}
		ps_ShowCost = projcom.ps_ShowCost;
		projcom.ps_ShowAnalysis = "1";
		DataSet lds_temp = projcom.OutputXML(ProjectCode, "XM1");
		projcom = null;
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = UserID;
		DataTable DT_PGBK = DBCLS.GetUserDefine("Select SNo,IsPageBreak from " + CommonMethods.GetActionNameString(FormActionName) + "PageBreak Where ProjectCode='" + ProjectCode + "' ");
		for (int z = 0; z < DT_PGBK.Rows.Count; z++)
		{
			if (DT_PGBK.Rows[z]["IsPageBreak"].ToString() == "Y")
			{
				int idx = GetDTDetailRowIndex(lds_temp.Tables["Items"], (int)DT_PGBK.Rows[z]["SNo"]);
				if (idx > -1)
				{
					DataRow dataRow;
					(dataRow = lds_temp.Tables["Items"].Rows[idx])["memo"] = string.Concat(dataRow["memo"], "[跳頁]");
				}
			}
		}
		DBCLS = new DBClass();
		DBCLS._FS_UserID = UserID;
		DT_PGBK = DBCLS.GetUserDefine("Select SNo,PrintToAnalysis from " + CommonMethods.GetActionNameString(FormActionName) + "ItemA Where ProjectCode='" + ProjectCode + "' ");
		for (int z = 0; z < DT_PGBK.Rows.Count; z++)
		{
			if (DT_PGBK.Rows[z]["PrintToAnalysis"].ToString() == "1")
			{
				int idx = GetDTDetailRowIndex(lds_temp.Tables["Items"], (int)DT_PGBK.Rows[z]["SNo"]);
				if (idx > -1)
				{
					DataRow dataRow;
					(dataRow = lds_temp.Tables["Items"].Rows[idx])["memo"] = string.Concat(dataRow["memo"], "[印單]");
				}
			}
		}
		ChgXMLStru XMLCom = new ChgXMLStru();
		if (FormActionName == PccesFormAction.BUD)
		{
			XMLCom._CurrentActionName = "BUD";
		}
		else
		{
			XMLCom._CurrentActionName = "BID";
		}
		if (F_IsContract)
		{
			XMLCom._CurrentActionName = "CNT";
		}
		XMLCom._CheckoutFlag = "CKOut";
		string OutputFilePath = CheckOutputFilePath() + ".PccesBak";
		XMLCom.OutputXML1(lds_temp, OutputFilePath, outItem: true, outAnalysis: true, outResource: true, sXMLKind, ps_ShowCost, FormActionName.ToString());
		PubTools.WriteRoughlyLog(aArr);
		Cursor = Cursors.Default;
		lbOutputFilePath.Text = OutputFilePath;
		Tab_D.Tab.Selected = true;
		Cursor = Cursors.Default;
	}

	private int GetDTDetailRowIndex(DataTable dataTable, int sNo)
	{
		int rowIndex = -1;
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			if (PubTools.Str2Int(dataTable.Rows[i]["sNo"]) == sNo)
			{
				rowIndex = i;
				break;
			}
		}
		return rowIndex;
	}

	private string CheckOutputFilePath()
	{
		string FilePath = tbBackupPath.Text.Trim();
		string FileName = tbFileName.Text.Trim();
		if (FilePath.EndsWith("\\"))
		{
			return FilePath + FileName;
		}
		return FilePath + "\\" + FileName;
	}

	private void A1_Btn_Next_Click(object sender, EventArgs e)
	{
		Tab_B.Tab.Selected = true;
	}

	private void B_Btn_Prev_Click(object sender, EventArgs e)
	{
		Tab_A.Tab.Selected = true;
	}

	private void btnOpenFolder_Click(object sender, EventArgs e)
	{
		ShellExecute SHExe = new ShellExecute();
		SHExe.OwnerHandle = base.Handle;
		SHExe.Parameters = tbBackupPath.Text.Trim();
		SHExe.Path = tbBackupPath.Text.Trim();
		SHExe.Execute();
	}
}
