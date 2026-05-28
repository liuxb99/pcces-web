using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Archnowledge.Common;
using Archnowledge.Pcces.DomainModule.Budget;
using Archnowledge.Pcces.PccesMain.ArchControls;
using C1.Win.C1FlexGrid;
using C1.Win.C1FlexGrid.Util.BaseControls;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinStatusBar;

namespace Archnowledge.Pcces.PccesMain.Budget;

public class FormBudgetSetSurName : Form
{
	private DataSet dsWorkItems;

	private string ProjectCode;

	private IContainer components;

	private Panel panelFooter;

	private GroupBox gbButtons;

	private UltraButton btnCancel;

	private UltraButton btnOK;

	private Panel panelTitle;

	private Panel panelContent;

	private GridBudget gridItems;

	private UltraLabel lbDescription;

	private UltraLabel lbTitle;

	private UltraStatusBar statusBar;

	private ImageList imageList;

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

	public FormBudgetSetSurName()
	{
		InitializeComponent();
	}

	private void FormBudgetBidCostSet_Load(object sender, EventArgs e)
	{
		DataToGrid();
		InitCheckAllCheckBox();
	}

	private void DataToGrid()
	{
		Cursor = Cursors.WaitCursor;
		BudProjMrsA budProjMrsa = new BudProjMrsA();
		dsWorkItems = budProjMrsa.GetWorkItem(ProjectCode, 0);
		int rowCount = dsWorkItems.Tables[0].Rows.Count;
		CellStyle csAnalysis = gridItems.Styles.Add("AnalysisColor");
		csAnalysis.ForeColor = Color.Red;
		gridItems.Redraw = false;
		gridItems.Rows.Count = rowCount + 1;
		for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
		{
			DataRow row = dsWorkItems.Tables[0].Rows[rowIndex];
			gridItems[rowIndex + 1, "PccesCode"] = row["pccesCode"];
			gridItems[rowIndex + 1, "CName"] = row["cName"];
			gridItems[rowIndex + 1, "UnitName"] = row["unitName"];
			gridItems[rowIndex + 1, "surName"] = row["surName"];
			gridItems[rowIndex + 1, "IsChange"] = row["IssurName"].ToString() == "Y";
			if (row["analysis"].ToString().Trim() == "1")
			{
				gridItems.Rows[rowIndex + 1].Style = gridItems.Styles["AnalysisColor"];
			}
		}
		gridItems.Redraw = true;
		statusBar.Panels[0].Text = "資料筆數 : " + rowCount;
		Cursor = Cursors.Default;
	}

	private void InitCheckAllCheckBox()
	{
		gridItems.SetData(0, 1, "勾選", coerce: false);
		SetCheckAllStatus();
	}

