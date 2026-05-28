using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.BUDClass;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.CTRClass;
using Archnowledge.Pcces.DomainModule.Budget;
using Archnowledge.Pcces.DomainModule.Sub;
using Archnowledge.Pcces.PccesMain.ArchControls;
using Archnowledge.Pcces.PccesMain.SysMaintain;
using Archnowledge.Pcces.STDClass;
using C1.Win.C1FlexGrid;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace Archnowledge.Pcces.PccesMain.SplitContract;

public class FormSplitCnt_ItemPick : Form
{
	private sub_Ctr ctrcom;

	private DataTable DT_FPick = new DataTable();

	private PccesFormAction FormActionName;

	private string UserID = "";

	private string ProjectCode = "";

	private string SubProjetCode = "";

	private DataTable F_DT1;

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

	public string _SubProjetCode => SubProjetCode;

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

	public FormSplitCnt_ItemPick()
	{
		InitializeComponent();
	}

	private void FormSplitCnt_ItemPick_Load(object sender, EventArgs e)
	{
		LoadData();
		BindDataToGrid();
	}

	private void LoadData()
	{
		BudItemA theItemA = new BudItemA();
		DataSet dsItemA = theItemA.GetItemA(ProjectCode, 0);
		DT_FPick = dsItemA.Tables[0];
	}

