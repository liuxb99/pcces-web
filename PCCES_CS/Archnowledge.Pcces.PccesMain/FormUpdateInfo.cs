using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace Archnowledge.Pcces.PccesMain;

public class FormUpdateInfo : Form
{
	private Panel panel9;

	private UltraButton btnClose;

	private UltraLabel lbTitle;

	private UltraButton btnUpdate;

	private UltraLabel lbInstruction;

	private Container components = null;

	private Panel panelMessage;

	private Label lbMessage;

	private WebBrowser browserUpdateDetail;

	private string updateLogFilePath = AppDomain.CurrentDomain.BaseDirectory + "News.html";

	private string updateLogWebAddress = "http://pcces3.archnowledge.com/csi/pccesNews.html";

	private void InitializeComponent()
	{
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.FormUpdateInfo));
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		this.panel9 = new System.Windows.Forms.Panel();
		this.lbInstruction = new Infragistics.Win.Misc.UltraLabel();
		this.btnUpdate = new Infragistics.Win.Misc.UltraButton();
		this.btnClose = new Infragistics.Win.Misc.UltraButton();
		this.lbTitle = new Infragistics.Win.Misc.UltraLabel();
		this.panelMessage = new System.Windows.Forms.Panel();
		this.lbMessage = new System.Windows.Forms.Label();
		this.browserUpdateDetail = new System.Windows.Forms.WebBrowser();
		this.panel9.SuspendLayout();
		this.panelMessage.SuspendLayout();
		base.SuspendLayout();
		this.panel9.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel9.Controls.Add(this.lbInstruction);
		this.panel9.Controls.Add(this.btnUpdate);
		this.panel9.Controls.Add(this.btnClose);
		this.panel9.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel9.Location = new System.Drawing.Point(0, 414);
		this.panel9.Name = "panel9";
		this.panel9.Size = new System.Drawing.Size(688, 48);
		this.panel9.TabIndex = 21;
		appearance1.ForeColor = System.Drawing.Color.Red;
		this.lbInstruction.Appearance = appearance1;
		this.lbInstruction.Font = new System.Drawing.Font("新細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lbInstruction.Location = new System.Drawing.Point(12, 16);
		this.lbInstruction.Name = "lbInstruction";
		this.lbInstruction.Size = new System.Drawing.Size(259, 16);
		this.lbInstruction.TabIndex = 23;
		this.lbInstruction.Text = "若要再檢視此訊息，請從[說明]-->[最新消息]。";
		this.btnUpdate.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnUpdate.Appearance = appearance2;
		this.btnUpdate.BackColor = System.Drawing.SystemColors.Control;
		this.btnUpdate.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnUpdate.Font = new System.Drawing.Font("細明體", 11f);
		this.btnUpdate.ImageSize = new System.Drawing.Size(20, 20);
		this.btnUpdate.ImageTransparentColor = System.Drawing.Color.White;
		this.btnUpdate.Location = new System.Drawing.Point(494, 9);
		this.btnUpdate.Name = "btnUpdate";
		this.btnUpdate.ShowFocusRect = false;
		this.btnUpdate.ShowOutline = false;
		this.btnUpdate.Size = new System.Drawing.Size(88, 31);
		this.btnUpdate.SupportThemes = false;
		this.btnUpdate.TabIndex = 4;
		this.btnUpdate.Text = "更新";
		this.btnUpdate.Click += new System.EventHandler(btnUpdate_Click);
		this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance3.Image = resources.GetObject("appearance3.Image");
		appearance3.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnClose.Appearance = appearance3;
		this.btnClose.BackColor = System.Drawing.SystemColors.Control;
		this.btnClose.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnClose.Font = new System.Drawing.Font("細明體", 11f);
		this.btnClose.ImageSize = new System.Drawing.Size(20, 20);
		this.btnClose.ImageTransparentColor = System.Drawing.Color.White;
		this.btnClose.Location = new System.Drawing.Point(588, 9);
		this.btnClose.Name = "btnClose";
		this.btnClose.ShowFocusRect = false;
		this.btnClose.ShowOutline = false;
		this.btnClose.Size = new System.Drawing.Size(88, 31);
		this.btnClose.SupportThemes = false;
		this.btnClose.TabIndex = 2;
		this.btnClose.Text = "關閉";
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		this.lbTitle.Location = new System.Drawing.Point(12, 12);
		this.lbTitle.Name = "lbTitle";
		this.lbTitle.Size = new System.Drawing.Size(78, 17);
		this.lbTitle.TabIndex = 22;
		this.lbTitle.Text = "訊息列表";
		this.panelMessage.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panelMessage.Controls.Add(this.lbMessage);
		this.panelMessage.Location = new System.Drawing.Point(244, 181);
		this.panelMessage.Name = "panelMessage";
		this.panelMessage.Size = new System.Drawing.Size(200, 100);
		this.panelMessage.TabIndex = 28;
		this.lbMessage.Location = new System.Drawing.Point(16, 39);
		this.lbMessage.Name = "lbMessage";
		this.lbMessage.Size = new System.Drawing.Size(168, 23);
		this.lbMessage.TabIndex = 1;
		this.lbMessage.Text = "讀取中…";
		this.lbMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.browserUpdateDetail.Location = new System.Drawing.Point(12, 35);
		this.browserUpdateDetail.MinimumSize = new System.Drawing.Size(20, 20);
		this.browserUpdateDetail.Name = "browserUpdateDetail";
		this.browserUpdateDetail.Size = new System.Drawing.Size(664, 364);
		this.browserUpdateDetail.TabIndex = 29;
		this.browserUpdateDetail.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(browserUpdateDetail_DocumentCompleted);
		this.AutoScaleBaseSize = new System.Drawing.Size(6, 18);
		base.ClientSize = new System.Drawing.Size(688, 462);
		base.Controls.Add(this.panelMessage);
		base.Controls.Add(this.browserUpdateDetail);
		base.Controls.Add(this.lbTitle);
		base.Controls.Add(this.panel9);
		this.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.Name = "FormUpdateInfo";
		base.ShowIcon = false;
		this.Text = "最新消息";
		base.Load += new System.EventHandler(FormUpdateInfo_Load);
		this.panel9.ResumeLayout(false);
		this.panelMessage.ResumeLayout(false);
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

	public FormUpdateInfo()
	{
		InitializeComponent();
	}

	private void FormUpdateInfo_Load(object sender, EventArgs e)
	{
		Cursor = Cursors.WaitCursor;
		lbMessage.Text = "讀取中…";
		panelMessage.Visible = true;
		browserUpdateDetail.Navigate(updateLogFilePath);
	}

	private void btnUpdate_Click(object sender, EventArgs e)
	{
		Cursor = Cursors.WaitCursor;
		lbMessage.Text = "更新中…";
		panelMessage.Visible = true;
		browserUpdateDetail.Navigate(updateLogWebAddress);
	}

	private void browserUpdateDetail_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
	{
		panelMessage.Visible = false;
		StreamReader reader = new StreamReader(browserUpdateDetail.DocumentStream, Encoding.GetEncoding("big5"));
		string html = reader.ReadToEnd();
		File.WriteAllText(updateLogFilePath, html, Encoding.GetEncoding("big5"));
		Cursor = Cursors.Default;
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}
}
