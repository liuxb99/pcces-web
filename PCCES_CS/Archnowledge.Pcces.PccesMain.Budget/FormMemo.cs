using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.STDClass;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormMemo : Form
{
	private string userID;

	private string projectCode;

	private string version;

	private PccesFormAction FormActionName;

	private IContainer components = null;

	private Panel panel1;

	private TextBox txtMemo;

	private UltraButton ultraButton4;

	private UltraButton BtnPick;

	private Label label1;

	public string _ProjectCode
	{
		get
		{
			return projectCode;
		}
		set
		{
			projectCode = value;
		}
	}

	public string _UserID
	{
		get
		{
			return userID;
		}
		set
		{
			userID = value;
		}
	}

	public string _iCount
	{
		get
		{
			return version;
		}
		set
		{
			version = value;
		}
	}

	public PccesFormAction _ActionName
	{
		get
		{
			return FormActionName;
		}
		set
		{
			FormActionName = value;
			switch (value)
			{
			case PccesFormAction.BID:
				label1.Text = "*可記錄保存此標單版本目的及說明";
				break;
			case PccesFormAction.CNT:
				label1.Text = "*可記錄保存契約書版本目的及說明";
				break;
			}
		}
	}

	public FormMemo()
	{
		InitializeComponent();
	}

	private void BtnPick_Click(object sender, EventArgs e)
	{
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(userID);
		aArr.Add("判斷是否要重新總計--" + projectCode);
		string sSQL = string.Concat("Update tmpProject set memo = '", txtMemo.Text.Trim(), "' where projectCode = '", projectCode, "' and version = '", version, "' and sKind = '", FormActionName, "'");
		ModifyDB ModDB = new ModifyDB(projectCode, aArr);
		ModDB.DBUpd(sSQL);
		aArr = null;
		ModDB = null;
		Close();
	}

	private void InitializeComponent()
	{
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormMemo));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		this.panel1 = new System.Windows.Forms.Panel();
		this.label1 = new System.Windows.Forms.Label();
		this.ultraButton4 = new Infragistics.Win.Misc.UltraButton();
		this.BtnPick = new Infragistics.Win.Misc.UltraButton();
		this.txtMemo = new System.Windows.Forms.TextBox();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.Controls.Add(this.label1);
		this.panel1.Controls.Add(this.ultraButton4);
		this.panel1.Controls.Add(this.BtnPick);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Font = new System.Drawing.Font("細明體", 11.25f);
		this.panel1.Location = new System.Drawing.Point(0, 54);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(536, 40);
		this.panel1.TabIndex = 1;
		this.label1.ForeColor = System.Drawing.Color.Red;
		this.label1.Location = new System.Drawing.Point(16, 8);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(320, 23);
		this.label1.TabIndex = 13;
		this.label1.Text = "*可記錄保存此預算書版本目的及說明";
		this.ultraButton4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton4.Appearance = appearance1;
		this.ultraButton4.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton4.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ultraButton4.HotTracking = true;
		this.ultraButton4.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton4.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton4.Location = new System.Drawing.Point(342, 5);
		this.ultraButton4.Name = "ultraButton4";
		this.ultraButton4.ShowFocusRect = false;
		this.ultraButton4.ShowOutline = false;
		this.ultraButton4.Size = new System.Drawing.Size(88, 31);
		this.ultraButton4.SupportThemes = false;
		this.ultraButton4.TabIndex = 12;
		this.ultraButton4.Text = "取消";
		this.ultraButton4.Visible = false;
		this.BtnPick.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnPick.Appearance = appearance2;
		this.BtnPick.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnPick.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.BtnPick.HotTracking = true;
		this.BtnPick.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnPick.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnPick.Location = new System.Drawing.Point(438, 5);
		this.BtnPick.Name = "BtnPick";
		this.BtnPick.ShowFocusRect = false;
		this.BtnPick.ShowOutline = false;
		this.BtnPick.Size = new System.Drawing.Size(88, 31);
		this.BtnPick.SupportThemes = false;
		this.BtnPick.TabIndex = 11;
		this.BtnPick.Text = "確定";
		this.BtnPick.Click += new System.EventHandler(BtnPick_Click);
		this.txtMemo.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.txtMemo.Location = new System.Drawing.Point(8, 16);
		this.txtMemo.MaxLength = 180;
		this.txtMemo.Name = "txtMemo";
		this.txtMemo.Size = new System.Drawing.Size(520, 25);
		this.txtMemo.TabIndex = 2;
		this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.ClientSize = new System.Drawing.Size(536, 94);
		base.ControlBox = false;
		base.Controls.Add(this.txtMemo);
		base.Controls.Add(this.panel1);
		this.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.Name = "FormMemo";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "暫存說明";
		this.panel1.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
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
