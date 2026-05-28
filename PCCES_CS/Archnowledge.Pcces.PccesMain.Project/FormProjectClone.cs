using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.DomainModule.Bid;
using Archnowledge.Pcces.DomainModule.Budget;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace Archnowledge.Pcces.PccesMain.Project;

public class FormProjectClone : Form
{
	private const string CallFormHelp = "FormProjectClone";

	private string F_UserID = "";

	private string F_OldProjectCode = "";

	private string F_OldProjectName = "";

	private string F_OldProjectNameE = "";

	private string F_OldProjectAddr = "";

	private bool F_IsBid = false;

	private bool F_IsBud = false;

	private string F_PID = "";

	private string SProjectCode = "工程代碼";

	private string SProjectCodeAlias = "工程別號";

	private Panel panel5;

	private UltraLabel ultraLabel7;

	private UltraLabel ultraLabel6;

	private UltraButton D_Btn_Fnsh;

	private GroupBox groupBox2;

	private UltraButton A_Btn_Cncl;

	private Panel panel1;

	private GroupBox groupBox1;

	private UltraLabel lblOldProjectCode;

	private UltraLabel lblOldProjectName;

	private GroupBox groupBox3;

	private UltraTextEditor txtNewProjectCode;

	private GroupBox groupBox4;

	private UltraCheckEditor chkBud;

	private UltraCheckEditor chkBid;

	private UltraLabel ultraLabel2;

	private UltraTextEditor txtNewProjectNameC;

	private UltraLabel ultraLabel3;

	private UltraLabel ultraLabel4;

	private UltraTextEditor txtNewProjectNameE;

	private UltraTextEditor txtNewProjectAddress;

	private UltraTextEditor txtNewProjectCodeAlias;

	private UltraLabel lblProjectCodeAlias;

	private UltraLabel lblProjectCode;

	private Container components = null;

	public Panel panel2;

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

	public string _OldProjectCode
	{
		get
		{
			return F_OldProjectCode;
		}
		set
		{
			F_OldProjectCode = value;
		}
	}

	public string _OldProjectName
	{
		get
		{
			return F_OldProjectName;
		}
		set
		{
			F_OldProjectName = value;
		}
	}

	public string _OldProjectNameE
	{
		get
		{
			return F_OldProjectNameE;
		}
		set
		{
			F_OldProjectNameE = value;
		}
	}

	public string _OldProjectAddr
	{
		get
		{
			return F_OldProjectAddr;
		}
		set
		{
			F_OldProjectAddr = value;
		}
	}

	public bool _IsBid
	{
		get
		{
			return F_IsBid;
		}
		set
		{
			F_IsBid = value;
		}
	}

	public bool _IsBud
	{
		get
		{
			return F_IsBud;
		}
		set
		{
			F_IsBud = value;
		}
	}

	public FormProjectClone()
	{
		InitializeComponent();
		F_PID = ConfigurationManager.AppSettings["PID"];
	}

	private void FormProjectClone_Load(object sender, EventArgs e)
	{
		if (F_PID != null && F_PID.Trim() == "Z14AC1100")
		{
			SProjectCodeAlias = "動支單號：";
			SProjectCode = "工程號/執行號：";
			lblProjectCodeAlias.Text = SProjectCodeAlias;
			lblProjectCode.Text = SProjectCode;
		}
		lblOldProjectCode.Text = "工程代碼：" + F_OldProjectCode;
		lblOldProjectName.Text = "工程名稱：" + F_OldProjectName;
		txtNewProjectCode.Text = F_OldProjectCode;
		txtNewProjectNameC.Text = F_OldProjectName;
		txtNewProjectNameE.Text = F_OldProjectNameE;
		txtNewProjectAddress.Text = F_OldProjectAddr;
		if (F_IsBid)
		{
			chkBid.Checked = true;
		}
		else
		{
			chkBid.Checked = false;
			chkBid.Enabled = false;
		}
		if (F_IsBud)
		{
			chkBud.Checked = true;
		}
		else
		{
			chkBud.Checked = false;
			chkBud.Enabled = false;
		}
		txtNewProjectCode.Focus();
	}

