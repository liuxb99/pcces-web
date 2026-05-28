using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.PccesMain.ShellLib;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormBudgetExp_Wzd_Help1 : Form
{
	private Panel panel4;

	private GroupBox groupBox2;

	private UltraButton btnOK;

	private Panel panel1;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	private UltraLabel ultraLabel3;

	private UltraLabel ultraLabel4;

	private UltraLabel ultraLabel5;

	private UltraLabel ultraLabel6;

	private UltraLabel ultraLabel7;

	private UltraLabel ultraLabel8;

	private UltraLabel ultraLabel9;

	private UltraLabel ultraLabel10;

	private LinkLabel llbXMLStandard;

	private UltraLabel ultraLabel11;

	private LinkLabel llbApplicationProcedure;

	private UltraLabel ultraLabel12;

	private UltraLabel ultraLabel13;

	private UltraLabel ultraLabel14;

	private Container components = null;

	public FormBudgetExp_Wzd_Help1()
	{
		InitializeComponent();
	}

	private void llbXMLStandard_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		ShellExecute SHExe = new ShellExecute();
		SHExe.OwnerHandle = base.Handle;
		SHExe.Path = "http://210.69.177.70/XMLPlan/";
		SHExe.Execute();
	}

	private void llbApplicationProcedure_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		ShellExecute SHExe = new ShellExecute();
		SHExe.OwnerHandle = base.Handle;
		SHExe.Path = "http://210.69.177.70/XMLPlan/plan/basic_flowchart.jsp";
		SHExe.Execute();
	}

	private void InitializeComponent()
	{
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormBudgetExp_Wzd_Help1));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		this.panel4 = new System.Windows.Forms.Panel();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.btnOK = new Infragistics.Win.Misc.UltraButton();
		this.panel1 = new System.Windows.Forms.Panel();
		this.ultraLabel14 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel13 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel12 = new Infragistics.Win.Misc.UltraLabel();
		this.llbApplicationProcedure = new System.Windows.Forms.LinkLabel();
		this.ultraLabel11 = new Infragistics.Win.Misc.UltraLabel();
		this.llbXMLStandard = new System.Windows.Forms.LinkLabel();
		this.ultraLabel10 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel8 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel9 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.panel4.SuspendLayout();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.panel4.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel4.Controls.Add(this.groupBox2);
		this.panel4.Controls.Add(this.btnOK);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel4.Location = new System.Drawing.Point(0, 388);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(474, 44);
		this.panel4.TabIndex = 11;
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(474, 8);
		this.groupBox2.TabIndex = 3;
		this.groupBox2.TabStop = false;
		this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnOK.Appearance = appearance1;
		this.btnOK.BackColor = System.Drawing.SystemColors.Control;
		this.btnOK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnOK.Font = new System.Drawing.Font("細明體", 11f);
		this.btnOK.ImageSize = new System.Drawing.Size(20, 20);
		this.btnOK.ImageTransparentColor = System.Drawing.Color.White;
		this.btnOK.Location = new System.Drawing.Point(378, 10);
		this.btnOK.Name = "btnOK";
		this.btnOK.ShowFocusRect = false;
		this.btnOK.ShowOutline = false;
		this.btnOK.Size = new System.Drawing.Size(88, 31);
		this.btnOK.SupportThemes = false;
		this.btnOK.TabIndex = 2;
		this.btnOK.Text = "確定";
		this.panel1.BackColor = System.Drawing.Color.White;
		this.panel1.Controls.Add(this.ultraLabel14);
		this.panel1.Controls.Add(this.ultraLabel13);
		this.panel1.Controls.Add(this.ultraLabel12);
		this.panel1.Controls.Add(this.llbApplicationProcedure);
		this.panel1.Controls.Add(this.ultraLabel11);
		this.panel1.Controls.Add(this.llbXMLStandard);
		this.panel1.Controls.Add(this.ultraLabel10);
		this.panel1.Controls.Add(this.ultraLabel7);
		this.panel1.Controls.Add(this.ultraLabel8);
		this.panel1.Controls.Add(this.ultraLabel9);
		this.panel1.Controls.Add(this.ultraLabel6);
		this.panel1.Controls.Add(this.ultraLabel5);
		this.panel1.Controls.Add(this.ultraLabel4);
		this.panel1.Controls.Add(this.ultraLabel3);
		this.panel1.Controls.Add(this.ultraLabel2);
		this.panel1.Controls.Add(this.ultraLabel1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(474, 388);
		this.panel1.TabIndex = 12;
		appearance2.ForeColor = System.Drawing.Color.Red;
		this.ultraLabel14.Appearance = appearance2;
		this.ultraLabel14.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel14.Location = new System.Drawing.Point(8, 16);
		this.ultraLabel14.Name = "ultraLabel14";
		this.ultraLabel14.Size = new System.Drawing.Size(432, 23);
		this.ultraLabel14.TabIndex = 15;
		this.ultraLabel14.Text = "此專案的取位原則不符合工程會公佈之公共工程XML標準格式";
		this.ultraLabel13.Location = new System.Drawing.Point(16, 344);
		this.ultraLabel13.Name = "ultraLabel13";
		this.ultraLabel13.Size = new System.Drawing.Size(400, 23);
		this.ultraLabel13.TabIndex = 14;
		this.ultraLabel13.Text = "或洽工程會企劃處 (02)87897640";
		this.ultraLabel12.Location = new System.Drawing.Point(208, 320);
		this.ultraLabel12.Name = "ultraLabel12";
		this.ultraLabel12.Size = new System.Drawing.Size(40, 23);
		this.ultraLabel12.TabIndex = 13;
		this.ultraLabel12.Text = "提出";
		this.llbApplicationProcedure.Location = new System.Drawing.Point(142, 320);
		this.llbApplicationProcedure.Name = "llbApplicationProcedure";
		this.llbApplicationProcedure.Size = new System.Drawing.Size(74, 23);
		this.llbApplicationProcedure.TabIndex = 12;
		((System.Windows.Forms.Label)this.llbApplicationProcedure).TabStop = true;
		this.llbApplicationProcedure.Text = "申請流程";
		this.llbApplicationProcedure.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(llbApplicationProcedure_LinkClicked);
		this.ultraLabel11.Location = new System.Drawing.Point(16, 320);
		this.ultraLabel11.Name = "ultraLabel11";
		this.ultraLabel11.Size = new System.Drawing.Size(136, 23);
		this.ultraLabel11.TabIndex = 11;
		this.ultraLabel11.Text = "若有其他需求請依";
		this.llbXMLStandard.Location = new System.Drawing.Point(157, 296);
		this.llbXMLStandard.Name = "llbXMLStandard";
		this.llbXMLStandard.Size = new System.Drawing.Size(179, 23);
		this.llbXMLStandard.TabIndex = 10;
		((System.Windows.Forms.Label)this.llbXMLStandard).TabStop = true;
		this.llbXMLStandard.Text = "公共工程資料交換標準";
		this.llbXMLStandard.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(llbXMLStandard_LinkClicked);
		this.ultraLabel10.Location = new System.Drawing.Point(16, 296);
		this.ultraLabel10.Name = "ultraLabel10";
		this.ultraLabel10.Size = new System.Drawing.Size(152, 23);
		this.ultraLabel10.TabIndex = 9;
		this.ultraLabel10.Text = "其餘詳細規定請參閱";
		this.ultraLabel7.Location = new System.Drawing.Point(48, 256);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(120, 23);
		this.ultraLabel7.TabIndex = 8;
		this.ultraLabel7.Text = "複價: 小數 2位";
		this.ultraLabel8.Location = new System.Drawing.Point(48, 232);
		this.ultraLabel8.Name = "ultraLabel8";
		this.ultraLabel8.Size = new System.Drawing.Size(120, 23);
		this.ultraLabel8.TabIndex = 7;
		this.ultraLabel8.Text = "單價: 小數 2位";
		this.ultraLabel9.Location = new System.Drawing.Point(48, 208);
		this.ultraLabel9.Name = "ultraLabel9";
		this.ultraLabel9.Size = new System.Drawing.Size(120, 23);
		this.ultraLabel9.TabIndex = 6;
		this.ultraLabel9.Text = "數量: 小數 4位";
		this.ultraLabel6.Location = new System.Drawing.Point(16, 184);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(408, 23);
		this.ultraLabel6.TabIndex = 5;
		this.ultraLabel6.Text = "其中針對單價分析表小數取位原則有以下規範";
		this.ultraLabel5.Location = new System.Drawing.Point(48, 152);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(120, 23);
		this.ultraLabel5.TabIndex = 4;
		this.ultraLabel5.Text = "複價: 小數 2位";
		this.ultraLabel4.Location = new System.Drawing.Point(48, 128);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(120, 23);
		this.ultraLabel4.TabIndex = 3;
		this.ultraLabel4.Text = "單價: 小數 2位";
		this.ultraLabel3.Location = new System.Drawing.Point(48, 104);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(120, 23);
		this.ultraLabel3.TabIndex = 2;
		this.ultraLabel3.Text = "數量: 小數 4位";
		this.ultraLabel2.Location = new System.Drawing.Point(16, 80);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(408, 23);
		this.ultraLabel2.TabIndex = 1;
		this.ultraLabel2.Text = "其中針對標單詳細表小數取位原則有以下規範";
		appearance3.ForeColor = System.Drawing.Color.Blue;
		this.ultraLabel1.Appearance = appearance3;
		this.ultraLabel1.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel1.Location = new System.Drawing.Point(8, 48);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(416, 24);
		this.ultraLabel1.TabIndex = 0;
		this.ultraLabel1.Text = "XML標準格式小數取位原則如下";
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		base.ClientSize = new System.Drawing.Size(474, 432);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.panel4);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormBudgetExp_Wzd_Help1";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "提示說明";
		this.panel4.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
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
