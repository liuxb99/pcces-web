using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Archnowledge.Pcces.PccesMain;

public class FormMessage : Form
{
	private ImageList imageList1;

	private IContainer components;

	private CheckBox chkBox;

	private Panel panel1;

	private Panel panel2;

	private PictureBox pictureBox1;

	private MessageBoxIcon F_Icon;

	public MessageBoxIcon _Icon
	{
		set
		{
			F_Icon = value;
		}
	}

	public FormMessage()
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
		this.components = new System.ComponentModel.Container();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.chkBox = new System.Windows.Forms.CheckBox();
		this.panel1 = new System.Windows.Forms.Panel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		base.SuspendLayout();
		this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.chkBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.chkBox.Location = new System.Drawing.Point(8, 8);
		this.chkBox.Name = "chkBox";
		this.chkBox.Size = new System.Drawing.Size(229, 24);
		this.chkBox.TabIndex = 0;
		this.chkBox.Text = "將來不再顯示此對話框";
		this.chkBox.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
		this.chkBox.CheckedChanged += new System.EventHandler(chkBox_CheckedChanged);
		this.panel1.Controls.Add(this.chkBox);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 85);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(472, 40);
		this.panel1.TabIndex = 1;
		this.panel2.Controls.Add(this.pictureBox1);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(80, 85);
		this.panel2.TabIndex = 2;
		this.pictureBox1.Location = new System.Drawing.Point(16, 16);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(48, 50);
		this.pictureBox1.TabIndex = 3;
		this.pictureBox1.TabStop = false;
		this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
		base.ClientSize = new System.Drawing.Size(472, 125);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel1);
		this.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.Name = "FormMessage";
		this.Text = "FormMessage";
		base.Load += new System.EventHandler(FormMessage_Load);
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	private void FormMessage_Load(object sender, EventArgs e)
	{
	}

	private void chkBox_CheckedChanged(object sender, EventArgs e)
	{
	}
}
