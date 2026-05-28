using System;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.PccesMain.PccesUpdateServices;
using Archnowledge.Pcces.STDClass;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTabControl;

namespace Archnowledge.Pcces.PccesMain;

public class FormRegister : Form
{
	private Panel panel1;

	private GroupBox groupBox1;

	private UltraButton btn_B_Cancel;

	private UltraButton btnRegister;

	private Panel panel2;

	private GroupBox gbOnlineRegister;

	private UltraTextEditor tbUserName;

	private UltraLabel lbUserName;

	private UltraLabel lbEMailAddress;

	private UltraTextEditor tbEMailAddress;

	private UltraLabel lbCompanyName;

	private UltraTextEditor tbCompanyName;

	private UltraLabel lbDepartmentName;

	private UltraLabel lbPhoneNumber;

	private UltraTextEditor tbDepartmentName;

	private UltraTextEditor tbPhoneNumber;

	private UltraTabControl tabControl;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl tabA;

	private UltraTabPageControl tabB;

	private UltraTabPageControl tabC;

	private UltraLabel lbWelcomeMessage;

	private UltraLabel lbRegisterTitle;

	private Panel panel3;

	private GroupBox groupBox3;

	private UltraButton btn_A_Cancel;

	private UltraButton btn_A_Next;

	private UltraLabel lbRegisterInstruction;

	private Panel panel4;

	private UltraButton btnRegisterManually;

	private GroupBox groupBox4;

	private UltraButton btn_C_Cancel;

	private GroupBox gbManualRegistration;

	private UltraLabel lbManualRegistrationTitle;

	private UltraLabel lbManualRegistrationInstruction;

	private UltraLabel lbSerialNumber;

	private UltraTextEditor tbSerialNumber;

	private UltraOptionSet optionRegister;

	private Panel panel5;

	private UltraButton btnFinish;

	private GroupBox groupBox6;

	private UltraLabel lbRegistrationCode;

	private UltraLabel lbRegistrationCodeTitle;

	private Label lbSerialNumberInstruction;

	private Container components = null;

	private UltraTabPageControl tabD;

	private UltraLabel lbRegistrationCompleteTitle;

	private UltraLabel lbRegistrationCompleteInstruction;

	private string guid = string.Empty;

	private string S1 = string.Empty;

	private string S2 = string.Empty;

