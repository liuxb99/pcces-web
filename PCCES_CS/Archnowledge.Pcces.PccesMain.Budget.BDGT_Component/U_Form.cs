using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.STDClass;
using C1.Win.C1FlexGrid;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTabControl;

namespace Archnowledge.Pcces.PccesMain.Budget.BDGT_Component;

public class U_Form : UserControl
{
	private UltraTabControl ultraTabControl1;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl ultraTabPageControl1;

	private C1FlexGrid c1FlexGrid1;

	private Panel panel1;

	private UltraButton BtnPick;

	private UltraTabPageControl ultraTabPageControl2;

	private Panel panel3;

	private Panel panel2;

	private UltraLabel ultraLabel1;

	private UltraButton ultraButton2;

	private Panel panel4;

	private UltraLabel ultraLabel3;

	private UltraTextEditor txtFormula;

	private UltraButton ultraButton1;

	private GroupBox groupBox1;

	private RichTextBox Description;

	private Container components = null;

	private int F_Issue;

	private PccesFormAction F_ActionName;

	private string userID;

	private string projectCode = "";

	private string F_ParentPrintNo = "";

	private DataTable F_STable1 = new DataTable();

	private DataTable F_STable2 = new DataTable();

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

	public string _txtFormula => txtFormula.Text.Trim();

