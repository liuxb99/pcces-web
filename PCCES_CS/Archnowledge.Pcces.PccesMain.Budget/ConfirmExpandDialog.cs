using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class ConfirmExpandDialog : Form
{
	private IContainer components = null;

	private Button btnCancel;

	private Button btnOK;

	private Label label1;

	private CheckBox CBox_Ok;

	public ConfirmExpandDialog()
	{
		InitializeComponent();
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
	}

	private void CBox_Ok_CheckStateChanged(object sender, EventArgs e)
	{
		btnOK.Enabled = true;
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
		this.btnCancel = new System.Windows.Forms.Button();
		this.btnOK = new System.Windows.Forms.Button();
		this.label1 = new System.Windows.Forms.Label();
		this.CBox_Ok = new System.Windows.Forms.CheckBox();
		base.SuspendLayout();
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.Location = new System.Drawing.Point(212, 93);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(75, 23);
		this.btnCancel.TabIndex = 0;
		this.btnCancel.Text = "否";
		this.btnCancel.UseVisualStyleBackColor = true;
		this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
		this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.btnOK.Enabled = false;
		this.btnOK.Location = new System.Drawing.Point(12, 93);
		this.btnOK.Name = "btnOK";
		this.btnOK.Size = new System.Drawing.Size(75, 23);
		this.btnOK.TabIndex = 1;
		this.btnOK.Text = "是";
		this.btnOK.UseVisualStyleBackColor = true;
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(21, 24);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(257, 12);
		this.label1.TabIndex = 3;
		this.label1.Text = "執行後無法對此次預算變更作【解鎖】的動作！";
		this.CBox_Ok.AutoSize = true;
		this.CBox_Ok.Location = new System.Drawing.Point(101, 56);
		this.CBox_Ok.Name = "CBox_Ok";
		this.CBox_Ok.Size = new System.Drawing.Size(112, 16);
		this.CBox_Ok.TabIndex = 4;
		this.CBox_Ok.Text = "我要展開明細表!";
		this.CBox_Ok.UseVisualStyleBackColor = true;
		this.CBox_Ok.CheckStateChanged += new System.EventHandler(CBox_Ok_CheckStateChanged);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(299, 128);
		base.Controls.Add(this.CBox_Ok);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.btnOK);
		base.Controls.Add(this.btnCancel);
		base.Name = "ConfirmExpandDialog";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "詢問";
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