	private void SetCheckAllStatus()
	{
		for (int i = 1; i < gridItems.Rows.Count; i++)
		{
			if (!(bool)gridItems[i, "IsChange"])
			{
				gridItems.SetCellCheck(0, 1, CheckEnum.Unchecked);
				return;
			}
		}
		gridItems.SetCellCheck(0, 1, CheckEnum.Checked);
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		Cursor = Cursors.WaitCursor;
		GridToData();
		BudProjMrsA budProjMrsA = new BudProjMrsA();
		ExecResult ER = budProjMrsA.UpdateProjMrsA(dsWorkItems);
		if (ER.ReturnCode != 0)
		{
			MessageBox.Show(this, "更新失敗！" + ER.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		base.DialogResult = DialogResult.OK;
		Cursor = Cursors.Default;
	}

	private void GridToData()
	{
		dsWorkItems.Tables[0].PrimaryKey = new DataColumn[1] { dsWorkItems.Tables[0].Columns["PccesCode"] };
		for (int rowIndex = 1; rowIndex < gridItems.Rows.Count; rowIndex++)
		{
			string PccesCode = gridItems[rowIndex, "pccesCode"].ToString();
			DataRow row = dsWorkItems.Tables[0].Rows.Find(PccesCode);
			if ((bool)gridItems[rowIndex, "IsChange"])
			{
				row["IssurName"] = "Y";
			}
			else
			{
				row["IssurName"] = "N";
			}
		}
	}

	private void gridItems_AfterEdit(object sender, RowColEventArgs e)
	{
		if (e.Row == 0 && e.Col == 1)
		{
			CheckEnum CheckStatus = gridItems.GetCellCheck(e.Row, e.Col);
			for (int i = 1; i < gridItems.Rows.Count; i++)
			{
				gridItems.SetCellCheck(i, 1, CheckStatus);
			}
		}
		else
		{
			SetCheckAllStatus();
		}
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormBudgetSetSurName));
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
		this.panelTitle = new System.Windows.Forms.Panel();
		this.lbDescription = new Infragistics.Win.Misc.UltraLabel();
		this.lbTitle = new Infragistics.Win.Misc.UltraLabel();
		this.panelContent = new System.Windows.Forms.Panel();
		this.gridItems = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.statusBar = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.imageList = new System.Windows.Forms.ImageList(this.components);
		this.panelFooter.SuspendLayout();
		this.panelTitle.SuspendLayout();
		this.panelContent.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridItems).BeginInit();
		base.SuspendLayout();
		this.panelFooter.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panelFooter.Controls.Add(this.gbButtons);
		this.panelFooter.Controls.Add(this.btnCancel);
		this.panelFooter.Controls.Add(this.btnOK);
		this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panelFooter.Location = new System.Drawing.Point(0, 422);
		this.panelFooter.Name = "panelFooter";
		this.panelFooter.Size = new System.Drawing.Size(672, 44);
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
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		this.panelTitle.BackColor = System.Drawing.Color.White;
		this.panelTitle.Controls.Add(this.lbDescription);
		this.panelTitle.Controls.Add(this.lbTitle);
		this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
		this.panelTitle.Location = new System.Drawing.Point(0, 0);
		this.panelTitle.Name = "panelTitle";
		this.panelTitle.Size = new System.Drawing.Size(672, 74);
		this.panelTitle.TabIndex = 11;
		appearance3.BackColor = System.Drawing.Color.White;
		this.lbDescription.Appearance = appearance3;
		this.lbDescription.Location = new System.Drawing.Point(26, 29);
		this.lbDescription.Name = "lbDescription";
		this.lbDescription.Size = new System.Drawing.Size(622, 35);
		this.lbDescription.TabIndex = 6;
		this.lbDescription.Text = "如果你要特別指定某些項目及說明用別名取代，請將項目勾選起來，匯出電子檔時則會以別名取代原本的項目及說明。";
		appearance4.BackColor = System.Drawing.Color.White;
		this.lbTitle.Appearance = appearance4;
		this.lbTitle.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.lbTitle.Location = new System.Drawing.Point(10, 8);
		this.lbTitle.Name = "lbTitle";
		this.lbTitle.Size = new System.Drawing.Size(408, 20);
		this.lbTitle.TabIndex = 5;
		this.lbTitle.Text = "輸出電子檔時，以別名替換工項名稱";
		this.panelContent.Controls.Add(this.gridItems);
		this.panelContent.Controls.Add(this.statusBar);
		this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panelContent.Location = new System.Drawing.Point(0, 74);
		this.panelContent.Name = "panelContent";
		this.panelContent.Size = new System.Drawing.Size(672, 348);
		this.panelContent.TabIndex = 12;
		this.gridItems._ExcelFileName = "";
		this.gridItems._ExcelSheeName = "";
		this.gridItems._IsOpenExcelAfterExport = false;
		this.gridItems.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridItems.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.gridItems.ColumnInfo = resources.GetString("gridItems.ColumnInfo");
		this.gridItems.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridItems.ExtendLastCol = true;
		this.gridItems.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridItems.ForeColor = System.Drawing.Color.Black;
		this.gridItems.Location = new System.Drawing.Point(0, 0);
		this.gridItems.Name = "gridItems";
		this.gridItems.Rows.Count = 1;
		this.gridItems.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.gridItems.ShowCursor = true;
		this.gridItems.ShowSort = false;
		this.gridItems.ShowToolTipOnNarrowColumn = true;
		this.gridItems.Size = new System.Drawing.Size(672, 322);
		this.gridItems.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridItems.Styles"));
		this.gridItems.TabIndex = 1;
		this.gridItems.Tree.Column = 1;
		this.gridItems.Tree.LineColor = System.Drawing.Color.Gray;
		this.gridItems.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(gridItems_AfterEdit);
		appearance5.FontData.SizeInPoints = 11f;
		this.statusBar.Appearance = appearance5;
		this.statusBar.Location = new System.Drawing.Point(0, 322);
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
		this.statusBar.Text = "ultraStatusBar1";
		this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
		this.imageList.ImageSize = new System.Drawing.Size(16, 16);
		this.imageList.TransparentColor = System.Drawing.Color.Transparent;
		this.AutoScaleBaseSize = new System.Drawing.Size(6, 18);
		base.CancelButton = this.btnCancel;
		base.ClientSize = new System.Drawing.Size(672, 466);
		base.Controls.Add(this.panelContent);
		base.Controls.Add(this.panelTitle);
		base.Controls.Add(this.panelFooter);
		this.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.Name = "FormBudgetSetSurName";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "工項名稱別名替換設定";
		base.Load += new System.EventHandler(FormBudgetBidCostSet_Load);
		this.panelFooter.ResumeLayout(false);
		this.panelTitle.ResumeLayout(false);
		this.panelContent.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridItems).EndInit();
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
