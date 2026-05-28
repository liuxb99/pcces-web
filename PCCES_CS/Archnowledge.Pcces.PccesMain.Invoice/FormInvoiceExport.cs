using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.CTRClass;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTabControl;

namespace Archnowledge.Pcces.PccesMain.Invoice;

public class FormInvoiceExport : Form
{
	private const string CallFormHelp = "FormInvoiceExport";

	private UltraTabControl TabCtrl;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl Tab_B;

	private Panel panel3;

	private UltraLabel ultraLabel5;

	private UltraButton ultraButton4;

	private UltraTextEditor txtExpDirFile;

	private Panel panel2;

	private GroupBox groupBox2;

	private UltraButton B_Btn_Cncl;

	private UltraButton B_Btn_Next;

	private Panel panel5;

	private UltraLabel ultraLabel7;

	private UltraLabel ultraLabel6;

	private UltraTabPageControl Tab_C;

	private UltraLabel lblWait;

	private Panel panel7;

	private GroupBox groupBox4;

	private Panel panel4;

	private UltraLabel ultraLabel9;

	private UltraTabPageControl Tab_D;

	private UltraLabel lblEXCEL;

	private UltraLabel ultraLabel14;

	private UltraLabel ultraLabel13;

	private UltraLabel ultraLabel12;

	private Panel panel6;

	private GroupBox groupBox3;

	private UltraButton D_Btn_Fnsh;

	private UltraLabel ultraLabel1;

	private SaveFileDialog saveFileDialog1;

	private UltraLabel lbl_Issue;

	private Container components = null;

	private string F_UserID;

	private string F_ProjectCode;

	private string F_SubProjectCode = "";

	private string F_Issue;

	private string ls_sProj;

	private string ls_ProjectCode;

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

	public string _SubProjectCode
	{
		get
		{
			return F_SubProjectCode;
		}
		set
		{
			F_SubProjectCode = value;
		}
	}

	public string _Issue
	{
		get
		{
			return F_Issue;
		}
		set
		{
			F_Issue = value;
		}
	}

