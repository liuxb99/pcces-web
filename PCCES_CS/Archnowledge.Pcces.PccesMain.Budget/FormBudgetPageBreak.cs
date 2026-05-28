using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Pcces.CommonClass;
using Archnowledge.Pcces.PccesMain.ArchControls;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinStatusBar;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormBudgetPageBreak : Form
{
	private IContainer components;

	private GroupBox gbButtons;

	private UltraButton btnCancel;

	private UltraButton btnOK;

	private Panel panelHeader;

	private Panel panelContent;

	private GridBudget gridBudget;

	private UltraLabel labelDescription;

	private UltraLabel labelTitle;

	private UltraStatusBar statusBar;

	private Panel panelLevelSwitchButton;

	private ImageList imageList;

	private LevelSwitchButton levelSwitchButton;

	public Panel panelFooter;

	private DataTable DT1 = new DataTable();

	private string UserID;

	private string ProjectCode;

	private PccesFormAction ActionName;

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

	public PccesFormAction _ActionName
	{
		get
		{
			return ActionName;
		}
		set
		{
			ActionName = value;
		}
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormBudgetPageBreak));
		Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel3 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
		this.panelFooter = new System.Windows.Forms.Panel();
		this.gbButtons = new System.Windows.Forms.GroupBox();
		this.btnCancel = new Infragistics.Win.Misc.UltraButton();
		this.btnOK = new Infragistics.Win.Misc.UltraButton();
		this.panelHeader = new System.Windows.Forms.Panel();
		this.panelLevelSwitchButton = new System.Windows.Forms.Panel();
		this.levelSwitchButton = new Archnowledge.Pcces.PccesMain.ArchControls.LevelSwitchButton();
		this.labelDescription = new Infragistics.Win.Misc.UltraLabel();
		this.labelTitle = new Infragistics.Win.Misc.UltraLabel();
		this.panelContent = new System.Windows.Forms.Panel();
		this.gridBudget = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.statusBar = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.imageList = new System.Windows.Forms.ImageList(this.components);
		this.panelFooter.SuspendLayout();
		this.panelHeader.SuspendLayout();
		this.panelLevelSwitchButton.SuspendLayout();
		this.panelContent.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridBudget).BeginInit();
		base.SuspendLayout();
		this.panelFooter.AutoSize = true;
		this.panelFooter.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		this.panelFooter.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panelFooter.Controls.Add(this.gbButtons);
		this.panelFooter.Controls.Add(this.btnCancel);
		this.panelFooter.Controls.Add(this.btnOK);
		this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panelFooter.Location = new System.Drawing.Point(0, 423);
		this.panelFooter.Name = "panelFooter";
		this.panelFooter.Size = new System.Drawing.Size(672, 43);
		this.panelFooter.TabIndex = 10;
		this.gbButtons.Dock = System.Windows.Forms.DockStyle.Top;
		this.gbButtons.Location = new System.Drawing.Point(0, 0);
		this.gbButtons.Name = "gbButtons";
		this.gbButtons.Size = new System.Drawing.Size(672, 8);
		this.gbButtons.TabIndex = 3;
		this.gbButtons.TabStop = false;
		this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance1.Image = resources.GetObject("appearance1.Image");
		appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnCancel.Appearance = appearance1;
		this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
		this.btnCancel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.Font = new System.Drawing.Font("細明體", 11f);
		this.btnCancel.ImageSize = new System.Drawing.Size(20, 20);
		this.btnCancel.ImageTransparentColor = System.Drawing.Color.White;
		this.btnCancel.Location = new System.Drawing.Point(576, 9);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.ShowFocusRect = false;
		this.btnCancel.ShowOutline = false;
		this.btnCancel.Size = new System.Drawing.Size(88, 31);
		this.btnCancel.SupportThemes = false;
		this.btnCancel.TabIndex = 2;
		this.btnCancel.Text = "取消";
		this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance2.Image = resources.GetObject("appearance2.Image");
		appearance2.ImageHAlign = Infragistics.Win.HAlign.Left;
		appearance2.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnOK.Appearance = appearance2;
		this.btnOK.BackColor = System.Drawing.SystemColors.Control;
		this.btnOK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnOK.Font = new System.Drawing.Font("細明體", 11f);
		this.btnOK.ImageSize = new System.Drawing.Size(20, 20);
		this.btnOK.ImageTransparentColor = System.Drawing.Color.White;
		this.btnOK.Location = new System.Drawing.Point(484, 9);
		this.btnOK.Name = "btnOK";
		this.btnOK.ShowFocusRect = false;
		this.btnOK.ShowOutline = false;
		this.btnOK.Size = new System.Drawing.Size(88, 31);
		this.btnOK.SupportThemes = false;
		this.btnOK.TabIndex = 1;
		this.btnOK.Text = "確定";
		this.btnOK.Click += new System.EventHandler(A_Btn_Next_Click);
		this.panelHeader.BackColor = System.Drawing.Color.White;
		this.panelHeader.Controls.Add(this.panelLevelSwitchButton);
		this.panelHeader.Controls.Add(this.labelDescription);
		this.panelHeader.Controls.Add(this.labelTitle);
		this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
		this.panelHeader.Location = new System.Drawing.Point(0, 0);
		this.panelHeader.Name = "panelHeader";
		this.panelHeader.Size = new System.Drawing.Size(672, 72);
		this.panelHeader.TabIndex = 11;
		this.panelLevelSwitchButton.Controls.Add(this.levelSwitchButton);
		this.panelLevelSwitchButton.Location = new System.Drawing.Point(16, 48);
		this.panelLevelSwitchButton.Name = "panelLevelSwitchButton";
		this.panelLevelSwitchButton.Size = new System.Drawing.Size(166, 24);
		this.panelLevelSwitchButton.TabIndex = 17;
		this.levelSwitchButton.Location = new System.Drawing.Point(0, 2);
		this.levelSwitchButton.Name = "levelSwitchButton";
		this.levelSwitchButton.Size = new System.Drawing.Size(166, 22);
		this.levelSwitchButton.TabIndex = 0;
		this.levelSwitchButton.LevelSwitchButtonsClicked += new Archnowledge.Pcces.PccesMain.ArchControls.LevelSwitchButton.LevelSwitchButtonClickHandler(levelSwitchButton_LevelSwitchButtonsClicked);
		appearance3.BackColor = System.Drawing.Color.White;
		this.labelDescription.Appearance = appearance3;
		this.labelDescription.Location = new System.Drawing.Point(26, 32);
		this.labelDescription.Name = "labelDescription";
		this.labelDescription.Size = new System.Drawing.Size(634, 20);
		this.labelDescription.TabIndex = 6;
		this.labelDescription.Text = "如果你要指定跳頁位置，請於要跳頁的資料列勾選，勾選的位置會先跳至下一頁，再列印。";
		appearance4.BackColor = System.Drawing.Color.White;
		this.labelTitle.Appearance = appearance4;
		this.labelTitle.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.labelTitle.Location = new System.Drawing.Point(10, 8);
		this.labelTitle.Name = "labelTitle";
		this.labelTitle.Size = new System.Drawing.Size(408, 20);
		this.labelTitle.TabIndex = 5;
		this.labelTitle.Text = "詳細表跳頁設定";
		this.panelContent.Controls.Add(this.gridBudget);
		this.panelContent.Controls.Add(this.statusBar);
		this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panelContent.Location = new System.Drawing.Point(0, 72);
		this.panelContent.Name = "panelContent";
		this.panelContent.Size = new System.Drawing.Size(672, 351);
		this.panelContent.TabIndex = 12;
		this.gridBudget._ExcelFileName = "";
		this.gridBudget._ExcelSheeName = "";
		this.gridBudget._IsOpenExcelAfterExport = false;
		this.gridBudget.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridBudget.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.gridBudget.ColumnInfo = resources.GetString("gridBudget.ColumnInfo");
		this.gridBudget.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridBudget.ExtendLastCol = true;
		this.gridBudget.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridBudget.ForeColor = System.Drawing.Color.Black;
		this.gridBudget.Location = new System.Drawing.Point(0, 0);
		this.gridBudget.Name = "gridBudget";
		this.gridBudget.Rows.Count = 1;
		this.gridBudget.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.gridBudget.ShowCursor = true;
		this.gridBudget.ShowSort = false;
		this.gridBudget.ShowToolTipOnNarrowColumn = true;
		this.gridBudget.Size = new System.Drawing.Size(672, 325);
		this.gridBudget.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridBudget.Styles"));
		this.gridBudget.TabIndex = 1;
		this.gridBudget.Tree.Column = 1;
		this.gridBudget.Tree.LineColor = System.Drawing.Color.Gray;
		this.gridBudget.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(gridBudget_AfterEdit);
		appearance5.FontData.SizeInPoints = 11f;
		this.statusBar.Appearance = appearance5;
		this.statusBar.Location = new System.Drawing.Point(0, 325);
		this.statusBar.Name = "statusBar";
		ultraStatusPanel1.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		appearance6.BackColor = System.Drawing.Color.FromArgb(192, 192, 255);
		appearance6.BackColor2 = System.Drawing.Color.Navy;
		appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
		ultraStatusPanel1.ProgressBarInfo.Appearance = appearance6;
		ultraStatusPanel1.Text = "資料筆數：";
		ultraStatusPanel1.Width = 200;
		ultraStatusPanel2.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		appearance7.BackColor = System.Drawing.Color.LightSlateGray;
		appearance7.BackColor2 = System.Drawing.Color.DarkBlue;
		appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
		ultraStatusPanel2.ProgressBarInfo.FillAppearance = appearance7;
		ultraStatusPanel2.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		ultraStatusPanel2.Width = 0;
		appearance8.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance8.TextVAlign = Infragistics.Win.VAlign.Bottom;
		ultraStatusPanel3.Appearance = appearance8;
		ultraStatusPanel3.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel3.Text = "客服電話：(02)2708-8090";
		ultraStatusPanel3.Width = 200;
		this.statusBar.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[3] { ultraStatusPanel1, ultraStatusPanel2, ultraStatusPanel3 });
		this.statusBar.Size = new System.Drawing.Size(672, 26);
		this.statusBar.SupportThemes = false;
		this.statusBar.TabIndex = 2;
		this.imageList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList.ImageStream");
		this.imageList.TransparentColor = System.Drawing.Color.White;
		this.imageList.Images.SetKeyName(0, "");
		this.imageList.Images.SetKeyName(1, "");
		this.imageList.Images.SetKeyName(2, "");
		this.AutoScaleBaseSize = new System.Drawing.Size(6, 18);
		base.CancelButton = this.btnCancel;
		base.ClientSize = new System.Drawing.Size(672, 466);
		base.Controls.Add(this.panelContent);
		base.Controls.Add(this.panelHeader);
		base.Controls.Add(this.panelFooter);
		this.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.Name = "FormBudgetPageBreak";
		base.ShowIcon = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "跳頁設定";
		base.Load += new System.EventHandler(FormBudgetPageBreak_Load);
		this.panelFooter.ResumeLayout(false);
		this.panelHeader.ResumeLayout(false);
		this.panelLevelSwitchButton.ResumeLayout(false);
		this.panelContent.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridBudget).EndInit();
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

	public FormBudgetPageBreak()
	{
		InitializeComponent();
	}

	private void HideCols(bool IsHide)
	{
		if (IsHide)
		{
			gridBudget.Cols["LevelNo"].Visible = false;
			gridBudget.Cols["Kind"].Visible = false;
			gridBudget.Cols["SNo"].Visible = false;
			gridBudget.Cols["Analysis"].Visible = false;
		}
	}

	private void SetColsEditSymbol()
	{
		CellStyle csEditMode = gridBudget.Styles.Add("EditMode");
		csEditMode.DataType = typeof(Image);
		csEditMode.ImageAlign = ImageAlignEnum.RightCenter;
		for (int i = 1; i < gridBudget.Cols.Count; i++)
		{
			if (gridBudget.Cols[i].AllowEditing)
			{
				CellRange rg = gridBudget.GetCellRange(0, i);
				rg.Style = gridBudget.Styles["EditMode"];
				rg.Image = imageList.Images[1];
			}
		}
	}

	private void FormBudgetPageBreak_Load(object sender, EventArgs e)
	{
		HideCols(IsHide: true);
		DT1 = GetItemAPageBreakSettings();
		BindToGrid();
		if (CommonMethods.GetActionNameString(ActionName).ToUpper() == "BID")
		{
			btnOK.Enabled = false;
		}
	}

	private DataTable GetItemAPageBreakSettings()
	{
		DataTable RetV = new DataTable();
		string ls_selectstr = "Select b.pccescode,b.analysis, b.analysisQty, a.itemNo, a.cName, a.unitName, a.SNo, a.PrintNo, a.Kind, c.IsPageBreak ";
		ls_selectstr = ((!(CommonMethods.GetActionNameString(ActionName).ToUpper() == "BUD")) ? (ls_selectstr + " from bidItemA a left outer join bidProjMrsA b  on a.pubcode=b.pubcode and a.projectcode=b.projectcode  left outer join bidPageBreak c  on a.SNo = c.SNo and a.ProjectCode = c.ProjectCode ") : (ls_selectstr + " from budItemA a left outer join budProjMrsA b  on a.pubcode=b.pubcode and a.projectcode=b.projectcode  left outer join budPageBreak c  on a.SNo = c.SNo and a.ProjectCode = c.ProjectCode "));
		ls_selectstr = ls_selectstr + " where a.ProjectCode = '" + ProjectCode + "' ";
		ls_selectstr += " order by a.PrintNo ";
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = UserID;
		RetV = DBCLS.GetUserDefine(ls_selectstr);
		DBCLS = null;
		return RetV;
	}

	private void BindToGrid()
	{
		Cursor = Cursors.WaitCursor;
		int iLevel = 0;
		statusBar.Panels[1].ProgressBarInfo.Value = 0;
		statusBar.Panels[1].ProgressBarInfo.Minimum = 0;
		statusBar.Panels[1].ProgressBarInfo.Maximum = DT1.Rows.Count;
		statusBar.Panels[1].ProgressBarInfo.ShowLabel = true;
		statusBar.Panels[0].Text = "資料筆數 : " + DT1.Rows.Count;
		CellStyle CS0 = gridBudget.Styles.Add("Transparent");
		CellStyle CS1 = gridBudget.Styles.Add("AnalysisColor");
		CellStyle CS2 = gridBudget.Styles.Add("MainColor");
		CellStyle CS9 = gridBudget.Styles.Add("IsSharedColor");
		CellStyle CSA = gridBudget.Styles.Add("Adjustment");
		CS0.ForeColor = Color.Transparent;
		CS1.ForeColor = Color.Red;
		CS2.ForeColor = Color.Blue;
		CS9.ForeColor = Color.Green;
		CSA.BackColor = Color.OrangeRed;
		gridBudget.Redraw = false;
		gridBudget.Rows.Count = DT1.Rows.Count + 1;
		string sKind = "";
		for (int i = 0; i < DT1.Rows.Count; i++)
		{
			gridBudget[i + 1, "ItemNo"] = DT1.Rows[i]["itemNo"].ToString().Trim();
			gridBudget[i + 1, "CName"] = DT1.Rows[i]["cName"].ToString().Trim();
			gridBudget[i + 1, "UnitName"] = DT1.Rows[i]["unitName"].ToString().Trim();
			gridBudget[i + 1, "IsPageBreak"] = DT1.Rows[i]["IsPageBreak"].ToString().Trim() == "Y";
			gridBudget[i + 1, "Kind"] = DT1.Rows[i]["Kind"].ToString().Trim();
			gridBudget[i + 1, "SNo"] = DT1.Rows[i]["sNo"].ToString().Trim();
			gridBudget.Rows[i + 1].IsNode = true;
			gridBudget.Rows[i + 1].Node.Level = DT1.Rows[i]["PrintNo"].ToString().Trim().Length / 4;
			sKind = ((DT1.Rows[i]["kind"].ToString().Length > 0) ? DT1.Rows[i]["kind"].ToString().ToUpper().Trim() : "");
			switch (sKind)
			{
			default:
				if (!(sKind == "U"))
				{
					break;
				}
				goto case "B";
			case "B":
			case "L":
			case "F":
			case "S":
			case "Z":
				gridBudget.Rows[i + 1].Style = gridBudget.Styles["MainColor"];
				break;
			}
			if (DT1.Rows[i]["analysis"].ToString().Trim() == "1")
			{
				gridBudget[i + 1, "Analysis"] = true;
				gridBudget.Rows[i + 1].Style = gridBudget.Styles["AnalysisColor"];
			}
			else
			{
				gridBudget[i + 1, "Analysis"] = false;
			}
			if (DT1.Rows[i]["PrintNo"].ToString().Trim() == "".PadLeft(32, '9'))
			{
				gridBudget.Rows[i + 1].Node.Level = 1;
			}
			if (gridBudget.Rows[i + 1].Node.Level > iLevel)
			{
				iLevel = gridBudget.Rows[i + 1].Node.Level;
			}
			Application.DoEvents();
			statusBar.Panels[1].ProgressBarInfo.Value = i + 1;
		}
		gridBudget.Redraw = true;
		levelSwitchButton.MaxLevel = iLevel;
		SetColsEditSymbol();
		statusBar.Panels[1].ProgressBarInfo.Value = 0;
		statusBar.Panels[1].ProgressBarInfo.ShowLabel = false;
		Cursor = Cursors.Default;
	}

	private void A_Btn_Next_Click(object sender, EventArgs e)
	{
		string sSQL = "";
		string sSrcKind = CommonMethods.GetActionNameString(ActionName);
		DBClass DBCLS = new DBClass();
		DBCLS._FS_UserID = UserID;
		DataTable DT_IsPageBreak = DBCLS.GetUserDefine("Select SNo, IsPageBreak From " + sSrcKind + "PageBreak Where ProjectCode='" + ProjectCode + "' --and ( IsPageBreak is not null and IsPageBreak = 'Y')");
		for (int i = 1; i < gridBudget.Rows.Count; i++)
		{
			if (DT_IsPageBreak.Rows.Count == 0 && (bool)gridBudget[i, "IsPageBreak"])
			{
				object obj = sSQL;
				sSQL = string.Concat(obj, " Insert Into ", sSrcKind, "PageBreak (ProjectCode, SNo, IsPageBreak) values ('", ProjectCode, "',", gridBudget[i, "SNo"].ToString(), ",'Y') ", '\r');
			}
			else if (DT_IsPageBreak.Rows.Count > 0)
			{
				DataRow[] DR1 = DT_IsPageBreak.Select("SNo ='" + gridBudget[i, "SNo"].ToString().Trim() + "' ");
				if ((bool)gridBudget[i, "IsPageBreak"] && DR1.Length > 0)
				{
					object obj = sSQL;
					sSQL = string.Concat(obj, " Update ", sSrcKind, "PageBreak Set IsPageBreak ='Y'  Where ProjectCode = '", ProjectCode, "' and SNo=", gridBudget[i, "SNo"].ToString(), " ", '\r');
				}
				else if (!(bool)gridBudget[i, "IsPageBreak"] && DR1.Length > 0)
				{
					object obj = sSQL;
					sSQL = string.Concat(obj, " Update ", sSrcKind, "PageBreak Set IsPageBreak = null  Where ProjectCode = '", ProjectCode, "' and SNo=", gridBudget[i, "SNo"].ToString(), " ", '\r');
				}
				else if ((bool)gridBudget[i, "IsPageBreak"] && DR1.Length == 0)
				{
					object obj = sSQL;
					sSQL = string.Concat(obj, " Insert Into ", sSrcKind, "PageBreak (ProjectCode, SNo, IsPageBreak) values ('", ProjectCode, "',", gridBudget[i, "SNo"].ToString(), ",'Y') ", '\r');
				}
			}
			if (sSQL != "" && i % 50 == 0)
			{
				DBCLS.ExecuteCommand(sSQL);
				sSQL = "";
			}
		}
		if (sSQL != "")
		{
			DBCLS.ExecuteCommand(sSQL);
		}
		DBCLS = null;
		base.DialogResult = DialogResult.OK;
	}

	private void gridBudget_AfterEdit(object sender, RowColEventArgs e)
	{
		if (e.Col == gridBudget.Cols["IsPageBreak"].SafeIndex && !gridBudget.Rows[e.Row].Visible)
		{
			gridBudget[e.Row, "IsPageBreak"] = !(bool)gridBudget[e.Row, "IsPageBreak"];
			e.Cancel = true;
		}
	}

	private void levelSwitchButton_LevelSwitchButtonsClicked()
	{
		gridBudget.Tree.Show(levelSwitchButton.SelectedLevel);
	}
}
