using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinTabControl;

namespace Archnowledge.Pcces.PccesMain.Budget.ItemNoset;

public class FormBDGT_ItemSetCheck : Form
{
	private Panel panel2;

	private UltraLabel ultraLabel7;

	private UltraLabel ultraLabel1;

	private GroupBox groupBox1;

	private UltraLabel ultraLabel2;

	private UltraTabControl Tab_Ctrl;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl Tab_A;

	private UltraTabPageControl Tab_B;

	private Panel panel4;

	private GroupBox groupBox2;

	private Panel panel1;

	private UltraLabel ultraLabel5;

	private Panel panel3;

	private GroupBox groupBox3;

	private UltraButton ultraButton1;

	private UltraButton ultraButton3;

	private UltraButton A_Btn_Cncl;

	private UltraButton A_Btn_Chg;

	private UltraButton A_Btn_OK;

	private RadioButton radioCombo2;

	private RadioButton radioSingle2;

	private GroupBox groupBox4;

	private RadioButton radioCombo1;

	private RadioButton radioSingle1;

	private Container components = null;

	public FormBDGT_ItemSetCheck()
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
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.ItemNoset.FormBDGT_ItemSetCheck));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		this.Tab_A = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel4 = new System.Windows.Forms.Panel();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.A_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.A_Btn_Chg = new Infragistics.Win.Misc.UltraButton();
		this.A_Btn_OK = new Infragistics.Win.Misc.UltraButton();
		this.panel2 = new System.Windows.Forms.Panel();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_B = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel3 = new System.Windows.Forms.Panel();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton3 = new Infragistics.Win.Misc.UltraButton();
		this.panel1 = new System.Windows.Forms.Panel();
		this.radioCombo2 = new System.Windows.Forms.RadioButton();
		this.radioSingle2 = new System.Windows.Forms.RadioButton();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_Ctrl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.radioCombo1 = new System.Windows.Forms.RadioButton();
		this.radioSingle1 = new System.Windows.Forms.RadioButton();
		this.Tab_A.SuspendLayout();
		this.panel4.SuspendLayout();
		this.panel2.SuspendLayout();
		this.groupBox4.SuspendLayout();
		this.Tab_B.SuspendLayout();
		this.panel3.SuspendLayout();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).BeginInit();
		this.Tab_Ctrl.SuspendLayout();
		base.SuspendLayout();
		this.Tab_A.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.Tab_A.Controls.Add(this.panel4);
		this.Tab_A.Controls.Add(this.panel2);
		this.Tab_A.Location = new System.Drawing.Point(0, 0);
		this.Tab_A.Name = "Tab_A";
		this.Tab_A.Size = new System.Drawing.Size(352, 231);
		this.panel4.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel4.Controls.Add(this.groupBox2);
		this.panel4.Controls.Add(this.A_Btn_Cncl);
		this.panel4.Controls.Add(this.A_Btn_Chg);
		this.panel4.Controls.Add(this.A_Btn_OK);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel4.Location = new System.Drawing.Point(0, 185);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(350, 44);
		this.panel4.TabIndex = 12;
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(350, 8);
		this.groupBox2.TabIndex = 3;
		this.groupBox2.TabStop = false;
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
		this.A_Btn_Cncl.Location = new System.Drawing.Point(226, 9);
		this.A_Btn_Cncl.Name = "A_Btn_Cncl";
		this.A_Btn_Cncl.ShowFocusRect = false;
		this.A_Btn_Cncl.ShowOutline = false;
		this.A_Btn_Cncl.Size = new System.Drawing.Size(97, 31);
		this.A_Btn_Cncl.SupportThemes = false;
		this.A_Btn_Cncl.TabIndex = 2;
		this.A_Btn_Cncl.Text = "取消";
		this.A_Btn_Chg.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Chg.Appearance = appearance2;
		this.A_Btn_Chg.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Chg.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Chg.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Chg.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Chg.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_Chg.Location = new System.Drawing.Point(125, 9);
		this.A_Btn_Chg.Name = "A_Btn_Chg";
		this.A_Btn_Chg.ShowFocusRect = false;
		this.A_Btn_Chg.ShowOutline = false;
		this.A_Btn_Chg.Size = new System.Drawing.Size(97, 31);
		this.A_Btn_Chg.SupportThemes = false;
		this.A_Btn_Chg.TabIndex = 1;
		this.A_Btn_Chg.Text = "變更設定";
		this.A_Btn_Chg.Click += new System.EventHandler(A_Btn_Chg_Click);
		this.A_Btn_OK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance3.Image = resources.GetObject("appearance3.Image");
		appearance3.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_OK.Appearance = appearance3;
		this.A_Btn_OK.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_OK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.A_Btn_OK.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_OK.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_OK.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_OK.Location = new System.Drawing.Point(25, 9);
		this.A_Btn_OK.Name = "A_Btn_OK";
		this.A_Btn_OK.ShowFocusRect = false;
		this.A_Btn_OK.ShowOutline = false;
		this.A_Btn_OK.Size = new System.Drawing.Size(97, 31);
		this.A_Btn_OK.SupportThemes = false;
		this.A_Btn_OK.TabIndex = 0;
		this.A_Btn_OK.Text = "確定執行";
		this.panel2.BackColor = System.Drawing.Color.White;
		this.panel2.Controls.Add(this.groupBox4);
		this.panel2.Controls.Add(this.ultraLabel2);
		this.panel2.Controls.Add(this.groupBox1);
		this.panel2.Controls.Add(this.ultraLabel1);
		this.panel2.Controls.Add(this.ultraLabel7);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(350, 229);
		this.panel2.TabIndex = 1;
		this.groupBox4.Controls.Add(this.radioCombo1);
		this.groupBox4.Controls.Add(this.radioSingle1);
		this.groupBox4.Location = new System.Drawing.Point(24, 35);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(312, 72);
		this.groupBox4.TabIndex = 12;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = "目前PCCES的設定值";
		appearance4.BackColor = System.Drawing.Color.White;
		appearance4.ForeColor = System.Drawing.Color.Red;
		this.ultraLabel2.Appearance = appearance4;
		this.ultraLabel2.Font = new System.Drawing.Font("新細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel2.Location = new System.Drawing.Point(56, 134);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(288, 40);
		this.ultraLabel2.TabIndex = 11;
		this.ultraLabel2.Text = "如果目前的設定值與你的預算或標單不同，會造成單價分析表輸出錯誤。若您要修改，請按[變更設定]的按鈕。";
		this.groupBox1.Location = new System.Drawing.Point(8, 113);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(328, 8);
		this.groupBox1.TabIndex = 8;
		this.groupBox1.TabStop = false;
		appearance5.BackColor = System.Drawing.Color.White;
		this.ultraLabel1.Appearance = appearance5;
		this.ultraLabel1.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel1.Location = new System.Drawing.Point(8, 134);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(48, 20);
		this.ultraLabel1.TabIndex = 7;
		this.ultraLabel1.Text = "說明:";
		appearance6.BackColor = System.Drawing.Color.White;
		this.ultraLabel7.Appearance = appearance6;
		this.ultraLabel7.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel7.Location = new System.Drawing.Point(8, 8);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(336, 20);
		this.ultraLabel7.TabIndex = 4;
		this.ultraLabel7.Text = "執行輸出前，請先檢查下列設定值是否正確";
		this.ultraLabel7.Click += new System.EventHandler(ultraLabel7_Click);
		this.Tab_B.Controls.Add(this.panel3);
		this.Tab_B.Controls.Add(this.panel1);
		this.Tab_B.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_B.Name = "Tab_B";
		this.Tab_B.Size = new System.Drawing.Size(352, 231);
		this.panel3.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel3.Controls.Add(this.groupBox3);
		this.panel3.Controls.Add(this.ultraButton1);
		this.panel3.Controls.Add(this.ultraButton3);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel3.Location = new System.Drawing.Point(0, 187);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(352, 44);
		this.panel3.TabIndex = 13;
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox3.Location = new System.Drawing.Point(0, 0);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(352, 8);
		this.groupBox3.TabIndex = 3;
		this.groupBox3.TabStop = false;
		this.ultraButton1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance7.Image = resources.GetObject("appearance7.Image");
		appearance7.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton1.Appearance = appearance7;
		this.ultraButton1.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ultraButton1.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton1.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton1.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton1.Location = new System.Drawing.Point(176, 9);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.ShowFocusRect = false;
		this.ultraButton1.ShowOutline = false;
		this.ultraButton1.Size = new System.Drawing.Size(97, 31);
		this.ultraButton1.SupportThemes = false;
		this.ultraButton1.TabIndex = 2;
		this.ultraButton1.Text = "取消";
		this.ultraButton3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance8.Image = resources.GetObject("appearance8.Image");
		appearance8.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton3.Appearance = appearance8;
		this.ultraButton3.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton3.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton3.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton3.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton3.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton3.Location = new System.Drawing.Point(72, 9);
		this.ultraButton3.Name = "ultraButton3";
		this.ultraButton3.ShowFocusRect = false;
		this.ultraButton3.ShowOutline = false;
		this.ultraButton3.Size = new System.Drawing.Size(97, 31);
		this.ultraButton3.SupportThemes = false;
		this.ultraButton3.TabIndex = 0;
		this.ultraButton3.Text = "確定";
		this.ultraButton3.Click += new System.EventHandler(ultraButton3_Click);
		this.panel1.BackColor = System.Drawing.Color.White;
		this.panel1.Controls.Add(this.radioCombo2);
		this.panel1.Controls.Add(this.radioSingle2);
		this.panel1.Controls.Add(this.ultraLabel5);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(352, 231);
		this.panel1.TabIndex = 2;
		this.radioCombo2.Location = new System.Drawing.Point(25, 66);
		this.radioCombo2.Name = "radioCombo2";
		this.radioCombo2.TabIndex = 10;
		this.radioCombo2.Text = "組合編號";
		this.radioSingle2.Checked = true;
		this.radioSingle2.Location = new System.Drawing.Point(25, 41);
		this.radioSingle2.Name = "radioSingle2";
		this.radioSingle2.Size = new System.Drawing.Size(215, 24);
		this.radioSingle2.TabIndex = 9;
		this.radioSingle2.TabStop = true;
		this.radioSingle2.Text = "獨立編號";
		appearance9.BackColor = System.Drawing.Color.White;
		this.ultraLabel5.Appearance = appearance9;
		this.ultraLabel5.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel5.Location = new System.Drawing.Point(8, 8);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(336, 20);
		this.ultraLabel5.TabIndex = 4;
		this.ultraLabel5.Text = "請變更下面的設定值";
		this.Tab_Ctrl.Controls.Add(this.ultraTabSharedControlsPage1);
		this.Tab_Ctrl.Controls.Add(this.Tab_A);
		this.Tab_Ctrl.Controls.Add(this.Tab_B);
		this.Tab_Ctrl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Tab_Ctrl.Location = new System.Drawing.Point(0, 0);
		this.Tab_Ctrl.Name = "Tab_Ctrl";
		this.Tab_Ctrl.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.Tab_Ctrl.Size = new System.Drawing.Size(352, 231);
		this.Tab_Ctrl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
		this.Tab_Ctrl.TabIndex = 12;
		ultraTab1.TabPage = this.Tab_A;
		ultraTab1.Text = "tab1";
		ultraTab2.TabPage = this.Tab_B;
		ultraTab2.Text = "tab2";
		this.Tab_Ctrl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[2] { ultraTab1, ultraTab2 });
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(352, 231);
		this.radioCombo1.Location = new System.Drawing.Point(16, 43);
		this.radioCombo1.Name = "radioCombo1";
		this.radioCombo1.TabIndex = 12;
		this.radioCombo1.Text = "組合編號";
		this.radioSingle1.Checked = true;
		this.radioSingle1.Location = new System.Drawing.Point(16, 19);
		this.radioSingle1.Name = "radioSingle1";
		this.radioSingle1.Size = new System.Drawing.Size(215, 24);
		this.radioSingle1.TabIndex = 11;
		this.radioSingle1.TabStop = true;
		this.radioSingle1.Text = "獨立編號";
		base.AcceptButton = this.A_Btn_OK;
		this.AutoScaleBaseSize = new System.Drawing.Size(6, 18);
		this.BackColor = System.Drawing.Color.White;
		base.CancelButton = this.A_Btn_Cncl;
		base.ClientSize = new System.Drawing.Size(352, 231);
		base.Controls.Add(this.Tab_Ctrl);
		this.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormBDGT_ItemSetCheck";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "項次編碼設定檢查";
		base.Load += new System.EventHandler(FormBDGT_ItemSetCheck_Load);
		this.Tab_A.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.groupBox4.ResumeLayout(false);
		this.Tab_B.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Tab_Ctrl).EndInit();
		this.Tab_Ctrl.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	private void ultraLabel7_Click(object sender, EventArgs e)
	{
	}

	private void A_Btn_Chg_Click(object sender, EventArgs e)
	{
		Tab_B.Tab.Selected = true;
	}

	private void FormBDGT_ItemSetCheck_Load(object sender, EventArgs e)
	{
		string sAssem = CommonMethods.GetIniValue("AutoItemNo", "AssemType");
		if (sAssem == "1")
		{
			radioSingle1.Checked = true;
			radioSingle2.Checked = true;
		}
		else
		{
			radioCombo1.Checked = true;
			radioCombo2.Checked = true;
		}
	}

	private void ultraButton3_Click(object sender, EventArgs e)
	{
		if (radioSingle2.Checked)
		{
			CommonMethods.WriteIniValue("AutoItemNo", "AssemType", "1");
		}
		else
		{
			CommonMethods.WriteIniValue("AutoItemNo", "AssemType", "2");
		}
		base.DialogResult = DialogResult.OK;
	}
}
