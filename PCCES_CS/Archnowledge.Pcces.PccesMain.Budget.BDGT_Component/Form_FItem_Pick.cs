using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.STDClass;
using C1.Win.C1FlexGrid;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace Archnowledge.Pcces.PccesMain.Budget.BDGT_Component;

public class Form_FItem_Pick : Form
{
	private int F_Issue;

	private string F_CallerType = "";

	private string UserID;

	private ArrayList F_AList = new ArrayList();

	private ArrayList F_BList = new ArrayList();

	private string projectCode = "";

	private string F_ParentCode = "";

	private string F_ParentSNo = "";

	private PccesFormAction F_ActionName = PccesFormAction.None;

	private string iLevel = "";

	private string F_iLevel = "";

	private DataTable DT_FPick = new DataTable();

	private IContainer components;

	private Panel panel1;

	private Panel panel2;

	private C1FlexGrid c1FlexGrid1;

	private UltraLabel ultraLabel1;

	private Panel panel3;

	private UltraButton ultraButton1;

	private UltraButton BtnPick;

	private UltraCheckEditor ultraCheckEditor1;

	private System.Windows.Forms.ToolTip toolTip1;

	private ImageList imageList2;

	private LevelSwitchButton levelSwitchButton;

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

	public string _CallerType
	{
		get
		{
			return F_CallerType;
		}
		set
		{
			F_CallerType = value;
		}
	}

	public string _UserID
	{
		get
		{
			return UserID;
		}
		set
		{
			UserID = value;
		}
	}

	public ArrayList ChosenPrintNoList
	{
		get
		{
			return F_AList;
		}
		set
		{
			F_AList = value;
		}
	}

	public ArrayList _ChosenItemSignList
	{
		get
		{
			return F_BList;
		}
		set
		{
			F_BList = value;
		}
	}

	public string ProjectCode
	{
		get
		{
			return projectCode;
		}
		set
		{
			projectCode = value;
		}
	}

	public string ParentCode
	{
		get
		{
			return F_ParentCode;
		}
		set
		{
			F_ParentCode = value;
		}
	}

	public string _ParentSNo
	{
		get
		{
			return F_ParentSNo;
		}
		set
		{
			F_ParentSNo = value;
		}
	}

	public Form_FItem_Pick()
	{
		InitializeComponent();
		CellStyle cs1 = c1FlexGrid1.Styles.Add("EditMode");
		cs1.DataType = typeof(Image);
		cs1.ImageAlign = ImageAlignEnum.RightCenter;
	}

	private void HideCols(bool IsHide)
	{
		if (IsHide)
		{
			c1FlexGrid1.Cols["PrintNo"].Visible = false;
			c1FlexGrid1.Cols["CanCheck"].Visible = false;
			if (F_ActionName == PccesFormAction.BUD)
			{
				c1FlexGrid1.Cols["VarSign"].Visible = true;
			}
		}
	}

	private void Form_FItem_Pick_Load(object sender, EventArgs e)
	{
		HideCols(IsHide: true);
		string IPStr = CommonMethods.GetIPAddress();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(UserID);
		aArr.Add("預算書加總項目挑選" + projectCode + "(" + IPStr + ")");
		ItemA dbItemA = new ItemA(aArr);
		dbItemA.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		dbItemA.ps_projectCode = projectCode;
		dbItemA.ps_Issue = F_Issue.ToString();
		DT_FPick = dbItemA.ListItem(" printNo < '" + F_ParentCode + "' ", projectCode);
		CellRange rg1 = c1FlexGrid1.GetCellRange(0, 0);
		rg1.Style = c1FlexGrid1.Styles["EditMode"];
		rg1.Image = imageList2.Images[1];
		CellRange rg2 = c1FlexGrid1.GetCellRange(0, 1);
		rg2.Style = c1FlexGrid1.Styles["EditMode"];
		rg2.Image = imageList2.Images[1];
		bool IsFound = false;
		PCals PCLS1 = new PCals(aArr);
		PCLS1.ps_projectCode = projectCode;
		PCLS1.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		DataTable DT_List = PCLS1.GetCustomVarList();
		for (int i = 0; i < DT_List.Rows.Count; i++)
		{
			DataTable DT_CustOpList = PCLS1.GetCustomOperationList(DT_List.Rows[i]["VarName"].ToString().Trim());
			for (int k = 0; k < DT_CustOpList.Rows.Count; k++)
			{
				if (DT_CustOpList.Rows[k]["SNo"].ToString() == F_ParentSNo.Trim())
				{
					IsFound = true;
					break;
				}
			}
			if (IsFound)
			{
				IsFound = false;
				continue;
			}
			DataRow DR = DT_FPick.NewRow();
			DR["ItemNo"] = "";
			DR["CName"] = DT_List.Rows[i]["VarAlias"];
			DR["PrintNo"] = DT_List.Rows[i]["VarName"];
			DT_FPick.Rows.Add(DR);
		}
		BindDataToGrid();
	}

