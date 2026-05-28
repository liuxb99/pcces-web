using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace Archnowledge.Pcces.PccesMain.SysMaintain;

public class FormSys_A_Edit : Form
{
	private const string CallFormHelp = "FormSys_A_Edit";

	private Panel panel1;

	private Panel panel2;

	private GroupBox groupBox1;

	private UltraButton Btn_Cncl;

	private UltraButton Btn_OK;

	private Panel panel14;

	private UltraComboEditor Cbo1;

	private UltraLabel ultraLabel15;

	private UltraTextEditor txtPwdConfirm;

	private UltraLabel ultraLabel8;

	private UltraTextEditor txtPwd;

	private UltraLabel ultraLabel9;

	private UltraTextEditor txtUserName;

	private UltraLabel ultraLabel10;

	private UltraTextEditor txtUserID;

	private UltraLabel ultraLabel11;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	private Container components = null;

	private string F_UserID;

	private string F_UserName = "";

	private string F_Password = "";

	private string F_Power = "";

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

	public string _UserName
	{
		get
		{
			return F_UserName;
		}
		set
		{
			F_UserName = value;
		}
	}

	public string _Password
	{
		get
		{
			return F_Password;
		}
		set
		{
			F_Password = value;
		}
	}

	public string _Power
	{
		get
		{
			return F_Power;
		}
		set
		{
			F_Power = value;
		}
	}

