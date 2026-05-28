using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.STDClass;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace Archnowledge.Pcces.PccesMain.Compare;

public class FormCompareItm_Scope : Form
{
	private const string CallFormHelp = "FormCompareItm_Scope";

	private Panel panel6;

	private GroupBox groupBox3;

	private UltraButton F_Btn_Fnsh;

	private Panel panel1;

	private Panel panel2;

	private UltraLabel ultraLabel15;

	private UltraLabel ultraLabel16;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	private UltraLabel lblProjectCode;

	private UltraLabel lblProjectNameC;

	private UltraLabel ultraLabel4;

	private UltraNumericEditor txtScope;

	private UltraLabel ultraLabel5;

	private UltraTextEditor txtWorkUnit;

	private Container components = null;

	private string F_UserID;

	private string F_ProjectCode;

	private string F_ProjectNameC;

	private PccesFormAction F_ActionName;

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
			lblProjectCode.Text = F_ProjectCode;
		}
	}

	public string _ProjectNameC
	{
		get
		{
			return F_ProjectNameC;
		}
		set
		{
			F_ProjectNameC = value;
			lblProjectNameC.Text = F_ProjectNameC;
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

	public FormCompareItm_Scope()
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
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.Compare.FormCompareItm_Scope));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		this.panel6 = new System.Windows.Forms.Panel();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.F_Btn_Fnsh = new Infragistics.Win.Misc.UltraButton();
		this.panel1 = new System.Windows.Forms.Panel();
		this.ultraLabel15 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel16 = new Infragistics.Win.Misc.UltraLabel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.txtWorkUnit = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.txtScope = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.lblProjectNameC = new Infragistics.Win.Misc.UltraLabel();
		this.lblProjectCode = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.panel6.SuspendLayout();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtWorkUnit).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtScope).BeginInit();
		base.SuspendLayout();
		this.panel6.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel6.Controls.Add(this.groupBox3);
		this.panel6.Controls.Add(this.F_Btn_Fnsh);
		this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel6.Location = new System.Drawing.Point(0, 226);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(520, 44);
		this.panel6.TabIndex = 10;
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox3.Location = new System.Drawing.Point(0, 0);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(520, 8);
		this.groupBox3.TabIndex = 3;
		this.groupBox3.TabStop = false;
		this.F_Btn_Fnsh.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.F_Btn_Fnsh.Appearance = appearance1;
		this.F_Btn_Fnsh.BackColor = System.Drawing.SystemColors.Control;
		this.F_Btn_Fnsh.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.F_Btn_Fnsh.Font = new System.Drawing.Font("細明體", 11f);
		this.F_Btn_Fnsh.ImageSize = new System.Drawing.Size(20, 20);
		this.F_Btn_Fnsh.ImageTransparentColor = System.Drawing.Color.White;
		this.F_Btn_Fnsh.Location = new System.Drawing.Point(216, 10);
		this.F_Btn_Fnsh.Name = "F_Btn_Fnsh";
		this.F_Btn_Fnsh.ShowFocusRect = false;
		this.F_Btn_Fnsh.ShowOutline = false;
		this.F_Btn_Fnsh.Size = new System.Drawing.Size(88, 31);
		this.F_Btn_Fnsh.SupportThemes = false;
		this.F_Btn_Fnsh.TabIndex = 1;
		this.F_Btn_Fnsh.Text = "確定";
		this.F_Btn_Fnsh.Click += new System.EventHandler(F_Btn_Fnsh_Click);
		this.panel1.BackColor = System.Drawing.Color.White;
		this.panel1.Controls.Add(this.ultraLabel15);
		this.panel1.Controls.Add(this.ultraLabel16);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(520, 56);
		this.panel1.TabIndex = 11;
		appearance2.BackColor = System.Drawing.Color.White;
		this.ultraLabel15.Appearance = appearance2;
		this.ultraLabel15.Location = new System.Drawing.Point(24, 32);
		this.ultraLabel15.Name = "ultraLabel15";
		this.ultraLabel15.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel15.TabIndex = 5;
		this.ultraLabel15.Text = "這個專案尚未填入工程規模，會造成無法計算單位造價";
		appearance3.BackColor = System.Drawing.Color.White;
		this.ultraLabel16.Appearance = appearance3;
		this.ultraLabel16.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel16.Location = new System.Drawing.Point(8, 8);
		this.ultraLabel16.Name = "ultraLabel16";
		this.ultraLabel16.Size = new System.Drawing.Size(408, 20);
		this.ultraLabel16.TabIndex = 4;
		this.ultraLabel16.Text = "輸入工程規模";
		this.panel2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel2.Controls.Add(this.txtWorkUnit);
		this.panel2.Controls.Add(this.ultraLabel5);
		this.panel2.Controls.Add(this.txtScope);
		this.panel2.Controls.Add(this.ultraLabel4);
		this.panel2.Controls.Add(this.lblProjectNameC);
		this.panel2.Controls.Add(this.lblProjectCode);
		this.panel2.Controls.Add(this.ultraLabel2);
		this.panel2.Controls.Add(this.ultraLabel1);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Location = new System.Drawing.Point(0, 56);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(520, 170);
		this.panel2.TabIndex = 12;
		this.txtWorkUnit.Location = new System.Drawing.Point(88, 128);
		this.txtWorkUnit.MaxLength = 20;
		this.txtWorkUnit.Name = "txtWorkUnit";
		this.txtWorkUnit.Size = new System.Drawing.Size(312, 21);
		this.txtWorkUnit.TabIndex = 12;
		this.ultraLabel5.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel5.Location = new System.Drawing.Point(8, 128);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(88, 20);
		this.ultraLabel5.TabIndex = 11;
		this.ultraLabel5.Text = "工程單位:";
		this.txtScope.Location = new System.Drawing.Point(89, 93);
		this.txtScope.MaxValue = 1410065407;
		this.txtScope.MinValue = 0;
		this.txtScope.Name = "txtScope";
		this.txtScope.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
		this.txtScope.PromptChar = ' ';
		this.txtScope.Size = new System.Drawing.Size(311, 21);
		this.txtScope.TabIndex = 10;
		this.ultraLabel4.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel4.Location = new System.Drawing.Point(8, 96);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(88, 20);
		this.ultraLabel4.TabIndex = 9;
		this.ultraLabel4.Text = "工程規模:";
		this.lblProjectNameC.Location = new System.Drawing.Point(90, 41);
		this.lblProjectNameC.Name = "lblProjectNameC";
		this.lblProjectNameC.Size = new System.Drawing.Size(414, 47);
		this.lblProjectNameC.TabIndex = 8;
		this.lblProjectNameC.Text = "[ProjectNameC]";
		this.lblProjectCode.Location = new System.Drawing.Point(90, 12);
		this.lblProjectCode.Name = "lblProjectCode";
		this.lblProjectCode.Size = new System.Drawing.Size(414, 23);
		this.lblProjectCode.TabIndex = 7;
		this.lblProjectCode.Text = "[ProjectCode]";
		this.ultraLabel2.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel2.Location = new System.Drawing.Point(8, 40);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(88, 20);
		this.ultraLabel2.TabIndex = 6;
		this.ultraLabel2.Text = "專案名稱:";
		this.ultraLabel1.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.ultraLabel1.Location = new System.Drawing.Point(8, 13);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(88, 20);
		this.ultraLabel1.TabIndex = 5;
		this.ultraLabel1.Text = "專案代碼:";
		this.AutoScaleBaseSize = new System.Drawing.Size(6, 18);
		base.ClientSize = new System.Drawing.Size(520, 270);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.panel6);
		this.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.Name = "FormCompareItm_Scope";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "輸入工程規模";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormCompareItm_Scope_KeyDown);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormCompareItm_Scope_FormClosing);
		base.Load += new System.EventHandler(FormCompareItm_Scope_Load);
		this.panel6.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtWorkUnit).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtScope).EndInit();
		base.ResumeLayout(false);
	}

	private void F_Btn_Fnsh_Click(object sender, EventArgs e)
	{
		if (!CommonMethods.IsStrByteLenValid(txtWorkUnit.Text, 20))
		{
			MessageBox.Show(this, ultraLabel5.Text + "的長度不可超過 20 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtWorkUnit.Focus();
		}
		else if (PubTools.Str2Decimal(txtScope.Value) > 0m)
		{
			ArrayList aArr = new ArrayList();
			aArr.Clear();
			aArr.Add(F_UserID);
			aArr.Add("工程單位造價--填入工程規模");
			Archnowledge.Pcces.BUDClass.Project PROJ = new Archnowledge.Pcces.BUDClass.Project(aArr);
			PROJ.ps_projectCode = F_ProjectCode;
			PROJ.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
			PROJ.ps_projectScope = txtScope.Value.ToString();
			PROJ.ps_workUnit = txtWorkUnit.Text.Trim();
			PROJ.UpdItem();
			(base.Owner as FormCompareItm)._dec_TempValue = PubTools.Str2Decimal(txtScope.Value);
			base.DialogResult = DialogResult.OK;
		}
		else
		{
			MessageBox.Show(this, "工程規模的值必需大於 0", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtScope.Focus();
		}
	}

	private void FormCompareItm_Scope_Load(object sender, EventArgs e)
	{
		LoadingScreen();
	}

	private void LoadingScreen()
	{
		string Status = CommonMethods.GetIniValue("Compare_Scope", "WindowState");
		if (Status == "Maximized")
		{
			base.WindowState = FormWindowState.Maximized;
		}
		int iLoc_X = PubTools.Str2Int(CommonMethods.GetIniValue("Compare_Scope", "PK_LocationX"));
		int iLoc_Y = PubTools.Str2Int(CommonMethods.GetIniValue("Compare_Scope", "PK_LocationY"));
		int iSiz_W = PubTools.Str2Int(CommonMethods.GetIniValue("Compare_Scope", "PK_Width"));
		int iSiz_H = PubTools.Str2Int(CommonMethods.GetIniValue("Compare_Scope", "PK_Height"));
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

	private void FormCompareItm_Scope_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (base.WindowState != FormWindowState.Maximized)
		{
			CommonMethods.WriteIniValue("Compare_Scope", "PK_LocationX", base.Location.X.ToString());
			CommonMethods.WriteIniValue("Compare_Scope", "PK_LocationY", base.Location.Y.ToString());
			CommonMethods.WriteIniValue("Compare_Scope", "PK_Width", base.Size.Width.ToString());
			CommonMethods.WriteIniValue("Compare_Scope", "PK_Height", base.Size.Height.ToString());
		}
		CommonMethods.WriteIniValue("Compare_Scope", "WindowState", base.WindowState.ToString());
	}

	private void FormCompareItm_Scope_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormCompareItm_Scope");
		}
	}
}
