using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.STDClass;
using Infragistics.Win.Misc;

namespace Archnowledge.Pcces.PccesMain;

public class FormSplash : Form
{
	private IContainer components = null;

	private Panel panel1;

	private UltraLabel lblVersion;

	private OnlineList onlineList1;

	private Label label1;

	public FormSplash()
	{
		InitializeComponent();
	}

	private void FormSplash_Load(object sender, EventArgs e)
	{
		Cursor = Cursors.WaitCursor;
		lblVersion.Text = "Build: Win" + PccesVersion.PccesAssemblyVersion;
		(base.Owner as frmPccesMain)._PreConnectOK = true;
	}

	private void InitializeComponent()
	{
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.FormSplash));
		this.panel1 = new System.Windows.Forms.Panel();
		this.label1 = new System.Windows.Forms.Label();
		this.onlineList1 = new Archnowledge.Pcces.PccesMain.ArchControls.OnlineList();
		this.lblVersion = new Infragistics.Win.Misc.UltraLabel();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.panel1.BackColor = System.Drawing.Color.White;
		this.panel1.BackgroundImage = (System.Drawing.Image)resources.GetObject("panel1.BackgroundImage");
		this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel1.Controls.Add(this.label1);
		this.panel1.Controls.Add(this.onlineList1);
		this.panel1.Controls.Add(this.lblVersion);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(599, 399);
		this.panel1.TabIndex = 0;
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Arial", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.White;
		this.label1.Location = new System.Drawing.Point(157, 218);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(68, 19);
		this.label1.TabIndex = 3;
		this.label1.Text = "Win 4.3 ";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.onlineList1._FunctionName = "";
		this.onlineList1._HasRegistered = false;
		this.onlineList1._ServerName = "localhost";
		this.onlineList1._TRY_Flag = "";
		this.onlineList1._UserID = "";
		this.onlineList1._UserName = "";
		this.onlineList1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.onlineList1.Location = new System.Drawing.Point(408, 72);
		this.onlineList1.Name = "onlineList1";
		this.onlineList1.Size = new System.Drawing.Size(160, 160);
		this.onlineList1.TabIndex = 1;
		this.onlineList1.Visible = false;
		this.lblVersion.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.lblVersion.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblVersion.Location = new System.Drawing.Point(15, 376);
		this.lblVersion.Name = "lblVersion";
		this.lblVersion.Size = new System.Drawing.Size(210, 17);
		this.lblVersion.TabIndex = 0;
		this.lblVersion.Text = "version: 4.6.0001";
		base.Width = 600;
		base.Height = 400;
		this.AutoScaleBaseSize = new System.Drawing.Size(7, 19);
		base.ClientSize = new System.Drawing.Size(599, 399);
		base.Controls.Add(this.panel1);
		this.Font = new System.Drawing.Font("Times New Roman", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Name = "FormSplash";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "FormSplash";
		base.Load += new System.EventHandler(FormSplash_Load);
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
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
}