	public FormSys_A_Edit()
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
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
		this.panel1 = new System.Windows.Forms.Panel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.Btn_OK = new Infragistics.Win.Misc.UltraButton();
		this.panel14 = new System.Windows.Forms.Panel();
		this.Cbo1 = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.ultraLabel15 = new Infragistics.Win.Misc.UltraLabel();
		this.txtPwdConfirm = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel8 = new Infragistics.Win.Misc.UltraLabel();
		this.txtPwd = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel9 = new Infragistics.Win.Misc.UltraLabel();
		this.txtUserName = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel10 = new Infragistics.Win.Misc.UltraLabel();
		this.txtUserID = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel11 = new Infragistics.Win.Misc.UltraLabel();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		this.panel14.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Cbo1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPwdConfirm).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPwd).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtUserName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtUserID).BeginInit();
		base.SuspendLayout();
		this.panel1.BackColor = System.Drawing.Color.White;
		this.panel1.Controls.Add(this.ultraLabel1);
		this.panel1.Controls.Add(this.ultraLabel2);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(364, 56);
		this.panel1.TabIndex = 1;
		this.ultraLabel1.BackColor = System.Drawing.Color.White;
		this.ultraLabel1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel1.Location = new System.Drawing.Point(8, 8);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(224, 23);
		this.ultraLabel1.TabIndex = 0;
		this.ultraLabel1.Text = "使用者帳號編輯";
		this.ultraLabel2.BackColor = System.Drawing.Color.White;
		this.ultraLabel2.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel2.Location = new System.Drawing.Point(23, 32);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(321, 23);
		this.ultraLabel2.TabIndex = 0;
		this.ultraLabel2.Text = "以此介面變更指定的使用者名稱或密碼";
		this.panel2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel2.Controls.Add(this.groupBox1);
		this.panel2.Controls.Add(this.Btn_Cncl);
		this.panel2.Controls.Add(this.Btn_OK);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 209);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(364, 44);
		this.panel2.TabIndex = 11;
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(364, 8);
		this.groupBox1.TabIndex = 3;
		this.groupBox1.TabStop = false;
		this.Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.FontData.Name = "細明體";
		appearance1.FontData.SizeInPoints = 11f;
		appearance1.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.Btn_Cncl.Appearance = appearance1;
		this.Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.Btn_Cncl.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.Btn_Cncl.Location = new System.Drawing.Point(264, 10);
		this.Btn_Cncl.Name = "Btn_Cncl";
		this.Btn_Cncl.ShowFocusRect = false;
		this.Btn_Cncl.ShowOutline = false;
		this.Btn_Cncl.Size = new System.Drawing.Size(88, 28);
		this.Btn_Cncl.SupportThemes = false;
		this.Btn_Cncl.TabIndex = 2;
		this.Btn_Cncl.Text = "取消";
		this.Btn_OK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance2.FontData.Name = "細明體";
		appearance2.FontData.SizeInPoints = 11f;
		appearance2.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.Btn_OK.Appearance = appearance2;
		this.Btn_OK.BackColor = System.Drawing.SystemColors.Control;
		this.Btn_OK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.Btn_OK.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.Btn_OK.Location = new System.Drawing.Point(172, 10);
		this.Btn_OK.Name = "Btn_OK";
		this.Btn_OK.ShowFocusRect = false;
		this.Btn_OK.ShowOutline = false;
		this.Btn_OK.Size = new System.Drawing.Size(88, 28);
		this.Btn_OK.SupportThemes = false;
		this.Btn_OK.TabIndex = 1;
		this.Btn_OK.Text = "確定";
		this.Btn_OK.Click += new System.EventHandler(Btn_OK_Click);
		this.panel14.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel14.Controls.Add(this.Cbo1);
		this.panel14.Controls.Add(this.ultraLabel15);
		this.panel14.Controls.Add(this.txtPwdConfirm);
		this.panel14.Controls.Add(this.ultraLabel8);
		this.panel14.Controls.Add(this.txtPwd);
		this.panel14.Controls.Add(this.ultraLabel9);
		this.panel14.Controls.Add(this.txtUserName);
		this.panel14.Controls.Add(this.ultraLabel10);
		this.panel14.Controls.Add(this.txtUserID);
		this.panel14.Controls.Add(this.ultraLabel11);
		this.panel14.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel14.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.panel14.Location = new System.Drawing.Point(0, 56);
		this.panel14.Name = "panel14";
		this.panel14.Size = new System.Drawing.Size(364, 153);
		this.panel14.TabIndex = 12;
		appearance3.FontData.Name = "細明體";
		appearance3.FontData.SizeInPoints = 11f;
		this.Cbo1.Appearance = appearance3;
		this.Cbo1.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
		valueListItem1.DataValue = "1";
		valueListItem1.DisplayText = "系統管理員";
		valueListItem2.DataValue = "2";
		valueListItem2.DisplayText = "一般使用者";
		this.Cbo1.Items.Add(valueListItem1);
		this.Cbo1.Items.Add(valueListItem2);
		this.Cbo1.Location = new System.Drawing.Point(96, 116);
		this.Cbo1.Name = "Cbo1";
		this.Cbo1.Size = new System.Drawing.Size(252, 24);
		this.Cbo1.TabIndex = 6;
		this.Cbo1.Text = null;
		appearance4.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel15.Appearance = appearance4;
		this.ultraLabel15.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel15.Location = new System.Drawing.Point(4, 120);
		this.ultraLabel15.Name = "ultraLabel15";
		this.ultraLabel15.Size = new System.Drawing.Size(77, 23);
		this.ultraLabel15.TabIndex = 10;
		this.ultraLabel15.Text = "身份別:";
		appearance5.FontData.Name = "細明體";
		appearance5.FontData.SizeInPoints = 11f;
		this.txtPwdConfirm.Appearance = appearance5;
		this.txtPwdConfirm.Location = new System.Drawing.Point(96, 89);
		this.txtPwdConfirm.MaxLength = 20;
		this.txtPwdConfirm.Name = "txtPwdConfirm";
		this.txtPwdConfirm.PasswordChar = '*';
		this.txtPwdConfirm.Size = new System.Drawing.Size(252, 24);
		this.txtPwdConfirm.TabIndex = 5;
		this.txtPwdConfirm.Validating += new System.ComponentModel.CancelEventHandler(txtUserID_Validating);
		appearance6.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel8.Appearance = appearance6;
		this.ultraLabel8.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel8.Location = new System.Drawing.Point(4, 92);
		this.ultraLabel8.Name = "ultraLabel8";
		this.ultraLabel8.Size = new System.Drawing.Size(96, 23);
		this.ultraLabel8.TabIndex = 7;
		this.ultraLabel8.Text = "確認密碼:";
		appearance7.FontData.Name = "細明體";
		appearance7.FontData.SizeInPoints = 11f;
		this.txtPwd.Appearance = appearance7;
		this.txtPwd.Location = new System.Drawing.Point(96, 62);
		this.txtPwd.MaxLength = 20;
		this.txtPwd.Name = "txtPwd";
		this.txtPwd.PasswordChar = '*';
		this.txtPwd.Size = new System.Drawing.Size(252, 24);
		this.txtPwd.TabIndex = 4;
		this.txtPwd.Validating += new System.ComponentModel.CancelEventHandler(txtUserID_Validating);
		appearance8.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel9.Appearance = appearance8;
		this.ultraLabel9.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel9.Location = new System.Drawing.Point(4, 64);
		this.ultraLabel9.Name = "ultraLabel9";
		this.ultraLabel9.Size = new System.Drawing.Size(96, 23);
		this.ultraLabel9.TabIndex = 5;
		this.ultraLabel9.Text = "使用者密碼:";
		appearance9.FontData.Name = "細明體";
		appearance9.FontData.SizeInPoints = 11f;
		this.txtUserName.Appearance = appearance9;
		this.txtUserName.Location = new System.Drawing.Point(96, 35);
		this.txtUserName.MaxLength = 10;
		this.txtUserName.Name = "txtUserName";
		this.txtUserName.Size = new System.Drawing.Size(252, 24);
		this.txtUserName.TabIndex = 3;
		this.txtUserName.Validating += new System.ComponentModel.CancelEventHandler(txtUserID_Validating);
		appearance10.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel10.Appearance = appearance10;
		this.ultraLabel10.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel10.Location = new System.Drawing.Point(4, 36);
		this.ultraLabel10.Name = "ultraLabel10";
		this.ultraLabel10.Size = new System.Drawing.Size(96, 23);
		this.ultraLabel10.TabIndex = 3;
		this.ultraLabel10.Text = "使用者名稱:";
		appearance11.FontData.Name = "細明體";
		appearance11.FontData.SizeInPoints = 11f;
		this.txtUserID.Appearance = appearance11;
		this.txtUserID.Enabled = false;
		this.txtUserID.Location = new System.Drawing.Point(96, 8);
		this.txtUserID.MaxLength = 10;
		this.txtUserID.Name = "txtUserID";
		this.txtUserID.Size = new System.Drawing.Size(252, 24);
		this.txtUserID.TabIndex = 2;
		this.txtUserID.Validating += new System.ComponentModel.CancelEventHandler(txtUserID_Validating);
		appearance12.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel11.Appearance = appearance12;
		this.ultraLabel11.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel11.Location = new System.Drawing.Point(4, 10);
		this.ultraLabel11.Name = "ultraLabel11";
		this.ultraLabel11.Size = new System.Drawing.Size(96, 23);
		this.ultraLabel11.TabIndex = 1;
		this.ultraLabel11.Text = "使用者帳號:";
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		base.ClientSize = new System.Drawing.Size(364, 253);
		base.Controls.Add(this.panel14);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel1);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.Name = "FormSys_A_Edit";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "使用者帳號編輯";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormSys_A_Edit_KeyDown);
		base.Load += new System.EventHandler(FormSys_A_Edit_Load);
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel14.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Cbo1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPwdConfirm).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPwd).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtUserName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtUserID).EndInit();
		base.ResumeLayout(false);
	}

	private void Btn_OK_Click(object sender, EventArgs e)
	{
		if (txtUserName.Text.Trim() == "")
		{
			MessageBox.Show(this, "使用者名稱不可空白", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtUserName.Focus();
			return;
		}
		for (int i = 0; i < txtUserID.Text.Length; i++)
		{
			if (txtUserID.Text[i] > '\u007f')
			{
				MessageBox.Show(this, "使用者帳號不可以是中文或特殊字元", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtUserID.Focus();
				return;
			}
		}
		if (txtPwd.Text.Trim() != txtPwdConfirm.Text.Trim())
		{
			MessageBox.Show(this, "密碼並未確認正確。請確定您輸入的密碼和確認的密碼完全相符。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtPwd.Text = "";
			txtPwdConfirm.Text = "";
			txtUserID.Focus();
		}
		else
		{
			DBClass DBCLS = new DBClass();
			DBCLS.SaveUsers(txtUserID.Text.Trim(), txtUserName.Text.Trim(), txtPwd.Text.Trim(), Cbo1.Value.ToString());
			base.DialogResult = DialogResult.OK;
		}
	}

	private void FormSys_A_Edit_Load(object sender, EventArgs e)
	{
		txtUserID.Text = F_UserID;
		txtUserName.Text = F_UserName;
		txtPwd.Text = F_Password;
		txtPwdConfirm.Text = F_Password;
		if (F_Power.Trim() == "1")
		{
			Cbo1.SelectedIndex = 0;
		}
		else
		{
			Cbo1.SelectedIndex = 1;
		}
	}

	private void txtUserID_Validating(object sender, CancelEventArgs e)
	{
		if (!CommonMethods.CheckValidString((sender as UltraTextEditor).Text))
		{
			e.Cancel = true;
		}
		for (int i = 0; i < txtUserID.Text.Length; i++)
		{
			if (!CommonMethods.EngNumValid(txtUserID.Text[i]))
			{
				MessageBox.Show(this, "不可輸入非數字或英文字母及的字", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtUserID.Focus();
				break;
			}
		}
	}

	private void FormSys_A_Edit_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormSys_A_Edit");
		}
	}
}
