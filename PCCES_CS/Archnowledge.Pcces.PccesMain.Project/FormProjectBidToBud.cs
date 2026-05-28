using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.STDClass;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace Archnowledge.Pcces.PccesMain.Project;

public class FormProjectBidToBud : Form
{
	private string F_UserID = "";

	private string F_OldProjectCode = "";

	private string F_OldProjectName = "";

	private string F_OldProjectNameE = "";

	private string F_OldProjectAddr = "";

	private bool F_IsBid = false;

	private bool F_IsBud = false;

	private string F_PID = "";

	private Panel panel5;

	private UltraLabel ultraLabel7;

	private UltraLabel ultraLabel6;

	private Panel panel2;

	private UltraButton D_Btn_Fnsh;

	private GroupBox groupBox2;

	private UltraButton A_Btn_Cncl;

	private Panel panel1;

	private GroupBox groupBox1;

	private UltraLabel lblOldProjectCode;

	private UltraLabel lblOldProjectName;

	private GroupBox groupBox3;

	private UltraTextEditor txtNewProjectCode;

	private UltraLabel ultraLabel2;

	private UltraTextEditor txtNewProjectNameC;

	private UltraLabel lblProjectCode;

	private Container components = null;

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

	public FormProjectBidToBud()
	{
		InitializeComponent();
		F_PID = ConfigurationManager.AppSettings["PID"];
	}

	private void FormProjectClone_Load(object sender, EventArgs e)
	{
		lblOldProjectCode.Text = "工程代碼：" + F_OldProjectCode;
		lblOldProjectName.Text = "工程名稱：" + F_OldProjectName;
		txtNewProjectCode.Text = F_OldProjectCode;
		txtNewProjectNameC.Text = F_OldProjectName;
		txtNewProjectCode.Focus();
	}

