using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.STDClass;
using AxPVMarqueeLib;

namespace Archnowledge.Pcces.PccesMain.About;

public class FormAbout : Form
{
	private Panel panel1;

	private AxPVMarquee axPVMarquee1;

	private Label lblVersion;

	private Label label1;

	private Container components = null;

	public FormAbout()
	{
		InitializeComponent();
	}

	private void FormAbout_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void FormAbout_Load(object sender, EventArgs e)
	{
		lblVersion.Text = PccesVersion.PccesAssemblyVersion;
	}

	private void InitializeComponent()
	{
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.About.FormAbout));
		this.panel1 = new System.Windows.Forms.Panel();
		this.label1 = new System.Windows.Forms.Label();
		this.axPVMarquee1 = new AxPVMarqueeLib.AxPVMarquee();
		this.lblVersion = new System.Windows.Forms.Label();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.axPVMarquee1).BeginInit();
		base.SuspendLayout();
		this.panel1.BackgroundImage = (System.Drawing.Image)resources.GetObject("panel1.BackgroundImage");
		this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel1.Controls.Add(this.label1);
		this.panel1.Controls.Add(this.axPVMarquee1);
		this.panel1.Controls.Add(this.lblVersion);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(600, 400);
		this.panel1.TabIndex = 0;
		this.panel1.Click += new System.EventHandler(FormAbout_Click);
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Arial", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.White;
		this.label1.Location = new System.Drawing.Point(157, 109);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(68, 19);
		this.label1.TabIndex = 2;
		this.label1.Text = "Win 4.3 ";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.axPVMarquee1.Enabled = true;
		this.axPVMarquee1.Location = new System.Drawing.Point(84, 188);
		this.axPVMarquee1.Name = "axPVMarquee1";
		this.axPVMarquee1.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("axPVMarquee1.OcxState");
		this.axPVMarquee1.Size = new System.Drawing.Size(476, 140);
		this.axPVMarquee1.TabIndex = 1;
		this.axPVMarquee1.ClickEvent += new System.EventHandler(FormAbout_Click);
		this.lblVersion.AutoSize = true;
		this.lblVersion.BackColor = System.Drawing.Color.Transparent;
		this.lblVersion.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblVersion.ForeColor = System.Drawing.Color.White;
		this.lblVersion.Location = new System.Drawing.Point(14, 356);
		this.lblVersion.Name = "lblVersion";
		this.lblVersion.Size = new System.Drawing.Size(106, 16);
		this.lblVersion.TabIndex = 0;
		this.lblVersion.Text = "version: 4.6.0001";
		this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.lblVersion.Click += new System.EventHandler(FormAbout_Click);
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		this.BackColor = System.Drawing.Color.White;
		base.ClientSize = new System.Drawing.Size(600, 400);
		base.Controls.Add(this.panel1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Name = "FormAbout";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "關於 PCCES 4.3";
		base.Load += new System.EventHandler(FormAbout_Load);
		base.Click += new System.EventHandler(FormAbout_Click);
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.axPVMarquee1).EndInit();
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