	private void InitializeComponent()
	{
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.FormRegister));
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
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		this.tabA = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.optionRegister = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
		this.lbRegisterInstruction = new Infragistics.Win.Misc.UltraLabel();
		this.panel3 = new System.Windows.Forms.Panel();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.btn_A_Cancel = new Infragistics.Win.Misc.UltraButton();
		this.btn_A_Next = new Infragistics.Win.Misc.UltraButton();
		this.lbRegisterTitle = new Infragistics.Win.Misc.UltraLabel();
		this.lbWelcomeMessage = new Infragistics.Win.Misc.UltraLabel();
		this.tabB = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel1 = new System.Windows.Forms.Panel();
		this.btnRegister = new Infragistics.Win.Misc.UltraButton();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.btn_B_Cancel = new Infragistics.Win.Misc.UltraButton();
		this.panel2 = new System.Windows.Forms.Panel();
		this.gbOnlineRegister = new System.Windows.Forms.GroupBox();
		this.tbPhoneNumber = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.lbPhoneNumber = new Infragistics.Win.Misc.UltraLabel();
		this.tbDepartmentName = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.lbDepartmentName = new Infragistics.Win.Misc.UltraLabel();
		this.tbCompanyName = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.lbCompanyName = new Infragistics.Win.Misc.UltraLabel();
		this.lbEMailAddress = new Infragistics.Win.Misc.UltraLabel();
		this.tbEMailAddress = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.lbUserName = new Infragistics.Win.Misc.UltraLabel();
		this.tbUserName = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.tabC = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.gbManualRegistration = new System.Windows.Forms.GroupBox();
		this.lbSerialNumberInstruction = new System.Windows.Forms.Label();
		this.lbRegistrationCodeTitle = new Infragistics.Win.Misc.UltraLabel();
		this.lbRegistrationCode = new Infragistics.Win.Misc.UltraLabel();
		this.tbSerialNumber = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.lbSerialNumber = new Infragistics.Win.Misc.UltraLabel();
		this.lbManualRegistrationInstruction = new Infragistics.Win.Misc.UltraLabel();
		this.lbManualRegistrationTitle = new Infragistics.Win.Misc.UltraLabel();
		this.panel4 = new System.Windows.Forms.Panel();
		this.btnRegisterManually = new Infragistics.Win.Misc.UltraButton();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.btn_C_Cancel = new Infragistics.Win.Misc.UltraButton();
		this.tabD = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.lbRegistrationCompleteInstruction = new Infragistics.Win.Misc.UltraLabel();
		this.lbRegistrationCompleteTitle = new Infragistics.Win.Misc.UltraLabel();
		this.panel5 = new System.Windows.Forms.Panel();
		this.btnFinish = new Infragistics.Win.Misc.UltraButton();
		this.groupBox6 = new System.Windows.Forms.GroupBox();
		this.tabControl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.tabA.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.optionRegister).BeginInit();
		this.panel3.SuspendLayout();
		this.tabB.SuspendLayout();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		this.gbOnlineRegister.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.tbPhoneNumber).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbDepartmentName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbCompanyName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbEMailAddress).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tbUserName).BeginInit();
		this.tabC.SuspendLayout();
		this.gbManualRegistration.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.tbSerialNumber).BeginInit();
		this.panel4.SuspendLayout();
		this.tabD.SuspendLayout();
		this.panel5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.tabControl).BeginInit();
		this.tabControl.SuspendLayout();
		base.SuspendLayout();
		this.tabA.Controls.Add(this.optionRegister);
		this.tabA.Controls.Add(this.lbRegisterInstruction);
		this.tabA.Controls.Add(this.panel3);
		this.tabA.Controls.Add(this.lbRegisterTitle);
		this.tabA.Controls.Add(this.lbWelcomeMessage);
		this.tabA.Location = new System.Drawing.Point(-10000, -10000);
		this.tabA.Name = "tabA";
		this.tabA.Size = new System.Drawing.Size(528, 342);
		this.optionRegister.BackColor = System.Drawing.Color.Transparent;
		this.optionRegister.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
		this.optionRegister.CheckedIndex = 0;
		this.optionRegister.ItemAppearance = appearance1;
		valueListItem1.DataValue = "A";
		valueListItem1.DisplayText = "線上註冊(透過網際網路)";
		valueListItem2.DataValue = "B";
		valueListItem2.DisplayText = "手動註冊(經由客服專線)";
		this.optionRegister.Items.Add(valueListItem1);
		this.optionRegister.Items.Add(valueListItem2);
		this.optionRegister.ItemSpacingVertical = 10;
		this.optionRegister.Location = new System.Drawing.Point(88, 208);
		this.optionRegister.Name = "optionRegister";
		this.optionRegister.Size = new System.Drawing.Size(256, 64);
		this.optionRegister.TabIndex = 11;
		this.optionRegister.Text = "線上註冊(透過網際網路)";
		this.lbRegisterInstruction.BackColor = System.Drawing.Color.Transparent;
		this.lbRegisterInstruction.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lbRegisterInstruction.Location = new System.Drawing.Point(40, 177);
		this.lbRegisterInstruction.Name = "lbRegisterInstruction";
		this.lbRegisterInstruction.Size = new System.Drawing.Size(160, 23);
		this.lbRegisterInstruction.TabIndex = 10;
		this.lbRegisterInstruction.Text = "請先選擇註冊方式:";
		this.panel3.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel3.Controls.Add(this.groupBox3);
		this.panel3.Controls.Add(this.btn_A_Cancel);
		this.panel3.Controls.Add(this.btn_A_Next);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel3.Location = new System.Drawing.Point(0, 292);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(528, 50);
		this.panel3.TabIndex = 9;
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox3.Location = new System.Drawing.Point(0, 0);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(528, 8);
		this.groupBox3.TabIndex = 3;
		this.groupBox3.TabStop = false;
		this.btn_A_Cancel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btn_A_Cancel.Appearance = appearance2;
		this.btn_A_Cancel.BackColor = System.Drawing.SystemColors.Control;
		this.btn_A_Cancel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btn_A_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btn_A_Cancel.Font = new System.Drawing.Font("細明體", 11f);
		this.btn_A_Cancel.ImageSize = new System.Drawing.Size(20, 20);
		this.btn_A_Cancel.ImageTransparentColor = System.Drawing.Color.White;
		this.btn_A_Cancel.Location = new System.Drawing.Point(428, 12);
		this.btn_A_Cancel.Name = "btn_A_Cancel";
		this.btn_A_Cancel.ShowFocusRect = false;
		this.btn_A_Cancel.ShowOutline = false;
		this.btn_A_Cancel.Size = new System.Drawing.Size(88, 31);
		this.btn_A_Cancel.SupportThemes = false;
		this.btn_A_Cancel.TabIndex = 2;
		this.btn_A_Cancel.Text = "取消";
		this.btn_A_Next.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance3.Image = resources.GetObject("appearance3.Image");
		appearance3.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance3.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btn_A_Next.Appearance = appearance3;
		this.btn_A_Next.BackColor = System.Drawing.SystemColors.Control;
		this.btn_A_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btn_A_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.btn_A_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.btn_A_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.btn_A_Next.Location = new System.Drawing.Point(334, 12);
		this.btn_A_Next.Name = "btn_A_Next";
		this.btn_A_Next.ShowFocusRect = false;
		this.btn_A_Next.ShowOutline = false;
		this.btn_A_Next.Size = new System.Drawing.Size(88, 31);
		this.btn_A_Next.SupportThemes = false;
		this.btn_A_Next.TabIndex = 1;
		this.btn_A_Next.Text = "下一步";
		this.btn_A_Next.Click += new System.EventHandler(btn_A_Next_Click);
		this.lbRegisterTitle.BackColor = System.Drawing.Color.Transparent;
		this.lbRegisterTitle.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.lbRegisterTitle.Location = new System.Drawing.Point(16, 24);
		this.lbRegisterTitle.Name = "lbRegisterTitle";
		this.lbRegisterTitle.Size = new System.Drawing.Size(100, 23);
		this.lbRegisterTitle.TabIndex = 3;
		this.lbRegisterTitle.Text = "註冊：";
		appearance4.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.lbWelcomeMessage.Appearance = appearance4;
		this.lbWelcomeMessage.BackColor = System.Drawing.Color.Transparent;
		this.lbWelcomeMessage.Font = new System.Drawing.Font("新細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lbWelcomeMessage.InnerBorderPadding = new System.Drawing.Size(2, 2);
		this.lbWelcomeMessage.Location = new System.Drawing.Point(40, 56);
		this.lbWelcomeMessage.Name = "lbWelcomeMessage";
		this.lbWelcomeMessage.Size = new System.Drawing.Size(456, 104);
		this.lbWelcomeMessage.TabIndex = 2;
		this.lbWelcomeMessage.Text = "為加快服務反應速度，希望您藉由線上註冊\r\n\r\n提供給我們與您聯絡的方式，並可讓我們為您提供更好的服務。\r\n\r\nPCCES 是免費軟體，註冊不需付任何費用，謝謝您的配合！";
		this.tabB.Controls.Add(this.panel1);
		this.tabB.Controls.Add(this.panel2);
		this.tabB.Location = new System.Drawing.Point(-10000, -10000);
		this.tabB.Name = "tabB";
		this.tabB.Size = new System.Drawing.Size(528, 342);
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.Controls.Add(this.btnRegister);
		this.panel1.Controls.Add(this.groupBox1);
		this.panel1.Controls.Add(this.btn_B_Cancel);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 292);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(528, 50);
		this.panel1.TabIndex = 10;
		this.btnRegister.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance5.Image = resources.GetObject("appearance5.Image");
		appearance5.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnRegister.Appearance = appearance5;
		this.btnRegister.BackColor = System.Drawing.SystemColors.Control;
		this.btnRegister.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnRegister.Font = new System.Drawing.Font("細明體", 11f);
		this.btnRegister.ImageSize = new System.Drawing.Size(20, 20);
		this.btnRegister.ImageTransparentColor = System.Drawing.Color.White;
		this.btnRegister.Location = new System.Drawing.Point(334, 12);
		this.btnRegister.Name = "btnRegister";
		this.btnRegister.ShowFocusRect = false;
		this.btnRegister.ShowOutline = false;
		this.btnRegister.Size = new System.Drawing.Size(88, 31);
		this.btnRegister.SupportThemes = false;
		this.btnRegister.TabIndex = 4;
		this.btnRegister.Text = "註冊";
		this.btnRegister.Click += new System.EventHandler(btnRegister_Click);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(528, 8);
		this.groupBox1.TabIndex = 3;
		this.groupBox1.TabStop = false;
		this.btn_B_Cancel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance6.Image = resources.GetObject("appearance6.Image");
		appearance6.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btn_B_Cancel.Appearance = appearance6;
		this.btn_B_Cancel.BackColor = System.Drawing.SystemColors.Control;
		this.btn_B_Cancel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btn_B_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btn_B_Cancel.Font = new System.Drawing.Font("細明體", 11f);
		this.btn_B_Cancel.ImageSize = new System.Drawing.Size(20, 20);
		this.btn_B_Cancel.ImageTransparentColor = System.Drawing.Color.White;
		this.btn_B_Cancel.Location = new System.Drawing.Point(428, 12);
		this.btn_B_Cancel.Name = "btn_B_Cancel";
		this.btn_B_Cancel.ShowFocusRect = false;
		this.btn_B_Cancel.ShowOutline = false;
		this.btn_B_Cancel.Size = new System.Drawing.Size(88, 31);
		this.btn_B_Cancel.SupportThemes = false;
		this.btn_B_Cancel.TabIndex = 2;
		this.btn_B_Cancel.Text = "取消";
		this.panel2.BackColor = System.Drawing.Color.White;
		this.panel2.Controls.Add(this.gbOnlineRegister);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(528, 342);
		this.panel2.TabIndex = 11;
		this.gbOnlineRegister.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.gbOnlineRegister.Controls.Add(this.tbPhoneNumber);
		this.gbOnlineRegister.Controls.Add(this.lbPhoneNumber);
		this.gbOnlineRegister.Controls.Add(this.tbDepartmentName);
		this.gbOnlineRegister.Controls.Add(this.lbDepartmentName);
		this.gbOnlineRegister.Controls.Add(this.tbCompanyName);
		this.gbOnlineRegister.Controls.Add(this.lbCompanyName);
		this.gbOnlineRegister.Controls.Add(this.lbEMailAddress);
		this.gbOnlineRegister.Controls.Add(this.tbEMailAddress);
		this.gbOnlineRegister.Controls.Add(this.lbUserName);
		this.gbOnlineRegister.Controls.Add(this.tbUserName);
		this.gbOnlineRegister.Location = new System.Drawing.Point(12, 8);
		this.gbOnlineRegister.Name = "gbOnlineRegister";
		this.gbOnlineRegister.Size = new System.Drawing.Size(504, 278);
		this.gbOnlineRegister.TabIndex = 0;
		this.gbOnlineRegister.TabStop = false;
		this.gbOnlineRegister.Text = "註冊資訊";
		this.tbPhoneNumber.AutoSize = true;
		this.tbPhoneNumber.Location = new System.Drawing.Point(144, 216);
		this.tbPhoneNumber.MaxLength = 50;
		this.tbPhoneNumber.Name = "tbPhoneNumber";
		this.tbPhoneNumber.Size = new System.Drawing.Size(344, 24);
		this.tbPhoneNumber.TabIndex = 11;
		appearance7.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance7.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lbPhoneNumber.Appearance = appearance7;
		this.lbPhoneNumber.Location = new System.Drawing.Point(16, 220);
		this.lbPhoneNumber.Name = "lbPhoneNumber";
		this.lbPhoneNumber.Size = new System.Drawing.Size(128, 23);
		this.lbPhoneNumber.TabIndex = 10;
		this.lbPhoneNumber.Text = "聯絡電話：";
		this.tbDepartmentName.AutoSize = true;
		this.tbDepartmentName.Location = new System.Drawing.Point(144, 168);
		this.tbDepartmentName.MaxLength = 100;
		this.tbDepartmentName.Name = "tbDepartmentName";
		this.tbDepartmentName.Size = new System.Drawing.Size(344, 24);
		this.tbDepartmentName.TabIndex = 9;
		appearance8.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance8.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lbDepartmentName.Appearance = appearance8;
		this.lbDepartmentName.Location = new System.Drawing.Point(16, 172);
		this.lbDepartmentName.Name = "lbDepartmentName";
		this.lbDepartmentName.Size = new System.Drawing.Size(128, 23);
		this.lbDepartmentName.TabIndex = 8;
		this.lbDepartmentName.Text = "部門名稱：";
		this.tbCompanyName.AutoSize = true;
		this.tbCompanyName.Location = new System.Drawing.Point(144, 128);
		this.tbCompanyName.MaxLength = 100;
		this.tbCompanyName.Name = "tbCompanyName";
		this.tbCompanyName.Size = new System.Drawing.Size(344, 24);
		this.tbCompanyName.TabIndex = 7;
		appearance9.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance9.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lbCompanyName.Appearance = appearance9;
		this.lbCompanyName.Location = new System.Drawing.Point(16, 132);
		this.lbCompanyName.Name = "lbCompanyName";
		this.lbCompanyName.Size = new System.Drawing.Size(128, 23);
		this.lbCompanyName.TabIndex = 6;
		this.lbCompanyName.Text = "機關/公司名稱：";
		appearance10.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance10.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lbEMailAddress.Appearance = appearance10;
		this.lbEMailAddress.Location = new System.Drawing.Point(16, 92);
		this.lbEMailAddress.Name = "lbEMailAddress";
		this.lbEMailAddress.Size = new System.Drawing.Size(128, 23);
		this.lbEMailAddress.TabIndex = 5;
		this.lbEMailAddress.Text = "電子郵件信箱：";
		this.tbEMailAddress.AutoSize = true;
		this.tbEMailAddress.Location = new System.Drawing.Point(144, 88);
		this.tbEMailAddress.MaxLength = 100;
		this.tbEMailAddress.Name = "tbEMailAddress";
		this.tbEMailAddress.Size = new System.Drawing.Size(344, 24);
		this.tbEMailAddress.TabIndex = 4;
		appearance11.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance11.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lbUserName.Appearance = appearance11;
		this.lbUserName.Location = new System.Drawing.Point(16, 52);
		this.lbUserName.Name = "lbUserName";
		this.lbUserName.Size = new System.Drawing.Size(128, 23);
		this.lbUserName.TabIndex = 3;
		this.lbUserName.Text = "使用者姓名：";
		this.tbUserName.AutoSize = true;
		this.tbUserName.Location = new System.Drawing.Point(144, 48);
		this.tbUserName.MaxLength = 30;
		this.tbUserName.Name = "tbUserName";
		this.tbUserName.Size = new System.Drawing.Size(344, 24);
		this.tbUserName.TabIndex = 2;
		this.tabC.Controls.Add(this.gbManualRegistration);
		this.tabC.Controls.Add(this.panel4);
		this.tabC.Location = new System.Drawing.Point(0, 0);
		this.tabC.Name = "tabC";
		this.tabC.Size = new System.Drawing.Size(528, 342);
		this.gbManualRegistration.BackColor = System.Drawing.Color.White;
		this.gbManualRegistration.Controls.Add(this.lbSerialNumberInstruction);
		this.gbManualRegistration.Controls.Add(this.lbRegistrationCodeTitle);
		this.gbManualRegistration.Controls.Add(this.lbRegistrationCode);
		this.gbManualRegistration.Controls.Add(this.tbSerialNumber);
		this.gbManualRegistration.Controls.Add(this.lbSerialNumber);
		this.gbManualRegistration.Controls.Add(this.lbManualRegistrationInstruction);
		this.gbManualRegistration.Controls.Add(this.lbManualRegistrationTitle);
		this.gbManualRegistration.Location = new System.Drawing.Point(12, 8);
		this.gbManualRegistration.Name = "gbManualRegistration";
		this.gbManualRegistration.Size = new System.Drawing.Size(504, 278);
		this.gbManualRegistration.TabIndex = 12;
		this.gbManualRegistration.TabStop = false;
		this.gbManualRegistration.Text = "手動註冊";
		this.lbSerialNumberInstruction.Location = new System.Drawing.Point(186, 237);
		this.lbSerialNumberInstruction.Name = "lbSerialNumberInstruction";
		this.lbSerialNumberInstruction.Size = new System.Drawing.Size(100, 23);
		this.lbSerialNumberInstruction.TabIndex = 16;
		this.lbSerialNumberInstruction.Text = "(10碼)";
		this.lbRegistrationCodeTitle.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.lbRegistrationCodeTitle.Location = new System.Drawing.Point(16, 203);
		this.lbRegistrationCodeTitle.Name = "lbRegistrationCodeTitle";
		this.lbRegistrationCodeTitle.Size = new System.Drawing.Size(64, 23);
		this.lbRegistrationCodeTitle.TabIndex = 15;
		this.lbRegistrationCodeTitle.Text = "註冊碼:";
		this.lbRegistrationCode.Location = new System.Drawing.Point(86, 203);
		this.lbRegistrationCode.Name = "lbRegistrationCode";
		this.lbRegistrationCode.Size = new System.Drawing.Size(397, 23);
		this.lbRegistrationCode.TabIndex = 14;
		this.tbSerialNumber.AutoSize = true;
		this.tbSerialNumber.Location = new System.Drawing.Point(86, 233);
		this.tbSerialNumber.MaxLength = 10;
		this.tbSerialNumber.Name = "tbSerialNumber";
		this.tbSerialNumber.Size = new System.Drawing.Size(96, 24);
		this.tbSerialNumber.TabIndex = 5;
		this.lbSerialNumber.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.lbSerialNumber.Location = new System.Drawing.Point(16, 238);
		this.lbSerialNumber.Name = "lbSerialNumber";
		this.lbSerialNumber.Size = new System.Drawing.Size(64, 23);
		this.lbSerialNumber.TabIndex = 4;
		this.lbSerialNumber.Text = "序\u3000號:";
		appearance12.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.lbManualRegistrationInstruction.Appearance = appearance12;
		this.lbManualRegistrationInstruction.BackColor = System.Drawing.Color.Transparent;
		this.lbManualRegistrationInstruction.Font = new System.Drawing.Font("新細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lbManualRegistrationInstruction.InnerBorderPadding = new System.Drawing.Size(2, 2);
		this.lbManualRegistrationInstruction.Location = new System.Drawing.Point(32, 54);
		this.lbManualRegistrationInstruction.Name = "lbManualRegistrationInstruction";
		this.lbManualRegistrationInstruction.Size = new System.Drawing.Size(456, 136);
		this.lbManualRegistrationInstruction.TabIndex = 3;
		this.lbManualRegistrationInstruction.Text = "請撥客服專線：(02) 2708-8090\r\n\r\n由專人替你登錄使用者資訊，\r\n\r\n並回報您一組註冊序號，\r\n\r\n請您依回報之序號填入以下文字框內。";
		this.lbManualRegistrationTitle.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.lbManualRegistrationTitle.Location = new System.Drawing.Point(16, 24);
		this.lbManualRegistrationTitle.Name = "lbManualRegistrationTitle";
		this.lbManualRegistrationTitle.Size = new System.Drawing.Size(100, 23);
		this.lbManualRegistrationTitle.TabIndex = 0;
		this.lbManualRegistrationTitle.Text = "註冊方法:";
		this.panel4.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel4.Controls.Add(this.btnRegisterManually);
		this.panel4.Controls.Add(this.groupBox4);
		this.panel4.Controls.Add(this.btn_C_Cancel);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel4.Location = new System.Drawing.Point(0, 292);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(528, 50);
		this.panel4.TabIndex = 11;
		this.btnRegisterManually.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance13.Image = resources.GetObject("appearance13.Image");
		appearance13.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnRegisterManually.Appearance = appearance13;
		this.btnRegisterManually.BackColor = System.Drawing.SystemColors.Control;
		this.btnRegisterManually.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnRegisterManually.Font = new System.Drawing.Font("細明體", 11f);
		this.btnRegisterManually.ImageSize = new System.Drawing.Size(20, 20);
		this.btnRegisterManually.ImageTransparentColor = System.Drawing.Color.White;
		this.btnRegisterManually.Location = new System.Drawing.Point(334, 12);
		this.btnRegisterManually.Name = "btnRegisterManually";
		this.btnRegisterManually.ShowFocusRect = false;
		this.btnRegisterManually.ShowOutline = false;
		this.btnRegisterManually.Size = new System.Drawing.Size(88, 31);
		this.btnRegisterManually.SupportThemes = false;
		this.btnRegisterManually.TabIndex = 4;
		this.btnRegisterManually.Text = "註冊";
		this.btnRegisterManually.Click += new System.EventHandler(btnRegisterManually_Click);
		this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox4.Location = new System.Drawing.Point(0, 0);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(528, 8);
		this.groupBox4.TabIndex = 3;
		this.groupBox4.TabStop = false;
		this.btn_C_Cancel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance14.Image = resources.GetObject("appearance14.Image");
		appearance14.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btn_C_Cancel.Appearance = appearance14;
		this.btn_C_Cancel.BackColor = System.Drawing.SystemColors.Control;
		this.btn_C_Cancel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btn_C_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btn_C_Cancel.Font = new System.Drawing.Font("細明體", 11f);
		this.btn_C_Cancel.ImageSize = new System.Drawing.Size(20, 20);
		this.btn_C_Cancel.ImageTransparentColor = System.Drawing.Color.White;
		this.btn_C_Cancel.Location = new System.Drawing.Point(428, 12);
		this.btn_C_Cancel.Name = "btn_C_Cancel";
		this.btn_C_Cancel.ShowFocusRect = false;
		this.btn_C_Cancel.ShowOutline = false;
		this.btn_C_Cancel.Size = new System.Drawing.Size(88, 31);
		this.btn_C_Cancel.SupportThemes = false;
		this.btn_C_Cancel.TabIndex = 2;
		this.btn_C_Cancel.Text = "取消";
		this.tabD.Controls.Add(this.lbRegistrationCompleteInstruction);
		this.tabD.Controls.Add(this.lbRegistrationCompleteTitle);
		this.tabD.Controls.Add(this.panel5);
		this.tabD.Location = new System.Drawing.Point(-10000, -10000);
		this.tabD.Name = "tabD";
		this.tabD.Size = new System.Drawing.Size(528, 342);
		appearance15.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.lbRegistrationCompleteInstruction.Appearance = appearance15;
		this.lbRegistrationCompleteInstruction.BackColor = System.Drawing.Color.Transparent;
		this.lbRegistrationCompleteInstruction.Font = new System.Drawing.Font("新細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lbRegistrationCompleteInstruction.InnerBorderPadding = new System.Drawing.Size(2, 2);
		this.lbRegistrationCompleteInstruction.Location = new System.Drawing.Point(40, 64);
		this.lbRegistrationCompleteInstruction.Name = "lbRegistrationCompleteInstruction";
		this.lbRegistrationCompleteInstruction.Size = new System.Drawing.Size(456, 59);
		this.lbRegistrationCompleteInstruction.TabIndex = 14;
		this.lbRegistrationCompleteInstruction.Text = "謝謝您的合作，\r\n\r\n我們將盡力讓 PCCES 更方便使用及增進功能。";
		this.lbRegistrationCompleteTitle.BackColor = System.Drawing.Color.Transparent;
		this.lbRegistrationCompleteTitle.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.lbRegistrationCompleteTitle.Location = new System.Drawing.Point(24, 24);
		this.lbRegistrationCompleteTitle.Name = "lbRegistrationCompleteTitle";
		this.lbRegistrationCompleteTitle.Size = new System.Drawing.Size(100, 23);
		this.lbRegistrationCompleteTitle.TabIndex = 13;
		this.lbRegistrationCompleteTitle.Text = "註冊完成：";
		this.panel5.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel5.Controls.Add(this.btnFinish);
		this.panel5.Controls.Add(this.groupBox6);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel5.Location = new System.Drawing.Point(0, 292);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(528, 50);
		this.panel5.TabIndex = 12;
		this.btnFinish.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance16.Image = resources.GetObject("appearance16.Image");
		appearance16.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnFinish.Appearance = appearance16;
		this.btnFinish.BackColor = System.Drawing.SystemColors.Control;
		this.btnFinish.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnFinish.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.btnFinish.Font = new System.Drawing.Font("細明體", 11f);
		this.btnFinish.ImageSize = new System.Drawing.Size(20, 20);
		this.btnFinish.ImageTransparentColor = System.Drawing.Color.White;
		this.btnFinish.Location = new System.Drawing.Point(428, 12);
		this.btnFinish.Name = "btnFinish";
		this.btnFinish.ShowFocusRect = false;
		this.btnFinish.ShowOutline = false;
		this.btnFinish.Size = new System.Drawing.Size(88, 31);
		this.btnFinish.SupportThemes = false;
		this.btnFinish.TabIndex = 4;
		this.btnFinish.Text = "完成";
		this.groupBox6.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox6.Location = new System.Drawing.Point(0, 0);
		this.groupBox6.Name = "groupBox6";
		this.groupBox6.Size = new System.Drawing.Size(528, 8);
		this.groupBox6.TabIndex = 3;
		this.groupBox6.TabStop = false;
		appearance17.BackColor = System.Drawing.Color.White;
		this.tabControl.Appearance = appearance17;
		this.tabControl.Controls.Add(this.ultraTabSharedControlsPage1);
		this.tabControl.Controls.Add(this.tabA);
		this.tabControl.Controls.Add(this.tabB);
		this.tabControl.Controls.Add(this.tabC);
		this.tabControl.Controls.Add(this.tabD);
		this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tabControl.Location = new System.Drawing.Point(0, 0);
		this.tabControl.Name = "tabControl";
		this.tabControl.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.tabControl.Size = new System.Drawing.Size(528, 342);
		this.tabControl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
		this.tabControl.TabIndex = 12;
		ultraTab1.TabPage = this.tabA;
		ultraTab1.Text = "tab1";
		ultraTab2.TabPage = this.tabB;
		ultraTab2.Text = "tab2";
		ultraTab3.TabPage = this.tabC;
		ultraTab3.Text = "tab3";
		ultraTab4.TabPage = this.tabD;
		ultraTab4.Text = "tab4";
		this.tabControl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[4] { ultraTab1, ultraTab2, ultraTab3, ultraTab4 });
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(528, 342);
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		base.CancelButton = this.btn_A_Cancel;
		base.ClientSize = new System.Drawing.Size(528, 342);
		base.Controls.Add(this.tabControl);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.Name = "FormRegister";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "線上註冊";
		base.Load += new System.EventHandler(FormRegister_Load);
		this.tabA.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.optionRegister).EndInit();
		this.panel3.ResumeLayout(false);
		this.tabB.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.gbOnlineRegister.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.tbPhoneNumber).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbDepartmentName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbCompanyName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbEMailAddress).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tbUserName).EndInit();
		this.tabC.ResumeLayout(false);
		this.gbManualRegistration.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.tbSerialNumber).EndInit();
		this.panel4.ResumeLayout(false);
		this.tabD.ResumeLayout(false);
		this.panel5.ResumeLayout(false);
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

	public FormRegister()
	{
		InitializeComponent();
	}

	private void FormRegister_Load(object sender, EventArgs e)
	{
		tbUserName.Text = CommonMethods.GetIniValue("Register", "UserName");
		tbEMailAddress.Text = CommonMethods.GetIniValue("Register", "EMAIL");
		tbCompanyName.Text = CommonMethods.GetIniValue("Register", "CompanyName");
		tbDepartmentName.Text = CommonMethods.GetIniValue("Register", "Dept");
		tbPhoneNumber.Text = CommonMethods.GetIniValue("Register", "TEL");
		guid = Guid.NewGuid().ToString().ToUpper();
		S1 = guid.Substring(0, 4);
		S2 = guid.Substring(4, 4);
		lbRegistrationCode.Text = S1 + "-" + S2 + "-" + guid.Substring(9, 4) + "-" + guid.Substring(14, 4) + "-" + guid.Substring(19, 4) + "-" + guid.Substring(24, 6) + "-" + guid.Substring(30, 6);
	}

	private void btnRegister_Click(object sender, EventArgs e)
	{
		string warningMessage = string.Empty;
		if (tbUserName.Text.Trim() == string.Empty)
		{
			warningMessage += "使用者名稱不可空白！\n";
		}
		if (tbEMailAddress.Text.Trim() == "")
		{
			warningMessage += "電子郵件信箱不可空白！\n";
		}
		else if (!tbEMailAddress.Text.Contains("@"))
		{
			warningMessage += "電子郵件信箱應該包含 '@' 字元！";
		}
		if (warningMessage != string.Empty)
		{
			MessageBox.Show(this, warningMessage, "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			if (warningMessage.Contains("使用者"))
			{
				tbUserName.Focus();
			}
			else
			{
				tbEMailAddress.Focus();
			}
			return;
		}
		Cursor = Cursors.WaitCursor;
		string UserName = tbUserName.Text.Trim();
		string EMail = tbEMailAddress.Text.Trim();
		string CompanyName = tbCompanyName.Text.Trim();
		string Dept = tbDepartmentName.Text.Trim();
		string TEL = tbPhoneNumber.Text.Trim();
		string MAC = ArchNet.GetMacAddress();
		string IP = ArchNet.GetIPAddress();
		Update serviceRequest = new Update();
		string webServiceRoute = CommonMethods.GetIniValue("DownloadInfo", "webServiceRoute");
		if (webServiceRoute == string.Empty)
		{
			webServiceRoute = "http://bisc.archnowledge.com/pccesupdateservices/Update.asmx";
		}
		serviceRequest.Url = webServiceRoute;
		if (CommonMethods.GetIniValue("ProxyInfo", "usingProxy").Trim().ToLower() == "true")
		{
			serviceRequest.Proxy = GetProxy();
		}
		string registerID = serviceRequest.RegisterWithVersion(UserName, EMail, CompanyName, Dept, TEL, MAC, IP, PccesVersion.PccesAssemblyVersion);
		if (registerID.Trim().ToUpper() == "TR-INVALID")
		{
			MessageBox.Show(this, "這個測試用帳號已經過期，請使用個人帳號註冊。");
			tbUserName.Focus();
		}
		else if (registerID.Trim() != string.Empty)
		{
			CommonMethods.WriteIniValue("Register", "RegID", registerID);
			CommonMethods.WriteIniValue("Register", "UserName", UserName);
			CommonMethods.WriteIniValue("Register", "EMAIL", EMail);
			CommonMethods.WriteIniValue("Register", "CompanyName", CompanyName);
			CommonMethods.WriteIniValue("Register", "Dept", Dept);
			CommonMethods.WriteIniValue("Register", "TEL", TEL);
			tabD.Tab.Selected = true;
		}
		else
		{
			MessageBox.Show(this, "註冊失敗，請再重試！", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		Cursor = Cursors.Default;
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

	private void btnRegisterManually_Click(object sender, EventArgs e)
	{
		string decodedString = PubTools.KeyDec8(tbSerialNumber.Text);
		if (decodedString != S1 + S2)
		{
			MessageBox.Show(this, "序號不對，請檢查後再執行註冊。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			tbSerialNumber.Focus();
		}
		else
		{
			CommonMethods.WriteIniValue("Register", "RegID", guid);
			tabD.Tab.Selected = true;
		}
	}

	private void btn_A_Next_Click(object sender, EventArgs e)
	{
		if (optionRegister.CheckedIndex == 0)
		{
			tabB.Tab.Selected = true;
		}
		else
		{
			tabC.Tab.Selected = true;
		}
	}
}