	private void BindDataToGrid()
	{
		int iLevel = 0;
		c1FlexGrid1.Rows.Count = DT_FPick.Rows.Count + 1;
		int iidx = -1;
		string sTmpStr = "";
		CellStyle CS2 = c1FlexGrid1.Styles.Add("MainColor");
		CellStyle CSZ = c1FlexGrid1.Styles.Add("ZColor");
		CellStyle CS_Cust = c1FlexGrid1.Styles.Add("CustColor");
		CS2.ForeColor = Color.Blue;
		CSZ.ForeColor = Color.Green;
		CS_Cust.ForeColor = Color.FromArgb(0, 51, 0);
		CS_Cust.BackColor = Color.FromArgb(255, 204, 153);
		F_iLevel = PrintNo_Level(F_ParentCode);
		if (!c1FlexGrid1.Cols.Contains("sNo"))
		{
			Column C_sNo = c1FlexGrid1.Cols.Add();
			C_sNo.Name = "sNo";
			C_sNo.Visible = false;
		}
		for (int i = 0; i < DT_FPick.Rows.Count; i++)
		{
			c1FlexGrid1.Rows[i + 1].IsNode = true;
			sTmpStr = DT_FPick.Rows[i]["PrintNo"].ToString().Trim();
			iidx = ((sTmpStr.Length >= F_ParentCode.Length) ? F_ParentCode.IndexOf(sTmpStr) : F_ParentCode.Substring(0, sTmpStr.Length).IndexOf(sTmpStr));
			c1FlexGrid1[i + 1, "ItemNo"] = DT_FPick.Rows[i]["ItemNo"];
			c1FlexGrid1[i + 1, "CName"] = DT_FPick.Rows[i]["CName"];
			c1FlexGrid1[i + 1, "PrintNo"] = DT_FPick.Rows[i]["PrintNo"].ToString().Trim();
			c1FlexGrid1[i + 1, "VarSign"] = "";
			c1FlexGrid1[i + 1, "sNo"] = DT_FPick.Rows[i]["sNo"];
			if (DT_FPick.Rows[i]["kind"].ToString() == "Z")
			{
				c1FlexGrid1.Rows[i + 1].Style = CSZ;
			}
			else if (DT_FPick.Rows[i]["kind"].ToString() != "W")
			{
				c1FlexGrid1.Rows[i + 1].Style = CS2;
			}
			if (DT_FPick.Rows[i]["PrintNo"].ToString().Trim().Substring(0, 3)
				.ToUpper() == "VAR")
			{
				c1FlexGrid1.Rows[i + 1].Style = CS_Cust;
			}
			if (iidx > -1)
			{
				c1FlexGrid1[i + 1, "CanCheck"] = false;
			}
			else
			{
				c1FlexGrid1[i + 1, "CanCheck"] = true;
			}
			string st1 = DT_FPick.Rows[i]["PrintNo"].ToString().Trim();
			int iFindIndex = F_AList.IndexOf(DT_FPick.Rows[i]["PrintNo"].ToString().Trim());
			if (iFindIndex > -1)
			{
				c1FlexGrid1[i + 1, "IsCheck"] = true;
				c1FlexGrid1[i + 1, "VarSign"] = ((PubTools.Str2Int(F_BList[iFindIndex]) == 1) ? "＋" : "－");
			}
			else
			{
				c1FlexGrid1[i + 1, "IsCheck"] = false;
			}
			c1FlexGrid1.Rows[i + 1].Node.Level = Convert.ToInt32(DT_FPick.Rows[i]["PrintNo"].ToString().Trim().Length / 4);
			if (c1FlexGrid1.Rows[i + 1].Node.Level > iLevel)
			{
				iLevel = c1FlexGrid1.Rows[i + 1].Node.Level;
			}
		}
		levelSwitchButton.MaxLevel = iLevel;
		this.iLevel = iLevel.ToString();
	}

