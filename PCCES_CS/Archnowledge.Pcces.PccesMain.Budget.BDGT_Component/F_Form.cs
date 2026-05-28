using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.STDClass;
using C1.Win.C1FlexGrid;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTabControl;

namespace Archnowledge.Pcces.PccesMain.Budget.BDGT_Component;

public class F_Form : UserControl
{
	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	private UltraTabControl ultraTabControl1;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl ultraTabPageControl1;

	private C1FlexGrid c1FlexGrid1;

	private Panel panel1;

	private UltraButton BtnPick;

	private UltraButton ultraButton1;

	private UltraTextEditor txtRate;

	private Container components = null;

	private UltraTextEditor txtEvenDiff;

	private UltraLabel lblEvenDiff;

	private int F_Issue;

	private PccesFormAction F_ActionName;

	private string userID;

	private string projectCode = "";

	private string F_ParentPrintNo = "";

	private string F_ParentSno = "";

	private DataTable F_FTable = new DataTable();

	private DataTable F_DT_Var = new DataTable();

	private decimal F_VDF1 = 0m;

	private bool showVDF1 = false;

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

	public string _UserID
	{
		get
		{
			return userID;
		}
		set
		{
			userID = value;
		}
	}

	public string _txtRate => txtRate.Text.Trim();

	public int _Issue
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

	public decimal _VDF1
	{
		get
		{
			return F_VDF1;
		}
		set
		{
			F_VDF1 = value;
			txtEvenDiff.Text = F_VDF1.ToString();
		}
	}

