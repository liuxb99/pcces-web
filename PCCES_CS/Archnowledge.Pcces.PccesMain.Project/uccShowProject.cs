using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Archnowledge.Pcces.PccesMain.Project;

public class uccShowProject : UserControl
{
	private IContainer components = null;

	private CheckBox chbid;

	private CheckBox chbud;

	private Panel panel1;

	public bool ShowBudType4 => chbud.Checked;

	public bool ShowBidType3 => chbid.Checked;

	public uccShowProject()
	{
		InitializeComponent();
	}

	private void CheckedChanged(object sender, EventArgs e)
	{
		(base.ParentForm as FormProject).BindDataToGrid();
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
		this.chbid = new System.Windows.Forms.CheckBox();
		this.chbud = new System.Windows.Forms.CheckBox();
		this.panel1 = new System.Windows.Forms.Panel();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.chbid.AutoSize = true;
		this.chbid.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.chbid.Location = new System.Drawing.Point(113, 8);
		this.chbid.Name = "chbid";
		this.chbid.Size = new System.Drawing.Size(108, 16);
		this.chbid.TabIndex = 1;
		this.chbid.Text = "顯示廠商報價單";
		this.chbid.UseVisualStyleBackColor = false;
		this.chbid.CheckedChanged += new System.EventHandler(CheckedChanged);
		this.chbud.AutoSize = true;
		this.chbud.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.chbud.Location = new System.Drawing.Point(11, 8);
		this.chbud.Name = "chbud";
		this.chbud.Size = new System.Drawing.Size(96, 16);
		this.chbud.TabIndex = 3;
		this.chbud.Text = "顯示發包預算";
		this.chbud.UseVisualStyleBackColor = false;
		this.chbud.CheckedChanged += new System.EventHandler(CheckedChanged);
		this.panel1.BackColor = System.Drawing.Color.FromArgb(153, 204, 102);
		this.panel1.Controls.Add(this.chbid);
		this.panel1.Controls.Add(this.chbud);
		this.panel1.Location = new System.Drawing.Point(3, 3);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(237, 31);
		this.panel1.TabIndex = 4;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Control;
		base.Controls.Add(this.panel1);
		base.Name = "uccShowProject";
		base.Size = new System.Drawing.Size(244, 36);
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		base.ResumeLayout(false);
	}
}
