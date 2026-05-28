using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Archnowledge.Pcces.PccesMain.MrsBase;

public class FormProgress : Form
{
	private IContainer components = null;

	private ProgressBar progressBar1;

	private Label label1;

	private string sMessage = "";

	private int iMin;

	private int iMax;

	public int _Min
	{
		set
		{
			iMin = value;
		}
	}

	public int _Max
	{
		get
		{
			return iMax;
		}
		set
		{
			iMax = value;
		}
	}

	public string Message
	{
		set
		{
			sMessage = value;
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
		this.progressBar1 = new System.Windows.Forms.ProgressBar();
		this.label1 = new System.Windows.Forms.Label();
		base.SuspendLayout();
		this.progressBar1.Location = new System.Drawing.Point(23, 64);
		this.progressBar1.Name = "progressBar1";
		this.progressBar1.Size = new System.Drawing.Size(244, 23);
		this.progressBar1.TabIndex = 0;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.label1.Location = new System.Drawing.Point(20, 32);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(92, 15);
		this.label1.TabIndex = 1;
		this.label1.Text = "資料整理中!!";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(292, 114);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.progressBar1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Name = "FormProgress";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "FormProgress";
		base.TopMost = true;
		base.Load += new System.EventHandler(FormProgress_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public FormProgress()
	{
		InitializeComponent();
	}

	public void SetMessage(string Mess)
	{
		label1.Text = Mess;
		label1.Refresh();
		Application.DoEvents();
	}

	public void SetMax(int MaxValue)
	{
		progressBar1.Maximum = MaxValue;
	}

	public void SetProgressValue(int iVal)
	{
		progressBar1.Value = iVal;
	}

	private void FormProgress_Load(object sender, EventArgs e)
	{
		label1.Text = sMessage;
		progressBar1.Minimum = iMin;
		progressBar1.Maximum = iMax;
		progressBar1.Value = 0;
	}
}