	private void InitializeComponent()
	{
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.BDGT_Component.F_Form));
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
		this.ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.c1FlexGrid1 = new C1.Win.C1FlexGrid.C1FlexGrid();
		this.panel1 = new System.Windows.Forms.Panel();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.BtnPick = new Infragistics.Win.Misc.UltraButton();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.txtRate = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.txtEvenDiff = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.lblEvenDiff = new Infragistics.Win.Misc.UltraLabel();
		this.ultraTabPageControl1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ultraTabControl1).BeginInit();
		this.ultraTabControl1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtRate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEvenDiff).BeginInit();
		base.SuspendLayout();
		this.ultraTabPageControl1.Controls.Add(this.c1FlexGrid1);
		this.ultraTabPageControl1.Controls.Add(this.panel1);
		this.ultraTabPageControl1.Location = new System.Drawing.Point(2, 26);
		this.ultraTabPageControl1.Name = "ultraTabPageControl1";
		this.ultraTabPageControl1.Size = new System.Drawing.Size(680, 145);
		this.c1FlexGrid1.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
		this.c1FlexGrid1.AllowEditing = false;
		this.c1FlexGrid1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.c1FlexGrid1.ColumnInfo = resources.GetString("c1FlexGrid1.ColumnInfo");
		this.c1FlexGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.c1FlexGrid1.ExtendLastCol = true;
		this.c1FlexGrid1.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.c1FlexGrid1.ForeColor = System.Drawing.Color.Black;
		this.c1FlexGrid1.Location = new System.Drawing.Point(0, 8);
		this.c1FlexGrid1.Name = "c1FlexGrid1";
		this.c1FlexGrid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.c1FlexGrid1.Size = new System.Drawing.Size(680, 137);
		this.c1FlexGrid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("c1FlexGrid1.Styles"));
		this.c1FlexGrid1.TabIndex = 0;
		this.c1FlexGrid1.KeyDown += new System.Windows.Forms.KeyEventHandler(c1FlexGrid1_KeyDown);
		this.panel1.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(680, 8);
		this.panel1.TabIndex = 1;
		appearance1.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel1.Appearance = appearance1;
		this.ultraLabel1.Location = new System.Drawing.Point(3, 3);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(196, 23);
		this.ultraLabel1.TabIndex = 3;
		this.ultraLabel1.Text = "單價 =\u3000加總項目總金額 x ";
		appearance2.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel2.Appearance = appearance2;
		this.ultraLabel2.Location = new System.Drawing.Point(344, 4);
		this.ultraLabel2.Name = "ultraLabel2";
		this.ultraLabel2.Size = new System.Drawing.Size(32, 23);
		this.ultraLabel2.TabIndex = 5;
		this.ultraLabel2.Text = "%";
		appearance3.BackColor = System.Drawing.Color.FromArgb(153, 204, 255);
		appearance3.BackColor2 = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance3.ForeColor = System.Drawing.Color.Black;
		appearance3.TextVAlign = Infragistics.Win.VAlign.Top;
		this.ultraTabControl1.ActiveTabAppearance = appearance3;
		appearance4.TextVAlign = Infragistics.Win.VAlign.Top;
		this.ultraTabControl1.Appearance = appearance4;
		this.ultraTabControl1.Controls.Add(this.ultraTabSharedControlsPage1);
		this.ultraTabControl1.Controls.Add(this.ultraTabPageControl1);
		this.ultraTabControl1.FlatMode = true;
		this.ultraTabControl1.Location = new System.Drawing.Point(10, 30);
		this.ultraTabControl1.Name = "ultraTabControl1";
		this.ultraTabControl1.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.ultraTabControl1.Size = new System.Drawing.Size(684, 173);
		this.ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
		this.ultraTabControl1.TabIndex = 6;
		this.ultraTabControl1.TabPadding = new System.Drawing.Size(1, 3);
		appearance5.BackColor = System.Drawing.Color.FromArgb(153, 204, 255);
		appearance5.BackColor2 = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance5.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		appearance5.BorderColor3DBase = System.Drawing.Color.FromArgb(96, 145, 234);
		ultraTab1.ActiveAppearance = appearance5;
		appearance6.BorderColor = System.Drawing.Color.Transparent;
		ultraTab1.Appearance = appearance6;
		ultraTab1.FixedWidth = 120;
		ultraTab1.TabPage = this.ultraTabPageControl1;
		ultraTab1.Text = "加總項目";
		this.ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[1] { ultraTab1 });
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(680, 145);
		appearance7.BackColor = System.Drawing.Color.Silver;
		appearance7.BackColor2 = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		appearance7.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnPick.Appearance = appearance7;
		this.BtnPick.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		appearance8.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance8.BackColor2 = System.Drawing.Color.White;
		appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.BtnPick.HotTrackAppearance = appearance8;
		this.BtnPick.HotTracking = true;
		this.BtnPick.Location = new System.Drawing.Point(11, 202);
		this.BtnPick.Name = "BtnPick";
		this.BtnPick.ShowFocusRect = false;
		this.BtnPick.ShowOutline = false;
		this.BtnPick.Size = new System.Drawing.Size(128, 28);
		this.BtnPick.SupportThemes = false;
		this.BtnPick.TabIndex = 7;
		this.BtnPick.Text = "加總項目挑選";
		this.BtnPick.Click += new System.EventHandler(BtnPick_Click);
		appearance9.BackColor = System.Drawing.Color.Silver;
		appearance9.BackColor2 = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		appearance9.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton1.Appearance = appearance9;
		this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		appearance10.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance10.BackColor2 = System.Drawing.Color.White;
		appearance10.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.ultraButton1.HotTrackAppearance = appearance10;
		this.ultraButton1.HotTracking = true;
		this.ultraButton1.Location = new System.Drawing.Point(141, 202);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.ShowFocusRect = false;
		this.ultraButton1.ShowOutline = false;
		this.ultraButton1.Size = new System.Drawing.Size(128, 28);
		this.ultraButton1.SupportThemes = false;
		this.ultraButton1.TabIndex = 8;
		this.ultraButton1.Text = "刪除選擇項目";
		this.ultraButton1.Visible = false;
		this.ultraButton1.Click += new System.EventHandler(ultraButton1_Click);
		this.txtRate.AutoSize = true;
		this.txtRate.Location = new System.Drawing.Point(204, 3);
		this.txtRate.Name = "txtRate";
		this.txtRate.Size = new System.Drawing.Size(132, 21);
		this.txtRate.TabIndex = 9;
		this.txtRate.Text = "0";
		this.txtRate.Validating += new System.ComponentModel.CancelEventHandler(txtRate_Validating);
		appearance11.BackColorDisabled = System.Drawing.Color.White;
		appearance11.BackColorDisabled2 = System.Drawing.Color.White;
		appearance11.ForeColorDisabled = System.Drawing.Color.Black;
		this.txtEvenDiff.Appearance = appearance11;
		this.txtEvenDiff.AutoSize = true;
		this.txtEvenDiff.Enabled = false;
		this.txtEvenDiff.Location = new System.Drawing.Point(559, 4);
		this.txtEvenDiff.Name = "txtEvenDiff";
		this.txtEvenDiff.Size = new System.Drawing.Size(132, 21);
		this.txtEvenDiff.TabIndex = 11;
		this.txtEvenDiff.Text = "0";
		appearance12.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.lblEvenDiff.Appearance = appearance12;
		this.lblEvenDiff.Location = new System.Drawing.Point(421, 5);
		this.lblEvenDiff.Name = "lblEvenDiff";
		this.lblEvenDiff.Size = new System.Drawing.Size(132, 23);
		this.lblEvenDiff.TabIndex = 10;
		this.lblEvenDiff.Text = "攤提項差額(VDF1)";
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.Controls.Add(this.lblEvenDiff);
		base.Controls.Add(this.txtEvenDiff);
		base.Controls.Add(this.txtRate);
		base.Controls.Add(this.ultraButton1);
		base.Controls.Add(this.BtnPick);
		base.Controls.Add(this.ultraTabControl1);
		base.Controls.Add(this.ultraLabel2);
		base.Controls.Add(this.ultraLabel1);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.Name = "F_Form";
		base.Size = new System.Drawing.Size(700, 230);
		base.Load += new System.EventHandler(F_Form_Load);
		this.ultraTabPageControl1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ultraTabControl1).EndInit();
		this.ultraTabControl1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtRate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEvenDiff).EndInit();
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

	public void updateVisibleVDF1(bool toShow)
	{
		showVDF1 = toShow;
		showVDF1 = false;
		if (showVDF1)
		{
			lblEvenDiff.Visible = true;
			txtEvenDiff.Visible = true;
		}
		else
		{
			lblEvenDiff.Visible = false;
			txtEvenDiff.Visible = false;
		}
	}

	public F_Form()
	{
		InitializeComponent();
	}

	private void F_Form_Load(object sender, EventArgs e)
	{
		txtRate.Text = (base.ParentForm as FormBudgetEditMain).ItemRate.ToString();
		projectCode = (base.ParentForm as FormBudgetEditMain).ProjectCode;
		F_ParentPrintNo = (base.ParentForm as FormBudgetEditMain).PrintNo;
		F_ParentSno = (base.ParentForm as FormBudgetEditMain).Item_sNo.ToString();
		if (F_ActionName == PccesFormAction.BUD)
		{
			c1FlexGrid1.Cols["VarSign"].Visible = true;
		}
		LoadingData();
		BindDataToGrid();
	}

	private int SelectedItems()
	{
		int RetV = 0;
		for (int i = 1; i < c1FlexGrid1.Rows.Count; i++)
		{
			if (c1FlexGrid1.Rows[i].Selected)
			{
				RetV++;
			}
		}
		return RetV;
	}

	private void LoadingData()
	{
		string IPStr = CommonMethods.GetIPAddress();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(userID);
		aArr.Add("載入加總項目資料-" + IPStr);
		ItemB dbItemB = new ItemB(aArr);
		dbItemB.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		dbItemB.ps_parentCode = F_ParentPrintNo;
		dbItemB.ps_Issue = F_Issue.ToString();
		F_FTable = dbItemB.ListItem("", projectCode, F_ParentSno);
		PCals PCLS = new PCals(aArr);
		PCLS.ps_projectCode = projectCode;
		PCLS.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		F_DT_Var = PCLS.GetCustomVarList();
	}

	private void BindDataToGrid()
	{
		CellStyle CS_Cust = c1FlexGrid1.Styles.Add("CustColor");
		CS_Cust.Font = new Font("細明體", 11f, FontStyle.Bold);
		CS_Cust.ForeColor = Color.FromArgb(0, 51, 0);
		CS_Cust.BackColor = Color.FromArgb(255, 204, 153);
		if (!c1FlexGrid1.Cols.Contains("parentCodeSno"))
		{
			Column C_PSno = c1FlexGrid1.Cols.Add();
			C_PSno.Name = "parentCodeSno";
			C_PSno.Visible = false;
		}
		if (!c1FlexGrid1.Cols.Contains("itemCodeSno"))
		{
			Column C_ISno = c1FlexGrid1.Cols.Add();
			C_ISno.Name = "itemCodeSno";
			C_ISno.Visible = false;
		}
		c1FlexGrid1.Rows.Count = F_FTable.Rows.Count + 1;
		for (int i = 0; i < F_FTable.Rows.Count; i++)
		{
			string sPrintNo = F_FTable.Rows[i]["PrintNo"].ToString().Trim();
			bool IsVAR = sPrintNo.Substring(0, 3).ToUpper() == "VAR";
			c1FlexGrid1[i + 1, "ItemNo"] = F_FTable.Rows[i]["ItemNo"];
			c1FlexGrid1[i + 1, "CName"] = ((!IsVAR) ? F_FTable.Rows[i]["CName"] : GetVarAliasNameByPrintNo(sPrintNo));
			c1FlexGrid1[i + 1, "PrintNo"] = sPrintNo;
			c1FlexGrid1[i + 1, "VarSign"] = ((F_FTable.Rows[i]["VarSign"].ToString() == "-1") ? "－" : "＋");
			c1FlexGrid1[i + 1, "parentCodeSno"] = F_FTable.Rows[i]["parentCodeSno"];
			c1FlexGrid1[i + 1, "itemCodeSno"] = F_FTable.Rows[i]["itemCodeSno"];
			if (IsVAR)
			{
				c1FlexGrid1.Rows[i + 1].Style = CS_Cust;
			}
		}
	}

	private string GetVarAliasNameByPrintNo(string sPrintNo)
	{
		string RetV = "";
		for (int i = 0; i < F_DT_Var.Rows.Count; i++)
		{
			if (sPrintNo.Trim() == F_DT_Var.Rows[i]["VarName"].ToString().Trim())
			{
				RetV = F_DT_Var.Rows[i]["VarAlias"].ToString();
				break;
			}
		}
		return RetV;
	}

	private ArrayList CheckChosenItemList()
	{
		ArrayList RetV = new ArrayList();
		for (int i = 1; i < c1FlexGrid1.Rows.Count; i++)
		{
			RetV.Add(c1FlexGrid1[i, "PrintNo"].ToString().Trim());
		}
		return RetV;
	}

	private ArrayList CheckChosenItemSign()
	{
		ArrayList RetV = new ArrayList();
		for (int i = 1; i < c1FlexGrid1.Rows.Count; i++)
		{
			string sSign = c1FlexGrid1[i, "VarSign"].ToString().Trim();
			int iSignValue = ((sSign == "＋") ? 1 : (-1));
			RetV.Add(iSignValue);
		}
		return RetV;
	}

	private void ultraButton1_Click(object sender, EventArgs e)
	{
		string sQuestionStr = "確定要刪除選擇的 " + SelectedItems() + " 筆資料 ?";
		if (MessageBox.Show(this, sQuestionStr, CommonMethods.GetFormTypeTitle(FormType.Budget), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			string IPStr = CommonMethods.GetIPAddress();
			ArrayList aArr = new ArrayList();
			aArr.Clear();
			aArr.Add(userID);
			aArr.Add("刪除預算書主項大類公式設定--" + projectCode + "(" + IPStr + ")");
			ItemB dbItemB = new ItemB(aArr);
			dbItemB.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
			dbItemB.ps_projectCode = projectCode;
			dbItemB.ps_parentCode = F_ParentPrintNo;
			for (int i = c1FlexGrid1.Rows.Count - 1; i > 0; i--)
			{
				if (c1FlexGrid1.Rows[i].Selected)
				{
					dbItemB.ps_parentCode = F_ParentPrintNo;
					dbItemB.ps_itemCode = c1FlexGrid1[i, "PrintNo"].ToString().Trim();
					dbItemB.DeleItem();
				}
			}
		}
		LoadingData();
		BindDataToGrid();
	}

	private void BtnPick_Click(object sender, EventArgs e)
	{
		Form_FItem_Pick FM_FIT_PK = new Form_FItem_Pick();
		FM_FIT_PK._ActionName = F_ActionName;
		FM_FIT_PK._UserID = userID;
		FM_FIT_PK.ProjectCode = projectCode;
		FM_FIT_PK.ParentCode = F_ParentPrintNo;
		FM_FIT_PK.ChosenPrintNoList = CheckChosenItemList();
		FM_FIT_PK._ChosenItemSignList = CheckChosenItemSign();
		FM_FIT_PK._ParentSNo = (base.ParentForm as FormBudgetEditMain).Item_sNo.ToString();
		FM_FIT_PK._CallerType = "F";
		FM_FIT_PK._Issue = F_Issue;
		FM_FIT_PK.ShowDialog(this);
		FM_FIT_PK.Dispose();
		FM_FIT_PK = null;
		LoadingData();
		BindDataToGrid();
	}

	private void c1FlexGrid1_KeyDown(object sender, KeyEventArgs e)
	{
		if ((e.Control && e.KeyCode == Keys.Delete) || e.KeyCode == Keys.Delete)
		{
			ultraButton1_Click(this, EventArgs.Empty);
		}
	}

	private void txtRate_Validating(object sender, CancelEventArgs e)
	{
		try
		{
			Convert.ToDouble(txtRate.Text.Trim());
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "Budget.BDGT_Component.F_Form.cs" + ex.Message);
			MessageBox.Show(this, "比率有誤。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtRate.Focus();
		}
		string RtnVal = "";
		RtnVal = PubTools.ARound(txtRate.Text, 2).ToString().Trim();
		txtRate.Text = RtnVal;
	}
}
