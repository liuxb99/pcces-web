using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using C1.Win.C1Input;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinProgressBar;

namespace Archnowledge.Pcces.PccesMain.SysMaintain;

public class FormSys_G_Info1 : Form
{
	private Panel panel1;

	private C1PictureBox c1PictureBox1;

	private UltraLabel ultraLabel1;

	private IContainer components = null;

	private UltraProgressBar ProgressBar1;

	private UltraLabel lbMessage;

	public string _InfoString
	{
		get
		{
			return ultraLabel1.Text;
		}
		set
		{
			ultraLabel1.Text = value;
		}
	}

	public int _MaxValue
	{
		get
		{
			return ProgressBar1.Maximum;
		}
		set
		{
			ProgressBar1.Maximum = value;
		}
	}

	public int _MinValue
	{
		get
		{
			return ProgressBar1.Minimum;
		}
		set
		{
			ProgressBar1.Minimum = value;
		}
	}

	public int _ProgressValue
	{
		get
		{
			return ProgressBar1.Value;
		}
		set
		{
			if (ProgressBar1.Minimum <= value && value <= ProgressBar1.Maximum)
			{
				ProgressBar1.Value = value;
			}
			Application.DoEvents();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.SysMaintain.FormSys_G_Info1));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		this.panel1 = new System.Windows.Forms.Panel();
		this.lbMessage = new Infragistics.Win.Misc.UltraLabel();
		this.ProgressBar1 = new Infragistics.Win.UltraWinProgressBar.UltraProgressBar();
		this.c1PictureBox1 = new C1.Win.C1Input.C1PictureBox();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.c1PictureBox1).BeginInit();
		base.SuspendLayout();
		this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel1.Controls.Add(this.lbMessage);
		this.panel1.Controls.Add(this.ProgressBar1);
		this.panel1.Controls.Add(this.c1PictureBox1);
		this.panel1.Controls.Add(this.ultraLabel1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(336, 212);
		this.panel1.TabIndex = 0;
		this.lbMessage.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance1.ForeColor = System.Drawing.Color.Black;
		appearance1.TextHAlign = Infragistics.Win.HAlign.Center;
		this.lbMessage.Appearance = appearance1;
		this.lbMessage.BackColor = System.Drawing.Color.White;
		this.lbMessage.Location = new System.Drawing.Point(8, 41);
		this.lbMessage.Name = "lbMessage";
		this.lbMessage.Size = new System.Drawing.Size(320, 50);
		this.lbMessage.TabIndex = 27;
		this.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.ProgressBar1.Location = new System.Drawing.Point(0, 191);
		this.ProgressBar1.Name = "ProgressBar1";
		this.ProgressBar1.Size = new System.Drawing.Size(334, 19);
		this.ProgressBar1.SupportThemes = false;
		this.ProgressBar1.TabIndex = 26;
		this.ProgressBar1.Text = "[Formatted]";
		this.c1PictureBox1.Image = (System.Drawing.Image)resources.GetObject("c1PictureBox1.Image");
		this.c1PictureBox1.ImmediateUpdate = true;
		this.c1PictureBox1.Location = new System.Drawing.Point(122, 97);
		this.c1PictureBox1.Name = "c1PictureBox1";
		this.c1PictureBox1.Size = new System.Drawing.Size(100, 87);
		this.c1PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.c1PictureBox1.TabIndex = 25;
		this.c1PictureBox1.TabStop = false;
		this.ultraLabel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance2.ForeColor = System.Drawing.Color.Black;
		appearance2.TextHAlign = Infragistics.Win.HAlign.Center;
		this.ultraLabel1.Appearance = appearance2;
		this.ultraLabel1.BackColor = System.Drawing.Color.White;
		this.ultraLabel1.Location = new System.Drawing.Point(8, 16);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(320, 19);
		this.ultraLabel1.TabIndex = 24;
		this.ultraLabel1.Text = "資料庫處理中，請稍候...";
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		this.BackColor = System.Drawing.Color.White;
		base.ClientSize = new System.Drawing.Size(336, 212);
		base.Controls.Add(this.panel1);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Name = "FormSys_G_Info1";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "請稍候";
		base.Load += new System.EventHandler(FormSys_G_Info1_Load);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormSys_G_Info1_FormClosing);
		this.panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.c1PictureBox1).EndInit();
		base.ResumeLayout(false);
	}

	public FormSys_G_Info1()
	{
		InitializeComponent();
	}

	private void FormSys_G_Info1_Load(object sender, EventArgs e)
	{
		Cursor = Cursors.WaitCursor;
	}

	private void FormSys_G_Info1_FormClosing(object sender, FormClosingEventArgs e)
	{
		c1PictureBox1.Image = null;
		Cursor = Cursors.Default;
	}

	public void SetValue(string Message, int Progress)
	{
		lbMessage.Text = Message;
		_ProgressValue = Progress;
		Application.DoEvents();
	}

	public void SetValue(string Message)
	{
		lbMessage.Text = Message;
		Application.DoEvents();
	}
}
