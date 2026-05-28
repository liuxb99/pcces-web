using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormAskQuestion : Form
{
	private IContainer components = null;

	private Panel panel3;

	private GroupBox groupBox3;

	private UltraButton BtnCancel;

	private UltraButton BtnOK;

	private Label label1;

	private Label label2;

	private Label lbl_ProjectCode;

	private string F_SelectedProjectCode = "";

	public string _SelectedProjectCode
	{
		get
		{
			return F_SelectedProjectCode;
		}
		set
		{
			F_SelectedProjectCode = value;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormAskQuestion));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		this.panel3 = new System.Windows.Forms.Panel();
		this.BtnCancel = new Infragistics.Win.Misc.UltraButton();
		this.BtnOK = new Infragistics.Win.Misc.UltraButton();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.lbl_ProjectCode = new System.Windows.Forms.Label();
		this.panel3.SuspendLayout();
		base.SuspendLayout();
		this.panel3.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel3.Controls.Add(this.BtnCancel);
		this.panel3.Controls.Add(this.BtnOK);
		this.panel3.Controls.Add(this.groupBox3);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel3.Location = new System.Drawing.Point(0, 163);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(488, 50);
		this.panel3.TabIndex = 10;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnCancel.Appearance = appearance1;
		this.BtnCancel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.BtnCancel.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnCancel.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnCancel.Location = new System.Drawing.Point(245, 13);
		this.BtnCancel.Name = "BtnCancel";
		this.BtnCancel.ShowFocusRect = false;
		this.BtnCancel.ShowOutline = false;
		this.BtnCancel.Size = new System.Drawing.Size(88, 31);
		this.BtnCancel.SupportThemes = false;
		this.BtnCancel.TabIndex = 7;
		this.BtnCancel.Text = "否";
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnOK.Appearance = appearance2;
		this.BtnOK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnOK.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnOK.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnOK.Location = new System.Drawing.Point(155, 13);
		this.BtnOK.Name = "BtnOK";
		this.BtnOK.ShowFocusRect = false;
		this.BtnOK.ShowOutline = false;
		this.BtnOK.Size = new System.Drawing.Size(88, 31);
		this.BtnOK.SupportThemes = false;
		this.BtnOK.TabIndex = 6;
		this.BtnOK.Text = "是";
		this.BtnOK.Click += new System.EventHandler(BtnOK_Click);
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox3.Location = new System.Drawing.Point(0, 0);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(488, 8);
		this.groupBox3.TabIndex = 3;
		this.groupBox3.TabStop = false;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(34, 29);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(144, 19);
		this.label1.TabIndex = 11;
		this.label1.Text = "選定的標單案號為：";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(34, 64);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(369, 76);
		this.label2.TabIndex = 12;
		this.label2.Text = "再次確定：\r\n確定繼續執行的話，將覆蓋本契約畫面中之所有資料！\r\n\r\n是否執行?";
		this.lbl_ProjectCode.AutoSize = true;
		this.lbl_ProjectCode.Font = new System.Drawing.Font("微軟正黑體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.lbl_ProjectCode.ForeColor = System.Drawing.Color.Red;
		this.lbl_ProjectCode.Location = new System.Drawing.Point(184, 29);
		this.lbl_ProjectCode.Name = "lbl_ProjectCode";
		this.lbl_ProjectCode.Size = new System.Drawing.Size(53, 19);
		this.lbl_ProjectCode.TabIndex = 13;
		this.lbl_ProjectCode.Text = "label3";
		base.AcceptButton = this.BtnOK;
		base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 19f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(255, 255, 128);
		base.CancelButton = this.BtnCancel;
		base.ClientSize = new System.Drawing.Size(488, 213);
		base.Controls.Add(this.lbl_ProjectCode);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.panel3);
		this.Font = new System.Drawing.Font("微軟正黑體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		base.Name = "FormAskQuestion";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "詢問";
		base.Load += new System.EventHandler(FormAskQuestion_Load);
		this.panel3.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public FormAskQuestion()
	{
		InitializeComponent();
	}

	private void FormAskQuestion_Load(object sender, EventArgs e)
	{
		lbl_ProjectCode.Text = F_SelectedProjectCode;
	}

	private void BtnOK_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.OK;
	}
}
