using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.CTRClass;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.PccesMain.SysMaintain;
using Archnowledge.Pcces.STDClass;
using C1.Win.C1FlexGrid;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace Archnowledge.Pcces.PccesMain.SubFinal;

public class FormSubFinal_ItemPick : Form
{
	private IContainer components;

	private Panel panel4;

	private UltraLabel ultraLabel7;

	private Panel panel5;

	private UltraButton ultraButton4;

	private UltraButton BtnPick;

	private GridBudget c1FlexGrid2;

	private System.Windows.Forms.ToolTip toolTip1;

	private FormSys_G_Info1 FM_INFO;

	private Timer tmr_Progress;

	private LevelSwitchButton levelSwitchButton;

	private DataTable DT_FPick = new DataTable();

	private PccesFormAction FormActionName;

	private string UserID = "";

	private string ProjectCode = "";

	private string F_SubProjetCode = "";

	private DataTable F_DT1;

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

	public string _ProjectCode
	{
		get
		{
			return ProjectCode;
		}
		set
		{
			ProjectCode = value;
		}
	}

	public string _SubProjetCode => F_SubProjetCode;

	public PccesFormAction _ActionName
	{
		get
		{
			return FormActionName;
		}
		set
		{
			FormActionName = value;
		}
	}

	public DataTable _DT1
	{
		get
		{
			return F_DT1;
		}
		set
		{
			F_DT1 = value;
		}
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.SubFinal.FormSubFinal_ItemPick));
		this.panel4 = new System.Windows.Forms.Panel();
		this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
		this.panel5 = new System.Windows.Forms.Panel();
		this.ultraButton4 = new Infragistics.Win.Misc.UltraButton();
		this.BtnPick = new Infragistics.Win.Misc.UltraButton();
		this.c1FlexGrid2 = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.tmr_Progress = new System.Windows.Forms.Timer(this.components);
		this.levelSwitchButton = new Archnowledge.Pcces.PccesMain.ArchControls.LevelSwitchButton();
		this.panel4.SuspendLayout();
		this.panel5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid2).BeginInit();
		base.SuspendLayout();
		this.panel4.Controls.Add(this.levelSwitchButton);
		this.panel4.Controls.Add(this.ultraLabel7);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel4.Location = new System.Drawing.Point(0, 0);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(696, 44);
		this.panel4.TabIndex = 2;
		this.ultraLabel7.Location = new System.Drawing.Point(8, 7);
		this.ultraLabel7.Name = "ultraLabel7";
		this.ultraLabel7.Size = new System.Drawing.Size(184, 16);
		this.ultraLabel7.TabIndex = 0;
		this.ultraLabel7.Text = "請勾選要加入的項目";
		this.panel5.Controls.Add(this.ultraButton4);
		this.panel5.Controls.Add(this.BtnPick);
		this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel5.Location = new System.Drawing.Point(0, 413);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(696, 32);
		this.panel5.TabIndex = 4;
		this.ultraButton4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.BackColor = System.Drawing.Color.Silver;
		appearance1.BackColor2 = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.ultraButton4.Appearance = appearance1;
		this.ultraButton4.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.ultraButton4.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		appearance2.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance2.BackColor2 = System.Drawing.Color.White;
		appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.ultraButton4.HotTrackAppearance = appearance2;
		this.ultraButton4.HotTracking = true;
		this.ultraButton4.Location = new System.Drawing.Point(616, 3);
		this.ultraButton4.Name = "ultraButton4";
		this.ultraButton4.Size = new System.Drawing.Size(76, 28);
		this.ultraButton4.TabIndex = 10;
		this.ultraButton4.Text = "取消";
		this.BtnPick.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance3.BackColor = System.Drawing.Color.Silver;
		appearance3.BackColor2 = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		appearance3.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.BtnPick.Appearance = appearance3;
		this.BtnPick.ButtonStyle = Infragistics.Win.UIElementButtonStyle.ButtonSoft;
		this.BtnPick.DialogResult = System.Windows.Forms.DialogResult.OK;
		appearance4.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		appearance4.BackColor2 = System.Drawing.Color.White;
		appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
		this.BtnPick.HotTrackAppearance = appearance4;
		this.BtnPick.HotTracking = true;
		this.BtnPick.Location = new System.Drawing.Point(534, 3);
		this.BtnPick.Name = "BtnPick";
		this.BtnPick.Size = new System.Drawing.Size(80, 28);
		this.BtnPick.TabIndex = 9;
		this.BtnPick.Text = "確定";
		this.BtnPick.Click += new System.EventHandler(BtnPick_Click);
		this.c1FlexGrid2._ExcelFileName = "";
		this.c1FlexGrid2._ExcelSheeName = "";
		this.c1FlexGrid2._IsOpenExcelAfterExport = false;
		this.c1FlexGrid2.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.c1FlexGrid2.ColumnInfo = resources.GetString("c1FlexGrid2.ColumnInfo");
		this.c1FlexGrid2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.c1FlexGrid2.ExtendLastCol = true;
		this.c1FlexGrid2.Font = new System.Drawing.Font("細明體", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.c1FlexGrid2.ForeColor = System.Drawing.Color.Black;
		this.c1FlexGrid2.Location = new System.Drawing.Point(0, 44);
		this.c1FlexGrid2.Name = "c1FlexGrid2";
		this.c1FlexGrid2.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.c1FlexGrid2.ShowToolTipOnNarrowColumn = true;
		this.c1FlexGrid2.Size = new System.Drawing.Size(696, 369);
		this.c1FlexGrid2.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("c1FlexGrid2.Styles"));
		this.c1FlexGrid2.TabIndex = 5;
		this.c1FlexGrid2.Tree.Column = 2;
		this.c1FlexGrid2.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.SimpleLeaf;
		this.c1FlexGrid2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(c1FlexGrid2_KeyPress);
		this.c1FlexGrid2.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(c1FlexGrid2_AfterEdit);
		this.toolTip1.AutoPopDelay = 6000;
		this.toolTip1.InitialDelay = 500;
		this.toolTip1.ReshowDelay = 100;
		this.levelSwitchButton.Location = new System.Drawing.Point(8, 21);
		this.levelSwitchButton.Name = "levelSwitchButton";
		this.levelSwitchButton.Size = new System.Drawing.Size(166, 22);
		this.levelSwitchButton.TabIndex = 1;
		this.levelSwitchButton.LevelSwitchButtonsClicked += new Archnowledge.Pcces.PccesMain.ArchControls.LevelSwitchButton.LevelSwitchButtonClickHandler(levelSwitchButton_LevelSwitchButtonsClicked);
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.ClientSize = new System.Drawing.Size(696, 445);
		base.Controls.Add(this.c1FlexGrid2);
		base.Controls.Add(this.panel5);
		base.Controls.Add(this.panel4);
		base.KeyPreview = true;
		base.Name = "FormSubFinal_ItemPick";
		base.ShowIcon = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "預算項目挑選";
		base.Load += new System.EventHandler(FormSubFinal_ItemPick_Load);
		this.panel4.ResumeLayout(false);
		this.panel5.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.c1FlexGrid2).EndInit();
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

	public FormSubFinal_ItemPick()
	{
		InitializeComponent();
	}

	private void FormSubFinal_ItemPick_Load(object sender, EventArgs e)
	{
		LoadData();
		BindDataToGrid();
	}

	private void LoadData()
	{
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(UserID);
		aArr.Add(CommonMethods.GetFormTypeTitle(FormType.Budget));
		ItemA dbItemA = new ItemA(aArr);
		dbItemA.ps_srckind = "BUD";
		dbItemA.ps_projectCode = ProjectCode;
		DT_FPick = dbItemA.ListItem("", ProjectCode);
	}

	private void BindDataToGrid()
	{
		int iLevel = 0;
		c1FlexGrid2.Rows.Count = DT_FPick.Rows.Count + 1;
		string sTmpStr = "";
		CellStyle CS0 = c1FlexGrid2.Styles.Add("Gray");
		CS0.ForeColor = Color.Gray;
		for (int i = 0; i < DT_FPick.Rows.Count; i++)
		{
			c1FlexGrid2.Rows[i + 1].IsNode = true;
			sTmpStr = DT_FPick.Rows[i]["PrintNo"].ToString().Trim();
			c1FlexGrid2[i + 1, "ItemNo"] = DT_FPick.Rows[i]["ItemNo"].ToString().Trim();
			c1FlexGrid2[i + 1, "CName"] = DT_FPick.Rows[i]["CName"].ToString().Trim();
			c1FlexGrid2[i + 1, "PrintNo"] = DT_FPick.Rows[i]["PrintNo"].ToString().Trim();
			c1FlexGrid2[i + 1, "SNo"] = DT_FPick.Rows[i]["sNo"].ToString().Trim();
			c1FlexGrid2[i + 1, "Cost"] = DT_FPick.Rows[i]["cost"].ToString().Trim();
			c1FlexGrid2[i + 1, "Qty"] = DT_FPick.Rows[i]["qty"].ToString().Trim();
			c1FlexGrid2[i + 1, "UnitName"] = DT_FPick.Rows[i]["unitName"].ToString().Trim();
			c1FlexGrid2[i + 1, "CanCheck"] = true;
			string st1 = DT_FPick.Rows[i]["PrintNo"].ToString().Trim();
			c1FlexGrid2[i + 1, "IsCheck"] = _IsCheck(DT_FPick.Rows[i]["sNo"].ToString().Trim());
			c1FlexGrid2.Rows[i + 1].AllowEditing = _IsEnable(DT_FPick.Rows[i]["sNo"].ToString().Trim());
			c1FlexGrid2[i + 1, "IsShow"] = _IsCheck(DT_FPick.Rows[i]["sNo"].ToString().Trim());
			if (!c1FlexGrid2.Rows[i + 1].AllowEditing)
			{
				c1FlexGrid2.Rows[i + 1].Style = c1FlexGrid2.Styles["Gray"];
				c1FlexGrid2[i + 1, "IsShow"] = false;
			}
			c1FlexGrid2.Rows[i + 1].Node.Level = Convert.ToInt32(DT_FPick.Rows[i]["PrintNo"].ToString().Trim().Length / 4);
			if (DT_FPick.Rows[i]["PrintNo"].ToString().Trim() == "".PadLeft(32, '9'))
			{
				c1FlexGrid2.Rows[i + 1].Node.Level = 1;
			}
			if (c1FlexGrid2.Rows[i + 1].Node.Level > iLevel)
			{
				iLevel = c1FlexGrid2.Rows[i + 1].Node.Level;
			}
		}
		levelSwitchButton.MaxLevel = iLevel;
	}

	private bool _IsCheck(string SNO)
	{
		bool RetV = false;
		DataView DV1 = F_DT1.DefaultView;
		DV1.Sort = "sno";
		int iidx = DV1.Find(SNO);
		if (iidx > -1)
		{
			RetV = true;
		}
		return RetV;
	}

	private bool _IsEnable(string SNO)
	{
		bool RetV = false;
		DataView DV1 = F_DT1.DefaultView;
		DV1.Sort = "sno";
		int iidx = DV1.Find(SNO);
		if (iidx > -1)
		{
			if (DV1[iidx]["FinalFlag"].ToString() == "F")
			{
				RetV = true;
			}
		}
		else
		{
			RetV = true;
		}
		return RetV;
	}

	private void c1FlexGrid2_AfterEdit(object sender, RowColEventArgs e)
	{
		if (c1FlexGrid2.Cols[c1FlexGrid2.MouseCol].Name != "IsCheck")
		{
			c1FlexGrid2[e.Row, "IsCheck"] = true;
		}
		if (c1FlexGrid2.Cols[c1FlexGrid2.MouseCol].Name != "IsShow")
		{
			c1FlexGrid2[e.Row, "IsShow"] = true;
		}
		try
		{
			bool show = (bool)c1FlexGrid2[e.Row, "IsShow"];
			Node LastNode = c1FlexGrid2.Rows[c1FlexGrid2.Row].Node;
			while (LastNode != null && LastNode.Children > 0)
			{
				LastNode = LastNode.GetNode(NodeTypeEnum.LastChild);
			}
			for (int i = c1FlexGrid2.Row; i <= LastNode.Row.SafeIndex; i++)
			{
				c1FlexGrid2[i, "IsCheck"] = show;
				c1FlexGrid2[i, "IsShow"] = show;
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "SubFinal.FormSubFinal_ItemPick.cs" + ex.Message);
		}
	}

	private void c1FlexGrid2_KeyPress(object sender, KeyPressEventArgs e)
	{
		int iSelRows = c1FlexGrid2.Selection.r2 - c1FlexGrid2.Selection.r1;
		if (iSelRows <= 1)
		{
			return;
		}
		for (int i = c1FlexGrid2.Selection.r1; i <= c1FlexGrid2.Selection.r2; i++)
		{
			if ((bool)c1FlexGrid2[i, "CanCheck"])
			{
				c1FlexGrid2[i, "IsCheck"] = true;
			}
		}
	}

	private void BtnPick_Click(object sender, EventArgs e)
	{
		ArrayList aArr = new ArrayList();
		aArr.Clear();
		aArr.Add(UserID);
		aArr.Add(CommonMethods.GetFormTypeTitle(FormType.Budget));
		submfq MfqCom = new submfq(aArr);
		FM_INFO = new FormSys_G_Info1();
		FM_INFO._InfoString = "契約項目載入中，請稍候! ";
		FM_INFO.Owner = this;
		FM_INFO.Show();
		FM_INFO.BringToFront();
		for (int i = 1; i < c1FlexGrid2.Rows.Count - 1; i++)
		{
			if (c1FlexGrid2.Rows[i].AllowEditing)
			{
				MfqCom.ps_itemcost = c1FlexGrid2[i, "Cost"].ToString();
				MfqCom.ps_itemqty = c1FlexGrid2[i, "Qty"].ToString();
				MfqCom.ps_sno = c1FlexGrid2[i, "SNo"].ToString();
				MfqCom.ps_prjcode = ProjectCode;
				MfqCom.ps_subcode = "";
				MfqCom.ps_itemno = "10000";
				MfqCom.ps_itemdes = c1FlexGrid2[i, "PrintNo"].ToString().Trim();
				MfqCom.ps_itemunit = c1FlexGrid2[i, "UnitName"].ToString().Trim();
				MfqCom.ps_chgcount = "0";
				MfqCom.ps_final = "F";
				MfqCom.DeleItem();
			}
		}
		for (int i = 1; i < c1FlexGrid2.Rows.Count - 1; i++)
		{
			if (c1FlexGrid2.Rows[i].AllowEditing && PubTools.Str2Boolean(c1FlexGrid2[i, "IsCheck"]))
			{
				MfqCom.ps_itemcost = c1FlexGrid2[i, "Cost"].ToString();
				MfqCom.ps_itemqty = c1FlexGrid2[i, "Qty"].ToString();
				MfqCom.ps_quantity = c1FlexGrid2[i, "Qty"].ToString();
				MfqCom.ps_tom_amt = (PubTools.Str2Double(c1FlexGrid2[i, "Qty"]) * PubTools.Str2Double(c1FlexGrid2[i, "Cost"])).ToString();
				MfqCom.ps_sno = c1FlexGrid2[i, "SNo"].ToString();
				MfqCom.ps_prjcode = ProjectCode;
				MfqCom.ps_subcode = "";
				MfqCom.ps_itemno = "10000";
				MfqCom.ps_itemdes = c1FlexGrid2[i, "PrintNo"].ToString().Trim();
				MfqCom.ps_itemunit = c1FlexGrid2[i, "UnitName"].ToString().Trim();
				MfqCom.ps_chgcount = "0";
				MfqCom.ps_final = "F";
				MfqCom.ps_FinalPick = "Y";
				MfqCom.InseItem();
			}
		}
		PubTools.WriteRoughlyLog(aArr);
		FM_INFO.Close();
		FM_INFO.Dispose();
	}

	private void levelSwitchButton_LevelSwitchButtonsClicked()
	{
		c1FlexGrid2.Tree.Show(levelSwitchButton.SelectedLevel);
	}
}