	private void InitializeComponent()
	{
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
		Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
		this.ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.ultraButton2 = new Infragistics.Win.Misc.UltraButton();
		this.c1FlexGrid1 = new C1.Win.C1FlexGrid.C1FlexGrid();
		this.panel1 = new System.Windows.Forms.Panel();
		this.BtnPick = new Infragistics.Win.Misc.UltraButton();
		this.ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
		this.panel4 = new System.Windows.Forms.Panel();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.Description = new System.Windows.Forms.RichTextBox();
		this.panel3 = new System.Windows.Forms.Panel();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.txtFormula = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
		this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
		this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.ultraTabPageControl1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid1).BeginInit();
		this.ultraTabPageControl2.SuspendLayout();
		this.panel4.SuspendLayout();
		this.groupBox1.SuspendLayout();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtFormula).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ultraTabControl1).BeginInit();
		this.ultraTabControl1.SuspendLayout();
		base.SuspendLayout();
		this.ultraTabPageControl1.Controls.Add(this.ultraButton2);
		this.ultraTabPageControl1.Controls.Add(this.c1FlexGrid1);
		this.ultraTabPageControl1.Controls.Add(this.panel1);
		this.ultraTabPageControl1.Controls.Add(this.BtnPick);
		this.ultraTabPageControl1.Location = new System.Drawing.Point(2, 26);
		this.ultraTabPageControl1.Name = "ultraTabPageControl1";
		this.ultraTabPageControl1.Size = new System.Drawing.Size(680, 178);
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton2.Appearance = appearance1;
		this.ultraButton2.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		appearance2.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance2.BackColor2 = System.Drawing.Color.White;
		appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.ultraButton2.HotTrackAppearance = appearance2;
		this.ultraButton2.HotTracking = true;
		this.ultraButton2.Location = new System.Drawing.Point(129, 146);
		this.ultraButton2.Name = "ultraButton2";
		this.ultraButton2.ShowFocusRect = false;
		this.ultraButton2.ShowOutline = false;
		this.ultraButton2.Size = new System.Drawing.Size(128, 28);
		this.ultraButton2.SupportThemes = false;
		this.ultraButton2.TabIndex = 13;
		this.ultraButton2.Text = "刪除選擇項目";
		this.ultraButton2.Visible = false;
		this.ultraButton2.Click += new System.EventHandler(ultraButton2_Click);
		this.c1FlexGrid1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.c1FlexGrid1.ColumnInfo = "4,0,0,0,0,110,Columns:0{Width:45;Name:\"VarSign\";Caption:\"± 號\";DataType:System.String;TextAlign:CenterCenter;TextAlignFixed:GeneralTop;}\t1{Width:135;Name:\"ItemNo\";Caption:\"選項代碼\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t2{Name:\"CName\";Caption:\"選項名稱\";DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t3{Name:\"PrintNo\";Caption:\"PrintNo\";Visible:False;DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:GeneralTop;}\t";
		this.c1FlexGrid1.ExtendLastCol = true;
		this.c1FlexGrid1.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.c1FlexGrid1.ForeColor = System.Drawing.Color.Black;
		this.c1FlexGrid1.Location = new System.Drawing.Point(0, 8);
		this.c1FlexGrid1.Name = "c1FlexGrid1";
		this.c1FlexGrid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.c1FlexGrid1.Size = new System.Drawing.Size(682, 137);
		this.c1FlexGrid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection("Normal{Font:細明體, 11pt;BackColor:237, 243, 254;ForeColor:Black;Border:Flat,1,Silver,Both;}\tAlternate{BackColor:White;}\tFixed{BackColor:225, 247, 223;TextAlign:GeneralTop;Border:Raised,1,Black,Both;}\tHighlight{BackColor:102, 153, 255;}\tFocus{BackColor:102, 153, 255;}\tSearch{BackColor:Highlight;ForeColor:HighlightText;}\tFrozen{BackColor:Beige;}\tEmptyArea{BackColor:AppWorkspace;Border:Flat,1,ControlDarkDark,Both;}\tGrandTotal{BackColor:Black;ForeColor:White;}\tSubtotal0{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal1{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal2{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal3{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal4{BackColor:ControlDarkDark;ForeColor:White;}\tSubtotal5{BackColor:ControlDarkDark;ForeColor:White;}\t");
		this.c1FlexGrid1.TabIndex = 0;
		this.c1FlexGrid1.KeyDown += new System.Windows.Forms.KeyEventHandler(c1FlexGrid1_KeyDown);
		this.panel1.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(680, 8);
		this.panel1.TabIndex = 1;
		appearance3.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnPick.Appearance = appearance3;
		this.BtnPick.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		appearance4.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance4.BackColor2 = System.Drawing.Color.White;
		appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.BtnPick.HotTrackAppearance = appearance4;
		this.BtnPick.HotTracking = true;
		this.BtnPick.Location = new System.Drawing.Point(-1, 146);
		this.BtnPick.Name = "BtnPick";
		this.BtnPick.ShowFocusRect = false;
		this.BtnPick.ShowOutline = false;
		this.BtnPick.Size = new System.Drawing.Size(128, 28);
		this.BtnPick.SupportThemes = false;
		this.BtnPick.TabIndex = 11;
		this.BtnPick.Text = "加總項目挑選";
		this.BtnPick.Click += new System.EventHandler(BtnPick_Click);
		this.ultraTabPageControl2.Controls.Add(this.panel4);
		this.ultraTabPageControl2.Controls.Add(this.panel3);
		this.ultraTabPageControl2.Controls.Add(this.panel2);
		this.ultraTabPageControl2.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabPageControl2.Name = "ultraTabPageControl2";
		this.ultraTabPageControl2.Size = new System.Drawing.Size(680, 178);
		this.panel4.Controls.Add(this.groupBox1);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel4.Location = new System.Drawing.Point(0, 44);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(680, 134);
		this.panel4.TabIndex = 4;
		this.groupBox1.Controls.Add(this.Description);
		this.groupBox1.Location = new System.Drawing.Point(8, 8);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(664, 116);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "說明";
		this.Description.Font = new System.Drawing.Font("細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.Description.Location = new System.Drawing.Point(11, 18);
		this.Description.Name = "Description";
		this.Description.ReadOnly = true;
		this.Description.Size = new System.Drawing.Size(640, 93);
		this.Description.TabIndex = 0;
		this.Description.Text = "[Description]";
		this.panel3.Controls.Add(this.ultraButton1);
		this.panel3.Controls.Add(this.txtFormula);
		this.panel3.Controls.Add(this.ultraLabel3);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel3.Location = new System.Drawing.Point(0, 8);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(680, 36);
		this.panel3.TabIndex = 3;
		this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		appearance5.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance5.BackColor2 = System.Drawing.Color.White;
		appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.ultraButton1.HotTrackAppearance = appearance5;
		this.ultraButton1.Location = new System.Drawing.Point(564, 4);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.ShowFocusRect = false;
		this.ultraButton1.ShowOutline = false;
		this.ultraButton1.Size = new System.Drawing.Size(108, 28);
		this.ultraButton1.SupportThemes = false;
		this.ultraButton1.TabIndex = 2;
		this.ultraButton1.Text = "公式檢查";
		this.ultraButton1.Click += new System.EventHandler(ultraButton1_Click);
		this.txtFormula.Location = new System.Drawing.Point(92, 8);
		this.txtFormula.MaxLength = 200;
		this.txtFormula.Name = "txtFormula";
		this.txtFormula.Size = new System.Drawing.Size(468, 24);
		this.txtFormula.TabIndex = 1;
		this.txtFormula.Text = "[txtFormula]";
		this.txtFormula.Validating += new System.ComponentModel.CancelEventHandler(txtFormula_Validating);
		appearance6.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel3.Appearance = appearance6;
		this.ultraLabel3.Location = new System.Drawing.Point(8, 10);
		this.ultraLabel3.Name = "ultraLabel3";
		this.ultraLabel3.Size = new System.Drawing.Size(88, 23);
		this.ultraLabel3.TabIndex = 0;
		this.ultraLabel3.Text = "自訂公式：";
		this.panel2.BackColor = System.Drawing.Color.FromArgb(0, 102, 153);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(680, 8);
		this.panel2.TabIndex = 2;
		appearance7.BackColor = System.Drawing.Color.FromArgb(153, 204, 255);
		appearance7.BackColor2 = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance7.ForeColor = System.Drawing.Color.Black;
		appearance7.TextVAlign = Infragistics.Win.VAlign.Top;
		this.ultraTabControl1.ActiveTabAppearance = appearance7;
		appearance8.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance8.BackColor2 = System.Drawing.Color.FromArgb(102, 153, 255);
		appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
		appearance8.TextVAlign = Infragistics.Win.VAlign.Top;
		this.ultraTabControl1.Appearance = appearance8;
		appearance9.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		appearance9.BackColor2 = System.Drawing.Color.FromArgb(237, 243, 254);
		this.ultraTabControl1.ClientAreaAppearance = appearance9;
		this.ultraTabControl1.Controls.Add(this.ultraTabSharedControlsPage1);
		this.ultraTabControl1.Controls.Add(this.ultraTabPageControl1);
		this.ultraTabControl1.Controls.Add(this.ultraTabPageControl2);
		this.ultraTabControl1.FlatMode = true;
		this.ultraTabControl1.HotTrack = true;
		this.ultraTabControl1.InterTabSpacing = new Infragistics.Win.DefaultableInteger(0);
		this.ultraTabControl1.Location = new System.Drawing.Point(10, 30);
		this.ultraTabControl1.Name = "ultraTabControl1";
		this.ultraTabControl1.SharedControlsPage = this.ultraTabSharedControlsPage1;
		this.ultraTabControl1.ShowButtonSeparators = true;
		this.ultraTabControl1.Size = new System.Drawing.Size(684, 206);
		this.ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
		this.ultraTabControl1.TabIndex = 13;
		this.ultraTabControl1.TabPadding = new System.Drawing.Size(1, 3);
		appearance10.BackColor = System.Drawing.Color.FromArgb(153, 204, 255);
		appearance10.BackColor2 = System.Drawing.Color.FromArgb(0, 102, 153);
		appearance10.BorderColor = System.Drawing.Color.FromArgb(96, 145, 234);
		appearance10.BorderColor3DBase = System.Drawing.Color.FromArgb(96, 145, 234);
		ultraTab1.ActiveAppearance = appearance10;
		appearance11.BorderColor = System.Drawing.Color.Transparent;
		ultraTab1.Appearance = appearance11;
		ultraTab1.FixedWidth = 120;
		ultraTab1.TabPage = this.ultraTabPageControl1;
		ultraTab1.Text = "加總項目";
		appearance12.TextVAlign = Infragistics.Win.VAlign.Top;
		ultraTab2.Appearance = appearance12;
		ultraTab2.FixedWidth = 120;
		ultraTab2.TabPage = this.ultraTabPageControl2;
		ultraTab2.Text = "自訂公式";
		this.ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[2] { ultraTab1, ultraTab2 });
		this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
		this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
		this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(680, 178);
		appearance13.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraLabel1.Appearance = appearance13;
		this.ultraLabel1.Location = new System.Drawing.Point(3, 3);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(196, 23);
		this.ultraLabel1.TabIndex = 11;
		this.ultraLabel1.Text = "單價 =\u3000依自訂公式計算";
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.Controls.Add(this.ultraTabControl1);
		base.Controls.Add(this.ultraLabel1);
		this.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.Name = "U_Form";
		base.Size = new System.Drawing.Size(700, 230);
		base.Load += new System.EventHandler(U_Form_Load);
		this.ultraTabPageControl1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid1).EndInit();
		this.ultraTabPageControl2.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
		this.groupBox1.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtFormula).EndInit();
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

	public U_Form()
	{
		InitializeComponent();
	}

	private void U_Form_Load(object sender, EventArgs e)
	{
		txtFormula.Text = "";
		txtFormula.Text = (base.ParentForm as FormBudgetEditMain).FormulaStr.ToString();
		projectCode = (base.ParentForm as FormBudgetEditMain).ProjectCode;
		F_ParentPrintNo = (base.ParentForm as FormBudgetEditMain).PrintNo;
		F_ParentsNo = (base.ParentForm as FormBudgetEditMain).Item_sNo.ToString();
		if (F_ActionName == PccesFormAction.BUD)
		{
			c1FlexGrid1.Cols["VarSign"].Visible = true;
		}
		Description.Text = GetDescText();
		LoadingData();
		BindDataToGrid();
	}

	private string GetDescText()
	{
		string RetV = "";
		return "自訂公式範例：\n  Rnd([Amount]*0.01,0)+100 \n\n說明：\n  [Amount]： 挑選的項目加總金額 \n\n函數：\n  Rnd(數值,位數) 四捨五入\u3000\n  Sqrt(數值) 開根號\u3000\n  Trnc(數值) 取整數  \n\n  ^ 次方    \n    Ex: 5 ^ 3     = 125(5的3次方)    \n    Ex: 125^(1/3) =   5(125的3次方根)  \n";
	}

	private int SelectedItems(int Index)
	{
		int RetV = 0;
		if (Index == 1)
		{
			for (int i = 1; i < c1FlexGrid1.Rows.Count; i++)
			{
				if (c1FlexGrid1.Rows[i].Selected)
				{
					RetV++;
				}
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
		aArr.Add("載入加總項目資料--" + projectCode + "(" + IPStr + ")");
		ItemB dbItemB = new ItemB(aArr);
		dbItemB.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		dbItemB.ps_parentCode = F_ParentPrintNo;
		dbItemB.ps_parentCodeSno = F_ParentsNo;
		dbItemB.ps_Issue = F_Issue.ToString();
		F_STable1 = dbItemB.ListItem("", projectCode, F_ParentsNo);
		PCals PCLS = new PCals(aArr);
		PCLS.ps_projectCode = projectCode;
		PCLS.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		F_DT_Var = PCLS.GetCustomVarList();
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
		c1FlexGrid1.Rows.Count = F_STable1.Rows.Count + 1;
		for (int i = 0; i < F_STable1.Rows.Count; i++)
		{
			string sPrintNo = F_STable1.Rows[i]["PrintNo"].ToString().Trim();
			bool IsVAR = sPrintNo.Substring(0, 3).ToUpper() == "VAR";
			c1FlexGrid1[i + 1, "ItemNo"] = F_STable1.Rows[i]["ItemNo"];
			c1FlexGrid1[i + 1, "CName"] = ((!IsVAR) ? F_STable1.Rows[i]["CName"] : GetVarAliasNameByPrintNo(sPrintNo));
			c1FlexGrid1[i + 1, "PrintNo"] = F_STable1.Rows[i]["PrintNo"].ToString().Trim();
			c1FlexGrid1[i + 1, "VarSign"] = ((F_STable1.Rows[i]["VarSign"].ToString() == "-1") ? "－" : "＋");
			c1FlexGrid1[i + 1, "parentCodeSno"] = F_STable1.Rows[i]["parentCodeSno"];
			c1FlexGrid1[i + 1, "itemCodeSno"] = F_STable1.Rows[i]["itemCodeSno"];
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

	private void ultraButton2_Click(object sender, EventArgs e)
	{
		string sQuestionStr = "確定要刪除選擇的 " + SelectedItems(1) + " 筆資料 ?";
		if (MessageBox.Show(this, sQuestionStr, CommonMethods.GetFormTypeTitle(FormType.Budget), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			string IPStr = CommonMethods.GetIPAddress();
			ArrayList aArr = new ArrayList();
			aArr.Clear();
			aArr.Add(userID);
			aArr.Add("刪除加總項目--" + projectCode + "(" + IPStr + ")");
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
					dbItemB.ps_parentCodeSno = F_ParentsNo;
					dbItemB.ps_itemCode = c1FlexGrid1[i, "PrintNo"].ToString().Trim();
					dbItemB.ps_itemCodeSno = c1FlexGrid1[i, "itemCodeSno"].ToString().Trim();
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
		FM_FIT_PK._CallerType = "U";
		FM_FIT_PK._Issue = F_Issue;
		FM_FIT_PK.ShowDialog(this);
		FM_FIT_PK.Dispose();
		FM_FIT_PK = null;
		LoadingData();
		BindDataToGrid();
	}

	private void c1FlexGrid1_KeyDown(object sender, KeyEventArgs e)
	{
		if ((!e.Control || e.KeyCode != Keys.Delete) && e.KeyCode != Keys.Delete)
		{
		}
	}

	private void ultraButton1_Click(object sender, EventArgs e)
	{
		ExecResult ER = PubTools.ArchChkFormula2(txtFormula.Text);
		if (ER.ReturnCode == 0)
		{
			MessageBox.Show(this, "公式設定正確!", CommonMethods.GetFormTypeTitle(FormType.Budget), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		else
		{
			MessageBox.Show(this, "公式設定有誤，請重新設定! Error : " + ER.Message, CommonMethods.GetFormTypeTitle(FormType.Budget), MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		txtFormula.Focus();
	}

	private void txtFormula_Validating(object sender, CancelEventArgs e)
	{
		if (!CommonMethods.CheckValidString((sender as UltraTextEditor).Text))
		{
			e.Cancel = true;
		}
		if (!CommonMethods.IsStrByteLenValid(txtFormula.Text, 200))
		{
			MessageBox.Show(this, "工程代碼的長度不可超過 200 Bytes。", "警示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtFormula.Focus();
		}
	}
}