	private void BindDataToGrid()
	{
		int iLevel = 0;
		c1FlexGrid2.Rows.Count = DT_FPick.Rows.Count + 1;
		for (int i = 0; i < DT_FPick.Rows.Count; i++)
		{
			c1FlexGrid2.Rows[i + 1].IsNode = true;
			c1FlexGrid2[i + 1, "ItemNo"] = DT_FPick.Rows[i]["ItemNo"].ToString().Trim();
			c1FlexGrid2[i + 1, "CName"] = DT_FPick.Rows[i]["CName"].ToString().Trim();
			c1FlexGrid2[i + 1, "PrintNo"] = DT_FPick.Rows[i]["PrintNo"].ToString().Trim();
			c1FlexGrid2[i + 1, "SNo"] = DT_FPick.Rows[i]["sNo"].ToString().Trim();
			c1FlexGrid2[i + 1, "CanCheck"] = true;
			string PrintNo = DT_FPick.Rows[i]["PrintNo"].ToString().Trim();
			c1FlexGrid2[i + 1, "IsCheck"] = _IsCheck(DT_FPick.Rows[i]["sNo"].ToString().Trim());
			c1FlexGrid2.Rows[i + 1].Node.Level = PrintNo.Length / 4;
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

	private void c1FlexGrid2_AfterEdit(object sender, RowColEventArgs e)
	{
		if (c1FlexGrid2.Cols[c1FlexGrid2.MouseCol].Name != "IsCheck")
		{
			c1FlexGrid2[e.Row, "IsCheck"] = true;
		}
		try
		{
			bool check = (bool)c1FlexGrid2[e.Row, "IsCheck"];
			Node LastNode = c1FlexGrid2.Rows[c1FlexGrid2.Row].Node;
			while (LastNode != null && LastNode.Children > 0)
			{
				LastNode = LastNode.GetNode(NodeTypeEnum.LastChild);
			}
			for (int i = c1FlexGrid2.Row; i <= LastNode.Row.SafeIndex; i++)
			{
				c1FlexGrid2[i, "IsCheck"] = check;
			}
		}
		catch (Exception ex)
		{
			CommonMethods.LogFile("Pcces46", "M", "SplitContract.FormSplitCnt_ItemPick.cs" + ex.Message);
		}
		if (!(bool)c1FlexGrid2[e.Row, "IsCheck"])
		{
			string PrintNo = c1FlexGrid2[e.Row, "PrintNo"].ToString().Trim();
			int iNum = PrintNo.Length - 4;
			PrintNo = PrintNo.Substring(0, iNum);
			for (int i = 1; i < c1FlexGrid2.Rows.Count; i++)
			{
				string TempPrintNo = c1FlexGrid2[i, "PrintNo"].ToString().Trim();
				iNum = TempPrintNo.Length - 4;
				TempPrintNo = TempPrintNo.Substring(0, iNum);
				if (PrintNo.Trim() == TempPrintNo.Trim() && (bool)c1FlexGrid2[i, "IsCheck"])
				{
					return;
				}
			}
		}
		string sPrintNo = c1FlexGrid2[e.Row, "PrintNo"].ToString().Trim();
		int iCount = sPrintNo.Length - 4;
		iCount /= 4;
		int j = 4;
		ArrayList aPrintNo = new ArrayList();
		for (int i = 0; i < iCount; i++)
		{
			string sNo = sPrintNo.Substring(0, j);
			aPrintNo.Add(sNo);
			j += 4;
		}
		if (aPrintNo.Count <= 0)
		{
			return;
		}
		for (int k = 0; k < aPrintNo.Count; k++)
		{
			for (int i = 1; i < c1FlexGrid2.Rows.Count; i++)
			{
				if (aPrintNo[k].ToString().Trim() == c1FlexGrid2[i, "PrintNo"].ToString().Trim())
				{
					c1FlexGrid2[i, "IsCheck"] = (bool)c1FlexGrid2[e.Row, "IsCheck"];
					break;
				}
			}
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
		ctrcom = new sub_Ctr(aArr);
		ctrcom.ps_srckind = "SUB";
		string ls_prjcode = ProjectCode;
		string ls_subproj = SubProjetCode;
		int temp = ctrcom.DeleItemAll(ls_subproj, ls_prjcode);
		FM_INFO = new FormSys_G_Info1();
		FM_INFO._InfoString = "契約項目載入中，請稍候! ";
		FM_INFO.Owner = this;
		FM_INFO.Show();
		FM_INFO.BringToFront();
		DataTable DT_SUB1 = new DataTable();
		DT_SUB1.Columns.Add("sNO", Type.GetType("System.String"));
		for (int i = 1; i < c1FlexGrid2.Rows.Count; i++)
		{
			if ((bool)c1FlexGrid2[i, "IsCheck"])
			{
				DataRow DR1 = DT_SUB1.NewRow();
				DR1["sNO"] = c1FlexGrid2[i, "SNo"].ToString();
				DT_SUB1.Rows.Add(DR1);
			}
		}
		tmr_Progress.Enabled = true;
		subProject subcom = new subProject(aArr);
		Archnowledge.Pcces.BUDClass.Project proj = new Archnowledge.Pcces.BUDClass.Project(aArr);
		proj.ps_projectCode = ProjectCode;
		proj.ps_srckind = "BUD";
		DataTable dt = proj.ListItem("", ProjectCode);
		if (dt.Rows.Count > 0)
		{
			subcom.ps_prjcode = ProjectCode;
			subcom.ps_subcode = "";
			subcom.ps_subdesc = "";
			subcom.ps_invoice = "";
			subcom.ps_owner = "";
			subcom.ps_mainCode = dt.Rows[0]["mainCode"].ToString();
			subcom.ps_mainCName = dt.Rows[0]["mainCName"].ToString();
			subcom.ps_projectNameC = dt.Rows[0]["projectNameC"].ToString();
			subcom.ps_projectNameE = dt.Rows[0]["projectNameE"].ToString();
			subcom.ps_projectAddress = dt.Rows[0]["projectAddress"].ToString();
			subcom.ps_reCalType = dt.Rows[0]["ReCalType"].ToString();
		}
		subcom.InseItem();
		int temp2 = ctrcom.CopyBudItem(DT_SUB1, ls_subproj, ls_prjcode);
		tmr_Progress.Enabled = false;
		ctrcom.ReSet_ItemB_and_ItemC(ls_subproj, ls_prjcode);
		PubTools.WriteRoughlyLog(aArr);
		SubProject subProject = new SubProject();
		ExecResult ER = subProject.SyncLockCostFromBud(ls_prjcode);
		if (ER.ReturnCode != 0)
		{
			MessageBox.Show("錯誤：" + ER.Message);
		}
		ModifyDB StdCom = new ModifyDB("", aArr);
		string ls_selectstr = "Update SubInfo set flag = '' where  projectcode='" + ls_prjcode.Trim() + "' and Sproj='" + ls_subproj.Trim() + "' ";
		StdCom.DBUpd(ls_selectstr);
		FM_INFO.Close();
		FM_INFO.Dispose();
	}

	private void tmr_Progress_Tick(object sender, EventArgs e)
	{
		FM_INFO._MinValue = ctrcom.ps_Min;
		FM_INFO._MaxValue = ctrcom.ps_Max;
		FM_INFO._ProgressValue = ctrcom.ps_CurrentProgress;
	}

	private void levelSwitchButton_LevelSwitchButtonsClicked()
	{
		c1FlexGrid2.Tree.Show(levelSwitchButton.SelectedLevel);
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.SplitContract.FormSplitCnt_ItemPick));
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
		this.c1FlexGrid2.Tree.Column = 1;
		this.c1FlexGrid2.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.SimpleLeaf;
		this.c1FlexGrid2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(c1FlexGrid2_KeyPress);
		this.c1FlexGrid2.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(c1FlexGrid2_AfterEdit);
		this.toolTip1.AutoPopDelay = 6000;
		this.toolTip1.InitialDelay = 500;
		this.toolTip1.ReshowDelay = 100;
		this.tmr_Progress.Tick += new System.EventHandler(tmr_Progress_Tick);
		this.levelSwitchButton.Location = new System.Drawing.Point(13, 21);
		this.levelSwitchButton.Name = "levelSwitchButton";
		this.levelSwitchButton.Size = new System.Drawing.Size(165, 22);
		this.levelSwitchButton.TabIndex = 1;
		this.levelSwitchButton.LevelSwitchButtonsClicked += new Archnowledge.Pcces.PccesMain.ArchControls.LevelSwitchButton.LevelSwitchButtonClickHandler(levelSwitchButton_LevelSwitchButtonsClicked);
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
		this.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		base.CancelButton = this.ultraButton4;
		base.ClientSize = new System.Drawing.Size(696, 445);
		base.Controls.Add(this.c1FlexGrid2);
		base.Controls.Add(this.panel5);
		base.Controls.Add(this.panel4);
		base.KeyPreview = true;
		base.Name = "FormSplitCnt_ItemPick";
		base.ShowIcon = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "預算項目挑選";
		base.Load += new System.EventHandler(FormSplitCnt_ItemPick_Load);
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
}
