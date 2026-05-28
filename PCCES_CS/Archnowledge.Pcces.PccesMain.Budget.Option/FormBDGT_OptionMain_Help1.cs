using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace Archnowledge.Pcces.PccesMain.Budget.Option;

public class FormBDGT_OptionMain_Help1 : Form
{
	private Panel panel1;

	private Panel panel9;

	private GroupBox groupBox5;

	private UltraButton ultraButton2;

	private FolderBrowserDialog folderBrowserDialog1;

	private PictureBox picbox;

	private Container components = null;

	public FormBDGT_OptionMain_Help1()
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
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.Option.FormBDGT_OptionMain_Help1));
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		this.panel1 = new System.Windows.Forms.Panel();
		this.picbox = new System.Windows.Forms.PictureBox();
		this.panel9 = new System.Windows.Forms.Panel();
		this.ultraButton2 = new Infragistics.Win.Misc.UltraButton();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
		this.panel1.SuspendLayout();
		this.panel9.SuspendLayout();
		base.SuspendLayout();
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.Controls.Add(this.picbox);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(790, 604);
		this.panel1.TabIndex = 0;
		this.picbox.Image = (System.Drawing.Image)resources.GetObject("picbox.Image");
		this.picbox.Location = new System.Drawing.Point(0, 0);
		this.picbox.Name = "picbox";
		this.picbox.Size = new System.Drawing.Size(792, 560);
		this.picbox.TabIndex = 0;
		this.picbox.TabStop = false;
		this.panel9.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel9.Controls.Add(this.ultraButton2);
		this.panel9.Controls.Add(this.groupBox5);
		this.panel9.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel9.Location = new System.Drawing.Point(0, 560);
		this.panel9.Name = "panel9";
		this.panel9.Size = new System.Drawing.Size(790, 44);
		this.panel9.TabIndex = 22;
		this.ultraButton2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton2.Appearance = appearance1;
		this.ultraButton2.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton2.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ultraButton2.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton2.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton2.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton2.Location = new System.Drawing.Point(694, 8);
		this.ultraButton2.Name = "ultraButton2";
		this.ultraButton2.ShowFocusRect = false;
		this.ultraButton2.ShowOutline = false;
		this.ultraButton2.Size = new System.Drawing.Size(88, 31);
		this.ultraButton2.SupportThemes = false;
		this.ultraButton2.TabIndex = 7;
		this.ultraButton2.Text = "關閉";
		this.ultraButton2.Click += new System.EventHandler(ultraButton2_Click);
		this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox5.Location = new System.Drawing.Point(0, 0);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(790, 4);
		this.groupBox5.TabIndex = 3;
		this.groupBox5.TabStop = false;
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		base.ClientSize = new System.Drawing.Size(790, 604);
		base.Controls.Add(this.panel9);
		base.Controls.Add(this.panel1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.MinimizeBox = false;
		base.Name = "FormBDGT_OptionMain_Help1";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "說明畫面";
		this.panel1.ResumeLayout(false);
		this.panel9.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	private void ultraButton2_Click(object sender, EventArgs e)
	{
	}
}