	private void BtnPick_Click(object sender, EventArgs e)
	{
		string IPStr = CommonMethods.GetIPAddress();
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(UserID);
		aArr.Add("預算設詳細表公式項設定--存檔--" + projectCode + "(" + IPStr + ")");
		ItemB dbItemB = new ItemB(aArr);
		dbItemB.ps_srckind = CommonMethods.GetActionNameString(F_ActionName);
		dbItemB.ps_projectCode = projectCode;
		dbItemB.ps_parentCode = F_ParentCode;
		dbItemB.ps_parentCodeSno = F_ParentSNo;
		dbItemB.ps_Issue = F_Issue.ToString();
		dbItemB.DeleItem();
		for (int i = 1; i < c1FlexGrid1.Rows.Count; i++)
		{
			if ((bool)c1FlexGrid1[i, "IsCheck"])
			{
				dbItemB.ps_itemCode = c1FlexGrid1[i, "PrintNo"].ToString().Trim();
				dbItemB.ps_itemCodeSno = c1FlexGrid1[i, "sNo"].ToString().Trim();
				dbItemB.ps_VarSign = ((c1FlexGrid1[i, "VarSign"].ToString().Trim() == "－") ? "-1" : "1");
				dbItemB.InseItem();
			}
		}
	}

	private void c1FlexGrid1_BeforeMouseDown(object sender, BeforeMouseDownEventArgs e)
	{
		if (c1FlexGrid1.MouseRow > 0 && c1FlexGrid1.MouseCol >= 0)
		{
			int rowIndex = c1FlexGrid1.MouseRow;
			c1FlexGrid1.Row = rowIndex;
			if (e.Button == MouseButtons.Left && !(bool)c1FlexGrid1[rowIndex, "IsCheck"] && !(bool)c1FlexGrid1[rowIndex, "CanCheck"])
			{
				e.Cancel = true;
				MessageBox.Show(this, "父項不可勾選", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
	}

	private void ultraCheckEditor1_CheckedChanged(object sender, EventArgs e)
	{
		string slevel = "";
		if (ultraCheckEditor1.Checked && Convert.ToInt32(F_iLevel) < Convert.ToInt32(iLevel))
		{
			iLevel = F_iLevel;
		}
		for (int i = 1; i < c1FlexGrid1.Rows.Count; i++)
		{
			int rowIndex = c1FlexGrid1.MouseRow;
			slevel = PrintNo_Level(c1FlexGrid1[i, "PrintNo"].ToString());
			if (c1FlexGrid1.Rows[i].Visible && (bool)c1FlexGrid1[i, "CanCheck"] && slevel == iLevel)
			{
				if (ultraCheckEditor1.Checked)
				{
					c1FlexGrid1[i, "IsCheck"] = true;
					c1FlexGrid1[rowIndex, "VarSign"] = "＋";
				}
				else
				{
					c1FlexGrid1[i, "IsCheck"] = false;
					c1FlexGrid1[rowIndex, "VarSign"] = "";
				}
			}
			else
			{
				c1FlexGrid1[i, "IsCheck"] = false;
				c1FlexGrid1[rowIndex, "VarSign"] = "";
			}
		}
	}

	private string PrintNo_Level(string printNo)
	{
		int iCount = 0;
		if (printNo != "")
		{
			iCount = printNo.Length;
			return (iCount / 4).ToString();
		}
		return "";
	}

	private void c1FlexGrid1_Click(object sender, EventArgs e)
	{
		if (c1FlexGrid1.MouseRow <= 0 || c1FlexGrid1.MouseCol <= 0)
		{
			return;
		}
		int rowIndex = c1FlexGrid1.MouseRow;
		if ((F_CallerType == "F" || F_CallerType == "Z") && (bool)c1FlexGrid1[rowIndex, "CanCheck"])
		{
			string sPrintNo = c1FlexGrid1[rowIndex, "PrintNo"].ToString().Trim();
			string sShrtPntNo = sPrintNo.Substring(0, sPrintNo.Length - 4);
			for (int i = 1; i < c1FlexGrid1.Rows.Count; i++)
			{
				string myPrintNo = c1FlexGrid1[i, "PrintNo"].ToString().Trim();
				if (myPrintNo.Length < sPrintNo.Length)
				{
					if (myPrintNo == sShrtPntNo)
					{
						c1FlexGrid1[i, "IsCheck"] = false;
					}
				}
				else if (myPrintNo.Length > sPrintNo.Length && myPrintNo.Substring(0, myPrintNo.Length - 4) == sPrintNo)
				{
					c1FlexGrid1[i, "IsCheck"] = false;
				}
			}
		}
		if (c1FlexGrid1.MouseCol == 1 && (bool)c1FlexGrid1[rowIndex, "IsCheck"])
		{
			switch (c1FlexGrid1[rowIndex, "VarSign"].ToString().Trim())
			{
			case "＋":
				c1FlexGrid1[rowIndex, "VarSign"] = "－";
				break;
			case "－":
				c1FlexGrid1[rowIndex, "VarSign"] = "＋";
				break;
			case "":
				c1FlexGrid1[rowIndex, "VarSign"] = "＋";
				break;
			}
		}
	}

	private void c1FlexGrid1_AfterEdit(object sender, RowColEventArgs e)
	{
		if (e.Col != 0 || e.Row <= 0)
		{
			return;
		}
		for (int i = c1FlexGrid1.Selection.r1; i <= c1FlexGrid1.Selection.r2; i++)
		{
			if (!(bool)c1FlexGrid1[i, "IsCheck"])
			{
				c1FlexGrid1[i, "VarSign"] = "";
			}
			else
			{
				c1FlexGrid1[i, "VarSign"] = "＋";
			}
		}
	}

	private void levelSwitchButton_LevelSwitchButtonsClicked()
	{
		c1FlexGrid1.Tree.Show(levelSwitchButton.SelectedLevel);
		iLevel = levelSwitchButton.SelectedLevel.ToString();
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.BDGT_Component.Form_FItem_Pick));
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		this.panel1 = new System.Windows.Forms.Panel();
		this.ultraCheckEditor1 = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
		this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.c1FlexGrid1 = new C1.Win.C1FlexGrid.C1FlexGrid();
		this.panel3 = new System.Windows.Forms.Panel();
		this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
		this.BtnPick = new Infragistics.Win.Misc.UltraButton();
		this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.levelSwitchButton = new Archnowledge.Pcces.PccesMain.ArchControls.LevelSwitchButton();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid1).BeginInit();
		this.panel3.SuspendLayout();
		base.SuspendLayout();
		this.panel1.Controls.Add(this.levelSwitchButton);
		this.panel1.Controls.Add(this.ultraCheckEditor1);
		this.panel1.Controls.Add(this.ultraLabel1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(584, 44);
		this.panel1.TabIndex = 0;
		this.ultraCheckEditor1.Location = new System.Drawing.Point(13, 24);
		this.ultraCheckEditor1.Name = "ultraCheckEditor1";
		this.ultraCheckEditor1.Size = new System.Drawing.Size(16, 20);
		this.ultraCheckEditor1.TabIndex = 9;
		this.ultraCheckEditor1.CheckedChanged += new System.EventHandler(ultraCheckEditor1_CheckedChanged);
		this.ultraLabel1.Location = new System.Drawing.Point(8, 7);
		this.ultraLabel1.Name = "ultraLabel1";
		this.ultraLabel1.Size = new System.Drawing.Size(184, 16);
		this.ultraLabel1.TabIndex = 0;
		this.ultraLabel1.Text = "請勾選要加入的項目";
		this.panel2.Controls.Add(this.c1FlexGrid1);
		this.panel2.Controls.Add(this.panel3);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Location = new System.Drawing.Point(0, 44);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(584, 337);
		this.panel2.TabIndex = 1;
		this.c1FlexGrid1.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.c1FlexGrid1.ColumnInfo = resources.GetString("c1FlexGrid1.ColumnInfo");
		this.c1FlexGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.c1FlexGrid1.ExtendLastCol = true;
		this.c1FlexGrid1.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.c1FlexGrid1.ForeColor = System.Drawing.Color.Black;
		this.c1FlexGrid1.Location = new System.Drawing.Point(0, 0);
		this.c1FlexGrid1.Name = "c1FlexGrid1";
		this.c1FlexGrid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
		this.c1FlexGrid1.Size = new System.Drawing.Size(584, 305);
		this.c1FlexGrid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("c1FlexGrid1.Styles"));
		this.c1FlexGrid1.TabIndex = 1;
		this.c1FlexGrid1.Tree.Column = 2;
		this.c1FlexGrid1.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.SimpleLeaf;
		this.c1FlexGrid1.Click += new System.EventHandler(c1FlexGrid1_Click);
		this.c1FlexGrid1.BeforeMouseDown += new C1.Win.C1FlexGrid.BeforeMouseDownEventHandler(c1FlexGrid1_BeforeMouseDown);
		this.c1FlexGrid1.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(c1FlexGrid1_AfterEdit);
		this.panel3.Controls.Add(this.ultraButton1);
		this.panel3.Controls.Add(this.BtnPick);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel3.Location = new System.Drawing.Point(0, 305);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(584, 32);
		this.panel3.TabIndex = 2;
		this.ultraButton1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.ultraButton1.Appearance = appearance1;
		this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.ultraButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.ultraButton1.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance2.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance2.BackColor2 = System.Drawing.Color.White;
		appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.ultraButton1.HotTrackAppearance = appearance2;
		this.ultraButton1.HotTracking = true;
		this.ultraButton1.ImageSize = new System.Drawing.Size(20, 20);
		this.ultraButton1.ImageTransparentColor = System.Drawing.Color.White;
		this.ultraButton1.Location = new System.Drawing.Point(504, 3);
		this.ultraButton1.Name = "ultraButton1";
		this.ultraButton1.ShowFocusRect = false;
		this.ultraButton1.ShowOutline = false;
		this.ultraButton1.Size = new System.Drawing.Size(76, 28);
		this.ultraButton1.SupportThemes = false;
		this.ultraButton1.TabIndex = 10;
		this.ultraButton1.Text = "取消";
		this.BtnPick.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance3.Image = resources.GetObject("appearance3.Image");
		appearance3.TextVAlign = Infragistics.Win.VAlign.Middle;
		this.BtnPick.Appearance = appearance3;
		this.BtnPick.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.BtnPick.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.BtnPick.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		appearance4.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance4.BackColor2 = System.Drawing.Color.White;
		appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.BtnPick.HotTrackAppearance = appearance4;
		this.BtnPick.HotTracking = true;
		this.BtnPick.ImageSize = new System.Drawing.Size(20, 20);
		this.BtnPick.ImageTransparentColor = System.Drawing.Color.White;
		this.BtnPick.Location = new System.Drawing.Point(422, 3);
		this.BtnPick.Name = "BtnPick";
		this.BtnPick.ShowFocusRect = false;
		this.BtnPick.ShowOutline = false;
		this.BtnPick.Size = new System.Drawing.Size(80, 28);
		this.BtnPick.SupportThemes = false;
		this.BtnPick.TabIndex = 9;
		this.BtnPick.Text = "確定";
		this.BtnPick.Click += new System.EventHandler(BtnPick_Click);
		this.toolTip1.AutoPopDelay = 6000;
		this.toolTip1.InitialDelay = 500;
		this.toolTip1.ReshowDelay = 100;
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.White;
		this.imageList2.Images.SetKeyName(0, "");
		this.imageList2.Images.SetKeyName(1, "");
		this.imageList2.Images.SetKeyName(2, "");
		this.levelSwitchButton.Location = new System.Drawing.Point(35, 21);
		this.levelSwitchButton.Name = "levelSwitchButton";
		this.levelSwitchButton.Size = new System.Drawing.Size(165, 22);
		this.levelSwitchButton.TabIndex = 10;
		this.levelSwitchButton.LevelSwitchButtonsClicked += new Archnowledge.Pcces.PccesMain.ArchControls.LevelSwitchButton.LevelSwitchButtonClickHandler(levelSwitchButton_LevelSwitchButtonsClicked);
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		base.ClientSize = new System.Drawing.Size(584, 381);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel1);
		base.Name = "Form_FItem_Pick";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "加總項目挑選";
		base.Load += new System.EventHandler(Form_FItem_Pick_Load);
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid1).EndInit();
		this.panel3.ResumeLayout(false);
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
}
