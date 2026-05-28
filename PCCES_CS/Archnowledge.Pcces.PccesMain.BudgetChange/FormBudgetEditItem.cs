using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.CTRClass;
using Archnowledge.Pcces.STDClass;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace Archnowledge.Pcces.PccesMain.BudgetChange;

public class FormBudgetEditItem : Form
{
	private const string CallFormHelp = "FormBudgetEditItem";

	private Panel panel1;

	private UltraComboEditor txtEName;

	private UltraComboEditor txtCName;

	private TextBox textBox4;

	private UltraLabel ultraLabel4;

	private UltraCombo cboEUnit;

	private UltraCombo cboCUnit;

	private UltraLabel ultraLabel15;

	private UltraLabel ultraLabel14;

	private TextBox txtItemNo;

	private UltraLabel ultraLabel3;

	private UltraLabel ultraLabel2;

	private UltraLabel ultraLabel1;

	private TextBox txtMemo;

	private UltraLabel ultraLabel7;

	private Panel panel2;

	private GroupBox groupBox2;

	private TextBox txtCost;

	private UltraLabel ultraLabel6;

	private TextBox txtQty;

	private UltraLabel ultraLabel5;

	private GroupBox groupBox1;

	private UltraOptionSet optItemType;

	private Panel panel16;

	private GroupBox groupBox6;

	private UltraButton D_Btn_Cncl;

	private UltraButton D_Btn_Next;

	private DataTable DT_Temp;

	private string F_ProjectCode;

	private string F_SubProjectCode;

	private int F_ChgCount;

	private string F_UserID;

	private int F_ChildCount = 0;

	private DataRow F_DR_forUpd;

	private Container components = null;

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

