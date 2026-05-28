using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.STDClass;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormBudgetResDial1 : Form
{
	private const string CallFormHelp = "FormBudgetResDial1";

	private Panel panel8;

	private GroupBox groupBox4;

	private UltraButton D_Btn_Fnsh;

	private Panel panel5;

	private UltraLabel ultraLabel11;

	private Panel panel1;

	private GroupBox groupBox2;

	private RadioButton RB1;

	private RadioButton RB2;

	private RadioButton RB3;

	private UltraNumericEditor T1;

	private UltraNumericEditor T2;

	private Label label1;

	private UltraButton Btn_CNL;

	private UltraNumericEditor T3;

	private UltraNumericEditor T4;

	private Container components = null;

	public FormBudgetResDial1()
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
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormBudgetResDial1));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		this.panel8 = new System.Windows.Forms.Panel();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.D_Btn_Fnsh = new Infragistics.Win.Misc.UltraButton();
		this.Btn_CNL = new Infragistics.Win.Misc.UltraButton();
		this.panel5 = new System.Windows.Forms.Panel();
		this.ultraLabel11 = new Infragistics.Win.Misc.UltraLabel();
		this.panel1 = new System.Windows.Forms.Panel();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.label1 = new System.Windows.Forms.Label();
		this.T4 = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
		this.T3 = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
		this.T2 = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
		this.T1 = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
		this.RB3 = new System.Windows.Forms.RadioButton();
		this.RB2 = new System.Windows.Forms.RadioButton();
		this.RB1 = new System.Windows.Forms.RadioButton();
		this.panel8.SuspendLayout();
		this.panel5.SuspendLayout();
		this.panel1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.T4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.T3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.T2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.T1).BeginInit();
		base.SuspendLayout();
		this.panel8.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel8.Controls.Add(this.groupBox4);
		this.panel8.Controls.Add(this.D_Btn_Fnsh);
		this.panel8.Controls.Add(this.Btn_CNL);
		this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel8.Location = new System.Drawing.Point(0, 194);
		this.panel8.Name = "panel8";
		this.panel8.Size = new System.Drawing.Size(368, 44);
		this.panel8.TabIndex = 18;
		this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox4.Location = new System.Drawing.Point(0, 0);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(368, 8);
		this.groupBox4.TabIndex = 3;
		this.groupBox4.TabStop = false;
		this.D_Btn_Fnsh.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Fnsh.Appearance = appearance1;
		this.D_Btn_Fnsh.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Fnsh.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.D_Btn_Fnsh.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.D_Btn_Fnsh.Font = new System.Drawing.Font("細明體", 11f);
		this.D_Btn_Fnsh.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Fnsh.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Fnsh.Location = new System.Drawing.Point(184, 9);
		this.D_Btn_Fnsh.Name = "D_Btn_Fnsh";
		this.D_Btn_Fnsh.ShowFocusRect = false;
		this.D_Btn_Fnsh.ShowOutline = false;
		this.D_Btn_Fnsh.Size = new System.Drawing.Size(88, 31);
		this.D_Btn_Fnsh.SupportThemes = false;
		this.D_Btn_Fnsh.TabIndex = 1;
		this.D_Btn_Fnsh.Text = "確定";
		this.D_Btn_Fnsh.Click += new System.EventHandler(D_Btn_Fnsh_Click);
		this.Btn_CNL.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.Btn_CNL.Appearance = appearance2;
		this.Btn_CNL.BackColor = System.Drawing.SystemColors.Control;
		this.Btn_CNL.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.Btn_CNL.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.Btn_CNL.Font = new System.Drawing.Font("細明體", 11f);
		this.Btn_CNL.ImageSize = new System.Drawing.Size(20, 20);
		this.Btn_CNL.ImageTransparentColor = System.Drawing.Color.White;
		this.Btn_CNL.Location = new System.Drawing.Point(277, 9);
		this.Btn_CNL.Name = "Btn_CNL";
		this.Btn_CNL.ShowFocusRect = false;
		this.Btn_CNL.ShowOutline = false;
		this.Btn_CNL.Size = new System.Drawing.Size(88, 31);
		this.Btn_CNL.SupportThemes = false;
		this.Btn_CNL.TabIndex = 0;
		this.Btn_CNL.Text = "取消";
		this.panel5.BackColor = System.Drawing.Color.White;
		this.panel5.Controls.Add(this.ultraLabel11);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel5.Location = new System.Drawing.Point(0, 0);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(368, 32);
		this.panel5.TabIndex = 19;
		appearance3.BackColor = System.Drawing.Color.White;
		this.ultraLabel11.Appearance = appearance3;
		this.ultraLabel11.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel11.Location = new System.Drawing.Point(8, 8);
		this.ultraLabel11.Name = "ultraLabel11";
		this.ultraLabel11.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel11.TabIndex = 3;
		this.ultraLabel11.Text = "請輸入篩選條件";
		this.panel1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel1.Controls.Add(this.groupBox2);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.panel1.Location = new System.Drawing.Point(0, 32);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(368, 162);
		this.panel1.TabIndex = 20;
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Controls.Add(this.T4);
		this.groupBox2.Controls.Add(this.T3);
		this.groupBox2.Controls.Add(this.T2);
		this.groupBox2.Controls.Add(this.T1);
		this.groupBox2.Controls.Add(this.RB3);
		this.groupBox2.Controls.Add(this.RB2);
		this.groupBox2.Controls.Add(this.RB1);
		this.groupBox2.Location = new System.Drawing.Point(8, 8);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(352, 148);
		this.groupBox2.TabIndex = 1;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "查詢條件";
		this.label1.Location = new System.Drawing.Point(179, 114);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(16, 23);
		this.label1.TabIndex = 7;
		this.label1.Text = "～";
		this.T4.FormatString = "N2";
		this.T4.Location = new System.Drawing.Point(200, 109);
		this.T4.MaxValue = 100;
		this.T4.MinValue = 0;
		this.T4.Name = "T4";
		this.T4.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
		this.T4.PromptChar = ' ';
		this.T4.Size = new System.Drawing.Size(139, 24);
		this.T4.TabIndex = 6;
		this.T4.Value = 20;
		this.T4.Enter += new System.EventHandler(T4_Enter);
		this.T3.FormatString = "N2";
		this.T3.Location = new System.Drawing.Point(37, 109);
		this.T3.MaxValue = 100;
		this.T3.MinValue = 0;
		this.T3.Name = "T3";
		this.T3.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
		this.T3.PromptChar = ' ';
		this.T3.Size = new System.Drawing.Size(139, 24);
		this.T3.TabIndex = 5;
		this.T3.Value = 10;
		this.T3.ValueChanged += new System.EventHandler(ultraNumericEditor1_ValueChanged);
		this.T3.Enter += new System.EventHandler(T3_Enter);
		this.T2.FormatString = "N2";
		this.T2.Location = new System.Drawing.Point(76, 67);
		this.T2.MaxValue = 100;
		this.T2.MinValue = 0;
		this.T2.Name = "T2";
		this.T2.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
		this.T2.PromptChar = ' ';
		this.T2.Size = new System.Drawing.Size(100, 24);
		this.T2.TabIndex = 4;
		this.T2.Value = 10;
		this.T2.Enter += new System.EventHandler(T2_Enter);
		this.T1.FormatString = "N2";
		this.T1.Location = new System.Drawing.Point(76, 29);
		this.T1.MaxValue = 100;
		this.T1.MinValue = 0;
		this.T1.Name = "T1";
		this.T1.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
		this.T1.PromptChar = ' ';
		this.T1.Size = new System.Drawing.Size(100, 24);
		this.T1.TabIndex = 3;
		this.T1.Value = 10;
		this.T1.Enter += new System.EventHandler(T1_Enter);
		this.RB3.Location = new System.Drawing.Point(16, 110);
		this.RB3.Name = "RB3";
		this.RB3.Size = new System.Drawing.Size(16, 24);
		this.RB3.TabIndex = 2;
		this.RB2.Location = new System.Drawing.Point(16, 69);
		this.RB2.Name = "RB2";
		this.RB2.Size = new System.Drawing.Size(56, 24);
		this.RB2.TabIndex = 1;
		this.RB2.Text = "小於";
		this.RB1.Checked = true;
		this.RB1.Location = new System.Drawing.Point(16, 29);
		this.RB1.Name = "RB1";
		this.RB1.Size = new System.Drawing.Size(56, 24);
		this.RB1.TabIndex = 0;
		this.RB1.TabStop = true;
		this.RB1.Text = "大於";
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		base.CancelButton = this.Btn_CNL;
		base.ClientSize = new System.Drawing.Size(368, 238);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.panel5);
		base.Controls.Add(this.panel8);
		base.KeyPreview = true;
		base.Name = "FormBudgetResDial1";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "金額權重過濾條件";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormBudgetResDial1_KeyDown);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormBudgetResDial1_FormClosing);
		base.Load += new System.EventHandler(FormBudgetResDial1_Load);
		this.panel8.ResumeLayout(false);
		this.panel5.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.T4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.T3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.T2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.T1).EndInit();
		base.ResumeLayout(false);
	}

	private void ultraNumericEditor1_ValueChanged(object sender, EventArgs e)
	{
	}

	private void D_Btn_Fnsh_Click(object sender, EventArgs e)
	{
		if (RB1.Checked)
		{
			try
			{
				Convert.ToDouble(T1.Value);
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudgetResDial1.cs" + ex.Message);
				MessageBox.Show(this, "請輸入大於的金額權重。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				T1.Focus();
				return;
			}
			(base.Owner as FormBudgetRes)._MnyRateType = 1;
			(base.Owner as FormBudgetRes)._Rate1 = Convert.ToDecimal(T1.Value);
		}
		else if (RB2.Checked)
		{
			try
			{
				Convert.ToDouble(T2.Value);
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudgetResDial1.cs" + ex.Message);
				MessageBox.Show(this, "請輸入小於的金額權重。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				T2.Focus();
				return;
			}
			(base.Owner as FormBudgetRes)._MnyRateType = 2;
			(base.Owner as FormBudgetRes)._Rate1 = Convert.ToDecimal(T2.Value);
		}
		else if (RB3.Checked)
		{
			try
			{
				Convert.ToDouble(T3.Value);
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudgetResDial1.cs" + ex.Message);
				MessageBox.Show(this, "請輸入金額權重。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				T3.Focus();
				return;
			}
			try
			{
				Convert.ToDouble(T4.Value);
			}
			catch (Exception ex)
			{
				CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudgetResDial1.cs" + ex.Message);
				MessageBox.Show(this, "請輸入金額權重。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				T4.Focus();
				return;
			}
			if (Convert.ToDouble(T4.Value) < Convert.ToDouble(T3.Value))
			{
				double dTmp = Convert.ToDouble(T3.Value);
				T3.Value = T4.Value;
				T4.Value = dTmp;
			}
			(base.Owner as FormBudgetRes)._MnyRateType = 3;
			(base.Owner as FormBudgetRes)._Rate1 = Convert.ToDecimal(T3.Value);
			(base.Owner as FormBudgetRes)._Rate2 = Convert.ToDecimal(T4.Value);
		}
		base.DialogResult = DialogResult.OK;
	}

	private void T1_Enter(object sender, EventArgs e)
	{
		RB1.Checked = true;
		T1.SelectAll();
	}

	private void T2_Enter(object sender, EventArgs e)
	{
		RB2.Checked = true;
		T2.SelectAll();
	}

	private void T3_Enter(object sender, EventArgs e)
	{
		RB3.Checked = true;
		T3.SelectAll();
	}

	private void T4_Enter(object sender, EventArgs e)
	{
		RB3.Checked = true;
		T4.SelectAll();
	}

	private void FormBudgetResDial1_Load(object sender, EventArgs e)
	{
		LoadingScreen();
	}

	private void LoadingScreen()
	{
		string Status = CommonMethods.GetIniValue("ResDial", "WindowState");
		if (Status == "Maximized")
		{
			base.WindowState = FormWindowState.Maximized;
		}
		int iLoc_X = PubTools.Str2Int(CommonMethods.GetIniValue("ResDial", "PK_LocationX"));
		int iLoc_Y = PubTools.Str2Int(CommonMethods.GetIniValue("ResDial", "PK_LocationY"));
		int iSiz_W = PubTools.Str2Int(CommonMethods.GetIniValue("ResDial", "PK_Width"));
		int iSiz_H = PubTools.Str2Int(CommonMethods.GetIniValue("ResDial", "PK_Height"));
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

	private void FormBudgetResDial1_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (base.WindowState != FormWindowState.Maximized)
		{
			CommonMethods.WriteIniValue("ResDial", "PK_LocationX", base.Location.X.ToString());
			CommonMethods.WriteIniValue("ResDial", "PK_LocationY", base.Location.Y.ToString());
			CommonMethods.WriteIniValue("ResDial", "PK_Width", base.Size.Width.ToString());
			CommonMethods.WriteIniValue("ResDial", "PK_Height", base.Size.Height.ToString());
		}
		CommonMethods.WriteIniValue("ResDial", "WindowState", base.WindowState.ToString());
	}

	private void FormBudgetResDial1_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormBudgetResDial1");
		}
	}
}