	private void D_Btn_Fnsh_Click(object sender, EventArgs e)
	{
		if (txtNewProjectCode.Text.Trim() == "")
		{
			MessageBox.Show(this, "預算的工程代碼不可空白", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("複製專案");
		Archnowledge.Pcces.BUDClass.Project ProjCom = new Archnowledge.Pcces.BUDClass.Project(aArr);
		ProjCom.ps_srckind = "BUD";
		DataTable DT_BUD = ProjCom.ListItem("", txtNewProjectCode.Text.Trim());
		if (DT_BUD.Rows.Count > 0)
		{
			MessageBox.Show(this, "預算書，已有相同工程代碼，請重新輸入", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		bool IsCloneSuccess = false;
		ProjCom.ps_srckind = "BID";
		ModifyDB StdCom = new ModifyDB("", aArr);
		string sBud = "Insert Into budProject(ProjectCode, mainCode, projectNameC, projectNameE, projectAddress, accountCode1,projectCodeAlias, accountCode2, buyMode, workMode, expectDaily, projectScope, workUnit, projamt, AutoCalcCost, UseIR, CloseBidDate, IsQtyModifiable, BudStartYear, BudEndYear, ExpectStartDate, expectFinishDate,  eightM1, eightM2, eightM3, eightM4, eightM5, eightM6, eightM7 ,eightM8, city, mainCName,IsType) Select '" + txtNewProjectCode.Text.Trim() + "', mainCode, N'" + txtNewProjectNameC.Text.Trim() + "', projectNameE, projectAddress, accountCode1,projectCodeAlias, accountCode2, buyMode, workMode, expectDaily, projectScope, workUnit, projamt, AutoCalcCost, UseIR, CloseBidDate, IsQtyModifiable, BudStartYear, BudEndYear, ExpectStartDate, expectFinishDate,  eightM1, eightM2, eightM3, eightM4, eightM5, eightM6, eightM7 ,eightM8, city, mainCName,'3' From bidProject Where ProjectCode ='" + F_OldProjectCode + "' ";
		StdCom.DBUpd(sBud);
		if (!ProjCom.CopyProjBidToBud(txtNewProjectCode.Text.Trim(), F_OldProjectCode))
		{
			MessageBox.Show(this, "標單轉決標預算書失敗!", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return;
		}
		string sSQL = "select * from pubProject where ProjectCode ='" + txtNewProjectCode.Text.Trim() + "' ";
		DataTable dt = StdCom.DBList(sSQL);
		if (dt.Rows.Count == 0)
		{
			sSQL = "Insert Into pubProject(projectCode, hubID, projCName, projEName, projAddress, projectCodeAlias) Select '" + txtNewProjectCode.Text.Trim() + "', hubID, N'" + txtNewProjectNameC.Text.Trim() + "', projEName, projAddress, projectCodeAlias  From pubProject Where ProjectCode ='" + F_OldProjectCode + "' ";
			StdCom.DBUpd(sSQL);
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
			if (!CommonMethods.EngNumValid(txtNewProjectCode.Text[i]))
			{
				MessageBox.Show(this, "不可輸入非數字或英文字母及的字", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.Project.FormProjectClone));
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
		this.panel5 = new System.Windows.Forms.Panel();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.D_Btn_Fnsh = new Infragistics.Win.Misc.UltraButton();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.A_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.panel1 = new System.Windows.Forms.Panel();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
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
		this.groupBox3.SuspendLayout();
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
		this.ultraLabel6.Text = "標單轉決標預算書";
		this.panel2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel2.Controls.Add(this.D_Btn_Fnsh);
		this.panel2.Controls.Add(this.groupBox2);
		this.panel2.Controls.Add(this.A_Btn_Cncl);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 276);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(464, 44);
		this.panel2.TabIndex = 17;
		this.D_Btn_Fnsh.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance3.Image = resources.GetObject("appearance3.Image");
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
		this.groupBox2.Size = new System.Drawing.Size(464, 4);
		this.groupBox2.TabIndex = 3;
		this.groupBox2.TabStop = false;
		this.A_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance4.Image = resources.GetObject("appearance4.Image");
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
		this.panel1.Controls.Add(this.groupBox3);
		this.panel1.Controls.Add(this.groupBox1);
		this.panel1.Location = new System.Drawing.Point(0, 48);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(464, 232);
		this.panel1.TabIndex = 18;
		this.groupBox3.Controls.Add(this.txtNewProjectNameC);
		this.groupBox3.Controls.Add(this.ultraLabel2);
		this.groupBox3.Controls.Add(this.lblProjectCode);
		this.groupBox3.Controls.Add(this.txtNewProjectCode);
		this.groupBox3.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		this.groupBox3.Location = new System.Drawing.Point(8, 112);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(448, 104);
		this.groupBox3.TabIndex = 1;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "決標預算專案資料";
		appearance5.ForeColor = System.Drawing.Color.Black;
		this.txtNewProjectNameC.Appearance = appearance5;
		this.txtNewProjectNameC.Location = new System.Drawing.Point(136, 62);
		this.txtNewProjectNameC.MaxLength = 200;
		this.txtNewProjectNameC.Name = "txtNewProjectNameC";
		this.txtNewProjectNameC.Size = new System.Drawing.Size(296, 21);
		this.txtNewProjectNameC.TabIndex = 3;
		this.txtNewProjectNameC.Leave += new System.EventHandler(txtNewProjectNameC_Leave);
		appearance6.ForeColor = System.Drawing.Color.Black;
		appearance6.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel2.Appearance = appearance6;
		this.ultraLabel2.Location = new System.Drawing.Point(48, 64);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(88, 23);
		this.ultraLabel2.TabIndex = 2;
		this.ultraLabel2.Text = "工程名稱：";
		appearance7.ForeColor = System.Drawing.Color.Black;
		appearance7.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblProjectCode.Appearance = appearance7;
		this.lblProjectCode.Location = new System.Drawing.Point(8, 32);
		this.lblProjectCode.Name = "lblProjectCode";
		this.lblProjectCode.Size = new System.Drawing.Size(128, 23);
		this.lblProjectCode.TabIndex = 1;
		this.lblProjectCode.Text = "     工程代碼：";
		appearance8.ForeColor = System.Drawing.Color.Black;
		this.txtNewProjectCode.Appearance = appearance8;
		this.txtNewProjectCode.Location = new System.Drawing.Point(136, 30);
		this.txtNewProjectCode.MaxLength = 20;
		this.txtNewProjectCode.Name = "txtNewProjectCode";
		this.txtNewProjectCode.Size = new System.Drawing.Size(296, 21);
		this.txtNewProjectCode.TabIndex = 0;
		this.txtNewProjectCode.Leave += new System.EventHandler(txtNewProjectCode_Leave);
		this.txtNewProjectCode.Validating += new System.ComponentModel.CancelEventHandler(txtNewProjectCode_Validating);
		this.groupBox1.Controls.Add(this.lblOldProjectName);
		this.groupBox1.Controls.Add(this.lblOldProjectCode);
		this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		this.groupBox1.Location = new System.Drawing.Point(8, 6);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(448, 100);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "標單原始專案";
		appearance9.ForeColor = System.Drawing.Color.Black;
		appearance9.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblOldProjectName.Appearance = appearance9;
		this.lblOldProjectName.Location = new System.Drawing.Point(11, 50);
		this.lblOldProjectName.Name = "lblOldProjectName";
		this.lblOldProjectName.Size = new System.Drawing.Size(424, 44);
		this.lblOldProjectName.TabIndex = 1;
		this.lblOldProjectName.Text = "工程名稱：";
		appearance10.ForeColor = System.Drawing.Color.Black;
		appearance10.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblOldProjectCode.Appearance = appearance10;
		this.lblOldProjectCode.Location = new System.Drawing.Point(11, 24);
		this.lblOldProjectCode.Name = "lblOldProjectCode";
		this.lblOldProjectCode.Size = new System.Drawing.Size(424, 23);
		this.lblOldProjectCode.TabIndex = 0;
		this.lblOldProjectCode.Text = "工程代碼：";
		base.AcceptButton = this.D_Btn_Fnsh;
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		base.CancelButton = this.A_Btn_Cncl;
		base.ClientSize = new System.Drawing.Size(464, 320);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel5);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormProjectClone";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "標單轉決標預算書";
		base.Load += new System.EventHandler(FormProjectClone_Load);
		this.panel5.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		this.groupBox3.ResumeLayout(false);
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
