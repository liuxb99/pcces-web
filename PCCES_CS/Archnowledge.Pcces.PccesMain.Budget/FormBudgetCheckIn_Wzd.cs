using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.DomainModule.Bid;
using Archnowledge.Pcces.DomainModule.Budget;
using Archnowledge.Pcces.DomainModule.LogicalBase;
using Archnowledge.Pcces.XML;
using Archnowledge.Pcces.XML.AuthenticationException;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTabControl;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormBudgetCheckIn_Wzd : Form
{
	private UltraTabControl Tab_Ctrl;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl Tab_A;

	private UltraLabel ultraLabel3;

	private UltraLabel lbl1;

	private UltraLabel ultraLabel18;

	private GroupBox groupBox5;

	private UltraButton A1_Btn_Cncl;

	private UltraButton A1_Btn_Next;

	private UltraTabPageControl Tab_B;

	private Panel panel1;

	private Panel panel4;

	private GroupBox groupBox2;

	private UltraButton B_Btn_Cncl;

	private UltraButton B_Btn_Next;

	private UltraButton B_Btn_Prev;

	private UltraTabPageControl Tab_D;

	private UltraLabel lblOutput;

	private UltraLabel lbRestoreMessage;

	private UltraLabel ultraLabel14;

	private UltraLabel ultraLabel12;

	private Panel panel8;

	private GroupBox groupBox4;

	private UltraButton btnFinish;

	private UltraTabPageControl Tab_C;

	private Panel panel2;

	private GroupBox groupBox1;

	private UltraButton btnPickRestoreFile;

	private UltraTextEditor tbRestoreFilePath;

	private UltraLabel ultraLabel17;

	private Container components = null;

	private OpenFileDialog openRestoreFileDialog;

	private UltraLabel lbl2;

	private UltraLabel lbl4;

	private UltraLabel lbl7;

	private UltraLabel lbl6;

	private UltraLabel lbl_P3;

	private UltraLabel lbl13;

	public Panel panel9;

	private string projectCode;

	private PccesFormAction FormActionName;

	private string F_UserID;

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
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormBudgetCheckIn_Wzd));
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
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		this.Tab_A = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.lbl4 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.lbl2 = new Infragistics.Win.Misc.UltraLabel();
		this.lbl1 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel18 = new Infragistics.Win.Misc.UltraLabel();
		this.panel9 = new System.Windows.Forms.Panel();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.A1_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.A1_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.Tab_B = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.btnPickRestoreFile = new Infragistics.Win.Misc.UltraButton();
		this.tbRestoreFilePath = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel17 = new Infragistics.Win.Misc.UltraLabel();
		this.panel1 = new System.Windows.Forms.Panel();
		this.lbl7 = new Infragistics.Win.Misc.UltraLabel();
		this.lbl6 = new Infragistics.Win.Misc.UltraLabel();
		this.panel4 = new System.Windows.Forms.Panel();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.B_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.B_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.B_Btn_Prev = new Infragistics.Win.Misc.UltraButton();
		this.Tab_C = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.lbl_P3 = new Infragistics.Win.Misc.UltraLabel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.Tab_D = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.lblOutput = new Infragistics.Win.Misc.UltraLabel();
		this.lbRestoreMessage = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel14 = new Infragistics.Win.Misc.UltraLabel();
		this.lbl13 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel12 = new Infragistics.Win.Misc.UltraLabel();
		this.panel8 = new System.Windows.Forms.Panel();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.btnFinish = new Infragistics.Win.Misc.UltraButton();
		this.Tab_Ctrl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.openRestoreFileDialog = new System.Windows.Forms.OpenFileDialog();
		this.Tab_A.SuspendLayout();
		this.panel9.SuspendLayout();
		this.Tab_B.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.tbRestoreFilePath).BeginInit();
		this.panel1.SuspendLayout();
		this.panel4.SuspendLayout();
		this.Tab_C.SuspendLayout();
		this.panel2.SuspendLayout();
		this.Tab_D.SuspendLayout();
		this.panel8.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).BeginInit();
		this.Tab_Ctrl.SuspendLayout();
		base.SuspendLayout();
		this.Tab_A.Controls.Add(this.lbl4);
		this.Tab_A.Controls.Add(this.ultraLabel3);
		this.Tab_A.Controls.Add(this.lbl2);
		this.Tab_A.Controls.Add(this.lbl1);
		this.Tab_A.Controls.Add(this.ultraLabel18);
		this.Tab_A.Controls.Add(this.panel9);
		this.Tab_A.Location = new System.Drawing.Point(0, 0);
		this.Tab_A.Name = "Tab_A";
		this.Tab_A.Size = new System.Drawing.Size(542, 306);
		appearance1.BackColor = System.Drawing.Color.White;
		this.lbl4.Appearance = appearance1;
		this.lbl4.Location = new System.Drawing.Point(56, 176);
		this.lbl4.Name = "lbl4";
		this.lbl4.Size = new System.Drawing.Size(456, 20);
		this.lbl4.TabIndex = 26;
		this.lbl4.Text = "反之，如果要回存的檔案不是之前備份的，將無法完成回存。";
		appearance2.BackColor = System.Drawing.Color.White;
		this.ultraLabel3.Appearance = appearance2;
		this.ultraLabel3.Location = new System.Drawing.Point(32, 56);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(56, 20);
		this.ultraLabel3.TabIndex = 25;
		this.ultraLabel3.Text = "說明:";
		appearance3.BackColor = System.Drawing.Color.White;
		this.lbl2.Appearance = appearance3;
		this.lbl2.Location = new System.Drawing.Point(56, 135);
		this.lbl2.Name = "lbl2";
		this.lbl2.Size = new System.Drawing.Size(456, 20);
		this.lbl2.TabIndex = 24;
		this.lbl2.Text = "如果你即將回存的檔案是這個專案之前轉出的，可以完成回存。";
		appearance4.BackColor = System.Drawing.Color.White;
		this.lbl1.Appearance = appearance4;
		this.lbl1.Location = new System.Drawing.Point(56, 90);
		this.lbl1.Name = "lbl1";
		this.lbl1.Size = new System.Drawing.Size(464, 24);
		this.lbl1.TabIndex = 23;
		this.lbl1.Text = "接下來的動作會將之前備份的資料，作回存動作。";
		appearance5.BackColor = System.Drawing.Color.White;
		this.ultraLabel18.Appearance = appearance5;
		this.ultraLabel18.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel18.Location = new System.Drawing.Point(16, 24);
		this.ultraLabel18.Name = "ultraLabel18";
		this.ultraLabel18.Size = new System.Drawing.Size(588, 20);
		this.ultraLabel18.TabIndex = 22;
		this.ultraLabel18.Text = "歡迎使用回存精靈，接下來我們將引導您一步一步回存資料。";
		this.panel9.AutoSize = true;
		this.panel9.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		this.panel9.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel9.Controls.Add(this.groupBox5);
		this.panel9.Controls.Add(this.A1_Btn_Cncl);
		this.panel9.Controls.Add(this.A1_Btn_Next);
		this.panel9.Location = new System.Drawing.Point(0, 262);
		this.panel9.Name = "panel9";
		this.panel9.Size = new System.Drawing.Size(542, 44);
		this.panel9.TabIndex = 21;
		this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox5.Location = new System.Drawing.Point(0, 0);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(542, 8);
		this.groupBox5.TabIndex = 3;
		this.groupBox5.TabStop = false;
		this.A1_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance6.Image = resources.GetObject("appearance6.Image");
		appearance6.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A1_Btn_Cncl.Appearance = appearance6;
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
		appearance7.Image = resources.GetObject("appearance7.Image");
		appearance7.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance7.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A1_Btn_Next.Appearance = appearance7;
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
		this.Tab_B.Controls.Add(this.btnPickRestoreFile);
		this.Tab_B.Controls.Add(this.tbRestoreFilePath);
		this.Tab_B.Controls.Add(this.ultraLabel17);
		this.Tab_B.Controls.Add(this.panel1);
		this.Tab_B.Controls.Add(this.panel4);
		this.Tab_B.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_B.Name = "Tab_B";
		this.Tab_B.Size = new System.Drawing.Size(542, 306);
		appearance8.FontData.Name = "Arial";
		appearance8.FontData.SizeInPoints = 8f;
		this.btnPickRestoreFile.Appearance = appearance8;
		this.btnPickRestoreFile.BackColor = System.Drawing.SystemColors.Control;
		this.btnPickRestoreFile.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.btnPickRestoreFile.Location = new System.Drawing.Point(488, 152);
		this.btnPickRestoreFile.Name = "btnPickRestoreFile";
		this.btnPickRestoreFile.ShowFocusRect = false;
		this.btnPickRestoreFile.ShowOutline = false;
		this.btnPickRestoreFile.Size = new System.Drawing.Size(48, 24);
		this.btnPickRestoreFile.SupportThemes = false;
		this.btnPickRestoreFile.TabIndex = 25;
		this.btnPickRestoreFile.Text = "瀏覽...";
		this.btnPickRestoreFile.Click += new System.EventHandler(btnPickRestoreFile_Click);
		appearance9.FontData.Name = "細明體";
		appearance9.FontData.SizeInPoints = 11f;
		this.tbRestoreFilePath.Appearance = appearance9;
		this.tbRestoreFilePath.AutoSize = true;
		this.tbRestoreFilePath.Location = new System.Drawing.Point(16, 152);
		this.tbRestoreFilePath.Name = "tbRestoreFilePath";
		this.tbRestoreFilePath.Size = new System.Drawing.Size(472, 24);
		this.tbRestoreFilePath.TabIndex = 24;
		this.ultraLabel17.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraLabel17.Location = new System.Drawing.Point(16, 120);
		this.ultraLabel17.Name = "ultraLabel17";
		this.ultraLabel17.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel17.TabIndex = 23;
		this.ultraLabel17.Text = "欲轉入的電子檔:";
		this.panel1.Controls.Add(this.lbl7);
		this.panel1.Controls.Add(this.lbl6);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(542, 56);
		this.panel1.TabIndex = 22;
		appearance10.BackColor = System.Drawing.Color.White;
		this.lbl7.Appearance = appearance10;
		this.lbl7.Location = new System.Drawing.Point(40, 32);
		this.lbl7.Name = "lbl7";
		this.lbl7.Size = new System.Drawing.Size(408, 20);
		this.lbl7.TabIndex = 5;
		this.lbl7.Text = "請你挑選要執行回存的檔案";
		appearance11.BackColor = System.Drawing.Color.White;
		this.lbl6.Appearance = appearance11;
		this.lbl6.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.lbl6.Location = new System.Drawing.Point(16, 8);
		this.lbl6.Name = "lbl6";
		this.lbl6.Size = new System.Drawing.Size(408, 20);
		this.lbl6.TabIndex = 4;
		this.lbl6.Text = "回存的檔案及路徑";
		this.panel4.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel4.Controls.Add(this.groupBox2);
		this.panel4.Controls.Add(this.B_Btn_Cncl);
		this.panel4.Controls.Add(this.B_Btn_Next);
		this.panel4.Controls.Add(this.B_Btn_Prev);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel4.Location = new System.Drawing.Point(0, 262);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(542, 44);
		this.panel4.TabIndex = 21;
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(542, 8);
		this.groupBox2.TabIndex = 3;
		this.groupBox2.TabStop = false;
		this.B_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance12.Image = resources.GetObject("appearance12.Image");
		appearance12.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.B_Btn_Cncl.Appearance = appearance12;
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
		appearance13.Image = resources.GetObject("appearance13.Image");
		appearance13.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance13.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.B_Btn_Next.Appearance = appearance13;
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
		appearance14.Image = resources.GetObject("appearance14.Image");
		appearance14.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.B_Btn_Prev.Appearance = appearance14;
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
		this.Tab_C.Controls.Add(this.lbl_P3);
		this.Tab_C.Controls.Add(this.panel2);
		this.Tab_C.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_C.Name = "Tab_C";
		this.Tab_C.Size = new System.Drawing.Size(542, 306);
		appearance15.BackColor = System.Drawing.Color.White;
		appearance15.TextHAlign = Infragistics.Win.HAlign.Center;
		this.lbl_P3.Appearance = appearance15;
		this.lbl_P3.Location = new System.Drawing.Point(8, 72);
		this.lbl_P3.Name = "lbl_P3";
		this.lbl_P3.Size = new System.Drawing.Size(528, 20);
		this.lbl_P3.TabIndex = 23;
		this.lbl_P3.Text = "資料回存中，依專案大小所需時間不同，請稍候...";
		this.panel2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel2.Controls.Add(this.groupBox1);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 262);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(542, 44);
		this.panel2.TabIndex = 22;
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(542, 8);
		this.groupBox1.TabIndex = 3;
		this.groupBox1.TabStop = false;
		this.Tab_D.Controls.Add(this.lblOutput);
		this.Tab_D.Controls.Add(this.lbRestoreMessage);
		this.Tab_D.Controls.Add(this.ultraLabel14);
		this.Tab_D.Controls.Add(this.lbl13);
		this.Tab_D.Controls.Add(this.ultraLabel12);
		this.Tab_D.Controls.Add(this.panel8);
		this.Tab_D.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_D.Name = "Tab_D";
		this.Tab_D.Size = new System.Drawing.Size(542, 306);
		this.lblOutput.Location = new System.Drawing.Point(32, 104);
		this.lblOutput.Name = "lblOutput";
		this.lblOutput.Size = new System.Drawing.Size(384, 16);
		this.lblOutput.TabIndex = 25;
		this.lblOutput.Visible = false;
		this.lbRestoreMessage.Location = new System.Drawing.Point(32, 155);
		this.lbRestoreMessage.Name = "lbRestoreMessage";
		this.lbRestoreMessage.Size = new System.Drawing.Size(488, 69);
		this.lbRestoreMessage.TabIndex = 24;
		this.lbRestoreMessage.Text = "[]";
		appearance16.BackColor = System.Drawing.Color.White;
		this.ultraLabel14.Appearance = appearance16;
		this.ultraLabel14.Location = new System.Drawing.Point(32, 76);
		this.ultraLabel14.Name = "ultraLabel14";
		this.ultraLabel14.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel14.TabIndex = 21;
		this.ultraLabel14.Text = "若要結束精靈，請按一下[完成]。";
		appearance17.BackColor = System.Drawing.Color.White;
		this.lbl13.Appearance = appearance17;
		this.lbl13.Location = new System.Drawing.Point(32, 48);
		this.lbl13.Name = "lbl13";
		this.lbl13.Size = new System.Drawing.Size(408, 20);
		this.lbl13.TabIndex = 20;
		this.lbl13.Text = "你已經成功回存資料。";
		appearance18.BackColor = System.Drawing.Color.White;
		this.ultraLabel12.Appearance = appearance18;
		this.ultraLabel12.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel12.Location = new System.Drawing.Point(16, 16);
		this.ultraLabel12.Name = "ultraLabel12";
		this.ultraLabel12.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel12.TabIndex = 19;
		this.ultraLabel12.Text = "恭禧您!";
		this.panel8.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel8.Controls.Add(this.groupBox4);
		this.panel8.Controls.Add(this.btnFinish);
		this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel8.Location = new System.Drawing.Point(0, 262);
		this.panel8.Name = "panel8";
		this.panel8.Size = new System.Drawing.Size(542, 44);
		this.panel8.TabIndex = 18;
		this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox4.Location = new System.Drawing.Point(0, 0);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(542, 8);
		this.groupBox4.TabIndex = 3;
		this.groupBox4.TabStop = false;
		appearance19.Image = resources.GetObject("appearance19.Image");
		appearance19.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnFinish.Appearance = appearance19;
		this.btnFinish.BackColor = System.Drawing.SystemColors.Control;
		this.btnFinish.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnFinish.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.btnFinish.Font = new System.Drawing.Font("細明體", 11f);
		this.btnFinish.ImageSize = new System.Drawing.Size(20, 20);
		this.btnFinish.ImageTransparentColor = System.Drawing.Color.White;
		this.btnFinish.Location = new System.Drawing.Point(448, 10);
		this.btnFinish.Name = "btnFinish";
		this.btnFinish.ShowFocusRect = false;
		this.btnFinish.ShowOutline = false;
		this.btnFinish.Size = new System.Drawing.Size(88, 31);
		this.btnFinish.SupportThemes = false;
		this.btnFinish.TabIndex = 1;
		this.btnFinish.Text = "完成";
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
		this.Tab_Ctrl.Size = new System.Drawing.Size(542, 306);
		this.Tab_Ctrl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
		this.Tab_Ctrl.TabIndex = 1;
		ultraTab1.TabPage = this.Tab_A;
		ultraTab1.Text = "tab1";
		appearance20.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		ultraTab2.Appearance = appearance20;
		ultraTab2.TabPage = this.Tab_B;
		ultraTab2.Text = "tab2";
		ultraTab3.TabPage = this.Tab_C;
		ultraTab3.Text = "tab3";
		ultraTab4.TabPage = this.Tab_D;
		ultraTab4.Text = "tab4";
		this.Tab_Ctrl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[4] { ultraTab1, ultraTab2, ultraTab3, ultraTab4 });
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(542, 306);
		this.openRestoreFileDialog.Filter = "PCCES備份檔(*.PccesBak)|*.PccesBak";
		this.openRestoreFileDialog.RestoreDirectory = true;
		this.AutoScaleBaseSize = new System.Drawing.Size(6, 18);
		base.CancelButton = this.A1_Btn_Cncl;
		base.ClientSize = new System.Drawing.Size(542, 306);
		base.Controls.Add(this.Tab_Ctrl);
		this.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.Name = "FormBudgetCheckIn_Wzd";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "回存";
		this.Tab_A.ResumeLayout(false);
		this.Tab_A.PerformLayout();
		this.panel9.ResumeLayout(false);
		this.Tab_B.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.tbRestoreFilePath).EndInit();
		this.panel1.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
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

	public FormBudgetCheckIn_Wzd()
	{
		InitializeComponent();
	}

	private void B_Btn_Next_Click(object sender, EventArgs e)
	{
		if (IsValidFile())
		{
			Tab_C.Tab.Selected = true;
			Application.DoEvents();
			Cursor = Cursors.WaitCursor;
			RemoveOriginalProject();
			RestoreBackupFile();
		}
	}

	private void RemoveOriginalProject()
	{
		Archnowledge.Pcces.DomainModule.LogicalBase.Project project = null;
		if (FormActionName == PccesFormAction.BUD)
		{
			project = new BudProject();
		}
		else if (FormActionName == PccesFormAction.BID)
		{
			project = new BidProject();
		}
		ExecResult ER = project.RemoveProject(projectCode);
		if (ER.ReturnCode != 0)
		{
			Archnowledge.Pcces.CommonClass.DebugUtil.OutputEventLog(ER.Message);
		}
	}

	private bool IsValidFile()
	{
		if (tbRestoreFilePath.Text.Trim() == "")
		{
			string warning = "請先挑選要轉入的檔案！";
			MessageBox.Show(this, warning, "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
		if (!File.Exists(tbRestoreFilePath.Text.Trim()))
		{
			string warning = "挑選的檔案不存在，請確認後再執行！";
			MessageBox.Show(this, warning, "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
		XMLImporter importer = new XMLImporter(tbRestoreFilePath.Text.Trim());
		if (!importer.IsBackupFile())
		{
			MessageBox.Show(this, "此檔案不是【回存】用格式檔，請確認後再執行！", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
		if (importer.GetProjectCode() != projectCode)
		{
			MessageBox.Show(this, "此檔案不是原本的【備份檔】\n專案代碼不同，請確認後再執行！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
		string documentType = importer.GetDocumentType();
		if ((documentType == "budget" && FormActionName != PccesFormAction.BUD) || (documentType == "contract" && FormActionName != PccesFormAction.BUD) || (documentType == "request" && FormActionName != PccesFormAction.BID) || documentType == "submit")
		{
			MessageBox.Show(this, "此檔案不是原本的【備份檔】\n專案類型不同，請確認後再執行！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return false;
		}
		return true;
	}

	private void RestoreBackupFile()
	{
		bool restoreSucceeded = false;
		XMLValidator validator = new XMLValidator();
		string XSDFilePath = AppDomain.CurrentDomain.BaseDirectory + "\\Report";
		string Message = validator.Validate(tbRestoreFilePath.Text.Trim(), XSDFilePath);
		if (Message != string.Empty)
		{
			MessageBox.Show(this, "轉入來源格式不正確！\n" + Message, "訊息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			Cursor = Cursors.Default;
			return;
		}
		XMLImporter importer = new XMLImporter(tbRestoreFilePath.Text.Trim());
		bool authenticationFailed = false;
		string errorMessage = string.Empty;
		string documentType = importer.GetDocumentType();
		try
		{
			importer.Import(skipAuthentication: false);
			restoreSucceeded = true;
		}
		catch (AuthenticationFailedException)
		{
			if (documentType == "budget" || documentType == "contract")
			{
				DialogResult result = MessageBox.Show(this, "動態驗證碼錯誤，請問是否繼續執行轉入？", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (result != DialogResult.Yes)
				{
					Cursor = Cursors.Default;
					return;
				}
				authenticationFailed = true;
				Application.DoEvents();
				importer.SetAuthenticationFailed();
				try
				{
					importer.Import(skipAuthentication: true);
					restoreSucceeded = true;
				}
				catch (Exception ex2)
				{
					errorMessage = ex2.Message;
				}
			}
			else if (documentType == "request" || documentType == "submit")
			{
				authenticationFailed = true;
				importer.SetAuthenticationFailed();
				try
				{
					importer.Import(skipAuthentication: true);
					restoreSucceeded = true;
				}
				catch (Exception ex2)
				{
					errorMessage = ex2.Message;
				}
			}
		}
		catch (Exception ex2)
		{
			errorMessage = ex2.Message;
		}
		if (restoreSucceeded && documentType == "contract")
		{
			ArrayList aArr = new ArrayList();
			aArr.Add(F_UserID);
			aArr.Add("預算編輯--設定目前預算編輯類型(預算書或契約書)");
			Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
			PROJ.ps_projectCode = projectCode;
			PROJ.ps_srckind = "Cnt";
			PROJ.SetCurrentProjectActionName(projectCode);
			PROJ = null;
		}
		string importMessage = (restoreSucceeded ? "轉入成功！" : ("轉入失敗！\n" + errorMessage));
		switch (documentType)
		{
		case "budget":
			importMessage = "預算書" + importMessage;
			break;
		case "contract":
			importMessage = "契約書" + importMessage;
			break;
		default:
			if (!(documentType == "submit"))
			{
				break;
			}
			goto case "request";
		case "request":
			importMessage = "標單" + importMessage;
			break;
		}
		string projectMessage = (restoreSucceeded ? ("【（" + projectCode + "） " + importer.GetContractTitle() + "】") : string.Empty);
		lbRestoreMessage.Text = projectMessage + Environment.NewLine + importMessage;
		if (!importer.IsOutputFromPcces())
		{
			lbRestoreMessage.Text = "本電子檔非 PCCES 產生，資料正確性請冾原投標廠商！\\n" + lbRestoreMessage.Text;
		}
		if (authenticationFailed)
		{
			lbRestoreMessage.Text = "動態驗證碼錯誤，檔案可能已遭他人修改！\n\n" + lbRestoreMessage.Text;
		}
		if (restoreSucceeded)
		{
			Tab_D.Tab.Selected = true;
		}
		Cursor = Cursors.Default;
	}

	private void B_Btn_Prev_Click(object sender, EventArgs e)
	{
		Tab_A.Tab.Selected = true;
	}

	private void btnPickRestoreFile_Click(object sender, EventArgs e)
	{
		if (openRestoreFileDialog.ShowDialog() == DialogResult.OK)
		{
			tbRestoreFilePath.Text = openRestoreFileDialog.FileName;
		}
	}

	private void A1_Btn_Next_Click(object sender, EventArgs e)
	{
		Tab_B.Tab.Selected = true;
	}
}
