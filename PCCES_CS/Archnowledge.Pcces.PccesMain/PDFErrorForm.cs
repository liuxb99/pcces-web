using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.PccesMain.ShellLib;

namespace Archnowledge.Pcces.PccesMain;

public class PDFErrorForm : Form
{
	private Label label5;

	private Label label4;

	private Label label3;

	private Label label2;

	private Label label1;

	private PictureBox pictureBox2;

	private Button button1;

	private PictureBox pictureBox1;

	private Container components = null;

	public PDFErrorForm()
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
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.PDFErrorForm));
		this.label5 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.pictureBox2 = new System.Windows.Forms.PictureBox();
		this.button1 = new System.Windows.Forms.Button();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		base.SuspendLayout();
		this.label5.Font = new System.Drawing.Font("新細明體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.label5.Location = new System.Drawing.Point(232, 160);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(160, 16);
		this.label5.TabIndex = 17;
		this.label5.Text = "以致無法開啟說明檔";
		this.label4.Font = new System.Drawing.Font("新細明體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.label4.Location = new System.Drawing.Point(232, 112);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(160, 16);
		this.label4.TabIndex = 16;
		this.label4.Text = "由於您的電腦未安裝";
		this.label3.Font = new System.Drawing.Font("新細明體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.label3.Location = new System.Drawing.Point(232, 184);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(160, 16);
		this.label3.TabIndex = 15;
		this.label3.Text = "請您點選下載連結，";
		this.label2.Font = new System.Drawing.Font("新細明體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.label2.Location = new System.Drawing.Point(232, 208);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(168, 16);
		this.label2.TabIndex = 14;
		this.label2.Text = "下載安裝即可，謝謝!";
		this.label1.Font = new System.Drawing.Font("新細明體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.label1.Location = new System.Drawing.Point(232, 136);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(104, 16);
		this.label1.TabIndex = 13;
		this.label1.Text = "Adobe Reader ";
		this.pictureBox2.Image = (System.Drawing.Image)resources.GetObject("pictureBox2.Image");
		this.pictureBox2.Location = new System.Drawing.Point(80, 48);
		this.pictureBox2.Name = "pictureBox2";
		this.pictureBox2.Size = new System.Drawing.Size(212, 41);
		this.pictureBox2.TabIndex = 12;
		this.pictureBox2.TabStop = false;
		this.button1.BackgroundImage = (System.Drawing.Image)resources.GetObject("button1.BackgroundImage");
		this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
		this.button1.Image = (System.Drawing.Image)resources.GetObject("button1.Image");
		this.button1.Location = new System.Drawing.Point(240, 256);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(112, 32);
		this.button1.TabIndex = 11;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
		this.pictureBox1.Location = new System.Drawing.Point(80, 120);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(104, 104);
		this.pictureBox1.TabIndex = 10;
		this.pictureBox1.TabStop = false;
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		base.ClientSize = new System.Drawing.Size(520, 358);
		base.Controls.Add(this.label5);
		base.Controls.Add(this.label4);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.pictureBox2);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.pictureBox1);
		base.Name = "PDFErrorForm";
		this.Text = "PDFErrorForm";
		base.ResumeLayout(false);
	}

	private void button1_Click(object sender, EventArgs e)
	{
		ShellExecute SHExe = new ShellExecute();
		SHExe.OwnerHandle = base.Handle;
		SHExe.Path = "http://www.adobe.com/tw/products/acrobat/readstep2.html";
		SHExe.Execute();
	}
}
