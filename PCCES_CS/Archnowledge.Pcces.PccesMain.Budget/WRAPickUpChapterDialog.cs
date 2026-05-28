using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Archnowledge.Pcces.DomainModule.General;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class WRAPickUpChapterDialog : Form
{
	private IContainer components = null;

	private ListBox lboxFileList;

	private Button btnOK;

	private Button btnCancel;

	public string WRAFilelist = "";

	public string UserID = "";

	public string ProjectCode = "";

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
		this.lboxFileList = new System.Windows.Forms.ListBox();
		this.btnOK = new System.Windows.Forms.Button();
		this.btnCancel = new System.Windows.Forms.Button();
		base.SuspendLayout();
		this.lboxFileList.FormattingEnabled = true;
		this.lboxFileList.ItemHeight = 12;
		this.lboxFileList.Location = new System.Drawing.Point(12, 12);
		this.lboxFileList.Name = "lboxFileList";
		this.lboxFileList.Size = new System.Drawing.Size(268, 184);
		this.lboxFileList.TabIndex = 0;
		this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.btnOK.Location = new System.Drawing.Point(12, 202);
		this.btnOK.Name = "btnOK";
		this.btnOK.Size = new System.Drawing.Size(75, 23);
		this.btnOK.TabIndex = 1;
		this.btnOK.Text = "確認";
		this.btnOK.UseVisualStyleBackColor = true;
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.Location = new System.Drawing.Point(205, 202);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(75, 23);
		this.btnCancel.TabIndex = 2;
		this.btnCancel.Text = "取消";
		this.btnCancel.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(292, 236);
		base.Controls.Add(this.btnCancel);
		base.Controls.Add(this.btnOK);
		base.Controls.Add(this.lboxFileList);
		base.Name = "WRAPickUpChapterDialog";
		this.Text = "請選擇章節";
		base.ResumeLayout(false);
	}

	public WRAPickUpChapterDialog()
	{
		InitializeComponent();
	}

	public void FileList2lboxFileList()
	{
		SysUser oSysUser = new SysUser();
		string CurrentDBName = oSysUser.GetSysUserDatabaseName(UserID);
		string AddOnPath = AppDomain.CurrentDomain.BaseDirectory + "WRAAddOn\\" + CurrentDBName + "\\" + ProjectCode;
		if (!File.Exists(AddOnPath + "\\List.txt"))
		{
			return;
		}
		using StreamReader sr = new StreamReader(AddOnPath + "\\List.txt", Encoding.GetEncoding("Big5"));
		char[] splitChars = new char[2] { '.', ',' };
		string line;
		while ((line = sr.ReadLine()) != null && line != "")
		{
			string[] words = line.Split(splitChars);
			lboxFileList.Items.Add(words[6] + "," + words[0] + "," + words[1]);
		}
		lboxFileList.Items.Add("其它文件(手動輸入)");
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		WRAFilelist = lboxFileList.SelectedItem.ToString();
	}
}
