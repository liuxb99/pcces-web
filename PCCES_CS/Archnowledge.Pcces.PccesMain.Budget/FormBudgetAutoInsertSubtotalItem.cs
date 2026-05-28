using System;
using System.Collections;
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

public class FormBudgetAutoInsertSubtotalItem : Form
{
	private string projectCode;

	private BudItemA budItemA = new BudItemA();

	private IContainer components;

	private Panel panelBottom;

	private GroupBox groupBox1;

	private Panel panelTop;

	private Panel panelGrid;

	private GridBudget gridMainItem;

	private UltraLabel lbInstruction;

	private UltraLabel lbTitle;

	private UltraStatusBar statusBar;

	private Panel panelLevel;

	private ImageList imageList;

	private UltraButton btnOK;

	private UltraButton btnCancel;

	private UltraLabel lbCaution;

	private LevelSwitchButton levelSwitchButton;

	public string _ProjectCode
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

	public FormBudgetAutoInsertSubtotalItem()
	{
		InitializeComponent();
	}

	private void FormBudgetBidSet_Load(object sender, EventArgs e)
	{
		DataSet dsBudItemAKindB = GetData();
		DataToForm(dsBudItemAKindB);
	}

	private DataSet GetData()
	{
		return budItemA.GetBudItemAKindB(projectCode);
	}

	private void DataToForm(DataSet dsBudItemAKindB)
	{
		statusBar.Panels[0].Text = "資料筆數 : " + dsBudItemAKindB.Tables[0].Rows.Count;
		CellStyle csMainItem = gridMainItem.Styles.Add("MainItem");
		csMainItem.ForeColor = Color.Blue;
		CellStyle csHasSubtotal = gridMainItem.Styles.Add("HasSubtotal");
		csHasSubtotal.BackColor = Color.LightGray;
		int maxLevel = 1;
		int level = 1;
		gridMainItem.Redraw = false;
		foreach (DataRow row in dsBudItemAKindB.Tables[0].Rows)
		{
			Row gridRow = gridMainItem.Rows.Add();
			gridRow["itemNo"] = row["itemNo"];
			gridRow["itemName"] = row["cName"];
			gridRow["sNo"] = row["sNo"];
			bool hasSubtotal = ArchConvert.Obj2Bool(row["hasSubtotal"]);
			gridRow["autoInsertSubtotal"] = hasSubtotal;
			gridRow.Style = (hasSubtotal ? csHasSubtotal : csMainItem);
			if (hasSubtotal)
			{
				gridRow.AllowEditing = false;
			}
			gridRow.IsNode = true;
			level = ArchConvert.Obj2Int(row["levelNo"]);
			gridRow.Node.Level = level;
			if (level > maxLevel)
			{
				maxLevel = level;
			}
		}
		gridMainItem.Redraw = true;
		levelSwitchButton.MaxLevel = maxLevel;
		SetColumnEditSymbol();
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		foreach (Row row in (IEnumerable)gridMainItem.Rows)
		{
			if (row.AllowEditing && ArchConvert.Obj2Bool(row["autoInsertSubtotal"]))
			{
				ExecResult ER = budItemA.AddSubTotal(projectCode, ArchConvert.Obj2Int(row["sNo"]));
				if (ER.ReturnCode != 0)
				{
					MessageBox.Show(ER.Message);
					break;
				}
			}
		}
		base.DialogResult = DialogResult.Yes;
		Close();
	}

	private void SetColumnEditSymbol()
	{
		CellStyle csEditMode = gridMainItem.Styles.Add("EditMode");
		csEditMode.DataType = typeof(Image);
		csEditMode.ImageAlign = ImageAlignEnum.RightCenter;
		for (int i = 1; i < gridMainItem.Cols.Count; i++)
		{
			if (gridMainItem.Cols[i].AllowEditing)
			{
				CellRange cellRange = gridMainItem.GetCellRange(0, i);
				cellRange.Style = gridMainItem.Styles["EditMode"];
				cellRange.Image = imageList.Images[0];
			}
		}
	}

