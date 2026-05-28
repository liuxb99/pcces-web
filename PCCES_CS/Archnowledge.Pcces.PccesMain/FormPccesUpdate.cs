using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Management;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.PccesMain.Library;
using Archnowledge.Pcces.PccesMain.PccesUpdateServices;
using Archnowledge.Pcces.STDClass;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinProgressBar;
using Infragistics.Win.UltraWinTabControl;

namespace Archnowledge.Pcces.PccesMain;

public class FormPccesUpdate : Form
{
	private const string defaultUpdateFileDownloadPath = "http://bisc.archnowledge.com/Pcces4Update/PccesUpdate.exe";

	private const string defaultWebServiceURL = "http://bisc.archnowledge.com/pccesupdateservices/Update.asmx";

	private IContainer components = null;

	private UltraTabControl tabControl;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl tabA;

	private UltraTabPageControl tabB;

	private UltraTabPageControl tabC;

	private Panel panel2;

	private Panel panel3;

	private Panel panel4;

	private Panel panelC;

	private UltraPictureBox pictureBoxC;

	private Panel panelA;

	private Panel panel6;

	private Panel panel7;

	private UltraPictureBox pictureBoxA;

	private Panel panel8;

	private Panel panelB;

	private Panel panel10;

	private Panel panel11;

	private UltraPictureBox pictureBoxB;

	private Panel panel12;

	private Label label5;

	private Label label7;

	private Label label6;

	private Label lbPccesVersion;

	private Label label4;

	private Label label2;

	private Label label9;

	private Label lbBMessage;

	private Label lbErrorMessage;

	private Label label10;

	private UltraProgressBar progressBarDownload;

	private Label lblBytesSoFar;

	private Label label1;

	private UltraButton btn_A_Next;

	private UltraButton btn_A_Cancel;

	private UltraButton btn_B_Next;

	private UltraButton btn_C_Retry;

	private UltraButton btn_C_Finish;

	private UltraTabPageControl tabD;

	private Panel panel1;

	private UltraButton btn_D_Finish;

	private Panel panel9;

	private Label label8;

	private Panel panel5;

	private UltraPictureBox pictureBoxD;

	private string appDirectory = AppDomain.CurrentDomain.BaseDirectory;

	private string updateFilePath;

	private Update updateWebService;

