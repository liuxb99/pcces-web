using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using AxAcroPDFLib;

namespace Archnowledge.Pcces.PccesMain;

public class PDFForm : Form
{
	private string F_FileName = string.Empty;

	private string F_BookMark = string.Empty;

	private Button CloseButton;

	private Panel panel1;

	private Panel panel2;

	private Container components = null;

	public string _FileName
	{
		get
		{
			return F_FileName;
		}
		set
		{
			F_FileName = value;
		}
	}

	public string _BookMark
	{
		get
		{
			return F_BookMark;
		}
		set
		{
			F_BookMark = value;
		}
	}

	public PDFForm()
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
		this.CloseButton = new System.Windows.Forms.Button();
		this.panel1 = new System.Windows.Forms.Panel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.panel2.SuspendLayout();
		base.SuspendLayout();
		this.CloseButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.CloseButton.Location = new System.Drawing.Point(824, 16);
		this.CloseButton.Name = "CloseButton";
		this.CloseButton.TabIndex = 1;
		this.CloseButton.Text = "關閉";
		this.CloseButton.Click += new System.EventHandler(CloseButton_Click);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(912, 446);
		this.panel1.TabIndex = 2;
		this.panel2.Controls.Add(this.CloseButton);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel2.Location = new System.Drawing.Point(0, 446);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(912, 56);
		this.panel2.TabIndex = 3;
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		base.ClientSize = new System.Drawing.Size(912, 502);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.panel2);
		base.Name = "PDFForm";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "說明文件";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(PDFForm_Load);
		this.panel2.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	private void PDFForm_Load(object sender, EventArgs e)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		FileInfo Info = new FileInfo(F_FileName);
		if (!Info.Exists)
		{
			Close();
			return;
		}
		try
		{
			AxAcroPDF axAcroPDF1 = new AxAcroPDF();
			((Control)(object)axAcroPDF1).Dock = DockStyle.Fill;
			((AxHost)(object)axAcroPDF1).Enabled = true;
			((Control)(object)axAcroPDF1).Location = new Point(8, 8);
			((Control)(object)axAcroPDF1).Name = "axAcroPDF1";
			ResourceManager resources = new ResourceManager(typeof(PDFForm));
			((AxHost)(object)axAcroPDF1).OcxState = (AxHost.State)resources.GetObject("axAcroPDF1.OcxState");
			((Control)(object)axAcroPDF1).TabIndex = 0;
			panel1.Controls.Add((Control)(object)axAcroPDF1);
			axAcroPDF1.LoadFile(F_FileName);
			axAcroPDF1.setPageMode("bookmarks");
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "PDFForm.cs" + ex.Message);
			PDFErrorForm PDFForm2 = new PDFErrorForm();
			Close();
			PDFForm2.Show();
		}
	}

	private void CloseButton_Click(object sender, EventArgs e)
	{
		Close();
	}
}
