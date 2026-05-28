using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormBudgetExp_Wzd_SelfExamDiaglog : Form
{
	private string sMessage = "";

	private IContainer components = null;

	private PictureBox pictureBox1;

	private Button BtnSkip;

	private Button BtnCancel;

	private Label label1;

	private Panel panel1;

	public string _Message
	{
		set
		{
			sMessage = value;
		}
	}

	public FormBudgetExp_Wzd_SelfExamDiaglog()
	{
		InitializeComponent();
	}

	private void FormBudgetExp_Wzd_SelfExamDiaglog_Load(object sender, EventArgs e)
	{
		label1.Text = sMessage;
	}

	private void BtnCancel_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.Cancel;
	}

	private void BtnSkip_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.OK;
	}

	private void FormBudgetExp_Wzd_SelfExamDiaglog_Activated(object sender, EventArgs e)
	{
		BtnCancel.Focus();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormBudgetExp_Wzd_SelfExamDiaglog));
		this.BtnCancel = new System.Windows.Forms.Button();
		this.BtnSkip = new System.Windows.Forms.Button();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.label1 = new System.Windows.Forms.Label();
		this.panel1 = new System.Windows.Forms.Panel();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.BtnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnCancel.Image = (System.Drawing.Image)resources.GetObject("BtnCancel.Image");
		this.BtnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.BtnCancel.Location = new System.Drawing.Point(521, 12);
		this.BtnCancel.Margin = new System.Windows.Forms.Padding(4);
		this.BtnCancel.Name = "BtnCancel";
		this.BtnCancel.Size = new System.Drawing.Size(118, 37);
		this.BtnCancel.TabIndex = 2;
		this.BtnCancel.Text = "返回";
		this.BtnCancel.UseVisualStyleBackColor = true;
		this.BtnCancel.Click += new System.EventHandler(BtnCancel_Click);
		this.BtnSkip.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.BtnSkip.Image = (System.Drawing.Image)resources.GetObject("BtnSkip.Image");
		this.BtnSkip.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.BtnSkip.Location = new System.Drawing.Point(227, 12);
		this.BtnSkip.Margin = new System.Windows.Forms.Padding(4);
		this.BtnSkip.Name = "BtnSkip";
		this.BtnSkip.Size = new System.Drawing.Size(282, 37);
		this.BtnSkip.TabIndex = 1;
		this.BtnSkip.Text = "我有把握不需檢查，繼續";
		this.BtnSkip.UseVisualStyleBackColor = true;
		this.BtnSkip.Click += new System.EventHandler(BtnSkip_Click);
		this.pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
		this.pictureBox1.Location = new System.Drawing.Point(41, 45);
		this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(48, 48);
		this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
		this.pictureBox1.TabIndex = 0;
		this.pictureBox1.TabStop = false;
		this.label1.Location = new System.Drawing.Point(114, 10);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(505, 105);
		this.label1.TabIndex = 3;
		this.label1.Text = "請先執行預算書編輯【工具】→【自主檢查】以免疏忽致造成流癈標！";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.panel1.BackColor = System.Drawing.SystemColors.Control;
		this.panel1.Controls.Add(this.BtnSkip);
		this.panel1.Controls.Add(this.BtnCancel);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 127);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(649, 58);
		this.panel1.TabIndex = 4;
		base.AcceptButton = this.BtnCancel;
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 15f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.White;
		base.ClientSize = new System.Drawing.Size(649, 185);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.pictureBox1);
		this.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.Margin = new System.Windows.Forms.Padding(4);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormBudgetExp_Wzd_SelfExamDiaglog";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "警示";
		base.Load += new System.EventHandler(FormBudgetExp_Wzd_SelfExamDiaglog_Load);
		base.Activated += new System.EventHandler(FormBudgetExp_Wzd_SelfExamDiaglog_Activated);
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		this.panel1.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
