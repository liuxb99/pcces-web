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

public class FormBudgetResFillRate : Form
{
	private const string CallFormHelp = "FormBudgetResFillRate";

	private Panel panel5;

	private UltraButton ultraButton4;

	private UltraButton BtnPick;

	private UltraOptionSet OptionSet1;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	private GroupBox groupBox1;

	private UltraLabel ultraLabel3;

	private UltraLabel ultraLabel4;

	private NumericUpDown upL;

	private NumericUpDown upE;

	private NumericUpDown upM;

	private NumericUpDown upW;

	private UltraLabel ultraLabel5;

	private GroupBox groupBox2;

	private Container components = null;

	private string F_UserID;

	private string F_ProjectCode;

	private UltraLabel ultraLabel6;

	private UltraLabel lblRateSum;

	private GroupBox groupBox3;

	private UltraLabel ultraLabel7;

	private PccesFormAction F_ActionName;

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

	public PccesFormAction _ActionName
	{
		get
		{
			return F_ActionName;
		}
		set
		{
			F_ActionName = value;
		}
	}

	public FormBudgetResFillRate()
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
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormBudgetResFillRate));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		this.panel5 = new System.Windows.Forms.Panel();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.ultraButton4 = new Infragistics.Win.Misc.UltraButton();
		this.BtnPick = new Infragistics.Win.Misc.UltraButton();
		this.OptionSet1 = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.lblRateSum = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.upW = new System.Windows.Forms.NumericUpDown();
		this.upM = new System.Windows.Forms.NumericUpDown();
		this.upE = new System.Windows.Forms.NumericUpDown();
		this.upL = new System.Windows.Forms.NumericUpDown();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.panel5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.OptionSet1).BeginInit();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.upW).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.upM).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.upE).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.upL).BeginInit();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.panel5.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel5.Controls.Add(this.groupBox3);
		this.panel5.Controls.Add(this.ultraButton4);
		this.panel5.Controls.Add(this.BtnPick);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel5.Location = new System.Drawing.Point(0, 334);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(496, 40);
		this.panel5.TabIndex = 5;
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox3.Location = new System.Drawing.Point(0, 0);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(496, 5);
		this.groupBox3.TabIndex = 11;
		this.groupBox3.TabStop = false;
		this.ultraButton4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton4.Appearance = appearance1;
		this.ultraButton4.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton4.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		appearance2.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance2.BackColor2 = System.Drawing.Color.White;
		appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.ultraButton4.HotTrackAppearance = appearance2;
		this.ultraButton4.HotTracking = true;
		this.ultraButton4.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton4.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton4.Location = new System.Drawing.Point(412, 8);
		this.ultraButton4.Name = "ultraButton4";
		this.ultraButton4.ShowFocusRect = false;
		this.ultraButton4.ShowOutline = false;
		this.ultraButton4.Size = new System.Drawing.Size(80, 28);
		this.ultraButton4.SupportThemes = false;
		this.ultraButton4.TabIndex = 10;
		this.ultraButton4.Text = "取消";
		this.BtnPick.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance3.Image = resources.GetObject("appearance3.Image");
		appearance3.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnPick.Appearance = appearance3;
		this.BtnPick.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		appearance4.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance4.BackColor2 = System.Drawing.Color.White;
		appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.BtnPick.HotTrackAppearance = appearance4;
		this.BtnPick.HotTracking = true;
		this.BtnPick.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnPick.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnPick.Location = new System.Drawing.Point(330, 8);
		this.BtnPick.Name = "BtnPick";
		this.BtnPick.ShowFocusRect = false;
		this.BtnPick.ShowOutline = false;
		this.BtnPick.Size = new System.Drawing.Size(80, 28);
		this.BtnPick.SupportThemes = false;
		this.BtnPick.TabIndex = 9;
		this.BtnPick.Text = "確定";
		this.BtnPick.Click += new System.EventHandler(BtnPick_Click);
		this.OptionSet1.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
		this.OptionSet1.CheckedIndex = 0;
		this.OptionSet1.ItemAppearance = appearance5;
		valueListItem1.DataValue = "0";
		valueListItem1.DisplayText = "人、機、料、雜項比率皆為 0 的項目";
		valueListItem2.DataValue = "1";
		valueListItem2.DisplayText = "所有非單價分析項目";
		valueListItem3.DataValue = "2";
		valueListItem3.DisplayText = "依各工項代碼開頭字母給定比率";
		this.OptionSet1.Items.Add(valueListItem1);
		this.OptionSet1.Items.Add(valueListItem2);
		this.OptionSet1.Items.Add(valueListItem3);
		this.OptionSet1.ItemSpacingVertical = 10;
		this.OptionSet1.Location = new System.Drawing.Point(16, 16);
		this.OptionSet1.Name = "OptionSet1";
		this.OptionSet1.Size = new System.Drawing.Size(440, 88);
		this.OptionSet1.TabIndex = 6;
		this.OptionSet1.Text = "人、機、料、雜項比率皆為 0 的項目";
		this.OptionSet1.ValueChanged += new System.EventHandler(OptionSet1_ValueChanged);
		this.ultraLabel1.Location = new System.Drawing.Point(32, 27);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(48, 23);
		this.ultraLabel1.TabIndex = 7;
		this.ultraLabel1.Text = "人工:";
		this.ultraLabel1.Click += new System.EventHandler(ultraLabel1_Click);
		this.ultraLabel2.Location = new System.Drawing.Point(32, 53);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(48, 23);
		this.ultraLabel2.TabIndex = 8;
		this.ultraLabel2.Text = "材料:";
		this.ultraLabel2.Click += new System.EventHandler(ultraLabel2_Click);
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.lblRateSum);
		this.groupBox1.Controls.Add(this.ultraLabel6);
		this.groupBox1.Controls.Add(this.upW);
		this.groupBox1.Controls.Add(this.upM);
		this.groupBox1.Controls.Add(this.upE);
		this.groupBox1.Controls.Add(this.upL);
		this.groupBox1.Controls.Add(this.ultraLabel3);
		this.groupBox1.Controls.Add(this.ultraLabel4);
		this.groupBox1.Controls.Add(this.ultraLabel2);
		this.groupBox1.Controls.Add(this.ultraLabel1);
		this.groupBox1.Location = new System.Drawing.Point(16, 8);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(464, 104);
		this.groupBox1.TabIndex = 9;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "各項比率";
		appearance6.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
		appearance6.FontData.SizeInPoints = 9f;
		appearance6.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		appearance6.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance6.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblRateSum.Appearance = appearance6;
		this.lblRateSum.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		this.lblRateSum.Location = new System.Drawing.Point(402, 78);
		this.lblRateSum.Name = "lblRateSum";
		this.lblRateSum.Size = new System.Drawing.Size(48, 18);
		this.lblRateSum.TabIndex = 16;
		this.lblRateSum.Text = "100";
		this.lblRateSum.TextChanged += new System.EventHandler(lblRateSum_TextChanged);
		appearance7.FontData.SizeInPoints = 9f;
		this.ultraLabel6.Appearance = appearance7;
		this.ultraLabel6.Location = new System.Drawing.Point(306, 81);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(96, 16);
		this.ultraLabel6.TabIndex = 15;
		this.ultraLabel6.Text = "各項比率總合:";
		this.upW.Location = new System.Drawing.Point(311, 51);
		this.upW.Name = "upW";
		this.upW.Size = new System.Drawing.Size(64, 25);
		this.upW.TabIndex = 14;
		this.upW.Value = new decimal(new int[4] { 10, 0, 0, 0 });
		this.upW.ValueChanged += new System.EventHandler(upL_ValueChanged);
		this.upM.Location = new System.Drawing.Point(88, 51);
		this.upM.Name = "upM";
		this.upM.Size = new System.Drawing.Size(64, 25);
		this.upM.TabIndex = 13;
		this.upM.Value = new decimal(new int[4] { 40, 0, 0, 0 });
		this.upM.ValueChanged += new System.EventHandler(upL_ValueChanged);
		this.upE.Location = new System.Drawing.Point(311, 24);
		this.upE.Name = "upE";
		this.upE.Size = new System.Drawing.Size(64, 25);
		this.upE.TabIndex = 12;
		this.upE.Value = new decimal(new int[4] { 20, 0, 0, 0 });
		this.upE.ValueChanged += new System.EventHandler(upL_ValueChanged);
		this.upL.Location = new System.Drawing.Point(88, 24);
		this.upL.Name = "upL";
		this.upL.Size = new System.Drawing.Size(64, 25);
		this.upL.TabIndex = 11;
		this.upL.Value = new decimal(new int[4] { 30, 0, 0, 0 });
		this.upL.ValueChanged += new System.EventHandler(upL_ValueChanged);
		this.ultraLabel3.Location = new System.Drawing.Point(247, 53);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(48, 23);
		this.ultraLabel3.TabIndex = 10;
		this.ultraLabel3.Text = "雜項:";
		this.ultraLabel4.Location = new System.Drawing.Point(247, 27);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(48, 23);
		this.ultraLabel4.TabIndex = 9;
		this.ultraLabel4.Text = "機具:";
		this.ultraLabel5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		appearance8.ForeColor = System.Drawing.Color.FromArgb(192, 64, 0);
		this.ultraLabel5.Appearance = appearance8;
		this.ultraLabel5.Location = new System.Drawing.Point(16, 264);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(464, 61);
		this.ultraLabel5.TabIndex = 10;
		this.ultraLabel5.Text = "說明:\r\n比率快速填入時，會依上方【各項比率】內的值填入，使用者可自行改變。請依合理比率調整。";
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.Controls.Add(this.OptionSet1);
		this.groupBox2.Controls.Add(this.ultraLabel7);
		this.groupBox2.Location = new System.Drawing.Point(16, 120);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(464, 136);
		this.groupBox2.TabIndex = 11;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "方式";
		this.ultraLabel7.Location = new System.Drawing.Point(32, 104);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(408, 23);
		this.ultraLabel7.TabIndex = 8;
		this.ultraLabel7.Text = "註:此項目不會使用上方之各項比率";
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		this.BackColor = System.Drawing.Color.White;
		base.CancelButton = this.ultraButton4;
		base.ClientSize = new System.Drawing.Size(496, 374);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.ultraLabel5);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.panel5);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.Name = "FormBudgetResFillRate";
		this.Text = "快速填入各項比率";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormBudgetResFillRate_KeyDown);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormBudgetResFillRate_FormClosing);
		base.Load += new System.EventHandler(FormBudgetResFillRate_Load);
		this.panel5.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.OptionSet1).EndInit();
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.upW).EndInit();
		((System.ComponentModel.ISupportInitialize)this.upM).EndInit();
		((System.ComponentModel.ISupportInitialize)this.upE).EndInit();
		((System.ComponentModel.ISupportInitialize)this.upL).EndInit();
		this.groupBox2.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	private void ultraLabel2_Click(object sender, EventArgs e)
	{
	}

	private void ultraLabel1_Click(object sender, EventArgs e)
	{
	}

	private void BtnPick_Click(object sender, EventArgs e)
	{
		ProcessFillRate();
		base.DialogResult = DialogResult.OK;
	}

	private void ProcessFillRate()
	{
		string sSQL = "";
		string sTbl = "";
		if (F_ActionName == PccesFormAction.BUD)
		{
			sTbl = "budProjMrsA";
		}
		if (F_ActionName == PccesFormAction.BID)
		{
			sTbl = "bidProjMrsA";
		}
		if (F_ActionName == PccesFormAction.MrsBase)
		{
			sTbl = "MrsBaseA";
		}
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = F_UserID;
		switch (OptionSet1.CheckedIndex)
		{
		case 0:
			sSQL = "Update " + sTbl + " set lRate = " + upL.Value + ",     eRate = " + upE.Value + ",     mRate = " + upM.Value + ",     wRate = " + upW.Value + " Where lRate=0 and eRate=0 and mRate=0 and wRate=0 and ( analysis<>'1' or analysis is null )  ";
			if (sTbl.ToUpper() != "MRSBASEA")
			{
				sSQL = sSQL + " and ProjectCode ='" + F_ProjectCode + "' ";
			}
			break;
		case 1:
			sSQL = "Update " + sTbl + " set lRate = " + upL.Value + ",     eRate = " + upE.Value + ",     mRate = " + upM.Value + ",     wRate = " + upW.Value + " Where ( analysis<>'1' or analysis is null )  ";
			if (sTbl.ToUpper() != "MRSBASEA")
			{
				sSQL = sSQL + " and ProjectCode ='" + F_ProjectCode + "' ";
			}
			break;
		case 2:
			sSQL = ((!(sTbl.ToUpper() != "MRSBASEA")) ? ("update " + sTbl + " set lRate = 100 where Upper(SubString(PccesCode,1,1))='L' " + '\r' + "update " + sTbl + " set eRate = 100 where Upper(SubString(PccesCode,1,1))='E' " + '\r' + "update " + sTbl + " set mRate = 100 where Upper(SubString(PccesCode,1,1))='M' " + '\r' + "update " + sTbl + " set wRate = 100 where Upper(SubString(PccesCode,1,1))='W' " + '\r') : ("update " + sTbl + " set lRate = 100 where Upper(SubString(PccesCode,1,1))='L' and ProjectCode='" + F_ProjectCode + "' " + '\r' + "update " + sTbl + " set eRate = 100 where Upper(SubString(PccesCode,1,1))='E' and ProjectCode='" + F_ProjectCode + "' " + '\r' + "update " + sTbl + " set mRate = 100 where Upper(SubString(PccesCode,1,1))='M' and ProjectCode='" + F_ProjectCode + "' " + '\r' + "update " + sTbl + " set wRate = 100 where Upper(SubString(PccesCode,1,1))='W' and ProjectCode='" + F_ProjectCode + "' " + '\r'));
			break;
		}
		DBCLS.ExecuteCommand(sSQL);
	}

	private void upL_ValueChanged(object sender, EventArgs e)
	{
		lblRateSum.Text = (upL.Value + upE.Value + upM.Value + upW.Value).ToString();
	}

	private void OptionSet1_ValueChanged(object sender, EventArgs e)
	{
		if (OptionSet1.CheckedIndex == 2)
		{
			groupBox1.Enabled = false;
		}
		else
		{
			groupBox1.Enabled = true;
		}
	}

	private void lblRateSum_TextChanged(object sender, EventArgs e)
	{
		if (PubTools.Str2Int(lblRateSum.Text) == 100)
		{
			BtnPick.Enabled = true;
		}
		else
		{
			BtnPick.Enabled = false;
		}
	}

	private void FormBudgetResFillRate_Load(object sender, EventArgs e)
	{
		LoadingScreen();
	}

	private void LoadingScreen()
	{
		string Status = CommonMethods.GetIniValue("ResFillRate", "WindowState");
		if (Status == "Maximized")
		{
			base.WindowState = FormWindowState.Maximized;
		}
		int iLoc_X = PubTools.Str2Int(CommonMethods.GetIniValue("ResFillRate", "PK_LocationX"));
		int iLoc_Y = PubTools.Str2Int(CommonMethods.GetIniValue("ResFillRate", "PK_LocationY"));
		int iSiz_W = PubTools.Str2Int(CommonMethods.GetIniValue("ResFillRate", "PK_Width"));
		int iSiz_H = PubTools.Str2Int(CommonMethods.GetIniValue("ResFillRate", "PK_Height"));
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

	private void FormBudgetResFillRate_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (base.WindowState != FormWindowState.Maximized)
		{
			CommonMethods.WriteIniValue("ResFillRate", "PK_LocationX", base.Location.X.ToString());
			CommonMethods.WriteIniValue("ResFillRate", "PK_LocationY", base.Location.Y.ToString());
			CommonMethods.WriteIniValue("ResFillRate", "PK_Width", base.Size.Width.ToString());
			CommonMethods.WriteIniValue("ResFillRate", "PK_Height", base.Size.Height.ToString());
		}
		CommonMethods.WriteIniValue("ResFillRate", "WindowState", base.WindowState.ToString());
	}

	private void FormBudgetResFillRate_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormBudgetResFillRate");
		}
	}
}
