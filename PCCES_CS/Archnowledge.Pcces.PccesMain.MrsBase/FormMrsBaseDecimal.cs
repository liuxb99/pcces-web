using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace Archnowledge.Pcces.PccesMain.MrsBase;

public class FormMrsBaseDecimal : Form
{
	private const string FileIni = "OptionSet.ini";

	private const string CallFormHelp = "FormMrsBaseDecimal";

	private GroupBox groupBox1;

	private Label label1;

	private Label label2;

	private Label label3;

	private Label label4;

	private Label label5;

	private Label label6;

	private Label label7;

	private Label label8;

	private Label label9;

	private Label label10;

	private Label label11;

	private Label label12;

	private Label label13;

	private UltraButton ultraButton2;

	private UltraButton ultraButton1;

	private NumericUpDown DecimalAnaAmt;

	private NumericUpDown DecimalAnaCost;

	private NumericUpDown DecimalAnaQty;

	private NumericUpDown DecimalCost;

	private CheckBox chkInterlock;

	private Container components = null;

	private string F_UserID;

	private string FormStatus = "EDIT";

	private DataTable DT1 = new DataTable();

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

	public FormMrsBaseDecimal()
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
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.MrsBase.FormMrsBaseDecimal));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.chkInterlock = new System.Windows.Forms.CheckBox();
		this.label8 = new System.Windows.Forms.Label();
		this.DecimalAnaAmt = new System.Windows.Forms.NumericUpDown();
		this.label9 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.DecimalAnaCost = new System.Windows.Forms.NumericUpDown();
		this.label7 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.DecimalAnaQty = new System.Windows.Forms.NumericUpDown();
		this.label5 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.DecimalCost = new System.Windows.Forms.NumericUpDown();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.ultraButton2 = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.DecimalAnaAmt).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.DecimalAnaCost).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.DecimalAnaQty).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.DecimalCost).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.chkInterlock);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.DecimalAnaAmt);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.DecimalAnaCost);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.DecimalAnaQty);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.DecimalCost);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Location = new System.Drawing.Point(8, 24);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(352, 184);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.chkInterlock.Checked = true;
		this.chkInterlock.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkInterlock.Font = new System.Drawing.Font("細明體", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.chkInterlock.ForeColor = System.Drawing.Color.FromArgb(0, 0, 192);
		this.chkInterlock.Location = new System.Drawing.Point(16, 152);
		this.chkInterlock.Name = "chkInterlock";
		this.chkInterlock.Size = new System.Drawing.Size(320, 24);
		this.chkInterlock.TabIndex = 12;
		this.chkInterlock.Text = "【分析主項單價】取位與【分析項複價】連動";
		this.chkInterlock.CheckedChanged += new System.EventHandler(chkInterlock_CheckedChanged);
		this.label8.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.label8.Location = new System.Drawing.Point(168, 119);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(80, 22);
		this.label8.TabIndex = 11;
		this.label8.Text = "小數位數:";
		this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.DecimalAnaAmt.Font = new System.Drawing.Font("Arial", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.DecimalAnaAmt.Location = new System.Drawing.Point(248, 118);
		this.DecimalAnaAmt.Maximum = new decimal(new int[4] { 4, 0, 0, 0 });
		this.DecimalAnaAmt.Name = "DecimalAnaAmt";
		this.DecimalAnaAmt.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.DecimalAnaAmt.Size = new System.Drawing.Size(56, 25);
		this.DecimalAnaAmt.TabIndex = 10;
		this.DecimalAnaAmt.ValueChanged += new System.EventHandler(DecimalAnaAmt_ValueChanged);
		this.label9.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.label9.Location = new System.Drawing.Point(16, 119);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(120, 22);
		this.label9.TabIndex = 9;
		this.label9.Text = "分析項複價";
		this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label6.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.label6.Location = new System.Drawing.Point(168, 90);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(80, 22);
		this.label6.TabIndex = 8;
		this.label6.Text = "小數位數:";
		this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.DecimalAnaCost.Font = new System.Drawing.Font("Arial", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.DecimalAnaCost.Location = new System.Drawing.Point(248, 88);
		this.DecimalAnaCost.Maximum = new decimal(new int[4] { 4, 0, 0, 0 });
		this.DecimalAnaCost.Name = "DecimalAnaCost";
		this.DecimalAnaCost.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.DecimalAnaCost.Size = new System.Drawing.Size(56, 25);
		this.DecimalAnaCost.TabIndex = 7;
		this.label7.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.label7.Location = new System.Drawing.Point(16, 90);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(120, 22);
		this.label7.TabIndex = 6;
		this.label7.Text = "分析項單價";
		this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label4.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.label4.Location = new System.Drawing.Point(168, 61);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(80, 22);
		this.label4.TabIndex = 5;
		this.label4.Text = "小數位數:";
		this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.DecimalAnaQty.Font = new System.Drawing.Font("Arial", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.DecimalAnaQty.Location = new System.Drawing.Point(248, 58);
		this.DecimalAnaQty.Maximum = new decimal(new int[4] { 4, 0, 0, 0 });
		this.DecimalAnaQty.Name = "DecimalAnaQty";
		this.DecimalAnaQty.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.DecimalAnaQty.Size = new System.Drawing.Size(56, 25);
		this.DecimalAnaQty.TabIndex = 4;
		this.label5.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.label5.Location = new System.Drawing.Point(16, 61);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(120, 22);
		this.label5.TabIndex = 3;
		this.label5.Text = "分析項數量";
		this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label3.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.label3.Location = new System.Drawing.Point(168, 32);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(80, 22);
		this.label3.TabIndex = 2;
		this.label3.Text = "小數位數:";
		this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.DecimalCost.Font = new System.Drawing.Font("Arial", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.DecimalCost.Location = new System.Drawing.Point(248, 28);
		this.DecimalCost.Maximum = new decimal(new int[4] { 4, 0, 0, 0 });
		this.DecimalCost.Name = "DecimalCost";
		this.DecimalCost.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.DecimalCost.Size = new System.Drawing.Size(56, 25);
		this.DecimalCost.TabIndex = 1;
		this.DecimalCost.ValueChanged += new System.EventHandler(DecimalCost_ValueChanged);
		this.label2.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.label2.Location = new System.Drawing.Point(16, 32);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(120, 22);
		this.label2.TabIndex = 0;
		this.label2.Text = "分析主項單價";
		this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label1.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.label1.Location = new System.Drawing.Point(8, 8);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(352, 23);
		this.label1.TabIndex = 1;
		this.label1.Text = "請設定下列項目的小數位數";
		this.label10.Font = new System.Drawing.Font("細明體", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.label10.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.label10.Location = new System.Drawing.Point(30, 200);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(328, 24);
		this.label10.TabIndex = 2;
		this.label10.Text = "四捨五入後之新舊差值須小於5%，否則將保留舊值";
		this.label10.Visible = false;
		this.label11.Font = new System.Drawing.Font("細明體", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.label11.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.label11.Location = new System.Drawing.Point(30, 232);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(328, 40);
		this.label11.TabIndex = 2;
		this.label11.Text = "有下層單價分析之分析細項之分析單價，將會依據分析主項之小數設定四捨五入";
		this.label11.Click += new System.EventHandler(label11_Click);
		this.label12.AutoSize = true;
		this.label12.Font = new System.Drawing.Font("細明體", 6f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.label12.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.label12.Location = new System.Drawing.Point(15, 203);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(11, 13);
		this.label12.TabIndex = 3;
		this.label12.Text = "●";
		this.label12.Visible = false;
		this.label13.AutoSize = true;
		this.label13.Font = new System.Drawing.Font("細明體", 6f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.label13.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.label13.Location = new System.Drawing.Point(15, 232);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(11, 13);
		this.label13.TabIndex = 4;
		this.label13.Text = "●";
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraButton2.Appearance = appearance1;
		this.ultraButton2.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ultraButton2.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton2.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton2.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton2.Location = new System.Drawing.Point(272, 280);
		this.ultraButton2.Name = "ultraButton2";
		this.ultraButton2.ShowFocusRect = false;
		this.ultraButton2.ShowOutline = false;
		this.ultraButton2.Size = new System.Drawing.Size(88, 31);
		this.ultraButton2.SupportThemes = false;
		this.ultraButton2.TabIndex = 6;
		this.ultraButton2.Text = "取消";
		this.ultraButton2.Click += new System.EventHandler(ultraButton2_Click);
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraButton1.Appearance = appearance2;
		this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton1.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.ultraButton1.Font = new System.Drawing.Font("細明體", 11f);
		this.ultraButton1.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton1.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton1.Location = new System.Drawing.Point(179, 280);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.ShowFocusRect = false;
		this.ultraButton1.ShowOutline = false;
		this.ultraButton1.Size = new System.Drawing.Size(88, 31);
		this.ultraButton1.SupportThemes = false;
		this.ultraButton1.TabIndex = 7;
		this.ultraButton1.Text = "確定";
		this.ultraButton1.Click += new System.EventHandler(ultraButton1_Click);
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 20);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.CancelButton = this.ultraButton2;
		base.ClientSize = new System.Drawing.Size(368, 323);
		base.Controls.Add(this.ultraButton1);
		base.Controls.Add(this.ultraButton2);
		base.Controls.Add(this.label13);
		base.Controls.Add(this.label12);
		base.Controls.Add(this.label10);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.label11);
		this.Font = new System.Drawing.Font("細明體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.KeyPreview = true;
		base.Name = "FormMrsBaseDecimal";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "設定小數位數";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormMrsBaseDecimal_KeyDown);
		base.Load += new System.EventHandler(FormMrsBaseDecimal_Load);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.DecimalAnaAmt).EndInit();
		((System.ComponentModel.ISupportInitialize)this.DecimalAnaCost).EndInit();
		((System.ComponentModel.ISupportInitialize)this.DecimalAnaQty).EndInit();
		((System.ComponentModel.ISupportInitialize)this.DecimalCost).EndInit();
		base.ResumeLayout(false);
	}

	private void label11_Click(object sender, EventArgs e)
	{
	}

	private void ultraButton2_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void FormMrsBaseDecimal_Load(object sender, EventArgs e)
	{
		CorrectRatio();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("WinFORM 基本工料");
		PubDecimal dbDecimal = new PubDecimal(aArr);
		DT1 = dbDecimal.ListItem("", "");
		if (DT1.Rows.Count > 0)
		{
			FormStatus = "EDIT";
			DecimalCost.Value = Convert.ToInt32(DT1.Rows[0]["itemCost"]);
			DecimalAnaQty.Value = Convert.ToInt32(DT1.Rows[0]["analysisQty"]);
			DecimalAnaCost.Value = Convert.ToInt32(DT1.Rows[0]["analysisCost"]);
			DecimalAnaAmt.Value = Convert.ToInt32(DT1.Rows[0]["analysisAmt"]);
		}
		else
		{
			FormStatus = "INSERT";
			DecimalCost.Value = 2m;
			DecimalAnaQty.Value = 3m;
			DecimalAnaCost.Value = 2m;
			DecimalAnaAmt.Value = 2m;
		}
		string sPath = CommonMethods.ExtractFilePath(Application.ExecutablePath);
		string sIsInterLock = CommonMethods.IniReadValue(sPath + "OptionSet.ini", "MrsBase", "IsDecimalInterlock");
		if (sIsInterLock.ToUpper() == "TRUE")
		{
			chkInterlock.Checked = true;
		}
		else
		{
			chkInterlock.Checked = false;
		}
	}

	private void ultraButton1_Click(object sender, EventArgs e)
	{
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(F_UserID);
		aArr.Add("WinFORM 基本工料");
		PubDecimal dbDecimal = new PubDecimal(aArr);
		dbDecimal.ps_projectCode = null;
		dbDecimal.ps_itemCost = DecimalCost.Value.ToString();
		dbDecimal.ps_analysisQty = DecimalAnaQty.Value.ToString();
		dbDecimal.ps_analysisCost = DecimalAnaCost.Value.ToString();
		dbDecimal.ps_analysisAmt = DecimalAnaAmt.Value.ToString();
		if (FormStatus == "EDIT")
		{
			dbDecimal.UpdItem();
		}
		else
		{
			dbDecimal.InseItem();
		}
		string sPath = CommonMethods.ExtractFilePath(Application.ExecutablePath);
		if (chkInterlock.Checked)
		{
			CommonMethods.IniWriteValue(sPath + "OptionSet.ini", "MrsBase", "IsDecimalInterlock", "TRUE");
		}
		else
		{
			CommonMethods.IniWriteValue(sPath + "OptionSet.ini", "MrsBase", "IsDecimalInterlock", "FALSE");
		}
		Close();
	}

	private void DecimalCost_ValueChanged(object sender, EventArgs e)
	{
		if (chkInterlock.Checked)
		{
			DecimalAnaAmt.Value = DecimalCost.Value;
		}
	}

	private void DecimalAnaAmt_ValueChanged(object sender, EventArgs e)
	{
		if (!chkInterlock.Checked)
		{
		}
	}

	private void CorrectRatio()
	{
		double ratio = CommonMethods.GetWindowRatio(base.Handle);
		if (ratio != 1.0)
		{
			groupBox1.Font = new Font(groupBox1.Font.Name, (float)((double)groupBox1.Font.Size * ratio));
			label10.Font = new Font(label10.Font.Name, (float)((double)label10.Font.Size * ratio));
			label11.Font = new Font(label11.Font.Name, (float)((double)label11.Font.Size * ratio));
			label12.Font = new Font(label12.Font.Name, (float)((double)label12.Font.Size * ratio));
			label13.Font = new Font(label13.Font.Name, (float)((double)label13.Font.Size * ratio));
		}
	}

	private void chkInterlock_CheckedChanged(object sender, EventArgs e)
	{
		if (chkInterlock.Checked)
		{
			DecimalCost.Value = DecimalAnaAmt.Value;
		}
	}

	private void FormMrsBaseDecimal_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormMrsBaseDecimal");
		}
	}
}