	private void levelSwitchButton_LevelSwitchButtonsClicked()
	{
		gridMainItem.Tree.Show(levelSwitchButton.SelectedLevel);
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archnowledge.Pcces.PccesMain.Budget.FormBudgetAutoInsertSubtotalItem));
		Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
		Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel7 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel8 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel9 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
		Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
		this.panelBottom = new System.Windows.Forms.Panel();
		this.lbCaution = new Infragistics.Win.Misc.UltraLabel();
		this.btnCancel = new Infragistics.Win.Misc.UltraButton();
		this.btnOK = new Infragistics.Win.Misc.UltraButton();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.panelTop = new System.Windows.Forms.Panel();
		this.panelLevel = new System.Windows.Forms.Panel();
		this.levelSwitchButton = new Archnowledge.Pcces.PccesMain.ArchControls.LevelSwitchButton();
		this.lbInstruction = new Infragistics.Win.Misc.UltraLabel();
		this.lbTitle = new Infragistics.Win.Misc.UltraLabel();
		this.panelGrid = new System.Windows.Forms.Panel();
		this.gridMainItem = new Archnowledge.Pcces.PccesMain.ArchControls.GridBudget(this.components);
		this.statusBar = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
		this.imageList = new System.Windows.Forms.ImageList(this.components);
		this.panelBottom.SuspendLayout();
		this.panelTop.SuspendLayout();
		this.panelLevel.SuspendLayout();
		this.panelGrid.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gridMainItem).BeginInit();
		base.SuspendLayout();
		this.panelBottom.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.panelBottom.Controls.Add(this.lbCaution);
		this.panelBottom.Controls.Add(this.btnCancel);
		this.panelBottom.Controls.Add(this.btnOK);
		this.panelBottom.Controls.Add(this.groupBox1);
		this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panelBottom.Location = new System.Drawing.Point(0, 422);
		this.panelBottom.Name = "panelBottom";
		this.panelBottom.Size = new System.Drawing.Size(672, 44);
		this.panelBottom.TabIndex = 10;
		appearance17.ForeColor = System.Drawing.Color.FromArgb(0, 51, 153);
		this.lbCaution.Appearance = appearance17;
		this.lbCaution.Font = new System.Drawing.Font("新細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.lbCaution.Location = new System.Drawing.Point(10, 13);
		this.lbCaution.Name = "lbCaution";
		this.lbCaution.Size = new System.Drawing.Size(448, 27);
		this.lbCaution.TabIndex = 6;
		this.lbCaution.Text = "灰色底色項目為末項已有小計項的主項大類，不得取消勾選，欲刪除請於預算書中直接刪除該小計項。";
		this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance18.Image = resources.GetObject("appearance18.Image");
		appearance18.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnCancel.Appearance = appearance18;
		this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
		this.btnCancel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.Font = new System.Drawing.Font("細明體", 11f);
		this.btnCancel.ImageSize = new System.Drawing.Size(20, 20);
		this.btnCancel.ImageTransparentColor = System.Drawing.Color.White;
		this.btnCancel.Location = new System.Drawing.Point(579, 10);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.ShowFocusRect = false;
		this.btnCancel.ShowOutline = false;
		this.btnCancel.Size = new System.Drawing.Size(88, 31);
		this.btnCancel.SupportThemes = false;
		this.btnCancel.TabIndex = 5;
		this.btnCancel.Text = "取消";
		this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		appearance19.Image = resources.GetObject("appearance19.Image");
		appearance19.ImageHAlign = Infragistics.Win.HAlign.Left;
		appearance19.TextVAlign = Infragistics.Win.VAlign.Bottom;
		this.btnOK.Appearance = appearance19;
		this.btnOK.BackColor = System.Drawing.SystemColors.Control;
		this.btnOK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
		this.btnOK.Font = new System.Drawing.Font("細明體", 11f);
		this.btnOK.ImageSize = new System.Drawing.Size(20, 20);
		this.btnOK.ImageTransparentColor = System.Drawing.Color.White;
		this.btnOK.Location = new System.Drawing.Point(487, 10);
		this.btnOK.Name = "btnOK";
		this.btnOK.ShowFocusRect = false;
		this.btnOK.ShowOutline = false;
		this.btnOK.Size = new System.Drawing.Size(88, 31);
		this.btnOK.SupportThemes = false;
		this.btnOK.TabIndex = 4;
		this.btnOK.Text = "確定";
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(672, 8);
		this.groupBox1.TabIndex = 3;
		this.groupBox1.TabStop = false;
		this.panelTop.BackColor = System.Drawing.Color.White;
		this.panelTop.Controls.Add(this.panelLevel);
		this.panelTop.Controls.Add(this.lbInstruction);
		this.panelTop.Controls.Add(this.lbTitle);
		this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
		this.panelTop.Location = new System.Drawing.Point(0, 0);
		this.panelTop.Name = "panelTop";
		this.panelTop.Size = new System.Drawing.Size(672, 72);
		this.panelTop.TabIndex = 11;
		this.panelLevel.Controls.Add(this.levelSwitchButton);
		this.panelLevel.Location = new System.Drawing.Point(16, 48);
		this.panelLevel.Name = "panelLevel";
		this.panelLevel.Size = new System.Drawing.Size(171, 24);
		this.panelLevel.TabIndex = 17;
		this.levelSwitchButton.Location = new System.Drawing.Point(3, 2);
		this.levelSwitchButton.Name = "levelSwitchButton";
		this.levelSwitchButton.Size = new System.Drawing.Size(166, 22);
		this.levelSwitchButton.TabIndex = 0;
		this.levelSwitchButton.LevelSwitchButtonsClicked += new Archnowledge.Pcces.PccesMain.ArchControls.LevelSwitchButton.LevelSwitchButtonClickHandler(levelSwitchButton_LevelSwitchButtonsClicked);
		appearance20.BackColor = System.Drawing.Color.White;
		this.lbInstruction.Appearance = appearance20;
		this.lbInstruction.Location = new System.Drawing.Point(26, 29);
		this.lbInstruction.Name = "lbInstruction";
		this.lbInstruction.Size = new System.Drawing.Size(622, 20);
		this.lbInstruction.TabIndex = 6;
		this.lbInstruction.Text = "請勾選欲於子階末項增加小計項的主項大類。";
		appearance21.BackColor = System.Drawing.Color.White;
		this.lbTitle.Appearance = appearance21;
		this.lbTitle.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
		this.lbTitle.Location = new System.Drawing.Point(10, 8);
		this.lbTitle.Name = "lbTitle";
		this.lbTitle.Size = new System.Drawing.Size(408, 20);
		this.lbTitle.TabIndex = 5;
		this.lbTitle.Text = "自動增加小計項設定";
		this.panelGrid.Controls.Add(this.gridMainItem);
		this.panelGrid.Controls.Add(this.statusBar);
		this.panelGrid.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panelGrid.Location = new System.Drawing.Point(0, 72);
		this.panelGrid.Name = "panelGrid";
		this.panelGrid.Size = new System.Drawing.Size(672, 350);
		this.panelGrid.TabIndex = 12;
		this.gridMainItem._ExcelFileName = "";
		this.gridMainItem._ExcelSheeName = "";
		this.gridMainItem._IsOpenExcelAfterExport = false;
		this.gridMainItem.BackColor = System.Drawing.Color.FromArgb(237, 243, 254);
		this.gridMainItem.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
		this.gridMainItem.ColumnInfo = resources.GetString("gridMainItem.ColumnInfo");
		this.gridMainItem.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gridMainItem.ExtendLastCol = true;
		this.gridMainItem.Font = new System.Drawing.Font("細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		this.gridMainItem.ForeColor = System.Drawing.Color.Black;
		this.gridMainItem.Location = new System.Drawing.Point(0, 0);
		this.gridMainItem.Name = "gridMainItem";
		this.gridMainItem.Rows.Count = 1;
		this.gridMainItem.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
		this.gridMainItem.ShowCursor = true;
		this.gridMainItem.ShowSort = false;
		this.gridMainItem.ShowToolTipOnNarrowColumn = true;
		this.gridMainItem.Size = new System.Drawing.Size(672, 324);
		this.gridMainItem.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("gridMainItem.Styles"));
		this.gridMainItem.TabIndex = 1;
		this.gridMainItem.Tree.Column = 1;
		this.gridMainItem.Tree.LineColor = System.Drawing.Color.Gray;
		appearance22.FontData.SizeInPoints = 11f;
		this.statusBar.Appearance = appearance22;
		this.statusBar.Location = new System.Drawing.Point(0, 324);
		this.statusBar.Name = "statusBar";
		ultraStatusPanel7.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		appearance23.BackColor = System.Drawing.Color.FromArgb(192, 192, 255);
		appearance23.BackColor2 = System.Drawing.Color.Navy;
		appearance23.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
		ultraStatusPanel7.ProgressBarInfo.Appearance = appearance23;
		ultraStatusPanel7.Text = "資料筆數：";
		ultraStatusPanel7.Width = 200;
		ultraStatusPanel8.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
		appearance24.TextHAlign = Infragistics.Win.HAlign.Right;
		appearance24.TextVAlign = Infragistics.Win.VAlign.Bottom;
		ultraStatusPanel9.Appearance = appearance24;
		ultraStatusPanel9.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
		ultraStatusPanel9.Text = "客服電話：(02)2716-5561";
		ultraStatusPanel9.Width = 200;
		this.statusBar.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[3] { ultraStatusPanel7, ultraStatusPanel8, ultraStatusPanel9 });
		this.statusBar.Size = new System.Drawing.Size(672, 26);
		this.statusBar.TabIndex = 2;
		this.imageList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList.ImageStream");
		this.imageList.TransparentColor = System.Drawing.Color.White;
		this.imageList.Images.SetKeyName(0, "");
		this.AutoScaleBaseSize = new System.Drawing.Size(6, 18);
		base.CancelButton = this.btnCancel;
		base.ClientSize = new System.Drawing.Size(672, 466);
		base.Controls.Add(this.panelGrid);
		base.Controls.Add(this.panelTop);
		base.Controls.Add(this.panelBottom);
		this.Font = new System.Drawing.Font("新細明體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
		base.KeyPreview = true;
		base.MinimizeBox = false;
		base.Name = "FormBudgetAutoInsertSubtotalItem";
		base.ShowIcon = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "自動增加小計項設定";
		base.Load += new System.EventHandler(FormBudgetBidSet_Load);
		this.panelBottom.ResumeLayout(false);
		this.panelTop.ResumeLayout(false);
		this.panelLevel.ResumeLayout(false);
		this.panelGrid.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gridMainItem).EndInit();
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
