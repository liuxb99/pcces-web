using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace Archnowledge.Pcces.PccesMain.ArchControls;

public class FormChangeUserInfo : Form
{
	private GroupBox groupBox1;

	private UltraButton ultraButton1;

	private UltraLabel lblUserID;

	private UltraTextEditor txtNewPwdCnfm;

	private UltraTextEditor txtNewPwd;

	private UltraTextEditor txtOldPwd;

	private UltraLabel ultraLabel5;

	private UltraTextEditor txtName;

	private UltraLabel ultraLabel4;

	private UltraLabel ultraLabel3;

	private UltraLabel ultraLabel2;

	private UltraLabel ultraLabel1;

	private Container components = null;

	private string F_UserID;

	private string F_UserName = "";

	private string F_OldPwd = "";

	private UltraButton ultraButton2;

	public string _UserID
	{
		get
		{
			return F_UserID;
		}
		set
		{
			F_UserID = value;
		}
	}

	public FormChangeUserInfo()
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
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.ArchControls.FormChangeUserInfo));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.ultraButton2 = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.lblUserID = new Infragistics.Win.Misc.UltraLabel();
		this.txtNewPwdCnfm = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtNewPwd = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtOldPwd = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.txtName = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtNewPwdCnfm).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNewPwd).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtOldPwd).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtName).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.ultraButton2);
		this.groupBox1.Controls.Add(this.ultraButton1);
		this.groupBox1.Controls.Add(this.lblUserID);
		this.groupBox1.Controls.Add(this.txtNewPwdCnfm);
		this.groupBox1.Controls.Add(this.txtNewPwd);
		this.groupBox1.Controls.Add(this.txtOldPwd);
		this.groupBox1.Controls.Add(this.ultraLabel5);
		this.groupBox1.Controls.Add(this.txtName);
		this.groupBox1.Controls.Add(this.ultraLabel4);
		this.groupBox1.Controls.Add(this.ultraLabel3);
		this.groupBox1.Controls.Add(this.ultraLabel2);
		this.groupBox1.Controls.Add(this.ultraLabel1);
		this.groupBox1.Location = new System.Drawing.Point(8, 8);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(324, 220);
		this.groupBox1.TabIndex = 1;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "登入者資料";
		this.ultraButton2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton2.Appearance = appearance1;
		this.ultraButton2.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ultraButton2.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton2.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton2.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton2.Location = new System.Drawing.Point(216, 178);
		this.ultraButton2.Name = "ultraButton2";
		this.ultraButton2.ShowFocusRect = false;
		this.ultraButton2.ShowOutline = false;
		this.ultraButton2.Size = new System.Drawing.Size(84, 32);
		this.ultraButton2.SupportThemes = false;
		this.ultraButton2.TabIndex = 11;
		this.ultraButton2.Text = "取消";
		this.ultraButton1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton1.Appearance = appearance2;
		this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton1.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton1.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton1.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton1.Location = new System.Drawing.Point(116, 178);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.ShowFocusRect = false;
		this.ultraButton1.ShowOutline = false;
		this.ultraButton1.Size = new System.Drawing.Size(96, 32);
		this.ultraButton1.SupportThemes = false;
		this.ultraButton1.TabIndex = 10;
		this.ultraButton1.Text = "確定變更";
		this.ultraButton1.Click += new System.EventHandler(ultraButton1_Click);
		appearance3.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblUserID.Appearance = appearance3;
		this.lblUserID.Location = new System.Drawing.Point(116, 24);
		this.lblUserID.Name = "lblUserID";
		this.lblUserID.Size = new System.Drawing.Size(180, 23);
		this.lblUserID.TabIndex = 9;
		this.lblUserID.Text = "[lblUserID]";
		this.txtNewPwdCnfm.Location = new System.Drawing.Point(116, 141);
		this.txtNewPwdCnfm.Name = "txtNewPwdCnfm";
		this.txtNewPwdCnfm.PasswordChar = '*';
		this.txtNewPwdCnfm.Size = new System.Drawing.Size(184, 21);
		this.txtNewPwdCnfm.TabIndex = 8;
		this.txtNewPwdCnfm.Validating += new System.ComponentModel.CancelEventHandler(txtNewPwd_Validating);
		this.txtNewPwd.Location = new System.Drawing.Point(116, 112);
		this.txtNewPwd.Name = "txtNewPwd";
		this.txtNewPwd.PasswordChar = '*';
		this.txtNewPwd.Size = new System.Drawing.Size(184, 21);
		this.txtNewPwd.TabIndex = 7;
		this.txtNewPwd.Validating += new System.ComponentModel.CancelEventHandler(txtNewPwd_Validating);
		this.txtOldPwd.Location = new System.Drawing.Point(116, 82);
		this.txtOldPwd.Name = "txtOldPwd";
		this.txtOldPwd.PasswordChar = '*';
		this.txtOldPwd.Size = new System.Drawing.Size(184, 21);
		this.txtOldPwd.TabIndex = 6;
		this.txtOldPwd.Validating += new System.ComponentModel.CancelEventHandler(txtNewPwd_Validating);
		appearance4.TextHAlign = Infragistics.Win.HAlign.Right;
		this.ultraLabel5.Appearance = appearance4;
		this.ultraLabel5.Location = new System.Drawing.Point(16, 85);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(92, 23);
		this.ultraLabel5.TabIndex = 5;
		this.ultraLabel5.Text = "原密碼:";
		this.txtName.Location = new System.Drawing.Point(116, 52);
		this.txtName.Name = "txtName";
		this.txtName.Size = new System.Drawing.Size(184, 21);
		this.txtName.TabIndex = 4;
		this.txtName.Validating += new System.ComponentModel.CancelEventHandler(txtNewPwd_Validating);
		appearance5.TextHAlign = Infragistics.Win.HAlign.Right;
		this.ultraLabel4.Appearance = appearance5;
		this.ultraLabel4.Location = new System.Drawing.Point(16, 143);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(92, 23);
		this.ultraLabel4.TabIndex = 3;
		this.ultraLabel4.Text = "新密碼確認:";
		appearance6.TextHAlign = Infragistics.Win.HAlign.Right;
		this.ultraLabel3.Appearance = appearance6;
		this.ultraLabel3.Location = new System.Drawing.Point(16, 114);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(92, 23);
		this.ultraLabel3.TabIndex = 2;
		this.ultraLabel3.Text = "新密碼:";
		appearance7.TextHAlign = Infragistics.Win.HAlign.Right;
		this.ultraLabel2.Appearance = appearance7;
		this.ultraLabel2.Location = new System.Drawing.Point(16, 56);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(92, 23);
		this.ultraLabel2.TabIndex = 1;
		this.ultraLabel2.Text = "名稱:";
		appearance8.TextHAlign = Infragistics.Win.HAlign.Right;
		this.ultraLabel1.Appearance = appearance8;
		this.ultraLabel1.Location = new System.Drawing.Point(16, 27);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(92, 23);
		this.ultraLabel1.TabIndex = 0;
		this.ultraLabel1.Text = "帳號:";
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		base.ClientSize = new System.Drawing.Size(340, 237);
		base.Controls.Add(this.groupBox1);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormChangeUserInfo";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "密碼變更";
		base.Load += new System.EventHandler(FormChangeUserInfo_Load);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtNewPwdCnfm).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNewPwd).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtOldPwd).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtName).EndInit();
		base.ResumeLayout(false);
	}

	private void ultraButton1_Click(object sender, EventArgs e)
	{
		DBClass DBCLS = new DBClass();
		DataTable DT1 = DBCLS.GetUserData(F_UserID);
		if (DT1.Rows.Count > 0)
		{
			F_OldPwd = DT1.Rows[0]["Pwd"].ToString();
		}
		if (txtName.Text.Trim() == "")
		{
			MessageBox.Show(this, "名稱不可空白 !", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtName.Focus();
			return;
		}
		if (F_OldPwd != txtOldPwd.Text.Trim())
		{
			MessageBox.Show(this, "原密碼不同，請再確認 !", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtOldPwd.Focus();
			return;
		}
		if (txtNewPwd.Text.Trim() != txtNewPwdCnfm.Text.Trim())
		{
			MessageBox.Show(this, "密碼並未確認正確。請確定您輸入的新密碼和確認的新密碼完全相符。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtNewPwdCnfm.Focus();
			return;
		}
		int iResult = DBCLS.UpdateUserInfo(F_UserID, txtName.Text.Trim(), txtNewPwd.Text.Trim());
		if (iResult > 0)
		{
			MessageBox.Show(this, "變更完畢，下次登入時，請用新的密碼登入!", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		Close();
	}

	private void FormChangeUserInfo_Load(object sender, EventArgs e)
	{
		lblUserID.Text = F_UserID;
		DBClass DBCLS = new DBClass();
		DataTable DT1 = DBCLS.GetUserData(F_UserID);
		if (DT1.Rows.Count > 0)
		{
			F_UserName = DT1.Rows[0]["UserName"].ToString();
			F_OldPwd = DT1.Rows[0]["Pwd"].ToString();
			txtName.Text = F_UserName;
		}
	}

	private void txtNewPwd_Validating(object sender, CancelEventArgs e)
	{
		if (!CommonMethods.CheckValidString((sender as UltraTextEditor).Text))
		{
			e.Cancel = true;
		}
	}
}
