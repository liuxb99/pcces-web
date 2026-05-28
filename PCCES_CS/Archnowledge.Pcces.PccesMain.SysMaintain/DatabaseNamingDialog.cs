using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace Archnowledge.Pcces.PccesMain.SysMaintain;

public class DatabaseNamingDialog : Form
{
	private IContainer components = null;

	private Panel panel1;

	private Label lbDescription;

	private UltraButton btnCancel;

	private GroupBox gbButtons;

	private Panel panel8;

	private UltraButton btnOK;

	private TextBox tbDatabaseName;

	private string invalidDatabaseName;

	private string newDatabaseName;

	public string InvalidDatabaseName
	{
		set
		{
			invalidDatabaseName = value;
		}
	}

	public string NewDatabaseName => newDatabaseName;

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.SysMaintain.DatabaseNamingDialog));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		this.panel1 = new System.Windows.Forms.Panel();
		this.tbDatabaseName = new System.Windows.Forms.TextBox();
		this.lbDescription = new System.Windows.Forms.Label();
		this.btnCancel = new Infragistics.Win.Misc.UltraButton();
		this.gbButtons = new System.Windows.Forms.GroupBox();
		this.panel8 = new System.Windows.Forms.Panel();
		this.btnOK = new Infragistics.Win.Misc.UltraButton();
		this.panel1.SuspendLayout();
		this.panel8.SuspendLayout();
		base.SuspendLayout();
		this.panel1.BackColor = System.Drawing.Color.White;
		this.panel1.Controls.Add(this.tbDatabaseName);
		this.panel1.Controls.Add(this.lbDescription);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(406, 85);
		this.panel1.TabIndex = 21;
		this.tbDatabaseName.Location = new System.Drawing.Point(15, 45);
		this.tbDatabaseName.Name = "tbDatabaseName";
		this.tbDatabaseName.Size = new System.Drawing.Size(287, 22);
		this.tbDatabaseName.TabIndex = 1;
		this.lbDescription.Font = new System.Drawing.Font("新細明體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.lbDescription.Location = new System.Drawing.Point(12, 19);
		this.lbDescription.Name = "lbDescription";
		this.lbDescription.Size = new System.Drawing.Size(382, 23);
		this.lbDescription.TabIndex = 0;
		this.lbDescription.Text = "資料庫已存在，請輸入其他名稱。";
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnCancel.Appearance = appearance1;
		this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
		this.btnCancel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnCancel.Font = new System.Drawing.Font("細明體", 11f);
		this.btnCancel.ImageSize = new System.Drawing.Size(20, 20);
		this.btnCancel.ImageTransparentColor = System.Drawing.Color.White;
		this.btnCancel.Location = new System.Drawing.Point(308, 10);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.ShowFocusRect = false;
		this.btnCancel.Size = new System.Drawing.Size(88, 31);
		this.btnCancel.SupportThemes = false;
		this.btnCancel.TabIndex = 4;
		this.btnCancel.Text = "取消";
		this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
		this.gbButtons.Dock = System.Windows.Forms.DockStyle.Top;
		this.gbButtons.Location = new System.Drawing.Point(0, 0);
		this.gbButtons.Name = "gbButtons";
		this.gbButtons.Size = new System.Drawing.Size(406, 8);
		this.gbButtons.TabIndex = 3;
		this.gbButtons.TabStop = false;
		this.panel8.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel8.Controls.Add(this.btnCancel);
		this.panel8.Controls.Add(this.gbButtons);
		this.panel8.Controls.Add(this.btnOK);
		this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel8.Location = new System.Drawing.Point(0, 85);
		this.panel8.Name = "panel8";
		this.panel8.Size = new System.Drawing.Size(406, 44);
		this.panel8.TabIndex = 20;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnOK.Appearance = appearance2;
		this.btnOK.BackColor = System.Drawing.SystemColors.Control;
		this.btnOK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnOK.Font = new System.Drawing.Font("細明體", 11f);
		this.btnOK.ImageSize = new System.Drawing.Size(20, 20);
		this.btnOK.ImageTransparentColor = System.Drawing.Color.White;
		this.btnOK.Location = new System.Drawing.Point(214, 10);
		this.btnOK.Name = "btnOK";
		this.btnOK.ShowFocusRect = false;
		this.btnOK.Size = new System.Drawing.Size(88, 31);
		this.btnOK.SupportThemes = false;
		this.btnOK.TabIndex = 1;
		this.btnOK.Text = "確定";
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(406, 129);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.panel8);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "DatabaseNamingDialog";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "資料庫名稱重複";
		base.Load += new System.EventHandler(DatabaseNamingDialog_Load);
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		this.panel8.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	public DatabaseNamingDialog()
	{
		InitializeComponent();
	}

	private void DatabaseNamingDialog_Load(object sender, EventArgs e)
	{
		lbDescription.Text = " 資料庫【" + invalidDatabaseName + "】已存在，請輸入其他名稱。";
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		newDatabaseName = tbDatabaseName.Text.Trim();
		if (!IsDatabaseNameValid(newDatabaseName))
		{
			MessageBox.Show("資料庫名稱只可包含數字及英文字母！", "注意");
		}
		else if (!IsDatabaseNameStartsWithLetter(newDatabaseName))
		{
			MessageBox.Show(this, "資料庫名稱開頭第一個字必須是英文字母。", "注意");
		}
		else
		{
			base.DialogResult = DialogResult.OK;
		}
	}

	private bool IsDatabaseNameValid(string DatabaseName)
	{
		string databaseName = tbDatabaseName.Text.Trim();
		return Regex.IsMatch(databaseName, "^[a-zA-Z0-9]+$");
	}

	private bool IsDatabaseNameStartsWithLetter(string newDatabaseName)
	{
		string databaseName = tbDatabaseName.Text.Trim();
		return Regex.IsMatch(databaseName, "^[a-zA-Z]");
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		DialogResult result = MessageBox.Show(this, "取消則系統不會建立此資料庫，是否確定取消？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
		if (result == DialogResult.Yes)
		{
			base.DialogResult = DialogResult.Cancel;
		}
	}
}