	private void InitializeComponent()
	{
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.FormPccesUpdate));
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		this.tabA = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panelA = new System.Windows.Forms.Panel();
		this.panel6 = new System.Windows.Forms.Panel();
		this.label1 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.lbPccesVersion = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.panel7 = new System.Windows.Forms.Panel();
		this.pictureBoxA = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
		this.panel8 = new System.Windows.Forms.Panel();
		this.btn_A_Cancel = new Infragistics.Win.Misc.UltraButton();
		this.btn_A_Next = new Infragistics.Win.Misc.UltraButton();
		this.tabB = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panelB = new System.Windows.Forms.Panel();
		this.panel10 = new System.Windows.Forms.Panel();
		this.lblBytesSoFar = new System.Windows.Forms.Label();
		this.progressBarDownload = new Infragistics.Win.UltraWinProgressBar.UltraProgressBar();
		this.label9 = new System.Windows.Forms.Label();
		this.lbBMessage = new System.Windows.Forms.Label();
		this.panel11 = new System.Windows.Forms.Panel();
		this.pictureBoxB = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
		this.panel12 = new System.Windows.Forms.Panel();
		this.btn_B_Next = new Infragistics.Win.Misc.UltraButton();
		this.tabC = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panelC = new System.Windows.Forms.Panel();
		this.panel4 = new System.Windows.Forms.Panel();
		this.lbErrorMessage = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.panel3 = new System.Windows.Forms.Panel();
		this.pictureBoxC = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
		this.panel2 = new System.Windows.Forms.Panel();
		this.btn_C_Retry = new Infragistics.Win.Misc.UltraButton();
		this.btn_C_Finish = new Infragistics.Win.Misc.UltraButton();
		this.tabD = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel9 = new System.Windows.Forms.Panel();
		this.label8 = new System.Windows.Forms.Label();
		this.panel5 = new System.Windows.Forms.Panel();
		this.pictureBoxD = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
		this.panel1 = new System.Windows.Forms.Panel();
		this.btn_D_Finish = new Infragistics.Win.Misc.UltraButton();
		this.tabControl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.tabA.SuspendLayout();
		this.panelA.SuspendLayout();
		this.panel6.SuspendLayout();
		this.panel7.SuspendLayout();
		this.panel8.SuspendLayout();
		this.tabB.SuspendLayout();
		this.panelB.SuspendLayout();
		this.panel10.SuspendLayout();
		this.panel11.SuspendLayout();
		this.panel12.SuspendLayout();
		this.tabC.SuspendLayout();
		this.panelC.SuspendLayout();
		this.panel4.SuspendLayout();
		this.panel3.SuspendLayout();
		this.panel2.SuspendLayout();
		this.tabD.SuspendLayout();
		this.panel9.SuspendLayout();
		this.panel5.SuspendLayout();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.tabControl).BeginInit();
		this.tabControl.SuspendLayout();
		base.SuspendLayout();
		this.tabA.Controls.Add(this.panelA);
		this.tabA.Location = new System.Drawing.Point(-10000, -10000);
		this.tabA.Name = "tabA";
		this.tabA.Size = new System.Drawing.Size(560, 354);
		this.panelA.Controls.Add(this.panel6);
		this.panelA.Controls.Add(this.panel7);
		this.panelA.Controls.Add(this.panel8);
		this.panelA.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panelA.Location = new System.Drawing.Point(0, 0);
		this.panelA.Name = "panelA";
		this.panelA.Size = new System.Drawing.Size(560, 354);
		this.panelA.TabIndex = 1;
		this.panel6.BackColor = System.Drawing.Color.White;
		this.panel6.Controls.Add(this.label1);
		this.panel6.Controls.Add(this.label5);
		this.panel6.Controls.Add(this.label7);
		this.panel6.Controls.Add(this.label6);
		this.panel6.Controls.Add(this.lbPccesVersion);
		this.panel6.Controls.Add(this.label4);
		this.panel6.Controls.Add(this.label2);
		this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel6.Location = new System.Drawing.Point(160, 0);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(400, 314);
		this.panel6.TabIndex = 2;
		this.label1.Location = new System.Drawing.Point(24, 228);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(264, 23);
		this.label1.TabIndex = 13;
		this.label1.Text = "調整 HTTP 代理伺服器設定";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label5.Location = new System.Drawing.Point(24, 264);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(272, 23);
		this.label5.TabIndex = 12;
		this.label5.Text = "請按 [ 下一步 ] 開始下載更新";
		this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label7.Location = new System.Drawing.Point(51, 193);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(319, 23);
		this.label7.TabIndex = 11;
		this.label7.Text = "系統維護 -> 選項 / 設定 ->  代理伺服器";
		this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label6.Location = new System.Drawing.Point(24, 161);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(264, 23);
		this.label6.TabIndex = 10;
		this.label6.Text = "您可以至";
		this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.lbPccesVersion.Location = new System.Drawing.Point(197, 72);
		this.lbPccesVersion.Name = "lbPccesVersion";
		this.lbPccesVersion.Size = new System.Drawing.Size(168, 23);
		this.lbPccesVersion.TabIndex = 9;
		this.lbPccesVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label4.Location = new System.Drawing.Point(24, 74);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(168, 23);
		this.label4.TabIndex = 8;
		this.label4.Text = "您現有的 PCCES 版本為";
		this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label2.Location = new System.Drawing.Point(24, 24);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(272, 23);
		this.label2.TabIndex = 7;
		this.label2.Text = "歡迎使用 Pcces 自動線上更新程式";
		this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.panel7.Controls.Add(this.pictureBoxA);
		this.panel7.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel7.Location = new System.Drawing.Point(0, 0);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(160, 314);
		this.panel7.TabIndex = 1;
		this.pictureBoxA.BorderShadowColor = System.Drawing.Color.Empty;
		this.pictureBoxA.Dock = System.Windows.Forms.DockStyle.Fill;
		this.pictureBoxA.Image = resources.GetObject("pictureBoxA.Image");
		this.pictureBoxA.Location = new System.Drawing.Point(0, 0);
		this.pictureBoxA.Name = "pictureBoxA";
		this.pictureBoxA.Size = new System.Drawing.Size(160, 314);
		this.pictureBoxA.TabIndex = 0;
		this.panel8.Controls.Add(this.btn_A_Cancel);
		this.panel8.Controls.Add(this.btn_A_Next);
		this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel8.Location = new System.Drawing.Point(0, 314);
		this.panel8.Name = "panel8";
		this.panel8.Size = new System.Drawing.Size(560, 40);
		this.panel8.TabIndex = 0;
		this.btn_A_Cancel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance9.Image = resources.GetObject("appearance9.Image");
		appearance9.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btn_A_Cancel.Appearance = appearance9;
		this.btn_A_Cancel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btn_A_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btn_A_Cancel.ImageSize = new System.Drawing.Size(20, 20);
		this.btn_A_Cancel.ImageTransparentColor = System.Drawing.Color.White;
		this.btn_A_Cancel.Location = new System.Drawing.Point(460, 5);
		this.btn_A_Cancel.Name = "btn_A_Cancel";
		this.btn_A_Cancel.ShowFocusRect = false;
		this.btn_A_Cancel.ShowOutline = false;
		this.btn_A_Cancel.Size = new System.Drawing.Size(88, 31);
		this.btn_A_Cancel.SupportThemes = false;
		this.btn_A_Cancel.TabIndex = 3;
		this.btn_A_Cancel.Text = "取消";
		this.btn_A_Next.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance19.Image = resources.GetObject("appearance19.Image");
		appearance19.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance19.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btn_A_Next.Appearance = appearance19;
		this.btn_A_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btn_A_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.btn_A_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.btn_A_Next.Location = new System.Drawing.Point(366, 5);
		this.btn_A_Next.Name = "btn_A_Next";
		this.btn_A_Next.ShowFocusRect = false;
		this.btn_A_Next.ShowOutline = false;
		this.btn_A_Next.Size = new System.Drawing.Size(88, 31);
		this.btn_A_Next.SupportThemes = false;
		this.btn_A_Next.TabIndex = 2;
		this.btn_A_Next.Text = "下一步";
		this.btn_A_Next.Click += new System.EventHandler(btn_A_Next_Click);
		this.tabB.Controls.Add(this.panelB);
		this.tabB.Location = new System.Drawing.Point(0, 0);
		this.tabB.Name = "tabB";
		this.tabB.Size = new System.Drawing.Size(560, 354);
		this.panelB.Controls.Add(this.panel10);
		this.panelB.Controls.Add(this.panel11);
		this.panelB.Controls.Add(this.panel12);
		this.panelB.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panelB.Location = new System.Drawing.Point(0, 0);
		this.panelB.Name = "panelB";
		this.panelB.Size = new System.Drawing.Size(560, 354);
		this.panelB.TabIndex = 1;
		this.panel10.BackColor = System.Drawing.Color.White;
		this.panel10.Controls.Add(this.lblBytesSoFar);
		this.panel10.Controls.Add(this.progressBarDownload);
		this.panel10.Controls.Add(this.label9);
		this.panel10.Controls.Add(this.lbBMessage);
		this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel10.Location = new System.Drawing.Point(160, 0);
		this.panel10.Name = "panel10";
		this.panel10.Size = new System.Drawing.Size(400, 314);
		this.panel10.TabIndex = 2;
		this.lblBytesSoFar.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lblBytesSoFar.Location = new System.Drawing.Point(24, 268);
		this.lblBytesSoFar.Name = "lblBytesSoFar";
		this.lblBytesSoFar.Size = new System.Drawing.Size(352, 23);
		this.lblBytesSoFar.TabIndex = 22;
		this.lblBytesSoFar.Text = "已下載：0K / 0K";
		this.lblBytesSoFar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		appearance20.BackColor = System.Drawing.Color.White;
		appearance20.BackColor2 = System.Drawing.Color.White;
		this.progressBarDownload.Appearance = appearance20;
		appearance21.BackColor = System.Drawing.Color.FromArgb(192, 192, 255);
		appearance21.BackColor2 = System.Drawing.Color.Navy;
		appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
		this.progressBarDownload.FillAppearance = appearance21;
		this.progressBarDownload.Location = new System.Drawing.Point(24, 158);
		this.progressBarDownload.Name = "progressBarDownload";
		this.progressBarDownload.Size = new System.Drawing.Size(352, 23);
		this.progressBarDownload.SupportThemes = false;
		this.progressBarDownload.TabIndex = 21;
		this.progressBarDownload.Text = "[Formatted]";
		this.label9.Location = new System.Drawing.Point(24, 134);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(120, 23);
		this.label9.TabIndex = 3;
		this.label9.Text = "處理更新清單";
		this.lbBMessage.Location = new System.Drawing.Point(24, 28);
		this.lbBMessage.Name = "lbBMessage";
		this.lbBMessage.Size = new System.Drawing.Size(344, 40);
		this.lbBMessage.TabIndex = 2;
		this.lbBMessage.Text = "下載更新檔，請耐心等候。";
		this.panel11.Controls.Add(this.pictureBoxB);
		this.panel11.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel11.Location = new System.Drawing.Point(0, 0);
		this.panel11.Name = "panel11";
		this.panel11.Size = new System.Drawing.Size(160, 314);
		this.panel11.TabIndex = 1;
		this.pictureBoxB.BorderShadowColor = System.Drawing.Color.Empty;
		this.pictureBoxB.Dock = System.Windows.Forms.DockStyle.Fill;
		this.pictureBoxB.Image = resources.GetObject("pictureBoxB.Image");
		this.pictureBoxB.Location = new System.Drawing.Point(0, 0);
		this.pictureBoxB.Name = "pictureBoxB";
		this.pictureBoxB.Size = new System.Drawing.Size(160, 314);
		this.pictureBoxB.TabIndex = 0;
		this.panel12.Controls.Add(this.btn_B_Next);
		this.panel12.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel12.Location = new System.Drawing.Point(0, 314);
		this.panel12.Name = "panel12";
		this.panel12.Size = new System.Drawing.Size(560, 40);
		this.panel12.TabIndex = 0;
		appearance22.Image = resources.GetObject("appearance22.Image");
		appearance22.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance22.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btn_B_Next.Appearance = appearance22;
		this.btn_B_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btn_B_Next.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.btn_B_Next.Enabled = false;
		this.btn_B_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.btn_B_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.btn_B_Next.Location = new System.Drawing.Point(452, 5);
		this.btn_B_Next.Name = "btn_B_Next";
		this.btn_B_Next.ShowFocusRect = false;
		this.btn_B_Next.ShowOutline = false;
		this.btn_B_Next.Size = new System.Drawing.Size(96, 31);
		this.btn_B_Next.SupportThemes = false;
		this.btn_B_Next.TabIndex = 4;
		this.btn_B_Next.Text = "開始更新";
		this.btn_B_Next.Click += new System.EventHandler(btn_B_Next_Click);
		this.tabC.Controls.Add(this.panelC);
		this.tabC.Location = new System.Drawing.Point(-10000, -10000);
		this.tabC.Name = "tabC";
		this.tabC.Size = new System.Drawing.Size(560, 354);
		this.panelC.Controls.Add(this.panel4);
		this.panelC.Controls.Add(this.panel3);
		this.panelC.Controls.Add(this.panel2);
		this.panelC.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panelC.Location = new System.Drawing.Point(0, 0);
		this.panelC.Name = "panelC";
		this.panelC.Size = new System.Drawing.Size(560, 354);
		this.panelC.TabIndex = 0;
		this.panel4.BackColor = System.Drawing.Color.White;
		this.panel4.Controls.Add(this.lbErrorMessage);
		this.panel4.Controls.Add(this.label10);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel4.Location = new System.Drawing.Point(160, 0);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(400, 314);
		this.panel4.TabIndex = 2;
		this.lbErrorMessage.Location = new System.Drawing.Point(24, 76);
		this.lbErrorMessage.Name = "lbErrorMessage";
		this.lbErrorMessage.Size = new System.Drawing.Size(352, 104);
		this.lbErrorMessage.TabIndex = 3;
		this.label10.Location = new System.Drawing.Point(24, 28);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(216, 23);
		this.label10.TabIndex = 2;
		this.label10.Text = "Pcces 線上更新錯誤";
		this.panel3.Controls.Add(this.pictureBoxC);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel3.Location = new System.Drawing.Point(0, 0);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(160, 314);
		this.panel3.TabIndex = 1;
		this.pictureBoxC.BorderShadowColor = System.Drawing.Color.Empty;
		this.pictureBoxC.Dock = System.Windows.Forms.DockStyle.Fill;
		this.pictureBoxC.Image = resources.GetObject("pictureBoxC.Image");
		this.pictureBoxC.Location = new System.Drawing.Point(0, 0);
		this.pictureBoxC.Name = "pictureBoxC";
		this.pictureBoxC.Size = new System.Drawing.Size(160, 314);
		this.pictureBoxC.TabIndex = 0;
		this.panel2.Controls.Add(this.btn_C_Retry);
		this.panel2.Controls.Add(this.btn_C_Finish);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 314);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(560, 40);
		this.panel2.TabIndex = 0;
		appearance23.Image = resources.GetObject("appearance23.Image");
		appearance23.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btn_C_Retry.Appearance = appearance23;
		this.btn_C_Retry.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btn_C_Retry.ImageSize = new System.Drawing.Size(20, 20);
		this.btn_C_Retry.ImageTransparentColor = System.Drawing.Color.White;
		this.btn_C_Retry.Location = new System.Drawing.Point(366, 5);
		this.btn_C_Retry.Name = "btn_C_Retry";
		this.btn_C_Retry.ShowFocusRect = false;
		this.btn_C_Retry.ShowOutline = false;
		this.btn_C_Retry.Size = new System.Drawing.Size(88, 31);
		this.btn_C_Retry.SupportThemes = false;
		this.btn_C_Retry.TabIndex = 4;
		this.btn_C_Retry.Text = "重試";
		this.btn_C_Retry.Click += new System.EventHandler(backToTabA_Click);
		appearance24.Image = resources.GetObject("appearance24.Image");
		appearance24.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btn_C_Finish.Appearance = appearance24;
		this.btn_C_Finish.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btn_C_Finish.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btn_C_Finish.ImageSize = new System.Drawing.Size(20, 20);
		this.btn_C_Finish.ImageTransparentColor = System.Drawing.Color.White;
		this.btn_C_Finish.Location = new System.Drawing.Point(460, 5);
		this.btn_C_Finish.Name = "btn_C_Finish";
		this.btn_C_Finish.ShowFocusRect = false;
		this.btn_C_Finish.ShowOutline = false;
		this.btn_C_Finish.Size = new System.Drawing.Size(88, 31);
		this.btn_C_Finish.SupportThemes = false;
		this.btn_C_Finish.TabIndex = 3;
		this.btn_C_Finish.Text = "結束";
		this.tabD.Controls.Add(this.panel9);
		this.tabD.Controls.Add(this.panel5);
		this.tabD.Controls.Add(this.panel1);
		this.tabD.Location = new System.Drawing.Point(-10000, -10000);
		this.tabD.Name = "tabD";
		this.tabD.Size = new System.Drawing.Size(560, 354);
		this.panel9.BackColor = System.Drawing.Color.White;
		this.panel9.Controls.Add(this.label8);
		this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel9.Location = new System.Drawing.Point(160, 0);
		this.panel9.Name = "panel9";
		this.panel9.Size = new System.Drawing.Size(400, 314);
		this.panel9.TabIndex = 3;
		this.label8.Location = new System.Drawing.Point(24, 28);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(216, 23);
		this.label8.TabIndex = 2;
		this.label8.Text = "您的 Pcces 已是最新版本。";
		this.panel5.Controls.Add(this.pictureBoxD);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel5.Location = new System.Drawing.Point(0, 0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(160, 314);
		this.panel5.TabIndex = 2;
		this.pictureBoxD.BorderShadowColor = System.Drawing.Color.Empty;
		this.pictureBoxD.Dock = System.Windows.Forms.DockStyle.Fill;
		this.pictureBoxD.Image = resources.GetObject("pictureBoxD.Image");
		this.pictureBoxD.Location = new System.Drawing.Point(0, 0);
		this.pictureBoxD.Name = "pictureBoxD";
		this.pictureBoxD.Size = new System.Drawing.Size(160, 314);
		this.pictureBoxD.TabIndex = 0;
		this.panel1.Controls.Add(this.btn_D_Finish);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 314);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(560, 40);
		this.panel1.TabIndex = 1;
		appearance25.Image = resources.GetObject("appearance25.Image");
		appearance25.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btn_D_Finish.Appearance = appearance25;
		this.btn_D_Finish.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btn_D_Finish.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btn_D_Finish.ImageSize = new System.Drawing.Size(20, 20);
		this.btn_D_Finish.ImageTransparentColor = System.Drawing.Color.White;
		this.btn_D_Finish.Location = new System.Drawing.Point(460, 5);
		this.btn_D_Finish.Name = "btn_D_Finish";
		this.btn_D_Finish.ShowFocusRect = false;
		this.btn_D_Finish.ShowOutline = false;
		this.btn_D_Finish.Size = new System.Drawing.Size(88, 31);
		this.btn_D_Finish.SupportThemes = false;
		this.btn_D_Finish.TabIndex = 3;
		this.btn_D_Finish.Text = "結束";
		appearance26.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.tabControl.Appearance = appearance26;
		this.tabControl.Controls.Add(this.ultraTabSharedControlsPage1);
		this.tabControl.Controls.Add(this.tabA);
		this.tabControl.Controls.Add(this.tabB);
		this.tabControl.Controls.Add(this.tabC);
		this.tabControl.Controls.Add(this.tabD);
		this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tabControl.Location = new System.Drawing.Point(0, 0);
		this.tabControl.Name = "tabControl";
		this.tabControl.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.tabControl.Size = new System.Drawing.Size(560, 354);
		this.tabControl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
		this.tabControl.TabIndex = 0;
		ultraTab1.Key = "Tab_A";
		ultraTab1.TabPage = this.tabA;
		ultraTab1.Text = "tab1";
		ultraTab2.Key = "Tab_B";
		ultraTab2.TabPage = this.tabB;
		ultraTab2.Text = "tab2";
		ultraTab3.Key = "Tab_E";
		ultraTab3.TabPage = this.tabC;
		ultraTab3.Text = "tab5";
		ultraTab4.TabPage = this.tabD;
		ultraTab4.Text = "tab3";
		this.tabControl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[4] { ultraTab1, ultraTab2, ultraTab3, ultraTab4 });
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(560, 354);
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.CancelButton = this.btn_A_Cancel;
		base.ClientSize = new System.Drawing.Size(560, 354);
		base.Controls.Add(this.tabControl);
		this.Cursor = System.Windows.Forms.Cursors.Default;
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormPccesUpdate";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "PCCES 線上更新";
		base.Load += new System.EventHandler(PccesUpdaterForm_Load);
		this.tabA.ResumeLayout(false);
		this.panelA.ResumeLayout(false);
		this.panel6.ResumeLayout(false);
		this.panel7.ResumeLayout(false);
		this.panel8.ResumeLayout(false);
		this.tabB.ResumeLayout(false);
		this.panelB.ResumeLayout(false);
		this.panel10.ResumeLayout(false);
		this.panel11.ResumeLayout(false);
		this.panel12.ResumeLayout(false);
		this.tabC.ResumeLayout(false);
		this.panelC.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.tabD.ResumeLayout(false);
		this.panel9.ResumeLayout(false);
		this.panel5.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.tabControl).EndInit();
		this.tabControl.ResumeLayout(false);
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

	public string getUpdateFileName()
	{
		return updateFilePath;
	}

	public FormPccesUpdate()
	{
		InitializeComponent();
	}

	private void PccesUpdaterForm_Load(object sender, EventArgs e)
	{
		initializeWebService();
		lbPccesVersion.Text = PccesVersion.PccesAssemblyVersion;
	}

	private void initializeWebService()
	{
		updateWebService = new Update();
		string URL = CommonMethods.GetIniValue("DownloadInfo", "webServiceRoute");
		string usingProxy = CommonMethods.GetIniValue("ProxyInfo", "usingProxy");
		if (URL == string.Empty)
		{
			URL = "http://bisc.archnowledge.com/pccesupdateservices/Update.asmx";
		}
		updateWebService.Url = URL;
		if (usingProxy.Trim().ToUpper() == "TRUE")
		{
			updateWebService.Proxy = GetProxy();
		}
		else
		{
			updateWebService.UseDefaultCredentials = true;
		}
	}

	private WebProxy GetProxy()
	{
		WebProxy proxy = new WebProxy();
		string port = CommonMethods.GetIniValue("ProxyInfo", "port");
		string account = CommonMethods.GetIniValue("ProxyInfo", "account");
		string password = CommonMethods.GetIniValue("ProxyInfo", "password");
		string address = CommonMethods.GetIniValue("ProxyInfo", "address");
		proxy.Address = new Uri(address + ":" + port);
		proxy.Credentials = new NetworkCredential(account, password);
		return proxy;
	}

	private void btn_A_Next_Click(object sender, EventArgs e)
	{
		string latestVersion = string.Empty;
		try
		{
			latestVersion = updateWebService.GetPccesVersion();
		}
		catch (Exception ex)
		{
			tabC.Tab.Selected = true;
			lbErrorMessage.Text = "無法讀取 Pcces 更新版本！請確定網路連線正常。" + ex.Message;
			return;
		}
		if (PccesVersion.CompareVersion(latestVersion, PccesVersion.PccesAssemblyVersion))
		{
			if (!isValidUser())
			{
				tabC.Tab.Selected = true;
				lbErrorMessage.Text = "請先確認您已經完成註冊！";
				return;
			}
			tabB.Tab.Selected = true;
			string fileAddress = updateWebService.GetUpdateFileAddressWithCurrentVersion(PccesVersion.PccesAssemblyVersion);
			if (fileAddress == string.Empty)
			{
				fileAddress = "http://bisc.archnowledge.com/Pcces4Update/PccesUpdate.exe";
			}
			updateFilePath = appDirectory + Path.GetFileName(fileAddress);
			DownloadThread downloadThread = new DownloadThread();
			downloadThread.CompleteCallback += DownloadCompleteCallback;
			downloadThread.ProgressCallback += DownloadProgressCallback;
			downloadThread.FailCallback += DownloadFailCallback;
			downloadThread.DownloadURL = fileAddress;
			downloadThread.savePath = updateFilePath;
			string usingProxy = CommonMethods.GetIniValue("ProxyInfo", "usingProxy");
			if (usingProxy.Trim().ToUpper() == "TRUE")
			{
				downloadThread.proxy = GetProxy();
			}
			Thread thread = new Thread(downloadThread.Download);
			thread.Start();
		}
		else
		{
			tabD.Tab.Selected = true;
		}
	}

	private bool isValidUser()
	{
		string registerID = CommonMethods.GetIniValue("Register", "RegID");
		string userName = CommonMethods.GetIniValue("Register", "UserName");
		string emailAddress = CommonMethods.GetIniValue("Register", "EMail");
		string macAddress = GetMacAddress();
		if (userName.Length >= 4 && userName.ToUpper().StartsWith("TR--"))
		{
			return true;
		}
		if (registerID.Trim() != string.Empty && updateWebService.IsStillValid(registerID, userName, emailAddress, macAddress) && updateWebService.IsApproved(registerID))
		{
			return true;
		}
		return false;
	}

	public string GetMacAddress()
	{
		ManagementObjectSearcher query = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration");
		ManagementObjectCollection queryCollection = query.Get();
		foreach (ManagementObject managementObject in queryCollection)
		{
			if ((bool)managementObject["IPEnabled"])
			{
				string[] addresses = (string[])managementObject["IPAddress"];
				if (addresses[0].ToString() != string.Empty)
				{
					return managementObject["MacAddress"].ToString();
				}
			}
		}
		return string.Empty;
	}

	private void DownloadFailCallback(Exception exception)
	{
		progressBarDownload.Minimum = 0;
		progressBarDownload.Maximum = 1;
		progressBarDownload.Value = 0;
		Cursor = Cursors.Default;
		lbErrorMessage.Text = "下載執行緒失敗！" + exception.Message;
		tabC.Tab.Selected = true;
	}

	private void DownloadProgressCallback(int byteSoFar, int totalBytes)
	{
		int charCount = 40 - totalBytes.ToString("#,##0").Length - byteSoFar.ToString("#,##0").Length;
		progressBarDownload.Minimum = 0;
		progressBarDownload.Maximum = totalBytes;
		progressBarDownload.Value = byteSoFar;
		lblBytesSoFar.Text = "已下載：" + Convert.ToInt32(byteSoFar / 1024) + "K / " + Convert.ToInt32(totalBytes / 1024) + "K";
	}

	private void DownloadCompleteCallback(int byteSoFar, int totalBytes)
	{
		int charCount = 40 - 2 * byteSoFar.ToString("#,##0").Length;
		progressBarDownload.Minimum = 0;
		progressBarDownload.Maximum = 1;
		progressBarDownload.Value = 1;
		if (totalBytes != byteSoFar)
		{
			tabC.Tab.Selected = true;
			lbErrorMessage.Text = "更新檔下載不完全請重新下載！";
		}
		else
		{
			lbBMessage.Text = "完成下載更新，請按 [ 開始 ] 更新以開始更新程序。";
			btn_B_Next.Enabled = true;
		}
	}

	private void backToTabA_Click(object sender, EventArgs e)
	{
		tabA.Tab.Selected = true;
	}

	private void btn_B_Next_Click(object sender, EventArgs e)
	{
		MessageBox.Show(this, "Pcces 4.3 即將關閉以執行更新程序！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}
}
