using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.PowerClass;
using Archnowledge.Pcces.STDClass;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace Archnowledge.Pcces.PccesMain;

public class FormLogin : Form
{
	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	private UltraTextEditor txtUserID;

	private UltraTextEditor txtPassword;

	private UltraButton BtnOK;

	private UltraButton BtnCancel;

	private Container components = null;

	private string IPAddress = "";

	private bool IsReLogin = false;

	public bool _IsReLogin
	{
		get
		{
			return IsReLogin;
		}
		set
		{
			IsReLogin = value;
		}
	}

	private void InitializeComponent()
	{
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.FormLogin));
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.txtUserID = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtPassword = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.BtnOK = new Infragistics.Win.Misc.UltraButton();
		this.BtnCancel = new Infragistics.Win.Misc.UltraButton();
		((System.ComponentModel.ISupportInitialize)this.txtUserID).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPassword).BeginInit();
		base.SuspendLayout();
		this.ultraLabel1.AutoSize = true;
		this.ultraLabel1.Location = new System.Drawing.Point(5, 16);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(90, 20);
		this.ultraLabel1.TabIndex = 0;
		this.ultraLabel1.Text = "使用者帳號:";
		this.ultraLabel2.AutoSize = true;
		this.ultraLabel2.Location = new System.Drawing.Point(5, 48);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(75, 20);
		this.ultraLabel2.TabIndex = 1;
		this.ultraLabel2.Text = "密    碼:";
		appearance1.FontData.Name = "細明體";
		appearance1.FontData.SizeInPoints = 11f;
		this.txtUserID.Appearance = appearance1;
		this.txtUserID.ImeMode = System.Windows.Forms.ImeMode.Off;
		this.txtUserID.Location = new System.Drawing.Point(116, 12);
		this.txtUserID.Name = "txtUserID";
		this.txtUserID.Size = new System.Drawing.Size(172, 24);
		this.txtUserID.TabIndex = 2;
		this.txtUserID.Text = "txtUserID";
		this.txtUserID.Validating += new System.ComponentModel.CancelEventHandler(txtUserID_Validating);
		appearance2.FontData.Name = "細明體";
		appearance2.FontData.SizeInPoints = 11f;
		this.txtPassword.Appearance = appearance2;
		this.txtPassword.Location = new System.Drawing.Point(116, 44);
		this.txtPassword.Name = "txtPassword";
		this.txtPassword.PasswordChar = '*';
		this.txtPassword.Size = new System.Drawing.Size(172, 24);
		this.txtPassword.TabIndex = 3;
		this.txtPassword.Text = "txtPassword";
		this.txtPassword.Validating += new System.ComponentModel.CancelEventHandler(txtUserID_Validating);
		appearance3.Image = resources.GetObject("appearance3.Image");
		appearance3.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnOK.Appearance = appearance3;
		this.BtnOK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnOK.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnOK.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnOK.Location = new System.Drawing.Point(66, 94);
		this.BtnOK.Name = "BtnOK";
		this.BtnOK.ShowFocusRect = false;
		this.BtnOK.ShowOutline = false;
		this.BtnOK.Size = new System.Drawing.Size(88, 31);
		this.BtnOK.SupportThemes = false;
		this.BtnOK.TabIndex = 4;
		this.BtnOK.Text = "確定";
		this.BtnOK.Click += new System.EventHandler(BtnOK_Click);
		appearance4.Image = resources.GetObject("appearance4.Image");
		appearance4.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnCancel.Appearance = appearance4;
		this.BtnCancel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnCancel.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnCancel.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnCancel.Location = new System.Drawing.Point(156, 94);
		this.BtnCancel.Name = "BtnCancel";
		this.BtnCancel.ShowFocusRect = false;
		this.BtnCancel.ShowOutline = false;
		this.BtnCancel.Size = new System.Drawing.Size(88, 31);
		this.BtnCancel.SupportThemes = false;
		this.BtnCancel.TabIndex = 5;
		this.BtnCancel.Text = "取消";
		this.BtnCancel.Click += new System.EventHandler(BtnCancel_Click);
		base.AcceptButton = this.BtnOK;
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		base.ClientSize = new System.Drawing.Size(308, 133);
		base.Controls.Add(this.BtnCancel);
		base.Controls.Add(this.BtnOK);
		base.Controls.Add(this.txtPassword);
		base.Controls.Add(this.txtUserID);
		base.Controls.Add(this.ultraLabel2);
		base.Controls.Add(this.ultraLabel1);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormLogin";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "使用者登入";
		base.Load += new System.EventHandler(FormLogin_Load);
		base.Activated += new System.EventHandler(FormLogin_Activated);
		((System.ComponentModel.ISupportInitialize)this.txtUserID).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPassword).EndInit();
		base.ResumeLayout(false);
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	public FormLogin()
	{
		InitializeComponent();
	}

	private void FormLogin_Load(object sender, EventArgs e)
	{
		txtUserID.Text = "";
		txtPassword.Text = "";
		string sIniFileName = CommonMethods.ExtractFilePath(Application.ExecutablePath) + "PccesMain.ini";
		txtUserID.Text = CommonMethods.IniReadValue(sIniFileName, "User", "UserID");
		IPAddress = CommonMethods.GetIPAddress();
	}

	private void BtnOK_Click(object sender, EventArgs e)
	{
		string IPStr = CommonMethods.GetIPAddress();
		ArrayList tmp_AL = new ArrayList();
		tmp_AL.Add("System");
		tmp_AL.Add("檢查權限-(" + IPStr + ")");
		StaffClass StaffCom = new StaffClass(tmp_AL);
		bool chkStaff = StaffCom.ChkLogon(txtUserID.Text, txtPassword.Text, IPAddress, Environment.MachineName);
		StaffCom = null;
		if (chkStaff)
		{
			UserClass UserCom = new UserClass(tmp_AL);
			DataTable DT_Tmp = UserCom.ListItem(" UserId='" + txtUserID.Text.Trim() + "' ");
			if (DT_Tmp.Rows.Count > 0)
			{
				(base.Owner as frmPccesMain)._UserName = DT_Tmp.Rows[0]["UserName"].ToString().Trim();
				(base.Owner as frmPccesMain)._UserID = DT_Tmp.Rows[0]["UserID"].ToString().Trim();
			}
			tmp_AL[0] = txtUserID.Text.Trim();
			tmp_AL[1] = txtUserID.Text.Trim() + "--登入(" + IPStr + ")";
			PubTools.WriteRoughlyLog(tmp_AL);
			base.DialogResult = DialogResult.OK;
			string sIniFileName = CommonMethods.ExtractFilePath(Application.ExecutablePath) + "PccesMain.ini";
			CommonMethods.IniWriteValue(sIniFileName, "User", "UserID", DT_Tmp.Rows[0]["UserID"].ToString().Trim());
			Close();
		}
		else
		{
			txtPassword.Text = "";
			MessageBox.Show(this, "帳號或密碼錯誤！", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtPassword.Focus();
		}
	}

	private void FormLogin_Activated(object sender, EventArgs e)
	{
		if (txtUserID.Text.Trim() != "")
		{
			txtPassword.Focus();
		}
	}

	private void BtnCancel_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.Cancel;
		(base.Owner as frmPccesMain)._LoginIsCancel = true;
	}

	private void txtUserID_Validating(object sender, CancelEventArgs e)
	{
		if (!CommonMethods.CheckValidString((sender as UltraTextEditor).Text))
		{
			e.Cancel = true;
		}
	}
}