	private void D_Btn_Fnsh_Click(object sender, EventArgs e)
	{
		if (txtNewProjectCode.Text.Trim() == F_OldProjectCode)
		{
			MessageBox.Show(this, "新舊工程代碼相同，請先變更新的工程代碼", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		if (txtNewProjectCode.Text.Trim() == "")
		{
			MessageBox.Show(this, "新的工程代碼不可空白", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		int iCount = 0;
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("複製專案");
		Archnowledge.Pcces.BUDClass.Project ProjCom = new Archnowledge.Pcces.BUDClass.Project(aArr);
		BudProject budproject = new BudProject();
		BidProject bidproject = new BidProject();
		if (chkBud.Checked)
		{
			DataSet dsBudProject = budproject.GetProject(txtNewProjectCode.Text.Trim());
			if (dsBudProject.Tables[0].Rows.Count != 0)
			{
				MessageBox.Show(this, "預算書，已有相同工程代碼，請重新輸入", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			iCount++;
		}
		if (chkBid.Checked)
		{
			DataSet dsBidProject = bidproject.GetProject(txtNewProjectCode.Text.Trim());
			if (dsBidProject.Tables[0].Rows.Count != 0)
			{
				MessageBox.Show(this, "投標單，已有相同工程代碼，請重新輸入", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			iCount++;
		}
		if (iCount == 0)
		{
			MessageBox.Show(this, "請先勾選【預算書】或【標單】", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		ExecResult ER = new ExecResult();
		if (chkBud.Checked)
		{
			ER = budproject.CopyBudProject(F_OldProjectCode, txtNewProjectCode.Text.Trim(), txtNewProjectNameC.Text.Trim(), txtNewProjectNameE.Text.Trim(), txtNewProjectAddress.Text.Trim(), txtNewProjectCodeAlias.Text.Trim());
		}
		if (chkBid.Checked)
		{
			ER = bidproject.CopyBidProject(F_OldProjectCode, txtNewProjectCode.Text.Trim(), txtNewProjectNameC.Text.Trim(), txtNewProjectNameE.Text.Trim(), txtNewProjectAddress.Text.Trim(), txtNewProjectCodeAlias.Text.Trim());
		}
		if (ER.ReturnCode != 0)
		{
			MessageBox.Show(this, "專案複製失敗！", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		base.DialogResult = DialogResult.OK;
		(base.Owner as FormProject)._NewProjectCode = txtNewProjectCode.Text.Trim();
		try
		{
			DBClass DBCLS = new DBClass();
			DBCLS._FS_UserID = F_UserID;
			DBCLS.ExecuteCommand("Insert Into ProjAuthority(ProjectCode, UserID) values('" + txtNewProjectCode.Text.Trim() + "', '" + F_UserID + "')");
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Project.FormProjectClone.cs" + ex.Message);
		}
		Close();
	}

	private void txtNewProjectCode_Validating(object sender, CancelEventArgs e)
	{
		if (!CommonMethods.CheckValidString((sender as UltraTextEditor).Text))
		{
			e.Cancel = true;
			return;
		}
		for (int i = 0; i < txtNewProjectCode.Text.Length; i++)
		{
			string IsCHT = "TRUE";
			if (IsCHT != "TRUE" && !CommonMethods.EngNumValid(txtNewProjectCode.Text[i]))
			{
				MessageBox.Show(this, "不可輸入非數字或英文字", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtNewProjectCode.Focus();
				return;
			}
		}
		if (!CommonMethods.IsStrByteLenValid(txtNewProjectCode.Text, 45))
		{
			MessageBox.Show(this, "工程代碼的長度不可超過 40 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtNewProjectCode.Focus();
		}
	}

	private void FormProjectClone_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormProjectClone");
		}
	}

	private void txtNewProjectNameC_Leave(object sender, EventArgs e)
	{
		string projectName = txtNewProjectNameC.Text.Trim();
		if (projectName.IndexOf("\\") > -1)
		{
			projectName = projectName.Replace("\\", "_");
		}
		if (projectName.IndexOf(":") > -1)
		{
			projectName = projectName.Replace(":", "_");
		}
		if (projectName.IndexOf("/") > -1)
		{
			projectName = projectName.Replace("/", "_");
		}
		if (projectName.IndexOf("*") > -1)
		{
			projectName = projectName.Replace("*", "_");
		}
		if (projectName.IndexOf("?") > -1)
		{
			projectName = projectName.Replace("?", "_");
		}
		if (projectName.IndexOf("<") > -1)
		{
			projectName = projectName.Replace("<", "_");
		}
		if (projectName.IndexOf(">") > -1)
		{
			projectName = projectName.Replace(">", "_");
		}
		if (projectName.IndexOf("|") > -1)
		{
			projectName = projectName.Replace("|", "_");
		}
		txtNewProjectNameC.Text = projectName;
	}

	private void txtNewProjectCode_Leave(object sender, EventArgs e)
	{
		string projectName = txtNewProjectCode.Text.Trim();
		if (projectName.IndexOf("\\") > -1)
		{
			projectName = projectName.Replace("\\", "_");
		}
		if (projectName.IndexOf(":") > -1)
		{
			projectName = projectName.Replace(":", "_");
		}
		if (projectName.IndexOf("/") > -1)
		{
			projectName = projectName.Replace("/", "_");
		}
		if (projectName.IndexOf("*") > -1)
		{
			projectName = projectName.Replace("*", "_");
		}
		if (projectName.IndexOf("?") > -1)
		{
			projectName = projectName.Replace("?", "_");
		}
		if (projectName.IndexOf("<") > -1)
		{
			projectName = projectName.Replace("<", "_");
		}
		if (projectName.IndexOf(">") > -1)
		{
			projectName = projectName.Replace(">", "_");
		}
		if (projectName.IndexOf("|") > -1)
		{
			projectName = projectName.Replace("|", "_");
		}
		txtNewProjectCode.Text = projectName;
	}

	private void InitializeComponent()
	{
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
		this.panel5 = new System.Windows.Forms.Panel();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.D_Btn_Fnsh = new Infragistics.Win.Misc.UltraButton();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.A_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.panel1 = new System.Windows.Forms.Panel();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.chkBid = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.chkBud = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.lblProjectCodeAlias = new Infragistics.Win.Misc.UltraLabel();
		this.txtNewProjectCodeAlias = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtNewProjectAddress = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtNewProjectNameE = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.txtNewProjectNameC = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.lblProjectCode = new Infragistics.Win.Misc.UltraLabel();
		this.txtNewProjectCode = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.lblOldProjectName = new Infragistics.Win.Misc.UltraLabel();
		this.lblOldProjectCode = new Infragistics.Win.Misc.UltraLabel();
		this.panel5.SuspendLayout();
		this.panel2.SuspendLayout();
		this.panel1.SuspendLayout();
		this.groupBox4.SuspendLayout();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtNewProjectCodeAlias).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNewProjectAddress).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNewProjectNameE).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNewProjectNameC).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNewProjectCode).BeginInit();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.panel5.BackColor = System.Drawing.Color.White;
		this.panel5.Controls.Add(this.ultraLabel7);
		this.panel5.Controls.Add(this.ultraLabel6);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel5.Location = new System.Drawing.Point(0, 0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(464, 48);
		this.panel5.TabIndex = 16;
		appearance1.BackColor = System.Drawing.Color.White;
		this.ultraLabel7.Appearance = appearance1;
		this.ultraLabel7.Location = new System.Drawing.Point(44, 27);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel7.TabIndex = 3;
		this.ultraLabel7.Text = "請給定新的工程代碼";
		appearance2.BackColor = System.Drawing.Color.White;
		this.ultraLabel6.Appearance = appearance2;
		this.ultraLabel6.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel6.Location = new System.Drawing.Point(12, 7);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel6.TabIndex = 2;
		this.ultraLabel6.Text = "專案複製";
		this.panel2.AutoSize = true;
		this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		this.panel2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel2.Controls.Add(this.D_Btn_Fnsh);
		this.panel2.Controls.Add(this.groupBox2);
		this.panel2.Controls.Add(this.A_Btn_Cncl);
		this.panel2.Location = new System.Drawing.Point(0, 400);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(464, 44);
		this.panel2.TabIndex = 17;
		this.D_Btn_Fnsh.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance3.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Fnsh.Appearance = appearance3;
		this.D_Btn_Fnsh.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Fnsh.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.D_Btn_Fnsh.Font = new System.Drawing.Font("細明體", 11f);
		this.D_Btn_Fnsh.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Fnsh.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Fnsh.Location = new System.Drawing.Point(280, 10);
		this.D_Btn_Fnsh.Name = "D_Btn_Fnsh";
		this.D_Btn_Fnsh.ShowFocusRect = false;
		this.D_Btn_Fnsh.Size = new System.Drawing.Size(88, 31);
		this.D_Btn_Fnsh.SupportThemes = false;
		this.D_Btn_Fnsh.TabIndex = 6;
		this.D_Btn_Fnsh.Text = "確定";
		this.D_Btn_Fnsh.Click += new System.EventHandler(D_Btn_Fnsh_Click);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(464, 8);
		this.groupBox2.TabIndex = 3;
		this.groupBox2.TabStop = false;
		this.A_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance4.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.A_Btn_Cncl.Appearance = appearance4;
		this.A_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.A_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.A_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.A_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.A_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.A_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.A_Btn_Cncl.Location = new System.Drawing.Point(371, 10);
		this.A_Btn_Cncl.Name = "A_Btn_Cncl";
		this.A_Btn_Cncl.ShowFocusRect = false;
		this.A_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.A_Btn_Cncl.SupportThemes = false;
		this.A_Btn_Cncl.TabIndex = 2;
		this.A_Btn_Cncl.Text = "取消";
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.Controls.Add(this.groupBox4);
		this.panel1.Controls.Add(this.panel2);
		this.panel1.Controls.Add(this.groupBox3);
		this.panel1.Controls.Add(this.groupBox1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 48);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(464, 447);
		this.panel1.TabIndex = 18;
		this.groupBox4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.groupBox4.Controls.Add(this.chkBid);
		this.groupBox4.Controls.Add(this.chkBud);
		this.groupBox4.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		this.groupBox4.Location = new System.Drawing.Point(8, 300);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(448, 72);
		this.groupBox4.TabIndex = 2;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = "複製選項";
		appearance5.ForeColor = System.Drawing.Color.Black;
		this.chkBid.Appearance = appearance5;
		this.chkBid.Location = new System.Drawing.Point(167, 32);
		this.chkBid.Name = "chkBid";
		this.chkBid.Size = new System.Drawing.Size(120, 20);
		this.chkBid.TabIndex = 1;
		this.chkBid.Text = "標單";
		appearance6.ForeColor = System.Drawing.Color.Black;
		this.chkBud.Appearance = appearance6;
		this.chkBud.Location = new System.Drawing.Point(16, 32);
		this.chkBud.Name = "chkBud";
		this.chkBud.Size = new System.Drawing.Size(120, 20);
		this.chkBud.TabIndex = 0;
		this.chkBud.Text = "預算書";
		this.groupBox3.Controls.Add(this.lblProjectCodeAlias);
		this.groupBox3.Controls.Add(this.txtNewProjectCodeAlias);
		this.groupBox3.Controls.Add(this.txtNewProjectAddress);
		this.groupBox3.Controls.Add(this.txtNewProjectNameE);
		this.groupBox3.Controls.Add(this.ultraLabel4);
		this.groupBox3.Controls.Add(this.ultraLabel3);
		this.groupBox3.Controls.Add(this.txtNewProjectNameC);
		this.groupBox3.Controls.Add(this.ultraLabel2);
		this.groupBox3.Controls.Add(this.lblProjectCode);
		this.groupBox3.Controls.Add(this.txtNewProjectCode);
		this.groupBox3.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		this.groupBox3.Location = new System.Drawing.Point(8, 112);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(448, 200);
		this.groupBox3.TabIndex = 1;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "新專案資料";
		appearance7.ForeColor = System.Drawing.Color.Black;
		appearance7.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblProjectCodeAlias.Appearance = appearance7;
		this.lblProjectCodeAlias.Location = new System.Drawing.Point(48, 162);
		this.lblProjectCodeAlias.Name = "lblProjectCodeAlias";
		this.lblProjectCodeAlias.Size = new System.Drawing.Size(88, 23);
		this.lblProjectCodeAlias.TabIndex = 9;
		this.lblProjectCodeAlias.Text = "工程別號：";
		appearance8.ForeColor = System.Drawing.Color.Black;
		this.txtNewProjectCodeAlias.Appearance = appearance8;
		this.txtNewProjectCodeAlias.AutoSize = true;
		this.txtNewProjectCodeAlias.Location = new System.Drawing.Point(136, 160);
		this.txtNewProjectCodeAlias.MaxLength = 20;
		this.txtNewProjectCodeAlias.Name = "txtNewProjectCodeAlias";
		this.txtNewProjectCodeAlias.Size = new System.Drawing.Size(296, 21);
		this.txtNewProjectCodeAlias.TabIndex = 8;
		appearance9.ForeColor = System.Drawing.Color.Black;
		this.txtNewProjectAddress.Appearance = appearance9;
		this.txtNewProjectAddress.AutoSize = true;
		this.txtNewProjectAddress.Location = new System.Drawing.Point(136, 128);
		this.txtNewProjectAddress.MaxLength = 200;
		this.txtNewProjectAddress.Name = "txtNewProjectAddress";
		this.txtNewProjectAddress.Size = new System.Drawing.Size(296, 21);
		this.txtNewProjectAddress.TabIndex = 7;
		appearance10.ForeColor = System.Drawing.Color.Black;
		this.txtNewProjectNameE.Appearance = appearance10;
		this.txtNewProjectNameE.AutoSize = true;
		this.txtNewProjectNameE.Location = new System.Drawing.Point(136, 96);
		this.txtNewProjectNameE.MaxLength = 200;
		this.txtNewProjectNameE.Name = "txtNewProjectNameE";
		this.txtNewProjectNameE.Size = new System.Drawing.Size(296, 21);
		this.txtNewProjectNameE.TabIndex = 6;
		appearance11.ForeColor = System.Drawing.Color.Black;
		appearance11.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel4.Appearance = appearance11;
		this.ultraLabel4.Location = new System.Drawing.Point(48, 128);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(88, 23);
		this.ultraLabel4.TabIndex = 5;
		this.ultraLabel4.Text = "工程地點：";
		appearance12.ForeColor = System.Drawing.Color.Black;
		appearance12.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel3.Appearance = appearance12;
		this.ultraLabel3.Location = new System.Drawing.Point(17, 96);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(119, 23);
		this.ultraLabel3.TabIndex = 4;
		this.ultraLabel3.Text = "Project Name：";
		appearance13.ForeColor = System.Drawing.Color.Black;
		this.txtNewProjectNameC.Appearance = appearance13;
		this.txtNewProjectNameC.AutoSize = true;
		this.txtNewProjectNameC.Location = new System.Drawing.Point(136, 62);
		this.txtNewProjectNameC.MaxLength = 200;
		this.txtNewProjectNameC.Name = "txtNewProjectNameC";
		this.txtNewProjectNameC.Size = new System.Drawing.Size(296, 21);
		this.txtNewProjectNameC.TabIndex = 3;
		this.txtNewProjectNameC.Leave += new System.EventHandler(txtNewProjectNameC_Leave);
		appearance14.ForeColor = System.Drawing.Color.Black;
		appearance14.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel2.Appearance = appearance14;
		this.ultraLabel2.Location = new System.Drawing.Point(48, 64);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(88, 23);
		this.ultraLabel2.TabIndex = 2;
		this.ultraLabel2.Text = "工程名稱：";
		appearance15.ForeColor = System.Drawing.Color.Black;
		appearance15.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblProjectCode.Appearance = appearance15;
		this.lblProjectCode.Location = new System.Drawing.Point(8, 32);
		this.lblProjectCode.Name = "lblProjectCode";
		this.lblProjectCode.Size = new System.Drawing.Size(128, 23);
		this.lblProjectCode.TabIndex = 1;
		this.lblProjectCode.Text = "     工程代碼：";
		appearance16.ForeColor = System.Drawing.Color.Black;
		this.txtNewProjectCode.Appearance = appearance16;
		this.txtNewProjectCode.AutoSize = true;
		this.txtNewProjectCode.Location = new System.Drawing.Point(136, 30);
		this.txtNewProjectCode.MaxLength = 40;
		this.txtNewProjectCode.Name = "txtNewProjectCode";
		this.txtNewProjectCode.Size = new System.Drawing.Size(296, 21);
		this.txtNewProjectCode.TabIndex = 0;
		this.txtNewProjectCode.Validating += new System.ComponentModel.CancelEventHandler(txtNewProjectCode_Validating);
		this.txtNewProjectCode.Leave += new System.EventHandler(txtNewProjectCode_Leave);
		this.groupBox1.Controls.Add(this.lblOldProjectName);
		this.groupBox1.Controls.Add(this.lblOldProjectCode);
		this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		this.groupBox1.Location = new System.Drawing.Point(8, 6);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(448, 100);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "原始專案";
		appearance17.ForeColor = System.Drawing.Color.Black;
		appearance17.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblOldProjectName.Appearance = appearance17;
		this.lblOldProjectName.Location = new System.Drawing.Point(11, 50);
		this.lblOldProjectName.Name = "lblOldProjectName";
		this.lblOldProjectName.Size = new System.Drawing.Size(424, 44);
		this.lblOldProjectName.TabIndex = 1;
		this.lblOldProjectName.Text = "工程名稱：";
		appearance18.ForeColor = System.Drawing.Color.Black;
		appearance18.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblOldProjectCode.Appearance = appearance18;
		this.lblOldProjectCode.Location = new System.Drawing.Point(11, 24);
		this.lblOldProjectCode.Name = "lblOldProjectCode";
		this.lblOldProjectCode.Size = new System.Drawing.Size(424, 23);
		this.lblOldProjectCode.TabIndex = 0;
		this.lblOldProjectCode.Text = "工程代碼：";
		base.AcceptButton = this.D_Btn_Fnsh;
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		base.CancelButton = this.A_Btn_Cncl;
		base.ClientSize = new System.Drawing.Size(464, 495);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.panel5);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormProjectClone";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "專案複製";
		base.Load += new System.EventHandler(FormProjectClone_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormProjectClone_KeyDown);
		this.panel5.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		this.groupBox4.ResumeLayout(false);
		this.groupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtNewProjectCodeAlias).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNewProjectAddress).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNewProjectNameE).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNewProjectNameC).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNewProjectCode).EndInit();
		this.groupBox1.ResumeLayout(false);
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
}
