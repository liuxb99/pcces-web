using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.PccesMain.Library;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace Archnowledge.Pcces.PccesMain;

public class FormModuleSetup : Form
{
	private IContainer components = null;

	private Label label2;

	private CheckBox cbBudget;

	private Panel panel1;

	private Panel panel3;

	private Label label4;

	private Label label3;

	private Label label7;

	private Label label8;

	private CheckBox cbCommon;

	private Label label5;

	private Label label6;

	private CheckBox cbBid;

	private UltraButton BtnOK;

	private Label label1;

	private Panel panel2;

	private CheckBox cbContract;

	private Label label10;

	private Label label9;

	public FormModuleSetup()
	{
		InitializeComponent();
	}

	private void FormModuleSetup_Load(object sender, EventArgs e)
	{
		ModuleManager oManager = new ModuleManager();
		cbBudget.Checked = oManager.EnableBudgetMdoule;
		cbBid.Checked = oManager.EnableBidMdoule;
		cbCommon.Checked = oManager.EnableCommonMdoule;
		cbContract.Checked = oManager.EnableContractModule;
	}

	private void BtnOK_Click(object sender, EventArgs e)
	{
		ModuleManager oManager = new ModuleManager();
		oManager.EnableBudgetMdoule = cbBudget.Checked;
		oManager.EnableBidMdoule = cbBid.Checked;
		oManager.EnableCommonMdoule = cbCommon.Checked;
		oManager.EnableContractModule = cbContract.Checked;
		oManager.IsFirstRun = false;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.FormModuleSetup));
		this.label2 = new System.Windows.Forms.Label();
		this.cbBudget = new System.Windows.Forms.CheckBox();
		this.panel1 = new System.Windows.Forms.Panel();
		this.panel3 = new System.Windows.Forms.Panel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.label7 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.cbCommon = new System.Windows.Forms.CheckBox();
		this.label5 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.cbBid = new System.Windows.Forms.CheckBox();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.BtnOK = new Infragistics.Win.Misc.UltraButton();
		this.label1 = new System.Windows.Forms.Label();
		this.cbContract = new System.Windows.Forms.CheckBox();
		this.label9 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.panel3.SuspendLayout();
		base.SuspendLayout();
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(8, 12);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(77, 12);
		this.label2.TabIndex = 34;
		this.label2.Text = "設定常用模組";
		this.cbBudget.AutoSize = true;
		this.cbBudget.Location = new System.Drawing.Point(10, 63);
		this.cbBudget.Name = "cbBudget";
		this.cbBudget.Size = new System.Drawing.Size(96, 16);
		this.cbBudget.TabIndex = 35;
		this.cbBudget.Text = "預算編製模組";
		this.cbBudget.UseVisualStyleBackColor = true;
		this.panel1.BackColor = System.Drawing.SystemColors.ButtonShadow;
		this.panel1.Location = new System.Drawing.Point(10, 166);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(749, 10);
		this.panel1.TabIndex = 36;
		this.panel3.BackColor = System.Drawing.SystemColors.ControlLightLight;
		this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel3.Controls.Add(this.cbContract);
		this.panel3.Controls.Add(this.panel2);
		this.panel3.Controls.Add(this.label7);
		this.panel3.Controls.Add(this.label8);
		this.panel3.Controls.Add(this.label2);
		this.panel3.Controls.Add(this.cbCommon);
		this.panel3.Controls.Add(this.label5);
		this.panel3.Controls.Add(this.label6);
		this.panel3.Controls.Add(this.cbBid);
		this.panel3.Controls.Add(this.label10);
		this.panel3.Controls.Add(this.label9);
		this.panel3.Controls.Add(this.label4);
		this.panel3.Controls.Add(this.label3);
		this.panel3.Controls.Add(this.cbBudget);
		this.panel3.Controls.Add(this.panel1);
		this.panel3.Location = new System.Drawing.Point(17, 83);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(789, 328);
		this.panel3.TabIndex = 37;
		this.panel2.BackColor = System.Drawing.SystemColors.ButtonShadow;
		this.panel2.Location = new System.Drawing.Point(10, 38);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(749, 10);
		this.panel2.TabIndex = 37;
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(637, 221);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(113, 36);
		this.label7.TabIndex = 44;
		this.label7.Text = "1. 基本資料庫維護\r\n2. 歷史工程單位造價\r\n3.  經費審查比對";
		this.label8.AutoSize = true;
		this.label8.ForeColor = System.Drawing.Color.Red;
		this.label8.Location = new System.Drawing.Point(566, 221);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(53, 12);
		this.label8.TabIndex = 43;
		this.label8.Text = "提供功能";
		this.cbCommon.AutoSize = true;
		this.cbCommon.Location = new System.Drawing.Point(434, 202);
		this.cbCommon.Name = "cbCommon";
		this.cbCommon.Size = new System.Drawing.Size(72, 16);
		this.cbCommon.TabIndex = 42;
		this.cbCommon.Text = "共用模組";
		this.cbCommon.UseVisualStyleBackColor = true;
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(637, 82);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(77, 24);
		this.label5.TabIndex = 41;
		this.label5.Text = "1. 標單轉入\r\n2. 投標單填寫\r\n";
		this.label6.AutoSize = true;
		this.label6.ForeColor = System.Drawing.Color.Red;
		this.label6.Location = new System.Drawing.Point(566, 82);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(53, 12);
		this.label6.TabIndex = 40;
		this.label6.Text = "提供功能";
		this.cbBid.AutoSize = true;
		this.cbBid.Location = new System.Drawing.Point(434, 63);
		this.cbBid.Name = "cbBid";
		this.cbBid.Size = new System.Drawing.Size(96, 16);
		this.cbBid.TabIndex = 39;
		this.cbBid.Text = "投標編製模組";
		this.cbBid.UseVisualStyleBackColor = true;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(213, 82);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(77, 48);
		this.label4.TabIndex = 38;
		this.label4.Text = "1. 專案目錄\r\n2. 預算書編製\r\n3. 契約暨估驗\r\n4. 外掛程式";
		this.label3.AutoSize = true;
		this.label3.ForeColor = System.Drawing.Color.Red;
		this.label3.Location = new System.Drawing.Point(142, 82);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(53, 12);
		this.label3.TabIndex = 37;
		this.label3.Text = "提供功能";
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnOK.Appearance = appearance1;
		this.BtnOK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.BtnOK.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnOK.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnOK.Location = new System.Drawing.Point(718, 427);
		this.BtnOK.Name = "BtnOK";
		this.BtnOK.ShowFocusRect = false;
		this.BtnOK.ShowOutline = false;
		this.BtnOK.Size = new System.Drawing.Size(88, 31);
		this.BtnOK.SupportThemes = false;
		this.BtnOK.TabIndex = 38;
		this.BtnOK.Text = "存檔";
		this.BtnOK.Click += new System.EventHandler(BtnOK_Click);
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("新細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.label1.Location = new System.Drawing.Point(15, 23);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(374, 36);
		this.label1.TabIndex = 0;
		this.label1.Text = "提示：\r\n           1. 當你做完設定值變更之後，記得要按一下右下角的「存檔」。\r\n           2. 設定完成之後，你仍可以至系統維護中重新設定。";
		this.cbContract.AutoSize = true;
		this.cbContract.Location = new System.Drawing.Point(10, 202);
		this.cbContract.Name = "cbContract";
		this.cbContract.Size = new System.Drawing.Size(108, 16);
		this.cbContract.TabIndex = 45;
		this.cbContract.Text = "契約暨估驗模組";
		this.cbContract.UseVisualStyleBackColor = true;
		this.label9.AutoSize = true;
		this.label9.ForeColor = System.Drawing.Color.Red;
		this.label9.Location = new System.Drawing.Point(142, 221);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(53, 12);
		this.label9.TabIndex = 37;
		this.label9.Text = "提供功能";
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(213, 221);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(65, 60);
		this.label10.TabIndex = 38;
		this.label10.Text = "1. 契約編製\r\n2. 估驗計價\r\n3. 契約變更\r\n4. 結算\r\n5. 決算";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.ClientSize = new System.Drawing.Size(818, 475);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.BtnOK);
		base.Controls.Add(this.panel3);
		base.Name = "FormModuleSetup";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "常用模組設定";
		base.Load += new System.EventHandler(FormModuleSetup_Load);
		this.panel3.ResumeLayout(false);
		this.panel3.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