	public FormInvoiceExport()
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
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.Invoice.FormInvoiceExport));
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		this.Tab_B = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel3 = new System.Windows.Forms.Panel();
		this.lbl_Issue = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraButton4 = new Infragistics.Win.Misc.UltraButton();
		this.txtExpDirFile = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.panel2 = new System.Windows.Forms.Panel();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.B_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.B_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.panel5 = new System.Windows.Forms.Panel();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_C = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.lblWait = new Infragistics.Win.Misc.UltraLabel();
		this.panel7 = new System.Windows.Forms.Panel();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.panel4 = new System.Windows.Forms.Panel();
		this.ultraLabel9 = new Infragistics.Win.Misc.UltraLabel();
		this.Tab_D = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.lblEXCEL = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel14 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel13 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel12 = new Infragistics.Win.Misc.UltraLabel();
		this.panel6 = new System.Windows.Forms.Panel();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.D_Btn_Fnsh = new Infragistics.Win.Misc.UltraButton();
		this.TabCtrl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
		this.Tab_B.SuspendLayout();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtExpDirFile).BeginInit();
		this.panel2.SuspendLayout();
		this.panel5.SuspendLayout();
		this.Tab_C.SuspendLayout();
		this.panel7.SuspendLayout();
		this.panel4.SuspendLayout();
		this.Tab_D.SuspendLayout();
		this.panel6.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TabCtrl).BeginInit();
		this.TabCtrl.SuspendLayout();
		base.SuspendLayout();
		this.Tab_B.Controls.Add(this.panel3);
		this.Tab_B.Controls.Add(this.panel2);
		this.Tab_B.Controls.Add(this.panel5);
		this.Tab_B.Location = new System.Drawing.Point(0, 0);
		this.Tab_B.Name = "Tab_B";
		this.Tab_B.Size = new System.Drawing.Size(516, 369);
		this.panel3.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel3.Controls.Add(this.lbl_Issue);
		this.panel3.Controls.Add(this.ultraLabel1);
		this.panel3.Controls.Add(this.ultraLabel5);
		this.panel3.Controls.Add(this.ultraButton4);
		this.panel3.Controls.Add(this.txtExpDirFile);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel3.Location = new System.Drawing.Point(0, 60);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(516, 265);
		this.panel3.TabIndex = 14;
		appearance1.ForeColor = System.Drawing.Color.Red;
		this.lbl_Issue.Appearance = appearance1;
		this.lbl_Issue.Location = new System.Drawing.Point(136, 32);
		this.lbl_Issue.Name = "lbl_Issue";
		this.lbl_Issue.Size = new System.Drawing.Size(125, 20);
		this.lbl_Issue.TabIndex = 6;
		this.lbl_Issue.Text = "【1】";
		this.ultraLabel1.Location = new System.Drawing.Point(11, 32);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(125, 20);
		this.ultraLabel1.TabIndex = 5;
		this.ultraLabel1.Text = "匯出的計價期別:";
		this.ultraLabel5.Location = new System.Drawing.Point(11, 112);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel5.TabIndex = 4;
		this.ultraLabel5.Text = "存放的目錄及檔名:";
		appearance2.FontData.Name = "Arial";
		appearance2.FontData.SizeInPoints = 8f;
		this.ultraButton4.Appearance = appearance2;
		this.ultraButton4.BackColor = System.Drawing.SystemColors.Control;
		this.ultraButton4.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.ultraButton4.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraButton4.Location = new System.Drawing.Point(459, 136);
		this.ultraButton4.Name = "ultraButton4";
		this.ultraButton4.ShowFocusRect = false;
		this.ultraButton4.ShowOutline = false;
		this.ultraButton4.Size = new System.Drawing.Size(48, 24);
		this.ultraButton4.SupportThemes = false;
		this.ultraButton4.TabIndex = 1;
		this.ultraButton4.Text = "瀏覽...";
		this.ultraButton4.Click += new System.EventHandler(ultraButton4_Click);
		appearance3.FontData.Name = "細明體";
		appearance3.FontData.SizeInPoints = 11f;
		this.txtExpDirFile.Appearance = appearance3;
		this.txtExpDirFile.Location = new System.Drawing.Point(12, 136);
		this.txtExpDirFile.Name = "txtExpDirFile";
		this.txtExpDirFile.Size = new System.Drawing.Size(448, 24);
		this.txtExpDirFile.TabIndex = 0;
		this.panel2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel2.Controls.Add(this.groupBox2);
		this.panel2.Controls.Add(this.B_Btn_Cncl);
		this.panel2.Controls.Add(this.B_Btn_Next);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 325);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(516, 44);
		this.panel2.TabIndex = 13;
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(516, 8);
		this.groupBox2.TabIndex = 3;
		this.groupBox2.TabStop = false;
		appearance4.Image = resources.GetObject("appearance4.Image");
		appearance4.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.B_Btn_Cncl.Appearance = appearance4;
		this.B_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.B_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.B_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.B_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.B_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.B_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.B_Btn_Cncl.Location = new System.Drawing.Point(416, 9);
		this.B_Btn_Cncl.Name = "B_Btn_Cncl";
		this.B_Btn_Cncl.ShowFocusRect = false;
		this.B_Btn_Cncl.ShowOutline = false;
		this.B_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.B_Btn_Cncl.SupportThemes = false;
		this.B_Btn_Cncl.TabIndex = 2;
		this.B_Btn_Cncl.Text = "取消";
		appearance5.Image = resources.GetObject("appearance5.Image");
		appearance5.ImageHAlign = Infragistics.Win.HAlign.Right;
		appearance5.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.B_Btn_Next.Appearance = appearance5;
		this.B_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.B_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.B_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.B_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.B_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.B_Btn_Next.Location = new System.Drawing.Point(324, 9);
		this.B_Btn_Next.Name = "B_Btn_Next";
		this.B_Btn_Next.ShowFocusRect = false;
		this.B_Btn_Next.ShowOutline = false;
		this.B_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.B_Btn_Next.SupportThemes = false;
		this.B_Btn_Next.TabIndex = 1;
		this.B_Btn_Next.Text = "下一步";
		this.B_Btn_Next.Click += new System.EventHandler(B_Btn_Next_Click);
		this.panel5.BackColor = System.Drawing.Color.White;
		this.panel5.Controls.Add(this.ultraLabel7);
		this.panel5.Controls.Add(this.ultraLabel6);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel5.Location = new System.Drawing.Point(0, 0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(516, 60);
		this.panel5.TabIndex = 12;
		appearance6.BackColor = System.Drawing.Color.White;
		this.ultraLabel7.Appearance = appearance6;
		this.ultraLabel7.Location = new System.Drawing.Point(47, 34);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel7.TabIndex = 3;
		this.ultraLabel7.Text = "請挑選匯出存放的目錄及檔案名稱";
		appearance7.BackColor = System.Drawing.Color.White;
		this.ultraLabel6.Appearance = appearance7;
		this.ultraLabel6.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel6.Location = new System.Drawing.Point(16, 12);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel6.TabIndex = 2;
		this.ultraLabel6.Text = "資料匯出路徑及檔案名稱";
		this.Tab_C.Controls.Add(this.lblWait);
		this.Tab_C.Controls.Add(this.panel7);
		this.Tab_C.Controls.Add(this.panel4);
		this.Tab_C.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_C.Name = "Tab_C";
		this.Tab_C.Size = new System.Drawing.Size(516, 369);
		this.lblWait.Location = new System.Drawing.Point(16, 84);
		this.lblWait.Name = "lblWait";
		this.lblWait.Size = new System.Drawing.Size(476, 20);
		this.lblWait.TabIndex = 17;
		this.lblWait.Text = "正在準備匯出的資料，這個動作會花些時間，請稍候。";
		this.panel7.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel7.Controls.Add(this.groupBox4);
		this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel7.Location = new System.Drawing.Point(0, 325);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(516, 44);
		this.panel7.TabIndex = 15;
		this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox4.Location = new System.Drawing.Point(0, 0);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(516, 8);
		this.groupBox4.TabIndex = 3;
		this.groupBox4.TabStop = false;
		this.panel4.BackColor = System.Drawing.Color.White;
		this.panel4.Controls.Add(this.ultraLabel9);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel4.Location = new System.Drawing.Point(0, 0);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(516, 60);
		this.panel4.TabIndex = 13;
		appearance8.BackColor = System.Drawing.Color.White;
		this.ultraLabel9.Appearance = appearance8;
		this.ultraLabel9.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel9.Location = new System.Drawing.Point(16, 12);
		this.ultraLabel9.Name = "ultraLabel9";
		this.ultraLabel9.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel9.TabIndex = 2;
		this.ultraLabel9.Text = "資料匯出中...";
		this.Tab_D.Controls.Add(this.lblEXCEL);
		this.Tab_D.Controls.Add(this.ultraLabel14);
		this.Tab_D.Controls.Add(this.ultraLabel13);
		this.Tab_D.Controls.Add(this.ultraLabel12);
		this.Tab_D.Controls.Add(this.panel6);
		this.Tab_D.Location = new System.Drawing.Point(-10000, -10000);
		this.Tab_D.Name = "Tab_D";
		this.Tab_D.Size = new System.Drawing.Size(516, 369);
		appearance9.ForeColor = System.Drawing.Color.Red;
		this.lblEXCEL.Appearance = appearance9;
		this.lblEXCEL.Location = new System.Drawing.Point(40, 163);
		this.lblEXCEL.Name = "lblEXCEL";
		this.lblEXCEL.Size = new System.Drawing.Size(456, 43);
		this.lblEXCEL.TabIndex = 22;
		this.lblEXCEL.Visible = false;
		appearance10.BackColor = System.Drawing.Color.White;
		this.ultraLabel14.Appearance = appearance10;
		this.ultraLabel14.Location = new System.Drawing.Point(36, 116);
		this.ultraLabel14.Name = "ultraLabel14";
		this.ultraLabel14.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel14.TabIndex = 13;
		this.ultraLabel14.Text = "若要結束精靈，請按一下[完成]。";
		appearance11.BackColor = System.Drawing.Color.White;
		this.ultraLabel13.Appearance = appearance11;
		this.ultraLabel13.Location = new System.Drawing.Point(36, 64);
		this.ultraLabel13.Name = "ultraLabel13";
		this.ultraLabel13.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel13.TabIndex = 12;
		this.ultraLabel13.Text = "你已經成功匯出資料。";
		appearance12.BackColor = System.Drawing.Color.White;
		this.ultraLabel12.Appearance = appearance12;
		this.ultraLabel12.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel12.Location = new System.Drawing.Point(20, 20);
		this.ultraLabel12.Name = "ultraLabel12";
		this.ultraLabel12.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel12.TabIndex = 11;
		this.ultraLabel12.Text = "恭禧您!";
		this.panel6.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel6.Controls.Add(this.groupBox3);
		this.panel6.Controls.Add(this.D_Btn_Fnsh);
		this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel6.Location = new System.Drawing.Point(0, 325);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(516, 44);
		this.panel6.TabIndex = 10;
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox3.Location = new System.Drawing.Point(0, 0);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(516, 8);
		this.groupBox3.TabIndex = 3;
		this.groupBox3.TabStop = false;
		appearance13.Image = resources.GetObject("appearance13.Image");
		appearance13.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Fnsh.Appearance = appearance13;
		this.D_Btn_Fnsh.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Fnsh.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.D_Btn_Fnsh.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.D_Btn_Fnsh.Font = new System.Drawing.Font("細明體", 11f);
		this.D_Btn_Fnsh.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Fnsh.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Fnsh.Location = new System.Drawing.Point(324, 9);
		this.D_Btn_Fnsh.Name = "D_Btn_Fnsh";
		this.D_Btn_Fnsh.ShowFocusRect = false;
		this.D_Btn_Fnsh.ShowOutline = false;
		this.D_Btn_Fnsh.Size = new System.Drawing.Size(88, 31);
		this.D_Btn_Fnsh.SupportThemes = false;
		this.D_Btn_Fnsh.TabIndex = 1;
		this.D_Btn_Fnsh.Text = "完成";
		this.TabCtrl.BackColor = System.Drawing.Color.White;
		this.TabCtrl.Controls.Add(this.ultraTabSharedControlsPage1);
		this.TabCtrl.Controls.Add(this.Tab_B);
		this.TabCtrl.Controls.Add(this.Tab_C);
		this.TabCtrl.Controls.Add(this.Tab_D);
		this.TabCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.TabCtrl.Location = new System.Drawing.Point(0, 0);
		this.TabCtrl.Name = "TabCtrl";
		this.TabCtrl.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.TabCtrl.Size = new System.Drawing.Size(516, 369);
		this.TabCtrl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
		this.TabCtrl.TabIndex = 1;
		ultraTab1.TabPage = this.Tab_B;
		ultraTab1.Text = "tab2";
		ultraTab2.TabPage = this.Tab_C;
		ultraTab2.Text = "tab3";
		ultraTab3.TabPage = this.Tab_D;
		ultraTab3.Text = "tab4";
		this.TabCtrl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[3] { ultraTab1, ultraTab2, ultraTab3 });
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(516, 369);
		this.saveFileDialog1.RestoreDirectory = true;
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		base.CancelButton = this.B_Btn_Cncl;
		base.ClientSize = new System.Drawing.Size(516, 369);
		base.Controls.Add(this.TabCtrl);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormInvoiceExport";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "計價資料匯出";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormInvoiceExport_KeyDown);
		base.Load += new System.EventHandler(FormInvoiceExport_Load);
		this.Tab_B.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtExpDirFile).EndInit();
		this.panel2.ResumeLayout(false);
		this.panel5.ResumeLayout(false);
		this.Tab_C.ResumeLayout(false);
		this.panel7.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
		this.Tab_D.ResumeLayout(false);
		this.panel6.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.TabCtrl).EndInit();
		this.TabCtrl.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	private void FormInvoiceExport_Load(object sender, EventArgs e)
	{
		ls_sProj = F_SubProjectCode;
		ls_ProjectCode = F_ProjectCode;
		lbl_Issue.Text = "【" + F_Issue + "】";
	}

	private void ultraButton4_Click(object sender, EventArgs e)
	{
		string sFilter = "XML files (*.xml)|*.xml";
		saveFileDialog1.Filter = sFilter;
		saveFileDialog1.RestoreDirectory = true;
		if (saveFileDialog1.ShowDialog() == DialogResult.OK)
		{
			txtExpDirFile.Text = saveFileDialog1.FileName;
		}
	}

	private void B_Btn_Next_Click(object sender, EventArgs e)
	{
		if (txtExpDirFile.Text.Trim() == "")
		{
			MessageBox.Show(this, "請先選定存放的目錄及檔名!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtExpDirFile.Focus();
			return;
		}
		if (CommonMethods.ExtractExtFileName(txtExpDirFile.Text.Trim()).ToUpper() != "XML")
		{
			MessageBox.Show(this, "你選定的檔案，副檔名不是XML，請重設!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtExpDirFile.Focus();
			return;
		}
		if (CommonMethods.ExtractFileNoExtName(txtExpDirFile.Text.Trim()) == "")
		{
			MessageBox.Show(this, "你選定的檔案，沒有檔名，請重設!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtExpDirFile.Focus();
			return;
		}
		if (txtExpDirFile.Text.IndexOf("/") >= 0)
		{
			MessageBox.Show(this, "你選定的檔案，不可以包含特殊字元 '/'，請重設!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtExpDirFile.Focus();
			return;
		}
		Cursor = Cursors.WaitCursor;
		Tab_C.Tab.Selected = true;
		Application.DoEvents();
		ExecuteExport();
		Tab_D.Tab.Selected = true;
		Cursor = Cursors.Default;
	}

	private void ExecuteExport()
	{
		ArrayList tmp_AL1 = new ArrayList();
		tmp_AL1 = new ArrayList();
		tmp_AL1.Add(F_UserID);
		tmp_AL1.Add("(subacc) 估驗計價--匯出計價資料");
		sub_acc AccCom = new sub_acc(tmp_AL1);
		DataTable OutAccDT = AccCom.ListItem("", ls_sProj, ls_ProjectCode);
		AccCom = null;
		for (int i = OutAccDT.Rows.Count - 2; i > -1; i--)
		{
			if (F_Issue != OutAccDT.Rows[i]["queue"].ToString())
			{
				OutAccDT.Rows.Remove(OutAccDT.Rows[i]);
			}
		}
		OutAccDT.AcceptChanges();
		string ls_Queue = OutAccDT.Rows[0]["queue"].ToString();
		submfq MfqCom = new submfq(tmp_AL1);
		DataTable OutMfqDT = MfqCom.ListItem("", ls_Queue, ls_sProj, ls_ProjectCode);
		MfqCom = null;
		DataSet RtnVal = new DataSet();
		OutAccDT.TableName = "SubAcc";
		OutMfqDT.TableName = "SubMfq";
		RtnVal.Tables.Add(OutAccDT.Copy());
		RtnVal.Tables.Add(OutMfqDT.Copy());
		lblEXCEL.Text = txtExpDirFile.Text.Trim();
		RtnVal.WriteXml(txtExpDirFile.Text.Trim());
	}

	private void FormInvoiceExport_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormInvoiceExport");
		}
	}
}
