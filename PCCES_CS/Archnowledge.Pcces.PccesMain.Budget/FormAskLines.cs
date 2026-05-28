using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormAskLines : Form
{
	private const string CallFormHelp = "FormAskLines";

	private string F_Question = "";

	private string F_Answer = "";

	private UltraButton ultraButton1;

	private Label lblQuestion;

	private Container components = null;

	private UltraNumericEditor txtAnswer;

	private UltraButton ultraButton2;

	public string _Question
	{
		get
		{
			return F_Question;
		}
		set
		{
			F_Question = value;
			lblQuestion.Text = F_Question;
		}
	}

	public string _Answer
	{
		get
		{
			return F_Answer;
		}
		set
		{
			F_Answer = value;
			txtAnswer.Text = F_Answer;
		}
	}

	public FormAskLines()
	{
		InitializeComponent();
	}

	private void FormAskLines_FormClosing(object sender, FormClosingEventArgs e)
	{
	}

	private void ultraButton1_Click(object sender, EventArgs e)
	{
		F_Answer = txtAnswer.Text;
		Hide();
		SendToBack();
	}

	private void txtAnswer_KeyDown(object sender, KeyEventArgs e)
	{
	}

	private void txtAnswer_Validating(object sender, CancelEventArgs e)
	{
		int iNum = 0;
		try
		{
			iNum = Convert.ToInt32(txtAnswer.Text.Trim());
			if (iNum > 50)
			{
				MessageBox.Show(this, "同一次新增筆數不可大於 50 ", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtAnswer.Text = "50";
				txtAnswer.Focus();
			}
			else if (iNum <= 0)
			{
				MessageBox.Show(this, "新增筆數不可小於 1 ", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtAnswer.Text = "1";
				txtAnswer.Focus();
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Budget.FormAskLines.cs" + ex.Message);
			MessageBox.Show(this, "輸入格式有誤", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void FormAskLines_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormAskLines");
		}
	}

	private void InitializeComponent()
	{
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormAskLines));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		this.lblQuestion = new System.Windows.Forms.Label();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.txtAnswer = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
		this.ultraButton2 = new Infragistics.Win.Misc.UltraButton();
		((System.ComponentModel.ISupportInitialize)this.txtAnswer).BeginInit();
		base.SuspendLayout();
		this.lblQuestion.AutoSize = true;
		this.lblQuestion.Location = new System.Drawing.Point(12, 12);
		this.lblQuestion.Name = "lblQuestion";
		this.lblQuestion.Size = new System.Drawing.Size(95, 15);
		this.lblQuestion.TabIndex = 0;
		this.lblQuestion.Text = "lblQuestion";
		appearance1.Image = resources.GetObject("appearance1.Image");
		this.ultraButton1.Appearance = appearance1;
		this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton1.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.ultraButton1.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton1.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton1.Location = new System.Drawing.Point(91, 112);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.ShowFocusRect = false;
		this.ultraButton1.ShowOutline = false;
		this.ultraButton1.Size = new System.Drawing.Size(96, 32);
		this.ultraButton1.SupportThemes = false;
		this.ultraButton1.TabIndex = 2;
		this.ultraButton1.Text = "確定";
		this.ultraButton1.Click += new System.EventHandler(ultraButton1_Click);
		appearance2.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance2.FontData.Name = "細明體";
		appearance2.FontData.SizeInPoints = 14f;
		this.txtAnswer.Appearance = appearance2;
		this.txtAnswer.Location = new System.Drawing.Point(16, 72);
		this.txtAnswer.MaxValue = 50;
		this.txtAnswer.MinValue = 1;
		this.txtAnswer.Name = "txtAnswer";
		this.txtAnswer.PromptChar = ' ';
		this.txtAnswer.Size = new System.Drawing.Size(356, 28);
		this.txtAnswer.SpinButtonDisplayStyle = Infragistics.Win.ButtonDisplayStyle.Always;
		this.txtAnswer.SupportThemes = false;
		this.txtAnswer.TabIndex = 4;
		this.txtAnswer.Validating += new System.ComponentModel.CancelEventHandler(txtAnswer_Validating);
		appearance3.Image = resources.GetObject("appearance3.Image");
		this.ultraButton2.Appearance = appearance3;
		this.ultraButton2.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ultraButton2.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton2.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton2.Location = new System.Drawing.Point(195, 112);
		this.ultraButton2.Name = "ultraButton2";
		this.ultraButton2.ShowFocusRect = false;
		this.ultraButton2.ShowOutline = false;
		this.ultraButton2.Size = new System.Drawing.Size(96, 32);
		this.ultraButton2.SupportThemes = false;
		this.ultraButton2.TabIndex = 5;
		this.ultraButton2.Text = "取消";
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		base.ClientSize = new System.Drawing.Size(384, 153);
		base.Controls.Add(this.ultraButton2);
		base.Controls.Add(this.txtAnswer);
		base.Controls.Add(this.ultraButton1);
		base.Controls.Add(this.lblQuestion);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormAskLines";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "詢問:";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormAskLines_FormClosing);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormAskLines_KeyDown);
		((System.ComponentModel.ISupportInitialize)this.txtAnswer).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
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
