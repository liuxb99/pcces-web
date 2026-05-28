using System;
using System.Collections;
using System.ComponentModel;
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

public class FormProjectEdit : Form
{
	private const string CallFormHelp = "FormProjectEdit";

	private Panel panel5;

	private UltraLabel ultraLabel7;

	private UltraLabel ultraLabel6;

	private Panel panel4;

	private UltraTextEditor txtProjectAddress;

	private UltraLabel ultraLabel11;

	private UltraTextEditor txtProjectEName;

	private UltraLabel ultraLabel10;

	private UltraTextEditor txtProjectCName;

	private UltraTextEditor txtProjectCode;

	private UltraLabel ultraLabel9;

	private UltraLabel ultraLabel8;

	private Panel panel3;

	private GroupBox groupBox2;

	private UltraButton B_Btn_Cncl;

	private UltraButton B_Btn_Next;

	private Container components = null;

	private string F_UserID;

	private DataTable DT1 = new DataTable();

	private string F_ProjectCode;

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

	public string _ProjectCode
	{
		get
		{
			return F_ProjectCode;
		}
		set
		{
			F_ProjectCode = value;
		}
	}

	public FormProjectEdit()
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
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.Project.FormProjectEdit));
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		this.panel5 = new System.Windows.Forms.Panel();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.panel4 = new System.Windows.Forms.Panel();
		this.panel3 = new System.Windows.Forms.Panel();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.B_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.B_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.txtProjectAddress = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel11 = new Infragistics.Win.Misc.UltraLabel();
		this.txtProjectEName = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel10 = new Infragistics.Win.Misc.UltraLabel();
		this.txtProjectCName = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtProjectCode = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel9 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel8 = new Infragistics.Win.Misc.UltraLabel();
		this.panel5.SuspendLayout();
		this.panel4.SuspendLayout();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtProjectAddress).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtProjectEName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtProjectCName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtProjectCode).BeginInit();
		base.SuspendLayout();
		this.panel5.BackColor = System.Drawing.Color.White;
		this.panel5.Controls.Add(this.ultraLabel7);
		this.panel5.Controls.Add(this.ultraLabel6);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel5.Location = new System.Drawing.Point(0, 0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(656, 60);
		this.panel5.TabIndex = 12;
		appearance1.BackColor = System.Drawing.Color.White;
		this.ultraLabel7.Appearance = appearance1;
		this.ultraLabel7.Location = new System.Drawing.Point(47, 34);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel7.TabIndex = 3;
		this.ultraLabel7.Text = "你可以修改專案的基本資料，工程代碼不可變更";
		appearance2.BackColor = System.Drawing.Color.White;
		this.ultraLabel6.Appearance = appearance2;
		this.ultraLabel6.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel6.Location = new System.Drawing.Point(16, 12);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel6.TabIndex = 2;
		this.ultraLabel6.Text = "專案基本資料";
		this.panel4.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel4.Controls.Add(this.panel3);
		this.panel4.Controls.Add(this.txtProjectAddress);
		this.panel4.Controls.Add(this.ultraLabel11);
		this.panel4.Controls.Add(this.txtProjectEName);
		this.panel4.Controls.Add(this.ultraLabel10);
		this.panel4.Controls.Add(this.txtProjectCName);
		this.panel4.Controls.Add(this.txtProjectCode);
		this.panel4.Controls.Add(this.ultraLabel9);
		this.panel4.Controls.Add(this.ultraLabel8);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel4.Location = new System.Drawing.Point(0, 0);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(656, 445);
		this.panel4.TabIndex = 13;
		this.panel3.Controls.Add(this.groupBox2);
		this.panel3.Controls.Add(this.B_Btn_Cncl);
		this.panel3.Controls.Add(this.B_Btn_Next);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel3.Location = new System.Drawing.Point(0, 401);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(656, 44);
		this.panel3.TabIndex = 12;
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(656, 8);
		this.groupBox2.TabIndex = 4;
		this.groupBox2.TabStop = false;
		appearance3.Image = resources.GetObject("appearance3.Image");
		appearance3.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.B_Btn_Cncl.Appearance = appearance3;
		this.B_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.B_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.B_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.B_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.B_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.B_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.B_Btn_Cncl.Location = new System.Drawing.Point(564, 10);
		this.B_Btn_Cncl.Name = "B_Btn_Cncl";
		this.B_Btn_Cncl.ShowFocusRect = false;
		this.B_Btn_Cncl.ShowOutline = false;
		this.B_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.B_Btn_Cncl.SupportThemes = false;
		this.B_Btn_Cncl.TabIndex = 2;
		this.B_Btn_Cncl.Text = "取消";
		appearance4.Image = resources.GetObject("appearance4.Image");
		appearance4.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.B_Btn_Next.Appearance = appearance4;
		this.B_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.B_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.B_Btn_Next.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.B_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.B_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.B_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.B_Btn_Next.Location = new System.Drawing.Point(472, 10);
		this.B_Btn_Next.Name = "B_Btn_Next";
		this.B_Btn_Next.ShowFocusRect = false;
		this.B_Btn_Next.ShowOutline = false;
		this.B_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.B_Btn_Next.SupportThemes = false;
		this.B_Btn_Next.TabIndex = 1;
		this.B_Btn_Next.Text = "確定";
		this.B_Btn_Next.Click += new System.EventHandler(B_Btn_Next_Click);
		this.txtProjectAddress.Location = new System.Drawing.Point(52, 312);
		this.txtProjectAddress.MaxLength = 200;
		this.txtProjectAddress.Multiline = true;
		this.txtProjectAddress.Name = "txtProjectAddress";
		this.txtProjectAddress.Scrollbars = System.Windows.Forms.ScrollBars.Vertical;
		this.txtProjectAddress.Size = new System.Drawing.Size(564, 45);
		this.txtProjectAddress.TabIndex = 11;
		this.txtProjectAddress.Text = "[txtProjectAddress]";
		this.txtProjectAddress.Validating += new System.ComponentModel.CancelEventHandler(txtProjectCode_Validating);
		this.ultraLabel11.Location = new System.Drawing.Point(48, 292);
		this.ultraLabel11.Name = "ultraLabel11";
		this.ultraLabel11.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel11.TabIndex = 10;
		this.ultraLabel11.Text = "工程地點:";
		this.txtProjectEName.Location = new System.Drawing.Point(52, 237);
		this.txtProjectEName.MaxLength = 200;
		this.txtProjectEName.Multiline = true;
		this.txtProjectEName.Name = "txtProjectEName";
		this.txtProjectEName.Scrollbars = System.Windows.Forms.ScrollBars.Vertical;
		this.txtProjectEName.Size = new System.Drawing.Size(564, 45);
		this.txtProjectEName.TabIndex = 9;
		this.txtProjectEName.Text = "[txtProjectEName]";
		this.txtProjectEName.Validating += new System.ComponentModel.CancelEventHandler(txtProjectCode_Validating);
		this.ultraLabel10.Location = new System.Drawing.Point(48, 216);
		this.ultraLabel10.Name = "ultraLabel10";
		this.ultraLabel10.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel10.TabIndex = 8;
		this.ultraLabel10.Text = "Project Name (English):";
		this.txtProjectCName.Location = new System.Drawing.Point(52, 159);
		this.txtProjectCName.MaxLength = 200;
		this.txtProjectCName.Multiline = true;
		this.txtProjectCName.Name = "txtProjectCName";
		this.txtProjectCName.Scrollbars = System.Windows.Forms.ScrollBars.Vertical;
		this.txtProjectCName.Size = new System.Drawing.Size(564, 45);
		this.txtProjectCName.TabIndex = 7;
		this.txtProjectCName.Text = "[txtProjectCName]";
		this.txtProjectCName.Validating += new System.ComponentModel.CancelEventHandler(txtProjectCode_Validating);
		this.txtProjectCode.Enabled = false;
		this.txtProjectCode.Location = new System.Drawing.Point(52, 100);
		this.txtProjectCode.MaxLength = 20;
		this.txtProjectCode.Name = "txtProjectCode";
		this.txtProjectCode.Size = new System.Drawing.Size(564, 21);
		this.txtProjectCode.TabIndex = 6;
		this.txtProjectCode.Text = "[txtProjectCode]";
		this.txtProjectCode.Validating += new System.ComponentModel.CancelEventHandler(txtProjectCode_Validating);
		this.ultraLabel9.Location = new System.Drawing.Point(48, 138);
		this.ultraLabel9.Name = "ultraLabel9";
		this.ultraLabel9.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel9.TabIndex = 5;
		this.ultraLabel9.Text = "工程名稱:";
		this.ultraLabel8.Location = new System.Drawing.Point(48, 80);
		this.ultraLabel8.Name = "ultraLabel8";
		this.ultraLabel8.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel8.TabIndex = 4;
		this.ultraLabel8.Text = "工程代碼:";
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		base.CancelButton = this.B_Btn_Cncl;
		base.ClientSize = new System.Drawing.Size(656, 445);
		base.Controls.Add(this.panel5);
		base.Controls.Add(this.panel4);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.Name = "FormProjectEdit";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "專案基本資料修改";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormProjectEdit_KeyDown);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormProjectEdit_FormClosing);
		base.Load += new System.EventHandler(FormProjectEdit_Load);
		this.panel5.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtProjectAddress).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtProjectEName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtProjectCName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtProjectCode).EndInit();
		base.ResumeLayout(false);
	}

	private void FormProjectEdit_Load(object sender, EventArgs e)
	{
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("WinFORM 基本工料");
		PubProject PUB_PROJ = new PubProject(aArr);
		PUB_PROJ.ps_projectCode = F_ProjectCode;
		DT1 = PUB_PROJ.ListItem(" a.projectCode ='" + F_ProjectCode + "' ");
		if (DT1.Rows.Count > 0)
		{
			txtProjectCode.Text = F_ProjectCode;
			txtProjectCName.Text = DT1.Rows[0]["projCName"].ToString();
			txtProjectEName.Text = DT1.Rows[0]["projEName"].ToString();
			txtProjectAddress.Text = DT1.Rows[0]["projAddress"].ToString();
		}
		LoadingScreen();
	}

	private void LoadingScreen()
	{
		string Status = CommonMethods.GetIniValue("ProjectEdit", "WindowState");
		if (Status == "Maximized")
		{
			base.WindowState = FormWindowState.Maximized;
		}
		int iLoc_X = PubTools.Str2Int(CommonMethods.GetIniValue("ProjectEdit", "PK_LocationX"));
		int iLoc_Y = PubTools.Str2Int(CommonMethods.GetIniValue("ProjectEdit", "PK_LocationY"));
		int iSiz_W = PubTools.Str2Int(CommonMethods.GetIniValue("ProjectEdit", "PK_Width"));
		int iSiz_H = PubTools.Str2Int(CommonMethods.GetIniValue("ProjectEdit", "PK_Height"));
		if (iLoc_X > 0 && iLoc_Y > 0)
		{
			base.Location = new Point(iLoc_X, iLoc_Y);
		}
		if (iSiz_W > 0)
		{
			base.Width = iSiz_W;
		}
		if (iSiz_H > 0)
		{
			base.Height = iSiz_H;
		}
	}

	private void B_Btn_Next_Click(object sender, EventArgs e)
	{
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("WinFORM 基本工料");
		PubProject PUB_PROJ = new PubProject(aArr);
		PUB_PROJ.ps_projectCode = F_ProjectCode;
		PUB_PROJ.ps_projectNameC = txtProjectCName.Text.Trim();
		PUB_PROJ.ps_projectNameE = txtProjectEName.Text.Trim();
		PUB_PROJ.ps_projectAddress = txtProjectAddress.Text.Trim();
		if (PUB_PROJ.UpdItem() == -2)
		{
			MessageBox.Show(this, "該筆資料已不存在, 可能已經被其他使用者刪除!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void txtProjectCode_Validating(object sender, CancelEventArgs e)
	{
		if (!CommonMethods.CheckValidString((sender as UltraTextEditor).Text))
		{
			e.Cancel = true;
		}
	}

	private void FormProjectEdit_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (base.WindowState != FormWindowState.Maximized)
		{
			CommonMethods.WriteIniValue("ProjectEdit", "PK_LocationX", base.Location.X.ToString());
			CommonMethods.WriteIniValue("ProjectEdit", "PK_LocationY", base.Location.Y.ToString());
			CommonMethods.WriteIniValue("ProjectEdit", "PK_Width", base.Size.Width.ToString());
			CommonMethods.WriteIniValue("ProjectEdit", "PK_Height", base.Size.Height.ToString());
		}
		CommonMethods.WriteIniValue("ProjectEdit", "WindowState", base.WindowState.ToString());
	}

	private void FormProjectEdit_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormProjectEdit");
		}
	}
}
