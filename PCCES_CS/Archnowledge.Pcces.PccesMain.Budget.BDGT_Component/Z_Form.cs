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
using Infragistics.Win.UltraWinTabControl;

namespace Archnowledge.Pcces.PccesMain.Budget.BDGT_Component;

public class Z_Form : UserControl
{
	private UltraButton BtnPick;

	private UltraTabControl ultraTabControl1;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl ultraTabPageControl1;

	private C1FlexGrid c1FlexGrid1;

	private Panel panel1;

	private UltraLabel ultraLabel1;

	private UltraButton ultraButton1;

	private Container components = null;

	private int F_Issue;

	private PccesFormAction F_ActionName;

	private string userID;

	private string projectCode = "";

	private string F_ParentPrintNo = "";

	private DataTable F_FTable = new DataTable();

	private DataTable F_DT_Var = new DataTable();

	private string F_ParentsNo = "";

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

	private void InitializeComponent()
	{
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		this.ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.c1FlexGrid1 = new C1.Win.C1FlexGrid.C1FlexGrid();
		this.panel1 = new System.Windows.Forms.Panel();
		this.BtnPick = new Infragistics.Win.Misc.UltraButton();
		this.ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.ultraTabPageControl1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ultraTabControl1).BeginInit();
		this.ultraTabControl1.SuspendLayout();
		base.SuspendLayout();
		this.ultraTabPageControl1.Controls.Add(this.c1FlexGrid1);
		this.ultraTabPageControl1.Controls.Add(this.panel1);
		this.ultraTabPageControl1.Location = new System.Drawing.Point(2, 26);
		this.ultraTabPageControl1.Name = "ultraTabPageControl1";
		this.ultraTabPageControl1.Size = new System.Drawing.Size(680, 145);
		this.c1FlexGrid1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.c1FlexGrid1.ColumnInfo = "4,0,0,0,0,110,Columns:0{Width:45;Name:\"VarSign\";Caption:\"± 號\";DataType:System.String;TextAlign:CenterCenter;TextAlignFixed:GeneralTop;}\t1{Width:135;Name:\"ItemNo\";Caption:\"選項代碼\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t2{Name:\"CName\";Caption:\"選項名稱\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t3{Name:\"PrintNo\";Visible:False;DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t";
		this.c1FlexGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.c1FlexGrid1.ExtendLastCol = true;
		this.c1FlexGrid1.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.c1FlexGrid1.ForeColor = System.Drawing.Color.Black;
		this.c1FlexGrid1.Location = new System.Drawing.Point(0, 8);
		this.c1FlexGrid1.Name = "c1FlexGrid1";
		this.c1FlexGrid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.c1FlexGrid1.Size = new System.Drawing.Size(680, 137);
		this.c1FlexGrid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection("Normal{Font:細明體, 11pt;BackColor:237, 243, 254;ForeColor:Black;Border:Flat,1,Silver,Both;}\tAlternate{BackColor:White;}\tFixed{BackColor:225, 247, 223;TextAlign:GeneralTop;Border:Raised,1,Black,Both;}\tHighlight{BackColor:102, 153, 255;}\tFocus{BackColor:102, 153, 255;}\tSearch{BackColor:Highlight;ForeColor:HighlightText;}\tFrozen{BackColor:Beige;}\tEmptyArea{BackColor:AppWorkspace;Border:Flat,1,ControlDarkDark,Both;}\tGrandTotal{BackColor:Black;ForeColor:White;}\tSubtotal0{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal1{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal2{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal3{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal4{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal5{BackColor:ControlDarkDark;ForeColor:White;}\t");
		this.c1FlexGrid1.TabIndex = 0;
		this.c1FlexGrid1.KeyDown += new System.Windows.Forms.KeyEventHandler(c1FlexGrid1_KeyDown);
		this.panel1.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(680, 8);
		this.panel1.TabIndex = 1;
		appearance1.BackColor = System.Drawing.Color.Silver;
		appearance1.BackColor2 = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnPick.Appearance = appearance1;
		this.BtnPick.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		appearance2.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance2.BackColor2 = System.Drawing.Color.White;
		appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.BtnPick.HotTrackAppearance = appearance2;
		this.BtnPick.HotTracking = true;
		this.BtnPick.Location = new System.Drawing.Point(11, 202);
		this.BtnPick.Name = "BtnPick";
		this.BtnPick.ShowFocusRect = false;
		this.BtnPick.ShowOutline = false;
		this.BtnPick.Size = new System.Drawing.Size(128, 28);
		this.BtnPick.SupportThemes = false;
		this.BtnPick.TabIndex = 10;
		this.BtnPick.Text = "加總項目挑選";
		this.BtnPick.Click += new System.EventHandler(BtnPick_Click);
		appearance3.BackColor = System.Drawing.Color.FromArgb(153, 204, 255);
		appearance3.BackColor2 = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance3.ForeColor = System.Drawing.Color.Black;
		appearance3.TextVAlign = Infragistics.Win.VAlign.Top;
		this.ultraTabControl1.ActiveTabAppearance = appearance3;
		this.ultraTabControl1.Controls.Add(this.ultraTabSharedControlsPage1);
		this.ultraTabControl1.Controls.Add(this.ultraTabPageControl1);
		this.ultraTabControl1.FlatMode = true;
		this.ultraTabControl1.Location = new System.Drawing.Point(10, 30);
		this.ultraTabControl1.Name = "ultraTabControl1";
		this.ultraTabControl1.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.ultraTabControl1.Size = new System.Drawing.Size(684, 173);
		this.ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
		this.ultraTabControl1.TabIndex = 9;
		this.ultraTabControl1.TabPadding = new System.Drawing.Size(1, 3);
		appearance4.BackColor = System.Drawing.Color.FromArgb(153, 204, 255);
		appearance4.BackColor2 = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance4.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		appearance4.BorderColor3DBase = System.Drawing.Color.FromArgb(96, 145, 234);
		ultraTab1.ActiveAppearance = appearance4;
		appearance5.BorderColor = System.Drawing.Color.Transparent;
		ultraTab1.Appearance = appearance5;
		ultraTab1.FixedWidth = 120;
		ultraTab1.TabPage = this.ultraTabPageControl1;
		ultraTab1.Text = "加總項目";
		this.ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[1] { ultraTab1 });
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(680, 145);
		appearance6.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel1.Appearance = appearance6;
		this.ultraLabel1.Location = new System.Drawing.Point(3, 3);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(196, 23);
		this.ultraLabel1.TabIndex = 8;
		this.ultraLabel1.Text = "單價 =\u3000加總項目總金額";
		appearance7.BackColor = System.Drawing.Color.Silver;
		appearance7.BackColor2 = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		appearance7.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton1.Appearance = appearance7;
		this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		appearance8.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance8.BackColor2 = System.Drawing.Color.White;
		appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.ultraButton1.HotTrackAppearance = appearance8;
		this.ultraButton1.HotTracking = true;
		this.ultraButton1.Location = new System.Drawing.Point(141, 202);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.ShowFocusRect = false;
		this.ultraButton1.ShowOutline = false;
		this.ultraButton1.Size = new System.Drawing.Size(128, 28);
		this.ultraButton1.SupportThemes = false;
		this.ultraButton1.TabIndex = 11;
		this.ultraButton1.Text = "刪除選擇項目";
		this.ultraButton1.Visible = false;
		this.ultraButton1.Click += new System.EventHandler(ultraButton1_Click);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.Controls.Add(this.ultraButton1);
		base.Controls.Add(this.BtnPick);
		base.Controls.Add(this.ultraTabControl1);
		base.Controls.Add(this.ultraLabel1);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.Name = "Z_Form";
		base.Size = new System.Drawing.Size(700, 230);
		base.Load += new System.EventHandler(Z_Form_Load);
		this.ultraTabPageControl1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ultraTabControl1).EndInit();
		this.ultraTabControl1.ResumeLayout(false);
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

	public Z_Form()
	{
		InitializeComponent();
	}

	private void Z_Form_Load(object sender, EventArgs e)
	{
		projectCode = (base.ParentForm as FormBudgetEditMain).ProjectCode;
		F_ParentPrintNo = (base.ParentForm as FormBudgetEditMain).PrintNo;
		F_ParentsNo = (base.ParentForm as FormBudgetEditMain).Item_sNo.ToString();
		if (F_ActionName == PccesFormAction.BUD)
		{
			c1FlexGrid1.Cols["VarSign"].Visible = true;
		}
		LoadingData();
		BindDataToGrid();
		if (F_ParentPrintNo == "99999999999999999999999999999999")
		{
			DelAmountItemB();
			BtnPick.Enabled = false;
		}
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
		aArr.Add("載入計項(Z)資料--" + projectCode + "(" + IPStr + ")");
		ItemB dbItemB = new ItemB(aArr);
		dbItemB.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		dbItemB.ps_parentCode = F_ParentPrintNo;
		dbItemB.ps_parentCodeSno = F_ParentsNo;
		dbItemB.ps_Issue = F_Issue.ToString();
		F_FTable = dbItemB.ListItem("", projectCode, F_ParentsNo);
		PCals PCLS = new PCals(aArr);
		PCLS.ps_projectCode = projectCode;
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
			c1FlexGrid1[i + 1, "CName"] = F_FTable.Rows[i]["CName"];
			c1FlexGrid1[i + 1, "PrintNo"] = F_FTable.Rows[i]["PrintNo"].ToString().Trim();
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
		FM_FIT_PK._CallerType = "Z";
		FM_FIT_PK._Issue = F_Issue;
		FM_FIT_PK.ShowDialog(this);
		FM_FIT_PK.Dispose();
		FM_FIT_PK = null;
		LoadingData();
		BindDataToGrid();
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
			aArr.Add("刪除計項--" + projectCode + "(" + IPStr + ")");
			ItemB dbItemB = new ItemB(aArr);
			dbItemB.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
			dbItemB.ps_projectCode = projectCode;
			dbItemB.ps_parentCode = F_ParentPrintNo;
			dbItemB.ps_Issue = F_Issue.ToString();
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

	private void c1FlexGrid1_KeyDown(object sender, KeyEventArgs e)
	{
		if ((!e.Control || e.KeyCode != Keys.Delete) && e.KeyCode != Keys.Delete)
		{
		}
	}

	private void DelAmountItemB()
	{
		string srckind = CommonMethods.GetActionNameString(F_ActionName);
		string sSQL = "Delete " + srckind + "ItemB where projectCode = '" + projectCode + "' and parentCode='99999999999999999999999999999999'";
		ArrayList aArr = new ArrayList();
		aArr.Add(userID);
		aArr.Add("取pccescode的值");
		ModifyDB ModDB = new ModifyDB(projectCode, aArr);
		DataTable DT = new DataTable();
		ModDB.DBDele(sSQL);
		ModDB = null;
		aArr = null;
	}
}