	public int _ChgCount
	{
		get
		{
			return F_ChgCount;
		}
		set
		{
			F_ChgCount = value;
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

	public int _ChildCount
	{
		get
		{
			return F_ChildCount;
		}
		set
		{
			F_ChildCount = value;
		}
	}

	public DataRow _DR_forUpd
	{
		get
		{
			return F_DR_forUpd;
		}
		set
		{
			F_DR_forUpd = value;
		}
	}

	public FormBudgetEditItem()
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
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinGrid.UltraGridLayout ultraGridLayout1 = new Infragistics.Win.UltraWinGrid.UltraGridLayout();
		Infragistics.Win.ValueList valueList1 = new Infragistics.Win.ValueList(86092282);
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
		Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
		Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Archnowledge.Pcces.PccesMain.BudgetChange.FormBudgetEditItem));
		Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
		this.panel1 = new System.Windows.Forms.Panel();
		this.txtMemo = new System.Windows.Forms.TextBox();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.txtEName = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.txtCName = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
		this.textBox4 = new System.Windows.Forms.TextBox();
		this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel15 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel14 = new Infragistics.Win.Misc.UltraLabel();
		this.txtItemNo = new System.Windows.Forms.TextBox();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.cboEUnit = new Infragistics.Win.UltraWinGrid.UltraCombo();
		this.cboCUnit = new Infragistics.Win.UltraWinGrid.UltraCombo();
		this.panel2 = new System.Windows.Forms.Panel();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.txtCost = new System.Windows.Forms.TextBox();
		this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
		this.txtQty = new System.Windows.Forms.TextBox();
		this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.optItemType = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
		this.panel16 = new System.Windows.Forms.Panel();
		this.groupBox6 = new System.Windows.Forms.GroupBox();
		this.D_Btn_Cncl = new Infragistics.Win.Misc.UltraButton();
		this.D_Btn_Next = new Infragistics.Win.Misc.UltraButton();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtEName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboEUnit).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCUnit).BeginInit();
		this.panel2.SuspendLayout();
		this.groupBox2.SuspendLayout();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.optItemType).BeginInit();
		this.panel16.SuspendLayout();
		base.SuspendLayout();
		this.panel1.Controls.Add(this.txtMemo);
		this.panel1.Controls.Add(this.ultraLabel7);
		this.panel1.Controls.Add(this.txtEName);
		this.panel1.Controls.Add(this.txtCName);
		this.panel1.Controls.Add(this.textBox4);
		this.panel1.Controls.Add(this.ultraLabel4);
		this.panel1.Controls.Add(this.ultraLabel15);
		this.panel1.Controls.Add(this.ultraLabel14);
		this.panel1.Controls.Add(this.txtItemNo);
		this.panel1.Controls.Add(this.ultraLabel3);
		this.panel1.Controls.Add(this.ultraLabel2);
		this.panel1.Controls.Add(this.ultraLabel1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(747, 128);
		this.panel1.TabIndex = 1;
		this.txtMemo.Location = new System.Drawing.Point(112, 96);
		this.txtMemo.Name = "txtMemo";
		this.txtMemo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.txtMemo.Size = new System.Drawing.Size(624, 25);
		this.txtMemo.TabIndex = 23;
		this.txtMemo.Text = "[txtMemo]";
		this.ultraLabel7.Location = new System.Drawing.Point(12, 99);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(64, 16);
		this.ultraLabel7.TabIndex = 22;
		this.ultraLabel7.Text = "備註:";
		appearance1.FontData.Name = "細明體";
		appearance1.FontData.SizeInPoints = 11f;
		this.txtEName.Appearance = appearance1;
		this.txtEName.Location = new System.Drawing.Point(112, 66);
		this.txtEName.Name = "txtEName";
		this.txtEName.Size = new System.Drawing.Size(484, 24);
		this.txtEName.TabIndex = 21;
		this.txtEName.Text = null;
		appearance2.FontData.Name = "細明體";
		appearance2.FontData.SizeInPoints = 11f;
		this.txtCName.Appearance = appearance2;
		this.txtCName.Location = new System.Drawing.Point(112, 38);
		this.txtCName.Name = "txtCName";
		this.txtCName.Size = new System.Drawing.Size(484, 24);
		this.txtCName.TabIndex = 20;
		this.txtCName.Text = null;
		this.textBox4.Location = new System.Drawing.Point(536, 8);
		this.textBox4.Name = "textBox4";
		this.textBox4.Size = new System.Drawing.Size(200, 25);
		this.textBox4.TabIndex = 19;
		this.textBox4.Text = "textBox4";
		this.textBox4.Visible = false;
		appearance3.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel4.Appearance = appearance3;
		this.ultraLabel4.Location = new System.Drawing.Point(490, 12);
		this.ultraLabel4.Name = "ultraLabel4";
		this.ultraLabel4.Size = new System.Drawing.Size(44, 20);
		this.ultraLabel4.TabIndex = 18;
		this.ultraLabel4.Text = "父項:";
		this.ultraLabel4.Visible = false;
		this.ultraLabel15.Location = new System.Drawing.Point(608, 40);
		this.ultraLabel15.Name = "ultraLabel15";
		this.ultraLabel15.Size = new System.Drawing.Size(47, 20);
		this.ultraLabel15.TabIndex = 15;
		this.ultraLabel15.Text = "單位:";
		this.ultraLabel14.Location = new System.Drawing.Point(608, 65);
		this.ultraLabel14.Name = "ultraLabel14";
		this.ultraLabel14.Size = new System.Drawing.Size(47, 20);
		this.ultraLabel14.TabIndex = 14;
		this.ultraLabel14.Text = "Unit:";
		this.txtItemNo.Location = new System.Drawing.Point(112, 8);
		this.txtItemNo.Name = "txtItemNo";
		this.txtItemNo.Size = new System.Drawing.Size(200, 25);
		this.txtItemNo.TabIndex = 3;
		this.txtItemNo.Text = "[txtItemNo]";
		appearance4.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel3.Appearance = appearance4;
		this.ultraLabel3.Location = new System.Drawing.Point(12, 64);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(100, 20);
		this.ultraLabel3.TabIndex = 2;
		this.ultraLabel3.Text = "Description:";
		appearance5.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel2.Appearance = appearance5;
		this.ultraLabel2.Location = new System.Drawing.Point(12, 40);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(92, 20);
		this.ultraLabel2.TabIndex = 1;
		this.ultraLabel2.Text = "項目及說明:";
		appearance6.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel1.Appearance = appearance6;
		this.ultraLabel1.Location = new System.Drawing.Point(12, 13);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(76, 18);
		this.ultraLabel1.TabIndex = 0;
		this.ultraLabel1.Text = "項次:";
		this.cboEUnit.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.cboEUnit.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
		this.cboEUnit.DisplayMember = "";
		this.cboEUnit.Location = new System.Drawing.Point(17, 17);
		this.cboEUnit.Name = "cboEUnit";
		this.cboEUnit.Size = new System.Drawing.Size(84, 21);
		this.cboEUnit.TabIndex = 17;
		this.cboEUnit.ValueMember = "";
		this.cboCUnit.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.cboCUnit.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
		this.cboCUnit.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Dotted;
		this.cboCUnit.DisplayLayout.BorderStyleCaption = Infragistics.Win.UIElementBorderStyle.Dashed;
		this.cboCUnit.DisplayMember = "";
		ultraGridLayout1.AutoFitColumns = true;
		valueList1.Key = "cString";
		ultraGridLayout1.ValueLists.Add(valueList1);
		this.cboCUnit.Layouts.Add(ultraGridLayout1);
		this.cboCUnit.Location = new System.Drawing.Point(113, 17);
		this.cboCUnit.Name = "cboCUnit";
		this.cboCUnit.Size = new System.Drawing.Size(84, 21);
		this.cboCUnit.TabIndex = 16;
		this.cboCUnit.ValueMember = "";
		this.panel2.Controls.Add(this.groupBox2);
		this.panel2.Controls.Add(this.groupBox1);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel2.Location = new System.Drawing.Point(0, 128);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(747, 104);
		this.panel2.TabIndex = 4;
		this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(panel2_Paint);
		this.groupBox2.Controls.Add(this.txtCost);
		this.groupBox2.Controls.Add(this.ultraLabel6);
		this.groupBox2.Controls.Add(this.txtQty);
		this.groupBox2.Controls.Add(this.ultraLabel5);
		this.groupBox2.Location = new System.Drawing.Point(480, 8);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(260, 84);
		this.groupBox2.TabIndex = 5;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "數量與取位原則";
		this.txtCost.Location = new System.Drawing.Point(91, 50);
		this.txtCost.Name = "txtCost";
		this.txtCost.Size = new System.Drawing.Size(157, 25);
		this.txtCost.TabIndex = 22;
		this.txtCost.Text = "[txtCost]";
		appearance7.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel6.Appearance = appearance7;
		this.ultraLabel6.Location = new System.Drawing.Point(12, 55);
		this.ultraLabel6.Name = "ultraLabel6";
		this.ultraLabel6.Size = new System.Drawing.Size(44, 20);
		this.ultraLabel6.TabIndex = 21;
		this.ultraLabel6.Text = "單價:";
		this.txtQty.Location = new System.Drawing.Point(91, 20);
		this.txtQty.Name = "txtQty";
		this.txtQty.Size = new System.Drawing.Size(157, 25);
		this.txtQty.TabIndex = 20;
		this.txtQty.Text = "[txtQty]";
		appearance8.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel5.Appearance = appearance8;
		this.ultraLabel5.Location = new System.Drawing.Point(12, 25);
		this.ultraLabel5.Name = "ultraLabel5";
		this.ultraLabel5.Size = new System.Drawing.Size(44, 20);
		this.ultraLabel5.TabIndex = 19;
		this.ultraLabel5.Text = "數量:";
		this.groupBox1.Controls.Add(this.optItemType);
		this.groupBox1.Location = new System.Drawing.Point(7, 8);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(465, 84);
		this.groupBox1.TabIndex = 4;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "項目種類";
		appearance9.BackColorDisabled = System.Drawing.Color.FromArgb(237, 243, 254);
		this.optItemType.Appearance = appearance9;
		this.optItemType.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
		this.optItemType.CheckedIndex = 1;
		this.optItemType.Dock = System.Windows.Forms.DockStyle.Fill;
		appearance10.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance10.BackColorDisabled = System.Drawing.Color.FromArgb(237, 243, 254);
		this.optItemType.ItemAppearance = appearance10;
		this.optItemType.ItemOrigin = new System.Drawing.Point(10, 0);
		valueListItem1.DataValue = "B";
		valueListItem1.DisplayText = "一般主項(由下層自動累算)";
		valueListItem2.DataValue = "L";
		valueListItem2.DisplayText = "單獨計價項目(直接輸入金額)";
		this.optItemType.Items.Add(valueListItem1);
		this.optItemType.Items.Add(valueListItem2);
		this.optItemType.ItemSpacingVertical = 5;
		this.optItemType.Location = new System.Drawing.Point(3, 21);
		this.optItemType.Name = "optItemType";
		this.optItemType.Size = new System.Drawing.Size(459, 60);
		this.optItemType.TabIndex = 0;
		this.optItemType.Text = "單獨計價項目(直接輸入金額)";
		this.panel16.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panel16.Controls.Add(this.groupBox6);
		this.panel16.Controls.Add(this.D_Btn_Cncl);
		this.panel16.Controls.Add(this.D_Btn_Next);
		this.panel16.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel16.Location = new System.Drawing.Point(0, 225);
		this.panel16.Name = "panel16";
		this.panel16.Size = new System.Drawing.Size(747, 44);
		this.panel16.TabIndex = 22;
		this.groupBox6.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox6.Location = new System.Drawing.Point(0, 0);
		this.groupBox6.Name = "groupBox6";
		this.groupBox6.Size = new System.Drawing.Size(747, 8);
		this.groupBox6.TabIndex = 4;
		this.groupBox6.TabStop = false;
		this.D_Btn_Cncl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance11.Image = resources.GetObject("appearance11.Image");
		appearance11.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Cncl.Appearance = appearance11;
		this.D_Btn_Cncl.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Cncl.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.D_Btn_Cncl.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.D_Btn_Cncl.Font = new System.Drawing.Font("細明體", 11f);
		this.D_Btn_Cncl.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Cncl.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Cncl.Location = new System.Drawing.Point(651, 9);
		this.D_Btn_Cncl.Name = "D_Btn_Cncl";
		this.D_Btn_Cncl.ShowFocusRect = false;
		this.D_Btn_Cncl.Size = new System.Drawing.Size(88, 31);
		this.D_Btn_Cncl.SupportThemes = false;
		this.D_Btn_Cncl.TabIndex = 2;
		this.D_Btn_Cncl.Text = "取消";
		this.D_Btn_Next.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance12.Image = resources.GetObject("appearance12.Image");
		appearance12.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.D_Btn_Next.Appearance = appearance12;
		this.D_Btn_Next.BackColor = System.Drawing.SystemColors.Control;
		this.D_Btn_Next.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.D_Btn_Next.Font = new System.Drawing.Font("細明體", 11f);
		this.D_Btn_Next.ImageSize = new System.Drawing.Size(20, 20);
		this.D_Btn_Next.ImageTransparentColor = System.Drawing.Color.White;
		this.D_Btn_Next.Location = new System.Drawing.Point(555, 9);
		this.D_Btn_Next.Name = "D_Btn_Next";
		this.D_Btn_Next.ShowFocusRect = false;
		this.D_Btn_Next.Size = new System.Drawing.Size(88, 31);
		this.D_Btn_Next.SupportThemes = false;
		this.D_Btn_Next.TabIndex = 1;
		this.D_Btn_Next.Text = "確定";
		this.D_Btn_Next.Click += new System.EventHandler(D_Btn_Next_Click);
		base.AcceptButton = this.D_Btn_Next;
		this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.CancelButton = this.D_Btn_Cncl;
		base.ClientSize = new System.Drawing.Size(747, 269);
		base.Controls.Add(this.panel16);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel1);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.Name = "FormBudgetEditItem";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "契約變更項目編輯";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormBudgetEditItem_KeyDown);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormBudgetEditItem_FormClosing);
		base.Load += new System.EventHandler(FormBudgetEditItem_Load);
		this.panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtEName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboEUnit).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCUnit).EndInit();
		this.panel2.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.optItemType).EndInit();
		this.panel16.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	private void panel2_Paint(object sender, PaintEventArgs e)
	{
	}

	private void FormBudgetEditItem_Load(object sender, EventArgs e)
	{
		LoadUasualString();
		GetUnit_DataSet();
		txtItemNo.Text = F_DR_forUpd["itemNo"].ToString().Trim();
		txtCName.Text = F_DR_forUpd["cName"].ToString().Trim();
		txtEName.Text = F_DR_forUpd["eName"].ToString().Trim();
		txtMemo.Text = F_DR_forUpd["memo"].ToString().Trim();
		txtCost.Text = F_DR_forUpd["chgCost"].ToString();
		txtQty.Text = F_DR_forUpd["chgQty"].ToString();
		cboCUnit.Text = F_DR_forUpd["unitName"].ToString().Trim();
		cboEUnit.Text = F_DR_forUpd["EUnit"].ToString().Trim();
		optItemType.CheckedIndex = ((!(F_DR_forUpd["kind"].ToString().Trim() == "B")) ? 1 : 0);
		if (F_DR_forUpd["kind"].ToString() == "B" && F_ChildCount > 0)
		{
			optItemType.Enabled = false;
		}
		LoadingScreen();
	}

	private void LoadingScreen()
	{
		string Status = CommonMethods.GetIniValue("Change_EditItem", "WindowState");
		if (Status == "Maximized")
		{
			base.WindowState = FormWindowState.Maximized;
		}
		int iLoc_X = PubTools.Str2Int(CommonMethods.GetIniValue("Change_EditItem", "PK_LocationX"));
		int iLoc_Y = PubTools.Str2Int(CommonMethods.GetIniValue("Change_EditItem", "PK_LocationY"));
		int iSiz_W = PubTools.Str2Int(CommonMethods.GetIniValue("Change_EditItem", "PK_Width"));
		int iSiz_H = PubTools.Str2Int(CommonMethods.GetIniValue("Change_EditItem", "PK_Height"));
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

	private void D_Btn_Next_Click(object sender, EventArgs e)
	{
		try
		{
			Convert.ToDouble(txtQty.Text);
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudgetEditItem.cs" + ex.Message);
			MessageBox.Show(this, "數量的值有問題，請確認!", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtQty.Focus();
			return;
		}
		try
		{
			Convert.ToDouble(txtCost.Text);
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Budget.FormBudgetEditItem.cs" + ex.Message);
			MessageBox.Show(this, "單價的值有問題，請確認!", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtCost.Focus();
			return;
		}
		ArrayList tmp_AL = new ArrayList();
		tmp_AL.Add(F_UserID);
		tmp_AL.Add("契約變更更新");
		Sub_ChgItemA ChgCom = new Sub_ChgItemA(tmp_AL);
		ChgCom.InseItem(F_ProjectCode, F_SubProjectCode, F_ChgCount.ToString(), F_DR_forUpd["sNo"].ToString(), GetUpdateDataRow());
		AddNewCNameString();
		AddNewENameString();
		base.DialogResult = DialogResult.OK;
	}

	private DataRow GetUpdateDataRow()
	{
		F_DR_forUpd["itemNo"] = txtItemNo.Text.Trim();
		F_DR_forUpd["cName"] = txtCName.Text.Trim();
		F_DR_forUpd["eName"] = txtEName.Text.Trim();
		F_DR_forUpd["memo"] = txtMemo.Text.Trim();
		F_DR_forUpd["unitName"] = cboCUnit.Text.Trim();
		F_DR_forUpd["EUnit"] = cboEUnit.Text.Trim();
		F_DR_forUpd["chgQty"] = txtQty.Text.Trim();
		F_DR_forUpd["chgCost"] = txtCost.Text.Trim();
		F_DR_forUpd["kind"] = ((optItemType.CheckedIndex == 0) ? "B" : "L");
		return F_DR_forUpd;
	}

	private void LoadUasualString()
	{
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = "PccAdmin";
		DataTable DT_cName = DBCLS.GetUserDefine("Select * from UserDefind Where kind='cName' Order By sno Desc ");
		for (int i = 0; i < DT_cName.Rows.Count; i++)
		{
			txtCName.Items.Add(DT_cName.Rows[i]["cString"].ToString(), DT_cName.Rows[i]["cString"].ToString());
		}
		DataTable DT_eName = DBCLS.GetUserDefine("Select * from UserDefind Where kind='eName' Order By sno Desc ");
		for (int i = 0; i < DT_eName.Rows.Count; i++)
		{
			txtEName.Items.Add(DT_eName.Rows[i]["cString"].ToString(), DT_eName.Rows[i]["cString"].ToString());
		}
	}

	private void GetUnit_DataSet()
	{
		DataSet DS1 = new DataSet();
		DBClass DBClass1 = new DBClass();
		DBClass1._FS_UserID = "PccAdmin";
		DT_Temp = DBClass1.GetUserDefine("Select cString as 中文單位 from UserDefind Where kind='cUnit' Order By IsNull(Times,0) Desc");
		DataRow DR = DT_Temp.NewRow();
		DR["中文單位"] = "";
		DT_Temp.Rows.Add(DR);
		DT_Temp.TableName = "cUnit";
		DS1.Tables.Add(DT_Temp.Copy());
		DT_Temp = DBClass1.GetUserDefine("Select cString as Unit from UserDefind Where kind='eUnit' Order By IsNull(Times,0) Desc");
		DR = DT_Temp.NewRow();
		DR["Unit"] = "";
		DT_Temp.Rows.Add(DR);
		DT_Temp.TableName = "eUnit";
		DS1.Tables.Add(DT_Temp.Copy());
		cboCUnit.DataSource = DS1;
		cboCUnit.DataMember = "cUnit";
		cboCUnit.DataBind();
		cboEUnit.DataSource = DS1;
		cboEUnit.DataMember = "eUnit";
		cboEUnit.DataBind();
	}

	private void AddNewCNameString()
	{
		if (txtCName.Text != null && !(txtCName.Text.Trim() == ""))
		{
			DBClass DBCLS = new DBClass();
			DBCLS._FS_UserID = "PccAdmin";
			int iCount = PubTools.Str2Int(DBCLS.GetUserDefine_String("Select Count(*) as iCount From UserDefind Where Kind='cName' And cString ='" + txtCName.Text.Trim() + "' ", "iCount"));
			if (iCount <= 0)
			{
				ArrayList aArr = new ArrayList();
				aArr.Clear();
				aArr.Add(F_UserID);
				aArr.Add("(UserDefind_Show) 新增常用字串資料");
				string sKind = "cName";
				UserDefind UserCom = new UserDefind(aArr);
				UserCom.ps_sNo = (UserCom.GetMaxSno(sKind) + 1).ToString();
				UserCom.ps_Kind = sKind;
				UserCom.ps_cString = txtCName.Text.Trim();
				UserCom.InseItem();
			}
		}
	}

	private void AddNewENameString()
	{
		if (txtEName.Text != null && !(txtEName.Text.Trim() == ""))
		{
			DBClass DBCLS = new DBClass();
			DBCLS._FS_UserID = "PccAdmin";
			int iCount = PubTools.Str2Int(DBCLS.GetUserDefine_String("Select Count(*) as iCount From UserDefind Where Kind='eName' And cString ='" + txtEName.Text.Trim() + "' ", "iCount"));
			if (iCount <= 0)
			{
				ArrayList aArr = new ArrayList();
				aArr.Clear();
				aArr.Add(F_UserID);
				aArr.Add("(UserDefind_Show) 新增常用字串資料");
				string sKind = "eName";
				UserDefind UserCom = new UserDefind(aArr);
				UserCom.ps_sNo = (UserCom.GetMaxSno(sKind) + 1).ToString();
				UserCom.ps_Kind = sKind;
				UserCom.ps_cString = txtEName.Text.Trim();
				UserCom.InseItem();
			}
		}
	}

	private void FormBudgetEditItem_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (base.WindowState != FormWindowState.Maximized)
		{
			CommonMethods.WriteIniValue("Change_EditItem", "PK_LocationX", base.Location.X.ToString());
			CommonMethods.WriteIniValue("Change_EditItem", "PK_LocationY", base.Location.Y.ToString());
			CommonMethods.WriteIniValue("Change_EditItem", "PK_Width", base.Size.Width.ToString());
			CommonMethods.WriteIniValue("Change_EditItem", "PK_Height", base.Size.Height.ToString());
		}
		CommonMethods.WriteIniValue("Change_EditItem", "WindowState", base.WindowState.ToString());
	}

	private void FormBudgetEditItem_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			PccesHelp.HelpPDF("FormBudgetEditItem");
		}
	}
}
