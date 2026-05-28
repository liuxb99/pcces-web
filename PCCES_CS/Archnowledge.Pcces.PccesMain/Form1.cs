using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using C1.Win.C1Input;

namespace Archnowledge.Pcces.PccesMain;

public class Form1 : Form
{
	private IContainer components = null;

	private C1TextBox c1TextBox1;

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
		this.c1TextBox1 = new C1.Win.C1Input.C1TextBox();
		((System.ComponentModel.ISupportInitialize)this.c1TextBox1).BeginInit();
		base.SuspendLayout();
		this.c1TextBox1.Location = new System.Drawing.Point(252, 201);
		this.c1TextBox1.Name = "c1TextBox1";
		this.c1TextBox1.Size = new System.Drawing.Size(100, 21);
		this.c1TextBox1.TabIndex = 0;
		this.c1TextBox1.Tag = null;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(292, 273);
		base.Controls.Add(this.c1TextBox1);
		base.Name = "Form1";
		this.Text = "Form1";
		((System.ComponentModel.ISupportInitialize)this.c1TextBox1).EndInit();
		base.ResumeLayout(false);
	}

	public Form1()
	{
		InitializeComponent();
	}
}
