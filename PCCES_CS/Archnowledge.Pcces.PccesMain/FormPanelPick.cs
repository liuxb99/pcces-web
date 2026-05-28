using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace Archnowledge.Pcces.PccesMain;

public class FormPanelPick : Form
{
	private const string CallFormHelp = "FormPanelPick";

	private Panel panel1;

	private UltraLabel ultraLabel1;

	private Panel panel2;

	private Panel panel3;

	private RadioButton RB1;

	private RadioButton RB2;

	private RadioButton RB3;

	private UltraButton Btn_Cncl;

	private UltraButton Btn_OK;

	private Container components = null;

	private UltraPictureBox Picture1;

	private UltraPictureBox Picture2;

	private UltraPictureBox Picture3;

	private string F_OriginalHomeID = "2";

	public string _OriginalHomeID
	{
		get
		{
			return F_OriginalHomeID;
		}
		set
		{
			F_OriginalHomeID = value;
		}
	}

	public FormPanelPick()
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
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.FormPanelPick));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		this.panel1 = new System.Windows.Forms.Panel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.RB3 = new System.Windows.Forms.RadioButton();
		this.RB2 = new System.Windows.Forms.RadioButton();
		this.RB1 = new System.Windows.Forms.RadioButton();
		this.Picture3 = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
		this.Picture2 = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
		this.Picture1 = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
		this.panel3 = new System.Windows.Forms.Panel();
		this.Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.Btn_OK = new Infragistics.Win.Misc.UltraButton();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		this.panel3.SuspendLayout();
		base.SuspendLayout();
		this.panel1.BackColor = System.Drawing.Color.White;
		this.panel1.Controls.Add(this.ultraLabel1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(476, 40);
		this.panel1.TabIndex = 0;
		this.ultraLabel1.Location = new System.Drawing.Point(12, 12);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(288, 23);
		this.ultraLabel1.TabIndex = 0;
		this.ultraLabel1.Text = "請挑選你喜愛的首頁面板";
		this.panel2.AutoScroll = true;
		this.panel2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel2.Controls.Add(this.RB3);
		this.panel2.Controls.Add(this.RB2);
		this.panel2.Controls.Add(this.RB1);
		this.panel2.Controls.Add(this.Picture3);
		this.panel2.Controls.Add(this.Picture2);
		this.panel2.Controls.Add(this.Picture1);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Location = new System.Drawing.Point(0, 40);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(476, 407);
		this.panel2.TabIndex = 2;
		this.panel2.RightToLeftChanged += new System.EventHandler(panel2_RightToLeftChanged);
		this.RB3.Location = new System.Drawing.Point(220, 276);
		this.RB3.Name = "RB3";
		this.RB3.Size = new System.Drawing.Size(212, 24);
		this.RB3.TabIndex = 6;
		this.RB3.Text = "3.5b 版舊式首頁";
		this.RB2.Checked = true;
		this.RB2.Location = new System.Drawing.Point(220, 12);
		this.RB2.Name = "RB2";
		this.RB2.Size = new System.Drawing.Size(212, 24);
		this.RB2.TabIndex = 5;
		this.RB2.TabStop = true;
		this.RB2.Text = "PCCES Win 4.3  新版首頁";
		this.RB1.Location = new System.Drawing.Point(220, 144);
		this.RB1.Name = "RB1";
		this.RB1.Size = new System.Drawing.Size(248, 24);
		this.RB1.TabIndex = 3;
		this.RB1.Text = "一般套裝軟體所使用之快速捷徑";
		appearance1.Cursor = System.Windows.Forms.Cursors.Hand;
		this.Picture3.Appearance = appearance1;
		this.Picture3.BorderShadowColor = System.Drawing.Color.Empty;
		this.Picture3.BorderStyle = Infragistics.Win.UIElementBorderStyle.Raised;
		this.Picture3.Image = resources.GetObject("Picture3.Image");
		this.Picture3.Location = new System.Drawing.Point(12, 276);
		this.Picture3.MaintainAspectRatio = false;
		this.Picture3.Name = "Picture3";
		this.Picture3.Size = new System.Drawing.Size(192, 121);
		this.Picture3.TabIndex = 2;
		this.Picture3.Click += new System.EventHandler(Picture3_Click);
		appearance2.Cursor = System.Windows.Forms.Cursors.Hand;
		this.Picture2.Appearance = appearance2;
		this.Picture2.BorderShadowColor = System.Drawing.Color.Empty;
		this.Picture2.BorderStyle = Infragistics.Win.UIElementBorderStyle.Raised;
		this.Picture2.Image = resources.GetObject("Picture2.Image");
		this.Picture2.Location = new System.Drawing.Point(12, 12);
		this.Picture2.MaintainAspectRatio = false;
		this.Picture2.Name = "Picture2";
		this.Picture2.Size = new System.Drawing.Size(192, 121);
		this.Picture2.TabIndex = 1;
		this.Picture2.Click += new System.EventHandler(Picture2_Click);
		appearance3.Cursor = System.Windows.Forms.Cursors.Hand;
		this.Picture1.Appearance = appearance3;
		this.Picture1.BorderShadowColor = System.Drawing.Color.Empty;
		this.Picture1.BorderStyle = Infragistics.Win.UIElementBorderStyle.Raised;
		this.Picture1.Image = resources.GetObject("Picture1.Image");
		this.Picture1.Location = new System.Drawing.Point(12, 144);
		this.Picture1.MaintainAspectRatio = false;
		this.Picture1.Name = "Picture1";
		this.Picture1.Size = new System.Drawing.Size(192, 121);
		this.Picture1.TabIndex = 0;
		this.Picture1.Click += new System.EventHandler(Picture1_Click);
		this.panel3.Controls.Add(this.Btn_Cncl);
		this.panel3.Controls.Add(this.Btn_OK);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel3.Location = new System.Drawing.Point(0, 447);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(476, 40);
		this.panel3.TabIndex = 3;
		this.Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance4.Image = resources.GetObject("appearance4.Image");
		appearance4.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.Btn_Cncl.Appearance = appearance4;
		this.Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.Btn_Cncl.Location = new System.Drawing.Point(384, 4);
		this.Btn_Cncl.Name = "Btn_Cncl";
		this.Btn_Cncl.ShowFocusRect = false;
		this.Btn_Cncl.ShowOutline = false;
		this.Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.Btn_Cncl.SupportThemes = false;
		this.Btn_Cncl.TabIndex = 4;
		this.Btn_Cncl.Text = "取消";
		this.Btn_OK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance5.Image = resources.GetObject("appearance5.Image");
		appearance5.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.Btn_OK.Appearance = appearance5;
		this.Btn_OK.BackColor = System.Drawing.SystemColors.Control;
		this.Btn_OK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.Btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.Btn_OK.Font = new System.Drawing.Font("細明體", 11f);
		this.Btn_OK.ImageSize = new System.Drawing.Size(20, 20);
		this.Btn_OK.ImageTransparentColor = System.Drawing.Color.White;
		this.Btn_OK.Location = new System.Drawing.Point(292, 4);
		this.Btn_OK.Name = "Btn_OK";
		this.Btn_OK.ShowFocusRect = false;
		this.Btn_OK.ShowOutline = false;
		this.Btn_OK.Size = new System.Drawing.Size(88, 31);
		this.Btn_OK.SupportThemes = false;
		this.Btn_OK.TabIndex = 3;
		this.Btn_OK.Text = "確定";
		this.Btn_OK.Click += new System.EventHandler(Btn_OK_Click);
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		base.ClientSize = new System.Drawing.Size(476, 487);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel3);
		base.Controls.Add(this.panel1);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormPanelPick";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "首頁面板挑選";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormPanelPick_KeyDown);
		base.Load += new System.EventHandler(FormPanelPick_Load);
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	private void Btn_OK_Click(object sender, EventArgs e)
	{
		string sIniFileName = CommonMethods.ExtractFilePath(Application.ExecutablePath) + "PccesMain.ini";
		string HomeID = "";
		if (RB1.Checked)
		{
			HomeID = "1";
		}
		if (RB2.Checked)
		{
			HomeID = "2";
		}
		if (RB3.Checked)
		{
			HomeID = "3";
		}
		CommonMethods.IniWriteValue(sIniFileName, "HomePanel", "Home", HomeID);
	}

	private void FormPanelPick_Load(object sender, EventArgs e)
	{
		switch (F_OriginalHomeID.Trim())
		{
		case "1":
			RB1.Checked = true;
			break;
		case "2":
			RB2.Checked = true;
			break;
		case "3":
			panel2.ScrollControlIntoView(Picture3);
			RB3.Checked = true;
			break;
		default:
			RB2.Checked = true;
			break;
		}
	}

	private void panel2_RightToLeftChanged(object sender, EventArgs e)
	{
	}

	private void Picture1_Click(object sender, EventArgs e)
	{
		RB1.Checked = true;
	}

	private void Picture2_Click(object sender, EventArgs e)
	{
		RB2.Checked = true;
	}

	private void Picture3_Click(object sender, EventArgs e)
	{
		RB3.Checked = true;
	}

	private void FormPanelPick_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormPanelPick");
		}
	}
}
