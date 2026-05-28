using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormDownloadDocDialog : Form
{
	private IContainer components = null;

	private Button button1;

	private Button button2;

	private Button button3;

	private Button button4;

	private Label label1;

	private Label label2;

	private string F_ExistingChapterCode = string.Empty;

	private string F_ExistingFileName = string.Empty;

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
		this.button1 = new System.Windows.Forms.Button();
		this.button2 = new System.Windows.Forms.Button();
		this.button3 = new System.Windows.Forms.Button();
		this.button4 = new System.Windows.Forms.Button();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		base.SuspendLayout();
		this.button1.Location = new System.Drawing.Point(30, 128);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(75, 25);
		this.button1.TabIndex = 0;
		this.button1.Text = "是";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.button2.Location = new System.Drawing.Point(115, 128);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(75, 25);
		this.button2.TabIndex = 1;
		this.button2.Text = "全部取代";
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.button3.Location = new System.Drawing.Point(200, 128);
		this.button3.Name = "button3";
		this.button3.Size = new System.Drawing.Size(75, 25);
		this.button3.TabIndex = 2;
		this.button3.Text = "否";
		this.button3.UseVisualStyleBackColor = true;
		this.button3.Click += new System.EventHandler(button3_Click);
		this.button4.Location = new System.Drawing.Point(285, 128);
		this.button4.Name = "button4";
		this.button4.Size = new System.Drawing.Size(75, 25);
		this.button4.TabIndex = 3;
		this.button4.Text = "皆不取代";
		this.button4.UseVisualStyleBackColor = true;
		this.button4.Click += new System.EventHandler(button4_Click);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(72, 48);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(33, 12);
		this.label1.TabIndex = 4;
		this.label1.Text = "label1";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(72, 77);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(33, 12);
		this.label2.TabIndex = 5;
		this.label2.Text = "label2";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(387, 190);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.button4);
		base.Controls.Add(this.button3);
		base.Controls.Add(this.button2);
		base.Controls.Add(this.button1);
		base.Name = "FormDownloadDocDialog";
		this.Text = "確認取代檔案";
		base.Load += new System.EventHandler(FormDownloadDocDialog_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public FormDownloadDocDialog(string chapterCode, string existingFileName)
	{
		InitializeComponent();
		F_ExistingChapterCode = chapterCode;
		F_ExistingFileName = existingFileName;
	}

	private void FormDownloadDocDialog_Load(object sender, EventArgs e)
	{
		label1.Text = $"第{F_ExistingChapterCode}章施工規範({Path.GetFileName(F_ExistingFileName)})";
		label2.Text = "已經存在，是否覆蓋該檔案？";
		AutoResize();
	}

	private void AutoResize()
	{
		label1.Location = new Point((base.Size.Width - label1.Size.Width) / 2, label1.Location.Y);
		label2.Location = new Point((base.Size.Width - label2.Size.Width) / 2, label2.Location.Y);
	}

	private void button1_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.Yes;
	}

	private void button2_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.OK;
	}

	private void button3_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.No;
	}

	private void button4_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.Ignore;
	}
}
