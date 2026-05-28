using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.PccesMain.ShellLib;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormOpenExcel : Form
{
	private IContainer components = null;

	private Button btnOpenExcel;

	private Label label1;

	private Label lbFileName;

	private Label label3;

	private Button btnOK;

	private Label label4;

	public string filepath;

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormOpenExcel));
		this.btnOpenExcel = new System.Windows.Forms.Button();
		this.label1 = new System.Windows.Forms.Label();
		this.lbFileName = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.btnOK = new System.Windows.Forms.Button();
		this.label4 = new System.Windows.Forms.Label();
		base.SuspendLayout();
		this.btnOpenExcel.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.btnOpenExcel.Location = new System.Drawing.Point(11, 152);
		this.btnOpenExcel.Name = "btnOpenExcel";
		this.btnOpenExcel.Size = new System.Drawing.Size(75, 23);
		this.btnOpenExcel.TabIndex = 0;
		this.btnOpenExcel.Text = "直接開啟";
		this.btnOpenExcel.UseVisualStyleBackColor = true;
		this.btnOpenExcel.Click += new System.EventHandler(btnOpenExcel_Click);
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("標楷體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.label1.ForeColor = System.Drawing.Color.Red;
		this.label1.Location = new System.Drawing.Point(8, 9);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(179, 19);
		this.label1.TabIndex = 1;
		this.label1.Text = "您已成功匯出資料!";
		this.lbFileName.AutoSize = true;
		this.lbFileName.ForeColor = System.Drawing.Color.Red;
		this.lbFileName.Location = new System.Drawing.Point(12, 112);
		this.lbFileName.Name = "lbFileName";
		this.lbFileName.Size = new System.Drawing.Size(0, 12);
		this.lbFileName.TabIndex = 2;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("細明體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.label3.Location = new System.Drawing.Point(8, 51);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(184, 16);
		this.label3.TabIndex = 3;
		this.label3.Text = "若要結束精靈請按下完成";
		this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnOK.Location = new System.Drawing.Point(265, 152);
		this.btnOK.Name = "btnOK";
		this.btnOK.Size = new System.Drawing.Size(75, 23);
		this.btnOK.TabIndex = 4;
		this.btnOK.Text = "完成";
		this.btnOK.UseVisualStyleBackColor = true;
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("細明體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.label4.Location = new System.Drawing.Point(8, 71);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(248, 16);
		this.label4.TabIndex = 5;
		this.label4.Text = "或是點選直接開啟來啟動電子檔。";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Control;
		base.ClientSize = new System.Drawing.Size(352, 187);
		base.Controls.Add(this.label4);
		base.Controls.Add(this.btnOK);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.lbFileName);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.btnOpenExcel);
		this.ForeColor = System.Drawing.SystemColors.InfoText;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "FormOpenExcel";
		this.Text = "匯出電子檔";
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public FormOpenExcel()
	{
		InitializeComponent();
	}

	public void ResetLable()
	{
		lbFileName.Text = filepath;
	}

	private void btnOpenExcel_Click(object sender, EventArgs e)
	{
		ShellExecute SHExe = new ShellExecute();
		SHExe.OwnerHandle = base.Handle;
		SHExe.Parameters = lbFileName.Text;
		SHExe.Path = lbFileName.Text;
		SHExe.Execute();
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		string iniFilePath = AppDomain.CurrentDomain.BaseDirectory + "PccesMain.ini";
		CommonMethods.IniWriteValue(iniFilePath, "FormBudget", "ExportPath", lbFileName.Text.Trim());
	}
}
